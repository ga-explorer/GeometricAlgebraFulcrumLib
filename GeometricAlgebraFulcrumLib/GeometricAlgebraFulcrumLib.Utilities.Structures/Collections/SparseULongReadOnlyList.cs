using System.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;

public sealed class SparseULongReadOnlyList<T> : IReadOnlyList<T>
{
    private readonly IReadOnlyDictionary<ulong, T> _dictionary;

    public T DefaultValue { get; }

    public int Count { get; }

    public T this[int index] 
        => _dictionary.TryGetValue((ulong)index, out var value) ? value : DefaultValue;


    public SparseULongReadOnlyList(int count, IReadOnlyDictionary<ulong, T> dictionary)
    {
        Count = count;
        DefaultValue = default;
        _dictionary = dictionary;
    }

    public SparseULongReadOnlyList(int count, T defaultValue, IReadOnlyDictionary<ulong, T> dictionary)
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