using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Graded
{
    public sealed record GaListGradedDenseList<T> :
        IGaListGraded<T>
    {
        private readonly List<IGaListEven<T>> _evenLists;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            return ((uint) _evenLists.Count).GetRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IGaListEven<T>> GetLists()
        {
            return _evenLists;
        }

        public int GradesCount 
            => _evenLists.Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return _evenLists.Sum(evenList => evenList.GetSparseCount());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            return _evenLists.SelectMany(evenList => evenList.GetValues());
        }


        internal GaListGradedDenseList()
        {
            _evenLists = new List<IGaListEven<T>>();
        }

        internal GaListGradedDenseList(int capacity)
        {
            _evenLists = new List<IGaListEven<T>>(capacity);
        }

        internal GaListGradedDenseList([NotNull] IEnumerable<IGaListEven<T>> evenLists)
        {
            _evenLists = new List<IGaListEven<T>>(
                evenLists.Select(
                    list => list.IsNullOrEmpty() ? GaListEvenEmpty<T>.EmptyList : list
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedDenseList<T> Clear()
        {
            _evenLists.Clear();
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedDenseList<T> SetList(int index, IGaListEven<T> evenList)
        {
            _evenLists[index] = evenList ?? GaListEvenEmpty<T>.EmptyList;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedDenseList<T> SetList(uint index, IGaListEven<T> evenList)
        {
            _evenLists[(int) index] = evenList ?? GaListEvenEmpty<T>.EmptyList;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedDenseList<T> AppendList(IGaListEven<T> evenList)
        {
            _evenLists.Add(evenList ?? GaListEvenEmpty<T>.EmptyList);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedDenseList<T> PrependList(IGaListEven<T> evenList)
        {
            _evenLists.Insert(0, evenList ?? GaListEvenEmpty<T>.EmptyList);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedDenseList<T> InsertList(int index, IGaListEven<T> evenList)
        {
            _evenLists.Insert(index, evenList ?? GaListEvenEmpty<T>.EmptyList);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedDenseList<T> InsertList(uint index, IGaListEven<T> evenList)
        {
            _evenLists.Insert((int) index, evenList ?? GaListEvenEmpty<T>.EmptyList);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedDenseList<T> Remove(int index)
        {
            _evenLists.RemoveAt(index);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedDenseList<T> Remove(uint index)
        {
            _evenLists.RemoveAt((int) index);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return _evenLists.Count == 0 || 
                   _evenLists.All(d => d.IsNullOrEmpty());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> GetList(uint grade)
        {
            return grade < _evenLists.Count
                ? _evenLists[(int) grade] 
                : GaListEvenEmpty<T>.EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(uint grade, ulong key)
        {
            return grade < _evenLists.Count
                ? _evenLists[(int) grade].GetValue(key)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(GaRecordGradeKey gradeKey)
        {
            var (grade, key) = gradeKey;

            return grade < _evenLists.Count
                ? _evenLists[(int) grade].GetValue(key)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsGrade(uint grade)
        {
            return grade < _evenLists.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(uint grade, ulong key)
        {
            return grade < _evenLists.Count && 
                   _evenLists[(int) grade].ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetList(uint grade, out IGaListEven<T> evenList)
        {
            if (grade < _evenLists.Count)
            {
                evenList = _evenLists[(int) grade];
                return true;
            }

            evenList = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(uint grade, ulong key, out T value)
        {
            if (grade < _evenLists.Count)
                return _evenLists[(int) grade].TryGetValue(key, out value);

            value = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMinGrade()
        {
            return 0U;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMaxGrade()
        {
            return (uint) (_evenLists.Count - 1);
        }

        public IEnumerable<GaRecordGradeKey> GetGradeKeyRecords()
        {
            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var evenDictionary = _evenLists[(int) grade];

                foreach (var key in evenDictionary.GetKeys())
                    yield return new GaRecordGradeKey(grade, key);
            }
        }

        public IEnumerable<GaRecordGradeKeyValue<T>> GetGradeKeyValueRecords()
        {
            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var evenDictionary = _evenLists[(int) grade];

                foreach (var (key, value) in evenDictionary.GetKeyValueRecords())
                    yield return new GaRecordGradeKeyValue<T>(grade, key, value);
            }
        }

        public IGaListGraded<T> GetCopy()
        {
            var evenDictionariesList = new IGaListEven<T>[_evenLists.Count];

            for (var grade = 0U; grade < _evenLists.Count; grade++)
                evenDictionariesList[grade] = 
                    _evenLists[(int) grade];

            return new GaListGradedDenseList<T>(evenDictionariesList);
        }

        public IGaListGraded<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            var evenDictionariesList = new IGaListEven<T2>[_evenLists.Count];

            for (var grade = 0U; grade < _evenLists.Count; grade++)
                evenDictionariesList[grade] = 
                    _evenLists[(int) grade].MapValues(valueMapping);

            return new GaListGradedDenseList<T2>(evenDictionariesList);
        }

        public IGaListGraded<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            var evenDictionariesList = new IGaListEven<T2>[_evenLists.Count];

            for (var grade = 0U; grade < _evenLists.Count; grade++)
                evenDictionariesList[grade] = 
                    _evenLists[(int) grade].MapValues(keyValueMapping);

            return new GaListGradedDenseList<T2>(evenDictionariesList);
        }

        public IGaListGraded<T2> MapValues<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            var evenDictionariesList = new IGaListEven<T2>[_evenLists.Count];

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var g = grade;

                evenDictionariesList[grade] =
                    _evenLists[(int) grade]
                        .MapValues((key, value) => gradeKeyValueMapping(g, key, value));
            }

            return new GaListGradedDenseList<T2>(evenDictionariesList);
        }

        public IGaListGraded<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaListEven<T>>();

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                if (!gradeFilter(grade)) continue;

                var evenDictionary = 
                    _evenLists[(int) grade];

                if (evenDictionary.IsEmpty())
                    continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        public IGaListGraded<T> FilterByKey(Func<ulong, bool> keyFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaListEven<T>>();

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var evenDictionary = 
                    _evenLists[(int) grade].FilterByKey(keyFilter);

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        public IGaListGraded<T> FilterByGradeKey(Func<uint, ulong, bool> gradeKeyFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaListEven<T>>();

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var g = grade;

                var evenDictionary = _evenLists[(int) grade].FilterByKey(
                    key => gradeKeyFilter(g, key)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        public IGaListGraded<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaListEven<T>>();

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var g = grade;

                var evenDictionary = _evenLists[(int) grade].FilterByValue(
                    value => gradeValueFilter(g, value)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        public IGaListGraded<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaListEven<T>>();

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var evenDictionary = 
                    _evenLists[(int) grade].FilterByKeyValue(keyValueFilter);

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        public IGaListGraded<T> FilterByGradeKeyValue(Func<uint, ulong, T, bool> gradeKeyValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaListEven<T>>();

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var g = grade;

                var evenDictionary = _evenLists[(int) grade].FilterByKeyValue(
                    (key, value) => gradeKeyValueFilter(g, key, value)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        public IGaListGraded<T> FilterByValue(Func<T, bool> valueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaListEven<T>>();

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var evenDictionary = _evenLists[(int) grade].FilterByValue(valueFilter);

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        public IGaListEven<T> ToEvenList(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            var keyValueDictionary = new Dictionary<ulong, T>();

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var evenDictionary = _evenLists[(int) grade];

                foreach (var (key, value) in evenDictionary.GetKeyValueRecords())
                    keyValueDictionary.Add(
                        gradeKeyToEvenKeyMapping(grade, key), 
                        value
                    );
            }

            return keyValueDictionary.CreateEvenList();
        }

        public bool TryGetCompactList(out IGaListGraded<T> gradedList)
        {
            if (GradesCount > 1)
            {
                var flag = false;
                for (var i = 0; i < _evenLists.Count; i++)
                {
                    if (!_evenLists[i].TryGetCompactList(out var evenList)) 
                        continue;

                    flag = true;
                    _evenLists[i] = evenList;
                }

                gradedList = this;
                return flag;
            }

            if (GradesCount == 0)
            {
                gradedList = GaListGradedEmpty<T>.EmptyList;
                return true;
            }

            gradedList = _evenLists[0].GetCompactList().CreateGradedListSingleGrade(0);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordGradeEvenList<T>> GetGradeListRecords()
        {
            return _evenLists.Select(
                (evenList, grade) => new GaRecordGradeEvenList<T>((uint) grade, evenList)
            );
        }
    }
}