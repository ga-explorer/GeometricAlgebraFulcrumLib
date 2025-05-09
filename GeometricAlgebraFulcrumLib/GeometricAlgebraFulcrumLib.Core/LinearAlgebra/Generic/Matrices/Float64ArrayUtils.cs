//using System.Diagnostics;
//using System.Numerics;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
//using GeometricAlgebraFulcrumLib.Core;
//using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
//using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars;

//namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Matrices;

//public static class Float64ArrayUtils
//{



//    public static bool IsVectorNearOrthonormalWith(this IReadOnlyList<double> u, IReadOnlyList<double> v, double zeroEpsilon = Float64Utils.ZeroEpsilon)
//    {
//        var uuDot = 0d;
//        var vvDot = 0d;
//        var uvDot = 0d;

//        for (var i = 0; i < u.Count; i++)
//        {
//            uuDot += u[i].Square();
//            vvDot += v[i].Square();
//            uvDot += u[i] * v[i];
//        }

//        return uvDot.IsNearZero(zeroEpsilon) &&
//               uuDot.IsNearOne(zeroEpsilon) &&
//               vvDot.IsNearOne(zeroEpsilon);
//    }

//    public static bool IsVectorNearOrthonormalWithUnit(this IReadOnlyList<double> u, IReadOnlyList<double> v, double zeroEpsilon = Float64Utils.ZeroEpsilon)
//    {
//        var uuDot = 0d;
//        var uvDot = 0d;

//        for (var i = 0; i < u.Count; i++)
//        {
//            uuDot += u[i].Square();
//            uvDot += u[i] * v[i];
//        }

//        return uvDot.IsNearZero(zeroEpsilon) &&
//               uuDot.IsNearOne(zeroEpsilon);
//    }

//    public static bool IsVectorNearParallelTo(this IReadOnlyList<double> u, IReadOnlyList<double> v, double zeroEpsilon = Float64Utils.ZeroEpsilon)
//    {
//        var uuDot = 0d;
//        var vvDot = 0d;
//        var uvDot = 0d;

//        for (var i = 0; i < u.Count; i++)
//        {
//            uuDot += u[i].Square();
//            vvDot += v[i].Square();
//            uvDot += u[i] * v[i];
//        }

//        var cosAngle =
//            uvDot / (uuDot * vvDot).Sqrt();

//        return cosAngle.Abs().IsNearOne(zeroEpsilon);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsVectorNearParallelTo(this IReadOnlyList<double> u, IReadOnlyDictionary<int, double> v, double zeroEpsilon = Float64Utils.ZeroEpsilon)
//    {
//        return u.VectorAngleCos(v).Abs().IsNearOne(zeroEpsilon);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsVectorNearParallelTo(this IReadOnlyDictionary<int, double> u, IReadOnlyList<double> v, double zeroEpsilon = Float64Utils.ZeroEpsilon)
//    {
//        return u.VectorAngleCos(v).Abs().IsNearOne(zeroEpsilon);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsVectorNearParallelTo(this IReadOnlyDictionary<int, double> u, IReadOnlyDictionary<int, double> v, double zeroEpsilon = Float64Utils.ZeroEpsilon)
//    {
//        return u.VectorAngleCos(v).Abs().IsNearOne(zeroEpsilon);
//    }

//    public static bool IsVectorNearParallelToUnit(this IReadOnlyList<double> u, IReadOnlyList<double> v, double zeroEpsilon = Float64Utils.ZeroEpsilon)
//    {
//        var uuDot = 0d;
//        var uvDot = 0d;

//        for (var i = 0; i < u.Count; i++)
//        {
//            uuDot += u[i].Square();
//            uvDot += u[i] * v[i];
//        }

//        var cosAngle =
//            uvDot / uuDot.Sqrt();

//        return cosAngle.Abs().IsNearOne(zeroEpsilon);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsVectorNearParallelToUnit(this IReadOnlyList<double> u, IReadOnlyDictionary<int, double> v, double zeroEpsilon = Float64Utils.ZeroEpsilon)
//    {
//        var uuDot = u.GetVectorNormSquared();
//        var uvDot = u.VectorDot(v);

//        var cosAngle = uvDot / uuDot.Sqrt();

//        return cosAngle.Abs().IsNearOne(zeroEpsilon);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsVectorNearParallelToUnit(this IReadOnlyDictionary<int, double> u, IReadOnlyList<double> v, double zeroEpsilon = Float64Utils.ZeroEpsilon)
//    {
//        var uuDot = u.GetVectorNormSquared();
//        var uvDot = u.VectorDot(v);

//        var cosAngle = uvDot / uuDot.Sqrt();

//        return cosAngle.Abs().IsNearOne(zeroEpsilon);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsVectorNearParallelToUnit(this IReadOnlyDictionary<int, double> u, IReadOnlyDictionary<int, double> v, double zeroEpsilon = Float64Utils.ZeroEpsilon)
//    {
//        var uuDot = u.GetVectorNormSquared();
//        var uvDot = u.VectorDot(v);

//        var cosAngle = uvDot / uuDot.Sqrt();

//        return cosAngle.Abs().IsNearOne(zeroEpsilon);
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsVectorNearOrthogonalTo(this IReadOnlyList<double> u, IReadOnlyList<double> v, double zeroEpsilon = Float64Utils.ZeroEpsilon)
//    {
//        return u.VectorDot(v).IsNearZero(zeroEpsilon);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsVectorNearOrthogonalTo(this IReadOnlyList<double> u, IReadOnlyDictionary<int, double> v, double zeroEpsilon = Float64Utils.ZeroEpsilon)
//    {
//        return u.VectorDot(v).IsNearZero(zeroEpsilon);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsVectorNearOrthogonalTo(this IReadOnlyDictionary<int, double> u, IReadOnlyList<double> v, double zeroEpsilon = Float64Utils.ZeroEpsilon)
//    {
//        return u.VectorDot(v).IsNearZero(zeroEpsilon);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsVectorNearOrthogonalTo(this IReadOnlyDictionary<int, double> u, IReadOnlyDictionary<int, double> v, double zeroEpsilon = Float64Utils.ZeroEpsilon)
//    {
//        return u.VectorDot(v).IsNearZero(zeroEpsilon);
//    }


