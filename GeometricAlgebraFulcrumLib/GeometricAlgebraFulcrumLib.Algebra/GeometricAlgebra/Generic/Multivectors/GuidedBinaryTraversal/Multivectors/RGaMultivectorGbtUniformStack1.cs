using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal.Multivectors;



public sealed class XGaMultivectorGbtUniformStack1<T> : 
    XGaGbtStack1, 
    IXGaGbtMultivectorStorageStack1<T>
{
    public static XGaMultivectorGbtUniformStack1<T> Create(int capacity, int treeDepth, XGaMultivector<T> multivectorStorage)
    {
        return new XGaMultivectorGbtUniformStack1<T>(capacity, treeDepth, multivectorStorage);
    }


    private int[] BinaryTreeNodeIndexArray { get; }


    public XGaProcessor<T> Processor 
        => Multivector.Processor;

    public IScalarProcessor<T> ScalarProcessor 
        => Multivector.ScalarProcessor;

    public XGaMultivector<T> Multivector { get; }

    public Pair<int> TosBinaryTreeNode { get; private set; }

    public T TosScalar { get; private set; }

    public XGaGbtBinaryTree<T> BinaryTree { get; }


    private XGaMultivectorGbtUniformStack1(int capacity, int treeDepth, XGaMultivector<T> multivectorStorage)
        : base(capacity, treeDepth, IndexSet.EmptySet)
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