using System.Collections.Generic;
using System.Linq;

namespace NumericalGeometryLib.Collections
{
    public static class CollectionsUtils
    {
        public static int[] GetRandomPermutation(this int maxIndex)
        {
            //Create an initial identity permutation
            var result = Enumerable.Range(0, maxIndex).ToArray();

            //Create the random permutation
            var randGen = new System.Random();

            for (var i = 0; i < maxIndex; i++)
            {
                //Select a random number between i and maxIndex
                var index = randGen.Next(i, maxIndex + 1);

                if (i == index) continue;

                //Swap the values at indices i and index
                (result[i], result[index]) = (result[index], result[i]);
            }

            return result;
        }

        public static int[] GetRandomPermutation(this int maxIndex, int seed)
        {
            //Create an initial identity permutation
            var result = Enumerable.Range(0, maxIndex).ToArray();

            //Create the random permutation
            var randGen = new System.Random(seed);

            for (var i = 0; i < maxIndex; i++)
            {
                //Select a random number between i and maxIndex
                var index = randGen.Next(i, maxIndex + 1);

                if (i == index) continue;

                //Swap the values at indices i and index
                (result[i], result[index]) = (result[index], result[i]);
            }

            return result;
        }

        /// <summary>
        /// Select a number of items by their indices from a collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="indexList"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static IEnumerable<T> SelectItemsFrom<T>(this IEnumerable<int> indexList, GenerativeCollection<T> c)
        {
            return c.GetItems(indexList);
        }
    }
}
