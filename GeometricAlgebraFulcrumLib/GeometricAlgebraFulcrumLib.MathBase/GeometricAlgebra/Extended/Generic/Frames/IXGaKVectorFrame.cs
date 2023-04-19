using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Frames
{
    public interface IXGaKVectorFrame<T> :
        IReadOnlyList<XGaKVector<T>>,
        IXGaElement<T>
    {
        int VSpaceDimensions { get; }
    }
}