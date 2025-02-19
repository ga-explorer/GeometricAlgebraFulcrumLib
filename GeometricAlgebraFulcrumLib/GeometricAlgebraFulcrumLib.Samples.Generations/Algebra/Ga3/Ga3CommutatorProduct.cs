using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga3;

public static class Ga3CommutatorProduct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 Cp(this Ga3KVector0 mv1, Ga3KVector0 mv2)
    {
        return Ga3KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 Cp(this Ga3KVector0 mv1, Ga3KVector1 mv2)
    {
        return Ga3KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 Cp(this Ga3KVector0 mv1, Ga3KVector2 mv2)
    {
        return Ga3KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 Cp(this Ga3KVector0 mv1, Ga3KVector3 mv2)
    {
        return Ga3KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 Cp(this Ga3KVector0 mv1, Ga3Multivector mv2)
    {
        return Ga3KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 Cp(this Ga3KVector1 mv1, Ga3KVector0 mv2)
    {
        return Ga3KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 Cp(this Ga3KVector1 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector2.Zero;
        
        return new Ga3KVector2
        {
            Scalar12 = mv1.Scalar1 * mv2.Scalar2 - mv1.Scalar2 * mv2.Scalar1,
            Scalar13 = mv1.Scalar1 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar1,
            Scalar23 = mv1.Scalar2 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 Cp(this Ga3KVector1 mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector1.Zero;
        
        return new Ga3KVector1
        {
            Scalar1 = -mv1.Scalar2 * mv2.Scalar12 - mv1.Scalar3 * mv2.Scalar13,
            Scalar2 = mv1.Scalar1 * mv2.Scalar12 - mv1.Scalar3 * mv2.Scalar23,
            Scalar3 = mv1.Scalar1 * mv2.Scalar13 + mv1.Scalar2 * mv2.Scalar23
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 Cp(this Ga3KVector1 mv1, Ga3KVector3 mv2)
    {
        return Ga3KVector0.Zero;
    }
    
    public static Ga3Multivector Cp(this Ga3KVector1 mv1, Ga3Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[3] += mv1.Scalar1 * mv2.KVector1.Scalar2 - mv1.Scalar2 * mv2.KVector1.Scalar1;
            tempScalar[5] += mv1.Scalar1 * mv2.KVector1.Scalar3 - mv1.Scalar3 * mv2.KVector1.Scalar1;
            tempScalar[6] += mv1.Scalar2 * mv2.KVector1.Scalar3 - mv1.Scalar3 * mv2.KVector1.Scalar2;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[1] += -mv1.Scalar2 * mv2.KVector2.Scalar12 - mv1.Scalar3 * mv2.KVector2.Scalar13;
            tempScalar[2] += mv1.Scalar1 * mv2.KVector2.Scalar12 - mv1.Scalar3 * mv2.KVector2.Scalar23;
            tempScalar[4] += mv1.Scalar1 * mv2.KVector2.Scalar13 + mv1.Scalar2 * mv2.KVector2.Scalar23;
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 Cp(this Ga3KVector2 mv1, Ga3KVector0 mv2)
    {
        return Ga3KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector1 Cp(this Ga3KVector2 mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector1.Zero;
        
        return new Ga3KVector1
        {
            Scalar1 = mv1.Scalar12 * mv2.Scalar2 + mv1.Scalar13 * mv2.Scalar3,
            Scalar2 = -mv1.Scalar12 * mv2.Scalar1 + mv1.Scalar23 * mv2.Scalar3,
            Scalar3 = -mv1.Scalar13 * mv2.Scalar1 - mv1.Scalar23 * mv2.Scalar2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector2 Cp(this Ga3KVector2 mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3KVector2.Zero;
        
        return new Ga3KVector2
        {
            Scalar12 = -mv1.Scalar13 * mv2.Scalar23 + mv1.Scalar23 * mv2.Scalar13,
            Scalar13 = mv1.Scalar12 * mv2.Scalar23 - mv1.Scalar23 * mv2.Scalar12,
            Scalar23 = -mv1.Scalar12 * mv2.Scalar13 + mv1.Scalar13 * mv2.Scalar12
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 Cp(this Ga3KVector2 mv1, Ga3KVector3 mv2)
    {
        return Ga3KVector0.Zero;
    }
    
    public static Ga3Multivector Cp(this Ga3KVector2 mv1, Ga3Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[1] += mv1.Scalar12 * mv2.KVector1.Scalar2 + mv1.Scalar13 * mv2.KVector1.Scalar3;
            tempScalar[2] += -mv1.Scalar12 * mv2.KVector1.Scalar1 + mv1.Scalar23 * mv2.KVector1.Scalar3;
            tempScalar[4] += -mv1.Scalar13 * mv2.KVector1.Scalar1 - mv1.Scalar23 * mv2.KVector1.Scalar2;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[3] += -mv1.Scalar13 * mv2.KVector2.Scalar23 + mv1.Scalar23 * mv2.KVector2.Scalar13;
            tempScalar[5] += mv1.Scalar12 * mv2.KVector2.Scalar23 - mv1.Scalar23 * mv2.KVector2.Scalar12;
            tempScalar[6] += -mv1.Scalar12 * mv2.KVector2.Scalar13 + mv1.Scalar13 * mv2.KVector2.Scalar12;
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 Cp(this Ga3KVector3 mv1, Ga3KVector0 mv2)
    {
        return Ga3KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 Cp(this Ga3KVector3 mv1, Ga3KVector1 mv2)
    {
        return Ga3KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 Cp(this Ga3KVector3 mv1, Ga3KVector2 mv2)
    {
        return Ga3KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 Cp(this Ga3KVector3 mv1, Ga3KVector3 mv2)
    {
        return Ga3KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 Cp(this Ga3KVector3 mv1, Ga3Multivector mv2)
    {
        return Ga3KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 Cp(this Ga3Multivector mv1, Ga3KVector0 mv2)
    {
        return Ga3KVector0.Zero;
    }
    
    public static Ga3Multivector Cp(this Ga3Multivector mv1, Ga3KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[3] += mv1.KVector1.Scalar1 * mv2.Scalar2 - mv1.KVector1.Scalar2 * mv2.Scalar1;
            tempScalar[5] += mv1.KVector1.Scalar1 * mv2.Scalar3 - mv1.KVector1.Scalar3 * mv2.Scalar1;
            tempScalar[6] += mv1.KVector1.Scalar2 * mv2.Scalar3 - mv1.KVector1.Scalar3 * mv2.Scalar2;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[1] += mv1.KVector2.Scalar12 * mv2.Scalar2 + mv1.KVector2.Scalar13 * mv2.Scalar3;
            tempScalar[2] += -mv1.KVector2.Scalar12 * mv2.Scalar1 + mv1.KVector2.Scalar23 * mv2.Scalar3;
            tempScalar[4] += -mv1.KVector2.Scalar13 * mv2.Scalar1 - mv1.KVector2.Scalar23 * mv2.Scalar2;
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
    public static Ga3Multivector Cp(this Ga3Multivector mv1, Ga3KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += -mv1.KVector1.Scalar2 * mv2.Scalar12 - mv1.KVector1.Scalar3 * mv2.Scalar13;
            tempScalar[2] += mv1.KVector1.Scalar1 * mv2.Scalar12 - mv1.KVector1.Scalar3 * mv2.Scalar23;
            tempScalar[4] += mv1.KVector1.Scalar1 * mv2.Scalar13 + mv1.KVector1.Scalar2 * mv2.Scalar23;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += -mv1.KVector2.Scalar13 * mv2.Scalar23 + mv1.KVector2.Scalar23 * mv2.Scalar13;
            tempScalar[5] += mv1.KVector2.Scalar12 * mv2.Scalar23 - mv1.KVector2.Scalar23 * mv2.Scalar12;
            tempScalar[6] += -mv1.KVector2.Scalar12 * mv2.Scalar13 + mv1.KVector2.Scalar13 * mv2.Scalar12;
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga3KVector0 Cp(this Ga3Multivector mv1, Ga3KVector3 mv2)
    {
        return Ga3KVector0.Zero;
    }
    
    public static Ga3Multivector Cp(this Ga3Multivector mv1, Ga3Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga3Multivector.Zero;
        
        var tempScalar = new double[8];
        
        if (!mv1.KVector1.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[3] += mv1.KVector1.Scalar1 * mv2.KVector1.Scalar2 - mv1.KVector1.Scalar2 * mv2.KVector1.Scalar1;
                tempScalar[5] += mv1.KVector1.Scalar1 * mv2.KVector1.Scalar3 - mv1.KVector1.Scalar3 * mv2.KVector1.Scalar1;
                tempScalar[6] += mv1.KVector1.Scalar2 * mv2.KVector1.Scalar3 - mv1.KVector1.Scalar3 * mv2.KVector1.Scalar2;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[1] += -mv1.KVector1.Scalar2 * mv2.KVector2.Scalar12 - mv1.KVector1.Scalar3 * mv2.KVector2.Scalar13;
                tempScalar[2] += mv1.KVector1.Scalar1 * mv2.KVector2.Scalar12 - mv1.KVector1.Scalar3 * mv2.KVector2.Scalar23;
                tempScalar[4] += mv1.KVector1.Scalar1 * mv2.KVector2.Scalar13 + mv1.KVector1.Scalar2 * mv2.KVector2.Scalar23;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[1] += mv1.KVector2.Scalar12 * mv2.KVector1.Scalar2 + mv1.KVector2.Scalar13 * mv2.KVector1.Scalar3;
                tempScalar[2] += -mv1.KVector2.Scalar12 * mv2.KVector1.Scalar1 + mv1.KVector2.Scalar23 * mv2.KVector1.Scalar3;
                tempScalar[4] += -mv1.KVector2.Scalar13 * mv2.KVector1.Scalar1 - mv1.KVector2.Scalar23 * mv2.KVector1.Scalar2;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[3] += -mv1.KVector2.Scalar13 * mv2.KVector2.Scalar23 + mv1.KVector2.Scalar23 * mv2.KVector2.Scalar13;
                tempScalar[5] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar23 - mv1.KVector2.Scalar23 * mv2.KVector2.Scalar12;
                tempScalar[6] += -mv1.KVector2.Scalar12 * mv2.KVector2.Scalar13 + mv1.KVector2.Scalar13 * mv2.KVector2.Scalar12;
            }
            
        }
        
        return Ga3Multivector.Create(tempScalar);
    }
    
}