//    public static double Sum(this IReadOnlyList<double> vector)
//    {
//        var count = vector.Count;
//        var sum = 0d;

//        for (var i = 0; i < count; i++)
//            sum += vector[i];

//        return sum;
//    }

//    public static double Min(this IReadOnlyList<double> vector)
//    {
//        var count = vector.Count;
//        var min = vector[0];

//        for (var i = 1; i < count; i++)
//            if (min > vector[i]) min = vector[i];

//        return min;
//    }

//    public static double Max(this IReadOnlyList<double> vector)
//    {
//        var count = vector.Count;
//        var max = vector[0];

//        for (var i = 1; i < count; i++)
//            if (max < vector[i]) max = vector[i];

//        return max;
//    }

//    public static double GetMedian(this IReadOnlyList<int> inputArray)
//    {
//        var length = inputArray.Count;
//        var copyArray = new int[length];

//        for (var i = 0; i < length; i++)
//            copyArray[i] = inputArray[i];

//        Array.Sort(copyArray);

//        return length % 2 == 0
//            ? copyArray[length / 2] + copyArray[length / 2 - 1] >> 1
//            : copyArray[(length - 1) / 2];
//    }

//    public static double GetMedian(this IReadOnlyList<double> inputArray)
//    {
//        var length = inputArray.Count;
//        var copyArray = new double[length];

//        for (var i = 0; i < length; i++)
//            copyArray[i] = inputArray[i];

//        Array.Sort(copyArray);

//        return length % 2 == 0
//            ? (copyArray[length / 2] + copyArray[length / 2 - 1]) / 2
//            : copyArray[(length - 1) / 2];
//    }

//    public static double[] SumRows(this double[,] matrix)
//    {
//        var rowCount = matrix.GetLength(0);
//        var colCount = matrix.GetLength(1);

//        var sumVector = new double[rowCount];

//        for (var row = 0; row < rowCount; row++)
//        {
//            var sum = 0d;

//            for (var col = 0; col < colCount; col++)
//                sum += matrix[row, col];

//            sumVector[row] = sum;
//        }

//        return sumVector;
//    }

//    public static double[] SumColumns(this double[,] matrix)
//    {
//        var rowCount = matrix.GetLength(0);
//        var colCount = matrix.GetLength(1);

//        var sumVector = new double[colCount];

//        for (var col = 0; col < colCount; col++)
//        {
//            var sum = 0d;

//            for (var row = 0; row < rowCount; row++)
//                sum += matrix[row, col];

//            sumVector[col] = sum;
//        }

//        return sumVector;
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double GetVectorNorm(this IEnumerable<double> vector)
//    {
//        return vector.Sum(s => s * s).Sqrt();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double GetVectorNorm(this IReadOnlyDictionary<int, double> vector)
//    {
//        return vector.Values.Sum(s => s * s).Sqrt();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double GetVectorNormSquared(this IEnumerable<double> vector)
//    {
//        return vector.Sum(s => s * s);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double GetVectorNormSquared(this IReadOnlyDictionary<int, double> vector)
//    {
//        return vector.Values.Sum(s => s * s);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] VectorNegative(this IEnumerable<double> vector)
//    {
//        return vector.Select(s => -s).ToArray();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] VectorMapScalarsInPlace(this double[] vector, Func<double, double> scalarMapping)
//    {
//        for (var i = 0; i < vector.Length; i++)
//            vector[i] = scalarMapping(vector[i]);

//        return vector;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] VectorMapScalarsInPlace(this double[] vector, Func<int, double, double> scalarMapping)
//    {
//        for (var i = 0; i < vector.Length; i++)
//            vector[i] = scalarMapping(i, vector[i]);

//        return vector;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] VectorTimesInPlace(this double[] vector, double scalar)
//    {
//        for (var i = 0; i < vector.Length; i++)
//            vector[i] *= scalar;

//        return vector;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] VectorTimes(this IEnumerable<double> vector, double scalar)
//    {
//        return vector.Select(s => s * scalar).ToArray();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] VectorDivideInPlace(this double[] vector, double scalar)
//    {
//        scalar = 1d / scalar;

//        for (var i = 0; i < vector.Length; i++)
//            vector[i] *= scalar;

//        return vector;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] VectorDivide(this IEnumerable<double> vector, double scalar)
//    {
//        scalar = 1d / scalar;

//        return vector.Select(s => s * scalar).ToArray();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] VectorNormalizeInPlace(this double[] vector)
//    {
//        var scalar = 1d / vector.GetVectorNorm();

//        for (var i = 0; i < vector.Length; i++)
//            vector[i] *= scalar;

//        return vector;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] VectorAddInPlace(this double[] vector1, IEnumerable<double> vector2)
//    {
//        var i = 0;
//        foreach (var scalar in vector2.Take(vector1.Length))
//        {
//            vector1[i] += scalar;

//            i++;
//        }

//        return vector1;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] VectorAdd(this IReadOnlyList<double> vector1, IReadOnlyList<double> vector2)
//    {
//        Debug.Assert(vector1.Count == vector2.Count);

//        var vector = new double[vector1.Count];

//        for (var i = 0; i < vector1.Count; i++)
//            vector[i] = vector1[i] + vector2[i];

