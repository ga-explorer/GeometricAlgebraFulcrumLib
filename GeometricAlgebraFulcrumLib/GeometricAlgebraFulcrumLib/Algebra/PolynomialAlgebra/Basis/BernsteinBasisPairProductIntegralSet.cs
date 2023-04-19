using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis
{
    /// <summary>
    /// A polynomial in this set is the integration of the product of two
    /// Bernstein polynomials from half degree basis set
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class BernsteinBasisPairProductIntegralSet<T> :
        IPolynomialPairProductIntegralSet<T>
    {
        private static readonly Dictionary<int, BernsteinBasisPairProductIntegralSet<T>> BasisSetCache
            = new Dictionary<int, BernsteinBasisPairProductIntegralSet<T>>();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BernsteinBasisPairProductIntegralSet<T> Create(BernsteinBasisPairProductSet<T> bernsteinBasisPairProductSet)
        {
            var scalarProcessor = bernsteinBasisPairProductSet.ScalarProcessor;
            var n2 = bernsteinBasisPairProductSet.Degree + 1;

            if (BasisSetCache.TryGetValue(n2, out var basisSet))
            {
                if (ReferenceEquals(basisSet.ScalarProcessor, scalarProcessor))
                    return basisSet;

                basisSet = new BernsteinBasisPairProductIntegralSet<T>(bernsteinBasisPairProductSet);

                BasisSetCache[n2] = basisSet;

                return basisSet;
            }
            else
            {
                basisSet = new BernsteinBasisPairProductIntegralSet<T>(bernsteinBasisPairProductSet);

                BasisSetCache.Add(n2, basisSet);

                return basisSet;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BernsteinBasisPairProductIntegralSet<T> Create(IScalarProcessor<T> scalarProcessor, int degree)
        {
            var n2 = 2 * degree + 1;

            if (BasisSetCache.TryGetValue(n2, out var basisSet))
            {
                if (ReferenceEquals(basisSet.ScalarProcessor, scalarProcessor))
                    return basisSet;

                var bernsteinBasisPairProductSet = BernsteinBasisPairProductSet<T>.Create(scalarProcessor, degree);
                basisSet = new BernsteinBasisPairProductIntegralSet<T>(bernsteinBasisPairProductSet);

                BasisSetCache[n2] = basisSet;

                return basisSet;
            }
            else
            {
                var bernsteinBasisPairProductSet = BernsteinBasisPairProductSet<T>.Create(scalarProcessor, degree);
                basisSet = new BernsteinBasisPairProductIntegralSet<T>(bernsteinBasisPairProductSet);

                BasisSetCache.Add(n2, basisSet);

                return basisSet;
            }
        }


        private readonly BernsteinBasisSet<T> _bernsteinBasisSet2;


        public BernsteinBasisPairProductSet<T> BasisPairProductSet { get; }

        public IScalarProcessor<T> ScalarProcessor 
            => BasisPairProductSet.ScalarProcessor;

        public int Degree 
            => 1 + BasisPairProductSet.Degree;


        private BernsteinBasisPairProductIntegralSet(BernsteinBasisPairProductSet<T> bernsteinBasisPairProductSet)
        {
            var degree = bernsteinBasisPairProductSet.Degree / 2;
            var scalarProcessor = bernsteinBasisPairProductSet.ScalarProcessor;

            _bernsteinBasisSet2 = BernsteinBasisSet<T>.Create(scalarProcessor, 2 * degree + 1);
            BasisPairProductSet = bernsteinBasisPairProductSet;
        }


        public Scalar<T> GetValueAt1(int index1, int index2)
        {
            return BasisPairProductSet.GetBinomialConstant(index1, index2) / Degree;
        }
        
        public Scalar<T> GetValueAt1(int index1, int index2, T termScalar)
        {
            return 
                termScalar * 
                BasisPairProductSet.GetBinomialConstant(index1, index2) / 
                ScalarProcessor.GetScalarFromNumber(Degree);
        }
        
        public Scalar<T> GetValueAt1(T[,] termScalarsList)
        {
            var m = (Degree - 1) / 2;
            var value = ScalarProcessor.CreateScalarZero();

            for (var i = 0; i <= m; i++)
            for (var j = 0; j <= m; j++)
            {
                value += GetValueAt1(i, j, termScalarsList[i, j]);
            }

            return value;
        }

        public T[,] GetValuesAt1()
        {
            var m = (Degree - 1) / 2;
            var valueArray = new T[m + 1, m + 1];

            for (var i = 0; i <= m; i++)
            {
                valueArray[i, i] = GetValueAt1(i, i).ScalarValue;

                for (var j = i + 1; j <= m; j++)
                {
                    var value = GetValueAt1(i, j).ScalarValue;

                    valueArray[i, j] = value;
                    valueArray[j, i] = value;
                }
            }

            return valueArray;
        }


        public Scalar<T> GetValue(int index1, int index2, T parameterValue)
        {
            var m = Degree;
            var cij = 
                BasisPairProductSet.GetBinomialConstant(index1, index2) / m;

            var value = ScalarProcessor.CreateScalarZero();

            var k0 = index1 + index2 + 1;
            for (var k = k0; k <= m; k++)
            {
                value += _bernsteinBasisSet2.GetValue(k, parameterValue, cij);
            }

            return value;
        }
        
        public Scalar<T> GetValue(int index1, int index2, T parameterValue, T termScalar)
        {
            return 
                termScalar *
                GetValue(index1, index2, parameterValue);
        }

        public Scalar<T> GetValue(T parameterValue, T[,] termScalarsList)
        {
            var m = (Degree - 1) / 2;
            var value = ScalarProcessor.CreateScalarZero();

            for (var i = 0; i <= m; i++)
            for (var j = 0; j <= m; j++)
            {
                value += GetValue(i, j, parameterValue, termScalarsList[i, j]);
            }

            return value;
        }

        public T[,] GetValues(T parameterValue)
        {
            var m = (Degree - 1) / 2;
            var valueArray = new T[m + 1, m + 1];

            for (var i = 0; i <= m; i++)
            for (var j = 0; j <= m; j++)
            {
                valueArray[i, j] = GetValue(i, j, parameterValue).ScalarValue;
            }

            return valueArray;
        }
    }
}