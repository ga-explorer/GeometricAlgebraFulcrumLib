using System;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;

namespace GeometricAlgebraFulcrumLib.Processing.Matrices
{
    public interface ILaProcessor<T>
        : IScalarProcessor<T>
    {
        IScalarProcessor<T> ScalarProcessor { get; }
    }

    /// <summary>
    /// This processor class provides basic operations on matrices.
    /// The matrix is represented in a single object of generic type TMatrix.
    /// A scalar element of the matrix is represented using the type TScalar.
    /// </summary>
    /// <typeparam name="TMatrix">The type of matrix objects</typeparam>
    /// <typeparam name="TScalar">The type of matrix scalars</typeparam>
    public interface ILaProcessor<TMatrix, TScalar>
        : IScalarProcessor<TScalar>
    {
        TMatrix CreateZeroMatrix(int size);

        TMatrix CreateZeroMatrix(int rowsCount, int columnsCount);

        TMatrix CreateUnityMatrix(int size);

        TMatrix CreateMatrix(ILaMatrixEvenStorage<TScalar> array);

        TMatrix CreateRowVectorMatrix(ILaVectorEvenStorage<TScalar> array);

        TMatrix CreateRowVectorMatrix(ILaMatrixEvenStorage<TScalar> array, int rowIndex);

        TMatrix CreateColumnVectorMatrix(ILaVectorEvenStorage<TScalar> array);

        TMatrix CreateColumnVectorMatrix(ILaMatrixEvenStorage<TScalar> array, int columnIndex);


        int GetDenseRowsCount(TMatrix matrix);

        int GetDenseColumnsCount(TMatrix matrix);

        Pair<int> GetDenseSize(TMatrix matrix);


        TScalar GetScalar(TMatrix matrix, int rowIndex, int colIndex);

        ILaVectorEvenStorage<TScalar> MatrixRowToVector(TMatrix matrix, int rowIndex);

        ILaVectorEvenStorage<TScalar> MatrixColumnToVector(TMatrix matrix, int colIndex);

        ILaMatrixEvenStorage<TScalar> MatrixToArray(TMatrix matrix);


        TMatrix AddMatrixScalar(TMatrix matrix, TScalar scalar);

        TMatrix AddScalarMatrix(TScalar scalar, TMatrix matrix);

        TMatrix AddMatrices(TMatrix matrix1, TMatrix matrix2);


        TMatrix SubtractMatrixScalar(TMatrix matrix, TScalar scalar);

        TMatrix SubtractScalarMatrix(TScalar scalar, TMatrix matrix);

        TMatrix SubtractMatrices(TMatrix matrix1, TMatrix matrix2);


        TMatrix TimesMatrixScalar(TMatrix matrix, TScalar scalar);

        TMatrix TimesScalarMatrix(TScalar scalar, TMatrix matrix);

        TMatrix TimesMatrixItems(TMatrix matrix1, TMatrix matrix2);

        TMatrix TimesMatrices(TMatrix matrix1, TMatrix matrix2);


        TMatrix DivideMatrixItems(TMatrix matrix1, TMatrix matrix2);

        TMatrix DivideMatrixScalar(TMatrix matrix, TScalar scalar);

        TMatrix DivideScalarMatrix(TScalar scalar, TMatrix matrix);


        TMatrix MapMatrixItems(TMatrix matrix, Func<TScalar, TScalar> mappingFunc);

        TMatrix MapMatrixItems(TMatrix matrix, Func<int, int, TScalar, TScalar> mappingFunc);

        TMatrix MapMatrixItems(TMatrix matrix1, TMatrix matrix2, Func<TScalar, TScalar, TScalar> mappingFunc);


        TMatrix MatrixNegative(TMatrix matrix);

        TMatrix MatrixTranspose(TMatrix matrix);

        TMatrix MatrixInverse(TMatrix matrix);

        TMatrix MatrixInverseTranspose(TMatrix matrix);

        int MatrixEigenDecomposition(TMatrix matrix, out Tuple<TScalar, ILaVectorEvenStorage<TScalar>>[] realPairs, out Tuple<TScalar, ILaVectorEvenStorage<TScalar>>[] imagPairs);

        Pair<TScalar>[] MatrixEigenValues(TMatrix matrix);

        Pair<ILaVectorEvenStorage<TScalar>>[] MatrixEigenVectors(TMatrix matrix);
    }
}