using System.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures;

/// <summary>
/// This structure can act as either a stack or queue depending on how it's
/// initialized
/// </summary>
/// <typeparam name="T"></typeparam>
public class QueueStack<T> : IEnumerable<T>
{
    private readonly Stack<T> _stack;
    private readonly Queue<T> _queue;


    public bool IsStack { get; }

    public bool IsQueue => !IsStack;

    public int Count 
        => IsStack ? _stack.Count : _queue.Count;


    public QueueStack(bool isStack)
    {
        IsStack = isStack;

        _stack = isStack ? new Stack<T>() : null;
        _queue = isStack ? null : new Queue<T>();
    }


    public void Clear()
    {
        if (IsStack)
            _stack.Clear();
        else
            _queue.Clear();
    }

    public bool Contains(T item)
    {
        return IsStack
            ? _stack.Contains(item)
            : _queue.Contains(item);
    }

    public T Peek()
    {
        return IsStack
            ? _stack.Peek()
            : _queue.Peek();
    }

    public void Add(T item)
    {
        if (IsStack)
            _stack.Push(item);

        else
            _queue.Enqueue(item);
    }

    public void AddItems(params T[] items)
    {
        if (IsStack)
            foreach (var item in items)
                _stack.Push(item);

        else
            foreach (var item in items)
                _queue.Enqueue(item);
    }

    public void AddItems(IEnumerable<T> items)
    {
        if (IsStack)
            foreach (var item in items)
                _stack.Push(item);

        else
            foreach (var item in items)
                _queue.Enqueue(item);
    }

    public T Remove()
    {
        return IsStack 
            ? _stack.Pop() 
            : _queue.Dequeue();
    }

    public IEnumerator<T> GetEnumerator()
    {
        if (IsStack)
            return _stack.GetEnumerator();

        return _queue.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        if (IsStack)
            return _stack.GetEnumerator();

        return _queue.GetEnumerator();
    }
}