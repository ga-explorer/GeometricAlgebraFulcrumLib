namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Chebyshev
{
    /// <summary>
    /// Represents a Chebyshev polynomial of the second kind (U_n) expansion:
    /// f(x) = Σ b_k * U_k(x),  x ∈ [-1,1].
    /// </summary>
    public class ChebyshevPolynomialU
    {
        private static readonly Dictionary<int, double[]> UPowerCache = new();

        private static double[] ExpandUkToPowerCached(int k)
        {
            lock (UPowerCache)
            {
                if (UPowerCache.TryGetValue(k, out var poly))
                    return poly;

                if (k == 0) poly = new[] { 1.0 };
                else if (k == 1) poly = new[] { 0.0, 2.0 };
                else
                {
                    var Uk1 = ExpandUkToPowerCached(k - 1);
                    var Uk2 = ExpandUkToPowerCached(k - 2);
                    poly = new double[k + 1];
                    for (var i = 0; i < Uk1.Length; i++) poly[i + 1] += 2.0 * Uk1[i];
                    for (var i = 0; i < Uk2.Length; i++) poly[i] -= Uk2[i];
                }

                UPowerCache[k] = poly;
                return poly;
            }
        }

        public int Degree { get; }
        public double[] Coeffs { get; }

        public ChebyshevPolynomialU(int degree, double[] coeffs)
        {
            if (coeffs == null) throw new ArgumentNullException(nameof(coeffs));
            if (coeffs.Length != degree + 1)
                throw new ArgumentException("coeffs length must equal degree+1");
            Degree = degree;
            Coeffs = (double[])coeffs.Clone();
        }

        // ---------------------------------------------------------------------
        // Factory helpers
        // ---------------------------------------------------------------------
        public static ChebyshevPolynomialU Constant(double c) => new(0, new[] { c });

        public static ChebyshevPolynomialU CreateLinear(double a, double b)
        {
            // U0=1, U1=2x → a + b·x ≈ a·U0 + (b/2)·U1
            return new ChebyshevPolynomialU(1, new[] { a, b / 2.0 });
        }

        // ---------------------------------------------------------------------
        // Core evaluation
        // ---------------------------------------------------------------------
        public double EvaluateAt(double x)
        {
            double b_kplus1 = 0.0, b_kplus2 = 0.0;
            for (var k = Degree; k >= 0; k--)
            {
                var b_k = 2.0 * x * b_kplus1 - b_kplus2 + Coeffs[k];
                b_kplus2 = b_kplus1;
                b_kplus1 = b_k;
            }
            return b_kplus1;
        }

        // ---------------------------------------------------------------------
        // Basic operations
        // ---------------------------------------------------------------------
        public ChebyshevPolynomialU Scale(double s)
        {
            var c = Coeffs.Select(a => a * s).ToArray();
            return new ChebyshevPolynomialU(Degree, c);
        }

        public ChebyshevPolynomialU Square() => this * this;

        public static ChebyshevPolynomialU operator +(ChebyshevPolynomialU a, ChebyshevPolynomialU b)
        {
            var n = Math.Max(a.Degree, b.Degree);
            var c = new double[n + 1];
            for (int i = 0; i <= n; i++)
            {
                var av = i <= a.Degree ? a.Coeffs[i] : 0.0;
                var bv = i <= b.Degree ? b.Coeffs[i] : 0.0;
                c[i] = av + bv;
            }
            return new ChebyshevPolynomialU(n, c);
        }

        public static ChebyshevPolynomialU operator -(ChebyshevPolynomialU a, ChebyshevPolynomialU b)
        {
            var n = Math.Max(a.Degree, b.Degree);
            var c = new double[n + 1];
            for (int i = 0; i <= n; i++)
            {
                var av = i <= a.Degree ? a.Coeffs[i] : 0.0;
                var bv = i <= b.Degree ? b.Coeffs[i] : 0.0;
                c[i] = av - bv;
            }
            return new ChebyshevPolynomialU(n, c);
        }

        public static ChebyshevPolynomialU operator *(ChebyshevPolynomialU a, double s) => a.Scale(s);
        public static ChebyshevPolynomialU operator *(double s, ChebyshevPolynomialU a) => a.Scale(s);
        public static ChebyshevPolynomialU operator /(ChebyshevPolynomialU a, double s) => a.Scale(1.0 / s);

        /// <summary>
        /// Multiply two U-basis polynomials by converting both to power basis.
        /// </summary>
        public static ChebyshevPolynomialU operator *(ChebyshevPolynomialU a, ChebyshevPolynomialU b)
        {
            var pa = a.ToPowerBasis();
            var pb = b.ToPowerBasis();
            var conv = new double[pa.Length + pb.Length - 1];
            for (int i = 0; i < pa.Length; i++)
                for (int j = 0; j < pb.Length; j++)
                    conv[i + j] += pa[i] * pb[j];
            return FromPowerCoefficients(conv);
        }

        // ---------------------------------------------------------------------
        // Conversions
        // ---------------------------------------------------------------------
        public double[] ToPowerBasis()
        {
            var n = Degree;
            var Upow = ExpandUAllToPower(n);
            var res = new double[n + 1];
            for (var p = 0; p <= n; p++)
            {
                var poly = Upow[p];
                var bp = Coeffs[p];
                for (var k = 0; k < poly.Length; k++)
                    res[k] += bp * poly[k];
            }
            return res;
        }

        private static double[][] ExpandUAllToPower(int maxK)
        {
            var result = new double[maxK + 1][];
            if (maxK >= 0) result[0] = new[] { 1.0 };
            if (maxK >= 1) result[1] = new[] { 0.0, 2.0 };
            for (var k = 2; k <= maxK; k++)
            {
                var A = result[k - 1];
                var B = result[k - 2];
                var poly = new double[k + 1];
                for (var i = 0; i < A.Length; i++) poly[i + 1] += 2.0 * A[i];
                for (var i = 0; i < B.Length; i++) poly[i] -= B[i];
                result[k] = poly;
            }
            return result;
        }

        public static ChebyshevPolynomialU FromPowerCoefficients(double[] powerCoeffs)
        {
            if (powerCoeffs == null) throw new ArgumentNullException(nameof(powerCoeffs));
            var n = powerCoeffs.Length - 1;
            var N = n + 1;
            Func<double, double> polyEval = x =>
            {
                var v = 0.0;
                for (var k = n; k >= 0; k--) v = v * x + powerCoeffs[k];
                return v;
            };
            return Fit(polyEval, n);
        }

        public ChebyshevPolynomialT ToChebyshevT()
        {
            var power = ToPowerBasis();
            return ChebyshevPolynomialT.FromPowerCoefficients(power);
        }

        public static ChebyshevPolynomialU FromChebyshevT(ChebyshevPolynomialT t)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));
            var power = t.ToPowerBasis();
            return FromPowerCoefficients(power);
        }

        // ---------------------------------------------------------------------
        // Differentiation
        // ---------------------------------------------------------------------
        public ChebyshevPolynomialT Derivative()
        {
            // f'(x) = Σ (k+1)*b_{k+1} * T_k(x)
            if (Degree == 0)
                return ChebyshevPolynomialT.Constant(0.0);

            var tCoeffs = new double[Degree + 1];
            for (int k = 0; k < Degree; k++)
                tCoeffs[k] = (k + 1) * Coeffs[k + 1];

            return new ChebyshevPolynomialT(Degree - 1, tCoeffs.Take(Degree).ToArray());
        }

        // ---------------------------------------------------------------------
        // Fitting
        // ---------------------------------------------------------------------
        public static ChebyshevPolynomialU Fit(Func<double, double> f, int degree)
        {
            if (f == null) throw new ArgumentNullException(nameof(f));
            if (degree < 0) throw new ArgumentOutOfRangeException(nameof(degree));

            var N = degree + 1;
            var samples = new double[N];
            for (var j = 0; j < N; j++)
            {
                var xj = Math.Cos(Math.PI * (j + 1) / (N + 1));
                samples[j] = f(xj);
            }

            var coeffs = new double[N];
            for (var k = 0; k < N; k++)
            {
                var sum = 0.0;
                for (var j = 0; j < N; j++)
                    sum += samples[j] * Math.Sin((k + 1) * (j + 1) * Math.PI / (N + 1));
                coeffs[k] = 2.0 / (N + 1) * sum;
            }

            return new ChebyshevPolynomialU(degree, coeffs);
        }

        // ---------------------------------------------------------------------
        // Utilities
        // ---------------------------------------------------------------------
        public ChebyshevPolynomialU Simplify(double eps = 1e-14)
        {
            var c = (double[])Coeffs.Clone();
            for (int i = 0; i < c.Length; i++)
                if (Math.Abs(c[i]) < eps) c[i] = 0.0;

            int deg = c.Length - 1;
            while (deg > 0 && Math.Abs(c[deg]) < eps) deg--;
            if (deg < c.Length - 1)
                Array.Resize(ref c, deg + 1);
            return new ChebyshevPolynomialU(deg, c);
        }

        public static double[] Roots(int n)
        {
            var roots = new double[n];
            for (var k = 1; k <= n; k++)
                roots[k - 1] = Math.Cos(Math.PI * k / (n + 1));
            return roots;
        }
    }
}


//namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Chebyshev;

///// <summary>
///// Represents a Chebyshev polynomial of the second kind (U_n) expansion:
///// f(x) ≈ sum_{k=0..n} a_k * U_k(x), x ∈ [-1,1].
///// </summary>
//public class ChebyshevPolynomialU
//{
//    private static readonly Dictionary<int, double[]> UPowerCache = new Dictionary<int, double[]>();

//    private static double[] ExpandUkToPowerCached(int k)
//    {
//        lock (UPowerCache)
//        {
//            if (UPowerCache.TryGetValue(k, out var poly))
//                return poly;

//            if (k == 0) poly = [1.0];
//            else if (k == 1) poly = [0.0, 2.0];
//            else
//            {
//                var Uk1 = ExpandUkToPowerCached(k - 1);
//                var Uk2 = ExpandUkToPowerCached(k - 2);
//                poly = new double[k + 1];
//                for (var i = 0; i < Uk1.Length; i++) poly[i + 1] += 2.0 * Uk1[i];
//                for (var i = 0; i < Uk2.Length; i++) poly[i] -= Uk2[i];
//            }

//            UPowerCache[k] = poly;
//            return poly;
//        }
//    }



//    public int Degree { get; }
//    public double[] Coeffs { get; } // b_k for U_k, k=0..Degree

