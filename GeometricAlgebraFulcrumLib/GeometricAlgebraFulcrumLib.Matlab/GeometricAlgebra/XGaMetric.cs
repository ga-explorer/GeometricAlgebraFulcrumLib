using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra;

/// <summary>
/// This class holds information about a metric of a set of
/// GA basis blades with orthonormal signature (p, q, r) where
/// p is the number of basis vectors with +1 signature (arbitrary and not specified),
/// q is the number of basis vectors with -1 signature, and
/// r is the number of basis vectors with 0 signature.
/// </summary>
public abstract class XGaMetric
{
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

    public XGaBasisBlade UnitBasisScalar { get; }

    public XGaSignedBasisBlade ZeroBasisScalar { get; }

    public IXGaSignedBasisBlade PositiveBasisScalar
        => UnitBasisScalar;

    public IXGaSignedBasisBlade NegativeBasisScalar { get; }


    
    protected XGaMetric(int negativeCount, int zeroCount)
    {
        if (negativeCount < 0 || zeroCount < 0)
            throw new ArgumentException();

        NegativeSignatureBasisCount = negativeCount;
        ZeroSignatureBasisCount = zeroCount;
        NonEuclideanBasisCount = negativeCount + zeroCount;

        IsEuclidean = NonEuclideanBasisCount == 0;
        IsNonEuclidean = !IsEuclidean;

        UnitBasisScalar = new XGaBasisBlade(this, IndexSet.EmptySet);
        ZeroBasisScalar = new XGaSignedBasisBlade(UnitBasisScalar, IntegerSign.Zero);
        NegativeBasisScalar = new XGaSignedBasisBlade(UnitBasisScalar, IntegerSign.Negative);
    }


    
    public bool HasSameSignature(XGaMetric metric)
    {
        return NegativeSignatureBasisCount == metric.NegativeSignatureBasisCount &&
               ZeroSignatureBasisCount == metric.ZeroSignatureBasisCount;
    }

    
    public bool IsValid(int index)
    {
        return index >= 0;
    }

    
    public bool IsValid(int index1, int index2)
    {
        return index1 >= 0 && index1 < index2;
    }

    
    public bool IsValid(params int[] indexArray)
    {
        return indexArray.All(IsValid);
    }

    
    public bool IsValid(IEnumerable<int> indexList)
    {
        return indexList.All(IsValid);
    }


    
    public IndexSet GetBasisScalarId()
    {
        return IndexSet.EmptySet;
    }

    public IEnumerable<IndexSet> GetBasisVectorIds(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        for (var index = 0; index < vSpaceDimensions; index++)
            yield return index.ToUnitIndexSet();
    }
        
    public IEnumerable<IndexSet> GetBasisBivectorIds(int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        for (var index1 = 0; index1 < vSpaceDimensions - 1; index1++)
        for (var index2 = index1 + 1; index2 < vSpaceDimensions; index2++)
            yield return IndexSet.CreatePair(index1, index2);
    }
        
