using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaFuLRecordUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexRecord GetGradeIndexRecord(this IGradeIndexRecord record)
        {
            return record is GradeIndexRecord gradeIndexRecord
                ? gradeIndexRecord
                : new GradeIndexRecord(record.Grade, record.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexPairRecord GetGradeIndexPairRecord(this IGradeIndexPairRecord record)
        {
            return record is GradeIndexPairRecord gradeIndexPairRecord
                ? gradeIndexPairRecord
                : new GradeIndexPairRecord(record.Grade, record.Index1, record.Index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexScalarRecord<T> GetIndexScalarRecord<T>(this IIndexScalarRecord<T> record)
        {
            return record is IndexScalarRecord<T> indexScalarRecord
                ? indexScalarRecord
                : new IndexScalarRecord<T>(record.Index, record.Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord GetIndexPairRecord(this IIndexPairRecord record)
        {
            return record is IndexPairRecord indexPairRecord
                ? indexPairRecord
                : new IndexPairRecord(record.Index1, record.Index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord GetIndexPairRecord(this IIndexPairRecord record, Func<IndexPairRecord, IndexPairRecord> mappingFunc)
        {
            return mappingFunc(record.GetIndexPairRecord());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord GetIndexPairRecord(this IIndexPairRecord record, Func<ulong, ulong, IndexPairRecord> mappingFunc)
        {
            var (index1, index2) = record.GetIndexPairRecord();

            return mappingFunc(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> GetIndexPairScalarRecord<T>(this IIndexPairScalarRecord<T> record)
        {
            return record is IndexPairScalarRecord<T> indexPairScalarRecord
                ? indexPairScalarRecord
                : new IndexPairScalarRecord<T>(record.Index1, record.Index2, record.Scalar);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords<T>(this IEnumerable<GradeIndexScalarRecord<T>> vectorGradedStorage)
        {
            return vectorGradedStorage.Select(
                record =>
                {
                    var (grade, index, value) = record;

                    return new IndexScalarRecord<T>(
                        index.BasisBladeIndexToId(grade),
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords<T>(this IEnumerable<GradeIndexScalarRecord<T>> vectorGradedStorage, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return vectorGradedStorage.Select(
                record =>
                {
                    var (grade, index, value) = record;

                    return new IndexScalarRecord<T>(
                        gradeIndexToIndexMapping(grade, index),
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords<T>(this IEnumerable<GradeIndexScalarRecord<T>> vectorGradedStorage, Func<uint, ulong, GradeIndexRecord> gradeIndexMapping)
        {
            return vectorGradedStorage.Select(
                record =>
                {
                    var (grade, index, value) = record;
                    var (newGrade, newIndex) = gradeIndexMapping(grade, index);

                    return new GradeIndexScalarRecord<T>(
                        newGrade,
                        newIndex,
                        value
                    );
                }
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices(this IEnumerable<GradeIndexRecord> vectorGradedStorage)
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
        public static IEnumerable<ulong> GetIndices(this IEnumerable<GradeIndexRecord> vectorGradedStorage, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
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
        public static IEnumerable<GradeIndexRecord> GetGradeIndexRecords(this IEnumerable<GradeIndexRecord> vectorGradedStorage, Func<uint, ulong, GradeIndexRecord> gradeIndexMapping)
        {
            return vectorGradedStorage.Select(
                record =>
                {
                    var (grade, index) = record;
                    var (newGrade, newIndex) = gradeIndexMapping(grade, index);

                    return new GradeIndexRecord(
                        newGrade,
                        newIndex
                    );
                }
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairScalarRecord<T>> GetIndexScalarRecords<T>(this IEnumerable<GradeIndexPairScalarRecord<T>> matrixGradedStorage)
        {
            return matrixGradedStorage.Select(
                record =>
                {
                    var (grade, index1, index2, value) = record;

                    return new IndexPairScalarRecord<T>(
                        index1.BasisBladeIndexToId(grade),
                        index2.BasisBladeIndexToId(grade),
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairScalarRecord<T>> GetIndexScalarRecords<T>(this IEnumerable<GradeIndexPairScalarRecord<T>> matrixGradedStorage, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return matrixGradedStorage.Select(
                record =>
                {
                    var (grade, index1, index2, value) = record;

                    return new IndexPairScalarRecord<T>(
                        gradeIndexToIndexMapping(grade, index1),
                        gradeIndexToIndexMapping(grade, index2),
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairScalarRecord<T>> GetIndexScalarRecords<T>(this IEnumerable<GradeIndexPairScalarRecord<T>> matrixGradedStorage, Func<uint, ulong, ulong, IndexPairRecord> gradeIndexToIndexMapping)
        {
            return matrixGradedStorage.Select(
                record =>
                {
                    var (grade, index1, index2, value) = record;
                    var (evenIndex1, evenIndex2) = gradeIndexToIndexMapping(grade, index1, index2);

                    return new IndexPairScalarRecord<T>(
                        evenIndex1,
                        evenIndex2,
                        value
                    );
                }
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexPairScalarRecord<T>> GetGradeIndexScalarRecords<T>(this IEnumerable<GradeIndexPairScalarRecord<T>> matrixGradedStorage, Func<uint, ulong, ulong, GradeIndexPairRecord> gradeIndexMapping)
        {
            return matrixGradedStorage.Select(
                record =>
                {
                    var (grade, index1, index2, value) = record;
                    var (newGrade, newIndex1, newIndex2) = gradeIndexMapping(grade, index1, index2);

                    return new GradeIndexPairScalarRecord<T>(
                        newGrade,
                        newIndex1,
                        newIndex2,
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairRecord> GetEvenIndexRecords(this IEnumerable<GradeIndexPairRecord> matrixGradedStorage)
        {
            return matrixGradedStorage.Select(
                record =>
                {
                    var (grade, index1, index2) = record;

                    return new IndexPairRecord(
                        index1.BasisBladeIndexToId(grade),
                        index2.BasisBladeIndexToId(grade)
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairRecord> GetEvenIndexRecords(this IEnumerable<GradeIndexPairRecord> matrixGradedStorage, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return matrixGradedStorage.Select(
                record =>
                {
                    var (grade, index1, index2) = record;

                    return new IndexPairRecord(
                        gradeIndexToIndexMapping(grade, index1),
                        gradeIndexToIndexMapping(grade, index2)
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairRecord> GetEvenIndexRecords(this IEnumerable<GradeIndexPairRecord> matrixGradedStorage, Func<uint, ulong, ulong, IndexPairRecord> gradeIndexToIndexMapping)
        {
            return matrixGradedStorage.Select(
                record =>
                {
                    var (grade, index1, index2) = record;
                    var (evenIndex1, evenIndex2) = gradeIndexToIndexMapping(grade, index1, index2);

                    return new IndexPairRecord(
                        evenIndex1,
                        evenIndex2
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexPairRecord> GetGradeIndexRecords(this IEnumerable<GradeIndexPairRecord> matrixGradedStorage, Func<uint, ulong, ulong, GradeIndexPairRecord> gradeIndexMapping)
        {
            return matrixGradedStorage.Select(
                record =>
                {
                    var (grade, index1, index2) = record;
                    var (newGrade, newIndex1, newIndex2) = gradeIndexMapping(grade, index1, index2);

                    return new GradeIndexPairRecord(
                        newGrade,
                        newIndex1,
                        newIndex2
                    );
                }
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices(this IndexPairRecord indexPair)
        {
            yield return indexPair.Index1;
            yield return indexPair.Index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices(this GradeIndexPairRecord gradeIndexPair)
        {
            yield return gradeIndexPair.Index1;
            yield return gradeIndexPair.Index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices<T>(this IndexPairScalarRecord<T> indexPairScalar)
        {
            yield return indexPairScalar.Index1;
            yield return indexPairScalar.Index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices<T>(this GradeIndexPairScalarRecord<T> gradeIndexPairScalar)
        {
            yield return gradeIndexPairScalar.Index1;
            yield return gradeIndexPairScalar.Index2;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrderedAscending(this IndexPairRecord record)
        {
            return record.Index1 < record.Index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrderedAscendingOrEqual(this IndexPairRecord record)
        {
            return record.Index1 <= record.Index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrderedDescending(this IndexPairRecord record)
        {
            return record.Index1 > record.Index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrderedDescendingOrEqual(this IndexPairRecord record)
        {
            return record.Index1 >= record.Index2;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong TimesIndices(this IndexPairRecord record)
        {
            var (index1, index2) = record;

            return index1 * index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord Add(this IndexPairRecord record, ulong value)
        {
            var (index1, index2) = record;

            return new IndexPairRecord(index1 + value, index2 + value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord Subtract(this IndexPairRecord record, ulong value)
        {
            var (index1, index2) = record;

            return new IndexPairRecord(index1 - value, index2 - value);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairRecord> GetIndexPairsInRange(int maxIndex1, int maxIndex2)
        {
            for (var k1 = 0; k1 <= maxIndex1; k1++)
            for (var k2 = 0; k2 <= maxIndex2; k2++)
                yield return new IndexPairRecord((ulong) k1, (ulong) k2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairRecord> GetIndexPairsInRange(ulong maxIndex1, ulong maxIndex2)
        {
            for (var k1 = 0UL; k1 <= maxIndex1; k1++)
            for (var k2 = 0UL; k2 <= maxIndex2; k2++)
                yield return new IndexPairRecord(k1, k2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairRecord> GetIndexPairsInRange(this IndexPairRecord maxCountPair)
        {
            var (maxIndex1, maxIndex2) = maxCountPair;

            for (var k1 = 0UL; k1 < maxIndex1; k1++)
            for (var k2 = 0UL; k2 < maxIndex2; k2++)
                yield return new IndexPairRecord(k1, k2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexScalarRecord<T> GetIndexScalarRecord<T>(this GradeIndexScalarRecord<T> record, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            var (grade, index, value) = record;

            return new IndexScalarRecord<T>(gradeIndexToIndexMapping(grade, index), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> GetIndexScalarRecord<T>(this GradeIndexPairScalarRecord<T> record, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            var (grade, index1, index2, value) = record;

            return new IndexPairScalarRecord<T>(
                gradeIndexToIndexMapping(grade, index1), 
                gradeIndexToIndexMapping(grade, index2), 
                value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexRecord GetGradeIndexRecord(this ulong index1, Func<ulong, GradeIndexRecord> indexToGradeIndexMapping)
        {
            var (grade, index) = indexToGradeIndexMapping(index1);

            return new GradeIndexRecord(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexScalarRecord<T> GetGradeIndexScalarRecord<T>(this IndexScalarRecord<T> record, Func<ulong, GradeIndexRecord> indexToGradeIndexMapping)
        {
            var (index1, value) = record;
            var (grade, index) = indexToGradeIndexMapping(index1);
            return new GradeIndexScalarRecord<T>(grade, index, value);
        }


    }
}
