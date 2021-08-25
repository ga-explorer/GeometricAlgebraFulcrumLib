using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearMaps
{
    public interface IGaUnilinearMap<T>
        : IGaSpaceElement
    {
        IGaScalarsGridProcessor<T> ScalarsGridProcessor { get; }

        IGaStorageMultivector<T> MapMultivector(IGaStorageMultivector<T> mv);
    }
}