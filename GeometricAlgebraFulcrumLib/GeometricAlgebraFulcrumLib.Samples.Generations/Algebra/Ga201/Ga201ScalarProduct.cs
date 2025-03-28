using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga201;

public static class Ga201ScalarProduct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201KVector0 mv1, Ga201KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector0.Zero;
        
        return new Ga201KVector0
        {
            Scalar = mv1.Scalar * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201KVector0 mv1, Ga201KVector1 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201KVector0 mv1, Ga201KVector2 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201KVector0 mv1, Ga201KVector3 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    public static Ga201KVector0 Sp(this Ga201KVector0 mv1, Ga201Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar += mv1.Scalar * mv2.KVector0.Scalar;
        }
        
        return new Ga201KVector0
        {
            Scalar = tempScalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201KVector1 mv1, Ga201KVector0 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201KVector1 mv1, Ga201KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector0.Zero;
        
        return new Ga201KVector0
        {
            Scalar = mv1.Scalar2 * mv2.Scalar2 + mv1.Scalar3 * mv2.Scalar3
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201KVector1 mv1, Ga201KVector2 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201KVector1 mv1, Ga201KVector3 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    public static Ga201KVector0 Sp(this Ga201KVector1 mv1, Ga201Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar += mv1.Scalar2 * mv2.KVector1.Scalar2 + mv1.Scalar3 * mv2.KVector1.Scalar3;
        }
        
        return new Ga201KVector0
        {
            Scalar = tempScalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201KVector2 mv1, Ga201KVector0 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201KVector2 mv1, Ga201KVector1 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201KVector2 mv1, Ga201KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector0.Zero;
        
        return new Ga201KVector0
        {
            Scalar = -mv1.Scalar23 * mv2.Scalar23
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201KVector2 mv1, Ga201KVector3 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    public static Ga201KVector0 Sp(this Ga201KVector2 mv1, Ga201Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar += -mv1.Scalar23 * mv2.KVector2.Scalar23;
        }
        
        return new Ga201KVector0
        {
            Scalar = tempScalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201KVector3 mv1, Ga201KVector0 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201KVector3 mv1, Ga201KVector1 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201KVector3 mv1, Ga201KVector2 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201KVector3 mv1, Ga201KVector3 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201KVector3 mv1, Ga201Multivector mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    public static Ga201KVector0 Sp(this Ga201Multivector mv1, Ga201KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar += mv1.KVector0.Scalar * mv2.Scalar;
        }
        
        return new Ga201KVector0
        {
            Scalar = tempScalar
        };
    }
    
    public static Ga201KVector0 Sp(this Ga201Multivector mv1, Ga201KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar += mv1.KVector1.Scalar2 * mv2.Scalar2 + mv1.KVector1.Scalar3 * mv2.Scalar3;
        }
        
        return new Ga201KVector0
        {
            Scalar = tempScalar
        };
    }
    
    public static Ga201KVector0 Sp(this Ga201Multivector mv1, Ga201KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar += -mv1.KVector2.Scalar23 * mv2.Scalar23;
        }
        
        return new Ga201KVector0
        {
            Scalar = tempScalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Sp(this Ga201Multivector mv1, Ga201KVector3 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    public static Ga201KVector0 Sp(this Ga201Multivector mv1, Ga201Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector0.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar += mv1.KVector0.Scalar * mv2.KVector0.Scalar;
            }
            
        }
        
        if (!mv1.KVector1.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar += mv1.KVector1.Scalar2 * mv2.KVector1.Scalar2 + mv1.KVector1.Scalar3 * mv2.KVector1.Scalar3;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector2.IsZero())
            {
                tempScalar += -mv1.KVector2.Scalar23 * mv2.KVector2.Scalar23;
            }
            
        }
        
        return new Ga201KVector0
        {
            Scalar = tempScalar
        };
    }
    
}
