using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines.Immutable
{
    public sealed class Line2D : ILine2D
    {
        public static Line2D Create(double originX, double originY, double directionX, double directionY)
        {
            return new Line2D(
                originX, 
                originY,
                directionX, 
                directionY
            );
        }

        public static Line2D Create(IFloat64Vector2D point, IFloat64Vector2D vector)
        {
            return new Line2D(
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


        public Line2D(double originX, double originY, double directionX, double directionY)
        {
            OriginX = originX;
            OriginY = originY;

            DirectionX = directionX;
            DirectionY = directionY;
        }


        public Line2D ToLine()
        {
            return this;
        }
    }
}
