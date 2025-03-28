﻿using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Planes.Space3D.Float64;

public sealed class Float64Plane3D :
    IFloat64Plane3D
{
    public double OriginX { get; }

    public double OriginY { get; }

    public double OriginZ { get; }


    public double Direction1X { get; }

    public double Direction1Y { get; }

    public double Direction1Z { get; }


    public double Direction2X { get; }

    public double Direction2Y { get; }

    public double Direction2Z { get; }


    public bool IsValid()
    {
        return !double.IsNaN(OriginX) &&
               !double.IsNaN(OriginY) &&
               !double.IsNaN(OriginZ) &&
               !double.IsNaN(Direction1X) &&
               !double.IsNaN(Direction1Y) &&
               !double.IsNaN(Direction1Z) &&
               !double.IsNaN(Direction2X) &&
               !double.IsNaN(Direction2Y) &&
               !double.IsNaN(Direction2Z);
    }


    public Float64Plane3D(double originX, double originY, double originZ, double direction1X, double direction1Y, double direction1Z, double direction2X, double direction2Y, double direction2Z)
    {
        OriginX = originX;
        OriginY = originY;
        OriginZ = originZ;

        Direction1X = direction1X;
        Direction1Y = direction1Y;
        Direction1Z = direction1Z;

        Direction2X = direction2X;
        Direction2Y = direction2Y;
        Direction2Z = direction2Z;

        Debug.Assert(IsValid());
    }

    public Float64Plane3D(ILinFloat64Vector3D origin, ILinFloat64Vector3D direction1, ILinFloat64Vector3D direction2)
    {
        OriginX = origin.X;
        OriginY = origin.Y;
        OriginZ = origin.Z;

        Direction1X = direction1.X;
        Direction1Y = direction1.Y;
        Direction1Z = direction1.Z;

        Direction2X = direction2.X;
        Direction2Y = direction2.Y;
        Direction2Z = direction2.Z;

        Debug.Assert(IsValid());
    }


    public Float64Plane3D ToPlane()
    {
        return this;
    }
}