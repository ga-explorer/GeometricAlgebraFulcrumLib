using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors
{
    public sealed record LaVectorSparseGradedStorage<T> :
        ILaVectorGradedStorage<T>
    {
        private readonly Dictionary<uint, ILaVectorEvenStorage<T>> _gradeIndexScalarDictionary;


        public int GradesCount 
            => _gradeIndexScalarDictionary.Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            return _gradeIndexScalarDictionary.Keys;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ILaVectorEvenStorage<T>> GetEvenStorages()
        {
            return _gradeIndexScalarDictionary.Values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return _gradeIndexScalarDictionary.Values.Sum(storage => storage.GetSparseCount());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return _gradeIndexScalarDictionary.Values.SelectMany(storage => storage.GetScalars());
        }


        internal LaVectorSparseGradedStorage()
        {
            _gradeIndexScalarDictionary = new Dictionary<uint, ILaVectorEvenStorage<T>>();
        }

        internal LaVectorSparseGradedStorage([NotNull] Dictionary<uint, ILaVectorEvenStorage<T>> gradeIndexScalarDictionary)
        {
            _gradeIndexScalarDictionary = gradeIndexScalarDictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorSparseGradedStorage<T> Clear()
        {
            _gradeIndexScalarDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorSparseGradedStorage<T> Remove(uint grade)
        {
            _gradeIndexScalarDictionary.Remove(grade);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorSparseGradedStorage<T> SetList(uint grade, [NotNull] ILaVectorEvenStorage<T> storage)
        {
            if (_gradeIndexScalarDictionary.ContainsKey(grade))
                _gradeIndexScalarDictionary[grade] = storage;
            else
                _gradeIndexScalarDictionary.Add(grade, storage);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorSparseGradedStorage<T> AddList(uint grade, [NotNull] ILaVectorEvenStorage<T> storage)
        {
            _gradeIndexScalarDictionary.Add(grade, storage);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return _gradeIndexScalarDictionary
                .Values
                .All(dict => dict.IsEmpty());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMinGrade()
        {
            return _gradeIndexScalarDictionary.Keys.TryGetMinValue(out var grade)
                ? grade
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMaxGrade()
        {
            return _gradeIndexScalarDictionary.Keys.TryGetMaxValue(out var grade)
                ? grade
                : throw new InvalidOperationException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> GetEvenStorage(uint grade)
        {
            return _gradeIndexScalarDictionary.TryGetValue(grade, out var list)
                ? list
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(uint grade, ulong index)
        {
            return _gradeIndexScalarDictionary.TryGetValue(grade, out var grid) &&
                   grid.TryGetScalar(index, out var scalar)
                ? scalar
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(GradeIndexRecord gradeIndex)
        {
            var (grade, index) = gradeIndex;

            return _gradeIndexScalarDictionary.TryGetValue(grade, out var grid) &&
                   grid.TryGetScalar(index, out var scalar)
                ? scalar
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsGrade(uint grade)
        {
            return _gradeIndexScalarDictionary.ContainsKey(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(uint grade, ulong index)
        {
            return _gradeIndexScalarDictionary.TryGetValue(grade, out var grid) &&
                   grid.ContainsIndex(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetEvenStorage(uint grade, out ILaVectorEvenStorage<T> storage)
        {
            return _gradeIndexScalarDictionary.TryGetValue(grade, out storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(uint grade, ulong index, out T scalar)
        {
            if (_gradeIndexScalarDictionary.TryGetValue(grade, out var grid))
                return grid.TryGetScalar(index, out scalar);
            
            scalar = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexRecord> GetGradeIndexRecords()
        {
            return _gradeIndexScalarDictionary.SelectMany(pair => 
                pair.Value.GetIndices().Select(index => 
                    new GradeIndexRecord(pair.Key, index)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeVectorStorageRecord<T>> GetGradeStorageRecords()
        {
            return _gradeIndexScalarDictionary.Select(pair => 
                new GradeVectorStorageRecord<T>(pair.Key, pair.Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            return _gradeIndexScalarDictionary.SelectMany(
                pair => pair.Value.GetIndexScalarRecords().Select(indexValuePair => 
                    new GradeIndexScalarRecord<T>(
                        pair.Key,
                        indexValuePair.Index,
                        indexValuePair.Scalar
                    )
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T> GetCopy()
        {
            return _gradeIndexScalarDictionary
                .CopyToDictionary(list => list.GetCopy())
                .CreateLaVectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T2> MapScalars<T2>(Func<T, T2> scalarMapping)
        {
            return _gradeIndexScalarDictionary
                .CopyToDictionary(list => list.MapScalars(scalarMapping))
                .CreateLaVectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            return _gradeIndexScalarDictionary
                .CopyToDictionary(list => list.MapScalars(indexScalarMapping))
                .CreateLaVectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return _gradeIndexScalarDictionary
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.MapScalars(
                        (index, scalar) => gradeIndexScalarMapping(pair.Key, index, scalar))
                    )
                .CreateLaVectorGradedStorage();
        }

        public ILaVectorGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaVectorEvenStorage<T>>();

            foreach (var (grade, evenDictionary) in _gradeIndexScalarDictionary)
            {
                if (!gradeFilter(grade) || evenDictionary.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorGradedStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaVectorEvenStorage<T>>();

            foreach (var (grade, grid) in _gradeIndexScalarDictionary)
            {
                var filteredGrid = 
                    grid.FilterByIndex(indexFilter);

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, bool> gradeIndexFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaVectorEvenStorage<T>>();

            foreach (var (grade, grid) in _gradeIndexScalarDictionary)
            {
                var filteredGrid = 
                    grid.FilterByIndex(index => gradeIndexFilter(grade, index));

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeScalarFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaVectorEvenStorage<T>>();

            foreach (var (grade, evenDict) in _gradeIndexScalarDictionary)
            {
                var evenDictionary = evenDict.FilterByScalar(
                    scalar => gradeScalarFilter(grade, scalar)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorGradedStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexScalarFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaVectorEvenStorage<T>>();

            foreach (var (grade, grid) in _gradeIndexScalarDictionary)
            {
                var filteredGrid = 
                    grid.FilterByIndexScalar(indexScalarFilter);

                if (filteredGrid.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, filteredGrid);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, T, bool> gradeIndexScalarFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaVectorEvenStorage<T>>();

            foreach (var (grade, evenDict) in _gradeIndexScalarDictionary)
            {
                var evenDictionary = evenDict.FilterByIndexScalar(
                    (index, scalar) => gradeIndexScalarFilter(grade, index, scalar)
                );

                if (evenDictionary.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorGradedStorage<T> FilterByScalar(Func<T, bool> scalarFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaVectorEvenStorage<T>>();

            foreach (var (grade, evenDict) in _gradeIndexScalarDictionary)
            {
                var evenDictionary = evenDict.FilterByScalar(scalarFilter);

                if (evenDictionary.IsEmpty()) 
                    continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong> gradeIndexToEvenIndexMapping)
        {
            var indexValueDictionary = new Dictionary<ulong, T>();

            foreach (var (grade, evenDictionary) in _gradeIndexScalarDictionary)
            {
                foreach (var (index, scalar) in evenDictionary.GetIndexScalarRecords())
                    indexValueDictionary.Add(
                        gradeIndexToEvenIndexMapping(grade, index), 
                        scalar
                    );
            }

            return indexValueDictionary.CreateLaVectorStorage();
        }

        public bool TryGetCompactStorage(out ILaVectorGradedStorage<T> gradedStorage)
        {
            if (_gradeIndexScalarDictionary.Count == 0)
            {
                gradedStorage = LaVectorEmptyGradedStorage<T>.EmptyList;
                return true;
            }

            if (_gradeIndexScalarDictionary.Count == 1)
            {
                var (grade, storage) = _gradeIndexScalarDictionary.First();

                gradedStorage = storage.TryGetCompactStorage(out var evenList1)
                    ? evenList1.CreateLaVectorSingleGradeStorage(grade)
                    : storage.CreateLaVectorSingleGradeStorage(grade);

                return true;
            }

            gradedStorage = this;
            return false;
        }
    }
}