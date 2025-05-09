using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Permutations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic2D;

public class PSeqPartial2D<T>
    : IPeriodicSequence2D<T>
{
    public IPeriodicSequence2D<T> BaseSequence { get; }

    public IndexMapRange1D BaseIndexRange1 { get; }

    public IndexMapRange1D BaseIndexRange2 { get; }

    public int Count1 
        => BaseIndexRange1.Count;

    public int Count2
        => BaseIndexRange2.Count;

    public int Count
        => BaseIndexRange1.Count * BaseIndexRange2.Count;

    public T this[int index]
    {
        get
        {
            var indexPair = this.GetItemIndexPair(index);

            var baseIndex1 = BaseIndexRange1[indexPair.Item1];
            var baseIndex2 = BaseIndexRange2[indexPair.Item2];

            return BaseSequence[baseIndex1, baseIndex2];
        }
    }

    public T this[int index1, int index2]
    {
        get
        {
            var baseIndex1 = BaseIndexRange1[index1.Mod(Count1)];
            var baseIndex2 = BaseIndexRange2[index2.Mod(Count2)];

            return BaseSequence[baseIndex1, baseIndex2];
        }
    }

    public bool IsBasic 
        => false;

    public bool IsOperator 
        => true;


    public PSeqPartial2D(IPeriodicSequence2D<T> baseSequence)
    {
        BaseSequence = baseSequence;
        BaseIndexRange1 = new IndexMapRange1D(baseSequence.Count1);
        BaseIndexRange2 = new IndexMapRange1D(baseSequence.Count2);
    }

    public PSeqPartial2D(IPeriodicSequence2D<T> baseSequence, IndexMapRange1D baseIndexRange1, IndexMapRange1D baseIndexRange2)
    {
        BaseSequence = baseSequence;
        BaseIndexRange1 = baseIndexRange1;
        BaseIndexRange2 = baseIndexRange2;
    }


    public virtual PSeqSlice1D<T> GetSliceAt(int dimension, int index)
    {
        return new PSeqSlice1D<T>(this, dimension, index);
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var baseIndex2 in BaseIndexRange2)
        foreach (var baseIndex1 in BaseIndexRange1)
            yield return BaseSequence[baseIndex1, baseIndex2];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}