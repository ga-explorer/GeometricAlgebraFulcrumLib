using System;
using GeometricAlgebraLib.Processors.Scalars;

namespace GeometricAlgebraLib.Processors.Matrices
{
    /// <summary>
    /// This processor class provides basic operations on matrices.
    /// The matrix is represented in a single object of generic type TMatrix.
    /// A scalar element of the matrix is represented using the type TScalar.
    /// </summary>
    /// <typeparam name="TMatrix">The type of matrix objects</typeparam>
    /// <typeparam name="TScalar">The type of matrix scalars</typeparam>
    public interface IGaMatrixProcessor<TMatrix, TScalar>
    {
        IGaScalarProcessor<TScalar> ScalarProcessor { get; }

        int GetRowsCount(TMatrix matrix);

        int GetColumnsCount(TMatrix matrix);

        Tuple<int, int> GetSize(TMatrix matrix);

        TScalar[,] GetArray(TMatrix matrix);

        TMatrix CreateMatrix(TScalar[,] array);

        TMatrix CreateZeroMatrix(int size);

        TMatrix CreateZeroMatrix(int rowsCount, int columnsCount);

        TMatrix CreateUnityMatrix(int size);

        TMatrix AddMatrixScalar(TMatrix matrix, TScalar scalar);

        TMatrix AddScalarMatrix(TScalar scalar, TMatrix matrix);

        TMatrix AddMatrices(TMatrix matrix1, TMatrix matrix2);

        TMatrix SubtractMatrixScalar(TMatrix matrix, TScalar scalar);

        TMatrix SubtractScalarMatrix(TScalar scalar, TMatrix matrix);

        TMatrix SubtractMatrices(TMatrix matrix1, TMatrix matrix2);

        TMatrix ScalarMatrixProduct(TScalar scalar, TMatrix matrix);

        TMatrix MatrixScalarProduct(TMatrix matrix, TScalar scalar);

        TMatrix TimesScalars(TMatrix matrix1, TMatrix matrix2);

        TMatrix DivideScalars(TMatrix matrix1, TMatrix matrix2);

        TMatrix DivideMatrixScalar(TMatrix matrix, TScalar scalar);

        TMatrix DivideScalarMatrix(TScalar scalar, TMatrix matrix);

        TMatrix MatrixProduct(TMatrix matrix1, TMatrix matrix2);

        TMatrix MapScalars(TMatrix matrix1, TMatrix matrix2, Func<TScalar, TScalar, TScalar> mappingFunc);

        TMatrix MapScalars(TMatrix matrix, Func<TScalar, TScalar> mappingFunc);

        TMatrix Negative(TMatrix matrix);

        TMatrix Adjoint(TMatrix matrix);

        TMatrix Inverse(TMatrix matrix);

        TMatrix InverseAdjoint(TMatrix matrix);

        int EigenDecomposition(TMatrix matrix, out Tuple<TScalar, TScalar[]>[] realPairs, out Tuple<TScalar, TScalar[]>[] imagPairs);

        Tuple<TScalar, TScalar>[] EigenValues(TMatrix matrix);

        Tuple<TScalar[], TScalar[]>[] EigenVectors(TMatrix matrix);
    }
}