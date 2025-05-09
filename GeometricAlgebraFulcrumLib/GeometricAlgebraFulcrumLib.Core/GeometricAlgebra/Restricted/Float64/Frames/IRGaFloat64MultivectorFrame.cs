using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Frames;

public interface IRGaFloat64MultivectorFrame :
    IReadOnlyList<RGaFloat64Multivector>,
    IRGaFloat64Element
{
    int VSpaceDimensions { get; }
}