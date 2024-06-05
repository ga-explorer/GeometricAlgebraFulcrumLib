using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.Signals;

public sealed class ScalarProcessorOfFloat64Signal :
    INumericScalarProcessor<Float64Signal>
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

    public Scalar<Float64Signal> Zero { get; }

    public Scalar<Float64Signal> PositiveInfinity { get; }

    public Scalar<Float64Signal> NegativeInfinity { get; }

    public Scalar<Float64Signal> One { get; }

    public Scalar<Float64Signal> MinusOne { get; }

    public Scalar<Float64Signal> Two { get; }

    public Scalar<Float64Signal> MinusTwo { get; }

    public Scalar<Float64Signal> Ten { get; }

    public Scalar<Float64Signal> MinusTen { get; }

    public Scalar<Float64Signal> Pi { get; }

    public Scalar<Float64Signal> PiTimes2 { get; }

    public Scalar<Float64Signal> PiTimes4 { get; }

    public Scalar<Float64Signal> PiOver2 { get; }

    public Scalar<Float64Signal> E { get; }

    public Scalar<Float64Signal> DegreeToRadianFactor { get; }

    public Scalar<Float64Signal> RadianToDegreeFactor { get; }

    public Float64Signal ZeroValue { get; }

    public Float64Signal PositiveInfinityValue { get; }

    public Float64Signal NegativeInfinityValue { get; }

    public Float64Signal OneValue { get; }

    public Float64Signal MinusOneValue { get; }

    public Float64Signal TwoValue { get; }

    public Float64Signal MinusTwoValue { get; }

    public Float64Signal TenValue { get; }

    public Float64Signal MinusTenValue { get; }

    public Float64Signal PiValue { get; }

    public Float64Signal PiTimes2Value { get; }

    public Float64Signal PiTimes4Value { get; }

    public Float64Signal PiOver2Value { get; }

    public Float64Signal EValue { get; }

    public Float64Signal DegreeToRadianFactorValue { get; }

    public Float64Signal RadianToDegreeFactorValue { get; }


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


    public Float64Signal GetReadOnlyScalarFromNumber(double value)
    {
        return Float64Signal.CreateConstant(
            SamplingRate,
            SampleCount,
            value,
            true
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> ScalarFromNumber(int value)
    {
        return Float64Signal.CreateConstant(
            SamplingRate,
            SampleCount,
            value,
            false
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> ScalarFromNumber(uint value)
    {
        return Float64Signal.CreateConstant(
            SamplingRate,
            SampleCount,
            value,
            false
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> ScalarFromNumber(long value)
    {
        return Float64Signal.CreateConstant(
            SamplingRate,
            SampleCount,
            value,
            false
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> ScalarFromNumber(ulong value)
    {
        return Float64Signal.CreateConstant(
            SamplingRate,
            SampleCount,
            value,
            false
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> ScalarFromNumber(float value)
    {
        return Float64Signal.CreateConstant(
            SamplingRate,
            SampleCount,
            value,
            false
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> ScalarFromNumber(double value)
    {
        return Float64Signal.CreateConstant(
            SamplingRate,
            SampleCount,
            value,
            false
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> ScalarFromRational(long numerator, long denominator)
    {
        return Float64Signal.CreateConstant(
            SamplingRate,
            SampleCount,
            numerator / (double)denominator,
            false
        ).ScalarFromValue(this);
    }

    public Scalar<Float64Signal> ScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
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

        return Float64Signal.Create(SamplingRate, scalarArray, false).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> ScalarFromText(string text)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Add(Float64Signal scalar1, Float64Signal scalar2)
    {
        return scalar1.MapSamples(
            scalar2,
            (a, b) => a + b
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Subtract(Float64Signal scalar1, Float64Signal scalar2)
    {
        return scalar1.MapSamples(scalar2, (a, b) => a - b).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Times(Float64Signal scalar1, Float64Signal scalar2)
    {
        return scalar1.MapSamples(
            scalar2,
            (s1, s2) => s1 * s2
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Divide(Float64Signal scalar1, Float64Signal scalar2)
    {
        return scalar1.MapSamples(scalar2, (a, b) => a / b).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> VectorToRadians(Float64Signal scalarX, Float64Signal scalarY)
    {
        return scalarX.MapSamples(scalarY, (a, b) => ScalarProcessor.VectorToRadians(a, b).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Positive(Float64Signal scalar)
    {
        return scalar.ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Negative(Float64Signal scalar)
    {
        return scalar.MapSamples(scalar1 => -scalar1).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Inverse(Float64Signal scalar)
    {
        return scalar.MapSamples(scalar1 => 1 / scalar1).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Sign(Float64Signal scalar)
    {
        return scalar.MapSamples(
            s => ScalarProcessor.Sign(s).ScalarValue
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> UnitStep(Float64Signal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.UnitStep(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Abs(Float64Signal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Abs(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Power(Float64Signal baseScalar, Float64Signal scalar)
    {
        return baseScalar.MapSamples(scalar, (baseScalar1, scalar1) => ScalarProcessor.Power(baseScalar1, scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Sqrt(Float64Signal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Sqrt(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> SqrtOfAbs(Float64Signal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.SqrtOfAbs(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Exp(Float64Signal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Exp(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> LogE(Float64Signal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.LogE(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Log2(Float64Signal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Log2(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Log10(Float64Signal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Log10(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Log(Float64Signal baseScalar, Float64Signal scalar)
    {
        return baseScalar.MapSamples(scalar, (baseScalar1, scalar1) => ScalarProcessor.Log(baseScalar1, scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Cos(Float64Signal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Cos(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Sin(Float64Signal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Sin(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Tan(Float64Signal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Tan(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Cosh(Float64Signal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Cosh(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Sinh(Float64Signal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Sinh(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<Float64Signal> Tanh(Float64Signal scalar)
    {
        return scalar.MapSamples(scalar1 => ScalarProcessor.Tanh(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(Float64Signal scalar)
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
    public double ToFloat64(Float64Signal scalar)
    {
        return double.NaN;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(Float64Signal scalar)
    {
        return scalar
            .Select(s => s.ToString("G"))
            .Concatenate(", ", "{", "}");
    }
}