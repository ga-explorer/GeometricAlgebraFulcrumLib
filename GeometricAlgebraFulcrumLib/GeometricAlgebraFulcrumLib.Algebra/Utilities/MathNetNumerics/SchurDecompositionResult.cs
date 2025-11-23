using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.MathNetNumerics;

/// <summary>
/// Result of Schur decomposition containing Q and T matrices.
/// A = Q * T * Q^T where Q is orthogonal and T is upper quasi-triangular.
/// </summary>
public sealed class SchurDecompositionResult
{
    /// <summary>
    /// Orthogonal matrix Q (Q^T * Q = I)
    /// </summary>
    public Matrix<double> Q { get; }

    /// <summary>
    /// Quasi-triangular Schur form T
    /// </summary>
    public Matrix<double> T { get; }

    /// <summary>
    /// Indicates if T is strictly upper triangular
    /// </summary>
    public bool IsUpperTriangular { get; }


    internal SchurDecompositionResult(Matrix<double> q, Matrix<double> t, bool isUpperTriangular)
    {
        Q = q ?? throw new ArgumentNullException(nameof(q));
        T = t ?? throw new ArgumentNullException(nameof(t));
        IsUpperTriangular = isUpperTriangular;
    }


    /// <summary>
    /// Reconstructs the original matrix: A = Q * T * Q^T
    /// </summary>
    /// <returns>The reconstructed matrix</returns>
    public Matrix<double> Reconstruct()
    {
        return Q * T * Q.Transpose();
    }

    /// <summary>
    /// Gets the eigenvalues from the Schur form
    /// </summary>
    /// <returns>Array of eigenvalues</returns>
    public Complex[] GetEigenvalues()
    {
        var eigenvalues = new Complex[T.RowCount];
        var i = 0;

        while (i < T.RowCount)
        {
            if (i == T.RowCount - 1 || Math.Abs(T[i + 1, i]) < 1e-14)
            {
                // 1x1 block - real eigenvalue
                eigenvalues[i] = new Complex(T[i, i], 0);
                i++;
            }
            else
            {
                // 2x2 block - complex conjugate pair
                double a = T[i, i], b = T[i, i + 1];
                double c = T[i + 1, i], d = T[i + 1, i + 1];

                var trace = a + d;
                var det = a * d - b * c;
                var discriminant = trace * trace - 4 * det;

                if (discriminant >= 0)
                {
                    var sqrtDisc = Math.Sqrt(discriminant);
                    eigenvalues[i] = new Complex((trace + sqrtDisc) / 2, 0);
                    eigenvalues[i + 1] = new Complex((trace - sqrtDisc) / 2, 0);
                }
                else
                {
                    var imag = Math.Sqrt(-discriminant) / 2;
                    eigenvalues[i] = new Complex(trace / 2, imag);
                    eigenvalues[i + 1] = new Complex(trace / 2, -imag);
                }

                i += 2;
            }
        }

        return eigenvalues;
    }
}