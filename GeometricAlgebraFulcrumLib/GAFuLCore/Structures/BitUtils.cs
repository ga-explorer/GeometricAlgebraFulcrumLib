using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GAFuLCore.Structures
{
    public static class BitUtils
    {
        /// <summary>
        /// https://stackoverflow.com/questions/109023/count-the-number-of-set-bits-in-a-32-bit-integer
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int PopCount(this int value)
        {
            value -= (value >> 1) & 0x55555555;
            value = (value & 0x33333333) + ((value >> 2) & 0x33333333);

            return (((value + (value >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
        }

        /// <summary>
        /// https://stackoverflow.com/questions/3815165/how-to-implement-bitcount-using-only-bitwise-operators
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int PopCount(this uint value)
        {
            var c = value - ((value >> 1) & 0x55555555);
            c = ((c >> 2) & 0x33333333) + (c & 0x33333333);
            c = ((c >> 4) + c) & 0x0F0F0F0F;
            c = ((c >> 8) + c) & 0x00FF00FF;
            c = ((c >> 16) + c) & 0x0000FFFF;

            return (int)c;

            //var count = 0;
            //while (value != 0)
            //{
            //    count++;
            //    value &= value - 1;
            //}

            //return count;
        }

        public static int PopCount(this ulong value)
        {
            var count = 0;
            while (value != 0)
            {
                count++;
                value &= value - 1UL;
            }

            return count;
        }

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

        public static ulong PatternToMask(this ulong bitPattern)
        {
            var bitsMask = 0ul;
            var bitPosition = 0;

            while (bitPattern > 0ul)
            {
                bitsMask |= 1ul << bitPosition;

                bitPosition++;
                bitPattern >>= 1;
            }

            return bitsMask;
        }

        /// <summary>
        /// Returns a list of bit positions where ones are present in the given bit pattern
        /// </summary>
        /// <param name="bitPattern"></param>
        /// <returns></returns>
        public static IEnumerable<int> PatternToPositions(this ulong bitPattern)
        {
            if (bitPattern == 0) yield break;

            //var bitPosition1 = BitOperations.TrailingZeroCount(bitPattern);
            //var bitPosition2 = 63 - BitOperations.LeadingZeroCount(bitPattern);

            //if (bitPosition1 == bitPosition2)
            //{
            //    if ((bitPattern & (1UL << bitPosition1)) != 0ul)
            //        yield return bitPosition1;
            //}
            //else
            //{
            //    for (var bitPosition = bitPosition1; bitPosition <= bitPosition2; bitPosition++)
            //    {
            //        if ((bitPattern & (1UL << bitPosition)) != 0ul)
            //            yield return bitPosition;
            //    }
            //}

            var bitPosition = bitPattern.FirstOneBitPosition();

            while (bitPattern > 0)
            {
                if ((bitPattern & 1ul) != 0ul)
                    yield return bitPosition;

                bitPosition++;
                bitPattern >>= 1;
            }
        }

    }
}
