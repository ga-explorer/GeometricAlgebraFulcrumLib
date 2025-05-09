using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections.PeriodicLists2D;

public class ProListTransposedList2D<TValue> :
    IPeriodicReadOnlyList2D<TValue>
{
    public IPeriodicReadOnlyList2D<TValue> SourceList { get; }

    public int Count 
        => SourceList.Count;

    public int Count1 
        => SourceList.Count2;

    public int Count2 
        => SourceList.Count1;

    public TValue this[int index] 
    {
        get
        {
            var (index1, index2) = 
                this.GetItemIndexTuple(index);

            return SourceList[index2, index1];
        }
    }

    public TValue this[int index1, int index2] 
        => SourceList[index2, index1];


    public ProListTransposedList2D([NotNull] IPeriodicReadOnlyList2D<TValue> sourceList)
    {
        SourceList = sourceList;
    }


    public TValue[,] ToArray2D()
    {
        var valuesArray = new TValue[Count1, Count2];

        for (var index2 = 0; index2 < Count2; index2++)
        for (var index1 = 0; index1 < Count1; index1++)
            valuesArray[index1, index2] = SourceList[index2, index1];

        return valuesArray;
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        for (var index2 = 0; index2 < SourceList.Count1; index2++)
        for (var index1 = 0; index1 < SourceList.Count2; index1++)
            yield return SourceList[index2, index1];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}