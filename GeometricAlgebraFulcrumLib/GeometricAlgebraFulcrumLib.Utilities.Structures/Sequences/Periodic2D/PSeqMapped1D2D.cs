using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic2D;

public abstract class PSeqMapped1D2D<T>
    : IPeriodicSequence2D<T>
{
    public IReadOnlyList<T> BaseSequence1 { get; set; }

    public IReadOnlyList<T> BaseSequence2 { get; set; }

    protected abstract T MappingFunction(T input1, T input2);

    public int Count1
        => BaseSequence1.Count;

    public int Count2
        => BaseSequence2.Count;

    public int Count
        => BaseSequence1.Count * BaseSequence2.Count;

    public T this[int index]
    {
        get
        {
            var indexPair = this.GetItemIndexPair(index);

            return MappingFunction(
                BaseSequence1[indexPair.Item1],
                BaseSequence2[indexPair.Item2]
            );
        }
    }

    public T this[int index1, int index2]
        => MappingFunction(
            BaseSequence1[index1], 
            BaseSequence2[index2]
        );

    public bool IsBasic
        => true;

    public bool IsOperator
        => false;


    protected PSeqMapped1D2D()
    {
    }

    protected PSeqMapped1D2D(IReadOnlyList<T> baseSequence1, IReadOnlyList<T> baseSequence2)
    {
        BaseSequence1 = baseSequence1;
        BaseSequence2 = baseSequence2;
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
        foreach (var item1 in BaseSequence1)
        foreach (var item2 in BaseSequence2)
            yield return MappingFunction(item1, item2);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public abstract class PSeqMapped1D2D<TU, T>
    : IPeriodicSequence2D<T>
{
    public IReadOnlyList<TU> BaseSequence1 { get; set; }

    public IReadOnlyList<TU> BaseSequence2 { get; set; }

    protected abstract T MappingFunction(TU input1, TU input2);

    public int Count1
        => BaseSequence1.Count;

    public int Count2
        => BaseSequence2.Count;

    public int Count
        => BaseSequence1.Count * BaseSequence2.Count;

    public T this[int index]
    {
        get
        {
            var indexPair = this.GetItemIndexPair(index);

            return MappingFunction(
                BaseSequence1[indexPair.Item1],
                BaseSequence2[indexPair.Item2]
            );
        }
    }

    public T this[int index1, int index2]
        => MappingFunction(
            BaseSequence1[index1], 
            BaseSequence2[index2]
        );

    public bool IsBasic
        => true;

    public bool IsOperator
        => false;


    protected PSeqMapped1D2D()
    {
    }

    protected PSeqMapped1D2D(IReadOnlyList<TU> baseSequence1, IReadOnlyList<TU> baseSequence2)
    {
        BaseSequence1 = baseSequence1;
        BaseSequence2 = baseSequence2;
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
        foreach (var item1 in BaseSequence1)
        foreach (var item2 in BaseSequence2)
            yield return MappingFunction(item1, item2);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
    
public abstract class PSeqMapped1D2D<TU1, TU2, T>
    : IPeriodicSequence2D<T>
{
    public IReadOnlyList<TU1> BaseSequence1 { get; set; }

    public IReadOnlyList<TU2> BaseSequence2 { get; set; }

    protected abstract T MappingFunction(TU1 input1, TU2 input2);

    public int Count1
        => BaseSequence1.Count;

    public int Count2
        => BaseSequence2.Count;

    public int Count
        => BaseSequence1.Count * BaseSequence2.Count;

    public T this[int index]
    {
        get
        {
            var indexPair = this.GetItemIndexPair(index);

            return MappingFunction(
                BaseSequence1[indexPair.Item1],
                BaseSequence2[indexPair.Item2]
            );
        }
    }

    public T this[int index1, int index2]
        => MappingFunction(
            BaseSequence1[index1], 
            BaseSequence2[index2]
        );

    public bool IsBasic
        => true;

    public bool IsOperator
        => false;


    protected PSeqMapped1D2D()
    {
    }

    protected PSeqMapped1D2D(IReadOnlyList<TU1> baseSequence1, IReadOnlyList<TU2> baseSequence2)
    {
        BaseSequence1 = baseSequence1;
        BaseSequence2 = baseSequence2;
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
        foreach (var item1 in BaseSequence1)
        foreach (var item2 in BaseSequence2)
            yield return MappingFunction(item1, item2);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}