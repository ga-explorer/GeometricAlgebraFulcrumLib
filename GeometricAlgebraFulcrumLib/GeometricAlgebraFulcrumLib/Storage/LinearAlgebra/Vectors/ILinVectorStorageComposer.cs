using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors
{
    public interface ILinVectorStorageComposer<T>
    {
        IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        bool IsEmpty();

        IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords();

        ILinVectorStorage<T> CreateLinVectorStorage();

        //ILinVectorDenseStorage<T> CreateLinVectorDenseStorage();

        ILinVectorGradedStorage<T> CreateLinVectorGradedStorage();

        VectorStorage<T> CreateVectorStorage();

        BivectorStorage<T> CreateBivectorStorage();

        KVectorStorage<T> CreateKVectorStorage(uint grade);

        IMultivectorStorage<T> CreateMultivectorStorage();

        MultivectorStorage<T> CreateMultivectorStorageSparse();

        MultivectorGradedStorage<T> CreateMultivectorGradedStorage();
    }
}