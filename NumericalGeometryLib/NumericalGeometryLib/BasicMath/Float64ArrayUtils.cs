using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.Extensions;

namespace NumericalGeometryLib.BasicMath;

public static class Float64ArrayUtils
{
    public static bool IsVectorNearOrthonormalWith(this double[] u, double[] v, double epsilon = 1e-12)
    {
        var uuDot = 0d;
        var vvDot = 0d;
        var uvDot = 0d;

        for (var i = 0; i < u.Length; i++)
        {
            uuDot += u[i].Square();
            vvDot += v[i].Square();
            uvDot += u[i] * v[i];
        }

        return uvDot.IsNearZero(epsilon) &&
               uuDot.IsNearOne(epsilon) &&
               vvDot.IsNearOne(epsilon);
    }

    public static bool IsVectorNearOrthonormalWithUnit(this double[] u, double[] v, double epsilon = 1e-12)
    {
        var uuDot = 0d;
        var uvDot = 0d;

        for (var i = 0; i < u.Length; i++)
        {
            uuDot += u[i].Square();
            uvDot += u[i] * v[i];
        }

        return uvDot.IsNearZero(epsilon) &&
               uuDot.IsNearOne(epsilon);
    }

    public static bool IsVectorNearParallelTo(this double[] u, double[] v, double epsilon = 1e-12)
    {
        var uuDot = 0d;
        var vvDot = 0d;
        var uvDot = 0d;

        for (var i = 0; i < u.Length; i++)
        {
            uuDot += u[i].Square();
            vvDot += v[i].Square();
            uvDot += u[i] * v[i];
        }

        var cosAngle = 
            uvDot / (uuDot * vvDot).Sqrt();

        return cosAngle.Abs().IsNearOne(epsilon);
    }
    
