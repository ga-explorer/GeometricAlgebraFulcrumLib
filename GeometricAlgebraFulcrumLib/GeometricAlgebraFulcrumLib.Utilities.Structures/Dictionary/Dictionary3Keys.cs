using System.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;

/// <inheritdoc />
/// <summary>
/// Implements a 3 key dictionary using Tuple structures for keys.
/// The type of keys is the same
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public sealed class Dictionary3Keys<TKey, TValue> : 
    IReadOnlyCollection<KeyValuePair<Tuple<TKey, TKey, TKey>, TValue>>
{
    private readonly Dictionary<Tuple<TKey, TKey, TKey>, TValue> _dictionary =
        new Dictionary<Tuple<TKey, TKey, TKey>, TValue>();


    public int Count => _dictionary.Count;

    public ICollection<Tuple<TKey, TKey, TKey>> Keys => _dictionary.Keys;

    public ICollection<TValue> Values => _dictionary.Values;

    public TValue this[TKey key1, TKey key2, TKey key3]
    {
        get => _dictionary[new Tuple<TKey, TKey, TKey>(key1, key2, key3)];
        set => _dictionary[new Tuple<TKey, TKey, TKey>(key1, key2, key3)] = value;
    }


    public Dictionary3Keys<TKey, TValue> Clear()
    {
        _dictionary.Clear();

        return this;
    }

    public Dictionary3Keys<TKey, TValue> Add(TKey key1, TKey key2, TKey key3, TValue value)
    {
        _dictionary.Add(new Tuple<TKey, TKey, TKey>(key1, key2, key3), value);

        return this;
    }

    public bool TryGetValue(TKey key1, TKey key2, TKey key3, out TValue value)
    {
        return _dictionary.TryGetValue(new Tuple<TKey, TKey, TKey>(key1, key2, key3), out value);
    }

    public bool Remove(TKey key1, TKey key2, TKey key3)
    {
        return _dictionary.Remove(new Tuple<TKey, TKey, TKey>(key1, key2, key3));
    }

    public bool ContainsKey(TKey key1, TKey key2, TKey key3)
    {
        return _dictionary.ContainsKey(new Tuple<TKey, TKey, TKey>(key1, key2, key3));
    }

    public IEnumerator<KeyValuePair<Tuple<TKey, TKey, TKey>, TValue>> GetEnumerator()
    {
        return _dictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _dictionary.GetEnumerator();
    }
}


/// <inheritdoc />
/// <summary>
/// Implements a 3 key dictionary using Tuple structures for keys.
/// The types of keys can be different
/// </summary>
/// <typeparam name="TKey1"></typeparam>
/// <typeparam name="TKey2"></typeparam>
/// <typeparam name="TKey3"></typeparam>
/// <typeparam name="TValue"></typeparam>
public sealed class Dictionary3Keys<TKey1, TKey2, TKey3, TValue> : IReadOnlyCollection<KeyValuePair<Tuple<TKey1, TKey2, TKey3>, TValue>>
{
    private readonly Dictionary<Tuple<TKey1, TKey2, TKey3>, TValue> _dictionary =
        new Dictionary<Tuple<TKey1, TKey2, TKey3>, TValue>();


    public int Count => _dictionary.Count;

    public ICollection<Tuple<TKey1, TKey2, TKey3>> Keys => _dictionary.Keys;

    public ICollection<TValue> Values => _dictionary.Values;

    public TValue this[TKey1 key1, TKey2 key2, TKey3 key3]
    {
        get => _dictionary[new Tuple<TKey1, TKey2, TKey3>(key1, key2, key3)];
        set => _dictionary[new Tuple<TKey1, TKey2, TKey3>(key1, key2, key3)] = value;
    }


    public Dictionary3Keys<TKey1, TKey2, TKey3, TValue> Clear()
    {
        _dictionary.Clear();

        return this;
    }

    public Dictionary3Keys<TKey1, TKey2, TKey3, TValue> Add(TKey1 key1, TKey2 key2, TKey3 key3, TValue value)
    {
        _dictionary.Add(new Tuple<TKey1, TKey2, TKey3>(key1, key2, key3), value);

        return this;
    }

    public bool TryGetValue(TKey1 key1, TKey2 key2, TKey3 key3, out TValue value)
    {
        return _dictionary.TryGetValue(new Tuple<TKey1, TKey2, TKey3>(key1, key2, key3), out value);
    }

    public bool Remove(TKey1 key1, TKey2 key2, TKey3 key3)
    {
        return _dictionary.Remove(new Tuple<TKey1, TKey2, TKey3>(key1, key2, key3));
    }

    public bool ContainsKey(TKey1 key1, TKey2 key2, TKey3 key3)
    {
        return _dictionary.ContainsKey(new Tuple<TKey1, TKey2, TKey3>(key1, key2, key3));
    }

    public IEnumerator<KeyValuePair<Tuple<TKey1, TKey2, TKey3>, TValue>> GetEnumerator()
    {
        return _dictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _dictionary.GetEnumerator();
    }
}