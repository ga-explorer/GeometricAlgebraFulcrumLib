using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.BSpline
{
    /// <summary>
    /// 4D B-spline curve (useful for homogeneous coordinates or 4D data).
    /// Includes fast point evaluation and interpolation / least-squares factories.
    /// </summary>
    public sealed class BSplineCurve4D : BSplineCurveBase<LinFloat64Vector4D>
    {
        internal BSplineCurve4D(BSplineBasisSet basisSet, IReadOnlyList<LinFloat64Vector4D> controlPoints)
            : base(basisSet, controlPoints)
        {
        }

        #region Vector Operations Implementation

        protected override LinFloat64Vector4D CreateZero() => LinFloat64Vector4D.Zero;

        protected override LinFloat64Vector4D CreateFromComponents(double[] components)
        {
            if (components.Length != 4)
                throw new ArgumentException("4D vector requires exactly 4 components");
            return LinFloat64Vector4D.Create(components[0], components[1], components[2], components[3]);
        }

        protected override LinFloat64Vector4D AddVectors(LinFloat64Vector4D a, LinFloat64Vector4D b)
            => LinFloat64Vector4D.Create(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);

        protected override LinFloat64Vector4D ScaleVector(LinFloat64Vector4D vector, double scalar)
            => LinFloat64Vector4D.Create(vector.X * scalar, vector.Y * scalar, vector.Z * scalar, vector.W * scalar);

        protected override double[] GetVectorComponents(LinFloat64Vector4D vector)
            => new[] { vector.X, vector.Y, vector.Z, vector.W };

        #endregion

        #region Curve Evaluation

        public LinFloat64Vector4D GetPoint(double t) => Evaluate(t, 0);

        public LinFloat64Vector4D GetDerivative1(double t) => Evaluate(t, 1);

        public LinFloat64Vector4D GetDerivative2(double t) => Evaluate(t, 2);

        public LinFloat64Vector4D GetTangent(double t)
        {
            var derivative = GetDerivative1(t);
            var norm = derivative.L2Norm();
            return norm <= Epsilon
                ? LinFloat64Vector4D.Zero
                : LinFloat64Vector4D.Create(
                    derivative.X / norm, 
                    derivative.Y / norm, 
                    derivative.Z / norm, 
                    derivative.W / norm);
        }

        #endregion

        #region Factories

        public static BSplineCurve4D CreateInterpolating(
            IReadOnlyList<LinFloat64Vector4D> dataPoints,
            int degree = 3,
            bool useCentripetal = true)
        {
            if (dataPoints == null) throw new ArgumentNullException(nameof(dataPoints));
            if (dataPoints.Count < 2) throw new ArgumentException("At least two points required.");
            if (degree < 1) throw new ArgumentOutOfRangeException(nameof(degree));

            // Parameterization
            var parameters = useCentripetal
                ? CreateCentripetalParameters(dataPoints, (p1, p2) =>
                {
                    var dx = p2.X - p1.X;
                    var dy = p2.Y - p1.Y;
                    var dz = p2.Z - p1.Z;
                    var dw = p2.W - p1.W;
                    return dx * dx + dy * dy + dz * dz + dw * dw;
                })
                : CreateUniformParameters(dataPoints.Count);

            // Solve interpolation system
            var (controlPoints, basisSet) = SolveInterpolationSystem(
                dataPoints,
                parameters,
                degree,
                p => new[] { p.X, p.Y, p.Z, p.W },
                comp => LinFloat64Vector4D.Create(comp[0], comp[1], comp[2], comp[3]));

            return new BSplineCurve4D(basisSet, controlPoints);
        }

        public static BSplineCurve4D CreateRegression(
            IReadOnlyList<LinFloat64Vector4D> dataPoints,
            int degree = 3,
            int controlPointCount = -1,
            bool clampEndpoints = true,
            bool useCentripetal = true)
        {
            if (dataPoints == null) throw new ArgumentNullException(nameof(dataPoints));
            if (dataPoints.Count < 2) throw new ArgumentException("At least two points required.");
            if (degree < 1) throw new ArgumentOutOfRangeException(nameof(degree));

            var actualControlPointCount = controlPointCount > 0 ? controlPointCount :
                Math.Max(degree + 1, dataPoints.Count / 2 + 1);

            if (actualControlPointCount <= degree)
                throw new ArgumentException("Control point count must be greater than degree.");

            // Parameterization
            var parameters = useCentripetal
                ? CreateCentripetalParameters(dataPoints, (p1, p2) =>
                {
                    var dx = p2.X - p1.X;
                    var dy = p2.Y - p1.Y;
                    var dz = p2.Z - p1.Z;
                    var dw = p2.W - p1.W;
                    return dx * dx + dy * dy + dz * dz + dw * dw;
                })
                : CreateUniformParameters(dataPoints.Count);

            // Solve least squares system
            var (controlPoints, basisSet) = SolveLeastSquaresSystem(
                dataPoints,
                parameters,
                degree,
                actualControlPointCount,
                p => new[] { p.X, p.Y, p.Z, p.W },
                comp => LinFloat64Vector4D.Create(comp[0], comp[1], comp[2], comp[3]),
                clampEndpoints);

            return new BSplineCurve4D(basisSet, controlPoints);
        }

        private static double[] CreateUniformParameters(int pointCount)
        {
            var parameters = new double[pointCount];
            for (var i = 0; i < pointCount; i++)
            {
                parameters[i] = (double)i / (pointCount - 1);
            }
            return parameters;
        }

        #endregion

        #region Homogeneous Coordinate Support

        /// <summary>
        /// Convert from homogeneous coordinates to 3D by perspective division
        /// </summary>
        public LinFloat64Vector3D GetPoint3D(double t, bool normalize = true)
        {
            var point4D = GetPoint(t);
            if (normalize && Math.Abs(point4D.W) > Epsilon)
            {
                return LinFloat64Vector3D.Create(
                    point4D.X / point4D.W,
                    point4D.Y / point4D.W,
                    point4D.Z / point4D.W);
            }
            else
            {
                return LinFloat64Vector3D.Create(point4D.X, point4D.Y, point4D.Z);
            }
        }

        /// <summary>
        /// Check if the curve represents rational B-spline weights (all W > 0)
        /// </summary>
        public bool IsRational()
        {
            return ControlPoints.All(cp => cp.W > 0);
        }

        #endregion
    }
}