﻿using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space3D;

public sealed record LinFloat64Scalar3D :
    ILinFloat64KVector3D,
    IFloat64Scalar
{
    public static LinFloat64Scalar3D Zero { get; }
        = new LinFloat64Scalar3D(Float64Scalar.Zero);

    public static LinFloat64Scalar3D E0 { get; }
        = new LinFloat64Scalar3D(Float64Scalar.One);

    public static LinFloat64Scalar3D NegativeE0 { get; }
        = new LinFloat64Scalar3D(Float64Scalar.NegativeOne);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D Create(Float64Scalar scalar)
    {
        if (scalar.IsZero())
            return Zero;

        if (scalar.IsOne())
            return E0;

        if (scalar.IsNegativeOne())
            return NegativeE0;

        return new LinFloat64Scalar3D(scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D operator +(LinFloat64Scalar3D v1)
    {
        return v1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D operator -(LinFloat64Scalar3D v1)
    {
        return new LinFloat64Scalar3D(-v1.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D operator +(LinFloat64Scalar3D v1, double v2)
    {
        return new LinFloat64Scalar3D(v1.Scalar + v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D operator +(double v1, LinFloat64Scalar3D v2)
    {
        return new LinFloat64Scalar3D(v1 + v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D operator +(LinFloat64Scalar3D v1, LinFloat64Scalar3D v2)
    {
        return new LinFloat64Scalar3D(v1.Scalar + v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D operator -(LinFloat64Scalar3D v1, double v2)
    {
        return new LinFloat64Scalar3D(v1.Scalar - v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D operator -(double v1, LinFloat64Scalar3D v2)
    {
        return new LinFloat64Scalar3D(v1 - v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D operator -(LinFloat64Scalar3D v1, LinFloat64Scalar3D v2)
    {
        return new LinFloat64Scalar3D(v1.Scalar - v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D operator *(LinFloat64Scalar3D v1, double v2)
    {
        return new LinFloat64Scalar3D(v1.Scalar * v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D operator *(double v1, LinFloat64Scalar3D v2)
    {
        return new LinFloat64Scalar3D(v1 * v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D operator *(LinFloat64Scalar3D v1, LinFloat64Scalar3D v2)
    {
        return new LinFloat64Scalar3D(v1.Scalar * v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D operator /(LinFloat64Scalar3D v1, double v2)
    {
        return new LinFloat64Scalar3D(v1.Scalar / v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D operator /(double v1, LinFloat64Scalar3D v2)
    {
        return new LinFloat64Scalar3D(v1 / v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D operator /(LinFloat64Scalar3D v1, LinFloat64Scalar3D v2)
    {
        return new LinFloat64Scalar3D(v1.Scalar / v2.Scalar);
    }


    public int VSpaceDimensions
        => 3;

    public int Grade
        => 0;

    public Float64Scalar Scalar { get; }

    public Float64Scalar Scalar1
        => Float64Scalar.Zero;

    public Float64Scalar Scalar2
        => Float64Scalar.Zero;

    public Float64Scalar Scalar3
        => Float64Scalar.Zero;

    public Float64Scalar Scalar12
        => Float64Scalar.Zero;

    public Float64Scalar Scalar13
        => Float64Scalar.Zero;

    public Float64Scalar Scalar23
        => Float64Scalar.Zero;

    public Float64Scalar Scalar123
        => Float64Scalar.Zero;

    public int Count
        => 8;

    public Float64Scalar this[int index]
    {
        get
        {
            if (index is < 0 or > 7)
                throw new IndexOutOfRangeException();

            return index == 0
                ? Scalar
                : Float64Scalar.Zero;
        }
    }

    public double ScalarValue 
        => Scalar.ScalarValue;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64Scalar3D(Float64Scalar scalar)
    {
        Scalar = scalar;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Scalar.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ToScalar()
    {
        return Scalar;
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
    public LinFloat64Multivector3D ToMultivector3D()
    {
        return LinFloat64Multivector3D.Create(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar3D Negative()
    {
        return new LinFloat64Scalar3D(-Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar3D GradeInvolution()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar3D Reverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar3D CliffordConjugate()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar3D Inverse()
    {
        return IsZero()
            ? throw new InvalidOperationException()
            : new LinFloat64Scalar3D(Scalar.Inverse());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Trivector3D DirectionToUnitNormal3D()
    {
        if (Scalar.IsZero())
            return LinFloat64Trivector3D.E123;

        return Scalar.IsPositive()
            ? LinFloat64Trivector3D.E123
            : LinFloat64Trivector3D.NegativeE123;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Trivector3D DirectionToNormal3D()
    {
        if (Scalar.IsZero())
            return LinFloat64Trivector3D.E123;

        return LinFloat64Trivector3D.Create(1d / Scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Trivector3D DirectionToNormal3D(double scalingFactor)
    {
        if (Scalar.IsZero())
            return LinFloat64Trivector3D.E123;

        return LinFloat64Trivector3D.Create(scalingFactor / Scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Trivector3D NormalToUnitDirection3D()
    {
        if (Scalar.IsZero())
            return LinFloat64Trivector3D.E123;

        return Scalar.IsPositive()
            ? LinFloat64Trivector3D.E123
            : LinFloat64Trivector3D.NegativeE123;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Trivector3D NormalToDirection3D()
    {
        if (Scalar.IsZero())
            return LinFloat64Trivector3D.E123;

        return LinFloat64Trivector3D.Create(1d / Scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Trivector3D NormalToDirection3D(double scalingFactor)
    {
        if (Scalar.IsZero())
            return LinFloat64Trivector3D.E123;

        return LinFloat64Trivector3D.Create(scalingFactor / Scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Trivector3D Dual()
    {
        return LinFloat64Trivector3D.Create(Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Trivector3D UnDual()
    {
        return LinFloat64Trivector3D.Create(Scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Float64Scalar> GetEnumerator()
    {
        yield return Scalar;
        yield return Scalar1;
        yield return Scalar2;
        yield return Scalar12;
        yield return Scalar3;
        yield return Scalar13;
        yield return Scalar23;
        yield return Scalar123;
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