﻿using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal;

public abstract class XGaGbtStack2 : 
    XGaGbtStack, 
    IXGaGbtStack2
{
    public IXGaGbtStack1 Stack1 { get; }

    public IXGaGbtStack1 Stack2 { get; }


    public IndexSet TosId1
        => Stack1.TosId;

    public IndexSet TosId2
        => Stack2.TosId;


    public IndexSet TosChildId10
        => Stack1.TosChildId0;

    public IndexSet TosChildId11
        => Stack1.TosChildId1;

    public IndexSet TosChildId20
        => Stack2.TosChildId0;

    public IndexSet TosChildId21
        => Stack2.TosChildId1;

    public IndexSet RootId1 
        => Stack1.RootId;

    public IndexSet RootId2 
        => Stack2.RootId;


    protected XGaGbtStack2(IXGaGbtStack1 stack1, IXGaGbtStack1 stack2)
        : base(stack1.Capacity, stack1.RootTreeDepth)
    {
        Debug.Assert(
            stack1.Capacity == stack2.Capacity &&
            stack1.RootTreeDepth == stack2.RootTreeDepth
        );

        Stack1 = stack1;
        Stack2 = stack2;
    }


    public override void PushRootData()
    {
        TosIndex = 0;

        TreeDepthArray[TosIndex] = RootTreeDepth;

        Stack1.PushRootData();
        Stack2.PushRootData();
    }

    public override void PopNodeData()
    {
        Stack1.PopNodeData();
        Stack2.PopNodeData();

        TosTreeDepth = TreeDepthArray[TosIndex];

        //Console.Out.WriteLine($"depth:{TosTreeDepth}, id1: {TosId1}, id2: {TosId2}");

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

    public int TosHasChildPattern()
    {
        var hasChild10 = Stack1.TosHasChild(0);
        var hasChild11 = Stack1.TosHasChild(1);

        var hasChild20 = Stack2.TosHasChild(0);
        var hasChild21 = Stack2.TosHasChild(1);

        var pattern = 0;
        if (hasChild10)
        {
            if (hasChild20) pattern |= 1;
            if (hasChild21) pattern |= 2;
        }

        if (hasChild11)
        {
            if (hasChild20) pattern |= 4;
            if (hasChild21) pattern |= 8;
        }

        return pattern;
    }

    public int TosHasChildPattern(int selectionMask)
    {
        var hasChild10 = Stack1.TosHasChild(0);
        var hasChild11 = Stack1.TosHasChild(1);

        var hasChild20 = Stack2.TosHasChild(0);
        var hasChild21 = Stack2.TosHasChild(1);

        var pattern = 0;
        if (hasChild10)
        {
            if (hasChild20) pattern |= 1;
            if (hasChild21) pattern |= 2;
        }

        if (hasChild11)
        {
            if (hasChild20) pattern |= 4;
            if (hasChild21) pattern |= 8;
        }

        return pattern & selectionMask;
    }

    public void PushDataOfChildren()
    {
        var selectionPattern = 
            TosHasChildPattern();

        if ((selectionPattern & 1) != 0) 
            PushDataOfChild(0);

        if ((selectionPattern & 2) != 0) 
            PushDataOfChild(1);

        if ((selectionPattern & 4) != 0) 
            PushDataOfChild(2);

        if ((selectionPattern & 8) != 0) 
            PushDataOfChild(3);
    }

    public void PushDataOfChildren(int selectionMask)
    {
        var selectionPattern = 
            TosHasChildPattern(selectionMask);

        if ((selectionPattern & 1) != 0) 
            PushDataOfChild(0);

        if ((selectionPattern & 2) != 0) 
            PushDataOfChild(1);

        if ((selectionPattern & 4) != 0) 
            PushDataOfChild(2);

        if ((selectionPattern & 8) != 0) 
            PushDataOfChild(3);
    }

    public void PushDataOfChild(int childIndex)
    {
        TosIndex++;
        TreeDepthArray[TosIndex] = TosTreeDepth - 1;

        Stack1.PushDataOfChild(childIndex & 1);
        Stack2.PushDataOfChild(childIndex >> 1 & 1);
    }
}