//        return vector;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] VectorSubtractInPlace(this double[] vector1, IEnumerable<double> vector2)
//    {
//        var i = 0;
//        foreach (var scalar in vector2.Take(vector1.Length))
//        {
//            vector1[i] -= scalar;

//            i++;
//        }

//        return vector1;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] VectorSubtract(this IReadOnlyList<double> vector1, IReadOnlyList<double> vector2)
//    {
//        Debug.Assert(vector1.Count == vector2.Count);

//        var vector = new double[vector1.Count];

//        for (var i = 0; i < vector1.Count; i++)
//            vector[i] = vector1[i] - vector2[i];

//        return vector;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double VectorDot(this IReadOnlyList<double> vector1, IReadOnlyList<double> vector2)
//    {
//        var n = Math.Min(vector1.Count, vector2.Count);
//        var d = 0d;

//        for (var i = 0; i < n; i++)
//            d += vector1[i] * vector2[i];

//        return d;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double VectorDot(this IReadOnlyList<double> vector1, IReadOnlyList<Float64Scalar> vector2)
//    {
//        var n = Math.Min(vector1.Count, vector2.Count);
//        var d = 0d;

//        for (var i = 0; i < n; i++)
//            d += vector1[i] * vector2[i];

//        return d;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double VectorDot(this IReadOnlyList<double> vector1, IReadOnlyDictionary<int, double> vector2)
//    {
//        var count = vector1.Count;

//        return vector2
//            .Where(pair => pair.Key < count)
//            .Aggregate(
//                0d,
//                (dot, pair) =>
//                    dot + pair.Value * vector1[pair.Key]
//            );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double VectorDot(this IReadOnlyDictionary<int, double> vector1, IReadOnlyList<double> vector2)
//    {
//        var count = vector2.Count;

//        return vector1
//            .Where(pair => pair.Key < count)
//            .Aggregate(
//                0d,
//                (dot, pair) =>
//                    dot + pair.Value * vector2[pair.Key]
//            );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double VectorDot(this IReadOnlyDictionary<int, double> vector1, IReadOnlyDictionary<int, double> vector2)
//    {
//        return Float64ScalarComposer
//            .Create()
//            .AddESpTerms(vector1, vector2)
//            .ScalarValue;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Pair<double> VectorDot(this IReadOnlyList<double> vector1, IReadOnlyList<double> vector2, IReadOnlyList<double> vector3)
//    {
//        var n = Math.Min(vector1.Count, vector2.Count);
//        var d1 = 0d;
//        var d2 = 0d;

//        for (var i = 0; i < n; i++)
//        {
//            var scalar = vector1[i];

//            d1 += scalar * vector2[i];
//            d2 += scalar * vector3[i];
//        }

//        return new Pair<double>(d1, d2);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Pair<double> VectorDot(this IReadOnlyDictionary<int, double> vector1, IReadOnlyDictionary<int, double> vector2, IReadOnlyDictionary<int, double> vector3)
//    {
//        return new Pair<double>(
//            vector1.VectorDot(vector2),
//            vector1.VectorDot(vector3)
//        );
//    }


//    public static double VectorAngleCos(this IReadOnlyList<double> u, IReadOnlyList<double> v)
//    {
//        var uuDot = 0d;
//        var vvDot = 0d;
//        var uvDot = 0d;

//        for (var i = 0; i < u.Count; i++)
//        {
//            uuDot += u[i].Square();
//            vvDot += v[i].Square();
//            uvDot += u[i] * v[i];
//        }

//        return BasicMathUtils.Clamp((uvDot / (uuDot * vvDot).Sqrt()), -1, 1);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double VectorAngleCos(this IReadOnlyList<double> vector1, IReadOnlyDictionary<int, double> vector2)
//    {
//        var uuDot = vector1.GetVectorNormSquared();
//        var vvDot = vector2.GetVectorNormSquared();
//        var uvDot = vector1.VectorDot(vector2);

//        return BasicMathUtils.Clamp((uvDot / (uuDot * vvDot).Sqrt()), -1, 1);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double VectorAngleCos(this IReadOnlyDictionary<int, double> vector1, IReadOnlyList<double> vector2)
//    {
//        var uuDot = vector1.GetVectorNormSquared();
//        var vvDot = vector2.GetVectorNormSquared();
//        var uvDot = vector1.VectorDot(vector2);

//        return BasicMathUtils.Clamp((uvDot / (uuDot * vvDot).Sqrt()), -1, 1);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double VectorAngleCos(this IReadOnlyDictionary<int, double> vector1, IReadOnlyDictionary<int, double> vector2)
//    {
//        var uuDot = vector1.GetVectorNormSquared();
//        var vvDot = vector2.GetVectorNormSquared();
//        var uvDot = vector1.VectorDot(vector2);

//        return BasicMathUtils.Clamp((uvDot / (uuDot * vvDot).Sqrt()), -1, 1);
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double GetVectorNorm(this double[,] array)
//    {
//        return array.GetItems().Sum(s => s * s).Sqrt();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double GetVectorNormSquared(this double[,] array)
//    {
//        return array.GetItems().Sum(s => s * s);
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Matrix4x4 ToMatrix4x4(this double[,] array)
//    {
//        return new Matrix4x4(
//            (float)array[0, 0], (float)array[0, 1], (float)array[0, 2], (float)array[0, 3],
//            (float)array[1, 0], (float)array[1, 1], (float)array[1, 2], (float)array[1, 3],
//            (float)array[2, 0], (float)array[2, 1], (float)array[2, 2], (float)array[2, 3],
//            (float)array[3, 0], (float)array[3, 1], (float)array[3, 2], (float)array[3, 3]
//        );
//    }


//    public static double[,] CreateIdentity(int size)
//    {
//        var array = new double[size, size];

//        for (var i = 0; i < size; i++)
//            array[i, i] = 1d;

//        return array;
//    }

