using System;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Processing.Matrices
{
    /// <summary>
    /// This processor class provides basic operations on matrices.
    /// The matrix is represented in a single object of generic type TMatrix.
    /// A scalar element of the matrix is represented using the type T.
    /// </summary>
    /// <typeparam name="TMatrix">The type of matrix objects</typeparam>
    /// <typeparam name="T">The type of matrix scalars</typeparam>
    public interface IGaMatrixProcessor<TMatrix, T>
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        int GetRowsCount(TMatrix matrix);

        int GetColumnsCount(TMatrix matrix);

        Tuple<int, int> GetSize(TMatrix matrix);

        T[,] GetArray(TMatrix matrix);

        TMatrix CreateMatrix(T[,] array);

        TMatrix CreateRowVectorMatrix(T[] array);

        TMatrix CreateRowVectorMatrix(T[,] array, int rowIndex);

        TMatrix CreateColumnVectorMatrix(T[] array);

        TMatrix CreateColumnVectorMatrix(T[,] array, int columnIndex);

        TMatrix CreateZeroMatrix(int size);

        TMatrix CreateZeroMatrix(int rowsCount, int columnsCount);

        TMatrix CreateUnityMatrix(int size);

        TMatrix AddMatrixScalar(TMatrix matrix, T scalar);

        TMatrix AddScalarMatrix(T scalar, TMatrix matrix);

        TMatrix AddMatrices(TMatrix matrix1, TMatrix matrix2);

        TMatrix SubtractMatrixScalar(TMatrix matrix, T scalar);

        TMatrix SubtractScalarMatrix(T scalar, TMatrix matrix);

        TMatrix SubtractMatrices(TMatrix matrix1, TMatrix matrix2);

        TMatrix ScalarMatrixProduct(T scalar, TMatrix matrix);

        TMatrix MatrixScalarProduct(TMatrix matrix, T scalar);

        TMatrix TimesScalars(TMatrix matrix1, TMatrix matrix2);

        TMatrix DivideScalars(TMatrix matrix1, TMatrix matrix2);

        TMatrix DivideMatrixScalar(TMatrix matrix, T scalar);

        TMatrix DivideScalarMatrix(T scalar, TMatrix matrix);

        TMatrix MatrixProduct(TMatrix matrix1, TMatrix matrix2);

        TMatrix MapScalars(TMatrix matrix1, TMatrix matrix2, Func<T, T, T> mappingFunc);

        TMatrix MapScalars(TMatrix matrix, Func<T, T> mappingFunc);

        TMatrix Negative(TMatrix matrix);

        TMatrix Adjoint(TMatrix matrix);

        TMatrix Inverse(TMatrix matrix);

        TMatrix InverseAdjoint(TMatrix matrix);

        int EigenDecomposition(TMatrix matrix, out Tuple<T, T[]>[] realPairs, out Tuple<T, T[]>[] imagPairs);

        Tuple<T, T>[] EigenValues(TMatrix matrix);

        Tuple<T[], T[]>[] EigenVectors(TMatrix matrix);
    }
}