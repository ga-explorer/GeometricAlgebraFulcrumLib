using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Structures.Factories
{
    public static class GaRecordsFactory
    {
        public static GaRecordGradeKey ZeroGridKey { get; }
            = new GaRecordGradeKey(0, 0);

        public static GaRecordGradeKeyPair ZeroGridKeyPair { get; }
            = new GaRecordGradeKeyPair(0, 0, 0);

        public static GaRecordKeyPair ZeroKeyPair { get; }
            = new GaRecordKeyPair(0, 0);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordGradeKeyValue<T> CreateRecordGradeKeyValue<T>(this GaRecordGradeKey record, T value)
        {
            return new GaRecordGradeKeyValue<T>(record.Grade, record.Key, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordGradeKeyPairValue<T> CreateRecordGradeKeyValue<T>(this GaRecordGradeKeyPair record, T value)
        {
            return new GaRecordGradeKeyPairValue<T>(record.Grade, record.Key1, record.Key2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyPairValue<T> CreateRecordKeyValue<T>(this GaRecordKeyPair record, T value)
        {
            return new GaRecordKeyPairValue<T>(record.Key1, record.Key2, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyValue<T> ToKeyValueRecord<T>(this KeyValuePair<ulong, T> keyValuePair)
        {
            var (key, value) = keyValuePair;

            return new GaRecordKeyValue<T>(key, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> ToKeyValueRecords<T>(this IEnumerable<KeyValuePair<ulong, T>> keyValuePairs)
        {
            return keyValuePairs.Select(ToKeyValueRecord);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<GaRecordKeyValue<T>> records)
        {
            return records.ToDictionary(
                record => record.Key,
                record => record.Value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<GaRecordKeyValue<T>> records, Func<ulong, ulong> keyMapping)
        {
            return records.ToDictionary(
                record => keyMapping(record.Key),
                record => record.Value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<GaRecordKeyValue<T>> records, Func<ulong, ulong> keyMapping, Func<T, T> valueMapping)
        {
            return records.ToDictionary(
                record => keyMapping(record.Key),
                record => valueMapping(record.Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<GaRecordKeyValue<T>> records, Func<ulong, ulong> keyMapping, Func<ulong, T, T> valueMapping)
        {
            return records.ToDictionary(
                record => keyMapping(record.Key),
                record => valueMapping(record.Key, record.Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<GaRecordKeyValue<T>> records, Func<T, T> valueMapping)
        {
            return records.ToDictionary(
                record => record.Key,
                record => valueMapping(record.Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<GaRecordKeyValue<T>> records, Func<ulong, T, T> valueMapping)
        {
            return records.ToDictionary(
                record => record.Key,
                record => valueMapping(record.Key, record.Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<GaRecordGradeKeyValue<T>> records, Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            return records.ToDictionary(
                record => gradeKeyToKeyMapping(record.Grade, record.Key),
                record => record.Value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<GaRecordGradeKeyValue<T>> records, Func<uint, ulong, ulong> gradeKeyToKeyMapping, Func<T, T> valueMapping)
        {
            return records.ToDictionary(
                record => gradeKeyToKeyMapping(record.Grade, record.Key),
                record => valueMapping(record.Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<GaRecordGradeKeyValue<T>> records, Func<uint, ulong, ulong> gradeKeyToKeyMapping, Func<uint, ulong, T, T> valueMapping)
        {
            return records.ToDictionary(
                record => gradeKeyToKeyMapping(record.Grade, record.Key),
                record => valueMapping(record.Grade, record.Key, record.Value)
            );
        }

        public static Dictionary<uint, Dictionary<ulong, T>> CreateDictionary<T>(this IEnumerable<GaRecordGradeKeyValue<T>> records)
        {
            var gradeKeyValueDict = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, key, value) in records)
            {
                if (!gradeKeyValueDict.TryGetValue(grade, out var keyValueDict))
                {
                    keyValueDict = new Dictionary<ulong, T>();
                    gradeKeyValueDict.Add(grade, keyValueDict);
                }

                keyValueDict.Add(key, value);
            }

            return gradeKeyValueDict;
        }
    }
}