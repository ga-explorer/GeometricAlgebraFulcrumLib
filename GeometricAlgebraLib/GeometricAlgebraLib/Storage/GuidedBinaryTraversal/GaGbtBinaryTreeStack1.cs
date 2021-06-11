using System;
using System.Collections.Generic;
using GeometricAlgebraLib.Storage.Trees;

namespace GeometricAlgebraLib.Storage.GuidedBinaryTraversal
{
    public sealed class GaGbtBinaryTreeStack1<T> 
        : GaGbtStack1, IGaGbtStack1<T>
    {
        public static GaGbtBinaryTreeStack1<T> Create(GaMultivectorStorageBase<T> mv)
        {
            var vSpaceDimension = 
                Math.Max(1, mv.VSpaceDimension);

            var stack = new GaGbtBinaryTreeStack1<T>(
                vSpaceDimension + 1,
                mv.GetBinaryTree(vSpaceDimension)
            );

            stack.PushRootData();

            return stack;
        }

        public static GaGbtBinaryTreeStack1<T> Create(int capacity, GaMultivectorStorageBase<T> mv)
        {
            var vSpaceDimension = 
                Math.Max(1, mv.VSpaceDimension);

            var stack = new GaGbtBinaryTreeStack1<T>(
                capacity,
                mv.GetBinaryTree(vSpaceDimension)
            );

            stack.PushRootData();

            return stack;
        }

        public static GaGbtBinaryTreeStack1<T> Create(GaBinaryTree<T> rootNode)
        {
            var treeDepth = rootNode.TreeDepth;

            var stack = new GaGbtBinaryTreeStack1<T>(
                treeDepth + 1,
                rootNode
            );

            stack.PushRootData();

            return stack;
        }

        public static GaGbtBinaryTreeStack1<T> Create(int capacity, GaBinaryTree<T> rootNode)
        {
            var stack = new GaGbtBinaryTreeStack1<T>(
                capacity,
                rootNode
            );

            stack.PushRootData();

            return stack;
        }


        private int[] BinaryTreeNodeIndexArray { get; }


        public Tuple<int, int> TosBinaryTreeNode { get; private set; }

        public T TosScalar { get; private set; }


        public GaBinaryTree<T> BinaryTree { get; }


        private GaGbtBinaryTreeStack1(int capacity, GaBinaryTree<T> binaryTree)
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
                TosScalar = BinaryTree.GetLeafNodeValueByIndex(
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


        public IEnumerable<KeyValuePair<ulong, T>> GetLeafIdScalarPairs()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return new KeyValuePair<ulong, T>(TosId, TosScalar);

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