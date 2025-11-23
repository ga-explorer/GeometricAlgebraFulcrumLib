using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.Space3D;

public interface ILinFloat64Subspace3D :
    IFloat64LinearAlgebraElement
{
    int SubspaceDimensions { get; }

    IEnumerable<LinFloat64Vector3D> BasisVectors { get; }

    LinFloat64Vector3D GetVectorProjection(LinFloat64Vector3D vector);

    LinFloat64Vector3D GetVectorRejection(LinFloat64Vector3D vector);

    LinFloat64PolarAngle GetVectorProjectionPolarAngle(LinFloat64Vector3D vector);

    bool NearContains(LinFloat64Vector3D vector, double tolerance = Float64Utils.DefaultTolerance);

    bool NearContains(ILinFloat64Subspace3D subspace, double tolerance = Float64Utils.DefaultTolerance);
}