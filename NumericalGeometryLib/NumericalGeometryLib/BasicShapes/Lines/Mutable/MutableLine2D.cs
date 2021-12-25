using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicShapes.Lines.Immutable;

namespace NumericalGeometryLib.BasicShapes.Lines.Mutable
{
    public sealed class MutableLine2D : ILine2D
    {
        public static MutableLine2D Create()
        {
            return new MutableLine2D();
        }

        public static MutableLine2D Create(double originX, double originY, double directionX, double directionY)
        {
            return new MutableLine2D(
                originX,
                originY,
                directionX,
                directionY
            );
        }

        public static MutableLine2D Create(ITuple2D origin, ITuple2D direction)
        {
            return new MutableLine2D(
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


        public MutableLine2D SetOrigin(ITuple2D origin)
        {
            OriginX = origin.X;
            OriginY = origin.Y;

            return this;
        }

        public MutableLine2D SetOrigin(double originX, double originY)
        {
            OriginX = originX;
            OriginY = originY;

            return this;
        }

        public MutableLine2D SetDirection(ITuple2D direction)
        {
            DirectionX = direction.X;
            DirectionY = direction.Y;

            return this;
        }

        public MutableLine2D SetDirection(double directionX, double directionY)
        {
            DirectionX = directionX;
            DirectionY = directionY;

            return this;
        }

        public MutableLine2D SetDirectionLength(double newLength)
        {
            var oldLength =
                DirectionX * DirectionX +
                DirectionY * DirectionY;

            var factor = newLength / oldLength;

            DirectionX = DirectionX * factor;
            DirectionY = DirectionY * factor;

            return this;
        }

        public MutableLine2D SetDirectionLengthToUnit()
        {
            var oldLength =
                DirectionX * DirectionX +
                DirectionY * DirectionY;

            var factor = 1 / oldLength;

            DirectionX = DirectionX * factor;
            DirectionY = DirectionY * factor;

            return this;
        }

        public MutableLine2D SetLine(double originX, double originY, double directionX, double directionY)
        {
            OriginX = originX;
            OriginY = originY;

            DirectionX = directionX;
            DirectionY = directionY;

            return this;
        }

        public MutableLine2D SetLine(ITuple2D origin, ITuple2D direction)
        {
            OriginX = origin.X;
            OriginY = origin.Y;

            DirectionX = direction.X;
            DirectionY = direction.Y;

            return this;
        }

        public MutableLine2D SetLine(ILine2D line)
        {
            OriginX = line.OriginX;
            OriginY = line.OriginY;

            DirectionX = line.DirectionX;
            DirectionY = line.DirectionY;

            return this;
        }


        public MutableLine2D()
        {
        }

        internal MutableLine2D(double originX, double originY, double directionX, double directionY)
        {
            OriginX = originX;
            OriginY = originY;

            DirectionX = directionX;
            DirectionY = directionY;
        }


        public Line2D ToLine()
        {
            return new Line2D(
                OriginX,
                OriginY,
                DirectionX,
                DirectionY
            );
        }
    }
}