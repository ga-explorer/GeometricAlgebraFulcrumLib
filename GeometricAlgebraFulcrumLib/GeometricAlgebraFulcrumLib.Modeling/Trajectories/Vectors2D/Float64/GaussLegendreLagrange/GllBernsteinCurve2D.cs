using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Bernstein;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Chebyshev;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.GaussLegendreLagrange
{
    /// <summary>
    /// Gauss–Legendre–Lagrange (GLL) 2D curve implemented in stable Chebyshev-Bernstein hybrid form.
    /// Based on:
    /// "A novel method for manipulating polynomial curves by the
    /// Gauss–Legendre control polygon with points interpolating property"
    /// </summary>
    public sealed class GllBernsteinCurve2D : Float64Path2D
    {
        private readonly LinFloat64Vector2D[] _pPoints;
        private readonly LinFloat64Vector2D[] _qPoints;
        private readonly double[] _tau;
        private readonly double[] _weights;
        private readonly BernsteinPolynomial[] _basis;
        private readonly BernsteinPolynomial[] _basisDeriv1;
        private readonly BernsteinPolynomial[] _basisDeriv2;
        private readonly int _n;

        // -------------------------------------------------------------------------
        // Factory methods
        // -------------------------------------------------------------------------
        public static GllBernsteinCurve2D CreateFromPoints(
            IReadOnlyList<LinFloat64Vector2D> pPoints,
            IReadOnlyList<LinFloat64Vector2D> qPoints)
        {
            if (pPoints == null || qPoints == null)
                throw new ArgumentNullException();

            var n = pPoints.Count - 1;
            if (qPoints.Count != n)
                throw new ArgumentException("qPoints length must equal n.");

            NodesWeights(n, out var tau, out var weights);
            return new GllBernsteinCurve2D(pPoints.ToArray(), qPoints.ToArray(), tau, weights);
        }

        public static GllBernsteinCurve2D CreateFromWaypoints(IReadOnlyList<LinFloat64Vector2D> waypoints)
        {
            if (waypoints == null)
                throw new ArgumentNullException(nameof(waypoints));
            if (waypoints.Count < 3)
                throw new ArgumentException("At least 3 waypoints required (n≥2).");

            var n = waypoints.Count - 1;
            NodesWeights(n, out var tau, out var weights);

            var qPoints = waypoints.Skip(1).Take(n).ToArray();
            var pPoints = new LinFloat64Vector2D[n + 1];
            pPoints[0] = waypoints[0];
            pPoints[n] = waypoints.Last();

            for (var i = 1; i < n; i++)
                pPoints[i] = 0.5 * (qPoints[i - 1] + qPoints[i]);

            return new GllBernsteinCurve2D(pPoints, qPoints, tau, weights);
        }

        private GllBernsteinCurve2D(
            LinFloat64Vector2D[] pPoints,
            LinFloat64Vector2D[] qPoints,
            double[] tau,
            double[] weights,
            Float64ScalarRange? timeRange = null)
            : base(timeRange ?? Float64ScalarRange.NegativeOneToOne, false)
        {
            _n = tau.Length;
            _pPoints = pPoints;
            _qPoints = qPoints;
            _tau = tau;
            _weights = weights;

            // Stable Chebyshev-based construction
            _basis = BuildChebyshevBasedGllBasis(_tau, _weights);
            _basisDeriv1 = _basis.Select(b => b.Derivative()).ToArray();
            _basisDeriv2 = _basisDeriv1.Select(b => b.Derivative()).ToArray();
        }

        // -------------------------------------------------------------------------
        // Gauss–Legendre nodes and weights
        // -------------------------------------------------------------------------
        private static void NodesWeights(int n, out double[] nodes, out double[] weights)
        {
            nodes = new double[n];
            weights = new double[n];

            for (var i = 0; i < n; ++i)
            {
                var x = Math.Cos(Math.PI * (4 * i + 3) / (4 * n + 2));
                double dx;

                do
                {
                    var p0 = 1.0;
                    var p1 = x;
                    for (var k = 2; k <= n; ++k)
                    {
                        var pk = ((2.0 * k - 1.0) * x * p1 - (k - 1.0) * p0) / k;
                        p0 = p1;
                        p1 = pk;
                    }
                    var pn = p1;
                    var pnm1 = p0;
                    var dpn = n * (x * pn - pnm1) / (x * x - 1.0);
                    dx = pn / dpn;
                    x -= dx;
                } while (Math.Abs(dx) > 1e-14);

                nodes[i] = x;

                var p0w = 1.0;
                var p1w = x;
                for (var k = 2; k <= n; ++k)
                {
                    var pk = ((2.0 * k - 1.0) * x * p1w - (k - 1.0) * p0w) / k;
                    p0w = p1w;
                    p1w = pk;
                }

                var pnw = p1w;
                var pnm1w = p0w;
                var dpnw = n * (x * pnw - pnm1w) / (x * x - 1.0);
                weights[i] = 2.0 / ((1 - x * x) * dpnw * dpnw);
            }

            Array.Sort(nodes, weights);
        }

        // -------------------------------------------------------------------------
        // Chebyshev-based Basis Construction
        // -------------------------------------------------------------------------
        private static BernsteinPolynomial[] BuildChebyshevBasedGllBasis(double[] tau, double[] weights)
        {
            var n = tau.Length;
            var basis = new BernsteinPolynomial[2 * n + 1];

            // --- f₀(t): product (t - τᵢ)² / (1 + τᵢ)² in Chebyshev form
            var f0 = ChebyshevPolynomialT.FromRoots(tau)
                        .Square()
                        .Scale(1.0 / tau.Select(t => (1.0 + t) * (1.0 + t)).Aggregate((a, b) => a * b))
                        .ToBernstein();

            // --- fₖ(t), gₖ(t)
            var fk = new BernsteinPolynomial[n + 1];
            var gk = new BernsteinPolynomial[n + 1];

            for (var k = 1; k <= n; k++)
            {
                var tauK = tau[k - 1];
                var GkCheb = BuildChebyshevGk(tau, k);
                var GkBern = GkCheb.ToBernstein();

                // fₖ(t) = (t - τₖ) Gₖ(t)
                var tMinusTau = ChebyshevPolynomialT.CreateLinear(-tauK, 1.0).ToBernstein();
                fk[k] = (tMinusTau * GkBern).Simplify();

                // gₖ(t) = Gₖ(t)[1 - (t - τₖ) Gₖ'(τₖ)]
                var GkPrimeAtTau = GkCheb.Derivative().EvaluateAt(tauK);
                var correction = BernsteinPolynomial.Constant(1.0) - (tMinusTau * GkPrimeAtTau);
                gk[k] = (GkBern * correction).Simplify();
            }

            // --- tilde fᵢ
            var tildeF = new BernsteinPolynomial[n + 2];
            for (var i = 1; i <= n; i++)
                tildeF[i] = fk[i] / weights[i - 1];
            tildeF[n + 1] = BernsteinPolynomial.Constant(0.0);

            // --- assemble Fᵢ basis
            basis[0] = (f0 - tildeF[1]).Simplify();
            for (var j = 1; j <= n; j++)
            {
                basis[2 * j - 1] = gk[j];
                basis[2 * j] = (tildeF[j] - tildeF[j + 1]).Simplify();
            }

            return BernsteinPolynomial.SimplifyToEqualDegree(basis);
        }

        /// <summary>
        /// Build Gₖ(t) in Chebyshev form: Gₖ(t) = (t + 1)/(τₖ + 1) ∏_{i≠k} (t - τᵢ)²/(τₖ - τᵢ)²
        /// </summary>
        private static ChebyshevPolynomialT BuildChebyshevGk(double[] tau, int k)
        {
            var tauK = tau[k - 1];
            var prod = ChebyshevPolynomialT.Constant(1.0);

            for (var i = 0; i < tau.Length; i++)
            {
                if (i == k - 1) continue;

                var factor = ChebyshevPolynomialT.CreateLinear(-tau[i], 1.0);
                var term = factor.Square().Scale(1.0 / ((tauK - tau[i]) * (tauK - tau[i])));
                prod *= term;
            }

            var tPlus1 = ChebyshevPolynomialT.CreateLinear(1.0, 1.0);
            return (tPlus1 / (tauK + 1.0)) * prod;
        }

        // -------------------------------------------------------------------------
        // Evaluation
        // -------------------------------------------------------------------------
        private static double MapTo01(double t) => (t + 1.0) * 0.5;

        public override LinFloat64Vector2D GetValue(double t)
        {
            var u = MapTo01(t);
            var res = _pPoints[0] * _basis[0].GetValue(u);

            for (var i = 1; i <= _n; ++i)
            {
                res += _qPoints[i - 1] * _basis[2 * i - 1].GetValue(u);
                res += _pPoints[i] * _basis[2 * i].GetValue(u);
            }

            return res;
        }

        public override LinFloat64Vector2D GetDerivative1Value(double t)
        {
            var u = MapTo01(t);
            const double du_dt = 0.5;

            var res = _pPoints[0] * _basisDeriv1[0].GetValue(u);
            for (var i = 1; i <= _n; ++i)
            {
                res += _qPoints[i - 1] * _basisDeriv1[2 * i - 1].GetValue(u);
                res += _pPoints[i] * _basisDeriv1[2 * i].GetValue(u);
            }

            return res * du_dt;
        }

        public override LinFloat64Vector2D GetDerivative2Value(double t)
        {
            var u = MapTo01(t);
            const double du_dt2 = 0.25;

            var res = _pPoints[0] * _basisDeriv2[0].GetValue(u);
            for (var i = 1; i <= _n; ++i)
            {
                res += _qPoints[i - 1] * _basisDeriv2[2 * i - 1].GetValue(u);
                res += _pPoints[i] * _basisDeriv2[2 * i].GetValue(u);
            }

            return res * du_dt2;
        }

        public override Float64Path2D ToFinitePath() => this;
        public override Float64Path2D ToPeriodicPath() => this;

        public override bool IsValid()
        {
            for (var u = 0.0; u <= 1.0; u += 0.2)
            {
                var sum = _basis.Sum(b => b.GetValue(u));
                if (Math.Abs(sum - 1.0) > 1e-6)
                    return false;
            }
            return true;
        }

        public static GllBernsteinCurve2D Example()
        {
            var waypoints = new[]
            {
                LinFloat64Vector2D.Create(0, 0),
                LinFloat64Vector2D.Create(1, 1),
                LinFloat64Vector2D.Create(2, 0),
                LinFloat64Vector2D.Create(3, 1)
            };
            return CreateFromWaypoints(waypoints);
        }
    }
}


