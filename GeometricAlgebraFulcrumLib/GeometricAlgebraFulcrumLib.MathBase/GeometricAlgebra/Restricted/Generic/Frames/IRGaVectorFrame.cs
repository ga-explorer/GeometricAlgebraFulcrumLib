using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Frames;

public interface IRGaVectorFrame<T> :
    IReadOnlyList<RGaVector<T>>,
    IRGaElement<T>
{
    int VSpaceDimensions { get; }
}