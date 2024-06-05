namespace GeometricAlgebraFulcrumLib.Utilities.Structures.SimpleTree;

/// <summary>
/// A simple tree branch. A branch is attached to a single child node and holds more information about this
/// node like an integer index, a string name, and a string type
/// </summary>
/// <typeparam name="TLeaf"></typeparam>
[Serializable]
public sealed class SimpleTreeBranch<TLeaf>
{
    public int BranchIndex { get; private set; }

    public string BranchName { get; private set; }

    public string BranchType { get; private set; }

    public SimpleTreeNode<TLeaf> BranchNode { get; private set; }


    public SimpleTreeBranch(int index, string name, string type, TLeaf value)
    {
        BranchIndex = index;
        BranchName = name;
        BranchType = type;
        BranchNode = new SimpleTreeLeaf<TLeaf>(value);
    }

    public SimpleTreeBranch(int index, string name, string type, SimpleTreeNode<TLeaf> value)
    {
        BranchIndex = index;
        BranchName = name;
        BranchType = type;
        BranchNode = value;
    }


    //public void ToString(LinearTextComposer textBuilder)
    //{
    //    textBuilder.Append(string.IsNullOrEmpty(BranchName) ? BranchIndex.ToString() : BranchName);
    //    textBuilder.Append(" = ");
    //    BranchNode.ToString(textBuilder);
    //}

    //public override string ToString()
    //{
    //    var textBuilder = new LinearTextComposer();

    //    ToString(textBuilder);

    //    return textBuilder.ToString();
    //}
}