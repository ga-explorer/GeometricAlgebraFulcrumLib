using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms
{
    public interface IOutermorphism<T> : 
        IUnilinearMap<T>
    {
        IOutermorphism<T> GetOmAdjoint();


        VectorStorage<T> OmMapBasisVector(ulong index);

        BivectorStorage<T> OmMapBasisBivector(ulong index);

        BivectorStorage<T> OmMapBasisBivector(ulong index1, ulong index2);

        KVectorStorage<T> OmMapBasisBlade(ulong id);

        KVectorStorage<T> OmMapBasisBlade(uint grade, ulong index);

        VectorStorage<T> OmMapVector(VectorStorage<T> vector);

        BivectorStorage<T> OmMapBivector(BivectorStorage<T> bivector);

        KVectorStorage<T> OmMapKVector(KVectorStorage<T> kVector);

        MultivectorStorage<T> OmMapMultivector(MultivectorStorage<T> multivector);

        MultivectorGradedStorage<T> OmMapMultivector(MultivectorGradedStorage<T> multivector);

        
        //IndexPairRecord GetKVectorOmMappingMatrixSize(uint grade);

        //IEnumerable<GradeIndexPairRecord> GetKVectorOmMappingMatrixSizes();

        ILinMatrixStorage<T> GetVectorOmMappingMatrix();

        ILinMatrixStorage<T> GetBivectorOmMappingMatrix();

        ILinMatrixStorage<T> GetKVectorOmMappingMatrix(uint grade);

        ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrix();

        IEnumerable<IndexVectorStorageRecord<T>> GetOmMappedBasisVectors();

        //IEnumerable<IndexBivectorStorageRecord<T>> GetOmMappedBasisBivectors();

        //IEnumerable<IndexKVectorStorageRecord<T>> GetOmMappedBasisKVectors(uint grade);
    }
}