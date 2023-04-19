using System.Collections;
using System.Collections.Generic;
using DataStructuresLib.Basic;

namespace NumericalGeometryLib.Collections.Finite.Iterators
{
    public sealed class FciPeriodicIterator<T> : IEnumerator<T>
    {
        private readonly int _firstIndex;

        private readonly bool _reverseDirection;

        private readonly int _count;

        private readonly GenerativeCollection<T> _collection;

        private int _currentOffset;


        internal FciPeriodicIterator(GenerativeCollection<T> collection, int firstIndex, int lastIndex)
        {
            _firstIndex = firstIndex;
            _reverseDirection = lastIndex < firstIndex;
            _count =
                _reverseDirection
                ? firstIndex - lastIndex + 1
                : lastIndex - firstIndex + 1;
            _collection = collection;
        }

        internal FciPeriodicIterator(FiniteCollection<T> collection)
        {
            _firstIndex = collection.MinIndex;
            _reverseDirection = false;
            _count = collection.Count;
            _collection = collection;
        }


        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            _currentOffset = (_currentOffset + 1).Mod(_count);

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

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}
