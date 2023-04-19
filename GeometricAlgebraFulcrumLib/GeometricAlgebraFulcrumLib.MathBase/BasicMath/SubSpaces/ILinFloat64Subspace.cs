using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.SubSpaces
{
    public interface ILinFloat64Subspace
    {
        int VSpaceDimensions { get; }

        int SubspaceDimensions { get; }

        IEnumerable<LinFloat64Vector> BasisVectors { get; }

        bool NearContains(LinFloat64Vector vector, double epsilon = 1e-12);

        bool NearContains(ILinFloat64Subspace subspace, double epsilon = 1e-12);
    }
}