﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using NumericalGeometryLib.BasicMath.Tuples;
using MathNet.Numerics;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace NumericalGeometryLib.BasicMath
{
    /// <summary>
    /// Utility static methods for double precision floating point numbers
    /// </summary>
    public static class Float64Utils
    {
        public static double MachineEpsilon { get; } 
            = Precision.PositiveMachineEpsilon;

        public static double NormalizedPositiveMin { get; }
            = 2.22507385850720138e-308d;

        /// <summary>
        /// Tolerance value for distances in double precision
        /// </summary>
        public static double DefaultAccuracyPositive { get; }
            = 10.0d * Precision.DoublePrecision;

        public static double DefaultAccuracyNegative { get; }
            = -10.0d * Precision.DoublePrecision;

        
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


        /// <summary>
        /// use of machine epsilon to compare floating-point values for
        /// equality
        /// http://en.cppreference.com/w/cpp/types/numeric_limits/epsilon
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="ulp"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostEqual(this double a, double b, int ulp)
        {
            Debug.Assert(!double.IsNaN(a) && !double.IsNaN(b));

            if (double.IsInfinity(a) || double.IsInfinity(b))
                return a == b;

            var absDiff = Math.Abs(a - b);

            // the machine epsilon has to be scaled to the magnitude of the
            // values used and multiplied by the desired precision in ULPs
            // (units in the last place) unless the result is subnormal
            return absDiff <= Precision.PositiveMachineEpsilon * Math.Abs(a + b) * ulp || 
                   absDiff < NormalizedPositiveMin;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearEqual(this double a, double b, double epsilon = 1e-7)
        {
            Debug.Assert(!double.IsNaN(a) && !double.IsNaN(b));

            if (double.IsInfinity(a) || double.IsInfinity(b))
                return a == b;

            var x = a - b;

            return x >= -epsilon && x <= epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostEqual(this double a, double b)
        {
            Debug.Assert(!double.IsNaN(a) && !double.IsNaN(b));

            if (double.IsInfinity(a) || double.IsInfinity(b))
                return a == b;

            var x = a - b;

            return
                x >= DefaultAccuracyNegative && 
                x <= DefaultAccuracyPositive;
        }

        /// <summary>
        /// True if the given values are equal relative to the default accuracy
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostEqual(this Complex x1, Complex x2)
        {
            return (x1 - x2).Magnitude.IsAlmostZero();
        }
        
        /// <summary>
        /// True if the given values are equal relative to the default accuracy
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostZero(this ITuple3D x)
        {
            return
                IsAlmostZero(x.X) &&
                IsAlmostZero(x.Y) &&
                IsAlmostZero(x.Z);
        }

        /// <summary>
        /// True if the given values are equal relative to the default accuracy
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostEqual(this ITuple2D x1, ITuple2D x2)
        {
            return
                IsAlmostEqual(x1.X, x2.X) &&
                IsAlmostEqual(x1.Y, x2.Y);
        }
        
        /// <summary>
        /// True if the given values are equal relative to the default accuracy
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostEqual(this ITuple3D x1, ITuple3D x2)
        {
            return
                IsAlmostEqual(x1.X, x2.X) &&
                IsAlmostEqual(x1.Y, x2.Y) &&
                IsAlmostEqual(x1.Z, x2.Z);
        }
        
        /// <summary>
        /// True if the given values are equal relative to the default accuracy
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqual(this ITuple3D x1, ITuple3D x2)
        {
            return
                x1.X == x2.X &&
                x1.Y == x2.Y &&
                x1.Z == x2.Z;
        }
        
        /// <summary>
        /// True if the given values are equal relative to the default accuracy
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegativeEqual(this ITuple3D x1, ITuple3D x2)
        {
            return
                -x1.X == x2.X &&
                -x1.Y == x2.Y &&
                -x1.Z == x2.Z;
        }

        /// <summary>
        /// True if the given values are equal relative to the default accuracy
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostEqual(this ITuple4D x1, ITuple4D x2)
        {
            return
                IsAlmostEqual(x1.X, x2.X) &&
                IsAlmostEqual(x1.Y, x2.Y) &&
                IsAlmostEqual(x1.Z, x2.Z) &&
                IsAlmostEqual(x1.W, x2.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValid(this double value)
        {
            return !double.IsNaN(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInvalid(this double value)
        {
            return double.IsNaN(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDefiniteNotEqual(this double a, double b)
        {
            if (double.IsInfinity(a) || double.IsInfinity(b))
                return a != b;

            if (double.IsNaN(a) || double.IsNaN(b))
                return false;

            var x = a - b;

            return 
                x < DefaultAccuracyNegative || 
                x > DefaultAccuracyPositive;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsExactZero(this double x)
        {
            Debug.Assert(!double.IsNaN(x));

            return x == 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNotExactZero(this double x)
        {
            Debug.Assert(!double.IsNaN(x));

            return x < 0 || x > 0;
        }

        /// <summary>
        /// True if the given value is near zero relative to the default accuracy
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostZero(this double x)
        {
            Debug.Assert(!double.IsNaN(x));

            return !(
                double.IsInfinity(x) || 
                x < DefaultAccuracyNegative || 
                x > DefaultAccuracyPositive
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero(this double x, double epsilon = 1e-7d)
        {
            Debug.Assert(!double.IsNaN(x));

            return !(double.IsInfinity(x) || x < -epsilon || x > epsilon);
        }

        /// <summary>
        /// True if the given value is not near zero relative to the default accuracy
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDefiniteNotZero(this double x)
        {
            Debug.Assert(!double.IsNaN(x));

            return  
                double.IsInfinity(x) || 
                x < DefaultAccuracyNegative || 
                x > DefaultAccuracyPositive;
        }

        /// <summary>
        /// True if the given value is more than the default accuracy
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDefinitePositive(this double x)
        {
            Debug.Assert(!double.IsNaN(x));

            return x > DefaultAccuracyPositive;
        }

        /// <summary>
        /// True if the given value is less than negative the default accuracy
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDefiniteNegative(this double x)
        {
            Debug.Assert(!double.IsNaN(x));

            return x < DefaultAccuracyNegative;
        }

        /// <summary>
        /// True if the given value is more than or equal negative the default accuracy
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostNotPositive(this double x)
        {
            Debug.Assert(!double.IsNaN(x));

            return x <= DefaultAccuracyPositive;
        }

        /// <summary>
        /// True if the given value is more than or equal negative the default accuracy
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlmostNotNegative(this double x)
        {
            Debug.Assert(!double.IsNaN(x));

            return x >= DefaultAccuracyNegative;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Abs(this double number)
        {
            return Math.Abs(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SqrtOfAbs(this double number)
        {
            return Math.Sqrt(Math.Abs(number));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Floor(this double number)
        {
            return Math.Floor(number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Ceiling(this double number)
        {
            return Math.Ceiling(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Truncate(this double number)
        {
            return Math.Truncate(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Round(this double number)
        {
            return Math.Round(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Round(this double number, MidpointRounding mode)
        {
            return Math.Round(number, mode);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Round(this double number, int decimalPlaces)
        {
            return Math.Round(number, decimalPlaces);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Round(this double number, int decimalPlaces, MidpointRounding mode)
        {
            return Math.Round(number, decimalPlaces, mode);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp(this double number, double minValue, double maxValue)
        {
            return Math.Clamp(number, minValue, maxValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(this double number)
        {
            return Math.Sign(number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max(this double number1, double number2)
        {
            return Math.Max(number1, number2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max(this double number1, double number2, double number3)
        {
            return Math.Max(number1, Math.Max(number2, number3));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MaxMagnitude(this double number1, double number2)
        {
            return Math.MaxMagnitude(number1, number2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MaxMagnitude(this double number1, double number2, double number3)
        {
            return Math.MaxMagnitude(number1, Math.MaxMagnitude(number2, number3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min(this double number1, double number2)
        {
            return Math.Min(number1, number2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min(this double number1, double number2, double number3)
        {
            return Math.Min(number1, Math.Min(number2, number3));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MinMagnitude(this double number1, double number2)
        {
            return Math.MinMagnitude(number1, number2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MinMagnitude(this double number1, double number2, double number3)
        {
            return Math.MinMagnitude(number1, Math.MinMagnitude(number2, number3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BinaryScale(this double number, int power)
        {
            return Math.ScaleB(number, power);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CopySign(this double magnitudeNumber, double signNumber)
        {
            return Math.CopySign(magnitudeNumber, signNumber);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Squared(this double number)
        {
            return number * number;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cubed(this double number)
        {
            return number * number * number;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sqrt(this double number)
        {
            return Math.Sqrt(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cbrt(this double number)
        {
            return Math.Cbrt(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double IEEERemainder(this double number1, double number2)
        {
            return Math.IEEERemainder(number1, number2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Power(this double number, double exponent)
        {
            return Math.Pow(number, exponent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Exp(this double number)
        {
            return Math.Exp(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Log(this double number)
        {
            return Math.Log(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Log(this double number, double baseNumber)
        {
            return Math.Log(number, baseNumber);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Log2(this double number)
        {
            return Math.Log2(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IntLog2(this double number)
        {
            return Math.ILogB(number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Log10(this double number)
        {
            return Math.Log10(number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sin(this double number)
        {
            return Math.Sin(number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cos(this double number)
        {
            return Math.Cos(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTuple<double, double> SinCos(this double number)
        {
            return (Math.Sin(number), Math.Cos(number));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Tan(this double number)
        {
            return Math.Tan(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle ArcCos(this double number)
        {
            return Math.Acos(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle ArcSin(this double number)
        {
            return Math.Asin(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle ArcTan(this double number)
        {
            return Math.Atan(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle ArcTan(this double numberX, double numberY)
        {
            return Math.Atan2(numberY, numberX);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sinh(this double number)
        {
            return Math.Sinh(number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cosh(this double number)
        {
            return Math.Cosh(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Tanh(this double number)
        {
            return Math.Tanh(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle ArcCosh(this double number)
        {
            return Math.Acosh(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle ArcSinh(this double number)
        {
            return Math.Asinh(number);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PlanarAngle ArcTanh(this double number)
        {
            return Math.Atanh(number);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LerpInv(this double value, double v1, double v2)
        {
            Debug.Assert(!double.IsNaN(v1) && !double.IsNaN(v2) && !double.IsNaN(value));

            return (value - v1) / (v2 - v1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Lerp(this double t, double v1, double v2)
        {
            Debug.Assert(!double.IsNaN(v1) && !double.IsNaN(v2) && !double.IsNaN(t));

            return (1.0d - t) * v1 + t * v2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                    s.Append(((1 << j) & n) > 0 ? "1" : "0");

                s.Append(" ");

                for (var j = 4; j <= 7; j++)
                    s.Append(((1 << j) & n) > 0 ? "1" : "0");

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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetLinearRange(this double start, double finish, int count, bool periodicRange = false)
        {
            var length = finish - start;
            var n = periodicRange
                ? length / count
                : length / (count - 1);

            return Enumerable.Range(0, count).Select(i => start + i * n);
        }
    }
}
