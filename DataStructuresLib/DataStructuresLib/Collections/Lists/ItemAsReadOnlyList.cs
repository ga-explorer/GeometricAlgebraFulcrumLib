using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructuresLib.Collections.Lists;

public readonly struct ItemAsReadOnlyList<T> : 
    IReadOnlyList<T>
{
    public T Value { get; }

    public int Count => 1;

    public T this[int index] 
        => index == 0 
            ? Value 
            : throw new IndexOutOfRangeException();


    public ItemAsReadOnlyList(T value)
    {
        Value = value;
    }
        

    public IEnumerator<T> GetEnumerator()
    {
        yield return Value;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return Value;
    }
}