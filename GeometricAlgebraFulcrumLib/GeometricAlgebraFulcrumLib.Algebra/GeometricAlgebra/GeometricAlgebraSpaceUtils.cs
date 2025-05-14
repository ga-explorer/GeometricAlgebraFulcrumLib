//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using System.Text;
//using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

//namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;

//public static class GeometricAlgebraSpaceUtils
//{
//    ///// <summary>
//    ///// The maximum allowed GA vector space dimension
//    ///// </summary>
//    //public static uint MaxVSpaceDimension { get; } 
//    //    = 63;

//    ///// <summary>
//    ///// The maximum possible basis blade ID in the maximum allowed GA vector space dimension
//    ///// </summary>
//    //public static ulong MaxVSpaceBasisBladeId { get; } 
//    //    = (1ul << (int) MaxVSpaceDimension) - 1ul;

//    //public static IReadOnlyList<string> DefaultBasisVectorsNames { get; } 
//    //    = MaxVSpaceDimension.GetRange()
//    //        .Select(i => "e" + i)
//    //        .ToArray();

        
//    /// <summary>
//    /// The number of basis blades in a GA with dimension vSpaceDimensions
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static ulong ToGaSpaceDimension(this int vSpaceDimensions)
//    {
//        if (vSpaceDimensions is < 0 or > 63)
//            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

//        return 1ul << vSpaceDimensions;
//    }

//    /// <summary>
//    /// The number of basis blades in a GA with dimension vSpaceDimensions
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static ulong ToGaSpaceDimension(this uint vSpaceDimensions)
//    {
//        if (vSpaceDimensions > 63)
//            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

//        return 1ul << (int) vSpaceDimensions;
//    }

//    /// <summary>
//    /// The number of basis vectors in a GA with dimension gaSpaceDimensions
//    /// </summary>
//    /// <param name="gaSpaceDimensions"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static int ToVSpaceDimension(this int gaSpaceDimensions)
//    {
//        return gaSpaceDimensions.LastOneBitPosition();
//    }

//    /// <summary>
//    /// The number of basis vectors in a GA with dimension gaSpaceDimensions
//    /// </summary>
//    /// <param name="gaSpaceDimensions"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static int ToVSpaceDimension(this ulong gaSpaceDimensions)
//    {
//        return gaSpaceDimensions.LastOneBitPosition();
//    }

//    /// <summary>
//    /// The number of grades in a GA space with a given dimension
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static uint GradesCount(this uint vSpaceDimensions)
//    {
//        return vSpaceDimensions + 1;
//    }

//    /// <summary>
//    /// The dimension of bivectors subspace of a GA with a given dimension
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static ulong BivectorSpaceDimension(this uint vSpaceDimensions)
//    {
//        return (vSpaceDimensions * (vSpaceDimensions - 1)) >> 1;
//    }
        
//    /// <summary>
//    /// The dimension of k-vectors subspace of some grade of a GA with a given dimension
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <param name="grade"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static ulong KVectorSpaceDimension(this int vSpaceDimensions, int grade)
//    {
//        return grade switch
//        {
//            < 0 => throw new InvalidOperationException(),
//            0 => 0UL,
//            1 => (ulong) vSpaceDimensions,
//            2 => ((ulong)vSpaceDimensions * (ulong)(vSpaceDimensions - 1)) >> 1,
//            _ => vSpaceDimensions.GetBinomialCoefficient(grade)
//        };
//    }
        
//    /// <summary>
//    /// The dimension of k-vectors subspace of some grade of a GA with a given dimension
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <param name="grade"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static ulong KVectorSpaceDimension(this uint vSpaceDimensions, int grade)
//    {
//        return grade switch
//        {
//            0 => 0UL,
//            1 => vSpaceDimensions,
//            2 => (vSpaceDimensions * (ulong)(vSpaceDimensions - 1)) >> 1,
//            _ => vSpaceDimensions.GetBinomialCoefficient((uint) grade)
//        };
//    }

//    /// <summary>
//    /// The dimension of k-vectors subspace of some grade of a GA with a given dimension
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <param name="grade"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static ulong KVectorSpaceDimension(this uint vSpaceDimensions, uint grade)
//    {
//        return grade switch
//        {
//            0U => 0UL,
//            1U => vSpaceDimensions,
//            2U => (vSpaceDimensions * (vSpaceDimensions - 1)) >> 1,
//            _ => vSpaceDimensions.GetBinomialCoefficient(grade)
//        };
//    }

