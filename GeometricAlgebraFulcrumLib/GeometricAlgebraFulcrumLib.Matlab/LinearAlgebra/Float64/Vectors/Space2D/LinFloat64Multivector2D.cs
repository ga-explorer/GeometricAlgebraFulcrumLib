using System;
using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;

public sealed record LinFloat64Multivector2D :
    ILinFloat64Multivector2D
{
    public static LinFloat64Multivector2D Zero { get; }
        = new LinFloat64Multivector2D();

    public static LinFloat64Multivector2D E0 { get; }
        = new LinFloat64Multivector2D(LinFloat64Scalar2D.E0);

    public static LinFloat64Multivector2D E1 { get; }
        = new LinFloat64Multivector2D(LinFloat64Vector2D.E1);

    public static LinFloat64Multivector2D E2 { get; }
        = new LinFloat64Multivector2D(LinFloat64Vector2D.E2);

    public static LinFloat64Multivector2D E12 { get; }
        = new LinFloat64Multivector2D(LinFloat64Bivector2D.E12);

    public static LinFloat64Multivector2D E21 { get; }
        = new LinFloat64Multivector2D(LinFloat64Bivector2D.E21);

    public static LinFloat64Multivector2D NegativeE0 { get; }
        = new LinFloat64Multivector2D(LinFloat64Scalar2D.NegativeE0);

    public static LinFloat64Multivector2D NegativeE1 { get; }
        = new LinFloat64Multivector2D(LinFloat64Vector2D.NegativeE1);

    public static LinFloat64Multivector2D NegativeE2 { get; }
        = new LinFloat64Multivector2D(LinFloat64Vector2D.NegativeE2);


    
    public static LinFloat64Multivector2D Create(LinFloat64Scalar2D kVector)
    {
        return new LinFloat64Multivector2D(kVector);
    }

    
    public static LinFloat64Multivector2D Create(LinFloat64Vector2D kVector)
    {
        return new LinFloat64Multivector2D(kVector);
    }

    
    public static LinFloat64Multivector2D Create(LinFloat64Bivector2D kVector)
    {
        return new LinFloat64Multivector2D(kVector);
    }

    
    public static LinFloat64Multivector2D Create(LinFloat64Scalar2D kVector0, LinFloat64Vector2D kVector1, LinFloat64Bivector2D kVector2)
    {
        return new LinFloat64Multivector2D(
            kVector0,
            kVector1,
            kVector2
        );
    }


    
    public static implicit operator LinFloat64Multivector2D(LinFloat64Scalar2D mv)
    {
        return new LinFloat64Multivector2D(mv);
    }

    
    public static implicit operator LinFloat64Multivector2D(LinFloat64Vector2D mv)
    {
        return new LinFloat64Multivector2D(mv);
    }

    
    public static implicit operator LinFloat64Multivector2D(LinFloat64Bivector2D mv)
    {
        return new LinFloat64Multivector2D(mv);
    }


    
    public static LinFloat64Multivector2D operator +(LinFloat64Multivector2D mv)
    {
        return mv;
    }

    
    public static LinFloat64Multivector2D operator -(LinFloat64Multivector2D mv)
    {
        return new LinFloat64Multivector2D(
            -mv.KVector0,
            -mv.KVector1,
            -mv.KVector2
        );
    }


    
    public static LinFloat64Multivector2D operator +(LinFloat64Multivector2D mv1, LinFloat64Scalar2D mv2)
    {
        return new LinFloat64Multivector2D(
            mv1.KVector0 + mv2,
            mv1.KVector1,
            mv1.KVector2
        );
    }

    
    public static LinFloat64Multivector2D operator +(LinFloat64Multivector2D mv1, LinFloat64Vector2D mv2)
    {
        return new LinFloat64Multivector2D(
            mv1.KVector0,
            mv1.KVector1 + mv2,
            mv1.KVector2
        );
    }

    
    public static LinFloat64Multivector2D operator +(LinFloat64Multivector2D mv1, LinFloat64Bivector2D mv2)
    {
        return new LinFloat64Multivector2D(
            mv1.KVector0,
            mv1.KVector1,
            mv1.KVector2 + mv2
        );
    }

    
    public static LinFloat64Multivector2D operator +(LinFloat64Scalar2D mv1, LinFloat64Multivector2D mv2)
    {
        return new LinFloat64Multivector2D(
            mv1 + mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2
        );
    }

    
    public static LinFloat64Multivector2D operator +(LinFloat64Vector2D mv1, LinFloat64Multivector2D mv2)
    {
        return new LinFloat64Multivector2D(
            mv2.KVector0,
            mv1 + mv2.KVector1,
            mv2.KVector2
        );
    }

    
    public static LinFloat64Multivector2D operator +(LinFloat64Bivector2D mv1, LinFloat64Multivector2D mv2)
    {
        return new LinFloat64Multivector2D(
            mv2.KVector0,
            mv2.KVector1,
            mv1 + mv2.KVector2
        );
    }

    
    public static LinFloat64Multivector2D operator +(LinFloat64Multivector2D mv1, LinFloat64Multivector2D mv2)
    {
        return new LinFloat64Multivector2D(
            mv1.KVector0 + mv2.KVector0,
            mv1.KVector1 + mv2.KVector1,
            mv1.KVector2 + mv2.KVector2
        );
    }


    
    public static LinFloat64Multivector2D operator -(LinFloat64Multivector2D mv1, LinFloat64Scalar2D mv2)
    {
        return new LinFloat64Multivector2D(
            mv1.KVector0 - mv2,
            mv1.KVector1,
            mv1.KVector2
        );
    }

    
    public static LinFloat64Multivector2D operator -(LinFloat64Multivector2D mv1, LinFloat64Vector2D mv2)
    {
        return new LinFloat64Multivector2D(
            mv1.KVector0,
            mv1.KVector1 - mv2,
            mv1.KVector2
        );
    }

    
    public static LinFloat64Multivector2D operator -(LinFloat64Multivector2D mv1, LinFloat64Bivector2D mv2)
    {
        return new LinFloat64Multivector2D(
            mv1.KVector0,
            mv1.KVector1,
            mv1.KVector2 - mv2
        );
    }

    
    public static LinFloat64Multivector2D operator -(LinFloat64Scalar2D mv1, LinFloat64Multivector2D mv2)
    {
        return new LinFloat64Multivector2D(
            mv1 - mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2
        );
    }

    
    public static LinFloat64Multivector2D operator -(LinFloat64Vector2D mv1, LinFloat64Multivector2D mv2)
    {
        return new LinFloat64Multivector2D(
            mv2.KVector0,
            mv1 - mv2.KVector1,
            mv2.KVector2
        );
    }

    
    public static LinFloat64Multivector2D operator -(LinFloat64Bivector2D mv1, LinFloat64Multivector2D mv2)
    {
        return new LinFloat64Multivector2D(
            mv2.KVector0,
            mv2.KVector1,
            mv1 - mv2.KVector2
        );
    }

    
    public static LinFloat64Multivector2D operator -(LinFloat64Multivector2D mv1, LinFloat64Multivector2D mv2)
    {
        return new LinFloat64Multivector2D(
            mv1.KVector0 - mv2.KVector0,
            mv1.KVector1 - mv2.KVector1,
            mv1.KVector2 - mv2.KVector2
        );
    }


    public int VSpaceDimensions
        => 3;

    public LinFloat64Scalar2D KVector0 { get; }

    public LinFloat64Vector2D KVector1 { get; }

    public LinFloat64Bivector2D KVector2 { get; }

    public double Scalar
        => KVector0.Scalar;

    public double Scalar1
        => KVector1.Scalar1;

    public double Scalar2
        => KVector1.Scalar2;

    public double Scalar12
        => KVector2.Scalar12;

    public int Count
        => 4;

    public double this[int grade, int index]
    {
        get
        {
            return grade switch
            {
                0 => index == 0
                    ? KVector0.Scalar
                    : throw new IndexOutOfRangeException(nameof(index)),

                1 => index switch
                {
                    0 => KVector1.Scalar1,
                    1 => KVector1.Scalar2,
                    _ => throw new IndexOutOfRangeException(nameof(index))
                },

                2 => index switch
                {
                    0 => KVector2.Scalar12,
                    _ => throw new IndexOutOfRangeException(nameof(index))
                },

                _ => throw new IndexOutOfRangeException(nameof(index))
            };
        }
    }

    public double this[int id]
    {
        get
        {
            return id switch
            {
                0 => KVector0.Scalar,

                1 => KVector1.Scalar1,
                2 => KVector1.Scalar2,

                3 => KVector2.Scalar12,

                _ => throw new IndexOutOfRangeException(nameof(id))
            };
        }
    }


    
    private LinFloat64Multivector2D()
    {
        KVector0 = LinFloat64Scalar2D.Zero;
        KVector1 = LinFloat64Vector2D.Zero;
        KVector2 = LinFloat64Bivector2D.Zero;
    }

    
    private LinFloat64Multivector2D(LinFloat64Scalar2D kVector0)
    {
        KVector0 = kVector0;
        KVector1 = LinFloat64Vector2D.Zero;
        KVector2 = LinFloat64Bivector2D.Zero;
    }

    
    private LinFloat64Multivector2D(LinFloat64Vector2D kVector1)
    {
        KVector0 = LinFloat64Scalar2D.Zero;
        KVector1 = kVector1;
        KVector2 = LinFloat64Bivector2D.Zero;
    }

    
    private LinFloat64Multivector2D(LinFloat64Bivector2D kVector2)
    {
        KVector0 = LinFloat64Scalar2D.Zero;
        KVector1 = LinFloat64Vector2D.Zero;
        KVector2 = kVector2;
    }

    
    private LinFloat64Multivector2D(LinFloat64Scalar2D kVector0, LinFloat64Bivector2D kVector2)
    {
        KVector0 = kVector0;
        KVector1 = LinFloat64Vector2D.Zero;
        KVector2 = kVector2;
    }

    
    private LinFloat64Multivector2D(LinFloat64Scalar2D kVector0, LinFloat64Vector2D kVector1, LinFloat64Bivector2D kVector2)
    {
        KVector0 = kVector0;
        KVector1 = kVector1;
        KVector2 = kVector2;
    }


    
    public bool IsValid()
    {
        return KVector0.IsValid() &&
               KVector1.IsValid() &&
               KVector2.IsValid();
    }

    
    public bool IsZero()
    {
        return KVector0.IsZero() &&
               KVector1.IsZero() &&
               KVector2.IsZero();
    }

    
    public bool IsNearZero(double zeroEpsilon = 1e-12d)
    {
        return Norm().IsNearZero(zeroEpsilon);
    }

    
    public double NormSquared()
    {
        return KVector0.NormSquared() +
               KVector1.NormSquared() +
               KVector2.NormSquared();
    }

    
    public LinFloat64Multivector2D ToMultivector2D()
    {
        return this;
    }

    
    public double Norm()
    {
        return NormSquared().Sqrt();
    }

    
    public LinFloat64Multivector2D GradeInvolution()
    {
        return new LinFloat64Multivector2D(
            KVector0,
            KVector1.GradeInvolution(),
            KVector2
        );
    }

    
    public LinFloat64Multivector2D Reverse()
    {
        return new LinFloat64Multivector2D(
            KVector0,
            KVector1,
            KVector2.Reverse()
        );
    }

    
    public LinFloat64Multivector2D CliffordConjugate()
    {
        return new LinFloat64Multivector2D(
            KVector0,
            KVector1.CliffordConjugate(),
            KVector2.CliffordConjugate()
        );
    }

    
    
    public double Sp(LinFloat64Scalar2D mv2)
    {
        var mv = 0d;

        if (!KVector0.IsZero() && !mv2.IsZero())
            mv += KVector0.Sp(mv2);

        return mv;
    }

    
    public double Sp(LinFloat64Vector2D mv2)
    {
        var mv = 0d;

        if (!KVector1.IsZero() && !mv2.IsZero())
            mv += KVector1.Sp(mv2);

        return mv;
    }

    
    public double Sp(LinFloat64Bivector2D mv2)
    {
        var mv = 0d;

        if (!KVector2.IsZero() && !mv2.IsZero())
            mv += KVector2.Sp(mv2);

        return mv;
    }

    
    public double Sp(LinFloat64Multivector2D mv2)
    {
        var mv = 0d;

        if (!KVector0.IsZero() && !mv2.KVector0.IsZero())
            mv += KVector0.Sp(mv2.KVector0);

        if (!KVector1.IsZero() && !mv2.KVector1.IsZero())
            mv += KVector1.Sp(mv2.KVector1);

        if (!KVector2.IsZero() && !mv2.KVector2.IsZero())
            mv += KVector2.Sp(mv2.KVector2);

        return mv;
    }

    
    
    public LinFloat64Multivector2D Op(LinFloat64Scalar2D mv2)
    {
        var mv = Zero;

        if (!KVector0.IsZero())
            mv += KVector0.Op(mv2);

        if (!KVector1.IsZero())
            mv += KVector1.Op(mv2);

        if (!KVector2.IsZero())
            mv += KVector2.Op(mv2);

        return mv;
    }
    
    
    public LinFloat64Multivector2D Op(LinFloat64Vector2D mv2)
    {
        var mv = Zero;

        if (!KVector0.IsZero())
            mv += KVector0.Op(mv2);

        if (!KVector1.IsZero())
            mv += KVector1.Op(mv2);

        if (!KVector2.IsZero())
            mv += KVector2.Op(mv2);

        return mv;
    }
    
    
    public LinFloat64Multivector2D Op(LinFloat64Bivector2D mv2)
    {
        var mv = Zero;

        if (!KVector0.IsZero())
            mv += KVector0.Op(mv2);

        if (!KVector1.IsZero())
            mv += KVector1.Op(mv2);

        return mv;
    }

    
    public LinFloat64Multivector2D Op(LinFloat64Multivector2D mv2)
    {
        var mv = Zero;

        if (!KVector0.IsZero())
            mv += KVector0.Op(mv2);

        if (!KVector1.IsZero())
            mv += KVector1.Op(mv2);

        if (!KVector2.IsZero())
            mv += KVector2.Op(mv2);

        return mv;
    }



    
    public IEnumerator<double> GetEnumerator()
    {
        yield return Scalar;
        yield return Scalar1;
        yield return Scalar2;
        yield return Scalar12;
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
    public override string ToString()
    {
        return $"{KVector0} + {KVector1} + {KVector2}";
    }

}