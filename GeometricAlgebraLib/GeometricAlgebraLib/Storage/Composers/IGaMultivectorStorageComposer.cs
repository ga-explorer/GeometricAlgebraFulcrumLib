using System;
using GeometricAlgebraLib.Processors.Scalars;

namespace GeometricAlgebraLib.Storage.Composers
{
    public interface IGaMultivectorStorageComposer<TScalar>
    {
        IGaScalarProcessor<TScalar> ScalarProcessor { get; }

        bool IsEmpty();

        IGaMultivectorStorage<TScalar> GetCompactStorage();

        IGaMultivectorGradedStorage<TScalar> GetCompactGradedStorage();

        IGaMultivectorStorage<TScalar> GetStorageCopy();
        
        IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping);

        GaMultivectorGradedStorage<TScalar> GetMultivectorGradedStorageCopy();

        GaMultivectorTermsStorage<TScalar> GetMultivectorTermsStorageCopy();

        GaMultivectorTreeStorage<TScalar> GetMultivectorTreeStorageCopy();
    }
}