//    /// <summary>
//    /// The grades of k-vectors in a GA with the given dimension
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<uint> Grades(this uint vSpaceDimensions)
//    {
//        return (vSpaceDimensions + 1).GetRange();
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IndexSet PseudoScalarId(this uint vSpaceDimensions)
//    {
//        return IndexSet.CreateDense((int) vSpaceDimensions);
//    }

//    /// <summary>
//    /// Test if the given integer is a legal GA space dimension (i.e. positive power of 2 less than or 
//    /// equal to 2 ^ MaxVSpaceDim)
//    /// </summary>
//    /// <param name="gaSpaceDimensions"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsValidGaSpaceDimension(this ulong gaSpaceDimensions)
//    {
//        return gaSpaceDimensions.Grade() == 1;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsValidGaSpaceDimension(this int gaSpaceDimensions)
//    {
//        return gaSpaceDimensions.Grade() == 1;
//    }


//    /// <summary>
//    /// The Basis blade IDs of a GA space with the given dimension
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <returns></returns>
//    public static IEnumerable<IndexSet> BasisBladeIDs(this uint vSpaceDimensions)
//    {
//        return vSpaceDimensions.GetMaxBasisBladeId().GetSubsets();
//    }

//    /// <summary>
//    /// Find all basis blade IDs with the given grade in a GA of dimension vSpaceDimensions
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <param name="grade"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisBladeIDsOfGrade(this int vSpaceDimensions, int grade)
//    {
//        return UInt64BitUtils.OnesPermutations(
//            vSpaceDimensions, 
//            grade
//        );
//    }

//    /// <summary>
//    /// Find all basis blade IDs with the given grade in a GA of dimension vSpaceDimensions
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <param name="grade"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisBladeIDsOfGrade(this uint vSpaceDimensions, uint grade)
//    {
//        return UInt64BitUtils.OnesPermutations(
//            (int) vSpaceDimensions, 
//            (int) grade
//        );
//    }
        
//    /// <summary>
//    /// The basis blade IDs of a GA space with the given dimension sorted by their grade and index
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <param name="startGrade"></param>
//    /// <returns></returns>
//    public static IEnumerable<ulong> BasisBladeIDsSortedByGrade(this int vSpaceDimensions, int startGrade = 0)
//    {
//        for (var grade = startGrade; grade <= vSpaceDimensions; grade++)
//            foreach (var id in BasisBladeIDsOfGrade(vSpaceDimensions, grade))
//                yield return id;
//    }

//    /// <summary>
//    /// The basis blade IDs of a GA space with the given dimension sorted by their grade and index
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <param name="startGrade"></param>
//    /// <returns></returns>
//    public static IEnumerable<ulong> BasisBladeIDsSortedByGrade(this uint vSpaceDimensions, uint startGrade = 0)
//    {
//        for (var grade = startGrade; grade <= vSpaceDimensions; grade++)
//            foreach (var id in BasisBladeIDsOfGrade(vSpaceDimensions, grade))
//                yield return id;
//    }

//    /// <summary>
//    /// Returns the basis blade IDs of having the given grades
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <param name="gradesSeq"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisBladeIDsOfGrades(this uint vSpaceDimensions, IEnumerable<uint> gradesSeq)
//    {
//        return gradesSeq
//            .OrderBy(g => g)
//            .SelectMany(grade => BasisBladeIDsOfGrade(vSpaceDimensions, grade));
//    }

//    /// <summary>
//    /// Returns the basis blade IDs of having the given grades
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <param name="gradesSeq"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisBladeIDsOfGrades(this uint vSpaceDimensions, params uint[] gradesSeq)
//    {
//        return gradesSeq
//            .OrderBy(g => g)
//            .SelectMany(grade => BasisBladeIDsOfGrade(vSpaceDimensions, grade));
//    }

//    /// <summary>
//    /// Returns the basis blade IDs of having the given grades grouped by their grade
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <param name="startGrade"></param>
//    /// <returns></returns>
//    public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(this uint vSpaceDimensions, uint startGrade = 0)
//    {
//        var result = new Dictionary<uint, IReadOnlyList<ulong>>();

//        for (var grade = startGrade; grade <= vSpaceDimensions; grade++)
//        {
//            result.Add(
//                grade, BasisBladeIDsOfGrade(vSpaceDimensions, grade).ToArray()
//            );
//        }

//        return result;
//    }

