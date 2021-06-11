using System.Collections.Generic;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Permutations
{
    public interface IIntegerMap1Dto2D
    {
        Pair<int> this[int input] { get; }

        IEnumerable<Pair<int>> this[IEnumerable<int> inputsList] { get; }
    }
}