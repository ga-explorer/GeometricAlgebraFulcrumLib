using System.Collections;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Collections.Lists
{
    public sealed class SparseList<T> : 
        IReadOnlyList<T> where T : IEquatable<T>
    {
        private readonly SortedDictionary<int, T> _indexValueDictionary
            = new SortedDictionary<int, T>();


        public T DefaultValue { get; }

        public int SparseCount 
            => _indexValueDictionary.Count;

        public SortedDictionary<int, T>.KeyCollection Indices 
            => _indexValueDictionary.Keys;

        public SortedDictionary<int, T>.ValueCollection Items 
            => _indexValueDictionary.Values;

        public IEnumerable<KeyValuePair<int, T>> IndexItemPairs 
            => _indexValueDictionary;

        public int FirstIndex 
            => _indexValueDictionary.Count > 0 ?_indexValueDictionary.Keys.First() : -1;

        public int LastIndex 
            => _indexValueDictionary.Count > 0 ?_indexValueDictionary.Keys.Last() : -1;

        public int Count { get; private set; } 

        public T this[int index]
        {
            get
            {
                if (index < 0)
                    throw new IndexOutOfRangeException();

                return _indexValueDictionary.GetValueOrDefault(index, DefaultValue);
            }
            set
            {
                if (index < 0)
                    throw new IndexOutOfRangeException();

                if (value is null) 
                    throw new ArgumentNullException(nameof(value));

                if (DefaultValue.Equals(value))
                {
                    _indexValueDictionary.Remove(index);
                }
                else
                {
                    if (_indexValueDictionary.ContainsKey(index))
                        _indexValueDictionary[index] = value;
                    else
                        _indexValueDictionary.Add(index, value);

                    if (index + 1 > Count)
                        Count = index + 1;
                }
            }
        }


        public SparseList(T defaultValue)
        {
            if (defaultValue is null)
                throw new ArgumentNullException(nameof(defaultValue));

            DefaultValue = defaultValue;
        }


        public void Clear()
        {
            _indexValueDictionary.Clear();
            Count = 0;
        }

        public int ResetCount()
        {
            Count = _indexValueDictionary.Count == 0
                ? 0 
                : _indexValueDictionary.Keys.Last() + 1;

            return Count;
        }

        public void Add(T value)
        {
            if (value is null) 
                throw new ArgumentNullException(nameof(value));

            if (!DefaultValue.Equals(value))
                _indexValueDictionary.Add(Count, value);

            Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var index1 = -1;
            foreach (var (index2, value2) in _indexValueDictionary)
            {
                var n = index2 - index1 - 1;

                for (var i = 0; i < n; i++)
                    yield return DefaultValue;

                yield return value2;
                index1 = index2;
            }

            {
                var n = Count - index1 - 1;

                for (var i = 0; i < n; i++)
                    yield return DefaultValue;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
