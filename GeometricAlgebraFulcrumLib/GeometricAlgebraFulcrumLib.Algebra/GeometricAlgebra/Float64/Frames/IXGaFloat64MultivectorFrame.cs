using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Frames;

public interface IXGaFloat64MultivectorFrame :
    IReadOnlyList<XGaFloat64Multivector>,
    IXGaFloat64Element
{
    int VSpaceDimensions { get; }
}