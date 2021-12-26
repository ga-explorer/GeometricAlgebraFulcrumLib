﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using NumericalGeometryLib.GeometricAlgebra.Structures;

namespace NumericalGeometryLib.GeometricAlgebra.Basis
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
        public static GradeIndexRecord BasisBladeIdToGradeIndex(this ulong basisBladeId)
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
        public static string BasisBladeIdToBinaryIndexedName(ulong basisBladeId, uint vSpaceDimension)
        {
            return "B" + basisBladeId.PatternToString((int) vSpaceDimension);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeGradeIndexToBinaryIndexedName(uint grade, ulong basisBladeIndex, uint vSpaceDimension)
        {
            return "B" + basisBladeIndex.BasisBladeIndexToId(grade).PatternToString((int) vSpaceDimension);
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
        public static IEnumerable<ulong> BasisBladeIDsContaining(ulong basisBladeId, uint vSpaceDimension)
        {
            return basisBladeId.GetSuperPatterns((int) vSpaceDimension);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsContaining(uint grade, ulong basisBladeIndex, uint vSpaceDimension)
        {
            return basisBladeIndex.BasisBladeIndexToId(grade).GetSuperPatterns((int) vSpaceDimension);
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
        public static bool IsValidBasisBladeId(this ulong basisBladeId, uint vSpaceDimension)
        {
            return basisBladeId < (1UL << (int) vSpaceDimension);
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
        public static bool IsValidBasisBladeIndex(this ulong basisBladeIndex, uint grade, uint vSpaceDimension)
        {
            if (grade > vSpaceDimension) 
                return false;

            var kvDim = vSpaceDimension.GetBinomialCoefficient(grade);

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
        public static bool IsValidBasisBladeGradeIndex(uint grade, ulong basisBladeIndex, uint vSpaceDimension)
        {
            if (grade > vSpaceDimension) 
                return false;

            var kvDim = vSpaceDimension.GetBinomialCoefficient(grade);

            return basisBladeIndex < kvDim;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidGrade(this uint grade)
        {
            return grade <= BasisBladeDataLookup.MaxVSpaceDimension;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidGrade(this uint grade, uint vSpaceDimension)
        {
            return grade <= vSpaceDimension;
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
        public static int GradeInvolutionSignOfGrade(this uint grade)
        {
            return (grade & 1) != 0 ? -1 : 1;
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
        public static int ReverseSignOfGrade(this uint grade)
        {
            return (grade % 4 > 1) ? -1 : 1;
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
            return (grade % 4) is 0 or 3;

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
            return (grade % 4) is 1 or 2;

            //return ((grade * (grade + 1)) & 2) != 0;
        }
        
        /// <summary>
        /// Test if the clifford conjugate of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +--+
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CliffordConjugateSignOfGrade(this uint grade)
        {
            return ((grade % 4) is 1 or 2) ? -1 : 1;
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
        public static int GradeInvolutionSignOfBasisBladeId(this ulong basisBladeId)
        {
            return BasisBladeDataLookup.GradeInvolutionSign(basisBladeId);
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
        public static int ReverseSignOfBasisBladeId(this ulong basisBladeId)
        {
            return BasisBladeDataLookup.ReverseSign(basisBladeId);
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
        public static int CliffordConjugateSignOfBasisBladeId(this ulong basisBladeId)
        {
            return BasisBladeDataLookup.CliffordConjugateSign(basisBladeId);
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
        /// <param name="vSpaceDimension"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong GetMaxBasisBladeId(this uint vSpaceDimension)
        {
            return (1ul << (int) vSpaceDimension) - 1ul;
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
