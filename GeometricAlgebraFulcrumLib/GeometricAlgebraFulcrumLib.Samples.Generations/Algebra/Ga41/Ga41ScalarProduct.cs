using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga41;

public static class Ga41ScalarProduct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector0 mv1, Ga41KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        return new Ga41KVector0
        {
            Scalar = mv1.Scalar * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector0 mv1, Ga41KVector1 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector0 mv1, Ga41KVector2 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector0 mv1, Ga41KVector3 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector0 mv1, Ga41KVector4 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector0 mv1, Ga41KVector5 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    public static Ga41KVector0 Sp(this Ga41KVector0 mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar += mv1.Scalar * mv2.KVector0.Scalar;
        }
        
        return new Ga41KVector0
        {
            Scalar = tempScalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector1 mv1, Ga41KVector0 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector1 mv1, Ga41KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        return new Ga41KVector0
        {
            Scalar = -mv1.Scalar1 * mv2.Scalar1 + mv1.Scalar2 * mv2.Scalar2 + mv1.Scalar3 * mv2.Scalar3 + mv1.Scalar4 * mv2.Scalar4 + mv1.Scalar5 * mv2.Scalar5
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector1 mv1, Ga41KVector2 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector1 mv1, Ga41KVector3 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector1 mv1, Ga41KVector4 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector1 mv1, Ga41KVector5 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    public static Ga41KVector0 Sp(this Ga41KVector1 mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar += -mv1.Scalar1 * mv2.KVector1.Scalar1 + mv1.Scalar2 * mv2.KVector1.Scalar2 + mv1.Scalar3 * mv2.KVector1.Scalar3 + mv1.Scalar4 * mv2.KVector1.Scalar4 + mv1.Scalar5 * mv2.KVector1.Scalar5;
        }
        
        return new Ga41KVector0
        {
            Scalar = tempScalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector2 mv1, Ga41KVector0 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector2 mv1, Ga41KVector1 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector2 mv1, Ga41KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        return new Ga41KVector0
        {
            Scalar = mv1.Scalar12 * mv2.Scalar12 + mv1.Scalar13 * mv2.Scalar13 - mv1.Scalar23 * mv2.Scalar23 + mv1.Scalar14 * mv2.Scalar14 - mv1.Scalar24 * mv2.Scalar24 - mv1.Scalar34 * mv2.Scalar34 + mv1.Scalar15 * mv2.Scalar15 - mv1.Scalar25 * mv2.Scalar25 - mv1.Scalar35 * mv2.Scalar35 - mv1.Scalar45 * mv2.Scalar45
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector2 mv1, Ga41KVector3 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector2 mv1, Ga41KVector4 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector2 mv1, Ga41KVector5 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    public static Ga41KVector0 Sp(this Ga41KVector2 mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar += mv1.Scalar12 * mv2.KVector2.Scalar12 + mv1.Scalar13 * mv2.KVector2.Scalar13 - mv1.Scalar23 * mv2.KVector2.Scalar23 + mv1.Scalar14 * mv2.KVector2.Scalar14 - mv1.Scalar24 * mv2.KVector2.Scalar24 - mv1.Scalar34 * mv2.KVector2.Scalar34 + mv1.Scalar15 * mv2.KVector2.Scalar15 - mv1.Scalar25 * mv2.KVector2.Scalar25 - mv1.Scalar35 * mv2.KVector2.Scalar35 - mv1.Scalar45 * mv2.KVector2.Scalar45;
        }
        
        return new Ga41KVector0
        {
            Scalar = tempScalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector3 mv1, Ga41KVector0 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector3 mv1, Ga41KVector1 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector3 mv1, Ga41KVector2 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector3 mv1, Ga41KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        return new Ga41KVector0
        {
            Scalar = mv1.Scalar123 * mv2.Scalar123 + mv1.Scalar124 * mv2.Scalar124 + mv1.Scalar134 * mv2.Scalar134 - mv1.Scalar234 * mv2.Scalar234 + mv1.Scalar125 * mv2.Scalar125 + mv1.Scalar135 * mv2.Scalar135 - mv1.Scalar235 * mv2.Scalar235 + mv1.Scalar145 * mv2.Scalar145 - mv1.Scalar245 * mv2.Scalar245 - mv1.Scalar345 * mv2.Scalar345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector3 mv1, Ga41KVector4 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector3 mv1, Ga41KVector5 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    public static Ga41KVector0 Sp(this Ga41KVector3 mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar += mv1.Scalar123 * mv2.KVector3.Scalar123 + mv1.Scalar124 * mv2.KVector3.Scalar124 + mv1.Scalar134 * mv2.KVector3.Scalar134 - mv1.Scalar234 * mv2.KVector3.Scalar234 + mv1.Scalar125 * mv2.KVector3.Scalar125 + mv1.Scalar135 * mv2.KVector3.Scalar135 - mv1.Scalar235 * mv2.KVector3.Scalar235 + mv1.Scalar145 * mv2.KVector3.Scalar145 - mv1.Scalar245 * mv2.KVector3.Scalar245 - mv1.Scalar345 * mv2.KVector3.Scalar345;
        }
        
        return new Ga41KVector0
        {
            Scalar = tempScalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector4 mv1, Ga41KVector0 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector4 mv1, Ga41KVector1 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector4 mv1, Ga41KVector2 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector4 mv1, Ga41KVector3 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector4 mv1, Ga41KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        return new Ga41KVector0
        {
            Scalar = -mv1.Scalar1234 * mv2.Scalar1234 - mv1.Scalar1235 * mv2.Scalar1235 - mv1.Scalar1245 * mv2.Scalar1245 - mv1.Scalar1345 * mv2.Scalar1345 + mv1.Scalar2345 * mv2.Scalar2345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector4 mv1, Ga41KVector5 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    public static Ga41KVector0 Sp(this Ga41KVector4 mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar += -mv1.Scalar1234 * mv2.KVector4.Scalar1234 - mv1.Scalar1235 * mv2.KVector4.Scalar1235 - mv1.Scalar1245 * mv2.KVector4.Scalar1245 - mv1.Scalar1345 * mv2.KVector4.Scalar1345 + mv1.Scalar2345 * mv2.KVector4.Scalar2345;
        }
        
        return new Ga41KVector0
        {
            Scalar = tempScalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector5 mv1, Ga41KVector0 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector5 mv1, Ga41KVector1 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector5 mv1, Ga41KVector2 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector5 mv1, Ga41KVector3 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector5 mv1, Ga41KVector4 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Sp(this Ga41KVector5 mv1, Ga41KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        return new Ga41KVector0
        {
            Scalar = -mv1.Scalar12345 * mv2.Scalar12345
        };
    }
    
    public static Ga41KVector0 Sp(this Ga41KVector5 mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar += -mv1.Scalar12345 * mv2.KVector5.Scalar12345;
        }
        
        return new Ga41KVector0
        {
            Scalar = tempScalar
        };
    }
    
    public static Ga41KVector0 Sp(this Ga41Multivector mv1, Ga41KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar += mv1.KVector0.Scalar * mv2.Scalar;
        }
        
        return new Ga41KVector0
        {
            Scalar = tempScalar
        };
    }
    
    public static Ga41KVector0 Sp(this Ga41Multivector mv1, Ga41KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar += -mv1.KVector1.Scalar1 * mv2.Scalar1 + mv1.KVector1.Scalar2 * mv2.Scalar2 + mv1.KVector1.Scalar3 * mv2.Scalar3 + mv1.KVector1.Scalar4 * mv2.Scalar4 + mv1.KVector1.Scalar5 * mv2.Scalar5;
        }
        
        return new Ga41KVector0
        {
            Scalar = tempScalar
        };
    }
    
    public static Ga41KVector0 Sp(this Ga41Multivector mv1, Ga41KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar += mv1.KVector2.Scalar12 * mv2.Scalar12 + mv1.KVector2.Scalar13 * mv2.Scalar13 - mv1.KVector2.Scalar23 * mv2.Scalar23 + mv1.KVector2.Scalar14 * mv2.Scalar14 - mv1.KVector2.Scalar24 * mv2.Scalar24 - mv1.KVector2.Scalar34 * mv2.Scalar34 + mv1.KVector2.Scalar15 * mv2.Scalar15 - mv1.KVector2.Scalar25 * mv2.Scalar25 - mv1.KVector2.Scalar35 * mv2.Scalar35 - mv1.KVector2.Scalar45 * mv2.Scalar45;
        }
        
        return new Ga41KVector0
        {
            Scalar = tempScalar
        };
    }
    
    public static Ga41KVector0 Sp(this Ga41Multivector mv1, Ga41KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar += mv1.KVector3.Scalar123 * mv2.Scalar123 + mv1.KVector3.Scalar124 * mv2.Scalar124 + mv1.KVector3.Scalar134 * mv2.Scalar134 - mv1.KVector3.Scalar234 * mv2.Scalar234 + mv1.KVector3.Scalar125 * mv2.Scalar125 + mv1.KVector3.Scalar135 * mv2.Scalar135 - mv1.KVector3.Scalar235 * mv2.Scalar235 + mv1.KVector3.Scalar145 * mv2.Scalar145 - mv1.KVector3.Scalar245 * mv2.Scalar245 - mv1.KVector3.Scalar345 * mv2.Scalar345;
        }
        
        return new Ga41KVector0
        {
            Scalar = tempScalar
        };
    }
    
    public static Ga41KVector0 Sp(this Ga41Multivector mv1, Ga41KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar += -mv1.KVector4.Scalar1234 * mv2.Scalar1234 - mv1.KVector4.Scalar1235 * mv2.Scalar1235 - mv1.KVector4.Scalar1245 * mv2.Scalar1245 - mv1.KVector4.Scalar1345 * mv2.Scalar1345 + mv1.KVector4.Scalar2345 * mv2.Scalar2345;
        }
        
        return new Ga41KVector0
        {
            Scalar = tempScalar
        };
    }
    
    public static Ga41KVector0 Sp(this Ga41Multivector mv1, Ga41KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar += -mv1.KVector5.Scalar12345 * mv2.Scalar12345;
        }
        
        return new Ga41KVector0
        {
            Scalar = tempScalar
        };
    }
    
    public static Ga41KVector0 Sp(this Ga41Multivector mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
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
                tempScalar += -mv1.KVector1.Scalar1 * mv2.KVector1.Scalar1 + mv1.KVector1.Scalar2 * mv2.KVector1.Scalar2 + mv1.KVector1.Scalar3 * mv2.KVector1.Scalar3 + mv1.KVector1.Scalar4 * mv2.KVector1.Scalar4 + mv1.KVector1.Scalar5 * mv2.KVector1.Scalar5;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector2.IsZero())
            {
                tempScalar += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar12 + mv1.KVector2.Scalar13 * mv2.KVector2.Scalar13 - mv1.KVector2.Scalar23 * mv2.KVector2.Scalar23 + mv1.KVector2.Scalar14 * mv2.KVector2.Scalar14 - mv1.KVector2.Scalar24 * mv2.KVector2.Scalar24 - mv1.KVector2.Scalar34 * mv2.KVector2.Scalar34 + mv1.KVector2.Scalar15 * mv2.KVector2.Scalar15 - mv1.KVector2.Scalar25 * mv2.KVector2.Scalar25 - mv1.KVector2.Scalar35 * mv2.KVector2.Scalar35 - mv1.KVector2.Scalar45 * mv2.KVector2.Scalar45;
            }
            
        }
        
        if (!mv1.KVector3.IsZero())
        {
            if (!mv2.KVector3.IsZero())
            {
                tempScalar += mv1.KVector3.Scalar123 * mv2.KVector3.Scalar123 + mv1.KVector3.Scalar124 * mv2.KVector3.Scalar124 + mv1.KVector3.Scalar134 * mv2.KVector3.Scalar134 - mv1.KVector3.Scalar234 * mv2.KVector3.Scalar234 + mv1.KVector3.Scalar125 * mv2.KVector3.Scalar125 + mv1.KVector3.Scalar135 * mv2.KVector3.Scalar135 - mv1.KVector3.Scalar235 * mv2.KVector3.Scalar235 + mv1.KVector3.Scalar145 * mv2.KVector3.Scalar145 - mv1.KVector3.Scalar245 * mv2.KVector3.Scalar245 - mv1.KVector3.Scalar345 * mv2.KVector3.Scalar345;
            }
            
        }
        
        if (!mv1.KVector4.IsZero())
        {
            if (!mv2.KVector4.IsZero())
            {
                tempScalar += -mv1.KVector4.Scalar1234 * mv2.KVector4.Scalar1234 - mv1.KVector4.Scalar1235 * mv2.KVector4.Scalar1235 - mv1.KVector4.Scalar1245 * mv2.KVector4.Scalar1245 - mv1.KVector4.Scalar1345 * mv2.KVector4.Scalar1345 + mv1.KVector4.Scalar2345 * mv2.KVector4.Scalar2345;
            }
            
        }
        
        if (!mv1.KVector5.IsZero())
        {
            if (!mv2.KVector5.IsZero())
            {
                tempScalar += -mv1.KVector5.Scalar12345 * mv2.KVector5.Scalar12345;
            }
            
        }
        
        return new Ga41KVector0
        {
            Scalar = tempScalar
        };
    }
    
}
