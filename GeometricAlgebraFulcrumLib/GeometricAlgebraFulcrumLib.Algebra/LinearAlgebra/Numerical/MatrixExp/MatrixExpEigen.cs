using System.Numerics;
using MathNet.Numerics.LinearAlgebra;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.MatrixExp;

/// <summary>
/// Matrix exponential utilities class
/// </summary>
public static class MatrixExpEigen
{
    
    
    /// <summary>
    /// Compute the matrix exponential via eigenvalue decomposition.
    /// This is most effective for symmetric and small matrices.
    /// </summary>
    public static (bool success, Matrix<double> matrixExp, string details) ExpViaEigendecomposition(this Matrix<double> matrix, double tolerance)
    {
        var n = matrix.Size();
        var details = new StringBuilder();

        try
        {
            // Convert real matrix to complex and compute eigendecomposition
            var complexMatrix = matrix.ToComplex();
            var eig = complexMatrix.Evd();
            
            // Validate EVD results
            if (eig.EigenValues == null || eig.EigenVectors == null)
            {
                details.AppendLine("Eigenvalue decomposition failed: null results");
                return (false, Matrix<double>.Build.Dense(n, n), details.ToString());
            }
            
            var eigenValues = eig.EigenValues;
            var eigenVectors = eig.EigenVectors;
            
            // Validate dimensions
            if (eigenValues.Count != n || eigenVectors.RowCount != n || eigenVectors.ColumnCount != n)
            {
                details.AppendLine($"Eigenvalue decomposition dimension mismatch: expected {n}, got eigenvalues: {eigenValues.Count}, eigenvectors: {eigenVectors.RowCount}x{eigenVectors.ColumnCount}");
                return (false, Matrix<double>.Build.Dense(n, n), details.ToString());
            }

            // Build exp(D): apply exp to each eigenvalue
            var expD = MathNet.Numerics.LinearAlgebra.Vector<Complex>.Build.Dense(n);
            for (var i = 0; i < n; i++)
            {
                expD[i] = Complex.Exp(eigenValues[i]); // e^(λi)
                
                // Check for numerical issues
                if (double.IsNaN(expD[i].Real) || double.IsNaN(expD[i].Imaginary) ||
                    double.IsInfinity(expD[i].Real) || double.IsInfinity(expD[i].Imaginary))
                {
                    details.AppendLine($"Numerical issue: exp(eigenvalue[{i}]) = {expD[i]} for eigenvalue {eigenValues[i]}");
                    return (false, Matrix<double>.Build.Dense(n, n), details.ToString());
                }
            }

            // Diagonal matrix from expD
            var expDm = Matrix<Complex>.Build.DiagonalOfDiagonalVector(expD);

            // Compute exp(A) = V * exp(D) * V^(-1)
            var expAComplex = eigenVectors * expDm * eigenVectors.ConjugateTranspose();

            // Extract real part and validate
            var success = true;
            var matrixExp = Matrix<double>.Build.Dense(n, n);
            
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    var z = expAComplex[i, j];
                    
                    // Check for numerical issues
                    if (double.IsNaN(z.Real) || double.IsNaN(z.Imaginary) ||
                        double.IsInfinity(z.Real) || double.IsInfinity(z.Imaginary))
                    {
                        details.AppendLine($"Numerical issue at ({i},{j}): {z}");
                        return (false, Matrix<double>.Build.Dense(n, n), details.ToString());
                    }
                    
                    if (Math.Abs(z.Imaginary) > tolerance)
                    {
                        success = false;
                        details.AppendLine($"Warning: Imaginary part {z.Imaginary} at ({i},{j}) exceeds tolerance {tolerance}");
                    }
                    
                    matrixExp[i, j] = z.Real;
                }
            }

            if (success)
            {
                details.AppendLine($"Eigenvalue decomposition successful for {n}×{n} matrix");
            }

            return (success, matrixExp, details.ToString());
        }
        catch (Exception ex)
        {
            details.AppendLine($"Eigenvalue decomposition failed with exception: {ex.Message}");
            return (false, Matrix<double>.Build.Dense(n, n), details.ToString());
        }
    }

}