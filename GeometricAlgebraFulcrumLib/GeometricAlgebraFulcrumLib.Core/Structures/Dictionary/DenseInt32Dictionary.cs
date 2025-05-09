using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Dictionary;

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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DenseInt32Dictionary(IReadOnlyList<TValue> valueList)
    {
        ValueList = valueList;
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
            value = ValueList[key];
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
            .Select(index => new KeyValuePair<int, TValue>(
                    index, 
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