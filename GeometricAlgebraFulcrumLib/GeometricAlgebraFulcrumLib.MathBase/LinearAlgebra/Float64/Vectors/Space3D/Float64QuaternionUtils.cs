using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D
{
    public static class Float64QuaternionUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion ToQuaternion(this Quaternion quaternion)
        {
            return Float64Quaternion.Create(quaternion.X,
                quaternion.Y,
                quaternion.Z,
                quaternion.W);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion AngleAxisToQuaternion(this Float64PlanarAngle angle, IFloat64Tuple3D axis)
        {
            return Float64Quaternion.CreateFromAxisAngle(
                axis.ToUnitVector(),
                angle
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion AxisAngleToQuaternion(this IFloat64Tuple3D axis, Float64PlanarAngle angle)
        {
            return Float64Quaternion.CreateFromAxisAngle(
                axis.ToUnitVector(),
                angle
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion RotationVectorToQuaternion(this IFloat64Tuple3D vector)
        {
            var (length, unitVector) = 
                vector.ToLengthAndUnitDirection();

            return Float64Quaternion.CreateFromAxisAngle(
                unitVector,
                length * 2d * Math.PI
            );
        }
        
        /// <summary>
        /// Creates a quaternion from the specified rotation matrix.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <returns>The newly created quaternion.</returns>
        public static Float64Quaternion RotationMatrixToQuaternion(this Matrix4x4 matrix)
        {
            var trace = matrix.M11 + matrix.M22 + matrix.M33;

            if (trace > 0.0f)
            {
                var s = Math.Sqrt(trace + 1d);
                var invS = 0.5d / s;

                return Float64Quaternion.Create((matrix.M23 - matrix.M32) * invS,
                    (matrix.M31 - matrix.M13) * invS,
                    (matrix.M12 - matrix.M21) * invS,
                    s * 0.5f);
            }

            if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
            {
                var s = Math.Sqrt(1d + matrix.M11 - matrix.M22 - matrix.M33);
                var invS = 0.5d / s;

                return Float64Quaternion.Create(0.5d * s,
                    (matrix.M12 + matrix.M21) * invS,
                    (matrix.M13 + matrix.M31) * invS,
                    (matrix.M23 - matrix.M32) * invS);
            }

            if (matrix.M22 > matrix.M33)
            {
                var s = Math.Sqrt(1d + matrix.M22 - matrix.M11 - matrix.M33);
                var invS = 0.5d / s;

                return Float64Quaternion.Create((matrix.M21 + matrix.M12) * invS,
                    0.5d * s,
                    (matrix.M32 + matrix.M23) * invS,
                    (matrix.M31 - matrix.M13) * invS);
            }
            else
            {
                var s = Math.Sqrt(1d + matrix.M33 - matrix.M11 - matrix.M22);
                var invS = 0.5d / s;

                return Float64Quaternion.Create((matrix.M31 + matrix.M13) * invS,
                    (matrix.M32 + matrix.M23) * invS,
                    0.5d * s,
                    (matrix.M12 - matrix.M21) * invS);
            }
        }

        /// <summary>
        /// Creates a new quaternion from the given yaw, pitch, and roll.
        /// </summary>
        /// <param name="yaw">The yaw angle, in radians, around the Y axis.</param>
        /// <param name="pitch">The pitch angle, in radians, around the X axis.</param>
        /// <param name="roll">The roll angle, in radians, around the Z axis.</param>
        /// <returns>The resulting quaternion.</returns>
        public static Float64Quaternion YawPitchRollToQuaternion(this double yaw, double pitch, double roll)
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

            return Float64Quaternion.Create(cy * sp * cr + sy * cp * sr,
                sy * cp * cr - cy * sp * sr,
                cy * cp * sr - sy * sp * cr,
                cy * cp * cr + sy * sp * sr);
        }


        /// <summary>
        /// Compute a unit vector and angle which define a rotation taking
        /// srcUnitVector into dstUnitVector
        /// </summary>
        /// <param name="srcUnitVector"></param>
        /// <param name="dstUnitVector"></param>
        /// <returns></returns>
        public static Tuple<Float64Vector3D, Float64PlanarAngle> GetRotationAxisAngle(IFloat64Tuple3D srcUnitVector, IFloat64Tuple3D dstUnitVector)
        {
            if (srcUnitVector.IsEqual(dstUnitVector))
                return new Tuple<Float64Vector3D, Float64PlanarAngle>(srcUnitVector.ToVector3D(), 0);

            if (srcUnitVector.IsNegativeEqual(dstUnitVector))
                return new Tuple<Float64Vector3D, Float64PlanarAngle>(
                    srcUnitVector.GetUnitNormal(),
                    Float64PlanarAngle.Angle180
                );

            var angle =
                srcUnitVector.GetAngle(dstUnitVector);

            var axis =
                srcUnitVector.VectorCross(dstUnitVector);

            Debug.Assert(axis != Float64Vector3D.Zero);

            return new Tuple<Float64Vector3D, Float64PlanarAngle>(
                axis.ToUnitVector(),
                angle
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D RotateUsingQuaternion(this IFloat64Tuple3D vector, Float64Quaternion quaternion)
        {
            return quaternion.RotateVector(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D RotateUsingAxisAngle(this IFloat64Tuple3D vector, IFloat64Tuple3D rotationVector, Float64PlanarAngle rotationAngle)
        {
            return rotationVector
                .AxisAngleToQuaternion(rotationAngle)
                .RotateVector(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D RotateUsingVector(this IFloat64Tuple3D vector, IFloat64Tuple3D rotationVector)
        {
            return rotationVector
                .RotationVectorToQuaternion()
                .RotateVector(vector);
        }

        
        public static Float64Quaternion CreateAxisToAxisRotationQuaternion(this LinUnitBasisVector3D axis1, LinUnitBasisVector3D axis2)
        {
            var sqrt2Inv = 1d / Math.Sqrt(2d);

            return axis1 switch
            {
                LinUnitBasisVector3D.PositiveX => axis2 switch
                {
                    LinUnitBasisVector3D.PositiveX => Float64Quaternion.Identity,
                    LinUnitBasisVector3D.PositiveY => Float64Quaternion.Create(0, 0, sqrt2Inv, sqrt2Inv),
                    LinUnitBasisVector3D.PositiveZ => Float64Quaternion.Create(0, -sqrt2Inv, 0, sqrt2Inv),
                    LinUnitBasisVector3D.NegativeX => Float64Quaternion.Create(0, 0, 1, 0),
                    LinUnitBasisVector3D.NegativeY => Float64Quaternion.Create(0, 0, -sqrt2Inv, sqrt2Inv),
                    _ => Float64Quaternion.Create(0, sqrt2Inv, 0, sqrt2Inv),
                },

                LinUnitBasisVector3D.PositiveY => axis2 switch
                {
                    LinUnitBasisVector3D.PositiveX => Float64Quaternion.Create(0, 0, -sqrt2Inv, sqrt2Inv),
                    LinUnitBasisVector3D.PositiveY => Float64Quaternion.Identity,
                    LinUnitBasisVector3D.PositiveZ => Float64Quaternion.Create(sqrt2Inv, 0, 0, sqrt2Inv),
                    LinUnitBasisVector3D.NegativeX => Float64Quaternion.Create(0, 0, sqrt2Inv, sqrt2Inv),
                    LinUnitBasisVector3D.NegativeY => Float64Quaternion.Create(1, 0, 0, 0),
                    _ => Float64Quaternion.Create(-sqrt2Inv, 0, 0, sqrt2Inv),
                },

                LinUnitBasisVector3D.PositiveZ => axis2 switch
                {
                    LinUnitBasisVector3D.PositiveX => Float64Quaternion.Create(0, sqrt2Inv, 0, sqrt2Inv),
                    LinUnitBasisVector3D.PositiveY => Float64Quaternion.Create(-sqrt2Inv, 0, 0, sqrt2Inv),
                    LinUnitBasisVector3D.PositiveZ => Float64Quaternion.Identity,
                    LinUnitBasisVector3D.NegativeX => Float64Quaternion.Create(0, -sqrt2Inv, 0, sqrt2Inv),
                    LinUnitBasisVector3D.NegativeY => Float64Quaternion.Create(sqrt2Inv, 0, 0, sqrt2Inv),
                    _ => Float64Quaternion.Create(0, 1, 0, 0),
                },

                LinUnitBasisVector3D.NegativeX => axis2 switch
                {
                    LinUnitBasisVector3D.PositiveX => Float64Quaternion.Create(0, 0, 1, 0),
                    LinUnitBasisVector3D.PositiveY => Float64Quaternion.Create(0, 0, -sqrt2Inv, sqrt2Inv),
                    LinUnitBasisVector3D.PositiveZ => Float64Quaternion.Create(0, sqrt2Inv, 0, sqrt2Inv),
                    LinUnitBasisVector3D.NegativeX => Float64Quaternion.Identity,
                    LinUnitBasisVector3D.NegativeY => Float64Quaternion.Create(0, 0, sqrt2Inv, sqrt2Inv),
                    _ => Float64Quaternion.Create(0, -sqrt2Inv, 0, sqrt2Inv),
                },

                LinUnitBasisVector3D.NegativeY => axis2 switch
                {
                    LinUnitBasisVector3D.PositiveX => Float64Quaternion.Create(0, 0, sqrt2Inv, sqrt2Inv),
                    LinUnitBasisVector3D.PositiveY => Float64Quaternion.Create(1, 0, 0, 0),
                    LinUnitBasisVector3D.PositiveZ => Float64Quaternion.Create(-sqrt2Inv, 0, 0, sqrt2Inv),
                    LinUnitBasisVector3D.NegativeX => Float64Quaternion.Create(0, 0, -sqrt2Inv, sqrt2Inv),
                    LinUnitBasisVector3D.NegativeY => Float64Quaternion.Identity,
                    _ => Float64Quaternion.Create(sqrt2Inv, 0, 0, sqrt2Inv),
                },

                _ => axis2 switch
                {
                    LinUnitBasisVector3D.PositiveX => Float64Quaternion.Create(0, -sqrt2Inv, 0, sqrt2Inv),
                    LinUnitBasisVector3D.PositiveY => Float64Quaternion.Create(sqrt2Inv, 0, 0, sqrt2Inv),
                    LinUnitBasisVector3D.PositiveZ => Float64Quaternion.Create(0, 1, 0, 0),
                    LinUnitBasisVector3D.NegativeX => Float64Quaternion.Create(0, sqrt2Inv, 0, sqrt2Inv),
                    LinUnitBasisVector3D.NegativeY => Float64Quaternion.Create(-sqrt2Inv, 0, 0, sqrt2Inv),
                    _ => Float64Quaternion.Identity,
                },
            };
        }

        public static Float64Vector3D CreateAxisToVectorRotationVector(this LinUnitBasisVector3D axis, IFloat64Tuple3D unitVector)
        {
            var (u, a) = 
                axis.CreateAxisToVectorRotationAxisAngle(unitVector);

            return u.Times(a.Radians / (2d * Math.PI));
        }
        
        public static Float64Vector3D CreateVectorToVectorRotationVector(this IFloat64Tuple3D unitVector1, IFloat64Tuple3D unitVector2)
        {
            var (u, a) = 
                unitVector1.CreateVectorToVectorRotationAxisAngle(unitVector2);

            return u.Times(a.Radians / (2d * Math.PI));
        }

        public static Tuple<IFloat64Tuple3D, Float64PlanarAngle> CreateAxisToVectorRotationAxisAngle(this LinUnitBasisVector3D axis, IFloat64Tuple3D unitVector)
        {
            //Debug.Assert(
            //    (unitVector.GetLengthSquared() - 1).IsNearZero()
            //);

            var dot12 = axis.ESp(unitVector);

            // The case where the two vectors are almost the same
            if ((dot12 - 1d).IsNearZero())
                return new Tuple<IFloat64Tuple3D, Float64PlanarAngle>(
                    unitVector,
                    Float64PlanarAngle.Angle0
                );

            // The case where the two vectors are almost opposite
            if ((dot12 + 1d).IsNearZero())
                return new Tuple<IFloat64Tuple3D, Float64PlanarAngle>(
                    axis.GetUnitNormal().ToVector3D(),
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
                (unitVector1.ENormSquared() - 1).IsNearZero() &&
                (unitVector2.ENormSquared() - 1).IsNearZero()
            );

            var dot12 = unitVector1.ESp(unitVector2);

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
                (unitVector1.ENormSquared() - 1).IsNearZero(1e-7) &&
                (unitVector2.ENormSquared() - 1).IsNearZero(1e-7) &&
                (unitNormal.ENormSquared() - 1).IsNearZero(1e-7) &&
                unitNormal.ESp(unitVector1).IsNearZero(1e-5) && 
                unitNormal.ESp(unitVector2).IsNearZero(1e-5)
            );

            var dot12 = unitVector1.ESp(unitVector2);

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

            var angle = unitVector1.VectorUnitCross(unitVector2).ESp(unitNormal) > 0
                ? Math.Acos(dot12) 
                : -Math.Acos(dot12);

            // The general case
            return new Tuple<IFloat64Tuple3D, Float64PlanarAngle>(
                unitNormal,
                angle
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion CreateVectorToVectorRotationQuaternion(this IFloat64Tuple3D unitVector1, IFloat64Tuple3D unitVector2)
        {
            var (u, a) =
                unitVector1.CreateVectorToVectorRotationAxisAngle(unitVector2);

            return u.AxisAngleToQuaternion(a);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion CreateVectorToVectorRotationQuaternion(this IFloat64Tuple3D unitVector1, IFloat64Tuple3D unitVector2, IFloat64Tuple3D unitNormal)
        {
            var (u, a) =
                unitVector1.CreateVectorToVectorRotationAxisAngle(unitVector2, unitNormal);

            return u.AxisAngleToQuaternion(a);
        }

        public static Tuple<LinUnitBasisVector3D, Float64Quaternion> CreateNearestAxisToVectorRotationQuaternion(this IFloat64Tuple3D unitVector)
        {
            Debug.Assert(
                (unitVector.ENormSquared() - 1).IsNearZero()
            );

            var axis = unitVector.SelectNearestAxis();

            var x = 0d;
            var y = 0d;
            var z = 0d;
            var w = 0d;

            if (axis == LinUnitBasisVector3D.PositiveX)
            {
                var v1 = 1d + unitVector.X;
                var v2 = 1d / Math.Sqrt(2d * v1);

                y = -unitVector.Z * v2;
                z = unitVector.Y * v2;
                w = v1 * v2;
            }

            if (axis == LinUnitBasisVector3D.NegativeX)
            {
                var v1 = 1d - unitVector.X;
                var v2 = 1d / Math.Sqrt(2d * v1);

                y = unitVector.Z * v2;
                z = -unitVector.Y * v2;
                w = v1 * v2;
            }

            if (axis == LinUnitBasisVector3D.PositiveY)
            {
                var v1 = 1d + unitVector.Y;
                var v2 = 1d / Math.Sqrt(2d * v1);

                x = unitVector.Z * v2;
                z = -unitVector.X * v2;
                w = v1 * v2;
            }

            if (axis == LinUnitBasisVector3D.NegativeY)
            {
                var v1 = 1d - unitVector.Y;
                var v2 = 1d / Math.Sqrt(2d * v1);

                x = -unitVector.Z * v2;
                z = unitVector.X * v2;
                w = v1 * v2;
            }

            if (axis == LinUnitBasisVector3D.PositiveZ)
            {
                var v1 = 1d + unitVector.Z;
                var v2 = 1d / Math.Sqrt(2d * v1);

                x = -unitVector.Y * v2;
                y = unitVector.X * v2;
                w = v1 * v2;
            }

            if (axis == LinUnitBasisVector3D.NegativeZ)
            {
                var v1 = 1d - unitVector.Z;
                var v2 = 1d / Math.Sqrt(2d * v1);

                x = unitVector.Y * v2;
                y = -unitVector.X * v2;
                w = v1 * v2;
            }

            var quaternion = Float64Quaternion.Create(x, y, z, w);

            return Tuple.Create(axis, quaternion);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion CreateAxisToVectorRotationQuaternion(this IFloat64Tuple3D unitVector, LinUnitBasisVector3D axis)
        {
            return axis.CreateAxisToVectorRotationQuaternion(unitVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion CreateAxisPairToVectorPairRotationQuaternion(this LinUnitBasisVector3D axis1, LinUnitBasisVector3D axis2, IFloat64Tuple3D unitVector1, IFloat64Tuple3D unitVector2)
        {
            Debug.Assert(
                unitVector1.ENormSquared().IsNearEqual(1) &&
                unitVector2.ENormSquared().IsNearEqual(1)
            );

            var q1 =
                axis1.CreateAxisToVectorRotationQuaternion(unitVector1);

            var axis2Rotated =
                q1.RotateVector(axis2).ToUnitVector();

            var q2 =
                axis2Rotated.CreateVectorToVectorRotationQuaternion(unitVector2, unitVector1);

            var quaternion = 
                q1.Concatenate(q2);

            Debug.Assert(
                (quaternion.RotateVector(axis1) - unitVector1).ENormSquared().IsNearZero()
            );
        
            Debug.Assert(
                (quaternion.RotateVector(axis2) - unitVector2).ENormSquared().IsNearZero()
            );

            return quaternion;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<Float64Quaternion> CreateAxisPairToVectorPairRotationQuaternionPair(this LinUnitBasisVector3D axis1, LinUnitBasisVector3D axis2, IFloat64Tuple3D unitVector1, IFloat64Tuple3D unitVector2)
        {
            Debug.Assert(
                unitVector1.ENormSquared().IsNearEqual(1) &&
                unitVector2.ENormSquared().IsNearEqual(1)
            );

            var q1 =
                axis1.CreateAxisToVectorRotationQuaternion(unitVector1);

            var axis2Rotated =
                q1.RotateVector(axis2).ToUnitVector();

            var q2 =
                axis2Rotated.CreateVectorToVectorRotationQuaternion(unitVector2, unitVector1);

            Debug.Assert(
                (q1.Concatenate(q2).RotateVector(axis1) - unitVector1).ENormSquared().IsNearZero()
            );
        
            Debug.Assert(
                (q1.Concatenate(q2).RotateVector(axis2) - unitVector2).ENormSquared().IsNearZero()
            );

            return new Pair<Float64Quaternion>(q1, q2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Quaternion CreateAxisToVectorRotationQuaternion(this LinUnitBasisVector3D axis, IFloat64Tuple3D unitVector)
        {
            var (u, a) =
                axis.CreateAxisToVectorRotationAxisAngle(unitVector);

            return u.AxisAngleToQuaternion(a);

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
        public static Quaternion ToSystemNumericsQuaternion(this LinUnitBasisVector3D axis)
        {
            return axis switch
            {
                LinUnitBasisVector3D.PositiveX => new Quaternion(1, 0, 0, 0),
                LinUnitBasisVector3D.PositiveY => new Quaternion(0, 1, 0, 0),
                LinUnitBasisVector3D.PositiveZ => new Quaternion(0, 0, 1, 0),
                LinUnitBasisVector3D.NegativeX => new Quaternion(-1, 0, 0, 0),
                LinUnitBasisVector3D.NegativeY => new Quaternion(0, -1, 0, 0),
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
        public static Float64Vector3D GetVectorPart(this Quaternion quaternion)
        {
            return Float64Vector3D.Create(quaternion.X, quaternion.Y, quaternion.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, Float64Vector3D> GetScalarVectorParts(this Quaternion quaternion)
        {
            return new Tuple<double, Float64Vector3D>(
                quaternion.W,
                Float64Vector3D.Create(quaternion.X, quaternion.Y, quaternion.Z)
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
        public static Float64Vector3D RotateVector(this Quaternion quaternion, LinUnitBasisVector3D axis)
        {
            Debug.Assert(
                quaternion.IsNearNormalized()
            );

            var q = quaternion.ToQuaternion();
            
            var v = Float64Quaternion.Create(
                axis.GetX(),
                axis.GetY(),
                axis.GetZ(),
                Float64Scalar.Zero
            );

            return (q * v * q.Inverse()).GetNormalVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D RotateVector(this Quaternion quaternion, double x, double y, double z)
        {
            Debug.Assert(
                quaternion.IsNearNormalized()
            );

            var v = Float64Quaternion.Create(
                x, 
                y, 
                z, 
                Float64Scalar.Zero
            );

            var q = quaternion.ToQuaternion();

            return (q * v * q.Inverse()).GetNormalVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D RotateVector(this Quaternion quaternion, IFloat64Tuple3D vector)
        {
            Debug.Assert(
                quaternion.IsNearNormalized()
            );

            var q = quaternion.ToQuaternion();
            
            var v = Float64Quaternion.Create(
                vector.X,
                vector.Y,
                vector.Z,
                Float64Scalar.Zero
            );

            return (q * v * q.Inverse()).GetNormalVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D RotateVectorUsing(this IFloat64Tuple3D vector, Quaternion quaternion)
        {
            Debug.Assert(
                quaternion.IsNearNormalized()
            );

            var q = quaternion.ToQuaternion();
            
            var v = Float64Quaternion.Create(
                vector.X,
                vector.Y,
                vector.Z,
                Float64Scalar.Zero
            );

            return (q * v * q.Inverse()).GetNormalVector();
        }

            
        /// <summary>
        /// Spherical Linear Interpolation of two normalized quaternions.
        /// See https://en.wikipedia.org/wiki/Slerp for details
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Float64Quaternion Slerp(this double t, Float64Quaternion v1, Float64Quaternion v2)
        {
            //Compute the cosine of the angle between the two vectors.
            var s = 1.0d - t;
            var cosTheta = v1.ESp(v2);

            if (cosTheta > 0.9995d)
            {
                //If the inputs are too close for comfort, linearly interpolate and normalize the result.
                var x = s * v1.ScalarI + t * v2.ScalarI;
                var y = s * v1.ScalarJ + t * v2.ScalarJ;
                var z = s * v1.ScalarK + t * v2.ScalarK;
                var w = s * v1.Scalar + t * v2.Scalar;

                var d = 1.0d / Math.Sqrt(x * x + y * y + z * z + w * w);

                return Float64Quaternion.Create(x * d, y * d, z * d, w * d);
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
            var theta = cosTheta.Clamp(-1.0d, 1.0d).ArcCos();

            //thetaP = angle between value1 and result quaternion, interpreted as vectors
            var thetaP = theta * t;
            var cosThetaP = Math.Cos(thetaP);
            var sinThetaP = Math.Sin(thetaP);

            //Make { value1, qPerp } an orthogonal basis in quaternion vector space
            var qPerpX = v2.ScalarI - v1.ScalarI * cosTheta;
            var qPerpY = v2.ScalarJ - v1.ScalarJ * cosTheta;
            var qPerpZ = v2.ScalarK - v1.ScalarK * cosTheta;
            var qPerpW = v2.Scalar - v1.Scalar * cosTheta;
            var qPerpInvLength = 1.0d / Math.Sqrt(qPerpX * qPerpX + qPerpY * qPerpY + qPerpZ * qPerpZ + qPerpW * qPerpW);

            //Final result
            return Float64Quaternion.Create(v1.ScalarI * cosThetaP + qPerpX * qPerpInvLength * sinThetaP,
                v1.ScalarJ * cosThetaP + qPerpY * qPerpInvLength * sinThetaP,
                v1.ScalarK * cosThetaP + qPerpZ * qPerpInvLength * sinThetaP,
                v1.Scalar * cosThetaP + qPerpW * qPerpInvLength * sinThetaP);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Quaternion> Slerp(this IEnumerable<double> tList, Float64Quaternion v1, Float64Quaternion v2)
        {
            return tList.Select(t => t.Slerp(v1, v2));
        }

    }
}