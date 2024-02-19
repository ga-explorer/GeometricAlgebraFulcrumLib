namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;

public interface IMatrixStorageContainer<T>
{
    ILinMatrixStorage<T> GetLinMatrixStorage();
}