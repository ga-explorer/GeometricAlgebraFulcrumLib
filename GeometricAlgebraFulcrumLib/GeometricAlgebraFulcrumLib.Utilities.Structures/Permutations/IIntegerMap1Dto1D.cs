namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Permutations;

public interface IIntegerMap1Dto1D
{
    int this[int input] { get; }

    IEnumerable<int> this[IEnumerable<int> inputsList] { get; }
}