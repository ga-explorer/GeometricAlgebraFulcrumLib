using System;
using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using Open.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

public readonly struct DenseIndexSet :
    IIndexSet,
    IEquatable<DenseIndexSet>,
    IComparable<DenseIndexSet>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static DenseIndexSet Create(int index)
    {
        return new DenseIndexSet(index, index);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static DenseIndexSet Create(int firstIndex, int count)
    {
        var lastIndex = firstIndex + count - 1;

        return new DenseIndexSet(firstIndex, lastIndex);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(DenseIndexSet operand1, IIndexSet operand2)
    {
        return operand1.Equals(operand2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(DenseIndexSet operand1, IIndexSet operand2)
    {
        return !operand1.Equals(operand2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(DenseIndexSet operand1, IIndexSet operand2)
    {
        return operand1.CompareTo(operand2) > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(DenseIndexSet operand1, IIndexSet operand2)
    {
        return operand1.CompareTo(operand2) < 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(DenseIndexSet operand1, IIndexSet operand2)
    {
        return operand1.CompareTo(operand2) >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(DenseIndexSet operand1, IIndexSet operand2)
    {
        return operand1.CompareTo(operand2) <= 0;
    }


    public bool IsEmptySet
        => false;

    public bool IsSingleIndexSet 
        => LastIndex == FirstIndex;
    
    public bool IsIndexPairSet 
        => LastIndex == FirstIndex + 1;

    public bool IsSparseSet 
        => false;
        
    public bool IsDenseSet 
        => false;

    public bool IsUInt64Set 
        => LastIndex < 64;

    public int FirstIndex { get; }

    public int LastIndex { get; }

    public int Count
        => LastIndex - FirstIndex + 1;

    public int this[int index]
        => index >= 0 && index < Count
            ? index + FirstIndex
            : throw new IndexOutOfRangeException();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DenseIndexSet(int firstIndex, int lastIndex)
    {
        Debug.Assert(
            firstIndex >= 0 && firstIndex <= lastIndex
        );

        FirstIndex = firstIndex;
        LastIndex = lastIndex;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object obj)
    {
        return obj is IIndexSet other && Equals(other);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(DenseIndexSet other)
    {
        return FirstIndex == other.FirstIndex &&
               LastIndex == other.LastIndex;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(IIndexSet other)
    {
        if (ReferenceEquals(null, other)) return false;

        return Count == other.Count &&
               FirstIndex == other.FirstIndex &&
               LastIndex == other.LastIndex;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        var hashCode = FirstIndex;

        for (var index = FirstIndex + 1; index <= LastIndex; index++)
            hashCode ^= index;

        return hashCode;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(DenseIndexSet other)
    {
        return LastIndex != other.LastIndex
            ? LastIndex.CompareTo(other.LastIndex)
            : other.FirstIndex.CompareTo(FirstIndex);
    }

    public int CompareTo(IIndexSet other)
    {
        var index1 = LastIndex + 1;
        var n2 = other.Count;

        //TODO: Use iterator instead of this[], just as in Equals() implementation here
        while (index1 > FirstIndex && n2 > 0)
        {
            index1--;
            n2--;

            var index2 = other[n2];

            if (index1 > index2) return 1;
            if (index1 < index2) return -1;
        }

        if (index1 == FirstIndex) return n2 == 0 ? 0 : -1;

        return 1;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<int> GetIndexRange()
    {
        return new Pair<int>(FirstIndex, LastIndex);
    }
    
    public IEnumerable<int> GetReversedIndices()
    {
        for (var i = LastIndex; i >= FirstIndex; i--)
            yield return i;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetUInt64BitPattern(out ulong bitPattern)
    {
        if (LastIndex >= 64)
        {
            bitPattern = 0UL;
            return false;
        }
        
        bitPattern = Count.CreateMaskUInt64() << FirstIndex;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UInt64IndexSet ToUInt64IndexSet()
    {
        return LastIndex < 64
            ? (Count.CreateMaskUInt64() << FirstIndex).BitPatternToUInt64IndexSet()
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SparseIndexSet ToSparseIndexSet()
    {
        return SparseIndexSet.Create(this);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DenseIndexSet ToDenseIndexSet()
    {
        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(int index)
    {
        return index >= FirstIndex && index <= LastIndex;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsSingleIndex(int index)
    {
        return index == FirstIndex && index == LastIndex;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(ulong indexSet)
    {
        if (indexSet == 0)
            return true;

        var (firstIndex, lastIndex) = 
            indexSet.FirstLastOneBitPosition();

        return Contains(firstIndex) &&
               Contains(lastIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(IIndexSet indexSet)
    {
        if (indexSet.IsEmptySet)
            return true;

        return Contains(indexSet.FirstIndex) &&
               Contains(indexSet.LastIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlaps(ulong indexSet)
    {
        if (indexSet == 0)
            return false;
        
        var (firstIndex, lastIndex) = 
            indexSet.FirstLastOneBitPosition();

        return Contains(firstIndex) &&
               Contains(lastIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlaps(IIndexSet indexSet)
    {
        if (indexSet.IsEmptySet)
            return false;

        return Contains(indexSet.FirstIndex) ||
               Contains(indexSet.LastIndex);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet MapIndices(Func<int, int> indexMapping)
    {
        return this
            .Select(indexMapping)
            .ToImmutableSortedSet()
            .ToIndexSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet ShiftIndices(int offset)
    {
        if (offset == 0) 
            return this;

        var firstIndex = FirstIndex + offset;

        if (firstIndex < 0)
            throw new InvalidOperationException();

        return new DenseIndexSet(
            firstIndex, 
            LastIndex + offset
        );
    }


    public IIndexSet Add(int index)
    {
        if (Contains(index))
            return this;
        
        if (index == FirstIndex - 1)
            return new DenseIndexSet(index, LastIndex);

        if (index == LastIndex + 1)
            return new DenseIndexSet(FirstIndex, index);

        if (index < 64 && LastIndex < 64)
        {
            var bitPattern = 
                (1UL << index) | (Count.CreateMaskUInt64() << FirstIndex);

            return UInt64IndexSet.Create(bitPattern);
        }

        var builder = ImmutableSortedSet.CreateBuilder<int>();

        builder.AddRange(this);
        builder.Add(index);

        return builder.ToSparseIndexSet();
    }

    public IIndexSet Remove(int index)
    {
        if (!Contains(index))
            throw new InvalidOperationException();
        
        if (FirstIndex == LastIndex)
            return EmptyIndexSet.Instance;

        if (index == FirstIndex)
            return new DenseIndexSet(FirstIndex + 1, LastIndex);

        if (index == LastIndex)
            return new DenseIndexSet(FirstIndex, LastIndex - 1);
        
        if (LastIndex < 64)
        {
            var bitPattern = 
                (Count.CreateMaskUInt64() << FirstIndex).SetBitToZeroAt(index);

            return UInt64IndexSet.Create(bitPattern);
        }

        var builder = ImmutableSortedSet.CreateBuilder<int>();

        for (var i = FirstIndex; i < index; i++)
            builder.Add(i);

        for (var i = index + 1; i <= LastIndex; i++)
            builder.Add(i);

        return builder.ToSparseIndexSet();
    }
    
    public IIndexSet RemoveIfContains(int index)
    {
        if (!Contains(index))
            return this;
        
        if (FirstIndex == LastIndex)
            return EmptyIndexSet.Instance;

        if (index == FirstIndex)
            return new DenseIndexSet(FirstIndex + 1, LastIndex);

        if (index == LastIndex)
            return new DenseIndexSet(FirstIndex, LastIndex - 1);
        
        if (LastIndex < 64)
        {
            var bitPattern = 
                (Count.CreateMaskUInt64() << FirstIndex).SetBitToZeroAt(index);

            return UInt64IndexSet.Create(bitPattern);
        }

        var builder = ImmutableSortedSet.CreateBuilder<int>();

        for (var i = FirstIndex; i < index; i++)
            builder.Add(i);

        for (var i = index + 1; i <= LastIndex; i++)
            builder.Add(i);

        return builder.ToSparseIndexSet();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Intersect(ulong indexSet)
    {
        if (TryGetUInt64BitPattern(out var b1))
            return (b1 & indexSet).BitPatternToUInt64IndexSet();

        if (Contains(indexSet))
            return indexSet.BitPatternToUInt64IndexSet();
        
        return UInt64IndexSet.Create(
            indexSet.PatternToPositions().Where(Contains)
        );
    }

    public IIndexSet Intersect(DenseIndexSet indexSet)
    {
        var flag1 = Contains(indexSet.FirstIndex);
        var flag2 = Contains(indexSet.LastIndex);

        if (flag1)
            return flag2
                ? indexSet
                : new DenseIndexSet(indexSet.FirstIndex, LastIndex);

        return flag2
            ? new DenseIndexSet(FirstIndex, indexSet.LastIndex)
            : EmptyIndexSet.Instance;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Intersect(IIndexSet indexSet)
    {
        if (indexSet is DenseIndexSet indexSet1)
            return Intersect(indexSet1);

        if (indexSet.TryGetUInt64BitPattern(out var bitPattern))
            return Intersect(bitPattern);

        if (Contains(indexSet))
            return indexSet;
        
        if (TryGetUInt64BitPattern(out var b1))
            return indexSet.Intersect(b1);

        return indexSet.Where(Contains).ToImmutableSortedSet().ToIndexSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Join(ulong indexSet)
    {
        if (Contains(indexSet))
            return this;

        if (TryGetUInt64BitPattern(out var b1))
            return (b1 | indexSet).BitPatternToUInt64IndexSet();

        return SparseIndexSet.Create(
            this.Concat(indexSet.PatternToPositions())
        );
    }

    public IIndexSet Join(DenseIndexSet indexSet)
    {
        var flag1 = Contains(indexSet.FirstIndex);
        var flag2 = Contains(indexSet.LastIndex);

        if (flag1)
        {
            if (flag2)
                return this;
            
            return new DenseIndexSet(FirstIndex, indexSet.LastIndex);
        }

        if (flag2)
            return new DenseIndexSet(indexSet.FirstIndex, LastIndex);
        
        if (TryGetUInt64BitPattern(out var b1) && indexSet.TryGetUInt64BitPattern(out var b2))
            return (b1 | b2).BitPatternToUInt64IndexSet();

        return SparseIndexSet.Create(
            this.Concat(indexSet)
        );
    }

    public IIndexSet Join(IIndexSet indexSet)
    {
        if (indexSet is DenseIndexSet indexSet1)
            return Join(indexSet1);
        
        if (Contains(indexSet))
            return this;

        if (indexSet.TryGetUInt64BitPattern(out var bitPattern))
            return Join(bitPattern);
        
        if (TryGetUInt64BitPattern(out var b1))
            return indexSet.Join(b1);

        return SparseIndexSet.Create(
            this.Concat(indexSet)
        );
    }

    public IIndexSet Difference(ulong indexSet)
    {
        if (indexSet == 0)
            return this;

        var sortedSetBuilder = ImmutableSortedSet.CreateBuilder<int>();

        sortedSetBuilder.AddRange(this);

        foreach (var index in indexSet.PatternToPositions())
        {
            if (sortedSetBuilder.Contains(index))
                sortedSetBuilder.Remove(index);
        }

        return sortedSetBuilder.ToImmutable().ToIndexSet();
    }

    public IIndexSet Difference(IIndexSet indexSet)
    {
        if (indexSet.IsEmptySet)
            return this;

        var sortedSetBuilder = ImmutableSortedSet.CreateBuilder<int>();

        sortedSetBuilder.AddRange(this);

        foreach (var index in indexSet)
        {
            if (sortedSetBuilder.Contains(index))
                sortedSetBuilder.Remove(index);
        }

        return sortedSetBuilder.ToImmutable().ToIndexSet();
    }

    public IIndexSet SymmetricDifference(ulong indexSet)
    {
        if (indexSet == 0)
            return this;

        var sortedSetBuilder = ImmutableSortedSet.CreateBuilder<int>();

        sortedSetBuilder.AddRange(this);

        foreach (var index in indexSet.PatternToPositions())
        {
            if (sortedSetBuilder.Contains(index))
                sortedSetBuilder.Remove(index);
            else
                sortedSetBuilder.Add(index);
        }

        return sortedSetBuilder.ToImmutable().ToIndexSet();
    }

    public IIndexSet SymmetricDifference(IIndexSet indexSet)
    {
        if (indexSet.IsEmptySet)
            return this;

        var sortedSetBuilder = ImmutableSortedSet.CreateBuilder<int>();

        sortedSetBuilder.AddRange(this);

        foreach (var index in indexSet)
        {
            if (sortedSetBuilder.Contains(index))
                sortedSetBuilder.Remove(index);
            else
                sortedSetBuilder.Add(index);
        }

        return sortedSetBuilder.ToImmutable().ToIndexSet();
    }
    

    public IEnumerator<int> GetEnumerator()
    {
        for (var index = FirstIndex; index <= LastIndex; index++)
            yield return index;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return this
            .Select(i => i.ToString())
            .ConcatenateText(", ", "<", ">");
    }
}