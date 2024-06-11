using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

public interface ILinFloat64PolarVector2D :
    ILinFloat64Vector2D
{
    Float64Scalar R { get; }

    LinFloat64PolarAngle Theta { get; }

    bool IsUnitVector();

    bool IsNearUnitVector(double epsilon = 1e-12d);
}