//    /// <summary>
//    /// See the paper "Generalized Clarke Components for Poly-phase Networks", 1969
//    /// </summary>
//    /// <param name="size"></param>
//    /// <returns></returns>
//    private static double[,] CreateClarkeRotationArrayOdd(int size)
//    {
//        var clarkeArray = new double[size, size];

//        var s = (2d / size).Sqrt();
//        //$"Sqrt[2 / {m}]";

//        // m is odd, fill all rows except the last
//        var n = (size - 1) / 2;
//        for (var k = 0; k < n; k++)
//        {
//            var rowIndex1 = 2 * k;
//            var rowIndex2 = 2 * k + 1;

//            clarkeArray[rowIndex1, 0] = s;
//            clarkeArray[rowIndex2, 0] = 0d;

//            for (var colIndex = 1; colIndex < size; colIndex++)
//            {
//                var angle = Math.Tau * (k + 1) * colIndex / size;
//                // $"2 * Pi * {k + 1} * {i} / {m}";

//                var cosAngle = s * Math.Cos(angle); // $"{s} * Cos[{angle}]";
//                var sinAngle = s * Math.Sin(angle); // $"{s} * Sin[{angle}]";

//                clarkeArray[rowIndex1, colIndex] = cosAngle;
//                clarkeArray[rowIndex2, colIndex] = sinAngle;
//            }
//        }

//        //Fill the last column
//        var v = 1d / Math.Sqrt(size);
//        // $"1 / Sqrt[{m}]";

//        for (var colIndex = 0; colIndex < size; colIndex++)
//            clarkeArray[size - 1, colIndex] = v;

//        return clarkeArray;
//    }

//    /// <summary>
//    /// See the paper "Generalized Clarke Components for Poly-phase Networks", 1969
//    /// </summary>
//    /// <param name="size"></param>
//    /// <returns></returns>
//    private static double[,] CreateClarkeRotationArrayEven(int size)
//    {
//        var clarkeArray = new double[size, size];

//        var s = Math.Sqrt(2d / size);
//        //$"Sqrt[2 / {m}]";

//        //m is even, fill all rows except the last two
//        var n = (size - 1) / 2;
//        for (var k = 0; k < n; k++)
//        {
//            var rowIndex1 = 2 * k;
//            var rowIndex2 = 2 * k + 1;

//            clarkeArray[rowIndex1, 0] = s;
//            clarkeArray[rowIndex2, 0] = 0d;

//            for (var colIndex = 1; colIndex < size; colIndex++)
//            {
//                var angle = Math.Tau * (k + 1) * colIndex / size;
//                // $"2 * Pi * {k + 1} * {i} / {m}";

//                var cosAngle = s * Math.Cos(angle); // $"{s} * Cos[{angle}]";
//                var sinAngle = s * Math.Sin(angle); // $"{s} * Sin[{angle}]";

//                clarkeArray[rowIndex1, colIndex] = cosAngle;
//                clarkeArray[rowIndex2, colIndex] = sinAngle;
//            }
//        }

//        //Fill the last two rows
//        var v0 = 1d / Math.Sqrt(size);
//        // $"1 / Sqrt[{m}]";

//        var v1 = -v0;
//        // $"-1 / Sqrt[{m}]";

//        for (var colIndex = 0; colIndex < size; colIndex++)
//        {
//            clarkeArray[size - 2, colIndex] = colIndex % 2 == 0 ? v0 : v1;
//            clarkeArray[size - 1, colIndex] = v0;
//        }

//        return clarkeArray;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] CreateClarkeRotationArray(int size)
//    {
//        return size % 2 == 0
//            ? CreateClarkeRotationArrayEven(size)
//            : CreateClarkeRotationArrayOdd(size);
//    }


//    public static double[,] CreateCirculantColumnArray(IReadOnlyList<double> column)
//    {
//        var n = column.Count;
//        var array = new double[n, n];

//        for (var j = 0; j < n; j++)
//            for (var i = 0; i < n; i++)
//                array[i, j] = column[(i - j).Mod(n)];

//        return array;
//    }


//    public static double[,] MapScalars(this double[,] scalarsArray1, double scalar2, Func<double, double, double> scalarMapping)
//    {
//        var rowsCount = scalarsArray1.GetLength(0);
//        var colsCount = scalarsArray1.GetLength(1);

//        var array = new double[rowsCount, colsCount];

//        for (var i = 0; i < rowsCount; i++)
//            for (var j = 0; j < colsCount; j++)
//            {
//                array[i, j] = scalarMapping(
//                    scalarsArray1[i, j],
//                    scalar2
//                );
//            }

//        return array;
//    }

//    public static double[,] MapScalars(this double scalar1, double[,] scalarsArray2, Func<double, double, double> scalarMapping)
//    {


//        var rowsCount = scalarsArray2.GetLength(0);
//        var colsCount = scalarsArray2.GetLength(1);

//        var array = new double[rowsCount, colsCount];

//        for (var i = 0; i < rowsCount; i++)
//            for (var j = 0; j < colsCount; j++)
//            {
//                array[i, j] = scalarMapping(
//                    scalar1,
//                    scalarsArray2[i, j]
//                );
//            }

//        return array;
//    }

//    public static double[,] MapScalars(this double[,] scalarsArray1, double[,] scalarsArray2, Func<double, double, double> scalarMapping)
//    {
//        var rowsCount = scalarsArray1.GetLength(0);
//        var colsCount = scalarsArray1.GetLength(1);

//        var array = new double[rowsCount, colsCount];

//        for (var i = 0; i < rowsCount; i++)
//            for (var j = 0; j < colsCount; j++)
//            {
//                array[i, j] = scalarMapping(
//                    scalarsArray1[i, j],
//                    scalarsArray2[i, j]
//                );
//            }

//        return array;
//    }

