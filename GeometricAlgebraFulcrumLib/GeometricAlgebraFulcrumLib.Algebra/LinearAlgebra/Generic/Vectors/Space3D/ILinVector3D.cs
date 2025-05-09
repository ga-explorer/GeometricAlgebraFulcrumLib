using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

public interface ILinVector3D<T> :
    ILinearAlgebraElement<T>,
    ITriplet<Scalar<T>>
{
    Scalar<T> X { get; }

    Scalar<T> Y { get; }

    Scalar<T> Z { get; }

    LinVector3D<T> ToVector3D();
}