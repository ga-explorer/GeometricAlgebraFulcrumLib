using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using MathNet.Numerics;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Float32;

public static class Float32Utils
{

    //public static float MachineEpsilon { get; }
    //    = Precision.PositiveMachineEpsilon;

    //public static float NormalizedPositiveMin { get; }
    //    = 2.22507385850720138e-308d;

    ///// <summary>
    ///// Tolerance value for distances in float precision
    ///// </summary>
    //public static float DefaultAccuracyPositive { get; }
    //    = 10.0d * Precision.DoublePrecision;

    //public static float DefaultAccuracyNegative { get; }
    //    = -10.0d * Precision.DoublePrecision;


    /// <summary>
    /// Compute the binomial coefficient (n, k). For more details see:
    /// http://csharphelper.com/blog/2014/08/calculate-the-binomial-coefficient-n-choose-k-efficiently-in-c/
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static float BinomialCoefficient(int n, int k)
    {
        var result = 1.0d;

        for (var i = 1; i <= k; i++)
        {
            result *= n - (k - i);
            result /= i;
        }

        return (float)result;
    }

    /// <summary>
    /// Compute row n of Pascal's triangle. For more details see:
    /// http://csharphelper.com/blog/2016/02/calculate-a-row-of-pascals-triangle-in-c/ 
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static float[] PascalTriangleRow(int n)
    {
        checked
        {
            var results = new float[n + 1];
            long value = 1;
            results[0] = 1.0f;
            results[n] = 1.0f;

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
    public static List<float[]> PascalTriangleRows(int n1, int n2)
    {
        var prevRow = PascalTriangleRow(n1);
        var results = new List<float[]>(n2 - n1 + 1);
        results.Add(prevRow);

        for (var n = n1 + 1; n <= n2; n++)
        {
            var nextRow = new float[n + 1];
            nextRow[0] = 1.0f;
            nextRow[n] = 1.0f;

            for (var i = 1; i < n; i++)
                nextRow[i] = prevRow[i - 1] + prevRow[i];

            prevRow = nextRow;
            results.Add(prevRow);
        }

        return results;
    }


    ///// <summary>
    ///// use of machine epsilon to compare floating-point values for
    ///// equality
    ///// http://en.cppreference.com/w/cpp/types/numeric_limits/epsilon
    ///// </summary>
    ///// <param name="a"></param>
    ///// <param name="b"></param>
    ///// <param name="ulp"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsAlmostEqual(this float a, float b, int ulp)
    //{
    //    Debug.Assert(!float.IsNaN(a) && !float.IsNaN(b));

    //    if (float.IsInfinity(a) || float.IsInfinity(b))
    //        return a == b;

    //    var absDiff = Math.Abs(a - b);

    //    // the machine epsilon has to be scaled to the magnitude of the
    //    // values used and multiplied by the desired precision in ULPs
    //    // (units in the last place) unless the result is subnormal
    //    return absDiff <= Precision.PositiveMachineEpsilon * Math.Abs(a + b) * ulp ||
    //           absDiff < NormalizedPositiveMin;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqual(this float a, float b, float epsilon = 1e-7f)
    {
        Debug.Assert(!float.IsNaN(a) && !float.IsNaN(b));

        if (float.IsInfinity(a) || float.IsInfinity(b))
            return a == b;

        var x = a - b;

        return x >= -epsilon && x <= epsilon;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsAlmostEqual(this float a, float b)
    //{
    //    Debug.Assert(!float.IsNaN(a) && !float.IsNaN(b));

    //    if (float.IsInfinity(a) || float.IsInfinity(b))
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
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsAlmostEqual(this Complex x1, Complex x2)
    //{
    //    return (x1 - x2).Magnitude.IsAlmostZero();
    //}
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValid(this float value)
    {
        return !float.IsNaN(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInvalid(this float value)
    {
        return float.IsNaN(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInteger(this float value)
    {
        return MathF.Truncate(value) == value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNonNegativeInteger(this float value)
    {
        return value >= 0 && value.IsInteger();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPositiveInteger(this float value)
    {
        return value > 0 && value.IsInteger();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNonPositiveInteger(this float value)
    {
        return value <= 0 && value.IsInteger();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegativeInteger(this float value)
    {
        return value < 0 && value.IsInteger();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearInRange(this float x, float value1, float value2, float epsilon = 1e-7f)
    {
        Debug.Assert(
            !float.IsNaN(x) &&
            !float.IsNaN(value1) &&
            !float.IsNaN(value2) &&
            value1 < value2
        );

        return !(float.IsInfinity(x) || x < value1 - epsilon || x > value2 + epsilon);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsDefiniteNotEqual(this float a, float b)
    //{
    //    if (float.IsInfinity(a) || float.IsInfinity(b))
    //        return a != b;

    //    if (float.IsNaN(a) || float.IsNaN(b))
    //        return false;

    //    var x = a - b;

    //    return
    //        x < DefaultAccuracyNegative ||
    //        x > DefaultAccuracyPositive;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(this float x)
    {
        Debug.Assert(!float.IsNaN(x));

        return x == 0d;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(this float x, bool nearZeroFlag)
    {
        Debug.Assert(!float.IsNaN(x));

        return nearZeroFlag ? x.IsNearZero() : x == 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotZero(this float x)
    {
        return !float.IsNaN(x) && x is < 0 or > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOne(this float x)
    {
        Debug.Assert(!float.IsNaN(x));

        return x == 1d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMinusOne(this float x)
    {
        Debug.Assert(!float.IsNaN(x));

        return x == -1d;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsTwo(this float x)
    {
        Debug.Assert(!float.IsNaN(x));

        return x == 2d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMinusTwo(this float x)
    {
        Debug.Assert(!float.IsNaN(x));

        return x == -2d;
    }

    ///// <summary>
    ///// True if the given value is near zero relative to the default accuracy
    ///// </summary>
    ///// <param name="x"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsAlmostZero(this float x)
    //{
    //    Debug.Assert(!float.IsNaN(x));

    //    return !(
    //        float.IsInfinity(x) ||
    //        x < DefaultAccuracyNegative ||
    //        x > DefaultAccuracyPositive
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero(this float x, float epsilon = 1e-7f)
    {
        Debug.Assert(
            !float.IsNaN(x) && 
            !float.IsNaN(epsilon) && 
            float.IsFinite(epsilon) && 
            epsilon >= 0
        );

        return !(float.IsInfinity(x) || x < -epsilon || x > epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOne(this float x, float epsilon = 1e-7f)
    {
        return (x - 1f).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearMinusOne(this float x, float epsilon = 1e-7f)
    {
        return (x + 1f).IsNearZero(epsilon);
    }

    ///// <summary>
    ///// True if the given value is not near zero relative to the default accuracy
    ///// </summary>
    ///// <param name="x"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsDefiniteNotZero(this float x)
    //{
    //    Debug.Assert(!float.IsNaN(x));

    //    return
    //        float.IsInfinity(x) ||
    //        x < DefaultAccuracyNegative ||
    //        x > DefaultAccuracyPositive;
    //}

    ///// <summary>
    ///// True if the given value is more than the default accuracy
    ///// </summary>
    ///// <param name="x"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsDefinitePositive(this float x)
    //{
    //    Debug.Assert(!float.IsNaN(x));

    //    return x > DefaultAccuracyPositive;
    //}

    ///// <summary>
    ///// True if the given value is less than negative the default accuracy
    ///// </summary>
    ///// <param name="x"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsDefiniteNegative(this float x)
    //{
    //    Debug.Assert(!float.IsNaN(x));

    //    return x < DefaultAccuracyNegative;
    //}

    ///// <summary>
    ///// True if the given value is more than or equal negative the default accuracy
    ///// </summary>
    ///// <param name="x"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsAlmostNotPositive(this float x)
    //{
    //    Debug.Assert(!float.IsNaN(x));

    //    return x <= DefaultAccuracyPositive;
    //}

    ///// <summary>
    ///// True if the given value is more than or equal negative the default accuracy
    ///// </summary>
    ///// <param name="x"></param>
    ///// <returns></returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsAlmostNotNegative(this float x)
    //{
    //    Debug.Assert(!float.IsNaN(x));

    //    return x >= DefaultAccuracyNegative;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInfinite(this float x)
    {
        return float.IsInfinity(x);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNaNOrInfinite(this float x)
    {
        return float.IsNaN(x) || float.IsInfinity(x);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNaN(this float x)
    {
        return float.IsNaN(x);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotNaN(this float x)
    {
        return !float.IsNaN(x);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Abs(this float number)
    {
        return Math.Abs(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float SqrtOfAbs(this float number)
    {
        return (float)Math.Sqrt(MathF.Abs(number));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Floor(this float number)
    {
        return MathF.Floor(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Ceiling(this float number)
    {
        return MathF.Ceiling(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float IntegerPart(this float number)
    {
        return MathF.Truncate(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float FractionalPart(this float number)
    {
        return (float)(number - Math.Truncate(number));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Round(this float number)
    {
        return MathF.Round(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Round(this float number, MidpointRounding mode)
    {
        return MathF.Round(number, mode);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Round(this float number, int decimalPlaces)
    {
        return MathF.Round(number, decimalPlaces);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Round(this float number, int decimalPlaces, MidpointRounding mode)
    {
        return MathF.Round(number, decimalPlaces, mode);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RoundToInt32(this float number)
    {
        return (int)MathF.Round(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long RoundToInt64(this float number)
    {
        return (long)MathF.Round(number);
    }
    public static float ClampToUnit(this float scalar)
    {
        if (scalar < 0.0d) return 0.0f;
        if (scalar > 1.0d) return 1.0f;
        return scalar;
    }

    public static float ClampTo(this float scalar, float maxValue)
    {
        if (scalar < 0.0d) return 0.0f;
        if (scalar > maxValue) return maxValue;
        return scalar;
    }

    public static float ClampTo(this float scalar, float minValue, float maxValue)
    {
        if (scalar < minValue) return minValue;
        if (scalar > maxValue) return maxValue;
        return scalar;
    }

    public static float ClampToSymmetric(this float scalar, float maxValue)
    {
        if (scalar < -maxValue) return -maxValue;
        if (scalar > maxValue) return maxValue;
        return scalar;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Clamp(this float number, float minValue, float maxValue)
    {
        return Math.Clamp(number, minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign Sign(this float number)
    {
        return IntegerSign.CreateFromInt32(
            Math.Sign(number)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Max(this float number1, float number2)
    {
        return Math.Max(number1, number2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Max(this float number1, float number2, float number3)
    {
        return Math.Max(number1, Math.Max(number2, number3));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float MaxMagnitude(this float number1, float number2)
    {
        return MathF.MaxMagnitude(number1, number2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float MaxMagnitude(this float number1, float number2, float number3)
    {
        return MathF.MaxMagnitude(number1, MathF.MaxMagnitude(number2, number3));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Min(this float number1, float number2)
    {
        return MathF.Min(number1, number2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Min(this float number1, float number2, float number3)
    {
        return MathF.Min(number1, MathF.Min(number2, number3));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float MinMagnitude(this float number1, float number2)
    {
        return MathF.MinMagnitude(number1, number2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float MinMagnitude(this float number1, float number2, float number3)
    {
        return MathF.MinMagnitude(number1, MathF.MinMagnitude(number2, number3));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float BinaryScale(this float number, int power)
    {
        return MathF.ScaleB(number, power);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float CopySign(this float magnitudeNumber, float signNumber)
    {
        return MathF.CopySign(magnitudeNumber, signNumber);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Square(this float number)
    {
        return number * number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Cube(this float number)
    {
        return number * number * number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Sqrt(this float number)
    {
        return MathF.Sqrt(number);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float SqrtOfInverse(this float number)
    {
        return (float)Math.Sqrt(1d / number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InverseOfSqrt(this float number)
    {
        return (float)(1d /  Math.Sqrt(number));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Cbrt(this float number)
    {
        return (float)Math.Cbrt(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float IEEERemainder(this float number1, float number2)
    {
        return (float)Math.IEEERemainder(number1, number2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Power(this float number, float exponent)
    {
        return (float)Math.Pow(number, exponent);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Exp(this float number)
    {
        return (float)Math.Exp(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Log(this float number)
    {
        return (float)Math.Log(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Log(this float number, float baseNumber)
    {
        return (float)Math.Log(number, baseNumber);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Log2(this float number)
    {
        return (float)Math.Log2(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IntLog2(this float number)
    {
        return Math.ILogB(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Log10(this float number)
    {
        return (float)Math.Log10(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float LogE(this float number)
    {
        return (float)Math.Log(number, Math.E);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Sin(this float number)
    {
        return (float)Math.Sin(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Cos(this float number)
    {
        return (float)Math.Cos(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ValueTuple<float, float> SinCos(this float number)
    {
        return ((float)Math.Sin(number), (float)Math.Cos(number));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Tan(this float number)
    {
        return (float)Math.Tan(number);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Csc(this float number)
    {
        return (float)(1d / Math.Sin(number));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Sec(this float number)
    {
        return (float)(1d / Math.Cos(number));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Cot(this float number)
    {
        return (float)(1d / Math.Tan(number));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ArcCos(this float number)
    {
        Debug.Assert(number is >= -1 and <= 1);

        var angle = Math.Acos(number);

        //if (angle < 0) angle += 2 * Math.PI;

        return (float)angle;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ArcSin(this float number)
    {
        Debug.Assert(number is >= -1 and <= 1);

        var angle = Math.Asin(number);

        if (angle < 0) angle += 2 * Math.PI;

        return (float)angle;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ArcTan(this float number)
    {
        var angle = Math.Atan(number);

        if (angle < 0) angle += 2 * Math.PI;

        return (float)angle;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ArcTan2(this float numberX, float numberY)
    {
        var angle = Math.Atan2(numberY, numberX);

        if (angle < 0) angle += 2 * Math.PI;

        return (float)angle;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Sinh(this float number)
    {
        return (float)Math.Sinh(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Cosh(this float number)
    {
        return (float)Math.Cosh(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Tanh(this float number)
    {
        return (float)Math.Tanh(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ArcCosh(this float number)
    {
        return (float)Math.Acosh(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ArcSinh(this float number)
    {
        return (float)Math.Asinh(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ArcTanh(this float number)
    {
        return (float)Math.Atanh(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float NaNToZero(this float number)
    {
        return float.IsNaN(number) ? 0f : number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float NaNInfinityToZero(this float number)
    {
        return float.IsNaN(number) || float.IsInfinity(number)
            ? 0f : number;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float NearZeroToZero(this float number, float epsilon)
    {
        return number.IsNearZero(epsilon) ? 0f : number;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float LerpRemap(this float value, float inputValue1, float inputValue2, float outputValue1, float outputValue2)
    {
        Debug.Assert(
            !float.IsNaN(inputValue1) &&
            !float.IsNaN(inputValue2) &&
            !float.IsNaN(outputValue1) &&
            !float.IsNaN(outputValue2) &&
            !float.IsNaN(value)
        );

        var t = ((double)value - inputValue1) / (inputValue2 - inputValue1);
        return (float)((1.0d - t) * outputValue1 + t * outputValue2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float LerpInv(this float value, float v1, float v2)
    {
        Debug.Assert(!float.IsNaN(v1) && !float.IsNaN(v2) && !float.IsNaN(value));

        return (float)(((double)value - v1) / (v2 - v1));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Lerp(this float t, IPair<float> valuePair)
    {
        var v1 = valuePair.Item1;
        var v2 = valuePair.Item2;

        Debug.Assert(!float.IsNaN(v1) && !float.IsNaN(v2) && !float.IsNaN(t));

        return (float)((1.0d - t) * v1 + t * v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Lerp(this float t, float v1, float v2)
    {
        Debug.Assert(!float.IsNaN(v1) && !float.IsNaN(v2) && !float.IsNaN(t));

        return (float)((1.0d - t) * v1 + t * v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<float> Lerp(this IEnumerable<float> tList, float v1, float v2)
    {
        return tList.Select(
            t => (float)((1.0d - t) * v1 + t * v2)
        );
    }



    public static float Geomma1 { get; } = ErrorGeomma(1);

    public static float Geomma2 { get; } = ErrorGeomma(2);

    public static float Geomma3 { get; } = ErrorGeomma(3);

    public static float Geomma4 { get; } = ErrorGeomma(4);

    public static float Geomma5 { get; } = ErrorGeomma(5);

    public static float Geomma6 { get; } = ErrorGeomma(6);

    public static float Geomma7 { get; } = ErrorGeomma(7);

    public static float ErrorGeomma(int n)
    {
        var s = n * Precision.PositiveMachineEpsilon;

        return (float)(s / (1.0d - s));
    }


    public static Pair<float> GetRange(this IEnumerable<float> valueList)
    {
        var minValue = float.PositiveInfinity;
        var maxValue = float.NegativeInfinity;

        foreach (var value in valueList)
        {
            if (value < minValue) minValue = value;
            if (value > maxValue) maxValue = value;
        }

        return new Pair<float>(minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<float> GetLinearRange(this float start, float finish, int count, bool isPeriodicRange = false)
    {
        var length = finish - start;
        var n = isPeriodicRange
            ? length / count
            : length / (count - 1);

        return Enumerable
            .Range(0, count)
            .Select(i => start + i * n);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<float> GetLinearPeriodicRange(this float start, float finish, int count)
    {
        var length = finish - start;
        var n = length / count;

        return Enumerable
            .Range(0, count)
            .Select(i => start + i * n);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float CosWave(this float t, float value1, float value2, int cycleCount = 1)
    {
        t = (cycleCount * t).ClampPeriodic(1);

        return (float)(value1 + 0.5d * (1d - Math.Cos(2d * Math.PI * t)) * (value2 - value1));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float CosWave(this float t, IPair<float> valuePair, int cycleCount = 1)
    {
        var value1 = valuePair.Item1;
        var value2 = valuePair.Item2;
            
        t = (cycleCount * t).ClampPeriodic(1);

        return (float)(value1 + 0.5d * (1d - Math.Cos(2d * Math.PI * t)) * (value2 - value1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float TriangleWave(this float t, float value1, float value2, int cycleCount = 1)
    {
        var t1 = (cycleCount * (double)t).ClampPeriodic(1);

        return (float)(t1 <= 0.5d
            ? value1 + 2 * t1 * (value2 - value1)
            : value1 + 2 * (1d - t1) * (value2 - value1));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float TriangleWave(this float t, IPair<float> valuePair, int cycleCount = 1)
    {
        var value1 = valuePair.Item1;
        var value2 = valuePair.Item2;
            
        var t1 = (cycleCount * (double)t).ClampPeriodic(1);

        return (float)(t1 <= 0.5d
            ? value1 + 2 * t1 * (value2 - value1)
            : value1 + 2 * (1d - t1) * (value2 - value1));
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<float> GetCosRange(this float start, float finish, int sampleCount, int cycleCount = 1, bool isPeriodicRange = false)
    {
        var length = finish - start;
        var n = isPeriodicRange
            ? 2d * Math.PI * cycleCount / sampleCount
            : 2d * Math.PI * cycleCount / (sampleCount - 1);

        return Enumerable
            .Range(0, sampleCount)
            .Select(i => (float)(start + 0.5d * (1d - Math.Cos(i * n)) * length));
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsInvalid(this IGeometricElement element)
    //{
    //    return !element.IsValid();
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float GetFractionPart(this float value)
    {
        return value - MathF.Truncate(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ToNearestInteger(this float value, ScalarToIntOption conversionMethod)
    {
        return conversionMethod switch
        {
            ScalarToIntOption.Round => MathF.Round(value),
            ScalarToIntOption.Ceiling => MathF.Ceiling(value),
            ScalarToIntOption.Floor => MathF.Floor(value),
            _ => throw new ArgumentOutOfRangeException(nameof(conversionMethod), conversionMethod, null)
        };
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ClampPeriodic(this float value, float minValue, float maxValue)
    {
        return (value - minValue).ClampPeriodic(maxValue - minValue) + minValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ClampPeriodic(this float value, float maxValue)
    {
        //Make sure maxValue > 0
        Debug.Assert(maxValue > 0);

        //value < -maxValue
        if (value < -maxValue)
            return value + MathF.Ceiling(-value / maxValue) * maxValue;

        //-maxValue <= value < 0
        if (value < 0)
            return value + maxValue;

        //value > maxValue
        if (value > maxValue)
            return value - MathF.Truncate(value / maxValue) * maxValue;

        //0 <= value <= maxValue
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Clamp(this float value, float maxValue)
    {
        if (value < 0) return 0;

        return value > maxValue ? maxValue : value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ClampInt(this int value, int maxValue)
    {
        if (value < 0) return 0;

        return value > maxValue ? maxValue : value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ClampToInt(this float value, int maxValue)
    {
        if (value < 0) return 0;

        return value > maxValue ? maxValue : (int)value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong ClampToULong(this float value, ulong maxValue)
    {
        if (value < 0ul) return 0ul;

        return value > maxValue ? maxValue : (ulong)value;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasSameSignAs(this float value1, float value2)
    {
        return value1 >= 0 && value2 >= 0 ||
               value1 <= 0 && value2 <= 0;
    }

    /// <summary>
    /// https://www.youtube.com/watch?v=vD5g8aVscUI
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float SmoothUnitStep(this float t)
    {
        //TODO: use this for Fourier interpolation of non-periodic signals
        if (t <= 0) return 0;
        if (t >= 1) return 1;

        var e1 = Math.Exp(-1d / t);
        var e2 = Math.Exp(-1d / (1d - t));

        return (float)(e1 / (e1 + e2));
    }


    /// <summary>
    /// Convert a single precision number into a string of bits
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static string ToBitPattern(this float x)
    {
        var bytesArray = BitConverter.GetBytes(x);

        var s = new StringBuilder(40);

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
}