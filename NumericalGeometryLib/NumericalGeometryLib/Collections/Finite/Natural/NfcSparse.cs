using System;
using System.Collections.Generic;

namespace NumericalGeometryLib.Collections.Finite.Natural
{
    /// <summary>
    /// This class represents a sparse collection based on an underlying base collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NfcSparse<T> : NaturalFiniteCollection<T>, IDictionary<int, T>
    {
        public static NfcSparse<T> Create(FiniteCollection<T> baseCollection)
        {
            return new NfcSparse<T>(baseCollection);
        }

        public static NfcSparse<T> Create(int itemsCount, T defaultValue)
        {
            return new NfcSparse<T>(NfcConstant<T>.Create(itemsCount, defaultValue));
        }


        private readonly SortedDictionary<int, T> _itemsDictionary = 
            new SortedDictionary<int, T>();


        public bool IsReadOnly
        {
            get { return false; }
        }

        public override int Count
        {
            get { return BaseCollection.Count; }
        }

        public FiniteCollection<T> BaseCollection { get; private set; }

        public T this[int index]
        {
            get
            {
                T value;

                return
                    _itemsDictionary.TryGetValue(index, out value)
                    ? value : BaseCollection.GetItem(BaseCollection.MinIndex + index);
            }

            set
            {
                if (_itemsDictionary.ContainsKey(index))
                    _itemsDictionary[index] = value;
                else
                    _itemsDictionary.Add(index, value);
            }
        }

        public ICollection<int> Keys
        {
            get { return _itemsDictionary.Keys; }
        }

        public ICollection<T> Values
        {
            get { return _itemsDictionary.Values; }
        }


        private NfcSparse(FiniteCollection<T> baseCollection)
        {
            BaseCollection = baseCollection;
        }


        public NfcSparse<T> ResetItem(int index)
        {
            _itemsDictionary.Remove(index);

            return this;
        }

        public void Add(KeyValuePair<int, T> item)
        {
            _itemsDictionary.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _itemsDictionary.Clear();
        }

        public bool Contains(KeyValuePair<int, T> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<int, T>[] array, int arrayIndex)
        {
            _itemsDictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<int, T> item)
        {
            return _itemsDictionary.Remove(item.Key);
        }

        public bool ContainsKey(int key)
        {
            return _itemsDictionary.ContainsKey(key);
        }

        public void Add(int key, T value)
        {
            _itemsDictionary.Add(key, value);
        }

        public bool Remove(int key)
        {
            return _itemsDictionary.Remove(key);
        }

        public bool TryGetValue(int key, out T value)
        {
            return _itemsDictionary.TryGetValue(key, out value);
        }

        public NfcSparse<T> Reset(FiniteCollection<T> baseCollection, bool clearData)
        {
            BaseCollection = baseCollection;

            if (clearData)
                _itemsDictionary.Clear();

            return this;
        }

        public override T GetItem(int index)
        {
            T value;

            return
                _itemsDictionary.TryGetValue(index, out value)
                ? value : BaseCollection.GetItem(BaseCollection.MinIndex + index);
        }

        IEnumerator<KeyValuePair<int, T>> IEnumerable<KeyValuePair<int, T>>.GetEnumerator()
        {
            return _itemsDictionary.GetEnumerator();
        }
    }
}
