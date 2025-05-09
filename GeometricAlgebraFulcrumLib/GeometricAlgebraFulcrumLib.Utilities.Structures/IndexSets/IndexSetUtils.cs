using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using Open.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

public static class IndexSetUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<IndexSet, T> CreateIndexSetDictionary<T>()
    {
        return new Dictionary<IndexSet, T>(IndexSetEqualityComparer.Instance);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong IndexToUInt64IndexSetBitPattern(this int index)
    {
        return index is >= 0 and < 64
            ? 1UL << index
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
    public static ulong IndexSetToUInt64BitPattern(this ImmutableSortedSet<int> sortedIndexSet)
    {
        return !sortedIndexSet.IsEmpty && sortedIndexSet[^1] >= 64
            ? throw new InvalidOperationException()
            : sortedIndexSet.Aggregate(0UL, (a, b) => a | (1UL << b));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet IndexToIndexSet(this int index)
    {
        return IndexSet.Create(index);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet IndexPairToIndexSet(int index1, int index2)
    {
        return IndexSet.Create(index1, index2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet IndexPairToIndexSet(this IPair<int> indexPair)
    {
        var index1 = indexPair.Item1;
        var index2 = indexPair.Item2;

        return IndexSet.Create(index1, index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet IndexTripletToIndexSet(int index1, int index2, int index3)
    {
        return IndexSet.Create(index1, index2, index3);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet IndexTripletToIndexSet(ITriplet<int> indexTriplet)
    {
        var index1 = indexTriplet.Item1;
        var index2 = indexTriplet.Item2;
        var index3 = indexTriplet.Item3;

        return IndexSet.Create(index1, index2, index3);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet IndexRangeToIndexSet(this int firstIndex, int count)
    {
        Debug.Assert(
            firstIndex >= 0 && count > 0
        );

        return IndexSet.CreateDense(firstIndex, count);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet BitPatternToIndexSet(this ulong bitPattern)
    {
        return bitPattern.GetSetBitPositions().ToArray().ToIndexSet(true);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet ToIndexSet(this IPair<int> indexList, bool assumeDistinctSorted)
    {
        return assumeDistinctSorted 
            ? IndexSet.Create(
                indexList.Item1, 
                indexList.Item2
            ) 
            : indexList.GetItems().ToArray().ToIndexSet(false);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet ToIndexSet(this ITriplet<int> indexList, bool assumeDistinctSorted)
    {
        return assumeDistinctSorted 
            ? IndexSet.Create(
                indexList.Item1, 
                indexList.Item2, 
                indexList.Item3
            ) 
            : indexList.GetItems().ToArray().ToIndexSet(false);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet ToIndexSet(this IQuad<int> indexList, bool assumeDistinctSorted)
    {
        return assumeDistinctSorted 
            ? IndexSet.Create(
                indexList.Item1, 
                indexList.Item2, 
                indexList.Item3, 
                indexList.Item4
            ) 
            : indexList.GetItems().ToArray().ToIndexSet(false);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet ToIndexSet(this IQuint<int> indexList, bool assumeDistinctSorted)
    {
        return assumeDistinctSorted 
            ? IndexSet.Create(
                indexList.Item1, 
                indexList.Item2, 
                indexList.Item3, 
                indexList.Item4, 
                indexList.Item5
            ) 
            : indexList.GetItems().ToArray().ToIndexSet(false);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet ToIndexSet(this IHexad<int> indexList, bool assumeDistinctSorted)
    {
        return assumeDistinctSorted 
            ? IndexSet.Create(
                indexList.Item1, 
                indexList.Item2, 
                indexList.Item3, 
                indexList.Item4, 
                indexList.Item5, 
                indexList.Item6
            ) 
            : indexList.GetItems().ToArray().ToIndexSet(false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet ToIndexSet(this IEnumerable<int> indexList, bool assumeDistinctSorted)
    {
        return assumeDistinctSorted 
            ? IndexSet.Create(indexList.ToArray()) 
            : IndexSet.Create(indexList.Distinct().OrderBy(i => i).ToArray());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet ToIndexSet(this int[] indexArray, bool assumeDistinctSorted)
    {
        if (indexArray.Length == 0)
            return IndexSet.EmptySet;

        return assumeDistinctSorted 
            ? IndexSet.Create(indexArray)
            : IndexSet.Create(indexArray.Distinct().OrderBy(i => i).ToArray());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet ToIndexSet(this ImmutableSortedSet<int> indexList)
    {
        return IndexSet.Create(indexList.ToArray());
    }

    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ImmutableSortedSet<int> ToSortedSet(this IndexSet indexSet)
    {
        return indexSet.ToImmutableSortedSet();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ImmutableSortedSet<int>.Builder ToSortedSetBuilder(this IndexSet indexSet)
    {
        var builder = ImmutableSortedSet.CreateBuilder<int>();

        if (!indexSet.IsEmptySet)
            builder.AddRange(indexSet);

        return builder;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetItems<T>(this IReadOnlyList<T> itemList, IndexSet indexSet)
    {
        return indexSet.IsEmptySet 
            ? [] 
            : indexSet.Select(i => itemList[i]);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IndexItemRecord<T>> GetIndexItems<T>(this IReadOnlyList<T> itemList, IndexSet indexSet)
    {
        if (indexSet.IsEmptySet)
            return [];

        return indexSet.Select(i => 
            new IndexItemRecord<T>(i, itemList[i])
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetExistingItems<T>(this IReadOnlyList<T> itemList, IndexSet indexSet)
    {
        if (indexSet.IsEmptySet)
            return [];

        return indexSet
            .Where(i => i >= 0 && i < itemList.Count)
            .Select(i => itemList[i]);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IndexItemRecord<T>> GetExistingIndexItems<T>(this IReadOnlyList<T> itemList, IndexSet indexSet)
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
    public static Pair<IndexSet> Sort(IndexSet a, IndexSet b)
    {
        return a.CompareTo(b) <= 0
            ? new Pair<IndexSet>(a, b)
            : new Pair<IndexSet>(b, a);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<IndexSet> Sort(IndexSet a, IndexSet b, IndexSet c)
    {
        if (a.CompareTo(b) <= 0)
        {
            if (b.CompareTo(c) <= 0)
                return new Triplet<IndexSet>(a, b, c);

            return a.CompareTo(c) <= 0
                ? new Triplet<IndexSet>(a, c, b)
                : new Triplet<IndexSet>(c, a, b);
        }

        if (a.CompareTo(c) <= 0)
            return new Triplet<IndexSet>(b, a, c);

        return b.CompareTo(c) <= 0
            ? new Triplet<IndexSet>(b, c, a)
            : new Triplet<IndexSet>(c, b, a);
    }
}