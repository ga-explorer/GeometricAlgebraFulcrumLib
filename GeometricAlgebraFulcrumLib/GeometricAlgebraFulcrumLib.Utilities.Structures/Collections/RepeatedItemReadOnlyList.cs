using System.Collections;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;

public sealed class RepeatedItemReadOnlyList<T> :
    IReadOnlyList<T>
{
    public T ItemValue { get; }

    public int Count { get; }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            return ItemValue;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RepeatedItemReadOnlyList(T itemValue, int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        ItemValue = itemValue;
        Count = count;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<T> GetEnumerator()
    {
        return Enumerable.Repeat(ItemValue, Count).GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}