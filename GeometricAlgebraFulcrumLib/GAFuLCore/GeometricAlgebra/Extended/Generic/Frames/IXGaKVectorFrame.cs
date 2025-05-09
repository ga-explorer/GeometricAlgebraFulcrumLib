using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Frames;

public interface IXGaKVectorFrame<T> :
    IReadOnlyList<XGaKVector<T>>,
    IXGaElement<T>
{
    int VSpaceDimensions { get; }
}