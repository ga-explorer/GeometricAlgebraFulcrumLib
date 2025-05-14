using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Frames;

public interface IXGaVectorFrame<T> :
    IReadOnlyList<XGaVector<T>>,
    IXGaElement<T>
{
    int VSpaceDimensions { get; }
}