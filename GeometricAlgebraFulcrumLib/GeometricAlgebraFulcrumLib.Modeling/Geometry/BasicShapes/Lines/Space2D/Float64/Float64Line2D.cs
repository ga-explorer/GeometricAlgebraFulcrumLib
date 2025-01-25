using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;

public sealed class Float64Line2D : 
    IFloat64Line2D
{
    public static Float64Line2D Create(double originX, double originY, double directionX, double directionY)
    {
        return new Float64Line2D(
            originX,
            originY,
            directionX,
            directionY
        );
    }

    public static Float64Line2D Create(ILinFloat64Vector2D point, ILinFloat64Vector2D vector)
    {
        return new Float64Line2D(
            point.X,
            point.Y,
            vector.X,
            vector.Y
        );
    }


    public double OriginX { get; }

    public double OriginY { get; }


    public double DirectionX { get; }

    public double DirectionY { get; }


    public bool IsValid()
    {
        return !double.IsNaN(OriginX) &&
               !double.IsNaN(OriginY) &&
               !double.IsNaN(DirectionX) &&
               !double.IsNaN(DirectionY);
    }


    public Float64Line2D(double originX, double originY, double directionX, double directionY)
    {
        OriginX = originX;
        OriginY = originY;

        DirectionX = directionX;
        DirectionY = directionY;
    }


    public Float64Line2D ToLine()
    {
        return this;
    }
}