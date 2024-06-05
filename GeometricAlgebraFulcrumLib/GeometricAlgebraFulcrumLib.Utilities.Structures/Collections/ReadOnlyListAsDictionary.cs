using System.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;

public sealed class ReadOnlyListAsReadOnlyDictionary<T> : IReadOnlyDictionary<int, T>
{
    public IReadOnlyList<T> ReadOnlyList { get; }

    public int Count 
        => ReadOnlyList.Count;

    public T this[int key]
    {
        get
        {
            if (key < 0 || key >= ReadOnlyList.Count)
                throw new KeyNotFoundException();

            return ReadOnlyList[key];
        }
    }

    public IEnumerable<int> Keys
        => Enumerable.Range(0, ReadOnlyList.Count);

    public IEnumerable<T> Values
        => ReadOnlyList;


    public ReadOnlyListAsReadOnlyDictionary(IReadOnlyList<T> readOnlyList)
    {
        ReadOnlyList = readOnlyList;
    }


    public bool ContainsKey(int key)
    {
        return key >= 0 && key < ReadOnlyList.Count;
    }

    public bool TryGetValue(int key, out T value)
    {
        if (key >= 0 && key < ReadOnlyList.Count)
        {
            value = ReadOnlyList[key];
            return true;
        }

        value = default;
        return false;
    }

    public IEnumerator<KeyValuePair<int, T>> GetEnumerator()
    {
        return ReadOnlyList
            .Select((v, i) => new KeyValuePair<int, T>(i, v))
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ReadOnlyList
            .Select((v, i) => new KeyValuePair<int, T>(i, v))
            .GetEnumerator();
    }
}