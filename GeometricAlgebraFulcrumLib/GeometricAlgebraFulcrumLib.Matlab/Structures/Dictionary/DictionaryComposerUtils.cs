using System;
using System.Collections.Generic;
using System.Linq;

namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;

public static class DictionaryComposerUtils
{
    
    public static EmptyDictionary<TKey, TValue> CreateEmptyDictionary<TKey, TValue>() 
        where TKey : IEquatable<TKey>
    {
        return new EmptyDictionary<TKey, TValue>();
    }

    
    public static SingleItemDictionary<TKey, TValue> CreateSingleItemDictionary<TKey, TValue>(this KeyValuePair<TKey, TValue> keyValuePair) 
        where TKey : IEquatable<TKey>
    {
        return new SingleItemDictionary<TKey, TValue>(
            keyValuePair.Key,
            keyValuePair.Value
        );
    }
    
    
    public static SingleItemDictionary<TKey, TValue> CreateSingleItemDictionary<TKey, TValue>(this TKey key, TValue value) 
        where TKey : IEquatable<TKey>
    {
        return new SingleItemDictionary<TKey, TValue>(key, value);
    }
    
    
    public static RepeatedItemInt32Dictionary<TValue> CreateRepeatedItemDictionary<TValue>(this TValue value, int count) 
    {
        return new RepeatedItemInt32Dictionary<TValue>(count, value);
    }

    
    public static RepeatedItemDictionary<TKey, TValue> CreateRepeatedItemDictionary<TKey, TValue>(this TValue value, int count, Func<TKey, int> keyToIntegerMapping, Func<int, TKey> integerToKeyMapping) 
        where TKey : IEquatable<TKey>
    {
        return new RepeatedItemDictionary<TKey, TValue>(
            count,
            value,
            keyToIntegerMapping,
            integerToKeyMapping
        );
    }
    
    
    public static DenseInt32Dictionary<TValue> CreateDenseDictionary<TValue>(this IReadOnlyList<TValue> valueList)
    {
        return new DenseInt32Dictionary<TValue>(valueList);
    }

    
    public static DenseDictionary<TKey, TValue> CreateDenseDictionary<TKey, TValue>(this IReadOnlyList<TValue> valueList, Func<TKey, int> keyToIntegerMapping, Func<int, TKey> integerToKeyMapping)
        where TKey : IEquatable<TKey>
    {
        return new DenseDictionary<TKey, TValue>(
            valueList, 
            keyToIntegerMapping, 
            integerToKeyMapping
        );
    }


    public static Dictionary<int, TValue> AddValues<TValue>(this Dictionary<int, TValue> inputDictionary, IEnumerable<TValue> valueList, Func<TValue, bool> keepValue)
    {
        var index = 0;

        foreach (var value in valueList)
        {
            if (keepValue(value))
                inputDictionary.Add(index, value);

            index++;
        }

        return inputDictionary;
    }
    
    public static Dictionary<int, TValue> AddValues<TValue>(this Dictionary<int, TValue> inputDictionary, IEnumerable<TValue> valueList, Func<int, TValue, bool> keepValue)
    {
        var index = 0;

        foreach (var value in valueList)
        {
            if (keepValue(index, value))
                inputDictionary.Add(index, value);

            index++;
        }

        return inputDictionary;
    }
    
    public static Dictionary<TKey, TValue> AddValues<TKey, TValue>(this Dictionary<TKey, TValue> inputDictionary, IEnumerable<TValue> valueList, Func<TValue, bool> keepValue, Func<int, TKey> indexToKeyMapping)
    {
        var index = 0;

        foreach (var value in valueList)
        {
            if (keepValue(value))
            {
                var key = indexToKeyMapping(index);

                inputDictionary.Add(key, value);
            }

            index++;
        }
        
        return inputDictionary;
    }
    
