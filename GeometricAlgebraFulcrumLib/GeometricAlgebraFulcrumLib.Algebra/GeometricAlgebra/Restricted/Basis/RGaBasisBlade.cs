using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Basis;

/// <summary>
/// This represents a basis blade of arbitrary grade for an extended GA
/// </summary>
public sealed record RGaBasisBlade : 
    IRGaSignedBasisBlade
{
    public RGaMetric Metric { get; }

    public ulong Id { get; }

    public IntegerSign Sign 
        => IntegerSign.Positive;

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
        => Id.IsBasisScalar();

    public bool IsVector 
        => Id.IsBasisVector();

    public bool IsBivector 
        => Id.IsBasisBivector();

    public int VSpaceDimensions 
        => Id.VSpaceDimensions();

    public int Grade 
        => Id.Grade();

    public ulong KvSpaceDimensions 
        => Id.KvSpaceDimensions();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBasisBlade(RGaMetric metric, ulong basisBladeId)
    {
        Metric = metric;
        Id = basisBladeId;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBasisBlade GetBasisBlade()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsKVector(int grade)
    {
        return BitOperations.PopCount(Id) == grade;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign Signature()
    {
        return Metric.Signature(Id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign NegativeSign()
    {
        return IntegerSign.Negative;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ShiftIndices(int offset)
    {
        if (IsScalar || offset == 0) return this;

        return new RGaBasisBlade(
            Metric,
            Id.ShiftOnes(offset)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Negative()
    {
        return new RGaSignedBasisBlade(this, IntegerSign.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Times(IntegerSign sign)
    {
        return sign.Value switch
        {
            > 0 => this,
            < 0 => new RGaSignedBasisBlade(this, IntegerSign.Negative),
            _ => new RGaSignedBasisBlade(this, IntegerSign.Zero)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Reverse()
    {
        return Grade.ReverseIsNegativeOfGrade() 
            ? Negative()
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ReverseSign()
    {
        return Grade.ReverseIsNegativeOfGrade()
            ? IntegerSign.Negative
            : IntegerSign.Positive;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade GradeInvolution()
    {
        return Grade.GradeInvolutionIsNegativeOfGrade()
            ? Negative()
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GradeInvolutionSign()
    {
        return Grade.GradeInvolutionIsNegativeOfGrade()
            ? IntegerSign.Negative
            : IntegerSign.Positive;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade CliffordConjugate()
    {
        return Grade.CliffordConjugateIsNegativeOfGrade()
            ? Negative()
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign CliffordConjugateSign()
    {
        return Grade.CliffordConjugateIsNegativeOfGrade()
            ? IntegerSign.Negative
            : IntegerSign.Positive;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EConjugate()
    {
        return Reverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EConjugateSign()
    {
        return ReverseSign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Conjugate()
    {
        return Metric.IsEuclidean 
            ? Reverse() 
            : Reverse().Times(Signature());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ConjugateSign()
    {
        return Metric.IsEuclidean 
            ? ReverseSign() 
            : ReverseSign() * Signature();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign TimesSign(IntegerSign sign)
    {
        return sign;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EGp(ulong id2)
    {
        var (isNegative, id) = 
            Id.EGpIsNegativeId(id2);

        var basisBlade = new RGaBasisBlade(Metric, id);

        return isNegative
            ? basisBlade.Negative()
            : basisBlade;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool EGpIsNegative(ulong id2)
    {
        return Id.EGpIsNegative(id2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSign(ulong id2)
    {
        return Id.EGpSign(id2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSign(IRGaSignedBasisBlade basisBlade)
    {
        return basisBlade.IsZero
            ? IntegerSign.Zero 
            : Id.EGpSign(basisBlade.Id, basisBlade.IsNegative);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Op(ulong id2)
    {
        return Id.OpIsNonZero(id2)
            ? EGp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ESp(ulong id2)
    {
        return Id.ESpIsNonZero(id2)
            ? EGp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ELcp(ulong id2)
    {
        return Id.ELcpIsNonZero(id2)
            ? EGp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ERcp(ulong id2)
    {
        return Id.ERcpIsNonZero(id2)
            ? EGp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EFdp(ulong id2)
    {
        return Id.EFdpIsNonZero(id2)
            ? EGp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EHip(ulong id2)
    {
        return Id.EHipIsNonZero(id2)
            ? EGp(id2)
            : Metric.ZeroBasisScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EAcp(ulong id2)
    {
        return Id.EAcpIsNonZero(id2)
            ? EGp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ECp(ulong id2)
    {
        return Id.ECpIsNonZero(id2)
            ? EGp(id2)
            : Metric.ZeroBasisScalar;
    }


    public IRGaSignedBasisBlade Gp(ulong id2)
    {
        if (Metric.IsEuclidean) 
            return EGp(id2);
        
        var meetBasisBladeSignature = 
            Metric.Signature(
                Id & id2
            );

        if (meetBasisBladeSignature.IsZero)
            return Metric.ZeroBasisScalar;

        var egpBasisBlade = EGp(id2);

        return meetBasisBladeSignature.IsPositive
            ? egpBasisBlade
            : egpBasisBlade.Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Sp(ulong id2)
    {
        return Id.ESpIsNonZero(id2)
            ? Gp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Lcp(ulong id2)
    {
        return Id.ELcpIsNonZero(id2)
            ? Gp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Rcp(ulong id2)
    {
        return Id.ERcpIsNonZero(id2)
            ? Gp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Fdp(ulong id2)
    {
        return Id.EFdpIsNonZero(id2)
            ? Gp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Hip(ulong id2)
    {
        return Id.EHipIsNonZero(id2)
            ? Gp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Acp(ulong id2)
    {
        return Id.EAcpIsNonZero(id2)
            ? Gp(id2)
            : Metric.ZeroBasisScalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Cp(ulong id2)
    {
        return Id.ECpIsNonZero(id2)
            ? Gp(id2)
            : Metric.ZeroBasisScalar;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EGp(IRGaSignedBasisBlade basisBlade)
    {
        return EGp(basisBlade.Id).Times(basisBlade.Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ESp(IRGaSignedBasisBlade basisBlade)
    {
        return ESp(basisBlade.Id).Times(basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Op(IRGaSignedBasisBlade basisBlade)
    {
        return Op(basisBlade.Id).Times(basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ELcp(IRGaSignedBasisBlade basisBlade)
    {
        return ELcp(basisBlade.Id).Times(basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ERcp(IRGaSignedBasisBlade basisBlade)
    {
        return ERcp(basisBlade.Id).Times(basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EFdp(IRGaSignedBasisBlade basisBlade)
    {
        return EFdp(basisBlade.Id).Times(basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EHip(IRGaSignedBasisBlade basisBlade)
    {
        return EHip(basisBlade.Id).Times(basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EAcp(IRGaSignedBasisBlade basisBlade)
    {
        return EAcp(basisBlade.Id).Times(basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ECp(IRGaSignedBasisBlade basisBlade)
    {
        return ECp(basisBlade.Id).Times(basisBlade.Sign);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Gp(IRGaSignedBasisBlade basisBlade)
    {
        return Gp(basisBlade.Id).Times(basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Sp(IRGaSignedBasisBlade basisBlade)
    {
        return Sp(basisBlade.Id).Times(basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Lcp(IRGaSignedBasisBlade basisBlade)
    {
        return Lcp(basisBlade.Id).Times(basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Rcp(IRGaSignedBasisBlade basisBlade)
    {
        return Rcp(basisBlade.Id).Times(basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Fdp(IRGaSignedBasisBlade basisBlade)
    {
        return Fdp(basisBlade.Id).Times(basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Hip(IRGaSignedBasisBlade basisBlade)
    {
        return Hip(basisBlade.Id).Times(basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Acp(IRGaSignedBasisBlade basisBlade)
    {
        return Acp(basisBlade.Id).Times(basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Cp(IRGaSignedBasisBlade basisBlade)
    {
        return Cp(basisBlade.Id).Times(basisBlade.Sign);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpReverseSign(ulong id2)
    {
        var reverseSign = id2.ReverseSignOfBasisBladeId();

        return Id.EGpIsNegative(id2) 
            ? -reverseSign 
            : reverseSign;
    }

    public IntegerSign GpSign(IRGaSignedBasisBlade basisBlade)
    {
        if (Metric.IsEuclidean) 
            return EGpSign(basisBlade);

        if (basisBlade.IsZero)
            return IntegerSign.Zero;

        var id2 = basisBlade.Id;

        var meetSignature = 
            Metric.Signature(
                Id & id2
            );

        return meetSignature.IsZero
            ? IntegerSign.Zero
            : Id.EGpSign(
                id2,
                (basisBlade.Sign * meetSignature).IsNegative
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign OpSign(ulong id2)
    {
        return Id.OpIsNonZero(id2) 
            ? EGpSign(id2) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign OpSign(IRGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.OpIsNonZero(basisBlade.Id) 
            ? EGpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ESpSign(ulong id2)
    {
        return Id.ESpIsNonZero(id2) 
            ? EGpSign(id2) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ESpSign(IRGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.ESpIsNonZero(basisBlade.Id) 
            ? EGpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign SpSign(IRGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.ESpIsNonZero(basisBlade.Id) 
            ? GpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ELcpSign(ulong id2)
    {
        return Id.ELcpIsNonZero(id2) 
            ? EGpSign(id2) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ELcpSign(IRGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.ELcpIsNonZero(basisBlade.Id) 
            ? EGpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign LcpSign(IRGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.ELcpIsNonZero(basisBlade.Id) 
            ? GpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ERcpSign(ulong id2)
    {
        return Id.ERcpIsNonZero(id2) 
            ? EGpSign(id2) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ERcpSign(IRGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.ERcpIsNonZero(basisBlade.Id) 
            ? EGpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign RcpSign(IRGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.ERcpIsNonZero(basisBlade.Id) 
            ? GpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EFdpSign(ulong id2)
    {
        return Id.EFdpIsNonZero(id2) 
            ? EGpSign(id2) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EFdpSign(IRGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.EFdpIsNonZero(basisBlade.Id) 
            ? EGpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign FdpSign(IRGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.EFdpIsNonZero(basisBlade.Id) 
            ? GpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EHipSign(ulong id2)
    {
        return Id.EHipIsNonZero(id2) 
            ? EGpSign(id2) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EHipSign(IRGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.EHipIsNonZero(basisBlade.Id) 
            ? EGpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign HipSign(IRGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.EHipIsNonZero(basisBlade.Id) 
            ? GpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EAcpSign(ulong id2)
    {
        return Id.EAcpIsNonZero(id2) 
            ? EGpSign(id2) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EAcpSign(IRGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.EAcpIsNonZero(basisBlade.Id) 
            ? EGpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign AcpSign(IRGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.EAcpIsNonZero(basisBlade.Id) 
            ? GpSign(basisBlade) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ECpSign(ulong id2)
    {
        return Id.ECpIsNonZero(id2) 
            ? EGpSign(id2) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ECpSign(IRGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.ECpIsNonZero(basisBlade.Id) 
            ? EGpSign(basisBlade) 
            : IntegerSign.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign CpSign(IRGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero) 
            return IntegerSign.Zero;

        return Id.ECpIsNonZero(basisBlade.Id) 
            ? GpSign(basisBlade) 
            : IntegerSign.Zero;
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GpSign(ulong id2)
    {
        if (Metric.IsEuclidean) return EGpSign(id2);
        
        var meetBladeSignature = 
            GetMeetBladeSignature(id2);

        return EGpIsNegative(id2) 
            ? -meetBladeSignature 
            : meetBladeSignature;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign SpSign(ulong id2)
    {
        return Id.ESpIsNonZero(id2) 
            ? GpSign(id2) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign LcpSign(ulong id2)
    {
        return Id.ELcpIsNonZero(id2) 
            ? GpSign(id2) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign RcpSign(ulong id2)
    {
        return Id.ERcpIsNonZero(id2) 
            ? GpSign(id2) 
            : IntegerSign.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign FdpSign(ulong id2)
    {
        return Id.EFdpIsNonZero(id2) 
            ? GpSign(id2) 
            : IntegerSign.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign HipSign(ulong id2)
    {
        return Id.EHipIsNonZero(id2) 
            ? GpSign(id2) 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign AcpSign(ulong id2)
    {
        return Id.EAcpIsNonZero(id2) 
            ? GpSign(id2) 
            : IntegerSign.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign CpSign(ulong id2)
    {
        return Id.ECpIsNonZero(id2) 
            ? GpSign(id2) 
            : IntegerSign.Zero;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GetMeetBladeSignature(ulong id2)
    {
        return Metric.Signature(
            Id & id2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBasisBlade GetMeetBlade(ulong id2)
    {
        return new RGaBasisBlade(
            Metric,
            Id & id2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBasisBlade GetJoinBlade(ulong id2)
    {
        return new RGaBasisBlade(
            Metric,
            Id | id2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBasisBlade GetEGpBlade(ulong id2)
    {
        return new RGaBasisBlade(
            Metric,
            Id ^ id2
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return Id
            .PatternToPositions()
            .Select(i => (i + 1).ToString())
            .ConcatenateText(", ", "<", ">");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(RGaBasisBlade? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        
        return Id == other.Id;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(IRGaSignedBasisBlade? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return other.Sign.IsPositive && Id == other.Id;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return HashCode.Combine(Sign, Id);
    }
}