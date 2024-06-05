using System.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.PeriodicLists2D;

public class ProListConstantValues2D<TValue> :
    IPeriodicReadOnlyList2D<TValue>
{
    public TValue Value { get; }

    public int Count 
        => Count1 * Count2;

    public int Count1 { get; }

    public int Count2 { get; }

    public TValue this[int index] 
        => Value;

    public TValue this[int index1, int index2] 
        => Value;


    public ProListConstantValues2D(int rowsCount, int columnsCount, TValue value)
    {
        Count1 = rowsCount;
        Count2 = columnsCount;
        Value = value;
    }


    public TValue[,] ToArray2D()
    {
        var valuesArray = new TValue[Count1, Count2];

        for (var index2 = 0; index2 < Count2; index2++)
        for (var index1 = 0; index1 < Count1; index1++)
            valuesArray[index2, index1] = Value;

        return valuesArray;
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        return Enumerable.Repeat(Value, Count).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}