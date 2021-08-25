using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Structures.Composers;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Factories
{
    public static class GaGridEvenFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridEvenComposerSparse<T> CreateEvenGridComposer<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaGridEvenComposerSparse<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridEvenComposerDense<T> CreateEvenGridComposer<T>(this IGaScalarProcessor<T> scalarProcessor, int count1, int count2)
        {
            return new GaGridEvenComposerDense<T>(scalarProcessor, count1, count2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridEvenDiagonal<T> CreateEvenGridDiagonal<T>(this IGaListEven<T> sourceList)
        {
            return new GaGridEvenDiagonal<T>(sourceList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> CreateEvenGridRow<T>(this IGaListEven<T> sourceList)
        {
            return sourceList is IGaListEvenDense<T> denseList
                ? new GaGridEvenDenseRow<T>(denseList)
                : new GaGridEvenRow<T>(0, sourceList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> CreateEvenGridRow<T>(this IGaListEven<T> sourceList, ulong key1)
        {
            return key1 == 0 && sourceList is IGaListEvenDense<T> denseList
                ? new GaGridEvenDenseRow<T>(denseList)
                : new GaGridEvenRow<T>(key1, sourceList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> CreateEvenGridColumn<T>(this IGaListEven<T> sourceList)
        {
            return sourceList is IGaListEvenDense<T> denseList
                ? new GaGridEvenDenseColumn<T>(denseList)
                : new GaGridEvenColumn<T>(0, sourceList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> CreateEvenGridColumn<T>(this IGaListEven<T> sourceList, ulong key2)
        {
            return key2 == 0 && sourceList is IGaListEvenDense<T> denseList
                ? new GaGridEvenDenseColumn<T>(denseList)
                : new GaGridEvenColumn<T>(key2, sourceList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridEvenDenseRepeatedValue<T> CreateEvenGridRepeatedValue<T>(this T value, int count1, int count2)
        {
            return new GaGridEvenDenseRepeatedValue<T>(count1, count2, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridEvenDenseRow<T> CreateEvenGridRow<T>(this IGaListEvenDense<T> sourceList)
        {
            return new GaGridEvenDenseRow<T>(sourceList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridEvenDenseColumn<T> CreateEvenGridColumn<T>(this IGaListEvenDense<T> sourceList)
        {
            return new GaGridEvenDenseColumn<T>(sourceList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEvenDense<T> CreateEvenGridDense<T>(this T value, int count1, int count2)
        {
            if (count1 < 0 || count2 < 0)
                throw new ArgumentOutOfRangeException();

            var count = count1 * count2;

            return count switch
            {
                < 1 => GaGridEvenEmpty<T>.EmptyGrid,
                1 => new GaGridEvenSingleKeyZero<T>(value),
                _ => new GaGridEvenDenseRepeatedValue<T>(count1, count2, value)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridEvenSingleKeyZero<T> CreateEvenGridSingleZeroKey<T>(this T value)
        {
            return new GaGridEvenSingleKeyZero<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEvenSingleKey<T> CreateEvenGridSingleKey<T>(this T value, ulong key1, ulong key2)
        {
            return key1 == 0UL && key2 == 0UL
                ? new GaGridEvenSingleKeyZero<T>(value)
                : new GaGridEvenSingleKey<T>(key1, key2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEvenSingleKey<T> CreateEvenGridSingleKey<T>(this GaRecordKeyPair key, T value)
        {
            var (key1, key2) = key;

            return key1 == 0UL && key2 == 0UL
                ? new GaGridEvenSingleKeyZero<T>(value)
                : new GaGridEvenSingleKey<T>(key, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEvenSingleKey<T> CreateEvenGridSingleKey<T>(this KeyValuePair<GaRecordKeyPair, T> keyValue)
        {
            var ((key1, key2), value) = keyValue;

            return key1 == 0UL && key2 == 0UL
                ? new GaGridEvenSingleKeyZero<T>(value)
                : new GaGridEvenSingleKey<T>(key1, key2, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEvenSingleKey<T> CreateEvenGridSingleKey<T>(this GaRecordKeyPairValue<T> keyValue)
        {
            var (key1, key2, value) = keyValue;

            return key1 == 0UL && key2 == 0UL
                ? new GaGridEvenSingleKeyZero<T>(value)
                : new GaGridEvenSingleKey<T>(key1, key1, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEvenSingleKey<T> CreateEvenGridSingleKey<T>(this GaRecordKeyValue<T> keyValue)
        {
            var (key, value) = keyValue;

            return key == 0UL
                ? new GaGridEvenSingleKeyZero<T>(value)
                : new GaGridEvenSingleKey<T>(key, key, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridEvenDenseArray<T> CreateEvenGridDenseArray<T>(int count1, int count2)
        {
            return new GaGridEvenDenseArray<T>(count1, count2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridEvenDenseArray<T> CreateEvenGridDenseArray<T>(this T[,] valuesArray)
        {
            return new GaGridEvenDenseArray<T>(valuesArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridEvenDenseMapped<T> CreateEvenGridDenseMapped<T>(this GaGridEvenDenseBase<T> evenDictionaryGrid, Func<ulong, ulong, T, T> keyValueMapping)
        {
            return new GaGridEvenDenseMapped<T>(evenDictionaryGrid, keyValueMapping);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridEvenDensePermuted<T> CreateEvenGridDensePermuted<T>(this GaGridEvenDenseBase<T> evenDictionaryGrid, Func<ulong, ulong, GaRecordKeyPair> keyMapping)
        {
            return new GaGridEvenDensePermuted<T>(evenDictionaryGrid, keyMapping);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEvenDense<T> CreateEvenGridDense<T>(this T[,] valuesArray)
        {
            return valuesArray.Length switch
            {
                0 => GaGridEvenEmpty<T>.EmptyGrid,
                1 => new GaGridEvenSingleKeyZero<T>(valuesArray[0, 0]),
                _ => new GaGridEvenDenseArray<T>(valuesArray)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGridEvenSparse<T> CreateEvenGridSparse<T>(this Dictionary<GaRecordKeyPair, T> valuesDictionary)
        {
            return new GaGridEvenSparse<T>(valuesDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> CreateEvenGrid<T>(this Dictionary<GaRecordKeyPair, T> valuesDictionary)
        {
            return valuesDictionary.Count switch
            {
                0 => GaGridEvenEmpty<T>.EmptyGrid,
                1 => CreateEvenGridSingleKey(valuesDictionary.First()),
                _ => new GaGridEvenSparse<T>(valuesDictionary)
            };
        }
    }
}