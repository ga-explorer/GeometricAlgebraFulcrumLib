using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using MathNet.Numerics;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra
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
        public static bool IsInteger(this double value)
        {
            return Math.Truncate(value) == value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonNegativeInteger(this double value)
        {
            return value >= 0 && value.IsInteger();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPositiveInteger(this double value)
        {
            return value > 0 && value.IsInteger();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonPositiveInteger(this double value)
        {
            return value <= 0 && value.IsInteger();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegativeInteger(this double value)
        {
            return value < 0 && value.IsInteger();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearInRange(this double x, double value1, double value2, double epsilon = 1e-12d)
        {
            Debug.Assert(
                !double.IsNaN(x) &&
                !double.IsNaN(value1) &&
                !double.IsNaN(value2) &&
                value1 < value2
            );

            return !(double.IsInfinity(x) || x < value1 - epsilon || x > value2 + epsilon);
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
        public static bool IsZero(this double x)
        {
            Debug.Assert(!double.IsNaN(x));

            return x == 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNotZero(this double x)
        {
            return !double.IsNaN(x) && x is < 0 or > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOne(this double x)
        {
            Debug.Assert(!double.IsNaN(x));

            return x == 1d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMinusOne(this double x)
        {
            Debug.Assert(!double.IsNaN(x));

            return x == -1d;
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
        public static bool IsNearZero(this double x, double epsilon = 1e-12d)
        {
            Debug.Assert(!double.IsNaN(x));

            return !(double.IsInfinity(x) || x < -epsilon || x > epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearOne(this double x, double epsilon = 1e-12d)
        {
            return (x - 1d).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearMinusOne(this double x, double epsilon = 1e-12d)
        {
            return (x + 1d).IsNearZero(epsilon);
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
        public static bool IsInfinite(this double x)
        {
            return double.IsInfinity(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNaNOrInfinite(this double x)
        {
            return double.IsNaN(x) || double.IsInfinity(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNaN(this double x)
        {
            return double.IsNaN(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNotNaN(this double x)
        {
            return !double.IsNaN(x);
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
        public static double IntegerPart(this double number)
        {
            return Math.Truncate(number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double FractionalPart(this double number)
        {
            return number - Math.Truncate(number);
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
        public static int RoundToInt32(this double number)
        {
            return (int)Math.Round(number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long RoundToInt64(this double number)
        {
            return (long)Math.Round(number);
        }
        public static double ClampToUnit(this double scalar)
        {
            if (scalar < 0.0d) return 0.0d;
            if (scalar > 1.0d) return 1.0d;
            return scalar;
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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp(this double number, double minValue, double maxValue)
        {
            return Math.Clamp(number, minValue, maxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntegerSign Sign(this double number)
        {
            return IntegerSign.CreateFromInt32(
                Math.Sign(number)
            );
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
        public static double Square(this double number)
        {
            return number * number;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cube(this double number)
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
        public static double LogE(this double number)
        {
            return Math.Log(number, Math.E);
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
        public static Float64PlanarAngle ArcCos(this double number)
        {
            return Math.Acos(number).RadiansToAngle();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle ArcSin(this double number)
        {
            return Math.Asin(number).RadiansToAngle();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle ArcTan(this double number)
        {
            return Math.Atan(number).RadiansToAngle();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle ArcTan(this double numberX, double numberY)
        {
            return Math.Atan2(numberY, numberX).RadiansToAngle();
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
        public static Float64PlanarAngle ArcCosh(this double number)
        {
            return Math.Acosh(number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle ArcSinh(this double number)
        {
            return Math.Asinh(number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64PlanarAngle ArcTanh(this double number)
        {
            return Math.Atanh(number);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NaNToZero(this double number)
        {
            return double.IsNaN(number) ? 0d : number;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NaNInfinityToZero(this double number)
        {
            return double.IsNaN(number) || double.IsInfinity(number)
                ? 0d : number;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NearZeroToZero(this double number, double epsilon)
        {
            return number.IsNearZero(epsilon) ? 0d : number;
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
        public static double Lerp(this double t, IPair<double> valuePair)
        {
            var v1 = valuePair.Item1;
            var v2 = valuePair.Item2;

            Debug.Assert(!double.IsNaN(v1) && !double.IsNaN(v2) && !double.IsNaN(t));

            return (1.0d - t) * v1 + t * v2;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetLinearPeriodicRange(this double start, double finish, int count)
        {
            var length = finish - start;
            var n = length / count;

            return Enumerable
                .Range(0, count)
                .Select(i => start + i * n);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CosWave(this double t, double value1, double value2, int cycleCount = 1)
        {
            t = (cycleCount * t).ClampPeriodic(1);

            return value1 + 0.5d * (1d - Math.Cos(2d * Math.PI * t)) * (value2 - value1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CosWave(this double t, IPair<double> valuePair, int cycleCount = 1)
        {
            var value1 = valuePair.Item1;
            var value2 = valuePair.Item2;
            
            t = (cycleCount * t).ClampPeriodic(1);

            return value1 + 0.5d * (1d - Math.Cos(2d * Math.PI * t)) * (value2 - value1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double TriangleWave(this double t, double value1, double value2, int cycleCount = 1)
        {
            t = (cycleCount * t).ClampPeriodic(1);

            return t <= 0.5d
                ? value1 + 2 * t * (value2 - value1)
                : value1 + 2 * (1d - t) * (value2 - value1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetCosRange(this double start, double finish, int sampleCount, int cycleCount = 1, bool isPeriodicRange = false)
        {
            var length = finish - start;
            var n = isPeriodicRange
                ? 2d * Math.PI * cycleCount / sampleCount
                : 2d * Math.PI * cycleCount / (sampleCount - 1);

            return Enumerable
                .Range(0, sampleCount)
                .Select(i => start + 0.5d * (1d - Math.Cos(i * n)) * length);
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static bool IsInvalid(this IGeometricElement element)
        //{
        //    return !element.IsValid();
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetFractionPart(this double value)
        {
            return value - Math.Truncate(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DegreesToRadians(this double angle)
        {
            const double c = Math.PI / 180.0d;

            return angle * c;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DegreesToRadians(this int angle)
        {
            const double c = Math.PI / 180.0d;

            return angle * c;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RadiansToDegrees(this double angle)
        {
            const double c = 180.0d / Math.PI;

            return angle * c;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RadiansToDegrees(this float angle)
        {
            const double c = 180.0d / Math.PI;

            return angle * c;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClampAngleInDegrees(this double value)
        {
            const int maxValue = 360;

            return value switch
            {
                //value < -maxValue
                < -maxValue => value + Math.Ceiling(-value / maxValue) * maxValue,

                //-maxValue <= value < 0
                < 0 => value + maxValue,

                //value > maxValue
                > maxValue => value - Math.Truncate(value / maxValue) * maxValue,

                //0 <= value <= maxValue
                _ => value
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClampAngleInDegrees(this Float64Scalar value)
        {
            return value.Value.ClampAngleInDegrees();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClampAngleInRadians(this double value)
        {
            const double maxValue = 2 * Math.PI;

            return value switch
            {
                //value < -maxValue
                < -maxValue => value + Math.Ceiling(-value / maxValue) * maxValue,

                //-maxValue <= value < 0
                < 0 => value + maxValue,

                //value > maxValue
                > maxValue => value - Math.Truncate(value / maxValue) * maxValue,

                //0 <= value <= maxValue
                _ => value
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClampAngleInRadians(this Float64Scalar value)
        {
            return value.Value.ClampAngleInRadians();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClampNegativeAngle(this double value)
        {
            const double maxValue = 2 * Math.PI;

            value += Math.PI;

            return value switch
            {
                //value < -maxValue
                < -maxValue => value + Math.Ceiling(-value / maxValue) * maxValue,

                //-maxValue <= value < 0
                < 0 => value + maxValue,

                //value > maxValue
                > maxValue => value - Math.Truncate(value / maxValue) * maxValue,

                //0 <= value <= maxValue
                _ => value
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClampPeriodic(this double value, double minValue, double maxValue)
        {
            return (value - minValue).ClampPeriodic(maxValue - minValue) + minValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp(this double value, double maxValue)
        {
            if (value < 0) return 0;

            return value > maxValue ? maxValue : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClampInt(this int value, int maxValue)
        {
            if (value < 0) return 0;

            return value > maxValue ? maxValue : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ClampToInt(this double value, int maxValue)
        {
            if (value < 0) return 0;

            return value > maxValue ? maxValue : (int)value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ClampToULong(this double value, ulong maxValue)
        {
            if (value < 0ul) return 0ul;

            return value > maxValue ? maxValue : (ulong)value;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
}