//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
//using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
//using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;

//namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.GaussLegendreLagrange
//{
//    /// <summary>
//    /// Gauss–Legendre–Lagrange (GLL) 2D curve implemented fully in Bernstein basis.
//    /// Based on:
//    /// “A novel method for manipulating polynomial curves by the
//    /// Gauss–Legendre control polygon with points interpolating property.”
//    /// 
//    /// Mathematical mapping:
//    ///   t ∈ [-1,1]   physical parameter
//    ///   u = (t+1)/2 ∈ [0,1]   Bernstein parameter
//    /// All basis functions Fᵢ^{2n}(t(u)) are expressed directly in Bernstein form.
//    /// </summary>
//    public sealed class GllBernsteinCurve2D : Float64Path2D
//    {
//        private readonly LinFloat64Vector2D[] _pPoints;
//        private readonly LinFloat64Vector2D[] _qPoints;
//        private readonly double[] _tau;
//        private readonly double[] _weights;
//        private readonly BernsteinPolynomial[] _basis;
//        private readonly BernsteinPolynomial[] _basisDeriv1;
//        private readonly BernsteinPolynomial[] _basisDeriv2;
//        private readonly int _n;

//        // -------------------------------------------------------------------------
//        // Factory methods
//        // -------------------------------------------------------------------------
//        public static GllBernsteinCurve2D CreateFromPoints(
//            IReadOnlyList<LinFloat64Vector2D> pPoints,
//            IReadOnlyList<LinFloat64Vector2D> qPoints)
//        {
//            if (pPoints == null || qPoints == null)
//                throw new ArgumentNullException();
//            var n = pPoints.Count - 1;
//            if (qPoints.Count != n)
//                throw new ArgumentException("qPoints length must equal n.");
//            NodesWeights(n, out var tau, out var weights);
//            return new GllBernsteinCurve2D(pPoints.ToArray(), qPoints.ToArray(), tau, weights);
//        }

