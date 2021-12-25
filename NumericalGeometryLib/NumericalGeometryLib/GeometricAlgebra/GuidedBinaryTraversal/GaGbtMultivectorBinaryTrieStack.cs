using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using NumericalGeometryLib.GeometricAlgebra.Basis;
using NumericalGeometryLib.GeometricAlgebra.Structures;

namespace NumericalGeometryLib.GeometricAlgebra.GuidedBinaryTraversal
{
    public sealed class GaGbtMultivectorBinaryTrieStack
    {
        /// <summary>
        /// Array containing node tree depth information in this stack
        /// </summary>
        private int[] TreeDepthArray { get; }


        /// <summary>
        /// Maximum number of nodes that can be stored in this stack
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        /// Number of nodes currently stored in this stack
        /// </summary>
        public int Count
            => TosIndex + 1;

        /// <summary>
        /// True if this stack is empty
        /// </summary>
        public bool IsEmpty
            => TosIndex < 0;

        /// <summary>
        /// Top-of-stack node is a leaf node
        /// </summary>
        public bool TosIsLeaf
            => TosTreeDepth == 0;

        /// <summary>
        /// Top-of-stack node is a leaf parent internal node
        /// </summary>
        public bool TosIsLeafParent
            => TosTreeDepth == 1;

        /// <summary>
        /// Top-of-stack node is an internal node
        /// </summary>
        public bool TosIsInternal
            => TosTreeDepth > 0;

        /// <summary>
        /// Top-of-stack node index
        /// </summary>
        public int TosIndex { get; private set; }

        /// <summary>
        /// Top-of-stack node tree depth
        /// </summary>
        public int TosTreeDepth { get; private set; }

        public int RootTreeDepth { get; }

        /// <summary>
        /// Array containing node ID information in this stack
        /// </summary>
        private ulong[] IdArray { get; }

        /// <summary>
        /// Top-of-stack node ID
        /// </summary>
        public ulong TosId { get; private set; }
        
        public double TosScalar { get; private set; }

        /// <summary>
        /// Top-of-stack node child 0 ID
        /// </summary>
        public ulong TosChildId0
            => TosId;

        /// <summary>
        /// Top-of-stack node child 1 ID
        /// </summary>
        public ulong TosChildId1
            => TosId | (1ul << (TosTreeDepth - 1));

        public ulong RootId { get; }

        private int[] BinaryTreeNodeIndexArray { get; }

        public GaBasisSet BasisSet { get; }
        
        public Pair<int> TosBinaryTreeNode { get; private set; }
        
        public GaMultivectorBinaryTrie BinaryTrie { get; }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GaGbtMultivectorBinaryTrieStack([NotNull] GaBasisSet basisSet, int capacity, [NotNull] GaMultivectorBinaryTrie binaryTrie)
        {
            Debug.Assert(binaryTrie.TreeDepth > 0);

            Capacity = capacity;

            TreeDepthArray = new int[Capacity];

            TosIndex = -1;
            RootTreeDepth = binaryTrie.TreeDepth;

            IdArray = new ulong[Capacity];

            RootId = 0UL;

            BasisSet = basisSet;
            BinaryTreeNodeIndexArray = new int[capacity];
            BinaryTrie = binaryTrie;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PushRootData()
        {
            TosIndex = 0;

            TreeDepthArray[TosIndex] = RootTreeDepth;
            IdArray[TosIndex] = RootId;
            BinaryTreeNodeIndexArray[TosIndex] = BinaryTrie.RootNodeIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PopNodeData()
        {
            TosTreeDepth = TreeDepthArray[TosIndex];
            TosId = IdArray[TosIndex];

            if (TosTreeDepth > 0)
            {
                TosBinaryTreeNode = BinaryTrie.GetInternalNodeByIndex(
                    BinaryTreeNodeIndexArray[TosIndex]
                );
            }
            else
            {
                TosScalar = BinaryTrie.GetLeafNodeScalarByIndex(
                    BinaryTreeNodeIndexArray[TosIndex]
                );
            }

            TosIndex--;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TosHasChild(int childIndex)
        {
            return (childIndex & 1) == 0
                ? TosBinaryTreeNode.Item1 >= 0
                : TosBinaryTreeNode.Item2 >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PushDataOfChild(int childIndex)
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