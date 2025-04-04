using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga51;

public static class Ga51LeftContractionProduct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector0 mv1, Ga51KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector0.Zero;
        
        return new Ga51KVector0
        {
            Scalar = mv1.Scalar * mv2.Scalar
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector1 Lcp(this Ga51KVector0 mv1, Ga51KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector1.Zero;
        
        return new Ga51KVector1
        {
            Scalar1 = mv1.Scalar * mv2.Scalar1,
            Scalar2 = mv1.Scalar * mv2.Scalar2,
            Scalar3 = mv1.Scalar * mv2.Scalar3,
            Scalar4 = mv1.Scalar * mv2.Scalar4,
            Scalar5 = mv1.Scalar * mv2.Scalar5,
            Scalar6 = mv1.Scalar * mv2.Scalar6
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector2 Lcp(this Ga51KVector0 mv1, Ga51KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector2.Zero;
        
        return new Ga51KVector2
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
            Scalar45 = mv1.Scalar * mv2.Scalar45,
            Scalar16 = mv1.Scalar * mv2.Scalar16,
            Scalar26 = mv1.Scalar * mv2.Scalar26,
            Scalar36 = mv1.Scalar * mv2.Scalar36,
            Scalar46 = mv1.Scalar * mv2.Scalar46,
            Scalar56 = mv1.Scalar * mv2.Scalar56
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector3 Lcp(this Ga51KVector0 mv1, Ga51KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector3.Zero;
        
        return new Ga51KVector3
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
            Scalar345 = mv1.Scalar * mv2.Scalar345,
            Scalar126 = mv1.Scalar * mv2.Scalar126,
            Scalar136 = mv1.Scalar * mv2.Scalar136,
            Scalar236 = mv1.Scalar * mv2.Scalar236,
            Scalar146 = mv1.Scalar * mv2.Scalar146,
            Scalar246 = mv1.Scalar * mv2.Scalar246,
            Scalar346 = mv1.Scalar * mv2.Scalar346,
            Scalar156 = mv1.Scalar * mv2.Scalar156,
            Scalar256 = mv1.Scalar * mv2.Scalar256,
            Scalar356 = mv1.Scalar * mv2.Scalar356,
            Scalar456 = mv1.Scalar * mv2.Scalar456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector4 Lcp(this Ga51KVector0 mv1, Ga51KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector4.Zero;
        
        return new Ga51KVector4
        {
            Scalar1234 = mv1.Scalar * mv2.Scalar1234,
            Scalar1235 = mv1.Scalar * mv2.Scalar1235,
            Scalar1245 = mv1.Scalar * mv2.Scalar1245,
            Scalar1345 = mv1.Scalar * mv2.Scalar1345,
            Scalar2345 = mv1.Scalar * mv2.Scalar2345,
            Scalar1236 = mv1.Scalar * mv2.Scalar1236,
            Scalar1246 = mv1.Scalar * mv2.Scalar1246,
            Scalar1346 = mv1.Scalar * mv2.Scalar1346,
            Scalar2346 = mv1.Scalar * mv2.Scalar2346,
            Scalar1256 = mv1.Scalar * mv2.Scalar1256,
            Scalar1356 = mv1.Scalar * mv2.Scalar1356,
            Scalar2356 = mv1.Scalar * mv2.Scalar2356,
            Scalar1456 = mv1.Scalar * mv2.Scalar1456,
            Scalar2456 = mv1.Scalar * mv2.Scalar2456,
            Scalar3456 = mv1.Scalar * mv2.Scalar3456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector5 Lcp(this Ga51KVector0 mv1, Ga51KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector5.Zero;
        
        return new Ga51KVector5
        {
            Scalar12345 = mv1.Scalar * mv2.Scalar12345,
            Scalar12346 = mv1.Scalar * mv2.Scalar12346,
            Scalar12356 = mv1.Scalar * mv2.Scalar12356,
            Scalar12456 = mv1.Scalar * mv2.Scalar12456,
            Scalar13456 = mv1.Scalar * mv2.Scalar13456,
            Scalar23456 = mv1.Scalar * mv2.Scalar23456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector6 Lcp(this Ga51KVector0 mv1, Ga51KVector6 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector6.Zero;
        
        return new Ga51KVector6
        {
            Scalar123456 = mv1.Scalar * mv2.Scalar123456
        };
    }
    
    public static Ga51Multivector Lcp(this Ga51KVector0 mv1, Ga51Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51Multivector.Zero;
        
        var tempScalar = new double[64];
        
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
            tempScalar[32] += mv1.Scalar * mv2.KVector1.Scalar6;
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
            tempScalar[33] += mv1.Scalar * mv2.KVector2.Scalar16;
            tempScalar[34] += mv1.Scalar * mv2.KVector2.Scalar26;
            tempScalar[36] += mv1.Scalar * mv2.KVector2.Scalar36;
            tempScalar[40] += mv1.Scalar * mv2.KVector2.Scalar46;
            tempScalar[48] += mv1.Scalar * mv2.KVector2.Scalar56;
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
            tempScalar[35] += mv1.Scalar * mv2.KVector3.Scalar126;
            tempScalar[37] += mv1.Scalar * mv2.KVector3.Scalar136;
            tempScalar[38] += mv1.Scalar * mv2.KVector3.Scalar236;
            tempScalar[41] += mv1.Scalar * mv2.KVector3.Scalar146;
            tempScalar[42] += mv1.Scalar * mv2.KVector3.Scalar246;
            tempScalar[44] += mv1.Scalar * mv2.KVector3.Scalar346;
            tempScalar[49] += mv1.Scalar * mv2.KVector3.Scalar156;
            tempScalar[50] += mv1.Scalar * mv2.KVector3.Scalar256;
            tempScalar[52] += mv1.Scalar * mv2.KVector3.Scalar356;
            tempScalar[56] += mv1.Scalar * mv2.KVector3.Scalar456;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[15] += mv1.Scalar * mv2.KVector4.Scalar1234;
            tempScalar[23] += mv1.Scalar * mv2.KVector4.Scalar1235;
            tempScalar[27] += mv1.Scalar * mv2.KVector4.Scalar1245;
            tempScalar[29] += mv1.Scalar * mv2.KVector4.Scalar1345;
            tempScalar[30] += mv1.Scalar * mv2.KVector4.Scalar2345;
            tempScalar[39] += mv1.Scalar * mv2.KVector4.Scalar1236;
            tempScalar[43] += mv1.Scalar * mv2.KVector4.Scalar1246;
            tempScalar[45] += mv1.Scalar * mv2.KVector4.Scalar1346;
            tempScalar[46] += mv1.Scalar * mv2.KVector4.Scalar2346;
            tempScalar[51] += mv1.Scalar * mv2.KVector4.Scalar1256;
            tempScalar[53] += mv1.Scalar * mv2.KVector4.Scalar1356;
            tempScalar[54] += mv1.Scalar * mv2.KVector4.Scalar2356;
            tempScalar[57] += mv1.Scalar * mv2.KVector4.Scalar1456;
            tempScalar[58] += mv1.Scalar * mv2.KVector4.Scalar2456;
            tempScalar[60] += mv1.Scalar * mv2.KVector4.Scalar3456;
        }
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[31] += mv1.Scalar * mv2.KVector5.Scalar12345;
            tempScalar[47] += mv1.Scalar * mv2.KVector5.Scalar12346;
            tempScalar[55] += mv1.Scalar * mv2.KVector5.Scalar12356;
            tempScalar[59] += mv1.Scalar * mv2.KVector5.Scalar12456;
            tempScalar[61] += mv1.Scalar * mv2.KVector5.Scalar13456;
            tempScalar[62] += mv1.Scalar * mv2.KVector5.Scalar23456;
        }
        
        if (!mv2.KVector6.IsZero())
        {
            tempScalar[63] += mv1.Scalar * mv2.KVector6.Scalar123456;
        }
        
        return Ga51Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector1 mv1, Ga51KVector0 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector1 mv1, Ga51KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector0.Zero;
        
        return new Ga51KVector0
        {
            Scalar = -mv1.Scalar1 * mv2.Scalar1 + mv1.Scalar2 * mv2.Scalar2 + mv1.Scalar3 * mv2.Scalar3 + mv1.Scalar4 * mv2.Scalar4 + mv1.Scalar5 * mv2.Scalar5 + mv1.Scalar6 * mv2.Scalar6
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector1 Lcp(this Ga51KVector1 mv1, Ga51KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector1.Zero;
        
        return new Ga51KVector1
        {
            Scalar1 = -mv1.Scalar2 * mv2.Scalar12 - mv1.Scalar3 * mv2.Scalar13 - mv1.Scalar4 * mv2.Scalar14 - mv1.Scalar5 * mv2.Scalar15 - mv1.Scalar6 * mv2.Scalar16,
            Scalar2 = -mv1.Scalar1 * mv2.Scalar12 - mv1.Scalar3 * mv2.Scalar23 - mv1.Scalar4 * mv2.Scalar24 - mv1.Scalar5 * mv2.Scalar25 - mv1.Scalar6 * mv2.Scalar26,
            Scalar3 = -mv1.Scalar1 * mv2.Scalar13 + mv1.Scalar2 * mv2.Scalar23 - mv1.Scalar4 * mv2.Scalar34 - mv1.Scalar5 * mv2.Scalar35 - mv1.Scalar6 * mv2.Scalar36,
            Scalar4 = -mv1.Scalar1 * mv2.Scalar14 + mv1.Scalar2 * mv2.Scalar24 + mv1.Scalar3 * mv2.Scalar34 - mv1.Scalar5 * mv2.Scalar45 - mv1.Scalar6 * mv2.Scalar46,
            Scalar5 = -mv1.Scalar1 * mv2.Scalar15 + mv1.Scalar2 * mv2.Scalar25 + mv1.Scalar3 * mv2.Scalar35 + mv1.Scalar4 * mv2.Scalar45 - mv1.Scalar6 * mv2.Scalar56,
            Scalar6 = -mv1.Scalar1 * mv2.Scalar16 + mv1.Scalar2 * mv2.Scalar26 + mv1.Scalar3 * mv2.Scalar36 + mv1.Scalar4 * mv2.Scalar46 + mv1.Scalar5 * mv2.Scalar56
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector2 Lcp(this Ga51KVector1 mv1, Ga51KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector2.Zero;
        
        return new Ga51KVector2
        {
            Scalar12 = mv1.Scalar3 * mv2.Scalar123 + mv1.Scalar4 * mv2.Scalar124 + mv1.Scalar5 * mv2.Scalar125 + mv1.Scalar6 * mv2.Scalar126,
            Scalar13 = -mv1.Scalar2 * mv2.Scalar123 + mv1.Scalar4 * mv2.Scalar134 + mv1.Scalar5 * mv2.Scalar135 + mv1.Scalar6 * mv2.Scalar136,
            Scalar23 = -mv1.Scalar1 * mv2.Scalar123 + mv1.Scalar4 * mv2.Scalar234 + mv1.Scalar5 * mv2.Scalar235 + mv1.Scalar6 * mv2.Scalar236,
            Scalar14 = -mv1.Scalar2 * mv2.Scalar124 - mv1.Scalar3 * mv2.Scalar134 + mv1.Scalar5 * mv2.Scalar145 + mv1.Scalar6 * mv2.Scalar146,
            Scalar24 = -mv1.Scalar1 * mv2.Scalar124 - mv1.Scalar3 * mv2.Scalar234 + mv1.Scalar5 * mv2.Scalar245 + mv1.Scalar6 * mv2.Scalar246,
            Scalar34 = -mv1.Scalar1 * mv2.Scalar134 + mv1.Scalar2 * mv2.Scalar234 + mv1.Scalar5 * mv2.Scalar345 + mv1.Scalar6 * mv2.Scalar346,
            Scalar15 = -mv1.Scalar2 * mv2.Scalar125 - mv1.Scalar3 * mv2.Scalar135 - mv1.Scalar4 * mv2.Scalar145 + mv1.Scalar6 * mv2.Scalar156,
            Scalar25 = -mv1.Scalar1 * mv2.Scalar125 - mv1.Scalar3 * mv2.Scalar235 - mv1.Scalar4 * mv2.Scalar245 + mv1.Scalar6 * mv2.Scalar256,
            Scalar35 = -mv1.Scalar1 * mv2.Scalar135 + mv1.Scalar2 * mv2.Scalar235 - mv1.Scalar4 * mv2.Scalar345 + mv1.Scalar6 * mv2.Scalar356,
            Scalar45 = -mv1.Scalar1 * mv2.Scalar145 + mv1.Scalar2 * mv2.Scalar245 + mv1.Scalar3 * mv2.Scalar345 + mv1.Scalar6 * mv2.Scalar456,
            Scalar16 = -mv1.Scalar2 * mv2.Scalar126 - mv1.Scalar3 * mv2.Scalar136 - mv1.Scalar4 * mv2.Scalar146 - mv1.Scalar5 * mv2.Scalar156,
            Scalar26 = -mv1.Scalar1 * mv2.Scalar126 - mv1.Scalar3 * mv2.Scalar236 - mv1.Scalar4 * mv2.Scalar246 - mv1.Scalar5 * mv2.Scalar256,
            Scalar36 = -mv1.Scalar1 * mv2.Scalar136 + mv1.Scalar2 * mv2.Scalar236 - mv1.Scalar4 * mv2.Scalar346 - mv1.Scalar5 * mv2.Scalar356,
            Scalar46 = -mv1.Scalar1 * mv2.Scalar146 + mv1.Scalar2 * mv2.Scalar246 + mv1.Scalar3 * mv2.Scalar346 - mv1.Scalar5 * mv2.Scalar456,
            Scalar56 = -mv1.Scalar1 * mv2.Scalar156 + mv1.Scalar2 * mv2.Scalar256 + mv1.Scalar3 * mv2.Scalar356 + mv1.Scalar4 * mv2.Scalar456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector3 Lcp(this Ga51KVector1 mv1, Ga51KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector3.Zero;
        
        return new Ga51KVector3
        {
            Scalar123 = -mv1.Scalar4 * mv2.Scalar1234 - mv1.Scalar5 * mv2.Scalar1235 - mv1.Scalar6 * mv2.Scalar1236,
            Scalar124 = mv1.Scalar3 * mv2.Scalar1234 - mv1.Scalar5 * mv2.Scalar1245 - mv1.Scalar6 * mv2.Scalar1246,
            Scalar134 = -mv1.Scalar2 * mv2.Scalar1234 - mv1.Scalar5 * mv2.Scalar1345 - mv1.Scalar6 * mv2.Scalar1346,
            Scalar234 = -mv1.Scalar1 * mv2.Scalar1234 - mv1.Scalar5 * mv2.Scalar2345 - mv1.Scalar6 * mv2.Scalar2346,
            Scalar125 = mv1.Scalar3 * mv2.Scalar1235 + mv1.Scalar4 * mv2.Scalar1245 - mv1.Scalar6 * mv2.Scalar1256,
            Scalar135 = -mv1.Scalar2 * mv2.Scalar1235 + mv1.Scalar4 * mv2.Scalar1345 - mv1.Scalar6 * mv2.Scalar1356,
            Scalar235 = -mv1.Scalar1 * mv2.Scalar1235 + mv1.Scalar4 * mv2.Scalar2345 - mv1.Scalar6 * mv2.Scalar2356,
            Scalar145 = -mv1.Scalar2 * mv2.Scalar1245 - mv1.Scalar3 * mv2.Scalar1345 - mv1.Scalar6 * mv2.Scalar1456,
            Scalar245 = -mv1.Scalar1 * mv2.Scalar1245 - mv1.Scalar3 * mv2.Scalar2345 - mv1.Scalar6 * mv2.Scalar2456,
            Scalar345 = -mv1.Scalar1 * mv2.Scalar1345 + mv1.Scalar2 * mv2.Scalar2345 - mv1.Scalar6 * mv2.Scalar3456,
            Scalar126 = mv1.Scalar3 * mv2.Scalar1236 + mv1.Scalar4 * mv2.Scalar1246 + mv1.Scalar5 * mv2.Scalar1256,
            Scalar136 = -mv1.Scalar2 * mv2.Scalar1236 + mv1.Scalar4 * mv2.Scalar1346 + mv1.Scalar5 * mv2.Scalar1356,
            Scalar236 = -mv1.Scalar1 * mv2.Scalar1236 + mv1.Scalar4 * mv2.Scalar2346 + mv1.Scalar5 * mv2.Scalar2356,
            Scalar146 = -mv1.Scalar2 * mv2.Scalar1246 - mv1.Scalar3 * mv2.Scalar1346 + mv1.Scalar5 * mv2.Scalar1456,
            Scalar246 = -mv1.Scalar1 * mv2.Scalar1246 - mv1.Scalar3 * mv2.Scalar2346 + mv1.Scalar5 * mv2.Scalar2456,
            Scalar346 = -mv1.Scalar1 * mv2.Scalar1346 + mv1.Scalar2 * mv2.Scalar2346 + mv1.Scalar5 * mv2.Scalar3456,
            Scalar156 = -mv1.Scalar2 * mv2.Scalar1256 - mv1.Scalar3 * mv2.Scalar1356 - mv1.Scalar4 * mv2.Scalar1456,
            Scalar256 = -mv1.Scalar1 * mv2.Scalar1256 - mv1.Scalar3 * mv2.Scalar2356 - mv1.Scalar4 * mv2.Scalar2456,
            Scalar356 = -mv1.Scalar1 * mv2.Scalar1356 + mv1.Scalar2 * mv2.Scalar2356 - mv1.Scalar4 * mv2.Scalar3456,
            Scalar456 = -mv1.Scalar1 * mv2.Scalar1456 + mv1.Scalar2 * mv2.Scalar2456 + mv1.Scalar3 * mv2.Scalar3456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector4 Lcp(this Ga51KVector1 mv1, Ga51KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector4.Zero;
        
        return new Ga51KVector4
        {
            Scalar1234 = mv1.Scalar5 * mv2.Scalar12345 + mv1.Scalar6 * mv2.Scalar12346,
            Scalar1235 = -mv1.Scalar4 * mv2.Scalar12345 + mv1.Scalar6 * mv2.Scalar12356,
            Scalar1245 = mv1.Scalar3 * mv2.Scalar12345 + mv1.Scalar6 * mv2.Scalar12456,
            Scalar1345 = -mv1.Scalar2 * mv2.Scalar12345 + mv1.Scalar6 * mv2.Scalar13456,
            Scalar2345 = -mv1.Scalar1 * mv2.Scalar12345 + mv1.Scalar6 * mv2.Scalar23456,
            Scalar1236 = -mv1.Scalar4 * mv2.Scalar12346 - mv1.Scalar5 * mv2.Scalar12356,
            Scalar1246 = mv1.Scalar3 * mv2.Scalar12346 - mv1.Scalar5 * mv2.Scalar12456,
            Scalar1346 = -mv1.Scalar2 * mv2.Scalar12346 - mv1.Scalar5 * mv2.Scalar13456,
            Scalar2346 = -mv1.Scalar1 * mv2.Scalar12346 - mv1.Scalar5 * mv2.Scalar23456,
            Scalar1256 = mv1.Scalar3 * mv2.Scalar12356 + mv1.Scalar4 * mv2.Scalar12456,
            Scalar1356 = -mv1.Scalar2 * mv2.Scalar12356 + mv1.Scalar4 * mv2.Scalar13456,
            Scalar2356 = -mv1.Scalar1 * mv2.Scalar12356 + mv1.Scalar4 * mv2.Scalar23456,
            Scalar1456 = -mv1.Scalar2 * mv2.Scalar12456 - mv1.Scalar3 * mv2.Scalar13456,
            Scalar2456 = -mv1.Scalar1 * mv2.Scalar12456 - mv1.Scalar3 * mv2.Scalar23456,
            Scalar3456 = -mv1.Scalar1 * mv2.Scalar13456 + mv1.Scalar2 * mv2.Scalar23456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector5 Lcp(this Ga51KVector1 mv1, Ga51KVector6 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector5.Zero;
        
        return new Ga51KVector5
        {
            Scalar12345 = -mv1.Scalar6 * mv2.Scalar123456,
            Scalar12346 = mv1.Scalar5 * mv2.Scalar123456,
            Scalar12356 = -mv1.Scalar4 * mv2.Scalar123456,
            Scalar12456 = mv1.Scalar3 * mv2.Scalar123456,
            Scalar13456 = -mv1.Scalar2 * mv2.Scalar123456,
            Scalar23456 = -mv1.Scalar1 * mv2.Scalar123456
        };
    }
    
    public static Ga51Multivector Lcp(this Ga51KVector1 mv1, Ga51Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[0] += -mv1.Scalar1 * mv2.KVector1.Scalar1 + mv1.Scalar2 * mv2.KVector1.Scalar2 + mv1.Scalar3 * mv2.KVector1.Scalar3 + mv1.Scalar4 * mv2.KVector1.Scalar4 + mv1.Scalar5 * mv2.KVector1.Scalar5 + mv1.Scalar6 * mv2.KVector1.Scalar6;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[1] += -mv1.Scalar2 * mv2.KVector2.Scalar12 - mv1.Scalar3 * mv2.KVector2.Scalar13 - mv1.Scalar4 * mv2.KVector2.Scalar14 - mv1.Scalar5 * mv2.KVector2.Scalar15 - mv1.Scalar6 * mv2.KVector2.Scalar16;
            tempScalar[2] += -mv1.Scalar1 * mv2.KVector2.Scalar12 - mv1.Scalar3 * mv2.KVector2.Scalar23 - mv1.Scalar4 * mv2.KVector2.Scalar24 - mv1.Scalar5 * mv2.KVector2.Scalar25 - mv1.Scalar6 * mv2.KVector2.Scalar26;
            tempScalar[4] += -mv1.Scalar1 * mv2.KVector2.Scalar13 + mv1.Scalar2 * mv2.KVector2.Scalar23 - mv1.Scalar4 * mv2.KVector2.Scalar34 - mv1.Scalar5 * mv2.KVector2.Scalar35 - mv1.Scalar6 * mv2.KVector2.Scalar36;
            tempScalar[8] += -mv1.Scalar1 * mv2.KVector2.Scalar14 + mv1.Scalar2 * mv2.KVector2.Scalar24 + mv1.Scalar3 * mv2.KVector2.Scalar34 - mv1.Scalar5 * mv2.KVector2.Scalar45 - mv1.Scalar6 * mv2.KVector2.Scalar46;
            tempScalar[16] += -mv1.Scalar1 * mv2.KVector2.Scalar15 + mv1.Scalar2 * mv2.KVector2.Scalar25 + mv1.Scalar3 * mv2.KVector2.Scalar35 + mv1.Scalar4 * mv2.KVector2.Scalar45 - mv1.Scalar6 * mv2.KVector2.Scalar56;
            tempScalar[32] += -mv1.Scalar1 * mv2.KVector2.Scalar16 + mv1.Scalar2 * mv2.KVector2.Scalar26 + mv1.Scalar3 * mv2.KVector2.Scalar36 + mv1.Scalar4 * mv2.KVector2.Scalar46 + mv1.Scalar5 * mv2.KVector2.Scalar56;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[3] += mv1.Scalar3 * mv2.KVector3.Scalar123 + mv1.Scalar4 * mv2.KVector3.Scalar124 + mv1.Scalar5 * mv2.KVector3.Scalar125 + mv1.Scalar6 * mv2.KVector3.Scalar126;
            tempScalar[5] += -mv1.Scalar2 * mv2.KVector3.Scalar123 + mv1.Scalar4 * mv2.KVector3.Scalar134 + mv1.Scalar5 * mv2.KVector3.Scalar135 + mv1.Scalar6 * mv2.KVector3.Scalar136;
            tempScalar[6] += -mv1.Scalar1 * mv2.KVector3.Scalar123 + mv1.Scalar4 * mv2.KVector3.Scalar234 + mv1.Scalar5 * mv2.KVector3.Scalar235 + mv1.Scalar6 * mv2.KVector3.Scalar236;
            tempScalar[9] += -mv1.Scalar2 * mv2.KVector3.Scalar124 - mv1.Scalar3 * mv2.KVector3.Scalar134 + mv1.Scalar5 * mv2.KVector3.Scalar145 + mv1.Scalar6 * mv2.KVector3.Scalar146;
            tempScalar[10] += -mv1.Scalar1 * mv2.KVector3.Scalar124 - mv1.Scalar3 * mv2.KVector3.Scalar234 + mv1.Scalar5 * mv2.KVector3.Scalar245 + mv1.Scalar6 * mv2.KVector3.Scalar246;
            tempScalar[12] += -mv1.Scalar1 * mv2.KVector3.Scalar134 + mv1.Scalar2 * mv2.KVector3.Scalar234 + mv1.Scalar5 * mv2.KVector3.Scalar345 + mv1.Scalar6 * mv2.KVector3.Scalar346;
            tempScalar[17] += -mv1.Scalar2 * mv2.KVector3.Scalar125 - mv1.Scalar3 * mv2.KVector3.Scalar135 - mv1.Scalar4 * mv2.KVector3.Scalar145 + mv1.Scalar6 * mv2.KVector3.Scalar156;
            tempScalar[18] += -mv1.Scalar1 * mv2.KVector3.Scalar125 - mv1.Scalar3 * mv2.KVector3.Scalar235 - mv1.Scalar4 * mv2.KVector3.Scalar245 + mv1.Scalar6 * mv2.KVector3.Scalar256;
            tempScalar[20] += -mv1.Scalar1 * mv2.KVector3.Scalar135 + mv1.Scalar2 * mv2.KVector3.Scalar235 - mv1.Scalar4 * mv2.KVector3.Scalar345 + mv1.Scalar6 * mv2.KVector3.Scalar356;
            tempScalar[24] += -mv1.Scalar1 * mv2.KVector3.Scalar145 + mv1.Scalar2 * mv2.KVector3.Scalar245 + mv1.Scalar3 * mv2.KVector3.Scalar345 + mv1.Scalar6 * mv2.KVector3.Scalar456;
            tempScalar[33] += -mv1.Scalar2 * mv2.KVector3.Scalar126 - mv1.Scalar3 * mv2.KVector3.Scalar136 - mv1.Scalar4 * mv2.KVector3.Scalar146 - mv1.Scalar5 * mv2.KVector3.Scalar156;
            tempScalar[34] += -mv1.Scalar1 * mv2.KVector3.Scalar126 - mv1.Scalar3 * mv2.KVector3.Scalar236 - mv1.Scalar4 * mv2.KVector3.Scalar246 - mv1.Scalar5 * mv2.KVector3.Scalar256;
            tempScalar[36] += -mv1.Scalar1 * mv2.KVector3.Scalar136 + mv1.Scalar2 * mv2.KVector3.Scalar236 - mv1.Scalar4 * mv2.KVector3.Scalar346 - mv1.Scalar5 * mv2.KVector3.Scalar356;
            tempScalar[40] += -mv1.Scalar1 * mv2.KVector3.Scalar146 + mv1.Scalar2 * mv2.KVector3.Scalar246 + mv1.Scalar3 * mv2.KVector3.Scalar346 - mv1.Scalar5 * mv2.KVector3.Scalar456;
            tempScalar[48] += -mv1.Scalar1 * mv2.KVector3.Scalar156 + mv1.Scalar2 * mv2.KVector3.Scalar256 + mv1.Scalar3 * mv2.KVector3.Scalar356 + mv1.Scalar4 * mv2.KVector3.Scalar456;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[7] += -mv1.Scalar4 * mv2.KVector4.Scalar1234 - mv1.Scalar5 * mv2.KVector4.Scalar1235 - mv1.Scalar6 * mv2.KVector4.Scalar1236;
            tempScalar[11] += mv1.Scalar3 * mv2.KVector4.Scalar1234 - mv1.Scalar5 * mv2.KVector4.Scalar1245 - mv1.Scalar6 * mv2.KVector4.Scalar1246;
            tempScalar[13] += -mv1.Scalar2 * mv2.KVector4.Scalar1234 - mv1.Scalar5 * mv2.KVector4.Scalar1345 - mv1.Scalar6 * mv2.KVector4.Scalar1346;
            tempScalar[14] += -mv1.Scalar1 * mv2.KVector4.Scalar1234 - mv1.Scalar5 * mv2.KVector4.Scalar2345 - mv1.Scalar6 * mv2.KVector4.Scalar2346;
            tempScalar[19] += mv1.Scalar3 * mv2.KVector4.Scalar1235 + mv1.Scalar4 * mv2.KVector4.Scalar1245 - mv1.Scalar6 * mv2.KVector4.Scalar1256;
            tempScalar[21] += -mv1.Scalar2 * mv2.KVector4.Scalar1235 + mv1.Scalar4 * mv2.KVector4.Scalar1345 - mv1.Scalar6 * mv2.KVector4.Scalar1356;
            tempScalar[22] += -mv1.Scalar1 * mv2.KVector4.Scalar1235 + mv1.Scalar4 * mv2.KVector4.Scalar2345 - mv1.Scalar6 * mv2.KVector4.Scalar2356;
            tempScalar[25] += -mv1.Scalar2 * mv2.KVector4.Scalar1245 - mv1.Scalar3 * mv2.KVector4.Scalar1345 - mv1.Scalar6 * mv2.KVector4.Scalar1456;
            tempScalar[26] += -mv1.Scalar1 * mv2.KVector4.Scalar1245 - mv1.Scalar3 * mv2.KVector4.Scalar2345 - mv1.Scalar6 * mv2.KVector4.Scalar2456;
            tempScalar[28] += -mv1.Scalar1 * mv2.KVector4.Scalar1345 + mv1.Scalar2 * mv2.KVector4.Scalar2345 - mv1.Scalar6 * mv2.KVector4.Scalar3456;
            tempScalar[35] += mv1.Scalar3 * mv2.KVector4.Scalar1236 + mv1.Scalar4 * mv2.KVector4.Scalar1246 + mv1.Scalar5 * mv2.KVector4.Scalar1256;
            tempScalar[37] += -mv1.Scalar2 * mv2.KVector4.Scalar1236 + mv1.Scalar4 * mv2.KVector4.Scalar1346 + mv1.Scalar5 * mv2.KVector4.Scalar1356;
            tempScalar[38] += -mv1.Scalar1 * mv2.KVector4.Scalar1236 + mv1.Scalar4 * mv2.KVector4.Scalar2346 + mv1.Scalar5 * mv2.KVector4.Scalar2356;
            tempScalar[41] += -mv1.Scalar2 * mv2.KVector4.Scalar1246 - mv1.Scalar3 * mv2.KVector4.Scalar1346 + mv1.Scalar5 * mv2.KVector4.Scalar1456;
            tempScalar[42] += -mv1.Scalar1 * mv2.KVector4.Scalar1246 - mv1.Scalar3 * mv2.KVector4.Scalar2346 + mv1.Scalar5 * mv2.KVector4.Scalar2456;
            tempScalar[44] += -mv1.Scalar1 * mv2.KVector4.Scalar1346 + mv1.Scalar2 * mv2.KVector4.Scalar2346 + mv1.Scalar5 * mv2.KVector4.Scalar3456;
            tempScalar[49] += -mv1.Scalar2 * mv2.KVector4.Scalar1256 - mv1.Scalar3 * mv2.KVector4.Scalar1356 - mv1.Scalar4 * mv2.KVector4.Scalar1456;
            tempScalar[50] += -mv1.Scalar1 * mv2.KVector4.Scalar1256 - mv1.Scalar3 * mv2.KVector4.Scalar2356 - mv1.Scalar4 * mv2.KVector4.Scalar2456;
            tempScalar[52] += -mv1.Scalar1 * mv2.KVector4.Scalar1356 + mv1.Scalar2 * mv2.KVector4.Scalar2356 - mv1.Scalar4 * mv2.KVector4.Scalar3456;
            tempScalar[56] += -mv1.Scalar1 * mv2.KVector4.Scalar1456 + mv1.Scalar2 * mv2.KVector4.Scalar2456 + mv1.Scalar3 * mv2.KVector4.Scalar3456;
        }
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[15] += mv1.Scalar5 * mv2.KVector5.Scalar12345 + mv1.Scalar6 * mv2.KVector5.Scalar12346;
            tempScalar[23] += -mv1.Scalar4 * mv2.KVector5.Scalar12345 + mv1.Scalar6 * mv2.KVector5.Scalar12356;
            tempScalar[27] += mv1.Scalar3 * mv2.KVector5.Scalar12345 + mv1.Scalar6 * mv2.KVector5.Scalar12456;
            tempScalar[29] += -mv1.Scalar2 * mv2.KVector5.Scalar12345 + mv1.Scalar6 * mv2.KVector5.Scalar13456;
            tempScalar[30] += -mv1.Scalar1 * mv2.KVector5.Scalar12345 + mv1.Scalar6 * mv2.KVector5.Scalar23456;
            tempScalar[39] += -mv1.Scalar4 * mv2.KVector5.Scalar12346 - mv1.Scalar5 * mv2.KVector5.Scalar12356;
            tempScalar[43] += mv1.Scalar3 * mv2.KVector5.Scalar12346 - mv1.Scalar5 * mv2.KVector5.Scalar12456;
            tempScalar[45] += -mv1.Scalar2 * mv2.KVector5.Scalar12346 - mv1.Scalar5 * mv2.KVector5.Scalar13456;
            tempScalar[46] += -mv1.Scalar1 * mv2.KVector5.Scalar12346 - mv1.Scalar5 * mv2.KVector5.Scalar23456;
            tempScalar[51] += mv1.Scalar3 * mv2.KVector5.Scalar12356 + mv1.Scalar4 * mv2.KVector5.Scalar12456;
            tempScalar[53] += -mv1.Scalar2 * mv2.KVector5.Scalar12356 + mv1.Scalar4 * mv2.KVector5.Scalar13456;
            tempScalar[54] += -mv1.Scalar1 * mv2.KVector5.Scalar12356 + mv1.Scalar4 * mv2.KVector5.Scalar23456;
            tempScalar[57] += -mv1.Scalar2 * mv2.KVector5.Scalar12456 - mv1.Scalar3 * mv2.KVector5.Scalar13456;
            tempScalar[58] += -mv1.Scalar1 * mv2.KVector5.Scalar12456 - mv1.Scalar3 * mv2.KVector5.Scalar23456;
            tempScalar[60] += -mv1.Scalar1 * mv2.KVector5.Scalar13456 + mv1.Scalar2 * mv2.KVector5.Scalar23456;
        }
        
        if (!mv2.KVector6.IsZero())
        {
            tempScalar[31] += -mv1.Scalar6 * mv2.KVector6.Scalar123456;
            tempScalar[47] += mv1.Scalar5 * mv2.KVector6.Scalar123456;
            tempScalar[55] += -mv1.Scalar4 * mv2.KVector6.Scalar123456;
            tempScalar[59] += mv1.Scalar3 * mv2.KVector6.Scalar123456;
            tempScalar[61] += -mv1.Scalar2 * mv2.KVector6.Scalar123456;
            tempScalar[62] += -mv1.Scalar1 * mv2.KVector6.Scalar123456;
        }
        
        return Ga51Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector2 mv1, Ga51KVector0 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector2 mv1, Ga51KVector1 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector2 mv1, Ga51KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector0.Zero;
        
        return new Ga51KVector0
        {
            Scalar = mv1.Scalar12 * mv2.Scalar12 + mv1.Scalar13 * mv2.Scalar13 - mv1.Scalar23 * mv2.Scalar23 + mv1.Scalar14 * mv2.Scalar14 - mv1.Scalar24 * mv2.Scalar24 - mv1.Scalar34 * mv2.Scalar34 + mv1.Scalar15 * mv2.Scalar15 - mv1.Scalar25 * mv2.Scalar25 - mv1.Scalar35 * mv2.Scalar35 - mv1.Scalar45 * mv2.Scalar45 + mv1.Scalar16 * mv2.Scalar16 - mv1.Scalar26 * mv2.Scalar26 - mv1.Scalar36 * mv2.Scalar36 - mv1.Scalar46 * mv2.Scalar46 - mv1.Scalar56 * mv2.Scalar56
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector1 Lcp(this Ga51KVector2 mv1, Ga51KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector1.Zero;
        
        return new Ga51KVector1
        {
            Scalar1 = -mv1.Scalar23 * mv2.Scalar123 - mv1.Scalar24 * mv2.Scalar124 - mv1.Scalar34 * mv2.Scalar134 - mv1.Scalar25 * mv2.Scalar125 - mv1.Scalar35 * mv2.Scalar135 - mv1.Scalar45 * mv2.Scalar145 - mv1.Scalar26 * mv2.Scalar126 - mv1.Scalar36 * mv2.Scalar136 - mv1.Scalar46 * mv2.Scalar146 - mv1.Scalar56 * mv2.Scalar156,
            Scalar2 = -mv1.Scalar13 * mv2.Scalar123 - mv1.Scalar14 * mv2.Scalar124 - mv1.Scalar34 * mv2.Scalar234 - mv1.Scalar15 * mv2.Scalar125 - mv1.Scalar35 * mv2.Scalar235 - mv1.Scalar45 * mv2.Scalar245 - mv1.Scalar16 * mv2.Scalar126 - mv1.Scalar36 * mv2.Scalar236 - mv1.Scalar46 * mv2.Scalar246 - mv1.Scalar56 * mv2.Scalar256,
            Scalar3 = mv1.Scalar12 * mv2.Scalar123 - mv1.Scalar14 * mv2.Scalar134 + mv1.Scalar24 * mv2.Scalar234 - mv1.Scalar15 * mv2.Scalar135 + mv1.Scalar25 * mv2.Scalar235 - mv1.Scalar45 * mv2.Scalar345 - mv1.Scalar16 * mv2.Scalar136 + mv1.Scalar26 * mv2.Scalar236 - mv1.Scalar46 * mv2.Scalar346 - mv1.Scalar56 * mv2.Scalar356,
            Scalar4 = mv1.Scalar12 * mv2.Scalar124 + mv1.Scalar13 * mv2.Scalar134 - mv1.Scalar23 * mv2.Scalar234 - mv1.Scalar15 * mv2.Scalar145 + mv1.Scalar25 * mv2.Scalar245 + mv1.Scalar35 * mv2.Scalar345 - mv1.Scalar16 * mv2.Scalar146 + mv1.Scalar26 * mv2.Scalar246 + mv1.Scalar36 * mv2.Scalar346 - mv1.Scalar56 * mv2.Scalar456,
            Scalar5 = mv1.Scalar12 * mv2.Scalar125 + mv1.Scalar13 * mv2.Scalar135 - mv1.Scalar23 * mv2.Scalar235 + mv1.Scalar14 * mv2.Scalar145 - mv1.Scalar24 * mv2.Scalar245 - mv1.Scalar34 * mv2.Scalar345 - mv1.Scalar16 * mv2.Scalar156 + mv1.Scalar26 * mv2.Scalar256 + mv1.Scalar36 * mv2.Scalar356 + mv1.Scalar46 * mv2.Scalar456,
            Scalar6 = mv1.Scalar12 * mv2.Scalar126 + mv1.Scalar13 * mv2.Scalar136 - mv1.Scalar23 * mv2.Scalar236 + mv1.Scalar14 * mv2.Scalar146 - mv1.Scalar24 * mv2.Scalar246 - mv1.Scalar34 * mv2.Scalar346 + mv1.Scalar15 * mv2.Scalar156 - mv1.Scalar25 * mv2.Scalar256 - mv1.Scalar35 * mv2.Scalar356 - mv1.Scalar45 * mv2.Scalar456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector2 Lcp(this Ga51KVector2 mv1, Ga51KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector2.Zero;
        
        return new Ga51KVector2
        {
            Scalar12 = -mv1.Scalar34 * mv2.Scalar1234 - mv1.Scalar35 * mv2.Scalar1235 - mv1.Scalar45 * mv2.Scalar1245 - mv1.Scalar36 * mv2.Scalar1236 - mv1.Scalar46 * mv2.Scalar1246 - mv1.Scalar56 * mv2.Scalar1256,
            Scalar13 = mv1.Scalar24 * mv2.Scalar1234 + mv1.Scalar25 * mv2.Scalar1235 - mv1.Scalar45 * mv2.Scalar1345 + mv1.Scalar26 * mv2.Scalar1236 - mv1.Scalar46 * mv2.Scalar1346 - mv1.Scalar56 * mv2.Scalar1356,
            Scalar23 = mv1.Scalar14 * mv2.Scalar1234 + mv1.Scalar15 * mv2.Scalar1235 - mv1.Scalar45 * mv2.Scalar2345 + mv1.Scalar16 * mv2.Scalar1236 - mv1.Scalar46 * mv2.Scalar2346 - mv1.Scalar56 * mv2.Scalar2356,
            Scalar14 = -mv1.Scalar23 * mv2.Scalar1234 + mv1.Scalar25 * mv2.Scalar1245 + mv1.Scalar35 * mv2.Scalar1345 + mv1.Scalar26 * mv2.Scalar1246 + mv1.Scalar36 * mv2.Scalar1346 - mv1.Scalar56 * mv2.Scalar1456,
            Scalar24 = -mv1.Scalar13 * mv2.Scalar1234 + mv1.Scalar15 * mv2.Scalar1245 + mv1.Scalar35 * mv2.Scalar2345 + mv1.Scalar16 * mv2.Scalar1246 + mv1.Scalar36 * mv2.Scalar2346 - mv1.Scalar56 * mv2.Scalar2456,
            Scalar34 = mv1.Scalar12 * mv2.Scalar1234 + mv1.Scalar15 * mv2.Scalar1345 - mv1.Scalar25 * mv2.Scalar2345 + mv1.Scalar16 * mv2.Scalar1346 - mv1.Scalar26 * mv2.Scalar2346 - mv1.Scalar56 * mv2.Scalar3456,
            Scalar15 = -mv1.Scalar23 * mv2.Scalar1235 - mv1.Scalar24 * mv2.Scalar1245 - mv1.Scalar34 * mv2.Scalar1345 + mv1.Scalar26 * mv2.Scalar1256 + mv1.Scalar36 * mv2.Scalar1356 + mv1.Scalar46 * mv2.Scalar1456,
            Scalar25 = -mv1.Scalar13 * mv2.Scalar1235 - mv1.Scalar14 * mv2.Scalar1245 - mv1.Scalar34 * mv2.Scalar2345 + mv1.Scalar16 * mv2.Scalar1256 + mv1.Scalar36 * mv2.Scalar2356 + mv1.Scalar46 * mv2.Scalar2456,
            Scalar35 = mv1.Scalar12 * mv2.Scalar1235 - mv1.Scalar14 * mv2.Scalar1345 + mv1.Scalar24 * mv2.Scalar2345 + mv1.Scalar16 * mv2.Scalar1356 - mv1.Scalar26 * mv2.Scalar2356 + mv1.Scalar46 * mv2.Scalar3456,
            Scalar45 = mv1.Scalar12 * mv2.Scalar1245 + mv1.Scalar13 * mv2.Scalar1345 - mv1.Scalar23 * mv2.Scalar2345 + mv1.Scalar16 * mv2.Scalar1456 - mv1.Scalar26 * mv2.Scalar2456 - mv1.Scalar36 * mv2.Scalar3456,
            Scalar16 = -mv1.Scalar23 * mv2.Scalar1236 - mv1.Scalar24 * mv2.Scalar1246 - mv1.Scalar34 * mv2.Scalar1346 - mv1.Scalar25 * mv2.Scalar1256 - mv1.Scalar35 * mv2.Scalar1356 - mv1.Scalar45 * mv2.Scalar1456,
            Scalar26 = -mv1.Scalar13 * mv2.Scalar1236 - mv1.Scalar14 * mv2.Scalar1246 - mv1.Scalar34 * mv2.Scalar2346 - mv1.Scalar15 * mv2.Scalar1256 - mv1.Scalar35 * mv2.Scalar2356 - mv1.Scalar45 * mv2.Scalar2456,
            Scalar36 = mv1.Scalar12 * mv2.Scalar1236 - mv1.Scalar14 * mv2.Scalar1346 + mv1.Scalar24 * mv2.Scalar2346 - mv1.Scalar15 * mv2.Scalar1356 + mv1.Scalar25 * mv2.Scalar2356 - mv1.Scalar45 * mv2.Scalar3456,
            Scalar46 = mv1.Scalar12 * mv2.Scalar1246 + mv1.Scalar13 * mv2.Scalar1346 - mv1.Scalar23 * mv2.Scalar2346 - mv1.Scalar15 * mv2.Scalar1456 + mv1.Scalar25 * mv2.Scalar2456 + mv1.Scalar35 * mv2.Scalar3456,
            Scalar56 = mv1.Scalar12 * mv2.Scalar1256 + mv1.Scalar13 * mv2.Scalar1356 - mv1.Scalar23 * mv2.Scalar2356 + mv1.Scalar14 * mv2.Scalar1456 - mv1.Scalar24 * mv2.Scalar2456 - mv1.Scalar34 * mv2.Scalar3456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector3 Lcp(this Ga51KVector2 mv1, Ga51KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector3.Zero;
        
        return new Ga51KVector3
        {
            Scalar123 = -mv1.Scalar45 * mv2.Scalar12345 - mv1.Scalar46 * mv2.Scalar12346 - mv1.Scalar56 * mv2.Scalar12356,
            Scalar124 = mv1.Scalar35 * mv2.Scalar12345 + mv1.Scalar36 * mv2.Scalar12346 - mv1.Scalar56 * mv2.Scalar12456,
            Scalar134 = -mv1.Scalar25 * mv2.Scalar12345 - mv1.Scalar26 * mv2.Scalar12346 - mv1.Scalar56 * mv2.Scalar13456,
            Scalar234 = -mv1.Scalar15 * mv2.Scalar12345 - mv1.Scalar16 * mv2.Scalar12346 - mv1.Scalar56 * mv2.Scalar23456,
            Scalar125 = -mv1.Scalar34 * mv2.Scalar12345 + mv1.Scalar36 * mv2.Scalar12356 + mv1.Scalar46 * mv2.Scalar12456,
            Scalar135 = mv1.Scalar24 * mv2.Scalar12345 - mv1.Scalar26 * mv2.Scalar12356 + mv1.Scalar46 * mv2.Scalar13456,
            Scalar235 = mv1.Scalar14 * mv2.Scalar12345 - mv1.Scalar16 * mv2.Scalar12356 + mv1.Scalar46 * mv2.Scalar23456,
            Scalar145 = -mv1.Scalar23 * mv2.Scalar12345 - mv1.Scalar26 * mv2.Scalar12456 - mv1.Scalar36 * mv2.Scalar13456,
            Scalar245 = -mv1.Scalar13 * mv2.Scalar12345 - mv1.Scalar16 * mv2.Scalar12456 - mv1.Scalar36 * mv2.Scalar23456,
            Scalar345 = mv1.Scalar12 * mv2.Scalar12345 - mv1.Scalar16 * mv2.Scalar13456 + mv1.Scalar26 * mv2.Scalar23456,
            Scalar126 = -mv1.Scalar34 * mv2.Scalar12346 - mv1.Scalar35 * mv2.Scalar12356 - mv1.Scalar45 * mv2.Scalar12456,
            Scalar136 = mv1.Scalar24 * mv2.Scalar12346 + mv1.Scalar25 * mv2.Scalar12356 - mv1.Scalar45 * mv2.Scalar13456,
            Scalar236 = mv1.Scalar14 * mv2.Scalar12346 + mv1.Scalar15 * mv2.Scalar12356 - mv1.Scalar45 * mv2.Scalar23456,
            Scalar146 = -mv1.Scalar23 * mv2.Scalar12346 + mv1.Scalar25 * mv2.Scalar12456 + mv1.Scalar35 * mv2.Scalar13456,
            Scalar246 = -mv1.Scalar13 * mv2.Scalar12346 + mv1.Scalar15 * mv2.Scalar12456 + mv1.Scalar35 * mv2.Scalar23456,
            Scalar346 = mv1.Scalar12 * mv2.Scalar12346 + mv1.Scalar15 * mv2.Scalar13456 - mv1.Scalar25 * mv2.Scalar23456,
            Scalar156 = -mv1.Scalar23 * mv2.Scalar12356 - mv1.Scalar24 * mv2.Scalar12456 - mv1.Scalar34 * mv2.Scalar13456,
            Scalar256 = -mv1.Scalar13 * mv2.Scalar12356 - mv1.Scalar14 * mv2.Scalar12456 - mv1.Scalar34 * mv2.Scalar23456,
            Scalar356 = mv1.Scalar12 * mv2.Scalar12356 - mv1.Scalar14 * mv2.Scalar13456 + mv1.Scalar24 * mv2.Scalar23456,
            Scalar456 = mv1.Scalar12 * mv2.Scalar12456 + mv1.Scalar13 * mv2.Scalar13456 - mv1.Scalar23 * mv2.Scalar23456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector4 Lcp(this Ga51KVector2 mv1, Ga51KVector6 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector4.Zero;
        
        return new Ga51KVector4
        {
            Scalar1234 = -mv1.Scalar56 * mv2.Scalar123456,
            Scalar1235 = mv1.Scalar46 * mv2.Scalar123456,
            Scalar1245 = -mv1.Scalar36 * mv2.Scalar123456,
            Scalar1345 = mv1.Scalar26 * mv2.Scalar123456,
            Scalar2345 = mv1.Scalar16 * mv2.Scalar123456,
            Scalar1236 = -mv1.Scalar45 * mv2.Scalar123456,
            Scalar1246 = mv1.Scalar35 * mv2.Scalar123456,
            Scalar1346 = -mv1.Scalar25 * mv2.Scalar123456,
            Scalar2346 = -mv1.Scalar15 * mv2.Scalar123456,
            Scalar1256 = -mv1.Scalar34 * mv2.Scalar123456,
            Scalar1356 = mv1.Scalar24 * mv2.Scalar123456,
            Scalar2356 = mv1.Scalar14 * mv2.Scalar123456,
            Scalar1456 = -mv1.Scalar23 * mv2.Scalar123456,
            Scalar2456 = -mv1.Scalar13 * mv2.Scalar123456,
            Scalar3456 = mv1.Scalar12 * mv2.Scalar123456
        };
    }
    
    public static Ga51Multivector Lcp(this Ga51KVector2 mv1, Ga51Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[0] += mv1.Scalar12 * mv2.KVector2.Scalar12 + mv1.Scalar13 * mv2.KVector2.Scalar13 - mv1.Scalar23 * mv2.KVector2.Scalar23 + mv1.Scalar14 * mv2.KVector2.Scalar14 - mv1.Scalar24 * mv2.KVector2.Scalar24 - mv1.Scalar34 * mv2.KVector2.Scalar34 + mv1.Scalar15 * mv2.KVector2.Scalar15 - mv1.Scalar25 * mv2.KVector2.Scalar25 - mv1.Scalar35 * mv2.KVector2.Scalar35 - mv1.Scalar45 * mv2.KVector2.Scalar45 + mv1.Scalar16 * mv2.KVector2.Scalar16 - mv1.Scalar26 * mv2.KVector2.Scalar26 - mv1.Scalar36 * mv2.KVector2.Scalar36 - mv1.Scalar46 * mv2.KVector2.Scalar46 - mv1.Scalar56 * mv2.KVector2.Scalar56;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[1] += -mv1.Scalar23 * mv2.KVector3.Scalar123 - mv1.Scalar24 * mv2.KVector3.Scalar124 - mv1.Scalar34 * mv2.KVector3.Scalar134 - mv1.Scalar25 * mv2.KVector3.Scalar125 - mv1.Scalar35 * mv2.KVector3.Scalar135 - mv1.Scalar45 * mv2.KVector3.Scalar145 - mv1.Scalar26 * mv2.KVector3.Scalar126 - mv1.Scalar36 * mv2.KVector3.Scalar136 - mv1.Scalar46 * mv2.KVector3.Scalar146 - mv1.Scalar56 * mv2.KVector3.Scalar156;
            tempScalar[2] += -mv1.Scalar13 * mv2.KVector3.Scalar123 - mv1.Scalar14 * mv2.KVector3.Scalar124 - mv1.Scalar34 * mv2.KVector3.Scalar234 - mv1.Scalar15 * mv2.KVector3.Scalar125 - mv1.Scalar35 * mv2.KVector3.Scalar235 - mv1.Scalar45 * mv2.KVector3.Scalar245 - mv1.Scalar16 * mv2.KVector3.Scalar126 - mv1.Scalar36 * mv2.KVector3.Scalar236 - mv1.Scalar46 * mv2.KVector3.Scalar246 - mv1.Scalar56 * mv2.KVector3.Scalar256;
            tempScalar[4] += mv1.Scalar12 * mv2.KVector3.Scalar123 - mv1.Scalar14 * mv2.KVector3.Scalar134 + mv1.Scalar24 * mv2.KVector3.Scalar234 - mv1.Scalar15 * mv2.KVector3.Scalar135 + mv1.Scalar25 * mv2.KVector3.Scalar235 - mv1.Scalar45 * mv2.KVector3.Scalar345 - mv1.Scalar16 * mv2.KVector3.Scalar136 + mv1.Scalar26 * mv2.KVector3.Scalar236 - mv1.Scalar46 * mv2.KVector3.Scalar346 - mv1.Scalar56 * mv2.KVector3.Scalar356;
            tempScalar[8] += mv1.Scalar12 * mv2.KVector3.Scalar124 + mv1.Scalar13 * mv2.KVector3.Scalar134 - mv1.Scalar23 * mv2.KVector3.Scalar234 - mv1.Scalar15 * mv2.KVector3.Scalar145 + mv1.Scalar25 * mv2.KVector3.Scalar245 + mv1.Scalar35 * mv2.KVector3.Scalar345 - mv1.Scalar16 * mv2.KVector3.Scalar146 + mv1.Scalar26 * mv2.KVector3.Scalar246 + mv1.Scalar36 * mv2.KVector3.Scalar346 - mv1.Scalar56 * mv2.KVector3.Scalar456;
            tempScalar[16] += mv1.Scalar12 * mv2.KVector3.Scalar125 + mv1.Scalar13 * mv2.KVector3.Scalar135 - mv1.Scalar23 * mv2.KVector3.Scalar235 + mv1.Scalar14 * mv2.KVector3.Scalar145 - mv1.Scalar24 * mv2.KVector3.Scalar245 - mv1.Scalar34 * mv2.KVector3.Scalar345 - mv1.Scalar16 * mv2.KVector3.Scalar156 + mv1.Scalar26 * mv2.KVector3.Scalar256 + mv1.Scalar36 * mv2.KVector3.Scalar356 + mv1.Scalar46 * mv2.KVector3.Scalar456;
            tempScalar[32] += mv1.Scalar12 * mv2.KVector3.Scalar126 + mv1.Scalar13 * mv2.KVector3.Scalar136 - mv1.Scalar23 * mv2.KVector3.Scalar236 + mv1.Scalar14 * mv2.KVector3.Scalar146 - mv1.Scalar24 * mv2.KVector3.Scalar246 - mv1.Scalar34 * mv2.KVector3.Scalar346 + mv1.Scalar15 * mv2.KVector3.Scalar156 - mv1.Scalar25 * mv2.KVector3.Scalar256 - mv1.Scalar35 * mv2.KVector3.Scalar356 - mv1.Scalar45 * mv2.KVector3.Scalar456;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[3] += -mv1.Scalar34 * mv2.KVector4.Scalar1234 - mv1.Scalar35 * mv2.KVector4.Scalar1235 - mv1.Scalar45 * mv2.KVector4.Scalar1245 - mv1.Scalar36 * mv2.KVector4.Scalar1236 - mv1.Scalar46 * mv2.KVector4.Scalar1246 - mv1.Scalar56 * mv2.KVector4.Scalar1256;
            tempScalar[5] += mv1.Scalar24 * mv2.KVector4.Scalar1234 + mv1.Scalar25 * mv2.KVector4.Scalar1235 - mv1.Scalar45 * mv2.KVector4.Scalar1345 + mv1.Scalar26 * mv2.KVector4.Scalar1236 - mv1.Scalar46 * mv2.KVector4.Scalar1346 - mv1.Scalar56 * mv2.KVector4.Scalar1356;
            tempScalar[6] += mv1.Scalar14 * mv2.KVector4.Scalar1234 + mv1.Scalar15 * mv2.KVector4.Scalar1235 - mv1.Scalar45 * mv2.KVector4.Scalar2345 + mv1.Scalar16 * mv2.KVector4.Scalar1236 - mv1.Scalar46 * mv2.KVector4.Scalar2346 - mv1.Scalar56 * mv2.KVector4.Scalar2356;
            tempScalar[9] += -mv1.Scalar23 * mv2.KVector4.Scalar1234 + mv1.Scalar25 * mv2.KVector4.Scalar1245 + mv1.Scalar35 * mv2.KVector4.Scalar1345 + mv1.Scalar26 * mv2.KVector4.Scalar1246 + mv1.Scalar36 * mv2.KVector4.Scalar1346 - mv1.Scalar56 * mv2.KVector4.Scalar1456;
            tempScalar[10] += -mv1.Scalar13 * mv2.KVector4.Scalar1234 + mv1.Scalar15 * mv2.KVector4.Scalar1245 + mv1.Scalar35 * mv2.KVector4.Scalar2345 + mv1.Scalar16 * mv2.KVector4.Scalar1246 + mv1.Scalar36 * mv2.KVector4.Scalar2346 - mv1.Scalar56 * mv2.KVector4.Scalar2456;
            tempScalar[12] += mv1.Scalar12 * mv2.KVector4.Scalar1234 + mv1.Scalar15 * mv2.KVector4.Scalar1345 - mv1.Scalar25 * mv2.KVector4.Scalar2345 + mv1.Scalar16 * mv2.KVector4.Scalar1346 - mv1.Scalar26 * mv2.KVector4.Scalar2346 - mv1.Scalar56 * mv2.KVector4.Scalar3456;
            tempScalar[17] += -mv1.Scalar23 * mv2.KVector4.Scalar1235 - mv1.Scalar24 * mv2.KVector4.Scalar1245 - mv1.Scalar34 * mv2.KVector4.Scalar1345 + mv1.Scalar26 * mv2.KVector4.Scalar1256 + mv1.Scalar36 * mv2.KVector4.Scalar1356 + mv1.Scalar46 * mv2.KVector4.Scalar1456;
            tempScalar[18] += -mv1.Scalar13 * mv2.KVector4.Scalar1235 - mv1.Scalar14 * mv2.KVector4.Scalar1245 - mv1.Scalar34 * mv2.KVector4.Scalar2345 + mv1.Scalar16 * mv2.KVector4.Scalar1256 + mv1.Scalar36 * mv2.KVector4.Scalar2356 + mv1.Scalar46 * mv2.KVector4.Scalar2456;
            tempScalar[20] += mv1.Scalar12 * mv2.KVector4.Scalar1235 - mv1.Scalar14 * mv2.KVector4.Scalar1345 + mv1.Scalar24 * mv2.KVector4.Scalar2345 + mv1.Scalar16 * mv2.KVector4.Scalar1356 - mv1.Scalar26 * mv2.KVector4.Scalar2356 + mv1.Scalar46 * mv2.KVector4.Scalar3456;
            tempScalar[24] += mv1.Scalar12 * mv2.KVector4.Scalar1245 + mv1.Scalar13 * mv2.KVector4.Scalar1345 - mv1.Scalar23 * mv2.KVector4.Scalar2345 + mv1.Scalar16 * mv2.KVector4.Scalar1456 - mv1.Scalar26 * mv2.KVector4.Scalar2456 - mv1.Scalar36 * mv2.KVector4.Scalar3456;
            tempScalar[33] += -mv1.Scalar23 * mv2.KVector4.Scalar1236 - mv1.Scalar24 * mv2.KVector4.Scalar1246 - mv1.Scalar34 * mv2.KVector4.Scalar1346 - mv1.Scalar25 * mv2.KVector4.Scalar1256 - mv1.Scalar35 * mv2.KVector4.Scalar1356 - mv1.Scalar45 * mv2.KVector4.Scalar1456;
            tempScalar[34] += -mv1.Scalar13 * mv2.KVector4.Scalar1236 - mv1.Scalar14 * mv2.KVector4.Scalar1246 - mv1.Scalar34 * mv2.KVector4.Scalar2346 - mv1.Scalar15 * mv2.KVector4.Scalar1256 - mv1.Scalar35 * mv2.KVector4.Scalar2356 - mv1.Scalar45 * mv2.KVector4.Scalar2456;
            tempScalar[36] += mv1.Scalar12 * mv2.KVector4.Scalar1236 - mv1.Scalar14 * mv2.KVector4.Scalar1346 + mv1.Scalar24 * mv2.KVector4.Scalar2346 - mv1.Scalar15 * mv2.KVector4.Scalar1356 + mv1.Scalar25 * mv2.KVector4.Scalar2356 - mv1.Scalar45 * mv2.KVector4.Scalar3456;
            tempScalar[40] += mv1.Scalar12 * mv2.KVector4.Scalar1246 + mv1.Scalar13 * mv2.KVector4.Scalar1346 - mv1.Scalar23 * mv2.KVector4.Scalar2346 - mv1.Scalar15 * mv2.KVector4.Scalar1456 + mv1.Scalar25 * mv2.KVector4.Scalar2456 + mv1.Scalar35 * mv2.KVector4.Scalar3456;
            tempScalar[48] += mv1.Scalar12 * mv2.KVector4.Scalar1256 + mv1.Scalar13 * mv2.KVector4.Scalar1356 - mv1.Scalar23 * mv2.KVector4.Scalar2356 + mv1.Scalar14 * mv2.KVector4.Scalar1456 - mv1.Scalar24 * mv2.KVector4.Scalar2456 - mv1.Scalar34 * mv2.KVector4.Scalar3456;
        }
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[7] += -mv1.Scalar45 * mv2.KVector5.Scalar12345 - mv1.Scalar46 * mv2.KVector5.Scalar12346 - mv1.Scalar56 * mv2.KVector5.Scalar12356;
            tempScalar[11] += mv1.Scalar35 * mv2.KVector5.Scalar12345 + mv1.Scalar36 * mv2.KVector5.Scalar12346 - mv1.Scalar56 * mv2.KVector5.Scalar12456;
            tempScalar[13] += -mv1.Scalar25 * mv2.KVector5.Scalar12345 - mv1.Scalar26 * mv2.KVector5.Scalar12346 - mv1.Scalar56 * mv2.KVector5.Scalar13456;
            tempScalar[14] += -mv1.Scalar15 * mv2.KVector5.Scalar12345 - mv1.Scalar16 * mv2.KVector5.Scalar12346 - mv1.Scalar56 * mv2.KVector5.Scalar23456;
            tempScalar[19] += -mv1.Scalar34 * mv2.KVector5.Scalar12345 + mv1.Scalar36 * mv2.KVector5.Scalar12356 + mv1.Scalar46 * mv2.KVector5.Scalar12456;
            tempScalar[21] += mv1.Scalar24 * mv2.KVector5.Scalar12345 - mv1.Scalar26 * mv2.KVector5.Scalar12356 + mv1.Scalar46 * mv2.KVector5.Scalar13456;
            tempScalar[22] += mv1.Scalar14 * mv2.KVector5.Scalar12345 - mv1.Scalar16 * mv2.KVector5.Scalar12356 + mv1.Scalar46 * mv2.KVector5.Scalar23456;
            tempScalar[25] += -mv1.Scalar23 * mv2.KVector5.Scalar12345 - mv1.Scalar26 * mv2.KVector5.Scalar12456 - mv1.Scalar36 * mv2.KVector5.Scalar13456;
            tempScalar[26] += -mv1.Scalar13 * mv2.KVector5.Scalar12345 - mv1.Scalar16 * mv2.KVector5.Scalar12456 - mv1.Scalar36 * mv2.KVector5.Scalar23456;
            tempScalar[28] += mv1.Scalar12 * mv2.KVector5.Scalar12345 - mv1.Scalar16 * mv2.KVector5.Scalar13456 + mv1.Scalar26 * mv2.KVector5.Scalar23456;
            tempScalar[35] += -mv1.Scalar34 * mv2.KVector5.Scalar12346 - mv1.Scalar35 * mv2.KVector5.Scalar12356 - mv1.Scalar45 * mv2.KVector5.Scalar12456;
            tempScalar[37] += mv1.Scalar24 * mv2.KVector5.Scalar12346 + mv1.Scalar25 * mv2.KVector5.Scalar12356 - mv1.Scalar45 * mv2.KVector5.Scalar13456;
            tempScalar[38] += mv1.Scalar14 * mv2.KVector5.Scalar12346 + mv1.Scalar15 * mv2.KVector5.Scalar12356 - mv1.Scalar45 * mv2.KVector5.Scalar23456;
            tempScalar[41] += -mv1.Scalar23 * mv2.KVector5.Scalar12346 + mv1.Scalar25 * mv2.KVector5.Scalar12456 + mv1.Scalar35 * mv2.KVector5.Scalar13456;
            tempScalar[42] += -mv1.Scalar13 * mv2.KVector5.Scalar12346 + mv1.Scalar15 * mv2.KVector5.Scalar12456 + mv1.Scalar35 * mv2.KVector5.Scalar23456;
            tempScalar[44] += mv1.Scalar12 * mv2.KVector5.Scalar12346 + mv1.Scalar15 * mv2.KVector5.Scalar13456 - mv1.Scalar25 * mv2.KVector5.Scalar23456;
            tempScalar[49] += -mv1.Scalar23 * mv2.KVector5.Scalar12356 - mv1.Scalar24 * mv2.KVector5.Scalar12456 - mv1.Scalar34 * mv2.KVector5.Scalar13456;
            tempScalar[50] += -mv1.Scalar13 * mv2.KVector5.Scalar12356 - mv1.Scalar14 * mv2.KVector5.Scalar12456 - mv1.Scalar34 * mv2.KVector5.Scalar23456;
            tempScalar[52] += mv1.Scalar12 * mv2.KVector5.Scalar12356 - mv1.Scalar14 * mv2.KVector5.Scalar13456 + mv1.Scalar24 * mv2.KVector5.Scalar23456;
            tempScalar[56] += mv1.Scalar12 * mv2.KVector5.Scalar12456 + mv1.Scalar13 * mv2.KVector5.Scalar13456 - mv1.Scalar23 * mv2.KVector5.Scalar23456;
        }
        
        if (!mv2.KVector6.IsZero())
        {
            tempScalar[15] += -mv1.Scalar56 * mv2.KVector6.Scalar123456;
            tempScalar[23] += mv1.Scalar46 * mv2.KVector6.Scalar123456;
            tempScalar[27] += -mv1.Scalar36 * mv2.KVector6.Scalar123456;
            tempScalar[29] += mv1.Scalar26 * mv2.KVector6.Scalar123456;
            tempScalar[30] += mv1.Scalar16 * mv2.KVector6.Scalar123456;
            tempScalar[39] += -mv1.Scalar45 * mv2.KVector6.Scalar123456;
            tempScalar[43] += mv1.Scalar35 * mv2.KVector6.Scalar123456;
            tempScalar[45] += -mv1.Scalar25 * mv2.KVector6.Scalar123456;
            tempScalar[46] += -mv1.Scalar15 * mv2.KVector6.Scalar123456;
            tempScalar[51] += -mv1.Scalar34 * mv2.KVector6.Scalar123456;
            tempScalar[53] += mv1.Scalar24 * mv2.KVector6.Scalar123456;
            tempScalar[54] += mv1.Scalar14 * mv2.KVector6.Scalar123456;
            tempScalar[57] += -mv1.Scalar23 * mv2.KVector6.Scalar123456;
            tempScalar[58] += -mv1.Scalar13 * mv2.KVector6.Scalar123456;
            tempScalar[60] += mv1.Scalar12 * mv2.KVector6.Scalar123456;
        }
        
        return Ga51Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector3 mv1, Ga51KVector0 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector3 mv1, Ga51KVector1 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector3 mv1, Ga51KVector2 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector3 mv1, Ga51KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector0.Zero;
        
        return new Ga51KVector0
        {
            Scalar = mv1.Scalar123 * mv2.Scalar123 + mv1.Scalar124 * mv2.Scalar124 + mv1.Scalar134 * mv2.Scalar134 - mv1.Scalar234 * mv2.Scalar234 + mv1.Scalar125 * mv2.Scalar125 + mv1.Scalar135 * mv2.Scalar135 - mv1.Scalar235 * mv2.Scalar235 + mv1.Scalar145 * mv2.Scalar145 - mv1.Scalar245 * mv2.Scalar245 - mv1.Scalar345 * mv2.Scalar345 + mv1.Scalar126 * mv2.Scalar126 + mv1.Scalar136 * mv2.Scalar136 - mv1.Scalar236 * mv2.Scalar236 + mv1.Scalar146 * mv2.Scalar146 - mv1.Scalar246 * mv2.Scalar246 - mv1.Scalar346 * mv2.Scalar346 + mv1.Scalar156 * mv2.Scalar156 - mv1.Scalar256 * mv2.Scalar256 - mv1.Scalar356 * mv2.Scalar356 - mv1.Scalar456 * mv2.Scalar456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector1 Lcp(this Ga51KVector3 mv1, Ga51KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector1.Zero;
        
        return new Ga51KVector1
        {
            Scalar1 = mv1.Scalar234 * mv2.Scalar1234 + mv1.Scalar235 * mv2.Scalar1235 + mv1.Scalar245 * mv2.Scalar1245 + mv1.Scalar345 * mv2.Scalar1345 + mv1.Scalar236 * mv2.Scalar1236 + mv1.Scalar246 * mv2.Scalar1246 + mv1.Scalar346 * mv2.Scalar1346 + mv1.Scalar256 * mv2.Scalar1256 + mv1.Scalar356 * mv2.Scalar1356 + mv1.Scalar456 * mv2.Scalar1456,
            Scalar2 = mv1.Scalar134 * mv2.Scalar1234 + mv1.Scalar135 * mv2.Scalar1235 + mv1.Scalar145 * mv2.Scalar1245 + mv1.Scalar345 * mv2.Scalar2345 + mv1.Scalar136 * mv2.Scalar1236 + mv1.Scalar146 * mv2.Scalar1246 + mv1.Scalar346 * mv2.Scalar2346 + mv1.Scalar156 * mv2.Scalar1256 + mv1.Scalar356 * mv2.Scalar2356 + mv1.Scalar456 * mv2.Scalar2456,
            Scalar3 = -mv1.Scalar124 * mv2.Scalar1234 - mv1.Scalar125 * mv2.Scalar1235 + mv1.Scalar145 * mv2.Scalar1345 - mv1.Scalar245 * mv2.Scalar2345 - mv1.Scalar126 * mv2.Scalar1236 + mv1.Scalar146 * mv2.Scalar1346 - mv1.Scalar246 * mv2.Scalar2346 + mv1.Scalar156 * mv2.Scalar1356 - mv1.Scalar256 * mv2.Scalar2356 + mv1.Scalar456 * mv2.Scalar3456,
            Scalar4 = mv1.Scalar123 * mv2.Scalar1234 - mv1.Scalar125 * mv2.Scalar1245 - mv1.Scalar135 * mv2.Scalar1345 + mv1.Scalar235 * mv2.Scalar2345 - mv1.Scalar126 * mv2.Scalar1246 - mv1.Scalar136 * mv2.Scalar1346 + mv1.Scalar236 * mv2.Scalar2346 + mv1.Scalar156 * mv2.Scalar1456 - mv1.Scalar256 * mv2.Scalar2456 - mv1.Scalar356 * mv2.Scalar3456,
            Scalar5 = mv1.Scalar123 * mv2.Scalar1235 + mv1.Scalar124 * mv2.Scalar1245 + mv1.Scalar134 * mv2.Scalar1345 - mv1.Scalar234 * mv2.Scalar2345 - mv1.Scalar126 * mv2.Scalar1256 - mv1.Scalar136 * mv2.Scalar1356 + mv1.Scalar236 * mv2.Scalar2356 - mv1.Scalar146 * mv2.Scalar1456 + mv1.Scalar246 * mv2.Scalar2456 + mv1.Scalar346 * mv2.Scalar3456,
            Scalar6 = mv1.Scalar123 * mv2.Scalar1236 + mv1.Scalar124 * mv2.Scalar1246 + mv1.Scalar134 * mv2.Scalar1346 - mv1.Scalar234 * mv2.Scalar2346 + mv1.Scalar125 * mv2.Scalar1256 + mv1.Scalar135 * mv2.Scalar1356 - mv1.Scalar235 * mv2.Scalar2356 + mv1.Scalar145 * mv2.Scalar1456 - mv1.Scalar245 * mv2.Scalar2456 - mv1.Scalar345 * mv2.Scalar3456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector2 Lcp(this Ga51KVector3 mv1, Ga51KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector2.Zero;
        
        return new Ga51KVector2
        {
            Scalar12 = -mv1.Scalar345 * mv2.Scalar12345 - mv1.Scalar346 * mv2.Scalar12346 - mv1.Scalar356 * mv2.Scalar12356 - mv1.Scalar456 * mv2.Scalar12456,
            Scalar13 = mv1.Scalar245 * mv2.Scalar12345 + mv1.Scalar246 * mv2.Scalar12346 + mv1.Scalar256 * mv2.Scalar12356 - mv1.Scalar456 * mv2.Scalar13456,
            Scalar23 = mv1.Scalar145 * mv2.Scalar12345 + mv1.Scalar146 * mv2.Scalar12346 + mv1.Scalar156 * mv2.Scalar12356 - mv1.Scalar456 * mv2.Scalar23456,
            Scalar14 = -mv1.Scalar235 * mv2.Scalar12345 - mv1.Scalar236 * mv2.Scalar12346 + mv1.Scalar256 * mv2.Scalar12456 + mv1.Scalar356 * mv2.Scalar13456,
            Scalar24 = -mv1.Scalar135 * mv2.Scalar12345 - mv1.Scalar136 * mv2.Scalar12346 + mv1.Scalar156 * mv2.Scalar12456 + mv1.Scalar356 * mv2.Scalar23456,
            Scalar34 = mv1.Scalar125 * mv2.Scalar12345 + mv1.Scalar126 * mv2.Scalar12346 + mv1.Scalar156 * mv2.Scalar13456 - mv1.Scalar256 * mv2.Scalar23456,
            Scalar15 = mv1.Scalar234 * mv2.Scalar12345 - mv1.Scalar236 * mv2.Scalar12356 - mv1.Scalar246 * mv2.Scalar12456 - mv1.Scalar346 * mv2.Scalar13456,
            Scalar25 = mv1.Scalar134 * mv2.Scalar12345 - mv1.Scalar136 * mv2.Scalar12356 - mv1.Scalar146 * mv2.Scalar12456 - mv1.Scalar346 * mv2.Scalar23456,
            Scalar35 = -mv1.Scalar124 * mv2.Scalar12345 + mv1.Scalar126 * mv2.Scalar12356 - mv1.Scalar146 * mv2.Scalar13456 + mv1.Scalar246 * mv2.Scalar23456,
            Scalar45 = mv1.Scalar123 * mv2.Scalar12345 + mv1.Scalar126 * mv2.Scalar12456 + mv1.Scalar136 * mv2.Scalar13456 - mv1.Scalar236 * mv2.Scalar23456,
            Scalar16 = mv1.Scalar234 * mv2.Scalar12346 + mv1.Scalar235 * mv2.Scalar12356 + mv1.Scalar245 * mv2.Scalar12456 + mv1.Scalar345 * mv2.Scalar13456,
            Scalar26 = mv1.Scalar134 * mv2.Scalar12346 + mv1.Scalar135 * mv2.Scalar12356 + mv1.Scalar145 * mv2.Scalar12456 + mv1.Scalar345 * mv2.Scalar23456,
            Scalar36 = -mv1.Scalar124 * mv2.Scalar12346 - mv1.Scalar125 * mv2.Scalar12356 + mv1.Scalar145 * mv2.Scalar13456 - mv1.Scalar245 * mv2.Scalar23456,
            Scalar46 = mv1.Scalar123 * mv2.Scalar12346 - mv1.Scalar125 * mv2.Scalar12456 - mv1.Scalar135 * mv2.Scalar13456 + mv1.Scalar235 * mv2.Scalar23456,
            Scalar56 = mv1.Scalar123 * mv2.Scalar12356 + mv1.Scalar124 * mv2.Scalar12456 + mv1.Scalar134 * mv2.Scalar13456 - mv1.Scalar234 * mv2.Scalar23456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector3 Lcp(this Ga51KVector3 mv1, Ga51KVector6 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector3.Zero;
        
        return new Ga51KVector3
        {
            Scalar123 = mv1.Scalar456 * mv2.Scalar123456,
            Scalar124 = -mv1.Scalar356 * mv2.Scalar123456,
            Scalar134 = mv1.Scalar256 * mv2.Scalar123456,
            Scalar234 = mv1.Scalar156 * mv2.Scalar123456,
            Scalar125 = mv1.Scalar346 * mv2.Scalar123456,
            Scalar135 = -mv1.Scalar246 * mv2.Scalar123456,
            Scalar235 = -mv1.Scalar146 * mv2.Scalar123456,
            Scalar145 = mv1.Scalar236 * mv2.Scalar123456,
            Scalar245 = mv1.Scalar136 * mv2.Scalar123456,
            Scalar345 = -mv1.Scalar126 * mv2.Scalar123456,
            Scalar126 = -mv1.Scalar345 * mv2.Scalar123456,
            Scalar136 = mv1.Scalar245 * mv2.Scalar123456,
            Scalar236 = mv1.Scalar145 * mv2.Scalar123456,
            Scalar146 = -mv1.Scalar235 * mv2.Scalar123456,
            Scalar246 = -mv1.Scalar135 * mv2.Scalar123456,
            Scalar346 = mv1.Scalar125 * mv2.Scalar123456,
            Scalar156 = mv1.Scalar234 * mv2.Scalar123456,
            Scalar256 = mv1.Scalar134 * mv2.Scalar123456,
            Scalar356 = -mv1.Scalar124 * mv2.Scalar123456,
            Scalar456 = mv1.Scalar123 * mv2.Scalar123456
        };
    }
    
    public static Ga51Multivector Lcp(this Ga51KVector3 mv1, Ga51Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[0] += mv1.Scalar123 * mv2.KVector3.Scalar123 + mv1.Scalar124 * mv2.KVector3.Scalar124 + mv1.Scalar134 * mv2.KVector3.Scalar134 - mv1.Scalar234 * mv2.KVector3.Scalar234 + mv1.Scalar125 * mv2.KVector3.Scalar125 + mv1.Scalar135 * mv2.KVector3.Scalar135 - mv1.Scalar235 * mv2.KVector3.Scalar235 + mv1.Scalar145 * mv2.KVector3.Scalar145 - mv1.Scalar245 * mv2.KVector3.Scalar245 - mv1.Scalar345 * mv2.KVector3.Scalar345 + mv1.Scalar126 * mv2.KVector3.Scalar126 + mv1.Scalar136 * mv2.KVector3.Scalar136 - mv1.Scalar236 * mv2.KVector3.Scalar236 + mv1.Scalar146 * mv2.KVector3.Scalar146 - mv1.Scalar246 * mv2.KVector3.Scalar246 - mv1.Scalar346 * mv2.KVector3.Scalar346 + mv1.Scalar156 * mv2.KVector3.Scalar156 - mv1.Scalar256 * mv2.KVector3.Scalar256 - mv1.Scalar356 * mv2.KVector3.Scalar356 - mv1.Scalar456 * mv2.KVector3.Scalar456;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[1] += mv1.Scalar234 * mv2.KVector4.Scalar1234 + mv1.Scalar235 * mv2.KVector4.Scalar1235 + mv1.Scalar245 * mv2.KVector4.Scalar1245 + mv1.Scalar345 * mv2.KVector4.Scalar1345 + mv1.Scalar236 * mv2.KVector4.Scalar1236 + mv1.Scalar246 * mv2.KVector4.Scalar1246 + mv1.Scalar346 * mv2.KVector4.Scalar1346 + mv1.Scalar256 * mv2.KVector4.Scalar1256 + mv1.Scalar356 * mv2.KVector4.Scalar1356 + mv1.Scalar456 * mv2.KVector4.Scalar1456;
            tempScalar[2] += mv1.Scalar134 * mv2.KVector4.Scalar1234 + mv1.Scalar135 * mv2.KVector4.Scalar1235 + mv1.Scalar145 * mv2.KVector4.Scalar1245 + mv1.Scalar345 * mv2.KVector4.Scalar2345 + mv1.Scalar136 * mv2.KVector4.Scalar1236 + mv1.Scalar146 * mv2.KVector4.Scalar1246 + mv1.Scalar346 * mv2.KVector4.Scalar2346 + mv1.Scalar156 * mv2.KVector4.Scalar1256 + mv1.Scalar356 * mv2.KVector4.Scalar2356 + mv1.Scalar456 * mv2.KVector4.Scalar2456;
            tempScalar[4] += -mv1.Scalar124 * mv2.KVector4.Scalar1234 - mv1.Scalar125 * mv2.KVector4.Scalar1235 + mv1.Scalar145 * mv2.KVector4.Scalar1345 - mv1.Scalar245 * mv2.KVector4.Scalar2345 - mv1.Scalar126 * mv2.KVector4.Scalar1236 + mv1.Scalar146 * mv2.KVector4.Scalar1346 - mv1.Scalar246 * mv2.KVector4.Scalar2346 + mv1.Scalar156 * mv2.KVector4.Scalar1356 - mv1.Scalar256 * mv2.KVector4.Scalar2356 + mv1.Scalar456 * mv2.KVector4.Scalar3456;
            tempScalar[8] += mv1.Scalar123 * mv2.KVector4.Scalar1234 - mv1.Scalar125 * mv2.KVector4.Scalar1245 - mv1.Scalar135 * mv2.KVector4.Scalar1345 + mv1.Scalar235 * mv2.KVector4.Scalar2345 - mv1.Scalar126 * mv2.KVector4.Scalar1246 - mv1.Scalar136 * mv2.KVector4.Scalar1346 + mv1.Scalar236 * mv2.KVector4.Scalar2346 + mv1.Scalar156 * mv2.KVector4.Scalar1456 - mv1.Scalar256 * mv2.KVector4.Scalar2456 - mv1.Scalar356 * mv2.KVector4.Scalar3456;
            tempScalar[16] += mv1.Scalar123 * mv2.KVector4.Scalar1235 + mv1.Scalar124 * mv2.KVector4.Scalar1245 + mv1.Scalar134 * mv2.KVector4.Scalar1345 - mv1.Scalar234 * mv2.KVector4.Scalar2345 - mv1.Scalar126 * mv2.KVector4.Scalar1256 - mv1.Scalar136 * mv2.KVector4.Scalar1356 + mv1.Scalar236 * mv2.KVector4.Scalar2356 - mv1.Scalar146 * mv2.KVector4.Scalar1456 + mv1.Scalar246 * mv2.KVector4.Scalar2456 + mv1.Scalar346 * mv2.KVector4.Scalar3456;
            tempScalar[32] += mv1.Scalar123 * mv2.KVector4.Scalar1236 + mv1.Scalar124 * mv2.KVector4.Scalar1246 + mv1.Scalar134 * mv2.KVector4.Scalar1346 - mv1.Scalar234 * mv2.KVector4.Scalar2346 + mv1.Scalar125 * mv2.KVector4.Scalar1256 + mv1.Scalar135 * mv2.KVector4.Scalar1356 - mv1.Scalar235 * mv2.KVector4.Scalar2356 + mv1.Scalar145 * mv2.KVector4.Scalar1456 - mv1.Scalar245 * mv2.KVector4.Scalar2456 - mv1.Scalar345 * mv2.KVector4.Scalar3456;
        }
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[3] += -mv1.Scalar345 * mv2.KVector5.Scalar12345 - mv1.Scalar346 * mv2.KVector5.Scalar12346 - mv1.Scalar356 * mv2.KVector5.Scalar12356 - mv1.Scalar456 * mv2.KVector5.Scalar12456;
            tempScalar[5] += mv1.Scalar245 * mv2.KVector5.Scalar12345 + mv1.Scalar246 * mv2.KVector5.Scalar12346 + mv1.Scalar256 * mv2.KVector5.Scalar12356 - mv1.Scalar456 * mv2.KVector5.Scalar13456;
            tempScalar[6] += mv1.Scalar145 * mv2.KVector5.Scalar12345 + mv1.Scalar146 * mv2.KVector5.Scalar12346 + mv1.Scalar156 * mv2.KVector5.Scalar12356 - mv1.Scalar456 * mv2.KVector5.Scalar23456;
            tempScalar[9] += -mv1.Scalar235 * mv2.KVector5.Scalar12345 - mv1.Scalar236 * mv2.KVector5.Scalar12346 + mv1.Scalar256 * mv2.KVector5.Scalar12456 + mv1.Scalar356 * mv2.KVector5.Scalar13456;
            tempScalar[10] += -mv1.Scalar135 * mv2.KVector5.Scalar12345 - mv1.Scalar136 * mv2.KVector5.Scalar12346 + mv1.Scalar156 * mv2.KVector5.Scalar12456 + mv1.Scalar356 * mv2.KVector5.Scalar23456;
            tempScalar[12] += mv1.Scalar125 * mv2.KVector5.Scalar12345 + mv1.Scalar126 * mv2.KVector5.Scalar12346 + mv1.Scalar156 * mv2.KVector5.Scalar13456 - mv1.Scalar256 * mv2.KVector5.Scalar23456;
            tempScalar[17] += mv1.Scalar234 * mv2.KVector5.Scalar12345 - mv1.Scalar236 * mv2.KVector5.Scalar12356 - mv1.Scalar246 * mv2.KVector5.Scalar12456 - mv1.Scalar346 * mv2.KVector5.Scalar13456;
            tempScalar[18] += mv1.Scalar134 * mv2.KVector5.Scalar12345 - mv1.Scalar136 * mv2.KVector5.Scalar12356 - mv1.Scalar146 * mv2.KVector5.Scalar12456 - mv1.Scalar346 * mv2.KVector5.Scalar23456;
            tempScalar[20] += -mv1.Scalar124 * mv2.KVector5.Scalar12345 + mv1.Scalar126 * mv2.KVector5.Scalar12356 - mv1.Scalar146 * mv2.KVector5.Scalar13456 + mv1.Scalar246 * mv2.KVector5.Scalar23456;
            tempScalar[24] += mv1.Scalar123 * mv2.KVector5.Scalar12345 + mv1.Scalar126 * mv2.KVector5.Scalar12456 + mv1.Scalar136 * mv2.KVector5.Scalar13456 - mv1.Scalar236 * mv2.KVector5.Scalar23456;
            tempScalar[33] += mv1.Scalar234 * mv2.KVector5.Scalar12346 + mv1.Scalar235 * mv2.KVector5.Scalar12356 + mv1.Scalar245 * mv2.KVector5.Scalar12456 + mv1.Scalar345 * mv2.KVector5.Scalar13456;
            tempScalar[34] += mv1.Scalar134 * mv2.KVector5.Scalar12346 + mv1.Scalar135 * mv2.KVector5.Scalar12356 + mv1.Scalar145 * mv2.KVector5.Scalar12456 + mv1.Scalar345 * mv2.KVector5.Scalar23456;
            tempScalar[36] += -mv1.Scalar124 * mv2.KVector5.Scalar12346 - mv1.Scalar125 * mv2.KVector5.Scalar12356 + mv1.Scalar145 * mv2.KVector5.Scalar13456 - mv1.Scalar245 * mv2.KVector5.Scalar23456;
            tempScalar[40] += mv1.Scalar123 * mv2.KVector5.Scalar12346 - mv1.Scalar125 * mv2.KVector5.Scalar12456 - mv1.Scalar135 * mv2.KVector5.Scalar13456 + mv1.Scalar235 * mv2.KVector5.Scalar23456;
            tempScalar[48] += mv1.Scalar123 * mv2.KVector5.Scalar12356 + mv1.Scalar124 * mv2.KVector5.Scalar12456 + mv1.Scalar134 * mv2.KVector5.Scalar13456 - mv1.Scalar234 * mv2.KVector5.Scalar23456;
        }
        
        if (!mv2.KVector6.IsZero())
        {
            tempScalar[7] += mv1.Scalar456 * mv2.KVector6.Scalar123456;
            tempScalar[11] += -mv1.Scalar356 * mv2.KVector6.Scalar123456;
            tempScalar[13] += mv1.Scalar256 * mv2.KVector6.Scalar123456;
            tempScalar[14] += mv1.Scalar156 * mv2.KVector6.Scalar123456;
            tempScalar[19] += mv1.Scalar346 * mv2.KVector6.Scalar123456;
            tempScalar[21] += -mv1.Scalar246 * mv2.KVector6.Scalar123456;
            tempScalar[22] += -mv1.Scalar146 * mv2.KVector6.Scalar123456;
            tempScalar[25] += mv1.Scalar236 * mv2.KVector6.Scalar123456;
            tempScalar[26] += mv1.Scalar136 * mv2.KVector6.Scalar123456;
            tempScalar[28] += -mv1.Scalar126 * mv2.KVector6.Scalar123456;
            tempScalar[35] += -mv1.Scalar345 * mv2.KVector6.Scalar123456;
            tempScalar[37] += mv1.Scalar245 * mv2.KVector6.Scalar123456;
            tempScalar[38] += mv1.Scalar145 * mv2.KVector6.Scalar123456;
            tempScalar[41] += -mv1.Scalar235 * mv2.KVector6.Scalar123456;
            tempScalar[42] += -mv1.Scalar135 * mv2.KVector6.Scalar123456;
            tempScalar[44] += mv1.Scalar125 * mv2.KVector6.Scalar123456;
            tempScalar[49] += mv1.Scalar234 * mv2.KVector6.Scalar123456;
            tempScalar[50] += mv1.Scalar134 * mv2.KVector6.Scalar123456;
            tempScalar[52] += -mv1.Scalar124 * mv2.KVector6.Scalar123456;
            tempScalar[56] += mv1.Scalar123 * mv2.KVector6.Scalar123456;
        }
        
        return Ga51Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector4 mv1, Ga51KVector0 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector4 mv1, Ga51KVector1 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector4 mv1, Ga51KVector2 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector4 mv1, Ga51KVector3 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector4 mv1, Ga51KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector0.Zero;
        
        return new Ga51KVector0
        {
            Scalar = -mv1.Scalar1234 * mv2.Scalar1234 - mv1.Scalar1235 * mv2.Scalar1235 - mv1.Scalar1245 * mv2.Scalar1245 - mv1.Scalar1345 * mv2.Scalar1345 + mv1.Scalar2345 * mv2.Scalar2345 - mv1.Scalar1236 * mv2.Scalar1236 - mv1.Scalar1246 * mv2.Scalar1246 - mv1.Scalar1346 * mv2.Scalar1346 + mv1.Scalar2346 * mv2.Scalar2346 - mv1.Scalar1256 * mv2.Scalar1256 - mv1.Scalar1356 * mv2.Scalar1356 + mv1.Scalar2356 * mv2.Scalar2356 - mv1.Scalar1456 * mv2.Scalar1456 + mv1.Scalar2456 * mv2.Scalar2456 + mv1.Scalar3456 * mv2.Scalar3456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector1 Lcp(this Ga51KVector4 mv1, Ga51KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector1.Zero;
        
        return new Ga51KVector1
        {
            Scalar1 = mv1.Scalar2345 * mv2.Scalar12345 + mv1.Scalar2346 * mv2.Scalar12346 + mv1.Scalar2356 * mv2.Scalar12356 + mv1.Scalar2456 * mv2.Scalar12456 + mv1.Scalar3456 * mv2.Scalar13456,
            Scalar2 = mv1.Scalar1345 * mv2.Scalar12345 + mv1.Scalar1346 * mv2.Scalar12346 + mv1.Scalar1356 * mv2.Scalar12356 + mv1.Scalar1456 * mv2.Scalar12456 + mv1.Scalar3456 * mv2.Scalar23456,
            Scalar3 = -mv1.Scalar1245 * mv2.Scalar12345 - mv1.Scalar1246 * mv2.Scalar12346 - mv1.Scalar1256 * mv2.Scalar12356 + mv1.Scalar1456 * mv2.Scalar13456 - mv1.Scalar2456 * mv2.Scalar23456,
            Scalar4 = mv1.Scalar1235 * mv2.Scalar12345 + mv1.Scalar1236 * mv2.Scalar12346 - mv1.Scalar1256 * mv2.Scalar12456 - mv1.Scalar1356 * mv2.Scalar13456 + mv1.Scalar2356 * mv2.Scalar23456,
            Scalar5 = -mv1.Scalar1234 * mv2.Scalar12345 + mv1.Scalar1236 * mv2.Scalar12356 + mv1.Scalar1246 * mv2.Scalar12456 + mv1.Scalar1346 * mv2.Scalar13456 - mv1.Scalar2346 * mv2.Scalar23456,
            Scalar6 = -mv1.Scalar1234 * mv2.Scalar12346 - mv1.Scalar1235 * mv2.Scalar12356 - mv1.Scalar1245 * mv2.Scalar12456 - mv1.Scalar1345 * mv2.Scalar13456 + mv1.Scalar2345 * mv2.Scalar23456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector2 Lcp(this Ga51KVector4 mv1, Ga51KVector6 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector2.Zero;
        
        return new Ga51KVector2
        {
            Scalar12 = mv1.Scalar3456 * mv2.Scalar123456,
            Scalar13 = -mv1.Scalar2456 * mv2.Scalar123456,
            Scalar23 = -mv1.Scalar1456 * mv2.Scalar123456,
            Scalar14 = mv1.Scalar2356 * mv2.Scalar123456,
            Scalar24 = mv1.Scalar1356 * mv2.Scalar123456,
            Scalar34 = -mv1.Scalar1256 * mv2.Scalar123456,
            Scalar15 = -mv1.Scalar2346 * mv2.Scalar123456,
            Scalar25 = -mv1.Scalar1346 * mv2.Scalar123456,
            Scalar35 = mv1.Scalar1246 * mv2.Scalar123456,
            Scalar45 = -mv1.Scalar1236 * mv2.Scalar123456,
            Scalar16 = mv1.Scalar2345 * mv2.Scalar123456,
            Scalar26 = mv1.Scalar1345 * mv2.Scalar123456,
            Scalar36 = -mv1.Scalar1245 * mv2.Scalar123456,
            Scalar46 = mv1.Scalar1235 * mv2.Scalar123456,
            Scalar56 = -mv1.Scalar1234 * mv2.Scalar123456
        };
    }
    
    public static Ga51Multivector Lcp(this Ga51KVector4 mv1, Ga51Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[0] += -mv1.Scalar1234 * mv2.KVector4.Scalar1234 - mv1.Scalar1235 * mv2.KVector4.Scalar1235 - mv1.Scalar1245 * mv2.KVector4.Scalar1245 - mv1.Scalar1345 * mv2.KVector4.Scalar1345 + mv1.Scalar2345 * mv2.KVector4.Scalar2345 - mv1.Scalar1236 * mv2.KVector4.Scalar1236 - mv1.Scalar1246 * mv2.KVector4.Scalar1246 - mv1.Scalar1346 * mv2.KVector4.Scalar1346 + mv1.Scalar2346 * mv2.KVector4.Scalar2346 - mv1.Scalar1256 * mv2.KVector4.Scalar1256 - mv1.Scalar1356 * mv2.KVector4.Scalar1356 + mv1.Scalar2356 * mv2.KVector4.Scalar2356 - mv1.Scalar1456 * mv2.KVector4.Scalar1456 + mv1.Scalar2456 * mv2.KVector4.Scalar2456 + mv1.Scalar3456 * mv2.KVector4.Scalar3456;
        }
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[1] += mv1.Scalar2345 * mv2.KVector5.Scalar12345 + mv1.Scalar2346 * mv2.KVector5.Scalar12346 + mv1.Scalar2356 * mv2.KVector5.Scalar12356 + mv1.Scalar2456 * mv2.KVector5.Scalar12456 + mv1.Scalar3456 * mv2.KVector5.Scalar13456;
            tempScalar[2] += mv1.Scalar1345 * mv2.KVector5.Scalar12345 + mv1.Scalar1346 * mv2.KVector5.Scalar12346 + mv1.Scalar1356 * mv2.KVector5.Scalar12356 + mv1.Scalar1456 * mv2.KVector5.Scalar12456 + mv1.Scalar3456 * mv2.KVector5.Scalar23456;
            tempScalar[4] += -mv1.Scalar1245 * mv2.KVector5.Scalar12345 - mv1.Scalar1246 * mv2.KVector5.Scalar12346 - mv1.Scalar1256 * mv2.KVector5.Scalar12356 + mv1.Scalar1456 * mv2.KVector5.Scalar13456 - mv1.Scalar2456 * mv2.KVector5.Scalar23456;
            tempScalar[8] += mv1.Scalar1235 * mv2.KVector5.Scalar12345 + mv1.Scalar1236 * mv2.KVector5.Scalar12346 - mv1.Scalar1256 * mv2.KVector5.Scalar12456 - mv1.Scalar1356 * mv2.KVector5.Scalar13456 + mv1.Scalar2356 * mv2.KVector5.Scalar23456;
            tempScalar[16] += -mv1.Scalar1234 * mv2.KVector5.Scalar12345 + mv1.Scalar1236 * mv2.KVector5.Scalar12356 + mv1.Scalar1246 * mv2.KVector5.Scalar12456 + mv1.Scalar1346 * mv2.KVector5.Scalar13456 - mv1.Scalar2346 * mv2.KVector5.Scalar23456;
            tempScalar[32] += -mv1.Scalar1234 * mv2.KVector5.Scalar12346 - mv1.Scalar1235 * mv2.KVector5.Scalar12356 - mv1.Scalar1245 * mv2.KVector5.Scalar12456 - mv1.Scalar1345 * mv2.KVector5.Scalar13456 + mv1.Scalar2345 * mv2.KVector5.Scalar23456;
        }
        
        if (!mv2.KVector6.IsZero())
        {
            tempScalar[3] += mv1.Scalar3456 * mv2.KVector6.Scalar123456;
            tempScalar[5] += -mv1.Scalar2456 * mv2.KVector6.Scalar123456;
            tempScalar[6] += -mv1.Scalar1456 * mv2.KVector6.Scalar123456;
            tempScalar[9] += mv1.Scalar2356 * mv2.KVector6.Scalar123456;
            tempScalar[10] += mv1.Scalar1356 * mv2.KVector6.Scalar123456;
            tempScalar[12] += -mv1.Scalar1256 * mv2.KVector6.Scalar123456;
            tempScalar[17] += -mv1.Scalar2346 * mv2.KVector6.Scalar123456;
            tempScalar[18] += -mv1.Scalar1346 * mv2.KVector6.Scalar123456;
            tempScalar[20] += mv1.Scalar1246 * mv2.KVector6.Scalar123456;
            tempScalar[24] += -mv1.Scalar1236 * mv2.KVector6.Scalar123456;
            tempScalar[33] += mv1.Scalar2345 * mv2.KVector6.Scalar123456;
            tempScalar[34] += mv1.Scalar1345 * mv2.KVector6.Scalar123456;
            tempScalar[36] += -mv1.Scalar1245 * mv2.KVector6.Scalar123456;
            tempScalar[40] += mv1.Scalar1235 * mv2.KVector6.Scalar123456;
            tempScalar[48] += -mv1.Scalar1234 * mv2.KVector6.Scalar123456;
        }
        
        return Ga51Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector5 mv1, Ga51KVector0 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector5 mv1, Ga51KVector1 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector5 mv1, Ga51KVector2 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector5 mv1, Ga51KVector3 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector5 mv1, Ga51KVector4 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector5 mv1, Ga51KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector0.Zero;
        
        return new Ga51KVector0
        {
            Scalar = -mv1.Scalar12345 * mv2.Scalar12345 - mv1.Scalar12346 * mv2.Scalar12346 - mv1.Scalar12356 * mv2.Scalar12356 - mv1.Scalar12456 * mv2.Scalar12456 - mv1.Scalar13456 * mv2.Scalar13456 + mv1.Scalar23456 * mv2.Scalar23456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector1 Lcp(this Ga51KVector5 mv1, Ga51KVector6 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector1.Zero;
        
        return new Ga51KVector1
        {
            Scalar1 = -mv1.Scalar23456 * mv2.Scalar123456,
            Scalar2 = -mv1.Scalar13456 * mv2.Scalar123456,
            Scalar3 = mv1.Scalar12456 * mv2.Scalar123456,
            Scalar4 = -mv1.Scalar12356 * mv2.Scalar123456,
            Scalar5 = mv1.Scalar12346 * mv2.Scalar123456,
            Scalar6 = -mv1.Scalar12345 * mv2.Scalar123456
        };
    }
    
    public static Ga51Multivector Lcp(this Ga51KVector5 mv1, Ga51Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[0] += -mv1.Scalar12345 * mv2.KVector5.Scalar12345 - mv1.Scalar12346 * mv2.KVector5.Scalar12346 - mv1.Scalar12356 * mv2.KVector5.Scalar12356 - mv1.Scalar12456 * mv2.KVector5.Scalar12456 - mv1.Scalar13456 * mv2.KVector5.Scalar13456 + mv1.Scalar23456 * mv2.KVector5.Scalar23456;
        }
        
        if (!mv2.KVector6.IsZero())
        {
            tempScalar[1] += -mv1.Scalar23456 * mv2.KVector6.Scalar123456;
            tempScalar[2] += -mv1.Scalar13456 * mv2.KVector6.Scalar123456;
            tempScalar[4] += mv1.Scalar12456 * mv2.KVector6.Scalar123456;
            tempScalar[8] += -mv1.Scalar12356 * mv2.KVector6.Scalar123456;
            tempScalar[16] += mv1.Scalar12346 * mv2.KVector6.Scalar123456;
            tempScalar[32] += -mv1.Scalar12345 * mv2.KVector6.Scalar123456;
        }
        
        return Ga51Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector6 mv1, Ga51KVector0 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector6 mv1, Ga51KVector1 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector6 mv1, Ga51KVector2 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector6 mv1, Ga51KVector3 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector6 mv1, Ga51KVector4 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector6 mv1, Ga51KVector5 mv2)
    {
        return Ga51KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga51KVector0 Lcp(this Ga51KVector6 mv1, Ga51KVector6 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector0.Zero;
        
        return new Ga51KVector0
        {
            Scalar = mv1.Scalar123456 * mv2.Scalar123456
        };
    }
    
    public static Ga51KVector0 Lcp(this Ga51KVector6 mv1, Ga51Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv2.KVector6.IsZero())
        {
            tempScalar += mv1.Scalar123456 * mv2.KVector6.Scalar123456;
        }
        
        return new Ga51KVector0
        {
            Scalar = tempScalar
        };
    }
    
    public static Ga51KVector0 Lcp(this Ga51Multivector mv1, Ga51KVector0 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51KVector0.Zero;
        
        var tempScalar = 0d;
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar += mv1.KVector0.Scalar * mv2.Scalar;
        }
        
        return new Ga51KVector0
        {
            Scalar = tempScalar
        };
    }
    
    public static Ga51Multivector Lcp(this Ga51Multivector mv1, Ga51KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[1] += mv1.KVector0.Scalar * mv2.Scalar1;
            tempScalar[2] += mv1.KVector0.Scalar * mv2.Scalar2;
            tempScalar[4] += mv1.KVector0.Scalar * mv2.Scalar3;
            tempScalar[8] += mv1.KVector0.Scalar * mv2.Scalar4;
            tempScalar[16] += mv1.KVector0.Scalar * mv2.Scalar5;
            tempScalar[32] += mv1.KVector0.Scalar * mv2.Scalar6;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[0] += -mv1.KVector1.Scalar1 * mv2.Scalar1 + mv1.KVector1.Scalar2 * mv2.Scalar2 + mv1.KVector1.Scalar3 * mv2.Scalar3 + mv1.KVector1.Scalar4 * mv2.Scalar4 + mv1.KVector1.Scalar5 * mv2.Scalar5 + mv1.KVector1.Scalar6 * mv2.Scalar6;
        }
        
        return Ga51Multivector.Create(tempScalar);
    }
    
    public static Ga51Multivector Lcp(this Ga51Multivector mv1, Ga51KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51Multivector.Zero;
        
        var tempScalar = new double[64];
        
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
            tempScalar[33] += mv1.KVector0.Scalar * mv2.Scalar16;
            tempScalar[34] += mv1.KVector0.Scalar * mv2.Scalar26;
            tempScalar[36] += mv1.KVector0.Scalar * mv2.Scalar36;
            tempScalar[40] += mv1.KVector0.Scalar * mv2.Scalar46;
            tempScalar[48] += mv1.KVector0.Scalar * mv2.Scalar56;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += -mv1.KVector1.Scalar2 * mv2.Scalar12 - mv1.KVector1.Scalar3 * mv2.Scalar13 - mv1.KVector1.Scalar4 * mv2.Scalar14 - mv1.KVector1.Scalar5 * mv2.Scalar15 - mv1.KVector1.Scalar6 * mv2.Scalar16;
            tempScalar[2] += -mv1.KVector1.Scalar1 * mv2.Scalar12 - mv1.KVector1.Scalar3 * mv2.Scalar23 - mv1.KVector1.Scalar4 * mv2.Scalar24 - mv1.KVector1.Scalar5 * mv2.Scalar25 - mv1.KVector1.Scalar6 * mv2.Scalar26;
            tempScalar[4] += -mv1.KVector1.Scalar1 * mv2.Scalar13 + mv1.KVector1.Scalar2 * mv2.Scalar23 - mv1.KVector1.Scalar4 * mv2.Scalar34 - mv1.KVector1.Scalar5 * mv2.Scalar35 - mv1.KVector1.Scalar6 * mv2.Scalar36;
            tempScalar[8] += -mv1.KVector1.Scalar1 * mv2.Scalar14 + mv1.KVector1.Scalar2 * mv2.Scalar24 + mv1.KVector1.Scalar3 * mv2.Scalar34 - mv1.KVector1.Scalar5 * mv2.Scalar45 - mv1.KVector1.Scalar6 * mv2.Scalar46;
            tempScalar[16] += -mv1.KVector1.Scalar1 * mv2.Scalar15 + mv1.KVector1.Scalar2 * mv2.Scalar25 + mv1.KVector1.Scalar3 * mv2.Scalar35 + mv1.KVector1.Scalar4 * mv2.Scalar45 - mv1.KVector1.Scalar6 * mv2.Scalar56;
            tempScalar[32] += -mv1.KVector1.Scalar1 * mv2.Scalar16 + mv1.KVector1.Scalar2 * mv2.Scalar26 + mv1.KVector1.Scalar3 * mv2.Scalar36 + mv1.KVector1.Scalar4 * mv2.Scalar46 + mv1.KVector1.Scalar5 * mv2.Scalar56;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[0] += mv1.KVector2.Scalar12 * mv2.Scalar12 + mv1.KVector2.Scalar13 * mv2.Scalar13 - mv1.KVector2.Scalar23 * mv2.Scalar23 + mv1.KVector2.Scalar14 * mv2.Scalar14 - mv1.KVector2.Scalar24 * mv2.Scalar24 - mv1.KVector2.Scalar34 * mv2.Scalar34 + mv1.KVector2.Scalar15 * mv2.Scalar15 - mv1.KVector2.Scalar25 * mv2.Scalar25 - mv1.KVector2.Scalar35 * mv2.Scalar35 - mv1.KVector2.Scalar45 * mv2.Scalar45 + mv1.KVector2.Scalar16 * mv2.Scalar16 - mv1.KVector2.Scalar26 * mv2.Scalar26 - mv1.KVector2.Scalar36 * mv2.Scalar36 - mv1.KVector2.Scalar46 * mv2.Scalar46 - mv1.KVector2.Scalar56 * mv2.Scalar56;
        }
        
        return Ga51Multivector.Create(tempScalar);
    }
    
    public static Ga51Multivector Lcp(this Ga51Multivector mv1, Ga51KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51Multivector.Zero;
        
        var tempScalar = new double[64];
        
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
            tempScalar[35] += mv1.KVector0.Scalar * mv2.Scalar126;
            tempScalar[37] += mv1.KVector0.Scalar * mv2.Scalar136;
            tempScalar[38] += mv1.KVector0.Scalar * mv2.Scalar236;
            tempScalar[41] += mv1.KVector0.Scalar * mv2.Scalar146;
            tempScalar[42] += mv1.KVector0.Scalar * mv2.Scalar246;
            tempScalar[44] += mv1.KVector0.Scalar * mv2.Scalar346;
            tempScalar[49] += mv1.KVector0.Scalar * mv2.Scalar156;
            tempScalar[50] += mv1.KVector0.Scalar * mv2.Scalar256;
            tempScalar[52] += mv1.KVector0.Scalar * mv2.Scalar356;
            tempScalar[56] += mv1.KVector0.Scalar * mv2.Scalar456;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[3] += mv1.KVector1.Scalar3 * mv2.Scalar123 + mv1.KVector1.Scalar4 * mv2.Scalar124 + mv1.KVector1.Scalar5 * mv2.Scalar125 + mv1.KVector1.Scalar6 * mv2.Scalar126;
            tempScalar[5] += -mv1.KVector1.Scalar2 * mv2.Scalar123 + mv1.KVector1.Scalar4 * mv2.Scalar134 + mv1.KVector1.Scalar5 * mv2.Scalar135 + mv1.KVector1.Scalar6 * mv2.Scalar136;
            tempScalar[6] += -mv1.KVector1.Scalar1 * mv2.Scalar123 + mv1.KVector1.Scalar4 * mv2.Scalar234 + mv1.KVector1.Scalar5 * mv2.Scalar235 + mv1.KVector1.Scalar6 * mv2.Scalar236;
            tempScalar[9] += -mv1.KVector1.Scalar2 * mv2.Scalar124 - mv1.KVector1.Scalar3 * mv2.Scalar134 + mv1.KVector1.Scalar5 * mv2.Scalar145 + mv1.KVector1.Scalar6 * mv2.Scalar146;
            tempScalar[10] += -mv1.KVector1.Scalar1 * mv2.Scalar124 - mv1.KVector1.Scalar3 * mv2.Scalar234 + mv1.KVector1.Scalar5 * mv2.Scalar245 + mv1.KVector1.Scalar6 * mv2.Scalar246;
            tempScalar[12] += -mv1.KVector1.Scalar1 * mv2.Scalar134 + mv1.KVector1.Scalar2 * mv2.Scalar234 + mv1.KVector1.Scalar5 * mv2.Scalar345 + mv1.KVector1.Scalar6 * mv2.Scalar346;
            tempScalar[17] += -mv1.KVector1.Scalar2 * mv2.Scalar125 - mv1.KVector1.Scalar3 * mv2.Scalar135 - mv1.KVector1.Scalar4 * mv2.Scalar145 + mv1.KVector1.Scalar6 * mv2.Scalar156;
            tempScalar[18] += -mv1.KVector1.Scalar1 * mv2.Scalar125 - mv1.KVector1.Scalar3 * mv2.Scalar235 - mv1.KVector1.Scalar4 * mv2.Scalar245 + mv1.KVector1.Scalar6 * mv2.Scalar256;
            tempScalar[20] += -mv1.KVector1.Scalar1 * mv2.Scalar135 + mv1.KVector1.Scalar2 * mv2.Scalar235 - mv1.KVector1.Scalar4 * mv2.Scalar345 + mv1.KVector1.Scalar6 * mv2.Scalar356;
            tempScalar[24] += -mv1.KVector1.Scalar1 * mv2.Scalar145 + mv1.KVector1.Scalar2 * mv2.Scalar245 + mv1.KVector1.Scalar3 * mv2.Scalar345 + mv1.KVector1.Scalar6 * mv2.Scalar456;
            tempScalar[33] += -mv1.KVector1.Scalar2 * mv2.Scalar126 - mv1.KVector1.Scalar3 * mv2.Scalar136 - mv1.KVector1.Scalar4 * mv2.Scalar146 - mv1.KVector1.Scalar5 * mv2.Scalar156;
            tempScalar[34] += -mv1.KVector1.Scalar1 * mv2.Scalar126 - mv1.KVector1.Scalar3 * mv2.Scalar236 - mv1.KVector1.Scalar4 * mv2.Scalar246 - mv1.KVector1.Scalar5 * mv2.Scalar256;
            tempScalar[36] += -mv1.KVector1.Scalar1 * mv2.Scalar136 + mv1.KVector1.Scalar2 * mv2.Scalar236 - mv1.KVector1.Scalar4 * mv2.Scalar346 - mv1.KVector1.Scalar5 * mv2.Scalar356;
            tempScalar[40] += -mv1.KVector1.Scalar1 * mv2.Scalar146 + mv1.KVector1.Scalar2 * mv2.Scalar246 + mv1.KVector1.Scalar3 * mv2.Scalar346 - mv1.KVector1.Scalar5 * mv2.Scalar456;
            tempScalar[48] += -mv1.KVector1.Scalar1 * mv2.Scalar156 + mv1.KVector1.Scalar2 * mv2.Scalar256 + mv1.KVector1.Scalar3 * mv2.Scalar356 + mv1.KVector1.Scalar4 * mv2.Scalar456;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[1] += -mv1.KVector2.Scalar23 * mv2.Scalar123 - mv1.KVector2.Scalar24 * mv2.Scalar124 - mv1.KVector2.Scalar34 * mv2.Scalar134 - mv1.KVector2.Scalar25 * mv2.Scalar125 - mv1.KVector2.Scalar35 * mv2.Scalar135 - mv1.KVector2.Scalar45 * mv2.Scalar145 - mv1.KVector2.Scalar26 * mv2.Scalar126 - mv1.KVector2.Scalar36 * mv2.Scalar136 - mv1.KVector2.Scalar46 * mv2.Scalar146 - mv1.KVector2.Scalar56 * mv2.Scalar156;
            tempScalar[2] += -mv1.KVector2.Scalar13 * mv2.Scalar123 - mv1.KVector2.Scalar14 * mv2.Scalar124 - mv1.KVector2.Scalar34 * mv2.Scalar234 - mv1.KVector2.Scalar15 * mv2.Scalar125 - mv1.KVector2.Scalar35 * mv2.Scalar235 - mv1.KVector2.Scalar45 * mv2.Scalar245 - mv1.KVector2.Scalar16 * mv2.Scalar126 - mv1.KVector2.Scalar36 * mv2.Scalar236 - mv1.KVector2.Scalar46 * mv2.Scalar246 - mv1.KVector2.Scalar56 * mv2.Scalar256;
            tempScalar[4] += mv1.KVector2.Scalar12 * mv2.Scalar123 - mv1.KVector2.Scalar14 * mv2.Scalar134 + mv1.KVector2.Scalar24 * mv2.Scalar234 - mv1.KVector2.Scalar15 * mv2.Scalar135 + mv1.KVector2.Scalar25 * mv2.Scalar235 - mv1.KVector2.Scalar45 * mv2.Scalar345 - mv1.KVector2.Scalar16 * mv2.Scalar136 + mv1.KVector2.Scalar26 * mv2.Scalar236 - mv1.KVector2.Scalar46 * mv2.Scalar346 - mv1.KVector2.Scalar56 * mv2.Scalar356;
            tempScalar[8] += mv1.KVector2.Scalar12 * mv2.Scalar124 + mv1.KVector2.Scalar13 * mv2.Scalar134 - mv1.KVector2.Scalar23 * mv2.Scalar234 - mv1.KVector2.Scalar15 * mv2.Scalar145 + mv1.KVector2.Scalar25 * mv2.Scalar245 + mv1.KVector2.Scalar35 * mv2.Scalar345 - mv1.KVector2.Scalar16 * mv2.Scalar146 + mv1.KVector2.Scalar26 * mv2.Scalar246 + mv1.KVector2.Scalar36 * mv2.Scalar346 - mv1.KVector2.Scalar56 * mv2.Scalar456;
            tempScalar[16] += mv1.KVector2.Scalar12 * mv2.Scalar125 + mv1.KVector2.Scalar13 * mv2.Scalar135 - mv1.KVector2.Scalar23 * mv2.Scalar235 + mv1.KVector2.Scalar14 * mv2.Scalar145 - mv1.KVector2.Scalar24 * mv2.Scalar245 - mv1.KVector2.Scalar34 * mv2.Scalar345 - mv1.KVector2.Scalar16 * mv2.Scalar156 + mv1.KVector2.Scalar26 * mv2.Scalar256 + mv1.KVector2.Scalar36 * mv2.Scalar356 + mv1.KVector2.Scalar46 * mv2.Scalar456;
            tempScalar[32] += mv1.KVector2.Scalar12 * mv2.Scalar126 + mv1.KVector2.Scalar13 * mv2.Scalar136 - mv1.KVector2.Scalar23 * mv2.Scalar236 + mv1.KVector2.Scalar14 * mv2.Scalar146 - mv1.KVector2.Scalar24 * mv2.Scalar246 - mv1.KVector2.Scalar34 * mv2.Scalar346 + mv1.KVector2.Scalar15 * mv2.Scalar156 - mv1.KVector2.Scalar25 * mv2.Scalar256 - mv1.KVector2.Scalar35 * mv2.Scalar356 - mv1.KVector2.Scalar45 * mv2.Scalar456;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[0] += mv1.KVector3.Scalar123 * mv2.Scalar123 + mv1.KVector3.Scalar124 * mv2.Scalar124 + mv1.KVector3.Scalar134 * mv2.Scalar134 - mv1.KVector3.Scalar234 * mv2.Scalar234 + mv1.KVector3.Scalar125 * mv2.Scalar125 + mv1.KVector3.Scalar135 * mv2.Scalar135 - mv1.KVector3.Scalar235 * mv2.Scalar235 + mv1.KVector3.Scalar145 * mv2.Scalar145 - mv1.KVector3.Scalar245 * mv2.Scalar245 - mv1.KVector3.Scalar345 * mv2.Scalar345 + mv1.KVector3.Scalar126 * mv2.Scalar126 + mv1.KVector3.Scalar136 * mv2.Scalar136 - mv1.KVector3.Scalar236 * mv2.Scalar236 + mv1.KVector3.Scalar146 * mv2.Scalar146 - mv1.KVector3.Scalar246 * mv2.Scalar246 - mv1.KVector3.Scalar346 * mv2.Scalar346 + mv1.KVector3.Scalar156 * mv2.Scalar156 - mv1.KVector3.Scalar256 * mv2.Scalar256 - mv1.KVector3.Scalar356 * mv2.Scalar356 - mv1.KVector3.Scalar456 * mv2.Scalar456;
        }
        
        return Ga51Multivector.Create(tempScalar);
    }
    
    public static Ga51Multivector Lcp(this Ga51Multivector mv1, Ga51KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[15] += mv1.KVector0.Scalar * mv2.Scalar1234;
            tempScalar[23] += mv1.KVector0.Scalar * mv2.Scalar1235;
            tempScalar[27] += mv1.KVector0.Scalar * mv2.Scalar1245;
            tempScalar[29] += mv1.KVector0.Scalar * mv2.Scalar1345;
            tempScalar[30] += mv1.KVector0.Scalar * mv2.Scalar2345;
            tempScalar[39] += mv1.KVector0.Scalar * mv2.Scalar1236;
            tempScalar[43] += mv1.KVector0.Scalar * mv2.Scalar1246;
            tempScalar[45] += mv1.KVector0.Scalar * mv2.Scalar1346;
            tempScalar[46] += mv1.KVector0.Scalar * mv2.Scalar2346;
            tempScalar[51] += mv1.KVector0.Scalar * mv2.Scalar1256;
            tempScalar[53] += mv1.KVector0.Scalar * mv2.Scalar1356;
            tempScalar[54] += mv1.KVector0.Scalar * mv2.Scalar2356;
            tempScalar[57] += mv1.KVector0.Scalar * mv2.Scalar1456;
            tempScalar[58] += mv1.KVector0.Scalar * mv2.Scalar2456;
            tempScalar[60] += mv1.KVector0.Scalar * mv2.Scalar3456;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[7] += -mv1.KVector1.Scalar4 * mv2.Scalar1234 - mv1.KVector1.Scalar5 * mv2.Scalar1235 - mv1.KVector1.Scalar6 * mv2.Scalar1236;
            tempScalar[11] += mv1.KVector1.Scalar3 * mv2.Scalar1234 - mv1.KVector1.Scalar5 * mv2.Scalar1245 - mv1.KVector1.Scalar6 * mv2.Scalar1246;
            tempScalar[13] += -mv1.KVector1.Scalar2 * mv2.Scalar1234 - mv1.KVector1.Scalar5 * mv2.Scalar1345 - mv1.KVector1.Scalar6 * mv2.Scalar1346;
            tempScalar[14] += -mv1.KVector1.Scalar1 * mv2.Scalar1234 - mv1.KVector1.Scalar5 * mv2.Scalar2345 - mv1.KVector1.Scalar6 * mv2.Scalar2346;
            tempScalar[19] += mv1.KVector1.Scalar3 * mv2.Scalar1235 + mv1.KVector1.Scalar4 * mv2.Scalar1245 - mv1.KVector1.Scalar6 * mv2.Scalar1256;
            tempScalar[21] += -mv1.KVector1.Scalar2 * mv2.Scalar1235 + mv1.KVector1.Scalar4 * mv2.Scalar1345 - mv1.KVector1.Scalar6 * mv2.Scalar1356;
            tempScalar[22] += -mv1.KVector1.Scalar1 * mv2.Scalar1235 + mv1.KVector1.Scalar4 * mv2.Scalar2345 - mv1.KVector1.Scalar6 * mv2.Scalar2356;
            tempScalar[25] += -mv1.KVector1.Scalar2 * mv2.Scalar1245 - mv1.KVector1.Scalar3 * mv2.Scalar1345 - mv1.KVector1.Scalar6 * mv2.Scalar1456;
            tempScalar[26] += -mv1.KVector1.Scalar1 * mv2.Scalar1245 - mv1.KVector1.Scalar3 * mv2.Scalar2345 - mv1.KVector1.Scalar6 * mv2.Scalar2456;
            tempScalar[28] += -mv1.KVector1.Scalar1 * mv2.Scalar1345 + mv1.KVector1.Scalar2 * mv2.Scalar2345 - mv1.KVector1.Scalar6 * mv2.Scalar3456;
            tempScalar[35] += mv1.KVector1.Scalar3 * mv2.Scalar1236 + mv1.KVector1.Scalar4 * mv2.Scalar1246 + mv1.KVector1.Scalar5 * mv2.Scalar1256;
            tempScalar[37] += -mv1.KVector1.Scalar2 * mv2.Scalar1236 + mv1.KVector1.Scalar4 * mv2.Scalar1346 + mv1.KVector1.Scalar5 * mv2.Scalar1356;
            tempScalar[38] += -mv1.KVector1.Scalar1 * mv2.Scalar1236 + mv1.KVector1.Scalar4 * mv2.Scalar2346 + mv1.KVector1.Scalar5 * mv2.Scalar2356;
            tempScalar[41] += -mv1.KVector1.Scalar2 * mv2.Scalar1246 - mv1.KVector1.Scalar3 * mv2.Scalar1346 + mv1.KVector1.Scalar5 * mv2.Scalar1456;
            tempScalar[42] += -mv1.KVector1.Scalar1 * mv2.Scalar1246 - mv1.KVector1.Scalar3 * mv2.Scalar2346 + mv1.KVector1.Scalar5 * mv2.Scalar2456;
            tempScalar[44] += -mv1.KVector1.Scalar1 * mv2.Scalar1346 + mv1.KVector1.Scalar2 * mv2.Scalar2346 + mv1.KVector1.Scalar5 * mv2.Scalar3456;
            tempScalar[49] += -mv1.KVector1.Scalar2 * mv2.Scalar1256 - mv1.KVector1.Scalar3 * mv2.Scalar1356 - mv1.KVector1.Scalar4 * mv2.Scalar1456;
            tempScalar[50] += -mv1.KVector1.Scalar1 * mv2.Scalar1256 - mv1.KVector1.Scalar3 * mv2.Scalar2356 - mv1.KVector1.Scalar4 * mv2.Scalar2456;
            tempScalar[52] += -mv1.KVector1.Scalar1 * mv2.Scalar1356 + mv1.KVector1.Scalar2 * mv2.Scalar2356 - mv1.KVector1.Scalar4 * mv2.Scalar3456;
            tempScalar[56] += -mv1.KVector1.Scalar1 * mv2.Scalar1456 + mv1.KVector1.Scalar2 * mv2.Scalar2456 + mv1.KVector1.Scalar3 * mv2.Scalar3456;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += -mv1.KVector2.Scalar34 * mv2.Scalar1234 - mv1.KVector2.Scalar35 * mv2.Scalar1235 - mv1.KVector2.Scalar45 * mv2.Scalar1245 - mv1.KVector2.Scalar36 * mv2.Scalar1236 - mv1.KVector2.Scalar46 * mv2.Scalar1246 - mv1.KVector2.Scalar56 * mv2.Scalar1256;
            tempScalar[5] += mv1.KVector2.Scalar24 * mv2.Scalar1234 + mv1.KVector2.Scalar25 * mv2.Scalar1235 - mv1.KVector2.Scalar45 * mv2.Scalar1345 + mv1.KVector2.Scalar26 * mv2.Scalar1236 - mv1.KVector2.Scalar46 * mv2.Scalar1346 - mv1.KVector2.Scalar56 * mv2.Scalar1356;
            tempScalar[6] += mv1.KVector2.Scalar14 * mv2.Scalar1234 + mv1.KVector2.Scalar15 * mv2.Scalar1235 - mv1.KVector2.Scalar45 * mv2.Scalar2345 + mv1.KVector2.Scalar16 * mv2.Scalar1236 - mv1.KVector2.Scalar46 * mv2.Scalar2346 - mv1.KVector2.Scalar56 * mv2.Scalar2356;
            tempScalar[9] += -mv1.KVector2.Scalar23 * mv2.Scalar1234 + mv1.KVector2.Scalar25 * mv2.Scalar1245 + mv1.KVector2.Scalar35 * mv2.Scalar1345 + mv1.KVector2.Scalar26 * mv2.Scalar1246 + mv1.KVector2.Scalar36 * mv2.Scalar1346 - mv1.KVector2.Scalar56 * mv2.Scalar1456;
            tempScalar[10] += -mv1.KVector2.Scalar13 * mv2.Scalar1234 + mv1.KVector2.Scalar15 * mv2.Scalar1245 + mv1.KVector2.Scalar35 * mv2.Scalar2345 + mv1.KVector2.Scalar16 * mv2.Scalar1246 + mv1.KVector2.Scalar36 * mv2.Scalar2346 - mv1.KVector2.Scalar56 * mv2.Scalar2456;
            tempScalar[12] += mv1.KVector2.Scalar12 * mv2.Scalar1234 + mv1.KVector2.Scalar15 * mv2.Scalar1345 - mv1.KVector2.Scalar25 * mv2.Scalar2345 + mv1.KVector2.Scalar16 * mv2.Scalar1346 - mv1.KVector2.Scalar26 * mv2.Scalar2346 - mv1.KVector2.Scalar56 * mv2.Scalar3456;
            tempScalar[17] += -mv1.KVector2.Scalar23 * mv2.Scalar1235 - mv1.KVector2.Scalar24 * mv2.Scalar1245 - mv1.KVector2.Scalar34 * mv2.Scalar1345 + mv1.KVector2.Scalar26 * mv2.Scalar1256 + mv1.KVector2.Scalar36 * mv2.Scalar1356 + mv1.KVector2.Scalar46 * mv2.Scalar1456;
            tempScalar[18] += -mv1.KVector2.Scalar13 * mv2.Scalar1235 - mv1.KVector2.Scalar14 * mv2.Scalar1245 - mv1.KVector2.Scalar34 * mv2.Scalar2345 + mv1.KVector2.Scalar16 * mv2.Scalar1256 + mv1.KVector2.Scalar36 * mv2.Scalar2356 + mv1.KVector2.Scalar46 * mv2.Scalar2456;
            tempScalar[20] += mv1.KVector2.Scalar12 * mv2.Scalar1235 - mv1.KVector2.Scalar14 * mv2.Scalar1345 + mv1.KVector2.Scalar24 * mv2.Scalar2345 + mv1.KVector2.Scalar16 * mv2.Scalar1356 - mv1.KVector2.Scalar26 * mv2.Scalar2356 + mv1.KVector2.Scalar46 * mv2.Scalar3456;
            tempScalar[24] += mv1.KVector2.Scalar12 * mv2.Scalar1245 + mv1.KVector2.Scalar13 * mv2.Scalar1345 - mv1.KVector2.Scalar23 * mv2.Scalar2345 + mv1.KVector2.Scalar16 * mv2.Scalar1456 - mv1.KVector2.Scalar26 * mv2.Scalar2456 - mv1.KVector2.Scalar36 * mv2.Scalar3456;
            tempScalar[33] += -mv1.KVector2.Scalar23 * mv2.Scalar1236 - mv1.KVector2.Scalar24 * mv2.Scalar1246 - mv1.KVector2.Scalar34 * mv2.Scalar1346 - mv1.KVector2.Scalar25 * mv2.Scalar1256 - mv1.KVector2.Scalar35 * mv2.Scalar1356 - mv1.KVector2.Scalar45 * mv2.Scalar1456;
            tempScalar[34] += -mv1.KVector2.Scalar13 * mv2.Scalar1236 - mv1.KVector2.Scalar14 * mv2.Scalar1246 - mv1.KVector2.Scalar34 * mv2.Scalar2346 - mv1.KVector2.Scalar15 * mv2.Scalar1256 - mv1.KVector2.Scalar35 * mv2.Scalar2356 - mv1.KVector2.Scalar45 * mv2.Scalar2456;
            tempScalar[36] += mv1.KVector2.Scalar12 * mv2.Scalar1236 - mv1.KVector2.Scalar14 * mv2.Scalar1346 + mv1.KVector2.Scalar24 * mv2.Scalar2346 - mv1.KVector2.Scalar15 * mv2.Scalar1356 + mv1.KVector2.Scalar25 * mv2.Scalar2356 - mv1.KVector2.Scalar45 * mv2.Scalar3456;
            tempScalar[40] += mv1.KVector2.Scalar12 * mv2.Scalar1246 + mv1.KVector2.Scalar13 * mv2.Scalar1346 - mv1.KVector2.Scalar23 * mv2.Scalar2346 - mv1.KVector2.Scalar15 * mv2.Scalar1456 + mv1.KVector2.Scalar25 * mv2.Scalar2456 + mv1.KVector2.Scalar35 * mv2.Scalar3456;
            tempScalar[48] += mv1.KVector2.Scalar12 * mv2.Scalar1256 + mv1.KVector2.Scalar13 * mv2.Scalar1356 - mv1.KVector2.Scalar23 * mv2.Scalar2356 + mv1.KVector2.Scalar14 * mv2.Scalar1456 - mv1.KVector2.Scalar24 * mv2.Scalar2456 - mv1.KVector2.Scalar34 * mv2.Scalar3456;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[1] += mv1.KVector3.Scalar234 * mv2.Scalar1234 + mv1.KVector3.Scalar235 * mv2.Scalar1235 + mv1.KVector3.Scalar245 * mv2.Scalar1245 + mv1.KVector3.Scalar345 * mv2.Scalar1345 + mv1.KVector3.Scalar236 * mv2.Scalar1236 + mv1.KVector3.Scalar246 * mv2.Scalar1246 + mv1.KVector3.Scalar346 * mv2.Scalar1346 + mv1.KVector3.Scalar256 * mv2.Scalar1256 + mv1.KVector3.Scalar356 * mv2.Scalar1356 + mv1.KVector3.Scalar456 * mv2.Scalar1456;
            tempScalar[2] += mv1.KVector3.Scalar134 * mv2.Scalar1234 + mv1.KVector3.Scalar135 * mv2.Scalar1235 + mv1.KVector3.Scalar145 * mv2.Scalar1245 + mv1.KVector3.Scalar345 * mv2.Scalar2345 + mv1.KVector3.Scalar136 * mv2.Scalar1236 + mv1.KVector3.Scalar146 * mv2.Scalar1246 + mv1.KVector3.Scalar346 * mv2.Scalar2346 + mv1.KVector3.Scalar156 * mv2.Scalar1256 + mv1.KVector3.Scalar356 * mv2.Scalar2356 + mv1.KVector3.Scalar456 * mv2.Scalar2456;
            tempScalar[4] += -mv1.KVector3.Scalar124 * mv2.Scalar1234 - mv1.KVector3.Scalar125 * mv2.Scalar1235 + mv1.KVector3.Scalar145 * mv2.Scalar1345 - mv1.KVector3.Scalar245 * mv2.Scalar2345 - mv1.KVector3.Scalar126 * mv2.Scalar1236 + mv1.KVector3.Scalar146 * mv2.Scalar1346 - mv1.KVector3.Scalar246 * mv2.Scalar2346 + mv1.KVector3.Scalar156 * mv2.Scalar1356 - mv1.KVector3.Scalar256 * mv2.Scalar2356 + mv1.KVector3.Scalar456 * mv2.Scalar3456;
            tempScalar[8] += mv1.KVector3.Scalar123 * mv2.Scalar1234 - mv1.KVector3.Scalar125 * mv2.Scalar1245 - mv1.KVector3.Scalar135 * mv2.Scalar1345 + mv1.KVector3.Scalar235 * mv2.Scalar2345 - mv1.KVector3.Scalar126 * mv2.Scalar1246 - mv1.KVector3.Scalar136 * mv2.Scalar1346 + mv1.KVector3.Scalar236 * mv2.Scalar2346 + mv1.KVector3.Scalar156 * mv2.Scalar1456 - mv1.KVector3.Scalar256 * mv2.Scalar2456 - mv1.KVector3.Scalar356 * mv2.Scalar3456;
            tempScalar[16] += mv1.KVector3.Scalar123 * mv2.Scalar1235 + mv1.KVector3.Scalar124 * mv2.Scalar1245 + mv1.KVector3.Scalar134 * mv2.Scalar1345 - mv1.KVector3.Scalar234 * mv2.Scalar2345 - mv1.KVector3.Scalar126 * mv2.Scalar1256 - mv1.KVector3.Scalar136 * mv2.Scalar1356 + mv1.KVector3.Scalar236 * mv2.Scalar2356 - mv1.KVector3.Scalar146 * mv2.Scalar1456 + mv1.KVector3.Scalar246 * mv2.Scalar2456 + mv1.KVector3.Scalar346 * mv2.Scalar3456;
            tempScalar[32] += mv1.KVector3.Scalar123 * mv2.Scalar1236 + mv1.KVector3.Scalar124 * mv2.Scalar1246 + mv1.KVector3.Scalar134 * mv2.Scalar1346 - mv1.KVector3.Scalar234 * mv2.Scalar2346 + mv1.KVector3.Scalar125 * mv2.Scalar1256 + mv1.KVector3.Scalar135 * mv2.Scalar1356 - mv1.KVector3.Scalar235 * mv2.Scalar2356 + mv1.KVector3.Scalar145 * mv2.Scalar1456 - mv1.KVector3.Scalar245 * mv2.Scalar2456 - mv1.KVector3.Scalar345 * mv2.Scalar3456;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[0] += -mv1.KVector4.Scalar1234 * mv2.Scalar1234 - mv1.KVector4.Scalar1235 * mv2.Scalar1235 - mv1.KVector4.Scalar1245 * mv2.Scalar1245 - mv1.KVector4.Scalar1345 * mv2.Scalar1345 + mv1.KVector4.Scalar2345 * mv2.Scalar2345 - mv1.KVector4.Scalar1236 * mv2.Scalar1236 - mv1.KVector4.Scalar1246 * mv2.Scalar1246 - mv1.KVector4.Scalar1346 * mv2.Scalar1346 + mv1.KVector4.Scalar2346 * mv2.Scalar2346 - mv1.KVector4.Scalar1256 * mv2.Scalar1256 - mv1.KVector4.Scalar1356 * mv2.Scalar1356 + mv1.KVector4.Scalar2356 * mv2.Scalar2356 - mv1.KVector4.Scalar1456 * mv2.Scalar1456 + mv1.KVector4.Scalar2456 * mv2.Scalar2456 + mv1.KVector4.Scalar3456 * mv2.Scalar3456;
        }
        
        return Ga51Multivector.Create(tempScalar);
    }
    
    public static Ga51Multivector Lcp(this Ga51Multivector mv1, Ga51KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[31] += mv1.KVector0.Scalar * mv2.Scalar12345;
            tempScalar[47] += mv1.KVector0.Scalar * mv2.Scalar12346;
            tempScalar[55] += mv1.KVector0.Scalar * mv2.Scalar12356;
            tempScalar[59] += mv1.KVector0.Scalar * mv2.Scalar12456;
            tempScalar[61] += mv1.KVector0.Scalar * mv2.Scalar13456;
            tempScalar[62] += mv1.KVector0.Scalar * mv2.Scalar23456;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[15] += mv1.KVector1.Scalar5 * mv2.Scalar12345 + mv1.KVector1.Scalar6 * mv2.Scalar12346;
            tempScalar[23] += -mv1.KVector1.Scalar4 * mv2.Scalar12345 + mv1.KVector1.Scalar6 * mv2.Scalar12356;
            tempScalar[27] += mv1.KVector1.Scalar3 * mv2.Scalar12345 + mv1.KVector1.Scalar6 * mv2.Scalar12456;
            tempScalar[29] += -mv1.KVector1.Scalar2 * mv2.Scalar12345 + mv1.KVector1.Scalar6 * mv2.Scalar13456;
            tempScalar[30] += -mv1.KVector1.Scalar1 * mv2.Scalar12345 + mv1.KVector1.Scalar6 * mv2.Scalar23456;
            tempScalar[39] += -mv1.KVector1.Scalar4 * mv2.Scalar12346 - mv1.KVector1.Scalar5 * mv2.Scalar12356;
            tempScalar[43] += mv1.KVector1.Scalar3 * mv2.Scalar12346 - mv1.KVector1.Scalar5 * mv2.Scalar12456;
            tempScalar[45] += -mv1.KVector1.Scalar2 * mv2.Scalar12346 - mv1.KVector1.Scalar5 * mv2.Scalar13456;
            tempScalar[46] += -mv1.KVector1.Scalar1 * mv2.Scalar12346 - mv1.KVector1.Scalar5 * mv2.Scalar23456;
            tempScalar[51] += mv1.KVector1.Scalar3 * mv2.Scalar12356 + mv1.KVector1.Scalar4 * mv2.Scalar12456;
            tempScalar[53] += -mv1.KVector1.Scalar2 * mv2.Scalar12356 + mv1.KVector1.Scalar4 * mv2.Scalar13456;
            tempScalar[54] += -mv1.KVector1.Scalar1 * mv2.Scalar12356 + mv1.KVector1.Scalar4 * mv2.Scalar23456;
            tempScalar[57] += -mv1.KVector1.Scalar2 * mv2.Scalar12456 - mv1.KVector1.Scalar3 * mv2.Scalar13456;
            tempScalar[58] += -mv1.KVector1.Scalar1 * mv2.Scalar12456 - mv1.KVector1.Scalar3 * mv2.Scalar23456;
            tempScalar[60] += -mv1.KVector1.Scalar1 * mv2.Scalar13456 + mv1.KVector1.Scalar2 * mv2.Scalar23456;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[7] += -mv1.KVector2.Scalar45 * mv2.Scalar12345 - mv1.KVector2.Scalar46 * mv2.Scalar12346 - mv1.KVector2.Scalar56 * mv2.Scalar12356;
            tempScalar[11] += mv1.KVector2.Scalar35 * mv2.Scalar12345 + mv1.KVector2.Scalar36 * mv2.Scalar12346 - mv1.KVector2.Scalar56 * mv2.Scalar12456;
            tempScalar[13] += -mv1.KVector2.Scalar25 * mv2.Scalar12345 - mv1.KVector2.Scalar26 * mv2.Scalar12346 - mv1.KVector2.Scalar56 * mv2.Scalar13456;
            tempScalar[14] += -mv1.KVector2.Scalar15 * mv2.Scalar12345 - mv1.KVector2.Scalar16 * mv2.Scalar12346 - mv1.KVector2.Scalar56 * mv2.Scalar23456;
            tempScalar[19] += -mv1.KVector2.Scalar34 * mv2.Scalar12345 + mv1.KVector2.Scalar36 * mv2.Scalar12356 + mv1.KVector2.Scalar46 * mv2.Scalar12456;
            tempScalar[21] += mv1.KVector2.Scalar24 * mv2.Scalar12345 - mv1.KVector2.Scalar26 * mv2.Scalar12356 + mv1.KVector2.Scalar46 * mv2.Scalar13456;
            tempScalar[22] += mv1.KVector2.Scalar14 * mv2.Scalar12345 - mv1.KVector2.Scalar16 * mv2.Scalar12356 + mv1.KVector2.Scalar46 * mv2.Scalar23456;
            tempScalar[25] += -mv1.KVector2.Scalar23 * mv2.Scalar12345 - mv1.KVector2.Scalar26 * mv2.Scalar12456 - mv1.KVector2.Scalar36 * mv2.Scalar13456;
            tempScalar[26] += -mv1.KVector2.Scalar13 * mv2.Scalar12345 - mv1.KVector2.Scalar16 * mv2.Scalar12456 - mv1.KVector2.Scalar36 * mv2.Scalar23456;
            tempScalar[28] += mv1.KVector2.Scalar12 * mv2.Scalar12345 - mv1.KVector2.Scalar16 * mv2.Scalar13456 + mv1.KVector2.Scalar26 * mv2.Scalar23456;
            tempScalar[35] += -mv1.KVector2.Scalar34 * mv2.Scalar12346 - mv1.KVector2.Scalar35 * mv2.Scalar12356 - mv1.KVector2.Scalar45 * mv2.Scalar12456;
            tempScalar[37] += mv1.KVector2.Scalar24 * mv2.Scalar12346 + mv1.KVector2.Scalar25 * mv2.Scalar12356 - mv1.KVector2.Scalar45 * mv2.Scalar13456;
            tempScalar[38] += mv1.KVector2.Scalar14 * mv2.Scalar12346 + mv1.KVector2.Scalar15 * mv2.Scalar12356 - mv1.KVector2.Scalar45 * mv2.Scalar23456;
            tempScalar[41] += -mv1.KVector2.Scalar23 * mv2.Scalar12346 + mv1.KVector2.Scalar25 * mv2.Scalar12456 + mv1.KVector2.Scalar35 * mv2.Scalar13456;
            tempScalar[42] += -mv1.KVector2.Scalar13 * mv2.Scalar12346 + mv1.KVector2.Scalar15 * mv2.Scalar12456 + mv1.KVector2.Scalar35 * mv2.Scalar23456;
            tempScalar[44] += mv1.KVector2.Scalar12 * mv2.Scalar12346 + mv1.KVector2.Scalar15 * mv2.Scalar13456 - mv1.KVector2.Scalar25 * mv2.Scalar23456;
            tempScalar[49] += -mv1.KVector2.Scalar23 * mv2.Scalar12356 - mv1.KVector2.Scalar24 * mv2.Scalar12456 - mv1.KVector2.Scalar34 * mv2.Scalar13456;
            tempScalar[50] += -mv1.KVector2.Scalar13 * mv2.Scalar12356 - mv1.KVector2.Scalar14 * mv2.Scalar12456 - mv1.KVector2.Scalar34 * mv2.Scalar23456;
            tempScalar[52] += mv1.KVector2.Scalar12 * mv2.Scalar12356 - mv1.KVector2.Scalar14 * mv2.Scalar13456 + mv1.KVector2.Scalar24 * mv2.Scalar23456;
            tempScalar[56] += mv1.KVector2.Scalar12 * mv2.Scalar12456 + mv1.KVector2.Scalar13 * mv2.Scalar13456 - mv1.KVector2.Scalar23 * mv2.Scalar23456;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[3] += -mv1.KVector3.Scalar345 * mv2.Scalar12345 - mv1.KVector3.Scalar346 * mv2.Scalar12346 - mv1.KVector3.Scalar356 * mv2.Scalar12356 - mv1.KVector3.Scalar456 * mv2.Scalar12456;
            tempScalar[5] += mv1.KVector3.Scalar245 * mv2.Scalar12345 + mv1.KVector3.Scalar246 * mv2.Scalar12346 + mv1.KVector3.Scalar256 * mv2.Scalar12356 - mv1.KVector3.Scalar456 * mv2.Scalar13456;
            tempScalar[6] += mv1.KVector3.Scalar145 * mv2.Scalar12345 + mv1.KVector3.Scalar146 * mv2.Scalar12346 + mv1.KVector3.Scalar156 * mv2.Scalar12356 - mv1.KVector3.Scalar456 * mv2.Scalar23456;
            tempScalar[9] += -mv1.KVector3.Scalar235 * mv2.Scalar12345 - mv1.KVector3.Scalar236 * mv2.Scalar12346 + mv1.KVector3.Scalar256 * mv2.Scalar12456 + mv1.KVector3.Scalar356 * mv2.Scalar13456;
            tempScalar[10] += -mv1.KVector3.Scalar135 * mv2.Scalar12345 - mv1.KVector3.Scalar136 * mv2.Scalar12346 + mv1.KVector3.Scalar156 * mv2.Scalar12456 + mv1.KVector3.Scalar356 * mv2.Scalar23456;
            tempScalar[12] += mv1.KVector3.Scalar125 * mv2.Scalar12345 + mv1.KVector3.Scalar126 * mv2.Scalar12346 + mv1.KVector3.Scalar156 * mv2.Scalar13456 - mv1.KVector3.Scalar256 * mv2.Scalar23456;
            tempScalar[17] += mv1.KVector3.Scalar234 * mv2.Scalar12345 - mv1.KVector3.Scalar236 * mv2.Scalar12356 - mv1.KVector3.Scalar246 * mv2.Scalar12456 - mv1.KVector3.Scalar346 * mv2.Scalar13456;
            tempScalar[18] += mv1.KVector3.Scalar134 * mv2.Scalar12345 - mv1.KVector3.Scalar136 * mv2.Scalar12356 - mv1.KVector3.Scalar146 * mv2.Scalar12456 - mv1.KVector3.Scalar346 * mv2.Scalar23456;
            tempScalar[20] += -mv1.KVector3.Scalar124 * mv2.Scalar12345 + mv1.KVector3.Scalar126 * mv2.Scalar12356 - mv1.KVector3.Scalar146 * mv2.Scalar13456 + mv1.KVector3.Scalar246 * mv2.Scalar23456;
            tempScalar[24] += mv1.KVector3.Scalar123 * mv2.Scalar12345 + mv1.KVector3.Scalar126 * mv2.Scalar12456 + mv1.KVector3.Scalar136 * mv2.Scalar13456 - mv1.KVector3.Scalar236 * mv2.Scalar23456;
            tempScalar[33] += mv1.KVector3.Scalar234 * mv2.Scalar12346 + mv1.KVector3.Scalar235 * mv2.Scalar12356 + mv1.KVector3.Scalar245 * mv2.Scalar12456 + mv1.KVector3.Scalar345 * mv2.Scalar13456;
            tempScalar[34] += mv1.KVector3.Scalar134 * mv2.Scalar12346 + mv1.KVector3.Scalar135 * mv2.Scalar12356 + mv1.KVector3.Scalar145 * mv2.Scalar12456 + mv1.KVector3.Scalar345 * mv2.Scalar23456;
            tempScalar[36] += -mv1.KVector3.Scalar124 * mv2.Scalar12346 - mv1.KVector3.Scalar125 * mv2.Scalar12356 + mv1.KVector3.Scalar145 * mv2.Scalar13456 - mv1.KVector3.Scalar245 * mv2.Scalar23456;
            tempScalar[40] += mv1.KVector3.Scalar123 * mv2.Scalar12346 - mv1.KVector3.Scalar125 * mv2.Scalar12456 - mv1.KVector3.Scalar135 * mv2.Scalar13456 + mv1.KVector3.Scalar235 * mv2.Scalar23456;
            tempScalar[48] += mv1.KVector3.Scalar123 * mv2.Scalar12356 + mv1.KVector3.Scalar124 * mv2.Scalar12456 + mv1.KVector3.Scalar134 * mv2.Scalar13456 - mv1.KVector3.Scalar234 * mv2.Scalar23456;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[1] += mv1.KVector4.Scalar2345 * mv2.Scalar12345 + mv1.KVector4.Scalar2346 * mv2.Scalar12346 + mv1.KVector4.Scalar2356 * mv2.Scalar12356 + mv1.KVector4.Scalar2456 * mv2.Scalar12456 + mv1.KVector4.Scalar3456 * mv2.Scalar13456;
            tempScalar[2] += mv1.KVector4.Scalar1345 * mv2.Scalar12345 + mv1.KVector4.Scalar1346 * mv2.Scalar12346 + mv1.KVector4.Scalar1356 * mv2.Scalar12356 + mv1.KVector4.Scalar1456 * mv2.Scalar12456 + mv1.KVector4.Scalar3456 * mv2.Scalar23456;
            tempScalar[4] += -mv1.KVector4.Scalar1245 * mv2.Scalar12345 - mv1.KVector4.Scalar1246 * mv2.Scalar12346 - mv1.KVector4.Scalar1256 * mv2.Scalar12356 + mv1.KVector4.Scalar1456 * mv2.Scalar13456 - mv1.KVector4.Scalar2456 * mv2.Scalar23456;
            tempScalar[8] += mv1.KVector4.Scalar1235 * mv2.Scalar12345 + mv1.KVector4.Scalar1236 * mv2.Scalar12346 - mv1.KVector4.Scalar1256 * mv2.Scalar12456 - mv1.KVector4.Scalar1356 * mv2.Scalar13456 + mv1.KVector4.Scalar2356 * mv2.Scalar23456;
            tempScalar[16] += -mv1.KVector4.Scalar1234 * mv2.Scalar12345 + mv1.KVector4.Scalar1236 * mv2.Scalar12356 + mv1.KVector4.Scalar1246 * mv2.Scalar12456 + mv1.KVector4.Scalar1346 * mv2.Scalar13456 - mv1.KVector4.Scalar2346 * mv2.Scalar23456;
            tempScalar[32] += -mv1.KVector4.Scalar1234 * mv2.Scalar12346 - mv1.KVector4.Scalar1235 * mv2.Scalar12356 - mv1.KVector4.Scalar1245 * mv2.Scalar12456 - mv1.KVector4.Scalar1345 * mv2.Scalar13456 + mv1.KVector4.Scalar2345 * mv2.Scalar23456;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[0] += -mv1.KVector5.Scalar12345 * mv2.Scalar12345 - mv1.KVector5.Scalar12346 * mv2.Scalar12346 - mv1.KVector5.Scalar12356 * mv2.Scalar12356 - mv1.KVector5.Scalar12456 * mv2.Scalar12456 - mv1.KVector5.Scalar13456 * mv2.Scalar13456 + mv1.KVector5.Scalar23456 * mv2.Scalar23456;
        }
        
        return Ga51Multivector.Create(tempScalar);
    }
    
    public static Ga51Multivector Lcp(this Ga51Multivector mv1, Ga51KVector6 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv1.KVector0.IsZero())
        {
            tempScalar[63] += mv1.KVector0.Scalar * mv2.Scalar123456;
        }
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[31] += -mv1.KVector1.Scalar6 * mv2.Scalar123456;
            tempScalar[47] += mv1.KVector1.Scalar5 * mv2.Scalar123456;
            tempScalar[55] += -mv1.KVector1.Scalar4 * mv2.Scalar123456;
            tempScalar[59] += mv1.KVector1.Scalar3 * mv2.Scalar123456;
            tempScalar[61] += -mv1.KVector1.Scalar2 * mv2.Scalar123456;
            tempScalar[62] += -mv1.KVector1.Scalar1 * mv2.Scalar123456;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[15] += -mv1.KVector2.Scalar56 * mv2.Scalar123456;
            tempScalar[23] += mv1.KVector2.Scalar46 * mv2.Scalar123456;
            tempScalar[27] += -mv1.KVector2.Scalar36 * mv2.Scalar123456;
            tempScalar[29] += mv1.KVector2.Scalar26 * mv2.Scalar123456;
            tempScalar[30] += mv1.KVector2.Scalar16 * mv2.Scalar123456;
            tempScalar[39] += -mv1.KVector2.Scalar45 * mv2.Scalar123456;
            tempScalar[43] += mv1.KVector2.Scalar35 * mv2.Scalar123456;
            tempScalar[45] += -mv1.KVector2.Scalar25 * mv2.Scalar123456;
            tempScalar[46] += -mv1.KVector2.Scalar15 * mv2.Scalar123456;
            tempScalar[51] += -mv1.KVector2.Scalar34 * mv2.Scalar123456;
            tempScalar[53] += mv1.KVector2.Scalar24 * mv2.Scalar123456;
            tempScalar[54] += mv1.KVector2.Scalar14 * mv2.Scalar123456;
            tempScalar[57] += -mv1.KVector2.Scalar23 * mv2.Scalar123456;
            tempScalar[58] += -mv1.KVector2.Scalar13 * mv2.Scalar123456;
            tempScalar[60] += mv1.KVector2.Scalar12 * mv2.Scalar123456;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += mv1.KVector3.Scalar456 * mv2.Scalar123456;
            tempScalar[11] += -mv1.KVector3.Scalar356 * mv2.Scalar123456;
            tempScalar[13] += mv1.KVector3.Scalar256 * mv2.Scalar123456;
            tempScalar[14] += mv1.KVector3.Scalar156 * mv2.Scalar123456;
            tempScalar[19] += mv1.KVector3.Scalar346 * mv2.Scalar123456;
            tempScalar[21] += -mv1.KVector3.Scalar246 * mv2.Scalar123456;
            tempScalar[22] += -mv1.KVector3.Scalar146 * mv2.Scalar123456;
            tempScalar[25] += mv1.KVector3.Scalar236 * mv2.Scalar123456;
            tempScalar[26] += mv1.KVector3.Scalar136 * mv2.Scalar123456;
            tempScalar[28] += -mv1.KVector3.Scalar126 * mv2.Scalar123456;
            tempScalar[35] += -mv1.KVector3.Scalar345 * mv2.Scalar123456;
            tempScalar[37] += mv1.KVector3.Scalar245 * mv2.Scalar123456;
            tempScalar[38] += mv1.KVector3.Scalar145 * mv2.Scalar123456;
            tempScalar[41] += -mv1.KVector3.Scalar235 * mv2.Scalar123456;
            tempScalar[42] += -mv1.KVector3.Scalar135 * mv2.Scalar123456;
            tempScalar[44] += mv1.KVector3.Scalar125 * mv2.Scalar123456;
            tempScalar[49] += mv1.KVector3.Scalar234 * mv2.Scalar123456;
            tempScalar[50] += mv1.KVector3.Scalar134 * mv2.Scalar123456;
            tempScalar[52] += -mv1.KVector3.Scalar124 * mv2.Scalar123456;
            tempScalar[56] += mv1.KVector3.Scalar123 * mv2.Scalar123456;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[3] += mv1.KVector4.Scalar3456 * mv2.Scalar123456;
            tempScalar[5] += -mv1.KVector4.Scalar2456 * mv2.Scalar123456;
            tempScalar[6] += -mv1.KVector4.Scalar1456 * mv2.Scalar123456;
            tempScalar[9] += mv1.KVector4.Scalar2356 * mv2.Scalar123456;
            tempScalar[10] += mv1.KVector4.Scalar1356 * mv2.Scalar123456;
            tempScalar[12] += -mv1.KVector4.Scalar1256 * mv2.Scalar123456;
            tempScalar[17] += -mv1.KVector4.Scalar2346 * mv2.Scalar123456;
            tempScalar[18] += -mv1.KVector4.Scalar1346 * mv2.Scalar123456;
            tempScalar[20] += mv1.KVector4.Scalar1246 * mv2.Scalar123456;
            tempScalar[24] += -mv1.KVector4.Scalar1236 * mv2.Scalar123456;
            tempScalar[33] += mv1.KVector4.Scalar2345 * mv2.Scalar123456;
            tempScalar[34] += mv1.KVector4.Scalar1345 * mv2.Scalar123456;
            tempScalar[36] += -mv1.KVector4.Scalar1245 * mv2.Scalar123456;
            tempScalar[40] += mv1.KVector4.Scalar1235 * mv2.Scalar123456;
            tempScalar[48] += -mv1.KVector4.Scalar1234 * mv2.Scalar123456;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[1] += -mv1.KVector5.Scalar23456 * mv2.Scalar123456;
            tempScalar[2] += -mv1.KVector5.Scalar13456 * mv2.Scalar123456;
            tempScalar[4] += mv1.KVector5.Scalar12456 * mv2.Scalar123456;
            tempScalar[8] += -mv1.KVector5.Scalar12356 * mv2.Scalar123456;
            tempScalar[16] += mv1.KVector5.Scalar12346 * mv2.Scalar123456;
            tempScalar[32] += -mv1.KVector5.Scalar12345 * mv2.Scalar123456;
        }
        
        if (!mv1.KVector6.IsZero())
        {
            tempScalar[0] += mv1.KVector6.Scalar123456 * mv2.Scalar123456;
        }
        
        return Ga51Multivector.Create(tempScalar);
    }
    
    public static Ga51Multivector Lcp(this Ga51Multivector mv1, Ga51Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga51Multivector.Zero;
        
        var tempScalar = new double[64];
        
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
                tempScalar[32] += mv1.KVector0.Scalar * mv2.KVector1.Scalar6;
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
                tempScalar[33] += mv1.KVector0.Scalar * mv2.KVector2.Scalar16;
                tempScalar[34] += mv1.KVector0.Scalar * mv2.KVector2.Scalar26;
                tempScalar[36] += mv1.KVector0.Scalar * mv2.KVector2.Scalar36;
                tempScalar[40] += mv1.KVector0.Scalar * mv2.KVector2.Scalar46;
                tempScalar[48] += mv1.KVector0.Scalar * mv2.KVector2.Scalar56;
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
                tempScalar[35] += mv1.KVector0.Scalar * mv2.KVector3.Scalar126;
                tempScalar[37] += mv1.KVector0.Scalar * mv2.KVector3.Scalar136;
                tempScalar[38] += mv1.KVector0.Scalar * mv2.KVector3.Scalar236;
                tempScalar[41] += mv1.KVector0.Scalar * mv2.KVector3.Scalar146;
                tempScalar[42] += mv1.KVector0.Scalar * mv2.KVector3.Scalar246;
                tempScalar[44] += mv1.KVector0.Scalar * mv2.KVector3.Scalar346;
                tempScalar[49] += mv1.KVector0.Scalar * mv2.KVector3.Scalar156;
                tempScalar[50] += mv1.KVector0.Scalar * mv2.KVector3.Scalar256;
                tempScalar[52] += mv1.KVector0.Scalar * mv2.KVector3.Scalar356;
                tempScalar[56] += mv1.KVector0.Scalar * mv2.KVector3.Scalar456;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[15] += mv1.KVector0.Scalar * mv2.KVector4.Scalar1234;
                tempScalar[23] += mv1.KVector0.Scalar * mv2.KVector4.Scalar1235;
                tempScalar[27] += mv1.KVector0.Scalar * mv2.KVector4.Scalar1245;
                tempScalar[29] += mv1.KVector0.Scalar * mv2.KVector4.Scalar1345;
                tempScalar[30] += mv1.KVector0.Scalar * mv2.KVector4.Scalar2345;
                tempScalar[39] += mv1.KVector0.Scalar * mv2.KVector4.Scalar1236;
                tempScalar[43] += mv1.KVector0.Scalar * mv2.KVector4.Scalar1246;
                tempScalar[45] += mv1.KVector0.Scalar * mv2.KVector4.Scalar1346;
                tempScalar[46] += mv1.KVector0.Scalar * mv2.KVector4.Scalar2346;
                tempScalar[51] += mv1.KVector0.Scalar * mv2.KVector4.Scalar1256;
                tempScalar[53] += mv1.KVector0.Scalar * mv2.KVector4.Scalar1356;
                tempScalar[54] += mv1.KVector0.Scalar * mv2.KVector4.Scalar2356;
                tempScalar[57] += mv1.KVector0.Scalar * mv2.KVector4.Scalar1456;
                tempScalar[58] += mv1.KVector0.Scalar * mv2.KVector4.Scalar2456;
                tempScalar[60] += mv1.KVector0.Scalar * mv2.KVector4.Scalar3456;
            }
            
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[31] += mv1.KVector0.Scalar * mv2.KVector5.Scalar12345;
                tempScalar[47] += mv1.KVector0.Scalar * mv2.KVector5.Scalar12346;
                tempScalar[55] += mv1.KVector0.Scalar * mv2.KVector5.Scalar12356;
                tempScalar[59] += mv1.KVector0.Scalar * mv2.KVector5.Scalar12456;
                tempScalar[61] += mv1.KVector0.Scalar * mv2.KVector5.Scalar13456;
                tempScalar[62] += mv1.KVector0.Scalar * mv2.KVector5.Scalar23456;
            }
            
            if (!mv2.KVector6.IsZero())
            {
                tempScalar[63] += mv1.KVector0.Scalar * mv2.KVector6.Scalar123456;
            }
            
        }
        
        if (!mv1.KVector1.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[0] += -mv1.KVector1.Scalar1 * mv2.KVector1.Scalar1 + mv1.KVector1.Scalar2 * mv2.KVector1.Scalar2 + mv1.KVector1.Scalar3 * mv2.KVector1.Scalar3 + mv1.KVector1.Scalar4 * mv2.KVector1.Scalar4 + mv1.KVector1.Scalar5 * mv2.KVector1.Scalar5 + mv1.KVector1.Scalar6 * mv2.KVector1.Scalar6;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[1] += -mv1.KVector1.Scalar2 * mv2.KVector2.Scalar12 - mv1.KVector1.Scalar3 * mv2.KVector2.Scalar13 - mv1.KVector1.Scalar4 * mv2.KVector2.Scalar14 - mv1.KVector1.Scalar5 * mv2.KVector2.Scalar15 - mv1.KVector1.Scalar6 * mv2.KVector2.Scalar16;
                tempScalar[2] += -mv1.KVector1.Scalar1 * mv2.KVector2.Scalar12 - mv1.KVector1.Scalar3 * mv2.KVector2.Scalar23 - mv1.KVector1.Scalar4 * mv2.KVector2.Scalar24 - mv1.KVector1.Scalar5 * mv2.KVector2.Scalar25 - mv1.KVector1.Scalar6 * mv2.KVector2.Scalar26;
                tempScalar[4] += -mv1.KVector1.Scalar1 * mv2.KVector2.Scalar13 + mv1.KVector1.Scalar2 * mv2.KVector2.Scalar23 - mv1.KVector1.Scalar4 * mv2.KVector2.Scalar34 - mv1.KVector1.Scalar5 * mv2.KVector2.Scalar35 - mv1.KVector1.Scalar6 * mv2.KVector2.Scalar36;
                tempScalar[8] += -mv1.KVector1.Scalar1 * mv2.KVector2.Scalar14 + mv1.KVector1.Scalar2 * mv2.KVector2.Scalar24 + mv1.KVector1.Scalar3 * mv2.KVector2.Scalar34 - mv1.KVector1.Scalar5 * mv2.KVector2.Scalar45 - mv1.KVector1.Scalar6 * mv2.KVector2.Scalar46;
                tempScalar[16] += -mv1.KVector1.Scalar1 * mv2.KVector2.Scalar15 + mv1.KVector1.Scalar2 * mv2.KVector2.Scalar25 + mv1.KVector1.Scalar3 * mv2.KVector2.Scalar35 + mv1.KVector1.Scalar4 * mv2.KVector2.Scalar45 - mv1.KVector1.Scalar6 * mv2.KVector2.Scalar56;
                tempScalar[32] += -mv1.KVector1.Scalar1 * mv2.KVector2.Scalar16 + mv1.KVector1.Scalar2 * mv2.KVector2.Scalar26 + mv1.KVector1.Scalar3 * mv2.KVector2.Scalar36 + mv1.KVector1.Scalar4 * mv2.KVector2.Scalar46 + mv1.KVector1.Scalar5 * mv2.KVector2.Scalar56;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[3] += mv1.KVector1.Scalar3 * mv2.KVector3.Scalar123 + mv1.KVector1.Scalar4 * mv2.KVector3.Scalar124 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar125 + mv1.KVector1.Scalar6 * mv2.KVector3.Scalar126;
                tempScalar[5] += -mv1.KVector1.Scalar2 * mv2.KVector3.Scalar123 + mv1.KVector1.Scalar4 * mv2.KVector3.Scalar134 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar135 + mv1.KVector1.Scalar6 * mv2.KVector3.Scalar136;
                tempScalar[6] += -mv1.KVector1.Scalar1 * mv2.KVector3.Scalar123 + mv1.KVector1.Scalar4 * mv2.KVector3.Scalar234 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar235 + mv1.KVector1.Scalar6 * mv2.KVector3.Scalar236;
                tempScalar[9] += -mv1.KVector1.Scalar2 * mv2.KVector3.Scalar124 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar134 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar145 + mv1.KVector1.Scalar6 * mv2.KVector3.Scalar146;
                tempScalar[10] += -mv1.KVector1.Scalar1 * mv2.KVector3.Scalar124 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar234 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar245 + mv1.KVector1.Scalar6 * mv2.KVector3.Scalar246;
                tempScalar[12] += -mv1.KVector1.Scalar1 * mv2.KVector3.Scalar134 + mv1.KVector1.Scalar2 * mv2.KVector3.Scalar234 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar345 + mv1.KVector1.Scalar6 * mv2.KVector3.Scalar346;
                tempScalar[17] += -mv1.KVector1.Scalar2 * mv2.KVector3.Scalar125 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar135 - mv1.KVector1.Scalar4 * mv2.KVector3.Scalar145 + mv1.KVector1.Scalar6 * mv2.KVector3.Scalar156;
                tempScalar[18] += -mv1.KVector1.Scalar1 * mv2.KVector3.Scalar125 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar235 - mv1.KVector1.Scalar4 * mv2.KVector3.Scalar245 + mv1.KVector1.Scalar6 * mv2.KVector3.Scalar256;
                tempScalar[20] += -mv1.KVector1.Scalar1 * mv2.KVector3.Scalar135 + mv1.KVector1.Scalar2 * mv2.KVector3.Scalar235 - mv1.KVector1.Scalar4 * mv2.KVector3.Scalar345 + mv1.KVector1.Scalar6 * mv2.KVector3.Scalar356;
                tempScalar[24] += -mv1.KVector1.Scalar1 * mv2.KVector3.Scalar145 + mv1.KVector1.Scalar2 * mv2.KVector3.Scalar245 + mv1.KVector1.Scalar3 * mv2.KVector3.Scalar345 + mv1.KVector1.Scalar6 * mv2.KVector3.Scalar456;
                tempScalar[33] += -mv1.KVector1.Scalar2 * mv2.KVector3.Scalar126 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar136 - mv1.KVector1.Scalar4 * mv2.KVector3.Scalar146 - mv1.KVector1.Scalar5 * mv2.KVector3.Scalar156;
                tempScalar[34] += -mv1.KVector1.Scalar1 * mv2.KVector3.Scalar126 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar236 - mv1.KVector1.Scalar4 * mv2.KVector3.Scalar246 - mv1.KVector1.Scalar5 * mv2.KVector3.Scalar256;
                tempScalar[36] += -mv1.KVector1.Scalar1 * mv2.KVector3.Scalar136 + mv1.KVector1.Scalar2 * mv2.KVector3.Scalar236 - mv1.KVector1.Scalar4 * mv2.KVector3.Scalar346 - mv1.KVector1.Scalar5 * mv2.KVector3.Scalar356;
                tempScalar[40] += -mv1.KVector1.Scalar1 * mv2.KVector3.Scalar146 + mv1.KVector1.Scalar2 * mv2.KVector3.Scalar246 + mv1.KVector1.Scalar3 * mv2.KVector3.Scalar346 - mv1.KVector1.Scalar5 * mv2.KVector3.Scalar456;
                tempScalar[48] += -mv1.KVector1.Scalar1 * mv2.KVector3.Scalar156 + mv1.KVector1.Scalar2 * mv2.KVector3.Scalar256 + mv1.KVector1.Scalar3 * mv2.KVector3.Scalar356 + mv1.KVector1.Scalar4 * mv2.KVector3.Scalar456;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[7] += -mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1234 - mv1.KVector1.Scalar5 * mv2.KVector4.Scalar1235 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar1236;
                tempScalar[11] += mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1234 - mv1.KVector1.Scalar5 * mv2.KVector4.Scalar1245 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar1246;
                tempScalar[13] += -mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1234 - mv1.KVector1.Scalar5 * mv2.KVector4.Scalar1345 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar1346;
                tempScalar[14] += -mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1234 - mv1.KVector1.Scalar5 * mv2.KVector4.Scalar2345 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar2346;
                tempScalar[19] += mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1235 + mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1245 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar1256;
                tempScalar[21] += -mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1235 + mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1345 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar1356;
                tempScalar[22] += -mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1235 + mv1.KVector1.Scalar4 * mv2.KVector4.Scalar2345 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar2356;
                tempScalar[25] += -mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1245 - mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1345 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar1456;
                tempScalar[26] += -mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1245 - mv1.KVector1.Scalar3 * mv2.KVector4.Scalar2345 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar2456;
                tempScalar[28] += -mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1345 + mv1.KVector1.Scalar2 * mv2.KVector4.Scalar2345 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar3456;
                tempScalar[35] += mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1236 + mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1246 + mv1.KVector1.Scalar5 * mv2.KVector4.Scalar1256;
                tempScalar[37] += -mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1236 + mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1346 + mv1.KVector1.Scalar5 * mv2.KVector4.Scalar1356;
                tempScalar[38] += -mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1236 + mv1.KVector1.Scalar4 * mv2.KVector4.Scalar2346 + mv1.KVector1.Scalar5 * mv2.KVector4.Scalar2356;
                tempScalar[41] += -mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1246 - mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1346 + mv1.KVector1.Scalar5 * mv2.KVector4.Scalar1456;
                tempScalar[42] += -mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1246 - mv1.KVector1.Scalar3 * mv2.KVector4.Scalar2346 + mv1.KVector1.Scalar5 * mv2.KVector4.Scalar2456;
                tempScalar[44] += -mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1346 + mv1.KVector1.Scalar2 * mv2.KVector4.Scalar2346 + mv1.KVector1.Scalar5 * mv2.KVector4.Scalar3456;
                tempScalar[49] += -mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1256 - mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1356 - mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1456;
                tempScalar[50] += -mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1256 - mv1.KVector1.Scalar3 * mv2.KVector4.Scalar2356 - mv1.KVector1.Scalar4 * mv2.KVector4.Scalar2456;
                tempScalar[52] += -mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1356 + mv1.KVector1.Scalar2 * mv2.KVector4.Scalar2356 - mv1.KVector1.Scalar4 * mv2.KVector4.Scalar3456;
                tempScalar[56] += -mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1456 + mv1.KVector1.Scalar2 * mv2.KVector4.Scalar2456 + mv1.KVector1.Scalar3 * mv2.KVector4.Scalar3456;
            }
            
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[15] += mv1.KVector1.Scalar5 * mv2.KVector5.Scalar12345 + mv1.KVector1.Scalar6 * mv2.KVector5.Scalar12346;
                tempScalar[23] += -mv1.KVector1.Scalar4 * mv2.KVector5.Scalar12345 + mv1.KVector1.Scalar6 * mv2.KVector5.Scalar12356;
                tempScalar[27] += mv1.KVector1.Scalar3 * mv2.KVector5.Scalar12345 + mv1.KVector1.Scalar6 * mv2.KVector5.Scalar12456;
                tempScalar[29] += -mv1.KVector1.Scalar2 * mv2.KVector5.Scalar12345 + mv1.KVector1.Scalar6 * mv2.KVector5.Scalar13456;
                tempScalar[30] += -mv1.KVector1.Scalar1 * mv2.KVector5.Scalar12345 + mv1.KVector1.Scalar6 * mv2.KVector5.Scalar23456;
                tempScalar[39] += -mv1.KVector1.Scalar4 * mv2.KVector5.Scalar12346 - mv1.KVector1.Scalar5 * mv2.KVector5.Scalar12356;
                tempScalar[43] += mv1.KVector1.Scalar3 * mv2.KVector5.Scalar12346 - mv1.KVector1.Scalar5 * mv2.KVector5.Scalar12456;
                tempScalar[45] += -mv1.KVector1.Scalar2 * mv2.KVector5.Scalar12346 - mv1.KVector1.Scalar5 * mv2.KVector5.Scalar13456;
                tempScalar[46] += -mv1.KVector1.Scalar1 * mv2.KVector5.Scalar12346 - mv1.KVector1.Scalar5 * mv2.KVector5.Scalar23456;
                tempScalar[51] += mv1.KVector1.Scalar3 * mv2.KVector5.Scalar12356 + mv1.KVector1.Scalar4 * mv2.KVector5.Scalar12456;
                tempScalar[53] += -mv1.KVector1.Scalar2 * mv2.KVector5.Scalar12356 + mv1.KVector1.Scalar4 * mv2.KVector5.Scalar13456;
                tempScalar[54] += -mv1.KVector1.Scalar1 * mv2.KVector5.Scalar12356 + mv1.KVector1.Scalar4 * mv2.KVector5.Scalar23456;
                tempScalar[57] += -mv1.KVector1.Scalar2 * mv2.KVector5.Scalar12456 - mv1.KVector1.Scalar3 * mv2.KVector5.Scalar13456;
                tempScalar[58] += -mv1.KVector1.Scalar1 * mv2.KVector5.Scalar12456 - mv1.KVector1.Scalar3 * mv2.KVector5.Scalar23456;
                tempScalar[60] += -mv1.KVector1.Scalar1 * mv2.KVector5.Scalar13456 + mv1.KVector1.Scalar2 * mv2.KVector5.Scalar23456;
            }
            
            if (!mv2.KVector6.IsZero())
            {
                tempScalar[31] += -mv1.KVector1.Scalar6 * mv2.KVector6.Scalar123456;
                tempScalar[47] += mv1.KVector1.Scalar5 * mv2.KVector6.Scalar123456;
                tempScalar[55] += -mv1.KVector1.Scalar4 * mv2.KVector6.Scalar123456;
                tempScalar[59] += mv1.KVector1.Scalar3 * mv2.KVector6.Scalar123456;
                tempScalar[61] += -mv1.KVector1.Scalar2 * mv2.KVector6.Scalar123456;
                tempScalar[62] += -mv1.KVector1.Scalar1 * mv2.KVector6.Scalar123456;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[0] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar12 + mv1.KVector2.Scalar13 * mv2.KVector2.Scalar13 - mv1.KVector2.Scalar23 * mv2.KVector2.Scalar23 + mv1.KVector2.Scalar14 * mv2.KVector2.Scalar14 - mv1.KVector2.Scalar24 * mv2.KVector2.Scalar24 - mv1.KVector2.Scalar34 * mv2.KVector2.Scalar34 + mv1.KVector2.Scalar15 * mv2.KVector2.Scalar15 - mv1.KVector2.Scalar25 * mv2.KVector2.Scalar25 - mv1.KVector2.Scalar35 * mv2.KVector2.Scalar35 - mv1.KVector2.Scalar45 * mv2.KVector2.Scalar45 + mv1.KVector2.Scalar16 * mv2.KVector2.Scalar16 - mv1.KVector2.Scalar26 * mv2.KVector2.Scalar26 - mv1.KVector2.Scalar36 * mv2.KVector2.Scalar36 - mv1.KVector2.Scalar46 * mv2.KVector2.Scalar46 - mv1.KVector2.Scalar56 * mv2.KVector2.Scalar56;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[1] += -mv1.KVector2.Scalar23 * mv2.KVector3.Scalar123 - mv1.KVector2.Scalar24 * mv2.KVector3.Scalar124 - mv1.KVector2.Scalar34 * mv2.KVector3.Scalar134 - mv1.KVector2.Scalar25 * mv2.KVector3.Scalar125 - mv1.KVector2.Scalar35 * mv2.KVector3.Scalar135 - mv1.KVector2.Scalar45 * mv2.KVector3.Scalar145 - mv1.KVector2.Scalar26 * mv2.KVector3.Scalar126 - mv1.KVector2.Scalar36 * mv2.KVector3.Scalar136 - mv1.KVector2.Scalar46 * mv2.KVector3.Scalar146 - mv1.KVector2.Scalar56 * mv2.KVector3.Scalar156;
                tempScalar[2] += -mv1.KVector2.Scalar13 * mv2.KVector3.Scalar123 - mv1.KVector2.Scalar14 * mv2.KVector3.Scalar124 - mv1.KVector2.Scalar34 * mv2.KVector3.Scalar234 - mv1.KVector2.Scalar15 * mv2.KVector3.Scalar125 - mv1.KVector2.Scalar35 * mv2.KVector3.Scalar235 - mv1.KVector2.Scalar45 * mv2.KVector3.Scalar245 - mv1.KVector2.Scalar16 * mv2.KVector3.Scalar126 - mv1.KVector2.Scalar36 * mv2.KVector3.Scalar236 - mv1.KVector2.Scalar46 * mv2.KVector3.Scalar246 - mv1.KVector2.Scalar56 * mv2.KVector3.Scalar256;
                tempScalar[4] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar123 - mv1.KVector2.Scalar14 * mv2.KVector3.Scalar134 + mv1.KVector2.Scalar24 * mv2.KVector3.Scalar234 - mv1.KVector2.Scalar15 * mv2.KVector3.Scalar135 + mv1.KVector2.Scalar25 * mv2.KVector3.Scalar235 - mv1.KVector2.Scalar45 * mv2.KVector3.Scalar345 - mv1.KVector2.Scalar16 * mv2.KVector3.Scalar136 + mv1.KVector2.Scalar26 * mv2.KVector3.Scalar236 - mv1.KVector2.Scalar46 * mv2.KVector3.Scalar346 - mv1.KVector2.Scalar56 * mv2.KVector3.Scalar356;
                tempScalar[8] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar124 + mv1.KVector2.Scalar13 * mv2.KVector3.Scalar134 - mv1.KVector2.Scalar23 * mv2.KVector3.Scalar234 - mv1.KVector2.Scalar15 * mv2.KVector3.Scalar145 + mv1.KVector2.Scalar25 * mv2.KVector3.Scalar245 + mv1.KVector2.Scalar35 * mv2.KVector3.Scalar345 - mv1.KVector2.Scalar16 * mv2.KVector3.Scalar146 + mv1.KVector2.Scalar26 * mv2.KVector3.Scalar246 + mv1.KVector2.Scalar36 * mv2.KVector3.Scalar346 - mv1.KVector2.Scalar56 * mv2.KVector3.Scalar456;
                tempScalar[16] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar125 + mv1.KVector2.Scalar13 * mv2.KVector3.Scalar135 - mv1.KVector2.Scalar23 * mv2.KVector3.Scalar235 + mv1.KVector2.Scalar14 * mv2.KVector3.Scalar145 - mv1.KVector2.Scalar24 * mv2.KVector3.Scalar245 - mv1.KVector2.Scalar34 * mv2.KVector3.Scalar345 - mv1.KVector2.Scalar16 * mv2.KVector3.Scalar156 + mv1.KVector2.Scalar26 * mv2.KVector3.Scalar256 + mv1.KVector2.Scalar36 * mv2.KVector3.Scalar356 + mv1.KVector2.Scalar46 * mv2.KVector3.Scalar456;
                tempScalar[32] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar126 + mv1.KVector2.Scalar13 * mv2.KVector3.Scalar136 - mv1.KVector2.Scalar23 * mv2.KVector3.Scalar236 + mv1.KVector2.Scalar14 * mv2.KVector3.Scalar146 - mv1.KVector2.Scalar24 * mv2.KVector3.Scalar246 - mv1.KVector2.Scalar34 * mv2.KVector3.Scalar346 + mv1.KVector2.Scalar15 * mv2.KVector3.Scalar156 - mv1.KVector2.Scalar25 * mv2.KVector3.Scalar256 - mv1.KVector2.Scalar35 * mv2.KVector3.Scalar356 - mv1.KVector2.Scalar45 * mv2.KVector3.Scalar456;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[3] += -mv1.KVector2.Scalar34 * mv2.KVector4.Scalar1234 - mv1.KVector2.Scalar35 * mv2.KVector4.Scalar1235 - mv1.KVector2.Scalar45 * mv2.KVector4.Scalar1245 - mv1.KVector2.Scalar36 * mv2.KVector4.Scalar1236 - mv1.KVector2.Scalar46 * mv2.KVector4.Scalar1246 - mv1.KVector2.Scalar56 * mv2.KVector4.Scalar1256;
                tempScalar[5] += mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1234 + mv1.KVector2.Scalar25 * mv2.KVector4.Scalar1235 - mv1.KVector2.Scalar45 * mv2.KVector4.Scalar1345 + mv1.KVector2.Scalar26 * mv2.KVector4.Scalar1236 - mv1.KVector2.Scalar46 * mv2.KVector4.Scalar1346 - mv1.KVector2.Scalar56 * mv2.KVector4.Scalar1356;
                tempScalar[6] += mv1.KVector2.Scalar14 * mv2.KVector4.Scalar1234 + mv1.KVector2.Scalar15 * mv2.KVector4.Scalar1235 - mv1.KVector2.Scalar45 * mv2.KVector4.Scalar2345 + mv1.KVector2.Scalar16 * mv2.KVector4.Scalar1236 - mv1.KVector2.Scalar46 * mv2.KVector4.Scalar2346 - mv1.KVector2.Scalar56 * mv2.KVector4.Scalar2356;
                tempScalar[9] += -mv1.KVector2.Scalar23 * mv2.KVector4.Scalar1234 + mv1.KVector2.Scalar25 * mv2.KVector4.Scalar1245 + mv1.KVector2.Scalar35 * mv2.KVector4.Scalar1345 + mv1.KVector2.Scalar26 * mv2.KVector4.Scalar1246 + mv1.KVector2.Scalar36 * mv2.KVector4.Scalar1346 - mv1.KVector2.Scalar56 * mv2.KVector4.Scalar1456;
                tempScalar[10] += -mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1234 + mv1.KVector2.Scalar15 * mv2.KVector4.Scalar1245 + mv1.KVector2.Scalar35 * mv2.KVector4.Scalar2345 + mv1.KVector2.Scalar16 * mv2.KVector4.Scalar1246 + mv1.KVector2.Scalar36 * mv2.KVector4.Scalar2346 - mv1.KVector2.Scalar56 * mv2.KVector4.Scalar2456;
                tempScalar[12] += mv1.KVector2.Scalar12 * mv2.KVector4.Scalar1234 + mv1.KVector2.Scalar15 * mv2.KVector4.Scalar1345 - mv1.KVector2.Scalar25 * mv2.KVector4.Scalar2345 + mv1.KVector2.Scalar16 * mv2.KVector4.Scalar1346 - mv1.KVector2.Scalar26 * mv2.KVector4.Scalar2346 - mv1.KVector2.Scalar56 * mv2.KVector4.Scalar3456;
                tempScalar[17] += -mv1.KVector2.Scalar23 * mv2.KVector4.Scalar1235 - mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1245 - mv1.KVector2.Scalar34 * mv2.KVector4.Scalar1345 + mv1.KVector2.Scalar26 * mv2.KVector4.Scalar1256 + mv1.KVector2.Scalar36 * mv2.KVector4.Scalar1356 + mv1.KVector2.Scalar46 * mv2.KVector4.Scalar1456;
                tempScalar[18] += -mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1235 - mv1.KVector2.Scalar14 * mv2.KVector4.Scalar1245 - mv1.KVector2.Scalar34 * mv2.KVector4.Scalar2345 + mv1.KVector2.Scalar16 * mv2.KVector4.Scalar1256 + mv1.KVector2.Scalar36 * mv2.KVector4.Scalar2356 + mv1.KVector2.Scalar46 * mv2.KVector4.Scalar2456;
                tempScalar[20] += mv1.KVector2.Scalar12 * mv2.KVector4.Scalar1235 - mv1.KVector2.Scalar14 * mv2.KVector4.Scalar1345 + mv1.KVector2.Scalar24 * mv2.KVector4.Scalar2345 + mv1.KVector2.Scalar16 * mv2.KVector4.Scalar1356 - mv1.KVector2.Scalar26 * mv2.KVector4.Scalar2356 + mv1.KVector2.Scalar46 * mv2.KVector4.Scalar3456;
                tempScalar[24] += mv1.KVector2.Scalar12 * mv2.KVector4.Scalar1245 + mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1345 - mv1.KVector2.Scalar23 * mv2.KVector4.Scalar2345 + mv1.KVector2.Scalar16 * mv2.KVector4.Scalar1456 - mv1.KVector2.Scalar26 * mv2.KVector4.Scalar2456 - mv1.KVector2.Scalar36 * mv2.KVector4.Scalar3456;
                tempScalar[33] += -mv1.KVector2.Scalar23 * mv2.KVector4.Scalar1236 - mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1246 - mv1.KVector2.Scalar34 * mv2.KVector4.Scalar1346 - mv1.KVector2.Scalar25 * mv2.KVector4.Scalar1256 - mv1.KVector2.Scalar35 * mv2.KVector4.Scalar1356 - mv1.KVector2.Scalar45 * mv2.KVector4.Scalar1456;
                tempScalar[34] += -mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1236 - mv1.KVector2.Scalar14 * mv2.KVector4.Scalar1246 - mv1.KVector2.Scalar34 * mv2.KVector4.Scalar2346 - mv1.KVector2.Scalar15 * mv2.KVector4.Scalar1256 - mv1.KVector2.Scalar35 * mv2.KVector4.Scalar2356 - mv1.KVector2.Scalar45 * mv2.KVector4.Scalar2456;
                tempScalar[36] += mv1.KVector2.Scalar12 * mv2.KVector4.Scalar1236 - mv1.KVector2.Scalar14 * mv2.KVector4.Scalar1346 + mv1.KVector2.Scalar24 * mv2.KVector4.Scalar2346 - mv1.KVector2.Scalar15 * mv2.KVector4.Scalar1356 + mv1.KVector2.Scalar25 * mv2.KVector4.Scalar2356 - mv1.KVector2.Scalar45 * mv2.KVector4.Scalar3456;
                tempScalar[40] += mv1.KVector2.Scalar12 * mv2.KVector4.Scalar1246 + mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1346 - mv1.KVector2.Scalar23 * mv2.KVector4.Scalar2346 - mv1.KVector2.Scalar15 * mv2.KVector4.Scalar1456 + mv1.KVector2.Scalar25 * mv2.KVector4.Scalar2456 + mv1.KVector2.Scalar35 * mv2.KVector4.Scalar3456;
                tempScalar[48] += mv1.KVector2.Scalar12 * mv2.KVector4.Scalar1256 + mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1356 - mv1.KVector2.Scalar23 * mv2.KVector4.Scalar2356 + mv1.KVector2.Scalar14 * mv2.KVector4.Scalar1456 - mv1.KVector2.Scalar24 * mv2.KVector4.Scalar2456 - mv1.KVector2.Scalar34 * mv2.KVector4.Scalar3456;
            }
            
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[7] += -mv1.KVector2.Scalar45 * mv2.KVector5.Scalar12345 - mv1.KVector2.Scalar46 * mv2.KVector5.Scalar12346 - mv1.KVector2.Scalar56 * mv2.KVector5.Scalar12356;
                tempScalar[11] += mv1.KVector2.Scalar35 * mv2.KVector5.Scalar12345 + mv1.KVector2.Scalar36 * mv2.KVector5.Scalar12346 - mv1.KVector2.Scalar56 * mv2.KVector5.Scalar12456;
                tempScalar[13] += -mv1.KVector2.Scalar25 * mv2.KVector5.Scalar12345 - mv1.KVector2.Scalar26 * mv2.KVector5.Scalar12346 - mv1.KVector2.Scalar56 * mv2.KVector5.Scalar13456;
                tempScalar[14] += -mv1.KVector2.Scalar15 * mv2.KVector5.Scalar12345 - mv1.KVector2.Scalar16 * mv2.KVector5.Scalar12346 - mv1.KVector2.Scalar56 * mv2.KVector5.Scalar23456;
                tempScalar[19] += -mv1.KVector2.Scalar34 * mv2.KVector5.Scalar12345 + mv1.KVector2.Scalar36 * mv2.KVector5.Scalar12356 + mv1.KVector2.Scalar46 * mv2.KVector5.Scalar12456;
                tempScalar[21] += mv1.KVector2.Scalar24 * mv2.KVector5.Scalar12345 - mv1.KVector2.Scalar26 * mv2.KVector5.Scalar12356 + mv1.KVector2.Scalar46 * mv2.KVector5.Scalar13456;
                tempScalar[22] += mv1.KVector2.Scalar14 * mv2.KVector5.Scalar12345 - mv1.KVector2.Scalar16 * mv2.KVector5.Scalar12356 + mv1.KVector2.Scalar46 * mv2.KVector5.Scalar23456;
                tempScalar[25] += -mv1.KVector2.Scalar23 * mv2.KVector5.Scalar12345 - mv1.KVector2.Scalar26 * mv2.KVector5.Scalar12456 - mv1.KVector2.Scalar36 * mv2.KVector5.Scalar13456;
                tempScalar[26] += -mv1.KVector2.Scalar13 * mv2.KVector5.Scalar12345 - mv1.KVector2.Scalar16 * mv2.KVector5.Scalar12456 - mv1.KVector2.Scalar36 * mv2.KVector5.Scalar23456;
                tempScalar[28] += mv1.KVector2.Scalar12 * mv2.KVector5.Scalar12345 - mv1.KVector2.Scalar16 * mv2.KVector5.Scalar13456 + mv1.KVector2.Scalar26 * mv2.KVector5.Scalar23456;
                tempScalar[35] += -mv1.KVector2.Scalar34 * mv2.KVector5.Scalar12346 - mv1.KVector2.Scalar35 * mv2.KVector5.Scalar12356 - mv1.KVector2.Scalar45 * mv2.KVector5.Scalar12456;
                tempScalar[37] += mv1.KVector2.Scalar24 * mv2.KVector5.Scalar12346 + mv1.KVector2.Scalar25 * mv2.KVector5.Scalar12356 - mv1.KVector2.Scalar45 * mv2.KVector5.Scalar13456;
                tempScalar[38] += mv1.KVector2.Scalar14 * mv2.KVector5.Scalar12346 + mv1.KVector2.Scalar15 * mv2.KVector5.Scalar12356 - mv1.KVector2.Scalar45 * mv2.KVector5.Scalar23456;
                tempScalar[41] += -mv1.KVector2.Scalar23 * mv2.KVector5.Scalar12346 + mv1.KVector2.Scalar25 * mv2.KVector5.Scalar12456 + mv1.KVector2.Scalar35 * mv2.KVector5.Scalar13456;
                tempScalar[42] += -mv1.KVector2.Scalar13 * mv2.KVector5.Scalar12346 + mv1.KVector2.Scalar15 * mv2.KVector5.Scalar12456 + mv1.KVector2.Scalar35 * mv2.KVector5.Scalar23456;
                tempScalar[44] += mv1.KVector2.Scalar12 * mv2.KVector5.Scalar12346 + mv1.KVector2.Scalar15 * mv2.KVector5.Scalar13456 - mv1.KVector2.Scalar25 * mv2.KVector5.Scalar23456;
                tempScalar[49] += -mv1.KVector2.Scalar23 * mv2.KVector5.Scalar12356 - mv1.KVector2.Scalar24 * mv2.KVector5.Scalar12456 - mv1.KVector2.Scalar34 * mv2.KVector5.Scalar13456;
                tempScalar[50] += -mv1.KVector2.Scalar13 * mv2.KVector5.Scalar12356 - mv1.KVector2.Scalar14 * mv2.KVector5.Scalar12456 - mv1.KVector2.Scalar34 * mv2.KVector5.Scalar23456;
                tempScalar[52] += mv1.KVector2.Scalar12 * mv2.KVector5.Scalar12356 - mv1.KVector2.Scalar14 * mv2.KVector5.Scalar13456 + mv1.KVector2.Scalar24 * mv2.KVector5.Scalar23456;
                tempScalar[56] += mv1.KVector2.Scalar12 * mv2.KVector5.Scalar12456 + mv1.KVector2.Scalar13 * mv2.KVector5.Scalar13456 - mv1.KVector2.Scalar23 * mv2.KVector5.Scalar23456;
            }
            
            if (!mv2.KVector6.IsZero())
            {
                tempScalar[15] += -mv1.KVector2.Scalar56 * mv2.KVector6.Scalar123456;
                tempScalar[23] += mv1.KVector2.Scalar46 * mv2.KVector6.Scalar123456;
                tempScalar[27] += -mv1.KVector2.Scalar36 * mv2.KVector6.Scalar123456;
                tempScalar[29] += mv1.KVector2.Scalar26 * mv2.KVector6.Scalar123456;
                tempScalar[30] += mv1.KVector2.Scalar16 * mv2.KVector6.Scalar123456;
                tempScalar[39] += -mv1.KVector2.Scalar45 * mv2.KVector6.Scalar123456;
                tempScalar[43] += mv1.KVector2.Scalar35 * mv2.KVector6.Scalar123456;
                tempScalar[45] += -mv1.KVector2.Scalar25 * mv2.KVector6.Scalar123456;
                tempScalar[46] += -mv1.KVector2.Scalar15 * mv2.KVector6.Scalar123456;
                tempScalar[51] += -mv1.KVector2.Scalar34 * mv2.KVector6.Scalar123456;
                tempScalar[53] += mv1.KVector2.Scalar24 * mv2.KVector6.Scalar123456;
                tempScalar[54] += mv1.KVector2.Scalar14 * mv2.KVector6.Scalar123456;
                tempScalar[57] += -mv1.KVector2.Scalar23 * mv2.KVector6.Scalar123456;
                tempScalar[58] += -mv1.KVector2.Scalar13 * mv2.KVector6.Scalar123456;
                tempScalar[60] += mv1.KVector2.Scalar12 * mv2.KVector6.Scalar123456;
            }
            
        }
        
        if (!mv1.KVector3.IsZero())
        {
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[0] += mv1.KVector3.Scalar123 * mv2.KVector3.Scalar123 + mv1.KVector3.Scalar124 * mv2.KVector3.Scalar124 + mv1.KVector3.Scalar134 * mv2.KVector3.Scalar134 - mv1.KVector3.Scalar234 * mv2.KVector3.Scalar234 + mv1.KVector3.Scalar125 * mv2.KVector3.Scalar125 + mv1.KVector3.Scalar135 * mv2.KVector3.Scalar135 - mv1.KVector3.Scalar235 * mv2.KVector3.Scalar235 + mv1.KVector3.Scalar145 * mv2.KVector3.Scalar145 - mv1.KVector3.Scalar245 * mv2.KVector3.Scalar245 - mv1.KVector3.Scalar345 * mv2.KVector3.Scalar345 + mv1.KVector3.Scalar126 * mv2.KVector3.Scalar126 + mv1.KVector3.Scalar136 * mv2.KVector3.Scalar136 - mv1.KVector3.Scalar236 * mv2.KVector3.Scalar236 + mv1.KVector3.Scalar146 * mv2.KVector3.Scalar146 - mv1.KVector3.Scalar246 * mv2.KVector3.Scalar246 - mv1.KVector3.Scalar346 * mv2.KVector3.Scalar346 + mv1.KVector3.Scalar156 * mv2.KVector3.Scalar156 - mv1.KVector3.Scalar256 * mv2.KVector3.Scalar256 - mv1.KVector3.Scalar356 * mv2.KVector3.Scalar356 - mv1.KVector3.Scalar456 * mv2.KVector3.Scalar456;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[1] += mv1.KVector3.Scalar234 * mv2.KVector4.Scalar1234 + mv1.KVector3.Scalar235 * mv2.KVector4.Scalar1235 + mv1.KVector3.Scalar245 * mv2.KVector4.Scalar1245 + mv1.KVector3.Scalar345 * mv2.KVector4.Scalar1345 + mv1.KVector3.Scalar236 * mv2.KVector4.Scalar1236 + mv1.KVector3.Scalar246 * mv2.KVector4.Scalar1246 + mv1.KVector3.Scalar346 * mv2.KVector4.Scalar1346 + mv1.KVector3.Scalar256 * mv2.KVector4.Scalar1256 + mv1.KVector3.Scalar356 * mv2.KVector4.Scalar1356 + mv1.KVector3.Scalar456 * mv2.KVector4.Scalar1456;
                tempScalar[2] += mv1.KVector3.Scalar134 * mv2.KVector4.Scalar1234 + mv1.KVector3.Scalar135 * mv2.KVector4.Scalar1235 + mv1.KVector3.Scalar145 * mv2.KVector4.Scalar1245 + mv1.KVector3.Scalar345 * mv2.KVector4.Scalar2345 + mv1.KVector3.Scalar136 * mv2.KVector4.Scalar1236 + mv1.KVector3.Scalar146 * mv2.KVector4.Scalar1246 + mv1.KVector3.Scalar346 * mv2.KVector4.Scalar2346 + mv1.KVector3.Scalar156 * mv2.KVector4.Scalar1256 + mv1.KVector3.Scalar356 * mv2.KVector4.Scalar2356 + mv1.KVector3.Scalar456 * mv2.KVector4.Scalar2456;
                tempScalar[4] += -mv1.KVector3.Scalar124 * mv2.KVector4.Scalar1234 - mv1.KVector3.Scalar125 * mv2.KVector4.Scalar1235 + mv1.KVector3.Scalar145 * mv2.KVector4.Scalar1345 - mv1.KVector3.Scalar245 * mv2.KVector4.Scalar2345 - mv1.KVector3.Scalar126 * mv2.KVector4.Scalar1236 + mv1.KVector3.Scalar146 * mv2.KVector4.Scalar1346 - mv1.KVector3.Scalar246 * mv2.KVector4.Scalar2346 + mv1.KVector3.Scalar156 * mv2.KVector4.Scalar1356 - mv1.KVector3.Scalar256 * mv2.KVector4.Scalar2356 + mv1.KVector3.Scalar456 * mv2.KVector4.Scalar3456;
                tempScalar[8] += mv1.KVector3.Scalar123 * mv2.KVector4.Scalar1234 - mv1.KVector3.Scalar125 * mv2.KVector4.Scalar1245 - mv1.KVector3.Scalar135 * mv2.KVector4.Scalar1345 + mv1.KVector3.Scalar235 * mv2.KVector4.Scalar2345 - mv1.KVector3.Scalar126 * mv2.KVector4.Scalar1246 - mv1.KVector3.Scalar136 * mv2.KVector4.Scalar1346 + mv1.KVector3.Scalar236 * mv2.KVector4.Scalar2346 + mv1.KVector3.Scalar156 * mv2.KVector4.Scalar1456 - mv1.KVector3.Scalar256 * mv2.KVector4.Scalar2456 - mv1.KVector3.Scalar356 * mv2.KVector4.Scalar3456;
                tempScalar[16] += mv1.KVector3.Scalar123 * mv2.KVector4.Scalar1235 + mv1.KVector3.Scalar124 * mv2.KVector4.Scalar1245 + mv1.KVector3.Scalar134 * mv2.KVector4.Scalar1345 - mv1.KVector3.Scalar234 * mv2.KVector4.Scalar2345 - mv1.KVector3.Scalar126 * mv2.KVector4.Scalar1256 - mv1.KVector3.Scalar136 * mv2.KVector4.Scalar1356 + mv1.KVector3.Scalar236 * mv2.KVector4.Scalar2356 - mv1.KVector3.Scalar146 * mv2.KVector4.Scalar1456 + mv1.KVector3.Scalar246 * mv2.KVector4.Scalar2456 + mv1.KVector3.Scalar346 * mv2.KVector4.Scalar3456;
                tempScalar[32] += mv1.KVector3.Scalar123 * mv2.KVector4.Scalar1236 + mv1.KVector3.Scalar124 * mv2.KVector4.Scalar1246 + mv1.KVector3.Scalar134 * mv2.KVector4.Scalar1346 - mv1.KVector3.Scalar234 * mv2.KVector4.Scalar2346 + mv1.KVector3.Scalar125 * mv2.KVector4.Scalar1256 + mv1.KVector3.Scalar135 * mv2.KVector4.Scalar1356 - mv1.KVector3.Scalar235 * mv2.KVector4.Scalar2356 + mv1.KVector3.Scalar145 * mv2.KVector4.Scalar1456 - mv1.KVector3.Scalar245 * mv2.KVector4.Scalar2456 - mv1.KVector3.Scalar345 * mv2.KVector4.Scalar3456;
            }
            
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[3] += -mv1.KVector3.Scalar345 * mv2.KVector5.Scalar12345 - mv1.KVector3.Scalar346 * mv2.KVector5.Scalar12346 - mv1.KVector3.Scalar356 * mv2.KVector5.Scalar12356 - mv1.KVector3.Scalar456 * mv2.KVector5.Scalar12456;
                tempScalar[5] += mv1.KVector3.Scalar245 * mv2.KVector5.Scalar12345 + mv1.KVector3.Scalar246 * mv2.KVector5.Scalar12346 + mv1.KVector3.Scalar256 * mv2.KVector5.Scalar12356 - mv1.KVector3.Scalar456 * mv2.KVector5.Scalar13456;
                tempScalar[6] += mv1.KVector3.Scalar145 * mv2.KVector5.Scalar12345 + mv1.KVector3.Scalar146 * mv2.KVector5.Scalar12346 + mv1.KVector3.Scalar156 * mv2.KVector5.Scalar12356 - mv1.KVector3.Scalar456 * mv2.KVector5.Scalar23456;
                tempScalar[9] += -mv1.KVector3.Scalar235 * mv2.KVector5.Scalar12345 - mv1.KVector3.Scalar236 * mv2.KVector5.Scalar12346 + mv1.KVector3.Scalar256 * mv2.KVector5.Scalar12456 + mv1.KVector3.Scalar356 * mv2.KVector5.Scalar13456;
                tempScalar[10] += -mv1.KVector3.Scalar135 * mv2.KVector5.Scalar12345 - mv1.KVector3.Scalar136 * mv2.KVector5.Scalar12346 + mv1.KVector3.Scalar156 * mv2.KVector5.Scalar12456 + mv1.KVector3.Scalar356 * mv2.KVector5.Scalar23456;
                tempScalar[12] += mv1.KVector3.Scalar125 * mv2.KVector5.Scalar12345 + mv1.KVector3.Scalar126 * mv2.KVector5.Scalar12346 + mv1.KVector3.Scalar156 * mv2.KVector5.Scalar13456 - mv1.KVector3.Scalar256 * mv2.KVector5.Scalar23456;
                tempScalar[17] += mv1.KVector3.Scalar234 * mv2.KVector5.Scalar12345 - mv1.KVector3.Scalar236 * mv2.KVector5.Scalar12356 - mv1.KVector3.Scalar246 * mv2.KVector5.Scalar12456 - mv1.KVector3.Scalar346 * mv2.KVector5.Scalar13456;
                tempScalar[18] += mv1.KVector3.Scalar134 * mv2.KVector5.Scalar12345 - mv1.KVector3.Scalar136 * mv2.KVector5.Scalar12356 - mv1.KVector3.Scalar146 * mv2.KVector5.Scalar12456 - mv1.KVector3.Scalar346 * mv2.KVector5.Scalar23456;
                tempScalar[20] += -mv1.KVector3.Scalar124 * mv2.KVector5.Scalar12345 + mv1.KVector3.Scalar126 * mv2.KVector5.Scalar12356 - mv1.KVector3.Scalar146 * mv2.KVector5.Scalar13456 + mv1.KVector3.Scalar246 * mv2.KVector5.Scalar23456;
                tempScalar[24] += mv1.KVector3.Scalar123 * mv2.KVector5.Scalar12345 + mv1.KVector3.Scalar126 * mv2.KVector5.Scalar12456 + mv1.KVector3.Scalar136 * mv2.KVector5.Scalar13456 - mv1.KVector3.Scalar236 * mv2.KVector5.Scalar23456;
                tempScalar[33] += mv1.KVector3.Scalar234 * mv2.KVector5.Scalar12346 + mv1.KVector3.Scalar235 * mv2.KVector5.Scalar12356 + mv1.KVector3.Scalar245 * mv2.KVector5.Scalar12456 + mv1.KVector3.Scalar345 * mv2.KVector5.Scalar13456;
                tempScalar[34] += mv1.KVector3.Scalar134 * mv2.KVector5.Scalar12346 + mv1.KVector3.Scalar135 * mv2.KVector5.Scalar12356 + mv1.KVector3.Scalar145 * mv2.KVector5.Scalar12456 + mv1.KVector3.Scalar345 * mv2.KVector5.Scalar23456;
                tempScalar[36] += -mv1.KVector3.Scalar124 * mv2.KVector5.Scalar12346 - mv1.KVector3.Scalar125 * mv2.KVector5.Scalar12356 + mv1.KVector3.Scalar145 * mv2.KVector5.Scalar13456 - mv1.KVector3.Scalar245 * mv2.KVector5.Scalar23456;
                tempScalar[40] += mv1.KVector3.Scalar123 * mv2.KVector5.Scalar12346 - mv1.KVector3.Scalar125 * mv2.KVector5.Scalar12456 - mv1.KVector3.Scalar135 * mv2.KVector5.Scalar13456 + mv1.KVector3.Scalar235 * mv2.KVector5.Scalar23456;
                tempScalar[48] += mv1.KVector3.Scalar123 * mv2.KVector5.Scalar12356 + mv1.KVector3.Scalar124 * mv2.KVector5.Scalar12456 + mv1.KVector3.Scalar134 * mv2.KVector5.Scalar13456 - mv1.KVector3.Scalar234 * mv2.KVector5.Scalar23456;
            }
            
            if (!mv2.KVector6.IsZero())
            {
                tempScalar[7] += mv1.KVector3.Scalar456 * mv2.KVector6.Scalar123456;
                tempScalar[11] += -mv1.KVector3.Scalar356 * mv2.KVector6.Scalar123456;
                tempScalar[13] += mv1.KVector3.Scalar256 * mv2.KVector6.Scalar123456;
                tempScalar[14] += mv1.KVector3.Scalar156 * mv2.KVector6.Scalar123456;
                tempScalar[19] += mv1.KVector3.Scalar346 * mv2.KVector6.Scalar123456;
                tempScalar[21] += -mv1.KVector3.Scalar246 * mv2.KVector6.Scalar123456;
                tempScalar[22] += -mv1.KVector3.Scalar146 * mv2.KVector6.Scalar123456;
                tempScalar[25] += mv1.KVector3.Scalar236 * mv2.KVector6.Scalar123456;
                tempScalar[26] += mv1.KVector3.Scalar136 * mv2.KVector6.Scalar123456;
                tempScalar[28] += -mv1.KVector3.Scalar126 * mv2.KVector6.Scalar123456;
                tempScalar[35] += -mv1.KVector3.Scalar345 * mv2.KVector6.Scalar123456;
                tempScalar[37] += mv1.KVector3.Scalar245 * mv2.KVector6.Scalar123456;
                tempScalar[38] += mv1.KVector3.Scalar145 * mv2.KVector6.Scalar123456;
                tempScalar[41] += -mv1.KVector3.Scalar235 * mv2.KVector6.Scalar123456;
                tempScalar[42] += -mv1.KVector3.Scalar135 * mv2.KVector6.Scalar123456;
                tempScalar[44] += mv1.KVector3.Scalar125 * mv2.KVector6.Scalar123456;
                tempScalar[49] += mv1.KVector3.Scalar234 * mv2.KVector6.Scalar123456;
                tempScalar[50] += mv1.KVector3.Scalar134 * mv2.KVector6.Scalar123456;
                tempScalar[52] += -mv1.KVector3.Scalar124 * mv2.KVector6.Scalar123456;
                tempScalar[56] += mv1.KVector3.Scalar123 * mv2.KVector6.Scalar123456;
            }
            
        }
        
        if (!mv1.KVector4.IsZero())
        {
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[0] += -mv1.KVector4.Scalar1234 * mv2.KVector4.Scalar1234 - mv1.KVector4.Scalar1235 * mv2.KVector4.Scalar1235 - mv1.KVector4.Scalar1245 * mv2.KVector4.Scalar1245 - mv1.KVector4.Scalar1345 * mv2.KVector4.Scalar1345 + mv1.KVector4.Scalar2345 * mv2.KVector4.Scalar2345 - mv1.KVector4.Scalar1236 * mv2.KVector4.Scalar1236 - mv1.KVector4.Scalar1246 * mv2.KVector4.Scalar1246 - mv1.KVector4.Scalar1346 * mv2.KVector4.Scalar1346 + mv1.KVector4.Scalar2346 * mv2.KVector4.Scalar2346 - mv1.KVector4.Scalar1256 * mv2.KVector4.Scalar1256 - mv1.KVector4.Scalar1356 * mv2.KVector4.Scalar1356 + mv1.KVector4.Scalar2356 * mv2.KVector4.Scalar2356 - mv1.KVector4.Scalar1456 * mv2.KVector4.Scalar1456 + mv1.KVector4.Scalar2456 * mv2.KVector4.Scalar2456 + mv1.KVector4.Scalar3456 * mv2.KVector4.Scalar3456;
            }
            
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[1] += mv1.KVector4.Scalar2345 * mv2.KVector5.Scalar12345 + mv1.KVector4.Scalar2346 * mv2.KVector5.Scalar12346 + mv1.KVector4.Scalar2356 * mv2.KVector5.Scalar12356 + mv1.KVector4.Scalar2456 * mv2.KVector5.Scalar12456 + mv1.KVector4.Scalar3456 * mv2.KVector5.Scalar13456;
                tempScalar[2] += mv1.KVector4.Scalar1345 * mv2.KVector5.Scalar12345 + mv1.KVector4.Scalar1346 * mv2.KVector5.Scalar12346 + mv1.KVector4.Scalar1356 * mv2.KVector5.Scalar12356 + mv1.KVector4.Scalar1456 * mv2.KVector5.Scalar12456 + mv1.KVector4.Scalar3456 * mv2.KVector5.Scalar23456;
                tempScalar[4] += -mv1.KVector4.Scalar1245 * mv2.KVector5.Scalar12345 - mv1.KVector4.Scalar1246 * mv2.KVector5.Scalar12346 - mv1.KVector4.Scalar1256 * mv2.KVector5.Scalar12356 + mv1.KVector4.Scalar1456 * mv2.KVector5.Scalar13456 - mv1.KVector4.Scalar2456 * mv2.KVector5.Scalar23456;
                tempScalar[8] += mv1.KVector4.Scalar1235 * mv2.KVector5.Scalar12345 + mv1.KVector4.Scalar1236 * mv2.KVector5.Scalar12346 - mv1.KVector4.Scalar1256 * mv2.KVector5.Scalar12456 - mv1.KVector4.Scalar1356 * mv2.KVector5.Scalar13456 + mv1.KVector4.Scalar2356 * mv2.KVector5.Scalar23456;
                tempScalar[16] += -mv1.KVector4.Scalar1234 * mv2.KVector5.Scalar12345 + mv1.KVector4.Scalar1236 * mv2.KVector5.Scalar12356 + mv1.KVector4.Scalar1246 * mv2.KVector5.Scalar12456 + mv1.KVector4.Scalar1346 * mv2.KVector5.Scalar13456 - mv1.KVector4.Scalar2346 * mv2.KVector5.Scalar23456;
                tempScalar[32] += -mv1.KVector4.Scalar1234 * mv2.KVector5.Scalar12346 - mv1.KVector4.Scalar1235 * mv2.KVector5.Scalar12356 - mv1.KVector4.Scalar1245 * mv2.KVector5.Scalar12456 - mv1.KVector4.Scalar1345 * mv2.KVector5.Scalar13456 + mv1.KVector4.Scalar2345 * mv2.KVector5.Scalar23456;
            }
            
            if (!mv2.KVector6.IsZero())
            {
                tempScalar[3] += mv1.KVector4.Scalar3456 * mv2.KVector6.Scalar123456;
                tempScalar[5] += -mv1.KVector4.Scalar2456 * mv2.KVector6.Scalar123456;
                tempScalar[6] += -mv1.KVector4.Scalar1456 * mv2.KVector6.Scalar123456;
                tempScalar[9] += mv1.KVector4.Scalar2356 * mv2.KVector6.Scalar123456;
                tempScalar[10] += mv1.KVector4.Scalar1356 * mv2.KVector6.Scalar123456;
                tempScalar[12] += -mv1.KVector4.Scalar1256 * mv2.KVector6.Scalar123456;
                tempScalar[17] += -mv1.KVector4.Scalar2346 * mv2.KVector6.Scalar123456;
                tempScalar[18] += -mv1.KVector4.Scalar1346 * mv2.KVector6.Scalar123456;
                tempScalar[20] += mv1.KVector4.Scalar1246 * mv2.KVector6.Scalar123456;
                tempScalar[24] += -mv1.KVector4.Scalar1236 * mv2.KVector6.Scalar123456;
                tempScalar[33] += mv1.KVector4.Scalar2345 * mv2.KVector6.Scalar123456;
                tempScalar[34] += mv1.KVector4.Scalar1345 * mv2.KVector6.Scalar123456;
                tempScalar[36] += -mv1.KVector4.Scalar1245 * mv2.KVector6.Scalar123456;
                tempScalar[40] += mv1.KVector4.Scalar1235 * mv2.KVector6.Scalar123456;
                tempScalar[48] += -mv1.KVector4.Scalar1234 * mv2.KVector6.Scalar123456;
            }
            
        }
        
        if (!mv1.KVector5.IsZero())
        {
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[0] += -mv1.KVector5.Scalar12345 * mv2.KVector5.Scalar12345 - mv1.KVector5.Scalar12346 * mv2.KVector5.Scalar12346 - mv1.KVector5.Scalar12356 * mv2.KVector5.Scalar12356 - mv1.KVector5.Scalar12456 * mv2.KVector5.Scalar12456 - mv1.KVector5.Scalar13456 * mv2.KVector5.Scalar13456 + mv1.KVector5.Scalar23456 * mv2.KVector5.Scalar23456;
            }
            
            if (!mv2.KVector6.IsZero())
            {
                tempScalar[1] += -mv1.KVector5.Scalar23456 * mv2.KVector6.Scalar123456;
                tempScalar[2] += -mv1.KVector5.Scalar13456 * mv2.KVector6.Scalar123456;
                tempScalar[4] += mv1.KVector5.Scalar12456 * mv2.KVector6.Scalar123456;
                tempScalar[8] += -mv1.KVector5.Scalar12356 * mv2.KVector6.Scalar123456;
                tempScalar[16] += mv1.KVector5.Scalar12346 * mv2.KVector6.Scalar123456;
                tempScalar[32] += -mv1.KVector5.Scalar12345 * mv2.KVector6.Scalar123456;
            }
            
        }
        
        if (!mv1.KVector6.IsZero())
        {
            if (!mv2.KVector6.IsZero())
            {
                tempScalar[0] += mv1.KVector6.Scalar123456 * mv2.KVector6.Scalar123456;
            }
            
        }
        
        return Ga51Multivector.Create(tempScalar);
    }
    
}
