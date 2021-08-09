using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors
{
    public interface IGaGbtMultivectorStorageStack1<T> : 
        IGaGbtStack1<T>
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        IGaStorageMultivector<T> Storage { get; }
    }
}