using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaGridGradedUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<IndexPairRecord>> GetKeysSetsUnion<T>(this ILaMatrixGradedStorage<T> gradedGrid1, ILaMatrixGradedStorage<T> gradedGrid2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<IndexPairRecord>>();
            var gradesSet = gradedGrid1.GetGradesSetUnion(gradedGrid2);

            foreach (var grade in gradesSet)
            {
                var evenGrid1 = gradedGrid1.GetEvenStorage(grade);
                var evenGrid2 = gradedGrid2.GetEvenStorage(grade);

               dictionary.Add(grade, evenGrid1.GetKeysUnion(evenGrid2));
            }

            return dictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<IndexPairRecord>> GetKeysSetsIntersection<T>(this ILaMatrixGradedStorage<T> gradedGrid1, ILaMatrixGradedStorage<T> gradedGrid2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<IndexPairRecord>>();
            var gradesSet = gradedGrid1.GetGradesSetUnion(gradedGrid2);

            foreach (var grade in gradesSet)
            {
                var evenGrid1 = gradedGrid1.GetEvenStorage(grade);
                var evenGrid2 = gradedGrid2.GetEvenStorage(grade);

                dictionary.Add(grade, evenGrid1.GetKeysIntersection(evenGrid2));
            }

            return dictionary;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<IndexPairRecord>> GetKeysSetsDifference<T>(this ILaMatrixGradedStorage<T> gradedGrid1, ILaMatrixGradedStorage<T> gradedGrid2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<IndexPairRecord>>();
            var gradesSet = gradedGrid1.GetGradesSetUnion(gradedGrid2);

            foreach (var grade in gradesSet)
            {
                var evenGrid1 = gradedGrid1.GetEvenStorage(grade);
                var evenGrid2 = gradedGrid2.GetEvenStorage(grade);

                dictionary.Add(grade, evenGrid1.GetKeysDifference(evenGrid2));
            }

            return dictionary;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, HashSet<IndexPairRecord>> GetKeysSetsSymmetricDifference<T>(this ILaMatrixGradedStorage<T> gradedGrid1, ILaMatrixGradedStorage<T> gradedGrid2)
        {
            var dictionary = new Dictionary<uint, HashSet<IndexPairRecord>>();
            var gradesSet = gradedGrid1.GetGradesSetUnion(gradedGrid2);

            foreach (var grade in gradesSet)
            {
                var evenGrid1 = gradedGrid1.GetEvenStorage(grade);
                var evenGrid2 = gradedGrid2.GetEvenStorage(grade);

                dictionary.Add(grade, evenGrid1.GetKeysSymmetricDifference(evenGrid2));
            }

            return dictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this ILaMatrixGradedStorage<T> gradedGrid, Func<T, T> mappingFunc)
        {
            return gradedGrid
                .GetScalars()
                .Select(mappingFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this ILaMatrixGradedStorage<T> gradedGrid, Func<uint, ulong, ulong, T, T> mappingFunc)
        {
            return gradedGrid
                .GetGradeIndexScalarRecords()
                .Select(indexValue => 
                    mappingFunc(indexValue.Grade, indexValue.Index1, indexValue.Index2, indexValue.Scalar)
                );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixGradedStorage<T> GetCompactGrid<T>(this ILaMatrixGradedStorage<T> evenGrid)
        {
            return evenGrid.TryGetCompactStorage(out var compactGrid)
                ? compactGrid
                : evenGrid;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairScalarRecord<T>> GetEvenKeyValueRecords<T>(this ILaMatrixGradedStorage<T> gradedGrid)
        {
            return gradedGrid.GetGradeIndexScalarRecords().Select(
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
        public static IEnumerable<IndexPairScalarRecord<T>> GetEvenKeyValueRecords<T>(this ILaMatrixGradedStorage<T> gradedGrid, Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return gradedGrid.GetGradeIndexScalarRecords().Select(
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
        public static IEnumerable<IndexPairScalarRecord<T>> GetEvenKeyValueRecords<T>(this ILaMatrixGradedStorage<T> gradedGrid, Func<uint, ulong, ulong, IndexPairRecord> gradeKeyToEvenKeyMapping)
        {
            return gradedGrid.GetGradeIndexScalarRecords().Select(
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
        public static IEnumerable<GradeIndexPairScalarRecord<T>> GetGradeKeyValueRecords<T>(this ILaMatrixGradedStorage<T> gradedGrid, Func<uint, ulong, ulong, GradeIndexPairRecord> gradeKeyMapping)
        {
            return gradedGrid.GetGradeIndexScalarRecords().Select(
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
        public static IEnumerable<IndexPairRecord> GetEvenKeyRecords<T>(this ILaMatrixGradedStorage<T> gradedGrid)
        {
            return gradedGrid.GetGradeIndexRecords().Select(
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
        public static IEnumerable<IndexPairRecord> GetEvenKeyRecords<T>(this ILaMatrixGradedStorage<T> gradedGrid, Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return gradedGrid.GetGradeIndexRecords().Select(
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
        public static IEnumerable<IndexPairRecord> GetEvenKeyRecords<T>(this ILaMatrixGradedStorage<T> gradedGrid, Func<uint, ulong, ulong, IndexPairRecord> gradeKeyToEvenKeyMapping)
        {
            return gradedGrid.GetGradeIndexRecords().Select(
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
        public static IEnumerable<GradeIndexPairRecord> GetGradeKeyRecords<T>(this ILaMatrixGradedStorage<T> gradedGrid, Func<uint, ulong, ulong, GradeIndexPairRecord> gradeKeyMapping)
        {
            return gradedGrid.GetGradeIndexRecords().Select(
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
        public static bool ContainsGrade<T>(this ILaMatrixGradedStorage<T> gradedGrid, int grade)
        {
            return grade >= 0 && gradedGrid.ContainsGrade((uint) grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaMatrixGradedStorage<T> gradedGrid, uint grade, int index1, int index2)
        {
            return index1 < 0 || index2 < 0
                ? throw new IndexOutOfRangeException()
                : gradedGrid.GetScalar(grade, (ulong) index1, (ulong) index2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaMatrixGradedStorage<T> gradedGrid, uint grade, int index1, int index2, T defaultValue)
        {
            return index1 >= 0 && index2 >= 0 && gradedGrid.TryGetScalar(grade, (ulong) index1, (ulong) index2, out var value)
                ? value
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaMatrixGradedStorage<T> gradedGrid, uint grade, ulong index1, ulong index2, T defaultValue)
        {
            return gradedGrid.TryGetScalar(grade, index1, index2, out var value)
                ? value
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaMatrixGradedStorage<T> gradedGrid, uint grade, int index1, int index2, Func<int, int, T> defaultValueFunc)
        {
            return gradedGrid.TryGetScalar(grade, (ulong) index1, (ulong) index2, out var value)
                ? value
                : defaultValueFunc(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaMatrixGradedStorage<T> gradedGrid, uint grade, ulong index1, ulong index2, Func<ulong, ulong, T> defaultValueFunc)
        {
            return gradedGrid.TryGetStorage(grade, out var evenGrid) &&
                   evenGrid.TryGetScalar(index1, index2, out var value)
                ? value
                : defaultValueFunc(index1, index2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this ILaMatrixGradedStorage<T> gradedGrid, uint grade, ulong index1, ulong index2, Func<uint, ulong, ulong, T> defaultValueFunc)
        {
            return gradedGrid.TryGetStorage(grade, out var evenGrid) &&
                   evenGrid.TryGetScalar(index1, index2, out var value)
                ? value
                : defaultValueFunc(grade, index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this ILaMatrixGradedStorage<T> gradedGrid, uint grade, int index1, int index2, out T value)
        {
            if (index1 >= 0 && index2 >= 0 && gradedGrid.TryGetStorage(grade, out var evenGrid) && evenGrid.TryGetScalar((ulong) index1, (ulong) index2, out value))
                return true;

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this ILaMatrixGradedStorage<T> gradedGrid, uint grade, ulong index1, ulong index2, out T value)
        {
            if (gradedGrid.TryGetStorage(grade, out var evenGrid) && evenGrid.TryGetScalar(index1, index2, out value))
                return true;

            value = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetGrid<T>(this ILaMatrixGradedStorage<T> gradedGrid, int grade, out ILaMatrixEvenStorage<T> evenGrid)
        {
            if (grade >= 0 && gradedGrid.TryGetStorage((uint) grade, out evenGrid))
                return true;

            evenGrid = null;
            return false;
        }


        /// <summary>
        /// The even list corresponding to the smallest grade stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> GetMinGradeGrid<T>(this ILaMatrixGradedStorage<T> gradedGrid)
        {
            return gradedGrid.GetEvenStorage(gradedGrid.GetMinGrade());
        }

        /// <summary>
        /// The even list corresponding to the largest grade stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixEvenStorage<T> GetMaxGradeGrid<T>(this ILaMatrixGradedStorage<T> gradedGrid)
        {
            return gradedGrid.GetEvenStorage(gradedGrid.GetMaxGrade());
        }
        
        /// <summary>
        /// The smallest grade and corresponding even list stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeScalarRecord<ILaMatrixEvenStorage<T>> GetMinGradeGridRecord<T>(this ILaMatrixGradedStorage<T> gradedGrid)
        {
            var grade = gradedGrid.GetMinGrade();

            return new GradeScalarRecord<ILaMatrixEvenStorage<T>>(grade, gradedGrid.GetEvenStorage(grade));
        }
        
        /// <summary>
        /// The largest grade and corresponding even list stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeScalarRecord<ILaMatrixEvenStorage<T>> GetMaxGradeGridRecord<T>(this ILaMatrixGradedStorage<T> gradedGrid)
        {
            var grade = gradedGrid.GetMaxGrade();

            return new GradeScalarRecord<ILaMatrixEvenStorage<T>>(grade, gradedGrid.GetEvenStorage(grade));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairScalarRecord<T>> GetKeyValueRecords<T>(this ILaMatrixGradedStorage<T> gradedGrid)
        {
            return gradedGrid
                .GetGradeIndexScalarRecords()
                .Select(record => record.GetKeyValueRecord(GaBasisBladeUtils.BasisBladeGradeIndexToId));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairScalarRecord<T>> GetKeyValueRecords<T>(this ILaMatrixGradedStorage<T> gradedGrid, Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            return gradedGrid
                .GetGradeIndexScalarRecords()
                .Select(record => record.GetKeyValueRecord(gradeKeyToKeyMapping));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IndexPairRecord> GetEmptyKeys<T>(this ILaMatrixGradedStorage<T> gradedGrid, uint grade, IndexPairRecord maxKey)
        {
            return gradedGrid.TryGetStorage(grade, out var evenGrid) 
                ? evenGrid.GetEmptyIndices(maxKey) 
                : maxKey.AddOne().GetKeyPairsInRange();
        }


        /// <summary>
        /// The value corresponding to the smallest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMinKeyValue<T>(this ILaMatrixGradedStorage<T> gradedGrid, uint grade)
        {
            return gradedGrid.GetEvenStorage(grade).GetMinKeyValue();
        }

        /// <summary>
        /// The value corresponding to the largest index stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMaxKeyValue<T>(this ILaMatrixGradedStorage<T> gradedGrid, uint grade)
        {
            return gradedGrid.GetEvenStorage(grade).GetMaxKeyValue();
        }


        /// <summary>
        /// The smallest index and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> GetMinKeyValueRecord<T>(this ILaMatrixGradedStorage<T> gradedGrid, uint grade)
        {
            return gradedGrid.GetEvenStorage(grade).GetMinKeyValueRecord();
        }

        /// <summary>
        /// The largest index and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexPairScalarRecord<T> GetMaxKeyValueRecord<T>(this ILaMatrixGradedStorage<T> gradedGrid, uint grade)
        {
            return gradedGrid.GetEvenStorage(grade).GetMaxKeyValueRecord();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<uint> GetMinVSpaceDimensionOfVector<T>(this ILaMatrixGradedStorage<T> gradeIndexScalarGrid)
        {
            return gradeIndexScalarGrid
                .GetEvenStorage(1)
                .GetMinVSpaceDimensionOfVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<uint> GetMinVSpaceDimensionOfBivector<T>(this ILaMatrixGradedStorage<T> gradeIndexScalarGrid)
        {
            return gradeIndexScalarGrid
                .GetEvenStorage(2)
                .GetMinVSpaceDimensionOfBivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<uint> GetMinVSpaceDimensionOfKVector<T>(this ILaMatrixGradedStorage<T> gradeIndexScalarGrid, uint grade)
        {
            return gradeIndexScalarGrid
                .GetEvenStorage(grade)
                .GetMinVSpaceDimensionOfKVector(grade);
        }

        public static Pair<uint> GetMinVSpaceDimensionOfMultivector<T>(this ILaMatrixGradedStorage<T> gradeIndexScalarGrid)
        {
            var maxVSpaceDimension1 = 0U;
            var maxVSpaceDimension2 = 0U;

            foreach (var (grade, indexScalarGrid) in gradeIndexScalarGrid.GetGradeStorageRecords())
            {
                var (vSpaceDimension1, vSpaceDimension2) = 
                    indexScalarGrid.GetMinVSpaceDimensionOfKVector(grade);

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
        public static ILaMatrixEvenStorage<T> ToEvenGrid<T>(this ILaMatrixGradedStorage<T> gradedGrid)
        {
            return gradedGrid.ToEvenStorage(GaBasisBladeUtils.BasisBladeGradeIndexToId);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSingleGradeGrid<T>(this ILaMatrixGradedStorage<T> gradedGrid)
        {
            return gradedGrid.GradesCount == 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILaMatrixSingleGradeStorage<T> AsSingleGradeGrid<T>(this ILaMatrixGradedStorage<T> gradedGrid)
        {
            if (gradedGrid is ILaMatrixSingleGradeStorage<T> singleGradeGradedGrid)
                return singleGradeGradedGrid;

            if (gradedGrid.GradesCount != 1)
                return null;

            var grade = gradedGrid.GetMinGrade();
            var indexValueGrid = gradedGrid.GetEvenStorage(grade);

            return indexValueGrid.CreateLaMatrixSingleGradeStorage(grade);
        }
    }
}