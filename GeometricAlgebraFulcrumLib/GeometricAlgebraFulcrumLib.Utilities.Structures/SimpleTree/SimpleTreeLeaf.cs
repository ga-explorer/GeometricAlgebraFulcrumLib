namespace GeometricAlgebraFulcrumLib.Utilities.Structures.SimpleTree;

/// <summary>
/// A leaf node of a simple tree. It contains a mutable value of type TLeaf and it can have no child nodes
/// </summary>
/// <typeparam name="TLeaf"></typeparam>
[Serializable]
public sealed class SimpleTreeLeaf<TLeaf> : SimpleTreeNode<TLeaf>
{
    public TLeaf Value { get; set; }


    public SimpleTreeLeaf()
    {
        Value = default(TLeaf);
    }

    public SimpleTreeLeaf(TLeaf value)
    {
        Value = value;
    }

    public override IEnumerable<SimpleTreeNode<TLeaf>> ChildNodes => Enumerable.Empty<SimpleTreeNode<TLeaf>>();

    public override IEnumerable<SimpleTreeBranch<TLeaf>> ChildBranches => Enumerable.Empty<SimpleTreeBranch<TLeaf>>();

    //public override void ToString(LinearTextComposer textBuilder)
    //{
    //    textBuilder.Append(Value.ToString());
    //}
}