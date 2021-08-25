using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Utils
{
    public static class GaListEvenUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReduceValues<T>(this IGaListEven<T> evenList, Func<T, T, T> itemMapping)
        {
            return evenList.GetValues().Aggregate(itemMapping);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyValue<T> ReduceKeyValues<T>(this IGaListEven<T> evenList, Func<GaRecordKeyValue<T>, GaRecordKeyValue<T>, GaRecordKeyValue<T>> itemMapping)
        {
            return evenList.GetKeyValueRecords().Aggregate(itemMapping);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 FoldValues<T, T2>(this IGaListEven<T> evenList, T2 initialValue, Func<T2, T, T2> itemMapping)
        {
            return evenList.GetValues().Aggregate(initialValue, itemMapping);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 FoldKeyValues<T, T2>(this IGaListEven<T> evenList, T2 initialValue, Func<T2, ulong, T, T2> itemMapping)
        {
            return evenList.GetKeyValueRecords().Aggregate(
                initialValue, 
                (accValue, keyValue) => itemMapping(accValue, keyValue.Key, keyValue.Value)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 FoldKeyValues<T, T2>(this IGaListEven<T> evenList, T2 initialValue, Func<T2, GaRecordKeyValue<T>, T2> itemMapping)
        {
            return evenList.GetKeyValueRecords().Aggregate(
                initialValue, 
                itemMapping
            );
        }


        public static IEnumerable<T2> ScanValues<T, T2>(this IGaListEven<T> evenList, T2 initialValue, Func<T2, T, T2> itemMapping)
        {
            var oldItem = initialValue;
            yield return oldItem;

            foreach (var item in evenList.GetValues())
            {
                oldItem = itemMapping(oldItem, item);
                yield return oldItem;
            }
        }

        public static IEnumerable<T2> ScanKeyValues<T, T2>(this IGaListEven<T> evenList, T2 initialValue, Func<T2, ulong, T, T2> itemMapping)
        {
            var oldItem = initialValue;
            yield return oldItem;

            foreach (var (key, value) in evenList.GetKeyValueRecords())
            {
                oldItem = itemMapping(oldItem, key, value);
                yield return oldItem;
            }
        }
        
        public static IEnumerable<T2> ScanKeyValues<T, T2>(this IGaListEven<T> evenList, T2 initialValue, Func<T2, GaRecordKeyValue<T>, T2> itemMapping)
        {
            var oldItem = initialValue;
            yield return oldItem;

            foreach (var keyValue in evenList.GetKeyValueRecords())
            {
                oldItem = itemMapping(oldItem, keyValue);
                yield return oldItem;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetKeysUnion<T>(this IGaListEven<T> evenList1, IGaListEven<T> evenList2)
        {
            return evenList1.GetKeys().Union(evenList2.GetKeys());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetKeysIntersection<T>(this IGaListEven<T> evenList1, IGaListEven<T> evenList2)
        {
            return evenList1.GetKeys().Intersect(evenList2.GetKeys());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetKeysDifference<T>(this IGaListEven<T> evenList1, IGaListEven<T> evenList2)
        {
            return evenList1.GetKeys().Except(evenList2.GetKeys());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<ulong> GetKeysSetSymmetricDifference<T>(this IGaListEven<T> evenList1, IGaListEven<T> evenList2)
        {
            var keysSet = new HashSet<ulong>(evenList1.GetKeys());

            keysSet.SymmetricExceptWith(evenList2.GetKeys());

            return keysSet;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this IGaListEven<T> evenList, Func<T, T> mappingFunc)
        {
            return evenList
                .GetValues()
                .Select(mappingFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this IGaListEven<T> evenList, Func<ulong, T, T> mappingFunc)
        {
            return evenList
                .GetKeyValueRecords()
                .Select(keyValue => 
                    mappingFunc(keyValue.Key, keyValue.Value)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this IGaListEven<T> evenList, uint grade, Func<uint, ulong, T, T> mappingFunc)
        {
            return evenList
                .GetKeyValueRecords()
                .Select(keyValue => 
                    mappingFunc(grade, keyValue.Key, keyValue.Value)
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> GetCompactList<T>(this IGaListEven<T> evenList)
        {
            return evenList.TryGetCompactList(out var compactList)
                ? compactList
                : evenList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetDenseCount<T>(this IGaListEven<T> evenList)
        {
            return evenList.IsEmpty()
                ? 0
                : (int) (evenList.GetMaxKey() + 1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsKey<T>(this IGaListEven<T> evenList, int key)
        {
            return key >= 0 && evenList.ContainsKey((ulong) key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaListEven<T> evenList, int key)
        {
            return key < 0
                ? throw new KeyNotFoundException()
                : evenList.GetValue((ulong) key);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaListEven<T> evenList, int key, T defaultValue)
        {
            return key >= 0 && evenList.TryGetValue((ulong) key, out var value)
                ? value ?? defaultValue
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaListEven<T> evenList, ulong key, T defaultValue)
        {
            return evenList.TryGetValue(key, out var value)
                ? value ?? defaultValue
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaListEven<T> evenList, int key, Func<T> defaultValueFunc)
        {
            return evenList.TryGetValue(key, out var value)
                ? value ?? defaultValueFunc()
                : defaultValueFunc();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaListEven<T> evenList, ulong key, Func<T> defaultValueFunc)
        {
            return evenList.TryGetValue(key, out var value)
                ? value ?? defaultValueFunc()
                : defaultValueFunc();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaListEven<T> evenList, int key, Func<int, T> defaultValueFunc)
        {
            return key >= 0 && evenList.TryGetValue((ulong) key, out var value)
                ? value ?? defaultValueFunc(key)
                : defaultValueFunc(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaListEven<T> evenList, ulong key, Func<ulong, T> defaultValueFunc)
        {
            return evenList.TryGetValue(key, out var value)
                ? value ?? defaultValueFunc(key)
                : defaultValueFunc(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this IGaListEven<T> evenList, int key, out T value)
        {
            if (key >= 0 && evenList.TryGetValue((ulong) key, out value))
                return true;

            value = default;
            return false;
        }


        /// <summary>
        /// The value corresponding to the smallest key stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMinKeyValue<T>(this IGaListEven<T> evenList)
        {
            return evenList.GetValue(evenList.GetMinKey());
        }

        /// <summary>
        /// The value corresponding to the largest key stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMaxKeyValue<T>(this IGaListEven<T> evenList)
        {
            return evenList.GetValue(evenList.GetMaxKey());
        }


        /// <summary>
        /// The smallest key and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyValue<T> GetMinKeyValueRecord<T>(this IGaListEven<T> evenList)
        {
            var key = evenList.GetMinKey();

            return new GaRecordKeyValue<T>(key, evenList.GetValue(key));
        }

        /// <summary>
        /// The largest key and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyValue<T> GetMaxKeyValueRecord<T>(this IGaListEven<T> evenList)
        {
            var key = evenList.GetMaxKey();

            return new GaRecordKeyValue<T>(key, evenList.GetValue(key));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfVector<T>(this IGaListEven<T> indexScalarList)
        {
            return indexScalarList.IsEmpty() 
                ? 0U
                : indexScalarList.GetMaxKey().BasisVectorIndexToMinVSpaceDimension();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfBivector<T>(this IGaListEven<T> indexScalarList)
        {
            return indexScalarList.IsEmpty()
                ? 0U
                : indexScalarList.GetMaxKey().BasisBivectorIndexToMinVSpaceDimension();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfKVector<T>(this IGaListEven<T> indexScalarList, uint grade)
        {
            return indexScalarList.IsEmpty()
                ? 0U
                : indexScalarList.GetMaxKey().BasisBladeIndexToMinVSpaceDimension(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfMultivector<T>(this IGaListEven<T> idScalarList)
        {
            return idScalarList.IsEmpty()
                ? 0U
                : idScalarList.GetMaxKey().BasisBladeIdToMinVSpaceDimension();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListGraded<T> ToGradedList<T>(this IGaListEven<T> evenList)
        {
            return evenList.ToGradedList(GaBasisBladeUtils.BasisBladeIdToGradeIndex);
        }


        /// <summary>
        /// Convert this structure to a read only list and replace empty
        /// keys using defaultValueFunc
        /// </summary>
        /// <param name="evenList"></param>
        /// <param name="count"></param>
        /// <param name="defaultValueFunc"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(this IGaListEven<T> evenList, int count, Func<int, T> defaultValueFunc)
        {
            var array = new T[count];

            foreach (var key in evenList.GetEmptyKeys((ulong) count))
                array[key] = defaultValueFunc((int) key);

            foreach (var (key, value) in evenList.GetKeyValueRecords())
                array[key] = value;

            return array;
        }

        /// <summary>
        /// Convert this structure to a read only list and replace empty
        /// keys using defaultValue
        /// </summary>
        /// <param name="evenList"></param>
        /// <param name="count"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(this IGaListEven<T> evenList, int count, T defaultValue)
        {
            var array = new T[count];

            foreach (var key in evenList.GetEmptyKeys((ulong) count))
                array[key] = defaultValue;

            foreach (var (key, value) in evenList.GetKeyValueRecords())
                array[key] = value;

            return array;
        }

        public static T[] ToArray<T>(this IGaListEven<T> evenList)
        {
            var count = evenList.GetDenseCount();
            var array = new T[count];
            
            foreach (var (key, value) in evenList.GetKeyValueRecords())
                array[key] = value;

            return array;
        }

        public static T[] ToArray<T>(this IGaListEven<T> evenList, T defaultValue)
        {
            var count = evenList.GetDenseCount();
            var array = new T[count];

            foreach (var key in evenList.GetEmptyKeys((ulong) count))
                array[key] = defaultValue;

            foreach (var (key, value) in evenList.GetKeyValueRecords())
                array[key] = value;

            return array;
        }

        public static T[] ToArray<T>(this IGaListEven<T> evenList, int count)
        {
            var array = new T[count];
            
            foreach (var (key, value) in evenList.GetKeyValueRecords())
                array[key] = value;

            return array;
        }

        public static T[] ToArray<T>(this IGaListEven<T> evenList, Func<T> defaultValueFunc)
        {
            var count = evenList.GetDenseCount();
            var array = new T[count];

            foreach (var key in evenList.GetEmptyKeys((ulong) count))
                array[key] = defaultValueFunc();

            foreach (var (key, value) in evenList.GetKeyValueRecords())
                array[key] = value;

            return array;
        }

        public static T[] ToArray<T>(this IGaListEven<T> evenList, Func<int, T> defaultValueFunc)
        {
            var count = evenList.GetDenseCount();
            var array = new T[count];

            foreach (var key in evenList.GetEmptyKeys((ulong) count))
                array[key] = defaultValueFunc((int) key);

            foreach (var (key, value) in evenList.GetKeyValueRecords())
                array[key] = value;

            return array;
        }

        public static T[] ToArray<T>(this IGaListEven<T> evenList, int count, Func<T> defaultValueFunc)
        {
            var array = new T[count];

            foreach (var key in evenList.GetEmptyKeys((ulong) count))
                array[key] = defaultValueFunc();

            foreach (var (key, value) in evenList.GetKeyValueRecords())
                array[key] = value;

            return array;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordGradeKeyValue<T>> GetGradeKeyValueRecords<T>(this IGaListEven<T> evenList)
        {
            return evenList.GetKeyValueRecords().Select(
                keyValueRecord => 
                    keyValueRecord.GetGradeKeyValueRecord(GaBasisBladeUtils.BasisBladeIdToGradeIndex)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordGradeKeyValue<T>> GetGradeKeyValueRecords<T>(this IGaListEven<T> evenList, Func<ulong, GaRecordGradeKey> keyToGradeKeyMapping)
        {
            return evenList.GetKeyValueRecords().Select(
                keyValueRecord => 
                    keyValueRecord.GetGradeKeyValueRecord(keyToGradeKeyMapping)
            );
        }
    }
}