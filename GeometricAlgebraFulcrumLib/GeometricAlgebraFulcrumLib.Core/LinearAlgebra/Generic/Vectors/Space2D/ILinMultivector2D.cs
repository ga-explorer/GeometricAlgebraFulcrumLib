using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space2D;

public interface ILinMultivector2D<T> :
    ILinearAlgebraElement<T>,
    IReadOnlyList<Scalar<T>>
{
    Scalar<T> Scalar { get; }

    Scalar<T> Scalar1 { get; }

    Scalar<T> Scalar2 { get; }

    Scalar<T> Scalar12 { get; }

    bool IsZero();

    bool IsNearZero();

    Scalar<T> Norm();

    Scalar<T> NormSquared();

    LinMultivector2D<T> ToMultivector2D();
}