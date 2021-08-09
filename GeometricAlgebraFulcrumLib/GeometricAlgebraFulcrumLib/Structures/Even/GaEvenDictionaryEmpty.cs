using System;
using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Structures.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Even
{
    public record GaEvenDictionaryEmpty<T> :
        IGaEvenDictionary<T>
    {
        public static GaEvenDictionaryEmpty<T> DefaultDictionary { get; }
            = new GaEvenDictionaryEmpty<T>();


        public int Count => 0;

        public T this[ulong key] 
            => default;

        public IEnumerable<ulong> Keys
        {
            get { yield break; }
        }
        
        public IEnumerable<T> Values
        {
            get { yield break; }
        }


        private GaEvenDictionaryEmpty()
        {
        }


        public bool ContainsKey(ulong key)
        {
            return false;
        }

        public bool TryGetValue(ulong key, out T value)
        {
            value = default;
            return false;
        }

        public bool IsEmpty()
        {
            return true;
        }

        public ulong GetMaxBasisBladeId()
        {
            return 0UL;
        }

        public ulong GetMaxBasisBladeId(uint grade)
        {
            return 0UL;
        }

        public ulong GetFirstKey()
        {
            throw new InvalidOperationException();
        }

        public ulong GetLastKey()
        {
            throw new InvalidOperationException();
        }

        public T GetFirstValue()
        {
            throw new InvalidOperationException();
        }

        public T GetLastValue()
        {
            throw new InvalidOperationException();
        }

        public KeyValuePair<ulong, T> GetFirstPair()
        {
            throw new InvalidOperationException();
        }

        public KeyValuePair<ulong, T> GetLastPair()
        {
            throw new InvalidOperationException();
        }

        public IGaEvenDictionary<T> GetCopy()
        {
            return this;
        }

        public IGaEvenDictionary<T> MapKeys(Func<ulong, ulong> keyMapping)
        {
            return GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaEvenDictionary<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return GaEvenDictionaryEmpty<T2>.DefaultDictionary;
        }

        public IGaEvenDictionary<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            return GaEvenDictionaryEmpty<T2>.DefaultDictionary;
        }

        public IGaEvenDictionary<T> FilterByKey(Func<ulong, bool> keyFilter)
        {
            return GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaEvenDictionary<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter)
        {
            return GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaEvenDictionary<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaGradedDictionary<T> ToGradedDictionary()
        {
            return GaGradedDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaGradedDictionary<T> ToGradedDictionary(Func<ulong, Tuple<uint, ulong>> evenKeyToGradeKeyMapping)
        {
            return GaGradedDictionaryEmpty<T>.DefaultDictionary;
        }

        public IEnumerator<KeyValuePair<ulong, T>> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}