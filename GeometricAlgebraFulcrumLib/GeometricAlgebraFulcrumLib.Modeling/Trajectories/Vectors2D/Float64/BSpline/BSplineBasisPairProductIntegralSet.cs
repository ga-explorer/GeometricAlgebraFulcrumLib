using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Tuples;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.CurveBasis;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.BSpline
{
    /// <summary>
    /// Integrals of B-spline basis pair products and evaluation of their primitive-like forms.
    /// This implementation uses proper mathematical foundations and adaptive numerical integration.
    /// </summary>
    public sealed class BSplineBasisPairProductIntegralSet : 
        IPolynomialPairProductIntegralSet
    {
        public BSplineBasisPairProductSet BasisPairProductSet { get; }
        public BSplineBasisSet BasisSet2 { get; }
        public int Degree => 2 * BasisPairProductSet.BasisSet.Degree + 1;
        public BSplineKnotVector KnotVector => BasisSet2.KnotVector;
        public int BasisCount => BasisPairProductSet.BasisCount;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BSplineBasisPairProductIntegralSet Create(BSplineBasisPairProductSet basisPairProductSet)
            => new(basisPairProductSet);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BSplineBasisPairProductIntegralSet Create(BSplineKnotVector knotVector, int degree)
        {
            var pair = BSplineBasisPairProductSet.Create(knotVector, degree);
            return new BSplineBasisPairProductIntegralSet(pair);
        }

        private BSplineBasisPairProductIntegralSet(BSplineBasisPairProductSet basisPairProductSet)
        {
            BasisPairProductSet = basisPairProductSet ?? throw new ArgumentNullException(nameof(basisPairProductSet));

            var originalBasisSet = basisPairProductSet.BasisSet;
            var originalDegree = originalBasisSet.Degree;
            
            // The integral of product of two degree-p B-splines has degree 2p+1
            // Create a refined knot vector for better numerical integration
            var knotVector = BSplineKnotVector.CreateRefinedKnotVector(
                originalBasisSet.KnotVector, 
                refinementLevel: 1);

            BasisSet2 = knotVector.CreateBSplineBasisSet(2 * originalDegree + 1);
        }

        /// <summary>
        /// Integral over domain of product N_i * N_j.
        /// </summary>
        public double GetValueAt1(int index1, int index2)
        {
            if (index1 < 0 || index1 >= BasisCount)
                throw new ArgumentOutOfRangeException(nameof(index1));
            if (index2 < 0 || index2 >= BasisCount)
                throw new ArgumentOutOfRangeException(nameof(index2));

            // Integrate BasisSet.GetValue(i,t) * BasisSet.GetValue(j,t) across shared support
            var supportI = BasisPairProductSet.BasisSet.GetSupportInterval(index1);
            var supportJ = BasisPairProductSet.BasisSet.GetSupportInterval(index2);
            var interval = supportI.Intersect(supportJ);
            
            if (interval.IsEmptyInterval) 
                return 0.0;

            return NumericalIntegrationUtils.IntegrateAcrossKnotSpans(
                Integrand, BasisPairProductSet.BasisSet.KnotVector, 
                interval.MinValue, interval.MaxValue);

            double Integrand(double t) => 
                BasisPairProductSet.BasisSet.GetValue(index1, t) * 
                BasisPairProductSet.BasisSet.GetValue(index2, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValueAt1(int index1, int index2, double termScalar) 
            => termScalar * GetValueAt1(index1, index2);

        public double GetValueAt1(double[,] termScalarsList)
        {
            if (termScalarsList == null)
                throw new ArgumentNullException(nameof(termScalarsList));

            if (termScalarsList.GetLength(0) != BasisCount || termScalarsList.GetLength(1) != BasisCount)
                throw new ArgumentException("termScalarsList dimensions must match basis count.");

            var value = 0.0;
            for (var i = 0; i < BasisCount; i++)
            for (var j = 0; j < BasisCount; j++)
                value += GetValueAt1(i, j, termScalarsList[i, j]);

            return value;
        }

        public Matrix<double> GetValuesAt1()
        {
            var valueArray = new double[BasisCount, BasisCount];

            // Use parallel processing for better performance
            Parallel.For(0, BasisCount, i =>
            {
                for (var j = 0; j <= i; j++)
                {
                    var value = GetValueAt1(i, j);
                    valueArray[i, j] = value;
                    valueArray[j, i] = value;
                }
            });

            return Matrix.Build.DenseOfArray(valueArray);
        }

        /// <summary>
        /// Value of the primitive-like representation at parameter value.
        /// Computes ∫₀ᵗ N_i(s) * N_j(s) ds using adaptive numerical integration.
        /// </summary>
        public double GetValue(int index1, int index2, double parameterValue)
        {
            if (index1 < 0 || index1 >= BasisCount)
                throw new ArgumentOutOfRangeException(nameof(index1));
            if (index2 < 0 || index2 >= BasisCount)
                throw new ArgumentOutOfRangeException(nameof(index2));

            var supportI = BasisPairProductSet.BasisSet.GetSupportInterval(index1);
            var supportJ = BasisPairProductSet.BasisSet.GetSupportInterval(index2);

            var support = supportI.Intersect(supportJ);
            if (support.IsEmptyInterval) 
                return 0.0;

            var a = support.MinValue;
            var b = Math.Min(parameterValue, support.MaxValue);
            if (b <= a) 
                return 0.0;

            return NumericalIntegrationUtils.IntegrateAcrossKnotSpans(
                Integrand, BasisPairProductSet.BasisSet.KnotVector, a, b);

            double Integrand(double t) => 
                BasisPairProductSet.BasisSet.GetValue(index1, t) * 
                BasisPairProductSet.BasisSet.GetValue(index2, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValue(int index1, int index2, double parameterValue, double termScalar)
            => termScalar * GetValue(index1, index2, parameterValue);

        public double GetValue(double parameterValue, double[,] termScalarsList)
        {
            if (termScalarsList == null)
                throw new ArgumentNullException(nameof(termScalarsList));

            if (termScalarsList.GetLength(0) != BasisCount || termScalarsList.GetLength(1) != BasisCount)
                throw new ArgumentException("termScalarsList dimensions must match basis count.");

            var value = 0.0;
            for (var i = 0; i < BasisCount; i++)
            for (var j = 0; j < BasisCount; j++)
                value += GetValue(i, j, parameterValue, termScalarsList[i, j]);

            return value;
        }

        public double[,] GetValues(double parameterValue)
        {
            var valueArray = new double[BasisCount, BasisCount];

            // Use parallel processing for better performance
            Parallel.For(0, BasisCount, i =>
            {
                for (var j = 0; j < BasisCount; j++)
                {
                    valueArray[i, j] = GetValue(i, j, parameterValue);
                }
            });

            return valueArray;
        }

        #region Advanced Features

        /// <summary>
        /// Compute the definite integral over a specific interval [a, b].
        /// </summary>
        public double GetDefiniteIntegral(int index1, int index2, double a, double b)
        {
            if (index1 < 0 || index1 >= BasisCount)
                throw new ArgumentOutOfRangeException(nameof(index1));
            if (index2 < 0 || index2 >= BasisCount)
                throw new ArgumentOutOfRangeException(nameof(index2));
            if (b <= a) 
                return 0.0;

            var supportI = BasisPairProductSet.BasisSet.GetSupportInterval(index1);
            var supportJ = BasisPairProductSet.BasisSet.GetSupportInterval(index2);
            var support = supportI.Intersect(supportJ);
            
            var integrationInterval = support.Intersect(Float64Interval.Create(a, b));
            if (integrationInterval.IsEmptyInterval) 
                return 0.0;

            return NumericalIntegrationUtils.IntegrateAcrossKnotSpans(
                Integrand, BasisPairProductSet.BasisSet.KnotVector, 
                integrationInterval.MinValue, integrationInterval.MaxValue);

            double Integrand(double t) => 
                BasisPairProductSet.BasisSet.GetValue(index1, t) * 
                BasisPairProductSet.BasisSet.GetValue(index2, t);
        }

        /// <summary>
        /// Check if the integral set is well-conditioned by examining the condition number.
        /// </summary>
        public bool IsWellConditioned(double threshold = 1e12)
        {
            try
            {
                var matrix = GetValuesAt1();
                var conditionNumber = BSplineCurveUtils.CalculateConditionNumber(matrix.ToArray());
                return conditionNumber < threshold;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get the condition number of the integral matrix.
        /// </summary>
        public double GetConditionNumber()
        {
            try
            {
                var matrix = GetValuesAt1();
                return BSplineCurveUtils.CalculateConditionNumber(matrix.ToArray());
            }
            catch
            {
                return double.PositiveInfinity;
            }
        }

        /// <summary>
        /// Clear cached data to free memory.
        /// </summary>
        public void ClearCache()
        {
            BasisPairProductSet.BasisSet.ClearCache();
            BasisSet2.ClearCache();
        }

        #endregion
    }
}