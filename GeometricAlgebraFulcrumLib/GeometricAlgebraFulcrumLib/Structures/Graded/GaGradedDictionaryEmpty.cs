using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Structures.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Graded
{
    public record GaGradedDictionaryEmpty<T> :
        IGaGradedDictionary<T>
    {
        public static GaGradedDictionaryEmpty<T> DefaultDictionary { get; }
            = new GaGradedDictionaryEmpty<T>();


        public int Count 
            => 0;

        public IEnumerable<uint> Grades 
            => Enumerable.Empty<uint>();

        public IEnumerable<IGaEvenDictionary<T>> EvenDictionaries 
            => Enumerable.Empty<IGaEvenDictionary<T>>();

        public IEnumerable<uint> Keys 
            => Enumerable.Empty<uint>();
        
        public IEnumerable<IGaEvenDictionary<T>> Values 
            => Enumerable.Empty<IGaEvenDictionary<T>>();

        public IGaEvenDictionary<T> this[uint key] 
            => GaEvenDictionaryEmpty<T>.DefaultDictionary;

        private GaGradedDictionaryEmpty()
        {
        }


        public bool ContainsKey(uint key)
        {
            return false;
        }

        public bool TryGetValue(uint key, out IGaEvenDictionary<T> value)
        {
            value = null;
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

        public uint GetMaxGrade()
        {
            return 0U;
        }

        public uint GetFirstGrade()
        {
            throw new InvalidOperationException();
        }

        public uint GetLastGrade()
        {
            throw new InvalidOperationException();
        }

        public IGaEvenDictionary<T> GetFirstEvenDictionary()
        {
            throw new InvalidOperationException();
        }

        public IGaEvenDictionary<T> GetLastEvenDictionary()
        {
            throw new InvalidOperationException();
        }

        public KeyValuePair<uint, IGaEvenDictionary<T>> GetFirstPair()
        {
            throw new InvalidOperationException();
        }

        public KeyValuePair<uint, IGaEvenDictionary<T>> GetLastPair()
        {
            throw new InvalidOperationException();
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetKeyValuePairs(Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            return Enumerable.Empty<KeyValuePair<ulong, T>>();
        }

        public IEnumerable<Tuple<ulong, T>> GetKeyValueTuples(Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            return Enumerable.Empty<Tuple<ulong, T>>();
        }

        public IEnumerable<Tuple<uint, ulong, T>> GetGradeKeyValueTuples()
        {
            return Enumerable.Empty<Tuple<uint, ulong, T>>();
        }

        public IGaGradedDictionary<T> GetCopy()
        {
            return GaGradedDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaGradedDictionary<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return GaGradedDictionaryEmpty<T2>.DefaultDictionary;
        }

        public IGaGradedDictionary<T2> MapValues<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            return GaGradedDictionaryEmpty<T2>.DefaultDictionary;
        }

        public IGaGradedDictionary<T> FilterByGrade(Func<ulong, bool> gradeFilter)
        {
            return GaGradedDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaGradedDictionary<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter)
        {
            return GaGradedDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaGradedDictionary<T> FilterByGradeKeyValue(Func<uint, ulong, T, bool> gradeKeyValueFilter)
        {
            return GaGradedDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaGradedDictionary<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return GaGradedDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaEvenDictionary<T> ToEvenDictionary()
        {
            return GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaEvenDictionary<T> ToEvenDictionary(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public IEnumerator<KeyValuePair<uint, IGaEvenDictionary<T>>> GetEnumerator()
        {
            return Enumerable.Empty<KeyValuePair<uint, IGaEvenDictionary<T>>>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}