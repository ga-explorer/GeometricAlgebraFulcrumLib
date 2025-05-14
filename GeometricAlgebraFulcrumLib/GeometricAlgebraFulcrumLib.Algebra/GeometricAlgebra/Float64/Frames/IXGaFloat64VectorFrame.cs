using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Frames;

public interface IXGaFloat64VectorFrame :
    IReadOnlyList<XGaFloat64Vector>,
    IXGaFloat64Element
{
    int VSpaceDimensions { get; }
}