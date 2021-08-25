using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Structures.Composers;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Factories
{
    public static class GaListEvenFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenComposerSparse<T> CreateEvenListComposer<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaListEvenComposerSparse<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenComposerDense<T> CreateEvenListComposer<T>(this IGaScalarProcessor<T> scalarProcessor, int count)
        {
            return new GaListEvenComposerDense<T>(scalarProcessor, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseRepeatedValue<T> CreateEvenListRepeatedValue<T>(this T value, int count)
        {
            return new GaListEvenDenseRepeatedValue<T>(count, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEvenDense<T> CreateEvenListDense<T>(this T value, int count)
        {
            return count switch
            {
                < 1 => GaListEvenEmpty<T>.EmptyList,
                1 => new GaListEvenSingleKeyZero<T>(value),
                _ => new GaListEvenDenseRepeatedValue<T>(count, value)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenSingleKeyZero<T> CreateEvenListSingleKeyZero<T>(this T value)
        {
            return new GaListEvenSingleKeyZero<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEvenSingleKey<T> CreateEvenListSingleKey<T>(this T value, ulong key)
        {
            return key == 0UL
                ? new GaListEvenSingleKeyZero<T>(value)
                : new GaListEvenSingleKey<T>(key, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEvenSingleKey<T> CreateEvenListSingleKey<T>(this KeyValuePair<ulong, T> kayValuePair)
        {
            var (key, value) = kayValuePair;

            return key == 0UL
                ? new GaListEvenSingleKeyZero<T>(value)
                : new GaListEvenSingleKey<T>(key, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseArray<T> CreateEvenListDenseArray<T>(int count)
        {
            return new GaListEvenDenseArray<T>(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseArray<T> CreateEvenListDenseArray<T>(this T[] valuesArray)
        {
            return new GaListEvenDenseArray<T>(valuesArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseArray<T> CreateEvenListDenseArray<T>(this IEnumerable<T> valuesList)
        {
            return new GaListEvenDenseArray<T>(valuesList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseList<T> CreateEvenListDenseList<T>()
        {
            return new GaListEvenDenseList<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseList<T> CreateEvenListDenseList<T>(int capacity)
        {
            return new GaListEvenDenseList<T>(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseList<T> CreateEvenListDenseList<T>(this IEnumerable<T> valuesList)
        {
            return new GaListEvenDenseList<T>(valuesList);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseGridSlice1<T> CreateEvenListDenseGridSlice1<T>(this IGaGridEven<T> grid, ulong key1)
        {
            return new GaListEvenDenseGridSlice1<T>(grid, key1, (_, _) => default);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseGridSlice1<T> CreateEvenListDenseGridSlice1<T>(this IGaGridEven<T> grid, ulong key1, T defaultValue)
        {
            return new GaListEvenDenseGridSlice1<T>(grid, key1, (_, _) => defaultValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseGridSlice1<T> CreateEvenListDenseGridSlice1<T>(this IGaGridEven<T> grid, ulong key1, Func<T> defaultValueFunc)
        {
            return new GaListEvenDenseGridSlice1<T>(grid, key1, (_, _) => defaultValueFunc());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseGridSlice1<T> CreateEvenListDenseGridSlice1<T>(this IGaGridEven<T> grid, ulong key1, Func<ulong, ulong, T> defaultValueFunc)
        {
            return new GaListEvenDenseGridSlice1<T>(grid, key1, defaultValueFunc);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseGridSlice2<T> CreateEvenListDenseGridSlice2<T>(this IGaGridEven<T> grid, ulong key2)
        {
            return new GaListEvenDenseGridSlice2<T>(grid, key2, (_, _) => default);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseGridSlice2<T> CreateEvenListDenseGridSlice2<T>(this IGaGridEven<T> grid, ulong key2, T defaultValue)
        {
            return new GaListEvenDenseGridSlice2<T>(grid, key2, (_, _) => defaultValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseGridSlice2<T> CreateEvenListDenseGridSlice2<T>(this IGaGridEven<T> grid, ulong key2, Func<T> defaultValueFunc)
        {
            return new GaListEvenDenseGridSlice2<T>(grid, key2, (_, _) => defaultValueFunc());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseGridSlice2<T> CreateEvenListDenseGridSlice2<T>(this IGaGridEven<T> grid, ulong key2, Func<ulong, ulong, T> defaultValueFunc)
        {
            return new GaListEvenDenseGridSlice2<T>(grid, key2, defaultValueFunc);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseGridSlice<T> CreateEvenListDenseGridSlice<T>(this IGaGridEven<T> grid, int count, Func<ulong, GaRecordKeyPair> keyMapping)
        {
            return new GaListEvenDenseGridSlice<T>(grid, count, keyMapping, (_, _) => default);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseGridSlice<T> CreateEvenListDenseGridSlice<T>(this IGaGridEven<T> grid, int count, Func<ulong, GaRecordKeyPair> keyMapping, T defaultValue)
        {
            return new GaListEvenDenseGridSlice<T>(grid, count, keyMapping, (_, _) => defaultValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseGridSlice<T> CreateEvenListDenseGridSlice<T>(this IGaGridEven<T> grid, int count, Func<ulong, GaRecordKeyPair> keyMapping, Func<T> defaultValueFunc)
        {
            return new GaListEvenDenseGridSlice<T>(grid, count, keyMapping, (_, _) => defaultValueFunc());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseGridSlice<T> CreateEvenListDenseGridSlice<T>(this IGaGridEven<T> grid, int count, Func<ulong, GaRecordKeyPair> keyMapping, Func<ulong, ulong, T> defaultValueFunc)
        {
            return new GaListEvenDenseGridSlice<T>(grid, count, keyMapping, defaultValueFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDenseMapped<T> CreateEvenListDenseMapped<T>(this IGaListEvenDense<T> evenDictionaryList, Func<ulong, T, T> keyValueMapping)
        {
            return new GaListEvenDenseMapped<T>(evenDictionaryList, keyValueMapping);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenDensePermuted<T> CreateEvenListDensePermuted<T>(this IGaListEvenDense<T> evenDictionaryList, Func<ulong, ulong> keyMapping)
        {
            return new GaListEvenDensePermuted<T>(evenDictionaryList, keyMapping);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEvenDense<T> CreateEvenListDense<T>(this IEnumerable<T> valuesList)
        {
            return CreateEvenListDense(valuesList.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEvenDense<T> CreateEvenListDense<T>(this IReadOnlyList<T> valuesList)
        {
            return valuesList.Count switch
            {
                0 => GaListEvenEmpty<T>.EmptyList,
                1 => new GaListEvenSingleKeyZero<T>(valuesList[0]),
                _ => new GaListEvenDenseList<T>(valuesList)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenSparse<T> CreateEvenListSparse<T>(this Dictionary<ulong, T> valuesDictionary)
        {
            return new GaListEvenSparse<T>(valuesDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> CreateEvenList<T>(this Dictionary<ulong, T> valuesDictionary)
        {
            return valuesDictionary.Count switch
            {
                0 => GaListEvenEmpty<T>.EmptyList,
                1 => valuesDictionary.First().CreateEvenListSingleKey(),
                _ => new GaListEvenSparse<T>(valuesDictionary)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenTree<T> CreateEvenListTree<T>(this IReadOnlyList<ulong> idsList, int treeDepth)
        {
            return new GaListEvenTree<T>(treeDepth, idsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenTree<T> CreateEvenListTree<T>(this IReadOnlyList<ulong> leafNodeIDsList, IReadOnlyCollection<T> leafNodeValuesList, int treeDepth)
        {
            return new GaListEvenTree<T>(treeDepth, leafNodeIDsList, leafNodeValuesList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenTree<T> CreateEvenListTree<T>(this IReadOnlyDictionary<ulong, T> leafNodes)
        {
            var treeDepth = 
                (int) leafNodes.Keys.GetMinVSpaceDimension();

            return new GaListEvenTree<T>(treeDepth, leafNodes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenTree<T> CreateEvenListTree<T>(this IReadOnlyDictionary<ulong, T> leafNodes, int treeDepth)
        {
            return new GaListEvenTree<T>(treeDepth, leafNodes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenTree<T> CopyToEvenListTree<T>(this GaListEvenTree<T> binaryTree)
        {
            return new GaListEvenTree<T>(binaryTree);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListEvenTree<T> CopyToEvenListTree<T>(this GaListEvenTree<T> binaryTree, int treeDepth)
        {
            return new GaListEvenTree<T>(treeDepth, binaryTree);
        }
    }
}