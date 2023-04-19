using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted;

public interface IRGaFloat64Element : 
    IRGaElement
{
    RGaFloat64Processor Processor { get; }
}