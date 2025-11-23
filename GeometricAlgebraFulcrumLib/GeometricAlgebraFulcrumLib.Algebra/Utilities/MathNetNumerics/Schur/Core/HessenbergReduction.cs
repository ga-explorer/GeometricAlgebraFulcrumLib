using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.MathNetNumerics.Schur.Utils;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.MathNetNumerics.Schur.Core
{
    /// <summary>
    /// Hessenberg reduction utilities for Schur decomposition.
    /// Translation of Julia's hessenberg.jl file.
    /// </summary>
    public static class HessenbergReduction
    {
        /// <summary>
        /// Reduce a complex matrix to Hessenberg form H = Q^H * matrixA * Q.
        /// Returns (H, Q) where H is in Hessenberg form and Q is unitary.
        /// </summary>
        public static (Matrix<Complex> H, Matrix<Complex>? Q) Reduce(Matrix<Complex> matrixA, bool computeQ = true)
        {
            if (matrixA == null) throw new ArgumentNullException(nameof(matrixA));
            if (matrixA.RowCount != matrixA.ColumnCount)
                throw new ArgumentException("Matrix must be square", nameof(matrixA));

            var n = matrixA.RowCount;
            var H = matrixA.Clone();
            Matrix<Complex>? Q = computeQ ? MathNet.Numerics.LinearAlgebra.Complex.DenseMatrix.CreateIdentity(n) : null;
            
            var tau = MathNet.Numerics.LinearAlgebra.Complex.DenseVector.Create(n - 1, Complex.Zero);

            // Perform Hessenberg reduction using Householder reflectors
            for (var i = 0; i < n - 1; i++)
            {
                // Extract column vector for Householder reflector
                var xi = H.SubMatrix(i + 1, n - i - 1, i, 1).Column(0);
                
                // Compute Householder reflector
                var tau_i = HouseholderReflector.ComputeReflector(xi);
                tau[i] = tau_i;

                if (tau_i != Complex.Zero)
                {
                    // Store the essential part of the Householder vector
                    for (var j = 1; j < xi.Count; j++)
                    {
                        H[i + 1 + j, i] = xi[j];
                    }
                    H[i + 1, i] = xi[0]; // This will be the beta from the reflector

                    // Apply H^H to the right part of the matrix: H := H * P^H
                    // where P = I - tau * v * v^H
                    ApplyHouseholderRight(H, xi, tau_i, i + 1, i + 1, n - 1, n - 1);

                    // Apply H to the left part of the matrix: H := P^H * H
                    ApplyHouseholderLeft(H, xi, tau_i, i + 1, 0, n - 1, n - 1);

                    // Update Q if requested: Q := Q * P
                    if (computeQ && Q != null)
                    {
                        ApplyHouseholderRight(Q, xi, tau_i, i + 1, 0, n - 1, n - 1);
                    }
                }
            }

            // Clean up: ensure proper Hessenberg form
            for (var j = 0; j < n - 2; j++)
            {
                for (var i = j + 2; i < n; i++)
                {
                    H[i, j] = Complex.Zero;
                }
            }

            return (H, Q);
        }

        /// <summary>
        /// Reduce a real matrix to Hessenberg form.
        /// </summary>
        public static (Matrix<double> H, Matrix<double>? Q) Reduce(Matrix<double> matrixA, bool computeQ = true)
        {
            if (matrixA == null) throw new ArgumentNullException(nameof(matrixA));
            if (matrixA.RowCount != matrixA.ColumnCount)
                throw new ArgumentException("Matrix must be square", nameof(matrixA));

            var n = matrixA.RowCount;
            var H = matrixA.Clone();
            Matrix<double>? Q = computeQ ? MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.CreateIdentity(n) : null;
            
            var tau = MathNet.Numerics.LinearAlgebra.Double.DenseVector.Create(n - 1, 0.0);

            // Perform Hessenberg reduction using Householder reflectors
            for (var i = 0; i < n - 1; i++)
            {
                // Extract column vector for Householder reflector
                var xi = H.SubMatrix(i + 1, n - i - 1, i, 1).Column(0);
                
                // Compute Householder reflector
                var tau_i = HouseholderReflector.ComputeReflector(xi);
                tau[i] = tau_i;

                if (tau_i != 0.0)
                {
                    // Store the essential part of the Householder vector
                    for (var j = 1; j < xi.Count; j++)
                    {
                        H[i + 1 + j, i] = xi[j];
                    }
                    H[i + 1, i] = xi[0]; // This will be the beta from the reflector

                    // Apply H^H to the right part of the matrix: H := H * P^H
                    ApplyHouseholderRight(H, xi, tau_i, i + 1, i + 1, n - 1, n - 1);

                    // Apply H to the left part of the matrix: H := P^H * H
                    ApplyHouseholderLeft(H, xi, tau_i, i + 1, 0, n - 1, n - 1);

                    // Update Q if requested: Q := Q * P
                    if (computeQ && Q != null)
                    {
                        ApplyHouseholderRight(Q, xi, tau_i, i + 1, 0, n - 1, n - 1);
                    }
                }
            }

            // Clean up: ensure proper Hessenberg form
            for (var j = 0; j < n - 2; j++)
            {
                for (var i = j + 2; i < n; i++)
                {
                    H[i, j] = 0.0;
                }
            }

            return (H, Q);
        }

        /// <summary>
        /// Reduce a Hermitian matrix to tridiagonal form.
        /// This is a special case optimization for Hermitian matrices.
        /// </summary>
        public static (Matrix<Complex> T, Matrix<Complex> Q, MathNet.Numerics.LinearAlgebra.Vector<double> diagonal, MathNet.Numerics.LinearAlgebra.Vector<double> subdiagonal) 
               ReduceHermitian(Matrix<Complex> matrixA, bool computeQ = true)
        {
            if (matrixA == null) throw new ArgumentNullException(nameof(matrixA));
            if (matrixA.RowCount != matrixA.ColumnCount)
                throw new ArgumentException("Matrix must be square", nameof(matrixA));

            var n = matrixA.RowCount;
            var H = matrixA.Clone();
            var Q = computeQ ? MathNet.Numerics.LinearAlgebra.Complex.DenseMatrix.CreateIdentity(n) : MathNet.Numerics.LinearAlgebra.Complex.DenseMatrix.Create(0, 0, Complex.Zero);
            
            var tau = MathNet.Numerics.LinearAlgebra.Complex.DenseVector.Create(n - 1, Complex.Zero);
            var d = MathNet.Numerics.LinearAlgebra.Double.DenseVector.Create(n, 0.0);
            var e = MathNet.Numerics.LinearAlgebra.Double.DenseVector.Create(n - 1, 0.0);

            // Ensure diagonal is real for Hermitian matrix
            for (var i = 0; i < n; i++)
            {
                H[i, i] = new Complex(H[i, i].Real, 0.0);
            }

            for (var i = 0; i < n - 1; i++)
            {
                // Extract subdiagonal column for Householder reflector
                var xi = MathNet.Numerics.LinearAlgebra.Complex.DenseVector.Create(n - i - 1, Complex.Zero);
                for (var k = 0; k < xi.Count; k++)
                {
                    xi[k] = H[i + 1 + k, i];
                }

                var tau_i = HouseholderReflector.ComputeReflector(xi);
                e[i] = xi[0].Real;
                
                if (tau_i != Complex.Zero)
                {
                    xi[0] = Complex.One;
                    
                    // For Hermitian reduction, we need to apply a rank-2 update
                    // H := H - tau * xi * w^H - w * xi^H where w = tau * H * xi
                    var w = MathNet.Numerics.LinearAlgebra.Complex.DenseVector.Create(xi.Count, Complex.Zero);
                    
                    // w = H[i+1:n, i+1:n] * xi (only lower triangular part)
                    for (var j = 0; j < xi.Count; j++)
                    {
                        for (var k = j; k < xi.Count; k++)
                        {
                            w[j] += H[i + 1 + k, i + 1 + j] * xi[k];
                            if (k != j)
                            {
                                w[k] += Complex.Conjugate(H[i + 1 + k, i + 1 + j]) * xi[j];
                            }
                        }
                    }
                    
                    for (var j = 0; j < w.Count; j++)
                    {
                        w[j] *= tau_i;
                    }

                    // Compute alpha = -(tau/2) * (w^H * xi)
                    var alpha = Complex.Zero;
                    for (var j = 0; j < xi.Count; j++)
                    {
                        alpha += Complex.Conjugate(w[j]) * xi[j];
                    }
                    alpha *= -tau_i / 2.0;

                    // w := w + alpha * xi
                    for (var j = 0; j < w.Count; j++)
                    {
                        w[j] += alpha * xi[j];
                    }

                    // Apply rank-2 update: H := H - xi * w^H - w * xi^H
                    for (var j = 0; j < xi.Count; j++)
                    {
                        for (var k = j; k < xi.Count; k++)
                        {
                            var update = xi[j] * Complex.Conjugate(w[k]) + w[j] * Complex.Conjugate(xi[k]);
                            H[i + 1 + j, i + 1 + k] -= update;
                            if (j != k)
                            {
                                H[i + 1 + k, i + 1 + j] = Complex.Conjugate(H[i + 1 + j, i + 1 + k]);
                            }
                        }
                    }

                    // Ensure diagonal remains real
                    for (var j = 0; j < xi.Count; j++)
                    {
                        H[i + 1 + j, i + 1 + j] = new Complex(H[i + 1 + j, i + 1 + j].Real, 0.0);
                    }
                }

                H[i + 1, i] = new Complex(e[i], 0.0);
                d[i] = H[i, i].Real;
                tau[i] = tau_i;
            }

            d[n - 1] = H[n - 1, n - 1].Real;

            return (H, Q, d, e);
        }

        #region Helper Methods

        private static void ApplyHouseholderLeft(Matrix<Complex> matrixA, MathNet.Numerics.LinearAlgebra.Vector<Complex> v, Complex tau,
                                               int vStart, int colStart, int rowEnd, int colEnd)
        {
            if (tau == Complex.Zero) return;

            for (var j = colStart; j <= colEnd; j++)
            {
                var sum = Complex.Zero;
                
                for (var i = 0; i < v.Count && (vStart + i) <= rowEnd; i++)
                {
                    if (i == 0)
                        sum += matrixA[vStart + i, j];
                    else
                        sum += Complex.Conjugate(v[i]) * matrixA[vStart + i, j];
                }

                sum *= tau;

                for (var i = 0; i < v.Count && (vStart + i) <= rowEnd; i++)
                {
                    if (i == 0)
                        matrixA[vStart + i, j] -= sum;
                    else
                        matrixA[vStart + i, j] -= sum * v[i];
                }
            }
        }

        private static void ApplyHouseholderRight(Matrix<Complex> matrixA, MathNet.Numerics.LinearAlgebra.Vector<Complex> v, Complex tau,
                                                int vStart, int rowStart, int rowEnd, int colEnd)
        {
            if (tau == Complex.Zero) return;

            for (var i = rowStart; i <= rowEnd; i++)
            {
                var sum = Complex.Zero;
                
                for (var j = 0; j < v.Count && (vStart + j) <= colEnd; j++)
                {
                    if (j == 0)
                        sum += matrixA[i, vStart + j];
                    else
                        sum += matrixA[i, vStart + j] * v[j];
                }

                sum *= Complex.Conjugate(tau);

                for (var j = 0; j < v.Count && (vStart + j) <= colEnd; j++)
                {
                    if (j == 0)
                        matrixA[i, vStart + j] -= sum;
                    else
                        matrixA[i, vStart + j] -= sum * Complex.Conjugate(v[j]);
                }
            }
        }

        private static void ApplyHouseholderLeft(Matrix<double> matrixA, MathNet.Numerics.LinearAlgebra.Vector<double> v, double tau,
                                               int vStart, int colStart, int rowEnd, int colEnd)
        {
            if (tau == 0.0) return;

            for (var j = colStart; j <= colEnd; j++)
            {
                var sum = 0.0;
                
                for (var i = 0; i < v.Count && (vStart + i) <= rowEnd; i++)
                {
                    if (i == 0)
                        sum += matrixA[vStart + i, j];
                    else
                        sum += v[i] * matrixA[vStart + i, j];
                }

                sum *= tau;

                for (var i = 0; i < v.Count && (vStart + i) <= rowEnd; i++)
                {
                    if (i == 0)
                        matrixA[vStart + i, j] -= sum;
                    else
                        matrixA[vStart + i, j] -= sum * v[i];
                }
            }
        }

        private static void ApplyHouseholderRight(Matrix<double> matrixA, MathNet.Numerics.LinearAlgebra.Vector<double> v, double tau,
                                                int vStart, int rowStart, int rowEnd, int colEnd)
        {
            if (tau == 0.0) return;

            for (var i = rowStart; i <= rowEnd; i++)
            {
                var sum = 0.0;
                
                for (var j = 0; j < v.Count && (vStart + j) <= colEnd; j++)
                {
                    if (j == 0)
                        sum += matrixA[i, vStart + j];
                    else
                        sum += matrixA[i, vStart + j] * v[j];
                }

                sum *= tau;

                for (var j = 0; j < v.Count && (vStart + j) <= colEnd; j++)
                {
                    if (j == 0)
                        matrixA[i, vStart + j] -= sum;
                    else
                        matrixA[i, vStart + j] -= sum * v[j];
                }
            }
        }

        #endregion
    }
}