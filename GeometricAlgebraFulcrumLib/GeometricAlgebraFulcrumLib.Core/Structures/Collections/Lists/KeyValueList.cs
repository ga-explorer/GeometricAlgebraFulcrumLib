using System.Collections;

namespace GeometricAlgebraFulcrumLib.Core.Structures.Collections.Lists
{
    public class KeyValueList<TKey, TValue> : 
        IReadOnlyList<TValue> 
        where TKey : IEquatable<TKey>
    {
        private readonly List<TKey> _keyList;
        private readonly List<TValue> _valueList;
        private readonly Dictionary<TKey, int> _keyIndexDictionary;

        public int Count 
            => _valueList.Count;

        public IReadOnlyList<TKey> Keys 
            => _keyList;
        
        public IReadOnlyList<TValue> Values 
            => _valueList;

        public IEnumerable<KeyValuePair<TKey, TValue>> KeyValuePairs
        {
            get
            {
                for (var i = 0; i < Count; i++)
                    yield return new KeyValuePair<TKey, TValue>(
                        _keyList[i], 
                        _valueList[i]
                    );
            }
        }

        public TValue this[int index]
        {
            get => _valueList[index];
            set => _valueList[index] = value;
        }


        public KeyValueList()
        {
            _keyList = new List<TKey>();
            _valueList = new List<TValue>();
            _keyIndexDictionary = new Dictionary<TKey, int>();
        }

        public KeyValueList(int capacity)
        {
            _keyList = new List<TKey>(capacity);
            _valueList = new List<TValue>(capacity);
            _keyIndexDictionary = new Dictionary<TKey, int>();
        }
        
        public KeyValueList(IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            _keyList = new List<TKey>();
            _valueList = new List<TValue>();
            _keyIndexDictionary = new Dictionary<TKey, int>();

            var index = 0;

            foreach (var (key, value) in keyValuePairs)
            {
                _keyIndexDictionary.Add(key, index);
                _keyList.Add(key);
                _valueList.Add(value);
                
                index++;
            }
        }

        
        public KeyValueList<TKey, TValue> Clear()
        {
            _keyList.Clear();
            _valueList.Clear();
            _keyIndexDictionary.Clear();

            return this;
        }

        public bool RemoveByIndex(int index)
        {
            if (index < 0 || index > Count)
                return false;

            var key = _keyList[index];

            _keyList.RemoveAt(index);
            _valueList.RemoveAt(index);
            _keyIndexDictionary.Remove(key);

            return true;
        }
        
        public bool RemoveByKey(TKey key)
        {
            if (!_keyIndexDictionary.TryGetValue(key, out var index))
                return false;

            _keyList.RemoveAt(index);
            _valueList.RemoveAt(index);
            _keyIndexDictionary.Remove(key);

            return true;
        }


        public bool ContainsIndex(int index)
        {
            return index >= 0 && index < Count;
        }
        
        public bool ContainsKey(TKey key)
        {
            return _keyIndexDictionary.ContainsKey(key);
        }


        public TKey GetKeyByIndex(int index)
        {
            return _keyList[index];
        }

        public TValue GetValueByIndex(int index)
        {
            return _valueList[index];
        }
        
        public KeyValuePair<TKey, TValue> GetKeyValueByIndex(int index)
        {
            return new KeyValuePair<TKey, TValue>(
                _keyList[index],
                _valueList[index]
            );
        }
        

        public int GetIndexByKey(TKey key)
        {
            return _keyIndexDictionary[key];
        }
        
        public TValue GetValueByKey(TKey key)
        {
            return _valueList[_keyIndexDictionary[key]];
        }
        
        public KeyValuePair<int, TValue> GetIndexValueByKey(TKey key)
        {
            var index = _keyIndexDictionary[key];

            return new KeyValuePair<int, TValue>(
                index,
                _valueList[index]
            );
        }


        public bool TryGetIndex(TKey key, out int index)
        {
            return _keyIndexDictionary.TryGetValue(key, out index);
        }
        
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_keyIndexDictionary.TryGetValue(key, out var index))
            {
                value = _valueList[index];
                return true;
            }

            value = default;
            return false;
        }
        
        public bool TryGetIndexValue(TKey key, out int index, out TValue value)
        {
            if (_keyIndexDictionary.TryGetValue(key, out index))
            {
                value = _valueList[index];
                return true;
            }

            value = default;
            return false;
        }

        
        public KeyValueList<TKey, TValue> Append(TKey key, TValue value)
        {
            var index = Count;

            _keyIndexDictionary.Add(key, index);
            _keyList.Add(key);
            _valueList.Add(value);

            return this;
        }
        
        public KeyValueList<TKey, TValue> Append(IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            var index = Count;

            foreach (var (key, value) in keyValuePairs)
            {
                _keyIndexDictionary.Add(key, index);
                _keyList.Add(key);
                _valueList.Add(value);
                
                index++;
            }

            return this;
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
