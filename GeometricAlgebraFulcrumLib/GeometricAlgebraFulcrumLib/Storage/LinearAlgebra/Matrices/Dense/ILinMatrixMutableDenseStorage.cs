namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;

public interface ILinMatrixMutableDenseStorage<T> :
    ILinMatrixDenseStorage<T>
{
    public T this[int index1, int index2] { get; set; }

    public T this[ulong index1, ulong index2] { get; set; }
}