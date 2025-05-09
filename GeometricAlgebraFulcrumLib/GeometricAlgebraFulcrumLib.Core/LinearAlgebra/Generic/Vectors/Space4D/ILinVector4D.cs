using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space4D;

public interface ILinVector4D<T> :
    ILinearAlgebraElement<T>,
    IQuad<Scalar<T>>
{
    Scalar<T> X { get; }

    Scalar<T> Y { get; }

    Scalar<T> Z { get; }

    Scalar<T> W { get; }
}