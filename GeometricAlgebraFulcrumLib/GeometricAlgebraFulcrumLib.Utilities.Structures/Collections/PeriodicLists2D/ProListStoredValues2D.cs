using System.Collections;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.PeriodicLists2D;

public class ProListStoredValues2D<TValue> :
    IPeriodicReadOnlyList2D<TValue>
{
    private readonly TValue[,] _valuesArray;

    public int Count
        => _valuesArray.Length;

    public int Count1 
        => _valuesArray.GetLength(0);

    public int Count2 
        => _valuesArray.GetLength(1);

    public TValue this[int index]
    {
        get
        {
            var (index1, index2) =
                this.GetItemIndexTuple(index);

            return _valuesArray[index1, index2];
        }
        set
        {
            var (index1, index2) =
                this.GetItemIndexTuple(index);

            _valuesArray[index1, index2] = value;
        }
    }

    public TValue this[int index1, int index2]
    {
        get => _valuesArray[
            index1.Mod(Count1),
            index2.Mod(Count2)
        ];
        set => _valuesArray[
            index1.Mod(Count1),
            index2.Mod(Count2)
        ] = value;
    }

    public ProListStoredValues2D(TValue[,] valuesArray)
    {
        Debug.Assert(valuesArray.Length > 0);

        _valuesArray = valuesArray;
    }


    public TValue[,] ToArray2D()
    {
        return _valuesArray;
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        for (var index2 = 0; index2 < Count2; index2++)
        for (var index1 = 0; index1 < Count1; index1++)
            yield return _valuesArray[index1, index2];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}