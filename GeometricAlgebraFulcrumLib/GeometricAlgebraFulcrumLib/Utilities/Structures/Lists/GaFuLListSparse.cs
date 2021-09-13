using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Lists
{
    public sealed class GaFuLListSparse<T> : 
        IList<T>, 
        IGaFuLReadOnlyList<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLListSparse<T> Create()
        {
            return new GaFuLListSparse<T>(default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLListSparse<T> Create(T defaultValue)
        {
            return new GaFuLListSparse<T>(defaultValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLListSparse<T> Create(T defaultValue, [NotNull] IEnumerable<KeyValuePair<ulong, T>> indexValuePairs)
        {
            return new GaFuLListSparse<T>(defaultValue, indexValuePairs);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLListSparse<T> Create(T defaultValue, [NotNull] SortedDictionary<ulong, T> itemsDictionary)
        {
            return new GaFuLListSparse<T>(defaultValue, itemsDictionary);
        }


        private SortedDictionary<ulong, T> _itemsDictionary
            = new SortedDictionary<ulong, T>();


        public int SparseCount 
            => _itemsDictionary.Count;

        public int Count { get; private set; }

        public bool IsReadOnly 
            => false;

        public T DefaultValue { get; }

        public T this[int index]
        {
            get => this[(ulong) index];
            set => this[(ulong) index] = value;
        }
        
        public T this[ulong index]
        {
            get
            {
                if (index >= (ulong) Count)
                    throw new IndexOutOfRangeException();

                return _itemsDictionary.TryGetValue(index, out var value)
                    ? value
                    : DefaultValue;
            }
            set
            {
                if (index >= (ulong) Count)
                {
                    Count = (int) (index + 1);
                    _itemsDictionary.Add(index, value);
                    return;
                }

                if (_itemsDictionary.ContainsKey(index))
                    _itemsDictionary[index] = value;
                else
                    _itemsDictionary.Add(index, value);
            }
        }


        private GaFuLListSparse(T defaultValue)
        {
            DefaultValue = defaultValue;
        }
        
        private GaFuLListSparse(T defaultValue, [NotNull] SortedDictionary<ulong, T> itemsDictionary)
        {
            DefaultValue = defaultValue;
            _itemsDictionary = itemsDictionary;
        }

        private GaFuLListSparse(T defaultValue, [NotNull] IEnumerable<KeyValuePair<ulong, T>> indexValuePairs)
        {
            DefaultValue = defaultValue;
            _itemsDictionary = new SortedDictionary<ulong, T>();

            foreach (var (index, value) in indexValuePairs)
                _itemsDictionary.Add(index, value);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T item)
        {
            this[Count] = item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            Count = 0;
            _itemsDictionary.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(T item)
        {
            return _itemsDictionary.ContainsValue(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(T[] array, int arrayIndex)
        {
            for (var index = 0; index < Count; index++)
                array[arrayIndex + index] = DefaultValue;

            foreach (var (index, item) in _itemsDictionary)
                array[arrayIndex + (int) index] = item;
        }

        public bool Remove(T item)
        {
            var removedIndexFound = false;
            var removedIndex = 0UL;
            foreach (var (index, value) in _itemsDictionary)
            {
                if (!value.Equals(item)) continue;

                removedIndexFound = true;
                removedIndex = index;
                break;
            }

            if (!removedIndexFound) 
                return false;

            var itemsDictionary = new SortedDictionary<ulong, T>();

            foreach (var (index, value) in _itemsDictionary)
            {
                if (index < removedIndex)
                {
                    itemsDictionary.Add(index, value);
                    Count = (int) (index + 1);
                }
                else if (index > removedIndex)
                {
                    itemsDictionary.Add(index - 1, value);
                    Count = (int) index;
                }
            }

            _itemsDictionary = itemsDictionary;

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IndexOf(T item)
        {
            foreach (var (index, value) in _itemsDictionary)
                if (value.Equals(item))
                    return (int) index;

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Insert(int index, T item)
        {
            if (index < 0)
                throw new IndexOutOfRangeException();

            if (index >= Count)
            {
                Count = index + 1;
                _itemsDictionary.Add((ulong) index, item);
                return;
            }

            if (!_itemsDictionary.ContainsKey((ulong) index))
            {
                _itemsDictionary.Add((ulong) index, item);
                return;
            }

            var itemsDictionary = new SortedDictionary<ulong, T>();

            foreach (var (i, value) in _itemsDictionary)
            {
                if (i < (ulong) index)
                {
                    itemsDictionary.Add((ulong) index, value);
                    Count = index + 1;
                }
                else if (i >= (ulong) index)
                {
                    itemsDictionary.Add((ulong) index + 1, value);
                    Count = index + 2;
                }
                else
                {
                    itemsDictionary.Add((ulong) index, item);
                    Count = index + 1;
                }
            }

            _itemsDictionary = itemsDictionary;
        }

        public void RemoveAt(int index)
        {
            if (!_itemsDictionary.ContainsKey((ulong) index))
                return;

            var itemsDictionary = new SortedDictionary<ulong, T>();

            foreach (var (i, v) in _itemsDictionary)
            {
                if (i < (ulong) index)
                {
                    itemsDictionary.Add(i, v);
                    Count = (int) i + 1;
                }
                else if (i > (ulong) index)
                {
                    itemsDictionary.Add(i - 1, v);
                    Count = (int) i;
                }
            }

            _itemsDictionary = itemsDictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return _itemsDictionary.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices()
        {
            return _itemsDictionary.Keys;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            return _itemsDictionary.Values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index)
        {
            return _itemsDictionary.ContainsKey(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong index, out T value)
        {
            return _itemsDictionary.TryGetValue(index, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<KeyValuePair<ulong, T>> GetIndexValuePairs()
        {
            return _itemsDictionary.Select(pair => 
                new KeyValuePair<ulong, T>(pair.Key, pair.Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<T> GetEnumerator()
        {
            for (var index = 0; index < Count; index++)
                yield return _itemsDictionary.TryGetValue((ulong) index, out var value)
                    ? value
                    : DefaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}