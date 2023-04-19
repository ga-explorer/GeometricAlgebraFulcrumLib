using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;

namespace DataStructuresLib.Dictionary;

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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RepeatedItemDictionary(int count, TValue value, Func<TKey, int> keyToIntegerMapping, Func<int, TKey> integerToKeyMapping)
    {
        Count = count;
        Value = value;
        KeyToIntegerMapping = keyToIntegerMapping;
        IntegerToKeyMapping = integerToKeyMapping;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out int count, out TValue value)
    {
        count = Count;
        value = Value;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(TKey key)
    {
        var index = KeyToIntegerMapping(key);

        return index >= 0 && index <= Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}