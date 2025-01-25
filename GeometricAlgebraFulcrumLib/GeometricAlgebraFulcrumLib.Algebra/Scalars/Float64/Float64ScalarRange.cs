using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

public readonly struct Float64ScalarRange :
    IAlgebraicElement,
    IPair<Float64Scalar>,
    IEquatable<Float64ScalarRange>
{
    public static Float64ScalarRange Infinite { get; }
        = new Float64ScalarRange(
            Float64Scalar.NegativeInfinity,
            Float64Scalar.PositiveInfinity
        );

    public static Float64ScalarRange ZeroToInfinity { get; }
        = new Float64ScalarRange(
            0d,
            Float64Scalar.PositiveInfinity
        );

    public static Float64ScalarRange InfinityToZero { get; }
        = new Float64ScalarRange(
            Float64Scalar.NegativeInfinity,
            0d
        );
    
    public static Float64ScalarRange MinusOneToOne { get; }
        = new Float64ScalarRange(-1d, 1d);

    public static Float64ScalarRange ZeroToOne { get; }
        = new Float64ScalarRange(0d, 1d);

    public static Float64ScalarRange ZeroToPi { get; }
        = new Float64ScalarRange(0d, Math.PI);

    public static Float64ScalarRange ZeroToTwoPi { get; }
        = new Float64ScalarRange(0d, 2d * Math.PI);

    public static Float64ScalarRange NegativeOneToZero { get; }
        = new Float64ScalarRange(-1d, 0d);

    public static Float64ScalarRange NegativeOneToOne { get; }
        = new Float64ScalarRange(-1d, 1d);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange CreateAround(Float64Scalar center, Float64Scalar delta)
    {
        return delta >= 0
            ? new Float64ScalarRange(center - delta, center + delta)
            : new Float64ScalarRange(center + delta, center - delta);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange Create(Float64Scalar value)
    {
        return value > 0d
            ? new Float64ScalarRange(Float64Scalar.Zero, value)
            : new Float64ScalarRange(value, Float64Scalar.Zero);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange Create(int value1, int value2)
    {
        return value1 <= value2
            ? new Float64ScalarRange(value1, value2)
            : new Float64ScalarRange(value2, value1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange Create(double value1, double value2)
    {
        return value1 <= value2
            ? new Float64ScalarRange(value1, value2)
            : new Float64ScalarRange(value2, value1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange Create(Float64Scalar value1, Float64Scalar value2)
    {
        return value1 <= value2
            ? new Float64ScalarRange(value1, value2)
            : new Float64ScalarRange(value2, value1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange Create(IPair<Float64Scalar> range)
    {
        var value1 = range.Item1;
        var value2 = range.Item2;

        return value1 < value2
            ? new Float64ScalarRange(value1, value2)
            : new Float64ScalarRange(value2, value1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange Create(Float64Scalar value1, Float64Scalar value2, Float64Scalar value3)
    {
        var minValue = value1;
        var maxValue = value1;

        if (minValue > value2) minValue = value2;
        if (minValue > value3) minValue = value3;

        if (maxValue < value2) maxValue = value2;
        if (maxValue < value3) maxValue = value3;

        return new Float64ScalarRange(minValue, maxValue);
    }

    public static Float64ScalarRange Create(params Float64Scalar[] valuesList)
    {
        var minValue = 0.0d;
        var maxValue = 0.0d;

        var flag = false;
        foreach (var value in valuesList)
        {
            if (!flag)
            {
                minValue = value;
                maxValue = value;

                flag = true;
                continue;
            }

            if (minValue > value) minValue = value;
            if (maxValue < value) maxValue = value;
        }

        return new Float64ScalarRange(minValue, maxValue);
    }
    
    public static Float64ScalarRange Create(IEnumerable<double> valuesList)
    {
        var minValue = 0.0d;
        var maxValue = 0.0d;

        var flag = false;
        foreach (var value in valuesList)
        {
            if (!flag)
            {
                minValue = value;
                maxValue = value;

                flag = true;
                continue;
            }

            if (minValue > value) minValue = value;
            if (maxValue < value) maxValue = value;
        }

        return new Float64ScalarRange(minValue, maxValue);
    }

    public static Float64ScalarRange Create(IEnumerable<Float64Scalar> valuesList)
    {
        var minValue = 0.0d;
        var maxValue = 0.0d;

        var flag = false;
        foreach (var value in valuesList)
        {
            if (!flag)
            {
                minValue = value;
                maxValue = value;

                flag = true;
                continue;
            }

            if (minValue > value) minValue = value;
            if (maxValue < value) maxValue = value;
        }

        return new Float64ScalarRange(minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange Create(Float64ScalarRange b1, Float64ScalarRange b2)
    {
        return new Float64ScalarRange(
            Math.Min(b1.MinValue, b2.MinValue),
            Math.Max(b1.MaxValue, b2.MaxValue)
        );
    }

    public static Float64ScalarRange Create(params Float64ScalarRange[] rangeList)
    {
        var minValue = 0.0d;
        var maxValue = 0.0d;

        var flag = false;
        foreach (var range in rangeList)
        {
            if (!flag)
            {
                minValue = range.MinValue;
                maxValue = range.MaxValue;

                flag = true;
                continue;
            }

            if (minValue > range.MinValue) minValue = range.MinValue;
            if (maxValue < range.MaxValue) maxValue = range.MaxValue;
        }

        return new Float64ScalarRange(minValue, maxValue);
    }

    public static Float64ScalarRange Create(IEnumerable<Float64ScalarRange> rangeList)
    {
        var minValue = 0.0d;
        var maxValue = 0.0d;

        var flag = false;
        foreach (var range in rangeList)
        {
            if (!flag)
            {
                minValue = range.MinValue;
                maxValue = range.MaxValue;

                flag = true;
                continue;
            }

            if (minValue > range.MinValue) minValue = range.MinValue;
            if (maxValue < range.MaxValue) maxValue = range.MaxValue;
        }

        return new Float64ScalarRange(minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange CreateFromIntersection(Float64ScalarRange b1, Float64ScalarRange b2)
    {
        return new Float64ScalarRange(
            Math.Max(b1.MinValue, b2.MinValue),
            Math.Min(b1.MaxValue, b2.MaxValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange CreateFromOuterBounds(Float64ScalarRange b1, Float64ScalarRange b2)
    {
        return new Float64ScalarRange(
            Math.Min(b1.MinValue, b2.MinValue),
            Math.Max(b1.MaxValue, b2.MaxValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange operator -(Float64ScalarRange b1)
    {
        return new Float64ScalarRange(
            -b1.MaxValue,
            -b1.MinValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange operator +(Float64ScalarRange b1, Float64Scalar b2)
    {
        return new Float64ScalarRange(
            b1.MinValue + b2,
            b1.MaxValue + b2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange operator +(Float64Scalar b1, Float64ScalarRange b2)
    {
        return new Float64ScalarRange(
            b1 + b2.MinValue,
            b1 + b2.MaxValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange operator -(Float64ScalarRange b1, Float64Scalar b2)
    {
        return new Float64ScalarRange(
            b1.MinValue - b2,
            b1.MaxValue - b2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange operator -(Float64Scalar b1, Float64ScalarRange b2)
    {
        return new Float64ScalarRange(
            b1 - b2.MaxValue,
            b1 - b2.MinValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange operator *(Float64ScalarRange b1, Float64Scalar b2)
    {
        if (b2 > 0)
            return new Float64ScalarRange(
                b1.MinValue * b2,
                b1.MaxValue * b2
            );

        return new Float64ScalarRange(
            b1.MaxValue * b2,
            b1.MinValue * b2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange operator *(Float64Scalar b1, Float64ScalarRange b2)
    {
        if (b1 > 0)
            return new Float64ScalarRange(
                b2.MinValue * b1,
                b2.MaxValue * b1
            );

        return new Float64ScalarRange(
            b2.MaxValue * b1,
            b2.MinValue * b1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange operator /(Float64ScalarRange b1, Float64Scalar b2)
    {
        if (b2 > 0)
            return new Float64ScalarRange(
                b1.MinValue / b2,
                b1.MaxValue / b2
            );

        return new Float64ScalarRange(
            b1.MaxValue / b2,
            b1.MinValue / b2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Float64ScalarRange left, Float64ScalarRange right)
    {
        return left.Equals(right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Float64ScalarRange left, Float64ScalarRange right)
    {
        return !left.Equals(right);
    }


    public Float64Scalar MinValue { get; }

    public Float64Scalar MaxValue { get; }

    public Float64Scalar MidValue
        => 0.5 * (MinValue + MaxValue);

    public Float64Scalar Length
        => MaxValue - MinValue;

    public Float64Scalar Item1
        => MinValue.ScalarValue;

    public Float64Scalar Item2
        => MaxValue.ScalarValue;

    public bool IsZeroLength 
        => MinValue == MaxValue;

    public bool IsFinite
        => MinValue.IsFinite() &&
           MaxValue.IsFinite();

    public bool IsInfinite
        => Float64Scalar.IsInfinity(MinValue) ||
           Float64Scalar.IsInfinity(MaxValue);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarRange(Float64Scalar minValue, Float64Scalar maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return MinValue.IsValid() &&
               MaxValue.IsValid() &&
               MinValue <= MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out Float64Scalar minValue, out Float64Scalar maxValue)
    {
        minValue = MinValue;
        maxValue = MaxValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarRange Intersect(Float64ScalarRange range2)
    {
        return CreateFromIntersection(this, range2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar AffineMapToZeroOneRange(Float64Scalar value)
    {
        if (!IsFinite)
            throw new InvalidOperationException();

        return (value - MinValue) / (MaxValue - MinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar AffineMapToRange(Float64Scalar value, Float64Scalar value1, Float64Scalar value2)
    {
        return AffineMapToZeroOneRange(value) * (value2 - value1) + value1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar AffineMapToRange(Float64Scalar value, IPair<Float64Scalar> valuePair, bool reverseLimits = false)
    {
        return reverseLimits
            ? AffineMapToZeroOneRange(value) * (valuePair.Item1 - valuePair.Item2) + valuePair.Item2
            : AffineMapToZeroOneRange(value) * (valuePair.Item2 - valuePair.Item1) + valuePair.Item1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar AffineMapToRange(Float64Scalar value, Float64ScalarRange range, bool reverseLimits = false)
    {
        if (!range.IsFinite)
            throw new InvalidOperationException();

        return reverseLimits
            ? range.MaxValue - AffineMapToZeroOneRange(value) * range.Length
            : range.MinValue + AffineMapToZeroOneRange(value) * range.Length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarRange Negative()
    {
        return new Float64ScalarRange(-MaxValue, -MinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarRange Plus(Float64Scalar value)
    {
        return new Float64ScalarRange(MinValue + value, MaxValue + value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarRange Times(Float64Scalar value)
    {
        return value > 0
            ? new Float64ScalarRange(MinValue * value, MaxValue * value)
            : new Float64ScalarRange(MaxValue * value, MinValue * value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarRange Divide(Float64Scalar value)
    {
        return value > 0
            ? new Float64ScalarRange(MinValue / value, MaxValue / value)
            : new Float64ScalarRange(MaxValue / value, MinValue / value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarRange ResetMinValue(Float64Scalar minValue)
    {
        return Create(minValue, MaxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarRange ResetMaxValue(Float64Scalar maxValue)
    {
        return Create(MinValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarRange ExpandBy(Float64Scalar delta)
    {
        return Create(
            MinValue - delta,
            MaxValue + delta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarRange ExpandByFactor(Float64Scalar deltaPercent)
    {
        var delta = deltaPercent * (MaxValue - MinValue);

        return Create(
            MinValue - delta,
            MaxValue + delta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarRange ExpandToInclude(Float64Scalar value)
    {
        if (value < MinValue)
            return new Float64ScalarRange(value, MaxValue);

        if (value > MaxValue)
            return new Float64ScalarRange(MinValue, value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarRange ExpandToInclude(Float64Scalar value1, Float64Scalar value2)
    {
        var minValue = MinValue;
        var maxValue = MaxValue;

        if (value1 < minValue)
            minValue = value1;

        else if (value1 > maxValue)
            maxValue = value1;

        if (value2 < minValue)
            minValue = value1;

        else if (value2 > maxValue)
            maxValue = value1;

        return new Float64ScalarRange(minValue, maxValue);
    }

    public Float64ScalarRange ExpandToInclude(params Float64Scalar[] valuesList)
    {
        var minValue = MinValue;
        var maxValue = MaxValue;

        foreach (var value in valuesList)
        {
            if (value < minValue)
                minValue = value;

            else if (value > MaxValue)
                maxValue = value;
        }

        return new Float64ScalarRange(minValue, maxValue);
    }

    public Float64ScalarRange ExpandToInclude(IEnumerable<Float64Scalar> valuesList)
    {
        var minValue = MinValue;
        var maxValue = MaxValue;

        foreach (var value in valuesList)
        {
            if (value < minValue)
                minValue = value;

            else if (value > MaxValue)
                maxValue = value;
        }

        return new Float64ScalarRange(minValue, maxValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarRange ExpandToInclude(IPair<Float64Scalar> range)
    {
        return ExpandToInclude(
            range.Item1,
            range.Item2
        );
    }

    public Float64ScalarRange ExpandToInclude(params Float64ScalarRange[] rangeList)
    {
        var minValue = MinValue;
        var maxValue = MaxValue;

        foreach (var (value1, value2) in rangeList)
        {
            if (value1 < minValue)
                minValue = value1;

            else if (value1 > MaxValue)
                maxValue = value1;

            if (value2 < minValue)
                minValue = value2;

            else if (value2 > MaxValue)
                maxValue = value2;
        }

        return new Float64ScalarRange(minValue, maxValue);
    }

    public Float64ScalarRange ExpandToInclude(IEnumerable<Float64ScalarRange> rangeList)
    {
        var minValue = MinValue;
        var maxValue = MaxValue;

        foreach (var (value1, value2) in rangeList)
        {
            if (value1 < minValue)
                minValue = value1;

            else if (value1 > MaxValue)
                maxValue = value1;

            if (value2 < minValue)
                minValue = value2;

            else if (value2 > MaxValue)
                maxValue = value2;
        }

        return new Float64ScalarRange(minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetOffsetFromMin(Float64Scalar value)
    {
        return value - MinValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetOffsetToMin(Float64Scalar value)
    {
        return MinValue - value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetOffsetFromMax(Float64Scalar value)
    {
        return value - MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetOffsetToMax(Float64Scalar value)
    {
        return MaxValue - value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetRelativeOffset(Float64Scalar value)
    {
        return (value - MinValue) / (MaxValue - MinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Scalar> GetLinearSamples(int count, bool assumePeriodic)
    {
        if (assumePeriodic && count < 1 || !assumePeriodic && count < 2)
            throw new ArgumentOutOfRangeException(nameof(count));

        return MinValue.GetLinearRange(MaxValue, count, assumePeriodic);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Float64Scalar> GetLinearPeriodicSamples(int count)
    {
        if (count < 1)
            throw new ArgumentOutOfRangeException(nameof(count));

        return MinValue.GetLinearPeriodicRange(MaxValue, count);
    }

    public Float64ScalarRange[] GetSubdivisions(int divisionCount, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        var length = Length / divisionCount;

        var minValue = MinValue;

        var minValues =
            Enumerable
                .Range(0, divisionCount)
                .Select(i => i * length + minValue + zeroEpsilon)
                .ToArray();

        var maxValues =
            minValues
                .Select(v => v + length - zeroEpsilon)
                .ToArray();

        var divisions = new Float64ScalarRange[divisionCount];

        for (var i = 0; i < divisionCount; i++)
            divisions[i] = Create(
                minValues[i],
                maxValues[i]
            );

        return divisions;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(double value, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        Debug.Assert(zeroEpsilon >= 0);

        return value >= MinValue - zeroEpsilon &&
               value <= MaxValue + zeroEpsilon;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Float64Scalar value, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        Debug.Assert(zeroEpsilon >= 0);

        return value >= MinValue - zeroEpsilon &&
               value <= MaxValue + zeroEpsilon;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Clamp(double value, double zeroEpsilon = 0)
    {
        return value.Clamp(
            MinValue.ScalarValue - zeroEpsilon, 
            MaxValue.ScalarValue + zeroEpsilon
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Clamp(Float64Scalar value, double zeroEpsilon = 0)
    {
        return value.Clamp(
            MinValue.ScalarValue - zeroEpsilon, 
            MaxValue.ScalarValue + zeroEpsilon
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ClampPeriodic(double value)
    {
        return value.ClampPeriodic(
            MinValue.ScalarValue, 
            MaxValue.ScalarValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ClampPeriodic(Float64Scalar value)
    {
        if (!IsFinite)
            throw new InvalidOperationException();

        return value.ClampPeriodic(
            MinValue.ScalarValue, 
            MaxValue.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Float64ScalarRange box, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        Debug.Assert(zeroEpsilon >= 0);

        return box.MinValue >= MinValue - zeroEpsilon &&
               box.MaxValue <= MaxValue + zeroEpsilon;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsUpperExclusive(Float64Scalar value, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        Debug.Assert(zeroEpsilon >= 0);

        return value >= MinValue - zeroEpsilon &&
               value < MaxValue + zeroEpsilon;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlaps(Float64ScalarRange box, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        Debug.Assert(zeroEpsilon >= 0);

        return box.MaxValue >= MinValue - zeroEpsilon &&
               box.MinValue <= MaxValue + zeroEpsilon;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Float64ScalarRange other)
    {
        return MinValue.Equals(other.MinValue) &&
               MaxValue.Equals(other.MaxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return obj is Float64ScalarRange other && Equals(other);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return HashCode.Combine(MinValue, MaxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"[{MinValue}, {MaxValue}]";
    }
}