namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors.GuidedBinaryTraversal;

public interface IRGaGbtStack1 : 
    IRGaGbtStack
{
    ulong TosId { get; }

    ulong TosChildId0 { get; }

    ulong TosChildId1 { get; }

    ulong RootId { get; }

    bool TosHasChild(int childIndex);

    void PushDataOfChild(int childIndex);
}

public interface IRGaGbtStack1<out T> : 
    IRGaGbtStack1
{
    T TosScalar { get; }
}