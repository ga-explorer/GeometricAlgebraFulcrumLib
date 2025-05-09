using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Permutations;

public interface IIntegerMap1Dto2D
{
    Pair<int> this[int input] { get; }

    IEnumerable<Pair<int>> this[IEnumerable<int> inputsList] { get; }
}