using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Frames;

public interface IXGaVectorFrame<T> :
    IReadOnlyList<XGaVector<T>>,
    IXGaElement<T>
{
    int VSpaceDimensions { get; }
}