using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;

/// <summary>
/// This represents a unit basis vector in a linear algebra
/// </summary>
public sealed record LinBasisVector :
    ILinSignedBasisVector
{
    public sealed class EqualityComparer : 
        IEqualityComparer<LinBasisVector>
    {
        public static EqualityComparer Instance { get; }
            = new EqualityComparer();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private EqualityComparer()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(LinBasisVector? x, LinBasisVector? y)
        {
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            if (ReferenceEquals(x, y))
                return true;

            return x.IsNegative.Equals(y.IsNegative) && x.Index.Equals(y.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetHashCode(LinBasisVector obj)
        {
            return HashCode.Combine(obj.Sign, obj.Index);
        }
    }


    public static LinBasisVector Px { get; }
        = new LinBasisVector(0, false);

    public static LinBasisVector Py { get; }
        = new LinBasisVector(1, false);

    public static LinBasisVector Pz { get; }
        = new LinBasisVector(2, false);

    public static LinBasisVector Pw { get; }
        = new LinBasisVector(3, false);

    public static LinBasisVector Nx { get; }
        = new LinBasisVector(0, true);

    public static LinBasisVector Ny { get; }
        = new LinBasisVector(1, true);

    public static LinBasisVector Nz { get; }
        = new LinBasisVector(2, true);

    public static LinBasisVector Nw { get; }
        = new LinBasisVector(3, true);
        
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector Create(int basisVectorIndex)
    {
        return new LinBasisVector(
            basisVectorIndex, 
            false
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector Create(LinBasisVector basisVector)
    {
        return new LinBasisVector(
            basisVector, 
            false
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector Create(int basisVectorIndex, bool isNegative)
    {
        return new LinBasisVector(
            basisVectorIndex, 
            isNegative
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector Create(LinBasisVector basisVector, bool isNegative)
    {
        return new LinBasisVector(
            basisVector, 
            isNegative
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector Create(int basisVectorIndex, IntegerSign sign)
    {
        if (sign == IntegerSign.Zero)
            throw new ArgumentOutOfRangeException(nameof(sign));

        return new LinBasisVector(
            basisVectorIndex, 
            sign.IsNegative
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector Create(LinBasisVector basisVector, IntegerSign sign)
    {
        if (sign == IntegerSign.Zero)
            throw new ArgumentOutOfRangeException(nameof(sign));

        return new LinBasisVector(
            basisVector, 
            sign.IsNegative
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LinBasisVector(int basisVectorIndex)
    {
        return new LinBasisVector(basisVectorIndex, false);
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector operator +(LinBasisVector b1)
    {
        return b1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector operator -(LinBasisVector b1)
    {
        return new LinBasisVector(b1.GetBasisVector(), !b1.IsNegative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector operator *(LinBasisVector b1, IntegerSign s2)
    {
        return Create(b1.GetBasisVector(), b1.IsNegative ? -s2 : s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector operator *(IntegerSign s1, LinBasisVector b2)
    {
        return Create(b2.GetBasisVector(), b2.IsNegative ? -s1 : s1);
    }


    private readonly LinBasisVector _basisVector;

    public IntegerSign Sign 
        => IsNegative ? IntegerSign.Negative : IntegerSign.Positive;
    
    public int Index 
        => _basisVector.Index;

    public bool IsNegative { get; }

    public bool IsZero
        => false;

    public bool IsPositive
        => !IsNegative;

    public bool IsNonNegative 
        => !IsNegative;

    public bool IsNonZero
        => true;

    public bool IsNonPositive 
        => IsNegative;
    
    public int VSpaceDimensions
        => _basisVector.VSpaceDimensions;
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinBasisVector(int basisVectorIndex, bool isNegative)
    {
        _basisVector = new LinBasisVector(basisVectorIndex);
        IsNegative = isNegative;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinBasisVector(LinBasisVector basisVector, bool isNegative)
    {
        _basisVector = basisVector;
        IsNegative = isNegative;
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
            : Create(Index + offset, IsNegative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinSignedBasisVector Negative()
    {
        return new LinBasisVector(_basisVector, !IsNegative);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinSignedBasisVector Times(IntegerSign sign)
    {
        if (sign.IsZero)
            return new LinSignedBasisVector(_basisVector, sign);

        if (sign.IsPositive)
            return this;

        return Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign NegativeSign()
    {
        return IsNegative 
            ? IntegerSign.Positive 
            : IntegerSign.Negative;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign TimesSign(IntegerSign sign)
    {
        return IsNegative ? -sign : sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSign(int basisBlade)
    {
        var sign = _basisVector.EGpSign(basisBlade);

        return IsNegative ? -sign : sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSign(ILinSignedBasisVector basisBlade)
    {
        var sign = _basisVector.EGpSign(basisBlade);

        return IsNegative ? -sign : sign;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign OpSign(int basisBlade)
    {
        var sign = _basisVector.OpSign(basisBlade);

        return IsNegative ? -sign : sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign OpSign(ILinSignedBasisVector basisBlade)
    {
        var sign = _basisVector.OpSign(basisBlade);

        return IsNegative ? -sign : sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ESpSign(int basisBlade)
    {
        var sign = _basisVector.ESpSign(basisBlade);

        return IsNegative ? -sign : sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ESpSign(ILinSignedBasisVector basisBlade)
    {
        var sign = _basisVector.ESpSign(basisBlade);

        return IsNegative ? -sign : sign;
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
    public bool Equals(LinBasisVector? other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;

        return IsNegative.Equals(other.IsNegative) && Index.Equals(other.Index);
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