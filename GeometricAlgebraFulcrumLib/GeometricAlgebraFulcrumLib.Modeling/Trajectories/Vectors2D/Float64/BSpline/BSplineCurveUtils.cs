using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.BSpline
{
    /// <summary>
    /// Shared utilities for B-spline factories and local basis computations.
    /// </summary>
    internal static class BSplineCurveUtils
    {
        private const double Epsilon = 1e-14;

        /// <summary>
        /// Find the knot span index for parameter u using binary search.
        /// Returns span index i such that u ∈ [U[i], U[i+1]).
        /// </summary>
        public static int FindSpan(BSplineKnotVector U, int p, double u, int n)
        {
            if (U == null) throw new ArgumentNullException(nameof(U));
            if (p < 0) throw new ArgumentOutOfRangeException(nameof(p));
            if (n < p) throw new ArgumentException("n must be >= p");
            
            // n = number of control points - 1
            if (u >= U[n + 1] - Epsilon) return n;
            if (u <= U[p] + Epsilon) return p;

            var low = p;
            var high = n + 1;
            var mid = (low + high) >> 1;

            while (u < U[mid] - Epsilon || u >= U[mid + 1] + Epsilon)
            {
                if (u < U[mid]) 
                    high = mid;
                else 
                    low = mid;
                mid = (low + high) >> 1;
            }

            return mid;
        }

        /// <summary>
        /// Algorithm A2.3 (Piegl & Tiller) — compute nonzero basis functions and derivatives
        /// up to nDerivs for the basis functions with local indices 0..p in the given span.
        /// Returns a (nDerivs+1) x (p+1) array ders[k, j].
        /// </summary>
        public static double[,] DersBasisFunsStatic(int span, double u, int p, int nDerivs, BSplineKnotVector U)
        {
            if (U == null) throw new ArgumentNullException(nameof(U));
            if (span < 0 || span >= U.Count - 1) 
                throw new ArgumentOutOfRangeException(nameof(span));
            if (p < 0) throw new ArgumentOutOfRangeException(nameof(p));
            if (nDerivs < 0) throw new ArgumentOutOfRangeException(nameof(nDerivs));

            nDerivs = Math.Min(nDerivs, p);

            var ndu = new double[p + 1, p + 1];
            var left = new double[p + 1];
            var right = new double[p + 1];

            ndu[0, 0] = 1.0;

            for (var j = 1; j <= p; j++)
            {
                left[j] = u - U[span + 1 - j];
                right[j] = U[span + j] - u;
                var saved = 0.0;

                for (var r = 0; r < j; r++)
                {
                    var denom = right[r + 1] + left[j - r];
                    if (Math.Abs(denom) < Epsilon)
                    {
                        // Handle zero denominator with numerical stability
                        ndu[r, j] = saved;
                        saved = 0.0;
                    }
                    else
                    {
                        var temp = ndu[r, j - 1] / denom;
                        ndu[r, j] = saved + right[r + 1] * temp;
                        saved = left[j - r] * temp;
                    }
                }
                ndu[j, j] = saved;
            }

            var ders = new double[nDerivs + 1, p + 1];
            
            // Load basis functions (0th derivatives)
            for (var j = 0; j <= p; j++)
                ders[0, j] = ndu[j, p];

            // Compute derivatives if needed
            if (nDerivs > 0)
            {
                var a = new double[2, p + 1];

                for (var r = 0; r <= p; r++)
                {
                    int s1 = 0, s2 = 1;
                    a[0, 0] = 1.0;

                    for (var k = 1; k <= nDerivs; k++)
                    {
                        var d = 0.0;
                        var rk = r - k;
                        var pk = p - k;

                        if (r >= k)
                        {
                            var denominator = ndu[pk + 1, rk];
                            if (Math.Abs(denominator) < Epsilon)
                                a[s2, 0] = 0.0;
                            else
                                a[s2, 0] = a[s1, 0] / denominator;
                            d = a[s2, 0] * ndu[rk, pk];
                        }

                        var j1 = (rk >= -1) ? 1 : -rk;
                        var j2 = (r - 1 <= pk) ? k - 1 : p - r;

                        for (var j = j1; j <= j2; j++)
                        {
                            var denominator = ndu[pk + 1, rk + j];
                            if (Math.Abs(denominator) < Epsilon)
                            {
                                a[s2, j] = 0.0;
                            }
                            else
                            {
                                a[s2, j] = (a[s1, j] - a[s1, j - 1]) / denominator;
                            }
                            d += a[s2, j] * ndu[rk + j, pk];
                        }

                        if (r <= pk)
                        {
                            var denominator = ndu[pk + 1, r];
                            if (Math.Abs(denominator) < Epsilon)
                            {
                                a[s2, k] = 0.0;
                            }
                            else
                            {
                                a[s2, k] = -a[s1, k - 1] / denominator;
                            }
                            d += a[s2, k] * ndu[r, pk];
                        }

                        ders[k, r] = d;

                        // swap s1, s2
                        (s1, s2) = (s2, s1);
                    }
                }

                // Multiply derivatives by factorial terms
                var factor = p;
                for (var k = 1; k <= nDerivs; k++)
                {
                    for (var j = 0; j <= p; j++)
                        ders[k, j] *= factor;
                    factor *= (p - k);
                }
            }

            return ders;
        }

        /// <summary>
        /// Build a clamped knot vector with degree p, n+1 control points and given parameter values.
        /// Uses the standard averaged internal knots approach.
        /// </summary>
        public static BSplineKnotVector BuildClampedKnotVectorFromParams(double[] u, int p, int n)
        {
            if (u == null) throw new ArgumentNullException(nameof(u));
            if (p < 0) throw new ArgumentOutOfRangeException(nameof(p));
            if (n < p) throw new ArgumentException("n must be >= p");
            if (u.Length < 2) throw new ArgumentException("At least two parameter values required");

            var kv = BSplineKnotVector.Create();

            // p+1 repeats at start
            kv.AppendKnot(u[0], p + 1);

            // internal knots: n - p internal knots
            var internalCount = n - p;
            if (internalCount > 0)
            {
                for (var j = 1; j <= internalCount; j++)
                {
                    var sum = 0.0;
                    var count = 0;
                    
                    // average u[j .. j + p - 1] with bounds checking
                    for (var r = j; r < j + p; r++)
                    {
                        if (r >= 0 && r < u.Length)
                        {
                            sum += u[r];
                            count++;
                        }
                    }
                    
                    if (count > 0)
                        kv.AppendKnot(sum / count);
                    else
                        kv.AppendKnot(u[0] + (u[^1] - u[0]) * j / (internalCount + 1));
                }
            }

            // p+1 repeats at end
            kv.AppendKnot(u[^1], p + 1);

            return kv;
        }

        /// <summary>
        /// Find the knot span index for parameter u (standard binary search).
        /// This is an alias for FindSpan with the same parameters.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FindSpanForInterpolation(BSplineKnotVector U, int p, double u, int n)
            => FindSpan(U, p, u, n);

        /// <summary>
        /// Solve square linear system A * x = b using Gaussian elimination with partial pivoting.
        /// A is copied internally. Returns the solution vector.
        /// If the matrix is singular (within tolerance) the method attempts to continue.
        /// </summary>
        public static double[] SolveLinearSystem(double[,] A, double[] b)
        {
            if (A == null) throw new ArgumentNullException(nameof(A));
            if (b == null) throw new ArgumentNullException(nameof(b));
            
            var n = b.Length;
            if (A.GetLength(0) != n || A.GetLength(1) != n)
                throw new ArgumentException("Matrix A must be square and match length of b.");

            // Use Math.NET for better numerical stability
            try
            {
                var matrixA = Matrix.Build.DenseOfArray(A);
                var vectorB = Vector.Build.Dense(b);
                
                // Try LU decomposition first
                var solution = matrixA.LU().Solve(vectorB);
                return solution.ToArray();
            }
            catch (NumericalBreakdownException)
            {
                // Fall back to QR decomposition if LU fails
                try
                {
                    var matrixA = Matrix.Build.DenseOfArray(A);
                    var vectorB = Vector.Build.Dense(b);
                    var solution = matrixA.QR().Solve(vectorB);
                    return solution.ToArray();
                }
                catch
                {
                    // Final fallback to SVD for ill-conditioned systems
                    var matrixA = Matrix.Build.DenseOfArray(A);
                    var vectorB = Vector.Build.Dense(b);
                    var solution = matrixA.Svd().Solve(vectorB);
                    return solution.ToArray();
                }
            }
        }

        /// <summary>
        /// Solve overdetermined system using least squares with QR decomposition.
        /// </summary>
        public static double[] SolveLeastSquares(double[,] A, double[] b)
        {
            if (A == null) throw new ArgumentNullException(nameof(A));
            if (b == null) throw new ArgumentNullException(nameof(b));
            
            var matrixA = Matrix.Build.DenseOfArray(A);
            var vectorB = Vector.Build.Dense(b);
            
            return matrixA.QR().Solve(vectorB).ToArray();
        }

        /// <summary>
        /// Create centripetal parameterization for given points.
        /// </summary>
        public static double[] CreateCentripetalParameters<T>(IReadOnlyList<T> points, Func<T, T, double> distanceFunction)
        {
            if (points == null) throw new ArgumentNullException(nameof(points));
            if (points.Count < 2) throw new ArgumentException("At least two points required");
            
            var m = points.Count - 1;
            var u = new double[m + 1];
            u[0] = 0.0;
            var total = 0.0;

            for (var i = 1; i <= m; i++)
            {
                var d = Math.Sqrt(distanceFunction(points[i], points[i - 1]));
                total += d;
                u[i] = total;
            }

            if (total <= Epsilon)
            {
                // Fall back to uniform parameterization
                for (var i = 1; i <= m; i++)
                    u[i] = (double)i / m;
            }
            else
            {
                // Normalize to [0,1]
                for (var i = 1; i <= m; i++)
                    u[i] /= total;
            }

            return u;
        }

        /// <summary>
        /// Create uniform parameterization for given number of points.
        /// </summary>
        public static double[] CreateUniformParameters(int pointCount)
        {
            if (pointCount < 2) throw new ArgumentException("At least two points required");
            
            var parameters = new double[pointCount];
            for (var i = 0; i < pointCount; i++)
            {
                parameters[i] = (double)i / (pointCount - 1);
            }
            return parameters;
        }

        /// <summary>
        /// Check if a knot vector is valid for the given degree and number of control points.
        /// </summary>
        public static bool IsValidKnotVector(BSplineKnotVector knotVector, int degree, int controlPointCount)
        {
            if (knotVector == null) return false;
            if (degree < 0) return false;
            if (controlPointCount <= degree) return false;

            var n = controlPointCount - 1;
            var expectedKnotCount = n + degree + 2; // 0 to n+p+1

            if (knotVector.Count != expectedKnotCount)
                return false;

            // Check non-decreasing property
            for (var i = 1; i < knotVector.Count; i++)
            {
                if (knotVector[i] < knotVector[i - 1] - Epsilon)
                    return false;
            }

            // Check clamp conditions
            if (knotVector.FirstMultiplicity < degree + 1)
                return false;
            if (knotVector.LastMultiplicity < degree + 1)
                return false;

            return true;
        }

        /// <summary>
        /// Calculate the condition number of a matrix using SVD.
        /// </summary>
        public static double CalculateConditionNumber(double[,] matrix)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));
            
            var mat = Matrix.Build.DenseOfArray(matrix);
            var svd = mat.Svd();
            
            var singularValues = svd.S;
            var maxSingularValue = singularValues.Maximum();
            var minSingularValue = singularValues.Minimum();
            
            if (Math.Abs(minSingularValue) < Epsilon)
                return double.PositiveInfinity;
            
            return maxSingularValue / minSingularValue;
        }
    }
}

//namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.BSpline
//{
//    /// <summary>
//    /// Shared utilities for B-spline factories and local basis computations.
//    /// </summary>
//    internal static class BSplineCurveUtils
//    {
//        public static int FindSpan(BSplineKnotVector U, int p, double u, int n)
//        {
//            // n = number of control points - 1
//            if (u >= U[n + 1]) return n;
//            if (u <= U[p]) return p;

//            var low = p;
//            var high = n + 1;
//            var mid = low + high >> 1;

//            while (u < U[mid] || u >= U[mid + 1])
//            {
//                if (u < U[mid]) high = mid;
//                else low = mid;
//                mid = low + high >> 1;
//            }

//            return mid;
//        }

//        /// <summary>
//        /// Algorithm A2.3 (Piegl & Tiller) — compute nonzero basis functions and derivatives
//        /// up to nDerivs for the basis functions with local indices 0..p in the given span.
//        /// Returns a (nDerivs+1) x (p+1) array ders[k, j].
//        /// </summary>
//        public static double[,] DersBasisFunsStatic(int span, double u, int p, int nDerivs, BSplineKnotVector U)
//        {
//            if (nDerivs > p) nDerivs = p;

//            var ndu = new double[p + 1, p + 1];
//            var left = new double[p + 1];
//            var right = new double[p + 1];

