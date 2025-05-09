using System.Collections;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;

public sealed record EmptyDictionary<TKey, TValue> :
    IReadOnlyDictionary<TKey, TValue> where TKey : IEquatable<TKey>
{
    public int Count 
        => 0;
        
    public TValue this[TKey key] 
        => throw new KeyNotFoundException();

    public IEnumerable<TKey> Keys
        => [];

    public IEnumerable<TValue> Values 
        => [];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(TKey key)
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(TKey key, out TValue value)
    {
        value = default;
        return false;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}