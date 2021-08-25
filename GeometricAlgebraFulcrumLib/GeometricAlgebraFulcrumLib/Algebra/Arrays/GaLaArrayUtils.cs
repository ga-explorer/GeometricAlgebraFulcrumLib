using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Utils;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Algebra.Arrays
{
    public static class GaLaArrayUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaLaVector<T>> GetRows<T>(this GaLaArray<T> matrix)
        {
            return Enumerable
                .Range(0, matrix.RowsCount)
                .Select(rowIndex =>
                    new GaLaVector<T>(
                        matrix.ScalarsGridProcessor,
                        matrix.ArrayStorage.CreateEvenListDenseGridSlice1((ulong) rowIndex)
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaVector<T> GetRow<T>(this GaLaArray<T> matrix, int rowIndex)
        {
            return new GaLaVector<T>(
                matrix.ScalarsGridProcessor,
                matrix.ArrayStorage.CreateEvenListDenseGridSlice1((ulong) rowIndex)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaVector<T> GetScaledRow<T>(this GaLaArray<T> matrix, int rowIndex, T scalingFactor)
        {
            var processor = matrix.ScalarsGridProcessor;

            return new GaLaVector<T>(
                processor,
                matrix
                    .ArrayStorage
                    .CreateEvenListDenseGridSlice1((ulong) rowIndex)
                    .MapValues(
                        value => processor.Times(scalingFactor, value)
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaVector<T> GetMappedRow<T>(this GaLaArray<T> matrix, int rowIndex, Func<T, T> scalarMapping)
        {
            return new GaLaVector<T>(
                matrix.ScalarsGridProcessor,
                matrix
                    .ArrayStorage
                    .CreateEvenListDenseGridSlice1((ulong) rowIndex)
                    .MapValues(scalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaVector<T> GetMappedRow<T>(this GaLaArray<T> matrix, int rowIndex, Func<ulong, T, T> indexScalarMapping)
        {
            return new GaLaVector<T>(
                matrix.ScalarsGridProcessor,
                matrix
                    .ArrayStorage
                    .CreateEvenListDenseGridSlice1((ulong) rowIndex)
                    .MapValues(indexScalarMapping)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaLaVector<T>> GetColumns<T>(this GaLaArray<T> matrix)
        {
            return Enumerable
                .Range(0, matrix.ColumnsCount)
                .Select(colIndex =>
                    new GaLaVector<T>(
                        matrix.ScalarsGridProcessor,
                        matrix.ArrayStorage.CreateEvenListDenseGridSlice2((ulong) colIndex)
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaVector<T> GetColumn<T>(this GaLaArray<T> matrix, int colIndex)
        {
            return new GaLaVector<T>(
                matrix.ScalarsGridProcessor,
                matrix.ArrayStorage.CreateEvenListDenseGridSlice2((ulong) colIndex)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaVector<T> GetScaledColumn<T>(this GaLaArray<T> matrix, int colIndex, T scalingFactor)
        {
            var processor = matrix.ScalarsGridProcessor;

            return new GaLaVector<T>(
                processor,
                matrix
                    .ArrayStorage
                    .CreateEvenListDenseGridSlice2((ulong) colIndex)
                    .MapValues(
                        value => processor.Times(scalingFactor, value)
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaVector<T> GetMappedColumn<T>(this GaLaArray<T> matrix, int colIndex, Func<T, T> scalarMapping)
        {
            return new GaLaVector<T>(
                matrix.ScalarsGridProcessor,
                matrix
                    .ArrayStorage
                    .CreateEvenListDenseGridSlice2((ulong) colIndex)
                    .MapValues(scalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaVector<T> GetMappedColumn<T>(this GaLaArray<T> matrix, int colIndex, Func<ulong, T, T> indexScalarMapping)
        {
            return new GaLaVector<T>(
                matrix.ScalarsGridProcessor,
                matrix
                    .ArrayStorage
                    .CreateEvenListDenseGridSlice2((ulong) colIndex)
                    .MapValues(indexScalarMapping)
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

        public static T[,] MapScalars<T>(this T[,] matrix1, T[,] matrix2, Func<T> initFunc, Func<T, T, T> scalarMapping)
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
                    : initFunc();

                var s2 = i < rowCount2 && j < colCount2
                    ? matrix2[i, j]
                    : initFunc();

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

        public static T[,] MapScalars<T>(this T[,] matrix1, T[,] matrix2, Func<T> initFunc, Func<T, T, T, T> accumulatorFunc)
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
                    var scalar = initFunc();

                    for (var k = 0; k < innerCount; k++)
                    {
                        var aik = i < rowsCount1 && k < colsCount1
                            ? matrix1[i, k]
                            : initFunc();

                        var bkj = k < rowsCount2 && j < colsCount2
                            ? matrix2[k, j]
                            : initFunc();

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
        public static T GetScalar<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] matrix, int rowIndex, int colIndex)
        {
            return rowIndex >= 0 &&
                   colIndex >= 0 &&
                   rowIndex < matrix.GetLength(0) &&
                   colIndex < matrix.GetLength(1)
                ? matrix[rowIndex, colIndex] ?? scalarProcessor.GetZeroScalar()
                : scalarProcessor.GetZeroScalar();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Add<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] matrix, T scalar)
        {
            return matrix.MapScalars(s => scalarProcessor.Add(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Add<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, T[,] matrix)
        {
            return matrix.MapScalars(s => scalarProcessor.Add(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Add<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] matrix1, T[,] matrix2)
        {
            return matrix1.MapScalars(
                matrix2, 
                scalarProcessor.GetZeroScalar,
                scalarProcessor.Add
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] matrix, T scalar)
        {
            return matrix.MapScalars(s => scalarProcessor.Subtract(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, T[,] matrix)
        {
            return matrix.MapScalars(s => scalarProcessor.Subtract(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] matrix1, T[,] matrix2)
        {
            return matrix1.MapScalars(
                matrix2, 
                () => scalarProcessor.GetZeroScalar(),
                scalarProcessor.Subtract
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Times<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] matrix, T scalar)
        {
            return matrix.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, T[,] matrix)
        {
            return matrix.MapScalars(s => scalarProcessor.Times(scalar, s));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Divide<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] matrix, T scalar)
        {
            return matrix.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Divide<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, T[,] matrix)
        {
            return matrix.MapScalars(s => scalarProcessor.Divide(scalar, s));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] MatrixTimes<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] matrix1, T[,] matrix2)
        {
            return MapScalars(
                matrix1,
                matrix2,
                scalarProcessor.GetZeroScalar,
                (accumulator, a, b) => 
                    scalarProcessor.Add(accumulator, scalarProcessor.Times(a, b))
            );
        }


        public static IGaStorageVector<T> MapVector<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> matrix, IGaStorageVector<T> vector)
        {
            var (rowsCount, colsCount) = matrix.GetDenseCountPair();
            var composer = scalarProcessor.CreateStorageKVectorComposer();

            foreach (var (index, scalar) in vector.IndexScalarList.GetKeyValueRecords())
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

            return composer.CreateStorageVector();
        }


        public static T[,] CreateRotationMatrixToVector<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> sourceVector, IGaStorageVector<T> targetVector, int size)
        {
            var matrix1 = scalarProcessor.CreateRotationMatrixFromE1(targetVector, size);
            var matrix2 = scalarProcessor.CreateRotationMatrixToE1(sourceVector, size);

            return scalarProcessor.MatrixTimes(matrix1, matrix2);
        }

        public static T[,] CreateRotationMatrixFromVector<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> targetVector, IGaStorageVector<T> sourceVector, int size)
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
        public static T[,] CreateRotationMatrixFromE1<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> targetVector, int size)
        {
            if (size < 2)
                throw new ArgumentOutOfRangeException(nameof(size));

            // Special case: vector == e_1
            var v1 = scalarProcessor.GetTermScalarByIndex(targetVector, 0);
            if (scalarProcessor.IsOne(v1))
                return scalarProcessor.CreateIdentityScalarArray(size);

            v1 = scalarProcessor.Add(scalarProcessor.GetOneScalar(), v1);

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
            foreach (var (index, scalar) in indexScalarPairs.GetKeyValueRecords())
            {
                if (index == 0)
                    continue;

                matrix[0, index] = 
                    scalarProcessor.Negative(scalar);
            }

            // Fill first column
            foreach (var (index, scalar) in indexScalarPairs.GetKeyValueRecords())
                matrix[index, 0] = scalar;

            // Fill diagonal
            foreach (var (index, scalar) in indexScalarPairs.GetKeyValueRecords())
            {
                if (index == 0)
                    continue;

                matrix[index, index] = 
                    scalarProcessor.Subtract(
                        scalarProcessor.GetOneScalar(),
                        scalarProcessor.Divide(
                            scalarProcessor.Square(scalar),
                            v1
                        )
                    );
            }

            // Fill remaining items
            foreach (var (index1, scalar1) in indexScalarPairs.GetKeyValueRecords())
            {
                foreach (var (index2, scalar2) in indexScalarPairs.GetKeyValueRecords())
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
        public static T[,] CreateRotationMatrixToE1<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> sourceVector, int size)
        {
            if (size < 2)
                throw new ArgumentOutOfRangeException(nameof(size));

            // Special case: vector == e_1
            var v1 = scalarProcessor.GetTermScalarByIndex(sourceVector, 0);
            if (scalarProcessor.IsOne(v1))
                return scalarProcessor.CreateIdentityScalarArray(size);

            v1 = scalarProcessor.Add(scalarProcessor.GetOneScalar(), v1);

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
            foreach (var (index, scalar) in indexScalarPairs.GetKeyValueRecords())
            {
                if (index == 0)
                    continue;

                matrix[index, 0] = 
                    scalarProcessor.Negative(scalar);
            }

            // Fill first row
            foreach (var (index, scalar) in indexScalarPairs.GetKeyValueRecords())
                matrix[0, index] = scalar;

            // Fill diagonal
            foreach (var (index, scalar) in indexScalarPairs.GetKeyValueRecords())
            {
                if (index == 0)
                    continue;

                matrix[index, index] = 
                    scalarProcessor.Subtract(
                        scalarProcessor.GetOneScalar(),
                        scalarProcessor.Divide(
                            scalarProcessor.Square(scalar),
                            v1
                        )
                    );
            }

            // Fill remaining items
            foreach (var (index1, scalar1) in indexScalarPairs.GetKeyValueRecords())
            {
                foreach (var (index2, scalar2) in indexScalarPairs.GetKeyValueRecords())
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