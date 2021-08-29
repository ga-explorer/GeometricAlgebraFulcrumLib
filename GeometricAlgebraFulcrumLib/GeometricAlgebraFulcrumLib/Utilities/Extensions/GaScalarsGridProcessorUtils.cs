using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaScalarsGridProcessorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> vector, int index)
        {
            return vector.GetValue(index, scalarProcessor.ScalarZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> vector, ulong index)
        {
            return vector.GetValue(index, scalarProcessor.ScalarZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorRepeatedScalarStorage<T> CreateOnesScalarsList<T>(this IScalarProcessor<T> scalarProcessor, int size)
        {
            var scalar = scalarProcessor.ScalarOne;

            return scalar.CreateLaVectorRepeatedScalarStorage(size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorRepeatedScalarStorage<T> CreateUnitOnesScalarsList<T>(this IScalarProcessor<T> scalarProcessor, int size)
        {
            var scalar = scalarProcessor.Sqrt(
                scalarProcessor.Divide(scalarProcessor.ScalarOne, size)
            );

            return scalar.CreateLaVectorRepeatedScalarStorage(size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixDiagonalStorage<T> CreateOnesDiagonalGrid<T>(this IScalarProcessor<T> scalarProcessor, int size)
        {
            return scalarProcessor.CreateOnesScalarsList(size).CreateLaMatrixDiagonalStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixDiagonalStorage<T> CreateUnitOnesDiagonalGrid<T>(this IScalarProcessor<T> scalarProcessor, int size)
        {
            return scalarProcessor.CreateUnitOnesScalarsList(size).CreateLaMatrixDiagonalStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> grid, int rowIndex, int colIndex)
        {
            return grid.GetValue(rowIndex, colIndex, scalarProcessor.ScalarZero);
        }


        public static T[,] MapScalars<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2, Func<T, T, T> scalarMapping)
        {
            scalar2 ??= scalarProcessor.ScalarZero;

            var rowsCount = scalarsArray1.GetLength(0);
            var colsCount = scalarsArray1.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                array[i, j] = scalarMapping(
                    scalarsArray1[i, j] ?? scalarProcessor.ScalarZero,
                    scalar2
                );
            }

            return array;
        }

        public static T[,] MapScalars<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2, Func<T, T, T> scalarMapping)
        {
            scalar1 ??= scalarProcessor.ScalarZero;

            var rowsCount = scalarsArray2.GetLength(0);
            var colsCount = scalarsArray2.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                array[i, j] = scalarMapping(
                    scalar1,
                    scalarsArray2[i, j] ?? scalarProcessor.ScalarZero
                );
            }

            return array;
        }

        public static T[,] MapScalars<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2, Func<T, T, T> scalarMapping)
        {
            var rowsCount = scalarsArray1.GetLength(0);
            var colsCount = scalarsArray1.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                array[i, j] = scalarMapping(
                    scalarsArray1[i, j] ?? scalarProcessor.ScalarZero,
                    scalarsArray2[i, j] ?? scalarProcessor.ScalarZero
                );
            }

            return array;
        }

        public static T[,] MapNotZeroScalars<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray, Func<T, T> scalarMapping)
        {
            var rowsCount = scalarsArray.GetLength(0);
            var colsCount = scalarsArray.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                var scalar = scalarsArray[i, j];

                if (ReferenceEquals(scalar, null) || scalarProcessor.IsZero(scalar))
                {
                    array[i, j] = scalarProcessor.ScalarZero;

                    continue;
                }

                array[i, j] = scalarMapping(
                    scalarsArray[i, j]
                );
            }

            return array;
        }

        public static T[,] MapScalars<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray, Func<T, T> scalarMapping)
        {
            var rowsCount = scalarsArray.GetLength(0);
            var colsCount = scalarsArray.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                array[i, j] = scalarMapping(
                    scalarsArray[i, j] ?? scalarProcessor.ScalarZero
                );
            }

            return array;
        }

        public static T[,] Add<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalarsArray2, 
                scalarProcessor.Add
            );
        }

        public static T[,] Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1, 
                scalarsArray2, 
                scalarProcessor.Add
            );
        }

        public static T[,] Add<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalar2,
                scalarProcessor.Add
            );
        }

        public static T[,] Subtract<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalarsArray2, 
                scalarProcessor.Subtract
            );
        }

        public static T[,] Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1, 
                scalarsArray2, 
                scalarProcessor.Subtract
            );
        }

        public static T[,] Subtract<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalar2,
                scalarProcessor.Subtract
            );
        }

        public static T[,] Times<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalarsArray2, 
                scalarProcessor.Times
            );
        }

        public static T[,] Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1, 
                scalarsArray2, 
                scalarProcessor.Times
            );
        }

        public static T[,] Times<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalar2,
                scalarProcessor.Times
            );
        }

        public static T[,] Divide<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalarsArray2, 
                scalarProcessor.Divide
            );
        }

        public static T[,] Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1, 
                scalarsArray2, 
                scalarProcessor.Divide
            );
        }

        public static T[,] Divide<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalar2,
                scalarProcessor.Divide
            );
        }
        
        public static T[,] NotZeroScalarsInverse<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapNotZeroScalars(
                scalarsArray, 
                scalarProcessor.Inverse
            );
        }
        
        public static T[,] ScalarsNegative<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Negative
            );
        }

        public static T[,] ScalarsCos<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Cos
            );
        }

        public static T[,] ScalarsSin<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Sin
            );
        }

        public static T[,] ScalarsTan<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Tan
            );
        }

        public static T[,] ScalarsExp<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Exp
            );
        }

        public static T[,] ScalarsLog<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Log
            );
        }

        public static T[,] ScalarsLog10<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Log10
            );
        }

        public static T[,] ScalarsLog2<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Log2
            );
        }

        public static T[,] ScalarsAbs<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Abs
            );
        }

        public static T[,] ScalarsSqrt<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Sqrt
            );
        }

        public static T[,] ScalarsSqrtOfAbs<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.SqrtOfAbs
            );
        }

        
        public static IGaVectorStorage<T> ColumnToVectorStorage<T>(this T[,] scalarsArray, int colIndex, IScalarProcessor<T> scalarProcessor)
        {
            var rowsCount = scalarsArray.GetLength(0);

            var composer = scalarProcessor.CreateKVectorStorageComposer(1);

            for (var i = 0; i < rowsCount; i++)
            {
                var scalar = scalarsArray[i, colIndex] ?? scalarProcessor.ScalarZero;

                if (!scalarProcessor.IsZero(scalar))
                    composer.SetTerm((ulong) i, scalar);
            }

            return composer.CreateGaVectorStorage();
        }

        public static IGaVectorStorage<T> RowToVectorStorage<T>(this T[,] scalarsArray, int rowIndex, IScalarProcessor<T> scalarProcessor)
        {
            var colsCount = scalarsArray.GetLength(1);

            var composer = scalarProcessor.CreateKVectorStorageComposer(1);

            for (var j = 0; j < colsCount; j++)
            {
                var scalar = scalarsArray[rowIndex, j] ?? scalarProcessor.ScalarZero;

                if (!scalarProcessor.IsZero(scalar))
                    composer.SetTerm((ulong) j, scalar);
            }

            return composer.CreateGaVectorStorage();
        }

        public static Dictionary<ulong, IGaVectorStorage<T>> ColumnsToVectorStoragesDictionary<T>(this T[,] scalarsArray, IScalarProcessor<T> scalarProcessor)
        {
            var rowsCount = scalarsArray.GetLength(0);
            var colsCount = scalarsArray.GetLength(1);

            var vectorsDictionary = 
                new Dictionary<ulong, IGaVectorStorage<T>>();

            for (var j = 0; j < colsCount; j++)
            {
                var composer = scalarProcessor.CreateKVectorStorageComposer(1);

                for (var i = 0; i < rowsCount; i++)
                {
                    var scalar = scalarsArray[i, j] ?? scalarProcessor.ScalarZero;

                    if (!scalarProcessor.IsZero(scalar))
                        composer.SetTerm((ulong) i, scalar);
                }

                vectorsDictionary.Add((ulong) j, composer.CreateGaVectorStorage());
            }

            return vectorsDictionary;
        }

        public static IGaVectorStorage<T>[] ColumnsToVectorStoragesArray<T>(this T[,] scalarsArray, IScalarProcessor<T> scalarProcessor)
        {
            var rowsCount = scalarsArray.GetLength(0);
            var colsCount = scalarsArray.GetLength(1);

            var vectorsArray = 
                new IGaVectorStorage<T>[colsCount];

            for (var j = 0; j < colsCount; j++)
            {
                var composer = scalarProcessor.CreateKVectorStorageComposer(1);

                for (var i = 0; i < rowsCount; i++)
                {
                    var scalar = scalarsArray[i, j] ?? scalarProcessor.ScalarZero;

                    if (!scalarProcessor.IsZero(scalar))
                        composer.SetTerm((ulong) i, scalar);
                }

                vectorsArray[j] = composer.CreateGaVectorStorage();
            }

            return vectorsArray;
        }

    }
}