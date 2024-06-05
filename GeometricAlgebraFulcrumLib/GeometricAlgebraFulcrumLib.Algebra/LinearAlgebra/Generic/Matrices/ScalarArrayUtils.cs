using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Matrices;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Matrices;

public static class ScalarArrayUtils
{
    public static IReadOnlyList<double> CombineRows(this IScalarProcessor<double> scalarProcessor, double[,] scalarArray, IReadOnlyList<double> scalarList, Func<double, IReadOnlyList<double>, IReadOnlyList<double>> scalingFunc, Func<IReadOnlyList<double>, IReadOnlyList<double>, IReadOnlyList<double>> reducingFunc)
    {
        var colCount = scalarArray.GetLength(0);
        var rowCount = scalarArray.GetLength(1);

        IReadOnlyList<double> vector = new RepeatedItemReadOnlyList<double>(
            scalarProcessor.ZeroValue,
            colCount
        );

        rowCount = Math.Min(
            scalarList.Count,
            rowCount
        );

        for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            var scalingFactor = scalarList[rowIndex];
            var rowVector = scalarArray.GetRow(rowIndex);
            var scaledVector = scalingFunc(scalingFactor, rowVector);

            vector = reducingFunc(vector, scaledVector);
        }

        return vector;
    }

    public static IReadOnlyList<double> CombineRows(this IScalarProcessor<double> scalarProcessor, double[,] scalarArray, IEnumerable<KeyValuePair<int, double>> rowIndexScalarRecords, Func<double, IReadOnlyList<double>, IReadOnlyList<double>> scalingFunc, Func<IReadOnlyList<double>, IReadOnlyList<double>, IReadOnlyList<double>> reducingFunc)
    {
        var colCount = scalarArray.GetLength(0);
        var rowCount = scalarArray.GetLength(1);

        IReadOnlyList<double> vector = new RepeatedItemReadOnlyList<double>(
            scalarProcessor.ZeroValue,
            colCount
        );

        foreach (var (rowIndex, scalingFactor) in rowIndexScalarRecords)
        {
            if (rowIndex >= rowCount) continue;

            var rowVector = scalarArray.GetRow(rowIndex);
            var scaledVector = scalingFunc(scalingFactor, rowVector);

            vector = reducingFunc(vector, scaledVector);
        }

        return vector;
    }

    public static IReadOnlyList<double> CombineColumns(this IScalarProcessor<double> scalarProcessor, double[,] scalarArray, IReadOnlyList<double> scalarList, Func<double, IReadOnlyList<double>, IReadOnlyList<double>> scalingFunc, Func<IReadOnlyList<double>, IReadOnlyList<double>, IReadOnlyList<double>> reducingFunc)
    {
        var colCount = scalarArray.GetLength(0);
        var rowCount = scalarArray.GetLength(1);

        IReadOnlyList<double> vector = new RepeatedItemReadOnlyList<double>(
            scalarProcessor.ZeroValue,
            rowCount
        );

        colCount = Math.Min(
            scalarList.Count,
            colCount
        );

        for (var columnIndex = 0; columnIndex < colCount; columnIndex++)
        {
            var scalingFactor = scalarList[columnIndex];
            var columnVector = scalarArray.GetColumn(columnIndex);
            var scaledVector = scalingFunc(scalingFactor, columnVector);

            vector = reducingFunc(vector, scaledVector);
        }

        return vector;
    }

    public static IReadOnlyList<double> CombineColumns(this IScalarProcessor<double> scalarProcessor, double[,] scalarArray, IEnumerable<KeyValuePair<int, double>> colIndexScalarRecords, Func<double, IReadOnlyList<double>, IReadOnlyList<double>> scalingFunc, Func<IReadOnlyList<double>, IReadOnlyList<double>, IReadOnlyList<double>> reducingFunc)
    {
        var colCount = scalarArray.GetLength(0);
        var rowCount = scalarArray.GetLength(1);

        IReadOnlyList<double> vector = new RepeatedItemReadOnlyList<double>(
            scalarProcessor.ZeroValue,
            rowCount
        );

        foreach (var (columnIndex, scalingFactor) in colIndexScalarRecords)
        {
            if (columnIndex >= colCount) continue;

            var columnVector = scalarArray.GetColumn(columnIndex);
            var scaledVector = scalingFunc(scalingFactor, columnVector);

            vector = reducingFunc(vector, scaledVector);
        }

        return vector;
    }

    
    public static IReadOnlyList<T> CombineRows<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray, IReadOnlyList<T> scalarList, Func<T, IReadOnlyList<T>, IReadOnlyList<T>> scalingFunc, Func<IReadOnlyList<T>, IReadOnlyList<T>, IReadOnlyList<T>> reducingFunc)
    {
        var colCount = scalarArray.GetLength(0);
        var rowCount = scalarArray.GetLength(1);

        IReadOnlyList<T> vector = new RepeatedItemReadOnlyList<T>(
            scalarProcessor.ZeroValue,
            colCount
        );

        rowCount = Math.Min(
            scalarList.Count,
            rowCount
        );

        for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            var scalingFactor = scalarList[rowIndex];
            var rowVector = scalarArray.GetRow(rowIndex);
            var scaledVector = scalingFunc(scalingFactor, rowVector);

            vector = reducingFunc(vector, scaledVector);
        }

        return vector;
    }

    public static IReadOnlyList<T> CombineRows<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray, IEnumerable<KeyValuePair<int, T>> rowIndexScalarRecords, Func<T, IReadOnlyList<T>, IReadOnlyList<T>> scalingFunc, Func<IReadOnlyList<T>, IReadOnlyList<T>, IReadOnlyList<T>> reducingFunc)
    {
        var colCount = scalarArray.GetLength(0);
        var rowCount = scalarArray.GetLength(1);

        IReadOnlyList<T> vector = new RepeatedItemReadOnlyList<T>(
            scalarProcessor.ZeroValue,
            colCount
        );

        foreach (var (rowIndex, scalingFactor) in rowIndexScalarRecords)
        {
            if (rowIndex >= rowCount) continue;

            var rowVector = scalarArray.GetRow(rowIndex);
            var scaledVector = scalingFunc(scalingFactor, rowVector);

            vector = reducingFunc(vector, scaledVector);
        }

        return vector;
    }

    public static IReadOnlyList<T> CombineColumns<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray, IReadOnlyList<T> scalarList, Func<T, IReadOnlyList<T>, IReadOnlyList<T>> scalingFunc, Func<IReadOnlyList<T>, IReadOnlyList<T>, IReadOnlyList<T>> reducingFunc)
    {
        var colCount = scalarArray.GetLength(0);
        var rowCount = scalarArray.GetLength(1);

        IReadOnlyList<T> vector = new RepeatedItemReadOnlyList<T>(
            scalarProcessor.ZeroValue,
            rowCount
        );

        colCount = Math.Min(
            scalarList.Count,
            colCount
        );

        for (var columnIndex = 0; columnIndex < colCount; columnIndex++)
        {
            var scalingFactor = scalarList[columnIndex];
            var columnVector = scalarArray.GetColumn(columnIndex);
            var scaledVector = scalingFunc(scalingFactor, columnVector);

            vector = reducingFunc(vector, scaledVector);
        }

        return vector;
    }

    public static IReadOnlyList<T> CombineColumns<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray, IEnumerable<KeyValuePair<int, T>> colIndexScalarRecords, Func<T, IReadOnlyList<T>, IReadOnlyList<T>> scalingFunc, Func<IReadOnlyList<T>, IReadOnlyList<T>, IReadOnlyList<T>> reducingFunc)
    {
        var colCount = scalarArray.GetLength(0);
        var rowCount = scalarArray.GetLength(1);

        IReadOnlyList<T> vector = new RepeatedItemReadOnlyList<T>(
            scalarProcessor.ZeroValue,
            rowCount
        );

        foreach (var (columnIndex, scalingFactor) in colIndexScalarRecords)
        {
            if (columnIndex >= colCount) continue;

            var columnVector = scalarArray.GetColumn(columnIndex);
            var scaledVector = scalingFunc(scalingFactor, columnVector);

            vector = reducingFunc(vector, scaledVector);
        }

        return vector;
    }


    public static T[,] CreateClarkeRotationArray3D<T>(IScalarProcessor<T> processor)
    {
        var clarkeArray = new T[3, 3];

        clarkeArray[0, 0] = processor.OneValue;
        clarkeArray[0, 1] = processor.Rational(-1, 2).ScalarValue;
        clarkeArray[0, 2] = processor.Rational(-1, 2).ScalarValue;

        clarkeArray[1, 0] = processor.ZeroValue;
        clarkeArray[1, 1] = (processor.Sqrt(3) / 2).ScalarValue;
        clarkeArray[1, 2] = (processor.Sqrt(3) / -2).ScalarValue;

        clarkeArray[2, 0] = processor.RationalSqrt(1, 2).ScalarValue;
        clarkeArray[2, 1] = processor.RationalSqrt(1, 2).ScalarValue;
        clarkeArray[2, 2] = processor.RationalSqrt(1, 2).ScalarValue;

        var scalar = processor.RationalSqrt(2, 3).ScalarValue;

        return processor.Times(scalar, clarkeArray);
    }

    public static T[,] CreateClarkeRotationArray5D<T>(IScalarProcessor<T> processor)
    {
        var clarkeArray = new T[5, 5];

        clarkeArray[0, 0] = processor.OneValue;
        clarkeArray[0, 1] = processor.PiRatioCos(2, 5).ScalarValue;
        clarkeArray[0, 2] = processor.PiRatioCos(4, 5).ScalarValue;
        clarkeArray[0, 3] = processor.PiRatioCos(6, 5).ScalarValue;
        clarkeArray[0, 4] = processor.PiRatioCos(8, 5).ScalarValue;

        clarkeArray[1, 0] = processor.ZeroValue;
        clarkeArray[1, 1] = processor.PiRatioSin(2, 5).ScalarValue;
        clarkeArray[1, 2] = processor.PiRatioSin(4, 5).ScalarValue;
        clarkeArray[1, 3] = processor.PiRatioSin(6, 5).ScalarValue;
        clarkeArray[1, 4] = processor.PiRatioSin(8, 5).ScalarValue;

        clarkeArray[2, 0] = processor.OneValue;
        clarkeArray[2, 1] = processor.PiRatioCos(4, 5).ScalarValue;
        clarkeArray[2, 2] = processor.PiRatioCos(8, 5).ScalarValue;
        clarkeArray[2, 3] = processor.PiRatioCos(12, 5).ScalarValue;
        clarkeArray[2, 4] = processor.PiRatioCos(16, 5).ScalarValue;

        clarkeArray[3, 0] = processor.ZeroValue;
        clarkeArray[3, 1] = processor.PiRatioSin(4, 5).ScalarValue;
        clarkeArray[3, 2] = processor.PiRatioSin(8, 5).ScalarValue;
        clarkeArray[3, 3] = processor.PiRatioSin(12, 5).ScalarValue;
        clarkeArray[3, 4] = processor.PiRatioSin(16, 5).ScalarValue;

        clarkeArray[4, 0] = processor.RationalSqrt(1, 2).ScalarValue;
        clarkeArray[4, 1] = processor.RationalSqrt(1, 2).ScalarValue;
        clarkeArray[4, 2] = processor.RationalSqrt(1, 2).ScalarValue;
        clarkeArray[4, 3] = processor.RationalSqrt(1, 2).ScalarValue;
        clarkeArray[4, 4] = processor.RationalSqrt(1, 2).ScalarValue;

        var scalar = processor.RationalSqrt(2, 5).ScalarValue;

        return processor.Times(scalar, clarkeArray);
    }

    public static T[,] CreateClarkeRotationArray6D<T>(IScalarProcessor<T> processor)
    {
        var clarkeArray = new T[6, 6];

        clarkeArray[0, 0] = processor.OneValue;
        clarkeArray[0, 1] = processor.Rational(-1, 2).ScalarValue;
        clarkeArray[0, 2] = processor.Rational(-1, 2).ScalarValue;
        clarkeArray[0, 3] = processor.Rational(1, 2).ScalarValue;
        clarkeArray[0, 4] = processor.MinusOneValue;
        clarkeArray[0, 5] = processor.Rational(1, 2).ScalarValue;

        clarkeArray[1, 0] = processor.ZeroValue;
        clarkeArray[1, 1] = processor.RationalSqrt(3, 4).ScalarValue;
        clarkeArray[1, 2] = processor.RationalSqrt(3, 4).Negative().ScalarValue;
        clarkeArray[1, 3] = processor.RationalSqrt(3, 4).ScalarValue;
        clarkeArray[1, 4] = processor.ZeroValue;
        clarkeArray[1, 5] = processor.RationalSqrt(3, 4).Negative().ScalarValue;

        clarkeArray[2, 0] = processor.OneValue;
        clarkeArray[2, 1] = processor.Rational(-1, 2).ScalarValue;
        clarkeArray[2, 2] = processor.Rational(-1, 2).ScalarValue;
        clarkeArray[2, 3] = processor.Rational(-1, 2).ScalarValue;
        clarkeArray[2, 4] = processor.OneValue;
        clarkeArray[2, 5] = processor.Rational(-1, 2).ScalarValue;

        clarkeArray[3, 0] = processor.ZeroValue;
        clarkeArray[3, 1] = processor.RationalSqrt(3, 4).Negative().ScalarValue;
        clarkeArray[3, 2] = processor.RationalSqrt(3, 4).ScalarValue;
        clarkeArray[3, 3] = processor.RationalSqrt(3, 4).ScalarValue;
        clarkeArray[3, 4] = processor.ZeroValue;
        clarkeArray[3, 5] = processor.RationalSqrt(3, 4).Negative().ScalarValue;

        clarkeArray[4, 0] = processor.RationalSqrt(1, 2).ScalarValue;
        clarkeArray[4, 1] = processor.RationalSqrt(1, 2).ScalarValue;
        clarkeArray[4, 2] = processor.RationalSqrt(1, 2).ScalarValue;
        clarkeArray[4, 3] = processor.RationalSqrt(1, 2).ScalarValue;
        clarkeArray[4, 4] = processor.RationalSqrt(1, 2).ScalarValue;
        clarkeArray[4, 5] = processor.RationalSqrt(1, 2).ScalarValue;

        clarkeArray[5, 0] = processor.RationalSqrt(1, 2).ScalarValue;
        clarkeArray[5, 1] = processor.RationalSqrt(1, 2).ScalarValue;
        clarkeArray[5, 2] = processor.RationalSqrt(1, 2).ScalarValue;
        clarkeArray[5, 3] = processor.RationalSqrt(1, 2).Negative().ScalarValue;
        clarkeArray[5, 4] = processor.RationalSqrt(1, 2).Negative().ScalarValue;
        clarkeArray[5, 5] = processor.RationalSqrt(1, 2).Negative().ScalarValue;

        var scalar = processor.RationalSqrt(2, 6).ScalarValue;

        return processor.Times(scalar, clarkeArray);
    }

    /// <summary>
    /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
    /// </summary>
    /// <param name="processor"></param>
    /// <param name="vectorsCount"></param>
    /// <returns></returns>
    private static T[,] CreateClarkeRotationArrayOdd<T>(IScalarProcessor<T> processor, int vectorsCount)
    {
        var clarkeArray = new T[vectorsCount, vectorsCount];

        var m = vectorsCount;
        var s = (processor.ScalarFromNumber(2) / processor.ScalarFromNumber(m)).Sqrt(); //$"Sqrt[2 / {m}]";

        // m is odd, fill all rows except the last
        var n = (m - 1) / 2;
        for (var k = 0; k < n; k++)
        {
            var rowIndex1 = 2 * k;
            var rowIndex2 = 2 * k + 1;

            clarkeArray[rowIndex1, 0] = s.ScalarValue;
            clarkeArray[rowIndex2, 0] = processor.ZeroValue;
            
            for (var colIndex = 1; colIndex < m; colIndex++)
            {
                var angle =
                    processor.PiRatioToPolarAngle(
                        2 * (k + 1) * colIndex,
                        m
                    ); // $"2 * Pi * {k + 1} * {i} / {m}";

                var cosAngle = s * angle.Cos(); // $"{s} * Cos[{angle}]";
                var sinAngle = s * angle.Sin(); // $"{s} * Sin[{angle}]";

                clarkeArray[rowIndex1, colIndex] = cosAngle.ScalarValue;
                clarkeArray[rowIndex2, colIndex] = sinAngle.ScalarValue;
            }
        }

        //Fill the last column
        var v = 1 / processor.Sqrt(m); // $"1 / Sqrt[{m}]";

        for (var colIndex = 0; colIndex < m; colIndex++)
            clarkeArray[m - 1, colIndex] = v.ScalarValue;

        return clarkeArray;
    }

    /// <summary>
    /// See the paper "Generalized Clarke Components for Polyphase Networks", 1969
    /// </summary>
    /// <param name="processor"></param>
    /// <param name="vectorsCount"></param>
    /// <returns></returns>
    private static T[,] CreateClarkeRotationArrayEven<T>(IScalarProcessor<T> processor, int vectorsCount)
    {
        var clarkeArray = new T[vectorsCount, vectorsCount];

        var m = vectorsCount;
        var s = processor.RationalSqrt(2, m); //$"Sqrt[2 / {m}]";

        //m is even, fill all rows except the last two
        var n = (m - 1) / 2;
        for (var k = 0; k < n; k++)
        {
            var rowIndex1 = 2 * k;
            var rowIndex2 = 2 * k + 1;

            clarkeArray[rowIndex1, 0] = s.ScalarValue;
            clarkeArray[rowIndex2, 0] = processor.ZeroValue;

            for (var colIndex = 1; colIndex < m; colIndex++)
            {
                var angle =
                    processor.PiRatioToPolarAngle(
                        2 * (k + 1) * colIndex,
                        m
                    ); // $"2 * Pi * {k + 1} * {i} / {m}";

                var cosAngle = processor.Times(
                    s,
                    angle.Cos()
                ); // $"{s} * Cos[{angle}]";

                var sinAngle = processor.Times(
                    s,
                    angle.Sin()
                ); // $"{s} * Sin[{angle}]";

                clarkeArray[rowIndex1, colIndex] = cosAngle.ScalarValue;
                clarkeArray[rowIndex2, colIndex] = sinAngle.ScalarValue;
            }
        }

        //Fill the last two rows
        var v0 = processor.One / processor.Sqrt(m); // $"1 / Sqrt[{m}]";

        var v1 = processor.MinusOne / processor.Sqrt(m); // $"-1 / Sqrt[{m}]";

        for (var colIndex = 0; colIndex < m; colIndex++)
        {
            clarkeArray[m - 2, colIndex] = (colIndex % 2 == 0 ? v0 : v1).ScalarValue;
            clarkeArray[m - 1, colIndex] = v0.ScalarValue;
        }

        return clarkeArray;
    }

    public static T[,] CreateClarkeRotationArray<T>(this IScalarProcessor<T> processor, int vectorsCount)
    {
        return vectorsCount % 2 == 0
            ? CreateClarkeRotationArrayEven(processor, vectorsCount)
            : CreateClarkeRotationArrayOdd(processor, vectorsCount);
    }


    public static T[,] MapScalars<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T scalar2, Func<T, T, T> scalarMapping)
    {
        scalar2 ??= scalarProcessor.ZeroValue;

        var rowsCount = scalarArray1.GetLength(0);
        var colsCount = scalarArray1.GetLength(1);

        var array = new T[rowsCount, colsCount];

        for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                array[i, j] = scalarMapping(
                    scalarArray1[i, j] ?? scalarProcessor.ZeroValue,
                    scalar2
                );
            }

        return array;
    }

    public static T[,] MapScalars<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarArray2, Func<T, T, T> scalarMapping)
    {
        scalar1 ??= scalarProcessor.ZeroValue;

        var rowsCount = scalarArray2.GetLength(0);
        var colsCount = scalarArray2.GetLength(1);

        var array = new T[rowsCount, colsCount];

        for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                array[i, j] = scalarMapping(
                    scalar1,
                    scalarArray2[i, j] ?? scalarProcessor.ZeroValue
                );
            }

        return array;
    }

    public static T[,] MapScalars<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T[,] scalarArray2, Func<T, T, T> scalarMapping)
    {
        var rowsCount = scalarArray1.GetLength(0);
        var colsCount = scalarArray1.GetLength(1);

        var array = new T[rowsCount, colsCount];

        for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                array[i, j] = scalarMapping(
                    scalarArray1[i, j] ?? scalarProcessor.ZeroValue,
                    scalarArray2[i, j] ?? scalarProcessor.ZeroValue
                );
            }

        return array;
    }

    public static T[,] MapNotZeroScalars<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray, Func<T, T> scalarMapping)
    {
        var rowsCount = scalarArray.GetLength(0);
        var colsCount = scalarArray.GetLength(1);

        var array = new T[rowsCount, colsCount];

        for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                var scalar = scalarArray[i, j];

                if (ReferenceEquals(scalar, null) || scalarProcessor.IsZero(scalar))
                {
                    array[i, j] = scalarProcessor.ZeroValue;

                    continue;
                }

                array[i, j] = scalarMapping(
                    scalarArray[i, j]
                );
            }

        return array;
    }

    public static T[,] MapScalars<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray, Func<T, T> scalarMapping)
    {
        var rowsCount = scalarArray.GetLength(0);
        var colsCount = scalarArray.GetLength(1);

        var array = new T[rowsCount, colsCount];

        for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < colsCount; j++)
            {
                array[i, j] = scalarMapping(
                    scalarArray[i, j] ?? scalarProcessor.ZeroValue
                );
            }

        return array;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Add<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T[,] scalarArray2)
    {
        return scalarProcessor.MapScalars(
            scalarArray1,
            scalarArray2,
            (a, b) => scalarProcessor.Add(a, b).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarArray2)
    {
        return scalarProcessor.MapScalars(
            scalar1,
            scalarArray2,
            (a, b) => scalarProcessor.Add(a, b).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Add<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T scalar2)
    {
        return scalarProcessor.MapScalars(
            scalarArray1,
            scalar2,
            (scalar1, scalar3) => scalarProcessor.Add(scalar1, scalar3).ScalarValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Subtract<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T[,] scalarArray2)
    {
        return scalarProcessor.MapScalars(
            scalarArray1,
            scalarArray2,
            (scalar1, scalar2) => scalarProcessor.Subtract(scalar1, scalar2).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarArray2)
    {
        return scalarProcessor.MapScalars(
            scalar1,
            scalarArray2,
            (a, b) => scalarProcessor.Subtract(a, b).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Subtract<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T scalar2)
    {
        return scalarProcessor.MapScalars(
            scalarArray1,
            scalar2,
            (scalar1, scalar3) => scalarProcessor.Subtract(scalar1, scalar3).ScalarValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Times<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T[,] scalarArray2)
    {
        return scalarProcessor.MapScalars(
            scalarArray1,
            scalarArray2,
            (scalar1, scalar2) => scalarProcessor.Times(scalar1, scalar2).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Times<T>(this IScalarProcessor<T> scalarProcessor, params T[][,] scalarArrays)
    {
        return scalarArrays.Skip(1).Aggregate(
            scalarArrays[0],
            scalarProcessor.Times
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Times<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T[,]> scalarArrays)
    {
        return scalarArrays.Skip(1).Aggregate(
            scalarArrays[0],
            scalarProcessor.Times
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarArray2)
    {
        return scalarProcessor.MapScalars(
            scalar1,
            scalarArray2,
            (a, b) => scalarProcessor.Times(a, b).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Times<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T scalar2)
    {
        return scalarProcessor.MapScalars(
            scalarArray1,
            scalar2,
            (scalar1, scalar3) => scalarProcessor.Times(scalar1, scalar3).ScalarValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Divide<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T[,] scalarArray2)
    {
        return scalarProcessor.MapScalars(
            scalarArray1,
            scalarArray2,
            (scalar1, scalar2) => scalarProcessor.Divide(scalar1, scalar2).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T[,] scalarArray2)
    {
        return scalarProcessor.MapScalars(
            scalar1,
            scalarArray2,
            (a, b) => scalarProcessor.Divide(a, b).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Divide<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray1, T scalar2)
    {
        return scalarProcessor.MapScalars(
            scalarArray1,
            scalar2,
            (scalar1, scalar3) => scalarProcessor.Divide(scalar1, scalar3).ScalarValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] NotZeroScalarsInverse<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
    {
        return scalarProcessor.MapNotZeroScalars(
            scalarArray,
            scalar => scalarProcessor.Inverse(scalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ScalarsNegative<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
    {
        return scalarProcessor.MapScalars(
            scalarArray,
            scalar => scalarProcessor.Negative(scalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ScalarsCos<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
    {
        return scalarProcessor.MapScalars(
            scalarArray,
            scalar => scalarProcessor.Cos(scalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ScalarsSin<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
    {
        return scalarProcessor.MapScalars(
            scalarArray,
            scalar => scalarProcessor.Sin(scalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ScalarsTan<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
    {
        return scalarProcessor.MapScalars(
            scalarArray,
            scalar => scalarProcessor.Tan(scalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ScalarsExp<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
    {
        return scalarProcessor.MapScalars(
            scalarArray,
            scalar => scalarProcessor.Exp(scalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ScalarsLog<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
    {
        return scalarProcessor.MapScalars(
            scalarArray,
            scalar => scalarProcessor.LogE(scalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ScalarsLog10<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
    {
        return scalarProcessor.MapScalars(
            scalarArray,
            scalar => scalarProcessor.Log10(scalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ScalarsLog2<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
    {
        return scalarProcessor.MapScalars(
            scalarArray,
            scalar => scalarProcessor.Log2(scalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ScalarsAbs<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
    {
        return scalarProcessor.MapScalars(
            scalarArray,
            scalar => scalarProcessor.Abs(scalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ScalarsSqrt<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
    {
        return scalarProcessor.MapScalars(
            scalarArray,
            scalar => scalarProcessor.Sqrt(scalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ScalarsSqrtOfAbs<T>(this IScalarProcessor<T> scalarProcessor, T[,] scalarArray)
    {
        return scalarProcessor.MapScalars(
            scalarArray,
            scalar => scalarProcessor.SqrtOfAbs(scalar).ScalarValue
        );
    }


    //public static VectorStorage<T> ColumnToVectorStorage<T>(this T[,] scalarArray, int colIndex, IScalarProcessor<T> scalarProcessor)
    //{
    //    var rowsCount = scalarArray.GetLength(0);

    //    var composer = scalarProcessor.CreateVectorStorageComposer(rowsCount);

    //    for (var i = 0; i < rowsCount; i++)
    //    {
    //        var scalar = scalarArray[i, colIndex] ?? scalarProcessor.ScalarZero;

    //        if (!scalarProcessor.IsZero(scalar))
    //            composer.SetTerm((ulong)i, scalar);
    //    }

    //    return composer.CreateVectorStorage();
    //}

    //public static VectorStorage<T> RowToVectorStorage<T>(this T[,] scalarArray, int rowIndex, IScalarProcessor<T> scalarProcessor)
    //{
    //    var colsCount = scalarArray.GetLength(1);

    //    var composer = scalarProcessor.CreateVectorStorageComposer(1);

    //    for (var j = 0; j < colsCount; j++)
    //    {
    //        var scalar = scalarArray[rowIndex, j] ?? scalarProcessor.ScalarZero;

    //        if (!scalarProcessor.IsZero(scalar))
    //            composer.SetTerm((ulong)j, scalar);
    //    }

    //    return composer.CreateVectorStorage();
    //}

    //public static Dictionary<ulong, VectorStorage<T>> ColumnsToVectorStoragesDictionary<T>(this T[,] scalarArray, IScalarProcessor<T> scalarProcessor)
    //{
    //    var rowsCount = scalarArray.GetLength(0);
    //    var colsCount = scalarArray.GetLength(1);

    //    var vectorsDictionary =
    //        new Dictionary<ulong, VectorStorage<T>>();

    //    for (var j = 0; j < colsCount; j++)
    //    {
    //        var composer = scalarProcessor.CreateVectorStorageComposer(1);

    //        for (var i = 0; i < rowsCount; i++)
    //        {
    //            var scalar = scalarArray[i, j] ?? scalarProcessor.ScalarZero;

    //            if (!scalarProcessor.IsZero(scalar))
    //                composer.SetTerm((ulong)i, scalar);
    //        }

    //        vectorsDictionary.Add((ulong)j, composer.CreateVectorStorage());
    //    }

    //    return vectorsDictionary;
    //}

    //public static VectorStorage<T>[] ColumnsToVectorStoragesArray<T>(this T[,] scalarArray, IScalarProcessor<T> scalarProcessor)
    //{
    //    var rowsCount = scalarArray.GetLength(0);
    //    var colsCount = scalarArray.GetLength(1);

    //    var vectorsArray =
    //        new VectorStorage<T>[colsCount];

    //    for (var j = 0; j < colsCount; j++)
    //    {
    //        var composer = scalarProcessor.CreateVectorStorageComposer(1);

    //        for (var i = 0; i < rowsCount; i++)
    //        {
    //            var scalar = scalarArray[i, j] ?? scalarProcessor.ScalarZero;

    //            if (!scalarProcessor.IsZero(scalar))
    //                composer.SetTerm((ulong)i, scalar);
    //        }

    //        vectorsArray[j] = composer.CreateVectorStorage();
    //    }

    //    return vectorsArray;
    //}

    //public static GaVector<T>[] ColumnsToVectorsArray<T>(this T[,] scalarArray, IScalarProcessor<T> scalarProcessor)
    //{
    //    var rowsCount = scalarArray.GetLength(0);
    //    var colsCount = scalarArray.GetLength(1);

    //    var vectorsArray =
    //        new GaVector<T>[colsCount];

    //    for (var j = 0; j < colsCount; j++)
    //    {
    //        var composer = scalarProcessor.CreateVectorStorageComposer(1);

    //        for (var i = 0; i < rowsCount; i++)
    //        {
    //            var scalar = scalarArray[i, j] ?? scalarProcessor.ScalarZero;

    //            if (!scalarProcessor.IsZero(scalar))
    //                composer.SetTerm((ulong)i, scalar);
    //        }

    //        vectorsArray[j] = composer.Vector();
    //    }

    //    return vectorsArray;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetScalar<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> matrix, int index)
    {
        return index >= 0 &&
               index < matrix.Count
            ? matrix[index] ?? scalarProcessor.ZeroValue
            : scalarProcessor.ZeroValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetScalar<T>(this IScalarProcessor<T> scalarProcessor, T[,] matrix, int rowIndex, int colIndex)
    {
        return rowIndex >= 0 &&
               colIndex >= 0 &&
               rowIndex < matrix.GetLength(0) &&
               colIndex < matrix.GetLength(1)
            ? matrix[rowIndex, colIndex] ?? scalarProcessor.ZeroValue
            : scalarProcessor.ZeroValue;
    }

    
    public static IReadOnlyList<T> MatrixProduct<T>(this IScalarProcessor<T> scalarProcessor, T[,] matrix1, IReadOnlyList<T> matrix2)
    {
        var m = matrix1.GetLength(0);
        var n = matrix1.GetLength(1);

        if (n != matrix2.Count)
            throw new InvalidOperationException();

        var matrix = new T[m];

        for (var i = 0; i < m; i++)
        {
            var d = scalarProcessor.ZeroValue;

            for (var j = 0; j < n; j++)
                d = scalarProcessor.Add(
                    d,
                    scalarProcessor.Times(
                        matrix1[i, j],
                        matrix2[j]
                    ).ScalarValue
                ).ScalarValue;

            matrix[i] = d;
        }

        return matrix;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] MatrixProduct<T>(this IScalarProcessor<T> scalarProcessor, T[,] matrix1, T[,] matrix2)
    {
        return matrix1.MapScalars(
            matrix2,
            scalarProcessor.ZeroValue,
            (accumulator, a, b) =>
                scalarProcessor.Add(accumulator, scalarProcessor.Times(a, b).ScalarValue).ScalarValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> Abs<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Abs(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> Sqrt<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Sqrt(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> SqrtOfAbs<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.SqrtOfAbs(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> Exp<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Exp(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> LogE<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.LogE(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> Log2<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Log2(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> Log10<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Log10(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> Log<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1, T scalar)
    {
        return v1.MapScalars(s => scalarProcessor.Log(s, scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> Log<T>(this IScalarProcessor<T> scalarProcessor, T scalar, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(s => scalarProcessor.Log(scalar, s).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> Power<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1, T scalar)
    {
        return v1.MapScalars(s => scalarProcessor.Power(s, scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(s => scalarProcessor.Power(scalar, s).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> Cos<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Cos(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> Sin<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Sin(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> Tan<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Tan(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> ArcCos<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.ArcCos(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> ArcSin<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.ArcSin(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> ArcTan<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.ArcTan(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> Cosh<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Cosh(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> Sinh<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Sinh(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<T> Tanh<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyList<T> v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Tanh(scalar).ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Abs<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Abs(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Sqrt<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Sqrt(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] SqrtOfAbs<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.SqrtOfAbs(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Exp<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Exp(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] LogE<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.LogE(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Log2<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Log2(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Log10<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Log10(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Log<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1, T scalar)
    {
        return v1.MapScalars(s => scalarProcessor.Log(s, scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Log<T>(this IScalarProcessor<T> scalarProcessor, T scalar, T[,] v1)
    {
        return v1.MapScalars(s => scalarProcessor.Log(scalar, s).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Power<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1, T scalar)
    {
        return v1.MapScalars(s => scalarProcessor.Power(s, scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar, T[,] v1)
    {
        return v1.MapScalars(s => scalarProcessor.Power(scalar, s).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Cos<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Cos(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Sin<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Sin(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Tan<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Tan(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ArcCos<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.ArcCos(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ArcSin<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.ArcSin(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ArcTan<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.ArcTan(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Cosh<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Cosh(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Sinh<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Sinh(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] Tanh<T>(this IScalarProcessor<T> scalarProcessor, T[,] v1)
    {
        return v1.MapScalars(scalar => scalarProcessor.Tanh(scalar).ScalarValue);
    }
}