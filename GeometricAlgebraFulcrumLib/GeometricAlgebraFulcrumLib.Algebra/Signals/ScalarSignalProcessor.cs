using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.Signals;

public sealed class ScalarSignalProcessor<T> :
    IScalarProcessor<IReadOnlyList<T>>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static ScalarSignalProcessor<T> Create(IScalarProcessor<T> scalarProcessor, int signalSamplesCount)
    {
        return new ScalarSignalProcessor<T>(scalarProcessor, signalSamplesCount);
    }


    public int SignalSamplesCount { get; }

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

    public Scalar<IReadOnlyList<T>> Zero { get; }

    public Scalar<IReadOnlyList<T>> PositiveInfinity { get; }

    public Scalar<IReadOnlyList<T>> NegativeInfinity { get; }

    public Scalar<IReadOnlyList<T>> One { get; }

    public Scalar<IReadOnlyList<T>> MinusOne { get; }

    public Scalar<IReadOnlyList<T>> Two { get; }

    public Scalar<IReadOnlyList<T>> MinusTwo { get; }

    public Scalar<IReadOnlyList<T>> Ten { get; }

    public Scalar<IReadOnlyList<T>> MinusTen { get; }

    public Scalar<IReadOnlyList<T>> Pi { get; }

    public Scalar<IReadOnlyList<T>> PiTimes2 { get; }

    public Scalar<IReadOnlyList<T>> PiTimes4 { get; }

    public Scalar<IReadOnlyList<T>> PiOver2 { get; }

    public Scalar<IReadOnlyList<T>> E { get; }

    public Scalar<IReadOnlyList<T>> DegreeToRadianFactor { get; }

    public Scalar<IReadOnlyList<T>> RadianToDegreeFactor { get; }

    public IReadOnlyList<T> ZeroValue { get; }

    public IReadOnlyList<T> PositiveInfinityValue { get; }

    public IReadOnlyList<T> NegativeInfinityValue { get; }

    public IReadOnlyList<T> OneValue { get; }

    public IReadOnlyList<T> MinusOneValue { get; }

    public IReadOnlyList<T> TwoValue { get; }

    public IReadOnlyList<T> MinusTwoValue { get; }

    public IReadOnlyList<T> TenValue { get; }

    public IReadOnlyList<T> MinusTenValue { get; }

    public IReadOnlyList<T> PiValue { get; }

    public IReadOnlyList<T> PiTimes2Value { get; }

    public IReadOnlyList<T> PiTimes4Value { get; }

    public IReadOnlyList<T> PiOver2Value { get; }

    public IReadOnlyList<T> EValue { get; }

    public IReadOnlyList<T> DegreeToRadianFactorValue { get; }

    public IReadOnlyList<T> RadianToDegreeFactorValue { get; }


    public ScalarSignalProcessor(IScalarProcessor<T> scalarProcessor, int signalSamplesCount)
    {
        if (signalSamplesCount < 1)
            throw new ArgumentOutOfRangeException(nameof(signalSamplesCount));

        SignalSamplesCount = signalSamplesCount;
        ScalarProcessor = scalarProcessor;

        ZeroValue = new RepeatedItemReadOnlyList<T>(ScalarProcessor.ZeroValue, SignalSamplesCount);
        OneValue = new RepeatedItemReadOnlyList<T>(ScalarProcessor.OneValue, SignalSamplesCount);
        MinusOneValue = new RepeatedItemReadOnlyList<T>(ScalarProcessor.MinusOneValue, SignalSamplesCount);
        TwoValue = new RepeatedItemReadOnlyList<T>(ScalarProcessor.TwoValue, SignalSamplesCount);
        MinusTwoValue = new RepeatedItemReadOnlyList<T>(ScalarProcessor.MinusTwoValue, SignalSamplesCount);
        TenValue = new RepeatedItemReadOnlyList<T>(ScalarProcessor.TenValue, SignalSamplesCount);
        MinusTenValue = new RepeatedItemReadOnlyList<T>(ScalarProcessor.MinusTenValue, SignalSamplesCount);
        PiValue = new RepeatedItemReadOnlyList<T>(ScalarProcessor.PiValue, SignalSamplesCount);
        PiTimes2Value = new RepeatedItemReadOnlyList<T>(ScalarProcessor.PiTimes2Value, SignalSamplesCount);
        PiTimes4Value = new RepeatedItemReadOnlyList<T>(ScalarProcessor.PiTimes4Value, SignalSamplesCount);
        PiOver2Value = new RepeatedItemReadOnlyList<T>(ScalarProcessor.PiOver2Value, SignalSamplesCount);
        EValue = new RepeatedItemReadOnlyList<T>(ScalarProcessor.EValue, SignalSamplesCount);
        DegreeToRadianFactorValue = new RepeatedItemReadOnlyList<T>(ScalarProcessor.DegreeToRadianFactorValue, SignalSamplesCount);
        RadianToDegreeFactorValue = new RepeatedItemReadOnlyList<T>(ScalarProcessor.RadianToDegreeFactorValue, SignalSamplesCount);
        PositiveInfinityValue = new RepeatedItemReadOnlyList<T>(ScalarProcessor.PositiveInfinityValue, SignalSamplesCount);
        NegativeInfinityValue = new RepeatedItemReadOnlyList<T>(ScalarProcessor.NegativeInfinityValue, SignalSamplesCount);

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
    public Scalar<IReadOnlyList<T>> ScalarFromNumber(int value)
    {
        return new RepeatedItemReadOnlyList<T>(
            ScalarProcessor.ValueFromNumber(value),
            SignalSamplesCount
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> ScalarFromNumber(uint value)
    {
        return new RepeatedItemReadOnlyList<T>(
            ScalarProcessor.ValueFromNumber(value),
            SignalSamplesCount
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> ScalarFromNumber(long value)
    {
        return new RepeatedItemReadOnlyList<T>(
            ScalarProcessor.ValueFromNumber(value),
            SignalSamplesCount
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> ScalarFromNumber(ulong value)
    {
        return new RepeatedItemReadOnlyList<T>(
            ScalarProcessor.ValueFromNumber(value),
            SignalSamplesCount
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> ScalarFromNumber(float value)
    {
        return new RepeatedItemReadOnlyList<T>(
            ScalarProcessor.ValueFromNumber(value),
            SignalSamplesCount
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> ScalarFromNumber(double value)
    {
        return new RepeatedItemReadOnlyList<T>(
            ScalarProcessor.ValueFromNumber(value),
            SignalSamplesCount
        ).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> ScalarFromRational(long numerator, long denominator)
    {
        return new RepeatedItemReadOnlyList<T>(
            ScalarProcessor.ScalarFromRational(numerator, denominator).ScalarValue,
            SignalSamplesCount
        ).ScalarFromValue(this);
    }

    public Scalar<IReadOnlyList<T>> ScalarFromRandom(Random randomGenerator, double minValue, double maxValue)
    {
        var scalarArray = new T[SignalSamplesCount];

        for (var i = 0; i < SignalSamplesCount; i++)
        {
            scalarArray[i] = ScalarProcessor.ScalarFromRandom(
                randomGenerator,
                minValue,
                maxValue
            ).ScalarValue;
        }

        return scalarArray.ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> ScalarFromText(string text)
    {
        return new RepeatedItemReadOnlyList<T>(
            ScalarProcessor.ScalarFromText(text).ScalarValue,
            SignalSamplesCount
        ).ScalarFromValue(this);
    }

    private IReadOnlyList<T> UnaryOperation(IReadOnlyList<T> scalar1, Func<T, T> scalarMapping)
    {
        var count1 = scalar1.Count;

        var scalarArray = new T[SignalSamplesCount];

        for (var i = 0; i < SignalSamplesCount; i++)
        {
            var s1 = i < count1 ? scalar1[i] : ScalarProcessor.ZeroValue;

            scalarArray[i] = scalarMapping(s1);
        }

        return scalarArray;
    }

    private IReadOnlyList<T> BinaryOperation(IReadOnlyList<T> scalar1, IReadOnlyList<T> scalar2, Func<T, T, T> scalarMapping)
    {
        var count1 = scalar1.Count;
        var count2 = scalar2.Count;

        var scalarArray = new T[SignalSamplesCount];

        for (var i = 0; i < SignalSamplesCount; i++)
        {
            var s1 = i < count1 ? scalar1[i] : ScalarProcessor.ZeroValue;
            var s2 = i < count2 ? scalar2[i] : ScalarProcessor.ZeroValue;

            scalarArray[i] = scalarMapping(s1, s2);
        }

        return scalarArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Add(IReadOnlyList<T> scalar1, IReadOnlyList<T> scalar2)
    {
        return BinaryOperation(scalar1, scalar2, (a, b) => ScalarProcessor.Add(a, b).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Subtract(IReadOnlyList<T> scalar1, IReadOnlyList<T> scalar2)
    {
        return BinaryOperation(scalar1, scalar2, (a, b) => ScalarProcessor.Subtract(a, b).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Times(IReadOnlyList<T> scalar1, IReadOnlyList<T> scalar2)
    {
        return BinaryOperation(scalar1, scalar2, (a, b) => ScalarProcessor.Times(a, b).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Divide(IReadOnlyList<T> scalar1, IReadOnlyList<T> scalar2)
    {
        return BinaryOperation(scalar1, scalar2, (a, b) => ScalarProcessor.Divide(a, b).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Positive(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.Positive(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Negative(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.Negative(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Inverse(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.Inverse(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Sign(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.Sign(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> UnitStep(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.UnitStep(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Abs(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.Abs(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Power(IReadOnlyList<T> baseScalar, IReadOnlyList<T> scalar)
    {
        return BinaryOperation(baseScalar, scalar, (baseScalar1, scalar1) => ScalarProcessor.Power(baseScalar1, scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Sqrt(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.Sqrt(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> SqrtOfAbs(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.SqrtOfAbs(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Exp(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.Exp(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> LogE(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.LogE(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Log2(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.Log2(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Log10(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.Log10(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Log(IReadOnlyList<T> baseScalar, IReadOnlyList<T> scalar)
    {
        return BinaryOperation(baseScalar, scalar, (baseScalar1, scalar1) => ScalarProcessor.Log(baseScalar1, scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Cos(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.Cos(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Sin(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.Sin(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Tan(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.Tan(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Cosh(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.Cosh(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Sinh(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.Sinh(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> Tanh(IReadOnlyList<T> scalar)
    {
        return UnaryOperation(scalar, scalar1 => ScalarProcessor.Tanh(scalar1).ScalarValue).ScalarFromValue(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(IReadOnlyList<T> scalar)
    {
        return scalar.All(ScalarProcessor.IsValid);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsFiniteNumber(IReadOnlyList<T> scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsFiniteNumber);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(IReadOnlyList<T> scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsZero);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsZero(IReadOnlyList<T> scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? IsNearZero(scalar) 
    //        : IsZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNearZero(IReadOnlyList<T> scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsNearZero);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(IReadOnlyList<T> scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsNotZero);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotZero(IReadOnlyList<T> scalar, bool nearZeroFlag)
    //{
    //    return nearZeroFlag
    //        ? IsNotNearZero(scalar) 
    //        : IsNotZero(scalar);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearZero(IReadOnlyList<T> scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsNotNearZero);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsPositive(IReadOnlyList<T> scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsPositive);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNegative(IReadOnlyList<T> scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsNegative);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotPositive(IReadOnlyList<T> scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsNotPositive);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNegative(IReadOnlyList<T> scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsNotNegative);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearPositive(IReadOnlyList<T> scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsNotNearPositive);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool IsNotNearNegative(IReadOnlyList<T> scalar)
    //{
    //    return scalar.All(ScalarProcessor.IsNotNearNegative);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ToFloat64(IReadOnlyList<T> scalar)
    {
        return double.NaN;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToText(IReadOnlyList<T> scalar)
    {
        return scalar
            .Select(ScalarProcessor.ToText)
            .Concatenate(", ", "{", "}");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<IReadOnlyList<T>> VectorToRadians(IReadOnlyList<T> scalarX, IReadOnlyList<T> scalarY)
    {
        return BinaryOperation(scalarX, scalarY, (scalar1, scalar2) => ScalarProcessor.VectorToRadians(scalar1, scalar2).ScalarValue).ScalarFromValue(this);
    }
}