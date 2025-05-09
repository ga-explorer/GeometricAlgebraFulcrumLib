using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Dictionary;

public sealed record SingleItemDictionary<TKey, TValue>([NotNull] TKey Key, [NotNull] TValue Value) :
    IReadOnlyDictionary<TKey, TValue> where TKey : IEquatable<TKey>
{
    public int Count 
        => 1;
        
    public TValue this[TKey key] 
        => key.Equals(Key) ? Value : throw new KeyNotFoundException();

    public IEnumerable<TKey> Keys
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get { yield return Key; }
    }

    public IEnumerable<TValue> Values 
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get { yield return Value; }
    }


    public SingleItemDictionary(KeyValuePair<TKey, TValue> keyValuePair) 
        : this(keyValuePair.Key, keyValuePair.Value)
    {
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(TKey key)
    {
        return key.Equals(Key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(TKey key, out TValue value)
    {
        if (key.Equals(Key))
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
        yield return new KeyValuePair<TKey, TValue>(Key, Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}