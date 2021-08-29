using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices
{
    public sealed record LaMatrixSparseStorage<T> :
        ILaMatrixSparseEvenStorage<T>
    {
        private readonly Dictionary<IndexPairRecord, T> _indexValueDictionary;


        public int GetSparseCount1()
        {
            return GetIndices1().Count();
        }

        public int GetSparseCount2()
        {
            return GetIndices2().Count();
        }

        public int GetSparseCount()
        {
            return _indexValueDictionary.Count;
        }

        public T GetScalar(ulong index1, ulong index2)
        {
            var indexPair = new IndexPairRecord(index1, index2);

            return _indexValueDictionary.TryGetValue(indexPair, out var value)
                ? value
                : throw new KeyNotFoundException();
        }

        public T GetScalar(IndexPairRecord index)
        {
            return _indexValueDictionary.TryGetValue(index, out var value)
                ? value
                : throw new KeyNotFoundException();
        }

        public IEnumerable<ulong> GetIndices1()
        {
            return _indexValueDictionary.Keys.Select(index => index.Index1).Distinct();
        }

        public IEnumerable<ulong> GetIndices2()
        {
            return _indexValueDictionary.Keys.Select(index => index.Index2).Distinct();
        }

        public IEnumerable<IndexPairRecord> GetIndices()
        {
            return _indexValueDictionary.Keys;
        }

        public IEnumerable<T> GetScalars()
        {
            return _indexValueDictionary.Values;
        }


        internal LaMatrixSparseStorage()
        {
            _indexValueDictionary = new Dictionary<IndexPairRecord, T>();
        }

        internal LaMatrixSparseStorage([NotNull] Dictionary<IndexPairRecord, T> indexValueDictionary)
        {
            _indexValueDictionary = indexValueDictionary;
        }


        public void Clear()
        {
            _indexValueDictionary.Clear();
        }

        public void SetValue(IndexPairRecord index, [NotNull] T value)
        {
            if (_indexValueDictionary.ContainsKey(index))
                _indexValueDictionary[index] = value;
            else
                _indexValueDictionary.Add(index, value);
        }

        public void AddValue(IndexPairRecord index, [NotNull] T value)
        {
            _indexValueDictionary.Add(index, value);
        }

        public bool Remove(IndexPairRecord index)
        {
            return _indexValueDictionary.Remove(index);
        }

        public void Remove(params IndexPairRecord[] indexsList)
        {
            foreach (var index in indexsList)
                _indexValueDictionary.Remove(index);
        }

        public void Remove(IEnumerable<IndexPairRecord> indexsList)
        {
            foreach (var index in indexsList.ToArray())
                _indexValueDictionary.Remove(index);
        }

        public bool IsEmpty()
        {
            return _indexValueDictionary.Count == 0;
        }

        public bool ContainsIndex(ulong index1, ulong index2)
        {
            var indexPair = new IndexPairRecord(index1, index2);

            return _indexValueDictionary.ContainsKey(indexPair);
        }

        public ulong GetMinIndex1()
        {
            throw new NotImplementedException();
        }

        public ulong GetMinIndex2()
        {
            throw new NotImplementedException();
        }

        public IndexPairRecord GetMinIndex()
        {
            throw new NotImplementedException();
        }

        public ulong GetMaxIndex1()
        {
            throw new NotImplementedException();
        }

        public ulong GetMaxIndex2()
        {
            throw new NotImplementedException();
        }

        public IndexPairRecord GetMaxIndex()
        {
            throw new NotImplementedException();
        }

        public bool ContainsIndex(IndexPairRecord index)
        {
            throw new NotImplementedException();
        }

        public bool TryGetScalar(IndexPairRecord index, out T value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndexPairRecord> GetEmptyIndices(ulong maxKey1, ulong maxKey2)
        {
            throw new NotImplementedException();
        }

        public bool TryGetScalar(ulong index1, ulong index2, out T value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndexPairRecord> GetEmptyIndices(IndexPairRecord maxKey)
        {
            throw new NotImplementedException();
        }

        public ILaMatrixEvenStorage<T> GetCopy()
        {
            throw new NotImplementedException();
        }

        public ILaMatrixEvenStorage<T> MapIndices(Func<ulong, ulong, IndexPairRecord> indexMapping)
        {
            throw new NotImplementedException();
        }

        public ILaMatrixEvenStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            throw new NotImplementedException();
        }

        public ILaMatrixEvenStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexValueMapping)
        {
            throw new NotImplementedException();
        }

        public ILaMatrixEvenStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            throw new NotImplementedException();
        }

        public ILaMatrixEvenStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexValueFilter)
        {
            throw new NotImplementedException();
        }

        public ILaMatrixEvenStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            throw new NotImplementedException();
        }

        public ILaMatrixEvenStorage<T> Transpose()
        {
            throw new NotImplementedException();
        }

        public ILaMatrixGradedStorage<T> ToGradedStorage(Func<ulong, ulong, GradeIndexPairRecord> evenKeyToGradeKeyMapping)
        {
            throw new NotImplementedException();
        }

        public ILaMatrixGradedStorage<T> ToGradedStorage(Func<ulong, ulong, T, GradeIndexPairScalarRecord<T>> evenIndexScalarToGradeIndexScalarMapping)
        {
            throw new NotImplementedException();
        }

        public ILaVectorEvenStorage<T> GetRow(ulong index1)
        {
            throw new NotImplementedException();
        }

        public ILaVectorEvenStorage<T> GetColumn(ulong index2)
        {
            throw new NotImplementedException();
        }

        public bool TryGetCompactStorage(out ILaMatrixEvenStorage<T> evenGrid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndexPairScalarRecord<T>> GetIndexScalarRecords()
        {
            throw new NotImplementedException();
        }
    }
}