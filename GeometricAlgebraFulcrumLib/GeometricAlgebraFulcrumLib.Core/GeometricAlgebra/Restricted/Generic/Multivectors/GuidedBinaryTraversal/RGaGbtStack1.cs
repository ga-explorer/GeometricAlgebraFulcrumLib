namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors.GuidedBinaryTraversal;

public abstract class RGaGbtStack1 : 
    RGaGbtStack, 
    IRGaGbtStack1
{
    /// <summary>
    /// Array containing node ID information in this stack
    /// </summary>
    protected ulong[] IdArray { get; }


    /// <summary>
    /// Top-of-stack node ID
    /// </summary>
    public ulong TosId { get; protected set; }


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


    protected RGaGbtStack1(int capacity, int rootTreeDepth, ulong rootId) 
        : base(capacity, rootTreeDepth)
    {
        IdArray = new ulong[Capacity];

        RootId = rootId;
    }


    public abstract bool TosHasChild(int childIndex);
        
    public abstract void PushDataOfChild(int childIndex);
}