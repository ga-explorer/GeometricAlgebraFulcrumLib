using System.Collections;
using System.Collections.Generic;

namespace DataStructuresLib.Collections.Generative.Finite.Iterators
{
    public sealed class FciMappedOffsetIterator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<int> _offsetSequence;

        private readonly GenerativeCollection<T> _collection;

        private readonly int _firstIndex;

        private readonly bool _reverseDirection;

        private int _currentOffset;


        internal FciMappedOffsetIterator(GenerativeCollection<T> collection, IEnumerator<int> offsetSequence, int firstIndex, bool reverseDirection)
        {
            _offsetSequence = offsetSequence;
            _collection = collection;
            _firstIndex = firstIndex;
            _reverseDirection = reverseDirection;
        }

        internal FciMappedOffsetIterator(GenerativeCollection<T> collection, IEnumerable<int> offsetSequence, int firstIndex, bool reverseDirection)
        {
            _offsetSequence = offsetSequence.GetEnumerator();
            _collection = collection;
            _firstIndex = firstIndex;
            _reverseDirection = reverseDirection;
        }


        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_offsetSequence.MoveNext() == false) return false;

            _currentOffset = _offsetSequence.Current;

            Current = _collection.GetItem(
                _reverseDirection
                ? _firstIndex - _currentOffset
                : _firstIndex + _currentOffset
                );

            return true;
        }

        public void Reset()
        {
            _offsetSequence.Reset();
            _currentOffset = _offsetSequence.Current;
            Current = default(T);
        }

        public T Current { get; private set; }

        object IEnumerator.Current => Current;
    }
}
