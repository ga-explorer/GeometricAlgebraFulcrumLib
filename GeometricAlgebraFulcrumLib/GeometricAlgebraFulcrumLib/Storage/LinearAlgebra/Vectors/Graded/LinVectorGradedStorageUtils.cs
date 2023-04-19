using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded
{
    public static class LinVectorGradedStorageUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<ulong>> GetIndicesSetsUnion<T>(this ILinVectorGradedStorage<T> vectorGradedStorage1, ILinVectorGradedStorage<T> vectorGradedStorage2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<ulong>>();
            var gradesSet = vectorGradedStorage1.GetGradesSetUnion(vectorGradedStorage2);

            foreach (var grade in gradesSet)
            {
                var evenList1 = vectorGradedStorage1.GetVectorStorage(grade);
                var evenList2 = vectorGradedStorage2.GetVectorStorage(grade);

                dictionary.Add(grade, evenList1.GetIndicesUnion(evenList2));
            }

            return dictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<ulong>> GetIndicesSetsIntersection<T>(this ILinVectorGradedStorage<T> vectorGradedStorage1, ILinVectorGradedStorage<T> vectorGradedStorage2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<ulong>>();
            var gradesSet = vectorGradedStorage1.GetGradesSetUnion(vectorGradedStorage2);

            foreach (var grade in gradesSet)
            {
                var evenList1 = vectorGradedStorage1.GetVectorStorage(grade);
                var evenList2 = vectorGradedStorage2.GetVectorStorage(grade);

                dictionary.Add(grade, evenList1.GetIndicesIntersection(evenList2));
            }

            return dictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<ulong>> GetIndicesSetsDifference<T>(this ILinVectorGradedStorage<T> vectorGradedStorage1, ILinVectorGradedStorage<T> vectorGradedStorage2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<ulong>>();
            var gradesSet = vectorGradedStorage1.GetGradesSetUnion(vectorGradedStorage2);

            foreach (var grade in gradesSet)
            {
                var evenList1 = vectorGradedStorage1.GetVectorStorage(grade);
                var evenList2 = vectorGradedStorage2.GetVectorStorage(grade);

                dictionary.Add(grade, evenList1.GetIndicesDifference(evenList2));
            }

            return dictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, HashSet<ulong>> GetIndicesSetsSymmetricDifference<T>(this ILinVectorGradedStorage<T> vectorGradedStorage1, ILinVectorGradedStorage<T> vectorGradedStorage2)
        {
            var dictionary = new Dictionary<uint, HashSet<ulong>>();
            var gradesSet = vectorGradedStorage1.GetGradesSetUnion(vectorGradedStorage2);

            foreach (var grade in gradesSet)
            {
                var evenList1 = vectorGradedStorage1.GetVectorStorage(grade);
                var evenList2 = vectorGradedStorage2.GetVectorStorage(grade);

                dictionary.Add(grade, evenList1.GetIndicesSetSymmetricDifference(evenList2));
            }

            return dictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalars<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, Func<T, T> mappingFunc)
        {
            return vectorGradedStorage
                .GetScalars()
                .Select(mappingFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalars<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, Func<uint, ulong, T, T> mappingFunc)
        {
            return vectorGradedStorage
                .GetGradeIndexScalarRecords()
                .Select(indexValue =>
                    mappingFunc(indexValue.Grade, indexValue.KvIndex, indexValue.Scalar)
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorGradedStorage<T> GetCompactStorage<T>(this ILinVectorGradedStorage<T> vectorStorage)
        {
            return vectorStorage.TryGetCompactStorage(out var compactList)
                ? compactList
                : vectorStorage;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexScalarRecord<T>> GetIndexScalarRecords<T>(this ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            return vectorGradedStorage
                .GetGradeIndexScalarRecords()
                .Select(record => record.GetIndexScalarRecord(BasisBladeUtils.BasisBladeGradeIndexToId));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexScalarRecord<T>> GetIndexScalarRecords<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return vectorGradedStorage
                .GetGradeIndexScalarRecords()
                .Select(record => record.GetIndexScalarRecord(BasisBladeUtils.BasisBladeGradeIndexToId));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaGradeKvIndexScalarRecord<T>> GetGradeIndexScalarRecords<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, Func<uint, ulong, RGaGradeKvIndexRecord> gradeIndexMapping)
        {
            return vectorGradedStorage.GetGradeIndexScalarRecords().Select(
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
        public static IEnumerable<ulong> GetIndices<T>(this ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            return vectorGradedStorage.GetGradeIndexRecords().Select(
                record =>
                {
                    var (grade, index) = record;

                    return index.BasisBladeIndexToId(grade);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetIndices<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return vectorGradedStorage.GetGradeIndexRecords().Select(
                record =>
                {
                    var (grade, index) = record;

                    return gradeIndexToIndexMapping(grade, index);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaGradeKvIndexRecord> GetGradeIndexRecords<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, Func<uint, ulong, RGaGradeKvIndexRecord> gradeIndexMapping)
        {
            return vectorGradedStorage.GetGradeIndexRecords().Select(
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
        public static bool ContainsGrade<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, int grade)
        {
            return grade >= 0 && vectorGradedStorage.ContainsGrade((uint)grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, uint grade, int index)
        {
            return index < 0
                ? throw new IndexOutOfRangeException()
                : vectorGradedStorage.GetScalar(grade, (ulong)index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, uint grade, int index, T defaultValue)
        {
            return index >= 0 && vectorGradedStorage.TryGetScalar(grade, (ulong)index, out var value)
                ? value
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, uint grade, ulong index, T defaultValue)
        {
            return vectorGradedStorage.TryGetScalar(grade, index, out var value)
                ? value
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, uint grade, int index, Func<int, T> defaultValueFunc)
        {
            return vectorGradedStorage.TryGetScalar(grade, (ulong)index, out var value)
                ? value
                : defaultValueFunc(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, uint grade, ulong index, Func<ulong, T> defaultValueFunc)
        {
            return vectorGradedStorage.TryGetVectorStorage(grade, out var vectorStorage) &&
                   vectorStorage.TryGetScalar(index, out var value)
                ? value
                : defaultValueFunc(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, uint grade, ulong index, Func<uint, ulong, T> defaultValueFunc)
        {
            return vectorGradedStorage.TryGetVectorStorage(grade, out var vectorStorage) &&
                   vectorStorage.TryGetScalar(index, out var value)
                ? value
                : defaultValueFunc(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetScalar<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, uint grade, int index, out T value)
        {
            if (index >= 0 && vectorGradedStorage.TryGetVectorStorage(grade, out var vectorStorage) && vectorStorage.TryGetScalar((ulong)index, out value))
                return true;

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetScalar<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, uint grade, ulong index, out T value)
        {
            if (vectorGradedStorage.TryGetVectorStorage(grade, out var vectorStorage) && vectorStorage.TryGetScalar(index, out value))
                return true;

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetVectorStorage<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, int grade, out ILinVectorStorage<T> vectorStorage)
        {
            if (grade >= 0 && vectorGradedStorage.TryGetVectorStorage((uint)grade, out vectorStorage))
                return true;

            vectorStorage = null;
            return false;
        }


        /// <summary>
        /// The even list corresponding to the smallest grade stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> GetMinGradeStorage<T>(this ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            return vectorGradedStorage.GetVectorStorage(vectorGradedStorage.GetMinGrade());
        }

        /// <summary>
        /// The even list corresponding to the largest grade stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> GetMaxGradeStorage<T>(this ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            return vectorGradedStorage.GetVectorStorage(vectorGradedStorage.GetMaxGrade());
        }

        /// <summary>
        /// The smallest grade and corresponding even list stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGradeLinVectorStorageRecord<T> GetMinGradeStorageRecord<T>(this ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            var grade = vectorGradedStorage.GetMinGrade();

            return new GaGradeLinVectorStorageRecord<T>(grade, vectorGradedStorage.GetVectorStorage(grade));
        }

        /// <summary>
        /// The largest grade and corresponding even list stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGradeLinVectorStorageRecord<T> GetMaxGradeStorageRecord<T>(this ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            var grade = vectorGradedStorage.GetMaxGrade();

            return new GaGradeLinVectorStorageRecord<T>(grade, vectorGradedStorage.GetVectorStorage(grade));
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetEmptyIndices<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, uint grade, ulong maxCount)
        {
            return vectorGradedStorage.TryGetVectorStorage(grade, out var vectorStorage)
                ? vectorStorage.GetEmptyIndices(maxCount)
                : maxCount.GetRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaGradeKvIndexRecord> GetEmptyGradeIndexRecords<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, uint vSpaceDimensions)
        {
            for (var grade = 0U; grade <= vSpaceDimensions; grade++)
            {
                var kvSpaceDimensions = vSpaceDimensions.KVectorSpaceDimension(grade);
                var emptyIndices = vectorGradedStorage.GetEmptyIndices(grade, kvSpaceDimensions);

                foreach (var index in emptyIndices)
                    yield return new RGaGradeKvIndexRecord(grade, index);
            }
        }


        /// <summary>
        /// The value corresponding to the smallest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMinIndex<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, uint grade)
        {
            return vectorGradedStorage.GetVectorStorage(grade).GetMinIndex();
        }

        /// <summary>
        /// The value corresponding to the largest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxIndex<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, uint grade)
        {
            return vectorGradedStorage.GetVectorStorage(grade).GetMaxIndex();
        }


        /// <summary>
        /// The value corresponding to the smallest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMinIndexScalar<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, uint grade)
        {
            return vectorGradedStorage.GetVectorStorage(grade).GetMinIndexScalar();
        }

        /// <summary>
        /// The value corresponding to the largest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMaxIndexScalar<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, uint grade)
        {
            return vectorGradedStorage.GetVectorStorage(grade).GetMaxIndexScalar();
        }


        /// <summary>
        /// The smallest index and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        public static RGaKvIndexScalarRecord<T> GetMinIndexScalarRecord<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, uint grade)
        {
            return vectorGradedStorage.GetVectorStorage(grade).GetMinIndexScalarRecord();
        }

        /// <summary>
        /// The largest index and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        public static RGaKvIndexScalarRecord<T> GetMaxIndexScalarRecord<T>(this ILinVectorGradedStorage<T> vectorGradedStorage, uint grade)
        {
            return vectorGradedStorage.GetVectorStorage(grade).GetMaxIndexScalarRecord();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfVector<T>(this ILinVectorGradedStorage<T> gradeIndexScalarList)
        {
            return gradeIndexScalarList
                .GetVectorStorage(1)
                .GetMinVSpaceDimensionOfVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfBivector<T>(this ILinVectorGradedStorage<T> gradeIndexScalarList)
        {
            return gradeIndexScalarList
                .GetVectorStorage(2)
                .GetMinVSpaceDimensionOfBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimensionOfKVector<T>(this ILinVectorGradedStorage<T> gradeIndexScalarList, uint grade)
        {
            return gradeIndexScalarList
                .GetVectorStorage(grade)
                .GetMinVSpaceDimensionOfKVector(grade);
        }

        public static uint GetMinVSpaceDimensionOfMultivector<T>(this ILinVectorGradedStorage<T> gradeIndexScalarList)
        {
            var maxVSpaceDimension = 0U;

            foreach (var (grade, indexScalarList) in gradeIndexScalarList.GetGradeStorageRecords())
            {
                var vSpaceDimensions =
                    indexScalarList.GetMinVSpaceDimensionOfKVector(grade);

                if (maxVSpaceDimension < vSpaceDimensions)
                    maxVSpaceDimension = vSpaceDimensions;
            }

            return maxVSpaceDimension;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<T> ToLinVectorStorage<T>(this ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            return vectorGradedStorage.ToVectorStorage(BasisBladeUtils.BasisBladeGradeIndexToId);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSingleGradeStorage<T>(this ILinArrayGradedStorage<T> vectorGradedStorage)
        {
            return vectorGradedStorage.GradesCount == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorSingleGradeStorage<T> AsLinVectorSingleGradeStorage<T>(this ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            if (vectorGradedStorage is ILinVectorSingleGradeStorage<T> singleGradeGradedList)
                return singleGradeGradedList;

            if (vectorGradedStorage.GradesCount != 1)
                return null;

            var grade = vectorGradedStorage.GetMinGrade();
            var indexValueList = vectorGradedStorage.GetVectorStorage(grade);

            return indexValueList.CreateLinVectorSingleGradeStorage(grade);
        }
    }
}