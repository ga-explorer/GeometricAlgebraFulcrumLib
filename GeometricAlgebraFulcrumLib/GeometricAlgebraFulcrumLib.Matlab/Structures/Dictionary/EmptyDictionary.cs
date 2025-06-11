using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;

public sealed record EmptyDictionary<TKey, TValue> :
    IReadOnlyDictionary<TKey, TValue> where TKey : IEquatable<TKey>
{
    public int Count 
        => 0;
        
    public TValue this[TKey key] 
        => throw new KeyNotFoundException();

    public IEnumerable<TKey> Keys
        => [];

    public IEnumerable<TValue> Values 
        => [];


    
    public bool ContainsKey(TKey key)
    {
        return false;
    }

    
    public bool TryGetValue(TKey key, out TValue value)
    {
        value = default;
        return false;
    }

        
    
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}