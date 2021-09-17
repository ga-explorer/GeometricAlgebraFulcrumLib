namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    public interface IMultivectorStorageContainer<T>
    {
        IMultivectorStorage<T> GetMultivectorStorage();
    }
}