using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Frames;

public interface IXGaVectorFrame<T> :
    IReadOnlyList<XGaVector<T>>,
    IXGaElement<T>
{
    int VSpaceDimensions { get; }
}