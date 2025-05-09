using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;

public static class ScalarArrayUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarArrayRow<T> GetRow<T>(this T[,] scalarArray, int rowIndex)
    {
        return new ScalarArrayRow<T>(scalarArray, rowIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarArrayColumn<T> GetColumn<T>(this T[,] scalarArray, int colIndex)
    {
        return new ScalarArrayColumn<T>(scalarArray, colIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarArrayDiagonal<T> GetDiagonal<T>(this T[,] scalarArray)
    {
        return new ScalarArrayDiagonal<T>(scalarArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ScalarArrayRow<T>> GetRows<T>(this T[,] scalarArray)
    {
        return scalarArray
            .GetLength(1)
            .GetRange(i =>
                new ScalarArrayRow<T>(scalarArray, i)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ScalarArrayColumn<T>> GetColumns<T>(this T[,] scalarArray)
    {
        return scalarArray
            .GetLength(1)
            .GetRange(i =>
                new ScalarArrayColumn<T>(scalarArray, i)
            );
    }

    
    public static IReadOnlyList<T> MapScalars<T1, T>(this IReadOnlyList<T1> matrix1, Func<T1, T> scalarMapping)
    {
        var count = matrix1.Count;
        var newMatrix = new T[count];

        for (var i = 0; i < count; i++)
            newMatrix[i] = scalarMapping(matrix1[i]);

        return newMatrix;
    }

    public static IReadOnlyList<T> MapScalars<T1, T>(this IReadOnlyList<T1> matrix1, Func<int, T1, T> scalarMapping)
    {
        var count = matrix1.Count;
        var newMatrix = new T[count];

        for (var i = 0; i < count; i++)
            newMatrix[i] = scalarMapping(i, matrix1[i]);

        return newMatrix;
    }

    public static IReadOnlyList<T> MapScalars<T>(this IReadOnlyList<T> matrix1, IReadOnlyList<T> matrix2, T initScalar, Func<T, T, T> scalarMapping)
    {
        var count1 = matrix1.Count;
        var count2 = matrix2.Count;
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

    public static IReadOnlyList<T> MapScalars<T1, T2, T>(this IReadOnlyList<T1> matrix1, IReadOnlyList<T2> matrix2, Func<T1> initFunc1, Func<T2> initFunc2, Func<T1, T2, T> scalarMapping)
    {
        var count1 = matrix1.Count;
        var count2 = matrix2.Count;
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
    

    public static T[] MapScalars<T1, T>(this T1[] matrix1, Func<T1, T> scalarMapping)
    {
        var count = matrix1.Length;
        var newMatrix = new T[count];

        for (var i = 0; i < count; i++)
            newMatrix[i] = scalarMapping(matrix1[i]);

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
}