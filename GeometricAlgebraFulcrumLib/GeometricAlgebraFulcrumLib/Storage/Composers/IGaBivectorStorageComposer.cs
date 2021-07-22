using System;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public interface IGaBivectorStorageComposer<T>
        : IGaKVectorStorageComposer<T>
    {
        IGasBivector<T> GetBivectorStorage();

        IGasBivector<T> GetBivectorStorageCopy();
        
        IGasBivector<T> GetBivectorStorageCopy(Func<T, T> scalarMapping);
    }
}