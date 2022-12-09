using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms
{
    public interface IGaOutermorphism<T> : 
        IGaUnilinearMap<T>
    {
        IGaOutermorphism<T> GetOmAdjoint();


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

        ILinMatrixStorage<T> GetVectorOmMappingMatrixStorage();

        ILinMatrixStorage<T> GetBivectorOmMappingMatrixStorage();

        ILinMatrixStorage<T> GetKVectorOmMappingMatrixStorage(uint grade);

        ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrixStorage();

        
        LinMatrix<T> GetVectorOmMappingMatrix();

        LinMatrix<T> GetBivectorOmMappingMatrix();

        LinMatrix<T> GetKVectorOmMappingMatrix(uint grade);
        

        IEnumerable<IndexVectorRecord<T>> GetOmMappedBasisVectors();

        //IEnumerable<IndexBivectorStorageRecord<T>> GetOmMappedBasisBivectors();

        //IEnumerable<IndexKVectorStorageRecord<T>> GetOmMappedBasisKVectors(uint grade);
    }
}