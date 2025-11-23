using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.CurveBasis;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.BSpline
{
    /// <summary>
    /// Efficient computation of products of pairs of B-spline basis functions.
    /// Uses mathematically correct product space representation with proper knot vector construction.
    /// </summary>
    public sealed class BSplineBasisPairProductSet : IPolynomialPairProductSet
    {
        private readonly Vector<double>[,] _xVectorArray;
        private readonly Matrix<double> _cachedMatrixAInverse;

        public BSplineBasisSet BasisSet { get; }
        public BSplineBasisSet BasisSet2 { get; }
        public int BasisCount => BasisSet.BasisCount;
        public BSplineKnotVector KnotVector => BasisSet.KnotVector;
        public int Degree => 2 * BasisSet.Degree;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BSplineBasisPairProductSet Create(BSplineBasisSet bSplineBasisSet) => new(bSplineBasisSet);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BSplineBasisPairProductSet Create(BSplineKnotVector knotVector, int degree) 
            => new(BSplineBasisSet.Create(knotVector, degree));

        private BSplineBasisPairProductSet(BSplineBasisSet bSplineBasisSet)
        {
            BasisSet = bSplineBasisSet ?? throw new ArgumentNullException(nameof(bSplineBasisSet));

            var degree = bSplineBasisSet.Degree;
            
            // Correct mathematical approach: create proper product space knot vector
            var knotVector2 = BSplineKnotVector.CreateProductKnotVector(
                bSplineBasisSet.KnotVector, 2 * degree);
            
            BasisSet2 = knotVector2.CreateBSplineBasisSet(2 * degree);

            _cachedMatrixAInverse = CreateMatrixAInverse();
            _xVectorArray = CreateVectorXArray();
        }

        #region Matrix Assembly with Proper Mathematical Foundation

        private double[,] CreateMatrixA()
        {
            var q = BasisSet2.BasisCount;
            var matrixA = new double[q, q];

            // Use parallel processing for large matrices
            Parallel.For(0, q, h =>
            {
                for (var k = 0; k <= h; k++)
                {
                    if (Math.Abs(h - k) > 2 * BasisSet.Degree) 
                    {
                        matrixA[h, k] = matrixA[k, h] = 0.0;
                        continue;
                    }

                    var interval = BasisSet2.GetSupportInterval(h)
                        .Intersect(BasisSet2.GetSupportInterval(k));

                    if (interval.IsEmptyInterval)
                    {
                        matrixA[h, k] = matrixA[k, h] = 0.0;
                        continue;
                    }

                    var h1 = h;
                    var k1 = k;

                    double integrand(double t) =>
                        BasisSet2.GetValue(h1, t) * BasisSet2.GetValue(k1, t);

                    var item = NumericalIntegrationUtils.IntegrateAcrossKnotSpans(
                        integrand, BasisSet2.KnotVector, interval.MinValue, interval.MaxValue);

                    matrixA[h, k] = matrixA[k, h] = item;
                }
            });

            return matrixA;
        }

        private Matrix<double> CreateMatrixAInverse()
        {
            var A = Matrix.Build.DenseOfArray(CreateMatrixA());
            
            // Use SVD for numerical stability instead of Cholesky
            try
            {
                var svd = A.Svd();
                
                // Check condition number
                var conditionNumber = svd.S.Maximum() / svd.S.Minimum();
                if (conditionNumber > 1e12)
                {
                    throw new InvalidOperationException(
                        $"Matrix is ill-conditioned (condition number: {conditionNumber:E2})");
                }

                // Use pseudo-inverse for numerical stability
                var tolerance = 1e-12 * svd.S.Maximum();
                var invS = svd.S.Map(s => s > tolerance ? 1.0 / s : 0.0);
                var Ainv = svd.VT.Transpose() * Matrix.Build.DiagonalOfDiagonalVector(invS) * svd.U.Transpose();
                
                return Ainv;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Failed to compute matrix inverse for product set. " +
                    "The basis set might be ill-conditioned.", ex);
            }
        }

        private double[] CreateVectorB(int i, int j)
        {
            var q = BasisSet2.BasisCount;
            var vectorB = new double[q];

            var supportIJ = BasisSet.GetSupportInterval(i).Intersect(BasisSet.GetSupportInterval(j));
            if (supportIJ.IsEmptyInterval)
                return vectorB; // Zero vector if no overlap

            // Use parallel processing for large vectors
            Parallel.For(0, q, k =>
            {
                var interval = supportIJ.Intersect(BasisSet2.GetSupportInterval(k));

                if (interval.IsEmptyInterval)
                {
                    vectorB[k] = 0.0;
                    return;
                }

                double integrand(double t) =>
                    BasisSet.GetValue(i, t) *
                    BasisSet.GetValue(j, t) *
                    BasisSet2.GetValue(k, t);

                vectorB[k] = NumericalIntegrationUtils.IntegrateAcrossKnotSpans(
                    integrand, BasisSet.KnotVector, interval.MinValue, interval.MaxValue);
            });

            return vectorB;
        }

        #endregion

        #region X Vector Computation

        private Vector<double> CreateVectorXFromMatrixAInv(Matrix<double> matrixAInv, int i, int j)
        {
            var vectorB = Vector.Build.DenseOfArray(CreateVectorB(i, j));
            var x = matrixAInv * vectorB;
            
            // Apply numerical cleaning
            x.MapInplace(v => Math.Abs(v) < 1e-14 ? 0.0 : v);
            
            return x;
        }

        private Vector<double>[,] CreateVectorXArray()
        {
            var p = BasisSet.BasisCount;
            var xVectorArray = new Vector<double>[p, p];

            // Precompute support intersections to avoid redundant calculations
            var supportOverlaps = new bool[p, p];
            for (var i = 0; i < p; i++)
            {
                for (var j = 0; j <= i; j++)
                {
                    supportOverlaps[i, j] = supportOverlaps[j, i] = 
                        !BasisSet.GetSupportInterval(i)
                            .Intersect(BasisSet.GetSupportInterval(j))
                            .IsEmptyInterval;
                }
            }

            // Use parallel processing for large arrays
            Parallel.For(0, p, i =>
            {
                for (var j = 0; j <= i; j++)
                {
                    if (!supportOverlaps[i, j])
                    {
                        var zeroVector = Vector.Build.Dense(BasisSet2.BasisCount);
                        xVectorArray[i, j] = zeroVector;
                        xVectorArray[j, i] = zeroVector;
                        continue;
                    }

                    var vectorX = CreateVectorXFromMatrixAInv(_cachedMatrixAInverse, i, j);
                    xVectorArray[i, j] = vectorX;
                    xVectorArray[j, i] = vectorX;
                }
            });

            return xVectorArray;
        }

        #endregion

        #region Value Evaluation

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValue(int index1, int index2, double parameterValue)
        {
            if (index1 < 0 || index1 >= BasisCount) 
                throw new ArgumentOutOfRangeException(nameof(index1));
            if (index2 < 0 || index2 >= BasisCount) 
                throw new ArgumentOutOfRangeException(nameof(index2));

            var xijVector = _xVectorArray[index1, index2];
            var q = BasisSet2.BasisCount;

            var value = 0.0;
            for (var k = 0; k < q; k++)
                value += xijVector[k] * BasisSet2.GetValue(k, parameterValue);

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValue(int index1, int index2, double parameterValue, double termScalar)
            => termScalar * GetValue(index1, index2, parameterValue);

        public double GetValue(double parameterValue, double[,] termScalarsList)
        {
            if (termScalarsList == null)
                throw new ArgumentNullException(nameof(termScalarsList));

            var n = BasisSet.BasisCount;
            if (termScalarsList.GetLength(0) != n || termScalarsList.GetLength(1) != n)
                throw new ArgumentException("termScalarsList dimensions must match BasisCount.");

            var total = 0.0;
            for (var i = 0; i < n; i++)
                for (var j = 0; j < n; j++)
                    total += termScalarsList[i, j] * GetValue(i, j, parameterValue);

            return total;
        }

        public double[,] GetValues(double parameterValue)
        {
            var n = BasisSet.BasisCount;
            var values = new double[n, n];

            Parallel.For(0, n, i =>
            {
                for (var j = 0; j < n; j++)
                {
                    values[i, j] = GetValue(i, j, parameterValue);
                }
            });

            return values;
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Check if the product set is well-conditioned.
        /// </summary>
        public bool IsWellConditioned()
        {
            try
            {
                var svd = _cachedMatrixAInverse.Svd();
                var conditionNumber = svd.S.Maximum() / svd.S.Minimum();
                return conditionNumber < 1e12;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get the condition number of the product set matrix.
        /// </summary>
        public double GetConditionNumber()
        {
            try
            {
                var svd = _cachedMatrixAInverse.Svd();
                return svd.S.Maximum() / svd.S.Minimum();
            }
            catch
            {
                return double.PositiveInfinity;
            }
        }

        #endregion
    }
}