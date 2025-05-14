using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64;

public interface IXGaFloat64Element :
    IFloat64Element,
    IXGaElement
{
    XGaFloat64Processor Processor { get; }
}