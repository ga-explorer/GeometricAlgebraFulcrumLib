using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Structures.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Even
{
    public record GaEvenDictionary<T> :
        IGaEvenDictionary<T>
    {
        private readonly Dictionary<ulong, T> _keyValueDictionary;


        public int Count 
            => _keyValueDictionary.Count;
        
        public T this[ulong key]
        {
            get => _keyValueDictionary.TryGetValue(key, out var value)
                ? value
                : default;
            set
            {
                if (_keyValueDictionary.ContainsKey(key))
                    _keyValueDictionary[key] = value;
                else
                    _keyValueDictionary.Add(key, value);
            }
        }

        public IEnumerable<ulong> Keys 
            => _keyValueDictionary.Keys;

        public IEnumerable<T> Values 
            => _keyValueDictionary.Values;


        internal GaEvenDictionary()
        {
            _keyValueDictionary = new Dictionary<ulong, T>();
        }

        internal GaEvenDictionary([NotNull] Dictionary<ulong, T> valueDictionary)
        {
            _keyValueDictionary = valueDictionary;
        }


        public void Clear()
        {
            _keyValueDictionary.Clear();
        }

        public bool Remove(ulong key)
        {
            return _keyValueDictionary.Remove(key);
        }

        public void Remove(params ulong[] keysList)
        {
            foreach (var key in keysList)
                _keyValueDictionary.Remove(key);
        }

        public void Remove(IEnumerable<ulong> keysList)
        {
            foreach (var key in keysList.ToArray())
                _keyValueDictionary.Remove(key);
        }


        public bool ContainsKey(ulong key)
        {
            return _keyValueDictionary.ContainsKey(key);
        }

        public bool TryGetValue(ulong key, out T value)
        {
            return _keyValueDictionary.TryGetValue(key, out value);
        }

        public bool IsEmpty()
        {
            return _keyValueDictionary.Count == 0;
        }

        public ulong GetMaxBasisBladeId()
        {
            return _keyValueDictionary.Keys.GetMaxBasisBladeId();
        }

        public ulong GetMaxBasisBladeId(uint grade)
        {
            return _keyValueDictionary.Keys.GetMaxBasisBladeId(grade);
        }

        public ulong GetFirstKey()
        {
            if (_keyValueDictionary.Count == 0)
                throw new InvalidOperationException();

            return _keyValueDictionary.Keys.Min();
        }

        public ulong GetLastKey()
        {
            if (_keyValueDictionary.Count == 0)
                throw new InvalidOperationException();

            return _keyValueDictionary.Keys.Max();
        }

        public T GetFirstValue()
        {
            var key = GetFirstKey();

            return _keyValueDictionary[key];
        }

        public T GetLastValue()
        {
            var key = GetLastKey();

            return _keyValueDictionary[key];
        }

        public KeyValuePair<ulong, T> GetFirstPair()
        {
            var key = GetFirstKey();

            return new KeyValuePair<ulong, T>(
                key, 
                _keyValueDictionary[key]
            );
        }

        public KeyValuePair<ulong, T> GetLastPair()
        {
            var key = GetLastKey();

            return new KeyValuePair<ulong, T>(
                key, 
                _keyValueDictionary[key]
            );
        }

        public IGaEvenDictionary<T> GetCopy()
        {
            return _keyValueDictionary
                .CopyToDictionary()
                .CreateEvenDictionary();
        }

        public IGaEvenDictionary<T> MapKeys(Func<ulong, ulong> keyMapping)
        {
            return _keyValueDictionary
                .ToDictionary(
                    pair => keyMapping(pair.Key),
                    pair => pair.Value
                )
                .CreateEvenDictionary();
        }

        public IGaEvenDictionary<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return _keyValueDictionary
                .CopyToDictionary(valueMapping)
                .CreateEvenDictionary();
        }

        public IGaEvenDictionary<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            return _keyValueDictionary.ToDictionary(
                pair => pair.Key,
                pair => keyValueMapping(pair.Key, pair.Value)
            ).CreateEvenDictionary();
        }

        public IGaEvenDictionary<T> FilterByKey(Func<ulong, bool> keyFilter)
        {
            return _keyValueDictionary
                .Where(pair => keyFilter(pair.Key))
                .CopyToDictionary()
                .CreateEvenDictionary();
        }

        public IGaEvenDictionary<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter)
        {
            return _keyValueDictionary
                .Where(pair => keyValueFilter(pair.Key, pair.Value))
                .CopyToDictionary()
                .CreateEvenDictionary();
        }

        public IGaEvenDictionary<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return _keyValueDictionary
                .Where(pair => valueFilter(pair.Value))
                .CopyToDictionary()
                .CreateEvenDictionary();
        }

        public IGaGradedDictionary<T> ToGradedDictionary()
        {
            return ToGradedDictionary(GaBasisUtils.BasisBladeGradeIndex);
        }

        public IGaGradedDictionary<T> ToGradedDictionary(Func<ulong, Tuple<uint, ulong>> evenKeyToGradeKeyMapping)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (id, value) in _keyValueDictionary)
            {
                var (grade, index) = evenKeyToGradeKeyMapping(id);

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var keyValueDictionary))
                {
                    keyValueDictionary = new Dictionary<ulong, T>();
                    gradeKeyValueDictionary.Add(grade, keyValueDictionary);
                }

                if (keyValueDictionary.ContainsKey(index))
                    keyValueDictionary[index] = value;
                else
                    keyValueDictionary.Add(index, value);
            }

            return gradeKeyValueDictionary.CreateGradedDictionary();
        }

        public IEnumerator<KeyValuePair<ulong, T>> GetEnumerator()
        {
            return _keyValueDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}