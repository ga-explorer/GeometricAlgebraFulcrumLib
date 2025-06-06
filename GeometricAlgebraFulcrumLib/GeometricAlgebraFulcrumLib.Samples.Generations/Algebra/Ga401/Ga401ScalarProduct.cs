using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga401;

public static class Ga401ScalarProduct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector0 mv1, Ga401KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        return new Ga401KVector0
        {
            Scalar = mv1.Scalar * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector0 mv1, Ga401KVector1 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector0 mv1, Ga401KVector2 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector0 mv1, Ga401KVector3 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector0 mv1, Ga401KVector4 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector0 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    public static Ga401KVector0 Sp(this Ga401KVector0 mv1, Ga401Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar += mv1.Scalar * mv2.KVector0.Scalar;
        }
        
        return new Ga401KVector0
        {
            Scalar = tempScalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector1 mv1, Ga401KVector0 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector1 mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        return new Ga401KVector0
        {
            Scalar = mv1.Scalar2 * mv2.Scalar2 + mv1.Scalar3 * mv2.Scalar3 + mv1.Scalar4 * mv2.Scalar4 + mv1.Scalar5 * mv2.Scalar5
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector1 mv1, Ga401KVector2 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector1 mv1, Ga401KVector3 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector1 mv1, Ga401KVector4 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector1 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    public static Ga401KVector0 Sp(this Ga401KVector1 mv1, Ga401Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar += mv1.Scalar2 * mv2.KVector1.Scalar2 + mv1.Scalar3 * mv2.KVector1.Scalar3 + mv1.Scalar4 * mv2.KVector1.Scalar4 + mv1.Scalar5 * mv2.KVector1.Scalar5;
        }
        
        return new Ga401KVector0
        {
            Scalar = tempScalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector2 mv1, Ga401KVector0 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector2 mv1, Ga401KVector1 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector2 mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        return new Ga401KVector0
        {
            Scalar = -mv1.Scalar23 * mv2.Scalar23 - mv1.Scalar24 * mv2.Scalar24 - mv1.Scalar34 * mv2.Scalar34 - mv1.Scalar25 * mv2.Scalar25 - mv1.Scalar35 * mv2.Scalar35 - mv1.Scalar45 * mv2.Scalar45
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector2 mv1, Ga401KVector3 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector2 mv1, Ga401KVector4 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector2 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    public static Ga401KVector0 Sp(this Ga401KVector2 mv1, Ga401Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar += -mv1.Scalar23 * mv2.KVector2.Scalar23 - mv1.Scalar24 * mv2.KVector2.Scalar24 - mv1.Scalar34 * mv2.KVector2.Scalar34 - mv1.Scalar25 * mv2.KVector2.Scalar25 - mv1.Scalar35 * mv2.KVector2.Scalar35 - mv1.Scalar45 * mv2.KVector2.Scalar45;
        }
        
        return new Ga401KVector0
        {
            Scalar = tempScalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector3 mv1, Ga401KVector0 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector3 mv1, Ga401KVector1 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector3 mv1, Ga401KVector2 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector3 mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        return new Ga401KVector0
        {
            Scalar = -mv1.Scalar234 * mv2.Scalar234 - mv1.Scalar235 * mv2.Scalar235 - mv1.Scalar245 * mv2.Scalar245 - mv1.Scalar345 * mv2.Scalar345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector3 mv1, Ga401KVector4 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector3 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    public static Ga401KVector0 Sp(this Ga401KVector3 mv1, Ga401Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar += -mv1.Scalar234 * mv2.KVector3.Scalar234 - mv1.Scalar235 * mv2.KVector3.Scalar235 - mv1.Scalar245 * mv2.KVector3.Scalar245 - mv1.Scalar345 * mv2.KVector3.Scalar345;
        }
        
        return new Ga401KVector0
        {
            Scalar = tempScalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector4 mv1, Ga401KVector0 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector4 mv1, Ga401KVector1 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector4 mv1, Ga401KVector2 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector4 mv1, Ga401KVector3 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector4 mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        return new Ga401KVector0
        {
            Scalar = mv1.Scalar2345 * mv2.Scalar2345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector4 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    public static Ga401KVector0 Sp(this Ga401KVector4 mv1, Ga401Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar += mv1.Scalar2345 * mv2.KVector4.Scalar2345;
        }
        
        return new Ga401KVector0
        {
            Scalar = tempScalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector5 mv1, Ga401KVector0 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector5 mv1, Ga401KVector1 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector5 mv1, Ga401KVector2 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector5 mv1, Ga401KVector3 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector5 mv1, Ga401KVector4 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector5 mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401KVector5 mv1, Ga401Multivector mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    public static Ga401KVector0 Sp(this Ga401Multivector mv1, Ga401KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar += mv1.KVector0.Scalar * mv2.Scalar;
        }
        
        return new Ga401KVector0
        {
            Scalar = tempScalar
        };
    }
    
    public static Ga401KVector0 Sp(this Ga401Multivector mv1, Ga401KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar += mv1.KVector1.Scalar2 * mv2.Scalar2 + mv1.KVector1.Scalar3 * mv2.Scalar3 + mv1.KVector1.Scalar4 * mv2.Scalar4 + mv1.KVector1.Scalar5 * mv2.Scalar5;
        }
        
        return new Ga401KVector0
        {
            Scalar = tempScalar
        };
    }
    
    public static Ga401KVector0 Sp(this Ga401Multivector mv1, Ga401KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar += -mv1.KVector2.Scalar23 * mv2.Scalar23 - mv1.KVector2.Scalar24 * mv2.Scalar24 - mv1.KVector2.Scalar34 * mv2.Scalar34 - mv1.KVector2.Scalar25 * mv2.Scalar25 - mv1.KVector2.Scalar35 * mv2.Scalar35 - mv1.KVector2.Scalar45 * mv2.Scalar45;
        }
        
        return new Ga401KVector0
        {
            Scalar = tempScalar
        };
    }
    
    public static Ga401KVector0 Sp(this Ga401Multivector mv1, Ga401KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar += -mv1.KVector3.Scalar234 * mv2.Scalar234 - mv1.KVector3.Scalar235 * mv2.Scalar235 - mv1.KVector3.Scalar245 * mv2.Scalar245 - mv1.KVector3.Scalar345 * mv2.Scalar345;
        }
        
        return new Ga401KVector0
        {
            Scalar = tempScalar
        };
    }
    
    public static Ga401KVector0 Sp(this Ga401Multivector mv1, Ga401KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar += mv1.KVector4.Scalar2345 * mv2.Scalar2345;
        }
        
        return new Ga401KVector0
        {
            Scalar = tempScalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga401KVector0 Sp(this Ga401Multivector mv1, Ga401KVector5 mv2)
    {
        return Ga401KVector0.Zero;
    }
    
    public static Ga401KVector0 Sp(this Ga401Multivector mv1, Ga401Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga401KVector0.Zero;
        
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
                tempScalar += mv1.KVector1.Scalar2 * mv2.KVector1.Scalar2 + mv1.KVector1.Scalar3 * mv2.KVector1.Scalar3 + mv1.KVector1.Scalar4 * mv2.KVector1.Scalar4 + mv1.KVector1.Scalar5 * mv2.KVector1.Scalar5;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector2.IsZero())
            {
                tempScalar += -mv1.KVector2.Scalar23 * mv2.KVector2.Scalar23 - mv1.KVector2.Scalar24 * mv2.KVector2.Scalar24 - mv1.KVector2.Scalar34 * mv2.KVector2.Scalar34 - mv1.KVector2.Scalar25 * mv2.KVector2.Scalar25 - mv1.KVector2.Scalar35 * mv2.KVector2.Scalar35 - mv1.KVector2.Scalar45 * mv2.KVector2.Scalar45;
            }
            
        }
        
        if (!mv1.KVector3.IsZero())
        {
            if (!mv2.KVector3.IsZero())
            {
                tempScalar += -mv1.KVector3.Scalar234 * mv2.KVector3.Scalar234 - mv1.KVector3.Scalar235 * mv2.KVector3.Scalar235 - mv1.KVector3.Scalar245 * mv2.KVector3.Scalar245 - mv1.KVector3.Scalar345 * mv2.KVector3.Scalar345;
            }
            
        }
        
        if (!mv1.KVector4.IsZero())
        {
            if (!mv2.KVector4.IsZero())
            {
                tempScalar += mv1.KVector4.Scalar2345 * mv2.KVector4.Scalar2345;
            }
            
        }
        
        return new Ga401KVector0
        {
            Scalar = tempScalar
        };
    }
    
}
