using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space2D
{
    public sealed class TranslationMap2D : IAffineMap2D
    {
        public double DirectionX { get; private set; }

        public double DirectionY { get; private set; }

        public Tuple2D Direction
        {
            get { return new Tuple2D(DirectionX, DirectionY); }
        }


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


        public SquareMatrix3 ToSquareMatrix3()
        {
            return SquareMatrix3.CreateTranslationMatrix2D(DirectionX, DirectionY);
        }

        public double[,] ToArray2D()
        {
            throw new System.NotImplementedException();
        }

        public Tuple2D MapPoint(ITuple2D point)
        {
            return new Tuple2D(
                point.X + DirectionX, 
                point.Y + DirectionY
            );
        }

        public Tuple2D MapVector(ITuple2D vector)
        {
            return vector.ToTuple2D();
        }

        public Tuple2D MapNormal(ITuple2D normal)
        {
            return normal.ToTuple2D();
        }

        public IAffineMap2D InverseMap()
        {
            return new TranslationMap2D(-DirectionX, -DirectionY);
        }

        public bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}
