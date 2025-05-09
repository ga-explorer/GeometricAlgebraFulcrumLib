using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended;

/// <summary>
/// This class holds information about a metric of a set of
/// GA basis blades with orthonormal signature (p, q, r) where
/// p is the number of basis vectors with +1 signature (arbitrary and not specified),
/// q is the number of basis vectors with -1 signature, and
/// r is the number of basis vectors with 0 signature.
/// </summary>
public abstract class XGaMetric
{
    //public static XGaMetric Euclidean { get; }
    //    = new XGaMetric(0, 0);

    //public static XGaMetric Projective { get; }
    //    = new XGaMetric(0, 1);

    //public static XGaMetric Conformal { get; }
    //    = new XGaMetric(1, 0);

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaMetric Create(int negativeCount, int zeroCount)
    //{
    //    return negativeCount switch
    //    {
    //        0 when zeroCount == 0 => Euclidean,
    //        0 when zeroCount == 1 => Projective,
    //        1 when zeroCount == 0 => Conformal,
    //        _ => new XGaMetric(negativeCount, zeroCount)
    //    };
    //}


    /// <summary>
    /// The number of basis vectors with signature equal to -1
    /// </summary>
    public int NegativeSignatureBasisCount { get; }

    /// <summary>
    /// The number of basis vectors with signature equal to 0
    /// </summary>
    public int ZeroSignatureBasisCount { get; }

    public int NonEuclideanBasisCount { get; }

    public bool IsEuclidean { get; }

    public bool IsNonEuclidean { get; }
    
    public bool IsDegenerate
        => ZeroSignatureBasisCount > 0;

    public bool IsProjective
        => NegativeSignatureBasisCount == 0 &&
           ZeroSignatureBasisCount == 1;

    public bool IsConformal
        => NegativeSignatureBasisCount == 1 &&
           ZeroSignatureBasisCount == 0;

    public XGaBasisBlade BasisScalar { get; }

    public XGaSignedBasisBlade ZeroBasisScalar { get; }

    public IXGaSignedBasisBlade PositiveBasisScalar
        => BasisScalar;

