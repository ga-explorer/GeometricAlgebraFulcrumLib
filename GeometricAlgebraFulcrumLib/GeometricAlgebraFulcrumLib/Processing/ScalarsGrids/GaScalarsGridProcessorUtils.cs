using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Processing.ScalarsGrids
{
    public static class GaScalarsGridProcessorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseRepeatedValue<T> CreateOnesScalarsList<T>(this IGaScalarProcessor<T> scalarProcessor, int size)
        {
            var scalar = scalarProcessor.GetOneScalar();

            return scalar.CreateEvenListRepeatedValue(size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseRepeatedValue<T> CreateUnitOnesScalarsList<T>(this IGaScalarProcessor<T> scalarProcessor, int size)
        {
            var scalar = scalarProcessor.Sqrt(
                scalarProcessor.Divide(scalarProcessor.GetOneScalar(), size)
            );

            return scalar.CreateEvenListRepeatedValue(size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridEvenDiagonal<T> CreateOnesDiagonalGrid<T>(this IGaScalarProcessor<T> scalarProcessor, int size)
        {
            return scalarProcessor.CreateOnesScalarsList(size).CreateEvenGridDiagonal();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridEvenDiagonal<T> CreateUnitOnesDiagonalGrid<T>(this IGaScalarProcessor<T> scalarProcessor, int size)
        {
            return scalarProcessor.CreateUnitOnesScalarsList(size).CreateEvenGridDiagonal();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> grid, int rowIndex, int colIndex)
        {
            return grid.GetValue(rowIndex, colIndex, scalarProcessor.GetZeroScalar);
        }


        public static T[,] MapScalars<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2, Func<T, T, T> scalarMapping)
        {
            scalar2 ??= scalarProcessor.GetZeroScalar();

            var rowsCount = scalarsArray1.GetLength(0);
            var colsCount = scalarsArray1.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                array[i, j] = scalarMapping(
                    scalarsArray1[i, j] ?? scalarProcessor.GetZeroScalar(),
                    scalar2
                );
            }

            return array;
        }

        public static T[,] MapScalars<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2, Func<T, T, T> scalarMapping)
        {
            scalar1 ??= scalarProcessor.GetZeroScalar();

            var rowsCount = scalarsArray2.GetLength(0);
            var colsCount = scalarsArray2.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                array[i, j] = scalarMapping(
                    scalar1,
                    scalarsArray2[i, j] ?? scalarProcessor.GetZeroScalar()
                );
            }

            return array;
        }

        public static T[,] MapScalars<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2, Func<T, T, T> scalarMapping)
        {
            var rowsCount = scalarsArray1.GetLength(0);
            var colsCount = scalarsArray1.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                array[i, j] = scalarMapping(
                    scalarsArray1[i, j] ?? scalarProcessor.GetZeroScalar(),
                    scalarsArray2[i, j] ?? scalarProcessor.GetZeroScalar()
                );
            }

            return array;
        }

        public static T[,] MapNotZeroScalars<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray, Func<T, T> scalarMapping)
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
                    array[i, j] = scalarProcessor.GetZeroScalar();

                    continue;
                }

                array[i, j] = scalarMapping(
                    scalarsArray[i, j]
                );
            }

            return array;
        }

        public static T[,] MapScalars<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray, Func<T, T> scalarMapping)
        {
            var rowsCount = scalarsArray.GetLength(0);
            var colsCount = scalarsArray.GetLength(1);

            var array = new T[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                array[i, j] = scalarMapping(
                    scalarsArray[i, j] ?? scalarProcessor.GetZeroScalar()
                );
            }

            return array;
        }

        public static T[,] Add<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalarsArray2, 
                scalarProcessor.Add
            );
        }

        public static T[,] Add<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1, 
                scalarsArray2, 
                scalarProcessor.Add
            );
        }

        public static T[,] Add<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalar2,
                scalarProcessor.Add
            );
        }

        public static T[,] Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalarsArray2, 
                scalarProcessor.Subtract
            );
        }

        public static T[,] Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1, 
                scalarsArray2, 
                scalarProcessor.Subtract
            );
        }

        public static T[,] Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalar2,
                scalarProcessor.Subtract
            );
        }

        public static T[,] Times<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalarsArray2, 
                scalarProcessor.Times
            );
        }

        public static T[,] Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1, 
                scalarsArray2, 
                scalarProcessor.Times
            );
        }

        public static T[,] Times<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalar2,
                scalarProcessor.Times
            );
        }

        public static T[,] Divide<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalarsArray2, 
                scalarProcessor.Divide
            );
        }

        public static T[,] Divide<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1, 
                scalarsArray2, 
                scalarProcessor.Divide
            );
        }

        public static T[,] Divide<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalar2,
                scalarProcessor.Divide
            );
        }
        
        public static T[,] NotZeroScalarsInverse<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapNotZeroScalars(
                scalarsArray, 
                scalarProcessor.Inverse
            );
        }
        
        public static T[,] ScalarsNegative<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Negative
            );
        }

        public static T[,] ScalarsCos<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Cos
            );
        }

        public static T[,] ScalarsSin<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Sin
            );
        }

        public static T[,] ScalarsTan<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Tan
            );
        }

        public static T[,] ScalarsExp<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Exp
            );
        }

        public static T[,] ScalarsLog<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Log
            );
        }

        public static T[,] ScalarsLog10<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Log10
            );
        }

        public static T[,] ScalarsLog2<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Log2
            );
        }

        public static T[,] ScalarsAbs<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Abs
            );
        }

        public static T[,] ScalarsSqrt<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Sqrt
            );
        }

        public static T[,] ScalarsSqrtOfAbs<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.SqrtOfAbs
            );
        }

        
        public static IGaStorageVector<T> ColumnToVectorStorage<T>(this T[,] scalarsArray, int colIndex, IGaScalarProcessor<T> scalarProcessor)
        {
            var rowsCount = scalarsArray.GetLength(0);

            var composer = scalarProcessor.CreateStorageKVectorComposer(1);

            for (var i = 0; i < rowsCount; i++)
            {
                var scalar = scalarsArray[i, colIndex] ?? scalarProcessor.GetZeroScalar();

                if (!scalarProcessor.IsZero(scalar))
                    composer.SetTerm((ulong) i, scalar);
            }

            return composer.CreateStorageVector();
        }

        public static IGaStorageVector<T> RowToVectorStorage<T>(this T[,] scalarsArray, int rowIndex, IGaScalarProcessor<T> scalarProcessor)
        {
            var colsCount = scalarsArray.GetLength(1);

            var composer = scalarProcessor.CreateStorageKVectorComposer(1);

            for (var j = 0; j < colsCount; j++)
            {
                var scalar = scalarsArray[rowIndex, j] ?? scalarProcessor.GetZeroScalar();

                if (!scalarProcessor.IsZero(scalar))
                    composer.SetTerm((ulong) j, scalar);
            }

            return composer.CreateStorageVector();
        }

        public static Dictionary<ulong, IGaStorageVector<T>> ColumnsToVectorStoragesDictionary<T>(this T[,] scalarsArray, IGaScalarProcessor<T> scalarProcessor)
        {
            var rowsCount = scalarsArray.GetLength(0);
            var colsCount = scalarsArray.GetLength(1);

            var vectorsDictionary = 
                new Dictionary<ulong, IGaStorageVector<T>>();

            for (var j = 0; j < colsCount; j++)
            {
                var composer = scalarProcessor.CreateStorageKVectorComposer(1);

                for (var i = 0; i < rowsCount; i++)
                {
                    var scalar = scalarsArray[i, j] ?? scalarProcessor.GetZeroScalar();

                    if (!scalarProcessor.IsZero(scalar))
                        composer.SetTerm((ulong) i, scalar);
                }

                vectorsDictionary.Add((ulong) j, composer.CreateStorageVector());
            }

            return vectorsDictionary;
        }

        public static IGaStorageVector<T>[] ColumnsToVectorStoragesArray<T>(this T[,] scalarsArray, IGaScalarProcessor<T> scalarProcessor)
        {
            var rowsCount = scalarsArray.GetLength(0);
            var colsCount = scalarsArray.GetLength(1);

            var vectorsArray = 
                new IGaStorageVector<T>[colsCount];

            for (var j = 0; j < colsCount; j++)
            {
                var composer = scalarProcessor.CreateStorageKVectorComposer(1);

                for (var i = 0; i < rowsCount; i++)
                {
                    var scalar = scalarsArray[i, j] ?? scalarProcessor.GetZeroScalar();

                    if (!scalarProcessor.IsZero(scalar))
                        composer.SetTerm((ulong) i, scalar);
                }

                vectorsArray[j] = composer.CreateStorageVector();
            }

            return vectorsArray;
        }

    }
}