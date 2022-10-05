using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Constants;
using NumericalGeometryLib.BasicMath.Tuples;

namespace NumericalGeometryLib.BasicMath.Matrices;

public static class QuaternionUtils
{
    public static Quaternion CreateAxisToAxisRotationQuaternion(this Axis3D axis1, Axis3D axis2)
    {
        var sqrt2Inv = (float)(1d / Math.Sqrt(2d));

        return axis1 switch
        {
            Axis3D.PositiveX => axis2 switch
            {
                Axis3D.PositiveX => Quaternion.Identity,
                Axis3D.PositiveY => new Quaternion(0, 0, sqrt2Inv, sqrt2Inv),
                Axis3D.PositiveZ => new Quaternion(0, -sqrt2Inv, 0, sqrt2Inv),
                Axis3D.NegativeX => new Quaternion(0, 0, 1, 0),
                Axis3D.NegativeY => new Quaternion(0, 0, -sqrt2Inv, sqrt2Inv),
                _ => new Quaternion(0, sqrt2Inv, 0, sqrt2Inv),
            },

            Axis3D.PositiveY => axis2 switch
            {
                Axis3D.PositiveX => new Quaternion(0, 0, -sqrt2Inv, sqrt2Inv),
                Axis3D.PositiveY => Quaternion.Identity,
                Axis3D.PositiveZ => new Quaternion(sqrt2Inv, 0, 0, sqrt2Inv),
                Axis3D.NegativeX => new Quaternion(0, 0, sqrt2Inv, sqrt2Inv),
                Axis3D.NegativeY => new Quaternion(1, 0, 0, 0),
                _ => new Quaternion(-sqrt2Inv, 0, 0, sqrt2Inv),
            },

            Axis3D.PositiveZ => axis2 switch
            {
                Axis3D.PositiveX => new Quaternion(0, sqrt2Inv, 0, sqrt2Inv),
                Axis3D.PositiveY => new Quaternion(-sqrt2Inv, 0, 0, sqrt2Inv),
                Axis3D.PositiveZ => Quaternion.Identity,
                Axis3D.NegativeX => new Quaternion(0, -sqrt2Inv, 0, sqrt2Inv),
                Axis3D.NegativeY => new Quaternion(sqrt2Inv, 0, 0, sqrt2Inv),
                _ => new Quaternion(0, 1, 0, 0),
            },

            Axis3D.NegativeX => axis2 switch
            {
                Axis3D.PositiveX => new Quaternion(0, 0, 1, 0),
                Axis3D.PositiveY => new Quaternion(0, 0, -sqrt2Inv, sqrt2Inv),
                Axis3D.PositiveZ => new Quaternion(0, sqrt2Inv, 0, sqrt2Inv),
                Axis3D.NegativeX => Quaternion.Identity,
                Axis3D.NegativeY => new Quaternion(0, 0, sqrt2Inv, sqrt2Inv),
                _ => new Quaternion(0, -sqrt2Inv, 0, sqrt2Inv),
            },

            Axis3D.NegativeY => axis2 switch
            {
                Axis3D.PositiveX => new Quaternion(0, 0, sqrt2Inv, sqrt2Inv),
                Axis3D.PositiveY => new Quaternion(1, 0, 0, 0),
                Axis3D.PositiveZ => new Quaternion(-sqrt2Inv, 0, 0, sqrt2Inv),
                Axis3D.NegativeX => new Quaternion(0, 0, -sqrt2Inv, sqrt2Inv),
                Axis3D.NegativeY => Quaternion.Identity,
                _ => new Quaternion(sqrt2Inv, 0, 0, sqrt2Inv),
            },

            _ => axis2 switch
            {
                Axis3D.PositiveX => new Quaternion(0, -sqrt2Inv, 0, sqrt2Inv),
                Axis3D.PositiveY => new Quaternion(sqrt2Inv, 0, 0, sqrt2Inv),
                Axis3D.PositiveZ => new Quaternion(0, 1, 0, 0),
                Axis3D.NegativeX => new Quaternion(0, sqrt2Inv, 0, sqrt2Inv),
                Axis3D.NegativeY => new Quaternion(-sqrt2Inv, 0, 0, sqrt2Inv),
                _ => Quaternion.Identity,
            },
        };
    }
    
    public static Tuple<ITuple3D, double> CreateAxisToVectorRotationAxisAngle(this Axis3D axis, ITuple3D unitVector)
    {
        //Debug.Assert(
        //    (unitVector.GetLengthSquared() - 1).IsNearZero()
        //);

        var dot12 = axis.VectorDot(unitVector);

        // The case where the two vectors are almost opposite
        if ((dot12 + 1d).IsNearZero())
            return new Tuple<ITuple3D, double>(
                axis.GetUnitNormal().GetVector3D(), 
                Math.PI
            );

        // The general case
        return new Tuple<ITuple3D, double>(
            axis.VectorUnitCross(unitVector), 
            Math.Acos(dot12)
        );
    }

