using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Frames;

public interface IXGaMultivectorFrame<T> :
    IReadOnlyList<XGaMultivector<T>>,
    IXGaElement<T>
{
    int VSpaceDimensions { get; }
}