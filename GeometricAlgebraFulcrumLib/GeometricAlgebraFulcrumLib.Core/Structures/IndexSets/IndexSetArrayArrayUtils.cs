using System.Diagnostics;

namespace GeometricAlgebraFulcrumLib.Core.Structures.IndexSets
{
    internal static class IndexSetArrayArrayUtils
    {
        
        public static bool Contains(IndexSet indexSet1, IndexSet indexSet2)
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
        
        public static bool Overlaps(IndexSet indexSet1, IndexSet indexSet2)
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


        public static IndexSet Intersect(IndexSet indexSet1, int index2)
        {
            var indexArray1 = indexSet1.GetInternalIndexArray();
            
            if (indexArray1.Length == 0)
                return IndexSet.EmptySet;

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

            return IndexSet.Create(commonBuffer[..commonBufferIndex]);
        }

        public static IndexSet Intersect(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexArray1 = indexSet1.GetInternalIndexArray();
            var indexArray2 = indexSet2.GetInternalIndexArray();

            if (indexArray1.Length == 0 || indexArray2.Length == 0)
                return IndexSet.EmptySet;

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

            return IndexSet.Create(commonBuffer[..commonBufferIndex]);
        }
        

        public static IndexSet Join(IndexSet indexSet1, int index2)
        {
            Debug.Assert(index2 >= 0);

            var indexArray1 = indexSet1.GetInternalIndexArray();
            
            if (indexArray1.Length == 0)
                return IndexSet.CreateUnit(index2);

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

            return IndexSet.Create(mergedBuffer[..mergedBufferIndex]);
        }

        public static IndexSet Join(IndexSet indexSet1, IndexSet indexSet2)
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

            return IndexSet.Create(mergedBuffer[..mergedBufferIndex]);
        }


        public static IndexSet Merge(IndexSet indexSet1, int index2)
        {
            Debug.Assert(index2 >= 0);

            var indexArray1 = indexSet1.GetInternalIndexArray();
            
            if (indexArray1.Length == 0)
                return IndexSet.CreateUnit(index2);

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

            return IndexSet.Create(mergedBuffer[..mergedBufferIndex]);
        }

        public static IndexSet Merge(IndexSet indexSet1, IndexSet indexSet2)
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

            return IndexSet.Create(mergedBuffer[..mergedBufferIndex]);
        }
        

        public static IndexSet Difference(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexArray1 = indexSet1.GetInternalIndexArray();
            var indexArray2 = indexSet2.GetInternalIndexArray();

            if (indexArray1.Length == 0)
                return IndexSet.EmptySet;

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

            return IndexSet.Create(mergedBuffer[..mergedBufferIndex]);
        }

        
        public static int CountSwapsWithSelf(IndexSet indexSet1)
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

        public static int CountSwaps(IndexSet indexSet1, int index2)
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

        public static int CountSwaps(IndexSet indexSet1, IndexSet indexSet2)
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

        
        public static (int swapCount, IndexSet mergedIndexSet) MergeCountSwaps(IndexSet indexSet1, int index2)
        {
            Debug.Assert(index2 >= 0);

            var indexArray1 = indexSet1.GetInternalIndexArray();
            
            if (indexArray1.Length == 0)
                return (
                    0, 
                    IndexSet.CreateUnit(index2)
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

            var mergedIndexSet = IndexSet.Create(mergedBuffer[..mergedBufferIndex]);
            
            return (swapCount, mergedIndexSet);
        }

        public static (int swapCount, IndexSet mergedIndexSet) MergeCountSwaps(IndexSet indexSet1, IndexSet indexSet2)
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

            var mergedIndexSet = IndexSet.Create(mergedBuffer[..mergedBufferIndex]);
            
            return (swapCount, mergedIndexSet);
        }

        
        public static (int swapCount, IndexSet mergedIndexSet, IndexSet commonIndexSet) MergeCountSwapsTrackCommon(IndexSet indexSet1, int index2)
        {
            Debug.Assert(index2 >= 0);

            var indexArray1 = indexSet1.GetInternalIndexArray();
            
            if (indexArray1.Length == 0)
                return (
                    0, 
                    IndexSet.CreateUnit(index2), 
                    IndexSet.EmptySet
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

            var mergedIndexSet = IndexSet.Create(mergedBuffer[..mergedBufferIndex]);
            var commonIndexSet = common ? IndexSet.CreateUnit(index2) : IndexSet.EmptySet;

            return (swapCount, mergedIndexSet, commonIndexSet);
        }

        public static (int swapCount, IndexSet mergedIndexSet, IndexSet commonIndexSet) MergeCountSwapsTrackCommon(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexArray1 = indexSet1.GetInternalIndexArray();
            var indexArray2 = indexSet2.GetInternalIndexArray();

            if (indexArray1.Length == 0)
                return (0, indexSet2, IndexSet.EmptySet);

            if (indexArray2.Length == 0)
                return (0, indexSet1, IndexSet.EmptySet);

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

            var mergedIndexSet = IndexSet.Create(mergedBuffer[..mergedBufferIndex]);
            var commonIndexSet = IndexSet.Create(commonBuffer[..commonBufferIndex]);

            return (swapCount, mergedIndexSet, commonIndexSet);
        }


    }
}
