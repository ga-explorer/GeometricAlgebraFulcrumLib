using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

public static class LinMultivector3DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinScalar3D<T> mv1, LinScalar3D<T> mv2)
    {
        return mv1.Scalar * mv2.Scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinScalar3D<T> mv1, LinVector3D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinScalar3D<T> mv1, LinBivector3D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinScalar3D<T> mv1, LinTrivector3D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinScalar3D<T> mv1, LinMultivector3D<T> mv2)
    {
        var mv = mv1.ScalarProcessor.Zero;

        if (!mv1.IsZero() && !mv2.KVector0.IsZero())
            mv += mv1.Sp(mv2.KVector0);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinVector3D<T> mv1, LinScalar3D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinVector3D<T> mv1, LinVector3D<T> mv2)
    {
        return mv1.Scalar1 * mv2.Scalar1 +
               mv1.Scalar2 * mv2.Scalar2 +
               mv1.Scalar3 * mv2.Scalar3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinVector3D<T> mv1, LinBivector3D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinVector3D<T> mv1, LinTrivector3D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinVector3D<T> mv1, LinMultivector3D<T> mv2)
    {
        var mv = mv1.ScalarProcessor.Zero;

        if (!mv1.IsZero() && !mv2.KVector1.IsZero())
            mv += mv1.Sp(mv2.KVector1);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinBivector3D<T> mv1, LinScalar3D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinBivector3D<T> mv1, LinVector3D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinBivector3D<T> mv1, LinBivector3D<T> mv2)
    {
        return -(mv1.Scalar12 * mv2.Scalar12 +
                 mv1.Scalar13 * mv2.Scalar13 +
                 mv1.Scalar23 * mv2.Scalar23);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinBivector3D<T> mv1, LinTrivector3D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinBivector3D<T> mv1, LinMultivector3D<T> mv2)
    {
        var mv = mv1.ScalarProcessor.Zero;

        if (!mv1.IsZero() && !mv2.KVector2.IsZero())
            mv += mv1.Sp(mv2.KVector2);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinTrivector3D<T> mv1, LinScalar3D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinTrivector3D<T> mv1, LinVector3D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinTrivector3D<T> mv1, LinBivector3D<T> mv2)
    {
        return mv1.ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinTrivector3D<T> mv1, LinTrivector3D<T> mv2)
    {
        return -(mv1.Scalar123 * mv2.Scalar123);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinTrivector3D<T> mv1, LinMultivector3D<T> mv2)
    {
        var mv = mv1.ScalarProcessor.Zero;

        if (!mv1.IsZero() && !mv2.KVector3.IsZero())
            mv += mv1.Sp(mv2.KVector3);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinMultivector3D<T> mv1, LinScalar3D<T> mv2)
    {
        var mv = mv1.ScalarProcessor.Zero;

        if (!mv1.KVector0.IsZero() && !mv2.IsZero())
            mv += mv1.KVector0.Sp(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinMultivector3D<T> mv1, LinVector3D<T> mv2)
    {
        var mv = mv1.ScalarProcessor.Zero;

        if (!mv1.KVector1.IsZero() && !mv2.IsZero())
            mv += mv1.KVector1.Sp(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinMultivector3D<T> mv1, LinBivector3D<T> mv2)
    {
        var mv = mv1.ScalarProcessor.Zero;

        if (!mv1.KVector2.IsZero() && !mv2.IsZero())
            mv += mv1.KVector2.Sp(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinMultivector3D<T> mv1, LinTrivector3D<T> mv2)
    {
        var mv = mv1.ScalarProcessor.Zero;

        if (!mv1.KVector3.IsZero() && !mv2.IsZero())
            mv += mv1.KVector3.Sp(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sp<T>(this LinMultivector3D<T> mv1, LinMultivector3D<T> mv2)
    {
        var mv = mv1.ScalarProcessor.Zero;

        if (!mv1.KVector0.IsZero() && !mv2.KVector0.IsZero())
            mv += mv1.KVector0.Sp(mv2.KVector0);

        if (!mv1.KVector1.IsZero() && !mv2.KVector1.IsZero())
            mv += mv1.KVector1.Sp(mv2.KVector1);

        if (!mv1.KVector2.IsZero() && !mv2.KVector2.IsZero())
            mv += mv1.KVector2.Sp(mv2.KVector2);

        if (!mv1.KVector3.IsZero() && !mv2.KVector3.IsZero())
            mv += mv1.KVector3.Sp(mv2.KVector3);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> Op<T>(this LinScalar3D<T> mv1, LinScalar3D<T> mv2)
    {
        return LinScalar3D<T>.Create(
            mv1.Scalar * mv2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> Op<T>(this LinVector3D<T> mv1, LinScalar3D<T> mv2)
    {
        return LinVector3D<T>.Create(
            mv1.Scalar1 * mv2.Scalar,
            mv1.Scalar2 * mv2.Scalar,
            mv1.Scalar3 * mv2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> Op<T>(this LinScalar3D<T> mv1, LinVector3D<T> mv2)
    {
        return LinVector3D<T>.Create(
            mv1.Scalar * mv2.Scalar1,
            mv1.Scalar * mv2.Scalar2,
            mv1.Scalar * mv2.Scalar3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> VectorOp<T>(this ITriplet<Scalar<T>> mv1, ITriplet<Scalar<T>> mv2)
    {
        return LinBivector3D<T>.Create(
            mv1.Item1 * mv2.Item2 - mv1.Item2 * mv2.Item1,
            mv1.Item1 * mv2.Item3 - mv1.Item3 * mv2.Item1,
            mv1.Item2 * mv2.Item3 - mv1.Item3 * mv2.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> Op<T>(this LinVector3D<T> mv1, LinVector3D<T> mv2)
    {
        return LinBivector3D<T>.Create(
            mv1.Scalar1 * mv2.Scalar2 - mv1.Scalar2 * mv2.Scalar1,
            mv1.Scalar1 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar1,
            mv1.Scalar2 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> Op<T>(this LinScalar3D<T> mv1, LinBivector3D<T> mv2)
    {
        return LinBivector3D<T>.Create(
            mv1.Scalar * mv2.Scalar12,
            mv1.Scalar * mv2.Scalar13,
            mv1.Scalar * mv2.Scalar23
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> Op<T>(this LinBivector3D<T> mv1, LinScalar3D<T> mv2)
    {
        return LinBivector3D<T>.Create(
            mv1.Scalar12 * mv2.Scalar,
            mv1.Scalar13 * mv2.Scalar,
            mv1.Scalar23 * mv2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> Op<T>(this LinVector3D<T> mv1, LinBivector3D<T> mv2)
    {
        return LinTrivector3D<T>.Create(
            mv1.Scalar1 * mv2.Scalar23 -
            mv1.Scalar2 * mv2.Scalar13 +
            mv1.Scalar3 * mv2.Scalar12
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> Op<T>(this LinBivector3D<T> mv1, LinVector3D<T> mv2)
    {
        return LinTrivector3D<T>.Create(
            mv1.Scalar12 * mv2.Scalar3 -
            mv1.Scalar13 * mv2.Scalar2 +
            mv1.Scalar23 * mv2.Scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> Op<T>(this LinBivector3D<T> mv1, LinBivector3D<T> mv2)
    {
        return LinScalar3D<T>.Zero(mv1.ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> Op<T>(this LinScalar3D<T> mv1, LinTrivector3D<T> mv2)
    {
        return LinTrivector3D<T>.Create(
            mv1.Scalar * mv2.Scalar123
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> Op<T>(this LinTrivector3D<T> mv1, LinScalar3D<T> mv2)
    {
        return LinTrivector3D<T>.Create(
            mv1.Scalar123 * mv2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> Op<T>(this LinVector3D<T> mv1, LinTrivector3D<T> mv2)
    {
        return LinScalar3D<T>.Zero(mv1.ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> Op<T>(this LinTrivector3D<T> mv1, LinVector3D<T> mv2)
    {
        return LinScalar3D<T>.Zero(mv1.ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> Op<T>(this LinBivector3D<T> mv1, LinTrivector3D<T> mv2)
    {
        return LinScalar3D<T>.Zero(mv1.ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> Op<T>(this LinTrivector3D<T> mv1, LinBivector3D<T> mv2)
    {
        return LinScalar3D<T>.Zero(mv1.ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> Op<T>(this LinTrivector3D<T> mv1, LinTrivector3D<T> mv2)
    {
        return LinScalar3D<T>.Zero(mv1.ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> Op<T>(this LinMultivector3D<T> mv1, LinScalar3D<T> mv2)
    {
        var mv = LinMultivector3D<T>.Zero(mv1.ScalarProcessor);

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);

        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);

        if (!mv1.KVector2.IsZero())
            mv += mv1.KVector2.Op(mv2);

        if (!mv1.KVector3.IsZero())
            mv += mv1.KVector3.Op(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> Op<T>(this LinScalar3D<T> mv1, LinMultivector3D<T> mv2)
    {
        var mv = LinMultivector3D<T>.Zero(mv1.ScalarProcessor);

        if (!mv2.KVector0.IsZero())
            mv += mv1.Op(mv2.KVector0);

        if (!mv2.KVector1.IsZero())
            mv += mv1.Op(mv2.KVector1);

        if (!mv2.KVector2.IsZero())
            mv += mv1.Op(mv2.KVector2);

        if (!mv2.KVector3.IsZero())
            mv += mv1.Op(mv2.KVector3);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> Op<T>(this LinVector3D<T> mv1, LinMultivector3D<T> mv2)
    {
        var mv = LinMultivector3D<T>.Zero(mv1.ScalarProcessor);

        if (!mv2.KVector0.IsZero())
            mv += mv1.Op(mv2.KVector0);

        if (!mv2.KVector1.IsZero())
            mv += mv1.Op(mv2.KVector1);

        if (!mv2.KVector2.IsZero())
            mv += mv1.Op(mv2.KVector2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> Op<T>(this LinMultivector3D<T> mv1, LinVector3D<T> mv2)
    {
        var mv = LinMultivector3D<T>.Zero(mv1.ScalarProcessor);

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);

        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);

        if (!mv1.KVector2.IsZero())
            mv += mv1.KVector2.Op(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> Op<T>(this LinBivector3D<T> mv1, LinMultivector3D<T> mv2)
    {
        var mv = LinMultivector3D<T>.Zero(mv1.ScalarProcessor);

        if (!mv2.KVector0.IsZero())
            mv += mv1.Op(mv2.KVector0);

        if (!mv2.KVector1.IsZero())
            mv += mv1.Op(mv2.KVector1);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> Op<T>(this LinMultivector3D<T> mv1, LinBivector3D<T> mv2)
    {
        var mv = LinMultivector3D<T>.Zero(mv1.ScalarProcessor);

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);

        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> Op<T>(this LinTrivector3D<T> mv1, LinMultivector3D<T> mv2)
    {
        var mv = LinMultivector3D<T>.Zero(mv1.ScalarProcessor);

        if (!mv2.KVector0.IsZero())
            mv += mv1.Op(mv2.KVector0);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> Op<T>(this LinMultivector3D<T> mv1, LinTrivector3D<T> mv2)
    {
        var mv = LinMultivector3D<T>.Zero(mv1.ScalarProcessor);

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> Op<T>(this LinMultivector3D<T> mv1, LinMultivector3D<T> mv2)
    {
        var mv = LinMultivector3D<T>.Zero(mv1.ScalarProcessor);

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);

        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);

        if (!mv1.KVector2.IsZero())
            mv += mv1.KVector2.Op(mv2);

        if (!mv1.KVector3.IsZero())
            mv += mv1.KVector3.Op(mv2);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> Lcp<T>(this LinVector3D<T> mv1, LinBivector3D<T> mv2)
    {
        var s1 =
            -mv1.Scalar2 * mv2.Scalar12 -
            mv1.Scalar3 * mv2.Scalar13;

        var s2 =
            mv1.Scalar1 * mv2.Scalar12 -
            mv1.Scalar3 * mv2.Scalar23;

        var s3 =
            mv1.Scalar1 * mv2.Scalar13 +
            mv1.Scalar2 * mv2.Scalar23;

        return LinVector3D<T>.Create(s1, s2, s3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> Rcp<T>(this LinBivector3D<T> b1, LinVector3D<T> v2)
    {
        var s1 =
            b1.Scalar12 * v2.Scalar2 +
            b1.Scalar13 * v2.Scalar3;

        var s2 =
            -b1.Scalar12 * v2.Scalar1 +
            b1.Scalar23 * v2.Scalar3;

        var s3 =
            -b1.Scalar13 * v2.Scalar1 -
            b1.Scalar23 * v2.Scalar2;

        return LinVector3D<T>.Create(s1, s2, s3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ProjectOnBivector<T>(this LinVector3D<T> mv1, LinBivector3D<T> mv2)
    {
        return mv1.Lcp(mv2).Lcp(mv2.Inverse());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> Gp<T>(this LinVector3D<T> mv1, LinBivector3D<T> mv2)
    {
        var s1 =
            -mv1.Scalar2 * mv2.Scalar12 -
            mv1.Scalar3 * mv2.Scalar13;

        var s2 =
            mv1.Scalar1 * mv2.Scalar12 -
            mv1.Scalar3 * mv2.Scalar23;

        var s3 =
            mv1.Scalar1 * mv2.Scalar13 +
            mv1.Scalar2 * mv2.Scalar23;

        var s123 =
            mv1.Scalar1 * mv2.Scalar23 -
            mv1.Scalar2 * mv2.Scalar13 +
            mv1.Scalar3 * mv2.Scalar12;

        return LinMultivector3D<T>.Create(
            LinScalar3D<T>.Zero(mv1.ScalarProcessor),
            LinVector3D<T>.Create(s1, s2, s3),
            LinBivector3D<T>.Zero(mv1.ScalarProcessor),
            LinTrivector3D<T>.Create(s123)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> Gp<T>(this LinBivector3D<T> mv1, LinVector3D<T> mv2)
    {
        var s1 =
            mv1.Scalar12 * mv2.Scalar2 +
            mv1.Scalar13 * mv2.Scalar3;

        var s2 =
            -mv1.Scalar12 * mv2.Scalar1 +
            mv1.Scalar23 * mv2.Scalar3;

        var s3 =
            -mv1.Scalar13 * mv2.Scalar1 -
            mv1.Scalar23 * mv2.Scalar2;

        var s123 =
            mv1.Scalar12 * mv2.Scalar3 -
            mv1.Scalar13 * mv2.Scalar2 +
            mv1.Scalar23 * mv2.Scalar1;

        return LinMultivector3D<T>.Create(
            LinScalar3D<T>.Zero(mv1.ScalarProcessor),
            LinVector3D<T>.Create(s1, s2, s3),
            LinBivector3D<T>.Zero(mv1.ScalarProcessor),
            LinTrivector3D<T>.Create(s123)
        );
    }
}