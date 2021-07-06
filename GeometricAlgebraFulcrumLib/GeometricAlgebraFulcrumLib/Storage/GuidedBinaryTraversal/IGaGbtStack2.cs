namespace GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal
{
    public interface IGaGbtStack2 : IGaGbtStack
    {
        ulong TosId1 { get; }

        ulong TosId2 { get; }

        ulong TosChildId10 { get; }

        ulong TosChildId11 { get; }

        ulong TosChildId20 { get; }

        ulong TosChildId21 { get; }


        ulong RootId1 { get; }

        ulong RootId2 { get; }


        bool TosHasChild10();

        bool TosHasChild11();

        bool TosHasChild20();

        bool TosHasChild21();


        void PushDataOfChild(int childIndex);
    }

    public interface IGaGbtStack2<out T> : IGaGbtStack2
    {
        T TosScalar1 { get; }

        T TosScalar2 { get; }
    }
}