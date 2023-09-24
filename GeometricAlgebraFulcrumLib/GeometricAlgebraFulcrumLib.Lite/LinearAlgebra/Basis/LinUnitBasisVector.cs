using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;

/// <summary>
/// This represents a unit basis vector in a linear algebra
/// </summary>
public sealed record LinUnitBasisVector :
    ILinSignedBasisVector
{
    public sealed class EqualityComparer : 
        IEqualityComparer<LinUnitBasisVector>
    {
        public static EqualityComparer Instance { get; }
            = new EqualityComparer();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private EqualityComparer()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(LinUnitBasisVector? x, LinUnitBasisVector? y)
        {
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            if (ReferenceEquals(x, y))
                return true;

            return x.IsNegative.Equals(y.IsNegative) && x.Index.Equals(y.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetHashCode(LinUnitBasisVector obj)
        {
            return HashCode.Combine(obj.Sign, obj.Index);
        }
    }


    public static LinUnitBasisVector PositiveX { get; }
        = new LinUnitBasisVector(0, false);

    public static LinUnitBasisVector PositiveY { get; }
        = new LinUnitBasisVector(1, false);

    public static LinUnitBasisVector PositiveZ { get; }
        = new LinUnitBasisVector(2, false);

    public static LinUnitBasisVector PositiveW { get; }
        = new LinUnitBasisVector(3, false);

    public static LinUnitBasisVector NegativeX { get; }
        = new LinUnitBasisVector(0, true);

    public static LinUnitBasisVector NegativeY { get; }
        = new LinUnitBasisVector(1, true);

    public static LinUnitBasisVector NegativeZ { get; }
        = new LinUnitBasisVector(2, true);

    public static LinUnitBasisVector NegativeW { get; }
        = new LinUnitBasisVector(3, true);
        
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector Create(int basisVectorIndex)
    {
        return new LinUnitBasisVector(
            basisVectorIndex, 
            false
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector Create(LinBasisVector basisVector)
    {
        return new LinUnitBasisVector(
            basisVector, 
            false
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector Create(int basisVectorIndex, bool isNegative)
    {
        return new LinUnitBasisVector(
            basisVectorIndex, 
            isNegative
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector Create(LinBasisVector basisVector, bool isNegative)
    {
        return new LinUnitBasisVector(
            basisVector, 
            isNegative
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector Create(int basisVectorIndex, IntegerSign sign)
    {
        if (sign == IntegerSign.Zero)
            throw new ArgumentOutOfRangeException(nameof(sign));

        return new LinUnitBasisVector(
            basisVectorIndex, 
            sign.IsNegative
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector Create(LinBasisVector basisVector, IntegerSign sign)
    {
        if (sign == IntegerSign.Zero)
            throw new ArgumentOutOfRangeException(nameof(sign));

        return new LinUnitBasisVector(
            basisVector, 
            sign.IsNegative
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LinUnitBasisVector(int basisVectorIndex)
    {
        return new LinUnitBasisVector(basisVectorIndex, false);
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector operator +(LinUnitBasisVector b1)
    {
        return b1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector operator -(LinUnitBasisVector b1)
    {
        return new LinUnitBasisVector(b1.GetBasisVector(), !b1.IsNegative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector operator *(LinUnitBasisVector b1, IntegerSign s2)
    {
        return Create(b1.GetBasisVector(), b1.IsNegative ? -s2 : s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnitBasisVector operator *(IntegerSign s1, LinUnitBasisVector b2)
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
    private LinUnitBasisVector(int basisVectorIndex, bool isNegative)
    {
        _basisVector = new LinBasisVector(basisVectorIndex);
        IsNegative = isNegative;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinUnitBasisVector(LinBasisVector basisVector, bool isNegative)
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
        return new LinUnitBasisVector(_basisVector, !IsNegative);
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
    public bool Equals(LinUnitBasisVector? other)
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