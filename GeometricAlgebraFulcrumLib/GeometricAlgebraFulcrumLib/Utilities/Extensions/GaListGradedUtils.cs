using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaListGradedUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<ulong>> GetKeysSetsUnion<T>(this ILaVectorGradedStorage<T> gradedList1, ILaVectorGradedStorage<T> gradedList2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<ulong>>();
            var gradesSet = gradedList1.GetGradesSetUnion(gradedList2);

            foreach (var grade in gradesSet)
            {
                var evenList1 = gradedList1.GetEvenStorage(grade);
                var evenList2 = gradedList2.GetEvenStorage(grade);

               dictionary.Add(grade, evenList1.GetKeysUnion(evenList2));
            }

            return dictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<ulong>> GetKeysSetsIntersection<T>(this ILaVectorGradedStorage<T> gradedList1, ILaVectorGradedStorage<T> gradedList2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<ulong>>();
            var gradesSet = gradedList1.GetGradesSetUnion(gradedList2);

            foreach (var grade in gradesSet)
            {
                var evenList1 = gradedList1.GetEvenStorage(grade);
                var evenList2 = gradedList2.GetEvenStorage(grade);

                dictionary.Add(grade, evenList1.GetKeysIntersection(evenList2));
            }

            return dictionary;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<ulong>> GetKeysSetsDifference<T>(this ILaVectorGradedStorage<T> gradedList1, ILaVectorGradedStorage<T> gradedList2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<ulong>>();
            var gradesSet = gradedList1.GetGradesSetUnion(gradedList2);

            foreach (var grade in gradesSet)
            {
                var evenList1 = gradedList1.GetEvenStorage(grade);
                var evenList2 = gradedList2.GetEvenStorage(grade);

                dictionary.Add(grade, evenList1.GetKeysDifference(evenList2));
            }

            return dictionary;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, HashSet<ulong>> GetKeysSetsSymmetricDifference<T>(this ILaVectorGradedStorage<T> gradedList1, ILaVectorGradedStorage<T> gradedList2)
        {
            var dictionary = new Dictionary<uint, HashSet<ulong>>();
            var gradesSet = gradedList1.GetGradesSetUnion(gradedList2);

            foreach (var grade in gradesSet)
            {
                var evenList1 = gradedList1.GetEvenStorage(grade);
                var evenList2 = gradedList2.GetEvenStorage(grade);

                dictionary.Add(grade, evenList1.GetKeysSetSymmetricDifference(evenList2));
            }

            return dictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this ILaVectorGradedStorage<T> gradedList, Func<T, T> mappingFunc)
        {
            return gradedList
                .GetScalars()
                .Select(mappingFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this ILaVectorGradedStorage<T> gradedList, Func<uint, ulong, T, T> mappingFunc)
        {
            return gradedList
                .GetGradeIndexScalarRecords()
                .Select(indexValue => 
                    mappingFunc(indexValue.Grade, indexValue.Index, indexValue.Scalar)
                );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorGradedStorage<T> GetCompactList<T>(this ILaVectorGradedStorage<T> evenList)
        {
            return evenList.TryGetCompactStorage(out var compactList)
                ? compactList
                : evenList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetEvenKeyValueRecords<T>(this ILaVectorGradedStorage<T> gradedList)
        {
            return gradedList.GetGradeIndexScalarRecords().Select(
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
        public static IEnumerable<IndexScalarRecord<T>> GetEvenKeyValueRecords<T>(this ILaVectorGradedStorage<T> gradedList, Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return gradedList.GetGradeIndexScalarRecords().Select(
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
        public static IEnumerable<GradeIndexScalarRecord<T>> GetGradeKeyValueRecords<T>(this ILaVectorGradedStorage<T> gradedList, Func<uint, ulong, GradeIndexRecord> gradeKeyMapping)
        {
            return gradedList.GetGradeIndexScalarRecords().Select(
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
        public static IEnumerable<ulong> GetEvenKeys<T>(this ILaVectorGradedStorage<T> gradedList)
        {
            return gradedList.GetGradeIndexRecords().Select(
                record =>
                {
                    var (grade, index) = record;

                    return index.BasisBladeIndexToId(grade);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetEvenKeys<T>(this ILaVectorGradedStorage<T> gradedList, Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return gradedList.GetGradeIndexRecords().Select(
                record =>
                {
                    var (grade, index) = record;

                    return gradeKeyToEvenKeyMapping(grade, index);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GradeIndexRecord> GetGradeKeyRecords<T>(this ILaVectorGradedStorage<T> gradedList, Func<uint, ulong, GradeIndexRecord> gradeKeyMapping)
        {
            return gradedList.GetGradeIndexRecords().Select(
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
        public static bool ContainsGrade<T>(this ILaVectorGradedStorage<T> gradedList, int grade)
        {
            return grade >= 0 && gradedList.ContainsGrade((uint) grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaVectorGradedStorage<T> gradedList, uint grade, int index)
        {
            return index < 0
                ? throw new IndexOutOfRangeException()
                : gradedList.GetScalar(grade, (ulong) index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaVectorGradedStorage<T> gradedList, uint grade, int index, T defaultValue)
        {
            return index >= 0 && gradedList.TryGetScalar(grade, (ulong) index, out var value)
                ? value
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaVectorGradedStorage<T> gradedList, uint grade, ulong index, T defaultValue)
        {
            return gradedList.TryGetScalar(grade, index, out var value)
                ? value
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaVectorGradedStorage<T> gradedList, uint grade, int index, Func<int, T> defaultValueFunc)
        {
            return gradedList.TryGetScalar(grade, (ulong) index, out var value)
                ? value
                : defaultValueFunc(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaVectorGradedStorage<T> gradedList, uint grade, ulong index, Func<ulong, T> defaultValueFunc)
        {
            return gradedList.TryGetEvenStorage(grade, out var evenList) &&
                   evenList.TryGetScalar(index, out var value)
                ? value
                : defaultValueFunc(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaVectorGradedStorage<T> gradedList, uint grade, ulong index, Func<uint, ulong, T> defaultValueFunc)
        {
            return gradedList.TryGetEvenStorage(grade, out var evenList) &&
                   evenList.TryGetScalar(index, out var value)
                ? value
                : defaultValueFunc(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this ILaVectorGradedStorage<T> gradedList, uint grade, int index, out T value)
        {
            if (index >= 0 && gradedList.TryGetEvenStorage(grade, out var evenList) && evenList.TryGetScalar((ulong) index, out value))
                return true;

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this ILaVectorGradedStorage<T> gradedList, uint grade, ulong index, out T value)
        {
            if (gradedList.TryGetEvenStorage(grade, out var evenList) && evenList.TryGetScalar(index, out value))
                return true;

            value = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetList<T>(this ILaVectorGradedStorage<T> gradedList, int grade, out ILaVectorEvenStorage<T> evenList)
        {
            if (grade >= 0 && gradedList.TryGetEvenStorage((uint) grade, out evenList))
                return true;

            evenList = null;
            return false;
        }


        /// <summary>
        /// The even list corresponding to the smallest grade stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> GetMinGradeList<T>(this ILaVectorGradedStorage<T> gradedList)
        {
            return gradedList.GetEvenStorage(gradedList.GetMinGrade());
        }

        /// <summary>
        /// The even list corresponding to the largest grade stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> GetMaxGradeList<T>(this ILaVectorGradedStorage<T> gradedList)
        {
            return gradedList.GetEvenStorage(gradedList.GetMaxGrade());
        }
        
        /// <summary>
        /// The smallest grade and corresponding even list stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeScalarRecord<ILaVectorEvenStorage<T>> GetMinGradeListRecord<T>(this ILaVectorGradedStorage<T> gradedList)
        {
            var grade = gradedList.GetMinGrade();

            return new GradeScalarRecord<ILaVectorEvenStorage<T>>(grade, gradedList.GetEvenStorage(grade));
        }
        
        /// <summary>
        /// The largest grade and corresponding even list stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeScalarRecord<ILaVectorEvenStorage<T>> GetMaxGradeListRecord<T>(this ILaVectorGradedStorage<T> gradedList)
        {
            var grade = gradedList.GetMaxGrade();

            return new GradeScalarRecord<ILaVectorEvenStorage<T>>(grade, gradedList.GetEvenStorage(grade));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetKeyValueRecords<T>(this ILaVectorGradedStorage<T> gradedList)
        {
            return gradedList
                .GetGradeIndexScalarRecords()
                .Select(record => record.GetKeyValueRecord(GaBasisBladeUtils.BasisBladeGradeIndexToId));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexScalarRecord<T>> GetKeyValueRecords<T>(this ILaVectorGradedStorage<T> gradedList, Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            return gradedList
                .GetGradeIndexScalarRecords()
                .Select(record => record.GetKeyValueRecord(GaBasisBladeUtils.BasisBladeGradeIndexToId));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetEmptyKeys<T>(this ILaVectorGradedStorage<T> gradedList, uint grade, ulong maxKey)
        {
            return gradedList.TryGetEvenStorage(grade, out var evenList) 
                ? evenList.GetEmptyIndices(maxKey) 
                : (maxKey + 1).GetRange();
        }

        
        /// <summary>
        /// The value corresponding to the smallest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMinKey<T>(this ILaVectorGradedStorage<T> gradedList, uint grade)
        {
            return gradedList.GetEvenStorage(grade).GetMinIndex();
        }

        /// <summary>
        /// The value corresponding to the largest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxKey<T>(this ILaVectorGradedStorage<T> gradedList, uint grade)
        {
            return gradedList.GetEvenStorage(grade).GetMaxIndex();
        }


        /// <summary>
        /// The value corresponding to the smallest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMinKeyValue<T>(this ILaVectorGradedStorage<T> gradedList, uint grade)
        {
            return gradedList.GetEvenStorage(grade).GetMinKeyValue();
        }

        /// <summary>
        /// The value corresponding to the largest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMaxKeyValue<T>(this ILaVectorGradedStorage<T> gradedList, uint grade)
        {
            return gradedList.GetEvenStorage(grade).GetMaxKeyValue();
        }


        /// <summary>
        /// The smallest index and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        public static IndexScalarRecord<T> GetMinKeyValueRecord<T>(this ILaVectorGradedStorage<T> gradedList, uint grade)
        {
            return gradedList.GetEvenStorage(grade).GetMinKeyValueRecord();
        }

        /// <summary>
        /// The largest index and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        public static IndexScalarRecord<T> GetMaxKeyValueRecord<T>(this ILaVectorGradedStorage<T> gradedList, uint grade)
        {
            return gradedList.GetEvenStorage(grade).GetMaxKeyValueRecord();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfVector<T>(this ILaVectorGradedStorage<T> gradeIndexScalarList)
        {
            return gradeIndexScalarList
                .GetEvenStorage(1)
                .GetMinVSpaceDimensionOfVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfBivector<T>(this ILaVectorGradedStorage<T> gradeIndexScalarList)
        {
            return gradeIndexScalarList
                .GetEvenStorage(2)
                .GetMinVSpaceDimensionOfBivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfKVector<T>(this ILaVectorGradedStorage<T> gradeIndexScalarList, uint grade)
        {
            return gradeIndexScalarList
                .GetEvenStorage(grade)
                .GetMinVSpaceDimensionOfKVector(grade);
        }

        public static uint GetMinVSpaceDimensionOfMultivector<T>(this ILaVectorGradedStorage<T> gradeIndexScalarList)
        {
            var maxVSpaceDimension = 0U;

            foreach (var (grade, indexScalarList) in gradeIndexScalarList.GetGradeStorageRecords())
            {
                var vSpaceDimension = 
                    indexScalarList.GetMinVSpaceDimensionOfKVector(grade);

                if (maxVSpaceDimension < vSpaceDimension)
                    maxVSpaceDimension = vSpaceDimension;
            }

            return maxVSpaceDimension;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorEvenStorage<T> ToEvenList<T>(this ILaVectorGradedStorage<T> gradedList)
        {
            return gradedList.ToEvenStorage(GaBasisBladeUtils.BasisBladeGradeIndexToId);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSingleGradeList<T>(this ILaVectorGradedStorage<T> gradedList)
        {
            return gradedList.GradesCount == 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaVectorSingleGradeStorage<T> AsSingleGradeList<T>(this ILaVectorGradedStorage<T> gradedList)
        {
            if (gradedList is ILaVectorSingleGradeStorage<T> singleGradeGradedList)
                return singleGradeGradedList;

            if (gradedList.GradesCount != 1)
                return null;

            var grade = gradedList.GetMinGrade();
            var indexValueList = gradedList.GetEvenStorage(grade);

            return indexValueList.CreateLaVectorSingleGradeStorage(grade);
        }
    }
}