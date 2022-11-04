using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace NumericalGeometryLib.BasicMath
{
    public static class Float32Utils
    {
        public static float Pi { get; } = (float)Math.PI;

        public static float TwoPi { get; } = (float)(2.0d * Math.PI);

        public static float FourPi { get; } = (float)(4.0d * Math.PI);

        public static float PiOverTwo { get; } = (float)(Math.PI / 2.0d);

        public static float PiOverFour { get; } = (float)(Math.PI / 4.0d);

        public static float InvPi { get; } = (float)(1.0d / Math.PI);

        public static float InvTwoPi { get; } = (float)(0.5d / Math.PI);

        public static float InvFourPi { get; } = (float)(0.25d / Math.PI);

        public static float SqrtTwo { get; } = (float)(Math.Sqrt(2.0d));

        public static float InvSqrtTwo { get; } = (float)(1.0d / Math.Sqrt(2.0d));

        public static float LogTwo { get; } = (float)(Math.Log(2.0d));

        public static float InvLogTwo { get; } = (float)(1.0d / Math.Log(2.0d));

        public static float PiOverOneEighty { get; } = (float)(Math.PI / 180.0d);

        public static float OneEightyOverPi { get; } = (float)(180.0d / Math.PI);

        /// <summary>
        /// Tolerance value for distances in single precision
        /// </summary>
        public static float DistanceEpsilon { get; }
            = (float)Math.Pow(2.0, -16);

        /// <summary>
        /// Tolerance value for squared distances in single precision
        /// </summary>
        public static float SquaredDistanceEpsilon { get; }
            = (2.0f + DistanceEpsilon) * DistanceEpsilon;

        /// <summary>
        /// Tolerance value for angles in single precision
        /// </summary>
        public static float AngleEpsilon { get; }
            = (float)Math.Pow(2.0d, -16);


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
        public static bool IsNearZero(this float x, float epsilon = 1e-7f)
        {
            Debug.Assert(!float.IsNaN(x));

            return !(float.IsInfinity(x) || x < -epsilon || x > epsilon);
        }

        /// <summary>
        /// True if the given value is near zero relative to the global tolerance
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZeroDistance(this float x)
        {
            return !(x < -DistanceEpsilon || x > DistanceEpsilon);
        }

        /// <summary>
        /// True if the given value is near zero relative to the global tolerance
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZeroSquaredDistance(this float x)
        {
            return !(x < -SquaredDistanceEpsilon || x > SquaredDistanceEpsilon);
        }

        /// <summary>
        /// True if the given value is near zero relative to the global tolerance
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZeroAngle(this float x)
        {
            return !(x < -AngleEpsilon || x > AngleEpsilon);
        }

        /// <summary>
        /// True if the given value is less than zero relative to the global tlerance
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegativeDistance(this float x)
        {
            return x < -DistanceEpsilon;
        }

        /// <summary>
        /// True if the given value is less than zero relative to the global tlerance
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegativeSquaredDistance(this float x)
        {
            return x < -SquaredDistanceEpsilon;
        }

        /// <summary>
        /// True if the given value is less than zero relative to the global tlerance
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegativeAngle(this float x)
        {
            return x < -AngleEpsilon;
        }

        /// <summary>
        /// True if the given value is less than zero relative to the global tlerance
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPositiveDistance(this float x)
        {
            return x > DistanceEpsilon;
        }

        /// <summary>
        /// True if the given value is less than zero relative to the global tlerance
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPositiveSquaredDistance(this float x)
        {
            return x > SquaredDistanceEpsilon;
        }

        /// <summary>
        /// True if the given value is less than zero relative to the global tlerance
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPositiveAngle(this float x)
        {
            return x > AngleEpsilon;
        }

        /// <summary>
        /// True if the given values are equal relative to the global tolerance
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AreNearEqualDistances(this float x1, float x2)
        {
            var x = x1 - x2;
            return !(x < -DistanceEpsilon || x > DistanceEpsilon);
        }

        /// <summary>
        /// True if the given values are equal relative to the global tolerance
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AreNearEqualSquaredDistances(this float x1, float x2)
        {
            var x = x1 - x2;
            return !(x < -SquaredDistanceEpsilon || x > SquaredDistanceEpsilon);
        }

        /// <summary>
        /// True if the given values are equal relative to the global tolerance
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AreNearEqualAngles(this float x1, float x2)
        {
            var x = x1 - x2;
            return !(x < -AngleEpsilon || x > AngleEpsilon);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DegreesToRadians(this float angle)
        {
            return angle * PiOverOneEighty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RadiansToDegrees(this float angle)
        {
            return angle * OneEightyOverPi;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Lerp(this float v1, float v2, float t)
        {
            Debug.Assert(!float.IsNaN(v1) && !float.IsNaN(v2) && !float.IsNaN(t));

            return (1.0f - t) * v1 + t * v2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<float> Lerp(this float v1, float v2, IEnumerable<float> tList)
        {
            return tList.Select(
                t => (1.0f - t) * v1 + t * v2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float[] Lerp(this float v1, float v2, params float[] tList)
        {
            return tList.Select(
                t => (1.0f - t) * v1 + t * v2
            ).ToArray();
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
                    s.Append(((1 << j) & n) > 0 ? "1" : "0");

                s.Append(" ");

                for (var j = 4; j <= 7; j++)
                    s.Append(((1 << j) & n) > 0 ? "1" : "0");

                s.Append(" ");
            }

            return s.ToString();
        }
    }
}