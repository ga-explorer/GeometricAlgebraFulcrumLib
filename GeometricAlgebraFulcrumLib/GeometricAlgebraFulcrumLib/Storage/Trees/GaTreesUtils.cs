using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Storage.Trees
{
    public static class GaTreesUtils
    {
        public static GaBinaryTree<T> GetBinaryIndexedSparseList<T>(this IReadOnlyDictionary<ulong, T> sourceDictionary, int treeDepth)
        {
            return new(
                treeDepth,
                sourceDictionary
            );
        }

        public static GaBinaryTree<T> GetBinaryIndexedSparseList<T>(this IReadOnlyList<ulong> idsList, int treeDepth)
        {
            return new(treeDepth, idsList);
        }
    }
}