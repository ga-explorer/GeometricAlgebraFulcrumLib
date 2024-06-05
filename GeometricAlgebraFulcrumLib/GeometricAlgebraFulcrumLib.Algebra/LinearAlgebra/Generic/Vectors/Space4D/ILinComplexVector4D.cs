using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Algebra.ComplexAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;

public interface ILinComplexVector4D<T> :
    ILinearAlgebraElement<T>,
    IQuad<ComplexNumber<T>>
{
    ComplexNumber<T> X { get; }

    ComplexNumber<T> Y { get; }

    ComplexNumber<T> Z { get; }

    ComplexNumber<T> W { get; }
}