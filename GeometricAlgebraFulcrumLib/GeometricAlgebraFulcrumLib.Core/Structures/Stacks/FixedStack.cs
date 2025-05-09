using System.Collections;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Stacks;

public sealed class FixedStack<T> : IReadOnlyList<T>
{
    private readonly T[] _itemsArray;
    private int _topOfStackIndex = -1;

    public int Count 
        => _topOfStackIndex + 1;

    public T this[int index]
    {
        get
        {
            if (index < 0 || index > _topOfStackIndex)
                throw new IndexOutOfRangeException();

            return _itemsArray[index];
        }
    }


    public FixedStack(int capacity)
    {
        _itemsArray = new T[capacity];
    }

    public FixedStack(int capacity, T firstItem)
    {
        _itemsArray = new T[capacity];

        Push(firstItem);
    }

    public FixedStack(int capacity, params T[] firstItems)
    {
        _itemsArray = new T[capacity];

        Push(firstItems);
    }

    public FixedStack(int capacity, IEnumerable<T> firstItems)
    {
        _itemsArray = new T[capacity];

        Push(firstItems);
    }


    public void Clear()
    {
        _topOfStackIndex = -1;
    }

    public void Push(T item)
    {
        if (Count > _itemsArray.Length)
            throw new InvalidOperationException();

        _topOfStackIndex++;

        _itemsArray[_topOfStackIndex] = item;
    }

    public void Push(params T[] items)
    {
        if (Count > _itemsArray.Length - items.Length)
            throw new InvalidOperationException();

        foreach (var item in items)
        {
            _topOfStackIndex++;

            _itemsArray[_topOfStackIndex] = item;
        }
    }

    public void Push(IEnumerable<T> items)
    {
        foreach (var item in items)
            Push(item);
    }

    public T Pop()
    {
        if (Count <= 0)
            throw new InvalidOperationException();

        var item = _itemsArray[_topOfStackIndex];

        _topOfStackIndex--;

        return item;
    }
        
    public IEnumerator<T> GetEnumerator()
    {
        return Count > 0
            ? _itemsArray.Take(Count).Reverse().GetEnumerator()
            : Enumerable.Empty<T>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Count > 0
            ? _itemsArray.Take(Count).Reverse().GetEnumerator()
            : Enumerable.Empty<T>().GetEnumerator();
    }
}