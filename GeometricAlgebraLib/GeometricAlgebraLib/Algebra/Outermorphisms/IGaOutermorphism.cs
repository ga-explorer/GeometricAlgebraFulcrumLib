using System.Collections.Generic;
using GeometricAlgebraLib.Processing.Multivectors;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Algebra.Outermorphisms
{
    public interface IGaOutermorphism<T>
    {
        int DomainVSpaceDimension { get; }

        ulong DomainGaSpaceDimension { get; }

        IGaMultivectorProcessor<T> MultivectorProcessor { get; }

        IGaScalarProcessor<T> ScalarProcessor { get; }

        IReadOnlyList<IGaVectorStorage<T>> GetMappedBasisVectors();

        /// <summary>
        /// det(T) = T[I] lcp BladeInverse(I), where I is the space pseudo-scalar
        /// </summary>
        /// <returns></returns>
        T GetDeterminant();

        IGaVectorStorage<T> MapBasisVector(int index);

        IGaVectorStorage<T> MapBasisVector(ulong index);

        IGaKVectorStorage<T> MapBasisBlade(ulong id);

        IGaKVectorStorage<T> MapBasisBlade(int grade, ulong index);

        IGaVectorStorage<T> MapVector(IGaVectorStorage<T> vector);

        IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> kVector);

        IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> mv);
    }
}