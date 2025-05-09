using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using Open.Collections;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

public static class IndexSetUtils
{
    public static EmptyIndexSet EmptySet 
        => EmptyIndexSet.Instance;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<IIndexSet, T> CreateIndexSetDictionary<T>()
    {
        return new Dictionary<IIndexSet, T>(IndexSetEqualityComparer.Instance);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IIndexSet IndexToIndexSet(this int index)
    {
        return index is >= 0 and < 64
            ? UInt64IndexSet.SimpleIndexSets[index]
            : SingleIndexSet.Create(index);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SingleIndexSet IndexToSingleIndexSet(this int index)
    {
        return SingleIndexSet.Create(index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SparseIndexSet IndexToSparseIndexSet(this int index)
    {
        return SparseIndexSet.Create(index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt64IndexSet IndexToUInt64IndexSet(this int index)
    {
        return index is >= 0 and < 64
            ? UInt64IndexSet.SimpleIndexSets[index]
            : throw new InvalidOperationException();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong IndexToUInt64IndexSetBitPattern(this int index)
    {
        return index is >= 0 and < 64
            ? 1UL << index
            : throw new InvalidOperationException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IIndexSet IndexPairToIndexSet(int index1, int index2)
    {
        if (index1 is >= 0 and < 64 && index2 is >= 0 and < 64)
            return UInt64IndexSet.Create((1UL << index1) | (1UL << index2));

        if (index2 == index1 + 1)
            return DenseIndexSet.Create(index1, 2);

        if (index1 == index2 + 1)
            return DenseIndexSet.Create(index2, 2);

        return SparseIndexSet.Create(index1, index2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IIndexSet IndexTripletToIndexSet(int index1, int index2, int index3)
    {
        if (index1 is >= 0 and < 64 && index2 is >= 0 and < 64 && index3 is >= 0 and < 64)
            return UInt64IndexSet.Create((1UL << index1) | (1UL << index2) | (1UL << index3));

        if (index1 == index2 - 1 && index1 == index3 - 2)
            return DenseIndexSet.Create(index1, 3);
        
        if (index1 == index3 - 1 && index1 == index2 - 2)
            return DenseIndexSet.Create(index1, 3);

        if (index2 == index1 - 1 && index2 == index3 - 2)
            return DenseIndexSet.Create(index2, 2);
        
        if (index2 == index3 - 1 && index2 == index1 - 2)
            return DenseIndexSet.Create(index2, 2);

        if (index3 == index1 - 1 && index3 == index2 - 2)
            return DenseIndexSet.Create(index3, 2);
        
        if (index3 == index2 - 1 && index3 == index1 - 2)
            return DenseIndexSet.Create(index3, 2);

        return SparseIndexSet.Create(index1, index2, index3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SparseIndexSet IndexPairToSparseIndexSet(int index1, int index2)
    {
        return SparseIndexSet.Create(index1, index2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt64IndexSet IndexPairToUInt64IndexSet(int index1, int index2)
    {
        return index1 is >= 0 and < 64 && index2 is >= 0 and < 64
            ? UInt64IndexSet.Create((1UL << index1) | (1UL << index2))
            : throw new InvalidOperationException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IIndexSet IndexPairToIndexSet(this IPair<int> indexPair)
    {
        var index1 = indexPair.Item1;
        var index2 = indexPair.Item2;

        if (index1 is >= 0 and < 64 && index2 is >= 0 and < 64)
            return UInt64IndexSet.Create((1UL << index1) | (1UL << index2));

        if (index2 == index1 + 1)
            return DenseIndexSet.Create(index1, 2);

        if (index1 == index2 + 1)
            return DenseIndexSet.Create(index2, 2);

        return SparseIndexSet.Create(index1, index2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SparseIndexSet IndexPairToSparseIndexSet(this IPair<int> indexPair)
    {
        var index1 = indexPair.Item1;
        var index2 = indexPair.Item2;

        return SparseIndexSet.Create(index1, index2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt64IndexSet IndexPairToUInt64IndexSet(this IPair<int> indexPair)
    {
        var index1 = indexPair.Item1;
        var index2 = indexPair.Item2;

        return index1 is >= 0 and < 64 && index2 is >= 0 and < 64
            ? UInt64IndexSet.Create((1UL << index1) | (1UL << index2))
            : throw new InvalidOperationException();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong IndexPairToUInt64IndexSetBitPattern(int index1, int index2)
    {
        return index1 is >= 0 and < 64 && index2 is >= 0 and < 64
            ? (1UL << index1) | (1UL << index2)
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong IndexPairToUInt64IndexSetBitPattern(this IPair<int> indexPair)
    {
        var index1 = indexPair.Item1;
        var index2 = indexPair.Item2;

        return index1 is >= 0 and < 64 && index2 is >= 0 and < 64
            ? (1UL << index1) | (1UL << index2)
            : throw new InvalidOperationException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IIndexSet IndexRangeToIndexSet(this int firstIndex, int count)
    {
        Debug.Assert(
            firstIndex >= 0 && count > 0
        );

        if (count == 1)
            return firstIndex.IndexToIndexSet();

        var lastIndex = firstIndex + count - 1;

        if (lastIndex < 64)
            return (count.CreateMaskUInt64() << firstIndex).BitPatternToUInt64IndexSet();

        return DenseIndexSet.Create(firstIndex, count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DenseIndexSet IndexRangeToDenseIndexSet(this int firstIndex, int count)
    {
        return DenseIndexSet.Create(firstIndex, count);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IIndexSet BitPatternToIndexSet(this ulong bitPattern)
    {
        return bitPattern == 0UL
            ? EmptySet
            : UInt64IndexSet.Create(bitPattern);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IIndexSet BitPatternToNonUInt64IndexSet(this ulong bitPattern)
    {
        if (bitPattern == 0UL)
            return EmptyIndexSet.Instance;

        var firstIndex = BitOperations.TrailingZeroCount(bitPattern);

        if (BitOperations.IsPow2(bitPattern))
            return SingleIndexSet.Create(firstIndex);

        var lastIndex = 63 - BitOperations.LeadingZeroCount(bitPattern);

        var count = lastIndex - firstIndex + 1;

        Debug.Assert(count > 0);

        if (count == bitPattern.CountOnes())
            return DenseIndexSet.Create(firstIndex, count);

        return SparseIndexSet.Create(
            bitPattern.PatternToPositions().ToImmutableSortedSet()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SparseIndexSet BitPatternToSparseIndexSet(this ulong bitPattern)
    {
        if (bitPattern == 0UL)
            return SparseIndexSet.EmptySet;

        if (!BitOperations.IsPow2(bitPattern))
            return SparseIndexSet.Create(
                bitPattern.PatternToPositions().ToImmutableSortedSet()
            );

        var index = BitOperations.TrailingZeroCount(bitPattern);

        return SparseIndexSet.Create(index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt64IndexSet BitPatternToUInt64IndexSet(this ulong bitPattern)
    {
        return UInt64IndexSet.Create(bitPattern);
    }

    /// <summary>
    /// The input is assumed to be sorted
    /// </summary>
    /// <param name="indexList"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IIndexSet ToIndexSet(this List<int> indexList)
    {
        if (indexList.Count == 0)
            return EmptyIndexSet.Instance;

        var firstIndex = indexList[0];

        if (indexList.Count == 1)
            return firstIndex < 64
                ? UInt64IndexSet.Create(1UL << firstIndex)
                : SingleIndexSet.Create(firstIndex);

        var lastIndex = indexList[^1];

        if (lastIndex < 64)
            return UInt64IndexSet.Create(indexList);
            
        var count = lastIndex - firstIndex + 1; 

        if (count == indexList.Count)
            return DenseIndexSet.Create(firstIndex, count);

        return SparseIndexSet.Create(indexList.ToImmutableSortedSet());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IIndexSet ToIndexSet(this ImmutableSortedSet<int> sortedIndexSet)
    {
        if (sortedIndexSet.IsEmpty)
            return EmptySet;

        if (sortedIndexSet.Count == 1)
        {
            var index = sortedIndexSet[0];

            return index < 64
                ? UInt64IndexSet.Create(1UL << index)
                : SingleIndexSet.Create(index);
        }

        var lastIndex = sortedIndexSet[^1];

        if (lastIndex < 64)
            return UInt64IndexSet.Create(sortedIndexSet);
            
        var firstIndex = sortedIndexSet[0];
        var count = lastIndex - firstIndex + 1;

        return count == sortedIndexSet.Count
            ? DenseIndexSet.Create(firstIndex, count)
            : SparseIndexSet.Create(sortedIndexSet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SparseIndexSet ToSparseIndexSet(this ImmutableSortedSet<int> sortedIndexSet)
    {
        return SparseIndexSet.Create(sortedIndexSet);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt64IndexSet ToUInt64IndexSet(this ImmutableSortedSet<int> sortedIndexSet)
    {
        return !sortedIndexSet.IsEmpty && sortedIndexSet[^1] >= 64
            ? throw new InvalidOperationException()
            : UInt64IndexSet.Create(sortedIndexSet);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong ToUInt64IndexSetBitPattern(this ImmutableSortedSet<int> sortedIndexSet)
    {
        return !sortedIndexSet.IsEmpty && sortedIndexSet[^1] >= 64
            ? throw new InvalidOperationException()
            : sortedIndexSet.Aggregate(0UL, (a, b) => a | (1UL << b));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IIndexSet ToIndexSet(this ImmutableSortedSet<int>.Builder sortedIndexSetBuilder)
    {
        if (sortedIndexSetBuilder.Count == 0)
            return EmptySet;
            
        if (sortedIndexSetBuilder.Count == 1)
        {
            var index = sortedIndexSetBuilder[0];

            return index >= 64
                ? SingleIndexSet.Create(index)
                : UInt64IndexSet.Create(1UL << index);
        }

        var lastIndex = sortedIndexSetBuilder[^1];

        if (lastIndex < 64)
            return UInt64IndexSet.Create(sortedIndexSetBuilder);
            
        var firstIndex = sortedIndexSetBuilder[0];
        var count = lastIndex - firstIndex + 1;

        return count == sortedIndexSetBuilder.Count
            ? DenseIndexSet.Create(firstIndex, count)
            : SparseIndexSet.Create(sortedIndexSetBuilder.ToImmutable());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SparseIndexSet ToSparseIndexSet(this ImmutableSortedSet<int>.Builder sortedIndexSetBuilder)
    {
        return sortedIndexSetBuilder.Count == 0 
            ? SparseIndexSet.EmptySet 
            : SparseIndexSet.Create(sortedIndexSetBuilder.ToImmutable());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt64IndexSet ToUInt64IndexSet(this ImmutableSortedSet<int>.Builder sortedIndexSetBuilder)
    {
        return sortedIndexSetBuilder.Count > 0 && sortedIndexSetBuilder[^1] >= 64
            ? throw new InvalidOperationException()
            : UInt64IndexSet.Create(sortedIndexSetBuilder);
    }


    public static int ToInt32(this IIndexSet indexSet)
    {
        if (indexSet.IsEmptySet) return 0;

        var value = 0;

        foreach (var index in indexSet)
        {
            if (index > 30) 
                throw new InvalidOperationException();

            value |= 1 << index;
        }

        return value;
    }

    public static uint ToUInt32(this IIndexSet indexSet)
    {
        if (indexSet.IsEmptySet) return 0;

        var value = 0U;

        foreach (var index in indexSet)
        {
            if (index > 31) 
                throw new InvalidOperationException();

            value |= 1U << index;
        }

        return value;
    }

    public static long ToInt64(this IIndexSet indexSet)
    {
        if (indexSet.IsEmptySet) return 0;

        var value = 0L;

        foreach (var index in indexSet)
        {
            if (index > 62) 
                throw new InvalidOperationException();

            value |= 1L << index;
        }

        return value;
    }

    public static ulong ToUInt64(this IIndexSet indexSet)
    {
        if (indexSet.TryGetUInt64BitPattern(out var indexBitPattern))
            return indexBitPattern;

        throw new InvalidOperationException();
    }
    
    public static EInteger ToEInteger(this IIndexSet indexSet)
    {
        if (indexSet.IsEmptySet)
            return EInteger.Zero;

        var two = EInteger.FromInt32(2);

        return indexSet.Aggregate(
            EInteger.Zero, 
            (current, index) => current + two.Pow(index)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ImmutableSortedSet<int> ToSortedSet(this IIndexSet indexSet)
    {
        return indexSet is SparseIndexSet indexSet1 
            ? indexSet1.SortedIndexSet 
            : indexSet.ToImmutableSortedSet();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ImmutableSortedSet<int>.Builder ToSortedSetBuilder(this IIndexSet indexSet)
    {
        var builder = ImmutableSortedSet.CreateBuilder<int>();

        if (!indexSet.IsEmptySet)
            builder.AddRange(indexSet);

        return builder;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetItems<T>(this IReadOnlyList<T> itemList, IIndexSet indexSet)
    {
        if (indexSet.IsEmptySet)
            return [];

        return indexSet.Select(i => itemList[i]);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IndexItemRecord<T>> GetIndexItems<T>(this IReadOnlyList<T> itemList, IIndexSet indexSet)
    {
        if (indexSet.IsEmptySet)
            return [];

        return indexSet.Select(i => 
            new IndexItemRecord<T>(i, itemList[i])
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetExistingItems<T>(this IReadOnlyList<T> itemList, IIndexSet indexSet)
    {
        if (indexSet.IsEmptySet)
            return [];

        return indexSet
            .Where(i => i >= 0 && i < itemList.Count)
            .Select(i => itemList[i]);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IndexItemRecord<T>> GetExistingIndexItems<T>(this IReadOnlyList<T> itemList, IIndexSet indexSet)
    {
        if (indexSet.IsEmptySet)
            return [];

        return indexSet
            .Where(i => i >= 0 && i < itemList.Count)
            .Select(i => 
                new IndexItemRecord<T>(i, itemList[i])
            );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<IIndexSet> Sort(IIndexSet a, IIndexSet b)
    {
        return a.CompareTo(b) <= 0
            ? new Pair<IIndexSet>(a, b)
            : new Pair<IIndexSet>(b, a);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<IIndexSet> Sort(IIndexSet a, IIndexSet b, IIndexSet c)
    {
        if (a.CompareTo(b) <= 0)
        {
            if (b.CompareTo(c) <= 0)
                return new Triplet<IIndexSet>(a, b, c);

            return a.CompareTo(c) <= 0
                ? new Triplet<IIndexSet>(a, c, b)
                : new Triplet<IIndexSet>(c, a, b);
        }

        if (a.CompareTo(c) <= 0)
            return new Triplet<IIndexSet>(b, a, c);

        return b.CompareTo(c) <= 0
            ? new Triplet<IIndexSet>(b, c, a)
            : new Triplet<IIndexSet>(c, b, a);
    }
}