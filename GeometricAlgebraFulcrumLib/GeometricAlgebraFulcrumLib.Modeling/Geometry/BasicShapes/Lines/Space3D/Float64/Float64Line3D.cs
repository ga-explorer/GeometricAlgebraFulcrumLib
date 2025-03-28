﻿using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;

public sealed class Float64Line3D : 
    IFloat64Line3D
{
    public double OriginX { get; }

    public double OriginY { get; }

    public double OriginZ { get; }


    public double DirectionX { get; }

    public double DirectionY { get; }

    public double DirectionZ { get; }


    public bool IsValid()
    {
        return !double.IsNaN(OriginX) &&
               !double.IsNaN(OriginY) &&
               !double.IsNaN(OriginZ) &&
               !double.IsNaN(DirectionX) &&
               !double.IsNaN(DirectionY) &&
               !double.IsNaN(DirectionZ);
    }


    public Float64Line3D(double originX, double originY, double originZ, double directionX, double directionY, double directionZ)
    {
        OriginX = originX;
        OriginY = originY;
        OriginZ = originZ;

        DirectionX = directionX;
        DirectionY = directionY;
        DirectionZ = directionZ;
    }

    public Float64Line3D(ILinFloat64Vector3D origin, ILinFloat64Vector3D direction)
    {
        OriginX = origin.X;
        OriginY = origin.Y;
        OriginZ = origin.Z;

        DirectionX = direction.X;
        DirectionY = direction.Y;
        DirectionZ = direction.Z;

        Debug.Assert(IsValid());
    }


    public Float64Line3D ToLine()
    {
        return this;
    }
}