using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.BSpline
{
    public sealed class BSplineCurve3D : BSplineCurveBase<LinFloat64Vector3D>
    {
        internal BSplineCurve3D(BSplineBasisSet basisSet, IReadOnlyList<LinFloat64Vector3D> controlPoints)
            : base(basisSet, controlPoints)
        {
        }

        #region Vector Operations Implementation

        protected override LinFloat64Vector3D CreateZero() => LinFloat64Vector3D.Zero;

        protected override LinFloat64Vector3D CreateFromComponents(double[] components)
        {
            if (components.Length != 3)
                throw new ArgumentException("3D vector requires exactly 3 components");
            return LinFloat64Vector3D.Create(components[0], components[1], components[2]);
        }

        protected override LinFloat64Vector3D AddVectors(LinFloat64Vector3D a, LinFloat64Vector3D b)
            => LinFloat64Vector3D.Create(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        protected override LinFloat64Vector3D ScaleVector(LinFloat64Vector3D vector, double scalar)
            => LinFloat64Vector3D.Create(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);

        protected override double[] GetVectorComponents(LinFloat64Vector3D vector)
            => new[] { vector.X, vector.Y, vector.Z };

        #endregion

        #region Curve Evaluation

        public LinFloat64Vector3D GetPoint(double t) => Evaluate(t, 0);

        public LinFloat64Vector3D GetDerivative1(double t) => Evaluate(t, 1);

        public LinFloat64Vector3D GetDerivative2(double t) => Evaluate(t, 2);

        public LinFloat64Vector3D GetTangent(double t)
        {
            var derivative = GetDerivative1(t);
            var norm = derivative.L2Norm();
            return norm <= Epsilon
                ? LinFloat64Vector3D.Zero
                : LinFloat64Vector3D.Create(derivative.X / norm, derivative.Y / norm, derivative.Z / norm);
        }

        public double GetCurvature(double t)
        {
            var d1 = GetDerivative1(t);
            var d2 = GetDerivative2(t);
            var cross = d1.VectorCross(d2);
            var numerator = cross.L2Norm();
            var denominator = Math.Pow(d1.L2Norm(), 3.0);
            return denominator <= Epsilon ? 0.0 : numerator / denominator;
        }

        public LinFloat64Vector3D GetBinormal(double t)
        {
            var tangent = GetTangent(t);
            var d2 = GetDerivative2(t);
            var binormal = tangent.VectorCross(d2);
            var norm = binormal.L2Norm();
            return norm <= Epsilon ? LinFloat64Vector3D.Zero : binormal / norm;
        }

        public LinFloat64Vector3D GetNormal(double t)
        {
            var tangent = GetTangent(t);
            var binormal = GetBinormal(t);
            return binormal.VectorCross(tangent);
        }

        #endregion

        #region Factories

        public static BSplineCurve3D CreateInterpolating(
            IReadOnlyList<LinFloat64Vector3D> dataPoints,
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
                    return dx * dx + dy * dy + dz * dz;
                })
                : CreateUniformParameters(dataPoints.Count);

            // Solve interpolation system
            var (controlPoints, basisSet) = SolveInterpolationSystem(
                dataPoints,
                parameters,
                degree,
                p => new[] { p.X, p.Y, p.Z },
                comp => LinFloat64Vector3D.Create(comp[0], comp[1], comp[2]));

            return new BSplineCurve3D(basisSet, controlPoints);
        }

        public static BSplineCurve3D CreateRegression(
            IReadOnlyList<LinFloat64Vector3D> dataPoints,
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
                    return dx * dx + dy * dy + dz * dz;
                })
                : CreateUniformParameters(dataPoints.Count);

            // Solve least squares system
            var (controlPoints, basisSet) = SolveLeastSquaresSystem(
                dataPoints,
                parameters,
                degree,
                actualControlPointCount,
                p => new[] { p.X, p.Y, p.Z },
                comp => LinFloat64Vector3D.Create(comp[0], comp[1], comp[2]),
                clampEndpoints);

            return new BSplineCurve3D(basisSet, controlPoints);
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
    }
}