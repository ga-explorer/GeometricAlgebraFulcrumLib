using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic
{
    public interface IRGaElement<T> : 
        IScalarAlgebraElement<T>,
        IRGaElement
    {
        RGaProcessor<T> Processor { get; }
    }
}
