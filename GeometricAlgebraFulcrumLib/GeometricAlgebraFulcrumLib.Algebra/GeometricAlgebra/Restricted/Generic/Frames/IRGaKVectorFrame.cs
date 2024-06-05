using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Frames;

public interface IRGaKVectorFrame<T> :
    IReadOnlyList<RGaKVector<T>>,
    IRGaElement<T>
{
    int VSpaceDimensions { get; }
}