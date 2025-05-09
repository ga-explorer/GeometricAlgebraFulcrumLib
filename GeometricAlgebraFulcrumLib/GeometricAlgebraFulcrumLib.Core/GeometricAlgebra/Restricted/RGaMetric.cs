using System.Collections.Immutable;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted;

/// <summary>
/// This class holds information about a metric of a set of
/// GA basis blades with orthonormal signature (p, q, r) where
/// p is the number of basis vectors with +1 signature (arbitrary and not specified),
/// q is the number of basis vectors with -1 signature, and
/// r is the number of basis vectors with 0 signature.
/// </summary>
public abstract class RGaMetric
{
    private readonly ulong _negativeMask;
    private readonly ulong _zeroMask;

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
    
    public RGaBasisBlade BasisScalar { get; }

    public RGaSignedBasisBlade ZeroBasisScalar { get; }

    public IRGaSignedBasisBlade PositiveBasisScalar
        => BasisScalar;

    public RGaSignedBasisBlade NegativeBasisScalar { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected RGaMetric(int negativeCount, int zeroCount)
    {
        if (negativeCount < 0 || zeroCount < 0)
            throw new ArgumentException();

        NegativeSignatureBasisCount = negativeCount;
        ZeroSignatureBasisCount = zeroCount;
        NonEuclideanBasisCount = negativeCount + zeroCount;

        IsEuclidean = NonEuclideanBasisCount == 0;
        IsNonEuclidean = !IsEuclidean;

        BasisScalar = new RGaBasisBlade(this, 0UL);
        ZeroBasisScalar = new RGaSignedBasisBlade(BasisScalar, IntegerSign.Zero);
        NegativeBasisScalar = new RGaSignedBasisBlade(BasisScalar, IntegerSign.Negative);

        _negativeMask = negativeCount.CreateMaskUInt64();
        _zeroMask = negativeCount == 0
            ? zeroCount.CreateMaskUInt64()
            : zeroCount.CreateMaskUInt64() << negativeCount;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasSameSignature(RGaMetric metric)
    {
        return NegativeSignatureBasisCount == metric.NegativeSignatureBasisCount &&
               ZeroSignatureBasisCount == metric.ZeroSignatureBasisCount;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(int index)
    {
        return index is >= 0 and < 64;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid(int index1, int index2)
    {
        return index1 >= 0 && index1 < index2 && index2 < 64;
    }

    public bool IsValid(params int[] indexArray)
    {
        if (indexArray.Length == 0)
            return true;

        var i1 = -1;

        foreach (var index in indexArray)
        {
            if (index <= i1 || index >= 64)
                return false;

            i1 = index;
        }

        return true;
    }

    public bool IsValid(IEnumerable<int> indexList)
    {
        var i1 = -1;

        foreach (var index in indexList)
        {
            if (index <= i1 || index >= 64)
                return false;

            i1 = index;
        }

        return true;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong GetBasisScalarId()
    {
        return 0UL;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong GetBasisPseudoScalarId(int vSpaceDimensions)
    {
        return vSpaceDimensions switch
        {
            < 1 or > 64 => throw new ArgumentOutOfRangeException(),
            < 64 => (1UL << vSpaceDimensions) - 1UL,
            _ => ulong.MaxValue
        };
    }

    public IEnumerable<ulong> GetBasisVectorIds(int vSpaceDimensions)
    {
        if (vSpaceDimensions is < 1 or > 64)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        return vSpaceDimensions.GetRange().Select(index => 1UL << index);
    }
        
    public IEnumerable<ulong> GetBasisBivectorIds(int vSpaceDimensions)
    {
        if (vSpaceDimensions is < 2 or > 64)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        for (var index1 = 0; index1 < vSpaceDimensions - 1; index1++)
        for (var index2 = index1 + 1; index2 < vSpaceDimensions; index2++)
            yield return (1UL << index1) | (1UL << index2);
    }
        
    public IEnumerable<ulong> GetBasisKVectorIds(int vSpaceDimensions, int grade)
    {
        if (grade < 0)
            throw new ArgumentOutOfRangeException(nameof(grade));
            
        if (vSpaceDimensions < grade)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        if (grade == vSpaceDimensions)
            return new [] { GetBasisPseudoScalarId(vSpaceDimensions) };

        return grade switch
        {
            0 => new [] { 0UL },
            1 => GetBasisVectorIds(vSpaceDimensions),
            2 => GetBasisBivectorIds(vSpaceDimensions),
            _ => vSpaceDimensions
                .GetBinomialCoefficient(grade)
                .GetRange()
                .Select(index => BasisBladeUtils.BasisBladeGradeIndexToId((uint) grade, index))
        };
    }
        
    public IEnumerable<ulong> GetBasisBladeIds(int vSpaceDimensions)
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

    public bool SignatureIsPositive(ulong indexSet)
    {
        if (IsEuclidean) return true;

        return (indexSet & _zeroMask) == 0 &&
               BitOperations.PopCount(indexSet & _negativeMask).IsEven();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SignatureIsNegative(int index)
    {
        Debug.Assert(IsValid(index));

        return index < NegativeSignatureBasisCount;
    }

    public bool SignatureIsNegative(ulong indexSet)
    {
        if (IsEuclidean) return false;

        return (indexSet & _zeroMask) == 0 &&
               BitOperations.PopCount(indexSet & _negativeMask).IsOdd();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SignatureIsZero(int index)
    {
        Debug.Assert(IsValid(index));

        return index >= NegativeSignatureBasisCount &&
               index < NonEuclideanBasisCount;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool SignatureIsZero(ulong indexSet)
    {
        if (IsEuclidean) return false;

        return (indexSet & _zeroMask) != 0;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign Signature(int index)
    {
        Debug.Assert(IsValid(index));

        if (index >= NonEuclideanBasisCount)
            return IntegerSign.Positive;

        return index < NegativeSignatureBasisCount
            ? IntegerSign.Negative
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign Signature(ulong indexSet)
    {
        if (IsEuclidean)
            return IntegerSign.Positive;

        if ((indexSet & _zeroMask) != 0)
            return IntegerSign.Zero;

        return BitOperations.PopCount(indexSet & _negativeMask).IsOdd()
            ? IntegerSign.Negative
            : IntegerSign.Positive;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign MeetSignature(ulong basisBlade1, ulong basisBlade2)
    {
        if (IsEuclidean) return IntegerSign.Positive;

        return Signature(
            basisBlade1 & basisBlade2
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GradeInvolutionSign(ulong basisBlade)
    {
        return basisBlade.Grade().GradeInvolutionSignOfGrade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ReverseSign(ulong basisBlade)
    {
        return basisBlade.Grade().ReverseSignOfGrade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign CliffordConjugateSign(ulong basisBlade)
    {
        return basisBlade.Grade().CliffordConjugateSignOfGrade();
    }
    
    /// <summary>
    /// See paper: "On SVD and Polar Decomposition in Real and Complexified Clifford Algebras"
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign HermitianConjugateSign(ulong basisBlade)
    {
        var signature = Signature(basisBlade);

        if (signature.IsZero)
            throw new DivideByZeroException();

        return signature * basisBlade.Grade().ReverseSignOfGrade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSquaredSign(ulong basisBlade)
    {
        return basisBlade.EGpIsNegative(basisBlade)
            ? IntegerSign.Negative
            : IntegerSign.Positive;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpReverseSign(ulong basisBlade)
    {
        return IntegerSign.Positive;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GpSquaredSign(ulong basisBlade)
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
    public IntegerSign GpReverseSign(ulong basisBlade)
    {
        return Signature(basisBlade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ESpSquaredSign(ulong basisBlade)
    {
        return EGpSquaredSign(basisBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ENormSquaredSign(ulong basisBlade)
    {
        return IntegerSign.Positive;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign SpSquaredSign(ulong basisBlade)
    {
        return GpSquaredSign(basisBlade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign NormSquaredSign(ulong basisBlade)
    {
        return Signature(basisBlade);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSign(ulong basisBlade1, ulong basisBlade2)
    {
        return basisBlade1.EGpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpReverseSign(ulong basisBlade1, ulong basisBlade2)
    {
        return basisBlade1.EGpReverseSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GpSign(ulong basisBlade1, ulong basisBlade2)
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
    public IntegerSign GpReverseSign(ulong basisBlade1, ulong basisBlade2)
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
    public IntegerSign OpSign(ulong basisBlade1, ulong basisBlade2)
    {
        return basisBlade1.OpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign SpSign(ulong basisBlade1, ulong basisBlade2)
    {
        return basisBlade1.ESpIsNonZero(basisBlade2)
            ? GpSquaredSign(basisBlade1)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ELcpSign(ulong basisBlade1, ulong basisBlade2)
    {
        return basisBlade1.ELcpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign LcpSign(ulong basisBlade1, ulong basisBlade2)
    {
        return basisBlade1.ELcpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ERcpSign(ulong basisBlade1, ulong basisBlade2)
    {
        return basisBlade1.ERcpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign RcpSign(ulong basisBlade1, ulong basisBlade2)
    {
        return basisBlade1.ERcpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EFdpSign(ulong basisBlade1, ulong basisBlade2)
    {
        return basisBlade1.EFdpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign FdpSign(ulong basisBlade1, ulong basisBlade2)
    {
        return basisBlade1.EFdpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EHipSign(ulong basisBlade1, ulong basisBlade2)
    {
        return basisBlade1.EHipSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign HipSign(ulong basisBlade1, ulong basisBlade2)
    {
        return basisBlade1.EHipIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EAcpSign(ulong basisBlade1, ulong basisBlade2)
    {
        return basisBlade1.EAcpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign AcpSign(ulong basisBlade1, ulong basisBlade2)
    {
        return basisBlade1.EAcpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ECpSign(ulong basisBlade1, ulong basisBlade2)
    {
        return basisBlade1.ECpSign(basisBlade2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign CpSign(ulong basisBlade1, ulong basisBlade2)
    {
        return basisBlade1.ECpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EDual(ulong basisBladeId, int vSpaceDimensions)
    {
        Debug.Assert(
            basisBladeId.IsValidBasisBladeId(vSpaceDimensions)
        );

        var pseudoScalarId = GetBasisPseudoScalarId(vSpaceDimensions);
        var unDual = EGp(basisBladeId, pseudoScalarId);

        return SignatureIsPositive(pseudoScalarId)
            ? unDual
            : unDual.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Dual(ulong basisBladeId, int vSpaceDimensions)
    {
        Debug.Assert(
            basisBladeId.IsValidBasisBladeId(vSpaceDimensions)
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
    public IRGaSignedBasisBlade EUnDual(ulong basisBladeId, int vSpaceDimensions)
    {
        Debug.Assert(
            basisBladeId.IsValidBasisBladeId(vSpaceDimensions)
        );

        var pseudoScalarId = GetBasisPseudoScalarId(vSpaceDimensions);

        return EGp(basisBladeId, pseudoScalarId);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade UnDual(ulong basisBladeId, int vSpaceDimensions)
    {
        Debug.Assert(
            basisBladeId.IsValidBasisBladeId(vSpaceDimensions)
        );

        var pseudoScalarId = GetBasisPseudoScalarId(vSpaceDimensions);

        return Gp(basisBladeId, pseudoScalarId);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EGpNegative(ulong basisBladeId1, ulong basisBladeId2)
    {
        var (isNegative, id) =
            basisBladeId1.EGpIsNegativeId(basisBladeId2);

        var basisBlade = new RGaBasisBlade(this, id);

        return isNegative
            ? basisBlade 
            : new RGaSignedBasisBlade(basisBlade, IntegerSign.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EGp(ulong basisBladeId1, ulong basisBladeId2)
    {
        var (isNegative, id) =
            basisBladeId1.EGpIsNegativeId(basisBladeId2);

        var basisBlade = new RGaBasisBlade(this, id);

        return isNegative
            ? new RGaSignedBasisBlade(basisBlade, IntegerSign.Negative)
            : basisBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Gp(ulong basisBladeId1, ulong basisBladeId2)
    {
        if (IsEuclidean)
            return EGp(basisBladeId1, basisBladeId2);

        var meetBasisBladeSignature =
            MeetSignature(basisBladeId1, basisBladeId2);

        if (meetBasisBladeSignature.IsZero)
            return ZeroBasisScalar;

        var (isNegative, id) =
            basisBladeId1.EGpIsNegativeId(basisBladeId2);

        var basisBlade = new RGaBasisBlade(this, id);

        return isNegative == meetBasisBladeSignature.IsNegative
            ? basisBlade
            : new RGaSignedBasisBlade(basisBlade, IntegerSign.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Op(ulong basisBladeId1, ulong basisBladeId2)
    {
        return basisBladeId1.OpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ESp(ulong basisBladeId1, ulong basisBladeId2)
    {
        return basisBladeId1.ESpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Sp(ulong basisBladeId1, ulong basisBladeId2)
    {
        return basisBladeId1.ESpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ELcp(ulong basisBladeId1, ulong basisBladeId2)
    {
        return basisBladeId1.ELcpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Lcp(ulong basisBladeId1, ulong basisBladeId2)
    {
        return basisBladeId1.ELcpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ERcp(ulong basisBladeId1, ulong basisBladeId2)
    {
        return basisBladeId1.ERcpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Rcp(ulong basisBladeId1, ulong basisBladeId2)
    {
        return basisBladeId1.ERcpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EFdp(ulong basisBladeId1, ulong basisBladeId2)
    {
        return basisBladeId1.EFdpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Fdp(ulong basisBladeId1, ulong basisBladeId2)
    {
        return basisBladeId1.EFdpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EHip(ulong basisBladeId1, ulong basisBladeId2)
    {
        return basisBladeId1.EHipIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Hip(ulong basisBladeId1, ulong basisBladeId2)
    {
        return basisBladeId1.EHipIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EAcp(ulong basisBladeId1, ulong basisBladeId2)
    {
        return basisBladeId1.EAcpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Acp(ulong basisBladeId1, ulong basisBladeId2)
    {
        return basisBladeId1.EAcpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ECp(ulong basisBladeId1, ulong basisBladeId2)
    {
        return basisBladeId1.ECpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Cp(ulong basisBladeId1, ulong basisBladeId2)
    {
        return basisBladeId1.ECpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }
    
    
    public IReadOnlyList<ulong> GetBasisBladeIDsOfGrade(int vSpaceDimensions, int grade)
    {
        if (grade < 0 || grade > vSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(grade));
            
        return ((uint)vSpaceDimensions).GetBinomialCoefficient((uint)grade)
            .GetRange(index =>
                BasisBladeUtils.BasisBladeGradeIndexToId(grade, index)
            ).ToImmutableArray();
    }
    
    public IReadOnlyList<ulong> GetBasisBladeIndicesOfGrade(int vSpaceDimensions, int grade)
    {
        if (grade < 0 || grade > vSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(grade));
            
        return ((uint)vSpaceDimensions).GetBinomialCoefficient((uint)grade)
            .GetRange()
            .ToImmutableArray();
    }

}