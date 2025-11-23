using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.MatrixExp;

/// <summary>
/// 
/// </summary>
public static class MatrixExpTaylor
{
    /// <summary>
    /// Computes exp(matrixA) using Taylor series only.
    /// Suitable for general matrices with small Frobenius norm.
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="tolerance"></param>
    /// <returns></returns>
    public static Matrix<double> ExpViaTaylor(this Matrix<double> matrix, double tolerance)
    {
        var n = matrix.Size();
        var result = Matrix<double>.Build.DenseIdentity(n); // Start with I
        var term = Matrix<double>.Build.DenseIdentity(n);   // Will be matrixA^k / k!
        var k = 0;

        do
        {
            k++;
            term = term * matrix / k;    // term = matrixA^k / k!
            result += term;              // accumulate
        }
        while (term.FrobeniusNorm() > tolerance && k < 100);

        return result;
    }

    /// <summary>
    /// Computes exp(matrixA) using Taylor series with scaling and squaring.
    /// Suitable for general matrices, especially skew-symmetric.
    /// </summary>
    public static Matrix<double> ExpViaTaylorScaled(this Matrix<double> matrix, double tolerance = Float64Utils.DefaultTolerance, int maxSquaringSteps = 64)
    {
        //var n = matrix.Size();

        // Choose scaling factor s so that ||matrixA / 2^s|| ~ 1 or less
        var s = ComputeScalingFactor(matrix, out var _);

        // Scale matrixA
        var matrixScaled = matrix / (1 << s);

        // Compute exp(matrixScaled) using Taylor series
        var matrixScaledExp = matrixScaled.ExpViaTaylor(tolerance);

        // Square s times: exp(matrixA) = (exp(matrixA/2^s))^(2^s)
        var result = matrixScaledExp;
        for (var i = 0; i < s; i++)
        {
            result *= result;
        }

        return result;
    }

    private static int ComputeScalingFactor(Matrix<double> matrix, out double norm, int maxSquaringSteps = 64)
    {
        norm = matrix.FrobeniusNorm(); // Frobenius norm
        if (norm < 1e-15)
        {
            norm = 0;
            return 0;
        }

        // Goal: norm / 2^s ≈ 1 → s ≈ log2(norm)
        // But we want it small enough for fast Taylor convergence
        // Empirical: aim for ||matrixA|| < 2 for ~6–8 terms
        var s = 0;
        const double target = 2.0;
        while (s < maxSquaringSteps && norm / (1 << s) > target)
            s++;

        return s;
    }

}