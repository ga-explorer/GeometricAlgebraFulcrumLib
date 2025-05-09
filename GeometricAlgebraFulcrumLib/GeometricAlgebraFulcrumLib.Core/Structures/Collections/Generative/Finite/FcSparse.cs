using System.Collections;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections.Generative.Finite;

/// <summary>
/// This class represents a sparse finite collection of elements. The missing elements 
/// are assumed to have values from an underlying base finite collection.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class FcSparse<T> : FiniteCollection<T>, IDictionary<int, T>
{
    /// <summary>
    /// Create a sparse finite collection of elements
    /// </summary>
    /// <param name="baseCollection"></param>
    /// <returns></returns>
    public static FcSparse<T> Create(FiniteCollection<T> baseCollection)
    {
        return new FcSparse<T>(baseCollection);
    }

    /// <summary>
    /// Create a sparse finite collection of elements
    /// </summary>
    /// <param name="minIndex"></param>
    /// <param name="maxIndex"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static FcSparse<T> Create(T defaultValue, int minIndex, int maxIndex)
    {
        return new FcSparse<T>(FcConstant<T>.Create(defaultValue, minIndex, maxIndex));
    }


    private readonly SortedDictionary<int, T> _itemsDictionary =
        new SortedDictionary<int, T>();


    public bool IsReadOnly => false;

    public override int Count => BaseCollection.Count;

    public override int MinIndex => BaseCollection.MinIndex;

    public override int MaxIndex => BaseCollection.MaxIndex;

    /// <summary>
    /// The base collection holding default values for this sparse collection
    /// </summary>
    public FiniteCollection<T> BaseCollection { get; private set; }

    /// <summary>
    /// Get or set an element in this sparse collection
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public T this[int index]
    {
        get
        {
            T value;

            return
                _itemsDictionary.TryGetValue(index, out value)
                    ? value : BaseCollection == null ? DefaultValue : BaseCollection.GetItem(index);
        }

        set
        {
            if (_itemsDictionary.ContainsKey(index))
                _itemsDictionary[index] = value;
            else
                _itemsDictionary.Add(index, value);
        }
    }

    public ICollection<int> Keys => _itemsDictionary.Keys;

    public ICollection<T> Values => _itemsDictionary.Values;


    private FcSparse(FiniteCollection<T> baseCollection)
    {
        if (baseCollection == null)
            throw new ArgumentNullException(nameof(baseCollection));

        BaseCollection = baseCollection;
    }


    /// <summary>
    /// Reset an item in this collection to its default value from the base collection
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public FcSparse<T> ResetItem(int index)
    {
        _itemsDictionary.Remove(index);

        return this;
    }

    public void Add(KeyValuePair<int, T> item)
    {
        _itemsDictionary.Add(item.Key, item.Value);
    }

    public void Clear()
    {
        _itemsDictionary.Clear();
    }

    public bool Contains(KeyValuePair<int, T> item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<int, T>[] array, int arrayIndex)
    {
        _itemsDictionary.CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<int, T> item)
    {
        return _itemsDictionary.Remove(item.Key);
    }

    public bool ContainsKey(int key)
    {
        return _itemsDictionary.ContainsKey(key);
    }

    public void Add(int key, T value)
    {
        _itemsDictionary.Add(key, value);
    }

    public bool Remove(int key)
    {
        return _itemsDictionary.Remove(key);
    }

    public bool TryGetValue(int key, out T value)
    {
        return _itemsDictionary.TryGetValue(key, out value);
    }

    /// <summary>
    /// Reset the specs of this sparse collection
    /// </summary>
    /// <param name="baseCollection"></param>
    /// <param name="clearData"></param>
    /// <returns></returns>
    public FcSparse<T> Reset(FiniteCollection<T> baseCollection, bool clearData)
    {
        if (baseCollection == null)
            throw new ArgumentNullException(nameof(baseCollection));

        BaseCollection = baseCollection;

        if (clearData)
            _itemsDictionary.Clear();

        return this;
    }

    public override T GetItem(int index)
    {
        T value;

        return
            _itemsDictionary.TryGetValue(index, out value)
                ? value : BaseCollection == null ? DefaultValue : BaseCollection.GetItem(index);
    }

    IEnumerator<KeyValuePair<int, T>> IEnumerable<KeyValuePair<int, T>>.GetEnumerator()
    {
        return _itemsDictionary.GetEnumerator();
    }

    public new IEnumerator GetEnumerator()
    {
        return _itemsDictionary.GetEnumerator();
    }
}