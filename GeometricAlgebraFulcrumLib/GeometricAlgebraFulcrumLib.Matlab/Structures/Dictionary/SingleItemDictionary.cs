using System;
using System.Collections;
using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;

public sealed record SingleItemDictionary<TKey, TValue> :
    IReadOnlyDictionary<TKey, TValue> where TKey : IEquatable<TKey>
{
    public int Count 
        => 1;
        
    public TValue this[TKey key] 
        => key.Equals(Key) ? Value : throw new KeyNotFoundException();

    public IEnumerable<TKey> Keys
    {
        
        get { yield return Key; }
    }

    public IEnumerable<TValue> Values 
    {
        
        get { yield return Value; }
    }


    public TKey Key { get; }

    public TValue Value { get; }


    public SingleItemDictionary(KeyValuePair<TKey, TValue> keyValuePair) 
        : this(keyValuePair.Key, keyValuePair.Value)
    {
    }

    public SingleItemDictionary(TKey Key, TValue Value)
    {
        this.Key = Key;
        this.Value = Value;
    }


    
    public bool ContainsKey(TKey key)
    {
        return key.Equals(Key);
    }

    
    public bool TryGetValue(TKey key, out TValue value)
    {
        if (key.Equals(Key))
        {
            value = Value;
            return true;
        }

        value = default;
        return false;
    }

        
    
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        yield return new KeyValuePair<TKey, TValue>(Key, Value);
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Deconstruct(out TKey Key, out TValue Value)
    {
        Key = this.Key;
        Value = this.Value;
    }
}