//    public static double[,] MapNotZeroScalars(this double[,] scalarsArray, Func<double, double> scalarMapping)
//    {
//        var rowsCount = scalarsArray.GetLength(0);
//        var colsCount = scalarsArray.GetLength(1);

//        var array = new double[rowsCount, colsCount];

//        for (var i = 0; i < rowsCount; i++)
//            for (var j = 0; j < colsCount; j++)
//            {
//                var scalar = scalarsArray[i, j];

//                if (scalar.IsZero())
//                {
//                    array[i, j] = 0d;

//                    continue;
//                }

//                array[i, j] = scalarMapping(
//                    scalarsArray[i, j]
//                );
//            }

//        return array;
//    }

//    public static double[,] MapScalars(this double[,] scalarsArray, Func<double, double> scalarMapping)
//    {
//        var rowsCount = scalarsArray.GetLength(0);
//        var colsCount = scalarsArray.GetLength(1);

//        var array = new double[rowsCount, colsCount];

//        for (var i = 0; i < rowsCount; i++)
//            for (var j = 0; j < colsCount; j++)
//            {
//                array[i, j] = scalarMapping(
//                    scalarsArray[i, j]
//                );
//            }

//        return array;
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Add(this double[,] scalarsArray1, double[,] scalarsArray2)
//    {
//        return scalarsArray1.MapScalars(
//            scalarsArray2,
//            (a, b) => a + b
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Add(this double scalar1, double[,] scalarsArray2)
//    {
//        return scalar1.MapScalars(
//            scalarsArray2,
//            (a, b) => a + b
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Add(this double[,] scalarsArray1, double scalar2)
//    {
//        return scalarsArray1.MapScalars(
//            scalar2,
//            (a, b) => a + b
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Subtract(this double[,] scalarsArray1, double[,] scalarsArray2)
//    {
//        return scalarsArray1.MapScalars(
//            scalarsArray2,
//            (a, b) => a - b
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Subtract(this double scalar1, double[,] scalarsArray2)
//    {
//        return scalar1.MapScalars(
//            scalarsArray2,
//            (a, b) => a - b
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Subtract(this double[,] scalarsArray1, double scalar2)
//    {
//        return scalarsArray1.MapScalars(
//            scalar2,
//            (a, b) => a - b
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Times(this double[,] scalarsArray1, double[,] scalarsArray2)
//    {
//        return scalarsArray1.MapScalars(
//            scalarsArray2,
//            (a, b) => a * b
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Times(this double[,] scalarsArray1, params double[][,] scalarsArrays)
//    {
//        return scalarsArrays.Aggregate(
//            scalarsArray1,
//            (a, b) => a.Times(b)
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Times(this double[,] scalarsArray1, IEnumerable<double[,]> scalarsArrays)
//    {
//        return scalarsArrays.Aggregate(
//            scalarsArray1,
//            (a, b) => a.Times(b)
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Times(this double scalar1, double[,] scalarsArray2)
//    {
//        return scalar1.MapScalars(
//            scalarsArray2,
//            (a, b) => a * b
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Times(this double[,] scalarsArray1, double scalar2)
//    {
//        return scalarsArray1.MapScalars(
//            scalar2,
//            (a, b) => a * b
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Divide(this double[,] scalarsArray1, double[,] scalarsArray2)
//    {
//        return scalarsArray1.MapScalars(
//            scalarsArray2,
//            (a, b) => a / b
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Divide(this double scalar1, double[,] scalarsArray2)
//    {
//        return scalar1.MapScalars(
//            scalarsArray2,
//            (a, b) => a / b
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Divide(this double[,] scalarsArray1, double scalar2)
//    {
//        return scalarsArray1.MapScalars(
//            scalar2,
//            (a, b) => a / b
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] NotZeroScalarsInverse(this double[,] scalarsArray)
//    {
//        return scalarsArray.MapNotZeroScalars(
//            a => 1d / a
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] ScalarsNegative(this double[,] scalarsArray)
//    {
//        return scalarsArray.MapScalars(
//            a => -a
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] ScalarsCos(this double[,] scalarsArray)
//    {
//        return scalarsArray.MapScalars(
//            Math.Cos
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] ScalarsSin(this double[,] scalarsArray)
//    {
//        return scalarsArray.MapScalars(
//            Math.Sin
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] ScalarsTan(this double[,] scalarsArray)
//    {
//        return scalarsArray.MapScalars(
//            Math.Tan
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] ScalarsExp(this double[,] scalarsArray)
//    {
//        return scalarsArray.MapScalars(
//            Math.Exp
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] ScalarsLog(this double[,] scalarsArray)
//    {
//        return scalarsArray.MapScalars(
//            a => a.LogE()
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] ScalarsLog10(this double[,] scalarsArray)
//    {
//        return scalarsArray.MapScalars(
//            Math.Log10
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] ScalarsLog2(this double[,] scalarsArray)
//    {
//        return scalarsArray.MapScalars(
//            Math.Log2
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] ScalarsAbs(this double[,] scalarsArray)
//    {
//        return scalarsArray.MapScalars(
//            Math.Abs
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] ScalarsSqrt(this double[,] scalarsArray)
//    {
//        return scalarsArray.MapScalars(
//            Math.Sqrt
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] ScalarsSqrtOfAbs(this double[,] scalarsArray)
//    {
//        return scalarsArray.MapScalars(
//            a => a.SqrtOfAbs()
//        );
//    }


//    //public static VectorStorage ColumnToVectorStorage(this double[,] scalarsArray, int colIndex, IScalarProcessor scalarProcessor)
//    //{
//    //    var rowsCount = scalarsArray.GetLength(0);

//    //    var composer = Math.CreateVectorStorageComposer(rowsCount);

