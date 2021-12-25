using System.Runtime.CompilerServices;

namespace NumericalGeometryLib.BasicMath
{
    public static class IntUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOf2(this int n)
        {
            return (n > 0 && ~(n & (n - 1)) > 0);
        }

        /// <summary>
        /// Finds the smallest power of 2 that is >= n
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Power2Ceiling(this int n)
        {
            n--;

            n |= n >> 1;
            n |= n >> 2;
            n |= n >> 4;
            n |= n >> 8;
            n |= n >> 16;

            return n + 1;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOf2(this long n)
        {
            return (n > 0 && ~(n & (n - 1)) > 0);
        }

        /// <summary>
        /// Finds the smallest power of 2 that is >= n
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Power2Ceiling(this long n)
        {
            n--;

            n |= n >> 1;
            n |= n >> 2;
            n |= n >> 4;
            n |= n >> 8;
            n |= n >> 16;

            return n + 1;
        }
    }
}
