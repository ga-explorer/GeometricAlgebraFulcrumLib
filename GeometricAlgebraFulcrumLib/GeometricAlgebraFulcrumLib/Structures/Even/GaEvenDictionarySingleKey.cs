using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Structures.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Even
{
    public sealed class GaEvenDictionarySingleKey<T> :
        IGaEvenDictionary<T>
    {
        public ulong Key { get; }

        public T Value { get; set; }

        public int Count 
            => 1;

        public T this[ulong key] 
            => key == Key 
                ? Value : default;

        public IEnumerable<ulong> Keys
        {
            get { yield return Key; }
        }
        
        public IEnumerable<T> Values
        {
            get { yield return Value; }
        }


        internal GaEvenDictionarySingleKey(ulong key, [NotNull] T value)
        {
            Key = key;
            Value = value;
        }

        
        public bool ContainsKey(ulong key)
        {
            return Key == key;
        }

        public bool TryGetValue(ulong key, out T value)
        {
            if (key == Key)
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
            return Key;
        }

        public ulong GetMaxBasisBladeId(uint grade)
        {
            return GaBasisUtils.BasisBladeId(grade, Key);
        }

        public ulong GetFirstKey()
        {
            return Key;
        }

        public ulong GetLastKey()
        {
            return Key;
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
            return new KeyValuePair<ulong, T>(Key, Value);
        }

        public KeyValuePair<ulong, T> GetLastPair()
        {
            return new KeyValuePair<ulong, T>(Key, Value);
        }

        public IGaEvenDictionary<T> GetCopy()
        {
            return this;
        }

        public IGaEvenDictionary<T> MapKeys(Func<ulong, ulong> keyMapping)
        {
            var key = keyMapping(Key);

            return key == 0UL
                ? new GaEvenDictionarySingleZeroKey<T>(Value)
                : new GaEvenDictionarySingleKey<T>(key, Value);
        }

        public IGaEvenDictionary<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return new GaEvenDictionarySingleKey<T2>(Key, valueMapping(Value));
        }

        public IGaEvenDictionary<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            return new GaEvenDictionarySingleKey<T2>(Key, keyValueMapping(Key, Value));
        }

        public IGaEvenDictionary<T> FilterByKey(Func<ulong, bool> keyFilter)
        {
            return keyFilter(Key)
                ? this
                : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaEvenDictionary<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter)
        {
            return keyValueFilter(Key, Value)
                ? this
                : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaEvenDictionary<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return valueFilter(Value)
                ? this
                : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaGradedDictionary<T> ToGradedDictionary()
        {
            var (grade, index) = Key.BasisBladeGradeIndex();

            IGaEvenDictionary<T> evenDictionary = 
                index == 0
                    ? new GaEvenDictionarySingleZeroKey<T>(Value) 
                    : new GaEvenDictionarySingleKey<T>(index, Value);

            return grade == 0
                ? new GaGradedDictionarySingleZeroGrade<T>(evenDictionary)
                : new GaGradedDictionarySingleGrade<T>(grade, evenDictionary);
        }

        public IGaGradedDictionary<T> ToGradedDictionary(Func<ulong, Tuple<uint, ulong>> evenKeyToGradeKeyMapping)
        {
            var (grade, index) = evenKeyToGradeKeyMapping(Key);

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
            yield return new KeyValuePair<ulong, T>(Key, Value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}