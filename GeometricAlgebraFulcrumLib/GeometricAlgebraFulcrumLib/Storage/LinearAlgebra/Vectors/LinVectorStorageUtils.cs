using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors
{
    public static class LinVectorStorageUtils
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> RemoveZeroValues<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> evenDictionary)
        {
            return evenDictionary.FilterByScalar(scalarProcessor.IsNotZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> RemoveZeroValues<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> evenDictionary, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? evenDictionary.FilterByScalar(scalarProcessor.IsNotNearZero)
                : evenDictionary.FilterByScalar(scalarProcessor.IsNotZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> RemoveNearZeroValues<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> evenDictionary)
        {
            return evenDictionary.FilterByScalar(scalarProcessor.IsNotNearZero);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReduceScalars<T>(this ILinVectorStorage<T> vectorStorage, Func<T, T, T> itemMapping)
        {
            return vectorStorage.GetScalars().Aggregate(itemMapping);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexScalarRecord<T> ReduceIndexScalarRecords<T>(this ILinVectorStorage<T> vectorStorage, Func<RGaKvIndexScalarRecord<T>, RGaKvIndexScalarRecord<T>, RGaKvIndexScalarRecord<T>> itemMapping)
        {
            return vectorStorage.GetIndexScalarRecords().Aggregate(itemMapping);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 FoldScalars<T, T2>(this ILinVectorStorage<T> vectorStorage, T2 initialScalar, Func<T2, T, T2> itemMapping)
        {
            return vectorStorage.GetScalars().Aggregate(initialScalar, itemMapping);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 FoldIndexScalars<T, T2>(this ILinVectorStorage<T> vectorStorage, T2 initialScalar, Func<T2, ulong, T, T2> itemMapping)
        {
            return vectorStorage.GetIndexScalarRecords().Aggregate(
                initialScalar,
                (accScalar, indexScalar) => itemMapping(accScalar, indexScalar.KvIndex, indexScalar.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 FoldIndexScalars<T, T2>(this ILinVectorStorage<T> vectorStorage, T2 initialScalar, Func<T2, RGaKvIndexScalarRecord<T>, T2> itemMapping)
        {
            return vectorStorage.GetIndexScalarRecords().Aggregate(
                initialScalar,
                itemMapping
            );
        }


        public static IEnumerable<T2> ScanScalars<T, T2>(this ILinVectorStorage<T> vectorStorage, T2 initialScalar, Func<T2, T, T2> itemMapping)
        {
            var oldItem = initialScalar;
            yield return oldItem;

            foreach (var item in vectorStorage.GetScalars())
            {
                oldItem = itemMapping(oldItem, item);
                yield return oldItem;
            }
        }

        public static IEnumerable<T2> ScanIndexScalars<T, T2>(this ILinVectorStorage<T> vectorStorage, T2 initialScalar, Func<T2, ulong, T, T2> itemMapping)
        {
            var oldItem = initialScalar;
            yield return oldItem;

            foreach (var (index, value) in vectorStorage.GetIndexScalarRecords())
            {
                oldItem = itemMapping(oldItem, index, value);
                yield return oldItem;
            }
        }

        public static IEnumerable<T2> ScanIndexScalars<T, T2>(this ILinVectorStorage<T> vectorStorage, T2 initialScalar, Func<T2, RGaKvIndexScalarRecord<T>, T2> itemMapping)
        {
            var oldItem = initialScalar;
            yield return oldItem;

            foreach (var indexScalar in vectorStorage.GetIndexScalarRecords())
            {
                oldItem = itemMapping(oldItem, indexScalar);
                yield return oldItem;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndicesUnion<T>(this ILinVectorStorage<T> vectorStorage1, ILinVectorStorage<T> vectorStorage2)
        {
            return vectorStorage1.GetIndices().Union(vectorStorage2.GetIndices());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndicesIntersection<T>(this ILinVectorStorage<T> vectorStorage1, ILinVectorStorage<T> vectorStorage2)
        {
            return vectorStorage1.GetIndices().Intersect(vectorStorage2.GetIndices());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndicesDifference<T>(this ILinVectorStorage<T> vectorStorage1, ILinVectorStorage<T> vectorStorage2)
        {
            return vectorStorage1.GetIndices().Except(vectorStorage2.GetIndices());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<ulong> GetIndicesSetSymmetricDifference<T>(this ILinVectorStorage<T> vectorStorage1, ILinVectorStorage<T> vectorStorage2)
        {
            var indexsSet = new HashSet<ulong>(vectorStorage1.GetIndices());

            indexsSet.SymmetricExceptWith(vectorStorage2.GetIndices());

            return indexsSet;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalars<T>(this ILinVectorStorage<T> vectorStorage, Func<T, T> mappingFunc)
        {
            return vectorStorage
                .GetScalars()
                .Select(mappingFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalars<T>(this ILinVectorStorage<T> vectorStorage, Func<ulong, T, T> mappingFunc)
        {
            return vectorStorage
                .GetIndexScalarRecords()
                .Select(indexScalar =>
                    mappingFunc(indexScalar.KvIndex, indexScalar.Scalar)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalars<T>(this ILinVectorStorage<T> vectorStorage, uint grade, Func<uint, ulong, T, T> mappingFunc)
        {
            return vectorStorage
                .GetIndexScalarRecords()
                .Select(indexScalar =>
                    mappingFunc(grade, indexScalar.KvIndex, indexScalar.Scalar)
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> GetCompactList<T>(this ILinVectorStorage<T> vectorStorage)
        {
            return vectorStorage.TryGetCompactStorage(out var compactList)
                ? compactList
                : vectorStorage;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetDenseCount<T>(this ILinVectorStorage<T> vectorStorage)
        {
            return vectorStorage.IsEmpty()
                ? 0
                : (int)(vectorStorage.GetMaxIndex() + 1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsIndex<T>(this ILinVectorStorage<T> vectorStorage, int index)
        {
            return index >= 0 && vectorStorage.ContainsIndex((ulong)index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinVectorStorage<T> vectorStorage, int index)
        {
            return index < 0
                ? throw new KeyNotFoundException()
                : vectorStorage.GetScalar((ulong)index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinVectorStorage<T> vectorStorage, int index, T defaultScalar)
        {
            return index >= 0 && vectorStorage.TryGetScalar((ulong)index, out var value)
                ? value ?? defaultScalar
                : defaultScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinVectorStorage<T> vectorStorage, ulong index, T defaultScalar)
        {
            return vectorStorage.TryGetScalar(index, out var value)
                ? value ?? defaultScalar
                : defaultScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinVectorStorage<T> vectorStorage, int index, Func<T> defaultScalarFunc)
        {
            return vectorStorage.TryGetScalar(index, out var value)
                ? value ?? defaultScalarFunc()
                : defaultScalarFunc();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinVectorStorage<T> vectorStorage, ulong index, Func<T> defaultScalarFunc)
        {
            return vectorStorage.TryGetScalar(index, out var value)
                ? value ?? defaultScalarFunc()
                : defaultScalarFunc();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinVectorStorage<T> vectorStorage, int index, Func<int, T> defaultScalarFunc)
        {
            return index >= 0 && vectorStorage.TryGetScalar((ulong)index, out var value)
                ? value ?? defaultScalarFunc(index)
                : defaultScalarFunc(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinVectorStorage<T> vectorStorage, ulong index, Func<ulong, T> defaultScalarFunc)
        {
            return vectorStorage.TryGetScalar(index, out var value)
                ? value ?? defaultScalarFunc(index)
                : defaultScalarFunc(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetScalar<T>(this ILinVectorStorage<T> vectorStorage, int index, out T value)
        {
            if (index >= 0 && vectorStorage.TryGetScalar((ulong)index, out value))
                return true;

            value = default;
            return false;
        }


        /// <summary>
        /// The value corresponding to the smallest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMinIndexScalar<T>(this ILinVectorStorage<T> vectorStorage)
        {
            return vectorStorage.GetScalar(vectorStorage.GetMinIndex());
        }

        /// <summary>
        /// The value corresponding to the largest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMaxIndexScalar<T>(this ILinVectorStorage<T> vectorStorage)
        {
            return vectorStorage.GetScalar(vectorStorage.GetMaxIndex());
        }


        /// <summary>
        /// The smallest index and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexScalarRecord<T> GetMinIndexScalarRecord<T>(this ILinVectorStorage<T> vectorStorage)
        {
            var index = vectorStorage.GetMinIndex();

            return new RGaKvIndexScalarRecord<T>(index, vectorStorage.GetScalar(index));
        }

        /// <summary>
        /// The largest index and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexScalarRecord<T> GetMaxIndexScalarRecord<T>(this ILinVectorStorage<T> vectorStorage)
        {
            var index = vectorStorage.GetMaxIndex();

            return new RGaKvIndexScalarRecord<T>(index, vectorStorage.GetScalar(index));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfVector<T>(this ILinVectorStorage<T> indexScalarList)
        {
            return indexScalarList.IsEmpty()
                ? 0U
                : indexScalarList.GetMaxIndex().BasisVectorIndexToMinVSpaceDimension();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfBivector<T>(this ILinVectorStorage<T> indexScalarList)
        {
            return indexScalarList.IsEmpty()
                ? 0U
                : indexScalarList.GetMaxIndex().BasisBivectorIndexToMinVSpaceDimension();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfKVector<T>(this ILinVectorStorage<T> indexScalarList, uint grade)
        {
            return indexScalarList.IsEmpty()
                ? 0U
                : indexScalarList.GetMaxIndex().BasisBladeIndexToMinVSpaceDimension(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfMultivector<T>(this ILinVectorStorage<T> idScalarList)
        {
            return idScalarList.IsEmpty()
                ? 0U
                : idScalarList.GetMaxIndex().BasisBladeIdToMinVSpaceDimension();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorGradedStorage<T> ToVectorGradedStorage<T>(this ILinVectorStorage<T> vectorStorage)
        {
            return vectorStorage.ToVectorGradedStorage(BasisBladeUtils.BasisBladeIdToGradeIndex);
        }


        /// <summary>
        /// Convert this structure to a read only list and replace empty
        /// indexs using defaultScalarFunc
        /// </summary>
        /// <param name="vectorStorage"></param>
        /// <param name="count"></param>
        /// <param name="defaultScalarFunc"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(this ILinVectorStorage<T> vectorStorage, int count, Func<int, T> defaultScalarFunc)
        {
            var array = new T[count];

            foreach (var index in vectorStorage.GetEmptyIndices((ulong)count))
                array[index] = defaultScalarFunc((int)index);

            foreach (var (index, value) in vectorStorage.GetIndexScalarRecords())
                array[index] = value;

            return array;
        }

        /// <summary>
        /// Convert this structure to a read only list and replace empty
        /// indexs using defaultScalar
        /// </summary>
        /// <param name="vectorStorage"></param>
        /// <param name="count"></param>
        /// <param name="defaultScalar"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(this ILinVectorStorage<T> vectorStorage, int count, T defaultScalar)
        {
            var array = new T[count];

            foreach (var index in vectorStorage.GetEmptyIndices((ulong)count))
                array[index] = defaultScalar;

            foreach (var (index, value) in vectorStorage.GetIndexScalarRecords())
                array[index] = value;

            return array;
        }

        public static T[] ToArray<T>(this ILinVectorStorage<T> vectorStorage)
        {
            var count = vectorStorage.GetDenseCount();
            var array = new T[count];

            foreach (var (index, value) in vectorStorage.GetIndexScalarRecords())
                array[index] = value;

            return array;
        }

        public static T[] ToArray<T>(this ILinVectorStorage<T> vectorStorage, T defaultScalar)
        {
            var count = vectorStorage.GetDenseCount();
            var array = new T[count];

            foreach (var index in vectorStorage.GetEmptyIndices((ulong)count))
                array[index] = defaultScalar;

            foreach (var (index, value) in vectorStorage.GetIndexScalarRecords())
                array[index] = value;

            return array;
        }

        public static T[] ToArrayOfSize<T>(this ILinVectorStorage<T> vectorStorage, int count)
        {
            var array = new T[count];

            foreach (var (index, value) in vectorStorage.GetIndexScalarRecords())
                if (index < (ulong)count) array[index] = value;

            return array;
        }

        public static T[] ToArray<T>(this ILinVectorStorage<T> vectorStorage, Func<T> defaultScalarFunc)
        {
            var count = vectorStorage.GetDenseCount();
            var array = new T[count];

            foreach (var index in vectorStorage.GetEmptyIndices((ulong)count))
                array[index] = defaultScalarFunc();

            foreach (var (index, value) in vectorStorage.GetIndexScalarRecords())
                array[index] = value;

            return array;
        }

        public static T[] ToArray<T>(this ILinVectorStorage<T> vectorStorage, Func<int, T> defaultScalarFunc)
        {
            var count = vectorStorage.GetDenseCount();
            var array = new T[count];

            foreach (var index in vectorStorage.GetEmptyIndices((ulong)count))
                array[index] = defaultScalarFunc((int)index);

            foreach (var (index, value) in vectorStorage.GetIndexScalarRecords())
                array[index] = value;

            return array;
        }

        public static T[] ToArray<T>(this ILinVectorStorage<T> vectorStorage, int count, Func<T> defaultScalarFunc)
        {
            var array = new T[count];

            foreach (var index in vectorStorage.GetEmptyIndices((ulong)count))
                array[index] = defaultScalarFunc();

            foreach (var (index, value) in vectorStorage.GetIndexScalarRecords())
                array[index] = value;

            return array;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaGradeKvIndexScalarRecord<T>> GetGradeIndexScalarRecords<T>(this ILinVectorStorage<T> vectorStorage)
        {
            return vectorStorage.GetIndexScalarRecords().Select(
                indexScalarRecord =>
                    indexScalarRecord.GetGradeIndexScalarRecord(BasisBladeUtils.BasisBladeIdToGradeIndex)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaGradeKvIndexScalarRecord<T>> GetGradeIndexScalarRecords<T>(this ILinVectorStorage<T> vectorStorage, Func<ulong, RGaGradeKvIndexRecord> indexToGradeIndexMapping)
        {
            return vectorStorage.GetIndexScalarRecords().Select(
                indexScalarRecord =>
                    indexScalarRecord.GetGradeIndexScalarRecord(indexToGradeIndexMapping)
            );
        }
    }
}