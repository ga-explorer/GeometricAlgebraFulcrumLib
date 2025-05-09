using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space3D;

public interface ILinFloat64SphericalVector3D :
    ILinFloat64Vector3D
{
    Float64Scalar R { get; }

    LinFloat64PolarAngle Theta { get; }

    LinFloat64PolarAngle Phi { get; }

    bool IsUnitVector();

    bool IsNearUnitVector(double zeroEpsilon = 1e-12d);
}