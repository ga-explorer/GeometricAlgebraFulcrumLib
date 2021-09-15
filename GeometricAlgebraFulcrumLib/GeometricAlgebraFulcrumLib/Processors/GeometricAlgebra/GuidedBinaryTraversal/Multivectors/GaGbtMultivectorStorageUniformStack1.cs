using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Multivectors
{
    public sealed class GeoGbtMultivectorStorageUniformStack1<T>
        : GeoGbtStack1, IGeoGbtMultivectorStorageStack1<T>
    {
        public static GeoGbtMultivectorStorageUniformStack1<T> Create(int capacity, int treeDepth, IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> multivectorStorage)
        {
            return new(capacity, treeDepth, scalarProcessor, multivectorStorage);
        }


        private int[] BinaryTreeNodeIndexArray { get; }


        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public IMultivectorStorage<T> Storage { get; }

        public Pair<int> TosBinaryTreeNode { get; private set; }

        public T TosScalar { get; private set; }

        public LinVectorTreeStorage<T> BinaryTree { get; }


        private GeoGbtMultivectorStorageUniformStack1(int capacity, int treeDepth, [NotNull] IScalarAlgebraProcessor<T> scalarProcessor, [NotNull] IMultivectorStorage<T> multivectorStorage)
            : base(capacity, treeDepth, 0ul)
        {
            ScalarProcessor = scalarProcessor;
            BinaryTreeNodeIndexArray = new int[capacity];
            Storage = multivectorStorage;

            BinaryTree = multivectorStorage.GetBinaryTree(RootTreeDepth);
        }


        public override void PushRootData()
        {
            TosIndex = 0;

            TreeDepthArray[TosIndex] = RootTreeDepth;
            IdArray[TosIndex] = RootId;
            BinaryTreeNodeIndexArray[TosIndex] = BinaryTree.RootNodeIndex;
        }

        public override void PopNodeData()
        {
            TosTreeDepth = TreeDepthArray[TosIndex];
            TosId = IdArray[TosIndex];

            if (TosTreeDepth > 0)
            {
                TosBinaryTreeNode = BinaryTree.GetInternalNodeByIndex(
                    BinaryTreeNodeIndexArray[TosIndex]
                );
            }
            else
            {
                TosScalar = BinaryTree.GetLeafNodeScalarByIndex(
                    BinaryTreeNodeIndexArray[TosIndex]
                );
            }

            TosIndex--;
        }

        public override bool TosHasChild(int childIndex)
        {
            return (childIndex & 1) == 0
                ? TosBinaryTreeNode.Item1 >= 0
                : TosBinaryTreeNode.Item2 >= 0;
        }

        public override void PushDataOfChild(int childIndex)
        {
            TosIndex++;
            TreeDepthArray[TosIndex] = TosTreeDepth - 1;

            if ((childIndex & 1) == 0)
            {
                IdArray[TosIndex] = TosChildId0;
                BinaryTreeNodeIndexArray[TosIndex] = TosBinaryTreeNode.Item1;
            }
            else
            {
                IdArray[TosIndex] = TosChildId1;
                BinaryTreeNodeIndexArray[TosIndex] = TosBinaryTreeNode.Item2;
            }
        }
    }
}