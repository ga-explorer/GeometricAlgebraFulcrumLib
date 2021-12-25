﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Mod(this int a, int b)
        {
            var r = a % b;
            return (r < 0) ? (r + b) : r;

            //This is slower by about 33%
            //return (a % b + b) % b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Mod(this long m, long n)
        {
            var k = m % n;
            return (k < 0) ? (k + n) : k;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetItems<T>(this Pair<T> pair)
        {
            yield return pair.Item1;
            yield return pair.Item2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<T2> MapItems<T, T2>(this Pair<T> pair, Func<T, T2> itemMapping)
        {
            return new Pair<T2>(
                itemMapping(pair.Item1), 
                itemMapping(pair.Item2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<T2> MapItems<T, T2>(this Pair<T> pair, Func<int, T, T2> itemMapping)
        {
            return new Pair<T2>(
                itemMapping(0, pair.Item1), 
                itemMapping(1, pair.Item2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 LeftFoldItems<T, T2>(this Pair<T> pair, T2 initialValue, Func<T2, T, T2> itemMapping)
        {
            return 
                itemMapping(
                    itemMapping(
                        initialValue,
                        pair.Item1
                    ),
                    pair.Item2
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> LeftScanItems<T, T2>(this Pair<T> pair, T2 initialValue, Func<T2, T, T2> itemMapping)
        {
            var item = initialValue;
            yield return item;

            item = itemMapping(item, pair.Item1);
            yield return item;

            item = itemMapping(item, pair.Item2);
            yield return item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 RightFoldItems<T, T2>(this Pair<T> pair, T2 initialValue, Func<T, T2, T2> itemMapping)
        {
            return 
                itemMapping(
                    pair.Item1, 
                    itemMapping(
                        pair.Item2, 
                        initialValue
                    )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> RightScanItems<T, T2>(this Pair<T> pair, T2 initialValue, Func<T, T2, T2> itemMapping)
        {
            var item = initialValue;
            yield return item;

            item = itemMapping(pair.Item2, item);
            yield return item;

            item = itemMapping(pair.Item1, item);
            yield return item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 ReduceItems<T, T2>(this Pair<T> pair, Func<T, T, T2> itemMapping)
        {
            return itemMapping(pair.Item1, pair.Item2);
        }

        /// <summary>
        /// Returns a new pair containing (pair.Item2, nextItem)
        /// </summary>
        /// <param name="pair"></param>
        /// <param name="nextItem"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<T> NextPair<T>(this Pair<T> pair, T nextItem)
        {
            return new Pair<T>(pair.Item2, nextItem);
        }

        /// <summary>
        /// Returns a new pair containing (previousItem, pair.Item1)
        /// </summary>
        /// <param name="pair"></param>
        /// <param name="previousItem"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<T> PreviousPair<T>(this Pair<T> pair, T previousItem)
        {
            return new Pair<T>(previousItem, pair.Item1);
        }

        /// <summary>
        /// Returns a new pair containing (pair.Item2, pair.Item1)
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<T> SwapItems<T>(this Pair<T> pair)
        {
            return new Pair<T>(pair.Item2, pair.Item1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetItems<T>(this Triplet<T> triplet)
        {
            yield return triplet.Item1;
            yield return triplet.Item2;
            yield return triplet.Item3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<T2> MapItems<T, T2>(this Triplet<T> triplet, Func<T, T2> itemMapping)
        {
            return new Triplet<T2>(
                itemMapping(triplet.Item1), 
                itemMapping(triplet.Item2), 
                itemMapping(triplet.Item3)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<T2> MapItems<T, T2>(this Triplet<T> triplet, Func<int, T, T2> itemMapping)
        {
            return new Triplet<T2>(
                itemMapping(0, triplet.Item1), 
                itemMapping(1, triplet.Item2), 
                itemMapping(2, triplet.Item3)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LeftReduceItems<T>(this Triplet<T> triplet, Func<T, T, T> itemMapping)
        {
            return itemMapping(itemMapping(triplet.Item1, triplet.Item2), triplet.Item3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 LeftFoldItems<T, T2>(this Triplet<T> triplet, T2 initialValue, Func<T2, T, T2> itemMapping)
        {
            return 
                itemMapping(
                    itemMapping(
                        itemMapping(
                            initialValue,
                            triplet.Item1
                        ),
                        triplet.Item2
                    ), 
                    triplet.Item3
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> LeftScanItems<T, T2>(this Triplet<T> triplet, T2 initialValue, Func<T2, T, T2> itemMapping)
        {
            var item = initialValue;
            yield return item;

            item = itemMapping(item, triplet.Item1);
            yield return item;

            item = itemMapping(item, triplet.Item2);
            yield return item;

            item = itemMapping(item, triplet.Item3);
            yield return item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T RightReduceItems<T>(this Triplet<T> triplet, Func<T, T, T> itemMapping)
        {
            return itemMapping(triplet.Item1, itemMapping(triplet.Item2, triplet.Item3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 RightFoldItems<T, T2>(this Triplet<T> triplet, T2 initialValue, Func<T, T2, T2> itemMapping)
        {
            return 
                itemMapping(
                    triplet.Item1, 
                    itemMapping(
                        triplet.Item2, 
                        itemMapping(
                            triplet.Item3, 
                            initialValue
                        )
                    )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> RightScanItems<T, T2>(this Triplet<T> triplet, T2 initialValue, Func<T, T2, T2> itemMapping)
        {
            var item = initialValue;
            yield return item;

            item = itemMapping(triplet.Item3, item);
            yield return item;

            item = itemMapping(triplet.Item2, item);
            yield return item;

            item = itemMapping(triplet.Item1, item);
            yield return item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 ReduceItems<T, T2>(this Triplet<T> triplet, Func<T, T, T, T2> itemMapping)
        {
            return itemMapping(triplet.Item1, triplet.Item2, triplet.Item3);
        }

        /// <summary>
        /// Returns a new triplet containing (this.Item2, nextItem)
        /// </summary>
        /// <param name="triplet"></param>
        /// <param name="nextItem"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<T> NextTriplet<T>(this Triplet<T> triplet, T nextItem)
        {
            return new Triplet<T>(triplet.Item2, triplet.Item3, nextItem);
        }

        /// <summary>
        /// Returns a new triplet containing (previousItem, this.Item1)
        /// </summary>
        /// <param name="triplet"></param>
        /// <param name="previousItem"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<T> PreviousTriplet<T>(this Triplet<T> triplet, T previousItem)
        {
            return new Triplet<T>(previousItem, triplet.Item1, triplet.Item2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<T> RotateForward<T>(this Triplet<T> triplet)
        {
            return new Triplet<T>(triplet.Item3, triplet.Item1, triplet.Item2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<T> RotateBackward<T>(this Triplet<T> triplet)
        {
            return new Triplet<T>(triplet.Item2, triplet.Item3, triplet.Item1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetItems<T>(this Quad<T> quad)
        {
            yield return quad.Item1;
            yield return quad.Item2;
            yield return quad.Item3;
            yield return quad.Item4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<T2> MapItems<T, T2>(this Quad<T> quad, Func<T, T2> itemMapping)
        {
            return new Quad<T2>(
                itemMapping(quad.Item1), 
                itemMapping(quad.Item2), 
                itemMapping(quad.Item3),
                itemMapping(quad.Item4)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<T2> MapItems<T, T2>(this Quad<T> quad, Func<int, T, T2> itemMapping)
        {
            return new Quad<T2>(
                itemMapping(0, quad.Item1), 
                itemMapping(1, quad.Item2), 
                itemMapping(2, quad.Item3),
                itemMapping(3, quad.Item3)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T LeftReduceItems<T>(this Quad<T> quad, Func<T, T, T> itemMapping)
        {
            return itemMapping(itemMapping(itemMapping(quad.Item1, quad.Item2), quad.Item3), quad.Item4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 LeftFoldItems<T, T2>(this Quad<T> quad, T2 initialValue, Func<T2, T, T2> itemMapping)
        {
            return 
                itemMapping(
                    itemMapping(
                        itemMapping(
                            itemMapping(
                                initialValue,
                                quad.Item1
                            ),
                            quad.Item2
                        ), 
                        quad.Item3
                    ), 
                    quad.Item4
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> LeftScanItems<T, T2>(this Quad<T> quad, T2 initialValue, Func<T2, T, T2> itemMapping)
        {
            var item = initialValue;
            yield return item;

            item = itemMapping(item, quad.Item1);
            yield return item;

            item = itemMapping(item, quad.Item2);
            yield return item;

            item = itemMapping(item, quad.Item3);
            yield return item;

            item = itemMapping(item, quad.Item4);
            yield return item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T RightReduceItems<T>(this Quad<T> quad, Func<T, T, T> itemMapping)
        {
            return itemMapping(quad.Item1, itemMapping(quad.Item2, itemMapping(quad.Item3, quad.Item4)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 RightFoldItems<T, T2>(this Quad<T> quad, T2 initialValue, Func<T, T2, T2> itemMapping)
        {
            return 
                itemMapping(
                    quad.Item1, 
                    itemMapping(
                        quad.Item2, 
                        itemMapping(
                            quad.Item3, 
                            itemMapping(
                                quad.Item4, 
                                initialValue
                            )
                        )
                    )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> RightScanItems<T, T2>(this Quad<T> quad, T2 initialValue, Func<T, T2, T2> itemMapping)
        {
            var item = initialValue;
            yield return item;

            item = itemMapping(quad.Item4, item);
            yield return item;

            item = itemMapping(quad.Item3, item);
            yield return item;

            item = itemMapping(quad.Item2, item);
            yield return item;

            item = itemMapping(quad.Item1, item);
            yield return item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 ReduceItems<T, T2>(this Quad<T> quad, Func<T, T, T, T, T2> itemMapping)
        {
            return itemMapping(quad.Item1, quad.Item2, quad.Item3, quad.Item4);
        }

        /// <summary>
        /// Returns a new quad containing (this.Item2, nextItem)
        /// </summary>
        /// <param name="quad"></param>
        /// <param name="nextItem"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<T> NextQuad<T>(this Quad<T> quad, T nextItem)
        {
            return new Quad<T>(quad.Item2, quad.Item3, quad.Item4, nextItem);
        }

        /// <summary>
        /// Returns a new quad containing (previousItem, this.Item1)
        /// </summary>
        /// <param name="quad"></param>
        /// <param name="previousItem"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<T> PreviousQuad<T>(this Quad<T> quad, T previousItem)
        {
            return new Quad<T>(previousItem, quad.Item1, quad.Item2, quad.Item3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<T> RotateForward<T>(this Quad<T> quad)
        {
            return new Quad<T>(quad.Item4, quad.Item1, quad.Item2, quad.Item3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<T> RotateBackward<T>(this Quad<T> quad)
        {
            return new Quad<T>(quad.Item2, quad.Item3, quad.Item4, quad.Item1);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<T> ToPair<T>(this IPair<T> pair)
        {
            return pair is Pair<T> p
                ? p
                : new Pair<T>(pair.Item1, pair.Item2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<T, T> ToTuple<T>(this IPair<T> pair)
        {
            return new Tuple<T, T>(pair.Item1, pair.Item2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<T, T, T> ToTuple<T>(this ITriplet<T> triplet)
        {
            return new Tuple<T, T, T>(triplet.Item1, triplet.Item2, triplet.Item3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<T, T, T, T> ToTuple<T>(this IQuad<T> quad)
        {
            return new Tuple<T, T, T, T>(quad.Item1, quad.Item2, quad.Item3, quad.Item4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IPair<T> ToPair<T>(this Tuple<T, T> tuple)
        {
            return new Pair<T>(tuple.Item1, tuple.Item2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<T> ToTriplet<T>(this ITriplet<T> triplet)
        {
            return triplet is Triplet<T> t
                ? t
                : new Triplet<T>(triplet.Item1, triplet.Item2, triplet.Item3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ITriplet<T> ToTriplet<T>(this Tuple<T, T, T> tuple)
        {
            return new Triplet<T>(tuple.Item1, tuple.Item2, tuple.Item3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quad<T> ToQuad<T>(this IQuad<T> quad)
        {
            return quad is Quad<T> q
                ? q
                : new Quad<T>(quad.Item1, quad.Item2, quad.Item3, quad.Item4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQuad<T> ToQuad<T>(this Tuple<T, T, T, T> tuple)
        {
            return new Quad<T>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        }

        /// <summary>
        /// Convert a sequence of items into pairs.
        /// For example, having the sequence 0, 1, 2, 3 this returns pairs
        /// (0, 1), (1, 2), (2, 3)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemsList"></param>
        /// <returns></returns>
        public static IEnumerable<Pair<T>> GetPairsChain<T>(this IEnumerable<T> itemsList)
        {
            var firstItemSkipped = false;
            T firstItem = default;

            foreach (var nextItem in itemsList.Skip(1))
            {
                if (!firstItemSkipped)
                {
                    firstItem = nextItem;
                    firstItemSkipped = true;
                    continue;
                }

                yield return new Pair<T>(firstItem, nextItem);

                firstItem = nextItem;
            }
        }

        /// <summary>
        /// Convert an even sequence of items into pairs.
        /// For example, having the sequence 0, 1, 2, 3 this returns pairs
        /// (0, 1), (2, 3)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemsList"></param>
        /// <returns></returns>
        public static IEnumerable<Pair<T>> GetPairsSequence<T>(this IEnumerable<T> itemsList)
        {
            var firstItemSkipped = false;
            T firstItem = default;

            foreach (var nextItem in itemsList.Skip(1))
            {
                if (!firstItemSkipped)
                {
                    firstItem = nextItem;
                    firstItemSkipped = true;
                    continue;
                }

                yield return new Pair<T>(firstItem, nextItem);

                firstItemSkipped = false;
                firstItem = default;
            }

            if (firstItemSkipped)
                yield return new Pair<T>(firstItem, default);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair2D<T> ToPair2D<T>(this IPair2D<T> pair2D)
        {
            return pair2D is Pair2D<T> p
                ? p
                : new Pair2D<T>(pair2D.Item11, pair2D.Item12, pair2D.Item21, pair2D.Item22);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<T> GetPair1_<T>(this IPair2D<T> pair2D)
        {
            return new Pair<T>(pair2D.Item11, pair2D.Item12);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<T> GetPair2_<T>(this IPair2D<T> pair2D)
        {
            return new Pair<T>(pair2D.Item21, pair2D.Item22);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<T> GetPair_1<T>(this IPair2D<T> pair2D)
        {
            return new Pair<T>(pair2D.Item11, pair2D.Item21);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<T> GetPair_2<T>(this IPair2D<T> pair2D)
        {
            return new Pair<T>(pair2D.Item12, pair2D.Item22);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToText<T>(this IPair<T> pair)
        {
            return $"({pair.Item1}, {pair.Item2})";
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToText<T>(this ITriplet<T> triplet)
        {
            return $"({triplet.Item1}, {triplet.Item2}, {triplet.Item3})";
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToText<T>(this IQuad<T> quad)
        {
            return $"({quad.Item1}, {quad.Item2}, {quad.Item3}, {quad.Item4})";
        }
    }
}
