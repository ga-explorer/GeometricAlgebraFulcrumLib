using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Collections;
using DataStructuresLib.Sequences.Periodic1D;

namespace DataStructuresLib.Sequences.Periodic2D;

public abstract class PSeqMapped2D<T>
    : IPeriodicSequence2D<T>
{
    public IReadOnlyList2D<T> BaseSequence { get; set; }

    protected abstract T MappingFunction(T input);

    public int Count1
        => BaseSequence.Count1;

    public int Count2
        => BaseSequence.Count2;

    public int Count
        => BaseSequence.Count;

    public T this[int index]
        => MappingFunction(BaseSequence[index]);

    public T this[int index1, int index2]
        => MappingFunction(
            BaseSequence[index1, index2]
        );

    public bool IsBasic
        => false;

    public bool IsOperator
        => true;


    protected PSeqMapped2D()
    {
    }

    protected PSeqMapped2D(IReadOnlyList2D<T> baseSequence)
    {
        BaseSequence = baseSequence;
    }


    public PSeqSlice1D<T> GetSliceAt(int dimension, int index)
    {
        return new PSeqSlice1D<T>(
            this,
            dimension,
            index
        );
    }

    public IEnumerator<T> GetEnumerator()
    {
        return BaseSequence.Select(MappingFunction).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public abstract class PSeqMapped2D<TU, T>
    : IPeriodicSequence2D<T>
{
    public IReadOnlyList2D<TU> BaseSequence { get; set; }

    protected abstract T MappingFunction(TU input);

    public int Count1
        => BaseSequence.Count1;

    public int Count2
        => BaseSequence.Count2;

    public int Count
        => BaseSequence.Count;

    public T this[int index]
        => MappingFunction(BaseSequence[index]);

    public T this[int index1, int index2]
        => MappingFunction(
            BaseSequence[index1, index2]
        );

    public bool IsBasic
        => false;

    public bool IsOperator
        => true;


    protected PSeqMapped2D()
    {
    }

    protected PSeqMapped2D(IReadOnlyList2D<TU> baseSequence)
    {
        BaseSequence = baseSequence;
    }


    public PSeqSlice1D<T> GetSliceAt(int dimension, int index)
    {
        return new PSeqSlice1D<T>(
            this,
            dimension,
            index
        );
    }

    public IEnumerator<T> GetEnumerator()
    {
        return BaseSequence.Select(MappingFunction).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}