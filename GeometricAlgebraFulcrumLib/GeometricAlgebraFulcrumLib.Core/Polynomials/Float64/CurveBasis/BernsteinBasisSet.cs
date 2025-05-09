using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Structures.Combinations;

namespace GeometricAlgebraFulcrumLib.Core.Polynomials.Float64.CurveBasis;

public sealed class BernsteinBasisSet :
    IPolynomialBasisSet
{
    private static readonly Dictionary<int, BernsteinBasisSet> BasisSetCache
        = new Dictionary<int, BernsteinBasisSet>();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BernsteinBasisSet Create(int degree)
    {
        if (BasisSetCache.TryGetValue(degree, out var basisSet))
            return basisSet;

        basisSet = new BernsteinBasisSet(degree);

        BasisSetCache.Add(degree, basisSet);

        return basisSet;
    }


    public int Degree { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BernsteinBasisSet(int degree)
    {
        if (degree is < 0 or > 64)
            throw new ArgumentOutOfRangeException(nameof(degree));

        Degree = degree;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(int index, double parameterValue)
    {
        if (index < 0 || index > Degree)
            return 0d;

        var parameterValueMinusOne = 1 - parameterValue;

        return Degree.GetBinomialCoefficient(index) *
               Math.Pow(parameterValue, index) *
               Math.Pow(parameterValueMinusOne, Degree - index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(int index, double parameterValue, double termScalar)
    {
        return termScalar * GetValue(index, parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(double parameterValue, params double[] termScalarsList)
    {
        return termScalarsList.Select(
            (termScalar, index) =>
                GetValue(index, parameterValue, termScalar)
        ).Sum();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<double> GetValues(double parameterValue)
    {
        return Enumerable.Range(0, Degree + 1).Select(
            index => GetValue(index, parameterValue)
        ).ToArray();
    }
}