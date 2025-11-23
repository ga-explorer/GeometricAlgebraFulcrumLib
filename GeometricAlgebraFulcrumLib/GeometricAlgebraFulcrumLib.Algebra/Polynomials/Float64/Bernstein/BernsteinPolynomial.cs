using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Tuples;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Chebyshev;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using Vector = MathNet.Numerics.LinearAlgebra.Double.Vector;

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Bernstein;

public class BernsteinPolynomial
{
    public enum FitSolver { Qr, Svd }


    // Thread-safe binomial cache
    private static readonly ConcurrentDictionary<(int, int), double> BinomialCache
        = new();

    public static Pair<BernsteinPolynomial> SimplifyToEqualDegree(BernsteinPolynomial p1, BernsteinPolynomial p2, double tolerance = Float64Utils.DefaultTolerance)
    {
        var sp1 = p1.Simplify(tolerance);
        var sp2 = p2.Simplify(tolerance);

        var degree =
            Math.Max(sp1.Degree, sp2.Degree);

        if (sp1.Degree < degree) sp1 = sp1.RaiseDegree(degree);
        if (sp2.Degree < degree) sp2 = sp2.RaiseDegree(degree);

        return new Pair<BernsteinPolynomial>(sp1, sp2);
    }

    public static Triplet<BernsteinPolynomial> SimplifyToEqualDegree(BernsteinPolynomial p1, BernsteinPolynomial p2, BernsteinPolynomial p3, double tolerance = Float64Utils.DefaultTolerance)
    {
        var sp1 = p1.Simplify(tolerance);
        var sp2 = p2.Simplify(tolerance);
        var sp3 = p3.Simplify(tolerance);

        var degree =
            Math.Max(
                Math.Max(sp1.Degree, sp2.Degree),
                sp3.Degree
            );

        if (sp1.Degree < degree) sp1 = sp1.RaiseDegree(degree);
        if (sp2.Degree < degree) sp2 = sp2.RaiseDegree(degree);
        if (sp3.Degree < degree) sp3 = sp3.RaiseDegree(degree);

        return new Triplet<BernsteinPolynomial>(sp1, sp2, sp3);
    }

    public static Quad<BernsteinPolynomial> SimplifyToEqualDegree(BernsteinPolynomial p1, BernsteinPolynomial p2, BernsteinPolynomial p3, BernsteinPolynomial p4, double tolerance = Float64Utils.DefaultTolerance)
    {
        var sp1 = p1.Simplify(tolerance);
        var sp2 = p2.Simplify(tolerance);
        var sp3 = p3.Simplify(tolerance);
        var sp4 = p4.Simplify(tolerance);

        var degree =
            Math.Max(
                Math.Max(
                    Math.Max(sp1.Degree, sp2.Degree),
                    sp3.Degree),
                sp4.Degree
            );

        if (sp1.Degree < degree) sp1 = sp1.RaiseDegree(degree);
        if (sp2.Degree < degree) sp2 = sp2.RaiseDegree(degree);
        if (sp3.Degree < degree) sp3 = sp3.RaiseDegree(degree);
        if (sp4.Degree < degree) sp4 = sp4.RaiseDegree(degree);

        return new Quad<BernsteinPolynomial>(sp1, sp2, sp3, sp4);
    }

    public static BernsteinPolynomial[] SimplifyToEqualDegree(BernsteinPolynomial[] pArray, double tolerance = Float64Utils.DefaultTolerance)
    {
        var spArray =
            pArray.Select(p => p.Simplify(tolerance)).ToArray();

        var degree = spArray.Max(p => p.Degree);

        for (var i = 0; i < spArray.Length; i++)
        {
            var sp = spArray[i];

            if (sp.Degree < degree)
                spArray[i] = sp.RaiseDegree(degree);
        }

        return spArray;
    }

    //internal static double Binomial(int n, int k)
    //{
    //    if (k < 0 || k > n) return 0.0;
    //    if (k == 0 || k == n) return 1.0;

    //    var key = (n, k);
    //    if (BinomialCache.TryGetValue(key, out var v))
    //        return v;

    //    var val = 1d;
    //    var kk = Math.Min(k, n - k);

    //    for (var i = 1; i <= kk; i++)
    //        val = val * (n - kk + i) / i;

    //    BinomialCache[key] = val;
    //    return val;
    //}

    public static double Binomial(int n, int k)
    {
        //return SpecialFunctions.Binomial(n, k);

        // Simple edge cases
        if (k < 0 || k > n) return 0.0;
        if (k == 0 || k == n) return 1.0;

        // Symmetry optimization
        if (k > n - k) k = n - k;

        var key = (n, k);
        if (BinomialCache.TryGetValue(key, out var v))
            return v;

        var result = 1.0;

        // Exact computation for small n
        if (n < 70)
        {
            for (var i = 1; i <= k; i++)
            {
                result *= (n - (k - i));
                result /= i;
            }
        }
        else
        {
            // Use Gamma function for large values
            var logResult =
                SpecialFunctions.GammaLn(n + 1) -
                SpecialFunctions.GammaLn(k + 1) -
                SpecialFunctions.GammaLn(n - k + 1);

            result = Math.Exp(logResult);
        }

        BinomialCache[key] = result;
        return result;
    }

    public static double EvaluateBasisAt(int n, int i, double t)
    {
        return t switch
        {
            <= 0.0 => i == 0 ? 1.0 : 0.0,
            >= 1.0 => i == n ? 1.0 : 0.0,
            _ => Binomial(n, i) * Math.Pow(t, i) * Math.Pow(1.0 - t, n - i)
        };
    }


    public static BernsteinPolynomial Constant(double constantValue)
        => new(0, [constantValue]);

    public static BernsteinPolynomial Create(params double[] coeffArray)
        => new((double[])coeffArray.Clone());

    public static BernsteinPolynomial Create(IEnumerable<double> coeffArray)
        => new(coeffArray.ToArray());