//    /// <summary>
//    /// Returns the basis blade IDs of having the given grades grouped by their grade
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <param name="gradesSeq"></param>
//    /// <returns></returns>
//    public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(this uint vSpaceDimensions, IEnumerable<uint> gradesSeq)
//    {
//        var result = new Dictionary<uint, IReadOnlyList<ulong>>();

//        foreach (var grade in gradesSeq)
//        {
//            result.Add(
//                grade, BasisBladeIDsOfGrade(vSpaceDimensions, grade).ToArray()
//            );
//        }

//        return result;
//    }

//    /// <summary>
//    /// Returns the basis blade IDs of having the given grades grouped by their grade
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <param name="gradesSeq"></param>
//    /// <returns></returns>
//    public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(this uint vSpaceDimensions, params uint[] gradesSeq)
//    {
//        var result = new Dictionary<uint, IReadOnlyList<ulong>>();

//        foreach (var grade in gradesSeq)
//        {
//            result.Add(
//                grade, BasisBladeIDsOfGrade(vSpaceDimensions, grade).ToArray()
//            );
//        }

//        return result;
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static ulong BivectorSpaceDimension(this IGeometricAlgebraSpace space)
//    {
//        var n = (ulong) space.VSpaceDimensions;

//        return n * (n - 1) / 2UL;
//    }
        
//    /// <summary>
//    /// The dimension of k-vectors subspace of some grade of this basis set
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="grade"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static ulong KVectorSpaceDimension(this IGeometricAlgebraSpace space, uint grade)
//    {
//        Debug.Assert(grade <= space.VSpaceDimensions);

//        var n = (ulong) space.VSpaceDimensions;

//        return grade switch
//        {
//            0 => 1,
//            1 => n,
//            2 => n * (n - 1) / 2UL,
//            _ => space.VSpaceDimensions.GetBinomialCoefficient(grade)
//        };
//    }
        
//    /// <summary>
//    /// The Basis blade IDs of this basis set
//    /// </summary>
//    /// <returns></returns>
//    public static IEnumerable<ulong> BasisBladeIDs(this IGeometricAlgebraSpace space)
//    {
//        for (var id = 0ul; id <= space.MaxBasisBladeId; id++)
//            yield return id;
//    }

//    /// <summary>
//    /// The basis vectors IDs of the given frame
//    /// </summary>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisVectorIDs(this IGeometricAlgebraSpace space)
//    {
//        return space.VSpaceDimensions
//            .GetRange()
//            .Select(i => (1ul << (int) i));
//    }

//    /// <summary>
//    /// Find all basis blade IDs with the given grade and indexes in a given frame
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="grade"></param>
//    /// <param name="indexSeq"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisBladeIDsOfGrade(this IGeometricAlgebraSpace space, uint grade, IEnumerable<ulong> indexSeq)
//    {
//        return indexSeq.Select(index => index.BasisBladeIndexToId(grade));
//    }

//    /// <summary>
//    /// Find all basis blade IDs with the given grade in a given frame
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="grade"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisBladeIDsOfGrade(this IGeometricAlgebraSpace space, uint grade)
//    {
//        return UInt64BitUtils.OnesPermutations(
//            (int) space.VSpaceDimensions, 
//            (int) grade
//        );
//    }

//    /// <summary>
//    /// The basis blade IDs of the given frame sorted by their grade and index
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="startGrade"></param>
//    /// <returns></returns>
//    public static IEnumerable<ulong> BasisBladeIDsSortedByGrade(this IGeometricAlgebraSpace space, uint startGrade = 0)
//    {
//        for (var grade = startGrade; grade <= space.VSpaceDimensions; grade++)
//            foreach (var id in space.BasisBladeIDsOfGrade(grade))
//                yield return id;
//    }
        
//    /// <summary>
//    /// Returns the basis blade IDs of having the given grades
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <param name="gradesSeq"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisBladeIDsOfGrades(this int vSpaceDimensions, params int[] gradesSeq)
//    {
//        return gradesSeq
//            .OrderBy(g => g)
//            .SelectMany(grade => 
//                UInt64BitUtils.OnesPermutations(
//                    vSpaceDimensions, 
//                    grade
//                )
//            );
//    }

//    /// <summary>
//    /// Returns the basis blade IDs of having the given grades
//    /// </summary>
//    /// <param name="vSpaceDimensions"></param>
//    /// <param name="gradesSeq"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisBladeIDsOfGrades(this int vSpaceDimensions, IEnumerable<int> gradesSeq)
//    {
//        return gradesSeq
//            .OrderBy(g => g)
//            .SelectMany(grade => 
//                UInt64BitUtils.OnesPermutations(
//                    vSpaceDimensions, 
//                    grade
//                )
//            );
//    }

