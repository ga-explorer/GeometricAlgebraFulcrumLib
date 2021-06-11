using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.Permutations
{
    public static class PermutationsUtils
    {
        public static IEnumerable<IList<int>> GetIndexPermutations(int count)
        {
            var indicesArray = Enumerable.Range(0, count).ToArray();

            var list = new List<IList<int>>();

            return GetIndexPermutations(indicesArray, 0, indicesArray.Length - 1, list);
        }

        private static IEnumerable<IList<int>> GetIndexPermutations(int[] indicesArray, int start, int end, IList<IList<int>> list)
        {
            if (start == end)
            {
                // We have one of our possible n! solutions,
                // add it to the list.
                list.Add(new List<int>(indicesArray));
            }
            else
            {
                for (var i = start; i <= end; i++)
                {
                    Swap(ref indicesArray[start], ref indicesArray[i]);

                    GetIndexPermutations(indicesArray, start + 1, end, list);

                    Swap(ref indicesArray[start], ref indicesArray[i]);
                }
            }

            return list;
        }

        private static void Swap(ref int a, ref int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }
    }
}