using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Records;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded
{
    public static class LinMatrixGradedStorageUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<RGaKvIndexPairRecord>> GetIndicesSetsUnion<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage1, ILinMatrixGradedStorage<T> matrixGradedStorage2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<RGaKvIndexPairRecord>>();
            var gradesSet = matrixGradedStorage1.GetGradesSetUnion(matrixGradedStorage2);

            foreach (var grade in gradesSet)
            {
                var matrixStorage1 = matrixGradedStorage1.GetMatrixStorage(grade);
                var matrixStorage2 = matrixGradedStorage2.GetMatrixStorage(grade);

                dictionary.Add(grade, matrixStorage1.GetIndicesUnion(matrixStorage2));
            }

            return dictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<RGaKvIndexPairRecord>> GetIndicesSetsIntersection<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage1, ILinMatrixGradedStorage<T> matrixGradedStorage2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<RGaKvIndexPairRecord>>();
            var gradesSet = matrixGradedStorage1.GetGradesSetUnion(matrixGradedStorage2);

            foreach (var grade in gradesSet)
            {
                var matrixStorage1 = matrixGradedStorage1.GetMatrixStorage(grade);
                var matrixStorage2 = matrixGradedStorage2.GetMatrixStorage(grade);

                dictionary.Add(grade, matrixStorage1.GetIndicesIntersection(matrixStorage2));
            }

            return dictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<RGaKvIndexPairRecord>> GetIndicesSetsDifference<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage1, ILinMatrixGradedStorage<T> matrixGradedStorage2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<RGaKvIndexPairRecord>>();
            var gradesSet = matrixGradedStorage1.GetGradesSetUnion(matrixGradedStorage2);

            foreach (var grade in gradesSet)
            {
                var matrixStorage1 = matrixGradedStorage1.GetMatrixStorage(grade);
                var matrixStorage2 = matrixGradedStorage2.GetMatrixStorage(grade);

                dictionary.Add(grade, matrixStorage1.GetIndicesDifference(matrixStorage2));
            }

            return dictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, HashSet<RGaKvIndexPairRecord>> GetIndicesSetsSymmetricDifference<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage1, ILinMatrixGradedStorage<T> matrixGradedStorage2)
        {
            var dictionary = new Dictionary<uint, HashSet<RGaKvIndexPairRecord>>();
            var gradesSet = matrixGradedStorage1.GetGradesSetUnion(matrixGradedStorage2);

            foreach (var grade in gradesSet)
            {
                var matrixStorage1 = matrixGradedStorage1.GetMatrixStorage(grade);
                var matrixStorage2 = matrixGradedStorage2.GetMatrixStorage(grade);

                dictionary.Add(grade, matrixStorage1.GetIndicesSymmetricDifference(matrixStorage2));
            }

            return dictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalars<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, Func<T, T> mappingFunc)
        {
            return matrixGradedStorage
                .GetScalars()
                .Select(mappingFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetScalars<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, Func<uint, ulong, ulong, T, T> mappingFunc)
        {
            return matrixGradedStorage
                .GetGradeIndexScalarRecords()
                .Select(indexScalar =>
                    mappingFunc(indexScalar.Grade, indexScalar.KvIndex1, indexScalar.KvIndex2, indexScalar.Scalar)
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixGradedStorage<T> GetCompactMatrix<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            return matrixGradedStorage.TryGetCompactStorage(out var compactMatrix)
                ? compactMatrix
                : matrixGradedStorage;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexPairScalarRecord<T>> GetEvenIndexScalarRecords<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            return matrixGradedStorage.GetGradeIndexScalarRecords().Select(
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
        public static IEnumerable<RGaKvIndexPairScalarRecord<T>> GetEvenIndexScalarRecords<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return matrixGradedStorage.GetGradeIndexScalarRecords().Select(
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
        public static IEnumerable<RGaKvIndexPairScalarRecord<T>> GetEvenIndexScalarRecords<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, Func<uint, ulong, ulong, RGaKvIndexPairRecord> gradeIndexToIndexMapping)
        {
            return matrixGradedStorage.GetGradeIndexScalarRecords().Select(
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
        public static IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> GetGradeIndexScalarRecords<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, Func<uint, ulong, ulong, RGaGradeKvIndexPairRecord> gradeIndexMapping)
        {
            return matrixGradedStorage.GetGradeIndexScalarRecords().Select(
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
        public static IEnumerable<RGaKvIndexPairRecord> GetEvenIndexRecords<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            return matrixGradedStorage.GetGradeIndexRecords().Select(
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
        public static IEnumerable<RGaKvIndexPairRecord> GetEvenIndexRecords<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return matrixGradedStorage.GetGradeIndexRecords().Select(
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
        public static IEnumerable<RGaKvIndexPairRecord> GetEvenIndexRecords<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, Func<uint, ulong, ulong, RGaKvIndexPairRecord> gradeIndexToIndexMapping)
        {
            return matrixGradedStorage.GetGradeIndexRecords().Select(
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
        public static IEnumerable<RGaGradeKvIndexPairRecord> GetGradeIndexRecords<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, Func<uint, ulong, ulong, RGaGradeKvIndexPairRecord> gradeIndexMapping)
        {
            return matrixGradedStorage.GetGradeIndexRecords().Select(
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
        public static bool ContainsGrade<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, int grade)
        {
            return grade >= 0 && matrixGradedStorage.ContainsGrade((uint)grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, uint grade, int index1, int index2)
        {
            return index1 < 0 || index2 < 0
                ? throw new IndexOutOfRangeException()
                : matrixGradedStorage.GetScalar(grade, (ulong)index1, (ulong)index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, uint grade, int index1, int index2, T defaultScalar)
        {
            return index1 >= 0 && index2 >= 0 && matrixGradedStorage.TryGetScalar(grade, (ulong)index1, (ulong)index2, out var value)
                ? value
                : defaultScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, uint grade, ulong index1, ulong index2, T defaultScalar)
        {
            return matrixGradedStorage.TryGetScalar(grade, index1, index2, out var value)
                ? value
                : defaultScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, uint grade, int index1, int index2, Func<int, int, T> defaultScalarFunc)
        {
            return matrixGradedStorage.TryGetScalar(grade, (ulong)index1, (ulong)index2, out var value)
                ? value
                : defaultScalarFunc(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, uint grade, ulong index1, ulong index2, Func<ulong, ulong, T> defaultScalarFunc)
        {
            return matrixGradedStorage.TryGetMatrixStorage(grade, out var matrixStorage) &&
                   matrixStorage.TryGetScalar(index1, index2, out var value)
                ? value
                : defaultScalarFunc(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetScalar<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, uint grade, ulong index1, ulong index2, Func<uint, ulong, ulong, T> defaultScalarFunc)
        {
            return matrixGradedStorage.TryGetMatrixStorage(grade, out var matrixStorage) &&
                   matrixStorage.TryGetScalar(index1, index2, out var value)
                ? value
                : defaultScalarFunc(grade, index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetScalar<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, uint grade, int index1, int index2, out T value)
        {
            if (index1 >= 0 && index2 >= 0 && matrixGradedStorage.TryGetMatrixStorage(grade, out var matrixStorage) && matrixStorage.TryGetScalar((ulong)index1, (ulong)index2, out value))
                return true;

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetScalar<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, uint grade, ulong index1, ulong index2, out T value)
        {
            if (matrixGradedStorage.TryGetMatrixStorage(grade, out var matrixStorage) && matrixStorage.TryGetScalar(index1, index2, out value))
                return true;

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetMatrix<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, int grade, out ILinMatrixStorage<T> matrixStorage)
        {
            if (grade >= 0 && matrixGradedStorage.TryGetMatrixStorage((uint)grade, out matrixStorage))
                return true;

            matrixStorage = null;
            return false;
        }


        /// <summary>
        /// The even list corresponding to the smallest grade stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> GetMinGradeStorage<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            return matrixGradedStorage.GetMatrixStorage(matrixGradedStorage.GetMinGrade());
        }

        /// <summary>
        /// The even list corresponding to the largest grade stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> GetMaxGradeStorage<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            return matrixGradedStorage.GetMatrixStorage(matrixGradedStorage.GetMaxGrade());
        }

        /// <summary>
        /// The smallest grade and corresponding even list stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGradeScalarRecord<ILinMatrixStorage<T>> GetMinGradeMatrixRecord<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            var grade = matrixGradedStorage.GetMinGrade();

            return new GaGradeScalarRecord<ILinMatrixStorage<T>>(grade, matrixGradedStorage.GetMatrixStorage(grade));
        }

        /// <summary>
        /// The largest grade and corresponding even list stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaGradeScalarRecord<ILinMatrixStorage<T>> GetMaxGradeMatrixRecord<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            var grade = matrixGradedStorage.GetMaxGrade();

            return new GaGradeScalarRecord<ILinMatrixStorage<T>>(grade, matrixGradedStorage.GetMatrixStorage(grade));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexPairScalarRecord<T>> GetIndexScalarRecords<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            return matrixGradedStorage
                .GetGradeIndexScalarRecords()
                .Select(record => record.GetIndexScalarRecord(BasisBladeUtils.BasisBladeGradeIndexToId));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexPairScalarRecord<T>> GetIndexScalarRecords<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return matrixGradedStorage
                .GetGradeIndexScalarRecords()
                .Select(record => record.GetIndexScalarRecord(gradeIndexToIndexMapping));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaKvIndexPairRecord> GetEmptyIndices<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, uint grade, RGaKvIndexPairRecord maxCountPair)
        {
            return matrixGradedStorage.TryGetMatrixStorage(grade, out var matrixStorage)
                ? matrixStorage.GetEmptyIndices(maxCountPair)
                : maxCountPair.GetIndexPairsInRange();
        }


        /// <summary>
        /// The value corresponding to the smallest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMinIndexScalar<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, uint grade)
        {
            return matrixGradedStorage.GetMatrixStorage(grade).GetMinIndexScalar();
        }

        /// <summary>
        /// The value corresponding to the largest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMaxIndexScalar<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, uint grade)
        {
            return matrixGradedStorage.GetMatrixStorage(grade).GetMaxIndexScalar();
        }


        /// <summary>
        /// The smallest index and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairScalarRecord<T> GetMinIndexScalarRecord<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, uint grade)
        {
            return matrixGradedStorage.GetMatrixStorage(grade).GetMinIndexScalarRecord();
        }

        /// <summary>
        /// The largest index and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKvIndexPairScalarRecord<T> GetMaxIndexScalarRecord<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, uint grade)
        {
            return matrixGradedStorage.GetMatrixStorage(grade).GetMaxIndexScalarRecord();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<uint> GetMinVSpaceDimensionOfVector<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            return matrixGradedStorage
                .GetMatrixStorage(1)
                .GetMinVSpaceDimensionOfVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<uint> GetMinVSpaceDimensionOfBivector<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            return matrixGradedStorage
                .GetMatrixStorage(2)
                .GetMinVSpaceDimensionOfBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<uint> GetMinVSpaceDimensionOfKVector<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, uint grade)
        {
            return matrixGradedStorage
                .GetMatrixStorage(grade)
                .GetMinVSpaceDimensionOfKVector(grade);
        }

        public static Pair<uint> GetMinVSpaceDimensionOfMultivector<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            var maxVSpaceDimension1 = 0U;
            var maxVSpaceDimension2 = 0U;

            foreach (var (grade, indexScalarMatrix) in matrixGradedStorage.GetGradeStorageRecords())
            {
                var (vSpaceDimension1, vSpaceDimension2) =
                    indexScalarMatrix.GetMinVSpaceDimensionOfKVector(grade);

                if (maxVSpaceDimension1 < vSpaceDimension1)
                    maxVSpaceDimension1 = vSpaceDimension1;

                if (maxVSpaceDimension2 < vSpaceDimension2)
                    maxVSpaceDimension2 = vSpaceDimension2;
            }

            return new Pair<uint>(
                maxVSpaceDimension1,
                maxVSpaceDimension2
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<T> ToMatrixStorage<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            return matrixGradedStorage.ToMatrixStorage(BasisBladeUtils.BasisBladeGradeIndexToId);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixSingleGradeStorage<T> AsLinMatrixSingleGradeStorage<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            if (matrixGradedStorage is ILinMatrixSingleGradeStorage<T> singleGradeGradedMatrix)
                return singleGradeGradedMatrix;

            if (matrixGradedStorage.GradesCount != 1)
                return null;

            var grade = matrixGradedStorage.GetMinGrade();
            var indexScalarMatrix = matrixGradedStorage.GetMatrixStorage(grade);

            return indexScalarMatrix.CreateLinMatrixSingleGradeStorage(grade);
        }
    }
}