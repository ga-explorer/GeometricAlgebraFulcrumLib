﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;
using MathNet.Numerics;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

/// <summary>
/// Utility static methods for double precision floating point numbers
/// </summary>
public static class Float64Utils
{
    public const double ZeroEpsilon = 1e-12;

    //public static double MachineEpsilon { get; }
    //    = Precision.PositiveMachineEpsilon;

    //public static double NormalizedPositiveMin { get; }
    //    = 2.22507385850720138e-308d;

    ///// <summary>
    ///// Tolerance value for distances in double precision
    ///// </summary>
    //public static double DefaultAccuracyPositive { get; }
    //    = 10.0d * Precision.DoublePrecision;

    //public static double DefaultAccuracyNegative { get; }
    //    = -10.0d * Precision.DoublePrecision;


    /// <summary>
    /// Compute the binomial coefficient (n, k). For more details see:
    /// http://csharphelper.com/blog/2014/08/calculate-the-binomial-coefficient-n-choose-k-efficiently-in-c/
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static double BinomialCoefficient(int n, int k)
    {
        var result = 1.0d;

        for (var i = 1; i <= k; i++)
        {
            result *= n - (k - i);
            result /= i;
        }

        return result;
    }

    /// <summary>
    /// Compute row n of Pascal's triangle. For more details see:
    /// http://csharphelper.com/blog/2016/02/calculate-a-row-of-pascals-triangle-in-c/ 
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static double[] PascalTriangleRow(int n)
    {
        checked
        {
            var results = new double[n + 1];
            long value = 1;
            results[0] = 1.0d;
            results[n] = 1.0d;

            for (var i = 1; i < n; i++)
            {
                value *= n + 1 - i;
                value /= i;

                results[i] = value;
            }

            return results;
        }
    }

    /// <summary>
    /// Compute several consecutive rows of Pascal's triangle
    /// </summary>
    /// <param name="n1"></param>
    /// <param name="n2"></param>
    /// <returns></returns>
    public static List<double[]> PascalTriangleRows(int n1, int n2)
    {
        var prevRow = PascalTriangleRow(n1);
        var results = new List<double[]>(n2 - n1 + 1);
        results.Add(prevRow);

        for (var n = n1 + 1; n <= n2; n++)
        {
            var nextRow = new double[n + 1];
            nextRow[0] = 1.0d;
            nextRow[n] = 1.0d;

            for (var i = 1; i < n; i++)
                nextRow[i] = prevRow[i - 1] + prevRow[i];

            prevRow = nextRow;
            results.Add(prevRow);
        }

        return results;
    }


    ///// <summary>
    ///// use of machine zeroEpsilon to compare floating-point values for
    ///// equality
    ///// http://en.cppreference.com/w/cpp/types/numeric_limits/zeroEpsilon
    ///// </summary>
    ///// <param name="a"></param>
    ///// <param name="b"></param>
    ///// <param name="ulp"></param>
    ///// <returns></returns>
    //
    //public static bool IsAlmostEqual(this double a, double b, int ulp)
    //{
    //    Debug.Assert(!double.IsNaN(a) && !double.IsNaN(b));

    //    if (double.IsInfinity(a) || double.IsInfinity(b))
    //        return a == b;

    //    var absDiff = Math.Abs(a - b);

    //    // the machine zeroEpsilon has to be scaled to the magnitude of the
    //    // values used and multiplied by the desired precision in ULPs
    //    // (units in the last place) unless the result is subnormal
    //    return absDiff <= Precision.PositiveMachineEpsilon * Math.Abs(a + b) * ulp ||
    //           absDiff < NormalizedPositiveMin;
    //}

    
    public static bool IsNearEqual(this double a, double b, double zeroEpsilon = ZeroEpsilon)
    {
        Debug.Assert(!double.IsNaN(a) && !double.IsNaN(b));

        if (double.IsInfinity(a) || double.IsInfinity(b))
            return a == b;

        var x = a - b;

        return x >= -zeroEpsilon && x <= zeroEpsilon;
    }

    //
    //public static bool IsAlmostEqual(this double a, double b)
    //{
    //    Debug.Assert(!double.IsNaN(a) && !double.IsNaN(b));

