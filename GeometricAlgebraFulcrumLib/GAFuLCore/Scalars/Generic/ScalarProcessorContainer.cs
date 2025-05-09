using System;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

public abstract class ScalarProcessorContainer<T> :
    IScalarProcessor<T>
{
    public IScalarProcessor<T> ScalarProcessor { get; }

    public double ZeroEpsilon
    {
        get => ScalarProcessor.ZeroEpsilon; 
        set => ScalarProcessor.ZeroEpsilon = value;
    }

    public bool IsNumeric
        => ScalarProcessor.IsNumeric;

    public bool IsSymbolic
        => ScalarProcessor.IsSymbolic;

    public Scalar<T> Zero => ScalarProcessor.Zero;
    
    public Scalar<T> PositiveInfinity => ScalarProcessor.PositiveInfinity;
    
    public Scalar<T> NegativeInfinity => ScalarProcessor.NegativeInfinity;

    public Scalar<T> One => ScalarProcessor.One;
    
    public Scalar<T> MinusOne => ScalarProcessor.MinusOne;
    
    public Scalar<T> Two => ScalarProcessor.Two;
    
    public Scalar<T> MinusTwo => ScalarProcessor.MinusTwo;
    
    public Scalar<T> Ten => ScalarProcessor.Ten;
    
    public Scalar<T> MinusTen => ScalarProcessor.MinusTen;
    
    public Scalar<T> Pi => ScalarProcessor.Pi;
    
    public Scalar<T> PiTimes2 => ScalarProcessor.PiTimes2;
    
    public Scalar<T> PiTimes4 => ScalarProcessor.PiTimes4;

    public Scalar<T> PiOver2 => ScalarProcessor.PiOver2;
    
    public Scalar<T> E => ScalarProcessor.E;
    
    public Scalar<T> DegreeToRadianFactor => ScalarProcessor.DegreeToRadianFactor;
    
    public Scalar<T> RadianToDegreeFactor => ScalarProcessor.RadianToDegreeFactor;

    public T ZeroValue
        => ScalarProcessor.ZeroValue;

    public T PositiveInfinityValue 
        => ScalarProcessor.PositiveInfinityValue;

    public T NegativeInfinityValue 
        => ScalarProcessor.NegativeInfinityValue;

    public T OneValue
        => ScalarProcessor.OneValue;

    public T MinusOneValue
        => ScalarProcessor.MinusOneValue;

    public T TwoValue
        => ScalarProcessor.TwoValue;

    public T MinusTwoValue
        => ScalarProcessor.MinusTwoValue;

    public T TenValue
        => ScalarProcessor.TenValue;

    public T MinusTenValue
        => ScalarProcessor.MinusTenValue;

    public T PiValue
        => ScalarProcessor.PiValue;

    public T PiTimes2Value
        => ScalarProcessor.PiTimes2Value;
    
    public T PiTimes4Value
        => ScalarProcessor.PiTimes4Value;

    public T PiOver2Value
        => ScalarProcessor.PiOver2Value;

    public T EValue
        => ScalarProcessor.EValue;

    public T DegreeToRadianFactorValue
        => ScalarProcessor.DegreeToRadianFactorValue;

    public T RadianToDegreeFactorValue
        => ScalarProcessor.RadianToDegreeFactorValue;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected ScalarProcessorContainer(IScalarProcessor<T> scalarProcessor)
    {
        ScalarProcessor = scalarProcessor;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Add(T scalar1, T scalar2)
    {
        return ScalarProcessor.Add(scalar1, scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Subtract(T scalar1, T scalar2)
    {
        return ScalarProcessor.Subtract(scalar1, scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Times(T scalar1, T scalar2)
    {
        return ScalarProcessor.Times(scalar1, scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Divide(T scalar1, T scalar2)
    {
        return ScalarProcessor.Divide(scalar1, scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> VectorToRadians(T scalarX, T scalarY)
    {
        return ScalarProcessor.VectorToRadians(scalarX, scalarY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Positive(T scalar)
    {
        return ScalarProcessor.Positive(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Negative(T scalar)
    {
        return ScalarProcessor.Negative(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Inverse(T scalar)
    {
        return ScalarProcessor.Inverse(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Sign(T scalar)
    {
        return ScalarProcessor.Sign(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> UnitStep(T scalar)
    {
        return ScalarProcessor.UnitStep(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Abs(T scalar)
    {
        return ScalarProcessor.Abs(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Sqrt(T scalar)
    {
        return ScalarProcessor.Sqrt(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> SqrtOfAbs(T scalar)
    {
        return ScalarProcessor.SqrtOfAbs(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Exp(T scalar)
    {
        return ScalarProcessor.Exp(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> LogE(T scalar)
    {
        return ScalarProcessor.LogE(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Log2(T scalar)
    {
        return ScalarProcessor.Log2(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Log10(T scalar)
    {
        return ScalarProcessor.Log10(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Power(T baseScalar, T scalar)
    {
        return ScalarProcessor.Power(baseScalar, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Log(T baseScalar, T scalar)
    {
        return ScalarProcessor.Log(baseScalar, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Cos(T scalar)
    {
        return ScalarProcessor.Cos(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Sin(T scalar)
    {
        return ScalarProcessor.Sin(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Tan(T scalar)
    {
        return ScalarProcessor.Tan(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Cosh(T scalar)
    {
        return ScalarProcessor.Cosh(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Sinh(T scalar)
    {
        return ScalarProcessor.Sinh(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Tanh(T scalar)
    {
        return ScalarProcessor.Tanh(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(T scalar)
    {
        return ScalarProcessor.IsValid(scalar);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsFiniteNumber(T scalar)
    //{
    //    return ScalarProcessor.IsFiniteNumber(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(T scalar)
    //{
    //    return ScalarProcessor.IsZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(T scalar, bool nearZeroFlag)
    //{
    //    return ScalarProcessor.IsZero(scalar, nearZeroFlag);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNearZero(T scalar)
    //{
    //    return ScalarProcessor.IsNearZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(T scalar)
    //{
    //    return !ScalarProcessor.IsZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(T scalar, bool nearZeroFlag)
    //{
    //    return !ScalarProcessor.IsZero(scalar, nearZeroFlag);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearZero(T scalar)
    //{
    //    return !ScalarProcessor.IsNearZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsPositive(T scalar)
    //{
    //    return ScalarProcessor.IsPositive(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNegative(T scalar)
    //{
    //    return ScalarProcessor.IsNegative(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotPositive(T scalar)
    //{
    //    return !ScalarProcessor.IsPositive(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNegative(T scalar)
    //{
    //    return !ScalarProcessor.IsNegative(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearPositive(T scalar)
    //{
    //    return ScalarProcessor.IsNotNearPositive(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearNegative(T scalar)
    //{
    //    return ScalarProcessor.IsNotNearNegative(scalar);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ScalarFromText(string text)
    {
        return ScalarProcessor.ScalarFromText(text);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ScalarFromNumber(int value)
    {
        return ScalarProcessor.ScalarFromNumber(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ScalarFromNumber(uint value)
    {
        return ScalarProcessor.ScalarFromNumber(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ScalarFromNumber(long value)
    {
        return ScalarProcessor.ScalarFromNumber(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ScalarFromNumber(ulong value)
    {
        return ScalarProcessor.ScalarFromNumber(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ScalarFromNumber(float value)
    {
        return ScalarProcessor.ScalarFromNumber(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ScalarFromNumber(double value)
    {
        return ScalarProcessor.ScalarFromNumber(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ScalarFromRational(long numerator, long denominator)
    {
        return ScalarProcessor.ScalarFromRational(numerator, denominator);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
    {
        return ScalarProcessor.ScalarFromRandom(randomGenerator, minValue, maxValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ToFloat64(T scalar)
    {
        return ScalarProcessor.ToFloat64(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(T scalar)
    {
        return ScalarProcessor.ToText(scalar);
    }
}