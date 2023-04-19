using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space2D
{
    public sealed class TranslationMap2D : IAffineMap2D
    {
        public double DirectionX { get; private set; }

        public double DirectionY { get; private set; }

        public Float64Tuple2D Direction
        {
            get { return new Float64Tuple2D(DirectionX, DirectionY); }
        }


        public TranslationMap2D(double directionX, double directionY)
        {
            DirectionX = directionX;
            DirectionY = directionY;
        }

        public TranslationMap2D(Float64Tuple2D direction)
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
            throw new NotImplementedException();
        }

        public Float64Tuple2D MapPoint(IFloat64Tuple2D point)
        {
            return new Float64Tuple2D(
                point.X + DirectionX, 
                point.Y + DirectionY
            );
        }

        public Float64Tuple2D MapVector(IFloat64Tuple2D vector)
        {
            return vector.ToTuple2D();
        }

        public Float64Tuple2D MapNormal(IFloat64Tuple2D normal)
        {
            return normal.ToTuple2D();
        }

        public IAffineMap2D InverseMap()
        {
            return new TranslationMap2D(-DirectionX, -DirectionY);
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
