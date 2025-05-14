using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Frames;

public interface IXGaMultivectorFrame<T> :
    IReadOnlyList<XGaMultivector<T>>,
    IXGaElement<T>
{
    int VSpaceDimensions { get; }
}