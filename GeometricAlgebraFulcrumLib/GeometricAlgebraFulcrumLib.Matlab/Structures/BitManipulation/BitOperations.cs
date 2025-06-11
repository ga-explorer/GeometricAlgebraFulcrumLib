

// Some routines inspired by the Stanford Bit Twiddling Hacks by Sean Eron Anderson:
// http://graphics.stanford.edu/~seander/bithacks.html
namespace GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation
{
    /// <summary>
    /// Utility methods for intrinsic bit-twiddling operations.
    /// The methods use hardware intrinsics when available on the underlying platform,
    /// otherwise they use optimized software fallbacks.
    /// </summary>
    public static class BitOperations
    {
        /// <summary>
        /// Evaluate whether a given integral value is a power of 2.
        /// </summary>
        /// <param name="value">The value.</param>
        
        public static bool IsPow2(int value) => (value & (value - 1)) == 0 && value > 0;

        /// <summary>
        /// Evaluate whether a given integral value is a power of 2.
        /// </summary>
        /// <param name="value">The value.</param>
        
        
        public static bool IsPow2(uint value) => (value & (value - 1)) == 0 && value != 0;

        /// <summary>
        /// Evaluate whether a given integral value is a power of 2.
        /// </summary>
        /// <param name="value">The value.</param>
        
        public static bool IsPow2(long value) => (value & (value - 1)) == 0 && value > 0;

        /// <summary>
        /// Evaluate whether a given integral value is a power of 2.
        /// </summary>
        /// <param name="value">The value.</param>
        
        
        public static bool IsPow2(ulong value) => (value & (value - 1)) == 0 && value != 0;

        /// <summary>
        /// Evaluate whether a given integral value is a power of 2.
        /// </summary>
        /// <param name="value">The value.</param>
        
        public static bool IsPow2(nint value) => (value & (value - 1)) == 0 && value > 0;

        /// <summary>
        /// Evaluate whether a given integral value is a power of 2.
        /// </summary>
        /// <param name="value">The value.</param>
        
        
        public static bool IsPow2(nuint value) => (value & (value - 1)) == 0 && value != 0;

        /// <summary>Round the given integral value up to a power of 2.</summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The smallest power of 2 which is greater than or equal to <paramref name="value"/>.
        /// If <paramref name="value"/> is 0 or the result overflows, returns 0.
        /// </returns>
        
        
        public static uint RoundUpToPowerOf2(uint value)
        {
            // Based on https://graphics.stanford.edu/~seander/bithacks.html#RoundUpPowerOf2
            --value;
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;
            return value + 1;
        }

        /// <summary>
        /// Round the given integral value up to a power of 2.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The smallest power of 2 which is greater than or equal to <paramref name="value"/>.
        /// If <paramref name="value"/> is 0 or the result overflows, returns 0.
        /// </returns>
        
        
        public static ulong RoundUpToPowerOf2(ulong value)
        {
            // Based on https://graphics.stanford.edu/~seander/bithacks.html#RoundUpPowerOf2
            --value;
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;
            value |= value >> 32;
            return value + 1;
        }

        /// <summary>
        /// Round the given integral value up to a power of 2.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The smallest power of 2 which is greater than or equal to <paramref name="value"/>.
        /// If <paramref name="value"/> is 0 or the result overflows, returns 0.
        /// </returns>
        
        
        public static nuint RoundUpToPowerOf2(nuint value)
        {
#if TARGET_64BIT
            return (nuint)RoundUpToPowerOf2((ulong)value);
#else
            return RoundUpToPowerOf2((uint)value);
#endif
        }

        /// <summary>
        /// Count the number of leading zero bits in a mask.
        /// Similar in behavior to the x86 instruction LZCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        
        public static int LeadingZeroCount(uint value)
        {
            if (value == 0) return 32;

            var count = 0;
            if ((value & 0xFFFF0000U) == 0) { count += 16; value <<= 16; }
            if ((value & 0xFF000000U) == 0) { count += 8;  value <<= 8;  }
            if ((value & 0xF0000000U) == 0) { count += 4;  value <<= 4;  }
            if ((value & 0xC0000000U) == 0) { count += 2;  value <<= 2;  }
            if ((value & 0x80000000U) == 0) { count += 1;              }

            return count;
        }

