using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Utils
{
    public static class GaGridGradedUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<GaRecordKeyPair>> GetKeysSetsUnion<T>(this IGaGridGraded<T> gradedGrid1, IGaGridGraded<T> gradedGrid2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<GaRecordKeyPair>>();
            var gradesSet = gradedGrid1.GetGradesSetUnion(gradedGrid2);

            foreach (var grade in gradesSet)
            {
                var evenGrid1 = gradedGrid1.GetGrid(grade);
                var evenGrid2 = gradedGrid2.GetGrid(grade);

               dictionary.Add(grade, evenGrid1.GetKeysUnion(evenGrid2));
            }

            return dictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<GaRecordKeyPair>> GetKeysSetsIntersection<T>(this IGaGridGraded<T> gradedGrid1, IGaGridGraded<T> gradedGrid2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<GaRecordKeyPair>>();
            var gradesSet = gradedGrid1.GetGradesSetUnion(gradedGrid2);

            foreach (var grade in gradesSet)
            {
                var evenGrid1 = gradedGrid1.GetGrid(grade);
                var evenGrid2 = gradedGrid2.GetGrid(grade);

                dictionary.Add(grade, evenGrid1.GetKeysIntersection(evenGrid2));
            }

            return dictionary;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, IEnumerable<GaRecordKeyPair>> GetKeysSetsDifference<T>(this IGaGridGraded<T> gradedGrid1, IGaGridGraded<T> gradedGrid2)
        {
            var dictionary = new Dictionary<uint, IEnumerable<GaRecordKeyPair>>();
            var gradesSet = gradedGrid1.GetGradesSetUnion(gradedGrid2);

            foreach (var grade in gradesSet)
            {
                var evenGrid1 = gradedGrid1.GetGrid(grade);
                var evenGrid2 = gradedGrid2.GetGrid(grade);

                dictionary.Add(grade, evenGrid1.GetKeysDifference(evenGrid2));
            }

            return dictionary;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyDictionary<uint, HashSet<GaRecordKeyPair>> GetKeysSetsSymmetricDifference<T>(this IGaGridGraded<T> gradedGrid1, IGaGridGraded<T> gradedGrid2)
        {
            var dictionary = new Dictionary<uint, HashSet<GaRecordKeyPair>>();
            var gradesSet = gradedGrid1.GetGradesSetUnion(gradedGrid2);

            foreach (var grade in gradesSet)
            {
                var evenGrid1 = gradedGrid1.GetGrid(grade);
                var evenGrid2 = gradedGrid2.GetGrid(grade);

                dictionary.Add(grade, evenGrid1.GetKeysSymmetricDifference(evenGrid2));
            }

            return dictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this IGaGridGraded<T> gradedGrid, Func<T, T> mappingFunc)
        {
            return gradedGrid
                .GetValues()
                .Select(mappingFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetValues<T>(this IGaGridGraded<T> gradedGrid, Func<uint, ulong, ulong, T, T> mappingFunc)
        {
            return gradedGrid
                .GetGradeKeyValueRecords()
                .Select(keyValue => 
                    mappingFunc(keyValue.Grade, keyValue.Key1, keyValue.Key2, keyValue.Value)
                );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridGraded<T> GetCompactGrid<T>(this IGaGridGraded<T> evenGrid)
        {
            return evenGrid.TryGetCompactGrid(out var compactGrid)
                ? compactGrid
                : evenGrid;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyPairValue<T>> GetEvenKeyValueRecords<T>(this IGaGridGraded<T> gradedGrid)
        {
            return gradedGrid.GetGradeKeyValueRecords().Select(
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
        public static IEnumerable<GaRecordKeyPairValue<T>> GetEvenKeyValueRecords<T>(this IGaGridGraded<T> gradedGrid, Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return gradedGrid.GetGradeKeyValueRecords().Select(
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
        public static IEnumerable<GaRecordKeyPairValue<T>> GetEvenKeyValueRecords<T>(this IGaGridGraded<T> gradedGrid, Func<uint, ulong, ulong, GaRecordKeyPair> gradeKeyToEvenKeyMapping)
        {
            return gradedGrid.GetGradeKeyValueRecords().Select(
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
        public static IEnumerable<GaRecordGradeKeyPairValue<T>> GetGradeKeyValueRecords<T>(this IGaGridGraded<T> gradedGrid, Func<uint, ulong, ulong, GaRecordGradeKeyPair> gradeKeyMapping)
        {
            return gradedGrid.GetGradeKeyValueRecords().Select(
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
        public static IEnumerable<GaRecordKeyPair> GetEvenKeyRecords<T>(this IGaGridGraded<T> gradedGrid)
        {
            return gradedGrid.GetGradeKeyRecords().Select(
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
        public static IEnumerable<GaRecordKeyPair> GetEvenKeyRecords<T>(this IGaGridGraded<T> gradedGrid, Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return gradedGrid.GetGradeKeyRecords().Select(
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
        public static IEnumerable<GaRecordKeyPair> GetEvenKeyRecords<T>(this IGaGridGraded<T> gradedGrid, Func<uint, ulong, ulong, GaRecordKeyPair> gradeKeyToEvenKeyMapping)
        {
            return gradedGrid.GetGradeKeyRecords().Select(
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
        public static IEnumerable<GaRecordGradeKeyPair> GetGradeKeyRecords<T>(this IGaGridGraded<T> gradedGrid, Func<uint, ulong, ulong, GaRecordGradeKeyPair> gradeKeyMapping)
        {
            return gradedGrid.GetGradeKeyRecords().Select(
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
        public static bool ContainsGrade<T>(this IGaGridGraded<T> gradedGrid, int grade)
        {
            return grade >= 0 && gradedGrid.ContainsGrade((uint) grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaGridGraded<T> gradedGrid, uint grade, int key1, int key2)
        {
            return key1 < 0 || key2 < 0
                ? throw new IndexOutOfRangeException()
                : gradedGrid.GetValue(grade, (ulong) key1, (ulong) key2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaGridGraded<T> gradedGrid, uint grade, int key1, int key2, T defaultValue)
        {
            return key1 >= 0 && key2 >= 0 && gradedGrid.TryGetValue(grade, (ulong) key1, (ulong) key2, out var value)
                ? value
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaGridGraded<T> gradedGrid, uint grade, ulong key1, ulong key2, T defaultValue)
        {
            return gradedGrid.TryGetValue(grade, key1, key2, out var value)
                ? value
                : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaGridGraded<T> gradedGrid, uint grade, int key1, int key2, Func<int, int, T> defaultValueFunc)
        {
            return gradedGrid.TryGetValue(grade, (ulong) key1, (ulong) key2, out var value)
                ? value
                : defaultValueFunc(key1, key2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaGridGraded<T> gradedGrid, uint grade, ulong key1, ulong key2, Func<ulong, ulong, T> defaultValueFunc)
        {
            return gradedGrid.TryGetGrid(grade, out var evenGrid) &&
                   evenGrid.TryGetValue(key1, key2, out var value)
                ? value
                : defaultValueFunc(key1, key2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IGaGridGraded<T> gradedGrid, uint grade, ulong key1, ulong key2, Func<uint, ulong, ulong, T> defaultValueFunc)
        {
            return gradedGrid.TryGetGrid(grade, out var evenGrid) &&
                   evenGrid.TryGetValue(key1, key2, out var value)
                ? value
                : defaultValueFunc(grade, key1, key2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this IGaGridGraded<T> gradedGrid, uint grade, int key1, int key2, out T value)
        {
            if (key1 >= 0 && key2 >= 0 && gradedGrid.TryGetGrid(grade, out var evenGrid) && evenGrid.TryGetValue((ulong) key1, (ulong) key2, out value))
                return true;

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this IGaGridGraded<T> gradedGrid, uint grade, ulong key1, ulong key2, out T value)
        {
            if (gradedGrid.TryGetGrid(grade, out var evenGrid) && evenGrid.TryGetValue(key1, key2, out value))
                return true;

            value = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetGrid<T>(this IGaGridGraded<T> gradedGrid, int grade, out IGaGridEven<T> evenGrid)
        {
            if (grade >= 0 && gradedGrid.TryGetGrid((uint) grade, out evenGrid))
                return true;

            evenGrid = null;
            return false;
        }


        /// <summary>
        /// The even list corresponding to the smallest grade stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> GetMinGradeGrid<T>(this IGaGridGraded<T> gradedGrid)
        {
            return gradedGrid.GetGrid(gradedGrid.GetMinGrade());
        }

        /// <summary>
        /// The even list corresponding to the largest grade stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridEven<T> GetMaxGradeGrid<T>(this IGaGridGraded<T> gradedGrid)
        {
            return gradedGrid.GetGrid(gradedGrid.GetMaxGrade());
        }
        
        /// <summary>
        /// The smallest grade and corresponding even list stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordGradeValue<IGaGridEven<T>> GetMinGradeGridRecord<T>(this IGaGridGraded<T> gradedGrid)
        {
            var grade = gradedGrid.GetMinGrade();

            return new GaRecordGradeValue<IGaGridEven<T>>(grade, gradedGrid.GetGrid(grade));
        }
        
        /// <summary>
        /// The largest grade and corresponding even list stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordGradeValue<IGaGridEven<T>> GetMaxGradeGridRecord<T>(this IGaGridGraded<T> gradedGrid)
        {
            var grade = gradedGrid.GetMaxGrade();

            return new GaRecordGradeValue<IGaGridEven<T>>(grade, gradedGrid.GetGrid(grade));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyPairValue<T>> GetKeyValueRecords<T>(this IGaGridGraded<T> gradedGrid)
        {
            return gradedGrid
                .GetGradeKeyValueRecords()
                .Select(record => record.GetKeyValueRecord(GaBasisBladeUtils.BasisBladeGradeIndexToId));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyPairValue<T>> GetKeyValueRecords<T>(this IGaGridGraded<T> gradedGrid, Func<uint, ulong, ulong> gradeKeyToKeyMapping)
        {
            return gradedGrid
                .GetGradeKeyValueRecords()
                .Select(record => record.GetKeyValueRecord(gradeKeyToKeyMapping));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GaRecordKeyPair> GetEmptyKeys<T>(this IGaGridGraded<T> gradedGrid, uint grade, GaRecordKeyPair maxKey)
        {
            return gradedGrid.TryGetGrid(grade, out var evenGrid) 
                ? evenGrid.GetEmptyKeys(maxKey) 
                : maxKey.AddOne().GetKeyPairsInRange();
        }


        /// <summary>
        /// The value corresponding to the smallest key stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMinKeyValue<T>(this IGaGridGraded<T> gradedGrid, uint grade)
        {
            return gradedGrid.GetGrid(grade).GetMinKeyValue();
        }

        /// <summary>
        /// The value corresponding to the largest key stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMaxKeyValue<T>(this IGaGridGraded<T> gradedGrid, uint grade)
        {
            return gradedGrid.GetGrid(grade).GetMaxKeyValue();
        }


        /// <summary>
        /// The smallest key and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyPairValue<T> GetMinKeyValueRecord<T>(this IGaGridGraded<T> gradedGrid, uint grade)
        {
            return gradedGrid.GetGrid(grade).GetMinKeyValueRecord();
        }

        /// <summary>
        /// The largest key and corresponding value stored in this structure
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaRecordKeyPairValue<T> GetMaxKeyValueRecord<T>(this IGaGridGraded<T> gradedGrid, uint grade)
        {
            return gradedGrid.GetGrid(grade).GetMaxKeyValueRecord();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<uint> GetMinVSpaceDimensionOfVector<T>(this IGaGridGraded<T> gradeIndexScalarGrid)
        {
            return gradeIndexScalarGrid
                .GetGrid(1)
                .GetMinVSpaceDimensionOfVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<uint> GetMinVSpaceDimensionOfBivector<T>(this IGaGridGraded<T> gradeIndexScalarGrid)
        {
            return gradeIndexScalarGrid
                .GetGrid(2)
                .GetMinVSpaceDimensionOfBivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<uint> GetMinVSpaceDimensionOfKVector<T>(this IGaGridGraded<T> gradeIndexScalarGrid, uint grade)
        {
            return gradeIndexScalarGrid
                .GetGrid(grade)
                .GetMinVSpaceDimensionOfKVector(grade);
        }

        public static Pair<uint> GetMinVSpaceDimensionOfMultivector<T>(this IGaGridGraded<T> gradeIndexScalarGrid)
        {
            var maxVSpaceDimension1 = 0U;
            var maxVSpaceDimension2 = 0U;

            foreach (var (grade, indexScalarGrid) in gradeIndexScalarGrid.GetGradeGridRecords())
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
        public static IGaGridEven<T> ToEvenGrid<T>(this IGaGridGraded<T> gradedGrid)
        {
            return gradedGrid.ToEvenGrid(GaBasisBladeUtils.BasisBladeGradeIndexToId);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSingleGradeGrid<T>(this IGaGridGraded<T> gradedGrid)
        {
            return gradedGrid.GradesCount == 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaGridGradedSingleGrade<T> AsSingleGradeGrid<T>(this IGaGridGraded<T> gradedGrid)
        {
            if (gradedGrid is IGaGridGradedSingleGrade<T> singleGradeGradedGrid)
                return singleGradeGradedGrid;

            if (gradedGrid.GradesCount != 1)
                return null;

            var grade = gradedGrid.GetMinGrade();
            var keyValueGrid = gradedGrid.GetGrid(grade);

            return keyValueGrid.CreateGradedGridSingleGrade(grade);
        }
    }
}