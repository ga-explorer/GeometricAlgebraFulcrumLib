using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GaRecordsFactory
    {
        public static GradeIndexRecord ZeroGridKey { get; }
            = new GradeIndexRecord(0, 0);

        public static GradeIndexPairRecord ZeroGridKeyPair { get; }
            = new GradeIndexPairRecord(0, 0, 0);

        public static IndexPairRecord ZeroKeyPair { get; }
            = new IndexPairRecord(0, 0);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexScalarRecord<T> CreateRecordGradeKeyValue<T>(this GradeIndexRecord record, T value)
        {
            return new GradeIndexScalarRecord<T>(record.Grade, record.Index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexPairScalarRecord<T> CreateRecordGradeKeyValue<T>(this GradeIndexPairRecord record, T value)
        {
            return new GradeIndexPairScalarRecord<T>(record.Grade, record.Key1, record.Key2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> CreateRecordKeyValue<T>(this IndexPairRecord record, T value)
        {
            return new IndexPairScalarRecord<T>(record.Index1, record.Index2, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexScalarRecord<T> ToKeyValueRecord<T>(this KeyValuePair<ulong, T> keyValuePair)
        {
            var (key, value) = keyValuePair;

            return new IndexScalarRecord<T>(key, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> ToKeyValueRecords<T>(this IEnumerable<KeyValuePair<ulong, T>> keyValuePairs)
        {
            return keyValuePairs.Select(ToKeyValueRecord);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<IndexScalarRecord<T>> records)
        {
            return records.ToDictionary(
                record => record.Index,
                record => record.Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<IndexScalarRecord<T>> records, Func<ulong, ulong> keyMapping)
        {
            return records.ToDictionary(
                record => keyMapping(record.Index),
                record => record.Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<IndexScalarRecord<T>> records, Func<ulong, ulong> keyMapping, Func<T, T> valueMapping)
        {
            return records.ToDictionary(
                record => keyMapping(record.Index),
                record => valueMapping(record.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<IndexScalarRecord<T>> records, Func<ulong, ulong> keyMapping, Func<ulong, T, T> valueMapping)
        {
            return records.ToDictionary(
                record => keyMapping(record.Index),
                record => valueMapping(record.Index, record.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<IndexScalarRecord<T>> records, Func<T, T> valueMapping)
        {
            return records.ToDictionary(
                record => record.Index,
                record => valueMapping(record.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<IndexScalarRecord<T>> records, Func<ulong, T, T> valueMapping)
        {
            return records.ToDictionary(
                record => record.Index,
                record => valueMapping(record.Index, record.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<GradeIndexScalarRecord<T>> records, Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            return records.ToDictionary(
                record => gradeKeyToKeyMapping(record.Grade, record.Index),
                record => record.Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<GradeIndexScalarRecord<T>> records, Func<uint, ulong, ulong> gradeKeyToKeyMapping, Func<T, T> valueMapping)
        {
            return records.ToDictionary(
                record => gradeKeyToKeyMapping(record.Grade, record.Index),
                record => valueMapping(record.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<GradeIndexScalarRecord<T>> records, Func<uint, ulong, ulong> gradeKeyToKeyMapping, Func<uint, ulong, T, T> valueMapping)
        {
            return records.ToDictionary(
                record => gradeKeyToKeyMapping(record.Grade, record.Index),
                record => valueMapping(record.Grade, record.Index, record.Scalar)
            );
        }

        public static Dictionary<uint, Dictionary<ulong, T>> CreateDictionary<T>(this IEnumerable<GradeIndexScalarRecord<T>> records)
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