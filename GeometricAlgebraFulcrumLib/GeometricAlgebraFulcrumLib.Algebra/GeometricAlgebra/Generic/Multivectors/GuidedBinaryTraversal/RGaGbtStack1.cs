using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal;

public abstract class XGaGbtStack1 : 
    XGaGbtStack, 
    IXGaGbtStack1
{
    /// <summary>
    /// Array containing node ID information in this stack
    /// </summary>
    protected IndexSet[] IdArray { get; }


    /// <summary>
    /// Top-of-stack node ID
    /// </summary>
    public IndexSet TosId { get; protected set; }


    /// <summary>
    /// Top-of-stack node child 0 ID
    /// </summary>
    public IndexSet TosChildId0
        => TosId;

    /// <summary>
    /// Top-of-stack node child 1 ID
    /// </summary>
    public IndexSet TosChildId1
        => TosId | 1ul << TosTreeDepth - 1;


    public IndexSet RootId { get; }


    protected XGaGbtStack1(int capacity, int rootTreeDepth, IndexSet rootId) 
        : base(capacity, rootTreeDepth)
    {
        IdArray = new IndexSet[Capacity];

        RootId = rootId;
    }


    public abstract bool TosHasChild(int childIndex);
        
    public abstract void PushDataOfChild(int childIndex);
}