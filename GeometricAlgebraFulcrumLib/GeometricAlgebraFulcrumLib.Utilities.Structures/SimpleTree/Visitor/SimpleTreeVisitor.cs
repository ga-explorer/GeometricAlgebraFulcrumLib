namespace GeometricAlgebraFulcrumLib.Utilities.Structures.SimpleTree.Visitor;

public abstract class SimpleTreeVisitor<TLeaf>
{
    public abstract void Visit(SimpleTreeLeaf<TLeaf> treeNode);

    public virtual void Visit(SimpleTreeBranch<TLeaf> treeBranch)
    {
        Visit(treeBranch.BranchNode);
    }

    public virtual void Visit(SimpleTreeBranchDictionaryByIndex<TLeaf> treeNode)
    {
        foreach (var pair in treeNode)
            Visit(pair.Value);
    }

    public virtual void Visit(SimpleTreeBranchDictionaryByName<TLeaf> treeNode)
    {
        foreach (var pair in treeNode)
            Visit(pair.Value);
    }

    public virtual void Visit(SimpleTreeBranchList<TLeaf> treeNode)
    {
        foreach (var item in treeNode)
            Visit(item);
    }

    public virtual void Visit(SimpleTreeNodeDictionaryByIndex<TLeaf> treeNode)
    {
        foreach (var pair in treeNode)
            Visit(pair.Value);
    }

    public virtual void Visit(SimpleTreeNodeDictionaryByName<TLeaf> treeNode)
    {
        foreach (var pair in treeNode)
            Visit(pair.Value);
    }

    public virtual void Visit(SimpleTreeNodeList<TLeaf> treeNode)
    {
        foreach (var item in treeNode)
            Visit(item);
    }

    public virtual void Visit(SimpleTreeNode<TLeaf> treeNode)
    {
        var node1 = treeNode.AsLeaf;

        if (ReferenceEquals(node1, null) == false)
        {
            Visit(node1);
            return;
        }

        var node2 = treeNode.AsBranchDictionaryByIndex;

        if (ReferenceEquals(node2, null) == false)
        {
            Visit(node2);
            return;
        }

        var node3 = treeNode.AsBranchDictionaryByName;

        if (ReferenceEquals(node3, null) == false)
        {
            Visit(node3);
            return;
        }

        var node4 = treeNode.AsBranchList;

        if (ReferenceEquals(node4, null) == false)
        {
            Visit(node4);
            return;
        }

        var node5 = treeNode.AsNodeDictionaryByIndex;

        if (ReferenceEquals(node5, null) == false)
        {
            Visit(node5);
            return;
        }

        var node6 = treeNode.AsNodeDictionaryByName;

        if (ReferenceEquals(node6, null) == false)
        {
            Visit(node6);
            return;
        }

        var node7 = treeNode.AsNodeList;

        if (ReferenceEquals(node7, null) == false)
        {
            Visit(node7);
        }
    }
}

public abstract class SimpleTreeVisitor<TLeaf, TReturn>
{
    public abstract TReturn Visit(SimpleTreeLeaf<TLeaf> treeNode);

    public virtual TReturn Visit(SimpleTreeBranch<TLeaf> treeBranch)
    {
        Visit(treeBranch.BranchNode);

        return default(TReturn);
    }

    public virtual TReturn Visit(SimpleTreeBranchDictionaryByIndex<TLeaf> treeNode)
    {
        foreach (var pair in treeNode)
            Visit(pair.Value);

        return default(TReturn);
    }

    public virtual TReturn Visit(SimpleTreeBranchDictionaryByName<TLeaf> treeNode)
    {
        foreach (var pair in treeNode)
            Visit(pair.Value);

        return default(TReturn);
    }

    public virtual TReturn Visit(SimpleTreeBranchList<TLeaf> treeNode)
    {
        foreach (var item in treeNode)
            Visit(item);

        return default(TReturn);
    }

    public virtual TReturn Visit(SimpleTreeNodeDictionaryByIndex<TLeaf> treeNode)
    {
        foreach (var pair in treeNode)
            Visit(pair.Value);

        return default(TReturn);
    }

    public virtual TReturn Visit(SimpleTreeNodeDictionaryByName<TLeaf> treeNode)
    {
        foreach (var pair in treeNode)
            Visit(pair.Value);

        return default(TReturn);
    }

    public virtual TReturn Visit(SimpleTreeNodeList<TLeaf> treeNode)
    {
        foreach (var item in treeNode)
            Visit(item);

        return default(TReturn);
    }

    public virtual TReturn Visit(SimpleTreeNode<TLeaf> treeNode)
    {
        var node1 = treeNode.AsLeaf;

        if (ReferenceEquals(node1, null) == false)
            return Visit(node1);

        var node2 = treeNode.AsBranchDictionaryByIndex;

        if (ReferenceEquals(node2, null) == false)
            return Visit(node2);

        var node3 = treeNode.AsBranchDictionaryByName;

        if (ReferenceEquals(node3, null) == false)
            return Visit(node3);

        var node4 = treeNode.AsBranchList;

        if (ReferenceEquals(node4, null) == false)
            return Visit(node4);

        var node5 = treeNode.AsNodeDictionaryByIndex;

        if (ReferenceEquals(node5, null) == false)
            return Visit(node5);

        var node6 = treeNode.AsNodeDictionaryByName;

        if (ReferenceEquals(node6, null) == false)
            return Visit(node6);

        var node7 = treeNode.AsNodeList;

        if (ReferenceEquals(node7, null) == false)
            return Visit(node7);

        return default(TReturn);
    }
}