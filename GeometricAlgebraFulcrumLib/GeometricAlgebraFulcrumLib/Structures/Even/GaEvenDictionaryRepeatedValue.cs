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
    public sealed class GaEvenDictionaryRepeatedValue<T> :
        IGaEvenDictionary<T>
    {
        public T Value { get; set; }

        public int Count { get; }

        public T this[ulong key] 
            => key < (ulong) Count 
                ? Value : default;

        public IEnumerable<ulong> Keys 
            => ((ulong) Count).GetRange();

        public IEnumerable<T> Values 
            => Enumerable.Repeat(Value, Count);


        internal GaEvenDictionaryRepeatedValue(int count, [NotNull] T value)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count));

            Count = count;
            Value = value;
        }

        
        public bool ContainsKey(ulong key)
        {
            return key < (ulong) Count;
        }

        public bool TryGetValue(ulong key, out T value)
        {
            if (key < (ulong) Count)
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
            return (ulong) Count;
        }

        public ulong GetMaxBasisBladeId(uint grade)
        {
            return GaBasisUtils.BasisBladeId(grade, (ulong) Count);
        }

        public ulong GetFirstKey()
        {
            return 0UL;
        }

        public ulong GetLastKey()
        {
            return (ulong) (Count - 1);
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
            return new KeyValuePair<ulong, T>((ulong) (Count - 1), Value);
        }

        public IGaEvenDictionary<T> GetCopy()
        {
            return new GaEvenDictionaryRepeatedValue<T>(Count, Value);
        }

        public IGaEvenDictionary<T> MapKeys(Func<ulong, ulong> keyMapping)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            for (var key = 0UL; key < (ulong) Count; key++)
                valueDictionary.Add(keyMapping(key), Value);

            return valueDictionary.CreateEvenDictionary();
        }

        public IGaEvenDictionary<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return new GaEvenDictionaryRepeatedValue<T2>(
                Count, 
                valueMapping(Value)
            );
        }

        public IGaEvenDictionary<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            var valuesList = new T2[Count];

            for (var i = 0; i < Count; i++)
                valuesList[i] = keyValueMapping((ulong) i, Value);

            return new GaEvenDictionaryList<T2>(valuesList);
        }

        public IGaEvenDictionary<T> FilterByKey(Func<ulong, bool> keyFilter)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            for (var key = 0UL; key < (ulong) Count; key++)
                if (keyFilter(key))
                    valueDictionary.Add(key, Value);

            return valueDictionary.CreateEvenDictionary();
        }

        public IGaEvenDictionary<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            for (var key = 0UL; key < (ulong) Count; key++)
                if (keyValueFilter(key, Value))
                    valueDictionary.Add(key, Value);

            return new GaEvenDictionary<T>(valueDictionary);
        }

        public IGaEvenDictionary<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return valueFilter(Value)
                ? this 
                : GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaGradedDictionary<T> ToGradedDictionary()
        {
            return ToGradedDictionary(GaBasisUtils.BasisBladeGradeIndex);
        }

        public IGaGradedDictionary<T> ToGradedDictionary(Func<ulong, Tuple<uint, ulong>> evenKeyToGradeKeyMapping)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            for (var id = 0UL; id < (ulong) Count; id++)
            {
                var (grade, index) = evenKeyToGradeKeyMapping(id);

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var keyValueDictionary))
                {
                    keyValueDictionary = new Dictionary<ulong, T>();
                    gradeKeyValueDictionary.Add(grade, keyValueDictionary);
                }

                if (keyValueDictionary.ContainsKey(index))
                    keyValueDictionary[index] = Value;
                else
                    keyValueDictionary.Add(index, Value);
            }

            return gradeKeyValueDictionary.CreateGradedDictionary();
        }

        public IEnumerator<KeyValuePair<ulong, T>> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
                yield return new KeyValuePair<ulong, T>((ulong) i, Value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}