//    //    for (var i = 0; i < rowsCount; i++)
//    //    {
//    //        var scalar = scalarsArray[i, colIndex];

//    //        if (!scalar.IsZero())
//    //            composer.SetTerm((ulong)i, scalar);
//    //    }

//    //    return composer.CreateVectorStorage();
//    //}

//    //public static VectorStorage RowToVectorStorage(this double[,] scalarsArray, int rowIndex, IScalarProcessor scalarProcessor)
//    //{
//    //    var colsCount = scalarsArray.GetLength(1);

//    //    var composer = Math.CreateVectorStorageComposer(1);

//    //    for (var j = 0; j < colsCount; j++)
//    //    {
//    //        var scalar = scalarsArray[rowIndex, j];

//    //        if (!scalar.IsZero())
//    //            composer.SetTerm((ulong)j, scalar);
//    //    }

//    //    return composer.CreateVectorStorage();
//    //}

//    //public static Dictionary<ulong, VectorStorage> ColumnsToVectorStoragesDictionary(this double[,] scalarsArray, IScalarProcessor scalarProcessor)
//    //{
//    //    var rowsCount = scalarsArray.GetLength(0);
//    //    var colsCount = scalarsArray.GetLength(1);

//    //    var vectorsDictionary =
//    //        new Dictionary<ulong, VectorStorage>();

//    //    for (var j = 0; j < colsCount; j++)
//    //    {
//    //        var composer = Math.CreateVectorStorageComposer(1);

//    //        for (var i = 0; i < rowsCount; i++)
//    //        {
//    //            var scalar = scalarsArray[i, j];

//    //            if (!scalar.IsZero())
//    //                composer.SetTerm((ulong)i, scalar);
//    //        }

//    //        vectorsDictionary.Add((ulong)j, composer.CreateVectorStorage());
//    //    }

//    //    return vectorsDictionary;
//    //}

//    //public static VectorStorage[] ColumnsToVectorStoragesArray(this double[,] scalarsArray, IScalarProcessor scalarProcessor)
//    //{
//    //    var rowsCount = scalarsArray.GetLength(0);
//    //    var colsCount = scalarsArray.GetLength(1);

//    //    var vectorsArray =
//    //        new VectorStorage[colsCount];

//    //    for (var j = 0; j < colsCount; j++)
//    //    {
//    //        var composer = Math.CreateVectorStorageComposer(1);

//    //        for (var i = 0; i < rowsCount; i++)
//    //        {
//    //            var scalar = scalarsArray[i, j];

//    //            if (!scalar.IsZero())
//    //                composer.SetTerm((ulong)i, scalar);
//    //        }

//    //        vectorsArray[j] = composer.CreateVectorStorage();
//    //    }

//    //    return vectorsArray;
//    //}

//    //public static GaVector[] ColumnsToVectorsArray(this double[,] scalarsArray, IScalarProcessor scalarProcessor)
//    //{
//    //    var rowsCount = scalarsArray.GetLength(0);
//    //    var colsCount = scalarsArray.GetLength(1);

//    //    var vectorsArray =
//    //        new GaVector[colsCount];

//    //    for (var j = 0; j < colsCount; j++)
//    //    {
//    //        var composer = Math.CreateVectorStorageComposer(1);

//    //        for (var i = 0; i < rowsCount; i++)
//    //        {
//    //            var scalar = scalarsArray[i, j];

//    //            if (!scalar.IsZero())
//    //                composer.SetTerm((ulong)i, scalar);
//    //        }

//    //        vectorsArray[j] = composer.Vector();
//    //    }

//    //    return vectorsArray;
//    //}

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double GetScalar(this IReadOnlyList<double> matrix, int index)
//    {
//        return index >= 0 &&
//               index < matrix.Count
//            ? matrix[index]
//            : 0d;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double GetScalar(this double[,] matrix, int rowIndex, int colIndex)
//    {
//        return rowIndex >= 0 &&
//               colIndex >= 0 &&
//               rowIndex < matrix.GetLength(0) &&
//               colIndex < matrix.GetLength(1)
//            ? matrix[rowIndex, colIndex]
//            : 0d;
//    }


//    public static double[] MapScalars<T1>(this IReadOnlyList<T1> matrix1, Func<T1, double> scalarMapping)
//    {
//        var count = matrix1.Count;
//        var newMatrix = new double[count];

//        for (var i = 0; i < count; i++)
//            newMatrix[i] = scalarMapping(matrix1[i]);

//        return newMatrix;
//    }

//    public static double[] MapScalars<T1>(this IReadOnlyList<T1> matrix1, Func<int, T1, double> scalarMapping)
//    {
//        var count = matrix1.Count;
//        var newMatrix = new double[count];

//        for (var i = 0; i < count; i++)
//            newMatrix[i] = scalarMapping(i, matrix1[i]);

//        return newMatrix;
//    }

//    public static double[] MapScalars(this IReadOnlyList<double> matrix1, IReadOnlyList<double> matrix2, double initScalar, Func<double, double, double> scalarMapping)
//    {
//        var count1 = matrix1.Count;
//        var count2 = matrix2.Count;
//        var count = Math.Max(count1, count2);

//        var newMatrix = new double[count];

//        for (var i = 0; i < count; i++)
//        {
//            var s1 = i < count1
//                ? matrix1[i]
//                : initScalar;

//            var s2 = i < count2
//                ? matrix2[i]
//                : initScalar;

//            newMatrix[i] = scalarMapping(s1, s2);
//        }

//        return newMatrix;
//    }

//    public static double[] MapScalars<T1, T2>(this IReadOnlyList<T1> matrix1, IReadOnlyList<T2> matrix2, Func<T1> initFunc1, Func<T2> initFunc2, Func<T1, T2, double> scalarMapping)
//    {
//        var count1 = matrix1.Count;
//        var count2 = matrix2.Count;
//        var count = Math.Max(count1, count2);

