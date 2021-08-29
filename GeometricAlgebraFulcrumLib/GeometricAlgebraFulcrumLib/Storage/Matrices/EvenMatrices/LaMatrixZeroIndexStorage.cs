using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices
{
    public sealed record LaMatrixZeroIndexStorage<T> :
        ILaMatrixSingleIndexEvenStorage<T>,
        ILaMatrixMutableDenseEvenStorage<T>
    {
        public ulong Index1 
            => 0UL;

        public ulong Index2 
            => 0UL;

        public IndexPairRecord Index 
            => GaRecordsFactory.ZeroKeyPair;

        public T Scalar { get; set; }

        public int Count1 
            => 1;

        public int Count2 
            => 1;

        public int Count 
            => 1;

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

        internal LaMatrixZeroIndexStorage([NotNull] T value)
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
            yield return GaRecordsFactory.ZeroKeyPair;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            yield return Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(IndexPairRecord index)
        {
            return index == GaRecordsFactory.ZeroKeyPair
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
            return index == GaRecordsFactory.ZeroKeyPair;
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
            return GaRecordsFactory.ZeroKeyPair;
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
            return GaRecordsFactory.ZeroKeyPair;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(IndexPairRecord index, out T value)
        {
            if (index == GaRecordsFactory.ZeroKeyPair)
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
        public IEnumerable<IndexPairRecord> GetEmptyIndices(ulong maxKey1, ulong maxKey2)
        {
            return new IndexPairRecord(maxKey1, maxKey2)
                .GetKeyPairsInRange()
                .Where(index => index != GaRecordsFactory.ZeroKeyPair);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairRecord> GetEmptyIndices(IndexPairRecord maxKey)
        {
            return maxKey
                .GetKeyPairsInRange()
                .Where(index => index != GaRecordsFactory.ZeroKeyPair);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> GetCopy()
        {
            return new LaMatrixSingleIndexStorage<T>(GaRecordsFactory.ZeroKeyPair, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> MapIndices(Func<ulong, ulong, IndexPairRecord> indexMapping)
        {
            return new LaMatrixSingleIndexStorage<T>(indexMapping(0UL, 0UL), Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LaMatrixZeroIndexStorage<T2>(
                valueMapping(Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexValueMapping)
        {
            return new LaMatrixZeroIndexStorage<T2>(
                indexValueMapping(0UL, 0UL, Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            return indexFilter(0UL, 0UL)
                ? this
                : LaMatrixEmptyStorage<T>.EmptyMatrix;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexValueFilter)
        {
            return indexValueFilter(0UL, 0UL, Scalar)
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
            return new LaMatrixZeroIndexStorage<T>(Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T> ToGradedStorage(Func<ulong, ulong, GradeIndexPairRecord> evenKeyToGradeKeyMapping)
        {
            var (grade, index1, index2) = 
                evenKeyToGradeKeyMapping(0UL, 0UL);

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
                evenIndexScalarToGradeIndexScalarMapping(0UL, 0UL, Scalar);

            ILaMatrixEvenStorage<T> evenList = 
                index1 == 0 && index2 == 0
                    ? new LaMatrixZeroIndexStorage<T>(scalar) 
                    : new LaMatrixSingleIndexStorage<T>(index1, index2, scalar);

            return new LaMatrixSingleGradeStorage<T>(grade, evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> GetRow(ulong index1)
        {
            return index1 == 0
                ? LaVectorEmptyStorage<T>.ZeroStorage
                : new LaVectorZeroIndexStorage<T>(Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> GetColumn(ulong index2)
        {
            return index2 == 0
                ? LaVectorEmptyStorage<T>.ZeroStorage
                : new LaVectorZeroIndexStorage<T>(Scalar);
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
            yield return new IndexPairScalarRecord<T>(0UL, 0UL, Scalar);
        }

    }
}