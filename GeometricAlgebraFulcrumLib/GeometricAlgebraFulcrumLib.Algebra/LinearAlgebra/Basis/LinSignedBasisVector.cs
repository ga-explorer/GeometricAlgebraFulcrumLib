using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;

/// <summary>
/// This represents a signed basis blade of arbitrary grade for an extended GA
/// </summary>
public sealed record LinSignedBasisVector :
    ILinSignedBasisVector
{
    public sealed class EqualityComparer : 
        IEqualityComparer<LinSignedBasisVector>
    {
        public static EqualityComparer Instance { get; }
            = new EqualityComparer();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private EqualityComparer()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(LinSignedBasisVector? x, LinSignedBasisVector? y)
        {
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            if (ReferenceEquals(x, y))
                return true;

            return x.Sign.Equals(y.Sign) && x.Index.Equals(y.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetHashCode(LinSignedBasisVector obj)
        {
            return HashCode.Combine(obj.Sign, obj.Index);
        }
    }


    public static LinSignedBasisVector Px { get; }
        = new LinSignedBasisVector(0, false);

    public static LinSignedBasisVector Py { get; }
        = new LinSignedBasisVector(1, false);

    public static LinSignedBasisVector Pz { get; }
        = new LinSignedBasisVector(2, false);

    public static LinSignedBasisVector Pw { get; }
        = new LinSignedBasisVector(3, false);

    public static LinSignedBasisVector Nx { get; }
        = new LinSignedBasisVector(0, true);

    public static LinSignedBasisVector Ny { get; }
        = new LinSignedBasisVector(1, true);

    public static LinSignedBasisVector Nz { get; }
        = new LinSignedBasisVector(2, true);

    public static LinSignedBasisVector Nw { get; }
        = new LinSignedBasisVector(3, true);
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinSignedBasisVector Create(int basisVectorIndex, bool isNegative)
    {
        return new LinSignedBasisVector(
            basisVectorIndex, 
            isNegative ? IntegerSign.Negative : IntegerSign.Positive
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinSignedBasisVector Create(int basisVectorIndex, IntegerSign sign)
    {
        return new LinSignedBasisVector(
            basisVectorIndex, 
            sign
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LinSignedBasisVector(int basisVectorIndex)
    {
        return new LinSignedBasisVector(basisVectorIndex, false);
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinSignedBasisVector operator +(LinSignedBasisVector b1)
    {
        return b1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinSignedBasisVector operator -(LinSignedBasisVector b1)
    {
        return new LinSignedBasisVector(b1.GetBasisVector(), -b1.Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinSignedBasisVector operator *(LinSignedBasisVector b1, IntegerSign s2)
    {
        return new LinSignedBasisVector(b1.GetBasisVector(), b1.Sign * s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinSignedBasisVector operator *(IntegerSign s1, LinSignedBasisVector b2)
    {
        return new LinSignedBasisVector(b2.GetBasisVector(), s1 * b2.Sign);
    }


    private readonly LinBasisVector _basisVector;

    public IntegerSign Sign { get; }
    
    public int Index 
        => _basisVector.Index;

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
    
    public int VSpaceDimensions
        => _basisVector.VSpaceDimensions;
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinSignedBasisVector(LinBasisVector basisBlade, IntegerSign sign)
    {
        _basisVector = basisBlade;
        Sign = sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinSignedBasisVector(int basisVectorIndex, IntegerSign sign)
    {
        _basisVector = LinBasisVector.Create(basisVectorIndex);
        Sign = sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinSignedBasisVector(int basisVectorIndex, bool isNegative)
    {
        _basisVector = LinBasisVector.Create(basisVectorIndex);
        Sign = isNegative ? IntegerSign.Negative : IntegerSign.Positive;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out LinBasisVector basisBlade, out IntegerSign sign)
    {
        basisBlade = _basisVector;
        sign = Sign;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector GetBasisVector()
    {
        return _basisVector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinSignedBasisVector ShiftIndex(int offset)
    {
        return offset == 0 
            ? this 
            : Create(Index + offset, Sign);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinSignedBasisVector Negative()
    {
        return Sign.Value switch
        {
            0 => this,
            > 0 => new LinSignedBasisVector(_basisVector, IntegerSign.Negative),
            _ => _basisVector
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinSignedBasisVector Times(IntegerSign sign)
    {
        return (Sign * sign).Value switch
        {
            0 => new LinSignedBasisVector(_basisVector, IntegerSign.Zero),
            < 0 => new LinSignedBasisVector(_basisVector, IntegerSign.Negative),
            _ => _basisVector
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign NegativeSign()
    {
        return -Sign;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign TimesSign(IntegerSign sign)
    {
        return Sign * sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSign(int basisBlade)
    {
        return _basisVector.EGpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSign(ILinSignedBasisVector basisBlade)
    {
        return _basisVector.EGpSign(basisBlade) * Sign;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign OpSign(int basisBlade)
    {
        return _basisVector.OpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign OpSign(ILinSignedBasisVector basisBlade)
    {
        return _basisVector.OpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ESpSign(int basisBlade)
    {
        return _basisVector.ESpSign(basisBlade) * Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ESpSign(ILinSignedBasisVector basisBlade)
    {
        return _basisVector.ESpSign(basisBlade) * Sign;
    }
    
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSame(ILinSignedBasisVector axis)
    {
        return Index == axis.Index &&
               Sign == axis.Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsOpposite(ILinSignedBasisVector axis)
    {
        return Index == axis.Index &&
               Sign == -axis.Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(LinSignedBasisVector? other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;

        return Sign.Equals(other.Sign) && Index.Equals(other.Index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(ILinSignedBasisVector? other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;

        return Sign.Equals(other.Sign) && Index.Equals(other.Index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return HashCode.Combine(Sign, Index);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"({Sign}){_basisVector}";
    }
}