using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class LaVectorEvenStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorSparseEvenStorageComposer<T> CreateLaVectorSparseEvenStorageComposer<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return new LaVectorSparseEvenStorageComposer<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorDenseEvenStorageComposer<T> CreateLaVectorDenseEvenStorageComposer<T>(this IScalarProcessor<T> scalarProcessor, int count)
        {
            return new LaVectorDenseEvenStorageComposer<T>(scalarProcessor, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorEmptyStorage<T> CreateLaVectorEmptyStorage<T>()
        {
            return LaVectorEmptyStorage<T>.ZeroStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorRepeatedScalarStorage<T> CreateLaVectorRepeatedScalarStorage<T>(this T value, int count)
        {
            return new LaVectorRepeatedScalarStorage<T>(count, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorDenseEvenStorage<T> CreateLaVectorDenseEvenStorage<T>(this T value, int count)
        {
            return count switch
            {
                < 1 => LaVectorEmptyStorage<T>.ZeroStorage,
                1 => new LaVectorZeroIndexStorage<T>(value),
                _ => new LaVectorRepeatedScalarStorage<T>(count, value)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorZeroIndexStorage<T> CreateLaVectorZeroIndexStorage<T>(this T value)
        {
            return new LaVectorZeroIndexStorage<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorSingleIndexEvenStorage<T> CreateLaVectorSingleIndexEvenStorage<T>(this T value, ulong index)
        {
            return index == 0UL
                ? new LaVectorZeroIndexStorage<T>(value)
                : new LaVectorSingleIndexStorage<T>(index, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorSingleIndexEvenStorage<T> CreateLaVectorSingleIndexEvenStorage<T>(this KeyValuePair<ulong, T> kayValuePair)
        {
            var (index, value) = kayValuePair;

            return index == 0UL
                ? new LaVectorZeroIndexStorage<T>(value)
                : new LaVectorSingleIndexStorage<T>(index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorDenseStorage<T> CreateLaVectorDenseStorage<T>(int count)
        {
            return new LaVectorDenseStorage<T>(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorDenseStorage<T> LaVectorDenseStorage<T>(this T[] valuesArray)
        {
            return new LaVectorDenseStorage<T>(valuesArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorDenseStorage<T> LaVectorDenseStorage<T>(this IEnumerable<T> valuesList)
        {
            return new LaVectorDenseStorage<T>(valuesList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorListStorage<T> CreateLaVectorListStorage<T>()
        {
            return new LaVectorListStorage<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorListStorage<T> CreateLaVectorListStorage<T>(int capacity)
        {
            return new LaVectorListStorage<T>(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorListStorage<T> CreateLaVectorListStorage<T>(this IEnumerable<T> valuesList)
        {
            return new LaVectorListStorage<T>(valuesList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorDenseArrayRowStorage<T> CreateLaVectorRowStorage<T>(this ILaMatrixEvenStorage<T> grid, ulong index1)
        {
            return new LaVectorDenseArrayRowStorage<T>(grid, index1, (_, _) => default);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorDenseArrayRowStorage<T> CreateLaVectorRowStorage<T>(this ILaMatrixEvenStorage<T> grid, ulong index1, T defaultValue)
        {
            return new LaVectorDenseArrayRowStorage<T>(grid, index1, (_, _) => defaultValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorDenseArrayRowStorage<T> CreateLaVectorRowStorage<T>(this ILaMatrixEvenStorage<T> grid, ulong index1, Func<T> defaultValueFunc)
        {
            return new LaVectorDenseArrayRowStorage<T>(grid, index1, (_, _) => defaultValueFunc());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorDenseArrayRowStorage<T> CreateLaVectorRowStorage<T>(this ILaMatrixEvenStorage<T> grid, ulong index1, Func<ulong, ulong, T> defaultValueFunc)
        {
            return new LaVectorDenseArrayRowStorage<T>(grid, index1, defaultValueFunc);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorDenseArrayColumnStorage<T> CreateLaVectorColumnStorage<T>(this ILaMatrixEvenStorage<T> grid, ulong index2)
        {
            return new LaVectorDenseArrayColumnStorage<T>(grid, index2, (_, _) => default);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorDenseArrayColumnStorage<T> CreateLaVectorColumnStorage<T>(this ILaMatrixEvenStorage<T> grid, ulong index2, T defaultValue)
        {
            return new LaVectorDenseArrayColumnStorage<T>(grid, index2, (_, _) => defaultValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorDenseArrayColumnStorage<T> CreateLaVectorColumnStorage<T>(this ILaMatrixEvenStorage<T> grid, ulong index2, Func<T> defaultValueFunc)
        {
            return new LaVectorDenseArrayColumnStorage<T>(grid, index2, (_, _) => defaultValueFunc());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorDenseArrayColumnStorage<T> CreateLaVectorColumnStorage<T>(this ILaMatrixEvenStorage<T> grid, ulong index2, Func<ulong, ulong, T> defaultValueFunc)
        {
            return new LaVectorDenseArrayColumnStorage<T>(grid, index2, defaultValueFunc);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorDenseArraySliceStorage<T> CreateLaVectorSliceStorage<T>(this ILaMatrixEvenStorage<T> grid, int count, Func<ulong, IndexPairRecord> indexMapping)
        {
            return new LaVectorDenseArraySliceStorage<T>(grid, count, indexMapping, (_, _) => default);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorDenseArraySliceStorage<T> CreateLaVectorSliceStorage<T>(this ILaMatrixEvenStorage<T> grid, int count, Func<ulong, IndexPairRecord> indexMapping, T defaultValue)
        {
            return new LaVectorDenseArraySliceStorage<T>(grid, count, indexMapping, (_, _) => defaultValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorDenseArraySliceStorage<T> CreateLaVectorSliceStorage<T>(this ILaMatrixEvenStorage<T> grid, int count, Func<ulong, IndexPairRecord> indexMapping, Func<T> defaultValueFunc)
        {
            return new LaVectorDenseArraySliceStorage<T>(grid, count, indexMapping, (_, _) => defaultValueFunc());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorDenseArraySliceStorage<T> CreateLaVectorSliceStorage<T>(this ILaMatrixEvenStorage<T> grid, int count, Func<ulong, IndexPairRecord> indexMapping, Func<ulong, ulong, T> defaultValueFunc)
        {
            return new LaVectorDenseArraySliceStorage<T>(grid, count, indexMapping, defaultValueFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorComputedStorage<T> CreateLaVectorComputedStorage<T>(int count, Func<ulong, T> indexToScalarMapping)
        {
            return new LaVectorComputedStorage<T>(count, indexToScalarMapping);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorMappedDenseStorage<T> CreateLaVectorMappedStorage<T>(this ILaVectorDenseEvenStorage<T> evenDictionaryList, Func<ulong, ulong> indexMapping)
        {
            return new LaVectorMappedDenseStorage<T>(evenDictionaryList, indexMapping);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorMappedDenseStorage<T> CreateLaVectorMappedStorage<T>(this ILaVectorDenseEvenStorage<T> evenDictionaryList, Func<ulong, T, T> indexScalarMapping)
        {
            return new LaVectorMappedDenseStorage<T>(evenDictionaryList, indexScalarMapping);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorMappedDenseStorage<T> CreateLaVectorMappedStorage<T>(this ILaVectorDenseEvenStorage<T> evenDictionaryList, Func<ulong, ulong> indexMapping, Func<ulong, T, T> indexScalarMapping)
        {
            return new LaVectorMappedDenseStorage<T>(evenDictionaryList, indexMapping, indexScalarMapping);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorDenseEvenStorage<T> CreateLaVectorDenseStorage<T>(this IEnumerable<T> valuesList)
        {
            return CreateLaVectorDenseStorage(valuesList.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorDenseEvenStorage<T> CreateLaVectorDenseStorage<T>(this IReadOnlyList<T> valuesList)
        {
            return valuesList.Count switch
            {
                0 => LaVectorEmptyStorage<T>.ZeroStorage,
                1 => new LaVectorZeroIndexStorage<T>(valuesList[0]),
                _ => new LaVectorListStorage<T>(valuesList)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorSparseStorage<T> CreateLaVectorSparseStorage<T>(this Dictionary<ulong, T> valuesDictionary)
        {
            return new LaVectorSparseStorage<T>(valuesDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> CreateLaVectorStorage<T>(this Dictionary<ulong, T> valuesDictionary)
        {
            return valuesDictionary.Count switch
            {
                0 => LaVectorEmptyStorage<T>.ZeroStorage,
                1 => valuesDictionary.First().CreateLaVectorSingleIndexEvenStorage(),
                _ => new LaVectorSparseStorage<T>(valuesDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorTreeStorage<T> CreateLaVectorTreeStorage<T>(this IReadOnlyList<ulong> idsList, int treeDepth)
        {
            return new LaVectorTreeStorage<T>(treeDepth, idsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorTreeStorage<T> CreateLaVectorTreeStorage<T>(this IReadOnlyList<ulong> leafNodeIDsList, IReadOnlyCollection<T> leafNodeValuesList, int treeDepth)
        {
            return new LaVectorTreeStorage<T>(treeDepth, leafNodeIDsList, leafNodeValuesList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorTreeStorage<T> CreateLaVectorTreeStorage<T>(this IReadOnlyDictionary<ulong, T> leafNodes)
        {
            var treeDepth = 
                (int) leafNodes.Keys.GetMinVSpaceDimension();

            return new LaVectorTreeStorage<T>(treeDepth, leafNodes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorTreeStorage<T> CreateLaVectorTreeStorage<T>(this IReadOnlyDictionary<ulong, T> leafNodes, int treeDepth)
        {
            return new LaVectorTreeStorage<T>(treeDepth, leafNodes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorTreeStorage<T> CopyToLaVectorTreeStorage<T>(this LaVectorTreeStorage<T> binaryTree)
        {
            return new LaVectorTreeStorage<T>(binaryTree);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorTreeStorage<T> CopyToLaVectorTreeStorage<T>(this LaVectorTreeStorage<T> binaryTree, int treeDepth)
        {
            return new LaVectorTreeStorage<T>(treeDepth, binaryTree);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> CreateLaVectorRowStorage<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> grid, int rowIndex)
        {
            return grid.CreateLaVectorRowStorage((ulong) rowIndex, scalarProcessor.ScalarZero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> CreateLaVectorColumnStorage<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> grid, int colIndex)
        {
            return grid.CreateLaVectorColumnStorage((ulong) colIndex, scalarProcessor.ScalarZero);
        }

    }
}