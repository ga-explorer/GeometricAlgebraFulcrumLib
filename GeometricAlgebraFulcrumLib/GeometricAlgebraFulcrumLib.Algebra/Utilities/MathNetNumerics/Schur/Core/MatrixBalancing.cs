using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.MathNetNumerics.Schur.Utils;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.Utilities.MathNetNumerics.Schur.Core
{
    /// <summary>
    /// Matrix balancing for improved numerical stability.
    /// Translation of Julia's balance.jl file.
    /// </summary>
    public static class MatrixBalancing
    {
        /// <summary>
        /// Balance a complex square matrix to improve numerical stability.
        /// Returns the balanced matrix and balancing information.
        /// </summary>
        public static BalanceResult<Complex> Balance(Matrix<Complex> matrixA, bool scale = true, bool permute = true)
        {
            if (matrixA == null) throw new ArgumentNullException(nameof(matrixA));
            if (matrixA.RowCount != matrixA.ColumnCount)
                throw new ArgumentException("Matrix must be square", nameof(matrixA));

            var n = matrixA.RowCount;
            var beta = 2.0;
            var factor = 0.95;

            var sfmin1 = NumericalUtils.SafeMin() / Precision.DoublePrecision;
            var sfmax1 = 1.0 / sfmin1;
            var sfmin2 = sfmin1 * beta;
            var sfmax2 = 1.0 / sfmin2;

            var D = MathNet.Numerics.LinearAlgebra.Complex.DenseVector.Create(n, Complex.One);
            var ilo = 0;
            var ihi = n - 1;
            var trivial = true;
            var sp = new int[n];

            if (permute)
            {
                // Look for row permutations which make the lower portion upper triangular
                ihi = n;
                bool exchange;
                var js = 0;
                while (ihi > 0)
                {
                    ihi--;
                    exchange = false;
                    var ms = 0;
                    
                    for (var j = ihi; j >= 0; j--)
                    {
                        exchange = true;
                        js = j;
                        for (var i = 0; i < ihi + 1; i++)
                        {
                            if (i == j) continue;
                            if (matrixA[j, i] != Complex.Zero)
                            {
                                exchange = false;
                                break;
                            }
                        }
                        if (exchange)
                        {
                            ms = ihi;
                            break;
                        }
                    }
                    
                    if (exchange)
                    {
                        sp[ms] = js;
                        if (js != ms)
                        {
                            trivial = false;
                            // Swap rows js and ms
                            for (var i = 0; i < ihi + 1; i++)
                            {
                                (matrixA[i, js], matrixA[i, ms]) = (matrixA[i, ms], matrixA[i, js]);
                            }
                            // Swap columns js and ms
                            for (var i = ilo; i < n; i++)
                            {
                                (matrixA[js, i], matrixA[ms, i]) = (matrixA[ms, i], matrixA[js, i]);
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (ihi > 0)
                {
                    // Look for column permutations which make the left portion upper triangular
                    ilo = -1;
                    while (ilo < n - 1)
                    {
                        ilo++;
                        exchange = false;
                        var ms = 0;
                        js = 0;
                        
                        for (var j = ilo; j < ihi + 1; j++)
                        {
                            js = j;
                            exchange = true;
                            for (var i = ilo; i < ihi + 1; i++)
                            {
                                if (i == j) continue;
                                if (matrixA[i, j] != Complex.Zero)
                                {
                                    exchange = false;
                                    break;
                                }
                            }
                            if (exchange)
                            {
                                ms = ilo;
                                break;
                            }
                        }
                        
                        if (exchange)
                        {
                            sp[ms] = js;
                            if (ms != js)
                            {
                                trivial = false;
                                // Swap columns ms and js
                                for (var i = 0; i < ihi + 1; i++)
                                {
                                    (matrixA[i, js], matrixA[i, ms]) = (matrixA[i, ms], matrixA[i, js]);
                                }
                                // Swap rows ms and js
                                for (var i = ilo; i < n; i++)
                                {
                                    (matrixA[js, i], matrixA[ms, i]) = (matrixA[ms, i], matrixA[js, i]);
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            if (scale)
            {
                var converged = false;
                while (!converged)
                {
                    converged = true;
                    for (var i = ilo; i <= ihi; i++)
                    {
                        // Compute column and row norms
                        double c = 0.0, r = 0.0;
                        for (var k = ilo; k <= ihi; k++)
                        {
                            if (k != i)
                            {
                                c += matrixA[k, i].Magnitude;
                                r += matrixA[i, k].Magnitude;
                            }
                        }

                        double ca = 0.0, ra = 0.0;
                        for (var k = ilo; k <= ihi; k++)
                        {
                            ca = Math.Max(ca, matrixA[k, i].Magnitude);
                            ra = Math.Max(ra, matrixA[i, k].Magnitude);
                        }

                        if (c == 0.0 || r == 0.0)
                            continue;

                        var g = r / beta;
                        var s = c + r;
                        var f = 1.0;
                        
                        while (c < r / beta)
                        {
                            if (c >= g || Math.Max(f, Math.Max(c, ca)) >= sfmax2 || Math.Min(r, Math.Min(g, ra)) <= sfmin2)
                                break;
                            if (double.IsNaN(c + f + ca + r + g + ra))
                                throw new ArgumentException("NaN encountered while balancing");
                            
                            f *= beta;
                            c *= beta;
                            ca *= beta;
                            r /= beta;
                            g /= beta;
                            ra /= beta;
                        }
                        
                        g = c / beta;
                        while (r <= c / beta)
                        {
                            if (g < r || Math.Max(r, ra) >= sfmax2 || Math.Min(f, Math.Min(c, Math.Min(g, ca))) <= sfmin2)
                                break;
                            
                            f /= beta;
                            c /= beta;
                            g /= beta;
                            ca /= beta;
                            r *= beta;
                            ra *= beta;
                        }
                        
                        if (f != 1.0)
                            trivial = false;
                        
                        if ((c + r) >= factor * s)
                            continue;
                        
                        converged = false;
                        D[i] *= f;
                        
                        // Scale row i
                        for (var j = ilo; j < n; j++)
                        {
                            matrixA[i, j] /= f;
                        }
                        // Scale column i
                        for (var j = 0; j <= ihi; j++)
                        {
                            matrixA[j, i] *= f;
                        }
                    }
                }
            }

            var permutationRow = MathNet.Numerics.LinearAlgebra.Double.DenseVector.Create(ilo, i => sp[i]);
            var permutationCol = MathNet.Numerics.LinearAlgebra.Double.DenseVector.Create(n - ihi - 1, i => sp[ihi + 1 + i]);

            return new BalanceResult<Complex>(ilo, ihi, 
                permutationRow.Map(x => (int)x), 
                permutationCol.Map(x => (int)x), 
                D, trivial);
        }

        /// <summary>
        /// Balance a real square matrix.
        /// </summary>
        public static BalanceResult<double> Balance(Matrix<double> matrixA, bool scale = true, bool permute = true)
        {
            if (matrixA == null) throw new ArgumentNullException(nameof(matrixA));
            if (matrixA.RowCount != matrixA.ColumnCount)
                throw new ArgumentException("Matrix must be square", nameof(matrixA));

            var n = matrixA.RowCount;
            var beta = 2.0;
            var factor = 0.95;

            var sfmin1 = NumericalUtils.SafeMin() / Precision.DoublePrecision;
            var sfmax1 = 1.0 / sfmin1;
            var sfmin2 = sfmin1 * beta;
            var sfmax2 = 1.0 / sfmin2;

            var D = MathNet.Numerics.LinearAlgebra.Double.DenseVector.Create(n, 1.0);
            var ilo = 0;
            var ihi = n - 1;
            var trivial = true;
            var sp = new int[n];

            if (permute)
            {
                // Look for row permutations which make the lower portion upper triangular
                ihi = n;
                bool exchange;
                var js = 0;
                while (ihi > 0)
                {
                    ihi--;
                    exchange = false;
                    var ms = 0;
                    
                    for (var j = ihi; j >= 0; j--)
                    {
                        exchange = true;
                        js = j;
                        for (var i = 0; i < ihi + 1; i++)
                        {
                            if (i == j) continue;
                            if (matrixA[j, i] != 0.0)
                            {
                                exchange = false;
                                break;
                            }
                        }
                        if (exchange)
                        {
                            ms = ihi;
                            break;
                        }
                    }
                    
                    if (exchange)
                    {
                        sp[ms] = js;
                        if (js != ms)
                        {
                            trivial = false;
                            // Swap rows js and ms
                            for (var i = 0; i < ihi + 1; i++)
                            {
                                (matrixA[i, js], matrixA[i, ms]) = (matrixA[i, ms], matrixA[i, js]);
                            }
                            // Swap columns js and ms
                            for (var i = ilo; i < n; i++)
                            {
                                (matrixA[js, i], matrixA[ms, i]) = (matrixA[ms, i], matrixA[js, i]);
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (ihi > 0)
                {
                    // Look for column permutations which make the left portion upper triangular
                    ilo = -1;
                    while (ilo < n - 1)
                    {
                        ilo++;
                        exchange = false;
                        var ms = 0;
                        js = 0;
                        
                        for (var j = ilo; j < ihi + 1; j++)
                        {
                            js = j;
                            exchange = true;
                            for (var i = ilo; i < ihi + 1; i++)
                            {
                                if (i == j) continue;
                                if (matrixA[i, j] != 0.0)
                                {
                                    exchange = false;
                                    break;
                                }
                            }
                            if (exchange)
                            {
                                ms = ilo;
                                break;
                            }
                        }
                        
                        if (exchange)
                        {
                            sp[ms] = js;
                            if (ms != js)
                            {
                                trivial = false;
                                // Swap columns ms and js
                                for (var i = 0; i < ihi + 1; i++)
                                {
                                    (matrixA[i, js], matrixA[i, ms]) = (matrixA[i, ms], matrixA[i, js]);
                                }
                                // Swap rows ms and js
                                for (var i = ilo; i < n; i++)
                                {
                                    (matrixA[js, i], matrixA[ms, i]) = (matrixA[ms, i], matrixA[js, i]);
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            if (scale)
            {
                var converged = false;
                while (!converged)
                {
                    converged = true;
                    for (var i = ilo; i <= ihi; i++)
                    {
                        // Compute column and row norms
                        double c = 0.0, r = 0.0;
                        for (var k = ilo; k <= ihi; k++)
                        {
                            if (k != i)
                            {
                                c += Math.Abs(matrixA[k, i]);
                                r += Math.Abs(matrixA[i, k]);
                            }
                        }

                        double ca = 0.0, ra = 0.0;
                        for (var k = ilo; k <= ihi; k++)
                        {
                            ca = Math.Max(ca, Math.Abs(matrixA[k, i]));
                            ra = Math.Max(ra, Math.Abs(matrixA[i, k]));
                        }

                        if (c == 0.0 || r == 0.0)
                            continue;

                        var g = r / beta;
                        var s = c + r;
                        var f = 1.0;
                        
                        while (c < r / beta)
                        {
                            if (c >= g || Math.Max(f, Math.Max(c, ca)) >= sfmax2 || Math.Min(r, Math.Min(g, ra)) <= sfmin2)
                                break;
                            if (double.IsNaN(c + f + ca + r + g + ra))
                                throw new ArgumentException("NaN encountered while balancing");
                            
                            f *= beta;
                            c *= beta;
                            ca *= beta;
                            r /= beta;
                            g /= beta;
                            ra /= beta;
                        }
                        
                        g = c / beta;
                        while (r <= c / beta)
                        {
                            if (g < r || Math.Max(r, ra) >= sfmax2 || Math.Min(f, Math.Min(c, Math.Min(g, ca))) <= sfmin2)
                                break;
                            
                            f /= beta;
                            c /= beta;
                            g /= beta;
                            ca /= beta;
                            r *= beta;
                            ra *= beta;
                        }
                        
                        if (f != 1.0)
                            trivial = false;
                        
                        if ((c + r) >= factor * s)
                            continue;
                        
                        converged = false;
                        D[i] *= f;
                        
                        // Scale row i
                        for (var j = ilo; j < n; j++)
                        {
                            matrixA[i, j] /= f;
                        }
                        // Scale column i
                        for (var j = 0; j <= ihi; j++)
                        {
                            matrixA[j, i] *= f;
                        }
                    }
                }
            }

            var permutationRow = MathNet.Numerics.LinearAlgebra.Double.DenseVector.Create(ilo, i => sp[i]);
            var permutationCol = MathNet.Numerics.LinearAlgebra.Double.DenseVector.Create(n - ihi - 1, i => sp[ihi + 1 + i]);

            return new BalanceResult<double>(ilo, ihi, 
                permutationRow.Map(x => (int)x), 
                permutationCol.Map(x => (int)x), 
                D, trivial);
        }

        /// <summary>
        /// Apply inverse balancing transformation to restore original scaling.
        /// This is typically applied to eigenvectors after computing the Schur decomposition.
        /// </summary>
        public static void ApplyInverseBalancing<T>(Matrix<T> matrix, BalanceResult<T> balanceResult) 
            where T : struct, IEquatable<T>, IFormattable
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));
            if (balanceResult.Trivial) return;

            var n = matrix.RowCount;
            var ilo = balanceResult.Ilo;
            var ihi = balanceResult.Ihi;

            // Apply scaling transformation
            if (ilo != ihi)
            {
                for (var i = 0; i < n; i++)
                {
                    for (var j = ilo; j <= ihi; j++)
                    {
                        if (typeof(T) == typeof(Complex))
                        {
                            var scale = (Complex)(object)balanceResult.Scale[j];
                            var value = (Complex)(object)matrix[j, i];
                            matrix[j, i] = (T)(object)(value * scale);
                        }
                        else if (typeof(T) == typeof(double))
                        {
                            var scale = (double)(object)balanceResult.Scale[j];
                            var value = (double)(object)matrix[j, i];
                            matrix[j, i] = (T)(object)(value * scale);
                        }
                    }
                }
            }

            // Apply permutation transformations
            for (var j = ilo - 1; j >= 0; j--)
            {
                var m = balanceResult.PermutationRow[j];
                if (m == j) continue;
                
                for (var i = 0; i < n; i++)
                {
                    (matrix[j, i], matrix[m, i]) = (matrix[m, i], matrix[j, i]);
                }
            }

            for (var j = ihi + 1; j < n; j++)
            {
                var m = balanceResult.PermutationCol[j - ihi - 1];
                if (m == j) continue;
                
                for (var i = 0; i < n; i++)
                {
                    (matrix[j, i], matrix[m, i]) = (matrix[m, i], matrix[j, i]);
                }
            }
        }
    }
}