    public static Tuple<ITuple3D, double> CreateVectorToVectorRotationAxisAngle(this ITuple3D unitVector1, ITuple3D unitVector2)
    {
        Debug.Assert(
            (unitVector1.GetLengthSquared() - 1).IsNearZero() &&
            (unitVector2.GetLengthSquared() - 1).IsNearZero()
        );

        var dot12 = unitVector1.VectorDot(unitVector2);

        // The case where the two vectors are almost opposite
        if ((dot12 + 1d).IsNearZero())
            return new Tuple<ITuple3D, double>(
                unitVector1.GetUnitNormal(), 
                Math.PI
            );

        // The general case
        return new Tuple<ITuple3D, double>(
            unitVector1.VectorUnitCross(unitVector2), 
            Math.Acos(dot12)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion CreateVectorToVectorRotationQuaternion(this ITuple3D unitVector1, ITuple3D unitVector2)
    {
        var (u, a) = 
            unitVector1.CreateVectorToVectorRotationAxisAngle(unitVector2);

        return Quaternion.CreateFromAxisAngle(
            new Vector3((float) u.X, (float) u.Y, (float) u.Z),
            (float) a
        );
    }

    public static Tuple<Axis3D, Quaternion> CreateNearestAxisToVectorRotationQuaternion(this ITuple3D unitVector)
    {
        Debug.Assert(
            (unitVector.GetLengthSquared() - 1).IsNearZero()
        );

        var axis = unitVector.SelectNearestAxis();

        var x = 0f;
        var y = 0f;
        var z = 0f;
        var w = 0f;

        if (axis == Axis3D.PositiveX)
        {
            var v1 = 1d + unitVector.X;
            var v2 = 1d / Math.Sqrt(2d * v1);

            y = (float)(-unitVector.Z * v2);
            z = (float)(unitVector.Y * v2);
            w = (float)(v1 * v2);
        }

        if (axis == Axis3D.NegativeX)
        {
            var v1 = 1d - unitVector.X;
            var v2 = 1d / Math.Sqrt(2d * v1);

            y = (float)(unitVector.Z * v2);
            z = (float)(-unitVector.Y * v2);
            w = (float)(v1 * v2);
        }

        if (axis == Axis3D.PositiveY)
        {
            var v1 = 1d + unitVector.Y;
            var v2 = 1d / Math.Sqrt(2d * v1);

            x = (float)(unitVector.Z * v2);
            z = (float)(-unitVector.X * v2);
            w = (float)(v1 * v2);
        }

        if (axis == Axis3D.NegativeY)
        {
            var v1 = 1d - unitVector.Y;
            var v2 = 1d / Math.Sqrt(2d * v1);

            x = (float)(-unitVector.Z * v2);
            z = (float)(unitVector.X * v2);
            w = (float)(v1 * v2);
        }

        if (axis == Axis3D.PositiveZ)
        {
            var v1 = 1d + unitVector.Z;
            var v2 = 1d / Math.Sqrt(2d * v1);

            x = (float)(-unitVector.Y * v2);
            y = (float)(unitVector.X * v2);
            w = (float)(v1 * v2);
        }

        if (axis == Axis3D.NegativeZ)
        {
            var v1 = 1d - unitVector.Z;
            var v2 = 1d / Math.Sqrt(2d * v1);

            x = (float)(unitVector.Y * v2);
            y = (float)(-unitVector.X * v2);
            w = (float)(v1 * v2);
        }

        var quaternion = new Quaternion(x, y, z, w);
        
        return Tuple.Create(axis, quaternion);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion CreateAxisToVectorRotationQuaternion(this ITuple3D unitVector, Axis3D axis)
    {
        return axis.CreateAxisToVectorRotationQuaternion(unitVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion CreateAxisPairToVectorPairRotationQuaternion(this Axis3D axis1, Axis3D axis2, ITuple3D unitVector1, ITuple3D unitVector2)
    {
        var q1 = 
            axis1.CreateAxisToVectorRotationQuaternion(unitVector1);

        var axis2Rotated = 
            q1.Rotate(axis2).ToUnitVector();

        var q2 = 
            axis2Rotated.CreateVectorToVectorRotationQuaternion(unitVector2);

        return q1.Concatenate(q2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<Quaternion> CreateAxisPairToVectorPairRotationQuaternionPair(this Axis3D axis1, Axis3D axis2, ITuple3D unitVector1, ITuple3D unitVector2)
    {
        var q1 = 
            axis1.CreateAxisToVectorRotationQuaternion(unitVector1);

        var axis2Rotated = 
            q1.Rotate(axis2).ToUnitVector();

        var q2 = 
            axis2Rotated.CreateVectorToVectorRotationQuaternion(unitVector2);

        return new Pair<Quaternion>(q1, q2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion CreateAxisToVectorRotationQuaternion(this Axis3D axis, ITuple3D unitVector)
    {
        var (u, a) = 
            axis.CreateAxisToVectorRotationAxisAngle(unitVector);

        return Quaternion.CreateFromAxisAngle(
            new Vector3((float) u.X, (float) u.Y, (float) u.Z),
            (float) a
        );

        //This gives a correct quaternion but not the simplest one (the one with smallest angle)
        //var (nearestAxis, q2) =
        //    unitVector.CreateNearestAxisToVectorRotationQuaternion();

        //var q1 =
        //    axis.CreateAxisToAxisRotationQuaternion(nearestAxis);

        //return Quaternion.Concatenate(q1, q2);
    }
}