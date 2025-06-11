using System;
using System.Diagnostics;

namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples
{
    public readonly struct IndexPair :
        IPair<int>,
        IEquatable<IPair<int>>,
        IEquatable<IndexPair>,
        IComparable<IPair<int>>,
        IComparable<IndexPair>
    {
        public int Index1 { get; }

        public int Index2 { get; }

        public int Item1
            => Index1;

        public int Item2
            => Index2;

        public bool IsSymmetric 
            => Index1 == Index2;


        
        public IndexPair(int index1)
        {
            Debug.Assert(index1 >= 0);

            Index1 = index1;
            Index2 = index1;
        }

        
        public IndexPair(int index1, int index2)
        {
            Debug.Assert(index1 >= 0 && index2 >= 0);

            Index1 = index1;
            Index2 = index2;
        }

        
        public IndexPair(IPair<int> pair)
        {
            Debug.Assert(pair is { Item1: >= 0, Item2: >= 0 });

            Index1 = pair.Item1;
            Index2 = pair.Item2;
        }


        
        public IndexPair Transpose()
        {
            return new IndexPair(Index2, Index1);
        }


        
        public void Deconstruct(out int index1, out int index2)
        {
            index1 = Index1;
            index2 = Index2;
        }

        
        public bool Equals(IndexPair other)
        {
            return Index1 == other.Index1 && Index2 == other.Index2;
        }

        
        public bool Equals(IPair<int>? other)
        {
            if (ReferenceEquals(other, null)) return false;

            return Index1 == other.Item1 && Index2 == other.Item2;
        }

        
        public override bool Equals(object? obj)
        {
            return obj is IndexPair other && Equals(other) ||
                   obj is IPair<int> other1 && Equals(other1);
        }

        
        public override int GetHashCode()
        {
            return Index1 ^ Index2;
        }

        
        public int CompareTo(IPair<int>? other)
        {
            Debug.Assert(other != null, nameof(other) + " != null");

            var item1Comparison = Index1.CompareTo(other.Item1);
            if (item1Comparison != 0) return item1Comparison;
            return Index2.CompareTo(other.Item2);
        }

        
        public int CompareTo(IndexPair other)
        {
            var item1Comparison = Index1.CompareTo(other.Item1);
            if (item1Comparison != 0) return item1Comparison;
            return Index2.CompareTo(other.Item2);
        }

        
        public override string ToString()
        {
            return $"<{Index1}, {Index2}>";
        }
    }
}