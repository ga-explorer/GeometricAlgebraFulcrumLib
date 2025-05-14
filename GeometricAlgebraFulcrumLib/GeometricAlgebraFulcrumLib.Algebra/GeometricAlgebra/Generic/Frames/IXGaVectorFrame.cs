using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Frames;

public interface IXGaVectorFrame<T> :
    IReadOnlyList<XGaVector<T>>,
    IXGaElement<T>
{
    int VSpaceDimensions { get; }
}