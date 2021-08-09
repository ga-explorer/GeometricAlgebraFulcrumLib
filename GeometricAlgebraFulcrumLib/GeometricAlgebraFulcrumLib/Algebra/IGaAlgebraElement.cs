using GeometricAlgebraFulcrumLib.Processing.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra
{
    public interface IGaAlgebraElement<T> :
        IGaSpaceElement
    {
        IGaProcessor<T> Processor { get; }
    }
}