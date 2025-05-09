using System.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.SparseTable;

public class SparseTable1D<TKey, TValue> 
    : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : notnull
{
    private readonly Dictionary<TKey, TValue> _dictionary =
        new Dictionary<TKey, TValue>();


    public TValue DefaultValue { get; set; }

    public TValue this[TKey key]
    {
        get =>
            _dictionary.GetValueOrDefault(key, DefaultValue);
        set
        {
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

    public ICollection<TKey> Keys => _dictionary.Keys;

    public ICollection<TValue> Values => _dictionary.Values;


    public SparseTable1D()
    {
        DefaultValue = default(TValue);
    }

    public SparseTable1D(TValue defaultValue)
    {
        DefaultValue = defaultValue;
    }

    public SparseTable1D(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
    {
        DefaultValue = default(TValue);

        foreach (var pair in pairs)
            this[pair.Key] = pair.Value;
    }

    public SparseTable1D(TValue defaultValue, IEnumerable<KeyValuePair<TKey, TValue>> pairs)
    {
        DefaultValue = defaultValue;

        foreach (var pair in pairs)
            this[pair.Key] = pair.Value;
    }


    public virtual bool IsDefaultValue(TValue value)
    {
        return value.Equals(DefaultValue);
    }

    public SparseTable1D<TKey, TValue> Clear()
    {
        _dictionary.Clear();

        return this;
    }

    public bool ContainsKey(TKey key)
    {
        return _dictionary.ContainsKey(key);
    }

    public bool TryGetValue(TKey key1, out TValue value)
    {
        return _dictionary.TryGetValue(key1, out value);
    }

    public SparseTable1D<TKey, TValue> Remove(TKey key)
    {
        _dictionary.Remove(key);

        return this;
    }

    public IDictionary<TKey, TValue> ToDictionary()
    {
        return _dictionary;
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return _dictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _dictionary.GetEnumerator();
    }
}