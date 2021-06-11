namespace GeometricAlgebraLib.Storage.GuidedBinaryTraversal.Multivectors
{
    public interface IGaGbtMultivectorStorageStack1<T> : IGaGbtStack1<T>
    {
        IGaMultivectorStorage<T> Storage { get; }
    }
}