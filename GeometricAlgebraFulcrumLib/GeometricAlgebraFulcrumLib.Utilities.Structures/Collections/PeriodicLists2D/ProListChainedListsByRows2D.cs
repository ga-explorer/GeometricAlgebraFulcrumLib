using System.Collections;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.PeriodicLists2D;

public class ProListChainedListsByRows2D<TValue> :
    IPeriodicReadOnlyList2D<TValue>
{
    private readonly List<IPeriodicReadOnlyList2D<TValue>> _rowsList
        = new List<IPeriodicReadOnlyList2D<TValue>>();


    public int Count 
        => Count1 * Count2;

    public int Count1 
        => _rowsList.Sum(lattice => lattice.Count2);

    public int Count2 { get; }

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
            index1 = index1.Mod(Count1);
            foreach (var rowList in _rowsList)
            {
                if (index1 < rowList.Count1)
                    return rowList[index1, index2];

                index1 -= rowList.Count1;
            }

            //This should never happen
            throw new InvalidOperationException();
        }
    }


    public ProListChainedListsByRows2D(int count2)
    {
        Debug.Assert(count2 > 0);

        Count2 = count2;
    }


    public TValue[,] ToArray2D()
    {
        var valuesArray = new TValue[Count1, Count2];

        var index1 = 0;
        foreach (var rowList in _rowsList)
        {
            for (var sourceIndex1 = 0; sourceIndex1 < rowList.Count1; sourceIndex1++)
            {
                for (var index2 = 0; index2 < Count2; index2++)
                    valuesArray[index1, index2] = rowList[index1, sourceIndex1];

                index1++;
            }
        }

        return valuesArray;
    }

    public ProListChainedListsByRows2D<TValue> AppendList(IPeriodicReadOnlyList2D<TValue> rowList)
    {
        Debug.Assert(rowList.Count2 == Count2);

        _rowsList.Add(rowList);

        return this;
    }

    public ProListChainedListsByRows2D<TValue> PrependList(IPeriodicReadOnlyList2D<TValue> rowList)
    {
        Debug.Assert(rowList.Count2 == Count2);

        _rowsList.Insert(0, rowList);

        return this;
    }

    public ProListChainedListsByRows2D<TValue> PrependList(IPeriodicReadOnlyList2D<TValue> rowList, int index)
    {
        Debug.Assert(rowList.Count2 == Count2);

        _rowsList.Insert(index, rowList);

        return this;
    }

    public ProListChainedListsByRows2D<TValue> RemoveList(int index)
    {
        _rowsList.RemoveAt(index);

        return this;
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        for (var index2 = 0; index2 < Count2; index2++)
        {
            foreach (var rowList in _rowsList)
                for (var sourceIndex1 = 0; sourceIndex1 < rowList.Count1; sourceIndex1++)
                    yield return rowList[sourceIndex1, index2];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}