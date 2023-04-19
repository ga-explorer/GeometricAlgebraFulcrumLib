using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis
{
    public static class BasisBladeUtils
    {
        //private static ulong LookupBasisBladeId(uint grade, ulong basisBladeIndex)
        //{
        //    if (grade >= BasisBladeDataLookup.GradeIndexToIdTable.Count) 
        //        return basisBladeIndex.IndexToCombinadicPattern((int) grade);
            
        //    var table = 
        //        BasisBladeDataLookup.GradeIndexToIdTable[(int) grade];

        //    return basisBladeIndex < (ulong) table.Length 
        //        ? table[basisBladeIndex] 
        //        : basisBladeIndex.IndexToCombinadicPattern((int) grade);
        //}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int VSpaceDimensions(this int id)
        {
            Debug.Assert(id >= 0);

            return id == 0 
                ? 0 : BitOperations.TrailingZeroCount(id) + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int VSpaceDimensions(this ulong id)
        {
            return id == 0UL 
                ? 0 : BitOperations.TrailingZeroCount(id) + 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Grade(this int id)
        {
            Debug.Assert(id >= 0);

            return BitOperations.PopCount((uint) id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Grade(this ulong id)
        {
            return BitOperations.PopCount(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Grade(this IIndexSet id)
        {
            return id.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong KvSpaceDimensions(this ulong id)
        {
            return id == 0UL 
                ? 1UL 
                : (BitOperations.TrailingZeroCount(id) + 1).GetBinomialCoefficient(BitOperations.PopCount(id));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int VSpaceDimensions(this IIndexSet id)
        {
            return id.IsEmptySet 
                ? 0 : id.LastIndex + 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong KvSpaceDimensions(this IIndexSet id)
        {
            return id.IsEmptySet
                ? 1UL 
                : (id.LastIndex + 1).GetBinomialCoefficient(id.Count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBasisScalar(this ulong id)
        {
            return id == 0UL;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBasisScalar(this IIndexSet id)
        {
            return id.IsEmptySet;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBasisVector(this ulong id)
        {
            return BitOperations.IsPow2(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBasisVector(this IIndexSet id)
        {
            return id.Count == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBasisBivector(this ulong id)
        {
            return BitOperations.PopCount(id) == 2;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBasisBivector(this IIndexSet id)
        {
            return id.Count == 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBasisKVector(this ulong id, int grade)
        {
            return BitOperations.PopCount(id) == grade;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBasisKVector(this IIndexSet id, int grade)
        {
            return id.Count == grade;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetBasisBladeIds(this ulong maxBasisBladeId, uint grade)
        {
            var index = 0UL;
            var id = index.BasisBladeIndexToId(grade);

            while (id <= maxBasisBladeId)
            {
                yield return id;

                index++;
                id = index.BasisBladeIndexToId(grade);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> GetBasisBladeIndices(this ulong maxBasisBladeId, uint grade)
        {
            var index = 0UL;
            var id = index.BasisBladeIndexToId(grade);

            while (id <= maxBasisBladeId)
            {
                yield return index;

                index++;
                id = index.BasisBladeIndexToId(grade);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint BasisBladeIdToMinVSpaceDimension(this ulong basisBladeId)
        {
            return (uint) (1 + basisBladeId.LastOneBitPosition());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint BasisBladeIndexToMinVSpaceDimension(this ulong basisBladeIndex, uint grade)
        {
            return grade switch
            {
                0U => basisBladeIndex == 0UL ? 0U : throw new InvalidOperationException(),
                1U => 1U + (uint) basisBladeIndex,
                2U => 1U + (uint) (0.5d * (1d + Math.Sqrt(1UL + 8UL * basisBladeIndex))),
                _ => (uint) (1 + basisBladeIndex.BasisBladeIndexToId(grade).LastOneBitPosition())
            };
        }
        
        /// <summary>
        /// Find the ID of a basis blade given its grade and basisBladeIndex
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBladeGradeIndexToId(int grade, ulong index)
        {
            return grade switch
            {
                < 0 => throw new InvalidOperationException(),
                0 => index == 0 ? 0UL : throw new ArgumentOutOfRangeException(nameof(index)),
                1 => index.BasisVectorIndexToId(),
                2 => index.BasisBivectorIndexToId(),
                _ => BasisBladeDataLookup.BasisBladeId((uint) grade, index)
            };
        }

        /// <summary>
        /// Find the ID of a basis blade given its grade and basisBladeIndex
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBladeGradeIndexToId(uint grade, ulong index)
        {
            return grade switch
            {
                0U => index == 0 ? 0UL : throw new ArgumentOutOfRangeException(nameof(index)),
                1U => index.BasisVectorIndexToId(),
                2U => index.BasisBivectorIndexToId(),
                _ => BasisBladeDataLookup.BasisBladeId(grade, index)
            };
        }

        /// <summary>
        /// Find the ID of a basis blade given its grade and basisBladeIndex
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBladeIndexToId(this ulong index, uint grade)
        {
            return grade switch
            {
                0U => index == 0 ? 0UL : throw new ArgumentOutOfRangeException(nameof(index)),
                1U => index.BasisVectorIndexToId(),
                2U => index.BasisBivectorIndexToId(),
                _ => BasisBladeDataLookup.BasisBladeId(grade, index)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisVectorIndicesToBasisBladeId(this IEnumerable<int> basisVectorIndices)
        {
            return basisVectorIndices
                .Aggregate(0UL, (acc, item) => acc | item.BasisVectorIndexToId());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisVectorIndicesToBasisBladeId(this IEnumerable<ulong> basisVectorIndices)
        {
            return basisVectorIndices
                .Aggregate(0UL, (acc, item) => acc | item.BasisVectorIndexToId());
        }
        

        /// <summary>
        /// Get the largest basis vector basisBladeIndex of the given basis blade
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBladeMaxVectorIndex(uint grade, ulong index)
        {
            return (ulong) BasisBladeGradeIndexToId(grade, index).LastOneBitPosition();
        }
        
        /// <summary>
        /// Find the grade of a basis blade given its ID
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint BasisBladeIdToGrade(this ulong basisBladeId)
        {
            return BasisBladeDataLookup.BasisBladeGrade(basisBladeId);
        }
        
        /// <summary>
        /// Find the basisBladeIndex of a basis blade given its ID
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBladeIdToIndex(this ulong basisBladeId)
        {
            return BasisBladeDataLookup.BasisBladeIndex(basisBladeId);
        }

        /// <summary>
        /// Find the grade and basisBladeIndex of a basis blade given its ID
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BasisBladeIdToGradeIndex(this ulong basisBladeId, out uint grade, out ulong index)
        {
            (grade, index) = BasisBladeDataLookup.BasisBladeGradeIndex(basisBladeId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaGradeKvIndexRecord BasisBladeIdToGradeIndex(this ulong basisBladeId)
        {
            return BasisBladeDataLookup.BasisBladeGradeIndex(basisBladeId);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeIdToName(this ulong basisBladeId, params string[] basisVectorNames)
        {
            return basisVectorNames.ConcatenateUsingPattern(basisBladeId, "E0", "^");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeGradeIndexToName(uint grade, ulong basisBladeIndex, params string[] basisVectorNames)
        {
            return basisVectorNames
                .ConcatenateUsingPattern(
                    basisBladeIndex.BasisBladeIndexToId(grade), 
                    "E0", 
                    "^"
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeIdToIndexedName(this ulong basisBladeId)
        {
            return "E" + basisBladeId;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeGradeIndexToIndexedName(uint grade, ulong basisBladeIndex)
        {
            return "E" + basisBladeIndex.BasisBladeIndexToId(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeIdToBinaryIndexedName(ulong basisBladeId, uint vSpaceDimensions)
        {
            return "B" + basisBladeId.PatternToString((int) vSpaceDimensions);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeGradeIndexToBinaryIndexedName(uint grade, ulong basisBladeIndex, uint vSpaceDimensions)
        {
            return "B" + basisBladeIndex.BasisBladeIndexToId(grade).PatternToString((int) vSpaceDimensions);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeIdToGradeIndexName(this ulong basisBladeId)
        {
            return
                new StringBuilder(32)
                .Append('G')
                .Append(basisBladeId.BasisBladeIdToGrade())
                .Append('I')
                .Append(basisBladeId.BasisBladeIdToIndex())
                .ToString();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeGradeIndexToGradeIndexName(uint grade, ulong basisBladeIndex)
        {
            return
                new StringBuilder(32)
                .Append('G')
                .Append(grade)
                .Append('I')
                .Append(basisBladeIndex)
                .ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIdToBasisVectorIds(this ulong basisBladeId)
        {
            return basisBladeId.GetBasicPatterns();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeGradeIndexToBasisVectorIds(uint grade, ulong basisBladeIndex)
        {
            return basisBladeIndex.BasisBladeIndexToId(grade).GetBasicPatterns();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIdToBasisVectorIndices(this ulong basisBladeId)
        {
            return basisBladeId.PatternToPositions().Select(i => (ulong) i);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> BasisBladeGradeIndexToInt32BasisVectorIndices(uint grade, ulong basisBladeIndex)
        {
            return basisBladeIndex.BasisBladeIndexToId(grade).PatternToPositions();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeGradeIndexToBasisVectorIndices(uint grade, ulong basisBladeIndex)
        {
            return basisBladeIndex.BasisBladeIndexToId(grade).PatternToPositions().Select(i => (ulong)i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsInside(this ulong basisBladeId)
        {
            return basisBladeId.GetSubPatterns();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsInside(uint grade, ulong basisBladeIndex)
        {
            return basisBladeIndex.BasisBladeIndexToId(grade).GetSubPatterns();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsContaining(ulong basisBladeId, uint vSpaceDimensions)
        {
            return basisBladeId.GetSuperPatterns((int) vSpaceDimensions);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsContaining(uint grade, ulong basisBladeIndex, uint vSpaceDimensions)
        {
            return basisBladeIndex.BasisBladeIndexToId(grade).GetSuperPatterns((int) vSpaceDimensions);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> SortBasisBladeIDsByGrade(this IEnumerable<ulong> basisBladeIdsList)
        {
            return basisBladeIdsList.OrderBy(BasisBladeIdToGrade).ThenBy(BasisBladeIdToIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<uint, ulong>> GroupBasisBladeIDsByGrade(this IEnumerable<ulong> basisBladeIdsList)
        {
            return basisBladeIdsList.GroupBy(BasisBladeIdToGrade);
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<ulong> SplitByLargestBasisVectorId(this ulong basisBladeId)
        {
            basisBladeId.SplitByLargestBasicPattern(out var basisVectorId, out var subBasisBladeId);

            return new Pair<ulong>(basisVectorId, subBasisBladeId);
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
        public static ulong ComposeGeoSubspaceBasisBladeId(List<ulong> basisVectorsIds, ulong idIndex)
        {
            return idIndex
                .PatternToPositions()
                .Aggregate(
                    0UL, 
                    (current, pos) => current | basisVectorsIds[pos]
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
            return indexSeq.Select(index => index.BasisBladeIndexToId(grade));
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
            return indexSeq.Select(index => index.BasisBladeIndexToId(grade));
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBasisBladeOfGrade(this ulong basisBladeId, uint grade)
        {
            return grade == BasisBladeDataLookup.BasisBladeGrade(basisBladeId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBladeId(this ulong basisBladeId)
        {
            return basisBladeId <= BasisBladeDataLookup.MaxBasisBladeId;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBladeId(this ulong basisBladeId, uint vSpaceDimensions)
        {
            return basisBladeId < 1UL << (int) vSpaceDimensions;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBladeIndex(this ulong basisBladeIndex, uint grade)
        {
            if (grade > BasisBladeDataLookup.MaxVSpaceDimension) 
                return false;

            var kvDim = BasisBladeDataLookup.MaxVSpaceDimension.GetBinomialCoefficient(grade);

            return basisBladeIndex < kvDim;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBladeIndex(this ulong basisBladeIndex, uint grade, uint vSpaceDimensions)
        {
            if (grade > vSpaceDimensions) 
                return false;

            var kvDim = vSpaceDimensions.GetBinomialCoefficient(grade);

            return basisBladeIndex < kvDim;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBladeGradeIndex(uint grade, ulong basisBladeIndex)
        {
            if (grade > BasisBladeDataLookup.MaxVSpaceDimension) 
                return false;

            var kvDim = BasisBladeDataLookup.MaxVSpaceDimension.GetBinomialCoefficient(grade);

            return basisBladeIndex < kvDim;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBladeGradeIndex(uint grade, ulong basisBladeIndex, uint vSpaceDimensions)
        {
            if (grade > vSpaceDimensions) 
                return false;

            var kvDim = vSpaceDimensions.GetBinomialCoefficient(grade);

            return basisBladeIndex < kvDim;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidGrade(this uint grade)
        {
            return grade <= BasisBladeDataLookup.MaxVSpaceDimension;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidGrade(this uint grade, uint vSpaceDimensions)
        {
            return grade <= vSpaceDimensions;
        }

        
        /// <summary>
        /// Get all possible grades of the meet of two blades grades
        /// </summary>
        /// <param name="vSpaceDimensions"></param>
        /// <param name="grade1"></param>
        /// <param name="grade2"></param>
        /// <returns></returns>
        public static IEnumerable<int> GradesOfMeet(this int vSpaceDimensions, int grade1, int grade2)
        {
            if (grade1 > vSpaceDimensions || grade2 > vSpaceDimensions || grade1 < 0 || grade2 < 0)
                yield break;

            var maxGrade = Math.Min(grade1, grade2);

            //TODO: Should this be grade++ instead of grade += 2 ?
            for (var grade = 0; grade <= maxGrade; grade += 2)
                yield return grade;
        }

        /// <summary>
        /// Get all possible grades of the meet of two blades grades
        /// </summary>
        /// <param name="vSpaceDimensions"></param>
        /// <param name="grade1"></param>
        /// <param name="grade2"></param>
        /// <returns></returns>
        public static IEnumerable<int> GradesOfJoin(this int vSpaceDimensions, int grade1, int grade2)
        {
            if (grade1 > vSpaceDimensions || grade2 > vSpaceDimensions || grade1 < 0 || grade2 < 0)
                yield break;

            var minGrade = Math.Max(grade1, grade2);
            var maxGrade = Math.Min(vSpaceDimensions, grade1 + grade2);

            //TODO: Should this be grade++ instead of grade += 2 ?
            for (var grade = minGrade; grade <= maxGrade; grade += 2)
                yield return grade;
        }

        /// <summary>
        /// Return a list of all possible grades in the geometric product of two k-vectors with
        /// the given grades
        /// </summary>
        /// <param name="vSpaceDimensions"></param>
        /// <param name="grade1"></param>
        /// <param name="grade2"></param>
        /// <returns></returns>
        public static IEnumerable<int> GradesOfEGp(this int vSpaceDimensions, int grade1, int grade2)
        {
            if (grade1 > vSpaceDimensions || grade2 > vSpaceDimensions)
                yield break;

            var minGrade = Math.Abs(grade1 - grade2);
            var maxGrade = Math.Min(vSpaceDimensions, grade1 + grade2);

            for (var grade = minGrade; grade <= maxGrade; grade += 2)
                yield return grade;
        }


        /// <summary>
        /// Test if the grade inverse of a basis blade with a given grade is 1
        /// the original basis blade Sign Pattern: +-+-
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GradeInvolutionIsPositiveOfGrade(this int grade)
        {
            return (grade & 1) == 0;
        }

        /// <summary>
        /// Test if the grade inverse of a basis blade with a given grade is 1
        /// the original basis blade Sign Pattern: +-+-
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GradeInvolutionIsPositiveOfGrade(this uint grade)
        {
            return (grade & 1) == 0;
        }
        
        /// <summary>
        /// Test if the grade inverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +-+-
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GradeInvolutionIsNegativeOfGrade(this int grade)
        {
            return (grade & 1) != 0;
        }

        /// <summary>
        /// Test if the grade inverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +-+-
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GradeInvolutionIsNegativeOfGrade(this uint grade)
        {
            return (grade & 1) != 0;
        }
        
        /// <summary>
        /// Test if the grade inverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +-+-
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntegerSign GradeInvolutionSignOfGrade(this int grade)
        {
            return (grade & 1) != 0 
                ? IntegerSign.Positive 
                : IntegerSign.Negative;
        }

        /// <summary>
        /// Test if the grade inverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +-+-
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntegerSign GradeInvolutionSignOfGrade(this uint grade)
        {
            return (grade & 1) != 0 
                ? IntegerSign.Positive 
                : IntegerSign.Negative;
        }
        
        /// <summary>
        /// Test if the reverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: ++--
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReverseIsPositiveOfGrade(this int grade)
        {
            return grade % 4 < 2;

            //return ((grade * (grade - 1)) & 2) != 0;
        }

        /// <summary>
        /// Test if the reverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: ++--
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReverseIsPositiveOfGrade(this uint grade)
        {
            return grade % 4 < 2;

            //return ((grade * (grade - 1)) & 2) != 0;
        }
        
        /// <summary>
        /// Test if the reverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: ++--
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReverseIsNegativeOfGrade(this int grade)
        {
            return grade % 4 > 1;

            //return ((grade * (grade - 1)) & 2) != 0;
        }

        /// <summary>
        /// Test if the reverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: ++--
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReverseIsNegativeOfGrade(this uint grade)
        {
            return grade % 4 > 1;

            //return ((grade * (grade - 1)) & 2) != 0;
        }
        
        /// <summary>
        /// Test if the reverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: ++--
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntegerSign ReverseSignOfGrade(this int grade)
        {
            return grade % 4 > 1 
                ? IntegerSign.Negative 
                : IntegerSign.Positive;
        }

        /// <summary>
        /// Test if the reverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: ++--
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntegerSign ReverseSignOfGrade(this uint grade)
        {
            return grade % 4 > 1 
                ? IntegerSign.Negative 
                : IntegerSign.Positive;
        }
        
        /// <summary>
        /// Test if the clifford conjugate of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +--+
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CliffordConjugateIsPositiveOfGrade(this int grade)
        {
            return grade % 4 is 0 or 3;

            //return ((grade * (grade + 1)) & 2) != 0;
        }

        /// <summary>
        /// Test if the clifford conjugate of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +--+
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CliffordConjugateIsPositiveOfGrade(this uint grade)
        {
            return grade % 4 is 0 or 3;

            //return ((grade * (grade + 1)) & 2) != 0;
        }
        
        /// <summary>
        /// Test if the clifford conjugate of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +--+
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CliffordConjugateIsNegativeOfGrade(this int grade)
        {
            return grade % 4 is 1 or 2;

            //return ((grade * (grade + 1)) & 2) != 0;
        }

        /// <summary>
        /// Test if the clifford conjugate of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +--+
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CliffordConjugateIsNegativeOfGrade(this uint grade)
        {
            return grade % 4 is 1 or 2;

            //return ((grade * (grade + 1)) & 2) != 0;
        }
        
        /// <summary>
        /// Test if the clifford conjugate of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +--+
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntegerSign CliffordConjugateSignOfGrade(this int grade)
        {
            return grade % 4 is 1 or 2 
                ? IntegerSign.Negative 
                : IntegerSign.Positive;
        }

        /// <summary>
        /// Test if the clifford conjugate of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +--+
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntegerSign CliffordConjugateSignOfGrade(this uint grade)
        {
            return grade % 4 is 1 or 2 
                ? IntegerSign.Negative 
                : IntegerSign.Positive;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BasisBladeHasEvenGrade(this ulong basisBladeId)
        {
            return (basisBladeId.BasisBladeIdToGrade() & 1) == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BasisBladeHasOddGrade(this ulong basisBladeId)
        {
            return (basisBladeId.BasisBladeIdToGrade() & 1) != 0;
        }
        
        /// <summary>
        /// Test if the grade inverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GradeInvolutionIsPositiveOfBasisBladeId(this ulong basisBladeId)
        {
            return BasisBladeDataLookup.GradeInvolutionIsPositive(basisBladeId);
        }

        /// <summary>
        /// Test if the grade inverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GradeInvolutionIsNegativeOfBasisBladeId(this ulong basisBladeId)
        {
            return BasisBladeDataLookup.GradeInvolutionIsNegative(basisBladeId);
        }
        
        /// <summary>
        /// Test if the grade inverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntegerSign GradeInvolutionSignOfBasisBladeId(this ulong basisBladeId)
        {
            return BasisBladeDataLookup.GradeInvolutionSign(basisBladeId);
        }
        
        /// <summary>
        /// Test if the grade inverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntegerSign GradeInvolutionSignOfBasisBladeId(this IIndexSet basisBladeId)
        {
            return basisBladeId.Count.GradeInvolutionSignOfGrade();
        }

        /// <summary>
        /// Test if the reverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReverseIsPositiveOfBasisBladeId(this ulong basisBladeId)
        {
            return BasisBladeDataLookup.ReverseIsPositive(basisBladeId);
        }

        /// <summary>
        /// Test if the reverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReverseIsNegativeOfBasisBladeId(this ulong basisBladeId)
        {
            return BasisBladeDataLookup.ReverseIsNegative(basisBladeId);
        }
        
        /// <summary>
        /// Test if the reverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntegerSign ReverseSignOfBasisBladeId(this ulong basisBladeId)
        {
            return BasisBladeDataLookup.ReverseSign(basisBladeId);
        }
        
        /// <summary>
        /// Test if the reverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntegerSign ReverseSignOfBasisBladeId(this IIndexSet basisBladeId)
        {
            return basisBladeId.Count.ReverseSignOfGrade();
        }

        /// <summary>
        /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CliffordConjugateIsPositiveOfBasisBladeId(this ulong basisBladeId)
        {
            return BasisBladeDataLookup.CliffordConjugateIsPositive(basisBladeId);
        }

        /// <summary>
        /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CliffordConjugateIsNegativeOfBasisBladeId(this ulong basisBladeId)
        {
            return BasisBladeDataLookup.CliffordConjugateIsNegative(basisBladeId);
        }
        
        /// <summary>
        /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntegerSign CliffordConjugateSignOfBasisBladeId(this ulong basisBladeId)
        {
            return BasisBladeDataLookup.CliffordConjugateSign(basisBladeId);
        }
        
        /// <summary>
        /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntegerSign CliffordConjugateSignOfBasisBladeId(this IIndexSet basisBladeId)
        {
            return basisBladeId.Count.CliffordConjugateSignOfGrade();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId(this IEnumerable<ulong> basisBladeIdList)
        {
            return basisBladeIdList.Aggregate(
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
                : ((ulong) maxIndex).BasisBladeIndexToId(grade);
        }
        
        public static ulong GetMaxBasisBladeId<T>(this IReadOnlyDictionary<uint, Dictionary<ulong, T>> gradeIndexScalarDictionary)
        {
            var maxBasisBladeId = 0UL;

            foreach (var (grade, indexScalarDictionary) in gradeIndexScalarDictionary)
            {
                if (indexScalarDictionary.Count == 0)
                    continue;

                var id = 
                    indexScalarDictionary.Keys.Max().BasisBladeIndexToId(grade);

                if (id > maxBasisBladeId)
                    maxBasisBladeId = id;
            }

            return maxBasisBladeId;
        }

        /// <summary>
        /// The max basis blade ID in a GA space with a given dimension
        /// </summary>
        /// <param name="vSpaceDimensions"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId(this uint vSpaceDimensions)
        {
            return (1ul << (int) vSpaceDimensions) - 1ul;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimension(this IEnumerable<ulong> basisBladeIdList)
        {
            return basisBladeIdList
                .GetMaxBasisBladeId()
                .BasisBladeIdToMinVSpaceDimension();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GetMinVSpaceDimension(this IEnumerable<ulong> basisBladeIdList, uint grade)
        {
            return basisBladeIdList.GetMaxBasisBladeId(grade).BasisBladeIdToMinVSpaceDimension();
        }
    }
}
