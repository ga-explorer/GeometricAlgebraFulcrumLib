using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.Collections;

public sealed class EmptyReadOnlyList<T> : IReadOnlyList<T>
{
    public T DefaultValue { get; }

    public int Count { get; }

    public T this[int index]
        => DefaultValue;


    public EmptyReadOnlyList(int count)
    {
        Count = count;
        DefaultValue = default;
    }

    public EmptyReadOnlyList(int count, T defaultValue)
    {
        Count = count;
        DefaultValue = defaultValue;
    }


    public IEnumerator<T> GetEnumerator()
    {
        return Enumerable.Repeat(DefaultValue, Count).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Enumerable.Repeat(DefaultValue, Count).GetEnumerator();
    }
}