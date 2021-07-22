using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using EuclideanGeometryLib.BasicMath.Coordinates;
using EuclideanGeometryLib.BasicMath.Frames.Space3D;
using EuclideanGeometryLib.BasicMath.Maps.Space3D;
using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicOperations;
using EuclideanGeometryLib.BasicShapes;
using EuclideanGeometryLib.Borders.Space3D.Mutable;

namespace EuclideanGeometryLib.BasicMath
{
    public static class BasicMathUtils
    {
        #region Complex Numbers Operations
        #endregion


        #region General Operations

        public static double GetFractionPart(this double value)
            => value - Math.Truncate(value);

        public static double ClampAngleInDegrees(this double value)
        {
            const int maxValue = 360;

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

        public static double ClampAngle(this double value)
        {
            const double maxValue = 2 * Math.PI;

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

        public static double ClampNegativeAngle(this double value)
        {
            const double maxValue = 2 * Math.PI;

            value += Math.PI;

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

        public static double Clamp(this double value, double maxValue)
        {
            if (value < 0) return 0;

            if (value > maxValue) return maxValue;

            return value;
        }

        public static double ClampInt(this int value, int maxValue)
        {
            if (value < 0) return 0;

            if (value > maxValue) return maxValue;

            return value;
        }

        public static int ClampToInt(this double value, int maxValue)
        {
            if (value < 0) return 0;

            if (value > maxValue) return maxValue;

            return (int)value;
        }

        public static ulong ClampToULong(this double value, ulong maxValue)
        {
            if (value < 0ul) return 0ul;

            if (value > maxValue) return maxValue;

            return (ulong)value;
        }

        public static T Clamp<T>(this T value, T lowerLimit, T upperLimit) where T : IComparable<T>
        {
            if (value.CompareTo(lowerLimit) < 0) return lowerLimit;
            if (value.CompareTo(upperLimit) > 0) return upperLimit;
            return value;
        }

        public static IEnumerable<IIntersectable> GetIntersectables<T>(this IEnumerable<T> geometricObjectsList)
            where T : IFiniteGeometricShape2D
        {
            return geometricObjectsList
                .Cast<IIntersectable>()
                .Where(g => !ReferenceEquals(g, null));
        }

        public static IEnumerable<IIntersectable> GetIntersectables(this IEnumerable<IFiniteGeometricShape3D> geometricObjectsList)
        {
            return geometricObjectsList
                .Cast<IIntersectable>()
                .Where(g => !ReferenceEquals(g, null));
        }


        #endregion


        #region Integer Operations


        public static double ToNearestInteger(this double value, ScalarToIntOption conversionMethod)
        {
            switch (conversionMethod)
            {
                case ScalarToIntOption.Round:
                    return Math.Round(value);

                case ScalarToIntOption.Ceiling:
                    return Math.Ceiling(value);

                case ScalarToIntOption.Floor:
                    return Math.Floor(value);

                default:
                    throw new ArgumentOutOfRangeException(nameof(conversionMethod), conversionMethod, null);
            }
        }

        public static double DegreesToRadians(this double angle)
        {
            const double c = Math.PI / 180.0d;

            return angle * c;
        }

        public static double DegreesToRadians(this int angle)
        {
            const double c = Math.PI / 180.0d;

            return angle * c;
        }

        public static double RadiansToDegrees(this double angle)
        {
            const double c = 180.0d / Math.PI;

            return angle * c;
        }

        public static double RadiansToDegrees(this float angle)
        {
            const double c = 180.0d / Math.PI;

            return angle * c;
        }

        public static IEnumerable<double> GetRegularSamples(this int samplesCount, double firstValue, double lastValue, bool excludeFirst = false, bool excludeLast = false)
        {
            var firstSampleIndex = 0;
            var newSamplesCount = samplesCount - 1;

            if (excludeFirst)
            {
                firstSampleIndex++;
                newSamplesCount++;
            }

            if (excludeLast)
            {
                newSamplesCount++;
            }

            return
                Enumerable
                    .Range(firstSampleIndex, samplesCount)
                    .Select(i => i / (double)newSamplesCount)
                    .Select(t => (1.0d - t) * firstValue + t * lastValue);
        }

        public static IEnumerable<double> GetRegularSamplesTwoPi(this int samplesCount)
        {
            return Enumerable
                .Range(0, samplesCount)
                .Select(i => 
                    (MathNet.Numerics.Constants.Pi2 * i) / samplesCount
                );
        }

        public static IEnumerable<double> GetRegularSamplesPi(this int samplesCount)
        {
            return Enumerable
                .Range(0, samplesCount)
                .Select(i => (Math.PI * i) / samplesCount);
        }

        public static IEnumerable<double> GetRegularSamples(this int samplesCount, double angleMax)
        {
            return Enumerable
                .Range(0, samplesCount)
                .Select(i => (angleMax * i) / samplesCount);
        }

        public static bool IsPowerOf2(this int n)
        {
            return (n > 0 && ~(n & (n - 1)) > 0);
        }

        /// <summary>
        /// Finds the smallest power of 2 that is >= n
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Power2Ceiling(this int n)
        {
            n--;

            n |= n >> 1;
            n |= n >> 2;
            n |= n >> 4;
            n |= n >> 8;
            n |= n >> 16;

            return n + 1;
        }


        public static bool IsPowerOf2(this long n)
        {
            return (n > 0 && ~(n & (n - 1)) > 0);
        }

        /// <summary>
        /// Finds the smallest power of 2 that is >= n
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long Power2Ceiling(this long n)
        {
            n--;

            n |= n >> 1;
            n |= n >> 2;
            n |= n >> 4;
            n |= n >> 8;
            n |= n >> 16;

            return n + 1;
        }
        #endregion


        #region Coordinates Operations
        public static Tuple2D ToTuple2D(this Complex complexNumber)
            => new Tuple2D(
                complexNumber.Real,
                complexNumber.Imaginary
            );

        public static Tuple2D ToTuple2D(this IPolarPosition2D polarPosition)
            => new Tuple2D(
                polarPosition.R * Math.Cos(polarPosition.Theta),
                polarPosition.R * Math.Sin(polarPosition.Theta)
            );

        public static PolarPosition2D ToPolarPosition(this Complex complexNumber)
            => new PolarPosition2D(
                complexNumber.Magnitude,
                complexNumber.Phase
            );

        public static PolarPosition2D ToPolarPosition(this ITuple2D point)
            => new PolarPosition2D(
                Math.Sqrt(point.X * point.X + point.Y * point.Y),
                Math.Atan2(point.Y, point.X)
            );

        public static Tuple3D ToTuple3D(this UnitSphericalPosition3D sphericalPosition)
        {
            var sinTheta = Math.Sin(sphericalPosition.Theta);

            return new Tuple3D(
                sinTheta * Math.Cos(sphericalPosition.Phi),
                sinTheta * Math.Sin(sphericalPosition.Phi),
                Math.Cos(sphericalPosition.Theta)
            );
        }

        public static Tuple3D ToTuple3D(this UnitSphericalPosition3D sphericalPosition, double r)
        {
            var rSinTheta = r * Math.Sin(sphericalPosition.Theta);

            return new Tuple3D(
                rSinTheta * Math.Cos(sphericalPosition.Phi),
                rSinTheta * Math.Sin(sphericalPosition.Phi),
                r * Math.Cos(sphericalPosition.Theta)
            );
        }

        public static Tuple3D ToTuple3D(this ISphericalPosition3D sphericalPosition)
        {
            var rSinTheta = sphericalPosition.R * Math.Sin(sphericalPosition.Theta);

            return new Tuple3D(
                rSinTheta * Math.Cos(sphericalPosition.Phi),
                rSinTheta * Math.Sin(sphericalPosition.Phi),
                sphericalPosition.R * Math.Cos(sphericalPosition.Theta)
            );
        }

        public static SphericalPosition3D ToSphericalPosition(this ITuple3D position)
        {
            var r = Math.Sqrt(
                position.X * position.X +
                position.Y * position.Y +
                position.Z * position.Z
            );

            return new SphericalPosition3D(
                Math.Acos(r / position.Z),
                Math.Atan2(position.Y, position.X),
                r
            );
        }

        public static UnitSphericalPosition3D ToUnitSphericalPosition(this ITuple3D position)
        {
            var r = Math.Sqrt(
                position.X * position.X +
                position.Y * position.Y +
                position.Z * position.Z
            );

            return new UnitSphericalPosition3D(
                Math.Acos(r / position.Z),
                Math.Atan2(position.Y, position.X)
            );
        }

        public static UnitSphericalPosition3D ToUnitSphericalPosition(this ISphericalPosition3D sphericalPosition)
            => new UnitSphericalPosition3D(
                sphericalPosition.Theta,
                sphericalPosition.Phi
            );

        public static SphericalPosition3D ToSphericalPosition(this UnitSphericalPosition3D sphericalPosition, double r)
            => new SphericalPosition3D(
                sphericalPosition.Theta,
                sphericalPosition.Phi,
                r
            );

        public static Tuple3D GetUnitVectorR(this ISphericalPosition3D sphericalPosition)
        {
            var sinTheta = Math.Sin(sphericalPosition.Theta);
            var cosTheta = Math.Cos(sphericalPosition.Theta);

            var sinPhi = Math.Sin(sphericalPosition.Phi);
            var cosPhi = Math.Cos(sphericalPosition.Phi);

            return new Tuple3D(
                sinTheta * cosPhi,
                sinTheta * sinPhi,
                cosTheta
            );
        }

        public static Tuple3D GetUnitVectorR(this ITuple3D vector)
        {
            var r = vector.GetLength();

            var cosTheta = r / vector.Z;
            var sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

            var phi = Math.Atan2(vector.Y, vector.X);
            var cosPhi = Math.Cos(phi);
            var sinPhi = Math.Sin(phi);

            return new Tuple3D(
                sinTheta * cosPhi,
                sinTheta * sinPhi,
                cosTheta
            );
        }

        public static Tuple3D GetUnitVectorTheta(this ISphericalPosition3D sphericalPosition)
        {
            var sinTheta = Math.Sin(sphericalPosition.Theta);
            var cosTheta = Math.Cos(sphericalPosition.Theta);

            var sinPhi = Math.Sin(sphericalPosition.Phi);
            var cosPhi = Math.Cos(sphericalPosition.Phi);

            return new Tuple3D(
                cosTheta * cosPhi,
                cosTheta * sinPhi,
                -sinTheta
            );
        }

        public static Tuple3D GetUnitVectorTheta(this ITuple3D vector)
        {
            var r = vector.GetLength();

            var cosTheta = vector.Z / r;
            var sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

            var phi = Math.Atan2(vector.Y, vector.X);
            var cosPhi = Math.Cos(phi);
            var sinPhi = Math.Sin(phi);

            return new Tuple3D(
                cosTheta * cosPhi,
                cosTheta * sinPhi,
                -sinTheta
            );
        }

        public static Tuple3D GetUnitVectorPhi(this ISphericalPosition3D sphericalPosition)
        {
            var sinPhi = Math.Sin(sphericalPosition.Phi);
            var cosPhi = Math.Cos(sphericalPosition.Phi);

            return new Tuple3D(-sinPhi, cosPhi, 0);
        }

        public static Tuple3D GetUnitVectorPhi(this ITuple3D vector)
        {
            var phi = Math.Atan2(vector.Y, vector.X);
            var cosPhi = Math.Cos(phi);
            var sinPhi = Math.Sin(phi);

            return new Tuple3D(-sinPhi, cosPhi, 0);
        }

        #endregion


        #region Interpolation Operations

        public static TrsMap3Dp Lerp(this double t, TrsMap3Dp v1, TrsMap3Dp v2)
        {
            var s = 1.0d - t;

            var newValue = new TrsMap3Dp
            {
                RotationAngle = s * v1.RotationAngle + t * v2.RotationAngle,
                RotationVector = new Tuple3D(
                    s * v1.RotationVector.X + t * v2.RotationVector.X,
                    s * v1.RotationVector.Y + t * v2.RotationVector.Y,
                    s * v1.RotationVector.Z + t * v2.RotationVector.Z
                ),
                TranslationVector = new Tuple3D(
                    s * v1.TranslationVector.X + t * v2.TranslationVector.X,
                    s * v1.TranslationVector.Y + t * v2.TranslationVector.Y,
                    s * v1.TranslationVector.Z + t * v2.TranslationVector.Z
                ),
                StretchMatrix =
                {
                    [0] = s * v1.StretchMatrix[0] + t * v2.StretchMatrix[0],
                    [1] = s * v1.StretchMatrix[1] + t * v2.StretchMatrix[1],
                    [2] = s * v1.StretchMatrix[2] + t * v2.StretchMatrix[2],
                    [3] = s * v1.StretchMatrix[3] + t * v2.StretchMatrix[3],
                    [4] = s * v1.StretchMatrix[4] + t * v2.StretchMatrix[4],
                    [5] = s * v1.StretchMatrix[5] + t * v2.StretchMatrix[5],
                    [6] = s * v1.StretchMatrix[6] + t * v2.StretchMatrix[6],
                    [7] = s * v1.StretchMatrix[7] + t * v2.StretchMatrix[7],
                    [8] = s * v1.StretchMatrix[8] + t * v2.StretchMatrix[8]
                }
            };

            return newValue;
        }

        public static IEnumerable<TrsMap3Dp> Lerp(this IEnumerable<double> tList, TrsMap3Dp v1, TrsMap3Dp v2)
        {
            return tList.Select(t => t.Lerp(v1, v2));
        }

        ///// <summary>
        ///// Linear interpolation of two points\vectors in 3D
        ///// </summary>
        ///// <param name="t"></param>
        ///// <param name="p1"></param>
        ///// <param name="p2"></param>
        ///// <returns></returns>
        //public static Tuple3D Lerp(this double t, ITuple3D p1, ITuple3D p2)
        //{
        //    var s = 1.0d - t;

        //    return new Tuple3D(
        //        s * p1.X + t * p2.X,
        //        s * p1.Y + t * p2.Y,
        //        s * p1.Z + t * p2.Z
        //    );
        //}

        //public static IEnumerable<Tuple3D> Lerp(this IEnumerable<double> tList, ITuple3D p1, ITuple3D p2)
        //{
        //    return 
        //        from t in tList
        //        let s = 1.0d - t
        //        select new Tuple3D(
        //            s * p1.X + t * p2.X,
        //            s * p1.Y + t * p2.Y,
        //            s * p1.Z + t * p2.Z
        //        );
        //}

        public static double Lerp(this double t, double v1, double v2)
        {
            Debug.Assert(!Double.IsNaN(v1) && !Double.IsNaN(v2) && !Double.IsNaN(t));

            return (1.0d - t) * v1 + t * v2;
        }

        public static Tuple2D Lerp(this double t, ITuple2D v1, ITuple2D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid && !Double.IsNaN(t));

            var s = 1.0d - t;

            return new Tuple2D(
                s * v1.X + t * v2.X,
                s * v1.Y + t * v2.Y
            );
        }

        public static Tuple3D Lerp(this double t, ITuple3D v1, ITuple3D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid && !Double.IsNaN(t));

            var s = 1.0d - t;

            return new Tuple3D(
                s * v1.X + t * v2.X,
                s * v1.Y + t * v2.Y,
                s * v1.Z + t * v2.Z
            );
        }

        public static Tuple4D Lerp(this double t, ITuple4D v1, ITuple4D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid && !Double.IsNaN(t));

            var s = 1.0d - t;

            return new Tuple4D(
                s * v1.X + t * v2.X,
                s * v1.Y + t * v2.Y,
                s * v1.Z + t * v2.Z,
                s * v1.W + t * v2.W
            );
        }

