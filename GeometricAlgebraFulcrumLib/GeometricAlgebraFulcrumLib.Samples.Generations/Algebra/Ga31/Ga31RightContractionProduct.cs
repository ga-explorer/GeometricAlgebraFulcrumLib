using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga31;

public static class Ga31RightContractionProduct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Rcp(this Ga31KVector0 mv1, Ga31KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector0.Zero;
        
        return new Ga31KVector0
        {
            Scalar = mv1.Scalar * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Rcp(this Ga31KVector0 mv1, Ga31KVector1 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Rcp(this Ga31KVector0 mv1, Ga31KVector2 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Rcp(this Ga31KVector0 mv1, Ga31KVector3 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Rcp(this Ga31KVector0 mv1, Ga31KVector4 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    public static Ga31KVector0 Rcp(this Ga31KVector0 mv1, Ga31Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar += mv1.Scalar * mv2.KVector0.Scalar;
        }
        
        return new Ga31KVector0
        {
            Scalar = tempScalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector1 Rcp(this Ga31KVector1 mv1, Ga31KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector1.Zero;
        
        return new Ga31KVector1
        {
            Scalar1 = mv1.Scalar1 * mv2.Scalar,
            Scalar2 = mv1.Scalar2 * mv2.Scalar,
            Scalar3 = mv1.Scalar3 * mv2.Scalar,
            Scalar4 = mv1.Scalar4 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Rcp(this Ga31KVector1 mv1, Ga31KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector0.Zero;
        
        return new Ga31KVector0
        {
            Scalar = -mv1.Scalar1 * mv2.Scalar1 + mv1.Scalar2 * mv2.Scalar2 + mv1.Scalar3 * mv2.Scalar3 + mv1.Scalar4 * mv2.Scalar4
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Rcp(this Ga31KVector1 mv1, Ga31KVector2 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Rcp(this Ga31KVector1 mv1, Ga31KVector3 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Rcp(this Ga31KVector1 mv1, Ga31KVector4 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    public static Ga31Multivector Rcp(this Ga31KVector1 mv1, Ga31Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[1] += mv1.Scalar1 * mv2.KVector0.Scalar;
            tempScalar[2] += mv1.Scalar2 * mv2.KVector0.Scalar;
            tempScalar[4] += mv1.Scalar3 * mv2.KVector0.Scalar;
            tempScalar[8] += mv1.Scalar4 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[0] += -mv1.Scalar1 * mv2.KVector1.Scalar1 + mv1.Scalar2 * mv2.KVector1.Scalar2 + mv1.Scalar3 * mv2.KVector1.Scalar3 + mv1.Scalar4 * mv2.KVector1.Scalar4;
        }
        
        return Ga31Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector2 Rcp(this Ga31KVector2 mv1, Ga31KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector2.Zero;
        
        return new Ga31KVector2
        {
            Scalar12 = mv1.Scalar12 * mv2.Scalar,
            Scalar13 = mv1.Scalar13 * mv2.Scalar,
            Scalar23 = mv1.Scalar23 * mv2.Scalar,
            Scalar14 = mv1.Scalar14 * mv2.Scalar,
            Scalar24 = mv1.Scalar24 * mv2.Scalar,
            Scalar34 = mv1.Scalar34 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector1 Rcp(this Ga31KVector2 mv1, Ga31KVector1 mv2)
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
    public static Ga31KVector0 Rcp(this Ga31KVector2 mv1, Ga31KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector0.Zero;
        
        return new Ga31KVector0
        {
            Scalar = mv1.Scalar12 * mv2.Scalar12 + mv1.Scalar13 * mv2.Scalar13 - mv1.Scalar23 * mv2.Scalar23 + mv1.Scalar14 * mv2.Scalar14 - mv1.Scalar24 * mv2.Scalar24 - mv1.Scalar34 * mv2.Scalar34
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Rcp(this Ga31KVector2 mv1, Ga31KVector3 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Rcp(this Ga31KVector2 mv1, Ga31KVector4 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    public static Ga31Multivector Rcp(this Ga31KVector2 mv1, Ga31Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[3] += mv1.Scalar12 * mv2.KVector0.Scalar;
            tempScalar[5] += mv1.Scalar13 * mv2.KVector0.Scalar;
            tempScalar[6] += mv1.Scalar23 * mv2.KVector0.Scalar;
            tempScalar[9] += mv1.Scalar14 * mv2.KVector0.Scalar;
            tempScalar[10] += mv1.Scalar24 * mv2.KVector0.Scalar;
            tempScalar[12] += mv1.Scalar34 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[1] += mv1.Scalar12 * mv2.KVector1.Scalar2 + mv1.Scalar13 * mv2.KVector1.Scalar3 + mv1.Scalar14 * mv2.KVector1.Scalar4;
            tempScalar[2] += mv1.Scalar12 * mv2.KVector1.Scalar1 + mv1.Scalar23 * mv2.KVector1.Scalar3 + mv1.Scalar24 * mv2.KVector1.Scalar4;
            tempScalar[4] += mv1.Scalar13 * mv2.KVector1.Scalar1 - mv1.Scalar23 * mv2.KVector1.Scalar2 + mv1.Scalar34 * mv2.KVector1.Scalar4;
            tempScalar[8] += mv1.Scalar14 * mv2.KVector1.Scalar1 - mv1.Scalar24 * mv2.KVector1.Scalar2 - mv1.Scalar34 * mv2.KVector1.Scalar3;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[0] += mv1.Scalar12 * mv2.KVector2.Scalar12 + mv1.Scalar13 * mv2.KVector2.Scalar13 - mv1.Scalar23 * mv2.KVector2.Scalar23 + mv1.Scalar14 * mv2.KVector2.Scalar14 - mv1.Scalar24 * mv2.KVector2.Scalar24 - mv1.Scalar34 * mv2.KVector2.Scalar34;
        }
        
        return Ga31Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector3 Rcp(this Ga31KVector3 mv1, Ga31KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector3.Zero;
        
        return new Ga31KVector3
        {
            Scalar123 = mv1.Scalar123 * mv2.Scalar,
            Scalar124 = mv1.Scalar124 * mv2.Scalar,
            Scalar134 = mv1.Scalar134 * mv2.Scalar,
            Scalar234 = mv1.Scalar234 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector2 Rcp(this Ga31KVector3 mv1, Ga31KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector2.Zero;
        
        return new Ga31KVector2
        {
            Scalar12 = mv1.Scalar123 * mv2.Scalar3 + mv1.Scalar124 * mv2.Scalar4,
            Scalar13 = -mv1.Scalar123 * mv2.Scalar2 + mv1.Scalar134 * mv2.Scalar4,
            Scalar23 = -mv1.Scalar123 * mv2.Scalar1 + mv1.Scalar234 * mv2.Scalar4,
            Scalar14 = -mv1.Scalar124 * mv2.Scalar2 - mv1.Scalar134 * mv2.Scalar3,
            Scalar24 = -mv1.Scalar124 * mv2.Scalar1 - mv1.Scalar234 * mv2.Scalar3,
            Scalar34 = -mv1.Scalar134 * mv2.Scalar1 + mv1.Scalar234 * mv2.Scalar2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector1 Rcp(this Ga31KVector3 mv1, Ga31KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector1.Zero;
        
        return new Ga31KVector1
        {
            Scalar1 = -mv1.Scalar123 * mv2.Scalar23 - mv1.Scalar124 * mv2.Scalar24 - mv1.Scalar134 * mv2.Scalar34,
            Scalar2 = -mv1.Scalar123 * mv2.Scalar13 - mv1.Scalar124 * mv2.Scalar14 - mv1.Scalar234 * mv2.Scalar34,
            Scalar3 = mv1.Scalar123 * mv2.Scalar12 - mv1.Scalar134 * mv2.Scalar14 + mv1.Scalar234 * mv2.Scalar24,
            Scalar4 = mv1.Scalar124 * mv2.Scalar12 + mv1.Scalar134 * mv2.Scalar13 - mv1.Scalar234 * mv2.Scalar23
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Rcp(this Ga31KVector3 mv1, Ga31KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector0.Zero;
        
        return new Ga31KVector0
        {
            Scalar = mv1.Scalar123 * mv2.Scalar123 + mv1.Scalar124 * mv2.Scalar124 + mv1.Scalar134 * mv2.Scalar134 - mv1.Scalar234 * mv2.Scalar234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector0 Rcp(this Ga31KVector3 mv1, Ga31KVector4 mv2)
    {
        return Ga31KVector0.Zero;
    }
    
    public static Ga31Multivector Rcp(this Ga31KVector3 mv1, Ga31Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[7] += mv1.Scalar123 * mv2.KVector0.Scalar;
            tempScalar[11] += mv1.Scalar124 * mv2.KVector0.Scalar;
            tempScalar[13] += mv1.Scalar134 * mv2.KVector0.Scalar;
            tempScalar[14] += mv1.Scalar234 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[3] += mv1.Scalar123 * mv2.KVector1.Scalar3 + mv1.Scalar124 * mv2.KVector1.Scalar4;
            tempScalar[5] += -mv1.Scalar123 * mv2.KVector1.Scalar2 + mv1.Scalar134 * mv2.KVector1.Scalar4;
            tempScalar[6] += -mv1.Scalar123 * mv2.KVector1.Scalar1 + mv1.Scalar234 * mv2.KVector1.Scalar4;
            tempScalar[9] += -mv1.Scalar124 * mv2.KVector1.Scalar2 - mv1.Scalar134 * mv2.KVector1.Scalar3;
            tempScalar[10] += -mv1.Scalar124 * mv2.KVector1.Scalar1 - mv1.Scalar234 * mv2.KVector1.Scalar3;
            tempScalar[12] += -mv1.Scalar134 * mv2.KVector1.Scalar1 + mv1.Scalar234 * mv2.KVector1.Scalar2;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[1] += -mv1.Scalar123 * mv2.KVector2.Scalar23 - mv1.Scalar124 * mv2.KVector2.Scalar24 - mv1.Scalar134 * mv2.KVector2.Scalar34;
            tempScalar[2] += -mv1.Scalar123 * mv2.KVector2.Scalar13 - mv1.Scalar124 * mv2.KVector2.Scalar14 - mv1.Scalar234 * mv2.KVector2.Scalar34;
            tempScalar[4] += mv1.Scalar123 * mv2.KVector2.Scalar12 - mv1.Scalar134 * mv2.KVector2.Scalar14 + mv1.Scalar234 * mv2.KVector2.Scalar24;
            tempScalar[8] += mv1.Scalar124 * mv2.KVector2.Scalar12 + mv1.Scalar134 * mv2.KVector2.Scalar13 - mv1.Scalar234 * mv2.KVector2.Scalar23;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[0] += mv1.Scalar123 * mv2.KVector3.Scalar123 + mv1.Scalar124 * mv2.KVector3.Scalar124 + mv1.Scalar134 * mv2.KVector3.Scalar134 - mv1.Scalar234 * mv2.KVector3.Scalar234;
        }
        
        return Ga31Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector4 Rcp(this Ga31KVector4 mv1, Ga31KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector4.Zero;
        
        return new Ga31KVector4
        {
            Scalar1234 = mv1.Scalar1234 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector3 Rcp(this Ga31KVector4 mv1, Ga31KVector1 mv2)
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
    public static Ga31KVector2 Rcp(this Ga31KVector4 mv1, Ga31KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector2.Zero;
        
        return new Ga31KVector2
        {
            Scalar12 = -mv1.Scalar1234 * mv2.Scalar34,
            Scalar13 = mv1.Scalar1234 * mv2.Scalar24,
            Scalar23 = mv1.Scalar1234 * mv2.Scalar14,
            Scalar14 = -mv1.Scalar1234 * mv2.Scalar23,
            Scalar24 = -mv1.Scalar1234 * mv2.Scalar13,
            Scalar34 = mv1.Scalar1234 * mv2.Scalar12
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga31KVector1 Rcp(this Ga31KVector4 mv1, Ga31KVector3 mv2)
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
    public static Ga31KVector0 Rcp(this Ga31KVector4 mv1, Ga31KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector0.Zero;
        
        return new Ga31KVector0
        {
            Scalar = -mv1.Scalar1234 * mv2.Scalar1234
        };
    }
    
    public static Ga31Multivector Rcp(this Ga31KVector4 mv1, Ga31Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[15] += mv1.Scalar1234 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[7] += mv1.Scalar1234 * mv2.KVector1.Scalar4;
            tempScalar[11] += -mv1.Scalar1234 * mv2.KVector1.Scalar3;
            tempScalar[13] += mv1.Scalar1234 * mv2.KVector1.Scalar2;
            tempScalar[14] += mv1.Scalar1234 * mv2.KVector1.Scalar1;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[3] += -mv1.Scalar1234 * mv2.KVector2.Scalar34;
            tempScalar[5] += mv1.Scalar1234 * mv2.KVector2.Scalar24;
            tempScalar[6] += mv1.Scalar1234 * mv2.KVector2.Scalar14;
            tempScalar[9] += -mv1.Scalar1234 * mv2.KVector2.Scalar23;
            tempScalar[10] += -mv1.Scalar1234 * mv2.KVector2.Scalar13;
            tempScalar[12] += mv1.Scalar1234 * mv2.KVector2.Scalar12;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[1] += -mv1.Scalar1234 * mv2.KVector3.Scalar234;
            tempScalar[2] += -mv1.Scalar1234 * mv2.KVector3.Scalar134;
            tempScalar[4] += mv1.Scalar1234 * mv2.KVector3.Scalar124;
            tempScalar[8] += -mv1.Scalar1234 * mv2.KVector3.Scalar123;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[0] += -mv1.Scalar1234 * mv2.KVector4.Scalar1234;
        }
        
        return Ga31Multivector.Create(tempScalar);
    }
    
    public static Ga31Multivector Rcp(this Ga31Multivector mv1, Ga31KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[0] += mv1.KVector0.Scalar * mv2.Scalar;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += mv1.KVector1.Scalar1 * mv2.Scalar;
            tempScalar[2] += mv1.KVector1.Scalar2 * mv2.Scalar;
            tempScalar[4] += mv1.KVector1.Scalar3 * mv2.Scalar;
            tempScalar[8] += mv1.KVector1.Scalar4 * mv2.Scalar;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += mv1.KVector2.Scalar12 * mv2.Scalar;
            tempScalar[5] += mv1.KVector2.Scalar13 * mv2.Scalar;
            tempScalar[6] += mv1.KVector2.Scalar23 * mv2.Scalar;
            tempScalar[9] += mv1.KVector2.Scalar14 * mv2.Scalar;
            tempScalar[10] += mv1.KVector2.Scalar24 * mv2.Scalar;
            tempScalar[12] += mv1.KVector2.Scalar34 * mv2.Scalar;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += mv1.KVector3.Scalar123 * mv2.Scalar;
            tempScalar[11] += mv1.KVector3.Scalar124 * mv2.Scalar;
            tempScalar[13] += mv1.KVector3.Scalar134 * mv2.Scalar;
            tempScalar[14] += mv1.KVector3.Scalar234 * mv2.Scalar;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += mv1.KVector4.Scalar1234 * mv2.Scalar;
        }
        
        return Ga31Multivector.Create(tempScalar);
    }
    
    public static Ga31Multivector Rcp(this Ga31Multivector mv1, Ga31KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[0] += -mv1.KVector1.Scalar1 * mv2.Scalar1 + mv1.KVector1.Scalar2 * mv2.Scalar2 + mv1.KVector1.Scalar3 * mv2.Scalar3 + mv1.KVector1.Scalar4 * mv2.Scalar4;
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
            tempScalar[3] += mv1.KVector3.Scalar123 * mv2.Scalar3 + mv1.KVector3.Scalar124 * mv2.Scalar4;
            tempScalar[5] += -mv1.KVector3.Scalar123 * mv2.Scalar2 + mv1.KVector3.Scalar134 * mv2.Scalar4;
            tempScalar[6] += -mv1.KVector3.Scalar123 * mv2.Scalar1 + mv1.KVector3.Scalar234 * mv2.Scalar4;
            tempScalar[9] += -mv1.KVector3.Scalar124 * mv2.Scalar2 - mv1.KVector3.Scalar134 * mv2.Scalar3;
            tempScalar[10] += -mv1.KVector3.Scalar124 * mv2.Scalar1 - mv1.KVector3.Scalar234 * mv2.Scalar3;
            tempScalar[12] += -mv1.KVector3.Scalar134 * mv2.Scalar1 + mv1.KVector3.Scalar234 * mv2.Scalar2;
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
    
    public static Ga31Multivector Rcp(this Ga31Multivector mv1, Ga31KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[0] += mv1.KVector2.Scalar12 * mv2.Scalar12 + mv1.KVector2.Scalar13 * mv2.Scalar13 - mv1.KVector2.Scalar23 * mv2.Scalar23 + mv1.KVector2.Scalar14 * mv2.Scalar14 - mv1.KVector2.Scalar24 * mv2.Scalar24 - mv1.KVector2.Scalar34 * mv2.Scalar34;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[1] += -mv1.KVector3.Scalar123 * mv2.Scalar23 - mv1.KVector3.Scalar124 * mv2.Scalar24 - mv1.KVector3.Scalar134 * mv2.Scalar34;
            tempScalar[2] += -mv1.KVector3.Scalar123 * mv2.Scalar13 - mv1.KVector3.Scalar124 * mv2.Scalar14 - mv1.KVector3.Scalar234 * mv2.Scalar34;
            tempScalar[4] += mv1.KVector3.Scalar123 * mv2.Scalar12 - mv1.KVector3.Scalar134 * mv2.Scalar14 + mv1.KVector3.Scalar234 * mv2.Scalar24;
            tempScalar[8] += mv1.KVector3.Scalar124 * mv2.Scalar12 + mv1.KVector3.Scalar134 * mv2.Scalar13 - mv1.KVector3.Scalar234 * mv2.Scalar23;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[3] += -mv1.KVector4.Scalar1234 * mv2.Scalar34;
            tempScalar[5] += mv1.KVector4.Scalar1234 * mv2.Scalar24;
            tempScalar[6] += mv1.KVector4.Scalar1234 * mv2.Scalar14;
            tempScalar[9] += -mv1.KVector4.Scalar1234 * mv2.Scalar23;
            tempScalar[10] += -mv1.KVector4.Scalar1234 * mv2.Scalar13;
            tempScalar[12] += mv1.KVector4.Scalar1234 * mv2.Scalar12;
        }
        
        return Ga31Multivector.Create(tempScalar);
    }
    
    public static Ga31Multivector Rcp(this Ga31Multivector mv1, Ga31KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[0] += mv1.KVector3.Scalar123 * mv2.Scalar123 + mv1.KVector3.Scalar124 * mv2.Scalar124 + mv1.KVector3.Scalar134 * mv2.Scalar134 - mv1.KVector3.Scalar234 * mv2.Scalar234;
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
    
    public static Ga31KVector0 Rcp(this Ga31Multivector mv1, Ga31KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar += -mv1.KVector4.Scalar1234 * mv2.Scalar1234;
        }
        
        return new Ga31KVector0
        {
            Scalar = tempScalar
        };
    }
    
    public static Ga31Multivector Rcp(this Ga31Multivector mv1, Ga31Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga31Multivector.Zero;
        
        var tempScalar = new double[16];
        
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
                tempScalar[8] += mv1.KVector1.Scalar4 * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[0] += -mv1.KVector1.Scalar1 * mv2.KVector1.Scalar1 + mv1.KVector1.Scalar2 * mv2.KVector1.Scalar2 + mv1.KVector1.Scalar3 * mv2.KVector1.Scalar3 + mv1.KVector1.Scalar4 * mv2.KVector1.Scalar4;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[3] += mv1.KVector2.Scalar12 * mv2.KVector0.Scalar;
                tempScalar[5] += mv1.KVector2.Scalar13 * mv2.KVector0.Scalar;
                tempScalar[6] += mv1.KVector2.Scalar23 * mv2.KVector0.Scalar;
                tempScalar[9] += mv1.KVector2.Scalar14 * mv2.KVector0.Scalar;
                tempScalar[10] += mv1.KVector2.Scalar24 * mv2.KVector0.Scalar;
                tempScalar[12] += mv1.KVector2.Scalar34 * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[1] += mv1.KVector2.Scalar12 * mv2.KVector1.Scalar2 + mv1.KVector2.Scalar13 * mv2.KVector1.Scalar3 + mv1.KVector2.Scalar14 * mv2.KVector1.Scalar4;
                tempScalar[2] += mv1.KVector2.Scalar12 * mv2.KVector1.Scalar1 + mv1.KVector2.Scalar23 * mv2.KVector1.Scalar3 + mv1.KVector2.Scalar24 * mv2.KVector1.Scalar4;
                tempScalar[4] += mv1.KVector2.Scalar13 * mv2.KVector1.Scalar1 - mv1.KVector2.Scalar23 * mv2.KVector1.Scalar2 + mv1.KVector2.Scalar34 * mv2.KVector1.Scalar4;
                tempScalar[8] += mv1.KVector2.Scalar14 * mv2.KVector1.Scalar1 - mv1.KVector2.Scalar24 * mv2.KVector1.Scalar2 - mv1.KVector2.Scalar34 * mv2.KVector1.Scalar3;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[0] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar12 + mv1.KVector2.Scalar13 * mv2.KVector2.Scalar13 - mv1.KVector2.Scalar23 * mv2.KVector2.Scalar23 + mv1.KVector2.Scalar14 * mv2.KVector2.Scalar14 - mv1.KVector2.Scalar24 * mv2.KVector2.Scalar24 - mv1.KVector2.Scalar34 * mv2.KVector2.Scalar34;
            }
            
        }
        
        if (!mv1.KVector3.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[7] += mv1.KVector3.Scalar123 * mv2.KVector0.Scalar;
                tempScalar[11] += mv1.KVector3.Scalar124 * mv2.KVector0.Scalar;
                tempScalar[13] += mv1.KVector3.Scalar134 * mv2.KVector0.Scalar;
                tempScalar[14] += mv1.KVector3.Scalar234 * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[3] += mv1.KVector3.Scalar123 * mv2.KVector1.Scalar3 + mv1.KVector3.Scalar124 * mv2.KVector1.Scalar4;
                tempScalar[5] += -mv1.KVector3.Scalar123 * mv2.KVector1.Scalar2 + mv1.KVector3.Scalar134 * mv2.KVector1.Scalar4;
                tempScalar[6] += -mv1.KVector3.Scalar123 * mv2.KVector1.Scalar1 + mv1.KVector3.Scalar234 * mv2.KVector1.Scalar4;
                tempScalar[9] += -mv1.KVector3.Scalar124 * mv2.KVector1.Scalar2 - mv1.KVector3.Scalar134 * mv2.KVector1.Scalar3;
                tempScalar[10] += -mv1.KVector3.Scalar124 * mv2.KVector1.Scalar1 - mv1.KVector3.Scalar234 * mv2.KVector1.Scalar3;
                tempScalar[12] += -mv1.KVector3.Scalar134 * mv2.KVector1.Scalar1 + mv1.KVector3.Scalar234 * mv2.KVector1.Scalar2;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[1] += -mv1.KVector3.Scalar123 * mv2.KVector2.Scalar23 - mv1.KVector3.Scalar124 * mv2.KVector2.Scalar24 - mv1.KVector3.Scalar134 * mv2.KVector2.Scalar34;
                tempScalar[2] += -mv1.KVector3.Scalar123 * mv2.KVector2.Scalar13 - mv1.KVector3.Scalar124 * mv2.KVector2.Scalar14 - mv1.KVector3.Scalar234 * mv2.KVector2.Scalar34;
                tempScalar[4] += mv1.KVector3.Scalar123 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar134 * mv2.KVector2.Scalar14 + mv1.KVector3.Scalar234 * mv2.KVector2.Scalar24;
                tempScalar[8] += mv1.KVector3.Scalar124 * mv2.KVector2.Scalar12 + mv1.KVector3.Scalar134 * mv2.KVector2.Scalar13 - mv1.KVector3.Scalar234 * mv2.KVector2.Scalar23;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[0] += mv1.KVector3.Scalar123 * mv2.KVector3.Scalar123 + mv1.KVector3.Scalar124 * mv2.KVector3.Scalar124 + mv1.KVector3.Scalar134 * mv2.KVector3.Scalar134 - mv1.KVector3.Scalar234 * mv2.KVector3.Scalar234;
            }
            
        }
        
        if (!mv1.KVector4.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[15] += mv1.KVector4.Scalar1234 * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[7] += mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar4;
                tempScalar[11] += -mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar3;
                tempScalar[13] += mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar2;
                tempScalar[14] += mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar1;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[3] += -mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar34;
                tempScalar[5] += mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar24;
                tempScalar[6] += mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar14;
                tempScalar[9] += -mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar23;
                tempScalar[10] += -mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar13;
                tempScalar[12] += mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar12;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[1] += -mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar234;
                tempScalar[2] += -mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar134;
                tempScalar[4] += mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar124;
                tempScalar[8] += -mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar123;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[0] += -mv1.KVector4.Scalar1234 * mv2.KVector4.Scalar1234;
            }
            
        }
        
        return Ga31Multivector.Create(tempScalar);
    }
    
}
