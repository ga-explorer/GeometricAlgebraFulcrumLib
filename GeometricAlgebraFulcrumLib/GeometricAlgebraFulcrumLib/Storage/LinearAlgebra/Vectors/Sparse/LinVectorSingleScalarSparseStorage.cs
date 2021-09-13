using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse
{
    public sealed record LinVectorSingleScalarSparseStorage<T> :
        ILinVectorSingleScalarStorage<T>,
        ILinVectorSparseStorage<T>
    {
        public ulong Index { get; }

        public T Scalar { get; set; }

        public int Count 
            => 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(ulong index) =>
            index == Index
                ? Scalar
                : default;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices()
        {
            yield return Index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            yield return Scalar;
        }


        internal LinVectorSingleScalarSparseStorage(ulong index, [NotNull] T value)
        {
            Debug.Assert(index > 0);

            Index = index;
            Scalar = value;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index)
        {
            return Index == index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(ulong index, out T value)
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
        public bool IsEmpty()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex()
        {
            return Index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex()
        {
            return Index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetEmptyIndices(ulong maxCount)
        {
            for (var index = 0UL; index < Index; index++)
                yield return index;

            for (var index = Index + 1; index < maxCount; index++)
                yield return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetCopy()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetPermutation(Func<ulong, ulong> indexMapping)
        {
            var index = indexMapping(Index);

            return index == 0UL
                ? new LinVectorSingleScalarDenseStorage<T>(Scalar)
                : new LinVectorSingleScalarSparseStorage<T>(index, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LinVectorSingleScalarSparseStorage<T2>(Index, valueMapping(Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            return new LinVectorSingleScalarSparseStorage<T2>(Index, indexValueMapping(Index, Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            return indexFilter(Index)
                ? this
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            return indexValueFilter(Index, Scalar)
                ? this
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return valueFilter(Scalar)
                ? this
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> ToVectorGradedStorage(Func<ulong, GradeIndexRecord> indexToGradeIndexMapping)
        {
            var (grade, index) = indexToGradeIndexMapping(Index);

            ILinVectorStorage<T> vectorStorage = 
                index == 0
                    ? new LinVectorSingleScalarDenseStorage<T>(Scalar) 
                    : new LinVectorSingleScalarSparseStorage<T>(index, Scalar);

            return new LinVectorSingleGradeStorage<T>(grade, vectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> ToVectorGradedStorage(Func<ulong, T, GradeIndexScalarRecord<T>> indexScalarToGradeIndexScalarMapping)
        {
            var (grade, index, scalar) = indexScalarToGradeIndexScalarMapping(Index, Scalar);

            ILinVectorStorage<T> vectorStorage = 
                index == 0
                    ? new LinVectorSingleScalarDenseStorage<T>(scalar) 
                    : new LinVectorSingleScalarSparseStorage<T>(index, scalar);

            return new LinVectorSingleGradeStorage<T>(grade, vectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILinVectorStorage<T> vectorStorage)
        {
            if (Index > 0)
            {
                vectorStorage = this;
                return false;
            }

            vectorStorage = new LinVectorSingleScalarDenseStorage<T>(Scalar);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords()
        {
            yield return new IndexScalarRecord<T>(Index, Scalar);
        }
    }
}