using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal;

public sealed class XGaGbtBinaryTreeStack1<T> : 
    XGaGbtStack1, 
    IXGaGbtStack1<T>
{
    public static XGaGbtBinaryTreeStack1<T> Create(XGaMultivector<T> mv)
    {
        var vSpaceDimensions = 
            Math.Max(1, mv.VSpaceDimensions);

        var stack = new XGaGbtBinaryTreeStack1<T>(
            vSpaceDimensions + 1,
            mv.GetBinaryTree(vSpaceDimensions)
        );

        stack.PushRootData();

        return stack;
    }

    public static XGaGbtBinaryTreeStack1<T> Create(int capacity, XGaMultivector<T> mv)
    {
        var vSpaceDimensions = 
            Math.Max(1, mv.VSpaceDimensions);

        var stack = new XGaGbtBinaryTreeStack1<T>(
            capacity,
            mv.GetBinaryTree(vSpaceDimensions)
        );

        stack.PushRootData();

        return stack;
    }

    public static XGaGbtBinaryTreeStack1<T> Create(XGaGbtBinaryTree<T> rootNode)
    {
        var treeDepth = rootNode.TreeDepth;

        var stack = new XGaGbtBinaryTreeStack1<T>(
            treeDepth + 1,
            rootNode
        );

        stack.PushRootData();

        return stack;
    }

    public static XGaGbtBinaryTreeStack1<T> Create(int capacity, XGaGbtBinaryTree<T> rootNode)
    {
        var stack = new XGaGbtBinaryTreeStack1<T>(
            capacity,
            rootNode
        );

        stack.PushRootData();

        return stack;
    }


    private int[] BinaryTreeNodeIndexArray { get; }


    public Pair<int> TosBinaryTreeNode { get; private set; }

    public T TosScalar { get; private set; }


    public XGaGbtBinaryTree<T> BinaryTree { get; }


    private XGaGbtBinaryTreeStack1(int capacity, XGaGbtBinaryTree<T> binaryTree)
        : base(capacity, binaryTree.TreeDepth, IndexSet.EmptySet)
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


    public IEnumerable<Tuple<IndexSet, T>> GetLeafIdScalarPairs()
    {
        PushRootData();

        while (!IsEmpty)
        {
            PopNodeData();

            if (TosIsLeaf)
            {
                yield return new Tuple<IndexSet, T>(TosId, TosScalar);

                continue;
            }

            if (TosHasChild(1))
                PushDataOfChild(1);

            if (TosHasChild(0))
                PushDataOfChild(0);
        }
    }
}