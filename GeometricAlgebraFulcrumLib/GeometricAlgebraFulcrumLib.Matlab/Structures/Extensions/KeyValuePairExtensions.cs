using System;
using System.Collections.Generic;
using System.Linq;

namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions
{
    public static class KeyValuePairExtensions
    {
        
        public static Tuple<TKey, TValue> ToTuple<TKey, TValue>(this KeyValuePair<TKey, TValue> keyValuePair)
        {
            return new Tuple<TKey, TValue>(
                keyValuePair.Key,
                keyValuePair.Value
            );
        }

        
        public static IEnumerable<Tuple<TKey, TValue>> ToTuples<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairList)
        {
            return keyValuePairList.Select(keyValuePair => 
                new Tuple<TKey, TValue>(
                    keyValuePair.Key,
                    keyValuePair.Value
                )
            );
        }
        
        
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairList) where TKey : notnull
        {
            return keyValuePairList.ToDictionary(
                keyValuePair => keyValuePair.Key,
                keyValuePair => keyValuePair.Value
            );
        }


        
        public static KeyValuePair<TKey, TValue> ToKeyValuePair<TKey, TValue>(this Tuple<TKey, TValue> tuple)
        {
            return new KeyValuePair<TKey, TValue>(
                tuple.Item1,
                tuple.Item2
            );
        }
        
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> ToKeyValuePairs<TKey, TValue>(this IEnumerable<Tuple<TKey, TValue>> tupleList)
        {
            return tupleList.Select(tuple => 
                new KeyValuePair<TKey, TValue>(
                    tuple.Item1,
                    tuple.Item2
                )
            );
        }
        
        
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<Tuple<TKey, TValue>> keyValuePairList) where TKey : notnull
        {
            return keyValuePairList.ToDictionary(
                keyValuePair => keyValuePair.Item1,
                keyValuePair => keyValuePair.Item2
            );
        }

    }
}
