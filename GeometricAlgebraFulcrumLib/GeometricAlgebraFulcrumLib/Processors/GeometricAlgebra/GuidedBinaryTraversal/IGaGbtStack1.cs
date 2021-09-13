namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal
{
    public interface IGeoGbtStack1 : IGeoGbtStack
    {
        ulong TosId { get; }

        ulong TosChildId0 { get; }

        ulong TosChildId1 { get; }

        ulong RootId { get; }

        bool TosHasChild(int childIndex);

        void PushDataOfChild(int childIndex);
    }

    public interface IGeoGbtStack1<out T> : IGeoGbtStack1
    {
        T TosScalar { get; }
    }
}