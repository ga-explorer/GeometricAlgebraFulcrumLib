using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class LinArrayUtils
    {
        public static T[,] MapScalars<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2, Func<T, T, T> scalarMapping)
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

        public static T[,] MapScalars<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2, Func<T, T, T> scalarMapping)
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

        public static T[,] MapScalars<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2, Func<T, T, T> scalarMapping)
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

        public static T[,] MapNotZeroScalars<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray, Func<T, T> scalarMapping)
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

        public static T[,] MapScalars<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray, Func<T, T> scalarMapping)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalarsArray2, 
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1, 
                scalarsArray2, 
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalar2,
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Subtract<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalarsArray2, 
                scalarProcessor.Subtract
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Subtract<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1, 
                scalarsArray2, 
                scalarProcessor.Subtract
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Subtract<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalar2,
                scalarProcessor.Subtract
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalarsArray2, 
                scalarProcessor.Times
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1, 
                scalarsArray2, 
                scalarProcessor.Times
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalar2,
                scalarProcessor.Times
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalarsArray2, 
                scalarProcessor.Divide
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar1, T[,] scalarsArray2)
        {
            return scalarProcessor.MapScalars(
                scalar1, 
                scalarsArray2, 
                scalarProcessor.Divide
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray1, T scalar2)
        {
            return scalarProcessor.MapScalars(
                scalarsArray1, 
                scalar2,
                scalarProcessor.Divide
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] NotZeroScalarsInverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapNotZeroScalars(
                scalarsArray, 
                scalarProcessor.Inverse
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsNegative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Negative
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsCos<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Cos
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsSin<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Sin
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsTan<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Tan
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsExp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Exp
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsLog<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.LogE
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsLog10<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Log10
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsLog2<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Log2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsAbs<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Abs
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsSqrt<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.Sqrt
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ScalarsSqrtOfAbs<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] scalarsArray)
        {
            return scalarProcessor.MapScalars(
                scalarsArray, 
                scalarProcessor.SqrtOfAbs
            );
        }

        
        public static VectorStorage<T> ColumnToVectorStorage<T>(this T[,] scalarsArray, int colIndex, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            var rowsCount = scalarsArray.GetLength(0);

            var composer = scalarProcessor.CreateVectorStorageComposer(1);

            for (var i = 0; i < rowsCount; i++)
            {
                var scalar = scalarsArray[i, colIndex] ?? scalarProcessor.ScalarZero;

                if (!scalarProcessor.IsZero(scalar))
                    composer.SetTerm((ulong) i, scalar);
            }

            return composer.CreateVectorStorage();
        }

        public static VectorStorage<T> RowToVectorStorage<T>(this T[,] scalarsArray, int rowIndex, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            var colsCount = scalarsArray.GetLength(1);

            var composer = scalarProcessor.CreateVectorStorageComposer(1);

            for (var j = 0; j < colsCount; j++)
            {
                var scalar = scalarsArray[rowIndex, j] ?? scalarProcessor.ScalarZero;

                if (!scalarProcessor.IsZero(scalar))
                    composer.SetTerm((ulong) j, scalar);
            }

            return composer.CreateVectorStorage();
        }

        public static Dictionary<ulong, VectorStorage<T>> ColumnsToVectorStoragesDictionary<T>(this T[,] scalarsArray, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            var rowsCount = scalarsArray.GetLength(0);
            var colsCount = scalarsArray.GetLength(1);

            var vectorsDictionary = 
                new Dictionary<ulong, VectorStorage<T>>();

            for (var j = 0; j < colsCount; j++)
            {
                var composer = scalarProcessor.CreateVectorStorageComposer(1);

                for (var i = 0; i < rowsCount; i++)
                {
                    var scalar = scalarsArray[i, j] ?? scalarProcessor.ScalarZero;

                    if (!scalarProcessor.IsZero(scalar))
                        composer.SetTerm((ulong) i, scalar);
                }

                vectorsDictionary.Add((ulong) j, composer.CreateVectorStorage());
            }

            return vectorsDictionary;
        }

        public static VectorStorage<T>[] ColumnsToVectorStoragesArray<T>(this T[,] scalarsArray, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            var rowsCount = scalarsArray.GetLength(0);
            var colsCount = scalarsArray.GetLength(1);

            var vectorsArray = 
                new VectorStorage<T>[colsCount];

            for (var j = 0; j < colsCount; j++)
            {
                var composer = scalarProcessor.CreateVectorStorageComposer(1);

                for (var i = 0; i < rowsCount; i++)
                {
                    var scalar = scalarsArray[i, j] ?? scalarProcessor.ScalarZero;

                    if (!scalarProcessor.IsZero(scalar))
                        composer.SetTerm((ulong) i, scalar);
                }

                vectorsArray[j] = composer.CreateVectorStorage();
            }

            return vectorsArray;
        }
        
        public static Vector<T>[] ColumnsToVectorsArray<T>(this T[,] scalarsArray, IScalarAlgebraProcessor<T> scalarProcessor)
        {
            var rowsCount = scalarsArray.GetLength(0);
            var colsCount = scalarsArray.GetLength(1);

            var vectorsArray = 
                new Vector<T>[colsCount];

            for (var j = 0; j < colsCount; j++)
            {
                var composer = scalarProcessor.CreateVectorStorageComposer(1);

                for (var i = 0; i < rowsCount; i++)
                {
                    var scalar = scalarsArray[i, j] ?? scalarProcessor.ScalarZero;

                    if (!scalarProcessor.IsZero(scalar))
                        composer.SetTerm((ulong) i, scalar);
                }

                vectorsArray[j] = composer.CreateVector();
            }

            return vectorsArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] matrix, int index)
        {
            return index >= 0 &&
                   index < matrix.Length
                ? matrix[index] ?? scalarProcessor.ScalarZero
                : scalarProcessor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] matrix, int rowIndex, int colIndex)
        {
            return rowIndex >= 0 &&
                   colIndex >= 0 &&
                   rowIndex < matrix.GetLength(0) &&
                   colIndex < matrix.GetLength(1)
                ? matrix[rowIndex, colIndex] ?? scalarProcessor.ScalarZero
                : scalarProcessor.ScalarZero;
        }

        public static T[] MapScalars<T1, T>(this T1[] matrix1, Func<T1, T> scalarMapping)
        {
            var count = matrix1.Length;
            var newMatrix = new T[count];

            for (var i = 0; i < count; i++)
                newMatrix[i] = scalarMapping(matrix1[i]);

            return newMatrix;
        }

        public static T[] MapScalars<T1, T>(this T1[] matrix1, Func<int, T1, T> scalarMapping)
        {
            var count = matrix1.Length;
            var newMatrix = new T[count];

            for (var i = 0; i < count; i++)
                newMatrix[i] = scalarMapping(i, matrix1[i]);

            return newMatrix;
        }

        public static T[] MapScalars<T>(this T[] matrix1, T[] matrix2, T initScalar, Func<T, T, T> scalarMapping)
        {
            var count1 = matrix1.Length;
            var count2 = matrix2.Length;
            var count = Math.Max(count1, count2);

            var newMatrix = new T[count];

            for (var i = 0; i < count; i++)
            {
                var s1 = i < count1
                    ? matrix1[i]
                    : initScalar;

                var s2 = i < count2
                    ? matrix2[i]
                    : initScalar;

                newMatrix[i] = scalarMapping(s1, s2);
            }

            return newMatrix;
        }

        public static T[] MapScalars<T1, T2, T>(this T1[] matrix1, T2[] matrix2, Func<T1> initFunc1, Func<T2> initFunc2, Func<T1, T2, T> scalarMapping)
        {
            var count1 = matrix1.Length;
            var count2 = matrix2.Length;
            var count = Math.Max(count1, count2);

            var newMatrix = new T[count];

            for (var i = 0; i < count; i++)
            {
                var s1 = i < count1
                    ? matrix1[i]
                    : initFunc1();

                var s2 = i < count2
                    ? matrix2[i]
                    : initFunc2();

                newMatrix[i] = scalarMapping(s1, s2);
            }

            return newMatrix;
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
        public static T[,] MatrixProduct<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] matrix1, T[,] matrix2)
        {
            return MapScalars(
                matrix1,
                matrix2,
                scalarProcessor.ScalarZero,
                (accumulator, a, b) => 
                    scalarProcessor.Add(accumulator, scalarProcessor.Times(a, b))
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Abs<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1)
        {
            return v1.MapScalars(scalarProcessor.Abs);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Sqrt<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1)
        {
            return v1.MapScalars(scalarProcessor.Sqrt);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] SqrtOfAbs<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1)
        {
            return v1.MapScalars(scalarProcessor.SqrtOfAbs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Exp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1)
        {
            return v1.MapScalars(scalarProcessor.Exp);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] LogE<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1)
        {
            return v1.MapScalars(scalarProcessor.LogE);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Log2<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1)
        {
            return v1.MapScalars(scalarProcessor.Log2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Log10<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1)
        {
            return v1.MapScalars(scalarProcessor.Log10);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Log<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1, T scalar)
        {
            return v1.MapScalars(s => scalarProcessor.Log(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Log<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, T[] v1)
        {
            return v1.MapScalars(s => scalarProcessor.Log(scalar, s));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Power<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1, T scalar)
        {
            return v1.MapScalars(s => scalarProcessor.Power(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Power<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, T[] v1)
        {
            return v1.MapScalars(s => scalarProcessor.Power(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Cos<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1)
        {
            return v1.MapScalars(scalarProcessor.Cos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Sin<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1)
        {
            return v1.MapScalars(scalarProcessor.Sin);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Tan<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1)
        {
            return v1.MapScalars(scalarProcessor.Tan);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ArcCos<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1)
        {
            return v1.MapScalars(scalarProcessor.ArcCos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ArcSin<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1)
        {
            return v1.MapScalars(scalarProcessor.ArcSin);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ArcTan<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1)
        {
            return v1.MapScalars(scalarProcessor.ArcTan);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Cosh<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1)
        {
            return v1.MapScalars(scalarProcessor.Cosh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Sinh<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1)
        {
            return v1.MapScalars(scalarProcessor.Sinh);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Tanh<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[] v1)
        {
            return v1.MapScalars(scalarProcessor.Tanh);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Abs<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Abs);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Sqrt<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Sqrt);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] SqrtOfAbs<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.SqrtOfAbs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Exp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Exp);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] LogE<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.LogE);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Log2<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Log2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Log10<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Log10);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Log<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1, T scalar)
        {
            return v1.MapScalars(s => scalarProcessor.Log(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Log<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, T[,] v1)
        {
            return v1.MapScalars(s => scalarProcessor.Log(scalar, s));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Power<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1, T scalar)
        {
            return v1.MapScalars(s => scalarProcessor.Power(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Power<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, T[,] v1)
        {
            return v1.MapScalars(s => scalarProcessor.Power(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Cos<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Cos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Sin<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Sin);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Tan<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Tan);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ArcCos<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.ArcCos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ArcSin<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.ArcSin);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ArcTan<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.ArcTan);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Cosh<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Cosh);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Sinh<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Sinh);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] Tanh<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T[,] v1)
        {
            return v1.MapScalars(scalarProcessor.Tanh);
        }
    }
}