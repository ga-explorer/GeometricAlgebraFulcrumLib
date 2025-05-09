using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;

public interface ILinVector2D<T> :
    ILinearAlgebraElement<T>,
    IPair<Scalar<T>>
{
    Scalar<T> X { get; }

    Scalar<T> Y { get; }
}