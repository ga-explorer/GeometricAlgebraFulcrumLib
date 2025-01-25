using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;

public sealed class Float64ReflectionYAffineMap2D :
    IFloat64AffineMap2D
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

    public IFloat64AffineMap2D GetInverseAffineMap()
    {
        throw new NotImplementedException();
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
}