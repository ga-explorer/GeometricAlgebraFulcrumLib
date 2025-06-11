using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets
{
    public sealed class IndexSetEqualityComparer : 
        IEqualityComparer<IndexSet>
    {
        public static IndexSetEqualityComparer Instance { get; }
            = new IndexSetEqualityComparer();


        private IndexSetEqualityComparer()
        {
        }


        
        public bool Equals(IndexSet indexSet1, IndexSet indexSet2)
        {
            return indexSet1.Equals(indexSet2);
        }

        
        public int GetHashCode(IndexSet indexSet)
        {
            return indexSet.GetHashCode();
        }
    }
}