using System.Collections;
using System.Collections.Generic;

namespace DataStructuresLib.Collections.Queues;

public sealed class SetQueue<T> : 
    IReadOnlyCollection<T>
{
    private readonly HashSet<T> _set = new HashSet<T>();
    private readonly Queue<T> _queue = new Queue<T>();


    public bool IsEmpty 
        => _queue.Count == 0;

    public int Count 
        => _queue.Count;


    public SetQueue<T> Clear()
    {
        _set.Clear();
        _queue.Clear();

        return this;
    }
    
    public SetQueue<T> Enqueue(T value)
    {
        if (!_set.Add(value)) return this;

        _queue.Enqueue(value);

        return this;
    }

    public T Dequeue()
    {
        var item = _queue.Dequeue();
        _set.Remove(item);

        return item;
    }

    public T Peek()
    {
        return _queue.Peek();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _queue.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}