        public static Matrix2X2 Lerp(this double t, Matrix2X2 v1, Matrix2X2 v2)
        {
            Debug.Assert(!v1.HasNaNComponent && !v2.HasNaNComponent && !Double.IsNaN(t));

            var s = 1.0d - t;

            return new Matrix2X2()
            {
                [0] = s * v1[0] + t * v2[0],
                [1] = s * v1[1] + t * v2[1],
                [2] = s * v1[2] + t * v2[2],
                [3] = s * v1[3] + t * v2[3]
            };
        }

        public static Matrix3X3 Lerp(this double t, Matrix3X3 v1, Matrix3X3 v2)
        {
            Debug.Assert(!v1.HasNaNComponent && !v2.HasNaNComponent && !Double.IsNaN(t));

            var s = 1.0d - t;

            return new Matrix3X3()
            {
                [0] = s * v1[0] + t * v2[0],
                [1] = s * v1[1] + t * v2[1],
                [2] = s * v1[2] + t * v2[2],
                [3] = s * v1[3] + t * v2[3],
                [4] = s * v1[4] + t * v2[4],
                [5] = s * v1[5] + t * v2[5],
                [6] = s * v1[6] + t * v2[6],
                [7] = s * v1[7] + t * v2[7],
                [8] = s * v1[8] + t * v2[8]
            };
        }

