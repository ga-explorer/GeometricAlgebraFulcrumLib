using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;

public interface IFloat64AffineMap2D :
    IAlgebraicElement
{
    bool SwapsHandedness { get; }

    bool IsIdentity();

    LinFloat64Vector2D MapPoint(ILinFloat64Vector2D point);

    LinFloat64Vector2D MapVector(ILinFloat64Vector2D vector);

    LinFloat64Vector2D MapNormal(ILinFloat64Vector2D normal);

    SquareMatrix3 GetSquareMatrix3();

    double[,] GetArray2D();

    IFloat64AffineMap2D GetInverseAffineMap();
}