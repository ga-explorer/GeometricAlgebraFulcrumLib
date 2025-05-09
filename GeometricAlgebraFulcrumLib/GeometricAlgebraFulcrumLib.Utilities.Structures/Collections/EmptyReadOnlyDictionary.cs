using System.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;

public sealed class EmptyReadOnlyDictionary<TU, TV> 
    : IReadOnlyDictionary<TU, TV>
{
    public int Count 
        => 0;

    public TV this[TU key]
        => throw new KeyNotFoundException();

    public IEnumerable<TU> Keys
        => [];

    public IEnumerable<TV> Values
        => [];


    public bool ContainsKey(TU key)
    {
        return false;
    }

    public bool TryGetValue(TU key, out TV value)
    {
        value = default;
        return false;
    }

    public IEnumerator<KeyValuePair<TU, TV>> GetEnumerator()
    {
        yield break;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}