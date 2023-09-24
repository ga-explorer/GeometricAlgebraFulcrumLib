using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Sparse
{
    public sealed record LinMatrixSparseStorage<T> :
        ILinMatrixSparseStorage<T>
    {
        private readonly Dictionary<RGaKvIndexPairRecord, T> _indexScalarDictionary;

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return GetIndices1().Count();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return GetIndices2().Count();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return _indexScalarDictionary.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(ulong index1, ulong index2)
        {
            var indexPair = new RGaKvIndexPairRecord(index1, index2);

            return _indexScalarDictionary.TryGetValue(indexPair, out var value)
                ? value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(RGaKvIndexPairRecord index)
        {
            return _indexScalarDictionary.TryGetValue(index, out var value)
                ? value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices1()
        {
            return _indexScalarDictionary.Keys.Select(index => index.KvIndex1).Distinct();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices2()
        {
            return _indexScalarDictionary.Keys.Select(index => index.KvIndex2).Distinct();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetIndices()
        {
            return _indexScalarDictionary.Keys;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return _indexScalarDictionary.Values;
        }


        internal LinMatrixSparseStorage()
        {
            _indexScalarDictionary = new Dictionary<RGaKvIndexPairRecord, T>();
        }

        internal LinMatrixSparseStorage(Dictionary<RGaKvIndexPairRecord, T> indexScalarDictionary)
        {
            _indexScalarDictionary = indexScalarDictionary;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            _indexScalarDictionary.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValue(RGaKvIndexPairRecord index, T value)
        {
            if (_indexScalarDictionary.ContainsKey(index))
                _indexScalarDictionary[index] = value;
            else
                _indexScalarDictionary.Add(index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddValue(RGaKvIndexPairRecord index, T value)
        {
            _indexScalarDictionary.Add(index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Remove(RGaKvIndexPairRecord index)
        {
            return _indexScalarDictionary.Remove(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Remove(params RGaKvIndexPairRecord[] indexsList)
        {
            foreach (var index in indexsList)
                _indexScalarDictionary.Remove(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Remove(IEnumerable<RGaKvIndexPairRecord> indexsList)
        {
            foreach (var index in indexsList.ToArray())
                _indexScalarDictionary.Remove(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return _indexScalarDictionary.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index1, ulong index2)
        {
            var indexPair = new RGaKvIndexPairRecord(index1, index2);

            return _indexScalarDictionary.ContainsKey(indexPair);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex1()
        {
            return _indexScalarDictionary.Keys.Select(index => index.KvIndex1).Min();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex2()
        {
            return _indexScalarDictionary.Keys.Select(index => index.KvIndex2).Min();
        }

        public RGaKvIndexPairRecord GetMinIndex()
        {
            if (_indexScalarDictionary.Count == 0)
                throw new InvalidOperationException();

            var minIndex1 = ulong.MaxValue;
            var minIndex2 = ulong.MaxValue;

            foreach (var (index1, index2) in _indexScalarDictionary.Keys)
            {
                if (index1 < minIndex1)
                    minIndex1 = index1;
                
                if (index2 < minIndex2)
                    minIndex2 = index2;
            }

            return new RGaKvIndexPairRecord(minIndex1, minIndex2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex1()
        {
            return _indexScalarDictionary.Keys.Select(index => index.KvIndex1).Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex2()
        {
            return _indexScalarDictionary.Keys.Select(index => index.KvIndex2).Max();
        }

        public RGaKvIndexPairRecord GetMaxIndex()
        {
            if (_indexScalarDictionary.Count == 0)
                throw new InvalidOperationException();

            var maxIndex1 = ulong.MinValue;
            var maxIndex2 = ulong.MinValue;

            foreach (var (index1, index2) in _indexScalarDictionary.Keys)
            {
                if (index1 > maxIndex1)
                    maxIndex1 = index1;
                
                if (index2 > maxIndex2)
                    maxIndex2 = index2;
            }

            return new RGaKvIndexPairRecord(maxIndex1, maxIndex2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(RGaKvIndexPairRecord index)
        {
            return _indexScalarDictionary.ContainsKey(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(RGaKvIndexPairRecord index, out T value)
        {
            return _indexScalarDictionary.TryGetValue(index, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetEmptyIndices(ulong maxCount1, ulong maxCount2)
        {
            return new RGaKvIndexPairRecord(maxCount1, maxCount2)
                .GetIndexPairsInRange()
                .Except(_indexScalarDictionary.Keys);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(ulong index1, ulong index2, out T value)
        {
            return _indexScalarDictionary.TryGetValue(new RGaKvIndexPairRecord(index1, index2), out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetEmptyIndices(RGaKvIndexPairRecord maxCount)
        {
            return maxCount
                .GetIndexPairsInRange()
                .Except(_indexScalarDictionary.Keys);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetCopy()
        {
            return _indexScalarDictionary.CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> GetPermutation(Func<ulong, ulong, RGaKvIndexPairRecord> indexMapping)
        {
            var indexScalarDictionary = new Dictionary<RGaKvIndexPairRecord, T>();

            foreach (var ((index1, index2), scalar) in _indexScalarDictionary)
            {
                var index = indexMapping(index1, index2);
                
                indexScalarDictionary.Add(index, scalar);
            }

            return indexScalarDictionary.CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> GetPermutation(Func<RGaKvIndexPairRecord, RGaKvIndexPairRecord> indexMapping)
        {
            var indexScalarDictionary = new Dictionary<RGaKvIndexPairRecord, T>();

            foreach (var (index, scalar) in _indexScalarDictionary)
            {
                var index2 = indexMapping(index);
                
                indexScalarDictionary.Add(index2, scalar);
            }

            return indexScalarDictionary.CreateLinMatrixStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return _indexScalarDictionary
                .ToDictionary(valueMapping)
                .CreateLinMatrixStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexScalarMapping)
        {
            return _indexScalarDictionary
                .ToDictionary((indexPair, value) => indexScalarMapping(indexPair.KvIndex1, indexPair.KvIndex2, value))
                .CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            var indexScalarDictionary = new Dictionary<RGaKvIndexPairRecord, T>();

            foreach (var ((index1, index2), scalar) in _indexScalarDictionary)
            {
                if (!indexFilter(index1, index2)) continue;

                var index = new RGaKvIndexPairRecord(index1, index2);
                
                indexScalarDictionary.Add(index, scalar);
            }

            return indexScalarDictionary.CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexScalarFilter)
        {
            var indexScalarDictionary = new Dictionary<RGaKvIndexPairRecord, T>();

            foreach (var ((index1, index2), scalar) in _indexScalarDictionary)
            {
                if (!indexScalarFilter(index1, index2, scalar)) continue;

                var index = new RGaKvIndexPairRecord(index1, index2);
                
                indexScalarDictionary.Add(index, scalar);
            }

            return indexScalarDictionary.CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> FilterByScalar(Func<T, bool> scalarFilter)
        {
            var indexScalarDictionary = new Dictionary<RGaKvIndexPairRecord, T>();

            foreach (var ((index1, index2), scalar) in _indexScalarDictionary)
            {
                if (!scalarFilter(scalar)) continue;

                var index = new RGaKvIndexPairRecord(index1, index2);
                
                indexScalarDictionary.Add(index, scalar);
            }

            return indexScalarDictionary.CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> GetTranspose()
        {
            var indexScalarDictionary = new Dictionary<RGaKvIndexPairRecord, T>();

            foreach (var ((index1, index2), scalar) in _indexScalarDictionary)
            {
                var index = new RGaKvIndexPairRecord(index2, index1);
                
                indexScalarDictionary.Add(index, scalar);
            }

            return indexScalarDictionary.CreateLinMatrixStorage();
        }

        public ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, RGaGradeKvIndexPairRecord> indexToGradeIndexMapping)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<RGaKvIndexPairRecord, T>>();

            foreach (var ((id1, id2), scalar) in _indexScalarDictionary)
            {
                var (grade, index1, index2) = indexToGradeIndexMapping(id1, id2);

                if (!gradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary))
                {
                    indexScalarDictionary = new Dictionary<RGaKvIndexPairRecord, T>();
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
                }

                var index = new RGaKvIndexPairRecord(index1, index2);

                indexScalarDictionary.Add(index, scalar);
            }

            return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
        }

        public ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, T, RGaGradeKvIndexPairScalarRecord<T>> indexScalarToGradeIndexScalarMapping)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<RGaKvIndexPairRecord, T>>();

            foreach (var ((id1, id2), value) in _indexScalarDictionary)
            {
                var (grade, index1, index2, scalar) = indexScalarToGradeIndexScalarMapping(id1, id2, value);

                if (!gradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary))
                {
                    indexScalarDictionary = new Dictionary<RGaKvIndexPairRecord, T>();
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
                }

                var index = new RGaKvIndexPairRecord(index1, index2);

                indexScalarDictionary.Add(index, scalar);
            }

            return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetRow(ulong index1)
        {
            return _indexScalarDictionary
                .Where(indexScalar => indexScalar.Key.KvIndex1 == index1)
                .ToDictionary(
                    indexScalar => indexScalar.Key.KvIndex2,
                    indexScalar => indexScalar.Value
                ).CreateLinVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetColumn(ulong index2)
        {
            return _indexScalarDictionary
                .Where(indexScalar => indexScalar.Key.KvIndex1 == index2)
                .ToDictionary(
                    indexScalar => indexScalar.Key.KvIndex1,
                    indexScalar => indexScalar.Value
                ).CreateLinVectorStorage();
        }

        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetRows()
        {
            var indexPairScalarDictionary = new Dictionary<ulong, Dictionary<ulong, T>>();

            foreach (var ((index1, index2), scalar) in _indexScalarDictionary)
            {
                if (!indexPairScalarDictionary.TryGetValue(index1, out var indexScalarDictionary))
                {
                    indexScalarDictionary = new Dictionary<ulong, T>();
                    indexPairScalarDictionary.Add(index1, indexScalarDictionary);
                }

                indexScalarDictionary.Add(index2, scalar);
            }

            return indexPairScalarDictionary.Select(
                pair => new RGaKvIndexLinVectorStorageRecord<T>(
                    pair.Key, 
                    pair.Value.CreateLinVectorStorage()
                )
            );
        }

        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetRows(Func<ulong, bool> rowIndexFilter)
        {
            var indexPairScalarDictionary = new Dictionary<ulong, Dictionary<ulong, T>>();

            foreach (var ((index1, index2), scalar) in _indexScalarDictionary)
            {
                if (!rowIndexFilter(index1)) continue;

                if (!indexPairScalarDictionary.TryGetValue(index1, out var indexScalarDictionary))
                {
                    indexScalarDictionary = new Dictionary<ulong, T>();
                    indexPairScalarDictionary.Add(index1, indexScalarDictionary);
                }

                indexScalarDictionary.Add(index2, scalar);
            }

            return indexPairScalarDictionary.Select(
                pair => new RGaKvIndexLinVectorStorageRecord<T>(
                    pair.Key, 
                    pair.Value.CreateLinVectorStorage()
                )
            );
        }

        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetColumns()
        {
            var indexPairScalarDictionary = new Dictionary<ulong, Dictionary<ulong, T>>();

            foreach (var ((index1, index2), scalar) in _indexScalarDictionary)
            {
                if (!indexPairScalarDictionary.TryGetValue(index2, out var indexScalarDictionary))
                {
                    indexScalarDictionary = new Dictionary<ulong, T>();
                    indexPairScalarDictionary.Add(index2, indexScalarDictionary);
                }

                indexScalarDictionary.Add(index1, scalar);
            }

            return indexPairScalarDictionary.Select(
                pair => new RGaKvIndexLinVectorStorageRecord<T>(
                    pair.Key, 
                    pair.Value.CreateLinVectorStorage()
                )
            );
        }

        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetColumns(Func<ulong, bool> columnIndexFilter)
        {
            var indexPairScalarDictionary = new Dictionary<ulong, Dictionary<ulong, T>>();

            foreach (var ((index1, index2), scalar) in _indexScalarDictionary)
            {
                if (!columnIndexFilter(index2)) continue;

                if (!indexPairScalarDictionary.TryGetValue(index2, out var indexScalarDictionary))
                {
                    indexScalarDictionary = new Dictionary<ulong, T>();
                    indexPairScalarDictionary.Add(index2, indexScalarDictionary);
                }

                indexScalarDictionary.Add(index1, scalar);
            }

            return indexPairScalarDictionary.Select(
                pair => new RGaKvIndexLinVectorStorageRecord<T>(
                    pair.Key, 
                    pair.Value.CreateLinVectorStorage()
                )
            );
        }

        public bool TryGetCompactStorage(out ILinMatrixStorage<T> matrixStorage)
        {
            if (_indexScalarDictionary.Count == 0)
            {
                matrixStorage = LinMatrixEmptyStorage<T>.EmptyStorage;
                return true;
            }

            if (_indexScalarDictionary.Count == 1)
            {
                var ((index1, index2), value) = _indexScalarDictionary.First();

                matrixStorage = index1 == 0UL && index2 == 0UL
                    ? new LinMatrixSingleScalarDenseStorage<T>(value)
                    : new LinMatrixSingleScalarSparseStorage<T>(index1, index2, value);

                return true;
            }

            matrixStorage = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairScalarRecord<T>> GetIndexScalarRecords()
        {
            return _indexScalarDictionary.Select(pair => 
                new RGaKvIndexPairScalarRecord<T>(pair.Key.KvIndex1, pair.Key.KvIndex2, pair.Value)
            );
        }
        
        public ILinVectorStorage<T> CombineRows(IReadOnlyList<T> scalarList, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            ILinVectorStorage<T> vector = null;

            var count = scalarList.Count;
            for (var rowIndex = 0; rowIndex < count; rowIndex++)
            {
                var rowVector = GetRow((ulong) rowIndex);
                if (rowVector.IsEmpty()) continue;

                var scalingFactor = scalarList[rowIndex];
                var scaledVector = scalingFunc(scalingFactor, rowVector);

                vector = vector is null
                    ? scaledVector
                    : reducingFunc(vector, scaledVector);
            }

            return vector ?? LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public ILinVectorStorage<T> CombineRows(IEnumerable<RGaKvIndexScalarRecord<T>> rowIndexScalarRecords,
            Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc,
            Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            ILinVectorStorage<T> vector = null;

            foreach (var (rowIndex, scalingFactor) in rowIndexScalarRecords)
            {
                var rowVector = GetRow(rowIndex);
                if (rowVector.IsEmpty()) continue;

                var scaledVector = scalingFunc(scalingFactor, rowVector);

                vector = vector is null
                    ? scaledVector
                    : reducingFunc(vector, scaledVector);
            }

            return vector ?? LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public ILinVectorStorage<T> CombineColumns(IReadOnlyList<T> scalarList, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            ILinVectorStorage<T> vector = null;

            var count = scalarList.Count;
            for (var columnIndex = 0; columnIndex < count; columnIndex++)
            {
                var columnVector = GetColumn((ulong) columnIndex);
                if (columnVector.IsEmpty()) continue;

                var scalingFactor = scalarList[columnIndex];
                var scaledVector = scalingFunc(scalingFactor, columnVector);

                vector = vector is null
                    ? scaledVector
                    : reducingFunc(vector, scaledVector);
            }

            return vector ?? LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public ILinVectorStorage<T> CombineColumns(IEnumerable<RGaKvIndexScalarRecord<T>> columnIndexScalarRecords,
            Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc,
            Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            ILinVectorStorage<T> vector = null;

            foreach (var (columnIndex, scalingFactor) in columnIndexScalarRecords)
            {
                var columnVector = GetColumn(columnIndex);
                if (columnVector.IsEmpty()) continue;

                var scaledVector = scalingFunc(scalingFactor, columnVector);

                vector = vector is null
                    ? scaledVector
                    : reducingFunc(vector, scaledVector);
            }

            return vector ?? LinVectorEmptyStorage<T>.EmptyStorage;
        }
    }
}