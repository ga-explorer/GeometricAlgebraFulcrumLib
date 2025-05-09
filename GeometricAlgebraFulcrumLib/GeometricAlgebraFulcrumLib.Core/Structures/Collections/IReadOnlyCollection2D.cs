namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections;

public interface IReadOnlyCollection2D<out T> 
    : IReadOnlyCollection<T>
{
    int Count1 { get; }

    int Count2 { get; }
}