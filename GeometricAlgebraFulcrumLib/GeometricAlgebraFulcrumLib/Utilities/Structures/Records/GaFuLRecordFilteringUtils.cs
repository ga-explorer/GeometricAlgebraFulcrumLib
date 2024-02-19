using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

public static class GaFuLRecordFilteringUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaKvIndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaKvIndexScalarRecord<T>> records, Func<ulong, bool> indexFilter)
    {
        return records.Where(record => indexFilter(record.KvIndex));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaKvIndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaKvIndexScalarRecord<T>> records, Func<ulong, T, bool> indexScalarFilter)
    {
        return records.Where(record => indexScalarFilter(record.KvIndex, record.Scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaKvIndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaKvIndexScalarRecord<T>> records, Func<T, bool> scalarFilter)
    {
        return records.Where(record => scalarFilter(record.Scalar));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaKvIndexPairRecord> FilterRecords(this IEnumerable<RGaKvIndexPairRecord> records, Func<ulong, ulong, bool> indexFilter)
    {
        return records.Where(record => indexFilter(record.KvIndex1, record.KvIndex2));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaKvIndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaKvIndexPairScalarRecord<T>> records, Func<ulong, ulong, bool> indexFilter)
    {
        return records.Where(record => indexFilter(record.KvIndex1, record.KvIndex2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaKvIndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaKvIndexPairScalarRecord<T>> records, Func<ulong, ulong, T, bool> indexScalarFilter)
    {
        return records.Where(record => indexScalarFilter(record.KvIndex1, record.KvIndex2, record.Scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaKvIndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaKvIndexPairScalarRecord<T>> records, Func<T, bool> scalarFilter)
    {
        return records.Where(record => scalarFilter(record.Scalar));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexRecord> FilterRecords(this IEnumerable<RGaGradeKvIndexRecord> records, Func<uint, bool> gradeFilter)
    {
        return records.Where(record => gradeFilter(record.Grade));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexRecord> FilterRecords(this IEnumerable<RGaGradeKvIndexRecord> records, Func<ulong, bool> indexFilter)
    {
        return records.Where(record => indexFilter(record.KvIndex));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexRecord> FilterRecords(this IEnumerable<RGaGradeKvIndexRecord> records, Func<uint, ulong, bool> gradeIndexFilter)
    {
        return records.Where(record => gradeIndexFilter(record.Grade, record.KvIndex));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaGradeKvIndexScalarRecord<T>> records, Func<uint, bool> gradeFilter)
    {
        return records.Where(record => gradeFilter(record.Grade));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaGradeKvIndexScalarRecord<T>> records, Func<ulong, bool> indexFilter)
    {
        return records.Where(record => indexFilter(record.KvIndex));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaGradeKvIndexScalarRecord<T>> records, Func<uint, ulong, bool> gradeIndexFilter)
    {
        return records.Where(record => gradeIndexFilter(record.Grade, record.KvIndex));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaGradeKvIndexScalarRecord<T>> records, Func<T, bool> scalarFilter)
    {
        return records.Where(record => scalarFilter(record.Scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaGradeKvIndexScalarRecord<T>> records, Func<uint, T, bool> gradeScalarFilter)
    {
        return records.Where(record => gradeScalarFilter(record.Grade, record.Scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaGradeKvIndexScalarRecord<T>> records, Func<ulong, T, bool> indexScalarFilter)
    {
        return records.Where(record => indexScalarFilter(record.KvIndex, record.Scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaGradeKvIndexScalarRecord<T>> records, Func<uint, ulong, T, bool> gradeIndexScalarFilter)
    {
        return records.Where(record => gradeIndexScalarFilter(record.Grade, record.KvIndex, record.Scalar));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexPairRecord> FilterRecords(this IEnumerable<RGaGradeKvIndexPairRecord> records, Func<uint, bool> gradeFilter)
    {
        return records.Where(record => gradeFilter(record.Grade));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexPairRecord> FilterRecords(this IEnumerable<RGaGradeKvIndexPairRecord> records, Func<ulong, ulong, bool> indexFilter)
    {
        return records.Where(record => indexFilter(record.KvIndex1, record.KvIndex2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexPairRecord> FilterRecords(this IEnumerable<RGaGradeKvIndexPairRecord> records, Func<uint, ulong, ulong, bool> gradeIndexFilter)
    {
        return records.Where(record => gradeIndexFilter(record.Grade, record.KvIndex1, record.KvIndex2));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> records, Func<uint, bool> gradeFilter)
    {
        return records.Where(record => gradeFilter(record.Grade));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> records, Func<ulong, ulong, bool> indexFilter)
    {
        return records.Where(record => indexFilter(record.KvIndex1, record.KvIndex2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> records, Func<uint, ulong, ulong, bool> gradeIndexFilter)
    {
        return records.Where(record => gradeIndexFilter(record.Grade, record.KvIndex1, record.KvIndex2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> records, Func<T, bool> scalarFilter)
    {
        return records.Where(record => scalarFilter(record.Scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> records, Func<uint, T, bool> gradeScalarFilter)
    {
        return records.Where(record => gradeScalarFilter(record.Grade, record.Scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> records, Func<ulong, ulong, T, bool> indexScalarFilter)
    {
        return records.Where(record => indexScalarFilter(record.KvIndex1, record.KvIndex2, record.Scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> records, Func<uint, ulong, ulong, T, bool> gradeIndexScalarFilter)
    {
        return records.Where(record => gradeIndexScalarFilter(record.Grade, record.KvIndex1, record.KvIndex2, record.Scalar));
    }
}