        /// <summary>
        /// Count the number of leading zero bits in a mask.
        /// Similar in behavior to the x86 instruction LZCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        
        public static int LeadingZeroCount(ulong value)
        {
            if (value == 0) return 64;

            var count = 0;
            if ((value & 0xFFFFFFFF00000000UL) == 0) { count += 32; value <<= 32; }
            if ((value & 0xFFFF000000000000UL) == 0) { count += 16; value <<= 16; }
            if ((value & 0xFF00000000000000UL) == 0) { count += 8;  value <<= 8;  }
            if ((value & 0xF000000000000000UL) == 0) { count += 4;  value <<= 4;  }
            if ((value & 0xC000000000000000UL) == 0) { count += 2;  value <<= 2;  }
            if ((value & 0x8000000000000000UL) == 0) { count += 1;              }

            return count;
        }

        /// <summary>
        /// Count the number of leading zero bits in a mask.
        /// Similar in behavior to the x86 instruction LZCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        
        public static int LeadingZeroCount(nuint value)
        {
#if TARGET_64BIT
            return LeadingZeroCount((ulong)value);
#else
            return LeadingZeroCount((uint)value);
#endif
        }

        /// <summary>
        /// Returns the integer (floor) log of the specified value, base 2.
        /// Note that by convention, input value 0 returns 0 since log(0) is undefined.
        /// </summary>
        /// <param name="value">The value.</param>
        
        public static int Log2Floor(uint value)
        {
            if (value == 0)
                return -1; // undefined for zero

            var result = 0;
            if (value >> 16 != 0) { value >>= 16; result += 16; }
            if (value >> 8 != 0) { value >>= 8; result += 8; }
            if (value >> 4 != 0) { value >>= 4; result += 4; }
            if (value >> 2 != 0) { value >>= 2; result += 2; }
            if (value >> 1 != 0) {         result += 1; }

            return result;
        }

        /// <summary>
        /// Returns the integer (floor) log of the specified value, base 2.
        /// Note that by convention, input value 0 returns 0 since log(0) is undefined.
        /// </summary>
        /// <param name="value">The value.</param>
        
        public static int Log2Floor(ulong value)
        {
            if (value == 0)
                return -1; // undefined for zero

            var result = 0;
            if (value >> 32 != 0) { value >>= 32; result += 32; }
            if (value >> 16 != 0) { value >>= 16; result += 16; }
            if (value >> 8 != 0) { value >>= 8; result += 8; }
            if (value >> 4 != 0) { value >>= 4; result += 4; }
            if (value >> 2 != 0) { value >>= 2; result += 2; }
            if (value >> 1 != 0) {         result += 1; }

            return result;
        }

        /// <summary>
        /// Returns the integer (floor) log of the specified value, base 2.
        /// Note that by convention, input value 0 returns 0 since log(0) is undefined.
        /// </summary>
        /// <param name="value">The value.</param>
        
        public static int Log2Floor(nuint value)
        {
#if TARGET_64BIT
            return Log2((ulong)value);
#else
            return Log2Floor((uint)value);
#endif
        }


        /// <summary>Returns the integer (ceiling) log of the specified value, base 2.</summary>
        /// <param name="value">The value.</param>
        
        public static int Log2Ceiling(uint value)
        {
            var result = Log2Floor(value);
            if (PopCount(value) != 1) result++;
            return result;
        }

        /// <summary>Returns the integer (ceiling) log of the specified value, base 2.</summary>
        /// <param name="value">The value.</param>
        
        public static int Log2Ceiling(ulong value)
        {
            var result = Log2Floor(value);
            if (PopCount(value) != 1) result++;
            return result;
        }


        /// <summary>
        /// Returns the population count (number of bits set) of a mask.
        /// Similar in behavior to the x86 instruction POPCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        
        public static int PopCount(uint value)
        {
            const uint c1 = 0x_55555555u;
            const uint c2 = 0x_33333333u;
            const uint c3 = 0x_0F0F0F0Fu;
            const uint c4 = 0x_01010101u;

            value -= (value >> 1) & c1;
            value = (value & c2) + ((value >> 2) & c2);
            value = (((value + (value >> 4)) & c3) * c4) >> 24;

            return (int)value;
        }

