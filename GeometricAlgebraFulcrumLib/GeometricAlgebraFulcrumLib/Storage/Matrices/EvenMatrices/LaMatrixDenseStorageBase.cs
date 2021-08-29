using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices
{
    public abstract class LaMatrixDenseStorageBase<T> :
        ILaMatrixEvenStorage<T>
    {
        public abstract int Count1 { get; }

        public abstract int Count2 { get; }

        public int Count 
            => Count1 * Count2;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return Count1 * Count2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return Count1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return Count2;
        }

        public abstract T GetScalar(ulong index1, ulong index2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(IndexPairRecord key)
        {
            return GetScalar(key.Index1, key.Index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices1()
        {
            return ((ulong) Count1).GetRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices2()
        {
            return ((ulong) Count2).GetRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairRecord> GetIndices()
        {
            return GaRecordsUtils.GetKeyPairsInRange(Count1, Count2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual IEnumerable<T> GetScalars()
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
                yield return GetScalar(k1, k2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return Count1 == 0 || Count2 == 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index1, ulong index2)
        {
            return index1 < (ulong) Count1 && 
                   index2 < (ulong) Count2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(IndexPairRecord key)
        {
            var (key1, key2) = key;

            return key1 < (ulong) Count1 && key2 < (ulong) Count2;
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
            return new IndexPairRecord(0, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex1()
        {
            return (ulong) (Count1 - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex2()
        {
            return (ulong) (Count2 - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IndexPairRecord GetMaxIndex()
        {
            return new IndexPairRecord((ulong) (Count1 - 1), (ulong) (Count2 - 1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(ulong index1, ulong index2, out T value)
        {
            if (index1 < (ulong) Count1 && index2 < (ulong) Count2)
            {
                value = GetScalar(index1, index2);
                return true;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(IndexPairRecord key, out T value)
        {
            var (key1, key2) = key;

            if (key1 < (ulong) Count1 && key2 < (ulong) Count2)
            {
                value = GetScalar(key1, key2);
                return true;
            }

            value = default;
            return false;
        }

        //TODO: Review all GetEmptyIndices methods
        public IEnumerable<IndexPairRecord> GetEmptyIndices(ulong maxKey1, ulong maxKey2)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;
            
            if (maxKey1 < count1)
            {
                if (maxKey2 < count2)
                {
                    yield break;
                }
                else
                {
                    for (var k1 = 0UL; k1 <= maxKey1; k1++)
                    for (var k2 = count2; k2 <= maxKey2; k2++)
                        yield return new IndexPairRecord(k1, k2);
                }
            }
            else
            {
                if (maxKey2 < (ulong) Count2)
                {
                    for (var k1 = count1; k1 <= maxKey1; k1++)
                    for (var k2 = 0UL; k2 <= maxKey2; k2++)
                        yield return new IndexPairRecord(k1, k2);
                }
                else
                {
                    for (var k1 = 0UL; k1 < count1; k1++)
                    for (var k2 = count2; k2 <= maxKey2; k2++)
                        yield return new IndexPairRecord(k1, k2);

                    for (var k1 = count1; k1 <= maxKey1; k1++)
                    for (var k2 = 0UL; k2 <= maxKey2; k2++)
                        yield return new IndexPairRecord(k1, k2);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexPairRecord> GetEmptyIndices(IndexPairRecord maxKey)
        {
            var (maxKey1, maxKey2) = maxKey;

            return GetEmptyIndices(maxKey1, maxKey2);
        }

        public abstract ILaMatrixEvenStorage<T> GetCopy();

        public ILaMatrixEvenStorage<T> MapIndices(Func<ulong, ulong, IndexPairRecord> keyMapping)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            var valueDictionary = new Dictionary<IndexPairRecord, T>();

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
            {
                var (key1, key2) = keyMapping(k1, k2);

                valueDictionary.Add(
                    new IndexPairRecord(key1, key2),
                    GetScalar(k1, k2)
                );
            }

            return valueDictionary.CreateEvenGrid();
        }

        public ILaMatrixEvenStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;
            var valuesArray = new T2[count1, count2];

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
                valuesArray[k1, k2] = valueMapping(GetScalar(k1, k2));

            return valuesArray.CreateEvenGridDense();
        }

        public ILaMatrixEvenStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;
            var valuesArray = new T2[count1, count2];

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
                valuesArray[k1, k2] = keyValueMapping(k1, k2, GetScalar(k1, k2));

            return valuesArray.CreateEvenGridDense();
        }

        public ILaMatrixEvenStorage<T> FilterByIndex(Func<ulong, ulong, bool> keyFilter)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            var valueDictionary = new Dictionary<IndexPairRecord, T>();

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
            {
                if (!keyFilter(k1, k2)) continue;

                valueDictionary.Add(
                    new IndexPairRecord(k1, k2),
                    GetScalar(k1, k2)
                );
            }

            return valueDictionary.CreateEvenGrid();
        }

        public ILaMatrixEvenStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            var valueDictionary = new Dictionary<IndexPairRecord, T>();

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
            {
                var value = GetScalar(k1, k2);

                if (!keyValueFilter(k1, k2, value)) continue;

                valueDictionary.Add(
                    new IndexPairRecord(k1, k2),
                    value
                );
            }

            return valueDictionary.CreateEvenGrid();
        }

        public ILaMatrixEvenStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            var valueDictionary = new Dictionary<IndexPairRecord, T>();

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
            {
                var value = GetScalar(k1, k2);

                if (!valueFilter(value)) continue;

                valueDictionary.Add(
                    new IndexPairRecord(k1, k2),
                    value
                );
            }

            return valueDictionary.CreateEvenGrid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual ILaMatrixEvenStorage<T> Transpose()
        {
            return new LaMatrixDenseTransposedStorage<T>(this);
        }

        public ILaMatrixGradedStorage<T> ToGradedStorage(Func<ulong, ulong, GradeIndexPairRecord> evenKeyToGradeKeyMapping)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<IndexPairRecord, T>>();

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
            {
                var value = GetScalar(k1, k2);
                var (grade, key1, key2) = evenKeyToGradeKeyMapping(k1, k2);

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var keyValueDictionary))
                {
                    keyValueDictionary = new Dictionary<IndexPairRecord, T>();
                    gradeKeyValueDictionary.Add(grade, keyValueDictionary);
                }

                var key = new IndexPairRecord(key1, key2); 

                if (keyValueDictionary.ContainsKey(key))
                    keyValueDictionary[key] = value;
                else
                    keyValueDictionary.Add(key, value);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        public ILaMatrixGradedStorage<T> ToGradedStorage(Func<ulong, ulong, T, GradeIndexPairScalarRecord<T>> evenIndexScalarToGradeIndexScalarMapping)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<IndexPairRecord, T>>();

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
            {
                var (grade, key1, key2, scalar) = evenIndexScalarToGradeIndexScalarMapping(k1, k2, GetScalar(k1, k2));

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var keyValueDictionary))
                {
                    keyValueDictionary = new Dictionary<IndexPairRecord, T>();
                    gradeKeyValueDictionary.Add(grade, keyValueDictionary);
                }

                var key = new IndexPairRecord(key1, key2); 

                if (keyValueDictionary.ContainsKey(key))
                    keyValueDictionary[key] = scalar;
                else
                    keyValueDictionary.Add(key, scalar);
            }

            return gradeKeyValueDictionary.CreateLaMatrixGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> GetRow(ulong index1)
        {
            return this.CreateLaVectorColumnStorage(index1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> GetColumn(ulong index2)
        {
            return this.CreateLaVectorColumnStorage(index2);
        }


        public bool TryGetCompactStorage(out ILaMatrixEvenStorage<T> evenGrid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndexPairScalarRecord<T>> GetIndexScalarRecords()
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
                yield return new IndexPairScalarRecord<T>(
                    k1, k2, GetScalar(k1, k2)
                );
        }
    }
}