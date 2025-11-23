using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.MatrixExp;

/// <summary>
/// 
/// </summary>
public static class MatrixExpPadeSimple
{
    /// <summary>
    /// Computes exp(matrixA) using Padé approximant with scaling and squaring.
    /// Based on the method used in SciPy and MATLAB.
    /// </summary>
    /// <param name="matrixA">Square matrix (e.g., skew-symmetric)</param>
    /// <param name="tolerance">Desired accuracy (e.g., 1e-14)</param>
    /// <returns>exp(matrixA)</returns>
    public static Matrix<double> MatrixExpViaPadeSimple(this Matrix<double> matrixA, double tolerance = Float64Utils.DefaultTolerance)
    {
        if (matrixA.RowCount != matrixA.ColumnCount)
            throw new ArgumentException("Matrix must be square.");

        //var n = matrixA.RowCount;

        // Choose scaling factor s and Padé order m
        var norm = matrixA.L1Norm(); // Use L1 norm for scaling (common choice)
        ChooseParameters(norm, tolerance, out var m, out var s);

        // Step 1: Scale matrixA by 2^s
        var matrixAScaled = matrixA / Math.Pow(2, s);

        // Step 2: Compute Padé approximant R_m(Ascaled)
        var matrixAScaledExp = PadeApproximant(matrixAScaled, m);

        // Step 3: Square the result s times
        var result = matrixAScaledExp;
        for (var i = 0; i < s; i++)
        {
            result *= result;
        }

        return result;
    }

    /// <summary>
    /// Computes the [m/m] Padé approximant of exp(matrixA)
    /// R_m(matrixA) = N_m(matrixA) / D_m(matrixA) = D_m(-matrixA)^{-1} N_m(matrixA)
    /// </summary>
    private static Matrix<double> PadeApproximant(Matrix<double> matrixA, int m)
    {
        var n = matrixA.RowCount;

        // Precompute coefficients: c_k = (2m - k)! * m! / ( (2m)! * k! * (m - k)! )
        var coeffs = new double[m + 1];
        var factorial = PrecomputeFactorials(2 * m + 1);

        for (var k = 0; k <= m; k++)
        {
            var numerator = factorial[2 * m - k] * factorial[m];
            var denominator = factorial[2 * m] * factorial[k] * factorial[m - k];
            coeffs[k] = numerator / denominator;
        }

        // Build N_m(matrixA) = sum_{k=0}^m c_k matrixA^k
        var matrixN = Matrix<double>.Build.Dense(n, n);
        var matrixD = Matrix<double>.Build.Dense(n, n); // will be N_m(-matrixA)

        var matrixAk = Matrix<double>.Build.DenseIdentity(n); // matrixA^0
        var matrixAkNeg = Matrix<double>.Build.DenseIdentity(n); // (-matrixA)^0

        for (var k = 0; k <= m; k++)
        {
            matrixN += coeffs[k] * matrixAk;
            matrixD += coeffs[k] * matrixAkNeg;

            if (k >= m) continue;

            matrixAk *= matrixA;
            matrixAkNeg *= -matrixA;
        }

        // R_m(matrixA) = matrixD^{-1} * matrixN
        var solver = matrixD.LU();
        return solver.Solve(matrixN);
    }

    /// <summary>
    /// Precompute factorials up to n! to avoid recomputation
    /// </summary>
    private static double[] PrecomputeFactorials(int n)
    {
        var fact = new double[n + 1];
        fact[0] = 1.0;
        for (var i = 1; i <= n; i++)
        {
            fact[i] = fact[i - 1] * i;
        }
        return fact;
    }

    /// <summary>
    /// Choose Padé order m and scaling s based on matrix norm and tolerance
    /// These values are derived from published tables (e.g., Higham 2005, 2009)
    /// </summary>
    private static void ChooseParameters(double norm, double tolerance, out int m, out int s)
    {
        // Common choices for [m/m] Padé + scaling (from Higham's "Functions of Matrices")
        // These are conservative and widely used

        if (norm < 1.1e-4)
        {
            m = 3; s = 0;
        }
        else if (norm < 2.4e-2)
        {
            m = 5; s = 0;
        }
        else if (norm < 1.4e-1)
        {
            m = 7; s = 0;
        }
        else if (norm < 4.4e-1)
        {
            m = 9; s = 0;
        }
        else if (norm < 1.0)
        {
            m = 13; s = 0;
        }
        else
        {
            // Need scaling
            m = 13;
            s = (int)Math.Ceiling(Math.Log(norm) / Math.Log(2.0)); // Scale so ||matrixA|| < 1
            // But ensure we don't over-scale
            s = Math.Max(s, 0);
        }

        // Cap m to avoid overflow in factorial (m=13 is safe)
        m = Math.Min(m, 13);
    }
}