    public static Dictionary<TKey, TValue> AddValues<TKey, TValue>(this Dictionary<TKey, TValue> inputDictionary, IEnumerable<TValue> valueList, Func<int, TValue, bool> keepValue, Func<int, TKey> indexToKeyMapping)
    {
        var index = 0;

        foreach (var value in valueList)
        {
            if (keepValue(index, value))
            {
                var key = indexToKeyMapping(index);

                inputDictionary.Add(key, value);
            }

            index++;
        }
        
        return inputDictionary;
    }

    
    //
    //public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> keyValueList)
    //{
    //    var dict = new Dictionary<TKey, TValue>();

    //    foreach (var (key, value) in keyValueList)
    //        dict.Add(key, value);

    //    return dict;
    //}

    
    public static Dictionary<int, TValue> ToDictionary<TValue>(this IEnumerable<TValue> valueList, Func<TValue, bool> keepValue)
    {
        return new Dictionary<int, TValue>().AddValues(valueList, keepValue);
    }
    
    
    public static Dictionary<int, TValue> ToDictionary<TValue>(this IEnumerable<TValue> valueList, Func<int, TValue, bool> keepValue)
    {
        return new Dictionary<int, TValue>().AddValues(valueList, keepValue);
    }

    
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<TValue> valueList, Func<TValue, bool> keepValue, Func<int, TKey> indexToKeyMapping)
    {
        return new Dictionary<TKey, TValue>().AddValues(valueList, keepValue, indexToKeyMapping);
    }
    
    
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<TValue> valueList, Func<TValue, bool> keepValue, Func<int, TKey> indexToKeyMapping, IEqualityComparer<TKey> keyEqualityComparer)
    {
        return new Dictionary<TKey, TValue>(keyEqualityComparer).AddValues(valueList, keepValue, indexToKeyMapping);
    }

    
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<TValue> valueList, Func<int, TValue, bool> keepValue, Func<int, TKey> indexToKeyMapping)
    {
        return new Dictionary<TKey, TValue>().AddValues(valueList, keepValue, indexToKeyMapping);
    }
    
    
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<TValue> valueList, Func<int, TValue, bool> keepValue, Func<int, TKey> indexToKeyMapping, IEqualityComparer<TKey> keyEqualityComparer)
    {
        return new Dictionary<TKey, TValue>(keyEqualityComparer).AddValues(valueList, keepValue, indexToKeyMapping);
    }


    
    public static IReadOnlyDictionary<int, TValue> ToSimpleDictionary<TValue>(this IEnumerable<TValue> valueList, Func<TValue, bool> keepValue)
    {
        return valueList
            .ToDictionary(keepValue)
            .ToSimpleDictionary();
    }
    
    
    public static IReadOnlyDictionary<int, TValue> ToSimpleDictionary<TValue>(this IEnumerable<TValue> valueList, Func<int, TValue, bool> keepValue)
    {
        return valueList
            .ToDictionary(keepValue)
            .ToSimpleDictionary();
    }

    
    public static IReadOnlyDictionary<TKey, TValue> ToSimpleDictionary<TKey, TValue>(this IEnumerable<TValue> valueList, Func<TValue, bool> keepValue, Func<int, TKey> indexToKeyMapping) 
        where TKey : IEquatable<TKey>
    {
        return valueList
            .ToDictionary(keepValue, indexToKeyMapping)
            .ToSimpleDictionary();
    }
    
    
    public static IReadOnlyDictionary<TKey, TValue> ToSimpleDictionary<TKey, TValue>(this IEnumerable<TValue> valueList, Func<int, TValue, bool> keepValue, Func<int, TKey> indexToKeyMapping) 
        where TKey : IEquatable<TKey>
    {
        return valueList
            .ToDictionary(keepValue, indexToKeyMapping)
            .ToSimpleDictionary();
    }

    
    public static IReadOnlyDictionary<TKey, TValue> ToSimpleDictionary<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> inputDictionary) 
        where TKey : IEquatable<TKey>
    {
        return inputDictionary.Count switch
        {
            0 => inputDictionary is EmptyDictionary<TKey, TValue>
                ? inputDictionary
                : new EmptyDictionary<TKey, TValue>(),

            1 => inputDictionary is SingleItemDictionary<TKey, TValue>
                ? inputDictionary
                : new SingleItemDictionary<TKey, TValue>(
                    inputDictionary.First()
                ),

            _ => inputDictionary
        };
    }
}