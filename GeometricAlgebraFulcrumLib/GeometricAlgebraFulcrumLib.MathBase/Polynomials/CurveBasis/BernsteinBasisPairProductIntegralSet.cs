using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.MathBase.Polynomials.CurveBasis
{
    /// <summary>
    /// A polynomial in this set is the integration of the product of two
    /// Bernstein polynomials from half degree basis set
    /// </summary>
    public sealed class BernsteinBasisPairProductIntegralSet :
        IPolynomialPairProductIntegralSet
    {
        private static readonly Dictionary<int, BernsteinBasisPairProductIntegralSet> BasisSetCache
            = new Dictionary<int, BernsteinBasisPairProductIntegralSet>();
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BernsteinBasisPairProductIntegralSet Create(BernsteinBasisPairProductSet bernsteinBasisPairProductSet)
        {
            var n2 = bernsteinBasisPairProductSet.Degree + 1;

            if (BasisSetCache.TryGetValue(n2, out var basisSet))
                return basisSet;

            basisSet = new BernsteinBasisPairProductIntegralSet(bernsteinBasisPairProductSet);

            BasisSetCache.Add(n2, basisSet);

            return basisSet;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BernsteinBasisPairProductIntegralSet Create(int degree)
        {
            var n2 = 2 * degree + 1;

            if (BasisSetCache.TryGetValue(n2, out var basisSet))
                return basisSet;

            var bernsteinBasisPairProductSet = BernsteinBasisPairProductSet.Create(degree);
            basisSet = new BernsteinBasisPairProductIntegralSet(bernsteinBasisPairProductSet);

            BasisSetCache.Add(n2, basisSet);

            return basisSet;
        }


        private readonly BernsteinBasisSet _bernsteinBasisSet2;


        public BernsteinBasisPairProductSet BasisPairProductSet { get; }
        
        public int Degree 
            => 1 + BasisPairProductSet.Degree;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private BernsteinBasisPairProductIntegralSet(BernsteinBasisPairProductSet bernsteinBasisPairProductSet)
        {
            var degree = bernsteinBasisPairProductSet.Degree / 2;

            _bernsteinBasisSet2 = BernsteinBasisSet.Create(2 * degree + 1);
            BasisPairProductSet = bernsteinBasisPairProductSet;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValueAt1(int index1, int index2)
        {
            return BasisPairProductSet.GetBinomialConstant(index1, index2) / Degree;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValueAt1(int index1, int index2, double termScalar)
        {
            return 
                termScalar * 
                BasisPairProductSet.GetBinomialConstant(index1, index2) / 
                Degree;
        }
        
        public double GetValueAt1(double[,] termScalarsList)
        {
            var m = (Degree - 1) / 2;
            var value = 0d;

            for (var i = 0; i <= m; i++)
            for (var j = 0; j <= m; j++)
            {
                value += GetValueAt1(i, j, termScalarsList[i, j]);
            }

            return value;
        }

        public double[,] GetValuesAt1()
        {
            var m = (Degree - 1) / 2;
            var valueArray = new double[m + 1, m + 1];

            for (var i = 0; i <= m; i++)
            {
                valueArray[i, i] = GetValueAt1(i, i);

                for (var j = i + 1; j <= m; j++)
                {
                    var value = GetValueAt1(i, j);

                    valueArray[i, j] = value;
                    valueArray[j, i] = value;
                }
            }

            return valueArray;
        }


        public double GetValue(int index1, int index2, double parameterValue)
        {
            var m = Degree;
            var cij = BasisPairProductSet.GetBinomialConstant(index1, index2) / Degree;

            var value = 0d;

            var k0 = index1 + index2 + 1;
            for (var k = k0; k <= m; k++)
            {
                value += _bernsteinBasisSet2.GetValue(k, parameterValue, cij);
            }

            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetValue(int index1, int index2, double parameterValue, double termScalar)
        {
            return termScalar * GetValue(index1, index2, parameterValue);
        }

        public double GetValue(double parameterValue, double[,] termScalarsList)
        {
            var m = (Degree - 1) / 2;
            var value = 0d;

            for (var i = 0; i <= m; i++)
            for (var j = 0; j <= m; j++)
            {
                value += GetValue(i, j, parameterValue, termScalarsList[i, j]);
            }

            return value;
        }

        public double[,] GetValues(double parameterValue)
        {
            var m = (Degree - 1) / 2;
            var valueArray = new double[m + 1, m + 1];

            for (var i = 0; i <= m; i++)
            for (var j = 0; j <= m; j++)
            {
                valueArray[i, j] = GetValue(i, j, parameterValue);
            }

            return valueArray;
        }
    }
}