using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64;

public interface IXGaFloat64Element :
    IFloat64Element,
    IXGaElement
{
    XGaFloat64Processor Processor { get; }
}