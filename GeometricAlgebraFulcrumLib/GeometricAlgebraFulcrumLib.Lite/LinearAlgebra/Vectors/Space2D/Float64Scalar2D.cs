using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

public sealed record Float64Scalar2D :
    IFloat64Multivector2D
{
    public static Float64Scalar2D Zero { get; }
        = new Float64Scalar2D(Float64Scalar.Zero);

    public static Float64Scalar2D E0 { get; }
        = new Float64Scalar2D(Float64Scalar.One);

    public static Float64Scalar2D NegativeE0 { get; }
        = new Float64Scalar2D(Float64Scalar.NegativeOne);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar2D Create(double scalar)
    {
        if (scalar.IsZero())
            return Zero;

        if (scalar.IsOne())
            return E0;

        if (scalar.IsMinusOne())
            return NegativeE0;

        return new Float64Scalar2D(scalar);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar2D operator +(Float64Scalar2D v1)
    {
        return v1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar2D operator -(Float64Scalar2D v1)
    {
        return new Float64Scalar2D(-v1.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar2D operator +(Float64Scalar2D v1, double v2)
    {
        return new Float64Scalar2D(v1.Scalar + v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar2D operator +(double v1, Float64Scalar2D v2)
    {
        return new Float64Scalar2D(v1 + v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar2D operator +(Float64Scalar2D v1, Float64Scalar2D v2)
    {
        return new Float64Scalar2D(v1.Scalar + v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar2D operator -(Float64Scalar2D v1, double v2)
    {
        return new Float64Scalar2D(v1.Scalar - v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar2D operator -(double v1, Float64Scalar2D v2)
    {
        return new Float64Scalar2D(v1 - v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar2D operator -(Float64Scalar2D v1, Float64Scalar2D v2)
    {
        return new Float64Scalar2D(v1.Scalar - v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar2D operator *(Float64Scalar2D v1, double v2)
    {
        return new Float64Scalar2D(v1.Scalar * v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar2D operator *(double v1, Float64Scalar2D v2)
    {
        return new Float64Scalar2D(v1 * v2.Scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar2D operator *(Float64Scalar2D v1, Float64Scalar2D v2)
    {
        return new Float64Scalar2D(v1.Scalar * v2.Scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar2D operator /(Float64Scalar2D v1, double v2)
    {
        return new Float64Scalar2D(v1.Scalar / v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar2D operator /(double v1, Float64Scalar2D v2)
    {
        return new Float64Scalar2D(v1 / v2.Scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar2D operator /(Float64Scalar2D v1, Float64Scalar2D v2)
    {
        return new Float64Scalar2D(v1.Scalar / v2.Scalar);
    }


    public int VSpaceDimensions 
        => 3;

    public Float64Scalar Scalar { get; }

    public Float64Scalar Scalar1 
        => Float64Scalar.Zero;

    public Float64Scalar Scalar2 
        => Float64Scalar.Zero;
    
    public Float64Scalar Scalar12 
        => Float64Scalar.Zero;
    
    public int Count 
        => 4;

    public Float64Scalar this[int index]
    {
        get
        {
            if (index is < 0 or > 3)
                throw new IndexOutOfRangeException();

            return index == 0
                ? Scalar
                : Float64Scalar.Zero;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64Scalar2D(Float64Scalar scalar)
    {
        Scalar = scalar;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Scalar.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return Scalar.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double epsilon = 1E-12)
    {
        return Scalar.IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Norm()
    {
        return Scalar.Abs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar NormSquared()
    {
        return Scalar.Square();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Multivector2D ToMultivector2D()
    {
        return Float64Multivector2D.Create(this);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar2D Negative()
    {
        return new Float64Scalar2D(-Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar2D GradeInvolution()
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar2D Reverse()
    {
        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar2D CliffordConjugate()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar2D Inverse()
    {
        return IsZero()
            ? throw new InvalidOperationException()
            : new Float64Scalar2D(Scalar.Inverse());
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bivector2D DirectionToUnitNormal2D()
    {
        if (Scalar.IsZero())
            return Float64Bivector2D.E12;

        return Scalar.IsPositive()
            ? Float64Bivector2D.E12
            : Float64Bivector2D.E21;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bivector2D DirectionToNormal2D()
    {
        if (Scalar.IsZero())
            return Float64Bivector2D.E12;

        return Float64Bivector2D.Create(1d / Scalar.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bivector2D DirectionToNormal2D(double scalingFactor)
    {
        if (Scalar.IsZero())
            return Float64Bivector2D.E12;

        return Float64Bivector2D.Create(scalingFactor / Scalar.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bivector2D NormalToUnitDirection2D()
    {
        if (Scalar.IsZero())
            return Float64Bivector2D.E12;

        return Scalar.IsPositive()
            ? Float64Bivector2D.E12
            : Float64Bivector2D.E21;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bivector2D NormalToDirection2D()
    {
        if (Scalar.IsZero())
            return Float64Bivector2D.E12;

        return Float64Bivector2D.Create(1d / Scalar.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bivector2D NormalToDirection2D(double scalingFactor)
    {
        if (Scalar.IsZero())
            return Float64Bivector2D.E12;

        return Float64Bivector2D.Create(scalingFactor / Scalar.Value);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bivector2D Dual2D()
    {
        return Float64Bivector2D.Create(-Scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Bivector2D UnDual2D()
    {
        return Float64Bivector2D.Create(Scalar);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Float64Scalar> GetEnumerator()
    {
        yield return Scalar;
        yield return Scalar1;
        yield return Scalar2;
        yield return Scalar12;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"({Scalar})<>";
    }

}