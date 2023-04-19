using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

public interface IRGaProcessorContainer<T> :
    IScalarProcessor<T>
{
    RGaProcessor<T> RGaProcessor { get; }

    void AttachRGaProcessor(RGaProcessor<T> processor);
}