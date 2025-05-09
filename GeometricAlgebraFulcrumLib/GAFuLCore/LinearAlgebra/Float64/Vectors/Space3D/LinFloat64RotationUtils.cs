using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D
{
    public static class LinFloat64RotationUtils
    {
        /// <summary>
        /// Create a rotation quaternion given an axis and angle of rotation
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Quaternion RotationAngleAxisToQuaternion(this LinFloat64Angle angle, LinBasisVector3D axis)
        {
            return LinFloat64Quaternion.CreateFromAxisAngle(axis, angle);
        }

        /// <summary>
        /// Create a rotation quaternion given an axis and angle of rotation
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Quaternion RotationAngleAxisToQuaternion(this LinFloat64Angle angle, ILinFloat64Vector3D axis)
        {
            return LinFloat64Quaternion.CreateFromAxisAngle(axis, angle);
        }
        
        /// <summary>
        /// Create a rotation quaternion given an axis and angle of rotation
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Quaternion RotationAxisAngleToQuaternion(this LinBasisVector3D axis, LinFloat64Angle angle)
        {
            return LinFloat64Quaternion.CreateFromAxisAngle(axis, angle);
        }

        /// <summary>
        /// Create a rotation quaternion given an axis and angle of rotation
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Quaternion RotationAxisAngleToQuaternion(this ILinFloat64Vector3D axis, LinFloat64Angle angle)
        {
            return LinFloat64Quaternion.CreateFromAxisAngle(axis, angle);
        }


        /// <summary>
        /// Create a rotation quaternion given a vector of rotation.
        /// The length of the vector encodes the angle of rotation as (length * 2 PI).
        /// The normalized vector direction encodes the axis of rotation.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Quaternion RotationVectorToQuaternion(this ILinFloat64Vector3D vector)
        {
            var (length, unitVector) =
                vector.ToLengthAndUnitDirection();

            return LinFloat64Quaternion.CreateFromAxisAngle(
                unitVector,
                (length * (2 * Math.PI)).RadiansToPolarAngle()
            );
        }


        /// <summary>
        /// Create a rotation quaternion given a rotation matrix.
        /// TODO: Validate this
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <returns>The newly created quaternion.</returns>
        public static LinFloat64Quaternion RotationMatrixToQuaternion(this Matrix4x4 matrix)
        {
            var trace = matrix.M11 + matrix.M22 + matrix.M33;

            if (trace > 0.0f)
            {
                var s = Math.Sqrt(trace + 1d);
                var invS = 0.5d / s;

                return LinFloat64Quaternion.Create(
                    s * 0.5f, 
                    (matrix.M23 - matrix.M32) * invS, 
                    (matrix.M31 - matrix.M13) * invS, 
                    (matrix.M12 - matrix.M21) * invS
                );
            }

            if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
            {
                var s = Math.Sqrt(1d + matrix.M11 - matrix.M22 - matrix.M33);
                var invS = 0.5d / s;

                return LinFloat64Quaternion.Create(
                    (matrix.M23 - matrix.M32) * invS, 
                    0.5d * s, 
                    (matrix.M12 + matrix.M21) * invS, 
                    (matrix.M13 + matrix.M31) * invS
                );
            }

            if (matrix.M22 > matrix.M33)
            {
                var s = Math.Sqrt(1d + matrix.M22 - matrix.M11 - matrix.M33);
                var invS = 0.5d / s;

                return LinFloat64Quaternion.Create(
                    (matrix.M31 - matrix.M13) * invS, 
                    (matrix.M21 + matrix.M12) * invS, 
                    0.5d * s, 
                    (matrix.M32 + matrix.M23) * invS
                );
            }
            else
            {
                var s = Math.Sqrt(1d + matrix.M33 - matrix.M11 - matrix.M22);
                var invS = 0.5d / s;

                return LinFloat64Quaternion.Create(
                    (matrix.M12 - matrix.M21) * invS, 
                    (matrix.M31 + matrix.M13) * invS, 
                    (matrix.M32 + matrix.M23) * invS, 
                    0.5d * s
                );
            }
        }


        /// <summary>
        /// Create a rotation quaternion given yaw, pitch, and roll.
        /// https://en.wikipedia.org/wiki/Aircraft_principal_axes
        /// TODO: Validate this
        /// </summary>
        /// <param name="yaw">The yaw angle, in radians, around the Y axis.</param>
        /// <param name="pitch">The pitch angle, in radians, around the X axis.</param>
        /// <param name="roll">The roll angle, in radians, around the Z axis.</param>
        /// <returns>The resulting quaternion.</returns>
        public static LinFloat64Quaternion YawPitchRollToQuaternion(this double yaw, double pitch, double roll)
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

            return LinFloat64Quaternion.Create(
                cy * cp * cr + sy * sp * sr, 
                cy * sp * cr + sy * cp * sr, 
                sy * cp * cr - cy * sp * sr, 
                cy * cp * sr - sy * sp * cr
            );
        }
        

        public static LinFloat64Vector3D VectorToVectorRotationVector(this LinBasisVector3D srcVector, ILinFloat64Vector3D dstVector)
        {
            var (u, a) =
                srcVector.VectorToVectorRotationAxisAngle(dstVector);

            return u.VectorTimes(a.RadiansValue / ((2 * Math.PI)));
        }

        public static LinFloat64Vector3D VectorToVectorRotationVector(this ILinFloat64Vector3D srcVector, ILinFloat64Vector3D dstVector)
        {
            var (u, a) =
                srcVector.VectorToVectorRotationAxisAngle(dstVector);

            return u.VectorTimes(a.RadiansValue / ((2 * Math.PI)));
        }


        public static Tuple<LinFloat64Vector3D, LinFloat64Angle> VectorToVectorRotationAxisAngle(this LinBasisVector3D srcVector, ILinFloat64Vector3D dstVector)
        {
            Debug.Assert(
                dstVector.IsNearUnitVector()
            );

            var angleCos = srcVector.VectorESp(dstVector);

            // The case where the two vectors are almost the same
            if (angleCos.IsNearOne())
                return new Tuple<LinFloat64Vector3D, LinFloat64Angle>(
                    srcVector.GetUnitNormal().ToLinVector3D(),
                    LinFloat64PolarAngle.Angle0
                );

            // The case where the two vectors are almost opposite
            if (angleCos.IsNearNegativeOne())
                return new Tuple<LinFloat64Vector3D, LinFloat64Angle>(
                    srcVector.GetUnitNormal().ToLinVector3D(),
                    LinFloat64PolarAngle.Angle180
                );

            // The general case
            return new Tuple<LinFloat64Vector3D, LinFloat64Angle>(
                srcVector.VectorUnitCross(dstVector),
                angleCos.CosToPolarAngle()
            );
        }

        public static Tuple<LinFloat64Vector3D, LinFloat64Angle> VectorToVectorRotationAxisAngle(this ILinFloat64Vector3D srcVector, ILinFloat64Vector3D dstVector)
        {
            Debug.Assert(
                (srcVector.VectorENormSquared() - 1).IsNearZero() &&
                (dstVector.VectorENormSquared() - 1).IsNearZero()
            );

            var angleCos = srcVector.VectorESp(dstVector);

            // The case where the two vectors are almost identical
            if ((angleCos - 1d).IsNearZero())
                return new Tuple<LinFloat64Vector3D, LinFloat64Angle>(
                    srcVector.GetUnitNormal(),
                    LinFloat64PolarAngle.Angle0
                );

            // The case where the two vectors are almost opposite
            if ((angleCos + 1d).IsNearZero())
                return new Tuple<LinFloat64Vector3D, LinFloat64Angle>(
                    srcVector.GetUnitNormal(),
                    LinFloat64PolarAngle.Angle180
                );

            Debug.Assert(angleCos.Abs() < 1);

            // The general case
            return new Tuple<LinFloat64Vector3D, LinFloat64Angle>(
                srcVector.VectorUnitCross(dstVector),
                angleCos.CosToPolarAngle()
            );
        }
        
        /// <summary>
        /// Create a rotation axis and angle given a vector and its rotated version.
        /// The returned axis is parallel to the given axis of rotation
        /// </summary>
        /// <param name="srcVector"></param>
        /// <param name="dstVector"></param>
        /// <param name="rotationAxis"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static Tuple<LinFloat64Vector3D, LinFloat64Angle> VectorToVectorRotationAxisAngle(this ILinFloat64Vector3D srcVector, LinBasisVector3D dstVector, LinBasisVector3D rotationAxis)
        {
            return srcVector.VectorToVectorRotationAxisAngle(
                dstVector.ToLinVector3D(),
                rotationAxis.ToLinVector3D()
            );
        }

        /// <summary>
        /// Create a rotation axis and angle given a vector and its rotated version.
        /// The returned axis is parallel to the given axis of rotation
        /// </summary>
        /// <param name="srcVector"></param>
        /// <param name="dstVector"></param>
        /// <param name="rotationAxis"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static Tuple<LinFloat64Vector3D, LinFloat64Angle> VectorToVectorRotationAxisAngle(this ILinFloat64Vector3D srcVector, ILinFloat64Vector3D dstVector, ILinFloat64Vector3D rotationAxis)
        {
            if (rotationAxis.IsNearZero())
                return VectorToVectorRotationAxisAngle(srcVector, dstVector);

            var cosAngle1 = srcVector.GetAngleCos(rotationAxis);
            var cosAngle2 = dstVector.GetAngleCos(rotationAxis);

            if (!cosAngle1.IsNearEqual(cosAngle2))
                throw new InvalidOperationException();

            var cosAngle12 = srcVector.GetAngleCos(dstVector);

            if (cosAngle1.IsNearOne() || cosAngle2.IsNearOne() || cosAngle12.IsNearOne())
                return new Tuple<LinFloat64Vector3D, LinFloat64Angle>(
                    rotationAxis.ToUnitLinVector3D(),
                    LinFloat64PolarAngle.Angle0
                );

            var v1 = srcVector.RejectOnVector(rotationAxis).ToUnitLinVector3D();
            var v2 = dstVector.RejectOnVector(rotationAxis).ToUnitLinVector3D();
            
            var (axis, angle) = v1.VectorToVectorRotationAxisAngle(v2);

            if (axis.IsNearOppositeTo(rotationAxis))
                return new Tuple<LinFloat64Vector3D, LinFloat64Angle>(
                    axis.VectorNegative(), 
                    angle.NegativeAngle()
                );

            return new Tuple<LinFloat64Vector3D, LinFloat64Angle>(axis, angle);
        }
        

        /// <summary>
        /// Create a rotation quaternion given a vector and its rotated version
        /// </summary>
        /// <param name="srcVector"></param>
        /// <param name="dstVector"></param>
        /// <returns></returns>
        public static LinFloat64Quaternion VectorToVectorRotationQuaternion(this LinBasisVector3D srcVector, LinBasisVector3D dstVector)
        {
            return srcVector.ToLinVector3D().VectorToVectorRotationQuaternion(dstVector.ToLinVector3D());

            //var sqrt2Inv = 1d / Math.Sqrt(2d);

            //return srcVector switch
            //{
            //    LinBasisVector3D.Px => dstVector switch
            //    {
            //        LinBasisVector3D.Px => LinFloat64Quaternion.Identity,
            //        LinBasisVector3D.Py => LinFloat64Quaternion.Create(sqrt2Inv, 0, 0, sqrt2Inv),
            //        LinBasisVector3D.Pz => LinFloat64Quaternion.Create(sqrt2Inv, 0, -sqrt2Inv, 0),
            //        LinBasisVector3D.Nx => LinFloat64Quaternion.Create(0, 0, 0, 1),
            //        LinBasisVector3D.Ny => LinFloat64Quaternion.Create(sqrt2Inv, 0, 0, -sqrt2Inv),
            //        _ => LinFloat64Quaternion.Create(sqrt2Inv, 0, sqrt2Inv, 0),
            //    },

            //    LinBasisVector3D.Py => dstVector switch
            //    {
            //        LinBasisVector3D.Px => LinFloat64Quaternion.Create(sqrt2Inv, 0, 0, -sqrt2Inv),
            //        LinBasisVector3D.Py => LinFloat64Quaternion.Identity,
            //        LinBasisVector3D.Pz => LinFloat64Quaternion.Create(sqrt2Inv, sqrt2Inv, 0, 0),
            //        LinBasisVector3D.Nx => LinFloat64Quaternion.Create(sqrt2Inv, 0, 0, sqrt2Inv),
            //        LinBasisVector3D.Ny => LinFloat64Quaternion.Create(0, 1, 0, 0),
            //        _ => LinFloat64Quaternion.Create(sqrt2Inv, -sqrt2Inv, 0, 0),
            //    },

            //    LinBasisVector3D.Pz => dstVector switch
            //    {
            //        LinBasisVector3D.Px => LinFloat64Quaternion.Create(sqrt2Inv, 0, sqrt2Inv, 0),
            //        LinBasisVector3D.Py => LinFloat64Quaternion.Create(sqrt2Inv, -sqrt2Inv, 0, 0),
            //        LinBasisVector3D.Pz => LinFloat64Quaternion.Identity,
            //        LinBasisVector3D.Nx => LinFloat64Quaternion.Create(sqrt2Inv, 0, -sqrt2Inv, 0),
            //        LinBasisVector3D.Ny => LinFloat64Quaternion.Create(sqrt2Inv, sqrt2Inv, 0, 0),
            //        _ => LinFloat64Quaternion.Create(0, 0, 1, 0),
            //    },

            //    LinBasisVector3D.Nx => dstVector switch
            //    {
            //        LinBasisVector3D.Px => LinFloat64Quaternion.Create(0, 0, 0, 1),
            //        LinBasisVector3D.Py => LinFloat64Quaternion.Create(sqrt2Inv, 0, 0, -sqrt2Inv),
            //        LinBasisVector3D.Pz => LinFloat64Quaternion.Create(sqrt2Inv, 0, sqrt2Inv, 0),
            //        LinBasisVector3D.Nx => LinFloat64Quaternion.Identity,
            //        LinBasisVector3D.Ny => LinFloat64Quaternion.Create(sqrt2Inv, 0, 0, sqrt2Inv),
            //        _ => LinFloat64Quaternion.Create(sqrt2Inv, 0, -sqrt2Inv, 0),
            //    },

            //    LinBasisVector3D.Ny => dstVector switch
            //    {
            //        LinBasisVector3D.Px => LinFloat64Quaternion.Create(sqrt2Inv, 0, 0, sqrt2Inv),
            //        LinBasisVector3D.Py => LinFloat64Quaternion.Create(0, 1, 0, 0),
            //        LinBasisVector3D.Pz => LinFloat64Quaternion.Create(sqrt2Inv, -sqrt2Inv, 0, 0),
            //        LinBasisVector3D.Nx => LinFloat64Quaternion.Create(sqrt2Inv, 0, 0, -sqrt2Inv),
            //        LinBasisVector3D.Ny => LinFloat64Quaternion.Identity,
            //        _ => LinFloat64Quaternion.Create(sqrt2Inv, sqrt2Inv, 0, 0),
            //    },

            //    _ => dstVector switch
            //    {
            //        LinBasisVector3D.Px => LinFloat64Quaternion.Create(sqrt2Inv, 0, -sqrt2Inv, 0),
            //        LinBasisVector3D.Py => LinFloat64Quaternion.Create(sqrt2Inv, sqrt2Inv, 0, 0),
            //        LinBasisVector3D.Pz => LinFloat64Quaternion.Create(0, 0, 1, 0),
            //        LinBasisVector3D.Nx => LinFloat64Quaternion.Create(sqrt2Inv, 0, sqrt2Inv, 0),
            //        LinBasisVector3D.Ny => LinFloat64Quaternion.Create(sqrt2Inv, -sqrt2Inv, 0, 0),
            //        _ => LinFloat64Quaternion.Identity,
            //    },
            //};
        }
        
        /// <summary>
        /// Create a rotation quaternion given a vector and its rotated version
        /// </summary>
        /// <param name="srcVector"></param>
        /// <param name="dstVector"></param>
        /// <param name="zeroEpsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Quaternion VectorToVectorRotationQuaternion(this LinBasisVector3D srcVector, ILinFloat64Vector3D dstVector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
        {
            var angleCos = srcVector.GetAngleCos(dstVector);

            if (angleCos.IsNearOne(zeroEpsilon))
                return LinFloat64Quaternion.Identity;

            if (angleCos.IsNearMinusOne(zeroEpsilon))
                return LinFloat64Quaternion.CreateFromAxisAngle(
                    srcVector.GetUnitNormal(),
                    LinFloat64PolarAngle.Angle0
                );

            return LinFloat64Quaternion.CreateFromAxisAngle(
                srcVector.VectorUnitCross(dstVector),
                angleCos.CosToPolarAngle()
            );

            //var (u, a) =
            //    axis.CreateAxisToVectorRotationAxisAngle(unitVector);

            //return u.CreateQuaternion(a);

            ////This gives a correct quaternion but not the simplest one (the one with the smallest angle)
            ////var (nearestAxis, q2) =
            ////    unitVector.CreateNearestAxisToVectorRotationQuaternion();

            ////var q1 =
            ////    axis.CreateAxisToAxisRotationQuaternion(nearestAxis);

            ////return Tuple4D.ConcatenateText(q1, q2);
        }

        /// <summary>
        /// https://stackoverflow.com/questions/1171849/finding-quaternion-representing-the-rotation-from-one-vector-to-another
        /// https://math.stackexchange.com/questions/4520571/how-to-get-a-rotation-quaternion-from-two-vectors
        /// https://math.stackexchange.com/questions/180418/calculate-rotation-matrix-to-align-vector-a-to-vector-b-in-3d
        /// </summary>
        /// <param name="srcVector"></param>
        /// <param name="dstVector"></param>
        /// <param name="zeroEpsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Quaternion VectorToVectorRotationQuaternion(this ILinFloat64Vector3D srcVector, ILinFloat64Vector3D dstVector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
        {
            var angleCos = srcVector.GetAngleCos(dstVector);

            if (angleCos.IsNearOne(zeroEpsilon))
                return LinFloat64Quaternion.Identity;

            if (angleCos.IsNearMinusOne(zeroEpsilon))
                return LinFloat64Quaternion.CreateFromAxisAngle(
                    srcVector.GetUnitNormal(),
                    LinFloat64PolarAngle.Angle0
                );

            return LinFloat64Quaternion.CreateFromAxisAngle(
                srcVector.VectorUnitCross(dstVector),
                angleCos.CosToPolarAngle()
            );

            //var (u, a) =
            //    srcVector.CreateVectorToVectorRotationAxisAngle(dstVector);

            //return u.CreateQuaternion(a);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Quaternion VectorToVectorRotationQuaternion(this ILinFloat64Vector3D srcVector, LinBasisVector3D dstVector, LinBasisVector3D rotationAxis)
        {
            var (u, a) =
                srcVector.VectorToVectorRotationAxisAngle(dstVector, rotationAxis);

            return u.RotationAxisAngleToQuaternion(a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Quaternion VectorToVectorRotationQuaternion(this ILinFloat64Vector3D srcVector, ILinFloat64Vector3D dstVector, ILinFloat64Vector3D rotationAxis)
        {
            var (u, a) =
                srcVector.VectorToVectorRotationAxisAngle(dstVector, rotationAxis);

            return u.RotationAxisAngleToQuaternion(a);
        }

        public static Tuple<LinBasisVector3D, LinFloat64Quaternion> NearestBasisToVectorRotationQuaternion(this ILinFloat64Vector3D dstVector)
        {
            Debug.Assert(
                (dstVector.VectorENormSquared() - 1).IsNearZero()
            );

            var axis = dstVector.SelectNearestBasisVector();

            var x = 0d;
            var y = 0d;
            var z = 0d;
            var w = 0d;

            if (axis == LinBasisVector3D.Px)
            {
                var v1 = 1d + dstVector.X;
                var v2 = 1d / Math.Sqrt(2d * v1);

                y = -dstVector.Z * v2;
                z = dstVector.Y * v2;
                w = v1 * v2;
            }

            if (axis == LinBasisVector3D.Nx)
            {
                var v1 = 1d - dstVector.X;
                var v2 = 1d / Math.Sqrt(2d * v1);

                y = dstVector.Z * v2;
                z = -dstVector.Y * v2;
                w = v1 * v2;
            }

            if (axis == LinBasisVector3D.Py)
            {
                var v1 = 1d + dstVector.Y;
                var v2 = 1d / Math.Sqrt(2d * v1);

                x = dstVector.Z * v2;
                z = -dstVector.X * v2;
                w = v1 * v2;
            }

            if (axis == LinBasisVector3D.Ny)
            {
                var v1 = 1d - dstVector.Y;
                var v2 = 1d / Math.Sqrt(2d * v1);

                x = -dstVector.Z * v2;
                z = dstVector.X * v2;
                w = v1 * v2;
            }

            if (axis == LinBasisVector3D.Pz)
            {
                var v1 = 1d + dstVector.Z;
                var v2 = 1d / Math.Sqrt(2d * v1);

                x = -dstVector.Y * v2;
                y = dstVector.X * v2;
                w = v1 * v2;
            }

            if (axis == LinBasisVector3D.Nz)
            {
                var v1 = 1d - dstVector.Z;
                var v2 = 1d / Math.Sqrt(2d * v1);

                x = dstVector.Y * v2;
                y = -dstVector.X * v2;
                w = v1 * v2;
            }

            var quaternion = LinFloat64Quaternion.Create(w, x, y, z);

            return Tuple.Create(axis, quaternion);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Quaternion VectorToVectorRotationQuaternion(this ILinFloat64Vector3D srcVector, LinBasisVector3D dstVector)
        {
            return dstVector.VectorToVectorRotationQuaternion(srcVector);
        }

        
        public static LinFloat64Quaternion VectorPairToVectorPairRotationQuaternion(this LinBasisVectorPair3D srcVectorPair, LinBasisVectorPair3D dstVectorPair, double zeroEpsilon = Float64Utils.ZeroEpsilon)
        {
            var (srcAxis1, srcAxis2) = srcVectorPair;
            var (dstAxis1, dstAxis2) = dstVectorPair;

            var q1 =
                srcAxis1.VectorToVectorRotationQuaternion(dstAxis1);

            Debug.Assert(
                (q1.RotateVector(srcAxis1) - dstAxis1).VectorENormSquared().IsNearZero(zeroEpsilon)
            );

            var axis2Rotated =
                q1.RotateVector(srcAxis2).ToUnitLinVector3D();

            var q2 =
                axis2Rotated.VectorToVectorRotationQuaternion(dstAxis2, dstAxis1);

            var quaternion =
                q2.Concatenate(q1);

            Debug.Assert(
                (quaternion.RotateVector(srcAxis1) - dstAxis1).VectorENormSquared().IsNearZero(zeroEpsilon)
            );

            Debug.Assert(
                (quaternion.RotateVector(srcAxis2) - dstAxis2).VectorENormSquared().IsNearZero(zeroEpsilon)
            );

            return quaternion;
        }

        public static LinFloat64Quaternion VectorPairToVectorPairRotationQuaternion(this LinBasisVectorPair3D srcVectorPair, ILinFloat64Vector3D dstVector1, ILinFloat64Vector3D dstVector2, double zeroEpsilon = Float64Utils.ZeroEpsilon)
        {
            Debug.Assert(
                dstVector1.IsNearUnitVector(zeroEpsilon) &&
                dstVector2.IsNearUnitVector(zeroEpsilon) &&
                dstVector1.VectorESp(dstVector2).IsNearZero(zeroEpsilon)
            );

            var (axis1, axis2) = srcVectorPair;

            var q1 =
                axis1.VectorToVectorRotationQuaternion(dstVector1);

            Debug.Assert(
                (q1.RotateVector(axis1) - dstVector1).VectorENormSquared().IsNearZero(zeroEpsilon)
            );

            var axis2Rotated =
                q1.RotateVector(axis2).ToUnitLinVector3D();

            var q2 =
                axis2Rotated.VectorToVectorRotationQuaternion(dstVector2, dstVector1);

            var quaternion =
                q2.Concatenate(q1);

            Debug.Assert(
                (quaternion.RotateVector(axis1) - dstVector1).VectorENormSquared().IsNearZero(zeroEpsilon)
            );

            Debug.Assert(
                (quaternion.RotateVector(axis2) - dstVector2).VectorENormSquared().IsNearZero(zeroEpsilon)
            );

            return quaternion;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<LinFloat64Quaternion> VectorPairToVectorPairRotationQuaternionPair(this LinBasisVectorPair3D srcVectorPair, ILinFloat64Vector3D dstVector1, ILinFloat64Vector3D dstVector2)
        {
            Debug.Assert(
                dstVector1.VectorENormSquared().IsNearEqual(1) &&
                dstVector2.VectorENormSquared().IsNearEqual(1)
            );

            var (axis1, axis2) = srcVectorPair;

            var q1 =
                axis1.VectorToVectorRotationQuaternion(dstVector1);

            var axis2Rotated =
                q1.RotateVector(axis2).ToUnitLinVector3D();

            var q2 =
                axis2Rotated.VectorToVectorRotationQuaternion(dstVector2, dstVector1);

            Debug.Assert(
                (q1.Concatenate(q2).RotateVector(axis1) - dstVector1).VectorENormSquared().IsNearZero()
            );

            Debug.Assert(
                (q1.Concatenate(q2).RotateVector(axis2) - dstVector2).VectorENormSquared().IsNearZero()
            );

            return new Pair<LinFloat64Quaternion>(q1, q2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector3D RotateVectorUsingAxisAngle(this ILinFloat64Vector3D vector, ILinFloat64Vector3D axis, LinFloat64Angle angle)
        {
            var quaternion = LinFloat64Quaternion.CreateFromAxisAngle(axis, angle);

            return quaternion.RotateVector(vector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector3D RotateVectorUsingVector(this ILinFloat64Vector3D vector, ILinFloat64Vector3D rotationVector)
        {
            return rotationVector
                .RotationVectorToQuaternion()
                .RotateVector(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector3D RotateVectorUsingQuaternion(this ILinFloat64Vector3D vector, Quaternion quaternion)
        {
            return quaternion.ToQuaternion().RotateVector(vector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector3D RotateVectorUsingQuaternion(this ILinFloat64Vector3D vector, LinFloat64Quaternion quaternion)
        {
            return quaternion.RotateVector(vector);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector3D RotateVector(this Quaternion quaternion, LinBasisVector3D vector)
        {
            return quaternion.ToQuaternion().RotateVector(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector3D RotateVector(this Quaternion quaternion, double x, double y, double z)
        {
            return quaternion.ToQuaternion().RotateVector(
                LinFloat64Vector3D.Create(x, y, z)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64Vector3D RotateVector(this Quaternion quaternion, ILinFloat64Vector3D vector)
        {
            return quaternion.ToQuaternion().RotateVector(vector);
        }


        /// <summary>
        /// Spherical Linear Interpolation of two normalized quaternions.
        /// See https://en.wikipedia.org/wiki/Slerp for details
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static LinFloat64Quaternion Slerp(this double t, LinFloat64Quaternion v1, LinFloat64Quaternion v2)
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

                return LinFloat64Quaternion.Create(w * d, x * d, y * d, z * d);
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
            var thetaP = theta.AngleTimes(t);
            var cosThetaP = thetaP.Cos();
            var sinThetaP = thetaP.Sin();

            //Make { value1, qPerp } an orthogonal basis in quaternion vector space
            var qPerpX = v2.ScalarI - v1.ScalarI * cosTheta;
            var qPerpY = v2.ScalarJ - v1.ScalarJ * cosTheta;
            var qPerpZ = v2.ScalarK - v1.ScalarK * cosTheta;
            var qPerpW = v2.Scalar - v1.Scalar * cosTheta;
            var qPerpInvLength = 1.0d / Math.Sqrt(qPerpX * qPerpX + qPerpY * qPerpY + qPerpZ * qPerpZ + qPerpW * qPerpW);

            //Final result
            return LinFloat64Quaternion.Create(v1.Scalar * cosThetaP + qPerpW * qPerpInvLength * sinThetaP, v1.ScalarI * cosThetaP + qPerpX * qPerpInvLength * sinThetaP, v1.ScalarJ * cosThetaP + qPerpY * qPerpInvLength * sinThetaP, v1.ScalarK * cosThetaP + qPerpZ * qPerpInvLength * sinThetaP);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<LinFloat64Quaternion> Slerp(this IEnumerable<double> tList, LinFloat64Quaternion v1, LinFloat64Quaternion v2)
        {
            return tList.Select(t => t.Slerp(v1, v2));
        }

    }
}
