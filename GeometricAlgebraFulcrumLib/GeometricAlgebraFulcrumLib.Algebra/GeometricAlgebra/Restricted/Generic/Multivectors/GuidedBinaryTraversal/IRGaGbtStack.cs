namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.GuidedBinaryTraversal;

public interface IRGaGbtStack
{
    int Capacity { get; }

    int Count { get; }

    bool IsEmpty { get; }


    bool TosIsLeaf { get; }

    bool TosIsLeafParent { get; }

    bool TosIsInternal { get; }

    int TosIndex { get; }

    int TosTreeDepth { get; }


    int RootTreeDepth { get; }


    void PushRootData();

    void PopNodeData();
}