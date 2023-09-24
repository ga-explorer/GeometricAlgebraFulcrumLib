using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Frames
{
    public interface IXGaFloat64KVectorFrame :
        IReadOnlyList<XGaFloat64KVector>,
        IXGaFloat64Element
    {
        int VSpaceDimensions { get; }
    }
}