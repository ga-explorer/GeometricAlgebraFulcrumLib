using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga41;

public static class Ga41AntiCommutatorProduct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Acp(this Ga41KVector0 mv1, Ga41KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        return new Ga41KVector0
        {
            Scalar = mv1.Scalar * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector1 Acp(this Ga41KVector0 mv1, Ga41KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector1.Zero;
        
        return new Ga41KVector1
        {
            Scalar1 = mv1.Scalar * mv2.Scalar1,
            Scalar2 = mv1.Scalar * mv2.Scalar2,
            Scalar3 = mv1.Scalar * mv2.Scalar3,
            Scalar4 = mv1.Scalar * mv2.Scalar4,
            Scalar5 = mv1.Scalar * mv2.Scalar5
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector2 Acp(this Ga41KVector0 mv1, Ga41KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector2.Zero;
        
        return new Ga41KVector2
        {
            Scalar12 = mv1.Scalar * mv2.Scalar12,
            Scalar13 = mv1.Scalar * mv2.Scalar13,
            Scalar23 = mv1.Scalar * mv2.Scalar23,
            Scalar14 = mv1.Scalar * mv2.Scalar14,
            Scalar24 = mv1.Scalar * mv2.Scalar24,
            Scalar34 = mv1.Scalar * mv2.Scalar34,
            Scalar15 = mv1.Scalar * mv2.Scalar15,
            Scalar25 = mv1.Scalar * mv2.Scalar25,
            Scalar35 = mv1.Scalar * mv2.Scalar35,
            Scalar45 = mv1.Scalar * mv2.Scalar45
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector3 Acp(this Ga41KVector0 mv1, Ga41KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector3.Zero;
        
        return new Ga41KVector3
        {
            Scalar123 = mv1.Scalar * mv2.Scalar123,
            Scalar124 = mv1.Scalar * mv2.Scalar124,
            Scalar134 = mv1.Scalar * mv2.Scalar134,
            Scalar234 = mv1.Scalar * mv2.Scalar234,
            Scalar125 = mv1.Scalar * mv2.Scalar125,
            Scalar135 = mv1.Scalar * mv2.Scalar135,
            Scalar235 = mv1.Scalar * mv2.Scalar235,
            Scalar145 = mv1.Scalar * mv2.Scalar145,
            Scalar245 = mv1.Scalar * mv2.Scalar245,
            Scalar345 = mv1.Scalar * mv2.Scalar345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector4 Acp(this Ga41KVector0 mv1, Ga41KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector4.Zero;
        
        return new Ga41KVector4
        {
            Scalar1234 = mv1.Scalar * mv2.Scalar1234,
            Scalar1235 = mv1.Scalar * mv2.Scalar1235,
            Scalar1245 = mv1.Scalar * mv2.Scalar1245,
            Scalar1345 = mv1.Scalar * mv2.Scalar1345,
            Scalar2345 = mv1.Scalar * mv2.Scalar2345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector5 Acp(this Ga41KVector0 mv1, Ga41KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector5.Zero;
        
        return new Ga41KVector5
        {
            Scalar12345 = mv1.Scalar * mv2.Scalar12345
        };
    }
    
    public static Ga41Multivector Acp(this Ga41KVector0 mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[0] += mv1.Scalar * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[1] += mv1.Scalar * mv2.KVector1.Scalar1;
            tempScalar[2] += mv1.Scalar * mv2.KVector1.Scalar2;
            tempScalar[4] += mv1.Scalar * mv2.KVector1.Scalar3;
            tempScalar[8] += mv1.Scalar * mv2.KVector1.Scalar4;
            tempScalar[16] += mv1.Scalar * mv2.KVector1.Scalar5;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[3] += mv1.Scalar * mv2.KVector2.Scalar12;
            tempScalar[5] += mv1.Scalar * mv2.KVector2.Scalar13;
            tempScalar[6] += mv1.Scalar * mv2.KVector2.Scalar23;
            tempScalar[9] += mv1.Scalar * mv2.KVector2.Scalar14;
            tempScalar[10] += mv1.Scalar * mv2.KVector2.Scalar24;
            tempScalar[12] += mv1.Scalar * mv2.KVector2.Scalar34;
            tempScalar[17] += mv1.Scalar * mv2.KVector2.Scalar15;
            tempScalar[18] += mv1.Scalar * mv2.KVector2.Scalar25;
            tempScalar[20] += mv1.Scalar * mv2.KVector2.Scalar35;
            tempScalar[24] += mv1.Scalar * mv2.KVector2.Scalar45;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[7] += mv1.Scalar * mv2.KVector3.Scalar123;
            tempScalar[11] += mv1.Scalar * mv2.KVector3.Scalar124;
            tempScalar[13] += mv1.Scalar * mv2.KVector3.Scalar134;
            tempScalar[14] += mv1.Scalar * mv2.KVector3.Scalar234;
            tempScalar[19] += mv1.Scalar * mv2.KVector3.Scalar125;
            tempScalar[21] += mv1.Scalar * mv2.KVector3.Scalar135;
            tempScalar[22] += mv1.Scalar * mv2.KVector3.Scalar235;
            tempScalar[25] += mv1.Scalar * mv2.KVector3.Scalar145;
            tempScalar[26] += mv1.Scalar * mv2.KVector3.Scalar245;
            tempScalar[28] += mv1.Scalar * mv2.KVector3.Scalar345;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[15] += mv1.Scalar * mv2.KVector4.Scalar1234;
            tempScalar[23] += mv1.Scalar * mv2.KVector4.Scalar1235;
            tempScalar[27] += mv1.Scalar * mv2.KVector4.Scalar1245;
            tempScalar[29] += mv1.Scalar * mv2.KVector4.Scalar1345;
            tempScalar[30] += mv1.Scalar * mv2.KVector4.Scalar2345;
        }
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[31] += mv1.Scalar * mv2.KVector5.Scalar12345;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector1 Acp(this Ga41KVector1 mv1, Ga41KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector1.Zero;
        
        return new Ga41KVector1
        {
            Scalar1 = mv1.Scalar1 * mv2.Scalar,
            Scalar2 = mv1.Scalar2 * mv2.Scalar,
            Scalar3 = mv1.Scalar3 * mv2.Scalar,
            Scalar4 = mv1.Scalar4 * mv2.Scalar,
            Scalar5 = mv1.Scalar5 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Acp(this Ga41KVector1 mv1, Ga41KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        return new Ga41KVector0
        {
            Scalar = -mv1.Scalar1 * mv2.Scalar1 + mv1.Scalar2 * mv2.Scalar2 + mv1.Scalar3 * mv2.Scalar3 + mv1.Scalar4 * mv2.Scalar4 + mv1.Scalar5 * mv2.Scalar5
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector3 Acp(this Ga41KVector1 mv1, Ga41KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector3.Zero;
        
        return new Ga41KVector3
        {
            Scalar123 = mv1.Scalar1 * mv2.Scalar23 - mv1.Scalar2 * mv2.Scalar13 + mv1.Scalar3 * mv2.Scalar12,
            Scalar124 = mv1.Scalar1 * mv2.Scalar24 - mv1.Scalar2 * mv2.Scalar14 + mv1.Scalar4 * mv2.Scalar12,
            Scalar134 = mv1.Scalar1 * mv2.Scalar34 - mv1.Scalar3 * mv2.Scalar14 + mv1.Scalar4 * mv2.Scalar13,
            Scalar234 = mv1.Scalar2 * mv2.Scalar34 - mv1.Scalar3 * mv2.Scalar24 + mv1.Scalar4 * mv2.Scalar23,
            Scalar125 = mv1.Scalar1 * mv2.Scalar25 - mv1.Scalar2 * mv2.Scalar15 + mv1.Scalar5 * mv2.Scalar12,
            Scalar135 = mv1.Scalar1 * mv2.Scalar35 - mv1.Scalar3 * mv2.Scalar15 + mv1.Scalar5 * mv2.Scalar13,
            Scalar235 = mv1.Scalar2 * mv2.Scalar35 - mv1.Scalar3 * mv2.Scalar25 + mv1.Scalar5 * mv2.Scalar23,
            Scalar145 = mv1.Scalar1 * mv2.Scalar45 - mv1.Scalar4 * mv2.Scalar15 + mv1.Scalar5 * mv2.Scalar14,
            Scalar245 = mv1.Scalar2 * mv2.Scalar45 - mv1.Scalar4 * mv2.Scalar25 + mv1.Scalar5 * mv2.Scalar24,
            Scalar345 = mv1.Scalar3 * mv2.Scalar45 - mv1.Scalar4 * mv2.Scalar35 + mv1.Scalar5 * mv2.Scalar34
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector2 Acp(this Ga41KVector1 mv1, Ga41KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector2.Zero;
        
        return new Ga41KVector2
        {
            Scalar12 = mv1.Scalar3 * mv2.Scalar123 + mv1.Scalar4 * mv2.Scalar124 + mv1.Scalar5 * mv2.Scalar125,
            Scalar13 = -mv1.Scalar2 * mv2.Scalar123 + mv1.Scalar4 * mv2.Scalar134 + mv1.Scalar5 * mv2.Scalar135,
            Scalar23 = -mv1.Scalar1 * mv2.Scalar123 + mv1.Scalar4 * mv2.Scalar234 + mv1.Scalar5 * mv2.Scalar235,
            Scalar14 = -mv1.Scalar2 * mv2.Scalar124 - mv1.Scalar3 * mv2.Scalar134 + mv1.Scalar5 * mv2.Scalar145,
            Scalar24 = -mv1.Scalar1 * mv2.Scalar124 - mv1.Scalar3 * mv2.Scalar234 + mv1.Scalar5 * mv2.Scalar245,
            Scalar34 = -mv1.Scalar1 * mv2.Scalar134 + mv1.Scalar2 * mv2.Scalar234 + mv1.Scalar5 * mv2.Scalar345,
            Scalar15 = -mv1.Scalar2 * mv2.Scalar125 - mv1.Scalar3 * mv2.Scalar135 - mv1.Scalar4 * mv2.Scalar145,
            Scalar25 = -mv1.Scalar1 * mv2.Scalar125 - mv1.Scalar3 * mv2.Scalar235 - mv1.Scalar4 * mv2.Scalar245,
            Scalar35 = -mv1.Scalar1 * mv2.Scalar135 + mv1.Scalar2 * mv2.Scalar235 - mv1.Scalar4 * mv2.Scalar345,
            Scalar45 = -mv1.Scalar1 * mv2.Scalar145 + mv1.Scalar2 * mv2.Scalar245 + mv1.Scalar3 * mv2.Scalar345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector5 Acp(this Ga41KVector1 mv1, Ga41KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector5.Zero;
        
        return new Ga41KVector5
        {
            Scalar12345 = mv1.Scalar1 * mv2.Scalar2345 - mv1.Scalar2 * mv2.Scalar1345 + mv1.Scalar3 * mv2.Scalar1245 - mv1.Scalar4 * mv2.Scalar1235 + mv1.Scalar5 * mv2.Scalar1234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector4 Acp(this Ga41KVector1 mv1, Ga41KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector4.Zero;
        
        return new Ga41KVector4
        {
            Scalar1234 = mv1.Scalar5 * mv2.Scalar12345,
            Scalar1235 = -mv1.Scalar4 * mv2.Scalar12345,
            Scalar1245 = mv1.Scalar3 * mv2.Scalar12345,
            Scalar1345 = -mv1.Scalar2 * mv2.Scalar12345,
            Scalar2345 = -mv1.Scalar1 * mv2.Scalar12345
        };
    }
    
    public static Ga41Multivector Acp(this Ga41KVector1 mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[1] += mv1.Scalar1 * mv2.KVector0.Scalar;
            tempScalar[2] += mv1.Scalar2 * mv2.KVector0.Scalar;
            tempScalar[4] += mv1.Scalar3 * mv2.KVector0.Scalar;
            tempScalar[8] += mv1.Scalar4 * mv2.KVector0.Scalar;
            tempScalar[16] += mv1.Scalar5 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[0] += -mv1.Scalar1 * mv2.KVector1.Scalar1 + mv1.Scalar2 * mv2.KVector1.Scalar2 + mv1.Scalar3 * mv2.KVector1.Scalar3 + mv1.Scalar4 * mv2.KVector1.Scalar4 + mv1.Scalar5 * mv2.KVector1.Scalar5;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[7] += mv1.Scalar1 * mv2.KVector2.Scalar23 - mv1.Scalar2 * mv2.KVector2.Scalar13 + mv1.Scalar3 * mv2.KVector2.Scalar12;
            tempScalar[11] += mv1.Scalar1 * mv2.KVector2.Scalar24 - mv1.Scalar2 * mv2.KVector2.Scalar14 + mv1.Scalar4 * mv2.KVector2.Scalar12;
            tempScalar[13] += mv1.Scalar1 * mv2.KVector2.Scalar34 - mv1.Scalar3 * mv2.KVector2.Scalar14 + mv1.Scalar4 * mv2.KVector2.Scalar13;
            tempScalar[14] += mv1.Scalar2 * mv2.KVector2.Scalar34 - mv1.Scalar3 * mv2.KVector2.Scalar24 + mv1.Scalar4 * mv2.KVector2.Scalar23;
            tempScalar[19] += mv1.Scalar1 * mv2.KVector2.Scalar25 - mv1.Scalar2 * mv2.KVector2.Scalar15 + mv1.Scalar5 * mv2.KVector2.Scalar12;
            tempScalar[21] += mv1.Scalar1 * mv2.KVector2.Scalar35 - mv1.Scalar3 * mv2.KVector2.Scalar15 + mv1.Scalar5 * mv2.KVector2.Scalar13;
            tempScalar[22] += mv1.Scalar2 * mv2.KVector2.Scalar35 - mv1.Scalar3 * mv2.KVector2.Scalar25 + mv1.Scalar5 * mv2.KVector2.Scalar23;
            tempScalar[25] += mv1.Scalar1 * mv2.KVector2.Scalar45 - mv1.Scalar4 * mv2.KVector2.Scalar15 + mv1.Scalar5 * mv2.KVector2.Scalar14;
            tempScalar[26] += mv1.Scalar2 * mv2.KVector2.Scalar45 - mv1.Scalar4 * mv2.KVector2.Scalar25 + mv1.Scalar5 * mv2.KVector2.Scalar24;
            tempScalar[28] += mv1.Scalar3 * mv2.KVector2.Scalar45 - mv1.Scalar4 * mv2.KVector2.Scalar35 + mv1.Scalar5 * mv2.KVector2.Scalar34;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[3] += mv1.Scalar3 * mv2.KVector3.Scalar123 + mv1.Scalar4 * mv2.KVector3.Scalar124 + mv1.Scalar5 * mv2.KVector3.Scalar125;
            tempScalar[5] += -mv1.Scalar2 * mv2.KVector3.Scalar123 + mv1.Scalar4 * mv2.KVector3.Scalar134 + mv1.Scalar5 * mv2.KVector3.Scalar135;
            tempScalar[6] += -mv1.Scalar1 * mv2.KVector3.Scalar123 + mv1.Scalar4 * mv2.KVector3.Scalar234 + mv1.Scalar5 * mv2.KVector3.Scalar235;
            tempScalar[9] += -mv1.Scalar2 * mv2.KVector3.Scalar124 - mv1.Scalar3 * mv2.KVector3.Scalar134 + mv1.Scalar5 * mv2.KVector3.Scalar145;
            tempScalar[10] += -mv1.Scalar1 * mv2.KVector3.Scalar124 - mv1.Scalar3 * mv2.KVector3.Scalar234 + mv1.Scalar5 * mv2.KVector3.Scalar245;
            tempScalar[12] += -mv1.Scalar1 * mv2.KVector3.Scalar134 + mv1.Scalar2 * mv2.KVector3.Scalar234 + mv1.Scalar5 * mv2.KVector3.Scalar345;
            tempScalar[17] += -mv1.Scalar2 * mv2.KVector3.Scalar125 - mv1.Scalar3 * mv2.KVector3.Scalar135 - mv1.Scalar4 * mv2.KVector3.Scalar145;
            tempScalar[18] += -mv1.Scalar1 * mv2.KVector3.Scalar125 - mv1.Scalar3 * mv2.KVector3.Scalar235 - mv1.Scalar4 * mv2.KVector3.Scalar245;
            tempScalar[20] += -mv1.Scalar1 * mv2.KVector3.Scalar135 + mv1.Scalar2 * mv2.KVector3.Scalar235 - mv1.Scalar4 * mv2.KVector3.Scalar345;
            tempScalar[24] += -mv1.Scalar1 * mv2.KVector3.Scalar145 + mv1.Scalar2 * mv2.KVector3.Scalar245 + mv1.Scalar3 * mv2.KVector3.Scalar345;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[31] += mv1.Scalar1 * mv2.KVector4.Scalar2345 - mv1.Scalar2 * mv2.KVector4.Scalar1345 + mv1.Scalar3 * mv2.KVector4.Scalar1245 - mv1.Scalar4 * mv2.KVector4.Scalar1235 + mv1.Scalar5 * mv2.KVector4.Scalar1234;
        }
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[15] += mv1.Scalar5 * mv2.KVector5.Scalar12345;
            tempScalar[23] += -mv1.Scalar4 * mv2.KVector5.Scalar12345;
            tempScalar[27] += mv1.Scalar3 * mv2.KVector5.Scalar12345;
            tempScalar[29] += -mv1.Scalar2 * mv2.KVector5.Scalar12345;
            tempScalar[30] += -mv1.Scalar1 * mv2.KVector5.Scalar12345;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector2 Acp(this Ga41KVector2 mv1, Ga41KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector2.Zero;
        
        return new Ga41KVector2
        {
            Scalar12 = mv1.Scalar12 * mv2.Scalar,
            Scalar13 = mv1.Scalar13 * mv2.Scalar,
            Scalar23 = mv1.Scalar23 * mv2.Scalar,
            Scalar14 = mv1.Scalar14 * mv2.Scalar,
            Scalar24 = mv1.Scalar24 * mv2.Scalar,
            Scalar34 = mv1.Scalar34 * mv2.Scalar,
            Scalar15 = mv1.Scalar15 * mv2.Scalar,
            Scalar25 = mv1.Scalar25 * mv2.Scalar,
            Scalar35 = mv1.Scalar35 * mv2.Scalar,
            Scalar45 = mv1.Scalar45 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector3 Acp(this Ga41KVector2 mv1, Ga41KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector3.Zero;
        
        return new Ga41KVector3
        {
            Scalar123 = mv1.Scalar12 * mv2.Scalar3 - mv1.Scalar13 * mv2.Scalar2 + mv1.Scalar23 * mv2.Scalar1,
            Scalar124 = mv1.Scalar12 * mv2.Scalar4 - mv1.Scalar14 * mv2.Scalar2 + mv1.Scalar24 * mv2.Scalar1,
            Scalar134 = mv1.Scalar13 * mv2.Scalar4 - mv1.Scalar14 * mv2.Scalar3 + mv1.Scalar34 * mv2.Scalar1,
            Scalar234 = mv1.Scalar23 * mv2.Scalar4 - mv1.Scalar24 * mv2.Scalar3 + mv1.Scalar34 * mv2.Scalar2,
            Scalar125 = mv1.Scalar12 * mv2.Scalar5 - mv1.Scalar15 * mv2.Scalar2 + mv1.Scalar25 * mv2.Scalar1,
            Scalar135 = mv1.Scalar13 * mv2.Scalar5 - mv1.Scalar15 * mv2.Scalar3 + mv1.Scalar35 * mv2.Scalar1,
            Scalar235 = mv1.Scalar23 * mv2.Scalar5 - mv1.Scalar25 * mv2.Scalar3 + mv1.Scalar35 * mv2.Scalar2,
            Scalar145 = mv1.Scalar14 * mv2.Scalar5 - mv1.Scalar15 * mv2.Scalar4 + mv1.Scalar45 * mv2.Scalar1,
            Scalar245 = mv1.Scalar24 * mv2.Scalar5 - mv1.Scalar25 * mv2.Scalar4 + mv1.Scalar45 * mv2.Scalar2,
            Scalar345 = mv1.Scalar34 * mv2.Scalar5 - mv1.Scalar35 * mv2.Scalar4 + mv1.Scalar45 * mv2.Scalar3
        };
    }
    
    public static Ga41Multivector Acp(this Ga41KVector2 mv1, Ga41KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv2.IsZero())
        {
            tempScalar[0] += mv1.Scalar12 * mv2.Scalar12 + mv1.Scalar13 * mv2.Scalar13 - mv1.Scalar23 * mv2.Scalar23 + mv1.Scalar14 * mv2.Scalar14 - mv1.Scalar24 * mv2.Scalar24 - mv1.Scalar34 * mv2.Scalar34 + mv1.Scalar15 * mv2.Scalar15 - mv1.Scalar25 * mv2.Scalar25 - mv1.Scalar35 * mv2.Scalar35 - mv1.Scalar45 * mv2.Scalar45;
            tempScalar[15] += mv1.Scalar12 * mv2.Scalar34 - mv1.Scalar13 * mv2.Scalar24 + mv1.Scalar23 * mv2.Scalar14 + mv1.Scalar14 * mv2.Scalar23 - mv1.Scalar24 * mv2.Scalar13 + mv1.Scalar34 * mv2.Scalar12;
            tempScalar[23] += mv1.Scalar12 * mv2.Scalar35 - mv1.Scalar13 * mv2.Scalar25 + mv1.Scalar23 * mv2.Scalar15 + mv1.Scalar15 * mv2.Scalar23 - mv1.Scalar25 * mv2.Scalar13 + mv1.Scalar35 * mv2.Scalar12;
            tempScalar[27] += mv1.Scalar12 * mv2.Scalar45 - mv1.Scalar14 * mv2.Scalar25 + mv1.Scalar24 * mv2.Scalar15 + mv1.Scalar15 * mv2.Scalar24 - mv1.Scalar25 * mv2.Scalar14 + mv1.Scalar45 * mv2.Scalar12;
            tempScalar[29] += mv1.Scalar13 * mv2.Scalar45 - mv1.Scalar14 * mv2.Scalar35 + mv1.Scalar34 * mv2.Scalar15 + mv1.Scalar15 * mv2.Scalar34 - mv1.Scalar35 * mv2.Scalar14 + mv1.Scalar45 * mv2.Scalar13;
            tempScalar[30] += mv1.Scalar23 * mv2.Scalar45 - mv1.Scalar24 * mv2.Scalar35 + mv1.Scalar34 * mv2.Scalar25 + mv1.Scalar25 * mv2.Scalar34 - mv1.Scalar35 * mv2.Scalar24 + mv1.Scalar45 * mv2.Scalar23;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    public static Ga41Multivector Acp(this Ga41KVector2 mv1, Ga41KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv2.IsZero())
        {
            tempScalar[1] += -mv1.Scalar23 * mv2.Scalar123 - mv1.Scalar24 * mv2.Scalar124 - mv1.Scalar34 * mv2.Scalar134 - mv1.Scalar25 * mv2.Scalar125 - mv1.Scalar35 * mv2.Scalar135 - mv1.Scalar45 * mv2.Scalar145;
            tempScalar[2] += -mv1.Scalar13 * mv2.Scalar123 - mv1.Scalar14 * mv2.Scalar124 - mv1.Scalar34 * mv2.Scalar234 - mv1.Scalar15 * mv2.Scalar125 - mv1.Scalar35 * mv2.Scalar235 - mv1.Scalar45 * mv2.Scalar245;
            tempScalar[4] += mv1.Scalar12 * mv2.Scalar123 - mv1.Scalar14 * mv2.Scalar134 + mv1.Scalar24 * mv2.Scalar234 - mv1.Scalar15 * mv2.Scalar135 + mv1.Scalar25 * mv2.Scalar235 - mv1.Scalar45 * mv2.Scalar345;
            tempScalar[8] += mv1.Scalar12 * mv2.Scalar124 + mv1.Scalar13 * mv2.Scalar134 - mv1.Scalar23 * mv2.Scalar234 - mv1.Scalar15 * mv2.Scalar145 + mv1.Scalar25 * mv2.Scalar245 + mv1.Scalar35 * mv2.Scalar345;
            tempScalar[16] += mv1.Scalar12 * mv2.Scalar125 + mv1.Scalar13 * mv2.Scalar135 - mv1.Scalar23 * mv2.Scalar235 + mv1.Scalar14 * mv2.Scalar145 - mv1.Scalar24 * mv2.Scalar245 - mv1.Scalar34 * mv2.Scalar345;
            tempScalar[31] += mv1.Scalar12 * mv2.Scalar345 - mv1.Scalar13 * mv2.Scalar245 + mv1.Scalar23 * mv2.Scalar145 + mv1.Scalar14 * mv2.Scalar235 - mv1.Scalar24 * mv2.Scalar135 + mv1.Scalar34 * mv2.Scalar125 - mv1.Scalar15 * mv2.Scalar234 + mv1.Scalar25 * mv2.Scalar134 - mv1.Scalar35 * mv2.Scalar124 + mv1.Scalar45 * mv2.Scalar123;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector2 Acp(this Ga41KVector2 mv1, Ga41KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector2.Zero;
        
        return new Ga41KVector2
        {
            Scalar12 = -mv1.Scalar34 * mv2.Scalar1234 - mv1.Scalar35 * mv2.Scalar1235 - mv1.Scalar45 * mv2.Scalar1245,
            Scalar13 = mv1.Scalar24 * mv2.Scalar1234 + mv1.Scalar25 * mv2.Scalar1235 - mv1.Scalar45 * mv2.Scalar1345,
            Scalar23 = mv1.Scalar14 * mv2.Scalar1234 + mv1.Scalar15 * mv2.Scalar1235 - mv1.Scalar45 * mv2.Scalar2345,
            Scalar14 = -mv1.Scalar23 * mv2.Scalar1234 + mv1.Scalar25 * mv2.Scalar1245 + mv1.Scalar35 * mv2.Scalar1345,
            Scalar24 = -mv1.Scalar13 * mv2.Scalar1234 + mv1.Scalar15 * mv2.Scalar1245 + mv1.Scalar35 * mv2.Scalar2345,
            Scalar34 = mv1.Scalar12 * mv2.Scalar1234 + mv1.Scalar15 * mv2.Scalar1345 - mv1.Scalar25 * mv2.Scalar2345,
            Scalar15 = -mv1.Scalar23 * mv2.Scalar1235 - mv1.Scalar24 * mv2.Scalar1245 - mv1.Scalar34 * mv2.Scalar1345,
            Scalar25 = -mv1.Scalar13 * mv2.Scalar1235 - mv1.Scalar14 * mv2.Scalar1245 - mv1.Scalar34 * mv2.Scalar2345,
            Scalar35 = mv1.Scalar12 * mv2.Scalar1235 - mv1.Scalar14 * mv2.Scalar1345 + mv1.Scalar24 * mv2.Scalar2345,
            Scalar45 = mv1.Scalar12 * mv2.Scalar1245 + mv1.Scalar13 * mv2.Scalar1345 - mv1.Scalar23 * mv2.Scalar2345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector3 Acp(this Ga41KVector2 mv1, Ga41KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector3.Zero;
        
        return new Ga41KVector3
        {
            Scalar123 = -mv1.Scalar45 * mv2.Scalar12345,
            Scalar124 = mv1.Scalar35 * mv2.Scalar12345,
            Scalar134 = -mv1.Scalar25 * mv2.Scalar12345,
            Scalar234 = -mv1.Scalar15 * mv2.Scalar12345,
            Scalar125 = -mv1.Scalar34 * mv2.Scalar12345,
            Scalar135 = mv1.Scalar24 * mv2.Scalar12345,
            Scalar235 = mv1.Scalar14 * mv2.Scalar12345,
            Scalar145 = -mv1.Scalar23 * mv2.Scalar12345,
            Scalar245 = -mv1.Scalar13 * mv2.Scalar12345,
            Scalar345 = mv1.Scalar12 * mv2.Scalar12345
        };
    }
    
    public static Ga41Multivector Acp(this Ga41KVector2 mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[3] += mv1.Scalar12 * mv2.KVector0.Scalar;
            tempScalar[5] += mv1.Scalar13 * mv2.KVector0.Scalar;
            tempScalar[6] += mv1.Scalar23 * mv2.KVector0.Scalar;
            tempScalar[9] += mv1.Scalar14 * mv2.KVector0.Scalar;
            tempScalar[10] += mv1.Scalar24 * mv2.KVector0.Scalar;
            tempScalar[12] += mv1.Scalar34 * mv2.KVector0.Scalar;
            tempScalar[17] += mv1.Scalar15 * mv2.KVector0.Scalar;
            tempScalar[18] += mv1.Scalar25 * mv2.KVector0.Scalar;
            tempScalar[20] += mv1.Scalar35 * mv2.KVector0.Scalar;
            tempScalar[24] += mv1.Scalar45 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[7] += mv1.Scalar12 * mv2.KVector1.Scalar3 - mv1.Scalar13 * mv2.KVector1.Scalar2 + mv1.Scalar23 * mv2.KVector1.Scalar1;
            tempScalar[11] += mv1.Scalar12 * mv2.KVector1.Scalar4 - mv1.Scalar14 * mv2.KVector1.Scalar2 + mv1.Scalar24 * mv2.KVector1.Scalar1;
            tempScalar[13] += mv1.Scalar13 * mv2.KVector1.Scalar4 - mv1.Scalar14 * mv2.KVector1.Scalar3 + mv1.Scalar34 * mv2.KVector1.Scalar1;
            tempScalar[14] += mv1.Scalar23 * mv2.KVector1.Scalar4 - mv1.Scalar24 * mv2.KVector1.Scalar3 + mv1.Scalar34 * mv2.KVector1.Scalar2;
            tempScalar[19] += mv1.Scalar12 * mv2.KVector1.Scalar5 - mv1.Scalar15 * mv2.KVector1.Scalar2 + mv1.Scalar25 * mv2.KVector1.Scalar1;
            tempScalar[21] += mv1.Scalar13 * mv2.KVector1.Scalar5 - mv1.Scalar15 * mv2.KVector1.Scalar3 + mv1.Scalar35 * mv2.KVector1.Scalar1;
            tempScalar[22] += mv1.Scalar23 * mv2.KVector1.Scalar5 - mv1.Scalar25 * mv2.KVector1.Scalar3 + mv1.Scalar35 * mv2.KVector1.Scalar2;
            tempScalar[25] += mv1.Scalar14 * mv2.KVector1.Scalar5 - mv1.Scalar15 * mv2.KVector1.Scalar4 + mv1.Scalar45 * mv2.KVector1.Scalar1;
            tempScalar[26] += mv1.Scalar24 * mv2.KVector1.Scalar5 - mv1.Scalar25 * mv2.KVector1.Scalar4 + mv1.Scalar45 * mv2.KVector1.Scalar2;
            tempScalar[28] += mv1.Scalar34 * mv2.KVector1.Scalar5 - mv1.Scalar35 * mv2.KVector1.Scalar4 + mv1.Scalar45 * mv2.KVector1.Scalar3;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[0] += mv1.Scalar12 * mv2.KVector2.Scalar12 + mv1.Scalar13 * mv2.KVector2.Scalar13 - mv1.Scalar23 * mv2.KVector2.Scalar23 + mv1.Scalar14 * mv2.KVector2.Scalar14 - mv1.Scalar24 * mv2.KVector2.Scalar24 - mv1.Scalar34 * mv2.KVector2.Scalar34 + mv1.Scalar15 * mv2.KVector2.Scalar15 - mv1.Scalar25 * mv2.KVector2.Scalar25 - mv1.Scalar35 * mv2.KVector2.Scalar35 - mv1.Scalar45 * mv2.KVector2.Scalar45;
            tempScalar[15] += mv1.Scalar12 * mv2.KVector2.Scalar34 - mv1.Scalar13 * mv2.KVector2.Scalar24 + mv1.Scalar23 * mv2.KVector2.Scalar14 + mv1.Scalar14 * mv2.KVector2.Scalar23 - mv1.Scalar24 * mv2.KVector2.Scalar13 + mv1.Scalar34 * mv2.KVector2.Scalar12;
            tempScalar[23] += mv1.Scalar12 * mv2.KVector2.Scalar35 - mv1.Scalar13 * mv2.KVector2.Scalar25 + mv1.Scalar23 * mv2.KVector2.Scalar15 + mv1.Scalar15 * mv2.KVector2.Scalar23 - mv1.Scalar25 * mv2.KVector2.Scalar13 + mv1.Scalar35 * mv2.KVector2.Scalar12;
            tempScalar[27] += mv1.Scalar12 * mv2.KVector2.Scalar45 - mv1.Scalar14 * mv2.KVector2.Scalar25 + mv1.Scalar24 * mv2.KVector2.Scalar15 + mv1.Scalar15 * mv2.KVector2.Scalar24 - mv1.Scalar25 * mv2.KVector2.Scalar14 + mv1.Scalar45 * mv2.KVector2.Scalar12;
            tempScalar[29] += mv1.Scalar13 * mv2.KVector2.Scalar45 - mv1.Scalar14 * mv2.KVector2.Scalar35 + mv1.Scalar34 * mv2.KVector2.Scalar15 + mv1.Scalar15 * mv2.KVector2.Scalar34 - mv1.Scalar35 * mv2.KVector2.Scalar14 + mv1.Scalar45 * mv2.KVector2.Scalar13;
            tempScalar[30] += mv1.Scalar23 * mv2.KVector2.Scalar45 - mv1.Scalar24 * mv2.KVector2.Scalar35 + mv1.Scalar34 * mv2.KVector2.Scalar25 + mv1.Scalar25 * mv2.KVector2.Scalar34 - mv1.Scalar35 * mv2.KVector2.Scalar24 + mv1.Scalar45 * mv2.KVector2.Scalar23;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[1] += -mv1.Scalar23 * mv2.KVector3.Scalar123 - mv1.Scalar24 * mv2.KVector3.Scalar124 - mv1.Scalar34 * mv2.KVector3.Scalar134 - mv1.Scalar25 * mv2.KVector3.Scalar125 - mv1.Scalar35 * mv2.KVector3.Scalar135 - mv1.Scalar45 * mv2.KVector3.Scalar145;
            tempScalar[2] += -mv1.Scalar13 * mv2.KVector3.Scalar123 - mv1.Scalar14 * mv2.KVector3.Scalar124 - mv1.Scalar34 * mv2.KVector3.Scalar234 - mv1.Scalar15 * mv2.KVector3.Scalar125 - mv1.Scalar35 * mv2.KVector3.Scalar235 - mv1.Scalar45 * mv2.KVector3.Scalar245;
            tempScalar[4] += mv1.Scalar12 * mv2.KVector3.Scalar123 - mv1.Scalar14 * mv2.KVector3.Scalar134 + mv1.Scalar24 * mv2.KVector3.Scalar234 - mv1.Scalar15 * mv2.KVector3.Scalar135 + mv1.Scalar25 * mv2.KVector3.Scalar235 - mv1.Scalar45 * mv2.KVector3.Scalar345;
            tempScalar[8] += mv1.Scalar12 * mv2.KVector3.Scalar124 + mv1.Scalar13 * mv2.KVector3.Scalar134 - mv1.Scalar23 * mv2.KVector3.Scalar234 - mv1.Scalar15 * mv2.KVector3.Scalar145 + mv1.Scalar25 * mv2.KVector3.Scalar245 + mv1.Scalar35 * mv2.KVector3.Scalar345;
            tempScalar[16] += mv1.Scalar12 * mv2.KVector3.Scalar125 + mv1.Scalar13 * mv2.KVector3.Scalar135 - mv1.Scalar23 * mv2.KVector3.Scalar235 + mv1.Scalar14 * mv2.KVector3.Scalar145 - mv1.Scalar24 * mv2.KVector3.Scalar245 - mv1.Scalar34 * mv2.KVector3.Scalar345;
            tempScalar[31] += mv1.Scalar12 * mv2.KVector3.Scalar345 - mv1.Scalar13 * mv2.KVector3.Scalar245 + mv1.Scalar23 * mv2.KVector3.Scalar145 + mv1.Scalar14 * mv2.KVector3.Scalar235 - mv1.Scalar24 * mv2.KVector3.Scalar135 + mv1.Scalar34 * mv2.KVector3.Scalar125 - mv1.Scalar15 * mv2.KVector3.Scalar234 + mv1.Scalar25 * mv2.KVector3.Scalar134 - mv1.Scalar35 * mv2.KVector3.Scalar124 + mv1.Scalar45 * mv2.KVector3.Scalar123;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[3] += -mv1.Scalar34 * mv2.KVector4.Scalar1234 - mv1.Scalar35 * mv2.KVector4.Scalar1235 - mv1.Scalar45 * mv2.KVector4.Scalar1245;
            tempScalar[5] += mv1.Scalar24 * mv2.KVector4.Scalar1234 + mv1.Scalar25 * mv2.KVector4.Scalar1235 - mv1.Scalar45 * mv2.KVector4.Scalar1345;
            tempScalar[6] += mv1.Scalar14 * mv2.KVector4.Scalar1234 + mv1.Scalar15 * mv2.KVector4.Scalar1235 - mv1.Scalar45 * mv2.KVector4.Scalar2345;
            tempScalar[9] += -mv1.Scalar23 * mv2.KVector4.Scalar1234 + mv1.Scalar25 * mv2.KVector4.Scalar1245 + mv1.Scalar35 * mv2.KVector4.Scalar1345;
            tempScalar[10] += -mv1.Scalar13 * mv2.KVector4.Scalar1234 + mv1.Scalar15 * mv2.KVector4.Scalar1245 + mv1.Scalar35 * mv2.KVector4.Scalar2345;
            tempScalar[12] += mv1.Scalar12 * mv2.KVector4.Scalar1234 + mv1.Scalar15 * mv2.KVector4.Scalar1345 - mv1.Scalar25 * mv2.KVector4.Scalar2345;
            tempScalar[17] += -mv1.Scalar23 * mv2.KVector4.Scalar1235 - mv1.Scalar24 * mv2.KVector4.Scalar1245 - mv1.Scalar34 * mv2.KVector4.Scalar1345;
            tempScalar[18] += -mv1.Scalar13 * mv2.KVector4.Scalar1235 - mv1.Scalar14 * mv2.KVector4.Scalar1245 - mv1.Scalar34 * mv2.KVector4.Scalar2345;
            tempScalar[20] += mv1.Scalar12 * mv2.KVector4.Scalar1235 - mv1.Scalar14 * mv2.KVector4.Scalar1345 + mv1.Scalar24 * mv2.KVector4.Scalar2345;
            tempScalar[24] += mv1.Scalar12 * mv2.KVector4.Scalar1245 + mv1.Scalar13 * mv2.KVector4.Scalar1345 - mv1.Scalar23 * mv2.KVector4.Scalar2345;
        }
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[7] += -mv1.Scalar45 * mv2.KVector5.Scalar12345;
            tempScalar[11] += mv1.Scalar35 * mv2.KVector5.Scalar12345;
            tempScalar[13] += -mv1.Scalar25 * mv2.KVector5.Scalar12345;
            tempScalar[14] += -mv1.Scalar15 * mv2.KVector5.Scalar12345;
            tempScalar[19] += -mv1.Scalar34 * mv2.KVector5.Scalar12345;
            tempScalar[21] += mv1.Scalar24 * mv2.KVector5.Scalar12345;
            tempScalar[22] += mv1.Scalar14 * mv2.KVector5.Scalar12345;
            tempScalar[25] += -mv1.Scalar23 * mv2.KVector5.Scalar12345;
            tempScalar[26] += -mv1.Scalar13 * mv2.KVector5.Scalar12345;
            tempScalar[28] += mv1.Scalar12 * mv2.KVector5.Scalar12345;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector3 Acp(this Ga41KVector3 mv1, Ga41KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector3.Zero;
        
        return new Ga41KVector3
        {
            Scalar123 = mv1.Scalar123 * mv2.Scalar,
            Scalar124 = mv1.Scalar124 * mv2.Scalar,
            Scalar134 = mv1.Scalar134 * mv2.Scalar,
            Scalar234 = mv1.Scalar234 * mv2.Scalar,
            Scalar125 = mv1.Scalar125 * mv2.Scalar,
            Scalar135 = mv1.Scalar135 * mv2.Scalar,
            Scalar235 = mv1.Scalar235 * mv2.Scalar,
            Scalar145 = mv1.Scalar145 * mv2.Scalar,
            Scalar245 = mv1.Scalar245 * mv2.Scalar,
            Scalar345 = mv1.Scalar345 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector2 Acp(this Ga41KVector3 mv1, Ga41KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector2.Zero;
        
        return new Ga41KVector2
        {
            Scalar12 = mv1.Scalar123 * mv2.Scalar3 + mv1.Scalar124 * mv2.Scalar4 + mv1.Scalar125 * mv2.Scalar5,
            Scalar13 = -mv1.Scalar123 * mv2.Scalar2 + mv1.Scalar134 * mv2.Scalar4 + mv1.Scalar135 * mv2.Scalar5,
            Scalar23 = -mv1.Scalar123 * mv2.Scalar1 + mv1.Scalar234 * mv2.Scalar4 + mv1.Scalar235 * mv2.Scalar5,
            Scalar14 = -mv1.Scalar124 * mv2.Scalar2 - mv1.Scalar134 * mv2.Scalar3 + mv1.Scalar145 * mv2.Scalar5,
            Scalar24 = -mv1.Scalar124 * mv2.Scalar1 - mv1.Scalar234 * mv2.Scalar3 + mv1.Scalar245 * mv2.Scalar5,
            Scalar34 = -mv1.Scalar134 * mv2.Scalar1 + mv1.Scalar234 * mv2.Scalar2 + mv1.Scalar345 * mv2.Scalar5,
            Scalar15 = -mv1.Scalar125 * mv2.Scalar2 - mv1.Scalar135 * mv2.Scalar3 - mv1.Scalar145 * mv2.Scalar4,
            Scalar25 = -mv1.Scalar125 * mv2.Scalar1 - mv1.Scalar235 * mv2.Scalar3 - mv1.Scalar245 * mv2.Scalar4,
            Scalar35 = -mv1.Scalar135 * mv2.Scalar1 + mv1.Scalar235 * mv2.Scalar2 - mv1.Scalar345 * mv2.Scalar4,
            Scalar45 = -mv1.Scalar145 * mv2.Scalar1 + mv1.Scalar245 * mv2.Scalar2 + mv1.Scalar345 * mv2.Scalar3
        };
    }
    
    public static Ga41Multivector Acp(this Ga41KVector3 mv1, Ga41KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv2.IsZero())
        {
            tempScalar[1] += -mv1.Scalar123 * mv2.Scalar23 - mv1.Scalar124 * mv2.Scalar24 - mv1.Scalar134 * mv2.Scalar34 - mv1.Scalar125 * mv2.Scalar25 - mv1.Scalar135 * mv2.Scalar35 - mv1.Scalar145 * mv2.Scalar45;
            tempScalar[2] += -mv1.Scalar123 * mv2.Scalar13 - mv1.Scalar124 * mv2.Scalar14 - mv1.Scalar234 * mv2.Scalar34 - mv1.Scalar125 * mv2.Scalar15 - mv1.Scalar235 * mv2.Scalar35 - mv1.Scalar245 * mv2.Scalar45;
            tempScalar[4] += mv1.Scalar123 * mv2.Scalar12 - mv1.Scalar134 * mv2.Scalar14 + mv1.Scalar234 * mv2.Scalar24 - mv1.Scalar135 * mv2.Scalar15 + mv1.Scalar235 * mv2.Scalar25 - mv1.Scalar345 * mv2.Scalar45;
            tempScalar[8] += mv1.Scalar124 * mv2.Scalar12 + mv1.Scalar134 * mv2.Scalar13 - mv1.Scalar234 * mv2.Scalar23 - mv1.Scalar145 * mv2.Scalar15 + mv1.Scalar245 * mv2.Scalar25 + mv1.Scalar345 * mv2.Scalar35;
            tempScalar[16] += mv1.Scalar125 * mv2.Scalar12 + mv1.Scalar135 * mv2.Scalar13 - mv1.Scalar235 * mv2.Scalar23 + mv1.Scalar145 * mv2.Scalar14 - mv1.Scalar245 * mv2.Scalar24 - mv1.Scalar345 * mv2.Scalar34;
            tempScalar[31] += mv1.Scalar123 * mv2.Scalar45 - mv1.Scalar124 * mv2.Scalar35 + mv1.Scalar134 * mv2.Scalar25 - mv1.Scalar234 * mv2.Scalar15 + mv1.Scalar125 * mv2.Scalar34 - mv1.Scalar135 * mv2.Scalar24 + mv1.Scalar235 * mv2.Scalar14 + mv1.Scalar145 * mv2.Scalar23 - mv1.Scalar245 * mv2.Scalar13 + mv1.Scalar345 * mv2.Scalar12;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    public static Ga41Multivector Acp(this Ga41KVector3 mv1, Ga41KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv2.IsZero())
        {
            tempScalar[0] += mv1.Scalar123 * mv2.Scalar123 + mv1.Scalar124 * mv2.Scalar124 + mv1.Scalar134 * mv2.Scalar134 - mv1.Scalar234 * mv2.Scalar234 + mv1.Scalar125 * mv2.Scalar125 + mv1.Scalar135 * mv2.Scalar135 - mv1.Scalar235 * mv2.Scalar235 + mv1.Scalar145 * mv2.Scalar145 - mv1.Scalar245 * mv2.Scalar245 - mv1.Scalar345 * mv2.Scalar345;
            tempScalar[15] += mv1.Scalar125 * mv2.Scalar345 - mv1.Scalar135 * mv2.Scalar245 + mv1.Scalar235 * mv2.Scalar145 + mv1.Scalar145 * mv2.Scalar235 - mv1.Scalar245 * mv2.Scalar135 + mv1.Scalar345 * mv2.Scalar125;
            tempScalar[23] += -mv1.Scalar124 * mv2.Scalar345 + mv1.Scalar134 * mv2.Scalar245 - mv1.Scalar234 * mv2.Scalar145 - mv1.Scalar145 * mv2.Scalar234 + mv1.Scalar245 * mv2.Scalar134 - mv1.Scalar345 * mv2.Scalar124;
            tempScalar[27] += mv1.Scalar123 * mv2.Scalar345 - mv1.Scalar134 * mv2.Scalar235 + mv1.Scalar234 * mv2.Scalar135 + mv1.Scalar135 * mv2.Scalar234 - mv1.Scalar235 * mv2.Scalar134 + mv1.Scalar345 * mv2.Scalar123;
            tempScalar[29] += -mv1.Scalar123 * mv2.Scalar245 + mv1.Scalar124 * mv2.Scalar235 - mv1.Scalar234 * mv2.Scalar125 - mv1.Scalar125 * mv2.Scalar234 + mv1.Scalar235 * mv2.Scalar124 - mv1.Scalar245 * mv2.Scalar123;
            tempScalar[30] += -mv1.Scalar123 * mv2.Scalar145 + mv1.Scalar124 * mv2.Scalar135 - mv1.Scalar134 * mv2.Scalar125 - mv1.Scalar125 * mv2.Scalar134 + mv1.Scalar135 * mv2.Scalar124 - mv1.Scalar145 * mv2.Scalar123;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector3 Acp(this Ga41KVector3 mv1, Ga41KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector3.Zero;
        
        return new Ga41KVector3
        {
            Scalar123 = -mv1.Scalar145 * mv2.Scalar2345 + mv1.Scalar245 * mv2.Scalar1345 - mv1.Scalar345 * mv2.Scalar1245,
            Scalar124 = mv1.Scalar135 * mv2.Scalar2345 - mv1.Scalar235 * mv2.Scalar1345 + mv1.Scalar345 * mv2.Scalar1235,
            Scalar134 = -mv1.Scalar125 * mv2.Scalar2345 + mv1.Scalar235 * mv2.Scalar1245 - mv1.Scalar245 * mv2.Scalar1235,
            Scalar234 = -mv1.Scalar125 * mv2.Scalar1345 + mv1.Scalar135 * mv2.Scalar1245 - mv1.Scalar145 * mv2.Scalar1235,
            Scalar125 = -mv1.Scalar134 * mv2.Scalar2345 + mv1.Scalar234 * mv2.Scalar1345 - mv1.Scalar345 * mv2.Scalar1234,
            Scalar135 = mv1.Scalar124 * mv2.Scalar2345 - mv1.Scalar234 * mv2.Scalar1245 + mv1.Scalar245 * mv2.Scalar1234,
            Scalar235 = mv1.Scalar124 * mv2.Scalar1345 - mv1.Scalar134 * mv2.Scalar1245 + mv1.Scalar145 * mv2.Scalar1234,
            Scalar145 = -mv1.Scalar123 * mv2.Scalar2345 + mv1.Scalar234 * mv2.Scalar1235 - mv1.Scalar235 * mv2.Scalar1234,
            Scalar245 = -mv1.Scalar123 * mv2.Scalar1345 + mv1.Scalar134 * mv2.Scalar1235 - mv1.Scalar135 * mv2.Scalar1234,
            Scalar345 = mv1.Scalar123 * mv2.Scalar1245 - mv1.Scalar124 * mv2.Scalar1235 + mv1.Scalar125 * mv2.Scalar1234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector2 Acp(this Ga41KVector3 mv1, Ga41KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector2.Zero;
        
        return new Ga41KVector2
        {
            Scalar12 = -mv1.Scalar345 * mv2.Scalar12345,
            Scalar13 = mv1.Scalar245 * mv2.Scalar12345,
            Scalar23 = mv1.Scalar145 * mv2.Scalar12345,
            Scalar14 = -mv1.Scalar235 * mv2.Scalar12345,
            Scalar24 = -mv1.Scalar135 * mv2.Scalar12345,
            Scalar34 = mv1.Scalar125 * mv2.Scalar12345,
            Scalar15 = mv1.Scalar234 * mv2.Scalar12345,
            Scalar25 = mv1.Scalar134 * mv2.Scalar12345,
            Scalar35 = -mv1.Scalar124 * mv2.Scalar12345,
            Scalar45 = mv1.Scalar123 * mv2.Scalar12345
        };
    }
    
    public static Ga41Multivector Acp(this Ga41KVector3 mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[7] += mv1.Scalar123 * mv2.KVector0.Scalar;
            tempScalar[11] += mv1.Scalar124 * mv2.KVector0.Scalar;
            tempScalar[13] += mv1.Scalar134 * mv2.KVector0.Scalar;
            tempScalar[14] += mv1.Scalar234 * mv2.KVector0.Scalar;
            tempScalar[19] += mv1.Scalar125 * mv2.KVector0.Scalar;
            tempScalar[21] += mv1.Scalar135 * mv2.KVector0.Scalar;
            tempScalar[22] += mv1.Scalar235 * mv2.KVector0.Scalar;
            tempScalar[25] += mv1.Scalar145 * mv2.KVector0.Scalar;
            tempScalar[26] += mv1.Scalar245 * mv2.KVector0.Scalar;
            tempScalar[28] += mv1.Scalar345 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[3] += mv1.Scalar123 * mv2.KVector1.Scalar3 + mv1.Scalar124 * mv2.KVector1.Scalar4 + mv1.Scalar125 * mv2.KVector1.Scalar5;
            tempScalar[5] += -mv1.Scalar123 * mv2.KVector1.Scalar2 + mv1.Scalar134 * mv2.KVector1.Scalar4 + mv1.Scalar135 * mv2.KVector1.Scalar5;
            tempScalar[6] += -mv1.Scalar123 * mv2.KVector1.Scalar1 + mv1.Scalar234 * mv2.KVector1.Scalar4 + mv1.Scalar235 * mv2.KVector1.Scalar5;
            tempScalar[9] += -mv1.Scalar124 * mv2.KVector1.Scalar2 - mv1.Scalar134 * mv2.KVector1.Scalar3 + mv1.Scalar145 * mv2.KVector1.Scalar5;
            tempScalar[10] += -mv1.Scalar124 * mv2.KVector1.Scalar1 - mv1.Scalar234 * mv2.KVector1.Scalar3 + mv1.Scalar245 * mv2.KVector1.Scalar5;
            tempScalar[12] += -mv1.Scalar134 * mv2.KVector1.Scalar1 + mv1.Scalar234 * mv2.KVector1.Scalar2 + mv1.Scalar345 * mv2.KVector1.Scalar5;
            tempScalar[17] += -mv1.Scalar125 * mv2.KVector1.Scalar2 - mv1.Scalar135 * mv2.KVector1.Scalar3 - mv1.Scalar145 * mv2.KVector1.Scalar4;
            tempScalar[18] += -mv1.Scalar125 * mv2.KVector1.Scalar1 - mv1.Scalar235 * mv2.KVector1.Scalar3 - mv1.Scalar245 * mv2.KVector1.Scalar4;
            tempScalar[20] += -mv1.Scalar135 * mv2.KVector1.Scalar1 + mv1.Scalar235 * mv2.KVector1.Scalar2 - mv1.Scalar345 * mv2.KVector1.Scalar4;
            tempScalar[24] += -mv1.Scalar145 * mv2.KVector1.Scalar1 + mv1.Scalar245 * mv2.KVector1.Scalar2 + mv1.Scalar345 * mv2.KVector1.Scalar3;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[1] += -mv1.Scalar123 * mv2.KVector2.Scalar23 - mv1.Scalar124 * mv2.KVector2.Scalar24 - mv1.Scalar134 * mv2.KVector2.Scalar34 - mv1.Scalar125 * mv2.KVector2.Scalar25 - mv1.Scalar135 * mv2.KVector2.Scalar35 - mv1.Scalar145 * mv2.KVector2.Scalar45;
            tempScalar[2] += -mv1.Scalar123 * mv2.KVector2.Scalar13 - mv1.Scalar124 * mv2.KVector2.Scalar14 - mv1.Scalar234 * mv2.KVector2.Scalar34 - mv1.Scalar125 * mv2.KVector2.Scalar15 - mv1.Scalar235 * mv2.KVector2.Scalar35 - mv1.Scalar245 * mv2.KVector2.Scalar45;
            tempScalar[4] += mv1.Scalar123 * mv2.KVector2.Scalar12 - mv1.Scalar134 * mv2.KVector2.Scalar14 + mv1.Scalar234 * mv2.KVector2.Scalar24 - mv1.Scalar135 * mv2.KVector2.Scalar15 + mv1.Scalar235 * mv2.KVector2.Scalar25 - mv1.Scalar345 * mv2.KVector2.Scalar45;
            tempScalar[8] += mv1.Scalar124 * mv2.KVector2.Scalar12 + mv1.Scalar134 * mv2.KVector2.Scalar13 - mv1.Scalar234 * mv2.KVector2.Scalar23 - mv1.Scalar145 * mv2.KVector2.Scalar15 + mv1.Scalar245 * mv2.KVector2.Scalar25 + mv1.Scalar345 * mv2.KVector2.Scalar35;
            tempScalar[16] += mv1.Scalar125 * mv2.KVector2.Scalar12 + mv1.Scalar135 * mv2.KVector2.Scalar13 - mv1.Scalar235 * mv2.KVector2.Scalar23 + mv1.Scalar145 * mv2.KVector2.Scalar14 - mv1.Scalar245 * mv2.KVector2.Scalar24 - mv1.Scalar345 * mv2.KVector2.Scalar34;
            tempScalar[31] += mv1.Scalar123 * mv2.KVector2.Scalar45 - mv1.Scalar124 * mv2.KVector2.Scalar35 + mv1.Scalar134 * mv2.KVector2.Scalar25 - mv1.Scalar234 * mv2.KVector2.Scalar15 + mv1.Scalar125 * mv2.KVector2.Scalar34 - mv1.Scalar135 * mv2.KVector2.Scalar24 + mv1.Scalar235 * mv2.KVector2.Scalar14 + mv1.Scalar145 * mv2.KVector2.Scalar23 - mv1.Scalar245 * mv2.KVector2.Scalar13 + mv1.Scalar345 * mv2.KVector2.Scalar12;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[0] += mv1.Scalar123 * mv2.KVector3.Scalar123 + mv1.Scalar124 * mv2.KVector3.Scalar124 + mv1.Scalar134 * mv2.KVector3.Scalar134 - mv1.Scalar234 * mv2.KVector3.Scalar234 + mv1.Scalar125 * mv2.KVector3.Scalar125 + mv1.Scalar135 * mv2.KVector3.Scalar135 - mv1.Scalar235 * mv2.KVector3.Scalar235 + mv1.Scalar145 * mv2.KVector3.Scalar145 - mv1.Scalar245 * mv2.KVector3.Scalar245 - mv1.Scalar345 * mv2.KVector3.Scalar345;
            tempScalar[15] += mv1.Scalar125 * mv2.KVector3.Scalar345 - mv1.Scalar135 * mv2.KVector3.Scalar245 + mv1.Scalar235 * mv2.KVector3.Scalar145 + mv1.Scalar145 * mv2.KVector3.Scalar235 - mv1.Scalar245 * mv2.KVector3.Scalar135 + mv1.Scalar345 * mv2.KVector3.Scalar125;
            tempScalar[23] += -mv1.Scalar124 * mv2.KVector3.Scalar345 + mv1.Scalar134 * mv2.KVector3.Scalar245 - mv1.Scalar234 * mv2.KVector3.Scalar145 - mv1.Scalar145 * mv2.KVector3.Scalar234 + mv1.Scalar245 * mv2.KVector3.Scalar134 - mv1.Scalar345 * mv2.KVector3.Scalar124;
            tempScalar[27] += mv1.Scalar123 * mv2.KVector3.Scalar345 - mv1.Scalar134 * mv2.KVector3.Scalar235 + mv1.Scalar234 * mv2.KVector3.Scalar135 + mv1.Scalar135 * mv2.KVector3.Scalar234 - mv1.Scalar235 * mv2.KVector3.Scalar134 + mv1.Scalar345 * mv2.KVector3.Scalar123;
            tempScalar[29] += -mv1.Scalar123 * mv2.KVector3.Scalar245 + mv1.Scalar124 * mv2.KVector3.Scalar235 - mv1.Scalar234 * mv2.KVector3.Scalar125 - mv1.Scalar125 * mv2.KVector3.Scalar234 + mv1.Scalar235 * mv2.KVector3.Scalar124 - mv1.Scalar245 * mv2.KVector3.Scalar123;
            tempScalar[30] += -mv1.Scalar123 * mv2.KVector3.Scalar145 + mv1.Scalar124 * mv2.KVector3.Scalar135 - mv1.Scalar134 * mv2.KVector3.Scalar125 - mv1.Scalar125 * mv2.KVector3.Scalar134 + mv1.Scalar135 * mv2.KVector3.Scalar124 - mv1.Scalar145 * mv2.KVector3.Scalar123;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[7] += -mv1.Scalar145 * mv2.KVector4.Scalar2345 + mv1.Scalar245 * mv2.KVector4.Scalar1345 - mv1.Scalar345 * mv2.KVector4.Scalar1245;
            tempScalar[11] += mv1.Scalar135 * mv2.KVector4.Scalar2345 - mv1.Scalar235 * mv2.KVector4.Scalar1345 + mv1.Scalar345 * mv2.KVector4.Scalar1235;
            tempScalar[13] += -mv1.Scalar125 * mv2.KVector4.Scalar2345 + mv1.Scalar235 * mv2.KVector4.Scalar1245 - mv1.Scalar245 * mv2.KVector4.Scalar1235;
            tempScalar[14] += -mv1.Scalar125 * mv2.KVector4.Scalar1345 + mv1.Scalar135 * mv2.KVector4.Scalar1245 - mv1.Scalar145 * mv2.KVector4.Scalar1235;
            tempScalar[19] += -mv1.Scalar134 * mv2.KVector4.Scalar2345 + mv1.Scalar234 * mv2.KVector4.Scalar1345 - mv1.Scalar345 * mv2.KVector4.Scalar1234;
            tempScalar[21] += mv1.Scalar124 * mv2.KVector4.Scalar2345 - mv1.Scalar234 * mv2.KVector4.Scalar1245 + mv1.Scalar245 * mv2.KVector4.Scalar1234;
            tempScalar[22] += mv1.Scalar124 * mv2.KVector4.Scalar1345 - mv1.Scalar134 * mv2.KVector4.Scalar1245 + mv1.Scalar145 * mv2.KVector4.Scalar1234;
            tempScalar[25] += -mv1.Scalar123 * mv2.KVector4.Scalar2345 + mv1.Scalar234 * mv2.KVector4.Scalar1235 - mv1.Scalar235 * mv2.KVector4.Scalar1234;
            tempScalar[26] += -mv1.Scalar123 * mv2.KVector4.Scalar1345 + mv1.Scalar134 * mv2.KVector4.Scalar1235 - mv1.Scalar135 * mv2.KVector4.Scalar1234;
            tempScalar[28] += mv1.Scalar123 * mv2.KVector4.Scalar1245 - mv1.Scalar124 * mv2.KVector4.Scalar1235 + mv1.Scalar125 * mv2.KVector4.Scalar1234;
        }
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[3] += -mv1.Scalar345 * mv2.KVector5.Scalar12345;
            tempScalar[5] += mv1.Scalar245 * mv2.KVector5.Scalar12345;
            tempScalar[6] += mv1.Scalar145 * mv2.KVector5.Scalar12345;
            tempScalar[9] += -mv1.Scalar235 * mv2.KVector5.Scalar12345;
            tempScalar[10] += -mv1.Scalar135 * mv2.KVector5.Scalar12345;
            tempScalar[12] += mv1.Scalar125 * mv2.KVector5.Scalar12345;
            tempScalar[17] += mv1.Scalar234 * mv2.KVector5.Scalar12345;
            tempScalar[18] += mv1.Scalar134 * mv2.KVector5.Scalar12345;
            tempScalar[20] += -mv1.Scalar124 * mv2.KVector5.Scalar12345;
            tempScalar[24] += mv1.Scalar123 * mv2.KVector5.Scalar12345;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector4 Acp(this Ga41KVector4 mv1, Ga41KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector4.Zero;
        
        return new Ga41KVector4
        {
            Scalar1234 = mv1.Scalar1234 * mv2.Scalar,
            Scalar1235 = mv1.Scalar1235 * mv2.Scalar,
            Scalar1245 = mv1.Scalar1245 * mv2.Scalar,
            Scalar1345 = mv1.Scalar1345 * mv2.Scalar,
            Scalar2345 = mv1.Scalar2345 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector5 Acp(this Ga41KVector4 mv1, Ga41KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector5.Zero;
        
        return new Ga41KVector5
        {
            Scalar12345 = mv1.Scalar1234 * mv2.Scalar5 - mv1.Scalar1235 * mv2.Scalar4 + mv1.Scalar1245 * mv2.Scalar3 - mv1.Scalar1345 * mv2.Scalar2 + mv1.Scalar2345 * mv2.Scalar1
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector2 Acp(this Ga41KVector4 mv1, Ga41KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector2.Zero;
        
        return new Ga41KVector2
        {
            Scalar12 = -mv1.Scalar1234 * mv2.Scalar34 - mv1.Scalar1235 * mv2.Scalar35 - mv1.Scalar1245 * mv2.Scalar45,
            Scalar13 = mv1.Scalar1234 * mv2.Scalar24 + mv1.Scalar1235 * mv2.Scalar25 - mv1.Scalar1345 * mv2.Scalar45,
            Scalar23 = mv1.Scalar1234 * mv2.Scalar14 + mv1.Scalar1235 * mv2.Scalar15 - mv1.Scalar2345 * mv2.Scalar45,
            Scalar14 = -mv1.Scalar1234 * mv2.Scalar23 + mv1.Scalar1245 * mv2.Scalar25 + mv1.Scalar1345 * mv2.Scalar35,
            Scalar24 = -mv1.Scalar1234 * mv2.Scalar13 + mv1.Scalar1245 * mv2.Scalar15 + mv1.Scalar2345 * mv2.Scalar35,
            Scalar34 = mv1.Scalar1234 * mv2.Scalar12 + mv1.Scalar1345 * mv2.Scalar15 - mv1.Scalar2345 * mv2.Scalar25,
            Scalar15 = -mv1.Scalar1235 * mv2.Scalar23 - mv1.Scalar1245 * mv2.Scalar24 - mv1.Scalar1345 * mv2.Scalar34,
            Scalar25 = -mv1.Scalar1235 * mv2.Scalar13 - mv1.Scalar1245 * mv2.Scalar14 - mv1.Scalar2345 * mv2.Scalar34,
            Scalar35 = mv1.Scalar1235 * mv2.Scalar12 - mv1.Scalar1345 * mv2.Scalar14 + mv1.Scalar2345 * mv2.Scalar24,
            Scalar45 = mv1.Scalar1245 * mv2.Scalar12 + mv1.Scalar1345 * mv2.Scalar13 - mv1.Scalar2345 * mv2.Scalar23
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector3 Acp(this Ga41KVector4 mv1, Ga41KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector3.Zero;
        
        return new Ga41KVector3
        {
            Scalar123 = -mv1.Scalar1245 * mv2.Scalar345 + mv1.Scalar1345 * mv2.Scalar245 - mv1.Scalar2345 * mv2.Scalar145,
            Scalar124 = mv1.Scalar1235 * mv2.Scalar345 - mv1.Scalar1345 * mv2.Scalar235 + mv1.Scalar2345 * mv2.Scalar135,
            Scalar134 = -mv1.Scalar1235 * mv2.Scalar245 + mv1.Scalar1245 * mv2.Scalar235 - mv1.Scalar2345 * mv2.Scalar125,
            Scalar234 = -mv1.Scalar1235 * mv2.Scalar145 + mv1.Scalar1245 * mv2.Scalar135 - mv1.Scalar1345 * mv2.Scalar125,
            Scalar125 = -mv1.Scalar1234 * mv2.Scalar345 + mv1.Scalar1345 * mv2.Scalar234 - mv1.Scalar2345 * mv2.Scalar134,
            Scalar135 = mv1.Scalar1234 * mv2.Scalar245 - mv1.Scalar1245 * mv2.Scalar234 + mv1.Scalar2345 * mv2.Scalar124,
            Scalar235 = mv1.Scalar1234 * mv2.Scalar145 - mv1.Scalar1245 * mv2.Scalar134 + mv1.Scalar1345 * mv2.Scalar124,
            Scalar145 = -mv1.Scalar1234 * mv2.Scalar235 + mv1.Scalar1235 * mv2.Scalar234 - mv1.Scalar2345 * mv2.Scalar123,
            Scalar245 = -mv1.Scalar1234 * mv2.Scalar135 + mv1.Scalar1235 * mv2.Scalar134 - mv1.Scalar1345 * mv2.Scalar123,
            Scalar345 = mv1.Scalar1234 * mv2.Scalar125 - mv1.Scalar1235 * mv2.Scalar124 + mv1.Scalar1245 * mv2.Scalar123
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Acp(this Ga41KVector4 mv1, Ga41KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        return new Ga41KVector0
        {
            Scalar = -mv1.Scalar1234 * mv2.Scalar1234 - mv1.Scalar1235 * mv2.Scalar1235 - mv1.Scalar1245 * mv2.Scalar1245 - mv1.Scalar1345 * mv2.Scalar1345 + mv1.Scalar2345 * mv2.Scalar2345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector1 Acp(this Ga41KVector4 mv1, Ga41KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector1.Zero;
        
        return new Ga41KVector1
        {
            Scalar1 = mv1.Scalar2345 * mv2.Scalar12345,
            Scalar2 = mv1.Scalar1345 * mv2.Scalar12345,
            Scalar3 = -mv1.Scalar1245 * mv2.Scalar12345,
            Scalar4 = mv1.Scalar1235 * mv2.Scalar12345,
            Scalar5 = -mv1.Scalar1234 * mv2.Scalar12345
        };
    }
    
    public static Ga41Multivector Acp(this Ga41KVector4 mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[15] += mv1.Scalar1234 * mv2.KVector0.Scalar;
            tempScalar[23] += mv1.Scalar1235 * mv2.KVector0.Scalar;
            tempScalar[27] += mv1.Scalar1245 * mv2.KVector0.Scalar;
            tempScalar[29] += mv1.Scalar1345 * mv2.KVector0.Scalar;
            tempScalar[30] += mv1.Scalar2345 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[31] += mv1.Scalar1234 * mv2.KVector1.Scalar5 - mv1.Scalar1235 * mv2.KVector1.Scalar4 + mv1.Scalar1245 * mv2.KVector1.Scalar3 - mv1.Scalar1345 * mv2.KVector1.Scalar2 + mv1.Scalar2345 * mv2.KVector1.Scalar1;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[3] += -mv1.Scalar1234 * mv2.KVector2.Scalar34 - mv1.Scalar1235 * mv2.KVector2.Scalar35 - mv1.Scalar1245 * mv2.KVector2.Scalar45;
            tempScalar[5] += mv1.Scalar1234 * mv2.KVector2.Scalar24 + mv1.Scalar1235 * mv2.KVector2.Scalar25 - mv1.Scalar1345 * mv2.KVector2.Scalar45;
            tempScalar[6] += mv1.Scalar1234 * mv2.KVector2.Scalar14 + mv1.Scalar1235 * mv2.KVector2.Scalar15 - mv1.Scalar2345 * mv2.KVector2.Scalar45;
            tempScalar[9] += -mv1.Scalar1234 * mv2.KVector2.Scalar23 + mv1.Scalar1245 * mv2.KVector2.Scalar25 + mv1.Scalar1345 * mv2.KVector2.Scalar35;
            tempScalar[10] += -mv1.Scalar1234 * mv2.KVector2.Scalar13 + mv1.Scalar1245 * mv2.KVector2.Scalar15 + mv1.Scalar2345 * mv2.KVector2.Scalar35;
            tempScalar[12] += mv1.Scalar1234 * mv2.KVector2.Scalar12 + mv1.Scalar1345 * mv2.KVector2.Scalar15 - mv1.Scalar2345 * mv2.KVector2.Scalar25;
            tempScalar[17] += -mv1.Scalar1235 * mv2.KVector2.Scalar23 - mv1.Scalar1245 * mv2.KVector2.Scalar24 - mv1.Scalar1345 * mv2.KVector2.Scalar34;
            tempScalar[18] += -mv1.Scalar1235 * mv2.KVector2.Scalar13 - mv1.Scalar1245 * mv2.KVector2.Scalar14 - mv1.Scalar2345 * mv2.KVector2.Scalar34;
            tempScalar[20] += mv1.Scalar1235 * mv2.KVector2.Scalar12 - mv1.Scalar1345 * mv2.KVector2.Scalar14 + mv1.Scalar2345 * mv2.KVector2.Scalar24;
            tempScalar[24] += mv1.Scalar1245 * mv2.KVector2.Scalar12 + mv1.Scalar1345 * mv2.KVector2.Scalar13 - mv1.Scalar2345 * mv2.KVector2.Scalar23;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[7] += -mv1.Scalar1245 * mv2.KVector3.Scalar345 + mv1.Scalar1345 * mv2.KVector3.Scalar245 - mv1.Scalar2345 * mv2.KVector3.Scalar145;
            tempScalar[11] += mv1.Scalar1235 * mv2.KVector3.Scalar345 - mv1.Scalar1345 * mv2.KVector3.Scalar235 + mv1.Scalar2345 * mv2.KVector3.Scalar135;
            tempScalar[13] += -mv1.Scalar1235 * mv2.KVector3.Scalar245 + mv1.Scalar1245 * mv2.KVector3.Scalar235 - mv1.Scalar2345 * mv2.KVector3.Scalar125;
            tempScalar[14] += -mv1.Scalar1235 * mv2.KVector3.Scalar145 + mv1.Scalar1245 * mv2.KVector3.Scalar135 - mv1.Scalar1345 * mv2.KVector3.Scalar125;
            tempScalar[19] += -mv1.Scalar1234 * mv2.KVector3.Scalar345 + mv1.Scalar1345 * mv2.KVector3.Scalar234 - mv1.Scalar2345 * mv2.KVector3.Scalar134;
            tempScalar[21] += mv1.Scalar1234 * mv2.KVector3.Scalar245 - mv1.Scalar1245 * mv2.KVector3.Scalar234 + mv1.Scalar2345 * mv2.KVector3.Scalar124;
            tempScalar[22] += mv1.Scalar1234 * mv2.KVector3.Scalar145 - mv1.Scalar1245 * mv2.KVector3.Scalar134 + mv1.Scalar1345 * mv2.KVector3.Scalar124;
            tempScalar[25] += -mv1.Scalar1234 * mv2.KVector3.Scalar235 + mv1.Scalar1235 * mv2.KVector3.Scalar234 - mv1.Scalar2345 * mv2.KVector3.Scalar123;
            tempScalar[26] += -mv1.Scalar1234 * mv2.KVector3.Scalar135 + mv1.Scalar1235 * mv2.KVector3.Scalar134 - mv1.Scalar1345 * mv2.KVector3.Scalar123;
            tempScalar[28] += mv1.Scalar1234 * mv2.KVector3.Scalar125 - mv1.Scalar1235 * mv2.KVector3.Scalar124 + mv1.Scalar1245 * mv2.KVector3.Scalar123;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[0] += -mv1.Scalar1234 * mv2.KVector4.Scalar1234 - mv1.Scalar1235 * mv2.KVector4.Scalar1235 - mv1.Scalar1245 * mv2.KVector4.Scalar1245 - mv1.Scalar1345 * mv2.KVector4.Scalar1345 + mv1.Scalar2345 * mv2.KVector4.Scalar2345;
        }
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[1] += mv1.Scalar2345 * mv2.KVector5.Scalar12345;
            tempScalar[2] += mv1.Scalar1345 * mv2.KVector5.Scalar12345;
            tempScalar[4] += -mv1.Scalar1245 * mv2.KVector5.Scalar12345;
            tempScalar[8] += mv1.Scalar1235 * mv2.KVector5.Scalar12345;
            tempScalar[16] += -mv1.Scalar1234 * mv2.KVector5.Scalar12345;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector5 Acp(this Ga41KVector5 mv1, Ga41KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector5.Zero;
        
        return new Ga41KVector5
        {
            Scalar12345 = mv1.Scalar12345 * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector4 Acp(this Ga41KVector5 mv1, Ga41KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector4.Zero;
        
        return new Ga41KVector4
        {
            Scalar1234 = mv1.Scalar12345 * mv2.Scalar5,
            Scalar1235 = -mv1.Scalar12345 * mv2.Scalar4,
            Scalar1245 = mv1.Scalar12345 * mv2.Scalar3,
            Scalar1345 = -mv1.Scalar12345 * mv2.Scalar2,
            Scalar2345 = -mv1.Scalar12345 * mv2.Scalar1
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector3 Acp(this Ga41KVector5 mv1, Ga41KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector3.Zero;
        
        return new Ga41KVector3
        {
            Scalar123 = -mv1.Scalar12345 * mv2.Scalar45,
            Scalar124 = mv1.Scalar12345 * mv2.Scalar35,
            Scalar134 = -mv1.Scalar12345 * mv2.Scalar25,
            Scalar234 = -mv1.Scalar12345 * mv2.Scalar15,
            Scalar125 = -mv1.Scalar12345 * mv2.Scalar34,
            Scalar135 = mv1.Scalar12345 * mv2.Scalar24,
            Scalar235 = mv1.Scalar12345 * mv2.Scalar14,
            Scalar145 = -mv1.Scalar12345 * mv2.Scalar23,
            Scalar245 = -mv1.Scalar12345 * mv2.Scalar13,
            Scalar345 = mv1.Scalar12345 * mv2.Scalar12
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector2 Acp(this Ga41KVector5 mv1, Ga41KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector2.Zero;
        
        return new Ga41KVector2
        {
            Scalar12 = -mv1.Scalar12345 * mv2.Scalar345,
            Scalar13 = mv1.Scalar12345 * mv2.Scalar245,
            Scalar23 = mv1.Scalar12345 * mv2.Scalar145,
            Scalar14 = -mv1.Scalar12345 * mv2.Scalar235,
            Scalar24 = -mv1.Scalar12345 * mv2.Scalar135,
            Scalar34 = mv1.Scalar12345 * mv2.Scalar125,
            Scalar15 = mv1.Scalar12345 * mv2.Scalar234,
            Scalar25 = mv1.Scalar12345 * mv2.Scalar134,
            Scalar35 = -mv1.Scalar12345 * mv2.Scalar124,
            Scalar45 = mv1.Scalar12345 * mv2.Scalar123
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector1 Acp(this Ga41KVector5 mv1, Ga41KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector1.Zero;
        
        return new Ga41KVector1
        {
            Scalar1 = mv1.Scalar12345 * mv2.Scalar2345,
            Scalar2 = mv1.Scalar12345 * mv2.Scalar1345,
            Scalar3 = -mv1.Scalar12345 * mv2.Scalar1245,
            Scalar4 = mv1.Scalar12345 * mv2.Scalar1235,
            Scalar5 = -mv1.Scalar12345 * mv2.Scalar1234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Acp(this Ga41KVector5 mv1, Ga41KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector0.Zero;
        
        return new Ga41KVector0
        {
            Scalar = -mv1.Scalar12345 * mv2.Scalar12345
        };
    }
    
    public static Ga41Multivector Acp(this Ga41KVector5 mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv2.KVector0.IsZero())
        {
            tempScalar[31] += mv1.Scalar12345 * mv2.KVector0.Scalar;
        }
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[15] += mv1.Scalar12345 * mv2.KVector1.Scalar5;
            tempScalar[23] += -mv1.Scalar12345 * mv2.KVector1.Scalar4;
            tempScalar[27] += mv1.Scalar12345 * mv2.KVector1.Scalar3;
            tempScalar[29] += -mv1.Scalar12345 * mv2.KVector1.Scalar2;
            tempScalar[30] += -mv1.Scalar12345 * mv2.KVector1.Scalar1;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[7] += -mv1.Scalar12345 * mv2.KVector2.Scalar45;
            tempScalar[11] += mv1.Scalar12345 * mv2.KVector2.Scalar35;
            tempScalar[13] += -mv1.Scalar12345 * mv2.KVector2.Scalar25;
            tempScalar[14] += -mv1.Scalar12345 * mv2.KVector2.Scalar15;
            tempScalar[19] += -mv1.Scalar12345 * mv2.KVector2.Scalar34;
            tempScalar[21] += mv1.Scalar12345 * mv2.KVector2.Scalar24;
            tempScalar[22] += mv1.Scalar12345 * mv2.KVector2.Scalar14;
            tempScalar[25] += -mv1.Scalar12345 * mv2.KVector2.Scalar23;
            tempScalar[26] += -mv1.Scalar12345 * mv2.KVector2.Scalar13;
            tempScalar[28] += mv1.Scalar12345 * mv2.KVector2.Scalar12;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[3] += -mv1.Scalar12345 * mv2.KVector3.Scalar345;
            tempScalar[5] += mv1.Scalar12345 * mv2.KVector3.Scalar245;
            tempScalar[6] += mv1.Scalar12345 * mv2.KVector3.Scalar145;
            tempScalar[9] += -mv1.Scalar12345 * mv2.KVector3.Scalar235;
            tempScalar[10] += -mv1.Scalar12345 * mv2.KVector3.Scalar135;
            tempScalar[12] += mv1.Scalar12345 * mv2.KVector3.Scalar125;
            tempScalar[17] += mv1.Scalar12345 * mv2.KVector3.Scalar234;
            tempScalar[18] += mv1.Scalar12345 * mv2.KVector3.Scalar134;
            tempScalar[20] += -mv1.Scalar12345 * mv2.KVector3.Scalar124;
            tempScalar[24] += mv1.Scalar12345 * mv2.KVector3.Scalar123;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[1] += mv1.Scalar12345 * mv2.KVector4.Scalar2345;
            tempScalar[2] += mv1.Scalar12345 * mv2.KVector4.Scalar1345;
            tempScalar[4] += -mv1.Scalar12345 * mv2.KVector4.Scalar1245;
            tempScalar[8] += mv1.Scalar12345 * mv2.KVector4.Scalar1235;
            tempScalar[16] += -mv1.Scalar12345 * mv2.KVector4.Scalar1234;
        }
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[0] += -mv1.Scalar12345 * mv2.KVector5.Scalar12345;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    public static Ga41Multivector Acp(this Ga41Multivector mv1, Ga41KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
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
            tempScalar[16] += mv1.KVector1.Scalar5 * mv2.Scalar;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += mv1.KVector2.Scalar12 * mv2.Scalar;
            tempScalar[5] += mv1.KVector2.Scalar13 * mv2.Scalar;
            tempScalar[6] += mv1.KVector2.Scalar23 * mv2.Scalar;
            tempScalar[9] += mv1.KVector2.Scalar14 * mv2.Scalar;
            tempScalar[10] += mv1.KVector2.Scalar24 * mv2.Scalar;
            tempScalar[12] += mv1.KVector2.Scalar34 * mv2.Scalar;
            tempScalar[17] += mv1.KVector2.Scalar15 * mv2.Scalar;
            tempScalar[18] += mv1.KVector2.Scalar25 * mv2.Scalar;
            tempScalar[20] += mv1.KVector2.Scalar35 * mv2.Scalar;
            tempScalar[24] += mv1.KVector2.Scalar45 * mv2.Scalar;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += mv1.KVector3.Scalar123 * mv2.Scalar;
            tempScalar[11] += mv1.KVector3.Scalar124 * mv2.Scalar;
            tempScalar[13] += mv1.KVector3.Scalar134 * mv2.Scalar;
            tempScalar[14] += mv1.KVector3.Scalar234 * mv2.Scalar;
            tempScalar[19] += mv1.KVector3.Scalar125 * mv2.Scalar;
            tempScalar[21] += mv1.KVector3.Scalar135 * mv2.Scalar;
            tempScalar[22] += mv1.KVector3.Scalar235 * mv2.Scalar;
            tempScalar[25] += mv1.KVector3.Scalar145 * mv2.Scalar;
            tempScalar[26] += mv1.KVector3.Scalar245 * mv2.Scalar;
            tempScalar[28] += mv1.KVector3.Scalar345 * mv2.Scalar;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += mv1.KVector4.Scalar1234 * mv2.Scalar;
            tempScalar[23] += mv1.KVector4.Scalar1235 * mv2.Scalar;
            tempScalar[27] += mv1.KVector4.Scalar1245 * mv2.Scalar;
            tempScalar[29] += mv1.KVector4.Scalar1345 * mv2.Scalar;
            tempScalar[30] += mv1.KVector4.Scalar2345 * mv2.Scalar;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += mv1.KVector5.Scalar12345 * mv2.Scalar;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    public static Ga41Multivector Acp(this Ga41Multivector mv1, Ga41KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[1] += mv1.KVector0.Scalar * mv2.Scalar1;
            tempScalar[2] += mv1.KVector0.Scalar * mv2.Scalar2;
            tempScalar[4] += mv1.KVector0.Scalar * mv2.Scalar3;
            tempScalar[8] += mv1.KVector0.Scalar * mv2.Scalar4;
            tempScalar[16] += mv1.KVector0.Scalar * mv2.Scalar5;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[0] += -mv1.KVector1.Scalar1 * mv2.Scalar1 + mv1.KVector1.Scalar2 * mv2.Scalar2 + mv1.KVector1.Scalar3 * mv2.Scalar3 + mv1.KVector1.Scalar4 * mv2.Scalar4 + mv1.KVector1.Scalar5 * mv2.Scalar5;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[7] += mv1.KVector2.Scalar12 * mv2.Scalar3 - mv1.KVector2.Scalar13 * mv2.Scalar2 + mv1.KVector2.Scalar23 * mv2.Scalar1;
            tempScalar[11] += mv1.KVector2.Scalar12 * mv2.Scalar4 - mv1.KVector2.Scalar14 * mv2.Scalar2 + mv1.KVector2.Scalar24 * mv2.Scalar1;
            tempScalar[13] += mv1.KVector2.Scalar13 * mv2.Scalar4 - mv1.KVector2.Scalar14 * mv2.Scalar3 + mv1.KVector2.Scalar34 * mv2.Scalar1;
            tempScalar[14] += mv1.KVector2.Scalar23 * mv2.Scalar4 - mv1.KVector2.Scalar24 * mv2.Scalar3 + mv1.KVector2.Scalar34 * mv2.Scalar2;
            tempScalar[19] += mv1.KVector2.Scalar12 * mv2.Scalar5 - mv1.KVector2.Scalar15 * mv2.Scalar2 + mv1.KVector2.Scalar25 * mv2.Scalar1;
            tempScalar[21] += mv1.KVector2.Scalar13 * mv2.Scalar5 - mv1.KVector2.Scalar15 * mv2.Scalar3 + mv1.KVector2.Scalar35 * mv2.Scalar1;
            tempScalar[22] += mv1.KVector2.Scalar23 * mv2.Scalar5 - mv1.KVector2.Scalar25 * mv2.Scalar3 + mv1.KVector2.Scalar35 * mv2.Scalar2;
            tempScalar[25] += mv1.KVector2.Scalar14 * mv2.Scalar5 - mv1.KVector2.Scalar15 * mv2.Scalar4 + mv1.KVector2.Scalar45 * mv2.Scalar1;
            tempScalar[26] += mv1.KVector2.Scalar24 * mv2.Scalar5 - mv1.KVector2.Scalar25 * mv2.Scalar4 + mv1.KVector2.Scalar45 * mv2.Scalar2;
            tempScalar[28] += mv1.KVector2.Scalar34 * mv2.Scalar5 - mv1.KVector2.Scalar35 * mv2.Scalar4 + mv1.KVector2.Scalar45 * mv2.Scalar3;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[3] += mv1.KVector3.Scalar123 * mv2.Scalar3 + mv1.KVector3.Scalar124 * mv2.Scalar4 + mv1.KVector3.Scalar125 * mv2.Scalar5;
            tempScalar[5] += -mv1.KVector3.Scalar123 * mv2.Scalar2 + mv1.KVector3.Scalar134 * mv2.Scalar4 + mv1.KVector3.Scalar135 * mv2.Scalar5;
            tempScalar[6] += -mv1.KVector3.Scalar123 * mv2.Scalar1 + mv1.KVector3.Scalar234 * mv2.Scalar4 + mv1.KVector3.Scalar235 * mv2.Scalar5;
            tempScalar[9] += -mv1.KVector3.Scalar124 * mv2.Scalar2 - mv1.KVector3.Scalar134 * mv2.Scalar3 + mv1.KVector3.Scalar145 * mv2.Scalar5;
            tempScalar[10] += -mv1.KVector3.Scalar124 * mv2.Scalar1 - mv1.KVector3.Scalar234 * mv2.Scalar3 + mv1.KVector3.Scalar245 * mv2.Scalar5;
            tempScalar[12] += -mv1.KVector3.Scalar134 * mv2.Scalar1 + mv1.KVector3.Scalar234 * mv2.Scalar2 + mv1.KVector3.Scalar345 * mv2.Scalar5;
            tempScalar[17] += -mv1.KVector3.Scalar125 * mv2.Scalar2 - mv1.KVector3.Scalar135 * mv2.Scalar3 - mv1.KVector3.Scalar145 * mv2.Scalar4;
            tempScalar[18] += -mv1.KVector3.Scalar125 * mv2.Scalar1 - mv1.KVector3.Scalar235 * mv2.Scalar3 - mv1.KVector3.Scalar245 * mv2.Scalar4;
            tempScalar[20] += -mv1.KVector3.Scalar135 * mv2.Scalar1 + mv1.KVector3.Scalar235 * mv2.Scalar2 - mv1.KVector3.Scalar345 * mv2.Scalar4;
            tempScalar[24] += -mv1.KVector3.Scalar145 * mv2.Scalar1 + mv1.KVector3.Scalar245 * mv2.Scalar2 + mv1.KVector3.Scalar345 * mv2.Scalar3;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[31] += mv1.KVector4.Scalar1234 * mv2.Scalar5 - mv1.KVector4.Scalar1235 * mv2.Scalar4 + mv1.KVector4.Scalar1245 * mv2.Scalar3 - mv1.KVector4.Scalar1345 * mv2.Scalar2 + mv1.KVector4.Scalar2345 * mv2.Scalar1;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[15] += mv1.KVector5.Scalar12345 * mv2.Scalar5;
            tempScalar[23] += -mv1.KVector5.Scalar12345 * mv2.Scalar4;
            tempScalar[27] += mv1.KVector5.Scalar12345 * mv2.Scalar3;
            tempScalar[29] += -mv1.KVector5.Scalar12345 * mv2.Scalar2;
            tempScalar[30] += -mv1.KVector5.Scalar12345 * mv2.Scalar1;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    public static Ga41Multivector Acp(this Ga41Multivector mv1, Ga41KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[3] += mv1.KVector0.Scalar * mv2.Scalar12;
            tempScalar[5] += mv1.KVector0.Scalar * mv2.Scalar13;
            tempScalar[6] += mv1.KVector0.Scalar * mv2.Scalar23;
            tempScalar[9] += mv1.KVector0.Scalar * mv2.Scalar14;
            tempScalar[10] += mv1.KVector0.Scalar * mv2.Scalar24;
            tempScalar[12] += mv1.KVector0.Scalar * mv2.Scalar34;
            tempScalar[17] += mv1.KVector0.Scalar * mv2.Scalar15;
            tempScalar[18] += mv1.KVector0.Scalar * mv2.Scalar25;
            tempScalar[20] += mv1.KVector0.Scalar * mv2.Scalar35;
            tempScalar[24] += mv1.KVector0.Scalar * mv2.Scalar45;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[7] += mv1.KVector1.Scalar1 * mv2.Scalar23 - mv1.KVector1.Scalar2 * mv2.Scalar13 + mv1.KVector1.Scalar3 * mv2.Scalar12;
            tempScalar[11] += mv1.KVector1.Scalar1 * mv2.Scalar24 - mv1.KVector1.Scalar2 * mv2.Scalar14 + mv1.KVector1.Scalar4 * mv2.Scalar12;
            tempScalar[13] += mv1.KVector1.Scalar1 * mv2.Scalar34 - mv1.KVector1.Scalar3 * mv2.Scalar14 + mv1.KVector1.Scalar4 * mv2.Scalar13;
            tempScalar[14] += mv1.KVector1.Scalar2 * mv2.Scalar34 - mv1.KVector1.Scalar3 * mv2.Scalar24 + mv1.KVector1.Scalar4 * mv2.Scalar23;
            tempScalar[19] += mv1.KVector1.Scalar1 * mv2.Scalar25 - mv1.KVector1.Scalar2 * mv2.Scalar15 + mv1.KVector1.Scalar5 * mv2.Scalar12;
            tempScalar[21] += mv1.KVector1.Scalar1 * mv2.Scalar35 - mv1.KVector1.Scalar3 * mv2.Scalar15 + mv1.KVector1.Scalar5 * mv2.Scalar13;
            tempScalar[22] += mv1.KVector1.Scalar2 * mv2.Scalar35 - mv1.KVector1.Scalar3 * mv2.Scalar25 + mv1.KVector1.Scalar5 * mv2.Scalar23;
            tempScalar[25] += mv1.KVector1.Scalar1 * mv2.Scalar45 - mv1.KVector1.Scalar4 * mv2.Scalar15 + mv1.KVector1.Scalar5 * mv2.Scalar14;
            tempScalar[26] += mv1.KVector1.Scalar2 * mv2.Scalar45 - mv1.KVector1.Scalar4 * mv2.Scalar25 + mv1.KVector1.Scalar5 * mv2.Scalar24;
            tempScalar[28] += mv1.KVector1.Scalar3 * mv2.Scalar45 - mv1.KVector1.Scalar4 * mv2.Scalar35 + mv1.KVector1.Scalar5 * mv2.Scalar34;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[0] += mv1.KVector2.Scalar12 * mv2.Scalar12 + mv1.KVector2.Scalar13 * mv2.Scalar13 - mv1.KVector2.Scalar23 * mv2.Scalar23 + mv1.KVector2.Scalar14 * mv2.Scalar14 - mv1.KVector2.Scalar24 * mv2.Scalar24 - mv1.KVector2.Scalar34 * mv2.Scalar34 + mv1.KVector2.Scalar15 * mv2.Scalar15 - mv1.KVector2.Scalar25 * mv2.Scalar25 - mv1.KVector2.Scalar35 * mv2.Scalar35 - mv1.KVector2.Scalar45 * mv2.Scalar45;
            tempScalar[15] += mv1.KVector2.Scalar12 * mv2.Scalar34 - mv1.KVector2.Scalar13 * mv2.Scalar24 + mv1.KVector2.Scalar23 * mv2.Scalar14 + mv1.KVector2.Scalar14 * mv2.Scalar23 - mv1.KVector2.Scalar24 * mv2.Scalar13 + mv1.KVector2.Scalar34 * mv2.Scalar12;
            tempScalar[23] += mv1.KVector2.Scalar12 * mv2.Scalar35 - mv1.KVector2.Scalar13 * mv2.Scalar25 + mv1.KVector2.Scalar23 * mv2.Scalar15 + mv1.KVector2.Scalar15 * mv2.Scalar23 - mv1.KVector2.Scalar25 * mv2.Scalar13 + mv1.KVector2.Scalar35 * mv2.Scalar12;
            tempScalar[27] += mv1.KVector2.Scalar12 * mv2.Scalar45 - mv1.KVector2.Scalar14 * mv2.Scalar25 + mv1.KVector2.Scalar24 * mv2.Scalar15 + mv1.KVector2.Scalar15 * mv2.Scalar24 - mv1.KVector2.Scalar25 * mv2.Scalar14 + mv1.KVector2.Scalar45 * mv2.Scalar12;
            tempScalar[29] += mv1.KVector2.Scalar13 * mv2.Scalar45 - mv1.KVector2.Scalar14 * mv2.Scalar35 + mv1.KVector2.Scalar34 * mv2.Scalar15 + mv1.KVector2.Scalar15 * mv2.Scalar34 - mv1.KVector2.Scalar35 * mv2.Scalar14 + mv1.KVector2.Scalar45 * mv2.Scalar13;
            tempScalar[30] += mv1.KVector2.Scalar23 * mv2.Scalar45 - mv1.KVector2.Scalar24 * mv2.Scalar35 + mv1.KVector2.Scalar34 * mv2.Scalar25 + mv1.KVector2.Scalar25 * mv2.Scalar34 - mv1.KVector2.Scalar35 * mv2.Scalar24 + mv1.KVector2.Scalar45 * mv2.Scalar23;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[1] += -mv1.KVector3.Scalar123 * mv2.Scalar23 - mv1.KVector3.Scalar124 * mv2.Scalar24 - mv1.KVector3.Scalar134 * mv2.Scalar34 - mv1.KVector3.Scalar125 * mv2.Scalar25 - mv1.KVector3.Scalar135 * mv2.Scalar35 - mv1.KVector3.Scalar145 * mv2.Scalar45;
            tempScalar[2] += -mv1.KVector3.Scalar123 * mv2.Scalar13 - mv1.KVector3.Scalar124 * mv2.Scalar14 - mv1.KVector3.Scalar234 * mv2.Scalar34 - mv1.KVector3.Scalar125 * mv2.Scalar15 - mv1.KVector3.Scalar235 * mv2.Scalar35 - mv1.KVector3.Scalar245 * mv2.Scalar45;
            tempScalar[4] += mv1.KVector3.Scalar123 * mv2.Scalar12 - mv1.KVector3.Scalar134 * mv2.Scalar14 + mv1.KVector3.Scalar234 * mv2.Scalar24 - mv1.KVector3.Scalar135 * mv2.Scalar15 + mv1.KVector3.Scalar235 * mv2.Scalar25 - mv1.KVector3.Scalar345 * mv2.Scalar45;
            tempScalar[8] += mv1.KVector3.Scalar124 * mv2.Scalar12 + mv1.KVector3.Scalar134 * mv2.Scalar13 - mv1.KVector3.Scalar234 * mv2.Scalar23 - mv1.KVector3.Scalar145 * mv2.Scalar15 + mv1.KVector3.Scalar245 * mv2.Scalar25 + mv1.KVector3.Scalar345 * mv2.Scalar35;
            tempScalar[16] += mv1.KVector3.Scalar125 * mv2.Scalar12 + mv1.KVector3.Scalar135 * mv2.Scalar13 - mv1.KVector3.Scalar235 * mv2.Scalar23 + mv1.KVector3.Scalar145 * mv2.Scalar14 - mv1.KVector3.Scalar245 * mv2.Scalar24 - mv1.KVector3.Scalar345 * mv2.Scalar34;
            tempScalar[31] += mv1.KVector3.Scalar123 * mv2.Scalar45 - mv1.KVector3.Scalar124 * mv2.Scalar35 + mv1.KVector3.Scalar134 * mv2.Scalar25 - mv1.KVector3.Scalar234 * mv2.Scalar15 + mv1.KVector3.Scalar125 * mv2.Scalar34 - mv1.KVector3.Scalar135 * mv2.Scalar24 + mv1.KVector3.Scalar235 * mv2.Scalar14 + mv1.KVector3.Scalar145 * mv2.Scalar23 - mv1.KVector3.Scalar245 * mv2.Scalar13 + mv1.KVector3.Scalar345 * mv2.Scalar12;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[3] += -mv1.KVector4.Scalar1234 * mv2.Scalar34 - mv1.KVector4.Scalar1235 * mv2.Scalar35 - mv1.KVector4.Scalar1245 * mv2.Scalar45;
            tempScalar[5] += mv1.KVector4.Scalar1234 * mv2.Scalar24 + mv1.KVector4.Scalar1235 * mv2.Scalar25 - mv1.KVector4.Scalar1345 * mv2.Scalar45;
            tempScalar[6] += mv1.KVector4.Scalar1234 * mv2.Scalar14 + mv1.KVector4.Scalar1235 * mv2.Scalar15 - mv1.KVector4.Scalar2345 * mv2.Scalar45;
            tempScalar[9] += -mv1.KVector4.Scalar1234 * mv2.Scalar23 + mv1.KVector4.Scalar1245 * mv2.Scalar25 + mv1.KVector4.Scalar1345 * mv2.Scalar35;
            tempScalar[10] += -mv1.KVector4.Scalar1234 * mv2.Scalar13 + mv1.KVector4.Scalar1245 * mv2.Scalar15 + mv1.KVector4.Scalar2345 * mv2.Scalar35;
            tempScalar[12] += mv1.KVector4.Scalar1234 * mv2.Scalar12 + mv1.KVector4.Scalar1345 * mv2.Scalar15 - mv1.KVector4.Scalar2345 * mv2.Scalar25;
            tempScalar[17] += -mv1.KVector4.Scalar1235 * mv2.Scalar23 - mv1.KVector4.Scalar1245 * mv2.Scalar24 - mv1.KVector4.Scalar1345 * mv2.Scalar34;
            tempScalar[18] += -mv1.KVector4.Scalar1235 * mv2.Scalar13 - mv1.KVector4.Scalar1245 * mv2.Scalar14 - mv1.KVector4.Scalar2345 * mv2.Scalar34;
            tempScalar[20] += mv1.KVector4.Scalar1235 * mv2.Scalar12 - mv1.KVector4.Scalar1345 * mv2.Scalar14 + mv1.KVector4.Scalar2345 * mv2.Scalar24;
            tempScalar[24] += mv1.KVector4.Scalar1245 * mv2.Scalar12 + mv1.KVector4.Scalar1345 * mv2.Scalar13 - mv1.KVector4.Scalar2345 * mv2.Scalar23;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[7] += -mv1.KVector5.Scalar12345 * mv2.Scalar45;
            tempScalar[11] += mv1.KVector5.Scalar12345 * mv2.Scalar35;
            tempScalar[13] += -mv1.KVector5.Scalar12345 * mv2.Scalar25;
            tempScalar[14] += -mv1.KVector5.Scalar12345 * mv2.Scalar15;
            tempScalar[19] += -mv1.KVector5.Scalar12345 * mv2.Scalar34;
            tempScalar[21] += mv1.KVector5.Scalar12345 * mv2.Scalar24;
            tempScalar[22] += mv1.KVector5.Scalar12345 * mv2.Scalar14;
            tempScalar[25] += -mv1.KVector5.Scalar12345 * mv2.Scalar23;
            tempScalar[26] += -mv1.KVector5.Scalar12345 * mv2.Scalar13;
            tempScalar[28] += mv1.KVector5.Scalar12345 * mv2.Scalar12;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    public static Ga41Multivector Acp(this Ga41Multivector mv1, Ga41KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[7] += mv1.KVector0.Scalar * mv2.Scalar123;
            tempScalar[11] += mv1.KVector0.Scalar * mv2.Scalar124;
            tempScalar[13] += mv1.KVector0.Scalar * mv2.Scalar134;
            tempScalar[14] += mv1.KVector0.Scalar * mv2.Scalar234;
            tempScalar[19] += mv1.KVector0.Scalar * mv2.Scalar125;
            tempScalar[21] += mv1.KVector0.Scalar * mv2.Scalar135;
            tempScalar[22] += mv1.KVector0.Scalar * mv2.Scalar235;
            tempScalar[25] += mv1.KVector0.Scalar * mv2.Scalar145;
            tempScalar[26] += mv1.KVector0.Scalar * mv2.Scalar245;
            tempScalar[28] += mv1.KVector0.Scalar * mv2.Scalar345;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[3] += mv1.KVector1.Scalar3 * mv2.Scalar123 + mv1.KVector1.Scalar4 * mv2.Scalar124 + mv1.KVector1.Scalar5 * mv2.Scalar125;
            tempScalar[5] += -mv1.KVector1.Scalar2 * mv2.Scalar123 + mv1.KVector1.Scalar4 * mv2.Scalar134 + mv1.KVector1.Scalar5 * mv2.Scalar135;
            tempScalar[6] += -mv1.KVector1.Scalar1 * mv2.Scalar123 + mv1.KVector1.Scalar4 * mv2.Scalar234 + mv1.KVector1.Scalar5 * mv2.Scalar235;
            tempScalar[9] += -mv1.KVector1.Scalar2 * mv2.Scalar124 - mv1.KVector1.Scalar3 * mv2.Scalar134 + mv1.KVector1.Scalar5 * mv2.Scalar145;
            tempScalar[10] += -mv1.KVector1.Scalar1 * mv2.Scalar124 - mv1.KVector1.Scalar3 * mv2.Scalar234 + mv1.KVector1.Scalar5 * mv2.Scalar245;
            tempScalar[12] += -mv1.KVector1.Scalar1 * mv2.Scalar134 + mv1.KVector1.Scalar2 * mv2.Scalar234 + mv1.KVector1.Scalar5 * mv2.Scalar345;
            tempScalar[17] += -mv1.KVector1.Scalar2 * mv2.Scalar125 - mv1.KVector1.Scalar3 * mv2.Scalar135 - mv1.KVector1.Scalar4 * mv2.Scalar145;
            tempScalar[18] += -mv1.KVector1.Scalar1 * mv2.Scalar125 - mv1.KVector1.Scalar3 * mv2.Scalar235 - mv1.KVector1.Scalar4 * mv2.Scalar245;
            tempScalar[20] += -mv1.KVector1.Scalar1 * mv2.Scalar135 + mv1.KVector1.Scalar2 * mv2.Scalar235 - mv1.KVector1.Scalar4 * mv2.Scalar345;
            tempScalar[24] += -mv1.KVector1.Scalar1 * mv2.Scalar145 + mv1.KVector1.Scalar2 * mv2.Scalar245 + mv1.KVector1.Scalar3 * mv2.Scalar345;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[1] += -mv1.KVector2.Scalar23 * mv2.Scalar123 - mv1.KVector2.Scalar24 * mv2.Scalar124 - mv1.KVector2.Scalar34 * mv2.Scalar134 - mv1.KVector2.Scalar25 * mv2.Scalar125 - mv1.KVector2.Scalar35 * mv2.Scalar135 - mv1.KVector2.Scalar45 * mv2.Scalar145;
            tempScalar[2] += -mv1.KVector2.Scalar13 * mv2.Scalar123 - mv1.KVector2.Scalar14 * mv2.Scalar124 - mv1.KVector2.Scalar34 * mv2.Scalar234 - mv1.KVector2.Scalar15 * mv2.Scalar125 - mv1.KVector2.Scalar35 * mv2.Scalar235 - mv1.KVector2.Scalar45 * mv2.Scalar245;
            tempScalar[4] += mv1.KVector2.Scalar12 * mv2.Scalar123 - mv1.KVector2.Scalar14 * mv2.Scalar134 + mv1.KVector2.Scalar24 * mv2.Scalar234 - mv1.KVector2.Scalar15 * mv2.Scalar135 + mv1.KVector2.Scalar25 * mv2.Scalar235 - mv1.KVector2.Scalar45 * mv2.Scalar345;
            tempScalar[8] += mv1.KVector2.Scalar12 * mv2.Scalar124 + mv1.KVector2.Scalar13 * mv2.Scalar134 - mv1.KVector2.Scalar23 * mv2.Scalar234 - mv1.KVector2.Scalar15 * mv2.Scalar145 + mv1.KVector2.Scalar25 * mv2.Scalar245 + mv1.KVector2.Scalar35 * mv2.Scalar345;
            tempScalar[16] += mv1.KVector2.Scalar12 * mv2.Scalar125 + mv1.KVector2.Scalar13 * mv2.Scalar135 - mv1.KVector2.Scalar23 * mv2.Scalar235 + mv1.KVector2.Scalar14 * mv2.Scalar145 - mv1.KVector2.Scalar24 * mv2.Scalar245 - mv1.KVector2.Scalar34 * mv2.Scalar345;
            tempScalar[31] += mv1.KVector2.Scalar12 * mv2.Scalar345 - mv1.KVector2.Scalar13 * mv2.Scalar245 + mv1.KVector2.Scalar23 * mv2.Scalar145 + mv1.KVector2.Scalar14 * mv2.Scalar235 - mv1.KVector2.Scalar24 * mv2.Scalar135 + mv1.KVector2.Scalar34 * mv2.Scalar125 - mv1.KVector2.Scalar15 * mv2.Scalar234 + mv1.KVector2.Scalar25 * mv2.Scalar134 - mv1.KVector2.Scalar35 * mv2.Scalar124 + mv1.KVector2.Scalar45 * mv2.Scalar123;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[0] += mv1.KVector3.Scalar123 * mv2.Scalar123 + mv1.KVector3.Scalar124 * mv2.Scalar124 + mv1.KVector3.Scalar134 * mv2.Scalar134 - mv1.KVector3.Scalar234 * mv2.Scalar234 + mv1.KVector3.Scalar125 * mv2.Scalar125 + mv1.KVector3.Scalar135 * mv2.Scalar135 - mv1.KVector3.Scalar235 * mv2.Scalar235 + mv1.KVector3.Scalar145 * mv2.Scalar145 - mv1.KVector3.Scalar245 * mv2.Scalar245 - mv1.KVector3.Scalar345 * mv2.Scalar345;
            tempScalar[15] += mv1.KVector3.Scalar125 * mv2.Scalar345 - mv1.KVector3.Scalar135 * mv2.Scalar245 + mv1.KVector3.Scalar235 * mv2.Scalar145 + mv1.KVector3.Scalar145 * mv2.Scalar235 - mv1.KVector3.Scalar245 * mv2.Scalar135 + mv1.KVector3.Scalar345 * mv2.Scalar125;
            tempScalar[23] += -mv1.KVector3.Scalar124 * mv2.Scalar345 + mv1.KVector3.Scalar134 * mv2.Scalar245 - mv1.KVector3.Scalar234 * mv2.Scalar145 - mv1.KVector3.Scalar145 * mv2.Scalar234 + mv1.KVector3.Scalar245 * mv2.Scalar134 - mv1.KVector3.Scalar345 * mv2.Scalar124;
            tempScalar[27] += mv1.KVector3.Scalar123 * mv2.Scalar345 - mv1.KVector3.Scalar134 * mv2.Scalar235 + mv1.KVector3.Scalar234 * mv2.Scalar135 + mv1.KVector3.Scalar135 * mv2.Scalar234 - mv1.KVector3.Scalar235 * mv2.Scalar134 + mv1.KVector3.Scalar345 * mv2.Scalar123;
            tempScalar[29] += -mv1.KVector3.Scalar123 * mv2.Scalar245 + mv1.KVector3.Scalar124 * mv2.Scalar235 - mv1.KVector3.Scalar234 * mv2.Scalar125 - mv1.KVector3.Scalar125 * mv2.Scalar234 + mv1.KVector3.Scalar235 * mv2.Scalar124 - mv1.KVector3.Scalar245 * mv2.Scalar123;
            tempScalar[30] += -mv1.KVector3.Scalar123 * mv2.Scalar145 + mv1.KVector3.Scalar124 * mv2.Scalar135 - mv1.KVector3.Scalar134 * mv2.Scalar125 - mv1.KVector3.Scalar125 * mv2.Scalar134 + mv1.KVector3.Scalar135 * mv2.Scalar124 - mv1.KVector3.Scalar145 * mv2.Scalar123;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[7] += -mv1.KVector4.Scalar1245 * mv2.Scalar345 + mv1.KVector4.Scalar1345 * mv2.Scalar245 - mv1.KVector4.Scalar2345 * mv2.Scalar145;
            tempScalar[11] += mv1.KVector4.Scalar1235 * mv2.Scalar345 - mv1.KVector4.Scalar1345 * mv2.Scalar235 + mv1.KVector4.Scalar2345 * mv2.Scalar135;
            tempScalar[13] += -mv1.KVector4.Scalar1235 * mv2.Scalar245 + mv1.KVector4.Scalar1245 * mv2.Scalar235 - mv1.KVector4.Scalar2345 * mv2.Scalar125;
            tempScalar[14] += -mv1.KVector4.Scalar1235 * mv2.Scalar145 + mv1.KVector4.Scalar1245 * mv2.Scalar135 - mv1.KVector4.Scalar1345 * mv2.Scalar125;
            tempScalar[19] += -mv1.KVector4.Scalar1234 * mv2.Scalar345 + mv1.KVector4.Scalar1345 * mv2.Scalar234 - mv1.KVector4.Scalar2345 * mv2.Scalar134;
            tempScalar[21] += mv1.KVector4.Scalar1234 * mv2.Scalar245 - mv1.KVector4.Scalar1245 * mv2.Scalar234 + mv1.KVector4.Scalar2345 * mv2.Scalar124;
            tempScalar[22] += mv1.KVector4.Scalar1234 * mv2.Scalar145 - mv1.KVector4.Scalar1245 * mv2.Scalar134 + mv1.KVector4.Scalar1345 * mv2.Scalar124;
            tempScalar[25] += -mv1.KVector4.Scalar1234 * mv2.Scalar235 + mv1.KVector4.Scalar1235 * mv2.Scalar234 - mv1.KVector4.Scalar2345 * mv2.Scalar123;
            tempScalar[26] += -mv1.KVector4.Scalar1234 * mv2.Scalar135 + mv1.KVector4.Scalar1235 * mv2.Scalar134 - mv1.KVector4.Scalar1345 * mv2.Scalar123;
            tempScalar[28] += mv1.KVector4.Scalar1234 * mv2.Scalar125 - mv1.KVector4.Scalar1235 * mv2.Scalar124 + mv1.KVector4.Scalar1245 * mv2.Scalar123;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[3] += -mv1.KVector5.Scalar12345 * mv2.Scalar345;
            tempScalar[5] += mv1.KVector5.Scalar12345 * mv2.Scalar245;
            tempScalar[6] += mv1.KVector5.Scalar12345 * mv2.Scalar145;
            tempScalar[9] += -mv1.KVector5.Scalar12345 * mv2.Scalar235;
            tempScalar[10] += -mv1.KVector5.Scalar12345 * mv2.Scalar135;
            tempScalar[12] += mv1.KVector5.Scalar12345 * mv2.Scalar125;
            tempScalar[17] += mv1.KVector5.Scalar12345 * mv2.Scalar234;
            tempScalar[18] += mv1.KVector5.Scalar12345 * mv2.Scalar134;
            tempScalar[20] += -mv1.KVector5.Scalar12345 * mv2.Scalar124;
            tempScalar[24] += mv1.KVector5.Scalar12345 * mv2.Scalar123;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    public static Ga41Multivector Acp(this Ga41Multivector mv1, Ga41KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[15] += mv1.KVector0.Scalar * mv2.Scalar1234;
            tempScalar[23] += mv1.KVector0.Scalar * mv2.Scalar1235;
            tempScalar[27] += mv1.KVector0.Scalar * mv2.Scalar1245;
            tempScalar[29] += mv1.KVector0.Scalar * mv2.Scalar1345;
            tempScalar[30] += mv1.KVector0.Scalar * mv2.Scalar2345;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[31] += mv1.KVector1.Scalar1 * mv2.Scalar2345 - mv1.KVector1.Scalar2 * mv2.Scalar1345 + mv1.KVector1.Scalar3 * mv2.Scalar1245 - mv1.KVector1.Scalar4 * mv2.Scalar1235 + mv1.KVector1.Scalar5 * mv2.Scalar1234;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += -mv1.KVector2.Scalar34 * mv2.Scalar1234 - mv1.KVector2.Scalar35 * mv2.Scalar1235 - mv1.KVector2.Scalar45 * mv2.Scalar1245;
            tempScalar[5] += mv1.KVector2.Scalar24 * mv2.Scalar1234 + mv1.KVector2.Scalar25 * mv2.Scalar1235 - mv1.KVector2.Scalar45 * mv2.Scalar1345;
            tempScalar[6] += mv1.KVector2.Scalar14 * mv2.Scalar1234 + mv1.KVector2.Scalar15 * mv2.Scalar1235 - mv1.KVector2.Scalar45 * mv2.Scalar2345;
            tempScalar[9] += -mv1.KVector2.Scalar23 * mv2.Scalar1234 + mv1.KVector2.Scalar25 * mv2.Scalar1245 + mv1.KVector2.Scalar35 * mv2.Scalar1345;
            tempScalar[10] += -mv1.KVector2.Scalar13 * mv2.Scalar1234 + mv1.KVector2.Scalar15 * mv2.Scalar1245 + mv1.KVector2.Scalar35 * mv2.Scalar2345;
            tempScalar[12] += mv1.KVector2.Scalar12 * mv2.Scalar1234 + mv1.KVector2.Scalar15 * mv2.Scalar1345 - mv1.KVector2.Scalar25 * mv2.Scalar2345;
            tempScalar[17] += -mv1.KVector2.Scalar23 * mv2.Scalar1235 - mv1.KVector2.Scalar24 * mv2.Scalar1245 - mv1.KVector2.Scalar34 * mv2.Scalar1345;
            tempScalar[18] += -mv1.KVector2.Scalar13 * mv2.Scalar1235 - mv1.KVector2.Scalar14 * mv2.Scalar1245 - mv1.KVector2.Scalar34 * mv2.Scalar2345;
            tempScalar[20] += mv1.KVector2.Scalar12 * mv2.Scalar1235 - mv1.KVector2.Scalar14 * mv2.Scalar1345 + mv1.KVector2.Scalar24 * mv2.Scalar2345;
            tempScalar[24] += mv1.KVector2.Scalar12 * mv2.Scalar1245 + mv1.KVector2.Scalar13 * mv2.Scalar1345 - mv1.KVector2.Scalar23 * mv2.Scalar2345;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += -mv1.KVector3.Scalar145 * mv2.Scalar2345 + mv1.KVector3.Scalar245 * mv2.Scalar1345 - mv1.KVector3.Scalar345 * mv2.Scalar1245;
            tempScalar[11] += mv1.KVector3.Scalar135 * mv2.Scalar2345 - mv1.KVector3.Scalar235 * mv2.Scalar1345 + mv1.KVector3.Scalar345 * mv2.Scalar1235;
            tempScalar[13] += -mv1.KVector3.Scalar125 * mv2.Scalar2345 + mv1.KVector3.Scalar235 * mv2.Scalar1245 - mv1.KVector3.Scalar245 * mv2.Scalar1235;
            tempScalar[14] += -mv1.KVector3.Scalar125 * mv2.Scalar1345 + mv1.KVector3.Scalar135 * mv2.Scalar1245 - mv1.KVector3.Scalar145 * mv2.Scalar1235;
            tempScalar[19] += -mv1.KVector3.Scalar134 * mv2.Scalar2345 + mv1.KVector3.Scalar234 * mv2.Scalar1345 - mv1.KVector3.Scalar345 * mv2.Scalar1234;
            tempScalar[21] += mv1.KVector3.Scalar124 * mv2.Scalar2345 - mv1.KVector3.Scalar234 * mv2.Scalar1245 + mv1.KVector3.Scalar245 * mv2.Scalar1234;
            tempScalar[22] += mv1.KVector3.Scalar124 * mv2.Scalar1345 - mv1.KVector3.Scalar134 * mv2.Scalar1245 + mv1.KVector3.Scalar145 * mv2.Scalar1234;
            tempScalar[25] += -mv1.KVector3.Scalar123 * mv2.Scalar2345 + mv1.KVector3.Scalar234 * mv2.Scalar1235 - mv1.KVector3.Scalar235 * mv2.Scalar1234;
            tempScalar[26] += -mv1.KVector3.Scalar123 * mv2.Scalar1345 + mv1.KVector3.Scalar134 * mv2.Scalar1235 - mv1.KVector3.Scalar135 * mv2.Scalar1234;
            tempScalar[28] += mv1.KVector3.Scalar123 * mv2.Scalar1245 - mv1.KVector3.Scalar124 * mv2.Scalar1235 + mv1.KVector3.Scalar125 * mv2.Scalar1234;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[0] += -mv1.KVector4.Scalar1234 * mv2.Scalar1234 - mv1.KVector4.Scalar1235 * mv2.Scalar1235 - mv1.KVector4.Scalar1245 * mv2.Scalar1245 - mv1.KVector4.Scalar1345 * mv2.Scalar1345 + mv1.KVector4.Scalar2345 * mv2.Scalar2345;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[1] += mv1.KVector5.Scalar12345 * mv2.Scalar2345;
            tempScalar[2] += mv1.KVector5.Scalar12345 * mv2.Scalar1345;
            tempScalar[4] += -mv1.KVector5.Scalar12345 * mv2.Scalar1245;
            tempScalar[8] += mv1.KVector5.Scalar12345 * mv2.Scalar1235;
            tempScalar[16] += -mv1.KVector5.Scalar12345 * mv2.Scalar1234;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    public static Ga41Multivector Acp(this Ga41Multivector mv1, Ga41KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[31] += mv1.KVector0.Scalar * mv2.Scalar12345;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[15] += mv1.KVector1.Scalar5 * mv2.Scalar12345;
            tempScalar[23] += -mv1.KVector1.Scalar4 * mv2.Scalar12345;
            tempScalar[27] += mv1.KVector1.Scalar3 * mv2.Scalar12345;
            tempScalar[29] += -mv1.KVector1.Scalar2 * mv2.Scalar12345;
            tempScalar[30] += -mv1.KVector1.Scalar1 * mv2.Scalar12345;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[7] += -mv1.KVector2.Scalar45 * mv2.Scalar12345;
            tempScalar[11] += mv1.KVector2.Scalar35 * mv2.Scalar12345;
            tempScalar[13] += -mv1.KVector2.Scalar25 * mv2.Scalar12345;
            tempScalar[14] += -mv1.KVector2.Scalar15 * mv2.Scalar12345;
            tempScalar[19] += -mv1.KVector2.Scalar34 * mv2.Scalar12345;
            tempScalar[21] += mv1.KVector2.Scalar24 * mv2.Scalar12345;
            tempScalar[22] += mv1.KVector2.Scalar14 * mv2.Scalar12345;
            tempScalar[25] += -mv1.KVector2.Scalar23 * mv2.Scalar12345;
            tempScalar[26] += -mv1.KVector2.Scalar13 * mv2.Scalar12345;
            tempScalar[28] += mv1.KVector2.Scalar12 * mv2.Scalar12345;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[3] += -mv1.KVector3.Scalar345 * mv2.Scalar12345;
            tempScalar[5] += mv1.KVector3.Scalar245 * mv2.Scalar12345;
            tempScalar[6] += mv1.KVector3.Scalar145 * mv2.Scalar12345;
            tempScalar[9] += -mv1.KVector3.Scalar235 * mv2.Scalar12345;
            tempScalar[10] += -mv1.KVector3.Scalar135 * mv2.Scalar12345;
            tempScalar[12] += mv1.KVector3.Scalar125 * mv2.Scalar12345;
            tempScalar[17] += mv1.KVector3.Scalar234 * mv2.Scalar12345;
            tempScalar[18] += mv1.KVector3.Scalar134 * mv2.Scalar12345;
            tempScalar[20] += -mv1.KVector3.Scalar124 * mv2.Scalar12345;
            tempScalar[24] += mv1.KVector3.Scalar123 * mv2.Scalar12345;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[1] += mv1.KVector4.Scalar2345 * mv2.Scalar12345;
            tempScalar[2] += mv1.KVector4.Scalar1345 * mv2.Scalar12345;
            tempScalar[4] += -mv1.KVector4.Scalar1245 * mv2.Scalar12345;
            tempScalar[8] += mv1.KVector4.Scalar1235 * mv2.Scalar12345;
            tempScalar[16] += -mv1.KVector4.Scalar1234 * mv2.Scalar12345;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[0] += -mv1.KVector5.Scalar12345 * mv2.Scalar12345;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    public static Ga41Multivector Acp(this Ga41Multivector mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
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
                tempScalar[8] += mv1.KVector0.Scalar * mv2.KVector1.Scalar4;
                tempScalar[16] += mv1.KVector0.Scalar * mv2.KVector1.Scalar5;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[3] += mv1.KVector0.Scalar * mv2.KVector2.Scalar12;
                tempScalar[5] += mv1.KVector0.Scalar * mv2.KVector2.Scalar13;
                tempScalar[6] += mv1.KVector0.Scalar * mv2.KVector2.Scalar23;
                tempScalar[9] += mv1.KVector0.Scalar * mv2.KVector2.Scalar14;
                tempScalar[10] += mv1.KVector0.Scalar * mv2.KVector2.Scalar24;
                tempScalar[12] += mv1.KVector0.Scalar * mv2.KVector2.Scalar34;
                tempScalar[17] += mv1.KVector0.Scalar * mv2.KVector2.Scalar15;
                tempScalar[18] += mv1.KVector0.Scalar * mv2.KVector2.Scalar25;
                tempScalar[20] += mv1.KVector0.Scalar * mv2.KVector2.Scalar35;
                tempScalar[24] += mv1.KVector0.Scalar * mv2.KVector2.Scalar45;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[7] += mv1.KVector0.Scalar * mv2.KVector3.Scalar123;
                tempScalar[11] += mv1.KVector0.Scalar * mv2.KVector3.Scalar124;
                tempScalar[13] += mv1.KVector0.Scalar * mv2.KVector3.Scalar134;
                tempScalar[14] += mv1.KVector0.Scalar * mv2.KVector3.Scalar234;
                tempScalar[19] += mv1.KVector0.Scalar * mv2.KVector3.Scalar125;
                tempScalar[21] += mv1.KVector0.Scalar * mv2.KVector3.Scalar135;
                tempScalar[22] += mv1.KVector0.Scalar * mv2.KVector3.Scalar235;
                tempScalar[25] += mv1.KVector0.Scalar * mv2.KVector3.Scalar145;
                tempScalar[26] += mv1.KVector0.Scalar * mv2.KVector3.Scalar245;
                tempScalar[28] += mv1.KVector0.Scalar * mv2.KVector3.Scalar345;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[15] += mv1.KVector0.Scalar * mv2.KVector4.Scalar1234;
                tempScalar[23] += mv1.KVector0.Scalar * mv2.KVector4.Scalar1235;
                tempScalar[27] += mv1.KVector0.Scalar * mv2.KVector4.Scalar1245;
                tempScalar[29] += mv1.KVector0.Scalar * mv2.KVector4.Scalar1345;
                tempScalar[30] += mv1.KVector0.Scalar * mv2.KVector4.Scalar2345;
            }
            
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[31] += mv1.KVector0.Scalar * mv2.KVector5.Scalar12345;
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
                tempScalar[16] += mv1.KVector1.Scalar5 * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[0] += -mv1.KVector1.Scalar1 * mv2.KVector1.Scalar1 + mv1.KVector1.Scalar2 * mv2.KVector1.Scalar2 + mv1.KVector1.Scalar3 * mv2.KVector1.Scalar3 + mv1.KVector1.Scalar4 * mv2.KVector1.Scalar4 + mv1.KVector1.Scalar5 * mv2.KVector1.Scalar5;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[7] += mv1.KVector1.Scalar1 * mv2.KVector2.Scalar23 - mv1.KVector1.Scalar2 * mv2.KVector2.Scalar13 + mv1.KVector1.Scalar3 * mv2.KVector2.Scalar12;
                tempScalar[11] += mv1.KVector1.Scalar1 * mv2.KVector2.Scalar24 - mv1.KVector1.Scalar2 * mv2.KVector2.Scalar14 + mv1.KVector1.Scalar4 * mv2.KVector2.Scalar12;
                tempScalar[13] += mv1.KVector1.Scalar1 * mv2.KVector2.Scalar34 - mv1.KVector1.Scalar3 * mv2.KVector2.Scalar14 + mv1.KVector1.Scalar4 * mv2.KVector2.Scalar13;
                tempScalar[14] += mv1.KVector1.Scalar2 * mv2.KVector2.Scalar34 - mv1.KVector1.Scalar3 * mv2.KVector2.Scalar24 + mv1.KVector1.Scalar4 * mv2.KVector2.Scalar23;
                tempScalar[19] += mv1.KVector1.Scalar1 * mv2.KVector2.Scalar25 - mv1.KVector1.Scalar2 * mv2.KVector2.Scalar15 + mv1.KVector1.Scalar5 * mv2.KVector2.Scalar12;
                tempScalar[21] += mv1.KVector1.Scalar1 * mv2.KVector2.Scalar35 - mv1.KVector1.Scalar3 * mv2.KVector2.Scalar15 + mv1.KVector1.Scalar5 * mv2.KVector2.Scalar13;
                tempScalar[22] += mv1.KVector1.Scalar2 * mv2.KVector2.Scalar35 - mv1.KVector1.Scalar3 * mv2.KVector2.Scalar25 + mv1.KVector1.Scalar5 * mv2.KVector2.Scalar23;
                tempScalar[25] += mv1.KVector1.Scalar1 * mv2.KVector2.Scalar45 - mv1.KVector1.Scalar4 * mv2.KVector2.Scalar15 + mv1.KVector1.Scalar5 * mv2.KVector2.Scalar14;
                tempScalar[26] += mv1.KVector1.Scalar2 * mv2.KVector2.Scalar45 - mv1.KVector1.Scalar4 * mv2.KVector2.Scalar25 + mv1.KVector1.Scalar5 * mv2.KVector2.Scalar24;
                tempScalar[28] += mv1.KVector1.Scalar3 * mv2.KVector2.Scalar45 - mv1.KVector1.Scalar4 * mv2.KVector2.Scalar35 + mv1.KVector1.Scalar5 * mv2.KVector2.Scalar34;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[3] += mv1.KVector1.Scalar3 * mv2.KVector3.Scalar123 + mv1.KVector1.Scalar4 * mv2.KVector3.Scalar124 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar125;
                tempScalar[5] += -mv1.KVector1.Scalar2 * mv2.KVector3.Scalar123 + mv1.KVector1.Scalar4 * mv2.KVector3.Scalar134 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar135;
                tempScalar[6] += -mv1.KVector1.Scalar1 * mv2.KVector3.Scalar123 + mv1.KVector1.Scalar4 * mv2.KVector3.Scalar234 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar235;
                tempScalar[9] += -mv1.KVector1.Scalar2 * mv2.KVector3.Scalar124 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar134 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar145;
                tempScalar[10] += -mv1.KVector1.Scalar1 * mv2.KVector3.Scalar124 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar234 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar245;
                tempScalar[12] += -mv1.KVector1.Scalar1 * mv2.KVector3.Scalar134 + mv1.KVector1.Scalar2 * mv2.KVector3.Scalar234 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar345;
                tempScalar[17] += -mv1.KVector1.Scalar2 * mv2.KVector3.Scalar125 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar135 - mv1.KVector1.Scalar4 * mv2.KVector3.Scalar145;
                tempScalar[18] += -mv1.KVector1.Scalar1 * mv2.KVector3.Scalar125 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar235 - mv1.KVector1.Scalar4 * mv2.KVector3.Scalar245;
                tempScalar[20] += -mv1.KVector1.Scalar1 * mv2.KVector3.Scalar135 + mv1.KVector1.Scalar2 * mv2.KVector3.Scalar235 - mv1.KVector1.Scalar4 * mv2.KVector3.Scalar345;
                tempScalar[24] += -mv1.KVector1.Scalar1 * mv2.KVector3.Scalar145 + mv1.KVector1.Scalar2 * mv2.KVector3.Scalar245 + mv1.KVector1.Scalar3 * mv2.KVector3.Scalar345;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[31] += mv1.KVector1.Scalar1 * mv2.KVector4.Scalar2345 - mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1345 + mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1245 - mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1235 + mv1.KVector1.Scalar5 * mv2.KVector4.Scalar1234;
            }
            
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[15] += mv1.KVector1.Scalar5 * mv2.KVector5.Scalar12345;
                tempScalar[23] += -mv1.KVector1.Scalar4 * mv2.KVector5.Scalar12345;
                tempScalar[27] += mv1.KVector1.Scalar3 * mv2.KVector5.Scalar12345;
                tempScalar[29] += -mv1.KVector1.Scalar2 * mv2.KVector5.Scalar12345;
                tempScalar[30] += -mv1.KVector1.Scalar1 * mv2.KVector5.Scalar12345;
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
                tempScalar[17] += mv1.KVector2.Scalar15 * mv2.KVector0.Scalar;
                tempScalar[18] += mv1.KVector2.Scalar25 * mv2.KVector0.Scalar;
                tempScalar[20] += mv1.KVector2.Scalar35 * mv2.KVector0.Scalar;
                tempScalar[24] += mv1.KVector2.Scalar45 * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[7] += mv1.KVector2.Scalar12 * mv2.KVector1.Scalar3 - mv1.KVector2.Scalar13 * mv2.KVector1.Scalar2 + mv1.KVector2.Scalar23 * mv2.KVector1.Scalar1;
                tempScalar[11] += mv1.KVector2.Scalar12 * mv2.KVector1.Scalar4 - mv1.KVector2.Scalar14 * mv2.KVector1.Scalar2 + mv1.KVector2.Scalar24 * mv2.KVector1.Scalar1;
                tempScalar[13] += mv1.KVector2.Scalar13 * mv2.KVector1.Scalar4 - mv1.KVector2.Scalar14 * mv2.KVector1.Scalar3 + mv1.KVector2.Scalar34 * mv2.KVector1.Scalar1;
                tempScalar[14] += mv1.KVector2.Scalar23 * mv2.KVector1.Scalar4 - mv1.KVector2.Scalar24 * mv2.KVector1.Scalar3 + mv1.KVector2.Scalar34 * mv2.KVector1.Scalar2;
                tempScalar[19] += mv1.KVector2.Scalar12 * mv2.KVector1.Scalar5 - mv1.KVector2.Scalar15 * mv2.KVector1.Scalar2 + mv1.KVector2.Scalar25 * mv2.KVector1.Scalar1;
                tempScalar[21] += mv1.KVector2.Scalar13 * mv2.KVector1.Scalar5 - mv1.KVector2.Scalar15 * mv2.KVector1.Scalar3 + mv1.KVector2.Scalar35 * mv2.KVector1.Scalar1;
                tempScalar[22] += mv1.KVector2.Scalar23 * mv2.KVector1.Scalar5 - mv1.KVector2.Scalar25 * mv2.KVector1.Scalar3 + mv1.KVector2.Scalar35 * mv2.KVector1.Scalar2;
                tempScalar[25] += mv1.KVector2.Scalar14 * mv2.KVector1.Scalar5 - mv1.KVector2.Scalar15 * mv2.KVector1.Scalar4 + mv1.KVector2.Scalar45 * mv2.KVector1.Scalar1;
                tempScalar[26] += mv1.KVector2.Scalar24 * mv2.KVector1.Scalar5 - mv1.KVector2.Scalar25 * mv2.KVector1.Scalar4 + mv1.KVector2.Scalar45 * mv2.KVector1.Scalar2;
                tempScalar[28] += mv1.KVector2.Scalar34 * mv2.KVector1.Scalar5 - mv1.KVector2.Scalar35 * mv2.KVector1.Scalar4 + mv1.KVector2.Scalar45 * mv2.KVector1.Scalar3;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[0] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar12 + mv1.KVector2.Scalar13 * mv2.KVector2.Scalar13 - mv1.KVector2.Scalar23 * mv2.KVector2.Scalar23 + mv1.KVector2.Scalar14 * mv2.KVector2.Scalar14 - mv1.KVector2.Scalar24 * mv2.KVector2.Scalar24 - mv1.KVector2.Scalar34 * mv2.KVector2.Scalar34 + mv1.KVector2.Scalar15 * mv2.KVector2.Scalar15 - mv1.KVector2.Scalar25 * mv2.KVector2.Scalar25 - mv1.KVector2.Scalar35 * mv2.KVector2.Scalar35 - mv1.KVector2.Scalar45 * mv2.KVector2.Scalar45;
                tempScalar[15] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar34 - mv1.KVector2.Scalar13 * mv2.KVector2.Scalar24 + mv1.KVector2.Scalar23 * mv2.KVector2.Scalar14 + mv1.KVector2.Scalar14 * mv2.KVector2.Scalar23 - mv1.KVector2.Scalar24 * mv2.KVector2.Scalar13 + mv1.KVector2.Scalar34 * mv2.KVector2.Scalar12;
                tempScalar[23] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar35 - mv1.KVector2.Scalar13 * mv2.KVector2.Scalar25 + mv1.KVector2.Scalar23 * mv2.KVector2.Scalar15 + mv1.KVector2.Scalar15 * mv2.KVector2.Scalar23 - mv1.KVector2.Scalar25 * mv2.KVector2.Scalar13 + mv1.KVector2.Scalar35 * mv2.KVector2.Scalar12;
                tempScalar[27] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar45 - mv1.KVector2.Scalar14 * mv2.KVector2.Scalar25 + mv1.KVector2.Scalar24 * mv2.KVector2.Scalar15 + mv1.KVector2.Scalar15 * mv2.KVector2.Scalar24 - mv1.KVector2.Scalar25 * mv2.KVector2.Scalar14 + mv1.KVector2.Scalar45 * mv2.KVector2.Scalar12;
                tempScalar[29] += mv1.KVector2.Scalar13 * mv2.KVector2.Scalar45 - mv1.KVector2.Scalar14 * mv2.KVector2.Scalar35 + mv1.KVector2.Scalar34 * mv2.KVector2.Scalar15 + mv1.KVector2.Scalar15 * mv2.KVector2.Scalar34 - mv1.KVector2.Scalar35 * mv2.KVector2.Scalar14 + mv1.KVector2.Scalar45 * mv2.KVector2.Scalar13;
                tempScalar[30] += mv1.KVector2.Scalar23 * mv2.KVector2.Scalar45 - mv1.KVector2.Scalar24 * mv2.KVector2.Scalar35 + mv1.KVector2.Scalar34 * mv2.KVector2.Scalar25 + mv1.KVector2.Scalar25 * mv2.KVector2.Scalar34 - mv1.KVector2.Scalar35 * mv2.KVector2.Scalar24 + mv1.KVector2.Scalar45 * mv2.KVector2.Scalar23;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[1] += -mv1.KVector2.Scalar23 * mv2.KVector3.Scalar123 - mv1.KVector2.Scalar24 * mv2.KVector3.Scalar124 - mv1.KVector2.Scalar34 * mv2.KVector3.Scalar134 - mv1.KVector2.Scalar25 * mv2.KVector3.Scalar125 - mv1.KVector2.Scalar35 * mv2.KVector3.Scalar135 - mv1.KVector2.Scalar45 * mv2.KVector3.Scalar145;
                tempScalar[2] += -mv1.KVector2.Scalar13 * mv2.KVector3.Scalar123 - mv1.KVector2.Scalar14 * mv2.KVector3.Scalar124 - mv1.KVector2.Scalar34 * mv2.KVector3.Scalar234 - mv1.KVector2.Scalar15 * mv2.KVector3.Scalar125 - mv1.KVector2.Scalar35 * mv2.KVector3.Scalar235 - mv1.KVector2.Scalar45 * mv2.KVector3.Scalar245;
                tempScalar[4] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar123 - mv1.KVector2.Scalar14 * mv2.KVector3.Scalar134 + mv1.KVector2.Scalar24 * mv2.KVector3.Scalar234 - mv1.KVector2.Scalar15 * mv2.KVector3.Scalar135 + mv1.KVector2.Scalar25 * mv2.KVector3.Scalar235 - mv1.KVector2.Scalar45 * mv2.KVector3.Scalar345;
                tempScalar[8] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar124 + mv1.KVector2.Scalar13 * mv2.KVector3.Scalar134 - mv1.KVector2.Scalar23 * mv2.KVector3.Scalar234 - mv1.KVector2.Scalar15 * mv2.KVector3.Scalar145 + mv1.KVector2.Scalar25 * mv2.KVector3.Scalar245 + mv1.KVector2.Scalar35 * mv2.KVector3.Scalar345;
                tempScalar[16] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar125 + mv1.KVector2.Scalar13 * mv2.KVector3.Scalar135 - mv1.KVector2.Scalar23 * mv2.KVector3.Scalar235 + mv1.KVector2.Scalar14 * mv2.KVector3.Scalar145 - mv1.KVector2.Scalar24 * mv2.KVector3.Scalar245 - mv1.KVector2.Scalar34 * mv2.KVector3.Scalar345;
                tempScalar[31] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar345 - mv1.KVector2.Scalar13 * mv2.KVector3.Scalar245 + mv1.KVector2.Scalar23 * mv2.KVector3.Scalar145 + mv1.KVector2.Scalar14 * mv2.KVector3.Scalar235 - mv1.KVector2.Scalar24 * mv2.KVector3.Scalar135 + mv1.KVector2.Scalar34 * mv2.KVector3.Scalar125 - mv1.KVector2.Scalar15 * mv2.KVector3.Scalar234 + mv1.KVector2.Scalar25 * mv2.KVector3.Scalar134 - mv1.KVector2.Scalar35 * mv2.KVector3.Scalar124 + mv1.KVector2.Scalar45 * mv2.KVector3.Scalar123;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[3] += -mv1.KVector2.Scalar34 * mv2.KVector4.Scalar1234 - mv1.KVector2.Scalar35 * mv2.KVector4.Scalar1235 - mv1.KVector2.Scalar45 * mv2.KVector4.Scalar1245;
                tempScalar[5] += mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1234 + mv1.KVector2.Scalar25 * mv2.KVector4.Scalar1235 - mv1.KVector2.Scalar45 * mv2.KVector4.Scalar1345;
                tempScalar[6] += mv1.KVector2.Scalar14 * mv2.KVector4.Scalar1234 + mv1.KVector2.Scalar15 * mv2.KVector4.Scalar1235 - mv1.KVector2.Scalar45 * mv2.KVector4.Scalar2345;
                tempScalar[9] += -mv1.KVector2.Scalar23 * mv2.KVector4.Scalar1234 + mv1.KVector2.Scalar25 * mv2.KVector4.Scalar1245 + mv1.KVector2.Scalar35 * mv2.KVector4.Scalar1345;
                tempScalar[10] += -mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1234 + mv1.KVector2.Scalar15 * mv2.KVector4.Scalar1245 + mv1.KVector2.Scalar35 * mv2.KVector4.Scalar2345;
                tempScalar[12] += mv1.KVector2.Scalar12 * mv2.KVector4.Scalar1234 + mv1.KVector2.Scalar15 * mv2.KVector4.Scalar1345 - mv1.KVector2.Scalar25 * mv2.KVector4.Scalar2345;
                tempScalar[17] += -mv1.KVector2.Scalar23 * mv2.KVector4.Scalar1235 - mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1245 - mv1.KVector2.Scalar34 * mv2.KVector4.Scalar1345;
                tempScalar[18] += -mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1235 - mv1.KVector2.Scalar14 * mv2.KVector4.Scalar1245 - mv1.KVector2.Scalar34 * mv2.KVector4.Scalar2345;
                tempScalar[20] += mv1.KVector2.Scalar12 * mv2.KVector4.Scalar1235 - mv1.KVector2.Scalar14 * mv2.KVector4.Scalar1345 + mv1.KVector2.Scalar24 * mv2.KVector4.Scalar2345;
                tempScalar[24] += mv1.KVector2.Scalar12 * mv2.KVector4.Scalar1245 + mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1345 - mv1.KVector2.Scalar23 * mv2.KVector4.Scalar2345;
            }
            
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[7] += -mv1.KVector2.Scalar45 * mv2.KVector5.Scalar12345;
                tempScalar[11] += mv1.KVector2.Scalar35 * mv2.KVector5.Scalar12345;
                tempScalar[13] += -mv1.KVector2.Scalar25 * mv2.KVector5.Scalar12345;
                tempScalar[14] += -mv1.KVector2.Scalar15 * mv2.KVector5.Scalar12345;
                tempScalar[19] += -mv1.KVector2.Scalar34 * mv2.KVector5.Scalar12345;
                tempScalar[21] += mv1.KVector2.Scalar24 * mv2.KVector5.Scalar12345;
                tempScalar[22] += mv1.KVector2.Scalar14 * mv2.KVector5.Scalar12345;
                tempScalar[25] += -mv1.KVector2.Scalar23 * mv2.KVector5.Scalar12345;
                tempScalar[26] += -mv1.KVector2.Scalar13 * mv2.KVector5.Scalar12345;
                tempScalar[28] += mv1.KVector2.Scalar12 * mv2.KVector5.Scalar12345;
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
                tempScalar[19] += mv1.KVector3.Scalar125 * mv2.KVector0.Scalar;
                tempScalar[21] += mv1.KVector3.Scalar135 * mv2.KVector0.Scalar;
                tempScalar[22] += mv1.KVector3.Scalar235 * mv2.KVector0.Scalar;
                tempScalar[25] += mv1.KVector3.Scalar145 * mv2.KVector0.Scalar;
                tempScalar[26] += mv1.KVector3.Scalar245 * mv2.KVector0.Scalar;
                tempScalar[28] += mv1.KVector3.Scalar345 * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[3] += mv1.KVector3.Scalar123 * mv2.KVector1.Scalar3 + mv1.KVector3.Scalar124 * mv2.KVector1.Scalar4 + mv1.KVector3.Scalar125 * mv2.KVector1.Scalar5;
                tempScalar[5] += -mv1.KVector3.Scalar123 * mv2.KVector1.Scalar2 + mv1.KVector3.Scalar134 * mv2.KVector1.Scalar4 + mv1.KVector3.Scalar135 * mv2.KVector1.Scalar5;
                tempScalar[6] += -mv1.KVector3.Scalar123 * mv2.KVector1.Scalar1 + mv1.KVector3.Scalar234 * mv2.KVector1.Scalar4 + mv1.KVector3.Scalar235 * mv2.KVector1.Scalar5;
                tempScalar[9] += -mv1.KVector3.Scalar124 * mv2.KVector1.Scalar2 - mv1.KVector3.Scalar134 * mv2.KVector1.Scalar3 + mv1.KVector3.Scalar145 * mv2.KVector1.Scalar5;
                tempScalar[10] += -mv1.KVector3.Scalar124 * mv2.KVector1.Scalar1 - mv1.KVector3.Scalar234 * mv2.KVector1.Scalar3 + mv1.KVector3.Scalar245 * mv2.KVector1.Scalar5;
                tempScalar[12] += -mv1.KVector3.Scalar134 * mv2.KVector1.Scalar1 + mv1.KVector3.Scalar234 * mv2.KVector1.Scalar2 + mv1.KVector3.Scalar345 * mv2.KVector1.Scalar5;
                tempScalar[17] += -mv1.KVector3.Scalar125 * mv2.KVector1.Scalar2 - mv1.KVector3.Scalar135 * mv2.KVector1.Scalar3 - mv1.KVector3.Scalar145 * mv2.KVector1.Scalar4;
                tempScalar[18] += -mv1.KVector3.Scalar125 * mv2.KVector1.Scalar1 - mv1.KVector3.Scalar235 * mv2.KVector1.Scalar3 - mv1.KVector3.Scalar245 * mv2.KVector1.Scalar4;
                tempScalar[20] += -mv1.KVector3.Scalar135 * mv2.KVector1.Scalar1 + mv1.KVector3.Scalar235 * mv2.KVector1.Scalar2 - mv1.KVector3.Scalar345 * mv2.KVector1.Scalar4;
                tempScalar[24] += -mv1.KVector3.Scalar145 * mv2.KVector1.Scalar1 + mv1.KVector3.Scalar245 * mv2.KVector1.Scalar2 + mv1.KVector3.Scalar345 * mv2.KVector1.Scalar3;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[1] += -mv1.KVector3.Scalar123 * mv2.KVector2.Scalar23 - mv1.KVector3.Scalar124 * mv2.KVector2.Scalar24 - mv1.KVector3.Scalar134 * mv2.KVector2.Scalar34 - mv1.KVector3.Scalar125 * mv2.KVector2.Scalar25 - mv1.KVector3.Scalar135 * mv2.KVector2.Scalar35 - mv1.KVector3.Scalar145 * mv2.KVector2.Scalar45;
                tempScalar[2] += -mv1.KVector3.Scalar123 * mv2.KVector2.Scalar13 - mv1.KVector3.Scalar124 * mv2.KVector2.Scalar14 - mv1.KVector3.Scalar234 * mv2.KVector2.Scalar34 - mv1.KVector3.Scalar125 * mv2.KVector2.Scalar15 - mv1.KVector3.Scalar235 * mv2.KVector2.Scalar35 - mv1.KVector3.Scalar245 * mv2.KVector2.Scalar45;
                tempScalar[4] += mv1.KVector3.Scalar123 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar134 * mv2.KVector2.Scalar14 + mv1.KVector3.Scalar234 * mv2.KVector2.Scalar24 - mv1.KVector3.Scalar135 * mv2.KVector2.Scalar15 + mv1.KVector3.Scalar235 * mv2.KVector2.Scalar25 - mv1.KVector3.Scalar345 * mv2.KVector2.Scalar45;
                tempScalar[8] += mv1.KVector3.Scalar124 * mv2.KVector2.Scalar12 + mv1.KVector3.Scalar134 * mv2.KVector2.Scalar13 - mv1.KVector3.Scalar234 * mv2.KVector2.Scalar23 - mv1.KVector3.Scalar145 * mv2.KVector2.Scalar15 + mv1.KVector3.Scalar245 * mv2.KVector2.Scalar25 + mv1.KVector3.Scalar345 * mv2.KVector2.Scalar35;
                tempScalar[16] += mv1.KVector3.Scalar125 * mv2.KVector2.Scalar12 + mv1.KVector3.Scalar135 * mv2.KVector2.Scalar13 - mv1.KVector3.Scalar235 * mv2.KVector2.Scalar23 + mv1.KVector3.Scalar145 * mv2.KVector2.Scalar14 - mv1.KVector3.Scalar245 * mv2.KVector2.Scalar24 - mv1.KVector3.Scalar345 * mv2.KVector2.Scalar34;
                tempScalar[31] += mv1.KVector3.Scalar123 * mv2.KVector2.Scalar45 - mv1.KVector3.Scalar124 * mv2.KVector2.Scalar35 + mv1.KVector3.Scalar134 * mv2.KVector2.Scalar25 - mv1.KVector3.Scalar234 * mv2.KVector2.Scalar15 + mv1.KVector3.Scalar125 * mv2.KVector2.Scalar34 - mv1.KVector3.Scalar135 * mv2.KVector2.Scalar24 + mv1.KVector3.Scalar235 * mv2.KVector2.Scalar14 + mv1.KVector3.Scalar145 * mv2.KVector2.Scalar23 - mv1.KVector3.Scalar245 * mv2.KVector2.Scalar13 + mv1.KVector3.Scalar345 * mv2.KVector2.Scalar12;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[0] += mv1.KVector3.Scalar123 * mv2.KVector3.Scalar123 + mv1.KVector3.Scalar124 * mv2.KVector3.Scalar124 + mv1.KVector3.Scalar134 * mv2.KVector3.Scalar134 - mv1.KVector3.Scalar234 * mv2.KVector3.Scalar234 + mv1.KVector3.Scalar125 * mv2.KVector3.Scalar125 + mv1.KVector3.Scalar135 * mv2.KVector3.Scalar135 - mv1.KVector3.Scalar235 * mv2.KVector3.Scalar235 + mv1.KVector3.Scalar145 * mv2.KVector3.Scalar145 - mv1.KVector3.Scalar245 * mv2.KVector3.Scalar245 - mv1.KVector3.Scalar345 * mv2.KVector3.Scalar345;
                tempScalar[15] += mv1.KVector3.Scalar125 * mv2.KVector3.Scalar345 - mv1.KVector3.Scalar135 * mv2.KVector3.Scalar245 + mv1.KVector3.Scalar235 * mv2.KVector3.Scalar145 + mv1.KVector3.Scalar145 * mv2.KVector3.Scalar235 - mv1.KVector3.Scalar245 * mv2.KVector3.Scalar135 + mv1.KVector3.Scalar345 * mv2.KVector3.Scalar125;
                tempScalar[23] += -mv1.KVector3.Scalar124 * mv2.KVector3.Scalar345 + mv1.KVector3.Scalar134 * mv2.KVector3.Scalar245 - mv1.KVector3.Scalar234 * mv2.KVector3.Scalar145 - mv1.KVector3.Scalar145 * mv2.KVector3.Scalar234 + mv1.KVector3.Scalar245 * mv2.KVector3.Scalar134 - mv1.KVector3.Scalar345 * mv2.KVector3.Scalar124;
                tempScalar[27] += mv1.KVector3.Scalar123 * mv2.KVector3.Scalar345 - mv1.KVector3.Scalar134 * mv2.KVector3.Scalar235 + mv1.KVector3.Scalar234 * mv2.KVector3.Scalar135 + mv1.KVector3.Scalar135 * mv2.KVector3.Scalar234 - mv1.KVector3.Scalar235 * mv2.KVector3.Scalar134 + mv1.KVector3.Scalar345 * mv2.KVector3.Scalar123;
                tempScalar[29] += -mv1.KVector3.Scalar123 * mv2.KVector3.Scalar245 + mv1.KVector3.Scalar124 * mv2.KVector3.Scalar235 - mv1.KVector3.Scalar234 * mv2.KVector3.Scalar125 - mv1.KVector3.Scalar125 * mv2.KVector3.Scalar234 + mv1.KVector3.Scalar235 * mv2.KVector3.Scalar124 - mv1.KVector3.Scalar245 * mv2.KVector3.Scalar123;
                tempScalar[30] += -mv1.KVector3.Scalar123 * mv2.KVector3.Scalar145 + mv1.KVector3.Scalar124 * mv2.KVector3.Scalar135 - mv1.KVector3.Scalar134 * mv2.KVector3.Scalar125 - mv1.KVector3.Scalar125 * mv2.KVector3.Scalar134 + mv1.KVector3.Scalar135 * mv2.KVector3.Scalar124 - mv1.KVector3.Scalar145 * mv2.KVector3.Scalar123;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[7] += -mv1.KVector3.Scalar145 * mv2.KVector4.Scalar2345 + mv1.KVector3.Scalar245 * mv2.KVector4.Scalar1345 - mv1.KVector3.Scalar345 * mv2.KVector4.Scalar1245;
                tempScalar[11] += mv1.KVector3.Scalar135 * mv2.KVector4.Scalar2345 - mv1.KVector3.Scalar235 * mv2.KVector4.Scalar1345 + mv1.KVector3.Scalar345 * mv2.KVector4.Scalar1235;
                tempScalar[13] += -mv1.KVector3.Scalar125 * mv2.KVector4.Scalar2345 + mv1.KVector3.Scalar235 * mv2.KVector4.Scalar1245 - mv1.KVector3.Scalar245 * mv2.KVector4.Scalar1235;
                tempScalar[14] += -mv1.KVector3.Scalar125 * mv2.KVector4.Scalar1345 + mv1.KVector3.Scalar135 * mv2.KVector4.Scalar1245 - mv1.KVector3.Scalar145 * mv2.KVector4.Scalar1235;
                tempScalar[19] += -mv1.KVector3.Scalar134 * mv2.KVector4.Scalar2345 + mv1.KVector3.Scalar234 * mv2.KVector4.Scalar1345 - mv1.KVector3.Scalar345 * mv2.KVector4.Scalar1234;
                tempScalar[21] += mv1.KVector3.Scalar124 * mv2.KVector4.Scalar2345 - mv1.KVector3.Scalar234 * mv2.KVector4.Scalar1245 + mv1.KVector3.Scalar245 * mv2.KVector4.Scalar1234;
                tempScalar[22] += mv1.KVector3.Scalar124 * mv2.KVector4.Scalar1345 - mv1.KVector3.Scalar134 * mv2.KVector4.Scalar1245 + mv1.KVector3.Scalar145 * mv2.KVector4.Scalar1234;
                tempScalar[25] += -mv1.KVector3.Scalar123 * mv2.KVector4.Scalar2345 + mv1.KVector3.Scalar234 * mv2.KVector4.Scalar1235 - mv1.KVector3.Scalar235 * mv2.KVector4.Scalar1234;
                tempScalar[26] += -mv1.KVector3.Scalar123 * mv2.KVector4.Scalar1345 + mv1.KVector3.Scalar134 * mv2.KVector4.Scalar1235 - mv1.KVector3.Scalar135 * mv2.KVector4.Scalar1234;
                tempScalar[28] += mv1.KVector3.Scalar123 * mv2.KVector4.Scalar1245 - mv1.KVector3.Scalar124 * mv2.KVector4.Scalar1235 + mv1.KVector3.Scalar125 * mv2.KVector4.Scalar1234;
            }
            
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[3] += -mv1.KVector3.Scalar345 * mv2.KVector5.Scalar12345;
                tempScalar[5] += mv1.KVector3.Scalar245 * mv2.KVector5.Scalar12345;
                tempScalar[6] += mv1.KVector3.Scalar145 * mv2.KVector5.Scalar12345;
                tempScalar[9] += -mv1.KVector3.Scalar235 * mv2.KVector5.Scalar12345;
                tempScalar[10] += -mv1.KVector3.Scalar135 * mv2.KVector5.Scalar12345;
                tempScalar[12] += mv1.KVector3.Scalar125 * mv2.KVector5.Scalar12345;
                tempScalar[17] += mv1.KVector3.Scalar234 * mv2.KVector5.Scalar12345;
                tempScalar[18] += mv1.KVector3.Scalar134 * mv2.KVector5.Scalar12345;
                tempScalar[20] += -mv1.KVector3.Scalar124 * mv2.KVector5.Scalar12345;
                tempScalar[24] += mv1.KVector3.Scalar123 * mv2.KVector5.Scalar12345;
            }
            
        }
        
        if (!mv1.KVector4.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[15] += mv1.KVector4.Scalar1234 * mv2.KVector0.Scalar;
                tempScalar[23] += mv1.KVector4.Scalar1235 * mv2.KVector0.Scalar;
                tempScalar[27] += mv1.KVector4.Scalar1245 * mv2.KVector0.Scalar;
                tempScalar[29] += mv1.KVector4.Scalar1345 * mv2.KVector0.Scalar;
                tempScalar[30] += mv1.KVector4.Scalar2345 * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[31] += mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar5 - mv1.KVector4.Scalar1235 * mv2.KVector1.Scalar4 + mv1.KVector4.Scalar1245 * mv2.KVector1.Scalar3 - mv1.KVector4.Scalar1345 * mv2.KVector1.Scalar2 + mv1.KVector4.Scalar2345 * mv2.KVector1.Scalar1;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[3] += -mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar34 - mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar35 - mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar45;
                tempScalar[5] += mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar24 + mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar25 - mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar45;
                tempScalar[6] += mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar14 + mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar15 - mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar45;
                tempScalar[9] += -mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar23 + mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar25 + mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar35;
                tempScalar[10] += -mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar13 + mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar15 + mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar35;
                tempScalar[12] += mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar12 + mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar15 - mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar25;
                tempScalar[17] += -mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar23 - mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar24 - mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar34;
                tempScalar[18] += -mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar13 - mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar14 - mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar34;
                tempScalar[20] += mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar12 - mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar14 + mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar24;
                tempScalar[24] += mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar12 + mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar13 - mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar23;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[7] += -mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar345 + mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar245 - mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar145;
                tempScalar[11] += mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar345 - mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar235 + mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar135;
                tempScalar[13] += -mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar245 + mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar235 - mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar125;
                tempScalar[14] += -mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar145 + mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar135 - mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar125;
                tempScalar[19] += -mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar345 + mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar234 - mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar134;
                tempScalar[21] += mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar245 - mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar234 + mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar124;
                tempScalar[22] += mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar145 - mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar134 + mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar124;
                tempScalar[25] += -mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar235 + mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar234 - mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar123;
                tempScalar[26] += -mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar135 + mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar134 - mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar123;
                tempScalar[28] += mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar125 - mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar124 + mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar123;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[0] += -mv1.KVector4.Scalar1234 * mv2.KVector4.Scalar1234 - mv1.KVector4.Scalar1235 * mv2.KVector4.Scalar1235 - mv1.KVector4.Scalar1245 * mv2.KVector4.Scalar1245 - mv1.KVector4.Scalar1345 * mv2.KVector4.Scalar1345 + mv1.KVector4.Scalar2345 * mv2.KVector4.Scalar2345;
            }
            
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[1] += mv1.KVector4.Scalar2345 * mv2.KVector5.Scalar12345;
                tempScalar[2] += mv1.KVector4.Scalar1345 * mv2.KVector5.Scalar12345;
                tempScalar[4] += -mv1.KVector4.Scalar1245 * mv2.KVector5.Scalar12345;
                tempScalar[8] += mv1.KVector4.Scalar1235 * mv2.KVector5.Scalar12345;
                tempScalar[16] += -mv1.KVector4.Scalar1234 * mv2.KVector5.Scalar12345;
            }
            
        }
        
        if (!mv1.KVector5.IsZero())
        {
            if (!mv2.KVector0.IsZero())
            {
                tempScalar[31] += mv1.KVector5.Scalar12345 * mv2.KVector0.Scalar;
            }
            
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[15] += mv1.KVector5.Scalar12345 * mv2.KVector1.Scalar5;
                tempScalar[23] += -mv1.KVector5.Scalar12345 * mv2.KVector1.Scalar4;
                tempScalar[27] += mv1.KVector5.Scalar12345 * mv2.KVector1.Scalar3;
                tempScalar[29] += -mv1.KVector5.Scalar12345 * mv2.KVector1.Scalar2;
                tempScalar[30] += -mv1.KVector5.Scalar12345 * mv2.KVector1.Scalar1;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[7] += -mv1.KVector5.Scalar12345 * mv2.KVector2.Scalar45;
                tempScalar[11] += mv1.KVector5.Scalar12345 * mv2.KVector2.Scalar35;
                tempScalar[13] += -mv1.KVector5.Scalar12345 * mv2.KVector2.Scalar25;
                tempScalar[14] += -mv1.KVector5.Scalar12345 * mv2.KVector2.Scalar15;
                tempScalar[19] += -mv1.KVector5.Scalar12345 * mv2.KVector2.Scalar34;
                tempScalar[21] += mv1.KVector5.Scalar12345 * mv2.KVector2.Scalar24;
                tempScalar[22] += mv1.KVector5.Scalar12345 * mv2.KVector2.Scalar14;
                tempScalar[25] += -mv1.KVector5.Scalar12345 * mv2.KVector2.Scalar23;
                tempScalar[26] += -mv1.KVector5.Scalar12345 * mv2.KVector2.Scalar13;
                tempScalar[28] += mv1.KVector5.Scalar12345 * mv2.KVector2.Scalar12;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[3] += -mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar345;
                tempScalar[5] += mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar245;
                tempScalar[6] += mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar145;
                tempScalar[9] += -mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar235;
                tempScalar[10] += -mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar135;
                tempScalar[12] += mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar125;
                tempScalar[17] += mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar234;
                tempScalar[18] += mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar134;
                tempScalar[20] += -mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar124;
                tempScalar[24] += mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar123;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[1] += mv1.KVector5.Scalar12345 * mv2.KVector4.Scalar2345;
                tempScalar[2] += mv1.KVector5.Scalar12345 * mv2.KVector4.Scalar1345;
                tempScalar[4] += -mv1.KVector5.Scalar12345 * mv2.KVector4.Scalar1245;
                tempScalar[8] += mv1.KVector5.Scalar12345 * mv2.KVector4.Scalar1235;
                tempScalar[16] += -mv1.KVector5.Scalar12345 * mv2.KVector4.Scalar1234;
            }
            
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[0] += -mv1.KVector5.Scalar12345 * mv2.KVector5.Scalar12345;
            }
            
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
}
