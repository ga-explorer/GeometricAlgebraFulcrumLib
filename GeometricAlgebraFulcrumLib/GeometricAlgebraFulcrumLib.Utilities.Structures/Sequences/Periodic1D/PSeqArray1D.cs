using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

public class PSeqArray1D<T>
    : IPeriodicSequence1D<T>
{
    protected readonly T[] DataArray;


    public int Count 
        => DataArray.Length;

    public T this[int index]
    {
        get => DataArray[index.Mod(Count)];
        set => DataArray[index.Mod(Count)] = value;
    }

    public bool IsBasic 
        => true;

    public bool IsOperator 
        => false;


    public PSeqArray1D(int count)
    {
        DataArray = new T[count];
    }

    public PSeqArray1D(params T[] dataArray)
    {
        DataArray = dataArray;
    }

    public PSeqArray1D(IEnumerable<T> dataList)
    {
        DataArray = dataList.ToArray();
    }


    public IEnumerator<T> GetEnumerator()
    {
        return ((IEnumerable<T>)DataArray).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<T>)DataArray).GetEnumerator();
    }
}