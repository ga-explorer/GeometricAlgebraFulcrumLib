using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space3D;

public interface ILinMultivector3D<T> :
    ILinearAlgebraElement<T>,
    IReadOnlyList<Scalar<T>>
{
    Scalar<T> Scalar { get; }

    Scalar<T> Scalar1 { get; }

    Scalar<T> Scalar2 { get; }

    Scalar<T> Scalar3 { get; }

    Scalar<T> Scalar12 { get; }

    Scalar<T> Scalar13 { get; }

    Scalar<T> Scalar23 { get; }

    Scalar<T> Scalar123 { get; }

    bool IsZero();

    bool IsNearZero();

    Scalar<T> Norm();

    Scalar<T> NormSquared();

    LinMultivector3D<T> ToMultivector3D();
}