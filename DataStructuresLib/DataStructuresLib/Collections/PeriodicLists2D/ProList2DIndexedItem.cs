using System;
using System.Diagnostics.CodeAnalysis;

namespace DataStructuresLib.Collections.PeriodicLists2D
{
    public sealed class ProList2DIndexedItem<TValue>
    {
        public IPeriodicReadOnlyList2D<TValue> SourceList { get; }

        public int Index1 
            => IndexTuple.Item1;

        public int Index2 
            => IndexTuple.Item2;

        public int Index 
            => IndexTuple.Item2 * SourceList.Count1 + IndexTuple.Item1;

        public Tuple<int, int> IndexTuple { get; }

        public TValue Item 
            => SourceList[IndexTuple.Item1, IndexTuple.Item2];


        public ProList2DIndexedItem([NotNull] IPeriodicReadOnlyList2D<TValue> sourceList, int index1, int index2)
        {
            SourceList = sourceList;
            IndexTuple = new Tuple<int, int>(index1, index2);
        }
    }
}