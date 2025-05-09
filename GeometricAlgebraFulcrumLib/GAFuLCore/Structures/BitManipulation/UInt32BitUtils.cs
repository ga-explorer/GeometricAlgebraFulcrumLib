using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using GAFuLCore.Structures;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

/// <summary>
/// See here for reference:
/// https://graphics.stanford.edu/~seander/bithacks.html
/// </summary>
public static class UInt32BitUtils
{
    public static class Naive
    {
        public static int FirstOneBitPosition(uint bitPattern)
        {
            var bitPosition = 0;

            while (bitPattern > 0U)
            {
                if ((bitPattern & 1U) == 1U)
                    return bitPosition;

                bitPosition++;
                bitPattern >>= 1;
            }

            return -1;
        }

        public static int LastOneBitPosition(uint bitPattern)
        {
            var bitPosition = 0;

            var lastOneBitPos = -1;

            while (bitPattern > 0U)
            {
                if ((bitPattern & 1U) == 1U)
                    lastOneBitPos = bitPosition;

                bitPosition++;
                bitPattern >>= 1;
            }

            return lastOneBitPos;
        }

        public static uint PatternToMask(uint bitPattern)
        {
            var bitsMask = 0U;
            var bitPosition = 0;

            while (bitPattern > 0U)
            {
                bitsMask |= (1U << bitPosition);

                bitPosition++;
                bitPattern >>= 1;
            }

            return bitsMask;
        }
    }


    /// <summary>
    /// The minimum possible value for a bit position
    /// </summary>
    public static int MinBitPosition => 0;

    /// <summary>
    /// The maximum possible value for a bit position
    /// </summary>
    public static int MaxBitPosition => 31;

    /// <summary>
    /// The largest bit pattern that can be handled
    /// </summary>
    public static int MaxBitPatternSize => 32;


    private static readonly uint[] Log2CeilingArray =
    [
        0xFFFF0000U,
        0x0000FF00U,
        0x000000F0U,
        0x0000000CU,
        0x00000002U
    ];

    /// <summary>
    /// https://stackoverflow.com/questions/3272424/compute-fast-log-base-2-ceiling
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static int Log2Ceiling(this uint x)
    {
        var y = (((x & (x - 1U)) == 0U) ? 0 : 1);
        var j = 32;

        for (var i = 0; i < 5; i++) 
        {
            var k = (((x & Log2CeilingArray[i]) == 0) ? 0 : j);

            y += k;
            x >>= k;
            j >>= 1;
        }

        return y;
    }

    /// <summary>
    /// Tests if bitPattern is an odd integer
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(this uint bitPattern)
    {
        return (bitPattern & 1U) != 0U;
    }

    /// <summary>
    /// Tests if bitPattern is an even integer
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEven(this uint bitPattern)
    {
        return (bitPattern & 1U) == 0U;
    }
        
    /// <summary>
    /// Generate some patterns containing 1, 2, and 3 bits to test functions
    /// of this class
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<uint> GetUInt32TestPatterns()
    {
        for (var i = 0; i < MaxBitPatternSize; i++)
        {
            var pattern1 = 1U << i;

            for (var j = 0; j < MaxBitPatternSize; j++)
            {
                var pattern2 = 1U << j;

                for (var k = 0; k < MaxBitPatternSize; k++)
                {
                    var pattern3 = 1U << k;

                    yield return (pattern1 | pattern2 | pattern3);
                }
            }
        }
    }

    /// <summary>
    /// Returns true if this is a basic pattern (i.e. an integer between 1U and 2^30 that is a power of 2)
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBasicPattern(this uint bitPattern)
    {
        return bitPattern != 0U && (bitPattern & (bitPattern - 1U)) == 0U;
    }

    /// <summary>
    /// Returns true if this is a zero or basic pattern (i.e. an integer between 0U and 2^30 that is a power of 2)
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroOrBasicPattern(this uint bitPattern)
    {
        return (bitPattern & (bitPattern - 1U)) == 0U;
    }

    /// <summary>
    /// Tests if the bit at the given position is a one
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitPosition"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOneAt(this uint bitPattern, int bitPosition)
    {
        return ((1U << bitPosition) & bitPattern) != 0U;
    }

    /// <summary>
    /// Tests if the bit at the given position is a zero
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitPosition"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroAt(this uint bitPattern, int bitPosition)
    {
        return ((1U << bitPosition) & bitPattern) == 0U;
    }

        
    /// <summary>
    /// Count the number of ones in the given bit pattern
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CountOnes(this uint bitPattern)
    {
        return BitOperations.PopCount(bitPattern);

        //if (bitPattern == 0)
        //    return 0;

        //var count = 0;

        ////if (bitPattern > int.MaxValue)
        ////{
        ////    bitPattern &= int.MaxValue;
        ////    count = 1;
        ////}

        //// Brian Kernighan’s Algorithm. See here for more details:
        //// https://www.geeksforgeeks.org/count-set-bits-in-an-integer/
        //// Subtracting 1 from a decimal number flips all the bits after
        //// the rightmost set bit (which is 1) including the rightmost set bit.
        //// So if we subtract a number by 1 and do it bitwise & with itself
        //// (n & (n-1)), we unset the rightmost set bit. If we do n & (n-1)
        //// in a loop and count the number of times the loop executes, we get
        //// the set bit count. The beauty of this solution is the number of
        //// times it loops is equal to the number of set bits in a given integer. 

        //var n = bitPattern;
            
        //while (n > 0)
        //{
        //    n &= (n - 1);

        //    count++;
        //}

        //return count;

        ////var onesCount = 0;

        ////while (bitPattern > 0U)
        ////{
        ////    bitPattern &= bitPattern - 1U; // clear the least significant bit set
        ////    onesCount++;
        ////}

        ////return onesCount;
    }