//    public ChebyshevPolynomialU(int degree, double[] coeffs)
//    {
//        if (coeffs == null) throw new ArgumentNullException(nameof(coeffs));
//        if (coeffs.Length != degree + 1) throw new ArgumentException("coeffs length must be degree+1");
//        Degree = degree;
//        Coeffs = (double[])coeffs.Clone();
//    }

//    // Clenshaw for U-basis
//    public double EvaluateAt(double x)
//    {
//        double b_kplus1 = 0.0, b_kplus2 = 0.0;
//        for (var k = Degree; k >= 0; k--)
//        {
//            var b_k = 2.0 * x * b_kplus1 - b_kplus2 + Coeffs[k];
//            b_kplus2 = b_kplus1;
//            b_kplus1 = b_k;
//        }
//        return b_kplus1;
//    }

//    // Fit U coefficients from samples at U nodes xj = cos(pi*(j+1)/(N+1)), j=0..N-1.
//    // O(N^2) approach (direct sine-based projection), stable for moderate N.
//    public static ChebyshevPolynomialU Fit(Func<double, double> f, int degree)
//    {
//        if (f == null) throw new ArgumentNullException(nameof(f));
//        if (degree < 0) throw new ArgumentOutOfRangeException(nameof(degree));
//        var N = degree + 1;
//        var samples = new double[N];
//        for (var j = 0; j < N; j++)
//        {
//            var xj = Math.Cos(Math.PI * (j + 1) / (N + 1));
//            samples[j] = f(xj);
//        }

