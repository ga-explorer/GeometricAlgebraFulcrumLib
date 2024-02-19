namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;

public interface ILinVectorMutableDenseStorage<T> :
    ILinVectorDenseStorage<T>
{
    public T this[int index] { get; set; }

    public T this[ulong index] { get; set; }
}