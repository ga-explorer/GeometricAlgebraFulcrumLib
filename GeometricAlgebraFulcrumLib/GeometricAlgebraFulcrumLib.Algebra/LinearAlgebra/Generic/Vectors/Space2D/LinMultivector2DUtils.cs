using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;

public static class LinMultivector2DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinScalar2D<T> mv1, LinScalar2D<T> mv2)
    {
        return mv1.Scalar * mv2.Scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinScalar2D<T> mv1, LinVector2D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinScalar2D<T> mv1, LinBivector2D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinScalar2D<T> mv1, LinMultivector2D<T> mv2)
    {
        var mv = mv1.ScalarProcessor.Zero;

        if (!mv1.IsZero() && !mv2.KVector0.IsZero())
            mv += mv1.Sp(mv2.KVector0);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinVector2D<T> mv1, LinScalar2D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinVector2D<T> mv1, LinVector2D<T> mv2)
    {
        return mv1.Scalar1 * mv2.Scalar1 +
               mv1.Scalar2 * mv2.Scalar2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinVector2D<T> mv1, LinBivector2D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinVector2D<T> mv1, LinMultivector2D<T> mv2)
    {
        var mv = mv1.ScalarProcessor.Zero;

        if (!mv1.IsZero() && !mv2.KVector1.IsZero())
            mv += mv1.Sp(mv2.KVector1);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinBivector2D<T> mv1, LinScalar2D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinBivector2D<T> mv1, LinVector2D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinBivector2D<T> mv1, LinBivector2D<T> mv2)
    {
        return -(mv1.Scalar12 * mv2.Scalar12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinBivector2D<T> mv1, LinMultivector2D<T> mv2)
    {
        var mv = mv1.ScalarProcessor.Zero;

        if (!mv1.IsZero() && !mv2.KVector2.IsZero())
            mv += mv1.Sp(mv2.KVector2);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinMultivector2D<T> mv1, LinScalar2D<T> mv2)
    {
        var mv = mv1.ScalarProcessor.Zero;

        if (!mv1.KVector0.IsZero() && !mv2.IsZero())
            mv += mv1.KVector0.Sp(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinMultivector2D<T> mv1, LinVector2D<T> mv2)
    {
        var mv = mv1.ScalarProcessor.Zero;

        if (!mv1.KVector1.IsZero() && !mv2.IsZero())
            mv += mv1.KVector1.Sp(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinMultivector2D<T> mv1, LinBivector2D<T> mv2)
    {
        var mv = mv1.ScalarProcessor.Zero;

        if (!mv1.KVector2.IsZero() && !mv2.IsZero())
            mv += mv1.KVector2.Sp(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinMultivector2D<T> mv1, LinMultivector2D<T> mv2)
    {
        var mv = mv1.ScalarProcessor.Zero;

        if (!mv1.KVector0.IsZero() && !mv2.KVector0.IsZero())
            mv += mv1.KVector0.Sp(mv2.KVector0);

        if (!mv1.KVector1.IsZero() && !mv2.KVector1.IsZero())
            mv += mv1.KVector1.Sp(mv2.KVector1);

        if (!mv1.KVector2.IsZero() && !mv2.KVector2.IsZero())
            mv += mv1.KVector2.Sp(mv2.KVector2);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> Op<T>(this LinScalar2D<T> mv1, LinScalar2D<T> mv2)
    {
        return LinScalar2D<T>.Create(
            mv1.Scalar * mv2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Op<T>(this LinVector2D<T> mv1, LinScalar2D<T> mv2)
    {
        return LinVector2D<T>.Create(
            mv1.Scalar1 * mv2.Scalar,
            mv1.Scalar2 * mv2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Op<T>(this LinScalar2D<T> mv1, LinVector2D<T> mv2)
    {
        return LinVector2D<T>.Create(
            mv1.Scalar * mv2.Scalar1,
            mv1.Scalar * mv2.Scalar2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> VectorOp<T>(this IPair<Scalar<T>> mv1, IPair<Scalar<T>> mv2)
    {
        return LinBivector2D<T>.Create(
            mv1.Item1 * mv2.Item2 - mv1.Item2 * mv2.Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> Op<T>(this LinVector2D<T> mv1, LinVector2D<T> mv2)
    {
        return LinBivector2D<T>.Create(
            mv1.Scalar1 * mv2.Scalar2 - mv1.Scalar2 * mv2.Scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> Op<T>(this LinScalar2D<T> mv1, LinBivector2D<T> mv2)
    {
        return LinBivector2D<T>.Create(
            mv1.Scalar * mv2.Scalar12
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> Op<T>(this LinBivector2D<T> mv1, LinScalar2D<T> mv2)
    {
        return LinBivector2D<T>.Create(
            mv1.Scalar12 * mv2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> Op<T>(this LinBivector2D<T> mv1, LinBivector2D<T> mv2)
    {
        return LinScalar2D<T>.Create(mv1.ScalarProcessor.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> Op<T>(this LinMultivector2D<T> mv1, LinScalar2D<T> mv2)
    {
        var mv = LinMultivector2D<T>.Zero(mv1.ScalarProcessor);

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);

        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);

        if (!mv1.KVector2.IsZero())
            mv += mv1.KVector2.Op(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> Op<T>(this LinScalar2D<T> mv1, LinMultivector2D<T> mv2)
    {
        var mv = LinMultivector2D<T>.Zero(mv1.ScalarProcessor);

        if (!mv2.KVector0.IsZero())
            mv += mv1.Op(mv2.KVector0);

        if (!mv2.KVector1.IsZero())
            mv += mv1.Op(mv2.KVector1);

        if (!mv2.KVector2.IsZero())
            mv += mv1.Op(mv2.KVector2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> Op<T>(this LinVector2D<T> mv1, LinMultivector2D<T> mv2)
    {
        var mv = LinMultivector2D<T>.Zero(mv1.ScalarProcessor);

        if (!mv2.KVector0.IsZero())
            mv += mv1.Op(mv2.KVector0);

        if (!mv2.KVector1.IsZero())
            mv += mv1.Op(mv2.KVector1);

        if (!mv2.KVector2.IsZero())
            mv += mv1.Op(mv2.KVector2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> Op<T>(this LinMultivector2D<T> mv1, LinVector2D<T> mv2)
    {
        var mv = LinMultivector2D<T>.Zero(mv1.ScalarProcessor);

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);

        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);

        if (!mv1.KVector2.IsZero())
            mv += mv1.KVector2.Op(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> Op<T>(this LinBivector2D<T> mv1, LinMultivector2D<T> mv2)
    {
        var mv = LinMultivector2D<T>.Zero(mv1.ScalarProcessor);

        if (!mv2.KVector0.IsZero())
            mv += mv1.Op(mv2.KVector0);

        if (!mv2.KVector1.IsZero())
            mv += mv1.Op(mv2.KVector1);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> Op<T>(this LinMultivector2D<T> mv1, LinBivector2D<T> mv2)
    {
        var mv = LinMultivector2D<T>.Zero(mv1.ScalarProcessor);

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);

        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> Op<T>(this LinMultivector2D<T> mv1, LinMultivector2D<T> mv2)
    {
        var mv = LinMultivector2D<T>.Zero(mv1.ScalarProcessor);

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);

        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);

        if (!mv1.KVector2.IsZero())
            mv += mv1.KVector2.Op(mv2);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Lcp<T>(this LinVector2D<T> v1, LinBivector2D<T> b2)
    {
        var s1 =
            -v1.Scalar2 * b2.Scalar12;

        var s2 =
            v1.Scalar1 * b2.Scalar12;

        return LinVector2D<T>.Create(s1, s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Rcp<T>(this LinBivector2D<T> b1, LinVector2D<T> v2)
    {
        var s1 =
            b1.Scalar12 * v2.Scalar2;

        var s2 =
            -b1.Scalar12 * v2.Scalar1;

        return LinVector2D<T>.Create(s1, s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ProjectOn<T>(this LinVector2D<T> mv1, LinBivector2D<T> mv2)
    {
        return mv1.Lcp(mv2).Lcp(mv2.Inverse());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Gp<T>(this LinVector2D<T> mv1, LinBivector2D<T> mv2)
    {
        var s1 =
            -mv1.Scalar2 * mv2.Scalar12;

        var s2 =
            mv1.Scalar1 * mv2.Scalar12;

        return LinVector2D<T>.Create(s1, s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Gp<T>(this LinBivector2D<T> mv1, LinVector2D<T> mv2)
    {
        var s1 =
            mv1.Scalar12 * mv2.Scalar2;

        var s2 =
            -mv1.Scalar12 * mv2.Scalar1;

        return LinVector2D<T>.Create(s1, s2);
    }
}