    public static bool IsVectorNearParallelToUnit(this double[] u, double[] v, double epsilon = 1e-12)
    {
        var uuDot = 0d;
        var uvDot = 0d;

        for (var i = 0; i < u.Length; i++)
        {
            uuDot += u[i].Square();
            uvDot += u[i] * v[i];
        }

        var cosAngle = 
            uvDot / uuDot.Sqrt();

        return cosAngle.Abs().IsNearOne(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsVectorNearOrthogonalTo(this double[] u, double[] v, double epsilon = 1e-12)
    {
        return u.VectorDot(v).IsNearZero(epsilon);
    }


    public static double Sum(this double[] vector)
    {
        var count = vector.Length;
        var sum = 0d;

        for (var i = 0; i < count; i++)
            sum += vector[i];

        return sum;
    }

    public static double Min(this double[] vector)
    {
        var count = vector.Length;
        var min = vector[0];

        for (var i = 1; i < count; i++)
            if (min > vector[i]) min = vector[i];

        return min;
    }

    public static double Max(this double[] vector)
    {
        var count = vector.Length;
        var max = vector[0];

        for (var i = 1; i < count; i++)
            if (max < vector[i]) max = vector[i];

        return max;
    }

    public static double[] SumRows(this double[,] matrix)
    {
        var rowCount = matrix.GetLength(0);
        var colCount = matrix.GetLength(1);

        var sumVector = new double[rowCount];

        for (var row = 0; row < rowCount; row++)
        {
            var sum = 0d;

            for (var col = 0; col < colCount; col++)
                sum += matrix[row, col];

            sumVector[row] = sum;
        }

        return sumVector;
    }

    public static double[] SumColumns(this double[,] matrix)
    {
        var rowCount = matrix.GetLength(0);
        var colCount = matrix.GetLength(1);

        var sumVector = new double[colCount];

        for (var col = 0; col < colCount; col++)
        {
            var sum = 0d;

            for (var row = 0; row < rowCount; row++)
                sum += matrix[row, col];

            sumVector[col] = sum;
        }

        return sumVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetVectorNorm(this IEnumerable<double> vector)
    {
        return vector.Sum(s => s * s).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetVectorNormSquared(this IEnumerable<double> vector)
    {
        return vector.Sum(s => s * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] VectorNegative(this IEnumerable<double> vector)
    {
        return vector.Select(s => -s).ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] VectorMapScalarsInPlace(this double[] vector, Func<double, double> scalarMapping)
    {
        for (var i = 0; i < vector.Length; i++)
            vector[i] = scalarMapping(vector[i]);

        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] VectorMapScalarsInPlace(this double[] vector, Func<int, double, double> scalarMapping)
    {
        for (var i = 0; i < vector.Length; i++)
            vector[i] = scalarMapping(i, vector[i]);

        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] VectorTimesInPlace(this double[] vector, double scalar)
    {
        for (var i = 0; i < vector.Length; i++)
            vector[i] *= scalar;

        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] VectorTimes(this IEnumerable<double> vector, double scalar)
    {
        return vector.Select(s => s * scalar).ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] VectorDivideInPlace(this double[] vector, double scalar)
    {
        scalar = 1d / scalar;

        for (var i = 0; i < vector.Length; i++)
            vector[i] *= scalar;

        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] VectorDivide(this IEnumerable<double> vector, double scalar)
    {
        scalar = 1d / scalar;

        return vector.Select(s => s * scalar).ToArray();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] VectorNormalizeInPlace(this double[] vector)
    {
        var scalar = 1d / vector.GetVectorNorm();

        for (var i = 0; i < vector.Length; i++)
            vector[i] *= scalar;

        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] VectorAddInPlace(this double[] vector1, IEnumerable<double> vector2)
    {
        var i = 0;
        foreach (var scalar in vector2.Take(vector1.Length))
        {
            vector1[i] += scalar;

            i++;
        }

        return vector1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] VectorAdd(this IReadOnlyList<double> vector1, IReadOnlyList<double> vector2)
    {
        Debug.Assert(vector1.Count == vector2.Count);

        var vector = new double[vector1.Count];

        for (var i = 0; i < vector1.Count; i++)
            vector[i] = vector1[i] + vector2[i];

        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] VectorSubtractInPlace(this double[] vector1, IEnumerable<double> vector2)
    {
        var i = 0;
        foreach (var scalar in vector2.Take(vector1.Length))
        {
            vector1[i] -= scalar;

            i++;
        }

        return vector1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] VectorSubtract(this IReadOnlyList<double> vector1, IReadOnlyList<double> vector2)
    {
        Debug.Assert(vector1.Count == vector2.Count);

        var vector = new double[vector1.Count];

        for (var i = 0; i < vector1.Count; i++)
            vector[i] = vector1[i] - vector2[i];

        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double VectorDot(this double[] vector1, double[] vector2)
    {
        var n = Math.Min(vector1.Length, vector2.Length);
        var d = 0d;

        for (var i = 0; i < n; i++)
            d += vector1[i] * vector2[i];

        return d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> VectorDot(this double[] vector1, double[] vector2, double[] vector3)
    {
        var n = Math.Min(vector1.Length, vector2.Length);
        var d1 = 0d;
        var d2 = 0d;

        for (var i = 0; i < n; i++)
        {
            var scalar = vector1[i];

            d1 += scalar * vector2[i];
            d2 += scalar * vector3[i];
        }

        return new Pair<double>(d1, d2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetVectorNorm(this double[,] array)
    {
        return array.GetItems().Sum(s => s * s).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetVectorNormSquared(this double[,] array)
    {
        return array.GetItems().Sum(s => s * s);
    }


    public static double[] MatrixProduct(this double[,] matrix1, IReadOnlyList<double> matrix2)
    {
        var m = matrix1.GetLength(0);
        var n = matrix1.GetLength(1);

        if (n != matrix2.Count)
            throw new InvalidOperationException();

        var matrix = new double[m];

        for (var i = 0; i < m; i++)
        {
            var d = 0d;

            for (var j = 0; j < n; j++)
                d += matrix1[i, j] * matrix2[j];

            matrix[i] = d;
        }

        return matrix;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Matrix4x4 ToMatrix4x4(this double[,] array)
    {
        return new Matrix4x4(
            (float)array[0, 0], (float)array[0, 1], (float)array[0, 2], (float)array[0, 3],
            (float)array[1, 0], (float)array[1, 1], (float)array[1, 2], (float)array[1, 3],
            (float)array[2, 0], (float)array[2, 1], (float)array[2, 2], (float)array[2, 3],
            (float)array[3, 0], (float)array[3, 1], (float)array[3, 2], (float)array[3, 3]
        );
    }


    
    /// <summary>
    /// See the paper "Generalized Clarke Components for Poly-phase Networks", 1969
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    private static double[,] CreateClarkeRotationArrayOdd(int size)
    {
        var clarkeArray = new double[size, size];

        var s = (2d / size).Sqrt();
        //$"Sqrt[2 / {m}]";

        // m is odd, fill all rows except the last
        var n = (size - 1) / 2;
        for (var k = 0; k < n; k++)
        {
            var rowIndex1 = 2 * k;
            var rowIndex2 = 2 * k + 1;

            clarkeArray[rowIndex1, 0] = s;
            clarkeArray[rowIndex2, 0] = 0d;

            for (var colIndex = 1; colIndex < size; colIndex++)
            {
                var angle = 2d * Math.PI * (k + 1) * colIndex / size;
                // $"2 * Pi * {k + 1} * {i} / {m}";

                var cosAngle = s * Math.Cos(angle); // $"{s} * Cos[{angle}]";
                var sinAngle = s * Math.Sin(angle); // $"{s} * Sin[{angle}]";

                clarkeArray[rowIndex1, colIndex] = cosAngle;
                clarkeArray[rowIndex2, colIndex] = sinAngle;
            }
        }

        //Fill the last column
        var v = 1d / Math.Sqrt(size);
        // $"1 / Sqrt[{m}]";

        for (var colIndex = 0; colIndex < size; colIndex++)
            clarkeArray[size - 1, colIndex] = v;

        return clarkeArray;
    }

    /// <summary>
    /// See the paper "Generalized Clarke Components for Poly-phase Networks", 1969
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    private static double[,] CreateClarkeRotationArrayEven(int size)
    {
        var clarkeArray = new double[size, size];

        var s = Math.Sqrt(2d / size);
        //$"Sqrt[2 / {m}]";

        //m is even, fill all rows except the last two
        var n = (size - 1) / 2;
        for (var k = 0; k < n; k++)
        {
            var rowIndex1 = 2 * k;
            var rowIndex2 = 2 * k + 1;

            clarkeArray[rowIndex1, 0] = s;
            clarkeArray[rowIndex2, 0] = 0d;

            for (var colIndex = 1; colIndex < size; colIndex++)
            {
                var angle = 2d * Math.PI * (k + 1) * colIndex / size;
                // $"2 * Pi * {k + 1} * {i} / {m}";

                var cosAngle = s * Math.Cos(angle); // $"{s} * Cos[{angle}]";
                var sinAngle = s * Math.Sin(angle); // $"{s} * Sin[{angle}]";

                clarkeArray[rowIndex1, colIndex] = cosAngle;
                clarkeArray[rowIndex2, colIndex] = sinAngle;
            }
        }

        //Fill the last two rows
        var v0 = 1d / Math.Sqrt(size);
        // $"1 / Sqrt[{m}]";

        var v1 = -v0;
        // $"-1 / Sqrt[{m}]";

        for (var colIndex = 0; colIndex < size; colIndex++)
        {
            clarkeArray[size - 2, colIndex] = colIndex % 2 == 0 ? v0 : v1;
            clarkeArray[size - 1, colIndex] = v0;
        }

        return clarkeArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] CreateClarkeRotationArray(int size)
    {
        return size % 2 == 0
            ? CreateClarkeRotationArrayEven(size)
            : CreateClarkeRotationArrayOdd(size);
    }


    public static double[,] CreateCirculantColumnArray(IReadOnlyList<double> column)
    {
        var n = column.Count;
        var array = new double[n, n];

        for (var j = 0; j < n; j++)
        for (var i = 0; i < n; i++)
            array[i, j] = column[(i - j).Mod(n)];

        return array;
    }
}