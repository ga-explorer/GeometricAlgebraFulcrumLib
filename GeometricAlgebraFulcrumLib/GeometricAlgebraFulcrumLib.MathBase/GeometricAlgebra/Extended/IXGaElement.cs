using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended
{
    public interface IXGaElement : 
        IGeometricElement
    {
        XGaMetric Metric { get; }
    }

    public interface IXGaElement<T> : 
        IScalarAlgebraElement<T>,
        IXGaElement
    {
        XGaProcessor<T> Processor { get; }
    }
}