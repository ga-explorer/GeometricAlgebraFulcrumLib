using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.CurveBasis;

public abstract class GbBernsteinBasisSetBase :
    IPolynomialBasisSet
{
    public int Degree { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GbBernsteinBasisSetBase(int degree)
    {
        if (degree is < 2 or > 64)
            throw new ArgumentOutOfRangeException(nameof(degree));

        Degree = degree;
    }


    public abstract double GetValueDegree20(double parameterValue);

    public abstract double GetValueDegree22(double parameterValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValueDegree21(double parameterValue)
    {
        return 1d - (GetValueDegree20(parameterValue) + GetValueDegree22(parameterValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triplet<double> GetValueDegree2(double parameterValue)
    {
        var b02 = GetValueDegree20(parameterValue);
        var b22 = GetValueDegree22(parameterValue);
        var b12 = 1d - (b02 + b22);

        return new Triplet<double>(b02, b12, b22);
    }


    public double GetValue(int index, double parameterValue)
    {
        if (index < 0 || index > Degree)
            return 0d;

        if (Degree == 2)
            return index switch
            {
                0 => GetValueDegree20(parameterValue),
                2 => GetValueDegree22(parameterValue),
                _ => GetValueDegree21(parameterValue)
            };

        if (index == Degree)
            return Math.Pow(parameterValue, Degree - 2) *
                   GetValueDegree22(parameterValue);

        var oneMinusParameterValue = 1d - parameterValue;

        if (index == 0)
            return Math.Pow(oneMinusParameterValue, Degree - 2) *
                   GetValueDegree20(parameterValue);

        var (b02, b12, b22) = GetValueDegree2(parameterValue);

        if (index == Degree - 1)
            return Math.Pow(parameterValue, Degree - 2) *
                   b12
                   +
                   (Degree - 2) *
                   Math.Pow(parameterValue, Degree - 3) *
                   oneMinusParameterValue *
                   b22;

        if (index == 1)
            return (Degree - 2) *
                   parameterValue *
                   Math.Pow(oneMinusParameterValue, Degree - 3) *
                   b02
                   +
                   Math.Pow(oneMinusParameterValue, Degree - 2) *
                   b12;

        return (Degree - 2).GetBinomialCoefficient(index) *
               Math.Pow(parameterValue, index) *
               Math.Pow(oneMinusParameterValue, Degree - 2 - index) *
               b02
               +
               (Degree - 2).GetBinomialCoefficient(index - 1) *
               Math.Pow(parameterValue, index - 1) *
               Math.Pow(oneMinusParameterValue, Degree - 2 - (index - 1)) *
               b12
               +
               (Degree - 2).GetBinomialCoefficient(index - 2) *
               Math.Pow(parameterValue, index - 2) *
               Math.Pow(oneMinusParameterValue, Degree - 2 - (index - 2)) *
               b22;
    }

    private double GetValue(int index, double parameterValue, Triplet<double> degree2Values)
    {
        if (index < 0 || index > Degree)
            return 0d;

        if (Degree == 2)
            return index switch
            {
                0 => degree2Values.Item1,
                2 => degree2Values.Item3,
                _ => degree2Values.Item2
            };

        if (index == Degree)
            return Math.Pow(parameterValue, Degree - 2) * degree2Values.Item3;

        var oneMinusParameterValue = 1d - parameterValue;

        if (index == 0)
            return Math.Pow(oneMinusParameterValue, Degree - 2) * degree2Values.Item1;

        var (b02, b12, b22) = degree2Values;

        if (index == Degree - 1)
            return Math.Pow(parameterValue, Degree - 2) *
                   b12
                   +
                   (Degree - 2) *
                   Math.Pow(parameterValue, Degree - 3) *
                   oneMinusParameterValue *
                   b22;

        if (index == 1)
            return (Degree - 2) *
                   parameterValue *
                   Math.Pow(oneMinusParameterValue, Degree - 3) *
                   b02
                   +
                   Math.Pow(oneMinusParameterValue, Degree - 2) *
                   b12;

        return (Degree - 2).GetBinomialCoefficient(index) *
               Math.Pow(parameterValue, index) *
               Math.Pow(oneMinusParameterValue, Degree - 2 - index) *
               b02
               +
               (Degree - 2).GetBinomialCoefficient(index - 1) *
               Math.Pow(parameterValue, index - 1) *
               Math.Pow(oneMinusParameterValue, Degree - 2 - (index - 1)) *
               b12
               +
               (Degree - 2).GetBinomialCoefficient(index - 2) *
               Math.Pow(parameterValue, index - 2) *
               Math.Pow(oneMinusParameterValue, Degree - 2 - (index - 2)) *
               b22;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(int index, double parameterValue, double termScalar)
    {
        return termScalar * GetValue(index, parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetValue(double parameterValue, params double[] termScalarsList)
    {
        var degree2Values =
            GetValueDegree2(parameterValue);

        return termScalarsList.Select(
            (termScalar, index) =>
                termScalar * GetValue(index, parameterValue, degree2Values)
        ).Sum();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<double> GetValues(double parameterValue)
    {
        var degree2Values =
            GetValueDegree2(parameterValue);

        return Enumerable.Range(0, Degree + 1).Select(
            index => GetValue(index, parameterValue, degree2Values)
        ).ToArray();
    }
}