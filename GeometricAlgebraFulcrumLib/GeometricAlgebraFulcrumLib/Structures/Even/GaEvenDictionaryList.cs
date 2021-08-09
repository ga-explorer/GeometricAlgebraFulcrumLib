using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Structures.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Even
{
    public record GaEvenDictionaryList<T> :
        IGaEvenDictionary<T>
    {
        private readonly IReadOnlyList<T> _valuesList;

        public int Count 
            => _valuesList.Count;

        public T this[ulong key] 
        {
            get
            {
                var i = (int) key;

                return (i < _valuesList.Count) 
                    ? _valuesList[i] : default;
            }
        }

        public IEnumerable<ulong> Keys 
            => ((ulong) _valuesList.Count).GetRange();

        public IEnumerable<T> Values 
            => _valuesList;


        internal GaEvenDictionaryList([NotNull] IReadOnlyList<T> valuesList)
        {
            _valuesList = valuesList;
        }


        public bool ContainsKey(ulong key)
        {
            return key < (ulong) _valuesList.Count;
        }

        public bool TryGetValue(ulong key, out T value)
        {
            var i = (int) key;

            if (i < _valuesList.Count)
            {
                value = _valuesList[i];
                return true;
            }

            value = default;
            return false;
        }

        public bool IsEmpty()
        {
            return _valuesList.Count == 0;
        }

        public ulong GetMaxBasisBladeId()
        {
            return (ulong) _valuesList.Count;
        }

        public ulong GetMaxBasisBladeId(uint grade)
        {
            return GaBasisUtils.BasisBladeId(grade, (ulong) _valuesList.Count);
        }

        public ulong GetFirstKey()
        {
            return 0UL;
        }

        public ulong GetLastKey()
        {
            return (ulong ) (_valuesList.Count - 1);
        }

        public T GetFirstValue()
        {
            return _valuesList[0];
        }

        public T GetLastValue()
        {
            return _valuesList[^1];
        }

        public KeyValuePair<ulong, T> GetFirstPair()
        {
            return new KeyValuePair<ulong, T>(0UL, _valuesList[0]);
        }

        public KeyValuePair<ulong, T> GetLastPair()
        {
            return new KeyValuePair<ulong, T>((ulong ) (_valuesList.Count - 1), _valuesList[^1]);
        }

        public IGaEvenDictionary<T> GetCopy()
        {
            var valuesList = new List<T>(_valuesList);

            return new GaEvenDictionaryList<T>(valuesList);
        }

        public IGaEvenDictionary<T> MapKeys(Func<ulong, ulong> keyMapping)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            for (var key = 0UL; key < (ulong) Count; key++)
            {
                valueDictionary.Add(
                    keyMapping(key), 
                    _valuesList[(int) key]
                );
            }

            return valueDictionary.CreateEvenDictionary();
        }

        public IGaEvenDictionary<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            var valuesList = _valuesList.Select(valueMapping).ToArray();

            return new GaEvenDictionaryList<T2>(valuesList);
        }

        public IGaEvenDictionary<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            var valuesList = new T2[_valuesList.Count];

            for (var i = 0; i < _valuesList.Count; i++)
                valuesList[i] = keyValueMapping((ulong) i, _valuesList[i]);

            return new GaEvenDictionaryList<T2>(valuesList);
        }

        public IGaEvenDictionary<T> FilterByKey(Func<ulong, bool> keyFilter)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            for (var key = 0UL; key < (ulong) Count; key++)
            {
                if (keyFilter(key))
                    valueDictionary.Add(key, _valuesList[(int) key]);
            }

            return valueDictionary.CreateEvenDictionary();
        }

        public IGaEvenDictionary<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            for (var key = 0UL; key < (ulong) Count; key++)
            {
                var value = _valuesList[(int) key];

                if (keyValueFilter(key, value))
                    valueDictionary.Add(key, value);
            }

            return valueDictionary.CreateEvenDictionary();
        }

        public IGaEvenDictionary<T> FilterByValue(Func<T, bool> valueFilter)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            for (var key = 0UL; key < (ulong) Count; key++)
            {
                var value = _valuesList[(int) key];

                if (valueFilter(value))
                    valueDictionary.Add(key, value);
            }

            return valueDictionary.CreateEvenDictionary();
        }

        public IGaGradedDictionary<T> ToGradedDictionary()
        {
            return ToGradedDictionary(GaBasisUtils.BasisBladeGradeIndex);
        }

        public IGaGradedDictionary<T> ToGradedDictionary(Func<ulong, Tuple<uint, ulong>> evenKeyToGradeKeyMapping)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            for (var id = 0UL; id < (ulong) _valuesList.Count; id++)
            {
                var value = _valuesList[(int) id];
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
            return _valuesList
                .Select((value, i) => new KeyValuePair<ulong, T>((ulong) i, value))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}