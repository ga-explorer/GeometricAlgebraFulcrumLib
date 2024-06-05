using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;

public interface IRGaProcessorContainer<T> :
    IScalarProcessor<T>
{
    RGaProcessor<T> RGaProcessor { get; }

    void AttachRGaProcessor(RGaProcessor<T> processor);
}