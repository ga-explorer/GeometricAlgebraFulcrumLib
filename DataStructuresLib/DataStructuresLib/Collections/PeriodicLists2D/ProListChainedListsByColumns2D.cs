using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Collections.PeriodicLists2D;

public class ProListChainedListsByColumns2D<TValue> :
    IPeriodicReadOnlyList2D<TValue>
{
    private readonly List<IPeriodicReadOnlyList2D<TValue>> _columnsList
        = new List<IPeriodicReadOnlyList2D<TValue>>();


    public int Count 
        => Count1 * Count2;

    public int Count1 { get; }

    public int Count2 
        => _columnsList.Sum(lattice => lattice.Count2);

    public TValue this[int index]
    {
        get
        {
            var (index1, index2) = 
                this.GetItemIndexTuple(index);

            return this[index1, index2];
        }
    }

    public TValue this[int index1, int index2]
    {
        get
        {
            index2 = index2.Mod(Count2);
            foreach (var columnList in _columnsList)
            {
                if (index2 < columnList.Count2)
                    return columnList[index1, index2];

                index2 -= columnList.Count2;
            }

            //This should never happen
            throw new InvalidOperationException();
        }
    }


    public ProListChainedListsByColumns2D(int count1)
    {
        Debug.Assert(count1 > 0);

        Count1 = count1;
    }


    public TValue[,] ToArray2D()
    {
        var valuesArray = new TValue[Count1, Count2];

        var index2 = 0;
        foreach (var columnList in _columnsList)
        {
            for (var sourceIndex2 = 0; sourceIndex2 < columnList.Count2; sourceIndex2++)
            {
                for (var index1 = 0; index1 < Count1; index1++)
                    valuesArray[index1, index2] = columnList[index1, sourceIndex2];

                index2++;
            }
        }

        return valuesArray;
    }

    public ProListChainedListsByColumns2D<TValue> AppendList(IPeriodicReadOnlyList2D<TValue> columnList)
    {
        Debug.Assert(columnList.Count1 == Count1);

        _columnsList.Add(columnList);

        return this;
    }

    public ProListChainedListsByColumns2D<TValue> PrependList(IPeriodicReadOnlyList2D<TValue> columnList)
    {
        Debug.Assert(columnList.Count1 == Count1);

        _columnsList.Insert(0, columnList);

        return this;
    }

    public ProListChainedListsByColumns2D<TValue> PrependList(IPeriodicReadOnlyList2D<TValue> columnList, int index)
    {
        Debug.Assert(columnList.Count1 == Count1);

        _columnsList.Insert(index, columnList);

        return this;
    }

    public ProListChainedListsByColumns2D<TValue> RemoveList(int index)
    {
        _columnsList.RemoveAt(index);

        return this;
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