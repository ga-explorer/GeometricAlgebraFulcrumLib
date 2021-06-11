using System.Collections.Generic;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Outermorphisms
{
    public interface IGaOutermorphism<T>
    {
        int DomainVSpaceDimension { get; }

        ulong DomainGaSpaceDimension { get; }

        IGaScalarProcessor<T> ScalarProcessor { get; }

        IReadOnlyList<IGaVectorStorage<T>> GetMappedBasisVectors();

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