using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors
{
    public sealed record LinVectorEmptyStorage<T> :
        ILinVectorDenseStorage<T>,
        ILinVectorSparseStorage<T>
    {
        public static LinVectorEmptyStorage<T> EmptyStorage { get; }
            = new LinVectorEmptyStorage<T>();


        public int Count 
            => 0;
        
        public T this[int index] 
            => throw new IndexOutOfRangeException();


        private LinVectorEmptyStorage()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(ulong index, out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetEmptyIndices(ulong maxCount)
        {
            return maxCount.GetRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetCopy()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetPermutation(Func<ulong, ulong> indexMapping)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return LinVectorEmptyStorage<T2>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            return LinVectorEmptyStorage<T2>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return EmptyStorage;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> ToVectorGradedStorage(
            Func<ulong, RGaGradeKvIndexRecord> indexToGradeIndexMapping)
        {
            return LinVectorEmptyGradedStorage<T>.EmptyStorage;
        }

        public ILinVectorGradedStorage<T> ToVectorGradedStorage(Func<ulong, T, RGaGradeKvIndexScalarRecord<T>> indexScalarToGradeIndexScalarMapping)
        {
            return LinVectorEmptyGradedStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILinVectorStorage<T> vectorStorage)
        {
            vectorStorage = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexScalarRecord<T>> GetIndexScalarRecords()
        {
            return Enumerable.Empty<RGaKvIndexScalarRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorDenseStorage<T> GetDensePermutation(Func<ulong, ulong> indexMapping)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(ulong index)
        {
            throw new KeyNotFoundException(nameof(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices()
        {
            yield break;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            yield break;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Enumerable.Empty<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}