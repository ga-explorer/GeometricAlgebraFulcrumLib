using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense
{
    /// <summary>
    /// A base class for dense lists
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class LinVectorDenseStorageBase<T> :
        ILinVectorStorage<T>
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
            return ((ulong) Count).GetRange();
        }

        public virtual IEnumerable<T> GetScalars()
        {
            return ((ulong) Count).MapRange(GetScalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index)
        {
            return index < (ulong) Count;
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
            return (ulong) (Count - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetEmptyIndices(ulong maxCount)
        {
            var count = (ulong) Count;

            return maxCount <= count
                ? Enumerable.Empty<ulong>()
                : (maxCount - count).GetRange(count);
        }

        public abstract ILinVectorStorage<T> GetCopy();

        public ILinVectorStorage<T> GetPermutation(Func<ulong, ulong> indexMapping)
        {
            var count = (ulong) Count;
            var valueDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < count; index++)
            {
                valueDictionary.Add(
                    indexMapping(index), 
                    GetScalar(index)
                );
            }

            return valueDictionary.CreateLinVectorStorage();
        }

        public ILinVectorStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            var count = (ulong) Count;
            var valuesList = new T2[count];

            for (var i = 0UL; i < count; i++)
                valuesList[i] = valueMapping(GetScalar(i));

            return valuesList.CreateLinVectorDenseStorage();
        }

        public ILinVectorStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            var count = (ulong) Count;
            var valuesList = new T2[count];

            for (var i = 0UL; i < count; i++)
                valuesList[i] = indexValueMapping(i, GetScalar(i));

            return valuesList.CreateLinVectorDenseStorage();
        }

        public ILinVectorStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            var count = (ulong) Count;
            var valueDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < count; index++)
            {
                if (indexFilter(index))
                    valueDictionary.Add(index, GetScalar(index));
            }

            return valueDictionary.CreateLinVectorStorage();
        }

        public ILinVectorStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            var count = (ulong) Count;
            var valueDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < count; index++)
            {
                var value = GetScalar(index);

                if (indexValueFilter(index, value))
                    valueDictionary.Add(index, value);
            }

            return valueDictionary.CreateLinVectorStorage();
        }

        public ILinVectorStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            var count = (ulong) Count;
            var valueDictionary = new Dictionary<ulong, T>();

            for (var index = 0UL; index < count; index++)
            {
                var value = GetScalar(index);

                if (valueFilter(value))
                    valueDictionary.Add(index, value);
            }

            return valueDictionary.CreateLinVectorStorage();
        }

        public ILinVectorGradedStorage<T> ToVectorGradedStorage(Func<ulong, GradeIndexRecord> indexToGradeIndexMapping)
        {
            var count = (ulong) Count;
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            for (var id = 0UL; id < count; id++)
            {
                var value = GetScalar(id);
                var (grade, index) = indexToGradeIndexMapping(id);

                if (!gradeIndexScalarDictionary.TryGetValue(grade, out var indexValueDictionary))
                {
                    indexValueDictionary = new Dictionary<ulong, T>();
                    gradeIndexScalarDictionary.Add(grade, indexValueDictionary);
                }

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = value;
                else
                    indexValueDictionary.Add(index, value);
            }

            return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
        }

        public ILinVectorGradedStorage<T> ToVectorGradedStorage(Func<ulong, T, GradeIndexScalarRecord<T>> indexScalarToGradeIndexScalarMapping)
        {
            var count = (ulong) Count;
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            for (var id = 0UL; id < count; id++)
            {
                var (grade, index, scalar) = indexScalarToGradeIndexScalarMapping(id, GetScalar(id));

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

        public bool TryGetCompactStorage(out ILinVectorStorage<T> vectorStorage)
        {
            if (Count > 1)
            {
                vectorStorage = this;
                return false;
            }

            vectorStorage = 
                Count == 0
                    ? LinVectorEmptyStorage<T>.EmptyStorage
                    : new LinVectorSingleScalarDenseStorage<T>(GetScalar(0UL));

            return true;
        }

        public IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords()
        {
            var count = (ulong) Count;

            for (var index = 0UL; index < count; index++)
                yield return new IndexScalarRecord<T>(index, GetScalar(index));
        }
    }
}