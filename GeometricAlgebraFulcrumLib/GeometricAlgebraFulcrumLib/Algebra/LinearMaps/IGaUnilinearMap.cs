using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearMaps
{
    public interface IGaUnilinearMap<T>
        : IGaSpace
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        IGasMultivector<T> MapMultivector(IGasMultivector<T> mv);
    }
}