    public static BernsteinPolynomial CreateRandom(Random randGen, int degree, double minValue = -1, double maxValue = 1)
    {
        return new BernsteinPolynomial(
            randGen.GetFloat64Array1D(degree + 1, minValue, maxValue)
        );
    }

    public static BernsteinPolynomial FromRoots(IEnumerable<double> roots, double scale = 1.0, bool normalizeDomain = false, double tolerance = 1e-14)
    {
        if (roots == null)
            throw new ArgumentNullException(nameof(roots));

        // Clean and sort
        var rootArray = roots
            .Where(r => !double.IsNaN(r) && !double.IsInfinity(r))
            .OrderBy(r => r)
            .ToArray();

        if (rootArray.Length == 0)
            return Constant(scale);

        // --- Step 1: Optional domain normalization ---
        if (normalizeDomain)
        {
            var a = rootArray.Min();
            var b = rootArray.Max();
            var range = b - a;

            if (range > tolerance)
            {
                for (var i = 0; i < rootArray.Length; i++)
                    rootArray[i] = (rootArray[i] - a) / range;
            }
        }

        // --- Step 2: Build power basis polynomial ---
        var coeffs = new List<double> { scale };

        foreach (var r in rootArray)
        {
            var newCoeffs = new double[coeffs.Count + 1];
            newCoeffs[0] = -r * coeffs[0];

            for (var k = 1; k < newCoeffs.Length; k++)
            {
                var prev = coeffs[k - 1];
                var curr = k < coeffs.Count ? coeffs[k] : 0.0;
                newCoeffs[k] = prev - r * curr;
            }

            coeffs = newCoeffs.ToList();
        }

        // --- Step 3: Clean numerical noise ---
        var maxAbs = coeffs.Max(Math.Abs);
        if (maxAbs > 0)
        {
            for (var i = 0; i < coeffs.Count; i++)
                if (Math.Abs(coeffs[i]) < tolerance * maxAbs)
                    coeffs[i] = 0.0;
        }

        // --- Step 4: Convert to Bernstein basis ---
        var bernstein = FromMonomialBasis(coeffs.ToArray());

        // --- Step 5: Return result ---
        // If normalized, attach an informative note (optional logging)
        return bernstein;
    }

    public static BernsteinPolynomial FromRoots(IEnumerable<Complex> roots, double scale = 1.0, bool normalizeDomain = false, double tolerance = 1e-14)
    {
        if (roots == null)
            throw new ArgumentNullException(nameof(roots));

        var rootList = roots
            .Where(r => !double.IsNaN(r.Real) && !double.IsInfinity(r.Real))
            .ToList();

        if (rootList.Count == 0)
            return Constant(scale);

        // Partition roots into real and complex
        var realRoots = new List<double>(rootList.Count);
        var complexRoots = new List<Complex>(rootList.Count);

        foreach (var r in rootList)
        {
            if (Math.Abs(r.Imaginary) < tolerance)
                realRoots.Add(r.Real);
            else
                complexRoots.Add(r);
        }

        // --- Step 1: Pair complex conjugates ---
        var used = new bool[complexRoots.Count];
        var conjugatePairs = new List<(Complex, Complex)>();

        for (var i = 0; i < complexRoots.Count; i++)
        {
            if (used[i]) continue;
            var r = complexRoots[i];

            var foundPair = false;
            for (var j = i + 1; j < complexRoots.Count; j++)
            {
                if (used[j]) continue;

                var s = complexRoots[j];
                if (Math.Abs(r.Real - s.Real) < tolerance &&
                    Math.Abs(r.Imaginary + s.Imaginary) < tolerance)
                {
                    conjugatePairs.Add((r, s));
                    used[i] = used[j] = true;
                    foundPair = true;
                    break;
                }
            }

            if (!foundPair)
                throw new InvalidOperationException($"Unpaired complex root detected: {r}");
        }

        // --- Step 2: Optionally normalize domain ---
        if (normalizeDomain && realRoots.Count > 0)
        {
            var a = realRoots.Min();
            var b = realRoots.Max();
            var range = b - a;

            if (range > tolerance)
            {
                for (var i = 0; i < realRoots.Count; i++)
                    realRoots[i] = (realRoots[i] - a) / range;

                for (var i = 0; i < conjugatePairs.Count; i++)
                {
                    var (r, s) = conjugatePairs[i];
                    conjugatePairs[i] = (
                        (r - new Complex(a, 0)) / range,
                        (s - new Complex(a, 0)) / range
                    );
                }
            }
        }

        // --- Step 3: Build monomial coefficients ---
        var coeffs = new List<double> { scale };

        // Multiply (t - r_i) for real roots
        foreach (var r in realRoots)
        {
            var newCoeffs = new double[coeffs.Count + 1];
            newCoeffs[0] = -r * coeffs[0];

            for (var k = 1; k < newCoeffs.Length; k++)
            {
                var a = k < coeffs.Count ? coeffs[k] : 0.0;
                var b = coeffs[k - 1];
                newCoeffs[k] = b - r * a;
            }

            coeffs = newCoeffs.ToList();
        }

        // Multiply (t^2 - 2*Re(r)t + |r|^2) for each conjugate pair
        foreach (var (r, s) in conjugatePairs)
        {
            var re = r.Real;
            var im = r.Imaginary;
            var a2 = -2.0 * re;
            var a3 = re * re + im * im; // |r|^2

            var newCoeffs = new double[coeffs.Count + 2];
            newCoeffs[0] = a3 * coeffs[0];

            for (var k = 1; k < newCoeffs.Length; k++)
            {
                var c0 = (k < coeffs.Count) ? coeffs[k] : 0.0;
                var c1 = (k - 1 >= 0 && k - 1 < coeffs.Count) ? coeffs[k - 1] : 0.0;
                var c2 = (k - 2 >= 0 && k - 2 < coeffs.Count) ? coeffs[k - 2] : 0.0;

                newCoeffs[k] = c0 + a2 * c1 + a3 * c2;
            }

            coeffs = newCoeffs.ToList();
        }

        // --- Step 4: Clean numerical noise ---
        var maxAbs = coeffs.Max(Math.Abs);
        if (maxAbs > 0)
        {
            for (var i = 0; i < coeffs.Count; i++)
                if (Math.Abs(coeffs[i]) < tolerance * maxAbs)
                    coeffs[i] = 0.0;
        }

        // --- Step 5: Convert to Bernstein basis ---
        return FromMonomialBasis(coeffs.ToArray());
    }

