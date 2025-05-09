using System.Collections;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections.Lists
{
    public sealed class BijectiveList<TKey, TValue> :
        IReadOnlyList<TValue> where TKey : IEquatable<TKey>
    {
        private readonly IReadOnlyList<TValue> _valueList;
        private readonly Dictionary<TKey, int> _keyIndexDictionary 
            = new Dictionary<TKey, int>();


        public int Count 
            => _valueList.Count;

        public Func<TValue, TKey> ValueToKeyMap { get; }

        public IEnumerable<TKey> Keys 
            => _keyIndexDictionary.Keys;

        public IEnumerable<TValue> Values 
            => _valueList;

        public TValue this[int index] 
            => _valueList[index];


        public BijectiveList(IReadOnlyList<TValue> valueList, Func<TValue, TKey> valueToKeyMap)
        {
            _valueList = valueList;
            ValueToKeyMap = valueToKeyMap;

            var index = 0;
            foreach (var value in _valueList)
                _keyIndexDictionary.Add(ValueToKeyMap(value), index++);
        }
        
        
        public bool ContainsIndex(int index)
        {
            return index >= 0 && index < _valueList.Count;
        }
        
        public bool ContainsKey(TKey key)
        {
            return _keyIndexDictionary.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            var key = ValueToKeyMap(value);

            return _keyIndexDictionary.ContainsKey(key);
        }


        public TKey GetKeyByIndex(int index)
        {
            var value = _valueList[index];

            return ValueToKeyMap(value);
        }
        
        public TValue GetValueByIndex(int index)
        {
            return _valueList[index];
        }

        public KeyValuePair<TKey, TValue> GetKeyValueByIndex(int index)
        {
            var value = _valueList[index];

            return new KeyValuePair<TKey, TValue>(ValueToKeyMap(value), value);
        }


        public int GetIndexByKey(TKey key)
        {
            return _keyIndexDictionary[key];
        }
        
        public TValue GetValueByKey(TKey key)
        {
            var index = _keyIndexDictionary[key];

            return _valueList[index];
        }
        
        public KeyValuePair<int, TValue> GetIndexValueByKey(TKey key)
        {
            var index = _keyIndexDictionary[key];

            return new KeyValuePair<int, TValue>(index, _valueList[index]);
        }


        public TKey GetKeyByValue(TValue value)
        {
            return ValueToKeyMap(value);
        }
        
        public int GetIndexByValue(TValue value)
        {
            var key = ValueToKeyMap(value);

            return _keyIndexDictionary[key];
        }
        
        public KeyValuePair<int, TKey> GetIndexKeyByValue(TValue value)
        {
            var key = ValueToKeyMap(value);

            return new KeyValuePair<int, TKey>(_keyIndexDictionary[key], key);
        }
        

        public Tuple<bool, TKey?> TryGetKeyByIndex(int index)
        {
            if (index >= 0 && index < _valueList.Count)
            {
                var value = _valueList[index];

                var key = ValueToKeyMap(value);

                return new Tuple<bool, TKey?>(true, key);
            }

            return new Tuple<bool, TKey?>(false, default);
        }
        
        public Tuple<bool, TValue?> TryGetValueByIndex(int index)
        {
            if (index >= 0 && index < _valueList.Count)
            {
                var value = _valueList[index];

                return new Tuple<bool, TValue?>(true, value);
            }

            return new Tuple<bool, TValue?>(false, default);
        }
        
        public Tuple<bool, TKey?, TValue?> TryGetKeyValueByIndex(int index)
        {
            if (index >= 0 && index < _valueList.Count)
            {
                var value = _valueList[index];

                var key = ValueToKeyMap(value);

                return new Tuple<bool, TKey?, TValue?>(true, key, value);
            }

            return new Tuple<bool, TKey?, TValue?>(false, default, default);
        }

        
        public Tuple<bool, int> TryGetIndexByKey(TKey key)
        {
            return _keyIndexDictionary.TryGetValue(key, out var index) 
                ? new Tuple<bool, int>(true, index) 
                : new Tuple<bool, int>(false, -1);
        }

        public Tuple<bool, TValue?> TryGetValueByKey(TKey key)
        {
            return _keyIndexDictionary.TryGetValue(key, out var index) 
                ? new Tuple<bool, TValue?>(true, _valueList[index]) 
                : new Tuple<bool, TValue?>(false, default);
        }

        public Tuple<bool, int, TValue?> TryGetIndexValueByKey(TKey key)
        {
            return _keyIndexDictionary.TryGetValue(key, out var index) 
                ? new Tuple<bool, int, TValue?>(true, index, _valueList[index]) 
                : new Tuple<bool, int, TValue?>(false, -1, default);
        }

        
        public Tuple<bool, int> TryGetIndexByValue(TValue value)
        {
            var key = ValueToKeyMap(value);

            return _keyIndexDictionary.TryGetValue(key, out var index) 
                ? new Tuple<bool, int>(true, index) 
                : new Tuple<bool, int>(false, -1);
        }

        public Tuple<bool, TKey?> TryGetKeyByValue(TValue value)
        {
            var key = ValueToKeyMap(value);

            return _keyIndexDictionary.ContainsKey(key) 
                ? new Tuple<bool, TKey?>(true, key) 
                : new Tuple<bool, TKey?>(false, default);
        }

        public Tuple<bool, int, TKey?> TryGetIndexKeyByValue(TValue value)
        {
            var key = ValueToKeyMap(value);

            return _keyIndexDictionary.TryGetValue(key, out var index) 
                ? new Tuple<bool, int, TKey?>(true, index, key) 
                : new Tuple<bool, int, TKey?>(false, -1, default);
        }


        public IEnumerator<TValue> GetEnumerator()
        {
            return _valueList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
