using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices
{
    public sealed record LaMatrixSparseGradedStorage<T> :
        ILaMatrixGradedStorage<T>
    {
        private readonly Dictionary<uint, ILaMatrixEvenStorage<T>> _gradeKeyValueDictionary;


        public int GradesCount 
            => _gradeKeyValueDictionary.Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            return _gradeKeyValueDictionary.Keys;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ILaMatrixEvenStorage<T>> GetGrids()
        {
            return _gradeKeyValueDictionary.Values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return _gradeKeyValueDictionary.Values.Sum(grid => grid.GetSparseCount());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return _gradeKeyValueDictionary.Values.SelectMany(grid => grid.GetScalars());
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


        internal LaMatrixSparseGradedStorage()
        {
            _gradeKeyValueDictionary = new Dictionary<uint, ILaMatrixEvenStorage<T>>();
        }

        internal LaMatrixSparseGradedStorage([NotNull] Dictionary<uint, ILaMatrixEvenStorage<T>> gradeKeyValueDictionary)
        {
            _gradeKeyValueDictionary = gradeKeyValueDictionary;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixSparseGradedStorage<T> Clear()
        {
            _gradeKeyValueDictionary.Clear();
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixSparseGradedStorage<T> Remove(uint grade)
        {
            _gradeKeyValueDictionary.Remove(grade);
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixSparseGradedStorage<T> SetGrid(uint grade, [NotNull] ILaMatrixEvenStorage<T> evenGrid)
        {
            if (_gradeKeyValueDictionary.ContainsKey(grade))
                _gradeKeyValueDictionary[grade] = evenGrid;
            else
                _gradeKeyValueDictionary.Add(grade, evenGrid);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixSparseGradedStorage<T> AddGrid(uint grade, [NotNull] ILaMatrixEvenStorage<T> evenGrid)
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
        public ILaMatrixEvenStorage<T> GetEvenStorage(uint grade)
        {
            return _gradeKeyValueDictionary.TryGetValue(grade, out var grid)
                ? grid 
                : LaMatrixEmptyStorage<T>.EmptyMatrix;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(uint grade, ulong index1, ulong index2)
        {
            return _gradeKeyValueDictionary.TryGetValue(grade, out var grid) &&
                   grid.TryGetScalar(index1, index2, out var value)
                   ? value
                   : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(GradeIndexPairRecord gradeKey)
        {
            var (grade, index1, index2) = gradeKey;

            return _gradeKeyValueDictionary.TryGetValue(grade, out var grid) &&
                   grid.TryGetScalar(index1, index2, out var value)
                ? value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(uint grade, IndexPairRecord index)
        {
            var (index1, index2) = index;

            return _gradeKeyValueDictionary.TryGetValue(grade, out var grid) &&
                   grid.TryGetScalar(index1, index2, out var value)
                ? value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsGrade(uint grade)
        {
            return _gradeKeyValueDictionary.ContainsKey(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(uint grade, ulong index1, ulong index2)
        {
            return _gradeKeyValueDictionary.TryGetValue(grade, out var grid) &&
                   grid.ContainsIndex(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(uint grade, IndexPairRecord index)
        {
            return _gradeKeyValueDictionary.TryGetValue(grade, out var grid) &&
                   grid.ContainsIndex(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetStorage(uint grade, out ILaMatrixEvenStorage<T> evenGrid)
        {
            return _gradeKeyValueDictionary.TryGetValue(grade, out evenGrid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(uint grade, IndexPairRecord index, out T value)
        {
            if (_gradeKeyValueDictionary.TryGetValue(grade, out var grid))
                return grid.TryGetScalar(index, out value);
            
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(uint grade, ulong index1, ulong index2, out T value)
        {
            if (_gradeKeyValueDictionary.TryGetValue(grade, out var grid))
                return grid.TryGetScalar(index1, index2, out value);
            
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
        public IEnumerable<GradeIndexPairRecord> GetGradeIndexRecords()
        {
            return _gradeKeyValueDictionary.SelectMany(pair => 
                pair.Value.GetIndices().Select(index => 
                    new GradeIndexPairRecord(pair.Key, index.Index1, index.Index2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexPairScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            return _gradeKeyValueDictionary.SelectMany(pair => 
                pair.Value.GetIndexScalarRecords().Select(indexValuePair => 
                    new GradeIndexPairScalarRecord<T>(
                        pair.Key,
                        indexValuePair.Index1,
                        indexValuePair.Index2,
                        indexValuePair.Scalar
                    )
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T> GetCopy()
        {
            return _gradeKeyValueDictionary
                .CopyToDictionary(grid => grid.GetCopy())
                .CreateLaMatrixGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return _gradeKeyValueDictionary
                .CopyToDictionary(grid => grid.MapScalars(valueMapping))
                .CreateLaMatrixGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexValueMapping)
        {
            return _gradeKeyValueDictionary
                .CopyToDictionary(grid => grid.MapScalars(indexValueMapping))
                .CreateLaMatrixGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            return _gradeKeyValueDictionary
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.MapScalars(
                        (index1, index2, value) => gradeKeyValueMapping(pair.Key, index1, index2, value))
                )
                .CreateLaMatrixGradedStorage();
        }

        public ILaMatrixGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaMatrixEvenStorage<T>>();

            foreach (var (grade, evenDictionary) in _gradeKeyValueDictionary)
            {
                if (!gradeFilter(grade) || evenDictionary.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        public ILaMatrixGradedStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaMatrixEvenStorage<T>>();

            foreach (var (grade, grid) in _gradeKeyValueDictionary)
            {
                var filteredGrid = 
                    grid.FilterByIndex(indexFilter);

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        public ILaMatrixGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, ulong, bool> gradeKeyFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaMatrixEvenStorage<T>>();

            foreach (var (grade, grid) in _gradeKeyValueDictionary)
            {
                var filteredGrid = 
                    grid.FilterByIndex((index1, index2) => gradeKeyFilter(grade, index1, index2));

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        public ILaMatrixGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaMatrixEvenStorage<T>>();

            foreach (var (grade, grid) in _gradeKeyValueDictionary)
            {
                var filteredGrid = 
                    grid.FilterByScalar(valueFilter);

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        public ILaMatrixGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaMatrixEvenStorage<T>>();

            foreach (var (grade, grid) in _gradeKeyValueDictionary)
            {
                var filteredGrid = 
                    grid.FilterByScalar(value => gradeValueFilter(grade, value));

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        public ILaMatrixGradedStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaMatrixEvenStorage<T>>();

            foreach (var (grade, grid) in _gradeKeyValueDictionary)
            {
                var filteredGrid = 
                    grid.FilterByIndexScalar(indexValueFilter);

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        public ILaMatrixGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaMatrixEvenStorage<T>>();

            foreach (var (grade, grid) in _gradeKeyValueDictionary)
            {
                var filteredGrid = 
                    grid.FilterByIndexScalar((index1, index2, value) => 
                        gradeKeyValueFilter(grade, index1, index2, value)
                    );

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        public ILaMatrixEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            var indexValueDictionary = new Dictionary<IndexPairRecord, T>();

            foreach (var (grade, evenDictionary) in _gradeKeyValueDictionary)
            {
                foreach (var (index1, index2, value) in evenDictionary.GetIndexScalarRecords())
                    indexValueDictionary.Add(
                        new IndexPairRecord(
                            gradeKeyToEvenKeyMapping(grade, index1), 
                            gradeKeyToEvenKeyMapping(grade, index2)
                        ), 
                        value
                    );
            }

            return indexValueDictionary.CreateEvenGrid();
        }

        public ILaMatrixEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong, IndexPairRecord> gradeKeyToEvenKeyMapping)
        {
            var indexValueDictionary = new Dictionary<IndexPairRecord, T>();

            foreach (var (grade, evenDictionary) in _gradeKeyValueDictionary)
            {
                foreach (var (index1, index2, value) in evenDictionary.GetIndexScalarRecords())
                    indexValueDictionary.Add(
                        gradeKeyToEvenKeyMapping(grade, index1, index2), 
                        value
                    );
            }

            return indexValueDictionary.CreateEvenGrid();
        }

        public bool TryGetCompactStorage(out ILaMatrixGradedStorage<T> gradedGrid)
        {
            if (_gradeKeyValueDictionary.Count == 0)
            {
                gradedGrid = LaMatrixEmptyGradedStorage<T>.EmptyGrid;
                return true;
            }

            if (_gradeKeyValueDictionary.Count == 1)
            {
                var (grade, evenGrid) = _gradeKeyValueDictionary.First();

                gradedGrid = evenGrid.TryGetCompactStorage(out var evenGrid1)
                    ? evenGrid1.CreateLaMatrixSingleGradeStorage(grade)
                    : evenGrid.CreateLaMatrixSingleGradeStorage(grade);

                return true;
            }

            gradedGrid = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeArrayStorageRecord<T>> GetGradeStorageRecords()
        {
            return _gradeKeyValueDictionary.Select(pair => 
                new GradeArrayStorageRecord<T>(pair.Key, pair.Value)
            );
        }
    }
}