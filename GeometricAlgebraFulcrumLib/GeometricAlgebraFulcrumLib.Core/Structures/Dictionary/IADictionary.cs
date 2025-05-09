namespace GeometricAlgebraFulcrumLib.Core.Structures.Dictionary;

public interface IADictionary<TKey, TValue> : 
    IDictionary<TKey, TValue>
{
    void AddOrSetValue(TKey key, TValue value);

    bool TryAdd(TKey key, TValue value);

    bool TrySetValue(TKey key, TValue value);

    bool TrySwapValues(TKey key1, TKey key2);

    void SwapValues(TKey key1, TKey key2);
}