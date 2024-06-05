using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Frames;

public interface IRGaFloat64MultivectorFrame :
    IReadOnlyList<RGaFloat64Multivector>,
    IRGaFloat64Element
{
    int VSpaceDimensions { get; }
}