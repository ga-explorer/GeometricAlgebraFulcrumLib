using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Maps.Space2D;

public sealed class ReflectionXMap2D : 
    IAffineMap2D
{
    public SquareMatrix3 GetSquareMatrix3()
    {
        throw new NotImplementedException();
    }

    public double[,] GetArray2D()
    {
        throw new NotImplementedException();
    }

    public bool SwapsHandedness 
        => true;

    public LinFloat64Vector2D MapPoint(ILinFloat64Vector2D point)
    {
        throw new NotImplementedException();
    }

    public LinFloat64Vector2D MapVector(ILinFloat64Vector2D vector)
    {
        throw new NotImplementedException();
    }

    public LinFloat64Vector2D MapNormal(ILinFloat64Vector2D normal)
    {
        throw new NotImplementedException();
    }

    public IAffineMap2D GetInverseAffineMap()
    {
        throw new NotImplementedException();
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
}