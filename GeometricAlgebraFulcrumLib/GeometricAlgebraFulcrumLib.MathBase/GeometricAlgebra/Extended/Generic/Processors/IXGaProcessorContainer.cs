using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

public interface IXGaProcessorContainer<T> :
    IScalarProcessor<T>
{
    XGaProcessor<T> XGaProcessor { get; }

    void AttachXGaProcessor(XGaProcessor<T> processor);
}