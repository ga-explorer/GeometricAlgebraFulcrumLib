using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Basis;

public sealed record RGaFloat64ScaledBasisBlade
    : IRGaFloat64Element
{
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledBasisBlade operator +(RGaFloat64ScaledBasisBlade b1)
    {
        return b1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledBasisBlade operator -(RGaFloat64ScaledBasisBlade b1)
    {
        return new RGaFloat64ScaledBasisBlade(b1.Processor, b1.Id, -b1.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledBasisBlade operator *(RGaFloat64ScaledBasisBlade b1, IntegerSign s2)
    {
        return new RGaFloat64ScaledBasisBlade(b1.Processor, b1.Id, b1.Scalar * s2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64ScaledBasisBlade operator *(IntegerSign s1, RGaFloat64ScaledBasisBlade b2)
    {
        return new RGaFloat64ScaledBasisBlade(b2.Processor, b2.Id, s1 * b2.Scalar);
    }



    public RGaFloat64Processor Processor { get; }

    public RGaMetric Metric 
        => Processor;

    public ulong Id { get; }

    public Float64Scalar Scalar { get; }
        
    public RGaBasisBlade BasisBlade 
        => new RGaBasisBlade(Metric, Id);

    public bool IsNegative
        => Scalar.IsNegative();

    public bool IsZero
        => Scalar.IsZero();

    public bool IsPositive
        => Scalar.IsPositive();

    public bool IsNonNegative 
        => !Scalar.IsNegative();

    public bool IsNonZero
        => !Scalar.IsZero();

    public bool IsNonPositive 
        => !Scalar.IsPositive();
        
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
    public RGaFloat64ScaledBasisBlade(RGaFloat64Processor processor, ulong id, Float64Scalar scalar)
    {
        Processor = processor;
        Id = id;
        Scalar = scalar;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade(RGaFloat64Processor processor, RGaBasisBlade basisBlade, Float64Scalar scalar)
    {
        Processor = processor;
        Id = basisBlade.Id;
        Scalar = scalar;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade(RGaFloat64Processor processor, IRGaSignedBasisBlade basisBlade, Float64Scalar scalar)
    {
        Processor = processor;
        Id = basisBlade.Id;
        Scalar = scalar.ScalarValue * basisBlade.Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out ulong id, out double scalar)
    {
        id = Id;
        scalar = Scalar.ScalarValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade ShiftIndices(int offset)
    {
        if (IsScalar || offset == 0) return this;

        return new RGaFloat64ScaledBasisBlade(
            Processor,
            Id.ShiftOnes(offset),
            Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Negative()
    {
        return IsZero
            ? this
            : new RGaFloat64ScaledBasisBlade(Processor, Id, -Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Reverse()
    {
        return Grade.ReverseIsNegativeOfGrade()
            ? Negative()
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade GradeInvolution()
    {
        return Grade.GradeInvolutionIsNegativeOfGrade()
            ? Negative()
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade CliffordConjugate()
    {
        return Grade.CliffordConjugateIsNegativeOfGrade()
            ? Negative()
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade EConjugate()
    {
        return Reverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Conjugate()
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Conjugate(),
            Scalar
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Times(double scalar)
    {
        return new RGaFloat64ScaledBasisBlade(Processor, Id, scalar);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade EGp(ulong basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.EGp(basisBlade), 
            Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Op(ulong basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Op(basisBlade), 
            Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade ESp(ulong basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.ESp(basisBlade), 
            Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade ELcp(ulong basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.ELcp(basisBlade), 
            Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade ERcp(ulong basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.ERcp(basisBlade), 
            Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade EFdp(ulong basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.EFdp(basisBlade), 
            Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade EHip(ulong basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.EHip(basisBlade), 
            Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade EAcp(ulong basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.EAcp(basisBlade), 
            Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade ECp(ulong basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.ECp(basisBlade), 
            Scalar
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade EGp(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.EGp(basisBlade.BasisBlade), 
            Scalar * basisBlade.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Op(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Op(basisBlade.BasisBlade), 
            Scalar * basisBlade.Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade ESp(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.ESp(basisBlade.BasisBlade), 
            Scalar * basisBlade.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade ELcp(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.ELcp(basisBlade.BasisBlade), 
            Scalar * basisBlade.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade ERcp(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.ERcp(basisBlade.BasisBlade), 
            Scalar * basisBlade.Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade EFdp(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.EFdp(basisBlade.BasisBlade), 
            Scalar * basisBlade.Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade EHip(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.EHip(basisBlade.BasisBlade), 
            Scalar * basisBlade.Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade EAcp(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.EAcp(basisBlade.BasisBlade), 
            Scalar * basisBlade.Scalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade ECp(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.ECp(basisBlade.BasisBlade), 
            Scalar * basisBlade.Scalar
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Gp(ulong basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Gp(basisBlade), 
            Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Sp(ulong basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Sp(basisBlade), 
            Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Lcp(ulong basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Lcp(basisBlade), 
            Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Rcp(ulong basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Rcp(basisBlade), 
            Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Fdp(ulong basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Fdp(basisBlade), 
            Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Hip(ulong basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Hip(basisBlade),
            Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Acp(ulong basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Acp(basisBlade), 
            Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Cp(ulong basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Cp(basisBlade), 
            Scalar
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Gp(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Gp(basisBlade.BasisBlade), 
            Scalar * basisBlade.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Sp(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Sp(basisBlade.BasisBlade), 
            Scalar * basisBlade.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Lcp(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Lcp(basisBlade.BasisBlade), 
            Scalar * basisBlade.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Rcp(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Rcp(basisBlade.BasisBlade), 
            Scalar * basisBlade.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Fdp(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Fdp(basisBlade.BasisBlade), 
            Scalar * basisBlade.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Hip(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Hip(basisBlade.BasisBlade), 
            Scalar * basisBlade.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Acp(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Acp(basisBlade.BasisBlade),
            Scalar * basisBlade.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64ScaledBasisBlade Cp(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return new RGaFloat64ScaledBasisBlade(
            Processor,
            BasisBlade.Cp(basisBlade.BasisBlade), 
            Scalar * basisBlade.Scalar
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Signature()
    {
        return IsZero 
            ? 0d 
            : Metric.Signature(Id) * Scalar.ScalarValue.Square();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar NegativeScalar()
    {
        return -Scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ReverseScalar()
    {
        return BasisBlade.ReverseSign() * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GradeInvolutionScalar()
    {
        return BasisBlade.GradeInvolutionSign() * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar CliffordConjugateScalar()
    {
        return BasisBlade.CliffordConjugateSign() * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar EConjugateScalar()
    {
        return BasisBlade.EConjugateSign() * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ConjugateScalar()
    {
        return BasisBlade.ConjugateSign() * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar TimesScalar(IntegerSign sign)
    {
        return Scalar.ScalarValue * sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar EGpScalar(ulong basisBlade)
    {
        return BasisBlade.EGpSign(basisBlade) * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar EGpScalar(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return BasisBlade.EGpSign(basisBlade.BasisBlade) * Scalar.ScalarValue * basisBlade.Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GpScalar(ulong basisBlade)
    {
        return BasisBlade.GpSign(basisBlade) * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GpScalar(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return BasisBlade.GpSign(basisBlade.BasisBlade) * Scalar.ScalarValue * basisBlade.Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar OpScalar(ulong basisBlade)
    {
        return BasisBlade.OpSign(basisBlade) * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar OpScalar(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return BasisBlade.OpSign(basisBlade.BasisBlade) * Scalar.ScalarValue * basisBlade.Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ESpScalar(ulong basisBlade)
    {
        return BasisBlade.ESpSign(basisBlade) * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ESpScalar(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return BasisBlade.ESpSign(basisBlade.BasisBlade) * Scalar.ScalarValue * basisBlade.Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar SpScalar(ulong basisBlade)
    {
        return BasisBlade.SpSign(basisBlade) * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar SpScalar(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return BasisBlade.SpSign(basisBlade.BasisBlade) * Scalar.ScalarValue * basisBlade.Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ELcpScalar(ulong basisBlade)
    {
        return BasisBlade.ELcpSign(basisBlade) * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ELcpScalar(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return BasisBlade.ELcpSign(basisBlade.BasisBlade) * Scalar.ScalarValue * basisBlade.Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar LcpScalar(ulong basisBlade)
    {
        return BasisBlade.LcpSign(basisBlade) * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar LcpScalar(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return BasisBlade.LcpSign(basisBlade.BasisBlade) * Scalar.ScalarValue * basisBlade.Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ERcpScalar(ulong basisBlade)
    {
        return BasisBlade.ERcpSign(basisBlade) * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ERcpScalar(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return BasisBlade.ERcpSign(basisBlade.BasisBlade) * Scalar.ScalarValue * basisBlade.Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar RcpScalar(ulong basisBlade)
    {
        return BasisBlade.RcpSign(basisBlade) * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar RcpScalar(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return BasisBlade.RcpSign(basisBlade.BasisBlade) * Scalar.ScalarValue * basisBlade.Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar EFdpScalar(ulong basisBlade)
    {
        return BasisBlade.EFdpSign(basisBlade) * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar EFdpScalar(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return BasisBlade.EFdpSign(basisBlade.BasisBlade) * Scalar.ScalarValue * basisBlade.Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar FdpScalar(ulong basisBlade)
    {
        return BasisBlade.FdpSign(basisBlade) * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar FdpScalar(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return BasisBlade.FdpSign(basisBlade.BasisBlade) * Scalar.ScalarValue * basisBlade.Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar EHipScalar(ulong basisBlade)
    {
        return BasisBlade.EHipSign(basisBlade) * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar EHipScalar(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return BasisBlade.EHipSign(basisBlade.BasisBlade) * Scalar.ScalarValue * basisBlade.Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar HipScalar(ulong basisBlade)
    {
        return BasisBlade.HipSign(basisBlade) * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar HipScalar(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return BasisBlade.HipSign(basisBlade.BasisBlade) * Scalar.ScalarValue * basisBlade.Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar EAcpScalar(ulong basisBlade)
    {
        return BasisBlade.EAcpSign(basisBlade) * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar EAcpScalar(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return BasisBlade.EAcpSign(basisBlade.BasisBlade) * Scalar.ScalarValue * basisBlade.Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar AcpScalar(ulong basisBlade)
    {
        return BasisBlade.AcpSign(basisBlade) * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar AcpScalar(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return BasisBlade.AcpSign(basisBlade.BasisBlade) * Scalar.ScalarValue * basisBlade.Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ECpScalar(ulong basisBlade)
    {
        return BasisBlade.ECpSign(basisBlade) * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ECpScalar(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return BasisBlade.ECpSign(basisBlade.BasisBlade) * Scalar.ScalarValue * basisBlade.Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar CpScalar(ulong basisBlade)
    {
        return BasisBlade.CpSign(basisBlade) * Scalar.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar CpScalar(RGaFloat64ScaledBasisBlade basisBlade)
    {
        return BasisBlade.CpSign(basisBlade.BasisBlade) * Scalar.ScalarValue * basisBlade.Scalar.ScalarValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(RGaFloat64ScaledBasisBlade? other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;

        return Scalar.Equals(other.Scalar) && Id.Equals(other.Id);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return HashCode.Combine(Scalar, Id);
        //return Scalar.Value ^ Id.GetHashCode();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"({Scalar}){BasisBlade}";
    }
}