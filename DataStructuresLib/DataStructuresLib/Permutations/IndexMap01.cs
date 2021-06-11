using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Permutations
{
    public sealed class IndexMap01
        : IIndexMap1D2D
    {
        public int IndexCount1 { get; set; }

        public int IndexCount2 { get; set; }

        public int IndexCount 
            => IndexCount1 * IndexCount2;

        public Pair<int> this[int index]
        {
            get
            {
                var index1 = index % IndexCount1;
                var index2 = (index - index1) / IndexCount1;

                return new Pair<int>(index1, index2);
            }
        }

        public IEnumerable<Pair<int>> this[IEnumerable<int> indexList] 
            => indexList.Select(i => this[i]);

        public int this[Pair<int> indexPair]
            => indexPair.Item1 + indexPair.Item2 * IndexCount1;

        public int this[int index1, int index2]
            => index1 + index2 * IndexCount1;

        public IEnumerable<int> this[IEnumerable<Pair<int>> inputsList]
            => inputsList.Select(indexPair => 
                indexPair.Item1 + indexPair.Item2 * IndexCount1
            );


        public IndexMap01(int count1, int count2)
        {
            IndexCount1 = count1;
            IndexCount2 = count2;
        }
    }
}
