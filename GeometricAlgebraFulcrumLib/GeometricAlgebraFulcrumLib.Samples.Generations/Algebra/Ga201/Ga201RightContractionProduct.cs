using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga201;

public static class Ga201RightContractionProduct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Rcp(this Ga201KVector0 mv1, Ga201KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector0.Zero;
        
        return new Ga201KVector0
        {
            Scalar = mv1.Scalar * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Rcp(this Ga201KVector0 mv1, Ga201KVector1 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Rcp(this Ga201KVector0 mv1, Ga201KVector2 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Rcp(this Ga201KVector0 mv1, Ga201KVector3 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    public static Ga201KVector0 Rcp(this Ga201KVector0 mv1, Ga201Multivector mv2)
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
    public static Ga201KVector1 Rcp(this Ga201KVector1 mv1, Ga201KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector1.Zero;
        
        return new Ga201KVector1
        {
            Scalar1 = mv1.Scalar1 * mv2.Scalar,
            Scalar2 = mv1.Scalar2 * mv2.Scalar,
            Scalar3 = mv1.Scalar3 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Rcp(this Ga201KVector1 mv1, Ga201KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector0.Zero;
        
        return new Ga201KVector0
        {
            Scalar = mv1.Scalar2 * mv2.Scalar2 + mv1.Scalar3 * mv2.Scalar3
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Rcp(this Ga201KVector1 mv1, Ga201KVector2 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Rcp(this Ga201KVector1 mv1, Ga201KVector3 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    public static Ga201Multivector Rcp(this Ga201KVector1 mv1, Ga201Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[1] += mv1.Scalar1 * mv2.KVector0.Scalar;
            tempScalar[2] += mv1.Scalar2 * mv2.KVector0.Scalar;
            tempScalar[4] += mv1.Scalar3 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[0] += mv1.Scalar2 * mv2.KVector1.Scalar2 + mv1.Scalar3 * mv2.KVector1.Scalar3;
        }
        
        return Ga201Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector2 Rcp(this Ga201KVector2 mv1, Ga201KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector2.Zero;
        
        return new Ga201KVector2
        {
            Scalar12 = mv1.Scalar12 * mv2.Scalar,
            Scalar13 = mv1.Scalar13 * mv2.Scalar,
            Scalar23 = mv1.Scalar23 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector1 Rcp(this Ga201KVector2 mv1, Ga201KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector1.Zero;
        
        return new Ga201KVector1
        {
            Scalar1 = mv1.Scalar12 * mv2.Scalar2 + mv1.Scalar13 * mv2.Scalar3,
            Scalar2 = mv1.Scalar23 * mv2.Scalar3,
            Scalar3 = -mv1.Scalar23 * mv2.Scalar2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Rcp(this Ga201KVector2 mv1, Ga201KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector0.Zero;
        
        return new Ga201KVector0
        {
            Scalar = -mv1.Scalar23 * mv2.Scalar23
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Rcp(this Ga201KVector2 mv1, Ga201KVector3 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    public static Ga201Multivector Rcp(this Ga201KVector2 mv1, Ga201Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[3] += mv1.Scalar12 * mv2.KVector0.Scalar;
            tempScalar[5] += mv1.Scalar13 * mv2.KVector0.Scalar;
            tempScalar[6] += mv1.Scalar23 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[1] += mv1.Scalar12 * mv2.KVector1.Scalar2 + mv1.Scalar13 * mv2.KVector1.Scalar3;
            tempScalar[2] += mv1.Scalar23 * mv2.KVector1.Scalar3;
            tempScalar[4] += -mv1.Scalar23 * mv2.KVector1.Scalar2;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[0] += -mv1.Scalar23 * mv2.KVector2.Scalar23;
        }
        
        return Ga201Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector3 Rcp(this Ga201KVector3 mv1, Ga201KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector3.Zero;
        
        return new Ga201KVector3
        {
            Scalar123 = mv1.Scalar123 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector2 Rcp(this Ga201KVector3 mv1, Ga201KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector2.Zero;
        
        return new Ga201KVector2
        {
            Scalar12 = mv1.Scalar123 * mv2.Scalar3,
            Scalar13 = -mv1.Scalar123 * mv2.Scalar2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector1 Rcp(this Ga201KVector3 mv1, Ga201KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201KVector1.Zero;
        
        return new Ga201KVector1
        {
            Scalar1 = -mv1.Scalar123 * mv2.Scalar23
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Rcp(this Ga201KVector3 mv1, Ga201KVector3 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    public static Ga201Multivector Rcp(this Ga201KVector3 mv1, Ga201Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[7] += mv1.Scalar123 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[3] += mv1.Scalar123 * mv2.KVector1.Scalar3;
            tempScalar[5] += -mv1.Scalar123 * mv2.KVector1.Scalar2;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[1] += -mv1.Scalar123 * mv2.KVector2.Scalar23;
        }
        
        return Ga201Multivector.Create(tempScalar);
    }
    
    public static Ga201Multivector Rcp(this Ga201Multivector mv1, Ga201KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += mv1.KVector0.Scalar * mv2.Scalar;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += mv1.KVector1.Scalar1 * mv2.Scalar;
            tempScalar[2] += mv1.KVector1.Scalar2 * mv2.Scalar;
            tempScalar[4] += mv1.KVector1.Scalar3 * mv2.Scalar;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += mv1.KVector2.Scalar12 * mv2.Scalar;
            tempScalar[5] += mv1.KVector2.Scalar13 * mv2.Scalar;
            tempScalar[6] += mv1.KVector2.Scalar23 * mv2.Scalar;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += mv1.KVector3.Scalar123 * mv2.Scalar;
        }
        
        return Ga201Multivector.Create(tempScalar);
    }
    
    public static Ga201Multivector Rcp(this Ga201Multivector mv1, Ga201KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[0] += mv1.KVector1.Scalar2 * mv2.Scalar2 + mv1.KVector1.Scalar3 * mv2.Scalar3;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[1] += mv1.KVector2.Scalar12 * mv2.Scalar2 + mv1.KVector2.Scalar13 * mv2.Scalar3;
            tempScalar[2] += mv1.KVector2.Scalar23 * mv2.Scalar3;
            tempScalar[4] += -mv1.KVector2.Scalar23 * mv2.Scalar2;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[3] += mv1.KVector3.Scalar123 * mv2.Scalar3;
            tempScalar[5] += -mv1.KVector3.Scalar123 * mv2.Scalar2;
        }
        
        return Ga201Multivector.Create(tempScalar);
    }
    
    public static Ga201Multivector Rcp(this Ga201Multivector mv1, Ga201KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[0] += -mv1.KVector2.Scalar23 * mv2.Scalar23;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[1] += -mv1.KVector3.Scalar123 * mv2.Scalar23;
        }
        
        return Ga201Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga201KVector0 Rcp(this Ga201Multivector mv1, Ga201KVector3 mv2)
    {
        return Ga201KVector0.Zero;
    }
    
    public static Ga201Multivector Rcp(this Ga201Multivector mv1, Ga201Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga201Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[0] += mv1.KVector0.Scalar * mv2.KVector0.Scalar;
            }
            
        }
        
        if (!mv1.KVector1.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[1] += mv1.KVector1.Scalar1 * mv2.KVector0.Scalar;
                tempScalar[2] += mv1.KVector1.Scalar2 * mv2.KVector0.Scalar;
                tempScalar[4] += mv1.KVector1.Scalar3 * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[0] += mv1.KVector1.Scalar2 * mv2.KVector1.Scalar2 + mv1.KVector1.Scalar3 * mv2.KVector1.Scalar3;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[3] += mv1.KVector2.Scalar12 * mv2.KVector0.Scalar;
                tempScalar[5] += mv1.KVector2.Scalar13 * mv2.KVector0.Scalar;
                tempScalar[6] += mv1.KVector2.Scalar23 * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[1] += mv1.KVector2.Scalar12 * mv2.KVector1.Scalar2 + mv1.KVector2.Scalar13 * mv2.KVector1.Scalar3;
                tempScalar[2] += mv1.KVector2.Scalar23 * mv2.KVector1.Scalar3;
                tempScalar[4] += -mv1.KVector2.Scalar23 * mv2.KVector1.Scalar2;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[0] += -mv1.KVector2.Scalar23 * mv2.KVector2.Scalar23;
            }
            
        }
        
        if (!mv1.KVector3.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[7] += mv1.KVector3.Scalar123 * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[3] += mv1.KVector3.Scalar123 * mv2.KVector1.Scalar3;
                tempScalar[5] += -mv1.KVector3.Scalar123 * mv2.KVector1.Scalar2;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[1] += -mv1.KVector3.Scalar123 * mv2.KVector2.Scalar23;
            }
            
        }
        
        return Ga201Multivector.Create(tempScalar);
    }
    
}
