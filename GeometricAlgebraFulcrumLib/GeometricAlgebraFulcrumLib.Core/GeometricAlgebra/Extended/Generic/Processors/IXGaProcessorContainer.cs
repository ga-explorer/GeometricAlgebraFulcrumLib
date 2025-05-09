using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Processors;

public interface IXGaProcessorContainer<T> :
    IScalarProcessor<T>
{
    XGaProcessor<T> XGaProcessor { get; }

    void AttachXGaProcessor(XGaProcessor<T> processor);
}