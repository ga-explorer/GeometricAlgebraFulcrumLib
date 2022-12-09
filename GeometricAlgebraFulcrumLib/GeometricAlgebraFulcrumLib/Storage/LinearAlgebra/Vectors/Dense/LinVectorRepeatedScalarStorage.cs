using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense
{
    public sealed class LinVectorRepeatedScalarStorage<T> :
        ILinVectorDenseStorage<T>
    {
        public T Scalar { get; set; }

        public int Count { get; }

        public T this[int index] 
            => GetScalar((ulong) index);

        public T this[ulong index] 
            => GetScalar(index);


        internal LinVectorRepeatedScalarStorage(int count, [NotNull] T value)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Count = count;
            Scalar = value;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(ulong index)
        {
            return index < (ulong) Count
                ? Scalar
                : throw new KeyNotFoundException(nameof(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices()
        {
            return ((ulong) GetSparseCount()).GetRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return Enumerable.Repeat(Scalar, GetSparseCount());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index)
        {
            return index < (ulong) GetSparseCount();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(ulong index, out T value)
        {
            if (index < (ulong) GetSparseCount())
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
            return (ulong) (GetSparseCount() - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetEmptyIndices(ulong maxCount)
        {
            var count = (ulong) GetSparseCount();

            return maxCount <= count
                ? Enumerable.Empty<ulong>()
                : (maxCount - count).GetRange(count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetCopy()
        {
            return new LinVectorRepeatedScalarStorage<T>(GetSparseCount(), Scalar);
        }

        public ILinVectorStorage<T> GetPermutation(Func<ulong, ulong> indexMapping)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < (ulong) GetSparseCount(); index++)
                valueDictionary.Add(indexMapping(index), Scalar);

            return valueDictionary.CreateLinVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LinVectorRepeatedScalarStorage<T2>(
                GetSparseCount(), 
                valueMapping(Scalar)
            );
        }

        public ILinVectorStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            var valuesList = new T2[GetSparseCount()];

            for (var i = 0; i < GetSparseCount(); i++)
                valuesList[i] = indexValueMapping((ulong) i, Scalar);

            return new LinVectorListStorage<T2>(valuesList);
        }

        public ILinVectorStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < (ulong) GetSparseCount(); index++)
                if (indexFilter(index))
                    valueDictionary.Add(index, Scalar);

            return valueDictionary.CreateLinVectorStorage();
        }

        public ILinVectorStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < (ulong) GetSparseCount(); index++)
                if (indexValueFilter(index, Scalar))
                    valueDictionary.Add(index, Scalar);

            return new LinVectorSparseStorage<T>(valueDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return valueFilter(Scalar)
                ? this 
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public ILinVectorGradedStorage<T> ToVectorGradedStorage(Func<ulong, GradeIndexRecord> indexToGradeIndexMapping)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            for (var id = 0UL; id < (ulong) GetSparseCount(); id++)
            {
                var (grade, index) = indexToGradeIndexMapping(id);

                if (!gradeIndexScalarDictionary.TryGetValue(grade, out var indexValueDictionary))
                {
                    indexValueDictionary = new Dictionary<ulong, T>();
                    gradeIndexScalarDictionary.Add(grade, indexValueDictionary);
                }

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = Scalar;
                else
                    indexValueDictionary.Add(index, Scalar);
            }

            return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
        }

        public ILinVectorGradedStorage<T> ToVectorGradedStorage(Func<ulong, T, GradeIndexScalarRecord<T>> indexScalarToGradeIndexScalarMapping)
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            for (var id = 0UL; id < (ulong) GetSparseCount(); id++)
            {
                var (grade, index, scalar) = indexScalarToGradeIndexScalarMapping(id, Scalar);

                if (!gradeIndexScalarDictionary.TryGetValue(grade, out var indexValueDictionary))
                {
                    indexValueDictionary = new Dictionary<ulong, T>();
                    gradeIndexScalarDictionary.Add(grade, indexValueDictionary);
                }

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = scalar;
                else
                    indexValueDictionary.Add(index, scalar);
            }

            return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILinVectorStorage<T> vectorStorage)
        {
            if (Count == 0)
            {
                vectorStorage = LinVectorEmptyStorage<T>.EmptyStorage;
                return true;
            }

            if (Count == 1)
            {
                vectorStorage = new LinVectorSingleScalarDenseStorage<T>(Scalar);
                return true;
            }

            vectorStorage = this;
            return false;
        }

        public IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords()
        {
            var count = (ulong) GetSparseCount();

            for (var index = 0UL; index < count; index++)
                yield return new IndexScalarRecord<T>(index, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorDenseStorage<T> GetDensePermutation(Func<ulong, ulong> indexMapping)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<T> GetEnumerator()
        {
            return Enumerable.Repeat(Scalar, Count).GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}