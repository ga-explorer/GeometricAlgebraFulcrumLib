namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

public interface IRGaFloat64ProcessorContainer
{
    RGaFloat64Processor RGaProcessor { get; }

    void AttachRGaProcessor(RGaFloat64Processor processor);
}