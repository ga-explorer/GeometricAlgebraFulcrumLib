using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space2D
{
    public sealed class TranslationMap2D : 
        IAffineMap2D
    {
        public double DirectionX { get; private set; }

        public double DirectionY { get; private set; }

        public Float64Vector2D Direction
        {
            get { return new Float64Vector2D(DirectionX, DirectionY); }
        }


        public TranslationMap2D(double directionX, double directionY)
        {
            DirectionX = directionX;
            DirectionY = directionY;
        }

        public TranslationMap2D(Float64Vector2D direction)
        {
            DirectionX = direction.X;
            DirectionY = direction.Y;
        }


        public SquareMatrix3 GetSquareMatrix3()
        {
            return SquareMatrix3.CreateTranslationMatrix2D(DirectionX, DirectionY);
        }

        public double[,] GetArray2D()
        {
            throw new NotImplementedException();
        }

        public bool SwapsHandedness 
            => false;

        public Float64Vector2D MapPoint(IFloat64Tuple2D point)
        {
            return new Float64Vector2D(
                point.X + DirectionX, 
                point.Y + DirectionY
            );
        }

        public Float64Vector2D MapVector(IFloat64Tuple2D vector)
        {
            return vector.ToLinVector2D();
        }

        public Float64Vector2D MapNormal(IFloat64Tuple2D normal)
        {
            return normal.ToLinVector2D();
        }

        public IAffineMap2D GetInverseAffineMap()
        {
            return new TranslationMap2D(-DirectionX, -DirectionY);
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
