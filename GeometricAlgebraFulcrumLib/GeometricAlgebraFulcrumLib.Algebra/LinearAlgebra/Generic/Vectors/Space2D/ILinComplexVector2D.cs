using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;

public interface ILinComplexVector2D<T> :
    ILinearAlgebraElement<T>,
    IPair<ComplexNumber<T>>
{
    ComplexNumber<T> X { get; }

    ComplexNumber<T> Y { get; }
}