    public IEnumerable<IndexSet> GetBasisKVectorIds(int vSpaceDimensions, int grade)
    {
        if (grade < 0)
            throw new ArgumentOutOfRangeException(nameof(grade));
            
        if (grade > vSpaceDimensions)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        if (grade == vSpaceDimensions)
            return [GetBasisPseudoScalarId(vSpaceDimensions)];

        return grade switch
        {
            0 => [IndexSet.EmptySet],
            1 => GetBasisVectorIds(vSpaceDimensions),
            2 => GetBasisBivectorIds(vSpaceDimensions),
            _ => vSpaceDimensions.ToDenseIndexSet().GetSubsetsOfSize(grade)
        };
    }
        
    
    public IndexSet GetBasisPseudoScalarId(int vSpaceDimensions)
    {
        return vSpaceDimensions.ToDenseIndexSet();
    }

    
    public IEnumerable<IndexSet> GetBasisBladeIds(int vSpaceDimensions)
    {
        return vSpaceDimensions.ToDenseIndexSet().GetSubsets();
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
    

    
    public IntegerSign Signature(int index)
    {
        Debug.Assert(IsValid(index));

        if (index >= NonEuclideanBasisCount)
            return IntegerSign.Positive;

        if (index < NegativeSignatureBasisCount)
            return IntegerSign.Negative;

        return IntegerSign.Zero;
    }

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

    
    public IntegerSign MeetSignature(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        if (IsEuclidean) return IntegerSign.Positive;

        return Signature(
            basisBlade1.SetIntersect(basisBlade2)
        );
    }

    
    
    public XGaFloat64Processor CreateProcessor(IXGaFloat64ProcessorContainer scalarProcessor)
    {
        var processor = XGaFloat64Processor.Create(
            NegativeSignatureBasisCount, 
            ZeroSignatureBasisCount
        );

        scalarProcessor.AttachXGaProcessor(processor);

        return processor;
    }
    

    
    public IntegerSign GradeInvolutionSign(IndexSet basisBlade)
    {
        return basisBlade.Count.GradeInvolutionSignOfGrade();
    }

    
    public IntegerSign ReverseSign(IndexSet basisBlade)
    {
        return basisBlade.Count.ReverseSignOfGrade();
    }

    
    public IntegerSign CliffordConjugateSign(IndexSet basisBlade)
    {
        return basisBlade.Count.CliffordConjugateSignOfGrade();
    }
    
    /// <summary>
    /// See paper: "On SVD and Polar Decomposition in Real and Complexified Clifford Algebras"
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    
    public IntegerSign HermitianConjugateSign(IndexSet basisBlade)
    {
        var signature = Signature(basisBlade);

        if (signature.IsZero)
            throw new DivideByZeroException();

        return signature * basisBlade.Count.ReverseSignOfGrade();
    }


    
    public IntegerSign EGpSquaredSign(IndexSet basisBlade)
    {
        return basisBlade.EGpSquaredIsNegative()
            ? IntegerSign.Negative
            : IntegerSign.Positive;
    }

    
    public IntegerSign EGpReverseSign(IndexSet _)
    {
        return IntegerSign.Positive;
    }

    
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
    
    
    public IntegerSign GpReverseSign(IndexSet basisBlade)
    {
        return Signature(basisBlade);
    }


    
    public IntegerSign ESpSquaredSign(IndexSet basisBlade)
    {
        return EGpSquaredSign(basisBlade);
    }

    
    public IntegerSign ENormSquaredSign(IndexSet _)
    {
        return IntegerSign.Positive;
    }

    
    public IntegerSign SpSquaredSign(IndexSet basisBlade)
    {
        return GpSquaredSign(basisBlade);
    }

    
    public IntegerSign NormSquaredSign(IndexSet basisBlade)
    {
        return Signature(basisBlade);
    }


    
    public IntegerSign EGpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.EGpSign(basisBlade2);
    }

    
    public IntegerSign EGpReverseSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.EGpReverseSign(basisBlade2);
    }

    
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

    
    public IntegerSign OpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.OpSign(basisBlade2);
    }

    
    public IntegerSign SpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.ESpIsNonZero(basisBlade2)
            ? GpSquaredSign(basisBlade1)
            : IntegerSign.Zero;
    }

    
    public IntegerSign ELcpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.ELcpSign(basisBlade2);
    }

    
    public IntegerSign LcpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.ELcpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    
    public IntegerSign ERcpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.ERcpSign(basisBlade2);
    }

    
    public IntegerSign RcpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.ERcpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    
    public IntegerSign EFdpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.EFdpSign(basisBlade2);
    }

    
    public IntegerSign FdpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.EFdpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    
    public IntegerSign EHipSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.EHipSign(basisBlade2);
    }

    
    public IntegerSign HipSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.EHipIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    
    public IntegerSign EAcpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.EAcpSign(basisBlade2);
    }

    
    public IntegerSign AcpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.EAcpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }

    
    public IntegerSign ECpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.ECpSign(basisBlade2);
    }

    
    public IntegerSign CpSign(IndexSet basisBlade1, IndexSet basisBlade2)
    {
        return basisBlade1.ECpIsNonZero(basisBlade2)
            ? GpSign(basisBlade1, basisBlade2)
            : IntegerSign.Zero;
    }
    

    
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

    
    public IXGaSignedBasisBlade EDual(IndexSet basisBladeId, uint vSpaceDimensions)
    {
        return EDual(basisBladeId, (int)vSpaceDimensions);
    }

    
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
    
    
    public IXGaSignedBasisBlade Dual(IndexSet basisBladeId, uint vSpaceDimensions)
    {
        return Dual(basisBladeId, (int)vSpaceDimensions);
    }

    
    public IXGaSignedBasisBlade EUnDual(IndexSet basisBladeId, int vSpaceDimensions)
    {
        Debug.Assert(
            basisBladeId.IsEmptySet || 
            basisBladeId.LastIndex < vSpaceDimensions
        );

        var pseudoScalarId = GetBasisPseudoScalarId(vSpaceDimensions);

        return EGp(basisBladeId, pseudoScalarId);
    }
    
    
    public IXGaSignedBasisBlade EUnDual(IndexSet basisBladeId, uint vSpaceDimensions)
    {
        return EUnDual(basisBladeId, (int)vSpaceDimensions);
    }

    
    public IXGaSignedBasisBlade UnDual(IndexSet basisBladeId, int vSpaceDimensions)
    {
        Debug.Assert(
            basisBladeId.IsEmptySet || 
            basisBladeId.LastIndex < vSpaceDimensions
        );

        var pseudoScalarId = GetBasisPseudoScalarId(vSpaceDimensions);

        return Gp(basisBladeId, pseudoScalarId);
    }
    
    
    public IXGaSignedBasisBlade UnDual(IndexSet basisBladeId, uint vSpaceDimensions)
    {
        return UnDual(basisBladeId, (int)vSpaceDimensions);
    }


    
    public IXGaSignedBasisBlade EGp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        var (isNegative, id) =
            basisBladeId1.EGpIsNegativeId(basisBladeId2);

        var basisBlade = new XGaBasisBlade(this, id);

        return isNegative
            ? new XGaSignedBasisBlade(basisBlade, IntegerSign.Negative)
            : basisBlade;
    }

    
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

    
    public IXGaSignedBasisBlade Op(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.OpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    
    public IXGaSignedBasisBlade ESp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.ESpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    
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
    
    public IXGaSignedBasisBlade HSp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.ESpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2).Conjugate()
            : ZeroBasisScalar;
    }

    
    public IXGaSignedBasisBlade ELcp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.ELcpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    
    public IXGaSignedBasisBlade Lcp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.ELcpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    
    public IXGaSignedBasisBlade ERcp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.ERcpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    
    public IXGaSignedBasisBlade Rcp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.ERcpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    
    public IXGaSignedBasisBlade EFdp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.EFdpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    
    public IXGaSignedBasisBlade Fdp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.EFdpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    
    public IXGaSignedBasisBlade EHip(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.EHipIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    
    public IXGaSignedBasisBlade Hip(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.EHipIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    
    public IXGaSignedBasisBlade EAcp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.EAcpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    
    public IXGaSignedBasisBlade Acp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.EAcpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    
    public IXGaSignedBasisBlade ECp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.ECpIsNonZero(basisBladeId2)
            ? EGp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }

    
    public IXGaSignedBasisBlade Cp(IndexSet basisBladeId1, IndexSet basisBladeId2)
    {
        return basisBladeId1.ECpIsNonZero(basisBladeId2)
            ? Gp(basisBladeId1, basisBladeId2)
            : ZeroBasisScalar;
    }
    
    

    
    public XGaBasisBlade CreateBasisScalar()
    {
        return UnitBasisScalar;
    }

    
    public XGaBasisBlade BasisVector(int index)
    {
        return new XGaBasisBlade(
            this, 
            index.ToUnitIndexSet()
        );
    }
        
    
    public XGaBasisBlade BasisBivector(int index1, int index2)
    {
        if (index1 >= index2)
            throw new InvalidOperationException();

        return new XGaBasisBlade(
            this, 
            IndexSet.CreatePair(index1, index2)
        );
    }
    
    
    public XGaBasisBlade BasisBivector(IPair<int> indexPair)
    {
        return BasisBivector(indexPair.Item1, indexPair.Item2);
    }

    
    public XGaBasisBlade BasisTrivector(int index1, int index2, int index3)
    {
        if (index1 >= index2 || index2 >= index3)
            throw new InvalidOperationException();

        return new XGaBasisBlade(
            this, 
            IndexSet.CreateTriplet(index1, index2, index3)
        );
    }
    
    
    public XGaBasisBlade BasisTrivector(ITriplet<int> indexTriplet)
    {
        return BasisTrivector(
            indexTriplet.Item1,
            indexTriplet.Item2,
            indexTriplet.Item3
        );
    }

    
    public XGaBasisBlade BasisBlade(params int[] basisVectorIndexList)
    {
        if (basisVectorIndexList.Length == 0)
            return UnitBasisScalar;

        if (basisVectorIndexList.Length == 1)
            return BasisVector(basisVectorIndexList[0]);
        
        if (basisVectorIndexList.Length == 2)
            return BasisBivector(basisVectorIndexList[0], basisVectorIndexList[1]);

        var (indexSet, sign) = 
            basisVectorIndexList.GetBasisBladeIdSign();

        return sign.IsPositive 
            ? new XGaBasisBlade(this, indexSet) 
            : throw new InvalidOperationException();
    }

    
    public XGaBasisBlade BasisBlade(IndexSet id)
    {
        return new XGaBasisBlade(
            this, 
            id
        );
    }

    
    public XGaBasisBlade BasisBlade(int grade, int index)
    {
        var indexSet = 
            BasisBladeUtils.BasisBladeGradeIndexToId((uint) grade, (ulong) index);

        return new XGaBasisBlade(this, indexSet);
    }
          
    
    public XGaBasisBlade BasisBlade(int grade, ulong index)
    {
        var indexSet = 
            BasisBladeUtils.BasisBladeGradeIndexToId((uint) grade, index);

        return new XGaBasisBlade(this, indexSet);
    }
       
    
    public XGaBasisBlade BasisBlade(uint grade, ulong index)
    {
        var indexSet = 
            BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);

        return new XGaBasisBlade(this, indexSet);
    }
    
    
    public XGaBasisBlade BasisPseudoScalar(int vSpaceDimensions)
    {
        var id = GetBasisPseudoScalarId(vSpaceDimensions);

        return new XGaBasisBlade(this, id);
    }
    
    
    public IXGaSignedBasisBlade BasisPseudoScalar(int vSpaceDimensions, IntegerSign sign)
    {
        var id = GetBasisPseudoScalarId(vSpaceDimensions);

        return sign.IsPositive
            ? new XGaBasisBlade(this, id)
            : new XGaSignedBasisBlade(this, id, sign);
    }

    
    public IXGaSignedBasisBlade BasisPseudoScalarReverse(int vSpaceDimensions)
    {
        var id = GetBasisPseudoScalarId(vSpaceDimensions);

        return vSpaceDimensions.ReverseIsNegativeOfGrade()
            ? new XGaSignedBasisBlade(this, id, IntegerSign.Negative)
            : new XGaBasisBlade(this, id);
    }
    
    
    public IXGaSignedBasisBlade BasisPseudoScalarConjugate(int vSpaceDimensions)
    {
        var id = GetBasisPseudoScalarId(vSpaceDimensions);
        var sign = HermitianConjugateSign(id);

        if (sign.IsZero)
            throw new DivideByZeroException();

        return sign.IsNegative
            ? new XGaSignedBasisBlade(this, id, IntegerSign.Negative)
            : new XGaBasisBlade(this, id);
    }

    
    public IXGaSignedBasisBlade BasisPseudoScalarEInverse(int vSpaceDimensions)
    {
        var id = GetBasisPseudoScalarId(vSpaceDimensions);
        var sign = EGpSquaredSign(id);
        
        return sign.IsNegative
            ? new XGaSignedBasisBlade(this, id, IntegerSign.Negative)
            : new XGaBasisBlade(this, id);
    }

    
    public IXGaSignedBasisBlade BasisPseudoScalarInverse(int vSpaceDimensions)
    {
        var id = GetBasisPseudoScalarId(vSpaceDimensions);
        var sign = GpSquaredSign(id);

        if (sign.IsZero)
            throw new DivideByZeroException();

        return sign.IsNegative
            ? new XGaSignedBasisBlade(this, id, IntegerSign.Negative)
            : new XGaBasisBlade(this, id);
    }


    
    public IEnumerable<XGaBasisBlade> GetBasisVectors(int vSpaceDimensions)
    {
        return GetBasisVectorIds(vSpaceDimensions)
            .Select(id => new XGaBasisBlade(this, id));
    }
    
    
    public IEnumerable<XGaBasisBlade> GetBasisBivectors(int vSpaceDimensions)
    {
        return GetBasisBivectorIds(vSpaceDimensions)
            .Select(id => new XGaBasisBlade(this, id));
    }

    
    public IEnumerable<XGaBasisBlade> GetBasisKVectors(int vSpaceDimensions, int grade)
    {
        return GetBasisKVectorIds(vSpaceDimensions, grade)
            .Select(id => new XGaBasisBlade(this, id));
    }

    
    public IEnumerable<XGaBasisBlade> GetBasisBlades(int vSpaceDimensions)
    {
        return GetBasisBladeIds(vSpaceDimensions)
            .Select(id => new XGaBasisBlade(this, id));
    }


    
    public IXGaSignedBasisBlade SignedBasisScalar(IntegerSign sign)
    {
        return sign.Value switch
        {
            0 => ZeroBasisScalar,
            > 0 => UnitBasisScalar,
            _ => NegativeBasisScalar
        };
    }


    
    public XGaSignedBasisBlade ZeroBasisVector()
    {
        var basisBlade = BasisVector(0);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Zero);
    }

    
    public XGaSignedBasisBlade ZeroBasisVector(int index)
    {
        var basisBlade = BasisVector(index);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Zero);
    }

    
    public XGaSignedBasisBlade PositiveBasisVector(int index)
    {
        var basisBlade = BasisVector(index);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Positive);
    }

    
    public XGaSignedBasisBlade NegativeBasisVector(int index)
    {
        var basisBlade = BasisVector(index);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Negative);
    }

    
    public XGaSignedBasisBlade SignedBasisVector(int index, IntegerSign sign)
    {
        return new XGaSignedBasisBlade(
            BasisVector(index),
            sign
        );
    }


    
    public XGaSignedBasisBlade ZeroBasisBivector()
    {
        var basisBlade = BasisBivector(0, 1);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Zero);
    }

    
    public XGaSignedBasisBlade ZeroBasisBivector(int index1, int index2)
    {
        var basisBlade = index1 < index2
            ? BasisBivector(index1, index2)
            : BasisBivector(index2, index1);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Zero);
    }

    
    public XGaSignedBasisBlade PositiveBasisBivector(int index1, int index2)
    {
        var sign = index1 < index2 
            ? IntegerSign.Positive 
            : IntegerSign.Negative;

        var basisBlade = index1 < index2
            ? BasisBivector(index1, index2)
            : BasisBivector(index2, index1);

        return new XGaSignedBasisBlade(basisBlade, sign);
    }

    
    public XGaSignedBasisBlade NegativeBasisBivector(int index1, int index2)
    {
        var sign = index1 < index2 
            ? IntegerSign.Negative 
            : IntegerSign.Positive;

        var basisBlade = index1 < index2
            ? BasisBivector(index1, index2)
            : BasisBivector(index2, index1);

        return new XGaSignedBasisBlade(basisBlade, sign);
    }

    
    public XGaSignedBasisBlade SignedBasisBivector(int index1, int index2, IntegerSign sign)
    {
        if (index1 > index2) sign = -sign;

        var basisBlade = index1 < index2
            ? BasisBivector(index1, index2)
            : BasisBivector(index2, index1);

        return new XGaSignedBasisBlade(basisBlade, sign);
    }


    
    public XGaSignedBasisBlade ZeroBasisKVector(IndexSet indexSet)
    {
        var basisBlade = BasisBlade(indexSet);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Zero);
    }

    
    public XGaSignedBasisBlade PositiveBasisKVector(IndexSet indexSet)
    {
        var basisBlade = BasisBlade(indexSet);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Positive);
    }

    
    public XGaSignedBasisBlade NegativeBasisKVector(IndexSet indexSet)
    {
        var basisBlade = BasisBlade(indexSet);

        return new XGaSignedBasisBlade(basisBlade, IntegerSign.Negative);
    }

    
    public XGaSignedBasisBlade SignedBasisKVector(IndexSet indexSet, IntegerSign sign)
    {
        var basisBlade = BasisBlade(indexSet);

        return new XGaSignedBasisBlade(basisBlade, sign);
    }


    
    public XGaSignedBasisBlade ZeroBasisBlade(int grade)
    {
        return grade switch
        {
            < 0 => throw new ArgumentOutOfRangeException(nameof(grade)),
            0 => ZeroBasisScalar,
            1 => ZeroBasisVector(),
            2 => ZeroBasisBivector(),
            _ => new XGaSignedBasisBlade(
                BasisBlade(grade.ToDenseIndexSet()), 
                IntegerSign.Zero
            )
        };
    }



    
    public IXGaSignedBasisBlade EGp(int index1, int index2)
    {
        return BasisVector(index1).EGp(
            BasisVector(index2)
        );
    }
    
    public IXGaSignedBasisBlade EGp(params int[] indexList)
    {
        IXGaSignedBasisBlade basisBlade = 
            BasisVector(indexList[0]);

        foreach (var index in indexList.Skip(1))
        {
            basisBlade = basisBlade.EGp(
                BasisVector(index)
            );

            //if (basisBlade.IsZero) 
            //    return this.ZeroBasisScalar;
        }
        
        return basisBlade;
    }
    
    public IXGaSignedBasisBlade EGp(IReadOnlyList<int> indexList)
    {
        IXGaSignedBasisBlade basisBlade = 
            BasisVector(indexList[0]);

        foreach (var index in indexList.Skip(1))
        {
            basisBlade = basisBlade.EGp(
                BasisVector(index)
            );

            //if (basisBlade.IsZero) 
            //    return this.ZeroBasisScalar;
        }
        
        return basisBlade;
    }
        

    
    public IXGaSignedBasisBlade Gp(int index1, int index2)
    {
        return BasisVector(index1).Gp(
            BasisVector(index2)
        );
    }

    public IXGaSignedBasisBlade Gp(params int[] indexList)
    {
        IXGaSignedBasisBlade basisBlade = 
            BasisVector(indexList[0]);

        foreach (var index in indexList.Skip(1))
        {
            basisBlade = basisBlade.Gp(
                BasisVector(index)
            );

            if (basisBlade.IsZero) 
                return ZeroBasisScalar;
        }
        
        return basisBlade;
    }

    public IXGaSignedBasisBlade Gp(IReadOnlyList<int> indexList)
    {
        IXGaSignedBasisBlade basisBlade = 
            BasisVector(indexList[0]);

        foreach (var index in indexList.Skip(1))
        {
            basisBlade = basisBlade.Gp(
                BasisVector(index)
            );

            if (basisBlade.IsZero) 
                return ZeroBasisScalar;
        }
        
        return basisBlade;
    }
        
    
    
    public IXGaSignedBasisBlade Op()
    {
        return UnitBasisScalar;
    }

    
    public IXGaSignedBasisBlade Op(int index)
    {
        return BasisVector(index);
    }

    
    public IXGaSignedBasisBlade Op(int index1, int index2)
    {
        if (index1 == index2)
            return ZeroBasisScalar;

        if (index1 < index2)
            return new XGaBasisBlade(
                this, 
                IndexSet.CreatePair(index1, index2)
            );

        return new XGaSignedBasisBlade(
            new XGaBasisBlade(
                this, 
                IndexSet.CreatePair(index2, index1)
            ),
            IntegerSign.Negative
        );
    }
    
    
    public IXGaSignedBasisBlade Op(params int[] indexList)
    {
        if (indexList.Length == 0)
            return UnitBasisScalar;

        if (indexList.Length == 1)
            return BasisVector(indexList[0]);
        
        if (indexList.Length == 2)
            return Op(indexList[0], indexList[1]);

        if (!indexList.TryGetBasisBladeIdSign(out var indexSet, out var sign))
            return ZeroBasisScalar;

        var basisBlade = new XGaBasisBlade(this, indexSet);

        return sign.IsPositive
            ? basisBlade
            : new XGaSignedBasisBlade(
                basisBlade,
                IntegerSign.Negative
            );
    }

    public IXGaSignedBasisBlade Op1(params int[] indexList)
    {
        if (indexList.Length == 0)
            return UnitBasisScalar;

        IXGaSignedBasisBlade basisBlade =
            BasisVector(indexList[0]);

        foreach (var index in indexList.Skip(1))
        {
            basisBlade = basisBlade.Op(
                BasisVector(index)
            );

            if (basisBlade.IsZero)
                return ZeroBasisScalar;
        }

        return basisBlade;
    }

    
    public IXGaSignedBasisBlade Op(IReadOnlyList<int> indexList)
    {
        return Op(indexList.ToArray());
    }


    
    
    public bool IsValidMultivectorDictionary(IReadOnlyDictionary<int, XGaFloat64KVector> gradeKVectorDictionary)
    {
        return gradeKVectorDictionary.Count switch
        {
            0 => gradeKVectorDictionary is EmptyDictionary<int, XGaFloat64KVector>,

            1 => gradeKVectorDictionary is SingleItemDictionary<int, XGaFloat64KVector> dict &&
                 dict.Key >= 0 &&
                 dict.Value.Metric.HasSameSignature(this) &&
                 dict.Value.IsValid(),

            _ => gradeKVectorDictionary.All(p =>
                p.Key >= 0 &&
                p.Value.Metric.HasSameSignature(this) &&
                p.Value.IsValid()
            )
        };
    }

}