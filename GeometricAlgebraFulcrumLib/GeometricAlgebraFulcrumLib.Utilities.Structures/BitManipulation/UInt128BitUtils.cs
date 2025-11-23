using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation
{
    public static class UInt128BitUtils
    {
        /// <summary>
        /// The minimum possible value for a bit position
        /// </summary>
        public static int MinBitPosition => 0;

        /// <summary>
        /// The maximum possible value for a bit position
        /// </summary>
        public static int MaxBitPosition => 127;

        /// <summary>
        /// The largest bit pattern that can be handled
        /// </summary>
        public static int MaxBitPatternSize => 128;


        /// <summary>
        /// Tests if bitPattern is an odd integer
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOdd(this UInt128 bitPattern)
        {
            return (bitPattern & UInt128.One) != 0;
        }

        /// <summary>
        /// Tests if bitPattern is an even integer
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEven(this UInt128 bitPattern)
        {
            return (bitPattern & UInt128.One) == 0;
        }


        /// <summary>
        /// Returns true if this is a basic pattern (i.e. an integer between UInt128.One and 2^30 that is a power of 2)
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBasicPattern(this UInt128 bitPattern)
        {
            return bitPattern != 0 && (bitPattern & (bitPattern - UInt128.One)) == 0;
        }

        /// <summary>
        /// Returns true if this is a zero or basic pattern (i.e. an integer between UInt128.Zero and 2^30 that is a power of 2)
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroOrBasicPattern(this UInt128 bitPattern)
        {
            return (bitPattern & (bitPattern - UInt128.One)) == 0;
        }

        /// <summary>
        /// Tests if the bit at the given position is a one
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOneAt(this UInt128 bitPattern, int bitPosition)
        {
            return ((UInt128.One << bitPosition) & bitPattern) != 0;
        }

        /// <summary>
        /// Tests if the bit at the given position is a zero
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZeroAt(this UInt128 bitPattern, int bitPosition)
        {
            return ((UInt128.One << bitPosition) & bitPattern) == 0;
        }


        /// <summary>
        /// Count the number of ones in the given bit pattern
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CountOnes(this UInt128 bitPattern)
        {
            return (int)UInt128.PopCount(bitPattern);
        }

        /// <summary>
        /// Return true if the number of ones in the given bit pattern is
        /// equal to the given count
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="onesCount"></param>
        /// <returns></returns>
        public static bool CountOnesEquals(this UInt128 bitPattern, int onesCount)
        {
            return (int)UInt128.PopCount(bitPattern) == onesCount;
        }

        /// <summary>
        /// Returns the bit position of the first one bit in the given pattern.
        /// If the pattern is zero this returns -1.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FirstOneBitPosition(this UInt128 bitPattern)
        {
            return bitPattern == 0
                ? -1
                : (int)UInt128.TrailingZeroCount(bitPattern);
        }

        /// <summary>
        /// Returns the bit position of the last one bit in the given pattern. If the pattern is zero this
        /// returns -UInt128.One.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LastOneBitPosition(this UInt128 bitPattern)
        {
            return bitPattern == 0
                ? -1
                : 127 - (int)UInt128.LeadingZeroCount(bitPattern);

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<int> FirstLastOneBitPosition(this UInt128 bitPattern)
        {
            return bitPattern == 0
                ? new Pair<int>(-1, -1)
                : new Pair<int>(
                    (int)UInt128.TrailingZeroCount(bitPattern),
                    127 - (int)UInt128.LeadingZeroCount(bitPattern)
                );
        }

        /// <summary>
        /// Returns the largest integer that is a power of 2 (i.e. UInt128.One, 2, 4, 8, 16, etc.)
        /// that is smaller or equal to the given integer. If the given integer is 0
        /// this returns 0.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 Power2LowerLimit(this UInt128 bitPattern)
        {
            if (bitPattern == UInt128.Zero)
                return UInt128.Zero;

            if ((bitPattern >> 127) != 0)
                return UInt128.One << 127;

            var n = bitPattern;

            n--;
            n |= n >> 1;
            n |= n >> 2;
            n |= n >> 4;
            n |= n >> 8;
            n |= n >> 16;
            n |= n >> 32;
            n |= n >> 64;
            n++;

            return n == bitPattern ? n : (n >> 1);
        }

        /// <summary>
        /// Returns the smallest integer that is a power of 2 (i.e. UInt128.One, 2, 4, 8, 16, etc.)
        /// that is larger or equal to the given integer. If the given integer is UInt128.Zero
        /// this returns 0.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 Power2UpperLimit(this UInt128 bitPattern)
        {
            if (bitPattern == 0)
                return 0;

            bitPattern--;
            bitPattern |= bitPattern >> 1;
            bitPattern |= bitPattern >> 2;
            bitPattern |= bitPattern >> 4;
            bitPattern |= bitPattern >> 8;
            bitPattern |= bitPattern >> 16;
            bitPattern |= bitPattern >> 32;
            bitPattern |= bitPattern >> 64;
            bitPattern++;

            return bitPattern;
        }

        /// <summary>
        /// Create a full mask that contains the given pattern
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 PatternToMask(this UInt128 bitPattern)
        {
            if (bitPattern == 0)
                return 0;

            var bitsMask = bitPattern.Power2LowerLimit();

            return bitsMask | (bitsMask - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 ShiftOnes(this UInt128 bitPattern, int offset)
        {
            if (bitPattern == UInt128.Zero || offset == 0)
                return bitPattern;

            if (offset > 0)
                return (int)UInt128.LeadingZeroCount(bitPattern) >= offset
                    ? (bitPattern << offset)
                    : throw new InvalidOperationException();

            offset = -offset;

            return (int)UInt128.TrailingZeroCount(bitPattern) >= offset
                ? (bitPattern >> offset)
                : throw new InvalidOperationException();
        }


        /// <summary>
        /// Sets the bit at the given position to zero
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 SetBitToZeroAt(this UInt128 bitPattern, int bitPosition)
        {
            return bitPattern & ~(UInt128.One << bitPosition);
        }

        /// <summary>
        /// Sets the bits at the given positions to zero
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 SetBitsToZeroAt(this UInt128 bitPattern, IEnumerable<int> bitPositions)
        {
            var bitMask = bitPositions.Aggregate(
                UInt128.Zero,
                (currentBitPattern, bitPosition) => currentBitPattern | (UInt128.One << bitPosition)
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
        public static UInt128 SetBitsToZeroAt(this UInt128 bitPattern, params int[] bitPositions)
        {
            var bitMask = bitPositions.Aggregate(
                UInt128.Zero,
                (currentBitPattern, bitPosition) => currentBitPattern | (UInt128.One << bitPosition)
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
        public static UInt128 SetBitToOneAt(this UInt128 bitPattern, int bitPosition)
        {
            return bitPattern | (UInt128.One << bitPosition);
        }

        /// <summary>
        /// Sets the bits at the given positions to one
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 SetBitsToOneAt(this UInt128 bitPattern, IEnumerable<int> bitPositions)
        {
            var bitMask = bitPositions.Aggregate(
                UInt128.Zero,
                (currentBitPattern, bitPosition) => currentBitPattern | (UInt128.One << bitPosition)
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
        public static UInt128 SetBitsToOneAt(this UInt128 bitPattern, params int[] bitPositions)
        {
            var bitMask = bitPositions.Aggregate(
                UInt128.Zero,
                (currentBitPattern, bitPosition) => currentBitPattern | (UInt128.One << bitPosition)
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
        public static UInt128 SetBitAt(this UInt128 bitPattern, int bitPosition, bool value)
        {
            return value
                ? bitPattern | (UInt128.One << bitPosition)
                : bitPattern & ~(UInt128.One << bitPosition);
        }

        /// <summary>
        /// Inverts the value of the bit at the given position
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 InvertBitAt(this UInt128 bitPattern, int bitPosition)
        {
            return bitPattern ^ (UInt128.One << bitPosition);
        }

        /// <summary>
        /// Inverts the bits at the given positions
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 InvertBitsAt(this UInt128 bitPattern, IEnumerable<int> bitPositions)
        {
            return bitPositions.Aggregate(
                bitPattern,
                (current, bitPosition) => current ^ (UInt128.One << bitPosition)
            );
        }

        /// <summary>
        /// Inverts the bits at the given positions
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 InvertBitsAt(this UInt128 bitPattern, params int[] bitPositions)
        {
            return bitPositions.Aggregate(
                bitPattern,
                (current, bitPosition) => current ^ (UInt128.One << bitPosition)
            );
        }


        /// <summary>
        /// Returns true or false depending on the value of the bit at the given position being UInt128.One or UInt128.Zero.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitPosition"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BitToBoolean(this UInt128 bitPattern, int bitPosition)
        {
            return ((UInt128.One << bitPosition) & bitPattern) != UInt128.Zero;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 ComplementBits(this UInt128 bitPattern)
        {
            return ~bitPattern;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 ComplementBits(this UInt128 bitPattern, int bitCount)
        {
            return ~bitPattern & bitCount.CreateBitMaskUInt128();
        }

        /// <summary>
        /// Reverse all 128 bits of input UInt128.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 ReverseBits(this UInt128 bitPattern)
        {
            var lo = (ulong)bitPattern;
            var hi = (ulong)(bitPattern >> 64);

            var rLo = (UInt128)hi.ReverseBits(); // high becomes low
            var rHi = (UInt128)lo.ReverseBits(); // low becomes high

            return (rLo << 64) | rHi;
        }

        /// <summary>
        /// Reverse lowest n bits of input UInt128.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 ReversePrefixBits(this UInt128 bitPattern, int n)
        {
            if (n is < 0 or > 128)
                throw new ArgumentOutOfRangeException(nameof(n));

            if (n <= 1)
                return bitPattern;

            var mask = n == 128 ? UInt128.MaxValue : (UInt128.One << n) - 1;
            var prefix = bitPattern & mask;

            var reversed = ReverseBits(prefix);
            reversed >>= 128 - n;

            bitPattern &= ~mask;
            bitPattern |= reversed;

            return bitPattern;
        }

        /// <summary>
        /// Reverse highest n bits of input UInt128.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 ReverseSuffixBits(this UInt128 bitPattern, int n)
        {
            if (n is < 0 or > 128)
                throw new ArgumentOutOfRangeException(nameof(n));

            if (n <= 1)
                return bitPattern;

            var shift = 128 - n;
            var mask = n == 128 ? UInt128.MaxValue : UInt128.MaxValue << shift;

            var suffix = (bitPattern & mask) >> shift;
            var reversed = ReverseBits(suffix);
            reversed >>= 128 - n;

            bitPattern &= ~mask;
            bitPattern |= reversed << shift;

            return bitPattern;
        }

        /// <summary>
        /// Reverse bits within an arbitrary slice [start..end], inclusive (0 = LSB).
        /// Optimized so [0..127] simply calls ReverseBits().
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 ReverseSliceBits(this UInt128 bitPattern, int start, int end)
        {
            if (start < 0 || end > 127 || start > end)
                throw new ArgumentOutOfRangeException($"Invalid slice [{start}..{end}]");

            var len = end - start + 1;
            if (len == 1)
                return bitPattern;

            if (start == 0 && end == 127)
                return ReverseBits(bitPattern);

            // Extract slice
            var mask = ((UInt128.One << len) - 1) << start;
            var slice = (bitPattern & mask) >> start;

            // Reverse slice
            var reversed = ReverseBits(slice);
            reversed >>= (128 - len);

            // Reinsert
            bitPattern &= ~mask;
            bitPattern |= (reversed << start);

            return bitPattern;
        }


        /// <summary>
        /// Create a mask containing a number of ones as given. 
        /// For example CreateFullMask(3) returns 111 (i.e. 7 decimal)
        /// </summary>
        /// <param name="bitsCount"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 CreateBitMaskUInt128(this int bitsCount)
        {
            Debug.Assert(bitsCount is >= 0 and <= 128);

            return bitsCount switch
            {
                0 => UInt128.Zero,
                128 => UInt128.MaxValue,
                _ => (UInt128.One << bitsCount) - 1
            };
        }

        /// <summary>
        /// Create a mask where all positions between the given are set to 1
        /// </summary>
        /// <param name="bitPosition1"></param>
        /// <param name="bitPosition2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 CreateMask(int bitPosition1, int bitPosition2)
        {
            var bitsCount = bitPosition2 - bitPosition1;

            return ((UInt128.One << bitsCount) - 1) << bitPosition1;
        }



        /// <summary>
        /// Given two bit patterns and a bit size, this shifts the first pattern by size bits 
        /// to the left and appends the second to combine the two patterns using an OR bitwise operation.
        /// For example 1010.AppendPattern(5, 0001) gives 1010 UInt128.Zero 0001
        /// </summary>
        /// <param name="bitPattern1"></param>
        /// <param name="bitPattern2"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 AppendPattern(this UInt128 bitPattern1, int size, UInt128 bitPattern2)
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
        public static UInt128 MergeWithPattern(this UInt128 bitPattern1, UInt128 bitMask, UInt128 bitPattern2)
        {
            return bitPattern1 ^ ((bitPattern1 ^ bitPattern2) & bitMask);
        }


        /// <summary>
        /// Gets the numbers between two patterns.
        /// </summary>
        /// <param name="firstPattern"></param>
        /// <param name="secondPattern"></param>
        /// <returns></returns>
        public static IEnumerable<UInt128> PatternsBetween(UInt128 firstPattern, UInt128 secondPattern)
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
        /// Suppose we have a pattern of N bits set to UInt128.One in an integer and we want the next permutation 
        /// of N (UInt128.One bits) in a lexicographical sense. For example, if N is 3 and the bit pattern is 00010011, 
        /// the next patterns would be 00010101, 00010110, 00011001,00011010, 00011100, 00100011, and so forth.
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 NextPermutation(this UInt128 bitPattern)
        {
            var tempPattern = (bitPattern | (bitPattern - UInt128.One)) + UInt128.One;
            var tempPattern1 = tempPattern & (UInt128)(-(long)tempPattern);
            var tempPattern2 = bitPattern & (UInt128)(-(long)bitPattern);

            return tempPattern | (((tempPattern1 / tempPattern2) >> 1) - UInt128.One);
        }

        /// <summary>
        /// Constructs all distinct patterns of fixed size containing a given number of ones
        /// </summary>
        /// <param name="patternSize"></param>
        /// <param name="onesCount"></param>
        /// <returns></returns>
        public static IEnumerable<UInt128> OnesPermutations(int patternSize, int onesCount)
        {
            var startPattern = CreateBitMaskUInt128(onesCount);

            if (patternSize <= onesCount)
            {
                yield return startPattern;
                yield break;
            }

            var bitMask = CreateBitMaskUInt128(patternSize);

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
        public static IEnumerable<UInt128> ZerosPermutations(int patternSize, int zerosCount)
        {
            var startPattern = CreateBitMaskUInt128(zerosCount);

            if (patternSize <= zerosCount)
            {
                yield return UInt128.Zero;
                yield break;
            }

            var bitMask = CreateBitMaskUInt128(patternSize);

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
        public static IEnumerable<bool> PatternToBooleans(this UInt128 bitPattern)
        {
            while (bitPattern > UInt128.Zero)
            {
                yield return ((bitPattern & UInt128.One) != UInt128.Zero);

                bitPattern >>= 1;
            }
        }

        /// <summary>
        /// Converts the given pattern into a list of boolean values with a given number of items
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitsCount"></param>
        /// <returns></returns>
        public static IEnumerable<bool> PatternToBooleans(this UInt128 bitPattern, int bitsCount)
        {
            while (bitPattern > UInt128.Zero && bitsCount > 0)
            {
                yield return ((bitPattern & UInt128.One) != UInt128.Zero);

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
        public static IEnumerable<int> GetSetBitPositions(this UInt128 bitPattern)
        {
            while (bitPattern != 0)
            {
                yield return (int)UInt128.TrailingZeroCount(bitPattern);

                bitPattern &= bitPattern - 1; // Clear the least significant bit
            }
        }

        /// <summary>
        /// Converts an UInt128 bit pattern into an array containing the positions of set bits (1s),
        /// ordered from the least significant bit (0 min) to most (127 max).
        /// </summary>
        public static int[] GetSetBitPositionsAsArray(this UInt128 value)
        {
            var n = (int)UInt128.PopCount(value);
            var indexArray = new int[n];
            var count = 0;

            while (value != 0)
            {
                var position = (int)UInt128.TrailingZeroCount(value);
                indexArray[count++] = position;
                value &= value - 1; // Clear the least significant bit
            }

            return indexArray;
        }

        /// <summary>
        /// Converts an UInt128 bit pattern into a ReadOnlySpan containing the positions of set bits (1s),
        /// ordered from the least significant bit (0 min) to most (127 max).
        /// </summary>
        public static ReadOnlySpan<int> GetSetBitPositionsAsSpan(this UInt128 value)
        {
            var n = (int)UInt128.PopCount(value);
            Span<int> buffer = new int[n];
            var count = 0;

            while (value != 0)
            {
                var position = (int)UInt128.TrailingZeroCount(value);
                buffer[count++] = position;
                value &= value - 1; // Clear the least significant bit
            }

            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetSetBitMappedPositions<T>(this UInt128 bitPattern, Func<int, T> indexMapping)
        {
            return bitPattern.GetSetBitPositions().Select(indexMapping);
        }

        /// <summary>
        /// Returns a list of bit positions where ones are present in the given bit pattern
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetSetBitPositionsReversed(this UInt128 bitPattern)
        {
            if (bitPattern == 0) yield break;

            var bitPosition1 = (int)UInt128.TrailingZeroCount(bitPattern);
            var bitPosition2 = 127 - (int)UInt128.LeadingZeroCount(bitPattern);

            if (bitPosition1 == bitPosition2)
            {
                if ((bitPattern & (UInt128.One << bitPosition1)) != 0)
                    yield return bitPosition1;
            }
            else
            {
                for (var bitPosition = bitPosition2; bitPosition >= bitPosition2; bitPosition--)
                {
                    if ((bitPattern & (UInt128.One << bitPosition)) != UInt128.Zero)
                        yield return bitPosition;
                }
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
        public static IEnumerable<T> PatternToSequence<T>(this UInt128 bitPattern, int bitsCount, T zeroItem, T oneItem)
        {
            while (bitPattern > UInt128.Zero && bitsCount > 0)
            {
                yield return ((bitPattern & UInt128.One) != UInt128.Zero) ? oneItem : zeroItem;

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
        public static IEnumerable<T> PatternToSequence<T>(this UInt128 bitPattern, T zeroItem, T oneItem)
        {
            while (bitPattern > UInt128.Zero)
            {
                yield return ((bitPattern & UInt128.One) != UInt128.Zero) ? oneItem : zeroItem;

                bitPattern >>= 1;
            }
        }

        /// <summary>
        /// Converts the given bit pattern into a string of 'UInt128.One' and 'UInt128.Zero' characters with the MSB at the 
        /// first character and the LSB at the last character
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static string PatternToString(this UInt128 bitPattern)
        {
            if (bitPattern == UInt128.Zero)
                return "0";

            var charsArray =
                PatternToSequence(bitPattern, '0', '1').ToArray();

            var s = new StringBuilder(charsArray.Length);

            for (var i = charsArray.Length - 1; i >= 0; i--)
                s.Append(charsArray[i]);

            return s.ToString();
        }

        ///// <summary>
        ///// Converts the given bit pattern into a string of 'UInt128.One' and 'UInt128.Zero' characters with the MSB at the 
        ///// first character and the LSB at the last character
        ///// </summary>
        ///// <param name="bitPattern"></param>
        ///// <param name="bitsCount"></param>
        ///// <returns></returns>
        //public static string PatternToString(this UInt128 bitPattern, int bitsCount)
        //{
        //    return Convert.ToString(bitPattern, 2).PadLeft(bitsCount, '0');

        //    //var charsArray = PatternToSequence(bitPattern, bitsCount, 'UInt128.Zero', 'UInt128.One').ToArray();

        //    //var s = new StringBuilder(charsArray.Length);

        //    //for (var i = charsArray.Length - UInt128.One; i >= UInt128.Zero; i--)
        //    //    s.Append(charsArray[i]);

        //    //return s.ToString();
        //}

        /// <summary>
        /// Converts the given bit pattern into a string of 'UInt128.One' and 'UInt128.Zero' characters with the MSB at the 
        /// first character and the LSB at the last character
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <param name="bitsCount"></param>
        /// <returns></returns>
        public static string PatternToString(this UInt128 bitPattern, int bitsCount)
        {
            var s = new StringBuilder(bitsCount);

            for (var i = bitsCount - 1; i >= 0; i--)
                s.Append((bitPattern & (UInt128.One << i)) == UInt128.Zero ? '0' : '1');

            return s.ToString();
        }

        public static string PatternToString(this UInt128 bitPattern, IEnumerable<char> items)
        {
            var bitPosition = 0;

            var charList = items
                .Take(MaxBitPosition)
                .Where(_ => ((UInt128.One << (bitPosition++)) & bitPattern) != UInt128.Zero);

            return string.Concat(charList);
        }

        public static string PatternToStringPadRight(this UInt128 bitPattern, int bitsCount, int leftBitsCount, char paddingChar = '-')
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
        public static IEnumerable<T> PickItemsUsingPattern<T>(this IEnumerable<T> items, UInt128 bitPattern)
        {
            var bitPosition = 0;

            return items
                .Take(MaxBitPosition)
                .Where(_ => ((UInt128.One << (bitPosition++)) & bitPattern) != 0);
        }

        /// <summary>
        /// Picks elements of the given sequence based on the given pattern.
        /// For example new [] {"a", "b", "c", "d"}.PickUsingPattern(9) gives the sequence {"a", "d"}.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<UInt128, T>> PickIndexedItemsUsingPattern<T>(this IEnumerable<T> items, UInt128 bitPattern)
        {
            var bitPosition = 0;

            return items
                .Take(MaxBitPosition)
                .Select((item, index) => new KeyValuePair<UInt128, T>((UInt128)index, item))
                .Where(_ => ((UInt128.One << (bitPosition++)) & bitPattern) != UInt128.Zero);
        }


        /// <summary>
        /// Converts a sequence of boolean values into a bit pattern. The first item in the sequence
        /// corresponds to a UInt128.One or UInt128.Zero in the LSB of the pattern and so on.
        /// </summary>
        /// <param name="boolSeq"></param>
        /// <returns></returns>
        public static UInt128 BooleansToPattern(this IEnumerable<bool> boolSeq)
        {
            var bitPosition = 0;

            return boolSeq
                .Take(MaxBitPosition)
                .Aggregate(
                    UInt128.Zero,
                    (currentBitPattern, item) =>
                    {
                        var newPattern =
                            item
                                ? ((UInt128.One << bitPosition) | currentBitPattern)
                                : currentBitPattern;

                        bitPosition++;

                        return newPattern;
                    }
                );
        }

        /// <summary>
        /// Converts a sequence of boolean values into a bit pattern. The first item in the sequence
        /// corresponds to a UInt128.One or UInt128.Zero in the LSB of the pattern and so on.
        /// </summary>
        /// <param name="boolSeq"></param>
        /// <returns></returns>
        public static UInt128 BooleansToPattern(params bool[] boolSeq)
        {
            var bitPosition = 0;

            return boolSeq
                .Take(MaxBitPosition)
                .Aggregate(
                    UInt128.Zero,
                    (currentBitPattern, item) =>
                    {
                        var newPattern =
                            item
                                ? ((UInt128.One << bitPosition) | currentBitPattern)
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
        public static UInt128 PositionsToPattern(this IEnumerable<int> bitPositions)
        {
            return bitPositions.Aggregate(
                UInt128.Zero,
                (currentBitPattern, bitPosition) => currentBitPattern | (UInt128.One << bitPosition)
            );
        }

        /// <summary>
        /// Returns a bit pattern containing ones at the positions given in a list of bit positions
        /// </summary>
        /// <param name="bitPositions"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 PositionsToPattern(params int[] bitPositions)
        {
            return bitPositions.Aggregate(
                UInt128.Zero,
                (currentBitPattern, bitPosition) => currentBitPattern | (UInt128.One << bitPosition)
            );
        }

        /// <summary>
        /// Converts the given string into a bit pattern by putting a UInt128.One at the positions corresponding to
        /// where the 'UInt128.One' character is found in the string. The MSB in the pattern is the first character.
        /// </summary>
        /// <param name="binaryString"></param>
        /// <returns></returns>
        public static UInt128 StringToUnsignedLongPattern(this string binaryString)
        {
            if (binaryString.Length > MaxBitPatternSize)
                throw new InvalidOperationException();

            var id = UInt128.Zero;

            foreach (var c in binaryString)
            {
                id <<= 1;

                if (c == '1')
                    id |= UInt128.One;
            }

            return id;
        }

        /// <summary>
        /// Converts the given string into a bit pattern by putting a UInt128.One at the positions corresponding to
        /// where the oneChar character is found in the string. The MSB in the pattern is the first character
        /// of the string while the LSB is the last character.
        /// </summary>
        /// <param name="binaryString"></param>
        /// <param name="oneChar"></param>
        /// <returns></returns>
        public static UInt128 StringToPattern(this string binaryString, char oneChar)
        {
            if (binaryString.Length > MaxBitPatternSize)
                throw new InvalidOperationException();

            var id = UInt128.Zero;

            foreach (var c in binaryString)
            {
                id <<= 1;

                if (c == oneChar)
                    id |= UInt128.One;
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
        public static bool IsSubPatternOf(this UInt128 bitPattern, UInt128 superPattern)
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
        public static bool IsProperSubPatternOf(this UInt128 bitPattern, UInt128 superPattern)
        {
            return
                bitPattern != UInt128.Zero &&
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
        public static bool IsSuperPatternOf(this UInt128 bitPattern, UInt128 subPattern)
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
        public static bool IsProperSuperPatternOf(this UInt128 bitPattern, UInt128 subPattern)
        {
            return
                subPattern != UInt128.Zero &&
                bitPattern != subPattern &&
                (subPattern | bitPattern) == bitPattern;
        }

        /// <summary>
        /// Split the given pattern into its basic patterns each containing a single UInt128.One.
        /// For example GetBasicPatterns(14) gives {2, 4, 8}
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static IEnumerable<UInt128> GetBasicPatterns(this UInt128 bitPattern)
        {
            var bitPosition = 0;

            while (bitPattern > UInt128.Zero)
            {
                if ((bitPattern & UInt128.One) != UInt128.Zero)
                    yield return UInt128.One << bitPosition;

                bitPosition++;
                bitPattern >>= 1;
            }
        }

        /// <summary>
        /// Split the given pattern into its sub patterns.
        /// For example GetSubPatterns(11) gives {UInt128.Zero, UInt128.One, 2, 3, 8, 9, 10, 11} 
        /// (i.e. the sub-patterns of 1011 are 0000, 0001, 0010, 0011, 1000, 1001, 1010, and 1011)
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static IEnumerable<UInt128> GetSubPatterns(this UInt128 bitPattern)
        {
            //The zero pattern is always a sub-pattern of any bit pattern
            yield return UInt128.Zero;

            if (bitPattern == UInt128.Zero)
                yield break;

            //Find proper sub patterns that are not zero and nor equal to the original pattern
            var bitPositions = GetSetBitPositions(bitPattern).ToArray();
            var count = (UInt128.One << bitPositions.Length) - UInt128.One;

            for (var p = UInt128.One; p < count; p++)
                yield return PositionsToPattern(
                    bitPositions.PickItemsUsingPattern(p)
                );

            //The original pattern is a sub-pattern of itself
            yield return bitPattern;
        }

        /// <summary>
        /// Split the given pattern into its proper sub-patterns.
        /// For example GetProperSubPatterns(11) gives {UInt128.One, 2, 3, 8, 9, 10} 
        /// (i.e. the proper sub-patterns of 1011 are 0001, 0010, 0011, 1000, 1001, and 1010)
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static IEnumerable<UInt128> GetProperSubPatterns(this UInt128 bitPattern)
        {
            if (bitPattern == UInt128.Zero)
                yield break;

            //Find proper sub patterns that are not zero and not equal to the original pattern
            var bitPositions = GetSetBitPositions(bitPattern).ToArray();
            var count = (UInt128.One << bitPositions.Length) - UInt128.One;

            for (var p = UInt128.One; p < count; p++)
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
        public static IEnumerable<UInt128> GetSuperPatterns(this UInt128 bitPattern, int bitsCount)
        {
            var superPattern = CreateBitMaskUInt128(bitsCount);

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
        public static IEnumerable<UInt128> GetProperSuperPatterns(this UInt128 bitPattern, int bitsCount)
        {
            var superPattern = CreateBitMaskUInt128(bitsCount);

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
        public static IEnumerable<UInt128> GetSuperPatternsInside(this UInt128 bitPattern, UInt128 superPattern)
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
        public static IEnumerable<UInt128> GetProperSuperPatternsInside(this UInt128 bitPattern, UInt128 superPattern)
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
        public static void SplitBySmallestBasicPattern(this UInt128 bitPattern, out UInt128 basicPattern, out UInt128 subPattern)
        {
            if (bitPattern == UInt128.Zero)
            {
                basicPattern = UInt128.Zero;
                subPattern = UInt128.Zero;
                return;
            }

            basicPattern = UInt128.One << FirstOneBitPosition(bitPattern);
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
        public static void SplitByLargestBasicPattern(this UInt128 bitPattern, out UInt128 basicPattern, out UInt128 subPattern)
        {
            if (bitPattern == UInt128.Zero)
            {
                basicPattern = UInt128.Zero;
                subPattern = UInt128.Zero;
                return;
            }

            basicPattern = UInt128.One << LastOneBitPosition(bitPattern);
            subPattern = bitPattern ^ basicPattern;
        }


        /// <summary>
        /// True if the given string consists only of UInt128.One's and 0's and has length lass than 128 charactrers
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBitPattern(this string s)
        {
            return s.Length <= MaxBitPatternSize && s.All(c => c is '1' or '0');
        }

        /// <summary>
        /// True if the given string consists only of UInt128.One's and UInt128.Zero's and has length lass than 32 charactrers and 
        /// equal to the given bitsCount
        /// </summary>
        /// <param name="s"></param>
        /// <param name="bitsCount"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBitPattern(this string s, int bitsCount)
        {
            return bitsCount <= MaxBitPatternSize && s.Length == bitsCount && s.All(c => c is '1' or '0');
        }

        /// <summary>
        /// Create a concatenation of strings picked from the given list of strings using the bit pattern 
        /// </summary>
        /// <param name="stringsList"></param>
        /// <param name="bitPattern"></param>
        /// <param name="zeroElement"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ConcatenateUsingPattern(this IEnumerable<string> stringsList, UInt128 bitPattern, string zeroElement)
        {
            return
                bitPattern == UInt128.Zero
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
        public static IEnumerable<string> ConcatenateUsingPatterns(this IEnumerable<string> stringsList, IEnumerable<UInt128> bitPatternsList, string zeroElement)
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
        public static string ConcatenateUsingPattern(this IEnumerable<string> stringsList, UInt128 bitPattern, string zeroElement, string separator)
        {
            return
                bitPattern == UInt128.Zero
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
        public static string ConcatenateUsingPattern(this IEnumerable<string> stringsList, UInt128 bitPattern, string zeroElement, string separator, string finalPrefix, string finalSuffix)
        {
            return
                bitPattern == UInt128.Zero
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
        public static string ConcatenateUsingPattern(this IEnumerable<string> stringsList, UInt128 bitPattern, string zeroElement, string separator, string finalPrefix, string finalSuffix, string itemPrefix, string itemSuffix)
        {
            return
                bitPattern == UInt128.Zero
                    ? zeroElement
                    : PickItemsUsingPattern(stringsList, bitPattern).ConcatenateText(separator, finalPrefix, finalSuffix, itemPrefix, itemSuffix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Repeat<T>(this UInt128 count, T value)
        {
            while (count > 0)
            {
                yield return value;
                count--;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> MapRange<T>(this UInt128 count, Func<UInt128, T> mappingFunc)
        {
            return Enumerable
                .Range(0, (int)count)
                .Select(i => mappingFunc((UInt128)i));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> MapRange<T>(this UInt128 count, UInt128 offset, Func<UInt128, T> mappingFunc)
        {
            return Enumerable
                .Range(0, (int)count)
                .Select(i => mappingFunc(offset + (UInt128)i));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<UInt128, T>> GetMappedRange<T>(this UInt128 count, Func<UInt128, T> mappingFunc)
        {
            return Enumerable
                .Range(0, (int)count)
                .Select(
                    i =>
                    {
                        var key = (UInt128)i;
                        return new KeyValuePair<UInt128, T>(key, mappingFunc(key));
                    }
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<UInt128, T>> GetMappedRange<T>(this UInt128 count, UInt128 offset, Func<UInt128, T> mappingFunc)
        {
            return Enumerable
                .Range(0, (int)count)
                .Select(
                    i =>
                    {
                        var key = offset + (UInt128)i;
                        return new KeyValuePair<UInt128, T>(key, mappingFunc(key));
                    }
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt128> GetRange(this UInt128 count)
        {
            for (var i = UInt128.Zero; i < count; i++)
                yield return i;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt128> GetRange(this UInt128 count, UInt128 offset)
        {
            for (var i = UInt128.Zero; i < count; i++)
                yield return offset + i;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetRange<T>(this UInt128 count, Func<UInt128, T> mappingFunc)
        {
            for (var i = UInt128.Zero; i < count; i++)
                yield return mappingFunc(i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] RangeToArray<T>(this UInt128 count, Func<UInt128, T> keyValueFunc)
        {
            return Enumerable
                .Range(0, (int)count)
                .Select(
                    i => keyValueFunc((UInt128)i)
                ).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<UInt128, T> RangeToDictionary<T>(this UInt128 count, Func<UInt128, T> keyValueFunc)
        {
            return Enumerable
                .Range(0, (int)count)
                .ToDictionary(
                    i => (UInt128)i,
                    i => keyValueFunc((UInt128)i)
                );
        }

        public static bool TryGetMinValue(this IEnumerable<UInt128> valuesList, out UInt128 minValue)
        {
            var foundFlag = false;
            minValue = UInt128.MaxValue;

            foreach (var value in valuesList)
            {
                foundFlag = true;

                if (value < minValue)
                    minValue = value;
            }

            return foundFlag;
        }

        public static bool TryGetMaxValue(this IEnumerable<UInt128> valuesList, out UInt128 maxValue)
        {
            var foundFlag = false;
            maxValue = UInt128.MinValue;

            foreach (var value in valuesList)
            {
                foundFlag = true;

                if (value > maxValue)
                    maxValue = value;
            }

            return foundFlag;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 Min(this UInt128 a, UInt128 b)
        {
            return a <= b ? a : b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 Max(this UInt128 a, UInt128 b)
        {
            return a >= b ? a : b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 Min(this UInt128 a, UInt128 b, UInt128 c)
        {
            return a <= b
                ? (a <= c ? a : c)
                : (b <= c ? b : c);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 Max(this UInt128 a, UInt128 b, UInt128 c)
        {
            return a >= b
                ? (a >= c ? a : c)
                : (b >= c ? b : c);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<UInt128> Sort(UInt128 a, UInt128 b)
        {
            return a <= b
                ? new Pair<UInt128>(a, b)
                : new Pair<UInt128>(b, a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<UInt128> Sort(UInt128 a, UInt128 b, UInt128 c)
        {
            if (a <= b)
            {
                if (b <= c)
                    return new Triplet<UInt128>(a, b, c);

                return a <= c
                    ? new Triplet<UInt128>(a, c, b)
                    : new Triplet<UInt128>(c, a, b);
            }

            if (a <= c)
                return new Triplet<UInt128>(b, a, c);

            return b <= c
                ? new Triplet<UInt128>(b, c, a)
                : new Triplet<UInt128>(c, b, a);
        }


        /// <summary>
        /// Gets the position of the k-th set bit (1) in the bit pattern,
        /// scanning from LSB (bit 0) to MSB (bit 127).
        /// </summary>
        /// <param name="bits">The UInt128 bit pattern</param>
        /// <param name="index">The zero-based index of the bit to find</param>
        /// <returns>The position of the k-th set bit, or null if not enough bits are set.</returns>
        public static int GetNthSetBitPosition(this UInt128 bits, int index)
        {
            var count = 0;
            var offset = 0;

            while (bits != 0)
            {
                var trailingZeros = (int)UInt128.TrailingZeroCount(bits);
                offset += trailingZeros;

                if (count == index)
                    return offset;

                bits >>= trailingZeros + 1;  // Remove the found bit and trailing zeros
                offset++;  // Skip the set bit we just found
                count++;
            }

            throw new IndexOutOfRangeException(); // Not enough set bits
        }

        /// <summary>
        /// Gets the position of the k-th set bit (1) in the bit pattern,
        /// scanning from LSB (bit 0) to MSB (bit 127).
        /// </summary>
        /// <param name="bits">The UInt128 bit pattern</param>
        /// <param name="index">The zero-based index of the bit to find</param>
        /// <returns>The position of the k-th set bit, or null if not enough bits are set.</returns>
        public static int TryGetNthSetBitPosition(this UInt128 bits, int index)
        {
            var count = 0;
            var offset = 0;

            while (bits != 0)
            {
                var trailingZeros = (int)UInt128.TrailingZeroCount(bits);
                offset += trailingZeros;

                if (count == index)
                    return offset;

                bits >>= trailingZeros + 1;  // Remove the found bit and trailing zeros
                offset++;  // Skip the set bit we just found
                count++;
            }

            return -1; // Not enough set bits
        }


        public static IEnumerable<UInt128> GetSubBitPatterns(this UInt128 bitPattern)
        {
            // Step 1: Extract positions of set bits
            var bitPositions = bitPattern.GetSetBitPositionsAsArray();

            var k = bitPositions.Length;
            var totalSubsets = UInt128.One << k;

            // Step 2: Iterate over all masks of size k
            for (var mask = UInt128.Zero; mask < totalSubsets; mask++)
            {
                var subMask = UInt128.Zero;
                for (var j = 0; j < k; j++)
                {
                    if ((mask & (UInt128.One << j)) != 0)
                        subMask |= UInt128.One << bitPositions[j];
                }

                yield return subMask;
            }
        }

        public static IEnumerable<UInt128> GetSubBitPatternsOfSize(this UInt128 bitPattern, int k)
        {
            // Step 1: Extract the positions of all set bits
            var bitPositions = bitPattern.GetSetBitPositionsAsArray();
            var n = bitPositions.Length;

            if (k < 0 || k > n)
                yield break; // Invalid k

            // Step 2: Use combination generator to get all k-length subsets of bit positions
            var indices = new int[k];
            for (var i = 0; i < k; i++)
                indices[i] = i;

            while (true)
            {
                // Build sub-mask
                var subMask = UInt128.Zero;
                for (var i = 0; i < k; i++)
                    subMask |= (UInt128.One << bitPositions[indices[i]]);
                yield return subMask;

                // Move to next combination
                var iPos = k - 1;
                while (iPos >= 0 && indices[iPos] == n - k + iPos)
                    iPos--;

                if (iPos < 0) break;

                indices[iPos]++;
                for (var j = iPos + 1; j < k; j++)
                    indices[j] = indices[j - 1] + 1;
            }
        }

        public static IEnumerable<UInt128> GetSubBitPatternsOfSizeInRange(this UInt128 bitPattern, int minSize, int maxSize)
        {
            // Step 1: Extract positions of set bits
            var bitPositions = bitPattern.GetSetBitPositionsAsArray();

            var n = bitPositions.Length;

            // Clamp min and max size
            minSize = Math.Max(0, minSize);
            maxSize = Math.Min(n, maxSize);

            for (var k = minSize; k <= maxSize; k++)
            {
                if (k == 0)
                {
                    yield return UInt128.Zero;
                    continue;
                }

                // Generate all combinations of size k
                var indices = new int[k];
                for (var i = 0; i < k; i++) indices[i] = i;

                while (true)
                {
                    // Build sub-mask
                    var subMask = UInt128.Zero;
                    for (var i = 0; i < k; i++)
                        subMask |= UInt128.One << bitPositions[indices[i]];
                    yield return subMask;

                    // Move to next combination
                    var iPos = k - 1;
                    while (iPos >= 0 && indices[iPos] == n - k + iPos)
                        iPos--;

                    if (iPos < 0) break;

                    indices[iPos]++;
                    for (var j = iPos + 1; j < k; j++)
                        indices[j] = indices[j - 1] + 1;
                }
            }
        }


        public static IEnumerable<UInt128> GetSubBitPatternsInGrayOrder(this UInt128 bitPattern)
        {
            // Step 1: Extract positions of set bits
            var bitPositions = bitPattern.GetSetBitPositionsAsArray();

            var k = bitPositions.Length;
            if (k == 0)
            {
                yield return UInt128.Zero;
                yield break;
            }

            // Step 2: Generate all Gray codes of length k
            var totalSubsets = UInt128.One << k;
            for (var i = UInt128.Zero; i < totalSubsets; i++)
            {
                // Binary-reflected Gray code: g = i ^ (i >> 1)
                var gray = i ^ (i >> 1);

                // Build bit pattern from Gray code bits
                var subMask = UInt128.Zero;
                for (var j = 0; j < k; j++)
                {
                    if ((gray & (UInt128.One << j)) != 0)
                        subMask |= UInt128.One << bitPositions[j];
                }

                yield return subMask;
            }
        }

        public static IEnumerable<UInt128> GetSubBitPatternsOfSizeInGrayOrder(this UInt128 bitPattern, int k)
        {
            // Step 1: Extract positions of set bits
            var bitPositions = bitPattern.GetSetBitPositionsAsArray();
            var n = bitPositions.Length;

            if (k <= 0 || k > n)
            {
                if (k == 0) yield return UInt128.Zero;
                yield break;
            }

            // Step 2: Use Gray code-based combination generator
            var indices = new int[k];
            for (var i = 0; i < k; i++)
                indices[i] = i;

            var forward = true;
            var first = true;

            while (true)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    // Move to next combination using combinatorial Gray code algorithm
                    int i;
                    if (forward)
                    {
                        if (indices[k - 1] < n - 1)
                        {
                            indices[k - 1]++;
                        }
                        else
                        {
                            forward = false;
                            i = k - 2;
                            while (i >= 0 && indices[i + 1] - indices[i] <= 1)
                                i--;

                            if (i < 0) break;

                            indices[i]++;
                            for (var j = i + 1; j < k; j++)
                                indices[j] = indices[j - 1] + 1;
                        }
                    }
                    else
                    {
                        if (indices[0] > 0)
                        {
                            indices[0]--;
                        }
                        else
                        {
                            forward = true;
                            i = 1;
                            while (i < k && indices[i] - indices[i - 1] <= 1)
                                i++;

                            if (i >= k) break;

                            indices[i]--;
                            for (var j = i - 1; j >= 0; j--)
                                indices[j] = indices[j + 1] - 1;
                        }
                    }
                }

                // Build sub-mask
                var subMask = UInt128.Zero;
                for (var j = 0; j < k; j++)
                    subMask |= UInt128.One << bitPositions[indices[j]];

                yield return subMask;
            }

        }

        public static IEnumerable<UInt128> GetSubBitPatternsOfSizeInRangeInGrayOrder(this UInt128 bitPattern, int minSize, int maxSize)
        {
            // Step 1: Extract positions of set bits
            var bitPositions = bitPattern.GetSetBitPositionsAsArray();
            var n = bitPositions.Length;

            // Clamp min/max size
            minSize = Math.Max(0, minSize);
            maxSize = Math.Min(n, maxSize);

            // Handle empty pattern
            if (n == 0)
            {
                if (minSize <= 0 && maxSize >= 0)
                    yield return UInt128.Zero;
                yield break;
            }

            // Step 2: For each size k in [minSize, maxSize], generate Gray-coded subsets
            for (var k = minSize; k <= maxSize; k++)
            {
                if (k == 0)
                {
                    yield return UInt128.Zero;
                    continue;
                }

                var indices = new int[k];
                for (var i = 0; i < k; i++) indices[i] = i;

                var forward = true;
                var first = true;

                while (true)
                {
                    if (!first)
                    {
                        // Move to next combination using combinatorial Gray code logic
                        int i;
                        if (forward)
                        {
                            if (indices[k - 1] < n - 1)
                            {
                                indices[k - 1]++;
                            }
                            else
                            {
                                forward = false;
                                i = k - 2;
                                while (i >= 0 && indices[i + 1] - indices[i] <= 1)
                                    i--;

                                if (i < 0) break;

                                indices[i]++;
                                for (var j = i + 1; j < k; j++)
                                    indices[j] = indices[j - 1] + 1;
                            }
                        }
                        else
                        {
                            if (indices[0] > 0)
                            {
                                indices[0]--;
                            }
                            else
                            {
                                forward = true;
                                i = 1;
                                while (i < k && indices[i] - indices[i - 1] <= 1)
                                    i++;

                                if (i >= k) break;

                                indices[i]--;
                                for (var j = i - 1; j >= 0; j--)
                                    indices[j] = indices[j + 1] - 1;
                            }
                        }
                    }

                    // Build sub-mask
                    var subMask = UInt128.Zero;
                    for (var j = 0; j < k; j++)
                        subMask |= UInt128.One << bitPositions[indices[j]];

                    yield return subMask;
                    first = false;
                }
            }
        }
    }
}
