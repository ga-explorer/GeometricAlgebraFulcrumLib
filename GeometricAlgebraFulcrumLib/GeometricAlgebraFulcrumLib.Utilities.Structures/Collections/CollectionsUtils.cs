using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Lists;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.PeriodicLists2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;

public static class CollectionsUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<T> GetItemsPair<T>(this IReadOnlyList<T> inputsList, IPair<int> itemIndices)
    {
        return new Pair<T>(
            inputsList[itemIndices.Item1],
            inputsList[itemIndices.Item2]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<T> GetItemsPair<T>(this IReadOnlyList<T> inputsList, int index1)
    {
        return new Pair<T>(
            inputsList[index1],
            inputsList[index1 + 1]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<T> GetItemsPair<T>(this IReadOnlyList<T> inputsList, int index1, int index2)
    {
        return new Pair<T>(
            inputsList[index1],
            inputsList[index2]
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<T> GetItemsTriplet<T>(this IReadOnlyList<T> inputsList, ITriplet<int> itemIndices)
    {
        return new Triplet<T>(
            inputsList[itemIndices.Item1],
            inputsList[itemIndices.Item2],
            inputsList[itemIndices.Item3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<T> GetItemsTriplet<T>(this IReadOnlyList<T> inputsList, int index1, int index2, int index3)
    {
        return new Triplet<T>(
            inputsList[index1],
            inputsList[index2],
            inputsList[index3]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<T> GetItemsTriplet<T>(this IReadOnlyList<T> inputsList, int index1)
    {
        return new Triplet<T>(
            inputsList[index1],
            inputsList[index1 + 1],
            inputsList[index1 + 2]
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<T> GetItemsQuad<T>(this IReadOnlyList<T> inputsList, IQuad<int> itemIndices)
    {
        return new Quad<T>(
            inputsList[itemIndices.Item1],
            inputsList[itemIndices.Item2],
            inputsList[itemIndices.Item3],
            inputsList[itemIndices.Item4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<T> GetItemsQuad<T>(this IReadOnlyList<T> inputsList, int index1, int index2, int index3, int index4)
    {
        return new Quad<T>(
            inputsList[index1],
            inputsList[index2],
            inputsList[index3],
            inputsList[index4]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<T> GetItemsQuad<T>(this IReadOnlyList<T> inputsList, int index1)
    {
        return new Quad<T>(
            inputsList[index1],
            inputsList[index1 + 1],
            inputsList[index1 + 2],
            inputsList[index1 + 3]
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetItemIndex<T>(this IReadOnlyList2D<T> inputList, int index1, int index2)
    {
        index1 = index1.Mod(inputList.Count1);
        index2 = index2.Mod(inputList.Count2);

        return index1 + index2 * inputList.Count1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<int, int> GetItemIndexTuple<T>(this IPeriodicReadOnlyList2D<T> inputList, int index)
    {
        index = index.Mod(inputList.Count);

        var index1 = index % inputList.Count1;
        var index2 = (index - index1) / inputList.Count1;

        return new Tuple<int, int>(index1, index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<int, int> GetItemIndexTuple<T>(this IReadOnlyList2D<T> inputList, int index)
    {
        index = index.Mod(inputList.Count);

        var index1 = index % inputList.Count1;
        var index2 = (index - index1) / inputList.Count1;

        return new Tuple<int, int>(index1, index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<int> GetItemIndexPair<T>(this IReadOnlyList2D<T> inputList, int index)
    {
        index = index.Mod(inputList.Count);

        var index1 = index % inputList.Count1;
        var index2 = (index - index1) / inputList.Count1;

        return new Pair<int>(index1, index2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrEmpty<T>(this IReadOnlyCollection<T>? collection)
    {
        return collection == null || collection.Count == 0;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MappedReadOnlyList<T> CreateMappedList<T>(this IReadOnlyList<T> baseList, Func<T, T> itemMapping)
    {
        return new MappedReadOnlyList<T>(baseList, itemMapping);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MappedReadOnlyList<T1, T2> CreateMappedList<T1, T2>(this IReadOnlyList<T1> baseList, Func<T1, T2> itemMapping)
    {
        return new MappedReadOnlyList<T1, T2>(baseList, itemMapping);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BijectiveList<TKey, TValue> ToBijectiveList<TKey, TValue>(this IEnumerable<TValue> baseList, Func<TValue, TKey> valueToKeyMap) 
        where TKey : IEquatable<TKey>
    {
        return new BijectiveList<TKey, TValue>(baseList.ToImmutableArray(), valueToKeyMap);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BijectiveList<TKey, TValue> ToBijectiveList<TKey, TValue>(this IReadOnlyList<TValue> baseList, Func<TValue, TKey> valueToKeyMap) 
        where TKey : IEquatable<TKey>
    {
        return new BijectiveList<TKey, TValue>(baseList, valueToKeyMap);
    }
}