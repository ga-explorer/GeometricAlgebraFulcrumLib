using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    /// <summary>
    /// A base class for dense lists
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class LaVectorDenseStorageBase<T> :
        ILaVectorEvenStorage<T>
    {
        public abstract int Count { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return Count;
        }

        public abstract T GetScalar(ulong index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices()
        {
            return ((ulong) GetSparseCount()).GetRange();
        }

        public virtual IEnumerable<T> GetScalars()
        {
            return ((ulong) GetSparseCount()).MapRange(GetScalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return GetSparseCount() == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index)
        {
            return index < (ulong) GetSparseCount();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(ulong index, out T value)
        {
            if (ContainsIndex(index))
            {
                value = GetScalar(index);
                return true;
            }

            value = default;
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

        public abstract ILaVectorEvenStorage<T> GetCopy();

        public ILaVectorEvenStorage<T> MapIndices(Func<ulong, ulong> indexMapping)
        {
            var count = (ulong) GetSparseCount();
            var valueDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < count; index++)
            {
                valueDictionary.Add(
                    indexMapping(index), 
                    GetScalar(index)
                );
            }

            return valueDictionary.CreateLaVectorStorage();
        }

        public ILaVectorEvenStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            var count = (ulong) GetSparseCount();
            var valuesList = new T2[count];

            for (var i = 0UL; i < count; i++)
                valuesList[i] = valueMapping(GetScalar(i));

            return valuesList.CreateLaVectorDenseStorage();
        }

        public ILaVectorEvenStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            var count = (ulong) GetSparseCount();
            var valuesList = new T2[count];

            for (var i = 0UL; i < count; i++)
                valuesList[i] = indexValueMapping(i, GetScalar(i));

            return valuesList.CreateLaVectorDenseStorage();
        }

        public ILaVectorEvenStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            var count = (ulong) GetSparseCount();
            var valueDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < count; index++)
            {
                if (indexFilter(index))
                    valueDictionary.Add(index, GetScalar(index));
            }

            return valueDictionary.CreateLaVectorStorage();
        }

        public ILaVectorEvenStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            var count = (ulong) GetSparseCount();
            var valueDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < count; index++)
            {
                var value = GetScalar(index);

                if (indexValueFilter(index, value))
                    valueDictionary.Add(index, value);
            }

            return valueDictionary.CreateLaVectorStorage();
        }

        public ILaVectorEvenStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            var count = (ulong) GetSparseCount();
            var valueDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < count; index++)
            {
                var value = GetScalar(index);

                if (valueFilter(value))
                    valueDictionary.Add(index, value);
            }

            return valueDictionary.CreateLaVectorStorage();
        }

        public ILaVectorGradedStorage<T> ToGradedStorage(Func<ulong, GradeIndexRecord> evenKeyToGradeKeyMapping)
        {
            var count = (ulong) GetSparseCount();
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            for (var id = 0UL; id < count; id++)
            {
                var value = GetScalar(id);
                var (grade, index) = evenKeyToGradeKeyMapping(id);

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var indexValueDictionary))
                {
                    indexValueDictionary = new Dictionary<ulong, T>();
                    gradeKeyValueDictionary.Add(grade, indexValueDictionary);
                }

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = value;
                else
                    indexValueDictionary.Add(index, value);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorGradedStorage<T> ToGradedStorage(Func<ulong, T, GradeIndexScalarRecord<T>> evenIndexScalarToGradeIndexScalarMapping)
        {
            var count = (ulong) GetSparseCount();
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            for (var id = 0UL; id < count; id++)
            {
                var (grade, index, scalar) = evenIndexScalarToGradeIndexScalarMapping(id, GetScalar(id));

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

        public bool TryGetCompactStorage(out ILaVectorEvenStorage<T> evenList)
        {
            if (Count > 1)
            {
                evenList = this;
                return false;
            }

            evenList = 
                Count == 0
                    ? LaVectorEmptyStorage<T>.ZeroStorage
                    : new LaVectorZeroIndexStorage<T>(GetScalar(0UL));

            return true;
        }

        public IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords()
        {
            var count = (ulong) GetSparseCount();

            for (var index = 0UL; index < count; index++)
                yield return new IndexScalarRecord<T>(index, GetScalar(index));
        }
    }
}