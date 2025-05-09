using System.Collections;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections;

public class MappedReadOnlyList<T> :
    IReadOnlyList<T>
{
    public IReadOnlyList<T> BaseList { get; }

    public Func<T, T> ItemMapping { get; }

    public int Count
        => BaseList.Count;

    public T this[int index]
        => ItemMapping(BaseList[index]);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MappedReadOnlyList(IReadOnlyList<T> baseList, Func<T, T> itemMapping)
    {
        BaseList = baseList;
        ItemMapping = itemMapping;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<T> GetEnumerator()
    {
        return BaseList.Select(ItemMapping).GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class MappedReadOnlyList<T1, T2> :
    IReadOnlyList<T2>
{
    public IReadOnlyList<T1> BaseList { get; }

    public Func<T1, T2> ItemMapping { get; }

    public int Count 
        => BaseList.Count;

    public T2 this[int index] 
        => ItemMapping(BaseList[index]);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MappedReadOnlyList(IReadOnlyList<T1> baseList, Func<T1, T2> itemMapping)
    {
        BaseList = baseList;
        ItemMapping = itemMapping;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<T2> GetEnumerator()
    {
        return BaseList.Select(ItemMapping).GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}