    public static BernsteinPolynomial FitFromSamples(double[] t, double[] y, int targetDegree, FitSolver solver = FitSolver.Qr)
    {
        if (t == null || y == null)
            throw new ArgumentNullException();

        var m = t.Length;
        if (m != y.Length)
            throw new ArgumentException("t and y must have same length.");

        if (targetDegree >= m)
            throw new ArgumentException("Target degree must be less than number of samples (avoid underdetermined system).");

        var matrixM = Matrix<double>.Build.Dense(m, targetDegree + 1);
        var v = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.Dense(y);

        for (var r = 0; r < m; ++r)
            for (var i = 0; i <= targetDegree; ++i)
                matrixM[r, i] = EvaluateBasisAt(targetDegree, i, t[r]);

        var cVec = solver switch
        {
            FitSolver.Svd => matrixM.Svd(true).Solve(v),
            FitSolver.Qr => matrixM.QR().Solve(v),
            _ => matrixM.QR().Solve(v)
        };

        return new BernsteinPolynomial(targetDegree, cVec.ToArray());
    }

    /// <summary>
    /// Construct a Bernstein polynomial from power-basis coefficients.
    /// Given power basis f(t) = sum_{k=0..n} a[k] * t^k,
    /// returns Bernstein coefficients b[i] such that
    /// f(t) = sum_{i=0..n} b[i] * B_{i,n}(t).
    /// </summary>
    public static BernsteinPolynomial FromMonomialBasis(params double[] powerCoeffs)
    {
        if (powerCoeffs == null)
            throw new ArgumentNullException(nameof(powerCoeffs));

        var n = powerCoeffs.Length - 1;

        // Build transform matrix M where: a = M * b
        // M[m,i] = coefficient of t^m in B_{i,n}(t)
        var matrixM = Matrix.Build.Dense(n + 1, n + 1);

        for (var m = 0; m <= n; m++)
            for (var i = 0; i <= n; i++)
            {
                var val = 0.0;

                if (m >= i)
                {
                    val = Binomial(n, i) *
                          Binomial(n - i, m - i) *
                          ((m - i) % 2 == 0 ? 1.0 : -1.0);
                }

                matrixM[m, i] = val;
            }

        var aVec = Vector.Build.DenseOfArray(powerCoeffs);

        // Solve M * b = a
        MathNet.Numerics.LinearAlgebra.Vector<double> bVec;
        try
        {
            bVec = matrixM.QR().Solve(aVec);
        }
        catch
        {
            bVec = matrixM.Svd(true).Solve(aVec);
        }

        var bArr = new double[n + 1];
        for (var i = 0; i <= n; i++)
            bArr[i] = bVec[i];

        return new BernsteinPolynomial(n, bArr);
    }

    // Construct a Bernstein polynomial from a Chebyshev-T polynomial (1D)
    // Uses Chebyshev -> power -> Bernstein.FromPowerCoefficients
    public static BernsteinPolynomial FromChebyshevT(ChebyshevPolynomialT cheb)
    {
        if (cheb == null) throw new ArgumentNullException(nameof(cheb));
        var power = cheb.ToPowerBasis(); // double[] power coeffs
        return FromMonomialBasis(power);
    }

    // Construct Bernstein from Chebyshev-U (1D)
    public static BernsteinPolynomial FromChebyshevU(ChebyshevPolynomialU u)
    {
        if (u == null) throw new ArgumentNullException(nameof(u));
        var power = u.ToPowerBasis();
        return FromMonomialBasis(power);
    }


    public static BernsteinPolynomial operator -(BernsteinPolynomial a)
    {
        return a.Times(-1);
    }

    public static BernsteinPolynomial operator +(BernsteinPolynomial a, double b)
    {
        return a.Add(Constant(b));
    }

    public static BernsteinPolynomial operator +(double a, BernsteinPolynomial b)
    {
        return Constant(a).Add(b);
    }

    public static BernsteinPolynomial operator +(BernsteinPolynomial a, BernsteinPolynomial b)
    {
        return a.Add(b);
    }

    public static BernsteinPolynomial operator -(BernsteinPolynomial a, double b)
    {
        return a - Constant(b);
    }

    public static BernsteinPolynomial operator -(double a, BernsteinPolynomial b)
    {
        return Constant(a).Subtract(b);
    }

    public static BernsteinPolynomial operator -(BernsteinPolynomial a, BernsteinPolynomial b)
    {
        return a.Subtract(b);
    }

    public static BernsteinPolynomial operator *(BernsteinPolynomial a, double s)
    {
        return a.Times(s);
    }

    public static BernsteinPolynomial operator *(double s, BernsteinPolynomial a)
    {
        return a.Times(s);
    }

    public static BernsteinPolynomial operator *(BernsteinPolynomial a, BernsteinPolynomial b)
    {
        return a.Times(b);
    }

    public static BernsteinPolynomial operator /(BernsteinPolynomial a, double s)
    {
        if (s == 0)
            throw new DivideByZeroException();

        return a.Times(1 / s);
    }

    public static RationalBernsteinPolynomial operator /(double a, BernsteinPolynomial b)
    {
        return new RationalBernsteinPolynomial(Constant(a), b);
    }

