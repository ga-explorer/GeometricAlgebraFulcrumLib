using System.Collections;
using System.Collections.Immutable;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using Open.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

public readonly struct UInt64IndexSet :
    IIndexSet,
    IEquatable<UInt64IndexSet>,
    IComparable<UInt64IndexSet>
{
    private static UInt64IndexSet EmptyIndexSet { get; }
        = new UInt64IndexSet(0UL);

    public static IReadOnlyList<UInt64IndexSet> SimpleIndexSets { get; }
        = 64.GetRange()
            .Select(index => new UInt64IndexSet(1UL << index))
            .ToImmutableArray();

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator ulong(UInt64IndexSet indexSet)
    //{
    //    return indexSet.IndexBitPattern;
    //}
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator UInt64IndexSet(ulong indexBitPattern)
    {
        return Create(indexBitPattern);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt64IndexSet Create(ulong indexBitPattern)
    {
        //return new UInt64IndexSet(indexBitPattern);

        if (indexBitPattern == 0UL)
            return EmptyIndexSet;

        if (!BitOperations.IsPow2(indexBitPattern)) 
            return new UInt64IndexSet(indexBitPattern);

        var index = BitOperations.TrailingZeroCount(indexBitPattern);

        return SimpleIndexSets[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt64IndexSet Create(int index)
    {
        return SimpleIndexSets[index];
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt64IndexSet Create(int index1, int index2)
    {
        return index1 == index2
            ? SimpleIndexSets[index1]
            : new UInt64IndexSet(
                (1UL << index1) |
                (1UL << index2)
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt64IndexSet Create(params int[] indexArray)
    {
        return indexArray.Length switch
        {
            0 => EmptyIndexSet,
            1 => SimpleIndexSets[indexArray[0]],
            _ => Create(
                indexArray.Aggregate(0UL, (a, b) => a | (1UL << b))
            )
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt64IndexSet Create(IEnumerable<int> indexList)
    {
        return Create(
            indexList.Aggregate(0UL, (a, b) => a | (1UL << b))
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(UInt64IndexSet operand1, IIndexSet operand2)
    {
        return operand1.Equals(operand2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(UInt64IndexSet operand1, IIndexSet operand2)
    {
        return !operand1.Equals(operand2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(UInt64IndexSet operand1, IIndexSet operand2)
    {
        return operand1.CompareTo(operand2) > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(UInt64IndexSet operand1, IIndexSet operand2)
    {
        return operand1.CompareTo(operand2) < 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(UInt64IndexSet operand1, IIndexSet operand2)
    {
        return operand1.CompareTo(operand2) >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(UInt64IndexSet operand1, IIndexSet operand2)
    {
        return operand1.CompareTo(operand2) <= 0;
    }


    public ulong IndexBitPattern { get; }

    public int Count
        => BitOperations.PopCount(IndexBitPattern);

    public bool IsEmptySet
        => IndexBitPattern == 0;

    public bool IsSingleIndexSet 
        => BitOperations.IsPow2(IndexBitPattern);

    public bool IsIndexPairSet 
        => BitOperations.PopCount(IndexBitPattern) == 2;

    public bool IsSparseSet 
        => LastIndex - FirstIndex + 1 > Count;
        
    public bool IsDenseSet 
        => LastIndex - FirstIndex + 1 == Count;

    public bool IsUInt64Set 
        => true;

    public int FirstIndex
        => IndexBitPattern > 0
            ? BitOperations.TrailingZeroCount(IndexBitPattern)
            : throw new InvalidOperationException();

    public int LastIndex
        => IndexBitPattern > 0
            ? 63 - BitOperations.LeadingZeroCount(IndexBitPattern)
            : throw new InvalidOperationException();

    public int this[int index]
    {
        get
        {
            if (index is < 0 or > 63 || IndexBitPattern == 0)
                throw new IndexOutOfRangeException();

            var bitPattern = IndexBitPattern;

            var i = 0;
            while (bitPattern != 0)
            {
                if (i == index) return i;

                bitPattern >>= 1 + BitOperations.TrailingZeroCount(IndexBitPattern);
                i++;
            }

            //var firstIndex = BitOperations.TrailingZeroCount(IndexBitPattern);
            //var lastIndex = 63 - BitOperations.LeadingZeroCount(IndexBitPattern);

            //for (var i = firstIndex; i <= lastIndex; i++)
            //{
            //    if (((1UL << i) & 1) == 0) 
            //        continue;

            //    index--;

            //    if (index < 0) return i;
            //}

            throw new IndexOutOfRangeException();
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private UInt64IndexSet(ulong indexBitPattern)
    {
        IndexBitPattern = indexBitPattern;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object obj)
    {
        return obj is IIndexSet other && Equals(other);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(UInt64IndexSet other)
    {
        return IndexBitPattern == other.IndexBitPattern;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(IIndexSet indexSet)
    {
        return indexSet switch
        {
            null => 
                false,

            UInt64IndexSet indexSet1 => 
                IndexBitPattern == indexSet1.IndexBitPattern,

            _ => 
                indexSet.TryGetUInt64BitPattern(out var bitPattern) && 
                bitPattern == IndexBitPattern
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        if (IndexBitPattern == 0UL)
            return 0;

        var index1 = BitOperations.TrailingZeroCount(IndexBitPattern);

        if (BitOperations.IsPow2(IndexBitPattern))
            return index1;

        return IndexBitPattern
            .PatternToPositions()
            .Skip(1)
            .Aggregate(
                index1,
                (hashCode, index) => hashCode ^ index
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(UInt64IndexSet other)
    {
        return IndexBitPattern.CompareTo(other.IndexBitPattern);
    }

    public int CompareTo(IIndexSet other)
    {
        if (other is UInt64IndexSet uint64IndexSet)
            return IndexBitPattern.CompareTo(uint64IndexSet.IndexBitPattern);

        var n1 = Count;
        var n2 = other.Count;

        //TODO: Use iterator instead of this[], just as in Equals() implementation here
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<int> GetIndexRange()
    {
        if (IndexBitPattern == 0)
            throw new InvalidOperationException();

        return new Pair<int>(
            BitOperations.TrailingZeroCount(IndexBitPattern),
            63 - BitOperations.LeadingZeroCount(IndexBitPattern)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<int> GetReversedIndices()
    {
        return IndexBitPattern.PatternToPositionsReversed();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetUInt64BitPattern(out ulong bitPattern)
    {
        bitPattern = IndexBitPattern;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SparseIndexSet ToSparseIndexSet()
    {
        return IndexBitPattern.BitPatternToSparseIndexSet();
    }

    public DenseIndexSet ToDenseIndexSet()
    {
        if (IndexBitPattern == 0)
            throw new InvalidOperationException();

        var (firstIndex, lastIndex) = 
            IndexBitPattern.FirstLastOneBitPosition();

        var count = lastIndex - firstIndex + 1;

        return count == Count
            ? DenseIndexSet.Create(firstIndex, count)
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UInt64IndexSet ToUInt64IndexSet()
    {
        return this;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(int index)
    {
        return index is >= 0 and <= 63 &&
               ((1UL << index) & IndexBitPattern) != 0;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsSingleIndex(int index)
    {
        return index is >= 0 and <= 63 &&
               (1UL << index) == IndexBitPattern;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(ulong indexSet)
    {
        return (indexSet & ~IndexBitPattern) == 0UL;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(UInt64IndexSet indexSet)
    {
        return (indexSet.IndexBitPattern & ~IndexBitPattern) == 0UL;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(IIndexSet indexSet)
    {
        if (indexSet is UInt64IndexSet indexSet1)
            return (indexSet1.IndexBitPattern & ~IndexBitPattern) == 0UL;

        return indexSet.TryGetUInt64BitPattern(out var bitPattern) && 
               (bitPattern & ~IndexBitPattern) == 0UL;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlaps(ulong indexSet)
    {
        return (IndexBitPattern & indexSet) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlaps(UInt64IndexSet indexSet)
    {
        return (IndexBitPattern & indexSet.IndexBitPattern) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlaps(IIndexSet indexSet)
    {
        if (indexSet is UInt64IndexSet indexSet1)
            return (IndexBitPattern & indexSet1.IndexBitPattern) != 0;

        return !indexSet.IsEmptySet && indexSet.Any(Contains);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet MapIndices(Func<int, int> indexMapping)
    {
        if (IsEmptySet) return this;
            
        return IndexBitPattern
            .PatternToPositions()
            .Select(indexMapping)
            .ToImmutableSortedSet()
            .ToIndexSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet ShiftIndices(int offset)
    {
        if (IsEmptySet || offset == 0) return this;

        if (offset > 0)
            return BitOperations.LeadingZeroCount(IndexBitPattern) >= offset
                ? new UInt64IndexSet(IndexBitPattern << offset)
                : throw new InvalidOperationException();

        offset = -offset;

        return BitOperations.TrailingZeroCount(IndexBitPattern) >= offset
            ? new UInt64IndexSet(IndexBitPattern >> offset)
            : throw new InvalidOperationException();
    }


    public IIndexSet Add(int index)
    {
        if (index < 64)
        {
            var bitPattern = IndexBitPattern | (1UL << index);

            return bitPattern == IndexBitPattern
                ? this
                : bitPattern.BitPatternToUInt64IndexSet();
        }

        if (IndexBitPattern == 0UL)
            return SparseIndexSet.Create(index);

        var builder = ImmutableSortedSet.CreateBuilder<int>();

        builder.AddRange(IndexBitPattern.PatternToPositions());
        builder.Add(index);

        return builder.ToSparseIndexSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Remove(int index)
    {
        if (!Contains(index))
            throw new InvalidOperationException();

        return new UInt64IndexSet(
            IndexBitPattern & ~(1UL << index)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet RemoveIfContains(int index)
    {
        if (!Contains(index))
            return this;

        return new UInt64IndexSet(
            IndexBitPattern & ~(1UL << index)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Intersect(ulong indexSet)
    {
        var bitPattern = IndexBitPattern & indexSet;

        return bitPattern.BitPatternToUInt64IndexSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UInt64IndexSet Intersect(UInt64IndexSet indexSet)
    {
        var bitPattern = IndexBitPattern & indexSet.IndexBitPattern;

        return bitPattern.BitPatternToUInt64IndexSet();
    }

    public IIndexSet Intersect(IIndexSet indexSet)
    {
        if (indexSet.TryGetUInt64BitPattern(out var indexSet1))
            return (IndexBitPattern & indexSet1).BitPatternToUInt64IndexSet();

        if (IsEmptySet || indexSet.IsEmptySet)
            return EmptyIndexSet;

        var (firstIndex, lastIndex) = GetIndexRange();
        var indexBitPattern = IndexBitPattern;
        var bitPattern = 
            indexSet
                .SkipWhile(i => i < firstIndex)
                .TakeWhile(i => i <= lastIndex)
                .Select(index => 1UL << index)
                .Where(mask => (indexBitPattern & mask) != 0)
                .Aggregate(
                    0UL, 
                    (current, mask) => current | mask
                );

        return bitPattern.BitPatternToUInt64IndexSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Join(ulong indexSet)
    {
        return (IndexBitPattern | indexSet).BitPatternToUInt64IndexSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UInt64IndexSet Join(UInt64IndexSet indexSet)
    {
        return (IndexBitPattern | indexSet.IndexBitPattern).BitPatternToUInt64IndexSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Join(IIndexSet indexSet)
    {
        if (indexSet.TryGetUInt64BitPattern(out var indexSet1))
            return (IndexBitPattern | indexSet1).BitPatternToUInt64IndexSet();
            
        var sortedSetBuilder = ImmutableSortedSet.CreateBuilder<int>();

        if (IndexBitPattern != 0UL)
            sortedSetBuilder.AddRange(IndexBitPattern.PatternToPositions());

        if (indexSet.Count > 0)
            sortedSetBuilder.AddRange(indexSet);

        return sortedSetBuilder.ToSparseIndexSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Difference(ulong indexSet)
    {
        return (IndexBitPattern & ~indexSet).BitPatternToUInt64IndexSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UInt64IndexSet Difference(UInt64IndexSet indexSet)
    {
        return (IndexBitPattern & ~indexSet.IndexBitPattern).BitPatternToUInt64IndexSet();
    }

    public IIndexSet Difference(IIndexSet indexSet)
    {
        if (IsEmptySet)
            return EmptyIndexSet;

        if (indexSet.IsEmptySet)
            return this;

        if (indexSet.TryGetUInt64BitPattern(out var indexSet1))
            return (IndexBitPattern & ~indexSet1).BitPatternToUInt64IndexSet();

        var bitPattern = 
            IndexBitPattern
                .PatternToPositions()
                .Where(index => !indexSet.Contains(index))
                .Aggregate(
                    0UL, 
                    (current, index) => current | (1UL << index)
                );

        return bitPattern.BitPatternToUInt64IndexSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet SymmetricDifference(ulong indexSet)
    {
        var bitPattern =
            (IndexBitPattern & ~indexSet) |
            (~IndexBitPattern & indexSet);

        return bitPattern.BitPatternToUInt64IndexSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UInt64IndexSet SymmetricDifference(UInt64IndexSet indexSet)
    {
        var bitPattern =
            (IndexBitPattern & ~indexSet.IndexBitPattern) |
            (~IndexBitPattern & indexSet.IndexBitPattern);

        return bitPattern.BitPatternToUInt64IndexSet();
    }

    public IIndexSet SymmetricDifference(IIndexSet indexSet)
    {
        if (IsEmptySet)
            return indexSet;

        if (indexSet.IsEmptySet)
            return this;
            
        if (indexSet.TryGetUInt64BitPattern(out var bitPattern))
        {
            bitPattern =
                (IndexBitPattern & ~bitPattern) |
                (~IndexBitPattern & bitPattern);

            return bitPattern.BitPatternToUInt64IndexSet();
        }
        
        var sortedSetBuilder = ImmutableSortedSet.CreateBuilder<int>();
        
        sortedSetBuilder.AddRange(indexSet);

        foreach (var index in IndexBitPattern.PatternToPositions())
        {
            if (indexSet.Contains(index))
                sortedSetBuilder.Remove(index);
            else
                sortedSetBuilder.Add(index);
        }

        return sortedSetBuilder.ToSparseIndexSet();
    }
    
    //public Tuple<bool, IndexSet> EGp(UInt64IndexSet indexSet)
    //{
    //    var bitPattern = IndexBitPattern ^ indexSet.IndexBitPattern;
    //    var isNegative = BasisBladeProductUtils.EGpIsNegative(IndexBitPattern, indexSet.IndexBitPattern);

    //    return new Tuple<bool, IndexSet>(
    //        isNegative,
    //        new UInt64IndexSet(bitPattern)
    //    );
    //}

    //public bool EGpIsNegative(UInt64IndexSet indexSet)
    //{
    //    return BasisBladeProductUtils.EGpIsNegative(
    //        IndexBitPattern,
    //        indexSet.IndexBitPattern
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<int> GetEnumerator()
    {
        return IndexBitPattern.PatternToPositions().GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return IsEmptySet
            ? "<>"
            : IndexBitPattern
                .PatternToPositions()
                .Select(i => i.ToString())
                .ConcatenateText(", ", "<", ">");
    }
}