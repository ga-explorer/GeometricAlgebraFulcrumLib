using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic;

public interface IXGaElement<T> : 
    IScalarAlgebraElement<T>,
    IXGaElement
{
    XGaProcessor<T> Processor { get; }
}