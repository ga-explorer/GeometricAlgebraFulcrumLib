using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.SubSpaces.SpaceND
{
    public interface ILinFloat64Subspace :
        ILinearElement
    {
        int SubspaceDimensions { get; }

        IEnumerable<Float64Vector> BasisVectors { get; }
        
        Float64Vector GetVectorProjection(Float64Vector vector);

        Float64PlanarAngle GetVectorProjectionPolarAngle(Float64Vector vector);

        Float64Vector GetVectorRejection(Float64Vector vector);

        bool NearContains(Float64Vector vector, double epsilon = 1e-12);

        bool NearContains(ILinFloat64Subspace subspace, double epsilon = 1e-12);
    }
}