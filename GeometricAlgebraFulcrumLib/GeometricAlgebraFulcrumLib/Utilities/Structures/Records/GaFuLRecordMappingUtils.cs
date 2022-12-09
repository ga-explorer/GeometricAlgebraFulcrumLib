using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records
{
    public static class GaFuLRecordMappingUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> MapRecords<T, T2>(this IEnumerable<IIndexScalarRecord<T>> records, Func<ulong, T, T2> indexScalarMapping)
        {
            return records.Select(
                record => record.MapRecord(indexScalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> MapRecords<T, T2>(this IEnumerable<IIndexPairScalarRecord<T>> records, Func<ulong, ulong, T, T2> indexScalarMapping)
        {
            return records.Select(
                record => record.MapRecord(indexScalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> MapRecords<T, T2>(this IEnumerable<IGradeIndexScalarRecord<T>> records, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return records.Select(
                record => record.MapRecord(gradeIndexScalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> MapRecords<T, T2>(this IEnumerable<IGradeScalarRecord<T>> records, Func<uint, T, T2> gradeScalarMapping)
        {
            return records.Select(
                record => record.MapRecord(gradeScalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> MapRecords<T, T2>(this IEnumerable<IGradeIndexPairScalarRecord<T>> records, Func<uint, ulong, ulong, T, T2> gradeIndexScalarMapping)
        {
            return records.Select(
                record => record.MapRecord(gradeIndexScalarMapping)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 MapRecord<T, T2>(this IIndexScalarRecord<T> record, Func<ulong, T, T2> indexScalarMapping)
        {
            return indexScalarMapping(record.Index, record.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 MapRecord<T, T2>(this IIndexPairScalarRecord<T> record, Func<ulong, ulong, T, T2> indexScalarMapping)
        {
            return indexScalarMapping(record.Index1, record.Index2, record.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 MapRecord<T, T2>(this IGradeIndexScalarRecord<T> record, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return gradeIndexScalarMapping(record.Grade, record.Index, record.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 MapRecord<T, T2>(this IGradeScalarRecord<T> record, Func<uint, T, T2> gradeScalarMapping)
        {
            return gradeScalarMapping(record.Grade, record.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 MapRecord<T, T2>(this IGradeIndexPairScalarRecord<T> record, Func<uint, ulong, ulong, T, T2> gradeIndexScalarMapping)
        {
            return gradeIndexScalarMapping(record.Grade, record.Index1, record.Index2, record.Scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord MapRecord(this IndexPairRecord indexPair, Func<ulong, ulong> indexMapping)
        {
            var (index1, index2) = indexPair;

            return new IndexPairRecord(
                indexMapping(index1),
                indexMapping(index2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<T2> MapRecordIndices<T2>(this IIndexPairRecord indexPair, Func<ulong, T2> indexMapping)
        {
            return new Pair<T2>(
                indexMapping(indexPair.Index1),
                indexMapping(indexPair.Index2)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexRecord MapRecord(this GradeIndexRecord record, Func<ulong, ulong> mappingFunc)
        {
            var (grade, index) = record;

            return new GradeIndexRecord(grade, mappingFunc(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexRecord MapRecord(this GradeIndexRecord record, Func<uint, ulong, GradeIndexRecord> mappingFunc)
        {
            var (grade, index) = record;

            return mappingFunc(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexRecord MapRecord(this GradeIndexRecord record, Func<GradeIndexRecord, GradeIndexRecord> mappingFunc)
        {
            return mappingFunc(record);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexPairRecord MapRecord(this GradeIndexPairRecord record, Func<ulong, ulong> mappingFunc)
        {
            var (grade, index1, index2) = record;

            return new GradeIndexPairRecord(grade, mappingFunc(index1), mappingFunc(index2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexPairRecord MapRecord(this GradeIndexPairRecord record, Func<ulong, ulong, IndexPairRecord> mappingFunc)
        {
            var (grade, index1, index2) = record;

            (index1, index2) = mappingFunc(index1, index2);

            return new GradeIndexPairRecord(grade, index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexPairRecord MapRecord(this GradeIndexPairRecord record, Func<IndexPairRecord, IndexPairRecord> mappingFunc)
        {
            var (grade, index1, index2) = record;

            (index1, index2) = mappingFunc(new IndexPairRecord(index1, index2));

            return new GradeIndexPairRecord(grade, index1, index2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexPairRecord MapRecord(this IndexPairRecord record, Func<ulong, ulong, GradeIndexPairRecord> mappingFunc)
        {
            var (index1, index2) = record;

            return mappingFunc(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexPairRecord MapRecord(this IndexPairRecord record, Func<IndexPairRecord, GradeIndexPairRecord> mappingFunc)
        {
            return mappingFunc(record);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord MapRecord(this GradeIndexPairRecord record, Func<uint, ulong, ulong> mappingFunc)
        {
            var (grade, index1, index2) = record;

            return new IndexPairRecord(
                mappingFunc(grade, index1),
                mappingFunc(grade, index2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord MapRecord(this GradeIndexPairRecord record, Func<GradeIndexRecord, ulong> mappingFunc)
        {
            var (grade, index1, index2) = record;

            return new IndexPairRecord(
                mappingFunc(new GradeIndexRecord(grade, index1)),
                mappingFunc(new GradeIndexRecord(grade, index2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord MapRecord(this GradeIndexPairRecord record, Func<uint, ulong, ulong, IndexPairRecord> mappingFunc)
        {
            var (grade, index1, index2) = record;

            return mappingFunc(grade, index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord MapRecord(this GradeIndexPairRecord record, Func<GradeIndexPairRecord, IndexPairRecord> mappingFunc)
        {
            return mappingFunc(record);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexScalarRecord<T> MapRecord<T>(this IndexScalarRecord<T> record, Func<ulong, ulong> mappingFunc)
        {
            var (index, scalar) = record;

            return new IndexScalarRecord<T>(mappingFunc(index), scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexScalarRecord<T> MapRecord<T>(this IndexScalarRecord<T> record, Func<ulong, T, IndexScalarRecord<T>> mappingFunc)
        {
            var (index, scalar) = record;

            return mappingFunc(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexScalarRecord<T> MapRecord<T>(this IndexScalarRecord<T> record, Func<IndexScalarRecord<T>, IndexScalarRecord<T>> mappingFunc)
        {
            return mappingFunc(record);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexScalarRecord<T> MapRecord<T>(this IndexScalarRecord<T> record, Func<ulong, GradeIndexRecord> mappingFunc)
        {
            var (index, scalar) = record;

            var (grade, index1) = mappingFunc(index);

            return new GradeIndexScalarRecord<T>(grade, index1, scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> MapRecord<T>(this IndexPairScalarRecord<T> record, Func<ulong, ulong> mappingFunc)
        {
            var (index1, index2, scalar) = record;

            return new IndexPairScalarRecord<T>(
                mappingFunc(index1),
                mappingFunc(index2),
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> MapRecord<T>(this IndexPairScalarRecord<T> record, Func<ulong, ulong, IndexPairRecord> mappingFunc)
        {
            var (index1, index2, scalar) = record;

            (index1, index2) = mappingFunc(index1, index2);

            return new IndexPairScalarRecord<T>(index1, index2, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> MapRecord<T>(this IndexPairScalarRecord<T> record, Func<IndexPairRecord, IndexPairRecord> mappingFunc)
        {
            var (index1, index2, scalar) = record;

            (index1, index2) = mappingFunc(new IndexPairRecord(index1, index2));

            return new IndexPairScalarRecord<T>(index1, index2, scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexPairScalarRecord<T> MapRecord<T>(this IndexPairScalarRecord<T> record, Func<ulong, ulong, GradeIndexPairRecord> mappingFunc)
        {
            var (index1, index2, scalar) = record;

            var (grade, index3, index4) = mappingFunc(index1, index2);

            return new GradeIndexPairScalarRecord<T>(grade, index3, index4, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexPairScalarRecord<T> MapRecord<T>(this IndexPairScalarRecord<T> record, Func<IndexPairRecord, GradeIndexPairRecord> mappingFunc)
        {
            var (index1, index2, scalar) = record;

            var (grade, index3, index4) =
                mappingFunc(new IndexPairRecord(index1, index2));

            return new GradeIndexPairScalarRecord<T>(grade, index3, index4, scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> MapRecord<T>(this GradeIndexPairScalarRecord<T> record, Func<uint, ulong, ulong> mappingFunc)
        {
            var (grade, index1, index2, scalar) = record;

            return new IndexPairScalarRecord<T>(
                mappingFunc(grade, index1),
                mappingFunc(grade, index2),
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> MapRecord<T>(this GradeIndexPairScalarRecord<T> record, Func<GradeIndexRecord, ulong> mappingFunc)
        {
            var (grade, index1, index2, scalar) = record;

            return new IndexPairScalarRecord<T>(
                mappingFunc(new GradeIndexRecord(grade, index1)),
                mappingFunc(new GradeIndexRecord(grade, index2)),
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> MapRecord<T>(this GradeIndexPairScalarRecord<T> record, Func<uint, ulong, ulong, IndexPairRecord> mappingFunc)
        {
            var (grade, index1, index2, scalar) = record;

            (index1, index2) = mappingFunc(grade, index1, index2);

            return new IndexPairScalarRecord<T>(index1, index2, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> MapRecord<T>(this GradeIndexPairScalarRecord<T> record, Func<GradeIndexPairRecord, IndexPairRecord> mappingFunc)
        {
            var (grade, index1, index2, scalar) = record;

            (index1, index2) = mappingFunc(new GradeIndexPairRecord(grade, index1, index2));

            return new IndexPairScalarRecord<T>(index1, index2, scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexPairScalarRecord<T> MapRecord<T>(this GradeIndexPairScalarRecord<T> record, Func<ulong, ulong> mappingFunc)
        {
            var (grade, index1, index2, scalar) = record;

            return new GradeIndexPairScalarRecord<T>(grade, mappingFunc(index1), mappingFunc(index2), scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexPairScalarRecord<T> MapRecord<T>(this GradeIndexPairScalarRecord<T> record, Func<ulong, ulong, IndexPairRecord> mappingFunc)
        {
            var (grade, index1, index2, scalar) = record;

            (index1, index2) = mappingFunc(index1, index2);

            return new GradeIndexPairScalarRecord<T>(grade, index1, index2, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexPairScalarRecord<T> MapRecord<T>(this GradeIndexPairScalarRecord<T> record, Func<IndexPairRecord, IndexPairRecord> mappingFunc)
        {
            var (grade, index1, index2, scalar) = record;

            (index1, index2) = mappingFunc(new IndexPairRecord(index1, index2));

            return new GradeIndexPairScalarRecord<T>(grade, index1, index2, scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 LeftFoldIndices<T2>(this IndexPairRecord indexPair, T2 initialScalar, Func<T2, ulong, T2> indexMapping)
        {
            return
                indexMapping(
                    indexMapping(
                        initialScalar,
                        indexPair.Index1
                    ),
                    indexPair.Index2
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> LeftScanIndices<T2>(this IndexPairRecord indexPair, T2 initialScalar, Func<T2, ulong, T2> indexMapping)
        {
            var index = initialScalar;
            yield return index;

            index = indexMapping(index, indexPair.Index1);
            yield return index;

            index = indexMapping(index, indexPair.Index2);
            yield return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 RightFoldIndices<T2>(this IndexPairRecord indexPair, T2 initialScalar, Func<ulong, T2, T2> indexMapping)
        {
            return
                indexMapping(
                    indexPair.Index1,
                    indexMapping(
                        indexPair.Index2,
                        initialScalar
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> RightScanIndices<T2>(this IndexPairRecord indexPair, T2 initialScalar, Func<ulong, T2, T2> indexMapping)
        {
            var index = initialScalar;
            yield return index;

            index = indexMapping(indexPair.Index2, index);
            yield return index;

            index = indexMapping(indexPair.Index1, index);
            yield return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 ReduceIndices<T2>(this IndexPairRecord indexPair, Func<ulong, ulong, T2> indexMapping)
        {
            return indexMapping(indexPair.Index1, indexPair.Index2);
        }

        /// <summary>
        /// Returns a new indexPair containing (indexPair.Index2, nextIndex)
        /// </summary>
        /// <param name="indexPair"></param>
        /// <param name="nextIndex"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord NextPair(this IndexPairRecord indexPair, ulong nextIndex)
        {
            return new IndexPairRecord(indexPair.Index2, nextIndex);
        }

        /// <summary>
        /// Returns a new indexPair containing (previousIndex, indexPair.Index1)
        /// </summary>
        /// <param name="indexPair"></param>
        /// <param name="previousIndex"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord PreviousPair(this IndexPairRecord indexPair, ulong previousIndex)
        {
            return new IndexPairRecord(previousIndex, indexPair.Index1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord Transpose(this IndexPairRecord indexPair)
        {
            var (index1, index2) = indexPair;

            return new IndexPairRecord(index2, index1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexPairRecord Transpose(this GradeIndexPairRecord record)
        {
            var (grade, index1, index2) = record;

            return new GradeIndexPairRecord(grade, index2, index1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> Transpose<T>(this IndexPairScalarRecord<T> record)
        {
            var (index1, index2, value) = record;

            return new IndexPairScalarRecord<T>(index2, index1, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexPairScalarRecord<T> Transpose<T>(this GradeIndexPairScalarRecord<T> record)
        {
            var (grade, index1, index2, value) = record;

            return new GradeIndexPairScalarRecord<T>(grade, index2, index1, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairRecord> Transpose(this IEnumerable<IndexPairRecord> indexRecords)
        {
            return indexRecords.Select(record => record.Transpose());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairScalarRecord<T>> Transpose<T>(this IEnumerable<IndexPairScalarRecord<T>> indexScalarRecords)
        {
            return indexScalarRecords.Select(record => record.Transpose());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexPairRecord> Transpose(this IEnumerable<GradeIndexPairRecord> gradeIndexRecords)
        {
            return gradeIndexRecords.Select(record => record.Transpose());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexPairScalarRecord<T>> Transpose<T>(this IEnumerable<GradeIndexPairScalarRecord<T>> gradeIndexScalarRecords)
        {
            return gradeIndexScalarRecords.Select(record => record.Transpose());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairScalarRecord<T>> MapRecords<T>(this IEnumerable<IndexPairScalarRecord<T>> indexScalarRecords, Func<ulong, ulong, IndexPairRecord> indexMapping)
        {
            return indexScalarRecords.Select(record => record.MapRecord(indexMapping));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairScalarRecord<T>> MapRecords<T>(this IEnumerable<IndexPairScalarRecord<T>> indexScalarRecords, Func<IndexPairRecord, IndexPairRecord> indexMapping)
        {
            return indexScalarRecords.Select(record => record.MapRecord(indexMapping));
        }
    }
}