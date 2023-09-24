using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Sparse;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices
{
    public sealed record LinMatrixEmptyStorage<T> :
        ILinMatrixDenseStorage<T>, 
        ILinMatrixSparseStorage<T>
    {
        public static LinMatrixEmptyStorage<T> EmptyStorage { get; }
            = new LinMatrixEmptyStorage<T>();


        public int Count1 
            => 0;

        public int Count2 
            => 0;

        public int Count 
            => 0;

        public bool IsSquare 
            => true;


        private LinMatrixEmptyStorage()
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(RGaKvIndexPairRecord key)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(ulong index1, ulong index2)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices1()
        {
            return Enumerable.Empty<ulong>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices2()
        {
            return Enumerable.Empty<ulong>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetIndices()
        {
            return Enumerable.Empty<RGaKvIndexPairRecord>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return Enumerable.Empty<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index1, ulong index2)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(RGaKvIndexPairRecord key)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex1()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex2()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaKvIndexPairRecord GetMinIndex()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex1()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex2()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaKvIndexPairRecord GetMaxIndex()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(RGaKvIndexPairRecord key, out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(ulong index1, ulong index2, out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetEmptyIndices(ulong maxCount1, ulong maxCount2)
        {
            return new RGaKvIndexPairRecord(maxCount1, maxCount2).GetIndexPairsInRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetEmptyIndices(RGaKvIndexPairRecord maxCount)
        {
            return maxCount.GetIndexPairsInRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetCopy()
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetPermutation(Func<ulong, ulong, RGaKvIndexPairRecord> keyMapping)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetPermutation(Func<RGaKvIndexPairRecord, RGaKvIndexPairRecord> indexMapping)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return LinMatrixEmptyStorage<T2>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            return LinMatrixEmptyStorage<T2>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> FilterByIndex(Func<ulong, ulong, bool> keyFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetTranspose()
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, RGaGradeKvIndexPairRecord> indexToGradeIndexMapping)
        {
            return LinMatrixEmptyGradedStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, T, RGaGradeKvIndexPairScalarRecord<T>> indexScalarToGradeIndexScalarMapping)
        {
            return LinMatrixEmptyGradedStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetRow(ulong index1)
        {
            return LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetColumn(ulong index2)
        {
            return LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetRows()
        {
            return Enumerable.Empty<RGaKvIndexLinVectorStorageRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetRows(Func<ulong, bool> rowIndexFilter)
        {
            return Enumerable.Empty<RGaKvIndexLinVectorStorageRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetColumns()
        {
            return Enumerable.Empty<RGaKvIndexLinVectorStorageRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetColumns(Func<ulong, bool> columnIndexFilter)
        {
            return Enumerable.Empty<RGaKvIndexLinVectorStorageRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> CombineRows(IReadOnlyList<T> scalarList, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            return LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> CombineRows(IEnumerable<RGaKvIndexScalarRecord<T>> rowIndexScalarRecords,
            Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc,
            Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            return LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> CombineColumns(IReadOnlyList<T> scalarList, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            return LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> CombineColumns(IEnumerable<RGaKvIndexScalarRecord<T>> columnIndexScalarRecords,
            Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc,
            Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            return LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILinMatrixStorage<T> matrixStorage)
        {
            matrixStorage = EmptyStorage;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairScalarRecord<T>> GetIndexScalarRecords()
        {
            return Enumerable.Empty<RGaKvIndexPairScalarRecord<T>>();
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixDenseStorage<T> GetDensePermutation(Func<ulong, ulong, RGaKvIndexPairRecord> indexMapping)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixDenseStorage<T> GetDensePermutation(Func<RGaKvIndexPairRecord, RGaKvIndexPairRecord> indexMapping)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetDenseRows(IEnumerable<ulong> rowIndexList)
        {
            return Enumerable.Empty<RGaKvIndexLinVectorStorageRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetDenseColumns(IEnumerable<ulong> columnIndexList)
        {
            return Enumerable.Empty<RGaKvIndexLinVectorStorageRecord<T>>();
        }
    }
}