using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.GuidedBinaryTraversal
{
    public sealed class RGaGbtBinaryTreeStack1<T> : 
        RGaGbtStack1, 
        IRGaGbtStack1<T>
    {
        public static RGaGbtBinaryTreeStack1<T> Create(RGaMultivector<T> mv)
        {
            var vSpaceDimensions = 
                Math.Max(1, mv.VSpaceDimensions);

            var stack = new RGaGbtBinaryTreeStack1<T>(
                vSpaceDimensions + 1,
                mv.GetBinaryTree(vSpaceDimensions)
            );

            stack.PushRootData();

            return stack;
        }

        public static RGaGbtBinaryTreeStack1<T> Create(int capacity, RGaMultivector<T> mv)
        {
            var vSpaceDimensions = 
                Math.Max(1, mv.VSpaceDimensions);

            var stack = new RGaGbtBinaryTreeStack1<T>(
                capacity,
                mv.GetBinaryTree(vSpaceDimensions)
            );

            stack.PushRootData();

            return stack;
        }

        public static RGaGbtBinaryTreeStack1<T> Create(RGaGbtBinaryTree<T> rootNode)
        {
            var treeDepth = rootNode.TreeDepth;

            var stack = new RGaGbtBinaryTreeStack1<T>(
                treeDepth + 1,
                rootNode
            );

            stack.PushRootData();

            return stack;
        }

        public static RGaGbtBinaryTreeStack1<T> Create(int capacity, RGaGbtBinaryTree<T> rootNode)
        {
            var stack = new RGaGbtBinaryTreeStack1<T>(
                capacity,
                rootNode
            );

            stack.PushRootData();

            return stack;
        }


        private int[] BinaryTreeNodeIndexArray { get; }


        public Pair<int> TosBinaryTreeNode { get; private set; }

        public T TosScalar { get; private set; }


        public RGaGbtBinaryTree<T> BinaryTree { get; }


        private RGaGbtBinaryTreeStack1(int capacity, RGaGbtBinaryTree<T> binaryTree)
            : base(capacity, binaryTree.TreeDepth, 0ul)
        {
            BinaryTreeNodeIndexArray = new int[capacity];

            BinaryTree = binaryTree;
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
            //Pop data for both cases of leaf and internal node
            TosTreeDepth = TreeDepthArray[TosIndex];
            TosId = IdArray[TosIndex];

            if (TosTreeDepth > 0)
            {
                //Pop data for the case of internal node
                TosBinaryTreeNode = BinaryTree.GetInternalNodeByIndex(
                    BinaryTreeNodeIndexArray[TosIndex]
                );
            }
            else
            {
                //Pop data for the case of leaf node
                TosScalar = BinaryTree.GetLeafNodeScalarByIndex(
                    BinaryTreeNodeIndexArray[TosIndex]
                );
            }

            //Remove top of stack
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


        public IEnumerable<RGaKvIndexScalarRecord<T>> GetLeafIdScalarPairs()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return new RGaKvIndexScalarRecord<T>(TosId, TosScalar);

                    continue;
                }

                if (TosHasChild(1))
                    PushDataOfChild(1);

                if (TosHasChild(0))
                    PushDataOfChild(0);
            }
        }
    }
}