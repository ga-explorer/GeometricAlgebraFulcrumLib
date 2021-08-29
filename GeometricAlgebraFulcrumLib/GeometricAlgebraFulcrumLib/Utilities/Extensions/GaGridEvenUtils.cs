using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaGridEvenUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairRecord> GetKeysUnion<T>(this ILaMatrixEvenStorage<T> evenGrid1, ILaMatrixEvenStorage<T> evenGrid2)
        {
            return evenGrid1.GetIndices().Union(evenGrid2.GetIndices());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairRecord> GetKeysIntersection<T>(this ILaMatrixEvenStorage<T> evenGrid1, ILaMatrixEvenStorage<T> evenGrid2)
        {
            return evenGrid1.GetIndices().Intersect(evenGrid2.GetIndices());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairRecord> GetKeysDifference<T>(this ILaMatrixEvenStorage<T> evenGrid1, ILaMatrixEvenStorage<T> evenGrid2)
        {
            return evenGrid1.GetIndices().Except(evenGrid2.GetIndices());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<IndexPairRecord> GetKeysSymmetricDifference<T>(this ILaMatrixEvenStorage<T> evenGrid1, ILaMatrixEvenStorage<T> evenGrid2)
        {
            var indexsSet = new HashSet<IndexPairRecord>(evenGrid1.GetIndices());

            indexsSet.SymmetricExceptWith(evenGrid2.GetIndices());

            return indexsSet;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this ILaMatrixEvenStorage<T> evenGrid, Func<T, T> mappingFunc)
        {
            return evenGrid
                .GetScalars()
                .Select(mappingFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this ILaMatrixEvenStorage<T> evenGrid, Func<ulong, ulong, T, T> mappingFunc)
        {
            return evenGrid
                .GetIndexScalarRecords()
                .Select(indexValue => 
                    mappingFunc(indexValue.Index1, indexValue.Index2, indexValue.Scalar)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this ILaMatrixEvenStorage<T> evenGrid, uint grade, Func<uint, ulong, ulong, T, T> mappingFunc)
        {
            return evenGrid
                .GetIndexScalarRecords()
                .Select(indexValue => 
                    mappingFunc(grade, indexValue.Index1, indexValue.Index2, indexValue.Scalar)
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> GetCompactGrid<T>(this ILaMatrixEvenStorage<T> evenGrid)
        {
            return evenGrid.TryGetCompactStorage(out var compactGrid)
                ? compactGrid
                : evenGrid;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetDenseCount1<T>(this ILaMatrixEvenStorage<T> evenGrid)
        {
            return evenGrid.IsEmpty()
                ? 0
                : (int) (evenGrid.GetMaxIndex1() + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetDenseCount2<T>(this ILaMatrixEvenStorage<T> evenGrid)
        {
            return evenGrid.IsEmpty()
                ? 0
                : (int) (evenGrid.GetMaxIndex2() + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetDenseCount<T>(this ILaMatrixEvenStorage<T> evenGrid)
        {
            if (evenGrid.IsEmpty())
                return 0;

            var (index1, index2) = evenGrid.GetMaxIndex();

            return (int) ((index1 + 1) * (index2 + 1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<int> GetDenseCountPair<T>(this ILaMatrixEvenStorage<T> evenGrid)
        {
            if (evenGrid.IsEmpty())
                return new Pair<int>(0, 0);

            var (index1, index2) = evenGrid.GetMaxIndex();

            return new Pair<int>((int) (index1 + 1), (int) (index2 + 1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<int> GetSparseCountPair<T>(this ILaMatrixEvenStorage<T> evenGrid)
        {
            if (evenGrid.IsEmpty())
                return new Pair<int>(0, 0);

            var count1 = evenGrid.GetSparseCount1();
            var count2 = evenGrid.GetSparseCount2();

            return new Pair<int>(count1, count2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsKey<T>(this ILaMatrixEvenStorage<T> evenGrid, int index1, int index2)
        {
            return index1 >= 0 && index2 >= 0 && evenGrid.ContainsIndex((ulong) index1, (ulong) index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaMatrixEvenStorage<T> evenGrid, int index1, int index2)
        {
            return index1 < 0 || index2 < 0
                ? throw new KeyNotFoundException()
                : evenGrid.GetScalar((ulong) index1, (ulong) index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaMatrixEvenStorage<T> evenGrid, int index1, int index2, T defaultValue)
        {
            return index1 >= 0 && index2 >= 0 && evenGrid.TryGetScalar((ulong) index1, (ulong) index2, out var value)
                ? value ?? defaultValue
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaMatrixEvenStorage<T> evenGrid, ulong index1, ulong index2, T defaultValue)
        {
            return evenGrid.TryGetScalar(index1, index2, out var value)
                ? value ?? defaultValue
                : defaultValue;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaMatrixEvenStorage<T> evenGrid, IndexPairRecord index, T defaultValue)
        {
            return evenGrid.TryGetScalar(index, out var value)
                ? value ?? defaultValue
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaMatrixEvenStorage<T> evenGrid, int index1, int index2, Func<T> defaultValueFunc)
        {
            return index1 >= 0 && index2 >= 0 && evenGrid.TryGetScalar((ulong) index1, (ulong) index2, out var value)
                ? value ?? defaultValueFunc()
                : defaultValueFunc();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaMatrixEvenStorage<T> evenGrid, ulong index1, ulong index2, Func<T> defaultValueFunc)
        {
            return evenGrid.TryGetScalar(index1, index2, out var value)
                ? value ?? defaultValueFunc()
                : defaultValueFunc();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaMatrixEvenStorage<T> evenGrid, int index1, int index2, Func<int, int, T> defaultValueFunc)
        {
            return index1 >= 0 && index2 >= 0 && evenGrid.TryGetScalar((ulong) index1, (ulong) index2, out var value)
                ? value ?? defaultValueFunc(index1, index2)
                : defaultValueFunc(index1, index2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaMatrixEvenStorage<T> evenGrid, ulong index1, ulong index2, Func<ulong, ulong, T> defaultValueFunc)
        {
            return evenGrid.TryGetScalar(index1, index2, out var value)
                ? value ?? defaultValueFunc(index1, index2)
                : defaultValueFunc(index1, index2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaMatrixEvenStorage<T> evenGrid, IndexPairRecord index, Func<ulong, ulong, T> defaultValueFunc)
        {
            return evenGrid.TryGetScalar(index, out var value)
                ? value ?? defaultValueFunc(index.Index1, index.Index2)
                : defaultValueFunc(index.Index1, index.Index2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this ILaMatrixEvenStorage<T> evenGrid, int index1, int index2, out T value)
        {
            if (index1 >= 0 && index2 >= 0 && evenGrid.TryGetScalar((ulong) index1, (ulong) index2, out value))
                return true;

            value = default;
            return false;
        }


        /// <summary>
        /// The value corresponding to the smallest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMinKeyValue<T>(this ILaMatrixEvenStorage<T> evenGrid)
        {
            return evenGrid.GetScalar(evenGrid.GetMinIndex());
        }

        /// <summary>
        /// The value corresponding to the largest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMaxKeyValue<T>(this ILaMatrixEvenStorage<T> evenGrid)
        {
            return evenGrid.GetScalar(evenGrid.GetMaxIndex());
        }


        /// <summary>
        /// The smallest index and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> GetMinKeyValueRecord<T>(this ILaMatrixEvenStorage<T> evenGrid)
        {
            var index = evenGrid.GetMinIndex();

            return new IndexPairScalarRecord<T>(index.Index1, index.Index2, evenGrid.GetScalar(index));
        }

        /// <summary>
        /// The largest index and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> GetMaxKeyValueRecord<T>(this ILaMatrixEvenStorage<T> evenGrid)
        {
            var index = evenGrid.GetMaxIndex();

            return new IndexPairScalarRecord<T>(index.Index1, index.Index2, evenGrid.GetScalar(index));
        }

        /// <summary>
        /// Convert this structure to a read only list and replace empty
        /// indexs using defaultValueFunc
        /// </summary>
        /// <param name="evenGrid"></param>
        /// <param name="count1"></param>
        /// <param name="count2"></param>
        /// <param name="defaultValueFunc"></param>
        /// <returns></returns>
        public static T[,] ToArray<T>(this ILaMatrixEvenStorage<T> evenGrid, int count1, int count2, Func<int, int, T> defaultValueFunc)
        {
            var array = new T[count1, count2];

            var emptyKeys = 
                evenGrid.GetEmptyIndices((ulong) count1 - 1, (ulong) count2 - 1);

            foreach (var (index1, index2) in emptyKeys)
                array[index1, index2] = defaultValueFunc((int) index1, (int) index2);

            foreach (var (index1, index2, value) in evenGrid.GetIndexScalarRecords())
                array[index1, index2] = value;

            return array;
        }

        /// <summary>
        /// Convert this structure to a read only list and replace empty
        /// indexs using defaultValue
        /// </summary>
        /// <param name="evenGrid"></param>
        /// <param name="count1"></param>
        /// <param name="count2"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T[,] ToArray<T>(this ILaMatrixEvenStorage<T> evenGrid, int count1, int count2, T defaultValue)
        {
            var array = new T[count1, count2];
            
            var emptyKeys = 
                evenGrid.GetEmptyIndices((ulong) count1 - 1, (ulong) count2 - 1);

            foreach (var (index1, index2) in emptyKeys)
                array[index1, index2] = defaultValue;

            foreach (var (index1, index2, value) in evenGrid.GetIndexScalarRecords())
                array[index1, index2] = value;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ToArray<T>(this ILaMatrixEvenStorage<T> evenGrid)
        {
            var (count1, count2) = evenGrid.GetDenseCountPair();

            var array = new T[count1, count2];

            foreach (var (index1, index2, value) in evenGrid.GetIndexScalarRecords())
                array[index1, index2] = value;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ToArray<T>(this ILaMatrixEvenStorage<T> evenGrid, T defaultValue)
        {
            var (count1, count2) = evenGrid.GetDenseCountPair();
            var array = new T[count1, count2];
            
            var emptyKeys = 
                evenGrid.GetEmptyIndices((ulong) count1 - 1, (ulong) count2 - 1);

            foreach (var (index1, index2) in emptyKeys)
                array[index1, index2] = defaultValue;

            foreach (var (index1, index2, value) in evenGrid.GetIndexScalarRecords())
                array[index1, index2] = value;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ToArray<T>(this ILaMatrixEvenStorage<T> evenGrid, int count1, int count2)
        {
            var array = new T[count1, count2];
            
            foreach (var (index1, index2, value) in evenGrid.GetIndexScalarRecords())
                array[index1, index2] = value;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ToArray<T>(this ILaMatrixEvenStorage<T> evenGrid, Func<T> defaultValueFunc)
        {
            var (count1, count2) = evenGrid.GetDenseCountPair();
            var array = new T[count1, count2];

            var emptyKeys = 
                evenGrid.GetEmptyIndices((ulong) count1 - 1, (ulong) count2 - 1);

            foreach (var (index1, index2) in emptyKeys)
                array[index1, index2] = defaultValueFunc();

            foreach (var (index1, index2, value) in evenGrid.GetIndexScalarRecords())
                array[index1, index2] = value;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ToArray<T>(this ILaMatrixEvenStorage<T> evenGrid, Func<int, int, T> defaultValueFunc)
        {
            var (count1, count2) = evenGrid.GetDenseCountPair();
            var array = new T[count1, count2];

            var emptyKeys = 
                evenGrid.GetEmptyIndices((ulong) count1 - 1, (ulong) count2 - 1);

            foreach (var (index1, index2) in emptyKeys)
                array[index1, index2] = defaultValueFunc((int) index1, (int) index2);

            foreach (var (index1, index2, value) in evenGrid.GetIndexScalarRecords())
                array[index1, index2] = value;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] ToArray<T>(this ILaMatrixEvenStorage<T> evenGrid, int count1, int count2, Func<T> defaultValueFunc)
        {
            var array = new T[count1, count2];

            var emptyKeys = 
                evenGrid.GetEmptyIndices((ulong) count1 - 1, (ulong) count2 - 1);

            foreach (var (index1, index2) in emptyKeys)
                array[index1, index2] = defaultValueFunc();

            foreach (var (index1, index2, value) in evenGrid.GetIndexScalarRecords())
                array[index1, index2] = value;

            return array;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<uint> GetMinVSpaceDimensionOfVector<T>(this ILaMatrixEvenStorage<T> indexScalarGrid)
        {
            if (indexScalarGrid.IsEmpty())
                return new Pair<uint>(0U, 0U);

            return indexScalarGrid
                .GetMaxIndex()
                .MapKeys(GaBasisVectorUtils.BasisVectorIndexToMinVSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<uint> GetMinVSpaceDimensionOfBivector<T>(this ILaMatrixEvenStorage<T> indexScalarGrid)
        {
            if (indexScalarGrid.IsEmpty())
                return new Pair<uint>(0U, 0U);

            return indexScalarGrid
                .GetMaxIndex()
                .MapKeys(GaBasisBivectorUtils.BasisBivectorIndexToMinVSpaceDimension);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<uint> GetMinVSpaceDimensionOfKVector<T>(this ILaMatrixEvenStorage<T> indexScalarGrid, uint grade)
        {
            if (indexScalarGrid.IsEmpty())
                return new Pair<uint>(0U, 0U);

            return indexScalarGrid
                .GetMaxIndex()
                .MapKeys(index => index.BasisBladeIndexToMinVSpaceDimension(grade));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<uint> GetMinVSpaceDimensionOfMultivector<T>(this ILaMatrixEvenStorage<T> idScalarGrid)
        {
            if (idScalarGrid.IsEmpty())
                return new Pair<uint>(0U, 0U);

            return idScalarGrid
                .GetMaxIndex()
                .MapKeys(GaBasisBladeUtils.BasisBladeIdToMinVSpaceDimension);
        }
    }
}