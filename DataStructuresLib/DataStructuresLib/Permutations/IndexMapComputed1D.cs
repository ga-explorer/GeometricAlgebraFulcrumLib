using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.Permutations
{
    public abstract class IndexMapComputed1D
        : IIndexMap1D
    {
        public int IndexCount { get; set; }

        public int this[int index]
            => ComputeIndex(index);

        public IEnumerable<int> this[IEnumerable<int> indexList]
            => indexList.Select(ComputeIndex);


        public IndexMapComputed1D()
        {
            IndexCount = 0;
        }

        public IndexMapComputed1D(int indexCount)
        {
            IndexCount = indexCount;
        }


        protected abstract int ComputeIndex(int index);
    }
}