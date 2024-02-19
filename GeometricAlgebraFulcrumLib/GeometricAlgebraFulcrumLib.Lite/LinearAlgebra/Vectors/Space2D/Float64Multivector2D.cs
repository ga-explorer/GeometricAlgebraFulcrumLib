﻿using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

public sealed record Float64Multivector2D : 
    IFloat64Multivector2D
{
    public static Float64Multivector2D Zero { get; }
        = new Float64Multivector2D();

    public static Float64Multivector2D E0 { get; }
        = new Float64Multivector2D(Float64Scalar2D.E0);

    public static Float64Multivector2D E1 { get; }
        = new Float64Multivector2D(Float64Vector2D.E1);
        
    public static Float64Multivector2D E2 { get; }
        = new Float64Multivector2D(Float64Vector2D.E2);
        
    public static Float64Multivector2D E12 { get; }
        = new Float64Multivector2D(Float64Bivector2D.E12);
        
    public static Float64Multivector2D E21 { get; }
        = new Float64Multivector2D(Float64Bivector2D.E21);
        
    public static Float64Multivector2D NegativeE0 { get; }
        = new Float64Multivector2D(Float64Scalar2D.NegativeE0);
        
    public static Float64Multivector2D NegativeE1 { get; }
        = new Float64Multivector2D(Float64Vector2D.NegativeE1);

    public static Float64Multivector2D NegativeE2 { get; }
        = new Float64Multivector2D(Float64Vector2D.NegativeE2);
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D Create(Float64Scalar2D kVector)
    {
        return new Float64Multivector2D(kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D Create(Float64Vector2D kVector)
    {
        return new Float64Multivector2D(kVector);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D Create(Float64Bivector2D kVector)
    {
        return new Float64Multivector2D(kVector);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D Create(Float64Scalar2D kVector0, Float64Vector2D kVector1, Float64Bivector2D kVector2)
    {
        return new Float64Multivector2D(
            kVector0,
            kVector1,
            kVector2
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float64Multivector2D(Float64Scalar2D mv)
    {
        return new Float64Multivector2D(mv);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float64Multivector2D(Float64Vector2D mv)
    {
        return new Float64Multivector2D(mv);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float64Multivector2D(Float64Bivector2D mv)
    {
        return new Float64Multivector2D(mv);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D operator +(Float64Multivector2D mv)
    {
        return mv;
    } 
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D operator -(Float64Multivector2D mv)
    {
        return new Float64Multivector2D(
            -mv.KVector0, 
            -mv.KVector1, 
            -mv.KVector2
        );
    } 


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D operator +(Float64Multivector2D mv1, Float64Scalar2D mv2)
    {
        return new Float64Multivector2D(
            mv1.KVector0 + mv2,
            mv1.KVector1,
            mv1.KVector2
        );
    } 
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D operator +(Float64Multivector2D mv1, Float64Vector2D mv2)
    {
        return new Float64Multivector2D(
            mv1.KVector0,
            mv1.KVector1 + mv2,
            mv1.KVector2
        );
    } 
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D operator +(Float64Multivector2D mv1, Float64Bivector2D mv2)
    {
        return new Float64Multivector2D(
            mv1.KVector0,
            mv1.KVector1,
            mv1.KVector2 + mv2
        );
    } 
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D operator +(Float64Scalar2D mv1, Float64Multivector2D mv2)
    {
        return new Float64Multivector2D(
            mv1 + mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2
        );
    } 
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D operator +(Float64Vector2D mv1, Float64Multivector2D mv2)
    {
        return new Float64Multivector2D(
            mv2.KVector0,
            mv1 + mv2.KVector1,
            mv2.KVector2
        );
    } 
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D operator +(Float64Bivector2D mv1, Float64Multivector2D mv2)
    {
        return new Float64Multivector2D(
            mv2.KVector0,
            mv2.KVector1,
            mv1 + mv2.KVector2
        );
    } 
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D operator +(Float64Multivector2D mv1, Float64Multivector2D mv2)
    {
        return new Float64Multivector2D(
            mv1.KVector0 + mv2.KVector0,
            mv1.KVector1 + mv2.KVector1,
            mv1.KVector2 + mv2.KVector2
        );
    } 

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D operator -(Float64Multivector2D mv1, Float64Scalar2D mv2)
    {
        return new Float64Multivector2D(
            mv1.KVector0 - mv2,
            mv1.KVector1,
            mv1.KVector2
        );
    } 
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D operator -(Float64Multivector2D mv1, Float64Vector2D mv2)
    {
        return new Float64Multivector2D(
            mv1.KVector0,
            mv1.KVector1 - mv2,
            mv1.KVector2
        );
    } 
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D operator -(Float64Multivector2D mv1, Float64Bivector2D mv2)
    {
        return new Float64Multivector2D(
            mv1.KVector0,
            mv1.KVector1,
            mv1.KVector2 - mv2
        );
    } 
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D operator -(Float64Scalar2D mv1, Float64Multivector2D mv2)
    {
        return new Float64Multivector2D(
            mv1 - mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2
        );
    } 
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D operator -(Float64Vector2D mv1, Float64Multivector2D mv2)
    {
        return new Float64Multivector2D(
            mv2.KVector0,
            mv1 - mv2.KVector1,
            mv2.KVector2
        );
    } 
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D operator -(Float64Bivector2D mv1, Float64Multivector2D mv2)
    {
        return new Float64Multivector2D(
            mv2.KVector0,
            mv2.KVector1,
            mv1 - mv2.KVector2
        );
    } 
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D operator -(Float64Multivector2D mv1, Float64Multivector2D mv2)
    {
        return new Float64Multivector2D(
            mv1.KVector0 - mv2.KVector0,
            mv1.KVector1 - mv2.KVector1,
            mv1.KVector2 - mv2.KVector2
        );
    }


    public int VSpaceDimensions 
        => 3;

    public Float64Scalar2D KVector0 { get; }

    public Float64Vector2D KVector1 { get; }

    public Float64Bivector2D KVector2 { get; }
        
    public Float64Scalar Scalar 
        => KVector0.Scalar;

    public Float64Scalar Scalar1 
        => KVector1.Scalar1;

    public Float64Scalar Scalar2 
        => KVector1.Scalar2;
        
    public Float64Scalar Scalar12 
        => KVector2.Scalar12;
        
    public int Count 
        => 4;

    public Float64Scalar this[int grade, int index]
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

    public Float64Scalar this[int id]
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

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64Multivector2D()
    {
        KVector0 = Float64Scalar2D.Zero;
        KVector1 = Float64Vector2D.Zero;
        KVector2 = Float64Bivector2D.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64Multivector2D(Float64Scalar2D kVector0)
    {
        KVector0 = kVector0;
        KVector1 = Float64Vector2D.Zero;
        KVector2 = Float64Bivector2D.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64Multivector2D(Float64Vector2D kVector1)
    {
        KVector0 = Float64Scalar2D.Zero;
        KVector1 = kVector1;
        KVector2 = Float64Bivector2D.Zero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64Multivector2D(Float64Bivector2D kVector2)
    {
        KVector0 = Float64Scalar2D.Zero;
        KVector1 = Float64Vector2D.Zero;
        KVector2 = kVector2;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64Multivector2D(Float64Scalar2D kVector0, Float64Bivector2D kVector2)
    {
        KVector0 = kVector0;
        KVector1 = Float64Vector2D.Zero;
        KVector2 = kVector2;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64Multivector2D(Float64Scalar2D kVector0, Float64Vector2D kVector1, Float64Bivector2D kVector2)
    {
        KVector0 = kVector0;
        KVector1 = kVector1;
        KVector2 = kVector2;
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return KVector0.IsValid() &&
               KVector1.IsValid() &&
               KVector2.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return KVector0.IsZero() &&
               KVector1.IsZero() &&
               KVector2.IsZero();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double epsilon = 1e-12d)
    {
        return Norm().IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar NormSquared()
    {
        return KVector0.NormSquared() + 
               KVector1.NormSquared() + 
               KVector2.NormSquared();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Multivector2D ToMultivector2D()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Norm()
    {
        return NormSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Multivector2D GradeInvolution()
    {
        return new Float64Multivector2D(
            KVector0,
            KVector1.GradeInvolution(),
            KVector2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Multivector2D Reverse()
    {
        return new Float64Multivector2D(
            KVector0,
            KVector1,
            KVector2.Reverse()
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Multivector2D CliffordConjugate()
    {
        return new Float64Multivector2D(
            KVector0,
            KVector1.CliffordConjugate(),
            KVector2.CliffordConjugate()
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
        return $"{KVector0} + {KVector1} + {KVector2}";
    }

}