//        public static GllBernsteinCurve2D CreateFromWaypoints(IReadOnlyList<LinFloat64Vector2D> waypoints)
//        {
//            if (waypoints == null)
//                throw new ArgumentNullException(nameof(waypoints));
//            if (waypoints.Count < 3)
//                throw new ArgumentException("At least 3 waypoints required (n>=2).");

//            var n = waypoints.Count - 1;
//            NodesWeights(n, out var tau, out var weights);

//            var qPoints = waypoints.Skip(1).Take(n).ToArray();
//            var pPoints = new LinFloat64Vector2D[n + 1];
//            pPoints[0] = waypoints[0];
//            pPoints[n] = waypoints.Last();

//            for (var i = 1; i < n; i++)
//                pPoints[i] = 0.5 * (qPoints[i - 1] + qPoints[i]);

//            return new GllBernsteinCurve2D(pPoints, qPoints, tau, weights);
//        }

//        private GllBernsteinCurve2D(
//            LinFloat64Vector2D[] pPoints,
//            LinFloat64Vector2D[] qPoints,
//            double[] tau,
//            double[] weights,
//            Float64ScalarRange? timeRange = null)
//            : base(timeRange ?? Float64ScalarRange.NegativeOneToOne, false)
//        {
//            _n = tau.Length;
//            _pPoints = pPoints;
//            _qPoints = qPoints;
//            _tau = tau;
//            _weights = weights;
//            _basis = BuildGllBasis(_tau, _weights);
//            _basisDeriv1 = _basis.Select(b => b.Derivative()).ToArray();
//            _basisDeriv2 = _basisDeriv1.Select(b => b.Derivative()).ToArray();
//        }

