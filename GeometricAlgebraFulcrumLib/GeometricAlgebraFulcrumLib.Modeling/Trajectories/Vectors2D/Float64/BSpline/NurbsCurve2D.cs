using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.BSpline
{
    /// <summary>
    /// Rational B-spline curve (NURBS) in 2D.
    /// Control points provided as LinFloat64Vector2D with corresponding weights.
    /// </summary>
    public sealed class NurbsCurve2D
    {
        public BSplineBasisSet BasisSet { get; }
        public IReadOnlyList<LinFloat64Vector2D> ControlPoints { get; }
        public IReadOnlyList<double> Weights { get; }

        private const double Eps = 1e-14;

        /// <summary>
        /// Create from control points and weights (must match in length).
        /// Optionally provide a knot vector; otherwise creates a simple clamped uniform knot vector.
        /// </summary>
        public NurbsCurve2D(IEnumerable<LinFloat64Vector2D> controlPoints, IEnumerable<double> weights, int degree, BSplineKnotVector? knotVector = null)
        {
            var pts = controlPoints?.ToArray() ?? throw new ArgumentNullException(nameof(controlPoints));
            var ws = weights?.ToArray() ?? throw new ArgumentNullException(nameof(weights));
            if (pts.Length != ws.Length) throw new ArgumentException("Control points and weights length must match.");

            ControlPoints = pts;
            Weights = ws;

            knotVector ??= BSplineKnotVector.CreateSimpleClamped(0, 1, pts.Length - degree + 1, degree);
            BasisSet = knotVector.CreateBSplineBasisSet(degree);
        }

        /// <summary>
        /// Evaluate homogeneous numerator C(t) = sum N_i(t) w_i P_i (2D vector).
        /// </summary>
        private LinFloat64Vector2D ComputeNumerator(double t, bool derivative1, bool derivative2, out double W, out double Wd1, out double Wd2, out LinFloat64Vector2D C, out LinFloat64Vector2D Cd1, out LinFloat64Vector2D Cd2)
        {
            var n = ControlPoints.Count;
            C = LinFloat64Vector2D.Zero;
            Cd1 = LinFloat64Vector2D.Zero;
            Cd2 = LinFloat64Vector2D.Zero;
            W = 0.0;
            Wd1 = 0.0;
            Wd2 = 0.0;

            for (var i = 0; i < n; i++)
            {
                var Ni = BasisSet.GetValue(i, t);
                var wi = Weights[i];
                var wiNi = wi * Ni;

                C += ControlPoints[i] * wiNi;
                W += wiNi;

                if (derivative1 || derivative2)
                {
                    var Ni1 = BasisSet.GetDerivative(i, t);
                    var wiNi1 = wi * Ni1;
                    Cd1 += ControlPoints[i] * wiNi1;
                    Wd1 += wiNi1;
                }

                if (derivative2)
                {
                    var Ni2 = BasisSet.GetSecondDerivative(i, t);
                    var wiNi2 = wi * Ni2;
                    Cd2 += ControlPoints[i] * wiNi2;
                    Wd2 += wiNi2;
                }
            }

            return C;
        }

        /// <summary>
        /// Get rational point r(t).
        /// </summary>
        public LinFloat64Vector2D GetPoint(double t)
        {
            ComputeNumerator(t, derivative1: false, derivative2: false,
                out var W, out var Wd1, out var Wd2,
                out var C, out var Cd1, out var Cd2);

            if (Math.Abs(W) < Eps) throw new InvalidOperationException("Denominator W nearly zero at t.");

            return C / W;
        }

        /// <summary>
        /// First derivative r'(t).
        /// </summary>
        public LinFloat64Vector2D GetDerivative1(double t)
        {
            ComputeNumerator(t, derivative1: true, derivative2: false,
                out var W, out var Wd1, out var Wd2,
                out var C, out var Cd1, out var Cd2);

            if (Math.Abs(W) < Eps) throw new InvalidOperationException("Denominator W nearly zero at t.");

            var numerator = Cd1 * W - C * Wd1;
            var denom = W * W;
            return numerator / denom;
        }

        /// <summary>
        /// Second derivative r''(t).
        /// Implements r'' = (C''W - C W'')/W^2 - 2 (C'W - C W') W' / W^3.
        /// </summary>
        public LinFloat64Vector2D GetDerivative2(double t)
        {
            ComputeNumerator(t, derivative1: true, derivative2: true,
                out var W, out var Wd1, out var Wd2,
                out var C, out var Cd1, out var Cd2);

            if (Math.Abs(W) < Eps) throw new InvalidOperationException("Denominator W nearly zero at t.");

            var term1 = (Cd2 * W - C * Wd2) / (W * W);
            var term2 = 2.0 * ((Cd1 * W - C * Wd1) * Wd1) / (W * W * W);

            return term1 - term2;
        }

        /// <summary>
        /// Unit tangent vector (normalized first derivative).
        /// </summary>
        public LinFloat64Vector2D GetTangent(double t)
        {
            var d = GetDerivative1(t);
            var norm = d.L2Norm();
            return norm.IsNearZero() ? LinFloat64Vector2D.Zero : d / norm;
        }

        /// <summary>
        /// Signed curvature for planar NURBS: (x' y'' - y' x'') / |r'|^3
        /// </summary>
        public double GetCurvature(double t)
        {
            var d1 = GetDerivative1(t);
            var d2 = GetDerivative2(t);

            var num = d1.X * d2.Y - d1.Y * d2.X;
            var denom = Math.Pow(d1.L2Norm(), 3.0);
            if (denom.IsNearZero()) return 0.0;
            return num / denom;
        }
    }
}
