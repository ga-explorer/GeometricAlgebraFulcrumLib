using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra
{
    public interface IGeometricAlgebraElement<T> :
        IGaSpaceElement
    {
        IGaProcessor<T> Processor { get; }
    }
}