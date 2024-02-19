using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public static class GaFuLRecordsFactory
{
    public static RGaGradeKvIndexRecord ZeroGradeIndexRecord { get; }
        = new RGaGradeKvIndexRecord(0, 0);

    public static RGaGradeKvIndexPairRecord ZeroGradeIndexPairRecord { get; }
        = new RGaGradeKvIndexPairRecord(0, 0, 0);

    public static RGaKvIndexPairRecord ZeroIndexPairRecord { get; }
        = new RGaKvIndexPairRecord(0, 0);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaGradeKvIndexScalarRecord<T> CreateRecordGradeIndexScalar<T>(this RGaGradeKvIndexRecord record, T value)
    {
        return new RGaGradeKvIndexScalarRecord<T>(record.Grade, record.KvIndex, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaGradeKvIndexPairScalarRecord<T> CreateRecordGradeIndexScalar<T>(this RGaGradeKvIndexPairRecord record, T value)
    {
        return new RGaGradeKvIndexPairScalarRecord<T>(record.Grade, record.KvIndex1, record.KvIndex2, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKvIndexPairScalarRecord<T> CreateRecordIndexScalar<T>(this RGaKvIndexPairRecord record, T value)
    {
        return new RGaKvIndexPairScalarRecord<T>(record.KvIndex1, record.KvIndex2, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IRGaKvIndexScalarRecord<T> ToIndexScalarRecord<T>(this KeyValuePair<ulong, T> keyScalarPair)
    {
        var (key, value) = keyScalarPair;

        return new RGaKvIndexScalarRecord<T>(key, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IRGaKvIndexScalarRecord<T>> ToIndexScalarRecords<T>(this IEnumerable<KeyValuePair<ulong, T>> keyScalarPairs)
    {
        return keyScalarPairs.Select(ToIndexScalarRecord);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<IRGaKvIndexScalarRecord<T>> records)
    {
        return records.ToDictionary(
            record => record.KvIndex,
            record => record.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<IRGaKvIndexScalarRecord<T>> records, Func<ulong, ulong> keyMapping)
    {
        return records.ToDictionary(
            record => keyMapping(record.KvIndex),
            record => record.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<IRGaKvIndexScalarRecord<T>> records, Func<ulong, ulong> keyMapping, Func<T, T> valueMapping)
    {
        return records.ToDictionary(
            record => keyMapping(record.KvIndex),
            record => valueMapping(record.Scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<IRGaKvIndexScalarRecord<T>> records, Func<ulong, ulong> keyMapping, Func<ulong, T, T> valueMapping)
    {
        return records.ToDictionary(
            record => keyMapping(record.KvIndex),
            record => valueMapping(record.KvIndex, record.Scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<IRGaKvIndexScalarRecord<T>> records, Func<T, T> valueMapping)
    {
        return records.ToDictionary(
            record => record.KvIndex,
            record => valueMapping(record.Scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<IRGaKvIndexScalarRecord<T>> records, Func<ulong, T, T> valueMapping)
    {
        return records.ToDictionary(
            record => record.KvIndex,
            record => valueMapping(record.KvIndex, record.Scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<RGaGradeKvIndexScalarRecord<T>> records, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
    {
        return records.ToDictionary(
            record => gradeIndexToIndexMapping(record.Grade, record.KvIndex),
            record => record.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<RGaGradeKvIndexScalarRecord<T>> records, Func<uint, ulong, ulong> gradeIndexToIndexMapping, Func<T, T> valueMapping)
    {
        return records.ToDictionary(
            record => gradeIndexToIndexMapping(record.Grade, record.KvIndex),
            record => valueMapping(record.Scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<ulong, T> CreateDictionary<T>(this IEnumerable<RGaGradeKvIndexScalarRecord<T>> records, Func<uint, ulong, ulong> gradeIndexToIndexMapping, Func<uint, ulong, T, T> valueMapping)
    {
        return records.ToDictionary(
            record => gradeIndexToIndexMapping(record.Grade, record.KvIndex),
            record => valueMapping(record.Grade, record.KvIndex, record.Scalar)
        );
    }

    public static Dictionary<uint, Dictionary<ulong, T>> CreateDictionary<T>(this IEnumerable<RGaGradeKvIndexScalarRecord<T>> records)
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