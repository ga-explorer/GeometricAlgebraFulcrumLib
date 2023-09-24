using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records
{
    public static class GaFuLRecordUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexRecord GetGradeIndexRecord(this IRGaGradeKvIndexRecord record)
        {
            return record is RGaGradeKvIndexRecord gradeIndexRecord
                ? gradeIndexRecord
                : new RGaGradeKvIndexRecord(record.Grade, record.KvIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexPairRecord GetGradeIndexPairRecord(this IRGaGradeKvIndexPairRecord record)
        {
            return record is RGaGradeKvIndexPairRecord gradeIndexPairRecord
                ? gradeIndexPairRecord
                : new RGaGradeKvIndexPairRecord(record.Grade, record.KvIndex1, record.KvIndex2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRGaKvIndexScalarRecord<T> GetIndexScalarRecord<T>(this IRGaKvIndexScalarRecord<T> record)
        {
            return record is RGaKvIndexScalarRecord<T> indexScalarRecord
                ? indexScalarRecord
                : new RGaKvIndexScalarRecord<T>(record.KvIndex, record.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairRecord GetIndexPairRecord(this IRGaKvIndexPairRecord record)
        {
            return record is RGaKvIndexPairRecord indexPairRecord
                ? indexPairRecord
                : new RGaKvIndexPairRecord(record.KvIndex1, record.KvIndex2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairRecord GetIndexPairRecord(this IRGaKvIndexPairRecord record, Func<RGaKvIndexPairRecord, RGaKvIndexPairRecord> mappingFunc)
        {
            return mappingFunc(record.GetIndexPairRecord());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairRecord GetIndexPairRecord(this IRGaKvIndexPairRecord record, Func<ulong, ulong, RGaKvIndexPairRecord> mappingFunc)
        {
            var (index1, index2) = record.GetIndexPairRecord();

            return mappingFunc(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairScalarRecord<T> GetIndexPairScalarRecord<T>(this IRGaKvIndexPairScalarRecord<T> record)
        {
            return record is RGaKvIndexPairScalarRecord<T> indexPairScalarRecord
                ? indexPairScalarRecord
                : new RGaKvIndexPairScalarRecord<T>(record.KvIndex1, record.KvIndex2, record.Scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IRGaKvIndexScalarRecord<T>> GetIndexScalarRecords<T>(this IEnumerable<RGaGradeKvIndexScalarRecord<T>> vectorGradedStorage)
        {
            return vectorGradedStorage.Select(
                record =>
                {
                    var (grade, index, value) = record;

                    return new RGaKvIndexScalarRecord<T>(
                        index.BasisBladeIndexToId(grade),
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IRGaKvIndexScalarRecord<T>> GetIndexScalarRecords<T>(this IEnumerable<RGaGradeKvIndexScalarRecord<T>> vectorGradedStorage, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return vectorGradedStorage.Select(
                record =>
                {
                    var (grade, index, value) = record;

                    return new RGaKvIndexScalarRecord<T>(
                        gradeIndexToIndexMapping(grade, index),
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaGradeKvIndexScalarRecord<T>> GetGradeIndexScalarRecords<T>(this IEnumerable<RGaGradeKvIndexScalarRecord<T>> vectorGradedStorage, Func<uint, ulong, RGaGradeKvIndexRecord> gradeIndexMapping)
        {
            return vectorGradedStorage.Select(
                record =>
                {
                    var (grade, index, value) = record;
                    var (newGrade, newIndex) = gradeIndexMapping(grade, index);

                    return new RGaGradeKvIndexScalarRecord<T>(
                        newGrade,
                        newIndex,
                        value
                    );
                }
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices(this IEnumerable<RGaGradeKvIndexRecord> vectorGradedStorage)
        {
            return vectorGradedStorage.Select(
                record =>
                {
                    var (grade, index) = record;

                    return index.BasisBladeIndexToId(grade);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices(this IEnumerable<RGaGradeKvIndexRecord> vectorGradedStorage, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return vectorGradedStorage.Select(
                record =>
                {
                    var (grade, index) = record;

                    return gradeIndexToIndexMapping(grade, index);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaGradeKvIndexRecord> GetGradeIndexRecords(this IEnumerable<RGaGradeKvIndexRecord> vectorGradedStorage, Func<uint, ulong, RGaGradeKvIndexRecord> gradeIndexMapping)
        {
            return vectorGradedStorage.Select(
                record =>
                {
                    var (grade, index) = record;
                    var (newGrade, newIndex) = gradeIndexMapping(grade, index);

                    return new RGaGradeKvIndexRecord(
                        newGrade,
                        newIndex
                    );
                }
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexPairScalarRecord<T>> GetIndexScalarRecords<T>(this IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> matrixGradedStorage)
        {
            return matrixGradedStorage.Select(
                record =>
                {
                    var (grade, index1, index2, value) = record;

                    return new RGaKvIndexPairScalarRecord<T>(
                        index1.BasisBladeIndexToId(grade),
                        index2.BasisBladeIndexToId(grade),
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexPairScalarRecord<T>> GetIndexScalarRecords<T>(this IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> matrixGradedStorage, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return matrixGradedStorage.Select(
                record =>
                {
                    var (grade, index1, index2, value) = record;

                    return new RGaKvIndexPairScalarRecord<T>(
                        gradeIndexToIndexMapping(grade, index1),
                        gradeIndexToIndexMapping(grade, index2),
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexPairScalarRecord<T>> GetIndexScalarRecords<T>(this IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> matrixGradedStorage, Func<uint, ulong, ulong, RGaKvIndexPairRecord> gradeIndexToIndexMapping)
        {
            return matrixGradedStorage.Select(
                record =>
                {
                    var (grade, index1, index2, value) = record;
                    var (evenIndex1, evenIndex2) = gradeIndexToIndexMapping(grade, index1, index2);

                    return new RGaKvIndexPairScalarRecord<T>(
                        evenIndex1,
                        evenIndex2,
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> GetGradeIndexScalarRecords<T>(this IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> matrixGradedStorage, Func<uint, ulong, ulong, RGaGradeKvIndexPairRecord> gradeIndexMapping)
        {
            return matrixGradedStorage.Select(
                record =>
                {
                    var (grade, index1, index2, value) = record;
                    var (newGrade, newIndex1, newIndex2) = gradeIndexMapping(grade, index1, index2);

                    return new RGaGradeKvIndexPairScalarRecord<T>(
                        newGrade,
                        newIndex1,
                        newIndex2,
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexPairRecord> GetEvenIndexRecords(this IEnumerable<RGaGradeKvIndexPairRecord> matrixGradedStorage)
        {
            return matrixGradedStorage.Select(
                record =>
                {
                    var (grade, index1, index2) = record;

                    return new RGaKvIndexPairRecord(
                        index1.BasisBladeIndexToId(grade),
                        index2.BasisBladeIndexToId(grade)
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexPairRecord> GetEvenIndexRecords(this IEnumerable<RGaGradeKvIndexPairRecord> matrixGradedStorage, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return matrixGradedStorage.Select(
                record =>
                {
                    var (grade, index1, index2) = record;

                    return new RGaKvIndexPairRecord(
                        gradeIndexToIndexMapping(grade, index1),
                        gradeIndexToIndexMapping(grade, index2)
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexPairRecord> GetEvenIndexRecords(this IEnumerable<RGaGradeKvIndexPairRecord> matrixGradedStorage, Func<uint, ulong, ulong, RGaKvIndexPairRecord> gradeIndexToIndexMapping)
        {
            return matrixGradedStorage.Select(
                record =>
                {
                    var (grade, index1, index2) = record;
                    var (evenIndex1, evenIndex2) = gradeIndexToIndexMapping(grade, index1, index2);

                    return new RGaKvIndexPairRecord(
                        evenIndex1,
                        evenIndex2
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaGradeKvIndexPairRecord> GetGradeIndexRecords(this IEnumerable<RGaGradeKvIndexPairRecord> matrixGradedStorage, Func<uint, ulong, ulong, RGaGradeKvIndexPairRecord> gradeIndexMapping)
        {
            return matrixGradedStorage.Select(
                record =>
                {
                    var (grade, index1, index2) = record;
                    var (newGrade, newIndex1, newIndex2) = gradeIndexMapping(grade, index1, index2);

                    return new RGaGradeKvIndexPairRecord(
                        newGrade,
                        newIndex1,
                        newIndex2
                    );
                }
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices(this RGaKvIndexPairRecord indexPair)
        {
            yield return indexPair.KvIndex1;
            yield return indexPair.KvIndex2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices(this RGaGradeKvIndexPairRecord gradeIndexPair)
        {
            yield return gradeIndexPair.KvIndex1;
            yield return gradeIndexPair.KvIndex2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices<T>(this RGaKvIndexPairScalarRecord<T> indexPairScalar)
        {
            yield return indexPairScalar.KvIndex1;
            yield return indexPairScalar.KvIndex2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices<T>(this RGaGradeKvIndexPairScalarRecord<T> gradeIndexPairScalar)
        {
            yield return gradeIndexPairScalar.KvIndex1;
            yield return gradeIndexPairScalar.KvIndex2;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrderedAscending(this RGaKvIndexPairRecord record)
        {
            return record.KvIndex1 < record.KvIndex2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrderedAscendingOrEqual(this RGaKvIndexPairRecord record)
        {
            return record.KvIndex1 <= record.KvIndex2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrderedDescending(this RGaKvIndexPairRecord record)
        {
            return record.KvIndex1 > record.KvIndex2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrderedDescendingOrEqual(this RGaKvIndexPairRecord record)
        {
            return record.KvIndex1 >= record.KvIndex2;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong TimesIndices(this RGaKvIndexPairRecord record)
        {
            var (index1, index2) = record;

            return index1 * index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairRecord Add(this RGaKvIndexPairRecord record, ulong value)
        {
            var (index1, index2) = record;

            return new RGaKvIndexPairRecord(index1 + value, index2 + value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairRecord Subtract(this RGaKvIndexPairRecord record, ulong value)
        {
            var (index1, index2) = record;

            return new RGaKvIndexPairRecord(index1 - value, index2 - value);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexPairRecord> GetIndexPairsInRange(int maxIndex1, int maxIndex2)
        {
            for (var k1 = 0; k1 <= maxIndex1; k1++)
                for (var k2 = 0; k2 <= maxIndex2; k2++)
                    yield return new RGaKvIndexPairRecord((ulong)k1, (ulong)k2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexPairRecord> GetIndexPairsInRange(ulong maxIndex1, ulong maxIndex2)
        {
            for (var k1 = 0UL; k1 <= maxIndex1; k1++)
                for (var k2 = 0UL; k2 <= maxIndex2; k2++)
                    yield return new RGaKvIndexPairRecord(k1, k2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexPairRecord> GetIndexPairsInRange(this RGaKvIndexPairRecord maxCountPair)
        {
            var (maxIndex1, maxIndex2) = maxCountPair;

            for (var k1 = 0UL; k1 < maxIndex1; k1++)
                for (var k2 = 0UL; k2 < maxIndex2; k2++)
                    yield return new RGaKvIndexPairRecord(k1, k2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexScalarRecord<T> GetIndexScalarRecord<T>(this RGaGradeKvIndexScalarRecord<T> record, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            var (grade, index, value) = record;

            return new RGaKvIndexScalarRecord<T>(gradeIndexToIndexMapping(grade, index), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairScalarRecord<T> GetIndexScalarRecord<T>(this RGaGradeKvIndexPairScalarRecord<T> record, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            var (grade, index1, index2, value) = record;

            return new RGaKvIndexPairScalarRecord<T>(
                gradeIndexToIndexMapping(grade, index1),
                gradeIndexToIndexMapping(grade, index2),
                value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexRecord GetGradeIndexRecord(this ulong index1, Func<ulong, RGaGradeKvIndexRecord> indexToGradeIndexMapping)
        {
            var (grade, index) = indexToGradeIndexMapping(index1);

            return new RGaGradeKvIndexRecord(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexScalarRecord<T> GetGradeIndexScalarRecord<T>(this RGaKvIndexScalarRecord<T> record, Func<ulong, RGaGradeKvIndexRecord> indexToGradeIndexMapping)
        {
            var (index1, value) = record;
            var (grade, index) = indexToGradeIndexMapping(index1);
            return new RGaGradeKvIndexScalarRecord<T>(grade, index, value);
        }


    }
}
