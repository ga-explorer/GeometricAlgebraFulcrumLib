namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    public interface IBivectorStorageContainer<T> :
        IKVectorStorageContainer<T>
    {
        BivectorStorage<T> GetBivectorStorage();
    }
}