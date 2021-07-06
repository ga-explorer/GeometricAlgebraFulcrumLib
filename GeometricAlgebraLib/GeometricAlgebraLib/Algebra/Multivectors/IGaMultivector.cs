using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Algebra.Multivectors
{
    public interface IGaMultivector<T>
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        IGaMultivectorStorage<T> Storage { get; }
    }
}