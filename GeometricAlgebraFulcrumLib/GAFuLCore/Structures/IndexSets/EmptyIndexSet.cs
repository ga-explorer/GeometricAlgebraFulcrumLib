using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

public readonly struct EmptyIndexSet :
    IIndexSet,
    IEquatable<EmptyIndexSet>,
    IComparable<EmptyIndexSet>
{
    public static EmptyIndexSet Instance { get; }
        = [];
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator ulong(EmptyIndexSet indexSet)
    //{
    //    return indexSet.IndexBitPattern;
    //}
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(EmptyIndexSet operand1, IIndexSet operand2)
    {
        return operand1.Equals(operand2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(EmptyIndexSet operand1, IIndexSet operand2)
    {
        return !operand1.Equals(operand2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(EmptyIndexSet operand1, IIndexSet operand2)
    {
        return operand1.CompareTo(operand2) > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(EmptyIndexSet operand1, IIndexSet operand2)
    {
        return operand1.CompareTo(operand2) < 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(EmptyIndexSet operand1, IIndexSet operand2)
    {
        return operand1.CompareTo(operand2) >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(EmptyIndexSet operand1, IIndexSet operand2)
    {
        return operand1.CompareTo(operand2) <= 0;
    }

    
    public int Count
        => 0;

    public bool IsEmptySet
        => true;

    public bool IsSingleIndexSet 
        => false;

    public bool IsIndexPairSet 
        => false;

    public bool IsSparseSet 
        => false;
    
    public bool IsDenseSet 
        => false;

    public bool IsUInt64Set 
        => true;

    public int FirstIndex
        => throw new InvalidOperationException();

    public int LastIndex
        => throw new InvalidOperationException();

    public int this[int index] 
        => throw new IndexOutOfRangeException();

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object obj)
    {
        return obj is IIndexSet other && Equals(other);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(EmptyIndexSet other)
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(IIndexSet indexSet)
    {
        return indexSet is not null && indexSet.IsEmptySet;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(EmptyIndexSet other)
    {
        return 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(IIndexSet other)
    {
        return other.IsEmptySet ? 0 : -1;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<int> GetIndexRange()
    {
        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<int> GetReversedIndices()
    {
        return [];
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetUInt64BitPattern(out ulong bitPattern)
    {
        bitPattern = 0UL;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SparseIndexSet ToSparseIndexSet()
    {
        return SparseIndexSet.EmptySet;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DenseIndexSet ToDenseIndexSet()
    {
        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UInt64IndexSet ToUInt64IndexSet()
    {
        return 0UL.BitPatternToUInt64IndexSet();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(int index)
    {
        return false;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsSingleIndex(int index)
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(ulong indexSet)
    {
        return indexSet == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(IIndexSet indexSet)
    {
        return indexSet.IsEmptySet;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlaps(ulong indexSet)
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlaps(EmptyIndexSet indexSet)
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlaps(IIndexSet indexSet)
    {
        return false;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet MapIndices(Func<int, int> indexMapping)
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet ShiftIndices(int offset)
    {
        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Add(int index)
    {
        return index < 64 
            ? (1UL << index).BitPatternToUInt64IndexSet() 
            : SparseIndexSet.Create(index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Remove(int index)
    {
        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet RemoveIfContains(int index)
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Intersect(ulong indexSet)
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Intersect(IIndexSet indexSet)
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Join(ulong indexSet)
    {
        return indexSet == 0 
            ? this 
            : indexSet.BitPatternToUInt64IndexSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Join(IIndexSet indexSet)
    {
        return indexSet.IsEmptySet ? this : indexSet;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Difference(ulong indexSet)
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet Difference(IIndexSet indexSet)
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet SymmetricDifference(ulong indexSet)
    {
        return indexSet == 0 
            ? this 
            : indexSet.BitPatternToUInt64IndexSet();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet SymmetricDifference(IIndexSet indexSet)
    {
        return indexSet;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<int> GetEnumerator()
    {
        return Enumerable.Empty<int>().GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return "<>";
    }
}