    //    if (double.IsInfinity(a) || double.IsInfinity(b))
    //        return a == b;

    //    var x = a - b;

    //    return
    //        x >= DefaultAccuracyNegative &&
    //        x <= DefaultAccuracyPositive;
    //}

    ///// <summary>
    ///// True if the given values are equal relative to the default accuracy
    ///// </summary>
    ///// <param name="x1"></param>
    ///// <param name="x2"></param>
    ///// <returns></returns>
    //
    //public static bool IsAlmostEqual(this Complex x1, Complex x2)
    //{
    //    return (x1 - x2).Magnitude.IsAlmostZero();
    //}
        
    
    public static bool IsValid(this double value)
    {
        return !double.IsNaN(value);
    }

    
    public static bool IsInvalid(this double value)
    {
        return double.IsNaN(value);
    }

    
    public static bool IsFinite(this double value)
    {
        return !double.IsNaN(value) && !double.IsInfinity(value);
    }
    
    
    public static bool IsInteger(this double value)
    {
        return Math.Truncate(value) == value;
    }

    
    public static bool IsNonNegativeInteger(this double value)
    {
        return value >= 0 && value.IsInteger();
    }

    
    public static bool IsPositiveInteger(this double value)
    {
        return value > 0 && value.IsInteger();
    }

    
    public static bool IsNonPositiveInteger(this double value)
    {
        return value <= 0 && value.IsInteger();
    }

    
    public static bool IsNegativeInteger(this double value)
    {
        return value < 0 && value.IsInteger();
    }

    
    public static bool IsNearInRange(this double x, double value1, double value2, double zeroEpsilon = ZeroEpsilon)
    {
        Debug.Assert(
            !double.IsNaN(x) &&
            !double.IsNaN(value1) &&
            !double.IsNaN(value2) &&
            value1 < value2
        );

        return !(double.IsInfinity(x) || x < value1 - zeroEpsilon || x > value2 + zeroEpsilon);
    }

    //
    //public static bool IsDefiniteNotEqual(this double a, double b)
    //{
    //    if (double.IsInfinity(a) || double.IsInfinity(b))
    //        return a != b;

    //    if (double.IsNaN(a) || double.IsNaN(b))
    //        return false;

    //    var x = a - b;

    //    return
    //        x < DefaultAccuracyNegative ||
    //        x > DefaultAccuracyPositive;
    //}

    
    public static bool IsZero(this double x)
    {
        Debug.Assert(!double.IsNaN(x));

        return x == 0d;
    }
    
    
    public static bool IsZero(this double x, bool nearZeroFlag)
    {
        Debug.Assert(!double.IsNaN(x));

        return nearZeroFlag ? x.IsNearZero() : x == 0d;
    }

    
    public static bool IsNotZero(this double x)
    {
        return !double.IsNaN(x) && x is < 0 or > 0;
    }

    
    public static bool IsOne(this double x)
    {
        Debug.Assert(!double.IsNaN(x));

        return x == 1d;
    }

    
    public static bool IsMinusOne(this double x)
    {
        Debug.Assert(!double.IsNaN(x));

        return x == -1d;
    }
    
    
    public static bool IsNegativeOne(this double x)
    {
        Debug.Assert(!double.IsNaN(x));

        return x == -1d;
    }

    
    public static bool IsTwo(this double x)
    {
        Debug.Assert(!double.IsNaN(x));

        return x == 2d;
    }

    
    public static bool IsMinusTwo(this double x)
    {
        Debug.Assert(!double.IsNaN(x));

        return x == -2d;
    }

    ///// <summary>
    ///// True if the given value is near zero relative to the default accuracy
    ///// </summary>
    ///// <param name="x"></param>
    ///// <returns></returns>
    //
    //public static bool IsAlmostZero(this double x)
    //{
    //    Debug.Assert(!double.IsNaN(x));

