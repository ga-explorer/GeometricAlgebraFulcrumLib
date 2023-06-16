using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            return dict.TryGetValue(key, out var value)
                ? value
                : defaultValue;
        }

        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
                return false;

            dict.Add(
                new KeyValuePair<TKey, TValue>(key, value)
            );

            return true;
        }
        
        public static void AddOrSet<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
                dict[key] = value;
            else
                dict.Add(key, value);
        }

        public static void AddOrSet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
                dict[key] = value;
            else
                dict.Add(
                    new KeyValuePair<TKey, TValue>(key, value)
                );
        }

        public static void Remove<TKey, TValue>(this IDictionary<TKey, TValue> dict, IEnumerable<TKey> keysList)
        {
            var keysArray = keysList.ToArray();

            foreach (var key in keysArray)
                dict.Remove(key);
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> pairsList)
        {
            return pairsList.ToDictionary(
                pair => pair.Key,
                pair => pair.Value
            );
        }

        public static Dictionary<TKey, TValue2> ToDictionary<TKey, TValue1, TValue2>(this IEnumerable<KeyValuePair<TKey, TValue1>> pairsList, Func<TValue1, TValue2> mappingFunc)
        {
            return pairsList.ToDictionary(
                pair => pair.Key,
                pair => mappingFunc(pair.Value)
            );
        }

        public static Dictionary<TKey, TValue2> ToDictionary<TKey, TValue1, TValue2>(this IEnumerable<KeyValuePair<TKey, TValue1>> pairsList, Func<TKey, TValue1, TValue2> mappingFunc)
        {
            return pairsList.ToDictionary(
                pair => pair.Key,
                pair => mappingFunc(pair.Key, pair.Value)
            );
        }

        public static void UpdateValues<TKey, TValue>(this IDictionary<TKey, TValue> dict, Func<TValue, TValue> mappingFunc)
        {
            foreach (var pair in dict)
                dict[pair.Key] = mappingFunc(pair.Value);
        }
        

        /// <summary>
        /// Returns all pairs from the left dictionary not present in the right dictionary 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <param name="dictionary1"></param>
        /// <param name="dictionary2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<TKey, TValue1>> LeftAntiJoin<TKey, TValue1, TValue2>(this IReadOnlyDictionary<TKey, TValue1> dictionary1, IReadOnlyDictionary<TKey, TValue2> dictionary2)
        {
            return dictionary1
                .Where(pair1 => !dictionary2.ContainsKey(pair1.Key));
        }

        /// <summary>
        /// Returns all pairs from the right dictionary not present in the left dictionary 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <param name="dictionary1"></param>
        /// <param name="dictionary2"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<TKey, TValue2>> RightAntiJoin<TKey, TValue1, TValue2>(this IReadOnlyDictionary<TKey, TValue1> dictionary1, IReadOnlyDictionary<TKey, TValue2> dictionary2)
        {
            return dictionary2
                .Where(pair2 => !dictionary1.ContainsKey(pair2.Key));
        }

        /// <summary>
        /// Returns all pairs from the left dictionary even if not present in the right dictionary 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <param name="dictionary1"></param>
        /// <param name="dictionary2"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<TKey, TValue1, TValue2>> LeftOuterJoin<TKey, TValue1, TValue2>(this IReadOnlyDictionary<TKey, TValue1> dictionary1, IReadOnlyDictionary<TKey, TValue2> dictionary2)
        {
            foreach (var pair1 in dictionary1)
            {
                if (dictionary2.TryGetValue(pair1.Key, out var value2))
                    yield return new Tuple<TKey, TValue1, TValue2>(pair1.Key, pair1.Value, value2);
                else
                    yield return new Tuple<TKey, TValue1, TValue2>(pair1.Key, pair1.Value, default);
            }
        }

        /// <summary>
        /// Returns all pairs from the left dictionary even if not present in the right dictionary 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <param name="dictionary1"></param>
        /// <param name="dictionary2"></param>
        /// <param name="defaultValue2"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<TKey, TValue1, TValue2>> LeftOuterJoin<TKey, TValue1, TValue2>(this IReadOnlyDictionary<TKey, TValue1> dictionary1, IReadOnlyDictionary<TKey, TValue2> dictionary2, TValue2 defaultValue2)
        {
            foreach (var pair1 in dictionary1)
            {
                if (dictionary2.TryGetValue(pair1.Key, out var value2))
                    yield return new Tuple<TKey, TValue1, TValue2>(pair1.Key, pair1.Value, value2);
                else
                    yield return new Tuple<TKey, TValue1, TValue2>(pair1.Key, pair1.Value, defaultValue2);
            }
        }

        /// <summary>
        /// Returns all pairs from the left dictionary even if not present in the right dictionary 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <typeparam name="TValue3"></typeparam>
        /// <param name="dictionary1"></param>
        /// <param name="dictionary2"></param>
        /// <param name="defaultValue2"></param>
        /// <param name="mappingFunc"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<TKey, TValue3>> LeftOuterJoin<TKey, TValue1, TValue2, TValue3>(this IReadOnlyDictionary<TKey, TValue1> dictionary1, IReadOnlyDictionary<TKey, TValue2> dictionary2, TValue2 defaultValue2, Func<TValue1, TValue2, TValue3> mappingFunc)
        {
            foreach (var pair1 in dictionary1)
            {
                if (dictionary2.TryGetValue(pair1.Key, out var value2))
                    yield return new KeyValuePair<TKey, TValue3>(pair1.Key, mappingFunc(pair1.Value, value2));
                else
                    yield return new KeyValuePair<TKey, TValue3>(pair1.Key, mappingFunc(pair1.Value, defaultValue2));
            }
        }

        /// <summary>
        /// Returns all pairs from the right dictionary even if not present in the left dictionary 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <param name="dictionary1"></param>
        /// <param name="dictionary2"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<TKey, TValue1, TValue2>> RightOuterJoin<TKey, TValue1, TValue2>(this IReadOnlyDictionary<TKey, TValue1> dictionary1, IReadOnlyDictionary<TKey, TValue2> dictionary2)
        {
            foreach (var pair2 in dictionary2)
            {
                if (dictionary1.TryGetValue(pair2.Key, out var value1))
                    yield return new Tuple<TKey, TValue1, TValue2>(pair2.Key, value1, pair2.Value);
                else
                    yield return new Tuple<TKey, TValue1, TValue2>(pair2.Key, default, pair2.Value);
            }
        }
        
        /// <summary>
        /// Returns all pairs from the right dictionary even if not present in the left dictionary 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <param name="dictionary1"></param>
        /// <param name="dictionary2"></param>
        /// <param name="defaultValue1"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<TKey, TValue1, TValue2>> RightOuterJoin<TKey, TValue1, TValue2>(this IReadOnlyDictionary<TKey, TValue1> dictionary1, IReadOnlyDictionary<TKey, TValue2> dictionary2, TValue1 defaultValue1)
        {
            foreach (var pair2 in dictionary2)
            {
                if (dictionary1.TryGetValue(pair2.Key, out var value1))
                    yield return new Tuple<TKey, TValue1, TValue2>(pair2.Key, value1, pair2.Value);
                else
                    yield return new Tuple<TKey, TValue1, TValue2>(pair2.Key, defaultValue1, pair2.Value);
            }
        }

        /// <summary>
        /// Returns all pairs from the right dictionary even if not present in the left dictionary 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <typeparam name="TValue3"></typeparam>
        /// <param name="dictionary1"></param>
        /// <param name="dictionary2"></param>
        /// <param name="mappingFunc"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<TKey, TValue3>> RightOuterJoin<TKey, TValue1, TValue2, TValue3>(this IReadOnlyDictionary<TKey, TValue1> dictionary1, IReadOnlyDictionary<TKey, TValue2> dictionary2, Func<TValue1, TValue2, TValue3> mappingFunc)
        {
            foreach (var pair2 in dictionary2)
            {
                if (dictionary1.TryGetValue(pair2.Key, out var value1))
                    yield return new KeyValuePair<TKey, TValue3>(pair2.Key, mappingFunc(value1, pair2.Value));
                else
                    yield return new KeyValuePair<TKey, TValue3>(pair2.Key, mappingFunc(default, pair2.Value));
            }
        }

        /// <summary>
        /// Returns all pairs from the right dictionary even if not present in the left dictionary 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <typeparam name="TValue3"></typeparam>
        /// <param name="dictionary1"></param>
        /// <param name="dictionary2"></param>
        /// <param name="defaultValue1"></param>
        /// <param name="mappingFunc"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<TKey, TValue3>> RightOuterJoin<TKey, TValue1, TValue2, TValue3>(this IReadOnlyDictionary<TKey, TValue1> dictionary1, IReadOnlyDictionary<TKey, TValue2> dictionary2, TValue1 defaultValue1, Func<TValue1, TValue2, TValue3> mappingFunc)
        {
            foreach (var pair2 in dictionary2)
            {
                if (dictionary1.TryGetValue(pair2.Key, out var value1))
                    yield return new KeyValuePair<TKey, TValue3>(pair2.Key, mappingFunc(value1, pair2.Value));
                else
                    yield return new KeyValuePair<TKey, TValue3>(pair2.Key, mappingFunc(defaultValue1, pair2.Value));
            }
        }

        /// <summary>
        /// Returns all pairs from the left and right dictionaries even if not present in the other dictionary 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <param name="dictionary1"></param>
        /// <param name="dictionary2"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<TKey, TValue1, TValue2>> FullOuterJoin<TKey, TValue1, TValue2>(this IReadOnlyDictionary<TKey, TValue1> dictionary1, IReadOnlyDictionary<TKey, TValue2> dictionary2)
        {
            foreach (var (key1, value1) in dictionary1)
            {
                if (dictionary2.TryGetValue(key1, out var value2))
                    yield return new Tuple<TKey, TValue1, TValue2>(key1, value1, value2);
                else
                    yield return new Tuple<TKey, TValue1, TValue2>(key1, value1, default);
            }

            foreach (var (key2, value2) in dictionary2)
                if (!dictionary1.ContainsKey(key2))
                    yield return new Tuple<TKey, TValue1, TValue2>(key2, default, value2);
        }
        
        /// <summary>
        /// Returns all pairs from the left and right dictionaries even if not present in the other dictionary 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <param name="dictionary1"></param>
        /// <param name="dictionary2"></param>
        /// <param name="defaultValue1"></param>
        /// <param name="defaultValue2"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<TKey, TValue1, TValue2>> FullOuterJoin<TKey, TValue1, TValue2>(this IReadOnlyDictionary<TKey, TValue1> dictionary1, IReadOnlyDictionary<TKey, TValue2> dictionary2, TValue1 defaultValue1, TValue2 defaultValue2)
        {
            foreach (var pair1 in dictionary1)
            {
                if (dictionary2.TryGetValue(pair1.Key, out var value2))
                    yield return new Tuple<TKey, TValue1, TValue2>(pair1.Key, pair1.Value, value2);
                else
                    yield return new Tuple<TKey, TValue1, TValue2>(pair1.Key, pair1.Value, defaultValue2);
            }

            foreach (var pair2 in dictionary2)
                if (!dictionary1.ContainsKey(pair2.Key))
                    yield return new Tuple<TKey, TValue1, TValue2>(pair2.Key, defaultValue1, pair2.Value);
        }
        
        /// <summary>
        /// Returns all pairs from the left and right dictionaries even if not present in the other dictionary 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <typeparam name="TValue3"></typeparam>
        /// <param name="dictionary1"></param>
        /// <param name="dictionary2"></param>
        /// <param name="mappingFunc"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<TKey, TValue3>> FullOuterJoin<TKey, TValue1, TValue2, TValue3>(this IReadOnlyDictionary<TKey, TValue1> dictionary1, IReadOnlyDictionary<TKey, TValue2> dictionary2, Func<TValue1, TValue2, TValue3> mappingFunc)
        {
            foreach (var pair1 in dictionary1)
            {
                if (dictionary2.TryGetValue(pair1.Key, out var value2))
                    yield return new KeyValuePair<TKey, TValue3>(pair1.Key, mappingFunc(pair1.Value, value2));
                else
                    yield return new KeyValuePair<TKey, TValue3>(pair1.Key, mappingFunc(pair1.Value, default));
            }

            foreach (var pair2 in dictionary2)
                if (!dictionary1.ContainsKey(pair2.Key))
                    yield return new KeyValuePair<TKey, TValue3>(pair2.Key, mappingFunc(default, pair2.Value));
        }
        
        /// <summary>
        /// Returns all pairs from the left and right dictionaries even if not present in the other dictionary 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <typeparam name="TValue3"></typeparam>
        /// <param name="dictionary1"></param>
        /// <param name="dictionary2"></param>
        /// <param name="defaultValue1"></param>
        /// <param name="defaultValue2"></param>
        /// <param name="mappingFunc"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<TKey, TValue3>> FullOuterJoin<TKey, TValue1, TValue2, TValue3>(this IReadOnlyDictionary<TKey, TValue1> dictionary1, IReadOnlyDictionary<TKey, TValue2> dictionary2, TValue1 defaultValue1, TValue2 defaultValue2, Func<TValue1, TValue2, TValue3> mappingFunc)
        {
            //var queryResult = 
            //    dictionary1
            //        .GroupJoin(
            //            dictionary2, 
            //            leftElements => leftElements.Key, 
            //            rightElements => rightElements.Key,
            //            (leftElements, joinResult) => 
            //                new { leftElements, joinResult }
            //        )
            //        .SelectMany(@t => 
            //            @t.joinResult.DefaultIfEmpty(),
            //            (@t, result) => new { @t.leftElements.Key, result.Value }
            //        );

            foreach (var pair1 in dictionary1)
            {
                if (dictionary2.TryGetValue(pair1.Key, out var value2))
                    yield return new KeyValuePair<TKey, TValue3>(pair1.Key, mappingFunc(pair1.Value, value2));
                else
                    yield return new KeyValuePair<TKey, TValue3>(pair1.Key, mappingFunc(pair1.Value, defaultValue2));
            }

            foreach (var pair2 in dictionary2)
                if (!dictionary1.ContainsKey(pair2.Key))
                    yield return new KeyValuePair<TKey, TValue3>(pair2.Key, mappingFunc(defaultValue1, pair2.Value));
        }

        /// <summary>
        /// Returns all pairs from the left dictionary also present in the right dictionary 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <param name="dictionary1"></param>
        /// <param name="dictionary2"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<TKey, TValue1, TValue2>> InnerJoin<TKey, TValue1, TValue2>(this IReadOnlyDictionary<TKey, TValue1> dictionary1, IReadOnlyDictionary<TKey, TValue2> dictionary2)
        {
            foreach (var pair1 in dictionary1)
            {
                if (dictionary2.TryGetValue(pair1.Key, out var value2))
                    yield return new Tuple<TKey, TValue1, TValue2>(pair1.Key, pair1.Value, value2);
            }
        }

        /// <summary>
        /// Returns all pairs from the left dictionary also present in the right dictionary 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <typeparam name="TValue3"></typeparam>
        /// <param name="dictionary1"></param>
        /// <param name="dictionary2"></param>
        /// <param name="mappingFunc"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<TKey, TValue3>> InnerJoin<TKey, TValue1, TValue2, TValue3>(this IReadOnlyDictionary<TKey, TValue1> dictionary1, IReadOnlyDictionary<TKey, TValue2> dictionary2, Func<TValue1, TValue2, TValue3> mappingFunc)
        {
            foreach (var pair1 in dictionary1)
            {
                if (dictionary2.TryGetValue(pair1.Key, out var value2))
                    yield return new KeyValuePair<TKey, TValue3>(
                        pair1.Key, 
                        mappingFunc(pair1.Value, value2)
                    );
            }
        }

        
        public static KeyValuePair<TKey, TValue> First<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> pairsList, Func<TValue, TValue> valueMapping)
        {
            var pair = pairsList.First();

            return new KeyValuePair<TKey, TValue>(
                pair.Key, 
                valueMapping(pair.Value)
            );
        }

    }
}
