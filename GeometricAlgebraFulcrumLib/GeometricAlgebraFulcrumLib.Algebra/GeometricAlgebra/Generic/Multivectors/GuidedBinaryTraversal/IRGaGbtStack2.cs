using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal;

public interface IXGaGbtStack2 : 
    IXGaGbtStack
{
    IndexSet TosId1 { get; }

    IndexSet TosId2 { get; }

    IndexSet TosChildId10 { get; }

    IndexSet TosChildId11 { get; }

    IndexSet TosChildId20 { get; }

    IndexSet TosChildId21 { get; }


    IndexSet RootId1 { get; }

    IndexSet RootId2 { get; }


    bool TosHasChild10();

    bool TosHasChild11();

    bool TosHasChild20();

    bool TosHasChild21();


    void PushDataOfChild(int childIndex);
}

public interface IXGaGbtStack2<out T> : 
    IXGaGbtStack2
{
    T TosScalar1 { get; }

    T TosScalar2 { get; }
}