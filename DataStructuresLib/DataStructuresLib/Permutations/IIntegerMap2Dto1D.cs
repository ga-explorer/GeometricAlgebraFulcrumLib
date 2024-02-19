using System.Collections.Generic;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Permutations;

public interface IIntegerMap2Dto1D
{
    int this[Pair<int> input] { get; }

    int this[int input1, int input2] { get; }

    IEnumerable<int> this[IEnumerable<Pair<int>> inputsList] { get; }
}