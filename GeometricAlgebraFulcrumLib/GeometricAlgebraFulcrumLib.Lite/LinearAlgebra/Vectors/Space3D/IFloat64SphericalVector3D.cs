using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

public interface IFloat64SphericalVector3D : 
    IFloat64Vector3D
{
    Float64Scalar R { get; }

    Float64PlanarAngle Theta { get; }

    Float64PlanarAngle Phi { get; }

    bool IsUnitVector();

    bool IsNearUnitVector(double epsilon = 1e-12d);
}