using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using GeometricAlgebraFulcrumLib.Core.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Core.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;

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
    public static ulong GaSpaceDimensions(this int vSpaceDimensions)
    {
        Debug.Assert(
            vSpaceDimensions is >= 0 and < 64
        );

        return 1UL << vSpaceDimensions;
    }
       
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GaSpaceDimensions(this uint vSpaceDimensions)
    {
        Debug.Assert(
            vSpaceDimensions < 64
        );

        return 1UL << (int) vSpaceDimensions;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong KVectorSpaceDimensions(this int vSpaceDimensions, int grade)
    {
        Debug.Assert(
            vSpaceDimensions >= 0 && 
            grade >= 0 && grade <= vSpaceDimensions
        );

        return vSpaceDimensions.GetBinomialCoefficient(grade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong KVectorSpaceDimensions(this uint vSpaceDimensions, uint grade)
    {
        Debug.Assert(
            vSpaceDimensions > 0 && 
            grade <= vSpaceDimensions
        );

        return vSpaceDimensions.GetBinomialCoefficient(grade);
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
    public static int Grade(this IndexSet id)
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
    public static int VSpaceDimensions(this IndexSet id)
    {
        return id.IsEmptySet 
            ? 0 : id.LastIndex + 1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong KvSpaceDimensions(this IndexSet id)
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
    public static bool IsBasisScalar(this IndexSet id)
    {
        return id.IsEmptySet;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBasisVector(this ulong id)
    {
        return BitOperations.IsPow2(id);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBasisVector(this IndexSet id)
    {
        return id.Count == 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBasisBivector(this ulong id)
    {
        return BitOperations.PopCount(id) == 2;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBasisBivector(this IndexSet id)
    {
        return id.Count == 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBasisKVector(this ulong id, int grade)
    {
        return BitOperations.PopCount(id) == grade;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBasisKVector(this IndexSet id, int grade)
    {
        return id.Count == grade;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IndexSet> GetBasisBladeIDsOfGrade(this int vSpaceDimensions, uint grade)
    {
        return vSpaceDimensions.ToDenseIndexSet().GetSubsetsOfSize((int) grade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IndexSet> GetBasisBladeIDsOfGrades(this int vSpaceDimensions, params int[] gradeList)
    {
        var indexSet = vSpaceDimensions.ToDenseIndexSet();

        return gradeList.SelectMany(
            grade => indexSet.GetSubsetsOfSize(grade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IndexSet> GetBasisBladeIDsOfGrades(this int vSpaceDimensions, params uint[] gradeList)
    {
        var indexSet = vSpaceDimensions.ToDenseIndexSet();

        return gradeList.SelectMany(
            grade => indexSet.GetSubsetsOfSize((int) grade)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IndexSet> GetBasisBladeIDsOfGrades(this int vSpaceDimensions, IEnumerable<int> gradeList)
    {
        var indexSet = vSpaceDimensions.ToDenseIndexSet();

        return gradeList.SelectMany(
            grade => indexSet.GetSubsetsOfSize(grade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IndexSet> GetBasisBladeIDsOfGrades(this int vSpaceDimensions, IEnumerable<uint> gradeList)
    {
        var indexSet = vSpaceDimensions.ToDenseIndexSet();

        return gradeList.SelectMany(
            grade => indexSet.GetSubsetsOfSize((int) grade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ulong> GetBasisBladeIndices(this int vSpaceDimensions, uint grade)
    {
        var maxBasisBladeId = 1UL << vSpaceDimensions;
        var index = 0UL;
        var id = index.BasisBladeIndexToId(grade).ToUInt64();

        while (id <= maxBasisBladeId)
        {
            yield return index;

            index++;
            id = index.BasisBladeIndexToId(grade).ToUInt64();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint BasisBladeIdToMinVSpaceDimension(this IndexSet basisBladeId)
    {
        return (uint) (1 + basisBladeId.LastIndex);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint BasisBladeIndexToMinVSpaceDimension(this ulong basisBladeIndex, uint grade)
    {
        return grade switch
        {
            0U => basisBladeIndex == 0UL ? 0U : throw new InvalidOperationException(),
            1U => 1U + (uint) basisBladeIndex,
            2U => 1U + (uint) (0.5d * (1d + Math.Sqrt(1UL + 8UL * basisBladeIndex))),
            _ => (uint) (1 + basisBladeIndex.BasisBladeIndexToId(grade).LastIndex)
        };
    }
        
    /// <summary>
    /// Find the ID of a basis blade given its grade and basisBladeIndex
    /// </summary>
    /// <param name="grade"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet BasisBladeGradeIndexToId(int grade, ulong index)
    {
        return grade switch
        {
            < 0 => throw new InvalidOperationException(),
            0 => index == 0 ? IndexSet.EmptySet : throw new ArgumentOutOfRangeException(nameof(index)),
            1 => index.BasisVectorIndexToId(),
            2 => index.BasisBivectorIndexToId(),
            _ => (IndexSet)BasisBladeDataLookup.BasisBladeId((uint) grade, index)
        };
    }

    /// <summary>
    /// Find the ID of a basis blade given its grade and basisBladeIndex
    /// </summary>
    /// <param name="grade"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet BasisBladeGradeIndexToId(uint grade, ulong index)
    {
        return grade switch
        {
            0U => index == 0 ? IndexSet.EmptySet : throw new ArgumentOutOfRangeException(nameof(index)),
            1U => index.BasisVectorIndexToId(),
            2U => index.BasisBivectorIndexToId(),
            _ => (IndexSet)BasisBladeDataLookup.BasisBladeId(grade, index)
        };
    }

    /// <summary>
    /// Find the ID of a basis blade given its grade and basisBladeIndex
    /// </summary>
    /// <param name="grade"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet BasisBladeIndexToId(this ulong index, uint grade)
    {
        return grade switch
        {
            0U => index == 0 ? IndexSet.EmptySet : throw new ArgumentOutOfRangeException(nameof(index)),
            1U => index.BasisVectorIndexToId(),
            2U => index.BasisBivectorIndexToId(),
            _ => (IndexSet)BasisBladeDataLookup.BasisBladeId(grade, index)
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet IndexTripletToTrivectorId(this ITriplet<int> indexTriplet)
    {
        var index1 = indexTriplet.Item1;
        var index2 = indexTriplet.Item2;
        var index3 = indexTriplet.Item3;

        Debug.Assert(
            index1 >= 0 && index2 > index1 && index3 > index2
        );

        return IndexSet.CreateTriplet(index1, index2, index3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet IndexTripletToTrivectorId(int index1, int index2, int index3)
    {
        Debug.Assert(
            index1 >= 0 && index2 > index1 && index3 > index2
        );

        return IndexSet.CreateTriplet(index1, index2, index3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet IndexTripletToTrivectorId(ulong index1, ulong index2, ulong index3)
    {
        Debug.Assert(index2 > index1 && index3 > index2);

        return IndexSet.CreateTriplet((int)index1, (int)index2, (int)index3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet BasisVectorIndicesToBasisBladeId(params int[] basisVectorIndices)
    {
        return IndexSet.Create(basisVectorIndices, false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet BasisVectorIndicesToBasisBladeId(this IEnumerable<int> basisVectorIndices)
    {
        return IndexSet.Create(basisVectorIndices, false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet BasisVectorIndicesToBasisBladeId(this IEnumerable<IndexSet> basisVectorIndices)
    {
        return IndexSet.Create(basisVectorIndices.Select(i => (int)i), false);
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
        return (ulong) BasisBladeGradeIndexToId(grade, index).LastIndex;
    }
        
    /// <summary>
    /// Find the grade of a basis blade given its ID
    /// </summary>
    /// <param name="basisBladeId"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint BasisBladeIdToGrade(this IndexSet basisBladeId)
    {
        return (uint) basisBladeId.Count;
    }
    
    /// <summary>
    /// Find the basisBladeIndex of a basis blade given its ID
    /// </summary>
    /// <param name="basisBladeId"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong BasisBladeIdToIndex(this IndexSet basisBladeId)
    {
        return basisBladeId.DecodeCombinadicToUInt64();
    }

    /// <summary>
    /// Find the grade and basisBladeIndex of a basis blade given its ID
    /// </summary>
    /// <param name="basisBladeId"></param>
    /// <param name="grade"></param>
    /// <param name="index"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void BasisBladeIdToGradeIndex(this IndexSet basisBladeId, out uint grade, out ulong index)
    {
        grade = (uint) basisBladeId.Count;
        index = basisBladeId.DecodeCombinadicToUInt64();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (uint grade, ulong index) BasisBladeIdToGradeIndex(this IndexSet basisBladeId)
    {
        var grade = (uint) basisBladeId.Count;
        var index = basisBladeId.DecodeCombinadicToUInt64();

        return (grade, index);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string BasisBladeIdToName(this IndexSet basisBladeId, params string[] basisVectorNames)
    {
        return basisBladeId.IsEmptySet
            ? "E0"
            : basisVectorNames
                .GetItems(basisBladeId)
                .ConcatenateText("^");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string BasisBladeGradeIndexToName(uint grade, ulong basisBladeIndex, params string[] basisVectorNames)
    {
        var basisBladeId = basisBladeIndex.BasisBladeIndexToId(grade);

        return basisBladeId.IsEmptySet
            ? "E0"
            : basisVectorNames
                .GetItems(basisBladeId)
                .ConcatenateText("^");
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string BasisBladeGradeIndexToIndexedName(uint grade, ulong basisBladeIndex)
    {
        return "E" + basisBladeIndex.BasisBladeIndexToId(grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string BasisBladeIdToGradeIndexName(this IndexSet basisBladeId)
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
    public static IEnumerable<IndexSet> BasisBladeIdToBasisVectorIds(this IndexSet basisBladeId)
    {
        return basisBladeId.GetUnitSubsets();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IndexSet> BasisBladeGradeIndexToBasisVectorIds(uint grade, ulong basisBladeIndex)
    {
        return basisBladeIndex.BasisBladeIndexToId(grade).GetUnitSubsets();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ulong> BasisBladeIdToBasisVectorIndices(this IndexSet basisBladeId)
    {
        return basisBladeId.GetIndices().Select(i => (ulong) i);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> BasisBladeGradeIndexToInt32BasisVectorIndices(uint grade, ulong basisBladeIndex)
    {
        return basisBladeIndex.BasisBladeIndexToId(grade).GetIndices();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ulong> BasisBladeGradeIndexToBasisVectorIndices(uint grade, ulong basisBladeIndex)
    {
        return basisBladeIndex.BasisBladeIndexToId(grade).GetIndices(i => (ulong)i);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IndexSet> BasisBladeIDsInside(this IndexSet basisBladeId)
    {
        return basisBladeId.GetSubsets();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IndexSet> BasisBladeIDsInside(uint grade, ulong basisBladeIndex)
    {
        return basisBladeIndex.BasisBladeIndexToId(grade).GetSubsets();
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IEnumerable<ulong> BasisBladeIDsContaining(IndexSet basisBladeId, uint vSpaceDimensions)
    //{
    //    return basisBladeId.GetSuperPatterns((int) vSpaceDimensions);
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IEnumerable<ulong> BasisBladeIDsContaining(uint grade, ulong basisBladeIndex, uint vSpaceDimensions)
    //{
    //    return basisBladeIndex.BasisBladeIndexToId(grade).GetSuperPatterns((int) vSpaceDimensions);
    //}
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IndexSet> SortBasisBladeIDsByGrade(this IEnumerable<IndexSet> basisBladeIdsList)
    {
        return basisBladeIdsList.OrderBy(BasisBladeIdToGrade).ThenBy(BasisBladeIdToIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IGrouping<uint, IndexSet>> GroupBasisBladeIDsByGrade(this IEnumerable<IndexSet> basisBladeIdsList)
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
    public static bool BasisBladeIdContains(this IndexSet basisBladeId, IndexSet subId)
    {
        return basisBladeId.SetContains(subId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="basisBladeId"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (IndexSet basisVectorId, IndexSet subBasisBladeId) SplitBySmallestBasisVectorId(this IndexSet basisBladeId)
    {
        return basisBladeId.SplitByFirstIndex();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="basisBladeId"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (IndexSet basisVectorId, IndexSet subBasisBladeId) SplitByLargestBasisVectorId(this IndexSet basisBladeId)
    {
        return basisBladeId.SplitByLastIndex();
    }

    /// <summary>
    /// Find all basis blade IDs with the given grade and indexes
    /// </summary>
    /// <param name="grade"></param>
    /// <param name="indexSeq"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IndexSet> BasisBladeIDsOfGradeIndex(uint grade, IEnumerable<ulong> indexSeq)
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
    public static IEnumerable<IndexSet> BasisBladeIDsOfGradeIndex(uint grade, params ulong[] indexSeq)
    {
        return indexSeq.Select(index => index.BasisBladeIndexToId(grade));
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBasisBladeOfGrade(this IndexSet basisBladeId, uint grade)
    {
        return basisBladeId.Count == grade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidBasisBladeId(this IndexSet basisBladeId, int vSpaceDimensions)
    {
        return basisBladeId.Count <= vSpaceDimensions;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValidBasisBladeId(this IndexSet basisBladeId, uint vSpaceDimensions)
    {
        return basisBladeId.Count < (int) vSpaceDimensions;
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
    public static bool BasisBladeHasEvenGrade(this IndexSet basisBladeId)
    {
        return (basisBladeId.BasisBladeIdToGrade() & 1) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BasisBladeHasOddGrade(this IndexSet basisBladeId)
    {
        return (basisBladeId.BasisBladeIdToGrade() & 1) != 0;
    }
        
    /// <summary>
    /// Test if the grade inverse of a given basis blade is -1 the original basis blade
    /// </summary>
    /// <param name="basisBladeId"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GradeInvolutionIsPositiveOfBasisBladeId(this IndexSet basisBladeId)
    {
        return basisBladeId.Count.GradeInvolutionIsPositiveOfGrade();
    }

    /// <summary>
    /// Test if the grade inverse of a given basis blade is -1 the original basis blade
    /// </summary>
    /// <param name="basisBladeId"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GradeInvolutionIsNegativeOfBasisBladeId(this IndexSet basisBladeId)
    {
        return basisBladeId.Count.GradeInvolutionIsNegativeOfGrade();
    }
    
    /// <summary>
    /// Test if the grade inverse of a given basis blade is -1 the original basis blade
    /// </summary>
    /// <param name="basisBladeId"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign GradeInvolutionSignOfBasisBladeId(this IndexSet basisBladeId)
    {
        return basisBladeId.Count.GradeInvolutionSignOfGrade();
    }

    /// <summary>
    /// Test if the reverse of a given basis blade is -1 the original basis blade
    /// </summary>
    /// <param name="basisBladeId"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ReverseIsPositiveOfBasisBladeId(this IndexSet basisBladeId)
    {
        return basisBladeId.Count.ReverseIsPositiveOfGrade();
    }

    /// <summary>
    /// Test if the reverse of a given basis blade is -1 the original basis blade
    /// </summary>
    /// <param name="basisBladeId"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ReverseIsNegativeOfBasisBladeId(this IndexSet basisBladeId)
    {
        return basisBladeId.Count.ReverseIsNegativeOfGrade();
    }
    
    /// <summary>
    /// Test if the reverse of a given basis blade is -1 the original basis blade
    /// </summary>
    /// <param name="basisBladeId"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign ReverseSignOfBasisBladeId(this IndexSet basisBladeId)
    {
        return basisBladeId.Count.ReverseSignOfGrade();
    }

    /// <summary>
    /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
    /// </summary>
    /// <param name="basisBladeId"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CliffordConjugateIsPositiveOfBasisBladeId(this IndexSet basisBladeId)
    {
        return basisBladeId.Count.CliffordConjugateIsPositiveOfGrade();
    }

    /// <summary>
    /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
    /// </summary>
    /// <param name="basisBladeId"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CliffordConjugateIsNegativeOfBasisBladeId(this IndexSet basisBladeId)
    {
        return basisBladeId.Count.CliffordConjugateIsNegativeOfGrade();
    }
        
    /// <summary>
    /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
    /// </summary>
    /// <param name="basisBladeId"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntegerSign CliffordConjugateSignOfBasisBladeId(this IndexSet basisBladeId)
    {
        return basisBladeId.Count.CliffordConjugateSignOfGrade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet GetMaxBasisBladeId(this IEnumerable<IndexSet> basisBladeIdList)
    {
        return basisBladeIdList.Max();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet GetMaxBasisBladeId(this IEnumerable<ulong> indexList, uint grade)
    {
        return indexList.Max().BasisBladeIndexToId(grade);
    }
    
    /// <summary>
    /// The max basis blade ID in a GA space with a given dimension
    /// </summary>
    /// <param name="vSpaceDimensions"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IndexSet GetMaxBasisBladeId(this uint vSpaceDimensions)
    {
        return ((int)vSpaceDimensions).ToDenseIndexSet();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetMinVSpaceDimension(this IEnumerable<IndexSet> basisBladeIdList)
    {
        return basisBladeIdList
            .Max()
            .BasisBladeIdToMinVSpaceDimension();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetMinVSpaceDimension(this IEnumerable<ulong> indexList, uint grade)
    {
        return indexList.GetMaxBasisBladeId(grade).BasisBladeIdToMinVSpaceDimension();
    }
}