using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D
{
    /// <summary>
    /// This class can represent a 3D quaternion
    /// https://en.wikipedia.org/wiki/Quaternion
    /// </summary>
    public sealed record Float64Quaternion :
        IFloat64Multivector3D
    {
        public static Float64Quaternion Identity { get; } 
            = new Float64Quaternion(0, 0, 0, 1);
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion Create(Float64Scalar iScalar, Float64Scalar jScalar, Float64Scalar kScalar, Float64Scalar scalar)
        {
            return new Float64Quaternion(iScalar, jScalar, kScalar, scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion Create(Float64Scalar scalar, Float64Bivector3D bivectorPart)
        {
            return new Float64Quaternion(bivectorPart, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion Create(Float64Scalar scalar)
        {
            return new Float64Quaternion(Float64Bivector3D.Zero, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion Create(Float64Bivector3D bivectorPart)
        {
            return new Float64Quaternion(bivectorPart, Float64Scalar.Zero);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion Create(Quaternion v)
        {
            return new Float64Quaternion(
                -v.X, 
                -v.Y, 
                -v.Z, 
                v.W
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion CreateFromPlaneAndAngle(Float64Bivector3D bivector, Float64PlanarAngle angle)
        {
            var halfAngle = angle.GetHalfAngleInPositiveRange();
            
            var scalar = 
                halfAngle.Cos();

            var bivectorPart = 
                bivector * (halfAngle.Sin() / bivector.Norm());

            return new Float64Quaternion(
                bivectorPart,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion CreateFromNormalAndAngle(LinUnitBasisVector3D axis, Float64PlanarAngle angle)
        {
            var halfAngle = angle.GetHalfAngleInPositiveRange();
            
            var scalar = 
                halfAngle.Cos();
            
            var vector = 
                axis.ToVector3D(-halfAngle.Sin());

            return new Float64Quaternion(
                vector.X,
                vector.Y,
                vector.Z,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion CreateFromNormalAndAngle(Float64Vector3D axis, Float64PlanarAngle angle)
        {
            var halfAngle = angle.GetHalfAngleInPositiveRange();
            
            var scalar = 
                halfAngle.Cos();
            
            var vector = 
                axis.SetLength(-halfAngle.Sin());
            
            return new Float64Quaternion(
                vector.X,
                vector.Y,
                vector.Z,
                scalar
            );
        }

        /// <summary>
        /// Creates a quaternion from the specified rotation matrix.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <returns>The newly created quaternion.</returns>
        public static Float64Quaternion CreateFromRotationMatrix(Matrix4x4 matrix)
        {
            var trace = matrix.M11 + matrix.M22 + matrix.M33;

            if (trace > 0.0f)
            {
                var s = Math.Sqrt(trace + 1d);
                var invS = 0.5d / s;

                return new Float64Quaternion(
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

                return new Float64Quaternion(
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

                return new Float64Quaternion(
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

                return new Float64Quaternion(
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
        public static Float64Quaternion CreateFromYawPitchRoll(double yaw, double pitch, double roll)
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

            return new Float64Quaternion(
                cy * sp * cr + sy * cp * sr,
                sy * cp * cr - cy * sp * sr,
                cy * cp * sr - sy * sp * cr,
                cy * cp * cr + sy * sp * sr
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion operator -(Float64Quaternion v1)
        {
            return new Float64Quaternion(-v1.ScalarI, -v1.ScalarJ, -v1.ScalarK, -v1.Scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion operator +(Float64Quaternion v1, Float64Quaternion v2)
        {
            return new Float64Quaternion(v1.ScalarI + v2.ScalarI, v1.ScalarJ + v2.ScalarJ, v1.ScalarK + v2.ScalarK, v1.Scalar + v2.Scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion operator -(Float64Quaternion v1, Float64Quaternion v2)
        {
            return new Float64Quaternion(v1.ScalarI - v2.ScalarI, v1.ScalarJ - v2.ScalarJ, v1.ScalarK - v2.ScalarK, v1.Scalar - v2.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion operator *(Float64Quaternion v1, double s)
        {
            return new Float64Quaternion(v1.ScalarI * s, v1.ScalarJ * s, v1.ScalarK * s, v1.Scalar * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion operator *(double s, Float64Quaternion v1)
        {
            return new Float64Quaternion(v1.ScalarI * s, v1.ScalarJ * s, v1.ScalarK * s, v1.Scalar * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion operator /(Float64Quaternion v1, double s)
        {
            Debug.Assert(!s.IsAlmostZero());

            s = 1.0d / s;
            return new Float64Quaternion(v1.ScalarI * s, v1.ScalarJ * s, v1.ScalarK * s, v1.Scalar * s);
        }

        /// <summary>
        /// Quaternion product
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="q2"></param>
        /// <returns></returns>
        public static Float64Quaternion operator *(Float64Quaternion value1, Float64Quaternion q2)
        {
            var q1X = value1.ScalarI;
            var q1Y = value1.ScalarJ;
            var q1Z = value1.ScalarK;
            var q1W = value1.Scalar;

            var q2X = q2.ScalarI;
            var q2Y = q2.ScalarJ;
            var q2Z = q2.ScalarK;
            var q2W = q2.Scalar;

            // cross(av, bv)
            var cx = q1Y * q2Z - q1Z * q2Y;
            var cy = q1Z * q2X - q1X * q2Z;
            var cz = q1X * q2Y - q1Y * q2X;

            var dot = q1X * q2X + q1Y * q2Y + q1Z * q2Z;

            return new Float64Quaternion(
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
        /// <param name="q2"></param>
        /// <returns></returns>
        public static Float64Quaternion operator /(Float64Quaternion value1, Float64Quaternion q2)
        {
            var q1X = value1.ScalarI;
            var q1Y = value1.ScalarJ;
            var q1Z = value1.ScalarK;
            var q1W = value1.Scalar;

            //-------------------------------------
            // Inverse part.
            var ls =
                q2.ScalarI * q2.ScalarI + q2.ScalarJ * q2.ScalarJ +
                q2.ScalarK * q2.ScalarK + q2.Scalar * q2.Scalar;
            var invNorm = 1.0f / ls;

            var q2X = -q2.ScalarI * invNorm;
            var q2Y = -q2.ScalarJ * invNorm;
            var q2Z = -q2.ScalarK * invNorm;
            var q2W = q2.Scalar * invNorm;

            //-------------------------------------
            // Multiply part.

            // cross(av, bv)
            var cx = q1Y * q2Z - q1Z * q2Y;
            var cy = q1Z * q2X - q1X * q2Z;
            var cz = q1X * q2Y - q1Y * q2X;

            var dot = q1X * q2X + q1Y * q2Y + q1Z * q2Z;

            return new Float64Quaternion(
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
        public Float64Scalar ScalarI { get; }

        /// <summary>
        /// The 2nd component of this tuple. If this tuple holds a quaternion, this is the 2nd component
        /// of its imaginary (i.e. vector) part
        /// </summary>
        public Float64Scalar ScalarJ { get; }

        /// <summary>
        /// The 3rd component of this tuple. If this tuple holds a quaternion, this is the 3rd component
        /// of its imaginary (i.e. vector) part
        /// </summary>
        public Float64Scalar ScalarK { get; }

        /// <summary>
        /// The 4th component of this tuple. If this tuple holds a quaternion, this is its scalar part
        /// </summary>
        public Float64Scalar Scalar { get; }

        public Float64Scalar Scalar1
            => Float64Scalar.Zero;

        public Float64Scalar Scalar2 
            => Float64Scalar.Zero;

        public Float64Scalar Scalar3  
            => Float64Scalar.Zero;

        public Float64Scalar Scalar12 
            => -ScalarK;

        public Float64Scalar Scalar13 
            => ScalarJ;

        public Float64Scalar Scalar23 
            => -ScalarI;

        public Float64Scalar Scalar123 
            => Float64Scalar.Zero;

        public int VSpaceDimensions 
            => 4;

        public double Item1 
            => ScalarI.Value;

        public double Item2 
            => ScalarJ.Value;

        public double Item3 
            => ScalarK.Value;

        public double Item4 
            => Scalar.Value;
        
        public int Count 
            => 8;

        /// <summary>
        /// Get or set the ith component of this multivector
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Float64Scalar this[int index]
        {
            get
            {
                if (index is < 0 or > 7)
                    throw new IndexOutOfRangeException();

                return index switch
                {
                    0 => Scalar,
                    3 => -ScalarK,
                    5 => ScalarK,
                    6 => -ScalarI,
                    _ => Float64Scalar.Zero
                };
            }
        }


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public Float64Quaternion(double iScalar, double jScalar, double kScalar, double scalar)
        //{
        //    ScalarI = iScalar;
        //    ScalarJ = jScalar;
        //    ScalarK = kScalar;
        //    Scalar = scalar;
        //}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Float64Quaternion(Float64Scalar iScalar, Float64Scalar jScalar, Float64Scalar kScalar, Float64Scalar scalar)
        {
            ScalarI = iScalar;
            ScalarJ = jScalar;
            ScalarK = kScalar;
            Scalar = scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Float64Quaternion(Float64Bivector3D bivectorPart, Float64Scalar scalar)
        {
            ScalarI = -bivectorPart.Scalar23;
            ScalarJ = bivectorPart.Scalar13;
            ScalarK = -bivectorPart.Scalar12;
            Scalar = scalar;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return ScalarI.IsValid() &&
                   ScalarJ.IsValid() &&
                   ScalarK.IsValid() &&
                   Scalar.IsValid();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            return Scalar.IsZero() &&
                   ScalarI.IsZero() &&
                   ScalarJ.IsZero() &&
                   ScalarK.IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(double epsilon = 1e-12)
        {
            return Scalar.IsNearZero(epsilon) &&
                   ScalarI.IsNearZero(epsilon) &&
                   ScalarJ.IsNearZero(epsilon) &&
                   ScalarK.IsNearZero(epsilon);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNormalized()
        {
            return NormSquared().IsOne();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearNormalized(double epsilon = 1e-12)
        {
            return NormSquared().IsNearOne(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsIdentity()
        {
            return ScalarI.IsZero() &&
                   ScalarJ.IsZero() &&
                   ScalarK.IsZero() &&
                   Scalar.IsOne();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearIdentity(double epsilon = 1e-12)
        {
            return (this - Identity).NormSquared().IsNearZero(epsilon);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetNormalVector()
        {
            return Float64Vector3D.Create(
                -ScalarI, 
                -ScalarJ, 
                -ScalarK
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar Norm()
        {
            return NormSquared().Sqrt();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar NormInverse()
        {
            return NormSquared().Sqrt().Inverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar NormSquared()
        {
            return ScalarI.Square() + 
                   ScalarJ.Square() + 
                   ScalarK.Square() + 
                   Scalar.Square();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar NormSquaredInverse()
        {
            return NormSquared().Inverse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Bivector3D GetBivector()
        {
            return Float64Bivector3D.Create(Scalar12, Scalar13, Scalar23);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple<Float64Scalar3D, Float64Bivector3D> GetScalarAndBivector()
        {
            return new Tuple<Float64Scalar3D, Float64Bivector3D>(
                Float64Scalar3D.Create(Scalar),
                Float64Bivector3D.Create(Scalar12, Scalar13, Scalar23)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Multivector3D ToMultivector3D()
        {
            return Float64Multivector3D.Create(
                Float64Scalar3D.Create(Scalar),
                Float64Vector3D.Zero, 
                Float64Bivector3D.Create(Scalar12, Scalar13, Scalar23),
                Float64Trivector3D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar ESp(Float64Quaternion q2)
        {
            return ScalarI * q2.ScalarI + 
                   ScalarJ * q2.ScalarJ + 
                   ScalarK * q2.ScalarK + 
                   Scalar * q2.Scalar;
        }

        
        /// <summary>
        /// Quaternion product
        /// </summary>
        /// <param name="q2"></param>
        /// <returns></returns>
        public Float64Quaternion Times(Float64Quaternion q2)
        {
            // cross(av, bv)
            var cx = ScalarJ * q2.ScalarK - ScalarK * q2.ScalarJ;
            var cy = ScalarK * q2.ScalarI - ScalarI * q2.ScalarK;
            var cz = ScalarI * q2.ScalarJ - ScalarJ * q2.ScalarI;

            var dot = 
                ScalarI * q2.ScalarI + 
                ScalarJ * q2.ScalarJ + 
                ScalarK * q2.ScalarK;

            return new Float64Quaternion(
                ScalarI * q2.Scalar + q2.ScalarI * Scalar + cx,
                ScalarJ * q2.Scalar + q2.ScalarJ * Scalar + cy,
                ScalarK * q2.Scalar + q2.ScalarK * Scalar + cz,
                Scalar * q2.Scalar - dot
            );
        }

        /// <summary>
        /// Quaternion division
        /// </summary>
        /// <param name="q2"></param>
        /// <returns></returns>
        public Float64Quaternion Divide(Float64Quaternion q2)
        {
            //-------------------------------------
            // Inverse part.
            var invNorm = q2.NormInverse();

            var q2X = -q2.ScalarI * invNorm;
            var q2Y = -q2.ScalarJ * invNorm;
            var q2Z = -q2.ScalarK * invNorm;
            var q2W = q2.Scalar * invNorm;

            //-------------------------------------
            // Multiply part.

            // cross(av, bv)
            var cx = ScalarJ * q2Z - ScalarK * q2Y;
            var cy = ScalarK * q2X - ScalarI * q2Z;
            var cz = ScalarI * q2Y - ScalarJ * q2X;

            var dot = ScalarI * q2X + ScalarJ * q2Y + ScalarK * q2Z;

            return new Float64Quaternion(
                ScalarI * q2W + q2X * Scalar + cx,
                ScalarJ * q2W + q2Y * Scalar + cy,
                ScalarK * q2W + q2Z * Scalar + cz,
                Scalar * q2W - dot
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Quaternion Conjugate()
        {
            return new Float64Quaternion(
                -ScalarI,
                -ScalarJ,
                -ScalarK,
                Scalar
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Quaternion Reverse()
        {
            return new Float64Quaternion(
                -ScalarI,
                -ScalarJ,
                -ScalarK,
                Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Quaternion Inverse()
        {
            //  -1   (       a              -v       )
            // q   = ( -------------   ------------- )
            //       (  a^2 + |v|^2  ,  a^2 + |v|^2  )
            var invNormSquared = NormSquaredInverse();

            return new Float64Quaternion(
                -ScalarI * invNormSquared,
                -ScalarJ * invNormSquared,
                -ScalarK * invNormSquared,
                Scalar * invNormSquared
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Quaternion Normalize()
        {
            var invNorm = NormInverse();

            return new Float64Quaternion(
                ScalarI * invNorm,
                ScalarJ * invNorm,
                ScalarK * invNorm,
                Scalar * invNorm
            );
        }

        public Float64Quaternion Lerp(Float64Quaternion q2, double t)
        {
            var t1 = 1d - t;

            var dot =
                ScalarI * q2.ScalarI +
                ScalarJ * q2.ScalarJ +
                ScalarK * q2.ScalarK +
                Scalar * q2.Scalar;

            var r =
                dot >= 0d
                    ? new Float64Quaternion(
                        t1 * ScalarI + t * q2.ScalarI,
                        t1 * ScalarJ + t * q2.ScalarJ,
                        t1 * ScalarK + t * q2.ScalarK,
                        t1 * Scalar + t * q2.Scalar
                    ) :
                    new Float64Quaternion(
                        t1 * ScalarI - t * q2.ScalarI,
                        t1 * ScalarJ - t * q2.ScalarJ,
                        t1 * ScalarK - t * q2.ScalarK,
                        t1 * Scalar - t * q2.Scalar
                    );

            // Normalize it.
            return r.Normalize();
        }

        public Float64Quaternion Slerp(Float64Quaternion q2, double t, double slerpEpsilon = 1e-6)
        {
            var cosOmega =
                ScalarI * q2.ScalarI +
                ScalarJ * q2.ScalarJ +
                ScalarK * q2.ScalarK +
                Scalar * q2.Scalar;

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

            return new Float64Quaternion(
                s1 * ScalarI + s2 * q2.ScalarI,
                s1 * ScalarJ + s2 * q2.ScalarJ,
                s1 * ScalarK + s2 * q2.ScalarK,
                s1 * Scalar + s2 * q2.Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Quaternion Concatenate(Float64Quaternion q2)
        {
            // Concatenate rotation is actually q2 * q1 instead of q1 * q2.
            // So that's why q2 goes q1 and value1 goes q2.
            
            // cross(av, bv)
            var cx = q2.ScalarJ * ScalarK - q2.ScalarK * ScalarJ;
            var cy = q2.ScalarK * ScalarI - q2.ScalarI * ScalarK;
            var cz = q2.ScalarI * ScalarJ - q2.ScalarJ * ScalarI;

            var dot = 
                q2.ScalarI * ScalarI + 
                q2.ScalarJ * ScalarJ + 
                q2.ScalarK * ScalarK;

            return new Float64Quaternion(
                q2.ScalarI * Scalar + ScalarI * q2.Scalar + cx,
                q2.ScalarJ * Scalar + ScalarJ * q2.Scalar + cy,
                q2.ScalarK * Scalar + ScalarK * q2.Scalar + cz,
                q2.Scalar * Scalar - dot
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Quaternion Concatenate(Float64Quaternion q2, Float64Quaternion quaternion3)
        {
            return Concatenate(q2).Concatenate(quaternion3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Quaternion Concatenate(params Float64Quaternion[] quaternionList)
        {
            return quaternionList.Aggregate(
                this,
                Concatenate
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<double> GetHalfAngleCosSin(bool assumeNormalized = false)
        {
            var scalar = assumeNormalized
                ? Scalar
                : Scalar / Norm();

            var cosHalfAngle = scalar.Value;
            var sinHalfAngle = (1d - cosHalfAngle.Square()).Sqrt();

            return new Pair<double>(cosHalfAngle, sinHalfAngle);
        }

        public Tuple<Float64PlanarAngle, Float64Bivector3D> GetAngleAndBivector(bool assumeNormalized = false)
        {
            var quaternion = assumeNormalized
                ? this
                : Normalize();
            
            var (scalar, bivector) = 
                quaternion.GetScalarAndBivector();

            var cosHalfAngle = scalar.Scalar;
            var sinHalfAngle = (1 - cosHalfAngle.Square()).Sqrt();

            var halfAngle = 
                Math.Atan2(sinHalfAngle.Value, cosHalfAngle.Value)
                    .RadiansToAngle(Float64PlanarAngleRange.Positive);

            var angle = 
                (2 * halfAngle).GetAngleInPositiveRange();
            
            return new Tuple<Float64PlanarAngle, Float64Bivector3D>(
                angle,
                bivector / sinHalfAngle
            );
        }

        public Tuple<Float64PlanarAngle, Float64Vector3D> GetAngleAndNormal(bool assumeNormalized = false)
        {
            var quaternion = assumeNormalized
                ? this
                : Normalize();
            
            var (scalar, bivector) = 
                quaternion.GetScalarAndBivector();

            var cosHalfAngle = scalar.Scalar;
            var sinHalfAngle = (1 - cosHalfAngle.Square()).Sqrt();

            var halfAngle = 
                Math.Atan2(sinHalfAngle.Value, cosHalfAngle.Value)
                    .RadiansToAngle()
                    .GetAngleInPositiveRange();

            var angle = 
                (2 * halfAngle).GetAngleInPositiveRange();

            var normal = 
                bivector.Dual3D().ToUnitVector();

            return new Tuple<Float64PlanarAngle, Float64Vector3D>(angle, normal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D ToRotationVector(bool assumeNormalized = false)
        {
            var (angle, normal) = GetAngleAndNormal(assumeNormalized);

            var length = 
                angle.Radians.Value / (2d * Math.PI);

            return normal.SetLength(length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix3 ToSquareMatrix3()
        {
            var normSquared = NormSquared();

            if (normSquared.IsNearZero())
                throw new InvalidOperationException();

            var s = 2d / normSquared;
        
            return new SquareMatrix3
            {
                Scalar00 = 1d - s * (ScalarJ * ScalarJ + ScalarK * ScalarK),
                Scalar10 = s * (ScalarI * ScalarJ - Scalar * ScalarK),
                Scalar20 = s * (ScalarI * ScalarK + Scalar * ScalarJ),

                Scalar01 = s * (ScalarI * ScalarJ + Scalar * ScalarK),
                Scalar11 = 1d - s * (ScalarI * ScalarI + ScalarK * ScalarK),
                Scalar21 = s * (ScalarJ * ScalarK - Scalar * ScalarI),

                Scalar02 = s * (ScalarI * ScalarK - Scalar * ScalarJ),
                Scalar12 = s * (ScalarJ * ScalarK + Scalar * ScalarI),
                Scalar22 = 1d - s * (ScalarI * ScalarI + ScalarJ * ScalarJ)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triplet<Float64Vector3D> RotateBasisVectors()
        {
            var normSquared = NormSquared();

            if (normSquared.IsNearZero())
                throw new InvalidOperationException();

            var s = 2d / normSquared;

            var v1 = Float64Vector3D.Create(
                1d - s * (ScalarJ * ScalarJ + ScalarK * ScalarK),
                s * (ScalarI * ScalarJ - Scalar * ScalarK),
                s * (ScalarI * ScalarK + Scalar * ScalarJ)
            );

            var v2 = Float64Vector3D.Create(
                s * (ScalarI * ScalarJ + Scalar * ScalarK),
                1d - s * (ScalarI * ScalarI + ScalarK * ScalarK),
                s * (ScalarJ * ScalarK - Scalar * ScalarI)
            );

            var v3 = Float64Vector3D.Create(
                s * (ScalarI * ScalarK - Scalar * ScalarJ),
                s * (ScalarJ * ScalarK + Scalar * ScalarI),
                1d - s * (ScalarI * ScalarI + ScalarJ * ScalarJ)
            );

            return new Triplet<Float64Vector3D>(v1, v2, v3);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D RotateVector(double x, double y, double z)
        {
            return RotateVector(
                Float64Vector3D.Create(x, y, z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D RotateVector(LinUnitBasisVector3D axis)
        {
            return ToSquareMatrix3() * axis.ToVector3D();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D RotateVector(IFloat64Vector3D vector)
        {
            return ToSquareMatrix3() * vector;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<Float64Vector3D> RotateVectors(LinUnitBasisVector3D axis1, LinUnitBasisVector3D axis2)
        {
            var rotationMatrix = 
                ToSquareMatrix3();

            return new Pair<Float64Vector3D>(
                rotationMatrix * axis1.ToVector3D(),
                rotationMatrix * axis2.ToVector3D()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<Float64Vector3D> RotateVectors(IFloat64Vector3D vector1, IFloat64Vector3D vector2)
        {
            var rotationMatrix = 
                ToSquareMatrix3();

            return new Pair<Float64Vector3D>(
                rotationMatrix * vector1,
                rotationMatrix * vector2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triplet<Float64Vector3D> RotateVectors(IFloat64Vector3D vector1, IFloat64Vector3D vector2, IFloat64Vector3D vector3)
        {
            var rotationMatrix = 
                ToSquareMatrix3();

            return new Triplet<Float64Vector3D>(
                rotationMatrix * vector1,
                rotationMatrix * vector2,
                rotationMatrix * vector3
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<Float64Vector3D> RotateVectors(params IFloat64Vector3D[] vectorArray)
        {
            var rotationMatrix = 
                ToSquareMatrix3();

            return vectorArray
                .Select(vector => rotationMatrix * vector)
                .ToImmutableArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Float64Vector3D> RotateVectors(IEnumerable<IFloat64Vector3D> vectorList)
        {
            var rotationMatrix = 
                ToSquareMatrix3();

            return vectorList.Select(vector => rotationMatrix * vector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion ToSystemNumericsQuaternion()
        {
            return new Quaternion(
                (float)ScalarI,
                (float)ScalarJ,
                (float)ScalarK,
                (float)Scalar
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<Float64Scalar> GetEnumerator()
        {
            yield return Scalar;
            yield return Scalar1;
            yield return Scalar2;
            yield return Scalar12;
            yield return Scalar3;
            yield return Scalar13;
            yield return Scalar23;
            yield return Scalar123;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"'{Scalar:G}'<> + '{ScalarK:G}'<1,2> + '{ScalarI:G}'<2,3> + '{ScalarJ:G}'<3,1>";
        }
    }
}
