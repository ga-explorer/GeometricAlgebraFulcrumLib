using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

/// <summary>
/// A scalar processor for PeterO.Numbers.EDecimal numbers
/// https://github.com/peteroupc/Numbers
/// </summary>
public sealed class ScalarProcessorOfEDecimal
    : INumericScalarProcessor<EDecimal>
{
    public static ScalarProcessorOfEDecimal Instance { get; }
        = new ScalarProcessorOfEDecimal();


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

    public Scalar<EDecimal> Zero { get; }

    public Scalar<EDecimal> PositiveInfinity { get; }
    
    public Scalar<EDecimal> NegativeInfinity { get; }

    public Scalar<EDecimal> One { get; }
    
    public Scalar<EDecimal> MinusOne { get; }
    
    public Scalar<EDecimal> Two { get; }
    
    public Scalar<EDecimal> MinusTwo { get; }
    
    public Scalar<EDecimal> Ten { get; }
    
    public Scalar<EDecimal> MinusTen { get; }
    
    public Scalar<EDecimal> Pi { get; }
    
    public Scalar<EDecimal> PiTimes2 { get; }
    
    public Scalar<EDecimal> PiTimes4 { get; }

    public Scalar<EDecimal> PiOver2 { get; }
    
    public Scalar<EDecimal> E { get; }
    
    public Scalar<EDecimal> DegreeToRadianFactor { get; }
    
    public Scalar<EDecimal> RadianToDegreeFactor { get; }

    public EDecimal ZeroValue
        => EDecimal.Zero;

    public EDecimal PositiveInfinityValue 
        => EDecimal.PositiveInfinity;

    public EDecimal NegativeInfinityValue 
        => EDecimal.NegativeInfinity;

    public EDecimal OneValue
        => EDecimal.One;

    public EDecimal MinusOneValue
        => -EDecimal.One;

    public EDecimal TwoValue
        => 2;

    public EDecimal MinusTwoValue
        => -2;

    public EDecimal TenValue
        => 10;

    public EDecimal MinusTenValue
        => -10;

    public EDecimal PiValue { get; }

    public EDecimal PiTimes2Value { get; }
    
    public EDecimal PiTimes4Value { get; }

    public EDecimal PiOver2Value { get; }

    public EDecimal EValue { get; }

    public EDecimal DegreeToRadianFactorValue { get; }

    public EDecimal RadianToDegreeFactorValue { get; }


    private ScalarProcessorOfEDecimal()
    {
        PiValue = EDecimal.PI(NumericalContext);
        PiTimes2Value = PiValue * 2;
        PiTimes4Value = PiValue * 4;
        PiOver2Value = PiValue / 2;
        EValue = EDecimal.One.Exp(NumericalContext);
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
    public Scalar<EDecimal> Add(EDecimal scalar1, EDecimal scalar2)
    {
        return this.ScalarFromValue(scalar1 + scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Subtract(EDecimal scalar1, EDecimal scalar2)
    {
        return this.ScalarFromValue(scalar1 - scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Times(EDecimal scalar1, EDecimal scalar2)
    {
        return this.ScalarFromValue(scalar1 * scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Divide(EDecimal scalar1, EDecimal scalar2)
    {
        return this.ScalarFromValue(scalar1 / scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Positive(EDecimal scalar)
    {
        return this.ScalarFromValue(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Negative(EDecimal scalar)
    {
        return this.ScalarFromValue(-scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Inverse(EDecimal scalar)
    {
        return this.ScalarFromValue(EDecimal.One / scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Sign(EDecimal scalar)
    {
        return this.ScalarFromValue(scalar.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> UnitStep(EDecimal scalar)
    {
        return this.ScalarFromValue(scalar.IsNegative ? EDecimal.Zero : EDecimal.One);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Abs(EDecimal scalar)
    {
        return this.ScalarFromValue(scalar.Abs());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Sqrt(EDecimal scalar)
    {
        return this.ScalarFromValue(scalar.Sqrt(NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> SqrtOfAbs(EDecimal scalar)
    {
        return this.ScalarFromValue(scalar.Abs().Sqrt(NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Exp(EDecimal scalar)
    {
        return this.ScalarFromValue(scalar.Exp(NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> LogE(EDecimal scalar)
    {
        return this.ScalarFromValue(scalar.Log(NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Log2(EDecimal scalar)
    {
        return this.ScalarFromValue(scalar.LogN(2, NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Log10(EDecimal scalar)
    {
        return this.ScalarFromValue(scalar.Log10(NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Power(EDecimal baseScalar, EDecimal scalar)
    {
        return this.ScalarFromValue(baseScalar.Pow(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Log(EDecimal baseScalar, EDecimal scalar)
    {
        return this.ScalarFromValue(scalar.LogN(baseScalar, NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Cos(EDecimal scalar)
    {
        return this.ScalarFromValue(EDecimal.FromDouble(scalar.ToDouble().Cos()));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Sin(EDecimal scalar)
    {
        return this.ScalarFromValue(EDecimal.FromDouble(scalar.ToDouble().Sin()));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Tan(EDecimal scalar)
    {
        return this.ScalarFromValue(EDecimal.FromDouble(scalar.ToDouble().Tan()));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Cosh(EDecimal scalar)
    {
        return this.ScalarFromValue((scalar.Exp(NumericalContext) + (-scalar).Exp(NumericalContext)) / 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Sinh(EDecimal scalar)
    {
        return this.ScalarFromValue((scalar.Exp(NumericalContext) - (-scalar).Exp(NumericalContext)) / 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> Tanh(EDecimal scalar)
    {
        var sp = scalar.Exp(NumericalContext);
        var sn = (-scalar).Exp(NumericalContext);

        return this.ScalarFromValue((sp - sn) / (sp + sn));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(EDecimal scalar)
    {
        return !scalar.IsNaN();
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsFiniteNumber(EDecimal scalar)
    //{
    //    return scalar.IsFinite;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(EDecimal scalar)
    //{
    //    return scalar.IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(EDecimal scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? IsNearZero(scalar)
    //        : scalar.IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNearZero(EDecimal scalar)
    //{
    //    //TODO: Correctly handle this case
    //    return scalar.IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(EDecimal scalar)
    //{
    //    return !scalar.IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(EDecimal scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? !IsNearZero(scalar)
    //        : !scalar.IsZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearZero(EDecimal scalar)
    //{
    //    return !IsNearZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsPositive(EDecimal scalar)
    //{
    //    return scalar.CompareToValue(0) > 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNegative(EDecimal scalar)
    //{
    //    return scalar.CompareToValue(0) < 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotPositive(EDecimal scalar)
    //{
    //    return scalar.CompareToValue(0) <= 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNegative(EDecimal scalar)
    //{
    //    return scalar.CompareToValue(0) >= 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearPositive(EDecimal scalar)
    //{
    //    return scalar.CompareToValue(0) < 0 && IsNotNearZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearNegative(EDecimal scalar)
    //{
    //    return scalar.CompareToValue(0) > 0 && IsNotNearZero(scalar);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> ScalarFromText(string text)
    {
        return this.ScalarFromValue(EDecimal.FromString(text));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> ScalarFromNumber(int value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> ScalarFromNumber(uint value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> ScalarFromNumber(long value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> ScalarFromNumber(ulong value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> ScalarFromNumber(float value)
    {
        return this.ScalarFromValue(EDecimal.FromSingle(value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> ScalarFromNumber(double value)
    {
        return this.ScalarFromValue(EDecimal.FromDouble(value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> ScalarFromRational(long numerator, long denominator)
    {
        return this.ScalarFromValue((ERational.FromInt64(numerator) / ERational.FromInt64(denominator)).ToEDecimalExactIfPossible(NumericalContext));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> ScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
    {
        return this.ScalarFromValue(EDecimal.FromDouble(minValue + (maxValue - minValue) * randomGenerator.NextDouble()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ToFloat64(EDecimal scalar)
    {
        return scalar.ToDouble();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(EDecimal scalar)
    {
        return scalar.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<EDecimal> VectorToRadians(EDecimal scalarX, EDecimal scalarY)
    {
        var value = EDecimal.FromDouble(
            Math.Atan2(
                scalarY.ToDouble(), 
                scalarX.ToDouble()
            )
        );
        
        if (value.IsNegative) value += PiTimes2Value;

        return value.ScalarFromValue(this);
    }
}