using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra
{
    public static class GaSpaceUtils
    {
        /// <summary>
        /// The maximum allowed GA vector space dimension
        /// </summary>
        public static uint MaxVSpaceDimension { get; } 
            = 63;

        /// <summary>
        /// The maximum possible basis blade ID in the maximum allowed GA vector space dimension
        /// </summary>
        public static ulong MaxVSpaceBasisBladeId { get; } 
            = (1ul << (int) MaxVSpaceDimension) - 1ul;

        public static IReadOnlyList<string> DefaultBasisVectorsNames { get; } 
            = MaxVSpaceDimension.GetRange()
                .Select(i => "e" + i)
                .ToArray();

        /// <summary>
        /// The dimension of k-vectors subspace of some grade of this basis set
        /// </summary>
        /// <param name="space"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong KvSpaceDimension(this IGaSpace space, uint grade)
        {
            Debug.Assert(grade <= space.VSpaceDimension);

            return space.VSpaceDimension.GetBinomialCoefficient(grade);
        }
        
        /// <summary>
        /// The Basis blade IDs of this basis set
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ulong> BasisBladeIDs(this IGaSpace space)
        {
            for (var id = 0ul; id <= space.MaxBasisBladeId; id++)
                yield return id;
        }

        /// <summary>
        /// The basis vectors IDs of the given frame
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisVectorIDs(this IGaSpace space)
        {
            return space.VSpaceDimension
                .GetRange()
                .Select(i => (1ul << (int) i));
        }

        /// <summary>
        /// Find all basis blade IDs with the given grade and indexes in a given frame
        /// </summary>
        /// <param name="space"></param>
        /// <param name="grade"></param>
        /// <param name="indexSeq"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsOfGrade(this IGaSpace space, uint grade, IEnumerable<ulong> indexSeq)
        {
            return indexSeq.Select(index => GaBasisUtils.BasisBladeId(grade, index));
        }

        /// <summary>
        /// Find all basis blade IDs with the given grade in a given frame
        /// </summary>
        /// <param name="space"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsOfGrade(this IGaSpace space, uint grade)
        {
            return UInt64BitUtils.OnesPermutations(
                (int) space.VSpaceDimension, 
                (int) grade
            );
        }

        /// <summary>
        /// The basis blade IDs of the given frame sorted by their grade and index
        /// </summary>
        /// <param name="space"></param>
        /// <param name="startGrade"></param>
        /// <returns></returns>
        public static IEnumerable<ulong> BasisBladeIDsSortedByGrade(this IGaSpace space, uint startGrade = 0)
        {
            for (var grade = startGrade; grade <= space.VSpaceDimension; grade++)
                foreach (var id in space.BasisBladeIDsOfGrade(grade))
                    yield return id;
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades
        /// </summary>
        /// <param name="space"></param>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsOfGrades(this IGaSpace space, IEnumerable<uint> gradesSeq)
        {
            var vSpaceDimension = space.VSpaceDimension;

            return gradesSeq
                .OrderBy(g => g)
                .SelectMany(grade => 
                    UInt64BitUtils.OnesPermutations(
                        (int) vSpaceDimension, 
                        (int) grade
                    )
                );
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades
        /// </summary>
        /// <param name="space"></param>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsOfGrades(this IGaSpace space, params uint[] gradesSeq)
        {
            var vSpaceDimension = space.VSpaceDimension;

            return gradesSeq
                .OrderBy(g => g)
                .SelectMany(grade => 
                    UInt64BitUtils.OnesPermutations(
                        (int) vSpaceDimension, 
                        (int) grade
                    )
                );
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades grouped by their grade
        /// </summary>
        /// <param name="space"></param>
        /// <param name="startGrade"></param>
        /// <returns></returns>
        public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(this IGaSpace space, uint startGrade = 0)
        {
            var result = new Dictionary<uint, IReadOnlyList<ulong>>();

            for (var grade = startGrade; grade <= space.VSpaceDimension; grade++)
            {
                result.Add(
                    grade, 
                    space.BasisBladeIDsOfGrade(grade).ToArray()
                );
            }

            return result;
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades grouped by their grade
        /// </summary>
        /// <param name="space"></param>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(this IGaSpace space, IEnumerable<uint> gradesSeq)
        {
            var result = new Dictionary<uint, IReadOnlyList<ulong>>();

            foreach (var grade in gradesSeq)
            {
                result.Add(
                    grade, 
                    space.BasisBladeIDsOfGrade(grade).ToArray()
                );
            }

            return result;
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades grouped by their grade
        /// </summary>
        /// <param name="space"></param>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(this IGaSpace space, params uint[] gradesSeq)
        {
            var result = new Dictionary<uint, IReadOnlyList<ulong>>();

            foreach (var grade in gradesSeq)
            {
                result.Add(
                    grade, 
                    space.BasisBladeIDsOfGrade(grade).ToArray()
                );
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeBinaryIndexedName(this IGaSpace space, ulong basisBladeId)
        {
            return "B" + basisBladeId.PatternToString((int) space.VSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeBinaryIndexedName(this IGaSpace space, uint grade, ulong index)
        {
            return "B" + GaBasisUtils.BasisBladeId(grade, index).PatternToString((int) space.VSpaceDimension);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeGradeIndexName(ulong basisBladeId)
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
        public static IEnumerable<ulong> BasisBladeIDsContaining(this IGaSpace space, ulong basisBladeId)
        {
            return basisBladeId.GetSuperPatterns((int) space.VSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsContaining(this IGaSpace space, uint grade, ulong index)
        {
            return GaBasisUtils
                .BasisBladeId(grade, index)
                .GetSuperPatterns((int) space.VSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisVectorId(this IGaSpace space, ulong basisBladeId)
        {
            return space.IsValidBasisBladeId(basisBladeId) && 
                   basisBladeId.IsBasicPattern();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisVectorIndex(this IGaSpace space, ulong index)
        {
            return index < space.VSpaceDimension;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBladeId(this IGaSpace space, ulong basisBladeId)
        {
            return basisBladeId <= space.MaxBasisBladeId;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBladeGradeIndex(this IGaSpace space, uint grade, ulong index)
        {
            if (grade > space.VSpaceDimension) return false;

            var kvDim = GaBasisUtils.KvSpaceDimension(space.VSpaceDimension, grade);

            return index < kvDim;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidGrade(this IGaSpace space, uint grade)
        {
            return (grade <= space.VSpaceDimension);
        }

        /// <summary>
        /// Get all possible grades of the meet of two blades grades
        /// </summary>
        /// <param name="space"></param>
        /// <param name="grade1"></param>
        /// <param name="grade2"></param>
        /// <returns></returns>
        public static IEnumerable<int> GradesOfMeet(this IGaSpace space, int grade1, int grade2)
        {
            if (grade1 > space.VSpaceDimension || grade2 > space.VSpaceDimension || grade1 < 0 || grade2 < 0)
                yield break;

            var maxGrade = Math.Min(grade1, grade2);

            //TODO: Should this be grade++ instead of grade += 2 ?
            for (var grade = 0; grade <= maxGrade; grade += 2)
                yield return grade;
        }

        /// <summary>
        /// Get all possible grades of the meet of two blades grades
        /// </summary>
        /// <param name="space"></param>
        /// <param name="grade1"></param>
        /// <param name="grade2"></param>
        /// <returns></returns>
        public static IEnumerable<int> GradesOfJoin(this IGaSpace space, int grade1, int grade2)
        {
            if (grade1 > space.VSpaceDimension || grade2 > space.VSpaceDimension || grade1 < 0 || grade2 < 0)
                yield break;

            var minGrade = Math.Max(grade1, grade2);
            var maxGrade = Math.Min(space.VSpaceDimension, grade1 + grade2);

            //TODO: Should this be grade++ instead of grade += 2 ?
            for (var grade = minGrade; grade <= maxGrade; grade += 2)
                yield return grade;
        }

        /// <summary>
        /// Return a list of all possible grades in the geometric product of two k-vectors with
        /// the given grades
        /// </summary>
        /// <param name="space"></param>
        /// <param name="grade1"></param>
        /// <param name="grade2"></param>
        /// <returns></returns>
        public static IEnumerable<uint> GradesOfEGp(this IGaSpace space, uint grade1, uint grade2)
        {
            if (grade1 > space.VSpaceDimension || grade2 > space.VSpaceDimension)
                yield break;

            var minGrade = (uint) Math.Abs(grade1 - grade2);
            var maxGrade = Math.Min(space.VSpaceDimension, grade1 + grade2);

            for (var grade = minGrade; grade <= maxGrade; grade += 2)
                yield return grade;
        }

        /// <summary>
        /// Given a bit pattern in id1 and id2 this shifts id2 by space.VSpaceDimension bits to the left and 
        /// appends id1 to combine the two patterns using an OR bitwise operation
        /// </summary>
        /// <param name="space"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong JoinIDs(this IGaSpace space, ulong id1, ulong id2)
        {
            return id1 | (id2 << (int) space.VSpaceDimension);
        }

        /// <summary>
        /// Find all basis blade IDs with the given grade and indexes in a given frame
        /// </summary>
        /// <param name="space"></param>
        /// <param name="grade"></param>
        /// <param name="indexSeq"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsOfGradeIndex(this IGaSpace space, uint grade, params ulong[] indexSeq)
        {
            return indexSeq.Select(index => GaBasisUtils.BasisBladeId(grade, index));
        }

        /// <summary>
        /// Find the ID of a basis blade given its grade and index
        /// </summary>
        /// <param name="space"></param>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBladeId(this IGaSpace space, uint grade, ulong index)
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
        /// Find the ID of a basis vector given its index
        /// </summary>
        /// <param name="space"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisVectorId(this IGaSpace space, ulong index)
        {
            return 1UL << (int) index;
        }

        /// <summary>
        /// Find the grade of a basis blade given its ID
        /// </summary>
        /// <param name="space"></param>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint BasisBladeGrade(this IGaSpace space, ulong basisBladeId)
        {
            return basisBladeId < (ulong) GaLookupTables.IdToGradeTable.Length
                ? GaLookupTables.IdToGradeTable[basisBladeId]
                : (uint) basisBladeId.CountOnes();
        }

        /// <summary>
        /// Find the index of a basis blade given its ID
        /// </summary>
        /// <param name="space"></param>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBladeIndex(this IGaSpace space, ulong basisBladeId)
        {
            return basisBladeId < (ulong) GaLookupTables.IdToIndexTable.Length
                ? GaLookupTables.IdToIndexTable[basisBladeId]
                : basisBladeId.CombinadicPatternToIndex();
        }

        /// <summary>
        /// Find the grade and index of a basis blade given its ID
        /// </summary>
        /// <param name="space"></param>
        /// <param name="basisBladeId"></param>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BasisBladeGradeIndex(this IGaSpace space, ulong basisBladeId, out uint grade, out ulong index)
        {
            basisBladeId.BasisBladeGradeIndex(out grade, out index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<uint, ulong> BasisBladeGradeIndex(this IGaSpace space, ulong basisBladeId)
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
        public static string BasisBladeIndexedName(this IGaSpace space, ulong basisBladeId)
        {
            return "E" + basisBladeId;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeIndexedName(this IGaSpace space, uint grade, ulong index)
        {
            return "E" + space.BasisBladeId(grade, index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeGradeIndexName(this IGaSpace space, uint grade, ulong index)
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
        public static IEnumerable<ulong> BasisVectorIDsInside(this IGaSpace space, ulong basisBladeId)
        {
            return basisBladeId.GetBasicPatterns();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisVectorIDsInside(this IGaSpace space, uint grade, ulong index)
        {
            return space
                .BasisBladeId(grade, index)
                .GetBasicPatterns();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisVectorIndexesInside(this IGaSpace space, ulong basisBladeId)
        {
            return basisBladeId
                .PatternToPositions()
                .Select(i => (ulong)i);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisVectorIndexesInside(this IGaSpace space, uint grade, ulong index)
        {
            return space
                .BasisBladeId(grade, index)
                .PatternToPositions()
                .Select(i => (ulong)i);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsInside(this IGaSpace space, ulong basisBladeId)
        {
            return basisBladeId.GetSubPatterns();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsInside(this IGaSpace space, uint grade, ulong index)
        {
            return space
                .BasisBladeId(grade, index)
                .GetSubPatterns();
        }

        /// <summary>
        /// Test if the clifford conjugate of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +--+
        /// </summary>
        /// <param name="space"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GradeHasNegativeCliffordConjugate(this IGaSpace space, uint grade)
        {
            var v = grade % 4;
            return v == 1 || v == 2;

            //return ((grade * (grade + 1)) & 2) != 0;
        }

        /// <summary>
        /// Test if the grade inverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +-+-
        /// </summary>
        /// <param name="space"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GradeHasNegativeGradeInvolution(this IGaSpace space, uint grade)
        {
            return (grade & 1) != 0;
        }

        /// <summary>
        /// Test if the reverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: ++--
        /// </summary>
        /// <param name="space"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GradeHasNegativeReverse(this IGaSpace space, uint grade)
        {
            return grade % 4 > 1;

            //return ((grade * (grade - 1)) & 2) != 0;
        }

        /// <summary>
        /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
        /// </summary>
        /// <param name="space"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BasisBladeIdHasNegativeCliffordConjugate(this IGaSpace space, ulong id)
        {
            return id < (ulong)GaLookupTables.IsNegativeCliffConjTable.Length
                ? GaLookupTables.IsNegativeCliffConjTable.Get((int)id)
                : ((uint) id.CountOnes()).GradeHasNegativeCliffordConjugate();
        }

        /// <summary>
        /// Test if the grade inverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="space"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BasisBladeIdHasNegativeGradeInvolution(this IGaSpace space, ulong id)
        {
            return id < (ulong)GaLookupTables.IsNegativeGradeInvTable.Length
                ? GaLookupTables.IsNegativeGradeInvTable.Get((int)id)
                : ((uint) id.CountOnes()).GradeHasNegativeGradeInvolution();
        }

        /// <summary>
        /// Test if the reverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="space"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BasisBladeIdHasNegativeReverse(this IGaSpace space, ulong id)
        {
            return id < (ulong)GaLookupTables.IsNegativeReverseTable.Length
                ? GaLookupTables.IsNegativeReverseTable.Get((int)id)
                : ((uint) id.CountOnes()).GradeHasNegativeReverse();
        }
    }
}