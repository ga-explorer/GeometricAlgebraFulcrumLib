using System.Collections;
using System.Collections.Generic;

namespace NumericalGeometryLib.Collections.Finite.Iterators
{
    public sealed class FciMappedIndexIterator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<int> _indexSequence;

        private readonly GenerativeCollection<T> _collection;

        private int _currentIndex;


        internal FciMappedIndexIterator(GenerativeCollection<T> collection, IEnumerator<int> indexSequence)
        {
            _indexSequence = indexSequence;
            _collection = collection;
        }

        internal FciMappedIndexIterator(GenerativeCollection<T> collection, IEnumerable<int> indexSequence)
        {
            _indexSequence = indexSequence.GetEnumerator();
            _collection = collection;
        }


        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_indexSequence.MoveNext() == false) return false;

            _currentIndex = _indexSequence.Current;

            Current = _collection.GetItem(_currentIndex);

            return true;
        }

        public void Reset()
        {
            _indexSequence.Reset();
            _currentIndex = _indexSequence.Current;
            Current = default(T);
        }

        public T Current { get; private set; }

        object IEnumerator.Current => Current;
    }
}
