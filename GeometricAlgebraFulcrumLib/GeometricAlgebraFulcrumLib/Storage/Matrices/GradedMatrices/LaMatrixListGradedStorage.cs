using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices
{
    public sealed record LaMatrixListGradedStorage<T> :
        ILaMatrixGradedStorage<T>
    {
        private readonly List<ILaMatrixEvenStorage<T>> _evenGrids;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            return ((uint) _evenGrids.Count).GetRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ILaMatrixEvenStorage<T>> GetGrids()
        {
            return _evenGrids;
        }

        public int GradesCount 
            => _evenGrids.Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return _evenGrids.Sum(evenGrid => evenGrid.GetSparseCount());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return _evenGrids.SelectMany(evenList => evenList.GetScalars());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return _evenGrids.Sum(evenGrid => evenGrid.GetSparseCount1());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return _evenGrids.Sum(evenGrid => evenGrid.GetSparseCount2());
        }


        internal LaMatrixListGradedStorage()
        {
            _evenGrids = new List<ILaMatrixEvenStorage<T>>();
        }

        internal LaMatrixListGradedStorage(int capacity)
        {
            _evenGrids = new List<ILaMatrixEvenStorage<T>>(capacity);
        }

        internal LaMatrixListGradedStorage([NotNull] IEnumerable<ILaMatrixEvenStorage<T>> evenGrids)
        {
            _evenGrids = new List<ILaMatrixEvenStorage<T>>(
                evenGrids.Select(
                    list => list.IsNullOrEmpty() ? LaMatrixEmptyStorage<T>.EmptyMatrix : list
                )
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixListGradedStorage<T> Clear()
        {
            _evenGrids.Clear();
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixListGradedStorage<T> SetGrid(int index, ILaMatrixEvenStorage<T> evenGrid)
        {
            _evenGrids[index] = evenGrid ?? LaMatrixEmptyStorage<T>.EmptyMatrix;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixListGradedStorage<T> SetGrid(uint index, ILaMatrixEvenStorage<T> evenGrid)
        {
            _evenGrids[(int) index] = evenGrid ?? LaMatrixEmptyStorage<T>.EmptyMatrix;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixListGradedStorage<T> AppendGrid(ILaMatrixEvenStorage<T> evenGrid)
        {
            _evenGrids.Add(evenGrid ?? LaMatrixEmptyStorage<T>.EmptyMatrix);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixListGradedStorage<T> PrependGrid(ILaMatrixEvenStorage<T> evenGrid)
        {
            _evenGrids.Insert(0, evenGrid ?? LaMatrixEmptyStorage<T>.EmptyMatrix);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixListGradedStorage<T> InsertGrid(int index, ILaMatrixEvenStorage<T> evenGrid)
        {
            _evenGrids.Insert(index, evenGrid ?? LaMatrixEmptyStorage<T>.EmptyMatrix);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixListGradedStorage<T> InsertGrid(uint index, ILaMatrixEvenStorage<T> evenGrid)
        {
            _evenGrids.Insert((int) index, evenGrid ?? LaMatrixEmptyStorage<T>.EmptyMatrix);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixListGradedStorage<T> Remove(int index)
        {
            _evenGrids.RemoveAt(index);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixListGradedStorage<T> Remove(uint index)
        {
            _evenGrids.RemoveAt((int) index);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return _evenGrids.Count == 0 || 
                   _evenGrids.All(d => d.IsNullOrEmpty());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> GetEvenStorage(uint grade)
        {
            return grade < _evenGrids.Count
                ? _evenGrids[(int) grade] 
                : LaMatrixEmptyStorage<T>.EmptyMatrix;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(uint grade, ulong index1, ulong index2)
        {
            return grade < _evenGrids.Count
                ? _evenGrids[(int) grade].GetScalar(index1, index2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(GradeIndexPairRecord gradeKey)
        {
            var (grade, index1, index2) = gradeKey;

            return grade < _evenGrids.Count
                ? _evenGrids[(int) grade].GetScalar(index1, index2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(uint grade, IndexPairRecord index)
        {
            return grade < _evenGrids.Count
                ? _evenGrids[(int) grade].GetScalar(index)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsGrade(uint grade)
        {
            return grade < _evenGrids.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(uint grade, ulong index1, ulong index2)
        {
            return grade < _evenGrids.Count && 
                   _evenGrids[(int) grade].ContainsIndex(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(uint grade, IndexPairRecord index)
        {
            return grade < _evenGrids.Count && 
                   _evenGrids[(int) grade].ContainsIndex(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetStorage(uint grade, out ILaMatrixEvenStorage<T> evenGrid)
        {
            if (grade < _evenGrids.Count)
            {
                evenGrid = _evenGrids[(int) grade];
                return true;
            }

            evenGrid = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(uint grade, IndexPairRecord index, out T value)
        {
            if (grade < _evenGrids.Count)
                return _evenGrids[(int) grade].TryGetScalar(index, out value);

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(uint grade, ulong index1, ulong index2, out T value)
        {
            if (grade < _evenGrids.Count)
                return _evenGrids[(int) grade].TryGetScalar(index1, index2, out value);

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
            return (uint) (_evenGrids.Count - 1);
        }

        public IEnumerable<GradeIndexPairRecord> GetGradeIndexRecords()
        {
            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var evenDictionary = _evenGrids[(int) grade];

                foreach (var (index1, index2) in evenDictionary.GetIndices())
                    yield return new GradeIndexPairRecord(grade, index1, index2);
            }
        }

        public IEnumerable<GradeIndexPairScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var evenDictionary = _evenGrids[(int) grade];

                foreach (var (index1, index2, value) in evenDictionary.GetIndexScalarRecords())
                    yield return new GradeIndexPairScalarRecord<T>(grade, index1, index2, value);
            }
        }

        public ILaMatrixGradedStorage<T> GetCopy()
        {
            var evenDictionariesGrid = new ILaMatrixEvenStorage<T>[_evenGrids.Count];

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
                evenDictionariesGrid[grade] = 
                    _evenGrids[(int) grade];

            return new LaMatrixListGradedStorage<T>(evenDictionariesGrid);
        }

        public ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            var evenDictionariesGrid = new ILaMatrixEvenStorage<T2>[_evenGrids.Count];

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                evenDictionariesGrid[grade] =
                    _evenGrids[(int) grade].MapScalars(valueMapping);
            }

            return new LaMatrixListGradedStorage<T2>(evenDictionariesGrid);
        }

        public ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexValueMapping)
        {
            var evenDictionariesGrid = new ILaMatrixEvenStorage<T2>[_evenGrids.Count];

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                evenDictionariesGrid[grade] =
                    _evenGrids[(int) grade].MapScalars(indexValueMapping);
            }

            return new LaMatrixListGradedStorage<T2>(evenDictionariesGrid);
        }

        public ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            var evenDictionariesGrid = new ILaMatrixEvenStorage<T2>[_evenGrids.Count];

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var g = grade;

                evenDictionariesGrid[grade] =
                    _evenGrids[(int) grade]
                        .MapScalars((index1, index2, value) => 
                            gradeKeyValueMapping(g, index1, index2, value)
                        );
            }

            return new LaMatrixListGradedStorage<T2>(evenDictionariesGrid);
        }

        public ILaMatrixGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaMatrixEvenStorage<T>>();

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                if (gradeFilter(grade))
                    continue;

                var evenDictionary = 
                    _evenGrids[(int) grade];

                if (evenDictionary.IsEmpty())
                    continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        public ILaMatrixGradedStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaMatrixEvenStorage<T>>();

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var evenDictionary = 
                    _evenGrids[(int) grade].FilterByIndex(indexFilter);

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        public ILaMatrixGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, ulong, bool> gradeKeyFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaMatrixEvenStorage<T>>();

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var g = grade;

                var evenDictionary = _evenGrids[(int) grade].FilterByIndex(
                    (index1, index2) => 
                        gradeKeyFilter(g, index1, index2)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        public ILaMatrixGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaMatrixEvenStorage<T>>();

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var evenDictionary = 
                    _evenGrids[(int) grade].FilterByScalar(valueFilter);

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        public ILaMatrixGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaMatrixEvenStorage<T>>();

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var g = grade;

                var evenDictionary = _evenGrids[(int) grade].FilterByScalar(
                    value => gradeValueFilter(g, value)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        public ILaMatrixGradedStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaMatrixEvenStorage<T>>();

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var evenDictionary = 
                    _evenGrids[(int) grade].FilterByIndexScalar(indexValueFilter);

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        public ILaMatrixGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaMatrixEvenStorage<T>>();

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var g = grade;

                var evenDictionary = _evenGrids[(int) grade].FilterByIndexScalar(
                    (index1, index2, value) => 
                        gradeKeyValueFilter(g, index1, index2, value)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        public ILaMatrixEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            var indexValueDictionary = new Dictionary<IndexPairRecord, T>();

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var evenDictionary = _evenGrids[(int) grade];

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

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var evenDictionary = _evenGrids[(int) grade];

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
            if (GradesCount > 1)
            {
                var flag = false;
                for (var i = 0; i < _evenGrids.Count; i++)
                {
                    if (!_evenGrids[i].TryGetCompactStorage(out var evenGrid)) 
                        continue;

                    flag = true;
                    _evenGrids[i] = evenGrid;
                }

                gradedGrid = this;
                return flag;
            }

            if (GradesCount == 0)
            {
                gradedGrid = LaMatrixEmptyGradedStorage<T>.EmptyGrid;
                return true;
            }

            gradedGrid = _evenGrids[0].GetCompactGrid().CreateLaMatrixSingleGradeStorage(0);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeArrayStorageRecord<T>> GetGradeStorageRecords()
        {
            return _evenGrids.Select(
                (evenGrid, grade) => new GradeArrayStorageRecord<T>((uint) grade, evenGrid)
            );
        }
    }
}