using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.BasicMath.Maps.Space2D
{
    public sealed class TranslationMap2D : IAffineMap2D
    {
        public double DirectionX { get; private set; }

        public double DirectionY { get; private set; }

        public Tuple2D Direction 
            => new Tuple2D(DirectionX, DirectionY);


        public TranslationMap2D(double directionX, double directionY)
        {
            DirectionX = directionX;
            DirectionY = directionY;
        }

        public TranslationMap2D(Tuple2D direction)
        {
            DirectionX = direction.X;
            DirectionY = direction.Y;
        }


        public Matrix3X3 ToMatrix()
        {
            return Matrix3X3.CreateTranslationMatrix(DirectionX, DirectionY);
        }

        public ITuple2D MapPoint(ITuple2D point)
        {
            return new Tuple2D(
                point.X + DirectionX, 
                point.Y + DirectionY
            );
        }

        public ITuple2D MapVector(ITuple2D vector)
        {
            return vector;
        }

        public ITuple2D MapNormal(ITuple2D normal)
        {
            return normal;
        }

        public IAffineMap2D InverseMap()
        {
            return new TranslationMap2D(-DirectionX, -DirectionY);
        }
    }
}
