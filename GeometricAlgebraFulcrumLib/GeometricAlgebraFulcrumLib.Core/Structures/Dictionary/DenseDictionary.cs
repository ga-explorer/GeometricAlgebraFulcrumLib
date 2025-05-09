using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Dictionary;

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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DenseDictionary(IReadOnlyList<TValue> valueList, Func<TKey, int> keyToIntegerMapping, Func<int, TKey> integerToKeyMapping)
    {
        ValueList = valueList;
        KeyToIntegerMapping = keyToIntegerMapping;
        IntegerToKeyMapping = integerToKeyMapping;
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
        var index = KeyToIntegerMapping(key);

        if (index >= 0 && index <= Count)
        {
            value = ValueList[index];
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
                    ValueList[index]
                )
            ).GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}