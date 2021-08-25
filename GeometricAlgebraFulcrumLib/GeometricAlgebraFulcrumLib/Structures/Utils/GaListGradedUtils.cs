using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Utils
{
    public static class GaListGradedUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<ulong>> GetKeysSetsUnion<T>(this IGaListGraded<T> gradedList1, IGaListGraded<T> gradedList2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<ulong>>();
            var gradesSet = gradedList1.GetGradesSetUnion(gradedList2);

            foreach (var grade in gradesSet)
            {
                var evenList1 = gradedList1.GetList(grade);
                var evenList2 = gradedList2.GetList(grade);

               dictionary.Add(grade, evenList1.GetKeysUnion(evenList2));
            }

            return dictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<ulong>> GetKeysSetsIntersection<T>(this IGaListGraded<T> gradedList1, IGaListGraded<T> gradedList2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<ulong>>();
            var gradesSet = gradedList1.GetGradesSetUnion(gradedList2);

            foreach (var grade in gradesSet)
            {
                var evenList1 = gradedList1.GetList(grade);
                var evenList2 = gradedList2.GetList(grade);

                dictionary.Add(grade, evenList1.GetKeysIntersection(evenList2));
            }

            return dictionary;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<ulong>> GetKeysSetsDifference<T>(this IGaListGraded<T> gradedList1, IGaListGraded<T> gradedList2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<ulong>>();
            var gradesSet = gradedList1.GetGradesSetUnion(gradedList2);

            foreach (var grade in gradesSet)
            {
                var evenList1 = gradedList1.GetList(grade);
                var evenList2 = gradedList2.GetList(grade);

                dictionary.Add(grade, evenList1.GetKeysDifference(evenList2));
            }

            return dictionary;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, HashSet<ulong>> GetKeysSetsSymmetricDifference<T>(this IGaListGraded<T> gradedList1, IGaListGraded<T> gradedList2)
        {
            var dictionary = new Dictionary<uint, HashSet<ulong>>();
            var gradesSet = gradedList1.GetGradesSetUnion(gradedList2);

            foreach (var grade in gradesSet)
            {
                var evenList1 = gradedList1.GetList(grade);
                var evenList2 = gradedList2.GetList(grade);

                dictionary.Add(grade, evenList1.GetKeysSetSymmetricDifference(evenList2));
            }

            return dictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this IGaListGraded<T> gradedList, Func<T, T> mappingFunc)
        {
            return gradedList
                .GetValues()
                .Select(mappingFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this IGaListGraded<T> gradedList, Func<uint, ulong, T, T> mappingFunc)
        {
            return gradedList
                .GetGradeKeyValueRecords()
                .Select(keyValue => 
                    mappingFunc(keyValue.Grade, keyValue.Key, keyValue.Value)
                );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListGraded<T> GetCompactList<T>(this IGaListGraded<T> evenList)
        {
            return evenList.TryGetCompactList(out var compactList)
                ? compactList
                : evenList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetEvenKeyValueRecords<T>(this IGaListGraded<T> gradedList)
        {
            return gradedList.GetGradeKeyValueRecords().Select(
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
        public static IEnumerable<GaRecordKeyValue<T>> GetEvenKeyValueRecords<T>(this IGaListGraded<T> gradedList, Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return gradedList.GetGradeKeyValueRecords().Select(
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
        public static IEnumerable<GaRecordGradeKeyValue<T>> GetGradeKeyValueRecords<T>(this IGaListGraded<T> gradedList, Func<uint, ulong, GaRecordGradeKey> gradeKeyMapping)
        {
            return gradedList.GetGradeKeyValueRecords().Select(
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
        public static IEnumerable<ulong> GetEvenKeys<T>(this IGaListGraded<T> gradedList)
        {
            return gradedList.GetGradeKeyRecords().Select(
                record =>
                {
                    var (grade, key) = record;

                    return key.BasisBladeIndexToId(grade);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetEvenKeys<T>(this IGaListGraded<T> gradedList, Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return gradedList.GetGradeKeyRecords().Select(
                record =>
                {
                    var (grade, key) = record;

                    return gradeKeyToEvenKeyMapping(grade, key);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordGradeKey> GetGradeKeyRecords<T>(this IGaListGraded<T> gradedList, Func<uint, ulong, GaRecordGradeKey> gradeKeyMapping)
        {
            return gradedList.GetGradeKeyRecords().Select(
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
        public static bool ContainsGrade<T>(this IGaListGraded<T> gradedList, int grade)
        {
            return grade >= 0 && gradedList.ContainsGrade((uint) grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaListGraded<T> gradedList, uint grade, int key)
        {
            return key < 0
                ? throw new IndexOutOfRangeException()
                : gradedList.GetValue(grade, (ulong) key);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaListGraded<T> gradedList, uint grade, int key, T defaultValue)
        {
            return key >= 0 && gradedList.TryGetValue(grade, (ulong) key, out var value)
                ? value
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaListGraded<T> gradedList, uint grade, ulong key, T defaultValue)
        {
            return gradedList.TryGetValue(grade, key, out var value)
                ? value
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaListGraded<T> gradedList, uint grade, int key, Func<int, T> defaultValueFunc)
        {
            return gradedList.TryGetValue(grade, (ulong) key, out var value)
                ? value
                : defaultValueFunc(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaListGraded<T> gradedList, uint grade, ulong key, Func<ulong, T> defaultValueFunc)
        {
            return gradedList.TryGetList(grade, out var evenList) &&
                   evenList.TryGetValue(key, out var value)
                ? value
                : defaultValueFunc(key);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaListGraded<T> gradedList, uint grade, ulong key, Func<uint, ulong, T> defaultValueFunc)
        {
            return gradedList.TryGetList(grade, out var evenList) &&
                   evenList.TryGetValue(key, out var value)
                ? value
                : defaultValueFunc(grade, key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this IGaListGraded<T> gradedList, uint grade, int key, out T value)
        {
            if (key >= 0 && gradedList.TryGetList(grade, out var evenList) && evenList.TryGetValue((ulong) key, out value))
                return true;

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this IGaListGraded<T> gradedList, uint grade, ulong key, out T value)
        {
            if (gradedList.TryGetList(grade, out var evenList) && evenList.TryGetValue(key, out value))
                return true;

            value = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetList<T>(this IGaListGraded<T> gradedList, int grade, out IGaListEven<T> evenList)
        {
            if (grade >= 0 && gradedList.TryGetList((uint) grade, out evenList))
                return true;

            evenList = null;
            return false;
        }


        /// <summary>
        /// The even list corresponding to the smallest grade stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> GetMinGradeList<T>(this IGaListGraded<T> gradedList)
        {
            return gradedList.GetList(gradedList.GetMinGrade());
        }

        /// <summary>
        /// The even list corresponding to the largest grade stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> GetMaxGradeList<T>(this IGaListGraded<T> gradedList)
        {
            return gradedList.GetList(gradedList.GetMaxGrade());
        }
        
        /// <summary>
        /// The smallest grade and corresponding even list stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordGradeValue<IGaListEven<T>> GetMinGradeListRecord<T>(this IGaListGraded<T> gradedList)
        {
            var grade = gradedList.GetMinGrade();

            return new GaRecordGradeValue<IGaListEven<T>>(grade, gradedList.GetList(grade));
        }
        
        /// <summary>
        /// The largest grade and corresponding even list stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordGradeValue<IGaListEven<T>> GetMaxGradeListRecord<T>(this IGaListGraded<T> gradedList)
        {
            var grade = gradedList.GetMaxGrade();

            return new GaRecordGradeValue<IGaListEven<T>>(grade, gradedList.GetList(grade));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetKeyValueRecords<T>(this IGaListGraded<T> gradedList)
        {
            return gradedList
                .GetGradeKeyValueRecords()
                .Select(record => record.GetKeyValueRecord(GaBasisBladeUtils.BasisBladeGradeIndexToId));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyValue<T>> GetKeyValueRecords<T>(this IGaListGraded<T> gradedList, Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            return gradedList
                .GetGradeKeyValueRecords()
                .Select(record => record.GetKeyValueRecord(GaBasisBladeUtils.BasisBladeGradeIndexToId));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetEmptyKeys<T>(this IGaListGraded<T> gradedList, uint grade, ulong maxKey)
        {
            return gradedList.TryGetList(grade, out var evenList) 
                ? evenList.GetEmptyKeys(maxKey) 
                : (maxKey + 1).GetRange();
        }

        
        /// <summary>
        /// The value corresponding to the smallest key stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMinKey<T>(this IGaListGraded<T> gradedList, uint grade)
        {
            return gradedList.GetList(grade).GetMinKey();
        }

        /// <summary>
        /// The value corresponding to the largest key stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxKey<T>(this IGaListGraded<T> gradedList, uint grade)
        {
            return gradedList.GetList(grade).GetMaxKey();
        }


        /// <summary>
        /// The value corresponding to the smallest key stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMinKeyValue<T>(this IGaListGraded<T> gradedList, uint grade)
        {
            return gradedList.GetList(grade).GetMinKeyValue();
        }

        /// <summary>
        /// The value corresponding to the largest key stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMaxKeyValue<T>(this IGaListGraded<T> gradedList, uint grade)
        {
            return gradedList.GetList(grade).GetMaxKeyValue();
        }


        /// <summary>
        /// The smallest key and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        public static GaRecordKeyValue<T> GetMinKeyValueRecord<T>(this IGaListGraded<T> gradedList, uint grade)
        {
            return gradedList.GetList(grade).GetMinKeyValueRecord();
        }

        /// <summary>
        /// The largest key and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        public static GaRecordKeyValue<T> GetMaxKeyValueRecord<T>(this IGaListGraded<T> gradedList, uint grade)
        {
            return gradedList.GetList(grade).GetMaxKeyValueRecord();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfVector<T>(this IGaListGraded<T> gradeIndexScalarList)
        {
            return gradeIndexScalarList
                .GetList(1)
                .GetMinVSpaceDimensionOfVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfBivector<T>(this IGaListGraded<T> gradeIndexScalarList)
        {
            return gradeIndexScalarList
                .GetList(2)
                .GetMinVSpaceDimensionOfBivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfKVector<T>(this IGaListGraded<T> gradeIndexScalarList, uint grade)
        {
            return gradeIndexScalarList
                .GetList(grade)
                .GetMinVSpaceDimensionOfKVector(grade);
        }

        public static uint GetMinVSpaceDimensionOfMultivector<T>(this IGaListGraded<T> gradeIndexScalarList)
        {
            var maxVSpaceDimension = 0U;

            foreach (var (grade, indexScalarList) in gradeIndexScalarList.GetGradeListRecords())
            {
                var vSpaceDimension = 
                    indexScalarList.GetMinVSpaceDimensionOfKVector(grade);

                if (maxVSpaceDimension < vSpaceDimension)
                    maxVSpaceDimension = vSpaceDimension;
            }

            return maxVSpaceDimension;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListEven<T> ToEvenList<T>(this IGaListGraded<T> gradedList)
        {
            return gradedList.ToEvenList(GaBasisBladeUtils.BasisBladeGradeIndexToId);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSingleGradeList<T>(this IGaListGraded<T> gradedList)
        {
            return gradedList.GradesCount == 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaListGradedSingleGrade<T> AsSingleGradeList<T>(this IGaListGraded<T> gradedList)
        {
            if (gradedList is IGaListGradedSingleGrade<T> singleGradeGradedList)
                return singleGradeGradedList;

            if (gradedList.GradesCount != 1)
                return null;

            var grade = gradedList.GetMinGrade();
            var keyValueList = gradedList.GetList(grade);

            return keyValueList.CreateGradedListSingleGrade(grade);
        }
    }
}