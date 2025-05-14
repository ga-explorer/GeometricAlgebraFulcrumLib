using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Processors;

public interface IXGaProcessorContainer<T> :
    IScalarProcessor<T>
{
    XGaProcessor<T> XGaProcessor { get; }

    void AttachXGaProcessor(XGaProcessor<T> processor);
}