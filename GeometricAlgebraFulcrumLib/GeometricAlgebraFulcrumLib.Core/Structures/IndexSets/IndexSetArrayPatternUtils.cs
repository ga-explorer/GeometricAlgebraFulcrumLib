using System.Numerics;

namespace GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

internal static class IndexSetArrayPatternUtils
{
    
    public static bool Contains(IndexSet indexSet1, IndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexPattern2 = indexSet2.ToUInt64();

        if (indexArray1.Length == 0 || indexPattern2 == 0UL)
            return false;

        var count1 = indexArray1.Length;
        var count2 = BitOperations.PopCount(indexPattern2);

        var indexOrder1 = 0;
        var indexOrder2 = 0; 

        var index1 = indexArray1[0];
        var index2 = BitOperations.TrailingZeroCount(indexPattern2);
        
        while (indexOrder1 < count1 && indexPattern2 != 0UL)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                indexOrder1++;

                if (indexOrder1 < count1) index1 = indexArray1[indexOrder1];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                return false;
            }
            else // Found common integer, do swaps
            {
                indexOrder1++;
                indexOrder2++;
                indexPattern2 &= indexPattern2 - 1;
                
                if (indexOrder1 < count1) index1 = indexArray1[indexOrder1];
                if (indexOrder2 < count2) index2 = BitOperations.TrailingZeroCount(indexPattern2);
            }
        }

        return indexPattern2 == 0UL;
    }
    
    public static bool Overlaps(IndexSet indexSet1, IndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexPattern2 = indexSet2.ToUInt64();

        if (indexArray1.Length == 0 || indexPattern2 == 0UL)
            return false;

        var count1 = indexArray1.Length;
        var count2 = BitOperations.PopCount(indexPattern2);

        var indexOrder1 = 0;
        var indexOrder2 = 0; 

        var index1 = indexArray1[0];
        var index2 = BitOperations.TrailingZeroCount(indexPattern2);
        
        while (indexOrder1 < count1 && indexPattern2 != 0UL)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                indexOrder1++;

                if (indexOrder1 < count1) index1 = indexArray1[indexOrder1];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                indexOrder2++;
                indexPattern2 &= indexPattern2 - 1;

                if (indexOrder2 < count2) index2 = BitOperations.TrailingZeroCount(indexPattern2);
            }
            else // Found common integer, do swaps
            {
                return true;
            }
        }

        return false;
    }

    public static IndexSet Intersect(IndexSet indexSet1, IndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexPattern2 = indexSet2.ToUInt64();

        if (indexArray1.Length == 0 || indexPattern2 == 0UL)
            return IndexSet.EmptySet;

        var count1 = indexArray1.Length;
        var count2 = BitOperations.PopCount(indexPattern2);

        var indexOrder1 = 0;
        var indexOrder2 = 0; 

        var index1 = indexArray1[0];
        var index2 = BitOperations.TrailingZeroCount(indexPattern2);
        
        var commonBuffer = 0UL;

        while (indexOrder1 < count1 && indexPattern2 != 0UL)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                indexOrder1++;

                if (indexOrder1 < count1) index1 = indexArray1[indexOrder1];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                indexOrder2++;
                indexPattern2 &= indexPattern2 - 1;

                if (indexOrder2 < count2) index2 = BitOperations.TrailingZeroCount(indexPattern2);
            }
            else // Found common integer, do swaps
            {
                indexOrder1++;
                indexOrder2++;
                indexPattern2 &= indexPattern2 - 1;
                
                commonBuffer |= 1UL << index1;

                if (indexOrder1 < count1) index1 = indexArray1[indexOrder1];
                if (indexOrder2 < count2) index2 = BitOperations.TrailingZeroCount(indexPattern2);
            }
        }

        return IndexSet.CreateFromUInt64Pattern(commonBuffer);
    }
    
    public static IndexSet Join(IndexSet indexSet1, IndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexPattern2 = indexSet2.ToUInt64();

        if (indexArray1.Length == 0)
            return indexSet2;

        if (indexPattern2 == 0UL)
            return indexSet1;

        var count1 = indexArray1.Length;
        var count2 = BitOperations.PopCount(indexPattern2);

        var indexOrder1 = 0;
        var indexOrder2 = 0; 

        var index1 = indexArray1[0];
        var index2 = BitOperations.TrailingZeroCount(indexPattern2);
        
        var mergedBufferIndex = 0;
        Span<int> mergedBuffer = stackalloc int[count1 + count2];

        while (indexOrder1 < count1 && indexPattern2 != 0UL)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                indexOrder1++;

                mergedBuffer[mergedBufferIndex++] = index1;

                if (indexOrder1 < count1) index1 = indexArray1[indexOrder1];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                indexOrder2++;
                indexPattern2 &= indexPattern2 - 1;

                mergedBuffer[mergedBufferIndex++] = index2;
                
                if (indexOrder2 < count2) index2 = BitOperations.TrailingZeroCount(indexPattern2);
            }
            else // Found common integer, do swaps
            {
                indexOrder1++;
                indexOrder2++;
                indexPattern2 &= indexPattern2 - 1;
                
                mergedBuffer[mergedBufferIndex++] = index1;

                if (indexOrder1 < count1) index1 = indexArray1[indexOrder1];
                if (indexOrder2 < count2) index2 = BitOperations.TrailingZeroCount(indexPattern2);
            }
        }

        // Copy remaining from arr1
        while (indexOrder1 < count1) 
            mergedBuffer[mergedBufferIndex++] = indexArray1[indexOrder1++];

        // Copy remaining from arr2
        while (indexPattern2 != 0UL)
        {
            mergedBuffer[mergedBufferIndex++] = BitOperations.TrailingZeroCount(indexPattern2);
            indexPattern2 &= indexPattern2 - 1;
        }

        return IndexSet.Create(mergedBuffer[..mergedBufferIndex]);
    }
    
    public static IndexSet Merge(IndexSet indexSet1, IndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexPattern2 = indexSet2.ToUInt64();

        if (indexArray1.Length == 0)
            return indexSet2;

        if (indexPattern2 == 0UL)
            return indexSet1;

        var count1 = indexArray1.Length;
        var count2 = BitOperations.PopCount(indexPattern2);

        var indexOrder1 = 0;
        var indexOrder2 = 0; 

        var index1 = indexArray1[0];
        var index2 = BitOperations.TrailingZeroCount(indexPattern2);
        
        var mergedBufferIndex = 0;
        Span<int> mergedBuffer = stackalloc int[count1 + count2];

        while (indexOrder1 < count1 && indexPattern2 != 0UL)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                indexOrder1++;

                mergedBuffer[mergedBufferIndex++] = index1;

                if (indexOrder1 < count1) index1 = indexArray1[indexOrder1];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                indexOrder2++;
                indexPattern2 &= indexPattern2 - 1;

                mergedBuffer[mergedBufferIndex++] = index2;
                
                if (indexOrder2 < count2) index2 = BitOperations.TrailingZeroCount(indexPattern2);
            }
            else // Found common integer, do swaps
            {
                indexOrder1++;
                indexOrder2++;
                indexPattern2 &= indexPattern2 - 1;
                
                if (indexOrder1 < count1) index1 = indexArray1[indexOrder1];
                if (indexOrder2 < count2) index2 = BitOperations.TrailingZeroCount(indexPattern2);
            }
        }

        // Copy remaining from arr1
        while (indexOrder1 < count1) 
            mergedBuffer[mergedBufferIndex++] = indexArray1[indexOrder1++];

        // Copy remaining from arr2
        while (indexPattern2 != 0UL)
        {
            mergedBuffer[mergedBufferIndex++] = BitOperations.TrailingZeroCount(indexPattern2);
            indexPattern2 &= indexPattern2 - 1;
        }

        return IndexSet.Create(mergedBuffer[..mergedBufferIndex]);
    }
    
    public static IndexSet Difference(IndexSet indexSet1, IndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexPattern2 = indexSet2.ToUInt64();

        if (indexArray1.Length == 0)
            return IndexSet.EmptySet;

        if (indexPattern2 == 0UL)
            return indexSet1;

        var count1 = indexArray1.Length;
        var count2 = BitOperations.PopCount(indexPattern2);

        var indexOrder1 = 0;
        var indexOrder2 = 0; 

        var index1 = indexArray1[0];
        var index2 = BitOperations.TrailingZeroCount(indexPattern2);
        
        var mergedBufferIndex = 0;
        Span<int> mergedBuffer = stackalloc int[count1 + count2];

        while (indexOrder1 < count1 && indexPattern2 != 0UL)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                indexOrder1++;

                mergedBuffer[mergedBufferIndex++] = index1;

                if (indexOrder1 < count1) index1 = indexArray1[indexOrder1];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                indexOrder2++;
                indexPattern2 &= indexPattern2 - 1;

                if (indexOrder2 < count2) index2 = BitOperations.TrailingZeroCount(indexPattern2);
            }
            else // Found common integer, do swaps
            {
                indexOrder1++;
                indexOrder2++;
                indexPattern2 &= indexPattern2 - 1;
                
                if (indexOrder1 < count1) index1 = indexArray1[indexOrder1];
                if (indexOrder2 < count2) index2 = BitOperations.TrailingZeroCount(indexPattern2);
            }
        }

        // Copy remaining from arr1
        while (indexOrder1 < count1) 
            mergedBuffer[mergedBufferIndex++] = indexArray1[indexOrder1++];

        return IndexSet.Create(mergedBuffer[..mergedBufferIndex]);
    }
    
    public static int CountSwaps(IndexSet indexSet1, IndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexPattern2 = indexSet2.ToUInt64();

        if (indexArray1.Length == 0 || indexPattern2 == 0UL)
            return 0;

        var swapCount = 0;

        var count1 = indexArray1.Length;
        var count2 = BitOperations.PopCount(indexPattern2);

        var indexOrder1 = 0;
        var indexOrder2 = 0; 

        var index1 = indexArray1[0];
        var index2 = BitOperations.TrailingZeroCount(indexPattern2);
        
        while (indexOrder1 < count1 && indexPattern2 != 0UL)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                indexOrder1++;

                if (indexOrder1 < count1) index1 = indexArray1[indexOrder1];
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
                indexOrder2++;
                indexPattern2 &= indexPattern2 - 1;
                
                swapCount += count1 - indexOrder1;

                if (indexOrder1 < count1) index1 = indexArray1[indexOrder1];
                if (indexOrder2 < count2) index2 = BitOperations.TrailingZeroCount(indexPattern2);
            }
        }

        return swapCount;
    }

    public static (int swapCount, IndexSet mergedIndexSet) MergeCountSwaps(IndexSet indexSet1, IndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexPattern2 = indexSet2.ToUInt64();

        if (indexArray1.Length == 0)
            return (0, indexSet2);

        if (indexPattern2 == 0UL)
            return (0, indexSet1);

        var swapCount = 0;

        var count1 = indexArray1.Length;
        var count2 = BitOperations.PopCount(indexPattern2);

        var indexOrder1 = 0;
        var indexOrder2 = 0; 

        var index1 = indexArray1[0];
        var index2 = BitOperations.TrailingZeroCount(indexPattern2);
        
        var mergedBufferIndex = 0;
        Span<int> mergedBuffer = stackalloc int[count1 + count2];

        while (indexOrder1 < count1 && indexPattern2 != 0UL)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                indexOrder1++;

                mergedBuffer[mergedBufferIndex++] = index1;

                if (indexOrder1 < count1) index1 = indexArray1[indexOrder1];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                indexOrder2++;
                indexPattern2 &= indexPattern2 - 1;

                mergedBuffer[mergedBufferIndex++] = index2;
                swapCount += count1 - indexOrder1;
                
                if (indexOrder2 < count2) index2 = BitOperations.TrailingZeroCount(indexPattern2);
            }
            else // Found common integer, do swaps
            {
                indexOrder1++;
                indexOrder2++;
                indexPattern2 &= indexPattern2 - 1;
                
                swapCount += count1 - indexOrder1;

                if (indexOrder1 < count1) index1 = indexArray1[indexOrder1];
                if (indexOrder2 < count2) index2 = BitOperations.TrailingZeroCount(indexPattern2);
            }
        }

        // Copy remaining from arr1
        while (indexOrder1 < count1) 
            mergedBuffer[mergedBufferIndex++] = indexArray1[indexOrder1++];

        // Copy remaining from arr2
        while (indexPattern2 != 0UL)
        {
            mergedBuffer[mergedBufferIndex++] = BitOperations.TrailingZeroCount(indexPattern2);
            indexPattern2 &= indexPattern2 - 1;
        }

        var mergedIndexSet = IndexSet.Create(mergedBuffer[..mergedBufferIndex]);

        return (swapCount, mergedIndexSet);
    }
    
    public static (int swapCount, IndexSet mergedIndexSet, IndexSet commonIndexSet) MergeCountSwapsTrackCommon(IndexSet indexSet1, IndexSet indexSet2)
    {
        var indexArray1 = indexSet1.GetInternalIndexArray();
        var indexPattern2 = indexSet2.ToUInt64();

        if (indexArray1.Length == 0)
            return (0, indexSet2, IndexSet.EmptySet);

        if (indexPattern2 == 0UL)
            return (0, indexSet1, IndexSet.EmptySet);

        var swapCount = 0;

        var count1 = indexArray1.Length;
        var count2 = BitOperations.PopCount(indexPattern2);

        var indexOrder1 = 0;
        var indexOrder2 = 0; 

        var index1 = indexArray1[0];
        var index2 = BitOperations.TrailingZeroCount(indexPattern2);
        
        var mergedBufferIndex = 0;
        Span<int> mergedBuffer = stackalloc int[count1 + count2];

        var commonBuffer = 0UL;

        while (indexOrder1 < count1 && indexPattern2 != 0UL)
        {
            if (index1 < index2) // Take value from first array, no swaps needed
            {
                indexOrder1++;

                mergedBuffer[mergedBufferIndex++] = index1;

                if (indexOrder1 < count1) index1 = indexArray1[indexOrder1];
            }
            else if (index1 > index2) // Take value from second array, do swaps
            {
                indexOrder2++;
                indexPattern2 &= indexPattern2 - 1;

                mergedBuffer[mergedBufferIndex++] = index2;
                swapCount += count1 - indexOrder1;
                
                if (indexOrder2 < count2) index2 = BitOperations.TrailingZeroCount(indexPattern2);
            }
            else // Found common integer, do swaps
            {
                indexOrder1++;
                indexOrder2++;
                indexPattern2 &= indexPattern2 - 1;
                
                commonBuffer |= 1UL << index1;
                swapCount += count1 - indexOrder1;

                if (indexOrder1 < count1) index1 = indexArray1[indexOrder1];
                if (indexOrder2 < count2) index2 = BitOperations.TrailingZeroCount(indexPattern2);
            }
        }

        // Copy remaining from arr1
        while (indexOrder1 < count1) 
            mergedBuffer[mergedBufferIndex++] = indexArray1[indexOrder1++];

        // Copy remaining from arr2
        while (indexPattern2 != 0UL)
        {
            mergedBuffer[mergedBufferIndex++] = BitOperations.TrailingZeroCount(indexPattern2);
            indexPattern2 &= indexPattern2 - 1;
        }

        var mergedIndexSet = IndexSet.Create(mergedBuffer[..mergedBufferIndex]);
        var commonIndexSet = IndexSet.CreateFromUInt64Pattern(commonBuffer);

        return (swapCount, mergedIndexSet, commonIndexSet);
    }
}