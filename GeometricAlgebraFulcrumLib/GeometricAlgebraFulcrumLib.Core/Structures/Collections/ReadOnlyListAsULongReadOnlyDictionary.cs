using System.Collections;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections;

public sealed class ReadOnlyListAsULongReadOnlyDictionary<T> : IReadOnlyDictionary<ulong, T>
{
    public IReadOnlyList<T> ReadOnlyList { get; }

    public int Count 
        => ReadOnlyList.Count;

    public T this[ulong key]
    {
        get
        {
            if (key >= (ulong)ReadOnlyList.Count)
                throw new KeyNotFoundException();

            return ReadOnlyList[(int)key];
        }
    }

    public IEnumerable<ulong> Keys
        => Enumerable.Range(0, ReadOnlyList.Count).Select(key => (ulong)key);

    public IEnumerable<T> Values
        => ReadOnlyList;


    public ReadOnlyListAsULongReadOnlyDictionary(IReadOnlyList<T> readOnlyList)
    {
        ReadOnlyList = readOnlyList;
    }


    public bool ContainsKey(ulong key)
    {
        return key < (ulong)ReadOnlyList.Count;
    }

    public bool TryGetValue(ulong key, out T value)
    {
        if (key < (ulong)ReadOnlyList.Count)
        {
            value = ReadOnlyList[(int)key];
            return true;
        }

        value = default;
        return false;
    }

    public IEnumerator<KeyValuePair<ulong, T>> GetEnumerator()
    {
        return ReadOnlyList
            .Select((v, i) => new KeyValuePair<ulong, T>((ulong)i, v))
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ReadOnlyList
            .Select((v, i) => new KeyValuePair<ulong, T>((ulong)i, v))
            .GetEnumerator();
    }
}