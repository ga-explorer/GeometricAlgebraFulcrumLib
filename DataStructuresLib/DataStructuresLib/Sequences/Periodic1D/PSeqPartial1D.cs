using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using DataStructuresLib.Permutations;

namespace DataStructuresLib.Sequences.Periodic1D;

public class PSeqPartial1D<T>
    : IPeriodicSequence1D<T>
{
    public IPeriodicSequence1D<T> BaseSequence { get; }

    public IndexMapRange1D BaseIndexRange { get; }

    public int Count
        => BaseIndexRange.Count;

    public T this[int index]
    {
        get
        {
            var baseIndex = BaseIndexRange[index.Mod(Count)];

            return BaseSequence[baseIndex];
        }
    }

    public bool IsBasic
        => false;

    public bool IsOperator
        => true;


    public PSeqPartial1D(IPeriodicSequence1D<T> baseSequence, IndexMapRange1D baseIndexRange)
    {
        BaseSequence = baseSequence;
        BaseIndexRange = baseIndexRange;
    }

    public PSeqPartial1D(IPeriodicSequence1D<T> baseSequence, int firstIndex)
    {
        BaseSequence = baseSequence;
        BaseIndexRange = new IndexMapRange1D(firstIndex, baseSequence.Count, true);
    }

    public PSeqPartial1D(IPeriodicSequence1D<T> baseSequence, int firstIndex, int count)
    {
        BaseSequence = baseSequence;
        BaseIndexRange = new IndexMapRange1D(firstIndex, count, true);
    }

    public PSeqPartial1D(IPeriodicSequence1D<T> baseSequence, int firstIndex, int count, bool moveForward)
    {
        BaseSequence = baseSequence;
        BaseIndexRange = new IndexMapRange1D(firstIndex, count, moveForward);
    }


    public IEnumerator<T> GetEnumerator()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => BaseSequence[BaseIndexRange[i]])
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}