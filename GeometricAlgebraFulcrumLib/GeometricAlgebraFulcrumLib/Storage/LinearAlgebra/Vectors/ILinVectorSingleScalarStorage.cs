namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

public interface ILinVectorSingleScalarStorage<T> :
    ILinVectorStorage<T>
{
    public ulong Index { get; }

    public T Scalar { get; set; }
}