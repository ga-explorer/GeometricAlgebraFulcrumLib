using System.Collections.Generic;

namespace DataStructuresLib.Permutations;

public interface IIntegerMap1Dto1D
{
    int this[int input] { get; }

    IEnumerable<int> this[IEnumerable<int> inputsList] { get; }
}