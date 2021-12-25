using System.Collections.Generic;

namespace DataStructuresLib.ODS
{
    abstract public class SortedDictionaryBase<T> : ISortedDictionary<uint, T>
    {
        #region contract

        public abstract IEnumerator<KeyValuePair<uint, T>> GetEnumerator();
        public abstract KeyValuePair<uint, T>? First();
        public abstract KeyValuePair<uint, T>? Last();
        public abstract KeyValuePair<uint, T>? Lower(uint key);
        public abstract KeyValuePair<uint, T>? Higher(uint key);
        public abstract void Add(uint key, T value);
        public abstract bool ContainsKey(uint key);
        public abstract bool Remove(uint key);
        public abstract bool TryGetValue(uint key, out T value);
        public abstract T this[uint key] { get; set; }
        public abstract void Clear();
        public abstract int Count { get; }

        #endregion

        #region implicits

        void ICollection<KeyValuePair<uint, T>>.Add(KeyValuePair<uint, T> item)
        {
            Add(item.Key, item.Value);
        }

        bool ICollection<KeyValuePair<uint, T>>.Contains(KeyValuePair<uint, T> item)
        {
            T value;
            if (TryGetValue(item.Key, out value))
                return value.Equals(item.Value);
            return false;
        }

        void ICollection<KeyValuePair<uint, T>>.CopyTo(KeyValuePair<uint, T>[] array, int arrayIndex)
        {
            CollectionHelpers.ThrowIfInsufficientArray(this, array, arrayIndex);
            var iter = GetEnumerator();
            for (var i = 0; i < Count; i++)
            {
                iter.MoveNext();
                array[i] = iter.Current;
            }
        }

        bool ICollection<KeyValuePair<uint, T>>.Remove(KeyValuePair<uint, T> item)
        {
            T value;
            if (TryGetValue(item.Key, out value) && value.Equals(item.Value))
            {
                Remove(item.Key);
                return true;
            }
            return false;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ICollection<uint> Keys => new KeyCollection<T>(this);

        public ICollection<T> Values => new ValueCollection<T>(this);

        public bool IsReadOnly => false;

        #endregion
    }
}
