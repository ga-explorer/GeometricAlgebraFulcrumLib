using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space2D;

public interface ILinVector2D<T> :
    ILinearAlgebraElement<T>,
    IPair<Scalar<T>>
{
    Scalar<T> X { get; }

    Scalar<T> Y { get; }
}