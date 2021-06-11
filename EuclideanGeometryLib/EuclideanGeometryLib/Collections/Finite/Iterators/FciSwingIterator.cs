using System.Collections;
using System.Collections.Generic;

namespace EuclideanGeometryLib.Collections.Finite.Iterators
{
    public sealed class FciSwingIterator<T> : IEnumerator<T>
    {
        private readonly int _firstIndex;

        private readonly bool _reverseDirection;

        private readonly int _count;

        private readonly GenerativeCollection<T> _collection;

        private int _currentStep;

        private int _currentOffset;


        internal FciSwingIterator(GenerativeCollection<T> collection, int firstIndex, int lastIndex)
        {
            _firstIndex = firstIndex;
            _reverseDirection = lastIndex < firstIndex;
            _count =
                _reverseDirection
                ? (firstIndex - lastIndex + 1)
                : (lastIndex - firstIndex + 1);
            _collection = collection;
        }

        internal FciSwingIterator(FiniteCollection<T> collection)
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
            if (_count == 1)
            {
                _currentOffset = 0;
                Current = _collection.GetItem(_firstIndex);
                return true;
            }

            if (_currentStep == 1)
            {
                if (_currentOffset >= _count - 1)
                {
                    _currentStep = -1;
                    _currentOffset = _count - 2;
                }
                else
                    _currentOffset = _currentOffset + 1;
            }
            else
            {
                if (_currentOffset <= 0)
                {
                    _currentStep = 1;
                    _currentOffset = 1;
                }
                else
                    _currentOffset = _currentOffset - 1;
            }

            Current = _collection.GetItem(
                _reverseDirection
                ? (_firstIndex - _currentOffset)
                : (_firstIndex + _currentOffset)
            );

            return true;
        }

        public void Reset()
        {
            _currentOffset = -1;
            _currentStep = 1;
            Current = default(T);
        }

        public T Current { get; private set; }

        object IEnumerator.Current => Current;
    }
}
