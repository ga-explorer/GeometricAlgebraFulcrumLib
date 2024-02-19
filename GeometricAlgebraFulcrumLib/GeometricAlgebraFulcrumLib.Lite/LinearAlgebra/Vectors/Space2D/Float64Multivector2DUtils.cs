using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

public static class Float64Multivector2DUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this Float64Scalar2D mv1, Float64Scalar2D mv2)
    {
        return mv1.Scalar * mv2.Scalar;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this Float64Scalar2D mv1, Float64Vector2D mv2)
    {
        return Float64Scalar.Zero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this Float64Scalar2D mv1, Float64Bivector2D mv2)
    {
        return Float64Scalar.Zero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this Float64Scalar2D mv1, Float64Multivector2D mv2)
    {
        var mv = 0d;

        if (!mv1.IsZero() && !mv2.KVector0.IsZero())
            mv += mv1.Sp(mv2.KVector0);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this Float64Vector2D mv1, Float64Scalar2D mv2)
    {
        return Float64Scalar.Zero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this Float64Vector2D mv1, Float64Vector2D mv2)
    {
        return mv1.Scalar1 * mv2.Scalar1 + 
               mv1.Scalar2 * mv2.Scalar2;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this Float64Vector2D mv1, Float64Bivector2D mv2)
    {
        return Float64Scalar.Zero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this Float64Vector2D mv1, Float64Multivector2D mv2)
    {
        var mv = 0d;

        if (!mv1.IsZero() && !mv2.KVector1.IsZero())
            mv += mv1.Sp(mv2.KVector1);

        return mv;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this Float64Bivector2D mv1, Float64Scalar2D mv2)
    {
        return Float64Scalar.Zero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this Float64Bivector2D mv1, Float64Vector2D mv2)
    {
        return Float64Scalar.Zero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this Float64Bivector2D mv1, Float64Bivector2D mv2)
    {
        return -(mv1.Scalar12 * mv2.Scalar12);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this Float64Bivector2D mv1, Float64Multivector2D mv2)
    {
        var mv = 0d;

        if (!mv1.IsZero() && !mv2.KVector2.IsZero())
            mv += mv1.Sp(mv2.KVector2);

        return mv;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this Float64Multivector2D mv1, Float64Scalar2D mv2)
    {
        var mv = 0d;

        if (!mv1.KVector0.IsZero() && !mv2.IsZero())
            mv += mv1.KVector0.Sp(mv2);

        return mv;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this Float64Multivector2D mv1, Float64Vector2D mv2)
    {
        var mv = 0d;

        if (!mv1.KVector1.IsZero() && !mv2.IsZero())
            mv += mv1.KVector1.Sp(mv2);

        return mv;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this Float64Multivector2D mv1, Float64Bivector2D mv2)
    {
        var mv = 0d;

        if (!mv1.KVector2.IsZero() && !mv2.IsZero())
            mv += mv1.KVector2.Sp(mv2);

        return mv;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar Sp(this Float64Multivector2D mv1, Float64Multivector2D mv2)
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
    public static Float64Scalar2D Op(this Float64Scalar2D mv1, Float64Scalar2D mv2)
    {
        return Float64Scalar2D.Create(
            mv1.Scalar * mv2.Scalar
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Op(this Float64Vector2D mv1, Float64Scalar2D mv2)
    {
        return Float64Vector2D.Create(
            mv1.Scalar1 * mv2.Scalar,
            mv1.Scalar2 * mv2.Scalar
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Op(this Float64Scalar2D mv1, Float64Vector2D mv2)
    {
        return Float64Vector2D.Create(
            mv1.Scalar * mv2.Scalar1,
            mv1.Scalar * mv2.Scalar2
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bivector2D VectorOp(this IPair<double> mv1, IPair<double> mv2)
    {
        return Float64Bivector2D.Create(
            mv1.Item1 * mv2.Item2 - mv1.Item2 * mv2.Item1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bivector2D Op(this Float64Vector2D mv1, Float64Vector2D mv2)
    {
        return Float64Bivector2D.Create(
            mv1.Scalar1 * mv2.Scalar2 - mv1.Scalar2 * mv2.Scalar1
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bivector2D Op(this Float64Scalar2D mv1, Float64Bivector2D mv2)
    {
        return Float64Bivector2D.Create(
            mv1.Scalar * mv2.Scalar12
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Bivector2D Op(this Float64Bivector2D mv1, Float64Scalar2D mv2)
    {
        return Float64Bivector2D.Create(
            mv1.Scalar12 * mv2.Scalar
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar2D Op(this Float64Bivector2D mv1, Float64Bivector2D mv2)
    {
        return Float64Scalar2D.Zero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D Op(this Float64Multivector2D mv1, Float64Scalar2D mv2)
    {
        var mv = Float64Multivector2D.Zero;

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);
            
        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);
            
        if (!mv1.KVector2.IsZero())
            mv += mv1.KVector2.Op(mv2);
            
        return mv;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D Op(this Float64Scalar2D mv1, Float64Multivector2D mv2)
    {
        var mv = Float64Multivector2D.Zero;

        if (!mv2.KVector0.IsZero())
            mv += mv1.Op(mv2.KVector0);
            
        if (!mv2.KVector1.IsZero())
            mv += mv1.Op(mv2.KVector1);
            
        if (!mv2.KVector2.IsZero())
            mv += mv1.Op(mv2.KVector2);
            
        return mv;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D Op(this Float64Vector2D mv1, Float64Multivector2D mv2)
    {
        var mv = Float64Multivector2D.Zero;

        if (!mv2.KVector0.IsZero())
            mv += mv1.Op(mv2.KVector0);
            
        if (!mv2.KVector1.IsZero())
            mv += mv1.Op(mv2.KVector1);
            
        if (!mv2.KVector2.IsZero())
            mv += mv1.Op(mv2.KVector2);
            
        return mv;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D Op(this Float64Multivector2D mv1, Float64Vector2D mv2)
    {
        var mv = Float64Multivector2D.Zero;

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);
            
        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);
            
        if (!mv1.KVector2.IsZero())
            mv += mv1.KVector2.Op(mv2);
            
        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D Op(this Float64Bivector2D mv1, Float64Multivector2D mv2)
    {
        var mv = Float64Multivector2D.Zero;

        if (!mv2.KVector0.IsZero())
            mv += mv1.Op(mv2.KVector0);
            
        if (!mv2.KVector1.IsZero())
            mv += mv1.Op(mv2.KVector1);
            
        return mv;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D Op(this Float64Multivector2D mv1, Float64Bivector2D mv2)
    {
        var mv = Float64Multivector2D.Zero;

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);
            
        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);
            
        return mv;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Multivector2D Op(this Float64Multivector2D mv1, Float64Multivector2D mv2)
    {
        var mv = Float64Multivector2D.Zero;

        if (!mv1.KVector0.IsZero())
            mv += mv1.KVector0.Op(mv2);
            
        if (!mv1.KVector1.IsZero())
            mv += mv1.KVector1.Op(mv2);

        if (!mv1.KVector2.IsZero())
            mv += mv1.KVector2.Op(mv2);
            
        return mv;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Lcp(this Float64Vector2D v1, Float64Bivector2D b2)
    {
        var s1 = 
            -v1.Scalar2 * b2.Scalar12;

        var s2 = 
            v1.Scalar1 * b2.Scalar12;
            
        return Float64Vector2D.Create(s1, s2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Rcp(this Float64Bivector2D b1, Float64Vector2D v2)
    {
        var s1 = 
            b1.Scalar12 * v2.Scalar2;

        var s2 = 
            -b1.Scalar12 * v2.Scalar1;
            
        return Float64Vector2D.Create(s1, s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D ProjectOn(this Float64Vector2D mv1, Float64Bivector2D mv2)
    {
        return mv1.Lcp(mv2).Lcp(mv2.Inverse());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Gp(this Float64Vector2D mv1, Float64Bivector2D mv2)
    {
        var s1 = 
            -mv1.Scalar2 * mv2.Scalar12;

        var s2 = 
            mv1.Scalar1 * mv2.Scalar12;

        return Float64Vector2D.Create(s1, s2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D Gp(this Float64Bivector2D mv1, Float64Vector2D mv2)
    {
        var s1 = 
            mv1.Scalar12 * mv2.Scalar2;

        var s2 = 
            -mv1.Scalar12 * mv2.Scalar1;

        return Float64Vector2D.Create(s1, s2);
    }
}