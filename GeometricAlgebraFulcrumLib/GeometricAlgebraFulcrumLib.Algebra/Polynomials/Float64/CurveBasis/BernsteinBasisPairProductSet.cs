using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.CurveBasis;

/// <summary>
/// A polynomial in this set is the product of two Bernstein polynomials
/// from half degree basis set
/// </summary>
public sealed class BernsteinBasisPairProductSet :
    IPolynomialPairProductSet
{
    private static readonly Dictionary<int, BernsteinBasisPairProductSet> BasisSetCache
        = new Dictionary<int, BernsteinBasisPairProductSet>();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BernsteinBasisPairProductSet Create(BernsteinBasisSet bernsteinBasisSet)
    {
        var n2 = 2 * bernsteinBasisSet.Degree;

        if (BasisSetCache.TryGetValue(n2, out var basisSet))
            return basisSet;

        basisSet = new BernsteinBasisPairProductSet(bernsteinBasisSet);

        BasisSetCache.Add(n2, basisSet);

        return basisSet;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BernsteinBasisPairProductSet Create(int degree)
    {
        var n2 = 2 * degree;

        if (BasisSetCache.TryGetValue(n2, out var basisSet))
            return basisSet;

        var bernsteinBasisSet = BernsteinBasisSet.Create(degree);
        basisSet = new BernsteinBasisPairProductSet(bernsteinBasisSet);

        BasisSetCache.Add(n2, basisSet);

        return basisSet;
    }


    private readonly ulong _binomialConstantsDenominator;
    private readonly ulong[,] _binomialConstantsNumerators;
    private readonly BernsteinBasisSet _bernsteinBasisSet2;


    public BernsteinBasisSet BasisSet { get; }

    public int Degree
        => 2 * BasisSet.Degree;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BernsteinBasisPairProductSet(BernsteinBasisSet bernsteinBasisSet)
    {
        var degree = bernsteinBasisSet.Degree;
        var n2 = 2 * degree;

        BasisSet = bernsteinBasisSet;
        _bernsteinBasisSet2 = BernsteinBasisSet.Create(n2);
        _binomialConstantsNumerators = new ulong[degree + 1, degree + 1];
        _binomialConstantsDenominator = n2.GetMaxBinomialCoefficient();

        FillBinomialConstants();
    }


    private void FillBinomialConstants()
    {
        var degree = BasisSet.Degree;
        var n2 = 2 * degree;

        for (var i = 0; i <= degree; i++)
        {
            var nCi = degree.GetBinomialCoefficient(i);

            _binomialConstantsNumerators[i, i] =
                _binomialConstantsDenominator * nCi * nCi /
                n2.GetBinomialCoefficient(2 * i);

            for (var j = i + 1; j <= degree; j++)
            {
                var nCj = degree.GetBinomialCoefficient(j);

                var binomialConstantNumerator =
                    _binomialConstantsDenominator * nCi * nCj /
                    n2.GetBinomialCoefficient(i + j);

                _binomialConstantsNumerators[i, j] = binomialConstantNumerator;
                _binomialConstantsNumerators[j, i] = binomialConstantNumerator;
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetBinomialConstant(int index1, int index2)
    {
        return
            _binomialConstantsNumerators[index1, index2] /
            (double)_binomialConstantsDenominator;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(int index1, int index2, double parameterValue)
    {
        return _bernsteinBasisSet2.GetValue(
            index1 + index2,
            parameterValue,
            GetBinomialConstant(index1, index2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(int index1, int index2, double parameterValue, double termScalar)
    {
        return termScalar * GetValue(index1, index2, parameterValue);
    }

    public double GetValue(double parameterValue, double[,] termScalarsList)
    {
        var m = BasisSet.Degree;
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
        var n = BasisSet.Degree;
        var valueArray = new double[n + 1, n + 1];

        for (var i = 0; i <= n; i++)
            for (var j = 0; j <= n; j++)
            {
                valueArray[i, j] = GetValue(i, j, parameterValue);
            }

        return valueArray;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BernsteinBasisPairProductIntegralSet CreateIntegralSet()
    {
        return BernsteinBasisPairProductIntegralSet.Create(this);
    }
}