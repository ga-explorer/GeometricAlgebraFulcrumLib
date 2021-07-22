using System;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public interface IGaVectorStorageComposer<T>
        : IGaKVectorStorageComposer<T>
    {
        IGasVector<T> GetVectorStorage();

        IGasVector<T> GetVectorStorageCopy();

        IGasVector<T> GetVectorStorageCopy(Func<T, T> scalarMapping);
    }
}