using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DataStructuresLib.Dictionary;

public sealed class TextLookupDictionary : 
    IDictionary<string, string>
{
    private readonly Dictionary<string, string> _textDictionary
        = new Dictionary<string, string>();


    public int Count 
        => _textDictionary.Count;

    public bool IsReadOnly 
        => false;
        
    public string this[string key]
    {
        get
        {
            if (string.IsNullOrEmpty(key))
                throw new KeyNotFoundException();

            return _textDictionary[key];
        }
        set
        {
            if (string.IsNullOrEmpty(key))
                throw new KeyNotFoundException();

            if (string.IsNullOrEmpty(value))
                _textDictionary.Remove(key);

            else if (_textDictionary.ContainsKey(key))
                _textDictionary[key] = value;

            else
                _textDictionary.Add(key, value);
        }
    }

    public ICollection<string> Keys 
        => _textDictionary.Keys;

    public ICollection<string> Values 
        => _textDictionary.Values;
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(KeyValuePair<string, string> item)
    {
        var (key, value) = item;

        this[key] = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        _textDictionary.Clear();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(KeyValuePair<string, string> item)
    {
        var (key, value) = item;

        return _textDictionary.TryGetValue(key, out var value1) && value == value1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
    {
        var i = arrayIndex;
        foreach (var pair in _textDictionary)
            array[i++] = pair;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove(KeyValuePair<string, string> item)
    {
        var (key, value) = item;

        return _textDictionary.TryGetValue(key, out var value1) && 
               value == value1 &&
               _textDictionary.Remove(key);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(string key, string value)
    {
        this[key] = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(string key)
    {
        return _textDictionary.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove(string key)
    {
        return _textDictionary.Remove(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(string key, out string value)
    {
        return _textDictionary.TryGetValue(key, out value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        return _textDictionary.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}