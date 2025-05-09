using System.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.SimpleTree;

/// <summary>
/// A simple tree node that may have several child nodes organized as a dictionary with string keys
/// </summary>
/// <typeparam name="TLeaf"></typeparam>
[Serializable]
public sealed class SimpleTreeNodeDictionaryByName<TLeaf> 
    : SimpleTreeNode<TLeaf>, IDictionary<string, SimpleTreeNode<TLeaf>>
{
    private readonly Dictionary<string, SimpleTreeNode<TLeaf>> _nodes =
        new Dictionary<string, SimpleTreeNode<TLeaf>>();


    public void Add(string key, TLeaf value)
    {
        _nodes.Add(key, new SimpleTreeLeaf<TLeaf>(value));
    }

    public void Add(string key, SimpleTreeNode<TLeaf> value)
    {
        _nodes.Add(key, value);
    }

    public bool ContainsKey(string key)
    {
        return _nodes.ContainsKey(key);
    }

    public ICollection<string> Keys => _nodes.Keys;

    public bool Remove(string key)
    {
        return _nodes.Remove(key);
    }

    public bool TryGetValue(string key, out SimpleTreeNode<TLeaf> value)
    {
        return _nodes.TryGetValue(key, out value);
    }

    public ICollection<SimpleTreeNode<TLeaf>> Values => _nodes.Values;

    public SimpleTreeNode<TLeaf> this[string key]
    {
        get => _nodes[key];
        set => _nodes[key] = value;
    }

    public void Add(KeyValuePair<string, SimpleTreeNode<TLeaf>> item)
    {
        _nodes.Add(item.Key, item.Value);
    }

    public void Clear()
    {
        _nodes.Clear();
    }

    public bool Contains(KeyValuePair<string, SimpleTreeNode<TLeaf>> item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<string, SimpleTreeNode<TLeaf>>[] array, int arrayIndex)
    {
        foreach (var pair in _nodes)
            array[arrayIndex++] = pair;
    }

    public int Count => _nodes.Count;

    public bool IsReadOnly => false;

    public bool Remove(KeyValuePair<string, SimpleTreeNode<TLeaf>> item)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<KeyValuePair<string, SimpleTreeNode<TLeaf>>> GetEnumerator()
    {
        return _nodes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _nodes.GetEnumerator();
    }

    public override IEnumerable<SimpleTreeNode<TLeaf>> ChildNodes
    {
        get { return _nodes.Select(pair => pair.Value); }
    }

    public override IEnumerable<SimpleTreeBranch<TLeaf>> ChildBranches => [];

    //public override void ToString(LinearTextComposer textBuilder)
    //{
    //    textBuilder.AppendLine("{");
    //    textBuilder.IncreaseIndentation();

    //    var flag = false;
    //    foreach (var pair in _nodes)
    //    {
    //        if (flag)
    //            textBuilder.AppendLine(",");
    //        else
    //            flag = true;

    //        textBuilder.Append(pair.Key);
    //        pair.Value.ToString(textBuilder);
    //    }

    //    textBuilder.DecreaseIndentation();
    //    textBuilder.AppendAtNewLine("}");
    //}
}