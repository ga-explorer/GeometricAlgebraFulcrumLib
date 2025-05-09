using System.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.SimpleTree;

/// <summary>
/// A simple tree node that may have several child nodes organized as a list 
/// </summary>
/// <typeparam name="TLeaf"></typeparam>
[Serializable]
public sealed class SimpleTreeNodeList<TLeaf> : SimpleTreeNode<TLeaf>, IList<SimpleTreeNode<TLeaf>>
{
    private readonly List<SimpleTreeNode<TLeaf>> _nodes = [];


    public int IndexOf(SimpleTreeNode<TLeaf> item)
    {
        throw new NotImplementedException();
    }

    public void Insert(int index, SimpleTreeNode<TLeaf> item)
    {
        _nodes.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        _nodes.RemoveAt(index);
    }

    public SimpleTreeNode<TLeaf> this[int index]
    {
        get => _nodes[index];
        set => _nodes[index] = value;
    }

    public void Add(SimpleTreeNode<TLeaf> item)
    {
        _nodes.Add(item);
    }

    public void Clear()
    {
        _nodes.Clear();
    }

    public bool Contains(SimpleTreeNode<TLeaf> item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(SimpleTreeNode<TLeaf>[] array, int arrayIndex)
    {
        foreach (var item in _nodes)
            array[arrayIndex++] = item;
    }

    public int Count => _nodes.Count;

    public bool IsReadOnly => false;

    public bool Remove(SimpleTreeNode<TLeaf> item)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<SimpleTreeNode<TLeaf>> GetEnumerator()
    {
        return _nodes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _nodes.GetEnumerator();
    }

    public override IEnumerable<SimpleTreeNode<TLeaf>> ChildNodes => _nodes;

    public override IEnumerable<SimpleTreeBranch<TLeaf>> ChildBranches => [];

    //public override void ToString(LinearTextComposer textBuilder)
    //{
    //    textBuilder.Append("{");
    //    textBuilder.IncreaseIndentation();

    //    foreach (var item in _nodes)
    //        item.ToString(textBuilder);

    //    textBuilder.DecreaseIndentation();
    //    textBuilder.AppendAtNewLine("}");
    //}
}