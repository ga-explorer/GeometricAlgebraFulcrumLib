using GeometricAlgebraFulcrumLib.Core.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space2D;

public interface ILinComplexVector2D<T> :
    ILinearAlgebraElement<T>,
    IPair<ComplexNumber<T>>
{
    ComplexNumber<T> X { get; }

    ComplexNumber<T> Y { get; }
}