//        var coeffs = new double[N];
//        // b_k = (2/(N+1)) * sum_{j=0..N-1} samples[j] * sin((k+1)*(j+1)*pi/(N+1))
//        for (var k = 0; k < N; k++)
//        {
//            var sum = 0.0;
//            for (var j = 0; j < N; j++)
//            {
//                sum += samples[j] * Math.Sin((k + 1) * (j + 1) * Math.PI / (N + 1));
//            }
//            coeffs[k] = 2.0 / (N + 1) * sum;
//        }

//        return new ChebyshevPolynomialU(degree, coeffs);
//    }

//    // Convert U-basis → power basis
//    // Expand U_k to powers and accumulate
//    private static double[][] ExpandUAllToPower(int maxK)
//    {
//        var result = new double[maxK + 1][];
//        // U_0(x) = 1
//        if (maxK >= 0) result[0] = [1.0];
//        if (maxK >= 1) result[1] = [0.0, 2.0]; // U1(x) = 2x
//        for (var k = 2; k <= maxK; k++)
//        {
//            // recurrence: U_k(x) = 2x U_{k-1}(x) - U_{k-2}(x)
//            var A = result[k - 1];
//            var B = result[k - 2];
//            var poly = new double[k + 1];
//            for (var i = 0; i < A.Length; i++) poly[i + 1] += 2.0 * A[i];
//            for (var i = 0; i < B.Length; i++) poly[i] -= B[i];
//            result[k] = poly;
//        }
//        return result;
//    }

//    public double[] ToPowerBasis()
//    {
//        var n = Degree;
//        var Upow = ExpandUAllToPower(n);
//        var res = new double[n + 1];
//        for (var p = 0; p <= n; p++)
//        {
//            var poly = Upow[p];
//            var bp = Coeffs[p];
//            for (var k = 0; k < poly.Length; k++) res[k] += bp * poly[k];
//        }
//        return res;
//    }

//    // Build U coefficients from power basis by sampling polynomial at U nodes and using Fit above
//    public static ChebyshevPolynomialU FromPowerCoefficients(double[] powerCoeffs)
//    {
//        if (powerCoeffs == null) throw new ArgumentNullException(nameof(powerCoeffs));
//        var n = powerCoeffs.Length - 1;
//        var N = n + 1;
//        // create evaluator of power polynomial
//        Func<double, double> polyEval = x =>
//        {
//            var v = 0.0;
//            for (var k = n; k >= 0; k--) v = v * x + powerCoeffs[k];
//            return v;
//        };
//        return Fit(polyEval, n);
//    }

//    // Convert U -> T via power basis intermediate
//    public ChebyshevPolynomialT ToChebyshevT()
//    {
//        var power = ToPowerBasis();
//        return ChebyshevPolynomialT.FromPowerCoefficients(power);
//    }

//    public static ChebyshevPolynomialU FromChebyshevT(ChebyshevPolynomialT t)
//    {
//        if (t == null) throw new ArgumentNullException(nameof(t));
//        var power = t.ToPowerBasis();
//        return FromPowerCoefficients(power);
//    }



//    /// Roots of U_n
//    public static double[] Roots(int n)
//    {
//        var roots = new double[n];
//        for (var k = 1; k <= n; k++)
//            roots[k - 1] = Math.Cos(Math.PI * k / (n + 1));
//        return roots;
//    }





//}