    //    return !(
    //        double.IsInfinity(x) ||
    //        x < DefaultAccuracyNegative ||
    //        x > DefaultAccuracyPositive
    //    );
    //}

    
    public static bool IsNearZero(this double x, double zeroEpsilon = ZeroEpsilon)
    {
        Debug.Assert(
            !double.IsNaN(x) && 
            !double.IsNaN(zeroEpsilon) && 
            !double.IsInfinity(zeroEpsilon) && 
            zeroEpsilon >= 0
        );

        return !(double.IsInfinity(x) || x < -zeroEpsilon || x > zeroEpsilon);
    }

    
    public static bool IsNearOne(this double x, double zeroEpsilon = ZeroEpsilon)
    {
        return (x - 1d).IsNearZero(zeroEpsilon);
    }

    
    public static bool IsNearMinusOne(this double x, double zeroEpsilon = ZeroEpsilon)
    {
        return (x + 1d).IsNearZero(zeroEpsilon);
    }
    
    
    public static bool IsPositive(this double x)
    {
        Debug.Assert(!double.IsNaN(x));

        return x > 0d;
    }
    
    
    public static bool IsNegative(this double x)
    {
        Debug.Assert(!double.IsNaN(x));

        return x < 0d;
    }

    ///// <summary>
    ///// True if the given value is not near zero relative to the default accuracy
    ///// </summary>
    ///// <param name="x"></param>
    ///// <returns></returns>
    //
    //public static bool IsDefiniteNotZero(this double x)
    //{
    //    Debug.Assert(!double.IsNaN(x));

    //    return
    //        double.IsInfinity(x) ||
    //        x < DefaultAccuracyNegative ||
    //        x > DefaultAccuracyPositive;
    //}

    ///// <summary>
    ///// True if the given value is more than the default accuracy
    ///// </summary>
    ///// <param name="x"></param>
    ///// <returns></returns>
    //
    //public static bool IsDefinitePositive(this double x)
    //{
    //    Debug.Assert(!double.IsNaN(x));

    //    return x > DefaultAccuracyPositive;
    //}

    ///// <summary>
    ///// True if the given value is less than negative the default accuracy
    ///// </summary>
    ///// <param name="x"></param>
    ///// <returns></returns>
    //
    //public static bool IsDefiniteNegative(this double x)
    //{
    //    Debug.Assert(!double.IsNaN(x));

    //    return x < DefaultAccuracyNegative;
    //}

    ///// <summary>
    ///// True if the given value is more than or equal negative the default accuracy
    ///// </summary>
    ///// <param name="x"></param>
    ///// <returns></returns>
    //
    //public static bool IsAlmostNotPositive(this double x)
    //{
    //    Debug.Assert(!double.IsNaN(x));

    //    return x <= DefaultAccuracyPositive;
    //}

    ///// <summary>
    ///// True if the given value is more than or equal negative the default accuracy
    ///// </summary>
    ///// <param name="x"></param>
    ///// <returns></returns>
    //
    //public static bool IsAlmostNotNegative(this double x)
    //{
    //    Debug.Assert(!double.IsNaN(x));

    //    return x >= DefaultAccuracyNegative;
    //}

    
    public static bool IsInfinite(this double x)
    {
        return double.IsInfinity(x);
    }

    
    public static bool IsNaNOrInfinite(this double x)
    {
        return double.IsNaN(x) || double.IsInfinity(x);
    }

    
    public static bool IsNaN(this double x)
    {
        return double.IsNaN(x);
    }

    
    public static bool IsNotNaN(this double x)
    {
        return !double.IsNaN(x);
    }


    
    public static double Abs(this double number)
    {
        return Math.Abs(number);
    }

    
    public static double SqrtOfAbs(this double number)
    {
        return Math.Sqrt(Math.Abs(number));
    }

    
    public static double Floor(this double number)
    {
        return Math.Floor(number);
    }

    
    public static double Ceiling(this double number)
    {
        return Math.Ceiling(number);
    }

    
    public static double IntegerPart(this double number)
    {
        return Math.Truncate(number);
    }

    
    public static double FractionalPart(this double number)
    {
        return number - Math.Truncate(number);
    }

    
    public static double Round(this double number)
    {
        return Math.Round(number);
    }

    
    public static double Round(this double number, MidpointRounding mode)
    {
        return Math.Round(number, mode);
    }

    
    public static double Round(this double number, int decimalPlaces)
    {
        return Math.Round(number, decimalPlaces);
    }

    
    public static double Round(this double number, int decimalPlaces, MidpointRounding mode)
    {
        return Math.Round(number, decimalPlaces, mode);
    }
    
    
    public static int CeilingToInt32(this double number)
    {
        return (int)Math.Ceiling(number);
    }
    
    
    public static long CeilingToInt64(this double number)
    {
        return (long)Math.Ceiling(number);
    }

    
    public static int FloorToInt32(this double number)
    {
        return (int)Math.Floor(number);
    }
    
    
    public static long FloorToInt64(this double number)
    {
        return (long)Math.Floor(number);
    }
    
    
    public static byte RoundToByte(this double number)
    {
        return (byte)Math.Round(number);
    }

    
    public static int RoundToInt32(this double number)
    {
        return (int)Math.Round(number);
    }

    
    public static long RoundToInt64(this double number)
    {
        return (long)Math.Round(number);
    }

    
    public static double ClampToUnit(this double scalar)
    {
        return scalar switch
        {
            < 0.0d => 0.0d,
            > 1.0d => 1.0d,
            _ => scalar
        };
    }
    
    
    public static double ClampTo(this double scalar, double maxValue)
    {
        if (scalar < 0.0d) return 0.0d;
        if (scalar > maxValue) return maxValue;
        return scalar;
    }