        /// <summary>
        /// Spherical Linear Interpolation of two normalized quaternions.
        /// See https://en.wikipedia.org/wiki/Slerp for details
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Tuple4D Slerp(this double t, Tuple4D v1, Tuple4D v2)
        {
            //Compute the cosine of the angle between the two vectors.
            var s = 1.0d - t;
            var cosTheta = v1.VectorDot(v2);

            if (cosTheta > 0.9995d)
            {
                //If the inputs are too close for comfort, linearly interpolate and normalize the result.
                var x = s * v1.X + t * v2.X;
                var y = s * v1.Y + t * v2.Y;
                var z = s * v1.Z + t * v2.Z;
                var w = s * v1.W + t * v2.W;

                var d = 1.0d / Math.Sqrt(x * x + y * y + z * z + w * w);

                return new Tuple4D(x * d, y * d, z * d, w * d);
            }

            //If the dot product is negative, the quaternions have opposite handedness
            //and slerp won't take the shorter path. Fix by reversing one quaternion.
            if (cosTheta < 0.0d)
            {
                v1 = -v1;
                cosTheta = -cosTheta;
            }

            //Robustness: Stay within domain of acos()
            // theta = angle between input quaternions, interpreted as vectors
            var theta = Math.Acos(cosTheta.Clamp(-1.0d, 1.0d));

            //thetaP = angle between value1 and result quaternion, interpreted as vectors
            var thetaP = theta * t;
            var cosThetaP = Math.Cos(thetaP);
            var sinThetaP = Math.Sin(thetaP);

            //Make { value1, qPerp } an orthogonal basis in quaternion vector space
            var qPerpX = v2.X - v1.X * cosTheta;
            var qPerpY = v2.Y - v1.Y * cosTheta;
            var qPerpZ = v2.Z - v1.Z * cosTheta;
            var qPerpW = v2.W - v1.W * cosTheta;
            var qPerpInvLength = 1.0d / Math.Sqrt(qPerpX * qPerpX + qPerpY * qPerpY + qPerpZ * qPerpZ + qPerpW * qPerpW);

            //Final result
            return new Tuple4D(
                v1.X * cosThetaP + qPerpX * qPerpInvLength * sinThetaP,
                v1.Y * cosThetaP + qPerpY * qPerpInvLength * sinThetaP,
                v1.Z * cosThetaP + qPerpZ * qPerpInvLength * sinThetaP,
                v1.W * cosThetaP + qPerpW * qPerpInvLength * sinThetaP
                );
        }


