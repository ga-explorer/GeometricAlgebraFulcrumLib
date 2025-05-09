using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

public static class LinFloat64Multivector2DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Scalar2D mv1, LinFloat64Scalar2D mv2)
    {
        return mv1.Scalar * mv2.Scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Scalar2D mv1, LinFloat64Vector2D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Scalar2D mv1, LinFloat64Bivector2D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Scalar2D mv1, LinFloat64Multivector2D mv2)
    {
        var mv = 0d;

        if (!mv1.IsZero() && !mv2.KVector0.IsZero())
            mv += mv1.Sp(mv2.KVector0);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Vector2D mv1, LinFloat64Scalar2D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Vector2D mv1, LinFloat64Vector2D mv2)
    {
        return mv1.Scalar1 * mv2.Scalar1 +
               mv1.Scalar2 * mv2.Scalar2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Vector2D mv1, LinFloat64Bivector2D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Vector2D mv1, LinFloat64Multivector2D mv2)
    {
        var mv = 0d;

        if (!mv1.IsZero() && !mv2.KVector1.IsZero())
            mv += mv1.Sp(mv2.KVector1);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Bivector2D mv1, LinFloat64Scalar2D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Bivector2D mv1, LinFloat64Vector2D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Bivector2D mv1, LinFloat64Bivector2D mv2)
    {
        return -(mv1.Scalar12 * mv2.Scalar12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Bivector2D mv1, LinFloat64Multivector2D mv2)
    {
        var mv = 0d;

        if (!mv1.IsZero() && !mv2.KVector2.IsZero())
            mv += mv1.Sp(mv2.KVector2);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Multivector2D mv1, LinFloat64Scalar2D mv2)
    {
        var mv = 0d;

        if (!mv1.KVector0.IsZero() && !mv2.IsZero())
            mv += mv1.KVector0.Sp(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Multivector2D mv1, LinFloat64Vector2D mv2)
    {
        var mv = 0d;

        if (!mv1.KVector1.IsZero() && !mv2.IsZero())
            mv += mv1.KVector1.Sp(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Multivector2D mv1, LinFloat64Bivector2D mv2)
    {
        var mv = 0d;

        if (!mv1.KVector2.IsZero() && !mv2.IsZero())
            mv += mv1.KVector2.Sp(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Multivector2D mv1, LinFloat64Multivector2D mv2)
    {
        var mv = 0d;

        if (!mv1.KVector0.IsZero() && !mv2.KVector0.IsZero())
            mv += mv1.KVector0.Sp(mv2.KVector0);

        if (!mv1.KVector1.IsZero() && !mv2.KVector1.IsZero())
            mv += mv1.KVector1.Sp(mv2.KVector1);

        if (!mv1.KVector2.IsZero() && !mv2.KVector2.IsZero())
            mv += mv1.KVector2.Sp(mv2.KVector2);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar2D Op(this LinFloat64Scalar2D mv1, LinFloat64Scalar2D mv2)
    {
        return LinFloat64Scalar2D.Create(
            mv1.Scalar * mv2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Op(this LinFloat64Vector2D mv1, LinFloat64Scalar2D mv2)
    {
        return LinFloat64Vector2D.Create(
            mv1.Scalar1 * mv2.Scalar,
            mv1.Scalar2 * mv2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Op(this LinFloat64Scalar2D mv1, LinFloat64Vector2D mv2)
    {
        return LinFloat64Vector2D.Create(
            mv1.Scalar * mv2.Scalar1,
            mv1.Scalar * mv2.Scalar2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector2D VectorOp(this IPair<double> mv1, IPair<double> mv2)
    {
        return LinFloat64Bivector2D.Create(
            mv1.Item1 * mv2.Item2 - mv1.Item2 * mv2.Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector2D Op(this LinFloat64Vector2D mv1, LinFloat64Vector2D mv2)
    {
        return LinFloat64Bivector2D.Create(
            mv1.Scalar1 * mv2.Scalar2 - mv1.Scalar2 * mv2.Scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector2D Op(this LinFloat64Scalar2D mv1, LinFloat64Bivector2D mv2)
    {
        return LinFloat64Bivector2D.Create(
            mv1.Scalar * mv2.Scalar12
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector2D Op(this LinFloat64Bivector2D mv1, LinFloat64Scalar2D mv2)
    {
        return LinFloat64Bivector2D.Create(
            mv1.Scalar12 * mv2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar2D Op(this LinFloat64Bivector2D mv1, LinFloat64Bivector2D mv2)
    {
        return LinFloat64Scalar2D.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Multivector2D Op(this LinFloat64Multivector2D mv1, LinFloat64Scalar2D mv2)
    {
        var mv = LinFloat64Multivector2D.Zero;

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);

        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);

        if (!mv1.KVector2.IsZero())
            mv += mv1.KVector2.Op(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Multivector2D Op(this LinFloat64Scalar2D mv1, LinFloat64Multivector2D mv2)
    {
        var mv = LinFloat64Multivector2D.Zero;

        if (!mv2.KVector0.IsZero())
            mv += mv1.Op(mv2.KVector0);

        if (!mv2.KVector1.IsZero())
            mv += mv1.Op(mv2.KVector1);

        if (!mv2.KVector2.IsZero())
            mv += mv1.Op(mv2.KVector2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Multivector2D Op(this LinFloat64Vector2D mv1, LinFloat64Multivector2D mv2)
    {
        var mv = LinFloat64Multivector2D.Zero;

        if (!mv2.KVector0.IsZero())
            mv += mv1.Op(mv2.KVector0);

        if (!mv2.KVector1.IsZero())
            mv += mv1.Op(mv2.KVector1);

        if (!mv2.KVector2.IsZero())
            mv += mv1.Op(mv2.KVector2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Multivector2D Op(this LinFloat64Multivector2D mv1, LinFloat64Vector2D mv2)
    {
        var mv = LinFloat64Multivector2D.Zero;

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);

        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);

        if (!mv1.KVector2.IsZero())
            mv += mv1.KVector2.Op(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Multivector2D Op(this LinFloat64Bivector2D mv1, LinFloat64Multivector2D mv2)
    {
        var mv = LinFloat64Multivector2D.Zero;

        if (!mv2.KVector0.IsZero())
            mv += mv1.Op(mv2.KVector0);

        if (!mv2.KVector1.IsZero())
            mv += mv1.Op(mv2.KVector1);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Multivector2D Op(this LinFloat64Multivector2D mv1, LinFloat64Bivector2D mv2)
    {
        var mv = LinFloat64Multivector2D.Zero;

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);

        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Multivector2D Op(this LinFloat64Multivector2D mv1, LinFloat64Multivector2D mv2)
    {
        var mv = LinFloat64Multivector2D.Zero;

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);

        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);

        if (!mv1.KVector2.IsZero())
            mv += mv1.KVector2.Op(mv2);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Lcp(this LinFloat64Vector2D v1, LinFloat64Bivector2D b2)
    {
        var s1 =
            -v1.Scalar2 * b2.Scalar12;

        var s2 =
            v1.Scalar1 * b2.Scalar12;

        return LinFloat64Vector2D.Create(s1, s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Rcp(this LinFloat64Bivector2D b1, LinFloat64Vector2D v2)
    {
        var s1 =
            b1.Scalar12 * v2.Scalar2;

        var s2 =
            -b1.Scalar12 * v2.Scalar1;

        return LinFloat64Vector2D.Create(s1, s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ProjectOn(this LinFloat64Vector2D mv1, LinFloat64Bivector2D mv2)
    {
        return mv1.Lcp(mv2).Lcp(mv2.Inverse());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Gp(this LinFloat64Vector2D mv1, LinFloat64Bivector2D mv2)
    {
        var s1 =
            -mv1.Scalar2 * mv2.Scalar12;

        var s2 =
            mv1.Scalar1 * mv2.Scalar12;

        return LinFloat64Vector2D.Create(s1, s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Gp(this LinFloat64Bivector2D mv1, LinFloat64Vector2D mv2)
    {
        var s1 =
            mv1.Scalar12 * mv2.Scalar2;

        var s2 =
            -mv1.Scalar12 * mv2.Scalar1;

        return LinFloat64Vector2D.Create(s1, s2);
    }
}