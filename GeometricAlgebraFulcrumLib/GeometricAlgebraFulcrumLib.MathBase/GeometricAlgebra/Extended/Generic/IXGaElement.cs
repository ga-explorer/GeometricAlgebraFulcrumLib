using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic;

public interface IXGaElement<T> : 
    IScalarAlgebraElement<T>,
    IXGaElement
{
    XGaProcessor<T> Processor { get; }
}