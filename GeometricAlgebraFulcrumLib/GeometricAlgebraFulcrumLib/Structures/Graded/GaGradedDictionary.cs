using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Structures.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Graded
{
    public record GaGradedDictionary<T> :
        IGaGradedDictionary<T>
    {
        private readonly Dictionary<uint, IGaEvenDictionary<T>> _gradeKeyValueDictionary;


        public int Count 
            => _gradeKeyValueDictionary.Count;
        
        public IEnumerable<uint> Grades 
            => _gradeKeyValueDictionary.Keys;

        public IEnumerable<IGaEvenDictionary<T>> EvenDictionaries 
            => _gradeKeyValueDictionary.Values;

        public IEnumerable<uint> Keys 
            => _gradeKeyValueDictionary.Keys;

        public IEnumerable<IGaEvenDictionary<T>> Values 
            => _gradeKeyValueDictionary.Values;

        public IGaEvenDictionary<T> this[uint key]
        {
            get => _gradeKeyValueDictionary.TryGetValue(key, out var keyValueDictionary)
                ? keyValueDictionary
                : GaEvenDictionaryEmpty<T>.DefaultDictionary;
            set
            {
                if (ReferenceEquals(value, null))
                    _gradeKeyValueDictionary.Remove(key);

                else if (_gradeKeyValueDictionary.ContainsKey(key))
                    _gradeKeyValueDictionary[key] = value;

                else
                    _gradeKeyValueDictionary.Add(key, value);
            }
        }


        internal GaGradedDictionary()
        {
            _gradeKeyValueDictionary = new Dictionary<uint, IGaEvenDictionary<T>>();
        }

        internal GaGradedDictionary([NotNull] Dictionary<uint, IGaEvenDictionary<T>> gradeKeyValueDictionary)
        {
            _gradeKeyValueDictionary = gradeKeyValueDictionary;
        }


        public void Clear()
        {
            _gradeKeyValueDictionary.Clear();
        }

        public bool Remove(uint grade)
        {
            return _gradeKeyValueDictionary.Remove(grade);
        }

        public bool ContainsKey(uint key)
        {
            return _gradeKeyValueDictionary.ContainsKey(key);
        }

        public bool TryGetValue(uint key, out IGaEvenDictionary<T> value)
        {
            return _gradeKeyValueDictionary.TryGetValue(key, out value);
        }


        public bool IsEmpty()
        {
            return _gradeKeyValueDictionary
                .Values
                .All(dict => dict.IsEmpty());
        }

        public ulong GetMaxBasisBladeId()
        {
            var maxBasisBladeId = 0UL;

            foreach (var (grade, keyValueDictionary) in _gradeKeyValueDictionary)
            {
                var m = keyValueDictionary.GetMaxBasisBladeId(grade);

                if (m > maxBasisBladeId)
                    maxBasisBladeId = m;
            }

            return maxBasisBladeId;
        }

        public uint GetMaxGrade()
        {
            return _gradeKeyValueDictionary.Count == 0
                ? 0U
                : _gradeKeyValueDictionary.Keys.Max();
        }

        public uint GetFirstGrade()
        {
            return _gradeKeyValueDictionary.Count == 0
                ? throw new InvalidOperationException()
                : _gradeKeyValueDictionary.Keys.Min();
        }

        public uint GetLastGrade()
        {
            return _gradeKeyValueDictionary.Count == 0
                ? throw new InvalidOperationException()
                : _gradeKeyValueDictionary.Keys.Max();
        }

        public IGaEvenDictionary<T> GetFirstEvenDictionary()
        {
            var grade = GetFirstGrade();

            return _gradeKeyValueDictionary[grade];
        }

        public IGaEvenDictionary<T> GetLastEvenDictionary()
        {
            var grade = GetLastGrade();

            return _gradeKeyValueDictionary[grade];
        }

        public KeyValuePair<uint, IGaEvenDictionary<T>> GetFirstPair()
        {
            var grade = GetFirstGrade();

            return new KeyValuePair<uint, IGaEvenDictionary<T>>(
                grade,
                _gradeKeyValueDictionary[grade]
            );
        }

        public KeyValuePair<uint, IGaEvenDictionary<T>> GetLastPair()
        {
            var grade = GetLastGrade();

            return new KeyValuePair<uint, IGaEvenDictionary<T>>(
                grade,
                _gradeKeyValueDictionary[grade]
            );
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetKeyValuePairs(Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            foreach (var (grade, evenDictionary) in _gradeKeyValueDictionary)
            foreach (var (key, value) in evenDictionary)
                yield return new KeyValuePair<ulong, T>(
                    gradeKeyToKeyMapping(grade, key), 
                    value
                );
        }

        public IEnumerable<Tuple<ulong, T>> GetKeyValueTuples(Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            foreach (var (grade, evenDictionary) in _gradeKeyValueDictionary)
            foreach (var (key, value) in evenDictionary)
                yield return new Tuple<ulong, T>(
                    gradeKeyToKeyMapping(grade, key), 
                    value
                );
        }

        public IEnumerable<Tuple<uint, ulong, T>> GetGradeKeyValueTuples()
        {
            foreach (var (grade, evenDictionary) in _gradeKeyValueDictionary)
                foreach (var (key, value) in evenDictionary)
                    yield return new Tuple<uint, ulong, T>(grade, key, value);
        }

        public IGaGradedDictionary<T> GetCopy()
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaEvenDictionary<T>>();

            foreach (var (grade, evenDictionary) in _gradeKeyValueDictionary)
            {
                gradeKeyValueDictionary.Add(
                    grade,
                    evenDictionary.GetCopy()
                );
            }

            return gradeKeyValueDictionary.CreateGradedDictionary();
        }

        public IGaGradedDictionary<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaEvenDictionary<T2>>();

            foreach (var (grade, evenDictionary) in _gradeKeyValueDictionary)
            {
                gradeKeyValueDictionary.Add(
                    grade,
                    evenDictionary.MapValues(valueMapping)
                );
            }

            return gradeKeyValueDictionary.CreateGradedDictionary();
        }

        public IGaGradedDictionary<T2> MapValues<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaEvenDictionary<T2>>();

            foreach (var (grade, evenDictionary) in _gradeKeyValueDictionary)
            {
                gradeKeyValueDictionary.Add(
                    grade,
                    evenDictionary.MapValues((key, value) => gradeKeyValueMapping(grade, key, value))
                );
            }

            return gradeKeyValueDictionary.CreateGradedDictionary();
        }

        public IGaGradedDictionary<T> FilterByGrade(Func<ulong, bool> gradeFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaEvenDictionary<T>>();

            foreach (var (grade, evenDictionary) in _gradeKeyValueDictionary)
            {
                if (!gradeFilter(grade) || evenDictionary.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedDictionary();
        }

        public IGaGradedDictionary<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaEvenDictionary<T>>();

            foreach (var (grade, evenDict) in _gradeKeyValueDictionary)
            {
                var evenDictionary = evenDict.FilterByValue(
                    value => gradeValueFilter(grade, value)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedDictionary();
        }

        public IGaGradedDictionary<T> FilterByGradeKeyValue(Func<uint, ulong, T, bool> gradeKeyValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaEvenDictionary<T>>();

            foreach (var (grade, evenDict) in _gradeKeyValueDictionary)
            {
                var evenDictionary = evenDict.FilterByKeyValue(
                    (key, value) => gradeKeyValueFilter(grade, key, value)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedDictionary();
        }

        public IGaGradedDictionary<T> FilterByValue(Func<T, bool> valueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaEvenDictionary<T>>();

            foreach (var (grade, evenDict) in _gradeKeyValueDictionary)
            {
                var evenDictionary = evenDict.FilterByValue(valueFilter);

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedDictionary();
        }

        public IGaEvenDictionary<T> ToEvenDictionary()
        {
            return ToEvenDictionary(GaBasisUtils.BasisBladeId);
        }

        public IGaEvenDictionary<T> ToEvenDictionary(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            var keyValueDictionary = new Dictionary<ulong, T>();

            foreach (var (grade, evenDictionary) in _gradeKeyValueDictionary)
            {
                foreach (var (key, value) in evenDictionary)
                    keyValueDictionary.Add(
                        gradeKeyToEvenKeyMapping(grade, key), 
                        value
                    );
            }

            return keyValueDictionary.CreateEvenDictionary();
        }

        public IEnumerator<KeyValuePair<uint, IGaEvenDictionary<T>>> GetEnumerator()
        {
            return _gradeKeyValueDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}