//            ndu[0, 0] = 1.0;

//            for (var j = 1; j <= p; j++)
//            {
//                left[j] = u - U[span + 1 - j];
//                right[j] = U[span + j] - u;
//                var saved = 0.0;

//                for (var r = 0; r < j; r++)
//                {
//                    var denom = right[r + 1] + left[j - r];
//                    var temp = denom == 0.0 ? 0.0 : ndu[r, j - 1] / denom;
//                    ndu[r, j] = saved + right[r + 1] * temp;
//                    saved = left[j - r] * temp;
//                }

//                ndu[j, j] = saved;
//            }

//            var ders = new double[nDerivs + 1, p + 1];
//            for (var j = 0; j <= p; j++)
//                ders[0, j] = ndu[j, p];

//            var a = new double[2, p + 1];

//            for (var r = 0; r <= p; r++)
//            {
//                int s1 = 0, s2 = 1;
//                a[0, 0] = 1.0;

//                for (var k = 1; k <= nDerivs; k++)
//                {
//                    var d = 0.0;
//                    var rk = r - k;
//                    var pk = p - k;

//                    if (r >= k)
//                    {
//                        a[s2, 0] = a[s1, 0] / ndu[pk + 1, rk];
//                        d = a[s2, 0] * ndu[rk, pk];
//                    }

