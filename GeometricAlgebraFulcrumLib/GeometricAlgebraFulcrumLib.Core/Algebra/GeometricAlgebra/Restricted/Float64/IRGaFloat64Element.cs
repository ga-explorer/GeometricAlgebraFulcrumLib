using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64;

public interface IRGaFloat64Element :
    IRGaElement
{
    RGaFloat64Processor Processor { get; }
}