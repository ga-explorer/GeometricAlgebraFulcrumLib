using System;
using System.Collections.Generic;

namespace DataStructuresLib.ODS
{
    public class KeyCollection<T> : ICollection<uint>
    {
        private readonly IDictionary<uint, T> _tree;

        internal KeyCollection(IDictionary<uint, T> dict)
        {
            _tree = dict;
        }

        public IEnumerator<uint> GetEnumerator()
        {
            var iter = _tree.GetEnumerator();
            while (iter.MoveNext())
                yield return iter.Current.Key;
        }

        public int Count => _tree.Count;

        public bool IsReadOnly => true;

        #region implicits

        void ICollection<uint>.Add(uint item)
        {
            throw new NotSupportedException();
        }

        void ICollection<uint>.Clear()
        {
            throw new NotSupportedException();
        }

        bool ICollection<uint>.Contains(uint item)
        {
            return _tree.ContainsKey(item);
        }

        void ICollection<uint>.CopyTo(uint[] array, int arrayIndex)
        {
            CollectionHelpers.ThrowIfInsufficientArray(this, array, arrayIndex);
            var iter = _tree.GetEnumerator();
            for (var i = 0; i < _tree.Count; i++)
            {
                iter.MoveNext();
                array[i] = iter.Current.Key;
            }
        }

        bool ICollection<uint>.Remove(uint item)
        {
            throw new NotSupportedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

    }
}
