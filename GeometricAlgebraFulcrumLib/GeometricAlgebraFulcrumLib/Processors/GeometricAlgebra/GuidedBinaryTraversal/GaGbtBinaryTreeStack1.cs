using System;
using System.Collections.Generic;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal
{
    public sealed class GeoGbtBinaryTreeStack1<T> 
        : GeoGbtStack1, IGeoGbtStack1<T>
    {
        public static GeoGbtBinaryTreeStack1<T> Create(IMultivectorStorage<T> mv)
        {
            var vSpaceDimension = 
                Math.Max(1, mv.MinVSpaceDimension);

            var stack = new GeoGbtBinaryTreeStack1<T>(
                (int) vSpaceDimension + 1,
                mv.GetBinaryTree((int) vSpaceDimension)
            );

            stack.PushRootData();

            return stack;
        }

        public static GeoGbtBinaryTreeStack1<T> Create(int capacity, IMultivectorStorage<T> mv)
        {
            var vSpaceDimension = 
                Math.Max(1, mv.MinVSpaceDimension);

            var stack = new GeoGbtBinaryTreeStack1<T>(
                capacity,
                mv.GetBinaryTree((int) vSpaceDimension)
            );

            stack.PushRootData();

            return stack;
        }

        public static GeoGbtBinaryTreeStack1<T> Create(LinVectorTreeStorage<T> rootNode)
        {
            var treeDepth = rootNode.TreeDepth;

            var stack = new GeoGbtBinaryTreeStack1<T>(
                treeDepth + 1,
                rootNode
            );

            stack.PushRootData();

            return stack;
        }

        public static GeoGbtBinaryTreeStack1<T> Create(int capacity, LinVectorTreeStorage<T> rootNode)
        {
            var stack = new GeoGbtBinaryTreeStack1<T>(
                capacity,
                rootNode
            );

            stack.PushRootData();

            return stack;
        }


        private int[] BinaryTreeNodeIndexArray { get; }


        public Pair<int> TosBinaryTreeNode { get; private set; }

        public T TosScalar { get; private set; }


        public LinVectorTreeStorage<T> BinaryTree { get; }


        private GeoGbtBinaryTreeStack1(int capacity, LinVectorTreeStorage<T> binaryTree)
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


        public IEnumerable<IndexScalarRecord<T>> GetLeafIdScalarPairs()
        {
            PushRootData();

            while (!IsEmpty)
            {
                PopNodeData();

                if (TosIsLeaf)
                {
                    yield return new IndexScalarRecord<T>(TosId, TosScalar);

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