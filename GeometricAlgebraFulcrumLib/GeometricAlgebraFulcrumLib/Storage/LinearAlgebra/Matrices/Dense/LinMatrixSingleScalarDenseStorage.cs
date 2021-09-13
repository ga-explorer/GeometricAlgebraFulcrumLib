using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Sparse;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense
{
    public sealed record LinMatrixSingleScalarDenseStorage<T> :
        ILinMatrixSingleScalarStorage<T>,
        ILinMatrixMutableDenseStorage<T>
    {
        public ulong Index1 
            => 0UL;

        public ulong Index2 
            => 0UL;

        public IndexPairRecord Index 
            => GaFuLRecordsFactory.ZeroIndexPairRecord;

        public T Scalar { get; set; }

        public int Count1 
            => 1;

        public int Count2 
            => 1;

        public int Count 
            => 1;
        
        public bool IsSquare 
            => true;

        public T this[int index1, int index2]
        {
            get => index1 == 0 && index2 == 0
                ? Scalar 
                : throw new KeyNotFoundException();
            set
            {
                if (index1 == 0 && index2 == 0) 
                    Scalar = value;
                else
                    throw new KeyNotFoundException();
            }
        }

        public T this[ulong index1, ulong index2]
        {
            get => index1 == 0UL && index2 == 0UL
                ? Scalar 
                : throw new KeyNotFoundException();
            set
            {
                if (index1 == 0UL && index2 == 0UL)
                    Scalar = value;
                else
                    throw new KeyNotFoundException();
            }
        }

        internal LinMatrixSingleScalarDenseStorage([NotNull] T value)
        {
            Scalar = value;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices1()
        {
            yield return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices2()
        {
            yield return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairRecord> GetIndices()
        {
            yield return GaFuLRecordsFactory.ZeroIndexPairRecord;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            yield return Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(IndexPairRecord index)
        {
            return index == GaFuLRecordsFactory.ZeroIndexPairRecord
                ? Scalar
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(ulong index1, ulong index2)
        {
            return index1 == 0UL && index2 == 0UL
                ? Scalar
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index1, ulong index2)
        {
            return index1 == 0UL && 
                   index2 == 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(IndexPairRecord index)
        {
            return index == GaFuLRecordsFactory.ZeroIndexPairRecord;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex1()
        {
            return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex2()
        {
            return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexPairRecord GetMinIndex()
        {
            return GaFuLRecordsFactory.ZeroIndexPairRecord;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex1()
        {
            return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex2()
        {
            return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexPairRecord GetMaxIndex()
        {
            return GaFuLRecordsFactory.ZeroIndexPairRecord;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(IndexPairRecord index, out T value)
        {
            if (index == GaFuLRecordsFactory.ZeroIndexPairRecord)
            {
                value = Scalar;
                return true;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(ulong index1, ulong index2, out T value)
        {
            if (index1 == 0UL && index2 == 0UL)
            {
                value = Scalar;
                return true;
            }

            value = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairRecord> GetEmptyIndices(ulong maxCount1, ulong maxCount2)
        {
            return new IndexPairRecord(maxCount1, maxCount2)
                .GetIndexPairsInRange()
                .Where(index => index != GaFuLRecordsFactory.ZeroIndexPairRecord);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairRecord> GetEmptyIndices(IndexPairRecord maxCount)
        {
            return maxCount
                .GetIndexPairsInRange()
                .Where(index => index != GaFuLRecordsFactory.ZeroIndexPairRecord);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetCopy()
        {
            return new LinMatrixSingleScalarSparseStorage<T>(GaFuLRecordsFactory.ZeroIndexPairRecord, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetPermutation(Func<ulong, ulong, IndexPairRecord> indexMapping)
        {
            return Scalar.CreateLinMatrixSingleScalarStorage(indexMapping(0UL, 0UL));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetPermutation(Func<IndexPairRecord, IndexPairRecord> indexMapping)
        {
            return Scalar.CreateLinMatrixSingleScalarStorage(indexMapping(GaFuLRecordsFactory.ZeroIndexPairRecord));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LinMatrixSingleScalarDenseStorage<T2>(
                valueMapping(Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexValueMapping)
        {
            return new LinMatrixSingleScalarDenseStorage<T2>(
                indexValueMapping(0UL, 0UL, Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            return indexFilter(0UL, 0UL)
                ? this
                : LinMatrixEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexValueFilter)
        {
            return indexValueFilter(0UL, 0UL, Scalar)
                ? this
                : LinMatrixEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return valueFilter(Scalar)
                ? this
                : LinMatrixEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetTranspose()
        {
            return new LinMatrixSingleScalarDenseStorage<T>(Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, GradeIndexPairRecord> indexToGradeIndexMapping)
        {
            var (grade, index1, index2) = 
                indexToGradeIndexMapping(0UL, 0UL);

            ILinMatrixStorage<T> vectorStorage = 
                index1 == 0 && index2 == 0
                    ? new LinMatrixSingleScalarDenseStorage<T>(Scalar) 
                    : new LinMatrixSingleScalarSparseStorage<T>(index1, index2, Scalar);

            return new LinMatrixSingleGradeStorage<T>(grade, vectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, T, GradeIndexPairScalarRecord<T>> indexScalarToGradeIndexScalarMapping)
        {
            var (grade, index1, index2, scalar) = 
                indexScalarToGradeIndexScalarMapping(0UL, 0UL, Scalar);

            ILinMatrixStorage<T> vectorStorage = 
                index1 == 0 && index2 == 0
                    ? new LinMatrixSingleScalarDenseStorage<T>(scalar) 
                    : new LinMatrixSingleScalarSparseStorage<T>(index1, index2, scalar);

            return new LinMatrixSingleGradeStorage<T>(grade, vectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetRow(ulong index1)
        {
            return index1 == 0
                ? LinVectorEmptyStorage<T>.EmptyStorage
                : new LinVectorSingleScalarDenseStorage<T>(Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetColumn(ulong index2)
        {
            return index2 == 0
                ? LinVectorEmptyStorage<T>.EmptyStorage
                : new LinVectorSingleScalarDenseStorage<T>(Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexLinVectorStorageRecord<T>> GetRows()
        {
            yield return new IndexLinVectorStorageRecord<T>(
                0,
                new LinVectorSingleScalarDenseStorage<T>(Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexLinVectorStorageRecord<T>> GetColumns()
        {
            yield return new IndexLinVectorStorageRecord<T>(
                0,
                new LinVectorSingleScalarDenseStorage<T>(Scalar)
            );
        }

        public IEnumerable<IndexLinVectorStorageRecord<T>> GetColumns(Func<ulong, bool> columnIndexFilter)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> CombineRows(IReadOnlyList<T> scalarList, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            return scalingFunc(scalarList[0], new LinVectorSingleScalarDenseStorage<T>(Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> CombineRows(IEnumerable<IndexScalarRecord<T>> rowIndexScalarRecords, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            ILinVectorStorage<T> vector = null;
            var rowVector = new LinVectorSingleScalarDenseStorage<T>(Scalar);

            foreach (var (rowIndex, scalingFactor) in rowIndexScalarRecords)
            {
                if (rowIndex != 0) continue;

                var scaledVector = scalingFunc(scalingFactor, rowVector);

                vector = vector is null
                    ? scaledVector
                    : reducingFunc(vector, scaledVector);
            }

            return vector ?? LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> CombineColumns(IReadOnlyList<T> scalarList, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            return scalingFunc(scalarList[0], new LinVectorSingleScalarDenseStorage<T>(Scalar));
        }

        public ILinVectorStorage<T> CombineColumns(IEnumerable<IndexScalarRecord<T>> columnIndexScalarRecords, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            ILinVectorStorage<T> vector = null;
            var columnVector = new LinVectorSingleScalarDenseStorage<T>(Scalar);

            foreach (var (columnIndex, scalingFactor) in columnIndexScalarRecords)
            {
                if (columnIndex != 0) continue;

                var scaledVector = scalingFunc(scalingFactor, columnVector);

                vector = vector is null
                    ? scaledVector
                    : reducingFunc(vector, scaledVector);
            }

            return vector ?? LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILinMatrixStorage<T> matrixStorage)
        {
            matrixStorage = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairScalarRecord<T>> GetIndexScalarRecords()
        {
            yield return new IndexPairScalarRecord<T>(0UL, 0UL, Scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixDenseStorage<T> GetDensePermutation(Func<ulong, ulong, IndexPairRecord> indexMapping)
        {
            var (index1, index2) = 
                indexMapping(0, 0);

            return index1 == 0 && index2 == 0
                ? this
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixDenseStorage<T> GetDensePermutation(Func<IndexPairRecord, IndexPairRecord> indexMapping)
        {
            var (index1, index2) = 
                indexMapping(GaFuLRecordsFactory.ZeroIndexPairRecord);

            return index1 == 0 && index2 == 0
                ? this
                : throw new InvalidOperationException();
        }

        public IEnumerable<IndexLinVectorStorageRecord<T>> GetDenseColumns(IEnumerable<ulong> columnIndexList)
        {
            throw new NotImplementedException();
        }
    }
}