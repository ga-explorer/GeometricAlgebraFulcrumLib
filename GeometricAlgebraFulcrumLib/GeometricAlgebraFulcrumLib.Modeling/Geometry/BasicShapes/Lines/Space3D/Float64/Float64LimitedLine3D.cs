﻿using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;

public sealed class Float64LimitedLine3D : 
    IFloat64LimitedLine3D
{
    public static Float64LimitedLine3D Create(double originX, double originY, double originZ, double directionX, double directionY, double directionZ, double minParamValue, double maxParamValue)
    {
        return new Float64LimitedLine3D(
            originX,
            originY,
            originZ,
            directionX,
            directionY,
            directionZ,
            minParamValue,
            maxParamValue
        );
    }

    public static Float64LimitedLine3D Create(ILinFloat64Vector3D origin, ILinFloat64Vector3D direction, Float64ScalarRange parameterLimits)
    {
        return new Float64LimitedLine3D(
            origin.X,
            origin.Y,
            origin.Z,
            direction.X,
            direction.Y,
            direction.Z,
            parameterLimits.MinValue,
            parameterLimits.MaxValue
        );
    }


    public double OriginX { get; }

    public double OriginY { get; }

    public double OriginZ { get; }


    public double DirectionX { get; }

    public double DirectionY { get; }

    public double DirectionZ { get; }


    public double ParameterMinValue { get; }

    public double ParameterMaxValue { get; }


    public bool IsValid()
    {
        return !double.IsNaN(OriginX) &&
               !double.IsNaN(OriginY) &&
               !double.IsNaN(OriginZ) &&
               !double.IsNaN(DirectionX) &&
               !double.IsNaN(DirectionY) &&
               !double.IsNaN(DirectionZ) &&
               !double.IsNaN(ParameterMinValue) &&
               !double.IsNaN(ParameterMaxValue);
    }


    internal Float64LimitedLine3D(double originX, double originY, double originZ, double directionX, double directionY, double directionZ, double minParamValue, double maxParamValue)
    {
        OriginX = originX;
        OriginY = originY;
        OriginZ = originZ;

        DirectionX = directionX;
        DirectionY = directionY;
        DirectionZ = directionZ;

        ParameterMinValue = minParamValue;
        ParameterMaxValue = maxParamValue;
    }


    public Float64Line3D ToLine()
    {
        return new Float64Line3D(
            OriginX,
            OriginY,
            OriginZ,
            DirectionX,
            DirectionY,
            DirectionZ
        );
    }
}