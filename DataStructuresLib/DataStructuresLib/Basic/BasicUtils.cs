using System;

namespace DataStructuresLib.Basic
{
    public static class BasicUtils
    {
        
        /// <summary>
        /// Find the modulus of a over b assuming b is a positive integer
        /// See this for more details:
        /// https://blogs.msdn.microsoft.com/ericlippert/2011/12/05/whats-the-difference-remainder-vs-modulus/
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Mod(this int a, int b)
        {
            var r = a % b;
            return (r < 0) ? (r + b) : r;

            //This is slower by about 33%
            //return (a % b + b) % b;
        }

        public static long Mod(this long m, long n)
        {
            var k = m % n;
            return (k < 0) ? (k + n) : k;
        }


        public static Tuple<T, T> ToTuple<T>(this IPair<T> pair)
            => new Tuple<T, T>(pair.Item1, pair.Item2);

        public static Tuple<T, T, T> ToTuple<T>(this ITriplet<T> triplet)
            => new Tuple<T, T, T>(triplet.Item1, triplet.Item2, triplet.Item3);

        public static Tuple<T, T, T, T> ToTuple<T>(this IQuad<T> quad)
            => new Tuple<T, T, T, T>(quad.Item1, quad.Item2, quad.Item3, quad.Item4);

        public static IPair<T> ToPair<T>(this Tuple<T, T> tuple)
            => new Pair<T>(tuple.Item1, tuple.Item2);

        public static ITriplet<T> ToTriplet<T>(this Tuple<T, T, T> tuple)
            => new Triplet<T>(tuple.Item1, tuple.Item2, tuple.Item3);

        public static IQuad<T> ToQuad<T>(this Tuple<T, T, T, T> tuple)
            => new Quad<T>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
    }
}