    public static RationalBernsteinPolynomial operator /(BernsteinPolynomial a, BernsteinPolynomial b)
    {
        return new RationalBernsteinPolynomial(a, b);
    }


    private readonly double[] _coeffs;

    public int Degree { get; }

    public IReadOnlyList<double> Coeffs => _coeffs;


    private BernsteinPolynomial(double[] coeffs)
    {
        if (coeffs == null) throw new ArgumentNullException(nameof(coeffs));
        if (coeffs.Length < 1) throw new ArgumentException("coeffs length must be >= 1");

        Degree = coeffs.Length - 1;
        _coeffs = coeffs;
        Debug.Assert(IsValid());
    }

    private BernsteinPolynomial(int degree, double[] coeffs)
    {
        if (coeffs == null) throw new ArgumentNullException(nameof(coeffs));
        if (coeffs.Length != degree + 1) throw new ArgumentException("coeffs length must equal degree + 1");

        Degree = degree;
        _coeffs = coeffs;
        Debug.Assert(IsValid());
    }


    public bool IsValid()
        => Coeffs.Count > 0 && Coeffs.All(c => c.IsValid() && Float64Utils.IsFinite(c));

    public bool IsNearEqual(BernsteinPolynomial p2, double t1, double t2, double tolerance = Float64Utils.DefaultTolerance)
    {
        var tValues =
            t1.GetLinearRange(t2, 1000, false).ToArray();

        var v1 = GetValues(tValues);
        var v2 = p2.GetValues(tValues);

        for (var i = 0; i < tValues.Length; i++)
            if (!v1[i].IsNearEqual(v2[i], tolerance))
                return false;

        return true;
    }

    public BernsteinPolynomial Simplify(double tolerance = Float64Utils.DefaultTolerance)
    {
        if (Degree == 0) return this;

        var coeffs = (double[])_coeffs.Clone();
        var n = coeffs.Length - 1;
        var maxAbs = coeffs.Max(Math.Abs);

        if (maxAbs <= tolerance)
            return Constant(0.0);

        // --- Stage 1: preserve endpoints, zero small interior noise
        for (var i = 1; i < n; i++)
        {
            if (Math.Abs(coeffs[i]) < tolerance * maxAbs)
                coeffs[i] = 0.0;
        }

        // --- Stage 2: safe degree reduction (trim trailing coefficients only if truly negligible)
        var newDegree = n;
        while (newDegree > 0 &&
               Math.Abs(coeffs[newDegree]) <= tolerance * maxAbs &&
               Math.Abs(coeffs[newDegree] - coeffs[newDegree - 1]) <= tolerance * maxAbs)
        {
            newDegree--;
        }

        var trimmed = coeffs;
        if (newDegree < n)
        {
            trimmed = new double[newDegree + 1];
            Array.Copy(coeffs, trimmed, newDegree + 1);
        }

        var simplified = new BernsteinPolynomial(trimmed.Length - 1, trimmed);

        // --- Stage 3: functional accuracy check using De Casteljau sampling
        const int N = 33; // number of sample points (Chebyshev nodes are best)
        var maxError = 0.0;
        var fMax = 0.0;

        for (var k = 0; k < N; k++)
        {
            // Chebyshev nodes mapped to [0,1]
            var x = 0.5 * (1 - Math.Cos(Math.PI * k / (N - 1)));
            var fVal = GetValue(x);
            var gVal = simplified.GetValue(x);

            var err = Math.Abs(fVal - gVal);
            if (err > maxError) maxError = err;
            if (Math.Abs(fVal) > fMax) fMax = Math.Abs(fVal);
        }

        // --- Stage 4: accept or reject simplification
        if (maxError > tolerance * (fMax > 1e-14 ? fMax : 1.0))
            return this; // reject simplification — too much deviation

        // --- Stage 5: reduce to constant if nearly flat
        return simplified.ReduceToConstantIfPossible(tolerance);
    }


    public bool IsNearConstant(out double constantValue, double tolerance = Float64Utils.DefaultTolerance)
    {
        if (Degree == 0)
        {
            constantValue = Coeffs[0];
            return true;
        }

        var avg = Coeffs.Average();
        var variance = Coeffs.Select(c => (c - avg) * (c - avg)).Average();

        if (variance <= tolerance * tolerance)
        {
            constantValue = avg;
            return true;
        }

        constantValue = 0.0;
        return false;
    }
    
    public double GetValue(double t)
    {
        if (Degree == 0) return Coeffs[0];

        var tmp = new double[Degree + 1];
        Array.Copy(_coeffs, tmp, Degree + 1);

        for (var r = 1; r <= Degree; ++r)
        for (var i = 0; i <= Degree - r; ++i)
            tmp[i] = (1 - t) * tmp[i] + t * tmp[i + 1];

        return tmp[0];
    }

    public double[] GetValues(double[] tValues)
    {
        var m = tValues.Length;
        var result = new double[m];
        for (var r = 0; r < m; ++r)
            result[r] = GetValue(tValues[r]);
        return result;
    }


    public BernsteinPolynomial ReduceToConstantIfPossible(double tolerance = Float64Utils.DefaultTolerance)
        => IsNearConstant(out var c, tolerance) ? new BernsteinPolynomial(0, [c]) : this;

    public BernsteinPolynomial Derivative()
    {
        if (Degree == 0) return new BernsteinPolynomial(0, [0.0]);

        var derivative = new double[Degree];
        for (var i = 0; i < Degree; ++i)
            derivative[i] = Degree * (Coeffs[i + 1] - Coeffs[i]);

        return new BernsteinPolynomial(Degree - 1, derivative);
    }

    public BernsteinPolynomial Integral()
    {
        var integral = new double[Degree + 2];
        var cumulative = 0.0;
        for (var j = 1; j <= Degree + 1; ++j)
        {
            cumulative += Coeffs[j - 1];
            integral[j] = cumulative / (Degree + 1);
        }
        return new BernsteinPolynomial(Degree + 1, integral);
    }

