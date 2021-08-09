using System;
using System.Collections.Generic;
using System.Linq;

namespace GeometricAlgebraFulcrumLib.Storage.Terms
{
    public static class GaTermUtils
    {
        public static IEnumerable<GaTerm<T>> IdScalarToTermUniform<T>(this IEnumerable<KeyValuePair<ulong, T>> pairsList)
        {
            return pairsList.Select(pair => GaTerm<T>.CreateUniform(pair.Key, pair.Value));
        }

        public static IEnumerable<GaTerm<T>> IdScalarToTermGraded<T>(this IEnumerable<KeyValuePair<ulong, T>> pairsList)
        {
            return pairsList.Select(pair => GaTerm<T>.CreateGraded(pair.Key, pair.Value));
        }

        public static IEnumerable<GaTerm<T>> IdScalarToTermFull<T>(this IEnumerable<KeyValuePair<ulong, T>> pairsList)
        {
            return pairsList.Select(pair => GaTerm<T>.CreateFull(pair.Key, pair.Value));
        }


        public static IEnumerable<GaTerm<T>> IndexScalarToTermUniform<T>(this IEnumerable<KeyValuePair<ulong, T>> pairsList, uint grade)
        {
            return pairsList.Select(pair => GaTerm<T>.CreateUniform(grade, pair.Key, pair.Value));
        }

        public static IEnumerable<GaTerm<T>> IndexScalarToTermGraded<T>(this IEnumerable<KeyValuePair<ulong, T>> pairsList, uint grade)
        {
            return pairsList.Select(pair => GaTerm<T>.CreateGraded(grade, pair.Key, pair.Value));
        }

        public static IEnumerable<GaTerm<T>> IndexScalarToTermFull<T>(this IEnumerable<KeyValuePair<ulong, T>> pairsList, uint grade)
        {
            return pairsList.Select(pair => GaTerm<T>.CreateFull(grade, pair.Key, pair.Value));
        }

        public static IEnumerable<GaTerm<T>> IndexScalarToTermVector<T>(this IEnumerable<KeyValuePair<ulong, T>> pairsList)
        {
            return pairsList.Select(pair => GaTerm<T>.CreateVector(pair.Key, pair.Value));
        }

        public static IEnumerable<GaTerm<T>> IndexScalarToTermBivector<T>(this IEnumerable<KeyValuePair<ulong, T>> pairsList)
        {
            return pairsList.Select(pair => GaTerm<T>.CreateBivector(pair.Key, pair.Value));
        }


        public static IEnumerable<GaTerm<T>> IndexScalarToTermUniform<T>(this IEnumerable<Tuple<ulong, T>> pairsList, uint grade)
        {
            return pairsList.Select(tuple => GaTerm<T>.CreateUniform(grade, tuple.Item1, tuple.Item2));
        }

        public static IEnumerable<GaTerm<T>> IndexScalarToTermGraded<T>(this IEnumerable<Tuple<ulong, T>> pairsList, uint grade)
        {
            return pairsList.Select(tuple => GaTerm<T>.CreateGraded(grade, tuple.Item1, tuple.Item2));
        }

        public static IEnumerable<GaTerm<T>> IndexScalarToTermFull<T>(this IEnumerable<Tuple<ulong, T>> pairsList, uint grade)
        {
            return pairsList.Select(tuple => GaTerm<T>.CreateFull(grade, tuple.Item1, tuple.Item2));
        }

        public static IEnumerable<GaTerm<T>> IndexScalarToTermVector<T>(this IEnumerable<Tuple<ulong, T>> pairsList)
        {
            return pairsList.Select(tuple => GaTerm<T>.CreateVector(tuple.Item1, tuple.Item2));
        }

        public static IEnumerable<GaTerm<T>> IndexScalarToTermBivector<T>(this IEnumerable<Tuple<ulong, T>> pairsList)
        {
            return pairsList.Select(tuple => GaTerm<T>.CreateBivector(tuple.Item1, tuple.Item2));
        }


        public static IEnumerable<GaTerm<T>> GradeIndexScalarToTermUniform<T>(this IEnumerable<Tuple<uint, ulong, T>> tuplesList)
        {
            return tuplesList.Select(tuple => GaTerm<T>.CreateUniform(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        public static IEnumerable<GaTerm<T>> GradeIndexScalarToTermGraded<T>(this IEnumerable<Tuple<uint, ulong, T>> tuplesList)
        {
            return tuplesList.Select(tuple => GaTerm<T>.CreateGraded(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        public static IEnumerable<GaTerm<T>> GradeIndexScalarToTermFull<T>(this IEnumerable<Tuple<uint, ulong, T>> tuplesList)
        {
            return tuplesList.Select(tuple => GaTerm<T>.CreateFull(tuple.Item1, tuple.Item2, tuple.Item3));
        }
    }
}