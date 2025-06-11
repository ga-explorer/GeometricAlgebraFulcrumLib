using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;

public sealed class DenseDictionary<TKey, TValue> :
    IReadOnlyDictionary<TKey, TValue> where TKey : IEquatable<TKey>
{
    public int Count 
        => ValueList.Count;
        
    public IReadOnlyList<TValue> ValueList { get; }

    public Func<TKey, int> KeyToIntegerMapping { get; }
        
    public Func<int, TKey> IntegerToKeyMapping { get; }

    public TValue this[TKey key]
    {
        get
        {
            var index = KeyToIntegerMapping(key);

            return ValueList[index];
        }
    }

    public IEnumerable<TKey> Keys 
        => Count.GetRange().Select(IntegerToKeyMapping);

    public IEnumerable<TValue> Values
        => ValueList;


    
    public DenseDictionary(IReadOnlyList<TValue> valueList, Func<TKey, int> keyToIntegerMapping, Func<int, TKey> integerToKeyMapping)
    {
        ValueList = valueList;
        KeyToIntegerMapping = keyToIntegerMapping;
        IntegerToKeyMapping = integerToKeyMapping;
    }
    
        
    
    public bool ContainsKey(TKey key)
    {
        var index = KeyToIntegerMapping(key);

        return index >= 0 && index <= Count;
    }

    
    public bool TryGetValue(TKey key, out TValue value)
    {
        var index = KeyToIntegerMapping(key);

        if (index >= 0 && index <= Count)
        {
            value = ValueList[index];
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
                    ValueList[index]
                )
            ).GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}