using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class LaMatrixEvenStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixSparseEvenStorageComposer<T> CreateLaMatrixSparseEvenStorageComposer<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return new LaMatrixSparseEvenStorageComposer<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixDenseEvenStorageComposer<T> CreateLaMatrixDenseEvenStorageComposer<T>(this IScalarProcessor<T> scalarProcessor, int count1, int count2)
        {
            return new LaMatrixDenseEvenStorageComposer<T>(scalarProcessor, count1, count2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixDiagonalStorage<T> CreateLaMatrixDiagonalStorage<T>(this ILaVectorEvenStorage<T> sourceList)
        {
            return new LaMatrixDiagonalStorage<T>(sourceList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> CreateLaMatrixRowStorage<T>(this ILaVectorEvenStorage<T> sourceList)
        {
            return sourceList is ILaVectorDenseEvenStorage<T> denseList
                ? new LaMatrixDenseRowStorage<T>(denseList)
                : new LaMatrixRowStorage<T>(0, sourceList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> CreateLaMatrixRowStorage<T>(this ILaVectorEvenStorage<T> sourceList, ulong index1)
        {
            return index1 == 0 && sourceList is ILaVectorDenseEvenStorage<T> denseList
                ? new LaMatrixDenseRowStorage<T>(denseList)
                : new LaMatrixRowStorage<T>(index1, sourceList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> CreateLaMatrixColumnStorage<T>(this ILaVectorEvenStorage<T> sourceList)
        {
            return sourceList is ILaVectorDenseEvenStorage<T> denseList
                ? new LaMatrixDenseColumnStorage<T>(denseList)
                : new LaMatrixColumnStorage<T>(0, sourceList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> CreateLaMatrixColumnStorage<T>(this ILaVectorEvenStorage<T> sourceList, ulong index2)
        {
            return index2 == 0 && sourceList is ILaVectorDenseEvenStorage<T> denseList
                ? new LaMatrixDenseColumnStorage<T>(denseList)
                : new LaMatrixColumnStorage<T>(index2, sourceList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixRepeatedScalarStorage<T> CreateLaMatrixRepeatedScalarStorage<T>(this T value, int count1, int count2)
        {
            return new LaMatrixRepeatedScalarStorage<T>(count1, count2, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixDenseRowStorage<T> CreateLaMatrixRowStorage<T>(this ILaVectorDenseEvenStorage<T> sourceList)
        {
            return new LaMatrixDenseRowStorage<T>(sourceList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixDenseColumnStorage<T> CreateLaMatrixColumnStorage<T>(this ILaVectorDenseEvenStorage<T> sourceList)
        {
            return new LaMatrixDenseColumnStorage<T>(sourceList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixDenseEvenStorage<T> CreateLaMatrixDenseEvenStorage<T>(this T value, int count1, int count2)
        {
            if (count1 < 0 || count2 < 0)
                throw new ArgumentOutOfRangeException();

            var count = count1 * count2;

            return count switch
            {
                < 1 => LaMatrixEmptyStorage<T>.EmptyMatrix,
                1 => new LaMatrixZeroIndexStorage<T>(value),
                _ => new LaMatrixRepeatedScalarStorage<T>(count1, count2, value)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixEmptyStorage<T> CreateLaMatrixEmptyStorage<T>()
        {
            return LaMatrixEmptyStorage<T>.EmptyMatrix;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixZeroIndexStorage<T> CreateLaMatrixZeroIndexStorage<T>(this T value)
        {
            return new LaMatrixZeroIndexStorage<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixSingleIndexEvenStorage<T> CreateEvenGridSingleKey<T>(this T value, ulong index1, ulong index2)
        {
            return index1 == 0UL && index2 == 0UL
                ? new LaMatrixZeroIndexStorage<T>(value)
                : new LaMatrixSingleIndexStorage<T>(index1, index2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixSingleIndexEvenStorage<T> CreateEvenGridSingleKey<T>(this IndexPairRecord index, T value)
        {
            var (index1, index2) = index;

            return index1 == 0UL && index2 == 0UL
                ? new LaMatrixZeroIndexStorage<T>(value)
                : new LaMatrixSingleIndexStorage<T>(index, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixSingleIndexEvenStorage<T> CreateEvenGridSingleKey<T>(this KeyValuePair<IndexPairRecord, T> indexValue)
        {
            var ((index1, index2), value) = indexValue;

            return index1 == 0UL && index2 == 0UL
                ? new LaMatrixZeroIndexStorage<T>(value)
                : new LaMatrixSingleIndexStorage<T>(index1, index2, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixSingleIndexEvenStorage<T> CreateEvenGridSingleKey<T>(this IndexPairScalarRecord<T> indexValue)
        {
            var (index1, index2, value) = indexValue;

            return index1 == 0UL && index2 == 0UL
                ? new LaMatrixZeroIndexStorage<T>(value)
                : new LaMatrixSingleIndexStorage<T>(index1, index1, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixSingleIndexEvenStorage<T> CreateEvenGridSingleKey<T>(this IndexScalarRecord<T> indexValue)
        {
            var (index, value) = indexValue;

            return index == 0UL
                ? new LaMatrixZeroIndexStorage<T>(value)
                : new LaMatrixSingleIndexStorage<T>(index, index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixDenseStorage<T> CreateEvenGridDenseArray<T>(int count1, int count2)
        {
            return new LaMatrixDenseStorage<T>(count1, count2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixDenseStorage<T> CreateEvenGridDenseArray<T>(this T[,] valuesArray)
        {
            return new LaMatrixDenseStorage<T>(valuesArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixMappedDenseStorage<T> CreateEvenGridDenseMapped<T>(this ILaMatrixDenseEvenStorage<T> evenDictionaryGrid, Func<ulong, ulong, IndexPairRecord> indexMapping)
        {
            return new LaMatrixMappedDenseStorage<T>(evenDictionaryGrid, indexMapping);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixMappedDenseStorage<T> CreateEvenGridDenseMapped<T>(this ILaMatrixDenseEvenStorage<T> evenDictionaryGrid, Func<ulong, ulong, T, T> indexValueMapping)
        {
            return new LaMatrixMappedDenseStorage<T>(evenDictionaryGrid, indexValueMapping);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixMappedDenseStorage<T> CreateEvenGridDenseMapped<T>(this ILaMatrixDenseEvenStorage<T> evenDictionaryGrid, Func<ulong, ulong, IndexPairRecord> indexMapping, Func<ulong, ulong, T, T> indexValueMapping)
        {
            return new LaMatrixMappedDenseStorage<T>(evenDictionaryGrid, indexMapping, indexValueMapping);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixComputedStorage<T> CreateEvenGridDensePermuted<T>(int count1, int count2, Func<ulong, ulong, T> indexToScalarMapping)
        {
            return new LaMatrixComputedStorage<T>(count1, count2, indexToScalarMapping);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixDenseEvenStorage<T> CreateEvenGridDense<T>(this T[,] valuesArray)
        {
            return valuesArray.Length switch
            {
                0 => LaMatrixEmptyStorage<T>.EmptyMatrix,
                1 => new LaMatrixZeroIndexStorage<T>(valuesArray[0, 0]),
                _ => new LaMatrixDenseStorage<T>(valuesArray)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaMatrixSparseStorage<T> CreateEvenGridSparse<T>(this Dictionary<IndexPairRecord, T> valuesDictionary)
        {
            return new LaMatrixSparseStorage<T>(valuesDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> CreateEvenGrid<T>(this Dictionary<IndexPairRecord, T> valuesDictionary)
        {
            return valuesDictionary.Count switch
            {
                0 => LaMatrixEmptyStorage<T>.EmptyMatrix,
                1 => CreateEvenGridSingleKey(valuesDictionary.First()),
                _ => new LaMatrixSparseStorage<T>(valuesDictionary)
            };
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> CreateLaMatrixRowStorage<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> array)
        {
            return array.CreateLaMatrixRowStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> CreateLaMatrixRowStorage<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> array, int rowIndex)
        {
            return array.GetRow((ulong) rowIndex).CreateLaMatrixRowStorage((ulong) rowIndex);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> CreateLaMatrixColumnStorage<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> array)
        {
            return array.CreateLaMatrixColumnStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> CreateColumnVectorGrid<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> array, int columnIndex)
        {
            return array.GetColumn((ulong) columnIndex).CreateLaMatrixColumnStorage((ulong) columnIndex);
        }
    }
}