    /// <summary>
    /// Returns the bit position of the first one bit in the given pattern.
    /// If the pattern is zero this returns -1.
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int FirstOneBitPosition(this uint bitPattern)
    {
        return bitPattern == 0 
            ? -1 
            : BitOperations.TrailingZeroCount(bitPattern);

        //if (bitPattern == 0)
        //    return -1;

        //// Found ones in positions 0-31
        //if ((bitPattern & 65535U) != 0)
        //{
        //    // Found ones in positions 0-15
        //    if ((bitPattern & 255U) != 0)
        //    {
        //        // Found ones in positions 0-7
        //        if ((bitPattern & 15U) != 0)
        //        {
        //            // Found ones in positions 0-3
        //            if ((bitPattern & 3U) != 0)
        //            {
        //                // Found ones in positions 0-1
        //                return (bitPattern & 1U) != 0 ? 0 : 1;
        //            }

        //            // Found ones in positions 2-3
        //            return (bitPattern & 4U) != 0 ? 2 : 3;
        //        }

        //        // Found ones in positions 4-7
        //        if ((bitPattern & 48U) != 0)
        //        {
        //            // Found ones in positions 4-5
        //            return (bitPattern & 16U) != 0 ? 4 : 5;
        //        }

        //        // Found ones in positions 6-7
        //        return (bitPattern & 64U) != 0 ? 6 : 7;
        //    }

        //    // Found ones in positions 8-15
        //    if ((bitPattern & 3840U) != 0)
        //    {
        //        // Found ones in positions 8-11
        //        if ((bitPattern & 768U) != 0)
        //        {
        //            // Found ones in positions 8-9
        //            return (bitPattern & 256U) != 0 ? 8 : 9;
        //        }

        //        // Found ones in positions 10-11
        //        return (bitPattern & 1024U) != 0 ? 10 : 11;
        //    }

        //    // Found ones in positions 12-15
        //    if ((bitPattern & 12288U) != 0)
        //    {
        //        // Found ones in positions 12-13
        //        return (bitPattern & 4096U) != 0 ? 12 : 13;
        //    }

        //    // Found ones in positions 14-15
        //    return (bitPattern & 16384U) != 0 ? 14 : 15;
        //}

        //// Found ones in positions 16-31
        //if ((bitPattern & 16711680U) != 0)
        //{
        //    // Found ones in positions 16-23
        //    if ((bitPattern & 983040U) != 0)
        //    {
        //        // Found ones in positions 16-19
        //        if ((bitPattern & 196608U) != 0)
        //        {
        //            // Found ones in positions 16-17
        //            return (bitPattern & 65536U) != 0 ? 16 : 17;
        //        }

        //        // Found ones in positions 18-19
        //        return (bitPattern & 262144U) != 0 ? 18 : 19;
        //    }

        //    // Found ones in positions 20-23
        //    if ((bitPattern & 3145728U) != 0)
        //    {
        //        // Found ones in positions 20-21
        //        return (bitPattern & 1048576U) != 0 ? 20 : 21;
        //    }

        //    // Found ones in positions 22-23
        //    return (bitPattern & 4194304U) != 0 ? 22 : 23;
        //}

        //// Found ones in positions 24-31
        //if ((bitPattern & 251658240U) != 0)
        //{
        //    // Found ones in positions 24-27
        //    if ((bitPattern & 50331648U) != 0)
        //    {
        //        // Found ones in positions 24-25
        //        return (bitPattern & 16777216U) != 0 ? 24 : 25;
        //    }

        //    // Found ones in positions 26-27
        //    return (bitPattern & 67108864U) != 0 ? 26 : 27;
        //}

        //// Found ones in positions 28-31
        //if ((bitPattern & 805306368U) != 0)
        //{
        //    // Found ones in positions 28-29
        //    return (bitPattern & 268435456U) != 0 ? 28 : 29;
        //}

        //// Found ones in positions 30-31
        //return (bitPattern & 1073741824U) != 0 ? 30 : 31;

        ////var bitPosition = 0;

        ////while (bitPattern > 0U)
        ////{
        ////    if ((bitPattern & 1U) == 1U)
        ////        return bitPosition;

        ////    bitPosition++;
        ////    bitPattern >>= 1;
        ////}

        ////return -1;
    }