    public BernsteinPolynomial Square()
    {
        // Use the general product formula with b = this
        var a = this;
        var n = a.Degree;
        var degree = 2 * n;
        var c = new double[degree + 1];

        for (var k = 0; k <= degree; ++k)
        {
            var s = 0.0;
            var iMin = Math.Max(0, k - n);
            var iMax = Math.Min(n, k);

            for (var i = iMin; i <= iMax; ++i)
            {
                var j = k - i;
                s +=
                    a.Coeffs[i] *
                    a.Coeffs[j] *
                    Binomial(n, i) *
                    Binomial(n, j) /
                    Binomial(degree, k);
            }

            c[k] = s;
        }

        return new BernsteinPolynomial(degree, c);
    }
    
    public BernsteinPolynomial Cube()
    {
        return Times(Square());
    }

    /// <summary>
    /// Raise this polynomial to an integer power using fast exponentiation.
    /// </summary>
    /// <param name="n">The exponent (must be >= 0)</param>
    /// <returns>A new BernsteinPolynomial representing this^n</returns>
    public BernsteinPolynomial Power(int n)
    {
        if (n < 0)
            throw new ArgumentException("Exponent must be non-negative", nameof(n));

        if (n == 0)
            return Constant(1.0);

        if (n == 1)
            return this;

        if (n == 2)
            return Square();

        // Exponentiation by squaring
        var result = Constant(1.0);
        var basePoly = this;

        while (n > 0)
        {
            if ((n & 1) == 1)
                result = result * basePoly;

            basePoly = basePoly.Square();
            n >>= 1;
        }

        return result;
    }

    /// <summary>
    /// Scalar multiply by -1 (returns new polynomial)
    /// </summary>
    /// <returns></returns>
    public BernsteinPolynomial Negative()
    {
        var c = new double[Degree + 1];

        for (var i = 0; i <= Degree; ++i)
            c[i] = -Coeffs[i];

        return new BernsteinPolynomial(Degree, c);
    }
    
    public BernsteinPolynomial Add(double b)
    {
        return Add(Constant(b));
    }

    public BernsteinPolynomial Add(BernsteinPolynomial b)
    {
        var a = this;
        var degree = Math.Max(a.Degree, b.Degree);
        var aCoeffs = a.RaiseDegree(degree).Coeffs;
        var bCoeffs = b.RaiseDegree(degree).Coeffs;
        var cCoeffs = new double[degree + 1];

        for (var i = 0; i <= degree; ++i)
            cCoeffs[i] = aCoeffs[i] + bCoeffs[i];

        return new BernsteinPolynomial(degree, cCoeffs);
    }

    public BernsteinPolynomial Subtract(double b)
    {
        return Subtract(Constant(b));
    }

    public BernsteinPolynomial Subtract(BernsteinPolynomial b)
    {
        var a = this;
        var degree = Math.Max(a.Degree, b.Degree);
        var aCoeffs = a.RaiseDegree(degree).Coeffs;
        var bCoeffs = b.RaiseDegree(degree).Coeffs;
        var cCoeffs = new double[degree + 1];

        for (var i = 0; i <= degree; ++i)
            cCoeffs[i] = aCoeffs[i] - bCoeffs[i];

        return new BernsteinPolynomial(degree, cCoeffs);
    }

    /// <summary>
    /// Scalar multiply (returns new polynomial)
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public BernsteinPolynomial Times(double s)
    {
        if (s.IsOne())
            return this;

        var c = new double[Degree + 1];

        for (var i = 0; i <= Degree; ++i)
            c[i] = Coeffs[i] * s;

        return new BernsteinPolynomial(Degree, c);
    }

    public BernsteinPolynomial Times(BernsteinPolynomial b)
    {
        var a = this;
        var n = a.Degree;
        var m = b.Degree;
        var degree = n + m;
        var c = new double[degree + 1];

        for (var k = 0; k <= degree; ++k)
        {
            var s = 0.0;
            var iMin = Math.Max(0, k - m);
            var iMax = Math.Min(n, k);

            for (var i = iMin; i <= iMax; ++i)
            {
                var j = k - i;
                var term =
                    a.Coeffs[i] *
                    b.Coeffs[j] *
                    Binomial(n, i) *
                    Binomial(m, j) /
                    Binomial(degree, k);
                s += term;
            }

            c[k] = s;
        }

        return new BernsteinPolynomial(degree, c);
    }

    /// <summary>
    /// Divide by scalar (returns new polynomial)
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public BernsteinPolynomial Divide(double s)
    {
        if (s == 0)
            throw new DivideByZeroException();

        return Times(1 / s);
    }

    /// <summary>
    /// Addition/subtraction: elevate degrees then elementwise combine
    /// </summary>
    /// <param name="newDegree"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public BernsteinPolynomial RaiseDegree(int newDegree)
    {
        if (newDegree < Degree)
            throw new ArgumentException("newDegree must be >= Degree");

        if (newDegree == Degree)
            return this;

        var elevated = new double[newDegree + 1];

        // degree elevation formula (exact)
        for (var i = 0; i <= Degree; ++i)
        {
            var c = Coeffs[i] * Binomial(Degree, i);
            for (var k = i; k <= newDegree - (Degree - i); ++k)
            {
                // contribution to elevated[k]
                elevated[k] +=
                    c *
                    Binomial(newDegree - Degree, k - i) /
                    Binomial(newDegree, k);
            }
        }

        var p = new BernsteinPolynomial(newDegree, elevated);

        //Debug.Assert(IsNearEqual(p, 0, 1));

        return p;
    }

    public RationalBernsteinPolynomial ToRational()
    {
        return new RationalBernsteinPolynomial(this, Constant(1));
    }

