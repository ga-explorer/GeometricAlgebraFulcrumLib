using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices
{
    public interface ILinMatrixStorageComposer<T>
    {
        IScalarProcessor<T> ScalarProcessor { get; }

        bool IsEmpty();

        ILinMatrixStorage<T> CreateLinMatrixStorage();

        //ILinMatrixDenseStorage<T> CreateLinMatrixDenseStorage();

        //ILinMatrixGradedStorage<T> CreateLinMatrixGradedStorage();
    }
}