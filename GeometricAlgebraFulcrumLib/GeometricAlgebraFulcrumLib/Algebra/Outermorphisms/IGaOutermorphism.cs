using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms
{
    public interface IGaOutermorphism<T> 
        : IGaUnilinearMap<T>
    {
        /// <summary>
        /// det(T) = T[I] lcp BladeInverse(I), where I is the space pseudo-scalar
        /// </summary>
        /// <returns></returns>
        IGaStorageKVector<T> MappedPseudoScalar { get; }

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