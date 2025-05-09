using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

public static class LinFloat64Multivector3DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Scalar3D mv1, LinFloat64Scalar3D mv2)
    {
        return mv1.Scalar * mv2.Scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Scalar3D mv1, LinFloat64Vector3D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Scalar3D mv1, LinFloat64Bivector3D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Scalar3D mv1, LinFloat64Trivector3D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Scalar3D mv1, LinFloat64Multivector3D mv2)
    {
        var mv = 0d;

        if (!mv1.IsZero() && !mv2.KVector0.IsZero())
            mv += mv1.Sp(mv2.KVector0);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Vector3D mv1, LinFloat64Scalar3D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Vector3D mv1, LinFloat64Vector3D mv2)
    {
        return mv1.Scalar1 * mv2.Scalar1 +
               mv1.Scalar2 * mv2.Scalar2 +
               mv1.Scalar3 * mv2.Scalar3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Vector3D mv1, LinFloat64Bivector3D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Vector3D mv1, LinFloat64Trivector3D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Vector3D mv1, LinFloat64Multivector3D mv2)
    {
        var mv = 0d;

        if (!mv1.IsZero() && !mv2.KVector1.IsZero())
            mv += mv1.Sp(mv2.KVector1);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Bivector3D mv1, LinFloat64Scalar3D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Bivector3D mv1, LinFloat64Vector3D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Bivector3D mv1, LinFloat64Bivector3D mv2)
    {
        return -(mv1.Scalar12 * mv2.Scalar12 +
                 mv1.Scalar13 * mv2.Scalar13 +
                 mv1.Scalar23 * mv2.Scalar23);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Bivector3D mv1, LinFloat64Trivector3D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Bivector3D mv1, LinFloat64Multivector3D mv2)
    {
        var mv = 0d;

        if (!mv1.IsZero() && !mv2.KVector2.IsZero())
            mv += mv1.Sp(mv2.KVector2);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Trivector3D mv1, LinFloat64Scalar3D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Trivector3D mv1, LinFloat64Vector3D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Trivector3D mv1, LinFloat64Bivector3D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Trivector3D mv1, LinFloat64Trivector3D mv2)
    {
        return -(mv1.Scalar123 * mv2.Scalar123);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Trivector3D mv1, LinFloat64Multivector3D mv2)
    {
        var mv = 0d;

        if (!mv1.IsZero() && !mv2.KVector3.IsZero())
            mv += mv1.Sp(mv2.KVector3);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Multivector3D mv1, LinFloat64Scalar3D mv2)
    {
        var mv = 0d;

        if (!mv1.KVector0.IsZero() && !mv2.IsZero())
            mv += mv1.KVector0.Sp(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Multivector3D mv1, LinFloat64Vector3D mv2)
    {
        var mv = 0d;

        if (!mv1.KVector1.IsZero() && !mv2.IsZero())
            mv += mv1.KVector1.Sp(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Multivector3D mv1, LinFloat64Bivector3D mv2)
    {
        var mv = 0d;

        if (!mv1.KVector2.IsZero() && !mv2.IsZero())
            mv += mv1.KVector2.Sp(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Multivector3D mv1, LinFloat64Trivector3D mv2)
    {
        var mv = 0d;

        if (!mv1.KVector3.IsZero() && !mv2.IsZero())
            mv += mv1.KVector3.Sp(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this LinFloat64Multivector3D mv1, LinFloat64Multivector3D mv2)
    {
        var mv = 0d;

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
    public static LinFloat64Scalar3D Op(this LinFloat64Scalar3D mv1, LinFloat64Scalar3D mv2)
    {
        return LinFloat64Scalar3D.Create(
            mv1.Scalar * mv2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D Op(this LinFloat64Vector3D mv1, LinFloat64Scalar3D mv2)
    {
        return LinFloat64Vector3D.Create(
            mv1.Scalar1 * mv2.Scalar,
            mv1.Scalar2 * mv2.Scalar,
            mv1.Scalar3 * mv2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D Op(this LinFloat64Scalar3D mv1, LinFloat64Vector3D mv2)
    {
        return LinFloat64Vector3D.Create(
            mv1.Scalar * mv2.Scalar1,
            mv1.Scalar * mv2.Scalar2,
            mv1.Scalar * mv2.Scalar3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D VectorOp(this ITriplet<Float64Scalar> mv1, ITriplet<Float64Scalar> mv2)
    {
        return LinFloat64Bivector3D.Create(
            mv1.Item1 * mv2.Item2 - mv1.Item2 * mv2.Item1,
            mv1.Item1 * mv2.Item3 - mv1.Item3 * mv2.Item1,
            mv1.Item2 * mv2.Item3 - mv1.Item3 * mv2.Item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D Op(this LinFloat64Vector3D mv1, LinFloat64Vector3D mv2)
    {
        return LinFloat64Bivector3D.Create(
            mv1.Scalar1 * mv2.Scalar2 - mv1.Scalar2 * mv2.Scalar1,
            mv1.Scalar1 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar1,
            mv1.Scalar2 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D Op(this LinFloat64Scalar3D mv1, LinFloat64Bivector3D mv2)
    {
        return LinFloat64Bivector3D.Create(
            mv1.Scalar * mv2.Scalar12,
            mv1.Scalar * mv2.Scalar13,
            mv1.Scalar * mv2.Scalar23
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Bivector3D Op(this LinFloat64Bivector3D mv1, LinFloat64Scalar3D mv2)
    {
        return LinFloat64Bivector3D.Create(
            mv1.Scalar12 * mv2.Scalar,
            mv1.Scalar13 * mv2.Scalar,
            mv1.Scalar23 * mv2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3D Op(this LinFloat64Vector3D mv1, LinFloat64Bivector3D mv2)
    {
        return LinFloat64Trivector3D.Create(
            mv1.Scalar1 * mv2.Scalar23 -
            mv1.Scalar2 * mv2.Scalar13 +
            mv1.Scalar3 * mv2.Scalar12
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3D Op(this LinFloat64Bivector3D mv1, LinFloat64Vector3D mv2)
    {
        return LinFloat64Trivector3D.Create(
            mv1.Scalar12 * mv2.Scalar3 -
            mv1.Scalar13 * mv2.Scalar2 +
            mv1.Scalar23 * mv2.Scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D Op(this LinFloat64Bivector3D mv1, LinFloat64Bivector3D mv2)
    {
        return LinFloat64Scalar3D.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3D Op(this LinFloat64Scalar3D mv1, LinFloat64Trivector3D mv2)
    {
        return LinFloat64Trivector3D.Create(
            mv1.Scalar * mv2.Scalar123
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3D Op(this LinFloat64Trivector3D mv1, LinFloat64Scalar3D mv2)
    {
        return LinFloat64Trivector3D.Create(
            mv1.Scalar123 * mv2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D Op(this LinFloat64Vector3D mv1, LinFloat64Trivector3D mv2)
    {
        return LinFloat64Scalar3D.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D Op(this LinFloat64Trivector3D mv1, LinFloat64Vector3D mv2)
    {
        return LinFloat64Scalar3D.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D Op(this LinFloat64Bivector3D mv1, LinFloat64Trivector3D mv2)
    {
        return LinFloat64Scalar3D.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D Op(this LinFloat64Trivector3D mv1, LinFloat64Bivector3D mv2)
    {
        return LinFloat64Scalar3D.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Scalar3D Op(this LinFloat64Trivector3D mv1, LinFloat64Trivector3D mv2)
    {
        return LinFloat64Scalar3D.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Multivector3D Op(this LinFloat64Multivector3D mv1, LinFloat64Scalar3D mv2)
    {
        var mv = LinFloat64Multivector3D.Zero;

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
    public static LinFloat64Multivector3D Op(this LinFloat64Scalar3D mv1, LinFloat64Multivector3D mv2)
    {
        var mv = LinFloat64Multivector3D.Zero;

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
    public static LinFloat64Multivector3D Op(this LinFloat64Vector3D mv1, LinFloat64Multivector3D mv2)
    {
        var mv = LinFloat64Multivector3D.Zero;

        if (!mv2.KVector0.IsZero())
            mv += mv1.Op(mv2.KVector0);

        if (!mv2.KVector1.IsZero())
            mv += mv1.Op(mv2.KVector1);

        if (!mv2.KVector2.IsZero())
            mv += mv1.Op(mv2.KVector2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Multivector3D Op(this LinFloat64Multivector3D mv1, LinFloat64Vector3D mv2)
    {
        var mv = LinFloat64Multivector3D.Zero;

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);

        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);

        if (!mv1.KVector2.IsZero())
            mv += mv1.KVector2.Op(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Multivector3D Op(this LinFloat64Bivector3D mv1, LinFloat64Multivector3D mv2)
    {
        var mv = LinFloat64Multivector3D.Zero;

        if (!mv2.KVector0.IsZero())
            mv += mv1.Op(mv2.KVector0);

        if (!mv2.KVector1.IsZero())
            mv += mv1.Op(mv2.KVector1);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Multivector3D Op(this LinFloat64Multivector3D mv1, LinFloat64Bivector3D mv2)
    {
        var mv = LinFloat64Multivector3D.Zero;

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);

        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Multivector3D Op(this LinFloat64Trivector3D mv1, LinFloat64Multivector3D mv2)
    {
        var mv = LinFloat64Multivector3D.Zero;

        if (!mv2.KVector0.IsZero())
            mv += mv1.Op(mv2.KVector0);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Multivector3D Op(this LinFloat64Multivector3D mv1, LinFloat64Trivector3D mv2)
    {
        var mv = LinFloat64Multivector3D.Zero;

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Multivector3D Op(this LinFloat64Multivector3D mv1, LinFloat64Multivector3D mv2)
    {
        var mv = LinFloat64Multivector3D.Zero;

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
    public static LinFloat64Vector3D Lcp(this LinFloat64Vector3D mv1, LinFloat64Bivector3D mv2)
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

        return LinFloat64Vector3D.Create(s1, s2, s3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D Rcp(this LinFloat64Bivector3D b1, LinFloat64Vector3D v2)
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

        return LinFloat64Vector3D.Create(s1, s2, s3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ProjectOnBivector(this LinFloat64Vector3D mv1, LinFloat64Bivector3D mv2)
    {
        return mv1.Lcp(mv2).Lcp(mv2.Inverse());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Multivector3D Gp(this LinFloat64Vector3D mv1, LinFloat64Bivector3D mv2)
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

        return LinFloat64Multivector3D.Create(
            LinFloat64Scalar3D.Zero,
            LinFloat64Vector3D.Create(s1, s2, s3),
            LinFloat64Bivector3D.Zero,
            LinFloat64Trivector3D.Create(s123)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Multivector3D Gp(this LinFloat64Bivector3D mv1, LinFloat64Vector3D mv2)
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

        return LinFloat64Multivector3D.Create(
            LinFloat64Scalar3D.Zero,
            LinFloat64Vector3D.Create(s1, s2, s3),
            LinFloat64Bivector3D.Zero,
            LinFloat64Trivector3D.Create(s123)
        );
    }
}