    public static double ClampTo(this double scalar, double minValue, double maxValue)
    {
        if (scalar < minValue) return minValue;
        if (scalar > maxValue) return maxValue;
        return scalar;
    }

    public static double ClampToSymmetric(this double scalar, double maxValue)
    {
        if (scalar < -maxValue) return -maxValue;
        if (scalar > maxValue) return maxValue;
        return scalar;
    }


    
    public static double Clamp(this double number, double minValue, double maxValue)
    {
        if (number < minValue) return minValue;
        if (number > maxValue) return maxValue;

        return number;
    }

    
    public static IntegerSign Sign(this double number)
    {
        return IntegerSign.CreateFromInt32(
            Math.Sign(number)
        );
    }

    
    public static double Max(this double number1, double number2)
    {
        return Math.Max(number1, number2);
    }

    
    public static double Max(this double number1, double number2, double number3)
    {
        return Math.Max(number1, Math.Max(number2, number3));
    }

    
    public static double MaxMagnitude(this double number1, double number2)
    {
        return Math.Abs(number1) >= Math.Abs(number2) ? number1 : number2;
    }

    
    public static double MaxMagnitude(this double number1, double number2, double number3)
    {
        return number1.MaxMagnitude(number2).MaxMagnitude(number3);
    }

    
    public static double Min(this double number1, double number2)
    {
        return Math.Min(number1, number2);
    }

    
    public static double Min(this double number1, double number2, double number3)
    {
        return Math.Min(number1, Math.Min(number2, number3));
    }

    
    public static double MinMagnitude(this double number1, double number2)
    {
        return Math.Abs(number1) <= Math.Abs(number2) ? number1 : number2;
    }

    
    public static double MinMagnitude(this double number1, double number2, double number3)
    {
        return number1.MinMagnitude(number2).MinMagnitude(number3);
    }

    
    public static double BinaryScale(this double number, int power)
    {
        return number * Math.Pow(2, power);
    }

    
    public static double CopySign(this double magnitudeNumber, double signNumber)
    {
        return Math.Abs(magnitudeNumber) * Math.Sign(signNumber);
    }

    
    public static double Square(this double number)
    {
        return number * number;
    }

    
    public static double Cube(this double number)
    {
        return number * number * number;
    }

    
    public static double Sqrt(this double number)
    {
        return Math.Sqrt(number);
    }
        
    
    public static double SqrtOfInverse(this double number)
    {
        return Math.Sqrt(1d / number);
    }

    
    public static double InverseOfSqrt(this double number)
    {
        return 1d /  Math.Sqrt(number);
    }
    
    
    public static double Inverse(this double number)
    {
        return 1d /  number;
    }

    
    public static double Cbrt(this double number)
    {
        return Math.Pow(number, 1d/3);
    }

    
    public static double IEEERemainder(this double number1, double number2)
    {
        return Math.IEEERemainder(number1, number2);
    }

    
    public static double Power(this double number, double exponent)
    {
        return Math.Pow(number, exponent);
    }

    
    public static double Exp(this double number)
    {
        return Math.Exp(number);
    }

    
    public static double Log(this double number)
    {
        return Math.Log(number);
    }

    
    public static double Log(this double number, double baseNumber)
    {
        return Math.Log(number, baseNumber);
    }

    
    public static double Log2(this double number)
    {
        return Math.Log(number, 2);
    }

    
    public static int IntLog2(this double number)
    {
        if (number <= 0)
            return -1; // undefined for zero or negative numbers

        return (int)Math.Floor(Math.Log(number, 2));
    }

    
    public static double Log10(this double number)
    {
        return Math.Log10(number);
    }

    
    public static double LogE(this double number)
    {
        return Math.Log(number, Math.E);
    }

    
    public static double Sin(this double number)
    {
        return Math.Sin(number);
    }

    
    public static double Cos(this double number)
    {
        return Math.Cos(number);
    }

    
    public static ValueTuple<double, double> SinCos(this double number)
    {
        return (Math.Sin(number), Math.Cos(number));
    }

    
    public static double Tan(this double number)
    {
        return Math.Tan(number);
    }
    
    
    public static double Csc(this double number)
    {
        return 1d / Math.Sin(number);
    }

    
    public static double Sec(this double number)
    {
        return 1d / Math.Cos(number);
    }
    
    
    public static double Cot(this double number)
    {
        return 1d / Math.Tan(number);
    }

    
    public static double ArcCos(this double number)
    {
        Debug.Assert(number is >= -1 and <= 1);

        var angle = Math.Acos(number);

        //if (angle < 0) angle += (2 * Math.PI);

        return angle;
    }

    
    public static double ArcSin(this double number)
    {
        Debug.Assert(number is >= -1 and <= 1);

        var angle = Math.Asin(number);

        if (angle < 0) angle += 2 * Math.PI;

        return angle;
    }

    
    public static double ArcTan(this double number)
    {
        var angle = Math.Atan(number);

        if (angle < 0) angle += 2 * Math.PI;

        return angle;
    }

    
    public static double ArcTan2(this double numberX, double numberY)
    {
        var angle = Math.Atan2(numberY, numberX);

        if (angle < 0) angle += 2 * Math.PI;

        return angle;
    }

    
    public static double Sinh(this double number)
    {
        return Math.Sinh(number);
    }

    
    public static double Cosh(this double number)
    {
        return Math.Cosh(number);
    }

    
    public static double Tanh(this double number)
    {
        return Math.Tanh(number);
    }

    
    public static double ArcCosh(this double number)
    {
        if (number < 1.0)
            throw new ArgumentOutOfRangeException(nameof(number), "Value must be >= 1");

        return Math.Log(number + Math.Sqrt(number * number - 1.0));
    }

    
    public static double ArcSinh(this double number)
    {
        return Math.Log(number + Math.Sqrt(number * number + 1.0));
    }

    
    public static double ArcTanh(this double number)
    {
        if (number is <= -1.0 or >= 1.0)
            throw new ArgumentOutOfRangeException(nameof(number), "Value must be in the interval (-1, 1).");

        return 0.5 * Math.Log((1.0 + number) / (1.0 - number));
    }

    
    public static double NaNToZero(this double number)
    {
        return double.IsNaN(number) ? 0d : number;
    }

    
    public static double NaNInfinityToZero(this double number)
    {
        return double.IsNaN(number) || double.IsInfinity(number)
            ? 0d : number;
    }

    
    public static double NearZeroToZero(this double number, double zeroEpsilon = ZeroEpsilon)
    {
        return number.IsNearZero(zeroEpsilon) ? 0d : number;
    }

    
    
