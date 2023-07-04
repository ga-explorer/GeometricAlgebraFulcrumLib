using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructuresLib.AttributeSet
{
    public abstract class SparseCodeAttributeSet :
        ISparseCodeAttributeSet
    {
        private readonly Dictionary<string, ISparseCodeAttributeValue> _attributeDictionary
            = new Dictionary<string, ISparseCodeAttributeValue>();


        public int Count 
            => _attributeDictionary.Count;
        
        public ISparseCodeAttributeValue this[string key] 
            => _attributeDictionary[key];

        public IEnumerable<string> Keys 
            => _attributeDictionary.Keys;

        public IEnumerable<ISparseCodeAttributeValue> Values 
            => _attributeDictionary.Values;


        public ISparseCodeAttributeSet Clear()
        {
            _attributeDictionary.Clear();

            return this;
        }
        
        public bool RemoveAttribute(string key)
        {
            return _attributeDictionary.Remove(key);
        }

        public bool ContainsKey(string key)
        {
            return _attributeDictionary.ContainsKey(key);
        }

        public bool TryGetValue(string key, out ISparseCodeAttributeValue value)
        {
            return _attributeDictionary.TryGetValue(key, out value);
        }

        public string GetAttributeValueText(string key)
        {
            return _attributeDictionary.TryGetValue(key, out var attributeValue) 
                ? attributeValue.GetCode() 
                : string.Empty;
        }

        
        public T GetAttributeValueOrNull<T>(string key) 
            where T : ISparseCodeAttributeValue
        {
            if (!TryGetAttributeValue(key, out var attributeValue))
                return default;

            if (attributeValue is not T av)
                throw new InvalidCastException();

            return av;
        }

        public ISparseCodeAttributeValue GetAttributeValue(string key)
        {
            // Attribute not found
            if (!_attributeDictionary.TryGetValue(key, out var attributeValue))
                throw new KeyNotFoundException();

            // Attribute found but empty
            if (attributeValue.IsEmpty)
                throw new KeyNotFoundException();
            
            // Proper value found
            return attributeValue;
        }

        public ISparseCodeAttributeValue<T> GetAttributeValue<T>(string key)
        {
            // Attribute not found
            if (!_attributeDictionary.TryGetValue(key, out var attributeValue))
                throw new KeyNotFoundException();

            // Attribute found but empty
            if (attributeValue.IsEmpty)
                throw new KeyNotFoundException();

            // Attribute is not of required type
            if (attributeValue is not ISparseCodeAttributeValue<T> av) 
                throw new KeyNotFoundException();
            
            // Proper value found
            return av;
        }

        public Tuple<bool, ISparseCodeAttributeValue<T>> TryGetAttributeValue<T>(string key)
        {
            var falseResult = 
                new Tuple<bool, ISparseCodeAttributeValue<T>>(
                    false, 
                    default
                );

            // Attribute not found
            if (!_attributeDictionary.TryGetValue(key, out var attributeValue))
                return falseResult;

            // Attribute found but empty
            if (attributeValue.IsEmpty)
                return falseResult;

            // Attribute is not of required type
            if (attributeValue is not ISparseCodeAttributeValue<T> av) 
                return falseResult;

            // Proper value found
            return new Tuple<bool, ISparseCodeAttributeValue<T>>(true, av);
        }
        
        public bool TryGetAttributeValue(string key, out ISparseCodeAttributeValue value)
        {
            value = default;

            // Attribute not found
            if (!_attributeDictionary.TryGetValue(key, out var attributeValue))
                return false;

            // Attribute found but empty
            if (attributeValue.IsEmpty)
                return false;
            
            // Proper value found
            value = attributeValue;
            return true;
        }

        public bool TryGetAttributeValue<T>(string key, out ISparseCodeAttributeValue<T> value)
        {
            value = default;

            // Attribute not found
            if (!_attributeDictionary.TryGetValue(key, out var attributeValue))
                return false;

            // Attribute found but empty
            if (attributeValue.IsEmpty)
                return false;

            // Attribute is not of required type
            if (attributeValue is not ISparseCodeAttributeValue<T> av) 
                return false;
            
            // Proper value found
            value = av;
            return true;
        }

        public SparseCodeAttributeSet SetAttributeValue(string key, ISparseCodeAttributeValue value)
        {
            if (value is null || value.IsEmpty)
            {
                if (_attributeDictionary.ContainsKey(key))
                    _attributeDictionary.Remove(key);

                return this;
            }

            if (_attributeDictionary.ContainsKey(key))
                _attributeDictionary[key] = value;
            else
                _attributeDictionary.Add(key, value);

            return this;
        }

        public SparseCodeAttributeSet SetAttributeValues(IEnumerable<KeyValuePair<string, ISparseCodeAttributeValue>> keyValuePairs)
        {
            foreach (var (key, value) in keyValuePairs)
                SetAttributeValue(key, value);

            return this;
        }

        public abstract string GetCode();

        public virtual IEnumerable<KeyValuePair<string, string>> GetKeyValueCodePairs()
        {
            foreach (var (key, attributeValue) in _attributeDictionary)
            {
                yield return new KeyValuePair<string, string>(
                    key,
                    attributeValue.GetCode()
                );
            }
        }

        public IEnumerator<KeyValuePair<string, ISparseCodeAttributeValue>> GetEnumerator()
        {
            return _attributeDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
