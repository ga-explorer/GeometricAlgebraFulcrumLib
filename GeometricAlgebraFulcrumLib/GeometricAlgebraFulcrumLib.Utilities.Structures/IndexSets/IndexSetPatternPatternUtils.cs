using System.Numerics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets
{
    internal static class IndexSetPatternPatternUtils
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSubset(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexPattern2 = indexSet2.ToUInt64();

            return (indexPattern1 & indexPattern2) == indexPattern1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSuperset(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexPattern2 = indexSet2.ToUInt64();

            return (indexPattern1 & indexPattern2) == indexPattern2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexPattern2 = indexSet2.ToUInt64();

            return indexPattern1 != 0UL &&
                   indexPattern2 != 0UL &&
                   (indexPattern1 & indexPattern2) == indexPattern2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Overlaps(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexPattern2 = indexSet2.ToUInt64();

            return (indexPattern1 & indexPattern2) != 0;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet Intersect(IndexSet indexSet1, int index2)
        {
            var indexPattern1 = indexSet1.ToUInt64();

            return indexPattern1 == 0UL
                ? IndexSet.EmptySet
                : IndexSet.CreateFromUInt64Pattern(indexPattern1 & (1UL << index2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet Intersect(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexPattern2 = indexSet2.ToUInt64();

            return indexPattern1 == 0UL || indexPattern2 == 0UL
                ? IndexSet.EmptySet
                : IndexSet.CreateFromUInt64Pattern(indexPattern1 & indexPattern2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet Join(IndexSet indexSet1, int index2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexPattern2 = 1UL << index2;

            return indexPattern1 == 0UL
                ? IndexSet.CreateFromUInt64Pattern(indexPattern2)
                : IndexSet.CreateFromUInt64Pattern(indexPattern1 | indexPattern2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet Join(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexPattern2 = indexSet2.ToUInt64();

            if (indexPattern1 == 0UL)
                return indexSet2;

            if (indexPattern2 == 0UL)
                return indexSet1;

            return IndexSet.CreateFromUInt64Pattern(indexPattern1 | indexPattern2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet Merge(IndexSet indexSet1, int index2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexPattern2 = 1UL << index2;

            return indexPattern1 == 0UL
                ? IndexSet.CreateFromUInt64Pattern(indexPattern2)
                : IndexSet.CreateFromUInt64Pattern(indexPattern1 ^ indexPattern2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet Merge(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexPattern2 = indexSet2.ToUInt64();

            if (indexPattern1 == 0UL)
                return indexSet2;

            if (indexPattern2 == 0UL)
                return indexSet1;

            return IndexSet.CreateFromUInt64Pattern(indexPattern1 ^ indexPattern2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexSet Difference(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexPattern2 = indexSet2.ToUInt64();

            if (indexPattern1 == 0UL)
                return IndexSet.EmptySet;

            if (indexPattern2 == 0UL)
                return indexSet1;

            return IndexSet.CreateFromUInt64Pattern(indexPattern1 & ~indexPattern2);
        }


        public static int CountSwapsWithSelf(IndexSet indexSet1)
        {
            var indexPattern1 = indexSet1.ToUInt64();

            if (indexPattern1 == 0UL)
                return 0;

            var swapCount = 0;
            var count1 = BitOperations.PopCount(indexPattern1);
            var index1Order = 0;

            while (indexPattern1 != 0UL)
            {
                index1Order++;
                indexPattern1 &= indexPattern1 - 1;

                swapCount += count1 - index1Order;
            }

            return swapCount;
        }

        public static int CountSwaps(IndexSet indexSet1, int index2)
        {
            var indexPattern1 = indexSet1.ToUInt64();

            if (indexPattern1 == 0UL) return 0;

            var swapCount = 0;

            var count1 = BitOperations.PopCount(indexPattern1);

            var indexOrder1 = 0;

            var index1 = BitOperations.TrailingZeroCount(indexPattern1);

            while (indexPattern1 != 0UL)
            {
                if (index1 < index2) // Take value from first array, no swaps needed
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;

                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                }
                else if (index1 > index2) // Take value from second array, do swaps
                {
                    swapCount += count1 - indexOrder1;

                    break;
                }
                else // Found common integer, do swaps
                {
                    indexOrder1++;

                    swapCount += count1 - indexOrder1;
                    break;
                }
            }

            return swapCount;
        }

        public static int CountSwaps(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexPattern2 = indexSet2.ToUInt64();

            if (indexPattern1 == 0UL || indexPattern2 == 0UL) return 0;

            var swapCount = 0;

            var count1 = BitOperations.PopCount(indexPattern1);
            var count2 = BitOperations.PopCount(indexPattern2);

            var indexOrder1 = 0;
            var indexOrder2 = 0;

            var index1 = BitOperations.TrailingZeroCount(indexPattern1);
            var index2 = BitOperations.TrailingZeroCount(indexPattern2);

            while (indexPattern1 != 0UL && indexPattern2 != 0UL)
            {
                if (index1 < index2) // Take value from first array, no swaps needed
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;

                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                }
                else if (index1 > index2) // Take value from second array, do swaps
                {
                    indexOrder2++;
                    indexPattern2 &= indexPattern2 - 1;

                    swapCount += count1 - indexOrder1;

                    if (indexOrder2 < count2) index2 = BitOperations.TrailingZeroCount(indexPattern2);
                }
                else // Found common integer, do swaps
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;

                    indexOrder2++;
                    indexPattern2 &= indexPattern2 - 1;

                    swapCount += count1 - indexOrder1;

                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                    if (indexOrder2 < count2) index2 = BitOperations.TrailingZeroCount(indexPattern2);
                }
            }

            return swapCount;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int swapCount, IndexSet mergedIndexSet) MergeCountSwaps(IndexSet indexSet1, int index2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexPattern2 = 1UL << index2;

            if (indexPattern1 == 0UL)
                return (
                    0,
                    IndexSet.CreateFromUInt64Pattern(indexPattern2)
                );

            //if (indexPattern2 == 0UL)
            //    return (0, indexSet1, IndexSet.EmptySet);

            var swapCount = CountSwaps(indexSet1, index2);

            var mergedIndexSet =
                IndexSet.CreateFromUInt64Pattern(
                    indexPattern1 ^ indexPattern2
                );

            return (swapCount, mergedIndexSet);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int swapCount, IndexSet mergedIndexSet) MergeCountSwaps(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexPattern2 = indexSet2.ToUInt64();

            if (indexPattern1 == 0UL)
                return (0, indexSet2);

            if (indexPattern2 == 0UL)
                return (0, indexSet1);

            var swapCount = CountSwaps(indexSet1, indexSet2);

            var mergedIndexSet =
                IndexSet.CreateFromUInt64Pattern(
                    indexPattern1 ^ indexPattern2
                );

            return (swapCount, mergedIndexSet);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int swapCount, IndexSet mergedIndexSet, IndexSet commonIndexSet) MergeCountSwapsTrackCommon(IndexSet indexSet1, int index2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexPattern2 = 1UL << index2;

            if (indexPattern1 == 0UL)
                return (
                    0,
                    IndexSet.CreateFromUInt64Pattern(indexPattern2),
                    IndexSet.EmptySet
                );

            //if (indexPattern2 == 0UL)
            //    return (0, indexSet1, IndexSet.EmptySet);

            var swapCount = CountSwaps(indexSet1, index2);

            var mergedIndexSet =
                IndexSet.CreateFromUInt64Pattern(
                    indexPattern1 ^ indexPattern2
                );

            var commonIndexSet =
                (indexPattern1 & indexPattern2) != 0UL
                    ? IndexSet.CreateFromUInt64Pattern(indexPattern2)
                    : IndexSet.EmptySet;

            return (swapCount, mergedIndexSet, commonIndexSet);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int swapCount, IndexSet mergedIndexSet, IndexSet commonIndexSet) MergeCountSwapsTrackCommon(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexPattern2 = indexSet2.ToUInt64();

            if (indexPattern1 == 0UL)
                return (0, indexSet2, IndexSet.EmptySet);

            if (indexPattern2 == 0UL)
                return (0, indexSet1, IndexSet.EmptySet);

            var swapCount = CountSwaps(indexSet1, indexSet2);

            var mergedIndexSet =
                IndexSet.CreateFromUInt64Pattern(
                    indexPattern1 ^ indexPattern2
                );

            var commonIndexSet =
                IndexSet.CreateFromUInt64Pattern(
                    indexPattern1 & indexPattern2
                );

            return (swapCount, mergedIndexSet, commonIndexSet);
        }

    }
}