using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga2;

public static class Ga2OuterProduct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector0 Op(this Ga2KVector0 mv1, Ga2KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector0.Zero;
        
        return new Ga2KVector0
        {
            Scalar = mv1.Scalar * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 Op(this Ga2KVector0 mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector1.Zero;
        
        return new Ga2KVector1
        {
            Scalar1 = mv1.Scalar * mv2.Scalar1,
            Scalar2 = mv1.Scalar * mv2.Scalar2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 Op(this Ga2KVector0 mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        return new Ga2KVector2
        {
            Scalar12 = mv1.Scalar * mv2.Scalar12
        };
    }
    
    public static Ga2Multivector Op(this Ga2KVector0 mv1, Ga2Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2Multivector.Zero;
        
        var tempScalar = new double[4];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[0] += mv1.Scalar * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[1] += mv1.Scalar * mv2.KVector1.Scalar1;
            tempScalar[2] += mv1.Scalar * mv2.KVector1.Scalar2;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[3] += mv1.Scalar * mv2.KVector2.Scalar12;
        }
        
        return Ga2Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector1 Op(this Ga2KVector1 mv1, Ga2KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector1.Zero;
        
        return new Ga2KVector1
        {
            Scalar1 = mv1.Scalar1 * mv2.Scalar,
            Scalar2 = mv1.Scalar2 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 Op(this Ga2KVector1 mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        return new Ga2KVector2
        {
            Scalar12 = mv1.Scalar1 * mv2.Scalar2 - mv1.Scalar2 * mv2.Scalar1
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector0 Op(this Ga2KVector1 mv1, Ga2KVector2 mv2)
    {
        return Ga2KVector0.Zero;
    }
    
    public static Ga2Multivector Op(this Ga2KVector1 mv1, Ga2Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2Multivector.Zero;
        
        var tempScalar = new double[4];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[1] += mv1.Scalar1 * mv2.KVector0.Scalar;
            tempScalar[2] += mv1.Scalar2 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[3] += mv1.Scalar1 * mv2.KVector1.Scalar2 - mv1.Scalar2 * mv2.KVector1.Scalar1;
        }
        
        return Ga2Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector2 Op(this Ga2KVector2 mv1, Ga2KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        return new Ga2KVector2
        {
            Scalar12 = mv1.Scalar12 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector0 Op(this Ga2KVector2 mv1, Ga2KVector1 mv2)
    {
        return Ga2KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga2KVector0 Op(this Ga2KVector2 mv1, Ga2KVector2 mv2)
    {
        return Ga2KVector0.Zero;
    }
    
    public static Ga2KVector2 Op(this Ga2KVector2 mv1, Ga2Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar += mv1.Scalar12 * mv2.KVector0.Scalar;
        }
        
        return new Ga2KVector2
        {
            Scalar12 = tempScalar
        };
    }
    
    public static Ga2Multivector Op(this Ga2Multivector mv1, Ga2KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2Multivector.Zero;
        
        var tempScalar = new double[4];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += mv1.KVector0.Scalar * mv2.Scalar;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += mv1.KVector1.Scalar1 * mv2.Scalar;
            tempScalar[2] += mv1.KVector1.Scalar2 * mv2.Scalar;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += mv1.KVector2.Scalar12 * mv2.Scalar;
        }
        
        return Ga2Multivector.Create(tempScalar);
    }
    
    public static Ga2Multivector Op(this Ga2Multivector mv1, Ga2KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2Multivector.Zero;
        
        var tempScalar = new double[4];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[1] += mv1.KVector0.Scalar * mv2.Scalar1;
            tempScalar[2] += mv1.KVector0.Scalar * mv2.Scalar2;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[3] += mv1.KVector1.Scalar1 * mv2.Scalar2 - mv1.KVector1.Scalar2 * mv2.Scalar1;
        }
        
        return Ga2Multivector.Create(tempScalar);
    }
    
    public static Ga2KVector2 Op(this Ga2Multivector mv1, Ga2KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2KVector2.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar += mv1.KVector0.Scalar * mv2.Scalar12;
        }
        
        return new Ga2KVector2
        {
            Scalar12 = tempScalar
        };
    }
    
    public static Ga2Multivector Op(this Ga2Multivector mv1, Ga2Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga2Multivector.Zero;
        
        var tempScalar = new double[4];
        
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
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[3] += mv1.KVector0.Scalar * mv2.KVector2.Scalar12;
            }
            
        }
        
        if (!mv1.KVector1.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[1] += mv1.KVector1.Scalar1 * mv2.KVector0.Scalar;
                tempScalar[2] += mv1.KVector1.Scalar2 * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[3] += mv1.KVector1.Scalar1 * mv2.KVector1.Scalar2 - mv1.KVector1.Scalar2 * mv2.KVector1.Scalar1;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[3] += mv1.KVector2.Scalar12 * mv2.KVector0.Scalar;
            }
            
        }
        
        return Ga2Multivector.Create(tempScalar);
    }
    
}
