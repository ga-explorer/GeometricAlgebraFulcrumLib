using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic;

public interface IXGaElement<T> : 
    IScalarAlgebraElement<T>,
    IXGaElement
{
    XGaProcessor<T> Processor { get; }
}