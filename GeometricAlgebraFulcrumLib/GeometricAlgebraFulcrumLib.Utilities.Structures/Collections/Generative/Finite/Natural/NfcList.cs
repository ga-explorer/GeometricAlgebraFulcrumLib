namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Generative.Finite.Natural;

/// <summary>
/// This class represents a collection stored internally in a list. It can also be
/// used as a stack or queue through its methods
/// </summary>
/// <typeparam name="T"></typeparam>
public class NfcList<T> : NaturalFiniteCollection<T>, IList<T>
{
    public static NfcList<T> Create()
    {
        return new NfcList<T>();
    }

    public static NfcList<T> Create(int capacity)
    {
        return new NfcList<T>(capacity);
    }

    public static NfcList<T> CreateFromList(params T[] items)
    {
        return new NfcList<T>(items);
    }

    public static NfcList<T> CreateFromList(IEnumerable<T> items)
    {
        return new NfcList<T>(items);
    }


    private readonly List<T> _itemsList;


    public override int Count => _itemsList.Count;

    public bool IsReadOnly => false;

    public T this[int index]
    {
        get => _itemsList[index];
        set => _itemsList[index] = value;
    }


    private NfcList()
    {
        _itemsList = new List<T>();
    }

    private NfcList(int capacity)
    {
        _itemsList = new List<T>(capacity);
    }

    private NfcList(IEnumerable<T> items)
    {
        _itemsList = new List<T>(items);
    }


    public NfcList<T> Push(T value)
    {
        _itemsList.Add(value);

        return this;
    }

    public T Pop()
    {
        var index = _itemsList.Count - 1;
        var value = _itemsList[index];

        _itemsList.RemoveAt(index);

        return value;
    }

    public T Peek()
    {
        return _itemsList.Count > 0 
            ? _itemsList[_itemsList.Count - 1]
            : default;
    }

    public NfcList<T> Enqueue(T value)
    {
        _itemsList.Insert(0, value);

        return this;
    }

    public T Dequeue()
    {
        var index = _itemsList.Count - 1;
        var value = _itemsList[index];

        _itemsList.RemoveAt(index);

        return value;
    }

    public override T GetItem(int index)
    {
        return _itemsList[index];
    }

    public int IndexOf(T item)
    {
        return _itemsList.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        _itemsList.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        _itemsList.RemoveAt(index);
    }

    public void Add(T item)
    {
        _itemsList.Add(item);
    }

    public void Clear()
    {
        _itemsList.Clear();
    }

    public bool Contains(T item)
    {
        return _itemsList.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _itemsList.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        return _itemsList.Remove(item);
    }

    public override IEnumerator<T> GetEnumerator()
    {
        return _itemsList.GetEnumerator();
    }
}