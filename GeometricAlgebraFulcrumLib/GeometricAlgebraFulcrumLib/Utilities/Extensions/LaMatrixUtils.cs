using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class LaMatrixUtils
    {
        public static T[] GetArrayZero1D<T>(this IScalarProcessor<T> scalarProcessor, int count)
        {
            var exprArray = new T[count];

            for (var i = 0; i < count; i++)
                exprArray[i] = scalarProcessor.ScalarZero;

            return exprArray;
        }

        public static T[,] GetArrayZero2D<T>(this IScalarProcessor<T> scalarProcessor, int count)
        {
            var exprArray = new T[count, count];

            for (var i = 0; i < count; i++)
            for (var j = 0; j < count; j++)
                exprArray[i, j] = scalarProcessor.ScalarZero;

            return exprArray;
        }

        public static T[,] GetArrayZero2D<T>(this IScalarProcessor<T> scalarProcessor, int count1, int count2)
        {
            var exprArray = new T[count1, count2];

            for (var i = 0; i < count1; i++)
            for (var j = 0; j < count2; j++)
                exprArray[i, j] = scalarProcessor.ScalarZero;

            return exprArray;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<LaVector<T>> GetRows<T>(this LaMatrix<T> matrix)
        {
            return Enumerable
                .Range(0, matrix.RowsCount)
                .Select(rowIndex =>
                    new LaVector<T>(
                        matrix.ScalarsGridProcessor,
                        matrix.MatrixStorage.CreateLaVectorRowStorage((ulong) rowIndex)
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> GetRow<T>(this LaMatrix<T> matrix, int rowIndex)
        {
            return new LaVector<T>(
                matrix.ScalarsGridProcessor,
                matrix.MatrixStorage.CreateLaVectorRowStorage((ulong) rowIndex)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> GetScaledRow<T>(this LaMatrix<T> matrix, int rowIndex, T scalingFactor)
        {
            var processor = matrix.ScalarsGridProcessor;

            return new LaVector<T>(
                processor,
                matrix
                    .MatrixStorage
                    .CreateLaVectorRowStorage((ulong) rowIndex)
                    .MapScalars(
                        value => processor.Times(scalingFactor, value)
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> GetMappedRow<T>(this LaMatrix<T> matrix, int rowIndex, Func<T, T> scalarMapping)
        {
            return new LaVector<T>(
                matrix.ScalarsGridProcessor,
                matrix
                    .MatrixStorage
                    .CreateLaVectorRowStorage((ulong) rowIndex)
                    .MapScalars(scalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> GetMappedRow<T>(this LaMatrix<T> matrix, int rowIndex, Func<ulong, T, T> indexScalarMapping)
        {
            return new LaVector<T>(
                matrix.ScalarsGridProcessor,
                matrix
                    .MatrixStorage
                    .CreateLaVectorRowStorage((ulong) rowIndex)
                    .MapScalars(indexScalarMapping)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<LaVector<T>> GetColumns<T>(this LaMatrix<T> matrix)
        {
            return Enumerable
                .Range(0, matrix.ColumnsCount)
                .Select(colIndex =>
                    new LaVector<T>(
                        matrix.ScalarsGridProcessor,
                        matrix.MatrixStorage.CreateLaVectorColumnStorage((ulong) colIndex)
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> GetColumn<T>(this LaMatrix<T> matrix, int colIndex)
        {
            return new LaVector<T>(
                matrix.ScalarsGridProcessor,
                matrix.MatrixStorage.CreateLaVectorColumnStorage((ulong) colIndex)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> GetScaledColumn<T>(this LaMatrix<T> matrix, int colIndex, T scalingFactor)
        {
            var processor = matrix.ScalarsGridProcessor;

            return new LaVector<T>(
                processor,
                matrix
                    .MatrixStorage
                    .CreateLaVectorColumnStorage((ulong) colIndex)
                    .MapScalars(
                        value => processor.Times(scalingFactor, value)
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> GetMappedColumn<T>(this LaMatrix<T> matrix, int colIndex, Func<T, T> scalarMapping)
        {
            return new LaVector<T>(
                matrix.ScalarsGridProcessor,
                matrix
                    .MatrixStorage
                    .CreateLaVectorColumnStorage((ulong) colIndex)
                    .MapScalars(scalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVector<T> GetMappedColumn<T>(this LaMatrix<T> matrix, int colIndex, Func<ulong, T, T> indexScalarMapping)
        {
            return new LaVector<T>(
                matrix.ScalarsGridProcessor,
                matrix
                    .MatrixStorage
                    .CreateLaVectorColumnStorage((ulong) colIndex)
                    .MapScalars(indexScalarMapping)
            );
        }


        public static T[,] MapScalars<T1, T>(this T1[,] matrix1, Func<T1, T> scalarMapping)
        {
            var rowsCount = matrix1.GetLength(0);
            var colsCount = matrix1.GetLength(1);
            var newMatrix = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
                newMatrix[i, j] = scalarMapping(matrix1[i, j]);

            return newMatrix;
        }

        public static T[,] MapScalars<T1, T>(this T1[,] matrix1, Func<int, int, T1, T> scalarMapping)
        {
            var rowsCount = matrix1.GetLength(0);
            var colsCount = matrix1.GetLength(1);
            var newMatrix = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
                newMatrix[i, j] = scalarMapping(i, j, matrix1[i, j]);

            return newMatrix;
        }

        public static T[,] MapScalars<T>(this T[,] matrix1, T[,] matrix2, T initScalar, Func<T, T, T> scalarMapping)
        {
            var rowCount1 = matrix1.GetLength(0);
            var colCount1 = matrix1.GetLength(1);

            var rowCount2 = matrix2.GetLength(0);
            var colCount2 = matrix1.GetLength(2);

            var rowCount = Math.Max(rowCount1, rowCount2);
            var colCount = Math.Max(colCount1, colCount2);

            var newMatrix = new T[rowCount, colCount];

            for (var j = 0; j < colCount; j++)
            for (var i = 0; i < rowCount; i++)
            {
                var s1 = i < rowCount1 && j < colCount1
                    ? matrix1[i, j]
                    : initScalar;

                var s2 = i < rowCount2 && j < colCount2
                    ? matrix2[i, j]
                    : initScalar;

                newMatrix[i, j] = scalarMapping(s1, s2);
            }

            return newMatrix;
        }

        public static T[,] MapScalars<T1, T2, T>(this T1[,] matrix1, T2[,] matrix2, Func<T1> initFunc1, Func<T2> initFunc2, Func<T1, T2, T> scalarMapping)
        {
            var rowCount1 = matrix1.GetLength(0);
            var colCount1 = matrix1.GetLength(1);

            var rowCount2 = matrix2.GetLength(0);
            var colCount2 = matrix1.GetLength(2);

            var rowCount = Math.Max(rowCount1, rowCount2);
            var colCount = Math.Max(colCount1, colCount2);

            var newMatrix = new T[rowCount, colCount];

            for (var j = 0; j < colCount; j++)
            for (var i = 0; i < rowCount; i++)
            {
                var s1 = i < rowCount1 && j < colCount1
                    ? matrix1[i, j]
                    : initFunc1();

                var s2 = i < rowCount2 && j < colCount2
                    ? matrix2[i, j]
                    : initFunc2();

                newMatrix[i, j] = scalarMapping(s1, s2);
            }

            return newMatrix;
        }

        public static T[,] MapScalars<T>(this T[,] matrix1, T[,] matrix2, T initScalar, Func<T, T, T, T> accumulatorFunc)
        {
            var rowsCount1 = matrix1.GetLength(0);
            var colsCount1 = matrix1.GetLength(1);

            var rowsCount2 = matrix2.GetLength(0);
            var colsCount2 = matrix2.GetLength(1);

            var innerCount = Math.Max(colsCount1, rowsCount2);

            var newMatrix = new T[rowsCount1, colsCount2];

            for (var i = 0; i < rowsCount1; i++)
            {
                for (var j = 0; j < colsCount2; j++)
                {
                    var scalar = initScalar;

                    for (var k = 0; k < innerCount; k++)
                    {
                        var aik = i < rowsCount1 && k < colsCount1
                            ? matrix1[i, k]
                            : initScalar;

                        var bkj = k < rowsCount2 && j < colsCount2
                            ? matrix2[k, j]
                            : initScalar;

                        scalar = accumulatorFunc(scalar, aik, bkj);
                    }

                    newMatrix[i, j] = scalar;
                }
            }

            return newMatrix;
        }

        public static T[,] MapScalars<T1, T2, T>(this T1[,] matrix1, T2[,] matrix2, Func<T1> initFunc1, Func<T2> initFunc2, Func<T> accumulatorInitFunc, Func<T, T1, T2, T> accumulatorFunc)
        {
            var rowsCount1 = matrix1.GetLength(0);
            var colsCount1 = matrix1.GetLength(1);

            var rowsCount2 = matrix2.GetLength(0);
            var colsCount2 = matrix2.GetLength(1);

            var innerCount = Math.Max(colsCount1, rowsCount2);

            var newMatrix = new T[rowsCount1, colsCount2];

            for (var i = 0; i < rowsCount1; i++)
            {
                for (var j = 0; j < colsCount2; j++)
                {
                    var accumulator = accumulatorInitFunc();

                    for (var k = 0; k < innerCount; k++)
                    {
                        var aik = i < rowsCount1 && k < colsCount1
                            ? matrix1[i, k]
                            : initFunc1();

                        var bkj = k < rowsCount2 && j < colsCount2
                            ? matrix2[k, j]
                            : initFunc2();

                        accumulator = accumulatorFunc(accumulator, aik, bkj);
                    }

                    newMatrix[i, j] = accumulator;
                }
            }

            return newMatrix;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IScalarProcessor<T> scalarProcessor, T[,] matrix, int rowIndex, int colIndex)
        {
            return rowIndex >= 0 &&
                   colIndex >= 0 &&
                   rowIndex < matrix.GetLength(0) &&
                   colIndex < matrix.GetLength(1)
                ? matrix[rowIndex, colIndex] ?? scalarProcessor.ScalarZero
                : scalarProcessor.ScalarZero;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Add<T>(this IScalarProcessor<T> scalarProcessor, T[,] matrix, T scalar)
        {
            return matrix.MapScalars(s => scalarProcessor.Add(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar, T[,] matrix)
        {
            return matrix.MapScalars(s => scalarProcessor.Add(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Add<T>(this IScalarProcessor<T> scalarProcessor, T[,] matrix1, T[,] matrix2)
        {
            return matrix1.MapScalars(
                matrix2, 
                scalarProcessor.ScalarZero,
                scalarProcessor.Add
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Subtract<T>(this IScalarProcessor<T> scalarProcessor, T[,] matrix, T scalar)
        {
            return matrix.MapScalars(s => scalarProcessor.Subtract(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar, T[,] matrix)
        {
            return matrix.MapScalars(s => scalarProcessor.Subtract(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Subtract<T>(this IScalarProcessor<T> scalarProcessor, T[,] matrix1, T[,] matrix2)
        {
            return matrix1.MapScalars(
                matrix2, 
                scalarProcessor.ScalarZero,
                scalarProcessor.Subtract
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Times<T>(this IScalarProcessor<T> scalarProcessor, T[,] matrix, T scalar)
        {
            return matrix.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, T[,] matrix)
        {
            return matrix.MapScalars(s => scalarProcessor.Times(scalar, s));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Divide<T>(this IScalarProcessor<T> scalarProcessor, T[,] matrix, T scalar)
        {
            return matrix.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar, T[,] matrix)
        {
            return matrix.MapScalars(s => scalarProcessor.Divide(scalar, s));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] MatrixTimes<T>(this IScalarProcessor<T> scalarProcessor, T[,] matrix1, T[,] matrix2)
        {
            return MapScalars(
                matrix1,
                matrix2,
                scalarProcessor.ScalarZero,
                (accumulator, a, b) => 
                    scalarProcessor.Add(accumulator, scalarProcessor.Times(a, b))
            );
        }


        public static IGaVectorStorage<T> MapVector<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> matrix, IGaVectorStorage<T> vector)
        {
            var (rowsCount, colsCount) = matrix.GetDenseCountPair();
            var composer = scalarProcessor.CreateKVectorStorageComposer();

            foreach (var (index, scalar) in vector.IndexScalarList.GetIndexScalarRecords())
            {
                if (index >= (ulong) colsCount)
                    continue;

                var j = (int) index;
                for (var i = 0; i < rowsCount; i++)
                {
                    var matrixScalar = matrix.GetValue(i, j);

                    if (ReferenceEquals(matrixScalar, null))
                        continue;

                    composer.AddTerm(
                        (ulong) i,
                        scalarProcessor.Times(scalar, matrixScalar)
                    );
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateGaVectorStorage();
        }


        public static T[,] CreateRotationMatrixToVector<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> sourceVector, IGaVectorStorage<T> targetVector, int size)
        {
            var matrix1 = scalarProcessor.CreateRotationMatrixFromE1(targetVector, size);
            var matrix2 = scalarProcessor.CreateRotationMatrixToE1(sourceVector, size);

            return scalarProcessor.MatrixTimes(matrix1, matrix2);
        }

        public static T[,] CreateRotationMatrixFromVector<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> targetVector, IGaVectorStorage<T> sourceVector, int size)
        {
            var matrix1 = scalarProcessor.CreateRotationMatrixFromE1(targetVector, size);
            var matrix2 = scalarProcessor.CreateRotationMatrixToE1(sourceVector, size);

            return scalarProcessor.MatrixTimes(matrix1, matrix2);
        }

        /// <summary>
        /// This method creates a rotation matrix which rotates the basis vector e_1 into
        /// the given unit vector
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scalarProcessor"></param>
        /// <param name="targetVector"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static T[,] CreateRotationMatrixFromE1<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> targetVector, int size)
        {
            if (size < 2)
                throw new ArgumentOutOfRangeException(nameof(size));

            // Special case: vector == e_1
            var v1 = scalarProcessor.GetTermScalarByIndex(targetVector, 0);
            if (scalarProcessor.IsOne(v1))
                return scalarProcessor.CreateIdentityScalarArray(size);

            v1 = scalarProcessor.Add(scalarProcessor.ScalarOne, v1);

            // Special case: vector == -e_1
            if (scalarProcessor.IsZero(v1))
            {
                //TODO: Handle this case
                throw new InvalidOperationException();
            }

            var matrix = 
                scalarProcessor.CreateZeroScalarArray(size);

            var indexScalarPairs = 
                targetVector.IndexScalarList;

            // Fill first row
            foreach (var (index, scalar) in indexScalarPairs.GetIndexScalarRecords())
            {
                if (index == 0)
                    continue;

                matrix[0, index] = 
                    scalarProcessor.Negative(scalar);
            }

            // Fill first column
            foreach (var (index, scalar) in indexScalarPairs.GetIndexScalarRecords())
                matrix[index, 0] = scalar;

            // Fill diagonal
            foreach (var (index, scalar) in indexScalarPairs.GetIndexScalarRecords())
            {
                if (index == 0)
                    continue;

                matrix[index, index] = 
                    scalarProcessor.Subtract(
                        scalarProcessor.ScalarOne,
                        scalarProcessor.Divide(
                            scalarProcessor.Square(scalar),
                            v1
                        )
                    );
            }

            // Fill remaining items
            foreach (var (index1, scalar1) in indexScalarPairs.GetIndexScalarRecords())
            {
                foreach (var (index2, scalar2) in indexScalarPairs.GetIndexScalarRecords())
                {
                    if (index1 == 0 || index2 == 0 || index1 == index2)
                        continue;

                    matrix[index1, index2] = 
                        scalarProcessor.Divide(
                            scalarProcessor.NegativeTimes(scalar1, scalar2),
                            v1
                        );
                }
            }

            return matrix;
        }

        /// <summary>
        /// This method creates a rotation matrix which rotates the given unit vector
        /// into the basis vector e_1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scalarProcessor"></param>
        /// <param name="sourceVector"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static T[,] CreateRotationMatrixToE1<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> sourceVector, int size)
        {
            if (size < 2)
                throw new ArgumentOutOfRangeException(nameof(size));

            // Special case: vector == e_1
            var v1 = scalarProcessor.GetTermScalarByIndex(sourceVector, 0);
            if (scalarProcessor.IsOne(v1))
                return scalarProcessor.CreateIdentityScalarArray(size);

            v1 = scalarProcessor.Add(scalarProcessor.ScalarOne, v1);

            // Special case: vector == -e_1
            if (scalarProcessor.IsZero(v1))
            {
                //TODO: Handle this case
                throw new InvalidOperationException();
            }

            var matrix = 
                scalarProcessor.CreateZeroScalarArray(size);

            var indexScalarPairs = 
                sourceVector.IndexScalarList;

            // Fill first column
            foreach (var (index, scalar) in indexScalarPairs.GetIndexScalarRecords())
            {
                if (index == 0)
                    continue;

                matrix[index, 0] = 
                    scalarProcessor.Negative(scalar);
            }

            // Fill first row
            foreach (var (index, scalar) in indexScalarPairs.GetIndexScalarRecords())
                matrix[0, index] = scalar;

            // Fill diagonal
            foreach (var (index, scalar) in indexScalarPairs.GetIndexScalarRecords())
            {
                if (index == 0)
                    continue;

                matrix[index, index] = 
                    scalarProcessor.Subtract(
                        scalarProcessor.ScalarOne,
                        scalarProcessor.Divide(
                            scalarProcessor.Square(scalar),
                            v1
                        )
                    );
            }

            // Fill remaining items
            foreach (var (index1, scalar1) in indexScalarPairs.GetIndexScalarRecords())
            {
                foreach (var (index2, scalar2) in indexScalarPairs.GetIndexScalarRecords())
                {
                    if (index1 == 0 || index2 == 0 || index1 == index2)
                        continue;

                    matrix[index1, index2] = 
                        scalarProcessor.Divide(
                            scalarProcessor.NegativeTimes(scalar1, scalar2),
                            v1
                        );
                }
            }

            return matrix;
        }
    }
}