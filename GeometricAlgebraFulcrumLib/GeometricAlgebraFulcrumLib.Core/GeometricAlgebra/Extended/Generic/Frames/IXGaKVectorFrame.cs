using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Frames;

public interface IXGaKVectorFrame<T> :
    IReadOnlyList<XGaKVector<T>>,
    IXGaElement<T>
{
    int VSpaceDimensions { get; }
}