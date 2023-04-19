using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense
{
    public sealed class LinMatrixRepeatedScalarStorage<T> :
        ILinMatrixImmutableDenseStorage<T>
    {
        public T Scalar { get; set; }

        public int Count1 { get; }

        public int Count2 { get; }

        public int Count 
            => Count1 * Count2;
        
        public bool IsSquare 
            => Count1 == Count2;

        public T this[int index1, int index2] 
            => GetScalar((ulong) index1, (ulong) index2);

        public T this[ulong index1, ulong index2] 
            => GetScalar(index1, index2);


        internal LinMatrixRepeatedScalarStorage(int count1, int count2, T scalar)
        {
            Count1 = count1;
            Count2 = count2;
            Scalar = scalar;
        }

        
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(ulong index1, ulong index2)
        {
            if (index1 >= (ulong) Count1 || index2 >= (ulong) Count2)
                throw new KeyNotFoundException();

            return Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(RGaKvIndexPairRecord key)
        {
            return GetScalar(key.KvIndex1, key.KvIndex2);
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
        public IEnumerable<RGaKvIndexPairRecord> GetIndices()
        {
            return GaFuLRecordUtils.GetIndexPairsInRange(Count1, Count2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return Count.Repeat(Scalar);
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
        public bool ContainsIndex(RGaKvIndexPairRecord key)
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
        public RGaKvIndexPairRecord GetMinIndex()
        {
            return new RGaKvIndexPairRecord(0, 0);
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
        public RGaKvIndexPairRecord GetMaxIndex()
        {
            return new RGaKvIndexPairRecord((ulong) (Count1 - 1), (ulong) (Count2 - 1));
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
        public bool TryGetScalar(RGaKvIndexPairRecord key, out T value)
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

        public IEnumerable<RGaKvIndexPairRecord> GetEmptyIndices(ulong maxCount1, ulong maxCount2)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;
            
            if (maxCount1 <= count1)
            {
                if (maxCount2 <= count2) 
                    yield break;

                for (var k1 = 0UL; k1 < maxCount1; k1++)
                for (var k2 = count2; k2 < maxCount2; k2++)
                    yield return new RGaKvIndexPairRecord(k1, k2);
            }
            else
            {
                if (maxCount2 <= count2)
                {
                    for (var k1 = count1; k1 < maxCount1; k1++)
                    for (var k2 = 0UL; k2 < maxCount2; k2++)
                        yield return new RGaKvIndexPairRecord(k1, k2);
                }
                else
                {
                    for (var k1 = 0UL; k1 < count1; k1++)
                    for (var k2 = count2; k2 < maxCount2; k2++)
                        yield return new RGaKvIndexPairRecord(k1, k2);

                    for (var k1 = count1; k1 < maxCount1; k1++)
                    for (var k2 = 0UL; k2 < maxCount2; k2++)
                        yield return new RGaKvIndexPairRecord(k1, k2);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexPairRecord> GetEmptyIndices(RGaKvIndexPairRecord maxCount)
        {
            var (maxCount1, maxCount2) = maxCount;

            return GetEmptyIndices(maxCount1, maxCount2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetCopy()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetPermutation(Func<ulong, ulong, RGaKvIndexPairRecord> keyMapping)
        {
            return GetIndexScalarRecords().MapRecords(keyMapping).CreateLinMatrixStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetPermutation(Func<RGaKvIndexPairRecord, RGaKvIndexPairRecord> indexMapping)
        {
            return GetIndexScalarRecords().MapRecords(indexMapping).CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;
            var valuesArray = new T2[count1, count2];

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
                valuesArray[k1, k2] = valueMapping(GetScalar(k1, k2));

            return valuesArray.CreateLinMatrixDenseStorage();
        }

        public ILinMatrixStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;
            var valuesArray = new T2[count1, count2];

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
                valuesArray[k1, k2] = keyValueMapping(k1, k2, GetScalar(k1, k2));

            return valuesArray.CreateLinMatrixDenseStorage();
        }

        public ILinMatrixStorage<T> FilterByIndex(Func<ulong, ulong, bool> keyFilter)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            var valueDictionary = new Dictionary<RGaKvIndexPairRecord, T>();

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
            {
                if (!keyFilter(k1, k2)) continue;

                valueDictionary.Add(
                    new RGaKvIndexPairRecord(k1, k2),
                    GetScalar(k1, k2)
                );
            }

            return valueDictionary.CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            var valueDictionary = new Dictionary<RGaKvIndexPairRecord, T>();

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
            {
                var value = GetScalar(k1, k2);

                if (!keyValueFilter(k1, k2, value)) continue;

                valueDictionary.Add(
                    new RGaKvIndexPairRecord(k1, k2),
                    value
                );
            }

            return valueDictionary.CreateLinMatrixStorage();
        }

        public ILinMatrixStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            var valueDictionary = new Dictionary<RGaKvIndexPairRecord, T>();

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
            {
                var value = GetScalar(k1, k2);

                if (!valueFilter(value)) continue;

                valueDictionary.Add(
                    new RGaKvIndexPairRecord(k1, k2),
                    value
                );
            }

            return valueDictionary.CreateLinMatrixStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetTranspose()
        {
            return Count1 == Count2
                ? this
                : new LinMatrixRepeatedScalarStorage<T>(Count2, Count1, Scalar);
        }

        public ILinMatrixDenseStorage<T> GetDensePermutation(Func<ulong, ulong, RGaKvIndexPairRecord> indexMapping)
        {
            var scalarsArray = new T[Count1, Count2];

            for (var i1 = 0UL; i1 < (ulong) Count1; i1++)
            for (var i2 = 0UL; i2 < (ulong) Count2; i2++)
            {
                var (index1, index2) = 
                    indexMapping(i1, i2);

                scalarsArray[index1, index2] = 
                    GetScalar(i1, i2);
            }

            return new LinMatrixDenseStorage<T>(scalarsArray);
        }

        public ILinMatrixDenseStorage<T> GetDensePermutation(Func<RGaKvIndexPairRecord, RGaKvIndexPairRecord> indexMapping)
        {
            var scalarsArray = new T[Count1, Count2];

            for (var i1 = 0UL; i1 < (ulong) Count1; i1++)
            for (var i2 = 0UL; i2 < (ulong) Count2; i2++)
            {
                var (index1, index2) = 
                    indexMapping(new RGaKvIndexPairRecord(i1, i2));

                scalarsArray[index1, index2] = 
                    GetScalar(i1, i2);
            }

            return new LinMatrixDenseStorage<T>(scalarsArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetDenseRows(IEnumerable<ulong> rowIndexList)
        {
            return rowIndexList
                .Select(index => 
                    new RGaKvIndexLinVectorStorageRecord<T>(
                        index,
                        Scalar.CreateLinVectorRepeatedScalarStorage(Count2)
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetDenseColumns(IEnumerable<ulong> columnIndexList)
        {
            return columnIndexList
                .Select(index => 
                    new RGaKvIndexLinVectorStorageRecord<T>(
                        index,
                        Scalar.CreateLinVectorRepeatedScalarStorage(Count1)
                    )
                );
        }

        public ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, RGaGradeKvIndexPairRecord> indexToGradeIndexMapping)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<RGaKvIndexPairRecord, T>>();

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
            {
                var value = GetScalar(k1, k2);
                var (grade, key1, key2) = indexToGradeIndexMapping(k1, k2);

                if (!gradeIndexScalarDictionary.TryGetValue(grade, out var keyValueDictionary))
                {
                    keyValueDictionary = new Dictionary<RGaKvIndexPairRecord, T>();
                    gradeIndexScalarDictionary.Add(grade, keyValueDictionary);
                }

                var key = new RGaKvIndexPairRecord(key1, key2); 

                if (keyValueDictionary.ContainsKey(key))
                    keyValueDictionary[key] = value;
                else
                    keyValueDictionary.Add(key, value);
            }

            return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
        }

        public ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, T, RGaGradeKvIndexPairScalarRecord<T>> indexScalarToGradeIndexScalarMapping)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<RGaKvIndexPairRecord, T>>();

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
            {
                var (grade, key1, key2, scalar) = indexScalarToGradeIndexScalarMapping(k1, k2, GetScalar(k1, k2));

                if (!gradeIndexScalarDictionary.TryGetValue(grade, out var keyValueDictionary))
                {
                    keyValueDictionary = new Dictionary<RGaKvIndexPairRecord, T>();
                    gradeIndexScalarDictionary.Add(grade, keyValueDictionary);
                }

                var key = new RGaKvIndexPairRecord(key1, key2); 

                if (keyValueDictionary.ContainsKey(key))
                    keyValueDictionary[key] = scalar;
                else
                    keyValueDictionary.Add(key, scalar);
            }

            return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetRow(ulong index1)
        {
            return Scalar.CreateLinVectorRepeatedScalarStorage(Count2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetColumn(ulong index2)
        {
            return Scalar.CreateLinVectorRepeatedScalarStorage(Count1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetRows()
        {
            var vector = Scalar.CreateLinVectorRepeatedScalarStorage(Count2);

            return ((ulong) Count1)
                .GetRange()
                .Select(index => 
                    new RGaKvIndexLinVectorStorageRecord<T>(index, vector)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetRows(Func<ulong, bool> rowIndexFilter)
        {
            var vector = Scalar.CreateLinVectorRepeatedScalarStorage(Count2);

            return ((ulong) Count1)
                .GetRange()
                .Where(rowIndexFilter)
                .Select(index => 
                    new RGaKvIndexLinVectorStorageRecord<T>(index, vector)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetColumns()
        {
            var vector = Scalar.CreateLinVectorRepeatedScalarStorage(Count1);

            return ((ulong) Count2)
                .GetRange()
                .Select(index => 
                    new RGaKvIndexLinVectorStorageRecord<T>(index, vector)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetColumns(Func<ulong, bool> columnIndexFilter)
        {
            return ((ulong) Count2)
                .GetRange()
                .Where(columnIndexFilter)
                .Select(index => 
                    new RGaKvIndexLinVectorStorageRecord<T>(
                        index,
                        Scalar.CreateLinVectorRepeatedScalarStorage(Count1)
                    )
                );
        }

        public ILinVectorStorage<T> CombineRows(IReadOnlyList<T> scalarList, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            ILinVectorStorage<T> vector = null;
            var rowVector = Scalar.CreateLinVectorRepeatedScalarStorage(Count2);

            var count = Math.Min(scalarList.Count, Count1);
            for (var rowIndex = 0; rowIndex < count; rowIndex++)
            {
                var scalingFactor = scalarList[rowIndex];
                var scaledVector = scalingFunc(scalingFactor, rowVector);

                vector = vector is null
                    ? scaledVector
                    : reducingFunc(vector, scaledVector);
            }

            return vector ?? LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public ILinVectorStorage<T> CombineRows(IEnumerable<RGaKvIndexScalarRecord<T>> rowIndexScalarRecords,
            Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc,
            Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            ILinVectorStorage<T> vector = null;
            var rowVector = Scalar.CreateLinVectorRepeatedScalarStorage(Count2);

            foreach (var (rowIndex, scalingFactor) in rowIndexScalarRecords)
            {
                if (rowIndex >= (ulong) Count1) continue;

                var scaledVector = scalingFunc(scalingFactor, rowVector);

                vector = vector is null
                    ? scaledVector
                    : reducingFunc(vector, scaledVector);
            }

            return vector ?? LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public ILinVectorStorage<T> CombineColumns(IReadOnlyList<T> scalarList, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            ILinVectorStorage<T> vector = null;
            var columnVector = Scalar.CreateLinVectorRepeatedScalarStorage(Count1);

            var count = Math.Min(scalarList.Count, Count2);
            for (var columnIndex = 0; columnIndex < count; columnIndex++)
            {
                var scalingFactor = scalarList[columnIndex];
                var scaledVector = scalingFunc(scalingFactor, columnVector);

                vector = vector is null
                    ? scaledVector
                    : reducingFunc(vector, scaledVector);
            }

            return vector ?? LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public ILinVectorStorage<T> CombineColumns(IEnumerable<RGaKvIndexScalarRecord<T>> columnIndexScalarRecords,
            Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc,
            Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc)
        {
            ILinVectorStorage<T> vector = null;
            var columnVector = Scalar.CreateLinVectorRepeatedScalarStorage(Count1);

            foreach (var (columnIndex, scalingFactor) in columnIndexScalarRecords)
            {
                if (columnIndex >= (ulong) Count2) continue;

                var scaledVector = scalingFunc(scalingFactor, columnVector);

                vector = vector is null
                    ? scaledVector
                    : reducingFunc(vector, scaledVector);
            }

            return vector ?? LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public bool TryGetCompactStorage(out ILinMatrixStorage<T> matrixStorage)
        {
            matrixStorage = this;
            return false;
        }

        public IEnumerable<RGaKvIndexPairScalarRecord<T>> GetIndexScalarRecords()
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
                yield return new RGaKvIndexPairScalarRecord<T>(
                    k1, k2, GetScalar(k1, k2)
                );
        }
    }
}