    /// <summary>
    /// Returns the bit position of the last one bit in the given pattern. If the pattern is zero this
    /// returns -1U.
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastOneBitPosition(this uint bitPattern)
    {
        return bitPattern == 0 
            ? -1 
            : 31 - BitOperations.LeadingZeroCount(bitPattern);

        //if (bitPattern == 0U) 
        //    return -1;

        //if ((bitPattern & 4294901760U) != 0)
        //{
        //    // Found ones in positions 16-31
        //    if ((bitPattern & 4278190080U) != 0)
        //    {
        //        // Found ones in positions 24-31
        //        if ((bitPattern & 4026531840U) != 0)
        //        {
        //            // Found ones in positions 28-31
        //            if ((bitPattern & 3221225472U) != 0)
        //            {
        //                // Found ones in positions 30-31
        //                return (bitPattern & 2147483648U) != 0 ? 31 : 30;
        //            }

        //            // Found ones in positions 28-29
        //            return (bitPattern & 536870912U) != 0 ? 29 : 28;
        //        }

        //        // Found ones in positions 24-27
        //        if ((bitPattern & 201326592U) != 0)
        //        {
        //            // Found ones in positions 26-27
        //            return (bitPattern & 134217728U) != 0 ? 27 : 26;
        //        }

        //        // Found ones in positions 24-25
        //        return (bitPattern & 33554432U) != 0 ? 25 : 24;
        //    }

        //    // Found ones in positions 16-23
        //    if ((bitPattern & 15728640U) != 0)
        //    {
        //        // Found ones in positions 20-23
        //        if ((bitPattern & 12582912U) != 0)
        //        {
        //            // Found ones in positions 22-23
        //            return (bitPattern & 8388608U) != 0 ? 23 : 22;
        //        }

        //        // Found ones in positions 20-21
        //        return (bitPattern & 2097152U) != 0 ? 21 : 20;
        //    }

        //    // Found ones in positions 16-19
        //    if ((bitPattern & 786432U) != 0)
        //    {
        //        // Found ones in positions 18-19
        //        return (bitPattern & 524288U) != 0 ? 19 : 18;
        //    }

        //    // Found ones in positions 16-17
        //    return (bitPattern & 131072U) != 0 ? 17 : 16;
        //}

        //// Found ones in positions 0-15
        //if ((bitPattern & 65280U) != 0)
        //{
        //    // Found ones in positions 8-15
        //    if ((bitPattern & 61440U) != 0)
        //    {
        //        // Found ones in positions 12-15
        //        if ((bitPattern & 49152U) != 0)
        //        {
        //            // Found ones in positions 14-15
        //            return (bitPattern & 32768U) != 0 ? 15 : 14;
        //        }

        //        // Found ones in positions 12-13
        //        return (bitPattern & 8192U) != 0 ? 13 : 12;
        //    }

        //    // Found ones in positions 8-11
        //    if ((bitPattern & 3072U) != 0)
        //    {
        //        // Found ones in positions 10-11
        //        return (bitPattern & 2048U) != 0 ? 11 : 10;
        //    }

        //    // Found ones in positions 8-9
        //    return (bitPattern & 512U) != 0 ? 9 : 8;
        //}

        //// Found ones in positions 0-7
        //if ((bitPattern & 240U) != 0)
        //{
        //    // Found ones in positions 4-7
        //    if ((bitPattern & 192U) != 0)
        //    {
        //        // Found ones in positions 6-7
        //        return (bitPattern & 128U) != 0 ? 7 : 6;
        //    }

        //    // Found ones in positions 4-5
        //    return (bitPattern & 32U) != 0 ? 5 : 4;
        //}

        //// Found ones in positions 0-3
        //if ((bitPattern & 12U) != 0)
        //{
        //    // Found ones in positions 2-3
        //    return (bitPattern & 8U) != 0 ? 3 : 2;
        //}

        //// Found ones in positions 0-1
        //return (bitPattern & 2U) != 0 ? 1 : 0;

        ////var bitPosition = 0;

        ////var lastOneBitPos = -1;

        ////while (bitPattern > 0U)
        ////{
        ////    if ((bitPattern & 1U) == 1U)
        ////        lastOneBitPos = bitPosition;

        ////    bitPosition++;
        ////    bitPattern >>= 1;
        ////}

        ////return lastOneBitPos;
    }

    /// <summary>
    /// Returns the largest integer that is a power of 2 (i.e. 1U, 2, 4, 8, 16, etc.)
    /// that is smaller or equal to the given integer. If the given integer is 0
    /// this returns 0.
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Power2LowerLimit(this uint bitPattern)
    {
        if (bitPattern == 0U) 
            return 0U;

        if ((bitPattern >> 31) != 0)
            return 1U << 31;

        var n = bitPattern;

        n--;
        n |= n >> 1;
        n |= n >> 2;
        n |= n >> 4;
        n |= n >> 8;
        n |= n >> 16;
        n++;

        return n == bitPattern ? n : (n >> 1);
    }

    /// <summary>
    /// Returns the smallest integer that is a power of 2 (i.e. 1U, 2, 4, 8, 16, etc.)
    /// that is larger or equal to the given integer. If the given integer is 0U
    /// this returns 0.
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Power2UpperLimit(this uint bitPattern)
    {
        if (bitPattern == 0)
            return 0;

        bitPattern--;
        bitPattern |= bitPattern >> 1;
        bitPattern |= bitPattern >> 2;
        bitPattern |= bitPattern >> 4;
        bitPattern |= bitPattern >> 8;
        bitPattern |= bitPattern >> 16;
        bitPattern++;

        return bitPattern;
    }

    /// <summary>
    /// Create a fUl mask that contains the given pattern
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint PatternToMask(this uint bitPattern)
    {
        if (bitPattern == 0)
            return 0;

        var bitsMask = bitPattern.Power2LowerLimit();

        return bitsMask | (bitsMask - 1);
    }
        

    /// <summary>
    /// Sets the bit at the given position to zero
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitPosition"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint SetBitToZeroAt(this uint bitPattern, int bitPosition)
    {
        return bitPattern & ~(1U << bitPosition);
    }

    /// <summary>
    /// Sets the bits at the given positions to zero
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitPositions"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint SetBitsToZeroAt(this uint bitPattern, IEnumerable<int> bitPositions)
    {
        var bitMask = bitPositions.Aggregate(
            0U,
            (currentBitPattern, bitPosition) => currentBitPattern | (1U << bitPosition)
        );

        return bitPattern & ~bitMask;
    }

    /// <summary>
    /// Sets the bits at the given positions to zero
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitPositions"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint SetBitsToZeroAt(this uint bitPattern, params int[] bitPositions)
    {
        var bitMask = bitPositions.Aggregate(
            0U,
            (currentBitPattern, bitPosition) => currentBitPattern | (1U << bitPosition)
        );

        return bitPattern & ~bitMask;
    }

    /// <summary>
    /// Sets the bit at the given position to one
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitPosition"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint SetBitToOneAt(this uint bitPattern, int bitPosition)
    {
        return bitPattern | (1U << bitPosition);
    }

    /// <summary>
    /// Sets the bits at the given positions to one
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitPositions"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint SetBitsToOneAt(this uint bitPattern, IEnumerable<int> bitPositions)
    {
        var bitMask = bitPositions.Aggregate(
            0U,
            (currentBitPattern, bitPosition) => currentBitPattern | (1U << bitPosition)
        );

        return bitPattern | bitMask;
    }

