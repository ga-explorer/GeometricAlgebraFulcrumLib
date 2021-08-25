using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Graded
{
    public sealed record GaGridGradedSparse<T> :
        IGaGridGraded<T>
    {
        private readonly Dictionary<uint, IGaGridEven<T>> _gradeKeyValueDictionary;


        public int GradesCount 
            => _gradeKeyValueDictionary.Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            return _gradeKeyValueDictionary.Keys;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IGaGridEven<T>> GetGrids()
        {
            return _gradeKeyValueDictionary.Values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return _gradeKeyValueDictionary.Values.Sum(grid => grid.GetSparseCount());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            return _gradeKeyValueDictionary.Values.SelectMany(grid => grid.GetValues());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return _gradeKeyValueDictionary.Values.Sum(grid => grid.GetSparseCount1());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return _gradeKeyValueDictionary.Values.Sum(grid => grid.GetSparseCount2());
        }


        internal GaGridGradedSparse()
        {
            _gradeKeyValueDictionary = new Dictionary<uint, IGaGridEven<T>>();
        }

        internal GaGridGradedSparse([NotNull] Dictionary<uint, IGaGridEven<T>> gradeKeyValueDictionary)
        {
            _gradeKeyValueDictionary = gradeKeyValueDictionary;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedSparse<T> Clear()
        {
            _gradeKeyValueDictionary.Clear();
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedSparse<T> Remove(uint grade)
        {
            _gradeKeyValueDictionary.Remove(grade);
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedSparse<T> SetGrid(uint grade, [NotNull] IGaGridEven<T> evenGrid)
        {
            if (_gradeKeyValueDictionary.ContainsKey(grade))
                _gradeKeyValueDictionary[grade] = evenGrid;
            else
                _gradeKeyValueDictionary.Add(grade, evenGrid);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedSparse<T> AddGrid(uint grade, [NotNull] IGaGridEven<T> evenGrid)
        {
            _gradeKeyValueDictionary.Add(grade, evenGrid);

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
        public IGaGridEven<T> GetGrid(uint grade)
        {
            return _gradeKeyValueDictionary.TryGetValue(grade, out var grid)
                ? grid 
                : GaGridEvenEmpty<T>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(uint grade, ulong key1, ulong key2)
        {
            return _gradeKeyValueDictionary.TryGetValue(grade, out var grid) &&
                   grid.TryGetValue(key1, key2, out var value)
                   ? value
                   : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(GaRecordGradeKeyPair gradeKey)
        {
            var (grade, key1, key2) = gradeKey;

            return _gradeKeyValueDictionary.TryGetValue(grade, out var grid) &&
                   grid.TryGetValue(key1, key2, out var value)
                ? value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(uint grade, GaRecordKeyPair key)
        {
            var (key1, key2) = key;

            return _gradeKeyValueDictionary.TryGetValue(grade, out var grid) &&
                   grid.TryGetValue(key1, key2, out var value)
                ? value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsGrade(uint grade)
        {
            return _gradeKeyValueDictionary.ContainsKey(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(uint grade, ulong key1, ulong key2)
        {
            return _gradeKeyValueDictionary.TryGetValue(grade, out var grid) &&
                   grid.ContainsKey(key1, key2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(uint grade, GaRecordKeyPair key)
        {
            return _gradeKeyValueDictionary.TryGetValue(grade, out var grid) &&
                   grid.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetGrid(uint grade, out IGaGridEven<T> evenGrid)
        {
            return _gradeKeyValueDictionary.TryGetValue(grade, out evenGrid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(uint grade, GaRecordKeyPair key, out T value)
        {
            if (_gradeKeyValueDictionary.TryGetValue(grade, out var grid))
                return grid.TryGetValue(key, out value);
            
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(uint grade, ulong key1, ulong key2, out T value)
        {
            if (_gradeKeyValueDictionary.TryGetValue(grade, out var grid))
                return grid.TryGetValue(key1, key2, out value);
            
            value = default;
            return false;
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
        public IEnumerable<GaRecordGradeKeyPair> GetGradeKeyRecords()
        {
            return _gradeKeyValueDictionary.SelectMany(pair => 
                pair.Value.GetKeys().Select(key => 
                    new GaRecordGradeKeyPair(pair.Key, key.Key1, key.Key2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordGradeKeyPairValue<T>> GetGradeKeyValueRecords()
        {
            return _gradeKeyValueDictionary.SelectMany(pair => 
                pair.Value.GetKeyValueRecords().Select(keyValuePair => 
                    new GaRecordGradeKeyPairValue<T>(
                        pair.Key,
                        keyValuePair.Key1,
                        keyValuePair.Key2,
                        keyValuePair.Value
                    )
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T> GetCopy()
        {
            return _gradeKeyValueDictionary
                .CopyToDictionary(grid => grid.GetCopy())
                .CreateGradedGrid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return _gradeKeyValueDictionary
                .CopyToDictionary(grid => grid.MapValues(valueMapping))
                .CreateGradedGrid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T2> MapValues<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            return _gradeKeyValueDictionary
                .CopyToDictionary(grid => grid.MapValues(keyValueMapping))
                .CreateGradedGrid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T2> MapValues<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            return _gradeKeyValueDictionary
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.MapValues(
                        (key1, key2, value) => gradeKeyValueMapping(pair.Key, key1, key2, value))
                )
                .CreateGradedGrid();
        }

        public IGaGridGraded<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaGridEven<T>>();

            foreach (var (grade, evenDictionary) in _gradeKeyValueDictionary)
            {
                if (!gradeFilter(grade) || evenDictionary.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedGrid();
        }

        public IGaGridGraded<T> FilterByKey(Func<ulong, ulong, bool> keyFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaGridEven<T>>();

            foreach (var (grade, grid) in _gradeKeyValueDictionary)
            {
                var filteredGrid = 
                    grid.FilterByKey(keyFilter);

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateGradedGrid();
        }

        public IGaGridGraded<T> FilterByGradeKey(Func<uint, ulong, ulong, bool> gradeKeyFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaGridEven<T>>();

            foreach (var (grade, grid) in _gradeKeyValueDictionary)
            {
                var filteredGrid = 
                    grid.FilterByKey((key1, key2) => gradeKeyFilter(grade, key1, key2));

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateGradedGrid();
        }

        public IGaGridGraded<T> FilterByValue(Func<T, bool> valueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaGridEven<T>>();

            foreach (var (grade, grid) in _gradeKeyValueDictionary)
            {
                var filteredGrid = 
                    grid.FilterByValue(valueFilter);

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateGradedGrid();
        }

        public IGaGridGraded<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaGridEven<T>>();

            foreach (var (grade, grid) in _gradeKeyValueDictionary)
            {
                var filteredGrid = 
                    grid.FilterByValue(value => gradeValueFilter(grade, value));

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateGradedGrid();
        }

        public IGaGridGraded<T> FilterByKeyValue(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaGridEven<T>>();

            foreach (var (grade, grid) in _gradeKeyValueDictionary)
            {
                var filteredGrid = 
                    grid.FilterByKeyValue(keyValueFilter);

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateGradedGrid();
        }

        public IGaGridGraded<T> FilterByGradeKeyValue(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaGridEven<T>>();

            foreach (var (grade, grid) in _gradeKeyValueDictionary)
            {
                var filteredGrid = 
                    grid.FilterByKeyValue((key1, key2, value) => 
                        gradeKeyValueFilter(grade, key1, key2, value)
                    );

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateGradedGrid();
        }

        public IGaGridEven<T> ToEvenGrid(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            var keyValueDictionary = new Dictionary<GaRecordKeyPair, T>();

            foreach (var (grade, evenDictionary) in _gradeKeyValueDictionary)
            {
                foreach (var (key1, key2, value) in evenDictionary.GetKeyValueRecords())
                    keyValueDictionary.Add(
                        new GaRecordKeyPair(
                            gradeKeyToEvenKeyMapping(grade, key1), 
                            gradeKeyToEvenKeyMapping(grade, key2)
                        ), 
                        value
                    );
            }

            return keyValueDictionary.CreateEvenGrid();
        }

        public IGaGridEven<T> ToEvenGrid(Func<uint, ulong, ulong, GaRecordKeyPair> gradeKeyToEvenKeyMapping)
        {
            var keyValueDictionary = new Dictionary<GaRecordKeyPair, T>();

            foreach (var (grade, evenDictionary) in _gradeKeyValueDictionary)
            {
                foreach (var (key1, key2, value) in evenDictionary.GetKeyValueRecords())
                    keyValueDictionary.Add(
                        gradeKeyToEvenKeyMapping(grade, key1, key2), 
                        value
                    );
            }

            return keyValueDictionary.CreateEvenGrid();
        }

        public bool TryGetCompactGrid(out IGaGridGraded<T> gradedGrid)
        {
            if (_gradeKeyValueDictionary.Count == 0)
            {
                gradedGrid = GaGridGradedEmpty<T>.EmptyGrid;
                return true;
            }

            if (_gradeKeyValueDictionary.Count == 1)
            {
                var (grade, evenGrid) = _gradeKeyValueDictionary.First();

                gradedGrid = evenGrid.TryGetCompactGrid(out var evenGrid1)
                    ? evenGrid1.CreateGradedGridSingleGrade(grade)
                    : evenGrid.CreateGradedGridSingleGrade(grade);

                return true;
            }

            gradedGrid = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordGradeEvenGrid<T>> GetGradeGridRecords()
        {
            return _gradeKeyValueDictionary.Select(pair => 
                new GaRecordGradeEvenGrid<T>(pair.Key, pair.Value)
            );
        }
    }
}