        public static IEnumerable<double> Lerp(this IEnumerable<double> tList, double v1, double v2)
        {
            return tList.Select(t => (1.0d - t) * v1 + t * v2);
        }

        public static IEnumerable<Tuple2D> Lerp(this IEnumerable<double> tList, ITuple2D v1, ITuple2D v2)
        {
            return tList.Select(t => t.Lerp(v1, v2));
        }

        public static IEnumerable<Tuple3D> Lerp(this IEnumerable<double> tList, ITuple3D v1, ITuple3D v2)
        {
            return tList.Select(t => t.Lerp(v1, v2));
        }

        public static IEnumerable<Tuple4D> Lerp(this IEnumerable<double> tList, ITuple4D v1, ITuple4D v2)
        {
            return tList.Select(t => t.Lerp(v1, v2));
        }

        public static IEnumerable<Matrix2X2> Lerp(this IEnumerable<double> tList, Matrix2X2 v1, Matrix2X2 v2)
        {
            return tList.Select(t => t.Lerp(v1, v2));
        }

        public static IEnumerable<Matrix3X3> Lerp(this IEnumerable<double> tList, Matrix3X3 v1, Matrix3X3 v2)
        {
            return tList.Select(t => t.Lerp(v1, v2));
        }

        public static IEnumerable<Tuple4D> Slerp(this IEnumerable<double> tList, Tuple4D v1, Tuple4D v2)
        {
            return tList.Select(t => t.Slerp(v1, v2));
        }

