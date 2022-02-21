using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms
{
    public interface IOutermorphism<T> : 
        IUnilinearMap<T>
    {
        IOutermorphism<T> GetOmAdjoint();


        Vector<T> OmMapBasisVector(ulong index);

        Bivector<T> OmMapBasisBivector(ulong index);

        Bivector<T> OmMapBasisBivector(ulong index1, ulong index2);

        KVector<T> OmMapBasisBlade(ulong id);

        KVector<T> OmMapBasisBlade(uint grade, ulong index);

        Vector<T> OmMap(Vector<T> vector);

        Bivector<T> OmMap(Bivector<T> bivector);

        KVector<T> OmMap(KVector<T> kVector);

        Multivector<T> OmMap(Multivector<T> multivector);


        //IndexPairRecord GetKVectorOmMappingMatrixSize(uint grade);

        //IEnumerable<GradeIndexPairRecord> GetKVectorOmMappingMatrixSizes();

        ILinMatrixStorage<T> GetVectorOmMappingMatrix();

        ILinMatrixStorage<T> GetBivectorOmMappingMatrix();

        ILinMatrixStorage<T> GetKVectorOmMappingMatrix(uint grade);

        ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrix();

        IEnumerable<IndexVectorRecord<T>> GetOmMappedBasisVectors();

        //IEnumerable<IndexBivectorStorageRecord<T>> GetOmMappedBasisBivectors();

        //IEnumerable<IndexKVectorStorageRecord<T>> GetOmMappedBasisKVectors(uint grade);
    }
}