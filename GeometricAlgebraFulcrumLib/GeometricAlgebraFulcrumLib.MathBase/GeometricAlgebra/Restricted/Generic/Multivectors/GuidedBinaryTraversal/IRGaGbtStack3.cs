namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.GuidedBinaryTraversal;

public interface IRGaGbtStack3 : 
    IRGaGbtStack
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

public interface IRGaGbtStack3<out T> : 
    IRGaGbtStack3
{
    T TosScalar1 { get; }

    T TosScalar2 { get; }

    T TosScalar3 { get; }
}