using System.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.ReadOnlyLists;

/// <summary>
/// A class holding a sparse set of items. Items must be of structure\value type
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class SparseValuesReadOnlyList<T> 
    : IReadOnlyList<T> where T : struct
{
    private int _size;

    private readonly SortedDictionary<int, T> _itemsDictionary
        = new SortedDictionary<int, T>();


    public T DefaultValue { get; }

    public T this[int index] 
    {
        get
        {
            if (index < 0 || index >= _size)
                throw new IndexOutOfRangeException();

            return _itemsDictionary.TryGetValue(index, out var value) 
                ? value : DefaultValue;
        } 
        set
        {
            if (index < 0 || index >= _size)
                throw new IndexOutOfRangeException();

            if (value.Equals(DefaultValue))
            {
                _itemsDictionary.Remove(index);

                return;
            }

            if (_itemsDictionary.ContainsKey(index))
                _itemsDictionary[index] = value;
            else
                _itemsDictionary.Add(index, value);
        }
    }

    public int Count 
        => _size;

    public int StoredItemsCount 
        => _itemsDictionary.Count;


    public SparseValuesReadOnlyList(int count, T defaultValue)
    {
        if (count < 0)
            throw new InvalidOperationException("Number of items must be a non-negative number");

        _size = count;
        DefaultValue = defaultValue;
    }

    public SparseValuesReadOnlyList(int count)
    {
        if (count < 0)
            throw new InvalidOperationException("Number of items must be a non-negative number");

        _size = count;
        DefaultValue = default;
    }


    public SparseValuesReadOnlyList<T> Clear()
    {
        _itemsDictionary.Clear();

        return this;
    }

    public SparseValuesReadOnlyList<T> Clear(int count)
    {
        if (count < 0)
            throw new InvalidOperationException("Number of items must be a non-negative number");

        _itemsDictionary.Clear();

        _size = count;

        return this;
    }

    public SparseValuesReadOnlyList<T> Resize(int count)
    {
        if (count < 0)
            throw new InvalidOperationException("Number of items must be a non-negative number");

        _size = count; 

        if (count > _size)
            return this;

        var indicesArray = 
            _itemsDictionary.Keys.Where(k => k >= count).ToArray();

        foreach (var index in indicesArray)
            _itemsDictionary.Remove(index);

        return this;
    }

    public SparseValuesReadOnlyList<T> Remove(int index)
    {
        _itemsDictionary.Remove(index);

        return this;
    }

    public SparseValuesReadOnlyList<T> Remove(params int[] indexList)
    {
        foreach (var index in indexList)
            _itemsDictionary.Remove(index);

        return this;
    }

    public SparseValuesReadOnlyList<T> Remove(IEnumerable<int> indexList)
    {
        foreach (var index in indexList)
            _itemsDictionary.Remove(index);

        return this;
    }


    public IEnumerator<T> GetEnumerator()
    {
        var i = 0;

        foreach (var pair in _itemsDictionary)
        {
            if (pair.Key > i)
            {
                for (var j = i; j < pair.Key; j++)
                    yield return DefaultValue;

                i = pair.Key;
            }

            yield return pair.Value; 

            i++;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        var i = 0;

        foreach (var pair in _itemsDictionary)
        {
            if (pair.Key > i)
            {
                for (var j = i; j < pair.Key; j++)
                    yield return DefaultValue;

                i = pair.Key;
            }

            yield return pair.Value; 

            i++;
        }
    }
}