using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded
{
    public sealed record LinVectorSparseGradedStorage<T> :
        ILinVectorGradedStorage<T>
    {
        private readonly Dictionary<uint, ILinVectorStorage<T>> _gradeIndexScalarDictionary;


        public int GradesCount 
            => _gradeIndexScalarDictionary.Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            return _gradeIndexScalarDictionary.Keys;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetEmptyGrades(uint vSpaceDimension)
        {
            return (1U + vSpaceDimension).GetRange().Except(_gradeIndexScalarDictionary.Keys);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ILinVectorStorage<T>> GetVectorStorages()
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


        internal LinVectorSparseGradedStorage()
        {
            _gradeIndexScalarDictionary = new Dictionary<uint, ILinVectorStorage<T>>();
        }

        internal LinVectorSparseGradedStorage([NotNull] Dictionary<uint, ILinVectorStorage<T>> gradeIndexScalarDictionary)
        {
            _gradeIndexScalarDictionary = gradeIndexScalarDictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorSparseGradedStorage<T> Clear()
        {
            _gradeIndexScalarDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorSparseGradedStorage<T> Remove(uint grade)
        {
            _gradeIndexScalarDictionary.Remove(grade);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorSparseGradedStorage<T> SetList(uint grade, [NotNull] ILinVectorStorage<T> storage)
        {
            if (_gradeIndexScalarDictionary.ContainsKey(grade))
                _gradeIndexScalarDictionary[grade] = storage;
            else
                _gradeIndexScalarDictionary.Add(grade, storage);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorSparseGradedStorage<T> AddList(uint grade, [NotNull] ILinVectorStorage<T> storage)
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
        public ILinVectorStorage<T> GetVectorStorage(uint grade)
        {
            return _gradeIndexScalarDictionary.TryGetValue(grade, out var list)
                ? list
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(uint grade, ulong index)
        {
            return _gradeIndexScalarDictionary.TryGetValue(grade, out var matrixStorage) &&
                   matrixStorage.TryGetScalar(index, out var scalar)
                ? scalar
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(GradeIndexRecord gradeIndex)
        {
            var (grade, index) = gradeIndex;

            return _gradeIndexScalarDictionary.TryGetValue(grade, out var matrixStorage) &&
                   matrixStorage.TryGetScalar(index, out var scalar)
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
            return _gradeIndexScalarDictionary.TryGetValue(grade, out var matrixStorage) &&
                   matrixStorage.ContainsIndex(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetVectorStorage(uint grade, out ILinVectorStorage<T> storage)
        {
            return _gradeIndexScalarDictionary.TryGetValue(grade, out storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(uint grade, ulong index, out T scalar)
        {
            if (_gradeIndexScalarDictionary.TryGetValue(grade, out var matrixStorage))
                return matrixStorage.TryGetScalar(index, out scalar);
            
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
        public IEnumerable<GradeLinVectorStorageRecord<T>> GetGradeStorageRecords()
        {
            return _gradeIndexScalarDictionary.Select(pair => 
                new GradeLinVectorStorageRecord<T>(pair.Key, pair.Value)
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
        public ILinVectorGradedStorage<T> GetCopy()
        {
            return _gradeIndexScalarDictionary
                .ToDictionary(list => list.GetCopy())
                .CreateLinVectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T2> MapScalars<T2>(Func<T, T2> scalarMapping)
        {
            return _gradeIndexScalarDictionary
                .ToDictionary(list => list.MapScalars(scalarMapping))
                .CreateLinVectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            return _gradeIndexScalarDictionary
                .ToDictionary(list => list.MapScalars(indexScalarMapping))
                .CreateLinVectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return _gradeIndexScalarDictionary
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.MapScalars(
                        (index, scalar) => gradeIndexScalarMapping(pair.Key, index, scalar))
                    )
                .CreateLinVectorGradedStorage();
        }

        public ILinVectorGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, ILinVectorStorage<T>>();

            foreach (var (grade, evenDictionary) in _gradeIndexScalarDictionary)
            {
                if (!gradeFilter(grade) || evenDictionary.IsEmpty()) 
                    continue;

                gradeIndexScalarDictionary.Add(grade, evenDictionary);
            }

            return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
        }

        public ILinVectorGradedStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, ILinVectorStorage<T>>();

            foreach (var (grade, matrixStorage) in _gradeIndexScalarDictionary)
            {
                var filteredMatrixStorage = 
                    matrixStorage.FilterByIndex(indexFilter);

                if (filteredMatrixStorage.IsEmpty()) 
                    continue;

                gradeIndexScalarDictionary.Add(grade, filteredMatrixStorage);
            }

            return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
        }

        public ILinVectorGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, bool> gradeIndexFilter)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, ILinVectorStorage<T>>();

            foreach (var (grade, matrixStorage) in _gradeIndexScalarDictionary)
            {
                var filteredMatrixStorage = 
                    matrixStorage.FilterByIndex(index => gradeIndexFilter(grade, index));

                if (filteredMatrixStorage.IsEmpty()) 
                    continue;

                gradeIndexScalarDictionary.Add(grade, filteredMatrixStorage);
            }

            return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
        }

        public ILinVectorGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeScalarFilter)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, ILinVectorStorage<T>>();

            foreach (var (grade, evenDict) in _gradeIndexScalarDictionary)
            {
                var evenDictionary = evenDict.FilterByScalar(
                    scalar => gradeScalarFilter(grade, scalar)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeIndexScalarDictionary.Add(grade, evenDictionary);
            }

            return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
        }

        public ILinVectorGradedStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexScalarFilter)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, ILinVectorStorage<T>>();

            foreach (var (grade, matrixStorage) in _gradeIndexScalarDictionary)
            {
                var filteredMatrixStorage = 
                    matrixStorage.FilterByIndexScalar(indexScalarFilter);

                if (filteredMatrixStorage.IsEmpty()) 
                    continue;

                gradeIndexScalarDictionary.Add(grade, filteredMatrixStorage);
            }

            return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
        }

        public ILinVectorGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, T, bool> gradeIndexScalarFilter)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, ILinVectorStorage<T>>();

            foreach (var (grade, evenDict) in _gradeIndexScalarDictionary)
            {
                var evenDictionary = evenDict.FilterByIndexScalar(
                    (index, scalar) => gradeIndexScalarFilter(grade, index, scalar)
                );

                if (evenDictionary.IsEmpty()) 
                    continue;

                gradeIndexScalarDictionary.Add(grade, evenDictionary);
            }

            return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
        }

        public ILinVectorGradedStorage<T> FilterByScalar(Func<T, bool> scalarFilter)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, ILinVectorStorage<T>>();

            foreach (var (grade, evenDict) in _gradeIndexScalarDictionary)
            {
                var evenDictionary = evenDict.FilterByScalar(scalarFilter);

                if (evenDictionary.IsEmpty()) 
                    continue;

                gradeIndexScalarDictionary.Add(grade, evenDictionary);
            }

            return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
        }

        public ILinVectorStorage<T> ToVectorStorage(Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            var indexValueDictionary = new Dictionary<ulong, T>();

            foreach (var (grade, evenDictionary) in _gradeIndexScalarDictionary)
            {
                foreach (var (index, scalar) in evenDictionary.GetIndexScalarRecords())
                    indexValueDictionary.Add(
                        gradeIndexToIndexMapping(grade, index), 
                        scalar
                    );
            }

            return indexValueDictionary.CreateLinVectorStorage();
        }

        public bool TryGetCompactStorage(out ILinVectorGradedStorage<T> gradedStorage)
        {
            if (_gradeIndexScalarDictionary.Count == 0)
            {
                gradedStorage = LinVectorEmptyGradedStorage<T>.EmptyStorage;
                return true;
            }

            if (_gradeIndexScalarDictionary.Count == 1)
            {
                var (grade, storage) = _gradeIndexScalarDictionary.First();

                gradedStorage = storage.TryGetCompactStorage(out var evenList1)
                    ? evenList1.CreateLinVectorSingleGradeStorage(grade)
                    : storage.CreateLinVectorSingleGradeStorage(grade);

                return true;
            }

            gradedStorage = this;
            return false;
        }
    }
}