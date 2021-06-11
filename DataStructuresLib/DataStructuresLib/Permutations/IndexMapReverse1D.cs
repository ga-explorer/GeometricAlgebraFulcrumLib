using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.Permutations
{
    public sealed class IndexMapReverse1D
        : IIndexMap1D
    {
        public int IndexCount { get; set; }

        public int this[int index] 
            => IndexCount - index - 1;

        public IEnumerable<int> this[IEnumerable<int> indexList] 
            => indexList.Select(i => IndexCount - i - 1);


        public IndexMapReverse1D(int indexCount)
        {
            IndexCount = indexCount;
        }
    }
}