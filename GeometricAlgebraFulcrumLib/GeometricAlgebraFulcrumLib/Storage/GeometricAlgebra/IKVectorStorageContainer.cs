namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    public interface IKVectorStorageContainer<T> :
        IMultivectorStorageContainer<T>
    {
        KVectorStorage<T> GetKVectorStorage();
    }
}