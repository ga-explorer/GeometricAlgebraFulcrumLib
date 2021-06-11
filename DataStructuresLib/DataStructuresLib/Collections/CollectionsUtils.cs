using System;
using System.Collections.Generic;
using DataStructuresLib.Basic;
using DataStructuresLib.Collections.PeriodicLists2D;

namespace DataStructuresLib.Collections
{
    public static class CollectionsUtils
    {
        public static Pair<T> GetItemsPair<T>(this IReadOnlyList<T> inputsList, IPair<int> itemIndices)
        {
            return new Pair<T>(
                inputsList[itemIndices.Item1],
                inputsList[itemIndices.Item2]
            );
        }

        public static Pair<T> GetItemsPair<T>(this IReadOnlyList<T> inputsList, int index1)
        {
            return new Pair<T>(
                inputsList[index1],
                inputsList[index1 + 1]
            );
        }

        public static Pair<T> GetItemsPair<T>(this IReadOnlyList<T> inputsList, int index1, int index2)
        {
            return new Pair<T>(
                inputsList[index1],
                inputsList[index2]
            );
        }


        public static Triplet<T> GetItemsTriplet<T>(this IReadOnlyList<T> inputsList, ITriplet<int> itemIndices)
        {
            return new Triplet<T>(
                inputsList[itemIndices.Item1],
                inputsList[itemIndices.Item2],
                inputsList[itemIndices.Item3]
            );
        }

        public static Triplet<T> GetItemsTriplet<T>(this IReadOnlyList<T> inputsList, int index1, int index2, int index3)
        {
            return new Triplet<T>(
                inputsList[index1],
                inputsList[index2],
                inputsList[index3]
            );
        }

        public static Triplet<T> GetItemsTriplet<T>(this IReadOnlyList<T> inputsList, int index1)
        {
            return new Triplet<T>(
                inputsList[index1],
                inputsList[index1 + 1],
                inputsList[index1 + 2]
            );
        }


        public static Quad<T> GetItemsQuad<T>(this IReadOnlyList<T> inputsList, IQuad<int> itemIndices)
        {
            return new Quad<T>(
                inputsList[itemIndices.Item1],
                inputsList[itemIndices.Item2],
                inputsList[itemIndices.Item3],
                inputsList[itemIndices.Item4]
            );
        }

        public static Quad<T> GetItemsQuad<T>(this IReadOnlyList<T> inputsList, int index1, int index2, int index3, int index4)
        {
            return new Quad<T>(
                inputsList[index1],
                inputsList[index2],
                inputsList[index3],
                inputsList[index4]
            );
        }

        public static Quad<T> GetItemsQuad<T>(this IReadOnlyList<T> inputsList, int index1)
        {
            return new Quad<T>(
                inputsList[index1],
                inputsList[index1 + 1],
                inputsList[index1 + 2],
                inputsList[index1 + 3]
            );
        }

        
        public static int GetItemIndex<T>(this IReadOnlyList2D<T> inputList, int index1, int index2)
        {
            index1 = index1.Mod(inputList.Count1);
            index2 = index2.Mod(inputList.Count2);

            return index1 + index2 * inputList.Count1;
        }
        
        public static Tuple<int, int> GetItemIndexTuple<T>(this IPeriodicReadOnlyList2D<T> inputList, int index)
        {
            index = index.Mod(inputList.Count);

            var index1 = index % inputList.Count1;
            var index2 = (index - index1) / inputList.Count1;

            return new Tuple<int, int>(index1, index2);
        }
        
        public static Tuple<int, int> GetItemIndexTuple<T>(this IReadOnlyList2D<T> inputList, int index)
        {
            index = index.Mod(inputList.Count);

            var index1 = index % inputList.Count1;
            var index2 = (index - index1) / inputList.Count1;

            return new Tuple<int, int>(index1, index2);
        }

        public static Pair<int> GetItemIndexPair<T>(this IReadOnlyList2D<T> inputList, int index)
        {
            index = index.Mod(inputList.Count);

            var index1 = index % inputList.Count1;
            var index2 = (index - index1) / inputList.Count1;

            return new Pair<int>(index1, index2);
        }


        public static bool IsNullOrEmpty<T>(this IReadOnlyCollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }
    }
}