//    /// <summary>
//    /// Returns the basis blade IDs of having the given grades
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="gradesSeq"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisBladeIDsOfGrades(this IGeometricAlgebraSpace space, IEnumerable<uint> gradesSeq)
//    {
//        var vSpaceDimensions = space.VSpaceDimensions;

//        return gradesSeq
//            .OrderBy(g => g)
//            .SelectMany(grade => 
//                UInt64BitUtils.OnesPermutations(
//                    (int) vSpaceDimensions, 
//                    (int) grade
//                )
//            );
//    }

//    /// <summary>
//    /// Returns the basis blade IDs of having the given grades
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="gradesSeq"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisBladeIDsOfGrades(this IGeometricAlgebraSpace space, params uint[] gradesSeq)
//    {
//        var vSpaceDimensions = space.VSpaceDimensions;

//        return gradesSeq
//            .OrderBy(g => g)
//            .SelectMany(grade => 
//                UInt64BitUtils.OnesPermutations(
//                    (int) vSpaceDimensions, 
//                    (int) grade
//                )
//            );
//    }

//    /// <summary>
//    /// Returns the basis blade IDs of having the given grades grouped by their grade
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="startGrade"></param>
//    /// <returns></returns>
//    public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(this IGeometricAlgebraSpace space, uint startGrade = 0)
//    {
//        var result = new Dictionary<uint, IReadOnlyList<ulong>>();

//        for (var grade = startGrade; grade <= space.VSpaceDimensions; grade++)
//        {
//            result.Add(
//                grade, 
//                space.BasisBladeIDsOfGrade(grade).ToArray()
//            );
//        }

//        return result;
//    }

//    /// <summary>
//    /// Returns the basis blade IDs of having the given grades grouped by their grade
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="gradesSeq"></param>
//    /// <returns></returns>
//    public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(this IGeometricAlgebraSpace space, IEnumerable<uint> gradesSeq)
//    {
//        var result = new Dictionary<uint, IReadOnlyList<ulong>>();

//        foreach (var grade in gradesSeq)
//        {
//            result.Add(
//                grade, 
//                space.BasisBladeIDsOfGrade(grade).ToArray()
//            );
//        }

//        return result;
//    }

//    /// <summary>
//    /// Returns the basis blade IDs of having the given grades grouped by their grade
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="gradesSeq"></param>
//    /// <returns></returns>
//    public static Dictionary<uint, IReadOnlyList<ulong>> BasisBladeIDsGroupedByGrade(this IGeometricAlgebraSpace space, params uint[] gradesSeq)
//    {
//        var result = new Dictionary<uint, IReadOnlyList<ulong>>();

//        foreach (var grade in gradesSeq)
//        {
//            result.Add(
//                grade, 
//                space.BasisBladeIDsOfGrade(grade).ToArray()
//            );
//        }

