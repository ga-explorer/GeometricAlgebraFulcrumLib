using System.Collections;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections;

public sealed class SingleItemReadOnlyList<T> : 
    IReadOnlyList<T>
{
    public T ItemValue { get; }

    public int ItemIndex { get; }

    public int Count { get; }

    public T this[int index] 
        => index == ItemIndex 
            ? ItemValue 
            : default;


    public SingleItemReadOnlyList(int count, int itemIndex, T itemValue)
    {
        if (itemIndex < 0 || itemIndex >= Count)
            throw new IndexOutOfRangeException();

        Count = count;
        ItemIndex = itemIndex;
        ItemValue = itemValue;
    }


    public IEnumerator<T> GetEnumerator()
    {
        for (var i = 0; i < ItemIndex; i++)
            yield return default;

        yield return ItemValue;

        for (var i = ItemIndex + 1; i < Count; i++)
            yield return default;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        for (var i = 0; i < ItemIndex; i++)
            yield return default;

        yield return ItemValue;

        for (var i = ItemIndex + 1; i < Count; i++)
            yield return default;
    }
}