    /// <summary>
    /// Sets the bits at the given positions to one
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitPositions"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint SetBitsToOneAt(this uint bitPattern, params int[] bitPositions)
    {
        var bitMask = bitPositions.Aggregate(
            0U,
            (currentBitPattern, bitPosition) => currentBitPattern | (1U << bitPosition)
        );

        return bitPattern | bitMask;
    }

    /// <summary>
    /// Sets a bit at the given position using a boolean value
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitPosition"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint SetBitAt(this uint bitPattern, int bitPosition, bool value)
    {
        return 
            value
                ? bitPattern | (1U << bitPosition) 
                : bitPattern & ~(1U << bitPosition);
    }

    /// <summary>
    /// Inverts the value of the bit at the given position
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitPosition"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint InvertBitAt(this uint bitPattern, int bitPosition)
    {
        return bitPattern ^ (1U << bitPosition);
    }

    /// <summary>
    /// Inverts the bits at the given positions
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitPositions"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint InvertBitsAt(this uint bitPattern, IEnumerable<int> bitPositions)
    {
        return 
            bitPositions.Aggregate(
                bitPattern, 
                (current, bitPosition) => current ^ (1U << bitPosition)
            );
    }

    /// <summary>
    /// Inverts the bits at the given positions
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitPositions"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint InvertBitsAt(this uint bitPattern, params int[] bitPositions)
    {
        return
            bitPositions.Aggregate(
                bitPattern,
                (current, bitPosition) => current ^ (1U << bitPosition)
            );
    }


    /// <summary>
    /// Returns true or false depending on the value of the bit at the given position being 1U or 0U.
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitPosition"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BitToBoolean(this uint bitPattern, int bitPosition)
    {
        return ((1U << bitPosition) & bitPattern) != 0U;
    }

    /// <summary>
    /// Reverse the order of bits in the given bit pattern
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitsCount"></param>
    /// <returns></returns>
    public static uint ReverseBits(this uint bitPattern, int bitsCount)
    {
        Debug.Assert(
            bitsCount > 0 && 
            bitPattern < (1U << (bitsCount + 1))
        );

        var resUt = 0U;

        var i = bitsCount - 1;
        while (bitPattern != 0U)
        {
            if ((bitPattern & 1U) != 0U)
                resUt |= (1U << i);

            i--;
            bitPattern >>= 1;
        }

        return resUt;
    }


    /// <summary>
    /// Create a mask containing a number of ones as given. 
    /// For example CreateFUlMask(3) returns 111 (i.e. 7 decimal)
    /// </summary>
    /// <param name="bitsCount"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint CreateMask(int bitsCount)
    {
        return (1U << bitsCount) - 1U;
    }

    /// <summary>
    /// Create a mask where all positions between the given are set to 1
    /// </summary>
    /// <param name="bitPosition1"></param>
    /// <param name="bitPosition2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint CreateMask(int bitPosition1, int bitPosition2)
    {
        var bitsCount = bitPosition2 - bitPosition1;

        return ((1U << bitsCount) - 1U) << bitPosition1;
    }



    /// <summary>
    /// Given two bit patterns and a bit size, this shifts the first pattern by size bits 
    /// to the left and appends the second to combine the two patterns using an OR bitwise operation.
    /// For example 1010.AppendPattern(5, 0001) gives 1010 0U 0001
    /// </summary>
    /// <param name="bitPattern1"></param>
    /// <param name="bitPattern2"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint AppendPattern(this uint bitPattern1, int size, uint bitPattern2)
    {
        return bitPattern2 | (bitPattern1 << size);
    }

    /// <summary>
    /// This selects the bits from pattern1 where bitMask has a zero and bits from pattern2 where 
    /// bitMask has a one into a single pattern.
    /// </summary>
    /// <param name="bitPattern1"></param>
    /// <param name="bitMask"></param>
    /// <param name="bitPattern2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint MergeWithPattern(this uint bitPattern1, uint bitMask, uint bitPattern2)
    {
        return bitPattern1 ^ ((bitPattern1 ^ bitPattern2) & bitMask);
    }


    /// <summary>
    /// Gets the numbers between two patterns.
    /// </summary>
    /// <param name="firstPattern"></param>
    /// <param name="secondPattern"></param>
    /// <returns></returns>
    public static IEnumerable<uint> PatternsBetween(uint firstPattern, uint secondPattern)
    {
        if (firstPattern <= secondPattern)
        {
            for (var bitPattern = firstPattern; bitPattern <= secondPattern; bitPattern++)
                yield return bitPattern;
        }
        else
        {
            for (var bitPattern = firstPattern; bitPattern >= secondPattern; bitPattern--)
                yield return bitPattern;
        }
    }

    /// <summary>
    /// Suppose we have a pattern of N bits set to 1U in an integer and we want the next permutation 
    /// of N (1U bits) in a lexicographical sense. For example, if N is 3 and the bit pattern is 00010011, 
    /// the next patterns woUd be 00010101, 00010110, 00011001,00011010, 00011100, 00100011, and so forth.
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint NextPermutation(this uint bitPattern)
    {
        var tempPattern = (bitPattern | (bitPattern - 1U)) + 1U;
        var tempPattern1 = tempPattern & (uint)(-tempPattern);
        var tempPattern2 = bitPattern & (uint)(-bitPattern);

        return tempPattern | (((tempPattern1 / tempPattern2) >> 1) - 1U);
    }

    /// <summary>
    /// Constructs all distinct patterns of fixed size containing a given number of ones
    /// </summary>
    /// <param name="patternSize"></param>
    /// <param name="onesCount"></param>
    /// <returns></returns>
    public static IEnumerable<uint> OnesPermutations(int patternSize, int onesCount)
    {
        var startPattern = CreateMask(onesCount);

        if (patternSize <= onesCount)
        {
            yield return startPattern;
            yield break;
        }

        var bitMask = CreateMask(patternSize);

        while (startPattern <= bitMask)
        {
            yield return startPattern;

            startPattern = NextPermutation(startPattern);
        }
    }

