using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal;

public interface IXGaGbtStack1 : 
    IXGaGbtStack
{
    IndexSet TosId { get; }

    IndexSet TosChildId0 { get; }

    IndexSet TosChildId1 { get; }

    IndexSet RootId { get; }

    bool TosHasChild(int childIndex);

    void PushDataOfChild(int childIndex);
}

public interface IXGaGbtStack1<out T> : 
    IXGaGbtStack1
{
    T TosScalar { get; }
}