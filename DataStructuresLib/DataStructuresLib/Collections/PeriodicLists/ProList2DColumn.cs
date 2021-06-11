using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Collections.PeriodicLists2D;

namespace DataStructuresLib.Collections.PeriodicLists
{
    public class ProList2DColumn<TValue> :
        IPeriodicReadOnlyList<TValue>
    {
        public IPeriodicReadOnlyList2D<TValue> SourceLattice { get; }

        public int ColumnIndex { get; }

        public int Count 
            => SourceLattice.Count1;

        public TValue this[int index] 
            => SourceLattice[index, ColumnIndex];


        public ProList2DColumn(IPeriodicReadOnlyList2D<TValue> sourceGrid, int columnIndex)
        {
            SourceLattice = sourceGrid;
            ColumnIndex = columnIndex;
        }
        

        public IEnumerator<TValue> GetEnumerator()
        {
            return Enumerable
                .Range(0, SourceLattice.Count1)
                .Select(i => SourceLattice[i, ColumnIndex])
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}