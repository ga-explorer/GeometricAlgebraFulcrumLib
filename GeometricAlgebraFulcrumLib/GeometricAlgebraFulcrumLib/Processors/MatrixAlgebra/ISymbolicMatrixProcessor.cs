namespace GeometricAlgebraFulcrumLib.Processors.MatrixAlgebra;

/// <summary>
/// This processor class provides basic operations on matrices.
/// The matrix is represented in a single object of generic type TMatrix.
/// A scalar element of the matrix is represented using the type T.
/// This is typical when the scalars and matrices are represented using
/// a single symbolic expression tree type.
/// </summary>
/// <typeparam name="T">The type of matrix scalars</typeparam>
public interface ISymbolicMatrixProcessor<T> :
    IMatrixProcessor<T, T>
{

}