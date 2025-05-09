using GeometricAlgebraFulcrumLib.Core.ComplexAlgebra;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space3D;

public interface ILinComplexVector3D<T> :
    ILinearAlgebraElement<T>,
    ITriplet<ComplexNumber<T>>
{
    ComplexNumber<T> X { get; }

    ComplexNumber<T> Y { get; }

    ComplexNumber<T> Z { get; }
}