using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using EuclideanGeometryLib.BasicMath.Tuples;
using MathNet.Numerics;

namespace EuclideanGeometryLib.BasicMath
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
        public static bool IsAlmostEqual(this Complex x1, Complex x2)
        {
            return (x1 - x2).Magnitude.IsAlmostZero();
        }
        
        /// <summary>
        /// True if the given values are equal relative to the default accuracy
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
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
        public static bool IsAlmostEqual(this ITuple4D x1, ITuple4D x2)
        {
            return
                IsAlmostEqual(x1.X, x2.X) &&
                IsAlmostEqual(x1.Y, x2.Y) &&
                IsAlmostEqual(x1.Z, x2.Z) &&
                IsAlmostEqual(x1.W, x2.W);
        }


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

        public static bool IsExactZero(this double x)
        {
            Debug.Assert(!double.IsNaN(x));

            return x == 0;
        }

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
        public static bool IsAlmostZero(this double x)
        {
            Debug.Assert(!double.IsNaN(x));

            return !(
                double.IsInfinity(x) || 
                x < DefaultAccuracyNegative || 
                x > DefaultAccuracyPositive
            );
        }

        /// <summary>
        /// True if the given value is not near zero relative to the default accuracy
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
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
        public static bool IsAlmostNotNegative(this double x)
        {
            Debug.Assert(!double.IsNaN(x));

            return x >= DefaultAccuracyNegative;
        }


        public static double Lerp(this double v1, double v2, double t)
        {
            Debug.Assert(!double.IsNaN(v1) && !double.IsNaN(v2) && !double.IsNaN(t));

            return (1.0d - t) * v1 + t * v2;
        }

        public static IEnumerable<double> Lerp(this double v1, double v2, IEnumerable<double> tList)
        {
            return tList.Select(
                t => (1.0d - t) * v1 + t * v2
                );
        }

        public static double[] Lerp(this double v1, double v2, params double[] tList)
        {
            return tList.Select(
                t => (1.0d - t) * v1 + t * v2
                ).ToArray();
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
    }
}
