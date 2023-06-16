using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

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