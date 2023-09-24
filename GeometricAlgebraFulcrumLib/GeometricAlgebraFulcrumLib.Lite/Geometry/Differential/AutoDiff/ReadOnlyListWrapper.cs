using System.Collections;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.AutoDiff
{
#if (!NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6)
    [Serializable]
#endif
    internal class ReadOnlyListWrapper<T> : IReadOnlyList<T>
    {
        private readonly IReadOnlyList<T> _list;

        public ReadOnlyListWrapper(IReadOnlyList<T> list) { _list = list; }
        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_list).GetEnumerator();
        public int Count => _list.Count;
        public T this[int index] => _list[index];
    }

    internal static class ReadOnlyListWrapper
    {
        public static ReadOnlyListWrapper<T> AsReadOnly<T>(this IReadOnlyList<T> list)
        {
            return new ReadOnlyListWrapper<T>(list);
        }
    }
}