using System;
using System.Collections.Generic;
using System.Linq;

namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples
{
    public static class TupleUtils
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
            return r < 0 ? r + b : r;

            //This is slower by about 33%
            //return (a % b + b) % b;
        }

        
        public static long Mod(this long m, long n)
        {
            var k = m % n;
            return k < 0 ? k + n : k;
        }


        
        public static IEnumerable<Tuple<T2, T1>> SwapItem12<T1, T2>(this IEnumerable<Tuple<T1, T2>> tupleList)
        {
            return tupleList.Select(t => new Tuple<T2, T1>(
                t.Item2,
                t.Item1
            ));
        }

        
        public static IEnumerable<Tuple<T3, T2>> MapItem1<T1, T2, T3>(this IEnumerable<Tuple<T1, T2>> tupleList, Func<T1, T3> itemMapping)
        {
            return tupleList.Select(t => new Tuple<T3, T2>(
                itemMapping(t.Item1),
                t.Item2
            ));
        }
        
        
        public static IEnumerable<Tuple<T3, T2>> MapItem1<T1, T2, T3>(this IEnumerable<Tuple<T1, T2>> tupleList, Func<T1, T2, T3> itemMapping)
        {
            return tupleList.Select(t => new Tuple<T3, T2>(
                itemMapping(t.Item1, t.Item2),
                t.Item2
            ));
        }

        
        public static IEnumerable<Tuple<T1, T3>> MapItem2<T1, T2, T3>(this IEnumerable<Tuple<T1, T2>> tupleList, Func<T2, T3> itemMapping)
        {
            return tupleList.Select(t => new Tuple<T1, T3>(
                t.Item1,
                itemMapping(t.Item2)
            ));
        }

        
        public static IEnumerable<Tuple<T1, T3>> MapItem2<T1, T2, T3>(this IEnumerable<Tuple<T1, T2>> tupleList, Func<T1, T2, T3> itemMapping)
        {
            return tupleList.Select(t => new Tuple<T1, T3>(
                t.Item1,
                itemMapping(t.Item1, t.Item2)
            ));
        }


        
        public static IEnumerable<T> GetItems<T>(this IPair<T> pair)
        {
            yield return pair.Item1;
            yield return pair.Item2;
        }
        
        
        public static T GetItem<T>(this IPair<T> pair, int index)
        {
            return index switch
            {
                0 => pair.Item1,
                1 => pair.Item2,
                _ => throw new IndexOutOfRangeException()
            };
        }

        
        public static T[] GetItemArray<T>(this IPair<T> pair)
        {
            return
            [
                pair.Item1, 
                pair.Item2
            ];
        }


        
        public static IEnumerable<T2> MapTuples<T, T2>(this IEnumerable<IPair<T>> tupleList, Func<T, T, T2> itemMapping)
        {
            return tupleList.Select(t => 
                itemMapping(t.Item1, t.Item2)
            );
        }
    
        
        public static IEnumerable<T2> MapTuples<T, T2>(this IEnumerable<ITriplet<T>> tupleList, Func<T, T, T, T2> itemMapping)
        {
            return tupleList.Select(t => 
                itemMapping(t.Item1, t.Item2, t.Item3)
            );
        }
    
        
        public static IEnumerable<T2> MapTuples<T, T2>(this IEnumerable<IQuad<T>> tupleList, Func<T, T, T, T, T2> itemMapping)
        {
            return tupleList.Select(t => 
                itemMapping(t.Item1, t.Item2, t.Item3, t.Item4)
            );
        }
    
        
        public static IEnumerable<T2> MapTuples<T, T2>(this IEnumerable<IQuint<T>> tupleList, Func<T, T, T, T, T, T2> itemMapping)
        {
            return tupleList.Select(t => 
                itemMapping(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5)
            );
        }
    
        
        public static IEnumerable<T2> MapTuples<T, T2>(this IEnumerable<IHexad<T>> tupleList, Func<T, T, T, T, T, T, T2> itemMapping)
        {
            return tupleList.Select(t => 
                itemMapping(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6)
            );
        }


        
        public static Pair<T2> MapItems<T, T2>(this IPair<T> pair, Func<T, T2> itemMapping)
        {
            return new Pair<T2>(
                itemMapping(pair.Item1), 
                itemMapping(pair.Item2)
            );
        }

        
        public static Pair<T2> MapItems<T, T2>(this IPair<T> pair, Func<int, T, T2> itemMapping)
        {
            return new Pair<T2>(
                itemMapping(0, pair.Item1), 
                itemMapping(1, pair.Item2)
            );
        }

        
        public static T2 LeftFoldItems<T, T2>(this IPair<T> pair, T2 initialValue, Func<T2, T, T2> itemMapping)
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
        
        
        public static IEnumerable<T2> LeftScanItems<T, T2>(this IPair<T> pair, T2 initialValue, Func<T2, T, T2> itemMapping)
        {
            var item = initialValue;
            yield return item;

            item = itemMapping(item, pair.Item1);
            yield return item;

            item = itemMapping(item, pair.Item2);
            yield return item;
        }

        
        public static T2 RightFoldItems<T, T2>(this IPair<T> pair, T2 initialValue, Func<T, T2, T2> itemMapping)
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
        
        
        public static IEnumerable<T2> RightScanItems<T, T2>(this IPair<T> pair, T2 initialValue, Func<T, T2, T2> itemMapping)
        {
            var item = initialValue;
            yield return item;

            item = itemMapping(pair.Item2, item);
            yield return item;

            item = itemMapping(pair.Item1, item);
            yield return item;
        }

        
        public static T2 ReduceItems<T, T2>(this IPair<T> pair, Func<T, T, T2> itemMapping)
        {
            return itemMapping(pair.Item1, pair.Item2);
        }

        /// <summary>
        /// Returns a new pair containing (pair.Item2, nextItem)
        /// </summary>
        /// <param name="pair"></param>
        /// <param name="nextItem"></param>
        /// <returns></returns>
        
        public static Pair<T> NextPair<T>(this IPair<T> pair, T nextItem)
        {
            return new Pair<T>(pair.Item2, nextItem);
        }

        /// <summary>
        /// Returns a new pair containing (previousItem, pair.Item1)
        /// </summary>
        /// <param name="pair"></param>
        /// <param name="previousItem"></param>
        /// <returns></returns>
        
        public static Pair<T> PreviousPair<T>(this IPair<T> pair, T previousItem)
        {
            return new Pair<T>(previousItem, pair.Item1);
        }

        /// <summary>
        /// Returns a new pair containing (pair.Item2, pair.Item1)
        /// </summary>
        /// <returns></returns>
        
        public static Pair<T> SwapItems<T>(this IPair<T> pair)
        {
            return new Pair<T>(pair.Item2, pair.Item1);
        }
    
        
        
        public static T GetItem<T>(this ITriplet<T> triplet, int index)
        {
            return index switch
            {
                0 => triplet.Item1,
                1 => triplet.Item2,
                2 => triplet.Item3,
                _ => throw new IndexOutOfRangeException()
            };
        }

        
        public static IEnumerable<T> GetItems<T>(this ITriplet<T> triplet)
        {
            yield return triplet.Item1;
            yield return triplet.Item2;
            yield return triplet.Item3;
        }
        
        
        public static T[] GetItemArray<T>(this ITriplet<T> triplet)
        {
            return
            [
                triplet.Item1, 
                triplet.Item2,
                triplet.Item3
            ];
        }

        
        public static Triplet<T2> MapItems<T, T2>(this ITriplet<T> triplet, Func<T, T2> itemMapping)
        {
            return new Triplet<T2>(
                itemMapping(triplet.Item1), 
                itemMapping(triplet.Item2), 
                itemMapping(triplet.Item3)
            );
        }

        
        public static Triplet<T2> MapItems<T, T2>(this ITriplet<T> triplet, Func<int, T, T2> itemMapping)
        {
            return new Triplet<T2>(
                itemMapping(0, triplet.Item1), 
                itemMapping(1, triplet.Item2), 
                itemMapping(2, triplet.Item3)
            );
        }

        
        public static T LeftReduceItems<T>(this ITriplet<T> triplet, Func<T, T, T> itemMapping)
        {
            return itemMapping(itemMapping(triplet.Item1, triplet.Item2), triplet.Item3);
        }

        
        public static T2 LeftFoldItems<T, T2>(this ITriplet<T> triplet, T2 initialValue, Func<T2, T, T2> itemMapping)
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
        
        
        public static IEnumerable<T2> LeftScanItems<T, T2>(this ITriplet<T> triplet, T2 initialValue, Func<T2, T, T2> itemMapping)
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

        
        public static T RightReduceItems<T>(this ITriplet<T> triplet, Func<T, T, T> itemMapping)
        {
            return itemMapping(triplet.Item1, itemMapping(triplet.Item2, triplet.Item3));
        }

        
        public static T2 RightFoldItems<T, T2>(this ITriplet<T> triplet, T2 initialValue, Func<T, T2, T2> itemMapping)
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
        
        
        public static IEnumerable<T2> RightScanItems<T, T2>(this ITriplet<T> triplet, T2 initialValue, Func<T, T2, T2> itemMapping)
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

        
        public static T2 ReduceItems<T, T2>(this ITriplet<T> triplet, Func<T, T, T, T2> itemMapping)
        {
            return itemMapping(triplet.Item1, triplet.Item2, triplet.Item3);
        }

        /// <summary>
        /// Returns a new triplet containing (this.Item2, nextItem)
        /// </summary>
        /// <param name="triplet"></param>
        /// <param name="nextItem"></param>
        /// <returns></returns>
        
        public static Triplet<T> NextTriplet<T>(this ITriplet<T> triplet, T nextItem)
        {
            return new Triplet<T>(triplet.Item2, triplet.Item3, nextItem);
        }

        /// <summary>
        /// Returns a new triplet containing (previousItem, this.Item1)
        /// </summary>
        /// <param name="triplet"></param>
        /// <param name="previousItem"></param>
        /// <returns></returns>
        
        public static Triplet<T> PreviousTriplet<T>(this ITriplet<T> triplet, T previousItem)
        {
            return new Triplet<T>(previousItem, triplet.Item1, triplet.Item2);
        }

        
        public static Triplet<T> RotateForward<T>(this ITriplet<T> triplet)
        {
            return new Triplet<T>(triplet.Item3, triplet.Item1, triplet.Item2);
        }

        
        public static Triplet<T> RotateBackward<T>(this ITriplet<T> triplet)
        {
            return new Triplet<T>(triplet.Item2, triplet.Item3, triplet.Item1);
        }

        
        
        public static T GetItem<T>(this IQuad<T> quad, int index)
        {
            return index switch
            {
                0 => quad.Item1,
                1 => quad.Item2,
                2 => quad.Item3,
                3 => quad.Item4,
                _ => throw new IndexOutOfRangeException()
            };
        }

        
        public static IEnumerable<T> GetItems<T>(this IQuad<T> quad)
        {
            yield return quad.Item1;
            yield return quad.Item2;
            yield return quad.Item3;
            yield return quad.Item4;
        }
        
        
        public static T[] GetItemArray<T>(this IQuad<T> quad)
        {
            return
            [
                quad.Item1, 
                quad.Item2,
                quad.Item3,
                quad.Item4
            ];
        }

        
        public static Quad<T2> MapItems<T, T2>(this IQuad<T> quad, Func<T, T2> itemMapping)
        {
            return new Quad<T2>(
                itemMapping(quad.Item1), 
                itemMapping(quad.Item2), 
                itemMapping(quad.Item3),
                itemMapping(quad.Item4)
            );
        }

        
        public static Quad<T2> MapItems<T, T2>(this IQuad<T> quad, Func<int, T, T2> itemMapping)
        {
            return new Quad<T2>(
                itemMapping(0, quad.Item1), 
                itemMapping(1, quad.Item2), 
                itemMapping(2, quad.Item3),
                itemMapping(3, quad.Item3)
            );
        }

        
        public static T LeftReduceItems<T>(this IQuad<T> quad, Func<T, T, T> itemMapping)
        {
            return itemMapping(itemMapping(itemMapping(quad.Item1, quad.Item2), quad.Item3), quad.Item4);
        }

        
        public static T2 LeftFoldItems<T, T2>(this IQuad<T> quad, T2 initialValue, Func<T2, T, T2> itemMapping)
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

        
        public static IEnumerable<T2> LeftScanItems<T, T2>(this IQuad<T> quad, T2 initialValue, Func<T2, T, T2> itemMapping)
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

        
        public static T RightReduceItems<T>(this IQuad<T> quad, Func<T, T, T> itemMapping)
        {
            return itemMapping(quad.Item1, itemMapping(quad.Item2, itemMapping(quad.Item3, quad.Item4)));
        }

        
        public static T2 RightFoldItems<T, T2>(this IQuad<T> quad, T2 initialValue, Func<T, T2, T2> itemMapping)
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
        
        
        public static IEnumerable<T2> RightScanItems<T, T2>(this IQuad<T> quad, T2 initialValue, Func<T, T2, T2> itemMapping)
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

        
        public static T2 ReduceItems<T, T2>(this IQuad<T> quad, Func<T, T, T, T, T2> itemMapping)
        {
            return itemMapping(quad.Item1, quad.Item2, quad.Item3, quad.Item4);
        }

        /// <summary>
        /// Returns a new quad containing (this.Item2, nextItem)
        /// </summary>
        /// <param name="quad"></param>
        /// <param name="nextItem"></param>
        /// <returns></returns>
        
        public static Quad<T> NextQuad<T>(this IQuad<T> quad, T nextItem)
        {
            return new Quad<T>(quad.Item2, quad.Item3, quad.Item4, nextItem);
        }

        /// <summary>
        /// Returns a new quad containing (previousItem, this.Item1)
        /// </summary>
        /// <param name="quad"></param>
        /// <param name="previousItem"></param>
        /// <returns></returns>
        
        public static Quad<T> PreviousQuad<T>(this IQuad<T> quad, T previousItem)
        {
            return new Quad<T>(previousItem, quad.Item1, quad.Item2, quad.Item3);
        }

        
        public static Quad<T> RotateForward<T>(this IQuad<T> quad)
        {
            return new Quad<T>(quad.Item4, quad.Item1, quad.Item2, quad.Item3);
        }

        
        public static Quad<T> RotateBackward<T>(this IQuad<T> quad)
        {
            return new Quad<T>(quad.Item2, quad.Item3, quad.Item4, quad.Item1);
        }

        
        
        public static T GetItem<T>(this IQuint<T> quint, int index)
        {
            return index switch
            {
                0 => quint.Item1,
                1 => quint.Item2,
                2 => quint.Item3,
                3 => quint.Item4,
                4 => quint.Item5,
                _ => throw new IndexOutOfRangeException()
            };
        }

        
        public static IEnumerable<T> GetItems<T>(this IQuint<T> quint)
        {
            yield return quint.Item1;
            yield return quint.Item2;
            yield return quint.Item3;
            yield return quint.Item4;
            yield return quint.Item5;
        }
        
        
        public static T[] GetItemArray<T>(this IQuint<T> quint)
        {
            return
            [
                quint.Item1, 
                quint.Item2,
                quint.Item3,
                quint.Item4,
                quint.Item5
            ];
        }

        
        
        public static T GetItem<T>(this IHexad<T> hexad, int index)
        {
            return index switch
            {
                0 => hexad.Item1,
                1 => hexad.Item2,
                2 => hexad.Item3,
                3 => hexad.Item4,
                4 => hexad.Item5,
                5 => hexad.Item6,
                _ => throw new IndexOutOfRangeException()
            };
        }

        
        public static IEnumerable<T> GetItems<T>(this IHexad<T> hexad)
        {
            yield return hexad.Item1;
            yield return hexad.Item2;
            yield return hexad.Item3;
            yield return hexad.Item4;
            yield return hexad.Item5;
            yield return hexad.Item6;
        }
        
        
        public static T[] GetItemArray<T>(this IHexad<T> hexad)
        {
            return
            [
                hexad.Item1, 
                hexad.Item2,
                hexad.Item3,
                hexad.Item4,
                hexad.Item5,
                hexad.Item6
            ];
        }


        
        public static Pair<T> ToPair<T>(this IPair<T> pair)
        {
            return pair is Pair<T> p
                ? p
                : new Pair<T>(pair.Item1, pair.Item2);
        }

        
        public static Tuple<T, T> ToTuple<T>(this IPair<T> pair)
        {
            return new Tuple<T, T>(pair.Item1, pair.Item2);
        }

        
        public static Tuple<T, T, T> ToTuple<T>(this ITriplet<T> triplet)
        {
            return new Tuple<T, T, T>(triplet.Item1, triplet.Item2, triplet.Item3);
        }

        
        public static Tuple<T, T, T, T> ToTuple<T>(this IQuad<T> quad)
        {
            return new Tuple<T, T, T, T>(quad.Item1, quad.Item2, quad.Item3, quad.Item4);
        }

        
        public static IPair<T> ToPair<T>(this Tuple<T, T> tuple)
        {
            return new Pair<T>(tuple.Item1, tuple.Item2);
        }

        
        public static Triplet<T> ToTriplet<T>(this ITriplet<T> triplet)
        {
            return triplet is Triplet<T> t
                ? t
                : new Triplet<T>(triplet.Item1, triplet.Item2, triplet.Item3);
        }

        
        public static ITriplet<T> ToTriplet<T>(this Tuple<T, T, T> tuple)
        {
            return new Triplet<T>(tuple.Item1, tuple.Item2, tuple.Item3);
        }

        
        public static Quad<T> ToQuad<T>(this IQuad<T> quad)
        {
            return quad is Quad<T> q
                ? q
                : new Quad<T>(quad.Item1, quad.Item2, quad.Item3, quad.Item4);
        }

        
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


        
        public static Pair2D<T> ToPair2D<T>(this IPair2D<T> pair2D)
        {
            return pair2D as Pair2D<T> 
                   ?? new Pair2D<T>(
                       pair2D.Item11, 
                       pair2D.Item12, 
                       pair2D.Item21, 
                       pair2D.Item22
                   );
        }

        
        public static Pair<T> GetPair1_<T>(this IPair2D<T> pair2D)
        {
            return new Pair<T>(pair2D.Item11, pair2D.Item12);
        }

        
        public static Pair<T> GetPair2_<T>(this IPair2D<T> pair2D)
        {
            return new Pair<T>(pair2D.Item21, pair2D.Item22);
        }

        
        public static Pair<T> GetPair_1<T>(this IPair2D<T> pair2D)
        {
            return new Pair<T>(pair2D.Item11, pair2D.Item21);
        }

        
        public static Pair<T> GetPair_2<T>(this IPair2D<T> pair2D)
        {
            return new Pair<T>(pair2D.Item12, pair2D.Item22);
        }
        
        
        public static string ToText<T>(this IPair<T> pair)
        {
            return $"({pair.Item1}, {pair.Item2})";
        }
        
        
        public static string ToText<T>(this ITriplet<T> triplet)
        {
            return $"({triplet.Item1}, {triplet.Item2}, {triplet.Item3})";
        }
        
        
        public static string ToText<T>(this IQuad<T> quad)
        {
            return $"({quad.Item1}, {quad.Item2}, {quad.Item3}, {quad.Item4})";
        }

    
        /// <summary>
        /// https://bertdobbelaere.github.io/sorting_networks_extended.html
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns></returns>
        
        public static Pair<T> SortItems<T>(T item1, T item2)
            where T : IComparable
        {
            return item1.CompareTo(item2) <= 0
                ? new Pair<T>(item1, item2)
                : new Pair<T>(item2, item1);
        }
    
        /// <summary>
        /// https://bertdobbelaere.github.io/sorting_networks_extended.html
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pair"></param>
        /// <returns></returns>
        
        public static Pair<T> SortItems<T>(this IPair<T> pair)
            where T : IComparable
        {
            /* https://bertdobbelaere.github.io/sorting_networks_extended.html
               [(0,1)]
            */

            return SortItems(pair.Item1, pair.Item2);
        }

        /// <summary>
        /// https://bertdobbelaere.github.io/sorting_networks_extended.html
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="triplet"></param>
        /// <returns></returns>
        
        public static Triplet<T> SortItems<T>(this ITriplet<T> triplet) 
            where T : IComparable
        {
            var i0 = triplet.Item1;
            var i1 = triplet.Item2;
            var i2 = triplet.Item3;

            /* https://bertdobbelaere.github.io/sorting_networks_extended.html
               [(0,2)]
               [(0,1)]
               [(1,2)]
            */

            (i0, i2) = SortItems(i0, i2);
            (i0, i1) = SortItems(i0, i1);
            (i1, i2) = SortItems(i1, i2);

            return new Triplet<T>(i0, i1, i2);
        }
    
        /// <summary>
        /// https://bertdobbelaere.github.io/sorting_networks_extended.html
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="quad"></param>
        /// <returns></returns>
        
        public static Quad<T> SortItems<T>(this IQuad<T> quad) 
            where T : IComparable
        {
            var i0 = quad.Item1;
            var i1 = quad.Item2;
            var i2 = quad.Item3;
            var i3 = quad.Item4;

            /* https://bertdobbelaere.github.io/sorting_networks_extended.html
               [(0,2),(1,3)]
               [(0,1),(2,3)]
               [(1,2)]
            */

            (i0, i2) = SortItems(i0, i2);
            (i1, i3) = SortItems(i1, i3);

            (i0, i1) = SortItems(i0, i1);
            (i2, i3) = SortItems(i2, i3);

            (i1, i2) = SortItems(i1, i2);

            return new Quad<T>(i0, i1, i2, i3);
        }
    
        /// <summary>
        /// https://bertdobbelaere.github.io/sorting_networks_extended.html
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="quint"></param>
        /// <returns></returns>
        
        public static Quint<T> SortItems<T>(this IQuint<T> quint) 
            where T : IComparable
        {
            var i0 = quint.Item1;
            var i1 = quint.Item2;
            var i2 = quint.Item3;
            var i3 = quint.Item4;
            var i4 = quint.Item5;

            /* https://bertdobbelaere.github.io/sorting_networks_extended.html
               [(0,3),(1,4)]
               [(0,2),(1,3)]
               [(0,1),(2,4)]
               [(1,2),(3,4)]
               [(2,3)]
            */

            (i0, i3) = SortItems(i0, i3);
            (i1, i4) = SortItems(i1, i4);
        
            (i0, i2) = SortItems(i0, i2);
            (i1, i3) = SortItems(i1, i3);

            (i0, i1) = SortItems(i0, i1);
            (i2, i4) = SortItems(i2, i4);

            (i1, i2) = SortItems(i1, i2);
            (i3, i4) = SortItems(i3, i4);

            (i2, i3) = SortItems(i2, i3);

            return new Quint<T>(i0, i1, i2, i3, i4);
        }
    
        /// <summary>
        /// https://bertdobbelaere.github.io/sorting_networks_extended.html
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hexad"></param>
        /// <returns></returns>
        
        public static Hexad<T> SortItems<T>(this IHexad<T> hexad) 
            where T : IComparable
        {
            var i0 = hexad.Item1;
            var i1 = hexad.Item2;
            var i2 = hexad.Item3;
            var i3 = hexad.Item4;
            var i4 = hexad.Item5;
            var i5 = hexad.Item6;

            /* https://bertdobbelaere.github.io/sorting_networks_extended.html
               [(0,5),(1,3),(2,4)]
               [(1,2),(3,4)]
               [(0,3),(2,5)]
               [(0,1),(2,3),(4,5)]
               [(1,2),(3,4)]
            */
        
            (i0, i5) = SortItems(i0, i5);
            (i1, i3) = SortItems(i1, i3);
            (i2, i4) = SortItems(i2, i4);
        
            (i1, i2) = SortItems(i1, i2);
            (i3, i4) = SortItems(i3, i4);

            (i0, i3) = SortItems(i0, i3);
            (i2, i5) = SortItems(i2, i5);

            (i0, i1) = SortItems(i0, i1);
            (i2, i3) = SortItems(i2, i3);
            (i4, i5) = SortItems(i4, i5);
        
            (i1, i2) = SortItems(i1, i2);
            (i3, i4) = SortItems(i3, i4);

            return new Hexad<T>(i0, i1, i2, i3, i4, i5);
        }

    
        
        public static Pair<T> ReverseItems<T>(this IPair<T> pair)
        {
            return new Pair<T>(
                pair.Item2,
                pair.Item1
            );
        }

        
        public static Triplet<T> ReverseItems<T>(this ITriplet<T> triplet)
        {
            return new Triplet<T>(
                triplet.Item3,
                triplet.Item2,
                triplet.Item1
            );
        }
    
        
        public static Quad<T> ReverseItems<T>(this IQuad<T> quad)
        {
            return new Quad<T>(
                quad.Item4,
                quad.Item3,
                quad.Item2,
                quad.Item1
            );
        }
    
        
        public static Quint<T> ReverseItems<T>(this IQuint<T> quint)
        {
            return new Quint<T>(
                quint.Item5,
                quint.Item4,
                quint.Item3,
                quint.Item2,
                quint.Item1
            );
        }
    
        
        public static Hexad<T> ReverseItems<T>(this IHexad<T> hexad)
        {
            return new Hexad<T>(
                hexad.Item6,
                hexad.Item5,
                hexad.Item4,
                hexad.Item3,
                hexad.Item2,
                hexad.Item1
            );
        }


        
        public static T Min<T>(this IPair<T> pair)
            where T : IComparable
        {
            if (pair.Item1.CompareTo(pair.Item2) <= 0) return pair.Item1;

            return pair.Item2;
        }

        
        public static T Max<T>(this IPair<T> pair)
            where T : IComparable
        {
            var item = pair.Item1;

            if (item.CompareTo(pair.Item2) > 0) item = pair.Item2;
        
            return item;
        }

        
        public static T Min<T>(this ITriplet<T> triplet)
            where T : IComparable
        {
            var item = triplet.Item1;

            if (item.CompareTo(triplet.Item2) > 0) item = triplet.Item2;
            if (item.CompareTo(triplet.Item3) > 0) item = triplet.Item3;

            return item;
        }
    
        
        public static T Max<T>(this ITriplet<T> triplet)
            where T : IComparable
        {
            var item = triplet.Item1;

            if (item.CompareTo(triplet.Item2) < 0) item = triplet.Item2;
            if (item.CompareTo(triplet.Item3) < 0) item = triplet.Item3;

            return item;
        }
        
        
        public static T Min<T>(this IQuad<T> quad)
            where T : IComparable
        {
            var item = quad.Item1;

            if (item.CompareTo(quad.Item2) > 0) item = quad.Item2;
            if (item.CompareTo(quad.Item3) > 0) item = quad.Item3;
            if (item.CompareTo(quad.Item4) > 0) item = quad.Item4;

            return item;
        }
        
        
        public static T Max<T>(this IQuad<T> quad)
            where T : IComparable
        {
            var item = quad.Item1;

            if (item.CompareTo(quad.Item2) < 0) item = quad.Item2;
            if (item.CompareTo(quad.Item3) < 0) item = quad.Item3;
            if (item.CompareTo(quad.Item4) < 0) item = quad.Item4;

            return item;
        }
    
        
        public static T Min<T>(this IQuint<T> quint)
            where T : IComparable
        {
            var item = quint.Item1;

            if (item.CompareTo(quint.Item2) > 0) item = quint.Item2;
            if (item.CompareTo(quint.Item3) > 0) item = quint.Item3;
            if (item.CompareTo(quint.Item4) > 0) item = quint.Item4;
            if (item.CompareTo(quint.Item5) > 0) item = quint.Item5;

            return item;
        }
    
        
        public static T Max<T>(this IQuint<T> quint)
            where T : IComparable
        {
            var item = quint.Item1;

            if (item.CompareTo(quint.Item2) < 0) item = quint.Item2;
            if (item.CompareTo(quint.Item3) < 0) item = quint.Item3;
            if (item.CompareTo(quint.Item4) < 0) item = quint.Item4;
            if (item.CompareTo(quint.Item5) < 0) item = quint.Item5;

            return item;
        }
    
        
        public static T Min<T>(this IHexad<T> hexad)
            where T : IComparable
        {
            var item = hexad.Item1;

            if (item.CompareTo(hexad.Item2) > 0) item = hexad.Item2;
            if (item.CompareTo(hexad.Item3) > 0) item = hexad.Item3;
            if (item.CompareTo(hexad.Item4) > 0) item = hexad.Item4;
            if (item.CompareTo(hexad.Item5) > 0) item = hexad.Item5;
            if (item.CompareTo(hexad.Item6) > 0) item = hexad.Item6;

            return item;
        }
    
        
        public static T Max<T>(this IHexad<T> hexad)
            where T : IComparable
        {
            var item = hexad.Item1;

            if (item.CompareTo(hexad.Item2) < 0) item = hexad.Item2;
            if (item.CompareTo(hexad.Item3) < 0) item = hexad.Item3;
            if (item.CompareTo(hexad.Item4) < 0) item = hexad.Item4;
            if (item.CompareTo(hexad.Item5) < 0) item = hexad.Item5;
            if (item.CompareTo(hexad.Item6) < 0) item = hexad.Item6;

            return item;
        }


        
        public static Pair<T> GetItemPair<T>(this IReadOnlyList<T> itemArray, int index)
        {
            return new Pair<T>(
                itemArray[index],
                itemArray[index + 1]
            );
        }
        
        
        public static Triplet<T> GetItemTriplet<T>(this IReadOnlyList<T> itemArray, int index)
        {
            return new Triplet<T>(
                itemArray[index],
                itemArray[index + 1],
                itemArray[index + 2]
            );
        }

        
        public static Quad<T> GetItemQuad<T>(this IReadOnlyList<T> itemArray, int index)
        {
            return new Quad<T>(
                itemArray[index],
                itemArray[index + 1],
                itemArray[index + 2],
                itemArray[index + 3]
            );
        }

        
        public static Quint<T> GetItemQuint<T>(this IReadOnlyList<T> itemArray, int index)
        {
            return new Quint<T>(
                itemArray[index],
                itemArray[index + 1],
                itemArray[index + 2],
                itemArray[index + 3],
                itemArray[index + 4]
            );
        }

        
        public static Hexad<T> GetItemHexad<T>(this IReadOnlyList<T> itemArray, int index)
        {
            return new Hexad<T>(
                itemArray[index],
                itemArray[index + 1],
                itemArray[index + 2],
                itemArray[index + 3],
                itemArray[index + 4],
                itemArray[index + 5]
            );
        }
    }
}