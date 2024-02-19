using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructuresLib.SparseTable;

public class SparseTable3D<TKey1, TKey2, TKey3, TValue>
    : IEnumerable<KeyValuePair<Tuple<TKey1, TKey2, TKey3>, TValue>>
{
    private readonly Dictionary<Tuple<TKey1, TKey2, TKey3>, TValue> _dictionary =
        new Dictionary<Tuple<TKey1, TKey2, TKey3>, TValue>();


    public TValue DefaultValue { get; set; }

    public TValue this[TKey1 key1, TKey2 key2, TKey3 key3]
    {
        get =>
            _dictionary.TryGetValue(new Tuple<TKey1, TKey2, TKey3>(key1, key2, key3), out var value)
                ? value : DefaultValue;
        set
        {
            var key = new Tuple<TKey1, TKey2, TKey3>(key1, key2, key3);

            if (IsDefaultValue(value))
            {
                _dictionary.Remove(key);

                return;
            }

            if (_dictionary.ContainsKey(key))
                _dictionary[key] = value;
            else
                _dictionary.Add(key, value);
        }
    }

    public int Count => _dictionary.Count;

    public ICollection<Tuple<TKey1, TKey2, TKey3>> Keys => _dictionary.Keys;

    public ICollection<TValue> Values => _dictionary.Values;


    public SparseTable3D()
    {
        DefaultValue = default(TValue);
    }

    public SparseTable3D(TValue defaultValue)
    {
        DefaultValue = defaultValue;
    }

    public SparseTable3D(IEnumerable<KeyValuePair<Tuple<TKey1, TKey2, TKey3>, TValue>> pairs)
    {
        DefaultValue = default(TValue);

        foreach (var pair in pairs)
            this[pair.Key.Item1, pair.Key.Item2, pair.Key.Item3] = pair.Value;
    }

    public SparseTable3D(TValue defaultValue, IEnumerable<KeyValuePair<Tuple<TKey1, TKey2, TKey3>, TValue>> pairs)
    {
        DefaultValue = defaultValue;

        foreach (var pair in pairs)
            this[pair.Key.Item1, pair.Key.Item2, pair.Key.Item3] = pair.Value;
    }


    public Tuple<TKey1, TKey2, TKey3> GetKeyTuple(TKey1 key1, TKey2 key2, TKey3 key3)
    {
        return new Tuple<TKey1, TKey2, TKey3>(key1, key2, key3);
    }

    public virtual bool IsDefaultValue(TValue value)
    {
        return value.Equals(DefaultValue);
    }

    public SparseTable3D<TKey1, TKey2, TKey3, TValue> Clear()
    {
        _dictionary.Clear();

        return this;
    }

    public bool ContainsKey(TKey1 key1, TKey2 key2, TKey3 key3)
    {
        return _dictionary.ContainsKey(new Tuple<TKey1, TKey2, TKey3>(key1, key2, key3));
    }

    public bool TryGetValue(TKey1 key1, TKey2 key2, TKey3 key3, out TValue value)
    {
        return _dictionary.TryGetValue(new Tuple<TKey1, TKey2, TKey3>(key1, key2, key3), out value);
    }

    public SparseTable3D<TKey1, TKey2, TKey3, TValue> Remove(TKey1 key1, TKey2 key2, TKey3 key3)
    {
        _dictionary.Remove(new Tuple<TKey1, TKey2, TKey3>(key1, key2, key3));

        return this;
    }

    public IDictionary<Tuple<TKey1, TKey2, TKey3>, TValue> ToDictionary()
    {
        return _dictionary;
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