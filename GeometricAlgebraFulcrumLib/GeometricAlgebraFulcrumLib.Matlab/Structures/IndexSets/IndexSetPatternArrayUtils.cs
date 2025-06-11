using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;

namespace GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets
{
    internal static class IndexSetPatternArrayUtils
    {
    
        public static bool IsSuperset(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexArray2 = indexSet2.GetInternalIndexArray();

            if (indexArray2.Length == 0)
                return true;

            if (indexPattern1 == 0)
                return false;

            var count1 = BitOperations.PopCount(indexPattern1);
            var count2 = indexArray2.Length;

            var indexOrder1 = 0;
            var indexOrder2 = 0; 
            
            var index1 = BitOperations.TrailingZeroCount(indexPattern1);
            var index2 = indexArray2[0];
        
            while (indexPattern1 != 0UL && indexOrder2 < count2)
            {
                if (index1 < index2) // Take value from first array, no swaps needed
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;

                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                }
                else if (index1 > index2) // Take value from second array, do swaps
                {
                    return false;
                }
                else // Found common integer, do swaps
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;
                    indexOrder2++;

                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                    if (indexOrder2 < count2) index2 = indexArray2[indexOrder2];
                }
            }

            return indexOrder2 >= count2;
        }

        public static bool Contains(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexArray2 = indexSet2.GetInternalIndexArray();

            if (indexPattern1 == 0UL || indexArray2.Length == 0)
                return false;

            var count1 = BitOperations.PopCount(indexPattern1);
            var count2 = indexArray2.Length;

            var indexOrder1 = 0;
            var indexOrder2 = 0; 
            
            var index1 = BitOperations.TrailingZeroCount(indexPattern1);
            var index2 = indexArray2[0];
        
            while (indexPattern1 != 0UL && indexOrder2 < count2)
            {
                if (index1 < index2) // Take value from first array, no swaps needed
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;

                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                }
                else if (index1 > index2) // Take value from second array, do swaps
                {
                    return false;
                }
                else // Found common integer, do swaps
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;
                    indexOrder2++;

                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                    if (indexOrder2 < count2) index2 = indexArray2[indexOrder2];
                }
            }

            return indexOrder2 >= count2;
        }
    
        public static bool Overlaps(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexArray2 = indexSet2.GetInternalIndexArray();

            if (indexPattern1 == 0UL || indexArray2.Length == 0)
                return false;

            var count1 = BitOperations.PopCount(indexPattern1);
            var count2 = indexArray2.Length;

            var indexOrder1 = 0;
            var indexOrder2 = 0; 
            
            var index1 = BitOperations.TrailingZeroCount(indexPattern1);
            var index2 = indexArray2[0];
        
            while (indexPattern1 != 0UL && indexOrder2 < count2)
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

                    if (indexOrder2 < count2) index2 = indexArray2[indexOrder2];
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
            var indexPattern1 = indexSet1.ToUInt64();
        
            if (indexPattern1 == 0UL)
                return IndexSet.EmptySet;

            var count1 = BitOperations.PopCount(indexPattern1);
        
            var indexOrder1 = 0;
        
            var index1 = BitOperations.TrailingZeroCount(indexPattern1);
        
            var commonBuffer = 0UL;
            
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
                    break;
                }
                else // Found common integer, do swaps
                {
                    commonBuffer |= 1UL << index1;

                    break;
                }
            }

            return IndexSet.CreateFromUInt64Pattern(commonBuffer);
        }

        public static IndexSet Intersect(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexArray2 = indexSet2.GetInternalIndexArray();

            if (indexPattern1 == 0UL || indexArray2.Length == 0)
                return IndexSet.EmptySet;

            var count1 = BitOperations.PopCount(indexPattern1);
            var count2 = indexArray2.Length;

            var indexOrder1 = 0;
            var indexOrder2 = 0; 
            
            var index1 = BitOperations.TrailingZeroCount(indexPattern1);
            var index2 = indexArray2[0];
        
            var commonBuffer = 0UL;
            
            while (indexPattern1 != 0UL && indexOrder2 < count2)
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

                    if (indexOrder2 < count2) index2 = indexArray2[indexOrder2];
                }
                else // Found common integer, do swaps
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;
                    indexOrder2++;

                    commonBuffer |= 1UL << index1;

                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                    if (indexOrder2 < count2) index2 = indexArray2[indexOrder2];
                }
            }

            return IndexSet.CreateFromUInt64Pattern(commonBuffer);
        }
    

        public static IndexSet Join(IndexSet indexSet1, int index2)
        {
            var bitPattern = indexSet1.ToUInt64();
            var indexArray = new int[BitOperations.PopCount(bitPattern) + 1];
            var indexOrder = 0;

            while (bitPattern != 0)
            {
                var index1 = BitOperations.TrailingZeroCount(bitPattern);
                indexArray[indexOrder++] = index1;
                bitPattern &= bitPattern - 1; // Clear the least significant bit
            }

            indexArray[indexOrder] = index2;

            return IndexSet.Create(indexArray);
        }

        public static IndexSet Join(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexArray2 = indexSet2.GetInternalIndexArray();

            if (indexPattern1 == 0UL)
                return indexSet2;

            if (indexArray2.Length == 0)
                return indexSet1;

            var count1 = BitOperations.PopCount(indexPattern1);
            var count2 = indexArray2.Length;

            var indexOrder1 = 0;
            var indexOrder2 = 0; 
            
            var index1 = BitOperations.TrailingZeroCount(indexPattern1);
            var index2 = indexArray2[0];
        
            var mergedBufferIndex = 0;
            var mergedBuffer = new int[count1 + count2];

            while (indexPattern1 != 0UL && indexOrder2 < count2)
            {
                if (index1 < index2) // Take value from first array, no swaps needed
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;

                    mergedBuffer[mergedBufferIndex++] = index1;
                
                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                }
                else if (index1 > index2) // Take value from second array, do swaps
                {
                    indexOrder2++;

                    mergedBuffer[mergedBufferIndex++] = index2;
                
                    if (indexOrder2 < count2) index2 = indexArray2[indexOrder2];
                }
                else // Found common integer, do swaps
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;
                    indexOrder2++;

                    mergedBuffer[mergedBufferIndex++] = index1;

                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                    if (indexOrder2 < count2) index2 = indexArray2[indexOrder2];
                }
            }

            // Copy remaining from arr1
            while (indexPattern1 != 0)
            {
                mergedBuffer[mergedBufferIndex++] = BitOperations.TrailingZeroCount(indexPattern1);
                indexPattern1 &= indexPattern1 - 1;
            }

            // Copy remaining from arr2
            while (indexOrder2 < count2)
                mergedBuffer[mergedBufferIndex++] = indexArray2[indexOrder2++];

            return IndexSet.Create(mergedBuffer.CopyOfRange(0, mergedBufferIndex));
        }

    
        public static IndexSet Difference(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexArray2 = indexSet2.GetInternalIndexArray();

            if (indexPattern1 == 0UL)
                return IndexSet.EmptySet;

            if (indexArray2.Length == 0)
                return indexSet1;

            var count1 = BitOperations.PopCount(indexPattern1);
            var count2 = indexArray2.Length;

            var indexOrder1 = 0;
            var indexOrder2 = 0; 
            
            var index1 = BitOperations.TrailingZeroCount(indexPattern1);
            var index2 = indexArray2[0];
        
            var mergedBufferIndex = 0;
            var mergedBuffer = new int[count1 + count2];

            while (indexPattern1 != 0UL && indexOrder2 < count2)
            {
                if (index1 < index2) // Take value from first array, no swaps needed
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;

                    mergedBuffer[mergedBufferIndex++] = index1;
                
                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                }
                else if (index1 > index2) // Take value from second array, do swaps
                {
                    indexOrder2++;

                    if (indexOrder2 < count2) index2 = indexArray2[indexOrder2];
                }
                else // Found common integer, do swaps
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;
                    indexOrder2++;

                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                    if (indexOrder2 < count2) index2 = indexArray2[indexOrder2];
                }
            }

            // Copy remaining from arr1
            while (indexPattern1 != 0)
            {
                mergedBuffer[mergedBufferIndex++] = BitOperations.TrailingZeroCount(indexPattern1);
                indexPattern1 &= indexPattern1 - 1;
            }

            return IndexSet.Create(mergedBuffer.CopyOfRange(0, mergedBufferIndex));
        }


        public static IndexSet Merge(IndexSet indexSet1, int index2)
        {
            var bitPattern = indexSet1.ToUInt64();
            var indexArray = new int[BitOperations.PopCount(bitPattern) + 1];
            var indexOrder = 0;

            while (bitPattern != 0)
            {
                var index1 = BitOperations.TrailingZeroCount(bitPattern);
                indexArray[indexOrder++] = index1;
                bitPattern &= bitPattern - 1; // Clear the least significant bit
            }

            indexArray[indexOrder] = index2;

            return IndexSet.Create(indexArray);
        }

        public static IndexSet Merge(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexArray2 = indexSet2.GetInternalIndexArray();

            if (indexPattern1 == 0UL)
                return indexSet2;

            if (indexArray2.Length == 0)
                return indexSet1;

            var count1 = BitOperations.PopCount(indexPattern1);
            var count2 = indexArray2.Length;

            var indexOrder1 = 0;
            var indexOrder2 = 0; 
            
            var index1 = BitOperations.TrailingZeroCount(indexPattern1);
            var index2 = indexArray2[0];
        
            var mergedBufferIndex = 0;
            var mergedBuffer = new int[count1 + count2];

            while (indexPattern1 != 0UL && indexOrder2 < count2)
            {
                if (index1 < index2) // Take value from first array, no swaps needed
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;

                    mergedBuffer[mergedBufferIndex++] = index1;
                
                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                }
                else if (index1 > index2) // Take value from second array, do swaps
                {
                    indexOrder2++;

                    mergedBuffer[mergedBufferIndex++] = index2;
                
                    if (indexOrder2 < count2) index2 = indexArray2[indexOrder2];
                }
                else // Found common integer, do swaps
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;
                    indexOrder2++;

                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                    if (indexOrder2 < count2) index2 = indexArray2[indexOrder2];
                }
            }

            // Copy remaining from arr1
            while (indexPattern1 != 0)
            {
                mergedBuffer[mergedBufferIndex++] = BitOperations.TrailingZeroCount(indexPattern1);
                indexPattern1 &= indexPattern1 - 1;
            }

            // Copy remaining from arr2
            while (indexOrder2 < count2)
                mergedBuffer[mergedBufferIndex++] = indexArray2[indexOrder2++];

            return IndexSet.Create(mergedBuffer.CopyOfRange(0, mergedBufferIndex));
        }


        public static int CountSwaps(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexArray2 = indexSet2.GetInternalIndexArray();

            if (indexPattern1 == 0UL || indexArray2.Length == 0)
                return 0;

            var swapCount = 0;

            var count1 = BitOperations.PopCount(indexPattern1);
            var count2 = indexArray2.Length;

            var indexOrder1 = 0;
            var indexOrder2 = 0; 
            
            var index1 = BitOperations.TrailingZeroCount(indexPattern1);
            var index2 = indexArray2[0];
        
            while (indexPattern1 != 0UL && indexOrder2 < count2)
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

                    swapCount += count1 - indexOrder1;
                
                    if (indexOrder2 < count2) index2 = indexArray2[indexOrder2];
                }
                else // Found common integer, do swaps
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;
                    indexOrder2++;

                    swapCount += count1 - indexOrder1;

                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                    if (indexOrder2 < count2) index2 = indexArray2[indexOrder2];
                }
            }

            return swapCount;
        }


        public static (int swapCount, IndexSet mergedIndexSet) MergeCountSwaps(IndexSet indexSet1, int index2)
        {
            var bitPattern = indexSet1.ToUInt64();
            var indexArray = new int[BitOperations.PopCount(bitPattern) + 1];
            var indexOrder = 0;

            while (bitPattern != 0)
            {
                var index1 = BitOperations.TrailingZeroCount(bitPattern);
                indexArray[indexOrder++] = index1;
                bitPattern &= bitPattern - 1; // Clear the least significant bit
            }

            indexArray[indexOrder] = index2;

            return (
                0, 
                IndexSet.Create(indexArray)
            );
        }

        public static (int swapCount, IndexSet mergedIndexSet) MergeCountSwaps(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexArray2 = indexSet2.GetInternalIndexArray();

            if (indexPattern1 == 0UL)
                return (0, indexSet2);

            if (indexArray2.Length == 0)
                return (0, indexSet1);

            var swapCount = 0;

            var count1 = BitOperations.PopCount(indexPattern1);
            var count2 = indexArray2.Length;

            var indexOrder1 = 0;
            var indexOrder2 = 0; 
            
            var index1 = BitOperations.TrailingZeroCount(indexPattern1);
            var index2 = indexArray2[0];
        
            var mergedBufferIndex = 0;
            var mergedBuffer = new int[count1 + count2];

            while (indexPattern1 != 0UL && indexOrder2 < count2)
            {
                if (index1 < index2) // Take value from first array, no swaps needed
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;

                    mergedBuffer[mergedBufferIndex++] = index1;
                
                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                }
                else if (index1 > index2) // Take value from second array, do swaps
                {
                    indexOrder2++;

                    mergedBuffer[mergedBufferIndex++] = index2;
                    swapCount += count1 - indexOrder1;
                
                    if (indexOrder2 < count2) index2 = indexArray2[indexOrder2];
                }
                else // Found common integer, do swaps
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;
                    indexOrder2++;

                    swapCount += count1 - indexOrder1;

                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                    if (indexOrder2 < count2) index2 = indexArray2[indexOrder2];
                }
            }

            // Copy remaining from arr1
            while (indexPattern1 != 0)
            {
                mergedBuffer[mergedBufferIndex++] = BitOperations.TrailingZeroCount(indexPattern1);
                indexPattern1 &= indexPattern1 - 1;
            }

            // Copy remaining from arr2
            while (indexOrder2 < count2)
                mergedBuffer[mergedBufferIndex++] = indexArray2[indexOrder2++];

            var mergedIndexSet = IndexSet.Create(mergedBuffer.CopyOfRange(0, mergedBufferIndex));
        
            return (swapCount, mergedIndexSet);
        }


        public static (int swapCount, IndexSet mergedIndexSet, IndexSet commonIndexSet) MergeCountSwapsTrackCommon(IndexSet indexSet1, int index2)
        {
            var bitPattern = indexSet1.ToUInt64();
            var indexArray = new int[BitOperations.PopCount(bitPattern) + 1];
            var indexOrder = 0;

            while (bitPattern != 0)
            {
                var index1 = BitOperations.TrailingZeroCount(bitPattern);
                indexArray[indexOrder++] = index1;
                bitPattern &= bitPattern - 1; // Clear the least significant bit
            }

            indexArray[indexOrder] = index2;

            return (
                0, 
                IndexSet.Create(indexArray),
                IndexSet.EmptySet
            );
        }

        public static (int swapCount, IndexSet mergedIndexSet, IndexSet commonIndexSet) MergeCountSwapsTrackCommon(IndexSet indexSet1, IndexSet indexSet2)
        {
            var indexPattern1 = indexSet1.ToUInt64();
            var indexArray2 = indexSet2.GetInternalIndexArray();

            if (indexPattern1 == 0UL)
                return (0, indexSet2, IndexSet.EmptySet);

            if (indexArray2.Length == 0)
                return (0, indexSet1, IndexSet.EmptySet);

            var swapCount = 0;

            var count1 = BitOperations.PopCount(indexPattern1);
            var count2 = indexArray2.Length;

            var indexOrder1 = 0;
            var indexOrder2 = 0; 
            
            var index1 = BitOperations.TrailingZeroCount(indexPattern1);
            var index2 = indexArray2[0];
        
            var mergedBufferIndex = 0;
            var mergedBuffer = new int[count1 + count2];

            var commonBuffer = 0UL;
            
            while (indexPattern1 != 0UL && indexOrder2 < count2)
            {
                if (index1 < index2) // Take value from first array, no swaps needed
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;

                    mergedBuffer[mergedBufferIndex++] = index1;
                
                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                }
                else if (index1 > index2) // Take value from second array, do swaps
                {
                    indexOrder2++;

                    mergedBuffer[mergedBufferIndex++] = index2;
                    swapCount += count1 - indexOrder1;
                
                    if (indexOrder2 < count2) index2 = indexArray2[indexOrder2];
                }
                else // Found common integer, do swaps
                {
                    indexOrder1++;
                    indexPattern1 &= indexPattern1 - 1;
                    indexOrder2++;

                    commonBuffer |= 1UL << index1;
                    swapCount += count1 - indexOrder1;

                    if (indexOrder1 < count1) index1 = BitOperations.TrailingZeroCount(indexPattern1);
                    if (indexOrder2 < count2) index2 = indexArray2[indexOrder2];
                }
            }

            // Copy remaining from arr1
            while (indexPattern1 != 0)
            {
                mergedBuffer[mergedBufferIndex++] = BitOperations.TrailingZeroCount(indexPattern1);
                indexPattern1 &= indexPattern1 - 1;
            }

            // Copy remaining from arr2
            while (indexOrder2 < count2)
                mergedBuffer[mergedBufferIndex++] = indexArray2[indexOrder2++];

            var mergedIndexSet = IndexSet.Create(mergedBuffer.CopyOfRange(0, mergedBufferIndex));
            var commonIndexSet = IndexSet.CreateFromUInt64Pattern(commonBuffer);

            return (swapCount, mergedIndexSet, commonIndexSet);
        }
    }
}