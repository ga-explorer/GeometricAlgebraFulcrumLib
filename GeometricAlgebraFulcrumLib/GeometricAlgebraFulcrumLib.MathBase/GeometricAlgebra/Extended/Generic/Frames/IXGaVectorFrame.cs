using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Frames
{
    public interface IXGaVectorFrame<T> :
        IReadOnlyList<XGaVector<T>>,
        IXGaElement<T>
    {
        int VSpaceDimensions { get; }
    }
}