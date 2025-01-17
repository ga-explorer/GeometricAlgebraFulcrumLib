using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga31;

public static class Ga31CommutatorProduct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Cp(this Ga31KVector0 mv1, Ga31KVector0 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Cp(this Ga31KVector0 mv1, Ga31KVector1 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Cp(this Ga31KVector0 mv1, Ga31KVector2 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Cp(this Ga31KVector0 mv1, Ga31KVector3 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Cp(this Ga31KVector0 mv1, Ga31KVector4 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Cp(this Ga31KVector0 mv1, Ga31Multivector mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Cp(this Ga31KVector1 mv1, Ga31KVector0 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector2 Cp(this Ga31KVector1 mv1, Ga31KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector2.Zero;
        
        return new Ga31KVector2
        {
            Scalar12 = mv1.Scalar1 * mv2.Scalar2 - mv1.Scalar2 * mv2.Scalar1,
            Scalar13 = mv1.Scalar1 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar1,
            Scalar23 = mv1.Scalar2 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar2,
            Scalar14 = mv1.Scalar1 * mv2.Scalar4 - mv1.Scalar4 * mv2.Scalar1,
            Scalar24 = mv1.Scalar2 * mv2.Scalar4 - mv1.Scalar4 * mv2.Scalar2,
            Scalar34 = mv1.Scalar3 * mv2.Scalar4 - mv1.Scalar4 * mv2.Scalar3
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector1 Cp(this Ga31KVector1 mv1, Ga31KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector1.Zero;
        
        return new Ga31KVector1
        {
            Scalar1 = -mv1.Scalar2 * mv2.Scalar12 - mv1.Scalar3 * mv2.Scalar13 - mv1.Scalar4 * mv2.Scalar14,
            Scalar2 = -mv1.Scalar1 * mv2.Scalar12 - mv1.Scalar3 * mv2.Scalar23 - mv1.Scalar4 * mv2.Scalar24,
            Scalar3 = -mv1.Scalar1 * mv2.Scalar13 + mv1.Scalar2 * mv2.Scalar23 - mv1.Scalar4 * mv2.Scalar34,
            Scalar4 = -mv1.Scalar1 * mv2.Scalar14 + mv1.Scalar2 * mv2.Scalar24 + mv1.Scalar3 * mv2.Scalar34
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector4 Cp(this Ga31KVector1 mv1, Ga31KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector4.Zero;
        
        return new Ga31KVector4
        {
            Scalar1234 = mv1.Scalar1 * mv2.Scalar234 - mv1.Scalar2 * mv2.Scalar134 + mv1.Scalar3 * mv2.Scalar124 - mv1.Scalar4 * mv2.Scalar123
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector3 Cp(this Ga31KVector1 mv1, Ga31KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector3.Zero;
        
        return new Ga31KVector3
        {
            Scalar123 = -mv1.Scalar4 * mv2.Scalar1234,
            Scalar124 = mv1.Scalar3 * mv2.Scalar1234,
            Scalar134 = -mv1.Scalar2 * mv2.Scalar1234,
            Scalar234 = -mv1.Scalar1 * mv2.Scalar1234
        };
    }
    
    public static Ga31Multivector Cp(this Ga31KVector1 mv1, Ga31Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[3] += mv1.Scalar1 * mv2.KVector1.Scalar2 - mv1.Scalar2 * mv2.KVector1.Scalar1;
            tempScalar[5] += mv1.Scalar1 * mv2.KVector1.Scalar3 - mv1.Scalar3 * mv2.KVector1.Scalar1;
            tempScalar[6] += mv1.Scalar2 * mv2.KVector1.Scalar3 - mv1.Scalar3 * mv2.KVector1.Scalar2;
            tempScalar[9] += mv1.Scalar1 * mv2.KVector1.Scalar4 - mv1.Scalar4 * mv2.KVector1.Scalar1;
            tempScalar[10] += mv1.Scalar2 * mv2.KVector1.Scalar4 - mv1.Scalar4 * mv2.KVector1.Scalar2;
            tempScalar[12] += mv1.Scalar3 * mv2.KVector1.Scalar4 - mv1.Scalar4 * mv2.KVector1.Scalar3;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[1] += -mv1.Scalar2 * mv2.KVector2.Scalar12 - mv1.Scalar3 * mv2.KVector2.Scalar13 - mv1.Scalar4 * mv2.KVector2.Scalar14;
            tempScalar[2] += -mv1.Scalar1 * mv2.KVector2.Scalar12 - mv1.Scalar3 * mv2.KVector2.Scalar23 - mv1.Scalar4 * mv2.KVector2.Scalar24;
            tempScalar[4] += -mv1.Scalar1 * mv2.KVector2.Scalar13 + mv1.Scalar2 * mv2.KVector2.Scalar23 - mv1.Scalar4 * mv2.KVector2.Scalar34;
            tempScalar[8] += -mv1.Scalar1 * mv2.KVector2.Scalar14 + mv1.Scalar2 * mv2.KVector2.Scalar24 + mv1.Scalar3 * mv2.KVector2.Scalar34;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[15] += mv1.Scalar1 * mv2.KVector3.Scalar234 - mv1.Scalar2 * mv2.KVector3.Scalar134 + mv1.Scalar3 * mv2.KVector3.Scalar124 - mv1.Scalar4 * mv2.KVector3.Scalar123;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[7] += -mv1.Scalar4 * mv2.KVector4.Scalar1234;
            tempScalar[11] += mv1.Scalar3 * mv2.KVector4.Scalar1234;
            tempScalar[13] += -mv1.Scalar2 * mv2.KVector4.Scalar1234;
            tempScalar[14] += -mv1.Scalar1 * mv2.KVector4.Scalar1234;
        }
        
        return Ga31Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Cp(this Ga31KVector2 mv1, Ga31KVector0 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector1 Cp(this Ga31KVector2 mv1, Ga31KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector1.Zero;
        
        return new Ga31KVector1
        {
            Scalar1 = mv1.Scalar12 * mv2.Scalar2 + mv1.Scalar13 * mv2.Scalar3 + mv1.Scalar14 * mv2.Scalar4,
            Scalar2 = mv1.Scalar12 * mv2.Scalar1 + mv1.Scalar23 * mv2.Scalar3 + mv1.Scalar24 * mv2.Scalar4,
            Scalar3 = mv1.Scalar13 * mv2.Scalar1 - mv1.Scalar23 * mv2.Scalar2 + mv1.Scalar34 * mv2.Scalar4,
            Scalar4 = mv1.Scalar14 * mv2.Scalar1 - mv1.Scalar24 * mv2.Scalar2 - mv1.Scalar34 * mv2.Scalar3
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector2 Cp(this Ga31KVector2 mv1, Ga31KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector2.Zero;
        
        return new Ga31KVector2
        {
            Scalar12 = -mv1.Scalar13 * mv2.Scalar23 + mv1.Scalar23 * mv2.Scalar13 - mv1.Scalar14 * mv2.Scalar24 + mv1.Scalar24 * mv2.Scalar14,
            Scalar13 = mv1.Scalar12 * mv2.Scalar23 - mv1.Scalar23 * mv2.Scalar12 - mv1.Scalar14 * mv2.Scalar34 + mv1.Scalar34 * mv2.Scalar14,
            Scalar23 = mv1.Scalar12 * mv2.Scalar13 - mv1.Scalar13 * mv2.Scalar12 - mv1.Scalar24 * mv2.Scalar34 + mv1.Scalar34 * mv2.Scalar24,
            Scalar14 = mv1.Scalar12 * mv2.Scalar24 + mv1.Scalar13 * mv2.Scalar34 - mv1.Scalar24 * mv2.Scalar12 - mv1.Scalar34 * mv2.Scalar13,
            Scalar24 = mv1.Scalar12 * mv2.Scalar14 + mv1.Scalar23 * mv2.Scalar34 - mv1.Scalar14 * mv2.Scalar12 - mv1.Scalar34 * mv2.Scalar23,
            Scalar34 = mv1.Scalar13 * mv2.Scalar14 - mv1.Scalar23 * mv2.Scalar24 - mv1.Scalar14 * mv2.Scalar13 + mv1.Scalar24 * mv2.Scalar23
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector3 Cp(this Ga31KVector2 mv1, Ga31KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector3.Zero;
        
        return new Ga31KVector3
        {
            Scalar123 = mv1.Scalar14 * mv2.Scalar234 - mv1.Scalar24 * mv2.Scalar134 + mv1.Scalar34 * mv2.Scalar124,
            Scalar124 = -mv1.Scalar13 * mv2.Scalar234 + mv1.Scalar23 * mv2.Scalar134 - mv1.Scalar34 * mv2.Scalar123,
            Scalar134 = mv1.Scalar12 * mv2.Scalar234 - mv1.Scalar23 * mv2.Scalar124 + mv1.Scalar24 * mv2.Scalar123,
            Scalar234 = mv1.Scalar12 * mv2.Scalar134 - mv1.Scalar13 * mv2.Scalar124 + mv1.Scalar14 * mv2.Scalar123
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Cp(this Ga31KVector2 mv1, Ga31KVector4 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    public static Ga31Multivector Cp(this Ga31KVector2 mv1, Ga31Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[1] += mv1.Scalar12 * mv2.KVector1.Scalar2 + mv1.Scalar13 * mv2.KVector1.Scalar3 + mv1.Scalar14 * mv2.KVector1.Scalar4;
            tempScalar[2] += mv1.Scalar12 * mv2.KVector1.Scalar1 + mv1.Scalar23 * mv2.KVector1.Scalar3 + mv1.Scalar24 * mv2.KVector1.Scalar4;
            tempScalar[4] += mv1.Scalar13 * mv2.KVector1.Scalar1 - mv1.Scalar23 * mv2.KVector1.Scalar2 + mv1.Scalar34 * mv2.KVector1.Scalar4;
            tempScalar[8] += mv1.Scalar14 * mv2.KVector1.Scalar1 - mv1.Scalar24 * mv2.KVector1.Scalar2 - mv1.Scalar34 * mv2.KVector1.Scalar3;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[3] += -mv1.Scalar13 * mv2.KVector2.Scalar23 + mv1.Scalar23 * mv2.KVector2.Scalar13 - mv1.Scalar14 * mv2.KVector2.Scalar24 + mv1.Scalar24 * mv2.KVector2.Scalar14;
            tempScalar[5] += mv1.Scalar12 * mv2.KVector2.Scalar23 - mv1.Scalar23 * mv2.KVector2.Scalar12 - mv1.Scalar14 * mv2.KVector2.Scalar34 + mv1.Scalar34 * mv2.KVector2.Scalar14;
            tempScalar[6] += mv1.Scalar12 * mv2.KVector2.Scalar13 - mv1.Scalar13 * mv2.KVector2.Scalar12 - mv1.Scalar24 * mv2.KVector2.Scalar34 + mv1.Scalar34 * mv2.KVector2.Scalar24;
            tempScalar[9] += mv1.Scalar12 * mv2.KVector2.Scalar24 + mv1.Scalar13 * mv2.KVector2.Scalar34 - mv1.Scalar24 * mv2.KVector2.Scalar12 - mv1.Scalar34 * mv2.KVector2.Scalar13;
            tempScalar[10] += mv1.Scalar12 * mv2.KVector2.Scalar14 + mv1.Scalar23 * mv2.KVector2.Scalar34 - mv1.Scalar14 * mv2.KVector2.Scalar12 - mv1.Scalar34 * mv2.KVector2.Scalar23;
            tempScalar[12] += mv1.Scalar13 * mv2.KVector2.Scalar14 - mv1.Scalar23 * mv2.KVector2.Scalar24 - mv1.Scalar14 * mv2.KVector2.Scalar13 + mv1.Scalar24 * mv2.KVector2.Scalar23;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[7] += mv1.Scalar14 * mv2.KVector3.Scalar234 - mv1.Scalar24 * mv2.KVector3.Scalar134 + mv1.Scalar34 * mv2.KVector3.Scalar124;
            tempScalar[11] += -mv1.Scalar13 * mv2.KVector3.Scalar234 + mv1.Scalar23 * mv2.KVector3.Scalar134 - mv1.Scalar34 * mv2.KVector3.Scalar123;
            tempScalar[13] += mv1.Scalar12 * mv2.KVector3.Scalar234 - mv1.Scalar23 * mv2.KVector3.Scalar124 + mv1.Scalar24 * mv2.KVector3.Scalar123;
            tempScalar[14] += mv1.Scalar12 * mv2.KVector3.Scalar134 - mv1.Scalar13 * mv2.KVector3.Scalar124 + mv1.Scalar14 * mv2.KVector3.Scalar123;
        }
        
        return Ga31Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Cp(this Ga31KVector3 mv1, Ga31KVector0 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector4 Cp(this Ga31KVector3 mv1, Ga31KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector4.Zero;
        
        return new Ga31KVector4
        {
            Scalar1234 = mv1.Scalar123 * mv2.Scalar4 - mv1.Scalar124 * mv2.Scalar3 + mv1.Scalar134 * mv2.Scalar2 - mv1.Scalar234 * mv2.Scalar1
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector3 Cp(this Ga31KVector3 mv1, Ga31KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector3.Zero;
        
        return new Ga31KVector3
        {
            Scalar123 = -mv1.Scalar124 * mv2.Scalar34 + mv1.Scalar134 * mv2.Scalar24 - mv1.Scalar234 * mv2.Scalar14,
            Scalar124 = mv1.Scalar123 * mv2.Scalar34 - mv1.Scalar134 * mv2.Scalar23 + mv1.Scalar234 * mv2.Scalar13,
            Scalar134 = -mv1.Scalar123 * mv2.Scalar24 + mv1.Scalar124 * mv2.Scalar23 - mv1.Scalar234 * mv2.Scalar12,
            Scalar234 = -mv1.Scalar123 * mv2.Scalar14 + mv1.Scalar124 * mv2.Scalar13 - mv1.Scalar134 * mv2.Scalar12
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector2 Cp(this Ga31KVector3 mv1, Ga31KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector2.Zero;
        
        return new Ga31KVector2
        {
            Scalar12 = -mv1.Scalar134 * mv2.Scalar234 + mv1.Scalar234 * mv2.Scalar134,
            Scalar13 = mv1.Scalar124 * mv2.Scalar234 - mv1.Scalar234 * mv2.Scalar124,
            Scalar23 = mv1.Scalar124 * mv2.Scalar134 - mv1.Scalar134 * mv2.Scalar124,
            Scalar14 = -mv1.Scalar123 * mv2.Scalar234 + mv1.Scalar234 * mv2.Scalar123,
            Scalar24 = -mv1.Scalar123 * mv2.Scalar134 + mv1.Scalar134 * mv2.Scalar123,
            Scalar34 = mv1.Scalar123 * mv2.Scalar124 - mv1.Scalar124 * mv2.Scalar123
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector1 Cp(this Ga31KVector3 mv1, Ga31KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector1.Zero;
        
        return new Ga31KVector1
        {
            Scalar1 = mv1.Scalar234 * mv2.Scalar1234,
            Scalar2 = mv1.Scalar134 * mv2.Scalar1234,
            Scalar3 = -mv1.Scalar124 * mv2.Scalar1234,
            Scalar4 = mv1.Scalar123 * mv2.Scalar1234
        };
    }
    
    public static Ga31Multivector Cp(this Ga31KVector3 mv1, Ga31Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[15] += mv1.Scalar123 * mv2.KVector1.Scalar4 - mv1.Scalar124 * mv2.KVector1.Scalar3 + mv1.Scalar134 * mv2.KVector1.Scalar2 - mv1.Scalar234 * mv2.KVector1.Scalar1;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[7] += -mv1.Scalar124 * mv2.KVector2.Scalar34 + mv1.Scalar134 * mv2.KVector2.Scalar24 - mv1.Scalar234 * mv2.KVector2.Scalar14;
            tempScalar[11] += mv1.Scalar123 * mv2.KVector2.Scalar34 - mv1.Scalar134 * mv2.KVector2.Scalar23 + mv1.Scalar234 * mv2.KVector2.Scalar13;
            tempScalar[13] += -mv1.Scalar123 * mv2.KVector2.Scalar24 + mv1.Scalar124 * mv2.KVector2.Scalar23 - mv1.Scalar234 * mv2.KVector2.Scalar12;
            tempScalar[14] += -mv1.Scalar123 * mv2.KVector2.Scalar14 + mv1.Scalar124 * mv2.KVector2.Scalar13 - mv1.Scalar134 * mv2.KVector2.Scalar12;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[3] += -mv1.Scalar134 * mv2.KVector3.Scalar234 + mv1.Scalar234 * mv2.KVector3.Scalar134;
            tempScalar[5] += mv1.Scalar124 * mv2.KVector3.Scalar234 - mv1.Scalar234 * mv2.KVector3.Scalar124;
            tempScalar[6] += mv1.Scalar124 * mv2.KVector3.Scalar134 - mv1.Scalar134 * mv2.KVector3.Scalar124;
            tempScalar[9] += -mv1.Scalar123 * mv2.KVector3.Scalar234 + mv1.Scalar234 * mv2.KVector3.Scalar123;
            tempScalar[10] += -mv1.Scalar123 * mv2.KVector3.Scalar134 + mv1.Scalar134 * mv2.KVector3.Scalar123;
            tempScalar[12] += mv1.Scalar123 * mv2.KVector3.Scalar124 - mv1.Scalar124 * mv2.KVector3.Scalar123;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[1] += mv1.Scalar234 * mv2.KVector4.Scalar1234;
            tempScalar[2] += mv1.Scalar134 * mv2.KVector4.Scalar1234;
            tempScalar[4] += -mv1.Scalar124 * mv2.KVector4.Scalar1234;
            tempScalar[8] += mv1.Scalar123 * mv2.KVector4.Scalar1234;
        }
        
        return Ga31Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Cp(this Ga31KVector4 mv1, Ga31KVector0 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector3 Cp(this Ga31KVector4 mv1, Ga31KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector3.Zero;
        
        return new Ga31KVector3
        {
            Scalar123 = mv1.Scalar1234 * mv2.Scalar4,
            Scalar124 = -mv1.Scalar1234 * mv2.Scalar3,
            Scalar134 = mv1.Scalar1234 * mv2.Scalar2,
            Scalar234 = mv1.Scalar1234 * mv2.Scalar1
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Cp(this Ga31KVector4 mv1, Ga31KVector2 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector1 Cp(this Ga31KVector4 mv1, Ga31KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector1.Zero;
        
        return new Ga31KVector1
        {
            Scalar1 = -mv1.Scalar1234 * mv2.Scalar234,
            Scalar2 = -mv1.Scalar1234 * mv2.Scalar134,
            Scalar3 = mv1.Scalar1234 * mv2.Scalar124,
            Scalar4 = -mv1.Scalar1234 * mv2.Scalar123
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Cp(this Ga31KVector4 mv1, Ga31KVector4 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    public static Ga31Multivector Cp(this Ga31KVector4 mv1, Ga31Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[7] += mv1.Scalar1234 * mv2.KVector1.Scalar4;
            tempScalar[11] += -mv1.Scalar1234 * mv2.KVector1.Scalar3;
            tempScalar[13] += mv1.Scalar1234 * mv2.KVector1.Scalar2;
            tempScalar[14] += mv1.Scalar1234 * mv2.KVector1.Scalar1;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[1] += -mv1.Scalar1234 * mv2.KVector3.Scalar234;
            tempScalar[2] += -mv1.Scalar1234 * mv2.KVector3.Scalar134;
            tempScalar[4] += mv1.Scalar1234 * mv2.KVector3.Scalar124;
            tempScalar[8] += -mv1.Scalar1234 * mv2.KVector3.Scalar123;
        }
        
        return Ga31Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Cp(this Ga31Multivector mv1, Ga31KVector0 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    public static Ga31Multivector Cp(this Ga31Multivector mv1, Ga31KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[3] += mv1.KVector1.Scalar1 * mv2.Scalar2 - mv1.KVector1.Scalar2 * mv2.Scalar1;
            tempScalar[5] += mv1.KVector1.Scalar1 * mv2.Scalar3 - mv1.KVector1.Scalar3 * mv2.Scalar1;
            tempScalar[6] += mv1.KVector1.Scalar2 * mv2.Scalar3 - mv1.KVector1.Scalar3 * mv2.Scalar2;
            tempScalar[9] += mv1.KVector1.Scalar1 * mv2.Scalar4 - mv1.KVector1.Scalar4 * mv2.Scalar1;
            tempScalar[10] += mv1.KVector1.Scalar2 * mv2.Scalar4 - mv1.KVector1.Scalar4 * mv2.Scalar2;
            tempScalar[12] += mv1.KVector1.Scalar3 * mv2.Scalar4 - mv1.KVector1.Scalar4 * mv2.Scalar3;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[1] += mv1.KVector2.Scalar12 * mv2.Scalar2 + mv1.KVector2.Scalar13 * mv2.Scalar3 + mv1.KVector2.Scalar14 * mv2.Scalar4;
            tempScalar[2] += mv1.KVector2.Scalar12 * mv2.Scalar1 + mv1.KVector2.Scalar23 * mv2.Scalar3 + mv1.KVector2.Scalar24 * mv2.Scalar4;
            tempScalar[4] += mv1.KVector2.Scalar13 * mv2.Scalar1 - mv1.KVector2.Scalar23 * mv2.Scalar2 + mv1.KVector2.Scalar34 * mv2.Scalar4;
            tempScalar[8] += mv1.KVector2.Scalar14 * mv2.Scalar1 - mv1.KVector2.Scalar24 * mv2.Scalar2 - mv1.KVector2.Scalar34 * mv2.Scalar3;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[15] += mv1.KVector3.Scalar123 * mv2.Scalar4 - mv1.KVector3.Scalar124 * mv2.Scalar3 + mv1.KVector3.Scalar134 * mv2.Scalar2 - mv1.KVector3.Scalar234 * mv2.Scalar1;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[7] += mv1.KVector4.Scalar1234 * mv2.Scalar4;
            tempScalar[11] += -mv1.KVector4.Scalar1234 * mv2.Scalar3;
            tempScalar[13] += mv1.KVector4.Scalar1234 * mv2.Scalar2;
            tempScalar[14] += mv1.KVector4.Scalar1234 * mv2.Scalar1;
        }
        
        return Ga31Multivector.Create(tempScalar);
    }
    
    public static Ga31Multivector Cp(this Ga31Multivector mv1, Ga31KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += -mv1.KVector1.Scalar2 * mv2.Scalar12 - mv1.KVector1.Scalar3 * mv2.Scalar13 - mv1.KVector1.Scalar4 * mv2.Scalar14;
            tempScalar[2] += -mv1.KVector1.Scalar1 * mv2.Scalar12 - mv1.KVector1.Scalar3 * mv2.Scalar23 - mv1.KVector1.Scalar4 * mv2.Scalar24;
            tempScalar[4] += -mv1.KVector1.Scalar1 * mv2.Scalar13 + mv1.KVector1.Scalar2 * mv2.Scalar23 - mv1.KVector1.Scalar4 * mv2.Scalar34;
            tempScalar[8] += -mv1.KVector1.Scalar1 * mv2.Scalar14 + mv1.KVector1.Scalar2 * mv2.Scalar24 + mv1.KVector1.Scalar3 * mv2.Scalar34;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += -mv1.KVector2.Scalar13 * mv2.Scalar23 + mv1.KVector2.Scalar23 * mv2.Scalar13 - mv1.KVector2.Scalar14 * mv2.Scalar24 + mv1.KVector2.Scalar24 * mv2.Scalar14;
            tempScalar[5] += mv1.KVector2.Scalar12 * mv2.Scalar23 - mv1.KVector2.Scalar23 * mv2.Scalar12 - mv1.KVector2.Scalar14 * mv2.Scalar34 + mv1.KVector2.Scalar34 * mv2.Scalar14;
            tempScalar[6] += mv1.KVector2.Scalar12 * mv2.Scalar13 - mv1.KVector2.Scalar13 * mv2.Scalar12 - mv1.KVector2.Scalar24 * mv2.Scalar34 + mv1.KVector2.Scalar34 * mv2.Scalar24;
            tempScalar[9] += mv1.KVector2.Scalar12 * mv2.Scalar24 + mv1.KVector2.Scalar13 * mv2.Scalar34 - mv1.KVector2.Scalar24 * mv2.Scalar12 - mv1.KVector2.Scalar34 * mv2.Scalar13;
            tempScalar[10] += mv1.KVector2.Scalar12 * mv2.Scalar14 + mv1.KVector2.Scalar23 * mv2.Scalar34 - mv1.KVector2.Scalar14 * mv2.Scalar12 - mv1.KVector2.Scalar34 * mv2.Scalar23;
            tempScalar[12] += mv1.KVector2.Scalar13 * mv2.Scalar14 - mv1.KVector2.Scalar23 * mv2.Scalar24 - mv1.KVector2.Scalar14 * mv2.Scalar13 + mv1.KVector2.Scalar24 * mv2.Scalar23;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += -mv1.KVector3.Scalar124 * mv2.Scalar34 + mv1.KVector3.Scalar134 * mv2.Scalar24 - mv1.KVector3.Scalar234 * mv2.Scalar14;
            tempScalar[11] += mv1.KVector3.Scalar123 * mv2.Scalar34 - mv1.KVector3.Scalar134 * mv2.Scalar23 + mv1.KVector3.Scalar234 * mv2.Scalar13;
            tempScalar[13] += -mv1.KVector3.Scalar123 * mv2.Scalar24 + mv1.KVector3.Scalar124 * mv2.Scalar23 - mv1.KVector3.Scalar234 * mv2.Scalar12;
            tempScalar[14] += -mv1.KVector3.Scalar123 * mv2.Scalar14 + mv1.KVector3.Scalar124 * mv2.Scalar13 - mv1.KVector3.Scalar134 * mv2.Scalar12;
        }
        
        return Ga31Multivector.Create(tempScalar);
    }
    
    public static Ga31Multivector Cp(this Ga31Multivector mv1, Ga31KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[15] += mv1.KVector1.Scalar1 * mv2.Scalar234 - mv1.KVector1.Scalar2 * mv2.Scalar134 + mv1.KVector1.Scalar3 * mv2.Scalar124 - mv1.KVector1.Scalar4 * mv2.Scalar123;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[7] += mv1.KVector2.Scalar14 * mv2.Scalar234 - mv1.KVector2.Scalar24 * mv2.Scalar134 + mv1.KVector2.Scalar34 * mv2.Scalar124;
            tempScalar[11] += -mv1.KVector2.Scalar13 * mv2.Scalar234 + mv1.KVector2.Scalar23 * mv2.Scalar134 - mv1.KVector2.Scalar34 * mv2.Scalar123;
            tempScalar[13] += mv1.KVector2.Scalar12 * mv2.Scalar234 - mv1.KVector2.Scalar23 * mv2.Scalar124 + mv1.KVector2.Scalar24 * mv2.Scalar123;
            tempScalar[14] += mv1.KVector2.Scalar12 * mv2.Scalar134 - mv1.KVector2.Scalar13 * mv2.Scalar124 + mv1.KVector2.Scalar14 * mv2.Scalar123;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[3] += -mv1.KVector3.Scalar134 * mv2.Scalar234 + mv1.KVector3.Scalar234 * mv2.Scalar134;
            tempScalar[5] += mv1.KVector3.Scalar124 * mv2.Scalar234 - mv1.KVector3.Scalar234 * mv2.Scalar124;
            tempScalar[6] += mv1.KVector3.Scalar124 * mv2.Scalar134 - mv1.KVector3.Scalar134 * mv2.Scalar124;
            tempScalar[9] += -mv1.KVector3.Scalar123 * mv2.Scalar234 + mv1.KVector3.Scalar234 * mv2.Scalar123;
            tempScalar[10] += -mv1.KVector3.Scalar123 * mv2.Scalar134 + mv1.KVector3.Scalar134 * mv2.Scalar123;
            tempScalar[12] += mv1.KVector3.Scalar123 * mv2.Scalar124 - mv1.KVector3.Scalar124 * mv2.Scalar123;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[1] += -mv1.KVector4.Scalar1234 * mv2.Scalar234;
            tempScalar[2] += -mv1.KVector4.Scalar1234 * mv2.Scalar134;
            tempScalar[4] += mv1.KVector4.Scalar1234 * mv2.Scalar124;
            tempScalar[8] += -mv1.KVector4.Scalar1234 * mv2.Scalar123;
        }
        
        return Ga31Multivector.Create(tempScalar);
    }
    
    public static Ga31Multivector Cp(this Ga31Multivector mv1, Ga31KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[7] += -mv1.KVector1.Scalar4 * mv2.Scalar1234;
            tempScalar[11] += mv1.KVector1.Scalar3 * mv2.Scalar1234;
            tempScalar[13] += -mv1.KVector1.Scalar2 * mv2.Scalar1234;
            tempScalar[14] += -mv1.KVector1.Scalar1 * mv2.Scalar1234;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[1] += mv1.KVector3.Scalar234 * mv2.Scalar1234;
            tempScalar[2] += mv1.KVector3.Scalar134 * mv2.Scalar1234;
            tempScalar[4] += -mv1.KVector3.Scalar124 * mv2.Scalar1234;
            tempScalar[8] += mv1.KVector3.Scalar123 * mv2.Scalar1234;
        }
        
        return Ga31Multivector.Create(tempScalar);
    }
    
    public static Ga31Multivector Cp(this Ga31Multivector mv1, Ga31Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector1.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[3] += mv1.KVector1.Scalar1 * mv2.KVector1.Scalar2 - mv1.KVector1.Scalar2 * mv2.KVector1.Scalar1;
                tempScalar[5] += mv1.KVector1.Scalar1 * mv2.KVector1.Scalar3 - mv1.KVector1.Scalar3 * mv2.KVector1.Scalar1;
                tempScalar[6] += mv1.KVector1.Scalar2 * mv2.KVector1.Scalar3 - mv1.KVector1.Scalar3 * mv2.KVector1.Scalar2;
                tempScalar[9] += mv1.KVector1.Scalar1 * mv2.KVector1.Scalar4 - mv1.KVector1.Scalar4 * mv2.KVector1.Scalar1;
                tempScalar[10] += mv1.KVector1.Scalar2 * mv2.KVector1.Scalar4 - mv1.KVector1.Scalar4 * mv2.KVector1.Scalar2;
                tempScalar[12] += mv1.KVector1.Scalar3 * mv2.KVector1.Scalar4 - mv1.KVector1.Scalar4 * mv2.KVector1.Scalar3;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[1] += -mv1.KVector1.Scalar2 * mv2.KVector2.Scalar12 - mv1.KVector1.Scalar3 * mv2.KVector2.Scalar13 - mv1.KVector1.Scalar4 * mv2.KVector2.Scalar14;
                tempScalar[2] += -mv1.KVector1.Scalar1 * mv2.KVector2.Scalar12 - mv1.KVector1.Scalar3 * mv2.KVector2.Scalar23 - mv1.KVector1.Scalar4 * mv2.KVector2.Scalar24;
                tempScalar[4] += -mv1.KVector1.Scalar1 * mv2.KVector2.Scalar13 + mv1.KVector1.Scalar2 * mv2.KVector2.Scalar23 - mv1.KVector1.Scalar4 * mv2.KVector2.Scalar34;
                tempScalar[8] += -mv1.KVector1.Scalar1 * mv2.KVector2.Scalar14 + mv1.KVector1.Scalar2 * mv2.KVector2.Scalar24 + mv1.KVector1.Scalar3 * mv2.KVector2.Scalar34;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[15] += mv1.KVector1.Scalar1 * mv2.KVector3.Scalar234 - mv1.KVector1.Scalar2 * mv2.KVector3.Scalar134 + mv1.KVector1.Scalar3 * mv2.KVector3.Scalar124 - mv1.KVector1.Scalar4 * mv2.KVector3.Scalar123;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[7] += -mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1234;
                tempScalar[11] += mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1234;
                tempScalar[13] += -mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1234;
                tempScalar[14] += -mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1234;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[1] += mv1.KVector2.Scalar12 * mv2.KVector1.Scalar2 + mv1.KVector2.Scalar13 * mv2.KVector1.Scalar3 + mv1.KVector2.Scalar14 * mv2.KVector1.Scalar4;
                tempScalar[2] += mv1.KVector2.Scalar12 * mv2.KVector1.Scalar1 + mv1.KVector2.Scalar23 * mv2.KVector1.Scalar3 + mv1.KVector2.Scalar24 * mv2.KVector1.Scalar4;
                tempScalar[4] += mv1.KVector2.Scalar13 * mv2.KVector1.Scalar1 - mv1.KVector2.Scalar23 * mv2.KVector1.Scalar2 + mv1.KVector2.Scalar34 * mv2.KVector1.Scalar4;
                tempScalar[8] += mv1.KVector2.Scalar14 * mv2.KVector1.Scalar1 - mv1.KVector2.Scalar24 * mv2.KVector1.Scalar2 - mv1.KVector2.Scalar34 * mv2.KVector1.Scalar3;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[3] += -mv1.KVector2.Scalar13 * mv2.KVector2.Scalar23 + mv1.KVector2.Scalar23 * mv2.KVector2.Scalar13 - mv1.KVector2.Scalar14 * mv2.KVector2.Scalar24 + mv1.KVector2.Scalar24 * mv2.KVector2.Scalar14;
                tempScalar[5] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar23 - mv1.KVector2.Scalar23 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar14 * mv2.KVector2.Scalar34 + mv1.KVector2.Scalar34 * mv2.KVector2.Scalar14;
                tempScalar[6] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar13 - mv1.KVector2.Scalar13 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar24 * mv2.KVector2.Scalar34 + mv1.KVector2.Scalar34 * mv2.KVector2.Scalar24;
                tempScalar[9] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar24 + mv1.KVector2.Scalar13 * mv2.KVector2.Scalar34 - mv1.KVector2.Scalar24 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar34 * mv2.KVector2.Scalar13;
                tempScalar[10] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar14 + mv1.KVector2.Scalar23 * mv2.KVector2.Scalar34 - mv1.KVector2.Scalar14 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar34 * mv2.KVector2.Scalar23;
                tempScalar[12] += mv1.KVector2.Scalar13 * mv2.KVector2.Scalar14 - mv1.KVector2.Scalar23 * mv2.KVector2.Scalar24 - mv1.KVector2.Scalar14 * mv2.KVector2.Scalar13 + mv1.KVector2.Scalar24 * mv2.KVector2.Scalar23;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[7] += mv1.KVector2.Scalar14 * mv2.KVector3.Scalar234 - mv1.KVector2.Scalar24 * mv2.KVector3.Scalar134 + mv1.KVector2.Scalar34 * mv2.KVector3.Scalar124;
                tempScalar[11] += -mv1.KVector2.Scalar13 * mv2.KVector3.Scalar234 + mv1.KVector2.Scalar23 * mv2.KVector3.Scalar134 - mv1.KVector2.Scalar34 * mv2.KVector3.Scalar123;
                tempScalar[13] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar234 - mv1.KVector2.Scalar23 * mv2.KVector3.Scalar124 + mv1.KVector2.Scalar24 * mv2.KVector3.Scalar123;
                tempScalar[14] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar134 - mv1.KVector2.Scalar13 * mv2.KVector3.Scalar124 + mv1.KVector2.Scalar14 * mv2.KVector3.Scalar123;
            }
            
        }
        
        if (!mv1.KVector3.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[15] += mv1.KVector3.Scalar123 * mv2.KVector1.Scalar4 - mv1.KVector3.Scalar124 * mv2.KVector1.Scalar3 + mv1.KVector3.Scalar134 * mv2.KVector1.Scalar2 - mv1.KVector3.Scalar234 * mv2.KVector1.Scalar1;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[7] += -mv1.KVector3.Scalar124 * mv2.KVector2.Scalar34 + mv1.KVector3.Scalar134 * mv2.KVector2.Scalar24 - mv1.KVector3.Scalar234 * mv2.KVector2.Scalar14;
                tempScalar[11] += mv1.KVector3.Scalar123 * mv2.KVector2.Scalar34 - mv1.KVector3.Scalar134 * mv2.KVector2.Scalar23 + mv1.KVector3.Scalar234 * mv2.KVector2.Scalar13;
                tempScalar[13] += -mv1.KVector3.Scalar123 * mv2.KVector2.Scalar24 + mv1.KVector3.Scalar124 * mv2.KVector2.Scalar23 - mv1.KVector3.Scalar234 * mv2.KVector2.Scalar12;
                tempScalar[14] += -mv1.KVector3.Scalar123 * mv2.KVector2.Scalar14 + mv1.KVector3.Scalar124 * mv2.KVector2.Scalar13 - mv1.KVector3.Scalar134 * mv2.KVector2.Scalar12;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[3] += -mv1.KVector3.Scalar134 * mv2.KVector3.Scalar234 + mv1.KVector3.Scalar234 * mv2.KVector3.Scalar134;
                tempScalar[5] += mv1.KVector3.Scalar124 * mv2.KVector3.Scalar234 - mv1.KVector3.Scalar234 * mv2.KVector3.Scalar124;
                tempScalar[6] += mv1.KVector3.Scalar124 * mv2.KVector3.Scalar134 - mv1.KVector3.Scalar134 * mv2.KVector3.Scalar124;
                tempScalar[9] += -mv1.KVector3.Scalar123 * mv2.KVector3.Scalar234 + mv1.KVector3.Scalar234 * mv2.KVector3.Scalar123;
                tempScalar[10] += -mv1.KVector3.Scalar123 * mv2.KVector3.Scalar134 + mv1.KVector3.Scalar134 * mv2.KVector3.Scalar123;
                tempScalar[12] += mv1.KVector3.Scalar123 * mv2.KVector3.Scalar124 - mv1.KVector3.Scalar124 * mv2.KVector3.Scalar123;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[1] += mv1.KVector3.Scalar234 * mv2.KVector4.Scalar1234;
                tempScalar[2] += mv1.KVector3.Scalar134 * mv2.KVector4.Scalar1234;
                tempScalar[4] += -mv1.KVector3.Scalar124 * mv2.KVector4.Scalar1234;
                tempScalar[8] += mv1.KVector3.Scalar123 * mv2.KVector4.Scalar1234;
            }
            
        }
        
        if (!mv1.KVector4.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[7] += mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar4;
                tempScalar[11] += -mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar3;
                tempScalar[13] += mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar2;
                tempScalar[14] += mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar1;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[1] += -mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar234;
                tempScalar[2] += -mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar134;
                tempScalar[4] += mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar124;
                tempScalar[8] += -mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar123;
            }
            
        }
        
        return Ga31Multivector.Create(tempScalar);
    }
    
}
