using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

public interface ILinFloat64PolarVector2D :
    ILinFloat64Vector2D
{
    double R { get; }

    LinFloat64PolarAngle Theta { get; }

    bool IsUnitVector();

    bool IsNearUnitVector(double tolerance = 1e-12d);
}