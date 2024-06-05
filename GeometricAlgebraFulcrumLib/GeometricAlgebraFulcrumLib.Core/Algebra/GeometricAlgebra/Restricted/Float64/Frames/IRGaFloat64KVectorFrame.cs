using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Frames;

public interface IRGaFloat64KVectorFrame :
    IReadOnlyList<RGaFloat64KVector>,
    IRGaFloat64Element
{
    int VSpaceDimensions { get; }
}