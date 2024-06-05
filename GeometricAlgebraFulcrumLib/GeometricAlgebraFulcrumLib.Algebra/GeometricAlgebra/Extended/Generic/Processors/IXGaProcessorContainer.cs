using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;

public interface IXGaProcessorContainer<T> :
    IScalarProcessor<T>
{
    XGaProcessor<T> XGaProcessor { get; }

    void AttachXGaProcessor(XGaProcessor<T> processor);
}