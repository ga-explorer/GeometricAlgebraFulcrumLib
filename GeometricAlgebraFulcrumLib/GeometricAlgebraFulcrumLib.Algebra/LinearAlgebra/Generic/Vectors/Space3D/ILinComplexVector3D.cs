using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

public interface ILinComplexVector3D<T> :
    ILinearAlgebraElement<T>,
    ITriplet<ComplexNumber<T>>
{
    ComplexNumber<T> X { get; }

    ComplexNumber<T> Y { get; }

    ComplexNumber<T> Z { get; }
}