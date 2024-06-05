namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;

public class ADictionary<TKey, TValue> : 
    Dictionary<TKey, TValue>, 
    IADictionary<TKey, TValue>
{
    public void AddOrSetValue(TKey key, TValue value)
    {
        if (ContainsKey(key))
            this[key] = value;

        else
            Add(key, value);
    }

    public bool TryAdd(TKey key, TValue value)
    {
        if (ContainsKey(key))
            return false;

        Add(key, value);

        return true;
    }

    public bool TrySetValue(TKey key, TValue value)
    {
        if (ContainsKey(key) == false)
            return false;

        this[key] = value;

        return true;
    }

    public bool TrySwapValues(TKey key1, TKey key2)
    {
        if (ContainsKey(key1) == false || ContainsKey(key2) == false)
            return false;

        var value = this[key1];
        this[key1] = this[key2];
        this[key2] = value;

        return true;
    }

    public void SwapValues(TKey key1, TKey key2)
    {
        var value = this[key1];
        this[key1] = this[key2];
        this[key2] = value;
    }
}