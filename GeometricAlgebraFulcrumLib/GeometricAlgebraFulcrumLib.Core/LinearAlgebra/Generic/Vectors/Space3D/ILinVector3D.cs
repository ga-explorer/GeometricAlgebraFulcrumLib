using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space3D;

public interface ILinVector3D<T> :
    ILinearAlgebraElement<T>,
    ITriplet<Scalar<T>>
{
    Scalar<T> X { get; }

    Scalar<T> Y { get; }

    Scalar<T> Z { get; }

    LinVector3D<T> ToVector3D();
}