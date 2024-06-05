using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.GuidedBinaryTraversal.Multivectors;



public sealed class RGaMultivectorGbtUniformStack1<T> : 
    RGaGbtStack1, 
    IRGaGbtMultivectorStorageStack1<T>
{
    public static RGaMultivectorGbtUniformStack1<T> Create(int capacity, int treeDepth, RGaMultivector<T> multivectorStorage)
    {
        return new RGaMultivectorGbtUniformStack1<T>(capacity, treeDepth, multivectorStorage);
    }


    private int[] BinaryTreeNodeIndexArray { get; }


    public RGaProcessor<T> Processor 
        => Multivector.Processor;

    public IScalarProcessor<T> ScalarProcessor 
        => Multivector.ScalarProcessor;

    public RGaMultivector<T> Multivector { get; }

    public Pair<int> TosBinaryTreeNode { get; private set; }

    public T TosScalar { get; private set; }

    public RGaGbtBinaryTree<T> BinaryTree { get; }


    private RGaMultivectorGbtUniformStack1(int capacity, int treeDepth, RGaMultivector<T> multivectorStorage)
        : base(capacity, treeDepth, 0ul)
    {
        BinaryTreeNodeIndexArray = new int[capacity];
        Multivector = multivectorStorage;

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