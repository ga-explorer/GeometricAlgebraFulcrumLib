using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

/// <summary>
/// A scalar processor for PeterO.Numbers.EFloat numbers
/// https://github.com/peteroupc/Numbers
/// </summary>
public sealed class ScalarProcessorOfEFloat
    : INumericScalarProcessor<EFloat>
{
    public static ScalarProcessorOfEFloat Instance { get; }
        = new ScalarProcessorOfEFloat();


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

    public Scalar<EFloat> Zero { get; }
    
    public Scalar<EFloat> PositiveInfinity { get; }
    
    public Scalar<EFloat> NegativeInfinity { get; }

    public Scalar<EFloat> One { get; }
    
    public Scalar<EFloat> MinusOne { get; }
    
    public Scalar<EFloat> Two { get; }
    
    public Scalar<EFloat> MinusTwo { get; }
    
    public Scalar<EFloat> Ten { get; }
    
    public Scalar<EFloat> MinusTen { get; }
    
    public Scalar<EFloat> Pi { get; }
    
    public Scalar<EFloat> PiTimes2 { get; }
    
    public Scalar<EFloat> PiTimes4 { get; }

    public Scalar<EFloat> PiOver2 { get; }
    
    public Scalar<EFloat> E { get; }
    
    public Scalar<EFloat> DegreeToRadianFactor { get; }
    
    public Scalar<EFloat> RadianToDegreeFactor { get; }

    public EFloat ZeroValue
        => EFloat.Zero;

    public EFloat PositiveInfinityValue 
        => EFloat.PositiveInfinity;
    
    public EFloat NegativeInfinityValue 
        => EFloat.NegativeInfinity;

    public EFloat OneValue
        => EFloat.One;

    public EFloat MinusOneValue
        => -EFloat.One;

    public EFloat TwoValue
        => 2;

    public EFloat MinusTwoValue
        => -2;

    public EFloat TenValue
        => 10;

    public EFloat MinusTenValue
        => -10;

    public EFloat PiValue { get; }

    public EFloat PiTimes2Value { get; }
    
    public EFloat PiTimes4Value { get; }

    public EFloat PiOver2Value { get; }

    public EFloat EValue { get; }

    public EFloat DegreeToRadianFactorValue { get; }

    public EFloat RadianToDegreeFactorValue { get; }


    private ScalarProcessorOfEFloat()
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
    public Scalar<EFloat> Add(EFloat scalar1, EFloat scalar2)
    {
        return this.ScalarFromValue(scalar1 + scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Subtract(EFloat scalar1, EFloat scalar2)
    {
        return this.ScalarFromValue(scalar1 - scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Times(EFloat scalar1, EFloat scalar2)
    {
        return this.ScalarFromValue(scalar1 * scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Divide(EFloat scalar1, EFloat scalar2)
    {
        return this.ScalarFromValue(scalar1 / scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Positive(EFloat scalar)
    {
        return this.ScalarFromValue(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Negative(EFloat scalar)
    {
        return this.ScalarFromValue(-scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Inverse(EFloat scalar)
    {
        return this.ScalarFromValue(EFloat.One / scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Sign(EFloat scalar)
    {
        return this.ScalarFromValue(scalar.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> UnitStep(EFloat scalar)
    {
        return this.ScalarFromValue(scalar.IsNegative ? EFloat.Zero : EFloat.One);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Abs(EFloat scalar)
    {
        return this.ScalarFromValue(scalar.Abs());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Sqrt(EFloat scalar)
    {
        return this.ScalarFromValue(scalar.Sqrt(NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> SqrtOfAbs(EFloat scalar)
    {
        return this.ScalarFromValue(scalar.Abs().Sqrt(NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Exp(EFloat scalar)
    {
        return this.ScalarFromValue(scalar.Exp(NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> LogE(EFloat scalar)
    {
        return this.ScalarFromValue(scalar.Log(NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Log2(EFloat scalar)
    {
        return this.ScalarFromValue(scalar.LogN(2, NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Log10(EFloat scalar)
    {
        return this.ScalarFromValue(scalar.Log10(NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Power(EFloat baseScalar, EFloat scalar)
    {
        return this.ScalarFromValue(baseScalar.Pow(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Log(EFloat baseScalar, EFloat scalar)
    {
        return this.ScalarFromValue(scalar.LogN(baseScalar, NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Cos(EFloat scalar)
    {
        return this.ScalarFromValue(scalar.ToDouble().Cos());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Sin(EFloat scalar)
    {
        return this.ScalarFromValue(scalar.ToDouble().Sin());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Tan(EFloat scalar)
    {
        return this.ScalarFromValue(scalar.ToDouble().Tan());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Cosh(EFloat scalar)
    {
        return this.ScalarFromValue((scalar.Exp(NumericalContext) + (-scalar).Exp(NumericalContext)) / 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Sinh(EFloat scalar)
    {
        return this.ScalarFromValue((scalar.Exp(NumericalContext) - (-scalar).Exp(NumericalContext)) / 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> Tanh(EFloat scalar)
    {
        var sp = scalar.Exp(NumericalContext);
        var sn = (-scalar).Exp(NumericalContext);

        return this.ScalarFromValue((sp - sn) / (sp + sn));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(EFloat scalar)
    {
        return !scalar.IsNaN();
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsFiniteNumber(EFloat scalar)
    //{
    //    return scalar.IsFinite;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(EFloat scalar)
    //{
    //    return scalar.IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(EFloat scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? IsNearZero(scalar)
    //        : scalar.IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNearZero(EFloat scalar)
    //{
    //    //TODO: Correctly handle this case
    //    return scalar.IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(EFloat scalar)
    //{
    //    return !scalar.IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(EFloat scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? !IsNearZero(scalar)
    //        : !scalar.IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearZero(EFloat scalar)
    //{
    //    return !IsNearZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsPositive(EFloat scalar)
    //{
    //    return scalar.CompareToValue(0) > 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNegative(EFloat scalar)
    //{
    //    return scalar.CompareToValue(0) < 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotPositive(EFloat scalar)
    //{
    //    return scalar.CompareToValue(0) <= 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNegative(EFloat scalar)
    //{
    //    return scalar.CompareToValue(0) >= 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearPositive(EFloat scalar)
    //{
    //    return scalar.CompareToValue(0) < 0 && IsNotNearZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearNegative(EFloat scalar)
    //{
    //    return scalar.CompareToValue(0) > 0 && IsNotNearZero(scalar);
    //}
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> ScalarFromText(string text)
    {
        return this.ScalarFromValue(EFloat.FromString(text));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> ScalarFromNumber(int value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> ScalarFromNumber(uint value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> ScalarFromNumber(long value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> ScalarFromNumber(ulong value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> ScalarFromNumber(float value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> ScalarFromNumber(double value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> ScalarFromRational(long numerator, long denominator)
    {
        return this.ScalarFromValue((ERational.FromInt64(numerator) / ERational.FromInt64(denominator)).ToEFloatExactIfPossible(NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> ScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
    {
        return this.ScalarFromValue(minValue + (maxValue - minValue) * randomGenerator.NextDouble());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ToFloat64(EFloat scalar)
    {
        return scalar.ToDouble();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(EFloat scalar)
    {
        return scalar.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EFloat> VectorToRadians(EFloat scalarX, EFloat scalarY)
    {
        var value = EFloat.FromDouble(
            Math.Atan2(
                scalarY.ToDouble(), 
                scalarX.ToDouble()
            )
        );
        
        if (value.IsNegative) value += PiTimes2Value;

        return value.ScalarFromValue(this);
    }


}