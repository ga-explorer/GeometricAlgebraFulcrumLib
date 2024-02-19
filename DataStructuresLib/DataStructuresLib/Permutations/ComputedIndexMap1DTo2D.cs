using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Permutations;

public sealed class ComputedIndexMap1DTo2D 
    : IIndexMap1DTo2D
{
    public Func<int, Pair<int>> Mapping { get; set; }

    public int IndexCount { get; }

    public Pair<int> this[int input] 
        => Mapping(input);

    public IEnumerable<Pair<int>> this[IEnumerable<int> inputsList] 
        => inputsList.Select(Mapping);


    public ComputedIndexMap1DTo2D(int indexCount, Func<int, Pair<int>> mapping)
    {
        IndexCount = indexCount;
        Mapping = mapping;
    }

}