    /// <summary>
    /// Constructs all distinct patterns of fixed size containing a given number of zeros
    /// </summary>
    /// <param name="patternSize"></param>
    /// <param name="zerosCount"></param>
    /// <returns></returns>
    public static IEnumerable<uint> ZerosPermutations(int patternSize, int zerosCount)
    {
        var startPattern = CreateMask(zerosCount);

        if (patternSize <= zerosCount)
        {
            yield return 0U;
            yield break;
        }

        var bitMask = CreateMask(patternSize);

        while (startPattern <= bitMask)
        {
            yield return (bitMask & ~startPattern);

            startPattern = NextPermutation(startPattern);
        }
    }


    /// <summary>
    /// Converts the given pattern into a list of boolean values
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    public static IEnumerable<bool> PatternToBooleans(this uint bitPattern)
    {
        while (bitPattern > 0U)
        {
            yield return ((bitPattern & 1U) != 0U);

            bitPattern >>= 1;
        }
    }

    /// <summary>
    /// Converts the given pattern into a list of boolean values with a given number of items
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitsCount"></param>
    /// <returns></returns>
    public static IEnumerable<bool> PatternToBooleans(this uint bitPattern, int bitsCount)
    {
        while (bitPattern > 0U && bitsCount > 0)
        {
            yield return ((bitPattern & 1U) != 0U);

            bitPattern >>= 1;

            bitsCount--;
        }

        while (bitsCount > 0)
        {
            yield return false;

            bitsCount--;
        }
    }

    /// <summary>
    /// Returns a list of bit positions where ones are present in the given bit pattern
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    public static IEnumerable<int> PatternToPositions(this uint bitPattern)
    {
        var bitPosition = 0;

        while (bitPattern > 0U)
        {
            if ((bitPattern & 1U) != 0U)
                yield return bitPosition;

            bitPosition++;
            bitPattern >>= 1;
        }
    }

    /// <summary>
    /// Converts a bit pattern into a sequence with a given size where each item in the sequence is either 
    /// zeroItem or oneItem depending on the corresponding bit in the bit pattern.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bitPattern"></param>
    /// <param name="bitsCount"></param>
    /// <param name="zeroItem"></param>
    /// <param name="oneItem"></param>
    /// <returns></returns>
    public static IEnumerable<T> PatternToSequence<T>(this uint bitPattern, int bitsCount, T zeroItem, T oneItem)
    {
        while (bitPattern > 0U && bitsCount > 0)
        {
            yield return ((bitPattern & 1U) != 0U) ? oneItem : zeroItem;

            bitPattern >>= 1;

            bitsCount--;
        }

        while (bitsCount > 0)
        {
            yield return zeroItem;

            bitsCount--;
        }
    }

    /// <summary>
    /// Converts a bit pattern into a sequence where each item in the sequence is either 
    /// zeroItem or oneItem depending on the corresponding bit in the bit pattern.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bitPattern"></param>
    /// <param name="zeroItem"></param>
    /// <param name="oneItem"></param>
    /// <returns></returns>
    public static IEnumerable<T> PatternToSequence<T>(this uint bitPattern, T zeroItem, T oneItem)
    {
        while (bitPattern > 0U)
        {
            yield return ((bitPattern & 1U) != 0U) ? oneItem : zeroItem;

            bitPattern >>= 1;
        }
    }

    /// <summary>
    /// Converts the given bit pattern into a string of '1U' and '0U' characters with the MSB at the 
    /// first character and the LSB at the last character
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    public static string PatternToString(this uint bitPattern)
    {
        if (bitPattern == 0U) 
            return "0";

        var charsArray = 
            PatternToSequence(bitPattern, '0', '1').ToArray();

        var s = new StringBuilder(charsArray.Length);

        for (var i = charsArray.Length - 1; i >= 0; i--)
            s.Append(charsArray[i]);

        return s.ToString();

        //if (bitsCount is < 1 or > 64)
        //    throw new ArgumentException(nameof(bitsCount));

        //var lsb = Convert.ToString(
        //    (uint)(pattern & 4294967295U),
        //    2
        //).PadLeft(32, '0');

        //var msb = Convert.ToString(
        //    (uint)(pattern >> 32),
        //    2
        //).PadLeft(32, '0');

        //return (msb + lsb).Substring(64 - bitsCount);
    }

    ///// <summary>
    ///// Converts the given bit pattern into a string of '1U' and '0U' characters with the MSB at the 
    ///// first character and the LSB at the last character
    ///// </summary>
    ///// <param name="bitPattern"></param>
    ///// <param name="bitsCount"></param>
    ///// <returns></returns>
    //public static string PatternToString(this uint bitPattern, int bitsCount)
    //{
    //    return Convert.ToString(bitPattern, 2).PadLeft(bitsCount, '0');

    //    //var charsArray = PatternToSequence(bitPattern, bitsCount, '0U', '1U').ToArray();

    //    //var s = new StringBuilder(charsArray.Length);

    //    //for (var i = charsArray.Length - 1U; i >= 0U; i--)
    //    //    s.Append(charsArray[i]);

    //    //return s.ToString();
    //}

    /// <summary>
    /// Converts the given bit pattern into a string of '1U' and '0U' characters with the MSB at the 
    /// first character and the LSB at the last character
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitsCount"></param>
    /// <returns></returns>
    public static string PatternToString(this uint bitPattern, int bitsCount)
    {
        var s = new StringBuilder(bitsCount);

        for (var i = bitsCount - 1; i >= 0; i--)
            s.Append((bitPattern & (1U << i)) == 0U ? '0' : '1');

        return s.ToString();
    }

    public static string PatternToStringPadRight(this uint bitPattern, int bitsCount, int leftBitsCount, char paddingChar = '-')
    {
        var s = bitPattern.PatternToString(bitsCount);

        return s.Substring(0, bitsCount - leftBitsCount).PadRight(bitsCount, paddingChar);
    }

