using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis
{
    public static class GaBasisUtils
    {
        /// <summary>
        /// The number of basis blades in a GA with dimension vSpaceDimension
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ToGaSpaceDimension(this uint vSpaceDimension)
        {
            return 1ul << (int) vSpaceDimension;
        }

        /// <summary>
        /// The number of basis vectors in a GA with dimension gaSpaceDim
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToVSpaceDimension(this int gaSpaceDim)
        {
            return gaSpaceDim.LastOneBitPosition();
        }

        /// <summary>
        /// The number of basis vectors in a GA with dimension gaSpaceDim
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToVSpaceDimension(this ulong gaSpaceDim)
        {
            return gaSpaceDim.LastOneBitPosition();
        }
        
        /// <summary>
        /// The number of grades in a GA space with a given dimension
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GradesCount(uint vSpaceDimension)
        {
            return vSpaceDimension + 1;
        }

        /// <summary>
        /// The dimension of k-vectors subspace of some grade of a GA with a given dimension
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong KvSpaceDimension(uint vSpaceDimension, uint grade)
        {
            return vSpaceDimension.GetBinomialCoefficient(grade);
        }

        /// <summary>
        /// The grades of k-vectors in a GA with the given dimension
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<uint> Grades(uint vSpaceDimension)
        {
            return (vSpaceDimension + 1).GetRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBivectorId(int index1, int index2)
        {
            Debug.Assert(index1 < index2);

            return (1UL << index1) | (1UL << index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBivectorId(ulong index1, ulong index2)
        {
            Debug.Assert(index1 < index2);

            return (1UL << (int) index1) | (1UL << (int) index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBivectorIndex(int index1, int index2)
        {
            Debug.Assert(index1 < index2);

            return (ulong) (index1 + ((index2 * (index2 - 1)) >> 1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBivectorIndex(ulong index1, ulong index2)
        {
            Debug.Assert(index1 < index2);

            return index1 + ((index2 * (index2 - 1UL)) >> 1);
        }

        /// <summary>
        /// The Basis blade IDs of a GA space with the given dimension
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <returns></returns>
        public static IEnumerable<ulong> BasisBladeIDs(uint vSpaceDimension)
        {
            var maxBasisBladeId = GetMaxBasisBladeId(vSpaceDimension);

            for (var id = 0ul; id <= maxBasisBladeId; id++)
                yield return id;
        }
        
        /// <summary>
        /// The basis vector IDs of a GA with the given dimension
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisVectorIDs(uint vSpaceDimension)
        {
            return vSpaceDimension.GetRange().Select(i => (1ul << (int) i));
        }

        /// <summary>
        /// Find all basis blade IDs with the given grade in a GA of dimension vSpaceDimension
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsOfGrade(uint vSpaceDimension, uint grade)
        {
            return UInt64BitUtils.OnesPermutations(
                (int) vSpaceDimension, 
                (int) grade
            );
        }

        /// <summary>
        /// Find all basis blade IDs with the given grade and indexes
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexSeq"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsOfGradeIndex(uint grade, IEnumerable<ulong> indexSeq)
        {
            return indexSeq.Select(index => BasisBladeId(grade, index));
        }
        
        /// <summary>
        /// Find all basis blade IDs with the given grade and indexes
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexSeq"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsOfGradeIndex(uint grade, params ulong[] indexSeq)
        {
            return indexSeq.Select(index => BasisBladeId(grade, index));
        }
        
        /// <summary>
        /// The basis blade IDs of a GA space with the given dimension sorted by their grade and index
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <param name="startGrade"></param>
        /// <returns></returns>
        public static IEnumerable<ulong> BasisBladeIDsSortedByGrade(uint vSpaceDimension, uint startGrade = 0)
        {
            for (var grade = startGrade; grade <= vSpaceDimension; grade++)
                foreach (var id in BasisBladeIDsOfGrade(vSpaceDimension, grade))
                    yield return id;
        }
        
        /// <summary>
        /// Returns the basis blade IDs of having the given grades
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsOfGrades(uint vSpaceDimension, IEnumerable<uint> gradesSeq)
        {
            return gradesSeq
                .OrderBy(g => g)
                .SelectMany(grade => BasisBladeIDsOfGrade(vSpaceDimension, grade));
        }
        
        /// <summary>
        /// Returns the basis blade IDs of having the given grades
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsOfGrades(uint vSpaceDimension, params uint[] gradesSeq)
        {
            return gradesSeq
                .OrderBy(g => g)
                .SelectMany(grade => BasisBladeIDsOfGrade(vSpaceDimension, grade));
        }
        
        /// <summary>
        /// Returns the basis blade IDs of having the given grades grouped by their grade
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <param name="startGrade"></param>
        /// <returns></returns>
        public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(uint vSpaceDimension, uint startGrade = 0)
        {
            var result = new Dictionary<uint, IReadOnlyList<ulong>>();

            for (var grade = startGrade; grade <= vSpaceDimension; grade++)
            {
                result.Add(
                    grade, 
                    BasisBladeIDsOfGrade(vSpaceDimension, grade).ToArray()
                );
            }

            return result;
        }
        
        /// <summary>
        /// Returns the basis blade IDs of having the given grades grouped by their grade
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(uint vSpaceDimension, IEnumerable<uint> gradesSeq)
        {
            var result = new Dictionary<uint, IReadOnlyList<ulong>>();

            foreach (var grade in gradesSeq)
            {
                result.Add(
                    grade, 
                    BasisBladeIDsOfGrade(vSpaceDimension, grade).ToArray()
                );
            }

            return result;
        }
        
        /// <summary>
        /// Returns the basis blade IDs of having the given grades grouped by their grade
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(uint vSpaceDimension, params uint[] gradesSeq)
        {
            var result = new Dictionary<uint, IReadOnlyList<ulong>>();

            foreach (var grade in gradesSeq)
            {
                result.Add(
                    grade, 
                    BasisBladeIDsOfGrade(vSpaceDimension, grade).ToArray()
                );
            }

            return result;
        }
        
        /// <summary>
        /// Find the ID of a basis blade given its grade and index
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBladeId(uint grade, ulong index)
        {
            if (grade < GaLookupTables.GradeIndexToIdTable.Count)
            {
                var table = GaLookupTables.GradeIndexToIdTable[(int) grade];

                if (index < (ulong) table.Length)
                    return table[index];
            }

            return index.IndexToCombinadicPattern((int) grade);
        }

        /// <summary>
        /// Get the largest basis vector index of the given basis blade
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBladeMaxVectorIndex(this IGaBasisBlade basisBlade)
        {
            var (grade, index) = basisBlade.GetGradeIndex();

            if (grade >= GaLookupTables.GradeIndexToMaxBasisVectorIndexTable.Count)
                return (ulong) CombinationsUtilsUInt64.GetMaximalRowIndex(index, (int) grade);

            var table = 
                GaLookupTables.GradeIndexToMaxBasisVectorIndexTable[(int) grade];

            return index < (ulong) table.Length 
                ? table[(int) index] 
                : (ulong) CombinationsUtilsUInt64.GetMaximalRowIndex(index, (int) grade);
        }

        /// <summary>
        /// Get the largest basis vector index of the given basis blade
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBladeMaxVectorIndex(uint grade, ulong index)
        {
            if (grade >= GaLookupTables.GradeIndexToMaxBasisVectorIndexTable.Count)
                return (ulong) CombinationsUtilsUInt64.GetMaximalRowIndex(index, (int) grade);

            var table = 
                GaLookupTables.GradeIndexToMaxBasisVectorIndexTable[(int) grade];

            return index < (ulong) table.Length 
                ? table[index] 
                : (ulong) CombinationsUtilsUInt64.GetMaximalRowIndex(index, (int) grade);
        }
        
        /// <summary>
        /// Find the ID of a basis vector given its index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisVectorId(ulong index)
        {
            return 1UL << (int) index;
        }
        
        /// <summary>
        /// Find the grade of a basis blade given its ID
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint BasisBladeGrade(this ulong basisBladeId)
        {
            return basisBladeId < (ulong)GaLookupTables.IdToGradeTable.Length
                ? GaLookupTables.IdToGradeTable[basisBladeId]
                : (uint) basisBladeId.CountOnes();
        }
        
        /// <summary>
        /// Find the index of a basis blade given its ID
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBladeIndex(this ulong basisBladeId)
        {
            return basisBladeId < (ulong)GaLookupTables.IdToIndexTable.Length
                ? GaLookupTables.IdToIndexTable[basisBladeId]
                : basisBladeId.CombinadicPatternToIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisVectorIndex(this ulong basisVectorId)
        {
            return basisVectorId < (ulong)GaLookupTables.IdToIndexTable.Length
                ? GaLookupTables.IdToIndexTable[basisVectorId]
                : (ulong) basisVectorId.FirstOneBitPosition();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong PseudoScalarId(this uint vSpaceDimension)
        {
            return ((1UL << (int) vSpaceDimension) - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong PseudoScalarIndex(this uint vSpaceDimension)
        {
            return ((1UL << (int) vSpaceDimension) - 1).BasisBladeIndex();
        }

        /// <summary>
        /// Find the grade and index of a basis blade given its ID
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BasisBladeGradeIndex(this ulong basisBladeId, out uint grade, out ulong index)
        {
            if (basisBladeId < (ulong) GaLookupTables.IdToIndexTable.Length)
            {
                grade = GaLookupTables.IdToGradeTable[basisBladeId];
                index = GaLookupTables.IdToIndexTable[basisBladeId];

                return;
            }

            basisBladeId.CombinadicPatternToIndex(out var intGrade, out index);

            grade = (uint) intGrade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<uint, ulong> BasisBladeGradeIndex(this ulong basisBladeId)
        {
            if (basisBladeId < (ulong) GaLookupTables.IdToIndexTable.Length)
            {
                var grade = GaLookupTables.IdToGradeTable[basisBladeId];
                var index = GaLookupTables.IdToIndexTable[basisBladeId];

                return new Tuple<uint, ulong>(grade, index);
            }
            else
            {
                basisBladeId.CombinadicPatternToIndex(out var grade, out var index);

                return new Tuple<uint, ulong>((uint) grade, index);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeName(this ulong basisBladeId)
        {
            return GaSpaceUtils.DefaultBasisVectorsNames.ConcatenateUsingPattern(basisBladeId, "E0", "^");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeName(uint grade, ulong index)
        {
            return GaSpaceUtils.DefaultBasisVectorsNames.ConcatenateUsingPattern(BasisBladeId(grade, index), "E0", "^");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeName(this ulong basisBladeId, params string[] basisVectorNames)
        {
            return basisVectorNames.ConcatenateUsingPattern(basisBladeId, "E0", "^");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeName(uint grade, ulong index, params string[] basisVectorNames)
        {
            return basisVectorNames.ConcatenateUsingPattern(BasisBladeId(grade, index), "E0", "^");
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeIndexedName(this ulong basisBladeId)
        {
            return "E" + basisBladeId;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeIndexedName(uint grade, ulong index)
        {
            return "E" + BasisBladeId(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeBinaryIndexedName(uint vSpaceDimension, ulong basisBladeId)
        {
            return "B" + basisBladeId.PatternToString((int) vSpaceDimension);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeBinaryIndexedName(uint vSpaceDimension, uint grade, ulong index)
        {
            return "B" + BasisBladeId(grade, index).PatternToString((int) vSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeGradeIndexName(this ulong basisBladeId)
        {
            return
                new StringBuilder(32)
                .Append('G')
                .Append(basisBladeId.BasisBladeGrade())
                .Append('I')
                .Append(basisBladeId.BasisBladeIndex())
                .ToString();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeGradeIndexName(uint grade, ulong index)
        {
            return
                new StringBuilder(32)
                .Append('G')
                .Append(grade)
                .Append('I')
                .Append(index)
                .ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisVectorIDsInside(this ulong basisBladeId)
        {
            return basisBladeId.GetBasicPatterns();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisVectorIDsInside(uint grade, ulong index)
        {
            return BasisBladeId(grade, index).GetBasicPatterns();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisVectorIndexesInside(this ulong basisBladeId)
        {
            return basisBladeId.PatternToPositions().Select(i => (ulong)i);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisVectorIndexesInside(uint grade, ulong index)
        {
            return BasisBladeId(grade, index).PatternToPositions().Select(i => (ulong)i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsInside(this ulong basisBladeId)
        {
            return basisBladeId.GetSubPatterns();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsInside(uint grade, ulong index)
        {
            return BasisBladeId(grade, index).GetSubPatterns();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsContaining(uint vSpaceDimension, ulong basisBladeId)
        {
            return basisBladeId.GetSuperPatterns((int) vSpaceDimension);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsContaining(uint vSpaceDimension, uint grade, ulong index)
        {
            return BasisBladeId(grade, index).GetSuperPatterns((int) vSpaceDimension);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> SortBasisBladeIDsByGrade(this IEnumerable<ulong> idsSeq)
        {
            return idsSeq.OrderBy(BasisBladeGrade).ThenBy(BasisBladeIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<uint, ulong>> GroupBasisBladeIDsByGrade(this IEnumerable<ulong> idsSeq)
        {
            return idsSeq.GroupBy(BasisBladeGrade);
        }


        /// <summary>
        /// Returns true if the given id contains subId as a binary pattern 
        /// (i.e. whenever subId has a 1 we find id has 1 at the same bit position)
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <param name="subId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BasisBladeIdContains(this ulong basisBladeId, ulong subId)
        {
            return (basisBladeId | subId) == basisBladeId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <param name="basisVectorId"></param>
        /// <param name="subBasisBladeId"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SplitBySmallestBasisVectorId(this ulong basisBladeId, out ulong basisVectorId, out ulong subBasisBladeId)
        {
            basisBladeId.SplitBySmallestBasicPattern(out basisVectorId, out subBasisBladeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <param name="basisVectorId"></param>
        /// <param name="subBasisBladeId"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SplitByLargestBasisVectorId(this ulong basisBladeId, out ulong basisVectorId, out ulong subBasisBladeId)
        {
            basisBladeId.SplitByLargestBasicPattern(out basisVectorId, out subBasisBladeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basisVectorsIds"></param>
        /// <param name="idIndex"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ComposeGaSubspaceBasisBladeId(List<ulong> basisVectorsIds, ulong idIndex)
        {
            return idIndex
                .PatternToPositions()
                .Aggregate(
                    0UL, 
                    (current, pos) => current | basisVectorsIds[pos]
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidVSpaceDimension(this int vaSpaceDim)
        {
            return vaSpaceDim >= 2 && vaSpaceDim < GaSpaceUtils.MaxVSpaceDimension;
        }

        /// <summary>
        /// Test if the given integer is a legal GA space dimension (i.e. positive power of 2 less than or 
        /// equal to 2 ^ MaxVSpaceDim)
        /// </summary>
        /// <param name="gaSpaceDim"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidGaSpaceDimension(this ulong gaSpaceDim)
        {
            return
                gaSpaceDim == (GaSpaceUtils.MaxVSpaceBasisBladeId & gaSpaceDim) &&
                gaSpaceDim.CountOnes() == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidGaSpaceDimension(this int gaSpaceDim)
        {
            return IsValidGaSpaceDimension((ulong) gaSpaceDim);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBasisVectorId(this ulong basisBladeId)
        {
            return basisBladeId.IsBasicPattern();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBasisBivectorId(this ulong basisBladeId)
        {
            return basisBladeId.BasisBladeGrade() == 2;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisVectorId(this ulong basisBladeId)
        {
            return basisBladeId.IsValidBasisBladeId() && basisBladeId.IsBasicPattern();
        }

        public static bool TryGetValidBasisVectorIndex(this ulong basisBladeId, out ulong basisVectorIndex)
        {
            var onesCount = 0;
            basisVectorIndex = 0;

            var i = 0UL;
            while (basisBladeId != 0 && onesCount <= 1)
            {
                if ((basisBladeId & 1) != 0)
                {
                    basisVectorIndex = (onesCount == 0) ? i : 0UL;
                    onesCount++;
                }

                i++;
                basisBladeId >>= 1;
            }

            return onesCount == 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisVectorIndex(ulong index)
        {
            return index < GaSpaceUtils.MaxVSpaceDimension;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBladeId(this ulong basisBladeId)
        {
            return basisBladeId <= GaSpaceUtils.MaxVSpaceBasisBladeId;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBladeGradeIndex(uint vSpaceDimension, uint grade, ulong index)
        {
            if (grade > vSpaceDimension) return false;

            var kvDim = KvSpaceDimension(vSpaceDimension, grade);

            return index < kvDim;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidGrade(uint vSpaceDimension, uint grade)
        {
            return grade <= vSpaceDimension;
        }
        
        /// <summary>
        /// Test if the clifford conjugate of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +--+
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GradeHasNegativeCliffordConjugate(this uint grade)
        {
            var v = grade % 4;
            return v == 1 || v == 2;

            //return ((grade * (grade + 1)) & 2) != 0;
        }
        
        /// <summary>
        /// Test if the grade inverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +-+-
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GradeHasNegativeGradeInvolution(this uint grade)
        {
            return (grade & 1) != 0;
        }
        
        /// <summary>
        /// Test if the reverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: ++--
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GradeHasNegativeReverse(this uint grade)
        {
            return grade % 4 > 1;

            //return ((grade * (grade - 1)) & 2) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BasisBladeHasEvenGrade(this ulong basisBladeId)
        {
            return (basisBladeId.BasisBladeGrade() & 1) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BasisBladeHasOddGrade(this ulong basisBladeId)
        {
            return (basisBladeId.BasisBladeGrade() & 1) != 0;
        }
        
        /// <summary>
        /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BasisBladeIdHasNegativeCliffordConjugate(this ulong id)
        {
            return id < (ulong)GaLookupTables.IsNegativeCliffConjTable.Length
                ? GaLookupTables.IsNegativeCliffConjTable.Get((int)id)
                : ((uint) id.CountOnes()).GradeHasNegativeCliffordConjugate();
        }
        
        /// <summary>
        /// Test if the grade inverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BasisBladeIdHasNegativeGradeInvolution(this ulong id)
        {
            return id < (ulong)GaLookupTables.IsNegativeGradeInvTable.Length
                ? GaLookupTables.IsNegativeGradeInvTable.Get((int)id)
                : ((uint) id.CountOnes()).GradeHasNegativeGradeInvolution();
        }
        
        /// <summary>
        /// Test if the reverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BasisBladeIdHasNegativeReverse(this ulong id)
        {
            return id < (ulong) GaLookupTables.IsNegativeReverseTable.Length
                ? GaLookupTables.IsNegativeReverseTable.Get((int) id)
                : ((uint) id.CountOnes()).GradeHasNegativeReverse();
        }


        /// <summary>
        /// True if the outer product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroOp(ulong id1, ulong id2)
        {
            return (id1 & id2) == 0;
        }

        /// <summary>
        /// True if the outer product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroOp(ulong id1, ulong id2, ulong id3)
        {
            return (id1 & id2 & id3) == 0;
        }

        /// <summary>
        /// True if the Euclidean geometric product of the given euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroEGp(ulong id1, ulong id2)
        {
            return true;
        }

        /// <summary>
        /// True if the scalar product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroESp(ulong id1, ulong id2)
        {
            return id1 == id2;
        }

        /// <summary>
        /// True if the left contraction product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroELcp(ulong id1, ulong id2)
        {
            return (id1 & ~id2) == 0;
        }

        /// <summary>
        /// True if the right contraction product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroERcp(ulong id1, ulong id2)
        {
            return (id2 & ~id1) == 0;
        }

        /// <summary>
        /// True if the fat-dot product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroEFdp(ulong id1, ulong id2)
        {
            return (id1 & ~id2) == 0 || (id2 & ~id1) == 0;
        }

        /// <summary>
        /// True if the Hestenes inner product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroEHip(ulong id1, ulong id2)
        {
            return id1 != 0 && id2 != 0 && ((id1 & ~id2) == 0 || (id2 & ~id1) == 0);
        }

        /// <summary>
        /// True if the anti-commutator product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroEAcp(ulong id1, ulong id2)
        {
            //A acp B = (AB + BA) / 2
            return IsNegativeEGp(id1, id2) == IsNegativeEGp(id2, id1);
        }

        /// <summary>
        /// True if the commutator product of the given Euclidean basis blades is non-zero
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroECp(ulong id1, ulong id2)
        {
            //A cp B = (AB - BA) / 2
            return IsNegativeEGp(id1, id2) != IsNegativeEGp(id2, id1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroTriProductLeftAssociative(ulong id1, ulong id2, ulong id3, Func<ulong, ulong, bool> isNonZeroProductFunc1, Func<ulong, ulong, bool> isNonZeroProductFunc2)
        {
            return isNonZeroProductFunc1(id1, id2) && isNonZeroProductFunc2(id1 ^ id2, id3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroTriProductRightAssociative(ulong id1, ulong id2, ulong id3, Func<ulong, ulong, bool> isNonZeroProductFunc1, Func<ulong, ulong, bool> isNonZeroProductFunc2)
        {
            return isNonZeroProductFunc1(id1, id2 ^ id3) && isNonZeroProductFunc2(id2, id3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroELcpELcpLa(ulong id1, ulong id2, ulong id3)
        {
            return IsNonZeroTriProductLeftAssociative(id1, id2, id3, IsNonZeroELcp, IsNonZeroELcp);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNonZeroELcpELcpRa(ulong id1, ulong id2, ulong id3)
        {
            return IsNonZeroTriProductRightAssociative(id1, id2, id3, IsNonZeroELcp, IsNonZeroELcp);
        }


        /// <summary>
        /// Given a bit pattern in id1 and id2 this shifts id2 by MaxVSpaceDimension bits to the left and 
        /// appends id1 to combine the two patterns using an OR bitwise operation
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong JoinIDs(ulong id1, ulong id2)
        {
            return id1 | (id2 << (int) GaSpaceUtils.MaxVSpaceDimension);
        }

        /// <summary>
        /// Given a bit pattern in id1 and id2 this shifts id2 by VSpaceDim bits to the left and 
        /// appends id1 to combine the two patterns using an OR bitwise operation
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong JoinIDs(uint vSpaceDimension, ulong id1, ulong id2)
        {
            return id1 | (id2 << (int) vSpaceDimension);
        }
        
        //TODO: Try optimizing this
        /// <summary>
        /// Compute if the Euclidean Geometric Product of two basis blades is -1.
        /// This method is slower than lookup, but can be used for GAs with dimension
        /// more than 15
        /// </summary>
        /// <param name="id1"></param>
        /// <returns></returns>
        public static bool ComputeIsNegativeEGp(ulong id1)
        {
            if (id1 == 0ul) 
                return false;

            var flag = false;
            var id = id1;

            //Find largest 1-bit of ID1 and create a bit mask
            var initMask1 = 1ul;
            while (initMask1 <= id1)
                initMask1 <<= 1;

            initMask1 >>= 1;

            var mask2 = 1ul;
            while (mask2 <= id1)
            {
                //If the current bit in ID2 is one:
                if ((id1 & mask2) != 0ul)
                {
                    //Count number of swaps, each new swap inverts the final sign
                    var mask1 = initMask1;

                    while (mask1 > mask2)
                    {
                        if ((id & mask1) != 0ul)
                            flag = !flag;

                        mask1 >>= 1;
                    }
                }

                //Invert the corresponding bit in ID1
                id ^= mask2;

                mask2 <<= 1;
            }

            return flag;
        }

        /// <summary>
        /// Compute if the Euclidean Geometric Product of two basis blades is -1.
        /// This method is slower than lookup, but can be used for GAs with dimension
        /// more than 15
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public static bool ComputeIsNegativeEGp(ulong id1, ulong id2)
        {
            if (id1 == 0ul || id2 == 0ul) return false;

            var flag = false;
            var id = id1;

            //Find largest 1-bit of ID1 and create a bit mask
            var initMask1 = 1ul;
            while (initMask1 <= id1)
                initMask1 <<= 1;

            initMask1 >>= 1;

            var mask2 = 1ul;
            while (mask2 <= id2)
            {
                //If the current bit in ID2 is one:
                if ((id2 & mask2) != 0ul)
                {
                    //Count number of swaps, each new swap inverts the final sign
                    var mask1 = initMask1;

                    while (mask1 > mask2)
                    {
                        if ((id & mask1) != 0ul)
                            flag = !flag;

                        mask1 >>= 1;
                    }
                }

                //Invert the corresponding bit in ID1
                id ^= mask2;

                mask2 <<= 1;
            }

            return flag;
        }
        
        //TODO: Try optimizing this
        public static int ComputeEGpSignature(ulong id1)
        {
            if (id1 == 0ul) return 1;

            var signature = 1;
            var id = id1;

            //Find largest 1-bit of ID1 and create a bit mask
            var initMask1 = 
                id1.PatternToMask();

            //var initMask1 = 1ul;
            //while (initMask1 <= id1)
            //    initMask1 <<= 1;

            //initMask1 >>= 1;

            var mask2 = 1ul;
            while (mask2 <= id1)
            {
                //If the current bit in ID2 is one:
                if ((id1 & mask2) != 0ul)
                {
                    //Count number of swaps, each new swap inverts the final sign
                    var mask1 = initMask1;

                    while (mask1 > mask2)
                    {
                        if ((id & mask1) != 0ul)
                            signature = -signature;

                        mask1 >>= 1;
                    }
                }

                //Invert the corresponding bit in ID1
                id ^= mask2;

                mask2 <<= 1;
            }

            return signature;
        }

        public static int ComputeEGpSignature(ulong id1, ulong id2)
        {
            if (id1 == 0ul || id2 == 0ul) return 1;

            var signature = 1;
            var id = id1;

            //Find largest 1-bit of ID1 and create a bit mask
            var initMask1 = 1ul;
            while (initMask1 <= id1)
                initMask1 <<= 1;

            initMask1 >>= 1;

            var mask2 = 1ul;
            while (mask2 <= id2)
            {
                //If the current bit in ID2 is one:
                if ((id2 & mask2) != 0ul)
                {
                    //Count number of swaps, each new swap inverts the final sign
                    var mask1 = initMask1;

                    while (mask1 > mask2)
                    {
                        if ((id & mask1) != 0ul)
                            signature = -signature;

                        mask1 >>= 1;
                    }
                }

                //Invert the corresponding bit in ID1
                id ^= mask2;

                mask2 <<= 1;
            }

            return signature;
        }

        /// <summary>
        /// Find if the Euclidean Geometric Product of two basis blades is -1.
        /// For GAs with dimension less or equal to 16 this is 4 to 24 times faster than computing.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPositiveEGp(ulong id)
        {
            return !IsNegativeEGp(id);
        }
        
        /// <summary>
        /// Find if the Euclidean Geometric Product of two basis blades is -1.
        /// For GAs with dimension less or equal to 16 this is 4 to 24 times faster than computing.
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPositiveEGp(ulong id1, ulong id2)
        {
            return !IsNegativeEGp(id1, id2);
        }

        /// <summary>
        /// Find if the Euclidean Geometric Product of two basis blades is -1.
        /// For GAs with dimension less or equal to 16 this is 4 to 24 times faster than computing.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegativeEGp(ulong id)
        {
            if (id >= (ulong) GaLookupTables.IsNegativeEgpLookupTables.Length) 
                return ComputeIsNegativeEGp(id);

            var lookupTable = 
                GaLookupTables.IsNegativeEgpLookupTables[id];

            return id < (ulong) lookupTable.Count 
                ? lookupTable[(int) id] 
                : ComputeIsNegativeEGp(id);
        }

        /// <summary>
        /// Find if the Euclidean Geometric Product of two basis blades is -1.
        /// For GAs with dimension less or equal to 16 this is 4 to 24 times faster than computing.
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegativeEGp(ulong id1, ulong id2)
        {
            if (id1 >= (ulong) GaLookupTables.IsNegativeEgpLookupTables.Length) 
                return ComputeIsNegativeEGp(id1, id2);

            var lookupTable = 
                GaLookupTables.IsNegativeEgpLookupTables[id1];

            return id2 < (ulong)lookupTable.Count 
                ? lookupTable[(int) id2] 
                : ComputeIsNegativeEGp(id1, id2);
        }
        
        /// <summary>
        /// Find if the Geometric Product of two basis blades is -1, given the total non-degenerate
        /// metric signature value of their common basis vectors.
        /// For GAs with dimension less or equal to 16 this is 4 to 24 times faster than computing.
        /// </summary>
        /// <param name="basisBladeSignature">The total non-degenerate metric signature value of their common basis vectors. Must be non-zero.</param>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegativeGp(int basisBladeSignature, ulong id)
        {
            Debug.Assert(basisBladeSignature is 1 or -1);

            var isNegativeEGp = IsNegativeEGp(id);

            return (basisBladeSignature > 0)
                ? isNegativeEGp 
                : !isNegativeEGp;
        }

        /// <summary>
        /// Find if the Geometric Product of two basis blades is -1, given the total non-degenerate
        /// metric signature value of their common basis vectors.
        /// For GAs with dimension less or equal to 16 this is 4 to 24 times faster than computing.
        /// </summary>
        /// <param name="basisBladeSignature">The total non-degenerate metric signature value of their common basis vectors. Must be non-zero.</param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegativeGp(int basisBladeSignature, ulong id1, ulong id2)
        {
            Debug.Assert(basisBladeSignature is 1 or -1);

            var isNegativeEGp = IsNegativeEGp(id1, id2);

            return (basisBladeSignature > 0)
                ? isNegativeEGp 
                : !isNegativeEGp;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int EGpSignature(ulong id)
        {
            if (id >= (ulong) GaLookupTables.IsNegativeEgpLookupTables.Length) 
                return ComputeEGpSignature(id);

            var lookupTable = 
                GaLookupTables.IsNegativeEgpLookupTables[id];

            if (id < (ulong) lookupTable.Count)
                return lookupTable[(int) id] ? -1 : 1;

            return ComputeEGpSignature(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ENormSquaredSignature(ulong id)
        {
            var reverseSignature = 
                id.BasisBladeIdHasNegativeReverse() ? -1 : 1;

            if (id >= (ulong) GaLookupTables.IsNegativeEgpLookupTables.Length) 
                return reverseSignature * ComputeEGpSignature(id);

            var lookupTable = 
                GaLookupTables.IsNegativeEgpLookupTables[id];

            if (id < (ulong) lookupTable.Count)
                return lookupTable[(int) id] 
                    ? -reverseSignature 
                    : reverseSignature;

            return reverseSignature * ComputeEGpSignature(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int EGpSignature(ulong id1, ulong id2)
        {
            if (id1 >= (ulong) GaLookupTables.IsNegativeEgpLookupTables.Length) 
                return ComputeEGpSignature(id1, id2);

            var lookupTable = 
                GaLookupTables.IsNegativeEgpLookupTables[id1];

            if (id2 < (ulong) lookupTable.Count)
                return lookupTable[(int) id2] ? -1 : 1;

            return ComputeEGpSignature(id1, id2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int EGpReverseSignature(ulong id1, ulong id2)
        {
            var signature = EGpSignature(id1, id2);

            return id2.BasisBladeIdHasNegativeReverse()
                ? -signature
                : signature;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int OpSignature(ulong id1, ulong id2)
        {
            return (id1 & id2) == 0
                ? EGpSignature(id1, id2)
                : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ESpSignature(ulong id)
        {
            return EGpSignature(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ESpSignature(ulong id1, ulong id2)
        {
            return id1 == id2
                ? EGpSignature(id1, id2)
                : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ELcpSignature(ulong id1, ulong id2)
        {
            return (id1 & ~id2) == 0
                ? EGpSignature(id1, id2)
                : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ERcpSignature(ulong id1, ulong id2)
        {
            return (~id1 & id2) == 0
                ? EGpSignature(id1, id2)
                : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int EFdpSignature(ulong id1, ulong id2)
        {
            return (id1 & ~id2) == 0 || (id2 & ~id1) == 0
                ? EGpSignature(id1, id2)
                : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int EHipSignature(ulong id1, ulong id2)
        {
            return id1 != 0 && id2 != 0 && ((id1 & ~id2) == 0 || (id2 & ~id1) == 0)
                ? EGpSignature(id1, id2)
                : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int EAcpSignature(ulong id1, ulong id2)
        {
            //A acp B = (AB + BA) / 2
            return IsNegativeEGp(id1, id2) == IsNegativeEGp(id2, id1)
                ? EGpSignature(id1, id2)
                : 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ECpSignature(ulong id1, ulong id2)
        {
            //A cp B = (AB - BA) / 2
            return IsNegativeEGp(id1, id2) != IsNegativeEGp(id2, id1)
                ? EGpSignature(id1, id2)
                : 0;
        }

        /// <summary>
        /// Find if the Euclidean Geometric Product of the given basis blades is -1.
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegativeEGp(ulong id1, ulong id2, ulong id3)
        {
            return IsNegativeEGp(id1, id2) != IsNegativeEGp(id1 ^ id2, id3);
        }

        /// <summary>
        /// Find if the Euclidean Geometric Product of a basis vector and a basis blade is -1.
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegativeVectorEGp(ulong index1, ulong id2)
        {
            if (index1 < (ulong)GaLookupTables.IsNegativeVectorEgpLookupTables.Length)
            {
                var lookupTable = GaLookupTables.IsNegativeEgpLookupTables[index1];

                if (id2 < (ulong)lookupTable.Count)
                    return lookupTable[(int)id2];
            }

            var id1 = 1UL << (int)index1;

            return ComputeIsNegativeEGp(id1, id2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId(this IEnumerable<ulong> idList)
        {
            return idList.Aggregate(
                0UL, 
                (maxValue, item) => 
                    maxValue < item ? item : maxValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId(this IEnumerable<ulong> indexList, uint grade)
        {
            var maxIndex = 
                indexList.Aggregate(
                    -1L, 
                    (maxValue, item) => 
                        maxValue < (long)item ? (long)item : maxValue
                );
                
            return maxIndex < 0
                ? 0UL
                : BasisBladeId(grade, (ulong) maxIndex);
        }
        
        public static ulong GetMaxBasisBladeId<T>(this IReadOnlyDictionary<uint, Dictionary<ulong, T>> gradeIndexScalarDictionary)
        {
            var maxBasisBladeId = 0UL;

            foreach (var (grade, indexScalarDictionary) in gradeIndexScalarDictionary)
            {
                if (indexScalarDictionary.Count == 0)
                    continue;

                var id = BasisBladeId(
                    grade,
                    indexScalarDictionary.Keys.Max()
                );

                if (id > maxBasisBladeId)
                    maxBasisBladeId = id;
            }

            return maxBasisBladeId;
        }

        /// <summary>
        /// The max basis blade ID in a GA space with a given dimension
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId(this uint vSpaceDimension)
        {
            return (1ul << (int) vSpaceDimension) - 1ul;
        }

        /// <summary>
        /// The max basis blade ID in a GA space with a given dimension
        /// </summary>
        /// <param name="maxBasisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimension(this ulong maxBasisBladeId)
        {
            var vSpaceDimension = 
                maxBasisBladeId.LastOneBitPosition() + 1;

            return vSpaceDimension < 1
                ? 1U
                : (uint) vSpaceDimension;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimension(this IEnumerable<ulong> idList)
        {
            return idList.GetMaxBasisBladeId().GetMinVSpaceDimension();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimension(this IEnumerable<ulong> idList, uint grade)
        {
            return idList.GetMaxBasisBladeId(grade).GetMinVSpaceDimension();
        }
    }
}
