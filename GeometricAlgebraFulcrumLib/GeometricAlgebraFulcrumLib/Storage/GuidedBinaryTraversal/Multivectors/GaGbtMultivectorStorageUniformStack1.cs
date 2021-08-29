using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Multivectors
{
    public sealed class GaGbtMultivectorStorageUniformStack1<T>
        : GaGbtStack1, IGaGbtMultivectorStorageStack1<T>
    {
        public static GaGbtMultivectorStorageUniformStack1<T> Create(int capacity, int treeDepth, IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> multivectorStorage)
        {
            return new(capacity, treeDepth, scalarProcessor, multivectorStorage);
        }


        private int[] BinaryTreeNodeIndexArray { get; }


        public IScalarProcessor<T> ScalarProcessor { get; }

        public IGaMultivectorStorage<T> Storage { get; }

        public Pair<int> TosBinaryTreeNode { get; private set; }

        public T TosScalar { get; private set; }

        public LaVectorTreeStorage<T> BinaryTree { get; }


        private GaGbtMultivectorStorageUniformStack1(int capacity, int treeDepth, [NotNull] IScalarProcessor<T> scalarProcessor, [NotNull] IGaMultivectorStorage<T> multivectorStorage)
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
                TosScalar = BinaryTree.GetLeafNodeValueByIndex(
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