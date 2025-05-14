using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space1D;

public readonly struct Int32Range1D :
    IAlgebraicElement,
    IPair<int>,
    IReadOnlyList<int>,
    IEquatable<Int32Range1D>
{
    public static Int32Range1D MinToMax { get; }
        = new Int32Range1D(int.MinValue, int.MaxValue);

    public static Int32Range1D ZeroToMax { get; }
        = new Int32Range1D(0, int.MaxValue);

    public static Int32Range1D MinToZero { get; }
        = new Int32Range1D(int.MinValue, 0);

    public static Int32Range1D ZeroToOne { get; }
        = new Int32Range1D(0, 1);

    public static Int32Range1D NegativeOneToZero { get; }
        = new Int32Range1D(-1, 0);

    public static Int32Range1D NegativeOneToOne { get; }
        = new Int32Range1D(-1, 1);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int32Range1D CreateAround(int center, int delta)
    {
        return delta >= 0
            ? new Int32Range1D(center - delta, center + delta)
            : new Int32Range1D(center + delta, center - delta);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int32Range1D Create(int value1, int value2)
    {
        return value1 <= value2
            ? new Int32Range1D(value1, value2)
            : new Int32Range1D(value2, value1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int32Range1D Create(IPair<int> range)
    {
        var value1 = range.Item1;
        var value2 = range.Item2;

        return value1 < value2
            ? new Int32Range1D(value1, value2)
            : new Int32Range1D(value2, value1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int32Range1D Create(int value1, int value2, int value3)
    {
        var minValue = value1;
        var maxValue = value1;

        if (minValue > value2) minValue = value2;
        if (minValue > value3) minValue = value3;

        if (maxValue < value2) maxValue = value2;
        if (maxValue < value3) maxValue = value3;

        return new Int32Range1D(minValue, maxValue);
    }

    public static Int32Range1D Create(params int[] valuesList)
    {
        var minValue = 0;
        var maxValue = 0;

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

        return new Int32Range1D(minValue, maxValue);
    }

    public static Int32Range1D Create(IEnumerable<int> valuesList)
    {
        var minValue = 0;
        var maxValue = 0;

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

        return new Int32Range1D(minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int32Range1D Create(Int32Range1D b1, Int32Range1D b2)
    {
        return new Int32Range1D(
            Math.Min(b1.MinValue, b2.MinValue),
            Math.Max(b1.MaxValue, b2.MaxValue)
        );
    }

    public static Int32Range1D Create(params Int32Range1D[] rangeList)
    {
        var minValue = 0;
        var maxValue = 0;

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

        return new Int32Range1D(minValue, maxValue);
    }

    public static Int32Range1D Create(IEnumerable<Int32Range1D> rangeList)
    {
        var minValue = 0;
        var maxValue = 0;

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

        return new Int32Range1D(minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int32Range1D CreateFromIntersection(Int32Range1D b1, Int32Range1D b2)
    {
        return new Int32Range1D(
            Math.Max(b1.MinValue, b2.MinValue),
            Math.Min(b1.MaxValue, b2.MaxValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int32Range1D operator -(Int32Range1D b1)
    {
        return new Int32Range1D(
            -b1.MaxValue,
            -b1.MinValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int32Range1D operator +(Int32Range1D b1, int b2)
    {
        return new Int32Range1D(
            b1.MinValue + b2,
            b1.MaxValue + b2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int32Range1D operator +(int b1, Int32Range1D b2)
    {
        return new Int32Range1D(
            b1 + b2.MinValue,
            b1 + b2.MaxValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int32Range1D operator -(Int32Range1D b1, int b2)
    {
        return new Int32Range1D(
            b1.MinValue - b2,
            b1.MaxValue - b2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int32Range1D operator -(int b1, Int32Range1D b2)
    {
        return new Int32Range1D(
            b1 - b2.MaxValue,
            b1 - b2.MinValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int32Range1D operator *(Int32Range1D b1, int b2)
    {
        if (b2 > 0)
            return new Int32Range1D(
                b1.MinValue * b2,
                b1.MaxValue * b2
            );

        return new Int32Range1D(
            b1.MaxValue * b2,
            b1.MinValue * b2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int32Range1D operator *(int b1, Int32Range1D b2)
    {
        if (b1 > 0)
            return new Int32Range1D(
                b2.MinValue * b1,
                b2.MaxValue * b1
            );

        return new Int32Range1D(
            b2.MaxValue * b1,
            b2.MinValue * b1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Int32Range1D left, Int32Range1D right)
    {
        return left.Equals(right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Int32Range1D left, Int32Range1D right)
    {
        return !left.Equals(right);
    }


    public int MinValue { get; }

    public int MaxValue { get; }

    public int MidValue1
        => Count.IsEven()
            ? (MinValue + MaxValue - 1) / 2
            : (MinValue + MaxValue) / 2;

    public int MidValue2
        => Count.IsEven()
            ? (MinValue + MaxValue + 1) / 2
            : (MinValue + MaxValue) / 2;

    public int Count
        => MaxValue - MinValue + 1;

    public int Item1
        => MinValue;

    public int Item2
        => MaxValue;

    public int this[int index]
        => index >= 0 && index < Count
            ? index + MinValue
            : throw new IndexOutOfRangeException();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Int32Range1D(int minValue, int maxValue)
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
    public void Deconstruct(out int minValue, out int maxValue)
    {
        minValue = MinValue;
        maxValue = MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32Range1D Negative()
    {
        return new Int32Range1D(-MaxValue, -MinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32Range1D Plus(int value)
    {
        return new Int32Range1D(MinValue + value, MaxValue + value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32Range1D Times(int value)
    {
        return value > 0
            ? new Int32Range1D(MinValue * value, MaxValue * value)
            : new Int32Range1D(MaxValue * value, MinValue * value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32Range1D ResetMinValue(int minValue)
    {
        return Create(minValue, MaxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32Range1D ResetMaxValue(int maxValue)
    {
        return Create(MinValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32Range1D ExpandBy(int delta)
    {
        return Create(
            MinValue - delta,
            MaxValue + delta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32Range1D ExpandByFactor(int deltaPercent)
    {
        var delta = deltaPercent * Count;

        return Create(
            MinValue - delta,
            MaxValue + delta
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32Range1D ExpandToInclude(int value)
    {
        if (value < MinValue)
            return new Int32Range1D(value, MaxValue);

        if (value > MaxValue)
            return new Int32Range1D(MinValue, value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32Range1D ExpandToInclude(int value1, int value2)
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

        return new Int32Range1D(minValue, maxValue);
    }

    public Int32Range1D ExpandToInclude(params int[] valuesList)
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

        return new Int32Range1D(minValue, maxValue);
    }

    public Int32Range1D ExpandToInclude(IEnumerable<int> valuesList)
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

        return new Int32Range1D(minValue, maxValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet ToDenseIndexSet()
    {
        return IndexSet.CreateDense(MinValue, Count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32Range1D ExpandToInclude(IPair<int> range)
    {
        return ExpandToInclude(
            range.Item1,
            range.Item2
        );
    }

    public Int32Range1D ExpandToInclude(params Int32Range1D[] rangeList)
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

        return new Int32Range1D(minValue, maxValue);
    }

    public Int32Range1D ExpandToInclude(IEnumerable<Int32Range1D> rangeList)
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

        return new Int32Range1D(minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetOffsetFromMin(int value)
    {
        return value - MinValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetOffsetToMin(int value)
    {
        return MinValue - value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetOffsetFromMax(int value)
    {
        return value - MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetOffsetToMax(int value)
    {
        return MaxValue - value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetRelativeOffset(int value)
    {
        return (value - MinValue) / (double)(MaxValue - MinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(int value)
    {
        return value >= MinValue &&
               value <= MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Int32Range1D box)
    {
        return box.MinValue >= MinValue &&
               box.MaxValue <= MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsUpperExclusive(int value)
    {
        return value >= MinValue &&
               value < MaxValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlaps(Int32Range1D box)
    {
        return box.MaxValue >= MinValue &&
               box.MinValue <= MaxValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Int32Range1D other)
    {
        return MinValue.Equals(other.MinValue) &&
               MaxValue.Equals(other.MaxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return obj is Int32Range1D other && Equals(other);
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
    public IEnumerator<int> GetEnumerator()
    {
        return Enumerable.Range(MinValue, Count).GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}