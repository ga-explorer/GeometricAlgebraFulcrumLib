using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;

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


    
    public RepeatedItemInt32Dictionary(int count, TValue value)
    {
        Count = count;
        Value = value;
    }

    
    public void Deconstruct(out int count, out TValue value)
    {
        count = Count;
        value = Value;
    }

    
    
    public bool ContainsKey(int key)
    {
        return key >= 0 && key <= Count;
    }

    
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

        
    
    public IEnumerator<KeyValuePair<int, TValue>> GetEnumerator()
    {
        return Count
            .GetRange()
            .Select(index => new KeyValuePair<int,TValue>(index, Value))
            .GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}