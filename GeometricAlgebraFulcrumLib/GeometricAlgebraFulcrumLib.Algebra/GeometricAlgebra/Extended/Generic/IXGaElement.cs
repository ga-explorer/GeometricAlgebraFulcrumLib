using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic;

public interface IXGaElement<T> : 
    IScalarAlgebraElement<T>,
    IXGaElement
{
    XGaProcessor<T> Processor { get; }
}