using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records
{
    public static class GaFuLRecordFilteringUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<IndexScalarRecord<T>> records, Func<ulong, bool> indexFilter)
        {
            return records.Where(record => indexFilter(record.Index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<IndexScalarRecord<T>> records, Func<ulong, T, bool> indexScalarFilter)
        {
            return records.Where(record => indexScalarFilter(record.Index, record.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<IndexScalarRecord<T>> records, Func<T, bool> scalarFilter)
        {
            return records.Where(record => scalarFilter(record.Scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairRecord> FilterRecords(this IEnumerable<IndexPairRecord> records, Func<ulong, ulong, bool> indexFilter)
        {
            return records.Where(record => indexFilter(record.Index1, record.Index2));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<IndexPairScalarRecord<T>> records, Func<ulong, ulong, bool> indexFilter)
        {
            return records.Where(record => indexFilter(record.Index1, record.Index2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<IndexPairScalarRecord<T>> records, Func<ulong, ulong, T, bool> indexScalarFilter)
        {
            return records.Where(record => indexScalarFilter(record.Index1, record.Index2, record.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<IndexPairScalarRecord<T>> records, Func<T, bool> scalarFilter)
        {
            return records.Where(record => scalarFilter(record.Scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexRecord> FilterRecords(this IEnumerable<GradeIndexRecord> records, Func<uint, bool> gradeFilter)
        {
            return records.Where(record => gradeFilter(record.Grade));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexRecord> FilterRecords(this IEnumerable<GradeIndexRecord> records, Func<ulong, bool> indexFilter)
        {
            return records.Where(record => indexFilter(record.Index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexRecord> FilterRecords(this IEnumerable<GradeIndexRecord> records, Func<uint, ulong, bool> gradeIndexFilter)
        {
            return records.Where(record => gradeIndexFilter(record.Grade, record.Index));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<GradeIndexScalarRecord<T>> records, Func<uint, bool> gradeFilter)
        {
            return records.Where(record => gradeFilter(record.Grade));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<GradeIndexScalarRecord<T>> records, Func<ulong, bool> indexFilter)
        {
            return records.Where(record => indexFilter(record.Index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<GradeIndexScalarRecord<T>> records, Func<uint, ulong, bool> gradeIndexFilter)
        {
            return records.Where(record => gradeIndexFilter(record.Grade, record.Index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<GradeIndexScalarRecord<T>> records, Func<T, bool> scalarFilter)
        {
            return records.Where(record => scalarFilter(record.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<GradeIndexScalarRecord<T>> records, Func<uint, T, bool> gradeScalarFilter)
        {
            return records.Where(record => gradeScalarFilter(record.Grade, record.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<GradeIndexScalarRecord<T>> records, Func<ulong, T, bool> indexScalarFilter)
        {
            return records.Where(record => indexScalarFilter(record.Index, record.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexScalarRecord<T>> FilterRecords<T>(this IEnumerable<GradeIndexScalarRecord<T>> records, Func<uint, ulong, T, bool> gradeIndexScalarFilter)
        {
            return records.Where(record => gradeIndexScalarFilter(record.Grade, record.Index, record.Scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexPairRecord> FilterRecords(this IEnumerable<GradeIndexPairRecord> records, Func<uint, bool> gradeFilter)
        {
            return records.Where(record => gradeFilter(record.Grade));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexPairRecord> FilterRecords(this IEnumerable<GradeIndexPairRecord> records, Func<ulong, ulong, bool> indexFilter)
        {
            return records.Where(record => indexFilter(record.Index1, record.Index2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexPairRecord> FilterRecords(this IEnumerable<GradeIndexPairRecord> records, Func<uint, ulong, ulong, bool> gradeIndexFilter)
        {
            return records.Where(record => gradeIndexFilter(record.Grade, record.Index1, record.Index2));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<GradeIndexPairScalarRecord<T>> records, Func<uint, bool> gradeFilter)
        {
            return records.Where(record => gradeFilter(record.Grade));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<GradeIndexPairScalarRecord<T>> records, Func<ulong, ulong, bool> indexFilter)
        {
            return records.Where(record => indexFilter(record.Index1, record.Index2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<GradeIndexPairScalarRecord<T>> records, Func<uint, ulong, ulong, bool> gradeIndexFilter)
        {
            return records.Where(record => gradeIndexFilter(record.Grade, record.Index1, record.Index2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<GradeIndexPairScalarRecord<T>> records, Func<T, bool> scalarFilter)
        {
            return records.Where(record => scalarFilter(record.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<GradeIndexPairScalarRecord<T>> records, Func<uint, T, bool> gradeScalarFilter)
        {
            return records.Where(record => gradeScalarFilter(record.Grade, record.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<GradeIndexPairScalarRecord<T>> records, Func<ulong, ulong, T, bool> indexScalarFilter)
        {
            return records.Where(record => indexScalarFilter(record.Index1, record.Index2, record.Scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexPairScalarRecord<T>> FilterRecords<T>(this IEnumerable<GradeIndexPairScalarRecord<T>> records, Func<uint, ulong, ulong, T, bool> gradeIndexScalarFilter)
        {
            return records.Where(record => gradeIndexScalarFilter(record.Grade, record.Index1, record.Index2, record.Scalar));
        }
    }
}