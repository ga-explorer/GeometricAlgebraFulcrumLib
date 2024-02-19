using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Permutations;
using DataStructuresLib.Sequences.Periodic2D;

namespace DataStructuresLib.Sequences.Periodic1D;

/// <summary>
/// This class is a periodic sequence created as a slice from a base
/// 2D periodic sequence
/// </summary>
/// <typeparam name="T"></typeparam>
public class PSeq2DSubset1D<T>
    : IPeriodicSequence1D<T>
{
    public IPeriodicSequence2D<T> BaseSequence { get; }

    public IIndexMap1DTo2D IndexMapping { get; }

    public int Count
        => IndexMapping.IndexCount;

    public T this[int index] 
    {
        get
        {
            var indexPair = IndexMapping[index];

            return BaseSequence[indexPair.Item1, indexPair.Item2];
        }
    }

    public bool IsBasic
        => true;

    public bool IsOperator
        => false;


    public PSeq2DSubset1D(IPeriodicSequence2D<T> baseSequence, IIndexMap1DTo2D indexMapping)
    {
        BaseSequence = baseSequence;
        IndexMapping = indexMapping;
    }


    public IEnumerator<T> GetEnumerator()
    {
        return Enumerable
            .Range(0, BaseSequence.Count1)
            .Select(i => this[i])
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}