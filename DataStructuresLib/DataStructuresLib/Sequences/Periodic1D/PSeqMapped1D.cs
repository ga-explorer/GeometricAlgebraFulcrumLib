using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Sequences.Periodic1D;

public abstract class PSeqMapped1D<T>
    : IPeriodicSequence1D<T>
{
    public IPeriodicSequence1D<T> BaseSequence { get; }

    protected abstract T MappingFunction(T input);

    public int Count 
        => BaseSequence.Count;

    public T this[int index] 
        => MappingFunction(BaseSequence[index]);

    public bool IsBasic 
        => false;

    public bool IsOperator 
        => true;


    protected PSeqMapped1D(IPeriodicSequence1D<T> baseSequence)
    {
        BaseSequence = baseSequence;
    }


    public IEnumerator<T> GetEnumerator()
    {
        return BaseSequence.Select(MappingFunction).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return BaseSequence.Select(MappingFunction).GetEnumerator();
    }
}

public abstract class PSeqMapped1D<TU, T>
    : IPeriodicSequence1D<T>
{
    public IPeriodicSequence1D<TU> BaseSequence { get; }

    protected abstract T MappingFunction(TU input);

    public int Count 
        => BaseSequence.Count;

    public T this[int index] 
        => MappingFunction(BaseSequence[index]);

    public Pair<T> this[int index1, int index2] 
        => new Pair<T>(
            MappingFunction(BaseSequence[index1]),
            MappingFunction(BaseSequence[index2])
        );

    public bool IsBasic 
        => false;

    public bool IsOperator 
        => true;


    protected PSeqMapped1D(IPeriodicSequence1D<TU> baseSequence)
    {
        BaseSequence = baseSequence;
    }


    public IEnumerator<T> GetEnumerator()
    {
        return BaseSequence.Select(MappingFunction).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return BaseSequence.Select(MappingFunction).GetEnumerator();
    }
}