using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

public readonly struct SmallIndexSet :
    IReadOnlyList<int>,
    IEquatable<SmallIndexSet>,
    IComparable<SmallIndexSet>
{
    private static int _emptySetCount;
    public static SmallIndexSet EmptySet { get; }
        = new SmallIndexSet();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet Create(int index1)
    {
        if (index1 < 0)
            throw new InvalidOperationException();

        return new SmallIndexSet(index1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet Create(int index1, int index2)
    {
        if (index1 < 0 || index2 <= index1)
            throw new InvalidOperationException();

        return new SmallIndexSet(index1, index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet Create(int index1, int index2, int index3)
    {
        if (index1 < 0 || index2 <= index1 || index3 <= index2)
            throw new InvalidOperationException();

        return new SmallIndexSet(index1, index2, index3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet Create(int index1, int index2, int index3, int index4)
    {
        if (index1 < 0 || index2 <= index1 || index3 <= index2 || index4 <= index3)
            throw new InvalidOperationException();

        return new SmallIndexSet(index1, index2, index3, index4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet Create(int index1, int index2, int index3, int index4, int index5)
    {
        if (index1 < 0 || index2 <= index1 || index3 <= index2 || index4 <= index3 || index5 <= index4)
            throw new InvalidOperationException();

        return new SmallIndexSet(index1, index2, index3, index4, index5);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet Create(int index1, int index2, int index3, int index4, int index5, int index6)
    {
        if (index1 < 0 || index2 <= index1 || index3 <= index2 || index4 <= index3 || index5 <= index4 || index6 <= index5)
            throw new InvalidOperationException();

        return new SmallIndexSet(index1, index2, index3, index4, index5, index6);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet Create(int index1, int index2, int index3, int index4, int index5, int index6, int index7)
    {
        if (index1 < 0 || index2 <= index1 || index3 <= index2 || index4 <= index3 || index5 <= index4 || index6 <= index5 || index7 <= index6)
            throw new InvalidOperationException();

        return new SmallIndexSet(index1, index2, index3, index4, index5, index6, index7);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet Create(params int[] indexArray)
    {
        if (indexArray.Length == 0)
            return EmptySet;

        if (indexArray[0] < 0)
            throw new InvalidOperationException();

        return indexArray.Length switch
        {
            0 => EmptySet,
            1 => new SmallIndexSet(indexArray[0]),
            2 => new SmallIndexSet(indexArray[0], indexArray[1]),
            3 => new SmallIndexSet(indexArray[0], indexArray[1], indexArray[2]),
            4 => new SmallIndexSet(indexArray[0], indexArray[1], indexArray[2], indexArray[3]),
            5 => new SmallIndexSet(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4]),
            6 => new SmallIndexSet(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4], indexArray[5]),
            7 => new SmallIndexSet(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4], indexArray[5], indexArray[6]),
            _ => throw new NotSupportedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet Create(Span<int> indexArray)
    {
        if (indexArray.Length == 0)
            return EmptySet;

        if (indexArray[0] < 0)
            throw new InvalidOperationException();

        return indexArray.Length switch
        {
            0 => EmptySet,
            1 => new SmallIndexSet(indexArray[0]),
            2 => new SmallIndexSet(indexArray[0], indexArray[1]),
            3 => new SmallIndexSet(indexArray[0], indexArray[1], indexArray[2]),
            4 => new SmallIndexSet(indexArray[0], indexArray[1], indexArray[2], indexArray[3]),
            5 => new SmallIndexSet(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4]),
            6 => new SmallIndexSet(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4], indexArray[5]),
            7 => new SmallIndexSet(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4], indexArray[5], indexArray[6]),
            _ => throw new NotSupportedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet Create(ReadOnlySpan<int> indexArray)
    {
        if (indexArray.Length == 0)
            return EmptySet;

        if (indexArray[0] < 0)
            throw new InvalidOperationException();

        return indexArray.Length switch
        {
            0 => EmptySet,
            1 => new SmallIndexSet(indexArray[0]),
            2 => new SmallIndexSet(indexArray[0], indexArray[1]),
            3 => new SmallIndexSet(indexArray[0], indexArray[1], indexArray[2]),
            4 => new SmallIndexSet(indexArray[0], indexArray[1], indexArray[2], indexArray[3]),
            5 => new SmallIndexSet(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4]),
            6 => new SmallIndexSet(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4], indexArray[5]),
            7 => new SmallIndexSet(indexArray[0], indexArray[1], indexArray[2], indexArray[3], indexArray[4], indexArray[5], indexArray[6]),
            _ => throw new NotSupportedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet Create(IReadOnlyList<int> indexArray, bool assumeOrderedDistinct)
    {
        return Create(
            assumeOrderedDistinct
                ? indexArray.ToArray()
                : indexArray.Distinct().OrderBy(i => i).ToArray()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet Create(IEnumerable<int> indexArray, bool assumeOrderedDistinct)
    {
        return Create(
            assumeOrderedDistinct
                ? indexArray.ToArray()
                : indexArray.Distinct().OrderBy(i => i).ToArray()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet CreateDense(int count)
    {
        return count switch
        {
            0 => EmptySet,
            1 => new SmallIndexSet(0),
            2 => new SmallIndexSet(0, 1),
            3 => new SmallIndexSet(0, 1, 2),
            4 => new SmallIndexSet(0, 1, 2, 3),
            5 => new SmallIndexSet(0, 1, 2, 3, 4),
            6 => new SmallIndexSet(0, 1, 2, 3, 4, 5),
            7 => new SmallIndexSet(0, 1, 2, 3, 4, 5, 6),
            _ => throw new NotSupportedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet CreateDense(int firstIndex, int count)
    {
        if (firstIndex < 0)
            throw new InvalidOperationException();

        return count switch
        {
            0 => EmptySet,
            1 => new SmallIndexSet(firstIndex),
            2 => new SmallIndexSet(firstIndex, firstIndex + 1),
            3 => new SmallIndexSet(firstIndex, firstIndex + 1, firstIndex + 2),
            4 => new SmallIndexSet(firstIndex, firstIndex + 1, firstIndex + 2, firstIndex + 3),
            5 => new SmallIndexSet(firstIndex, firstIndex + 1, firstIndex + 2, firstIndex + 3, firstIndex + 4),
            6 => new SmallIndexSet(firstIndex, firstIndex + 1, firstIndex + 2, firstIndex + 3, firstIndex + 4, firstIndex + 5),
            7 => new SmallIndexSet(firstIndex, firstIndex + 1, firstIndex + 2, firstIndex + 3, firstIndex + 4, firstIndex + 5, firstIndex + 6),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet CreateFromUInt32Pattern(uint bitPattern)
    {
        return bitPattern == 0
            ? EmptySet
            : Create(bitPattern.PatternToPositions().ToArray());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet CreateFromUInt64Pattern(ulong bitPattern)
    {
        return bitPattern == 0
            ? EmptySet
            : Create(bitPattern.GetSetBitPositionsAsArray());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet EncodeUInt64AsCombinadic(int intValue, int digitsCount)
    {
        return Create(
            ((ulong)intValue).IndexToCombinadic(digitsCount),
            false
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet EncodeUInt64AsCombinadic(ulong intValue, int digitsCount)
    {
        return Create(
            intValue.IndexToCombinadic(digitsCount),
            false
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator int(SmallIndexSet indexSet)
    {
        return indexSet.ToInt32();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator uint(SmallIndexSet indexSet)
    {
        return indexSet.ToUInt32();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator long(SmallIndexSet indexSet)
    {
        return indexSet.ToInt64();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator ulong(SmallIndexSet indexSet)
    {
        return indexSet.ToUInt64();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator SmallIndexSet(int indexSet)
    {
        return indexSet == 0 ? EmptySet : Create(indexSet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator SmallIndexSet(ulong indexSet)
    {
        return indexSet == 0UL ? EmptySet : CreateFromUInt64Pattern(indexSet);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet operator <<(SmallIndexSet operand1, int shift)
    {
        return operand1.ShiftIndices(shift);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet operator >>(SmallIndexSet operand1, int shift)
    {
        return operand1.ShiftIndices(-shift);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet operator ^(ulong operand1, SmallIndexSet operand2)
    {
        return CreateFromUInt64Pattern(operand1).SetMerge(operand2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet operator ^(SmallIndexSet operand1, ulong operand2)
    {
        return operand1.SetMerge(CreateFromUInt64Pattern(operand2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet operator ^(SmallIndexSet operand1, SmallIndexSet operand2)
    {
        return operand1.SetMerge(operand2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet operator &(ulong operand1, SmallIndexSet operand2)
    {
        return CreateFromUInt64Pattern(operand1).SetIntersect(operand2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet operator &(SmallIndexSet operand1, ulong operand2)
    {
        return operand1.SetIntersect(CreateFromUInt64Pattern(operand2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet operator &(SmallIndexSet operand1, SmallIndexSet operand2)
    {
        return operand1.SetIntersect(operand2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet operator |(ulong operand1, SmallIndexSet operand2)
    {
        return CreateFromUInt64Pattern(operand1).SetUnion(operand2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet operator |(SmallIndexSet operand1, ulong operand2)
    {
        return operand1.SetUnion(CreateFromUInt64Pattern(operand2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet operator |(SmallIndexSet operand1, SmallIndexSet operand2)
    {
        return operand1.SetUnion(operand2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet operator -(ulong operand1, SmallIndexSet operand2)
    {
        return CreateFromUInt64Pattern(operand1).SetDifference(operand2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet operator -(SmallIndexSet operand1, ulong operand2)
    {
        return operand1.SetDifference(CreateFromUInt64Pattern(operand2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SmallIndexSet operator -(SmallIndexSet operand1, SmallIndexSet operand2)
    {
        return operand1.SetDifference(operand2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(SmallIndexSet operand1, SmallIndexSet operand2)
    {
        return operand1.Equals(operand2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(SmallIndexSet operand1, SmallIndexSet operand2)
    {
        return !operand1.Equals(operand2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(SmallIndexSet operand1, SmallIndexSet operand2)
    {
        return operand1.CompareTo(operand2) > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(SmallIndexSet operand1, SmallIndexSet operand2)
    {
        return operand1.CompareTo(operand2) < 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(SmallIndexSet operand1, SmallIndexSet operand2)
    {
        return operand1.CompareTo(operand2) >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(SmallIndexSet operand1, SmallIndexSet operand2)
    {
        return operand1.CompareTo(operand2) <= 0;
    }


    private const int Mask2SizeX1 = 32;
    private const ulong Mask2 = (1UL << Mask2SizeX1) - 1;

    private const int Mask3SizeX1 = 21;
    private const int Mask3SizeX2 = Mask3SizeX1 * 2;
    private const ulong Mask3 = (1UL << Mask3SizeX1) - 1;

    private const int Mask4SizeX1 = 16;
    private const int Mask4SizeX2 = Mask4SizeX1 * 2;
    private const int Mask4SizeX3 = Mask4SizeX1 * 3;
    private const ulong Mask4 = (1UL << Mask4SizeX1) - 1;

    private const int Mask5SizeX1 = 12;
    private const int Mask5SizeX2 = Mask5SizeX1 * 2;
    private const int Mask5SizeX3 = Mask5SizeX1 * 3;
    private const int Mask5SizeX4 = Mask5SizeX1 * 4;
    private const ulong Mask5 = (1UL << Mask5SizeX1) - 1;

    private const int Mask6SizeX1 = 10;
    private const int Mask6SizeX2 = Mask6SizeX1 * 2;
    private const int Mask6SizeX3 = Mask6SizeX1 * 3;
    private const int Mask6SizeX4 = Mask6SizeX1 * 4;
    private const int Mask6SizeX5 = Mask6SizeX1 * 5;
    private const ulong Mask6 = (1UL << Mask6SizeX1) - 1;

    private const int Mask7SizeX1 = 9;
    private const int Mask7SizeX2 = Mask7SizeX1 * 2;
    private const int Mask7SizeX3 = Mask7SizeX1 * 3;
    private const int Mask7SizeX4 = Mask7SizeX1 * 4;
    private const int Mask7SizeX5 = Mask7SizeX1 * 5;
    private const int Mask7SizeX6 = Mask7SizeX1 * 6;
    private const ulong Mask7 = (1UL << Mask7SizeX1) - 1;


    private readonly ulong _value;

    public bool IsEmptySet
        => Count == 0;

    public bool IsUnitSet
        => Count == 1;

    public bool IsPairSet
        => Count == 2;

    public bool IsTripletSet
        => Count == 3;

    public bool IsSparseSet
        => LastIndex - FirstIndex + 1 > Count;

    public bool IsDenseSet
        => LastIndex - FirstIndex + 1 == Count;

    public bool IsUInt64Set { get; }

    public int FirstIndex
        => this[0];

    public Pair<int> FirstIndexPair
        => new Pair<int>(this[0], this[1]);
    
    public Triplet<int> FirstIndexTriplet
        => new Triplet<int>(this[0], this[1], this[2]);

    public Quad<int> FirstIndexQuad
        => new Quad<int>(this[0], this[1], this[2], this[3]);
    
    public Quint<int> FirstIndexQuint
        => new Quint<int>(this[0], this[1], this[2], this[3], this[4]);
    
    public Hexad<int> FirstIndexHexad
        => new Hexad<int>(this[0], this[1], this[2], this[3], this[4], this[5]);

    public int LastIndex
        => this[^1];

    public Pair<int> IndexRange
        => new Pair<int>(this[0], this[^1]);

    public int Count { get; }

    public int Length
        => Count;

    public int this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            return Count switch
            {
                1 => (int)_value,

                2 => index == 0
                    ? (int)(_value & Mask2)
                    : (int)(_value >> Mask2SizeX1),

                3 => index switch
                {
                    0 => (int)(_value & Mask3),
                    1 => (int)((_value >> Mask3SizeX1) & Mask3),
                    _ => (int)(_value >> Mask3SizeX2)
                },

                4 => index switch
                {
                    0 => (int)(_value & Mask4),
                    1 => (int)((_value >> Mask4SizeX1) & Mask4),
                    2 => (int)((_value >> Mask4SizeX2) & Mask4),
                    _ => (int)(_value >> Mask4SizeX3)
                },

                5 => index switch
                {
                    0 => (int)(_value & Mask5),
                    1 => (int)((_value >> Mask5SizeX1) & Mask5),
                    2 => (int)((_value >> Mask5SizeX2) & Mask5),
                    3 => (int)((_value >> Mask5SizeX3) & Mask5),
                    _ => (int)(_value >> Mask5SizeX4)
                },

                6 => index switch
                {
                    0 => (int)(_value & Mask6),
                    1 => (int)((_value >> Mask6SizeX1) & Mask6),
                    2 => (int)((_value >> Mask6SizeX2) & Mask6),
                    3 => (int)((_value >> Mask6SizeX3) & Mask6),
                    4 => (int)((_value >> Mask6SizeX4) & Mask6),
                    _ => (int)(_value >> Mask6SizeX5)
                },

                7 => index switch
                {
                    0 => (int)(_value & Mask7),
                    1 => (int)((_value >> Mask7SizeX1) & Mask7),
                    2 => (int)((_value >> Mask7SizeX2) & Mask7),
                    3 => (int)((_value >> Mask7SizeX3) & Mask7),
                    4 => (int)((_value >> Mask7SizeX4) & Mask7),
                    5 => (int)((_value >> Mask7SizeX5) & Mask7),
                    _ => (int)(_value >> Mask7SizeX6)
                },

                _ => throw new NotSupportedException()
            };
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SmallIndexSet()
    {
        Count = 0;
        IsUInt64Set = true;
        _value = 0UL;

        _emptySetCount++;

        Debug.Assert(_emptySetCount <= 1);
        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SmallIndexSet(int i1)
    {
        Count = 1;
        IsUInt64Set = i1 < 64;
        _value = (ulong)i1;

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SmallIndexSet(int i1, int i2)
    {
        Count = 2;
        IsUInt64Set = i2 < 64;
        _value =
            (uint)i1 |
            ((ulong)i2 << Mask2SizeX1);

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SmallIndexSet(int i1, int i2, int i3)
    {
        Count = 3;
        IsUInt64Set = i3 < 64;
        _value =
            (uint)i1 |
            ((ulong)i2 << Mask3SizeX1) |
            ((ulong)i3 << Mask3SizeX2);

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SmallIndexSet(int i1, int i2, int i3, int i4)
    {
        Count = 4;
        IsUInt64Set = i4 < 64;
        _value =
            (uint)i1 |
            ((ulong)i2 << Mask4SizeX1) |
            ((ulong)i3 << Mask4SizeX2) |
            ((ulong)i4 << Mask4SizeX3);

        Debug.Assert(IsValid());

    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SmallIndexSet(int i1, int i2, int i3, int i4, int i5)
    {
        Count = 5;
        IsUInt64Set = i5 < 64;
        _value =
            (uint)i1 |
            ((ulong)i2 << Mask5SizeX1) |
            ((ulong)i3 << Mask5SizeX2) |
            ((ulong)i4 << Mask5SizeX3) |
            ((ulong)i5 << Mask5SizeX4);

        Debug.Assert(IsValid());

    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SmallIndexSet(int i1, int i2, int i3, int i4, int i5, int i6)
    {
        Count = 6;
        IsUInt64Set = i6 < 64;
        _value =
            (uint)i1 |
            ((ulong)i2 << Mask6SizeX1) |
            ((ulong)i3 << Mask6SizeX2) |
            ((ulong)i4 << Mask6SizeX3) |
            ((ulong)i5 << Mask6SizeX4) |
            ((ulong)i6 << Mask6SizeX5);

        Debug.Assert(IsValid());

    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SmallIndexSet(int i1, int i2, int i3, int i4, int i5, int i6, int i7)
    {
        Count = 7;
        IsUInt64Set = i7 < 64;
        _value =
            (uint)i1 |
            ((ulong)i2 << Mask7SizeX1) |
            ((ulong)i3 << Mask7SizeX2) |
            ((ulong)i4 << Mask7SizeX3) |
            ((ulong)i5 << Mask7SizeX4) |
            ((ulong)i6 << Mask7SizeX5) |
            ((ulong)i7 << Mask7SizeX6);

        Debug.Assert(IsValid());
    }


    public bool IsValid()
    {
        if (Count == 0) return _value == 0;

        var firstIndex = this[0];
        if (firstIndex < 0) return false;

        for (var i = 1; i < Count; i++)
        {
            var nextIndex = this[i];
            if (nextIndex <= firstIndex) return false;

            firstIndex = nextIndex;
        }

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return obj is SmallIndexSet other && Equals(other);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(SmallIndexSet other)
    {
        return Count == other.Count && _value == other._value;
    }

    public override int GetHashCode()
    {
        if (IsEmptySet) return 0;

        var hashCode = 0;

        foreach (var index in GetIndices())
            hashCode ^= index;

        return hashCode;
    }

    public int CompareTo(SmallIndexSet other)
    {
        var n1 = Count;
        var n2 = other.Count;

        while (n1 > 0 && n2 > 0)
        {
            n1--;
            n2--;

            var index1 = this[n1];
            var index2 = other[n2];

            if (index1 > index2) return 1;
            if (index1 < index2) return -1;
        }

        if (n1 == 0) return n2 == 0 ? 0 : -1;

        return 1;
    }


    public int GetIndexPosition(int index)
    {
        var low = 0;
        var high = Count - 1;

        while (low <= high)
        {
            var mid = (int)((uint)low + (uint)(high - low) / 2);
            var midValue = this[mid];

            if (midValue < index)
                low = mid + 1;
            else if (midValue > index)
                high = mid - 1;
            else
                return mid; // Found
        }

        throw new InvalidOperationException(); // Not found
    }

    public int TryGetIndexPosition(int index)
    {
        var low = 0;
        var high = Count - 1;

        while (low <= high)
        {
            var mid = (int)((uint)low + (uint)(high - low) / 2);
            var midValue = this[mid];

            if (midValue < index)
                low = mid + 1;
            else if (midValue > index)
                high = mid - 1;
            else
                return mid; // Found
        }

        return ~low; // Not found, return bitwise complement of insertion point
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int[] GetInternalIndexArray()
    {
        return Count switch
        {
            0 => [],

            1 => [
                (int)_value
            ],

            2 => [
                (int)(_value & Mask2),
                (int)(_value >> Mask2SizeX1)
            ],

            3 => [
                (int)(_value & Mask3),
                (int)((_value >> Mask3SizeX1) & Mask3),
                (int)(_value >> Mask3SizeX2)
            ],

            4 => [
                (int)(_value & Mask4),
                (int)((_value >> Mask4SizeX1) & Mask4),
                (int)((_value >> Mask4SizeX2) & Mask4),
                (int)(_value >> Mask4SizeX3)
            ],

            5 => [
                (int)(_value & Mask5),
                (int)((_value >> Mask5SizeX1) & Mask5),
                (int)((_value >> Mask5SizeX2) & Mask5),
                (int)((_value >> Mask5SizeX3) & Mask5),
                (int)(_value >> Mask5SizeX4)
            ],

            6 => [
                (int)(_value & Mask6),
                (int)((_value >> Mask6SizeX1) & Mask6),
                (int)((_value >> Mask6SizeX2) & Mask6),
                (int)((_value >> Mask6SizeX3) & Mask6),
                (int)((_value >> Mask6SizeX4) & Mask6),
                (int)(_value >> Mask6SizeX5)
            ],

            7 => [
                (int)(_value & Mask7),
                (int)((_value >> Mask7SizeX1) & Mask7),
                (int)((_value >> Mask7SizeX2) & Mask7),
                (int)((_value >> Mask7SizeX3) & Mask7),
                (int)((_value >> Mask7SizeX4) & Mask7),
                (int)((_value >> Mask7SizeX5) & Mask7),
                (int)(_value >> Mask7SizeX6)
            ],

            _ => throw new NotSupportedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<int> AsSpan()
    {
        return new ReadOnlySpan<int>(GetInternalIndexArray());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SmallIndexSet GetSubset(int index, int count)
    {
        return count == 0
            ? EmptySet
            : Create(AsSpan().Slice(index, count));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<int> GetIndices()
    {
        if (Count == 0) yield break;

        if (Count == 1)
        {
            yield return (int)_value;
        }
        else if (Count == 2)
        {
            yield return (int)(_value & Mask2);
            yield return (int)(_value >> Mask2SizeX1);
        }
        else if (Count == 3)
        {

            yield return (int)(_value & Mask3);
            yield return (int)((_value >> Mask3SizeX1) & Mask3);
            yield return (int)(_value >> Mask3SizeX2);
        }
        else if (Count == 4)
        {
            yield return (int)(_value & Mask4);
            yield return (int)((_value >> Mask4SizeX1) & Mask4);
            yield return (int)((_value >> Mask4SizeX2) & Mask4);
            yield return (int)(_value >> Mask4SizeX3);
        }
        else if (Count == 5)
        {
            yield return (int)(_value & Mask5);
            yield return (int)((_value >> Mask5SizeX1) & Mask5);
            yield return (int)((_value >> Mask5SizeX2) & Mask5);
            yield return (int)((_value >> Mask5SizeX3) & Mask5);
            yield return (int)(_value >> Mask5SizeX4);
        }
        else if (Count == 6)
        {
            yield return (int)(_value & Mask6);
            yield return (int)((_value >> Mask6SizeX1) & Mask6);
            yield return (int)((_value >> Mask6SizeX2) & Mask6);
            yield return (int)((_value >> Mask6SizeX3) & Mask6);
            yield return (int)((_value >> Mask6SizeX4) & Mask6);
            yield return (int)(_value >> Mask6SizeX5);
        }
        else if (Count == 7)
        {
            yield return (int)(_value & Mask7);
            yield return (int)((_value >> Mask7SizeX1) & Mask7);
            yield return (int)((_value >> Mask7SizeX2) & Mask7);
            yield return (int)((_value >> Mask7SizeX3) & Mask7);
            yield return (int)((_value >> Mask7SizeX4) & Mask7);
            yield return (int)((_value >> Mask7SizeX5) & Mask7);
            yield return (int)(_value >> Mask7SizeX6);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<T> GetIndices<T>(Func<int, T> indexMapFunc)
    {
        return GetIndices().Select(indexMapFunc);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<int> GetReversedIndices()
    {
        if (Count == 0) yield break;

        if (Count == 1)
        {
            yield return (int)_value;
        }
        else if (Count == 2)
        {
            yield return (int)(_value >> Mask2SizeX1);
            yield return (int)(_value & Mask2);
        }
        else if (Count == 3)
        {
            yield return (int)(_value >> Mask3SizeX2);
            yield return (int)((_value >> Mask3SizeX1) & Mask3);
            yield return (int)(_value & Mask3);
        }
        else if (Count == 4)
        {
            yield return (int)(_value >> Mask4SizeX3);
            yield return (int)((_value >> Mask4SizeX2) & Mask4);
            yield return (int)((_value >> Mask4SizeX1) & Mask4);
            yield return (int)(_value & Mask4);
        }
        else if (Count == 5)
        {
            yield return (int)(_value >> Mask5SizeX4);
            yield return (int)((_value >> Mask5SizeX3) & Mask5);
            yield return (int)((_value >> Mask5SizeX2) & Mask5);
            yield return (int)((_value >> Mask5SizeX1) & Mask5);
            yield return (int)(_value & Mask5);
        }
        else if (Count == 6)
        {
            yield return (int)(_value >> Mask6SizeX5);
            yield return (int)((_value >> Mask6SizeX4) & Mask6);
            yield return (int)((_value >> Mask6SizeX3) & Mask6);
            yield return (int)((_value >> Mask6SizeX2) & Mask6);
            yield return (int)((_value >> Mask6SizeX1) & Mask6);
            yield return (int)(_value & Mask6);
        }
        else if (Count == 7)
        {
            yield return (int)(_value >> Mask7SizeX6);
            yield return (int)((_value >> Mask7SizeX5) & Mask7);
            yield return (int)((_value >> Mask7SizeX4) & Mask7);
            yield return (int)((_value >> Mask7SizeX3) & Mask7);
            yield return (int)((_value >> Mask7SizeX2) & Mask7);
            yield return (int)((_value >> Mask7SizeX1) & Mask7);
            yield return (int)(_value & Mask7);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<T> GetReversedIndices<T>(Func<int, T> indexMapFunc)
    {
        return GetReversedIndices().Select(indexMapFunc);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets00()
    {
        yield return EmptySet;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets10()
    {
        yield return EmptySet;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets11()
    {
        var i1 = (int)_value;
        
        yield return new SmallIndexSet(i1);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets20()
    {
        yield return EmptySet;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets21()
    {
        var i1 = (int)(_value & Mask2);
        var i2 = (int)(_value >> Mask2SizeX1);
        
        yield return new SmallIndexSet(i1);
        yield return new SmallIndexSet(i2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets22()
    {
        var i1 = (int)(_value & Mask2);
        var i2 = (int)(_value >> Mask2SizeX1);
        
        yield return new SmallIndexSet(i1, i2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets30()
    {
        yield return EmptySet;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets31()
    {
        var i1 = (int)(_value & Mask3);
        var i2 = (int)((_value >> Mask3SizeX1) & Mask3);
        var i3 = (int)(_value >> Mask3SizeX2);
        
        yield return new SmallIndexSet(i1);
        yield return new SmallIndexSet(i2);
        yield return new SmallIndexSet(i3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets32()
    {
        var i1 = (int)(_value & Mask3);
        var i2 = (int)((_value >> Mask3SizeX1) & Mask3);
        var i3 = (int)(_value >> Mask3SizeX2);
        
        yield return new SmallIndexSet(i1, i2);
        yield return new SmallIndexSet(i1, i3);
        yield return new SmallIndexSet(i2, i3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets33()
    {
        var i1 = (int)(_value & Mask3);
        var i2 = (int)((_value >> Mask3SizeX1) & Mask3);
        var i3 = (int)(_value >> Mask3SizeX2);
        
        yield return new SmallIndexSet(i1, i2, i3);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets40()
    {
        yield return EmptySet;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets41()
    {
        var i1 = (int)(_value & Mask4);
        var i2 = (int)((_value >> Mask4SizeX1) & Mask4);
        var i3 = (int)((_value >> Mask4SizeX2) & Mask4);
        var i4 = (int)(_value >> Mask4SizeX3);
        
        yield return new SmallIndexSet(i1);
        yield return new SmallIndexSet(i2);
        yield return new SmallIndexSet(i3);
        yield return new SmallIndexSet(i4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets42()
    {
        var i1 = (int)(_value & Mask4);
        var i2 = (int)((_value >> Mask4SizeX1) & Mask4);
        var i3 = (int)((_value >> Mask4SizeX2) & Mask4);
        var i4 = (int)(_value >> Mask4SizeX3);
        
        yield return new SmallIndexSet(i1, i2);
        yield return new SmallIndexSet(i1, i3);
        yield return new SmallIndexSet(i2, i3);
        yield return new SmallIndexSet(i1, i4);
        yield return new SmallIndexSet(i2, i4);
        yield return new SmallIndexSet(i3, i4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets43()
    {
        var i1 = (int)(_value & Mask4);
        var i2 = (int)((_value >> Mask4SizeX1) & Mask4);
        var i3 = (int)((_value >> Mask4SizeX2) & Mask4);
        var i4 = (int)(_value >> Mask4SizeX3);
        
        yield return new SmallIndexSet(i1, i2, i3);
        yield return new SmallIndexSet(i1, i2, i4);
        yield return new SmallIndexSet(i1, i3, i4);
        yield return new SmallIndexSet(i2, i3, i4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets44()
    {
        var i1 = (int)(_value & Mask4);
        var i2 = (int)((_value >> Mask4SizeX1) & Mask4);
        var i3 = (int)((_value >> Mask4SizeX2) & Mask4);
        var i4 = (int)(_value >> Mask4SizeX3);
        
        yield return new SmallIndexSet(i1, i2, i3, i4);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets50()
    {
        yield return EmptySet;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets51()
    {
        var i1 = (int)(_value & Mask5);
        var i2 = (int)((_value >> Mask5SizeX1) & Mask5);
        var i3 = (int)((_value >> Mask5SizeX2) & Mask5);
        var i4 = (int)((_value >> Mask5SizeX3) & Mask5);
        var i5 = (int)(_value >> Mask5SizeX4);

        yield return new SmallIndexSet(i1);
        yield return new SmallIndexSet(i2);
        yield return new SmallIndexSet(i3);
        yield return new SmallIndexSet(i4);
        yield return new SmallIndexSet(i5);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets52()
    {
        var i1 = (int)(_value & Mask5);
        var i2 = (int)((_value >> Mask5SizeX1) & Mask5);
        var i3 = (int)((_value >> Mask5SizeX2) & Mask5);
        var i4 = (int)((_value >> Mask5SizeX3) & Mask5);
        var i5 = (int)(_value >> Mask5SizeX4);

        yield return new SmallIndexSet(i1, i2);
        yield return new SmallIndexSet(i1, i3);
        yield return new SmallIndexSet(i2, i3);
        yield return new SmallIndexSet(i1, i4);
        yield return new SmallIndexSet(i2, i4);
        yield return new SmallIndexSet(i3, i4);
        yield return new SmallIndexSet(i1, i5);
        yield return new SmallIndexSet(i2, i5);
        yield return new SmallIndexSet(i3, i5);
        yield return new SmallIndexSet(i4, i5);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets53()
    {
        var i1 = (int)(_value & Mask5);
        var i2 = (int)((_value >> Mask5SizeX1) & Mask5);
        var i3 = (int)((_value >> Mask5SizeX2) & Mask5);
        var i4 = (int)((_value >> Mask5SizeX3) & Mask5);
        var i5 = (int)(_value >> Mask5SizeX4);

        yield return new SmallIndexSet(i1, i2, i3);
        yield return new SmallIndexSet(i1, i2, i4);
        yield return new SmallIndexSet(i1, i3, i4);
        yield return new SmallIndexSet(i2, i3, i4);
        yield return new SmallIndexSet(i1, i2, i5);
        yield return new SmallIndexSet(i1, i3, i5);
        yield return new SmallIndexSet(i2, i3, i5);
        yield return new SmallIndexSet(i1, i4, i5);
        yield return new SmallIndexSet(i2, i4, i5);
        yield return new SmallIndexSet(i3, i4, i5);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets54()
    {
        var i1 = (int)(_value & Mask5);
        var i2 = (int)((_value >> Mask5SizeX1) & Mask5);
        var i3 = (int)((_value >> Mask5SizeX2) & Mask5);
        var i4 = (int)((_value >> Mask5SizeX3) & Mask5);
        var i5 = (int)(_value >> Mask5SizeX4);

        yield return new SmallIndexSet(i1, i2, i3, i4);
        yield return new SmallIndexSet(i1, i2, i3, i5);
        yield return new SmallIndexSet(i1, i2, i4, i5);
        yield return new SmallIndexSet(i1, i3, i4, i5);
        yield return new SmallIndexSet(i2, i3, i4, i5);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets55()
    {
        var i1 = (int)(_value & Mask5);
        var i2 = (int)((_value >> Mask5SizeX1) & Mask5);
        var i3 = (int)((_value >> Mask5SizeX2) & Mask5);
        var i4 = (int)((_value >> Mask5SizeX3) & Mask5);
        var i5 = (int)(_value >> Mask5SizeX4);

        yield return new SmallIndexSet(i1, i2, i3, i4, i5);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets60()
    {
        yield return EmptySet;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets61()
    {
        var i1 = (int)(_value & Mask6);
        var i2 = (int)((_value >> Mask6SizeX1) & Mask6);
        var i3 = (int)((_value >> Mask6SizeX2) & Mask6);
        var i4 = (int)((_value >> Mask6SizeX3) & Mask6);
        var i5 = (int)((_value >> Mask6SizeX4) & Mask6);
        var i6 = (int)(_value >> Mask6SizeX5);

        yield return new SmallIndexSet(i1);
        yield return new SmallIndexSet(i2);
        yield return new SmallIndexSet(i3);
        yield return new SmallIndexSet(i4);
        yield return new SmallIndexSet(i5);
        yield return new SmallIndexSet(i6);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets62()
    {
        var i1 = (int)(_value & Mask6);
        var i2 = (int)((_value >> Mask6SizeX1) & Mask6);
        var i3 = (int)((_value >> Mask6SizeX2) & Mask6);
        var i4 = (int)((_value >> Mask6SizeX3) & Mask6);
        var i5 = (int)((_value >> Mask6SizeX4) & Mask6);
        var i6 = (int)(_value >> Mask6SizeX5);

        yield return new SmallIndexSet(i1, i2);
        yield return new SmallIndexSet(i1, i3);
        yield return new SmallIndexSet(i2, i3);
        yield return new SmallIndexSet(i1, i4);
        yield return new SmallIndexSet(i2, i4);
        yield return new SmallIndexSet(i3, i4);
        yield return new SmallIndexSet(i1, i5);
        yield return new SmallIndexSet(i2, i5);
        yield return new SmallIndexSet(i3, i5);
        yield return new SmallIndexSet(i4, i5);
        yield return new SmallIndexSet(i1, i6);
        yield return new SmallIndexSet(i2, i6);
        yield return new SmallIndexSet(i3, i6);
        yield return new SmallIndexSet(i4, i6);
        yield return new SmallIndexSet(i5, i6);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets63()
    {
        var i1 = (int)(_value & Mask6);
        var i2 = (int)((_value >> Mask6SizeX1) & Mask6);
        var i3 = (int)((_value >> Mask6SizeX2) & Mask6);
        var i4 = (int)((_value >> Mask6SizeX3) & Mask6);
        var i5 = (int)((_value >> Mask6SizeX4) & Mask6);
        var i6 = (int)(_value >> Mask6SizeX5);

        yield return new SmallIndexSet(i1, i2, i3);
        yield return new SmallIndexSet(i1, i2, i4);
        yield return new SmallIndexSet(i1, i3, i4);
        yield return new SmallIndexSet(i2, i3, i4);
        yield return new SmallIndexSet(i1, i2, i5);
        yield return new SmallIndexSet(i1, i3, i5);
        yield return new SmallIndexSet(i2, i3, i5);
        yield return new SmallIndexSet(i1, i4, i5);
        yield return new SmallIndexSet(i2, i4, i5);
        yield return new SmallIndexSet(i3, i4, i5);
        yield return new SmallIndexSet(i1, i2, i6);
        yield return new SmallIndexSet(i1, i3, i6);
        yield return new SmallIndexSet(i2, i3, i6);
        yield return new SmallIndexSet(i1, i4, i6);
        yield return new SmallIndexSet(i2, i4, i6);
        yield return new SmallIndexSet(i3, i4, i6);
        yield return new SmallIndexSet(i1, i5, i6);
        yield return new SmallIndexSet(i2, i5, i6);
        yield return new SmallIndexSet(i3, i5, i6);
        yield return new SmallIndexSet(i4, i5, i6);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets64()
    {
        var i1 = (int)(_value & Mask6);
        var i2 = (int)((_value >> Mask6SizeX1) & Mask6);
        var i3 = (int)((_value >> Mask6SizeX2) & Mask6);
        var i4 = (int)((_value >> Mask6SizeX3) & Mask6);
        var i5 = (int)((_value >> Mask6SizeX4) & Mask6);
        var i6 = (int)(_value >> Mask6SizeX5);

        yield return new SmallIndexSet(i1, i2, i3, i4);
        yield return new SmallIndexSet(i1, i2, i3, i5);
        yield return new SmallIndexSet(i1, i2, i4, i5);
        yield return new SmallIndexSet(i1, i3, i4, i5);
        yield return new SmallIndexSet(i2, i3, i4, i5);
        yield return new SmallIndexSet(i1, i2, i3, i6);
        yield return new SmallIndexSet(i1, i2, i4, i6);
        yield return new SmallIndexSet(i1, i3, i4, i6);
        yield return new SmallIndexSet(i2, i3, i4, i6);
        yield return new SmallIndexSet(i1, i2, i5, i6);
        yield return new SmallIndexSet(i1, i3, i5, i6);
        yield return new SmallIndexSet(i2, i3, i5, i6);
        yield return new SmallIndexSet(i1, i4, i5, i6);
        yield return new SmallIndexSet(i2, i4, i5, i6);
        yield return new SmallIndexSet(i3, i4, i5, i6);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets65()
    {
        var i1 = (int)(_value & Mask6);
        var i2 = (int)((_value >> Mask6SizeX1) & Mask6);
        var i3 = (int)((_value >> Mask6SizeX2) & Mask6);
        var i4 = (int)((_value >> Mask6SizeX3) & Mask6);
        var i5 = (int)((_value >> Mask6SizeX4) & Mask6);
        var i6 = (int)(_value >> Mask6SizeX5);

        yield return new SmallIndexSet(i1, i2, i3, i4, i5);
        yield return new SmallIndexSet(i1, i2, i3, i4, i6);
        yield return new SmallIndexSet(i1, i2, i3, i5, i6);
        yield return new SmallIndexSet(i1, i2, i4, i5, i6);
        yield return new SmallIndexSet(i1, i3, i4, i5, i6);
        yield return new SmallIndexSet(i2, i3, i4, i5, i6);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets66()
    {
        var i1 = (int)(_value & Mask6);
        var i2 = (int)((_value >> Mask6SizeX1) & Mask6);
        var i3 = (int)((_value >> Mask6SizeX2) & Mask6);
        var i4 = (int)((_value >> Mask6SizeX3) & Mask6);
        var i5 = (int)((_value >> Mask6SizeX4) & Mask6);
        var i6 = (int)(_value >> Mask6SizeX5);

        yield return new SmallIndexSet(i1, i2, i3, i4, i5, i6);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets70()
    {
        yield return EmptySet;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets71()
    {
        var i1 = (int)(_value & Mask7);
        var i2 = (int)((_value >> Mask7SizeX1) & Mask7);
        var i3 = (int)((_value >> Mask7SizeX2) & Mask7);
        var i4 = (int)((_value >> Mask7SizeX3) & Mask7);
        var i5 = (int)((_value >> Mask7SizeX4) & Mask7);
        var i6 = (int)((_value >> Mask7SizeX5) & Mask7);
        var i7 = (int)(_value >> Mask7SizeX6);

        yield return new SmallIndexSet(i1);
        yield return new SmallIndexSet(i2);
        yield return new SmallIndexSet(i3);
        yield return new SmallIndexSet(i4);
        yield return new SmallIndexSet(i5);
        yield return new SmallIndexSet(i6);
        yield return new SmallIndexSet(i7);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets72()
    {
        var i1 = (int)(_value & Mask7);
        var i2 = (int)((_value >> Mask7SizeX1) & Mask7);
        var i3 = (int)((_value >> Mask7SizeX2) & Mask7);
        var i4 = (int)((_value >> Mask7SizeX3) & Mask7);
        var i5 = (int)((_value >> Mask7SizeX4) & Mask7);
        var i6 = (int)((_value >> Mask7SizeX5) & Mask7);
        var i7 = (int)(_value >> Mask7SizeX6);

        yield return new SmallIndexSet(i1, i2);
        yield return new SmallIndexSet(i1, i3);
        yield return new SmallIndexSet(i2, i3);
        yield return new SmallIndexSet(i1, i4);
        yield return new SmallIndexSet(i2, i4);
        yield return new SmallIndexSet(i3, i4);
        yield return new SmallIndexSet(i1, i5);
        yield return new SmallIndexSet(i2, i5);
        yield return new SmallIndexSet(i3, i5);
        yield return new SmallIndexSet(i4, i5);
        yield return new SmallIndexSet(i1, i6);
        yield return new SmallIndexSet(i2, i6);
        yield return new SmallIndexSet(i3, i6);
        yield return new SmallIndexSet(i4, i6);
        yield return new SmallIndexSet(i5, i6);
        yield return new SmallIndexSet(i1, i7);
        yield return new SmallIndexSet(i2, i7);
        yield return new SmallIndexSet(i3, i7);
        yield return new SmallIndexSet(i4, i7);
        yield return new SmallIndexSet(i5, i7);
        yield return new SmallIndexSet(i6, i7);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets73()
    {
        var i1 = (int)(_value & Mask7);
        var i2 = (int)((_value >> Mask7SizeX1) & Mask7);
        var i3 = (int)((_value >> Mask7SizeX2) & Mask7);
        var i4 = (int)((_value >> Mask7SizeX3) & Mask7);
        var i5 = (int)((_value >> Mask7SizeX4) & Mask7);
        var i6 = (int)((_value >> Mask7SizeX5) & Mask7);
        var i7 = (int)(_value >> Mask7SizeX6);

        yield return new SmallIndexSet(i1, i2, i3);
        yield return new SmallIndexSet(i1, i2, i4);
        yield return new SmallIndexSet(i1, i3, i4);
        yield return new SmallIndexSet(i2, i3, i4);
        yield return new SmallIndexSet(i1, i2, i5);
        yield return new SmallIndexSet(i1, i3, i5);
        yield return new SmallIndexSet(i2, i3, i5);
        yield return new SmallIndexSet(i1, i4, i5);
        yield return new SmallIndexSet(i2, i4, i5);
        yield return new SmallIndexSet(i3, i4, i5);
        yield return new SmallIndexSet(i1, i2, i6);
        yield return new SmallIndexSet(i1, i3, i6);
        yield return new SmallIndexSet(i2, i3, i6);
        yield return new SmallIndexSet(i1, i4, i6);
        yield return new SmallIndexSet(i2, i4, i6);
        yield return new SmallIndexSet(i3, i4, i6);
        yield return new SmallIndexSet(i1, i5, i6);
        yield return new SmallIndexSet(i2, i5, i6);
        yield return new SmallIndexSet(i3, i5, i6);
        yield return new SmallIndexSet(i4, i5, i6);
        yield return new SmallIndexSet(i1, i2, i7);
        yield return new SmallIndexSet(i1, i3, i7);
        yield return new SmallIndexSet(i2, i3, i7);
        yield return new SmallIndexSet(i1, i4, i7);
        yield return new SmallIndexSet(i2, i4, i7);
        yield return new SmallIndexSet(i3, i4, i7);
        yield return new SmallIndexSet(i1, i5, i7);
        yield return new SmallIndexSet(i2, i5, i7);
        yield return new SmallIndexSet(i3, i5, i7);
        yield return new SmallIndexSet(i4, i5, i7);
        yield return new SmallIndexSet(i1, i6, i7);
        yield return new SmallIndexSet(i2, i6, i7);
        yield return new SmallIndexSet(i3, i6, i7);
        yield return new SmallIndexSet(i4, i6, i7);
        yield return new SmallIndexSet(i5, i6, i7);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets74()
    {
        var i1 = (int)(_value & Mask7);
        var i2 = (int)((_value >> Mask7SizeX1) & Mask7);
        var i3 = (int)((_value >> Mask7SizeX2) & Mask7);
        var i4 = (int)((_value >> Mask7SizeX3) & Mask7);
        var i5 = (int)((_value >> Mask7SizeX4) & Mask7);
        var i6 = (int)((_value >> Mask7SizeX5) & Mask7);
        var i7 = (int)(_value >> Mask7SizeX6);

        yield return new SmallIndexSet(i1, i2, i3, i4);
        yield return new SmallIndexSet(i1, i2, i3, i5);
        yield return new SmallIndexSet(i1, i2, i4, i5);
        yield return new SmallIndexSet(i1, i3, i4, i5);
        yield return new SmallIndexSet(i2, i3, i4, i5);
        yield return new SmallIndexSet(i1, i2, i3, i6);
        yield return new SmallIndexSet(i1, i2, i4, i6);
        yield return new SmallIndexSet(i1, i3, i4, i6);
        yield return new SmallIndexSet(i2, i3, i4, i6);
        yield return new SmallIndexSet(i1, i2, i5, i6);
        yield return new SmallIndexSet(i1, i3, i5, i6);
        yield return new SmallIndexSet(i2, i3, i5, i6);
        yield return new SmallIndexSet(i1, i4, i5, i6);
        yield return new SmallIndexSet(i2, i4, i5, i6);
        yield return new SmallIndexSet(i3, i4, i5, i6);
        yield return new SmallIndexSet(i1, i2, i3, i7);
        yield return new SmallIndexSet(i1, i2, i4, i7);
        yield return new SmallIndexSet(i1, i3, i4, i7);
        yield return new SmallIndexSet(i2, i3, i4, i7);
        yield return new SmallIndexSet(i1, i2, i5, i7);
        yield return new SmallIndexSet(i1, i3, i5, i7);
        yield return new SmallIndexSet(i2, i3, i5, i7);
        yield return new SmallIndexSet(i1, i4, i5, i7);
        yield return new SmallIndexSet(i2, i4, i5, i7);
        yield return new SmallIndexSet(i3, i4, i5, i7);
        yield return new SmallIndexSet(i1, i2, i6, i7);
        yield return new SmallIndexSet(i1, i3, i6, i7);
        yield return new SmallIndexSet(i2, i3, i6, i7);
        yield return new SmallIndexSet(i1, i4, i6, i7);
        yield return new SmallIndexSet(i2, i4, i6, i7);
        yield return new SmallIndexSet(i3, i4, i6, i7);
        yield return new SmallIndexSet(i1, i5, i6, i7);
        yield return new SmallIndexSet(i2, i5, i6, i7);
        yield return new SmallIndexSet(i3, i5, i6, i7);
        yield return new SmallIndexSet(i4, i5, i6, i7);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets75()
    {
        var i1 = (int)(_value & Mask7);
        var i2 = (int)((_value >> Mask7SizeX1) & Mask7);
        var i3 = (int)((_value >> Mask7SizeX2) & Mask7);
        var i4 = (int)((_value >> Mask7SizeX3) & Mask7);
        var i5 = (int)((_value >> Mask7SizeX4) & Mask7);
        var i6 = (int)((_value >> Mask7SizeX5) & Mask7);
        var i7 = (int)(_value >> Mask7SizeX6);

        yield return new SmallIndexSet(i1, i2, i3, i4, i5);
        yield return new SmallIndexSet(i1, i2, i3, i4, i6);
        yield return new SmallIndexSet(i1, i2, i3, i5, i6);
        yield return new SmallIndexSet(i1, i2, i4, i5, i6);
        yield return new SmallIndexSet(i1, i3, i4, i5, i6);
        yield return new SmallIndexSet(i2, i3, i4, i5, i6);
        yield return new SmallIndexSet(i1, i2, i3, i4, i7);
        yield return new SmallIndexSet(i1, i2, i3, i5, i7);
        yield return new SmallIndexSet(i1, i2, i4, i5, i7);
        yield return new SmallIndexSet(i1, i3, i4, i5, i7);
        yield return new SmallIndexSet(i2, i3, i4, i5, i7);
        yield return new SmallIndexSet(i1, i2, i3, i6, i7);
        yield return new SmallIndexSet(i1, i2, i4, i6, i7);
        yield return new SmallIndexSet(i1, i3, i4, i6, i7);
        yield return new SmallIndexSet(i2, i3, i4, i6, i7);
        yield return new SmallIndexSet(i1, i2, i5, i6, i7);
        yield return new SmallIndexSet(i1, i3, i5, i6, i7);
        yield return new SmallIndexSet(i2, i3, i5, i6, i7);
        yield return new SmallIndexSet(i1, i4, i5, i6, i7);
        yield return new SmallIndexSet(i2, i4, i5, i6, i7);
        yield return new SmallIndexSet(i3, i4, i5, i6, i7);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets76()
    {
        var i1 = (int)(_value & Mask7);
        var i2 = (int)((_value >> Mask7SizeX1) & Mask7);
        var i3 = (int)((_value >> Mask7SizeX2) & Mask7);
        var i4 = (int)((_value >> Mask7SizeX3) & Mask7);
        var i5 = (int)((_value >> Mask7SizeX4) & Mask7);
        var i6 = (int)((_value >> Mask7SizeX5) & Mask7);
        var i7 = (int)(_value >> Mask7SizeX6);

        yield return new SmallIndexSet(i1, i2, i3, i4, i5, i6);
        yield return new SmallIndexSet(i1, i2, i3, i4, i5, i7);
        yield return new SmallIndexSet(i1, i2, i3, i4, i6, i7);
        yield return new SmallIndexSet(i1, i2, i3, i5, i6, i7);
        yield return new SmallIndexSet(i1, i2, i4, i5, i6, i7);
        yield return new SmallIndexSet(i1, i3, i4, i5, i6, i7);
        yield return new SmallIndexSet(i2, i3, i4, i5, i6, i7);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets77()
    {
        var i1 = (int)(_value & Mask7);
        var i2 = (int)((_value >> Mask7SizeX1) & Mask7);
        var i3 = (int)((_value >> Mask7SizeX2) & Mask7);
        var i4 = (int)((_value >> Mask7SizeX3) & Mask7);
        var i5 = (int)((_value >> Mask7SizeX4) & Mask7);
        var i6 = (int)((_value >> Mask7SizeX5) & Mask7);
        var i7 = (int)(_value >> Mask7SizeX6);

        yield return new SmallIndexSet(i1, i2, i3, i4, i5, i6, i7);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets0(int k)
    {
        return k switch
        {
            0 => GetSubsets00(),
            _ => []
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets1(int k)
    {
        return k switch
        {
            0 => GetSubsets10(),
            1 => GetSubsets11(),
            _ => []
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets2(int k)
    {
        return k switch
        {
            0 => GetSubsets20(),
            1 => GetSubsets21(),
            2 => GetSubsets22(),
            _ => []
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets3(int k)
    {
        return k switch
        {
            0 => GetSubsets30(),
            1 => GetSubsets31(),
            2 => GetSubsets32(),
            3 => GetSubsets33(),
            _ => []
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets4(int k)
    {
        return k switch
        {
            0 => GetSubsets40(),
            1 => GetSubsets41(),
            2 => GetSubsets42(),
            3 => GetSubsets43(),
            4 => GetSubsets44(),
            _ => []
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets5(int k)
    {
        return k switch
        {
            0 => GetSubsets50(),
            1 => GetSubsets51(),
            2 => GetSubsets52(),
            3 => GetSubsets53(),
            4 => GetSubsets54(),
            5 => GetSubsets55(),
            _ => []
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets6(int k)
    {
        return k switch
        {
            0 => GetSubsets60(),
            1 => GetSubsets61(),
            2 => GetSubsets62(),
            3 => GetSubsets63(),
            4 => GetSubsets64(),
            5 => GetSubsets65(),
            6 => GetSubsets66(),
            _ => []
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets7(int k)
    {
        return k switch
        {
            0 => GetSubsets70(),
            1 => GetSubsets71(),
            2 => GetSubsets72(),
            3 => GetSubsets73(),
            4 => GetSubsets74(),
            5 => GetSubsets75(),
            6 => GetSubsets76(),
            7 => GetSubsets77(),
            _ => []
        };
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets0()
    {
        return GetSubsets00();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets1()
    {
        return GetSubsets10()
            .Concat(GetSubsets11());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets2()
    {
        return GetSubsets20()
            .Concat(GetSubsets21())
            .Concat(GetSubsets22());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets3()
    {
        return GetSubsets30()
            .Concat(GetSubsets31())
            .Concat(GetSubsets32())
            .Concat(GetSubsets33());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets4()
    {
        return GetSubsets40()
            .Concat(GetSubsets41())
            .Concat(GetSubsets42())
            .Concat(GetSubsets43())
            .Concat(GetSubsets44());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets5()
    {
        return GetSubsets50()
            .Concat(GetSubsets51())
            .Concat(GetSubsets52())
            .Concat(GetSubsets53())
            .Concat(GetSubsets54())
            .Concat(GetSubsets55());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets6()
    {
        return GetSubsets60()
            .Concat(GetSubsets61())
            .Concat(GetSubsets62())
            .Concat(GetSubsets63())
            .Concat(GetSubsets64())
            .Concat(GetSubsets65())
            .Concat(GetSubsets66());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<SmallIndexSet> GetSubsets7()
    {
        return GetSubsets70()
            .Concat(GetSubsets71())
            .Concat(GetSubsets72())
            .Concat(GetSubsets73())
            .Concat(GetSubsets74())
            .Concat(GetSubsets75())
            .Concat(GetSubsets76())
            .Concat(GetSubsets77());
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<SmallIndexSet> GetUnitSubsets()
    {
        return Count switch
        {
            1 => GetSubsets11(),
            2 => GetSubsets21(),
            3 => GetSubsets31(),
            4 => GetSubsets41(),
            5 => GetSubsets51(),
            6 => GetSubsets61(),
            7 => GetSubsets71(),
            _ => []
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<SmallIndexSet> GetSubsets(int k)
    {
        return Count switch
        {
            0 => GetSubsets0(k),
            1 => GetSubsets1(k),
            2 => GetSubsets2(k),
            3 => GetSubsets3(k),
            4 => GetSubsets4(k),
            5 => GetSubsets5(k),
            6 => GetSubsets6(k),
            7 => GetSubsets7(k),
            _ => []
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<SmallIndexSet> GetSubsets()
    {
        return Count switch
        {
            0 => GetSubsets0(),
            1 => GetSubsets1(),
            2 => GetSubsets2(),
            3 => GetSubsets3(),
            4 => GetSubsets4(),
            5 => GetSubsets5(),
            6 => GetSubsets6(),
            7 => GetSubsets7(),
            _ => []
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<SmallIndexSet> GetSubsetsOfSize(int size)
    {
        return Count switch
        {
            0 => GetSubsets0(size),
            1 => GetSubsets1(size),
            2 => GetSubsets2(size),
            3 => GetSubsets3(size),
            4 => GetSubsets4(size),
            5 => GetSubsets5(size),
            6 => GetSubsets6(size),
            7 => GetSubsets7(size),
            _ => []
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<SmallIndexSet> GetSubsetsOfSizeInRange(int maxSize)
    {
        var k2 = int.Min(maxSize, Count);

        for (var k = 0; k <= k2; k++)
        {
            foreach (var subset in GetSubsets(k))
                yield return subset;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<SmallIndexSet> GetSubsetsOfSizeInRange(int minSize, int maxSize)
    {
        var k1 = int.Max(minSize, 0);
        var k2 = int.Min(maxSize, Count);

        for (var k = k1; k <= k2; k++)
        {
            foreach (var subset in GetSubsets(k))
                yield return subset;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int ToInt32()
    {
        return IsUInt64Set && LastIndex < 31
            ? this.Aggregate(0, (result, index) => result | (1 << index))
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint ToUInt32()
    {
        return IsUInt64Set && LastIndex < 32
            ? this.Aggregate(0u, (result, index) => result | (1u << index))
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long ToInt64()
    {
        return IsUInt64Set && LastIndex < 63
            ? this.Aggregate(0L, (result, index) => result | (1L << index))
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong ToUInt64()
    {
        return IsUInt64Set
            ? this.Aggregate(0UL, (result, index) => result | (1UL << index))
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public (bool found, ulong bitPattern) TryGetUInt64()
    {
        return IsUInt64Set
            ? (true, this.Aggregate(0UL, (result, index) => result | (1UL << index)))
            : (false, 0UL);
    }


    /// <summary>
    /// https://en.wikipedia.org/wiki/Combinatorial_number_system
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int DecodeCombinadicToInt32()
    {
        return (int)DecodeCombinadicToUInt64();
    }

    /// <summary>
    /// https://en.wikipedia.org/wiki/Combinatorial_number_system
    /// </summary>
    /// <returns></returns>
    public ulong DecodeCombinadicToUInt64()
    {
        if (IsEmptySet) return 0;
        if (IsUnitSet) return (ulong)FirstIndex;

        var intValue = 0UL;

        for (var i = 0; i < Count; i++)
            intValue += this[i].GetBinomialCoefficient(i + 1);

        return intValue;
    }


    public bool SetContains(int index)
    {
        var low = 0;
        var high = Count - 1;

        while (low <= high)
        {
            var mid = (int)((uint)low + (uint)(high - low) / 2);
            var midValue = this[mid];

            if (midValue < index)
                low = mid + 1;
            else if (midValue > index)
                high = mid - 1;
            else
                return true; // Found
        }

        return false; // Not found, return bitwise complement of insertion point
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SetContainsSingleIndex(int index)
    {
        return Count == 1 && this[0] == index;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SetIsSubsetOf(SmallIndexSet indexSet2)
    {
        return indexSet2.SetIsSupersetOf(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SetIsSupersetOf(SmallIndexSet indexSet2)
    {
        return SmallIndexSetUtils.IsSuperset(this, indexSet2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SetContains(SmallIndexSet indexSet2)
    {
        return SmallIndexSetUtils.Contains(this, indexSet2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SetOverlaps(SmallIndexSet indexSet2)
    {
        return SmallIndexSetUtils.Overlaps(this, indexSet2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SmallIndexSet MapIndicesByValue(Func<int, int> indexMapping)
    {
        if (IsEmptySet) return this;

        return Create(
            GetIndices().Select(indexMapping).Distinct().OrderBy(i => i).ToArray()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SmallIndexSet MapIndicesByOrderValue(Func<int, int, int> indexMapping)
    {
        if (IsEmptySet) return this;

        return Create(
            GetIndices().Select((index, order) => indexMapping(order, index)).Distinct().OrderBy(i => i).ToArray()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SmallIndexSet ShiftIndices(int offset)
    {
        if (IsEmptySet || offset == 0) return this;

        return Count switch
        {
            0 => this,

            1 => new SmallIndexSet(
                (int)_value + offset
            ),

            2 => new SmallIndexSet(
                (int)(_value & Mask2) + offset,
                (int)(_value >> Mask2SizeX1) + offset
            ),

            3 => new SmallIndexSet(
                (int)(_value & Mask3) + offset,
                (int)((_value >> Mask3SizeX1) & Mask3) + offset,
                (int)(_value >> Mask3SizeX2) + offset
            ),

            4 => new SmallIndexSet(
                (int)(_value & Mask4) + offset,
                (int)((_value >> Mask4SizeX1) & Mask4) + offset,
                (int)((_value >> Mask4SizeX2) & Mask4) + offset,
                (int)(_value >> Mask4SizeX3) + offset
            ),

            5 => new SmallIndexSet(
                (int)(_value & Mask5) + offset,
                (int)((_value >> Mask5SizeX1) & Mask5) + offset,
                (int)((_value >> Mask5SizeX2) & Mask5) + offset,
                (int)((_value >> Mask5SizeX3) & Mask5) + offset,
                (int)(_value >> Mask5SizeX4) + offset
            ),

            6 => new SmallIndexSet(
                (int)(_value & Mask6) + offset,
                (int)((_value >> Mask6SizeX1) & Mask6) + offset,
                (int)((_value >> Mask6SizeX2) & Mask6) + offset,
                (int)((_value >> Mask6SizeX3) & Mask6) + offset,
                (int)((_value >> Mask6SizeX4) & Mask6) + offset,
                (int)(_value >> Mask6SizeX5) + offset
            ),

            7 => new SmallIndexSet(
                (int)(_value & Mask7) + offset,
                (int)((_value >> Mask7SizeX1) & Mask7) + offset,
                (int)((_value >> Mask7SizeX2) & Mask7) + offset,
                (int)((_value >> Mask7SizeX3) & Mask7) + offset,
                (int)((_value >> Mask7SizeX4) & Mask7) + offset,
                (int)((_value >> Mask7SizeX5) & Mask7) + offset,
                (int)(_value >> Mask7SizeX6) + offset
            ),

            _ => throw new NotSupportedException()
        };
    }


    public SmallIndexSet Insert(int index)
    {
        var indexArray = GetInternalIndexArray();

        // Step 1: Binary search to find index
        var indexPosition = TryGetIndexPosition(index);

        if (indexPosition >= 0)
        {
            // Value already exists
            throw new InvalidOperationException();
        }

        // Value not found; compute insertion point
        var insertionIndex = ~indexPosition;

        // Step 2: Allocate new buffer
        var buffer = new int[indexArray.Length + 1];

        // Step 3: Copy prefix
        if (insertionIndex > 0)
            indexArray[..insertionIndex].CopyTo(buffer, 0);

        // Step 4: Insert new index
        buffer[insertionIndex] = index;

        // Step 5: Copy suffix
        if (insertionIndex < indexArray.Length)
            indexArray[insertionIndex..].CopyTo(buffer, insertionIndex + 1);

        return Create(buffer);
    }

    public SmallIndexSet TryInsert(int index)
    {
        var indexArray = GetInternalIndexArray();

        // Step 1: Binary search to find index
        var indexPosition = TryGetIndexPosition(index);

        if (indexPosition >= 0)
        {
            // Value already exists
            return this;
        }

        // Value not found; compute insertion point
        var insertionIndex = ~indexPosition;

        // Step 2: Allocate new buffer
        var buffer = new int[indexArray.Length + 1];

        // Step 3: Copy prefix
        if (insertionIndex > 0)
            indexArray[..insertionIndex].CopyTo(buffer, 0);

        // Step 4: Insert new index
        buffer[insertionIndex] = index;

        // Step 5: Copy suffix
        if (insertionIndex < indexArray.Length)
            indexArray[insertionIndex..].CopyTo(buffer, insertionIndex + 1);

        return Create(buffer);
    }

    public SmallIndexSet Remove(int index)
    {
        // Step 1: Binary search to find indexPosition
        var indexPosition = TryGetIndexPosition(index);

        if (indexPosition < 0)
        {
            // Value doesn't exist
            throw new InvalidOperationException();
        }

        // Step 2: Allocate new buffer
        var buffer = new int[Count - 1];

        // Step 3: Copy prefix
        var indexArray = GetInternalIndexArray();
        if (indexPosition > 0)
            indexArray[..indexPosition].CopyTo(buffer, 0);

        // Step 4: Copy suffix
        if (indexPosition < Count - 1)
            indexArray[(indexPosition + 1)..].CopyTo(buffer, indexPosition);

        return Create(buffer);
    }

    public SmallIndexSet TryRemove(int index)
    {
        // Step 1: Binary search to find indexPosition
        var indexPosition = TryGetIndexPosition(index);

        if (indexPosition < 0)
        {
            // Value doesn't exist
            return this;
        }

        // Step 2: Allocate new buffer
        var buffer = new int[Count - 1];

        // Step 3: Copy prefix
        var indexArray = GetInternalIndexArray();
        if (indexPosition > 0)
            indexArray[..indexPosition].CopyTo(buffer, 0);

        // Step 4: Copy suffix
        if (indexPosition < Count - 1)
            indexArray[(indexPosition + 1)..].CopyTo(buffer, indexPosition);

        return Create(buffer);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SmallIndexSet SetIntersect(int index2)
    {
        Debug.Assert(index2 >= 0);

        return SetContains(index2) ? Create(index2) : EmptySet;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SmallIndexSet SetIntersect(SmallIndexSet indexSet2)
    {
        return SmallIndexSetUtils.Intersect(this, indexSet2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SmallIndexSet SetUnion(int index2)
    {
        Debug.Assert(index2 >= 0);

        return SmallIndexSetUtils.Join(this, index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SmallIndexSet SetUnion(SmallIndexSet indexSet2)
    {
        return SmallIndexSetUtils.Join(this, indexSet2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SmallIndexSet SetMerge(int index2)
    {
        Debug.Assert(index2 >= 0);

        return SmallIndexSetUtils.Merge(this, index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SmallIndexSet SetMerge(SmallIndexSet indexSet2)
    {
        return SmallIndexSetUtils.Merge(this, indexSet2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SmallIndexSet SetDifference(int index2)
    {
        return TryRemove(index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SmallIndexSet SetDifference(SmallIndexSet indexSet2)
    {
        return SmallIndexSetUtils.Difference(this, indexSet2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int SetCountSwaps(int index2)
    {
        Debug.Assert(index2 >= 0);

        return SmallIndexSetUtils.CountSwaps(this, index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int SetCountSwaps(SmallIndexSet indexSet2)
    {
        return SmallIndexSetUtils.CountSwaps(this, indexSet2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int SetCountSwapsWithSelf()
    {
        return SmallIndexSetUtils.CountSwapsWithSelf(this);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public (int swapCount, SmallIndexSet mergedIndexSet) SetMergeCountSwaps(int index2)
    {
        Debug.Assert(index2 >= 0);

        return SmallIndexSetUtils.MergeCountSwaps(this, index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public (int swapCount, SmallIndexSet mergedIndexSet) SetMergeCountSwaps(SmallIndexSet indexSet2)
    {
        return SmallIndexSetUtils.MergeCountSwaps(this, indexSet2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public (int swapCount, SmallIndexSet mergedIndexSet, SmallIndexSet commonIndexSet) SetMergeCountSwapsTrackCommon(int index2)
    {
        Debug.Assert(index2 >= 0);

        return SmallIndexSetUtils.MergeCountSwapsTrackCommon(this, index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public (int swapCount, SmallIndexSet mergedIndexSet, SmallIndexSet commonIndexSet) SetMergeCountSwapsTrackCommon(SmallIndexSet indexSet2)
    {
        return SmallIndexSetUtils.MergeCountSwapsTrackCommon(this, indexSet2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public (SmallIndexSet unitIndexSet, SmallIndexSet remainingIndexSet) SplitByFirstIndex()
    {
        if (IsEmptySet)
            throw new InvalidOperationException();

        if (IsUnitSet)
            return (this, EmptySet);

        var firstIndex = FirstIndex;
        var unitIndexSet = Create(firstIndex);
        var remainingIndexSet = Remove(firstIndex);

        return (unitIndexSet, remainingIndexSet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public (SmallIndexSet unitIndexSet, SmallIndexSet remainingIndexSet) SplitByLastIndex()
    {
        if (IsEmptySet)
            throw new InvalidOperationException();

        if (IsUnitSet)
            return (this, EmptySet);

        var lastIndex = LastIndex;
        var unitIndexSet = Create(lastIndex);
        var remainingIndexSet = Remove(lastIndex);

        return (unitIndexSet, remainingIndexSet);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<int> GetEnumerator()
    {
        return GetIndices().GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return GetIndices()
            .Select(i => i.ToString())
            .ConcatenateText(
                ", ",
                "SmallIndexSet<",
                ">"
            );
    }
}