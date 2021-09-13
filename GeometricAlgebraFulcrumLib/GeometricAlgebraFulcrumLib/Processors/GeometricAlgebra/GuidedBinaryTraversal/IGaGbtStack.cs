namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal
{
    public interface IGeoGbtStack
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
}