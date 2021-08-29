using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearMaps
{
    public interface IGaUnilinearMap<T>
        : IGaSpaceElement
    {
        ILaProcessor<T> ScalarsGridProcessor { get; }

        IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> mv);
    }
}