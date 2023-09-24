using System.Collections;
using System.Collections.Generic;

namespace DataStructuresLib.Collections.Generative.Finite.Natural
{
    public sealed class NfcChainedEnumerator<T> : IEnumerator<T>
    {
        private readonly NfcChained<T> _chain;

        private int _currentCollectionIndex;

        private FiniteCollection<T> _currentCollection;

        private int _currentItemIndex;


        internal NfcChainedEnumerator(NfcChained<T> chain)
        {
            _chain = chain;
            _currentCollectionIndex = -1;
            _currentCollection = null;
            _currentItemIndex = -1;
            Current = default(T);
        }


        public void Dispose()
        {
            
        }

        private bool MoveToNextCollection()
        {
            _currentCollectionIndex++;
            _currentItemIndex = -1;

            while (_currentCollectionIndex < _chain.BaseCollections.Count)
            {
                _currentCollection = _chain.BaseCollections[_currentCollectionIndex];

                if (_currentCollection != null)
                    return true;
            }

            return false;
        }

        public bool MoveNext()
        {
            if (_currentCollectionIndex == -1 && MoveToNextCollection() == false) 
                return false;

            _currentItemIndex++;

            if (_currentItemIndex >= _currentCollection.Count && MoveToNextCollection() == false)
                return false;

            Current = 
                _currentCollection.GetItem(
                    _currentCollection.MinIndex + _currentItemIndex
                    );

            return true;
        }

        public void Reset()
        {
            _currentCollectionIndex = -1;
            _currentCollection = null;
            _currentItemIndex = -1;
            Current = default(T);
        }

        public T Current { get; private set; }

        object IEnumerator.Current => Current;
    }
}