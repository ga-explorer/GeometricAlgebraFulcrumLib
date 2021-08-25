using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Graded
{
    public sealed record GaListGradedSparse<T> :
        IGaListGraded<T>
    {
        private readonly Dictionary<uint, IGaListEven<T>> _gradeKeyValueDictionary;


        public int GradesCount 
            => _gradeKeyValueDictionary.Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            return _gradeKeyValueDictionary.Keys;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IGaListEven<T>> GetLists()
        {
            return _gradeKeyValueDictionary.Values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return _gradeKeyValueDictionary.Values.Sum(evenList => evenList.GetSparseCount());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            return _gradeKeyValueDictionary.Values.SelectMany(evenList => evenList.GetValues());
        }


        internal GaListGradedSparse()
        {
            _gradeKeyValueDictionary = new Dictionary<uint, IGaListEven<T>>();
        }

        internal GaListGradedSparse([NotNull] Dictionary<uint, IGaListEven<T>> gradeKeyValueDictionary)
        {
            _gradeKeyValueDictionary = gradeKeyValueDictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedSparse<T> Clear()
        {
            _gradeKeyValueDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedSparse<T> Remove(uint grade)
        {
            _gradeKeyValueDictionary.Remove(grade);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedSparse<T> SetList(uint grade, [NotNull] IGaListEven<T> evenList)
        {
            if (_gradeKeyValueDictionary.ContainsKey(grade))
                _gradeKeyValueDictionary[grade] = evenList;
            else
                _gradeKeyValueDictionary.Add(grade, evenList);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedSparse<T> AddList(uint grade, [NotNull] IGaListEven<T> evenList)
        {
            _gradeKeyValueDictionary.Add(grade, evenList);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return _gradeKeyValueDictionary
                .Values
                .All(dict => dict.IsEmpty());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMinGrade()
        {
            return _gradeKeyValueDictionary.Keys.TryGetMinValue(out var grade)
                ? grade
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMaxGrade()
        {
            return _gradeKeyValueDictionary.Keys.TryGetMaxValue(out var grade)
                ? grade
                : throw new InvalidOperationException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> GetList(uint grade)
        {
            return _gradeKeyValueDictionary.TryGetValue(grade, out var list)
                ? list
                : GaListEvenEmpty<T>.EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(uint grade, ulong key)
        {
            return _gradeKeyValueDictionary.TryGetValue(grade, out var grid) &&
                   grid.TryGetValue(key, out var value)
                ? value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(GaRecordGradeKey gradeKey)
        {
            var (grade, key) = gradeKey;

            return _gradeKeyValueDictionary.TryGetValue(grade, out var grid) &&
                   grid.TryGetValue(key, out var value)
                ? value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsGrade(uint grade)
        {
            return _gradeKeyValueDictionary.ContainsKey(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(uint grade, ulong key)
        {
            return _gradeKeyValueDictionary.TryGetValue(grade, out var grid) &&
                   grid.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetList(uint grade, out IGaListEven<T> evenList)
        {
            return _gradeKeyValueDictionary.TryGetValue(grade, out evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(uint grade, ulong key, out T value)
        {
            if (_gradeKeyValueDictionary.TryGetValue(grade, out var grid))
                return grid.TryGetValue(key, out value);
            
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordGradeKey> GetGradeKeyRecords()
        {
            return _gradeKeyValueDictionary.SelectMany(pair => 
                pair.Value.GetKeys().Select(key => 
                    new GaRecordGradeKey(pair.Key, key)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordGradeKeyValue<T>> GetGradeKeyValueRecords()
        {
            return _gradeKeyValueDictionary.SelectMany(
                pair => pair.Value.GetKeyValueRecords().Select(keyValuePair => 
                    new GaRecordGradeKeyValue<T>(
                        pair.Key,
                        keyValuePair.Key,
                        keyValuePair.Value
                    )
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListGraded<T> GetCopy()
        {
            return _gradeKeyValueDictionary
                .CopyToDictionary(list => list.GetCopy())
                .CreateGradedList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListGraded<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return _gradeKeyValueDictionary
                .CopyToDictionary(list => list.MapValues(valueMapping))
                .CreateGradedList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListGraded<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            return _gradeKeyValueDictionary
                .CopyToDictionary(list => list.MapValues(keyValueMapping))
                .CreateGradedList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListGraded<T2> MapValues<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            return _gradeKeyValueDictionary
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.MapValues(
                        (key, value) => gradeKeyValueMapping(pair.Key, key, value))
                    )
                .CreateGradedList();
        }

        public IGaListGraded<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaListEven<T>>();

            foreach (var (grade, evenDictionary) in _gradeKeyValueDictionary)
            {
                if (!gradeFilter(grade) || evenDictionary.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        public IGaListGraded<T> FilterByKey(Func<ulong, bool> keyFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaListEven<T>>();

            foreach (var (grade, grid) in _gradeKeyValueDictionary)
            {
                var filteredGrid = 
                    grid.FilterByKey(keyFilter);

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        public IGaListGraded<T> FilterByGradeKey(Func<uint, ulong, bool> gradeKeyFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaListEven<T>>();

            foreach (var (grade, grid) in _gradeKeyValueDictionary)
            {
                var filteredGrid = 
                    grid.FilterByKey(key => gradeKeyFilter(grade, key));

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        public IGaListGraded<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaListEven<T>>();

            foreach (var (grade, evenDict) in _gradeKeyValueDictionary)
            {
                var evenDictionary = evenDict.FilterByValue(
                    value => gradeValueFilter(grade, value)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        public IGaListGraded<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaListEven<T>>();

            foreach (var (grade, grid) in _gradeKeyValueDictionary)
            {
                var filteredGrid = 
                    grid.FilterByKeyValue(keyValueFilter);

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        public IGaListGraded<T> FilterByGradeKeyValue(Func<uint, ulong, T, bool> gradeKeyValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaListEven<T>>();

            foreach (var (grade, evenDict) in _gradeKeyValueDictionary)
            {
                var evenDictionary = evenDict.FilterByKeyValue(
                    (key, value) => gradeKeyValueFilter(grade, key, value)
                );

                if (evenDictionary.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        public IGaListGraded<T> FilterByValue(Func<T, bool> valueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaListEven<T>>();

            foreach (var (grade, evenDict) in _gradeKeyValueDictionary)
            {
                var evenDictionary = evenDict.FilterByValue(valueFilter);

                if (evenDictionary.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        public IGaListEven<T> ToEvenList(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            var keyValueDictionary = new Dictionary<ulong, T>();

            foreach (var (grade, evenDictionary) in _gradeKeyValueDictionary)
            {
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
            if (_gradeKeyValueDictionary.Count == 0)
            {
                gradedList = GaListGradedEmpty<T>.EmptyList;
                return true;
            }

            if (_gradeKeyValueDictionary.Count == 1)
            {
                var (grade, evenList) = _gradeKeyValueDictionary.First();

                gradedList = evenList.TryGetCompactList(out var evenList1)
                    ? evenList1.CreateGradedListSingleGrade(grade)
                    : evenList.CreateGradedListSingleGrade(grade);

                return true;
            }

            gradedList = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordGradeEvenList<T>> GetGradeListRecords()
        {
            return _gradeKeyValueDictionary.Select(pair => 
                new GaRecordGradeEvenList<T>(pair.Key, pair.Value)
            );
        }
    }
}