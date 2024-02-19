using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Sequences.Periodic1D;

public class PSeqSwitched1D<T>
    : IPeriodicSequencesAggregate1D<T>
{
    public IReadOnlyList<IPeriodicSequence1D<T>> BaseSequences { get; }

    private int _selectedSequenceIndex;
    public int SelectedSequenceIndex
    {
        get => _selectedSequenceIndex;
        set => _selectedSequenceIndex = value.Mod(BaseSequences.Count);
    }

    public int Count 
        => BaseSequences[0].Count;


    public T this[int index] 
        => BaseSequences[SelectedSequenceIndex][index];

    public bool IsBasic 
        => false;

    public bool IsOperator 
        => true;


    public PSeqSwitched1D(IReadOnlyList<IPeriodicSequence1D<T>> sequencesList)
    {
        var count = sequencesList[0].Count;
        if (sequencesList.Any(s => s.Count != count))
            throw new InvalidOperationException();

        BaseSequences = sequencesList;
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