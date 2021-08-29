using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms
{
    public interface IGaOutermorphism<T> 
        : IGaUnilinearMap<T>
    {
        /// <summary>
        /// det(T) = T[I] lcp BladeInverse(I), where I is the space pseudo-scalar
        /// </summary>
        /// <returns></returns>
        IGaKVectorStorage<T> MappedPseudoScalar { get; }

        IReadOnlyList<IGaVectorStorage<T>> GetMappedBasisVectors();

        IGaVectorStorage<T> MapBasisVector(ulong index);

        IGaBivectorStorage<T> MapBasisBivector(ulong index1, ulong index2);

        IGaKVectorStorage<T> MapBasisBlade(ulong id);

        IGaKVectorStorage<T> MapBasisBlade(uint grade, ulong index);

        IGaVectorStorage<T> MapVector(IGaVectorStorage<T> vector);

        IGaBivectorStorage<T> MapBivector(IGaBivectorStorage<T> bivector);

        IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> kVector);
    }
}