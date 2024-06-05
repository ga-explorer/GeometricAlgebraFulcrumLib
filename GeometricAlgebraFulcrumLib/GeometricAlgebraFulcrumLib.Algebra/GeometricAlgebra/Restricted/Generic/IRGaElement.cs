using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic;

public interface IRGaElement<T> : 
    IScalarAlgebraElement<T>,
    IRGaElement
{
    RGaProcessor<T> Processor { get; }
}