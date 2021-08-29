using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors
{
    public interface IGaGbtMultivectorStorageStack1<T> : 
        IGaGbtStack1<T>
    {
        IScalarProcessor<T> ScalarProcessor { get; }

        IGaMultivectorStorage<T> Storage { get; }
    }
}