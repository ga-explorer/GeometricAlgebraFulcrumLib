using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;

public sealed class DenseInt32Dictionary<TValue> :
    IReadOnlyDictionary<int, TValue>
{
    public int Count 
        => ValueList.Count;
        
    public IReadOnlyList<TValue> ValueList { get; }
    
    public TValue this[int key] 
        => ValueList[key];

    public IEnumerable<int> Keys 
        => Count.GetRange();

    public IEnumerable<TValue> Values
        => ValueList;


    
    public DenseInt32Dictionary(IReadOnlyList<TValue> valueList)
    {
        ValueList = valueList;
    }
    
        
    
    public bool ContainsKey(int key)
    {
        return key >= 0 && key <= Count;
    }

    
    public bool TryGetValue(int key, out TValue value)
    {
        if (key >= 0 && key <= Count)
        {
            value = ValueList[key];
            return true;
        }

        value = default;
        return false;
    }

        
    
    public IEnumerator<KeyValuePair<int, TValue>> GetEnumerator()
    {
        return Count
            .GetRange()
            .Select(index => new KeyValuePair<int, TValue>(
                    index, 
                    ValueList[index]
                )
            ).GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}