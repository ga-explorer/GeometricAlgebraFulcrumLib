using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using DataStructuresLib.Permutations;

namespace DataStructuresLib.Sequences.Periodic1D;

public class PSeqIndexMapped1D<T>
    : IPeriodicSequence1D<T>
{
    public IPeriodicSequence1D<T> BaseSequence { get; }

    public int Count 
        => BaseSequence.Count;

    public IIndexMap1D IndexMapping { get; }

    public T this[int index]
        => BaseSequence[IndexMapping[index.Mod(Count)]];

    public bool IsBasic
        => true;

    public bool IsOperator
        => false;


    protected PSeqIndexMapped1D(IPeriodicSequence1D<T> baseSequence, IIndexMap1D indexMapping)
    {
        BaseSequence = baseSequence;
        IndexMapping = indexMapping;
    }


    public IEnumerator<T> GetEnumerator()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => this[i])
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => this[i])
            .GetEnumerator();
    }
}