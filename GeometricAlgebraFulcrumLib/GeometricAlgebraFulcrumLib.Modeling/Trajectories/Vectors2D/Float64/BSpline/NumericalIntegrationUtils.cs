using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.BSpline
{
    /// <summary>
    /// Provides numerical integration methods with configurable precision and adaptive strategies.
    /// </summary>
    public static class NumericalIntegrationUtils
    {
        #region Gauss-Legendre Quadrature

        // Precomputed nodes & weights for various orders of Gauss-Legendre quadrature
        private static readonly Dictionary<int, (double[] nodes, double[] weights)> GaussLegendreData = new()
        {
            [8] = (
                new[] {
                    -0.960289856497536231683560868569,
                    -0.796666477413626739591553936476,
                    -0.525532409916328985817739049189,
                    -0.183434642495649804939476142360,
                    0.183434642495649804939476142360,
                    0.525532409916328985817739049189,
                    0.796666477413626739591553936476,
                    0.960289856497536231683560868569
                },
                new[] {
                    0.101228536290376259152531354310,
                    0.222381034453374470544355994426,
                    0.313706645877887287337962201987,
                    0.362683783378361982965150449277,
                    0.362683783378361982965150449277,
                    0.313706645877887287337962201987,
                    0.222381034453374470544355994426,
                    0.101228536290376259152531354310
                }
            ),
            [16] = (
                new[] {
                    -0.989400934991649932596154173450,
                    -0.944575023073232576077988415535,
                    -0.865631202387831743880467897712,
                    -0.755404408355003033895101194847,
                    -0.617876244402643748446671764049,
                    -0.458016777657227386342419442984,
                    -0.281603550779258913230460501460,
                    -0.095012509837637440185319335424,
                    0.095012509837637440185319335424,
                    0.281603550779258913230460501460,
                    0.458016777657227386342419442984,
                    0.617876244402643748446671764049,
                    0.755404408355003033895101194847,
                    0.865631202387831743880467897712,
                    0.944575023073232576077988415535,
                    0.989400934991649932596154173450
                },
                new[] {
                    0.027152459411754094851780572456,
                    0.062253523938647892862843836994,
                    0.095158511682492784809925107602,
                    0.124628971255533872052476282192,
                    0.149595988816576732081501730547,
                    0.169156519395002538189312079030,
                    0.182603415044923588866763667969,
                    0.189450610455068496285396723208,
                    0.189450610455068496285396723208,
                    0.182603415044923588866763667969,
                    0.169156519395002538189312079030,
                    0.149595988816576732081501730547,
                    0.124628971255533872052476282192,
                    0.095158511682492784809925107602,
                    0.062253523938647892862843836994,
                    0.027152459411754094851780572456
                }
            ),
            [32] = (
                new[] {
                    -0.997263861849481563544981128665,
                    -0.985611511545268335400175044631,
                    -0.964762255587506430773811928118,
                    -0.934906075937739689170919134835,
                    -0.896321155766052123965307243719,
                    -0.849367613732569970133693004968,
                    -0.794483795967942406963097298970,
                    -0.732182118740289680387426665091,
                    -0.663044266930215200975115168663,
                    -0.587715757240762329040745476402,
                    -0.506899908932229390023747474378,
                    -0.421351276130635345364119436172,
                    -0.331868602282127649779916805730,
                    -0.239287362252137074544603209166,
                    -0.144471961582796493485186373599,
                    -0.048307665687738316234812570440,
                    0.048307665687738316234812570440,
                    0.144471961582796493485186373599,
                    0.239287362252137074544603209166,
                    0.331868602282127649779916805730,
                    0.421351276130635345364119436172,
                    0.506899908932229390023747474378,
                    0.587715757240762329040745476402,
                    0.663044266930215200975115168663,
                    0.732182118740289680387426665091,
                    0.794483795967942406963097298970,
                    0.849367613732569970133693004968,
                    0.896321155766052123965307243719,
                    0.934906075937739689170919134835,
                    0.964762255587506430773811928118,
                    0.985611511545268335400175044631,
                    0.997263861849481563544981128665
                },
                new[] {
                    0.007018610009470096600407063739,
                    0.016274394730905670605170562206,
                    0.025392065309262059455752589789,
                    0.034273862913021433102687732252,
                    0.042835898022226680656878646606,
                    0.050998059262376176196163244690,
                    0.058684093478535547145283637300,
                    0.065822222776361846837650063706,
                    0.072345794108848506225399356478,
                    0.078193895787070306471740918828,
                    0.083311924226946755222199074604,
                    0.087652093004403811142771462751,
                    0.091173878695763884712868577111,
                    0.093844399080804565639180237668,
                    0.095638720079274859419082002204,
                    0.096540088514727800566764830064,
                    0.096540088514727800566764830064,
                    0.095638720079274859419082002204,
                    0.093844399080804565639180237668,
                    0.091173878695763884712868577111,
                    0.087652093004403811142771462751,
                    0.083311924226946755222199074604,
                    0.078193895787070306471740918828,
                    0.072345794108848506225399356478,
                    0.065822222776361846837650063706,
                    0.058684093478535547145283637300,
                    0.050998059262376176196163244690,
                    0.042835898022226680656878646606,
                    0.034273862913021433102687732252,
                    0.025392065309262059455752589789,
                    0.016274394730905670605170562206,
                    0.007018610009470096600407063739
                }
            )
        };

        /// <summary>
        /// Integrate function f on [a,b] using Gauss-Legendre quadrature of specified order.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GaussLegendre(Func<double, double> f, double a, double b, int order = 16)
        {
            if (!GaussLegendreData.TryGetValue(order, out var data))
                throw new ArgumentOutOfRangeException(nameof(order), "Unsupported Gauss-Legendre order.");

            var (nodes, weights) = data;
            var c = 0.5 * (a + b);
            var h = 0.5 * (b - a);
            var sum = 0.0;

            for (var i = 0; i < order; i++)
            {
                var x = c + h * nodes[i];
                sum += weights[i] * f(x);
            }

            return h * sum;
        }

        #endregion

        #region Adaptive Integration

        /// <summary>
        /// Adaptive integration using recursive subdivision with Gauss-Kronrod error estimation.
        /// </summary>
        public static double AdaptiveIntegrate(Func<double, double> f, double a, double b, 
            double tolerance = 1e-10, int maxDepth = 20)
        {
            if (maxDepth < 1) throw new ArgumentOutOfRangeException(nameof(maxDepth));
            if (tolerance <= 0) throw new ArgumentOutOfRangeException(nameof(tolerance));

            return AdaptiveIntegrateRecursive(f, a, b, tolerance, maxDepth, 0);
        }

        private static double AdaptiveIntegrateRecursive(Func<double, double> f, double a, double b,
            double tolerance, int maxDepth, int currentDepth)
        {
            if (currentDepth >= maxDepth)
            {
                // Fall back to high-order Gauss-Legendre when max depth reached
                return GaussLegendre(f, a, b, 32);
            }

            var mid = (a + b) / 2;
            
            // Compute integrals with different orders for error estimation
            var whole = GaussLegendre(f, a, b, 16);
            var left = GaussLegendre(f, a, mid, 16);
            var right = GaussLegendre(f, mid, b, 16);
            var composite = left + right;

            var error = Math.Abs(whole - composite);
            
            if (error < tolerance || currentDepth == maxDepth - 1)
            {
                // Use Richardson extrapolation for better accuracy
                return composite + (composite - whole) / 15.0;
            }

            return AdaptiveIntegrateRecursive(f, a, mid, tolerance / 2, maxDepth, currentDepth + 1) +
                   AdaptiveIntegrateRecursive(f, mid, b, tolerance / 2, maxDepth, currentDepth + 1);
        }

        #endregion

        #region Composite Integration

        /// <summary>
        /// Integrate function across multiple subintervals defined by breakpoints.
        /// </summary>
        public static double CompositeIntegrate(Func<double, double> f, double[] breakpoints, 
            double tolerance = 1e-10)
        {
            if (breakpoints == null || breakpoints.Length < 2)
                throw new ArgumentException("At least two breakpoints required.");

            var total = 0.0;
            for (var i = 0; i < breakpoints.Length - 1; i++)
            {
                var a = breakpoints[i];
                var b = breakpoints[i + 1];
                if (b <= a) continue;

                total += AdaptiveIntegrate(f, a, b, tolerance);
            }

            return total;
        }

        /// <summary>
        /// Integrate function across knot spans of a B-spline basis set.
        /// </summary>
        public static double IntegrateAcrossKnotSpans(Func<double, double> f, BSplineKnotVector knotVector,
            double a, double b, double tolerance = 1e-10)
        {
            if (knotVector == null) throw new ArgumentNullException(nameof(knotVector));
            if (b <= a) return 0.0;

            var knotValues = knotVector.GetDistinctKnotValues();
            var total = 0.0;

            for (var i = 0; i < knotValues.Length - 1; i++)
            {
                var spanStart = knotValues[i];
                var spanEnd = knotValues[i + 1];
                var intervalStart = Math.Max(spanStart, a);
                var intervalEnd = Math.Min(spanEnd, b);

                if (intervalEnd <= intervalStart) continue;

                total += AdaptiveIntegrate(f, intervalStart, intervalEnd, tolerance);
            }

            return total;
        }

        #endregion
    }
}