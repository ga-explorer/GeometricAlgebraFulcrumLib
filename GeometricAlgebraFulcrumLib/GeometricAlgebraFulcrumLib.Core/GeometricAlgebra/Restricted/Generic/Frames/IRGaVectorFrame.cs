using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Frames;

public interface IRGaVectorFrame<T> :
    IReadOnlyList<RGaVector<T>>,
    IRGaElement<T>
{
    int VSpaceDimensions { get; }
}