    /// <summary>
    /// Picks elements of the given sequence based on the given pattern.
    /// For example new [] {"a", "b", "c", "d"}.PickUsingPattern(9) gives the sequence {"a", "d"}.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> PickItemsUsingPattern<T>(this IEnumerable<T> items, uint bitPattern)
    {
        var bitPosition = 0;

        return
            items
                .Take(MaxBitPosition)
                .Where(item => ((1U << (bitPosition++)) & bitPattern) != 0U);
    }

    /// <summary>
    /// Picks elements of the given sequence based on the given pattern.
    /// For example new [] {"a", "b", "c", "d"}.PickUsingPattern(9) gives the sequence {"a", "d"}.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<KeyValuePair<uint, T>> PickIndexedItemsUsingPattern<T>(this IEnumerable<T> items, uint bitPattern)
    {
        var bitPosition = 0;

        return
            items
                .Take(MaxBitPosition)
                .Select((item, index) => new KeyValuePair<uint, T>((uint)index, item))
                .Where(item => ((1U << (bitPosition++)) & bitPattern) != 0U);
    }


    /// <summary>
    /// Converts a sequence of boolean values into a bit pattern. The first item in the sequence
    /// corresponds to a 1U or 0U in the LSB of the pattern and so on.
    /// </summary>
    /// <param name="boolsSeq"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint BooleansToPattern(this IEnumerable<bool> boolsSeq)
    {
        var bitPosition = 0;

        return
            boolsSeq
                .Take(MaxBitPosition)
                .Aggregate(
                    0U,
                    (currentBitPattern, item) =>
                    {
                        var newPattern = 
                            item 
                                ? ((1U << bitPosition) | currentBitPattern) 
                                : currentBitPattern;

                        bitPosition++;

                        return newPattern;
                    }
                );
    }

    /// <summary>
    /// Converts a sequence of boolean values into a bit pattern. The first item in the sequence
    /// corresponds to a 1U or 0U in the LSB of the pattern and so on.
    /// </summary>
    /// <param name="boolsSeq"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint BooleansToPattern(params bool[] boolsSeq)
    {
        var bitPosition = 0;

        return
            boolsSeq
                .Take(MaxBitPosition)
                .Aggregate(
                    0U,
                    (currentBitPattern, item) =>
                    {
                        var newPattern =
                            item
                                ? ((1U << bitPosition) | currentBitPattern)
                                : currentBitPattern;

                        bitPosition++;

                        return newPattern;
                    }
                );
    }

    /// <summary>
    /// Returns a bit pattern containing ones at the positions given in a list of bit positions
    /// </summary>
    /// <param name="bitPositions"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint PositionsToPattern(this IEnumerable<int> bitPositions)
    {
        return 
            bitPositions.Aggregate(
                0U, 
                (currentBitPattern, bitPosition) => currentBitPattern | (1U << bitPosition)
            );
    }

    /// <summary>
    /// Returns a bit pattern containing ones at the positions given in a list of bit positions
    /// </summary>
    /// <param name="bitPositions"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint PositionsToPattern(params int[] bitPositions)
    {
        return
            bitPositions.Aggregate(
                0U,
                (currentBitPattern, bitPosition) => currentBitPattern | (1U << bitPosition)
            );
    }

    /// <summary>
    /// Converts the given string into a bit pattern by putting a 1U at the positions corresponding to
    /// where the '1U' character is found in the string. The MSB in the pattern is the first character.
    /// </summary>
    /// <param name="binaryString"></param>
    /// <returns></returns>
    public static uint StringToUnsignedLongPattern(this string binaryString)
    {
        if (binaryString.Length > MaxBitPatternSize)
            throw new InvalidOperationException();

        var id = 0U;

        foreach (var c in binaryString)
        {
            id <<= 1;

            if (c == '1') 
                id |= 1U;
        }

        return id;
    }

    /// <summary>
    /// Converts the given string into a bit pattern by putting a 1U at the positions corresponding to
    /// where the oneChar character is found in the string. The MSB in the pattern is the first character
    /// of the string while the LSB is the last character.
    /// </summary>
    /// <param name="binaryString"></param>
    /// <param name="oneChar"></param>
    /// <returns></returns>
    public static uint StringToPattern(this string binaryString, char oneChar)
    {
        if (binaryString.Length > MaxBitPatternSize)
            throw new InvalidOperationException();

        var id = 0U;

        foreach (var c in binaryString)
        {
            id <<= 1;

            if (c == oneChar) 
                id |= 1U;
        }

        return id;
    }


    /// <summary>
    /// Returns true if the given bit pattern is a sub pattern of the other input.
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="superPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSubPatternOf(this uint bitPattern, uint superPattern)
    {
        return (superPattern | bitPattern) == superPattern;
    }

    /// <summary>
    /// Returns true if the given bit pattern is a proper sub pattern of the other input.
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="superPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsProperSubPatternOf(this uint bitPattern, uint superPattern)
    {
        return
            bitPattern != 0U &&
            bitPattern != superPattern &&
            (superPattern | bitPattern) == superPattern;
    }

    /// <summary>
    /// Returns true if the given bit pattern is a super pattern of the other input.
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="subPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSuperPatternOf(this uint bitPattern, uint subPattern)
    {
        return (subPattern | bitPattern) == bitPattern;
    }

    /// <summary>
    /// Returns true if the given bit pattern is a proper super pattern of the other input.
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="subPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsProperSuperPatternOf(this uint bitPattern, uint subPattern)
    {
        return
            subPattern != 0U &&
            bitPattern != subPattern &&
            (subPattern | bitPattern) == bitPattern;
    }

    /// <summary>
    /// Split the given pattern into its basic patterns each containing a single 1U.
    /// For example GetBasicPatterns(14) gives {2, 4, 8}
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    public static IEnumerable<uint> GetBasicPatterns(this uint bitPattern)
    {
        var bitPosition = 0;

        while (bitPattern > 0U)
        {
            if ((bitPattern & 1U) != 0U)
                yield return 1U << bitPosition;

            bitPosition++;
            bitPattern >>= 1;
        }
    }

