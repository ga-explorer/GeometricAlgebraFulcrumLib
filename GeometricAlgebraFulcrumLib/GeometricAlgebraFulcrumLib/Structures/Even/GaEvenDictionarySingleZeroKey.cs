using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Structures.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Even
{
    public sealed class GaEvenDictionarySingleZeroKey<T> :
        IGaEvenDictionary<T>
    {
        public ulong Key 
            => 0UL;

        public T Value { get; set; }

        public int Count 
            => 1;

        public T this[ulong key] 
            => key == 0UL 
                ? Value : default;

        public IEnumerable<ulong> Keys
        {
            get { yield return 0UL; }
        }
        
        public IEnumerable<T> Values
        {
            get { yield return Value; }
        }


        internal GaEvenDictionarySingleZeroKey([NotNull] T value)
        {
            Value = value;
        }

        
        public bool ContainsKey(ulong key)
        {
            return key == 0UL;
        }

        public bool TryGetValue(ulong key, out T value)
        {
            if (key == 0UL)
            {
                value = Value;
                return true;
            }

            value = default;
            return false;
        }

        public bool IsEmpty()
        {
            return false;
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
            return 0UL;
        }

        public ulong GetLastKey()
        {
            return 0UL;
        }

        public T GetFirstValue()
        {
            return Value;
        }

        public T GetLastValue()
        {
            return Value;
        }

        public KeyValuePair<ulong, T> GetFirstPair()
        {
            return new KeyValuePair<ulong, T>(0UL, Value);
        }

        public KeyValuePair<ulong, T> GetLastPair()
        {
            return new KeyValuePair<ulong, T>(0UL, Value);
        }

        public IGaEvenDictionary<T> GetCopy()
        {
            return this;
        }

        public IGaEvenDictionary<T> MapKeys(Func<ulong, ulong> keyMapping)
        {
            var key = keyMapping(0UL);

            return key == 0UL
                ? new GaEvenDictionarySingleZeroKey<T>(Value)
                : new GaEvenDictionarySingleKey<T>(key, Value);
        }

        public IGaEvenDictionary<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return new GaEvenDictionarySingleZeroKey<T2>(valueMapping(Value));
        }

        public IGaEvenDictionary<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            return new GaEvenDictionarySingleZeroKey<T2>(keyValueMapping(0UL, Value));
        }

        public IGaEvenDictionary<T> FilterByKey(Func<ulong, bool> keyFilter)
        {
            return keyFilter(0UL)
                ? this : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaEvenDictionary<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter)
        {
            return keyValueFilter(0UL, Value)
                ? this : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaEvenDictionary<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return valueFilter(Value)
                ? this : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaGradedDictionary<T> ToGradedDictionary()
        {
            IGaEvenDictionary<T> evenDictionary = 
                new GaEvenDictionarySingleZeroKey<T>(Value);

            return new GaGradedDictionarySingleZeroGrade<T>(evenDictionary);
        }

        public IGaGradedDictionary<T> ToGradedDictionary(Func<ulong, Tuple<uint, ulong>> evenKeyToGradeKeyMapping)
        {
            var (grade, index) = evenKeyToGradeKeyMapping(0UL);

            IGaEvenDictionary<T> evenDictionary = 
                index == 0
                    ? new GaEvenDictionarySingleZeroKey<T>(Value) 
                    : new GaEvenDictionarySingleKey<T>(index, Value);

            return grade == 0
                ? new GaGradedDictionarySingleZeroGrade<T>(evenDictionary)
                : new GaGradedDictionarySingleGrade<T>(grade, evenDictionary);
        }

        public IEnumerator<KeyValuePair<ulong, T>> GetEnumerator()
        {
            yield return new KeyValuePair<ulong, T>(0UL, Value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}