//        // -------------------------------------------------------------------------
//        // Gauss–Legendre node & weight computation
//        // -------------------------------------------------------------------------
//        private static void NodesWeights(int n, out double[] nodes, out double[] weights)
//        {
//            nodes = new double[n];
//            weights = new double[n];

//            for (var i = 0; i < n; ++i)
//            {
//                var x = Math.Cos(Math.PI * (4 * i + 3) / (4 * n + 2));
//                double dx;
//                do
//                {
//                    var p0 = 1.0;
//                    var p1 = x;
//                    for (var k = 2; k <= n; ++k)
//                    {
//                        var pk = ((2.0 * k - 1.0) * x * p1 - (k - 1.0) * p0) / k;
//                        p0 = p1; p1 = pk;
//                    }
//                    var pn = p1;
//                    var pnm1 = p0;
//                    var dpn = n * (x * pn - pnm1) / (x * x - 1.0);
//                    dx = pn / dpn;
//                    x -= dx;
//                } while (Math.Abs(dx) > 1e-14);

//                nodes[i] = x;

//                var p0w = 1.0;
//                var p1w = x;
//                for (var k = 2; k <= n; ++k)
//                {
//                    var pk = ((2.0 * k - 1.0) * x * p1w - (k - 1.0) * p0w) / k;
//                    p0w = p1w; p1w = pk;
//                }
//                var pnw = p1w;
//                var pnm1w = p0w;
//                var dpnw = n * (x * pnw - pnm1w) / (x * x - 1.0);
//                weights[i] = 2.0 / ((1 - x * x) * dpnw * dpnw);
//            }

//            Array.Sort(nodes, weights);
//        }

//        // -------------------------------------------------------------------------
//        // Core basis construction
//        // -------------------------------------------------------------------------
//        private static BernsteinPolynomial[] BuildGllBasis(double[] tau, double[] w)
//        {
//            var n = tau.Length;
//            var f0 = BuildF0(tau);
//            var fk = new BernsteinPolynomial[n + 1];
//            var gk = new BernsteinPolynomial[n + 1];

//            for (var k = 1; k <= n; k++)
//            {
//                var Gk = BuildGkCore(tau, k);
//                fk[k] = BuildFk(tau[k - 1], Gk);
//                gk[k] = BuildGk(tau[k - 1], Gk);
//            }

//            var tildeF = new BernsteinPolynomial[n + 2];
//            for (var i = 1; i <= n; i++)
//                tildeF[i] = fk[i] / w[i - 1];
//            tildeF[n + 1] = BernsteinPolynomial.Constant(0.0);

//            var basis = new BernsteinPolynomial[2 * n + 1];
//            basis[0] = (f0 - tildeF[1]).Simplify();
//            for (var j = 1; j <= n; j++)
//            {
//                basis[2 * j - 1] = gk[j].Simplify();
//                basis[2 * j] = (tildeF[j] - tildeF[j + 1]).Simplify();
//            }

//            return BernsteinPolynomial.SimplifyToEqualDegree(basis);
//        }

//        // --- Core polynomial builders ---
//        private static BernsteinPolynomial BuildF0(double[] tau)
//        {
//            var p = BernsteinPolynomial.Constant(1.0);
//            foreach (var taui in tau)
//            {
//                var lin = BernsteinPolynomial.Create(-1.0 - taui, 1.0 - taui);
//                p *= lin.Square() / ((1.0 + taui) * (1.0 + taui));
//            }
//            return p;
//        }

