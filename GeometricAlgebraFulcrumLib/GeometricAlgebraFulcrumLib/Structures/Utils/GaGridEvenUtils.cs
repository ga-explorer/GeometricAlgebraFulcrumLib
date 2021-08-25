using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Utils
{
    public static class GaGridEvenUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyPair> GetKeysUnion<T>(this IGaGridEven<T> evenGrid1, IGaGridEven<T> evenGrid2)
        {
            return evenGrid1.GetKeys().Union(evenGrid2.GetKeys());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyPair> GetKeysIntersection<T>(this IGaGridEven<T> evenGrid1, IGaGridEven<T> evenGrid2)
        {
            return evenGrid1.GetKeys().Intersect(evenGrid2.GetKeys());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyPair> GetKeysDifference<T>(this IGaGridEven<T> evenGrid1, IGaGridEven<T> evenGrid2)
        {
            return evenGrid1.GetKeys().Except(evenGrid2.GetKeys());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<GaRecordKeyPair> GetKeysSymmetricDifference<T>(this IGaGridEven<T> evenGrid1, IGaGridEven<T> evenGrid2)
        {
            var keysSet = new HashSet<GaRecordKeyPair>(evenGrid1.GetKeys());

            keysSet.SymmetricExceptWith(evenGrid2.GetKeys());

            return keysSet;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this IGaGridEven<T> evenGrid, Func<T, T> mappingFunc)
        {
            return evenGrid
                .GetValues()
                .Select(mappingFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this IGaGridEven<T> evenGrid, Func<ulong, ulong, T, T> mappingFunc)
        {
            return evenGrid
                .GetKeyValueRecords()
                .Select(keyValue => 
                    mappingFunc(keyValue.Key1, keyValue.Key2, keyValue.Value)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this IGaGridEven<T> evenGrid, uint grade, Func<uint, ulong, ulong, T, T> mappingFunc)
        {
            return evenGrid
                .GetKeyValueRecords()
                .Select(keyValue => 
                    mappingFunc(grade, keyValue.Key1, keyValue.Key2, keyValue.Value)
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> GetCompactGrid<T>(this IGaGridEven<T> evenGrid)
        {
            return evenGrid.TryGetCompactGrid(out var compactGrid)
                ? compactGrid
                : evenGrid;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetDenseCount1<T>(this IGaGridEven<T> evenGrid)
        {
            return evenGrid.IsEmpty()
                ? 0
                : (int) (evenGrid.GetMaxKey1() + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetDenseCount2<T>(this IGaGridEven<T> evenGrid)
        {
            return evenGrid.IsEmpty()
                ? 0
                : (int) (evenGrid.GetMaxKey2() + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetDenseCount<T>(this IGaGridEven<T> evenGrid)
        {
            if (evenGrid.IsEmpty())
                return 0;

            var (key1, key2) = evenGrid.GetMaxKey();

            return (int) ((key1 + 1) * (key2 + 1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<int> GetDenseCountPair<T>(this IGaGridEven<T> evenGrid)
        {
            if (evenGrid.IsEmpty())
                return new Pair<int>(0, 0);

            var (key1, key2) = evenGrid.GetMaxKey();

            return new Pair<int>((int) (key1 + 1), (int) (key2 + 1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<int> GetSparseCountPair<T>(this IGaGridEven<T> evenGrid)
        {
            if (evenGrid.IsEmpty())
                return new Pair<int>(0, 0);

            var count1 = evenGrid.GetSparseCount1();
            var count2 = evenGrid.GetSparseCount2();

            return new Pair<int>(count1, count2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsKey<T>(this IGaGridEven<T> evenGrid, int key1, int key2)
        {
            return key1 >= 0 && key2 >= 0 && evenGrid.ContainsKey((ulong) key1, (ulong) key2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaGridEven<T> evenGrid, int key1, int key2)
        {
            return key1 < 0 || key2 < 0
                ? throw new KeyNotFoundException()
                : evenGrid.GetValue((ulong) key1, (ulong) key2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaGridEven<T> evenGrid, int key1, int key2, T defaultValue)
        {
            return key1 >= 0 && key2 >= 0 && evenGrid.TryGetValue((ulong) key1, (ulong) key2, out var value)
                ? value ?? defaultValue
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaGridEven<T> evenGrid, ulong key1, ulong key2, T defaultValue)
        {
            return evenGrid.TryGetValue(key1, key2, out var value)
                ? value ?? defaultValue
                : defaultValue;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaGridEven<T> evenGrid, GaRecordKeyPair key, T defaultValue)
        {
            return evenGrid.TryGetValue(key, out var value)
                ? value ?? defaultValue
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaGridEven<T> evenGrid, int key1, int key2, Func<T> defaultValueFunc)
        {
            return key1 >= 0 && key2 >= 0 && evenGrid.TryGetValue((ulong) key1, (ulong) key2, out var value)
                ? value ?? defaultValueFunc()
                : defaultValueFunc();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaGridEven<T> evenGrid, ulong key1, ulong key2, Func<T> defaultValueFunc)
        {
            return evenGrid.TryGetValue(key1, key2, out var value)
                ? value ?? defaultValueFunc()
                : defaultValueFunc();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaGridEven<T> evenGrid, int key1, int key2, Func<int, int, T> defaultValueFunc)
        {
            return key1 >= 0 && key2 >= 0 && evenGrid.TryGetValue((ulong) key1, (ulong) key2, out var value)
                ? value ?? defaultValueFunc(key1, key2)
                : defaultValueFunc(key1, key2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaGridEven<T> evenGrid, ulong key1, ulong key2, Func<ulong, ulong, T> defaultValueFunc)
        {
            return evenGrid.TryGetValue(key1, key2, out var value)
                ? value ?? defaultValueFunc(key1, key2)
                : defaultValueFunc(key1, key2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaGridEven<T> evenGrid, GaRecordKeyPair key, Func<ulong, ulong, T> defaultValueFunc)
        {
            return evenGrid.TryGetValue(key, out var value)
                ? value ?? defaultValueFunc(key.Key1, key.Key2)
                : defaultValueFunc(key.Key1, key.Key2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this IGaGridEven<T> evenGrid, int key1, int key2, out T value)
        {
            if (key1 >= 0 && key2 >= 0 && evenGrid.TryGetValue((ulong) key1, (ulong) key2, out value))
                return true;

            value = default;
            return false;
        }


        /// <summary>
        /// The value corresponding to the smallest key stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMinKeyValue<T>(this IGaGridEven<T> evenGrid)
        {
            return evenGrid.GetValue(evenGrid.GetMinKey());
        }

        /// <summary>
        /// The value corresponding to the largest key stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMaxKeyValue<T>(this IGaGridEven<T> evenGrid)
        {
            return evenGrid.GetValue(evenGrid.GetMaxKey());
        }


        /// <summary>
        /// The smallest key and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyPairValue<T> GetMinKeyValueRecord<T>(this IGaGridEven<T> evenGrid)
        {
            var key = evenGrid.GetMinKey();

            return new GaRecordKeyPairValue<T>(key.Key1, key.Key2, evenGrid.GetValue(key));
        }

        /// <summary>
        /// The largest key and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyPairValue<T> GetMaxKeyValueRecord<T>(this IGaGridEven<T> evenGrid)
        {
            var key = evenGrid.GetMaxKey();

            return new GaRecordKeyPairValue<T>(key.Key1, key.Key2, evenGrid.GetValue(key));
        }

        /// <summary>
        /// Convert this structure to a read only list and replace empty
        /// keys using defaultValueFunc
        /// </summary>
        /// <param name="evenGrid"></param>
        /// <param name="count1"></param>
        /// <param name="count2"></param>
        /// <param name="defaultValueFunc"></param>
        /// <returns></returns>
        public static T[,] ToArray<T>(this IGaGridEven<T> evenGrid, int count1, int count2, Func<int, int, T> defaultValueFunc)
        {
            var array = new T[count1, count2];

            var emptyKeys = 
                evenGrid.GetEmptyKeys((ulong) count1 - 1, (ulong) count2 - 1);

            foreach (var (key1, key2) in emptyKeys)
                array[key1, key2] = defaultValueFunc((int) key1, (int) key2);

            foreach (var (key1, key2, value) in evenGrid.GetKeyValueRecords())
                array[key1, key2] = value;

            return array;
        }

        /// <summary>
        /// Convert this structure to a read only list and replace empty
        /// keys using defaultValue
        /// </summary>
        /// <param name="evenGrid"></param>
        /// <param name="count1"></param>
        /// <param name="count2"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T[,] ToArray<T>(this IGaGridEven<T> evenGrid, int count1, int count2, T defaultValue)
        {
            var array = new T[count1, count2];
            
            var emptyKeys = 
                evenGrid.GetEmptyKeys((ulong) count1 - 1, (ulong) count2 - 1);

            foreach (var (key1, key2) in emptyKeys)
                array[key1, key2] = defaultValue;

            foreach (var (key1, key2, value) in evenGrid.GetKeyValueRecords())
                array[key1, key2] = value;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ToArray<T>(this IGaGridEven<T> evenGrid)
        {
            var (count1, count2) = evenGrid.GetDenseCountPair();

            var array = new T[count1, count2];

            foreach (var (key1, key2, value) in evenGrid.GetKeyValueRecords())
                array[key1, key2] = value;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ToArray<T>(this IGaGridEven<T> evenGrid, T defaultValue)
        {
            var (count1, count2) = evenGrid.GetDenseCountPair();
            var array = new T[count1, count2];
            
            var emptyKeys = 
                evenGrid.GetEmptyKeys((ulong) count1 - 1, (ulong) count2 - 1);

            foreach (var (key1, key2) in emptyKeys)
                array[key1, key2] = defaultValue;

            foreach (var (key1, key2, value) in evenGrid.GetKeyValueRecords())
                array[key1, key2] = value;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ToArray<T>(this IGaGridEven<T> evenGrid, int count1, int count2)
        {
            var array = new T[count1, count2];
            
            foreach (var (key1, key2, value) in evenGrid.GetKeyValueRecords())
                array[key1, key2] = value;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ToArray<T>(this IGaGridEven<T> evenGrid, Func<T> defaultValueFunc)
        {
            var (count1, count2) = evenGrid.GetDenseCountPair();
            var array = new T[count1, count2];

            var emptyKeys = 
                evenGrid.GetEmptyKeys((ulong) count1 - 1, (ulong) count2 - 1);

            foreach (var (key1, key2) in emptyKeys)
                array[key1, key2] = defaultValueFunc();

            foreach (var (key1, key2, value) in evenGrid.GetKeyValueRecords())
                array[key1, key2] = value;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ToArray<T>(this IGaGridEven<T> evenGrid, Func<int, int, T> defaultValueFunc)
        {
            var (count1, count2) = evenGrid.GetDenseCountPair();
            var array = new T[count1, count2];

            var emptyKeys = 
                evenGrid.GetEmptyKeys((ulong) count1 - 1, (ulong) count2 - 1);

            foreach (var (key1, key2) in emptyKeys)
                array[key1, key2] = defaultValueFunc((int) key1, (int) key2);

            foreach (var (key1, key2, value) in evenGrid.GetKeyValueRecords())
                array[key1, key2] = value;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ToArray<T>(this IGaGridEven<T> evenGrid, int count1, int count2, Func<T> defaultValueFunc)
        {
            var array = new T[count1, count2];

            var emptyKeys = 
                evenGrid.GetEmptyKeys((ulong) count1 - 1, (ulong) count2 - 1);

            foreach (var (key1, key2) in emptyKeys)
                array[key1, key2] = defaultValueFunc();

            foreach (var (key1, key2, value) in evenGrid.GetKeyValueRecords())
                array[key1, key2] = value;

            return array;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<uint> GetMinVSpaceDimensionOfVector<T>(this IGaGridEven<T> indexScalarGrid)
        {
            if (indexScalarGrid.IsEmpty())
                return new Pair<uint>(0U, 0U);

            return indexScalarGrid
                .GetMaxKey()
                .MapKeys(GaBasisVectorUtils.BasisVectorIndexToMinVSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<uint> GetMinVSpaceDimensionOfBivector<T>(this IGaGridEven<T> indexScalarGrid)
        {
            if (indexScalarGrid.IsEmpty())
                return new Pair<uint>(0U, 0U);

            return indexScalarGrid
                .GetMaxKey()
                .MapKeys(GaBasisBivectorUtils.BasisBivectorIndexToMinVSpaceDimension);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<uint> GetMinVSpaceDimensionOfKVector<T>(this IGaGridEven<T> indexScalarGrid, uint grade)
        {
            if (indexScalarGrid.IsEmpty())
                return new Pair<uint>(0U, 0U);

            return indexScalarGrid
                .GetMaxKey()
                .MapKeys(index => index.BasisBladeIndexToMinVSpaceDimension(grade));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<uint> GetMinVSpaceDimensionOfMultivector<T>(this IGaGridEven<T> idScalarGrid)
        {
            if (idScalarGrid.IsEmpty())
                return new Pair<uint>(0U, 0U);

            return idScalarGrid
                .GetMaxKey()
                .MapKeys(GaBasisBladeUtils.BasisBladeIdToMinVSpaceDimension);
        }
    }
}