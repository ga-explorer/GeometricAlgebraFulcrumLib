using System.Numerics;
using MathNet.Numerics.IntegralTransforms;

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Chebyshev;

public class ChebyshevPolynomial2D
{

    public int DegreeT { get; }
    public int DegreeS { get; }
    public double[,] Coeffs { get; } // a[p,q] for T_p(t) * T_q(s)

    public ChebyshevPolynomial2D(int degreeT, int degreeS, double[,] coeffs)
    {
        if (coeffs == null) throw new ArgumentNullException(nameof(coeffs));
        if (coeffs.GetLength(0) != degreeT + 1 || coeffs.GetLength(1) != degreeS + 1)
            throw new ArgumentException("coeffs dimensions must be degreeT+1 by degreeS+1");
        DegreeT = degreeT;
        DegreeS = degreeS;
        Coeffs = (double[,])coeffs.Clone();
    }

    // nested Clenshaw for evaluation
    public double EvaluateAt(double t, double s)
    {
        var temp = new double[DegreeT + 1];
        for (var p = 0; p <= DegreeT; p++)
        {
            double b1 = 0.0, b2 = 0.0;
            for (var q = DegreeS; q >= 1; q--)
            {
                var b = 2.0 * s * b1 - b2 + Coeffs[p, q];
                b2 = b1; b1 = b;
            }
            temp[p] = s * b1 - b2 + Coeffs[p, 0];
        }

        double c1 = 0.0, c2 = 0.0;
        for (var p = DegreeT; p >= 1; p--)
        {
            var c = 2.0 * t * c1 - c2 + temp[p];
            c2 = c1; c1 = c;
        }
        return t * c1 - c2 + temp[0];
    }

    // ---------------- 1D FFT DCT-II helpers reused in 2D ----------------
    private static Complex[] BuildEvenSymmetricArray(double[] data)
    {
        var N = data.Length;
        var y = new Complex[2 * N];
        for (var j = 0; j < N; j++) y[j] = new Complex(data[j], 0.0);
        for (var j = 0; j < N; j++) y[2 * N - 1 - j] = new Complex(data[j], 0.0);
        return y;
    }

