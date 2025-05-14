using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;

/// <summary>
/// This represents a signed basis blade of arbitrary grade for an extended GA
/// </summary>
public sealed record XGaSignedBasisBlade :
    IXGaSignedBasisBlade
{
    private readonly XGaBasisBlade _basisBlade;

    public sealed class EqualityComparer : 
        IEqualityComparer<XGaSignedBasisBlade>
    {
        public static EqualityComparer Instance { get; }
            = new EqualityComparer();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private EqualityComparer()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(XGaSignedBasisBlade? x, XGaSignedBasisBlade? y)
        {
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            if (ReferenceEquals(x, y))
                return true;

            return x.Sign.Equals(y.Sign) && x.Id.Equals(y.Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetHashCode(XGaSignedBasisBlade obj)
        {
            return HashCode.Combine(obj.Sign, obj.Id);
            //return obj.Sign.Value ^ obj.Id.GetHashCode();
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade operator +(XGaSignedBasisBlade b1)
    {
        return b1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade operator -(XGaSignedBasisBlade b1)
    {
        return new XGaSignedBasisBlade(b1.GetBasisBlade(), -b1.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade operator *(XGaSignedBasisBlade b1, IntegerSign s2)
    {
        return new XGaSignedBasisBlade(b1.GetBasisBlade(), b1.Sign * s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaSignedBasisBlade operator *(IntegerSign s1, XGaSignedBasisBlade b2)
    {
        return new XGaSignedBasisBlade(b2.GetBasisBlade(), s1 * b2.Sign);
    }


    public IntegerSign Sign { get; }

    public XGaMetric Metric 
        => _basisBlade.Metric;

    public IndexSet Id 
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
    public XGaSignedBasisBlade(XGaBasisBlade basisBlade, IntegerSign sign)
    {
        _basisBlade = basisBlade;
        Sign = sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaSignedBasisBlade(XGaMetric metric, IndexSet basisBladeId, IntegerSign sign)
    {
        _basisBlade = new XGaBasisBlade(metric, basisBladeId);
        Sign = sign;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out XGaBasisBlade basisBlade, out IntegerSign sign)
    {
        basisBlade = GetBasisBlade();
        sign = Sign;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBasisBlade GetBasisBlade()
    {
        return _basisBlade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade ShiftIndices(int offset)
    {
        if (IsScalar || offset == 0) return this;

        return new XGaSignedBasisBlade(
            Metric,
            Id.ShiftIndices(offset),
            Sign
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Negative()
    {
        return Sign.Value switch
        {
            0 => this,
            > 0 => new XGaSignedBasisBlade(_basisBlade, IntegerSign.Negative),
            _ => _basisBlade
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Reverse()
    {
        return _basisBlade.Reverse().Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade GradeInvolution()
    {
        return _basisBlade.GradeInvolution().Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade CliffordConjugate()
    {
        return _basisBlade.CliffordConjugate().Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EConjugate()
    {
        return _basisBlade.EConjugate().Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Conjugate()
    {
        return _basisBlade.Conjugate().Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Times(IntegerSign sign)
    {
        return (Sign * sign).Value switch
        {
            0 => new XGaSignedBasisBlade(_basisBlade, IntegerSign.Zero),
            < 0 => new XGaSignedBasisBlade(_basisBlade, IntegerSign.Negative),
            _ => _basisBlade
        };
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EGp(IndexSet basisBlade)
    {
        return _basisBlade.EGp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Op(IndexSet basisBlade)
    {
        return _basisBlade.Op(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade ESp(IndexSet basisBlade)
    {
        return _basisBlade.ESp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade ELcp(IndexSet basisBlade)
    {
        return _basisBlade.ELcp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade ERcp(IndexSet basisBlade)
    {
        return _basisBlade.ERcp(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EFdp(IndexSet basisBlade)
    {
        return _basisBlade.EFdp(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EHip(IndexSet basisBlade)
    {
        return _basisBlade.EHip(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EAcp(IndexSet basisBlade)
    {
        return _basisBlade.EAcp(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade ECp(IndexSet basisBlade)
    {
        return _basisBlade.ECp(basisBlade).Times(Sign);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EGp(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.EGp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Op(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.Op(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade ESp(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.ESp(basisBlade.Id).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade ELcp(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.ELcp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade ERcp(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.ERcp(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EFdp(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.EFdp(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EHip(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.EHip(basisBlade).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade EAcp(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.EAcp(basisBlade.Id).Times(Sign);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade ECp(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.ECp(basisBlade.Id).Times(Sign);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Gp(IndexSet basisBlade)
    {
        return _basisBlade.Gp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Sp(IndexSet basisBlade)
    {
        return _basisBlade.Sp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Lcp(IndexSet basisBlade)
    {
        return _basisBlade.Lcp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Rcp(IndexSet basisBlade)
    {
        return _basisBlade.Rcp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Fdp(IndexSet basisBlade)
    {
        return _basisBlade.Fdp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Hip(IndexSet basisBlade)
    {
        return _basisBlade.Hip(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Acp(IndexSet basisBlade)
    {
        return _basisBlade.Acp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Cp(IndexSet basisBlade)
    {
        return _basisBlade.Cp(basisBlade).Times(Sign);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Gp(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.Gp(basisBlade).Times(Sign * basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Sp(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.Sp(basisBlade).Times(Sign * basisBlade.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Lcp(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.Lcp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Rcp(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.Rcp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Fdp(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.Fdp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Hip(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.Hip(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Acp(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.Acp(basisBlade).Times(Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaSignedBasisBlade Cp(IXGaSignedBasisBlade basisBlade)
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
    public IntegerSign EGpSign(IndexSet basisBlade)
    {
        return _basisBlade.EGpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSign(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.EGpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GpSign(IndexSet basisBlade)
    {
        return _basisBlade.GpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign GpSign(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.GpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign OpSign(IndexSet basisBlade)
    {
        return _basisBlade.OpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign OpSign(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.OpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ESpSign(IndexSet basisBlade)
    {
        return _basisBlade.ESpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ESpSign(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.ESpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign SpSign(IndexSet basisBlade)
    {
        return _basisBlade.SpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign SpSign(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.SpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ELcpSign(IndexSet basisBlade)
    {
        return _basisBlade.ELcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ELcpSign(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.ELcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign LcpSign(IndexSet basisBlade)
    {
        return _basisBlade.LcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign LcpSign(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.LcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ERcpSign(IndexSet basisBlade)
    {
        return _basisBlade.ERcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ERcpSign(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.ERcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign RcpSign(IndexSet basisBlade)
    {
        return _basisBlade.RcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign RcpSign(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.RcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EFdpSign(IndexSet basisBlade)
    {
        return _basisBlade.EFdpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EFdpSign(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.EFdpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign FdpSign(IndexSet basisBlade)
    {
        return _basisBlade.FdpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign FdpSign(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.FdpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EHipSign(IndexSet basisBlade)
    {
        return _basisBlade.EHipSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EHipSign(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.EHipSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign HipSign(IndexSet basisBlade)
    {
        return _basisBlade.HipSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign HipSign(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.HipSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EAcpSign(IndexSet basisBlade)
    {
        return _basisBlade.EAcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EAcpSign(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.EAcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign AcpSign(IndexSet basisBlade)
    {
        return _basisBlade.AcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign AcpSign(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.AcpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ECpSign(IndexSet basisBlade)
    {
        return _basisBlade.ECpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ECpSign(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.ECpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign CpSign(IndexSet basisBlade)
    {
        return _basisBlade.CpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign CpSign(IXGaSignedBasisBlade basisBlade)
    {
        return _basisBlade.CpSign(basisBlade) * Sign;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(XGaSignedBasisBlade? other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;

        return Sign.Equals(other.Sign) && Id.Equals(other.Id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(IXGaSignedBasisBlade? other)
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