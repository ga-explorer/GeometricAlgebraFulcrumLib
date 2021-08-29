using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaListEvenUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReduceValues<T>(this ILaVectorEvenStorage<T> evenList, Func<T, T, T> itemMapping)
        {
            return evenList.GetScalars().Aggregate(itemMapping);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexScalarRecord<T> ReduceKeyValues<T>(this ILaVectorEvenStorage<T> evenList, Func<IndexScalarRecord<T>, IndexScalarRecord<T>, IndexScalarRecord<T>> itemMapping)
        {
            return evenList.GetIndexScalarRecords().Aggregate(itemMapping);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 FoldValues<T, T2>(this ILaVectorEvenStorage<T> evenList, T2 initialValue, Func<T2, T, T2> itemMapping)
        {
            return evenList.GetScalars().Aggregate(initialValue, itemMapping);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 FoldKeyValues<T, T2>(this ILaVectorEvenStorage<T> evenList, T2 initialValue, Func<T2, ulong, T, T2> itemMapping)
        {
            return evenList.GetIndexScalarRecords().Aggregate(
                initialValue, 
                (accValue, indexValue) => itemMapping(accValue, indexValue.Index, indexValue.Scalar)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 FoldKeyValues<T, T2>(this ILaVectorEvenStorage<T> evenList, T2 initialValue, Func<T2, IndexScalarRecord<T>, T2> itemMapping)
        {
            return evenList.GetIndexScalarRecords().Aggregate(
                initialValue, 
                itemMapping
            );
        }


        public static IEnumerable<T2> ScanValues<T, T2>(this ILaVectorEvenStorage<T> evenList, T2 initialValue, Func<T2, T, T2> itemMapping)
        {
            var oldItem = initialValue;
            yield return oldItem;

            foreach (var item in evenList.GetScalars())
            {
                oldItem = itemMapping(oldItem, item);
                yield return oldItem;
            }
        }

        public static IEnumerable<T2> ScanKeyValues<T, T2>(this ILaVectorEvenStorage<T> evenList, T2 initialValue, Func<T2, ulong, T, T2> itemMapping)
        {
            var oldItem = initialValue;
            yield return oldItem;

            foreach (var (index, value) in evenList.GetIndexScalarRecords())
            {
                oldItem = itemMapping(oldItem, index, value);
                yield return oldItem;
            }
        }
        
        public static IEnumerable<T2> ScanKeyValues<T, T2>(this ILaVectorEvenStorage<T> evenList, T2 initialValue, Func<T2, IndexScalarRecord<T>, T2> itemMapping)
        {
            var oldItem = initialValue;
            yield return oldItem;

            foreach (var indexValue in evenList.GetIndexScalarRecords())
            {
                oldItem = itemMapping(oldItem, indexValue);
                yield return oldItem;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetKeysUnion<T>(this ILaVectorEvenStorage<T> evenList1, ILaVectorEvenStorage<T> evenList2)
        {
            return evenList1.GetIndices().Union(evenList2.GetIndices());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetKeysIntersection<T>(this ILaVectorEvenStorage<T> evenList1, ILaVectorEvenStorage<T> evenList2)
        {
            return evenList1.GetIndices().Intersect(evenList2.GetIndices());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetKeysDifference<T>(this ILaVectorEvenStorage<T> evenList1, ILaVectorEvenStorage<T> evenList2)
        {
            return evenList1.GetIndices().Except(evenList2.GetIndices());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<ulong> GetKeysSetSymmetricDifference<T>(this ILaVectorEvenStorage<T> evenList1, ILaVectorEvenStorage<T> evenList2)
        {
            var indexsSet = new HashSet<ulong>(evenList1.GetIndices());

            indexsSet.SymmetricExceptWith(evenList2.GetIndices());

            return indexsSet;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this ILaVectorEvenStorage<T> evenList, Func<T, T> mappingFunc)
        {
            return evenList
                .GetScalars()
                .Select(mappingFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this ILaVectorEvenStorage<T> evenList, Func<ulong, T, T> mappingFunc)
        {
            return evenList
                .GetIndexScalarRecords()
                .Select(indexValue => 
                    mappingFunc(indexValue.Index, indexValue.Scalar)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this ILaVectorEvenStorage<T> evenList, uint grade, Func<uint, ulong, T, T> mappingFunc)
        {
            return evenList
                .GetIndexScalarRecords()
                .Select(indexValue => 
                    mappingFunc(grade, indexValue.Index, indexValue.Scalar)
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> GetCompactList<T>(this ILaVectorEvenStorage<T> evenList)
        {
            return evenList.TryGetCompactStorage(out var compactList)
                ? compactList
                : evenList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetDenseCount<T>(this ILaVectorEvenStorage<T> evenList)
        {
            return evenList.IsEmpty()
                ? 0
                : (int) (evenList.GetMaxIndex() + 1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsKey<T>(this ILaVectorEvenStorage<T> evenList, int index)
        {
            return index >= 0 && evenList.ContainsIndex((ulong) index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaVectorEvenStorage<T> evenList, int index)
        {
            return index < 0
                ? throw new KeyNotFoundException()
                : evenList.GetScalar((ulong) index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaVectorEvenStorage<T> evenList, int index, T defaultValue)
        {
            return index >= 0 && evenList.TryGetScalar((ulong) index, out var value)
                ? value ?? defaultValue
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaVectorEvenStorage<T> evenList, ulong index, T defaultValue)
        {
            return evenList.TryGetScalar(index, out var value)
                ? value ?? defaultValue
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaVectorEvenStorage<T> evenList, int index, Func<T> defaultValueFunc)
        {
            return evenList.TryGetValue(index, out var value)
                ? value ?? defaultValueFunc()
                : defaultValueFunc();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaVectorEvenStorage<T> evenList, ulong index, Func<T> defaultValueFunc)
        {
            return evenList.TryGetScalar(index, out var value)
                ? value ?? defaultValueFunc()
                : defaultValueFunc();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaVectorEvenStorage<T> evenList, int index, Func<int, T> defaultValueFunc)
        {
            return index >= 0 && evenList.TryGetScalar((ulong) index, out var value)
                ? value ?? defaultValueFunc(index)
                : defaultValueFunc(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaVectorEvenStorage<T> evenList, ulong index, Func<ulong, T> defaultValueFunc)
        {
            return evenList.TryGetScalar(index, out var value)
                ? value ?? defaultValueFunc(index)
                : defaultValueFunc(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this ILaVectorEvenStorage<T> evenList, int index, out T value)
        {
            if (index >= 0 && evenList.TryGetScalar((ulong) index, out value))
                return true;

            value = default;
            return false;
        }


        /// <summary>
        /// The value corresponding to the smallest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMinKeyValue<T>(this ILaVectorEvenStorage<T> evenList)
        {
            return evenList.GetScalar(evenList.GetMinIndex());
        }

        /// <summary>
        /// The value corresponding to the largest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMaxKeyValue<T>(this ILaVectorEvenStorage<T> evenList)
        {
            return evenList.GetScalar(evenList.GetMaxIndex());
        }


        /// <summary>
        /// The smallest index and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexScalarRecord<T> GetMinKeyValueRecord<T>(this ILaVectorEvenStorage<T> evenList)
        {
            var index = evenList.GetMinIndex();

            return new IndexScalarRecord<T>(index, evenList.GetScalar(index));
        }

        /// <summary>
        /// The largest index and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexScalarRecord<T> GetMaxKeyValueRecord<T>(this ILaVectorEvenStorage<T> evenList)
        {
            var index = evenList.GetMaxIndex();

            return new IndexScalarRecord<T>(index, evenList.GetScalar(index));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfVector<T>(this ILaVectorEvenStorage<T> indexScalarList)
        {
            return indexScalarList.IsEmpty() 
                ? 0U
                : indexScalarList.GetMaxIndex().BasisVectorIndexToMinVSpaceDimension();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfBivector<T>(this ILaVectorEvenStorage<T> indexScalarList)
        {
            return indexScalarList.IsEmpty()
                ? 0U
                : indexScalarList.GetMaxIndex().BasisBivectorIndexToMinVSpaceDimension();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfKVector<T>(this ILaVectorEvenStorage<T> indexScalarList, uint grade)
        {
            return indexScalarList.IsEmpty()
                ? 0U
                : indexScalarList.GetMaxIndex().BasisBladeIndexToMinVSpaceDimension(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfMultivector<T>(this ILaVectorEvenStorage<T> idScalarList)
        {
            return idScalarList.IsEmpty()
                ? 0U
                : idScalarList.GetMaxIndex().BasisBladeIdToMinVSpaceDimension();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorGradedStorage<T> ToGradedList<T>(this ILaVectorEvenStorage<T> evenList)
        {
            return evenList.ToGradedStorage(GaBasisBladeUtils.BasisBladeIdToGradeIndex);
        }


        /// <summary>
        /// Convert this structure to a read only list and replace empty
        /// indexs using defaultValueFunc
        /// </summary>
        /// <param name="evenList"></param>
        /// <param name="count"></param>
        /// <param name="defaultValueFunc"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(this ILaVectorEvenStorage<T> evenList, int count, Func<int, T> defaultValueFunc)
        {
            var array = new T[count];

            foreach (var index in evenList.GetEmptyIndices((ulong) count))
                array[index] = defaultValueFunc((int) index);

            foreach (var (index, value) in evenList.GetIndexScalarRecords())
                array[index] = value;

            return array;
        }

        /// <summary>
        /// Convert this structure to a read only list and replace empty
        /// indexs using defaultValue
        /// </summary>
        /// <param name="evenList"></param>
        /// <param name="count"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(this ILaVectorEvenStorage<T> evenList, int count, T defaultValue)
        {
            var array = new T[count];

            foreach (var index in evenList.GetEmptyIndices((ulong) count))
                array[index] = defaultValue;

            foreach (var (index, value) in evenList.GetIndexScalarRecords())
                array[index] = value;

            return array;
        }

        public static T[] ToArray<T>(this ILaVectorEvenStorage<T> evenList)
        {
            var count = evenList.GetDenseCount();
            var array = new T[count];
            
            foreach (var (index, value) in evenList.GetIndexScalarRecords())
                array[index] = value;

            return array;
        }

        public static T[] ToArray<T>(this ILaVectorEvenStorage<T> evenList, T defaultValue)
        {
            var count = evenList.GetDenseCount();
            var array = new T[count];

            foreach (var index in evenList.GetEmptyIndices((ulong) count))
                array[index] = defaultValue;

            foreach (var (index, value) in evenList.GetIndexScalarRecords())
                array[index] = value;

            return array;
        }

        public static T[] ToArray<T>(this ILaVectorEvenStorage<T> evenList, int count)
        {
            var array = new T[count];
            
            foreach (var (index, value) in evenList.GetIndexScalarRecords())
                array[index] = value;

            return array;
        }

        public static T[] ToArray<T>(this ILaVectorEvenStorage<T> evenList, Func<T> defaultValueFunc)
        {
            var count = evenList.GetDenseCount();
            var array = new T[count];

            foreach (var index in evenList.GetEmptyIndices((ulong) count))
                array[index] = defaultValueFunc();

            foreach (var (index, value) in evenList.GetIndexScalarRecords())
                array[index] = value;

            return array;
        }

        public static T[] ToArray<T>(this ILaVectorEvenStorage<T> evenList, Func<int, T> defaultValueFunc)
        {
            var count = evenList.GetDenseCount();
            var array = new T[count];

            foreach (var index in evenList.GetEmptyIndices((ulong) count))
                array[index] = defaultValueFunc((int) index);

            foreach (var (index, value) in evenList.GetIndexScalarRecords())
                array[index] = value;

            return array;
        }

        public static T[] ToArray<T>(this ILaVectorEvenStorage<T> evenList, int count, Func<T> defaultValueFunc)
        {
            var array = new T[count];

            foreach (var index in evenList.GetEmptyIndices((ulong) count))
                array[index] = defaultValueFunc();

            foreach (var (index, value) in evenList.GetIndexScalarRecords())
                array[index] = value;

            return array;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexScalarRecord<T>> GetGradeKeyValueRecords<T>(this ILaVectorEvenStorage<T> evenList)
        {
            return evenList.GetIndexScalarRecords().Select(
                indexValueRecord => 
                    indexValueRecord.GetGradeKeyValueRecord(GaBasisBladeUtils.BasisBladeIdToGradeIndex)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexScalarRecord<T>> GetGradeKeyValueRecords<T>(this ILaVectorEvenStorage<T> evenList, Func<ulong, GradeIndexRecord> indexToGradeKeyMapping)
        {
            return evenList.GetIndexScalarRecords().Select(
                indexValueRecord => 
                    indexValueRecord.GetGradeKeyValueRecord(indexToGradeKeyMapping)
            );
        }
    }
}