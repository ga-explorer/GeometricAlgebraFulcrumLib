namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;

public interface IReadOnlyList4D<out T> 
    : IReadOnlyList<T>, IReadOnlyCollection4D<T>
{
    T this[int index1, int index2, int index3, int index4] { get; }
}