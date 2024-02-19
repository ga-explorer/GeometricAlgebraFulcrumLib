using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.Collections.PeriodicLists;

namespace DataStructuresLib.Collections.PeriodicLists2D;

public class ProListStoredColumns2D<TValue> : 
    IPeriodicReadOnlyList2D<TValue>
{
    private readonly List<IPeriodicReadOnlyList<TValue>> _columnsList
        = new List<IPeriodicReadOnlyList<TValue>>();


    public int Count1 { get; }

    public int Count2 
        => _columnsList.Count;

    public int Count 
        => Count1 * _columnsList.Count;

    public TValue this[int index] 
    {
        get
        {
            var (index1, index2) = 
                this.GetItemIndexTuple(index);

            var columnList = _columnsList[index2];

            return columnList[index1];
        }
    }

    public TValue this[int index1, int index2]
    {
        get
        {
            var columnList = _columnsList[index2];

            return columnList[index1];
        }
    }

    public IEnumerable<IPeriodicReadOnlyList<TValue>> SourceLists
        => _columnsList;


    public ProListStoredColumns2D(int count1)
    {
        Debug.Assert(count1 > 0);

        Count1 = count1;
    }


    public ProListStoredColumns2D<TValue> AppendColumn(IPeriodicReadOnlyList<TValue> column)
    {
        Debug.Assert(column.Count == Count1);

        _columnsList.Add(column);

        return this;
    }

    public ProListStoredColumns2D<TValue> PrependColumn(IPeriodicReadOnlyList<TValue> column)
    {
        Debug.Assert(column.Count == Count1);

        _columnsList.Insert(0, column);

        return this;
    }

    public ProListStoredColumns2D<TValue> InsertColumn(IPeriodicReadOnlyList<TValue> column, int index)
    {
        Debug.Assert(column.Count == Count1);

        _columnsList.Insert(index, column);

        return this;
    }
        
    public ProListStoredColumns2D<TValue> RemoveColumn(int index)
    {
        _columnsList.RemoveAt(index);

        return this;
    }

    public TValue[,] ToArray2D()
    {
        var valuesArray = new TValue[Count1, Count2];

        for (var index2 = 0; index2 < Count2; index2++)
        {
            var column = 
                _columnsList[index2];

            for (var index1 = 0; index1 < Count1; index1++)
                valuesArray[index1, index2] = column[index1];
        }

        return valuesArray;
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        return _columnsList
            .SelectMany(columnList => columnList)
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}