using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;

/// <summary>
/// This represents a signed basis blade of arbitrary grade for an extended GA
/// </summary>
public sealed record RGaSignedBasisBlade :
    IRGaSignedBasisBlade
{
    private readonly RGaBasisBlade _basisBlade;

    public sealed class EqualityComparer : 
        IEqualityComparer<RGaSignedBasisBlade>
    {
        public static EqualityComparer Instance { get; }
            = new EqualityComparer();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private EqualityComparer()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(RGaSignedBasisBlade? x, RGaSignedBasisBlade? y)
        {
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            if (ReferenceEquals(x, y))
                return true;

            return x.Sign.Equals(y.Sign) && x.Id.Equals(y.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetHashCode(RGaSignedBasisBlade obj)
        {
            return HashCode.Combine(obj.Sign, obj.Id);
            //return obj.Sign.Value ^ obj.Id.GetHashCode();
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade operator +(RGaSignedBasisBlade b1)
    {
        return b1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade operator -(RGaSignedBasisBlade b1)
    {
        return new RGaSignedBasisBlade(b1.GetBasisBlade(), -b1.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade operator *(RGaSignedBasisBlade b1, IntegerSign s2)
    {
        return new RGaSignedBasisBlade(b1.GetBasisBlade(), b1.Sign * s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaSignedBasisBlade operator *(IntegerSign s1, RGaSignedBasisBlade b2)
    {
        return new RGaSignedBasisBlade(b2.GetBasisBlade(), s1 * b2.Sign);
    }


    public IntegerSign Sign { get; }

    public RGaMetric Metric 
        => _basisBlade.Metric;

    public ulong Id 
        => _basisBlade.Id;

    public bool IsNegative
        => Sign.IsNegative;

    public bool IsZero
        => Sign.IsZero;

    public bool IsPositive
        => Sign.IsPositive;

    public bool IsNonNegative 
        => Sign.IsNotNegative;

    public bool IsNonZero
        => Sign.IsNotZero;

    public bool IsNonPositive 
        => Sign.IsNotPositive;

    public bool IsScalar 
        => _basisBlade.IsScalar;

    public bool IsVector 
        => _basisBlade.IsVector;

    public bool IsBivector 
        => _basisBlade.IsBivector;

    public int VSpaceDimensions
        => _basisBlade.VSpaceDimensions;

    public int Grade
        => _basisBlade.Grade;

    public ulong KvSpaceDimensions
        => _basisBlade.KvSpaceDimensions;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaSignedBasisBlade(RGaBasisBlade basisBlade, IntegerSign sign)
    {
        _basisBlade = basisBlade;
        Sign = sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaSignedBasisBlade(RGaMetric metric, ulong basisBladeId, IntegerSign sign)
    {
        _basisBlade = new RGaBasisBlade(metric, basisBladeId);
        Sign = sign;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out RGaBasisBlade basisBlade, out IntegerSign sign)
    {
        basisBlade = _basisBlade;
        sign = Sign;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBasisBlade GetBasisBlade()
    {
        return _basisBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ShiftIndices(int offset)
    {
        if (IsScalar || offset == 0) return this;

        return new RGaSignedBasisBlade(
            Metric,
            Id.ShiftOnes(offset),
            Sign
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Negative()
    {
        return Sign.Value switch
        {
            0 => this,
            > 0 => new RGaSignedBasisBlade(_basisBlade, IntegerSign.Negative),
            _ => _basisBlade
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Reverse()
    {
        return _basisBlade.Reverse().Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade GradeInvolution()
    {
        return _basisBlade.GradeInvolution().Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade CliffordConjugate()
    {
        return _basisBlade.CliffordConjugate().Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EConjugate()
    {
        return _basisBlade.EConjugate().Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Conjugate()
    {
        return _basisBlade.Conjugate().Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Times(IntegerSign sign)
    {
        return (Sign * sign).Value switch
        {
            0 => new RGaSignedBasisBlade(_basisBlade, IntegerSign.Zero),
            < 0 => new RGaSignedBasisBlade(_basisBlade, IntegerSign.Negative),
            _ => _basisBlade
        };
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EGp(ulong basisBlade)
    {
        return _basisBlade.EGp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Op(ulong basisBlade)
    {
        return _basisBlade.Op(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ESp(ulong basisBlade)
    {
        return _basisBlade.ESp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ELcp(ulong basisBlade)
    {
        return _basisBlade.ELcp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ERcp(ulong basisBlade)
    {
        return _basisBlade.ERcp(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EFdp(ulong basisBlade)
    {
        return _basisBlade.EFdp(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EHip(ulong basisBlade)
    {
        return _basisBlade.EHip(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EAcp(ulong basisBlade)
    {
        return _basisBlade.EAcp(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ECp(ulong basisBlade)
    {
        return _basisBlade.ECp(basisBlade).Times(Sign);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EGp(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.EGp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Op(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.Op(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ESp(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.ESp(basisBlade.Id).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ELcp(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.ELcp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ERcp(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.ERcp(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EFdp(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.EFdp(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EHip(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.EHip(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade EAcp(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.EAcp(basisBlade.Id).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade ECp(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.ECp(basisBlade.Id).Times(Sign);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Gp(ulong basisBlade)
    {
        return _basisBlade.Gp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Sp(ulong basisBlade)
    {
        return _basisBlade.Sp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Lcp(ulong basisBlade)
    {
        return _basisBlade.Lcp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Rcp(ulong basisBlade)
    {
        return _basisBlade.Rcp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Fdp(ulong basisBlade)
    {
        return _basisBlade.Fdp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Hip(ulong basisBlade)
    {
        return _basisBlade.Hip(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Acp(ulong basisBlade)
    {
        return _basisBlade.Acp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Cp(ulong basisBlade)
    {
        return _basisBlade.Cp(basisBlade).Times(Sign);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Gp(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.Gp(basisBlade).Times(Sign * basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Sp(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.Sp(basisBlade).Times(Sign * basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Lcp(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.Lcp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Rcp(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.Rcp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Fdp(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.Fdp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Hip(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.Hip(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Acp(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.Acp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaSignedBasisBlade Cp(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.Cp(basisBlade).Times(Sign);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign Signature()
    {
        return Sign.IsZero 
            ? IntegerSign.Zero 
            : _basisBlade.Signature();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign NegativeSign()
    {
        return -Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ReverseSign()
    {
        return _basisBlade.ReverseSign() * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GradeInvolutionSign()
    {
        return _basisBlade.GradeInvolutionSign() * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign CliffordConjugateSign()
    {
        return _basisBlade.CliffordConjugateSign() * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EConjugateSign()
    {
        return _basisBlade.EConjugateSign() * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ConjugateSign()
    {
        return _basisBlade.ConjugateSign() * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign TimesSign(IntegerSign sign)
    {
        return Sign * sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSign(ulong basisBlade)
    {
        return _basisBlade.EGpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSign(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.EGpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GpSign(ulong basisBlade)
    {
        return _basisBlade.GpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GpSign(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.GpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign OpSign(ulong basisBlade)
    {
        return _basisBlade.OpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign OpSign(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.OpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ESpSign(ulong basisBlade)
    {
        return _basisBlade.ESpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ESpSign(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.ESpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign SpSign(ulong basisBlade)
    {
        return _basisBlade.SpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign SpSign(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.SpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ELcpSign(ulong basisBlade)
    {
        return _basisBlade.ELcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ELcpSign(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.ELcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign LcpSign(ulong basisBlade)
    {
        return _basisBlade.LcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign LcpSign(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.LcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ERcpSign(ulong basisBlade)
    {
        return _basisBlade.ERcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ERcpSign(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.ERcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign RcpSign(ulong basisBlade)
    {
        return _basisBlade.RcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign RcpSign(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.RcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EFdpSign(ulong basisBlade)
    {
        return _basisBlade.EFdpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EFdpSign(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.EFdpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign FdpSign(ulong basisBlade)
    {
        return _basisBlade.FdpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign FdpSign(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.FdpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EHipSign(ulong basisBlade)
    {
        return _basisBlade.EHipSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EHipSign(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.EHipSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign HipSign(ulong basisBlade)
    {
        return _basisBlade.HipSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign HipSign(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.HipSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EAcpSign(ulong basisBlade)
    {
        return _basisBlade.EAcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EAcpSign(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.EAcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign AcpSign(ulong basisBlade)
    {
        return _basisBlade.AcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign AcpSign(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.AcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ECpSign(ulong basisBlade)
    {
        return _basisBlade.ECpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ECpSign(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.ECpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign CpSign(ulong basisBlade)
    {
        return _basisBlade.CpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign CpSign(IRGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.CpSign(basisBlade) * Sign;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(RGaSignedBasisBlade? other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;

        return Sign.Equals(other.Sign) && Id.Equals(other.Id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(IRGaSignedBasisBlade? other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;

        return Sign.Equals(other.Sign) && Id.Equals(other.Id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return HashCode.Combine(Sign, Id);
        //return Sign.Value ^ Id.GetHashCode();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"({Sign}){_basisBlade}";
    }
}