using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space4D;

public interface IFloat64AffineMap4D :
    IAlgebraicElement
{
    bool SwapsHandedness { get; }

    bool IsIdentity();

    bool IsNearIdentity(double tolerance = Float64Utils.DefaultTolerance);

    LinFloat64Vector4D MapPoint(ILinFloat64Vector4D point);

    LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector);

    LinFloat64Vector4D MapNormal(ILinFloat64Vector4D normal);

    SquareMatrix4 GetSquareMatrix4();

    Matrix<double> GetMatrix();

    IFloat64AffineMap4D GetInverseAffineMap();
}