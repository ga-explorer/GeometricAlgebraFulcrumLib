using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions
{
    public static class KeyValuePairExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<TKey, TValue> ToTuple<TKey, TValue>(this KeyValuePair<TKey, TValue> keyValuePair)
        {
            return new Tuple<TKey, TValue>(
                keyValuePair.Key,
                keyValuePair.Value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Tuple<TKey, TValue>> ToTuples<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairList)
        {
            return keyValuePairList.Select(keyValuePair => 
                new Tuple<TKey, TValue>(
                    keyValuePair.Key,
                    keyValuePair.Value
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairList) where TKey : notnull
        {
            return keyValuePairList.ToDictionary(
                keyValuePair => keyValuePair.Key,
                keyValuePair => keyValuePair.Value
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue> ToKeyValuePair<TKey, TValue>(this Tuple<TKey, TValue> tuple)
        {
            return new KeyValuePair<TKey, TValue>(
                tuple.Item1,
                tuple.Item2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<TKey, TValue>> ToKeyValuePairs<TKey, TValue>(this IEnumerable<Tuple<TKey, TValue>> tupleList)
        {
            return tupleList.Select(keyValuePair => 
                new KeyValuePair<TKey, TValue>(
                    keyValuePair.Item1,
                    keyValuePair.Item2
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<Tuple<TKey, TValue>> keyValuePairList) where TKey : notnull
        {
            return keyValuePairList.ToDictionary(
                keyValuePair => keyValuePair.Item1,
                keyValuePair => keyValuePair.Item2
            );
        }


    }
}
