using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Frames
{
    public interface IRGaMultivectorFrame<T> :
        IReadOnlyList<RGaMultivector<T>>,
        IRGaElement<T>
    {
        int VSpaceDimensions { get; }
    }
}