using System;
using System.Collections;
using System.Collections.Generic;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Collections.PeriodicLists2D;

public class ProListComputedValues2D<TValue> :
    IPeriodicReadOnlyList2D<TValue>
{
    public Func<int, int, TValue> ComputingFunc { get; }

    public int Count 
        => Count1 * Count2;

    public int Count1 { get; }

    public int Count2 { get; }

    public TValue this[int index]
    {
        get
        {
            var (index1, index2) =
                this.GetItemIndexTuple(index);

            return ComputingFunc(
                index1.Mod(Count1),
                index2.Mod(Count2)
            );
        }
    }

    public TValue this[int index1, int index2] 
        => ComputingFunc(
            index1.Mod(Count1),
            index2.Mod(Count2)
        );


    public ProListComputedValues2D(int count1, int count2, Func<int, int, TValue> computingFunc)
    {
        Count1 = count1;
        Count2 = count2;
        ComputingFunc = computingFunc;
    }


    public TValue[,] ToArray2D()
    {
        var valuesArray = new TValue[Count1, Count2];

        for (var index2 = 0; index2 < Count2; index2++)
        for (var index1 = 0; index1 < Count1; index1++)
            valuesArray[index1, index2] = ComputingFunc(index1, index2);

        return valuesArray;
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        for (var index2 = 0; index2 < Count2; index2++)
        for (var index1 = 0; index1 < Count1; index1++)
            yield return ComputingFunc(index1, index2);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}