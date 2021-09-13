using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public interface IMatrixStorageComposer<T>
    {
        IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        bool IsEmpty();

        ILinMatrixStorage<T> CreateLinMatrixStorage();

        //ILinMatrixDenseStorage<T> CreateLinMatrixDenseStorage();

        //ILinMatrixGradedStorage<T> CreateLinMatrixGradedStorage();
    }
}