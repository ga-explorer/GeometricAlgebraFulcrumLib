﻿using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

public sealed record LinFloat64Bivector2D :
    ILinFloat64Multivector2D
{
    public static LinFloat64Bivector2D Zero { get; }
        = new LinFloat64Bivector2D(Float64Scalar.Zero);

    public static LinFloat64Bivector2D E12 { get; }
        = new LinFloat64Bivector2D(Float64Scalar.One);

    public static LinFloat64Bivector2D E21 { get; }
        = new LinFloat64Bivector2D(Float64Scalar.NegativeOne);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector2D Create(double scalar12)
    {
        return new LinFloat64Bivector2D(scalar12);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector2D operator +(LinFloat64Bivector2D mv1)
    {
        return mv1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector2D operator -(LinFloat64Bivector2D mv1)
    {
        return new LinFloat64Bivector2D(-mv1.Scalar12);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector2D operator +(LinFloat64Bivector2D mv1, LinFloat64Bivector2D mv2)
    {
        return new LinFloat64Bivector2D(mv1.Scalar12 + mv2.Scalar12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector2D operator -(LinFloat64Bivector2D mv1, LinFloat64Bivector2D mv2)
    {
        return new LinFloat64Bivector2D(mv1.Scalar12 - mv2.Scalar12);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector2D operator *(LinFloat64Bivector2D mv1, double mv2)
    {
        return new LinFloat64Bivector2D(mv1.Scalar12 * mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector2D operator *(double mv1, LinFloat64Bivector2D mv2)
    {
        return new LinFloat64Bivector2D(mv1 * mv2.Scalar12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector2D operator /(LinFloat64Bivector2D mv1, double mv2)
    {
        mv2 = 1d / mv2;

        return new LinFloat64Bivector2D(mv1.Scalar12 * mv2);
    }


    public int VSpaceDimensions
        => 2;

    public double Item1
        => Scalar12;

    public Float64Scalar Xy
        => Scalar12;

    public Float64Scalar Yx
        => -Scalar12;

    public Float64Scalar Scalar
        => Float64Scalar.Zero;

    public Float64Scalar Scalar1
        => Float64Scalar.Zero;

    public Float64Scalar Scalar2
        => Float64Scalar.Zero;

    public Float64Scalar Scalar12 { get; }

    public int Count
        => 4;

    /// <summary>
    /// Get or set the ith component of this multivector
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Float64Scalar this[int index]
    {
        get
        {
            if (index is < 0 or > 3)
                throw new IndexOutOfRangeException();

            return index switch
            {
                3 => Scalar12,
                _ => Float64Scalar.Zero
            };
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64Bivector2D(Float64Scalar scalar12)
    {
        Scalar12 = scalar12;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Scalar12.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return Scalar12.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double epsilon = 1E-12)
    {
        return Norm().IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Norm()
    {
        return NormSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar NormSquared()
    {
        return Scalar12.Square();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D ToUnitBivector(bool zeroAsSymmetric = true)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroAsSymmetric ? E12 : Zero;

        return E12;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Multivector2D ToMultivector2D()
    {
        return LinFloat64Multivector2D.Create(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D Negative()
    {
        return new LinFloat64Bivector2D(-Scalar12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D Negative(double scalingFactor)
    {
        return new LinFloat64Bivector2D(
            -Scalar12 * scalingFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D GradeInvolution()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D Reverse()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D CliffordConjugate()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D Inverse()
    {
        return Negative(1d / NormSquared().ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar2D DirectionToUnitNormal2D()
    {
        if (Scalar12.IsZero())
            return LinFloat64Scalar2D.E0;

        return Scalar12.IsPositive()
            ? LinFloat64Scalar2D.E0
            : LinFloat64Scalar2D.NegativeE0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar2D DirectionToNormal2D()
    {
        if (Scalar12.IsZero())
            return LinFloat64Scalar2D.E0;

        return LinFloat64Scalar2D.Create(1d / Scalar12.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar2D DirectionToNormal2D(double scalingFactor)
    {
        if (Scalar12.IsZero())
            return LinFloat64Scalar2D.E0;

        return LinFloat64Scalar2D.Create(scalingFactor / Scalar12.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar2D NormalToUnitDirection2D()
    {
        if (Scalar12.IsZero())
            return LinFloat64Scalar2D.E0;

        return Scalar12.IsPositive()
            ? LinFloat64Scalar2D.E0
            : LinFloat64Scalar2D.NegativeE0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar2D NormalToDirection2D()
    {
        if (Scalar12.IsZero())
            return LinFloat64Scalar2D.E0;

        return LinFloat64Scalar2D.Create(1d / Scalar12.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar2D NormalToDirection2D(double scalingFactor)
    {
        if (Scalar12.IsZero())
            return LinFloat64Scalar2D.E0;

        return LinFloat64Scalar2D.Create(scalingFactor / Scalar12.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar2D Dual2D()
    {
        return LinFloat64Scalar2D.Create(Scalar12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar2D Dual2D(double scalingFactor)
    {
        return LinFloat64Scalar2D.Create(
            Scalar12 * scalingFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar2D UnDual2D()
    {
        return LinFloat64Scalar2D.Create(-Scalar12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar2D UnDual2D(double scalingFactor)
    {
        return LinFloat64Scalar2D.Create(
            -Scalar12 * scalingFactor
        );
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
        return $"({Scalar12:G10})<1,2>";
    }

}