using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices
{
    public interface ILaMatrixSingleGradeStorage<T> :
        ILaMatrixGradedStorage<T>
    {
        uint Grade { get; }
        
        ILaMatrixEvenStorage<T> EvenStorage { get; }
    }
}