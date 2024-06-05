using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64;

public interface IXGaFloat64Element :
    IFloat64Element,
    IXGaElement
{
    XGaFloat64Processor Processor { get; }
}