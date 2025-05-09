namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Generic.BSplines;

//public sealed class BSplineBasisPairProductSet<T> :
//    IPolynomialPairProductSet<T>
//{
//    private static readonly Dictionary<int, BSplineBasisPairProductSet<T>> BasisSetCache
//        = new Dictionary<int, BSplineBasisPairProductSet<T>>();

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static BSplineBasisPairProductSet<T> Create(BSplineBasisSet<T> bernsteinBasisSet)
//    {
//        var n2 = 2 * bernsteinBasisSet.Degree;
//        var scalarProcessor = bernsteinBasisSet.ScalarProcessor;

//        if (BasisSetCache.TryGetValue(n2, out var basisSet))
//        {
//            if (ReferenceEquals(basisSet.ScalarProcessor, scalarProcessor))
//                return basisSet;

//            basisSet = new BSplineBasisPairProductSet<T>(bernsteinBasisSet);

//            BasisSetCache[n2] = basisSet;

//            return basisSet;
//        }
//        else
//        {
//            basisSet = new BSplineBasisPairProductSet<T>(bernsteinBasisSet);

//            BasisSetCache.Add(n2, basisSet);

//            return basisSet;
//        }
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static BSplineBasisPairProductSet<T> Create(IScalarAlgebraProcessor<T> scalarProcessor, int degree)
//    {
//        var n2 = 2 * degree;

//        if (BasisSetCache.TryGetValue(n2, out var basisSet))
//        {
//            if (ReferenceEquals(basisSet.ScalarProcessor, scalarProcessor))
//                return basisSet;

//            var bernsteinBasisSet = BSplineBasisSet<T>.Create(scalarProcessor, degree);
//            basisSet = new BSplineBasisPairProductSet<T>(bernsteinBasisSet);

//            BasisSetCache[n2] = basisSet;

//            return basisSet;
//        }
//        else
//        {
//            var bernsteinBasisSet = BSplineBasisSet<T>.Create(scalarProcessor, degree);
//            basisSet = new BSplineBasisPairProductSet<T>(bernsteinBasisSet);

//            BasisSetCache.Add(n2, basisSet);

//            return basisSet;
//        }
//    }


//    //private readonly ulong _binomialConstantsDenominator;
//    //private readonly ulong[,] _binomialConstantsNumerators;
//    private readonly BSplineBasisSet<T> _bSplineBasisSet2;
        

//    public BSplineBasisSet<T> BasisSet { get; }

//    public IScalarAlgebraProcessor<T> ScalarProcessor 
//        => BasisSet.ScalarProcessor;

//    public int Degree 
//        => 2 * BasisSet.Degree;

        
//    private BSplineBasisPairProductSet(BSplineBasisSet<T> bSplineBasisSet)
//    {
//        //var scalarProcessor = bSplineBasisSet.ScalarProcessor;
//        var degree = bSplineBasisSet.Degree;
//        var n2 = 2 * degree;
            
//        BasisSet = bSplineBasisSet;
//        var knotVector2 = bSplineBasisSet.KnotVector.ScaleMultiplicity(degree + 1);
//        _bSplineBasisSet2 = BSplineBasisSet<T>.Create(knotVector2, n2);
//        _binomialConstantsNumerators = new ulong[degree + 1, degree + 1];
//        _binomialConstantsDenominator = n2.GetMaxBinomialCoefficient();

//        FillBinomialConstants();
//    }
        

//    private void FillBinomialConstants()
//    {
//        var degree = BasisSet.Degree;
//        var n2 = 2 * degree;

//        for (var i = 0; i <= degree; i++)
//        {
//            var nCi = degree.GetBinomialCoefficient(i);

//            _binomialConstantsNumerators[i, i] = 
//                _binomialConstantsDenominator * nCi * nCi /
//                n2.GetBinomialCoefficient(2 * i);

//            for (var j = i + 1; j <= degree; j++)
//            {
//                var nCj = degree.GetBinomialCoefficient(j);

//                var binomialConstantNumerator = 
//                    _binomialConstantsDenominator * nCi * nCj /
//                    n2.GetBinomialCoefficient(i + j);

//                _binomialConstantsNumerators[i, j] = binomialConstantNumerator;
//                _binomialConstantsNumerators[j, i] = binomialConstantNumerator;
//            }
//        }
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<T> GetBinomialConstant(int index1, int index2)
//    {
//        return ScalarProcessor.Divide(
//            ScalarProcessor.ScalarFromNumber(_binomialConstantsNumerators[index1, index2]) ,
//            ScalarProcessor.ScalarFromNumber(_binomialConstantsDenominator)
//        ).CreateScalar(ScalarProcessor);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<T> GetValue(int index1, int index2, T parameterValue)
//    {
//        var cij = GetBinomialConstant(index1, index2);

//        return _bSplineBasisSet2.GetValue(
//            index1 + index2, 
//            parameterValue, 
//            cij
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public Scalar<T> GetValue(int index1, int index2, T parameterValue, T termScalar)
//    {
//        return termScalar * GetValue(index1, index2, parameterValue);
//    }

//    public Scalar<T> GetValue(T parameterValue, T[,] termScalarsList)
//    {
//        var m = BasisSet.Degree;
//        var value = ScalarProcessor.CreateScalarZero();

//        for (var i = 0; i <= m; i++)
//        for (var j = 0; j <= m; j++)
//        {
//            value += GetValue(i, j, parameterValue, termScalarsList[i, j]);
//        }

//        return value;
//    }

//    public T[,] GetValues(T parameterValue)
//    {
//        var n = BasisSet.Degree;
//        var valueArray = new T[n + 1, n + 1];

//        for (var i = 0; i <= n; i++)
//        for (var j = 0; j <= n; j++)
//        {
//            valueArray[i, j] = GetValue(i, j, parameterValue).ScalarValue;
//        }

//        return valueArray;
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public BSplineBasisPairProductIntegralSet<T> CreateIntegralSet()
//    {
//        return BSplineBasisPairProductIntegralSet<T>.Create(this);
//    }
//}