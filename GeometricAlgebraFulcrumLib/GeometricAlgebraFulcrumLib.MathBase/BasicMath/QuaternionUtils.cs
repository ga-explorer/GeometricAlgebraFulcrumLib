using System.Collections.Immutable;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Constants;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath
{
    public static class QuaternionUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion ToSystemNumericsQuaternion(this IFloat64Tuple4D quaternion)
        {
            return new Quaternion(
                (float)quaternion.X,
                (float)quaternion.Y,
                (float)quaternion.Z,
                (float)quaternion.W
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D ToQuaternion(this Quaternion quaternion)
        {
            return new Float64Tuple4D(
                quaternion.X,
                quaternion.Y,
                quaternion.Z,
                quaternion.W
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsQuaternionIdentity(this IFloat64Tuple4D quaternion)
        {
            return quaternion.X == 0d &&
                   quaternion.Y == 0d &&
                   quaternion.Z == 0d &&
                   quaternion.W == 1d;
        }


        /// <summary>
        /// Quaternion product
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static Float64Tuple4D QuaternionTimes(this IFloat64Tuple4D value1, IFloat64Tuple4D value2)
        {
            var q1X = value1.X;
            var q1Y = value1.Y;
            var q1Z = value1.Z;
            var q1W = value1.W;

            var q2X = value2.X;
            var q2Y = value2.Y;
            var q2Z = value2.Z;
            var q2W = value2.W;

            // cross(av, bv)
            var cx = q1Y * q2Z - q1Z * q2Y;
            var cy = q1Z * q2X - q1X * q2Z;
            var cz = q1X * q2Y - q1Y * q2X;

            var dot = q1X * q2X + q1Y * q2Y + q1Z * q2Z;

            return new Float64Tuple4D(
                q1X * q2W + q2X * q1W + cx,
                q1Y * q2W + q2Y * q1W + cy,
                q1Z * q2W + q2Z * q1W + cz,
                q1W * q2W - dot
            );
        }

        /// <summary>
        /// Quaternion division
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static Float64Tuple4D QuaternionDivide(this IFloat64Tuple4D value1, IFloat64Tuple4D value2)
        {
            var q1X = value1.X;
            var q1Y = value1.Y;
            var q1Z = value1.Z;
            var q1W = value1.W;

            //-------------------------------------
            // Inverse part.
            var ls =
                value2.X * value2.X + value2.Y * value2.Y +
                value2.Z * value2.Z + value2.W * value2.W;
            var invNorm = 1.0f / ls;

            var q2X = -value2.X * invNorm;
            var q2Y = -value2.Y * invNorm;
            var q2Z = -value2.Z * invNorm;
            var q2W = value2.W * invNorm;

            //-------------------------------------
            // Multiply part.

            // cross(av, bv)
            var cx = q1Y * q2Z - q1Z * q2Y;
            var cy = q1Z * q2X - q1X * q2Z;
            var cz = q1X * q2Y - q1Y * q2X;

            var dot = q1X * q2X + q1Y * q2Y + q1Z * q2Z;

            return new Float64Tuple4D(
                q1X * q2W + q2X * q1W + cx,
                q1Y * q2W + q2Y * q1W + cy,
                q1Z * q2W + q2Z * q1W + cz,
                q1W * q2W - dot
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuaternionDot(this IFloat64Tuple4D v1, IFloat64Tuple4D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z + v1.W * v2.W;
        }

        public static Float64Tuple4D QuaternionConjugate(this IFloat64Tuple4D quaternion)
        {
            return new Float64Tuple4D(
                -quaternion.X,
                -quaternion.Y,
                -quaternion.Z,
                quaternion.W
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D QuaternionInverse(this IFloat64Tuple4D quaternion)
        {
            //  -1   (       a              -v       )
            // q   = ( -------------   ------------- )
            //       (  a^2 + |v|^2  ,  a^2 + |v|^2  )

            var ls =
                quaternion.X * quaternion.X +
                quaternion.Y * quaternion.Y +
                quaternion.Z * quaternion.Z +
                quaternion.W * quaternion.W;

            var invNorm = 1.0f / ls;

            return new Float64Tuple4D(
                -quaternion.X * invNorm,
                -quaternion.Y * invNorm,
                -quaternion.Z * invNorm,
                quaternion.W * invNorm
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D QuaternionNormalize(this IFloat64Tuple4D quaternion)
        {
            var ls =
                quaternion.X * quaternion.X +
                quaternion.Y * quaternion.Y +
                quaternion.Z * quaternion.Z +
                quaternion.W * quaternion.W;

            var invNorm = 1d / Math.Sqrt(ls);

            return new Float64Tuple4D(
                quaternion.X * invNorm,
                quaternion.Y * invNorm,
                quaternion.Z * invNorm,
                quaternion.W * invNorm
            );
        }

        public static Float64Tuple4D QuaternionLerp(IFloat64Tuple4D quaternion1, IFloat64Tuple4D quaternion2, double t)
        {
            var t1 = 1d - t;

            var dot =
                quaternion1.X * quaternion2.X +
                quaternion1.Y * quaternion2.Y +
                quaternion1.Z * quaternion2.Z +
                quaternion1.W * quaternion2.W;

            var r =
                dot >= 0d
                    ? new Float64Tuple4D(
                        t1 * quaternion1.X + t * quaternion2.X,
                        t1 * quaternion1.Y + t * quaternion2.Y,
                        t1 * quaternion1.Z + t * quaternion2.Z,
                        t1 * quaternion1.W + t * quaternion2.W
                    ) :
                    new Float64Tuple4D(
                        t1 * quaternion1.X - t * quaternion2.X,
                        t1 * quaternion1.Y - t * quaternion2.Y,
                        t1 * quaternion1.Z - t * quaternion2.Z,
                        t1 * quaternion1.W - t * quaternion2.W
                    );

            // Normalize it.
            var ls = r.X * r.X + r.Y * r.Y + r.Z * r.Z + r.W * r.W;
            var invNorm = 1d / Math.Sqrt(ls);

            return r * invNorm;
        }

        public static Float64Tuple4D QuaternionSlerp(IFloat64Tuple4D quaternion1, IFloat64Tuple4D quaternion2, double t, double slerpEpsilon = 1e-6)
        {
            var cosOmega =
                quaternion1.X * quaternion2.X +
                quaternion1.Y * quaternion2.Y +
                quaternion1.Z * quaternion2.Z +
                quaternion1.W * quaternion2.W;

            var flip = false;

            if (cosOmega < 0d)
            {
                flip = true;
                cosOmega = -cosOmega;
            }

            double s1, s2;

            if (cosOmega > 1d - slerpEpsilon)
            {
                // Too close, do straight linear interpolation.
                s1 = 1.0f - t;
                s2 = flip ? -t : t;
            }
            else
            {
                var omega = Math.Acos(cosOmega);
                var invSinOmega = 1d / Math.Sin(omega);

                s1 = Math.Sin((1.0f - t) * omega) * invSinOmega;
                s2 = flip
                    ? -Math.Sin(t * omega) * invSinOmega
                    : Math.Sin(t * omega) * invSinOmega;
            }

            return new Float64Tuple4D(
                s1 * quaternion1.X + s2 * quaternion2.X,
                s1 * quaternion1.Y + s2 * quaternion2.Y,
                s1 * quaternion1.Z + s2 * quaternion2.Z,
                s1 * quaternion1.W + s2 * quaternion2.W
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D QuaternionConcatenate(this IFloat64Tuple4D quaternion1, IFloat64Tuple4D quaternion2)
        {
            // Concatenate rotation is actually q2 * q1 instead of q1 * q2.
            // So that's why value2 goes q1 and value1 goes q2.
            var q1X = quaternion2.X;
            var q1Y = quaternion2.Y;
            var q1Z = quaternion2.Z;
            var q1W = quaternion2.W;

            var q2X = quaternion1.X;
            var q2Y = quaternion1.Y;
            var q2Z = quaternion1.Z;
            var q2W = quaternion1.W;

            // cross(av, bv)
            var cx = q1Y * q2Z - q1Z * q2Y;
            var cy = q1Z * q2X - q1X * q2Z;
            var cz = q1X * q2Y - q1Y * q2X;

            var dot = q1X * q2X + q1Y * q2Y + q1Z * q2Z;

            return new Float64Tuple4D(
                q1X * q2W + q2X * q1W + cx,
                q1Y * q2W + q2Y * q1W + cy,
                q1Z * q2W + q2Z * q1W + cz,
                q1W * q2W - dot
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D QuaternionConcatenate(this IFloat64Tuple4D quaternion1, IFloat64Tuple4D quaternion2, IFloat64Tuple4D quaternion3)
        {
            return quaternion1.QuaternionConcatenate(quaternion2).QuaternionConcatenate(quaternion3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D QuaternionConcatenate(this IFloat64Tuple4D quaternion1, params IFloat64Tuple4D[] quaternionList)
        {
            return quaternionList.Aggregate(
                quaternion1.ToTuple4D(),
                QuaternionConcatenate
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D CreateQuaternionFromAngleAxis(this Float64PlanarAngle angle, IFloat64Tuple3D axis)
        {
            var halfAngle = angle * 0.5;
            var s = halfAngle.Sin();
            var c = halfAngle.Cos();

            return new Float64Tuple4D(
                axis.X * s,
                axis.Y * s,
                axis.Z * s,
                c
            );
        }
    
        /// <summary>
        /// Create a rotation unit quaternion from this vector and the given angle. This vector must be
        /// first made of unit length before using this method
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="rotationAngle"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D CreateQuaternionFromAxisAngle(this IFloat64Tuple3D vector, Float64PlanarAngle rotationAngle)
        {
            Debug.Assert(vector.IsNearUnitVector());

            var halfTheta = 0.5 * rotationAngle;
            var cosHalfTheta = halfTheta.Cos();
            var sinHalfTheta = halfTheta.Sin();

            return new Float64Tuple4D(
                sinHalfTheta * vector.X,
                sinHalfTheta * vector.Y,
                sinHalfTheta * vector.Z,
                cosHalfTheta
            );
        }

        /// <summary>
        /// Creates a quaternion from the specified rotation matrix.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <returns>The newly created quaternion.</returns>
        public static Float64Tuple4D CreateQuaternionFromRotationMatrix(this Matrix4x4 matrix)
        {
            var trace = matrix.M11 + matrix.M22 + matrix.M33;

            if (trace > 0.0f)
            {
                var s = Math.Sqrt(trace + 1d);
                var invS = 0.5d / s;

                return new Float64Tuple4D(
                    (matrix.M23 - matrix.M32) * invS,
                    (matrix.M31 - matrix.M13) * invS,
                    (matrix.M12 - matrix.M21) * invS,
                    s * 0.5f
                );
            }

            if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
            {
                var s = Math.Sqrt(1d + matrix.M11 - matrix.M22 - matrix.M33);
                var invS = 0.5d / s;

                return new Float64Tuple4D(
                    0.5d * s,
                    (matrix.M12 + matrix.M21) * invS,
                    (matrix.M13 + matrix.M31) * invS,
                    (matrix.M23 - matrix.M32) * invS
                );
            }

            if (matrix.M22 > matrix.M33)
            {
                var s = Math.Sqrt(1d + matrix.M22 - matrix.M11 - matrix.M33);
                var invS = 0.5d / s;

                return new Float64Tuple4D(
                    (matrix.M21 + matrix.M12) * invS,
                    0.5d * s,
                    (matrix.M32 + matrix.M23) * invS,
                    (matrix.M31 - matrix.M13) * invS
                );
            }
            else
            {
                var s = Math.Sqrt(1d + matrix.M33 - matrix.M11 - matrix.M22);
                var invS = 0.5d / s;

                return new Float64Tuple4D(
                    (matrix.M31 + matrix.M13) * invS,
                    (matrix.M32 + matrix.M23) * invS,
                    0.5d * s,
                    (matrix.M12 - matrix.M21) * invS
                );
            }
        }

        /// <summary>
        /// Creates a new quaternion from the given yaw, pitch, and roll.
        /// </summary>
        /// <param name="yaw">The yaw angle, in radians, around the Y axis.</param>
        /// <param name="pitch">The pitch angle, in radians, around the X axis.</param>
        /// <param name="roll">The roll angle, in radians, around the Z axis.</param>
        /// <returns>The resulting quaternion.</returns>
        public static Float64Tuple4D CreateQuaternionFromYawPitchRoll(double yaw, double pitch, double roll)
        {
            //  Roll first, about axis the object is facing, then
            //  pitch upward, then yaw to face into the new heading
            var halfRoll = roll * 0.5d;
            var sr = Math.Sin(halfRoll);
            var cr = Math.Cos(halfRoll);

            var halfPitch = pitch * 0.5d;
            var sp = Math.Sin(halfPitch);
            var cp = Math.Cos(halfPitch);

            var halfYaw = yaw * 0.5d;
            var sy = Math.Sin(halfYaw);
            var cy = Math.Cos(halfYaw);

            return new Float64Tuple4D(
                cy * sp * cr + sy * cp * sr,
                sy * cp * cr - cy * sp * sr,
                cy * cp * sr - sy * sp * cr,
                cy * cp * cr + sy * sp * sr
            );
        }


        /// <summary>
        /// Compute a unit vector and angle which define a rotation taking
        /// srcUnitVector into dstUnitVector
        /// </summary>
        /// <param name="srcUnitVector"></param>
        /// <param name="dstUnitVector"></param>
        /// <returns></returns>
        public static Tuple<Float64Tuple3D, Float64PlanarAngle> GetRotationAxisAngle(IFloat64Tuple3D srcUnitVector, IFloat64Tuple3D dstUnitVector)
        {
            if (srcUnitVector.IsEqual(dstUnitVector))
                return new Tuple<Float64Tuple3D, Float64PlanarAngle>(srcUnitVector.ToTuple3D(), 0);

            if (srcUnitVector.IsNegativeEqual(dstUnitVector))
                return new Tuple<Float64Tuple3D, Float64PlanarAngle>(
                    srcUnitVector.GetUnitNormal(),
                    Math.PI
                );

            var angle =
                srcUnitVector.GetVectorsAngle(dstUnitVector);

            var axis =
                srcUnitVector.VectorCross(dstUnitVector);

            Debug.Assert(axis != Float64Tuple3D.Zero);

            return new Tuple<Float64Tuple3D, Float64PlanarAngle>(
                axis.ToUnitVector(),
                angle
            );
        }

        /// <summary>
        /// Create a rotation unit quaternion from this vector. The angle of rotation is computed as
        /// 2 * Pi * length of vector.
        /// </summary>
        /// <returns></returns>
        public static Float64Tuple4D ToRotationQuaternion(this IFloat64Tuple3D vector)
        {
            //Compute the vector length and its inverse
            var vectorLength = vector.GetVectorNorm();
            var invVectorLength = 1.0d / vectorLength;

            //Compute the rotation angle
            var halfTheta = (vectorLength - Math.Floor(vectorLength)) * Math.PI;
            var cosHalfTheta = Math.Cos(halfTheta);
            var sinHalfTheta = Math.Sin(halfTheta);

            return new Float64Tuple4D(
                sinHalfTheta * vector.X * invVectorLength,
                sinHalfTheta * vector.Y * invVectorLength,
                sinHalfTheta * vector.Z * invVectorLength,
                cosHalfTheta
            );
        }

        public static SquareMatrix3 RotationQuaternionToSquareMatrix3(this IFloat64Tuple4D quaternion)
        {
            var n = quaternion.GetVectorNormSquared();

            if (n.IsNearZero())
                return SquareMatrix3.CreateIdentityMatrix();

            var x = quaternion.X;
            var y = quaternion.Y;
            var z = quaternion.Z;
            var w = quaternion.W;

            var s = 2d / n;
        
            return new SquareMatrix3
            {
                Scalar00 = 1d - s * (y * y + z * z),
                Scalar10 = s * (x * y + w * z),
                Scalar20 = s * (x * z - w * y),

                Scalar01 = s * (x * y - w * z),
                Scalar11 = 1 - s * (x * x + z * z),
                Scalar21 = s * (y * z + w * x),

                Scalar02 = s * (x * z + w * y),
                Scalar12 = s * (y * z - w * x),
                Scalar22 = 1 - s * (x * x + y * y)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<Float64Tuple3D> QuaternionRotateBasisFrame(this IFloat64Tuple4D quaternion)
        {
            return new Triplet<Float64Tuple3D>(
                quaternion.QuaternionRotate(Float64Tuple3D.E1),
                quaternion.QuaternionRotate(Float64Tuple3D.E2),
                quaternion.QuaternionRotate(Float64Tuple3D.E3)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D QuaternionRotate(this IFloat64Tuple4D quaternion, double x, double y, double z)
        {
            return quaternion.QuaternionRotate(
                new Float64Tuple3D(x, y, z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D QuaternionRotate(this IFloat64Tuple4D quaternion, Axis3D axis)
        {
            var vector = axis.GetVector3D();

            return quaternion.QuaternionRotate(vector.X, vector.Y, vector.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D QuaternionRotate(this IFloat64Tuple4D quaternion, IFloat64Tuple3D vector)
        {
            var rotationMatrix = 
                quaternion.RotationQuaternionToSquareMatrix3();

            return rotationMatrix * vector;

            //Debug.Assert(quaternion.IsNearNormalizedQuaternion());

            //return (quaternion.ToTuple4D() * vector.ToQuaternion() * quaternion.QuaternionInverse()).GetQuaternionVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<Float64Tuple3D> QuaternionRotate(this IFloat64Tuple4D quaternion, IFloat64Tuple3D vector1, IFloat64Tuple3D vector2)
        {
            var rotationMatrix = 
                quaternion.RotationQuaternionToSquareMatrix3();

            return new Pair<Float64Tuple3D>(
                rotationMatrix * vector1,
                rotationMatrix * vector2
            );

            //return new Pair<Tuple3D>(
            //    quaternion.QuaternionRotate(vector1),
            //    quaternion.QuaternionRotate(vector2)
            //);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<Float64Tuple3D> QuaternionRotate(this IFloat64Tuple4D quaternion, IFloat64Tuple3D vector1, IFloat64Tuple3D vector2, IFloat64Tuple3D vector3)
        {
            var rotationMatrix = 
                quaternion.RotationQuaternionToSquareMatrix3();

            return new Triplet<Float64Tuple3D>(
                rotationMatrix * vector1,
                rotationMatrix * vector2,
                rotationMatrix * vector3
            );

            //return new Triplet<Tuple3D>(
            //    quaternion.QuaternionRotate(vector1),
            //    quaternion.QuaternionRotate(vector2),
            //    quaternion.QuaternionRotate(vector3)
            //);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<Float64Tuple3D> QuaternionRotate(this IFloat64Tuple4D quaternion, params IFloat64Tuple3D[] vectorArray)
        {
            var rotationMatrix = 
                quaternion.RotationQuaternionToSquareMatrix3();

            return vectorArray
                .Select(vector => rotationMatrix * vector)
                .ToImmutableArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Tuple3D> QuaternionRotate(this IFloat64Tuple4D quaternion, IEnumerable<IFloat64Tuple3D> vectorList)
        {
            var rotationMatrix = 
                quaternion.RotationQuaternionToSquareMatrix3();

            return vectorList.Select(vector => rotationMatrix * vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D RotateUsingQuaternion(this IFloat64Tuple3D vector, IFloat64Tuple4D quaternion)
        {
            return quaternion.QuaternionRotate(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D RotateUsingAxisAngle(this IFloat64Tuple3D vector, IFloat64Tuple3D rotationVector, Float64PlanarAngle rotationAngle)
        {
            return rotationVector
                .CreateQuaternionFromAxisAngle(rotationAngle)
                .QuaternionRotate(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D RotateUsingVector(this IFloat64Tuple3D vector, IFloat64Tuple3D rotationVector)
        {
            return rotationVector
                .ToRotationQuaternion()
                .QuaternionRotate(vector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZeroQuaternion(this IFloat64Tuple4D quaternion, double epsilon = 1e-12)
        {
            return quaternion.X.IsNearZero(epsilon) &&
                   quaternion.Y.IsNearZero(epsilon) &&
                   quaternion.Z.IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearNormalizedQuaternion(this IFloat64Tuple4D quaternion, double epsilon = 1e-12)
        {
            return (quaternion.GetVectorNormSquared() - 1f).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D ToQuaternion(this IFloat64Tuple3D vector)
        {
            return new Float64Tuple4D(
                vector.X,
                vector.Y,
                vector.Z,
                0f
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D ToQuaternion(this Axis3D axis)
        {
            return axis switch
            {
                Axis3D.PositiveX => new Float64Tuple4D(1, 0, 0, 0),
                Axis3D.PositiveY => new Float64Tuple4D(0, 1, 0, 0),
                Axis3D.PositiveZ => new Float64Tuple4D(0, 0, 1, 0),
                Axis3D.NegativeX => new Float64Tuple4D(-1, 0, 0, 0),
                Axis3D.NegativeY => new Float64Tuple4D(0, -1, 0, 0),
                _ => new Float64Tuple4D(0, 0, -1, 0),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D ToQuaternion(this IFloat64Tuple3D vector, double scalar)
        {
            return new Float64Tuple4D(
                vector.X,
                vector.Y,
                vector.Z,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetQuaternionScalarPart(this IFloat64Tuple4D quaternion)
        {
            return quaternion.W;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetQuaternionVectorPart(this IFloat64Tuple4D quaternion)
        {
            return new Float64Tuple3D(quaternion.X, quaternion.Y, quaternion.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, Float64Tuple3D> GetQuaternionScalarVectorParts(this IFloat64Tuple4D quaternion)
        {
            return new Tuple<double, Float64Tuple3D>(
                quaternion.W,
                new Float64Tuple3D(quaternion.X, quaternion.Y, quaternion.Z)
            );
        }


        public static Float64Tuple4D CreateAxisToAxisRotationQuaternion(this Axis3D axis1, Axis3D axis2)
        {
            var sqrt2Inv = 1d / Math.Sqrt(2d);

            return axis1 switch
            {
                Axis3D.PositiveX => axis2 switch
                {
                    Axis3D.PositiveX => Float64Tuple4D.IdentityQuaternion,
                    Axis3D.PositiveY => new Float64Tuple4D(0, 0, sqrt2Inv, sqrt2Inv),
                    Axis3D.PositiveZ => new Float64Tuple4D(0, -sqrt2Inv, 0, sqrt2Inv),
                    Axis3D.NegativeX => new Float64Tuple4D(0, 0, 1, 0),
                    Axis3D.NegativeY => new Float64Tuple4D(0, 0, -sqrt2Inv, sqrt2Inv),
                    _ => new Float64Tuple4D(0, sqrt2Inv, 0, sqrt2Inv),
                },

                Axis3D.PositiveY => axis2 switch
                {
                    Axis3D.PositiveX => new Float64Tuple4D(0, 0, -sqrt2Inv, sqrt2Inv),
                    Axis3D.PositiveY => Float64Tuple4D.IdentityQuaternion,
                    Axis3D.PositiveZ => new Float64Tuple4D(sqrt2Inv, 0, 0, sqrt2Inv),
                    Axis3D.NegativeX => new Float64Tuple4D(0, 0, sqrt2Inv, sqrt2Inv),
                    Axis3D.NegativeY => new Float64Tuple4D(1, 0, 0, 0),
                    _ => new Float64Tuple4D(-sqrt2Inv, 0, 0, sqrt2Inv),
                },

                Axis3D.PositiveZ => axis2 switch
                {
                    Axis3D.PositiveX => new Float64Tuple4D(0, sqrt2Inv, 0, sqrt2Inv),
                    Axis3D.PositiveY => new Float64Tuple4D(-sqrt2Inv, 0, 0, sqrt2Inv),
                    Axis3D.PositiveZ => Float64Tuple4D.IdentityQuaternion,
                    Axis3D.NegativeX => new Float64Tuple4D(0, -sqrt2Inv, 0, sqrt2Inv),
                    Axis3D.NegativeY => new Float64Tuple4D(sqrt2Inv, 0, 0, sqrt2Inv),
                    _ => new Float64Tuple4D(0, 1, 0, 0),
                },

                Axis3D.NegativeX => axis2 switch
                {
                    Axis3D.PositiveX => new Float64Tuple4D(0, 0, 1, 0),
                    Axis3D.PositiveY => new Float64Tuple4D(0, 0, -sqrt2Inv, sqrt2Inv),
                    Axis3D.PositiveZ => new Float64Tuple4D(0, sqrt2Inv, 0, sqrt2Inv),
                    Axis3D.NegativeX => Float64Tuple4D.IdentityQuaternion,
                    Axis3D.NegativeY => new Float64Tuple4D(0, 0, sqrt2Inv, sqrt2Inv),
                    _ => new Float64Tuple4D(0, -sqrt2Inv, 0, sqrt2Inv),
                },

                Axis3D.NegativeY => axis2 switch
                {
                    Axis3D.PositiveX => new Float64Tuple4D(0, 0, sqrt2Inv, sqrt2Inv),
                    Axis3D.PositiveY => new Float64Tuple4D(1, 0, 0, 0),
                    Axis3D.PositiveZ => new Float64Tuple4D(-sqrt2Inv, 0, 0, sqrt2Inv),
                    Axis3D.NegativeX => new Float64Tuple4D(0, 0, -sqrt2Inv, sqrt2Inv),
                    Axis3D.NegativeY => Float64Tuple4D.IdentityQuaternion,
                    _ => new Float64Tuple4D(sqrt2Inv, 0, 0, sqrt2Inv),
                },

                _ => axis2 switch
                {
                    Axis3D.PositiveX => new Float64Tuple4D(0, -sqrt2Inv, 0, sqrt2Inv),
                    Axis3D.PositiveY => new Float64Tuple4D(sqrt2Inv, 0, 0, sqrt2Inv),
                    Axis3D.PositiveZ => new Float64Tuple4D(0, 1, 0, 0),
                    Axis3D.NegativeX => new Float64Tuple4D(0, sqrt2Inv, 0, sqrt2Inv),
                    Axis3D.NegativeY => new Float64Tuple4D(-sqrt2Inv, 0, 0, sqrt2Inv),
                    _ => Float64Tuple4D.IdentityQuaternion,
                },
            };
        }

        public static Tuple<IFloat64Tuple3D, Float64PlanarAngle> CreateAxisToVectorRotationAxisAngle(this Axis3D axis, IFloat64Tuple3D unitVector)
        {
            //Debug.Assert(
            //    (unitVector.GetLengthSquared() - 1).IsNearZero()
            //);

            var dot12 = axis.VectorDot(unitVector);

            // The case where the two vectors are almost the same
            if ((dot12 - 1d).IsNearZero())
                return new Tuple<IFloat64Tuple3D, Float64PlanarAngle>(
                    unitVector,
                    Float64PlanarAngle.Angle0
                );

            // The case where the two vectors are almost opposite
            if ((dot12 + 1d).IsNearZero())
                return new Tuple<IFloat64Tuple3D, Float64PlanarAngle>(
                    axis.GetUnitNormal().GetVector3D(),
                    Float64PlanarAngle.Angle180
                );

            // The general case
            return new Tuple<IFloat64Tuple3D, Float64PlanarAngle>(
                axis.VectorUnitCross(unitVector),
                Math.Acos(dot12)
            );
        }

        public static Tuple<IFloat64Tuple3D, Float64PlanarAngle> CreateVectorToVectorRotationAxisAngle(this IFloat64Tuple3D unitVector1, IFloat64Tuple3D unitVector2)
        {
            Debug.Assert(
                (unitVector1.GetVectorNormSquared() - 1).IsNearZero() &&
                (unitVector2.GetVectorNormSquared() - 1).IsNearZero()
            );

            var dot12 = unitVector1.VectorDot(unitVector2);

            // The case where the two vectors are almost identical
            if ((dot12 - 1d).IsNearZero())
                return new Tuple<IFloat64Tuple3D, Float64PlanarAngle>(
                    unitVector1,
                    Float64PlanarAngle.Angle0
                );

            // The case where the two vectors are almost opposite
            if ((dot12 + 1d).IsNearZero())
                return new Tuple<IFloat64Tuple3D, Float64PlanarAngle>(
                    unitVector1.GetUnitNormal(),
                    Float64PlanarAngle.Angle180
                );

            Debug.Assert(dot12.Abs() < 1);

            // The general case
            return new Tuple<IFloat64Tuple3D, Float64PlanarAngle>(
                unitVector1.VectorUnitCross(unitVector2),
                Math.Acos(dot12)
            );
        }
    
        public static Tuple<IFloat64Tuple3D, Float64PlanarAngle> CreateVectorToVectorRotationAxisAngle(this IFloat64Tuple3D unitVector1, IFloat64Tuple3D unitVector2, IFloat64Tuple3D unitNormal)
        {
            Debug.Assert(
                (unitVector1.GetVectorNormSquared() - 1).IsNearZero(1e-7) &&
                (unitVector2.GetVectorNormSquared() - 1).IsNearZero(1e-7) &&
                (unitNormal.GetVectorNormSquared() - 1).IsNearZero(1e-7) &&
                unitNormal.VectorDot(unitVector1).IsNearZero(1e-5) && 
                unitNormal.VectorDot(unitVector2).IsNearZero(1e-5)
            );

            var dot12 = unitVector1.VectorDot(unitVector2);

            // The case where the two vectors are almost identical
            if ((dot12 - 1d).IsNearZero())
                return new Tuple<IFloat64Tuple3D, Float64PlanarAngle>(
                    unitNormal,
                    Float64PlanarAngle.Angle0
                );

            // The case where the two vectors are almost opposite
            if ((dot12 + 1d).IsNearZero())
                return new Tuple<IFloat64Tuple3D, Float64PlanarAngle>(
                    unitNormal,
                    Float64PlanarAngle.Angle180
                );

            Debug.Assert(dot12.Abs() < 1);

            var angle = unitVector1.VectorUnitCross(unitVector2).VectorDot(unitNormal) > 0
                ? Math.Acos(dot12) 
                : -Math.Acos(dot12);

            // The general case
            return new Tuple<IFloat64Tuple3D, Float64PlanarAngle>(
                unitNormal,
                angle
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D CreateVectorToVectorRotationQuaternion(this IFloat64Tuple3D unitVector1, IFloat64Tuple3D unitVector2)
        {
            var (u, a) =
                unitVector1.CreateVectorToVectorRotationAxisAngle(unitVector2);

            return u.CreateQuaternionFromAxisAngle(a);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D CreateVectorToVectorRotationQuaternion(this IFloat64Tuple3D unitVector1, IFloat64Tuple3D unitVector2, IFloat64Tuple3D unitNormal)
        {
            var (u, a) =
                unitVector1.CreateVectorToVectorRotationAxisAngle(unitVector2, unitNormal);

            return u.CreateQuaternionFromAxisAngle(a);
        }

        public static Tuple<Axis3D, Float64Tuple4D> CreateNearestAxisToVectorRotationQuaternion(this IFloat64Tuple3D unitVector)
        {
            Debug.Assert(
                (unitVector.GetVectorNormSquared() - 1).IsNearZero()
            );

            var axis = unitVector.SelectNearestAxis();

            var x = 0d;
            var y = 0d;
            var z = 0d;
            var w = 0d;

            if (axis == Axis3D.PositiveX)
            {
                var v1 = 1d + unitVector.X;
                var v2 = 1d / Math.Sqrt(2d * v1);

                y = -unitVector.Z * v2;
                z = unitVector.Y * v2;
                w = v1 * v2;
            }

            if (axis == Axis3D.NegativeX)
            {
                var v1 = 1d - unitVector.X;
                var v2 = 1d / Math.Sqrt(2d * v1);

                y = unitVector.Z * v2;
                z = -unitVector.Y * v2;
                w = v1 * v2;
            }

            if (axis == Axis3D.PositiveY)
            {
                var v1 = 1d + unitVector.Y;
                var v2 = 1d / Math.Sqrt(2d * v1);

                x = unitVector.Z * v2;
                z = -unitVector.X * v2;
                w = v1 * v2;
            }

            if (axis == Axis3D.NegativeY)
            {
                var v1 = 1d - unitVector.Y;
                var v2 = 1d / Math.Sqrt(2d * v1);

                x = -unitVector.Z * v2;
                z = unitVector.X * v2;
                w = v1 * v2;
            }

            if (axis == Axis3D.PositiveZ)
            {
                var v1 = 1d + unitVector.Z;
                var v2 = 1d / Math.Sqrt(2d * v1);

                x = -unitVector.Y * v2;
                y = unitVector.X * v2;
                w = v1 * v2;
            }

            if (axis == Axis3D.NegativeZ)
            {
                var v1 = 1d - unitVector.Z;
                var v2 = 1d / Math.Sqrt(2d * v1);

                x = unitVector.Y * v2;
                y = -unitVector.X * v2;
                w = v1 * v2;
            }

            var quaternion = new Float64Tuple4D(x, y, z, w);

            return Tuple.Create(axis, quaternion);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D CreateAxisToVectorRotationQuaternion(this IFloat64Tuple3D unitVector, Axis3D axis)
        {
            return axis.CreateAxisToVectorRotationQuaternion(unitVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D CreateAxisPairToVectorPairRotationQuaternion(this Axis3D axis1, Axis3D axis2, IFloat64Tuple3D unitVector1, IFloat64Tuple3D unitVector2)
        {
            Debug.Assert(
                unitVector1.GetVectorNormSquared().IsNearEqual(1) &&
                unitVector2.GetVectorNormSquared().IsNearEqual(1)
            );

            var q1 =
                axis1.CreateAxisToVectorRotationQuaternion(unitVector1);

            var axis2Rotated =
                q1.QuaternionRotate(axis2).ToUnitVector();

            var q2 =
                axis2Rotated.CreateVectorToVectorRotationQuaternion(unitVector2, unitVector1);

            var quaternion = 
                q1.QuaternionConcatenate(q2);

            Debug.Assert(
                (quaternion.QuaternionRotate(axis1) - unitVector1).GetVectorNormSquared().IsNearZero()
            );
        
            Debug.Assert(
                (quaternion.QuaternionRotate(axis2) - unitVector2).GetVectorNormSquared().IsNearZero()
            );

            return quaternion;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<Float64Tuple4D> CreateAxisPairToVectorPairRotationQuaternionPair(this Axis3D axis1, Axis3D axis2, IFloat64Tuple3D unitVector1, IFloat64Tuple3D unitVector2)
        {
            Debug.Assert(
                unitVector1.GetVectorNormSquared().IsNearEqual(1) &&
                unitVector2.GetVectorNormSquared().IsNearEqual(1)
            );

            var q1 =
                axis1.CreateAxisToVectorRotationQuaternion(unitVector1);

            var axis2Rotated =
                q1.QuaternionRotate(axis2).ToUnitVector();

            var q2 =
                axis2Rotated.CreateVectorToVectorRotationQuaternion(unitVector2, unitVector1);

            Debug.Assert(
                (q1.QuaternionConcatenate(q2).QuaternionRotate(axis1) - unitVector1).GetVectorNormSquared().IsNearZero()
            );
        
            Debug.Assert(
                (q1.QuaternionConcatenate(q2).QuaternionRotate(axis2) - unitVector2).GetVectorNormSquared().IsNearZero()
            );

            return new Pair<Float64Tuple4D>(q1, q2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple4D CreateAxisToVectorRotationQuaternion(this Axis3D axis, IFloat64Tuple3D unitVector)
        {
            var (u, a) =
                axis.CreateAxisToVectorRotationAxisAngle(unitVector);

            return u.CreateQuaternionFromAxisAngle(a);

            //This gives a correct quaternion but not the simplest one (the one with smallest angle)
            //var (nearestAxis, q2) =
            //    unitVector.CreateNearestAxisToVectorRotationQuaternion();

            //var q1 =
            //    axis.CreateAxisToAxisRotationQuaternion(nearestAxis);

            //return Tuple4D.Concatenate(q1, q2);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearZero(this Quaternion quaternion, float epsilon = 1e-5f)
        {
            return quaternion.X.IsNearZero(epsilon) &&
                   quaternion.Y.IsNearZero(epsilon) &&
                   quaternion.Z.IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNearNormalized(this Quaternion quaternion, float epsilon = 1e-5f)
        {
            return (quaternion.LengthSquared() - 1f).IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion ToSystemNumericsQuaternion(this IFloat64Tuple3D vector)
        {
            return new Quaternion(
                (float)vector.X,
                (float)vector.Y,
                (float)vector.Z,
                0f
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion ToSystemNumericsQuaternion(this Axis3D axis)
        {
            return axis switch
            {
                Axis3D.PositiveX => new Quaternion(1, 0, 0, 0),
                Axis3D.PositiveY => new Quaternion(0, 1, 0, 0),
                Axis3D.PositiveZ => new Quaternion(0, 0, 1, 0),
                Axis3D.NegativeX => new Quaternion(-1, 0, 0, 0),
                Axis3D.NegativeY => new Quaternion(0, -1, 0, 0),
                _ => new Quaternion(0, 0, -1, 0),
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion ToSystemNumericsQuaternion(this IFloat64Tuple3D vector, double scalar)
        {
            return new Quaternion(
                (float)vector.X,
                (float)vector.Y,
                (float)vector.Z,
                (float)scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetScalarPart(this Quaternion quaternion)
        {
            return quaternion.W;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D GetVectorPart(this Quaternion quaternion)
        {
            return new Float64Tuple3D(quaternion.X, quaternion.Y, quaternion.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, Float64Tuple3D> GetScalarVectorParts(this Quaternion quaternion)
        {
            return new Tuple<double, Float64Tuple3D>(
                quaternion.W,
                new Float64Tuple3D(quaternion.X, quaternion.Y, quaternion.Z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Conjugate(this Quaternion quaternion)
        {
            return Quaternion.Conjugate(quaternion);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Inverse(this Quaternion quaternion)
        {
            return Quaternion.Inverse(quaternion);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Normalize(this Quaternion quaternion)
        {
            return Quaternion.Normalize(quaternion);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Concatenate(this Quaternion quaternion1, Quaternion quaternion2)
        {
            return Quaternion.Concatenate(
                quaternion1,
                quaternion2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Concatenate(this Quaternion quaternion1, Quaternion quaternion2, Quaternion quaternion3)
        {
            return Quaternion.Concatenate(
                Quaternion.Concatenate(
                    quaternion1,
                    quaternion2
                ),
                quaternion3
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion Concatenate(this Quaternion quaternion1, params Quaternion[] quaternionList)
        {
            return quaternionList.Aggregate(
                quaternion1,
                Quaternion.Concatenate
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D Rotate(this Quaternion quaternion, Axis3D axis)
        {
            Debug.Assert(
                quaternion.IsNearNormalized()
            );

            var q = quaternion.ToQuaternion();

            return (q * axis.ToQuaternion() * q.QuaternionInverse()).GetQuaternionVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D Rotate(this Quaternion quaternion, double x, double y, double z)
        {
            Debug.Assert(
                quaternion.IsNearNormalized()
            );

            var vector = new Float64Tuple4D(x, y, z, 0);

            var q = quaternion.ToQuaternion();

            return (q * vector * q.QuaternionInverse()).GetQuaternionVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D Rotate(this Quaternion quaternion, IFloat64Tuple3D vector)
        {
            Debug.Assert(
                quaternion.IsNearNormalized()
            );

            var q = quaternion.ToQuaternion();

            return (q * vector.ToQuaternion() * q.QuaternionInverse()).GetQuaternionVectorPart();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D RotateUsing(this IFloat64Tuple3D vector, Quaternion quaternion)
        {
            Debug.Assert(
                quaternion.IsNearNormalized()
            );

            var q = quaternion.ToQuaternion();

            return (q * vector.ToQuaternion() * q.QuaternionInverse()).GetQuaternionVectorPart();
        }
    }
}