using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;

namespace GeometricAlgebraFulcrumLib.Structures.Utils
{
    public static class GaRecordsUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetEvenKeyValueRecords<T>(this IEnumerable<GaRecordGradeKeyValue<T>> gradedList)
        {
            return gradedList.Select(
                record =>
                {
                    var (grade, key, value) = record;

                    return new GaRecordKeyValue<T>(
                        key.BasisBladeIndexToId(grade),
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetEvenKeyValueRecords<T>(this IEnumerable<GaRecordGradeKeyValue<T>> gradedList, Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return gradedList.Select(
                record =>
                {
                    var (grade, key, value) = record;

                    return new GaRecordKeyValue<T>(
                        gradeKeyToEvenKeyMapping(grade, key),
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordGradeKeyValue<T>> GetGradeKeyValueRecords<T>(this IEnumerable<GaRecordGradeKeyValue<T>> gradedList, Func<uint, ulong, GaRecordGradeKey> gradeKeyMapping)
        {
            return gradedList.Select(
                record =>
                {
                    var (grade, key, value) = record;
                    var (newGrade, newKey) = gradeKeyMapping(grade, key);

                    return new GaRecordGradeKeyValue<T>(
                        newGrade,
                        newKey,
                        value
                    );
                }
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetEvenKeyRecords(this IEnumerable<GaRecordGradeKey> gradedList)
        {
            return gradedList.Select(
                record =>
                {
                    var (grade, key) = record;

                    return key.BasisBladeIndexToId(grade);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetEvenKeyRecords(this IEnumerable<GaRecordGradeKey> gradedList, Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return gradedList.Select(
                record =>
                {
                    var (grade, key) = record;

                    return gradeKeyToEvenKeyMapping(grade, key);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordGradeKey> GetGradeKeyRecords(this IEnumerable<GaRecordGradeKey> gradedList, Func<uint, ulong, GaRecordGradeKey> gradeKeyMapping)
        {
            return gradedList.Select(
                record =>
                {
                    var (grade, key) = record;
                    var (newGrade, newKey) = gradeKeyMapping(grade, key);

                    return new GaRecordGradeKey(
                        newGrade,
                        newKey
                    );
                }
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyPairValue<T>> GetEvenKeyValueRecords<T>(this IEnumerable<GaRecordGradeKeyPairValue<T>> gradedGrid)
        {
            return gradedGrid.Select(
                record =>
                {
                    var (grade, key1, key2, value) = record;

                    return new GaRecordKeyPairValue<T>(
                        key1.BasisBladeIndexToId(grade),
                        key2.BasisBladeIndexToId(grade),
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyPairValue<T>> GetEvenKeyValueRecords<T>(this IEnumerable<GaRecordGradeKeyPairValue<T>> gradedGrid, Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return gradedGrid.Select(
                record =>
                {
                    var (grade, key1, key2, value) = record;

                    return new GaRecordKeyPairValue<T>(
                        gradeKeyToEvenKeyMapping(grade, key1),
                        gradeKeyToEvenKeyMapping(grade, key2),
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyPairValue<T>> GetEvenKeyValueRecords<T>(this IEnumerable<GaRecordGradeKeyPairValue<T>> gradedGrid, Func<uint, ulong, ulong, GaRecordKeyPair> gradeKeyToEvenKeyMapping)
        {
            return gradedGrid.Select(
                record =>
                {
                    var (grade, key1, key2, value) = record;
                    var (evenKey1, evenKey2) = gradeKeyToEvenKeyMapping(grade, key1, key2);

                    return new GaRecordKeyPairValue<T>(
                        evenKey1,
                        evenKey2,
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordGradeKeyPairValue<T>> GetGradeKeyValueRecords<T>(this IEnumerable<GaRecordGradeKeyPairValue<T>> gradedGrid, Func<uint, ulong, ulong, GaRecordGradeKeyPair> gradeKeyMapping)
        {
            return gradedGrid.Select(
                record =>
                {
                    var (grade, key1, key2, value) = record;
                    var (newGrade, newKey1, newKey2) = gradeKeyMapping(grade, key1, key2);

                    return new GaRecordGradeKeyPairValue<T>(
                        newGrade,
                        newKey1,
                        newKey2,
                        value
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyPair> GetEvenKeyRecords(this IEnumerable<GaRecordGradeKeyPair> gradedGrid)
        {
            return gradedGrid.Select(
                record =>
                {
                    var (grade, key1, key2) = record;

                    return new GaRecordKeyPair(
                        key1.BasisBladeIndexToId(grade),
                        key2.BasisBladeIndexToId(grade)
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyPair> GetEvenKeyRecords(this IEnumerable<GaRecordGradeKeyPair> gradedGrid, Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return gradedGrid.Select(
                record =>
                {
                    var (grade, key1, key2) = record;

                    return new GaRecordKeyPair(
                        gradeKeyToEvenKeyMapping(grade, key1),
                        gradeKeyToEvenKeyMapping(grade, key2)
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyPair> GetEvenKeyRecords(this IEnumerable<GaRecordGradeKeyPair> gradedGrid, Func<uint, ulong, ulong, GaRecordKeyPair> gradeKeyToEvenKeyMapping)
        {
            return gradedGrid.Select(
                record =>
                {
                    var (grade, key1, key2) = record;
                    var (evenKey1, evenKey2) = gradeKeyToEvenKeyMapping(grade, key1, key2);

                    return new GaRecordKeyPair(
                        evenKey1,
                        evenKey2
                    );
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordGradeKeyPair> GetGradeKeyRecords(this IEnumerable<GaRecordGradeKeyPair> gradedGrid, Func<uint, ulong, ulong, GaRecordGradeKeyPair> gradeKeyMapping)
        {
            return gradedGrid.Select(
                record =>
                {
                    var (grade, key1, key2) = record;
                    var (newGrade, newKey1, newKey2) = gradeKeyMapping(grade, key1, key2);

                    return new GaRecordGradeKeyPair(
                        newGrade,
                        newKey1,
                        newKey2
                    );
                }
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetKeys(this GaRecordKeyPair keyPair)
        {
            yield return keyPair.Key1;
            yield return keyPair.Key2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetKeys(this GaRecordGradeKeyPair gradeKeyPair)
        {
            yield return gradeKeyPair.Key1;
            yield return gradeKeyPair.Key2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetKeys<T>(this GaRecordKeyPairValue<T> keyPairValue)
        {
            yield return keyPairValue.Key1;
            yield return keyPairValue.Key2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetKeys<T>(this GaRecordGradeKeyPairValue<T> gradeKeyPairValue)
        {
            yield return gradeKeyPairValue.Key1;
            yield return gradeKeyPairValue.Key2;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyPair MapKeys(this GaRecordKeyPair keyPair, Func<ulong, ulong> keyMapping)
        {
            return new GaRecordKeyPair(
                keyMapping(keyPair.Key1), 
                keyMapping(keyPair.Key2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<T2> MapKeys<T2>(this GaRecordKeyPair keyPair, Func<ulong, T2> keyMapping)
        {
            return new Pair<T2>(
                keyMapping(keyPair.Key1), 
                keyMapping(keyPair.Key2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordGradeKeyPair MapKeys(this GaRecordGradeKeyPair gradeKeyPair, Func<ulong, ulong> keyMapping)
        {
            return new GaRecordGradeKeyPair(
                gradeKeyPair.Grade,
                keyMapping(gradeKeyPair.Key1), 
                keyMapping(gradeKeyPair.Key2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyPairValue<T> MapKeys<T>(this GaRecordKeyPairValue<T> keyPairValue, Func<ulong, ulong> keyMapping)
        {
            return new GaRecordKeyPairValue<T>(
                keyMapping(keyPairValue.Key1), 
                keyMapping(keyPairValue.Key2),
                keyPairValue.Value
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordGradeKeyPairValue<T> MapKeys<T>(this GaRecordGradeKeyPairValue<T> gradeKeyPairValue, Func<ulong, ulong> keyMapping)
        {
            return new GaRecordGradeKeyPairValue<T>(
                gradeKeyPairValue.Grade,
                keyMapping(gradeKeyPairValue.Key1), 
                keyMapping(gradeKeyPairValue.Key2),
                gradeKeyPairValue.Value
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyPair MapKeys(this GaRecordKeyPair keyPair, Func<int, ulong, ulong> keyMapping)
        {
            return new GaRecordKeyPair(
                keyMapping(0, keyPair.Key1), 
                keyMapping(1, keyPair.Key2)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 LeftFoldKeys<T2>(this GaRecordKeyPair keyPair, T2 initialValue, Func<T2, ulong, T2> keyMapping)
        {
            return 
                keyMapping(
                    keyMapping(
                        initialValue,
                        keyPair.Key1
                    ),
                    keyPair.Key2
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> LeftScanKeys<T2>(this GaRecordKeyPair keyPair, T2 initialValue, Func<T2, ulong, T2> keyMapping)
        {
            var key = initialValue;
            yield return key;

            key = keyMapping(key, keyPair.Key1);
            yield return key;

            key = keyMapping(key, keyPair.Key2);
            yield return key;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 RightFoldKeys<T2>(this GaRecordKeyPair keyPair, T2 initialValue, Func<ulong, T2, T2> keyMapping)
        {
            return 
                keyMapping(
                    keyPair.Key1, 
                    keyMapping(
                        keyPair.Key2, 
                        initialValue
                    )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> RightScanKeys<T2>(this GaRecordKeyPair keyPair, T2 initialValue, Func<ulong, T2, T2> keyMapping)
        {
            var key = initialValue;
            yield return key;

            key = keyMapping(keyPair.Key2, key);
            yield return key;

            key = keyMapping(keyPair.Key1, key);
            yield return key;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T2 ReduceKeys<T2>(this GaRecordKeyPair keyPair, Func<ulong, ulong, T2> keyMapping)
        {
            return keyMapping(keyPair.Key1, keyPair.Key2);
        }

        /// <summary>
        /// Returns a new keyPair containing (keyPair.Key2, nextKey)
        /// </summary>
        /// <param name="keyPair"></param>
        /// <param name="nextKey"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyPair NextPair(this GaRecordKeyPair keyPair, ulong nextKey)
        {
            return new GaRecordKeyPair(keyPair.Key2, nextKey);
        }

        /// <summary>
        /// Returns a new keyPair containing (previousKey, keyPair.Key1)
        /// </summary>
        /// <param name="keyPair"></param>
        /// <param name="previousKey"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyPair PreviousPair(this GaRecordKeyPair keyPair, ulong previousKey)
        {
            return new GaRecordKeyPair(previousKey, keyPair.Key1);
        }

        /// <summary>
        /// Returns a new keyPair containing (keyPair.Key2, keyPair.Key1)
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyPair SwapKeys(this GaRecordKeyPair keyPair)
        {
            return new GaRecordKeyPair(keyPair.Key2, keyPair.Key1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrderedAscending(this GaRecordKeyPair record)
        {
            return record.Key1 < record.Key2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrderedAscendingOrEqual(this GaRecordKeyPair record)
        {
            return record.Key1 <= record.Key2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrderedDescending(this GaRecordKeyPair record)
        {
            return record.Key1 > record.Key2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrderedDescendingOrEqual(this GaRecordKeyPair record)
        {
            return record.Key1 >= record.Key2;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> GetMappedValues<T, T2>(this IEnumerable<GaRecordKeyValue<T>> records, Func<ulong, T, T2> keyValueMapping)
        {
            return records.Select(
                record => keyValueMapping(record.Key, record.Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> GetMappedValues<T, T2>(this IEnumerable<GaRecordKeyPairValue<T>> records, Func<ulong, ulong, T, T2> keyValueMapping)
        {
            return records.Select(
                record => keyValueMapping(record.Key1, record.Key2, record.Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> GetMappedValues<T, T2>(this IEnumerable<GaRecordGradeKeyValue<T>> records, Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            return records.Select(
                record => gradeKeyValueMapping(record.Grade, record.Key, record.Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> GetMappedValues<T, T2>(this IEnumerable<GaRecordGradeKeyPairValue<T>> records, Func<uint, T, T2> gradeValueMapping)
        {
            return records.Select(
                record => gradeValueMapping(record.Grade, record.Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T2> GetMappedValues<T, T2>(this IEnumerable<GaRecordGradeKeyPairValue<T>> records, Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            return records.Select(
                record => gradeKeyValueMapping(record.Grade, record.Key1, record.Key2, record.Value)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong TimesKeys(this GaRecordKeyPair record)
        {
            var (key1, key2) = record;

            return key1 * key2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyPair AddOne(this GaRecordKeyPair record)
        {
            var (key1, key2) = record;

            return new GaRecordKeyPair(key1 + 1, key2 + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordGradeKeyPair Transpose(this GaRecordGradeKeyPair record)
        {
            var (grade, key1, key2) = record;

            return new GaRecordGradeKeyPair(grade, key2, key1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyPairValue<T> Transpose<T>(this GaRecordKeyPairValue<T> record)
        {
            var (key1, key2, value) = record;

            return new GaRecordKeyPairValue<T>(key2, key1, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordGradeKeyPairValue<T> Transpose<T>(this GaRecordGradeKeyPairValue<T> record)
        {
            var (grade, key1, key2, value) = record;

            return new GaRecordGradeKeyPairValue<T>(grade, key2, key1, value);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyPair> GetKeyPairsInRange(int maxKey1, int maxKey2)
        {
            for (var k1 = 0; k1 <= maxKey1; k1++)
            for (var k2 = 0; k2 <= maxKey2; k2++)
                yield return new GaRecordKeyPair((ulong) k1, (ulong) k2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyPair> GetKeyPairsInRange(ulong maxKey1, ulong maxKey2)
        {
            for (var k1 = 0UL; k1 <= maxKey1; k1++)
            for (var k2 = 0UL; k2 <= maxKey2; k2++)
                yield return new GaRecordKeyPair(k1, k2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyPair> GetKeyPairsInRange(this GaRecordKeyPair maxKeyPair)
        {
            var (maxKey1, maxKey2) = maxKeyPair;

            for (var k1 = 0UL; k1 <= maxKey1; k1++)
            for (var k2 = 0UL; k2 <= maxKey2; k2++)
                yield return new GaRecordKeyPair(k1, k2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyValue<T> GetKeyValueRecord<T>(this GaRecordGradeKeyValue<T> record, Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            var (grade, key, value) = record;

            return new GaRecordKeyValue<T>(gradeKeyToKeyMapping(grade, key), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyPairValue<T> GetKeyValueRecord<T>(this GaRecordGradeKeyPairValue<T> record, Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            var (grade, key1, key2, value) = record;

            return new GaRecordKeyPairValue<T>(
                gradeKeyToKeyMapping(grade, key1), 
                gradeKeyToKeyMapping(grade, key2), 
                value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordGradeKey GetGradeKeyRecord(this ulong key1, Func<ulong, GaRecordGradeKey> keyToGradeKeyMapping)
        {
            var (grade, key) = keyToGradeKeyMapping(key1);

            return new GaRecordGradeKey(grade, key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordGradeKeyValue<T> GetGradeKeyValueRecord<T>(this GaRecordKeyValue<T> record, Func<ulong, GaRecordGradeKey> keyToGradeKeyMapping)
        {
            var (key1, value) = record;
            var (grade, key) = keyToGradeKeyMapping(key1);
            return new GaRecordGradeKeyValue<T>(grade, key, value);
        }


    }
}
