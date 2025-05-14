using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;

public interface IXGaProcessorContainer<T> :
    IScalarProcessor<T>
{
    XGaProcessor<T> XGaProcessor { get; }

    void AttachXGaProcessor(XGaProcessor<T> processor);
}