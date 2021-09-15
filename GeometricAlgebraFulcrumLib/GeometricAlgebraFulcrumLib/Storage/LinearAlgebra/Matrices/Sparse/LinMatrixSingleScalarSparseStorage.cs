using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Sparse
{
    public sealed record LinMatrixSingleScalarSparseStorage<T> :
        ILinMatrixSingleScalarStorage<T>,
        ILinMatrixSparseStorage<T>
    {
        public ulong Index2 
            => Index.Index2;

        public ulong Index1 
            => Index.Index1;

        public IndexPairRecord Index { get; }

        public T Scalar { get; set; }

        public int Count1 
            => 1;

        public int Count2 
            => 1;

        public int Count 
            => 1;


        internal LinMatrixSingleScalarSparseStorage([NotNull] IndexPairRecord indexPair, T value)
        {
            Debug.Assert(indexPair.Index1 > 0 || indexPair.Index2 > 0);

            Index = indexPair;
            Scalar = value;
        }

        internal LinMatrixSingleScalarSparseStorage(ulong index1, ulong index2, T value)
        {
            Index = new IndexPairRecord(index1, index2);
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
        public T GetScalar(IndexPairRecord index)
        {
            return index == Index
                ? Scalar
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(ulong index1, ulong index2)
        {
            return index1 == Index.Index1 && index2 == Index.Index2
                ? Scalar
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices1()
        {
            yield return Index.Index1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices2()
        {
            yield return Index.Index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairRecord> GetIndices()
        {
            yield return Index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            yield return Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index1, ulong index2)
        {
            return index1 == Index.Index1 && 
                   index2 == Index.Index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(IndexPairRecord index)
        {
            return Index == index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex1()
        {
            return Index.Index1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex2()
        {
            return Index.Index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexPairRecord GetMinIndex()
        {
            return Index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex1()
        {
            return Index.Index1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex2()
        {
            return Index.Index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexPairRecord GetMaxIndex()
        {
            return Index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(IndexPairRecord index, out T value)
        {
            if (index == Index)
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
            if (index1 == Index.Index1 && index2 == Index.Index2)
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
                .Where(index => index != Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairRecord> GetEmptyIndices(IndexPairRecord maxCountPair)
        {
            return maxCountPair
                .GetIndexPairsInRange()
                .Where(index => index != Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetCopy()
        {
            return new LinMatrixSingleScalarSparseStorage<T>(Index, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetPermutation(Func<ulong, ulong, IndexPairRecord> indexMapping)
        {
            return new LinMatrixSingleScalarSparseStorage<T>(indexMapping(Index.Index1, Index.Index2), Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetPermutation(Func<IndexPairRecord, IndexPairRecord> indexMapping)
        {
            return Scalar.CreateLinMatrixSingleScalarStorage(indexMapping(Index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LinMatrixSingleScalarSparseStorage<T2>(Index, valueMapping(Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexValueMapping)
        {
            return new LinMatrixSingleScalarSparseStorage<T2>(Index, indexValueMapping(Index.Index1, Index.Index2, Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            return indexFilter(Index.Index1, Index.Index2)
                ? this
                : LinMatrixEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexValueFilter)
        {
            return indexValueFilter(Index.Index1, Index.Index2, Scalar)
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
            return new LinMatrixSingleScalarSparseStorage<T>(Index.Transpose(), Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, GradeIndexPairRecord> indexToGradeIndexMapping)
        {
            var (grade, index1, index2) = 
                indexToGradeIndexMapping(Index.Index1, Index.Index2);

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
                indexScalarToGradeIndexScalarMapping(Index.Index1, Index.Index2, Scalar);

            ILinMatrixStorage<T> vectorStorage = 
                index1 == 0 && index2 == 0
                    ? new LinMatrixSingleScalarDenseStorage<T>(scalar) 
                    : new LinMatrixSingleScalarSparseStorage<T>(index1, index2, scalar);

            return new LinMatrixSingleGradeStorage<T>(grade, vectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetRow(ulong index1)
        {
            return index1 == Index1
                ? LinVectorEmptyStorage<T>.EmptyStorage
                : new LinVectorSingleScalarSparseStorage<T>(index1, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetColumn(ulong index2)
        {
            return index2 == Index2
                ? LinVectorEmptyStorage<T>.EmptyStorage
                : new LinVectorSingleScalarSparseStorage<T>(index2, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexLinVectorStorageRecord<T>> GetRows()
        {
            yield return new IndexLinVectorStorageRecord<T>(
                Index1,
                new LinVectorSingleScalarSparseStorage<T>(Index1, Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexLinVectorStorageRecord<T>> GetRows(Func<ulong, bool> rowIndexFilter)
        {
            if (rowIndexFilter(Index1))
                yield return new IndexLinVectorStorageRecord<T>(
                    Index1,
                    new LinVectorSingleScalarSparseStorage<T>(Index1, Scalar)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexLinVectorStorageRecord<T>> GetColumns()
        {
            yield return new IndexLinVectorStorageRecord<T>(
                Index2,
                new LinVectorSingleScalarSparseStorage<T>(Index2, Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexLinVectorStorageRecord<T>> GetColumns(Func<ulong, bool> columnIndexFilter)
        {
            if (columnIndexFilter(Index2))
                yield return new IndexLinVectorStorageRecord<T>(
                    Index2,
                    new LinVectorSingleScalarSparseStorage<T>(Index2, Scalar)
                );
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
            yield return new IndexPairScalarRecord<T>(Index.Index1, Index.Index2, Scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> CombineRows(IReadOnlyList<T> scalarList, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            return Index1 < (ulong) scalarList.Count
                ? scalingFunc(scalarList[(int) Index1], Scalar.CreateLinVectorSingleScalarStorage(Index2))
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public ILinVectorStorage<T> CombineRows(IEnumerable<IndexScalarRecord<T>> rowIndexScalarRecords, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            ILinVectorStorage<T> vector = null;
            var rowVector = Scalar.CreateLinVectorSingleScalarStorage(Index2);

            foreach (var (rowIndex, scalingFactor) in rowIndexScalarRecords)
            {
                if (rowIndex != Index1) continue;

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
            return Index2 < (ulong) scalarList.Count
                ? scalingFunc(scalarList[(int) Index2], Scalar.CreateLinVectorSingleScalarStorage(Index1))
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public ILinVectorStorage<T> CombineColumns(IEnumerable<IndexScalarRecord<T>> columnIndexScalarRecords, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            ILinVectorStorage<T> vector = null;
            var columnVector = Scalar.CreateLinVectorSingleScalarStorage(Index1);

            foreach (var (columnIndex, scalingFactor) in columnIndexScalarRecords)
            {
                if (columnIndex != Index2) continue;

                var scaledVector = scalingFunc(scalingFactor, columnVector);

                vector = vector is null
                    ? scaledVector
                    : reducingFunc(vector, scaledVector);
            }

            return vector ?? LinVectorEmptyStorage<T>.EmptyStorage;
        }
    }
}
