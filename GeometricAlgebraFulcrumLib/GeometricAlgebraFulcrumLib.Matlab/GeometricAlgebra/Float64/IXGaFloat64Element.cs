using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64;

public interface IXGaFloat64Element :
    IFloat64Element,
    IXGaElement
{
    XGaFloat64Processor Processor { get; }
}