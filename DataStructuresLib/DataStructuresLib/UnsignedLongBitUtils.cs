using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DataStructuresLib
{
    public static class UnsignedLongBitUtils
    {
        /// <summary>
        /// The minimum possible value for a bit position
        /// </summary>
        public static int MinBitPosition => 0;

        /// <summary>
        /// The maximum possible value for a bit position
        /// </summary>
        public static int MaxBitPosition => 63;

        /// <summary>
        /// The largest bit pattern that can be handled
        /// </summary>
        public static int MaxBitPatternSize => 64;


        private static readonly ulong[] Log2CeilingArray = new[]
        {
            0xFFFFFFFF00000000UL,
            0x00000000FFFF0000UL,
            0x000000000000FF00UL,
            0x00000000000000F0UL,
            0x000000000000000CUL,
            0x0000000000000002UL
        };

        /// <summary>
        /// https://stackoverflow.com/questions/3272424/compute-fast-log-base-2-ceiling
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Log2Ceiling(this ulong x)
        {
            var y = (((x & (x - 1UL)) == 0) ? 0 : 1);
            var j = 32;

            for (var i = 0; i < 6; i++) 
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
        public static bool IsOdd(this ulong bitPattern)
        {
            return (bitPattern & 1ul) != 0ul;
        }

        /// <summary>
        /// Tests if bitPattern is an even integer
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static bool IsEven(this ulong bitPattern)
        {
            return (bitPattern & 1ul) == 0ul;
        }

        /// <summary>
        /// Returns true if this is a basic pattern (i.e. an integer between 1ul and 2^30 that is a power of 2)
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static bool IsBasicPattern(this ulong bitPattern)
        {
            return bitPattern != 0ul && (bitPattern & (bitPattern - 1ul)) == 0ul;
        }

        /// <summary>
        /// Returns true if this is a zero or basic pattern (i.e. an integer between 0ul and 2^30 that is a power of 2)
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static bool IsZeroOrBasicPattern(this ulong bitPattern)
        {
            return (bitPattern & (bitPattern - 1ul)) == 0ul;
        }

        /// <summary>
        /// Tests if the bit at the given position is a one
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        public static bool IsOneAt(this ulong bitPattern, int bitPosition)
        {
            return ((1ul << bitPosition) & bitPattern) != 0ul;
        }

        /// <summary>
        /// Tests if the bit at the given position is a zero
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        public static bool IsZeroAt(this ulong bitPattern, int bitPosition)
        {
            return ((1ul << bitPosition) & bitPattern) == 0ul;
        }

        
        /// <summary>
        /// Count the number of ones in the given bit pattern
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static int CountOnes(this ulong bitPattern)
        {
            var onesCount = 0;

            while (bitPattern > 0ul)
            {
                bitPattern &= bitPattern - 1ul; // clear the least significant bit set
                onesCount++;
            }

            return onesCount;
        }

        /// <summary>
        /// Returns the bit position of the first one bit in the given pattern. If the pattern is zero this
        /// returns -1ul.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static int FirstOneBitPosition(this ulong bitPattern)
        {
            var bitPosition = 0;

            while (bitPattern > 0ul)
            {
                if ((bitPattern & 1ul) == 1ul)
                    return bitPosition;

                bitPosition++;
                bitPattern >>= 1;
            }

            return -1;
        }

        /// <summary>
        /// Returns the bit position of the last one bit in the given pattern. If the pattern is zero this
        /// returns -1ul.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static int LastOneBitPosition(this ulong bitPattern)
        {
            var bitPosition = 0;

            var lastOneBitPos = -1;

            while (bitPattern > 0ul)
            {
                if ((bitPattern & 1ul) == 1ul)
                    lastOneBitPos = bitPosition;

                bitPosition++;
                bitPattern >>= 1;
            }

            return lastOneBitPos;
        }

        /// <summary>
        /// Returns the largest integer that is a power of 2 (i.e. 1ul, 2, 4, 8, 16, etc.) that is smaller or
        /// equal to the given integer. If the given integer is 0ul this returns 0ul.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static ulong LowerPower2Limit(this ulong bitPattern)
        {
            if (bitPattern == 0ul) return 0ul;

            return 1ul << FirstOneBitPosition(bitPattern);
        }

        /// <summary>
        /// Returns the smallest integer that is a power of 2 (i.e. 1ul, 2, 4, 8, 16, etc.) that is larger or
        /// equal to the given integer. If the given integer is 0ul this returns 0ul.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static ulong UpperPower2Limit(this ulong bitPattern)
        {
            var bitPosition = 0;
            var onesCount = 0;
            var result = 1ul;

            while (bitPattern > 0ul)
            {
                if ((bitPattern & 1ul) == 1ul)
                {
                    onesCount++;
                    result = (1ul << bitPosition);
                }

                bitPosition++;
                bitPattern >>= 1;
            }

            return (onesCount <= 1) ? result : (result << 1);
        }

        /// <summary>
        /// Create a full mask that contains the given pattern
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static ulong PatternToFullMask(this ulong bitPattern)
        {
            var bitsMask = 0ul;
            var bitPosition = 0;

            while (bitPattern > 0ul)
            {
                bitsMask |= (1ul << bitPosition);

                bitPosition++;
                bitPattern >>= 1;
            }

            return bitsMask;
        }


        /// <summary>
        /// Sets the bit at the given position to zero
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        public static ulong SetBitToZeroAt(this ulong bitPattern, int bitPosition)
        {
            return bitPattern & ~(1ul << bitPosition);
        }

        /// <summary>
        /// Sets the bits at the given positions to zero
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        public static ulong SetBitsToZeroAt(this ulong bitPattern, IEnumerable<int> bitPositions)
        {
            var bitMask = bitPositions.Aggregate(
                0ul,
                (currentBitPattern, bitPosition) => currentBitPattern | (1ul << bitPosition)
            );

            return bitPattern & ~bitMask;
        }

        /// <summary>
        /// Sets the bits at the given positions to zero
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        public static ulong SetBitsToZeroAt(this ulong bitPattern, params int[] bitPositions)
        {
            var bitMask = bitPositions.Aggregate(
                0ul,
                (currentBitPattern, bitPosition) => currentBitPattern | (1ul << bitPosition)
            );

            return bitPattern & ~bitMask;
        }

        /// <summary>
        /// Sets the bit at the given position to one
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        public static ulong SetBitToOneAt(this ulong bitPattern, int bitPosition)
        {
            return bitPattern | (1ul << bitPosition);
        }

        /// <summary>
        /// Sets the bits at the given positions to one
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        public static ulong SetBitsToOneAt(this ulong bitPattern, IEnumerable<int> bitPositions)
        {
            var bitMask = bitPositions.Aggregate(
                0ul,
                (currentBitPattern, bitPosition) => currentBitPattern | (1ul << bitPosition)
            );

            return bitPattern | bitMask;
        }

        /// <summary>
        /// Sets the bits at the given positions to one
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        public static ulong SetBitsToOneAt(this ulong bitPattern, params int[] bitPositions)
        {
            var bitMask = bitPositions.Aggregate(
                0ul,
                (currentBitPattern, bitPosition) => currentBitPattern | (1ul << bitPosition)
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
        public static ulong SetBitAt(this ulong bitPattern, int bitPosition, bool value)
        {
            return 
                value
                    ? bitPattern | (1ul << bitPosition) 
                    : bitPattern & ~(1ul << bitPosition);
        }

        /// <summary>
        /// Inverts the value of the bit at the given position
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        public static ulong InvertBitAt(this ulong bitPattern, int bitPosition)
        {
            return bitPattern ^ (1ul << bitPosition);
        }

        /// <summary>
        /// Inverts the bits at the given positions
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        public static ulong InvertBitsAt(this ulong bitPattern, IEnumerable<int> bitPositions)
        {
            return 
                bitPositions.Aggregate(
                    bitPattern, 
                    (current, bitPosition) => current ^ (1ul << bitPosition)
                );
        }

        /// <summary>
        /// Inverts the bits at the given positions
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        public static ulong InvertBitsAt(this ulong bitPattern, params int[] bitPositions)
        {
            return
                bitPositions.Aggregate(
                    bitPattern,
                    (current, bitPosition) => current ^ (1ul << bitPosition)
                );
        }


        /// <summary>
        /// Returns true or false depending on the value of the bit at the given position being 1ul or 0ul.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        public static bool BitToBoolean(this ulong bitPattern, int bitPosition)
        {
            return ((1ul << bitPosition) & bitPattern) != 0ul;
        }

        /// <summary>
        /// Reverse the order of bits in the given bit pattern
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitsCount"></param>
        /// <returns></returns>
        public static ulong ReverseBits(this ulong bitPattern, int bitsCount)
        {
            Debug.Assert(
                bitsCount > 0 && 
                bitPattern < (1ul << (bitsCount + 1))
            );

            var result = 0ul;

            var i = bitsCount - 1;
            while (bitPattern != 0ul)
            {
                if ((bitPattern & 1ul) != 0ul)
                    result |= (1ul << i);

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
        public static ulong CreateFullMask(int bitsCount)
        {
            return (1ul << bitsCount) - 1ul;
        }

        /// <summary>
        /// Given two bit patterns and a bit size, this shifts the first pattern by size bits 
        /// to the left and appends the second to combine the two patterns using an OR bitwise operation.
        /// For example 1010.AppendPattern(5, 0001) gives 1010 0ul 0001
        /// </summary>
        /// <param name="bitPattern1"></param>
        /// <param name="bitPattern2"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static ulong AppendPattern(this ulong bitPattern1, int size, ulong bitPattern2)
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
        public static ulong MergeWithPattern(this ulong bitPattern1, ulong bitMask, ulong bitPattern2)
        {
            return bitPattern1 ^ ((bitPattern1 ^ bitPattern2) & bitMask);
        }


        /// <summary>
        /// Gets the numbers between two patterns.
        /// </summary>
        /// <param name="firstPattern"></param>
        /// <param name="secondPattern"></param>
        /// <returns></returns>
        public static IEnumerable<ulong> PatternsBetween(ulong firstPattern, ulong secondPattern)
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
        /// Suppose we have a pattern of N bits set to 1ul in an integer and we want the next permutation 
        /// of N (1ul bits) in a lexicographical sense. For example, if N is 3 and the bit pattern is 00010011, 
        /// the next patterns would be 00010101, 00010110, 00011001,00011010, 00011100, 00100011, and so forth.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static ulong NextPermutation(this ulong bitPattern)
        {
            var tempPattern = (bitPattern | (bitPattern - 1UL)) + 1UL;
            var tempPattern1 = tempPattern & (ulong)(-(long)tempPattern);
            var tempPattern2 = bitPattern & (ulong)(-(long)bitPattern);

            return tempPattern | (((tempPattern1 / tempPattern2) >> 1) - 1UL);
        }

        /// <summary>
        /// Constructs all distinct patterns of fixed size containing a given number of ones
        /// </summary>
        /// <param name="patternSize"></param>
        /// <param name="onesCount"></param>
        /// <returns></returns>
        public static IEnumerable<ulong> OnesPermutations(int patternSize, int onesCount)
        {
            var startPattern = CreateFullMask(onesCount);

            if (patternSize <= onesCount)
            {
                yield return startPattern;
                yield break;
            }

            var bitMask = CreateFullMask(patternSize);

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
        public static IEnumerable<ulong> ZerosPermutations(int patternSize, int zerosCount)
        {
            var startPattern = CreateFullMask(zerosCount);

            if (patternSize <= zerosCount)
            {
                yield return 0ul;
                yield break;
            }

            var bitMask = CreateFullMask(patternSize);

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
        public static IEnumerable<bool> PatternToBooleans(this ulong bitPattern)
        {
            while (bitPattern > 0ul)
            {
                yield return ((bitPattern & 1ul) != 0ul);

                bitPattern >>= 1;
            }
        }

        /// <summary>
        /// Converts the given pattern into a list of boolean values with a given number of items
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitsCount"></param>
        /// <returns></returns>
        public static IEnumerable<bool> PatternToBooleans(this ulong bitPattern, int bitsCount)
        {
            while (bitPattern > 0ul && bitsCount > 0)
            {
                yield return ((bitPattern & 1ul) != 0ul);

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
        public static IEnumerable<int> PatternToPositions(this ulong bitPattern)
        {
            var bitPosition = 0;

            while (bitPattern > 0ul)
            {
                if ((bitPattern & 1ul) != 0ul)
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
        public static IEnumerable<T> PatternToSequence<T>(this ulong bitPattern, int bitsCount, T zeroItem, T oneItem)
        {
            while (bitPattern > 0ul && bitsCount > 0)
            {
                yield return ((bitPattern & 1ul) != 0ul) ? oneItem : zeroItem;

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
        public static IEnumerable<T> PatternToSequence<T>(this ulong bitPattern, T zeroItem, T oneItem)
        {
            while (bitPattern > 0ul)
            {
                yield return ((bitPattern & 1ul) != 0ul) ? oneItem : zeroItem;

                bitPattern >>= 1;
            }
        }

        /// <summary>
        /// Converts the given bit pattern into a string of '1ul' and '0ul' characters with the MSB at the 
        /// first character and the LSB at the last character
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static string PatternToString(this ulong bitPattern)
        {
            //return Convert.ToString(bitPattern, 2);

            if (bitPattern == 0ul) return "0";

            var charsArray = PatternToSequence(bitPattern, '0', '1').ToArray();

            var s = new StringBuilder(charsArray.Length);

            for (var i = charsArray.Length - 1; i >= 0; i--)
                s.Append(charsArray[i]);

            return s.ToString();
        }

        ///// <summary>
        ///// Converts the given bit pattern into a string of '1ul' and '0ul' characters with the MSB at the 
        ///// first character and the LSB at the last character
        ///// </summary>
        ///// <param name="bitPattern"></param>
        ///// <param name="bitsCount"></param>
        ///// <returns></returns>
        //public static string PatternToString(this ulong bitPattern, int bitsCount)
        //{
        //    return Convert.ToString(bitPattern, 2).PadLeft(bitsCount, '0');

        //    //var charsArray = PatternToSequence(bitPattern, bitsCount, '0ul', '1ul').ToArray();

        //    //var s = new StringBuilder(charsArray.Length);

        //    //for (var i = charsArray.Length - 1ul; i >= 0ul; i--)
        //    //    s.Append(charsArray[i]);

        //    //return s.ToString();
        //}

        /// <summary>
        /// Converts the given bit pattern into a string of '1ul' and '0ul' characters with the MSB at the 
        /// first character and the LSB at the last character
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitsCount"></param>
        /// <returns></returns>
        public static string PatternToString(this ulong bitPattern, int bitsCount)
        {
            var s = new StringBuilder(bitsCount);

            for (var i = bitsCount - 1; i >= 0; i--)
                s.Append((bitPattern & (1ul << i)) == 0ul ? '0' : '1');

            return s.ToString();
        }

        public static string PatternToStringPadRight(this ulong bitPattern, int bitsCount, int leftBitsCount, char paddingChar = '-')
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
        public static IEnumerable<T> PickItemsUsingPattern<T>(this IEnumerable<T> items, ulong bitPattern)
        {
            var bitPosition = 0;

            return
                items
                    .Take(MaxBitPosition)
                    .Where(item => ((1ul << (bitPosition++)) & bitPattern) != 0ul);
        }

        /// <summary>
        /// Picks elements of the given sequence based on the given pattern.
        /// For example new [] {"a", "b", "c", "d"}.PickUsingPattern(9) gives the sequence {"a", "d"}.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<ulong, T>> PickIndexedItemsUsingPattern<T>(this IEnumerable<T> items, ulong bitPattern)
        {
            var bitPosition = 0;

            return
                items
                    .Take(MaxBitPosition)
                    .Select((item, index) => new KeyValuePair<ulong, T>((ulong)index, item))
                    .Where(item => ((1ul << (bitPosition++)) & bitPattern) != 0ul);
        }


        /// <summary>
        /// Converts a sequence of boolean values into a bit pattern. The first item in the sequence
        /// corresponds to a 1ul or 0ul in the LSB of the pattern and so on.
        /// </summary>
        /// <param name="boolsSeq"></param>
        /// <returns></returns>
        public static ulong BooleansToPattern(this IEnumerable<bool> boolsSeq)
        {
            var bitPosition = 0;

            return
                boolsSeq
                    .Take(MaxBitPosition)
                    .Aggregate(
                        0ul,
                        (currentBitPattern, item) =>
                        {
                            var newPattern = 
                                item 
                                    ? ((1ul << bitPosition) | currentBitPattern) 
                                    : currentBitPattern;

                            bitPosition++;

                            return newPattern;
                        }
                    );
        }

        /// <summary>
        /// Converts a sequence of boolean values into a bit pattern. The first item in the sequence
        /// corresponds to a 1ul or 0ul in the LSB of the pattern and so on.
        /// </summary>
        /// <param name="boolsSeq"></param>
        /// <returns></returns>
        public static ulong BooleansToPattern(params bool[] boolsSeq)
        {
            var bitPosition = 0;

            return
                boolsSeq
                    .Take(MaxBitPosition)
                    .Aggregate(
                        0ul,
                        (currentBitPattern, item) =>
                        {
                            var newPattern =
                                item
                                    ? ((1ul << bitPosition) | currentBitPattern)
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
        public static ulong PositionsToPattern(this IEnumerable<int> bitPositions)
        {
            return 
                bitPositions.Aggregate(
                    0ul, 
                    (currentBitPattern, bitPosition) => currentBitPattern | (1ul << bitPosition)
                );
        }

        /// <summary>
        /// Returns a bit pattern containing ones at the positions given in a list of bit positions
        /// </summary>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        public static ulong PositionsToPattern(params int[] bitPositions)
        {
            return
                bitPositions.Aggregate(
                    0ul,
                    (currentBitPattern, bitPosition) => currentBitPattern | (1ul << bitPosition)
                );
        }

        /// <summary>
        /// Converts the given string into a bit pattern by putting a 1ul at the positions corresponding to
        /// where the '1ul' character is found in the string. The MSB in the pattern is the first character.
        /// </summary>
        /// <param name="binaryString"></param>
        /// <returns></returns>
        public static ulong StringToUnsignedLongPattern(this string binaryString)
        {
            if (binaryString.Length > MaxBitPatternSize)
                throw new InvalidOperationException();

            var id = 0ul;

            foreach (var c in binaryString)
            {
                id <<= 1;

                if (c == '1') 
                    id |= 1ul;
            }

            return id;
        }

        /// <summary>
        /// Converts the given string into a bit pattern by putting a 1ul at the positions corresponding to
        /// where the oneChar character is found in the string. The MSB in the pattern is the first character
        /// of the string while the LSB is the last character.
        /// </summary>
        /// <param name="binaryString"></param>
        /// <param name="oneChar"></param>
        /// <returns></returns>
        public static ulong StringToPattern(this string binaryString, char oneChar)
        {
            if (binaryString.Length > MaxBitPatternSize)
                throw new InvalidOperationException();

            var id = 0ul;

            foreach (var c in binaryString)
            {
                id <<= 1;

                if (c == oneChar) 
                    id |= 1ul;
            }

            return id;
        }


        /// <summary>
        /// Returns true if the given bit pattern is a sub pattern of the other input.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="superPattern"></param>
        /// <returns></returns>
        public static bool IsSubPatternOf(this ulong bitPattern, ulong superPattern)
        {
            return (superPattern | bitPattern) == superPattern;
        }

        /// <summary>
        /// Returns true if the given bit pattern is a proper sub pattern of the other input.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="superPattern"></param>
        /// <returns></returns>
        public static bool IsProperSubPatternOf(this ulong bitPattern, ulong superPattern)
        {
            return
                bitPattern != 0ul &&
                bitPattern != superPattern &&
                (superPattern | bitPattern) == superPattern;
        }

        /// <summary>
        /// Returns true if the given bit pattern is a super pattern of the other input.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="subPattern"></param>
        /// <returns></returns>
        public static bool IsSuperPatternOf(this ulong bitPattern, ulong subPattern)
        {
            return (subPattern | bitPattern) == bitPattern;
        }

        /// <summary>
        /// Returns true if the given bit pattern is a proper super pattern of the other input.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="subPattern"></param>
        /// <returns></returns>
        public static bool IsProperSuperPatternOf(this ulong bitPattern, ulong subPattern)
        {
            return
                subPattern != 0ul &&
                bitPattern != subPattern &&
                (subPattern | bitPattern) == bitPattern;
        }

        /// <summary>
        /// Split the given pattern into its basic patterns each containing a single 1ul.
        /// For example GetBasicPatterns(14) gives {2, 4, 8}
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static IEnumerable<ulong> GetBasicPatterns(this ulong bitPattern)
        {
            var bitPosition = 0;

            while (bitPattern > 0ul)
            {
                if ((bitPattern & 1ul) != 0ul)
                    yield return 1ul << bitPosition;

                bitPosition++;
                bitPattern >>= 1;
            }
        }

        /// <summary>
        /// Split the given pattern into its sub patterns.
        /// For example GetSubPatterns(11) gives {0ul, 1ul, 2, 3, 8, 9, 10, 11} 
        /// (i.e. the sub-patterns of 1011 are 0000, 0001, 0010, 0011, 1000, 1001, 1010, and 1011)
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static IEnumerable<ulong> GetSubPatterns(this ulong bitPattern)
        {
            //The zero pattern is always a sub-pattern of any bit pattern
            yield return 0ul;

            if (bitPattern == 0ul)
                yield break;

            //Find proper sub patterns that are not zero and nor equal to the original pattern
            var bitPositions = PatternToPositions(bitPattern).ToArray();
            var count = (1ul << bitPositions.Length) - 1ul;

            for (var p = 1ul; p < count; p++)
                yield return PositionsToPattern(
                    bitPositions.PickItemsUsingPattern(p)
                );

            //The original pattern is a sub-pattern of itself
            yield return bitPattern;
        }

        /// <summary>
        /// Split the given pattern into its proper sub-patterns.
        /// For example GetProperSubPatterns(11) gives {1ul, 2, 3, 8, 9, 10} 
        /// (i.e. the proper sub-patterns of 1011 are 0001, 0010, 0011, 1000, 1001, and 1010)
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static IEnumerable<ulong> GetProperSubPatterns(this ulong bitPattern)
        {
            if (bitPattern == 0ul)
                yield break;

            //Find proper sub patterns that are not zero and not equal to the original pattern
            var bitPositions = PatternToPositions(bitPattern).ToArray();
            var count = (1ul << bitPositions.Length) - 1ul;

            for (var p = 1ul; p < count; p++)
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
        public static IEnumerable<ulong> GetSuperPatterns(this ulong bitPattern, int bitsCount)
        {
            var superPattern = CreateFullMask(bitsCount);

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
        public static IEnumerable<ulong> GetProperSuperPatterns(this ulong bitPattern, int bitsCount)
        {
            var superPattern = CreateFullMask(bitsCount);

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
        public static IEnumerable<ulong> GetSuperPatternsInside(this ulong bitPattern, ulong superPattern)
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
        public static IEnumerable<ulong> GetProperSuperPatternsInside(this ulong bitPattern, ulong superPattern)
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
        public static void SplitBySmallestBasicPattern(this ulong bitPattern, out ulong basicPattern, out ulong subPattern)
        {
            if (bitPattern == 0ul)
            {
                basicPattern = 0ul;
                subPattern = 0ul;
                return;
            }

            basicPattern = 1ul << FirstOneBitPosition(bitPattern);
            subPattern = bitPattern ^ basicPattern;
        }

        /// <summary>
        /// Splits ths given pattern into its largest basic pattern and the remaining sub-pattern.
        /// For example the pattern 11010 is split into the basic pattern 10000 and the sub-pattern 01010
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="basicPattern"></param>
        /// <param name="subPattern"></param>
        public static void SplitByLargestBasicPattern(this ulong bitPattern, out ulong basicPattern, out ulong subPattern)
        {
            if (bitPattern == 0ul)
            {
                basicPattern = 0ul;
                subPattern = 0ul;
                return;
            }

            basicPattern = 1ul << LastOneBitPosition(bitPattern);
            subPattern = bitPattern ^ basicPattern;
        }


        /// <summary>
        /// True if the given string consists only of 1ul's and 0ul's and has length lass than 32 charactrers
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidBitPattern(this string s)
        {
            return s.Length <= MaxBitPatternSize && s.All(c => c == '1' || c == '0');
        }

        /// <summary>
        /// True if the given string consists only of 1ul's and 0ul's and has length lass than 32 charactrers and 
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
        public static string ConcatenateUsingPattern(this IEnumerable<string> stringsList, ulong bitPattern, string zeroElement)
        {
            return
                bitPattern == 0ul
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
        public static IEnumerable<string> ConcatenateUsingPatterns(this IEnumerable<string> stringsList, IEnumerable<ulong> bitPatternsList, string zeroElement)
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
        public static string ConcatenateUsingPattern(this IEnumerable<string> stringsList, ulong bitPattern, string zeroElement, string separator)
        {
            return 
                bitPattern == 0ul 
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
        public static string ConcatenateUsingPattern(this IEnumerable<string> stringsList, ulong bitPattern, string zeroElement, string separator, string finalPrefix, string finalSuffix)
        {
            return
                bitPattern == 0ul
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
        public static string ConcatenateUsingPattern(this IEnumerable<string> stringsList, ulong bitPattern, string zeroElement, string separator, string finalPrefix, string finalSuffix, string itemPrefix, string itemSuffix)
        {
            return
                bitPattern == 0ul
                    ? zeroElement
                    : PickItemsUsingPattern(stringsList, bitPattern).ConcatenateText(separator, finalPrefix, finalSuffix, itemPrefix, itemSuffix);
        }
    }
}