//                    var j1 = rk >= -1 ? 1 : -rk;
//                    var j2 = r - 1 <= pk ? k - 1 : p - r;

//                    for (var j = j1; j <= j2; j++)
//                    {
//                        var numerator = a[s1, j] - a[s1, j - 1];
//                        var denom = ndu[pk + 1, rk + j];
//                        a[s2, j] = denom == 0.0 ? 0.0 : numerator / denom;
//                        d += a[s2, j] * ndu[rk + j, pk];
//                    }

//                    if (r <= pk)
//                    {
//                        a[s2, k] = -a[s1, k - 1] / ndu[pk + 1, r];
//                        d += a[s2, k] * ndu[r, pk];
//                    }

//                    ders[k, r] = d;

//                    // swap s1, s2
//                    (s1, s2) = (s2, s1);
//                }
//            }

//            // Multiply derivatives by factorial terms
//            var factor = p;
//            for (var k = 1; k <= nDerivs; k++)
//            {
//                for (var j = 0; j <= p; j++)
//                    ders[k, j] *= factor;
//                factor *= p - k;
//            }

//            return ders;
//        }

//        /// <summary>
//        /// Build a clamped knot vector with degree p, n+1 control points and given parameter values u (size m+1).
//        /// Uses the standard averaged internal knots approach.
//        /// </summary>
//        public static BSplineKnotVector BuildClampedKnotVectorFromParams(double[] u, int p, int n)
//        {
//            // u[] : parameter values size m+1 (data points); here we only need them to average.
//            var kv = BSplineKnotVector.Create();

//            // p+1 repeats at start
//            kv.AppendKnot(u[0], p + 1);

//            // internal knots: n - p internal knots (when n >= p)
//            var internalCount = n - p;
//            if (internalCount > 0)
//            {
//                for (var j = 1; j <= internalCount; j++)
//                {
//                    var sum = 0.0;
//                    // average u[j .. j + p - 1] if possible, typical formula
//                    for (var r = j; r <= j + p - 1; r++)
//                    {
//                        var idx = Math.Min(Math.Max(r, 0), u.Length - 1);
//                        sum += u[idx];
//                    }
//                    kv.AppendKnot(sum / p);
//                }
//            }

