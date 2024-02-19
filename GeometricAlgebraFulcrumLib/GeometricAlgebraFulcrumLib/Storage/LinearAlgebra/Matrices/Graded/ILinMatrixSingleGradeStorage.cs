namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;

public interface ILinMatrixSingleGradeStorage<T> :
    ILinMatrixGradedStorage<T>
{
    uint Grade { get; }
        
    ILinMatrixStorage<T> MatrixStorage { get; }
}