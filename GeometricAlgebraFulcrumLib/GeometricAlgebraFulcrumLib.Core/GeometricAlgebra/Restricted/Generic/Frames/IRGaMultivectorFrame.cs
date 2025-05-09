using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Frames;

public interface IRGaMultivectorFrame<T> :
    IReadOnlyList<RGaMultivector<T>>,
    IRGaElement<T>
{
    int VSpaceDimensions { get; }
}