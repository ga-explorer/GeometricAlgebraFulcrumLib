using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records
{
    public static class GaFuLRecordsFactory
    {
        public static GradeIndexRecord ZeroGradeIndexRecord { get; }
            = new GradeIndexRecord(0, 0);

        public static GradeIndexPairRecord ZeroGradeIndexPairRecord { get; }
            = new GradeIndexPairRecord(0, 0, 0);

        public static IndexPairRecord ZeroIndexPairRecord { get; }
            = new IndexPairRecord(0, 0);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexScalarRecord<T> CreateRecordGradeIndexScalar<T>(this GradeIndexRecord record, T value)
        {
            return new GradeIndexScalarRecord<T>(record.Grade, record.Index, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexPairScalarRecord<T> CreateRecordGradeIndexScalar<T>(this GradeIndexPairRecord record, T value)
        {
            return new GradeIndexPairScalarRecord<T>(record.Grade, record.Index1, record.Index2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> CreateRecordIndexScalar<T>(this IndexPairRecord record, T value)
        {
            return new IndexPairScalarRecord<T>(record.Index1, record.Index2, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexScalarRecord<T> ToIndexScalarRecord<T>(this KeyValuePair<ulong, T> keyScalarPair)
        {
            var (key, value) = keyScalarPair;

            return new IndexScalarRecord<T>(key, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> ToIndexScalarRecords<T>(this IEnumerable<KeyValuePair<ulong, T>> keyScalarPairs)
        {
            return keyScalarPairs.Select(ToIndexScalarRecord);
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
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<GradeIndexScalarRecord<T>> records, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return records.ToDictionary(
                record => gradeIndexToIndexMapping(record.Grade, record.Index),
                record => record.Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<GradeIndexScalarRecord<T>> records, Func<uint, ulong, ulong> gradeIndexToIndexMapping, Func<T, T> valueMapping)
        {
            return records.ToDictionary(
                record => gradeIndexToIndexMapping(record.Grade, record.Index),
                record => valueMapping(record.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<GradeIndexScalarRecord<T>> records, Func<uint, ulong, ulong> gradeIndexToIndexMapping, Func<uint, ulong, T, T> valueMapping)
        {
            return records.ToDictionary(
                record => gradeIndexToIndexMapping(record.Grade, record.Index),
                record => valueMapping(record.Grade, record.Index, record.Scalar)
            );
        }

        public static Dictionary<uint, Dictionary<ulong, T>> CreateDictionary<T>(this IEnumerable<GradeIndexScalarRecord<T>> records)
        {
            var gradeIndexScalarDict = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (grade, key, value) in records)
            {
                if (!gradeIndexScalarDict.TryGetValue(grade, out var keyScalarDict))
                {
                    keyScalarDict = new Dictionary<ulong, T>();
                    gradeIndexScalarDict.Add(grade, keyScalarDict);
                }

                keyScalarDict.Add(key, value);
            }

            return gradeIndexScalarDict;
        }
    }
}