using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals;

public sealed class ScalarProcessorOfFloat64Signal :
    INumericScalarProcessor<Float64SampledTimeSignal>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarProcessorOfFloat64Signal Create(double samplingRate, int sampleCount)
    {
        return new ScalarProcessorOfFloat64Signal(samplingRate, sampleCount);
    }


    public int SampleCount { get; }

    public double SamplingRate { get; }

    public IScalarProcessor<double> ScalarProcessor
        => ScalarProcessorOfFloat64.Instance;

    public double ZeroEpsilon
    {
        get => ScalarProcessor.ZeroEpsilon;
        set => ScalarProcessor.ZeroEpsilon = value;
    }

    public bool IsNumeric
        => ScalarProcessor.IsNumeric;

    public bool IsSymbolic
        => ScalarProcessor.IsSymbolic;

    public Scalar<Float64SampledTimeSignal> Zero { get; }

    public Scalar<Float64SampledTimeSignal> PositiveInfinity { get; }

    public Scalar<Float64SampledTimeSignal> NegativeInfinity { get; }

    public Scalar<Float64SampledTimeSignal> One { get; }

    public Scalar<Float64SampledTimeSignal> MinusOne { get; }

    public Scalar<Float64SampledTimeSignal> Two { get; }

    public Scalar<Float64SampledTimeSignal> MinusTwo { get; }

    public Scalar<Float64SampledTimeSignal> Ten { get; }

    public Scalar<Float64SampledTimeSignal> MinusTen { get; }

    public Scalar<Float64SampledTimeSignal> Pi { get; }

    public Scalar<Float64SampledTimeSignal> PiTimes2 { get; }

    public Scalar<Float64SampledTimeSignal> PiTimes4 { get; }

    public Scalar<Float64SampledTimeSignal> PiOver2 { get; }

    public Scalar<Float64SampledTimeSignal> E { get; }

    public Scalar<Float64SampledTimeSignal> DegreeToRadianFactor { get; }

    public Scalar<Float64SampledTimeSignal> RadianToDegreeFactor { get; }

    public Float64SampledTimeSignal ZeroValue { get; }

    public Float64SampledTimeSignal PositiveInfinityValue { get; }

    public Float64SampledTimeSignal NegativeInfinityValue { get; }

    public Float64SampledTimeSignal OneValue { get; }

    public Float64SampledTimeSignal MinusOneValue { get; }

    public Float64SampledTimeSignal TwoValue { get; }

    public Float64SampledTimeSignal MinusTwoValue { get; }

    public Float64SampledTimeSignal TenValue { get; }

    public Float64SampledTimeSignal MinusTenValue { get; }

    public Float64SampledTimeSignal PiValue { get; }

    public Float64SampledTimeSignal PiTimes2Value { get; }

    public Float64SampledTimeSignal PiTimes4Value { get; }

    public Float64SampledTimeSignal PiOver2Value { get; }

    public Float64SampledTimeSignal EValue { get; }

    public Float64SampledTimeSignal DegreeToRadianFactorValue { get; }

    public Float64SampledTimeSignal RadianToDegreeFactorValue { get; }


    public ScalarProcessorOfFloat64Signal(double samplingRate, int signalSamplesCount)
    {
        if (signalSamplesCount < 1)
            throw new ArgumentOutOfRangeException(nameof(signalSamplesCount));

        if (samplingRate <= 0)
            throw new ArgumentOutOfRangeException(nameof(samplingRate));

        SamplingRate = samplingRate;
        SampleCount = signalSamplesCount;

        ZeroValue = GetReadOnlyScalarFromNumber(ScalarProcessor.ZeroValue);
        OneValue = GetReadOnlyScalarFromNumber(ScalarProcessor.OneValue);
        MinusOneValue = GetReadOnlyScalarFromNumber(ScalarProcessor.MinusOneValue);
        TwoValue = GetReadOnlyScalarFromNumber(ScalarProcessor.TwoValue);
        MinusTwoValue = GetReadOnlyScalarFromNumber(ScalarProcessor.MinusTwoValue);
        TenValue = GetReadOnlyScalarFromNumber(ScalarProcessor.TenValue);
        MinusTenValue = GetReadOnlyScalarFromNumber(ScalarProcessor.MinusTenValue);
        PiValue = GetReadOnlyScalarFromNumber(ScalarProcessor.PiValue);
        PiTimes2Value = GetReadOnlyScalarFromNumber(ScalarProcessor.PiTimes2Value);
        PiTimes4Value = GetReadOnlyScalarFromNumber(ScalarProcessor.PiTimes4Value);
        PiOver2Value = GetReadOnlyScalarFromNumber(ScalarProcessor.PiOver2Value);
        EValue = GetReadOnlyScalarFromNumber(ScalarProcessor.EValue);
        DegreeToRadianFactorValue = GetReadOnlyScalarFromNumber(ScalarProcessor.DegreeToRadianFactorValue);
        RadianToDegreeFactorValue = GetReadOnlyScalarFromNumber(ScalarProcessor.RadianToDegreeFactorValue);
        PositiveInfinityValue = GetReadOnlyScalarFromNumber(ScalarProcessor.PositiveInfinityValue);
        NegativeInfinityValue = GetReadOnlyScalarFromNumber(ScalarProcessor.NegativeInfinityValue);

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


    public Float64SampledTimeSignal GetReadOnlyScalarFromNumber(double value)
    {
        return Float64SampledTimeSignal.FiniteConstant(
            SamplingRate,
            SampleCount,
            value
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> ScalarFromNumber(int value)
    {
        return Float64SampledTimeSignal.FiniteConstant(
            SamplingRate,
            SampleCount,
            value
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> ScalarFromNumber(uint value)
    {
        return Float64SampledTimeSignal.FiniteConstant(
            SamplingRate,
            SampleCount,
            value
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> ScalarFromNumber(long value)
    {
        return Float64SampledTimeSignal.FiniteConstant(
            SamplingRate,
            SampleCount,
            value
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> ScalarFromNumber(ulong value)
    {
        return Float64SampledTimeSignal.FiniteConstant(
            SamplingRate,
            SampleCount,
            value
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> ScalarFromNumber(float value)
    {
        return Float64SampledTimeSignal.FiniteConstant(
            SamplingRate,
            SampleCount,
            value
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> ScalarFromNumber(double value)
    {
        return Float64SampledTimeSignal.FiniteConstant(
            SamplingRate,
            SampleCount,
            value
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> ScalarFromRational(long numerator, long denominator)
    {
        return Float64SampledTimeSignal.FiniteConstant(
            SamplingRate,
            SampleCount,
            numerator / (double)denominator
        ).ScalarFromValue(this);
    }

    public Scalar<Float64SampledTimeSignal> ScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
    {
        var scalarArray = new double[SampleCount];

        for (var i = 0; i < SampleCount; i++)
        {
            scalarArray[i] = ScalarProcessor.ScalarFromRandom(
                randomGenerator,
                minValue,
                maxValue
            ).ScalarValue;
        }

        return Float64SampledTimeSignal.Create(SamplingRate, scalarArray, false).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> ScalarFromText(string text)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Add(Float64SampledTimeSignal scalar1, Float64SampledTimeSignal scalar2)
    {
        return scalar1.MapSamples(
            scalar2,
            (a, b) => a + b
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Subtract(Float64SampledTimeSignal scalar1, Float64SampledTimeSignal scalar2)
    {
        return scalar1.MapSamples(scalar2, (a, b) => a - b).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Times(Float64SampledTimeSignal scalar1, Float64SampledTimeSignal scalar2)
    {
        return scalar1.MapSamples(
            scalar2,
            (s1, s2) => s1 * s2
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Divide(Float64SampledTimeSignal scalar1, Float64SampledTimeSignal scalar2)
    {
        return scalar1.MapSamples(scalar2, (a, b) => a / b).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> VectorToRadians(Float64SampledTimeSignal scalarX, Float64SampledTimeSignal scalarY)
    {
        return scalarX.MapSamples(scalarY, (a, b) => ScalarProcessor.VectorToRadians(a, b).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Positive(Float64SampledTimeSignal scalar)
    {
        return scalar.ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Negative(Float64SampledTimeSignal scalar)
    {
        return scalar.MapSamples(scalar1 => -scalar1).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Inverse(Float64SampledTimeSignal scalar)
    {
        return scalar.MapSamples(scalar1 => 1 / scalar1).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Sign(Float64SampledTimeSignal scalar)
    {
        return scalar.MapSamples(
            s => ScalarProcessor.Sign(s).ScalarValue
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> UnitStep(Float64SampledTimeSignal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.UnitStep(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Abs(Float64SampledTimeSignal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Abs(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Power(Float64SampledTimeSignal baseScalar, Float64SampledTimeSignal scalar)
    {
        return baseScalar.MapSamples(scalar, (baseScalar1, scalar1) => ScalarProcessor.Power(baseScalar1, scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Sqrt(Float64SampledTimeSignal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Sqrt(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> SqrtOfAbs(Float64SampledTimeSignal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.SqrtOfAbs(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Exp(Float64SampledTimeSignal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Exp(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> LogE(Float64SampledTimeSignal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.LogE(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Log2(Float64SampledTimeSignal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Log2(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Log10(Float64SampledTimeSignal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Log10(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Log(Float64SampledTimeSignal baseScalar, Float64SampledTimeSignal scalar)
    {
        return baseScalar.MapSamples(scalar, (baseScalar1, scalar1) => ScalarProcessor.Log(baseScalar1, scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Cos(Float64SampledTimeSignal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Cos(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Sin(Float64SampledTimeSignal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Sin(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Tan(Float64SampledTimeSignal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Tan(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Cosh(Float64SampledTimeSignal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Cosh(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Sinh(Float64SampledTimeSignal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Sinh(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64SampledTimeSignal> Tanh(Float64SampledTimeSignal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Tanh(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(Float64SampledTimeSignal scalar)
    {
        return scalar.All(ScalarProcessor.IsValid);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsFiniteNumber(Float64Signal scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsFiniteNumber);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(Float64Signal scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsZero);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(Float64Signal scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? IsNearZero(scalar) 
    //        : IsZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNearZero(Float64Signal scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsNearZero);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(Float64Signal scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsNotZero);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(Float64Signal scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? IsNotNearZero(scalar) 
    //        : IsNotZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearZero(Float64Signal scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsNotNearZero);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsPositive(Float64Signal scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsPositive);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNegative(Float64Signal scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsNegative);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotPositive(Float64Signal scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsNotPositive);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNegative(Float64Signal scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsNotNegative);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearPositive(Float64Signal scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsNotNearPositive);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearNegative(Float64Signal scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsNotNearNegative);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ToFloat64(Float64SampledTimeSignal scalar)
    {
        return double.NaN;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(Float64SampledTimeSignal scalar)
    {
        return scalar
            .Select(s => s.ToString("G"))
            .Concatenate(", ", "{", "}");
    }
}