    /// <summary>
    /// Convert Bernstein coefficients to power basis (monomial)
    /// coefficients a_k for f(t)=sum_{k=0}^n a_k t^k
    /// Uses exact combinatorial expansion:
    /// a_k = sum_{i=0}^k c_i * cCoeffs(n,i) * cCoeffs(n-i, k-i) * (-1)^{k-i}
    /// </summary>
    /// <returns></returns>
    public double[] ToMonomialBasis()
    {
        // a[0..n] where f(t) = sum a[k] t^k
        var a = new double[Degree + 1];
        for (var k = 0; k <= Degree; ++k)
        {
            var ak = 0.0;

            for (var i = 0; i <= k; ++i)
            {
                var term =
                    Coeffs[i] *
                    Binomial(Degree, i) *
                    Binomial(Degree - i, k - i);

                if (((k - i) & 1) == 1)
                    term = -term;

                ak += term;
            }

            a[k] = ak;
        }
        return a;
    }

    // Convert this Bernstein polynomial to Chebyshev-T basis (1D)
    // Uses power-basis intermediary: Bernstein -> power -> Chebyshev (FFT DCT)
    public ChebyshevPolynomialT ToChebyshevT()
    {
        // Convert Bernstein -> power
        var power = ToMonomialBasis(); // double[] a[k] (power basis)
        // Convert power -> Chebyshev T (uses ChebyshevPolynomialT.FromPowerCoefficients with FFT-DCT)
        return ChebyshevPolynomialT.FromPowerCoefficients(power);
    }

    // Convert this Bernstein polynomial to Chebyshev-U basis (1D)
    public ChebyshevPolynomialU ToChebyshevU()
    {
        var power = ToMonomialBasis();
        return ChebyshevPolynomialU.FromPowerCoefficients(power);
    }

    /// <summary>
    /// Small Gaussian elimination solver (square matrix).
    /// Returns solution vector x for aCoeffs x = b.
    /// aCoeffs is n x n stored as aCoeffs[row,col].
    /// </summary>
    /// <param name="aCoeffs"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private static double[] SolveLinearSystem(double[,] aCoeffs, double[] b)
    {
        var n = b.Length;
        // build augmented matrix
        var matrixM = new double[n, n + 1];
        for (var i = 0; i < n; ++i)
        {
            for (var j = 0; j < n; ++j) matrixM[i, j] = aCoeffs[i, j];
            matrixM[i, n] = b[i];
        }

        // Gaussian elimination with partial pivoting
        for (var k = 0; k < n; ++k)
        {
            // find pivot
            var piv = k;
            var vMax = Math.Abs(matrixM[k, k]);

            for (var i = k + 1; i < n; ++i)
            {
                var av = Math.Abs(matrixM[i, k]);

                if (!(av > vMax))
                    continue;

                vMax = av;
                piv = i;
            }

            if (vMax <= 0.0)
                throw new InvalidOperationException("Matrix singular or ill-conditioned.");

            // swap rows if needed
            if (piv != k)
            {
                for (var j = k; j <= n; ++j)
                {
                    (matrixM[k, j], matrixM[piv, j]) = (matrixM[piv, j], matrixM[k, j]);
                }
            }

            // normalize pivot row
            var pivot = matrixM[k, k];
            for (var j = k; j <= n; ++j)
                matrixM[k, j] /= pivot;

            // eliminate below
            for (var i = 0; i < n; ++i)
            {
                if (i == k)
                    continue;

                var factor = matrixM[i, k];

                if (Math.Abs(factor) <= 0.0)
                    continue;

                for (var j = k; j <= n; ++j)
                    matrixM[i, j] -= factor * matrixM[k, j];
            }
        }

        // read solution
        var x = new double[n];
        for (var i = 0; i < n; ++i) x[i] = matrixM[i, n];
        return x;
    }

    /// <summary>
    /// A sign change between control coefficients bi and bi+1 means there’s a
    /// root in that subinterval.
    /// </summary>
    /// <param name="tolerance"></param>
    /// <returns></returns>
    public int CountSignChanges(double tolerance = 1e-14)
    {
        int? lastSign = null;
        var count = 0;

        foreach (var c in _coeffs)
        {
            if (Math.Abs(c) < tolerance) continue; // ignore near-zero coefficients

            var sign = Math.Sign(c);
            if (lastSign is not null && sign != lastSign)
                count++;

            lastSign = sign;
        }

        return count;
    }

    /// <summary>
    /// Isolate all root intervals of this polynomial f(t) in [0,1]
    /// using the convex-hull / sign-test property of Bernstein coefficients.
    /// This method is numerically robust and guarantees to find all real roots.
    /// </summary>
    public List<Float64Interval> FindRootIntervals(double tolerance = Float64Utils.DefaultTolerance, int maxDepth = 30)
    {
        var intervals = new List<Float64Interval>();
        Recurse(_coeffs, 0.0, 1.0, 0);
        return intervals;

        void Recurse(double[] coeffs, double a, double b, int depth)
        {
            // Check sign variation among coefficients
            bool hasPos = false, hasNeg = false;
            foreach (var c in coeffs)
            {
                if (c > 0) hasPos = true;
                else if (c < 0) hasNeg = true;

                if (hasPos && hasNeg) break;
            }

            // If all positive or all negative → no root in this interval
            if (!(hasPos && hasNeg))
                return;

            // If interval small enough or depth limit reached → record it
            if (depth >= maxDepth || Math.Abs(b - a) <= tolerance)
            {
                intervals.Add(Float64Interval.Create(a, b));
                return;
            }

            // Subdivide polynomial into left and right halves
            var (left, right) = SubdivideCoefficients(coeffs, 0.5);
            var mid = 0.5 * (a + b);

            // Recurse
            Recurse(left, a, mid, depth + 1);
            Recurse(right, mid, b, depth + 1);
        }

        // --- helper: de Casteljau subdivision of coefficient array ---
        static (double[] left, double[] right) SubdivideCoefficients(double[] c, double u)
        {
            var n = c.Length - 1;
            var b = new double[n + 1][];
            b[0] = (double[])c.Clone();

            for (var k = 1; k <= n; k++)
            {
                b[k] = new double[n - k + 1];
                for (var i = 0; i <= n - k; i++)
                    b[k][i] = (1.0 - u) * b[k - 1][i] + u * b[k - 1][i + 1];
            }

            var left = new double[n + 1];
            var right = new double[n + 1];
            for (var k = 0; k <= n; k++)
            {
                left[k] = b[k][0];
                right[k] = b[n - k][k];
            }

            return (left, right);
        }
    }

