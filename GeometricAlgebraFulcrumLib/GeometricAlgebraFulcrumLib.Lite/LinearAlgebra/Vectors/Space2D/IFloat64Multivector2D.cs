using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

public interface IFloat64Multivector2D :
    ILinearElement,
    IReadOnlyList<Float64Scalar>
{
    Float64Scalar Scalar { get; }

    Float64Scalar Scalar1 { get; }

    Float64Scalar Scalar2 { get; }
    
    Float64Scalar Scalar12 { get; }
    
    bool IsZero();

    bool IsNearZero(double epsilon = 1e-12d);

    Float64Scalar Norm();

    Float64Scalar NormSquared();

    Float64Multivector2D ToMultivector2D();
}