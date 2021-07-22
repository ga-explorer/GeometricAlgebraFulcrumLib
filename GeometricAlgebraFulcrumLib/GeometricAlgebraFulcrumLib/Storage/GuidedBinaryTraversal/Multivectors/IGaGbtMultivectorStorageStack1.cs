namespace GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors
{
    public interface IGaGbtMultivectorStorageStack1<T> : IGaGbtStack1<T>
    {
        IGasMultivector<T> Storage { get; }
    }
}