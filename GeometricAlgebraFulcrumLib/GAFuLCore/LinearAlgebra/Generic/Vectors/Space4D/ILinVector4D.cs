using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;

public interface ILinVector4D<T> :
    ILinearAlgebraElement<T>,
    IQuad<Scalar<T>>
{
    Scalar<T> X { get; }

    Scalar<T> Y { get; }

    Scalar<T> Z { get; }

    Scalar<T> W { get; }
}