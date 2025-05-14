using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Frames;

public interface IXGaFloat64KVectorFrame :
    IReadOnlyList<XGaFloat64KVector>,
    IXGaFloat64Element
{
    int VSpaceDimensions { get; }
}