using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space1D;

public readonly struct Int64Range1D :
    IAlgebraicElement,
    IPair<long>,
    IReadOnlyList<long>,
    IEquatable<Int64Range1D>
{
    public static Int64Range1D MinToMax { get; }
        = new Int64Range1D(long.MinValue, long.MaxValue);

    public static Int64Range1D ZeroToMax { get; }
        = new Int64Range1D(0, long.MaxValue);

    public static Int64Range1D MinToZero { get; }
        = new Int64Range1D(long.MinValue, 0);

    public static Int64Range1D ZeroToOne { get; }
        = new Int64Range1D(0, 1);

    public static Int64Range1D NegativeOneToZero { get; }
        = new Int64Range1D(-1, 0);

    public static Int64Range1D NegativeOneToOne { get; }
        = new Int64Range1D(-1, 1);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int64Range1D CreateAround(long center, long delta)
    {
        return delta >= 0
            ? new Int64Range1D(center - delta, center + delta)
            : new Int64Range1D(center + delta, center - delta);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int64Range1D Create(long value1, long value2)
    {
        return value1 <= value2
            ? new Int64Range1D(value1, value2)
            : new Int64Range1D(value2, value1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int64Range1D Create(IPair<long> range)
    {
        var value1 = range.Item1;
        var value2 = range.Item2;

        return value1 < value2
            ? new Int64Range1D(value1, value2)
            : new Int64Range1D(value2, value1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int64Range1D Create(long value1, long value2, long value3)
    {
        var minValue = value1;
        var maxValue = value1;

        if (minValue > value2) minValue = value2;
        if (minValue > value3) minValue = value3;

        if (maxValue < value2) maxValue = value2;
        if (maxValue < value3) maxValue = value3;

        return new Int64Range1D(minValue, maxValue);
    }

    public static Int64Range1D Create(params long[] valuesList)
    {
        var minValue = 0L;
        var maxValue = 0L;

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

        return new Int64Range1D(minValue, maxValue);
    }

    public static Int64Range1D Create(IEnumerable<long> valuesList)
    {
        var minValue = 0L;
        var maxValue = 0L;

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

        return new Int64Range1D(minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int64Range1D Create(Int64Range1D b1, Int64Range1D b2)
    {
        return new Int64Range1D(
            Math.Min(b1.MinValue, b2.MinValue),
            Math.Max(b1.MaxValue, b2.MaxValue)
        );
    }

    public static Int64Range1D Create(params Int64Range1D[] rangeList)
    {
        var minValue = 0L;
        var maxValue = 0L;

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

        return new Int64Range1D(minValue, maxValue);
    }

    public static Int64Range1D Create(IEnumerable<Int64Range1D> rangeList)
    {
        var minValue = 0L;
        var maxValue = 0L;

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

        return new Int64Range1D(minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int64Range1D CreateFromIntersection(Int64Range1D b1, Int64Range1D b2)
    {
        return new Int64Range1D(
            Math.Max(b1.MinValue, b2.MinValue),
            Math.Min(b1.MaxValue, b2.MaxValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int64Range1D operator -(Int64Range1D b1)
    {
        return new Int64Range1D(
            -b1.MaxValue,
            -b1.MinValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int64Range1D operator +(Int64Range1D b1, long b2)
    {
        return new Int64Range1D(
            b1.MinValue + b2,
            b1.MaxValue + b2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int64Range1D operator +(long b1, Int64Range1D b2)
    {
        return new Int64Range1D(
            b1 + b2.MinValue,
            b1 + b2.MaxValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int64Range1D operator -(Int64Range1D b1, long b2)
    {
        return new Int64Range1D(
            b1.MinValue - b2,
            b1.MaxValue - b2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int64Range1D operator -(long b1, Int64Range1D b2)
    {
        return new Int64Range1D(
            b1 - b2.MaxValue,
            b1 - b2.MinValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int64Range1D operator *(Int64Range1D b1, long b2)
    {
        if (b2 > 0)
            return new Int64Range1D(
                b1.MinValue * b2,
                b1.MaxValue * b2
            );

        return new Int64Range1D(
            b1.MaxValue * b2,
            b1.MinValue * b2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int64Range1D operator *(long b1, Int64Range1D b2)
    {
        if (b1 > 0)
            return new Int64Range1D(
                b2.MinValue * b1,
                b2.MaxValue * b1
            );

        return new Int64Range1D(
            b2.MaxValue * b1,
            b2.MinValue * b1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Int64Range1D left, Int64Range1D right)
    {
        return left.Equals(right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Int64Range1D left, Int64Range1D right)
    {
        return !left.Equals(right);
    }


    public long MinValue { get; }

    public long MaxValue { get; }

    public long MidValue1
        => Count.IsEven()
            ? (MinValue + MaxValue - 1) / 2
            : (MinValue + MaxValue) / 2;

    public long MidValue2
        => Count.IsEven()
            ? (MinValue + MaxValue + 1) / 2
            : (MinValue + MaxValue) / 2;

    public int Count
        => (int)(MaxValue - MinValue + 1);

    public long Item1
        => MinValue;

    public long Item2
        => MaxValue;

    public long this[int index]
        => index >= 0 && index < Count
            ? index + MinValue
            : throw new IndexOutOfRangeException();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Int64Range1D(long minValue, long maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return MinValue < MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out long minValue, out long maxValue)
    {
        minValue = MinValue;
        maxValue = MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64Range1D Negative()
    {
        return new Int64Range1D(-MaxValue, -MinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64Range1D Plus(long value)
    {
        return new Int64Range1D(MinValue + value, MaxValue + value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64Range1D Times(long value)
    {
        return value > 0
            ? new Int64Range1D(MinValue * value, MaxValue * value)
            : new Int64Range1D(MaxValue * value, MinValue * value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64Range1D ResetMinValue(long minValue)
    {
        return Create(minValue, MaxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64Range1D ResetMaxValue(long maxValue)
    {
        return Create(MinValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64Range1D ExpandBy(long delta)
    {
        return Create(
            MinValue - delta,
            MaxValue + delta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64Range1D ExpandByFactor(long deltaPercent)
    {
        var delta = deltaPercent * Count;

        return Create(
            MinValue - delta,
            MaxValue + delta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64Range1D ExpandToInclude(long value)
    {
        if (value < MinValue)
            return new Int64Range1D(value, MaxValue);

        if (value > MaxValue)
            return new Int64Range1D(MinValue, value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64Range1D ExpandToInclude(long value1, long value2)
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

        return new Int64Range1D(minValue, maxValue);
    }

    public Int64Range1D ExpandToInclude(params long[] valuesList)
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

        return new Int64Range1D(minValue, maxValue);
    }

    public Int64Range1D ExpandToInclude(IEnumerable<long> valuesList)
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

        return new Int64Range1D(minValue, maxValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64Range1D ExpandToInclude(IPair<long> range)
    {
        return ExpandToInclude(
            range.Item1,
            range.Item2
        );
    }

    public Int64Range1D ExpandToInclude(params Int64Range1D[] rangeList)
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

        return new Int64Range1D(minValue, maxValue);
    }

    public Int64Range1D ExpandToInclude(IEnumerable<Int64Range1D> rangeList)
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

        return new Int64Range1D(minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long GetOffsetFromMin(long value)
    {
        return value - MinValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long GetOffsetToMin(long value)
    {
        return MinValue - value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long GetOffsetFromMax(long value)
    {
        return value - MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long GetOffsetToMax(long value)
    {
        return MaxValue - value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetRelativeOffset(long value)
    {
        return (value - MinValue) / (double)(MaxValue - MinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(long value)
    {
        return value >= MinValue &&
               value <= MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Int64Range1D box)
    {
        return box.MinValue >= MinValue &&
               box.MaxValue <= MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsUpperExclusive(long value)
    {
        return value >= MinValue &&
               value < MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlaps(Int64Range1D box)
    {
        return box.MaxValue >= MinValue &&
               box.MinValue <= MaxValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Int64Range1D other)
    {
        return MinValue.Equals(other.MinValue) &&
               MaxValue.Equals(other.MaxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return obj is Int64Range1D other && Equals(other);
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<long> GetEnumerator()
    {
        for (var i = MinValue; i <= MaxValue; i++)
            yield return i;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}