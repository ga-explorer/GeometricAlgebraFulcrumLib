namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    public interface IVectorStorageContainer<T> :
        IKVectorStorageContainer<T>
    {
        VectorStorage<T> GetVectorStorage();
    }
}