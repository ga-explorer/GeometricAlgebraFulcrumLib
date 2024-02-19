using System.Collections;
using System.Collections.Generic;
using DataStructuresLib.Basic;
using DataStructuresLib.Collections;
using DataStructuresLib.Sequences.Periodic1D;

namespace DataStructuresLib.Sequences.Periodic2D;

public class PSeqArray2D<T>
    : IPeriodicSequence2D<T>
{
    private readonly T[,] _dataArray;


    public int Count1
        => _dataArray.GetLength(0);

    public int Count2
        => _dataArray.GetLength(1);

    public int Count 
        => _dataArray.Length;

    public T this[int index]
    {
        get
        {
            var (index1, index2) = this.GetItemIndexPair(index);

            return _dataArray[index1, index2];
        }
        set
        {
            var (index1, index2) = this.GetItemIndexPair(index);

            _dataArray[index1, index2] = value;
        }
    }

    public T this[int index1, int index2]
    {
        get => _dataArray[index1.Mod(Count1), index2.Mod(Count2)];
        set => _dataArray[index1.Mod(Count1), index2.Mod(Count2)] = value;
    }

    public bool IsBasic
        => true;

    public bool IsOperator
        => false;


    public PSeqArray2D(int count1, int count2)
    {
        _dataArray = new T[count1, count2];
    }

    public PSeqArray2D(T[,] dataArray)
    {
        _dataArray = dataArray;
    }


    public virtual PSeqSlice1D<T> GetSliceAt(int dimension, int index)
    {
        return new PSeqSlice1D<T>(this, dimension, index);
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var i2 = 0; i2 < Count2; i2++)
        for (var i1 = 0; i1 < Count1; i1++)
            yield return _dataArray[i1, i2];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}