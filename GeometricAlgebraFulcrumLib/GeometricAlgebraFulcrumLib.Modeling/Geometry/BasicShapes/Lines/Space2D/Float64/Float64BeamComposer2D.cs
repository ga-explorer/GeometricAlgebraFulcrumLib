﻿using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;

/// <summary>
/// This represents a mutable beam (a pair of rays with a common origin)
/// in 2D space
/// </summary>
public sealed class Float64BeamComposer2D : 
    IFloat64Beam2D
{
    public static Float64BeamComposer2D Create(ILinFloat64Vector2D origin, ILinFloat64Vector2D direction1, ILinFloat64Vector2D direction2)
    {
        return new Float64BeamComposer2D(
            origin.X,
            origin.Y,
            direction1.X,
            direction1.Y,
            direction2.X,
            direction2.Y
        );
    }


    public double OriginX { get; set; }

    public double OriginY { get; set; }

    public double Direction1X { get; set; }

    public double Direction1Y { get; set; }

    public double Direction2X { get; set; }

    public double Direction2Y { get; set; }


    public bool IsValid()
    {
        return !double.IsNaN(OriginX) &&
               !double.IsNaN(OriginY) &&
               !double.IsNaN(Direction1X) &&
               !double.IsNaN(Direction1Y) &&
               !double.IsNaN(Direction2X) &&
               !double.IsNaN(Direction2Y);
    }


    public Float64BeamComposer2D()
    {
    }

    internal Float64BeamComposer2D(double pX, double pY, double v1X, double v1Y, double v2X, double v2Y)
    {
        OriginX = pX;
        OriginY = pY;

        Direction1X = v1X;
        Direction1Y = v1Y;

        Direction2X = v2X;
        Direction2Y = v2Y;
    }


    public Float64BeamComposer2D SetOrigin(ILinFloat64Vector2D origin)
    {
        OriginX = origin.X;
        OriginY = origin.Y;

        return this;
    }

    public Float64BeamComposer2D SetOrigin(double originX, double originY)
    {
        OriginX = originX;
        OriginY = originY;

        return this;
    }

    public Float64BeamComposer2D SetDirection1(ILinFloat64Vector2D direction)
    {
        Direction1X = direction.X;
        Direction1Y = direction.Y;

        return this;
    }

    public Float64BeamComposer2D SetDirection1(double directionX, double directionY)
    {
        Direction1X = directionX;
        Direction1Y = directionY;

        return this;
    }

    public Float64BeamComposer2D SetDirection2(ILinFloat64Vector2D direction)
    {
        Direction2X = direction.X;
        Direction2Y = direction.Y;

        return this;
    }

    public Float64BeamComposer2D SetDirection2(double directionX, double directionY)
    {
        Direction2X = directionX;
        Direction2Y = directionY;

        return this;
    }

    public Float64BeamComposer2D SetBeam(ILinFloat64Vector2D origin, ILinFloat64Vector2D direction1, ILinFloat64Vector2D direction2)
    {
        OriginX = origin.X;
        OriginY = origin.Y;

        Direction1X = direction1.X;
        Direction1Y = direction1.Y;

        Direction2X = direction2.X;
        Direction2Y = direction2.Y;

        return this;
    }
}