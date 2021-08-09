using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;

namespace GeometricAlgebraFulcrumLib.Structures.Even
{
    public static class GaEvenDictionaryFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaEvenDictionary<T> CreateEvenDictionaryRepeatedValue<T>(this T value, int count)
        {
            return count switch
            {
                < 1 => GaEvenDictionaryEmpty<T>.DefaultDictionary,
                1 => new GaEvenDictionarySingleZeroKey<T>(value),
                _ => new GaEvenDictionaryRepeatedValue<T>(count, value)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaEvenDictionary<T> CreateEvenDictionarySingleZeroKey<T>(this T value)
        {
            return new GaEvenDictionarySingleZeroKey<T>(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaEvenDictionary<T> CreateEvenDictionarySingleKey<T>(this T value, ulong key)
        {
            return key == 0UL
                ? new GaEvenDictionarySingleZeroKey<T>(value)
                : new GaEvenDictionarySingleKey<T>(key, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaEvenDictionary<T> CreateEvenDictionarySingleKey<T>(this ulong key, T value)
        {
            return key == 0UL
                ? new GaEvenDictionarySingleZeroKey<T>(value)
                : new GaEvenDictionarySingleKey<T>(key, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaEvenDictionary<T> CreateEvenDictionaryList<T>(this IEnumerable<T> valuesList)
        {
            return CreateEvenDictionaryList(valuesList.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaEvenDictionary<T> CreateEvenDictionaryList<T>(this IReadOnlyList<T> valuesList)
        {
            return valuesList.Count switch
            {
                0 => GaEvenDictionaryEmpty<T>.DefaultDictionary,
                1 => new GaEvenDictionarySingleZeroKey<T>(valuesList[0]),
                _ => new GaEvenDictionaryList<T>(valuesList)
            };
        }

        public static IGaEvenDictionary<T> CreateEvenDictionary<T>(this Dictionary<ulong, T> valuesDictionary)
        {
            if (valuesDictionary.Count == 0)
                return GaEvenDictionaryEmpty<T>.DefaultDictionary;

            if (valuesDictionary.Count != 1) 
                return new GaEvenDictionary<T>(valuesDictionary);

            var (key, value) = valuesDictionary.First();

            return key == 0
                ? new GaEvenDictionarySingleZeroKey<T>(value)
                : new GaEvenDictionarySingleKey<T>(key, value);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaEvenDictionaryTree<T> CreateEvenDictionaryTree<T>(this IReadOnlyList<ulong> idsList, int treeDepth)
        {
            return new GaEvenDictionaryTree<T>(treeDepth, idsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaEvenDictionaryTree<T> CreateEvenDictionaryTree<T>(this IReadOnlyList<ulong> leafNodeIDsList, IReadOnlyCollection<T> leafNodeValuesList, int treeDepth)
        {
            return new GaEvenDictionaryTree<T>(treeDepth, leafNodeIDsList, leafNodeValuesList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaEvenDictionaryTree<T> CreateEvenDictionaryTree<T>(this IReadOnlyDictionary<ulong, T> leafNodes)
        {
            var treeDepth = 
                (int) leafNodes.Keys.GetMinVSpaceDimension();

            return new GaEvenDictionaryTree<T>(treeDepth, leafNodes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaEvenDictionaryTree<T> CreateEvenDictionaryTree<T>(this IReadOnlyDictionary<ulong, T> leafNodes, int treeDepth)
        {
            return new GaEvenDictionaryTree<T>(treeDepth, leafNodes);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaEvenDictionaryTree<T> CopyToEvenDictionaryTree<T>(this GaEvenDictionaryTree<T> binaryTree)
        {
            return new GaEvenDictionaryTree<T>(binaryTree);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaEvenDictionaryTree<T> CopyToEvenDictionaryTree<T>(this GaEvenDictionaryTree<T> binaryTree, int treeDepth)
        {
            return new GaEvenDictionaryTree<T>(treeDepth, binaryTree);
        }
    }
}