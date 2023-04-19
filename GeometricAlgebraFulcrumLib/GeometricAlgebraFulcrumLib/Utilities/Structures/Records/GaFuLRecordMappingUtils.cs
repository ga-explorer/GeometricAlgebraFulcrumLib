using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records
{
    public static class GaFuLRecordMappingUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> MapRecords<T, T2>(this IEnumerable<IRGaKvIndexScalarRecord<T>> records, Func<ulong, T, T2> indexScalarMapping)
        {
            return records.Select(
                record => record.MapRecord(indexScalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> MapRecords<T, T2>(this IEnumerable<IRGaKvIndexPairScalarRecord<T>> records, Func<ulong, ulong, T, T2> indexScalarMapping)
        {
            return records.Select(
                record => record.MapRecord(indexScalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> MapRecords<T, T2>(this IEnumerable<IRGaGradeKvIndexScalarRecord<T>> records, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return records.Select(
                record => record.MapRecord(gradeIndexScalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> MapRecords<T, T2>(this IEnumerable<IGaGradeScalarRecord<T>> records, Func<uint, T, T2> gradeScalarMapping)
        {
            return records.Select(
                record => record.MapRecord(gradeScalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> MapRecords<T, T2>(this IEnumerable<IRGaGradeKvIndexPairScalarRecord<T>> records, Func<uint, ulong, ulong, T, T2> gradeIndexScalarMapping)
        {
            return records.Select(
                record => record.MapRecord(gradeIndexScalarMapping)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 MapRecord<T, T2>(this IRGaKvIndexScalarRecord<T> record, Func<ulong, T, T2> indexScalarMapping)
        {
            return indexScalarMapping(record.KvIndex, record.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 MapRecord<T, T2>(this IRGaKvIndexPairScalarRecord<T> record, Func<ulong, ulong, T, T2> indexScalarMapping)
        {
            return indexScalarMapping(record.KvIndex1, record.KvIndex2, record.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 MapRecord<T, T2>(this IRGaGradeKvIndexScalarRecord<T> record, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return gradeIndexScalarMapping(record.Grade, record.KvIndex, record.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 MapRecord<T, T2>(this IGaGradeScalarRecord<T> record, Func<uint, T, T2> gradeScalarMapping)
        {
            return gradeScalarMapping(record.Grade, record.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 MapRecord<T, T2>(this IRGaGradeKvIndexPairScalarRecord<T> record, Func<uint, ulong, ulong, T, T2> gradeIndexScalarMapping)
        {
            return gradeIndexScalarMapping(record.Grade, record.KvIndex1, record.KvIndex2, record.Scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairRecord MapRecord(this RGaKvIndexPairRecord indexPair, Func<ulong, ulong> indexMapping)
        {
            var (index1, index2) = indexPair;

            return new RGaKvIndexPairRecord(
                indexMapping(index1),
                indexMapping(index2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<T2> MapRecordIndices<T2>(this IRGaKvIndexPairRecord indexPair, Func<ulong, T2> indexMapping)
        {
            return new Pair<T2>(
                indexMapping(indexPair.KvIndex1),
                indexMapping(indexPair.KvIndex2)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexRecord MapRecord(this RGaGradeKvIndexRecord record, Func<ulong, ulong> mappingFunc)
        {
            var (grade, index) = record;

            return new RGaGradeKvIndexRecord(grade, mappingFunc(index));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexRecord MapRecord(this RGaGradeKvIndexRecord record, Func<uint, ulong, RGaGradeKvIndexRecord> mappingFunc)
        {
            var (grade, index) = record;

            return mappingFunc(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexRecord MapRecord(this RGaGradeKvIndexRecord record, Func<RGaGradeKvIndexRecord, RGaGradeKvIndexRecord> mappingFunc)
        {
            return mappingFunc(record);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexPairRecord MapRecord(this RGaGradeKvIndexPairRecord record, Func<ulong, ulong> mappingFunc)
        {
            var (grade, index1, index2) = record;

            return new RGaGradeKvIndexPairRecord(grade, mappingFunc(index1), mappingFunc(index2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexPairRecord MapRecord(this RGaGradeKvIndexPairRecord record, Func<ulong, ulong, RGaKvIndexPairRecord> mappingFunc)
        {
            var (grade, index1, index2) = record;

            (index1, index2) = mappingFunc(index1, index2);

            return new RGaGradeKvIndexPairRecord(grade, index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexPairRecord MapRecord(this RGaGradeKvIndexPairRecord record, Func<RGaKvIndexPairRecord, RGaKvIndexPairRecord> mappingFunc)
        {
            var (grade, index1, index2) = record;

            (index1, index2) = mappingFunc(new RGaKvIndexPairRecord(index1, index2));

            return new RGaGradeKvIndexPairRecord(grade, index1, index2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexPairRecord MapRecord(this RGaKvIndexPairRecord record, Func<ulong, ulong, RGaGradeKvIndexPairRecord> mappingFunc)
        {
            var (index1, index2) = record;

            return mappingFunc(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexPairRecord MapRecord(this RGaKvIndexPairRecord record, Func<RGaKvIndexPairRecord, RGaGradeKvIndexPairRecord> mappingFunc)
        {
            return mappingFunc(record);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairRecord MapRecord(this RGaGradeKvIndexPairRecord record, Func<uint, ulong, ulong> mappingFunc)
        {
            var (grade, index1, index2) = record;

            return new RGaKvIndexPairRecord(
                mappingFunc(grade, index1),
                mappingFunc(grade, index2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairRecord MapRecord(this RGaGradeKvIndexPairRecord record, Func<RGaGradeKvIndexRecord, ulong> mappingFunc)
        {
            var (grade, index1, index2) = record;

            return new RGaKvIndexPairRecord(
                mappingFunc(new RGaGradeKvIndexRecord(grade, index1)),
                mappingFunc(new RGaGradeKvIndexRecord(grade, index2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairRecord MapRecord(this RGaGradeKvIndexPairRecord record, Func<uint, ulong, ulong, RGaKvIndexPairRecord> mappingFunc)
        {
            var (grade, index1, index2) = record;

            return mappingFunc(grade, index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairRecord MapRecord(this RGaGradeKvIndexPairRecord record, Func<RGaGradeKvIndexPairRecord, RGaKvIndexPairRecord> mappingFunc)
        {
            return mappingFunc(record);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexScalarRecord<T> MapRecord<T>(this RGaKvIndexScalarRecord<T> record, Func<ulong, ulong> mappingFunc)
        {
            var (index, scalar) = record;

            return new RGaKvIndexScalarRecord<T>(mappingFunc(index), scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexScalarRecord<T> MapRecord<T>(this RGaKvIndexScalarRecord<T> record, Func<ulong, T, RGaKvIndexScalarRecord<T>> mappingFunc)
        {
            var (index, scalar) = record;

            return mappingFunc(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexScalarRecord<T> MapRecord<T>(this RGaKvIndexScalarRecord<T> record, Func<RGaKvIndexScalarRecord<T>, RGaKvIndexScalarRecord<T>> mappingFunc)
        {
            return mappingFunc(record);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexScalarRecord<T> MapRecord<T>(this RGaKvIndexScalarRecord<T> record, Func<ulong, RGaGradeKvIndexRecord> mappingFunc)
        {
            var (index, scalar) = record;

            var (grade, index1) = mappingFunc(index);

            return new RGaGradeKvIndexScalarRecord<T>(grade, index1, scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairScalarRecord<T> MapRecord<T>(this RGaKvIndexPairScalarRecord<T> record, Func<ulong, ulong> mappingFunc)
        {
            var (index1, index2, scalar) = record;

            return new RGaKvIndexPairScalarRecord<T>(
                mappingFunc(index1),
                mappingFunc(index2),
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairScalarRecord<T> MapRecord<T>(this RGaKvIndexPairScalarRecord<T> record, Func<ulong, ulong, RGaKvIndexPairRecord> mappingFunc)
        {
            var (index1, index2, scalar) = record;

            (index1, index2) = mappingFunc(index1, index2);

            return new RGaKvIndexPairScalarRecord<T>(index1, index2, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairScalarRecord<T> MapRecord<T>(this RGaKvIndexPairScalarRecord<T> record, Func<RGaKvIndexPairRecord, RGaKvIndexPairRecord> mappingFunc)
        {
            var (index1, index2, scalar) = record;

            (index1, index2) = mappingFunc(new RGaKvIndexPairRecord(index1, index2));

            return new RGaKvIndexPairScalarRecord<T>(index1, index2, scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexPairScalarRecord<T> MapRecord<T>(this RGaKvIndexPairScalarRecord<T> record, Func<ulong, ulong, RGaGradeKvIndexPairRecord> mappingFunc)
        {
            var (index1, index2, scalar) = record;

            var (grade, index3, index4) = mappingFunc(index1, index2);

            return new RGaGradeKvIndexPairScalarRecord<T>(grade, index3, index4, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexPairScalarRecord<T> MapRecord<T>(this RGaKvIndexPairScalarRecord<T> record, Func<RGaKvIndexPairRecord, RGaGradeKvIndexPairRecord> mappingFunc)
        {
            var (index1, index2, scalar) = record;

            var (grade, index3, index4) =
                mappingFunc(new RGaKvIndexPairRecord(index1, index2));

            return new RGaGradeKvIndexPairScalarRecord<T>(grade, index3, index4, scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairScalarRecord<T> MapRecord<T>(this RGaGradeKvIndexPairScalarRecord<T> record, Func<uint, ulong, ulong> mappingFunc)
        {
            var (grade, index1, index2, scalar) = record;

            return new RGaKvIndexPairScalarRecord<T>(
                mappingFunc(grade, index1),
                mappingFunc(grade, index2),
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairScalarRecord<T> MapRecord<T>(this RGaGradeKvIndexPairScalarRecord<T> record, Func<RGaGradeKvIndexRecord, ulong> mappingFunc)
        {
            var (grade, index1, index2, scalar) = record;

            return new RGaKvIndexPairScalarRecord<T>(
                mappingFunc(new RGaGradeKvIndexRecord(grade, index1)),
                mappingFunc(new RGaGradeKvIndexRecord(grade, index2)),
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairScalarRecord<T> MapRecord<T>(this RGaGradeKvIndexPairScalarRecord<T> record, Func<uint, ulong, ulong, RGaKvIndexPairRecord> mappingFunc)
        {
            var (grade, index1, index2, scalar) = record;

            (index1, index2) = mappingFunc(grade, index1, index2);

            return new RGaKvIndexPairScalarRecord<T>(index1, index2, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairScalarRecord<T> MapRecord<T>(this RGaGradeKvIndexPairScalarRecord<T> record, Func<RGaGradeKvIndexPairRecord, RGaKvIndexPairRecord> mappingFunc)
        {
            var (grade, index1, index2, scalar) = record;

            (index1, index2) = mappingFunc(new RGaGradeKvIndexPairRecord(grade, index1, index2));

            return new RGaKvIndexPairScalarRecord<T>(index1, index2, scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexPairScalarRecord<T> MapRecord<T>(this RGaGradeKvIndexPairScalarRecord<T> record, Func<ulong, ulong> mappingFunc)
        {
            var (grade, index1, index2, scalar) = record;

            return new RGaGradeKvIndexPairScalarRecord<T>(grade, mappingFunc(index1), mappingFunc(index2), scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexPairScalarRecord<T> MapRecord<T>(this RGaGradeKvIndexPairScalarRecord<T> record, Func<ulong, ulong, RGaKvIndexPairRecord> mappingFunc)
        {
            var (grade, index1, index2, scalar) = record;

            (index1, index2) = mappingFunc(index1, index2);

            return new RGaGradeKvIndexPairScalarRecord<T>(grade, index1, index2, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexPairScalarRecord<T> MapRecord<T>(this RGaGradeKvIndexPairScalarRecord<T> record, Func<RGaKvIndexPairRecord, RGaKvIndexPairRecord> mappingFunc)
        {
            var (grade, index1, index2, scalar) = record;

            (index1, index2) = mappingFunc(new RGaKvIndexPairRecord(index1, index2));

            return new RGaGradeKvIndexPairScalarRecord<T>(grade, index1, index2, scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 LeftFoldIndices<T2>(this RGaKvIndexPairRecord indexPair, T2 initialScalar, Func<T2, ulong, T2> indexMapping)
        {
            return
                indexMapping(
                    indexMapping(
                        initialScalar,
                        indexPair.KvIndex1
                    ),
                    indexPair.KvIndex2
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> LeftScanIndices<T2>(this RGaKvIndexPairRecord indexPair, T2 initialScalar, Func<T2, ulong, T2> indexMapping)
        {
            var index = initialScalar;
            yield return index;

            index = indexMapping(index, indexPair.KvIndex1);
            yield return index;

            index = indexMapping(index, indexPair.KvIndex2);
            yield return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 RightFoldIndices<T2>(this RGaKvIndexPairRecord indexPair, T2 initialScalar, Func<ulong, T2, T2> indexMapping)
        {
            return
                indexMapping(
                    indexPair.KvIndex1,
                    indexMapping(
                        indexPair.KvIndex2,
                        initialScalar
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> RightScanIndices<T2>(this RGaKvIndexPairRecord indexPair, T2 initialScalar, Func<ulong, T2, T2> indexMapping)
        {
            var index = initialScalar;
            yield return index;

            index = indexMapping(indexPair.KvIndex2, index);
            yield return index;

            index = indexMapping(indexPair.KvIndex1, index);
            yield return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 ReduceIndices<T2>(this RGaKvIndexPairRecord indexPair, Func<ulong, ulong, T2> indexMapping)
        {
            return indexMapping(indexPair.KvIndex1, indexPair.KvIndex2);
        }

        /// <summary>
        /// Returns a new indexPair containing (indexPair.Index2, nextIndex)
        /// </summary>
        /// <param name="indexPair"></param>
        /// <param name="nextIndex"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairRecord NextPair(this RGaKvIndexPairRecord indexPair, ulong nextIndex)
        {
            return new RGaKvIndexPairRecord(indexPair.KvIndex2, nextIndex);
        }

        /// <summary>
        /// Returns a new indexPair containing (previousIndex, indexPair.Index1)
        /// </summary>
        /// <param name="indexPair"></param>
        /// <param name="previousIndex"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairRecord PreviousPair(this RGaKvIndexPairRecord indexPair, ulong previousIndex)
        {
            return new RGaKvIndexPairRecord(previousIndex, indexPair.KvIndex1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairRecord Transpose(this RGaKvIndexPairRecord indexPair)
        {
            var (index1, index2) = indexPair;

            return new RGaKvIndexPairRecord(index2, index1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexPairRecord Transpose(this RGaGradeKvIndexPairRecord record)
        {
            var (grade, index1, index2) = record;

            return new RGaGradeKvIndexPairRecord(grade, index2, index1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairScalarRecord<T> Transpose<T>(this RGaKvIndexPairScalarRecord<T> record)
        {
            var (index1, index2, value) = record;

            return new RGaKvIndexPairScalarRecord<T>(index2, index1, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexPairScalarRecord<T> Transpose<T>(this RGaGradeKvIndexPairScalarRecord<T> record)
        {
            var (grade, index1, index2, value) = record;

            return new RGaGradeKvIndexPairScalarRecord<T>(grade, index2, index1, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexPairRecord> Transpose(this IEnumerable<RGaKvIndexPairRecord> indexRecords)
        {
            return indexRecords.Select(record => record.Transpose());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexPairScalarRecord<T>> Transpose<T>(this IEnumerable<RGaKvIndexPairScalarRecord<T>> indexScalarRecords)
        {
            return indexScalarRecords.Select(record => record.Transpose());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaGradeKvIndexPairRecord> Transpose(this IEnumerable<RGaGradeKvIndexPairRecord> gradeIndexRecords)
        {
            return gradeIndexRecords.Select(record => record.Transpose());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> Transpose<T>(this IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> gradeIndexScalarRecords)
        {
            return gradeIndexScalarRecords.Select(record => record.Transpose());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexPairScalarRecord<T>> MapRecords<T>(this IEnumerable<RGaKvIndexPairScalarRecord<T>> indexScalarRecords, Func<ulong, ulong, RGaKvIndexPairRecord> indexMapping)
        {
            return indexScalarRecords.Select(record => record.MapRecord(indexMapping));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexPairScalarRecord<T>> MapRecords<T>(this IEnumerable<RGaKvIndexPairScalarRecord<T>> indexScalarRecords, Func<RGaKvIndexPairRecord, RGaKvIndexPairRecord> indexMapping)
        {
            return indexScalarRecords.Select(record => record.MapRecord(indexMapping));
        }
    }
}