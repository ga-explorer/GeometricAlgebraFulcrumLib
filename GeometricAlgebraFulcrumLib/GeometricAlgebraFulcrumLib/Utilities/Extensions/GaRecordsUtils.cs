using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaRecordsUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetEvenKeyValueRecords<T>(this IEnumerable<GradeIndexScalarRecord<T>> gradedList)
        {
            return gradedList.Select(
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
        public static IEnumerable<IndexScalarRecord<T>> GetEvenKeyValueRecords<T>(this IEnumerable<GradeIndexScalarRecord<T>> gradedList, Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return gradedList.Select(
                record =>
                {
                    var (grade, index, value) = record;

                    return new IndexScalarRecord<T>(
                        gradeKeyToEvenKeyMapping(grade, index),
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexScalarRecord<T>> GetGradeKeyValueRecords<T>(this IEnumerable<GradeIndexScalarRecord<T>> gradedList, Func<uint, ulong, GradeIndexRecord> gradeKeyMapping)
        {
            return gradedList.Select(
                record =>
                {
                    var (grade, index, value) = record;
                    var (newGrade, newKey) = gradeKeyMapping(grade, index);

                    return new GradeIndexScalarRecord<T>(
                        newGrade,
                        newKey,
                        value
                    );
                }
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetEvenKeyRecords(this IEnumerable<GradeIndexRecord> gradedList)
        {
            return gradedList.Select(
                record =>
                {
                    var (grade, index) = record;

                    return index.BasisBladeIndexToId(grade);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetEvenKeyRecords(this IEnumerable<GradeIndexRecord> gradedList, Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return gradedList.Select(
                record =>
                {
                    var (grade, index) = record;

                    return gradeKeyToEvenKeyMapping(grade, index);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexRecord> GetGradeKeyRecords(this IEnumerable<GradeIndexRecord> gradedList, Func<uint, ulong, GradeIndexRecord> gradeKeyMapping)
        {
            return gradedList.Select(
                record =>
                {
                    var (grade, index) = record;
                    var (newGrade, newKey) = gradeKeyMapping(grade, index);

                    return new GradeIndexRecord(
                        newGrade,
                        newKey
                    );
                }
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairScalarRecord<T>> GetEvenKeyValueRecords<T>(this IEnumerable<GradeIndexPairScalarRecord<T>> gradedGrid)
        {
            return gradedGrid.Select(
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
        public static IEnumerable<IndexPairScalarRecord<T>> GetEvenKeyValueRecords<T>(this IEnumerable<GradeIndexPairScalarRecord<T>> gradedGrid, Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return gradedGrid.Select(
                record =>
                {
                    var (grade, index1, index2, value) = record;

                    return new IndexPairScalarRecord<T>(
                        gradeKeyToEvenKeyMapping(grade, index1),
                        gradeKeyToEvenKeyMapping(grade, index2),
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairScalarRecord<T>> GetEvenKeyValueRecords<T>(this IEnumerable<GradeIndexPairScalarRecord<T>> gradedGrid, Func<uint, ulong, ulong, IndexPairRecord> gradeKeyToEvenKeyMapping)
        {
            return gradedGrid.Select(
                record =>
                {
                    var (grade, index1, index2, value) = record;
                    var (evenKey1, evenKey2) = gradeKeyToEvenKeyMapping(grade, index1, index2);

                    return new IndexPairScalarRecord<T>(
                        evenKey1,
                        evenKey2,
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexPairScalarRecord<T>> GetGradeKeyValueRecords<T>(this IEnumerable<GradeIndexPairScalarRecord<T>> gradedGrid, Func<uint, ulong, ulong, GradeIndexPairRecord> gradeKeyMapping)
        {
            return gradedGrid.Select(
                record =>
                {
                    var (grade, index1, index2, value) = record;
                    var (newGrade, newKey1, newKey2) = gradeKeyMapping(grade, index1, index2);

                    return new GradeIndexPairScalarRecord<T>(
                        newGrade,
                        newKey1,
                        newKey2,
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairRecord> GetEvenKeyRecords(this IEnumerable<GradeIndexPairRecord> gradedGrid)
        {
            return gradedGrid.Select(
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
        public static IEnumerable<IndexPairRecord> GetEvenKeyRecords(this IEnumerable<GradeIndexPairRecord> gradedGrid, Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return gradedGrid.Select(
                record =>
                {
                    var (grade, index1, index2) = record;

                    return new IndexPairRecord(
                        gradeKeyToEvenKeyMapping(grade, index1),
                        gradeKeyToEvenKeyMapping(grade, index2)
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairRecord> GetEvenKeyRecords(this IEnumerable<GradeIndexPairRecord> gradedGrid, Func<uint, ulong, ulong, IndexPairRecord> gradeKeyToEvenKeyMapping)
        {
            return gradedGrid.Select(
                record =>
                {
                    var (grade, index1, index2) = record;
                    var (evenKey1, evenKey2) = gradeKeyToEvenKeyMapping(grade, index1, index2);

                    return new IndexPairRecord(
                        evenKey1,
                        evenKey2
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexPairRecord> GetGradeKeyRecords(this IEnumerable<GradeIndexPairRecord> gradedGrid, Func<uint, ulong, ulong, GradeIndexPairRecord> gradeKeyMapping)
        {
            return gradedGrid.Select(
                record =>
                {
                    var (grade, index1, index2) = record;
                    var (newGrade, newKey1, newKey2) = gradeKeyMapping(grade, index1, index2);

                    return new GradeIndexPairRecord(
                        newGrade,
                        newKey1,
                        newKey2
                    );
                }
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetKeys(this IndexPairRecord indexPair)
        {
            yield return indexPair.Index1;
            yield return indexPair.Index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetKeys(this GradeIndexPairRecord gradeKeyPair)
        {
            yield return gradeKeyPair.Key1;
            yield return gradeKeyPair.Key2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetKeys<T>(this IndexPairScalarRecord<T> indexPairValue)
        {
            yield return indexPairValue.Index1;
            yield return indexPairValue.Index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetKeys<T>(this GradeIndexPairScalarRecord<T> gradeKeyPairValue)
        {
            yield return gradeKeyPairValue.Index1;
            yield return gradeKeyPairValue.Index2;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord MapKeys(this IndexPairRecord indexPair, Func<ulong, ulong> indexMapping)
        {
            return new IndexPairRecord(
                indexMapping(indexPair.Index1), 
                indexMapping(indexPair.Index2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<T2> MapKeys<T2>(this IndexPairRecord indexPair, Func<ulong, T2> indexMapping)
        {
            return new Pair<T2>(
                indexMapping(indexPair.Index1), 
                indexMapping(indexPair.Index2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexPairRecord MapKeys(this GradeIndexPairRecord gradeKeyPair, Func<ulong, ulong> indexMapping)
        {
            return new GradeIndexPairRecord(
                gradeKeyPair.Grade,
                indexMapping(gradeKeyPair.Key1), 
                indexMapping(gradeKeyPair.Key2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> MapKeys<T>(this IndexPairScalarRecord<T> indexPairValue, Func<ulong, ulong> indexMapping)
        {
            return new IndexPairScalarRecord<T>(
                indexMapping(indexPairValue.Index1), 
                indexMapping(indexPairValue.Index2),
                indexPairValue.Scalar
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexPairScalarRecord<T> MapKeys<T>(this GradeIndexPairScalarRecord<T> gradeKeyPairValue, Func<ulong, ulong> indexMapping)
        {
            return new GradeIndexPairScalarRecord<T>(
                gradeKeyPairValue.Grade,
                indexMapping(gradeKeyPairValue.Index1), 
                indexMapping(gradeKeyPairValue.Index2),
                gradeKeyPairValue.Scalar
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord MapKeys(this IndexPairRecord indexPair, Func<int, ulong, ulong> indexMapping)
        {
            return new IndexPairRecord(
                indexMapping(0, indexPair.Index1), 
                indexMapping(1, indexPair.Index2)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 LeftFoldKeys<T2>(this IndexPairRecord indexPair, T2 initialValue, Func<T2, ulong, T2> indexMapping)
        {
            return 
                indexMapping(
                    indexMapping(
                        initialValue,
                        indexPair.Index1
                    ),
                    indexPair.Index2
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> LeftScanKeys<T2>(this IndexPairRecord indexPair, T2 initialValue, Func<T2, ulong, T2> indexMapping)
        {
            var index = initialValue;
            yield return index;

            index = indexMapping(index, indexPair.Index1);
            yield return index;

            index = indexMapping(index, indexPair.Index2);
            yield return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 RightFoldKeys<T2>(this IndexPairRecord indexPair, T2 initialValue, Func<ulong, T2, T2> indexMapping)
        {
            return 
                indexMapping(
                    indexPair.Index1, 
                    indexMapping(
                        indexPair.Index2, 
                        initialValue
                    )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> RightScanKeys<T2>(this IndexPairRecord indexPair, T2 initialValue, Func<ulong, T2, T2> indexMapping)
        {
            var index = initialValue;
            yield return index;

            index = indexMapping(indexPair.Index2, index);
            yield return index;

            index = indexMapping(indexPair.Index1, index);
            yield return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 ReduceKeys<T2>(this IndexPairRecord indexPair, Func<ulong, ulong, T2> indexMapping)
        {
            return indexMapping(indexPair.Index1, indexPair.Index2);
        }

        /// <summary>
        /// Returns a new indexPair containing (indexPair.Key2, nextKey)
        /// </summary>
        /// <param name="indexPair"></param>
        /// <param name="nextKey"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord NextPair(this IndexPairRecord indexPair, ulong nextKey)
        {
            return new IndexPairRecord(indexPair.Index2, nextKey);
        }

        /// <summary>
        /// Returns a new indexPair containing (previousKey, indexPair.Key1)
        /// </summary>
        /// <param name="indexPair"></param>
        /// <param name="previousKey"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord PreviousPair(this IndexPairRecord indexPair, ulong previousKey)
        {
            return new IndexPairRecord(previousKey, indexPair.Index1);
        }

        /// <summary>
        /// Returns a new indexPair containing (indexPair.Key2, indexPair.Key1)
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord SwapKeys(this IndexPairRecord indexPair)
        {
            return new IndexPairRecord(indexPair.Index2, indexPair.Index1);
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
        public static IEnumerable<T2> GetMappedValues<T, T2>(this IEnumerable<IndexScalarRecord<T>> records, Func<ulong, T, T2> indexValueMapping)
        {
            return records.Select(
                record => indexValueMapping(record.Index, record.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> GetMappedValues<T, T2>(this IEnumerable<IndexPairScalarRecord<T>> records, Func<ulong, ulong, T, T2> indexValueMapping)
        {
            return records.Select(
                record => indexValueMapping(record.Index1, record.Index2, record.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> GetMappedValues<T, T2>(this IEnumerable<GradeIndexScalarRecord<T>> records, Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            return records.Select(
                record => gradeKeyValueMapping(record.Grade, record.Index, record.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> GetMappedValues<T, T2>(this IEnumerable<GradeIndexPairScalarRecord<T>> records, Func<uint, T, T2> gradeValueMapping)
        {
            return records.Select(
                record => gradeValueMapping(record.Grade, record.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> GetMappedValues<T, T2>(this IEnumerable<GradeIndexPairScalarRecord<T>> records, Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            return records.Select(
                record => gradeKeyValueMapping(record.Grade, record.Index1, record.Index2, record.Scalar)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong TimesKeys(this IndexPairRecord record)
        {
            var (index1, index2) = record;

            return index1 * index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairRecord AddOne(this IndexPairRecord record)
        {
            var (index1, index2) = record;

            return new IndexPairRecord(index1 + 1, index2 + 1);
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
        public static IEnumerable<IndexPairRecord> GetKeyPairsInRange(int maxKey1, int maxKey2)
        {
            for (var k1 = 0; k1 <= maxKey1; k1++)
            for (var k2 = 0; k2 <= maxKey2; k2++)
                yield return new IndexPairRecord((ulong) k1, (ulong) k2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairRecord> GetKeyPairsInRange(ulong maxKey1, ulong maxKey2)
        {
            for (var k1 = 0UL; k1 <= maxKey1; k1++)
            for (var k2 = 0UL; k2 <= maxKey2; k2++)
                yield return new IndexPairRecord(k1, k2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairRecord> GetKeyPairsInRange(this IndexPairRecord maxKeyPair)
        {
            var (maxKey1, maxKey2) = maxKeyPair;

            for (var k1 = 0UL; k1 <= maxKey1; k1++)
            for (var k2 = 0UL; k2 <= maxKey2; k2++)
                yield return new IndexPairRecord(k1, k2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexScalarRecord<T> GetKeyValueRecord<T>(this GradeIndexScalarRecord<T> record, Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            var (grade, index, value) = record;

            return new IndexScalarRecord<T>(gradeKeyToKeyMapping(grade, index), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> GetKeyValueRecord<T>(this GradeIndexPairScalarRecord<T> record, Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            var (grade, index1, index2, value) = record;

            return new IndexPairScalarRecord<T>(
                gradeKeyToKeyMapping(grade, index1), 
                gradeKeyToKeyMapping(grade, index2), 
                value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexRecord GetGradeKeyRecord(this ulong index1, Func<ulong, GradeIndexRecord> indexToGradeKeyMapping)
        {
            var (grade, index) = indexToGradeKeyMapping(index1);

            return new GradeIndexRecord(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexScalarRecord<T> GetGradeKeyValueRecord<T>(this IndexScalarRecord<T> record, Func<ulong, GradeIndexRecord> indexToGradeKeyMapping)
        {
            var (index1, value) = record;
            var (grade, index) = indexToGradeKeyMapping(index1);
            return new GradeIndexScalarRecord<T>(grade, index, value);
        }


    }
}
