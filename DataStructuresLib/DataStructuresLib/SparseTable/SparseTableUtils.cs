using System;
using System.Collections.Generic;

namespace DataStructuresLib.SparseTable
{
    public static class SparseTableUtils
    {

        public static SparseTable1D<TKey, TValue> ToSparseTable<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            return new SparseTable1D<TKey, TValue>(pairs);
        }

        public static SparseTable1D<TKey, TValue> ToSparseTable<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> pairs, TValue defaultValue)
        {
            return new SparseTable1D<TKey, TValue>(defaultValue, pairs);
        }

        public static SparseTable2D<TKey1, TKey2, TValue> ToSparseTable<TKey1, TKey2, TValue>(
            this IEnumerable<KeyValuePair<Tuple<TKey1, TKey2>, TValue>> pairs)
        {
            return new SparseTable2D<TKey1, TKey2, TValue>(pairs);
        }

        public static SparseTable2D<TKey1, TKey2, TValue> ToSparseTable<TKey1, TKey2, TValue>(
            this IEnumerable<KeyValuePair<Tuple<TKey1, TKey2>, TValue>> pairs, TValue defaultValue)
        {
            return new SparseTable2D<TKey1, TKey2, TValue>(defaultValue, pairs);
        }

        public static SparseTable3D<TKey1, TKey2, TKey3, TValue> ToSparseTable<TKey1, TKey2, TKey3, TValue>(
            this IEnumerable<KeyValuePair<Tuple<TKey1, TKey2, TKey3>, TValue>> pairs)
        {
            return new SparseTable3D<TKey1, TKey2, TKey3, TValue>(pairs);
        }

        public static SparseTable3D<TKey1, TKey2, TKey3, TValue> ToSparseTable<TKey1, TKey2, TKey3, TValue>(
            this IEnumerable<KeyValuePair<Tuple<TKey1, TKey2, TKey3>, TValue>> pairs, TValue defaultValue)
        {
            return new SparseTable3D<TKey1, TKey2, TKey3, TValue>(defaultValue, pairs);
        }
    }
}
