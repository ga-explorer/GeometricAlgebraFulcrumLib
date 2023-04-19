using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

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