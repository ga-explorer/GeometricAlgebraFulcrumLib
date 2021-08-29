using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Utilities.Structures;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
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
        /// The number of basis vectors in a GA with dimension gaSpaceDimension
        /// </summary>
        /// <param name="gaSpaceDimension"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToVSpaceDimension(this int gaSpaceDimension)
        {
            return gaSpaceDimension.LastOneBitPosition();
        }

        /// <summary>
        /// The number of basis vectors in a GA with dimension gaSpaceDimension
        /// </summary>
        /// <param name="gaSpaceDimension"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToVSpaceDimension(this ulong gaSpaceDimension)
        {
            return gaSpaceDimension.LastOneBitPosition();
        }

        /// <summary>
        /// The number of grades in a GA space with a given dimension
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint GradesCount(this uint vSpaceDimension)
        {
            return vSpaceDimension + 1;
        }

        /// <summary>
        /// The dimension of bivectors subspace of a GA with a given dimension
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BivectorSpaceDimension(this uint vSpaceDimension)
        {
            return (vSpaceDimension * (vSpaceDimension - 1)) >> 1;
        }

        /// <summary>
        /// The dimension of k-vectors subspace of some grade of a GA with a given dimension
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong KVectorSpaceDimension(this uint vSpaceDimension, uint grade)
        {
            return grade switch
            {
                0U => 0UL,
                1U => vSpaceDimension,
                2U => (vSpaceDimension * (vSpaceDimension - 1)) >> 1,
                _ => vSpaceDimension.GetBinomialCoefficient(grade)
            };
        }

        /// <summary>
        /// The basis vector IDs of a GA with the given dimension
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisVectorIDs(this uint vSpaceDimension)
        {
            return vSpaceDimension.GetRange().Select(i => (1ul << (int) i));
        }

        /// <summary>
        /// The grades of k-vectors in a GA with the given dimension
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<uint> Grades(this uint vSpaceDimension)
        {
            return (vSpaceDimension + 1).GetRange();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong PseudoScalarId(this uint vSpaceDimension)
        {
            return ((1UL << (int) vSpaceDimension) - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong PseudoScalarIndex(this uint vSpaceDimension)
        {
            return ((1UL << (int) vSpaceDimension) - 1).BasisBladeIdToIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidVSpaceDimension(this int vSpaceDimension)
        {
            return vSpaceDimension >= 2 && vSpaceDimension < MaxVSpaceDimension;
        }

        /// <summary>
        /// Test if the given integer is a legal GA space dimension (i.e. positive power of 2 less than or 
        /// equal to 2 ^ MaxVSpaceDim)
        /// </summary>
        /// <param name="gaSpaceDimension"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidGaSpaceDimension(this ulong gaSpaceDimension)
        {
            return
                gaSpaceDimension == (MaxVSpaceBasisBladeId & gaSpaceDimension) &&
                gaSpaceDimension.CountOnes() == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidGaSpaceDimension(this int gaSpaceDimension)
        {
            return IsValidGaSpaceDimension((ulong) gaSpaceDimension);
        }


        /// <summary>
        /// The Basis blade IDs of a GA space with the given dimension
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <returns></returns>
        public static IEnumerable<ulong> BasisBladeIDs(this uint vSpaceDimension)
        {
            var maxBasisBladeId = vSpaceDimension.GetMaxBasisBladeId();

            for (var id = 0ul; id <= maxBasisBladeId; id++)
                yield return id;
        }

        /// <summary>
        /// Find all basis blade IDs with the given grade in a GA of dimension vSpaceDimension
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsOfGrade(this uint vSpaceDimension, uint grade)
        {
            return UInt64BitUtils.OnesPermutations(
                (int) vSpaceDimension, 
                (int) grade
            );
        }
        
        /// <summary>
        /// The basis blade IDs of a GA space with the given dimension sorted by their grade and index
        /// </summary>
        /// <param name="vSpaceDimension"></param>
        /// <param name="startGrade"></param>
        /// <returns></returns>
        public static IEnumerable<ulong> BasisBladeIDsSortedByGrade(this uint vSpaceDimension, uint startGrade = 0)
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
        public static IEnumerable<ulong> BasisBladeIDsOfGrades(this uint vSpaceDimension, IEnumerable<uint> gradesSeq)
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
        public static IEnumerable<ulong> BasisBladeIDsOfGrades(this uint vSpaceDimension, params uint[] gradesSeq)
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
        public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(this uint vSpaceDimension, uint startGrade = 0)
        {
            var result = new Dictionary<uint, IReadOnlyList<ulong>>();

            for (var grade = startGrade; grade <= vSpaceDimension; grade++)
            {
                result.Add(
                    grade, BasisBladeIDsOfGrade(vSpaceDimension, grade).ToArray()
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
        public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(this uint vSpaceDimension, IEnumerable<uint> gradesSeq)
        {
            var result = new Dictionary<uint, IReadOnlyList<ulong>>();

            foreach (var grade in gradesSeq)
            {
                result.Add(
                    grade, BasisBladeIDsOfGrade(vSpaceDimension, grade).ToArray()
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
        public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(this uint vSpaceDimension, params uint[] gradesSeq)
        {
            var result = new Dictionary<uint, IReadOnlyList<ulong>>();

            foreach (var grade in gradesSeq)
            {
                result.Add(
                    grade, BasisBladeIDsOfGrade(vSpaceDimension, grade).ToArray()
                );
            }

            return result;
        }


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
            return indexSeq.Select(index => index.BasisBladeIndexToId(grade));
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
            return "B" + index.BasisBladeIndexToId(grade).PatternToString((int) space.VSpaceDimension);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeGradeIndexName(ulong basisBladeId)
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
        public static IEnumerable<ulong> BasisBladeIDsContaining(this IGaSpace space, ulong basisBladeId)
        {
            return basisBladeId.GetSuperPatterns((int) space.VSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsContaining(this IGaSpace space, uint grade, ulong index)
        {
            return index
                .BasisBladeIndexToId(grade)
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

            var kvDim = KVectorSpaceDimension(space.VSpaceDimension, grade);

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
            return indexSeq.Select(index => index.BasisBladeIndexToId(grade));
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
            basisBladeId.BasisBladeIdToGradeIndex(out grade, out index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexRecord BasisBladeGradeIndex(this IGaSpace space, ulong basisBladeId)
        {
            if (basisBladeId < (ulong) GaLookupTables.IdToIndexTable.Length)
            {
                var grade = GaLookupTables.IdToGradeTable[basisBladeId];
                var index = GaLookupTables.IdToIndexTable[basisBladeId];

                return new GradeIndexRecord(grade, index);
            }
            else
            {
                basisBladeId.CombinadicPatternToIndex(out var grade, out var index);

                return new GradeIndexRecord((uint) grade, index);
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