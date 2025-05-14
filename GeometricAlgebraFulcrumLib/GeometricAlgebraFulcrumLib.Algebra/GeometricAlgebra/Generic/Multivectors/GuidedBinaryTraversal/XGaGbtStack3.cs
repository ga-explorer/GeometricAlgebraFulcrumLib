using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal;

public abstract class XGaGbtStack3 : 
    XGaGbtStack, 
    IXGaGbtStack3
{
    public IXGaGbtStack1 Stack1 { get; }

    public IXGaGbtStack1 Stack2 { get; }

    public IXGaGbtStack1 Stack3 { get; }


    public IndexSet TosId1
        => Stack1.TosId;

    public IndexSet TosId2
        => Stack2.TosId;

    public IndexSet TosId3
        => Stack3.TosId;


    public IndexSet TosChildId10
        => Stack1.TosChildId0;

    public IndexSet TosChildId11
        => Stack1.TosChildId1;

    public IndexSet TosChildId20
        => Stack2.TosChildId0;

    public IndexSet TosChildId21
        => Stack2.TosChildId1;

    public IndexSet TosChildId30
        => Stack3.TosChildId0;

    public IndexSet TosChildId31
        => Stack3.TosChildId1;


    public IndexSet RootId1
        => Stack1.RootId;

    public IndexSet RootId2
        => Stack2.RootId;

    public IndexSet RootId3
        => Stack3.RootId;


    protected XGaGbtStack3(IXGaGbtStack1 stack1, IXGaGbtStack1 stack2, IXGaGbtStack1 stack3)
        : base(stack1.Capacity, stack1.RootTreeDepth)
    {
        Debug.Assert(
            stack1.Capacity == stack2.Capacity && stack2.Capacity == stack3.Capacity &&
            stack1.RootTreeDepth == stack2.RootTreeDepth && stack2.RootTreeDepth == stack3.RootTreeDepth
        );

        Stack1 = stack1;
        Stack2 = stack2;
        Stack3 = stack3;
    }


    public override void PushRootData()
    {
        TosIndex = 0;

        TreeDepthArray[TosIndex] = RootTreeDepth;

        Stack1.PushRootData();
        Stack2.PushRootData();
        Stack3.PushRootData();
    }

    public override void PopNodeData()
    {
        Stack1.PopNodeData();
        Stack2.PopNodeData();
        Stack3.PopNodeData();

        TosTreeDepth = TreeDepthArray[TosIndex];

        //Console.Out.WriteLine($"depth:{TosTreeDepth}, id1: {TosId1}, id2: {TosId2}, id3: {TosId3}");

        TosIndex--;
    }


    public bool TosHasChild10()
    {
        return Stack1.TosHasChild(0);
    }

    public bool TosHasChild11()
    {
        return Stack1.TosHasChild(1);
    }

    public bool TosHasChild20()
    {
        return Stack2.TosHasChild(0);
    }

    public bool TosHasChild21()
    {
        return Stack2.TosHasChild(1);
    }

    public bool TosHasChild30()
    {
        return Stack3.TosHasChild(0);
    }

    public bool TosHasChild31()
    {
        return Stack3.TosHasChild(1);
    }


    public void PushDataOfChild(int childIndex)
    {
        TosIndex++;

        TreeDepthArray[TosIndex] = TosTreeDepth - 1;

        Stack1.PushDataOfChild(childIndex & 1);
        Stack2.PushDataOfChild(childIndex >> 1 & 1);
        Stack3.PushDataOfChild(childIndex >> 2 & 1);
    }
}