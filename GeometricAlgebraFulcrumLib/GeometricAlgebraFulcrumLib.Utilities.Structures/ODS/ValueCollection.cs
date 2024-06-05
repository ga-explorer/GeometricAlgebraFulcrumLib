namespace GeometricAlgebraFulcrumLib.Utilities.Structures.ODS;

public class ValueCollection<T> : ICollection<T>
{
    private readonly IDictionary<uint, T> _tree;

    internal ValueCollection(IDictionary<uint, T> dict)
    {
        _tree = dict;
    }

    public IEnumerator<T> GetEnumerator()
    {
        using var iter = _tree.GetEnumerator();
        while (iter.MoveNext())
            yield return iter.Current.Value;
    }

    public bool IsReadOnly => true;

    #region implicits

    int ICollection<T>.Count => _tree.Count;

    void ICollection<T>.Add(T item)
    {
        throw new NotSupportedException();
    }

    void ICollection<T>.Clear()
    {
        throw new NotSupportedException();
    }

    bool ICollection<T>.Contains(T item)
    {
        return _tree.Any(kvp => kvp.Value.Equals(item));
    }

    void ICollection<T>.CopyTo(T[] array, int arrayIndex)
    {
        CollectionHelpers.ThrowIfInsufficientArray(this, array, arrayIndex);
        using var iter = _tree.GetEnumerator();
        for (var i = 0; i < _tree.Count; i++)
        {
            iter.MoveNext();
            array[i] = iter.Current.Value;
        }
    }

    bool ICollection<T>.Remove(T item)
    {
        throw new NotSupportedException();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion
        
}