namespace GeometricAlgebraLib.Storage.GuidedBinaryTraversal
{
    public interface IGaGbtStack3 : IGaGbtStack
    {
        ulong TosId1 { get; }

        ulong TosId2 { get; }

        ulong TosId3 { get; }

        ulong TosChildId10 { get; }

        ulong TosChildId11 { get; }

        ulong TosChildId20 { get; }

        ulong TosChildId21 { get; }

        ulong TosChildId30 { get; }

        ulong TosChildId31 { get; }


        ulong RootId1 { get; }

        ulong RootId2 { get; }

        ulong RootId3 { get; }


        bool TosHasChild10();

        bool TosHasChild11();

        bool TosHasChild20();

        bool TosHasChild21();

        bool TosHasChild30();

        bool TosHasChild31();


        void PushDataOfChild(int childIndex);
    }

    public interface IGaGbtStack3<out T> : IGaGbtStack3
    {
        T TosScalar1 { get; }

        T TosScalar2 { get; }

        T TosScalar3 { get; }
    }
}