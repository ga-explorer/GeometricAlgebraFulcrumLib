using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Permutations;

namespace DataStructuresLib.Sequences.Periodic1D;

public class PSeqPermuted1D<T>
    : IPeriodicSequence1D<T>
{
    public IPeriodicSequence1D<T> BaseSequence { get; }

    public IndexMapPermutation1D Permutation { get; }

    public int Count 
        => BaseSequence.Count;

    public T this[int index] 
        => BaseSequence[Permutation[index]];

    public bool IsBasic 
        => false;

    public bool IsOperator 
        => true;


    public PSeqPermuted1D(IPeriodicSequence1D<T> baseSequence, IndexMapPermutation1D permutation)
    {
        if (baseSequence.Count != permutation.Count)
            throw new InvalidOperationException();

        BaseSequence = baseSequence;
        Permutation = permutation;
    }


    public IEnumerator<T> GetEnumerator()
    {
        return 
            Permutation[Enumerable.Range(0, Count)]
                .Select(i => BaseSequence[i])
                .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return 
            Permutation[Enumerable.Range(0, Count)]
                .Select(i => BaseSequence[i])
                .GetEnumerator();
    }
}