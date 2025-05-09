using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Frames;

public interface IXGaMultivectorFrame<T> :
    IReadOnlyList<XGaMultivector<T>>,
    IXGaElement<T>
{
    int VSpaceDimensions { get; }
}