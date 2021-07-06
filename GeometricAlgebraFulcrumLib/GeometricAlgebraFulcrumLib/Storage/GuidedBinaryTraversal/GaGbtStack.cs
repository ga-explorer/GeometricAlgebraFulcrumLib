using System.Diagnostics;

namespace GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal
{
    public abstract class GaGbtStack : IGaGbtStack
    {
        /// <summary>
        /// Array containing node tree depth information in this stack
        /// </summary>
        protected int[] TreeDepthArray { get; }

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
        public int TosIndex { get; protected set; }

        /// <summary>
        /// Top-of-stack node tree depth
        /// </summary>
        public int TosTreeDepth { get; protected set; }

        public int RootTreeDepth { get; }


        protected GaGbtStack(int capacity, int rootTreeDepth)
        {
            Debug.Assert(rootTreeDepth > 0);

            Capacity = capacity;

            TreeDepthArray = new int[Capacity];

            TosIndex = -1;
            RootTreeDepth = rootTreeDepth;
        }


        /// <summary>
        /// Push root node data from top-of-stack variables
        /// </summary>
        public abstract void PushRootData();

        /// <summary>
        /// Read current node data into top-of-stack variables and pop stack
        /// </summary>
        public abstract void PopNodeData();

        ///// <summary>
        ///// Read leaf node data into top-of-stack variables and pop stack
        ///// </summary>
        //public abstract void PopDataOfLeafNode();

        ///// <summary>
        ///// Read internal node data into top-of-stack variables and pop stack
        ///// </summary>
        //public abstract void PopDataOfInternalNode();
    }
}