    public static double LerpRemap(this double value, double inputValue1, double inputValue2, double outputValue1, double outputValue2)
    {
        Debug.Assert(
            !double.IsNaN(inputValue1) &&
            !double.IsNaN(inputValue2) &&
            !double.IsNaN(outputValue1) &&
            !double.IsNaN(outputValue2) &&
            !double.IsNaN(value)
        );

        var t = (value - inputValue1) / (inputValue2 - inputValue1);
        return (1.0d - t) * outputValue1 + t * outputValue2;
    }

    
    public static double LerpInv(this double value, double v1, double v2)
    {
        Debug.Assert(!double.IsNaN(v1) && !double.IsNaN(v2) && !double.IsNaN(value));

        return (value - v1) / (v2 - v1);
    }
        
    
    public static double Lerp(this double t, IPair<double> valuePair)
    {
        var v1 = valuePair.Item1;
        var v2 = valuePair.Item2;

        Debug.Assert(!double.IsNaN(v1) && !double.IsNaN(v2) && !double.IsNaN(t));

        return (1.0d - t) * v1 + t * v2;
    }
    
    
    public static double Lerp(this double t, double v1, double v2)
    {
        Debug.Assert(!double.IsNaN(v1) && !double.IsNaN(v2) && !double.IsNaN(t));

        return (1.0d - t) * v1 + t * v2;
    }

    
    public static IEnumerable<double> Lerp(this IEnumerable<double> tList, double v1, double v2)
    {
        return tList.Select(
            t => (1.0d - t) * v1 + t * v2
        );
    }


