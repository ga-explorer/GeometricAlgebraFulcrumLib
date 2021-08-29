using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public sealed class LaVectorSparseStorage<T> :
        ILaVectorSparseEvenStorage<T>
    {
        private readonly Dictionary<ulong, T> _indexValueDictionary;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return _indexValueDictionary.Count;
        }

        public T this[int index]
        {
            get => _indexValueDictionary[(ulong) index];
            set
            {
                if (_indexValueDictionary.ContainsKey((ulong) index))
                    _indexValueDictionary[(ulong) index] = value;
                else
                    _indexValueDictionary.Add((ulong) index, value);
            }
        }

        public T this[ulong index]
        {
            get => _indexValueDictionary[index];
            set
            {
                if (_indexValueDictionary.ContainsKey(index))
                    _indexValueDictionary[index] = value;
                else
                    _indexValueDictionary.Add(index, value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(ulong index)
        {
            return _indexValueDictionary.TryGetValue(index, out var value)
                ? value
                : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices()
        {
            return _indexValueDictionary.Keys;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return _indexValueDictionary.Values;
        }


        internal LaVectorSparseStorage()
        {
            _indexValueDictionary = new Dictionary<ulong, T>();
        }

        internal LaVectorSparseStorage([NotNull] Dictionary<ulong, T> valueDictionary)
        {
            _indexValueDictionary = valueDictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorSparseStorage<T> Clear()
        {
            _indexValueDictionary.Clear();
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorSparseStorage<T> SetValue(ulong index, [NotNull] T value)
        {
            if (_indexValueDictionary.ContainsKey(index))
                _indexValueDictionary[index] = value;
            else
                _indexValueDictionary.Add(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorSparseStorage<T> AddValue(ulong index, [NotNull] T value)
        {
            _indexValueDictionary.Add(index, value);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorSparseStorage<T> Remove(ulong index)
        {
            _indexValueDictionary.Remove(index);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorSparseStorage<T> Remove(params ulong[] indexsList)
        {
            foreach (var index in indexsList)
                _indexValueDictionary.Remove(index);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorSparseStorage<T> Remove(IEnumerable<ulong> indexsList)
        {
            foreach (var index in indexsList.ToArray())
                _indexValueDictionary.Remove(index);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index)
        {
            return _indexValueDictionary.ContainsKey(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(ulong index, out T value)
        {
            return _indexValueDictionary.TryGetValue(index, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return _indexValueDictionary.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex()
        {
            if (_indexValueDictionary.Count == 0)
                throw new InvalidOperationException();

            return _indexValueDictionary.Keys.Min();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex()
        {
            if (_indexValueDictionary.Count == 0)
                throw new InvalidOperationException();

            return _indexValueDictionary.Keys.Max();
        }

        public IEnumerable<ulong> GetEmptyIndices(ulong maxKey)
        {
            var indexsList = maxKey.GetRange().ToList();

            foreach (var index in _indexValueDictionary.Keys) 
                indexsList.Remove(index);

            return indexsList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> GetCopy()
        {
            return _indexValueDictionary
                .CopyToDictionary()
                .CreateLaVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> MapIndices(Func<ulong, ulong> indexMapping)
        {
            return _indexValueDictionary
                .ToDictionary(
                    pair => indexMapping(pair.Key),
                    pair => pair.Value
                )
                .CreateLaVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return _indexValueDictionary
                .CopyToDictionary(valueMapping)
                .CreateLaVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            return _indexValueDictionary.ToDictionary(
                pair => pair.Key,
                pair => indexValueMapping(pair.Key, pair.Value)
            ).CreateLaVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            return _indexValueDictionary
                .Where(pair => indexFilter(pair.Key))
                .CopyToDictionary()
                .CreateLaVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            return _indexValueDictionary
                .Where(pair => indexValueFilter(pair.Key, pair.Value))
                .CopyToDictionary()
                .CreateLaVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return _indexValueDictionary
                .Where(pair => valueFilter(pair.Value))
                .CopyToDictionary()
                .CreateLaVectorStorage();
        }

        public ILaVectorGradedStorage<T> ToGradedStorage(Func<ulong, GradeIndexRecord> evenKeyToGradeKeyMapping)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (id, value) in _indexValueDictionary)
            {
                var (grade, index) = evenKeyToGradeKeyMapping(id);

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var indexValueDictionary))
                {
                    indexValueDictionary = new Dictionary<ulong, T>();
                    gradeKeyValueDictionary.Add(grade, indexValueDictionary);
                }

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = value;
                else
                    indexValueDictionary.Add(index, value);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorGradedStorage<T> ToGradedStorage(Func<ulong, T, GradeIndexScalarRecord<T>> evenIndexScalarToGradeIndexScalarMapping)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (id, value) in _indexValueDictionary)
            {
                var (grade, index, scalar) = evenIndexScalarToGradeIndexScalarMapping(id, value);

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var indexValueDictionary))
                {
                    indexValueDictionary = new Dictionary<ulong, T>();
                    gradeKeyValueDictionary.Add(grade, indexValueDictionary);
                }

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = scalar;
                else
                    indexValueDictionary.Add(index, scalar);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public bool TryGetCompactStorage(out ILaVectorEvenStorage<T> evenList)
        {
            if (_indexValueDictionary.Count == 0)
            {
                evenList = LaVectorEmptyStorage<T>.ZeroStorage;
                return true;
            }

            if (_indexValueDictionary.Count == 1)
            {
                var (index, value) = _indexValueDictionary.First();

                evenList = index == 0UL
                    ? new LaVectorZeroIndexStorage<T>(value)
                    : new LaVectorSingleIndexStorage<T>(index, value);

                return true;
            }

            evenList = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords()
        {
            return _indexValueDictionary.Select(
                pair => new IndexScalarRecord<T>(pair.Key, pair.Value)
            );
        }
    }
}