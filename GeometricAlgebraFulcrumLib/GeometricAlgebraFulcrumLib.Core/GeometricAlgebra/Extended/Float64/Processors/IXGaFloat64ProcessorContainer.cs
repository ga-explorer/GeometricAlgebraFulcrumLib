namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Processors;

public interface IXGaFloat64ProcessorContainer
{
    XGaFloat64Processor XGaProcessor { get; }

    void AttachXGaProcessor(XGaFloat64Processor processor);
}