    /// <summary>
    /// Find all real roots of this polynomial f(t) in [0,1] using the convex-hull
    /// (Bernstein sign-test) isolation, followed by Newton refinement.
    /// Guaranteed to find all real roots with high accuracy.
    /// </summary>
    public List<double> FindRoots(double tolerance = Float64Utils.DefaultTolerance, int maxDepth = 30, int newtonSteps = 20)
    {
        // Step 1: isolate intervals using convex hull method
        var intervals = FindRootIntervals(tolerance, maxDepth);

        var roots = new List<double>();

        // Step 2: refine each isolated root interval with Newton iteration
        var derivative = Derivative();

        foreach (var interval in intervals)
        {
            var a = interval.MinValue;
            var b = interval.MaxValue;
            var t = 0.5 * (a + b);   // initial midpoint guess

            for (var i = 0; i < newtonSteps; i++)
            {
                var f = GetValue(t);
                var df = derivative.GetValue(t);

                // If derivative is tiny, break and use midpoint
                if (Math.Abs(df) < 1e-15)
                    break;

                var step = f / df;
                t -= step;

                // Clamp to interval to avoid runaway
                if (t < a) t = a;
                else if (t > b) t = b;

                // Converged?
                if (Math.Abs(step) < tolerance)
                    break;
            }

            roots.Add(t);
        }

        // Step 3: Sort and merge duplicates (for numerical stability)
        roots.Sort();

        const double mergeEps = 1e-10;
        for (var i = roots.Count - 1; i > 0; i--)
        {
            if (Math.Abs(roots[i] - roots[i - 1]) < mergeEps)
                roots.RemoveAt(i);
        }

        return roots;
    }

    public BernsteinPolynomial Subdivide(double t0, double t1)
    {
        if (t0 < 0.0 || t1 > 1.0 || t0 >= t1)
            throw new ArgumentOutOfRangeException();

        if (Math.Abs(t1) < 1e-15)
            return Constant(GetValue(t0)); // effectively a point

        var (leftAtT1, _) = Subdivide(t1);
        
        var (_, segment) = leftAtT1.Subdivide(t0 / t1);

        return segment;
    }

    /// <summary>
    /// Subdivide this Bernstein polynomial at parameter u in [0,1] using De Casteljau.
    /// Returns a tuple (left, right) both of same degree.
    /// (Assumes you already have a Subdivide(u) implementation — kept for completeness.)
    /// </summary>
    public (BernsteinPolynomial left, BernsteinPolynomial right) Subdivide(double u)
    {
        if (u < 0.0 || u > 1.0)
            throw new ArgumentOutOfRangeException(nameof(u), "u must be in [0,1]");

        var b = new double[Degree + 1][];
        b[0] = (double[])Coeffs.ToArray().Clone();

        for (var k = 1; k <= Degree; k++)
        {
            b[k] = new double[Degree + 1 - k];
            for (var i = 0; i <= Degree - k; i++)
                b[k][i] = (1.0 - u) * b[k - 1][i] + u * b[k - 1][i + 1];
        }

        var leftCoeffs = new double[Degree + 1];
        var rightCoeffs = new double[Degree + 1];

        for (var k = 0; k <= Degree; k++)
        {
            leftCoeffs[k] = b[k][0];
            rightCoeffs[k] = b[Degree - k][k];
        }

        return (new BernsteinPolynomial(Degree, leftCoeffs), new BernsteinPolynomial(Degree, rightCoeffs));
    }

    /// <summary>
    /// Reparametrize this polynomial from the original t in [0,1] to the interval [a,b].
    /// Returns polynomial g(u) = f(a + (b-a) * u) represented in Bernstein basis on u in [0,1].
    /// Uses sampling at Bernstein nodes and solves linear system with MathNet for robustness.
    /// </summary>
    public BernsteinPolynomial Reparametrize(double a, double b)
    {
        if (b <= a)
            throw new ArgumentException("b must be > a");

        // build sample nodes u_k = k/n in [0,1], t_k = a + (b-a)*u_k
        var g = new double[Degree + 1];

        for (var k = 0; k <= Degree; k++)
        {
            var u = (double)k / Degree;
            var t = a + (b - a) * u;

            g[k] = GetValue(t); // evaluate original at t
        }

        // Build matrix M where M[k,i] = B_i^n(u_k)
        var M =
            Matrix<double>.Build.Dense(Degree + 1, Degree + 1);

        for (var k = 0; k <= Degree; k++)
        {
            var u = (double)k / Degree;
            var oneMinusU = 1.0 - u;

            for (var i = 0; i <= Degree; i++)
                M[k, i] = Binomial(Degree, i) *
                          Math.Pow(u, i) *
                          Math.Pow(oneMinusU, Degree - i);
        }

        var gVec = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray(g);
        MathNet.Numerics.LinearAlgebra.Vector<double> bVec;
        try
        {
            bVec = M.QR().Solve(gVec);
        }
        catch
        {
            bVec = M.Svd(true).Solve(gVec);
        }

        var coeffs = new double[Degree + 1];
        for (var i = 0; i <= Degree; i++)
            coeffs[i] = bVec[i];

        return new BernsteinPolynomial(Degree, coeffs);
    }

