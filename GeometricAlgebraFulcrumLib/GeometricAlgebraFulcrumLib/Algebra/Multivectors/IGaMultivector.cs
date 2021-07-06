using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors
{
    public interface IGaMultivector<T>
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        IGaMultivectorStorage<T> Storage { get; }
    }
}