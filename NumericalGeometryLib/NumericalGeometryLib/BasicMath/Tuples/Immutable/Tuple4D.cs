using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace NumericalGeometryLib.BasicMath.Tuples.Immutable
{
    /// <summary>
    /// This class can represent a 4D vector or a 3D quaternion
    /// </summary>
    public sealed record Tuple4D : 
        ITuple4D
    {
        public static Tuple4D Zero { get; } = new Tuple4D(0, 0, 0, 0);

        public static Tuple4D E1 { get; } = new Tuple4D(1, 0, 0, 0);

        public static Tuple4D E2 { get; } = new Tuple4D(0, 1, 0, 0);

        public static Tuple4D E3 { get; } = new Tuple4D(0, 0, 1, 0);

        public static Tuple4D E4 { get; } = new Tuple4D(0, 0, 0, 1);

        public static Tuple4D NegativeE1 { get; } = new Tuple4D(-1, 0, 0, 0);

        public static Tuple4D NegativeE2 { get; } = new Tuple4D(0, -1, 0, 0);

        public static Tuple4D NegativeE3 { get; } = new Tuple4D(0, 0, -1, 0);
        
        public static Tuple4D NegativeE4 { get; } = new Tuple4D(0, 0, 0, -1);

        public static Tuple4D IdentityQuaternion { get; } = new Tuple4D(0, 0, 0, 1);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple4D CreateAffineVector(double x, double y, double z)
        {
            return new Tuple4D(x, y, z, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple4D CreateAffinePoint(double x, double y, double z)
        {
            return new Tuple4D(x, y, z, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D CreateQuaternionFromAxisAngle(ITuple3D axis, PlanarAngle angle)
        {
            var halfAngle = angle * 0.5;
            var s = halfAngle.Sin();
            var c = halfAngle.Cos();

            return new Tuple4D(
                axis.X * s,
                axis.Y * s,
                axis.Z * s,
                c
            );
        }

        /// <summary>
        /// Creates a quaternion from the specified rotation matrix.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <returns>The newly created quaternion.</returns>
        public static Tuple4D CreateQuaternionFromRotationMatrix(Matrix4x4 matrix)
        {
            var trace = matrix.M11 + matrix.M22 + matrix.M33;

            if (trace > 0.0f)
            {
                var s = Math.Sqrt(trace + 1d);
                var invS = 0.5d / s;

                return new Tuple4D(
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

                return new Tuple4D(
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

                return new Tuple4D(
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

                return new Tuple4D(
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
        public static Tuple4D CreateQuaternionFromYawPitchRoll(double yaw, double pitch, double roll)
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

            return new Tuple4D(
                cy * sp * cr + sy * cp * sr,
                sy * cp * cr - cy * sp * sr,
                cy * cp * sr - sy * sp * cr,
                cy * cp * cr + sy * sp * sr
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator -(Tuple4D v1)
        {
            return new Tuple4D(-v1.X, -v1.Y, -v1.Z, -v1.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator +(Tuple4D v1, ITuple4D v2)
        {
            return new Tuple4D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator +(ITuple4D v1, Tuple4D v2)
        {
            return new Tuple4D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator +(Tuple4D v1, Tuple4D v2)
        {
            return new Tuple4D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator -(Tuple4D v1, ITuple4D v2)
        {
            return new Tuple4D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator -(ITuple4D v1, Tuple4D v2)
        {
            return new Tuple4D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator -(Tuple4D v1, Tuple4D v2)
        {
            return new Tuple4D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator *(Tuple4D v1, double s)
        {
            return new Tuple4D(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator *(double s, Tuple4D v1)
        {
            return new Tuple4D(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator /(Tuple4D v1, double s)
        {
            Debug.Assert(!s.IsAlmostZero());

            s = 1.0d / s;
            return new Tuple4D(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
        }

        /// <summary>
        /// Quaternion product
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static Tuple4D operator *(Tuple4D value1, Tuple4D value2)
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

            return new Tuple4D(
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
        public static Tuple4D operator /(Tuple4D value1, Tuple4D value2)
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

            return new Tuple4D(
                q1X * q2W + q2X * q1W + cx,
                q1Y * q2W + q2Y * q1W + cy,
                q1Z * q2W + q2Z * q1W + cz,
                q1W * q2W - dot
            );
        }


        /// <summary>
        /// The 1st component of this tuple. If this tuple holds a quaternion, this is the 1st component
        /// of its imaginary (i.e. vector) part
        /// </summary>
        public double X { get; }

        /// <summary>
        /// The 2nd component of this tuple. If this tuple holds a quaternion, this is the 2nd component
        /// of its imaginary (i.e. vector) part
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// The 3rd component of this tuple. If this tuple holds a quaternion, this is the 3rd component
        /// of its imaginary (i.e. vector) part
        /// </summary>
        public double Z { get; }

        /// <summary>
        /// The 4th component of this tuple. If this tuple holds a quaternion, this is its scalar part
        /// </summary>
        public double W { get; }

        public double Item1 => X;

        public double Item2 => Y;

        public double Item3 => Z;

        public double Item4 => W;

        /// <summary>
        /// Get or set the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                Debug.Assert(index is >= 0 and <= 3);

                return index switch
                {
                    0 => X,
                    1 => Y,
                    2 => Z,
                    3 => W,
                    _ => 0.0d
                };
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple4D(double x, double y, double z, double w)
        {
            Debug.Assert(
                x.IsNotNaN() &&
                y.IsNotNaN() &&
                z.IsNotNaN() &&
                w.IsNotNaN()
            );

            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple4D(ITriplet<double> v, double s)
        {
            Debug.Assert(
                v.Item1.IsNotNaN() &&
                v.Item2.IsNotNaN() &&
                v.Item3.IsNotNaN() &&
                s.IsNotNaN()
            );

            X = v.Item1;
            Y = v.Item2;
            Z = v.Item3;
            W = s;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple4D(IQuad<double> v)
        {
            Debug.Assert(
                v.Item1.IsNotNaN() &&
                v.Item2.IsNotNaN() &&
                v.Item3.IsNotNaN() &&
                v.Item4.IsNotNaN()
            );

            X = v.Item1;
            Y = v.Item2;
            Z = v.Item3;
            W = v.Item4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple4D(Quaternion v)
        {
            Debug.Assert(
                v.X.IsNotNaN() &&
                v.Y.IsNotNaN() &&
                v.Z.IsNotNaN() &&
                v.W.IsNotNaN()
            );

            X = v.X;
            Y = v.Y;
            Z = v.Z;
            W = v.W;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return !double.IsNaN(X) &&
                   !double.IsNaN(Y) &&
                   !double.IsNaN(Z) &&
                   !double.IsNaN(W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({X:G}, {Y:G}, {Z:G}, {W:G})";
        }
    }
}
