using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.PeriodicLists;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.PeriodicLists2D;

public static class PeriodicLists2DUtils
{
    public static ProList2DIndexedItem<TValue> GetIndexedItem<TValue>(this IPeriodicReadOnlyList2D<TValue> proList, int index1, int index2)
    {
        return new ProList2DIndexedItem<TValue>(proList, index1, index2);
    }

    public static ProList2DIndexedItem<TValue> GetIndexedItem<TValue>(this IPeriodicReadOnlyList2D<TValue> proList, int index)
    {
        var (index1, index2) = proList.GetItemIndexTuple(index);

        return new ProList2DIndexedItem<TValue>(proList, index1, index2);
    }

    public static ProListStoredValues2D<TValue> GetProListStoredValues2D<TValue>(this IPeriodicReadOnlyList2D<TValue> proList)
    {
        if (proList is ProListStoredValues2D<TValue> storedValuesList)
            return storedValuesList;

        return new ProListStoredValues2D<TValue>(proList.ToArray2D());
    }

    public static IPeriodicReadOnlyList2D<TValue> GetProListTransposedList2D<TValue>(this IPeriodicReadOnlyList2D<TValue> proList)
    {
        if (proList is ProListTransposedList2D<TValue> transposeList)
            return transposeList.SourceList;

        return new ProListTransposedList2D<TValue>(proList);
    }

    public static ProListCartesianProduct2D<TValue1, TValue2, TValue> GetProListCartesianProduct2D<TValue1, TValue2, TValue>(this IPeriodicReadOnlyList<TValue1> proList1, IPeriodicReadOnlyList<TValue2> proList2, Func<TValue1, TValue2, TValue> combinationFunc)
    {
        return new ProListCartesianProduct2D<TValue1, TValue2, TValue>(
            proList1,
            proList2,
            combinationFunc
        );
    }

    public static ProListMappedValues2D<TValue1, TValue2> GetProListMappedValues2D<TValue1, TValue2>(this IPeriodicReadOnlyList2D<TValue1> proList, Func<TValue1, TValue2> valueMapping)
    {
        return new ProListMappedValues2D<TValue1, TValue2>(
            proList,
            valueMapping
        );
    }
}