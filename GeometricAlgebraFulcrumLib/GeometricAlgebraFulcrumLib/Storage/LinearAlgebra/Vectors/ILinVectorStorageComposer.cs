using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors
{
    public interface ILinVectorStorageComposer<T>
    {
        IScalarProcessor<T> ScalarProcessor { get; }

        bool IsEmpty();

        IEnumerable<RGaKvIndexScalarRecord<T>> GetIndexScalarRecords();

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