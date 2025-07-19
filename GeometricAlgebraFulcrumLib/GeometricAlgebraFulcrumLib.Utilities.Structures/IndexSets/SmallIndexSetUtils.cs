using System.Diagnostics;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

internal static class SmallIndexSetUtils
{

    public static bool IsSuperset(SmallIndexSet indexSet1, SmallIndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexArray2 = indexSet2.GetInternalIndexArray();

        if (indexArray2.Length == 0)
            return true;

        if (indexArray1.Length == 0)
            return false;

        var count1 = indexArray1.Length;
        var count2 = indexArray2.Length;

        var index1Order = 0;
        var index2Order = 0;

        var index1 = indexArray1[0];
        var index2 = indexArray2[0];

        while (index1Order < count1 && index2Order < count2)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                index1Order++;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                return false;
            }
            else // Found common integer, do swaps
            {
                index1Order++;
                index2Order++;

                if (index1Order < count1) index1 = indexArray1[index1Order];
                if (index2Order < count2) index2 = indexArray2[index2Order];
            }
        }

        return index2Order >= count2;
    }

    public static bool Contains(SmallIndexSet indexSet1, SmallIndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexArray2 = indexSet2.GetInternalIndexArray();

        if (indexArray1.Length == 0 || indexArray2.Length == 0)
            return false;

        var count1 = indexArray1.Length;
        var count2 = indexArray2.Length;

        var index1Order = 0;
        var index2Order = 0;

        var index1 = indexArray1[0];
        var index2 = indexArray2[0];

        while (index1Order < count1 && index2Order < count2)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                index1Order++;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                return false;
            }
            else // Found common integer, do swaps
            {
                index1Order++;
                index2Order++;

                if (index1Order < count1) index1 = indexArray1[index1Order];
                if (index2Order < count2) index2 = indexArray2[index2Order];
            }
        }

        return index2Order >= count2;
    }

    public static bool Overlaps(SmallIndexSet indexSet1, SmallIndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexArray2 = indexSet2.GetInternalIndexArray();

        if (indexArray1.Length == 0 || indexArray2.Length == 0)
            return false;

        var count1 = indexArray1.Length;
        var count2 = indexArray2.Length;

        var index1Order = 0;
        var index2Order = 0;

        var index1 = indexArray1[0];
        var index2 = indexArray2[0];

        while (index1Order < count1 && index2Order < count2)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                index1Order++;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                index2Order++;

                if (index2Order < count2) index2 = indexArray2[index2Order];
            }
            else // Found common integer, do swaps
            {
                return true;
            }
        }

        return false;
    }


    public static SmallIndexSet Intersect(SmallIndexSet indexSet1, int index2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();

        if (indexArray1.Length == 0)
            return SmallIndexSet.EmptySet;

        var count1 = indexArray1.Length;

        var index1Order = 0;

        var index1 = indexArray1[0];

        var commonBufferIndex = 0;
        Span<int> commonBuffer = stackalloc int[1];

        while (index1Order < count1)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                index1Order++;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                break;
            }
            else // Found common integer, do swaps
            {
                commonBuffer[commonBufferIndex++] = index1;

                break;
            }
        }

        return SmallIndexSet.Create(commonBuffer[..commonBufferIndex]);
    }

    public static SmallIndexSet Intersect(SmallIndexSet indexSet1, SmallIndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexArray2 = indexSet2.GetInternalIndexArray();

        if (indexArray1.Length == 0 || indexArray2.Length == 0)
            return SmallIndexSet.EmptySet;

        var count1 = indexArray1.Length;
        var count2 = indexArray2.Length;

        var index1Order = 0;
        var index2Order = 0;

        var index1 = indexArray1[0];
        var index2 = indexArray2[0];

        var commonBufferIndex = 0;
        Span<int> commonBuffer = stackalloc int[Math.Min(count1, count2)];

        while (index1Order < count1 && index2Order < count2)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                index1Order++;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                index2Order++;

                if (index2Order < count2) index2 = indexArray2[index2Order];
            }
            else // Found common integer, do swaps
            {
                index1Order++;
                index2Order++;

                commonBuffer[commonBufferIndex++] = index1;

                if (index1Order < count1) index1 = indexArray1[index1Order];
                if (index2Order < count2) index2 = indexArray2[index2Order];
            }
        }

        return SmallIndexSet.Create(commonBuffer[..commonBufferIndex]);
    }


    public static SmallIndexSet Join(SmallIndexSet indexSet1, int index2)
    {
        Debug.Assert(index2 >= 0);

        var indexArray1 = indexSet1.GetInternalIndexArray();

        if (indexArray1.Length == 0)
            return SmallIndexSet.Create(index2);

        var count1 = indexArray1.Length;

        var index1Order = 0;
        var index2Order = 0;

        var index1 = indexArray1[0];

        var mergedBufferIndex = 0;
        Span<int> mergedBuffer = stackalloc int[count1 + 1];

        while (index1Order < count1 && index2Order < 1)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                index1Order++;

                mergedBuffer[mergedBufferIndex++] = index1;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                index2Order++;

                mergedBuffer[mergedBufferIndex++] = index2;
            }
            else // Found common integer, do swaps
            {
                index1Order++;
                index2Order++;

                mergedBuffer[mergedBufferIndex++] = index1;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
        }

        // Copy remaining from arr1
        while (index1Order < count1)
            mergedBuffer[mergedBufferIndex++] = indexArray1[index1Order++];

        // Copy remaining from arr2
        if (index2Order < 1)
            mergedBuffer[mergedBufferIndex++] = index2;

        return SmallIndexSet.Create(mergedBuffer[..mergedBufferIndex]);
    }

    public static SmallIndexSet Join(SmallIndexSet indexSet1, SmallIndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexArray2 = indexSet2.GetInternalIndexArray();

        if (indexArray1.Length == 0)
            return indexSet2;

        if (indexArray2.Length == 0)
            return indexSet1;

        var count1 = indexArray1.Length;
        var count2 = indexArray2.Length;

        var index1Order = 0;
        var index2Order = 0;

        var index1 = indexArray1[0];
        var index2 = indexArray2[0];

        var mergedBufferIndex = 0;
        Span<int> mergedBuffer = stackalloc int[count1 + count2];

        while (index1Order < count1 && index2Order < count2)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                index1Order++;

                mergedBuffer[mergedBufferIndex++] = index1;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                index2Order++;

                mergedBuffer[mergedBufferIndex++] = index2;

                if (index2Order < count2) index2 = indexArray2[index2Order];
            }
            else // Found common integer, do swaps
            {
                index1Order++;
                index2Order++;

                mergedBuffer[mergedBufferIndex++] = index1;

                if (index1Order < count1) index1 = indexArray1[index1Order];
                if (index2Order < count2) index2 = indexArray2[index2Order];
            }
        }

        // Copy remaining from arr1
        while (index1Order < count1)
            mergedBuffer[mergedBufferIndex++] = indexArray1[index1Order++];

        // Copy remaining from arr2
        while (index2Order < count2)
            mergedBuffer[mergedBufferIndex++] = indexArray2[index2Order++];

        return SmallIndexSet.Create(mergedBuffer[..mergedBufferIndex]);
    }


    public static SmallIndexSet Merge(SmallIndexSet indexSet1, int index2)
    {
        Debug.Assert(index2 >= 0);

        var indexArray1 = indexSet1.GetInternalIndexArray();

        if (indexArray1.Length == 0)
            return SmallIndexSet.Create(index2);

        var count1 = indexArray1.Length;

        var index1Order = 0;
        var index2Order = 0;

        var index1 = indexArray1[0];

        var mergedBufferIndex = 0;
        Span<int> mergedBuffer = stackalloc int[count1 + 1];

        while (index1Order < count1 && index2Order < 1)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                index1Order++;

                mergedBuffer[mergedBufferIndex++] = index1;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                index2Order++;

                mergedBuffer[mergedBufferIndex++] = index2;
            }
            else // Found common integer, do swaps
            {
                index1Order++;
                index2Order++;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
        }

        // Copy remaining from arr1
        while (index1Order < count1)
            mergedBuffer[mergedBufferIndex++] = indexArray1[index1Order++];

        // Copy remaining from arr2
        if (index2Order < 1)
            mergedBuffer[mergedBufferIndex++] = index2;

        return SmallIndexSet.Create(mergedBuffer[..mergedBufferIndex]);
    }

    public static SmallIndexSet Merge(SmallIndexSet indexSet1, SmallIndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexArray2 = indexSet2.GetInternalIndexArray();

        if (indexArray1.Length == 0)
            return indexSet2;

        if (indexArray2.Length == 0)
            return indexSet1;

        var count1 = indexArray1.Length;
        var count2 = indexArray2.Length;

        var index1Order = 0;
        var index2Order = 0;

        var index1 = indexArray1[0];
        var index2 = indexArray2[0];

        var mergedBufferIndex = 0;
        Span<int> mergedBuffer = stackalloc int[count1 + count2];

        while (index1Order < count1 && index2Order < count2)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                index1Order++;

                mergedBuffer[mergedBufferIndex++] = index1;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                index2Order++;

                mergedBuffer[mergedBufferIndex++] = index2;

                if (index2Order < count2) index2 = indexArray2[index2Order];
            }
            else // Found common integer, do swaps
            {
                index1Order++;
                index2Order++;

                if (index1Order < count1) index1 = indexArray1[index1Order];
                if (index2Order < count2) index2 = indexArray2[index2Order];
            }
        }

        // Copy remaining from arr1
        while (index1Order < count1)
            mergedBuffer[mergedBufferIndex++] = indexArray1[index1Order++];

        // Copy remaining from arr2
        while (index2Order < count2)
            mergedBuffer[mergedBufferIndex++] = indexArray2[index2Order++];

        return SmallIndexSet.Create(mergedBuffer[..mergedBufferIndex]);
    }


    public static SmallIndexSet Difference(SmallIndexSet indexSet1, SmallIndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexArray2 = indexSet2.GetInternalIndexArray();

        if (indexArray1.Length == 0)
            return SmallIndexSet.EmptySet;

        if (indexArray2.Length == 0)
            return indexSet1;

        var count1 = indexArray1.Length;
        var count2 = indexArray2.Length;

        var index1Order = 0;
        var index2Order = 0;

        var index1 = indexArray1[0];
        var index2 = indexArray2[0];

        var mergedBufferIndex = 0;
        Span<int> mergedBuffer = stackalloc int[count1 + count2];

        while (index1Order < count1 && index2Order < count2)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                index1Order++;

                mergedBuffer[mergedBufferIndex++] = index1;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                index2Order++;

                if (index2Order < count2) index2 = indexArray2[index2Order];
            }
            else // Found common integer, do swaps
            {
                index1Order++;
                index2Order++;

                if (index1Order < count1) index1 = indexArray1[index1Order];
                if (index2Order < count2) index2 = indexArray2[index2Order];
            }
        }

        // Copy remaining from arr1
        while (index1Order < count1)
            mergedBuffer[mergedBufferIndex++] = indexArray1[index1Order++];

        return SmallIndexSet.Create(mergedBuffer[..mergedBufferIndex]);
    }


    public static int CountSwapsWithSelf(SmallIndexSet indexSet1)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();

        if (indexArray1.Length == 0)
            return 0;

        var swapCount = 0;
        var count1 = indexArray1.Length;
        var index1Order = 0;

        while (index1Order < count1)
        {
            index1Order++;

            swapCount += count1 - index1Order;
        }

        return swapCount;
    }

    public static int CountSwaps(SmallIndexSet indexSet1, int index2)
    {
        Debug.Assert(index2 >= 0);

        var indexArray1 = indexSet1.GetInternalIndexArray();

        if (indexArray1.Length == 0)
            return 0;

        var swapCount = 0;

        var count1 = indexArray1.Length;

        var index1Order = 0;
        var index2Order = 0;

        var index1 = indexArray1[0];

        while (index1Order < count1 && index2Order < 1)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                index1Order++;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                index2Order++;

                swapCount += count1 - index1Order;
            }
            else // Found common integer, do swaps
            {
                index1Order++;
                index2Order++;

                swapCount += count1 - index1Order;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
        }

        return swapCount;
    }

    public static int CountSwaps(SmallIndexSet indexSet1, SmallIndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexArray2 = indexSet2.GetInternalIndexArray();

        if (indexArray1.Length == 0 || indexArray2.Length == 0)
            return 0;

        var swapCount = 0;

        var count1 = indexArray1.Length;
        var count2 = indexArray2.Length;

        var index1Order = 0;
        var index2Order = 0;

        var index1 = indexArray1[0];
        var index2 = indexArray2[0];

        while (index1Order < count1 && index2Order < count2)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                index1Order++;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                index2Order++;

                swapCount += count1 - index1Order;

                if (index2Order < count2) index2 = indexArray2[index2Order];
            }
            else // Found common integer, do swaps
            {
                index1Order++;
                index2Order++;

                swapCount += count1 - index1Order;

                if (index1Order < count1) index1 = indexArray1[index1Order];
                if (index2Order < count2) index2 = indexArray2[index2Order];
            }
        }

        return swapCount;
    }


    public static (int swapCount, SmallIndexSet mergedSmallIndexSet) MergeCountSwaps(SmallIndexSet indexSet1, int index2)
    {
        Debug.Assert(index2 >= 0);

        var indexArray1 = indexSet1.GetInternalIndexArray();

        if (indexArray1.Length == 0)
            return (
                0,
                SmallIndexSet.Create(index2)
            );

        var swapCount = 0;

        var count1 = indexArray1.Length;

        var index1Order = 0;
        var index2Order = 0;

        var index1 = indexArray1[0];

        var mergedBufferIndex = 0;
        Span<int> mergedBuffer = stackalloc int[count1 + 1];

        while (index1Order < count1 && index2Order < 1)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                index1Order++;

                mergedBuffer[mergedBufferIndex++] = index1;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                index2Order++;

                mergedBuffer[mergedBufferIndex++] = index2;
                swapCount += count1 - index1Order;
            }
            else // Found common integer, do swaps
            {
                index1Order++;
                index2Order++;

                swapCount += count1 - index1Order;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
        }

        // Copy remaining from arr1
        while (index1Order < count1)
            mergedBuffer[mergedBufferIndex++] = indexArray1[index1Order++];

        // Copy remaining from arr2
        if (index2Order < 1)
            mergedBuffer[mergedBufferIndex++] = index2;

        var mergedSmallIndexSet = SmallIndexSet.Create(mergedBuffer[..mergedBufferIndex]);

        return (swapCount, mergedSmallIndexSet);
    }

    public static (int swapCount, SmallIndexSet mergedSmallIndexSet) MergeCountSwaps(SmallIndexSet indexSet1, SmallIndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexArray2 = indexSet2.GetInternalIndexArray();

        if (indexArray1.Length == 0)
            return (0, indexSet2);

        if (indexArray2.Length == 0)
            return (0, indexSet1);

        var swapCount = 0;

        var count1 = indexArray1.Length;
        var count2 = indexArray2.Length;

        var index1Order = 0;
        var index2Order = 0;

        var index1 = indexArray1[0];
        var index2 = indexArray2[0];

        var mergedBufferIndex = 0;
        Span<int> mergedBuffer = stackalloc int[count1 + count2];

        while (index1Order < count1 && index2Order < count2)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                index1Order++;

                mergedBuffer[mergedBufferIndex++] = index1;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                index2Order++;

                mergedBuffer[mergedBufferIndex++] = index2;
                swapCount += count1 - index1Order;

                if (index2Order < count2) index2 = indexArray2[index2Order];
            }
            else // Found common integer, do swaps
            {
                index1Order++;
                index2Order++;

                swapCount += count1 - index1Order;

                if (index1Order < count1) index1 = indexArray1[index1Order];
                if (index2Order < count2) index2 = indexArray2[index2Order];
            }
        }

        // Copy remaining from arr1
        while (index1Order < count1)
            mergedBuffer[mergedBufferIndex++] = indexArray1[index1Order++];

        // Copy remaining from arr2
        while (index2Order < count2)
            mergedBuffer[mergedBufferIndex++] = indexArray2[index2Order++];

        var mergedSmallIndexSet = SmallIndexSet.Create(mergedBuffer[..mergedBufferIndex]);

        return (swapCount, mergedSmallIndexSet);
    }


    public static (int swapCount, SmallIndexSet mergedSmallIndexSet, SmallIndexSet commonSmallIndexSet) MergeCountSwapsTrackCommon(SmallIndexSet indexSet1, int index2)
    {
        Debug.Assert(index2 >= 0);

        var indexArray1 = indexSet1.GetInternalIndexArray();

        if (indexArray1.Length == 0)
            return (
                0,
                SmallIndexSet.Create(index2),
                SmallIndexSet.EmptySet
            );

        var swapCount = 0;

        var count1 = indexArray1.Length;

        var index1Order = 0;
        var index2Order = 0;

        var index1 = indexArray1[0];

        var mergedBufferIndex = 0;
        Span<int> mergedBuffer = stackalloc int[count1 + 1];

        var common = false;

        while (index1Order < count1 && index2Order < 1)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                index1Order++;

                mergedBuffer[mergedBufferIndex++] = index1;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                index2Order++;

                mergedBuffer[mergedBufferIndex++] = index2;
                swapCount += count1 - index1Order;
            }
            else // Found common integer, do swaps
            {
                index1Order++;
                index2Order++;

                common = true;
                swapCount += count1 - index1Order;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
        }

        // Copy remaining from arr1
        while (index1Order < count1)
            mergedBuffer[mergedBufferIndex++] = indexArray1[index1Order++];

        // Copy remaining from arr2
        if (index2Order < 1)
            mergedBuffer[mergedBufferIndex++] = index2;

        var mergedSmallIndexSet = SmallIndexSet.Create(mergedBuffer[..mergedBufferIndex]);
        var commonSmallIndexSet = common ? SmallIndexSet.Create(index2) : SmallIndexSet.EmptySet;

        return (swapCount, mergedSmallIndexSet, commonSmallIndexSet);
    }

    public static (int swapCount, SmallIndexSet mergedSmallIndexSet, SmallIndexSet commonSmallIndexSet) MergeCountSwapsTrackCommon(SmallIndexSet indexSet1, SmallIndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexArray2 = indexSet2.GetInternalIndexArray();

        if (indexArray1.Length == 0)
            return (0, indexSet2, SmallIndexSet.EmptySet);

        if (indexArray2.Length == 0)
            return (0, indexSet1, SmallIndexSet.EmptySet);

        var swapCount = 0;

        var count1 = indexArray1.Length;
        var count2 = indexArray2.Length;

        var index1Order = 0;
        var index2Order = 0;

        var index1 = indexArray1[0];
        var index2 = indexArray2[0];

        var mergedBufferIndex = 0;
        Span<int> mergedBuffer = stackalloc int[count1 + count2];

        var commonBufferIndex = 0;
        Span<int> commonBuffer = stackalloc int[Math.Min(count1, count2)];

        while (index1Order < count1 && index2Order < count2)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                index1Order++;

                mergedBuffer[mergedBufferIndex++] = index1;

                if (index1Order < count1) index1 = indexArray1[index1Order];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                index2Order++;

                mergedBuffer[mergedBufferIndex++] = index2;
                swapCount += count1 - index1Order;

                if (index2Order < count2) index2 = indexArray2[index2Order];
            }
            else // Found common integer, do swaps
            {
                index1Order++;
                index2Order++;

                commonBuffer[commonBufferIndex++] = index1;
                swapCount += count1 - index1Order;

                if (index1Order < count1) index1 = indexArray1[index1Order];
                if (index2Order < count2) index2 = indexArray2[index2Order];
            }
        }

        // Copy remaining from arr1
        while (index1Order < count1)
            mergedBuffer[mergedBufferIndex++] = indexArray1[index1Order++];

        // Copy remaining from arr2
        while (index2Order < count2)
            mergedBuffer[mergedBufferIndex++] = indexArray2[index2Order++];

        var mergedSmallIndexSet = SmallIndexSet.Create(mergedBuffer[..mergedBufferIndex]);
        var commonSmallIndexSet = SmallIndexSet.Create(commonBuffer[..commonBufferIndex]);

        return (swapCount, mergedSmallIndexSet, commonSmallIndexSet);
    }


}