using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse
{
    public sealed class LinVectorSparseStorage<T> :
        ILinVectorSparseStorage<T>
    {
        private readonly Dictionary<ulong, T> _indexScalarDictionary;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return _indexScalarDictionary.Count;
        }

        public T this[int index]
        {
            get => _indexScalarDictionary[(ulong) index];
            set
            {
                if (_indexScalarDictionary.ContainsKey((ulong) index))
                    _indexScalarDictionary[(ulong) index] = value;
                else
                    _indexScalarDictionary.Add((ulong) index, value);
            }
        }

        public T this[ulong index]
        {
            get => _indexScalarDictionary[index];
            set
            {
                if (_indexScalarDictionary.ContainsKey(index))
                    _indexScalarDictionary[index] = value;
                else
                    _indexScalarDictionary.Add(index, value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(ulong index)
        {
            return _indexScalarDictionary.TryGetValue(index, out var value)
                ? value
                : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices()
        {
            return _indexScalarDictionary.Keys;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return _indexScalarDictionary.Values;
        }


        internal LinVectorSparseStorage()
        {
            _indexScalarDictionary = new Dictionary<ulong, T>();
        }

        internal LinVectorSparseStorage(Dictionary<ulong, T> valueDictionary)
        {
            _indexScalarDictionary = valueDictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorSparseStorage<T> Clear()
        {
            _indexScalarDictionary.Clear();
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorSparseStorage<T> SetValue(ulong index, T value)
        {
            if (_indexScalarDictionary.ContainsKey(index))
                _indexScalarDictionary[index] = value;
            else
                _indexScalarDictionary.Add(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorSparseStorage<T> AddValue(ulong index, T value)
        {
            _indexScalarDictionary.Add(index, value);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorSparseStorage<T> Remove(ulong index)
        {
            _indexScalarDictionary.Remove(index);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorSparseStorage<T> Remove(params ulong[] indexsList)
        {
            foreach (var index in indexsList)
                _indexScalarDictionary.Remove(index);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorSparseStorage<T> Remove(IEnumerable<ulong> indexsList)
        {
            foreach (var index in indexsList.ToArray())
                _indexScalarDictionary.Remove(index);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index)
        {
            return _indexScalarDictionary.ContainsKey(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(ulong index, out T value)
        {
            return _indexScalarDictionary.TryGetValue(index, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return _indexScalarDictionary.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex()
        {
            if (_indexScalarDictionary.Count == 0)
                throw new InvalidOperationException();

            return _indexScalarDictionary.Keys.Min();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex()
        {
            if (_indexScalarDictionary.Count == 0)
                throw new InvalidOperationException();

            return _indexScalarDictionary.Keys.Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetEmptyIndices(ulong maxCount)
        {
            return maxCount.GetRange().Except(_indexScalarDictionary.Keys);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetCopy()
        {
            return _indexScalarDictionary
                .ToDictionary()
                .CreateLinVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetPermutation(Func<ulong, ulong> indexMapping)
        {
            return _indexScalarDictionary
                .ToDictionary(
                    pair => indexMapping(pair.Key),
                    pair => pair.Value
                )
                .CreateLinVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return _indexScalarDictionary
                .ToDictionary(valueMapping)
                .CreateLinVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            return _indexScalarDictionary.ToDictionary(
                pair => pair.Key,
                pair => indexValueMapping(pair.Key, pair.Value)
            ).CreateLinVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            return _indexScalarDictionary
                .Where(pair => indexFilter(pair.Key))
                .ToDictionary()
                .CreateLinVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            return _indexScalarDictionary
                .Where(pair => indexValueFilter(pair.Key, pair.Value))
                .ToDictionary()
                .CreateLinVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return _indexScalarDictionary
                .Where(pair => valueFilter(pair.Value))
                .ToDictionary()
                .CreateLinVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> ToVectorGradedStorage(
            Func<ulong, RGaGradeKvIndexRecord> indexToGradeIndexMapping)
        {
            return GetIndexScalarRecords()
                .Select(record => record.MapRecord(indexToGradeIndexMapping))
                .CreateLinVectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> ToVectorGradedStorage(Func<ulong, T, RGaGradeKvIndexScalarRecord<T>> indexScalarToGradeIndexScalarMapping)
        {
            return GetIndexScalarRecords()
                .Select(record => record.MapRecord(indexScalarToGradeIndexScalarMapping))
                .CreateLinVectorGradedStorage();
        }

        public bool TryGetCompactStorage(out ILinVectorStorage<T> vectorStorage)
        {
            if (_indexScalarDictionary.Count == 0)
            {
                vectorStorage = LinVectorEmptyStorage<T>.EmptyStorage;
                return true;
            }

            if (_indexScalarDictionary.Count == 1)
            {
                var (index, value) = _indexScalarDictionary.First();

                vectorStorage = index == 0UL
                    ? new LinVectorSingleScalarDenseStorage<T>(value)
                    : new LinVectorSingleScalarSparseStorage<T>(index, value);

                return true;
            }

            vectorStorage = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexScalarRecord<T>> GetIndexScalarRecords()
        {
            return _indexScalarDictionary.Select(
                pair => new RGaKvIndexScalarRecord<T>(pair.Key, pair.Value)
            );
        }
    }
}