using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

public abstract class ScalarProcessorContainer<T> :
    IScalarProcessor<T>
{
    public IScalarProcessor<T> ScalarProcessor { get; }

    public bool IsNumeric
        => ScalarProcessor.IsNumeric;

    public bool IsSymbolic
        => ScalarProcessor.IsSymbolic;

    public T ScalarZero
        => ScalarProcessor.ScalarZero;

    public T ScalarOne
        => ScalarProcessor.ScalarOne;

    public T ScalarMinusOne
        => ScalarProcessor.ScalarMinusOne;

    public T ScalarTwo
        => ScalarProcessor.ScalarTwo;

    public T ScalarMinusTwo
        => ScalarProcessor.ScalarMinusTwo;

    public T ScalarTen
        => ScalarProcessor.ScalarTen;

    public T ScalarMinusTen
        => ScalarProcessor.ScalarMinusTen;

    public T ScalarPi
        => ScalarProcessor.ScalarPi;

    public T ScalarTwoPi
        => ScalarProcessor.ScalarTwoPi;

    public T ScalarPiOver2
        => ScalarProcessor.ScalarPiOver2;

    public T ScalarE
        => ScalarProcessor.ScalarE;

    public T ScalarDegreeToRadian
        => ScalarProcessor.ScalarDegreeToRadian;

    public T ScalarRadianToDegree
        => ScalarProcessor.ScalarRadianToDegree;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected ScalarProcessorContainer(IScalarProcessor<T> scalarProcessor)
    {
        ScalarProcessor = scalarProcessor;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Add(T scalar1, T scalar2)
    {
        return ScalarProcessor.Add(scalar1, scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Subtract(T scalar1, T scalar2)
    {
        return ScalarProcessor.Subtract(scalar1, scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Times(T scalar1, T scalar2)
    {
        return ScalarProcessor.Times(scalar1, scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Times(IntegerSign sign, T scalar)
    {
        if (sign.IsZero) return ScalarZero;

        return sign.IsPositive
            ? scalar
            : ScalarProcessor.Negative(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T NegativeTimes(T scalar1, T scalar2)
    {
        return ScalarProcessor.NegativeTimes(scalar1, scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Divide(T scalar1, T scalar2)
    {
        return ScalarProcessor.Divide(scalar1, scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T NegativeDivide(T scalar1, T scalar2)
    {
        return ScalarProcessor.NegativeDivide(scalar1, scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Positive(T scalar)
    {
        return ScalarProcessor.Positive(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Negative(T scalar)
    {
        return ScalarProcessor.Negative(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Inverse(T scalar)
    {
        return ScalarProcessor.Inverse(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Sign(T scalar)
    {
        return ScalarProcessor.Sign(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T UnitStep(T scalar)
    {
        return ScalarProcessor.UnitStep(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Abs(T scalar)
    {
        return ScalarProcessor.Abs(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Sqrt(T scalar)
    {
        return ScalarProcessor.Sqrt(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T SqrtOfAbs(T scalar)
    {
        return ScalarProcessor.SqrtOfAbs(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Exp(T scalar)
    {
        return ScalarProcessor.Exp(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T LogE(T scalar)
    {
        return ScalarProcessor.LogE(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Log2(T scalar)
    {
        return ScalarProcessor.Log2(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Log10(T scalar)
    {
        return ScalarProcessor.Log10(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Power(T baseScalar, T scalar)
    {
        return ScalarProcessor.Power(baseScalar, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Log(T baseScalar, T scalar)
    {
        return ScalarProcessor.Log(baseScalar, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Cos(T scalar)
    {
        return ScalarProcessor.Cos(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Sin(T scalar)
    {
        return ScalarProcessor.Sin(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Tan(T scalar)
    {
        return ScalarProcessor.Tan(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T ArcCos(T scalar)
    {
        return ScalarProcessor.ArcCos(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T ArcSin(T scalar)
    {
        return ScalarProcessor.ArcSin(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T ArcTan(T scalar)
    {
        return ScalarProcessor.ArcTan(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T ArcTan2(T scalarX, T scalarY)
    {
        return ScalarProcessor.ArcTan2(scalarX, scalarY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Cosh(T scalar)
    {
        return ScalarProcessor.Cosh(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Sinh(T scalar)
    {
        return ScalarProcessor.Sinh(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Tanh(T scalar)
    {
        return ScalarProcessor.Tanh(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Sinc(T scalar)
    {
        return ScalarProcessor.Sinc(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(T scalar)
    {
        return ScalarProcessor.IsValid(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsFiniteNumber(T scalar)
    {
        return ScalarProcessor.IsFiniteNumber(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero(T scalar)
    {
        return ScalarProcessor.IsZero(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero(T scalar, bool nearZeroFlag)
    {
        return ScalarProcessor.IsZero(scalar, nearZeroFlag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(T scalar)
    {
        return ScalarProcessor.IsNearZero(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotZero(T scalar)
    {
        return !ScalarProcessor.IsZero(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotZero(T scalar, bool nearZeroFlag)
    {
        return !ScalarProcessor.IsZero(scalar, nearZeroFlag);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNearZero(T scalar)
    {
        return !ScalarProcessor.IsNearZero(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsPositive(T scalar)
    {
        return ScalarProcessor.IsPositive(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNegative(T scalar)
    {
        return ScalarProcessor.IsNegative(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotPositive(T scalar)
    {
        return !ScalarProcessor.IsPositive(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNegative(T scalar)
    {
        return !ScalarProcessor.IsNegative(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNearPositive(T scalar)
    {
        return ScalarProcessor.IsNotNearPositive(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNotNearNegative(T scalar)
    {
        return ScalarProcessor.IsNotNearNegative(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalarFromText(string text)
    {
        return ScalarProcessor.GetScalarFromText(text);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalarFromNumber(int value)
    {
        return ScalarProcessor.GetScalarFromNumber(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalarFromNumber(uint value)
    {
        return ScalarProcessor.GetScalarFromNumber(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalarFromNumber(long value)
    {
        return ScalarProcessor.GetScalarFromNumber(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalarFromNumber(ulong value)
    {
        return ScalarProcessor.GetScalarFromNumber(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalarFromNumber(float value)
    {
        return ScalarProcessor.GetScalarFromNumber(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalarFromNumber(double value)
    {
        return ScalarProcessor.GetScalarFromNumber(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalarFromRational(long numerator, long denominator)
    {
        return ScalarProcessor.GetScalarFromRational(numerator, denominator);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalarFromRandom(System.Random randomGenerator, double minValue, double maxValue)
    {
        return ScalarProcessor.GetScalarFromRandom(randomGenerator, minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(T scalar)
    {
        return ScalarProcessor.ToText(scalar);
    }
}