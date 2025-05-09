using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences.Periodic1D;

public class PSeqMultiplexed1D<T>
    : IPeriodicSequencesAggregate1D<T>
{
    public IReadOnlyList<IPeriodicSequence1D<T>> BaseSequences { get; }

    public int[] SequenceSelectionArray { get; }

    public int Count 
        => SequenceSelectionArray.Length;


    public T this[int index] 
    {
        get
        {
            var seqIndex = SequenceSelectionArray[index.Mod(Count)];

            return BaseSequences[seqIndex][index];
        }
    }

    public bool IsBasic 
        => false;

    public bool IsOperator 
        => true;


    public PSeqMultiplexed1D(IReadOnlyList<IPeriodicSequence1D<T>> sequencesList, int count)
    {
        BaseSequences = sequencesList;
        SequenceSelectionArray = Enumerable.Range(0, count).ToArray();
    }

    public PSeqMultiplexed1D(IReadOnlyList<IPeriodicSequence1D<T>> sequencesList, params int[] sequenceSelectionArray)
    {
        BaseSequences = sequencesList;
        SequenceSelectionArray = sequenceSelectionArray;
    }

    public PSeqMultiplexed1D(IReadOnlyList<IPeriodicSequence1D<T>> sequencesList, IEnumerable<int> sequenceSelectionList)
    {
        BaseSequences = sequencesList;
        SequenceSelectionArray = sequenceSelectionList.ToArray();
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