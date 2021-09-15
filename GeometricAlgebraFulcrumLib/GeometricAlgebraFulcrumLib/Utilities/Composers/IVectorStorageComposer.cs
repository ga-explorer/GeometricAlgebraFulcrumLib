using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public interface IVectorStorageComposer<T>
    {
        IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        bool IsEmpty();

        ILinVectorStorage<T> CreateLinVectorStorage();

        //ILinVectorDenseStorage<T> CreateLinVectorDenseStorage();

        ILinVectorGradedStorage<T> CreateLinVectorGradedStorage();

        VectorStorage<T> CreateVectorStorage();

        BivectorStorage<T> CreateBivectorStorage();

        KVectorStorage<T> CreateKVectorStorage(uint grade);

        IMultivectorStorage<T> CreateMultivectorStorage();

        MultivectorStorage<T> CreateMultivectorSparseStorage();

        MultivectorGradedStorage<T> CreateMultivectorGradedStorage();
    }
}