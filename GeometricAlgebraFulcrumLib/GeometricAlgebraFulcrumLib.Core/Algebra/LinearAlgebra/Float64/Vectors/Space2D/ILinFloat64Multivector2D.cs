using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

public interface ILinFloat64Multivector2D :
    IFloat64LinearAlgebraElement,
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

    LinFloat64Multivector2D ToMultivector2D();
}