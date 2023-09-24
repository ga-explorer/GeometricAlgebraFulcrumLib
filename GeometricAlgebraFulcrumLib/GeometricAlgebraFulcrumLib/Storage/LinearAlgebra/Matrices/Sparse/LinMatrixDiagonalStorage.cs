using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Sparse
{
    public sealed class LinMatrixDiagonalStorage<T> :
        ILinMatrixSparseStorage<T>
    {
        public ILinVectorStorage<T> SourceList { get; }


        internal LinMatrixDiagonalStorage(ILinVectorStorage<T> sourceList)
        {
            SourceList = sourceList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return SourceList.IsEmpty();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return SourceList.GetSparseCount();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return SourceList.GetScalars();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return SourceList.GetSparseCount();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return SourceList.GetSparseCount();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices1()
        {
            return SourceList.GetIndices();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices2()
        {
            return SourceList.GetIndices();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetIndices()
        {
            return SourceList.GetIndices().Select(index => new RGaKvIndexPairRecord(index, index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairScalarRecord<T>> GetIndexScalarRecords()
        {
            return SourceList.GetIndexScalarRecords().Select(indexValue => new RGaKvIndexPairScalarRecord<T>(indexValue.KvIndex, indexValue.KvIndex, indexValue.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetEmptyIndices(ulong maxCount1, ulong maxCount2)
        {
            var maxKey = new RGaKvIndexPairRecord(maxCount1, maxCount2);

            return new HashSet<RGaKvIndexPairRecord>(maxKey.GetIndexPairsInRange())
                .Except(SourceList
                    .GetIndices()
                    .Select(index => new RGaKvIndexPairRecord(index, index))
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetEmptyIndices(RGaKvIndexPairRecord maxCount)
        {
            return new HashSet<RGaKvIndexPairRecord>(maxCount.GetIndexPairsInRange())
                .Except(SourceList
                    .GetIndices()
                    .Select(index => new RGaKvIndexPairRecord(index, index))
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(ulong index1, ulong index2)
        {
            return index1 == index2
                ? SourceList.GetScalar(index1)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(RGaKvIndexPairRecord index)
        {
            var (index1, index2) = index;

            return index1 == index2
                ? SourceList.GetScalar(index1)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index1, ulong index2)
        {
            return index1 == index2 && SourceList.ContainsIndex(index1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(RGaKvIndexPairRecord index)
        {
            var (index1, index2) = index;

            return index1 == index2 && SourceList.ContainsIndex(index1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex1()
        {
            return SourceList.GetMinIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex2()
        {
            return SourceList.GetMinIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaKvIndexPairRecord GetMinIndex()
        {
            var index = SourceList.GetMinIndex();

            return new RGaKvIndexPairRecord(index, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex1()
        {
            return SourceList.GetMaxIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex2()
        {
            return SourceList.GetMaxIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaKvIndexPairRecord GetMaxIndex()
        {
            var index = SourceList.GetMaxIndex();

            return new RGaKvIndexPairRecord(index, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(ulong index1, ulong index2, out T value)
        {
            if (index1 == index2 && SourceList.TryGetScalar(index1, out value))
                return true;

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(RGaKvIndexPairRecord index, out T value)
        {
            var (index1, index2) = index;

            if (index1 == index2 && SourceList.TryGetScalar(index1, out value))
                return true;

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetCopy()
        {
            return this;
        }

        public ILinMatrixStorage<T> GetPermutation(Func<ulong, ulong, RGaKvIndexPairRecord> indexMapping)
        {
            var indexValueDictionary = new Dictionary<RGaKvIndexPairRecord, T>();

            foreach (var (k, value) in SourceList.GetIndexScalarRecords())
            {
                var (index1, index2) = indexMapping(k, k);

                var index = new RGaKvIndexPairRecord(index1, index2); 

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = value;
                else
                    indexValueDictionary.Add(index, value);
            }

            return indexValueDictionary.CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> GetPermutation(Func<RGaKvIndexPairRecord, RGaKvIndexPairRecord> indexMapping)
        {
            var indexValueDictionary = new Dictionary<RGaKvIndexPairRecord, T>();

            foreach (var (k, value) in SourceList.GetIndexScalarRecords())
            {
                var (index1, index2) = indexMapping(new RGaKvIndexPairRecord(k, k));

                var index = new RGaKvIndexPairRecord(index1, index2); 

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = value;
                else
                    indexValueDictionary.Add(index, value);
            }

            return indexValueDictionary.CreateLinMatrixStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LinMatrixDiagonalStorage<T2>(SourceList.MapScalars(valueMapping));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexValueMapping)
        {
            return new LinMatrixDiagonalStorage<T2>(SourceList.MapScalars((index, value) => indexValueMapping(index, index, value)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            return new LinMatrixDiagonalStorage<T>(SourceList.FilterByIndex(index => indexFilter(index, index)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexValueFilter)
        {
            return new LinMatrixDiagonalStorage<T>(SourceList.FilterByIndexScalar((index, value) => indexValueFilter(index, index, value)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return new LinMatrixDiagonalStorage<T>(SourceList.FilterByScalar(valueFilter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetTranspose()
        {
            return this;
        }

        public bool TryGetCompactStorage(out ILinMatrixStorage<T> matrixStorage)
        {
            if (SourceList.TryGetCompactStorage(out var sourceList))
            {
                var count = sourceList.GetSparseCount();

                matrixStorage = count switch
                {
                    0 => LinMatrixEmptyStorage<T>.EmptyStorage,
                    1 => sourceList.GetMinIndexScalarRecord().CreateLinMatrixSingleScalarStorage(),
                    _ => new LinMatrixDiagonalStorage<T>(sourceList)
                };

                return true;
            }
            else
            {
                var count = SourceList.GetSparseCount();

                if (count > 1)
                {
                    matrixStorage = this;
                    return false;
                }
                
                matrixStorage = count == 0
                    ? LinMatrixEmptyStorage<T>.EmptyStorage
                    : sourceList.GetMinIndexScalarRecord().CreateLinMatrixSingleScalarStorage();

                return true;
            }
        }

        public ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, RGaGradeKvIndexPairRecord> indexToGradeIndexMapping)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<RGaKvIndexPairRecord, T>>();

            foreach (var (k, value) in SourceList.GetIndexScalarRecords())
            {
                var (grade, index1, index2) = indexToGradeIndexMapping(k, k);

                if (!gradeIndexScalarDictionary.TryGetValue(grade, out var indexValueDictionary))
                {
                    indexValueDictionary = new Dictionary<RGaKvIndexPairRecord, T>();
                    gradeIndexScalarDictionary.Add(grade, indexValueDictionary);
                }

                var index = new RGaKvIndexPairRecord(index1, index2); 

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = value;
                else
                    indexValueDictionary.Add(index, value);
            }

            return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
        }

        public ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, T, RGaGradeKvIndexPairScalarRecord<T>> indexScalarToGradeIndexScalarMapping)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<RGaKvIndexPairRecord, T>>();

            foreach (var (k, value) in SourceList.GetIndexScalarRecords())
            {
                var (grade, index1, index2, scalar) = indexScalarToGradeIndexScalarMapping(k, k, value);

                if (!gradeIndexScalarDictionary.TryGetValue(grade, out var indexValueDictionary))
                {
                    indexValueDictionary = new Dictionary<RGaKvIndexPairRecord, T>();
                    gradeIndexScalarDictionary.Add(grade, indexValueDictionary);
                }

                var index = new RGaKvIndexPairRecord(index1, index2); 

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = scalar;
                else
                    indexValueDictionary.Add(index, scalar);
            }

            return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetRow(ulong index1)
        {
            return SourceList.TryGetScalar(index1, out var value)
                ? value.CreateLinVectorSingleScalarStorage(index1)
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetColumn(ulong index2)
        {
            return SourceList.TryGetScalar(index2, out var value)
                ? value.CreateLinVectorSingleScalarStorage(index2)
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetRows()
        {
            return SourceList
                .GetIndexScalarRecords()
                .Select(indexScalar => 
                        new RGaKvIndexLinVectorStorageRecord<T>(indexScalar.KvIndex,
                        indexScalar.Scalar.CreateLinVectorSingleScalarStorage(indexScalar.KvIndex)
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetRows(Func<ulong, bool> rowIndexFilter)
        {
            return SourceList
                .GetIndexScalarRecords()
                .Where(r => rowIndexFilter(r.KvIndex))
                .Select(indexScalar => 
                    new RGaKvIndexLinVectorStorageRecord<T>(indexScalar.KvIndex,
                        indexScalar.Scalar.CreateLinVectorSingleScalarStorage(indexScalar.KvIndex)
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetColumns()
        {
            return SourceList
                .GetIndexScalarRecords()
                .Select(indexScalar => 
                    new RGaKvIndexLinVectorStorageRecord<T>(indexScalar.KvIndex,
                        indexScalar.Scalar.CreateLinVectorSingleScalarStorage(indexScalar.KvIndex)
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetColumns(Func<ulong, bool> columnIndexFilter)
        {
            return SourceList
                .GetIndexScalarRecords()
                .Where(r => columnIndexFilter(r.KvIndex))
                .Select(indexScalar => 
                    new RGaKvIndexLinVectorStorageRecord<T>(indexScalar.KvIndex,
                        indexScalar.Scalar.CreateLinVectorSingleScalarStorage(indexScalar.KvIndex)
                    )
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