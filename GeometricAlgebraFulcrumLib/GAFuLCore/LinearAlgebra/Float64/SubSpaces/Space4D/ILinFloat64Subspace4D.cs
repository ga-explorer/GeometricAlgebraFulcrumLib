using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.Space4D;

public interface ILinFloat64Subspace4D :
    IFloat64LinearAlgebraElement
{
    int SubspaceDimensions { get; }

    IEnumerable<LinFloat64Vector4D> BasisVectors { get; }

    bool NearContains(ILinFloat64Vector4D vector, double zeroEpsilon = Float64Utils.ZeroEpsilon);

    bool NearContains(ILinFloat64Subspace4D subspace, double zeroEpsilon = Float64Utils.ZeroEpsilon);
}