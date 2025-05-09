namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections.PeriodicLists;

public static class PeriodicListsUtils
{
    public static ProListStoredValues<TValue> GetProListStoredValues<TValue>(this IPeriodicReadOnlyList<TValue> proList)
    {
        if (proList is ProListStoredValues<TValue> storedValuesList)
            return storedValuesList;

        return new ProListStoredValues<TValue>(proList.ToArray());
    }

    public static ProListMappedValues<TValue1, TValue2> GetProListMappedValues<TValue1, TValue2>(this IPeriodicReadOnlyList<TValue1> proList, Func<TValue1, TValue2> valueMapping)
    {
        return new ProListMappedValues<TValue1, TValue2>(
            proList,
            valueMapping
        );
    }
}