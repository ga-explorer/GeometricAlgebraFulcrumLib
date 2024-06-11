using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

public sealed class ScalarProcessorOfFloat32
    : INumericScalarProcessor<float>
{
    public static ScalarProcessorOfFloat32 Instance { get; }
        = new ScalarProcessorOfFloat32();

    
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

    public Scalar<float> Zero { get; }
    
    public Scalar<float> PositiveInfinity { get; }
    
    public Scalar<float> NegativeInfinity { get; }

    public Scalar<float> One { get; }
    
    public Scalar<float> MinusOne { get; }
    
    public Scalar<float> Two { get; }
    
    public Scalar<float> MinusTwo { get; }
    
    public Scalar<float> Ten { get; }
    
    public Scalar<float> MinusTen { get; }
    
    public Scalar<float> Pi { get; }
    
    public Scalar<float> PiTimes2 { get; }
    
    public Scalar<float> PiTimes4 { get; }

    public Scalar<float> PiOver2 { get; }
    
    public Scalar<float> E { get; }
    
    public Scalar<float> DegreeToRadianFactor { get; }
    
    public Scalar<float> RadianToDegreeFactor { get; }

    public float ZeroValue
        => 0f;

    public float PositiveInfinityValue 
        => float.PositiveInfinity;

    public float NegativeInfinityValue 
        => float.NegativeInfinity;

    public float OneValue
        => 1f;

    public float MinusOneValue
        => -1f;

    public float TwoValue
        => 2f;

    public float MinusTwoValue
        => -2f;

    public float TenValue
        => 10f;

    public float MinusTenValue
        => -10f;

    public float PiValue
        => MathF.PI;

    public float PiTimes2Value
        => 2f * MathF.PI;
    
    public float PiTimes4Value
        => 4f * MathF.PI;

    public float PiOver2Value
        => 0.5f * MathF.PI;

    public float EValue
        => MathF.E;

    public float DegreeToRadianFactorValue
        => MathF.PI / 180f;

    public float RadianToDegreeFactorValue
        => 180f / MathF.PI;


    private ScalarProcessorOfFloat32()
    {
        Zero = ScalarFromNumber(ZeroValue);
        One = ScalarFromNumber(OneValue);
        MinusOne = ScalarFromNumber(MinusOneValue);
        Two = ScalarFromNumber(TwoValue);
        MinusTwo = ScalarFromNumber(MinusTwoValue);
        Ten = ScalarFromNumber(TenValue);
        MinusTen = ScalarFromNumber(MinusTenValue);
        Pi = ScalarFromNumber(PiValue);
        E = ScalarFromNumber(EValue);
        PiTimes2 = ScalarFromNumber(PiTimes2Value);
        PiTimes4 = ScalarFromNumber(PiTimes4Value);
        PiOver2 = ScalarFromNumber(PiOver2Value);
        DegreeToRadianFactor = ScalarFromNumber(DegreeToRadianFactorValue);
        RadianToDegreeFactor = ScalarFromNumber(RadianToDegreeFactorValue);
        PositiveInfinity = ScalarFromNumber(PositiveInfinityValue);
        NegativeInfinity = ScalarFromNumber(NegativeInfinityValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Add(float scalar1, float scalar2)
    {
        return this.ScalarFromValue(scalar1 + scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Subtract(float scalar1, float scalar2)
    {
        return this.ScalarFromValue(scalar1 - scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Times(float scalar1, float scalar2)
    {
        return this.ScalarFromValue(scalar1 * scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Divide(float scalar1, float scalar2)
    {
        return this.ScalarFromValue(scalar1 / scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Positive(float scalar)
    {
        return this.ScalarFromValue(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Negative(float scalar)
    {
        return this.ScalarFromValue(-scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Inverse(float scalar)
    {
        return this.ScalarFromValue(1f / scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Sign(float scalar)
    {
        return this.ScalarFromValue(MathF.Sign(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> UnitStep(float scalar)
    {
        return this.ScalarFromValue(scalar < 0f ? 0f : 1f);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Abs(float scalar)
    {
        return this.ScalarFromValue(MathF.Abs(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Power(float baseScalar, float scalar)
    {
        return this.ScalarFromValue(MathF.Pow(baseScalar, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Sqrt(float scalar)
    {
        return this.ScalarFromValue(MathF.Sqrt(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> SqrtOfAbs(float scalar)
    {
        return this.ScalarFromValue(MathF.Sqrt(MathF.Abs(scalar)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Exp(float scalar)
    {
        return this.ScalarFromValue(MathF.Exp(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> LogE(float scalar)
    {
        return this.ScalarFromValue(MathF.Log(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Log2(float scalar)
    {
        return this.ScalarFromValue(MathF.Log2(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Log10(float scalar)
    {
        return this.ScalarFromValue(MathF.Log10(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Log(float baseScalar, float scalar)
    {
        return this.ScalarFromValue(MathF.Log(scalar, baseScalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Cos(float scalar)
    {
        return this.ScalarFromValue(MathF.Cos(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Sin(float scalar)
    {
        return this.ScalarFromValue(MathF.Sin(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Tan(float scalar)
    {
        return this.ScalarFromValue(MathF.Tan(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Cosh(float scalar)
    {
        return this.ScalarFromValue(MathF.Cosh(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Sinh(float scalar)
    {
        return this.ScalarFromValue(MathF.Sinh(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> Tanh(float scalar)
    {
        return this.ScalarFromValue(MathF.Tanh(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(float scalar)
    {
        return !float.IsNaN(scalar);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsFiniteNumber(float scalar)
    //{
    //    return float.IsFinite(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(float scalar)
    //{
    //    return scalar == 0f;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(float scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? scalar > -ZeroEpsilon && scalar < ZeroEpsilon
    //        : scalar == 0f;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNearZero(float scalar)
    //{
    //    return scalar > -ZeroEpsilon && scalar < ZeroEpsilon;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(float scalar)
    //{
    //    return scalar != 0f;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(float scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? scalar <= -ZeroEpsilon || scalar >= ZeroEpsilon
    //        : scalar != 0f;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearZero(float scalar)
    //{
    //    return scalar <= -ZeroEpsilon || scalar >= ZeroEpsilon;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsPositive(float scalar)
    //{
    //    return scalar > 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNegative(float scalar)
    //{
    //    return scalar < 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotPositive(float scalar)
    //{
    //    return scalar <= 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNegative(float scalar)
    //{
    //    return scalar >= 0;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearPositive(float scalar)
    //{
    //    return scalar < -ZeroEpsilon;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearNegative(float scalar)
    //{
    //    return scalar > ZeroEpsilon;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> ScalarFromText(string text)
    {
        return this.ScalarFromValue(float.Parse(text));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> ScalarFromNumber(int value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> ScalarFromNumber(uint value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> ScalarFromNumber(long value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> ScalarFromNumber(ulong value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> ScalarFromNumber(float value)
    {
        return this.ScalarFromValue(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> ScalarFromNumber(double value)
    {
        return this.ScalarFromValue((float)value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> ScalarFromRational(long numerator, long denominator)
    {
        return this.ScalarFromValue((float)(numerator / (double)denominator));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> ScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
    {
        return this.ScalarFromValue((float)(minValue + (maxValue - minValue) * randomGenerator.NextDouble()));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ToFloat64(float scalar)
    {
        return scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(float scalar)
    {
        return scalar.ToString("G");
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<float> VectorToRadians(float scalarX, float scalarY)
    {
        var value = Math.Atan2(scalarY, scalarX);

        if (value < 0) value += 2 * Math.PI;

        return ScalarFromNumber(value);
    }

}