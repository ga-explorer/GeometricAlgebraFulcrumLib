using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

public interface ILinFloat64Multivector4D :
    IFloat64LinearAlgebraElement,
    IReadOnlyList<Float64Scalar>
{
    Float64Scalar Scalar { get; }

    Float64Scalar Scalar1 { get; }

    Float64Scalar Scalar2 { get; }

    Float64Scalar Scalar3 { get; }

    Float64Scalar Scalar4 { get; }

    Float64Scalar Scalar12 { get; }

    Float64Scalar Scalar13 { get; }

    Float64Scalar Scalar23 { get; }

    Float64Scalar Scalar14 { get; }

    Float64Scalar Scalar24 { get; }

    Float64Scalar Scalar34 { get; }

    Float64Scalar Scalar123 { get; }

    Float64Scalar Scalar124 { get; }

    Float64Scalar Scalar134 { get; }

    Float64Scalar Scalar234 { get; }

    Float64Scalar Scalar1234 { get; }

    bool IsZero();

    bool IsNearZero(double zeroEpsilon = 1e-12d);

    Float64Scalar Norm();

    Float64Scalar NormSquared();

    //Float64Multivector4D ToMultivector4D();
}