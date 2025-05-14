using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Frames;

public interface IXGaKVectorFrame<T> :
    IReadOnlyList<XGaKVector<T>>,
    IXGaElement<T>
{
    int VSpaceDimensions { get; }
}