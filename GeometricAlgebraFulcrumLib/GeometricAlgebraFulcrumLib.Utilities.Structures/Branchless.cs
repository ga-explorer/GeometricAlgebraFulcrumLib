using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures
{
    public static class Branchless
    {
        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(sbyte value)
        {
            return Sign((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(short value)
        {
            return Sign((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(int value)
        {
            return unchecked(value >> 31 | -value >>> 31);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(long value)
        {
            return unchecked((int)(value >> 63 | -value >>> 63));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(nint value)
        {
#if TARGET_64BIT
            return (int)unchecked((int)(value >> 63 | -value >>> 63));
#else
            return (int)unchecked((int)(value >> 31) | -value >>> 31);
#endif
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsGreaterThan(byte a, byte b)
        {
            return 1 - Sign(Sign(b - a) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsGreaterOrEqual(byte a, byte b)
        {
            return Sign(Sign(a - b) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsLessThan(byte a, byte b)
        {
            return 1 - Sign(Sign(a - b) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsLessOrEqual(byte a, byte b)
        {
            return Sign(Sign(b - a) + 1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsGreaterThan(sbyte a, sbyte b)
        {
            return 1 - Sign(Sign(b - a) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsGreaterOrEqual(sbyte a, sbyte b)
        {
            return Sign(Sign(a - b) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsLessThan(sbyte a, sbyte b)
        {
            return 1 - Sign(Sign(a - b) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsLessOrEqual(sbyte a, sbyte b)
        {
            return Sign(Sign(b - a) + 1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsGreaterThan(ushort a, ushort b)
        {
            return 1 - Sign(Sign(b - a) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsGreaterOrEqual(ushort a, ushort b)
        {
            return Sign(Sign(a - b) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsLessThan(ushort a, ushort b)
        {
            return 1 - Sign(Sign(a - b) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsLessOrEqual(ushort a, ushort b)
        {
            return Sign(Sign(b - a) + 1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsGreaterThan(short a, short b)
        {
            return 1 - Sign(Sign(b - a) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsGreaterOrEqual(short a, short b)
        {
            return Sign(Sign(a - b) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsLessThan(short a, short b)
        {
            return 1 - Sign(Sign(a - b) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsLessOrEqual(short a, short b)
        {
            return Sign(Sign(b - a) + 1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsGreaterThan(uint a, uint b)
        {
            return 1 - Sign(Sign(b - a) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsGreaterOrEqual(uint a, uint b)
        {
            return Sign(Sign(a - b) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsLessThan(uint a, uint b)
        {
            return 1 - Sign(Sign(a - b) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsLessOrEqual(uint a, uint b)
        {
            return Sign(Sign(b - a) + 1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsGreaterThan(int a, int b)
        {
            return 1 - Sign(Sign(b - a) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsGreaterOrEqual(int a, int b)
        {
            return Sign(Sign(a - b) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsLessThan(int a, int b)
        {
            return 1 - Sign(Sign(a - b) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsLessOrEqual(int a, int b)
        {
            return Sign(Sign(b - a) + 1);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsGreaterThan(long a, long b)
        {
            return 1 - Sign(Sign(b - a) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsGreaterOrEqual(long a, long b)
        {
            return Sign(Sign(a - b) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsLessThan(long a, long b)
        {
            return 1 - Sign(Sign(a - b) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsLessOrEqual(long a, long b)
        {
            return Sign(Sign(b - a) + 1);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsGreaterThan(nint a, nint b)
        {
            return 1 - Sign(Sign(b - a) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsGreaterOrEqual(nint a, nint b)
        {
            return Sign(Sign(a - b) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsLessThan(nint a, nint b)
        {
            return 1 - Sign(Sign(a - b) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsLessOrEqual(nint a, nint b)
        {
            return Sign(Sign(b - a) + 1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapIfLessThan(ref byte left, ref byte right)
        {
            var diff = left - right;
            var diffSign = diff >> 31;
            var max = (byte)(left - (diff & diffSign));
            var min = (byte)(right + (diff & diffSign));

            right = min;
            left = max;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapIfGreaterThan(ref byte left, ref byte right)
        {
            var diff = left - right;
            var diffSign = diff >> 31;
            var max = (byte)(left - (diff & diffSign));
            var min = (byte)(right + (diff & diffSign));

            right = max;
            left = min;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapIfLessThan(ref sbyte left, ref sbyte right)
        {
            var diff = left - right;
            var diffSign = diff >> 31;
            var max = (sbyte)(left - (diff & diffSign));
            var min = (sbyte)(right + (diff & diffSign));

            right = min;
            left = max;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapIfGreaterThan(ref sbyte left, ref sbyte right)
        {
            var diff = left - right;
            var diffSign = diff >> 31;
            var max = (sbyte)(left - (diff & diffSign));
            var min = (sbyte)(right + (diff & diffSign));

            right = max;
            left = min;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapIfLessThan(ref ushort left, ref ushort right)
        {
            var diff = left - right;
            var diffSign = diff >> 31;
            var max = (ushort)(left - (diff & diffSign));
            var min = (ushort)(right + (diff & diffSign));

            right = min;
            left = max;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapIfGreaterThan(ref ushort left, ref ushort right)
        {
            var diff = left - right;
            var diffSign = diff >> 31;
            var max = (ushort)(left - (diff & diffSign));
            var min = (ushort)(right + (diff & diffSign));

            right = max;
            left = min;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapIfLessThan(ref short left, ref short right)
        {
            var diff = left - right;
            var diffSign = diff >> 31;
            var max = (short)(left - (diff & diffSign));
            var min = (short)(right + (diff & diffSign));

            right = min;
            left = max;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapIfGreaterThan(ref short left, ref short right)
        {
            var diff = left - right;
            var diffSign = diff >> 31;
            var max = (short)(left - (diff & diffSign));
            var min = (short)(right + (diff & diffSign));

            right = max;
            left = min;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapIfLessThan(ref uint left, ref uint right)
        {
            var diff = (long)left - right;
            var diffSign = diff >> 63;
            var max = (uint)(left - (diff & diffSign));
            var min = (uint)(right + (diff & diffSign));

            right = min;
            left = max;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapIfGreaterThan(ref uint left, ref uint right)
        {
            var diff = (long)left - right;
            var diffSign = diff >> 63;
            var max = (uint)(left - (diff & diffSign));
            var min = (uint)(right + (diff & diffSign));

            right = max;
            left = min;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapIfLessThan(ref int left, ref int right)
        {
            var diff = (long)left - right;
            var diffSign = diff >> 63;
            var max = (int)(left - (diff & diffSign));
            var min = (int)(right + (diff & diffSign));

            right = min;
            left = max;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapIfGreaterThan(ref int left, ref int right)
        {
            var diff = (long)left - right;
            var diffSign = diff >> 63;
            var max = (int)(left - (diff & diffSign));
            var min = (int)(right + (diff & diffSign));

            right = max;
            left = min;
        }
    }
}
