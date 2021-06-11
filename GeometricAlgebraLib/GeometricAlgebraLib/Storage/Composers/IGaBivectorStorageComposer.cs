using System;

namespace GeometricAlgebraLib.Storage.Composers
{
    public interface IGaBivectorStorageComposer<TScalar>
        : IGaKVectorStorageComposer<TScalar>
    {
        IGaBivectorStorage<TScalar> GetBivectorStorage();

        IGaBivectorStorage<TScalar> GetBivectorStorageCopy();
        
        IGaBivectorStorage<TScalar> GetBivectorStorageCopy(Func<TScalar, TScalar> scalarMapping);
    }
}