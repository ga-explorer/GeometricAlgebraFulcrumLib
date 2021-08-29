using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public sealed record LaVectorSingleIndexStorage<T> :
        ILaVectorSingleIndexEvenStorage<T>
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


        internal LaVectorSingleIndexStorage(ulong index, [NotNull] T value)
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
        public IEnumerable<ulong> GetEmptyIndices(ulong maxKey)
        {
            for (var index = 0UL; index < Index; index++)
                yield return index;

            for (var index = Index + 1; index <= maxKey; index++)
                yield return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> GetCopy()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> MapIndices(Func<ulong, ulong> indexMapping)
        {
            var index = indexMapping(Index);

            return index == 0UL
                ? new LaVectorZeroIndexStorage<T>(Scalar)
                : new LaVectorSingleIndexStorage<T>(index, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LaVectorSingleIndexStorage<T2>(Index, valueMapping(Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            return new LaVectorSingleIndexStorage<T2>(Index, indexValueMapping(Index, Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            return indexFilter(Index)
                ? this
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            return indexValueFilter(Index, Scalar)
                ? this
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return valueFilter(Scalar)
                ? this
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T> ToGradedStorage(Func<ulong, GradeIndexRecord> evenKeyToGradeKeyMapping)
        {
            var (grade, index) = evenKeyToGradeKeyMapping(Index);

            ILaVectorEvenStorage<T> evenDictionary = 
                index == 0
                    ? new LaVectorZeroIndexStorage<T>(Scalar) 
                    : new LaVectorSingleIndexStorage<T>(index, Scalar);

            return new LaVectorSingleGradeStorage<T>(grade, evenDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T> ToGradedStorage(Func<ulong, T, GradeIndexScalarRecord<T>> evenIndexScalarToGradeIndexScalarMapping)
        {
            var (grade, index, scalar) = evenIndexScalarToGradeIndexScalarMapping(Index, Scalar);

            ILaVectorEvenStorage<T> evenDictionary = 
                index == 0
                    ? new LaVectorZeroIndexStorage<T>(scalar) 
                    : new LaVectorSingleIndexStorage<T>(index, scalar);

            return new LaVectorSingleGradeStorage<T>(grade, evenDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILaVectorEvenStorage<T> evenList)
        {
            if (Index > 0)
            {
                evenList = this;
                return false;
            }

            evenList = new LaVectorZeroIndexStorage<T>(Scalar);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords()
        {
            yield return new IndexScalarRecord<T>(Index, Scalar);
        }
    }
}