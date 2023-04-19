using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using Open.Collections;

namespace DataStructuresLib.IndexSets
{
    public readonly struct SparseIndexSet :
        IIndexSet,
        IEquatable<SparseIndexSet>,
        IComparable<SparseIndexSet>
    {
        internal static SparseIndexSet EmptySet { get; }
            = new SparseIndexSet(ImmutableSortedSet<int>.Empty);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static SparseIndexSet Create(int index)
        {
            return new SparseIndexSet(
                ImmutableSortedSet.Create(index)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static SparseIndexSet Create(int index1, int index2)
        {
            return new SparseIndexSet(
                ImmutableSortedSet.Create(index1, index2)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static SparseIndexSet Create(params int[] indexArray)
        {
            return new SparseIndexSet(
                ImmutableSortedSet.Create(indexArray)
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static SparseIndexSet Create(IEnumerable<int> indexList)
        {
            return new SparseIndexSet(
                indexList.ToImmutableSortedSet()
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static SparseIndexSet Create(ImmutableSortedSet<int> indexSet)
        {
            return indexSet.Count == 0 
                ? EmptySet 
                : new SparseIndexSet(indexSet);
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(SparseIndexSet operand1, IIndexSet operand2)
        {
            return operand1.Equals(operand2);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(SparseIndexSet operand1, IIndexSet operand2)
        {
            return !operand1.Equals(operand2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(SparseIndexSet operand1, IIndexSet operand2)
        {
            return operand1.CompareTo(operand2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(SparseIndexSet operand1, IIndexSet operand2)
        {
            return operand1.CompareTo(operand2) < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(SparseIndexSet operand1, IIndexSet operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(SparseIndexSet operand1, IIndexSet operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }


        public ImmutableSortedSet<int> SortedIndexSet { get; }

        public bool IsEmptySet
            => SortedIndexSet.Count == 0;

        public bool IsSingleIndexSet 
            => SortedIndexSet.Count == 1;
    
        public bool IsIndexPairSet 
            => SortedIndexSet.Count == 2;

        public bool IsSparseSet 
            => LastIndex - FirstIndex + 1 > Count;
        
        public bool IsDenseSet 
            => LastIndex - FirstIndex + 1 == Count;

        public bool IsUInt64Set 
            => LastIndex < 64;

        public int FirstIndex
            => SortedIndexSet[0];

        public int LastIndex
            => SortedIndexSet[^1];

        public int Count
            => SortedIndexSet.Count;

        public int this[int index]
            => SortedIndexSet[index];


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private SparseIndexSet(ImmutableSortedSet<int> sortedIndexSet)
        {
            Debug.Assert(
                sortedIndexSet.Count == 0 ||
                sortedIndexSet[0] >= 0
            );

            SortedIndexSet = sortedIndexSet;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return obj is IIndexSet other && Equals(other);
        }

        public bool Equals(SparseIndexSet other)
        {
            if (SortedIndexSet.Count != other.SortedIndexSet.Count)
                return false;

            using var enumerator1 = SortedIndexSet.GetEnumerator();
            using var enumerator2 = other.SortedIndexSet.GetEnumerator();

            while (enumerator1.MoveNext())
            {
                if (!enumerator2.MoveNext() || enumerator1.Current != enumerator2.Current)
                    return false;
            }

            return !enumerator2.MoveNext();
        }

        public bool Equals(IIndexSet other)
        {
            if (ReferenceEquals(null, other)) return false;

            if (Count != other.Count) return false;

            using var enumerator1 = SortedIndexSet.GetEnumerator();
            using var enumerator2 = other.GetEnumerator();

            while (enumerator1.MoveNext())
            {
                if (!enumerator2.MoveNext() || enumerator1.Current != enumerator2.Current)
                    return false;
            }

            return !enumerator2.MoveNext();
        }

        public override int GetHashCode()
        {
            if (SortedIndexSet.Count == 0)
                return 0;

            var index1 = SortedIndexSet[0];

            if (SortedIndexSet.Count == 1)
                return index1;

            return SortedIndexSet
                .Skip(1)
                .Aggregate(
                    index1,
                    (hashCode, index) => hashCode ^ index
                );
        }
    
        public int CompareTo(SparseIndexSet other)
        {
            var n1 = Count;
            var n2 = other.Count;

            //TODO: Use iterator instead of this[], just as in Equals() implementation here
            while (n1 > 0 && n2 > 0)
            {
                n1--;
                n2--;

                var index1 = this[n1];
                var index2 = other[n2];

                if (index1 > index2) return 1;
                if (index1 < index2) return -1;
            }

            if (n1 == 0) return n2 == 0 ? 0 : -1;

            return 1;
        }

        public int CompareTo(IIndexSet other)
        {
            var n1 = Count;
            var n2 = other.Count;

            //TODO: Use iterator instead of this[], just as in Equals() implementation here
            while (n1 > 0 && n2 > 0)
            {
                n1--;
                n2--;

                var index1 = this[n1];
                var index2 = other[n2];

                if (index1 > index2) return 1;
                if (index1 < index2) return -1;
            }

            if (n1 == 0) return n2 == 0 ? 0 : -1;

            return 1;
        }
    
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<int> GetIndexRange()
        {
            return new Pair<int>(
                SortedIndexSet[0],
                SortedIndexSet[^1]
            );
        }
    
        public IEnumerable<int> GetReversedIndices()
        {
            for (var i = SortedIndexSet.Count - 1; i >= 0; i--)
                yield return SortedIndexSet[i];
        }
    
    
        public bool TryGetUInt64BitPattern(out ulong bitPattern)
        {
            if (SortedIndexSet.Count == 0)
            {
                bitPattern = 0UL;
                return true;
            }

            if (SortedIndexSet[^1] >= 64)
            {
                bitPattern = 0UL;
                return false;
            }

            bitPattern = SortedIndexSet.Count == 1 
                ? 1UL << SortedIndexSet[0]
                : SortedIndexSet.Aggregate(
                    0UL,
                    (id, index) => id | (1UL << index)
                );

            return true;
        }

        public DenseIndexSet ToDenseIndexSet()
        {
            if (IsEmptySet)
                throw new InvalidOperationException();

            var firstIndex = FirstIndex;
            var lastIndex = LastIndex;

            var count = lastIndex - firstIndex + 1;

            return count == Count
                ? DenseIndexSet.Create(firstIndex, count)
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt64IndexSet ToUInt64IndexSet()
        {
            return SortedIndexSet.ToUInt64IndexSet();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseIndexSet ToSparseIndexSet()
        {
            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(int index)
        {
            return SortedIndexSet.Contains(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsSingleIndex(int index)
        {
            return SortedIndexSet.Count == 1 && 
                   SortedIndexSet[0] == index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(ulong indexSet)
        {
            if (IsEmptySet) 
                return false;

            return indexSet == 0 || indexSet.PatternToPositions().All(Contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(IIndexSet indexSet)
        {
            if (IsEmptySet) 
                return false;

            return indexSet.IsEmptySet || indexSet.All(Contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(ulong indexSet)
        {
            if (IsEmptySet || indexSet == 0) 
                return false;

            return indexSet.PatternToPositions().Any(Contains);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(IIndexSet indexSet)
        {
            if (IsEmptySet || indexSet.IsEmptySet) 
                return false;

            return indexSet.Any(Contains);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IIndexSet MapIndices(Func<int, int> indexMapping)
        {
            if (IsEmptySet) return this;
            
            return SortedIndexSet
                .Select(indexMapping)
                .ToImmutableSortedSet()
                .ToIndexSet();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IIndexSet ShiftIndices(int offset)
        {
            if (IsEmptySet || offset == 0) return this;

            if (offset > 0)
                return SortedIndexSet
                    .Select(i => i + offset)
                    .ToImmutableSortedSet()
                    .ToIndexSet();

            offset = -offset;

            return FirstIndex >= offset
                ? SortedIndexSet
                    .Select(i => i - offset)
                    .ToImmutableSortedSet()
                    .ToIndexSet()
                : throw new InvalidOperationException();
        }


        public IIndexSet Add(int index)
        {
            if (IsEmptySet)
            {
                return index < 64 
                    ? UInt64IndexSet.SimpleIndexSets[index] 
                    : Create(index);
            }

            if (index < 64 && SortedIndexSet[^1] < 64)
            {
                var bitPattern = SortedIndexSet.Aggregate(
                    1UL << index,
                    (b, i) => b | (1UL << i)
                );

                return UInt64IndexSet.Create(bitPattern);
            }

            if (SortedIndexSet.Contains(index))
                return this;

            return new SparseIndexSet(
                SortedIndexSet.Add(index)
            );
        }

        public IIndexSet Remove(int index)
        {
            if (!SortedIndexSet.Contains(index))
                throw new InvalidOperationException();

            var sortedIndexSet = SortedIndexSet.Remove(index);

            if (sortedIndexSet.IsEmpty)
                return EmptyIndexSet.Instance;

            if (sortedIndexSet[^1] >= 64) 
                return new SparseIndexSet(sortedIndexSet);

            if (sortedIndexSet.Count == 1)
                return UInt64IndexSet.SimpleIndexSets[sortedIndexSet[0]];

            var bitPattern = sortedIndexSet.Aggregate(
                0UL,
                (b, i) => b | (1UL << i)
            );

            return UInt64IndexSet.Create(bitPattern);
        }
    
        public IIndexSet RemoveIfContains(int index)
        {
            if (!SortedIndexSet.Contains(index))
                return this;

            var sortedIndexSet = SortedIndexSet.Remove(index);

            if (sortedIndexSet.IsEmpty)
                return EmptyIndexSet.Instance;

            if (sortedIndexSet[^1] >= 64) 
                return new SparseIndexSet(sortedIndexSet);

            if (sortedIndexSet.Count == 1)
                return UInt64IndexSet.SimpleIndexSets[sortedIndexSet[0]];

            var bitPattern = sortedIndexSet.Aggregate(
                0UL,
                (b, i) => b | (1UL << i)
            );

            return UInt64IndexSet.Create(bitPattern);
        }
        
        public IIndexSet Intersect(ulong indexSet)
        {
            if (IsEmptySet || indexSet == 0)
                return EmptyIndexSet.Instance;

            var bitPattern = 0UL;
            foreach (var index in SortedIndexSet)
            {
                if (index >= 64) 
                    continue;

                var mask = 1UL << index;

                if ((indexSet & mask) == 0)
                    continue;

                bitPattern |= mask;
            }

            return bitPattern.BitPatternToUInt64IndexSet();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IIndexSet Intersect(IIndexSet indexSet)
        {
            if (indexSet.TryGetUInt64BitPattern(out var bitPattern))
                return Intersect(bitPattern);

            if (IsEmptySet || indexSet.IsEmptySet)
                return EmptyIndexSet.Instance;

            return SortedIndexSet.Intersect(indexSet).ToIndexSet();
        }

        public IIndexSet Join(ulong indexSet)
        {
            if (TryGetUInt64BitPattern(out var b1))
                return (b1 | indexSet).BitPatternToUInt64IndexSet();
            
            if (indexSet == 0)
                return this;

            var sortedSetBuilder = SortedIndexSet.ToBuilder();

            sortedSetBuilder.AddRange(indexSet.PatternToPositions());

            return sortedSetBuilder.ToIndexSet();
        }

        public IIndexSet Join(IIndexSet indexSet)
        {
            if (TryGetUInt64BitPattern(out var b1) && indexSet.TryGetUInt64BitPattern(out var b2))
                return (b1 | b2).BitPatternToUInt64IndexSet();

            if (IsEmptySet)
                return indexSet;

            if (indexSet.IsEmptySet)
                return this;

            var sortedSetBuilder = SortedIndexSet.ToBuilder();

            sortedSetBuilder.AddRange(indexSet);

            return sortedSetBuilder.ToIndexSet();
        }

        public IIndexSet Difference(ulong indexSet)
        {
            if (IsEmptySet)
                return EmptyIndexSet.Instance;

            if (indexSet == 0)
                return this;

            var sortedIndexSet = 
                SortedIndexSet.Except(indexSet.PatternToPositions());

            if (sortedIndexSet.Count == SortedIndexSet.Count)
                return this;

            return new SparseIndexSet(sortedIndexSet);
        }

        public IIndexSet Difference(IIndexSet indexSet)
        {
            if (IsEmptySet)
                return EmptyIndexSet.Instance;

            if (indexSet.IsEmptySet)
                return this;

            var sortedIndexSet = 
                SortedIndexSet.Except(indexSet);

            if (sortedIndexSet.Count == SortedIndexSet.Count)
                return this;

            return new SparseIndexSet(sortedIndexSet);
        }

        public IIndexSet SymmetricDifference(ulong indexSet)
        {
            if (IsEmptySet)
                return indexSet.BitPatternToUInt64IndexSet();

            if (indexSet == 0)
                return this;

            var sortedSetBuilder = SortedIndexSet.ToBuilder();

            foreach (var index in indexSet.PatternToPositions())
            {
                if (sortedSetBuilder.Contains(index))
                    sortedSetBuilder.Remove(index);
                else
                    sortedSetBuilder.Add(index);
            }

            return new SparseIndexSet(
                sortedSetBuilder.ToImmutable()
            );
        }

        public IIndexSet SymmetricDifference(IIndexSet indexSet)
        {
            if (IsEmptySet)
                return indexSet;

            if (indexSet.IsEmptySet)
                return this;

            var sortedSetBuilder = SortedIndexSet.ToBuilder();

            foreach (var index in indexSet)
            {
                if (sortedSetBuilder.Contains(index))
                    sortedSetBuilder.Remove(index);
                else
                    sortedSetBuilder.Add(index);
            }

            return new SparseIndexSet(
                sortedSetBuilder.ToImmutable()
            );
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<int> GetEnumerator()
        {
            return SortedIndexSet.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return SortedIndexSet
                .Select(i => i.ToString())
                .ConcatenateText(", ", "<", ">");
        }
    }
}