using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64;

public interface IRGaFloat64Element :
    IRGaElement
{
    RGaFloat64Processor Processor { get; }
}