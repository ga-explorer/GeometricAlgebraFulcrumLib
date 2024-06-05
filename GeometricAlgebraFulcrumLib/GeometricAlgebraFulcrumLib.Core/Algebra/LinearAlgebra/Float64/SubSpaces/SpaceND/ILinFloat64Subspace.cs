using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.SubSpaces.SpaceND;

public interface ILinFloat64Subspace :
    IFloat64LinearAlgebraElement
{
    int SubspaceDimensions { get; }

    IEnumerable<LinFloat64Vector> BasisVectors { get; }

    LinFloat64Vector GetVectorProjection(LinFloat64Vector vector);

    LinFloat64PolarAngle GetVectorProjectionPolarAngle(LinFloat64Vector vector);

    LinFloat64Vector GetVectorRejection(LinFloat64Vector vector);

    bool NearContains(LinFloat64Vector vector, double epsilon = 1e-12);

    bool NearContains(ILinFloat64Subspace subspace, double epsilon = 1e-12);
}