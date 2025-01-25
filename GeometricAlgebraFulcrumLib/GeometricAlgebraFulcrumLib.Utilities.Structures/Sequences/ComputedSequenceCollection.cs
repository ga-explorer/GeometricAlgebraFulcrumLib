using System.Collections;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Sequences;

public class ComputedSequenceCollection<T> :
    IReadOnlyDictionary<string, int>
{
    private readonly Dictionary<string, int> _keyIndexDictionary
        = new Dictionary<string, int>();


    public Func<string, int, T> KeyIndexMapping { get; }
    
    public int Count 
        => _keyIndexDictionary.Count;

    public int this[string key] 
        => _keyIndexDictionary.GetValueOrDefault(key, 0);

    public IEnumerable<string> Keys 
        => _keyIndexDictionary.Keys;

    public IEnumerable<int> Values 
        => _keyIndexDictionary.Values;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComputedSequenceCollection(Func<string, int, T> keyIndexMapping)
    {
        KeyIndexMapping = keyIndexMapping;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _keyIndexDictionary.Clear();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset(string key)
    {
        _keyIndexDictionary.Remove(key);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(string key)
    {
        return _keyIndexDictionary.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(string key, out int value)
    {
        return _keyIndexDictionary.TryGetValue(key, out value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetIndex(string key, int index)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (_keyIndexDictionary.ContainsKey(key))
            _keyIndexDictionary[key] = index;
        else
            _keyIndexDictionary.Add(key, index);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetNextItem(string key)
    {
        if (_keyIndexDictionary.TryGetValue(key, out var index))
        {
            _keyIndexDictionary[key] += 1;

            return KeyIndexMapping(key, index);
        }

        _keyIndexDictionary.Add(key, 1);

        return KeyIndexMapping(key, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<string, int>> GetEnumerator()
    {
        return _keyIndexDictionary.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}