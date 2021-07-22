using System;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public interface IGaKVectorStorageComposer<T> 
        : IGaMultivectorStorageComposer<T>
    {
        IGasKVector<T> GetKVectorStorage();

        IGasKVector<T> GetKVectorStorageCopy();

        IGasKVector<T> GetKVectorStorageCopy(Func<T, T> scalarMapping);
    }
}