    /// <summary>
    /// Convert a double precision number into a string of bits
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static string ToBitPattern(this double x)
    {
        var bytesArray = BitConverter.GetBytes(x);

        var s = new StringBuilder(80);

        foreach (var n in bytesArray)
        {
            for (var j = 0; j <= 3; j++)
                s.Append((1 << j & n) > 0 ? "1" : "0");

            s.Append(" ");

            for (var j = 4; j <= 7; j++)
                s.Append((1 << j & n) > 0 ? "1" : "0");

            s.Append(" ");
        }

        return s.ToString();
    }


    public static double Geomma1 { get; } = ErrorGeomma(1);

    public static double Geomma2 { get; } = ErrorGeomma(2);

    public static double Geomma3 { get; } = ErrorGeomma(3);

    public static double Geomma4 { get; } = ErrorGeomma(4);

    public static double Geomma5 { get; } = ErrorGeomma(5);

    public static double Geomma6 { get; } = ErrorGeomma(6);

    public static double Geomma7 { get; } = ErrorGeomma(7);

    public static double ErrorGeomma(int n)
    {
        var s = n * Precision.PositiveMachineEpsilon;

        return s / (1.0d - s);
    }


    public static Pair<double> GetRange(this IEnumerable<double> valueList)
    {
        var minValue = double.PositiveInfinity;
        var maxValue = double.NegativeInfinity;

        foreach (var value in valueList)
        {
            if (value < minValue) minValue = value;
            if (value > maxValue) maxValue = value;
        }

        return new Pair<double>(minValue, maxValue);
    }

    
    public static IEnumerable<double> GetLinearRange(this double start, double finish, int count, bool isPeriodicRange = false)
    {
        var length = finish - start;
        var n = isPeriodicRange
            ? length / count
            : length / (count - 1);

        return Enumerable
            .Range(0, count)
            .Select(i => start + i * n);
    }
        
    
    public static IEnumerable<double> GetLinearPeriodicRange(this double start, double finish, int count)
    {
        var length = finish - start;
        var n = length / count;

        return Enumerable
            .Range(0, count)
            .Select(i => start + i * n);
    }

    
    public static double CosWave(this double t, double value1, double value2, int cycleCount = 1)
    {
        t = (cycleCount * t).ClampPeriodic(1);

        return value1 + 0.5d * (1d - Math.Cos(2 * Math.PI * t)) * (value2 - value1);
    }
        
    
    public static double CosWave(this double t, IPair<double> valuePair, int cycleCount = 1)
    {
        var value1 = valuePair.Item1;
        var value2 = valuePair.Item2;
            
        t = (cycleCount * t).ClampPeriodic(1);

        return value1 + 0.5d * (1d - Math.Cos(2 * Math.PI * t)) * (value2 - value1);
    }
    
    
    public static double TriangleWave(this double t, double value1, double value2, int cycleCount = 1)
    {
        t = (cycleCount * t).ClampPeriodic(1);

        return t <= 0.5d
            ? value1 + 2 * t * (value2 - value1)
            : value1 + 2 * (1d - t) * (value2 - value1);
    }
        
    
    public static double TriangleWave(this double t, IPair<double> valuePair, int cycleCount = 1)
    {
        var value1 = valuePair.Item1;
        var value2 = valuePair.Item2;
            
        t = (cycleCount * t).ClampPeriodic(1);

        return t <= 0.5d
            ? value1 + 2 * t * (value2 - value1)
            : value1 + 2 * (1d - t) * (value2 - value1);
    }
    
