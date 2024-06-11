using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic;

public interface IRGaElement<T> : 
    IScalarAlgebraElement<T>,
    IRGaElement
{
    RGaProcessor<T> Processor { get; }
}