    public XGaSignedBasisBlade NegativeBasisScalar { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaMetric(int negativeCount, int zeroCount)
    {
        if (negativeCount < 0 || zeroCount < 0)
            throw new ArgumentException();

        NegativeSignatureBasisCount = negativeCount;
        ZeroSignatureBasisCount = zeroCount;
        NonEuclideanBasisCount = negativeCount + zeroCount;

        IsEuclidean = NonEuclideanBasisCount == 0;
        IsNonEuclidean = !IsEuclidean;

        BasisScalar = new XGaBasisBlade(this, IndexSet.EmptySet);
        ZeroBasisScalar = new XGaSignedBasisBlade(BasisScalar, IntegerSign.Zero);
        NegativeBasisScalar = new XGaSignedBasisBlade(BasisScalar, IntegerSign.Negative);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasSameSignature(XGaMetric metric)
    {
        return NegativeSignatureBasisCount == metric.NegativeSignatureBasisCount &&
               ZeroSignatureBasisCount == metric.ZeroSignatureBasisCount;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(int index)
    {
        return index >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(int index1, int index2)
    {
        return index1 >= 0 && index1 < index2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(params int[] indexArray)
    {
        return indexArray.All(IsValid);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(IEnumerable<int> indexList)
    {
        return indexList.All(IsValid);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet GetBasisScalarId()
    {
        return IndexSet.EmptySet;
    }

    public IEnumerable<IndexSet> GetBasisVectorIds(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        for (var index = 0; index < vSpaceDimensions; index++)
            yield return index.IndexToIndexSet();
    }
        
    public IEnumerable<IndexSet> GetBasisBivectorIds(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        for (var index1 = 0; index1 < vSpaceDimensions - 1; index1++)
        for (var index2 = index1 + 1; index2 < vSpaceDimensions; index2++)
            yield return IndexSetUtils.IndexPairToIndexSet(index1, index2);
    }
        
    public IEnumerable<IndexSet> GetBasisKVectorIds(int vSpaceDimensions, int grade)
    {
        if (grade < 0)
            throw new ArgumentOutOfRangeException(nameof(grade));
            
        if (vSpaceDimensions < grade)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        if (grade == vSpaceDimensions)
            return [GetBasisPseudoScalarId(vSpaceDimensions)];

        return grade switch
        {
            0 => [IndexSet.EmptySet],
            1 => GetBasisVectorIds(vSpaceDimensions),
            2 => GetBasisBivectorIds(vSpaceDimensions),
            _ => vSpaceDimensions <= 64
                ? vSpaceDimensions
                    .GetBinomialCoefficient(grade)
                    .GetRange()
                    .Select(index => BasisBladeUtils.BasisBladeGradeIndexToId((uint) grade, index).BitPatternToIndexSet())
                : vSpaceDimensions
                    .GetRange()
                    .ToArray()
                    .GetCombinationsDistinct(grade)
                    .Select(c => c.ToIndexSet(false))
        };

        //var indexList = 
        //    vSpaceDimensions.GetRange().ToArray();

        //foreach (var c in GetKCombinations(indexList, grade))
        //{
        //    //Console.WriteLine(c.ConcatenateText(", ", "<", ">"));
        //    yield return new XGaBasisKVector(this, c.ToImmutableSortedSet());
        //}

    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet GetBasisPseudoScalarId(int vSpaceDimensions)
    {
        return vSpaceDimensions switch
        {
            64 => ulong.MaxValue.BitPatternToIndexSet(),

            < 64 => ((1UL << vSpaceDimensions) - 1UL).BitPatternToIndexSet(),

            _ => vSpaceDimensions
                .GetRange()
                .ToIndexSet(true)
        };
    }

    public IEnumerable<IndexSet> GetBasisBladeIds(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 0)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        for (var grade = 0; grade <= vSpaceDimensions; grade++)
        {
            var idList = GetBasisKVectorIds(vSpaceDimensions, grade);

            foreach (var basisBlade in idList)
                yield return basisBlade;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SignatureIsPositive(int index)
    {
        Debug.Assert(IsValid(index));

        return index >= NonEuclideanBasisCount;
    }

    public bool SignatureIsPositive(IndexSet indexSet)
    {
        if (IsEuclidean) return true;

        var signature = 1;
        foreach (var index in indexSet)
        {
            if (index >= NegativeSignatureBasisCount)
                return index >= NonEuclideanBasisCount && signature > 0;

            signature = -signature;
        }

        return signature > 0;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SignatureIsNegative(int index)
    {
        Debug.Assert(IsValid(index));

        return index < NegativeSignatureBasisCount;
    }

    public bool SignatureIsNegative(IndexSet indexSet)
    {
        if (IsEuclidean) return false;

        var signature = 1;
        foreach (var index in indexSet)
        {
            if (index >= NegativeSignatureBasisCount)
                return index >= NonEuclideanBasisCount && signature < 0;

            signature = -signature;
        }

        return signature < 0;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SignatureIsZero(int index)
    {
        Debug.Assert(IsValid(index));

        return index >= NegativeSignatureBasisCount &&
               index < NonEuclideanBasisCount;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SignatureIsZero(IndexSet indexSet)
    {
        if (IsEuclidean) return false;

        var signature = 1;
        foreach (var index in indexSet)
        {
            if (index >= NegativeSignatureBasisCount)
                return index < NonEuclideanBasisCount;

            signature = -signature;
        }

        return false;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign Signature(int index)
    {
        Debug.Assert(IsValid(index));

        if (index >= NonEuclideanBasisCount)
            return IntegerSign.Positive;

        if (index < NegativeSignatureBasisCount)
            return IntegerSign.Negative;

        return IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign Signature(IndexSet indexSet)
    {
        if (IsEuclidean)
            return IntegerSign.Positive;

        var isNegative = false;
        foreach (var index in indexSet)
        {
            if (index >= NegativeSignatureBasisCount)
                return index >= NonEuclideanBasisCount
                    ? isNegative
                        ? IntegerSign.Negative
                        : IntegerSign.Positive
                    : IntegerSign.Zero;

            isNegative = !isNegative;
        }

        return isNegative
            ? IntegerSign.Negative
            : IntegerSign.Positive;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign MeetSignature(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        if (IsEuclidean) return IntegerSign.Positive;

        return Signature(
            basisBlade1.SetIntersect(basisBlade2)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GradeInvolutionSign(IndexSet basisBlade)
    {
        return basisBlade.Count.GradeInvolutionSignOfGrade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ReverseSign(IndexSet basisBlade)
    {
        return basisBlade.Count.ReverseSignOfGrade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign CliffordConjugateSign(IndexSet basisBlade)
    {
        return basisBlade.Count.CliffordConjugateSignOfGrade();
    }
    
    /// <summary>
    /// See paper: "On SVD and Polar Decomposition in Real and Complexified Clifford Algebras"
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign HermitianConjugateSign(IndexSet basisBlade)
    {
        var signature = Signature(basisBlade);

        if (signature.IsZero)
            throw new DivideByZeroException();

        return signature * basisBlade.Count.ReverseSignOfGrade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSquaredSign(IndexSet basisBlade)
    {
        return basisBlade.EGpIsNegative(basisBlade)
            ? IntegerSign.Negative
            : IntegerSign.Positive;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpReverseSign(IndexSet basisBlade)
    {
        return IntegerSign.Positive;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GpSquaredSign(IndexSet basisBlade)
    {
        if (IsEuclidean) return EGpSquaredSign(basisBlade);

        var signature = Signature(basisBlade);

        if (signature.IsZero)
            return IntegerSign.Zero;

        var euclideanSignature =
            EGpSquaredSign(basisBlade);

        return signature.IsPositive
            ? euclideanSignature
            : -euclideanSignature;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GpReverseSign(IndexSet basisBlade)
    {
        return Signature(basisBlade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ESpSquaredSign(IndexSet basisBlade)
    {
        return EGpSquaredSign(basisBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ENormSquaredSign(IndexSet basisBlade)
    {
        return IntegerSign.Positive;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign SpSquaredSign(IndexSet basisBlade)
    {
        return GpSquaredSign(basisBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign NormSquaredSign(IndexSet basisBlade)
    {
        return Signature(basisBlade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.EGpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpReverseSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.EGpReverseSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        if (IsEuclidean) return basisBlade1.EGpSign(basisBlade2);

        var meetBasisBladeSignature =
            MeetSignature(basisBlade1, basisBlade2);

        return meetBasisBladeSignature.IsZero
            ? IntegerSign.Zero
            : basisBlade1.EGpSign(basisBlade2) *
              meetBasisBladeSignature;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GpReverseSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        if (IsEuclidean) return basisBlade1.EGpReverseSign(basisBlade2);

        var meetBasisBladeSignature =
            MeetSignature(basisBlade1, basisBlade2);

        return meetBasisBladeSignature.IsZero
            ? IntegerSign.Zero
            : basisBlade1.EGpReverseSign(basisBlade2) *
              meetBasisBladeSignature;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign OpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.OpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign SpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.ESpIsNonZero(basisBlade2)
            ? GpSquaredSign(basisBlade1)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ELcpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.ELcpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign LcpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.ELcpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ERcpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.ERcpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign RcpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.ERcpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EFdpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.EFdpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign FdpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.EFdpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EHipSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.EHipSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign HipSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.EHipIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EAcpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.EAcpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign AcpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.EAcpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ECpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.ECpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign CpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.ECpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EDual(IndexSet basisBladeId, int vSpaceDimensions)
    {
        Debug.Assert(
            basisBladeId.IsEmptySet || 
            basisBladeId.LastIndex < vSpaceDimensions
        );

        var pseudoScalarId = GetBasisPseudoScalarId(vSpaceDimensions);
        var unDual = EGp(basisBladeId, pseudoScalarId);

        return SignatureIsPositive(pseudoScalarId)
            ? unDual
            : unDual.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EDual(IndexSet basisBladeId, uint vSpaceDimensions)
    {
        return EDual(basisBladeId, (int)vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Dual(IndexSet basisBladeId, int vSpaceDimensions)
    {
        Debug.Assert(
            basisBladeId.IsEmptySet || 
            basisBladeId.LastIndex < vSpaceDimensions
        );

        var pseudoScalarId = GetBasisPseudoScalarId(vSpaceDimensions);
        var pseudoScalarSignature = Signature(pseudoScalarId);

        if (pseudoScalarSignature.IsZero)
            throw new DivideByZeroException();

        var unDual = Gp(basisBladeId, pseudoScalarId);

        return pseudoScalarSignature.IsPositive
            ? unDual
            : unDual.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Dual(IndexSet basisBladeId, uint vSpaceDimensions)
    {
        return Dual(basisBladeId, (int)vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EUnDual(IndexSet basisBladeId, int vSpaceDimensions)
    {
        Debug.Assert(
            basisBladeId.IsEmptySet || 
            basisBladeId.LastIndex < vSpaceDimensions
        );

        var pseudoScalarId = GetBasisPseudoScalarId(vSpaceDimensions);

        return EGp(basisBladeId, pseudoScalarId);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EUnDual(IndexSet basisBladeId, uint vSpaceDimensions)
    {
        return EUnDual(basisBladeId, (int)vSpaceDimensions);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade UnDual(IndexSet basisBladeId, int vSpaceDimensions)
    {
        Debug.Assert(
            basisBladeId.IsEmptySet || 
            basisBladeId.LastIndex < vSpaceDimensions
        );

        var pseudoScalarId = GetBasisPseudoScalarId(vSpaceDimensions);

        return Gp(basisBladeId, pseudoScalarId);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade UnDual(IndexSet basisBladeId, uint vSpaceDimensions)
    {
        return UnDual(basisBladeId, (int)vSpaceDimensions);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EGp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        var (isNegative, id) =
            basisBladeId1.EGpIsNegativeId(basisBladeId2);

        var basisBlade = new XGaBasisBlade(this, id);

        return isNegative
            ? new XGaSignedBasisBlade(basisBlade, IntegerSign.Negative)
            : basisBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Gp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        if (IsEuclidean)
            return EGp(basisBladeId1, basisBladeId2);

        var meetBasisBladeSignature =
            MeetSignature(basisBladeId1, basisBladeId2);

        if (meetBasisBladeSignature.IsZero)
            return ZeroBasisScalar;

        var (isNegative, id) =
            basisBladeId1.EGpIsNegativeId(basisBladeId2);

        var basisBlade = new XGaBasisBlade(this, id);

        return isNegative == meetBasisBladeSignature.IsNegative
            ? basisBlade
            : new XGaSignedBasisBlade(basisBlade, IntegerSign.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Op(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.OpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade ESp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.ESpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Sp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.ESpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }
    
    /// <summary>
    /// Hermitian Scalar Product
    /// See paper "On SVD and Polar Decomposition in Real and Complexified Clifford Algebras"
    /// </summary>
    /// <param name="basisBladeId1"></param>
    /// <param name="basisBladeId2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade HSp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.ESpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2).Conjugate()
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade ELcp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.ELcpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Lcp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.ELcpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade ERcp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.ERcpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Rcp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.ERcpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EFdp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.EFdpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Fdp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.EFdpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EHip(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.EHipIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Hip(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.EHipIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EAcp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.EAcpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Acp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.EAcpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade ECp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.ECpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Cp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.ECpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

}