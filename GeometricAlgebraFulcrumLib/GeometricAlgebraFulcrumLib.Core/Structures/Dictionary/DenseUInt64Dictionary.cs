using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Dictionary;

public sealed class DenseUInt64Dictionary<TValue> :
    IReadOnlyDictionary<ulong, TValue>
{
    public int Count 
        => ValueList.Count;
        
    public IReadOnlyList<TValue> ValueList { get; }
    
    public TValue this[ulong key] 
        => ValueList[(int) key];

    public IEnumerable<ulong> Keys 
        => Count.GetRange(k => (ulong) k);

    public IEnumerable<TValue> Values
        => ValueList;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DenseUInt64Dictionary(IReadOnlyList<TValue> valueList)
    {
        ValueList = valueList;
    }
    
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(ulong key)
    {
        return key <= (ulong) Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(ulong key, out TValue value)
    {
        var index = (int) key;

        if (index <= Count)
        {
            value = ValueList[index];
            return true;
        }

        value = default;
        return false;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<ulong, TValue>> GetEnumerator()
    {
        return Count
            .GetRange()
            .Select(index => new KeyValuePair<ulong, TValue>(
                    (ulong) index, 
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