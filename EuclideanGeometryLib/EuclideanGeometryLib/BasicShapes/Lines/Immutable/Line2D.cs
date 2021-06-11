using EuclideanGeometryLib.BasicMath.Tuples;

namespace EuclideanGeometryLib.BasicShapes.Lines.Immutable
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

        public static Line2D Create(ITuple2D point, ITuple2D vector)
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


        public bool IsValid
            => !double.IsNaN(OriginX) &&
               !double.IsNaN(OriginY) &&
               !double.IsNaN(DirectionX) &&
               !double.IsNaN(DirectionY);

        public bool IsInvalid
            => double.IsNaN(OriginX) || 
               double.IsNaN(OriginY) ||
               double.IsNaN(DirectionX) || 
               double.IsNaN(DirectionY);


        internal Line2D(double originX, double originY, double directionX, double directionY)
        {
            OriginX = originX;
            OriginY = originY;

            DirectionX = directionX;
            DirectionY = directionY;
        }


        public Line2D ToLine() 
            => this;
    }
}
