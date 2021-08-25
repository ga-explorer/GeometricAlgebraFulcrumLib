using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Storage.Outermorphisms
{
    public interface IGaStorageOutermorphism
    {

    }

    public interface IGaStorageOutermorphism<T> :
        IGaStorageOutermorphism
    {
        IReadOnlyList<IGaStorageVector<T>> GetMappedBasisVectors();

        IGaStorageVector<T> MapBasisVector(ulong index);

        IGaStorageBivector<T> MapBasisBivector(ulong index1, ulong index2);

        IGaStorageKVector<T> MapBasisBlade(ulong id);

        IGaStorageKVector<T> MapBasisBlade(uint grade, ulong index);

        IGaStorageVector<T> MapVector(IGaStorageVector<T> vector);

        IGaStorageBivector<T> MapBivector(IGaStorageBivector<T> bivector);

        IGaStorageKVector<T> MapKVector(IGaStorageKVector<T> kVector);
    }
}