using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.MathNetNumerics.Schur.Exceptions;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.MathNetNumerics.Schur.Utils;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.MathNetNumerics.Schur.Core
{
    /// <summary>
    /// Represents a Schur decomposition matrixA = Q * T * Q^H where T is upper triangular (complex case)
    /// or quasi-upper triangular (real case), and Q is unitary/orthogonal.
    /// </summary>
    public class SchurDecomposition<T> where T : struct, IEquatable<T>, IFormattable
    {
        public Matrix<T> TriangularMatrix { get; }
        public Matrix<T> UnitaryMatrix { get; }
        public MathNet.Numerics.LinearAlgebra.Vector<Complex> Eigenvalues { get; }

        public SchurDecomposition(Matrix<T> triangularMatrix, Matrix<T> unitaryMatrix, MathNet.Numerics.LinearAlgebra.Vector<Complex> eigenvalues)
        {
            TriangularMatrix = triangularMatrix ?? throw new ArgumentNullException(nameof(triangularMatrix));
            UnitaryMatrix = unitaryMatrix ?? throw new ArgumentNullException(nameof(unitaryMatrix));
            Eigenvalues = eigenvalues ?? throw new ArgumentNullException(nameof(eigenvalues));
        }

        /// <summary>
        /// Compute the Schur decomposition of a complex matrix using the QR algorithm.
        /// </summary>
        public static SchurDecomposition<Complex> Compute(Matrix<Complex> matrixA, 
                                                          bool computeQ = true, 
                                                          bool balance = true,
                                                          int maxIterations = -1,
                                                          int maxInnerIterations = -1)
        {
            if (matrixA == null) throw new ArgumentNullException(nameof(matrixA));
            if (matrixA.RowCount != matrixA.ColumnCount)
                throw new ArgumentException("Matrix must be square", nameof(matrixA));

            var n = matrixA.RowCount;
            maxIterations = maxIterations < 0 ? 100 * n : maxIterations;
            maxInnerIterations = maxInnerIterations < 0 ? 30 * n : maxInnerIterations;

            // Make a copy to avoid modifying the input
            var ACopy = matrixA.Clone();

            // Optional balancing for numerical stability
            BalanceResult<Complex>? balanceResult = null;
            if (balance)
            {
                balanceResult = BalanceMatrix(ACopy);
            }

            // Optional scaling
            var (scaleA, cscale, anrm) = NumericalUtils.ScaleMatrix(ACopy);

            // Reduce to Hessenberg form
            var (H, QHess) = HessenbergReduction.Reduce(ACopy, computeQ);

            // Apply QR algorithm to Hessenberg matrix
            var schur = QRAlgorithmComplex(H, computeQ ? QHess : null, maxIterations, maxInnerIterations);

            // Undo scaling if applied
            if (scaleA)
            {
                NumericalUtils.SafeScale(schur.TriangularMatrix, cscale, anrm);
                for (var i = 0; i < schur.Eigenvalues.Count; i++)
                {
                    schur.Eigenvalues[i] = schur.Eigenvalues[i] * (anrm / cscale);
                }
            }

            // Apply inverse balancing if balancing was used
            if (balanceResult.HasValue && computeQ)
            {
                ApplyInverseBalancing(schur.UnitaryMatrix, balanceResult.Value);
            }

            return schur;
        }

        /// <summary>
        /// Compute the Schur decomposition of a real matrix.
        /// </summary>
        public static SchurDecomposition<double> Compute(Matrix<double> matrixA,
                                                        bool computeQ = true,
                                                        bool balance = true,
                                                        int maxIterations = -1,
                                                        int maxInnerIterations = -1)
        {
            if (matrixA == null) throw new ArgumentNullException(nameof(matrixA));
            if (matrixA.RowCount != matrixA.ColumnCount)
                throw new ArgumentException("Matrix must be square", nameof(matrixA));

            var n = matrixA.RowCount;
            maxIterations = maxIterations < 0 ? 100 * n : maxIterations;
            maxInnerIterations = maxInnerIterations < 0 ? 30 * n : maxInnerIterations;

            // Make a copy to avoid modifying the input
            var ACopy = matrixA.Clone();

            // Optional balancing
            BalanceResult<double>? balanceResult = null;
            if (balance)
            {
                balanceResult = BalanceMatrix(ACopy);
            }

            // Optional scaling
            var (scaleA, cscale, anrm) = NumericalUtils.ScaleMatrix(ACopy);

            // Reduce to Hessenberg form
            var (H, QHess) = HessenbergReduction.Reduce(ACopy, computeQ);

            // Apply QR algorithm to Hessenberg matrix
            var schur = QRAlgorithmReal(H, computeQ ? QHess : null, maxIterations, maxInnerIterations);

            // Undo scaling if applied
            if (scaleA)
            {
                NumericalUtils.SafeScale(schur.TriangularMatrix, cscale, anrm);
                for (var i = 0; i < schur.Eigenvalues.Count; i++)
                {
                    schur.Eigenvalues[i] = schur.Eigenvalues[i] * (anrm / cscale);
                }
            }

            // Apply inverse balancing if balancing was used
            if (balanceResult.HasValue && computeQ)
            {
                ApplyInverseBalancing(schur.UnitaryMatrix, balanceResult.Value);
            }

            return schur;
        }

        /// <summary>
        /// QR algorithm for complex Hessenberg matrices using single-shift Francis iteration.
        /// Translation of Julia's gschur! function for complex matrices.
        /// </summary>
        private static SchurDecomposition<Complex> QRAlgorithmComplex(Matrix<Complex> H, Matrix<Complex>? Q,
                                                                     int maxIterations, int maxInnerIterations)
        {
            var n = H.RowCount;
            var ulp = Precision.DoublePrecision;
            var smallnum = NumericalUtils.SafeMin() * n / ulp;
            
            var eigenvalues = MathNet.Numerics.LinearAlgebra.Complex.DenseVector.Create(n, Complex.Zero);
            
            // Check that subdiagonal is real (requirement for algorithm)
            for (var j = 0; j < n - 1; j++)
            {
                if (H[j + 1, j].Imaginary != 0.0)
                {
                    throw new ArgumentException("Algorithm assumes real subdiagonal elements");
                }
            }

            // Zero out below subdiagonal
            for (var j = 0; j < n - 1; j++)
            {
                for (var i = j + 2; i < n; i++)
                {
                    H[i, j] = Complex.Zero;
                }
            }

            var istart = 0;
            var iend = n - 1;
            var iterationCount = 0;
            var v2 = MathNet.Numerics.LinearAlgebra.Complex.DenseVector.Create(2, Complex.Zero);

            while (iend >= 0)
            {
                istart = 0;
                
                for (var its = 0; its < maxInnerIterations; its++)
                {
                    iterationCount++;
                    if (iterationCount > maxIterations)
                    {
                        throw new UnconvergedException($"iteration limit {maxIterations} reached");
                    }

                    // Find deflation point
                    var found = false;
                    for (var k = iend - 1; k >= istart; k--)
                    {
                        if (IsNegligibleSubdiagonal(H, k, smallnum, ulp))
                        {
                            istart = k + 1;
                            found = true;
                            break;
                        }
                    }
                    
                    if (!found)
                    {
                        istart = 0;
                    }

                    if (istart > 0)
                    {
                        H[istart, istart - 1] = Complex.Zero;
                    }

                    // Check for convergence
                    if (istart >= iend)
                    {
                        eigenvalues[iend] = H[iend, iend];
                        iend--;
                        break;
                    }

                    // Compute shift
                    var shift = ComputeWilkinsonShift(H, istart, iend, its);

                    // Apply single-shift QR step
                    SingleShiftQR(H, Q, shift, istart, iend, v2);
                }
            }

            // Extract eigenvalues from diagonal
            for (var i = 0; i < n; i++)
            {
                eigenvalues[i] = H[i, i];
            }

            // Ensure upper triangular form
            for (var j = 0; j < n; j++)
            {
                for (var i = j + 1; i < n; i++)
                {
                    H[i, j] = Complex.Zero;
                }
            }

            return new SchurDecomposition<Complex>(H, Q ?? MathNet.Numerics.LinearAlgebra.Complex.DenseMatrix.CreateIdentity(n), eigenvalues);
        }

        /// <summary>
        /// QR algorithm for real Hessenberg matrices using Francis double-shift iteration.
        /// Translation of Julia's gschur! function for real matrices.
        /// </summary>
        private static SchurDecomposition<double> QRAlgorithmReal(Matrix<double> H, Matrix<double>? Q,
                                                                 int maxIterations, int maxInnerIterations)
        {
            var n = H.RowCount;
            var smallnum = double.Epsilon * n / Precision.DoublePrecision;
            
            var eigenvalues = MathNet.Numerics.LinearAlgebra.Complex.DenseVector.Create(n, Complex.Zero);
            var eigenvalueIndex = n - 1;

            // Zero out below subdiagonal
            for (var j = 0; j < n - 1; j++)
            {
                for (var i = j + 2; i < n; i++)
                {
                    H[i, j] = 0.0;
                }
            }

            var istart = 0;
            var iend = n - 1;
            var iterationCount = 0;
            var v3 = MathNet.Numerics.LinearAlgebra.Double.DenseVector.Create(3, 0.0);

            while (iend >= 0)
            {
                istart = 0;
                var qrIterations = 0;
                var deflate = false;

                while (true)
                {
                    iterationCount++;
                    if (iterationCount > maxIterations)
                    {
                        throw new UnconvergedException($"iteration limit {maxIterations} reached");
                    }

                    // Find deflation point
                    var split = false;
                    for (var k = iend; k > istart; k--)
                    {
                        if (IsNegligibleSubdiagonal(H, k - 1, smallnum))
                        {
                            split = true;
                            istart = k;
                            break;
                        }
                    }

                    if (!split)
                    {
                        istart = 0;
                    }

                    if (istart > 0)
                    {
                        H[istart, istart - 1] = 0.0;
                    }

                    if (istart >= iend - 1)
                    {
                        deflate = true;
                        break;
                    }

                    // Compute shifts for Francis double-shift
                    qrIterations++;
                    var (shift1, shift2) = ComputeFrancisShifts(H, istart, iend, qrIterations);

                    // Apply double-shift QR step
                    DoubleShiftQR(H, Q, shift1, shift2, istart, iend, v3);
                }

                if (deflate && istart >= iend)
                {
                    // 1x1 block converged
                    eigenvalues[eigenvalueIndex] = new Complex(H[iend, iend], 0.0);
                    eigenvalueIndex--;
                    iend = istart - 1;
                }
                else if (deflate && istart + 1 == iend)
                {
                    // 2x2 block converged
                    var (eig1, eig2) = Compute2x2Eigenvalues(H, iend - 1, iend);
                    eigenvalues[eigenvalueIndex] = eig2;
                    eigenvalues[eigenvalueIndex - 1] = eig1;
                    eigenvalueIndex -= 2;
                    iend = istart - 1;
                }
            }

            return new SchurDecomposition<double>(H, Q ?? MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.CreateIdentity(n), eigenvalues);
        }

        #region Helper Methods

        private static bool IsNegligibleSubdiagonal(Matrix<Complex> H, int k, double smallnum, double ulp)
        {
            var n = H.RowCount;
            if (k < 0 || k >= n - 1) return true;

            if (NumericalUtils.Abs1(H[k + 1, k]) <= smallnum)
            {
                return true;
            }

            // Ahues & Tisseur deflation criterion
            var tst = NumericalUtils.Abs1(H[k, k]) + NumericalUtils.Abs1(H[k + 1, k + 1]);
            if (tst == 0)
            {
                if (k - 1 >= 0) tst += Math.Abs(H[k, k - 1].Real);
                if (k + 2 < n) tst += Math.Abs(H[k + 2, k + 1].Real);
            }

            if (Math.Abs(H[k + 1, k].Real) <= ulp * tst)
            {
                var ab = Math.Max(NumericalUtils.Abs1(H[k + 1, k]), NumericalUtils.Abs1(H[k, k + 1]));
                var ba = Math.Min(NumericalUtils.Abs1(H[k + 1, k]), NumericalUtils.Abs1(H[k, k + 1]));
                var aa = Math.Max(NumericalUtils.Abs1(H[k + 1, k + 1]), 
                                   NumericalUtils.Abs1(H[k, k] - H[k + 1, k + 1]));
                var bb = Math.Min(NumericalUtils.Abs1(H[k + 1, k + 1]), 
                                   NumericalUtils.Abs1(H[k, k] - H[k + 1, k + 1]));
                var s = aa + ab;
                
                return ba * (ab / s) <= Math.Max(smallnum, ulp * (bb * (aa / s)));
            }

            return false;
        }

        private static bool IsNegligibleSubdiagonal(Matrix<double> H, int k, double smallnum)
        {
            var n = H.RowCount;
            if (k < 0 || k >= n - 1) return true;

            if (Math.Abs(H[k + 1, k]) < smallnum)
            {
                return true;
            }

            var Hkk = H[k, k];
            var Hkp1kp1 = H[k + 1, k + 1];
            var t = Math.Abs(Hkk) + Math.Abs(Hkp1kp1);
            
            if (t == 0)
            {
                if (k > 0) t += Math.Abs(H[k, k - 1]);
                if (k + 2 < n) t += Math.Abs(H[k + 2, k + 1]);
            }

            var aHkp1k = Math.Abs(H[k + 1, k]);
            if (aHkp1k <= t * Precision.DoublePrecision)
            {
                var aHkkp1 = Math.Abs(H[k, k + 1]);
                var ab = Math.Max(aHkp1k, aHkkp1);
                var ba = Math.Min(aHkp1k, aHkkp1);
                var aa = Math.Max(Math.Abs(Hkp1kp1), Math.Abs(Hkk - Hkp1kp1));
                var bb = Math.Min(Math.Abs(Hkp1kp1), Math.Abs(Hkk - Hkp1kp1));
                var s = aa + bb;
                
                return ba * (ab / s) <= Math.Max(smallnum, Precision.DoublePrecision * (bb * (aa / s)));
            }

            return false;
        }

        private static Complex ComputeWilkinsonShift(Matrix<Complex> H, int istart, int iend, int iteration)
        {
            // Exceptional shifts based on iteration count
            if (iteration % 30 == 10)
            {
                var s = 0.75 * Math.Abs(H[istart + 1, istart].Real);
                return H[istart, istart] + new Complex(s, 0);
            }
            else if (iteration % 30 == 20)
            {
                var s = 0.75 * Math.Abs(H[iend, iend - 1].Real);
                return H[iend, iend] + new Complex(s, 0);
            }
            else
            {
                // Wilkinson shift
                var t = H[iend, iend];
                var u = Complex.Sqrt(H[iend - 1, iend] * H[iend, iend - 1]);
                var s = u.Magnitude;
                
                if (s != 0.0)
                {
                    var x = 0.5 * (H[iend - 1, iend - 1] - t);
                    var sx = x.Magnitude;
                    s = Math.Max(s, x.Magnitude);
                    var y = s * Complex.Sqrt((x / s) * (x / s) + (u / s) * (u / s));
                    
                    if (sx > 0.0)
                    {
                        if ((x / sx).Real * y.Real + (x / sx).Imaginary * y.Imaginary < 0.0)
                        {
                            y = -y;
                        }
                    }
                    
                    t -= u * (u / (x + y));
                }
                
                return t;
            }
        }

        private static (Complex shift1, Complex shift2) ComputeFrancisShifts(Matrix<double> H, int istart, int iend, int iteration)
        {
            double H11, H12, H21, H22;
            
            if (iteration == 10)
            {
                // Exceptional shift from top
                var s1 = Math.Abs(H[istart + 1, istart]) + Math.Abs(H[istart + 2, istart + 1]);
                H11 = 0.75 * s1 + H[istart, istart];
                H12 = -0.4375 * s1;
                H21 = s1;
                H22 = H11;
            }
            else if (iteration == 20)
            {
                // Exceptional shift from bottom
                var s2 = Math.Abs(H[iend, iend - 1]) + Math.Abs(H[iend - 1, iend - 2]);
                H11 = 0.75 * s2 + H[iend, iend];
                H12 = -0.4375 * s2;
                H21 = s2;
                H22 = H11;
            }
            else
            {
                // Francis' double shift
                H11 = H[iend - 1, iend - 1];
                H21 = H[iend, iend - 1];
                H12 = H[iend - 1, iend];
                H22 = H[iend, iend];
            }

            var s = Math.Abs(H11) + Math.Abs(H12) + Math.Abs(H21) + Math.Abs(H22);
            if (s == 0)
            {
                return (Complex.Zero, Complex.Zero);
            }
            
            H11 /= s;
            H12 /= s;
            H21 /= s;
            H22 /= s;
            
            var tr = (H11 + H22) / 2.0;
            var det = (H11 - tr) * (H22 - tr) - H12 * H21;
            var rtd = Math.Sqrt(Math.Abs(det));
            
            if (det >= 0)
            {
                // Complex conjugate pair
                return (new Complex(tr * s, rtd * s), new Complex(tr * s, -rtd * s));
            }
            else
            {
                // Real eigenvalues
                var r1 = tr + rtd;
                var r2 = tr - rtd;
                
                if (Math.Abs(r1 - H22) <= Math.Abs(r2 - H22))
                {
                    r1 *= s;
                    r2 = r1;
                }
                else
                {
                    r2 *= s;
                    r1 = r2;
                }
                
                return (new Complex(r1, 0), new Complex(r2, 0));
            }
        }

        private static (Complex eig1, Complex eig2) Compute2x2Eigenvalues(Matrix<double> H, int i, int j)
        {
            var a = H[i, i];
            var b = H[i, j];
            var c = H[j, i];
            var d = H[j, j];
            
            var tr = a + d;
            var det = a * d - b * c;
            var disc = tr * tr - 4 * det;
            
            if (disc >= 0)
            {
                var sqrt_disc = Math.Sqrt(disc);
                return (new Complex((tr + sqrt_disc) / 2, 0), new Complex((tr - sqrt_disc) / 2, 0));
            }
            else
            {
                var sqrt_disc = Math.Sqrt(-disc);
                return (new Complex(tr / 2, sqrt_disc / 2), new Complex(tr / 2, -sqrt_disc / 2));
            }
        }

        #endregion

        #region Placeholder Methods (to be implemented)

        private static void SingleShiftQR(Matrix<Complex> H, Matrix<Complex>? Q, Complex shift,
                                         int istart, int iend, MathNet.Numerics.LinearAlgebra.Vector<Complex> v)
        {
            QRAlgorithm.SingleShiftQR(H, Q, shift, istart, iend, v);
        }

        private static void DoubleShiftQR(Matrix<double> H, Matrix<double>? Q, Complex shift1, Complex shift2,
                                        int istart, int iend, MathNet.Numerics.LinearAlgebra.Vector<double> v)
        {
            QRAlgorithm.DoubleShiftQR(H, Q, shift1, shift2, istart, iend, v);
        }

        private static BalanceResult<TMatrix> BalanceMatrix<TMatrix>(Matrix<TMatrix> matrix)
            where TMatrix : struct, IEquatable<TMatrix>, IFormattable
        {
            if (typeof(TMatrix) == typeof(Complex))
            {
                var complexMatrix = matrix as Matrix<Complex>;
                var result = MatrixBalancing.Balance(complexMatrix!);
                return new BalanceResult<TMatrix>(result.Ilo, result.Ihi,
                    result.PermutationRow, result.PermutationCol,
                    result.Scale.Map(x => (TMatrix)(object)x), result.Trivial);
            }
            else if (typeof(TMatrix) == typeof(double))
            {
                var doubleMatrix = matrix as Matrix<double>;
                var result = MatrixBalancing.Balance(doubleMatrix!);
                return new BalanceResult<TMatrix>(result.Ilo, result.Ihi,
                    result.PermutationRow, result.PermutationCol,
                    result.Scale.Map(x => (TMatrix)(object)x), result.Trivial);
            }
            throw new NotSupportedException($"Balancing not supported for type {typeof(TMatrix)}");
        }

        private static void ApplyInverseBalancing<TMatrix>(Matrix<TMatrix> matrix, BalanceResult<TMatrix> balanceResult)
            where TMatrix : struct, IEquatable<TMatrix>, IFormattable
        {
            MatrixBalancing.ApplyInverseBalancing(matrix, balanceResult);
        }

        #endregion
    }

    public struct BalanceResult<T> where T : struct, IEquatable<T>, IFormattable
    {
        public int Ilo { get; }
        public int Ihi { get; }
        public MathNet.Numerics.LinearAlgebra.Vector<int> PermutationRow { get; }
        public MathNet.Numerics.LinearAlgebra.Vector<int> PermutationCol { get; }
        public MathNet.Numerics.LinearAlgebra.Vector<T> Scale { get; }
        public bool Trivial { get; }

        public BalanceResult(int ilo, int ihi, MathNet.Numerics.LinearAlgebra.Vector<int> permRow, MathNet.Numerics.LinearAlgebra.Vector<int> permCol, MathNet.Numerics.LinearAlgebra.Vector<T> scale, bool trivial)
        {
            Ilo = ilo;
            Ihi = ihi;
            PermutationRow = permRow;
            PermutationCol = permCol;
            Scale = scale;
            Trivial = trivial;
        }
    }
}