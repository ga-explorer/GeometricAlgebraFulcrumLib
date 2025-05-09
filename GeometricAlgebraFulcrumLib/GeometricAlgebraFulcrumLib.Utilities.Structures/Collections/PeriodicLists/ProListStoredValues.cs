using System.Collections;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.PeriodicLists;

public class ProListStoredValues<TValue> :
    IPeriodicReadOnlyList<TValue>
{
    private readonly TValue[] _valuesArray;

    public int Count 
        => _valuesArray.Length;

    public TValue this[int index] 
        => _valuesArray[index.Mod(Count)];


    public ProListStoredValues(TValue[] valuesArray)
    {
        Debug.Assert(valuesArray.Length > 0);

        _valuesArray = valuesArray;
    }


    public IEnumerator<TValue> GetEnumerator()
    {
        return (IEnumerator<TValue>)_valuesArray.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}