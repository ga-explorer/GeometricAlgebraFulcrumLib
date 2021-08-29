using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices
{
    public sealed record LaMatrixSingleIndexStorage<T> :
        ILaMatrixSingleIndexEvenStorage<T>
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


        internal LaMatrixSingleIndexStorage([NotNull] IndexPairRecord indexPair, T value)
        {
            Debug.Assert(indexPair.Index1 > 0 || indexPair.Index2 > 0);

            Index = indexPair;
            Scalar = value;
        }

        internal LaMatrixSingleIndexStorage(ulong index1, ulong index2, T value)
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
        public IEnumerable<IndexPairRecord> GetEmptyIndices(ulong maxKey1, ulong maxKey2)
        {
            return new IndexPairRecord(maxKey1, maxKey2)
                .GetKeyPairsInRange()
                .Where(index => index != Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairRecord> GetEmptyIndices(IndexPairRecord maxKey)
        {
            return maxKey
                .GetKeyPairsInRange()
                .Where(index => index != Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> GetCopy()
        {
            return new LaMatrixSingleIndexStorage<T>(Index, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> MapIndices(Func<ulong, ulong, IndexPairRecord> indexMapping)
        {
            return new LaMatrixSingleIndexStorage<T>(indexMapping(Index.Index1, Index.Index2), Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LaMatrixSingleIndexStorage<T2>(Index, valueMapping(Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexValueMapping)
        {
            return new LaMatrixSingleIndexStorage<T2>(Index, indexValueMapping(Index.Index1, Index.Index2, Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            return indexFilter(Index.Index1, Index.Index2)
                ? this
                : LaMatrixEmptyStorage<T>.EmptyMatrix;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexValueFilter)
        {
            return indexValueFilter(Index.Index1, Index.Index2, Scalar)
                ? this
                : LaMatrixEmptyStorage<T>.EmptyMatrix;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return valueFilter(Scalar)
                ? this
                : LaMatrixEmptyStorage<T>.EmptyMatrix;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> Transpose()
        {
            return new LaMatrixSingleIndexStorage<T>(Index.SwapKeys(), Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T> ToGradedStorage(Func<ulong, ulong, GradeIndexPairRecord> evenKeyToGradeKeyMapping)
        {
            var (grade, index1, index2) = 
                evenKeyToGradeKeyMapping(Index.Index1, Index.Index2);

            ILaMatrixEvenStorage<T> evenList = 
                index1 == 0 && index2 == 0
                    ? new LaMatrixZeroIndexStorage<T>(Scalar) 
                    : new LaMatrixSingleIndexStorage<T>(index1, index2, Scalar);

            return new LaMatrixSingleGradeStorage<T>(grade, evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T> ToGradedStorage(Func<ulong, ulong, T, GradeIndexPairScalarRecord<T>> evenIndexScalarToGradeIndexScalarMapping)
        {
            var (grade, index1, index2, scalar) = 
                evenIndexScalarToGradeIndexScalarMapping(Index.Index1, Index.Index2, Scalar);

            ILaMatrixEvenStorage<T> evenList = 
                index1 == 0 && index2 == 0
                    ? new LaMatrixZeroIndexStorage<T>(scalar) 
                    : new LaMatrixSingleIndexStorage<T>(index1, index2, scalar);

            return new LaMatrixSingleGradeStorage<T>(grade, evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> GetRow(ulong index1)
        {
            return index1 == Index1
                ? LaVectorEmptyStorage<T>.ZeroStorage
                : new LaVectorSingleIndexStorage<T>(index1, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> GetColumn(ulong index2)
        {
            return index2 == Index2
                ? LaVectorEmptyStorage<T>.ZeroStorage
                : new LaVectorSingleIndexStorage<T>(index2, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILaMatrixEvenStorage<T> evenGrid)
        {
            evenGrid = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairScalarRecord<T>> GetIndexScalarRecords()
        {
            yield return new IndexPairScalarRecord<T>(Index.Index1, Index.Index2, Scalar);
        }
    }
}