//        return result;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static string BasisBladeBinaryIndexedName(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return "B" + basisBladeId.PatternToString((int) space.VSpaceDimensions);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static string BasisBladeBinaryIndexedName(this IGeometricAlgebraSpace space, uint grade, ulong index)
//    {
//        return "B" + index.BasisBladeIndexToId(grade).PatternToString((int) space.VSpaceDimensions);
//    }
        
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static string BasisBladeGradeIndexName(ulong basisBladeId)
//    {
//        return
//            new StringBuilder(32)
//                .Append('G')
//                .Append(basisBladeId.BasisBladeIdToGrade())
//                .Append('I')
//                .Append(basisBladeId.BasisBladeIdToIndex())
//                .ToString();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisBladeIDsContaining(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return basisBladeId.GetSuperPatterns((int) space.VSpaceDimensions);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisBladeIDsContaining(this IGeometricAlgebraSpace space, uint grade, ulong index)
//    {
//        return index
//            .BasisBladeIndexToId(grade)
//            .GetSuperPatterns((int) space.VSpaceDimensions);
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsValidBasisVectorId(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return space.IsValidBasisBladeId(basisBladeId) && 
//               basisBladeId.IsBasicPattern();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsValidBasisVectorIndex(this IGeometricAlgebraSpace space, ulong index)
//    {
//        return index < space.VSpaceDimensions;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsValidBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return basisBladeId <= space.MaxBasisBladeId;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsValidBasisBladeGradeIndex(this IGeometricAlgebraSpace space, uint grade, ulong index)
//    {
//        if (grade > space.VSpaceDimensions) return false;

//        var kvDim = KVectorSpaceDimension(space.VSpaceDimensions, grade);

//        return index < kvDim;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool IsValidGrade(this IGeometricAlgebraSpace space, uint grade)
//    {
//        return (grade <= space.VSpaceDimensions);
//    }

//    /// <summary>
//    /// Get all possible grades of the meet of two blades grades
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="grade1"></param>
//    /// <param name="grade2"></param>
//    /// <returns></returns>
//    public static IEnumerable<int> GradesOfMeet(this IGeometricAlgebraSpace space, int grade1, int grade2)
//    {
//        if (grade1 > space.VSpaceDimensions || grade2 > space.VSpaceDimensions || grade1 < 0 || grade2 < 0)
//            yield break;

//        var maxGrade = Math.Min(grade1, grade2);

//        //TODO: Should this be grade++ instead of grade += 2 ?
//        for (var grade = 0; grade <= maxGrade; grade += 2)
//            yield return grade;
//    }

//    /// <summary>
//    /// Get all possible grades of the meet of two blades grades
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="grade1"></param>
//    /// <param name="grade2"></param>
//    /// <returns></returns>
//    public static IEnumerable<int> GradesOfJoin(this IGeometricAlgebraSpace space, int grade1, int grade2)
//    {
//        if (grade1 > space.VSpaceDimensions || grade2 > space.VSpaceDimensions || grade1 < 0 || grade2 < 0)
//            yield break;

//        var minGrade = Math.Max(grade1, grade2);
//        var maxGrade = Math.Min(space.VSpaceDimensions, grade1 + grade2);

//        //TODO: Should this be grade++ instead of grade += 2 ?
//        for (var grade = minGrade; grade <= maxGrade; grade += 2)
//            yield return grade;
//    }

//    /// <summary>
//    /// Return a list of all possible grades in the geometric product of two k-vectors with
//    /// the given grades
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="grade1"></param>
//    /// <param name="grade2"></param>
//    /// <returns></returns>
//    public static IEnumerable<uint> GradesOfEGp(this IGeometricAlgebraSpace space, uint grade1, uint grade2)
//    {
//        if (grade1 > space.VSpaceDimensions || grade2 > space.VSpaceDimensions)
//            yield break;

//        var minGrade = (uint) Math.Abs((int)grade1 - (int)grade2);
//        var maxGrade = Math.Min(space.VSpaceDimensions, grade1 + grade2);

//        for (var grade = minGrade; grade <= maxGrade; grade += 2)
//            yield return grade;
//    }

//    /// <summary>
//    /// Given a bit pattern in id1 and id2 this shifts id2 by space.VSpaceDimensions bits to the left and 
//    /// appends id1 to combine the two patterns using an OR bitwise operation
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="id1"></param>
//    /// <param name="id2"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static ulong JoinIDs(this IGeometricAlgebraSpace space, ulong id1, ulong id2)
//    {
//        return id1 | (id2 << (int) space.VSpaceDimensions);
//    }

//    /// <summary>
//    /// Find all basis blade IDs with the given grade and indexes in a given frame
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="grade"></param>
//    /// <param name="indexSeq"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisBladeIDsOfGradeIndex(this IGeometricAlgebraSpace space, uint grade, params ulong[] indexSeq)
//    {
//        return indexSeq.Select(index => index.BasisBladeIndexToId(grade));
//    }

//    /// <summary>
//    /// Find the ID of a basis blade given its grade and index
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="grade"></param>
//    /// <param name="index"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static ulong BasisBladeId(this IGeometricAlgebraSpace space, uint grade, ulong index)
//    {
//        return BasisBladeDataLookup.BasisBladeId(grade, index);
//    }

//    /// <summary>
//    /// Find the ID of a basis vector given its index
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="index"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static ulong BasisVectorId(this IGeometricAlgebraSpace space, ulong index)
//    {
//        return 1UL << (int) index;
//    }

//    /// <summary>
//    /// Find the grade of a basis blade given its ID
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="basisBladeId"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static uint BasisBladeGrade(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return BasisBladeDataLookup.BasisBladeGrade(basisBladeId);
//    }

//    /// <summary>
//    /// Find the index of a basis blade given its ID
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="basisBladeId"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static ulong BasisBladeIndex(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return BasisBladeDataLookup.BasisBladeIndex(basisBladeId);
//    }

//    /// <summary>
//    /// Find the grade and index of a basis blade given its ID
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="basisBladeId"></param>
//    /// <param name="grade"></param>
//    /// <param name="index"></param>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static void BasisBladeGradeIndex(this IGeometricAlgebraSpace space, ulong basisBladeId, out uint grade, out ulong index)
//    {
//        basisBladeId.BasisBladeIdToGradeIndex(out grade, out index);
//    }
        
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Tuple<uint, ulong> BasisBladeGradeIndex(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return BasisBladeDataLookup.BasisBladeGradeIndex(basisBladeId);
//    }
        
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static string BasisBladeIndexedName(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return "E" + basisBladeId;
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static string BasisBladeIndexedName(this IGeometricAlgebraSpace space, uint grade, ulong index)
//    {
//        return "E" + space.BasisBladeId(grade, index);
//    }
        
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static string BasisBladeGradeIndexName(this IGeometricAlgebraSpace space, uint grade, ulong index)
//    {
//        return
//            new StringBuilder(32)
//                .Append('G')
//                .Append(grade)
//                .Append('I')
//                .Append(index)
//                .ToString();
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisVectorIDsInside(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return basisBladeId.GetBasicPatterns();
//    }
        
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisVectorIDsInside(this IGeometricAlgebraSpace space, uint grade, ulong index)
//    {
//        return space
//            .BasisBladeId(grade, index)
//            .GetBasicPatterns();
//    }
        
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisVectorIndexesInside(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return basisBladeId
//            .GetSetBitPositions()
//            .Select(i => (ulong)i);
//    }
        
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisVectorIndexesInside(this IGeometricAlgebraSpace space, uint grade, ulong index)
//    {
//        return space
//            .BasisBladeId(grade, index)
//            .GetSetBitPositions()
//            .Select(i => (ulong)i);
//    }
        
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisBladeIDsInside(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return basisBladeId.GetSubPatterns();
//    }
        
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IEnumerable<ulong> BasisBladeIDsInside(this IGeometricAlgebraSpace space, uint grade, ulong index)
//    {
//        return space
//            .BasisBladeId(grade, index)
//            .GetSubPatterns();
//    }


//    /// <summary>
//    /// Test if the grade inverse of a given basis blade is -1 the original basis blade
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="basisBladeId"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool GradeInvolutionIsPositiveOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return BasisBladeDataLookup.GradeInvolutionIsPositive(basisBladeId);
//    }

//    /// <summary>
//    /// Test if the grade inverse of a given basis blade is -1 the original basis blade
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="basisBladeId"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool GradeInvolutionIsNegativeOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return BasisBladeDataLookup.GradeInvolutionIsNegative(basisBladeId);
//    }

//    /// <summary>
//    /// Test if the grade inverse of a given basis blade is -1 the original basis blade
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="basisBladeId"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IntegerSign GradeInvolutionSignOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return BasisBladeDataLookup.GradeInvolutionSign(basisBladeId);
//    }

//    /// <summary>
//    /// Test if the reverse of a given basis blade is -1 the original basis blade
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="basisBladeId"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool ReverseIsPositiveOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return BasisBladeDataLookup.ReverseIsPositive(basisBladeId);
//    }

//    /// <summary>
//    /// Test if the reverse of a given basis blade is -1 the original basis blade
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="basisBladeId"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool ReverseIsNegativeOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return BasisBladeDataLookup.ReverseIsNegative(basisBladeId);
//    }

//    /// <summary>
//    /// Test if the reverse of a given basis blade is -1 the original basis blade
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="basisBladeId"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IntegerSign ReverseSignOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return BasisBladeDataLookup.ReverseSign(basisBladeId);
//    }

//    /// <summary>
//    /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="basisBladeId"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool CliffordConjugateIsPositiveOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return BasisBladeDataLookup.CliffordConjugateIsPositive(basisBladeId);
//    }

//    /// <summary>
//    /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="basisBladeId"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static bool CliffordConjugateIsNegativeOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return BasisBladeDataLookup.CliffordConjugateIsNegative(basisBladeId);
//    }

//    /// <summary>
//    /// Test if the clifford conjugate of a given basis blade is -1 the original basis blade 
//    /// </summary>
//    /// <param name="space"></param>
//    /// <param name="basisBladeId"></param>
//    /// <returns></returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static IntegerSign CliffordConjugateSignOfBasisBladeId(this IGeometricAlgebraSpace space, ulong basisBladeId)
//    {
//        return BasisBladeDataLookup.CliffordConjugateSign(basisBladeId);
//    }

//}