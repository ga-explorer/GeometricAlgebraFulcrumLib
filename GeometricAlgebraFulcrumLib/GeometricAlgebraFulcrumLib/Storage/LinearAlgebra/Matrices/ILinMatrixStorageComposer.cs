using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices
{
    public interface ILinMatrixStorageComposer<T>
    {
        IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        bool IsEmpty();

        ILinMatrixStorage<T> CreateLinMatrixStorage();

        //ILinMatrixDenseStorage<T> CreateLinMatrixDenseStorage();

        //ILinMatrixGradedStorage<T> CreateLinMatrixGradedStorage();
    }
}