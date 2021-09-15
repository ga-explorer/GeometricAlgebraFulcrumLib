using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Sparse
{
    public sealed record LinMatrixSparseStorage<T> :
        ILinMatrixSparseStorage<T>
    {
        private readonly Dictionary<IndexPairRecord, T> _indexScalarDictionary;

        
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
            var indexPair = new IndexPairRecord(index1, index2);

            return _indexScalarDictionary.TryGetValue(indexPair, out var value)
                ? value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(IndexPairRecord index)
        {
            return _indexScalarDictionary.TryGetValue(index, out var value)
                ? value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices1()
        {
            return _indexScalarDictionary.Keys.Select(index => index.Index1).Distinct();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices2()
        {
            return _indexScalarDictionary.Keys.Select(index => index.Index2).Distinct();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairRecord> GetIndices()
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
            _indexScalarDictionary = new Dictionary<IndexPairRecord, T>();
        }

        internal LinMatrixSparseStorage([NotNull] Dictionary<IndexPairRecord, T> indexScalarDictionary)
        {
            _indexScalarDictionary = indexScalarDictionary;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            _indexScalarDictionary.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValue(IndexPairRecord index, [NotNull] T value)
        {
            if (_indexScalarDictionary.ContainsKey(index))
                _indexScalarDictionary[index] = value;
            else
                _indexScalarDictionary.Add(index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddValue(IndexPairRecord index, [NotNull] T value)
        {
            _indexScalarDictionary.Add(index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Remove(IndexPairRecord index)
        {
            return _indexScalarDictionary.Remove(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Remove(params IndexPairRecord[] indexsList)
        {
            foreach (var index in indexsList)
                _indexScalarDictionary.Remove(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Remove(IEnumerable<IndexPairRecord> indexsList)
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
            var indexPair = new IndexPairRecord(index1, index2);

            return _indexScalarDictionary.ContainsKey(indexPair);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex1()
        {
            return _indexScalarDictionary.Keys.Select(index => index.Index1).Min();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex2()
        {
            return _indexScalarDictionary.Keys.Select(index => index.Index2).Min();
        }

        public IndexPairRecord GetMinIndex()
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

            return new IndexPairRecord(minIndex1, minIndex2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex1()
        {
            return _indexScalarDictionary.Keys.Select(index => index.Index1).Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex2()
        {
            return _indexScalarDictionary.Keys.Select(index => index.Index2).Max();
        }

        public IndexPairRecord GetMaxIndex()
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

            return new IndexPairRecord(maxIndex1, maxIndex2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(IndexPairRecord index)
        {
            return _indexScalarDictionary.ContainsKey(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(IndexPairRecord index, out T value)
        {
            return _indexScalarDictionary.TryGetValue(index, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairRecord> GetEmptyIndices(ulong maxCount1, ulong maxCount2)
        {
            return new IndexPairRecord(maxCount1, maxCount2)
                .GetIndexPairsInRange()
                .Except(_indexScalarDictionary.Keys);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(ulong index1, ulong index2, out T value)
        {
            return _indexScalarDictionary.TryGetValue(new IndexPairRecord(index1, index2), out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairRecord> GetEmptyIndices(IndexPairRecord maxCount)
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

        public ILinMatrixStorage<T> GetPermutation(Func<ulong, ulong, IndexPairRecord> indexMapping)
        {
            var indexScalarDictionary = new Dictionary<IndexPairRecord, T>();

            foreach (var ((index1, index2), scalar) in _indexScalarDictionary)
            {
                var index = indexMapping(index1, index2);
                
                indexScalarDictionary.Add(index, scalar);
            }

            return indexScalarDictionary.CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> GetPermutation(Func<IndexPairRecord, IndexPairRecord> indexMapping)
        {
            var indexScalarDictionary = new Dictionary<IndexPairRecord, T>();

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
                .ToDictionary((indexPair, value) => indexScalarMapping(indexPair.Index1, indexPair.Index2, value))
                .CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            var indexScalarDictionary = new Dictionary<IndexPairRecord, T>();

            foreach (var ((index1, index2), scalar) in _indexScalarDictionary)
            {
                if (!indexFilter(index1, index2)) continue;

                var index = new IndexPairRecord(index1, index2);
                
                indexScalarDictionary.Add(index, scalar);
            }

            return indexScalarDictionary.CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexScalarFilter)
        {
            var indexScalarDictionary = new Dictionary<IndexPairRecord, T>();

            foreach (var ((index1, index2), scalar) in _indexScalarDictionary)
            {
                if (!indexScalarFilter(index1, index2, scalar)) continue;

                var index = new IndexPairRecord(index1, index2);
                
                indexScalarDictionary.Add(index, scalar);
            }

            return indexScalarDictionary.CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> FilterByScalar(Func<T, bool> scalarFilter)
        {
            var indexScalarDictionary = new Dictionary<IndexPairRecord, T>();

            foreach (var ((index1, index2), scalar) in _indexScalarDictionary)
            {
                if (!scalarFilter(scalar)) continue;

                var index = new IndexPairRecord(index1, index2);
                
                indexScalarDictionary.Add(index, scalar);
            }

            return indexScalarDictionary.CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> GetTranspose()
        {
            var indexScalarDictionary = new Dictionary<IndexPairRecord, T>();

            foreach (var ((index1, index2), scalar) in _indexScalarDictionary)
            {
                var index = new IndexPairRecord(index2, index1);
                
                indexScalarDictionary.Add(index, scalar);
            }

            return indexScalarDictionary.CreateLinMatrixStorage();
        }

        public ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, GradeIndexPairRecord> indexToGradeIndexMapping)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<IndexPairRecord, T>>();

            foreach (var ((id1, id2), scalar) in _indexScalarDictionary)
            {
                var (grade, index1, index2) = indexToGradeIndexMapping(id1, id2);

                if (!gradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary))
                {
                    indexScalarDictionary = new Dictionary<IndexPairRecord, T>();
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
                }

                var index = new IndexPairRecord(index1, index2);

                indexScalarDictionary.Add(index, scalar);
            }

            return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
        }

        public ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, T, GradeIndexPairScalarRecord<T>> indexScalarToGradeIndexScalarMapping)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<IndexPairRecord, T>>();

            foreach (var ((id1, id2), value) in _indexScalarDictionary)
            {
                var (grade, index1, index2, scalar) = indexScalarToGradeIndexScalarMapping(id1, id2, value);

                if (!gradeIndexScalarDictionary.TryGetValue(grade, out var indexScalarDictionary))
                {
                    indexScalarDictionary = new Dictionary<IndexPairRecord, T>();
                    gradeIndexScalarDictionary.Add(grade, indexScalarDictionary);
                }

                var index = new IndexPairRecord(index1, index2);

                indexScalarDictionary.Add(index, scalar);
            }

            return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetRow(ulong index1)
        {
            return _indexScalarDictionary
                .Where(indexScalar => indexScalar.Key.Index1 == index1)
                .ToDictionary(
                    indexScalar => indexScalar.Key.Index2,
                    indexScalar => indexScalar.Value
                ).CreateLinVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetColumn(ulong index2)
        {
            return _indexScalarDictionary
                .Where(indexScalar => indexScalar.Key.Index1 == index2)
                .ToDictionary(
                    indexScalar => indexScalar.Key.Index1,
                    indexScalar => indexScalar.Value
                ).CreateLinVectorStorage();
        }

        public IEnumerable<IndexLinVectorStorageRecord<T>> GetRows()
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
                pair => new IndexLinVectorStorageRecord<T>(
                    pair.Key, 
                    pair.Value.CreateLinVectorStorage()
                )
            );
        }

        public IEnumerable<IndexLinVectorStorageRecord<T>> GetRows(Func<ulong, bool> rowIndexFilter)
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
                pair => new IndexLinVectorStorageRecord<T>(
                    pair.Key, 
                    pair.Value.CreateLinVectorStorage()
                )
            );
        }

        public IEnumerable<IndexLinVectorStorageRecord<T>> GetColumns()
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
                pair => new IndexLinVectorStorageRecord<T>(
                    pair.Key, 
                    pair.Value.CreateLinVectorStorage()
                )
            );
        }

        public IEnumerable<IndexLinVectorStorageRecord<T>> GetColumns(Func<ulong, bool> columnIndexFilter)
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
                pair => new IndexLinVectorStorageRecord<T>(
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
        public IEnumerable<IndexPairScalarRecord<T>> GetIndexScalarRecords()
        {
            return _indexScalarDictionary.Select(pair => 
                new IndexPairScalarRecord<T>(pair.Key.Index1, pair.Key.Index2, pair.Value)
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

        public ILinVectorStorage<T> CombineRows(IEnumerable<IndexScalarRecord<T>> rowIndexScalarRecords, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
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

        public ILinVectorStorage<T> CombineColumns(IEnumerable<IndexScalarRecord<T>> columnIndexScalarRecords, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
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