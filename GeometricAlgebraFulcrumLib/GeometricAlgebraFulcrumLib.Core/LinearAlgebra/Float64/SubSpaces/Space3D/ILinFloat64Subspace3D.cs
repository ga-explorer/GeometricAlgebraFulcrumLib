using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.SubSpaces.Space3D;

public interface ILinFloat64Subspace3D :
    IFloat64LinearAlgebraElement
{
    int SubspaceDimensions { get; }

    IEnumerable<LinFloat64Vector3D> BasisVectors { get; }

    LinFloat64Vector3D GetVectorProjection(ILinFloat64Vector3D vector);

    LinFloat64Vector3D GetVectorRejection(ILinFloat64Vector3D vector);

    LinFloat64PolarAngle GetVectorProjectionPolarAngle(ILinFloat64Vector3D vector);

    bool NearContains(ILinFloat64Vector3D vector, double zeroEpsilon = Float64Utils.ZeroEpsilon);

    bool NearContains(ILinFloat64Subspace3D subspace, double zeroEpsilon = Float64Utils.ZeroEpsilon);
}