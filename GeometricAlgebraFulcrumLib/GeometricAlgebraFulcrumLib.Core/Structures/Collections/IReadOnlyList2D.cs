namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections;

public interface IReadOnlyList2D<out T> : 
    IReadOnlyList<T>, 
    IReadOnlyCollection2D<T>
{
    T this[int index1, int index2] { get; }
}