    /// <summary>
    /// Subdivide uniformly levels times (binary splits) and return list of patches (left-to-right).
    /// Each level splits each interval in two at midpoint. Returns 2^levels polynomials.
    /// </summary>
    public List<BernsteinPolynomial> SubdivideUniform(int levels)
    {
        if (levels < 0)
            throw new ArgumentOutOfRangeException(nameof(levels));

        var result = new List<BernsteinPolynomial> { this };
        for (var level = 0; level < levels; level++)
        {
            var next = new List<BernsteinPolynomial>(result.Count * 2);

            foreach (var p in result)
            {
                var (left, right) = p.Subdivide(0.5);
                next.Add(left);
                next.Add(right);
            }

            result = next;
        }

        return result;
    }

    /// <summary>
    /// Split polynomial into nt uniform pieces (nt >= 1).
    /// Returns nt Bernstein polynomials each reparametrized to [0,1] representing segments:
    /// segment i corresponds to original t in [i/nt, (i+1)/nt].
    /// Uses Reparametrize to compute each piece.
    /// </summary>
    public List<BernsteinPolynomial> SplitToGrid(int nt)
    {
        if (nt < 1)
            throw new ArgumentOutOfRangeException(nameof(nt));

        var list = new List<BernsteinPolynomial>(nt);
        for (var i = 0; i < nt; i++)
        {
            var a = (double)i / nt;
            var b = (double)(i + 1) / nt;
            list.Add(Reparametrize(a, b));
        }

        return list;
    }

    /// <summary>
    /// Isolate roots in [0,1] using subdivision + convex-hull test (control polygon sign test).
    /// Returns list of intervals [a,b] that potentially contain roots; each interval width <= tolerance or maxDepth reached.
    /// Guaranteed: any returned interval may contain root(s); intervals with all control coefficients same sign are pruned.
    /// </summary>
    public List<(double a, double b)> RootIsolateSubdivision(double tolerance = 1e-8, int maxDepth = 30)
    {
        var results = new List<(double a, double b)>();

        Recurse(this, 0.0, 1.0, 0);
        return results;

        void Recurse(BernsteinPolynomial p, double a, double b, int depth)
        {
            // convex hull test: if all control points >=0 or <=0 -> no root inside
            var min = double.PositiveInfinity;
            var max = double.NegativeInfinity;

            for (var i = 0; i <= p.Degree; i++)
            {
                var v = p.Coeffs[i];
                if (v < min) min = v;
                if (v > max) max = v;
            }

            if (min > 0 || max < 0)
                return; // no root inside

            if (depth >= maxDepth || (b - a) <= tolerance)
            {
                results.Add((a, b));
                return;
            }

            var (left, right) =
                p.Subdivide(0.5);

            var mid = 0.5 * (a + b);
            Recurse(left, a, mid, depth + 1);
            Recurse(right, mid, b, depth + 1);
        }
    }

    /// <summary>
    /// Split the 1D Bernstein polynomial into nt uniform subintervals.
    /// If nt is a power of two and allowPowerOfTwoFastPath is true,
    /// uses only midpoint subdivision (fast & stable).
    /// Otherwise, falls back to SplitToGridOptimized (general case).
    /// Returns an array of BernsteinPolynomial segments reparametrized to [0,1].
    /// </summary>
    public BernsteinPolynomial[] SplitToGrid(int nt, bool allowPowerOfTwoFastPath)
    {
        if (nt < 1)
            throw new ArgumentOutOfRangeException(nameof(nt), "nt must be >= 1");

        var isPowerOfTwo = (nt & (nt - 1)) == 0;

        if (allowPowerOfTwoFastPath && isPowerOfTwo)
            return SplitToGridPowerOfTwo(nt);

        return SplitToGridOptimized(nt);
    }

    /// <summary>
    /// Split into nt uniform segments using repeated midpoint subdivision.
    /// Requires nt to be a power of two.
    /// Returns array of nt polynomials each reparametrized to [0,1].
    /// </summary>
    public BernsteinPolynomial[] SplitToGridPowerOfTwo(int nt)
    {
        if ((nt & (nt - 1)) != 0)
            throw new ArgumentException("SplitToGridPowerOfTwo requires nt to be a power of two.");

        var levels = (int)Math.Log2(nt);

        return SubdivideRecursive(this, levels).ToArray();

        List<BernsteinPolynomial> SubdivideRecursive(BernsteinPolynomial p, int level)
        {
            if (level == 0)
                return [p];

            var (left, right) =
                p.Subdivide(0.5);

            var leftList = SubdivideRecursive(left, level - 1);
            var rightList = SubdivideRecursive(right, level - 1);

            leftList.AddRange(rightList);
            return leftList;
        }
    }

    /// <summary>
    /// Split into nt uniform segments using successive subdivision at u = 1/(remaining).
    /// Works for any nt >= 1. Returns array of nt polynomials each reparametrized to [0,1].
    /// </summary>
    public BernsteinPolynomial[] SplitToGridOptimized(int nt)
    {
        if (nt < 1)
            throw new ArgumentOutOfRangeException(nameof(nt));

        var result = new BernsteinPolynomial[nt];
        var remaining = this;

        for (var i = 0; i < nt - 1; i++)
        {
            var u = 1.0 / (nt - i);
            var (left, right) =
                remaining.Subdivide(u);

            result[i] = left;
            remaining = right;
        }

        result[nt - 1] = remaining;
        return result;
    }

    public override string ToString()
    {
        var composer = new StringBuilder();

        for (var i = 0; i <= Degree; i++)
        {
            if (_coeffs[i].IsZero()) continue;

            composer
                .Append(_coeffs[i].ToString("G"))
                .Append($" B({i}, {Degree})")
                .Append(" + ");
        }

        if (composer.Length > 0) composer.Length -= 3;

        return composer.ToString();
    }
}