using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using Open.Collections;

namespace GeometricAlgebraFulcrumLib.Benchmarks.Structures;

public readonly struct IndexSet2 :
    IReadOnlyList<int>,
    IEquatable<IndexSet2>,
    IComparable<IndexSet2>
{
    internal static IndexSet2 EmptySet { get; }
        = new IndexSet2(ImmutableSortedSet<int>.Empty);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static IndexSet2 Create(int index)
    {
        return new IndexSet2(
            ImmutableSortedSet.Create(index)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static IndexSet2 Create(int index1, int index2)
    {
        return new IndexSet2(
            ImmutableSortedSet.Create(index1, index2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static IndexSet2 Create(params int[] indexArray)
    {
        return new IndexSet2(
            ImmutableSortedSet.Create(indexArray)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static IndexSet2 Create(IEnumerable<int> indexList)
    {
        return new IndexSet2(
            indexList.ToImmutableSortedSet()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet2 Create(ImmutableSortedSet<int> indexSet)
    {
        return indexSet.Count == 0 
            ? EmptySet 
            : new IndexSet2(indexSet);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(IndexSet2 operand1, IndexSet2 operand2)
    {
        return operand1.Equals(operand2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(IndexSet2 operand1, IndexSet2 operand2)
    {
        return !operand1.Equals(operand2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(IndexSet2 operand1, IndexSet2 operand2)
    {
        return operand1.CompareTo(operand2) > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(IndexSet2 operand1, IndexSet2 operand2)
    {
        return operand1.CompareTo(operand2) < 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(IndexSet2 operand1, IndexSet2 operand2)
    {
        return operand1.CompareTo(operand2) >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(IndexSet2 operand1, IndexSet2 operand2)
    {
        return operand1.CompareTo(operand2) <= 0;
    }


    private readonly ImmutableSortedSet<int> _indexSet;

    public bool IsEmptySet
        => _indexSet.Count == 0;

    public bool IsUnitSet 
        => _indexSet.Count == 1;
    
    public bool IsPairSet 
        => _indexSet.Count == 2;
    
    public bool IsTripletSet 
        => _indexSet.Count == 3;

    public bool IsSparseSet 
        => LastIndex - FirstIndex + 1 > Count;
        
    public bool IsDenseSet 
        => LastIndex - FirstIndex + 1 == Count;

    public bool IsUInt64Set 
        => LastIndex < 64;

    public int FirstIndex
        => _indexSet[0];

    public int LastIndex
        => _indexSet[^1];

    public int Count
        => _indexSet.Count;

    public int this[int index]
        => _indexSet[index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IndexSet2(ImmutableSortedSet<int> sortedIndexSet)
    {
        Debug.Assert(
            sortedIndexSet.Count == 0 ||
            sortedIndexSet[0] >= 0
        );

        _indexSet = sortedIndexSet;

        //Debug.Assert(Count > 3 && IsSparseSet);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object obj)
    {
        return obj is IndexSet2 other && Equals(other);
    }

    public bool Equals(IndexSet2 other)
    {
        if (_indexSet.Count != other._indexSet.Count)
            return false;

        using var enumerator1 = _indexSet.GetEnumerator();
        using var enumerator2 = other._indexSet.GetEnumerator();

        while (enumerator1.MoveNext())
        {
            if (!enumerator2.MoveNext() || enumerator1.Current != enumerator2.Current)
                return false;
        }

        return !enumerator2.MoveNext();
    }

    public override int GetHashCode()
    {
        if (_indexSet.Count == 0)
            return 0;

        var index1 = _indexSet[0];

        if (_indexSet.Count == 1)
            return index1;

        return _indexSet
            .Skip(1)
            .Aggregate(
                index1,
                (hashCode, index) => hashCode ^ index
            );
    }
    
    public int CompareTo(IndexSet2 other)
    {
        var n1 = Count;
        var n2 = other.Count;

        while (n1 > 0 && n2 > 0)
        {
            n1--;
            n2--;

            var index1 = _indexSet[n1];
            var index2 = other._indexSet[n2];

            if (index1 > index2) return 1;
            if (index1 < index2) return -1;
        }

        if (n1 == 0) return n2 == 0 ? 0 : -1;

        return 1;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<int> GetIndexRange()
    {
        return new Pair<int>(
            _indexSet[0],
            _indexSet[^1]
        );
    }
    
    public IEnumerable<int> GetReversedIndices()
    {
        for (var i = _indexSet.Count - 1; i >= 0; i--)
            yield return _indexSet[i];
    }
    
    
    public bool TryGetUInt64BitPattern(out ulong bitPattern)
    {
        if (_indexSet.Count == 0)
        {
            bitPattern = 0UL;
            return true;
        }

        if (_indexSet[^1] >= 64)
        {
            bitPattern = 0UL;
            return false;
        }

        bitPattern = _indexSet.Count == 1 
            ? 1UL << _indexSet[0]
            : _indexSet.Aggregate(
                0UL,
                (id, index) => id | (1UL << index)
            );

        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet2 GetSubset(int index, int count)
    {
        if (count == 0) return EmptySet;

        var indexSet = 
            _indexSet.Skip(index).Take(count).ToImmutableSortedSet();

        return new IndexSet2(indexSet);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(int index)
    {
        return _indexSet.Contains(index);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsSingleIndex(int index)
    {
        return _indexSet.Count == 1 && 
               _indexSet[0] == index;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(IndexSet2 indexSet)
    {
        if (IsEmptySet) 
            return false;

        return indexSet.IsEmptySet || indexSet.All(Contains);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlaps(IndexSet2 indexSet)
    {
        if (IsEmptySet || indexSet.IsEmptySet) 
            return false;

        return indexSet.Any(Contains);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet2 MapIndices(Func<int, int> indexMapping)
    {
        if (IsEmptySet) return this;
        
        return new IndexSet2(
            _indexSet
                .Select(indexMapping)
                .ToImmutableSortedSet()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet2 ShiftIndices(int offset)
    {
        if (IsEmptySet || offset == 0) return this;

        if (offset > 0)
            return Create(
                _indexSet
                    .Select(i => i + offset)
                    .ToImmutableSortedSet()
            );

        offset = -offset;

        return FirstIndex >= offset
            ? Create(
                _indexSet
                    .Select(i => i - offset)
                    .ToImmutableSortedSet()
            )
            : throw new InvalidOperationException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet2 Insert(int index)
    {
        if (IsEmptySet)
        {
            return Create(index);
        }

        if (_indexSet.Contains(index))
            throw new InvalidOperationException();

        return new IndexSet2(
            _indexSet.Add(index)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet2 TryInsert(int index)
    {
        if (IsEmptySet)
        {
            return Create(index);
        }

        if (_indexSet.Contains(index))
            return this;

        return new IndexSet2(
            _indexSet.Add(index)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet2 Remove(int index)
    {
        if (!_indexSet.Contains(index))
            throw new InvalidOperationException();

        var sortedIndexSet = _indexSet.Remove(index);

        return sortedIndexSet.IsEmpty 
            ? EmptySet 
            : new IndexSet2(sortedIndexSet);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet2 TryRemove(int index)
    {
        if (!_indexSet.Contains(index))
            return this;

        var sortedIndexSet = _indexSet.Remove(index);

        return sortedIndexSet.IsEmpty 
            ? EmptySet 
            : new IndexSet2(sortedIndexSet);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet2 Intersect(IndexSet2 indexSet)
    {
        if (IsEmptySet || indexSet.IsEmptySet)
            return EmptySet;

        var sortedIndexSet = _indexSet.Intersect(indexSet);

        return sortedIndexSet.IsEmpty 
            ? EmptySet 
            : new IndexSet2(sortedIndexSet);
    }

    public IndexSet2 Join(IndexSet2 indexSet)
    {
        if (IsEmptySet)
            return indexSet;

        if (indexSet.IsEmptySet)
            return this;

        var sortedSetBuilder = _indexSet.ToBuilder();

        sortedSetBuilder.AddRange(indexSet);

        return new IndexSet2( 
            sortedSetBuilder.ToImmutable() 
        );
    }

    public IndexSet2 Difference(IndexSet2 indexSet)
    {
        if (IsEmptySet)
            return EmptySet;

        if (indexSet.IsEmptySet)
            return this;

        var sortedIndexSet = 
            _indexSet.Except(indexSet);

        return sortedIndexSet.Count == _indexSet.Count 
            ? this 
            : new IndexSet2(sortedIndexSet);
    }

    public IndexSet2 SymmetricDifference(IndexSet2 indexSet)
    {
        if (IsEmptySet)
            return indexSet;

        if (indexSet.IsEmptySet)
            return this;

        var sortedSetBuilder = _indexSet.ToBuilder();

        foreach (var index in indexSet)
        {
            if (!sortedSetBuilder.Add(index))
                sortedSetBuilder.Remove(index);
        }

        return new IndexSet2( 
            sortedSetBuilder.ToImmutable()
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public (int swapCount, IndexSet2 mergedIndexSet, IndexSet2 commonIndexSet) MergeCountSwapsTrackCommon(IndexSet2 indexSet)
    {
        if (IsEmptySet)
            return (0, indexSet, EmptySet);

        if (indexSet.IsEmptySet)
            return (0, this, EmptySet);

        var swapCount = 0;
            
        // Active index range of index sets
        var id1IndexRange = new IndexRange(_indexSet.Count - 1);
        var id2IndexRange = new IndexRange(indexSet.Count - 1);

        var mergedIndexList = new List<int>(_indexSet.Count + indexSet.Count);
        var commonIndexList = new List<int>(Math.Min(_indexSet.Count, indexSet.Count));

        while (id1IndexRange.IsValid && id2IndexRange.IsValid)
        {
            var id1FirstIndex = _indexSet[id1IndexRange.Index1];
            var id2FirstIndex = indexSet[id2IndexRange.Index1];

            while (id1FirstIndex == id2FirstIndex)
            {
                commonIndexList.Add(id1FirstIndex);

                id1IndexRange.IncreaseIndex1();
                id2IndexRange.IncreaseIndex1();

                swapCount += id1IndexRange.Count;

                if (!id1IndexRange.IsValid || !id2IndexRange.IsValid)
                    break;

                id1FirstIndex = _indexSet[id1IndexRange.Index1];
                id2FirstIndex = indexSet[id2IndexRange.Index1];
            }

            // One or both of the two sets is empty, no more swaps are needed
            if (!id1IndexRange.IsValid || !id2IndexRange.IsValid)
                break;

            if (id1FirstIndex < id2FirstIndex)
            {
                while (id1FirstIndex < id2FirstIndex)
                {
                    mergedIndexList.Add(id1FirstIndex);

                    id1IndexRange.IncreaseIndex1();

                    if (!id1IndexRange.IsValid)
                        break;

                    id1FirstIndex = _indexSet[id1IndexRange.Index1];
                }

                if (!id1IndexRange.IsValid)
                    break;
            }
            else // id1FirstIndex > id2FirstIndex
            {
                while (id1FirstIndex > id2FirstIndex)
                {
                    mergedIndexList.Add(id2FirstIndex);

                    swapCount += id1IndexRange.Count;
                        
                    id2IndexRange.IncreaseIndex1();

                    if (!id2IndexRange.IsValid)
                        break;

                    id2FirstIndex = indexSet[id2IndexRange.Index1];
                }

                if (!id2IndexRange.IsValid)
                    break;
            }
        }
            
        if (!id1IndexRange.IsValid)
        {
            if (id2IndexRange.IsValid)
                mergedIndexList.AddRange(
                    id2IndexRange.GetItems(indexSet)
                );
        }
        else if (!id2IndexRange.IsValid)
        {
            if (id1IndexRange.IsValid)
                mergedIndexList.AddRange(
                    id1IndexRange.GetItems(_indexSet)
                );
        }
        
        return (
            swapCount,
            Create(mergedIndexList),
            Create(commonIndexList)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<int> GetEnumerator()
    {
        return _indexSet.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return _indexSet
            .Select(i => i.ToString())
            .ConcatenateText(", ", "<", ">");
    }
}