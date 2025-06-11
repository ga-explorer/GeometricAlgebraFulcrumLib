using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets
{
    public sealed class IndexSetEqualityComparer : 
        IEqualityComparer<IndexSet>
    {
        public static IndexSetEqualityComparer Instance { get; }
            = new IndexSetEqualityComparer();


        private IndexSetEqualityComparer()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(IndexSet indexSet1, IndexSet indexSet2)
        {
            return indexSet1.Equals(indexSet2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetHashCode(IndexSet indexSet)
        {
            return indexSet.GetHashCode();
        }
    }
}