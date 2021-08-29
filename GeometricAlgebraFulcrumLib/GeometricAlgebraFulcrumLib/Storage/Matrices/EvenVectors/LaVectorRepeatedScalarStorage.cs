using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public sealed class LaVectorRepeatedScalarStorage<T> :
        ILaVectorDenseEvenStorage<T>
    {
        public T Scalar { get; set; }

        public int Count { get; }
        
        public T this[int index] 
            => GetScalar((ulong) index);

        public T this[ulong index] 
            => GetScalar(index);


        internal LaVectorRepeatedScalarStorage(int count, [NotNull] T value)
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
        public IEnumerable<ulong> GetEmptyIndices(ulong maxKey)
        {
            var lastKey = (ulong) (GetSparseCount() - 1);

            return maxKey <= lastKey
                ? Enumerable.Empty<ulong>()
                : (maxKey - lastKey).GetRange(lastKey + 1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> GetCopy()
        {
            return new LaVectorRepeatedScalarStorage<T>(GetSparseCount(), Scalar);
        }

        public ILaVectorEvenStorage<T> MapIndices(Func<ulong, ulong> indexMapping)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < (ulong) GetSparseCount(); index++)
                valueDictionary.Add(indexMapping(index), Scalar);

            return valueDictionary.CreateLaVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LaVectorRepeatedScalarStorage<T2>(
                GetSparseCount(), 
                valueMapping(Scalar)
            );
        }

        public ILaVectorEvenStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            var valuesList = new T2[GetSparseCount()];

            for (var i = 0; i < GetSparseCount(); i++)
                valuesList[i] = indexValueMapping((ulong) i, Scalar);

            return new LaVectorListStorage<T2>(valuesList);
        }

        public ILaVectorEvenStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < (ulong) GetSparseCount(); index++)
                if (indexFilter(index))
                    valueDictionary.Add(index, Scalar);

            return valueDictionary.CreateLaVectorStorage();
        }

        public ILaVectorEvenStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < (ulong) GetSparseCount(); index++)
                if (indexValueFilter(index, Scalar))
                    valueDictionary.Add(index, Scalar);

            return new LaVectorSparseStorage<T>(valueDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return valueFilter(Scalar)
                ? this 
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        public ILaVectorGradedStorage<T> ToGradedStorage(Func<ulong, GradeIndexRecord> evenKeyToGradeKeyMapping)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            for (var id = 0UL; id < (ulong) GetSparseCount(); id++)
            {
                var (grade, index) = evenKeyToGradeKeyMapping(id);

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var indexValueDictionary))
                {
                    indexValueDictionary = new Dictionary<ulong, T>();
                    gradeKeyValueDictionary.Add(grade, indexValueDictionary);
                }

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = Scalar;
                else
                    indexValueDictionary.Add(index, Scalar);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorGradedStorage<T> ToGradedStorage(Func<ulong, T, GradeIndexScalarRecord<T>> evenIndexScalarToGradeIndexScalarMapping)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            for (var id = 0UL; id < (ulong) GetSparseCount(); id++)
            {
                var (grade, index, scalar) = evenIndexScalarToGradeIndexScalarMapping(id, Scalar);

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var indexValueDictionary))
                {
                    indexValueDictionary = new Dictionary<ulong, T>();
                    gradeKeyValueDictionary.Add(grade, indexValueDictionary);
                }

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = scalar;
                else
                    indexValueDictionary.Add(index, scalar);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILaVectorEvenStorage<T> evenList)
        {
            if (Count == 0)
            {
                evenList = LaVectorEmptyStorage<T>.ZeroStorage;
                return true;
            }

            if (Count == 1)
            {
                evenList = new LaVectorZeroIndexStorage<T>(Scalar);
                return true;
            }

            evenList = this;
            return false;
        }

        public IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords()
        {
            var count = (ulong) GetSparseCount();

            for (var index = 0UL; index < count; index++)
                yield return new IndexScalarRecord<T>(index, Scalar);
        }
    }
}