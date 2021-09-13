using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Lists;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GaFuLStructureFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLListDense<T> CreateGaFuLDenseList<T>()
        {
            return GaFuLListDense<T>.Create();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLListDense<T> CreateGaFuLDenseList<T>(int capacity)
        {
            return GaFuLListDense<T>.Create(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLListDense<T> CreateGaFuLDenseList<T>(this IEnumerable<T> itemsList)
        {
            return GaFuLListDense<T>.Create(itemsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLListDense<T> CreateGaFuLDenseList<T>(this List<T> itemsList)
        {
            return GaFuLListDense<T>.Create(itemsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLListSparse<T> CreateGaFuLSparseList<T>()
        {
            return GaFuLListSparse<T>.Create();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLListSparse<T> CreateGaFuLSparseList<T>(T defaultValue)
        {
            return GaFuLListSparse<T>.Create(defaultValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLListSparse<T> CreateGaFuLSparseList<T>(this IEnumerable<KeyValuePair<ulong, T>> indexValuePairs, T defaultValue)
        {
            return GaFuLListSparse<T>.Create(defaultValue, indexValuePairs);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLListSparse<T> CreateGaFuLSparseList<T>(this SortedDictionary<ulong, T> itemsDictionary, T defaultValue)
        {
            return GaFuLListSparse<T>.Create(defaultValue, itemsDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLReadOnlyListComputedValues<T> CreateGaFuLComputedValuesList<T>(int count, Func<ulong, T> valueFunc)
        {
            return GaFuLReadOnlyListComputedValues<T>.Create(count, valueFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLReadOnlyListRepeatedValue<T> CreateGaFuLRepeatedValueList<T>(this T value, int count)
        {
            return GaFuLReadOnlyListRepeatedValue<T>.Create(value, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLReadOnlyListSingleValue<T> CreateGaFuLSingleValueList<T>(this T value, ulong index)
        {
            return GaFuLReadOnlyListSingleValue<T>.Create(value, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLReadOnlyListSingleValue<T> CreateGaFuLSingleValueList<T>(this T value, ulong index, int count)
        {
            return GaFuLReadOnlyListSingleValue<T>.Create(value, index, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLReadOnlyListSingleValue<T> CreateGaFuLSingleValueList<T>(this T value, ulong index, T defaultValue)
        {
            return GaFuLReadOnlyListSingleValue<T>.Create(value, index, (int) index + 1, defaultValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaFuLReadOnlyListSingleValue<T> CreateGaFuLSingleValueList<T>(this T value, ulong index, int count, T defaultValue)
        {
            return GaFuLReadOnlyListSingleValue<T>.Create(value, index, count, defaultValue);
        }
    }
}