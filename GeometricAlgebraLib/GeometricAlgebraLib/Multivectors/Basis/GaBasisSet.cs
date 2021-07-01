using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DataStructuresLib;
using DataStructuresLib.Combinations;

namespace GeometricAlgebraLib.Multivectors.Basis
{
    public readonly struct GaBasisSet
    {
        public int VSpaceDimension { get; }

        public ulong GaSpaceDimension { get; }

        public ulong MaxBasisBladeId { get; }

        public int GradesCount { get; }

        public IEnumerable<int> Grades 
            => Enumerable.Range(0, GradesCount);


        public GaBasisSet(int vSpaceDimension)
        {
            if (vSpaceDimension < 0 || vSpaceDimension > GaBasisUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            VSpaceDimension = vSpaceDimension;
            GaSpaceDimension = 1UL << VSpaceDimension;
            MaxBasisBladeId = GaSpaceDimension - 1;
            GradesCount = VSpaceDimension + 1;
        }


        /// <summary>
        /// The dimension of k-vectors subspace of some grade of this basis set
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public ulong KvSpaceDimension(int grade)
        {
            Debug.Assert(grade >= 0 && grade <= VSpaceDimension);

            return VSpaceDimension.GetBinomialCoefficient(grade);
        }
        
        /// <summary>
        /// The Basis blade IDs of this basis set
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ulong> BasisBladeIDs()
        {
            for (var id = 0ul; id <= MaxBasisBladeId; id++)
                yield return id;
        }

        /// <summary>
        /// The basis vectors IDs of the given frame
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ulong> BasisVectorIDs()
        {
            return Enumerable
                .Range(0, VSpaceDimension)
                .Select(i => (1ul << i));
        }
        
        /// <summary>
        /// Find all basis blade IDs with the given grade and indexes in a given frame
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexSeq"></param>
        /// <returns></returns>
        public IEnumerable<ulong> BasisBladeIDsOfGrade(int grade, IEnumerable<ulong> indexSeq)
        {
            return indexSeq.Select(index => GaBasisUtils.BasisBladeId(grade, index));
        }

        /// <summary>
        /// Find all basis blade IDs with the given grade in a given frame
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public IEnumerable<ulong> BasisBladeIDsOfGrade(int grade)
        {
            return UnsignedLongBitUtils.OnesPermutations(
                VSpaceDimension, 
                grade
            );
        }

        /// <summary>
        /// The basis blade IDs of the given frame sorted by their grade and index
        /// </summary>
        /// <param name="startGrade"></param>
        /// <returns></returns>
        public IEnumerable<ulong> BasisBladeIDsSortedByGrade(int startGrade = 0)
        {
            for (var grade = startGrade; grade <= VSpaceDimension; grade++)
                foreach (var id in BasisBladeIDsOfGrade(grade))
                    yield return id;
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades
        /// </summary>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public IEnumerable<ulong> BasisBladeIDsOfGrades(IEnumerable<int> gradesSeq)
        {
            var vSpaceDimension = VSpaceDimension;

            return gradesSeq
                .OrderBy(g => g)
                .SelectMany(grade => 
                    UnsignedLongBitUtils.OnesPermutations(
                        vSpaceDimension, 
                        grade
                    )
                );
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades
        /// </summary>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public IEnumerable<ulong> BasisBladeIDsOfGrades(params int[] gradesSeq)
        {
            var vSpaceDimension = VSpaceDimension;

            return gradesSeq
                .OrderBy(g => g)
                .SelectMany(grade => 
                    UnsignedLongBitUtils.OnesPermutations(
                        vSpaceDimension, 
                        grade
                    )
                );
        }
        
        /// <summary>
        /// Returns the basis blade IDs of having the given grades grouped by their grade
        /// </summary>
        /// <param name="startGrade"></param>
        /// <returns></returns>
        public Dictionary<int, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(int startGrade = 0)
        {
            var result = new Dictionary<int, IReadOnlyList<ulong>>();

            for (var grade = startGrade; grade <= VSpaceDimension; grade++)
            {
                result.Add(
                    grade, 
                    BasisBladeIDsOfGrade(grade).ToArray()
                );
            }

            return result;
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades grouped by their grade
        /// </summary>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public Dictionary<int, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(IEnumerable<int> gradesSeq)
        {
            var result = new Dictionary<int, IReadOnlyList<ulong>>();

            foreach (var grade in gradesSeq)
            {
                result.Add(
                    grade, 
                    BasisBladeIDsOfGrade(grade).ToArray()
                );
            }

            return result;
        }

        /// <summary>
        /// Returns the basis blade IDs of having the given grades grouped by their grade
        /// </summary>
        /// <param name="gradesSeq"></param>
        /// <returns></returns>
        public Dictionary<int, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(params int[] gradesSeq)
        {
            var result = new Dictionary<int, IReadOnlyList<ulong>>();

            foreach (var grade in gradesSeq)
            {
                result.Add(
                    grade, 
                    BasisBladeIDsOfGrade(grade).ToArray()
                );
            }

            return result;
        }

        public string BasisBladeBinaryIndexedName(ulong basisBladeId)
        {
            return "B" + basisBladeId.PatternToString(VSpaceDimension);
        }

        public string BasisBladeBinaryIndexedName(int grade, ulong index)
        {
            return "B" + GaBasisUtils.BasisBladeId(grade, index).PatternToString(VSpaceDimension);
        }
        
        public string BasisBladeGradeIndexName(ulong basisBladeId)
        {
            return
                new StringBuilder(32)
                    .Append('G')
                    .Append(basisBladeId.BasisBladeGrade())
                    .Append('I')
                    .Append(basisBladeId.BasisBladeIndex())
                    .ToString();
        }

        public IEnumerable<ulong> BasisBladeIDsContaining(ulong basisBladeId)
        {
            return basisBladeId.GetSuperPatterns(VSpaceDimension);
        }

        public IEnumerable<ulong> BasisBladeIDsContaining(int grade, ulong index)
        {
            return GaBasisUtils.BasisBladeId(grade, index).GetSuperPatterns(VSpaceDimension);
        }

        public bool IsValidBasisVectorId(ulong basisBladeId)
        {
            return IsValidBasisBladeId(basisBladeId) && basisBladeId.IsBasicPattern();
        }

        public bool IsValidBasisVectorIndex(ulong index)
        {
            return index < (ulong) VSpaceDimension;
        }

        public bool IsValidBasisBladeId(ulong basisBladeId)
        {
            return basisBladeId <= MaxBasisBladeId;
        }

        public bool IsValidBasisBladeGradeIndex(int grade, ulong index)
        {
            if (grade < 0 || grade > VSpaceDimension) return false;

            var kvDim = GaBasisUtils.KvSpaceDimension(VSpaceDimension, grade);

            return index < kvDim;
        }

        public bool IsValidGrade(int grade)
        {
            return (grade >= 0 && grade <= VSpaceDimension);
        }

        /// <summary>
        /// Get all possible grades of the meet of two blades grades
        /// </summary>
        /// <param name="grade1"></param>
        /// <param name="grade2"></param>
        /// <returns></returns>
        public IEnumerable<int> GradesOfMeet(int grade1, int grade2)
        {
            if (grade1 > VSpaceDimension || grade2 > VSpaceDimension || grade1 < 0 || grade2 < 0)
                yield break;

            var maxGrade = Math.Min(grade1, grade2);

            //TODO: Should this be grade++ instead of grade += 2 ?
            for (var grade = 0; grade <= maxGrade; grade += 2)
                yield return grade;
        }

        /// <summary>
        /// Get all possible grades of the meet of two blades grades
        /// </summary>
        /// <param name="grade1"></param>
        /// <param name="grade2"></param>
        /// <returns></returns>
        public IEnumerable<int> GradesOfJoin(int grade1, int grade2)
        {
            if (grade1 > VSpaceDimension || grade2 > VSpaceDimension || grade1 < 0 || grade2 < 0)
                yield break;

            var minGrade = Math.Max(grade1, grade2);
            var maxGrade = Math.Min(VSpaceDimension, grade1 + grade2);

            //TODO: Should this be grade++ instead of grade += 2 ?
            for (var grade = minGrade; grade <= maxGrade; grade += 2)
                yield return grade;
        }

        /// <summary>
        /// Return a list of all possible grades in the geometric product of two k-vectors with
        /// the given grades
        /// </summary>
        /// <param name="grade1"></param>
        /// <param name="grade2"></param>
        /// <returns></returns>
        public IEnumerable<int> GradesOfEGp(int grade1, int grade2)
        {
            if (grade1 > VSpaceDimension || grade2 > VSpaceDimension || grade1 < 0 || grade2 < 0)
                yield break;

            var minGrade = Math.Abs(grade1 - grade2);
            var maxGrade = Math.Min(VSpaceDimension, grade1 + grade2);

            for (var grade = minGrade; grade <= maxGrade; grade += 2)
                yield return grade;
        }
 
        /// <summary>
        /// Given a bit pattern in id1 and id2 this shifts id2 by VSpaceDimension bits to the left and 
        /// appends id1 to combine the two patterns using an OR bitwise operation
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public ulong JoinIDs(ulong id1, ulong id2)
        {
            return id1 | (id2 << VSpaceDimension);
        }

        /// <summary>
        /// Find all basis blade IDs with the given grade and indexes in a given frame
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="indexSeq"></param>
        /// <returns></returns>
        public IEnumerable<ulong> BasisBladeIDsOfGradeIndex(int grade, params ulong[] indexSeq)
        {
            return indexSeq.Select(index => GaBasisUtils.BasisBladeId(grade, index));
        }
        
        /// <summary>
        /// Find the ID of a basis blade given its grade and index
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public ulong BasisBladeId(int grade, ulong index)
        {
            if (grade < GaLookupTables.GradeIndexToIdTable.Count)
            {
                var table = GaLookupTables.GradeIndexToIdTable[grade];

                if (index < (ulong) table.Length)
                    return table[index];
            }

            return index.IndexToCombinadicPattern(grade);
        }
        
        /// <summary>
        /// Find the ID of a basis vector given its index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ulong BasisVectorId(ulong index)
        {
            return 1UL << (int) index;
        }
        
        /// <summary>
        /// Find the grade of a basis blade given its ID
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        public int BasisBladeGrade(ulong basisBladeId)
        {
            return basisBladeId < (ulong) GaLookupTables.IdToGradeTable.Length
                ? GaLookupTables.IdToGradeTable[basisBladeId]
                : basisBladeId.CountOnes();
        }
        
        /// <summary>
        /// Find the index of a basis blade given its ID
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <returns></returns>
        public ulong BasisBladeIndex(ulong basisBladeId)
        {
            return basisBladeId < (ulong) GaLookupTables.IdToIndexTable.Length
                ? GaLookupTables.IdToIndexTable[basisBladeId]
                : basisBladeId.CombinadicPatternToIndex();
        }
        
        /// <summary>
        /// Find the grade and index of a basis blade given its ID
        /// </summary>
        /// <param name="basisBladeId"></param>
        /// <param name="grade"></param>
        /// <param name="index"></param>
        public void BasisBladeGradeIndex(ulong basisBladeId, out int grade, out ulong index)
        {
            basisBladeId.BasisBladeGradeIndex(out grade, out index);
        }
        
        public Tuple<int, ulong> BasisBladeGradeIndex(ulong basisBladeId)
        {
            if (basisBladeId < (ulong) GaLookupTables.IdToIndexTable.Length)
            {
                var grade = GaLookupTables.IdToGradeTable[basisBladeId];
                var index = GaLookupTables.IdToIndexTable[basisBladeId];

                return new Tuple<int, ulong>(grade, index);
            }
            else
            {
                basisBladeId.CombinadicPatternToIndex(out var grade, out var index);

                return new Tuple<int, ulong>(grade, index);
            }
        }
        
        public string BasisBladeIndexedName(ulong basisBladeId)
        {
            return "E" + basisBladeId;
        }

        public string BasisBladeIndexedName(int grade, ulong index)
        {
            return "E" + BasisBladeId(grade, index);
        }
        
        public string BasisBladeGradeIndexName(int grade, ulong index)
        {
            return
                new StringBuilder(32)
                    .Append('G')
                    .Append(grade)
                    .Append('I')
                    .Append(index)
                    .ToString();
        }

        public IEnumerable<ulong> BasisVectorIDsInside(ulong basisBladeId)
        {
            return basisBladeId.GetBasicPatterns();
        }
        
        public IEnumerable<ulong> BasisVectorIDsInside(int grade, ulong index)
        {
            return BasisBladeId(grade, index).GetBasicPatterns();
        }
        
        public IEnumerable<ulong> BasisVectorIndexesInside(ulong basisBladeId)
        {
            return basisBladeId.PatternToPositions().Select(i => (ulong)i);
        }
        
        public IEnumerable<ulong> BasisVectorIndexesInside(int grade, ulong index)
        {
            return BasisBladeId(grade, index).PatternToPositions().Select(i => (ulong)i);
        }
        
        public IEnumerable<ulong> BasisBladeIDsInside(ulong basisBladeId)
        {
            return basisBladeId.GetSubPatterns();
        }
        
        public IEnumerable<ulong> BasisBladeIDsInside(int grade, ulong index)
        {
            return BasisBladeId(grade, index).GetSubPatterns();
        }

        /// <summary>
        /// Test if the clifford conjugate of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: +--+
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public bool GradeHasNegativeCliffordConjugate(int grade)
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
        public bool GradeHasNegativeGradeInvolution(int grade)
        {
            return (grade & 1) != 0;
        }
        
        /// <summary>
        /// Test if the reverse of a basis blade with a given grade is -1 the original basis blade
        /// Sign Pattern: ++--
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public bool GradeHasNegativeReverse(int grade)
        {
            return grade % 4 > 1;

            //return ((grade * (grade - 1)) & 2) != 0;
        }
        
        /// <summary>
        /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool BasisBladeIdHasNegativeCliffordConjugate(ulong id)
        {
            return id < (ulong)GaLookupTables.IsNegativeCliffConjTable.Length
                ? GaLookupTables.IsNegativeCliffConjTable.Get((int)id)
                : id.CountOnes().GradeHasNegativeCliffordConjugate();
        }
        
        /// <summary>
        /// Test if the grade inverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool BasisBladeIdHasNegativeGradeInvolution(ulong id)
        {
            return id < (ulong)GaLookupTables.IsNegativeGradeInvTable.Length
                ? GaLookupTables.IsNegativeGradeInvTable.Get((int)id)
                : id.CountOnes().GradeHasNegativeGradeInvolution();
        }
        
        /// <summary>
        /// Test if the reverse of a given basis blade is -1 the original basis blade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool BasisBladeIdHasNegativeReverse(ulong id)
        {
            return id < (ulong)GaLookupTables.IsNegativeReverseTable.Length
                ? GaLookupTables.IsNegativeReverseTable.Get((int)id)
                : id.CountOnes().GradeHasNegativeReverse();
        }
    }
}