using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga21;

public static class Ga21OuterProduct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 Op(this Ga21KVector0 mv1, Ga21KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector0.Zero;
        
        return new Ga21KVector0
        {
            Scalar = mv1.Scalar * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector1 Op(this Ga21KVector0 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector1.Zero;
        
        return new Ga21KVector1
        {
            Scalar1 = mv1.Scalar * mv2.Scalar1,
            Scalar2 = mv1.Scalar * mv2.Scalar2,
            Scalar3 = mv1.Scalar * mv2.Scalar3
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 Op(this Ga21KVector0 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector2.Zero;
        
        return new Ga21KVector2
        {
            Scalar12 = mv1.Scalar * mv2.Scalar12,
            Scalar13 = mv1.Scalar * mv2.Scalar13,
            Scalar23 = mv1.Scalar * mv2.Scalar23
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector3 Op(this Ga21KVector0 mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        return new Ga21KVector3
        {
            Scalar123 = mv1.Scalar * mv2.Scalar123
        };
    }
    
    public static Ga21Multivector Op(this Ga21KVector0 mv1, Ga21Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[0] += mv1.Scalar * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[1] += mv1.Scalar * mv2.KVector1.Scalar1;
            tempScalar[2] += mv1.Scalar * mv2.KVector1.Scalar2;
            tempScalar[4] += mv1.Scalar * mv2.KVector1.Scalar3;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[3] += mv1.Scalar * mv2.KVector2.Scalar12;
            tempScalar[5] += mv1.Scalar * mv2.KVector2.Scalar13;
            tempScalar[6] += mv1.Scalar * mv2.KVector2.Scalar23;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[7] += mv1.Scalar * mv2.KVector3.Scalar123;
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector1 Op(this Ga21KVector1 mv1, Ga21KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector1.Zero;
        
        return new Ga21KVector1
        {
            Scalar1 = mv1.Scalar1 * mv2.Scalar,
            Scalar2 = mv1.Scalar2 * mv2.Scalar,
            Scalar3 = mv1.Scalar3 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 Op(this Ga21KVector1 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector2.Zero;
        
        return new Ga21KVector2
        {
            Scalar12 = mv1.Scalar1 * mv2.Scalar2 - mv1.Scalar2 * mv2.Scalar1,
            Scalar13 = mv1.Scalar1 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar1,
            Scalar23 = mv1.Scalar2 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector3 Op(this Ga21KVector1 mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        return new Ga21KVector3
        {
            Scalar123 = mv1.Scalar1 * mv2.Scalar23 - mv1.Scalar2 * mv2.Scalar13 + mv1.Scalar3 * mv2.Scalar12
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 Op(this Ga21KVector1 mv1, Ga21KVector3 mv2)
    {
        return Ga21KVector0.Zero;
    }
    
    public static Ga21Multivector Op(this Ga21KVector1 mv1, Ga21Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[1] += mv1.Scalar1 * mv2.KVector0.Scalar;
            tempScalar[2] += mv1.Scalar2 * mv2.KVector0.Scalar;
            tempScalar[4] += mv1.Scalar3 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[3] += mv1.Scalar1 * mv2.KVector1.Scalar2 - mv1.Scalar2 * mv2.KVector1.Scalar1;
            tempScalar[5] += mv1.Scalar1 * mv2.KVector1.Scalar3 - mv1.Scalar3 * mv2.KVector1.Scalar1;
            tempScalar[6] += mv1.Scalar2 * mv2.KVector1.Scalar3 - mv1.Scalar3 * mv2.KVector1.Scalar2;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[7] += mv1.Scalar1 * mv2.KVector2.Scalar23 - mv1.Scalar2 * mv2.KVector2.Scalar13 + mv1.Scalar3 * mv2.KVector2.Scalar12;
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector2 Op(this Ga21KVector2 mv1, Ga21KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector2.Zero;
        
        return new Ga21KVector2
        {
            Scalar12 = mv1.Scalar12 * mv2.Scalar,
            Scalar13 = mv1.Scalar13 * mv2.Scalar,
            Scalar23 = mv1.Scalar23 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector3 Op(this Ga21KVector2 mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        return new Ga21KVector3
        {
            Scalar123 = mv1.Scalar12 * mv2.Scalar3 - mv1.Scalar13 * mv2.Scalar2 + mv1.Scalar23 * mv2.Scalar1
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 Op(this Ga21KVector2 mv1, Ga21KVector2 mv2)
    {
        return Ga21KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 Op(this Ga21KVector2 mv1, Ga21KVector3 mv2)
    {
        return Ga21KVector0.Zero;
    }
    
    public static Ga21Multivector Op(this Ga21KVector2 mv1, Ga21Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[3] += mv1.Scalar12 * mv2.KVector0.Scalar;
            tempScalar[5] += mv1.Scalar13 * mv2.KVector0.Scalar;
            tempScalar[6] += mv1.Scalar23 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[7] += mv1.Scalar12 * mv2.KVector1.Scalar3 - mv1.Scalar13 * mv2.KVector1.Scalar2 + mv1.Scalar23 * mv2.KVector1.Scalar1;
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector3 Op(this Ga21KVector3 mv1, Ga21KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        return new Ga21KVector3
        {
            Scalar123 = mv1.Scalar123 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 Op(this Ga21KVector3 mv1, Ga21KVector1 mv2)
    {
        return Ga21KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 Op(this Ga21KVector3 mv1, Ga21KVector2 mv2)
    {
        return Ga21KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga21KVector0 Op(this Ga21KVector3 mv1, Ga21KVector3 mv2)
    {
        return Ga21KVector0.Zero;
    }
    
    public static Ga21KVector3 Op(this Ga21KVector3 mv1, Ga21Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar += mv1.Scalar123 * mv2.KVector0.Scalar;
        }
        
        return new Ga21KVector3
        {
            Scalar123 = tempScalar
        };
    }
    
    public static Ga21Multivector Op(this Ga21Multivector mv1, Ga21KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
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
        
        return Ga21Multivector.Create(tempScalar);
    }
    
    public static Ga21Multivector Op(this Ga21Multivector mv1, Ga21KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[1] += mv1.KVector0.Scalar * mv2.Scalar1;
            tempScalar[2] += mv1.KVector0.Scalar * mv2.Scalar2;
            tempScalar[4] += mv1.KVector0.Scalar * mv2.Scalar3;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[3] += mv1.KVector1.Scalar1 * mv2.Scalar2 - mv1.KVector1.Scalar2 * mv2.Scalar1;
            tempScalar[5] += mv1.KVector1.Scalar1 * mv2.Scalar3 - mv1.KVector1.Scalar3 * mv2.Scalar1;
            tempScalar[6] += mv1.KVector1.Scalar2 * mv2.Scalar3 - mv1.KVector1.Scalar3 * mv2.Scalar2;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[7] += mv1.KVector2.Scalar12 * mv2.Scalar3 - mv1.KVector2.Scalar13 * mv2.Scalar2 + mv1.KVector2.Scalar23 * mv2.Scalar1;
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
    public static Ga21Multivector Op(this Ga21Multivector mv1, Ga21KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[3] += mv1.KVector0.Scalar * mv2.Scalar12;
            tempScalar[5] += mv1.KVector0.Scalar * mv2.Scalar13;
            tempScalar[6] += mv1.KVector0.Scalar * mv2.Scalar23;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[7] += mv1.KVector1.Scalar1 * mv2.Scalar23 - mv1.KVector1.Scalar2 * mv2.Scalar13 + mv1.KVector1.Scalar3 * mv2.Scalar12;
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
    public static Ga21KVector3 Op(this Ga21Multivector mv1, Ga21KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21KVector3.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar += mv1.KVector0.Scalar * mv2.Scalar123;
        }
        
        return new Ga21KVector3
        {
            Scalar123 = tempScalar
        };
    }
    
    public static Ga21Multivector Op(this Ga21Multivector mv1, Ga21Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga21Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector0.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[0] += mv1.KVector0.Scalar * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[1] += mv1.KVector0.Scalar * mv2.KVector1.Scalar1;
                tempScalar[2] += mv1.KVector0.Scalar * mv2.KVector1.Scalar2;
                tempScalar[4] += mv1.KVector0.Scalar * mv2.KVector1.Scalar3;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[3] += mv1.KVector0.Scalar * mv2.KVector2.Scalar12;
                tempScalar[5] += mv1.KVector0.Scalar * mv2.KVector2.Scalar13;
                tempScalar[6] += mv1.KVector0.Scalar * mv2.KVector2.Scalar23;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[7] += mv1.KVector0.Scalar * mv2.KVector3.Scalar123;
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
                tempScalar[3] += mv1.KVector1.Scalar1 * mv2.KVector1.Scalar2 - mv1.KVector1.Scalar2 * mv2.KVector1.Scalar1;
                tempScalar[5] += mv1.KVector1.Scalar1 * mv2.KVector1.Scalar3 - mv1.KVector1.Scalar3 * mv2.KVector1.Scalar1;
                tempScalar[6] += mv1.KVector1.Scalar2 * mv2.KVector1.Scalar3 - mv1.KVector1.Scalar3 * mv2.KVector1.Scalar2;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[7] += mv1.KVector1.Scalar1 * mv2.KVector2.Scalar23 - mv1.KVector1.Scalar2 * mv2.KVector2.Scalar13 + mv1.KVector1.Scalar3 * mv2.KVector2.Scalar12;
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
                tempScalar[7] += mv1.KVector2.Scalar12 * mv2.KVector1.Scalar3 - mv1.KVector2.Scalar13 * mv2.KVector1.Scalar2 + mv1.KVector2.Scalar23 * mv2.KVector1.Scalar1;
            }
            
        }
        
        if (!mv1.KVector3.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[7] += mv1.KVector3.Scalar123 * mv2.KVector0.Scalar;
            }
            
        }
        
        return Ga21Multivector.Create(tempScalar);
    }
    
}
