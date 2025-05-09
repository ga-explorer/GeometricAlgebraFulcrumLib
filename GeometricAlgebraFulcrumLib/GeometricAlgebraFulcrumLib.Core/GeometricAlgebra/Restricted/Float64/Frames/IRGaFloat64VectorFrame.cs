using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Frames;

public interface IRGaFloat64VectorFrame :
    IReadOnlyList<RGaFloat64Vector>,
    IRGaFloat64Element
{
    int VSpaceDimensions { get; }
}