using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Processors;

public interface IRGaProcessorContainer<T> :
    IScalarProcessor<T>
{
    RGaProcessor<T> RGaProcessor { get; }

    void AttachRGaProcessor(RGaProcessor<T> processor);
}