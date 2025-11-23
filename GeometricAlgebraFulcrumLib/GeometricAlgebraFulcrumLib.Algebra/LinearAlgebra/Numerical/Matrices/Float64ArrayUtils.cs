using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;

public static class Float64ArrayUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetSum(this IEnumerable<double> vector)
    {
        return vector.Sum();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetMin(this IEnumerable<double> vector)
    {
        return vector.Prepend(double.PositiveInfinity).Min();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetMax(this IEnumerable<double> vector)
    {
        return vector.Prepend(double.NegativeInfinity).Max();
    }

    public static Tuple<double, double> GetMinMax(this IEnumerable<double> vector)
    {
        var min = double.PositiveInfinity;
        var max = double.NegativeInfinity;

        foreach (var value in vector)
        {
            if (min > value) min = value;
            if (max < value) max = value;
        }

        return new Tuple<double, double>(min, max);
    }

    public static double GetMedian(this IReadOnlyList<int> inputArray)
    {
        var length = inputArray.Count;
        var copyArray = new int[length];

        for (var i = 0; i < length; i++)
            copyArray[i] = inputArray[i];

        Array.Sort(copyArray);

        return length % 2 == 0
            ? copyArray[length / 2] + copyArray[length / 2 - 1] >> 1
            : copyArray[(length - 1) / 2];
    }

    public static double GetMedian(this IReadOnlyList<double> inputArray)
    {
        var length = inputArray.Count;
        var copyArray = new double[length];

        for (var i = 0; i < length; i++)
            copyArray[i] = inputArray[i];

        Array.Sort(copyArray);

        return length % 2 == 0
            ? (copyArray[length / 2] + copyArray[length / 2 - 1]) / 2
            : copyArray[(length - 1) / 2];
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
        var scalar = 1d / vector.L2Norm();

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
    public static double VectorDot(this IReadOnlyList<double> vector1, IReadOnlyList<double> vector2)
    {
        var n = Math.Min(vector1.Count, vector2.Count);
        var d = 0d;

        for (var i = 0; i < n; i++)
            d += vector1[i] * vector2[i];

        return d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double VectorDot(this IReadOnlyList<double> vector1, IReadOnlyList<Float64Scalar> vector2)
    {
        var n = Math.Min(vector1.Count, vector2.Count);
        var d = 0d;

        for (var i = 0; i < n; i++)
            d += vector1[i] * vector2[i];

        return d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double VectorDot(this IReadOnlyList<double> vector1, IReadOnlyDictionary<int, double> vector2)
    {
        var count = vector1.Count;

        return vector2
            .Where(pair => pair.Key < count)
            .Aggregate(
                0d,
                (dot, pair) =>
                    dot + pair.Value * vector1[pair.Key]
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double VectorDot(this IReadOnlyDictionary<int, double> vector1, IReadOnlyList<double> vector2)
    {
        var count = vector2.Count;

        return vector1
            .Where(pair => pair.Key < count)
            .Aggregate(
                0d,
                (dot, pair) =>
                    dot + pair.Value * vector2[pair.Key]
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double VectorDot(this IReadOnlyDictionary<int, double> vector1, IReadOnlyDictionary<int, double> vector2)
    {
        return Float64ScalarComposer
            .Create()
            .AddESpTerms(vector1, vector2)
            .ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<double> VectorDot(this IReadOnlyList<double> vector1, IReadOnlyList<double> vector2, IReadOnlyList<double> vector3)
    {
        var n = Math.Min(vector1.Count, vector2.Count);
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
    public static Pair<double> VectorDot(this IReadOnlyDictionary<int, double> vector1, IReadOnlyDictionary<int, double> vector2, IReadOnlyDictionary<int, double> vector3)
    {
        return new Pair<double>(
            vector1.VectorDot(vector2),
            vector1.VectorDot(vector3)
        );
    }


    public static double VectorAngleCos(this IReadOnlyList<double> u, IReadOnlyList<double> v)
    {
        var uuDot = 0d;
        var vvDot = 0d;
        var uvDot = 0d;

        for (var i = 0; i < u.Count; i++)
        {
            uuDot += u[i].Square();
            vvDot += v[i].Square();
            uvDot += u[i] * v[i];
        }

        return (uvDot / (uuDot * vvDot).Sqrt()).Clamp(-1, 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double VectorAngleCos(this IReadOnlyList<double> vector1, IReadOnlyDictionary<int, double> vector2)
    {
        var uuDot = vector1.L2NormSquared();
        var vvDot = vector2.L2NormSquared();
        var uvDot = vector1.VectorDot(vector2);

        return (uvDot / (uuDot * vvDot).Sqrt()).Clamp(-1, 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double VectorAngleCos(this IReadOnlyDictionary<int, double> vector1, IReadOnlyList<double> vector2)
    {
        var uuDot = vector1.L2NormSquared();
        var vvDot = vector2.L2NormSquared();
        var uvDot = vector1.VectorDot(vector2);

        return (uvDot / (uuDot * vvDot).Sqrt()).Clamp(-1, 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double VectorAngleCos(this IReadOnlyDictionary<int, double> vector1, IReadOnlyDictionary<int, double> vector2)
    {
        var uuDot = vector1.L2NormSquared();
        var vvDot = vector2.L2NormSquared();
        var uvDot = vector1.VectorDot(vector2);

        return (uvDot / (uuDot * vvDot).Sqrt()).Clamp(-1, 1);
    }



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    // ReSharper disable once InconsistentNaming
    public static Matrix4x4 ToSystemNumericsMatrix4x4(this double[,] array)
    {
        return new Matrix4x4(
            (float)array[0, 0], (float)array[0, 1], (float)array[0, 2], (float)array[0, 3],
            (float)array[1, 0], (float)array[1, 1], (float)array[1, 2], (float)array[1, 3],
            (float)array[2, 0], (float)array[2, 1], (float)array[2, 2], (float)array[2, 3],
            (float)array[3, 0], (float)array[3, 1], (float)array[3, 2], (float)array[3, 3]
        );
    }
    

    public static double[] CreateZero1D(int size)
    {
        var array = new double[size];

        return array;
    }

    public static double[,] CreateZero(int size)
    {
        var array = new double[size, size];

        return array;
    }

    public static double[,] CreateZero(int rowCount, int colCount)
    {
        var array = new double[rowCount, colCount];

        return array;
    }

    public static double[,] CreateIdentity(int size)
    {
        var array = new double[size, size];

        for (var i = 0; i < size; i++)
            array[i, i] = 1d;

        return array;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] MapNotZeroScalars(this double[,] scalarsArray, Func<double, double> scalarMapping)
    {
        return scalarsArray.MapItems(
            scalarMapping,
            0d,
            s => !s.IsZero()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] Add(this double[,] scalarsArray1, double[,] scalarsArray2)
    {
        return scalarsArray1.MapItems(
            scalarsArray2,
            (a, b) => a + b
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] Add(this double scalar1, double[,] scalarsArray2)
    {
        return scalar1.MapItems(
            scalarsArray2,
            (a, b) => a + b
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] Add(this double[,] scalarsArray1, double scalar2)
    {
        return scalarsArray1.MapItems(
            scalar2,
            (a, b) => a + b
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] Subtract(this double[,] scalarsArray1, double[,] scalarsArray2)
    {
        return scalarsArray1.MapItems(
            scalarsArray2,
            (a, b) => a - b
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] Subtract(this double scalar1, double[,] scalarsArray2)
    {
        return scalar1.MapItems(
            scalarsArray2,
            (a, b) => a - b
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] Subtract(this double[,] scalarsArray1, double scalar2)
    {
        return scalarsArray1.MapItems(
            scalar2,
            (a, b) => a - b
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] Times(this double[,] scalarsArray1, double[,] scalarsArray2)
    {
        return scalarsArray1.MapItems(
            scalarsArray2,
            (a, b) => a * b
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] Times(this double[,] scalarsArray1, params double[][,] scalarsArrays)
    {
        return scalarsArrays.Aggregate(
            scalarsArray1,
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] Times(this double[,] scalarsArray1, IEnumerable<double[,]> scalarsArrays)
    {
        return scalarsArrays.Aggregate(
            scalarsArray1,
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] Times(this double scalar1, double[,] scalarsArray2)
    {
        return scalar1.MapItems(
            scalarsArray2,
            (a, b) => a * b
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] Times(this double[,] scalarsArray1, double scalar2)
    {
        return scalarsArray1.MapItems(
            scalar2,
            (a, b) => a * b
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] Divide(this double[,] scalarsArray1, double[,] scalarsArray2)
    {
        return scalarsArray1.MapItems(
            scalarsArray2,
            (a, b) => a / b
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] Divide(this double scalar1, double[,] scalarsArray2)
    {
        return scalar1.MapItems(
            scalarsArray2,
            (a, b) => a / b
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] Divide(this double[,] scalarsArray1, double scalar2)
    {
        return scalarsArray1.MapItems(
            scalar2,
            (a, b) => a / b
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] NotZeroScalarsInverse(this double[,] scalarsArray)
    {
        return scalarsArray.MapNotZeroScalars(
            a => 1d / a
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsAbs(this IReadOnlyList<double> v1)
    {
        return v1.MapItems(Math.Abs);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsSqrt(this IReadOnlyList<double> v1)
    {
        return v1.MapItems(Math.Sqrt);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsSqrtOfAbs(this IReadOnlyList<double> v1)
    {
        return v1.MapItems(a => a.SqrtOfAbs());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsExp(this IReadOnlyList<double> v1)
    {
        return v1.MapItems(Math.Exp);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsLogE(this IReadOnlyList<double> v1)
    {
        return v1.MapItems(a => a.LogE());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsLog2(this IReadOnlyList<double> v1)
    {
        return v1.MapItems(Math.Log2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsLog10(this IReadOnlyList<double> v1)
    {
        return v1.MapItems(Math.Log10);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsLog(this IReadOnlyList<double> v1, double scalar)
    {
        return v1.MapItems(s => Math.Log(s, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsLog(this double scalar, IReadOnlyList<double> v1)
    {
        return v1.MapItems(s => Math.Log(scalar, s));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsPower(this IReadOnlyList<double> v1, double scalar)
    {
        return v1.MapItems(s => s.Power(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsPower(this double scalar, IReadOnlyList<double> v1)
    {
        return v1.MapItems(s => scalar.Power(s));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsCos(this IReadOnlyList<double> v1)
    {
        return v1.MapItems(Math.Cos);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsSin(this IReadOnlyList<double> v1)
    {
        return v1.MapItems(Math.Sin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsTan(this IReadOnlyList<double> v1)
    {
        return v1.MapItems(Math.Tan);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsArcCos(this IReadOnlyList<double> v1)
    {
        return v1.MapItems(a => a.ArcCos());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsArcSin(this IReadOnlyList<double> v1)
    {
        return v1.MapItems(a => a.ArcSin());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsArcTan(this IReadOnlyList<double> v1)
    {
        return v1.MapItems(a => a.ArcTan());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsCosh(this IReadOnlyList<double> v1)
    {
        return v1.MapItems(Math.Cosh);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsSinh(this IReadOnlyList<double> v1)
    {
        return v1.MapItems(Math.Sinh);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[] ScalarsTanh(this IReadOnlyList<double> v1)
    {
        return v1.MapItems(Math.Tanh);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsNegative(this double[,] scalarsArray)
    {
        return scalarsArray.MapItems(
            a => -a
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsCos(this double[,] scalarsArray)
    {
        return scalarsArray.MapItems(
            Math.Cos
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsSin(this double[,] scalarsArray)
    {
        return scalarsArray.MapItems(
            Math.Sin
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsTan(this double[,] scalarsArray)
    {
        return scalarsArray.MapItems(
            Math.Tan
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsExp(this double[,] scalarsArray)
    {
        return scalarsArray.MapItems(
            Math.Exp
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsLog(this double[,] scalarsArray)
    {
        return scalarsArray.MapItems(
            a => a.LogE()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsLog10(this double[,] scalarsArray)
    {
        return scalarsArray.MapItems(
            Math.Log10
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsLog2(this double[,] scalarsArray)
    {
        return scalarsArray.MapItems(
            Math.Log2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsAbs(this double[,] scalarsArray)
    {
        return scalarsArray.MapItems(
            Math.Abs
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsSqrt(this double[,] scalarsArray)
    {
        return scalarsArray.MapItems(
            Math.Sqrt
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsSqrtOfAbs(this double[,] scalarsArray)
    {
        return scalarsArray.MapItems(
            a => a.SqrtOfAbs()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsLogE(this double[,] v1)
    {
        return v1.MapItems(a => a.LogE());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsLog(this double[,] v1, double scalar)
    {
        return v1.MapItems(s => Math.Log(s, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsLog(this double scalar, double[,] v1)
    {
        return v1.MapItems(s => Math.Log(scalar, s));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsPower(this double[,] v1, double scalar)
    {
        return v1.MapItems(s => s.Power(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsPower(this double scalar, double[,] v1)
    {
        return v1.MapItems(s => scalar.Power(s));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsArcCos(this double[,] v1)
    {
        return v1.MapItems(a => a.ArcCos());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsArcSin(this double[,] v1)
    {
        return v1.MapItems(a => a.ArcSin());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsArcTan(this double[,] v1)
    {
        return v1.MapItems(a => a.ArcTan());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsCosh(this double[,] v1)
    {
        return v1.MapItems(Math.Cosh);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsSinh(this double[,] v1)
    {
        return v1.MapItems(Math.Sinh);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] ScalarsTanh(this double[,] v1)
    {
        return v1.MapItems(Math.Tanh);
    }
}