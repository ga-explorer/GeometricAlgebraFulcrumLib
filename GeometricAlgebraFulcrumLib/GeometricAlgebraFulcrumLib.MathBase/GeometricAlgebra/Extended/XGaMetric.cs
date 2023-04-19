using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Basis;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended;

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

        BasisScalar = new XGaBasisBlade(this, EmptyIndexSet.Instance);
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
    public IIndexSet GetBasisScalarId()
    {
        return EmptyIndexSet.Instance;
    }

    public IEnumerable<IIndexSet> GetBasisVectorIds(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        for (var index = 0; index < vSpaceDimensions; index++)
            yield return index.IndexToIndexSet();
    }
        
    public IEnumerable<IIndexSet> GetBasisBivectorIds(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        for (var index1 = 0; index1 < vSpaceDimensions - 1; index1++)
        for (var index2 = index1 + 1; index2 < vSpaceDimensions; index2++)
            yield return IndexSetUtils.IndexPairToIndexSet(index1, index2);
    }
        
    public IEnumerable<IIndexSet> GetBasisKVectorIds(int vSpaceDimensions, int grade)
    {
        if (grade < 0)
            throw new ArgumentOutOfRangeException(nameof(grade));
            
        if (vSpaceDimensions < grade)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        if (grade == vSpaceDimensions)
            return new [] { GetBasisPseudoScalarId(vSpaceDimensions) };

        return grade switch
        {
            0 => new IIndexSet[] { EmptyIndexSet.Instance },
            1 => GetBasisVectorIds(vSpaceDimensions),
            2 => GetBasisBivectorIds(vSpaceDimensions),
            _ => vSpaceDimensions <= 64
                ? vSpaceDimensions
                    .GetBinomialCoefficient(grade)
                    .GetRange()
                    .Select(index => (IIndexSet) BasisBladeUtils.BasisBladeGradeIndexToId((uint) grade, index).BitPatternToUInt64IndexSet())
                : vSpaceDimensions
                    .GetRange()
                    .ToArray()
                    .GetCombinationsDistinct(grade)
                    .Select(c => c.ToImmutableSortedSet().ToIndexSet())
        };

        //var indexList = 
        //    vSpaceDimensions.GetRange().ToArray();

        //foreach (var c in GetKCombinations(indexList, grade))
        //{
        //    //Console.WriteLine(c.Concatenate(", ", "<", ">"));
        //    yield return new XGaBasisKVector(this, c.ToImmutableSortedSet());
        //}

    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IIndexSet GetBasisPseudoScalarId(int vSpaceDimensions)
    {
        return vSpaceDimensions switch
        {
            64 => ulong.MaxValue.BitPatternToUInt64IndexSet(),

            < 64 => ((1UL << vSpaceDimensions) - 1UL).BitPatternToUInt64IndexSet(),

            _ => vSpaceDimensions
                .GetRange()
                .ToImmutableSortedSet()
                .ToSparseIndexSet()
        };
    }

    public IEnumerable<IIndexSet> GetBasisBladeIds(int vSpaceDimensions)
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

    public bool SignatureIsPositive(IIndexSet indexSet)
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

    public bool SignatureIsNegative(IIndexSet indexSet)
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
    public bool SignatureIsZero(IIndexSet indexSet)
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
    public IntegerSign Signature(IIndexSet indexSet)
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
    public IntegerSign MeetSignature(IIndexSet basisBlade1, IIndexSet basisBlade2)
    {
        if (IsEuclidean) return IntegerSign.Positive;

        return Signature(
            basisBlade1.Intersect(basisBlade2)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ConjugateSign(IIndexSet basisBlade)
    {
        return Signature(basisBlade) *
               basisBlade.Count.ReverseSignOfGrade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GradeInvolutionSign(IIndexSet basisBlade)
    {
        return basisBlade.Count.GradeInvolutionSignOfGrade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ReverseSign(IIndexSet basisBlade)
    {
        return basisBlade.Count.ReverseSignOfGrade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign CliffordConjugateSign(IIndexSet basisBlade)
    {
        return basisBlade.Count.CliffordConjugateSignOfGrade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSquaredSign(IIndexSet basisBlade)
    {
        return basisBlade.EGpIsNegative(basisBlade)
            ? IntegerSign.Negative
            : IntegerSign.Positive;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpReverseSign(IIndexSet basisBlade)
    {
        return IntegerSign.Positive;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GpSquaredSign(IIndexSet basisBlade)
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
    public IntegerSign GpReverseSign(IIndexSet basisBlade)
    {
        return Signature(basisBlade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ESpSquaredSign(IIndexSet basisBlade)
    {
        return EGpSquaredSign(basisBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ENormSquaredSign(IIndexSet basisBlade)
    {
        return IntegerSign.Positive;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign SpSquaredSign(IIndexSet basisBlade)
    {
        return GpSquaredSign(basisBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign NormSquaredSign(IIndexSet basisBlade)
    {
        return Signature(basisBlade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
    {
        return basisBlade1.EGpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpReverseSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
    {
        return basisBlade1.EGpReverseSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GpSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
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
    public IntegerSign GpReverseSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
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
    public IntegerSign OpSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
    {
        return basisBlade1.OpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign SpSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
    {
        return basisBlade1.ESpIsNonZero(basisBlade2)
            ? GpSquaredSign(basisBlade1)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ELcpSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
    {
        return basisBlade1.ELcpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign LcpSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
    {
        return basisBlade1.ELcpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ERcpSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
    {
        return basisBlade1.ERcpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign RcpSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
    {
        return basisBlade1.ERcpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EFdpSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
    {
        return basisBlade1.EFdpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign FdpSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
    {
        return basisBlade1.EFdpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EHipSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
    {
        return basisBlade1.EHipSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign HipSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
    {
        return basisBlade1.EHipIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EAcpSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
    {
        return basisBlade1.EAcpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign AcpSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
    {
        return basisBlade1.EAcpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ECpSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
    {
        return basisBlade1.ECpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign CpSign(IIndexSet basisBlade1, IIndexSet basisBlade2)
    {
        return basisBlade1.ECpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EGp(UInt64IndexSet basisBladeId1, UInt64IndexSet basisBladeId2)
    {
        var (isNegative, id) =
            basisBladeId1.EGpIsNegativeId(basisBladeId2);

        var basisBlade = new XGaBasisBlade(this, id);

        return isNegative
            ? new XGaSignedBasisBlade(basisBlade, IntegerSign.Negative)
            : basisBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EGp(IIndexSet basisBladeId1, IIndexSet basisBladeId2)
    {
        var (isNegative, id) =
            basisBladeId1.EGpIsNegativeId(basisBladeId2);

        var basisBlade = new XGaBasisBlade(this, id);

        return isNegative
            ? new XGaSignedBasisBlade(basisBlade, IntegerSign.Negative)
            : basisBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Gp(IIndexSet basisBladeId1, IIndexSet basisBladeId2)
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
    public IXGaSignedBasisBlade Op(IIndexSet basisBladeId1, IIndexSet basisBladeId2)
    {
        return basisBladeId1.OpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade ESp(IIndexSet basisBladeId1, IIndexSet basisBladeId2)
    {
        return basisBladeId1.ESpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Sp(IIndexSet basisBladeId1, IIndexSet basisBladeId2)
    {
        return basisBladeId1.ESpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade ELcp(IIndexSet basisBladeId1, IIndexSet basisBladeId2)
    {
        return basisBladeId1.ELcpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Lcp(IIndexSet basisBladeId1, IIndexSet basisBladeId2)
    {
        return basisBladeId1.ELcpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade ERcp(IIndexSet basisBladeId1, IIndexSet basisBladeId2)
    {
        return basisBladeId1.ERcpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Rcp(IIndexSet basisBladeId1, IIndexSet basisBladeId2)
    {
        return basisBladeId1.ERcpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EFdp(IIndexSet basisBladeId1, IIndexSet basisBladeId2)
    {
        return basisBladeId1.EFdpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Fdp(IIndexSet basisBladeId1, IIndexSet basisBladeId2)
    {
        return basisBladeId1.EFdpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EHip(IIndexSet basisBladeId1, IIndexSet basisBladeId2)
    {
        return basisBladeId1.EHipIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Hip(IIndexSet basisBladeId1, IIndexSet basisBladeId2)
    {
        return basisBladeId1.EHipIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EAcp(IIndexSet basisBladeId1, IIndexSet basisBladeId2)
    {
        return basisBladeId1.EAcpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Acp(IIndexSet basisBladeId1, IIndexSet basisBladeId2)
    {
        return basisBladeId1.EAcpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade ECp(IIndexSet basisBladeId1, IIndexSet basisBladeId2)
    {
        return basisBladeId1.ECpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Cp(IIndexSet basisBladeId1, IIndexSet basisBladeId2)
    {
        return basisBladeId1.ECpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

}