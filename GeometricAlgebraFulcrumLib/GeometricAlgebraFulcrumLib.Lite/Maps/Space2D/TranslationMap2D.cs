using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Maps.Space2D
{
    public sealed class TranslationMap2D : 
        IAffineMap2D
    {
        public double DirectionX { get; private set; }

        public double DirectionY { get; private set; }

        public Float64Vector2D Direction
        {
            get { return Float64Vector2D.Create((Float64Scalar)DirectionX, (Float64Scalar)DirectionY); }
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

        public Float64Vector2D MapPoint(IFloat64Vector2D point)
        {
            return Float64Vector2D.Create(point.X + DirectionX, 
                point.Y + DirectionY);
        }

        public Float64Vector2D MapVector(IFloat64Vector2D vector)
        {
            return vector.ToVector2D();
        }

        public Float64Vector2D MapNormal(IFloat64Vector2D normal)
        {
            return normal.ToVector2D();
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