        /// <summary>
        /// Returns the population count (number of bits set) of a mask.
        /// Similar in behavior to the x86 instruction POPCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        
        public static int PopCount(ulong value)
        {
#if TARGET_32BIT
            return PopCount((uint)value) // lo
                + PopCount((uint)(value >> 32)); // hi
#else
            const ulong c1 = 0x_55555555_55555555ul;
            const ulong c2 = 0x_33333333_33333333ul;
            const ulong c3 = 0x_0F0F0F0F_0F0F0F0Ful;
            const ulong c4 = 0x_01010101_01010101ul;

            value -= (value >> 1) & c1;
            value = (value & c2) + ((value >> 2) & c2);
            value = (((value + (value >> 4)) & c3) * c4) >> 56;

            return (int)value;
#endif
        }

        /// <summary>
        /// Returns the population count (number of bits set) of a mask.
        /// Similar in behavior to the x86 instruction POPCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        
        public static int PopCount(nuint value)
        {
#if TARGET_64BIT
            return PopCount((ulong)value);
#else
            return PopCount((uint)value);
#endif
        }
        
        /// <summary>
        /// Count the number of trailing zero bits in a mask.
        /// Similar in behavior to the x86 instruction TZCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        public static int TrailingZeroCount(ulong value)
        {
            if (value == 0) return 64;

            var count = 0;
            while ((value & 0xFFFFUL) == 0)
            {
                count += 16; 
                value >>= 16;
            }

            if ((value & 0x00FFUL) == 0)
            {
                count += 8;  
                value >>= 8;
            }

            if ((value & 0x000FUL) == 0)
            {
                count += 4;  
                value >>= 4;
            }

            if ((value & 0x0003UL) == 0)
            {
                count += 2;  
                value >>= 2;
            }

            if ((value & 0x0001UL) == 0)
            {
                count += 1;
            }

            return count;
        }

        /// <summary>
        /// Count the number of trailing zero bits in an integer value.
        /// Similar in behavior to the x86 instruction TZCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        
        public static int TrailingZeroCount(int value)
        {
            return TrailingZeroCount((uint)value);
        }

        /// <summary>
        /// Count the number of trailing zero bits in an integer value.
        /// Similar in behavior to the x86 instruction TZCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        
        public static int TrailingZeroCount(uint value)
        {
            if (value == 0) return 32;

            var count = 0;
            if ((value & 0x0000FFFF) == 0) { count += 16; value >>= 16; }
            if ((value & 0x000000FF) == 0) { count += 8;  value >>= 8;  }
            if ((value & 0x0000000F) == 0) { count += 4;  value >>= 4;  }
            if ((value & 0x00000003) == 0) { count += 2;  value >>= 2;  }
            if ((value & 0x00000001) == 0) { count += 1;              }

            return count;
        }

        /// <summary>
        /// Count the number of trailing zero bits in a mask.
        /// Similar in behavior to the x86 instruction TZCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        
        public static int TrailingZeroCount(long value)
        {
            return TrailingZeroCount((ulong)value);
        }

        /// <summary>
        /// Count the number of trailing zero bits in a mask.
        /// Similar in behavior to the x86 instruction TZCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        
        public static int TrailingZeroCount(nint value)
        {
#if TARGET_64BIT
            return TrailingZeroCount((ulong)(nuint)value);
#else
            return TrailingZeroCount((uint)(nuint)value);
#endif
        }

        /// <summary>
        /// Count the number of trailing zero bits in a mask.
        /// Similar in behavior to the x86 instruction TZCNT.
        /// </summary>
        /// <param name="value">The value.</param>
        
        public static int TrailingZeroCount(nuint value)
        {
#if TARGET_64BIT
            return TrailingZeroCount((ulong)value);
#else
            return TrailingZeroCount((uint)value);
#endif
        }


        /// <summary>
        /// Rotates the specified value left by the specified number of bits.
        /// Similar in behavior to the x86 instruction ROL.
        /// </summary>
        /// <param name="value">The value to rotate.</param>
        /// <param name="offset">The number of bits to rotate by.
        /// Any value outside the range [0..31] is treated as congruent mod 32.</param>
        /// <returns>The rotated value.</returns>
        
        public static uint RotateLeft(uint value, int offset)
            => (value << offset) | (value >> (32 - offset));

