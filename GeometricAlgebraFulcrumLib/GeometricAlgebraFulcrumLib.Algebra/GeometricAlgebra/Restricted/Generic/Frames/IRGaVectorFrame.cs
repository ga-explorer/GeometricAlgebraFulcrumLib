using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Frames;

public interface IRGaVectorFrame<T> :
    IReadOnlyList<RGaVector<T>>,
    IRGaElement<T>
{
    int VSpaceDimensions { get; }
}