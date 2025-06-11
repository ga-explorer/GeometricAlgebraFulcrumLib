using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

public interface ILinFloat64SphericalVector3D :
    ILinFloat64Vector3D
{
    double R { get; }

    LinFloat64PolarAngle Theta { get; }

    LinFloat64PolarAngle Phi { get; }

    bool IsUnitVector();

    bool IsNearUnitVector(double zeroEpsilon = 1e-12d);
}