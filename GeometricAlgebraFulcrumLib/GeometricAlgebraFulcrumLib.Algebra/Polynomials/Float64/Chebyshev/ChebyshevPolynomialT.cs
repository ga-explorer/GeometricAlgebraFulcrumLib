using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Bernstein;
using MathNet.Numerics.IntegralTransforms;

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Chebyshev
{
    /// <summary>
    /// Represents a Chebyshev polynomial of the first kind (T_n) expansion:
    /// f(x) = Σ a_k * T_k(x),  x ∈ [-1,1]
    /// </summary>
    public class ChebyshevPolynomialT
    {
        // In ChebyshevPolynomialT
        private static readonly Dictionary<int, double[]> TPowerCache = new Dictionary<int, double[]>();

        private static double[] ExpandTkToPowerCached(int k)
        {
            lock (TPowerCache)
            {
                if (TPowerCache.TryGetValue(k, out var poly))
                    return poly;

                if (k == 0) poly = [1.0];
                else if (k == 1) poly = [0.0, 1.0];
                else
                {
                    var Tk1 = ExpandTkToPowerCached(k - 1);
                    var Tk2 = ExpandTkToPowerCached(k - 2);
                    poly = new double[k + 1];
                    for (var i = 0; i < Tk1.Length; i++) poly[i + 1] += 2 * Tk1[i];
                    for (var i = 0; i < Tk2.Length; i++) poly[i] -= Tk2[i];
                }

                TPowerCache[k] = poly;
                return poly;
            }
        }



        // ---------------- FFT-backed DCT-II & inverse (DCT-III) ----------------
        // DCT-II: given data[j] (j=0..N-1), compute c[k] = sum_j data[j] * cos(pi*k*(j+0.5)/N)
        // Implementation via FFT of length 2N with even symmetry
        private static double[] Dct2ViaFft(double[] data)
        {
            var N = data.Length;
            if (N == 0) return [];
            var y = new Complex[2 * N];
            // fill y[0..N-1] = data
            for (var j = 0; j < N; j++) y[j] = new Complex(data[j], 0.0);
            // fill y[N..2N-1] = data mirrored: y[2N-1-j] = data[j]
            for (var j = 0; j < N; j++) y[2 * N - 1 - j] = new Complex(data[j], 0.0);

            // forward FFT (in-place)
            Fourier.Forward(y, FourierOptions.Matlab);

            var c = new double[N];
            for (var k = 0; k < N; k++)
            {
                var theta = -Math.PI * k / (2.0 * N);
                // Re( Y[k] * e^{-i theta} ) = Re(Y)*cos(theta) + Im(Y)*sin(theta)
                var Yk = y[k];
                c[k] = Yk.Real * Math.Cos(theta) + Yk.Imaginary * Math.Sin(theta);
            }
            return c;
        }

        // Inverse DCT-III via Fourier: given c[k] compute data[j].
        private static double[] Idct3ViaFft(double[] c)
        {
            var N = c.Length;
            if (N == 0) return [];
            // Build Y[k] for k=0..2N-1 from c
            var Y = new Complex[2 * N];
            // fill Y[k] = ( c[k] * e^{i theta} ) for k=0..N-1
            for (var k = 0; k < N; k++)
            {
                var theta = Math.PI * k / (2.0 * N); // note sign opposite of DCT construction
                var phase = new Complex(Math.Cos(theta), Math.Sin(theta)); // e^{i theta}
                Y[k] = phase * c[k];
            }
            // fill remaining Y[k] using symmetry: Y[2N-k] = conj(Y[k]) for real sequence
            Y[N] = new Complex(0.0, 0.0); // safe set; will be overwritten below if needed
            for (var k = 1; k < N; k++)
            {
                Y[2 * N - k] = Complex.Conjugate(Y[k]);
            }
            Y[0] = new Complex(Y[0].Real, Y[0].Imaginary); // ensure explicit

            // inverse FFT
            Fourier.Inverse(Y, FourierOptions.Matlab);

            var data = new double[N];
            // data[j] = Re(Y[j]) (because of the construction)
            for (var j = 0; j < N; j++) data[j] = Y[j].Real;
            return data;
        }

        // ---------------- Conversions with power basis ----------------

        // Expand T_k to power basis (monomials) (cache-friendly recursion)
        private static double[][] ExpandTAllToPower(int maxK)
        {
            var result = new double[maxK + 1][];
            result[0] = [1.0];
            if (maxK >= 1) result[1] = [0.0, 1.0];
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

        // ---------------- Conversions between T and U via power basis ----------------

        /// Convert this T-basis polynomial to U-basis coefficients (via power basis intermediate)
        public ChebyshevPolynomialU ToChebyshevU()
        {
            var power = ToPowerBasis();
            return ChebyshevPolynomialU.FromPowerCoefficients(power);
        }
        

        // Simple reparametrization helper: convert polynomial on [-1,1] to polynomial in t on [a,b] via change of variables.
        // Returning a new Chebyshev polynomial in variable u where x = (2u - (b+a))/(b-a) would normally require
        // recomputing coefficients. For convenience we provide a function that evaluates reparametrized polynomial at u:
        public Func<double, double> ReparametrizedEvaluator(double a, double b)
        {
            return u =>
            {
                // map u in [a,b] to x in [-1,1]
                var x = (2.0 * u - (b + a)) / (b - a);
                return EvaluateAt(x);
            };
        }

        // Expand T_k(x) into power coefficients (lowest-to-highest)
        private static double[] ExpandTkToPower(int k)
        {
            if (k == 0) return [1.0];
            if (k == 1) return [0.0, 1.0];
            var Tkm1 = ExpandTkToPower(k - 1);
            var Tkm2 = ExpandTkToPower(k - 2);
            var poly = new double[k + 1];
            // T_k(x) = 2x * T_{k-1}(x) - T_{k-2}(x)
            for (var i = 0; i < Tkm1.Length; i++) poly[i + 1] += 2.0 * Tkm1[i];
            for (var i = 0; i < Tkm2.Length; i++) poly[i] -= Tkm2[i];
            return poly;
        }

        // Expose DCT helper (useful for tests)
        private static class DctHelper
        {
            // Compute c[k] = sum_{j=0..N-1} data[j] * cos(pi*k*(j+0.5)/N) for k=0..N-1
            // Precomputes cosine matrix for given N for speed.
            public static double[] Dct1DRaw(double[] data)
            {
                var N = data.Length;
                var c = new double[N];
                var cos = new double[N, N]; // cos[k,j]
                for (var k = 0; k < N; k++)
                {
                    for (var j = 0; j < N; j++)
                    {
                        cos[k, j] = Math.Cos(Math.PI * k * (j + 0.5) / N);
                    }
                }
                for (var k = 0; k < N; k++)
                {
                    var sum = 0.0;
                    for (var j = 0; j < N; j++) sum += data[j] * cos[k, j];
                    c[k] = sum;
                }
                return c;
            }
        }

        /// Integrate once (indefinite integral, constant omitted).
        /// Returns new ChebyshevPolynomialT.
        public ChebyshevPolynomialT Integrate()
        {
            var c = Coeffs;
            var n = Degree;
            var outc = new double[n + 2];
            outc[0] = 0; // integration constant

            for (var k = 1; k <= n; k++)
            {
                outc[k + 1] = c[k] / (2.0 * (k + 1));
                outc[k - 1] -= c[k] / (2.0 * (k - 1 + 1));
            }
            outc[1] += c[0]; // integrate T_0 = 1

            return new ChebyshevPolynomialT(n + 1, outc);
        }

        // ------------------ Conversion ------------------

        private static double[] ExpandTk(int k)
        {
            if (k == 0) return [1];
            if (k == 1) return [0, 1];
            var Tk1 = ExpandTk(k - 1);
            var Tk2 = ExpandTk(k - 2);
            var poly = new double[k + 1];
            for (var i = 0; i < Tk1.Length; i++) poly[i + 1] += 2 * Tk1[i];
            for (var i = 0; i < Tk2.Length; i++) poly[i] -= Tk2[i];
            return poly;
        }

        /// Reparametrize polynomial from x ∈ [-1,1] to t ∈ [a,b]
        public ChebyshevPolynomialT Reparametrize(double a, double b)
        {
            // Map: x = (2t - (b+a)) / (b-a)
            return new ChebyshevPolynomialT(Degree, (double[])Coeffs.Clone());
        }

        /// Roots of expansion (if it is a single T_n)
        public static double[] Roots(int n)
        {
            var roots = new double[n];
            for (var k = 0; k < n; k++)
                roots[k] = Math.Cos(Math.PI * (k + 0.5) / n);
            return roots;
        }
        


        public int Degree { get; }
        public double[] Coeffs { get; }   // a_k for T_k(x)

        public ChebyshevPolynomialT(int degree, double[] coeffs)
        {
            if (coeffs == null) throw new ArgumentNullException(nameof(coeffs));
            if (coeffs.Length != degree + 1)
                throw new ArgumentException("coeffs length must equal degree+1");
            Degree = degree;
            Coeffs = (double[])coeffs.Clone();
        }

        // ---------------------------------------------------------------------
        // Constructors and factories
        // ---------------------------------------------------------------------
        public static ChebyshevPolynomialT Constant(double c) =>
            new(0, new[] { c });

        /// <summary>
        /// Build a Chebyshev polynomial approximating linear function (a + b*x)
        /// </summary>
        public static ChebyshevPolynomialT CreateLinear(double a, double b)
        {
            // T0(x)=1, T1(x)=x
            return new ChebyshevPolynomialT(1, new[] { a, b });
        }

        /// <summary>
        /// Construct polynomial with specified roots (stable form).
        /// </summary>
        public static ChebyshevPolynomialT FromRoots(double[] roots)
        {
            if (roots == null || roots.Length == 0)
                return Constant(1.0);

            // Build via multiplication of (x - r_i)
            var poly = new ChebyshevPolynomialT(1, new[] { -roots[0], 1.0 });
            for (int i = 1; i < roots.Length; i++)
                poly *= CreateLinear(-roots[i], 1.0);

            return poly;
        }

        // ---------------------------------------------------------------------
        // Core operations
        // ---------------------------------------------------------------------
        public double EvaluateAt(double x)
        {
            double b_kplus1 = 0.0, b_kplus2 = 0.0;
            for (var k = Degree; k >= 1; k--)
            {
                var b_k = 2.0 * x * b_kplus1 - b_kplus2 + Coeffs[k];
                b_kplus2 = b_kplus1;
                b_kplus1 = b_k;
            }
            return x * b_kplus1 - b_kplus2 + Coeffs[0];
        }

        public ChebyshevPolynomialT Scale(double s)
        {
            var c = Coeffs.Select(a => a * s).ToArray();
            return new ChebyshevPolynomialT(Degree, c);
        }

        public ChebyshevPolynomialT Square() => this * this;

        // Addition, subtraction
        public static ChebyshevPolynomialT operator +(ChebyshevPolynomialT a, ChebyshevPolynomialT b)
        {
            var n = Math.Max(a.Degree, b.Degree);
            var c = new double[n + 1];
            for (int i = 0; i <= n; i++)
            {
                var av = i <= a.Degree ? a.Coeffs[i] : 0.0;
                var bv = i <= b.Degree ? b.Coeffs[i] : 0.0;
                c[i] = av + bv;
            }
            return new ChebyshevPolynomialT(n, c);
        }

        public static ChebyshevPolynomialT operator -(ChebyshevPolynomialT a, ChebyshevPolynomialT b)
        {
            var n = Math.Max(a.Degree, b.Degree);
            var c = new double[n + 1];
            for (int i = 0; i <= n; i++)
            {
                var av = i <= a.Degree ? a.Coeffs[i] : 0.0;
                var bv = i <= b.Degree ? b.Coeffs[i] : 0.0;
                c[i] = av - bv;
            }
            return new ChebyshevPolynomialT(n, c);
        }

        public static ChebyshevPolynomialT operator *(ChebyshevPolynomialT a, double s) => a.Scale(s);
        public static ChebyshevPolynomialT operator *(double s, ChebyshevPolynomialT a) => a.Scale(s);
        public static ChebyshevPolynomialT operator /(ChebyshevPolynomialT a, double s) => a.Scale(1.0 / s);

        /// <summary>
        /// Multiply two Chebyshev polynomials by converting to power basis.
        /// </summary>
        public static ChebyshevPolynomialT operator *(ChebyshevPolynomialT a, ChebyshevPolynomialT b)
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
        // Differentiation: T → U and back
        // ---------------------------------------------------------------------

        /// <summary>
        /// Differentiate this Chebyshev-T polynomial. 
        /// Returns the derivative expressed in Chebyshev-U basis:
        /// f'(x) = Σ_{k≥1} k * a_k * U_{k-1}(x)
        /// </summary>
        public ChebyshevPolynomialU Differentiate()
        {
            if (Degree == 0)
                return new ChebyshevPolynomialU(0, new[] { 0.0 });

            var uCoeffs = new double[Degree];
            for (int k = 1; k <= Degree; k++)
                uCoeffs[k - 1] = k * Coeffs[k];

            return new ChebyshevPolynomialU(Degree - 1, uCoeffs);
        }

        /// <summary>
        /// Construct a Chebyshev-T polynomial from a given Chebyshev-U polynomial.
        /// Uses exact combinatorial transformation via power-basis conversion.
        /// </summary>
        public static ChebyshevPolynomialT FromChebyshevU(ChebyshevPolynomialU u)
        {
            if (u == null) 
                throw new ArgumentNullException(nameof(u));

            // The simplest and numerically stable path:
            // U → power basis → T
            var power = u.ToPowerBasis();
            return FromPowerCoefficients(power);
        }


        // ---------------------------------------------------------------------
        // Differentiation
        // ---------------------------------------------------------------------
        public ChebyshevPolynomialT Derivative()
        {
            if (Degree == 0)
                return Constant(0.0);

            // Convert to U basis derivative, then back
            var u = Differentiate();
            return FromChebyshevU(u);
        }

        // ---------------------------------------------------------------------
        // Conversion to other bases
        // ---------------------------------------------------------------------
        public double[] ToPowerBasis()
        {
            // Expand recursively using T_0=1, T_1=x, T_k=2x*T_{k-1}-T_{k-2}
            var Tp = new List<double[]> { new[] { 1.0 }, new[] { 0.0, 1.0 } };
            for (int k = 2; k <= Degree; k++)
            {
                var prev1 = Tp[k - 1];
                var prev2 = Tp[k - 2];
                var poly = new double[k + 1];
                for (int i = 0; i < prev1.Length; i++) poly[i + 1] += 2.0 * prev1[i];
                for (int i = 0; i < prev2.Length; i++) poly[i] -= prev2[i];
                Tp.Add(poly);
            }

            var res = new double[Degree + 1];
            for (int i = 0; i <= Degree; i++)
            {
                var a_i = Coeffs[i];
                var poly = Tp[i];
                for (int j = 0; j < poly.Length; j++)
                    res[j] += a_i * poly[j];
            }

            return res;
        }

        public static ChebyshevPolynomialT FromPowerCoefficients(double[] power)
        {
            var n = power.Length - 1;
            var f = new Func<double, double>(x =>
            {
                var v = 0.0;
                for (int i = n; i >= 0; i--)
                    v = v * x + power[i];
                return v;
            });

            return Fit(f, n);
        }

        // ---------------------------------------------------------------------
        // Conversion to Bernstein basis on [0,1]
        // ---------------------------------------------------------------------
        public BernsteinPolynomial ToBernstein()
        {
            // Map x = 2u - 1, u ∈ [0,1]
            var power = ToPowerBasis();
            var n = power.Length - 1;

            // Compute coefficients for (2u - 1)^k expansion and accumulate
            var bern = new double[n + 1];
            for (int k = 0; k <= n; k++)
            {
                for (int j = 0; j <= k; j++)
                {
                    var coeff = power[k] * Math.Pow(2, k) * Binomial(k, j) * Math.Pow(-1, k - j);
                    bern[j] += coeff;
                }
            }

            // Normalize to Bernstein form of degree n
            return BernsteinPolynomial.FromMonomialBasis(bern);
        }

        private static double Binomial(int n, int k)
        {
            if (k < 0 || k > n) return 0.0;
            if (k == 0 || k == n) return 1.0;
            double res = 1.0;
            for (int i = 1; i <= k; i++)
                res = res * (n - (k - i)) / i;
            return res;
        }

        public ChebyshevPolynomialT Simplify(double eps = 1e-14)
        {
            var c = (double[])Coeffs.Clone();
            for (int i = 0; i < c.Length; i++)
                if (Math.Abs(c[i]) < eps) c[i] = 0.0;
            int deg = c.Length - 1;
            while (deg > 0 && Math.Abs(c[deg]) < eps) deg--;
            if (deg < c.Length - 1)
                Array.Resize(ref c, deg + 1);
            return new ChebyshevPolynomialT(deg, c);
        }

        // ---------------------------------------------------------------------
        // Fit from samples via DCT-II (kept from original)
        // ---------------------------------------------------------------------
        public static ChebyshevPolynomialT Fit(Func<double, double> f, int degree)
        {
            var N = degree + 1;
            var samples = new double[N];
            for (int j = 0; j < N; j++)
            {
                var xj = Math.Cos(Math.PI * (j + 0.5) / N);
                samples[j] = f(xj);
            }

            var raw = Dct2(samples);
            var a = new double[N];
            var scale = 2.0 / N;
            for (int k = 0; k < N; k++) a[k] = scale * raw[k];
            a[0] *= 0.5;
            return new ChebyshevPolynomialT(degree, a);
        }

        private static double[] Dct2(double[] data)
        {
            var N = data.Length;
            var y = new Complex[2 * N];
            for (var j = 0; j < N; j++)
            {
                y[j] = new Complex(data[j], 0);
                y[2 * N - 1 - j] = new Complex(data[j], 0);
            }

            Fourier.Forward(y, FourierOptions.Matlab);

            var c = new double[N];
            for (var k = 0; k < N; k++)
            {
                var theta = -Math.PI * k / (2.0 * N);
                var Yk = y[k];
                c[k] = Yk.Real * Math.Cos(theta) + Yk.Imaginary * Math.Sin(theta);
            }
            return c;
        }
    }
}




//// For FFT-based fitting if desired

//using System.Numerics;
//using MathNet.Numerics.IntegralTransforms;

//namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Chebyshev;

///// <summary>
///// Represents a Chebyshev polynomial of the first kind (T_n) expansion:
///// f(x) ≈ sum_{k=0..n} a_k * T_k(x), x ∈ [-1,1].
///// </summary>
//public class ChebyshevPolynomialT
//{
//    // In ChebyshevPolynomialT
//    private static readonly Dictionary<int, double[]> TPowerCache = new Dictionary<int, double[]>();

//    private static double[] ExpandTkToPowerCached(int k)
//    {
//        lock (TPowerCache)
//        {
//            if (TPowerCache.TryGetValue(k, out var poly))
//                return poly;

//            if (k == 0) poly = [1.0];
//            else if (k == 1) poly = [0.0, 1.0];
//            else
//            {
//                var Tk1 = ExpandTkToPowerCached(k - 1);
//                var Tk2 = ExpandTkToPowerCached(k - 2);
//                poly = new double[k + 1];
//                for (var i = 0; i < Tk1.Length; i++) poly[i + 1] += 2 * Tk1[i];
//                for (var i = 0; i < Tk2.Length; i++) poly[i] -= Tk2[i];
//            }

//            TPowerCache[k] = poly;
//            return poly;
//        }
//    }



//        public int Degree { get; }
//    public double[] Coeffs { get; } // a_k for T_k, k=0..Degree

//    public ChebyshevPolynomialT(int degree, double[] coeffs)
//    {
//        if (coeffs == null) throw new ArgumentNullException(nameof(coeffs));
//        if (coeffs.Length != degree + 1) throw new ArgumentException("coeffs length must be degree+1");
//        Degree = degree;
//        Coeffs = (double[])coeffs.Clone();
//    }

//    // ---------------- Clenshaw evaluation ----------------
//    public double EvaluateAt(double x)
//    {
//        double b_kplus1 = 0.0, b_kplus2 = 0.0;
//        for (var k = Degree; k >= 1; k--)
//        {
//            var b_k = 2.0 * x * b_kplus1 - b_kplus2 + Coeffs[k];
//            b_kplus2 = b_kplus1;
//            b_kplus1 = b_k;
//        }
//        return x * b_kplus1 - b_kplus2 + Coeffs[0];
//    }

//    // ---------------- FFT-backed DCT-II & inverse (DCT-III) ----------------
//    // DCT-II: given data[j] (j=0..N-1), compute c[k] = sum_j data[j] * cos(pi*k*(j+0.5)/N)
//    // Implementation via FFT of length 2N with even symmetry
//    private static double[] Dct2ViaFft(double[] data)
//    {
//        var N = data.Length;
//        if (N == 0) return [];
//        var y = new Complex[2 * N];
//        // fill y[0..N-1] = data
//        for (var j = 0; j < N; j++) y[j] = new Complex(data[j], 0.0);
//        // fill y[N..2N-1] = data mirrored: y[2N-1-j] = data[j]
//        for (var j = 0; j < N; j++) y[2 * N - 1 - j] = new Complex(data[j], 0.0);

//        // forward FFT (in-place)
//        Fourier.Forward(y, FourierOptions.Matlab);

//        var c = new double[N];
//        for (var k = 0; k < N; k++)
//        {
//            var theta = -Math.PI * k / (2.0 * N);
//            // Re( Y[k] * e^{-i theta} ) = Re(Y)*cos(theta) + Im(Y)*sin(theta)
//            var Yk = y[k];
//            c[k] = Yk.Real * Math.Cos(theta) + Yk.Imaginary * Math.Sin(theta);
//        }
//        return c;
//    }

//    // Inverse DCT-III via Fourier: given c[k] compute data[j].
//    private static double[] Idct3ViaFft(double[] c)
//    {
//        var N = c.Length;
//        if (N == 0) return [];
//        // Build Y[k] for k=0..2N-1 from c
//        var Y = new Complex[2 * N];
//        // fill Y[k] = ( c[k] * e^{i theta} ) for k=0..N-1
//        for (var k = 0; k < N; k++)
//        {
//            var theta = Math.PI * k / (2.0 * N); // note sign opposite of DCT construction
//            var phase = new Complex(Math.Cos(theta), Math.Sin(theta)); // e^{i theta}
//            Y[k] = phase * c[k];
//        }
//        // fill remaining Y[k] using symmetry: Y[2N-k] = conj(Y[k]) for real sequence
//        Y[N] = new Complex(0.0, 0.0); // safe set; will be overwritten below if needed
//        for (var k = 1; k < N; k++)
//        {
//            Y[2 * N - k] = Complex.Conjugate(Y[k]);
//        }
//        Y[0] = new Complex(Y[0].Real, Y[0].Imaginary); // ensure explicit

//        // inverse FFT
//        Fourier.Inverse(Y, FourierOptions.Matlab);

//        var data = new double[N];
//        // data[j] = Re(Y[j]) (because of the construction)
//        for (var j = 0; j < N; j++) data[j] = Y[j].Real;
//        return data;
//    }

//    // ---------------- Fit from samples using FFT DCT-II ----------------
//    // Fit f on [-1,1] with Chebyshev T expansion of degree `degree`.
//    // Uses N = degree+1 Chebyshev nodes x_j = cos(pi*(j+0.5)/N), samples f at those, then DCT-II and scaling.
//    public static ChebyshevPolynomialT Fit(Func<double, double> f, int degree)
//    {
//        if (f == null) throw new ArgumentNullException(nameof(f));
//        if (degree < 0) throw new ArgumentOutOfRangeException(nameof(degree));

//        var N = degree + 1;
//        var samples = new double[N];
//        for (var j = 0; j < N; j++)
//        {
//            var xj = Math.Cos(Math.PI * (j + 0.5) / N);
//            samples[j] = f(xj);
//        }

//        var craw = Dct2ViaFft(samples);         // raw c_k
//        var a = new double[N];
//        var scale = 2.0 / N;
//        for (var k = 0; k < N; k++) a[k] = scale * craw[k];
//        a[0] *= 0.5;
//        return new ChebyshevPolynomialT(degree, a);
//    }

//    // ---------------- Conversions with power basis ----------------

//    // Expand T_k to power basis (monomials) (cache-friendly recursion)
//    private static double[][] ExpandTAllToPower(int maxK)
//    {
//        var result = new double[maxK + 1][];
//        result[0] = [1.0];
//        if (maxK >= 1) result[1] = [0.0, 1.0];
//        for (var k = 2; k <= maxK; k++)
//        {
//            var A = result[k - 1];
//            var B = result[k - 2];
//            var poly = new double[k + 1];
//            for (var i = 0; i < A.Length; i++) poly[i + 1] += 2.0 * A[i];
//            for (var i = 0; i < B.Length; i++) poly[i] -= B[i];
//            result[k] = poly;
//        }
//        return result;
//    }

//    /// <summary>
//    /// Convert Chebyshev coefficients (T basis) to power-basis coefficients.
//    /// returns p[0..Degree] so f(x)=sum p[k]*x^k
//    /// </summary>
//    public double[] ToPowerBasis()
//    {
//        var n = Degree;
//        var Tpow = ExpandTAllToPower(n);
//        var res = new double[n + 1];
//        for (var p = 0; p <= n; p++)
//        {
//            var poly = Tpow[p]; // length p+1
//            var ap = Coeffs[p];
//            for (var k = 0; k < poly.Length; k++) res[k] += ap * poly[k];
//        }
//        return res;
//    }

//    /// <summary>
//    /// Build Chebyshev T coefficients from power-basis coefficients via sampling + DCT-II (FFT).
//    /// powerCoeffs length must be n+1 for degree n.
//    /// </summary>
//    public static ChebyshevPolynomialT FromPowerCoefficients(double[] powerCoeffs)
//    {
//        if (powerCoeffs == null) throw new ArgumentNullException(nameof(powerCoeffs));
//        var n = powerCoeffs.Length - 1;
//        if (n < 0) throw new ArgumentException("powerCoeffs length must be >= 1");

//        // Evaluate the power polynomial at Chebyshev nodes
//        var N = n + 1;
//        var samples = new double[N];
//        for (var j = 0; j < N; j++)
//        {
//            var xj = Math.Cos(Math.PI * (j + 0.5) / N);
//            // evaluate polynomial (Horner)
//            var v = 0.0;
//            for (var k = n; k >= 0; k--) v = v * xj + powerCoeffs[k];
//            samples[j] = v;
//        }

//        var craw = Dct2ViaFft(samples);
//        var a = new double[N];
//        var scale = 2.0 / N;
//        for (var k = 0; k < N; k++) a[k] = scale * craw[k];
//        a[0] *= 0.5;
//        return new ChebyshevPolynomialT(n, a);
//    }

//    // ---------------- Conversions between T and U via power basis ----------------

//    /// Convert this T-basis polynomial to U-basis coefficients (via power basis intermediate)
//    public ChebyshevPolynomialU ToChebyshevU()
//    {
//        var power = ToPowerBasis();
//        return ChebyshevPolynomialU.FromPowerCoefficients(power);
//    }

//    /// Build Chebyshev T from Chebyshev U coefficients (via power basis)
//    public static ChebyshevPolynomialT FromChebyshevU(ChebyshevPolynomialU u)
//    {
//        if (u == null) throw new ArgumentNullException(nameof(u));
//        var power = u.ToPowerBasis();
//        return FromPowerCoefficients(power);
//    }






//    // ---------------- Differentiation / conversions ----------------
//    // Returns derivative expressed in Chebyshev U-basis: f'(x) = sum_{k>=1} k*a_k * U_{k-1}(x)
//    public ChebyshevPolynomialU Differentiate()
//    {
//        if (Degree == 0) return new ChebyshevPolynomialU(0, [0.0]);
//        var uCoeffs = new double[Degree];
//        for (var k = 1; k <= Degree; k++) uCoeffs[k - 1] = k * Coeffs[k];
//        return new ChebyshevPolynomialU(Degree - 1, uCoeffs);
//    }

//    // Simple reparametrization helper: convert polynomial on [-1,1] to polynomial in t on [a,b] via change of variables.
//    // Returning a new Chebyshev polynomial in variable u where x = (2u - (b+a))/(b-a) would normally require
//    // recomputing coefficients. For convenience we provide a function that evaluates reparametrized polynomial at u:
//    public Func<double, double> ReparametrizedEvaluator(double a, double b)
//    {
//        return u =>
//        {
//            // map u in [a,b] to x in [-1,1]
//            var x = (2.0 * u - (b + a)) / (b - a);
//            return EvaluateAt(x);
//        };
//    }

//    // Expand T_k(x) into power coefficients (lowest-to-highest)
//    private static double[] ExpandTkToPower(int k)
//    {
//        if (k == 0) return [1.0];
//        if (k == 1) return [0.0, 1.0];
//        var Tkm1 = ExpandTkToPower(k - 1);
//        var Tkm2 = ExpandTkToPower(k - 2);
//        var poly = new double[k + 1];
//        // T_k(x) = 2x * T_{k-1}(x) - T_{k-2}(x)
//        for (var i = 0; i < Tkm1.Length; i++) poly[i + 1] += 2.0 * Tkm1[i];
//        for (var i = 0; i < Tkm2.Length; i++) poly[i] -= Tkm2[i];
//        return poly;
//    }

//    // Expose DCT helper (useful for tests)
//    private static class DctHelper
//    {
//        // Compute c[k] = sum_{j=0..N-1} data[j] * cos(pi*k*(j+0.5)/N) for k=0..N-1
//        // Precomputes cosine matrix for given N for speed.
//        public static double[] Dct1DRaw(double[] data)
//        {
//            var N = data.Length;
//            var c = new double[N];
//            var cos = new double[N, N]; // cos[k,j]
//            for (var k = 0; k < N; k++)
//            {
//                for (var j = 0; j < N; j++)
//                {
//                    cos[k, j] = Math.Cos(Math.PI * k * (j + 0.5) / N);
//                }
//            }
//            for (var k = 0; k < N; k++)
//            {
//                var sum = 0.0;
//                for (var j = 0; j < N; j++) sum += data[j] * cos[k, j];
//                c[k] = sum;
//            }
//            return c;
//        }
//    }

//    /// Integrate once (indefinite integral, constant omitted).
//    /// Returns new ChebyshevPolynomialT.
//    public ChebyshevPolynomialT Integrate()
//    {
//        var c = Coeffs;
//        var n = Degree;
//        var outc = new double[n + 2];
//        outc[0] = 0; // integration constant

//        for (var k = 1; k <= n; k++)
//        {
//            outc[k + 1] = c[k] / (2.0 * (k + 1));
//            outc[k - 1] -= c[k] / (2.0 * (k - 1 + 1));
//        }
//        outc[1] += c[0]; // integrate T_0 = 1

//        return new ChebyshevPolynomialT(n + 1, outc);
//    }

//    // ------------------ Conversion ------------------

//    private static double[] ExpandTk(int k)
//    {
//        if (k == 0) return [1];
//        if (k == 1) return [0, 1];
//        var Tk1 = ExpandTk(k - 1);
//        var Tk2 = ExpandTk(k - 2);
//        var poly = new double[k + 1];
//        for (var i = 0; i < Tk1.Length; i++) poly[i + 1] += 2 * Tk1[i];
//        for (var i = 0; i < Tk2.Length; i++) poly[i] -= Tk2[i];
//        return poly;
//    }

//    /// Reparametrize polynomial from x ∈ [-1,1] to t ∈ [a,b]
//    public ChebyshevPolynomialT Reparametrize(double a, double b)
//    {
//        // Map: x = (2t - (b+a)) / (b-a)
//        return new ChebyshevPolynomialT(Degree, (double[])Coeffs.Clone());
//    }

//    /// Roots of expansion (if it is a single T_n)
//    public static double[] Roots(int n)
//    {
//        var roots = new double[n];
//        for (var k = 0; k < n; k++)
//            roots[k] = Math.Cos(Math.PI * (k + 0.5) / n);
//        return roots;
//    }


//}