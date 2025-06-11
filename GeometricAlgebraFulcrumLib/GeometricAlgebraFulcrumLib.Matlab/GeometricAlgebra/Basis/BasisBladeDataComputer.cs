using System;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Combibnations;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;

internal static class BasisBladeDataComputer
{
    
    public static uint BasisBladeGrade(ulong id)
    {
        return (uint) BitOperations.PopCount(id);
    }
        
    
    public static ulong BasisBladeIndex(ulong id)
    {
        return id.CombinadicPatternToIndex();
    }
        
    
    public static Tuple<uint, ulong> BasisBladeGradeIndex(ulong id)
    {
        return new Tuple<uint, ulong>(
            (uint) BitOperations.PopCount(id), 
            id.CombinadicPatternToIndex()
        );
    }

    
    public static ulong BasisBladeId(uint grade, ulong index)
    {
        return index.IndexToCombinadicPattern((int) grade);
    }
    
    /// <summary>
    /// Never remove this code for future validation and reference
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    public static int CountEGpSwapsRefImp(this ulong id1, ulong id2)
    {
        if (id1 == 0UL || id2 == 0UL) return 0;

        var swapCount = 0;
        var id = id1;

        //Find the largest 1-bit of ID1 and create a bit mask
        var initMask1 = 1UL;
        while (initMask1 <= id1)
            initMask1 <<= 1;

        initMask1 >>= 1;

        var mask2 = 1UL;
        while (mask2 <= id2)
        {
            //If the current bit in ID2 is one:
            if ((id2 & mask2) != 0)
            {
                //Count number of swaps, each new swap inverts the final sign
                var mask1 = initMask1;

                while (mask1 > mask2)
                {
                    if ((id & mask1) != 0)
                        swapCount++;

                    mask1 >>= 1;
                }
            }

            //Invert the corresponding bit in ID1
            id ^= mask2;

            mask2 <<= 1;
        }

        return swapCount;
    }

    /// <summary>
    /// Merge two sorted arrays, exclude common integers, counts swaps,
    /// and store common integers in a separate array.
    /// </summary>
    public static int CountEGpSwaps(this ulong indexSet1, ulong indexSet2)
    {
        if (indexSet1 == 0UL || indexSet2 == 0L)
            return 0;

        var swapCount = 0;

        var count1 = BitOperations.PopCount(indexSet1);
        var count2 = BitOperations.PopCount(indexSet2);

        var index1 = 0;
        var index2 = 0;

        var item1 = BitOperations.TrailingZeroCount(indexSet1);
        indexSet1 &= indexSet1 - 1; // Clear the least significant bit

        var item2 = BitOperations.TrailingZeroCount(indexSet2);
        indexSet2 &= indexSet2 - 1; // Clear the least significant bit

        while (true)
        {
            if (item1 < item2) // Take value from first array, no swaps needed
            {
                index1++;
                if (index1 >= count1) break;
                item1 = BitOperations.TrailingZeroCount(indexSet1);
                indexSet1 &= indexSet1 - 1;
            }
            else if (item1 > item2) // Take value from second array, do swaps
            {
                swapCount += count1 - index1;

                index2++;
                if (index2 >= count2) break;
                item2 = BitOperations.TrailingZeroCount(indexSet2);
                indexSet2 &= indexSet2 - 1;
            }
            else // Found common integer, do swaps
            {
                index1++;
                if (index1 >= count1) break;
                item1 = BitOperations.TrailingZeroCount(indexSet1);
                indexSet1 &= indexSet1 - 1;

                swapCount += count1 - index1;

                index2++;
                if (index2 >= count2) break;
                item2 = BitOperations.TrailingZeroCount(indexSet2);
                indexSet2 &= indexSet2 - 1;
            }
        }

        return swapCount;
    }

    /// <summary>
    /// Compute if the Euclidean Geometric Product of two basis blades is -1.
    /// This method is slower than lookup, but can be used for GAs with dimension
    /// less than 64
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    
    public static bool EGpIsPositive(ulong id1, ulong id2)
    {
        return CountEGpSwaps(id1, id2).IsEven();
    }

    /// <summary>
    /// Compute if the Euclidean Geometric Product of two basis blades is -1.
    /// This method is slower than lookup, but can be used for GAs with dimension
    /// less than 64
    /// </summary>
    /// <param name="id1"></param>
    /// <param name="id2"></param>
    /// <returns></returns>
    
    public static bool EGpIsNegative(ulong id1, ulong id2)
    {
        return CountEGpSwaps(id1, id2).IsOdd();

    }

    
    public static IntegerSign EGpSign(ulong id1, ulong id2)
    {
        return CountEGpSwaps(id1, id2).IsEven()
            ? IntegerSign.Positive
            : IntegerSign.Negative;
    }

    
    public static IntegerSign EGpReverseSign(ulong id1, ulong id2)
    {
        var egpSign = EGpSign(id1, id2);

        return ((uint) BitOperations.PopCount(id2)).ReverseIsNegativeOfGrade()
            ? -egpSign 
            : egpSign;
    }
}