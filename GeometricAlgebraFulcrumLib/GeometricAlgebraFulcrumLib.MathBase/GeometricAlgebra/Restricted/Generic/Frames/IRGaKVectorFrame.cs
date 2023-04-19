using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Frames
{
    public interface IRGaKVectorFrame<T> :
        IReadOnlyList<RGaKVector<T>>,
        IRGaElement<T>
    {
        int VSpaceDimensions { get; }
    }
}