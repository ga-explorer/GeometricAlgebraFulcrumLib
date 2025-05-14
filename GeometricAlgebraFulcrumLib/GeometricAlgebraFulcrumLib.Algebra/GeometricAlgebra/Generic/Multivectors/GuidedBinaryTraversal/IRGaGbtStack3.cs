using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal;

public interface IXGaGbtStack3 : 
    IXGaGbtStack
{
    IndexSet TosId1 { get; }

    IndexSet TosId2 { get; }

    IndexSet TosId3 { get; }

    IndexSet TosChildId10 { get; }

    IndexSet TosChildId11 { get; }

    IndexSet TosChildId20 { get; }

    IndexSet TosChildId21 { get; }

    IndexSet TosChildId30 { get; }

    IndexSet TosChildId31 { get; }


    IndexSet RootId1 { get; }

    IndexSet RootId2 { get; }

    IndexSet RootId3 { get; }


    bool TosHasChild10();

    bool TosHasChild11();

    bool TosHasChild20();

    bool TosHasChild21();

    bool TosHasChild30();

    bool TosHasChild31();


    void PushDataOfChild(int childIndex);
}

public interface IXGaGbtStack3<out T> : 
    IXGaGbtStack3
{
    T TosScalar1 { get; }

    T TosScalar2 { get; }

    T TosScalar3 { get; }
}