        #endregion


        #region Affine Maps

        private static Tuple<Tuple3D, Tuple3D> MapPoint(this Matrix4X4 mapMatrix, Tuple3D point, Tuple3D ptError)
        {
            //TODO: Study techniques in book "Computer-Aided Geometric Design - A Totally Four-Dimensional Approach 2002"
            //and section Section 3.9 on managing rounding errors in PBRT book
            var x = point.X;
            var y = point.Y;
            var z = point.Z;

            var absErrorX =
                (Float64Utils.Gamma3 + 1) *
                (Math.Abs(mapMatrix[0, 0]) * ptError.X + Math.Abs(mapMatrix[0, 1]) * ptError.Y +
                 Math.Abs(mapMatrix[0, 2]) * ptError.Z) +
                Float64Utils.Gamma3 * (Math.Abs(mapMatrix[0, 0] * x) + Math.Abs(mapMatrix[0, 1] * y) +
                                       Math.Abs(mapMatrix[0, 2] * z) + Math.Abs(mapMatrix[0, 3]));
            var absErrorY =
                (Float64Utils.Gamma3 + 1) *
                (Math.Abs(mapMatrix[1, 0]) * ptError.X + Math.Abs(mapMatrix[1, 1]) * ptError.Y +
                 Math.Abs(mapMatrix[1, 2]) * ptError.Z) +
                Float64Utils.Gamma3 * (Math.Abs(mapMatrix[1, 0] * x) + Math.Abs(mapMatrix[1, 1] * y) +
                                       Math.Abs(mapMatrix[1, 2] * z) + Math.Abs(mapMatrix[1, 3]));
            var absErrorZ =
                (Float64Utils.Gamma3 + 1) *
                (Math.Abs(mapMatrix[2, 0]) * ptError.X + Math.Abs(mapMatrix[2, 1]) * ptError.Y +
                 Math.Abs(mapMatrix[2, 2]) * ptError.Z) +
                Float64Utils.Gamma3 * (Math.Abs(mapMatrix[2, 0] * x) + Math.Abs(mapMatrix[2, 1] * y) +
                                       Math.Abs(mapMatrix[2, 2] * z) + Math.Abs(mapMatrix[2, 3]));

            var p = mapMatrix.MapProjectivePoint(point);

            var xp = p.X;
            var yp = p.Y;
            var zp = p.Z;
            var wp = p.W;

            Debug.Assert(wp != 0.0d);

            if (wp == 1.0d)
                return Tuple.Create(
                    new Tuple3D(xp, yp, zp),
                    new Tuple3D(absErrorX, absErrorY, absErrorZ)
                );
            else
            {
                var s = 1.0d / wp;

                return Tuple.Create(
                    new Tuple3D(xp * s, yp * s, zp * s),
                    new Tuple3D(absErrorX, absErrorY, absErrorZ)
                );
            }
        }

