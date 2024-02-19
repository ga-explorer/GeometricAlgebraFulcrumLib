using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.ReadOnlyLists;

public sealed class SparseReadOnlyList<T>
    : IReadOnlyList<T>
{
    private readonly Dictionary<int, T> _itemsDictionary
        = new Dictionary<int, T>();


    public int Count { get; }

    public T DefaultValue { get; set; }
        = default;

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            return _itemsDictionary.TryGetValue(index, out var value)
                ? value
                : DefaultValue;
        }
        set
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            if (_itemsDictionary.ContainsKey(index))
                _itemsDictionary[index] = value;
            else
                _itemsDictionary.Add(index, value);
        }
    }


    public SparseReadOnlyList(int count)
    {
        Count = Count;
    }


    public void SetToDefault(int index)
    {
        _itemsDictionary.Remove(index);
    }

    public void Clear()
    {
        _itemsDictionary.Clear();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return Enumerable
            .Range(0, Count)
            .Select(i => this[i])
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}