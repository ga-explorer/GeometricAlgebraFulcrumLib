using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.Polynomials.CurveBasis;

public sealed class MonomialBasisSet :
    IPolynomialBasisSet
{
    public int Degree { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MonomialBasisSet(int degree)
    {
        if (degree < 0)
            throw new ArgumentOutOfRangeException(nameof(degree));

        Degree = degree;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(int index, double parameterValue)
    {
        if (index < 0 || index > Degree)
            throw new ArgumentOutOfRangeException(nameof(index));

        return Math.Pow(parameterValue, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(int index, double parameterValue, double termScalar)
    {
        if (index < 0 || index > Degree)
            throw new ArgumentOutOfRangeException(nameof(index));

        return termScalar * Math.Pow(parameterValue, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(double parameterValue, params double[] termScalarsList)
    {
        return termScalarsList
            .Select((scalar, index) => scalar * Math.Pow(parameterValue, index))
            .Sum();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<double> GetValues(double parameterValue)
    {
        return Enumerable
            .Range(0, Degree + 1)
            .Select(index => Math.Pow(parameterValue, index))
            .ToArray();
    }
}