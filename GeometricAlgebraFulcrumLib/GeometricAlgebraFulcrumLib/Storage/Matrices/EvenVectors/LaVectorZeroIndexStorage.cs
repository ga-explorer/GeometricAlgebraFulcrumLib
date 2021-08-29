using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public sealed record LaVectorZeroIndexStorage<T> :
        ILaVectorSingleIndexEvenStorage<T>,
        ILaVectorMutableDenseEvenStorage<T>
    {
        public ulong Index 
            => 0UL;

        public T Scalar { get; set; }

        public int Count 
            => 1;

        public T this[int index]
        {
            get => index == 0 
                ? Scalar 
                : throw new KeyNotFoundException();
            set
            {
                if (index == 0) 
                    Scalar = value;
                else
                    throw new KeyNotFoundException();
            }
        }

        public T this[ulong index]
        {
            get => index == 0UL 
                ? Scalar 
                : throw new KeyNotFoundException();
            set
            {
                if (index == 0UL) 
                    Scalar = value;
                else
                    throw new KeyNotFoundException();
            }
        }


        internal LaVectorZeroIndexStorage([NotNull] T value)
        {
            Scalar = value;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(ulong index)
        {
            return index == 0UL
                ? Scalar
                : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices()
        {
            yield return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            yield return Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index)
        {
            return index == 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(ulong index, out T value)
        {
            if (index == 0UL)
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
            return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex()
        {
            return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetEmptyIndices(ulong maxKey)
        {
            return maxKey > 0
                ? (maxKey - 1).GetRange(1)
                : Enumerable.Empty<ulong>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> GetCopy()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> MapIndices(Func<ulong, ulong> indexMapping)
        {
            var index = indexMapping(0UL);

            return index == 0UL
                ? new LaVectorZeroIndexStorage<T>(Scalar)
                : new LaVectorSingleIndexStorage<T>(index, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LaVectorZeroIndexStorage<T2>(valueMapping(Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            return new LaVectorZeroIndexStorage<T2>(indexValueMapping(0UL, Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            return indexFilter(0UL)
                ? this : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            return indexValueFilter(0UL, Scalar)
                ? this : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return valueFilter(Scalar)
                ? this : LaVectorEmptyStorage<T>.ZeroStorage;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T> ToGradedStorage(Func<ulong, GradeIndexRecord> evenKeyToGradeKeyMapping)
        {
            var (grade, index) = evenKeyToGradeKeyMapping(0UL);

            ILaVectorEvenStorage<T> evenDictionary = 
                index == 0
                    ? new LaVectorZeroIndexStorage<T>(Scalar) 
                    : new LaVectorSingleIndexStorage<T>(index, Scalar);

            return new LaVectorSingleGradeStorage<T>(grade, evenDictionary);
        }

        public ILaVectorGradedStorage<T> ToGradedStorage(Func<ulong, T, GradeIndexScalarRecord<T>> evenIndexScalarToGradeIndexScalarMapping)
        {
            var (grade, index, scalar) = evenIndexScalarToGradeIndexScalarMapping(0UL, Scalar);

            ILaVectorEvenStorage<T> evenDictionary = 
                index == 0
                    ? new LaVectorZeroIndexStorage<T>(scalar) 
                    : new LaVectorSingleIndexStorage<T>(index, scalar);

            return new LaVectorSingleGradeStorage<T>(grade, evenDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILaVectorEvenStorage<T> evenList)
        {
            evenList = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords()
        {
            yield return new IndexScalarRecord<T>(0UL, Scalar);
        }
    }
}