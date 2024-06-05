using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

public class PSeqReadOnlyList1D<T>
    : IPeriodicSequence1D<T>
{
    protected readonly IReadOnlyList<T> DataList;


    public int Count 
        => DataList.Count;

    public T this[int index] 
        => DataList[index.Mod(Count)];

    public bool IsBasic 
        => true;

    public bool IsOperator 
        => false;


    public PSeqReadOnlyList1D(params T[] data)
    {
        DataList = data;
    }

    public PSeqReadOnlyList1D(IReadOnlyList<T> data)
    {
        DataList = data;
    }

    public PSeqReadOnlyList1D(IEnumerable<T> data)
    {
        DataList = data.ToArray();
    }


    public IEnumerator<T> GetEnumerator()
    {
        return DataList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return DataList.GetEnumerator();
    }
}