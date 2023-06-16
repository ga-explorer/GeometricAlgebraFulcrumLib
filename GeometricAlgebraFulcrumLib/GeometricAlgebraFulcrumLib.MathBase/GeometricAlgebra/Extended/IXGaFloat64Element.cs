using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended;

public interface IXGaFloat64Element : 
    IFloat64Element,
    IXGaElement
{
    XGaFloat64Processor Processor { get; }
}