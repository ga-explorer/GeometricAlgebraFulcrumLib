using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.SimpleTree;

/// <summary>
/// A simple tree node that may have several child nodes organized as a dictionary with integer keys
/// </summary>
/// <typeparam name="TLeaf"></typeparam>
[Serializable]
public sealed class SimpleTreeNodeDictionaryByIndex<TLeaf>
    : SimpleTreeNode<TLeaf>, IDictionary<int, SimpleTreeNode<TLeaf>>
{
    private readonly Dictionary<int, SimpleTreeNode<TLeaf>> _nodes =
        new Dictionary<int, SimpleTreeNode<TLeaf>>();


    public void Add(int key, TLeaf value)
    {
        _nodes.Add(key, new SimpleTreeLeaf<TLeaf>(value));
    }

    public void Add(int key, SimpleTreeNode<TLeaf> value)
    {
        _nodes.Add(key, value);
    }

    public bool ContainsKey(int key)
    {
        return _nodes.ContainsKey(key);
    }

    public ICollection<int> Keys => _nodes.Keys;

    public bool Remove(int key)
    {
        return _nodes.Remove(key);
    }

    public bool TryGetValue(int key, out SimpleTreeNode<TLeaf> value)
    {
        return _nodes.TryGetValue(key, out value);
    }

    public ICollection<SimpleTreeNode<TLeaf>> Values => _nodes.Values;

    public SimpleTreeNode<TLeaf> this[int key]
    {
        get => _nodes[key];
        set => _nodes[key] = value;
    }

    public void Add(KeyValuePair<int, SimpleTreeNode<TLeaf>> item)
    {
        _nodes.Add(item.Key, item.Value);
    }

    public void Clear()
    {
        _nodes.Clear();
    }

    public bool Contains(KeyValuePair<int, SimpleTreeNode<TLeaf>> item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<int, SimpleTreeNode<TLeaf>>[] array, int arrayIndex)
    {
        foreach (var pair in _nodes)
            array[arrayIndex++] = pair;
    }

    public int Count => _nodes.Count;

    public bool IsReadOnly => false;

    public bool Remove(KeyValuePair<int, SimpleTreeNode<TLeaf>> item)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<KeyValuePair<int, SimpleTreeNode<TLeaf>>> GetEnumerator()
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

    public override IEnumerable<SimpleTreeBranch<TLeaf>> ChildBranches => Enumerable.Empty<SimpleTreeBranch<TLeaf>>();

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

    //        textBuilder.Append(pair.Key.ToString());
    //        pair.Value.ToString(textBuilder);
    //    }

    //    textBuilder.DecreaseIndentation();
    //    textBuilder.AppendAtNewLine("}");
    //}
}