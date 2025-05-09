using System.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures;

public sealed class SparseTable<TKey, TValue> : IDictionary<TKey, TValue> where TKey : notnull
{
    private readonly Dictionary<TKey, TValue> _dictionary = 
        new Dictionary<TKey, TValue>();


    public TValue DefaultValue { get; }

    public Func<TValue, bool> IsDefaultValue { get; }

    public int Count => _dictionary.Count;

    public bool IsReadOnly => false;

    public TValue this[TKey key]
    {
        get => _dictionary.TryGetValue(key, out var value) ? value : DefaultValue;
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

    public ICollection<TKey> Keys => _dictionary.Keys;

    public ICollection<TValue> Values => _dictionary.Values;


    public SparseTable(Func<TValue, bool> isDefaultValue)
    {
        if (ReferenceEquals(isDefaultValue, null))
            throw new ArgumentNullException(nameof(isDefaultValue));

        DefaultValue = default(TValue);
        IsDefaultValue = isDefaultValue;
    }

    public SparseTable(TValue defaultValue, Func<TValue, bool> isDefaultValue)
    {
        if (ReferenceEquals(isDefaultValue, null))
            throw new ArgumentNullException(nameof(isDefaultValue));

        DefaultValue = defaultValue;
        IsDefaultValue = isDefaultValue;
    }


    public void Add(KeyValuePair<TKey, TValue> item)
    {
        _dictionary.Add(item.Key, item.Value);
    }

    public void Clear()
    {
        _dictionary.Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        if (!_dictionary.TryGetValue(item.Key, out var value))
            return IsDefaultValue(item.Value);

        return item.Value.Equals(value);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        foreach (var pair in _dictionary)
            array[arrayIndex++] = pair;
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        throw new NotImplementedException();
    }

    public bool ContainsKey(TKey key)
    {
        return _dictionary.ContainsKey(key);
    }

    public void Add(TKey key, TValue value)
    {
        if (!IsDefaultValue(value))
            _dictionary.Add(key, value);

        if (_dictionary.ContainsKey(key))
            throw new ArgumentException(nameof(key));
    }

    public bool Remove(TKey key)
    {
        return _dictionary.Remove(key);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        return _dictionary.TryGetValue(key, out value);
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return _dictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _dictionary.GetEnumerator();
    }

    public override string ToString()
    {
        return _dictionary.ToString();
    }
}