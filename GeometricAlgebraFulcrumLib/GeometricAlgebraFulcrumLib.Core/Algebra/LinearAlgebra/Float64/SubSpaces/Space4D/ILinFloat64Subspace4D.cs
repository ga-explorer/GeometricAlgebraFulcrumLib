using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.SubSpaces.Space4D;

public interface ILinFloat64Subspace4D :
    IFloat64LinearAlgebraElement
{
    int SubspaceDimensions { get; }

    IEnumerable<LinFloat64Vector4D> BasisVectors { get; }

    bool NearContains(ILinFloat64Vector4D vector, double epsilon = 1e-12);

    bool NearContains(ILinFloat64Subspace4D subspace, double epsilon = 1e-12);
}