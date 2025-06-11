using System;
using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

public sealed record LinFloat64Multivector3D :
    ILinFloat64Multivector3D
{
    public static LinFloat64Multivector3D Zero { get; }
        = new LinFloat64Multivector3D();

    public static LinFloat64Multivector3D E0 { get; }
        = new LinFloat64Multivector3D(LinFloat64Scalar3D.E0);

    public static LinFloat64Multivector3D E1 { get; }
        = new LinFloat64Multivector3D(LinFloat64Vector3D.E1);

    public static LinFloat64Multivector3D E2 { get; }
        = new LinFloat64Multivector3D(LinFloat64Vector3D.E2);

    public static LinFloat64Multivector3D E3 { get; }
        = new LinFloat64Multivector3D(LinFloat64Vector3D.E3);

    public static LinFloat64Multivector3D E12 { get; }
        = new LinFloat64Multivector3D(LinFloat64Bivector3D.E12);

    public static LinFloat64Multivector3D E21 { get; }
        = new LinFloat64Multivector3D(LinFloat64Bivector3D.E21);

    public static LinFloat64Multivector3D E13 { get; }
        = new LinFloat64Multivector3D(LinFloat64Bivector3D.E13);

    public static LinFloat64Multivector3D E31 { get; }
        = new LinFloat64Multivector3D(LinFloat64Bivector3D.E31);

    public static LinFloat64Multivector3D E23 { get; }
        = new LinFloat64Multivector3D(LinFloat64Bivector3D.E23);

    public static LinFloat64Multivector3D E32 { get; }
        = new LinFloat64Multivector3D(LinFloat64Bivector3D.E32);

    public static LinFloat64Multivector3D E123 { get; }
        = new LinFloat64Multivector3D(LinFloat64Trivector3D.E123);

    public static LinFloat64Multivector3D NegativeE0 { get; }
        = new LinFloat64Multivector3D(LinFloat64Scalar3D.NegativeE0);

    public static LinFloat64Multivector3D NegativeE1 { get; }
        = new LinFloat64Multivector3D(LinFloat64Vector3D.NegativeE1);

    public static LinFloat64Multivector3D NegativeE2 { get; }
        = new LinFloat64Multivector3D(LinFloat64Vector3D.NegativeE2);

    public static LinFloat64Multivector3D NegativeE3 { get; }
        = new LinFloat64Multivector3D(LinFloat64Vector3D.NegativeE3);

    public static LinFloat64Multivector3D NegativeE123 { get; }
        = new LinFloat64Multivector3D(LinFloat64Trivector3D.NegativeE123);

    public static LinFloat64Multivector3D InverseE123 { get; }
        = new LinFloat64Multivector3D(LinFloat64Trivector3D.InverseE123);


    
    public static LinFloat64Multivector3D Create(LinFloat64Scalar3D kVector)
    {
        return new LinFloat64Multivector3D(kVector);
    }

    
    public static LinFloat64Multivector3D Create(LinFloat64Vector3D kVector)
    {
        return new LinFloat64Multivector3D(kVector);
    }

    
    public static LinFloat64Multivector3D Create(LinFloat64Bivector3D kVector)
    {
        return new LinFloat64Multivector3D(kVector);
    }

    
    public static LinFloat64Multivector3D Create(LinFloat64Trivector3D kVector)
    {
        return new LinFloat64Multivector3D(kVector);
    }

    
    public static LinFloat64Multivector3D Create(LinFloat64Scalar3D kVector0, LinFloat64Vector3D kVector1, LinFloat64Bivector3D kVector2, LinFloat64Trivector3D kVector3)
    {
        return new LinFloat64Multivector3D(
            kVector0,
            kVector1,
            kVector2,
            kVector3
        );
    }


    
    public static implicit operator LinFloat64Multivector3D(LinFloat64Scalar3D mv)
    {
        return new LinFloat64Multivector3D(mv);
    }

    
    public static implicit operator LinFloat64Multivector3D(LinFloat64Vector3D mv)
    {
        return new LinFloat64Multivector3D(mv);
    }

    
    public static implicit operator LinFloat64Multivector3D(LinFloat64Bivector3D mv)
    {
        return new LinFloat64Multivector3D(mv);
    }

    
    public static implicit operator LinFloat64Multivector3D(LinFloat64Trivector3D mv)
    {
        return new LinFloat64Multivector3D(mv);
    }


    
    public static LinFloat64Multivector3D operator +(LinFloat64Multivector3D mv)
    {
        return mv;
    }

    
    public static LinFloat64Multivector3D operator -(LinFloat64Multivector3D mv)
    {
        return new LinFloat64Multivector3D(
            -mv.KVector0,
            -mv.KVector1,
            -mv.KVector2,
            -mv.KVector3
        );
    }


    
    public static LinFloat64Multivector3D operator +(LinFloat64Multivector3D mv1, LinFloat64Scalar3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv1.KVector0 + mv2,
            mv1.KVector1,
            mv1.KVector2,
            mv1.KVector3
        );
    }

    
    public static LinFloat64Multivector3D operator +(LinFloat64Multivector3D mv1, LinFloat64Vector3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv1.KVector0,
            mv1.KVector1 + mv2,
            mv1.KVector2,
            mv1.KVector3
        );
    }

    
    public static LinFloat64Multivector3D operator +(LinFloat64Multivector3D mv1, LinFloat64Bivector3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv1.KVector0,
            mv1.KVector1,
            mv1.KVector2 + mv2,
            mv1.KVector3
        );
    }

    
    public static LinFloat64Multivector3D operator +(LinFloat64Multivector3D mv1, LinFloat64Trivector3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv1.KVector0,
            mv1.KVector1,
            mv1.KVector2,
            mv1.KVector3 + mv2
        );
    }

    
    public static LinFloat64Multivector3D operator +(LinFloat64Scalar3D mv1, LinFloat64Multivector3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv1 + mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3
        );
    }

    
    public static LinFloat64Multivector3D operator +(LinFloat64Vector3D mv1, LinFloat64Multivector3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv2.KVector0,
            mv1 + mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3
        );
    }

    
    public static LinFloat64Multivector3D operator +(LinFloat64Bivector3D mv1, LinFloat64Multivector3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv2.KVector0,
            mv2.KVector1,
            mv1 + mv2.KVector2,
            mv2.KVector3
        );
    }

    
    public static LinFloat64Multivector3D operator +(LinFloat64Trivector3D mv1, LinFloat64Multivector3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            mv1 + mv2.KVector3
        );
    }

    
    public static LinFloat64Multivector3D operator +(LinFloat64Multivector3D mv1, LinFloat64Multivector3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv1.KVector0 + mv2.KVector0,
            mv1.KVector1 + mv2.KVector1,
            mv1.KVector2 + mv2.KVector2,
            mv1.KVector3 + mv2.KVector3
        );
    }


    
    public static LinFloat64Multivector3D operator -(LinFloat64Multivector3D mv1, LinFloat64Scalar3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv1.KVector0 - mv2,
            mv1.KVector1,
            mv1.KVector2,
            mv1.KVector3
        );
    }

    
    public static LinFloat64Multivector3D operator -(LinFloat64Multivector3D mv1, LinFloat64Vector3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv1.KVector0,
            mv1.KVector1 - mv2,
            mv1.KVector2,
            mv1.KVector3
        );
    }

    
    public static LinFloat64Multivector3D operator -(LinFloat64Multivector3D mv1, LinFloat64Bivector3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv1.KVector0,
            mv1.KVector1,
            mv1.KVector2 - mv2,
            mv1.KVector3
        );
    }

    
    public static LinFloat64Multivector3D operator -(LinFloat64Multivector3D mv1, LinFloat64Trivector3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv1.KVector0,
            mv1.KVector1,
            mv1.KVector2,
            mv1.KVector3 - mv2
        );
    }

    
    public static LinFloat64Multivector3D operator -(LinFloat64Scalar3D mv1, LinFloat64Multivector3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv1 - mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3
        );
    }

    
    public static LinFloat64Multivector3D operator -(LinFloat64Vector3D mv1, LinFloat64Multivector3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv2.KVector0,
            mv1 - mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3
        );
    }

    
    public static LinFloat64Multivector3D operator -(LinFloat64Bivector3D mv1, LinFloat64Multivector3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv2.KVector0,
            mv2.KVector1,
            mv1 - mv2.KVector2,
            mv2.KVector3
        );
    }

    
    public static LinFloat64Multivector3D operator -(LinFloat64Trivector3D mv1, LinFloat64Multivector3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            mv1 - mv2.KVector3
        );
    }

    
    public static LinFloat64Multivector3D operator -(LinFloat64Multivector3D mv1, LinFloat64Multivector3D mv2)
    {
        return new LinFloat64Multivector3D(
            mv1.KVector0 - mv2.KVector0,
            mv1.KVector1 - mv2.KVector1,
            mv1.KVector2 - mv2.KVector2,
            mv1.KVector3 - mv2.KVector3
        );
    }


    public int VSpaceDimensions
        => 3;

    public LinFloat64Scalar3D KVector0 { get; }

    public LinFloat64Vector3D KVector1 { get; }

    public LinFloat64Bivector3D KVector2 { get; }

    public LinFloat64Trivector3D KVector3 { get; }

    public double Scalar
        => KVector0.Scalar;

    public double Scalar1
        => KVector1.Scalar1;

    public double Scalar2
        => KVector1.Scalar2;

    public double Scalar3
        => KVector1.Scalar3;

    public double Scalar12
        => KVector2.Scalar12;

    public double Scalar13
        => KVector2.Scalar13;

    public double Scalar23
        => KVector2.Scalar23;

    public double Scalar123
        => KVector3.Scalar123;

    public int Count
        => 8;

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
                    2 => KVector1.Scalar3,
                    _ => throw new IndexOutOfRangeException(nameof(index))
                },

                2 => index switch
                {
                    0 => KVector2.Scalar12,
                    1 => KVector2.Scalar13,
                    2 => KVector2.Scalar23,
                    _ => throw new IndexOutOfRangeException(nameof(index))
                },

                3 => index == 0
                    ? KVector3.Scalar123
                    : throw new IndexOutOfRangeException(nameof(index)),

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
                4 => KVector1.Scalar3,

                3 => KVector2.Scalar12,
                5 => KVector2.Scalar13,
                6 => KVector2.Scalar23,

                7 => KVector3.Scalar123,

                _ => throw new IndexOutOfRangeException(nameof(id))
            };
        }
    }


    
    private LinFloat64Multivector3D()
    {
        KVector0 = LinFloat64Scalar3D.Zero;
        KVector1 = LinFloat64Vector3D.Zero;
        KVector2 = LinFloat64Bivector3D.Zero;
        KVector3 = LinFloat64Trivector3D.Zero;
    }

    
    private LinFloat64Multivector3D(LinFloat64Scalar3D kVector0)
    {
        KVector0 = kVector0;
        KVector1 = LinFloat64Vector3D.Zero;
        KVector2 = LinFloat64Bivector3D.Zero;
        KVector3 = LinFloat64Trivector3D.Zero;
    }

    
    private LinFloat64Multivector3D(LinFloat64Vector3D kVector1)
    {
        KVector0 = LinFloat64Scalar3D.Zero;
        KVector1 = kVector1;
        KVector2 = LinFloat64Bivector3D.Zero;
        KVector3 = LinFloat64Trivector3D.Zero;
    }

    
    private LinFloat64Multivector3D(LinFloat64Bivector3D kVector2)
    {
        KVector0 = LinFloat64Scalar3D.Zero;
        KVector1 = LinFloat64Vector3D.Zero;
        KVector2 = kVector2;
        KVector3 = LinFloat64Trivector3D.Zero;
    }

    
    private LinFloat64Multivector3D(LinFloat64Trivector3D kVector3)
    {
        KVector0 = LinFloat64Scalar3D.Zero;
        KVector1 = LinFloat64Vector3D.Zero;
        KVector2 = LinFloat64Bivector3D.Zero;
        KVector3 = kVector3;
    }

    
    private LinFloat64Multivector3D(LinFloat64Scalar3D kVector0, LinFloat64Bivector3D kVector2)
    {
        KVector0 = kVector0;
        KVector1 = LinFloat64Vector3D.Zero;
        KVector2 = kVector2;
        KVector3 = LinFloat64Trivector3D.Zero;
    }

    
    private LinFloat64Multivector3D(LinFloat64Vector3D kVector1, LinFloat64Trivector3D kVector3)
    {
        KVector0 = LinFloat64Scalar3D.Zero;
        KVector1 = kVector1;
        KVector2 = LinFloat64Bivector3D.Zero;
        KVector3 = kVector3;
    }

    
    private LinFloat64Multivector3D(LinFloat64Scalar3D kVector0, LinFloat64Vector3D kVector1, LinFloat64Bivector3D kVector2, LinFloat64Trivector3D kVector3)
    {
        KVector0 = kVector0;
        KVector1 = kVector1;
        KVector2 = kVector2;
        KVector3 = kVector3;
    }


    
    public bool IsValid()
    {
        return KVector0.IsValid() &&
               KVector1.IsValid() &&
               KVector2.IsValid() &&
               KVector3.IsValid();
    }

    
    public bool IsZero()
    {
        return KVector0.IsZero() &&
               KVector1.IsZero() &&
               KVector2.IsZero() &&
               KVector3.IsZero();
    }

    
    public bool IsNearZero(double zeroEpsilon = 1e-12d)
    {
        return Norm().IsNearZero(zeroEpsilon);
    }

    
    public double NormSquared()
    {
        return KVector0.NormSquared() +
               KVector1.NormSquared() +
               KVector2.NormSquared() +
               KVector3.NormSquared();
    }

    
    public LinFloat64Multivector3D ToMultivector3D()
    {
        return this;
    }

    
    public double Norm()
    {
        return NormSquared().Sqrt();
    }

    
    public LinFloat64Multivector3D GradeInvolution()
    {
        return new LinFloat64Multivector3D(
            KVector0,
            KVector1.GradeInvolution(),
            KVector2,
            KVector3.GradeInvolution()
        );
    }

    
    public LinFloat64Multivector3D Reverse()
    {
        return new LinFloat64Multivector3D(
            KVector0,
            KVector1,
            KVector2.Reverse(),
            KVector3.Reverse()
        );
    }

    
    public LinFloat64Multivector3D CliffordConjugate()
    {
        return new LinFloat64Multivector3D(
            KVector0,
            KVector1.CliffordConjugate(),
            KVector2.CliffordConjugate(),
            KVector3
        );
    }
    

    
    public double Sp(LinFloat64Scalar3D mv2)
    {
        var mv = 0d;

        if (!KVector0.IsZero() && !mv2.IsZero())
            mv += KVector0.Sp(mv2);

        return mv;
    }

    
    public double Sp(LinFloat64Vector3D mv2)
    {
        var mv = 0d;

        if (!KVector1.IsZero() && !mv2.IsZero())
            mv += KVector1.Sp(mv2);

        return mv;
    }

    
    public double Sp(LinFloat64Bivector3D mv2)
    {
        var mv = 0d;

        if (!KVector2.IsZero() && !mv2.IsZero())
            mv += KVector2.Sp(mv2);

        return mv;
    }

    
    public double Sp(LinFloat64Trivector3D mv2)
    {
        var mv = 0d;

        if (!KVector3.IsZero() && !mv2.IsZero())
            mv += KVector3.Sp(mv2);

        return mv;
    }

    
    public double Sp(LinFloat64Multivector3D mv2)
    {
        var mv = 0d;

        if (!KVector0.IsZero() && !mv2.KVector0.IsZero())
            mv += KVector0.Sp(mv2.KVector0);

        if (!KVector1.IsZero() && !mv2.KVector1.IsZero())
            mv += KVector1.Sp(mv2.KVector1);

        if (!KVector2.IsZero() && !mv2.KVector2.IsZero())
            mv += KVector2.Sp(mv2.KVector2);

        if (!KVector3.IsZero() && !mv2.KVector3.IsZero())
            mv += KVector3.Sp(mv2.KVector3);

        return mv;
    }


    
    public LinFloat64Multivector3D Op(LinFloat64Scalar3D mv2)
    {
        var mv = Zero;

        if (!KVector0.IsZero())
            mv += KVector0.Op(mv2);

        if (!KVector1.IsZero())
            mv += KVector1.Op(mv2);

        if (!KVector2.IsZero())
            mv += KVector2.Op(mv2);

        if (!KVector3.IsZero())
            mv += KVector3.Op(mv2);

        return mv;
    }
    
    
    public LinFloat64Multivector3D Op(LinFloat64Vector3D mv2)
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

    
    public LinFloat64Multivector3D Op(LinFloat64Bivector3D mv2)
    {
        var mv = Zero;

        if (!KVector0.IsZero())
            mv += KVector0.Op(mv2);

        if (!KVector1.IsZero())
            mv += KVector1.Op(mv2);

        return mv;
    }

    
    public LinFloat64Multivector3D Op(LinFloat64Trivector3D mv2)
    {
        var mv = Zero;

        if (!KVector0.IsZero())
            mv += KVector0.Op(mv2);

        return mv;
    }

    
    public LinFloat64Multivector3D Op(LinFloat64Multivector3D mv2)
    {
        var mv = Zero;

        if (!KVector0.IsZero())
            mv += KVector0.Op(mv2);

        if (!KVector1.IsZero())
            mv += KVector1.Op(mv2);

        if (!KVector2.IsZero())
            mv += KVector2.Op(mv2);

        if (!KVector3.IsZero())
            mv += KVector3.Op(mv2);

        return mv;
    }


    
    public IEnumerator<double> GetEnumerator()
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

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
    public override string ToString()
    {
        return $"({KVector0}) + {KVector1} + {KVector2} + {KVector3}";
    }

}