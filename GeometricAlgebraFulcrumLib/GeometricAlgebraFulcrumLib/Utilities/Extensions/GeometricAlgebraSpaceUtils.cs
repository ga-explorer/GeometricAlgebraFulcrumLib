using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GeometricAlgebraSpaceUtils
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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BivectorSpaceDimension(this IGeometricAlgebraSpace space)
        {
            var n = (ulong) space.VSpaceDimension;

            return n * (n - 1) / 2UL;
        }
        
        /// <summary>
        /// The dimension of k-vectors subspace of some grade of this basis set
        /// </summary>
        /// <param name="space"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong KVectorSpaceDimension(this IGeometricAlgebraSpace space, uint grade)
        {
            Debug.Assert(grade <= space.VSpaceDimension);

            var n = (ulong) space.VSpaceDimension;

            return grade switch
            {
                0 => 1,
                1 => n,
                2 => n * (n - 1) / 2UL,
                _ => space.VSpaceDimension.GetBinomialCoefficient(grade)
            };
        }
        
        /// <summary>
        /// The Basis blade IDs of this basis set
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ulong> BasisBladeIDs(this IGeometricAlgebraSpace space)
        {
            for (var id = 0ul; id <= space.MaxBasisBladeId; id++)
                yield return id;
        }

        /// <summary>
        /// The basis vectors IDs of the given frame
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisVectorIDs(this IGeometricAlgebraSpace space)
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
        public static IEnumerable<ulong> BasisBladeIDsOfGrade(this IGeometricAlgebraSpace space, uint grade, IEnumerable<ulong> indexSeq)
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
        public static IEnumerable<ulong> BasisBladeIDsOfGrade(this IGeometricAlgebraSpace space, uint grade)
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
        public static IEnumerable<ulong> BasisBladeIDsSortedByGrade(this IGeometricAlgebraSpace space, uint startGrade = 0)
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
        public static IEnumerable<ulong> BasisBladeIDsOfGrades(this IGeometricAlgebraSpace space, IEnumerable<uint> gradesSeq)
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
        public static IEnumerable<ulong> BasisBladeIDsOfGrades(this IGeometricAlgebraSpace space, params uint[] gradesSeq)
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
        public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(this IGeometricAlgebraSpace space, uint startGrade = 0)
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
        public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(this IGeometricAlgebraSpace space, IEnumerable<uint> gradesSeq)
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
        public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(this IGeometricAlgebraSpace space, params uint[] gradesSeq)
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
        public static string BasisBladeBinaryIndexedName(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return "B" + basisBladeId.PatternToString((int) space.VSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeBinaryIndexedName(this IGeometricAlgebraSpace space, uint grade, ulong index)
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
        public static IEnumerable<ulong> BasisBladeIDsContaining(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return basisBladeId.GetSuperPatterns((int) space.VSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsContaining(this IGeometricAlgebraSpace space, uint grade, ulong index)
        {
            return index
                .BasisBladeIndexToId(grade)
                .GetSuperPatterns((int) space.VSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisVectorId(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return space.IsValidBasisBladeId(basisBladeId) && 
                   basisBladeId.IsBasicPattern();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisVectorIndex(this IGeometricAlgebraSpace space, ulong index)
        {
            return index < space.VSpaceDimension;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return basisBladeId <= space.MaxBasisBladeId;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidBasisBladeGradeIndex(this IGeometricAlgebraSpace space, uint grade, ulong index)
        {
            if (grade > space.VSpaceDimension) return false;

            var kvDim = KVectorSpaceDimension(space.VSpaceDimension, grade);

            return index < kvDim;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidGrade(this IGeometricAlgebraSpace space, uint grade)
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
        public static IEnumerable<int> GradesOfMeet(this IGeometricAlgebraSpace space, int grade1, int grade2)
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
        public static IEnumerable<int> GradesOfJoin(this IGeometricAlgebraSpace space, int grade1, int grade2)
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
        public static IEnumerable<uint> GradesOfEGp(this IGeometricAlgebraSpace space, uint grade1, uint grade2)
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
        public static ulong JoinIDs(this IGeometricAlgebraSpace space, ulong id1, ulong id2)
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
        public static IEnumerable<ulong> BasisBladeIDsOfGradeIndex(this IGeometricAlgebraSpace space, uint grade, params ulong[] indexSeq)
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
        public static ulong BasisBladeId(this IGeometricAlgebraSpace space, uint grade, ulong index)
        {
            return BasisBladeDataLookup.BasisBladeId(grade, index);
        }

        /// <summary>
        /// Find the ID of a basis vector given its index
        /// </summary>
        /// <param name="space"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisVectorId(this IGeometricAlgebraSpace space, ulong index)
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
        public static uint BasisBladeGrade(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return BasisBladeDataLookup.BasisBladeGrade(basisBladeId);
        }

        /// <summary>
        /// Find the index of a basis blade given its ID
        /// </summary>
        /// <param name="space"></param>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong BasisBladeIndex(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return BasisBladeDataLookup.BasisBladeIndex(basisBladeId);
        }

        /// <summary>
        /// Find the grade and index of a basis blade given its ID
        /// </summary>
        /// <param name="space"></param>
        /// <param name="basisBladeId"></param>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BasisBladeGradeIndex(this IGeometricAlgebraSpace space, ulong basisBladeId, out uint grade, out ulong index)
        {
            basisBladeId.BasisBladeIdToGradeIndex(out grade, out index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GradeIndexRecord BasisBladeGradeIndex(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return BasisBladeDataLookup.BasisBladeGradeIndex(basisBladeId);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeIndexedName(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return "E" + basisBladeId;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeIndexedName(this IGeometricAlgebraSpace space, uint grade, ulong index)
        {
            return "E" + space.BasisBladeId(grade, index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string BasisBladeGradeIndexName(this IGeometricAlgebraSpace space, uint grade, ulong index)
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
        public static IEnumerable<ulong> BasisVectorIDsInside(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return basisBladeId.GetBasicPatterns();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisVectorIDsInside(this IGeometricAlgebraSpace space, uint grade, ulong index)
        {
            return space
                .BasisBladeId(grade, index)
                .GetBasicPatterns();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisVectorIndexesInside(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return basisBladeId
                .PatternToPositions()
                .Select(i => (ulong)i);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisVectorIndexesInside(this IGeometricAlgebraSpace space, uint grade, ulong index)
        {
            return space
                .BasisBladeId(grade, index)
                .PatternToPositions()
                .Select(i => (ulong)i);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsInside(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return basisBladeId.GetSubPatterns();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ulong> BasisBladeIDsInside(this IGeometricAlgebraSpace space, uint grade, ulong index)
        {
            return space
                .BasisBladeId(grade, index)
                .GetSubPatterns();
        }


        /// <summary>
        /// Test if the grade inverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="space"></param>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GradeInvolutionIsPositiveOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return BasisBladeDataLookup.GradeInvolutionIsPositive(basisBladeId);
        }

        /// <summary>
        /// Test if the grade inverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="space"></param>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GradeInvolutionIsNegativeOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return BasisBladeDataLookup.GradeInvolutionIsNegative(basisBladeId);
        }

        /// <summary>
        /// Test if the grade inverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="space"></param>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GradeInvolutionSignOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return BasisBladeDataLookup.GradeInvolutionSign(basisBladeId);
        }

        /// <summary>
        /// Test if the reverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="space"></param>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReverseIsPositiveOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return BasisBladeDataLookup.ReverseIsPositive(basisBladeId);
        }

        /// <summary>
        /// Test if the reverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="space"></param>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReverseIsNegativeOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return BasisBladeDataLookup.ReverseIsNegative(basisBladeId);
        }

        /// <summary>
        /// Test if the reverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="space"></param>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReverseSignOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return BasisBladeDataLookup.ReverseSign(basisBladeId);
        }

        /// <summary>
        /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
        /// </summary>
        /// <param name="space"></param>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CliffordConjugateIsPositiveOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return BasisBladeDataLookup.CliffordConjugateIsPositive(basisBladeId);
        }

        /// <summary>
        /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
        /// </summary>
        /// <param name="space"></param>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CliffordConjugateIsNegativeOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return BasisBladeDataLookup.CliffordConjugateIsNegative(basisBladeId);
        }

        /// <summary>
        /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
        /// </summary>
        /// <param name="space"></param>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CliffordConjugateSignOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
        {
            return BasisBladeDataLookup.CliffordConjugateSign(basisBladeId);
        }

    }
}