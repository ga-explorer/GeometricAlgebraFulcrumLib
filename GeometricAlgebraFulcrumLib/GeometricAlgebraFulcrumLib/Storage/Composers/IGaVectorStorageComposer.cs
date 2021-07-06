using System;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public interface IGaVectorStorageComposer<TScalar>
        : IGaKVectorStorageComposer<TScalar>
    {
        IGaVectorStorage<TScalar> GetVectorStorage();

        IGaVectorStorage<TScalar> GetVectorStorageCopy();

        IGaVectorStorage<TScalar> GetVectorStorageCopy(Func<TScalar, TScalar> scalarMapping);
    }
}