//        private static BernsteinPolynomial BuildGkCore(double[] tau, int k)
//        {
//            var tauk = tau[k - 1];
//            var G = BernsteinPolynomial.Constant(1.0);

//            for (var i = 0; i < tau.Length; i++)
//            {
//                if (i == k - 1) continue;
//                var lin = BernsteinPolynomial.Create(-1.0 - tau[i], 1.0 - tau[i]);
//                G *= lin.Square() / ((tauk - tau[i]) * (tauk - tau[i]));
//            }

//            var tPlus1 = BernsteinPolynomial.Create(0.0, 2.0);
//            return G * (tPlus1 / (tauk + 1.0));
//        }

//        private static BernsteinPolynomial BuildFk(double tauk, BernsteinPolynomial Gk)
//        {
//            var lin = BernsteinPolynomial.Create(-1.0 - tauk, 1.0 - tauk);
//            return lin * Gk;
//        }

//        private static BernsteinPolynomial BuildGk(double tauk, BernsteinPolynomial Gk)
//        {
//            var u_k = (tauk + 1.0) * 0.5;
//            // numerical derivative of Gk wrt t ≈ (G(u+du)-G(u-du))/du * du/dt scaling
//            const double eps = 1e-6;
//            var du = eps;
//            var G_plus = Gk.EvaluateAt(u_k + du);
//            var G_minus = Gk.EvaluateAt(u_k - du);
//            var dG_dt = (G_plus - G_minus) / (2 * du) * 2.0; // dt/du=2

//            var tMinusTau = BernsteinPolynomial.Create(-1.0 - tauk, 1.0 - tauk);
//            var correction = BernsteinPolynomial.Constant(1.0) - (tMinusTau * dG_dt);
//            return (Gk * correction).Simplify();
//        }

//        // -------------------------------------------------------------------------
//        // Evaluation
//        // -------------------------------------------------------------------------
//        private static double MapTo01(double t) => (t + 1.0) * 0.5;

//        public override LinFloat64Vector2D GetValue(double t)
//        {
//            var u = MapTo01(t);
//            var res = _pPoints[0] * _basis[0].EvaluateAt(u);
//            for (var i = 1; i <= _n; i++)
//            {
//                res += _qPoints[i - 1] * _basis[2 * i - 1].EvaluateAt(u);
//                res += _pPoints[i] * _basis[2 * i].EvaluateAt(u);
//            }
//            return res;
//        }

//        public override LinFloat64Vector2D GetDerivative1Value(double t)
//        {
//            var u = MapTo01(t);
//            const double du_dt = 0.5;
//            var res = _pPoints[0] * _basisDeriv1[0].EvaluateAt(u);
//            for (var i = 1; i <= _n; i++)
//            {
//                res += _qPoints[i - 1] * _basisDeriv1[2 * i - 1].EvaluateAt(u);
//                res += _pPoints[i] * _basisDeriv1[2 * i].EvaluateAt(u);
//            }
//            return res * du_dt;
//        }

//        public override LinFloat64Vector2D GetDerivative2Value(double t)
//        {
//            var u = MapTo01(t);
//            const double du_dt2 = 0.25;
//            var res = _pPoints[0] * _basisDeriv2[0].EvaluateAt(u);
//            for (var i = 1; i <= _n; i++)
//            {
//                res += _qPoints[i - 1] * _basisDeriv2[2 * i - 1].EvaluateAt(u);
//                res += _pPoints[i] * _basisDeriv2[2 * i].EvaluateAt(u);
//            }
//            return res * du_dt2;
//        }

//        public override Float64Path2D ToFinitePath() => this;
//        public override Float64Path2D ToPeriodicPath() => this;

//        public override bool IsValid()
//        {
//            for (var u = 0.0; u <= 1.0; u += 0.2)
//            {
//                var sum = _basis.Sum(b => b.EvaluateAt(u));
//                if (Math.Abs(sum - 1.0) > 1e-6)
//                    return false;
//            }
//            return true;
//        }

//        public static GllBernsteinCurve2D Example()
//        {
//            var waypoints = new[]
//            {
//                LinFloat64Vector2D.Create(0, 0),
//                LinFloat64Vector2D.Create(1, 1),
//                LinFloat64Vector2D.Create(2, 0),
//                LinFloat64Vector2D.Create(3, 1)
//            };
//            return CreateFromWaypoints(waypoints);
//        }
//    }
//}