using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Structures.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Graded
{
    public sealed class GaGradedDictionarySingleGrade<T> :
        IGaGradedDictionary<T>
    {
        private IGaEvenDictionary<T> _evenDictionary;


        public uint Grade { get; }

        public IGaEvenDictionary<T> EvenDictionary
        {
            get => _evenDictionary;
            set => _evenDictionary = value ?? GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        public int Count 
            => 1;

        public IEnumerable<uint> Grades
        {
            get { yield return Grade; }
        }
        
        public IEnumerable<IGaEvenDictionary<T>> EvenDictionaries
        {
            get { yield return _evenDictionary; }
        }

        public IEnumerable<uint> Keys
        {
            get { yield return Grade; }
        }

        public IEnumerable<IGaEvenDictionary<T>> Values
        {
            get { yield return _evenDictionary; }
        }

        public IGaEvenDictionary<T> this[uint key] 
            => key == Grade
                ? _evenDictionary 
                : GaEvenDictionaryEmpty<T>.DefaultDictionary;


        internal GaGradedDictionarySingleGrade(uint grade)
        {
            Grade = grade;
            _evenDictionary = GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }

        internal GaGradedDictionarySingleGrade(uint grade, IGaEvenDictionary<T> evenDictionary)
        {
            Grade = grade;
            _evenDictionary = evenDictionary 
                              ?? GaEvenDictionaryEmpty<T>.DefaultDictionary;
        }


        public bool ContainsKey(uint key)
        {
            return key == Grade;
        }

        public bool TryGetValue(uint key, out IGaEvenDictionary<T> value)
        {
            if (key == Grade)
            {
                value = _evenDictionary;
                return true;
            }

            value = null;
            return false;
        }

        public bool IsEmpty()
        {
            return false;
        }

        public ulong GetMaxBasisBladeId()
        {
            return _evenDictionary.GetMaxBasisBladeId(Grade);
        }

        public uint GetMaxGrade()
        {
            return Grade;
        }

        public uint GetFirstGrade()
        {
            return Grade;
        }

        public uint GetLastGrade()
        {
            return Grade;
        }

        public IGaEvenDictionary<T> GetFirstEvenDictionary()
        {
            return _evenDictionary;
        }

        public IGaEvenDictionary<T> GetLastEvenDictionary()
        {
            return _evenDictionary;
        }

        public KeyValuePair<uint, IGaEvenDictionary<T>> GetFirstPair()
        {
            return new KeyValuePair<uint, IGaEvenDictionary<T>>(
                Grade,
                _evenDictionary
            );
        }

        public KeyValuePair<uint, IGaEvenDictionary<T>> GetLastPair()
        {
            return new KeyValuePair<uint, IGaEvenDictionary<T>>(
                Grade,
                _evenDictionary
            );
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetKeyValuePairs(Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            return _evenDictionary.Select(pair => 
                new KeyValuePair<ulong, T>(
                    gradeKeyToKeyMapping(Grade, pair.Key),
                    pair.Value
                )
            );
        }

        public IEnumerable<Tuple<ulong, T>> GetKeyValueTuples(Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            return _evenDictionary.Select(pair => 
                new Tuple<ulong, T>(
                    gradeKeyToKeyMapping(Grade, pair.Key),
                    pair.Value
                )
            );
        }

        public IEnumerable<Tuple<uint, ulong, T>> GetGradeKeyValueTuples()
        {
            return _evenDictionary.Select(pair => 
                new Tuple<uint, ulong, T>(Grade, pair.Key, pair.Value)
            );
        }

        public IGaGradedDictionary<T> GetCopy()
        {
            return new GaGradedDictionarySingleGrade<T>(
                Grade,
                _evenDictionary.GetCopy()
            );
        }

        public IGaGradedDictionary<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return new GaGradedDictionarySingleGrade<T2>(
                Grade,
                _evenDictionary.MapValues(valueMapping)
            );
        }

        public IGaGradedDictionary<T2> MapValues<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            return new GaGradedDictionarySingleGrade<T2>(
                Grade,
                _evenDictionary.MapValues((key, value) => 
                    gradeKeyValueMapping(Grade, key, value)
                )
            );
        }

        public IGaGradedDictionary<T> FilterByGrade(Func<ulong, bool> gradeFilter)
        {
            return gradeFilter(Grade)
                ? this
                : GaGradedDictionaryEmpty<T>.DefaultDictionary;
        }

        public IGaGradedDictionary<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter)
        {
            var evenDictionary = _evenDictionary.FilterByValue(
                value => gradeValueFilter(Grade, value)
            );

            return evenDictionary.IsEmpty()
                ? GaGradedDictionaryEmpty<T>.DefaultDictionary
                : new GaGradedDictionarySingleGrade<T>(Grade, evenDictionary);
        }

        public IGaGradedDictionary<T> FilterByGradeKeyValue(Func<uint, ulong, T, bool> gradeKeyValueFilter)
        {
            var evenDictionary = _evenDictionary.FilterByKeyValue(
                (key, value) => gradeKeyValueFilter(Grade, key, value)
            );

            return evenDictionary.IsEmpty()
                ? GaGradedDictionaryEmpty<T>.DefaultDictionary
                : new GaGradedDictionarySingleGrade<T>(Grade, evenDictionary);
        }

        public IGaGradedDictionary<T> FilterByValue(Func<T, bool> valueFilter)
        {
            var evenDictionary = _evenDictionary.FilterByValue(valueFilter);

            return evenDictionary.IsEmpty()
                ? GaGradedDictionaryEmpty<T>.DefaultDictionary
                : new GaGradedDictionarySingleGrade<T>(Grade, evenDictionary);
        }

        public IGaEvenDictionary<T> ToEvenDictionary()
        {
            return _evenDictionary.MapKeys(index => 
                GaBasisUtils.BasisBladeId(Grade, index)
            );
        }

        public IGaEvenDictionary<T> ToEvenDictionary(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return _evenDictionary.MapKeys(index => 
                gradeKeyToEvenKeyMapping(Grade, index)
            );
        }

        public IEnumerator<KeyValuePair<uint, IGaEvenDictionary<T>>> GetEnumerator()
        {
            yield return new KeyValuePair<uint, IGaEvenDictionary<T>>(Grade, _evenDictionary);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}