using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

/// <summary>
/// A scalar processor for PeterO.Numbers.ERational numbers
/// https://github.com/peteroupc/Numbers
/// </summary>
public sealed class ScalarProcessorOfERational
    : INumericScalarProcessor<ERational>
{
    public static ScalarProcessorOfERational Instance { get; }
        = new ScalarProcessorOfERational();


    public EContext NumericalContext { get; set; }
        = EContext.Binary128;
    
    private double _zeroEpsilon = 1e-12;
    public double ZeroEpsilon
    {
        get => _zeroEpsilon;
        set
        {
            if (value.IsNaN() || value.Abs() > 1)
                throw new ArgumentException(nameof(value));

            _zeroEpsilon = value.Abs();
        }
    }

    public bool IsNumeric
        => true;

    public bool IsSymbolic
        => false;

    public Scalar<ERational> Zero { get; }
    
    public Scalar<ERational> PositiveInfinity { get; }
    
    public Scalar<ERational> NegativeInfinity { get; }

    public Scalar<ERational> One { get; }
    
    public Scalar<ERational> MinusOne { get; }
    
    public Scalar<ERational> Two { get; }
    
    public Scalar<ERational> MinusTwo { get; }
    
    public Scalar<ERational> Ten { get; }
    
    public Scalar<ERational> MinusTen { get; }
    
    public Scalar<ERational> Pi { get; }
    
    public Scalar<ERational> PiTimes2 { get; }
    
    public Scalar<ERational> PiTimes4 { get; }

    public Scalar<ERational> PiOver2 { get; }
    
    public Scalar<ERational> E { get; }
    
    public Scalar<ERational> DegreeToRadianFactor { get; }
    
    public Scalar<ERational> RadianToDegreeFactor { get; }

    public ERational ZeroValue
        => ERational.Zero;

    public ERational PositiveInfinityValue 
        => ERational.PositiveInfinity;

    public ERational NegativeInfinityValue 
        => ERational.NegativeInfinity;

    public ERational OneValue
        => ERational.One;

    public ERational MinusOneValue
        => -ERational.One;

    public ERational TwoValue
        => 2;

    public ERational MinusTwoValue
        => -2;

    public ERational TenValue
        => 10;

    public ERational MinusTenValue
        => -10;

    public ERational PiValue { get; }

    public ERational PiTimes2Value { get; }
    
    public ERational PiTimes4Value { get; }

    public ERational PiOver2Value { get; }

    public ERational EValue { get; }

    public ERational DegreeToRadianFactorValue { get; }

    public ERational RadianToDegreeFactorValue { get; }


    private ScalarProcessorOfERational()
    {
        PiValue = EFloat.PI(NumericalContext);
        PiTimes2Value = PiValue * 2;
        PiTimes4Value = PiValue * 4;
        PiOver2Value = PiValue / 2;
        EValue = EFloat.One.Exp(NumericalContext);
        DegreeToRadianFactorValue = PiValue / 180;
        RadianToDegreeFactorValue = 180 / PiValue;

        Zero = this.ScalarFromValue(ZeroValue);
        One = this.ScalarFromValue(OneValue);
        MinusOne = this.ScalarFromValue(MinusOneValue);
        Two = this.ScalarFromValue(TwoValue);
        MinusTwo = this.ScalarFromValue(MinusTwoValue);
        Ten = this.ScalarFromValue(TenValue);
        MinusTen = this.ScalarFromValue(MinusTenValue);
        Pi = this.ScalarFromValue(PiValue);
        E = this.ScalarFromValue(EValue);
        PiTimes2 = this.ScalarFromValue(PiTimes2Value);
        PiTimes4 = this.ScalarFromValue(PiTimes4Value);
        PiOver2 = this.ScalarFromValue(PiOver2Value);
        DegreeToRadianFactor = this.ScalarFromValue(DegreeToRadianFactorValue);
        RadianToDegreeFactor = this.ScalarFromValue(RadianToDegreeFactorValue);
        PositiveInfinity = this.ScalarFromValue(PositiveInfinityValue);
        NegativeInfinity = this.ScalarFromValue(NegativeInfinityValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Add(ERational scalar1, ERational scalar2)
    {
        return this.ScalarFromValue(scalar1 + scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Subtract(ERational scalar1, ERational scalar2)
    {
        return this.ScalarFromValue(scalar1 - scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Times(ERational scalar1, ERational scalar2)
    {
        return this.ScalarFromValue(scalar1 * scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Divide(ERational scalar1, ERational scalar2)
    {
        return this.ScalarFromValue(scalar1 / scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Positive(ERational scalar)
    {
        return this.ScalarFromValue(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Negative(ERational scalar)
    {
        return this.ScalarFromValue(-scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Inverse(ERational scalar)
    {
        return this.ScalarFromValue(ERational.One / scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Sign(ERational scalar)
    {
        return this.ScalarFromValue(scalar.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> UnitStep(ERational scalar)
    {
        return this.ScalarFromValue(scalar.IsNegative ? ERational.Zero : ERational.One);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Abs(ERational scalar)
    {
        return this.ScalarFromValue(scalar.Abs());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Sqrt(ERational scalar)
    {
        return this.ScalarFromValue(scalar.ToEFloatExactIfPossible(NumericalContext).Sqrt(NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> SqrtOfAbs(ERational scalar)
    {
        return this.ScalarFromValue(scalar.Abs().ToEFloatExactIfPossible(NumericalContext).Sqrt(NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Exp(ERational scalar)
    {
        return this.ScalarFromValue(scalar.ToEFloatExactIfPossible(NumericalContext).Exp(NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> LogE(ERational scalar)
    {
        return this.ScalarFromValue(scalar.ToEFloatExactIfPossible(NumericalContext).Log(NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Log2(ERational scalar)
    {
        return this.ScalarFromValue(scalar.ToEFloatExactIfPossible(NumericalContext).LogN(2, NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Log10(ERational scalar)
    {
        return this.ScalarFromValue(scalar.ToEFloatExactIfPossible(NumericalContext).Log10(NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Power(ERational baseScalar, ERational scalar)
    {
        return this.ScalarFromValue(baseScalar.ToEFloatExactIfPossible(NumericalContext).Pow(scalar.ToEFloatExactIfPossible(NumericalContext)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Log(ERational baseScalar, ERational scalar)
    {
        return this.ScalarFromValue(scalar.ToEFloatExactIfPossible(NumericalContext).LogN(baseScalar.ToEFloatExactIfPossible(NumericalContext), NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Cos(ERational scalar)
    {
        return this.ScalarFromValue(scalar.ToDouble().Cos());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Sin(ERational scalar)
    {
        return this.ScalarFromValue(scalar.ToDouble().Sin());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Tan(ERational scalar)
    {
        return this.ScalarFromValue(scalar.ToDouble().Tan());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Cosh(ERational scalar)
    {
        var s = scalar.ToEFloatExactIfPossible(NumericalContext);

        return this.ScalarFromValue((s.Exp(NumericalContext) + (-s).Exp(NumericalContext)) / 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Sinh(ERational scalar)
    {
        var s = scalar.ToEFloatExactIfPossible(NumericalContext);

        return this.ScalarFromValue((s.Exp(NumericalContext) - (-s).Exp(NumericalContext)) / 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> Tanh(ERational scalar)
    {
        var s = scalar.ToEFloatExactIfPossible(NumericalContext);
        var sp = s.Exp(NumericalContext);
        var sn = (-s).Exp(NumericalContext);

        return this.ScalarFromValue((sp - sn) / (sp + sn));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(ERational scalar)
    {
        return !scalar.IsNaN();
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsFiniteNumber(ERational scalar)
    //{
    //    return scalar.IsFinite;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(ERational scalar)
    //{
    //    return scalar.IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(ERational scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? IsNearZero(scalar)
    //        : scalar.IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNearZero(ERational scalar)
    //{
    //    //TODO: Correctly handle this case
    //    return scalar.IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(ERational scalar)
    //{
    //    return !scalar.IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(ERational scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? !IsNearZero(scalar)
    //        : !scalar.IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearZero(ERational scalar)
    //{
    //    return !IsNearZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsPositive(ERational scalar)
    //{
    //    return scalar.CompareToValue(0) > 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNegative(ERational scalar)
    //{
    //    return scalar.CompareToValue(0) < 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotPositive(ERational scalar)
    //{
    //    return scalar.CompareToValue(0) <= 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNegative(ERational scalar)
    //{
    //    return scalar.CompareToValue(0) >= 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearPositive(ERational scalar)
    //{
    //    return scalar.CompareToValue(0) < 0 && IsNotNearZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearNegative(ERational scalar)
    //{
    //    return scalar.CompareToValue(0) > 0 && IsNotNearZero(scalar);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> ScalarFromText(string text)
    {
        return this.ScalarFromValue(ERational.FromString(text));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> ScalarFromNumber(int value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> ScalarFromNumber(uint value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> ScalarFromNumber(long value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> ScalarFromNumber(ulong value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> ScalarFromNumber(float value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> ScalarFromNumber(double value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> ScalarFromRational(long numerator, long denominator)
    {
        return this.ScalarFromValue(numerator / (ERational)denominator);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> ScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
    {
        return this.ScalarFromValue(minValue + (maxValue - minValue) * randomGenerator.NextDouble());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ToFloat64(ERational scalar)
    {
        return scalar.ToDouble();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(ERational scalar)
    {
        return scalar.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<ERational> VectorToRadians(ERational scalarX, ERational scalarY)
    {
        var value = ERational.FromDouble(
            Math.Atan2(
                scalarY.ToDouble(), 
                scalarX.ToDouble()
            )
        );
        
        if (value.IsNegative) value += PiTimes2Value;

        return value.ScalarFromValue(this);
    }


}