using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.SubSpaces.Space3D;

public interface ILinFloat64Subspace3D :
    ILinearElement
{
    int SubspaceDimensions { get; }

    IEnumerable<Float64Vector3D> BasisVectors { get; }
        
    Float64Vector3D GetVectorProjection(IFloat64Vector3D vector);

    Float64Vector3D GetVectorRejection(IFloat64Vector3D vector);

    Float64PlanarAngle GetVectorProjectionPolarAngle(IFloat64Vector3D vector);

    bool NearContains(IFloat64Vector3D vector, double epsilon = 1e-12);

    bool NearContains(ILinFloat64Subspace3D subspace, double epsilon = 1e-12);
}