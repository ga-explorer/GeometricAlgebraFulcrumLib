using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;

public sealed record RepeatedItemDictionary<TKey, TValue> :
    IReadOnlyDictionary<TKey, TValue> where TKey : IEquatable<TKey>
{
    public int Count { get; }
        
    public TValue Value { get; }

    public Func<TKey, int> KeyToIntegerMapping { get; }
        
    public Func<int, TKey> IntegerToKeyMapping { get; }

    public TValue this[TKey key]
    {
        get
        {
            var index = KeyToIntegerMapping(key);

            return index >= 0 && index < Count 
                ? Value 
                : throw new KeyNotFoundException();
        }
    }

    public IEnumerable<TKey> Keys 
        => Count.GetRange().Select(IntegerToKeyMapping);

    public IEnumerable<TValue> Values
        => Enumerable.Repeat(Value, Count);


    
    public RepeatedItemDictionary(int count, TValue value, Func<TKey, int> keyToIntegerMapping, Func<int, TKey> integerToKeyMapping)
    {
        Count = count;
        Value = value;
        KeyToIntegerMapping = keyToIntegerMapping;
        IntegerToKeyMapping = integerToKeyMapping;
    }

    
    public void Deconstruct(out int count, out TValue value)
    {
        count = Count;
        value = Value;
    }

        
    
    public bool ContainsKey(TKey key)
    {
        var index = KeyToIntegerMapping(key);

        return index >= 0 && index <= Count;
    }

    
    public bool TryGetValue(TKey key, out TValue value)
    {
        if (ContainsKey(key))
        {
            value = Value;
            return true;
        }

        value = default;
        return false;
    }

        
    
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return Count
            .GetRange()
            .Select(index => new KeyValuePair<TKey,TValue>(
                    IntegerToKeyMapping(index), 
                    Value
                )
            ).GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}