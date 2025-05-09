using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic;

public interface IRGaElement<T> : 
    IScalarAlgebraElement<T>,
    IRGaElement
{
    RGaProcessor<T> Processor { get; }
}