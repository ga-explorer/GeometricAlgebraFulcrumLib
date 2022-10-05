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


        GaVector<T> OmMapBasisVector(ulong index);

        GaBivector<T> OmMapBasisBivector(ulong index);

        GaBivector<T> OmMapBasisBivector(ulong index1, ulong index2);

        GaKVector<T> OmMapBasisBlade(ulong id);

        GaKVector<T> OmMapBasisBlade(uint grade, ulong index);

        GaVector<T> OmMap(GaVector<T> vector);

        GaBivector<T> OmMap(GaBivector<T> bivector);

        GaKVector<T> OmMap(GaKVector<T> kVector);

        GaMultivector<T> OmMap(GaMultivector<T> multivector);


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