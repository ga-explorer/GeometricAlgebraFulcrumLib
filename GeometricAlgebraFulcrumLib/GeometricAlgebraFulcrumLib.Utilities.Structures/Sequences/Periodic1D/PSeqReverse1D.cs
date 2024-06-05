using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

public class PSeqReverse1D<T>
    : IPeriodicSequence1D<T>
{
    public IPeriodicSequence1D<T> BaseSequence { get; }

    public int Count 
        => BaseSequence.Count;

    public T this[int index] 
        => BaseSequence[Count - index.Mod(Count) - 1];

    public bool IsBasic 
        => false;

    public bool IsOperator 
        => true;


    public PSeqReverse1D(IPeriodicSequence1D<T> baseSequence)
    {
        BaseSequence = baseSequence;
    }


    public IEnumerator<T> GetEnumerator()
    {
        for (var index = BaseSequence.Count - 1; index >= 0; index--)
            yield return BaseSequence[index];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}