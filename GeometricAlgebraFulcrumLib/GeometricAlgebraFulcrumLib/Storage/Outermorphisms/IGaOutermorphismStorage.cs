using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Storage.Outermorphisms
{
    public interface IGaOutermorphismStorage
    {

    }

    public interface IGaOutermorphismStorage<T> :
        IGaOutermorphismStorage
    {
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