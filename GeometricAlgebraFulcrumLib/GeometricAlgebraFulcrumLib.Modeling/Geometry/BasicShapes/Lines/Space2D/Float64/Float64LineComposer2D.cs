﻿using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;

public sealed class Float64LineComposer2D : 
    IFloat64Line2D
{
    public static Float64LineComposer2D Create()
    {
        return new Float64LineComposer2D();
    }

    public static Float64LineComposer2D Create(double originX, double originY, double directionX, double directionY)
    {
        return new Float64LineComposer2D(
            originX,
            originY,
            directionX,
            directionY
        );
    }

    public static Float64LineComposer2D Create(ILinFloat64Vector2D origin, ILinFloat64Vector2D direction)
    {
        return new Float64LineComposer2D(
            origin.X,
            origin.Y,
            direction.X,
            direction.Y
        );
    }


    public double OriginX { get; set; }

    public double OriginY { get; set; }

    public double DirectionX { get; set; }

    public double DirectionY { get; set; }

    public bool IsValid()
    {
        return double.IsNaN(OriginX) &&
               double.IsNaN(OriginY) &&
               double.IsNaN(DirectionX) &&
               double.IsNaN(DirectionY);
    }


    public Float64LineComposer2D SetOrigin(ILinFloat64Vector2D origin)
    {
        OriginX = origin.X;
        OriginY = origin.Y;

        return this;
    }

    public Float64LineComposer2D SetOrigin(double originX, double originY)
    {
        OriginX = originX;
        OriginY = originY;

        return this;
    }

    public Float64LineComposer2D SetDirection(ILinFloat64Vector2D direction)
    {
        DirectionX = direction.X;
        DirectionY = direction.Y;

        return this;
    }

    public Float64LineComposer2D SetDirection(double directionX, double directionY)
    {
        DirectionX = directionX;
        DirectionY = directionY;

        return this;
    }

    public Float64LineComposer2D SetDirectionLength(double newLength)
    {
        var oldLength =
            DirectionX * DirectionX +
            DirectionY * DirectionY;

        var factor = newLength / oldLength;

        DirectionX = DirectionX * factor;
        DirectionY = DirectionY * factor;

        return this;
    }

    public Float64LineComposer2D SetDirectionLengthToUnit()
    {
        var oldLength =
            DirectionX * DirectionX +
            DirectionY * DirectionY;

        var factor = 1 / oldLength;

        DirectionX = DirectionX * factor;
        DirectionY = DirectionY * factor;

        return this;
    }

    public Float64LineComposer2D SetLine(double originX, double originY, double directionX, double directionY)
    {
        OriginX = originX;
        OriginY = originY;

        DirectionX = directionX;
        DirectionY = directionY;

        return this;
    }

    public Float64LineComposer2D SetLine(ILinFloat64Vector2D origin, ILinFloat64Vector2D direction)
    {
        OriginX = origin.X;
        OriginY = origin.Y;

        DirectionX = direction.X;
        DirectionY = direction.Y;

        return this;
    }

    public Float64LineComposer2D SetLine(IFloat64Line2D line)
    {
        OriginX = line.OriginX;
        OriginY = line.OriginY;

        DirectionX = line.DirectionX;
        DirectionY = line.DirectionY;

        return this;
    }


    public Float64LineComposer2D()
    {
    }

    internal Float64LineComposer2D(double originX, double originY, double directionX, double directionY)
    {
        OriginX = originX;
        OriginY = originY;

        DirectionX = directionX;
        DirectionY = directionY;
    }


    public Float64Line2D ToLine()
    {
        return new Float64Line2D(
            OriginX,
            OriginY,
            DirectionX,
            DirectionY
        );
    }
}