    /// <summary>
    /// Split the given pattern into its sub patterns.
    /// For example GetSubPatterns(11) gives {0U, 1U, 2, 3, 8, 9, 10, 11} 
    /// (i.e. the sub-patterns of 1011 are 0000, 0001, 0010, 0011, 1000, 1001, 1010, and 1011)
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    public static IEnumerable<uint> GetSubPatterns(this uint bitPattern)
    {
        //The zero pattern is always a sub-pattern of any bit pattern
        yield return 0U;

        if (bitPattern == 0U)
            yield break;

        //Find proper sub patterns that are not zero and nor equal to the original pattern
        var bitPositions = PatternToPositions(bitPattern).ToArray();
        var count = (1U << bitPositions.Length) - 1U;

        for (var p = 1U; p < count; p++)
            yield return PositionsToPattern(
                bitPositions.PickItemsUsingPattern(p)
            );

        //The original pattern is a sub-pattern of itself
        yield return bitPattern;
    }

    /// <summary>
    /// Split the given pattern into its proper sub-patterns.
    /// For example GetProperSubPatterns(11) gives {1U, 2, 3, 8, 9, 10} 
    /// (i.e. the proper sub-patterns of 1011 are 0001, 0010, 0011, 1000, 1001, and 1010)
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <returns></returns>
    public static IEnumerable<uint> GetProperSubPatterns(this uint bitPattern)
    {
        if (bitPattern == 0U)
            yield break;

        //Find proper sub patterns that are not zero and not equal to the original pattern
        var bitPositions = PatternToPositions(bitPattern).ToArray();
        var count = (1U << bitPositions.Length) - 1U;

        for (var p = 1U; p < count; p++)
            yield return PositionsToPattern(
                bitPositions.PickItemsUsingPattern(p)
            );
    }

    /// <summary>
    /// Finds all super patterns of the given pattern having a given number of bits.
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitsCount"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<uint> GetSuperPatterns(this uint bitPattern, int bitsCount)
    {
        var superPattern = CreateMask(bitsCount);

        //Make sure bitPattern is a sub-pattern of superPattern
        if ((superPattern | bitPattern) != superPattern)
            throw new InvalidOperationException();

        return GetSubPatterns(superPattern ^ bitPattern).Select(p => p | bitPattern);
    }

    /// <summary>
    /// Finds all proper super patterns of the given pattern having a given number of bits.
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="bitsCount"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<uint> GetProperSuperPatterns(this uint bitPattern, int bitsCount)
    {
        var superPattern = CreateMask(bitsCount);

        //Make sure bitPattern is a sub-pattern of superPattern
        if ((superPattern | bitPattern) != superPattern)
            throw new InvalidOperationException();

        return GetProperSubPatterns(superPattern ^ bitPattern).Select(p => p | bitPattern);
    }

    /// <summary>
    /// Finds all super patterns of the given pattern that are sub-patterns of another superPattern.
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="superPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<uint> GetSuperPatternsInside(this uint bitPattern, uint superPattern)
    {
        //Make sure bitPattern is a sub-pattern of superPattern
        if ((superPattern | bitPattern) != superPattern)
            throw new InvalidOperationException();

        return GetSubPatterns(superPattern ^ bitPattern).Select(p => p | bitPattern);
    }

    /// <summary>
    /// Finds all proper super patterns of the given pattern that are sub-patterns of another superPattern.
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="superPattern"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<uint> GetProperSuperPatternsInside(this uint bitPattern, uint superPattern)
    {
        //Make sure bitPattern is a sub-pattern of superPattern
        if ((superPattern | bitPattern) != superPattern)
            throw new InvalidOperationException();

        return GetProperSubPatterns(superPattern ^ bitPattern).Select(p => p | bitPattern);
    }

    /// <summary>
    /// Splits ths given pattern into its smallest basic pattern and the remaining sub-pattern.
    /// For example the pattern 11010 is split into the basic pattern 00010 and the sub-pattern 11000
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="basicPattern"></param>
    /// <param name="subPattern"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SplitBySmallestBasicPattern(this uint bitPattern, out uint basicPattern, out uint subPattern)
    {
        if (bitPattern == 0U)
        {
            basicPattern = 0U;
            subPattern = 0U;
            return;
        }

        basicPattern = 1U << FirstOneBitPosition(bitPattern);
        subPattern = bitPattern ^ basicPattern;
    }

    /// <summary>
    /// Splits ths given pattern into its largest basic pattern and the remaining sub-pattern.
    /// For example the pattern 11010 is split into the basic pattern 10000 and the sub-pattern 01010
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="basicPattern"></param>
    /// <param name="subPattern"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SplitByLargestBasicPattern(this uint bitPattern, out uint basicPattern, out uint subPattern)
    {
        if (bitPattern == 0U)
        {
            basicPattern = 0U;
            subPattern = 0U;
            return;
        }

        basicPattern = 1U << LastOneBitPosition(bitPattern);
        subPattern = bitPattern ^ basicPattern;
    }


    /// <summary>
    /// True if the given string consists only of 1U's and 0U's and has length lass than 32 charactrers
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidBitPattern(this string s)
    {
        return s.Length <= MaxBitPatternSize && s.All(c => c == '1' || c == '0');
    }

    /// <summary>
    /// True if the given string consists only of 1U's and 0U's and has length lass than 32 charactrers and 
    /// equal to the given bitsCount
    /// </summary>
    /// <param name="s"></param>
    /// <param name="bitsCount"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidBitPattern(this string s, int bitsCount)
    {
        return bitsCount <= MaxBitPatternSize && s.Length == bitsCount && s.All(c => c == '1' || c == '0');
    }

