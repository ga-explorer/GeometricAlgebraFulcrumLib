using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Frames;

public interface IRGaKVectorFrame<T> :
    IReadOnlyList<RGaKVector<T>>,
    IRGaElement<T>
{
    int VSpaceDimensions { get; }
}