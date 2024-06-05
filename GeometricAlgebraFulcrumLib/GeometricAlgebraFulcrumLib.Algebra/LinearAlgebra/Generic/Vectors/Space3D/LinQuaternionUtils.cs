﻿using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

public static class LinQuaternionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> ToQuaternion<T>(this Quaternion quaternion, IScalarProcessor<T> scalarProcessor)
    {
        return LinQuaternion<T>.Create(
            scalarProcessor,
            -quaternion.X,
            -quaternion.Y,
            -quaternion.Z,
            quaternion.W
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> CreateQuaternion<T>(this LinPolarAngle<T> angle, ILinVector3D<T> normal)
    {
        return LinQuaternion<T>.CreateFromNormalAndAngle(
            normal.ToUnitVector(),
            angle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> CreateQuaternion<T>(this ILinVector3D<T> normal, LinPolarAngle<T> angle)
    {
        return LinQuaternion<T>.CreateFromNormalAndAngle(
            normal.ToUnitVector(),
            angle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> RotationVectorToQuaternion<T>(this ILinVector3D<T> vector)
    {
        var (length, unitVector) =
            vector.ToLengthAndUnitDirection();

        var scalarProcessor = vector.ScalarProcessor;

        return LinQuaternion<T>.CreateFromNormalAndAngle(
            unitVector,
            (length * scalarProcessor.PiTimes2).RadiansToPolarAngle()
        );
    }

    /// <summary>
    /// Creates a quaternion from the specified rotation matrix.
    /// </summary>
    /// <param name="matrix">The rotation matrix.</param>
    /// <param name="scalarProcessor"></param>
    /// <returns>The newly created quaternion.</returns>
    public static LinQuaternion<T> RotationMatrixToQuaternion<T>(this Matrix4x4 matrix, IScalarProcessor<T> scalarProcessor)
    {
        var trace = matrix.M11 + matrix.M22 + matrix.M33;

        if (trace > 0.0f)
        {
            var s = Math.Sqrt(trace + 1d);
            var invS = 0.5d / s;

            return LinQuaternion<T>.Create(
                scalarProcessor,
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

            return LinQuaternion<T>.Create(
                scalarProcessor,
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

            return LinQuaternion<T>.Create(
                scalarProcessor,
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

            return LinQuaternion<T>.Create(
                scalarProcessor,
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
    public static LinQuaternion<T> YawPitchRollToQuaternion<T>(this Scalar<T> yaw, Scalar<T> pitch, Scalar<T> roll)
    {
        //  Roll first, about axis the object is facing, then
        //  pitch upward, then yaw to face into the new heading
        var halfRoll = roll / 2;
        var sr = halfRoll.Sin();
        var cr = halfRoll.Cos();

        var halfPitch = pitch / 2;
        var sp = halfPitch.Sin();
        var cp = halfPitch.Cos();

        var halfYaw = yaw / 2;
        var sy = halfYaw.Sin();
        var cy = halfYaw.Cos();

        return LinQuaternion<T>.Create(
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
    public static Tuple<LinVector3D<T>, LinPolarAngle<T>> GetRotationNormalAndAngle<T>(ILinVector3D<T> srcUnitVector, ILinVector3D<T> dstUnitVector)
    {
        var scalarProcessor = srcUnitVector.ScalarProcessor;

        if (srcUnitVector.IsEqual(dstUnitVector))
            return new Tuple<LinVector3D<T>, LinPolarAngle<T>>(srcUnitVector.ToVector3D(), LinPolarAngle<T>.Angle0(scalarProcessor));

        if (srcUnitVector.IsNegativeEqual(dstUnitVector))
            return new Tuple<LinVector3D<T>, LinPolarAngle<T>>(
                srcUnitVector.GetUnitNormal(),
                LinPolarAngle<T>.Angle180(scalarProcessor)
            );

        var angle =
            srcUnitVector.GetAngle(dstUnitVector);

        var axis =
            srcUnitVector.VectorCross(dstUnitVector);

        Debug.Assert(!axis.IsZero());

        return new Tuple<LinVector3D<T>, LinPolarAngle<T>>(
            axis.ToUnitVector(),
            angle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> RotateUsing<T>(this ILinVector3D<T> vector, LinQuaternion<T> quaternion)
    {
        return quaternion.RotateVector(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> RotateUsing<T>(this ILinVector3D<T> vector, ILinVector3D<T> normal, LinPolarAngle<T> angle)
    {
        return normal
            .CreateQuaternion(angle)
            .RotateVector(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> RotateUsingVector<T>(this ILinVector3D<T> vector, ILinVector3D<T> rotationVector)
    {
        return rotationVector
            .RotationVectorToQuaternion()
            .RotateVector(vector);
    }


    public static LinQuaternion<T> CreateAxisToAxisRotationQuaternion<T>(this LinUnitBasisVector3D axis1, LinUnitBasisVector3D axis2, IScalarProcessor<T> scalarProcessor)
    {
        var sqrt2Inv = scalarProcessor.One.Divide(scalarProcessor.Sqrt(2));
        var zero = scalarProcessor.Zero;
        var one = scalarProcessor.One;

        return axis1 switch
        {
            LinUnitBasisVector3D.PositiveX => axis2 switch
            {
                LinUnitBasisVector3D.PositiveX => LinQuaternion<T>.Identity(scalarProcessor),
                LinUnitBasisVector3D.PositiveY => LinQuaternion<T>.Create(zero, zero, sqrt2Inv, sqrt2Inv),
                LinUnitBasisVector3D.PositiveZ => LinQuaternion<T>.Create(zero, -sqrt2Inv, zero, sqrt2Inv),
                LinUnitBasisVector3D.NegativeX => LinQuaternion<T>.Create(zero, zero, one, zero),
                LinUnitBasisVector3D.NegativeY => LinQuaternion<T>.Create(zero, zero, -sqrt2Inv, sqrt2Inv),
                _ => LinQuaternion<T>.Create(zero, sqrt2Inv, zero, sqrt2Inv),
            },

            LinUnitBasisVector3D.PositiveY => axis2 switch
            {
                LinUnitBasisVector3D.PositiveX => LinQuaternion<T>.Create(zero, zero, -sqrt2Inv, sqrt2Inv),
                LinUnitBasisVector3D.PositiveY => LinQuaternion<T>.Identity(scalarProcessor),
                LinUnitBasisVector3D.PositiveZ => LinQuaternion<T>.Create(sqrt2Inv, zero, zero, sqrt2Inv),
                LinUnitBasisVector3D.NegativeX => LinQuaternion<T>.Create(zero, zero, sqrt2Inv, sqrt2Inv),
                LinUnitBasisVector3D.NegativeY => LinQuaternion<T>.Create(one, zero, zero, zero),
                _ => LinQuaternion<T>.Create(-sqrt2Inv, zero, zero, sqrt2Inv),
            },

            LinUnitBasisVector3D.PositiveZ => axis2 switch
            {
                LinUnitBasisVector3D.PositiveX => LinQuaternion<T>.Create(zero, sqrt2Inv, zero, sqrt2Inv),
                LinUnitBasisVector3D.PositiveY => LinQuaternion<T>.Create(-sqrt2Inv, zero, zero, sqrt2Inv),
                LinUnitBasisVector3D.PositiveZ => LinQuaternion<T>.Identity(scalarProcessor),
                LinUnitBasisVector3D.NegativeX => LinQuaternion<T>.Create(zero, -sqrt2Inv, zero, sqrt2Inv),
                LinUnitBasisVector3D.NegativeY => LinQuaternion<T>.Create(sqrt2Inv, zero, zero, sqrt2Inv),
                _ => LinQuaternion<T>.Create(zero, one, zero, zero),
            },

            LinUnitBasisVector3D.NegativeX => axis2 switch
            {
                LinUnitBasisVector3D.PositiveX => LinQuaternion<T>.Create(zero, zero, one, zero),
                LinUnitBasisVector3D.PositiveY => LinQuaternion<T>.Create(zero, zero, -sqrt2Inv, sqrt2Inv),
                LinUnitBasisVector3D.PositiveZ => LinQuaternion<T>.Create(zero, sqrt2Inv, zero, sqrt2Inv),
                LinUnitBasisVector3D.NegativeX => LinQuaternion<T>.Identity(scalarProcessor),
                LinUnitBasisVector3D.NegativeY => LinQuaternion<T>.Create(zero, zero, sqrt2Inv, sqrt2Inv),
                _ => LinQuaternion<T>.Create(zero, -sqrt2Inv, zero, sqrt2Inv),
            },

            LinUnitBasisVector3D.NegativeY => axis2 switch
            {
                LinUnitBasisVector3D.PositiveX => LinQuaternion<T>.Create(zero, zero, sqrt2Inv, sqrt2Inv),
                LinUnitBasisVector3D.PositiveY => LinQuaternion<T>.Create(one, zero, zero, zero),
                LinUnitBasisVector3D.PositiveZ => LinQuaternion<T>.Create(-sqrt2Inv, zero, zero, sqrt2Inv),
                LinUnitBasisVector3D.NegativeX => LinQuaternion<T>.Create(zero, zero, -sqrt2Inv, sqrt2Inv),
                LinUnitBasisVector3D.NegativeY => LinQuaternion<T>.Identity(scalarProcessor),
                _ => LinQuaternion<T>.Create(sqrt2Inv, zero, zero, sqrt2Inv),
            },

            _ => axis2 switch
            {
                LinUnitBasisVector3D.PositiveX => LinQuaternion<T>.Create(zero, -sqrt2Inv, zero, sqrt2Inv),
                LinUnitBasisVector3D.PositiveY => LinQuaternion<T>.Create(sqrt2Inv, zero, zero, sqrt2Inv),
                LinUnitBasisVector3D.PositiveZ => LinQuaternion<T>.Create(zero, one, zero, zero),
                LinUnitBasisVector3D.NegativeX => LinQuaternion<T>.Create(zero, sqrt2Inv, zero, sqrt2Inv),
                LinUnitBasisVector3D.NegativeY => LinQuaternion<T>.Create(-sqrt2Inv, zero, zero, sqrt2Inv),
                _ => LinQuaternion<T>.Identity(scalarProcessor),
            },
        };
    }

    public static LinVector3D<T> CreateAxisToVectorRotationVector<T>(this LinUnitBasisVector3D axis, ILinVector3D<T> unitVector)
    {
        var scalarProcessor = unitVector.ScalarProcessor;

        var (u, a) =
            axis.CreateAxisToVectorRotationAxisAngle(unitVector);

        return u.VectorTimes(a.Radians.Divide(scalarProcessor.PiTimes2Value));
    }

    public static LinVector3D<T> CreateVectorToVectorRotationVector<T>(this ILinVector3D<T> unitVector1, ILinVector3D<T> unitVector2)
    {
        var scalarProcessor = unitVector1.ScalarProcessor;

        var (u, a) =
            unitVector1.CreateVectorToVectorRotationAxisAngle(unitVector2);

        return u.VectorTimes(a.Radians.Divide(scalarProcessor.PiTimes2Value));
    }

    public static Tuple<ILinVector3D<T>, LinPolarAngle<T>> CreateAxisToVectorRotationAxisAngle<T>(this LinUnitBasisVector3D axis, ILinVector3D<T> unitVector)
    {
        var scalarProcessor = unitVector.ScalarProcessor;

        //Debug.Assert(
        //    (unitVector.GetLengthSquared() - 1).IsNearZero()
        //);

        var dot12 = axis.VectorESp(unitVector);

        // The case where the two vectors are almost the same
        if ((dot12 - 1d).IsNearZero())
            return new Tuple<ILinVector3D<T>, LinPolarAngle<T>>(
                unitVector,
                LinPolarAngle<T>.Angle0(scalarProcessor)
            );

        // The case where the two vectors are almost opposite
        if ((dot12 + 1d).IsNearZero())
            return new Tuple<ILinVector3D<T>, LinPolarAngle<T>>(
                axis.GetUnitNormal().ToVector3D(scalarProcessor),
                LinPolarAngle<T>.Angle180(scalarProcessor)
            );

        // The general case
        return new Tuple<ILinVector3D<T>, LinPolarAngle<T>>(
            axis.VectorUnitCross(unitVector),
            scalarProcessor.CreatePolarAngleFromRadians(dot12.ArcCos().ScalarValue)
        );
    }

    public static Tuple<ILinVector3D<T>, LinPolarAngle<T>> CreateVectorToVectorRotationAxisAngle<T>(this ILinVector3D<T> unitVector1, ILinVector3D<T> unitVector2)
    {
        var scalarProcessor = unitVector1.ScalarProcessor;

        Debug.Assert(
            (unitVector1.VectorENormSquared() - 1).IsNearZero() &&
            (unitVector2.VectorENormSquared() - 1).IsNearZero()
        );

        var dot12 = unitVector1.VectorESp(unitVector2);

        // The case where the two vectors are almost identical
        if ((dot12 - 1d).IsNearZero())
            return new Tuple<ILinVector3D<T>, LinPolarAngle<T>>(
                unitVector1,
                LinPolarAngle<T>.Angle0(scalarProcessor)
            );

        // The case where the two vectors are almost opposite
        if ((dot12 + 1d).IsNearZero())
            return new Tuple<ILinVector3D<T>, LinPolarAngle<T>>(
                unitVector1.GetUnitNormal(),
                LinPolarAngle<T>.Angle180(scalarProcessor)
            );

        Debug.Assert(dot12.Abs() < 1);

        // The general case
        return new Tuple<ILinVector3D<T>, LinPolarAngle<T>>(
            unitVector1.VectorUnitCross(unitVector2),
            scalarProcessor.CreatePolarAngleFromRadians(dot12.ArcCos().ScalarValue)
        );
    }

    public static Tuple<ILinVector3D<T>, LinPolarAngle<T>> CreateVectorToVectorRotationAxisAngle<T>(this ILinVector3D<T> unitVector1, ILinVector3D<T> unitVector2, ILinVector3D<T> unitNormal)
    {
        var scalarProcessor = unitVector1.ScalarProcessor;

        //Debug.Assert(
        //    (unitVector1.ENormSquared() - 1).IsNearZero(1e-7) &&
        //    (unitVector2.ENormSquared() - 1).IsNearZero(1e-7) &&
        //    (unitNormal.ENormSquared() - 1).IsNearZero(1e-7) &&
        //    unitNormal.ESp(unitVector1).IsNearZero(1e-5) &&
        //    unitNormal.ESp(unitVector2).IsNearZero(1e-5)
        //);

        var dot12 = unitVector1.VectorESp(unitVector2);

        // The case where the two vectors are almost identical
        if ((dot12 - 1d).IsNearZero())
            return new Tuple<ILinVector3D<T>, LinPolarAngle<T>>(
                unitNormal,
                LinPolarAngle<T>.Angle0(scalarProcessor)
            );

        // The case where the two vectors are almost opposite
        if ((dot12 + 1d).IsNearZero())
            return new Tuple<ILinVector3D<T>, LinPolarAngle<T>>(
                unitNormal,
                LinPolarAngle<T>.Angle180(scalarProcessor)
            );

        Debug.Assert(dot12.Abs() < 1);

        var angle = unitVector1.VectorUnitCross(unitVector2).VectorESp(unitNormal) > 0
            ? dot12.ArcCos()
            : -dot12.ArcCos();

        // The general case
        return new Tuple<ILinVector3D<T>, LinPolarAngle<T>>(
            unitNormal,
            scalarProcessor.CreatePolarAngleFromRadians(angle.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> CreateVectorToVectorRotationQuaternion<T>(this ILinVector3D<T> unitVector1, ILinVector3D<T> unitVector2)
    {
        var (u, a) =
            unitVector1.CreateVectorToVectorRotationAxisAngle(unitVector2);

        return u.CreateQuaternion(a);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> CreateVectorToVectorRotationQuaternion<T>(this ILinVector3D<T> unitVector1, ILinVector3D<T> unitVector2, ILinVector3D<T> unitNormal)
    {
        var (u, a) =
            unitVector1.CreateVectorToVectorRotationAxisAngle(unitVector2, unitNormal);

        return u.CreateQuaternion(a);
    }

    public static Tuple<LinUnitBasisVector3D, LinQuaternion<T>> CreateNearestAxisToVectorRotationQuaternion<T>(this ILinVector3D<T> unitVector)
    {
        //Debug.Assert(
        //    unitVector.ENormSquared().IsNearOne()
        //);
        var scalarProcessor = unitVector.ScalarProcessor;

        var axis = unitVector.SelectNearestAxis();

        var x = scalarProcessor.Zero;
        var y = scalarProcessor.Zero;
        var z = scalarProcessor.Zero;
        var w = scalarProcessor.Zero;

        if (axis == LinUnitBasisVector3D.PositiveX)
        {
            var v1 = 1 + unitVector.X;
            var v2 = 1 / (2 * v1).Sqrt();

            y = -unitVector.Z * v2;
            z = unitVector.Y * v2;
            w = v1 * v2;
        }

        if (axis == LinUnitBasisVector3D.NegativeX)
        {
            var v1 = 1 - unitVector.X;
            var v2 = 1 / (2 * v1).Sqrt();

            y = unitVector.Z * v2;
            z = -unitVector.Y * v2;
            w = v1 * v2;
        }

        if (axis == LinUnitBasisVector3D.PositiveY)
        {
            var v1 = 1 + unitVector.Y;
            var v2 = 1 / (2 * v1).Sqrt();

            x = unitVector.Z * v2;
            z = -unitVector.X * v2;
            w = v1 * v2;
        }

        if (axis == LinUnitBasisVector3D.NegativeY)
        {
            var v1 = 1d - unitVector.Y;
            var v2 = 1 / (2 * v1).Sqrt();

            x = -unitVector.Z * v2;
            z = unitVector.X * v2;
            w = v1 * v2;
        }

        if (axis == LinUnitBasisVector3D.PositiveZ)
        {
            var v1 = 1d + unitVector.Z;
            var v2 = 1 / (2 * v1).Sqrt();

            x = -unitVector.Y * v2;
            y = unitVector.X * v2;
            w = v1 * v2;
        }

        if (axis == LinUnitBasisVector3D.NegativeZ)
        {
            var v1 = 1d - unitVector.Z;
            var v2 = 1 / (2 * v1).Sqrt();

            x = unitVector.Y * v2;
            y = -unitVector.X * v2;
            w = v1 * v2;
        }

        var quaternion = LinQuaternion<T>.Create(x, y, z, w);

        return Tuple.Create(axis, quaternion);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> CreateAxisToVectorRotationQuaternion<T>(this ILinVector3D<T> unitVector, LinUnitBasisVector3D axis)
    {
        return axis.CreateAxisToVectorRotationQuaternion(unitVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> CreateAxisPairToVectorPairRotationQuaternion<T>(this LinUnitBasisVector3D axis1, LinUnitBasisVector3D axis2, ILinVector3D<T> unitVector1, ILinVector3D<T> unitVector2)
    {
        Debug.Assert(
            unitVector1.VectorENormSquared().IsNearOne() &&
            unitVector2.VectorENormSquared().IsNearOne()
        );

        var q1 =
            axis1.CreateAxisToVectorRotationQuaternion(unitVector1);

        Debug.Assert(
            (q1.RotateVector(axis1) - unitVector1).VectorENormSquared().IsNearZero()
        );

        var axis2Rotated =
            q1.RotateVector(axis2).ToUnitVector();

        var q2 =
            axis2Rotated.CreateVectorToVectorRotationQuaternion(unitVector2, unitVector1);

        var quaternion =
            q2.Concatenate(q1);

        Debug.Assert(
            (quaternion.RotateVector(axis1) - unitVector1).VectorENormSquared().IsNearZero()
        );

        Debug.Assert(
            (quaternion.RotateVector(axis2) - unitVector2).VectorENormSquared().IsNearZero()
        );

        return quaternion;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<LinQuaternion<T>> CreateAxisPairToVectorPairRotationQuaternionPair<T>(this LinUnitBasisVector3D axis1, LinUnitBasisVector3D axis2, ILinVector3D<T> unitVector1, ILinVector3D<T> unitVector2)
    {
        Debug.Assert(
            unitVector1.VectorENormSquared().IsNearOne() &&
            unitVector2.VectorENormSquared().IsNearOne()
        );

        var q1 =
            axis1.CreateAxisToVectorRotationQuaternion(unitVector1);

        var axis2Rotated =
            q1.RotateVector(axis2).ToUnitVector();

        var q2 =
            axis2Rotated.CreateVectorToVectorRotationQuaternion(unitVector2, unitVector1);

        Debug.Assert(
            (q1.Concatenate(q2).RotateVector(axis1) - unitVector1).VectorENormSquared().IsNearZero()
        );

        Debug.Assert(
            (q1.Concatenate(q2).RotateVector(axis2) - unitVector2).VectorENormSquared().IsNearZero()
        );

        return new Pair<LinQuaternion<T>>(q1, q2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> CreateAxisToVectorRotationQuaternion<T>(this LinUnitBasisVector3D axis, ILinVector3D<T> unitVector)
    {
        var (u, a) =
            axis.CreateAxisToVectorRotationAxisAngle(unitVector);

        return u.CreateQuaternion(a);

        //This gives a correct quaternion but not the simplest one (the one with the smallest angle)
        //var (nearestAxis, q2) =
        //    unitVector.CreateNearestAxisToVectorRotationQuaternion();

        //var q1 =
        //    axis.CreateAxisToAxisRotationQuaternion(nearestAxis);

        //return Tuple4D.ConcatenateText(q1, q2);
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsNearZero<T>(this Quaternion quaternion, float epsilon = 1e-5f)
    //{
    //    return quaternion.X.IsNearZero(epsilon) &&
    //           quaternion.Y.IsNearZero(epsilon) &&
    //           quaternion.Z.IsNearZero(epsilon);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static bool IsNearNormalized<T>(this Quaternion quaternion, float epsilon = 1e-5f)
    //{
    //    return (quaternion.LengthSquared() - 1f).IsNearZero(epsilon);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Quaternion ToSystemNumericsQuaternion<T>(this ILinVector3D<T> vector)
    //{
    //    return new Quaternion(
    //        (float)vector.X,
    //        (float)vector.Y,
    //        (float)vector.Z,
    //        0f
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Quaternion ToSystemNumericsQuaternion<T>(this LinUnitBasisVector3D axis)
    //{
    //    return axis switch
    //    {
    //        LinUnitBasisVector3D.PositiveX => new Quaternion(1, 0, 0, 0),
    //        LinUnitBasisVector3D.PositiveY => new Quaternion(0, 1, 0, 0),
    //        LinUnitBasisVector3D.PositiveZ => new Quaternion(0, 0, 1, 0),
    //        LinUnitBasisVector3D.NegativeX => new Quaternion(-1, 0, 0, 0),
    //        LinUnitBasisVector3D.NegativeY => new Quaternion(0, -1, 0, 0),
    //        _ => new Quaternion(0, 0, -1, 0),
    //    };
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Quaternion ToSystemNumericsQuaternion<T>(this ILinVector3D<T> vector, Scalar<T> scalar)
    //{
    //    return new Quaternion(
    //        (float)vector.X,
    //        (float)vector.Y,
    //        (float)vector.Z,
    //        (float)scalar
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> GetScalarPart<T>(this Quaternion quaternion, IScalarProcessor<T> scalarProcessor)
    {
        return quaternion.W.ScalarFromNumber(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> GetVectorPart<T>(this Quaternion quaternion, IScalarProcessor<T> scalarProcessor)
    {
        return LinVector3D<T>.Create(
            scalarProcessor,
            quaternion.X,
            quaternion.Y,
            quaternion.Z
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<Scalar<T>, LinVector3D<T>> GetScalarVectorParts<T>(this Quaternion quaternion, IScalarProcessor<T> scalarProcessor)
    {
        return new Tuple<Scalar<T>, LinVector3D<T>>(
            quaternion.W.ScalarFromNumber(scalarProcessor),
            LinVector3D<T>.Create(scalarProcessor, quaternion.X, quaternion.Y, quaternion.Z)
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Quaternion Conjugate<T>(this Quaternion quaternion)
    //{
    //    return Quaternion.Conjugate(quaternion);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Quaternion Inverse<T>(this Quaternion quaternion)
    //{
    //    return Quaternion.Inverse(quaternion);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Quaternion Normalize<T>(this Quaternion quaternion)
    //{
    //    return Quaternion.Normalize(quaternion);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Quaternion Concatenate<T>(this Quaternion quaternion1, Quaternion quaternion2)
    //{
    //    return Quaternion.Concatenate(
    //        quaternion1,
    //        quaternion2
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Quaternion Concatenate<T>(this Quaternion quaternion1, Quaternion quaternion2, Quaternion quaternion3)
    //{
    //    return Quaternion.Concatenate(
    //        Quaternion.Concatenate(
    //            quaternion1,
    //            quaternion2
    //        ),
    //        quaternion3
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Quaternion Concatenate<T>(this Quaternion quaternion1, params Quaternion[] quaternionList)
    //{
    //    return quaternionList.Aggregate(
    //        quaternion1,
    //        Quaternion.Concatenate
    //    );
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> RotateVector<T>(this Quaternion quaternion, LinUnitBasisVector3D axis, IScalarProcessor<T> scalarProcessor)
    {
        Debug.Assert(
            quaternion.IsNearNormalized()
        );

        var q = quaternion.ToQuaternion(scalarProcessor);

        var v = LinQuaternion<T>.Create(
            axis.GetX(scalarProcessor),
            axis.GetY(scalarProcessor),
            axis.GetZ(scalarProcessor),
            scalarProcessor.Zero
        );

        return (q * v * q.Inverse()).GetNormalVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> RotateVector<T>(this Quaternion quaternion, Scalar<T> x, Scalar<T> y, Scalar<T> z)
    {
        Debug.Assert(
            quaternion.IsNearNormalized()
        );

        var scalarProcessor = x.ScalarProcessor;

        var v = LinQuaternion<T>.Create(
            x,
            y,
            z,
            scalarProcessor.Zero
        );

        var q = quaternion.ToQuaternion(scalarProcessor);

        return (q * v * q.Inverse()).GetNormalVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> RotateVector<T>(this Quaternion quaternion, ILinVector3D<T> vector)
    {
        Debug.Assert(
            quaternion.IsNearNormalized()
        );

        var scalarProcessor = vector.ScalarProcessor;

        var q = quaternion.ToQuaternion(scalarProcessor);

        var v = LinQuaternion<T>.Create(
            vector.X,
            vector.Y,
            vector.Z,
            scalarProcessor.Zero
        );

        return (q * v * q.Inverse()).GetNormalVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> RotateVectorUsing<T>(this ILinVector3D<T> vector, Quaternion quaternion)
    {
        Debug.Assert(
            quaternion.IsNearNormalized()
        );

        var scalarProcessor = vector.ScalarProcessor;

        var q = quaternion.ToQuaternion(scalarProcessor);

        var v = LinQuaternion<T>.Create(
            vector.X,
            vector.Y,
            vector.Z,
            scalarProcessor.Zero
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
    public static LinQuaternion<T> Slerp<T>(this Scalar<T> t, LinQuaternion<T> v1, LinQuaternion<T> v2)
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

            var d = 1 / (x * x + y * y + z * z + w * w).Sqrt();

            return LinQuaternion<T>.Create(x * d, y * d, z * d, w * d);
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
        var theta = cosTheta.ArcCos();

        //thetaP = angle between value1 and result quaternion, interpreted as vectors
        var thetaP = theta * t;
        var cosThetaP = thetaP.Cos();
        var sinThetaP = thetaP.Sin();

        //Make { value1, qPerp } an orthogonal basis in quaternion vector space
        var qPerpX = v2.ScalarI - v1.ScalarI * cosTheta;
        var qPerpY = v2.ScalarJ - v1.ScalarJ * cosTheta;
        var qPerpZ = v2.ScalarK - v1.ScalarK * cosTheta;
        var qPerpW = v2.Scalar - v1.Scalar * cosTheta;
        var qPerpInvLength = 1 / (qPerpX * qPerpX + qPerpY * qPerpY + qPerpZ * qPerpZ + qPerpW * qPerpW).Sqrt();

        //Final result
        return LinQuaternion<T>.Create(v1.ScalarI * cosThetaP + qPerpX * qPerpInvLength * sinThetaP,
            v1.ScalarJ * cosThetaP + qPerpY * qPerpInvLength * sinThetaP,
            v1.ScalarK * cosThetaP + qPerpZ * qPerpInvLength * sinThetaP,
            v1.Scalar * cosThetaP + qPerpW * qPerpInvLength * sinThetaP);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinQuaternion<T>> Slerp<T>(this IEnumerable<Scalar<T>> tList, LinQuaternion<T> v1, LinQuaternion<T> v2)
    {
        return tList.Select(t => t.Slerp(v1, v2));
    }

}