using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.SubSpaces.Space4D
{
    public interface ILinFloat64Subspace4D :
        ILinearElement
    {
        int SubspaceDimensions { get; }

        IEnumerable<Float64Vector4D> BasisVectors { get; }

        bool NearContains(IFloat64Vector4D vector, double epsilon = 1e-12);

        bool NearContains(ILinFloat64Subspace4D subspace, double epsilon = 1e-12);
    }
}