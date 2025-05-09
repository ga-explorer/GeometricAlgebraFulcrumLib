namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections;

public interface IReadOnlyCollection4D<out T> : IReadOnlyCollection<T>
{
    int Count1 { get; }

    int Count2 { get; }

    int Count3 { get; }

    int Count4 { get; }
}