        /// <summary>
        /// Rotates the specified value left by the specified number of bits.
        /// Similar in behavior to the x86 instruction ROL.
        /// </summary>
        /// <param name="value">The value to rotate.</param>
        /// <param name="offset">The number of bits to rotate by.
        /// Any value outside the range [0..63] is treated as congruent mod 64.</param>
        /// <returns>The rotated value.</returns>
        
        public static ulong RotateLeft(ulong value, int offset)
            => (value << offset) | (value >> (64 - offset));

        /// <summary>
        /// Rotates the specified value left by the specified number of bits.
        /// Similar in behavior to the x86 instruction ROL.
        /// </summary>
        /// <param name="value">The value to rotate.</param>
        /// <param name="offset">The number of bits to rotate by.
        /// Any value outside the range [0..31] is treated as congruent mod 32 on a 32-bit process,
        /// and any value outside the range [0..63] is treated as congruent mod 64 on a 64-bit process.</param>
        /// <returns>The rotated value.</returns>
        
        public static nuint RotateLeft(nuint value, int offset)
        {
#if TARGET_64BIT
            return (nuint)RotateLeft((ulong)value, offset);
#else
            return RotateLeft((uint)value, offset);
#endif
        }


        /// <summary>
        /// Rotates the specified value right by the specified number of bits.
        /// Similar in behavior to the x86 instruction ROR.
        /// </summary>
        /// <param name="value">The value to rotate.</param>
        /// <param name="offset">The number of bits to rotate by.
        /// Any value outside the range [0..31] is treated as congruent mod 32.</param>
        /// <returns>The rotated value.</returns>
        
        public static uint RotateRight(uint value, int offset)
            => (value >> offset) | (value << (32 - offset));

        /// <summary>
        /// Rotates the specified value right by the specified number of bits.
        /// Similar in behavior to the x86 instruction ROR.
        /// </summary>
        /// <param name="value">The value to rotate.</param>
        /// <param name="offset">The number of bits to rotate by.
        /// Any value outside the range [0..63] is treated as congruent mod 64.</param>
        /// <returns>The rotated value.</returns>
        
        
        
        public static ulong RotateRight(ulong value, int offset)
            => (value >> offset) | (value << (64 - offset));

        /// <summary>
        /// Rotates the specified value right by the specified number of bits.
        /// Similar in behavior to the x86 instruction ROR.
        /// </summary>
        /// <param name="value">The value to rotate.</param>
        /// <param name="offset">The number of bits to rotate by.
        /// Any value outside the range [0..31] is treated as congruent mod 32 on a 32-bit process,
        /// and any value outside the range [0..63] is treated as congruent mod 64 on a 64-bit process.</param>
        /// <returns>The rotated value.</returns>
        
        
        
        public static nuint RotateRight(nuint value, int offset)
        {
#if TARGET_64BIT
            return (nuint)RotateRight((ulong)value, offset);
#else
            return RotateRight((uint)value, offset);
#endif
        }

        
        /// <summary>
        /// Reset the lowest significant bit in the given value
        /// </summary>
        
        internal static uint ResetLowestSetBit(uint value)
        {
            // It's lowered to BLSR on x86
            return value & (value - 1);
        }

        /// <summary>
        /// Reset specific bit in the given value
        /// Reset the lowest significant bit in the given value
        /// </summary>
        
        internal static ulong ResetLowestSetBit(ulong value)
        {
            // It's lowered to BLSR on x86
            return value & (value - 1);
        }

        /// <summary>
        /// Flip the bit at a specific position in a given value.
        /// Similar in behavior to the x86 instruction BTC (Bit Test and Complement).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="index">The zero-based index of the bit to flip.
        /// Any value outside the range [0..31] is treated as congruent mod 32.</param>
        /// <returns>The new value.</returns>
        
        internal static uint FlipBit(uint value, int index)
        {
            return value ^ (1u << index);
        }

        /// <summary>
        /// Flip the bit at a specific position in a given value.
        /// Similar in behavior to the x86 instruction BTC (Bit Test and Complement).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="index">The zero-based index of the bit to flip.
        /// Any value outside the range [0..63] is treated as congruent mod 64.</param>
        /// <returns>The new value.</returns>
        
        internal static ulong FlipBit(ulong value, int index)
        {
            return value ^ (1ul << index);
        }
    }
}
