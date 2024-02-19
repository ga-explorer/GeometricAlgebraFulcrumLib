using System.Collections;
using System.Collections.Generic;

namespace DataStructuresLib.Collections;

public sealed class SparseReadOnlyList<T> : IReadOnlyList<T>
{
    private readonly IReadOnlyDictionary<int, T> _dictionary;

    public T DefaultValue { get; }

    public int Count { get; }

    public T this[int index] 
        => _dictionary.TryGetValue(index, out var value) ? value : DefaultValue;


    public SparseReadOnlyList(int count, IReadOnlyDictionary<int, T> dictionary)
    {
        Count = count;
        DefaultValue = default;
        _dictionary = dictionary;
    }

    public SparseReadOnlyList(int count, T defaultValue, IReadOnlyDictionary<int, T> dictionary)
    {
        Count = count;
        DefaultValue = defaultValue;
        _dictionary = dictionary;
    }


    public IEnumerator<T> GetEnumerator()
    {
        for (var i = 0; i < Count; i++)
            yield return this[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        for (var i = 0; i < Count; i++)
            yield return this[i];
    }
}