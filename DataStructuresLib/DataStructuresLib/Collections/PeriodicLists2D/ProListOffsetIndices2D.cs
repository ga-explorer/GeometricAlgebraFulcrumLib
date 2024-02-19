using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructuresLib.Collections.PeriodicLists2D;

public class ProListOffsetIndices2D<TValue> :
    IPeriodicReadOnlyList2D<TValue>
{
    public IPeriodicReadOnlyList2D<TValue> SourceList { get; }

    public int Count
        => SourceList.Count;

    public int Count1 
        => SourceList.Count1;

    public int Count2 
        => SourceList.Count2;

    public int Offset1 { get; }

    public int Offset2 { get; }

    public TValue this[int index]
    {
        get
        {
            var (index1, index2) = 
                this.GetItemIndexTuple(index);

            return SourceList[
                Offset1 + index1,
                Offset2 + index2
            ];
        }
    }

    public TValue this[int index1, int index2] 
        => SourceList[
            Offset1 + index1, 
            Offset2 + index2
        ];


    public ProListOffsetIndices2D(IPeriodicReadOnlyList2D<TValue> sourceLattice, int offset1, int offset2)
    {
        SourceList = sourceLattice;
        Offset1 = offset1;
        Offset2 = offset2;
    }


    public TValue[,] ToArray2D()
    {
        var valuesArray = new TValue[Count1, Count2];

        for (var index2 = 0; index2 < Count2; index2++)
        {
            var sourceIndex2 = Offset2 + index2;

            for (var index1 = 0; index1 < Count1; index1++)
            {
                var sourceIndex1 = Offset1 + index1;

                valuesArray[index1, index2] = 
                    SourceList[sourceIndex1, sourceIndex2];
            }
        }

        return valuesArray;
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        for (var index2 = 0; index2 < Count2; index2++)
        {
            var sourceIndex2 = Offset2 + index2;

            for (var index1 = 0; index1 < Count1; index1++)
            {
                var sourceIndex1 = Offset1 + index1;

                yield return SourceList[sourceIndex1, sourceIndex2];
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}