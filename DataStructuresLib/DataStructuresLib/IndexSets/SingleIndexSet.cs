using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using Open.Collections;

namespace DataStructuresLib.IndexSets;

public readonly struct SingleIndexSet :
    IIndexSet,
    IEquatable<SingleIndexSet>,
    IComparable<SingleIndexSet>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static SingleIndexSet Create(int index)
    {
        return new SingleIndexSet(index);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(SingleIndexSet operand1, IIndexSet operand2)
    {
        return operand1.Equals(operand2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(SingleIndexSet operand1, IIndexSet operand2)
    {
        return !operand1.Equals(operand2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(SingleIndexSet operand1, IIndexSet operand2)
    {
        return operand1.CompareTo(operand2) > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(SingleIndexSet operand1, IIndexSet operand2)
    {
        return operand1.CompareTo(operand2) < 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(SingleIndexSet operand1, IIndexSet operand2)
    {
        return operand1.CompareTo(operand2) >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(SingleIndexSet operand1, IIndexSet operand2)
    {
        return operand1.CompareTo(operand2) <= 0;
    }


    public int Index { get; }

    public bool IsEmptySet
        => false;

    public bool IsSingleIndexSet 
        => true;
    
    public bool IsIndexPairSet 
        => false;

    public bool IsSparseSet 
        => false;
    
    public bool IsDenseSet 
        => true;

    public bool IsUInt64Set 
        => Index < 64;

    public int FirstIndex
        => Index;

    public int LastIndex
        => Index;

    public int Count
        => 1;

    public int this[int index]
        => index == 0 
            ? Index : throw new IndexOutOfRangeException();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SingleIndexSet(int index)
    {
        Debug.Assert(
            index >= 0
        );

        Index = index;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object obj)
    {
        return obj is IIndexSet other && Equals(other);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(SingleIndexSet other)
    {
        return Index == other.Index;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(IIndexSet other)
    {
        if (ReferenceEquals(null, other)) return false;

        return other.Count == 1 && other.FirstIndex == Index;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return Index.GetHashCode();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(SingleIndexSet other)
    {
        return Index.CompareTo(other.Index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(IIndexSet other)
    {
        return other.IsEmptySet 
            ? 1 
            : Index.CompareTo(other.LastIndex);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<int> GetIndexRange()
    {
        return new Pair<int>(Index, Index);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<int> GetReversedIndices()
    {
        yield return Index;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetUInt64BitPattern(out ulong bitPattern)
    {
        if (Index >= 64)
        {
            bitPattern = 0UL;
            return false;
        }

        bitPattern = 1UL << Index;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DenseIndexSet ToDenseIndexSet()
    {
        return DenseIndexSet.Create(Index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UInt64IndexSet ToUInt64IndexSet()
    {
        return Index <= 63
            ? (1UL << Index).BitPatternToUInt64IndexSet()
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SparseIndexSet ToSparseIndexSet()
    {
        return Index.IndexToSparseIndexSet();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(int index)
    {
        return index == Index;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsSingleIndex(int index)
    {
        return index == Index;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(ulong indexSet)
    {
        return indexSet == 0 || 
               (Index < 64 && BitOperations.IsPow2(indexSet) && BitOperations.TrailingZeroCount(indexSet) == Index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(IIndexSet indexSet)
    {
        return indexSet.IsEmptySet || 
               (indexSet.IsSingleIndexSet && indexSet.FirstIndex == Index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlaps(ulong indexSet)
    {
        return indexSet != 0 && 
               Index < 64 &&
               (indexSet & (1UL << Index)) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlaps(IIndexSet indexSet)
    {
        return !indexSet.IsEmptySet && 
               indexSet.Contains(Index);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet MapIndices(Func<int, int> indexMapping)
    {
        return new SingleIndexSet(
            indexMapping(Index)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet ShiftIndices(int offset)
    {
        return new SingleIndexSet(
            Index + offset
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Add(int index)
    {
        if (index == Index)
            return this;

        return index < 64 && Index < 64
            ? UInt64IndexSet.Create((1UL << index) | (1UL << Index))
            : SparseIndexSet.Create(index, Index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Remove(int index)
    {
        return index == Index 
            ? EmptyIndexSet.Instance 
            : throw new InvalidOperationException();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet RemoveIfContains(int index)
    {
        return index == Index 
            ? EmptyIndexSet.Instance 
            : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Intersect(ulong indexSet)
    {
        return Index <= 63 && ((1UL << Index) & indexSet) != 0
            ? UInt64IndexSet.SimpleIndexSets[Index]
            : EmptyIndexSet.Instance;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Intersect(IIndexSet indexSet)
    {
        if (indexSet.TryGetUInt64BitPattern(out var bitPattern))
            return Intersect(bitPattern);

        return indexSet.Contains(Index)
            ? this
            : EmptyIndexSet.Instance;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Join(ulong indexSet)
    {
        if (indexSet == 0)
            return this;

        if (Index < 64)
            return indexSet.SetBitToOneAt(Index).BitPatternToUInt64IndexSet();
        
        var builder = ImmutableSortedSet.CreateBuilder<int>();

        builder.AddRange(indexSet.PatternToPositions());
        builder.Add(Index);

        return builder.ToIndexSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Join(IIndexSet indexSet)
    {
        if (indexSet.IsEmptySet)
            return this;

        return indexSet.Contains(Index) 
            ? indexSet 
            : indexSet.Add(Index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Difference(ulong indexSet)
    {
        return Index < 64 && indexSet.IsOneAt(Index) 
            ? EmptyIndexSet.Instance 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Difference(IIndexSet indexSet)
    {
        return indexSet.Contains(Index) 
            ? EmptyIndexSet.Instance 
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet SymmetricDifference(ulong indexSet)
    {
        if (Index < 64)
            return indexSet.InvertBitAt(Index).BitPatternToNonUInt64IndexSet();

        var builder = ImmutableSortedSet.CreateBuilder<int>();

        builder.AddRange(indexSet.PatternToPositions());
        builder.Add(Index);

        return builder.ToIndexSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet SymmetricDifference(IIndexSet indexSet)
    {
        return indexSet.Contains(Index) 
            ? indexSet.Remove(Index)
            : indexSet.Add(Index);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<int> GetEnumerator()
    {
        yield return Index;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"<{Index}>";
    }
}