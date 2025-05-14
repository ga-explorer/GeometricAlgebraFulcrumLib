using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Frames;

public interface IXGaMultivectorFrame<T> :
    IReadOnlyList<XGaMultivector<T>>,
    IXGaElement<T>
{
    int VSpaceDimensions { get; }
}