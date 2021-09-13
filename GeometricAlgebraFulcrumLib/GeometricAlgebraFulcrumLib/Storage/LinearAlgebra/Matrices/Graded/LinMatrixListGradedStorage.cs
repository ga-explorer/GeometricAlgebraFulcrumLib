using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded
{
    public sealed record LinMatrixListGradedStorage<T> :
        ILinMatrixGradedStorage<T>
    {
        private readonly List<ILinMatrixStorage<T>> _matrixStorageList;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            return ((uint) _matrixStorageList.Count).GetRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetEmptyGrades(uint vSpaceDimension)
        {
            var countDiff = (1 + vSpaceDimension - _matrixStorageList.Count);

            return countDiff > 0
                ? ((uint) countDiff).GetRange((uint) _matrixStorageList.Count)
                : Enumerable.Empty<uint>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ILinMatrixStorage<T>> GetMatrixStorages()
        {
            return _matrixStorageList;
        }

        public int GradesCount 
            => _matrixStorageList.Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return _matrixStorageList.Sum(matrixStorage => matrixStorage.GetSparseCount());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return _matrixStorageList.SelectMany(vectorStorage => vectorStorage.GetScalars());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return _matrixStorageList.Sum(matrixStorage => matrixStorage.GetSparseCount1());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return _matrixStorageList.Sum(matrixStorage => matrixStorage.GetSparseCount2());
        }


        internal LinMatrixListGradedStorage()
        {
            _matrixStorageList = new List<ILinMatrixStorage<T>>();
        }

        internal LinMatrixListGradedStorage(int capacity)
        {
            _matrixStorageList = new List<ILinMatrixStorage<T>>(capacity);
        }

        internal LinMatrixListGradedStorage([NotNull] IEnumerable<ILinMatrixStorage<T>> matrixStorages)
        {
            _matrixStorageList = new List<ILinMatrixStorage<T>>(
                matrixStorages.Select(
                    list => list.IsNullOrEmpty() ? LinMatrixEmptyStorage<T>.EmptyStorage : list
                )
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixListGradedStorage<T> Clear()
        {
            _matrixStorageList.Clear();
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixListGradedStorage<T> SetMatrixStorage(int index, ILinMatrixStorage<T> matrixStorage)
        {
            _matrixStorageList[index] = matrixStorage ?? LinMatrixEmptyStorage<T>.EmptyStorage;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixListGradedStorage<T> SetMatrixStorage(uint index, ILinMatrixStorage<T> matrixStorage)
        {
            _matrixStorageList[(int) index] = matrixStorage ?? LinMatrixEmptyStorage<T>.EmptyStorage;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixListGradedStorage<T> AppendMatrixStorage(ILinMatrixStorage<T> matrixStorage)
        {
            _matrixStorageList.Add(matrixStorage ?? LinMatrixEmptyStorage<T>.EmptyStorage);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixListGradedStorage<T> PrependMatrixStorage(ILinMatrixStorage<T> matrixStorage)
        {
            _matrixStorageList.Insert(0, matrixStorage ?? LinMatrixEmptyStorage<T>.EmptyStorage);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixListGradedStorage<T> InsertMatrixStorage(int index, ILinMatrixStorage<T> matrixStorage)
        {
            _matrixStorageList.Insert(index, matrixStorage ?? LinMatrixEmptyStorage<T>.EmptyStorage);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixListGradedStorage<T> InsertMatrixStorage(uint index, ILinMatrixStorage<T> matrixStorage)
        {
            _matrixStorageList.Insert((int) index, matrixStorage ?? LinMatrixEmptyStorage<T>.EmptyStorage);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixListGradedStorage<T> Remove(int index)
        {
            _matrixStorageList.RemoveAt(index);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixListGradedStorage<T> Remove(uint index)
        {
            _matrixStorageList.RemoveAt((int) index);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return _matrixStorageList.Count == 0 || 
                   _matrixStorageList.All(d => d.IsNullOrEmpty());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetMatrixStorage(uint grade)
        {
            return grade < _matrixStorageList.Count
                ? _matrixStorageList[(int) grade] 
                : LinMatrixEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(uint grade, ulong index1, ulong index2)
        {
            return grade < _matrixStorageList.Count
                ? _matrixStorageList[(int) grade].GetScalar(index1, index2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(GradeIndexPairRecord gradeKey)
        {
            var (grade, index1, index2) = gradeKey;

            return grade < _matrixStorageList.Count
                ? _matrixStorageList[(int) grade].GetScalar(index1, index2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(uint grade, IndexPairRecord index)
        {
            return grade < _matrixStorageList.Count
                ? _matrixStorageList[(int) grade].GetScalar(index)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsGrade(uint grade)
        {
            return grade < _matrixStorageList.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(uint grade, ulong index1, ulong index2)
        {
            return grade < _matrixStorageList.Count && 
                   _matrixStorageList[(int) grade].ContainsIndex(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(uint grade, IndexPairRecord index)
        {
            return grade < _matrixStorageList.Count && 
                   _matrixStorageList[(int) grade].ContainsIndex(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetMatrixStorage(uint grade, out ILinMatrixStorage<T> matrixStorage)
        {
            if (grade < _matrixStorageList.Count)
            {
                matrixStorage = _matrixStorageList[(int) grade];
                return true;
            }

            matrixStorage = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(uint grade, IndexPairRecord index, out T value)
        {
            if (grade < _matrixStorageList.Count)
                return _matrixStorageList[(int) grade].TryGetScalar(index, out value);

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(uint grade, ulong index1, ulong index2, out T value)
        {
            if (grade < _matrixStorageList.Count)
                return _matrixStorageList[(int) grade].TryGetScalar(index1, index2, out value);

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
            return (uint) (_matrixStorageList.Count - 1);
        }

        public IEnumerable<GradeIndexPairRecord> GetGradeIndexRecords()
        {
            for (var grade = 0U; grade < _matrixStorageList.Count; grade++)
            {
                var matrixStorage = _matrixStorageList[(int) grade];

                foreach (var (index1, index2) in matrixStorage.GetIndices())
                    yield return new GradeIndexPairRecord(grade, index1, index2);
            }
        }

        public IEnumerable<GradeIndexPairScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            for (var grade = 0U; grade < _matrixStorageList.Count; grade++)
            {
                var matrixStorage = _matrixStorageList[(int) grade];

                foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
                    yield return new GradeIndexPairScalarRecord<T>(grade, index1, index2, value);
            }
        }

        public ILinMatrixGradedStorage<T> GetCopy()
        {
            var matrixStorage = new ILinMatrixStorage<T>[_matrixStorageList.Count];

            for (var grade = 0U; grade < _matrixStorageList.Count; grade++)
                matrixStorage[grade] = 
                    _matrixStorageList[(int) grade];

            return new LinMatrixListGradedStorage<T>(matrixStorage);
        }

        public ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            var matrixStorage = new ILinMatrixStorage<T2>[_matrixStorageList.Count];

            for (var grade = 0U; grade < _matrixStorageList.Count; grade++)
            {
                matrixStorage[grade] =
                    _matrixStorageList[(int) grade].MapScalars(valueMapping);
            }

            return new LinMatrixListGradedStorage<T2>(matrixStorage);
        }

        public ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexValueMapping)
        {
            var matrixStorage = new ILinMatrixStorage<T2>[_matrixStorageList.Count];

            for (var grade = 0U; grade < _matrixStorageList.Count; grade++)
            {
                matrixStorage[grade] =
                    _matrixStorageList[(int) grade].MapScalars(indexValueMapping);
            }

            return new LinMatrixListGradedStorage<T2>(matrixStorage);
        }

        public ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            var matrixStorage = new ILinMatrixStorage<T2>[_matrixStorageList.Count];

            for (var grade = 0U; grade < _matrixStorageList.Count; grade++)
            {
                var g = grade;

                matrixStorage[grade] =
                    _matrixStorageList[(int) grade]
                        .MapScalars((index1, index2, value) => 
                            gradeKeyValueMapping(g, index1, index2, value)
                        );
            }

            return new LinMatrixListGradedStorage<T2>(matrixStorage);
        }

        public ILinMatrixGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();

            for (var grade = 0U; grade < _matrixStorageList.Count; grade++)
            {
                if (gradeFilter(grade))
                    continue;

                var matrixStorage = 
                    _matrixStorageList[(int) grade];

                if (matrixStorage.IsEmpty())
                    continue;

                gradeIndexScalarDictionary.Add(grade, matrixStorage);
            }

            return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
        }

        public ILinMatrixGradedStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();

            for (var grade = 0U; grade < _matrixStorageList.Count; grade++)
            {
                var matrixStorage = 
                    _matrixStorageList[(int) grade].FilterByIndex(indexFilter);

                if (matrixStorage.IsEmpty()) continue;

                gradeIndexScalarDictionary.Add(grade, matrixStorage);
            }

            return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
        }

        public ILinMatrixGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, ulong, bool> gradeKeyFilter)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();

            for (var grade = 0U; grade < _matrixStorageList.Count; grade++)
            {
                var g = grade;

                var matrixStorage = _matrixStorageList[(int) grade].FilterByIndex(
                    (index1, index2) => 
                        gradeKeyFilter(g, index1, index2)
                );

                if (matrixStorage.IsEmpty()) continue;

                gradeIndexScalarDictionary.Add(grade, matrixStorage);
            }

            return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
        }

        public ILinMatrixGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();

            for (var grade = 0U; grade < _matrixStorageList.Count; grade++)
            {
                var matrixStorage = 
                    _matrixStorageList[(int) grade].FilterByScalar(valueFilter);

                if (matrixStorage.IsEmpty()) continue;

                gradeIndexScalarDictionary.Add(grade, matrixStorage);
            }

            return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
        }

        public ILinMatrixGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();

            for (var grade = 0U; grade < _matrixStorageList.Count; grade++)
            {
                var g = grade;

                var matrixStorage = _matrixStorageList[(int) grade].FilterByScalar(
                    value => gradeValueFilter(g, value)
                );

                if (matrixStorage.IsEmpty()) continue;

                gradeIndexScalarDictionary.Add(grade, matrixStorage);
            }

            return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
        }

        public ILinMatrixGradedStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexValueFilter)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();

            for (var grade = 0U; grade < _matrixStorageList.Count; grade++)
            {
                var matrixStorage = 
                    _matrixStorageList[(int) grade].FilterByIndexScalar(indexValueFilter);

                if (matrixStorage.IsEmpty()) continue;

                gradeIndexScalarDictionary.Add(grade, matrixStorage);
            }

            return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
        }

        public ILinMatrixGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();

            for (var grade = 0U; grade < _matrixStorageList.Count; grade++)
            {
                var g = grade;

                var matrixStorage = _matrixStorageList[(int) grade].FilterByIndexScalar(
                    (index1, index2, value) => 
                        gradeKeyValueFilter(g, index1, index2, value)
                );

                if (matrixStorage.IsEmpty()) continue;

                gradeIndexScalarDictionary.Add(grade, matrixStorage);
            }

            return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
        }

        public ILinMatrixStorage<T> ToMatrixStorage(Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            var indexValueDictionary = new Dictionary<IndexPairRecord, T>();

            for (var grade = 0U; grade < _matrixStorageList.Count; grade++)
            {
                var matrixStorage = _matrixStorageList[(int) grade];

                foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
                    indexValueDictionary.Add(
                        new IndexPairRecord(
                            gradeIndexToIndexMapping(grade, index1), 
                            gradeIndexToIndexMapping(grade, index2)
                        ),
                        value
                    );
            }

            return indexValueDictionary.CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> ToMatrixStorage(Func<uint, ulong, ulong, IndexPairRecord> gradeIndexToIndexMapping)
        {
            var indexValueDictionary = new Dictionary<IndexPairRecord, T>();

            for (var grade = 0U; grade < _matrixStorageList.Count; grade++)
            {
                var matrixStorage = _matrixStorageList[(int) grade];

                foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
                    indexValueDictionary.Add(
                        gradeIndexToIndexMapping(grade, index1, index2), 
                        value
                    );
            }

            return indexValueDictionary.CreateLinMatrixStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexLinVectorStorageRecord<T>> GetRows(uint grade)
        {
            return TryGetMatrixStorage(grade, out var matrixStorage) 
                ? matrixStorage.GetRows() 
                : Enumerable.Empty<IndexLinVectorStorageRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexLinVectorStorageRecord<T>> GetRows()
        {
            for (var grade = 0U; grade < _matrixStorageList.Count; grade++)
            {
                var matrixStorage = _matrixStorageList[(int) grade];

                foreach (var (index, vectorStorage) in matrixStorage.GetRows())
                    yield return new GradeIndexLinVectorStorageRecord<T>(grade, index, vectorStorage);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexLinVectorStorageRecord<T>> GetColumns(uint grade)
        {
            return TryGetMatrixStorage(grade, out var matrixStorage) 
                ? matrixStorage.GetColumns() 
                : Enumerable.Empty<IndexLinVectorStorageRecord<T>>();
        }

        public IEnumerable<GradeIndexLinVectorStorageRecord<T>> GetColumns()
        {
            for (var grade = 0U; grade < _matrixStorageList.Count; grade++)
            {
                var matrixStorage = _matrixStorageList[(int) grade];

                foreach (var (index, vectorStorage) in matrixStorage.GetColumns())
                    yield return new GradeIndexLinVectorStorageRecord<T>(grade, index, vectorStorage);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> GetTranspose()
        {
            return new LinMatrixListGradedStorage<T>(
                _matrixStorageList.Select(m => m.GetTranspose())
            );
        }

        public bool TryGetCompactStorage(out ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            if (GradesCount > 1)
            {
                var flag = false;
                for (var i = 0; i < _matrixStorageList.Count; i++)
                {
                    if (!_matrixStorageList[i].TryGetCompactStorage(out var matrixStorage)) 
                        continue;

                    flag = true;
                    _matrixStorageList[i] = matrixStorage;
                }

                matrixGradedStorage = this;
                return flag;
            }

            if (GradesCount == 0)
            {
                matrixGradedStorage = LinMatrixEmptyGradedStorage<T>.EmptyStorage;
                return true;
            }

            matrixGradedStorage = _matrixStorageList[0].GetCompactMatrixStorage().CreateLinMatrixSingleGradeStorage(0);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeLinMatrixStorageRecord<T>> GetGradeStorageRecords()
        {
            return _matrixStorageList.Select(
                (matrixStorage, grade) => new GradeLinMatrixStorageRecord<T>((uint) grade, matrixStorage)
            );
        }
    }
}