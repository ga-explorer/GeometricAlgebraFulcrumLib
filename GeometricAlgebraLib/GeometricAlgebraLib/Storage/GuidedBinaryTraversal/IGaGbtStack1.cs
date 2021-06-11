namespace GeometricAlgebraLib.Storage.GuidedBinaryTraversal
{
    public interface IGaGbtStack1 : IGaGbtStack
    {
        ulong TosId { get; }

        ulong TosChildId0 { get; }

        ulong TosChildId1 { get; }

        ulong RootId { get; }

        bool TosHasChild(int childIndex);

        void PushDataOfChild(int childIndex);
    }

    public interface IGaGbtStack1<out T> : IGaGbtStack1
    {
        T TosScalar { get; }
    }
}