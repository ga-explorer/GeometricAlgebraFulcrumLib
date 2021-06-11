using System;

namespace GeometricAlgebraLib.Storage.Composers
{
    public interface IGaKVectorStorageComposer<TScalar> 
        : IGaMultivectorStorageComposer<TScalar>
    {
        IGaKVectorStorage<TScalar> GetKVectorStorage();

        IGaKVectorStorage<TScalar> GetKVectorStorageCopy();

        IGaKVectorStorage<TScalar> GetKVectorStorageCopy(Func<TScalar, TScalar> scalarMapping);
    }
}