//        var newMatrix = new double[count];

//        for (var i = 0; i < count; i++)
//        {
//            var s1 = i < count1
//                ? matrix1[i]
//                : initFunc1();

//            var s2 = i < count2
//                ? matrix2[i]
//                : initFunc2();

//            newMatrix[i] = scalarMapping(s1, s2);
//        }

//        return newMatrix;
//    }

//    public static double[,] MapScalars<T1>(this T1[,] matrix1, Func<T1, double> scalarMapping)
//    {
//        var rowsCount = matrix1.GetLength(0);
//        var colsCount = matrix1.GetLength(1);
//        var newMatrix = new double[rowsCount, colsCount];

//        for (var i = 0; i < rowsCount; i++)
//            for (var j = 0; j < colsCount; j++)
//                newMatrix[i, j] = scalarMapping(matrix1[i, j]);

//        return newMatrix;
//    }

//    public static double[,] MapScalars<T1>(this T1[,] matrix1, Func<int, int, T1, double> scalarMapping)
//    {
//        var rowsCount = matrix1.GetLength(0);
//        var colsCount = matrix1.GetLength(1);
//        var newMatrix = new double[rowsCount, colsCount];

//        for (var i = 0; i < rowsCount; i++)
//            for (var j = 0; j < colsCount; j++)
//                newMatrix[i, j] = scalarMapping(i, j, matrix1[i, j]);

//        return newMatrix;
//    }

//    public static double[,] MapScalars(this double[,] matrix1, double[,] matrix2, double initScalar, Func<double, double, double> scalarMapping)
//    {
//        var rowCount1 = matrix1.GetLength(0);
//        var colCount1 = matrix1.GetLength(1);

//        var rowCount2 = matrix2.GetLength(0);
//        var colCount2 = matrix1.GetLength(2);

//        var rowCount = Math.Max(rowCount1, rowCount2);
//        var colCount = Math.Max(colCount1, colCount2);

//        var newMatrix = new double[rowCount, colCount];

//        for (var j = 0; j < colCount; j++)
//            for (var i = 0; i < rowCount; i++)
//            {
//                var s1 = i < rowCount1 && j < colCount1
//                    ? matrix1[i, j]
//                    : initScalar;

//                var s2 = i < rowCount2 && j < colCount2
//                    ? matrix2[i, j]
//                    : initScalar;

//                newMatrix[i, j] = scalarMapping(s1, s2);
//            }

//        return newMatrix;
//    }

//    public static double[,] MapScalars<T1, T2>(this T1[,] matrix1, T2[,] matrix2, Func<T1> initFunc1, Func<T2> initFunc2, Func<T1, T2, double> scalarMapping)
//    {
//        var rowCount1 = matrix1.GetLength(0);
//        var colCount1 = matrix1.GetLength(1);

//        var rowCount2 = matrix2.GetLength(0);
//        var colCount2 = matrix1.GetLength(2);

//        var rowCount = Math.Max(rowCount1, rowCount2);
//        var colCount = Math.Max(colCount1, colCount2);

//        var newMatrix = new double[rowCount, colCount];

//        for (var j = 0; j < colCount; j++)
//            for (var i = 0; i < rowCount; i++)
//            {
//                var s1 = i < rowCount1 && j < colCount1
//                    ? matrix1[i, j]
//                    : initFunc1();

//                var s2 = i < rowCount2 && j < colCount2
//                    ? matrix2[i, j]
//                    : initFunc2();

//                newMatrix[i, j] = scalarMapping(s1, s2);
//            }

//        return newMatrix;
//    }

//    //public static double[,] MapScalars(this double[,] matrix1, double[,] matrix2, double initScalar, Func<double, double, double> accumulatorFunc)
//    //{
//    //    var rowsCount1 = matrix1.GetLength(0);
//    //    var colsCount1 = matrix1.GetLength(1);

//    //    var rowsCount2 = matrix2.GetLength(0);
//    //    var colsCount2 = matrix2.GetLength(1);

//    //    var innerCount = Math.Max(colsCount1, rowsCount2);

//    //    var newMatrix = new double[rowsCount1, colsCount2];

//    //    for (var i = 0; i < rowsCount1; i++)
//    //    {
//    //        for (var j = 0; j < colsCount2; j++)
//    //        {
//    //            var scalar = initScalar;

//    //            for (var k = 0; k < innerCount; k++)
//    //            {
//    //                var aik = i < rowsCount1 && k < colsCount1
//    //                    ? matrix1[i, k]
//    //                    : initScalar;

//    //                var bkj = k < rowsCount2 && j < colsCount2
//    //                    ? matrix2[k, j]
//    //                    : initScalar;

//    //                scalar = accumulatorFunc(scalar, aik, bkj);
//    //            }

//    //            newMatrix[i, j] = scalar;
//    //        }
//    //    }

//    //    return newMatrix;
//    //}

//    public static double[,] MapScalars<T1, T2>(this T1[,] matrix1, T2[,] matrix2, Func<T1> initFunc1, Func<T2> initFunc2, Func<double> accumulatorInitFunc, Func<double, T1, T2, double> accumulatorFunc)
//    {
//        var rowsCount1 = matrix1.GetLength(0);
//        var colsCount1 = matrix1.GetLength(1);

//        var rowsCount2 = matrix2.GetLength(0);
//        var colsCount2 = matrix2.GetLength(1);

//        var innerCount = Math.Max(colsCount1, rowsCount2);

//        var newMatrix = new double[rowsCount1, colsCount2];

//        for (var i = 0; i < rowsCount1; i++)
//        {
//            for (var j = 0; j < colsCount2; j++)
//            {
//                var accumulator = accumulatorInitFunc();

