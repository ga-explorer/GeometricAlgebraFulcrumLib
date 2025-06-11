using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Structures;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Matlab.Structures.System;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;

/// <summary>
/// This represents a basis blade of arbitrary grade for an extended GA
/// </summary>
public sealed record XGaBasisBlade : 
    IXGaSignedBasisBlade
{
    public XGaMetric Metric { get; }

    public IndexSet Id { get; }

    public IntegerSign Sign 
        => IntegerSign.Positive;

    public XGaBasisBlade GetBasisBlade() => this;

    public bool IsNegative 
        => false;

    public bool IsZero 
        => false;

    public bool IsPositive 
        => true;

    public bool IsNonNegative 
        => true;

    public bool IsNonZero 
        => true;

    public bool IsNonPositive 
        => false;

    public bool IsScalar 
        => Id.IsEmptySet;

    public bool IsVector 
        => Id.IsUnitSet;

    public bool IsBivector 
        => Id.IsPairSet;

    public int VSpaceDimensions 
        => Id.IsEmptySet 
            ? 0 
            : Id.VSpaceDimensions();

    public int Grade 
        => Id.Grade();

    public ulong KvSpaceDimensions 
        => Id.KvSpaceDimensions();


    
    internal XGaBasisBlade(XGaMetric metric, IndexSet basisBladeId)
    {
        Metric = metric;
        Id = basisBladeId;
    }


    
    public bool IsValid()
    {
        return true;
    }
    
    
    public bool IsKVector(int grade)
    {
        return Id.Count == grade;
    }

    
    
    public XGaSignedBasisBlade ToZeroBasisBlade()
    {
        return new XGaSignedBasisBlade(this, IntegerSign.Zero);
    }

    
    public XGaSignedBasisBlade ToPositiveBasisBlade()
    {
        return new XGaSignedBasisBlade(this, IntegerSign.Positive);
    }

    
    public XGaSignedBasisBlade ToNegativeBasisBlade()
    {
        return new XGaSignedBasisBlade(this, IntegerSign.Negative);
    }

    
    public XGaSignedBasisBlade ToSignedBasisBlade(IntegerSign sign)
    {
        return new XGaSignedBasisBlade(this, sign);
    }


    
    public IntegerSign Signature()
    {
        return Metric.Signature(Id);
    }

    
    public IntegerSign NegativeSign()
    {
        return IntegerSign.Negative;
    }

    public IXGaSignedBasisBlade ShiftIndices(int offset)
    {
        if (IsScalar || offset == 0) return this;

        return new XGaBasisBlade(
            Metric, 
            Id.ShiftIndices(offset)
        );
    }

    
    public IXGaSignedBasisBlade Negative()
    {
        return new XGaSignedBasisBlade(this, IntegerSign.Negative);
    }

    
    public IXGaSignedBasisBlade Times(IntegerSign sign)
    {
        return sign.Value switch
        {
            > 0 => this,
            < 0 => new XGaSignedBasisBlade(this, IntegerSign.Negative),
            _ => new XGaSignedBasisBlade(this, IntegerSign.Zero)
        };
    }

    
    public IXGaSignedBasisBlade Reverse()
    {
        return Grade.ReverseIsNegativeOfGrade() 
            ? Negative()
            : this;
    }

    
    public IntegerSign ReverseSign()
    {
        return Grade.ReverseIsNegativeOfGrade()
            ? IntegerSign.Negative
            : IntegerSign.Positive;
    }

    
    public IXGaSignedBasisBlade GradeInvolution()
    {
        return Grade.GradeInvolutionIsNegativeOfGrade()
            ? Negative()
            : this;
    }

    
    public IntegerSign GradeInvolutionSign()
    {
        return Grade.GradeInvolutionIsNegativeOfGrade()
            ? IntegerSign.Negative
            : IntegerSign.Positive;
    }

    
    public IXGaSignedBasisBlade CliffordConjugate()
    {
        return Grade.CliffordConjugateIsNegativeOfGrade()
            ? Negative()
            : this;
    }

    
    public IntegerSign CliffordConjugateSign()
    {
        return Grade.CliffordConjugateIsNegativeOfGrade()
            ? IntegerSign.Negative
            : IntegerSign.Positive;
    }
    
    
    public IXGaSignedBasisBlade EConjugate()
    {
        return Reverse();
    }

    
    public IntegerSign EConjugateSign()
    {
        return ReverseSign();
    }

    
    public IXGaSignedBasisBlade Conjugate()
    {
        return Metric.IsEuclidean 
            ? Reverse() 
            : Reverse().Times(Signature());
    }
    
    
    public IntegerSign ConjugateSign()
    {
        return Metric.IsEuclidean 
            ? ReverseSign() 
            : ReverseSign() * Signature();
    }

    
    public IntegerSign TimesSign(IntegerSign sign)
    {
        return sign;
    }


    
    public IXGaSignedBasisBlade EGp(IndexSet id2)
    {
        var (isNegative, id) = 
            Id.EGpIsNegativeId(id2);

        var basisBlade = new XGaBasisBlade(Metric, id);

        return isNegative
            ? basisBlade.Negative()
            : basisBlade;
    }
    
    
    public bool EGpIsNegative(IndexSet id2)
    {
        return Id.EGpIsNegative(id2);
    }
    
    
    public IntegerSign EGpSign(IndexSet id2)
    {
        return Id.EGpSign(id2);
    }

    
    public IntegerSign EGpSign(IXGaSignedBasisBlade basisBlade)
    {
        return basisBlade.IsZero
            ? IntegerSign.Zero 
            : Id.EGpSign(basisBlade.Id, basisBlade.IsNegative);
    }


    
    public IXGaSignedBasisBlade Op(IndexSet id2)
    {
        return Id.OpIsNonZero(id2)
            ? EGp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    
    public IXGaSignedBasisBlade ESp(IndexSet id2)
    {
        return Id.ESpIsNonZero(id2)
            ? EGp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    
    public IXGaSignedBasisBlade ELcp(IndexSet id2)
    {
        return Id.ELcpIsNonZero(id2)
            ? EGp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    
    public IXGaSignedBasisBlade ERcp(IndexSet id2)
    {
        return Id.ERcpIsNonZero(id2)
            ? EGp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    
    public IXGaSignedBasisBlade EFdp(IndexSet id2)
    {
        return Id.EFdpIsNonZero(id2)
            ? EGp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    
    public IXGaSignedBasisBlade EHip(IndexSet id2)
    {
        return Id.EHipIsNonZero(id2)
            ? EGp(id2)
            : Metric.ZeroBasisScalar;
    }

    
    public IXGaSignedBasisBlade EAcp(IndexSet id2)
    {
        return Id.EAcpIsNonZero(id2)
            ? EGp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    
    public IXGaSignedBasisBlade ECp(IndexSet id2)
    {
        return Id.ECpIsNonZero(id2)
            ? EGp(id2)
            : Metric.ZeroBasisScalar;
    }


    public IXGaSignedBasisBlade Gp(IndexSet id2)
    {
        if (Metric.IsEuclidean) 
            return EGp(id2);
        
        var meetBasisBladeSignature = 
            Metric.Signature(
                Id.SetIntersect(id2)
            );

        if (meetBasisBladeSignature.IsZero)
            return Metric.ZeroBasisScalar;

        var egpBasisBlade = EGp(id2);

        return meetBasisBladeSignature.IsPositive
            ? egpBasisBlade
            : egpBasisBlade.Negative();
    }
    
    
    public IXGaSignedBasisBlade Sp(IndexSet id2)
    {
        return Id.ESpIsNonZero(id2)
            ? Gp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    
    public IXGaSignedBasisBlade Lcp(IndexSet id2)
    {
        return Id.ELcpIsNonZero(id2)
            ? Gp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    
    public IXGaSignedBasisBlade Rcp(IndexSet id2)
    {
        return Id.ERcpIsNonZero(id2)
            ? Gp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    
    public IXGaSignedBasisBlade Fdp(IndexSet id2)
    {
        return Id.EFdpIsNonZero(id2)
            ? Gp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    
    public IXGaSignedBasisBlade Hip(IndexSet id2)
    {
        return Id.EHipIsNonZero(id2)
            ? Gp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    
    public IXGaSignedBasisBlade Acp(IndexSet id2)
    {
        return Id.EAcpIsNonZero(id2)
            ? Gp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    
    public IXGaSignedBasisBlade Cp(IndexSet id2)
    {
        return Id.ECpIsNonZero(id2)
            ? Gp(id2)
            : Metric.ZeroBasisScalar;
    }
    

    
    public IXGaSignedBasisBlade EGp(IXGaSignedBasisBlade basisBlade)
    {
        return EGp(basisBlade.Id).Times(basisBlade.Sign);
    }
    
    
    public IXGaSignedBasisBlade ESp(IXGaSignedBasisBlade basisBlade)
    {
        return ESp(basisBlade.Id).Times(basisBlade.Sign);
    }

    
    public IXGaSignedBasisBlade Op(IXGaSignedBasisBlade basisBlade)
    {
        return Op(basisBlade.Id).Times(basisBlade.Sign);
    }

    
    public IXGaSignedBasisBlade ELcp(IXGaSignedBasisBlade basisBlade)
    {
        return ELcp(basisBlade.Id).Times(basisBlade.Sign);
    }

    
    public IXGaSignedBasisBlade ERcp(IXGaSignedBasisBlade basisBlade)
    {
        return ERcp(basisBlade.Id).Times(basisBlade.Sign);
    }

    
    public IXGaSignedBasisBlade EFdp(IXGaSignedBasisBlade basisBlade)
    {
        return EFdp(basisBlade.Id).Times(basisBlade.Sign);
    }

    
    public IXGaSignedBasisBlade EHip(IXGaSignedBasisBlade basisBlade)
    {
        return EHip(basisBlade.Id).Times(basisBlade.Sign);
    }

    
    public IXGaSignedBasisBlade EAcp(IXGaSignedBasisBlade basisBlade)
    {
        return EAcp(basisBlade.Id).Times(basisBlade.Sign);
    }

    
    public IXGaSignedBasisBlade ECp(IXGaSignedBasisBlade basisBlade)
    {
        return ECp(basisBlade.Id).Times(basisBlade.Sign);
    }


    
    public IXGaSignedBasisBlade Gp(IXGaSignedBasisBlade basisBlade)
    {
        return Gp(basisBlade.Id).Times(basisBlade.Sign);
    }

    
    public IXGaSignedBasisBlade Sp(IXGaSignedBasisBlade basisBlade)
    {
        return Sp(basisBlade.Id).Times(basisBlade.Sign);
    }

    
    public IXGaSignedBasisBlade Lcp(IXGaSignedBasisBlade basisBlade)
    {
        return Lcp(basisBlade.Id).Times(basisBlade.Sign);
    }

    
    public IXGaSignedBasisBlade Rcp(IXGaSignedBasisBlade basisBlade)
    {
        return Rcp(basisBlade.Id).Times(basisBlade.Sign);
    }

    
    public IXGaSignedBasisBlade Fdp(IXGaSignedBasisBlade basisBlade)
    {
        return Fdp(basisBlade.Id).Times(basisBlade.Sign);
    }

    
    public IXGaSignedBasisBlade Hip(IXGaSignedBasisBlade basisBlade)
    {
        return Hip(basisBlade.Id).Times(basisBlade.Sign);
    }

    
    public IXGaSignedBasisBlade Acp(IXGaSignedBasisBlade basisBlade)
    {
        return Acp(basisBlade.Id).Times(basisBlade.Sign);
    }

    
    public IXGaSignedBasisBlade Cp(IXGaSignedBasisBlade basisBlade)
    {
        return Cp(basisBlade.Id).Times(basisBlade.Sign);
    }

    
    
    public IntegerSign EGpReverseSign(IndexSet id2)
    {
        var reverseSign = id2.ReverseSignOfBasisBladeId();

        return Id.EGpIsNegative(id2) 
            ? -reverseSign 
            : reverseSign;
    }

    public IntegerSign GpSign(IXGaSignedBasisBlade basisBlade)
    {
        if (Metric.IsEuclidean) 
            return EGpSign(basisBlade);

        if (basisBlade.IsZero)
            return IntegerSign.Zero;

        var id2 = basisBlade.Id;

        var meetSignature = 
            Metric.Signature(
                Id.SetIntersect(id2)
            );

        return meetSignature.IsZero
            ? IntegerSign.Zero
            : Id.EGpSign(
                id2,
                (basisBlade.Sign * meetSignature).IsNegative
            );
    }

    
    public IntegerSign OpSign(IndexSet id2)
    {
        return Id.OpIsNonZero(id2) 
            ? EGpSign(id2) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign OpSign(IXGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.OpIsNonZero(basisBlade.Id) 
            ? EGpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign ESpSign(IndexSet id2)
    {
        return Id.ESpIsNonZero(id2) 
            ? EGpSign(id2) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign ESpSign(IXGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.ESpIsNonZero(basisBlade.Id) 
            ? EGpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign SpSign(IXGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.ESpIsNonZero(basisBlade.Id) 
            ? GpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign ELcpSign(IndexSet id2)
    {
        return Id.ELcpIsNonZero(id2) 
            ? EGpSign(id2) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign ELcpSign(IXGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.ELcpIsNonZero(basisBlade.Id) 
            ? EGpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign LcpSign(IXGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.ELcpIsNonZero(basisBlade.Id) 
            ? GpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign ERcpSign(IndexSet id2)
    {
        return Id.ERcpIsNonZero(id2) 
            ? EGpSign(id2) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign ERcpSign(IXGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.ERcpIsNonZero(basisBlade.Id) 
            ? EGpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign RcpSign(IXGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.ERcpIsNonZero(basisBlade.Id) 
            ? GpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign EFdpSign(IndexSet id2)
    {
        return Id.EFdpIsNonZero(id2) 
            ? EGpSign(id2) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign EFdpSign(IXGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.EFdpIsNonZero(basisBlade.Id) 
            ? EGpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign FdpSign(IXGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.EFdpIsNonZero(basisBlade.Id) 
            ? GpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign EHipSign(IndexSet id2)
    {
        return Id.EHipIsNonZero(id2) 
            ? EGpSign(id2) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign EHipSign(IXGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.EHipIsNonZero(basisBlade.Id) 
            ? EGpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign HipSign(IXGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.EHipIsNonZero(basisBlade.Id) 
            ? GpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign EAcpSign(IndexSet id2)
    {
        return Id.EAcpIsNonZero(id2) 
            ? EGpSign(id2) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign EAcpSign(IXGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.EAcpIsNonZero(basisBlade.Id) 
            ? EGpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign AcpSign(IXGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.EAcpIsNonZero(basisBlade.Id) 
            ? GpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign ECpSign(IndexSet id2)
    {
        return Id.ECpIsNonZero(id2) 
            ? EGpSign(id2) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign ECpSign(IXGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.ECpIsNonZero(basisBlade.Id) 
            ? EGpSign(basisBlade) 
            : IntegerSign.Zero;
    }
    
    
    public IntegerSign CpSign(IXGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.ECpIsNonZero(basisBlade.Id) 
            ? GpSign(basisBlade) 
            : IntegerSign.Zero;
    }
    

    
    public IntegerSign GpSign(IndexSet id2)
    {
        if (Metric.IsEuclidean) return EGpSign(id2);
        
        var meetBladeSignature = 
            GetMeetBladeSignature(id2);

        return EGpIsNegative(id2) 
            ? -meetBladeSignature 
            : meetBladeSignature;
    }
    
    
    public IntegerSign SpSign(IndexSet id2)
    {
        return Id.ESpIsNonZero(id2) 
            ? GpSign(id2) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign LcpSign(IndexSet id2)
    {
        return Id.ELcpIsNonZero(id2) 
            ? GpSign(id2) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign RcpSign(IndexSet id2)
    {
        return Id.ERcpIsNonZero(id2) 
            ? GpSign(id2) 
            : IntegerSign.Zero;
    }
    
    
    public IntegerSign FdpSign(IndexSet id2)
    {
        return Id.EFdpIsNonZero(id2) 
            ? GpSign(id2) 
            : IntegerSign.Zero;
    }
    
    
    public IntegerSign HipSign(IndexSet id2)
    {
        return Id.EHipIsNonZero(id2) 
            ? GpSign(id2) 
            : IntegerSign.Zero;
    }

    
    public IntegerSign AcpSign(IndexSet id2)
    {
        return Id.EAcpIsNonZero(id2) 
            ? GpSign(id2) 
            : IntegerSign.Zero;
    }
    
    
    public IntegerSign CpSign(IndexSet id2)
    {
        return Id.ECpIsNonZero(id2) 
            ? GpSign(id2) 
            : IntegerSign.Zero;
    }

    
    
    public IntegerSign GetMeetBladeSignature(IndexSet id2)
    {
        return Metric.Signature(
            Id.SetIntersect(id2)
        );
    }
    
    
    public XGaBasisBlade GetMeetBlade(IndexSet id2)
    {
        return new XGaBasisBlade(
            Metric,
            Id.SetIntersect(id2)
        );
    }

    
    public XGaBasisBlade GetJoinBlade(IndexSet id2)
    {
        return new XGaBasisBlade(
            Metric,
            Id.SetUnion(id2)
        );
    }
    
    
    public XGaBasisBlade GetEGpBlade(IndexSet id2)
    {
        return new XGaBasisBlade(
            Metric,
            Id.SetMerge(id2)
        );
    }


    
    public XGaFloat64KVector ToKVector()
    {
        var processor = (XGaFloat64Processor)Metric;

        return processor.KVectorTerm(
            Id,
            1d
        );
    }

    
    
    public string GetBasisBladeText()
    {
        return Id.GetBasisBladeText();
    }

    
    public bool Equals(XGaBasisBlade other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return Equals(Id, other.Id);
    }
    
    
    public bool Equals(IXGaSignedBasisBlade other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return other.Sign.IsPositive && Equals(Id, other.Id);
    }

    
    public override int GetHashCode()
    {
        return HashCode.Combine(Sign, Id);
    }
    
    
    public override string ToString()
    {
        return Id
            .Select(i => i.ToString())
            .ConcatenateText(", ", "<", ">");
    }
}