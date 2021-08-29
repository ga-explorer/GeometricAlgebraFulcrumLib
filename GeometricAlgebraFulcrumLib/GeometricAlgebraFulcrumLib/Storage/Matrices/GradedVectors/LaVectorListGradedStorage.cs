using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors
{
    public sealed record LaVectorListGradedStorage<T> :
        ILaVectorGradedStorage<T>
    {
        private readonly List<ILaVectorEvenStorage<T>> _evenLists;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            return ((uint) _evenLists.Count).GetRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ILaVectorEvenStorage<T>> GetEvenStorages()
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
        public IEnumerable<T> GetScalars()
        {
            return _evenLists.SelectMany(evenList => evenList.GetScalars());
        }


        internal LaVectorListGradedStorage()
        {
            _evenLists = new List<ILaVectorEvenStorage<T>>();
        }

        internal LaVectorListGradedStorage(int capacity)
        {
            _evenLists = new List<ILaVectorEvenStorage<T>>(capacity);
        }

        internal LaVectorListGradedStorage([NotNull] IEnumerable<ILaVectorEvenStorage<T>> evenLists)
        {
            _evenLists = new List<ILaVectorEvenStorage<T>>(
                evenLists.Select(
                    list => list.IsNullOrEmpty() ? LaVectorEmptyStorage<T>.ZeroStorage : list
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorListGradedStorage<T> Clear()
        {
            _evenLists.Clear();
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorListGradedStorage<T> SetList(int index, ILaVectorEvenStorage<T> evenList)
        {
            _evenLists[index] = evenList ?? LaVectorEmptyStorage<T>.ZeroStorage;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorListGradedStorage<T> SetList(uint index, ILaVectorEvenStorage<T> evenList)
        {
            _evenLists[(int) index] = evenList ?? LaVectorEmptyStorage<T>.ZeroStorage;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorListGradedStorage<T> AppendList(ILaVectorEvenStorage<T> evenList)
        {
            _evenLists.Add(evenList ?? LaVectorEmptyStorage<T>.ZeroStorage);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorListGradedStorage<T> PrependList(ILaVectorEvenStorage<T> evenList)
        {
            _evenLists.Insert(0, evenList ?? LaVectorEmptyStorage<T>.ZeroStorage);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorListGradedStorage<T> InsertList(int index, ILaVectorEvenStorage<T> evenList)
        {
            _evenLists.Insert(index, evenList ?? LaVectorEmptyStorage<T>.ZeroStorage);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorListGradedStorage<T> InsertList(uint index, ILaVectorEvenStorage<T> evenList)
        {
            _evenLists.Insert((int) index, evenList ?? LaVectorEmptyStorage<T>.ZeroStorage);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorListGradedStorage<T> Remove(int index)
        {
            _evenLists.RemoveAt(index);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorListGradedStorage<T> Remove(uint index)
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
        public ILaVectorEvenStorage<T> GetEvenStorage(uint grade)
        {
            return grade < _evenLists.Count
                ? _evenLists[(int) grade] 
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(uint grade, ulong index)
        {
            return grade < _evenLists.Count
                ? _evenLists[(int) grade].GetScalar(index)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(GradeIndexRecord gradeKey)
        {
            var (grade, index) = gradeKey;

            return grade < _evenLists.Count
                ? _evenLists[(int) grade].GetScalar(index)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsGrade(uint grade)
        {
            return grade < _evenLists.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(uint grade, ulong index)
        {
            return grade < _evenLists.Count && 
                   _evenLists[(int) grade].ContainsIndex(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetEvenStorage(uint grade, out ILaVectorEvenStorage<T> evenList)
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
        public bool TryGetScalar(uint grade, ulong index, out T value)
        {
            if (grade < _evenLists.Count)
                return _evenLists[(int) grade].TryGetScalar(index, out value);

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

        public IEnumerable<GradeIndexRecord> GetGradeIndexRecords()
        {
            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var evenDictionary = _evenLists[(int) grade];

                foreach (var index in evenDictionary.GetIndices())
                    yield return new GradeIndexRecord(grade, index);
            }
        }

        public IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var evenDictionary = _evenLists[(int) grade];

                foreach (var (index, value) in evenDictionary.GetIndexScalarRecords())
                    yield return new GradeIndexScalarRecord<T>(grade, index, value);
            }
        }

        public ILaVectorGradedStorage<T> GetCopy()
        {
            var evenDictionariesList = new ILaVectorEvenStorage<T>[_evenLists.Count];

            for (var grade = 0U; grade < _evenLists.Count; grade++)
                evenDictionariesList[grade] = 
                    _evenLists[(int) grade];

            return new LaVectorListGradedStorage<T>(evenDictionariesList);
        }

        public ILaVectorGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            var evenDictionariesList = new ILaVectorEvenStorage<T2>[_evenLists.Count];

            for (var grade = 0U; grade < _evenLists.Count; grade++)
                evenDictionariesList[grade] = 
                    _evenLists[(int) grade].MapScalars(valueMapping);

            return new LaVectorListGradedStorage<T2>(evenDictionariesList);
        }

        public ILaVectorGradedStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            var evenDictionariesList = new ILaVectorEvenStorage<T2>[_evenLists.Count];

            for (var grade = 0U; grade < _evenLists.Count; grade++)
                evenDictionariesList[grade] = 
                    _evenLists[(int) grade].MapScalars(indexValueMapping);

            return new LaVectorListGradedStorage<T2>(evenDictionariesList);
        }

        public ILaVectorGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            var evenDictionariesList = new ILaVectorEvenStorage<T2>[_evenLists.Count];

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var g = grade;

                evenDictionariesList[grade] =
                    _evenLists[(int) grade]
                        .MapScalars((index, value) => gradeKeyValueMapping(g, index, value));
            }

            return new LaVectorListGradedStorage<T2>(evenDictionariesList);
        }

        public ILaVectorGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaVectorEvenStorage<T>>();

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                if (!gradeFilter(grade)) continue;

                var evenDictionary = 
                    _evenLists[(int) grade];

                if (evenDictionary.IsEmpty())
                    continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorGradedStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaVectorEvenStorage<T>>();

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var evenDictionary = 
                    _evenLists[(int) grade].FilterByIndex(indexFilter);

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, bool> gradeKeyFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaVectorEvenStorage<T>>();

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var g = grade;

                var evenDictionary = _evenLists[(int) grade].FilterByIndex(
                    index => gradeKeyFilter(g, index)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaVectorEvenStorage<T>>();

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var g = grade;

                var evenDictionary = _evenLists[(int) grade].FilterByScalar(
                    value => gradeValueFilter(g, value)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorGradedStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaVectorEvenStorage<T>>();

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var evenDictionary = 
                    _evenLists[(int) grade].FilterByIndexScalar(indexValueFilter);

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, T, bool> gradeKeyValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaVectorEvenStorage<T>>();

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var g = grade;

                var evenDictionary = _evenLists[(int) grade].FilterByIndexScalar(
                    (index, value) => gradeKeyValueFilter(g, index, value)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, ILaVectorEvenStorage<T>>();

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var evenDictionary = _evenLists[(int) grade].FilterByScalar(valueFilter);

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            var indexValueDictionary = new Dictionary<ulong, T>();

            for (var grade = 0U; grade < _evenLists.Count; grade++)
            {
                var evenDictionary = _evenLists[(int) grade];

                foreach (var (index, value) in evenDictionary.GetIndexScalarRecords())
                    indexValueDictionary.Add(
                        gradeKeyToEvenKeyMapping(grade, index), 
                        value
                    );
            }

            return indexValueDictionary.CreateLaVectorStorage();
        }

        public bool TryGetCompactStorage(out ILaVectorGradedStorage<T> gradedList)
        {
            if (GradesCount > 1)
            {
                var flag = false;
                for (var i = 0; i < _evenLists.Count; i++)
                {
                    if (!_evenLists[i].TryGetCompactStorage(out var evenList)) 
                        continue;

                    flag = true;
                    _evenLists[i] = evenList;
                }

                gradedList = this;
                return flag;
            }

            if (GradesCount == 0)
            {
                gradedList = LaVectorEmptyGradedStorage<T>.EmptyList;
                return true;
            }

            gradedList = _evenLists[0].GetCompactList().CreateLaVectorSingleGradeStorage(0);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeVectorStorageRecord<T>> GetGradeStorageRecords()
        {
            return _evenLists.Select(
                (evenList, grade) => new GradeVectorStorageRecord<T>((uint) grade, evenList)
            );
        }
    }
}