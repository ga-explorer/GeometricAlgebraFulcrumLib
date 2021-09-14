using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps
{
    public interface IUnilinearMap<T> : 
        ILinearAlgebraElement<T>
    {
        bool IsValid();

        bool IsInvalid();

        IUnilinearMap<T> GetAdjoint();


        IMultivectorStorage<T> MapBasisScalar();

        IMultivectorStorage<T> MapBasisVector(ulong index);

        IMultivectorStorage<T> MapBasisBivector(ulong index);

        IMultivectorStorage<T> MapBasisBivector(ulong index1, ulong index2);

        IMultivectorStorage<T> MapBasisBlade(ulong id);

        IMultivectorStorage<T> MapBasisBlade(uint grade, ulong index);


        IMultivectorStorage<T> MapScalar(T mv);

        IMultivectorStorage<T> MapVector(VectorStorage<T> vector);

        IMultivectorStorage<T> MapBivector(BivectorStorage<T> bivector);

        IMultivectorStorage<T> MapKVector(KVectorStorage<T> kVector);

        IMultivectorStorage<T> MapMultivector(MultivectorStorage<T> multivector);

        IMultivectorStorage<T> MapMultivector(MultivectorGradedStorage<T> multivector);

        
        //IndexPairRecord GetMultivectorMappingMatrixSize();

        ILinMatrixStorage<T> GetMultivectorMappingMatrix();

        IEnumerable<IdMultivectorStorageRecord<T>> GetMappedBasisBlades();
    }
}