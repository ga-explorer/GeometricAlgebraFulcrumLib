using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

public sealed record LinFloat64Scalar2D :
    ILinFloat64Multivector2D,
    IFloat64Scalar
{
    public static LinFloat64Scalar2D Zero { get; }
        = new LinFloat64Scalar2D(Float64Scalar.Zero);

    public static LinFloat64Scalar2D E0 { get; }
        = new LinFloat64Scalar2D(Float64Scalar.One);

    public static LinFloat64Scalar2D NegativeE0 { get; }
        = new LinFloat64Scalar2D(Float64Scalar.NegativeOne);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar2D Create(double scalar)
    {
        if (scalar.IsZero())
            return Zero;

        if (scalar.IsOne())
            return E0;

        if (scalar.IsMinusOne())
            return NegativeE0;

        return new LinFloat64Scalar2D(scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar2D operator +(LinFloat64Scalar2D v1)
    {
        return v1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar2D operator -(LinFloat64Scalar2D v1)
    {
        return new LinFloat64Scalar2D(-v1.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar2D operator +(LinFloat64Scalar2D v1, double v2)
    {
        return new LinFloat64Scalar2D(v1.Scalar + v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar2D operator +(double v1, LinFloat64Scalar2D v2)
    {
        return new LinFloat64Scalar2D(v1 + v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar2D operator +(LinFloat64Scalar2D v1, LinFloat64Scalar2D v2)
    {
        return new LinFloat64Scalar2D(v1.Scalar + v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar2D operator -(LinFloat64Scalar2D v1, double v2)
    {
        return new LinFloat64Scalar2D(v1.Scalar - v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar2D operator -(double v1, LinFloat64Scalar2D v2)
    {
        return new LinFloat64Scalar2D(v1 - v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar2D operator -(LinFloat64Scalar2D v1, LinFloat64Scalar2D v2)
    {
        return new LinFloat64Scalar2D(v1.Scalar - v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar2D operator *(LinFloat64Scalar2D v1, double v2)
    {
        return new LinFloat64Scalar2D(v1.Scalar * v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar2D operator *(double v1, LinFloat64Scalar2D v2)
    {
        return new LinFloat64Scalar2D(v1 * v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar2D operator *(LinFloat64Scalar2D v1, LinFloat64Scalar2D v2)
    {
        return new LinFloat64Scalar2D(v1.Scalar * v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar2D operator /(LinFloat64Scalar2D v1, double v2)
    {
        return new LinFloat64Scalar2D(v1.Scalar / v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar2D operator /(double v1, LinFloat64Scalar2D v2)
    {
        return new LinFloat64Scalar2D(v1 / v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar2D operator /(LinFloat64Scalar2D v1, LinFloat64Scalar2D v2)
    {
        return new LinFloat64Scalar2D(v1.Scalar / v2.Scalar);
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
    
    public double ScalarValue 
        => Scalar.ScalarValue;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64Scalar2D(Float64Scalar scalar)
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
    public bool IsNearZero(double zeroEpsilon = 1E-12)
    {
        return Scalar.IsNearZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ToScalar()
    {
        return Scalar;
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
    public LinFloat64Multivector2D ToMultivector2D()
    {
        return LinFloat64Multivector2D.Create(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar2D Negative()
    {
        return new LinFloat64Scalar2D(-Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar2D GradeInvolution()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar2D Reverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar2D CliffordConjugate()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar2D Inverse()
    {
        return IsZero()
            ? throw new InvalidOperationException()
            : new LinFloat64Scalar2D(Scalar.Inverse());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D DirectionToUnitNormal2D()
    {
        if (Scalar.IsZero())
            return LinFloat64Bivector2D.E12;

        return Scalar.IsPositive()
            ? LinFloat64Bivector2D.E12
            : LinFloat64Bivector2D.E21;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D DirectionToNormal2D()
    {
        if (Scalar.IsZero())
            return LinFloat64Bivector2D.E12;

        return LinFloat64Bivector2D.Create(1d / Scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D DirectionToNormal2D(double scalingFactor)
    {
        if (Scalar.IsZero())
            return LinFloat64Bivector2D.E12;

        return LinFloat64Bivector2D.Create(scalingFactor / Scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D NormalToUnitDirection2D()
    {
        if (Scalar.IsZero())
            return LinFloat64Bivector2D.E12;

        return Scalar.IsPositive()
            ? LinFloat64Bivector2D.E12
            : LinFloat64Bivector2D.E21;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D NormalToDirection2D()
    {
        if (Scalar.IsZero())
            return LinFloat64Bivector2D.E12;

        return LinFloat64Bivector2D.Create(1d / Scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D NormalToDirection2D(double scalingFactor)
    {
        if (Scalar.IsZero())
            return LinFloat64Bivector2D.E12;

        return LinFloat64Bivector2D.Create(scalingFactor / Scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D Dual2D()
    {
        return LinFloat64Bivector2D.Create(-Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D UnDual2D()
    {
        return LinFloat64Bivector2D.Create(Scalar);
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