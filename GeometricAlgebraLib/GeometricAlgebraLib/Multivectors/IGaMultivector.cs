using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Multivectors
{
    public interface IGaMultivector<T>
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        IGaMultivectorStorage<T> Storage { get; }
    }
}