using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

public interface IFloat64PolarVector2D : 
    IFloat64Vector2D
{
    Float64Scalar R { get; }

    Float64PlanarAngle Theta { get; }
        
    bool IsUnitVector();

    bool IsNearUnitVector(double epsilon = 1e-12d);
}