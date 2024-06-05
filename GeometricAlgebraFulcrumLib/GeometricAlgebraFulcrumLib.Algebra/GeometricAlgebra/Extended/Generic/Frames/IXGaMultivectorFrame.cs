using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Frames;

public interface IXGaMultivectorFrame<T> :
    IReadOnlyList<XGaMultivector<T>>,
    IXGaElement<T>
{
    int VSpaceDimensions { get; }
}