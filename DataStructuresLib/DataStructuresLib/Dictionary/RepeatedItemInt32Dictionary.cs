using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;

namespace DataStructuresLib.Dictionary;

public sealed record RepeatedItemInt32Dictionary<TValue> :
    IReadOnlyDictionary<int, TValue>
{
    public int Count { get; }
        
    public TValue Value { get; }
    
    public TValue this[int key] =>
        key >= 0 && key < Count 
            ? Value 
            : throw new KeyNotFoundException();

    public IEnumerable<int> Keys 
        => Count.GetRange();

    public IEnumerable<TValue> Values
        => Enumerable.Repeat(Value, Count);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RepeatedItemInt32Dictionary(int count, TValue value)
    {
        Count = count;
        Value = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out int count, out TValue value)
    {
        count = Count;
        value = Value;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(int key)
    {
        return key >= 0 && key <= Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(int key, out TValue value)
    {
        if (key >= 0 && key <= Count)
        {
            value = Value;
            return true;
        }

        value = default;
        return false;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<int, TValue>> GetEnumerator()
    {
        return Count
            .GetRange()
            .Select(index => new KeyValuePair<int,TValue>(index, Value))
            .GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}