//            // p+1 repeats at end
//            kv.AppendKnot(u[u.Length - 1], p + 1);

//            return kv;
//        }




//        /// <summary>
//        /// Find the knot span index for parameter u (standard binary search).
//        /// </summary>
//        public static int FindSpanForInterpolation(
//            BSplineKnotVector U,
//            int p,
//            double u,
//            int n)
//        {
//            // n = number of control points - 1
//            // knot vector indexed 0..n+p+1
//            if (u >= U[n + 1]) return n;
//            if (u <= U[p]) return p;

//            var low = p;
//            var high = n + 1;
//            var mid = low + high >> 1;

//            while (u < U[mid] || u >= U[mid + 1])
//            {
//                if (u < U[mid]) high = mid;
//                else low = mid;
//                mid = low + high >> 1;
//            }

//            return mid;
//        }

//        /// <summary>
//        /// Solve square linear system A * x = b using Gaussian elimination with partial pivoting.
//        /// A is copied internally. Returns the solution vector.
//        /// If the matrix is singular (within tolerance) the method attempts to continue (may produce a least-squares-ish result).
//        /// </summary>
//        public static double[] SolveLinearSystem(double[,] A, double[] b)
//        {
//            if (A == null) throw new ArgumentNullException(nameof(A));
//            if (b == null) throw new ArgumentNullException(nameof(b));
//            var n = b.Length;
//            if (A.GetLength(0) != n || A.GetLength(1) != n)
//                throw new ArgumentException("Matrix A must be square and match length of b.");

//            // Copy matrix and rhs
//            var M = (double[,])A.Clone();
//            var rhs = (double[])b.Clone();
//            var x = new double[n];

//            const double eps = 1e-14;

//            // Forward elimination with partial pivoting
//            for (var i = 0; i < n; i++)
//            {
//                // Pivot
//                var maxRow = i;
//                var maxVal = Math.Abs(M[i, i]);
//                for (var r = i + 1; r < n; r++)
//                {
//                    var val = Math.Abs(M[r, i]);
//                    if (val > maxVal)
//                    {
//                        maxVal = val;
//                        maxRow = r;
//                    }
//                }

//                if (maxRow != i)
//                {
//                    // swap rows i and maxRow in M and rhs
//                    for (var c = i; c < n; c++)
//                    {
//                        (M[i, c], M[maxRow, c]) = (M[maxRow, c], M[i, c]);
//                    }
//                    (rhs[i], rhs[maxRow]) = (rhs[maxRow], rhs[i]);
//                }

//                // If pivot is (near) zero, skip elimination step to avoid /0
//                var pivot = M[i, i];
//                if (Math.Abs(pivot) < eps)
//                {
//                    // singular or nearly singular; skip scaling to avoid division by zero
//                    // leave row as-is; will attempt back-substitution (may produce NaNs if unsalvageable)
//                    continue;
//                }

//                // Normalize pivot row
//                for (var c = i; c < n; c++)
//                    M[i, c] /= pivot;
//                rhs[i] /= pivot;

//                // Eliminate below
//                for (var r = i + 1; r < n; r++)
//                {
//                    var factor = M[r, i];
//                    if (factor == 0.0) continue;
//                    for (var c = i; c < n; c++)
//                        M[r, c] -= factor * M[i, c];
//                    rhs[r] -= factor * rhs[i];
//                }
//            }

//            // Back substitution
//            for (var i = n - 1; i >= 0; i--)
//            {
//                var sum = rhs[i];
//                for (var j = i + 1; j < n; j++)
//                    sum -= M[i, j] * x[j];

//                var diag = M[i, i];
//                if (Math.Abs(diag) < eps)
//                {
//                    // singular diagonal — set solution component to 0 (or rhs if available)
//                    // Prefer rhs if it's consistent, otherwise 0
//                    x[i] = Math.Abs(sum) < eps ? 0.0 : sum; // fallback
//                }
//                else
//                {
//                    x[i] = sum / diag;
//                }
//            }

//            return x;
//        }
//    }
//}
