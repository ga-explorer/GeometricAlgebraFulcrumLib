using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Frames;

public interface IXGaKVectorFrame<T> :
    IReadOnlyList<XGaKVector<T>>,
    IXGaElement<T>
{
    int VSpaceDimensions { get; }
}