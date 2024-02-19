using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Collections.PeriodicLists2D;

namespace DataStructuresLib.Collections.PeriodicLists;

public class ProList2DRow<TValue> :
    IPeriodicReadOnlyList<TValue>
{
    public IPeriodicReadOnlyList2D<TValue> SourceList { get; }

    public int RowIndex { get; }

    public int Count 
        => SourceList.Count2;

    public TValue this[int index] 
        => SourceList[RowIndex, index];


    public ProList2DRow(IPeriodicReadOnlyList2D<TValue> sourceList, int rowIndex)
    {
        SourceList = sourceList;
        RowIndex = rowIndex;
    }


    public IEnumerator<TValue> GetEnumerator()
    {
        return Enumerable
            .Range(0, SourceList.Count2)
            .Select(i => SourceList[RowIndex, i])
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}