    /// <summary>
    /// Go from start to finish then to start again following a cos shape
    /// </summary>
    /// <param name="start"></param>
    /// <param name="finish"></param>
    /// <param name="sampleCount"></param>
    /// <param name="cycleCount"></param>
    /// <param name="isPeriodicRange"></param>
    /// <returns></returns>
    
    public static IEnumerable<double> GetCosRange(this double start, double finish, int sampleCount, int cycleCount = 1, bool isPeriodicRange = false)
    {
        var length = finish - start;
        var n = isPeriodicRange
            ? 2 * Math.PI * cycleCount / sampleCount
            : 2 * Math.PI * cycleCount / (sampleCount - 1);

        return Enumerable
            .Range(0, sampleCount)
            .Select(i => start + 0.5d * (1d - Math.Cos(i * n)) * length);
    }


    //
    //public static bool IsInvalid(this IGeometricElement element)
    //{
    //    return !element.IsValid();
    //}

    
    public static double GetFractionPart(this double value)
    {
        return value - Math.Truncate(value);
    }

    
    public static double ToNearestInteger(this double value, ScalarToIntOption conversionMethod)
    {
        return conversionMethod switch
        {
            ScalarToIntOption.Round => Math.Round(value),
            ScalarToIntOption.Ceiling => Math.Ceiling(value),
            ScalarToIntOption.Floor => Math.Floor(value),
            _ => throw new ArgumentOutOfRangeException(nameof(conversionMethod), conversionMethod, null)
        };
    }
    
    
    
    public static double ClampPeriodic(this double value, double minValue, double maxValue)
    {
        return (value - minValue).ClampPeriodic(maxValue - minValue) + minValue;
    }

    
    public static double ClampPeriodic(this double value, double maxValue)
    {
        //Make sure maxValue > 0
        Debug.Assert(maxValue > 0);

        //value < -maxValue
        if (value < -maxValue)
            return value + Math.Ceiling(-value / maxValue) * maxValue;

        //-maxValue <= value < 0
        if (value < 0)
            return value + maxValue;

        //value > maxValue
        if (value > maxValue)
            return value - Math.Truncate(value / maxValue) * maxValue;

        //0 <= value <= maxValue
        return value;
    }
    
    
    public static double ClampPeriodicToUnit(this double value)
    {
        //value < -maxValue
        if (value < -1)
            return value + Math.Ceiling(-value);

        //-maxValue <= value < 0
        if (value < 0)
            return value + 1;

        //value > maxValue
        if (value > 1)
            return value - Math.Truncate(value);

        //0 <= value <= maxValue
        return value;
    }

    
    public static double Clamp(this double value, double maxValue)
    {
        if (value < 0) return 0;

        return value > maxValue ? maxValue : value;
    }

    
    public static int ClampInt(this int value, int maxValue)
    {
        if (value < 0) return 0;

        return value > maxValue ? maxValue : value;
    }

    
    public static int ClampToInt(this double value, int maxValue)
    {
        if (value < 0) return 0;

        return value > maxValue ? maxValue : (int)value;
    }

    
    public static ulong ClampToULong(this double value, ulong maxValue)
    {
        if (value < 0ul) return 0ul;

        return value > maxValue ? maxValue : (ulong)value;
    }


    
    public static bool HasSameSignAs(this double value1, double value2)
    {
        return value1 >= 0 && value2 >= 0 ||
               value1 <= 0 && value2 <= 0;
    }

    /// <summary>
    /// https://www.youtube.com/watch?v=vD5g8aVscUI
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    
    public static double SmoothUnitStep(this double t)
    {
        //TODO: use this for Fourier interpolation of non-periodic signals
        if (t <= 0) return 0;
        if (t >= 1) return 1;

        var e1 = Math.Exp(-1d / t);
        var e2 = Math.Exp(-1d / (1d - t));

        return e1 / (e1 + e2);
    }
}