    private static double[] Dct2ViaFft_Single(double[] data)
    {
        var N = data.Length;
        var y = BuildEvenSymmetricArray(data);
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

    private static double[] Idct3ViaFft_Single(double[] c)
    {
        var N = c.Length;
        var Y = new Complex[2 * N];
        for (var k = 0; k < N; k++)
        {
            var theta = Math.PI * k / (2.0 * N);
            var phase = new Complex(Math.Cos(theta), Math.Sin(theta));
            Y[k] = phase * c[k];
        }
        for (var k = 1; k < N; k++) Y[2 * N - k] = Complex.Conjugate(Y[k]);
        Fourier.Inverse(Y, FourierOptions.Matlab);
        var data = new double[N];
        for (var j = 0; j < N; j++) data[j] = Y[j].Real;
        return data;
    }

    // ---------------- 2D Fit using separable FFT DCT-II ----------------
    // f: function on [-1,1]^2; degrees = DegreeT, DegreeS
    public static ChebyshevPolynomial2D Fit(Func<double, double, double> f, int degreeT, int degreeS)
    {
        if (f == null) throw new ArgumentNullException(nameof(f));
        if (degreeT < 0 || degreeS < 0) throw new ArgumentOutOfRangeException("degrees must be >= 0");

        var N = degreeT + 1;
        var M = degreeS + 1;

        // sample function on Chebyshev tensor grid
        var tNodes = new double[N];
        var sNodes = new double[M];
        for (var j = 0; j < N; j++) tNodes[j] = Math.Cos(Math.PI * (j + 0.5) / N);
        for (var k = 0; k < M; k++) sNodes[k] = Math.Cos(Math.PI * (k + 0.5) / M);

        // samples[j,k]
        var samples = new double[N, M];
        for (var j = 0; j < N; j++)
            for (var k = 0; k < M; k++)
                samples[j, k] = f(tNodes[j], sNodes[k]);

        // Step 1: for each row j, compute 1D DCT-II across s (size M)
        var temp = new double[N, M];
        var rowBuffer = new double[M];
        for (var j = 0; j < N; j++)
        {
            for (var k = 0; k < M; k++) rowBuffer[k] = samples[j, k];
            var craw = Dct2ViaFft_Single(rowBuffer);
            for (var q = 0; q < M; q++) temp[j, q] = craw[q];
        }

        // Step 2: for each q, compute DCT-II across t (size N) on temp[:,q]
        var S = new double[N, M];
        var colBuffer = new double[N];
        for (var q = 0; q < M; q++)
        {
            for (var j = 0; j < N; j++) colBuffer[j] = temp[j, q];
            var craw = Dct2ViaFft_Single(colBuffer);
            for (var p = 0; p < N; p++) S[p, q] = craw[p];
        }

        // Step 3: scale to get Chebyshev coefficients
        var coeffs = new double[N, M];
        var scale = (2.0 / N) * (2.0 / M);
        for (var p = 0; p < N; p++)
            for (var q = 0; q < M; q++)
            {
                var a = scale * S[p, q];
                if (p == 0) a *= 0.5;
                if (q == 0) a *= 0.5;
                coeffs[p, q] = a;
            }

        return new ChebyshevPolynomial2D(degreeT, degreeS, coeffs);
    }

    // ---------------- 2D -> power-basis conversion ----------------
    // Expand T_p(x) and T_q(y) to power basis and accumulate tensor products
    public double[,] ToPowerBasis2D()
    {
        var n = DegreeT;
        var m = DegreeS;
        // Expand T_p to power polynomials up to degree n and T_q up to m
        var TpowT = ExpandTAllToPower(n);
        var TpowS = ExpandTAllToPower(m);

        var power = new double[n + 1, m + 1]; // power[i,j] for x^i y^j

        for (var p = 0; p <= n; p++)
        {
            var polyP = TpowT[p]; // length p+1
            for (var q = 0; q <= m; q++)
            {
                var polyQ = TpowS[q]; // length q+1
                var a_pq = Coeffs[p, q];
                for (var i = 0; i < polyP.Length; i++)
                {
                    for (var j = 0; j < polyQ.Length; j++)
                    {
                        power[i, j] += a_pq * polyP[i] * polyQ[j];
                    }
                }
            }
        }
        return power;
    }

    // helper to expand T_k to power coefficients for k=0..maxK
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

    // ---------------- Power -> Chebyshev 2D conversion ----------------
    // Given power basis coefficients power[i,j] for degrees n,m produce Chebyshev coefficients by
    // sampling the power polynomial at Chebyshev nodes and applying 2D DCT-II via FFT as above.
    public static ChebyshevPolynomial2D FromPowerBasis2D(double[,] power)
    {
        if (power == null) throw new ArgumentNullException(nameof(power));
        var n = power.GetLength(0) - 1;
        var m = power.GetLength(1) - 1;
        var N = n + 1;
        var M = m + 1;

        var tNodes = new double[N];
        var sNodes = new double[M];
        for (var j = 0; j < N; j++) tNodes[j] = Math.Cos(Math.PI * (j + 0.5) / N);
        for (var k = 0; k < M; k++) sNodes[k] = Math.Cos(Math.PI * (k + 0.5) / M);

        var samples = new double[N, M];
        for (var j = 0; j < N; j++)
        {
            for (var k = 0; k < M; k++)
            {
                var x = tNodes[j];
                var y = sNodes[k];
                // evaluate power polynomial at (x,y)
                var v = 0.0;
                for (var ii = n; ii >= 0; ii--)
                {
                    var rowAcc = 0.0;
                    for (var jj = m; jj >= 0; jj--) rowAcc = rowAcc * y + power[ii, jj];
                    v = v * x + rowAcc;
                }
                samples[j, k] = v;
            }
        }

        // Now apply separable DCT-II on samples as in Fit
        // Step 1: rows (s-direction)
        var temp = new double[N, M];
        var rowBuffer = new double[M];
        for (var j = 0; j < N; j++)
        {
            for (var k = 0; k < M; k++) rowBuffer[k] = samples[j, k];
            var craw = Dct2ViaFft_Single(rowBuffer);
            for (var q = 0; q < M; q++) temp[j, q] = craw[q];
        }

        // Step 2: columns (t-direction)
        var S = new double[N, M];
        var colBuffer = new double[N];
        for (var q = 0; q < M; q++)
        {
            for (var j = 0; j < N; j++) colBuffer[j] = temp[j, q];
            var craw = Dct2ViaFft_Single(colBuffer);
            for (var p = 0; p < N; p++) S[p, q] = craw[p];
        }

        var coeffs = new double[N, M];
        var scale = (2.0 / N) * (2.0 / M);
        for (var p = 0; p < N; p++)
            for (var q = 0; q < M; q++)
            {
                var a = scale * S[p, q];
                if (p == 0) a *= 0.5;
                if (q == 0) a *= 0.5;
                coeffs[p, q] = a;
            }

        return new ChebyshevPolynomial2D(n, m, coeffs);
    }

    // ---------------- Conversions T <-> U via power-basis intermediate ----------------

    public ChebyshevPolynomialU ToChebyshevU()
    {
        var power = ToPowerBasis2D();
        // Convert each row/column etc by handling 2D? Simpler approach: treat 2D as multivariate power and convert to tensor of U.
        // For now we provide per-axis conversions: convert coefficients a[p,q] in T->power->T->U by converting along each dimension
        // We'll convert by constructing function evaluator from Chebyshev 2D and fitting U basis in both dims (costly but robust).
        // Build function eval f(x,y) and fit with ChebyshevPolynomial2D.Fit approach but targeting U basis requires different transforms.
        throw new NotImplementedException("Direct T->U 2D conversion via U-basis not implemented. Use ToPowerBasis2D() and FromPowerBasis2D with appropriate basis transforms for each axis.");
    }

    // For 2D -> power basis we implemented ToPowerBasis2D()





    // ---------------- Reparametrization helper ----------------
    // Returns an evaluator for F mapped from (u,v) in [a,b]x[c,d] to x,y in [-1,1]^2: x = (2u-(b+a))/(b-a)
    public Func<double, double, double> ReparametrizedEvaluator(double a, double b, double c, double d)
    {
        return (u, v) =>
        {
            var x = (2.0 * u - (b + a)) / (b - a);
            var y = (2.0 * v - (d + c)) / (d - c);
            return EvaluateAt(x, y);
        };
    }

    // ---------------- Utilities ----------------

    // Precompute cosines for DCT: returns array cos[k,j] = cos(pi*k*(j+0.5)/N)
    private static double[,] PrecomputeCosines(int N)
    {
        var cos = new double[N, N];
        for (var k = 0; k < N; k++)
        {
            for (var j = 0; j < N; j++)
            {
                cos[k, j] = Math.Cos(Math.PI * k * (j + 0.5) / N);
            }
        }
        return cos;
    }

    // Optionally, convert this Chebyshev 2D representation back to power basis if needed (tensor-product expansion)
    // (Left as an exercise — can be implemented by expanding each T_p and T_q to monomials and accumulating.)

    /// <summary>
    /// Convert this Chebyshev 2D tensor coefficients from T_t × T_s basis to T_t × U_s basis (convert only s-axis).
    /// Returns newCoeffs[p, q] representing coefficients for T_p(t) * U_q(s).
    /// </summary>
    public double[,] ConvertTtoU_AxisS()
    {
        var n = DegreeT;
        var m = DegreeS;

        var outCoeffs = new double[n + 1, m + 1];

        for (var p = 0; p <= n; p++)
        {
            // extract row coefficients in s: a_{p,q} for q=0..m (these are T_q(s) coefficients)
            var row = new double[m + 1];
            for (var q = 0; q <= m; q++) row[q] = Coeffs[p, q];

            // Convert 1D Chebyshev T (row) -> U (1D) using 1D converter:
            var tRow = new ChebyshevPolynomialT(m, row);
            var uRow = tRow.ToChebyshevU(); // ChebyshevPolynomialU with degree m

            // write back as T_p(t) * U_q(s)
            for (var q = 0; q <= m; q++) outCoeffs[p, q] = uRow.Coeffs[q];
        }

        return outCoeffs;
    }

    /// <summary>
    /// Convert this Chebyshev 2D tensor coefficients from T_t × T_s basis to U_t × T_s basis (convert only t-axis).
    /// Returns newCoeffs[p, q] representing coefficients for U_p(t) * T_q(s).
    /// </summary>
    public double[,] ConvertTtoU_AxisT()
    {
        var n = DegreeT;
        var m = DegreeS;

        var outCoeffs = new double[n + 1, m + 1];

        // For each q (column) convert the column (over p) from T_p(t) -> U_p(t)
        for (var q = 0; q <= m; q++)
        {
            var col = new double[n + 1];
            for (var p = 0; p <= n; p++) col[p] = Coeffs[p, q];

            var tCol = new ChebyshevPolynomialT(n, col);
            var uCol = tCol.ToChebyshevU();

            for (var p = 0; p <= n; p++) outCoeffs[p, q] = uCol.Coeffs[p];
        }

        return outCoeffs;
    }

    /// <summary>
    /// Convert this Chebyshev 2D from T_t × T_s basis to U_t × U_s basis (convert both axes).
    /// Returns coeffsUU[p,q] corresponding to U_p(t) * U_q(s).
    /// Implementation: first convert each row T->U in s, then convert each column T->U in t.
    /// </summary>
    public double[,] ConvertTtoU_BothAxes()
    {
        // step 1: convert rows (s axis) T->U
        var step1 = ConvertTtoU_AxisS(); // T_t × U_s coefficients (still T in t)
        int n = DegreeT, m = DegreeS;

        // step 2: treat each column q of step1 (which is T_p(t) coefficients) and convert T->U
        var outCoeffs = new double[n + 1, m + 1];
        for (var q = 0; q <= m; q++)
        {
            var col = new double[n + 1];
            for (var p = 0; p <= n; p++) col[p] = step1[p, q];

            var tCol = new ChebyshevPolynomialT(n, col);
            var uCol = tCol.ToChebyshevU();

            for (var p = 0; p <= n; p++) outCoeffs[p, q] = uCol.Coeffs[p];
        }

        return outCoeffs;
    }

    /// <summary>
    /// Convert from U_t × U_s basis to T_t × T_s basis (inverse). Axis-wise using 1D U->T conversions:
    /// First convert columns (t axis) U->T, then rows (s axis) U->T.
    /// </summary>
    public double[,] ConvertUtoT_BothAxes(double[,] coeffsUU)
    {
        if (coeffsUU == null) throw new ArgumentNullException(nameof(coeffsUU));
        var n = DegreeT;
        var m = DegreeS;
        if (coeffsUU.GetLength(0) != n + 1 || coeffsUU.GetLength(1) != m + 1)
            throw new ArgumentException("coeffs dimension mismatch");

        // step 1: convert columns (t axis) U->T
        var step1 = new double[n + 1, m + 1];
        for (var q = 0; q <= m; q++)
        {
            var colU = new double[n + 1];
            for (var p = 0; p <= n; p++) colU[p] = coeffsUU[p, q];

            var uColPoly = new ChebyshevPolynomialU(n, colU);
            var tColPoly = uColPoly.ToChebyshevT();

            for (var p = 0; p <= n; p++) step1[p, q] = tColPoly.Coeffs[p];
        }

        // step 2: convert rows (s axis) U->T
        var outCoeffs = new double[n + 1, m + 1];
        for (var p = 0; p <= n; p++)
        {
            var rowU = new double[m + 1];
            for (var q = 0; q <= m; q++) rowU[q] = coeffsUU[p, q];

            var uRowPoly = new ChebyshevPolynomialU(m, rowU);
            var tRowPoly = uRowPoly.ToChebyshevT();

            for (var q = 0; q <= m; q++) outCoeffs[p, q] = tRowPoly.Coeffs[q];
        }

        return outCoeffs;
    }

}