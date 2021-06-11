using System;
using System.Diagnostics;

namespace DataStructuresLib.Combinations
{
    /// <summary>
    /// General utilities for computing Combinations of the form C(n, 2)
    /// </summary>
    public static class BinaryCombinationsUtilsUInt64
    {
        public static Tuple<ulong, ulong> IndexToCombinadic(ulong index)
        {
            var n2 = (ulong)(0.5d * (1d + Math.Sqrt(1UL + 8UL * index)));
            var n1 = index - ((n2 * (n2 - 1UL)) >> 1);

            return new Tuple<ulong, ulong>(n1, n2);
        }

        public static ulong IndexToCombinadicPattern(ulong index)
        {
            var n2 = (ulong)(0.5d * (1d + Math.Sqrt(1UL + 8UL * index)));
            var n1 = index - ((n2 * (n2 - 1UL)) >> 1);

            return (1UL << (int)n1) | (1UL << (int)n2);
        }

        public static ulong CombinadicToIndex(Tuple<ulong, ulong> combinadic)
        {
            var (n1, n2) = combinadic;

            Debug.Assert(n1 < n2);

            return n1 + ((n2 * (n2 - 1UL)) >> 1);
        }

        public static ulong CombinadicToIndex(int i1, int i2)
        {
            var n1 = (ulong) i1;
            var n2 = (ulong) i2;

            Debug.Assert(n1 < n2);

            return n1 + ((n2 * (n2 - 1UL)) >> 1);
        }

        public static ulong CombinadicToIndex(ulong n1, ulong n2)
        {
            Debug.Assert(n1 < n2);

            return n1 + ((n2 * (n2 - 1UL)) >> 1);
        }

        public static ulong CombinadicPatternToIndex(ulong pattern)
        {
            var n2 = (ulong) Math.Log(pattern, 2);
            var n1 = (ulong) Math.Log(pattern - (1UL << (int)n2), 2);
            
            return n1 + ((n2 * (n2 - 1UL)) >> 1);
        }

        /// <summary>
        /// Compute the binomial coefficient C(n, 2) where n is the set size using the simple relation:
        /// C(n, 2) = n * (n - 1) / 2
        /// </summary>
        /// <param name="setSize"></param>
        /// <returns></returns>
        public static ulong ComputeBinomialCoefficient(int setSize)
        {
            if (setSize < 0)
                throw new ArgumentOutOfRangeException(nameof(setSize));

            if (setSize < 2)
                return 0;

            var n = (ulong) setSize;
            return (n * (n - 1UL)) >> 1;
        }
    }
}