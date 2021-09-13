using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Outermorphisms
{
    /// <summary>
    /// An outermorphism is defined as a linear mapping that takes a basis blade into
    /// a k-vector of the same grade as the mapped basis blade.
    /// </summary>
    public interface IOutermorphismStorage
    {
        IEnumerable<ulong> GetMappedIds();

        IEnumerable<ulong> GetMappedIds(uint grade);

        IEnumerable<ulong> GetMappedIndices(uint grade);

        IEnumerable<GradeIndexRecord> GetMappedGradeIndexRecords();

        IEnumerable<GradeIndexRecord> GetMappedGradeIndexRecords(uint grade);
    }

    /// <summary>
    /// An outermorphism is defined as a linear mapping that takes a basis blade into
    /// a k-vector of the same grade as the mapped basis blade.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOutermorphismStorage<T> :
        IOutermorphismStorage
    {
        IOutermorphismStorage<T> GetTranspose();


        IEnumerable<IndexVectorStorageRecord<T>> GetMappedBasisVectors();

        IEnumerable<IndexBivectorStorageRecord<T>> GetMappedBasisBivectors();

        IEnumerable<GradeIndexKVectorStorageRecord<T>> GetMappedBasisBlades();

        IEnumerable<IndexKVectorStorageRecord<T>> GetMappedBasisBlades(uint grade);

        IEnumerable<IdKVectorStorageRecord<T>> GetMappedBasisBladesById();

        IEnumerable<IdKVectorStorageRecord<T>> GetMappedBasisBladesById(uint grade);


        VectorStorage<T> GetMappedBasisVector(ulong index);

        BivectorStorage<T> GetMappedBasisBivector(ulong index);

        BivectorStorage<T> GetMappedBasisBivector(ulong index1, ulong index2);

        KVectorStorage<T> GetMappedBasisBlade(ulong id);

        KVectorStorage<T> GetMappedBasisBlade(uint grade, ulong index);


        ILinMatrixStorage<T> GetVectorMappingMatrix();

        ILinMatrixStorage<T> GetBivectorMappingMatrix();

        ILinMatrixStorage<T> GetKVectorMappingMatrix(uint grade);

        ILinMatrixStorage<T> GetMultivectorMappingMatrix();

        ILinMatrixGradedStorage<T> GetMultivectorGradedMappingMatrix();
    }
}