        public static MutableBoundingBox3D MapBoundingBox(this IAffineMap3D affineMap, MutableBoundingBox3D boundingBox)
        {
            return boundingBox.MapUsing(affineMap) as MutableBoundingBox3D;
        }

        public static Tuple3D MapUnitVector(this IAffineMap3D linearMap, ITuple3D vector)
        {
            return linearMap.MapVector(vector).ToUnitVector();
        }

        public static Tuple3D MapUnitNormal(this IAffineMap3D linearMap, ITuple3D normal)
        {
            return linearMap.MapNormal(normal).ToUnitVector();
        }

        public static LinearFrame3D MapFrame(this IAffineMap3D linearMap, LinearFrame3D frame)
        {
            //TODO: Use GMac to generate this and compare
            //Map the 3 vectors and apply The Gram–Schmidt process
            //https://en.wikipedia.org/wiki/Gram%E2%80%93Schmidt_process
            var uDirection = linearMap.MapVector(frame.UDirection).ToUnitVector();

            var vDirection = linearMap.MapVector(frame.VDirection);
            vDirection = vDirection.ToTuple3D() -
                         vDirection.ProjectOnUnitVector(uDirection);
            vDirection = vDirection.ToUnitVector();

            var wDirection = linearMap.MapVector(frame.WDirection);
            wDirection = wDirection.ToTuple3D() -
                         wDirection.ProjectOnUnitVector(uDirection) -
                         wDirection.ProjectOnUnitVector(vDirection);
            wDirection = wDirection.ToUnitVector();

            return new LinearFrame3D(uDirection, vDirection, wDirection);
        }
        #endregion
    }
}
