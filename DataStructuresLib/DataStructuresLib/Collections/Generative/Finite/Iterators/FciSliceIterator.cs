using System.Collections;
using System.Collections.Generic;

namespace DataStructuresLib.Collections.Generative.Finite.Iterators;

public sealed class FciSliceIterator<T> : IEnumerator<T>
{
    private readonly int _firstIndex;

    private readonly bool _reverseDirection;

    private readonly int _count;

    private readonly GenerativeCollection<T> _collection;

    private int _currentOffset;


    internal FciSliceIterator(GenerativeCollection<T> collection, int firstIndex, int lastIndex)
    {
        _firstIndex = firstIndex;
        _reverseDirection = lastIndex < firstIndex;
        _count = 
            _reverseDirection 
                ? firstIndex - lastIndex + 1 
                : lastIndex - firstIndex + 1;
        _collection = collection;
    }


    public void Dispose()
    {
    }

    public bool MoveNext()
    {
        if (++_currentOffset >= _count) return false;

        Current = _collection.GetItem(
            _reverseDirection
                ? _firstIndex - _currentOffset 
                : _firstIndex + _currentOffset
        );

        return true;
    }

    public void Reset()
    {
        _currentOffset = -1;
        Current = default(T);
    }

    public T Current { get; private set; }

    object IEnumerator.Current => Current;
}