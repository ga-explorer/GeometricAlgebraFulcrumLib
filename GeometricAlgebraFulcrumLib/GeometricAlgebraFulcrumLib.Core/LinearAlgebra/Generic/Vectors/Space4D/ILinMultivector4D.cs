using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space4D;

public interface ILinMultivector4D<T> :
    ILinearAlgebraElement<T>,
    IReadOnlyList<Scalar<T>>
{
    Scalar<T> Scalar { get; }

    Scalar<T> Scalar1 { get; }

    Scalar<T> Scalar2 { get; }

    Scalar<T> Scalar3 { get; }

    Scalar<T> Scalar4 { get; }

    Scalar<T> Scalar12 { get; }

    Scalar<T> Scalar13 { get; }

    Scalar<T> Scalar23 { get; }

    Scalar<T> Scalar14 { get; }

    Scalar<T> Scalar24 { get; }

    Scalar<T> Scalar34 { get; }

    Scalar<T> Scalar123 { get; }

    Scalar<T> Scalar124 { get; }

    Scalar<T> Scalar134 { get; }

    Scalar<T> Scalar234 { get; }

    Scalar<T> Scalar1234 { get; }

    bool IsZero();

    bool IsNearZero();

    Scalar<T> Norm();

    Scalar<T> NormSquared();

    //Float64Multivector4D ToMultivector4D();
}