//                for (var k = 0; k < innerCount; k++)
//                {
//                    var aik = i < rowsCount1 && k < colsCount1
//                        ? matrix1[i, k]
//                        : initFunc1();

//                    var bkj = k < rowsCount2 && j < colsCount2
//                        ? matrix2[k, j]
//                        : initFunc2();

//                    accumulator = accumulatorFunc(accumulator, aik, bkj);
//                }

//                newMatrix[i, j] = accumulator;
//            }
//        }

//        return newMatrix;
//    }


//    public static double[] MatrixProduct(this double[,] matrix1, double[] matrix2)
//    {
//        var m = matrix1.GetLength(0);
//        var n = matrix1.GetLength(1);

//        if (n != matrix2.Length)
//            throw new InvalidOperationException();

//        var matrix = new double[m];

//        for (var i = 0; i < m; i++)
//        {
//            var d = 0d;

//            for (var j = 0; j < n; j++)
//                d += matrix1[i, j] * matrix2[j];

//            matrix[i] = d;
//        }

//        return matrix;
//    }

//    public static double[] MatrixProduct(this double[,] matrix1, IReadOnlyList<double> matrix2)
//    {
//        var m = matrix1.GetLength(0);
//        var n = matrix1.GetLength(1);

//        if (n != matrix2.Count)
//            throw new InvalidOperationException();

//        var matrix = new double[m];

//        for (var i = 0; i < m; i++)
//        {
//            var d = 0d;

//            for (var j = 0; j < n; j++)
//                d += matrix1[i, j] * matrix2[j];

//            matrix[i] = d;
//        }

//        return matrix;
//    }

//    public static LinFloat64Vector MatrixProduct(this double[,] matrix, IReadOnlyDictionary<int, double> vector)
//    {
//        var m = matrix.GetLength(0);
//        var n = matrix.GetLength(1);

//        var composer = LinFloat64VectorComposer.Create();

//        foreach (var (j, scalar) in vector)
//        {
//            if (j >= n) continue;

//            for (var i = 0; i < m; i++)
//                composer.AddTerm(i, matrix[i, j] * scalar);
//        }

//        return composer.GetVector();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] MatrixProduct(this double[,] matrix1, double[,] matrix2)
//    {
//        return matrix1.MapScalars(
//            matrix2,
//            0d,
//            (accumulator, a, b) =>
//                accumulator + a * b
//        );
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] Abs(this IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(Math.Abs);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] Sqrt(this IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(Math.Sqrt);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] SqrtOfAbs(this IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(a => a.SqrtOfAbs());
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] Exp(this IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(Math.Exp);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] LogE(this IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(a => a.LogE());
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] Log2(this IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(Math.Log2);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] Log10(this IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(Math.Log10);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] Log(this IReadOnlyList<double> v1, double scalar)
//    {
//        return v1.MapScalars(s => Math.Log(s, scalar));
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] Log(this double scalar, IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(s => Math.Log(scalar, s));
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] Power(this IReadOnlyList<double> v1, double scalar)
//    {
//        return v1.MapScalars(s => s.Power(scalar));
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] Power(this double scalar, IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(s => scalar.Power(s));
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] Cos(this IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(Math.Cos);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] Sin(this IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(Math.Sin);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] Tan(this IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(Math.Tan);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] ArcCos(this IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(a => a.ArcCos().Radians.Value);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] ArcSin(this IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(a => a.ArcSin().Radians.Value);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] ArcTan(this IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(a => a.ArcTan().Radians.Value);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] Cosh(this IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(Math.Cosh);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] Sinh(this IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(Math.Sinh);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[] Tanh(this IReadOnlyList<double> v1)
//    {
//        return v1.MapScalars(Math.Tanh);
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Abs(this double[,] v1)
//    {
//        return v1.MapScalars(Math.Abs);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Sqrt(this double[,] v1)
//    {
//        return v1.MapScalars(Math.Sqrt);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] SqrtOfAbs(this double[,] v1)
//    {
//        return v1.MapScalars(a => a.SqrtOfAbs());
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Exp(this double[,] v1)
//    {
//        return v1.MapScalars(Math.Exp);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] LogE(this double[,] v1)
//    {
//        return v1.MapScalars(a => a.LogE());
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Log2(this double[,] v1)
//    {
//        return v1.MapScalars(Math.Log2);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Log10(this double[,] v1)
//    {
//        return v1.MapScalars(Math.Log10);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Log(this double[,] v1, double scalar)
//    {
//        return v1.MapScalars(s => Math.Log(s, scalar));
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Log(this double scalar, double[,] v1)
//    {
//        return v1.MapScalars(s => Math.Log(scalar, s));
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Power(this double[,] v1, double scalar)
//    {
//        return v1.MapScalars(s => s.Power(scalar));
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Power(this double scalar, double[,] v1)
//    {
//        return v1.MapScalars(s => scalar.Power(s));
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Cos(this double[,] v1)
//    {
//        return v1.MapScalars(Math.Cos);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Sin(this double[,] v1)
//    {
//        return v1.MapScalars(Math.Sin);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Tan(this double[,] v1)
//    {
//        return v1.MapScalars(Math.Tan);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] ArcCos(this double[,] v1)
//    {
//        return v1.MapScalars(a => a.ArcCos().Radians.Value);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] ArcSin(this double[,] v1)
//    {
//        return v1.MapScalars(a => a.ArcSin().Radians.Value);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] ArcTan(this double[,] v1)
//    {
//        return v1.MapScalars(a => a.ArcTan().Radians.Value);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Cosh(this double[,] v1)
//    {
//        return v1.MapScalars(Math.Cosh);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Sinh(this double[,] v1)
//    {
//        return v1.MapScalars(Math.Sinh);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static double[,] Tanh(this double[,] v1)
//    {
//        return v1.MapScalars(Math.Tanh);
//    }
//}