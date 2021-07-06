using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DataStructuresLib.BitManipulation
{
    /// <summary>
    /// See here for reference:
    /// https://graphics.stanford.edu/~seander/bithacks.html
    /// </summary>
    public static class Int32BitUtils
    {
        public static class Naive
        {
            public static int FirstOneBitPosition(int bitPattern)
            {
                var bitPosition = 0;

                while (bitPattern > 0)
                {
                    if ((bitPattern & 1) == 1)
                        return bitPosition;

                    bitPosition++;
                    bitPattern >>= 1;
                }

                return -1;
            }

            public static int LastOneBitPosition(int bitPattern)
            {
                var bitPosition = 0;

                var lastOneBitPos = -1;

                while (bitPattern > 0)
                {
                    if ((bitPattern & 1) == 1)
                        lastOneBitPos = bitPosition;

                    bitPosition++;
                    bitPattern >>= 1;
                }

                return lastOneBitPos;
            }

            
            public static int PatternToMask(int bitPattern)
            {
                var bitsMask = 0;
                var bitPosition = 0;

                while (bitPattern > 0)
                {
                    bitsMask |= (1 << bitPosition);

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
        public static int MaxBitPosition => 30;

        /// <summary>
        /// The largest bit pattern that can be handled
        /// </summary>
        public static int MaxBitPatternSize => 31;


        /// <summary>
        /// Tests if bitPattern is an odd integer
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static bool IsOdd(this int bitPattern)
        {
            return (bitPattern & 1) != 0;
        }

        /// <summary>
        /// Tests if bitPattern is an even integer
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static bool IsEven(this int bitPattern)
        {
            return (bitPattern & 1) == 0;
        }

        /// <summary>
        /// Returns true if this is a basic pattern (i.e. an integer between 1 and 2^30 that is a power of 2)
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static bool IsBasicPattern(this int bitPattern)
        {
            return bitPattern != 0 && (bitPattern & (bitPattern - 1)) == 0;
        }

        /// <summary>
        /// Returns true if this is a zero or basic pattern (i.e. an integer between 0 and 2^30 that is a power of 2)
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static bool IsZeroOrBasicPattern(this int bitPattern)
        {
            return (bitPattern & (bitPattern - 1)) == 0;
        }

        /// <summary>
        /// Tests if the bit at the given position is a one
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        public static bool IsOneAt(this int bitPattern, int bitPosition)
        {
            return ((1 << bitPosition) & bitPattern) != 0;
        }

        /// <summary>
        /// Tests if the bit at the given position is a zero
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        public static bool IsZeroAt(this int bitPattern, int bitPosition)
        {
            return ((1 << bitPosition) & bitPattern) == 0;
        }


        /// <summary>
        /// Count the number of ones in the given bit pattern
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static int CountOnes(this int bitPattern)
        {
            if (bitPattern == 0)
                return 0;

            // Brian Kernighan’s Algorithm. See here for more details:
            // https://www.geeksforgeeks.org/count-set-bits-in-an-integer/
            // Subtracting 1 from a decimal number flips all the bits after
            // the rightmost set bit(which is 1) including the rightmost set bit.
            // So if we subtract a number by 1 and do it bitwise & with itself
            // (n & (n-1)), we unset the rightmost set bit. If we do n & (n-1)
            // in a loop and count the number of times the loop executes, we get
            // the set bit count. The beauty of this solution is the number of
            // times it loops is equal to the number of set bits in a given integer. 

            var count = 0;
            var n = bitPattern;
            
            while (n > 0)
            {
                n &= (n - 1);

                count++;
            }

            return count;

            //while (bitPattern > 0)
            //{
            //    if ((bitPattern & 1) == 1)
            //        count++;

            //    bitPattern >>= 1;
            //}

            //return count;
        }
        
        /// <summary>
        /// Returns the bit position of the first one bit in the given pattern.
        /// If the pattern is zero this returns -1.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static int FirstOneBitPosition(this int bitPattern)
        {
            if (bitPattern == 0)
                return -1;
            
            var bitPatternUInt32 = (uint) bitPattern;

            if ((bitPatternUInt32 & 65535U) != 0)
            {
                // Found ones in positions 0-15
                if ((bitPatternUInt32 & 255U) != 0)
                {
                    // Found ones in positions 0-7
                    if ((bitPatternUInt32 & 15U) != 0)
                    {
                        // Found ones in positions 0-3
                        if ((bitPatternUInt32 & 3U) != 0)
                        {
                            // Found ones in positions 0-1
                            return (bitPatternUInt32 & 1U) != 0 ? 0 : 1;
                        }

                        // Found ones in positions 2-3
                        return (bitPatternUInt32 & 4U) != 0 ? 2 : 3;
                    }

                    // Found ones in positions 4-7
                    if ((bitPatternUInt32 & 48U) != 0)
                    {
                        // Found ones in positions 4-5
                        return (bitPatternUInt32 & 16U) != 0 ? 4 : 5;
                    }

                    // Found ones in positions 6-7
                    return (bitPatternUInt32 & 64U) != 0 ? 6 : 7;
                }

                // Found ones in positions 8-15
                if ((bitPatternUInt32 & 3840U) != 0)
                {
                    // Found ones in positions 8-11
                    if ((bitPatternUInt32 & 768U) != 0)
                    {
                        // Found ones in positions 8-9
                        return (bitPatternUInt32 & 256U) != 0 ? 8 : 9;
                    }

                    // Found ones in positions 10-11
                    return (bitPatternUInt32 & 1024U) != 0 ? 10 : 11;
                }

                // Found ones in positions 12-15
                if ((bitPatternUInt32 & 12288U) != 0)
                {
                    // Found ones in positions 12-13
                    return (bitPatternUInt32 & 4096U) != 0 ? 12 : 13;
                }

                // Found ones in positions 14-15
                return (bitPatternUInt32 & 16384U) != 0 ? 14 : 15;
            }

            // Found ones in positions 16-31
            if ((bitPatternUInt32 & 16711680U) != 0)
            {
                // Found ones in positions 16-23
                if ((bitPatternUInt32 & 983040U) != 0)
                {
                    // Found ones in positions 16-19
                    if ((bitPatternUInt32 & 196608U) != 0)
                    {
                        // Found ones in positions 16-17
                        return (bitPatternUInt32 & 65536U) != 0 ? 16 : 17;
                    }

                    // Found ones in positions 18-19
                    return (bitPatternUInt32 & 262144U) != 0 ? 18 : 19;
                }

                // Found ones in positions 20-23
                if ((bitPatternUInt32 & 3145728U) != 0)
                {
                    // Found ones in positions 20-21
                    return (bitPatternUInt32 & 1048576U) != 0 ? 20 : 21;
                }

                // Found ones in positions 22-23
                return (bitPatternUInt32 & 4194304U) != 0 ? 22 : 23;
            }

            // Found ones in positions 24-31
            if ((bitPatternUInt32 & 251658240U) != 0)
            {
                // Found ones in positions 24-27
                if ((bitPatternUInt32 & 50331648U) != 0)
                {
                    // Found ones in positions 24-25
                    return (bitPatternUInt32 & 16777216U) != 0 ? 24 : 25;
                }

                // Found ones in positions 26-27
                return (bitPatternUInt32 & 67108864U) != 0 ? 26 : 27;
            }

            // Found ones in positions 28-31
            if ((bitPatternUInt32 & 805306368U) != 0)
            {
                // Found ones in positions 28-29
                return (bitPatternUInt32 & 268435456U) != 0 ? 28 : 29;
            }

            // Found ones in positions 30-31
            return (bitPatternUInt32 & 1073741824U) != 0 ? 30 : 31;

            //if (bitPattern == 0)
            //    return -1;

            //var bitPosition = 0;

            //while (bitPattern > 0)
            //{
            //    if ((bitPattern & 1) == 1)
            //        return bitPosition;

            //    bitPosition++;
            //    bitPattern >>= 1;
            //}

            //return -1;
        }

        /// <summary>
        /// Returns the bit position of the last one bit in the given pattern. If the pattern is zero this
        /// returns -1.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static int LastOneBitPosition(this int bitPattern)
        {
            if (bitPattern == 0U) 
                return -1;

            var bitPatternUInt32 = (uint) bitPattern;

            if ((bitPatternUInt32 & 4294901760U) != 0)
            {
                // Found ones in positions 16-31
                if ((bitPatternUInt32 & 4278190080U) != 0)
                {
                    // Found ones in positions 24-31
                    if ((bitPatternUInt32 & 4026531840U) != 0)
                    {
                        // Found ones in positions 28-31
                        if ((bitPatternUInt32 & 3221225472U) != 0)
                        {
                            // Found ones in positions 30-31
                            return (bitPatternUInt32 & 2147483648U) != 0 ? 31 : 30;
                        }

                        // Found ones in positions 28-29
                        return (bitPatternUInt32 & 536870912U) != 0 ? 29 : 28;
                    }

                    // Found ones in positions 24-27
                    if ((bitPatternUInt32 & 201326592U) != 0)
                    {
                        // Found ones in positions 26-27
                        return (bitPatternUInt32 & 134217728U) != 0 ? 27 : 26;
                    }

                    // Found ones in positions 24-25
                    return (bitPatternUInt32 & 33554432U) != 0 ? 25 : 24;
                }

                // Found ones in positions 16-23
                if ((bitPatternUInt32 & 15728640U) != 0)
                {
                    // Found ones in positions 20-23
                    if ((bitPatternUInt32 & 12582912U) != 0)
                    {
                        // Found ones in positions 22-23
                        return (bitPatternUInt32 & 8388608U) != 0 ? 23 : 22;
                    }

                    // Found ones in positions 20-21
                    return (bitPatternUInt32 & 2097152U) != 0 ? 21 : 20;
                }

                // Found ones in positions 16-19
                if ((bitPatternUInt32 & 786432U) != 0)
                {
                    // Found ones in positions 18-19
                    return (bitPatternUInt32 & 524288U) != 0 ? 19 : 18;
                }

                // Found ones in positions 16-17
                return (bitPatternUInt32 & 131072U) != 0 ? 17 : 16;
            }

            // Found ones in positions 0-15
            if ((bitPatternUInt32 & 65280U) != 0)
            {
                // Found ones in positions 8-15
                if ((bitPatternUInt32 & 61440U) != 0)
                {
                    // Found ones in positions 12-15
                    if ((bitPatternUInt32 & 49152U) != 0)
                    {
                        // Found ones in positions 14-15
                        return (bitPatternUInt32 & 32768U) != 0 ? 15 : 14;
                    }

                    // Found ones in positions 12-13
                    return (bitPatternUInt32 & 8192U) != 0 ? 13 : 12;
                }

                // Found ones in positions 8-11
                if ((bitPatternUInt32 & 3072U) != 0)
                {
                    // Found ones in positions 10-11
                    return (bitPatternUInt32 & 2048U) != 0 ? 11 : 10;
                }

                // Found ones in positions 8-9
                return (bitPatternUInt32 & 512U) != 0 ? 9 : 8;
            }

            // Found ones in positions 0-7
            if ((bitPatternUInt32 & 240U) != 0)
            {
                // Found ones in positions 4-7
                if ((bitPatternUInt32 & 192U) != 0)
                {
                    // Found ones in positions 6-7
                    return (bitPatternUInt32 & 128U) != 0 ? 7 : 6;
                }

                // Found ones in positions 4-5
                return (bitPatternUInt32 & 32U) != 0 ? 5 : 4;
            }

            // Found ones in positions 0-3
            if ((bitPatternUInt32 & 12U) != 0)
            {
                // Found ones in positions 2-3
                return (bitPatternUInt32 & 8U) != 0 ? 3 : 2;
            }

            // Found ones in positions 0-1
            return (bitPatternUInt32 & 2U) != 0 ? 1 : 0;

            //var bitPosition = 0;

            //var lastOneBitPos = -1;

            //while (bitPattern > 0)
            //{
            //    if ((bitPattern & 1) == 1)
            //        lastOneBitPos = bitPosition;

            //    bitPosition++;
            //    bitPattern >>= 1;
            //}

            //return lastOneBitPos;
        }

        /// <summary>
        /// Returns the largest integer that is a power of 2 (i.e. 1, 2, 4, 8, 16, etc.)
        /// that is smaller or equal to the given integer. If the given integer is 0
        /// this returns 0.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static int Power2LowerLimit(this int bitPattern)
        {
            if (bitPattern == 0) 
                return 0;

            if ((bitPattern >> 30) != 0)
                return 1 << 30;

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
        /// Returns the smallest integer that is a power of 2 (i.e. 1, 2, 4, 8, 16, etc.)
        /// that is larger or equal to the given integer. If the given integer is 0
        /// this returns 0.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static int Power2UpperLimit(this int bitPattern)
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
        /// Create a full mask that contains the given pattern
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static int PatternToMask(this int bitPattern)
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
        public static int SetBitToZeroAt(this int bitPattern, int bitPosition)
        {
            return bitPattern & ~(1 << bitPosition);
        }

        /// <summary>
        /// Sets the bits at the given positions to zero
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        public static int SetBitsToZeroAt(this int bitPattern, IEnumerable<int> bitPositions)
        {
            var bitMask = bitPositions.Aggregate(
                0,
                (currentBitPattern, bitPosition) => currentBitPattern | (1 << bitPosition)
            );

            return bitPattern & ~bitMask;
        }

        /// <summary>
        /// Sets the bits at the given positions to zero
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        public static int SetBitsToZeroAt(this int bitPattern, params int[] bitPositions)
        {
            var bitMask = bitPositions.Aggregate(
                0,
                (currentBitPattern, bitPosition) => currentBitPattern | (1 << bitPosition)
            );

            return bitPattern & ~bitMask;
        }

        /// <summary>
        /// Sets the bit at the given position to one
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        public static int SetBitToOneAt(this int bitPattern, int bitPosition)
        {
            return bitPattern | (1 << bitPosition);
        }

        /// <summary>
        /// Sets the bits at the given positions to one
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        public static int SetBitsToOneAt(this int bitPattern, IEnumerable<int> bitPositions)
        {
            var bitMask = bitPositions.Aggregate(
                0,
                (currentBitPattern, bitPosition) => currentBitPattern | (1 << bitPosition)
            );

            return bitPattern | bitMask;
        }

        /// <summary>
        /// Sets the bits at the given positions to one
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        public static int SetBitsToOneAt(this int bitPattern, params int[] bitPositions)
        {
            var bitMask = bitPositions.Aggregate(
                0,
                (currentBitPattern, bitPosition) => currentBitPattern | (1 << bitPosition)
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
        public static int SetBitAt(this int bitPattern, int bitPosition, bool value)
        {
            return
                value
                    ? bitPattern | (1 << bitPosition)
                    : bitPattern & ~(1 << bitPosition);
        }

        /// <summary>
        /// Inverts the value of the bit at the given position
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        public static int InvertBitAt(this int bitPattern, int bitPosition)
        {
            return bitPattern ^ (1 << bitPosition);
        }

        /// <summary>
        /// Inverts the bits at the given positions
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        public static int InvertBitsAt(this int bitPattern, IEnumerable<int> bitPositions)
        {
            return
                bitPositions.Aggregate(
                    bitPattern,
                    (current, bitPosition) => current ^ (1 << bitPosition)
                );
        }

        /// <summary>
        /// Inverts the bits at the given positions
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        public static int InvertBitsAt(this int bitPattern, params int[] bitPositions)
        {
            return
                bitPositions.Aggregate(
                    bitPattern,
                    (current, bitPosition) => current ^ (1 << bitPosition)
                );
        }


        /// <summary>
        /// Returns true or false depending on the value of the bit at the given position being 1 or 0.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        public static bool BitToBoolean(this int bitPattern, int bitPosition)
        {
            return ((1 << bitPosition) & bitPattern) != 0;
        }

        /// <summary>
        /// Reverse the order of bits in the given bit pattern
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitsCount"></param>
        /// <returns></returns>
        public static int ReverseBits(this int bitPattern, int bitsCount)
        {
            Debug.Assert(
                bitsCount > 0 &&
                bitPattern < (1 << (bitsCount + 1))
            );

            var result = 0;

            var i = bitsCount - 1;
            while (bitPattern != 0)
            {
                if ((bitPattern & 1) != 0)
                    result |= (1 << i);

                i--;
                bitPattern >>= 1;
            }

            return result;
        }


        /// <summary>
        /// Create a mask containing a number of ones as given. 
        /// For example CreateFullMask(3) returns 111 (i.e. 7 decimal)
        /// </summary>
        /// <param name="bitsCount"></param>
        /// <returns></returns>
        public static int CreateMask(int bitsCount)
        {
            return (int) ((1u << bitsCount) - 1);
        }
        
        /// <summary>
        /// Create a mask where all positions between the given are set to 1
        /// </summary>
        /// <param name="bitPosition1"></param>
        /// <param name="bitPosition2"></param>
        /// <returns></returns>
        public static int CreateMask(int bitPosition1, int bitPosition2)
        {
            var bitsCount = bitPosition2 - bitPosition1;

            return (int) (((1u << bitsCount) - 1u) << bitPosition1);
        }

        /// <summary>
        /// Given two bit patterns and a bit size, this shifts the first pattern by size bits 
        /// to the left and appends the second to combine the two patterns using an OR bitwise operation.
        /// For example 1010.AppendPattern(5, 0001) gives 1010 0 0001
        /// </summary>
        /// <param name="bitPattern1"></param>
        /// <param name="bitPattern2"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static int AppendPattern(this int bitPattern1, int size, int bitPattern2)
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
        public static int MergeWithPattern(this int bitPattern1, int bitMask, int bitPattern2)
        {
            return bitPattern1 ^ ((bitPattern1 ^ bitPattern2) & bitMask);
        }


        /// <summary>
        /// Gets the numbers between two patterns.
        /// </summary>
        /// <param name="firstPattern"></param>
        /// <param name="secondPattern"></param>
        /// <returns></returns>
        public static IEnumerable<int> PatternsBetween(int firstPattern, int secondPattern)
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
        /// Suppose we have a pattern of N bits set to 1 in an integer and we want the next permutation 
        /// of N (1 bits) in a lexicographical sense. For example, if N is 3 and the bit pattern is 00010011, 
        /// the next patterns would be 00010101, 00010110, 00011001,00011010, 00011100, 00100011, and so forth.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static int NextPermutation(this int bitPattern)
        {
            var tempPattern = (bitPattern | (bitPattern - 1)) + 1;
            var tempPattern1 = tempPattern & -tempPattern;
            var tempPattern2 = bitPattern & -bitPattern;

            return tempPattern | (((tempPattern1 / tempPattern2) >> 1) - 1);
        }

        /// <summary>
        /// Constructs all distinct patterns of fixed size containing a given number of ones
        /// </summary>
        /// <param name="patternSize"></param>
        /// <param name="onesCount"></param>
        /// <returns></returns>
        public static IEnumerable<int> OnesPermutations(int patternSize, int onesCount)
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
        public static IEnumerable<int> ZerosPermutations(int patternSize, int zerosCount)
        {
            var startPattern = CreateMask(zerosCount);

            if (patternSize <= zerosCount)
            {
                yield return 0;
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
        public static IEnumerable<bool> PatternToBooleans(this int bitPattern)
        {
            while (bitPattern > 0)
            {
                yield return ((bitPattern & 1) != 0);

                bitPattern >>= 1;
            }
        }

        /// <summary>
        /// Converts the given pattern into a list of boolean values with a given number of items
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitsCount"></param>
        /// <returns></returns>
        public static IEnumerable<bool> PatternToBooleans(this int bitPattern, int bitsCount)
        {
            while (bitPattern > 0 && bitsCount > 0)
            {
                yield return ((bitPattern & 1) != 0);

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
        public static IEnumerable<int> PatternToPositions(this int bitPattern)
        {
            var bitPosition = 0;

            while (bitPattern > 0)
            {
                if ((bitPattern & 1) != 0)
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
        public static IEnumerable<T> PatternToSequence<T>(this int bitPattern, int bitsCount, T zeroItem, T oneItem)
        {
            while (bitPattern > 0 && bitsCount > 0)
            {
                yield return ((bitPattern & 1) != 0) ? oneItem : zeroItem;

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
        public static IEnumerable<T> PatternToSequence<T>(this int bitPattern, T zeroItem, T oneItem)
        {
            while (bitPattern > 0)
            {
                yield return ((bitPattern & 1) != 0) ? oneItem : zeroItem;

                bitPattern >>= 1;
            }
        }

        /// <summary>
        /// Converts the given bit pattern into a string of '1' and '0' characters with the MSB at the 
        /// first character and the LSB at the last character
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static string PatternToString(this int bitPattern)
        {
            return Convert.ToString(bitPattern, 2);
        }

        /// <summary>
        /// Converts the given bit pattern into a string of '1' and '0' characters with the MSB at the 
        /// first character and the LSB at the last character
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitsCount"></param>
        /// <returns></returns>
        public static string PatternToString(this int bitPattern, int bitsCount)
        {
            return Convert.ToString(bitPattern, 2).PadLeft(bitsCount, '0');
        }
        
        /// <summary>
        /// Picks elements of the given sequence based on the given pattern.
        /// For example new [] {"a", "b", "c", "d"}.PickUsingPattern(9) gives the sequence {"a", "d"}.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static IEnumerable<T> PickUsingPattern<T>(this IEnumerable<T> items, int bitPattern)
        {
            var bitPosition = 0;

            return
                items
                    .Take(MaxBitPosition)
                    .Where(item => ((1 << (bitPosition++)) & bitPattern) != 0);
        }


        /// <summary>
        /// Converts a sequence of boolean values into a bit pattern. The first item in the sequence
        /// corresponds to a 1 or 0 in the LSB of the pattern and so on.
        /// </summary>
        /// <param name="boolsSeq"></param>
        /// <returns></returns>
        public static int BooleansToPattern(this IEnumerable<bool> boolsSeq)
        {
            var bitPosistion = 0;

            return
                boolsSeq
                    .Take(MaxBitPosition)
                    .Aggregate(
                        0,
                        (currentBitPattern, item) =>
                        {
                            var newPattern =
                                item
                                    ? ((1 << bitPosistion) | currentBitPattern)
                                    : currentBitPattern;

                            bitPosistion++;

                            return newPattern;
                        }
                    );
        }

        /// <summary>
        /// Converts a sequence of boolean values into a bit pattern. The first item in the sequence
        /// corresponds to a 1 or 0 in the LSB of the pattern and so on.
        /// </summary>
        /// <param name="boolsSeq"></param>
        /// <returns></returns>
        public static int BooleansToPattern(params bool[] boolsSeq)
        {
            var bitPosistion = 0;

            return
                boolsSeq
                    .Take(MaxBitPosition)
                    .Aggregate(
                        0,
                        (currentBitPattern, item) =>
                        {
                            var newPattern =
                                item
                                    ? ((1 << bitPosistion) | currentBitPattern)
                                    : currentBitPattern;

                            bitPosistion++;

                            return newPattern;
                        }
                    );
        }

        /// <summary>
        /// Returns a bit pattern containing ones at the positions given in a list of bit positions
        /// </summary>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        public static int PositionsToPattern(this IEnumerable<int> bitPositions)
        {
            return
                bitPositions.Aggregate(
                    0,
                    (currentBitPattern, bitPosition) => currentBitPattern | (1 << bitPosition)
                );
        }

        /// <summary>
        /// Returns a bit pattern containing ones at the positions given in a list of bit positions
        /// </summary>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        public static int PositionsToPattern(params int[] bitPositions)
        {
            return
                bitPositions.Aggregate(
                    0,
                    (currentBitPattern, bitPosition) => currentBitPattern | (1 << bitPosition)
                );
        }

        /// <summary>
        /// Converts the given string into a bit pattern by putting a 1 at the positions corresponding to
        /// where the '1' character is found in the string. The MSB in the pattern is the first character.
        /// </summary>
        /// <param name="binaryString"></param>
        /// <returns></returns>
        public static int StringToPattern(this string binaryString)
        {
            if (binaryString.Length > MaxBitPatternSize)
                throw new InvalidOperationException();

            var id = 0;

            foreach (var c in binaryString)
            {
                id <<= 1;

                if (c == '1') id |= 1;
            }

            return id;
        }

        /// <summary>
        /// Converts the given string into a bit pattern by putting a 1 at the positions corresponding to
        /// where the oneChar character is found in the string. The MSB in the pattern is the first character
        /// of the string while the LSB is the last character.
        /// </summary>
        /// <param name="binaryString"></param>
        /// <param name="oneChar"></param>
        /// <returns></returns>
        public static int StringToPattern(this string binaryString, char oneChar)
        {
            if (binaryString.Length > MaxBitPatternSize)
                throw new InvalidOperationException();

            var id = 0;

            foreach (var c in binaryString)
            {
                id <<= 1;

                if (c == oneChar) id |= 1;
            }

            return id;
        }


        /// <summary>
        /// Returns true if the given bit pattern is a sub pattern of the other input.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="superPattern"></param>
        /// <returns></returns>
        public static bool IsSubPatternOf(this int bitPattern, int superPattern)
        {
            return (superPattern | bitPattern) == superPattern;
        }

        /// <summary>
        /// Returns true if the given bit pattern is a proper sub pattern of the other input.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="superPattern"></param>
        /// <returns></returns>
        public static bool IsProperSubPatternOf(this int bitPattern, int superPattern)
        {
            return
                bitPattern != 0 &&
                bitPattern != superPattern &&
                (superPattern | bitPattern) == superPattern;
        }

        /// <summary>
        /// Returns true if the given bit pattern is a super pattern of the other input.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="subPattern"></param>
        /// <returns></returns>
        public static bool IsSuperPatternOf(this int bitPattern, int subPattern)
        {
            return (subPattern | bitPattern) == bitPattern;
        }

        /// <summary>
        /// Returns true if the given bit pattern is a proper super pattern of the other input.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="subPattern"></param>
        /// <returns></returns>
        public static bool IsProperSuperPatternOf(this int bitPattern, int subPattern)
        {
            return
                subPattern != 0 &&
                bitPattern != subPattern &&
                (subPattern | bitPattern) == bitPattern;
        }

        /// <summary>
        /// Split the given pattern into its basic patterns each containing a single 1.
        /// For example GetBasicPatterns(14) gives {2, 4, 8}
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetBasicPatterns(this int bitPattern)
        {
            var bitPosition = 0;

            while (bitPattern > 0)
            {
                if ((bitPattern & 1) != 0)
                    yield return 1 << bitPosition;

                bitPosition++;
                bitPattern >>= 1;
            }
        }

        /// <summary>
        /// Split the given pattern into its sub patterns.
        /// For example GetSubPatterns(11) gives {0, 1, 2, 3, 8, 9, 10, 11} 
        /// (i.e. the sub-patterns of 1011 are 0000, 0001, 0010, 0011, 1000, 1001, 1010, and 1011)
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetSubPatterns(this int bitPattern)
        {
            //The zero pattern is always a sub-pattern of any bit pattern
            yield return 0;

            if (bitPattern == 0)
                yield break;

            //Find proper sub patterns that are not zero and nor equal to the original pattern
            var bitPositions = PatternToPositions(bitPattern).ToArray();
            var count = (1 << bitPositions.Length) - 1;

            for (var p = 1; p < count; p++)
                yield return PositionsToPattern(
                    bitPositions.PickUsingPattern(p)
                );

            //The original pattern is a sub-pattern of itself
            yield return bitPattern;
        }

        /// <summary>
        /// Split the given pattern into its proper sub-patterns.
        /// For example GetProperSubPatterns(11) gives {1, 2, 3, 8, 9, 10} 
        /// (i.e. the proper sub-patterns of 1011 are 0001, 0010, 0011, 1000, 1001, and 1010)
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetProperSubPatterns(this int bitPattern)
        {
            if (bitPattern == 0)
                yield break;

            //Find proper sub patterns that are not zero and not equal to the original pattern
            var bitPositions = PatternToPositions(bitPattern).ToArray();
            var count = (1 << bitPositions.Length) - 1;

            for (var p = 1; p < count; p++)
                yield return PositionsToPattern(
                    bitPositions.PickUsingPattern(p)
                );
        }

        /// <summary>
        /// Finds all super patterns of the given pattern having a given number of bits.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitsCount"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetSuperPatterns(this int bitPattern, int bitsCount)
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
        public static IEnumerable<int> GetProperSuperPatterns(this int bitPattern, int bitsCount)
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
        public static IEnumerable<int> GetSuperPatternsInside(this int bitPattern, int superPattern)
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
        public static IEnumerable<int> GetProperSuperPatternsInside(this int bitPattern, int superPattern)
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
        public static void SplitBySmallestBasicPattern(this int bitPattern, out int basicPattern, out int subPattern)
        {
            if (bitPattern == 0)
            {
                basicPattern = 0;
                subPattern = 0;
                return;
            }

            basicPattern = 1 << FirstOneBitPosition(bitPattern);
            subPattern = bitPattern ^ basicPattern;
        }

        /// <summary>
        /// Splits ths given pattern into its largest basic pattern and the remaining sub-pattern.
        /// For example the pattern 11010 is split into the basic pattern 10000 and the sub-pattern 01010
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="basicPattern"></param>
        /// <param name="subPattern"></param>
        public static void SplitByLargestBasicPattern(this int bitPattern, out int basicPattern, out int subPattern)
        {
            if (bitPattern == 0)
            {
                basicPattern = 0;
                subPattern = 0;
                return;
            }

            basicPattern = 1 << LastOneBitPosition(bitPattern);
            subPattern = bitPattern ^ basicPattern;
        }


        /// <summary>
        /// True if the given string consists only of 1's and 0's and has length lass than 32 charactrers
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidBitPattern(this string s)
        {
            return s.Length <= MaxBitPatternSize && s.All(c => c == '1' || c == '0');
        }

        /// <summary>
        /// True if the given string consists only of 1's and 0's and has length lass than 32 charactrers and 
        /// equal to the given bitsCount
        /// </summary>
        /// <param name="s"></param>
        /// <param name="bitsCount"></param>
        /// <returns></returns>
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
        public static string ConcatenateUsingPattern(this IEnumerable<string> stringsList, int bitPattern,
            string zeroElement)
        {
            return
                bitPattern == 0
                    ? zeroElement
                    : PickUsingPattern(stringsList, bitPattern).ConcatenateText();
        }

        /// <summary>
        /// Create a concatenation of strings picked from the given list of strings using the bit pattern 
        /// </summary>
        /// <param name="stringsList"></param>
        /// <param name="bitPatternsList"></param>
        /// <param name="zeroElement"></param>
        /// <returns></returns>
        public static IEnumerable<string> ConcatenateUsingPatterns(this IEnumerable<string> stringsList,
            IEnumerable<int> bitPatternsList, string zeroElement)
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
        public static string ConcatenateUsingPattern(this IEnumerable<string> stringsList, int bitPattern,
            string zeroElement, string separator)
        {
            return
                bitPattern == 0
                    ? zeroElement
                    : PickUsingPattern(stringsList, bitPattern).ConcatenateText(separator);
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
        public static string ConcatenateUsingPattern(this IEnumerable<string> stringsList, int bitPattern,
            string zeroElement, string separator, string finalPrefix, string finalSuffix)
        {
            return
                bitPattern == 0
                    ? zeroElement
                    : PickUsingPattern(stringsList, bitPattern).ConcatenateText(separator, finalPrefix, finalSuffix);
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
        public static string ConcatenateUsingPattern(this IEnumerable<string> stringsList, int bitPattern,
            string zeroElement, string separator, string finalPrefix, string finalSuffix, string itemPrefix,
            string itemSuffix)
        {
            return
                bitPattern == 0
                    ? zeroElement
                    : PickUsingPattern(stringsList, bitPattern)
                        .ConcatenateText(separator, finalPrefix, finalSuffix, itemPrefix, itemSuffix);
        }
    }
}
