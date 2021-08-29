using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public interface ILaVectorStorageComposer<T>
    {
        IScalarProcessor<T> ScalarProcessor { get; }

        bool IsEmpty();

        ILaVectorGradedStorage<T> CreateLaVectorGradedStorage();

        ILaVectorEvenStorage<T> CreateLaVectorEvenStorage();

        GaVectorStorage<T> CreateGaVectorStorage();

        GaBivectorStorage<T> CreateGaBivectorStorage();

        IGaKVectorStorage<T> CreateGaKVectorStorage(uint grade);

        IGaMultivectorStorage<T> CreateGaMultivectorStorage();

        GaMultivectorSparseStorage<T> CreateGaMultivectorSparseStorage();

        GaMultivectorGradedStorage<T> CreateGaMultivectorGradedStorage();
    }
}