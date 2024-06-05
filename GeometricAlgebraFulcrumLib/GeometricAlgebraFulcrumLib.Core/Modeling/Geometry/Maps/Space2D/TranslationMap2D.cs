using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Maps.Space2D;

public sealed class TranslationMap2D : 
    IAffineMap2D
{
    public double DirectionX { get; private set; }

    public double DirectionY { get; private set; }

    public LinFloat64Vector2D Direction
    {
        get { return LinFloat64Vector2D.Create((Float64Scalar)DirectionX, (Float64Scalar)DirectionY); }
    }


    public TranslationMap2D(double directionX, double directionY)
    {
        DirectionX = directionX;
        DirectionY = directionY;
    }

    public TranslationMap2D(LinFloat64Vector2D direction)
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

    public LinFloat64Vector2D MapPoint(ILinFloat64Vector2D point)
    {
        return LinFloat64Vector2D.Create(point.X + DirectionX, 
            point.Y + DirectionY);
    }

    public LinFloat64Vector2D MapVector(ILinFloat64Vector2D vector)
    {
        return vector.ToLinVector2D();
    }

    public LinFloat64Vector2D MapNormal(ILinFloat64Vector2D normal)
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