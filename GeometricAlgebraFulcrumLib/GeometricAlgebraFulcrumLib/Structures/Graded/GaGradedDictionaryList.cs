using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Structures.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Graded
{
    public record GaGradedDictionaryList<T> :
        IGaGradedDictionary<T>
    {
        private readonly IReadOnlyList<IGaEvenDictionary<T>> _evenDictionariesList;

        
        public IEnumerable<uint> Grades 
            => ((uint) _evenDictionariesList.Count).GetRange();
        
        public IEnumerable<IGaEvenDictionary<T>> EvenDictionaries 
            => _evenDictionariesList.Select(i => 
                i ?? GaEvenDictionaryEmpty<T>.DefaultDictionary
            );

        public IEnumerable<uint> Keys 
            => ((uint) _evenDictionariesList.Count).GetRange();

        public IEnumerable<IGaEvenDictionary<T>> Values
            => _evenDictionariesList.Select(i => 
                i ?? GaEvenDictionaryEmpty<T>.DefaultDictionary
            );

        public int Count 
            => _evenDictionariesList.Count;

        public IGaEvenDictionary<T> this[uint key]
        {
            get
            {
                var i = (int) key;

                return i >= _evenDictionariesList.Count
                    ? GaEvenDictionaryEmpty<T>.DefaultDictionary
                    : _evenDictionariesList[i] ?? GaEvenDictionaryEmpty<T>.DefaultDictionary;
            }
        }


        internal GaGradedDictionaryList([NotNull] IReadOnlyList<IGaEvenDictionary<T>> evenDictionariesList)
        {
            _evenDictionariesList = evenDictionariesList;
        }


        public bool ContainsKey(uint key)
        {
            return key < _evenDictionariesList.Count;
        }

        public bool TryGetValue(uint key, out IGaEvenDictionary<T> value)
        {
            if (key < _evenDictionariesList.Count)
            {
                value = _evenDictionariesList[(int) key] 
                        ?? GaEvenDictionaryEmpty<T>.DefaultDictionary;

                return true;
            }

            value = null;
            return false;
        }

        public bool IsEmpty()
        {
            return _evenDictionariesList.Count == 0 || 
                   _evenDictionariesList.All(d => d.IsEmpty());
        }

        public ulong GetMaxBasisBladeId()
        {
            var maxBasisBladeId = 0UL;

            for (var grade = 0; grade < _evenDictionariesList.Count; grade++)
            {
                var evenDictionary = _evenDictionariesList[grade];
                var m = evenDictionary.GetMaxBasisBladeId((uint) grade);

                if (m > maxBasisBladeId)
                    maxBasisBladeId = m;
            }

            return maxBasisBladeId;
        }

        public uint GetMaxGrade()
        {
            return (uint) (_evenDictionariesList.Count - 1);
        }

        public uint GetFirstGrade()
        {
            return 0U;
        }

        public uint GetLastGrade()
        {
            return (uint) (_evenDictionariesList.Count - 1);
        }

        public IGaEvenDictionary<T> GetFirstEvenDictionary()
        {
            return _evenDictionariesList[0];
        }

        public IGaEvenDictionary<T> GetLastEvenDictionary()
        {
            return _evenDictionariesList[^1];
        }

        public KeyValuePair<uint, IGaEvenDictionary<T>> GetFirstPair()
        {
            return new KeyValuePair<uint, IGaEvenDictionary<T>>(
                0, 
                _evenDictionariesList[0]
            );
        }

        public KeyValuePair<uint, IGaEvenDictionary<T>> GetLastPair()
        {
            return new KeyValuePair<uint, IGaEvenDictionary<T>>(
                (uint) (_evenDictionariesList.Count - 1), 
                _evenDictionariesList[^1]
            );
        }

        public IEnumerable<KeyValuePair<ulong, T>> GetKeyValuePairs(Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            for (var grade = 0U; grade < _evenDictionariesList.Count; grade++)
            {
                var evenDictionary = _evenDictionariesList[(int) grade];

                foreach (var (key, value) in evenDictionary)
                    yield return new KeyValuePair<ulong, T>(
                        gradeKeyToKeyMapping(grade, key),
                        value
                    );
            }
        }

        public IEnumerable<Tuple<ulong, T>> GetKeyValueTuples(Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            for (var grade = 0U; grade < _evenDictionariesList.Count; grade++)
            {
                var evenDictionary = _evenDictionariesList[(int) grade];

                foreach (var (key, value) in evenDictionary)
                    yield return new Tuple<ulong, T>(
                        gradeKeyToKeyMapping(grade, key),
                        value
                    );
            }
        }

        public IEnumerable<Tuple<uint, ulong, T>> GetGradeKeyValueTuples()
        {
            for (var grade = 0U; grade < _evenDictionariesList.Count; grade++)
            {
                var evenDictionary = _evenDictionariesList[(int) grade];

                foreach (var (key, value) in evenDictionary)
                    yield return new Tuple<uint, ulong, T>(grade, key, value);
            }
        }

        public IGaGradedDictionary<T> GetCopy()
        {
            var evenDictionariesList = new IGaEvenDictionary<T>[_evenDictionariesList.Count];

            for (var grade = 0U; grade < _evenDictionariesList.Count; grade++)
                evenDictionariesList[grade] = 
                    _evenDictionariesList[(int) grade];

            return new GaGradedDictionaryList<T>(evenDictionariesList);
        }

        public IGaGradedDictionary<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            var evenDictionariesList = new IGaEvenDictionary<T2>[_evenDictionariesList.Count];

            for (var grade = 0U; grade < _evenDictionariesList.Count; grade++)
                evenDictionariesList[grade] = 
                    _evenDictionariesList[(int) grade].MapValues(valueMapping);

            return new GaGradedDictionaryList<T2>(evenDictionariesList);
        }

        public IGaGradedDictionary<T2> MapValues<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            var evenDictionariesList = new IGaEvenDictionary<T2>[_evenDictionariesList.Count];

            for (var grade = 0U; grade < _evenDictionariesList.Count; grade++)
            {
                var g = grade;

                evenDictionariesList[grade] =
                    _evenDictionariesList[(int) grade]
                        .MapValues((key, value) => gradeKeyValueMapping(g, key, value));
            }

            return new GaGradedDictionaryList<T2>(evenDictionariesList);
        }

        public IGaGradedDictionary<T> FilterByGrade(Func<ulong, bool> gradeFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaEvenDictionary<T>>();

            for (var grade = 0U; grade < _evenDictionariesList.Count; grade++)
            {
                if (!gradeFilter(grade)) continue;

                var evenDictionary = _evenDictionariesList[(int) grade];

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedDictionary();
        }

        public IGaGradedDictionary<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaEvenDictionary<T>>();

            for (var grade = 0U; grade < _evenDictionariesList.Count; grade++)
            {
                var g = grade;

                var evenDictionary = _evenDictionariesList[(int) grade].FilterByValue(
                    value => gradeValueFilter(g, value)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedDictionary();
        }

        public IGaGradedDictionary<T> FilterByGradeKeyValue(Func<uint, ulong, T, bool> gradeKeyValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaEvenDictionary<T>>();

            for (var grade = 0U; grade < _evenDictionariesList.Count; grade++)
            {
                var g = grade;

                var evenDictionary = _evenDictionariesList[(int) grade].FilterByKeyValue(
                    (key, value) => gradeKeyValueFilter(g, key, value)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedDictionary();
        }

        public IGaGradedDictionary<T> FilterByValue(Func<T, bool> valueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaEvenDictionary<T>>();

            for (var grade = 0U; grade < _evenDictionariesList.Count; grade++)
            {
                var evenDictionary = _evenDictionariesList[(int) grade].FilterByValue(valueFilter);

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

            for (var grade = 0U; grade < _evenDictionariesList.Count; grade++)
            {
                var evenDictionary = _evenDictionariesList[(int) grade];

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
            return _evenDictionariesList.Select((evenDictionary, grade) => 
                    new KeyValuePair<uint, IGaEvenDictionary<T>>((uint) grade, evenDictionary)
                )
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}