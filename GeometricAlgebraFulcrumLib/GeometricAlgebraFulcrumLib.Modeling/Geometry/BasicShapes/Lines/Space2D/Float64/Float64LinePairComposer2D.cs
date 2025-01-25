using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;

public sealed class Float64LinePairComposer2D : 
    IFloat64LinePair2D
{
    public Float64LinePairComposer2D Create(IFloat64Line2D ray1, IFloat64Line2D ray2)
    {
        return new Float64LinePairComposer2D(
            ray1.OriginX, ray1.OriginY, ray1.DirectionX, ray1.DirectionY,
            ray2.OriginX, ray2.OriginY, ray2.DirectionX, ray2.DirectionY
        );
    }

    public Float64LinePairComposer2D Create(ILinFloat64Vector2D origin1, ILinFloat64Vector2D direction1, ILinFloat64Vector2D origin2, ILinFloat64Vector2D direction2)
    {
        return new Float64LinePairComposer2D(
            origin1.X,
            origin1.Y,
            direction1.X,
            direction1.Y,
            origin2.X,
            origin2.Y,
            direction2.X,
            direction2.Y
        );
    }


    public double Origin1X { get; set; }

    public double Origin1Y { get; set; }


    public double Direction1X { get; set; }

    public double Direction1Y { get; set; }


    public double Origin2X { get; set; }

    public double Origin2Y { get; set; }


    public double Direction2X { get; set; }

    public double Direction2Y { get; set; }


    public bool IsValid()
    {
        return !double.IsNaN(Origin1X) &&
               !double.IsNaN(Origin1Y) &&
               !double.IsNaN(Origin2X) &&
               !double.IsNaN(Origin2Y) &&
               !double.IsNaN(Direction1X) &&
               !double.IsNaN(Direction1Y) &&
               !double.IsNaN(Direction2X) &&
               !double.IsNaN(Direction2Y);
    }


    public Float64LinePairComposer2D()
    {
    }

    internal Float64LinePairComposer2D(double o1X, double o1Y, double d1X, double d1Y, double o2X, double o2Y, double d2X, double d2Y)
    {
        Origin1X = o1X;
        Origin1Y = o1Y;

        Direction1X = d1X;
        Direction1Y = d1Y;

        Origin2X = o2X;
        Origin2Y = o2Y;

        Direction2X = d2X;
        Direction2Y = d2Y;

        Debug.Assert(IsValid());
    }


    public Float64LinePairComposer2D SetOrigin1(double originX, double originY)
    {
        Origin1X = originX;
        Origin1Y = originY;

        return this;
    }

    public Float64LinePairComposer2D SetOrigin1(ILinFloat64Vector2D origin)
    {
        Origin1X = origin.X;
        Origin1Y = origin.Y;

        return this;
    }

    public Float64LinePairComposer2D SetDirection1(double directionX, double directionY)
    {
        Direction1X = directionX;
        Direction1Y = directionY;

        return this;
    }

    public Float64LinePairComposer2D SetDirection1(ILinFloat64Vector2D direction)
    {
        Direction1X = direction.X;
        Direction1Y = direction.Y;

        return this;
    }

    public Float64LinePairComposer2D SetLine1(IFloat64Line2D ray)
    {
        Origin1X = ray.OriginX;
        Origin1Y = ray.OriginY;

        Direction1X = ray.DirectionX;
        Direction1Y = ray.DirectionY;

        return this;
    }

    public Float64LinePairComposer2D SetOrigin2(double originX, double originY)
    {
        Origin2X = originX;
        Origin2Y = originY;

        return this;
    }

    public Float64LinePairComposer2D SetOrigin2(ILinFloat64Vector2D origin)
    {
        Origin2X = origin.X;
        Origin2Y = origin.Y;

        return this;
    }

    public Float64LinePairComposer2D SetDirection2(double directionX, double directionY)
    {
        Direction2X = directionX;
        Direction2Y = directionY;

        return this;
    }

    public Float64LinePairComposer2D SetDirection2(ILinFloat64Vector2D direction)
    {
        Direction2X = direction.X;
        Direction2Y = direction.Y;

        return this;
    }

    public Float64LinePairComposer2D SetLine2(IFloat64Line2D ray)
    {
        Origin2X = ray.OriginX;
        Origin2Y = ray.OriginY;

        Direction2X = ray.DirectionX;
        Direction2Y = ray.DirectionY;

        return this;
    }

    public Float64LinePairComposer2D SetLinePair(IFloat64Line2D ray1, IFloat64Line2D ray2)
    {
        Origin1X = ray1.OriginX;
        Origin1Y = ray1.OriginY;

        Direction1X = ray1.DirectionX;
        Direction1Y = ray1.DirectionY;

        Origin2X = ray2.OriginX;
        Origin2Y = ray2.OriginY;

        Direction2X = ray2.DirectionX;
        Direction2Y = ray2.DirectionY;

        return this;
    }
}