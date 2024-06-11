using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;

/// <summary>
/// This represents a basis vector in a linear algebra
/// </summary>
public sealed record LinBasisVector : 
    ILinSignedBasisVector
{
    public int Index { get; }

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
    
    public int VSpaceDimensions 
        => Index + 1;
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector(int basisVectorIndex)
    {
        if (Index < 0)
            throw new ArgumentOutOfRangeException(nameof(basisVectorIndex));

        Index = basisVectorIndex;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector GetBasisVector()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign NegativeSign()
    {
        return IntegerSign.Negative;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinSignedBasisVector ShiftIndex(int offset)
    {
        return offset == 0 
            ? this 
            : new LinBasisVector(Index + offset);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinSignedBasisVector Negative()
    {
        return new LinSignedBasisVector(this, IntegerSign.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinSignedBasisVector Times(IntegerSign sign)
    {
        return sign.Value switch
        {
            > 0 => this,
            < 0 => new LinSignedBasisVector(this, IntegerSign.Negative),
            _ => new LinSignedBasisVector(this, IntegerSign.Zero)
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign TimesSign(IntegerSign sign)
    {
        return sign;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSign(int index2)
    {
        return Index <= index2 
            ? IntegerSign.Positive 
            : IntegerSign.Negative;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign EGpSign(ILinSignedBasisVector basisBlade)
    {
        if (basisBlade.IsZero)
            return IntegerSign.Zero;

        var sign = Index <= basisBlade.Index
            ? IntegerSign.Positive
            : IntegerSign.Negative;

        return basisBlade.IsPositive
            ? sign 
            : -sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign OpSign(int index2)
    {
        if (Index == index2)
            return IntegerSign.Zero;

        return Index < index2 
            ? IntegerSign.Positive 
            : IntegerSign.Negative;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign OpSign(ILinSignedBasisVector basisBlade)
    {
        if (basisBlade.IsZero || Index == basisBlade.Index)
            return IntegerSign.Zero;

        var sign = Index <= basisBlade.Index
            ? IntegerSign.Positive
            : IntegerSign.Negative;

        return basisBlade.IsPositive
            ? sign 
            : -sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ESpSign(int index2)
    {
        return Index == index2 
            ? IntegerSign.Positive 
            : IntegerSign.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IntegerSign ESpSign(ILinSignedBasisVector basisBlade)
    {
        return basisBlade.IsZero || Index != basisBlade.Index 
            ? IntegerSign.Zero
            : basisBlade.Sign;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"<{Index}>";
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSame(ILinSignedBasisVector axis)
    {
        return Index == axis.Index &&
               axis.Sign == IntegerSign.Positive;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsOpposite(ILinSignedBasisVector axis)
    {
        return Index == axis.Index &&
               axis.Sign == IntegerSign.Negative;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(LinBasisVector? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        
        return Index == other.Index;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(ILinSignedBasisVector? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return other.Sign.IsPositive && Index == other.Index;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return HashCode.Combine(Sign, Index);
    }
}