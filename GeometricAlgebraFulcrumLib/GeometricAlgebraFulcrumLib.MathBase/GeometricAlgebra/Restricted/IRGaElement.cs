using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted
{
    public interface IRGaElement : 
        IGeometricElement
    {
        RGaMetric Metric { get; }
    }

    public interface IRGaElement<T> : 
        IScalarAlgebraElement<T>,
        IRGaElement
    {
        RGaProcessor<T> Processor { get; }
    }
}