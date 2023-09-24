using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

public interface IFloat64Multivector3D :
    ILinearElement,
    IReadOnlyList<Float64Scalar>
{
    Float64Scalar Scalar { get; }

    Float64Scalar Scalar1 { get; }

    Float64Scalar Scalar2 { get; }

    Float64Scalar Scalar3 { get; }

    Float64Scalar Scalar12 { get; }

    Float64Scalar Scalar13 { get; }
        
    Float64Scalar Scalar23 { get; }

    Float64Scalar Scalar123 { get; }

    bool IsZero();

    bool IsNearZero(double epsilon = 1e-12d);

    Float64Scalar Norm();

    Float64Scalar NormSquared();

    Float64Multivector3D ToMultivector3D();
}