    /// <summary>
    /// Create a concatenation of strings picked from the given list of strings using the bit pattern 
    /// </summary>
    /// <param name="stringsList"></param>
    /// <param name="bitPattern"></param>
    /// <param name="zeroElement"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ConcatenateUsingPattern(this IEnumerable<string> stringsList, uint bitPattern, string zeroElement)
    {
        return
            bitPattern == 0U
                ? zeroElement
                : PickItemsUsingPattern(stringsList, bitPattern).ConcatenateText();
    }

    /// <summary>
    /// Create a concatenation of strings picked from the given list of strings using the bit pattern 
    /// </summary>
    /// <param name="stringsList"></param>
    /// <param name="bitPatternsList"></param>
    /// <param name="zeroElement"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<string> ConcatenateUsingPatterns(this IEnumerable<string> stringsList, IEnumerable<uint> bitPatternsList, string zeroElement)
    {
        return bitPatternsList.Select(
            bitPattern => 
                stringsList.ConcatenateUsingPattern(bitPattern, zeroElement)
        );
    }

    /// <summary>
    /// Create a concatenation of strings picked from the given list of strings using the bit pattern 
    /// </summary>
    /// <param name="bitPattern"></param>
    /// <param name="stringsList"></param>
    /// <param name="separator"></param>
    /// <param name="zeroElement"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ConcatenateUsingPattern(this IEnumerable<string> stringsList, uint bitPattern, string zeroElement, string separator)
    {
        return 
            bitPattern == 0U 
                ? zeroElement 
                : PickItemsUsingPattern(stringsList, bitPattern).ConcatenateText(separator);
    }

    /// <summary>
    /// Create a concatenation of strings picked from the given list of strings using the bit pattern 
    /// </summary>
    /// <param name="stringsList"></param>
    /// <param name="bitPattern"></param>
    /// <param name="zeroElement"></param>
    /// <param name="separator"></param>
    /// <param name="finalPrefix"></param>
    /// <param name="finalSuffix"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ConcatenateUsingPattern(this IEnumerable<string> stringsList, uint bitPattern, string zeroElement, string separator, string finalPrefix, string finalSuffix)
    {
        return
            bitPattern == 0U
                ? zeroElement
                : PickItemsUsingPattern(stringsList, bitPattern).ConcatenateText(separator, finalPrefix, finalSuffix);
    }

    /// <summary>
    /// Create a concatenation of strings picked from the given list of strings using the bit pattern 
    /// </summary>
    /// <param name="stringsList"></param>
    /// <param name="bitPattern"></param>
    /// <param name="zeroElement"></param>
    /// <param name="separator"></param>
    /// <param name="finalPrefix"></param>
    /// <param name="finalSuffix"></param>
    /// <param name="itemPrefix"></param>
    /// <param name="itemSuffix"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ConcatenateUsingPattern(this IEnumerable<string> stringsList, uint bitPattern, string zeroElement, string separator, string finalPrefix, string finalSuffix, string itemPrefix, string itemSuffix)
    {
        return
            bitPattern == 0U
                ? zeroElement
                : PickItemsUsingPattern(stringsList, bitPattern).ConcatenateText(separator, finalPrefix, finalSuffix, itemPrefix, itemSuffix);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Repeat<T>(this uint count, T value)
    {
        while (count > 0)
        {
            yield return value;
            count--;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> MapRange<T>(this uint count, Func<uint, T> mappingFunc)
    {
        return Enumerable
            .Range(0, (int) count)
            .Select(i => mappingFunc((uint) i));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> MapRange<T>(this uint count, uint offset, Func<uint, T> mappingFunc)
    {
        return Enumerable
            .Range(0, (int) count)
            .Select(i => mappingFunc(offset + (uint) i));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<KeyValuePair<uint, T>> GetMappedRange<T>(this uint count, Func<uint, T> mappingFunc)
    {
        return Enumerable
            .Range(0, (int) count)
            .Select(
                i =>
                {
                    var key = (uint) i;
                    return new KeyValuePair<uint, T>(key, mappingFunc(key));
                }
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<KeyValuePair<uint, T>> GetMappedRange<T>(this uint count, uint offset, Func<uint, T> mappingFunc)
    {
        return Enumerable
            .Range(0, (int) count)
            .Select(
                i =>
                {
                    var key = offset + (uint) i;
                    return new KeyValuePair<uint, T>(key, mappingFunc(key));
                }
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<uint> GetRange(this uint count)
    {
        return Enumerable.Range(0, (int) count).Select(i => (uint) i);
    }
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<uint> GetRange(this uint count, uint offset)
    {
        return Enumerable
            .Range(0, (int) count)
            .Select(i => offset + (uint) i);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<uint, T> RangeToDictionary<T>(this uint count, Func<uint, T> keyValueFunc)
    {
        return Enumerable
            .Range(0, (int) count)
            .ToDictionary(
                i => (uint) i, 
                i => keyValueFunc((uint) i)
            );
    }
        
    public static bool TryGetMinValue(this IEnumerable<uint> valuesList, out uint minValue)
    {
        var foundFlag = false;
        minValue = uint.MaxValue;

        foreach (var value in valuesList)
        {
            foundFlag = true;

            if (value < minValue)
                minValue = value;
        }

        return foundFlag;
    }

    public static bool TryGetMaxValue(this IEnumerable<uint> valuesList, out uint maxValue)
    {
        var foundFlag = false;
        maxValue = uint.MinValue;

        foreach (var value in valuesList)
        {
            foundFlag = true;

            if (value > maxValue)
                maxValue = value;
        }

        return foundFlag;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Min(this uint a, uint b)
    {
        return a <= b ? a : b;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Max(this uint a, uint b)
    {
        return a >= b ? a : b;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Min(this uint a, uint b, uint c)
    {
        return a <= b 
            ? (a <= c ? a : c)
            : (b <= c ? b : c);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Max(this uint a, uint b, uint c)
    {
        return a >= b 
            ? (a >= c ? a : c)
            : (b >= c ? b : c);
    }
}