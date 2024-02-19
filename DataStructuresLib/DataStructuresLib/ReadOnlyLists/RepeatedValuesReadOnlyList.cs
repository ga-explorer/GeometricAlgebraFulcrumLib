using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.ReadOnlyLists;

public sealed class RepeatedValuesReadOnlyList<T>
    : IReadOnlyList<T> where T : struct
{
    public T DefaultValue { get; }

    public int Count { get; }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            return DefaultValue;
        }
    }


    public RepeatedValuesReadOnlyList(int count, T defaultValue)
    {
        if (count < 0) 
            throw new InvalidOperationException();

        Count = count;
        DefaultValue = defaultValue;
    }
        

    public IEnumerator<T> GetEnumerator()
    {
        return Enumerable.Repeat(DefaultValue, Count).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}