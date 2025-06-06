using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga6;

public static class Ga6CommutatorProduct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector0 mv1, Ga6KVector0 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector0 mv1, Ga6KVector1 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector0 mv1, Ga6KVector2 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector0 mv1, Ga6KVector3 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector0 mv1, Ga6KVector4 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector0 mv1, Ga6KVector5 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector0 mv1, Ga6KVector6 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector0 mv1, Ga6Multivector mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector1 mv1, Ga6KVector0 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector2 Cp(this Ga6KVector1 mv1, Ga6KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector2.Zero;
        
        return new Ga6KVector2
        {
            Scalar12 = mv1.Scalar1 * mv2.Scalar2 - mv1.Scalar2 * mv2.Scalar1,
            Scalar13 = mv1.Scalar1 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar1,
            Scalar23 = mv1.Scalar2 * mv2.Scalar3 - mv1.Scalar3 * mv2.Scalar2,
            Scalar14 = mv1.Scalar1 * mv2.Scalar4 - mv1.Scalar4 * mv2.Scalar1,
            Scalar24 = mv1.Scalar2 * mv2.Scalar4 - mv1.Scalar4 * mv2.Scalar2,
            Scalar34 = mv1.Scalar3 * mv2.Scalar4 - mv1.Scalar4 * mv2.Scalar3,
            Scalar15 = mv1.Scalar1 * mv2.Scalar5 - mv1.Scalar5 * mv2.Scalar1,
            Scalar25 = mv1.Scalar2 * mv2.Scalar5 - mv1.Scalar5 * mv2.Scalar2,
            Scalar35 = mv1.Scalar3 * mv2.Scalar5 - mv1.Scalar5 * mv2.Scalar3,
            Scalar45 = mv1.Scalar4 * mv2.Scalar5 - mv1.Scalar5 * mv2.Scalar4,
            Scalar16 = mv1.Scalar1 * mv2.Scalar6 - mv1.Scalar6 * mv2.Scalar1,
            Scalar26 = mv1.Scalar2 * mv2.Scalar6 - mv1.Scalar6 * mv2.Scalar2,
            Scalar36 = mv1.Scalar3 * mv2.Scalar6 - mv1.Scalar6 * mv2.Scalar3,
            Scalar46 = mv1.Scalar4 * mv2.Scalar6 - mv1.Scalar6 * mv2.Scalar4,
            Scalar56 = mv1.Scalar5 * mv2.Scalar6 - mv1.Scalar6 * mv2.Scalar5
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector1 Cp(this Ga6KVector1 mv1, Ga6KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector1.Zero;
        
        return new Ga6KVector1
        {
            Scalar1 = -mv1.Scalar2 * mv2.Scalar12 - mv1.Scalar3 * mv2.Scalar13 - mv1.Scalar4 * mv2.Scalar14 - mv1.Scalar5 * mv2.Scalar15 - mv1.Scalar6 * mv2.Scalar16,
            Scalar2 = mv1.Scalar1 * mv2.Scalar12 - mv1.Scalar3 * mv2.Scalar23 - mv1.Scalar4 * mv2.Scalar24 - mv1.Scalar5 * mv2.Scalar25 - mv1.Scalar6 * mv2.Scalar26,
            Scalar3 = mv1.Scalar1 * mv2.Scalar13 + mv1.Scalar2 * mv2.Scalar23 - mv1.Scalar4 * mv2.Scalar34 - mv1.Scalar5 * mv2.Scalar35 - mv1.Scalar6 * mv2.Scalar36,
            Scalar4 = mv1.Scalar1 * mv2.Scalar14 + mv1.Scalar2 * mv2.Scalar24 + mv1.Scalar3 * mv2.Scalar34 - mv1.Scalar5 * mv2.Scalar45 - mv1.Scalar6 * mv2.Scalar46,
            Scalar5 = mv1.Scalar1 * mv2.Scalar15 + mv1.Scalar2 * mv2.Scalar25 + mv1.Scalar3 * mv2.Scalar35 + mv1.Scalar4 * mv2.Scalar45 - mv1.Scalar6 * mv2.Scalar56,
            Scalar6 = mv1.Scalar1 * mv2.Scalar16 + mv1.Scalar2 * mv2.Scalar26 + mv1.Scalar3 * mv2.Scalar36 + mv1.Scalar4 * mv2.Scalar46 + mv1.Scalar5 * mv2.Scalar56
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector4 Cp(this Ga6KVector1 mv1, Ga6KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector4.Zero;
        
        return new Ga6KVector4
        {
            Scalar1234 = mv1.Scalar1 * mv2.Scalar234 - mv1.Scalar2 * mv2.Scalar134 + mv1.Scalar3 * mv2.Scalar124 - mv1.Scalar4 * mv2.Scalar123,
            Scalar1235 = mv1.Scalar1 * mv2.Scalar235 - mv1.Scalar2 * mv2.Scalar135 + mv1.Scalar3 * mv2.Scalar125 - mv1.Scalar5 * mv2.Scalar123,
            Scalar1245 = mv1.Scalar1 * mv2.Scalar245 - mv1.Scalar2 * mv2.Scalar145 + mv1.Scalar4 * mv2.Scalar125 - mv1.Scalar5 * mv2.Scalar124,
            Scalar1345 = mv1.Scalar1 * mv2.Scalar345 - mv1.Scalar3 * mv2.Scalar145 + mv1.Scalar4 * mv2.Scalar135 - mv1.Scalar5 * mv2.Scalar134,
            Scalar2345 = mv1.Scalar2 * mv2.Scalar345 - mv1.Scalar3 * mv2.Scalar245 + mv1.Scalar4 * mv2.Scalar235 - mv1.Scalar5 * mv2.Scalar234,
            Scalar1236 = mv1.Scalar1 * mv2.Scalar236 - mv1.Scalar2 * mv2.Scalar136 + mv1.Scalar3 * mv2.Scalar126 - mv1.Scalar6 * mv2.Scalar123,
            Scalar1246 = mv1.Scalar1 * mv2.Scalar246 - mv1.Scalar2 * mv2.Scalar146 + mv1.Scalar4 * mv2.Scalar126 - mv1.Scalar6 * mv2.Scalar124,
            Scalar1346 = mv1.Scalar1 * mv2.Scalar346 - mv1.Scalar3 * mv2.Scalar146 + mv1.Scalar4 * mv2.Scalar136 - mv1.Scalar6 * mv2.Scalar134,
            Scalar2346 = mv1.Scalar2 * mv2.Scalar346 - mv1.Scalar3 * mv2.Scalar246 + mv1.Scalar4 * mv2.Scalar236 - mv1.Scalar6 * mv2.Scalar234,
            Scalar1256 = mv1.Scalar1 * mv2.Scalar256 - mv1.Scalar2 * mv2.Scalar156 + mv1.Scalar5 * mv2.Scalar126 - mv1.Scalar6 * mv2.Scalar125,
            Scalar1356 = mv1.Scalar1 * mv2.Scalar356 - mv1.Scalar3 * mv2.Scalar156 + mv1.Scalar5 * mv2.Scalar136 - mv1.Scalar6 * mv2.Scalar135,
            Scalar2356 = mv1.Scalar2 * mv2.Scalar356 - mv1.Scalar3 * mv2.Scalar256 + mv1.Scalar5 * mv2.Scalar236 - mv1.Scalar6 * mv2.Scalar235,
            Scalar1456 = mv1.Scalar1 * mv2.Scalar456 - mv1.Scalar4 * mv2.Scalar156 + mv1.Scalar5 * mv2.Scalar146 - mv1.Scalar6 * mv2.Scalar145,
            Scalar2456 = mv1.Scalar2 * mv2.Scalar456 - mv1.Scalar4 * mv2.Scalar256 + mv1.Scalar5 * mv2.Scalar246 - mv1.Scalar6 * mv2.Scalar245,
            Scalar3456 = mv1.Scalar3 * mv2.Scalar456 - mv1.Scalar4 * mv2.Scalar356 + mv1.Scalar5 * mv2.Scalar346 - mv1.Scalar6 * mv2.Scalar345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector3 Cp(this Ga6KVector1 mv1, Ga6KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector3.Zero;
        
        return new Ga6KVector3
        {
            Scalar123 = -mv1.Scalar4 * mv2.Scalar1234 - mv1.Scalar5 * mv2.Scalar1235 - mv1.Scalar6 * mv2.Scalar1236,
            Scalar124 = mv1.Scalar3 * mv2.Scalar1234 - mv1.Scalar5 * mv2.Scalar1245 - mv1.Scalar6 * mv2.Scalar1246,
            Scalar134 = -mv1.Scalar2 * mv2.Scalar1234 - mv1.Scalar5 * mv2.Scalar1345 - mv1.Scalar6 * mv2.Scalar1346,
            Scalar234 = mv1.Scalar1 * mv2.Scalar1234 - mv1.Scalar5 * mv2.Scalar2345 - mv1.Scalar6 * mv2.Scalar2346,
            Scalar125 = mv1.Scalar3 * mv2.Scalar1235 + mv1.Scalar4 * mv2.Scalar1245 - mv1.Scalar6 * mv2.Scalar1256,
            Scalar135 = -mv1.Scalar2 * mv2.Scalar1235 + mv1.Scalar4 * mv2.Scalar1345 - mv1.Scalar6 * mv2.Scalar1356,
            Scalar235 = mv1.Scalar1 * mv2.Scalar1235 + mv1.Scalar4 * mv2.Scalar2345 - mv1.Scalar6 * mv2.Scalar2356,
            Scalar145 = -mv1.Scalar2 * mv2.Scalar1245 - mv1.Scalar3 * mv2.Scalar1345 - mv1.Scalar6 * mv2.Scalar1456,
            Scalar245 = mv1.Scalar1 * mv2.Scalar1245 - mv1.Scalar3 * mv2.Scalar2345 - mv1.Scalar6 * mv2.Scalar2456,
            Scalar345 = mv1.Scalar1 * mv2.Scalar1345 + mv1.Scalar2 * mv2.Scalar2345 - mv1.Scalar6 * mv2.Scalar3456,
            Scalar126 = mv1.Scalar3 * mv2.Scalar1236 + mv1.Scalar4 * mv2.Scalar1246 + mv1.Scalar5 * mv2.Scalar1256,
            Scalar136 = -mv1.Scalar2 * mv2.Scalar1236 + mv1.Scalar4 * mv2.Scalar1346 + mv1.Scalar5 * mv2.Scalar1356,
            Scalar236 = mv1.Scalar1 * mv2.Scalar1236 + mv1.Scalar4 * mv2.Scalar2346 + mv1.Scalar5 * mv2.Scalar2356,
            Scalar146 = -mv1.Scalar2 * mv2.Scalar1246 - mv1.Scalar3 * mv2.Scalar1346 + mv1.Scalar5 * mv2.Scalar1456,
            Scalar246 = mv1.Scalar1 * mv2.Scalar1246 - mv1.Scalar3 * mv2.Scalar2346 + mv1.Scalar5 * mv2.Scalar2456,
            Scalar346 = mv1.Scalar1 * mv2.Scalar1346 + mv1.Scalar2 * mv2.Scalar2346 + mv1.Scalar5 * mv2.Scalar3456,
            Scalar156 = -mv1.Scalar2 * mv2.Scalar1256 - mv1.Scalar3 * mv2.Scalar1356 - mv1.Scalar4 * mv2.Scalar1456,
            Scalar256 = mv1.Scalar1 * mv2.Scalar1256 - mv1.Scalar3 * mv2.Scalar2356 - mv1.Scalar4 * mv2.Scalar2456,
            Scalar356 = mv1.Scalar1 * mv2.Scalar1356 + mv1.Scalar2 * mv2.Scalar2356 - mv1.Scalar4 * mv2.Scalar3456,
            Scalar456 = mv1.Scalar1 * mv2.Scalar1456 + mv1.Scalar2 * mv2.Scalar2456 + mv1.Scalar3 * mv2.Scalar3456
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector6 Cp(this Ga6KVector1 mv1, Ga6KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector6.Zero;
        
        return new Ga6KVector6
        {
            Scalar123456 = mv1.Scalar1 * mv2.Scalar23456 - mv1.Scalar2 * mv2.Scalar13456 + mv1.Scalar3 * mv2.Scalar12456 - mv1.Scalar4 * mv2.Scalar12356 + mv1.Scalar5 * mv2.Scalar12346 - mv1.Scalar6 * mv2.Scalar12345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector5 Cp(this Ga6KVector1 mv1, Ga6KVector6 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector5.Zero;
        
        return new Ga6KVector5
        {
            Scalar12345 = -mv1.Scalar6 * mv2.Scalar123456,
            Scalar12346 = mv1.Scalar5 * mv2.Scalar123456,
            Scalar12356 = -mv1.Scalar4 * mv2.Scalar123456,
            Scalar12456 = mv1.Scalar3 * mv2.Scalar123456,
            Scalar13456 = -mv1.Scalar2 * mv2.Scalar123456,
            Scalar23456 = mv1.Scalar1 * mv2.Scalar123456
        };
    }
    
    public static Ga6Multivector Cp(this Ga6KVector1 mv1, Ga6Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[3] += mv1.Scalar1 * mv2.KVector1.Scalar2 - mv1.Scalar2 * mv2.KVector1.Scalar1;
            tempScalar[5] += mv1.Scalar1 * mv2.KVector1.Scalar3 - mv1.Scalar3 * mv2.KVector1.Scalar1;
            tempScalar[6] += mv1.Scalar2 * mv2.KVector1.Scalar3 - mv1.Scalar3 * mv2.KVector1.Scalar2;
            tempScalar[9] += mv1.Scalar1 * mv2.KVector1.Scalar4 - mv1.Scalar4 * mv2.KVector1.Scalar1;
            tempScalar[10] += mv1.Scalar2 * mv2.KVector1.Scalar4 - mv1.Scalar4 * mv2.KVector1.Scalar2;
            tempScalar[12] += mv1.Scalar3 * mv2.KVector1.Scalar4 - mv1.Scalar4 * mv2.KVector1.Scalar3;
            tempScalar[17] += mv1.Scalar1 * mv2.KVector1.Scalar5 - mv1.Scalar5 * mv2.KVector1.Scalar1;
            tempScalar[18] += mv1.Scalar2 * mv2.KVector1.Scalar5 - mv1.Scalar5 * mv2.KVector1.Scalar2;
            tempScalar[20] += mv1.Scalar3 * mv2.KVector1.Scalar5 - mv1.Scalar5 * mv2.KVector1.Scalar3;
            tempScalar[24] += mv1.Scalar4 * mv2.KVector1.Scalar5 - mv1.Scalar5 * mv2.KVector1.Scalar4;
            tempScalar[33] += mv1.Scalar1 * mv2.KVector1.Scalar6 - mv1.Scalar6 * mv2.KVector1.Scalar1;
            tempScalar[34] += mv1.Scalar2 * mv2.KVector1.Scalar6 - mv1.Scalar6 * mv2.KVector1.Scalar2;
            tempScalar[36] += mv1.Scalar3 * mv2.KVector1.Scalar6 - mv1.Scalar6 * mv2.KVector1.Scalar3;
            tempScalar[40] += mv1.Scalar4 * mv2.KVector1.Scalar6 - mv1.Scalar6 * mv2.KVector1.Scalar4;
            tempScalar[48] += mv1.Scalar5 * mv2.KVector1.Scalar6 - mv1.Scalar6 * mv2.KVector1.Scalar5;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[1] += -mv1.Scalar2 * mv2.KVector2.Scalar12 - mv1.Scalar3 * mv2.KVector2.Scalar13 - mv1.Scalar4 * mv2.KVector2.Scalar14 - mv1.Scalar5 * mv2.KVector2.Scalar15 - mv1.Scalar6 * mv2.KVector2.Scalar16;
            tempScalar[2] += mv1.Scalar1 * mv2.KVector2.Scalar12 - mv1.Scalar3 * mv2.KVector2.Scalar23 - mv1.Scalar4 * mv2.KVector2.Scalar24 - mv1.Scalar5 * mv2.KVector2.Scalar25 - mv1.Scalar6 * mv2.KVector2.Scalar26;
            tempScalar[4] += mv1.Scalar1 * mv2.KVector2.Scalar13 + mv1.Scalar2 * mv2.KVector2.Scalar23 - mv1.Scalar4 * mv2.KVector2.Scalar34 - mv1.Scalar5 * mv2.KVector2.Scalar35 - mv1.Scalar6 * mv2.KVector2.Scalar36;
            tempScalar[8] += mv1.Scalar1 * mv2.KVector2.Scalar14 + mv1.Scalar2 * mv2.KVector2.Scalar24 + mv1.Scalar3 * mv2.KVector2.Scalar34 - mv1.Scalar5 * mv2.KVector2.Scalar45 - mv1.Scalar6 * mv2.KVector2.Scalar46;
            tempScalar[16] += mv1.Scalar1 * mv2.KVector2.Scalar15 + mv1.Scalar2 * mv2.KVector2.Scalar25 + mv1.Scalar3 * mv2.KVector2.Scalar35 + mv1.Scalar4 * mv2.KVector2.Scalar45 - mv1.Scalar6 * mv2.KVector2.Scalar56;
            tempScalar[32] += mv1.Scalar1 * mv2.KVector2.Scalar16 + mv1.Scalar2 * mv2.KVector2.Scalar26 + mv1.Scalar3 * mv2.KVector2.Scalar36 + mv1.Scalar4 * mv2.KVector2.Scalar46 + mv1.Scalar5 * mv2.KVector2.Scalar56;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[15] += mv1.Scalar1 * mv2.KVector3.Scalar234 - mv1.Scalar2 * mv2.KVector3.Scalar134 + mv1.Scalar3 * mv2.KVector3.Scalar124 - mv1.Scalar4 * mv2.KVector3.Scalar123;
            tempScalar[23] += mv1.Scalar1 * mv2.KVector3.Scalar235 - mv1.Scalar2 * mv2.KVector3.Scalar135 + mv1.Scalar3 * mv2.KVector3.Scalar125 - mv1.Scalar5 * mv2.KVector3.Scalar123;
            tempScalar[27] += mv1.Scalar1 * mv2.KVector3.Scalar245 - mv1.Scalar2 * mv2.KVector3.Scalar145 + mv1.Scalar4 * mv2.KVector3.Scalar125 - mv1.Scalar5 * mv2.KVector3.Scalar124;
            tempScalar[29] += mv1.Scalar1 * mv2.KVector3.Scalar345 - mv1.Scalar3 * mv2.KVector3.Scalar145 + mv1.Scalar4 * mv2.KVector3.Scalar135 - mv1.Scalar5 * mv2.KVector3.Scalar134;
            tempScalar[30] += mv1.Scalar2 * mv2.KVector3.Scalar345 - mv1.Scalar3 * mv2.KVector3.Scalar245 + mv1.Scalar4 * mv2.KVector3.Scalar235 - mv1.Scalar5 * mv2.KVector3.Scalar234;
            tempScalar[39] += mv1.Scalar1 * mv2.KVector3.Scalar236 - mv1.Scalar2 * mv2.KVector3.Scalar136 + mv1.Scalar3 * mv2.KVector3.Scalar126 - mv1.Scalar6 * mv2.KVector3.Scalar123;
            tempScalar[43] += mv1.Scalar1 * mv2.KVector3.Scalar246 - mv1.Scalar2 * mv2.KVector3.Scalar146 + mv1.Scalar4 * mv2.KVector3.Scalar126 - mv1.Scalar6 * mv2.KVector3.Scalar124;
            tempScalar[45] += mv1.Scalar1 * mv2.KVector3.Scalar346 - mv1.Scalar3 * mv2.KVector3.Scalar146 + mv1.Scalar4 * mv2.KVector3.Scalar136 - mv1.Scalar6 * mv2.KVector3.Scalar134;
            tempScalar[46] += mv1.Scalar2 * mv2.KVector3.Scalar346 - mv1.Scalar3 * mv2.KVector3.Scalar246 + mv1.Scalar4 * mv2.KVector3.Scalar236 - mv1.Scalar6 * mv2.KVector3.Scalar234;
            tempScalar[51] += mv1.Scalar1 * mv2.KVector3.Scalar256 - mv1.Scalar2 * mv2.KVector3.Scalar156 + mv1.Scalar5 * mv2.KVector3.Scalar126 - mv1.Scalar6 * mv2.KVector3.Scalar125;
            tempScalar[53] += mv1.Scalar1 * mv2.KVector3.Scalar356 - mv1.Scalar3 * mv2.KVector3.Scalar156 + mv1.Scalar5 * mv2.KVector3.Scalar136 - mv1.Scalar6 * mv2.KVector3.Scalar135;
            tempScalar[54] += mv1.Scalar2 * mv2.KVector3.Scalar356 - mv1.Scalar3 * mv2.KVector3.Scalar256 + mv1.Scalar5 * mv2.KVector3.Scalar236 - mv1.Scalar6 * mv2.KVector3.Scalar235;
            tempScalar[57] += mv1.Scalar1 * mv2.KVector3.Scalar456 - mv1.Scalar4 * mv2.KVector3.Scalar156 + mv1.Scalar5 * mv2.KVector3.Scalar146 - mv1.Scalar6 * mv2.KVector3.Scalar145;
            tempScalar[58] += mv1.Scalar2 * mv2.KVector3.Scalar456 - mv1.Scalar4 * mv2.KVector3.Scalar256 + mv1.Scalar5 * mv2.KVector3.Scalar246 - mv1.Scalar6 * mv2.KVector3.Scalar245;
            tempScalar[60] += mv1.Scalar3 * mv2.KVector3.Scalar456 - mv1.Scalar4 * mv2.KVector3.Scalar356 + mv1.Scalar5 * mv2.KVector3.Scalar346 - mv1.Scalar6 * mv2.KVector3.Scalar345;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[7] += -mv1.Scalar4 * mv2.KVector4.Scalar1234 - mv1.Scalar5 * mv2.KVector4.Scalar1235 - mv1.Scalar6 * mv2.KVector4.Scalar1236;
            tempScalar[11] += mv1.Scalar3 * mv2.KVector4.Scalar1234 - mv1.Scalar5 * mv2.KVector4.Scalar1245 - mv1.Scalar6 * mv2.KVector4.Scalar1246;
            tempScalar[13] += -mv1.Scalar2 * mv2.KVector4.Scalar1234 - mv1.Scalar5 * mv2.KVector4.Scalar1345 - mv1.Scalar6 * mv2.KVector4.Scalar1346;
            tempScalar[14] += mv1.Scalar1 * mv2.KVector4.Scalar1234 - mv1.Scalar5 * mv2.KVector4.Scalar2345 - mv1.Scalar6 * mv2.KVector4.Scalar2346;
            tempScalar[19] += mv1.Scalar3 * mv2.KVector4.Scalar1235 + mv1.Scalar4 * mv2.KVector4.Scalar1245 - mv1.Scalar6 * mv2.KVector4.Scalar1256;
            tempScalar[21] += -mv1.Scalar2 * mv2.KVector4.Scalar1235 + mv1.Scalar4 * mv2.KVector4.Scalar1345 - mv1.Scalar6 * mv2.KVector4.Scalar1356;
            tempScalar[22] += mv1.Scalar1 * mv2.KVector4.Scalar1235 + mv1.Scalar4 * mv2.KVector4.Scalar2345 - mv1.Scalar6 * mv2.KVector4.Scalar2356;
            tempScalar[25] += -mv1.Scalar2 * mv2.KVector4.Scalar1245 - mv1.Scalar3 * mv2.KVector4.Scalar1345 - mv1.Scalar6 * mv2.KVector4.Scalar1456;
            tempScalar[26] += mv1.Scalar1 * mv2.KVector4.Scalar1245 - mv1.Scalar3 * mv2.KVector4.Scalar2345 - mv1.Scalar6 * mv2.KVector4.Scalar2456;
            tempScalar[28] += mv1.Scalar1 * mv2.KVector4.Scalar1345 + mv1.Scalar2 * mv2.KVector4.Scalar2345 - mv1.Scalar6 * mv2.KVector4.Scalar3456;
            tempScalar[35] += mv1.Scalar3 * mv2.KVector4.Scalar1236 + mv1.Scalar4 * mv2.KVector4.Scalar1246 + mv1.Scalar5 * mv2.KVector4.Scalar1256;
            tempScalar[37] += -mv1.Scalar2 * mv2.KVector4.Scalar1236 + mv1.Scalar4 * mv2.KVector4.Scalar1346 + mv1.Scalar5 * mv2.KVector4.Scalar1356;
            tempScalar[38] += mv1.Scalar1 * mv2.KVector4.Scalar1236 + mv1.Scalar4 * mv2.KVector4.Scalar2346 + mv1.Scalar5 * mv2.KVector4.Scalar2356;
            tempScalar[41] += -mv1.Scalar2 * mv2.KVector4.Scalar1246 - mv1.Scalar3 * mv2.KVector4.Scalar1346 + mv1.Scalar5 * mv2.KVector4.Scalar1456;
            tempScalar[42] += mv1.Scalar1 * mv2.KVector4.Scalar1246 - mv1.Scalar3 * mv2.KVector4.Scalar2346 + mv1.Scalar5 * mv2.KVector4.Scalar2456;
            tempScalar[44] += mv1.Scalar1 * mv2.KVector4.Scalar1346 + mv1.Scalar2 * mv2.KVector4.Scalar2346 + mv1.Scalar5 * mv2.KVector4.Scalar3456;
            tempScalar[49] += -mv1.Scalar2 * mv2.KVector4.Scalar1256 - mv1.Scalar3 * mv2.KVector4.Scalar1356 - mv1.Scalar4 * mv2.KVector4.Scalar1456;
            tempScalar[50] += mv1.Scalar1 * mv2.KVector4.Scalar1256 - mv1.Scalar3 * mv2.KVector4.Scalar2356 - mv1.Scalar4 * mv2.KVector4.Scalar2456;
            tempScalar[52] += mv1.Scalar1 * mv2.KVector4.Scalar1356 + mv1.Scalar2 * mv2.KVector4.Scalar2356 - mv1.Scalar4 * mv2.KVector4.Scalar3456;
            tempScalar[56] += mv1.Scalar1 * mv2.KVector4.Scalar1456 + mv1.Scalar2 * mv2.KVector4.Scalar2456 + mv1.Scalar3 * mv2.KVector4.Scalar3456;
        }
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[63] += mv1.Scalar1 * mv2.KVector5.Scalar23456 - mv1.Scalar2 * mv2.KVector5.Scalar13456 + mv1.Scalar3 * mv2.KVector5.Scalar12456 - mv1.Scalar4 * mv2.KVector5.Scalar12356 + mv1.Scalar5 * mv2.KVector5.Scalar12346 - mv1.Scalar6 * mv2.KVector5.Scalar12345;
        }
        
        if (!mv2.KVector6.IsZero())
        {
            tempScalar[31] += -mv1.Scalar6 * mv2.KVector6.Scalar123456;
            tempScalar[47] += mv1.Scalar5 * mv2.KVector6.Scalar123456;
            tempScalar[55] += -mv1.Scalar4 * mv2.KVector6.Scalar123456;
            tempScalar[59] += mv1.Scalar3 * mv2.KVector6.Scalar123456;
            tempScalar[61] += -mv1.Scalar2 * mv2.KVector6.Scalar123456;
            tempScalar[62] += mv1.Scalar1 * mv2.KVector6.Scalar123456;
        }
        
        return Ga6Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector2 mv1, Ga6KVector0 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector1 Cp(this Ga6KVector2 mv1, Ga6KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector1.Zero;
        
        return new Ga6KVector1
        {
            Scalar1 = mv1.Scalar12 * mv2.Scalar2 + mv1.Scalar13 * mv2.Scalar3 + mv1.Scalar14 * mv2.Scalar4 + mv1.Scalar15 * mv2.Scalar5 + mv1.Scalar16 * mv2.Scalar6,
            Scalar2 = -mv1.Scalar12 * mv2.Scalar1 + mv1.Scalar23 * mv2.Scalar3 + mv1.Scalar24 * mv2.Scalar4 + mv1.Scalar25 * mv2.Scalar5 + mv1.Scalar26 * mv2.Scalar6,
            Scalar3 = -mv1.Scalar13 * mv2.Scalar1 - mv1.Scalar23 * mv2.Scalar2 + mv1.Scalar34 * mv2.Scalar4 + mv1.Scalar35 * mv2.Scalar5 + mv1.Scalar36 * mv2.Scalar6,
            Scalar4 = -mv1.Scalar14 * mv2.Scalar1 - mv1.Scalar24 * mv2.Scalar2 - mv1.Scalar34 * mv2.Scalar3 + mv1.Scalar45 * mv2.Scalar5 + mv1.Scalar46 * mv2.Scalar6,
            Scalar5 = -mv1.Scalar15 * mv2.Scalar1 - mv1.Scalar25 * mv2.Scalar2 - mv1.Scalar35 * mv2.Scalar3 - mv1.Scalar45 * mv2.Scalar4 + mv1.Scalar56 * mv2.Scalar6,
            Scalar6 = -mv1.Scalar16 * mv2.Scalar1 - mv1.Scalar26 * mv2.Scalar2 - mv1.Scalar36 * mv2.Scalar3 - mv1.Scalar46 * mv2.Scalar4 - mv1.Scalar56 * mv2.Scalar5
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector2 Cp(this Ga6KVector2 mv1, Ga6KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector2.Zero;
        
        return new Ga6KVector2
        {
            Scalar12 = -mv1.Scalar13 * mv2.Scalar23 + mv1.Scalar23 * mv2.Scalar13 - mv1.Scalar14 * mv2.Scalar24 + mv1.Scalar24 * mv2.Scalar14 - mv1.Scalar15 * mv2.Scalar25 + mv1.Scalar25 * mv2.Scalar15 - mv1.Scalar16 * mv2.Scalar26 + mv1.Scalar26 * mv2.Scalar16,
            Scalar13 = mv1.Scalar12 * mv2.Scalar23 - mv1.Scalar23 * mv2.Scalar12 - mv1.Scalar14 * mv2.Scalar34 + mv1.Scalar34 * mv2.Scalar14 - mv1.Scalar15 * mv2.Scalar35 + mv1.Scalar35 * mv2.Scalar15 - mv1.Scalar16 * mv2.Scalar36 + mv1.Scalar36 * mv2.Scalar16,
            Scalar23 = -mv1.Scalar12 * mv2.Scalar13 + mv1.Scalar13 * mv2.Scalar12 - mv1.Scalar24 * mv2.Scalar34 + mv1.Scalar34 * mv2.Scalar24 - mv1.Scalar25 * mv2.Scalar35 + mv1.Scalar35 * mv2.Scalar25 - mv1.Scalar26 * mv2.Scalar36 + mv1.Scalar36 * mv2.Scalar26,
            Scalar14 = mv1.Scalar12 * mv2.Scalar24 + mv1.Scalar13 * mv2.Scalar34 - mv1.Scalar24 * mv2.Scalar12 - mv1.Scalar34 * mv2.Scalar13 - mv1.Scalar15 * mv2.Scalar45 + mv1.Scalar45 * mv2.Scalar15 - mv1.Scalar16 * mv2.Scalar46 + mv1.Scalar46 * mv2.Scalar16,
            Scalar24 = -mv1.Scalar12 * mv2.Scalar14 + mv1.Scalar23 * mv2.Scalar34 + mv1.Scalar14 * mv2.Scalar12 - mv1.Scalar34 * mv2.Scalar23 - mv1.Scalar25 * mv2.Scalar45 + mv1.Scalar45 * mv2.Scalar25 - mv1.Scalar26 * mv2.Scalar46 + mv1.Scalar46 * mv2.Scalar26,
            Scalar34 = -mv1.Scalar13 * mv2.Scalar14 - mv1.Scalar23 * mv2.Scalar24 + mv1.Scalar14 * mv2.Scalar13 + mv1.Scalar24 * mv2.Scalar23 - mv1.Scalar35 * mv2.Scalar45 + mv1.Scalar45 * mv2.Scalar35 - mv1.Scalar36 * mv2.Scalar46 + mv1.Scalar46 * mv2.Scalar36,
            Scalar15 = mv1.Scalar12 * mv2.Scalar25 + mv1.Scalar13 * mv2.Scalar35 + mv1.Scalar14 * mv2.Scalar45 - mv1.Scalar25 * mv2.Scalar12 - mv1.Scalar35 * mv2.Scalar13 - mv1.Scalar45 * mv2.Scalar14 - mv1.Scalar16 * mv2.Scalar56 + mv1.Scalar56 * mv2.Scalar16,
            Scalar25 = -mv1.Scalar12 * mv2.Scalar15 + mv1.Scalar23 * mv2.Scalar35 + mv1.Scalar24 * mv2.Scalar45 + mv1.Scalar15 * mv2.Scalar12 - mv1.Scalar35 * mv2.Scalar23 - mv1.Scalar45 * mv2.Scalar24 - mv1.Scalar26 * mv2.Scalar56 + mv1.Scalar56 * mv2.Scalar26,
            Scalar35 = -mv1.Scalar13 * mv2.Scalar15 - mv1.Scalar23 * mv2.Scalar25 + mv1.Scalar34 * mv2.Scalar45 + mv1.Scalar15 * mv2.Scalar13 + mv1.Scalar25 * mv2.Scalar23 - mv1.Scalar45 * mv2.Scalar34 - mv1.Scalar36 * mv2.Scalar56 + mv1.Scalar56 * mv2.Scalar36,
            Scalar45 = -mv1.Scalar14 * mv2.Scalar15 - mv1.Scalar24 * mv2.Scalar25 - mv1.Scalar34 * mv2.Scalar35 + mv1.Scalar15 * mv2.Scalar14 + mv1.Scalar25 * mv2.Scalar24 + mv1.Scalar35 * mv2.Scalar34 - mv1.Scalar46 * mv2.Scalar56 + mv1.Scalar56 * mv2.Scalar46,
            Scalar16 = mv1.Scalar12 * mv2.Scalar26 + mv1.Scalar13 * mv2.Scalar36 + mv1.Scalar14 * mv2.Scalar46 + mv1.Scalar15 * mv2.Scalar56 - mv1.Scalar26 * mv2.Scalar12 - mv1.Scalar36 * mv2.Scalar13 - mv1.Scalar46 * mv2.Scalar14 - mv1.Scalar56 * mv2.Scalar15,
            Scalar26 = -mv1.Scalar12 * mv2.Scalar16 + mv1.Scalar23 * mv2.Scalar36 + mv1.Scalar24 * mv2.Scalar46 + mv1.Scalar25 * mv2.Scalar56 + mv1.Scalar16 * mv2.Scalar12 - mv1.Scalar36 * mv2.Scalar23 - mv1.Scalar46 * mv2.Scalar24 - mv1.Scalar56 * mv2.Scalar25,
            Scalar36 = -mv1.Scalar13 * mv2.Scalar16 - mv1.Scalar23 * mv2.Scalar26 + mv1.Scalar34 * mv2.Scalar46 + mv1.Scalar35 * mv2.Scalar56 + mv1.Scalar16 * mv2.Scalar13 + mv1.Scalar26 * mv2.Scalar23 - mv1.Scalar46 * mv2.Scalar34 - mv1.Scalar56 * mv2.Scalar35,
            Scalar46 = -mv1.Scalar14 * mv2.Scalar16 - mv1.Scalar24 * mv2.Scalar26 - mv1.Scalar34 * mv2.Scalar36 + mv1.Scalar45 * mv2.Scalar56 + mv1.Scalar16 * mv2.Scalar14 + mv1.Scalar26 * mv2.Scalar24 + mv1.Scalar36 * mv2.Scalar34 - mv1.Scalar56 * mv2.Scalar45,
            Scalar56 = -mv1.Scalar15 * mv2.Scalar16 - mv1.Scalar25 * mv2.Scalar26 - mv1.Scalar35 * mv2.Scalar36 - mv1.Scalar45 * mv2.Scalar46 + mv1.Scalar16 * mv2.Scalar15 + mv1.Scalar26 * mv2.Scalar25 + mv1.Scalar36 * mv2.Scalar35 + mv1.Scalar46 * mv2.Scalar45
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector3 Cp(this Ga6KVector2 mv1, Ga6KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector3.Zero;
        
        return new Ga6KVector3
        {
            Scalar123 = mv1.Scalar14 * mv2.Scalar234 - mv1.Scalar24 * mv2.Scalar134 + mv1.Scalar34 * mv2.Scalar124 + mv1.Scalar15 * mv2.Scalar235 - mv1.Scalar25 * mv2.Scalar135 + mv1.Scalar35 * mv2.Scalar125 + mv1.Scalar16 * mv2.Scalar236 - mv1.Scalar26 * mv2.Scalar136 + mv1.Scalar36 * mv2.Scalar126,
            Scalar124 = -mv1.Scalar13 * mv2.Scalar234 + mv1.Scalar23 * mv2.Scalar134 - mv1.Scalar34 * mv2.Scalar123 + mv1.Scalar15 * mv2.Scalar245 - mv1.Scalar25 * mv2.Scalar145 + mv1.Scalar45 * mv2.Scalar125 + mv1.Scalar16 * mv2.Scalar246 - mv1.Scalar26 * mv2.Scalar146 + mv1.Scalar46 * mv2.Scalar126,
            Scalar134 = mv1.Scalar12 * mv2.Scalar234 - mv1.Scalar23 * mv2.Scalar124 + mv1.Scalar24 * mv2.Scalar123 + mv1.Scalar15 * mv2.Scalar345 - mv1.Scalar35 * mv2.Scalar145 + mv1.Scalar45 * mv2.Scalar135 + mv1.Scalar16 * mv2.Scalar346 - mv1.Scalar36 * mv2.Scalar146 + mv1.Scalar46 * mv2.Scalar136,
            Scalar234 = -mv1.Scalar12 * mv2.Scalar134 + mv1.Scalar13 * mv2.Scalar124 - mv1.Scalar14 * mv2.Scalar123 + mv1.Scalar25 * mv2.Scalar345 - mv1.Scalar35 * mv2.Scalar245 + mv1.Scalar45 * mv2.Scalar235 + mv1.Scalar26 * mv2.Scalar346 - mv1.Scalar36 * mv2.Scalar246 + mv1.Scalar46 * mv2.Scalar236,
            Scalar125 = -mv1.Scalar13 * mv2.Scalar235 + mv1.Scalar23 * mv2.Scalar135 - mv1.Scalar14 * mv2.Scalar245 + mv1.Scalar24 * mv2.Scalar145 - mv1.Scalar35 * mv2.Scalar123 - mv1.Scalar45 * mv2.Scalar124 + mv1.Scalar16 * mv2.Scalar256 - mv1.Scalar26 * mv2.Scalar156 + mv1.Scalar56 * mv2.Scalar126,
            Scalar135 = mv1.Scalar12 * mv2.Scalar235 - mv1.Scalar23 * mv2.Scalar125 - mv1.Scalar14 * mv2.Scalar345 + mv1.Scalar34 * mv2.Scalar145 + mv1.Scalar25 * mv2.Scalar123 - mv1.Scalar45 * mv2.Scalar134 + mv1.Scalar16 * mv2.Scalar356 - mv1.Scalar36 * mv2.Scalar156 + mv1.Scalar56 * mv2.Scalar136,
            Scalar235 = -mv1.Scalar12 * mv2.Scalar135 + mv1.Scalar13 * mv2.Scalar125 - mv1.Scalar24 * mv2.Scalar345 + mv1.Scalar34 * mv2.Scalar245 - mv1.Scalar15 * mv2.Scalar123 - mv1.Scalar45 * mv2.Scalar234 + mv1.Scalar26 * mv2.Scalar356 - mv1.Scalar36 * mv2.Scalar256 + mv1.Scalar56 * mv2.Scalar236,
            Scalar145 = mv1.Scalar12 * mv2.Scalar245 + mv1.Scalar13 * mv2.Scalar345 - mv1.Scalar24 * mv2.Scalar125 - mv1.Scalar34 * mv2.Scalar135 + mv1.Scalar25 * mv2.Scalar124 + mv1.Scalar35 * mv2.Scalar134 + mv1.Scalar16 * mv2.Scalar456 - mv1.Scalar46 * mv2.Scalar156 + mv1.Scalar56 * mv2.Scalar146,
            Scalar245 = -mv1.Scalar12 * mv2.Scalar145 + mv1.Scalar23 * mv2.Scalar345 + mv1.Scalar14 * mv2.Scalar125 - mv1.Scalar34 * mv2.Scalar235 - mv1.Scalar15 * mv2.Scalar124 + mv1.Scalar35 * mv2.Scalar234 + mv1.Scalar26 * mv2.Scalar456 - mv1.Scalar46 * mv2.Scalar256 + mv1.Scalar56 * mv2.Scalar246,
            Scalar345 = -mv1.Scalar13 * mv2.Scalar145 - mv1.Scalar23 * mv2.Scalar245 + mv1.Scalar14 * mv2.Scalar135 + mv1.Scalar24 * mv2.Scalar235 - mv1.Scalar15 * mv2.Scalar134 - mv1.Scalar25 * mv2.Scalar234 + mv1.Scalar36 * mv2.Scalar456 - mv1.Scalar46 * mv2.Scalar356 + mv1.Scalar56 * mv2.Scalar346,
            Scalar126 = -mv1.Scalar13 * mv2.Scalar236 + mv1.Scalar23 * mv2.Scalar136 - mv1.Scalar14 * mv2.Scalar246 + mv1.Scalar24 * mv2.Scalar146 - mv1.Scalar15 * mv2.Scalar256 + mv1.Scalar25 * mv2.Scalar156 - mv1.Scalar36 * mv2.Scalar123 - mv1.Scalar46 * mv2.Scalar124 - mv1.Scalar56 * mv2.Scalar125,
            Scalar136 = mv1.Scalar12 * mv2.Scalar236 - mv1.Scalar23 * mv2.Scalar126 - mv1.Scalar14 * mv2.Scalar346 + mv1.Scalar34 * mv2.Scalar146 - mv1.Scalar15 * mv2.Scalar356 + mv1.Scalar35 * mv2.Scalar156 + mv1.Scalar26 * mv2.Scalar123 - mv1.Scalar46 * mv2.Scalar134 - mv1.Scalar56 * mv2.Scalar135,
            Scalar236 = -mv1.Scalar12 * mv2.Scalar136 + mv1.Scalar13 * mv2.Scalar126 - mv1.Scalar24 * mv2.Scalar346 + mv1.Scalar34 * mv2.Scalar246 - mv1.Scalar25 * mv2.Scalar356 + mv1.Scalar35 * mv2.Scalar256 - mv1.Scalar16 * mv2.Scalar123 - mv1.Scalar46 * mv2.Scalar234 - mv1.Scalar56 * mv2.Scalar235,
            Scalar146 = mv1.Scalar12 * mv2.Scalar246 + mv1.Scalar13 * mv2.Scalar346 - mv1.Scalar24 * mv2.Scalar126 - mv1.Scalar34 * mv2.Scalar136 - mv1.Scalar15 * mv2.Scalar456 + mv1.Scalar45 * mv2.Scalar156 + mv1.Scalar26 * mv2.Scalar124 + mv1.Scalar36 * mv2.Scalar134 - mv1.Scalar56 * mv2.Scalar145,
            Scalar246 = -mv1.Scalar12 * mv2.Scalar146 + mv1.Scalar23 * mv2.Scalar346 + mv1.Scalar14 * mv2.Scalar126 - mv1.Scalar34 * mv2.Scalar236 - mv1.Scalar25 * mv2.Scalar456 + mv1.Scalar45 * mv2.Scalar256 - mv1.Scalar16 * mv2.Scalar124 + mv1.Scalar36 * mv2.Scalar234 - mv1.Scalar56 * mv2.Scalar245,
            Scalar346 = -mv1.Scalar13 * mv2.Scalar146 - mv1.Scalar23 * mv2.Scalar246 + mv1.Scalar14 * mv2.Scalar136 + mv1.Scalar24 * mv2.Scalar236 - mv1.Scalar35 * mv2.Scalar456 + mv1.Scalar45 * mv2.Scalar356 - mv1.Scalar16 * mv2.Scalar134 - mv1.Scalar26 * mv2.Scalar234 - mv1.Scalar56 * mv2.Scalar345,
            Scalar156 = mv1.Scalar12 * mv2.Scalar256 + mv1.Scalar13 * mv2.Scalar356 + mv1.Scalar14 * mv2.Scalar456 - mv1.Scalar25 * mv2.Scalar126 - mv1.Scalar35 * mv2.Scalar136 - mv1.Scalar45 * mv2.Scalar146 + mv1.Scalar26 * mv2.Scalar125 + mv1.Scalar36 * mv2.Scalar135 + mv1.Scalar46 * mv2.Scalar145,
            Scalar256 = -mv1.Scalar12 * mv2.Scalar156 + mv1.Scalar23 * mv2.Scalar356 + mv1.Scalar24 * mv2.Scalar456 + mv1.Scalar15 * mv2.Scalar126 - mv1.Scalar35 * mv2.Scalar236 - mv1.Scalar45 * mv2.Scalar246 - mv1.Scalar16 * mv2.Scalar125 + mv1.Scalar36 * mv2.Scalar235 + mv1.Scalar46 * mv2.Scalar245,
            Scalar356 = -mv1.Scalar13 * mv2.Scalar156 - mv1.Scalar23 * mv2.Scalar256 + mv1.Scalar34 * mv2.Scalar456 + mv1.Scalar15 * mv2.Scalar136 + mv1.Scalar25 * mv2.Scalar236 - mv1.Scalar45 * mv2.Scalar346 - mv1.Scalar16 * mv2.Scalar135 - mv1.Scalar26 * mv2.Scalar235 + mv1.Scalar46 * mv2.Scalar345,
            Scalar456 = -mv1.Scalar14 * mv2.Scalar156 - mv1.Scalar24 * mv2.Scalar256 - mv1.Scalar34 * mv2.Scalar356 + mv1.Scalar15 * mv2.Scalar146 + mv1.Scalar25 * mv2.Scalar246 + mv1.Scalar35 * mv2.Scalar346 - mv1.Scalar16 * mv2.Scalar145 - mv1.Scalar26 * mv2.Scalar245 - mv1.Scalar36 * mv2.Scalar345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector4 Cp(this Ga6KVector2 mv1, Ga6KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector4.Zero;
        
        return new Ga6KVector4
        {
            Scalar1234 = -mv1.Scalar15 * mv2.Scalar2345 + mv1.Scalar25 * mv2.Scalar1345 - mv1.Scalar35 * mv2.Scalar1245 + mv1.Scalar45 * mv2.Scalar1235 - mv1.Scalar16 * mv2.Scalar2346 + mv1.Scalar26 * mv2.Scalar1346 - mv1.Scalar36 * mv2.Scalar1246 + mv1.Scalar46 * mv2.Scalar1236,
            Scalar1235 = mv1.Scalar14 * mv2.Scalar2345 - mv1.Scalar24 * mv2.Scalar1345 + mv1.Scalar34 * mv2.Scalar1245 - mv1.Scalar45 * mv2.Scalar1234 - mv1.Scalar16 * mv2.Scalar2356 + mv1.Scalar26 * mv2.Scalar1356 - mv1.Scalar36 * mv2.Scalar1256 + mv1.Scalar56 * mv2.Scalar1236,
            Scalar1245 = -mv1.Scalar13 * mv2.Scalar2345 + mv1.Scalar23 * mv2.Scalar1345 - mv1.Scalar34 * mv2.Scalar1235 + mv1.Scalar35 * mv2.Scalar1234 - mv1.Scalar16 * mv2.Scalar2456 + mv1.Scalar26 * mv2.Scalar1456 - mv1.Scalar46 * mv2.Scalar1256 + mv1.Scalar56 * mv2.Scalar1246,
            Scalar1345 = mv1.Scalar12 * mv2.Scalar2345 - mv1.Scalar23 * mv2.Scalar1245 + mv1.Scalar24 * mv2.Scalar1235 - mv1.Scalar25 * mv2.Scalar1234 - mv1.Scalar16 * mv2.Scalar3456 + mv1.Scalar36 * mv2.Scalar1456 - mv1.Scalar46 * mv2.Scalar1356 + mv1.Scalar56 * mv2.Scalar1346,
            Scalar2345 = -mv1.Scalar12 * mv2.Scalar1345 + mv1.Scalar13 * mv2.Scalar1245 - mv1.Scalar14 * mv2.Scalar1235 + mv1.Scalar15 * mv2.Scalar1234 - mv1.Scalar26 * mv2.Scalar3456 + mv1.Scalar36 * mv2.Scalar2456 - mv1.Scalar46 * mv2.Scalar2356 + mv1.Scalar56 * mv2.Scalar2346,
            Scalar1236 = mv1.Scalar14 * mv2.Scalar2346 - mv1.Scalar24 * mv2.Scalar1346 + mv1.Scalar34 * mv2.Scalar1246 + mv1.Scalar15 * mv2.Scalar2356 - mv1.Scalar25 * mv2.Scalar1356 + mv1.Scalar35 * mv2.Scalar1256 - mv1.Scalar46 * mv2.Scalar1234 - mv1.Scalar56 * mv2.Scalar1235,
            Scalar1246 = -mv1.Scalar13 * mv2.Scalar2346 + mv1.Scalar23 * mv2.Scalar1346 - mv1.Scalar34 * mv2.Scalar1236 + mv1.Scalar15 * mv2.Scalar2456 - mv1.Scalar25 * mv2.Scalar1456 + mv1.Scalar45 * mv2.Scalar1256 + mv1.Scalar36 * mv2.Scalar1234 - mv1.Scalar56 * mv2.Scalar1245,
            Scalar1346 = mv1.Scalar12 * mv2.Scalar2346 - mv1.Scalar23 * mv2.Scalar1246 + mv1.Scalar24 * mv2.Scalar1236 + mv1.Scalar15 * mv2.Scalar3456 - mv1.Scalar35 * mv2.Scalar1456 + mv1.Scalar45 * mv2.Scalar1356 - mv1.Scalar26 * mv2.Scalar1234 - mv1.Scalar56 * mv2.Scalar1345,
            Scalar2346 = -mv1.Scalar12 * mv2.Scalar1346 + mv1.Scalar13 * mv2.Scalar1246 - mv1.Scalar14 * mv2.Scalar1236 + mv1.Scalar25 * mv2.Scalar3456 - mv1.Scalar35 * mv2.Scalar2456 + mv1.Scalar45 * mv2.Scalar2356 + mv1.Scalar16 * mv2.Scalar1234 - mv1.Scalar56 * mv2.Scalar2345,
            Scalar1256 = -mv1.Scalar13 * mv2.Scalar2356 + mv1.Scalar23 * mv2.Scalar1356 - mv1.Scalar14 * mv2.Scalar2456 + mv1.Scalar24 * mv2.Scalar1456 - mv1.Scalar35 * mv2.Scalar1236 - mv1.Scalar45 * mv2.Scalar1246 + mv1.Scalar36 * mv2.Scalar1235 + mv1.Scalar46 * mv2.Scalar1245,
            Scalar1356 = mv1.Scalar12 * mv2.Scalar2356 - mv1.Scalar23 * mv2.Scalar1256 - mv1.Scalar14 * mv2.Scalar3456 + mv1.Scalar34 * mv2.Scalar1456 + mv1.Scalar25 * mv2.Scalar1236 - mv1.Scalar45 * mv2.Scalar1346 - mv1.Scalar26 * mv2.Scalar1235 + mv1.Scalar46 * mv2.Scalar1345,
            Scalar2356 = -mv1.Scalar12 * mv2.Scalar1356 + mv1.Scalar13 * mv2.Scalar1256 - mv1.Scalar24 * mv2.Scalar3456 + mv1.Scalar34 * mv2.Scalar2456 - mv1.Scalar15 * mv2.Scalar1236 - mv1.Scalar45 * mv2.Scalar2346 + mv1.Scalar16 * mv2.Scalar1235 + mv1.Scalar46 * mv2.Scalar2345,
            Scalar1456 = mv1.Scalar12 * mv2.Scalar2456 + mv1.Scalar13 * mv2.Scalar3456 - mv1.Scalar24 * mv2.Scalar1256 - mv1.Scalar34 * mv2.Scalar1356 + mv1.Scalar25 * mv2.Scalar1246 + mv1.Scalar35 * mv2.Scalar1346 - mv1.Scalar26 * mv2.Scalar1245 - mv1.Scalar36 * mv2.Scalar1345,
            Scalar2456 = -mv1.Scalar12 * mv2.Scalar1456 + mv1.Scalar23 * mv2.Scalar3456 + mv1.Scalar14 * mv2.Scalar1256 - mv1.Scalar34 * mv2.Scalar2356 - mv1.Scalar15 * mv2.Scalar1246 + mv1.Scalar35 * mv2.Scalar2346 + mv1.Scalar16 * mv2.Scalar1245 - mv1.Scalar36 * mv2.Scalar2345,
            Scalar3456 = -mv1.Scalar13 * mv2.Scalar1456 - mv1.Scalar23 * mv2.Scalar2456 + mv1.Scalar14 * mv2.Scalar1356 + mv1.Scalar24 * mv2.Scalar2356 - mv1.Scalar15 * mv2.Scalar1346 - mv1.Scalar25 * mv2.Scalar2346 + mv1.Scalar16 * mv2.Scalar1345 + mv1.Scalar26 * mv2.Scalar2345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector5 Cp(this Ga6KVector2 mv1, Ga6KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector5.Zero;
        
        return new Ga6KVector5
        {
            Scalar12345 = mv1.Scalar16 * mv2.Scalar23456 - mv1.Scalar26 * mv2.Scalar13456 + mv1.Scalar36 * mv2.Scalar12456 - mv1.Scalar46 * mv2.Scalar12356 + mv1.Scalar56 * mv2.Scalar12346,
            Scalar12346 = -mv1.Scalar15 * mv2.Scalar23456 + mv1.Scalar25 * mv2.Scalar13456 - mv1.Scalar35 * mv2.Scalar12456 + mv1.Scalar45 * mv2.Scalar12356 - mv1.Scalar56 * mv2.Scalar12345,
            Scalar12356 = mv1.Scalar14 * mv2.Scalar23456 - mv1.Scalar24 * mv2.Scalar13456 + mv1.Scalar34 * mv2.Scalar12456 - mv1.Scalar45 * mv2.Scalar12346 + mv1.Scalar46 * mv2.Scalar12345,
            Scalar12456 = -mv1.Scalar13 * mv2.Scalar23456 + mv1.Scalar23 * mv2.Scalar13456 - mv1.Scalar34 * mv2.Scalar12356 + mv1.Scalar35 * mv2.Scalar12346 - mv1.Scalar36 * mv2.Scalar12345,
            Scalar13456 = mv1.Scalar12 * mv2.Scalar23456 - mv1.Scalar23 * mv2.Scalar12456 + mv1.Scalar24 * mv2.Scalar12356 - mv1.Scalar25 * mv2.Scalar12346 + mv1.Scalar26 * mv2.Scalar12345,
            Scalar23456 = -mv1.Scalar12 * mv2.Scalar13456 + mv1.Scalar13 * mv2.Scalar12456 - mv1.Scalar14 * mv2.Scalar12356 + mv1.Scalar15 * mv2.Scalar12346 - mv1.Scalar16 * mv2.Scalar12345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector2 mv1, Ga6KVector6 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    public static Ga6Multivector Cp(this Ga6KVector2 mv1, Ga6Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[1] += mv1.Scalar12 * mv2.KVector1.Scalar2 + mv1.Scalar13 * mv2.KVector1.Scalar3 + mv1.Scalar14 * mv2.KVector1.Scalar4 + mv1.Scalar15 * mv2.KVector1.Scalar5 + mv1.Scalar16 * mv2.KVector1.Scalar6;
            tempScalar[2] += -mv1.Scalar12 * mv2.KVector1.Scalar1 + mv1.Scalar23 * mv2.KVector1.Scalar3 + mv1.Scalar24 * mv2.KVector1.Scalar4 + mv1.Scalar25 * mv2.KVector1.Scalar5 + mv1.Scalar26 * mv2.KVector1.Scalar6;
            tempScalar[4] += -mv1.Scalar13 * mv2.KVector1.Scalar1 - mv1.Scalar23 * mv2.KVector1.Scalar2 + mv1.Scalar34 * mv2.KVector1.Scalar4 + mv1.Scalar35 * mv2.KVector1.Scalar5 + mv1.Scalar36 * mv2.KVector1.Scalar6;
            tempScalar[8] += -mv1.Scalar14 * mv2.KVector1.Scalar1 - mv1.Scalar24 * mv2.KVector1.Scalar2 - mv1.Scalar34 * mv2.KVector1.Scalar3 + mv1.Scalar45 * mv2.KVector1.Scalar5 + mv1.Scalar46 * mv2.KVector1.Scalar6;
            tempScalar[16] += -mv1.Scalar15 * mv2.KVector1.Scalar1 - mv1.Scalar25 * mv2.KVector1.Scalar2 - mv1.Scalar35 * mv2.KVector1.Scalar3 - mv1.Scalar45 * mv2.KVector1.Scalar4 + mv1.Scalar56 * mv2.KVector1.Scalar6;
            tempScalar[32] += -mv1.Scalar16 * mv2.KVector1.Scalar1 - mv1.Scalar26 * mv2.KVector1.Scalar2 - mv1.Scalar36 * mv2.KVector1.Scalar3 - mv1.Scalar46 * mv2.KVector1.Scalar4 - mv1.Scalar56 * mv2.KVector1.Scalar5;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[3] += -mv1.Scalar13 * mv2.KVector2.Scalar23 + mv1.Scalar23 * mv2.KVector2.Scalar13 - mv1.Scalar14 * mv2.KVector2.Scalar24 + mv1.Scalar24 * mv2.KVector2.Scalar14 - mv1.Scalar15 * mv2.KVector2.Scalar25 + mv1.Scalar25 * mv2.KVector2.Scalar15 - mv1.Scalar16 * mv2.KVector2.Scalar26 + mv1.Scalar26 * mv2.KVector2.Scalar16;
            tempScalar[5] += mv1.Scalar12 * mv2.KVector2.Scalar23 - mv1.Scalar23 * mv2.KVector2.Scalar12 - mv1.Scalar14 * mv2.KVector2.Scalar34 + mv1.Scalar34 * mv2.KVector2.Scalar14 - mv1.Scalar15 * mv2.KVector2.Scalar35 + mv1.Scalar35 * mv2.KVector2.Scalar15 - mv1.Scalar16 * mv2.KVector2.Scalar36 + mv1.Scalar36 * mv2.KVector2.Scalar16;
            tempScalar[6] += -mv1.Scalar12 * mv2.KVector2.Scalar13 + mv1.Scalar13 * mv2.KVector2.Scalar12 - mv1.Scalar24 * mv2.KVector2.Scalar34 + mv1.Scalar34 * mv2.KVector2.Scalar24 - mv1.Scalar25 * mv2.KVector2.Scalar35 + mv1.Scalar35 * mv2.KVector2.Scalar25 - mv1.Scalar26 * mv2.KVector2.Scalar36 + mv1.Scalar36 * mv2.KVector2.Scalar26;
            tempScalar[9] += mv1.Scalar12 * mv2.KVector2.Scalar24 + mv1.Scalar13 * mv2.KVector2.Scalar34 - mv1.Scalar24 * mv2.KVector2.Scalar12 - mv1.Scalar34 * mv2.KVector2.Scalar13 - mv1.Scalar15 * mv2.KVector2.Scalar45 + mv1.Scalar45 * mv2.KVector2.Scalar15 - mv1.Scalar16 * mv2.KVector2.Scalar46 + mv1.Scalar46 * mv2.KVector2.Scalar16;
            tempScalar[10] += -mv1.Scalar12 * mv2.KVector2.Scalar14 + mv1.Scalar23 * mv2.KVector2.Scalar34 + mv1.Scalar14 * mv2.KVector2.Scalar12 - mv1.Scalar34 * mv2.KVector2.Scalar23 - mv1.Scalar25 * mv2.KVector2.Scalar45 + mv1.Scalar45 * mv2.KVector2.Scalar25 - mv1.Scalar26 * mv2.KVector2.Scalar46 + mv1.Scalar46 * mv2.KVector2.Scalar26;
            tempScalar[12] += -mv1.Scalar13 * mv2.KVector2.Scalar14 - mv1.Scalar23 * mv2.KVector2.Scalar24 + mv1.Scalar14 * mv2.KVector2.Scalar13 + mv1.Scalar24 * mv2.KVector2.Scalar23 - mv1.Scalar35 * mv2.KVector2.Scalar45 + mv1.Scalar45 * mv2.KVector2.Scalar35 - mv1.Scalar36 * mv2.KVector2.Scalar46 + mv1.Scalar46 * mv2.KVector2.Scalar36;
            tempScalar[17] += mv1.Scalar12 * mv2.KVector2.Scalar25 + mv1.Scalar13 * mv2.KVector2.Scalar35 + mv1.Scalar14 * mv2.KVector2.Scalar45 - mv1.Scalar25 * mv2.KVector2.Scalar12 - mv1.Scalar35 * mv2.KVector2.Scalar13 - mv1.Scalar45 * mv2.KVector2.Scalar14 - mv1.Scalar16 * mv2.KVector2.Scalar56 + mv1.Scalar56 * mv2.KVector2.Scalar16;
            tempScalar[18] += -mv1.Scalar12 * mv2.KVector2.Scalar15 + mv1.Scalar23 * mv2.KVector2.Scalar35 + mv1.Scalar24 * mv2.KVector2.Scalar45 + mv1.Scalar15 * mv2.KVector2.Scalar12 - mv1.Scalar35 * mv2.KVector2.Scalar23 - mv1.Scalar45 * mv2.KVector2.Scalar24 - mv1.Scalar26 * mv2.KVector2.Scalar56 + mv1.Scalar56 * mv2.KVector2.Scalar26;
            tempScalar[20] += -mv1.Scalar13 * mv2.KVector2.Scalar15 - mv1.Scalar23 * mv2.KVector2.Scalar25 + mv1.Scalar34 * mv2.KVector2.Scalar45 + mv1.Scalar15 * mv2.KVector2.Scalar13 + mv1.Scalar25 * mv2.KVector2.Scalar23 - mv1.Scalar45 * mv2.KVector2.Scalar34 - mv1.Scalar36 * mv2.KVector2.Scalar56 + mv1.Scalar56 * mv2.KVector2.Scalar36;
            tempScalar[24] += -mv1.Scalar14 * mv2.KVector2.Scalar15 - mv1.Scalar24 * mv2.KVector2.Scalar25 - mv1.Scalar34 * mv2.KVector2.Scalar35 + mv1.Scalar15 * mv2.KVector2.Scalar14 + mv1.Scalar25 * mv2.KVector2.Scalar24 + mv1.Scalar35 * mv2.KVector2.Scalar34 - mv1.Scalar46 * mv2.KVector2.Scalar56 + mv1.Scalar56 * mv2.KVector2.Scalar46;
            tempScalar[33] += mv1.Scalar12 * mv2.KVector2.Scalar26 + mv1.Scalar13 * mv2.KVector2.Scalar36 + mv1.Scalar14 * mv2.KVector2.Scalar46 + mv1.Scalar15 * mv2.KVector2.Scalar56 - mv1.Scalar26 * mv2.KVector2.Scalar12 - mv1.Scalar36 * mv2.KVector2.Scalar13 - mv1.Scalar46 * mv2.KVector2.Scalar14 - mv1.Scalar56 * mv2.KVector2.Scalar15;
            tempScalar[34] += -mv1.Scalar12 * mv2.KVector2.Scalar16 + mv1.Scalar23 * mv2.KVector2.Scalar36 + mv1.Scalar24 * mv2.KVector2.Scalar46 + mv1.Scalar25 * mv2.KVector2.Scalar56 + mv1.Scalar16 * mv2.KVector2.Scalar12 - mv1.Scalar36 * mv2.KVector2.Scalar23 - mv1.Scalar46 * mv2.KVector2.Scalar24 - mv1.Scalar56 * mv2.KVector2.Scalar25;
            tempScalar[36] += -mv1.Scalar13 * mv2.KVector2.Scalar16 - mv1.Scalar23 * mv2.KVector2.Scalar26 + mv1.Scalar34 * mv2.KVector2.Scalar46 + mv1.Scalar35 * mv2.KVector2.Scalar56 + mv1.Scalar16 * mv2.KVector2.Scalar13 + mv1.Scalar26 * mv2.KVector2.Scalar23 - mv1.Scalar46 * mv2.KVector2.Scalar34 - mv1.Scalar56 * mv2.KVector2.Scalar35;
            tempScalar[40] += -mv1.Scalar14 * mv2.KVector2.Scalar16 - mv1.Scalar24 * mv2.KVector2.Scalar26 - mv1.Scalar34 * mv2.KVector2.Scalar36 + mv1.Scalar45 * mv2.KVector2.Scalar56 + mv1.Scalar16 * mv2.KVector2.Scalar14 + mv1.Scalar26 * mv2.KVector2.Scalar24 + mv1.Scalar36 * mv2.KVector2.Scalar34 - mv1.Scalar56 * mv2.KVector2.Scalar45;
            tempScalar[48] += -mv1.Scalar15 * mv2.KVector2.Scalar16 - mv1.Scalar25 * mv2.KVector2.Scalar26 - mv1.Scalar35 * mv2.KVector2.Scalar36 - mv1.Scalar45 * mv2.KVector2.Scalar46 + mv1.Scalar16 * mv2.KVector2.Scalar15 + mv1.Scalar26 * mv2.KVector2.Scalar25 + mv1.Scalar36 * mv2.KVector2.Scalar35 + mv1.Scalar46 * mv2.KVector2.Scalar45;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[7] += mv1.Scalar14 * mv2.KVector3.Scalar234 - mv1.Scalar24 * mv2.KVector3.Scalar134 + mv1.Scalar34 * mv2.KVector3.Scalar124 + mv1.Scalar15 * mv2.KVector3.Scalar235 - mv1.Scalar25 * mv2.KVector3.Scalar135 + mv1.Scalar35 * mv2.KVector3.Scalar125 + mv1.Scalar16 * mv2.KVector3.Scalar236 - mv1.Scalar26 * mv2.KVector3.Scalar136 + mv1.Scalar36 * mv2.KVector3.Scalar126;
            tempScalar[11] += -mv1.Scalar13 * mv2.KVector3.Scalar234 + mv1.Scalar23 * mv2.KVector3.Scalar134 - mv1.Scalar34 * mv2.KVector3.Scalar123 + mv1.Scalar15 * mv2.KVector3.Scalar245 - mv1.Scalar25 * mv2.KVector3.Scalar145 + mv1.Scalar45 * mv2.KVector3.Scalar125 + mv1.Scalar16 * mv2.KVector3.Scalar246 - mv1.Scalar26 * mv2.KVector3.Scalar146 + mv1.Scalar46 * mv2.KVector3.Scalar126;
            tempScalar[13] += mv1.Scalar12 * mv2.KVector3.Scalar234 - mv1.Scalar23 * mv2.KVector3.Scalar124 + mv1.Scalar24 * mv2.KVector3.Scalar123 + mv1.Scalar15 * mv2.KVector3.Scalar345 - mv1.Scalar35 * mv2.KVector3.Scalar145 + mv1.Scalar45 * mv2.KVector3.Scalar135 + mv1.Scalar16 * mv2.KVector3.Scalar346 - mv1.Scalar36 * mv2.KVector3.Scalar146 + mv1.Scalar46 * mv2.KVector3.Scalar136;
            tempScalar[14] += -mv1.Scalar12 * mv2.KVector3.Scalar134 + mv1.Scalar13 * mv2.KVector3.Scalar124 - mv1.Scalar14 * mv2.KVector3.Scalar123 + mv1.Scalar25 * mv2.KVector3.Scalar345 - mv1.Scalar35 * mv2.KVector3.Scalar245 + mv1.Scalar45 * mv2.KVector3.Scalar235 + mv1.Scalar26 * mv2.KVector3.Scalar346 - mv1.Scalar36 * mv2.KVector3.Scalar246 + mv1.Scalar46 * mv2.KVector3.Scalar236;
            tempScalar[19] += -mv1.Scalar13 * mv2.KVector3.Scalar235 + mv1.Scalar23 * mv2.KVector3.Scalar135 - mv1.Scalar14 * mv2.KVector3.Scalar245 + mv1.Scalar24 * mv2.KVector3.Scalar145 - mv1.Scalar35 * mv2.KVector3.Scalar123 - mv1.Scalar45 * mv2.KVector3.Scalar124 + mv1.Scalar16 * mv2.KVector3.Scalar256 - mv1.Scalar26 * mv2.KVector3.Scalar156 + mv1.Scalar56 * mv2.KVector3.Scalar126;
            tempScalar[21] += mv1.Scalar12 * mv2.KVector3.Scalar235 - mv1.Scalar23 * mv2.KVector3.Scalar125 - mv1.Scalar14 * mv2.KVector3.Scalar345 + mv1.Scalar34 * mv2.KVector3.Scalar145 + mv1.Scalar25 * mv2.KVector3.Scalar123 - mv1.Scalar45 * mv2.KVector3.Scalar134 + mv1.Scalar16 * mv2.KVector3.Scalar356 - mv1.Scalar36 * mv2.KVector3.Scalar156 + mv1.Scalar56 * mv2.KVector3.Scalar136;
            tempScalar[22] += -mv1.Scalar12 * mv2.KVector3.Scalar135 + mv1.Scalar13 * mv2.KVector3.Scalar125 - mv1.Scalar24 * mv2.KVector3.Scalar345 + mv1.Scalar34 * mv2.KVector3.Scalar245 - mv1.Scalar15 * mv2.KVector3.Scalar123 - mv1.Scalar45 * mv2.KVector3.Scalar234 + mv1.Scalar26 * mv2.KVector3.Scalar356 - mv1.Scalar36 * mv2.KVector3.Scalar256 + mv1.Scalar56 * mv2.KVector3.Scalar236;
            tempScalar[25] += mv1.Scalar12 * mv2.KVector3.Scalar245 + mv1.Scalar13 * mv2.KVector3.Scalar345 - mv1.Scalar24 * mv2.KVector3.Scalar125 - mv1.Scalar34 * mv2.KVector3.Scalar135 + mv1.Scalar25 * mv2.KVector3.Scalar124 + mv1.Scalar35 * mv2.KVector3.Scalar134 + mv1.Scalar16 * mv2.KVector3.Scalar456 - mv1.Scalar46 * mv2.KVector3.Scalar156 + mv1.Scalar56 * mv2.KVector3.Scalar146;
            tempScalar[26] += -mv1.Scalar12 * mv2.KVector3.Scalar145 + mv1.Scalar23 * mv2.KVector3.Scalar345 + mv1.Scalar14 * mv2.KVector3.Scalar125 - mv1.Scalar34 * mv2.KVector3.Scalar235 - mv1.Scalar15 * mv2.KVector3.Scalar124 + mv1.Scalar35 * mv2.KVector3.Scalar234 + mv1.Scalar26 * mv2.KVector3.Scalar456 - mv1.Scalar46 * mv2.KVector3.Scalar256 + mv1.Scalar56 * mv2.KVector3.Scalar246;
            tempScalar[28] += -mv1.Scalar13 * mv2.KVector3.Scalar145 - mv1.Scalar23 * mv2.KVector3.Scalar245 + mv1.Scalar14 * mv2.KVector3.Scalar135 + mv1.Scalar24 * mv2.KVector3.Scalar235 - mv1.Scalar15 * mv2.KVector3.Scalar134 - mv1.Scalar25 * mv2.KVector3.Scalar234 + mv1.Scalar36 * mv2.KVector3.Scalar456 - mv1.Scalar46 * mv2.KVector3.Scalar356 + mv1.Scalar56 * mv2.KVector3.Scalar346;
            tempScalar[35] += -mv1.Scalar13 * mv2.KVector3.Scalar236 + mv1.Scalar23 * mv2.KVector3.Scalar136 - mv1.Scalar14 * mv2.KVector3.Scalar246 + mv1.Scalar24 * mv2.KVector3.Scalar146 - mv1.Scalar15 * mv2.KVector3.Scalar256 + mv1.Scalar25 * mv2.KVector3.Scalar156 - mv1.Scalar36 * mv2.KVector3.Scalar123 - mv1.Scalar46 * mv2.KVector3.Scalar124 - mv1.Scalar56 * mv2.KVector3.Scalar125;
            tempScalar[37] += mv1.Scalar12 * mv2.KVector3.Scalar236 - mv1.Scalar23 * mv2.KVector3.Scalar126 - mv1.Scalar14 * mv2.KVector3.Scalar346 + mv1.Scalar34 * mv2.KVector3.Scalar146 - mv1.Scalar15 * mv2.KVector3.Scalar356 + mv1.Scalar35 * mv2.KVector3.Scalar156 + mv1.Scalar26 * mv2.KVector3.Scalar123 - mv1.Scalar46 * mv2.KVector3.Scalar134 - mv1.Scalar56 * mv2.KVector3.Scalar135;
            tempScalar[38] += -mv1.Scalar12 * mv2.KVector3.Scalar136 + mv1.Scalar13 * mv2.KVector3.Scalar126 - mv1.Scalar24 * mv2.KVector3.Scalar346 + mv1.Scalar34 * mv2.KVector3.Scalar246 - mv1.Scalar25 * mv2.KVector3.Scalar356 + mv1.Scalar35 * mv2.KVector3.Scalar256 - mv1.Scalar16 * mv2.KVector3.Scalar123 - mv1.Scalar46 * mv2.KVector3.Scalar234 - mv1.Scalar56 * mv2.KVector3.Scalar235;
            tempScalar[41] += mv1.Scalar12 * mv2.KVector3.Scalar246 + mv1.Scalar13 * mv2.KVector3.Scalar346 - mv1.Scalar24 * mv2.KVector3.Scalar126 - mv1.Scalar34 * mv2.KVector3.Scalar136 - mv1.Scalar15 * mv2.KVector3.Scalar456 + mv1.Scalar45 * mv2.KVector3.Scalar156 + mv1.Scalar26 * mv2.KVector3.Scalar124 + mv1.Scalar36 * mv2.KVector3.Scalar134 - mv1.Scalar56 * mv2.KVector3.Scalar145;
            tempScalar[42] += -mv1.Scalar12 * mv2.KVector3.Scalar146 + mv1.Scalar23 * mv2.KVector3.Scalar346 + mv1.Scalar14 * mv2.KVector3.Scalar126 - mv1.Scalar34 * mv2.KVector3.Scalar236 - mv1.Scalar25 * mv2.KVector3.Scalar456 + mv1.Scalar45 * mv2.KVector3.Scalar256 - mv1.Scalar16 * mv2.KVector3.Scalar124 + mv1.Scalar36 * mv2.KVector3.Scalar234 - mv1.Scalar56 * mv2.KVector3.Scalar245;
            tempScalar[44] += -mv1.Scalar13 * mv2.KVector3.Scalar146 - mv1.Scalar23 * mv2.KVector3.Scalar246 + mv1.Scalar14 * mv2.KVector3.Scalar136 + mv1.Scalar24 * mv2.KVector3.Scalar236 - mv1.Scalar35 * mv2.KVector3.Scalar456 + mv1.Scalar45 * mv2.KVector3.Scalar356 - mv1.Scalar16 * mv2.KVector3.Scalar134 - mv1.Scalar26 * mv2.KVector3.Scalar234 - mv1.Scalar56 * mv2.KVector3.Scalar345;
            tempScalar[49] += mv1.Scalar12 * mv2.KVector3.Scalar256 + mv1.Scalar13 * mv2.KVector3.Scalar356 + mv1.Scalar14 * mv2.KVector3.Scalar456 - mv1.Scalar25 * mv2.KVector3.Scalar126 - mv1.Scalar35 * mv2.KVector3.Scalar136 - mv1.Scalar45 * mv2.KVector3.Scalar146 + mv1.Scalar26 * mv2.KVector3.Scalar125 + mv1.Scalar36 * mv2.KVector3.Scalar135 + mv1.Scalar46 * mv2.KVector3.Scalar145;
            tempScalar[50] += -mv1.Scalar12 * mv2.KVector3.Scalar156 + mv1.Scalar23 * mv2.KVector3.Scalar356 + mv1.Scalar24 * mv2.KVector3.Scalar456 + mv1.Scalar15 * mv2.KVector3.Scalar126 - mv1.Scalar35 * mv2.KVector3.Scalar236 - mv1.Scalar45 * mv2.KVector3.Scalar246 - mv1.Scalar16 * mv2.KVector3.Scalar125 + mv1.Scalar36 * mv2.KVector3.Scalar235 + mv1.Scalar46 * mv2.KVector3.Scalar245;
            tempScalar[52] += -mv1.Scalar13 * mv2.KVector3.Scalar156 - mv1.Scalar23 * mv2.KVector3.Scalar256 + mv1.Scalar34 * mv2.KVector3.Scalar456 + mv1.Scalar15 * mv2.KVector3.Scalar136 + mv1.Scalar25 * mv2.KVector3.Scalar236 - mv1.Scalar45 * mv2.KVector3.Scalar346 - mv1.Scalar16 * mv2.KVector3.Scalar135 - mv1.Scalar26 * mv2.KVector3.Scalar235 + mv1.Scalar46 * mv2.KVector3.Scalar345;
            tempScalar[56] += -mv1.Scalar14 * mv2.KVector3.Scalar156 - mv1.Scalar24 * mv2.KVector3.Scalar256 - mv1.Scalar34 * mv2.KVector3.Scalar356 + mv1.Scalar15 * mv2.KVector3.Scalar146 + mv1.Scalar25 * mv2.KVector3.Scalar246 + mv1.Scalar35 * mv2.KVector3.Scalar346 - mv1.Scalar16 * mv2.KVector3.Scalar145 - mv1.Scalar26 * mv2.KVector3.Scalar245 - mv1.Scalar36 * mv2.KVector3.Scalar345;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[15] += -mv1.Scalar15 * mv2.KVector4.Scalar2345 + mv1.Scalar25 * mv2.KVector4.Scalar1345 - mv1.Scalar35 * mv2.KVector4.Scalar1245 + mv1.Scalar45 * mv2.KVector4.Scalar1235 - mv1.Scalar16 * mv2.KVector4.Scalar2346 + mv1.Scalar26 * mv2.KVector4.Scalar1346 - mv1.Scalar36 * mv2.KVector4.Scalar1246 + mv1.Scalar46 * mv2.KVector4.Scalar1236;
            tempScalar[23] += mv1.Scalar14 * mv2.KVector4.Scalar2345 - mv1.Scalar24 * mv2.KVector4.Scalar1345 + mv1.Scalar34 * mv2.KVector4.Scalar1245 - mv1.Scalar45 * mv2.KVector4.Scalar1234 - mv1.Scalar16 * mv2.KVector4.Scalar2356 + mv1.Scalar26 * mv2.KVector4.Scalar1356 - mv1.Scalar36 * mv2.KVector4.Scalar1256 + mv1.Scalar56 * mv2.KVector4.Scalar1236;
            tempScalar[27] += -mv1.Scalar13 * mv2.KVector4.Scalar2345 + mv1.Scalar23 * mv2.KVector4.Scalar1345 - mv1.Scalar34 * mv2.KVector4.Scalar1235 + mv1.Scalar35 * mv2.KVector4.Scalar1234 - mv1.Scalar16 * mv2.KVector4.Scalar2456 + mv1.Scalar26 * mv2.KVector4.Scalar1456 - mv1.Scalar46 * mv2.KVector4.Scalar1256 + mv1.Scalar56 * mv2.KVector4.Scalar1246;
            tempScalar[29] += mv1.Scalar12 * mv2.KVector4.Scalar2345 - mv1.Scalar23 * mv2.KVector4.Scalar1245 + mv1.Scalar24 * mv2.KVector4.Scalar1235 - mv1.Scalar25 * mv2.KVector4.Scalar1234 - mv1.Scalar16 * mv2.KVector4.Scalar3456 + mv1.Scalar36 * mv2.KVector4.Scalar1456 - mv1.Scalar46 * mv2.KVector4.Scalar1356 + mv1.Scalar56 * mv2.KVector4.Scalar1346;
            tempScalar[30] += -mv1.Scalar12 * mv2.KVector4.Scalar1345 + mv1.Scalar13 * mv2.KVector4.Scalar1245 - mv1.Scalar14 * mv2.KVector4.Scalar1235 + mv1.Scalar15 * mv2.KVector4.Scalar1234 - mv1.Scalar26 * mv2.KVector4.Scalar3456 + mv1.Scalar36 * mv2.KVector4.Scalar2456 - mv1.Scalar46 * mv2.KVector4.Scalar2356 + mv1.Scalar56 * mv2.KVector4.Scalar2346;
            tempScalar[39] += mv1.Scalar14 * mv2.KVector4.Scalar2346 - mv1.Scalar24 * mv2.KVector4.Scalar1346 + mv1.Scalar34 * mv2.KVector4.Scalar1246 + mv1.Scalar15 * mv2.KVector4.Scalar2356 - mv1.Scalar25 * mv2.KVector4.Scalar1356 + mv1.Scalar35 * mv2.KVector4.Scalar1256 - mv1.Scalar46 * mv2.KVector4.Scalar1234 - mv1.Scalar56 * mv2.KVector4.Scalar1235;
            tempScalar[43] += -mv1.Scalar13 * mv2.KVector4.Scalar2346 + mv1.Scalar23 * mv2.KVector4.Scalar1346 - mv1.Scalar34 * mv2.KVector4.Scalar1236 + mv1.Scalar15 * mv2.KVector4.Scalar2456 - mv1.Scalar25 * mv2.KVector4.Scalar1456 + mv1.Scalar45 * mv2.KVector4.Scalar1256 + mv1.Scalar36 * mv2.KVector4.Scalar1234 - mv1.Scalar56 * mv2.KVector4.Scalar1245;
            tempScalar[45] += mv1.Scalar12 * mv2.KVector4.Scalar2346 - mv1.Scalar23 * mv2.KVector4.Scalar1246 + mv1.Scalar24 * mv2.KVector4.Scalar1236 + mv1.Scalar15 * mv2.KVector4.Scalar3456 - mv1.Scalar35 * mv2.KVector4.Scalar1456 + mv1.Scalar45 * mv2.KVector4.Scalar1356 - mv1.Scalar26 * mv2.KVector4.Scalar1234 - mv1.Scalar56 * mv2.KVector4.Scalar1345;
            tempScalar[46] += -mv1.Scalar12 * mv2.KVector4.Scalar1346 + mv1.Scalar13 * mv2.KVector4.Scalar1246 - mv1.Scalar14 * mv2.KVector4.Scalar1236 + mv1.Scalar25 * mv2.KVector4.Scalar3456 - mv1.Scalar35 * mv2.KVector4.Scalar2456 + mv1.Scalar45 * mv2.KVector4.Scalar2356 + mv1.Scalar16 * mv2.KVector4.Scalar1234 - mv1.Scalar56 * mv2.KVector4.Scalar2345;
            tempScalar[51] += -mv1.Scalar13 * mv2.KVector4.Scalar2356 + mv1.Scalar23 * mv2.KVector4.Scalar1356 - mv1.Scalar14 * mv2.KVector4.Scalar2456 + mv1.Scalar24 * mv2.KVector4.Scalar1456 - mv1.Scalar35 * mv2.KVector4.Scalar1236 - mv1.Scalar45 * mv2.KVector4.Scalar1246 + mv1.Scalar36 * mv2.KVector4.Scalar1235 + mv1.Scalar46 * mv2.KVector4.Scalar1245;
            tempScalar[53] += mv1.Scalar12 * mv2.KVector4.Scalar2356 - mv1.Scalar23 * mv2.KVector4.Scalar1256 - mv1.Scalar14 * mv2.KVector4.Scalar3456 + mv1.Scalar34 * mv2.KVector4.Scalar1456 + mv1.Scalar25 * mv2.KVector4.Scalar1236 - mv1.Scalar45 * mv2.KVector4.Scalar1346 - mv1.Scalar26 * mv2.KVector4.Scalar1235 + mv1.Scalar46 * mv2.KVector4.Scalar1345;
            tempScalar[54] += -mv1.Scalar12 * mv2.KVector4.Scalar1356 + mv1.Scalar13 * mv2.KVector4.Scalar1256 - mv1.Scalar24 * mv2.KVector4.Scalar3456 + mv1.Scalar34 * mv2.KVector4.Scalar2456 - mv1.Scalar15 * mv2.KVector4.Scalar1236 - mv1.Scalar45 * mv2.KVector4.Scalar2346 + mv1.Scalar16 * mv2.KVector4.Scalar1235 + mv1.Scalar46 * mv2.KVector4.Scalar2345;
            tempScalar[57] += mv1.Scalar12 * mv2.KVector4.Scalar2456 + mv1.Scalar13 * mv2.KVector4.Scalar3456 - mv1.Scalar24 * mv2.KVector4.Scalar1256 - mv1.Scalar34 * mv2.KVector4.Scalar1356 + mv1.Scalar25 * mv2.KVector4.Scalar1246 + mv1.Scalar35 * mv2.KVector4.Scalar1346 - mv1.Scalar26 * mv2.KVector4.Scalar1245 - mv1.Scalar36 * mv2.KVector4.Scalar1345;
            tempScalar[58] += -mv1.Scalar12 * mv2.KVector4.Scalar1456 + mv1.Scalar23 * mv2.KVector4.Scalar3456 + mv1.Scalar14 * mv2.KVector4.Scalar1256 - mv1.Scalar34 * mv2.KVector4.Scalar2356 - mv1.Scalar15 * mv2.KVector4.Scalar1246 + mv1.Scalar35 * mv2.KVector4.Scalar2346 + mv1.Scalar16 * mv2.KVector4.Scalar1245 - mv1.Scalar36 * mv2.KVector4.Scalar2345;
            tempScalar[60] += -mv1.Scalar13 * mv2.KVector4.Scalar1456 - mv1.Scalar23 * mv2.KVector4.Scalar2456 + mv1.Scalar14 * mv2.KVector4.Scalar1356 + mv1.Scalar24 * mv2.KVector4.Scalar2356 - mv1.Scalar15 * mv2.KVector4.Scalar1346 - mv1.Scalar25 * mv2.KVector4.Scalar2346 + mv1.Scalar16 * mv2.KVector4.Scalar1345 + mv1.Scalar26 * mv2.KVector4.Scalar2345;
        }
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[31] += mv1.Scalar16 * mv2.KVector5.Scalar23456 - mv1.Scalar26 * mv2.KVector5.Scalar13456 + mv1.Scalar36 * mv2.KVector5.Scalar12456 - mv1.Scalar46 * mv2.KVector5.Scalar12356 + mv1.Scalar56 * mv2.KVector5.Scalar12346;
            tempScalar[47] += -mv1.Scalar15 * mv2.KVector5.Scalar23456 + mv1.Scalar25 * mv2.KVector5.Scalar13456 - mv1.Scalar35 * mv2.KVector5.Scalar12456 + mv1.Scalar45 * mv2.KVector5.Scalar12356 - mv1.Scalar56 * mv2.KVector5.Scalar12345;
            tempScalar[55] += mv1.Scalar14 * mv2.KVector5.Scalar23456 - mv1.Scalar24 * mv2.KVector5.Scalar13456 + mv1.Scalar34 * mv2.KVector5.Scalar12456 - mv1.Scalar45 * mv2.KVector5.Scalar12346 + mv1.Scalar46 * mv2.KVector5.Scalar12345;
            tempScalar[59] += -mv1.Scalar13 * mv2.KVector5.Scalar23456 + mv1.Scalar23 * mv2.KVector5.Scalar13456 - mv1.Scalar34 * mv2.KVector5.Scalar12356 + mv1.Scalar35 * mv2.KVector5.Scalar12346 - mv1.Scalar36 * mv2.KVector5.Scalar12345;
            tempScalar[61] += mv1.Scalar12 * mv2.KVector5.Scalar23456 - mv1.Scalar23 * mv2.KVector5.Scalar12456 + mv1.Scalar24 * mv2.KVector5.Scalar12356 - mv1.Scalar25 * mv2.KVector5.Scalar12346 + mv1.Scalar26 * mv2.KVector5.Scalar12345;
            tempScalar[62] += -mv1.Scalar12 * mv2.KVector5.Scalar13456 + mv1.Scalar13 * mv2.KVector5.Scalar12456 - mv1.Scalar14 * mv2.KVector5.Scalar12356 + mv1.Scalar15 * mv2.KVector5.Scalar12346 - mv1.Scalar16 * mv2.KVector5.Scalar12345;
        }
        
        return Ga6Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector3 mv1, Ga6KVector0 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector4 Cp(this Ga6KVector3 mv1, Ga6KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector4.Zero;
        
        return new Ga6KVector4
        {
            Scalar1234 = mv1.Scalar123 * mv2.Scalar4 - mv1.Scalar124 * mv2.Scalar3 + mv1.Scalar134 * mv2.Scalar2 - mv1.Scalar234 * mv2.Scalar1,
            Scalar1235 = mv1.Scalar123 * mv2.Scalar5 - mv1.Scalar125 * mv2.Scalar3 + mv1.Scalar135 * mv2.Scalar2 - mv1.Scalar235 * mv2.Scalar1,
            Scalar1245 = mv1.Scalar124 * mv2.Scalar5 - mv1.Scalar125 * mv2.Scalar4 + mv1.Scalar145 * mv2.Scalar2 - mv1.Scalar245 * mv2.Scalar1,
            Scalar1345 = mv1.Scalar134 * mv2.Scalar5 - mv1.Scalar135 * mv2.Scalar4 + mv1.Scalar145 * mv2.Scalar3 - mv1.Scalar345 * mv2.Scalar1,
            Scalar2345 = mv1.Scalar234 * mv2.Scalar5 - mv1.Scalar235 * mv2.Scalar4 + mv1.Scalar245 * mv2.Scalar3 - mv1.Scalar345 * mv2.Scalar2,
            Scalar1236 = mv1.Scalar123 * mv2.Scalar6 - mv1.Scalar126 * mv2.Scalar3 + mv1.Scalar136 * mv2.Scalar2 - mv1.Scalar236 * mv2.Scalar1,
            Scalar1246 = mv1.Scalar124 * mv2.Scalar6 - mv1.Scalar126 * mv2.Scalar4 + mv1.Scalar146 * mv2.Scalar2 - mv1.Scalar246 * mv2.Scalar1,
            Scalar1346 = mv1.Scalar134 * mv2.Scalar6 - mv1.Scalar136 * mv2.Scalar4 + mv1.Scalar146 * mv2.Scalar3 - mv1.Scalar346 * mv2.Scalar1,
            Scalar2346 = mv1.Scalar234 * mv2.Scalar6 - mv1.Scalar236 * mv2.Scalar4 + mv1.Scalar246 * mv2.Scalar3 - mv1.Scalar346 * mv2.Scalar2,
            Scalar1256 = mv1.Scalar125 * mv2.Scalar6 - mv1.Scalar126 * mv2.Scalar5 + mv1.Scalar156 * mv2.Scalar2 - mv1.Scalar256 * mv2.Scalar1,
            Scalar1356 = mv1.Scalar135 * mv2.Scalar6 - mv1.Scalar136 * mv2.Scalar5 + mv1.Scalar156 * mv2.Scalar3 - mv1.Scalar356 * mv2.Scalar1,
            Scalar2356 = mv1.Scalar235 * mv2.Scalar6 - mv1.Scalar236 * mv2.Scalar5 + mv1.Scalar256 * mv2.Scalar3 - mv1.Scalar356 * mv2.Scalar2,
            Scalar1456 = mv1.Scalar145 * mv2.Scalar6 - mv1.Scalar146 * mv2.Scalar5 + mv1.Scalar156 * mv2.Scalar4 - mv1.Scalar456 * mv2.Scalar1,
            Scalar2456 = mv1.Scalar245 * mv2.Scalar6 - mv1.Scalar246 * mv2.Scalar5 + mv1.Scalar256 * mv2.Scalar4 - mv1.Scalar456 * mv2.Scalar2,
            Scalar3456 = mv1.Scalar345 * mv2.Scalar6 - mv1.Scalar346 * mv2.Scalar5 + mv1.Scalar356 * mv2.Scalar4 - mv1.Scalar456 * mv2.Scalar3
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector3 Cp(this Ga6KVector3 mv1, Ga6KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector3.Zero;
        
        return new Ga6KVector3
        {
            Scalar123 = -mv1.Scalar124 * mv2.Scalar34 + mv1.Scalar134 * mv2.Scalar24 - mv1.Scalar234 * mv2.Scalar14 - mv1.Scalar125 * mv2.Scalar35 + mv1.Scalar135 * mv2.Scalar25 - mv1.Scalar235 * mv2.Scalar15 - mv1.Scalar126 * mv2.Scalar36 + mv1.Scalar136 * mv2.Scalar26 - mv1.Scalar236 * mv2.Scalar16,
            Scalar124 = mv1.Scalar123 * mv2.Scalar34 - mv1.Scalar134 * mv2.Scalar23 + mv1.Scalar234 * mv2.Scalar13 - mv1.Scalar125 * mv2.Scalar45 + mv1.Scalar145 * mv2.Scalar25 - mv1.Scalar245 * mv2.Scalar15 - mv1.Scalar126 * mv2.Scalar46 + mv1.Scalar146 * mv2.Scalar26 - mv1.Scalar246 * mv2.Scalar16,
            Scalar134 = -mv1.Scalar123 * mv2.Scalar24 + mv1.Scalar124 * mv2.Scalar23 - mv1.Scalar234 * mv2.Scalar12 - mv1.Scalar135 * mv2.Scalar45 + mv1.Scalar145 * mv2.Scalar35 - mv1.Scalar345 * mv2.Scalar15 - mv1.Scalar136 * mv2.Scalar46 + mv1.Scalar146 * mv2.Scalar36 - mv1.Scalar346 * mv2.Scalar16,
            Scalar234 = mv1.Scalar123 * mv2.Scalar14 - mv1.Scalar124 * mv2.Scalar13 + mv1.Scalar134 * mv2.Scalar12 - mv1.Scalar235 * mv2.Scalar45 + mv1.Scalar245 * mv2.Scalar35 - mv1.Scalar345 * mv2.Scalar25 - mv1.Scalar236 * mv2.Scalar46 + mv1.Scalar246 * mv2.Scalar36 - mv1.Scalar346 * mv2.Scalar26,
            Scalar125 = mv1.Scalar123 * mv2.Scalar35 + mv1.Scalar124 * mv2.Scalar45 - mv1.Scalar135 * mv2.Scalar23 + mv1.Scalar235 * mv2.Scalar13 - mv1.Scalar145 * mv2.Scalar24 + mv1.Scalar245 * mv2.Scalar14 - mv1.Scalar126 * mv2.Scalar56 + mv1.Scalar156 * mv2.Scalar26 - mv1.Scalar256 * mv2.Scalar16,
            Scalar135 = -mv1.Scalar123 * mv2.Scalar25 + mv1.Scalar134 * mv2.Scalar45 + mv1.Scalar125 * mv2.Scalar23 - mv1.Scalar235 * mv2.Scalar12 - mv1.Scalar145 * mv2.Scalar34 + mv1.Scalar345 * mv2.Scalar14 - mv1.Scalar136 * mv2.Scalar56 + mv1.Scalar156 * mv2.Scalar36 - mv1.Scalar356 * mv2.Scalar16,
            Scalar235 = mv1.Scalar123 * mv2.Scalar15 + mv1.Scalar234 * mv2.Scalar45 - mv1.Scalar125 * mv2.Scalar13 + mv1.Scalar135 * mv2.Scalar12 - mv1.Scalar245 * mv2.Scalar34 + mv1.Scalar345 * mv2.Scalar24 - mv1.Scalar236 * mv2.Scalar56 + mv1.Scalar256 * mv2.Scalar36 - mv1.Scalar356 * mv2.Scalar26,
            Scalar145 = -mv1.Scalar124 * mv2.Scalar25 - mv1.Scalar134 * mv2.Scalar35 + mv1.Scalar125 * mv2.Scalar24 + mv1.Scalar135 * mv2.Scalar34 - mv1.Scalar245 * mv2.Scalar12 - mv1.Scalar345 * mv2.Scalar13 - mv1.Scalar146 * mv2.Scalar56 + mv1.Scalar156 * mv2.Scalar46 - mv1.Scalar456 * mv2.Scalar16,
            Scalar245 = mv1.Scalar124 * mv2.Scalar15 - mv1.Scalar234 * mv2.Scalar35 - mv1.Scalar125 * mv2.Scalar14 + mv1.Scalar235 * mv2.Scalar34 + mv1.Scalar145 * mv2.Scalar12 - mv1.Scalar345 * mv2.Scalar23 - mv1.Scalar246 * mv2.Scalar56 + mv1.Scalar256 * mv2.Scalar46 - mv1.Scalar456 * mv2.Scalar26,
            Scalar345 = mv1.Scalar134 * mv2.Scalar15 + mv1.Scalar234 * mv2.Scalar25 - mv1.Scalar135 * mv2.Scalar14 - mv1.Scalar235 * mv2.Scalar24 + mv1.Scalar145 * mv2.Scalar13 + mv1.Scalar245 * mv2.Scalar23 - mv1.Scalar346 * mv2.Scalar56 + mv1.Scalar356 * mv2.Scalar46 - mv1.Scalar456 * mv2.Scalar36,
            Scalar126 = mv1.Scalar123 * mv2.Scalar36 + mv1.Scalar124 * mv2.Scalar46 + mv1.Scalar125 * mv2.Scalar56 - mv1.Scalar136 * mv2.Scalar23 + mv1.Scalar236 * mv2.Scalar13 - mv1.Scalar146 * mv2.Scalar24 + mv1.Scalar246 * mv2.Scalar14 - mv1.Scalar156 * mv2.Scalar25 + mv1.Scalar256 * mv2.Scalar15,
            Scalar136 = -mv1.Scalar123 * mv2.Scalar26 + mv1.Scalar134 * mv2.Scalar46 + mv1.Scalar135 * mv2.Scalar56 + mv1.Scalar126 * mv2.Scalar23 - mv1.Scalar236 * mv2.Scalar12 - mv1.Scalar146 * mv2.Scalar34 + mv1.Scalar346 * mv2.Scalar14 - mv1.Scalar156 * mv2.Scalar35 + mv1.Scalar356 * mv2.Scalar15,
            Scalar236 = mv1.Scalar123 * mv2.Scalar16 + mv1.Scalar234 * mv2.Scalar46 + mv1.Scalar235 * mv2.Scalar56 - mv1.Scalar126 * mv2.Scalar13 + mv1.Scalar136 * mv2.Scalar12 - mv1.Scalar246 * mv2.Scalar34 + mv1.Scalar346 * mv2.Scalar24 - mv1.Scalar256 * mv2.Scalar35 + mv1.Scalar356 * mv2.Scalar25,
            Scalar146 = -mv1.Scalar124 * mv2.Scalar26 - mv1.Scalar134 * mv2.Scalar36 + mv1.Scalar145 * mv2.Scalar56 + mv1.Scalar126 * mv2.Scalar24 + mv1.Scalar136 * mv2.Scalar34 - mv1.Scalar246 * mv2.Scalar12 - mv1.Scalar346 * mv2.Scalar13 - mv1.Scalar156 * mv2.Scalar45 + mv1.Scalar456 * mv2.Scalar15,
            Scalar246 = mv1.Scalar124 * mv2.Scalar16 - mv1.Scalar234 * mv2.Scalar36 + mv1.Scalar245 * mv2.Scalar56 - mv1.Scalar126 * mv2.Scalar14 + mv1.Scalar236 * mv2.Scalar34 + mv1.Scalar146 * mv2.Scalar12 - mv1.Scalar346 * mv2.Scalar23 - mv1.Scalar256 * mv2.Scalar45 + mv1.Scalar456 * mv2.Scalar25,
            Scalar346 = mv1.Scalar134 * mv2.Scalar16 + mv1.Scalar234 * mv2.Scalar26 + mv1.Scalar345 * mv2.Scalar56 - mv1.Scalar136 * mv2.Scalar14 - mv1.Scalar236 * mv2.Scalar24 + mv1.Scalar146 * mv2.Scalar13 + mv1.Scalar246 * mv2.Scalar23 - mv1.Scalar356 * mv2.Scalar45 + mv1.Scalar456 * mv2.Scalar35,
            Scalar156 = -mv1.Scalar125 * mv2.Scalar26 - mv1.Scalar135 * mv2.Scalar36 - mv1.Scalar145 * mv2.Scalar46 + mv1.Scalar126 * mv2.Scalar25 + mv1.Scalar136 * mv2.Scalar35 + mv1.Scalar146 * mv2.Scalar45 - mv1.Scalar256 * mv2.Scalar12 - mv1.Scalar356 * mv2.Scalar13 - mv1.Scalar456 * mv2.Scalar14,
            Scalar256 = mv1.Scalar125 * mv2.Scalar16 - mv1.Scalar235 * mv2.Scalar36 - mv1.Scalar245 * mv2.Scalar46 - mv1.Scalar126 * mv2.Scalar15 + mv1.Scalar236 * mv2.Scalar35 + mv1.Scalar246 * mv2.Scalar45 + mv1.Scalar156 * mv2.Scalar12 - mv1.Scalar356 * mv2.Scalar23 - mv1.Scalar456 * mv2.Scalar24,
            Scalar356 = mv1.Scalar135 * mv2.Scalar16 + mv1.Scalar235 * mv2.Scalar26 - mv1.Scalar345 * mv2.Scalar46 - mv1.Scalar136 * mv2.Scalar15 - mv1.Scalar236 * mv2.Scalar25 + mv1.Scalar346 * mv2.Scalar45 + mv1.Scalar156 * mv2.Scalar13 + mv1.Scalar256 * mv2.Scalar23 - mv1.Scalar456 * mv2.Scalar34,
            Scalar456 = mv1.Scalar145 * mv2.Scalar16 + mv1.Scalar245 * mv2.Scalar26 + mv1.Scalar345 * mv2.Scalar36 - mv1.Scalar146 * mv2.Scalar15 - mv1.Scalar246 * mv2.Scalar25 - mv1.Scalar346 * mv2.Scalar35 + mv1.Scalar156 * mv2.Scalar14 + mv1.Scalar256 * mv2.Scalar24 + mv1.Scalar356 * mv2.Scalar34
        };
    }
    
    public static Ga6Multivector Cp(this Ga6KVector3 mv1, Ga6KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv2.IsZero())
        {
            tempScalar[3] += -mv1.Scalar134 * mv2.Scalar234 + mv1.Scalar234 * mv2.Scalar134 - mv1.Scalar135 * mv2.Scalar235 + mv1.Scalar235 * mv2.Scalar135 - mv1.Scalar145 * mv2.Scalar245 + mv1.Scalar245 * mv2.Scalar145 - mv1.Scalar136 * mv2.Scalar236 + mv1.Scalar236 * mv2.Scalar136 - mv1.Scalar146 * mv2.Scalar246 + mv1.Scalar246 * mv2.Scalar146 - mv1.Scalar156 * mv2.Scalar256 + mv1.Scalar256 * mv2.Scalar156;
            tempScalar[5] += mv1.Scalar124 * mv2.Scalar234 - mv1.Scalar234 * mv2.Scalar124 + mv1.Scalar125 * mv2.Scalar235 - mv1.Scalar235 * mv2.Scalar125 - mv1.Scalar145 * mv2.Scalar345 + mv1.Scalar345 * mv2.Scalar145 + mv1.Scalar126 * mv2.Scalar236 - mv1.Scalar236 * mv2.Scalar126 - mv1.Scalar146 * mv2.Scalar346 + mv1.Scalar346 * mv2.Scalar146 - mv1.Scalar156 * mv2.Scalar356 + mv1.Scalar356 * mv2.Scalar156;
            tempScalar[6] += -mv1.Scalar124 * mv2.Scalar134 + mv1.Scalar134 * mv2.Scalar124 - mv1.Scalar125 * mv2.Scalar135 + mv1.Scalar135 * mv2.Scalar125 - mv1.Scalar245 * mv2.Scalar345 + mv1.Scalar345 * mv2.Scalar245 - mv1.Scalar126 * mv2.Scalar136 + mv1.Scalar136 * mv2.Scalar126 - mv1.Scalar246 * mv2.Scalar346 + mv1.Scalar346 * mv2.Scalar246 - mv1.Scalar256 * mv2.Scalar356 + mv1.Scalar356 * mv2.Scalar256;
            tempScalar[9] += -mv1.Scalar123 * mv2.Scalar234 + mv1.Scalar234 * mv2.Scalar123 + mv1.Scalar125 * mv2.Scalar245 + mv1.Scalar135 * mv2.Scalar345 - mv1.Scalar245 * mv2.Scalar125 - mv1.Scalar345 * mv2.Scalar135 + mv1.Scalar126 * mv2.Scalar246 + mv1.Scalar136 * mv2.Scalar346 - mv1.Scalar246 * mv2.Scalar126 - mv1.Scalar346 * mv2.Scalar136 - mv1.Scalar156 * mv2.Scalar456 + mv1.Scalar456 * mv2.Scalar156;
            tempScalar[10] += mv1.Scalar123 * mv2.Scalar134 - mv1.Scalar134 * mv2.Scalar123 - mv1.Scalar125 * mv2.Scalar145 + mv1.Scalar235 * mv2.Scalar345 + mv1.Scalar145 * mv2.Scalar125 - mv1.Scalar345 * mv2.Scalar235 - mv1.Scalar126 * mv2.Scalar146 + mv1.Scalar236 * mv2.Scalar346 + mv1.Scalar146 * mv2.Scalar126 - mv1.Scalar346 * mv2.Scalar236 - mv1.Scalar256 * mv2.Scalar456 + mv1.Scalar456 * mv2.Scalar256;
            tempScalar[12] += -mv1.Scalar123 * mv2.Scalar124 + mv1.Scalar124 * mv2.Scalar123 - mv1.Scalar135 * mv2.Scalar145 - mv1.Scalar235 * mv2.Scalar245 + mv1.Scalar145 * mv2.Scalar135 + mv1.Scalar245 * mv2.Scalar235 - mv1.Scalar136 * mv2.Scalar146 - mv1.Scalar236 * mv2.Scalar246 + mv1.Scalar146 * mv2.Scalar136 + mv1.Scalar246 * mv2.Scalar236 - mv1.Scalar356 * mv2.Scalar456 + mv1.Scalar456 * mv2.Scalar356;
            tempScalar[17] += -mv1.Scalar123 * mv2.Scalar235 - mv1.Scalar124 * mv2.Scalar245 - mv1.Scalar134 * mv2.Scalar345 + mv1.Scalar235 * mv2.Scalar123 + mv1.Scalar245 * mv2.Scalar124 + mv1.Scalar345 * mv2.Scalar134 + mv1.Scalar126 * mv2.Scalar256 + mv1.Scalar136 * mv2.Scalar356 + mv1.Scalar146 * mv2.Scalar456 - mv1.Scalar256 * mv2.Scalar126 - mv1.Scalar356 * mv2.Scalar136 - mv1.Scalar456 * mv2.Scalar146;
            tempScalar[18] += mv1.Scalar123 * mv2.Scalar135 + mv1.Scalar124 * mv2.Scalar145 - mv1.Scalar234 * mv2.Scalar345 - mv1.Scalar135 * mv2.Scalar123 - mv1.Scalar145 * mv2.Scalar124 + mv1.Scalar345 * mv2.Scalar234 - mv1.Scalar126 * mv2.Scalar156 + mv1.Scalar236 * mv2.Scalar356 + mv1.Scalar246 * mv2.Scalar456 + mv1.Scalar156 * mv2.Scalar126 - mv1.Scalar356 * mv2.Scalar236 - mv1.Scalar456 * mv2.Scalar246;
            tempScalar[20] += -mv1.Scalar123 * mv2.Scalar125 + mv1.Scalar134 * mv2.Scalar145 + mv1.Scalar234 * mv2.Scalar245 + mv1.Scalar125 * mv2.Scalar123 - mv1.Scalar145 * mv2.Scalar134 - mv1.Scalar245 * mv2.Scalar234 - mv1.Scalar136 * mv2.Scalar156 - mv1.Scalar236 * mv2.Scalar256 + mv1.Scalar346 * mv2.Scalar456 + mv1.Scalar156 * mv2.Scalar136 + mv1.Scalar256 * mv2.Scalar236 - mv1.Scalar456 * mv2.Scalar346;
            tempScalar[24] += -mv1.Scalar124 * mv2.Scalar125 - mv1.Scalar134 * mv2.Scalar135 - mv1.Scalar234 * mv2.Scalar235 + mv1.Scalar125 * mv2.Scalar124 + mv1.Scalar135 * mv2.Scalar134 + mv1.Scalar235 * mv2.Scalar234 - mv1.Scalar146 * mv2.Scalar156 - mv1.Scalar246 * mv2.Scalar256 - mv1.Scalar346 * mv2.Scalar356 + mv1.Scalar156 * mv2.Scalar146 + mv1.Scalar256 * mv2.Scalar246 + mv1.Scalar356 * mv2.Scalar346;
            tempScalar[33] += -mv1.Scalar123 * mv2.Scalar236 - mv1.Scalar124 * mv2.Scalar246 - mv1.Scalar134 * mv2.Scalar346 - mv1.Scalar125 * mv2.Scalar256 - mv1.Scalar135 * mv2.Scalar356 - mv1.Scalar145 * mv2.Scalar456 + mv1.Scalar236 * mv2.Scalar123 + mv1.Scalar246 * mv2.Scalar124 + mv1.Scalar346 * mv2.Scalar134 + mv1.Scalar256 * mv2.Scalar125 + mv1.Scalar356 * mv2.Scalar135 + mv1.Scalar456 * mv2.Scalar145;
            tempScalar[34] += mv1.Scalar123 * mv2.Scalar136 + mv1.Scalar124 * mv2.Scalar146 - mv1.Scalar234 * mv2.Scalar346 + mv1.Scalar125 * mv2.Scalar156 - mv1.Scalar235 * mv2.Scalar356 - mv1.Scalar245 * mv2.Scalar456 - mv1.Scalar136 * mv2.Scalar123 - mv1.Scalar146 * mv2.Scalar124 + mv1.Scalar346 * mv2.Scalar234 - mv1.Scalar156 * mv2.Scalar125 + mv1.Scalar356 * mv2.Scalar235 + mv1.Scalar456 * mv2.Scalar245;
            tempScalar[36] += -mv1.Scalar123 * mv2.Scalar126 + mv1.Scalar134 * mv2.Scalar146 + mv1.Scalar234 * mv2.Scalar246 + mv1.Scalar135 * mv2.Scalar156 + mv1.Scalar235 * mv2.Scalar256 - mv1.Scalar345 * mv2.Scalar456 + mv1.Scalar126 * mv2.Scalar123 - mv1.Scalar146 * mv2.Scalar134 - mv1.Scalar246 * mv2.Scalar234 - mv1.Scalar156 * mv2.Scalar135 - mv1.Scalar256 * mv2.Scalar235 + mv1.Scalar456 * mv2.Scalar345;
            tempScalar[40] += -mv1.Scalar124 * mv2.Scalar126 - mv1.Scalar134 * mv2.Scalar136 - mv1.Scalar234 * mv2.Scalar236 + mv1.Scalar145 * mv2.Scalar156 + mv1.Scalar245 * mv2.Scalar256 + mv1.Scalar345 * mv2.Scalar356 + mv1.Scalar126 * mv2.Scalar124 + mv1.Scalar136 * mv2.Scalar134 + mv1.Scalar236 * mv2.Scalar234 - mv1.Scalar156 * mv2.Scalar145 - mv1.Scalar256 * mv2.Scalar245 - mv1.Scalar356 * mv2.Scalar345;
            tempScalar[48] += -mv1.Scalar125 * mv2.Scalar126 - mv1.Scalar135 * mv2.Scalar136 - mv1.Scalar235 * mv2.Scalar236 - mv1.Scalar145 * mv2.Scalar146 - mv1.Scalar245 * mv2.Scalar246 - mv1.Scalar345 * mv2.Scalar346 + mv1.Scalar126 * mv2.Scalar125 + mv1.Scalar136 * mv2.Scalar135 + mv1.Scalar236 * mv2.Scalar235 + mv1.Scalar146 * mv2.Scalar145 + mv1.Scalar246 * mv2.Scalar245 + mv1.Scalar346 * mv2.Scalar345;
            tempScalar[63] += mv1.Scalar123 * mv2.Scalar456 - mv1.Scalar124 * mv2.Scalar356 + mv1.Scalar134 * mv2.Scalar256 - mv1.Scalar234 * mv2.Scalar156 + mv1.Scalar125 * mv2.Scalar346 - mv1.Scalar135 * mv2.Scalar246 + mv1.Scalar235 * mv2.Scalar146 + mv1.Scalar145 * mv2.Scalar236 - mv1.Scalar245 * mv2.Scalar136 + mv1.Scalar345 * mv2.Scalar126 - mv1.Scalar126 * mv2.Scalar345 + mv1.Scalar136 * mv2.Scalar245 - mv1.Scalar236 * mv2.Scalar145 - mv1.Scalar146 * mv2.Scalar235 + mv1.Scalar246 * mv2.Scalar135 - mv1.Scalar346 * mv2.Scalar125 + mv1.Scalar156 * mv2.Scalar234 - mv1.Scalar256 * mv2.Scalar134 + mv1.Scalar356 * mv2.Scalar124 - mv1.Scalar456 * mv2.Scalar123;
        }
        
        return Ga6Multivector.Create(tempScalar);
    }
    
    public static Ga6Multivector Cp(this Ga6KVector3 mv1, Ga6KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv2.IsZero())
        {
            tempScalar[1] += mv1.Scalar234 * mv2.Scalar1234 + mv1.Scalar235 * mv2.Scalar1235 + mv1.Scalar245 * mv2.Scalar1245 + mv1.Scalar345 * mv2.Scalar1345 + mv1.Scalar236 * mv2.Scalar1236 + mv1.Scalar246 * mv2.Scalar1246 + mv1.Scalar346 * mv2.Scalar1346 + mv1.Scalar256 * mv2.Scalar1256 + mv1.Scalar356 * mv2.Scalar1356 + mv1.Scalar456 * mv2.Scalar1456;
            tempScalar[2] += -mv1.Scalar134 * mv2.Scalar1234 - mv1.Scalar135 * mv2.Scalar1235 - mv1.Scalar145 * mv2.Scalar1245 + mv1.Scalar345 * mv2.Scalar2345 - mv1.Scalar136 * mv2.Scalar1236 - mv1.Scalar146 * mv2.Scalar1246 + mv1.Scalar346 * mv2.Scalar2346 - mv1.Scalar156 * mv2.Scalar1256 + mv1.Scalar356 * mv2.Scalar2356 + mv1.Scalar456 * mv2.Scalar2456;
            tempScalar[4] += mv1.Scalar124 * mv2.Scalar1234 + mv1.Scalar125 * mv2.Scalar1235 - mv1.Scalar145 * mv2.Scalar1345 - mv1.Scalar245 * mv2.Scalar2345 + mv1.Scalar126 * mv2.Scalar1236 - mv1.Scalar146 * mv2.Scalar1346 - mv1.Scalar246 * mv2.Scalar2346 - mv1.Scalar156 * mv2.Scalar1356 - mv1.Scalar256 * mv2.Scalar2356 + mv1.Scalar456 * mv2.Scalar3456;
            tempScalar[8] += -mv1.Scalar123 * mv2.Scalar1234 + mv1.Scalar125 * mv2.Scalar1245 + mv1.Scalar135 * mv2.Scalar1345 + mv1.Scalar235 * mv2.Scalar2345 + mv1.Scalar126 * mv2.Scalar1246 + mv1.Scalar136 * mv2.Scalar1346 + mv1.Scalar236 * mv2.Scalar2346 - mv1.Scalar156 * mv2.Scalar1456 - mv1.Scalar256 * mv2.Scalar2456 - mv1.Scalar356 * mv2.Scalar3456;
            tempScalar[16] += -mv1.Scalar123 * mv2.Scalar1235 - mv1.Scalar124 * mv2.Scalar1245 - mv1.Scalar134 * mv2.Scalar1345 - mv1.Scalar234 * mv2.Scalar2345 + mv1.Scalar126 * mv2.Scalar1256 + mv1.Scalar136 * mv2.Scalar1356 + mv1.Scalar236 * mv2.Scalar2356 + mv1.Scalar146 * mv2.Scalar1456 + mv1.Scalar246 * mv2.Scalar2456 + mv1.Scalar346 * mv2.Scalar3456;
            tempScalar[31] += -mv1.Scalar126 * mv2.Scalar3456 + mv1.Scalar136 * mv2.Scalar2456 - mv1.Scalar236 * mv2.Scalar1456 - mv1.Scalar146 * mv2.Scalar2356 + mv1.Scalar246 * mv2.Scalar1356 - mv1.Scalar346 * mv2.Scalar1256 + mv1.Scalar156 * mv2.Scalar2346 - mv1.Scalar256 * mv2.Scalar1346 + mv1.Scalar356 * mv2.Scalar1246 - mv1.Scalar456 * mv2.Scalar1236;
            tempScalar[32] += -mv1.Scalar123 * mv2.Scalar1236 - mv1.Scalar124 * mv2.Scalar1246 - mv1.Scalar134 * mv2.Scalar1346 - mv1.Scalar234 * mv2.Scalar2346 - mv1.Scalar125 * mv2.Scalar1256 - mv1.Scalar135 * mv2.Scalar1356 - mv1.Scalar235 * mv2.Scalar2356 - mv1.Scalar145 * mv2.Scalar1456 - mv1.Scalar245 * mv2.Scalar2456 - mv1.Scalar345 * mv2.Scalar3456;
            tempScalar[47] += mv1.Scalar125 * mv2.Scalar3456 - mv1.Scalar135 * mv2.Scalar2456 + mv1.Scalar235 * mv2.Scalar1456 + mv1.Scalar145 * mv2.Scalar2356 - mv1.Scalar245 * mv2.Scalar1356 + mv1.Scalar345 * mv2.Scalar1256 - mv1.Scalar156 * mv2.Scalar2345 + mv1.Scalar256 * mv2.Scalar1345 - mv1.Scalar356 * mv2.Scalar1245 + mv1.Scalar456 * mv2.Scalar1235;
            tempScalar[55] += -mv1.Scalar124 * mv2.Scalar3456 + mv1.Scalar134 * mv2.Scalar2456 - mv1.Scalar234 * mv2.Scalar1456 - mv1.Scalar145 * mv2.Scalar2346 + mv1.Scalar245 * mv2.Scalar1346 - mv1.Scalar345 * mv2.Scalar1246 + mv1.Scalar146 * mv2.Scalar2345 - mv1.Scalar246 * mv2.Scalar1345 + mv1.Scalar346 * mv2.Scalar1245 - mv1.Scalar456 * mv2.Scalar1234;
            tempScalar[59] += mv1.Scalar123 * mv2.Scalar3456 - mv1.Scalar134 * mv2.Scalar2356 + mv1.Scalar234 * mv2.Scalar1356 + mv1.Scalar135 * mv2.Scalar2346 - mv1.Scalar235 * mv2.Scalar1346 + mv1.Scalar345 * mv2.Scalar1236 - mv1.Scalar136 * mv2.Scalar2345 + mv1.Scalar236 * mv2.Scalar1345 - mv1.Scalar346 * mv2.Scalar1235 + mv1.Scalar356 * mv2.Scalar1234;
            tempScalar[61] += -mv1.Scalar123 * mv2.Scalar2456 + mv1.Scalar124 * mv2.Scalar2356 - mv1.Scalar234 * mv2.Scalar1256 - mv1.Scalar125 * mv2.Scalar2346 + mv1.Scalar235 * mv2.Scalar1246 - mv1.Scalar245 * mv2.Scalar1236 + mv1.Scalar126 * mv2.Scalar2345 - mv1.Scalar236 * mv2.Scalar1245 + mv1.Scalar246 * mv2.Scalar1235 - mv1.Scalar256 * mv2.Scalar1234;
            tempScalar[62] += mv1.Scalar123 * mv2.Scalar1456 - mv1.Scalar124 * mv2.Scalar1356 + mv1.Scalar134 * mv2.Scalar1256 + mv1.Scalar125 * mv2.Scalar1346 - mv1.Scalar135 * mv2.Scalar1246 + mv1.Scalar145 * mv2.Scalar1236 - mv1.Scalar126 * mv2.Scalar1345 + mv1.Scalar136 * mv2.Scalar1245 - mv1.Scalar146 * mv2.Scalar1235 + mv1.Scalar156 * mv2.Scalar1234;
        }
        
        return Ga6Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector4 Cp(this Ga6KVector3 mv1, Ga6KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector4.Zero;
        
        return new Ga6KVector4
        {
            Scalar1234 = -mv1.Scalar156 * mv2.Scalar23456 + mv1.Scalar256 * mv2.Scalar13456 - mv1.Scalar356 * mv2.Scalar12456 + mv1.Scalar456 * mv2.Scalar12356,
            Scalar1235 = mv1.Scalar146 * mv2.Scalar23456 - mv1.Scalar246 * mv2.Scalar13456 + mv1.Scalar346 * mv2.Scalar12456 - mv1.Scalar456 * mv2.Scalar12346,
            Scalar1245 = -mv1.Scalar136 * mv2.Scalar23456 + mv1.Scalar236 * mv2.Scalar13456 - mv1.Scalar346 * mv2.Scalar12356 + mv1.Scalar356 * mv2.Scalar12346,
            Scalar1345 = mv1.Scalar126 * mv2.Scalar23456 - mv1.Scalar236 * mv2.Scalar12456 + mv1.Scalar246 * mv2.Scalar12356 - mv1.Scalar256 * mv2.Scalar12346,
            Scalar2345 = -mv1.Scalar126 * mv2.Scalar13456 + mv1.Scalar136 * mv2.Scalar12456 - mv1.Scalar146 * mv2.Scalar12356 + mv1.Scalar156 * mv2.Scalar12346,
            Scalar1236 = -mv1.Scalar145 * mv2.Scalar23456 + mv1.Scalar245 * mv2.Scalar13456 - mv1.Scalar345 * mv2.Scalar12456 + mv1.Scalar456 * mv2.Scalar12345,
            Scalar1246 = mv1.Scalar135 * mv2.Scalar23456 - mv1.Scalar235 * mv2.Scalar13456 + mv1.Scalar345 * mv2.Scalar12356 - mv1.Scalar356 * mv2.Scalar12345,
            Scalar1346 = -mv1.Scalar125 * mv2.Scalar23456 + mv1.Scalar235 * mv2.Scalar12456 - mv1.Scalar245 * mv2.Scalar12356 + mv1.Scalar256 * mv2.Scalar12345,
            Scalar2346 = mv1.Scalar125 * mv2.Scalar13456 - mv1.Scalar135 * mv2.Scalar12456 + mv1.Scalar145 * mv2.Scalar12356 - mv1.Scalar156 * mv2.Scalar12345,
            Scalar1256 = -mv1.Scalar134 * mv2.Scalar23456 + mv1.Scalar234 * mv2.Scalar13456 - mv1.Scalar345 * mv2.Scalar12346 + mv1.Scalar346 * mv2.Scalar12345,
            Scalar1356 = mv1.Scalar124 * mv2.Scalar23456 - mv1.Scalar234 * mv2.Scalar12456 + mv1.Scalar245 * mv2.Scalar12346 - mv1.Scalar246 * mv2.Scalar12345,
            Scalar2356 = -mv1.Scalar124 * mv2.Scalar13456 + mv1.Scalar134 * mv2.Scalar12456 - mv1.Scalar145 * mv2.Scalar12346 + mv1.Scalar146 * mv2.Scalar12345,
            Scalar1456 = -mv1.Scalar123 * mv2.Scalar23456 + mv1.Scalar234 * mv2.Scalar12356 - mv1.Scalar235 * mv2.Scalar12346 + mv1.Scalar236 * mv2.Scalar12345,
            Scalar2456 = mv1.Scalar123 * mv2.Scalar13456 - mv1.Scalar134 * mv2.Scalar12356 + mv1.Scalar135 * mv2.Scalar12346 - mv1.Scalar136 * mv2.Scalar12345,
            Scalar3456 = -mv1.Scalar123 * mv2.Scalar12456 + mv1.Scalar124 * mv2.Scalar12356 - mv1.Scalar125 * mv2.Scalar12346 + mv1.Scalar126 * mv2.Scalar12345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector3 Cp(this Ga6KVector3 mv1, Ga6KVector6 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector3.Zero;
        
        return new Ga6KVector3
        {
            Scalar123 = mv1.Scalar456 * mv2.Scalar123456,
            Scalar124 = -mv1.Scalar356 * mv2.Scalar123456,
            Scalar134 = mv1.Scalar256 * mv2.Scalar123456,
            Scalar234 = -mv1.Scalar156 * mv2.Scalar123456,
            Scalar125 = mv1.Scalar346 * mv2.Scalar123456,
            Scalar135 = -mv1.Scalar246 * mv2.Scalar123456,
            Scalar235 = mv1.Scalar146 * mv2.Scalar123456,
            Scalar145 = mv1.Scalar236 * mv2.Scalar123456,
            Scalar245 = -mv1.Scalar136 * mv2.Scalar123456,
            Scalar345 = mv1.Scalar126 * mv2.Scalar123456,
            Scalar126 = -mv1.Scalar345 * mv2.Scalar123456,
            Scalar136 = mv1.Scalar245 * mv2.Scalar123456,
            Scalar236 = -mv1.Scalar145 * mv2.Scalar123456,
            Scalar146 = -mv1.Scalar235 * mv2.Scalar123456,
            Scalar246 = mv1.Scalar135 * mv2.Scalar123456,
            Scalar346 = -mv1.Scalar125 * mv2.Scalar123456,
            Scalar156 = mv1.Scalar234 * mv2.Scalar123456,
            Scalar256 = -mv1.Scalar134 * mv2.Scalar123456,
            Scalar356 = mv1.Scalar124 * mv2.Scalar123456,
            Scalar456 = -mv1.Scalar123 * mv2.Scalar123456
        };
    }
    
    public static Ga6Multivector Cp(this Ga6KVector3 mv1, Ga6Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[15] += mv1.Scalar123 * mv2.KVector1.Scalar4 - mv1.Scalar124 * mv2.KVector1.Scalar3 + mv1.Scalar134 * mv2.KVector1.Scalar2 - mv1.Scalar234 * mv2.KVector1.Scalar1;
            tempScalar[23] += mv1.Scalar123 * mv2.KVector1.Scalar5 - mv1.Scalar125 * mv2.KVector1.Scalar3 + mv1.Scalar135 * mv2.KVector1.Scalar2 - mv1.Scalar235 * mv2.KVector1.Scalar1;
            tempScalar[27] += mv1.Scalar124 * mv2.KVector1.Scalar5 - mv1.Scalar125 * mv2.KVector1.Scalar4 + mv1.Scalar145 * mv2.KVector1.Scalar2 - mv1.Scalar245 * mv2.KVector1.Scalar1;
            tempScalar[29] += mv1.Scalar134 * mv2.KVector1.Scalar5 - mv1.Scalar135 * mv2.KVector1.Scalar4 + mv1.Scalar145 * mv2.KVector1.Scalar3 - mv1.Scalar345 * mv2.KVector1.Scalar1;
            tempScalar[30] += mv1.Scalar234 * mv2.KVector1.Scalar5 - mv1.Scalar235 * mv2.KVector1.Scalar4 + mv1.Scalar245 * mv2.KVector1.Scalar3 - mv1.Scalar345 * mv2.KVector1.Scalar2;
            tempScalar[39] += mv1.Scalar123 * mv2.KVector1.Scalar6 - mv1.Scalar126 * mv2.KVector1.Scalar3 + mv1.Scalar136 * mv2.KVector1.Scalar2 - mv1.Scalar236 * mv2.KVector1.Scalar1;
            tempScalar[43] += mv1.Scalar124 * mv2.KVector1.Scalar6 - mv1.Scalar126 * mv2.KVector1.Scalar4 + mv1.Scalar146 * mv2.KVector1.Scalar2 - mv1.Scalar246 * mv2.KVector1.Scalar1;
            tempScalar[45] += mv1.Scalar134 * mv2.KVector1.Scalar6 - mv1.Scalar136 * mv2.KVector1.Scalar4 + mv1.Scalar146 * mv2.KVector1.Scalar3 - mv1.Scalar346 * mv2.KVector1.Scalar1;
            tempScalar[46] += mv1.Scalar234 * mv2.KVector1.Scalar6 - mv1.Scalar236 * mv2.KVector1.Scalar4 + mv1.Scalar246 * mv2.KVector1.Scalar3 - mv1.Scalar346 * mv2.KVector1.Scalar2;
            tempScalar[51] += mv1.Scalar125 * mv2.KVector1.Scalar6 - mv1.Scalar126 * mv2.KVector1.Scalar5 + mv1.Scalar156 * mv2.KVector1.Scalar2 - mv1.Scalar256 * mv2.KVector1.Scalar1;
            tempScalar[53] += mv1.Scalar135 * mv2.KVector1.Scalar6 - mv1.Scalar136 * mv2.KVector1.Scalar5 + mv1.Scalar156 * mv2.KVector1.Scalar3 - mv1.Scalar356 * mv2.KVector1.Scalar1;
            tempScalar[54] += mv1.Scalar235 * mv2.KVector1.Scalar6 - mv1.Scalar236 * mv2.KVector1.Scalar5 + mv1.Scalar256 * mv2.KVector1.Scalar3 - mv1.Scalar356 * mv2.KVector1.Scalar2;
            tempScalar[57] += mv1.Scalar145 * mv2.KVector1.Scalar6 - mv1.Scalar146 * mv2.KVector1.Scalar5 + mv1.Scalar156 * mv2.KVector1.Scalar4 - mv1.Scalar456 * mv2.KVector1.Scalar1;
            tempScalar[58] += mv1.Scalar245 * mv2.KVector1.Scalar6 - mv1.Scalar246 * mv2.KVector1.Scalar5 + mv1.Scalar256 * mv2.KVector1.Scalar4 - mv1.Scalar456 * mv2.KVector1.Scalar2;
            tempScalar[60] += mv1.Scalar345 * mv2.KVector1.Scalar6 - mv1.Scalar346 * mv2.KVector1.Scalar5 + mv1.Scalar356 * mv2.KVector1.Scalar4 - mv1.Scalar456 * mv2.KVector1.Scalar3;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[7] += -mv1.Scalar124 * mv2.KVector2.Scalar34 + mv1.Scalar134 * mv2.KVector2.Scalar24 - mv1.Scalar234 * mv2.KVector2.Scalar14 - mv1.Scalar125 * mv2.KVector2.Scalar35 + mv1.Scalar135 * mv2.KVector2.Scalar25 - mv1.Scalar235 * mv2.KVector2.Scalar15 - mv1.Scalar126 * mv2.KVector2.Scalar36 + mv1.Scalar136 * mv2.KVector2.Scalar26 - mv1.Scalar236 * mv2.KVector2.Scalar16;
            tempScalar[11] += mv1.Scalar123 * mv2.KVector2.Scalar34 - mv1.Scalar134 * mv2.KVector2.Scalar23 + mv1.Scalar234 * mv2.KVector2.Scalar13 - mv1.Scalar125 * mv2.KVector2.Scalar45 + mv1.Scalar145 * mv2.KVector2.Scalar25 - mv1.Scalar245 * mv2.KVector2.Scalar15 - mv1.Scalar126 * mv2.KVector2.Scalar46 + mv1.Scalar146 * mv2.KVector2.Scalar26 - mv1.Scalar246 * mv2.KVector2.Scalar16;
            tempScalar[13] += -mv1.Scalar123 * mv2.KVector2.Scalar24 + mv1.Scalar124 * mv2.KVector2.Scalar23 - mv1.Scalar234 * mv2.KVector2.Scalar12 - mv1.Scalar135 * mv2.KVector2.Scalar45 + mv1.Scalar145 * mv2.KVector2.Scalar35 - mv1.Scalar345 * mv2.KVector2.Scalar15 - mv1.Scalar136 * mv2.KVector2.Scalar46 + mv1.Scalar146 * mv2.KVector2.Scalar36 - mv1.Scalar346 * mv2.KVector2.Scalar16;
            tempScalar[14] += mv1.Scalar123 * mv2.KVector2.Scalar14 - mv1.Scalar124 * mv2.KVector2.Scalar13 + mv1.Scalar134 * mv2.KVector2.Scalar12 - mv1.Scalar235 * mv2.KVector2.Scalar45 + mv1.Scalar245 * mv2.KVector2.Scalar35 - mv1.Scalar345 * mv2.KVector2.Scalar25 - mv1.Scalar236 * mv2.KVector2.Scalar46 + mv1.Scalar246 * mv2.KVector2.Scalar36 - mv1.Scalar346 * mv2.KVector2.Scalar26;
            tempScalar[19] += mv1.Scalar123 * mv2.KVector2.Scalar35 + mv1.Scalar124 * mv2.KVector2.Scalar45 - mv1.Scalar135 * mv2.KVector2.Scalar23 + mv1.Scalar235 * mv2.KVector2.Scalar13 - mv1.Scalar145 * mv2.KVector2.Scalar24 + mv1.Scalar245 * mv2.KVector2.Scalar14 - mv1.Scalar126 * mv2.KVector2.Scalar56 + mv1.Scalar156 * mv2.KVector2.Scalar26 - mv1.Scalar256 * mv2.KVector2.Scalar16;
            tempScalar[21] += -mv1.Scalar123 * mv2.KVector2.Scalar25 + mv1.Scalar134 * mv2.KVector2.Scalar45 + mv1.Scalar125 * mv2.KVector2.Scalar23 - mv1.Scalar235 * mv2.KVector2.Scalar12 - mv1.Scalar145 * mv2.KVector2.Scalar34 + mv1.Scalar345 * mv2.KVector2.Scalar14 - mv1.Scalar136 * mv2.KVector2.Scalar56 + mv1.Scalar156 * mv2.KVector2.Scalar36 - mv1.Scalar356 * mv2.KVector2.Scalar16;
            tempScalar[22] += mv1.Scalar123 * mv2.KVector2.Scalar15 + mv1.Scalar234 * mv2.KVector2.Scalar45 - mv1.Scalar125 * mv2.KVector2.Scalar13 + mv1.Scalar135 * mv2.KVector2.Scalar12 - mv1.Scalar245 * mv2.KVector2.Scalar34 + mv1.Scalar345 * mv2.KVector2.Scalar24 - mv1.Scalar236 * mv2.KVector2.Scalar56 + mv1.Scalar256 * mv2.KVector2.Scalar36 - mv1.Scalar356 * mv2.KVector2.Scalar26;
            tempScalar[25] += -mv1.Scalar124 * mv2.KVector2.Scalar25 - mv1.Scalar134 * mv2.KVector2.Scalar35 + mv1.Scalar125 * mv2.KVector2.Scalar24 + mv1.Scalar135 * mv2.KVector2.Scalar34 - mv1.Scalar245 * mv2.KVector2.Scalar12 - mv1.Scalar345 * mv2.KVector2.Scalar13 - mv1.Scalar146 * mv2.KVector2.Scalar56 + mv1.Scalar156 * mv2.KVector2.Scalar46 - mv1.Scalar456 * mv2.KVector2.Scalar16;
            tempScalar[26] += mv1.Scalar124 * mv2.KVector2.Scalar15 - mv1.Scalar234 * mv2.KVector2.Scalar35 - mv1.Scalar125 * mv2.KVector2.Scalar14 + mv1.Scalar235 * mv2.KVector2.Scalar34 + mv1.Scalar145 * mv2.KVector2.Scalar12 - mv1.Scalar345 * mv2.KVector2.Scalar23 - mv1.Scalar246 * mv2.KVector2.Scalar56 + mv1.Scalar256 * mv2.KVector2.Scalar46 - mv1.Scalar456 * mv2.KVector2.Scalar26;
            tempScalar[28] += mv1.Scalar134 * mv2.KVector2.Scalar15 + mv1.Scalar234 * mv2.KVector2.Scalar25 - mv1.Scalar135 * mv2.KVector2.Scalar14 - mv1.Scalar235 * mv2.KVector2.Scalar24 + mv1.Scalar145 * mv2.KVector2.Scalar13 + mv1.Scalar245 * mv2.KVector2.Scalar23 - mv1.Scalar346 * mv2.KVector2.Scalar56 + mv1.Scalar356 * mv2.KVector2.Scalar46 - mv1.Scalar456 * mv2.KVector2.Scalar36;
            tempScalar[35] += mv1.Scalar123 * mv2.KVector2.Scalar36 + mv1.Scalar124 * mv2.KVector2.Scalar46 + mv1.Scalar125 * mv2.KVector2.Scalar56 - mv1.Scalar136 * mv2.KVector2.Scalar23 + mv1.Scalar236 * mv2.KVector2.Scalar13 - mv1.Scalar146 * mv2.KVector2.Scalar24 + mv1.Scalar246 * mv2.KVector2.Scalar14 - mv1.Scalar156 * mv2.KVector2.Scalar25 + mv1.Scalar256 * mv2.KVector2.Scalar15;
            tempScalar[37] += -mv1.Scalar123 * mv2.KVector2.Scalar26 + mv1.Scalar134 * mv2.KVector2.Scalar46 + mv1.Scalar135 * mv2.KVector2.Scalar56 + mv1.Scalar126 * mv2.KVector2.Scalar23 - mv1.Scalar236 * mv2.KVector2.Scalar12 - mv1.Scalar146 * mv2.KVector2.Scalar34 + mv1.Scalar346 * mv2.KVector2.Scalar14 - mv1.Scalar156 * mv2.KVector2.Scalar35 + mv1.Scalar356 * mv2.KVector2.Scalar15;
            tempScalar[38] += mv1.Scalar123 * mv2.KVector2.Scalar16 + mv1.Scalar234 * mv2.KVector2.Scalar46 + mv1.Scalar235 * mv2.KVector2.Scalar56 - mv1.Scalar126 * mv2.KVector2.Scalar13 + mv1.Scalar136 * mv2.KVector2.Scalar12 - mv1.Scalar246 * mv2.KVector2.Scalar34 + mv1.Scalar346 * mv2.KVector2.Scalar24 - mv1.Scalar256 * mv2.KVector2.Scalar35 + mv1.Scalar356 * mv2.KVector2.Scalar25;
            tempScalar[41] += -mv1.Scalar124 * mv2.KVector2.Scalar26 - mv1.Scalar134 * mv2.KVector2.Scalar36 + mv1.Scalar145 * mv2.KVector2.Scalar56 + mv1.Scalar126 * mv2.KVector2.Scalar24 + mv1.Scalar136 * mv2.KVector2.Scalar34 - mv1.Scalar246 * mv2.KVector2.Scalar12 - mv1.Scalar346 * mv2.KVector2.Scalar13 - mv1.Scalar156 * mv2.KVector2.Scalar45 + mv1.Scalar456 * mv2.KVector2.Scalar15;
            tempScalar[42] += mv1.Scalar124 * mv2.KVector2.Scalar16 - mv1.Scalar234 * mv2.KVector2.Scalar36 + mv1.Scalar245 * mv2.KVector2.Scalar56 - mv1.Scalar126 * mv2.KVector2.Scalar14 + mv1.Scalar236 * mv2.KVector2.Scalar34 + mv1.Scalar146 * mv2.KVector2.Scalar12 - mv1.Scalar346 * mv2.KVector2.Scalar23 - mv1.Scalar256 * mv2.KVector2.Scalar45 + mv1.Scalar456 * mv2.KVector2.Scalar25;
            tempScalar[44] += mv1.Scalar134 * mv2.KVector2.Scalar16 + mv1.Scalar234 * mv2.KVector2.Scalar26 + mv1.Scalar345 * mv2.KVector2.Scalar56 - mv1.Scalar136 * mv2.KVector2.Scalar14 - mv1.Scalar236 * mv2.KVector2.Scalar24 + mv1.Scalar146 * mv2.KVector2.Scalar13 + mv1.Scalar246 * mv2.KVector2.Scalar23 - mv1.Scalar356 * mv2.KVector2.Scalar45 + mv1.Scalar456 * mv2.KVector2.Scalar35;
            tempScalar[49] += -mv1.Scalar125 * mv2.KVector2.Scalar26 - mv1.Scalar135 * mv2.KVector2.Scalar36 - mv1.Scalar145 * mv2.KVector2.Scalar46 + mv1.Scalar126 * mv2.KVector2.Scalar25 + mv1.Scalar136 * mv2.KVector2.Scalar35 + mv1.Scalar146 * mv2.KVector2.Scalar45 - mv1.Scalar256 * mv2.KVector2.Scalar12 - mv1.Scalar356 * mv2.KVector2.Scalar13 - mv1.Scalar456 * mv2.KVector2.Scalar14;
            tempScalar[50] += mv1.Scalar125 * mv2.KVector2.Scalar16 - mv1.Scalar235 * mv2.KVector2.Scalar36 - mv1.Scalar245 * mv2.KVector2.Scalar46 - mv1.Scalar126 * mv2.KVector2.Scalar15 + mv1.Scalar236 * mv2.KVector2.Scalar35 + mv1.Scalar246 * mv2.KVector2.Scalar45 + mv1.Scalar156 * mv2.KVector2.Scalar12 - mv1.Scalar356 * mv2.KVector2.Scalar23 - mv1.Scalar456 * mv2.KVector2.Scalar24;
            tempScalar[52] += mv1.Scalar135 * mv2.KVector2.Scalar16 + mv1.Scalar235 * mv2.KVector2.Scalar26 - mv1.Scalar345 * mv2.KVector2.Scalar46 - mv1.Scalar136 * mv2.KVector2.Scalar15 - mv1.Scalar236 * mv2.KVector2.Scalar25 + mv1.Scalar346 * mv2.KVector2.Scalar45 + mv1.Scalar156 * mv2.KVector2.Scalar13 + mv1.Scalar256 * mv2.KVector2.Scalar23 - mv1.Scalar456 * mv2.KVector2.Scalar34;
            tempScalar[56] += mv1.Scalar145 * mv2.KVector2.Scalar16 + mv1.Scalar245 * mv2.KVector2.Scalar26 + mv1.Scalar345 * mv2.KVector2.Scalar36 - mv1.Scalar146 * mv2.KVector2.Scalar15 - mv1.Scalar246 * mv2.KVector2.Scalar25 - mv1.Scalar346 * mv2.KVector2.Scalar35 + mv1.Scalar156 * mv2.KVector2.Scalar14 + mv1.Scalar256 * mv2.KVector2.Scalar24 + mv1.Scalar356 * mv2.KVector2.Scalar34;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[3] += -mv1.Scalar134 * mv2.KVector3.Scalar234 + mv1.Scalar234 * mv2.KVector3.Scalar134 - mv1.Scalar135 * mv2.KVector3.Scalar235 + mv1.Scalar235 * mv2.KVector3.Scalar135 - mv1.Scalar145 * mv2.KVector3.Scalar245 + mv1.Scalar245 * mv2.KVector3.Scalar145 - mv1.Scalar136 * mv2.KVector3.Scalar236 + mv1.Scalar236 * mv2.KVector3.Scalar136 - mv1.Scalar146 * mv2.KVector3.Scalar246 + mv1.Scalar246 * mv2.KVector3.Scalar146 - mv1.Scalar156 * mv2.KVector3.Scalar256 + mv1.Scalar256 * mv2.KVector3.Scalar156;
            tempScalar[5] += mv1.Scalar124 * mv2.KVector3.Scalar234 - mv1.Scalar234 * mv2.KVector3.Scalar124 + mv1.Scalar125 * mv2.KVector3.Scalar235 - mv1.Scalar235 * mv2.KVector3.Scalar125 - mv1.Scalar145 * mv2.KVector3.Scalar345 + mv1.Scalar345 * mv2.KVector3.Scalar145 + mv1.Scalar126 * mv2.KVector3.Scalar236 - mv1.Scalar236 * mv2.KVector3.Scalar126 - mv1.Scalar146 * mv2.KVector3.Scalar346 + mv1.Scalar346 * mv2.KVector3.Scalar146 - mv1.Scalar156 * mv2.KVector3.Scalar356 + mv1.Scalar356 * mv2.KVector3.Scalar156;
            tempScalar[6] += -mv1.Scalar124 * mv2.KVector3.Scalar134 + mv1.Scalar134 * mv2.KVector3.Scalar124 - mv1.Scalar125 * mv2.KVector3.Scalar135 + mv1.Scalar135 * mv2.KVector3.Scalar125 - mv1.Scalar245 * mv2.KVector3.Scalar345 + mv1.Scalar345 * mv2.KVector3.Scalar245 - mv1.Scalar126 * mv2.KVector3.Scalar136 + mv1.Scalar136 * mv2.KVector3.Scalar126 - mv1.Scalar246 * mv2.KVector3.Scalar346 + mv1.Scalar346 * mv2.KVector3.Scalar246 - mv1.Scalar256 * mv2.KVector3.Scalar356 + mv1.Scalar356 * mv2.KVector3.Scalar256;
            tempScalar[9] += -mv1.Scalar123 * mv2.KVector3.Scalar234 + mv1.Scalar234 * mv2.KVector3.Scalar123 + mv1.Scalar125 * mv2.KVector3.Scalar245 + mv1.Scalar135 * mv2.KVector3.Scalar345 - mv1.Scalar245 * mv2.KVector3.Scalar125 - mv1.Scalar345 * mv2.KVector3.Scalar135 + mv1.Scalar126 * mv2.KVector3.Scalar246 + mv1.Scalar136 * mv2.KVector3.Scalar346 - mv1.Scalar246 * mv2.KVector3.Scalar126 - mv1.Scalar346 * mv2.KVector3.Scalar136 - mv1.Scalar156 * mv2.KVector3.Scalar456 + mv1.Scalar456 * mv2.KVector3.Scalar156;
            tempScalar[10] += mv1.Scalar123 * mv2.KVector3.Scalar134 - mv1.Scalar134 * mv2.KVector3.Scalar123 - mv1.Scalar125 * mv2.KVector3.Scalar145 + mv1.Scalar235 * mv2.KVector3.Scalar345 + mv1.Scalar145 * mv2.KVector3.Scalar125 - mv1.Scalar345 * mv2.KVector3.Scalar235 - mv1.Scalar126 * mv2.KVector3.Scalar146 + mv1.Scalar236 * mv2.KVector3.Scalar346 + mv1.Scalar146 * mv2.KVector3.Scalar126 - mv1.Scalar346 * mv2.KVector3.Scalar236 - mv1.Scalar256 * mv2.KVector3.Scalar456 + mv1.Scalar456 * mv2.KVector3.Scalar256;
            tempScalar[12] += -mv1.Scalar123 * mv2.KVector3.Scalar124 + mv1.Scalar124 * mv2.KVector3.Scalar123 - mv1.Scalar135 * mv2.KVector3.Scalar145 - mv1.Scalar235 * mv2.KVector3.Scalar245 + mv1.Scalar145 * mv2.KVector3.Scalar135 + mv1.Scalar245 * mv2.KVector3.Scalar235 - mv1.Scalar136 * mv2.KVector3.Scalar146 - mv1.Scalar236 * mv2.KVector3.Scalar246 + mv1.Scalar146 * mv2.KVector3.Scalar136 + mv1.Scalar246 * mv2.KVector3.Scalar236 - mv1.Scalar356 * mv2.KVector3.Scalar456 + mv1.Scalar456 * mv2.KVector3.Scalar356;
            tempScalar[17] += -mv1.Scalar123 * mv2.KVector3.Scalar235 - mv1.Scalar124 * mv2.KVector3.Scalar245 - mv1.Scalar134 * mv2.KVector3.Scalar345 + mv1.Scalar235 * mv2.KVector3.Scalar123 + mv1.Scalar245 * mv2.KVector3.Scalar124 + mv1.Scalar345 * mv2.KVector3.Scalar134 + mv1.Scalar126 * mv2.KVector3.Scalar256 + mv1.Scalar136 * mv2.KVector3.Scalar356 + mv1.Scalar146 * mv2.KVector3.Scalar456 - mv1.Scalar256 * mv2.KVector3.Scalar126 - mv1.Scalar356 * mv2.KVector3.Scalar136 - mv1.Scalar456 * mv2.KVector3.Scalar146;
            tempScalar[18] += mv1.Scalar123 * mv2.KVector3.Scalar135 + mv1.Scalar124 * mv2.KVector3.Scalar145 - mv1.Scalar234 * mv2.KVector3.Scalar345 - mv1.Scalar135 * mv2.KVector3.Scalar123 - mv1.Scalar145 * mv2.KVector3.Scalar124 + mv1.Scalar345 * mv2.KVector3.Scalar234 - mv1.Scalar126 * mv2.KVector3.Scalar156 + mv1.Scalar236 * mv2.KVector3.Scalar356 + mv1.Scalar246 * mv2.KVector3.Scalar456 + mv1.Scalar156 * mv2.KVector3.Scalar126 - mv1.Scalar356 * mv2.KVector3.Scalar236 - mv1.Scalar456 * mv2.KVector3.Scalar246;
            tempScalar[20] += -mv1.Scalar123 * mv2.KVector3.Scalar125 + mv1.Scalar134 * mv2.KVector3.Scalar145 + mv1.Scalar234 * mv2.KVector3.Scalar245 + mv1.Scalar125 * mv2.KVector3.Scalar123 - mv1.Scalar145 * mv2.KVector3.Scalar134 - mv1.Scalar245 * mv2.KVector3.Scalar234 - mv1.Scalar136 * mv2.KVector3.Scalar156 - mv1.Scalar236 * mv2.KVector3.Scalar256 + mv1.Scalar346 * mv2.KVector3.Scalar456 + mv1.Scalar156 * mv2.KVector3.Scalar136 + mv1.Scalar256 * mv2.KVector3.Scalar236 - mv1.Scalar456 * mv2.KVector3.Scalar346;
            tempScalar[24] += -mv1.Scalar124 * mv2.KVector3.Scalar125 - mv1.Scalar134 * mv2.KVector3.Scalar135 - mv1.Scalar234 * mv2.KVector3.Scalar235 + mv1.Scalar125 * mv2.KVector3.Scalar124 + mv1.Scalar135 * mv2.KVector3.Scalar134 + mv1.Scalar235 * mv2.KVector3.Scalar234 - mv1.Scalar146 * mv2.KVector3.Scalar156 - mv1.Scalar246 * mv2.KVector3.Scalar256 - mv1.Scalar346 * mv2.KVector3.Scalar356 + mv1.Scalar156 * mv2.KVector3.Scalar146 + mv1.Scalar256 * mv2.KVector3.Scalar246 + mv1.Scalar356 * mv2.KVector3.Scalar346;
            tempScalar[33] += -mv1.Scalar123 * mv2.KVector3.Scalar236 - mv1.Scalar124 * mv2.KVector3.Scalar246 - mv1.Scalar134 * mv2.KVector3.Scalar346 - mv1.Scalar125 * mv2.KVector3.Scalar256 - mv1.Scalar135 * mv2.KVector3.Scalar356 - mv1.Scalar145 * mv2.KVector3.Scalar456 + mv1.Scalar236 * mv2.KVector3.Scalar123 + mv1.Scalar246 * mv2.KVector3.Scalar124 + mv1.Scalar346 * mv2.KVector3.Scalar134 + mv1.Scalar256 * mv2.KVector3.Scalar125 + mv1.Scalar356 * mv2.KVector3.Scalar135 + mv1.Scalar456 * mv2.KVector3.Scalar145;
            tempScalar[34] += mv1.Scalar123 * mv2.KVector3.Scalar136 + mv1.Scalar124 * mv2.KVector3.Scalar146 - mv1.Scalar234 * mv2.KVector3.Scalar346 + mv1.Scalar125 * mv2.KVector3.Scalar156 - mv1.Scalar235 * mv2.KVector3.Scalar356 - mv1.Scalar245 * mv2.KVector3.Scalar456 - mv1.Scalar136 * mv2.KVector3.Scalar123 - mv1.Scalar146 * mv2.KVector3.Scalar124 + mv1.Scalar346 * mv2.KVector3.Scalar234 - mv1.Scalar156 * mv2.KVector3.Scalar125 + mv1.Scalar356 * mv2.KVector3.Scalar235 + mv1.Scalar456 * mv2.KVector3.Scalar245;
            tempScalar[36] += -mv1.Scalar123 * mv2.KVector3.Scalar126 + mv1.Scalar134 * mv2.KVector3.Scalar146 + mv1.Scalar234 * mv2.KVector3.Scalar246 + mv1.Scalar135 * mv2.KVector3.Scalar156 + mv1.Scalar235 * mv2.KVector3.Scalar256 - mv1.Scalar345 * mv2.KVector3.Scalar456 + mv1.Scalar126 * mv2.KVector3.Scalar123 - mv1.Scalar146 * mv2.KVector3.Scalar134 - mv1.Scalar246 * mv2.KVector3.Scalar234 - mv1.Scalar156 * mv2.KVector3.Scalar135 - mv1.Scalar256 * mv2.KVector3.Scalar235 + mv1.Scalar456 * mv2.KVector3.Scalar345;
            tempScalar[40] += -mv1.Scalar124 * mv2.KVector3.Scalar126 - mv1.Scalar134 * mv2.KVector3.Scalar136 - mv1.Scalar234 * mv2.KVector3.Scalar236 + mv1.Scalar145 * mv2.KVector3.Scalar156 + mv1.Scalar245 * mv2.KVector3.Scalar256 + mv1.Scalar345 * mv2.KVector3.Scalar356 + mv1.Scalar126 * mv2.KVector3.Scalar124 + mv1.Scalar136 * mv2.KVector3.Scalar134 + mv1.Scalar236 * mv2.KVector3.Scalar234 - mv1.Scalar156 * mv2.KVector3.Scalar145 - mv1.Scalar256 * mv2.KVector3.Scalar245 - mv1.Scalar356 * mv2.KVector3.Scalar345;
            tempScalar[48] += -mv1.Scalar125 * mv2.KVector3.Scalar126 - mv1.Scalar135 * mv2.KVector3.Scalar136 - mv1.Scalar235 * mv2.KVector3.Scalar236 - mv1.Scalar145 * mv2.KVector3.Scalar146 - mv1.Scalar245 * mv2.KVector3.Scalar246 - mv1.Scalar345 * mv2.KVector3.Scalar346 + mv1.Scalar126 * mv2.KVector3.Scalar125 + mv1.Scalar136 * mv2.KVector3.Scalar135 + mv1.Scalar236 * mv2.KVector3.Scalar235 + mv1.Scalar146 * mv2.KVector3.Scalar145 + mv1.Scalar246 * mv2.KVector3.Scalar245 + mv1.Scalar346 * mv2.KVector3.Scalar345;
            tempScalar[63] += mv1.Scalar123 * mv2.KVector3.Scalar456 - mv1.Scalar124 * mv2.KVector3.Scalar356 + mv1.Scalar134 * mv2.KVector3.Scalar256 - mv1.Scalar234 * mv2.KVector3.Scalar156 + mv1.Scalar125 * mv2.KVector3.Scalar346 - mv1.Scalar135 * mv2.KVector3.Scalar246 + mv1.Scalar235 * mv2.KVector3.Scalar146 + mv1.Scalar145 * mv2.KVector3.Scalar236 - mv1.Scalar245 * mv2.KVector3.Scalar136 + mv1.Scalar345 * mv2.KVector3.Scalar126 - mv1.Scalar126 * mv2.KVector3.Scalar345 + mv1.Scalar136 * mv2.KVector3.Scalar245 - mv1.Scalar236 * mv2.KVector3.Scalar145 - mv1.Scalar146 * mv2.KVector3.Scalar235 + mv1.Scalar246 * mv2.KVector3.Scalar135 - mv1.Scalar346 * mv2.KVector3.Scalar125 + mv1.Scalar156 * mv2.KVector3.Scalar234 - mv1.Scalar256 * mv2.KVector3.Scalar134 + mv1.Scalar356 * mv2.KVector3.Scalar124 - mv1.Scalar456 * mv2.KVector3.Scalar123;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[1] += mv1.Scalar234 * mv2.KVector4.Scalar1234 + mv1.Scalar235 * mv2.KVector4.Scalar1235 + mv1.Scalar245 * mv2.KVector4.Scalar1245 + mv1.Scalar345 * mv2.KVector4.Scalar1345 + mv1.Scalar236 * mv2.KVector4.Scalar1236 + mv1.Scalar246 * mv2.KVector4.Scalar1246 + mv1.Scalar346 * mv2.KVector4.Scalar1346 + mv1.Scalar256 * mv2.KVector4.Scalar1256 + mv1.Scalar356 * mv2.KVector4.Scalar1356 + mv1.Scalar456 * mv2.KVector4.Scalar1456;
            tempScalar[2] += -mv1.Scalar134 * mv2.KVector4.Scalar1234 - mv1.Scalar135 * mv2.KVector4.Scalar1235 - mv1.Scalar145 * mv2.KVector4.Scalar1245 + mv1.Scalar345 * mv2.KVector4.Scalar2345 - mv1.Scalar136 * mv2.KVector4.Scalar1236 - mv1.Scalar146 * mv2.KVector4.Scalar1246 + mv1.Scalar346 * mv2.KVector4.Scalar2346 - mv1.Scalar156 * mv2.KVector4.Scalar1256 + mv1.Scalar356 * mv2.KVector4.Scalar2356 + mv1.Scalar456 * mv2.KVector4.Scalar2456;
            tempScalar[4] += mv1.Scalar124 * mv2.KVector4.Scalar1234 + mv1.Scalar125 * mv2.KVector4.Scalar1235 - mv1.Scalar145 * mv2.KVector4.Scalar1345 - mv1.Scalar245 * mv2.KVector4.Scalar2345 + mv1.Scalar126 * mv2.KVector4.Scalar1236 - mv1.Scalar146 * mv2.KVector4.Scalar1346 - mv1.Scalar246 * mv2.KVector4.Scalar2346 - mv1.Scalar156 * mv2.KVector4.Scalar1356 - mv1.Scalar256 * mv2.KVector4.Scalar2356 + mv1.Scalar456 * mv2.KVector4.Scalar3456;
            tempScalar[8] += -mv1.Scalar123 * mv2.KVector4.Scalar1234 + mv1.Scalar125 * mv2.KVector4.Scalar1245 + mv1.Scalar135 * mv2.KVector4.Scalar1345 + mv1.Scalar235 * mv2.KVector4.Scalar2345 + mv1.Scalar126 * mv2.KVector4.Scalar1246 + mv1.Scalar136 * mv2.KVector4.Scalar1346 + mv1.Scalar236 * mv2.KVector4.Scalar2346 - mv1.Scalar156 * mv2.KVector4.Scalar1456 - mv1.Scalar256 * mv2.KVector4.Scalar2456 - mv1.Scalar356 * mv2.KVector4.Scalar3456;
            tempScalar[16] += -mv1.Scalar123 * mv2.KVector4.Scalar1235 - mv1.Scalar124 * mv2.KVector4.Scalar1245 - mv1.Scalar134 * mv2.KVector4.Scalar1345 - mv1.Scalar234 * mv2.KVector4.Scalar2345 + mv1.Scalar126 * mv2.KVector4.Scalar1256 + mv1.Scalar136 * mv2.KVector4.Scalar1356 + mv1.Scalar236 * mv2.KVector4.Scalar2356 + mv1.Scalar146 * mv2.KVector4.Scalar1456 + mv1.Scalar246 * mv2.KVector4.Scalar2456 + mv1.Scalar346 * mv2.KVector4.Scalar3456;
            tempScalar[31] += -mv1.Scalar126 * mv2.KVector4.Scalar3456 + mv1.Scalar136 * mv2.KVector4.Scalar2456 - mv1.Scalar236 * mv2.KVector4.Scalar1456 - mv1.Scalar146 * mv2.KVector4.Scalar2356 + mv1.Scalar246 * mv2.KVector4.Scalar1356 - mv1.Scalar346 * mv2.KVector4.Scalar1256 + mv1.Scalar156 * mv2.KVector4.Scalar2346 - mv1.Scalar256 * mv2.KVector4.Scalar1346 + mv1.Scalar356 * mv2.KVector4.Scalar1246 - mv1.Scalar456 * mv2.KVector4.Scalar1236;
            tempScalar[32] += -mv1.Scalar123 * mv2.KVector4.Scalar1236 - mv1.Scalar124 * mv2.KVector4.Scalar1246 - mv1.Scalar134 * mv2.KVector4.Scalar1346 - mv1.Scalar234 * mv2.KVector4.Scalar2346 - mv1.Scalar125 * mv2.KVector4.Scalar1256 - mv1.Scalar135 * mv2.KVector4.Scalar1356 - mv1.Scalar235 * mv2.KVector4.Scalar2356 - mv1.Scalar145 * mv2.KVector4.Scalar1456 - mv1.Scalar245 * mv2.KVector4.Scalar2456 - mv1.Scalar345 * mv2.KVector4.Scalar3456;
            tempScalar[47] += mv1.Scalar125 * mv2.KVector4.Scalar3456 - mv1.Scalar135 * mv2.KVector4.Scalar2456 + mv1.Scalar235 * mv2.KVector4.Scalar1456 + mv1.Scalar145 * mv2.KVector4.Scalar2356 - mv1.Scalar245 * mv2.KVector4.Scalar1356 + mv1.Scalar345 * mv2.KVector4.Scalar1256 - mv1.Scalar156 * mv2.KVector4.Scalar2345 + mv1.Scalar256 * mv2.KVector4.Scalar1345 - mv1.Scalar356 * mv2.KVector4.Scalar1245 + mv1.Scalar456 * mv2.KVector4.Scalar1235;
            tempScalar[55] += -mv1.Scalar124 * mv2.KVector4.Scalar3456 + mv1.Scalar134 * mv2.KVector4.Scalar2456 - mv1.Scalar234 * mv2.KVector4.Scalar1456 - mv1.Scalar145 * mv2.KVector4.Scalar2346 + mv1.Scalar245 * mv2.KVector4.Scalar1346 - mv1.Scalar345 * mv2.KVector4.Scalar1246 + mv1.Scalar146 * mv2.KVector4.Scalar2345 - mv1.Scalar246 * mv2.KVector4.Scalar1345 + mv1.Scalar346 * mv2.KVector4.Scalar1245 - mv1.Scalar456 * mv2.KVector4.Scalar1234;
            tempScalar[59] += mv1.Scalar123 * mv2.KVector4.Scalar3456 - mv1.Scalar134 * mv2.KVector4.Scalar2356 + mv1.Scalar234 * mv2.KVector4.Scalar1356 + mv1.Scalar135 * mv2.KVector4.Scalar2346 - mv1.Scalar235 * mv2.KVector4.Scalar1346 + mv1.Scalar345 * mv2.KVector4.Scalar1236 - mv1.Scalar136 * mv2.KVector4.Scalar2345 + mv1.Scalar236 * mv2.KVector4.Scalar1345 - mv1.Scalar346 * mv2.KVector4.Scalar1235 + mv1.Scalar356 * mv2.KVector4.Scalar1234;
            tempScalar[61] += -mv1.Scalar123 * mv2.KVector4.Scalar2456 + mv1.Scalar124 * mv2.KVector4.Scalar2356 - mv1.Scalar234 * mv2.KVector4.Scalar1256 - mv1.Scalar125 * mv2.KVector4.Scalar2346 + mv1.Scalar235 * mv2.KVector4.Scalar1246 - mv1.Scalar245 * mv2.KVector4.Scalar1236 + mv1.Scalar126 * mv2.KVector4.Scalar2345 - mv1.Scalar236 * mv2.KVector4.Scalar1245 + mv1.Scalar246 * mv2.KVector4.Scalar1235 - mv1.Scalar256 * mv2.KVector4.Scalar1234;
            tempScalar[62] += mv1.Scalar123 * mv2.KVector4.Scalar1456 - mv1.Scalar124 * mv2.KVector4.Scalar1356 + mv1.Scalar134 * mv2.KVector4.Scalar1256 + mv1.Scalar125 * mv2.KVector4.Scalar1346 - mv1.Scalar135 * mv2.KVector4.Scalar1246 + mv1.Scalar145 * mv2.KVector4.Scalar1236 - mv1.Scalar126 * mv2.KVector4.Scalar1345 + mv1.Scalar136 * mv2.KVector4.Scalar1245 - mv1.Scalar146 * mv2.KVector4.Scalar1235 + mv1.Scalar156 * mv2.KVector4.Scalar1234;
        }
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[15] += -mv1.Scalar156 * mv2.KVector5.Scalar23456 + mv1.Scalar256 * mv2.KVector5.Scalar13456 - mv1.Scalar356 * mv2.KVector5.Scalar12456 + mv1.Scalar456 * mv2.KVector5.Scalar12356;
            tempScalar[23] += mv1.Scalar146 * mv2.KVector5.Scalar23456 - mv1.Scalar246 * mv2.KVector5.Scalar13456 + mv1.Scalar346 * mv2.KVector5.Scalar12456 - mv1.Scalar456 * mv2.KVector5.Scalar12346;
            tempScalar[27] += -mv1.Scalar136 * mv2.KVector5.Scalar23456 + mv1.Scalar236 * mv2.KVector5.Scalar13456 - mv1.Scalar346 * mv2.KVector5.Scalar12356 + mv1.Scalar356 * mv2.KVector5.Scalar12346;
            tempScalar[29] += mv1.Scalar126 * mv2.KVector5.Scalar23456 - mv1.Scalar236 * mv2.KVector5.Scalar12456 + mv1.Scalar246 * mv2.KVector5.Scalar12356 - mv1.Scalar256 * mv2.KVector5.Scalar12346;
            tempScalar[30] += -mv1.Scalar126 * mv2.KVector5.Scalar13456 + mv1.Scalar136 * mv2.KVector5.Scalar12456 - mv1.Scalar146 * mv2.KVector5.Scalar12356 + mv1.Scalar156 * mv2.KVector5.Scalar12346;
            tempScalar[39] += -mv1.Scalar145 * mv2.KVector5.Scalar23456 + mv1.Scalar245 * mv2.KVector5.Scalar13456 - mv1.Scalar345 * mv2.KVector5.Scalar12456 + mv1.Scalar456 * mv2.KVector5.Scalar12345;
            tempScalar[43] += mv1.Scalar135 * mv2.KVector5.Scalar23456 - mv1.Scalar235 * mv2.KVector5.Scalar13456 + mv1.Scalar345 * mv2.KVector5.Scalar12356 - mv1.Scalar356 * mv2.KVector5.Scalar12345;
            tempScalar[45] += -mv1.Scalar125 * mv2.KVector5.Scalar23456 + mv1.Scalar235 * mv2.KVector5.Scalar12456 - mv1.Scalar245 * mv2.KVector5.Scalar12356 + mv1.Scalar256 * mv2.KVector5.Scalar12345;
            tempScalar[46] += mv1.Scalar125 * mv2.KVector5.Scalar13456 - mv1.Scalar135 * mv2.KVector5.Scalar12456 + mv1.Scalar145 * mv2.KVector5.Scalar12356 - mv1.Scalar156 * mv2.KVector5.Scalar12345;
            tempScalar[51] += -mv1.Scalar134 * mv2.KVector5.Scalar23456 + mv1.Scalar234 * mv2.KVector5.Scalar13456 - mv1.Scalar345 * mv2.KVector5.Scalar12346 + mv1.Scalar346 * mv2.KVector5.Scalar12345;
            tempScalar[53] += mv1.Scalar124 * mv2.KVector5.Scalar23456 - mv1.Scalar234 * mv2.KVector5.Scalar12456 + mv1.Scalar245 * mv2.KVector5.Scalar12346 - mv1.Scalar246 * mv2.KVector5.Scalar12345;
            tempScalar[54] += -mv1.Scalar124 * mv2.KVector5.Scalar13456 + mv1.Scalar134 * mv2.KVector5.Scalar12456 - mv1.Scalar145 * mv2.KVector5.Scalar12346 + mv1.Scalar146 * mv2.KVector5.Scalar12345;
            tempScalar[57] += -mv1.Scalar123 * mv2.KVector5.Scalar23456 + mv1.Scalar234 * mv2.KVector5.Scalar12356 - mv1.Scalar235 * mv2.KVector5.Scalar12346 + mv1.Scalar236 * mv2.KVector5.Scalar12345;
            tempScalar[58] += mv1.Scalar123 * mv2.KVector5.Scalar13456 - mv1.Scalar134 * mv2.KVector5.Scalar12356 + mv1.Scalar135 * mv2.KVector5.Scalar12346 - mv1.Scalar136 * mv2.KVector5.Scalar12345;
            tempScalar[60] += -mv1.Scalar123 * mv2.KVector5.Scalar12456 + mv1.Scalar124 * mv2.KVector5.Scalar12356 - mv1.Scalar125 * mv2.KVector5.Scalar12346 + mv1.Scalar126 * mv2.KVector5.Scalar12345;
        }
        
        if (!mv2.KVector6.IsZero())
        {
            tempScalar[7] += mv1.Scalar456 * mv2.KVector6.Scalar123456;
            tempScalar[11] += -mv1.Scalar356 * mv2.KVector6.Scalar123456;
            tempScalar[13] += mv1.Scalar256 * mv2.KVector6.Scalar123456;
            tempScalar[14] += -mv1.Scalar156 * mv2.KVector6.Scalar123456;
            tempScalar[19] += mv1.Scalar346 * mv2.KVector6.Scalar123456;
            tempScalar[21] += -mv1.Scalar246 * mv2.KVector6.Scalar123456;
            tempScalar[22] += mv1.Scalar146 * mv2.KVector6.Scalar123456;
            tempScalar[25] += mv1.Scalar236 * mv2.KVector6.Scalar123456;
            tempScalar[26] += -mv1.Scalar136 * mv2.KVector6.Scalar123456;
            tempScalar[28] += mv1.Scalar126 * mv2.KVector6.Scalar123456;
            tempScalar[35] += -mv1.Scalar345 * mv2.KVector6.Scalar123456;
            tempScalar[37] += mv1.Scalar245 * mv2.KVector6.Scalar123456;
            tempScalar[38] += -mv1.Scalar145 * mv2.KVector6.Scalar123456;
            tempScalar[41] += -mv1.Scalar235 * mv2.KVector6.Scalar123456;
            tempScalar[42] += mv1.Scalar135 * mv2.KVector6.Scalar123456;
            tempScalar[44] += -mv1.Scalar125 * mv2.KVector6.Scalar123456;
            tempScalar[49] += mv1.Scalar234 * mv2.KVector6.Scalar123456;
            tempScalar[50] += -mv1.Scalar134 * mv2.KVector6.Scalar123456;
            tempScalar[52] += mv1.Scalar124 * mv2.KVector6.Scalar123456;
            tempScalar[56] += -mv1.Scalar123 * mv2.KVector6.Scalar123456;
        }
        
        return Ga6Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector4 mv1, Ga6KVector0 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector3 Cp(this Ga6KVector4 mv1, Ga6KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector3.Zero;
        
        return new Ga6KVector3
        {
            Scalar123 = mv1.Scalar1234 * mv2.Scalar4 + mv1.Scalar1235 * mv2.Scalar5 + mv1.Scalar1236 * mv2.Scalar6,
            Scalar124 = -mv1.Scalar1234 * mv2.Scalar3 + mv1.Scalar1245 * mv2.Scalar5 + mv1.Scalar1246 * mv2.Scalar6,
            Scalar134 = mv1.Scalar1234 * mv2.Scalar2 + mv1.Scalar1345 * mv2.Scalar5 + mv1.Scalar1346 * mv2.Scalar6,
            Scalar234 = -mv1.Scalar1234 * mv2.Scalar1 + mv1.Scalar2345 * mv2.Scalar5 + mv1.Scalar2346 * mv2.Scalar6,
            Scalar125 = -mv1.Scalar1235 * mv2.Scalar3 - mv1.Scalar1245 * mv2.Scalar4 + mv1.Scalar1256 * mv2.Scalar6,
            Scalar135 = mv1.Scalar1235 * mv2.Scalar2 - mv1.Scalar1345 * mv2.Scalar4 + mv1.Scalar1356 * mv2.Scalar6,
            Scalar235 = -mv1.Scalar1235 * mv2.Scalar1 - mv1.Scalar2345 * mv2.Scalar4 + mv1.Scalar2356 * mv2.Scalar6,
            Scalar145 = mv1.Scalar1245 * mv2.Scalar2 + mv1.Scalar1345 * mv2.Scalar3 + mv1.Scalar1456 * mv2.Scalar6,
            Scalar245 = -mv1.Scalar1245 * mv2.Scalar1 + mv1.Scalar2345 * mv2.Scalar3 + mv1.Scalar2456 * mv2.Scalar6,
            Scalar345 = -mv1.Scalar1345 * mv2.Scalar1 - mv1.Scalar2345 * mv2.Scalar2 + mv1.Scalar3456 * mv2.Scalar6,
            Scalar126 = -mv1.Scalar1236 * mv2.Scalar3 - mv1.Scalar1246 * mv2.Scalar4 - mv1.Scalar1256 * mv2.Scalar5,
            Scalar136 = mv1.Scalar1236 * mv2.Scalar2 - mv1.Scalar1346 * mv2.Scalar4 - mv1.Scalar1356 * mv2.Scalar5,
            Scalar236 = -mv1.Scalar1236 * mv2.Scalar1 - mv1.Scalar2346 * mv2.Scalar4 - mv1.Scalar2356 * mv2.Scalar5,
            Scalar146 = mv1.Scalar1246 * mv2.Scalar2 + mv1.Scalar1346 * mv2.Scalar3 - mv1.Scalar1456 * mv2.Scalar5,
            Scalar246 = -mv1.Scalar1246 * mv2.Scalar1 + mv1.Scalar2346 * mv2.Scalar3 - mv1.Scalar2456 * mv2.Scalar5,
            Scalar346 = -mv1.Scalar1346 * mv2.Scalar1 - mv1.Scalar2346 * mv2.Scalar2 - mv1.Scalar3456 * mv2.Scalar5,
            Scalar156 = mv1.Scalar1256 * mv2.Scalar2 + mv1.Scalar1356 * mv2.Scalar3 + mv1.Scalar1456 * mv2.Scalar4,
            Scalar256 = -mv1.Scalar1256 * mv2.Scalar1 + mv1.Scalar2356 * mv2.Scalar3 + mv1.Scalar2456 * mv2.Scalar4,
            Scalar356 = -mv1.Scalar1356 * mv2.Scalar1 - mv1.Scalar2356 * mv2.Scalar2 + mv1.Scalar3456 * mv2.Scalar4,
            Scalar456 = -mv1.Scalar1456 * mv2.Scalar1 - mv1.Scalar2456 * mv2.Scalar2 - mv1.Scalar3456 * mv2.Scalar3
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector4 Cp(this Ga6KVector4 mv1, Ga6KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector4.Zero;
        
        return new Ga6KVector4
        {
            Scalar1234 = -mv1.Scalar1235 * mv2.Scalar45 + mv1.Scalar1245 * mv2.Scalar35 - mv1.Scalar1345 * mv2.Scalar25 + mv1.Scalar2345 * mv2.Scalar15 - mv1.Scalar1236 * mv2.Scalar46 + mv1.Scalar1246 * mv2.Scalar36 - mv1.Scalar1346 * mv2.Scalar26 + mv1.Scalar2346 * mv2.Scalar16,
            Scalar1235 = mv1.Scalar1234 * mv2.Scalar45 - mv1.Scalar1245 * mv2.Scalar34 + mv1.Scalar1345 * mv2.Scalar24 - mv1.Scalar2345 * mv2.Scalar14 - mv1.Scalar1236 * mv2.Scalar56 + mv1.Scalar1256 * mv2.Scalar36 - mv1.Scalar1356 * mv2.Scalar26 + mv1.Scalar2356 * mv2.Scalar16,
            Scalar1245 = -mv1.Scalar1234 * mv2.Scalar35 + mv1.Scalar1235 * mv2.Scalar34 - mv1.Scalar1345 * mv2.Scalar23 + mv1.Scalar2345 * mv2.Scalar13 - mv1.Scalar1246 * mv2.Scalar56 + mv1.Scalar1256 * mv2.Scalar46 - mv1.Scalar1456 * mv2.Scalar26 + mv1.Scalar2456 * mv2.Scalar16,
            Scalar1345 = mv1.Scalar1234 * mv2.Scalar25 - mv1.Scalar1235 * mv2.Scalar24 + mv1.Scalar1245 * mv2.Scalar23 - mv1.Scalar2345 * mv2.Scalar12 - mv1.Scalar1346 * mv2.Scalar56 + mv1.Scalar1356 * mv2.Scalar46 - mv1.Scalar1456 * mv2.Scalar36 + mv1.Scalar3456 * mv2.Scalar16,
            Scalar2345 = -mv1.Scalar1234 * mv2.Scalar15 + mv1.Scalar1235 * mv2.Scalar14 - mv1.Scalar1245 * mv2.Scalar13 + mv1.Scalar1345 * mv2.Scalar12 - mv1.Scalar2346 * mv2.Scalar56 + mv1.Scalar2356 * mv2.Scalar46 - mv1.Scalar2456 * mv2.Scalar36 + mv1.Scalar3456 * mv2.Scalar26,
            Scalar1236 = mv1.Scalar1234 * mv2.Scalar46 + mv1.Scalar1235 * mv2.Scalar56 - mv1.Scalar1246 * mv2.Scalar34 + mv1.Scalar1346 * mv2.Scalar24 - mv1.Scalar2346 * mv2.Scalar14 - mv1.Scalar1256 * mv2.Scalar35 + mv1.Scalar1356 * mv2.Scalar25 - mv1.Scalar2356 * mv2.Scalar15,
            Scalar1246 = -mv1.Scalar1234 * mv2.Scalar36 + mv1.Scalar1245 * mv2.Scalar56 + mv1.Scalar1236 * mv2.Scalar34 - mv1.Scalar1346 * mv2.Scalar23 + mv1.Scalar2346 * mv2.Scalar13 - mv1.Scalar1256 * mv2.Scalar45 + mv1.Scalar1456 * mv2.Scalar25 - mv1.Scalar2456 * mv2.Scalar15,
            Scalar1346 = mv1.Scalar1234 * mv2.Scalar26 + mv1.Scalar1345 * mv2.Scalar56 - mv1.Scalar1236 * mv2.Scalar24 + mv1.Scalar1246 * mv2.Scalar23 - mv1.Scalar2346 * mv2.Scalar12 - mv1.Scalar1356 * mv2.Scalar45 + mv1.Scalar1456 * mv2.Scalar35 - mv1.Scalar3456 * mv2.Scalar15,
            Scalar2346 = -mv1.Scalar1234 * mv2.Scalar16 + mv1.Scalar2345 * mv2.Scalar56 + mv1.Scalar1236 * mv2.Scalar14 - mv1.Scalar1246 * mv2.Scalar13 + mv1.Scalar1346 * mv2.Scalar12 - mv1.Scalar2356 * mv2.Scalar45 + mv1.Scalar2456 * mv2.Scalar35 - mv1.Scalar3456 * mv2.Scalar25,
            Scalar1256 = -mv1.Scalar1235 * mv2.Scalar36 - mv1.Scalar1245 * mv2.Scalar46 + mv1.Scalar1236 * mv2.Scalar35 + mv1.Scalar1246 * mv2.Scalar45 - mv1.Scalar1356 * mv2.Scalar23 + mv1.Scalar2356 * mv2.Scalar13 - mv1.Scalar1456 * mv2.Scalar24 + mv1.Scalar2456 * mv2.Scalar14,
            Scalar1356 = mv1.Scalar1235 * mv2.Scalar26 - mv1.Scalar1345 * mv2.Scalar46 - mv1.Scalar1236 * mv2.Scalar25 + mv1.Scalar1346 * mv2.Scalar45 + mv1.Scalar1256 * mv2.Scalar23 - mv1.Scalar2356 * mv2.Scalar12 - mv1.Scalar1456 * mv2.Scalar34 + mv1.Scalar3456 * mv2.Scalar14,
            Scalar2356 = -mv1.Scalar1235 * mv2.Scalar16 - mv1.Scalar2345 * mv2.Scalar46 + mv1.Scalar1236 * mv2.Scalar15 + mv1.Scalar2346 * mv2.Scalar45 - mv1.Scalar1256 * mv2.Scalar13 + mv1.Scalar1356 * mv2.Scalar12 - mv1.Scalar2456 * mv2.Scalar34 + mv1.Scalar3456 * mv2.Scalar24,
            Scalar1456 = mv1.Scalar1245 * mv2.Scalar26 + mv1.Scalar1345 * mv2.Scalar36 - mv1.Scalar1246 * mv2.Scalar25 - mv1.Scalar1346 * mv2.Scalar35 + mv1.Scalar1256 * mv2.Scalar24 + mv1.Scalar1356 * mv2.Scalar34 - mv1.Scalar2456 * mv2.Scalar12 - mv1.Scalar3456 * mv2.Scalar13,
            Scalar2456 = -mv1.Scalar1245 * mv2.Scalar16 + mv1.Scalar2345 * mv2.Scalar36 + mv1.Scalar1246 * mv2.Scalar15 - mv1.Scalar2346 * mv2.Scalar35 - mv1.Scalar1256 * mv2.Scalar14 + mv1.Scalar2356 * mv2.Scalar34 + mv1.Scalar1456 * mv2.Scalar12 - mv1.Scalar3456 * mv2.Scalar23,
            Scalar3456 = -mv1.Scalar1345 * mv2.Scalar16 - mv1.Scalar2345 * mv2.Scalar26 + mv1.Scalar1346 * mv2.Scalar15 + mv1.Scalar2346 * mv2.Scalar25 - mv1.Scalar1356 * mv2.Scalar14 - mv1.Scalar2356 * mv2.Scalar24 + mv1.Scalar1456 * mv2.Scalar13 + mv1.Scalar2456 * mv2.Scalar23
        };
    }
    
    public static Ga6Multivector Cp(this Ga6KVector4 mv1, Ga6KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv2.IsZero())
        {
            tempScalar[1] += -mv1.Scalar1234 * mv2.Scalar234 - mv1.Scalar1235 * mv2.Scalar235 - mv1.Scalar1245 * mv2.Scalar245 - mv1.Scalar1345 * mv2.Scalar345 - mv1.Scalar1236 * mv2.Scalar236 - mv1.Scalar1246 * mv2.Scalar246 - mv1.Scalar1346 * mv2.Scalar346 - mv1.Scalar1256 * mv2.Scalar256 - mv1.Scalar1356 * mv2.Scalar356 - mv1.Scalar1456 * mv2.Scalar456;
            tempScalar[2] += mv1.Scalar1234 * mv2.Scalar134 + mv1.Scalar1235 * mv2.Scalar135 + mv1.Scalar1245 * mv2.Scalar145 - mv1.Scalar2345 * mv2.Scalar345 + mv1.Scalar1236 * mv2.Scalar136 + mv1.Scalar1246 * mv2.Scalar146 - mv1.Scalar2346 * mv2.Scalar346 + mv1.Scalar1256 * mv2.Scalar156 - mv1.Scalar2356 * mv2.Scalar356 - mv1.Scalar2456 * mv2.Scalar456;
            tempScalar[4] += -mv1.Scalar1234 * mv2.Scalar124 - mv1.Scalar1235 * mv2.Scalar125 + mv1.Scalar1345 * mv2.Scalar145 + mv1.Scalar2345 * mv2.Scalar245 - mv1.Scalar1236 * mv2.Scalar126 + mv1.Scalar1346 * mv2.Scalar146 + mv1.Scalar2346 * mv2.Scalar246 + mv1.Scalar1356 * mv2.Scalar156 + mv1.Scalar2356 * mv2.Scalar256 - mv1.Scalar3456 * mv2.Scalar456;
            tempScalar[8] += mv1.Scalar1234 * mv2.Scalar123 - mv1.Scalar1245 * mv2.Scalar125 - mv1.Scalar1345 * mv2.Scalar135 - mv1.Scalar2345 * mv2.Scalar235 - mv1.Scalar1246 * mv2.Scalar126 - mv1.Scalar1346 * mv2.Scalar136 - mv1.Scalar2346 * mv2.Scalar236 + mv1.Scalar1456 * mv2.Scalar156 + mv1.Scalar2456 * mv2.Scalar256 + mv1.Scalar3456 * mv2.Scalar356;
            tempScalar[16] += mv1.Scalar1235 * mv2.Scalar123 + mv1.Scalar1245 * mv2.Scalar124 + mv1.Scalar1345 * mv2.Scalar134 + mv1.Scalar2345 * mv2.Scalar234 - mv1.Scalar1256 * mv2.Scalar126 - mv1.Scalar1356 * mv2.Scalar136 - mv1.Scalar2356 * mv2.Scalar236 - mv1.Scalar1456 * mv2.Scalar146 - mv1.Scalar2456 * mv2.Scalar246 - mv1.Scalar3456 * mv2.Scalar346;
            tempScalar[31] += mv1.Scalar1236 * mv2.Scalar456 - mv1.Scalar1246 * mv2.Scalar356 + mv1.Scalar1346 * mv2.Scalar256 - mv1.Scalar2346 * mv2.Scalar156 + mv1.Scalar1256 * mv2.Scalar346 - mv1.Scalar1356 * mv2.Scalar246 + mv1.Scalar2356 * mv2.Scalar146 + mv1.Scalar1456 * mv2.Scalar236 - mv1.Scalar2456 * mv2.Scalar136 + mv1.Scalar3456 * mv2.Scalar126;
            tempScalar[32] += mv1.Scalar1236 * mv2.Scalar123 + mv1.Scalar1246 * mv2.Scalar124 + mv1.Scalar1346 * mv2.Scalar134 + mv1.Scalar2346 * mv2.Scalar234 + mv1.Scalar1256 * mv2.Scalar125 + mv1.Scalar1356 * mv2.Scalar135 + mv1.Scalar2356 * mv2.Scalar235 + mv1.Scalar1456 * mv2.Scalar145 + mv1.Scalar2456 * mv2.Scalar245 + mv1.Scalar3456 * mv2.Scalar345;
            tempScalar[47] += -mv1.Scalar1235 * mv2.Scalar456 + mv1.Scalar1245 * mv2.Scalar356 - mv1.Scalar1345 * mv2.Scalar256 + mv1.Scalar2345 * mv2.Scalar156 - mv1.Scalar1256 * mv2.Scalar345 + mv1.Scalar1356 * mv2.Scalar245 - mv1.Scalar2356 * mv2.Scalar145 - mv1.Scalar1456 * mv2.Scalar235 + mv1.Scalar2456 * mv2.Scalar135 - mv1.Scalar3456 * mv2.Scalar125;
            tempScalar[55] += mv1.Scalar1234 * mv2.Scalar456 - mv1.Scalar1245 * mv2.Scalar346 + mv1.Scalar1345 * mv2.Scalar246 - mv1.Scalar2345 * mv2.Scalar146 + mv1.Scalar1246 * mv2.Scalar345 - mv1.Scalar1346 * mv2.Scalar245 + mv1.Scalar2346 * mv2.Scalar145 + mv1.Scalar1456 * mv2.Scalar234 - mv1.Scalar2456 * mv2.Scalar134 + mv1.Scalar3456 * mv2.Scalar124;
            tempScalar[59] += -mv1.Scalar1234 * mv2.Scalar356 + mv1.Scalar1235 * mv2.Scalar346 - mv1.Scalar1345 * mv2.Scalar236 + mv1.Scalar2345 * mv2.Scalar136 - mv1.Scalar1236 * mv2.Scalar345 + mv1.Scalar1346 * mv2.Scalar235 - mv1.Scalar2346 * mv2.Scalar135 - mv1.Scalar1356 * mv2.Scalar234 + mv1.Scalar2356 * mv2.Scalar134 - mv1.Scalar3456 * mv2.Scalar123;
            tempScalar[61] += mv1.Scalar1234 * mv2.Scalar256 - mv1.Scalar1235 * mv2.Scalar246 + mv1.Scalar1245 * mv2.Scalar236 - mv1.Scalar2345 * mv2.Scalar126 + mv1.Scalar1236 * mv2.Scalar245 - mv1.Scalar1246 * mv2.Scalar235 + mv1.Scalar2346 * mv2.Scalar125 + mv1.Scalar1256 * mv2.Scalar234 - mv1.Scalar2356 * mv2.Scalar124 + mv1.Scalar2456 * mv2.Scalar123;
            tempScalar[62] += -mv1.Scalar1234 * mv2.Scalar156 + mv1.Scalar1235 * mv2.Scalar146 - mv1.Scalar1245 * mv2.Scalar136 + mv1.Scalar1345 * mv2.Scalar126 - mv1.Scalar1236 * mv2.Scalar145 + mv1.Scalar1246 * mv2.Scalar135 - mv1.Scalar1346 * mv2.Scalar125 - mv1.Scalar1256 * mv2.Scalar134 + mv1.Scalar1356 * mv2.Scalar124 - mv1.Scalar1456 * mv2.Scalar123;
        }
        
        return Ga6Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector2 Cp(this Ga6KVector4 mv1, Ga6KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector2.Zero;
        
        return new Ga6KVector2
        {
            Scalar12 = mv1.Scalar1345 * mv2.Scalar2345 - mv1.Scalar2345 * mv2.Scalar1345 + mv1.Scalar1346 * mv2.Scalar2346 - mv1.Scalar2346 * mv2.Scalar1346 + mv1.Scalar1356 * mv2.Scalar2356 - mv1.Scalar2356 * mv2.Scalar1356 + mv1.Scalar1456 * mv2.Scalar2456 - mv1.Scalar2456 * mv2.Scalar1456,
            Scalar13 = -mv1.Scalar1245 * mv2.Scalar2345 + mv1.Scalar2345 * mv2.Scalar1245 - mv1.Scalar1246 * mv2.Scalar2346 + mv1.Scalar2346 * mv2.Scalar1246 - mv1.Scalar1256 * mv2.Scalar2356 + mv1.Scalar2356 * mv2.Scalar1256 + mv1.Scalar1456 * mv2.Scalar3456 - mv1.Scalar3456 * mv2.Scalar1456,
            Scalar23 = mv1.Scalar1245 * mv2.Scalar1345 - mv1.Scalar1345 * mv2.Scalar1245 + mv1.Scalar1246 * mv2.Scalar1346 - mv1.Scalar1346 * mv2.Scalar1246 + mv1.Scalar1256 * mv2.Scalar1356 - mv1.Scalar1356 * mv2.Scalar1256 + mv1.Scalar2456 * mv2.Scalar3456 - mv1.Scalar3456 * mv2.Scalar2456,
            Scalar14 = mv1.Scalar1235 * mv2.Scalar2345 - mv1.Scalar2345 * mv2.Scalar1235 + mv1.Scalar1236 * mv2.Scalar2346 - mv1.Scalar2346 * mv2.Scalar1236 - mv1.Scalar1256 * mv2.Scalar2456 - mv1.Scalar1356 * mv2.Scalar3456 + mv1.Scalar2456 * mv2.Scalar1256 + mv1.Scalar3456 * mv2.Scalar1356,
            Scalar24 = -mv1.Scalar1235 * mv2.Scalar1345 + mv1.Scalar1345 * mv2.Scalar1235 - mv1.Scalar1236 * mv2.Scalar1346 + mv1.Scalar1346 * mv2.Scalar1236 + mv1.Scalar1256 * mv2.Scalar1456 - mv1.Scalar2356 * mv2.Scalar3456 - mv1.Scalar1456 * mv2.Scalar1256 + mv1.Scalar3456 * mv2.Scalar2356,
            Scalar34 = mv1.Scalar1235 * mv2.Scalar1245 - mv1.Scalar1245 * mv2.Scalar1235 + mv1.Scalar1236 * mv2.Scalar1246 - mv1.Scalar1246 * mv2.Scalar1236 + mv1.Scalar1356 * mv2.Scalar1456 + mv1.Scalar2356 * mv2.Scalar2456 - mv1.Scalar1456 * mv2.Scalar1356 - mv1.Scalar2456 * mv2.Scalar2356,
            Scalar15 = -mv1.Scalar1234 * mv2.Scalar2345 + mv1.Scalar2345 * mv2.Scalar1234 + mv1.Scalar1236 * mv2.Scalar2356 + mv1.Scalar1246 * mv2.Scalar2456 + mv1.Scalar1346 * mv2.Scalar3456 - mv1.Scalar2356 * mv2.Scalar1236 - mv1.Scalar2456 * mv2.Scalar1246 - mv1.Scalar3456 * mv2.Scalar1346,
            Scalar25 = mv1.Scalar1234 * mv2.Scalar1345 - mv1.Scalar1345 * mv2.Scalar1234 - mv1.Scalar1236 * mv2.Scalar1356 - mv1.Scalar1246 * mv2.Scalar1456 + mv1.Scalar2346 * mv2.Scalar3456 + mv1.Scalar1356 * mv2.Scalar1236 + mv1.Scalar1456 * mv2.Scalar1246 - mv1.Scalar3456 * mv2.Scalar2346,
            Scalar35 = -mv1.Scalar1234 * mv2.Scalar1245 + mv1.Scalar1245 * mv2.Scalar1234 + mv1.Scalar1236 * mv2.Scalar1256 - mv1.Scalar1346 * mv2.Scalar1456 - mv1.Scalar2346 * mv2.Scalar2456 - mv1.Scalar1256 * mv2.Scalar1236 + mv1.Scalar1456 * mv2.Scalar1346 + mv1.Scalar2456 * mv2.Scalar2346,
            Scalar45 = mv1.Scalar1234 * mv2.Scalar1235 - mv1.Scalar1235 * mv2.Scalar1234 + mv1.Scalar1246 * mv2.Scalar1256 + mv1.Scalar1346 * mv2.Scalar1356 + mv1.Scalar2346 * mv2.Scalar2356 - mv1.Scalar1256 * mv2.Scalar1246 - mv1.Scalar1356 * mv2.Scalar1346 - mv1.Scalar2356 * mv2.Scalar2346,
            Scalar16 = -mv1.Scalar1234 * mv2.Scalar2346 - mv1.Scalar1235 * mv2.Scalar2356 - mv1.Scalar1245 * mv2.Scalar2456 - mv1.Scalar1345 * mv2.Scalar3456 + mv1.Scalar2346 * mv2.Scalar1234 + mv1.Scalar2356 * mv2.Scalar1235 + mv1.Scalar2456 * mv2.Scalar1245 + mv1.Scalar3456 * mv2.Scalar1345,
            Scalar26 = mv1.Scalar1234 * mv2.Scalar1346 + mv1.Scalar1235 * mv2.Scalar1356 + mv1.Scalar1245 * mv2.Scalar1456 - mv1.Scalar2345 * mv2.Scalar3456 - mv1.Scalar1346 * mv2.Scalar1234 - mv1.Scalar1356 * mv2.Scalar1235 - mv1.Scalar1456 * mv2.Scalar1245 + mv1.Scalar3456 * mv2.Scalar2345,
            Scalar36 = -mv1.Scalar1234 * mv2.Scalar1246 - mv1.Scalar1235 * mv2.Scalar1256 + mv1.Scalar1345 * mv2.Scalar1456 + mv1.Scalar2345 * mv2.Scalar2456 + mv1.Scalar1246 * mv2.Scalar1234 + mv1.Scalar1256 * mv2.Scalar1235 - mv1.Scalar1456 * mv2.Scalar1345 - mv1.Scalar2456 * mv2.Scalar2345,
            Scalar46 = mv1.Scalar1234 * mv2.Scalar1236 - mv1.Scalar1245 * mv2.Scalar1256 - mv1.Scalar1345 * mv2.Scalar1356 - mv1.Scalar2345 * mv2.Scalar2356 - mv1.Scalar1236 * mv2.Scalar1234 + mv1.Scalar1256 * mv2.Scalar1245 + mv1.Scalar1356 * mv2.Scalar1345 + mv1.Scalar2356 * mv2.Scalar2345,
            Scalar56 = mv1.Scalar1235 * mv2.Scalar1236 + mv1.Scalar1245 * mv2.Scalar1246 + mv1.Scalar1345 * mv2.Scalar1346 + mv1.Scalar2345 * mv2.Scalar2346 - mv1.Scalar1236 * mv2.Scalar1235 - mv1.Scalar1246 * mv2.Scalar1245 - mv1.Scalar1346 * mv2.Scalar1345 - mv1.Scalar2346 * mv2.Scalar2345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector3 Cp(this Ga6KVector4 mv1, Ga6KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector3.Zero;
        
        return new Ga6KVector3
        {
            Scalar123 = -mv1.Scalar1456 * mv2.Scalar23456 + mv1.Scalar2456 * mv2.Scalar13456 - mv1.Scalar3456 * mv2.Scalar12456,
            Scalar124 = mv1.Scalar1356 * mv2.Scalar23456 - mv1.Scalar2356 * mv2.Scalar13456 + mv1.Scalar3456 * mv2.Scalar12356,
            Scalar134 = -mv1.Scalar1256 * mv2.Scalar23456 + mv1.Scalar2356 * mv2.Scalar12456 - mv1.Scalar2456 * mv2.Scalar12356,
            Scalar234 = mv1.Scalar1256 * mv2.Scalar13456 - mv1.Scalar1356 * mv2.Scalar12456 + mv1.Scalar1456 * mv2.Scalar12356,
            Scalar125 = -mv1.Scalar1346 * mv2.Scalar23456 + mv1.Scalar2346 * mv2.Scalar13456 - mv1.Scalar3456 * mv2.Scalar12346,
            Scalar135 = mv1.Scalar1246 * mv2.Scalar23456 - mv1.Scalar2346 * mv2.Scalar12456 + mv1.Scalar2456 * mv2.Scalar12346,
            Scalar235 = -mv1.Scalar1246 * mv2.Scalar13456 + mv1.Scalar1346 * mv2.Scalar12456 - mv1.Scalar1456 * mv2.Scalar12346,
            Scalar145 = -mv1.Scalar1236 * mv2.Scalar23456 + mv1.Scalar2346 * mv2.Scalar12356 - mv1.Scalar2356 * mv2.Scalar12346,
            Scalar245 = mv1.Scalar1236 * mv2.Scalar13456 - mv1.Scalar1346 * mv2.Scalar12356 + mv1.Scalar1356 * mv2.Scalar12346,
            Scalar345 = -mv1.Scalar1236 * mv2.Scalar12456 + mv1.Scalar1246 * mv2.Scalar12356 - mv1.Scalar1256 * mv2.Scalar12346,
            Scalar126 = mv1.Scalar1345 * mv2.Scalar23456 - mv1.Scalar2345 * mv2.Scalar13456 + mv1.Scalar3456 * mv2.Scalar12345,
            Scalar136 = -mv1.Scalar1245 * mv2.Scalar23456 + mv1.Scalar2345 * mv2.Scalar12456 - mv1.Scalar2456 * mv2.Scalar12345,
            Scalar236 = mv1.Scalar1245 * mv2.Scalar13456 - mv1.Scalar1345 * mv2.Scalar12456 + mv1.Scalar1456 * mv2.Scalar12345,
            Scalar146 = mv1.Scalar1235 * mv2.Scalar23456 - mv1.Scalar2345 * mv2.Scalar12356 + mv1.Scalar2356 * mv2.Scalar12345,
            Scalar246 = -mv1.Scalar1235 * mv2.Scalar13456 + mv1.Scalar1345 * mv2.Scalar12356 - mv1.Scalar1356 * mv2.Scalar12345,
            Scalar346 = mv1.Scalar1235 * mv2.Scalar12456 - mv1.Scalar1245 * mv2.Scalar12356 + mv1.Scalar1256 * mv2.Scalar12345,
            Scalar156 = -mv1.Scalar1234 * mv2.Scalar23456 + mv1.Scalar2345 * mv2.Scalar12346 - mv1.Scalar2346 * mv2.Scalar12345,
            Scalar256 = mv1.Scalar1234 * mv2.Scalar13456 - mv1.Scalar1345 * mv2.Scalar12346 + mv1.Scalar1346 * mv2.Scalar12345,
            Scalar356 = -mv1.Scalar1234 * mv2.Scalar12456 + mv1.Scalar1245 * mv2.Scalar12346 - mv1.Scalar1246 * mv2.Scalar12345,
            Scalar456 = mv1.Scalar1234 * mv2.Scalar12356 - mv1.Scalar1235 * mv2.Scalar12346 + mv1.Scalar1236 * mv2.Scalar12345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector4 mv1, Ga6KVector6 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    public static Ga6Multivector Cp(this Ga6KVector4 mv1, Ga6Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[7] += mv1.Scalar1234 * mv2.KVector1.Scalar4 + mv1.Scalar1235 * mv2.KVector1.Scalar5 + mv1.Scalar1236 * mv2.KVector1.Scalar6;
            tempScalar[11] += -mv1.Scalar1234 * mv2.KVector1.Scalar3 + mv1.Scalar1245 * mv2.KVector1.Scalar5 + mv1.Scalar1246 * mv2.KVector1.Scalar6;
            tempScalar[13] += mv1.Scalar1234 * mv2.KVector1.Scalar2 + mv1.Scalar1345 * mv2.KVector1.Scalar5 + mv1.Scalar1346 * mv2.KVector1.Scalar6;
            tempScalar[14] += -mv1.Scalar1234 * mv2.KVector1.Scalar1 + mv1.Scalar2345 * mv2.KVector1.Scalar5 + mv1.Scalar2346 * mv2.KVector1.Scalar6;
            tempScalar[19] += -mv1.Scalar1235 * mv2.KVector1.Scalar3 - mv1.Scalar1245 * mv2.KVector1.Scalar4 + mv1.Scalar1256 * mv2.KVector1.Scalar6;
            tempScalar[21] += mv1.Scalar1235 * mv2.KVector1.Scalar2 - mv1.Scalar1345 * mv2.KVector1.Scalar4 + mv1.Scalar1356 * mv2.KVector1.Scalar6;
            tempScalar[22] += -mv1.Scalar1235 * mv2.KVector1.Scalar1 - mv1.Scalar2345 * mv2.KVector1.Scalar4 + mv1.Scalar2356 * mv2.KVector1.Scalar6;
            tempScalar[25] += mv1.Scalar1245 * mv2.KVector1.Scalar2 + mv1.Scalar1345 * mv2.KVector1.Scalar3 + mv1.Scalar1456 * mv2.KVector1.Scalar6;
            tempScalar[26] += -mv1.Scalar1245 * mv2.KVector1.Scalar1 + mv1.Scalar2345 * mv2.KVector1.Scalar3 + mv1.Scalar2456 * mv2.KVector1.Scalar6;
            tempScalar[28] += -mv1.Scalar1345 * mv2.KVector1.Scalar1 - mv1.Scalar2345 * mv2.KVector1.Scalar2 + mv1.Scalar3456 * mv2.KVector1.Scalar6;
            tempScalar[35] += -mv1.Scalar1236 * mv2.KVector1.Scalar3 - mv1.Scalar1246 * mv2.KVector1.Scalar4 - mv1.Scalar1256 * mv2.KVector1.Scalar5;
            tempScalar[37] += mv1.Scalar1236 * mv2.KVector1.Scalar2 - mv1.Scalar1346 * mv2.KVector1.Scalar4 - mv1.Scalar1356 * mv2.KVector1.Scalar5;
            tempScalar[38] += -mv1.Scalar1236 * mv2.KVector1.Scalar1 - mv1.Scalar2346 * mv2.KVector1.Scalar4 - mv1.Scalar2356 * mv2.KVector1.Scalar5;
            tempScalar[41] += mv1.Scalar1246 * mv2.KVector1.Scalar2 + mv1.Scalar1346 * mv2.KVector1.Scalar3 - mv1.Scalar1456 * mv2.KVector1.Scalar5;
            tempScalar[42] += -mv1.Scalar1246 * mv2.KVector1.Scalar1 + mv1.Scalar2346 * mv2.KVector1.Scalar3 - mv1.Scalar2456 * mv2.KVector1.Scalar5;
            tempScalar[44] += -mv1.Scalar1346 * mv2.KVector1.Scalar1 - mv1.Scalar2346 * mv2.KVector1.Scalar2 - mv1.Scalar3456 * mv2.KVector1.Scalar5;
            tempScalar[49] += mv1.Scalar1256 * mv2.KVector1.Scalar2 + mv1.Scalar1356 * mv2.KVector1.Scalar3 + mv1.Scalar1456 * mv2.KVector1.Scalar4;
            tempScalar[50] += -mv1.Scalar1256 * mv2.KVector1.Scalar1 + mv1.Scalar2356 * mv2.KVector1.Scalar3 + mv1.Scalar2456 * mv2.KVector1.Scalar4;
            tempScalar[52] += -mv1.Scalar1356 * mv2.KVector1.Scalar1 - mv1.Scalar2356 * mv2.KVector1.Scalar2 + mv1.Scalar3456 * mv2.KVector1.Scalar4;
            tempScalar[56] += -mv1.Scalar1456 * mv2.KVector1.Scalar1 - mv1.Scalar2456 * mv2.KVector1.Scalar2 - mv1.Scalar3456 * mv2.KVector1.Scalar3;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[15] += -mv1.Scalar1235 * mv2.KVector2.Scalar45 + mv1.Scalar1245 * mv2.KVector2.Scalar35 - mv1.Scalar1345 * mv2.KVector2.Scalar25 + mv1.Scalar2345 * mv2.KVector2.Scalar15 - mv1.Scalar1236 * mv2.KVector2.Scalar46 + mv1.Scalar1246 * mv2.KVector2.Scalar36 - mv1.Scalar1346 * mv2.KVector2.Scalar26 + mv1.Scalar2346 * mv2.KVector2.Scalar16;
            tempScalar[23] += mv1.Scalar1234 * mv2.KVector2.Scalar45 - mv1.Scalar1245 * mv2.KVector2.Scalar34 + mv1.Scalar1345 * mv2.KVector2.Scalar24 - mv1.Scalar2345 * mv2.KVector2.Scalar14 - mv1.Scalar1236 * mv2.KVector2.Scalar56 + mv1.Scalar1256 * mv2.KVector2.Scalar36 - mv1.Scalar1356 * mv2.KVector2.Scalar26 + mv1.Scalar2356 * mv2.KVector2.Scalar16;
            tempScalar[27] += -mv1.Scalar1234 * mv2.KVector2.Scalar35 + mv1.Scalar1235 * mv2.KVector2.Scalar34 - mv1.Scalar1345 * mv2.KVector2.Scalar23 + mv1.Scalar2345 * mv2.KVector2.Scalar13 - mv1.Scalar1246 * mv2.KVector2.Scalar56 + mv1.Scalar1256 * mv2.KVector2.Scalar46 - mv1.Scalar1456 * mv2.KVector2.Scalar26 + mv1.Scalar2456 * mv2.KVector2.Scalar16;
            tempScalar[29] += mv1.Scalar1234 * mv2.KVector2.Scalar25 - mv1.Scalar1235 * mv2.KVector2.Scalar24 + mv1.Scalar1245 * mv2.KVector2.Scalar23 - mv1.Scalar2345 * mv2.KVector2.Scalar12 - mv1.Scalar1346 * mv2.KVector2.Scalar56 + mv1.Scalar1356 * mv2.KVector2.Scalar46 - mv1.Scalar1456 * mv2.KVector2.Scalar36 + mv1.Scalar3456 * mv2.KVector2.Scalar16;
            tempScalar[30] += -mv1.Scalar1234 * mv2.KVector2.Scalar15 + mv1.Scalar1235 * mv2.KVector2.Scalar14 - mv1.Scalar1245 * mv2.KVector2.Scalar13 + mv1.Scalar1345 * mv2.KVector2.Scalar12 - mv1.Scalar2346 * mv2.KVector2.Scalar56 + mv1.Scalar2356 * mv2.KVector2.Scalar46 - mv1.Scalar2456 * mv2.KVector2.Scalar36 + mv1.Scalar3456 * mv2.KVector2.Scalar26;
            tempScalar[39] += mv1.Scalar1234 * mv2.KVector2.Scalar46 + mv1.Scalar1235 * mv2.KVector2.Scalar56 - mv1.Scalar1246 * mv2.KVector2.Scalar34 + mv1.Scalar1346 * mv2.KVector2.Scalar24 - mv1.Scalar2346 * mv2.KVector2.Scalar14 - mv1.Scalar1256 * mv2.KVector2.Scalar35 + mv1.Scalar1356 * mv2.KVector2.Scalar25 - mv1.Scalar2356 * mv2.KVector2.Scalar15;
            tempScalar[43] += -mv1.Scalar1234 * mv2.KVector2.Scalar36 + mv1.Scalar1245 * mv2.KVector2.Scalar56 + mv1.Scalar1236 * mv2.KVector2.Scalar34 - mv1.Scalar1346 * mv2.KVector2.Scalar23 + mv1.Scalar2346 * mv2.KVector2.Scalar13 - mv1.Scalar1256 * mv2.KVector2.Scalar45 + mv1.Scalar1456 * mv2.KVector2.Scalar25 - mv1.Scalar2456 * mv2.KVector2.Scalar15;
            tempScalar[45] += mv1.Scalar1234 * mv2.KVector2.Scalar26 + mv1.Scalar1345 * mv2.KVector2.Scalar56 - mv1.Scalar1236 * mv2.KVector2.Scalar24 + mv1.Scalar1246 * mv2.KVector2.Scalar23 - mv1.Scalar2346 * mv2.KVector2.Scalar12 - mv1.Scalar1356 * mv2.KVector2.Scalar45 + mv1.Scalar1456 * mv2.KVector2.Scalar35 - mv1.Scalar3456 * mv2.KVector2.Scalar15;
            tempScalar[46] += -mv1.Scalar1234 * mv2.KVector2.Scalar16 + mv1.Scalar2345 * mv2.KVector2.Scalar56 + mv1.Scalar1236 * mv2.KVector2.Scalar14 - mv1.Scalar1246 * mv2.KVector2.Scalar13 + mv1.Scalar1346 * mv2.KVector2.Scalar12 - mv1.Scalar2356 * mv2.KVector2.Scalar45 + mv1.Scalar2456 * mv2.KVector2.Scalar35 - mv1.Scalar3456 * mv2.KVector2.Scalar25;
            tempScalar[51] += -mv1.Scalar1235 * mv2.KVector2.Scalar36 - mv1.Scalar1245 * mv2.KVector2.Scalar46 + mv1.Scalar1236 * mv2.KVector2.Scalar35 + mv1.Scalar1246 * mv2.KVector2.Scalar45 - mv1.Scalar1356 * mv2.KVector2.Scalar23 + mv1.Scalar2356 * mv2.KVector2.Scalar13 - mv1.Scalar1456 * mv2.KVector2.Scalar24 + mv1.Scalar2456 * mv2.KVector2.Scalar14;
            tempScalar[53] += mv1.Scalar1235 * mv2.KVector2.Scalar26 - mv1.Scalar1345 * mv2.KVector2.Scalar46 - mv1.Scalar1236 * mv2.KVector2.Scalar25 + mv1.Scalar1346 * mv2.KVector2.Scalar45 + mv1.Scalar1256 * mv2.KVector2.Scalar23 - mv1.Scalar2356 * mv2.KVector2.Scalar12 - mv1.Scalar1456 * mv2.KVector2.Scalar34 + mv1.Scalar3456 * mv2.KVector2.Scalar14;
            tempScalar[54] += -mv1.Scalar1235 * mv2.KVector2.Scalar16 - mv1.Scalar2345 * mv2.KVector2.Scalar46 + mv1.Scalar1236 * mv2.KVector2.Scalar15 + mv1.Scalar2346 * mv2.KVector2.Scalar45 - mv1.Scalar1256 * mv2.KVector2.Scalar13 + mv1.Scalar1356 * mv2.KVector2.Scalar12 - mv1.Scalar2456 * mv2.KVector2.Scalar34 + mv1.Scalar3456 * mv2.KVector2.Scalar24;
            tempScalar[57] += mv1.Scalar1245 * mv2.KVector2.Scalar26 + mv1.Scalar1345 * mv2.KVector2.Scalar36 - mv1.Scalar1246 * mv2.KVector2.Scalar25 - mv1.Scalar1346 * mv2.KVector2.Scalar35 + mv1.Scalar1256 * mv2.KVector2.Scalar24 + mv1.Scalar1356 * mv2.KVector2.Scalar34 - mv1.Scalar2456 * mv2.KVector2.Scalar12 - mv1.Scalar3456 * mv2.KVector2.Scalar13;
            tempScalar[58] += -mv1.Scalar1245 * mv2.KVector2.Scalar16 + mv1.Scalar2345 * mv2.KVector2.Scalar36 + mv1.Scalar1246 * mv2.KVector2.Scalar15 - mv1.Scalar2346 * mv2.KVector2.Scalar35 - mv1.Scalar1256 * mv2.KVector2.Scalar14 + mv1.Scalar2356 * mv2.KVector2.Scalar34 + mv1.Scalar1456 * mv2.KVector2.Scalar12 - mv1.Scalar3456 * mv2.KVector2.Scalar23;
            tempScalar[60] += -mv1.Scalar1345 * mv2.KVector2.Scalar16 - mv1.Scalar2345 * mv2.KVector2.Scalar26 + mv1.Scalar1346 * mv2.KVector2.Scalar15 + mv1.Scalar2346 * mv2.KVector2.Scalar25 - mv1.Scalar1356 * mv2.KVector2.Scalar14 - mv1.Scalar2356 * mv2.KVector2.Scalar24 + mv1.Scalar1456 * mv2.KVector2.Scalar13 + mv1.Scalar2456 * mv2.KVector2.Scalar23;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[1] += -mv1.Scalar1234 * mv2.KVector3.Scalar234 - mv1.Scalar1235 * mv2.KVector3.Scalar235 - mv1.Scalar1245 * mv2.KVector3.Scalar245 - mv1.Scalar1345 * mv2.KVector3.Scalar345 - mv1.Scalar1236 * mv2.KVector3.Scalar236 - mv1.Scalar1246 * mv2.KVector3.Scalar246 - mv1.Scalar1346 * mv2.KVector3.Scalar346 - mv1.Scalar1256 * mv2.KVector3.Scalar256 - mv1.Scalar1356 * mv2.KVector3.Scalar356 - mv1.Scalar1456 * mv2.KVector3.Scalar456;
            tempScalar[2] += mv1.Scalar1234 * mv2.KVector3.Scalar134 + mv1.Scalar1235 * mv2.KVector3.Scalar135 + mv1.Scalar1245 * mv2.KVector3.Scalar145 - mv1.Scalar2345 * mv2.KVector3.Scalar345 + mv1.Scalar1236 * mv2.KVector3.Scalar136 + mv1.Scalar1246 * mv2.KVector3.Scalar146 - mv1.Scalar2346 * mv2.KVector3.Scalar346 + mv1.Scalar1256 * mv2.KVector3.Scalar156 - mv1.Scalar2356 * mv2.KVector3.Scalar356 - mv1.Scalar2456 * mv2.KVector3.Scalar456;
            tempScalar[4] += -mv1.Scalar1234 * mv2.KVector3.Scalar124 - mv1.Scalar1235 * mv2.KVector3.Scalar125 + mv1.Scalar1345 * mv2.KVector3.Scalar145 + mv1.Scalar2345 * mv2.KVector3.Scalar245 - mv1.Scalar1236 * mv2.KVector3.Scalar126 + mv1.Scalar1346 * mv2.KVector3.Scalar146 + mv1.Scalar2346 * mv2.KVector3.Scalar246 + mv1.Scalar1356 * mv2.KVector3.Scalar156 + mv1.Scalar2356 * mv2.KVector3.Scalar256 - mv1.Scalar3456 * mv2.KVector3.Scalar456;
            tempScalar[8] += mv1.Scalar1234 * mv2.KVector3.Scalar123 - mv1.Scalar1245 * mv2.KVector3.Scalar125 - mv1.Scalar1345 * mv2.KVector3.Scalar135 - mv1.Scalar2345 * mv2.KVector3.Scalar235 - mv1.Scalar1246 * mv2.KVector3.Scalar126 - mv1.Scalar1346 * mv2.KVector3.Scalar136 - mv1.Scalar2346 * mv2.KVector3.Scalar236 + mv1.Scalar1456 * mv2.KVector3.Scalar156 + mv1.Scalar2456 * mv2.KVector3.Scalar256 + mv1.Scalar3456 * mv2.KVector3.Scalar356;
            tempScalar[16] += mv1.Scalar1235 * mv2.KVector3.Scalar123 + mv1.Scalar1245 * mv2.KVector3.Scalar124 + mv1.Scalar1345 * mv2.KVector3.Scalar134 + mv1.Scalar2345 * mv2.KVector3.Scalar234 - mv1.Scalar1256 * mv2.KVector3.Scalar126 - mv1.Scalar1356 * mv2.KVector3.Scalar136 - mv1.Scalar2356 * mv2.KVector3.Scalar236 - mv1.Scalar1456 * mv2.KVector3.Scalar146 - mv1.Scalar2456 * mv2.KVector3.Scalar246 - mv1.Scalar3456 * mv2.KVector3.Scalar346;
            tempScalar[31] += mv1.Scalar1236 * mv2.KVector3.Scalar456 - mv1.Scalar1246 * mv2.KVector3.Scalar356 + mv1.Scalar1346 * mv2.KVector3.Scalar256 - mv1.Scalar2346 * mv2.KVector3.Scalar156 + mv1.Scalar1256 * mv2.KVector3.Scalar346 - mv1.Scalar1356 * mv2.KVector3.Scalar246 + mv1.Scalar2356 * mv2.KVector3.Scalar146 + mv1.Scalar1456 * mv2.KVector3.Scalar236 - mv1.Scalar2456 * mv2.KVector3.Scalar136 + mv1.Scalar3456 * mv2.KVector3.Scalar126;
            tempScalar[32] += mv1.Scalar1236 * mv2.KVector3.Scalar123 + mv1.Scalar1246 * mv2.KVector3.Scalar124 + mv1.Scalar1346 * mv2.KVector3.Scalar134 + mv1.Scalar2346 * mv2.KVector3.Scalar234 + mv1.Scalar1256 * mv2.KVector3.Scalar125 + mv1.Scalar1356 * mv2.KVector3.Scalar135 + mv1.Scalar2356 * mv2.KVector3.Scalar235 + mv1.Scalar1456 * mv2.KVector3.Scalar145 + mv1.Scalar2456 * mv2.KVector3.Scalar245 + mv1.Scalar3456 * mv2.KVector3.Scalar345;
            tempScalar[47] += -mv1.Scalar1235 * mv2.KVector3.Scalar456 + mv1.Scalar1245 * mv2.KVector3.Scalar356 - mv1.Scalar1345 * mv2.KVector3.Scalar256 + mv1.Scalar2345 * mv2.KVector3.Scalar156 - mv1.Scalar1256 * mv2.KVector3.Scalar345 + mv1.Scalar1356 * mv2.KVector3.Scalar245 - mv1.Scalar2356 * mv2.KVector3.Scalar145 - mv1.Scalar1456 * mv2.KVector3.Scalar235 + mv1.Scalar2456 * mv2.KVector3.Scalar135 - mv1.Scalar3456 * mv2.KVector3.Scalar125;
            tempScalar[55] += mv1.Scalar1234 * mv2.KVector3.Scalar456 - mv1.Scalar1245 * mv2.KVector3.Scalar346 + mv1.Scalar1345 * mv2.KVector3.Scalar246 - mv1.Scalar2345 * mv2.KVector3.Scalar146 + mv1.Scalar1246 * mv2.KVector3.Scalar345 - mv1.Scalar1346 * mv2.KVector3.Scalar245 + mv1.Scalar2346 * mv2.KVector3.Scalar145 + mv1.Scalar1456 * mv2.KVector3.Scalar234 - mv1.Scalar2456 * mv2.KVector3.Scalar134 + mv1.Scalar3456 * mv2.KVector3.Scalar124;
            tempScalar[59] += -mv1.Scalar1234 * mv2.KVector3.Scalar356 + mv1.Scalar1235 * mv2.KVector3.Scalar346 - mv1.Scalar1345 * mv2.KVector3.Scalar236 + mv1.Scalar2345 * mv2.KVector3.Scalar136 - mv1.Scalar1236 * mv2.KVector3.Scalar345 + mv1.Scalar1346 * mv2.KVector3.Scalar235 - mv1.Scalar2346 * mv2.KVector3.Scalar135 - mv1.Scalar1356 * mv2.KVector3.Scalar234 + mv1.Scalar2356 * mv2.KVector3.Scalar134 - mv1.Scalar3456 * mv2.KVector3.Scalar123;
            tempScalar[61] += mv1.Scalar1234 * mv2.KVector3.Scalar256 - mv1.Scalar1235 * mv2.KVector3.Scalar246 + mv1.Scalar1245 * mv2.KVector3.Scalar236 - mv1.Scalar2345 * mv2.KVector3.Scalar126 + mv1.Scalar1236 * mv2.KVector3.Scalar245 - mv1.Scalar1246 * mv2.KVector3.Scalar235 + mv1.Scalar2346 * mv2.KVector3.Scalar125 + mv1.Scalar1256 * mv2.KVector3.Scalar234 - mv1.Scalar2356 * mv2.KVector3.Scalar124 + mv1.Scalar2456 * mv2.KVector3.Scalar123;
            tempScalar[62] += -mv1.Scalar1234 * mv2.KVector3.Scalar156 + mv1.Scalar1235 * mv2.KVector3.Scalar146 - mv1.Scalar1245 * mv2.KVector3.Scalar136 + mv1.Scalar1345 * mv2.KVector3.Scalar126 - mv1.Scalar1236 * mv2.KVector3.Scalar145 + mv1.Scalar1246 * mv2.KVector3.Scalar135 - mv1.Scalar1346 * mv2.KVector3.Scalar125 - mv1.Scalar1256 * mv2.KVector3.Scalar134 + mv1.Scalar1356 * mv2.KVector3.Scalar124 - mv1.Scalar1456 * mv2.KVector3.Scalar123;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[3] += mv1.Scalar1345 * mv2.KVector4.Scalar2345 - mv1.Scalar2345 * mv2.KVector4.Scalar1345 + mv1.Scalar1346 * mv2.KVector4.Scalar2346 - mv1.Scalar2346 * mv2.KVector4.Scalar1346 + mv1.Scalar1356 * mv2.KVector4.Scalar2356 - mv1.Scalar2356 * mv2.KVector4.Scalar1356 + mv1.Scalar1456 * mv2.KVector4.Scalar2456 - mv1.Scalar2456 * mv2.KVector4.Scalar1456;
            tempScalar[5] += -mv1.Scalar1245 * mv2.KVector4.Scalar2345 + mv1.Scalar2345 * mv2.KVector4.Scalar1245 - mv1.Scalar1246 * mv2.KVector4.Scalar2346 + mv1.Scalar2346 * mv2.KVector4.Scalar1246 - mv1.Scalar1256 * mv2.KVector4.Scalar2356 + mv1.Scalar2356 * mv2.KVector4.Scalar1256 + mv1.Scalar1456 * mv2.KVector4.Scalar3456 - mv1.Scalar3456 * mv2.KVector4.Scalar1456;
            tempScalar[6] += mv1.Scalar1245 * mv2.KVector4.Scalar1345 - mv1.Scalar1345 * mv2.KVector4.Scalar1245 + mv1.Scalar1246 * mv2.KVector4.Scalar1346 - mv1.Scalar1346 * mv2.KVector4.Scalar1246 + mv1.Scalar1256 * mv2.KVector4.Scalar1356 - mv1.Scalar1356 * mv2.KVector4.Scalar1256 + mv1.Scalar2456 * mv2.KVector4.Scalar3456 - mv1.Scalar3456 * mv2.KVector4.Scalar2456;
            tempScalar[9] += mv1.Scalar1235 * mv2.KVector4.Scalar2345 - mv1.Scalar2345 * mv2.KVector4.Scalar1235 + mv1.Scalar1236 * mv2.KVector4.Scalar2346 - mv1.Scalar2346 * mv2.KVector4.Scalar1236 - mv1.Scalar1256 * mv2.KVector4.Scalar2456 - mv1.Scalar1356 * mv2.KVector4.Scalar3456 + mv1.Scalar2456 * mv2.KVector4.Scalar1256 + mv1.Scalar3456 * mv2.KVector4.Scalar1356;
            tempScalar[10] += -mv1.Scalar1235 * mv2.KVector4.Scalar1345 + mv1.Scalar1345 * mv2.KVector4.Scalar1235 - mv1.Scalar1236 * mv2.KVector4.Scalar1346 + mv1.Scalar1346 * mv2.KVector4.Scalar1236 + mv1.Scalar1256 * mv2.KVector4.Scalar1456 - mv1.Scalar2356 * mv2.KVector4.Scalar3456 - mv1.Scalar1456 * mv2.KVector4.Scalar1256 + mv1.Scalar3456 * mv2.KVector4.Scalar2356;
            tempScalar[12] += mv1.Scalar1235 * mv2.KVector4.Scalar1245 - mv1.Scalar1245 * mv2.KVector4.Scalar1235 + mv1.Scalar1236 * mv2.KVector4.Scalar1246 - mv1.Scalar1246 * mv2.KVector4.Scalar1236 + mv1.Scalar1356 * mv2.KVector4.Scalar1456 + mv1.Scalar2356 * mv2.KVector4.Scalar2456 - mv1.Scalar1456 * mv2.KVector4.Scalar1356 - mv1.Scalar2456 * mv2.KVector4.Scalar2356;
            tempScalar[17] += -mv1.Scalar1234 * mv2.KVector4.Scalar2345 + mv1.Scalar2345 * mv2.KVector4.Scalar1234 + mv1.Scalar1236 * mv2.KVector4.Scalar2356 + mv1.Scalar1246 * mv2.KVector4.Scalar2456 + mv1.Scalar1346 * mv2.KVector4.Scalar3456 - mv1.Scalar2356 * mv2.KVector4.Scalar1236 - mv1.Scalar2456 * mv2.KVector4.Scalar1246 - mv1.Scalar3456 * mv2.KVector4.Scalar1346;
            tempScalar[18] += mv1.Scalar1234 * mv2.KVector4.Scalar1345 - mv1.Scalar1345 * mv2.KVector4.Scalar1234 - mv1.Scalar1236 * mv2.KVector4.Scalar1356 - mv1.Scalar1246 * mv2.KVector4.Scalar1456 + mv1.Scalar2346 * mv2.KVector4.Scalar3456 + mv1.Scalar1356 * mv2.KVector4.Scalar1236 + mv1.Scalar1456 * mv2.KVector4.Scalar1246 - mv1.Scalar3456 * mv2.KVector4.Scalar2346;
            tempScalar[20] += -mv1.Scalar1234 * mv2.KVector4.Scalar1245 + mv1.Scalar1245 * mv2.KVector4.Scalar1234 + mv1.Scalar1236 * mv2.KVector4.Scalar1256 - mv1.Scalar1346 * mv2.KVector4.Scalar1456 - mv1.Scalar2346 * mv2.KVector4.Scalar2456 - mv1.Scalar1256 * mv2.KVector4.Scalar1236 + mv1.Scalar1456 * mv2.KVector4.Scalar1346 + mv1.Scalar2456 * mv2.KVector4.Scalar2346;
            tempScalar[24] += mv1.Scalar1234 * mv2.KVector4.Scalar1235 - mv1.Scalar1235 * mv2.KVector4.Scalar1234 + mv1.Scalar1246 * mv2.KVector4.Scalar1256 + mv1.Scalar1346 * mv2.KVector4.Scalar1356 + mv1.Scalar2346 * mv2.KVector4.Scalar2356 - mv1.Scalar1256 * mv2.KVector4.Scalar1246 - mv1.Scalar1356 * mv2.KVector4.Scalar1346 - mv1.Scalar2356 * mv2.KVector4.Scalar2346;
            tempScalar[33] += -mv1.Scalar1234 * mv2.KVector4.Scalar2346 - mv1.Scalar1235 * mv2.KVector4.Scalar2356 - mv1.Scalar1245 * mv2.KVector4.Scalar2456 - mv1.Scalar1345 * mv2.KVector4.Scalar3456 + mv1.Scalar2346 * mv2.KVector4.Scalar1234 + mv1.Scalar2356 * mv2.KVector4.Scalar1235 + mv1.Scalar2456 * mv2.KVector4.Scalar1245 + mv1.Scalar3456 * mv2.KVector4.Scalar1345;
            tempScalar[34] += mv1.Scalar1234 * mv2.KVector4.Scalar1346 + mv1.Scalar1235 * mv2.KVector4.Scalar1356 + mv1.Scalar1245 * mv2.KVector4.Scalar1456 - mv1.Scalar2345 * mv2.KVector4.Scalar3456 - mv1.Scalar1346 * mv2.KVector4.Scalar1234 - mv1.Scalar1356 * mv2.KVector4.Scalar1235 - mv1.Scalar1456 * mv2.KVector4.Scalar1245 + mv1.Scalar3456 * mv2.KVector4.Scalar2345;
            tempScalar[36] += -mv1.Scalar1234 * mv2.KVector4.Scalar1246 - mv1.Scalar1235 * mv2.KVector4.Scalar1256 + mv1.Scalar1345 * mv2.KVector4.Scalar1456 + mv1.Scalar2345 * mv2.KVector4.Scalar2456 + mv1.Scalar1246 * mv2.KVector4.Scalar1234 + mv1.Scalar1256 * mv2.KVector4.Scalar1235 - mv1.Scalar1456 * mv2.KVector4.Scalar1345 - mv1.Scalar2456 * mv2.KVector4.Scalar2345;
            tempScalar[40] += mv1.Scalar1234 * mv2.KVector4.Scalar1236 - mv1.Scalar1245 * mv2.KVector4.Scalar1256 - mv1.Scalar1345 * mv2.KVector4.Scalar1356 - mv1.Scalar2345 * mv2.KVector4.Scalar2356 - mv1.Scalar1236 * mv2.KVector4.Scalar1234 + mv1.Scalar1256 * mv2.KVector4.Scalar1245 + mv1.Scalar1356 * mv2.KVector4.Scalar1345 + mv1.Scalar2356 * mv2.KVector4.Scalar2345;
            tempScalar[48] += mv1.Scalar1235 * mv2.KVector4.Scalar1236 + mv1.Scalar1245 * mv2.KVector4.Scalar1246 + mv1.Scalar1345 * mv2.KVector4.Scalar1346 + mv1.Scalar2345 * mv2.KVector4.Scalar2346 - mv1.Scalar1236 * mv2.KVector4.Scalar1235 - mv1.Scalar1246 * mv2.KVector4.Scalar1245 - mv1.Scalar1346 * mv2.KVector4.Scalar1345 - mv1.Scalar2346 * mv2.KVector4.Scalar2345;
        }
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[7] += -mv1.Scalar1456 * mv2.KVector5.Scalar23456 + mv1.Scalar2456 * mv2.KVector5.Scalar13456 - mv1.Scalar3456 * mv2.KVector5.Scalar12456;
            tempScalar[11] += mv1.Scalar1356 * mv2.KVector5.Scalar23456 - mv1.Scalar2356 * mv2.KVector5.Scalar13456 + mv1.Scalar3456 * mv2.KVector5.Scalar12356;
            tempScalar[13] += -mv1.Scalar1256 * mv2.KVector5.Scalar23456 + mv1.Scalar2356 * mv2.KVector5.Scalar12456 - mv1.Scalar2456 * mv2.KVector5.Scalar12356;
            tempScalar[14] += mv1.Scalar1256 * mv2.KVector5.Scalar13456 - mv1.Scalar1356 * mv2.KVector5.Scalar12456 + mv1.Scalar1456 * mv2.KVector5.Scalar12356;
            tempScalar[19] += -mv1.Scalar1346 * mv2.KVector5.Scalar23456 + mv1.Scalar2346 * mv2.KVector5.Scalar13456 - mv1.Scalar3456 * mv2.KVector5.Scalar12346;
            tempScalar[21] += mv1.Scalar1246 * mv2.KVector5.Scalar23456 - mv1.Scalar2346 * mv2.KVector5.Scalar12456 + mv1.Scalar2456 * mv2.KVector5.Scalar12346;
            tempScalar[22] += -mv1.Scalar1246 * mv2.KVector5.Scalar13456 + mv1.Scalar1346 * mv2.KVector5.Scalar12456 - mv1.Scalar1456 * mv2.KVector5.Scalar12346;
            tempScalar[25] += -mv1.Scalar1236 * mv2.KVector5.Scalar23456 + mv1.Scalar2346 * mv2.KVector5.Scalar12356 - mv1.Scalar2356 * mv2.KVector5.Scalar12346;
            tempScalar[26] += mv1.Scalar1236 * mv2.KVector5.Scalar13456 - mv1.Scalar1346 * mv2.KVector5.Scalar12356 + mv1.Scalar1356 * mv2.KVector5.Scalar12346;
            tempScalar[28] += -mv1.Scalar1236 * mv2.KVector5.Scalar12456 + mv1.Scalar1246 * mv2.KVector5.Scalar12356 - mv1.Scalar1256 * mv2.KVector5.Scalar12346;
            tempScalar[35] += mv1.Scalar1345 * mv2.KVector5.Scalar23456 - mv1.Scalar2345 * mv2.KVector5.Scalar13456 + mv1.Scalar3456 * mv2.KVector5.Scalar12345;
            tempScalar[37] += -mv1.Scalar1245 * mv2.KVector5.Scalar23456 + mv1.Scalar2345 * mv2.KVector5.Scalar12456 - mv1.Scalar2456 * mv2.KVector5.Scalar12345;
            tempScalar[38] += mv1.Scalar1245 * mv2.KVector5.Scalar13456 - mv1.Scalar1345 * mv2.KVector5.Scalar12456 + mv1.Scalar1456 * mv2.KVector5.Scalar12345;
            tempScalar[41] += mv1.Scalar1235 * mv2.KVector5.Scalar23456 - mv1.Scalar2345 * mv2.KVector5.Scalar12356 + mv1.Scalar2356 * mv2.KVector5.Scalar12345;
            tempScalar[42] += -mv1.Scalar1235 * mv2.KVector5.Scalar13456 + mv1.Scalar1345 * mv2.KVector5.Scalar12356 - mv1.Scalar1356 * mv2.KVector5.Scalar12345;
            tempScalar[44] += mv1.Scalar1235 * mv2.KVector5.Scalar12456 - mv1.Scalar1245 * mv2.KVector5.Scalar12356 + mv1.Scalar1256 * mv2.KVector5.Scalar12345;
            tempScalar[49] += -mv1.Scalar1234 * mv2.KVector5.Scalar23456 + mv1.Scalar2345 * mv2.KVector5.Scalar12346 - mv1.Scalar2346 * mv2.KVector5.Scalar12345;
            tempScalar[50] += mv1.Scalar1234 * mv2.KVector5.Scalar13456 - mv1.Scalar1345 * mv2.KVector5.Scalar12346 + mv1.Scalar1346 * mv2.KVector5.Scalar12345;
            tempScalar[52] += -mv1.Scalar1234 * mv2.KVector5.Scalar12456 + mv1.Scalar1245 * mv2.KVector5.Scalar12346 - mv1.Scalar1246 * mv2.KVector5.Scalar12345;
            tempScalar[56] += mv1.Scalar1234 * mv2.KVector5.Scalar12356 - mv1.Scalar1235 * mv2.KVector5.Scalar12346 + mv1.Scalar1236 * mv2.KVector5.Scalar12345;
        }
        
        return Ga6Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector5 mv1, Ga6KVector0 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector6 Cp(this Ga6KVector5 mv1, Ga6KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector6.Zero;
        
        return new Ga6KVector6
        {
            Scalar123456 = mv1.Scalar12345 * mv2.Scalar6 - mv1.Scalar12346 * mv2.Scalar5 + mv1.Scalar12356 * mv2.Scalar4 - mv1.Scalar12456 * mv2.Scalar3 + mv1.Scalar13456 * mv2.Scalar2 - mv1.Scalar23456 * mv2.Scalar1
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector5 Cp(this Ga6KVector5 mv1, Ga6KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector5.Zero;
        
        return new Ga6KVector5
        {
            Scalar12345 = -mv1.Scalar12346 * mv2.Scalar56 + mv1.Scalar12356 * mv2.Scalar46 - mv1.Scalar12456 * mv2.Scalar36 + mv1.Scalar13456 * mv2.Scalar26 - mv1.Scalar23456 * mv2.Scalar16,
            Scalar12346 = mv1.Scalar12345 * mv2.Scalar56 - mv1.Scalar12356 * mv2.Scalar45 + mv1.Scalar12456 * mv2.Scalar35 - mv1.Scalar13456 * mv2.Scalar25 + mv1.Scalar23456 * mv2.Scalar15,
            Scalar12356 = -mv1.Scalar12345 * mv2.Scalar46 + mv1.Scalar12346 * mv2.Scalar45 - mv1.Scalar12456 * mv2.Scalar34 + mv1.Scalar13456 * mv2.Scalar24 - mv1.Scalar23456 * mv2.Scalar14,
            Scalar12456 = mv1.Scalar12345 * mv2.Scalar36 - mv1.Scalar12346 * mv2.Scalar35 + mv1.Scalar12356 * mv2.Scalar34 - mv1.Scalar13456 * mv2.Scalar23 + mv1.Scalar23456 * mv2.Scalar13,
            Scalar13456 = -mv1.Scalar12345 * mv2.Scalar26 + mv1.Scalar12346 * mv2.Scalar25 - mv1.Scalar12356 * mv2.Scalar24 + mv1.Scalar12456 * mv2.Scalar23 - mv1.Scalar23456 * mv2.Scalar12,
            Scalar23456 = mv1.Scalar12345 * mv2.Scalar16 - mv1.Scalar12346 * mv2.Scalar15 + mv1.Scalar12356 * mv2.Scalar14 - mv1.Scalar12456 * mv2.Scalar13 + mv1.Scalar13456 * mv2.Scalar12
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector4 Cp(this Ga6KVector5 mv1, Ga6KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector4.Zero;
        
        return new Ga6KVector4
        {
            Scalar1234 = -mv1.Scalar12356 * mv2.Scalar456 + mv1.Scalar12456 * mv2.Scalar356 - mv1.Scalar13456 * mv2.Scalar256 + mv1.Scalar23456 * mv2.Scalar156,
            Scalar1235 = mv1.Scalar12346 * mv2.Scalar456 - mv1.Scalar12456 * mv2.Scalar346 + mv1.Scalar13456 * mv2.Scalar246 - mv1.Scalar23456 * mv2.Scalar146,
            Scalar1245 = -mv1.Scalar12346 * mv2.Scalar356 + mv1.Scalar12356 * mv2.Scalar346 - mv1.Scalar13456 * mv2.Scalar236 + mv1.Scalar23456 * mv2.Scalar136,
            Scalar1345 = mv1.Scalar12346 * mv2.Scalar256 - mv1.Scalar12356 * mv2.Scalar246 + mv1.Scalar12456 * mv2.Scalar236 - mv1.Scalar23456 * mv2.Scalar126,
            Scalar2345 = -mv1.Scalar12346 * mv2.Scalar156 + mv1.Scalar12356 * mv2.Scalar146 - mv1.Scalar12456 * mv2.Scalar136 + mv1.Scalar13456 * mv2.Scalar126,
            Scalar1236 = -mv1.Scalar12345 * mv2.Scalar456 + mv1.Scalar12456 * mv2.Scalar345 - mv1.Scalar13456 * mv2.Scalar245 + mv1.Scalar23456 * mv2.Scalar145,
            Scalar1246 = mv1.Scalar12345 * mv2.Scalar356 - mv1.Scalar12356 * mv2.Scalar345 + mv1.Scalar13456 * mv2.Scalar235 - mv1.Scalar23456 * mv2.Scalar135,
            Scalar1346 = -mv1.Scalar12345 * mv2.Scalar256 + mv1.Scalar12356 * mv2.Scalar245 - mv1.Scalar12456 * mv2.Scalar235 + mv1.Scalar23456 * mv2.Scalar125,
            Scalar2346 = mv1.Scalar12345 * mv2.Scalar156 - mv1.Scalar12356 * mv2.Scalar145 + mv1.Scalar12456 * mv2.Scalar135 - mv1.Scalar13456 * mv2.Scalar125,
            Scalar1256 = -mv1.Scalar12345 * mv2.Scalar346 + mv1.Scalar12346 * mv2.Scalar345 - mv1.Scalar13456 * mv2.Scalar234 + mv1.Scalar23456 * mv2.Scalar134,
            Scalar1356 = mv1.Scalar12345 * mv2.Scalar246 - mv1.Scalar12346 * mv2.Scalar245 + mv1.Scalar12456 * mv2.Scalar234 - mv1.Scalar23456 * mv2.Scalar124,
            Scalar2356 = -mv1.Scalar12345 * mv2.Scalar146 + mv1.Scalar12346 * mv2.Scalar145 - mv1.Scalar12456 * mv2.Scalar134 + mv1.Scalar13456 * mv2.Scalar124,
            Scalar1456 = -mv1.Scalar12345 * mv2.Scalar236 + mv1.Scalar12346 * mv2.Scalar235 - mv1.Scalar12356 * mv2.Scalar234 + mv1.Scalar23456 * mv2.Scalar123,
            Scalar2456 = mv1.Scalar12345 * mv2.Scalar136 - mv1.Scalar12346 * mv2.Scalar135 + mv1.Scalar12356 * mv2.Scalar134 - mv1.Scalar13456 * mv2.Scalar123,
            Scalar3456 = -mv1.Scalar12345 * mv2.Scalar126 + mv1.Scalar12346 * mv2.Scalar125 - mv1.Scalar12356 * mv2.Scalar124 + mv1.Scalar12456 * mv2.Scalar123
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector3 Cp(this Ga6KVector5 mv1, Ga6KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector3.Zero;
        
        return new Ga6KVector3
        {
            Scalar123 = mv1.Scalar12456 * mv2.Scalar3456 - mv1.Scalar13456 * mv2.Scalar2456 + mv1.Scalar23456 * mv2.Scalar1456,
            Scalar124 = -mv1.Scalar12356 * mv2.Scalar3456 + mv1.Scalar13456 * mv2.Scalar2356 - mv1.Scalar23456 * mv2.Scalar1356,
            Scalar134 = mv1.Scalar12356 * mv2.Scalar2456 - mv1.Scalar12456 * mv2.Scalar2356 + mv1.Scalar23456 * mv2.Scalar1256,
            Scalar234 = -mv1.Scalar12356 * mv2.Scalar1456 + mv1.Scalar12456 * mv2.Scalar1356 - mv1.Scalar13456 * mv2.Scalar1256,
            Scalar125 = mv1.Scalar12346 * mv2.Scalar3456 - mv1.Scalar13456 * mv2.Scalar2346 + mv1.Scalar23456 * mv2.Scalar1346,
            Scalar135 = -mv1.Scalar12346 * mv2.Scalar2456 + mv1.Scalar12456 * mv2.Scalar2346 - mv1.Scalar23456 * mv2.Scalar1246,
            Scalar235 = mv1.Scalar12346 * mv2.Scalar1456 - mv1.Scalar12456 * mv2.Scalar1346 + mv1.Scalar13456 * mv2.Scalar1246,
            Scalar145 = mv1.Scalar12346 * mv2.Scalar2356 - mv1.Scalar12356 * mv2.Scalar2346 + mv1.Scalar23456 * mv2.Scalar1236,
            Scalar245 = -mv1.Scalar12346 * mv2.Scalar1356 + mv1.Scalar12356 * mv2.Scalar1346 - mv1.Scalar13456 * mv2.Scalar1236,
            Scalar345 = mv1.Scalar12346 * mv2.Scalar1256 - mv1.Scalar12356 * mv2.Scalar1246 + mv1.Scalar12456 * mv2.Scalar1236,
            Scalar126 = -mv1.Scalar12345 * mv2.Scalar3456 + mv1.Scalar13456 * mv2.Scalar2345 - mv1.Scalar23456 * mv2.Scalar1345,
            Scalar136 = mv1.Scalar12345 * mv2.Scalar2456 - mv1.Scalar12456 * mv2.Scalar2345 + mv1.Scalar23456 * mv2.Scalar1245,
            Scalar236 = -mv1.Scalar12345 * mv2.Scalar1456 + mv1.Scalar12456 * mv2.Scalar1345 - mv1.Scalar13456 * mv2.Scalar1245,
            Scalar146 = -mv1.Scalar12345 * mv2.Scalar2356 + mv1.Scalar12356 * mv2.Scalar2345 - mv1.Scalar23456 * mv2.Scalar1235,
            Scalar246 = mv1.Scalar12345 * mv2.Scalar1356 - mv1.Scalar12356 * mv2.Scalar1345 + mv1.Scalar13456 * mv2.Scalar1235,
            Scalar346 = -mv1.Scalar12345 * mv2.Scalar1256 + mv1.Scalar12356 * mv2.Scalar1245 - mv1.Scalar12456 * mv2.Scalar1235,
            Scalar156 = mv1.Scalar12345 * mv2.Scalar2346 - mv1.Scalar12346 * mv2.Scalar2345 + mv1.Scalar23456 * mv2.Scalar1234,
            Scalar256 = -mv1.Scalar12345 * mv2.Scalar1346 + mv1.Scalar12346 * mv2.Scalar1345 - mv1.Scalar13456 * mv2.Scalar1234,
            Scalar356 = mv1.Scalar12345 * mv2.Scalar1246 - mv1.Scalar12346 * mv2.Scalar1245 + mv1.Scalar12456 * mv2.Scalar1234,
            Scalar456 = -mv1.Scalar12345 * mv2.Scalar1236 + mv1.Scalar12346 * mv2.Scalar1235 - mv1.Scalar12356 * mv2.Scalar1234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector2 Cp(this Ga6KVector5 mv1, Ga6KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector2.Zero;
        
        return new Ga6KVector2
        {
            Scalar12 = mv1.Scalar13456 * mv2.Scalar23456 - mv1.Scalar23456 * mv2.Scalar13456,
            Scalar13 = -mv1.Scalar12456 * mv2.Scalar23456 + mv1.Scalar23456 * mv2.Scalar12456,
            Scalar23 = mv1.Scalar12456 * mv2.Scalar13456 - mv1.Scalar13456 * mv2.Scalar12456,
            Scalar14 = mv1.Scalar12356 * mv2.Scalar23456 - mv1.Scalar23456 * mv2.Scalar12356,
            Scalar24 = -mv1.Scalar12356 * mv2.Scalar13456 + mv1.Scalar13456 * mv2.Scalar12356,
            Scalar34 = mv1.Scalar12356 * mv2.Scalar12456 - mv1.Scalar12456 * mv2.Scalar12356,
            Scalar15 = -mv1.Scalar12346 * mv2.Scalar23456 + mv1.Scalar23456 * mv2.Scalar12346,
            Scalar25 = mv1.Scalar12346 * mv2.Scalar13456 - mv1.Scalar13456 * mv2.Scalar12346,
            Scalar35 = -mv1.Scalar12346 * mv2.Scalar12456 + mv1.Scalar12456 * mv2.Scalar12346,
            Scalar45 = mv1.Scalar12346 * mv2.Scalar12356 - mv1.Scalar12356 * mv2.Scalar12346,
            Scalar16 = mv1.Scalar12345 * mv2.Scalar23456 - mv1.Scalar23456 * mv2.Scalar12345,
            Scalar26 = -mv1.Scalar12345 * mv2.Scalar13456 + mv1.Scalar13456 * mv2.Scalar12345,
            Scalar36 = mv1.Scalar12345 * mv2.Scalar12456 - mv1.Scalar12456 * mv2.Scalar12345,
            Scalar46 = -mv1.Scalar12345 * mv2.Scalar12356 + mv1.Scalar12356 * mv2.Scalar12345,
            Scalar56 = mv1.Scalar12345 * mv2.Scalar12346 - mv1.Scalar12346 * mv2.Scalar12345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector1 Cp(this Ga6KVector5 mv1, Ga6KVector6 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector1.Zero;
        
        return new Ga6KVector1
        {
            Scalar1 = -mv1.Scalar23456 * mv2.Scalar123456,
            Scalar2 = mv1.Scalar13456 * mv2.Scalar123456,
            Scalar3 = -mv1.Scalar12456 * mv2.Scalar123456,
            Scalar4 = mv1.Scalar12356 * mv2.Scalar123456,
            Scalar5 = -mv1.Scalar12346 * mv2.Scalar123456,
            Scalar6 = mv1.Scalar12345 * mv2.Scalar123456
        };
    }
    
    public static Ga6Multivector Cp(this Ga6KVector5 mv1, Ga6Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[63] += mv1.Scalar12345 * mv2.KVector1.Scalar6 - mv1.Scalar12346 * mv2.KVector1.Scalar5 + mv1.Scalar12356 * mv2.KVector1.Scalar4 - mv1.Scalar12456 * mv2.KVector1.Scalar3 + mv1.Scalar13456 * mv2.KVector1.Scalar2 - mv1.Scalar23456 * mv2.KVector1.Scalar1;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[31] += -mv1.Scalar12346 * mv2.KVector2.Scalar56 + mv1.Scalar12356 * mv2.KVector2.Scalar46 - mv1.Scalar12456 * mv2.KVector2.Scalar36 + mv1.Scalar13456 * mv2.KVector2.Scalar26 - mv1.Scalar23456 * mv2.KVector2.Scalar16;
            tempScalar[47] += mv1.Scalar12345 * mv2.KVector2.Scalar56 - mv1.Scalar12356 * mv2.KVector2.Scalar45 + mv1.Scalar12456 * mv2.KVector2.Scalar35 - mv1.Scalar13456 * mv2.KVector2.Scalar25 + mv1.Scalar23456 * mv2.KVector2.Scalar15;
            tempScalar[55] += -mv1.Scalar12345 * mv2.KVector2.Scalar46 + mv1.Scalar12346 * mv2.KVector2.Scalar45 - mv1.Scalar12456 * mv2.KVector2.Scalar34 + mv1.Scalar13456 * mv2.KVector2.Scalar24 - mv1.Scalar23456 * mv2.KVector2.Scalar14;
            tempScalar[59] += mv1.Scalar12345 * mv2.KVector2.Scalar36 - mv1.Scalar12346 * mv2.KVector2.Scalar35 + mv1.Scalar12356 * mv2.KVector2.Scalar34 - mv1.Scalar13456 * mv2.KVector2.Scalar23 + mv1.Scalar23456 * mv2.KVector2.Scalar13;
            tempScalar[61] += -mv1.Scalar12345 * mv2.KVector2.Scalar26 + mv1.Scalar12346 * mv2.KVector2.Scalar25 - mv1.Scalar12356 * mv2.KVector2.Scalar24 + mv1.Scalar12456 * mv2.KVector2.Scalar23 - mv1.Scalar23456 * mv2.KVector2.Scalar12;
            tempScalar[62] += mv1.Scalar12345 * mv2.KVector2.Scalar16 - mv1.Scalar12346 * mv2.KVector2.Scalar15 + mv1.Scalar12356 * mv2.KVector2.Scalar14 - mv1.Scalar12456 * mv2.KVector2.Scalar13 + mv1.Scalar13456 * mv2.KVector2.Scalar12;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[15] += -mv1.Scalar12356 * mv2.KVector3.Scalar456 + mv1.Scalar12456 * mv2.KVector3.Scalar356 - mv1.Scalar13456 * mv2.KVector3.Scalar256 + mv1.Scalar23456 * mv2.KVector3.Scalar156;
            tempScalar[23] += mv1.Scalar12346 * mv2.KVector3.Scalar456 - mv1.Scalar12456 * mv2.KVector3.Scalar346 + mv1.Scalar13456 * mv2.KVector3.Scalar246 - mv1.Scalar23456 * mv2.KVector3.Scalar146;
            tempScalar[27] += -mv1.Scalar12346 * mv2.KVector3.Scalar356 + mv1.Scalar12356 * mv2.KVector3.Scalar346 - mv1.Scalar13456 * mv2.KVector3.Scalar236 + mv1.Scalar23456 * mv2.KVector3.Scalar136;
            tempScalar[29] += mv1.Scalar12346 * mv2.KVector3.Scalar256 - mv1.Scalar12356 * mv2.KVector3.Scalar246 + mv1.Scalar12456 * mv2.KVector3.Scalar236 - mv1.Scalar23456 * mv2.KVector3.Scalar126;
            tempScalar[30] += -mv1.Scalar12346 * mv2.KVector3.Scalar156 + mv1.Scalar12356 * mv2.KVector3.Scalar146 - mv1.Scalar12456 * mv2.KVector3.Scalar136 + mv1.Scalar13456 * mv2.KVector3.Scalar126;
            tempScalar[39] += -mv1.Scalar12345 * mv2.KVector3.Scalar456 + mv1.Scalar12456 * mv2.KVector3.Scalar345 - mv1.Scalar13456 * mv2.KVector3.Scalar245 + mv1.Scalar23456 * mv2.KVector3.Scalar145;
            tempScalar[43] += mv1.Scalar12345 * mv2.KVector3.Scalar356 - mv1.Scalar12356 * mv2.KVector3.Scalar345 + mv1.Scalar13456 * mv2.KVector3.Scalar235 - mv1.Scalar23456 * mv2.KVector3.Scalar135;
            tempScalar[45] += -mv1.Scalar12345 * mv2.KVector3.Scalar256 + mv1.Scalar12356 * mv2.KVector3.Scalar245 - mv1.Scalar12456 * mv2.KVector3.Scalar235 + mv1.Scalar23456 * mv2.KVector3.Scalar125;
            tempScalar[46] += mv1.Scalar12345 * mv2.KVector3.Scalar156 - mv1.Scalar12356 * mv2.KVector3.Scalar145 + mv1.Scalar12456 * mv2.KVector3.Scalar135 - mv1.Scalar13456 * mv2.KVector3.Scalar125;
            tempScalar[51] += -mv1.Scalar12345 * mv2.KVector3.Scalar346 + mv1.Scalar12346 * mv2.KVector3.Scalar345 - mv1.Scalar13456 * mv2.KVector3.Scalar234 + mv1.Scalar23456 * mv2.KVector3.Scalar134;
            tempScalar[53] += mv1.Scalar12345 * mv2.KVector3.Scalar246 - mv1.Scalar12346 * mv2.KVector3.Scalar245 + mv1.Scalar12456 * mv2.KVector3.Scalar234 - mv1.Scalar23456 * mv2.KVector3.Scalar124;
            tempScalar[54] += -mv1.Scalar12345 * mv2.KVector3.Scalar146 + mv1.Scalar12346 * mv2.KVector3.Scalar145 - mv1.Scalar12456 * mv2.KVector3.Scalar134 + mv1.Scalar13456 * mv2.KVector3.Scalar124;
            tempScalar[57] += -mv1.Scalar12345 * mv2.KVector3.Scalar236 + mv1.Scalar12346 * mv2.KVector3.Scalar235 - mv1.Scalar12356 * mv2.KVector3.Scalar234 + mv1.Scalar23456 * mv2.KVector3.Scalar123;
            tempScalar[58] += mv1.Scalar12345 * mv2.KVector3.Scalar136 - mv1.Scalar12346 * mv2.KVector3.Scalar135 + mv1.Scalar12356 * mv2.KVector3.Scalar134 - mv1.Scalar13456 * mv2.KVector3.Scalar123;
            tempScalar[60] += -mv1.Scalar12345 * mv2.KVector3.Scalar126 + mv1.Scalar12346 * mv2.KVector3.Scalar125 - mv1.Scalar12356 * mv2.KVector3.Scalar124 + mv1.Scalar12456 * mv2.KVector3.Scalar123;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[7] += mv1.Scalar12456 * mv2.KVector4.Scalar3456 - mv1.Scalar13456 * mv2.KVector4.Scalar2456 + mv1.Scalar23456 * mv2.KVector4.Scalar1456;
            tempScalar[11] += -mv1.Scalar12356 * mv2.KVector4.Scalar3456 + mv1.Scalar13456 * mv2.KVector4.Scalar2356 - mv1.Scalar23456 * mv2.KVector4.Scalar1356;
            tempScalar[13] += mv1.Scalar12356 * mv2.KVector4.Scalar2456 - mv1.Scalar12456 * mv2.KVector4.Scalar2356 + mv1.Scalar23456 * mv2.KVector4.Scalar1256;
            tempScalar[14] += -mv1.Scalar12356 * mv2.KVector4.Scalar1456 + mv1.Scalar12456 * mv2.KVector4.Scalar1356 - mv1.Scalar13456 * mv2.KVector4.Scalar1256;
            tempScalar[19] += mv1.Scalar12346 * mv2.KVector4.Scalar3456 - mv1.Scalar13456 * mv2.KVector4.Scalar2346 + mv1.Scalar23456 * mv2.KVector4.Scalar1346;
            tempScalar[21] += -mv1.Scalar12346 * mv2.KVector4.Scalar2456 + mv1.Scalar12456 * mv2.KVector4.Scalar2346 - mv1.Scalar23456 * mv2.KVector4.Scalar1246;
            tempScalar[22] += mv1.Scalar12346 * mv2.KVector4.Scalar1456 - mv1.Scalar12456 * mv2.KVector4.Scalar1346 + mv1.Scalar13456 * mv2.KVector4.Scalar1246;
            tempScalar[25] += mv1.Scalar12346 * mv2.KVector4.Scalar2356 - mv1.Scalar12356 * mv2.KVector4.Scalar2346 + mv1.Scalar23456 * mv2.KVector4.Scalar1236;
            tempScalar[26] += -mv1.Scalar12346 * mv2.KVector4.Scalar1356 + mv1.Scalar12356 * mv2.KVector4.Scalar1346 - mv1.Scalar13456 * mv2.KVector4.Scalar1236;
            tempScalar[28] += mv1.Scalar12346 * mv2.KVector4.Scalar1256 - mv1.Scalar12356 * mv2.KVector4.Scalar1246 + mv1.Scalar12456 * mv2.KVector4.Scalar1236;
            tempScalar[35] += -mv1.Scalar12345 * mv2.KVector4.Scalar3456 + mv1.Scalar13456 * mv2.KVector4.Scalar2345 - mv1.Scalar23456 * mv2.KVector4.Scalar1345;
            tempScalar[37] += mv1.Scalar12345 * mv2.KVector4.Scalar2456 - mv1.Scalar12456 * mv2.KVector4.Scalar2345 + mv1.Scalar23456 * mv2.KVector4.Scalar1245;
            tempScalar[38] += -mv1.Scalar12345 * mv2.KVector4.Scalar1456 + mv1.Scalar12456 * mv2.KVector4.Scalar1345 - mv1.Scalar13456 * mv2.KVector4.Scalar1245;
            tempScalar[41] += -mv1.Scalar12345 * mv2.KVector4.Scalar2356 + mv1.Scalar12356 * mv2.KVector4.Scalar2345 - mv1.Scalar23456 * mv2.KVector4.Scalar1235;
            tempScalar[42] += mv1.Scalar12345 * mv2.KVector4.Scalar1356 - mv1.Scalar12356 * mv2.KVector4.Scalar1345 + mv1.Scalar13456 * mv2.KVector4.Scalar1235;
            tempScalar[44] += -mv1.Scalar12345 * mv2.KVector4.Scalar1256 + mv1.Scalar12356 * mv2.KVector4.Scalar1245 - mv1.Scalar12456 * mv2.KVector4.Scalar1235;
            tempScalar[49] += mv1.Scalar12345 * mv2.KVector4.Scalar2346 - mv1.Scalar12346 * mv2.KVector4.Scalar2345 + mv1.Scalar23456 * mv2.KVector4.Scalar1234;
            tempScalar[50] += -mv1.Scalar12345 * mv2.KVector4.Scalar1346 + mv1.Scalar12346 * mv2.KVector4.Scalar1345 - mv1.Scalar13456 * mv2.KVector4.Scalar1234;
            tempScalar[52] += mv1.Scalar12345 * mv2.KVector4.Scalar1246 - mv1.Scalar12346 * mv2.KVector4.Scalar1245 + mv1.Scalar12456 * mv2.KVector4.Scalar1234;
            tempScalar[56] += -mv1.Scalar12345 * mv2.KVector4.Scalar1236 + mv1.Scalar12346 * mv2.KVector4.Scalar1235 - mv1.Scalar12356 * mv2.KVector4.Scalar1234;
        }
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[3] += mv1.Scalar13456 * mv2.KVector5.Scalar23456 - mv1.Scalar23456 * mv2.KVector5.Scalar13456;
            tempScalar[5] += -mv1.Scalar12456 * mv2.KVector5.Scalar23456 + mv1.Scalar23456 * mv2.KVector5.Scalar12456;
            tempScalar[6] += mv1.Scalar12456 * mv2.KVector5.Scalar13456 - mv1.Scalar13456 * mv2.KVector5.Scalar12456;
            tempScalar[9] += mv1.Scalar12356 * mv2.KVector5.Scalar23456 - mv1.Scalar23456 * mv2.KVector5.Scalar12356;
            tempScalar[10] += -mv1.Scalar12356 * mv2.KVector5.Scalar13456 + mv1.Scalar13456 * mv2.KVector5.Scalar12356;
            tempScalar[12] += mv1.Scalar12356 * mv2.KVector5.Scalar12456 - mv1.Scalar12456 * mv2.KVector5.Scalar12356;
            tempScalar[17] += -mv1.Scalar12346 * mv2.KVector5.Scalar23456 + mv1.Scalar23456 * mv2.KVector5.Scalar12346;
            tempScalar[18] += mv1.Scalar12346 * mv2.KVector5.Scalar13456 - mv1.Scalar13456 * mv2.KVector5.Scalar12346;
            tempScalar[20] += -mv1.Scalar12346 * mv2.KVector5.Scalar12456 + mv1.Scalar12456 * mv2.KVector5.Scalar12346;
            tempScalar[24] += mv1.Scalar12346 * mv2.KVector5.Scalar12356 - mv1.Scalar12356 * mv2.KVector5.Scalar12346;
            tempScalar[33] += mv1.Scalar12345 * mv2.KVector5.Scalar23456 - mv1.Scalar23456 * mv2.KVector5.Scalar12345;
            tempScalar[34] += -mv1.Scalar12345 * mv2.KVector5.Scalar13456 + mv1.Scalar13456 * mv2.KVector5.Scalar12345;
            tempScalar[36] += mv1.Scalar12345 * mv2.KVector5.Scalar12456 - mv1.Scalar12456 * mv2.KVector5.Scalar12345;
            tempScalar[40] += -mv1.Scalar12345 * mv2.KVector5.Scalar12356 + mv1.Scalar12356 * mv2.KVector5.Scalar12345;
            tempScalar[48] += mv1.Scalar12345 * mv2.KVector5.Scalar12346 - mv1.Scalar12346 * mv2.KVector5.Scalar12345;
        }
        
        if (!mv2.KVector6.IsZero())
        {
            tempScalar[1] += -mv1.Scalar23456 * mv2.KVector6.Scalar123456;
            tempScalar[2] += mv1.Scalar13456 * mv2.KVector6.Scalar123456;
            tempScalar[4] += -mv1.Scalar12456 * mv2.KVector6.Scalar123456;
            tempScalar[8] += mv1.Scalar12356 * mv2.KVector6.Scalar123456;
            tempScalar[16] += -mv1.Scalar12346 * mv2.KVector6.Scalar123456;
            tempScalar[32] += mv1.Scalar12345 * mv2.KVector6.Scalar123456;
        }
        
        return Ga6Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector6 mv1, Ga6KVector0 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector5 Cp(this Ga6KVector6 mv1, Ga6KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector5.Zero;
        
        return new Ga6KVector5
        {
            Scalar12345 = mv1.Scalar123456 * mv2.Scalar6,
            Scalar12346 = -mv1.Scalar123456 * mv2.Scalar5,
            Scalar12356 = mv1.Scalar123456 * mv2.Scalar4,
            Scalar12456 = -mv1.Scalar123456 * mv2.Scalar3,
            Scalar13456 = mv1.Scalar123456 * mv2.Scalar2,
            Scalar23456 = -mv1.Scalar123456 * mv2.Scalar1
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector6 mv1, Ga6KVector2 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector3 Cp(this Ga6KVector6 mv1, Ga6KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector3.Zero;
        
        return new Ga6KVector3
        {
            Scalar123 = -mv1.Scalar123456 * mv2.Scalar456,
            Scalar124 = mv1.Scalar123456 * mv2.Scalar356,
            Scalar134 = -mv1.Scalar123456 * mv2.Scalar256,
            Scalar234 = mv1.Scalar123456 * mv2.Scalar156,
            Scalar125 = -mv1.Scalar123456 * mv2.Scalar346,
            Scalar135 = mv1.Scalar123456 * mv2.Scalar246,
            Scalar235 = -mv1.Scalar123456 * mv2.Scalar146,
            Scalar145 = -mv1.Scalar123456 * mv2.Scalar236,
            Scalar245 = mv1.Scalar123456 * mv2.Scalar136,
            Scalar345 = -mv1.Scalar123456 * mv2.Scalar126,
            Scalar126 = mv1.Scalar123456 * mv2.Scalar345,
            Scalar136 = -mv1.Scalar123456 * mv2.Scalar245,
            Scalar236 = mv1.Scalar123456 * mv2.Scalar145,
            Scalar146 = mv1.Scalar123456 * mv2.Scalar235,
            Scalar246 = -mv1.Scalar123456 * mv2.Scalar135,
            Scalar346 = mv1.Scalar123456 * mv2.Scalar125,
            Scalar156 = -mv1.Scalar123456 * mv2.Scalar234,
            Scalar256 = mv1.Scalar123456 * mv2.Scalar134,
            Scalar356 = -mv1.Scalar123456 * mv2.Scalar124,
            Scalar456 = mv1.Scalar123456 * mv2.Scalar123
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector6 mv1, Ga6KVector4 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector1 Cp(this Ga6KVector6 mv1, Ga6KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6KVector1.Zero;
        
        return new Ga6KVector1
        {
            Scalar1 = mv1.Scalar123456 * mv2.Scalar23456,
            Scalar2 = -mv1.Scalar123456 * mv2.Scalar13456,
            Scalar3 = mv1.Scalar123456 * mv2.Scalar12456,
            Scalar4 = -mv1.Scalar123456 * mv2.Scalar12356,
            Scalar5 = mv1.Scalar123456 * mv2.Scalar12346,
            Scalar6 = -mv1.Scalar123456 * mv2.Scalar12345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6KVector6 mv1, Ga6KVector6 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    public static Ga6Multivector Cp(this Ga6KVector6 mv1, Ga6Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[31] += mv1.Scalar123456 * mv2.KVector1.Scalar6;
            tempScalar[47] += -mv1.Scalar123456 * mv2.KVector1.Scalar5;
            tempScalar[55] += mv1.Scalar123456 * mv2.KVector1.Scalar4;
            tempScalar[59] += -mv1.Scalar123456 * mv2.KVector1.Scalar3;
            tempScalar[61] += mv1.Scalar123456 * mv2.KVector1.Scalar2;
            tempScalar[62] += -mv1.Scalar123456 * mv2.KVector1.Scalar1;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[7] += -mv1.Scalar123456 * mv2.KVector3.Scalar456;
            tempScalar[11] += mv1.Scalar123456 * mv2.KVector3.Scalar356;
            tempScalar[13] += -mv1.Scalar123456 * mv2.KVector3.Scalar256;
            tempScalar[14] += mv1.Scalar123456 * mv2.KVector3.Scalar156;
            tempScalar[19] += -mv1.Scalar123456 * mv2.KVector3.Scalar346;
            tempScalar[21] += mv1.Scalar123456 * mv2.KVector3.Scalar246;
            tempScalar[22] += -mv1.Scalar123456 * mv2.KVector3.Scalar146;
            tempScalar[25] += -mv1.Scalar123456 * mv2.KVector3.Scalar236;
            tempScalar[26] += mv1.Scalar123456 * mv2.KVector3.Scalar136;
            tempScalar[28] += -mv1.Scalar123456 * mv2.KVector3.Scalar126;
            tempScalar[35] += mv1.Scalar123456 * mv2.KVector3.Scalar345;
            tempScalar[37] += -mv1.Scalar123456 * mv2.KVector3.Scalar245;
            tempScalar[38] += mv1.Scalar123456 * mv2.KVector3.Scalar145;
            tempScalar[41] += mv1.Scalar123456 * mv2.KVector3.Scalar235;
            tempScalar[42] += -mv1.Scalar123456 * mv2.KVector3.Scalar135;
            tempScalar[44] += mv1.Scalar123456 * mv2.KVector3.Scalar125;
            tempScalar[49] += -mv1.Scalar123456 * mv2.KVector3.Scalar234;
            tempScalar[50] += mv1.Scalar123456 * mv2.KVector3.Scalar134;
            tempScalar[52] += -mv1.Scalar123456 * mv2.KVector3.Scalar124;
            tempScalar[56] += mv1.Scalar123456 * mv2.KVector3.Scalar123;
        }
        
        if (!mv2.KVector5.IsZero())
        {
            tempScalar[1] += mv1.Scalar123456 * mv2.KVector5.Scalar23456;
            tempScalar[2] += -mv1.Scalar123456 * mv2.KVector5.Scalar13456;
            tempScalar[4] += mv1.Scalar123456 * mv2.KVector5.Scalar12456;
            tempScalar[8] += -mv1.Scalar123456 * mv2.KVector5.Scalar12356;
            tempScalar[16] += mv1.Scalar123456 * mv2.KVector5.Scalar12346;
            tempScalar[32] += -mv1.Scalar123456 * mv2.KVector5.Scalar12345;
        }
        
        return Ga6Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga6KVector0 Cp(this Ga6Multivector mv1, Ga6KVector0 mv2)
    {
        return Ga6KVector0.Zero;
    }
    
    public static Ga6Multivector Cp(this Ga6Multivector mv1, Ga6KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[3] += mv1.KVector1.Scalar1 * mv2.Scalar2 - mv1.KVector1.Scalar2 * mv2.Scalar1;
            tempScalar[5] += mv1.KVector1.Scalar1 * mv2.Scalar3 - mv1.KVector1.Scalar3 * mv2.Scalar1;
            tempScalar[6] += mv1.KVector1.Scalar2 * mv2.Scalar3 - mv1.KVector1.Scalar3 * mv2.Scalar2;
            tempScalar[9] += mv1.KVector1.Scalar1 * mv2.Scalar4 - mv1.KVector1.Scalar4 * mv2.Scalar1;
            tempScalar[10] += mv1.KVector1.Scalar2 * mv2.Scalar4 - mv1.KVector1.Scalar4 * mv2.Scalar2;
            tempScalar[12] += mv1.KVector1.Scalar3 * mv2.Scalar4 - mv1.KVector1.Scalar4 * mv2.Scalar3;
            tempScalar[17] += mv1.KVector1.Scalar1 * mv2.Scalar5 - mv1.KVector1.Scalar5 * mv2.Scalar1;
            tempScalar[18] += mv1.KVector1.Scalar2 * mv2.Scalar5 - mv1.KVector1.Scalar5 * mv2.Scalar2;
            tempScalar[20] += mv1.KVector1.Scalar3 * mv2.Scalar5 - mv1.KVector1.Scalar5 * mv2.Scalar3;
            tempScalar[24] += mv1.KVector1.Scalar4 * mv2.Scalar5 - mv1.KVector1.Scalar5 * mv2.Scalar4;
            tempScalar[33] += mv1.KVector1.Scalar1 * mv2.Scalar6 - mv1.KVector1.Scalar6 * mv2.Scalar1;
            tempScalar[34] += mv1.KVector1.Scalar2 * mv2.Scalar6 - mv1.KVector1.Scalar6 * mv2.Scalar2;
            tempScalar[36] += mv1.KVector1.Scalar3 * mv2.Scalar6 - mv1.KVector1.Scalar6 * mv2.Scalar3;
            tempScalar[40] += mv1.KVector1.Scalar4 * mv2.Scalar6 - mv1.KVector1.Scalar6 * mv2.Scalar4;
            tempScalar[48] += mv1.KVector1.Scalar5 * mv2.Scalar6 - mv1.KVector1.Scalar6 * mv2.Scalar5;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[1] += mv1.KVector2.Scalar12 * mv2.Scalar2 + mv1.KVector2.Scalar13 * mv2.Scalar3 + mv1.KVector2.Scalar14 * mv2.Scalar4 + mv1.KVector2.Scalar15 * mv2.Scalar5 + mv1.KVector2.Scalar16 * mv2.Scalar6;
            tempScalar[2] += -mv1.KVector2.Scalar12 * mv2.Scalar1 + mv1.KVector2.Scalar23 * mv2.Scalar3 + mv1.KVector2.Scalar24 * mv2.Scalar4 + mv1.KVector2.Scalar25 * mv2.Scalar5 + mv1.KVector2.Scalar26 * mv2.Scalar6;
            tempScalar[4] += -mv1.KVector2.Scalar13 * mv2.Scalar1 - mv1.KVector2.Scalar23 * mv2.Scalar2 + mv1.KVector2.Scalar34 * mv2.Scalar4 + mv1.KVector2.Scalar35 * mv2.Scalar5 + mv1.KVector2.Scalar36 * mv2.Scalar6;
            tempScalar[8] += -mv1.KVector2.Scalar14 * mv2.Scalar1 - mv1.KVector2.Scalar24 * mv2.Scalar2 - mv1.KVector2.Scalar34 * mv2.Scalar3 + mv1.KVector2.Scalar45 * mv2.Scalar5 + mv1.KVector2.Scalar46 * mv2.Scalar6;
            tempScalar[16] += -mv1.KVector2.Scalar15 * mv2.Scalar1 - mv1.KVector2.Scalar25 * mv2.Scalar2 - mv1.KVector2.Scalar35 * mv2.Scalar3 - mv1.KVector2.Scalar45 * mv2.Scalar4 + mv1.KVector2.Scalar56 * mv2.Scalar6;
            tempScalar[32] += -mv1.KVector2.Scalar16 * mv2.Scalar1 - mv1.KVector2.Scalar26 * mv2.Scalar2 - mv1.KVector2.Scalar36 * mv2.Scalar3 - mv1.KVector2.Scalar46 * mv2.Scalar4 - mv1.KVector2.Scalar56 * mv2.Scalar5;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[15] += mv1.KVector3.Scalar123 * mv2.Scalar4 - mv1.KVector3.Scalar124 * mv2.Scalar3 + mv1.KVector3.Scalar134 * mv2.Scalar2 - mv1.KVector3.Scalar234 * mv2.Scalar1;
            tempScalar[23] += mv1.KVector3.Scalar123 * mv2.Scalar5 - mv1.KVector3.Scalar125 * mv2.Scalar3 + mv1.KVector3.Scalar135 * mv2.Scalar2 - mv1.KVector3.Scalar235 * mv2.Scalar1;
            tempScalar[27] += mv1.KVector3.Scalar124 * mv2.Scalar5 - mv1.KVector3.Scalar125 * mv2.Scalar4 + mv1.KVector3.Scalar145 * mv2.Scalar2 - mv1.KVector3.Scalar245 * mv2.Scalar1;
            tempScalar[29] += mv1.KVector3.Scalar134 * mv2.Scalar5 - mv1.KVector3.Scalar135 * mv2.Scalar4 + mv1.KVector3.Scalar145 * mv2.Scalar3 - mv1.KVector3.Scalar345 * mv2.Scalar1;
            tempScalar[30] += mv1.KVector3.Scalar234 * mv2.Scalar5 - mv1.KVector3.Scalar235 * mv2.Scalar4 + mv1.KVector3.Scalar245 * mv2.Scalar3 - mv1.KVector3.Scalar345 * mv2.Scalar2;
            tempScalar[39] += mv1.KVector3.Scalar123 * mv2.Scalar6 - mv1.KVector3.Scalar126 * mv2.Scalar3 + mv1.KVector3.Scalar136 * mv2.Scalar2 - mv1.KVector3.Scalar236 * mv2.Scalar1;
            tempScalar[43] += mv1.KVector3.Scalar124 * mv2.Scalar6 - mv1.KVector3.Scalar126 * mv2.Scalar4 + mv1.KVector3.Scalar146 * mv2.Scalar2 - mv1.KVector3.Scalar246 * mv2.Scalar1;
            tempScalar[45] += mv1.KVector3.Scalar134 * mv2.Scalar6 - mv1.KVector3.Scalar136 * mv2.Scalar4 + mv1.KVector3.Scalar146 * mv2.Scalar3 - mv1.KVector3.Scalar346 * mv2.Scalar1;
            tempScalar[46] += mv1.KVector3.Scalar234 * mv2.Scalar6 - mv1.KVector3.Scalar236 * mv2.Scalar4 + mv1.KVector3.Scalar246 * mv2.Scalar3 - mv1.KVector3.Scalar346 * mv2.Scalar2;
            tempScalar[51] += mv1.KVector3.Scalar125 * mv2.Scalar6 - mv1.KVector3.Scalar126 * mv2.Scalar5 + mv1.KVector3.Scalar156 * mv2.Scalar2 - mv1.KVector3.Scalar256 * mv2.Scalar1;
            tempScalar[53] += mv1.KVector3.Scalar135 * mv2.Scalar6 - mv1.KVector3.Scalar136 * mv2.Scalar5 + mv1.KVector3.Scalar156 * mv2.Scalar3 - mv1.KVector3.Scalar356 * mv2.Scalar1;
            tempScalar[54] += mv1.KVector3.Scalar235 * mv2.Scalar6 - mv1.KVector3.Scalar236 * mv2.Scalar5 + mv1.KVector3.Scalar256 * mv2.Scalar3 - mv1.KVector3.Scalar356 * mv2.Scalar2;
            tempScalar[57] += mv1.KVector3.Scalar145 * mv2.Scalar6 - mv1.KVector3.Scalar146 * mv2.Scalar5 + mv1.KVector3.Scalar156 * mv2.Scalar4 - mv1.KVector3.Scalar456 * mv2.Scalar1;
            tempScalar[58] += mv1.KVector3.Scalar245 * mv2.Scalar6 - mv1.KVector3.Scalar246 * mv2.Scalar5 + mv1.KVector3.Scalar256 * mv2.Scalar4 - mv1.KVector3.Scalar456 * mv2.Scalar2;
            tempScalar[60] += mv1.KVector3.Scalar345 * mv2.Scalar6 - mv1.KVector3.Scalar346 * mv2.Scalar5 + mv1.KVector3.Scalar356 * mv2.Scalar4 - mv1.KVector3.Scalar456 * mv2.Scalar3;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[7] += mv1.KVector4.Scalar1234 * mv2.Scalar4 + mv1.KVector4.Scalar1235 * mv2.Scalar5 + mv1.KVector4.Scalar1236 * mv2.Scalar6;
            tempScalar[11] += -mv1.KVector4.Scalar1234 * mv2.Scalar3 + mv1.KVector4.Scalar1245 * mv2.Scalar5 + mv1.KVector4.Scalar1246 * mv2.Scalar6;
            tempScalar[13] += mv1.KVector4.Scalar1234 * mv2.Scalar2 + mv1.KVector4.Scalar1345 * mv2.Scalar5 + mv1.KVector4.Scalar1346 * mv2.Scalar6;
            tempScalar[14] += -mv1.KVector4.Scalar1234 * mv2.Scalar1 + mv1.KVector4.Scalar2345 * mv2.Scalar5 + mv1.KVector4.Scalar2346 * mv2.Scalar6;
            tempScalar[19] += -mv1.KVector4.Scalar1235 * mv2.Scalar3 - mv1.KVector4.Scalar1245 * mv2.Scalar4 + mv1.KVector4.Scalar1256 * mv2.Scalar6;
            tempScalar[21] += mv1.KVector4.Scalar1235 * mv2.Scalar2 - mv1.KVector4.Scalar1345 * mv2.Scalar4 + mv1.KVector4.Scalar1356 * mv2.Scalar6;
            tempScalar[22] += -mv1.KVector4.Scalar1235 * mv2.Scalar1 - mv1.KVector4.Scalar2345 * mv2.Scalar4 + mv1.KVector4.Scalar2356 * mv2.Scalar6;
            tempScalar[25] += mv1.KVector4.Scalar1245 * mv2.Scalar2 + mv1.KVector4.Scalar1345 * mv2.Scalar3 + mv1.KVector4.Scalar1456 * mv2.Scalar6;
            tempScalar[26] += -mv1.KVector4.Scalar1245 * mv2.Scalar1 + mv1.KVector4.Scalar2345 * mv2.Scalar3 + mv1.KVector4.Scalar2456 * mv2.Scalar6;
            tempScalar[28] += -mv1.KVector4.Scalar1345 * mv2.Scalar1 - mv1.KVector4.Scalar2345 * mv2.Scalar2 + mv1.KVector4.Scalar3456 * mv2.Scalar6;
            tempScalar[35] += -mv1.KVector4.Scalar1236 * mv2.Scalar3 - mv1.KVector4.Scalar1246 * mv2.Scalar4 - mv1.KVector4.Scalar1256 * mv2.Scalar5;
            tempScalar[37] += mv1.KVector4.Scalar1236 * mv2.Scalar2 - mv1.KVector4.Scalar1346 * mv2.Scalar4 - mv1.KVector4.Scalar1356 * mv2.Scalar5;
            tempScalar[38] += -mv1.KVector4.Scalar1236 * mv2.Scalar1 - mv1.KVector4.Scalar2346 * mv2.Scalar4 - mv1.KVector4.Scalar2356 * mv2.Scalar5;
            tempScalar[41] += mv1.KVector4.Scalar1246 * mv2.Scalar2 + mv1.KVector4.Scalar1346 * mv2.Scalar3 - mv1.KVector4.Scalar1456 * mv2.Scalar5;
            tempScalar[42] += -mv1.KVector4.Scalar1246 * mv2.Scalar1 + mv1.KVector4.Scalar2346 * mv2.Scalar3 - mv1.KVector4.Scalar2456 * mv2.Scalar5;
            tempScalar[44] += -mv1.KVector4.Scalar1346 * mv2.Scalar1 - mv1.KVector4.Scalar2346 * mv2.Scalar2 - mv1.KVector4.Scalar3456 * mv2.Scalar5;
            tempScalar[49] += mv1.KVector4.Scalar1256 * mv2.Scalar2 + mv1.KVector4.Scalar1356 * mv2.Scalar3 + mv1.KVector4.Scalar1456 * mv2.Scalar4;
            tempScalar[50] += -mv1.KVector4.Scalar1256 * mv2.Scalar1 + mv1.KVector4.Scalar2356 * mv2.Scalar3 + mv1.KVector4.Scalar2456 * mv2.Scalar4;
            tempScalar[52] += -mv1.KVector4.Scalar1356 * mv2.Scalar1 - mv1.KVector4.Scalar2356 * mv2.Scalar2 + mv1.KVector4.Scalar3456 * mv2.Scalar4;
            tempScalar[56] += -mv1.KVector4.Scalar1456 * mv2.Scalar1 - mv1.KVector4.Scalar2456 * mv2.Scalar2 - mv1.KVector4.Scalar3456 * mv2.Scalar3;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[63] += mv1.KVector5.Scalar12345 * mv2.Scalar6 - mv1.KVector5.Scalar12346 * mv2.Scalar5 + mv1.KVector5.Scalar12356 * mv2.Scalar4 - mv1.KVector5.Scalar12456 * mv2.Scalar3 + mv1.KVector5.Scalar13456 * mv2.Scalar2 - mv1.KVector5.Scalar23456 * mv2.Scalar1;
        }
        
        if (!mv1.KVector6.IsZero())
        {
            tempScalar[31] += mv1.KVector6.Scalar123456 * mv2.Scalar6;
            tempScalar[47] += -mv1.KVector6.Scalar123456 * mv2.Scalar5;
            tempScalar[55] += mv1.KVector6.Scalar123456 * mv2.Scalar4;
            tempScalar[59] += -mv1.KVector6.Scalar123456 * mv2.Scalar3;
            tempScalar[61] += mv1.KVector6.Scalar123456 * mv2.Scalar2;
            tempScalar[62] += -mv1.KVector6.Scalar123456 * mv2.Scalar1;
        }
        
        return Ga6Multivector.Create(tempScalar);
    }
    
    public static Ga6Multivector Cp(this Ga6Multivector mv1, Ga6KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += -mv1.KVector1.Scalar2 * mv2.Scalar12 - mv1.KVector1.Scalar3 * mv2.Scalar13 - mv1.KVector1.Scalar4 * mv2.Scalar14 - mv1.KVector1.Scalar5 * mv2.Scalar15 - mv1.KVector1.Scalar6 * mv2.Scalar16;
            tempScalar[2] += mv1.KVector1.Scalar1 * mv2.Scalar12 - mv1.KVector1.Scalar3 * mv2.Scalar23 - mv1.KVector1.Scalar4 * mv2.Scalar24 - mv1.KVector1.Scalar5 * mv2.Scalar25 - mv1.KVector1.Scalar6 * mv2.Scalar26;
            tempScalar[4] += mv1.KVector1.Scalar1 * mv2.Scalar13 + mv1.KVector1.Scalar2 * mv2.Scalar23 - mv1.KVector1.Scalar4 * mv2.Scalar34 - mv1.KVector1.Scalar5 * mv2.Scalar35 - mv1.KVector1.Scalar6 * mv2.Scalar36;
            tempScalar[8] += mv1.KVector1.Scalar1 * mv2.Scalar14 + mv1.KVector1.Scalar2 * mv2.Scalar24 + mv1.KVector1.Scalar3 * mv2.Scalar34 - mv1.KVector1.Scalar5 * mv2.Scalar45 - mv1.KVector1.Scalar6 * mv2.Scalar46;
            tempScalar[16] += mv1.KVector1.Scalar1 * mv2.Scalar15 + mv1.KVector1.Scalar2 * mv2.Scalar25 + mv1.KVector1.Scalar3 * mv2.Scalar35 + mv1.KVector1.Scalar4 * mv2.Scalar45 - mv1.KVector1.Scalar6 * mv2.Scalar56;
            tempScalar[32] += mv1.KVector1.Scalar1 * mv2.Scalar16 + mv1.KVector1.Scalar2 * mv2.Scalar26 + mv1.KVector1.Scalar3 * mv2.Scalar36 + mv1.KVector1.Scalar4 * mv2.Scalar46 + mv1.KVector1.Scalar5 * mv2.Scalar56;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += -mv1.KVector2.Scalar13 * mv2.Scalar23 + mv1.KVector2.Scalar23 * mv2.Scalar13 - mv1.KVector2.Scalar14 * mv2.Scalar24 + mv1.KVector2.Scalar24 * mv2.Scalar14 - mv1.KVector2.Scalar15 * mv2.Scalar25 + mv1.KVector2.Scalar25 * mv2.Scalar15 - mv1.KVector2.Scalar16 * mv2.Scalar26 + mv1.KVector2.Scalar26 * mv2.Scalar16;
            tempScalar[5] += mv1.KVector2.Scalar12 * mv2.Scalar23 - mv1.KVector2.Scalar23 * mv2.Scalar12 - mv1.KVector2.Scalar14 * mv2.Scalar34 + mv1.KVector2.Scalar34 * mv2.Scalar14 - mv1.KVector2.Scalar15 * mv2.Scalar35 + mv1.KVector2.Scalar35 * mv2.Scalar15 - mv1.KVector2.Scalar16 * mv2.Scalar36 + mv1.KVector2.Scalar36 * mv2.Scalar16;
            tempScalar[6] += -mv1.KVector2.Scalar12 * mv2.Scalar13 + mv1.KVector2.Scalar13 * mv2.Scalar12 - mv1.KVector2.Scalar24 * mv2.Scalar34 + mv1.KVector2.Scalar34 * mv2.Scalar24 - mv1.KVector2.Scalar25 * mv2.Scalar35 + mv1.KVector2.Scalar35 * mv2.Scalar25 - mv1.KVector2.Scalar26 * mv2.Scalar36 + mv1.KVector2.Scalar36 * mv2.Scalar26;
            tempScalar[9] += mv1.KVector2.Scalar12 * mv2.Scalar24 + mv1.KVector2.Scalar13 * mv2.Scalar34 - mv1.KVector2.Scalar24 * mv2.Scalar12 - mv1.KVector2.Scalar34 * mv2.Scalar13 - mv1.KVector2.Scalar15 * mv2.Scalar45 + mv1.KVector2.Scalar45 * mv2.Scalar15 - mv1.KVector2.Scalar16 * mv2.Scalar46 + mv1.KVector2.Scalar46 * mv2.Scalar16;
            tempScalar[10] += -mv1.KVector2.Scalar12 * mv2.Scalar14 + mv1.KVector2.Scalar23 * mv2.Scalar34 + mv1.KVector2.Scalar14 * mv2.Scalar12 - mv1.KVector2.Scalar34 * mv2.Scalar23 - mv1.KVector2.Scalar25 * mv2.Scalar45 + mv1.KVector2.Scalar45 * mv2.Scalar25 - mv1.KVector2.Scalar26 * mv2.Scalar46 + mv1.KVector2.Scalar46 * mv2.Scalar26;
            tempScalar[12] += -mv1.KVector2.Scalar13 * mv2.Scalar14 - mv1.KVector2.Scalar23 * mv2.Scalar24 + mv1.KVector2.Scalar14 * mv2.Scalar13 + mv1.KVector2.Scalar24 * mv2.Scalar23 - mv1.KVector2.Scalar35 * mv2.Scalar45 + mv1.KVector2.Scalar45 * mv2.Scalar35 - mv1.KVector2.Scalar36 * mv2.Scalar46 + mv1.KVector2.Scalar46 * mv2.Scalar36;
            tempScalar[17] += mv1.KVector2.Scalar12 * mv2.Scalar25 + mv1.KVector2.Scalar13 * mv2.Scalar35 + mv1.KVector2.Scalar14 * mv2.Scalar45 - mv1.KVector2.Scalar25 * mv2.Scalar12 - mv1.KVector2.Scalar35 * mv2.Scalar13 - mv1.KVector2.Scalar45 * mv2.Scalar14 - mv1.KVector2.Scalar16 * mv2.Scalar56 + mv1.KVector2.Scalar56 * mv2.Scalar16;
            tempScalar[18] += -mv1.KVector2.Scalar12 * mv2.Scalar15 + mv1.KVector2.Scalar23 * mv2.Scalar35 + mv1.KVector2.Scalar24 * mv2.Scalar45 + mv1.KVector2.Scalar15 * mv2.Scalar12 - mv1.KVector2.Scalar35 * mv2.Scalar23 - mv1.KVector2.Scalar45 * mv2.Scalar24 - mv1.KVector2.Scalar26 * mv2.Scalar56 + mv1.KVector2.Scalar56 * mv2.Scalar26;
            tempScalar[20] += -mv1.KVector2.Scalar13 * mv2.Scalar15 - mv1.KVector2.Scalar23 * mv2.Scalar25 + mv1.KVector2.Scalar34 * mv2.Scalar45 + mv1.KVector2.Scalar15 * mv2.Scalar13 + mv1.KVector2.Scalar25 * mv2.Scalar23 - mv1.KVector2.Scalar45 * mv2.Scalar34 - mv1.KVector2.Scalar36 * mv2.Scalar56 + mv1.KVector2.Scalar56 * mv2.Scalar36;
            tempScalar[24] += -mv1.KVector2.Scalar14 * mv2.Scalar15 - mv1.KVector2.Scalar24 * mv2.Scalar25 - mv1.KVector2.Scalar34 * mv2.Scalar35 + mv1.KVector2.Scalar15 * mv2.Scalar14 + mv1.KVector2.Scalar25 * mv2.Scalar24 + mv1.KVector2.Scalar35 * mv2.Scalar34 - mv1.KVector2.Scalar46 * mv2.Scalar56 + mv1.KVector2.Scalar56 * mv2.Scalar46;
            tempScalar[33] += mv1.KVector2.Scalar12 * mv2.Scalar26 + mv1.KVector2.Scalar13 * mv2.Scalar36 + mv1.KVector2.Scalar14 * mv2.Scalar46 + mv1.KVector2.Scalar15 * mv2.Scalar56 - mv1.KVector2.Scalar26 * mv2.Scalar12 - mv1.KVector2.Scalar36 * mv2.Scalar13 - mv1.KVector2.Scalar46 * mv2.Scalar14 - mv1.KVector2.Scalar56 * mv2.Scalar15;
            tempScalar[34] += -mv1.KVector2.Scalar12 * mv2.Scalar16 + mv1.KVector2.Scalar23 * mv2.Scalar36 + mv1.KVector2.Scalar24 * mv2.Scalar46 + mv1.KVector2.Scalar25 * mv2.Scalar56 + mv1.KVector2.Scalar16 * mv2.Scalar12 - mv1.KVector2.Scalar36 * mv2.Scalar23 - mv1.KVector2.Scalar46 * mv2.Scalar24 - mv1.KVector2.Scalar56 * mv2.Scalar25;
            tempScalar[36] += -mv1.KVector2.Scalar13 * mv2.Scalar16 - mv1.KVector2.Scalar23 * mv2.Scalar26 + mv1.KVector2.Scalar34 * mv2.Scalar46 + mv1.KVector2.Scalar35 * mv2.Scalar56 + mv1.KVector2.Scalar16 * mv2.Scalar13 + mv1.KVector2.Scalar26 * mv2.Scalar23 - mv1.KVector2.Scalar46 * mv2.Scalar34 - mv1.KVector2.Scalar56 * mv2.Scalar35;
            tempScalar[40] += -mv1.KVector2.Scalar14 * mv2.Scalar16 - mv1.KVector2.Scalar24 * mv2.Scalar26 - mv1.KVector2.Scalar34 * mv2.Scalar36 + mv1.KVector2.Scalar45 * mv2.Scalar56 + mv1.KVector2.Scalar16 * mv2.Scalar14 + mv1.KVector2.Scalar26 * mv2.Scalar24 + mv1.KVector2.Scalar36 * mv2.Scalar34 - mv1.KVector2.Scalar56 * mv2.Scalar45;
            tempScalar[48] += -mv1.KVector2.Scalar15 * mv2.Scalar16 - mv1.KVector2.Scalar25 * mv2.Scalar26 - mv1.KVector2.Scalar35 * mv2.Scalar36 - mv1.KVector2.Scalar45 * mv2.Scalar46 + mv1.KVector2.Scalar16 * mv2.Scalar15 + mv1.KVector2.Scalar26 * mv2.Scalar25 + mv1.KVector2.Scalar36 * mv2.Scalar35 + mv1.KVector2.Scalar46 * mv2.Scalar45;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += -mv1.KVector3.Scalar124 * mv2.Scalar34 + mv1.KVector3.Scalar134 * mv2.Scalar24 - mv1.KVector3.Scalar234 * mv2.Scalar14 - mv1.KVector3.Scalar125 * mv2.Scalar35 + mv1.KVector3.Scalar135 * mv2.Scalar25 - mv1.KVector3.Scalar235 * mv2.Scalar15 - mv1.KVector3.Scalar126 * mv2.Scalar36 + mv1.KVector3.Scalar136 * mv2.Scalar26 - mv1.KVector3.Scalar236 * mv2.Scalar16;
            tempScalar[11] += mv1.KVector3.Scalar123 * mv2.Scalar34 - mv1.KVector3.Scalar134 * mv2.Scalar23 + mv1.KVector3.Scalar234 * mv2.Scalar13 - mv1.KVector3.Scalar125 * mv2.Scalar45 + mv1.KVector3.Scalar145 * mv2.Scalar25 - mv1.KVector3.Scalar245 * mv2.Scalar15 - mv1.KVector3.Scalar126 * mv2.Scalar46 + mv1.KVector3.Scalar146 * mv2.Scalar26 - mv1.KVector3.Scalar246 * mv2.Scalar16;
            tempScalar[13] += -mv1.KVector3.Scalar123 * mv2.Scalar24 + mv1.KVector3.Scalar124 * mv2.Scalar23 - mv1.KVector3.Scalar234 * mv2.Scalar12 - mv1.KVector3.Scalar135 * mv2.Scalar45 + mv1.KVector3.Scalar145 * mv2.Scalar35 - mv1.KVector3.Scalar345 * mv2.Scalar15 - mv1.KVector3.Scalar136 * mv2.Scalar46 + mv1.KVector3.Scalar146 * mv2.Scalar36 - mv1.KVector3.Scalar346 * mv2.Scalar16;
            tempScalar[14] += mv1.KVector3.Scalar123 * mv2.Scalar14 - mv1.KVector3.Scalar124 * mv2.Scalar13 + mv1.KVector3.Scalar134 * mv2.Scalar12 - mv1.KVector3.Scalar235 * mv2.Scalar45 + mv1.KVector3.Scalar245 * mv2.Scalar35 - mv1.KVector3.Scalar345 * mv2.Scalar25 - mv1.KVector3.Scalar236 * mv2.Scalar46 + mv1.KVector3.Scalar246 * mv2.Scalar36 - mv1.KVector3.Scalar346 * mv2.Scalar26;
            tempScalar[19] += mv1.KVector3.Scalar123 * mv2.Scalar35 + mv1.KVector3.Scalar124 * mv2.Scalar45 - mv1.KVector3.Scalar135 * mv2.Scalar23 + mv1.KVector3.Scalar235 * mv2.Scalar13 - mv1.KVector3.Scalar145 * mv2.Scalar24 + mv1.KVector3.Scalar245 * mv2.Scalar14 - mv1.KVector3.Scalar126 * mv2.Scalar56 + mv1.KVector3.Scalar156 * mv2.Scalar26 - mv1.KVector3.Scalar256 * mv2.Scalar16;
            tempScalar[21] += -mv1.KVector3.Scalar123 * mv2.Scalar25 + mv1.KVector3.Scalar134 * mv2.Scalar45 + mv1.KVector3.Scalar125 * mv2.Scalar23 - mv1.KVector3.Scalar235 * mv2.Scalar12 - mv1.KVector3.Scalar145 * mv2.Scalar34 + mv1.KVector3.Scalar345 * mv2.Scalar14 - mv1.KVector3.Scalar136 * mv2.Scalar56 + mv1.KVector3.Scalar156 * mv2.Scalar36 - mv1.KVector3.Scalar356 * mv2.Scalar16;
            tempScalar[22] += mv1.KVector3.Scalar123 * mv2.Scalar15 + mv1.KVector3.Scalar234 * mv2.Scalar45 - mv1.KVector3.Scalar125 * mv2.Scalar13 + mv1.KVector3.Scalar135 * mv2.Scalar12 - mv1.KVector3.Scalar245 * mv2.Scalar34 + mv1.KVector3.Scalar345 * mv2.Scalar24 - mv1.KVector3.Scalar236 * mv2.Scalar56 + mv1.KVector3.Scalar256 * mv2.Scalar36 - mv1.KVector3.Scalar356 * mv2.Scalar26;
            tempScalar[25] += -mv1.KVector3.Scalar124 * mv2.Scalar25 - mv1.KVector3.Scalar134 * mv2.Scalar35 + mv1.KVector3.Scalar125 * mv2.Scalar24 + mv1.KVector3.Scalar135 * mv2.Scalar34 - mv1.KVector3.Scalar245 * mv2.Scalar12 - mv1.KVector3.Scalar345 * mv2.Scalar13 - mv1.KVector3.Scalar146 * mv2.Scalar56 + mv1.KVector3.Scalar156 * mv2.Scalar46 - mv1.KVector3.Scalar456 * mv2.Scalar16;
            tempScalar[26] += mv1.KVector3.Scalar124 * mv2.Scalar15 - mv1.KVector3.Scalar234 * mv2.Scalar35 - mv1.KVector3.Scalar125 * mv2.Scalar14 + mv1.KVector3.Scalar235 * mv2.Scalar34 + mv1.KVector3.Scalar145 * mv2.Scalar12 - mv1.KVector3.Scalar345 * mv2.Scalar23 - mv1.KVector3.Scalar246 * mv2.Scalar56 + mv1.KVector3.Scalar256 * mv2.Scalar46 - mv1.KVector3.Scalar456 * mv2.Scalar26;
            tempScalar[28] += mv1.KVector3.Scalar134 * mv2.Scalar15 + mv1.KVector3.Scalar234 * mv2.Scalar25 - mv1.KVector3.Scalar135 * mv2.Scalar14 - mv1.KVector3.Scalar235 * mv2.Scalar24 + mv1.KVector3.Scalar145 * mv2.Scalar13 + mv1.KVector3.Scalar245 * mv2.Scalar23 - mv1.KVector3.Scalar346 * mv2.Scalar56 + mv1.KVector3.Scalar356 * mv2.Scalar46 - mv1.KVector3.Scalar456 * mv2.Scalar36;
            tempScalar[35] += mv1.KVector3.Scalar123 * mv2.Scalar36 + mv1.KVector3.Scalar124 * mv2.Scalar46 + mv1.KVector3.Scalar125 * mv2.Scalar56 - mv1.KVector3.Scalar136 * mv2.Scalar23 + mv1.KVector3.Scalar236 * mv2.Scalar13 - mv1.KVector3.Scalar146 * mv2.Scalar24 + mv1.KVector3.Scalar246 * mv2.Scalar14 - mv1.KVector3.Scalar156 * mv2.Scalar25 + mv1.KVector3.Scalar256 * mv2.Scalar15;
            tempScalar[37] += -mv1.KVector3.Scalar123 * mv2.Scalar26 + mv1.KVector3.Scalar134 * mv2.Scalar46 + mv1.KVector3.Scalar135 * mv2.Scalar56 + mv1.KVector3.Scalar126 * mv2.Scalar23 - mv1.KVector3.Scalar236 * mv2.Scalar12 - mv1.KVector3.Scalar146 * mv2.Scalar34 + mv1.KVector3.Scalar346 * mv2.Scalar14 - mv1.KVector3.Scalar156 * mv2.Scalar35 + mv1.KVector3.Scalar356 * mv2.Scalar15;
            tempScalar[38] += mv1.KVector3.Scalar123 * mv2.Scalar16 + mv1.KVector3.Scalar234 * mv2.Scalar46 + mv1.KVector3.Scalar235 * mv2.Scalar56 - mv1.KVector3.Scalar126 * mv2.Scalar13 + mv1.KVector3.Scalar136 * mv2.Scalar12 - mv1.KVector3.Scalar246 * mv2.Scalar34 + mv1.KVector3.Scalar346 * mv2.Scalar24 - mv1.KVector3.Scalar256 * mv2.Scalar35 + mv1.KVector3.Scalar356 * mv2.Scalar25;
            tempScalar[41] += -mv1.KVector3.Scalar124 * mv2.Scalar26 - mv1.KVector3.Scalar134 * mv2.Scalar36 + mv1.KVector3.Scalar145 * mv2.Scalar56 + mv1.KVector3.Scalar126 * mv2.Scalar24 + mv1.KVector3.Scalar136 * mv2.Scalar34 - mv1.KVector3.Scalar246 * mv2.Scalar12 - mv1.KVector3.Scalar346 * mv2.Scalar13 - mv1.KVector3.Scalar156 * mv2.Scalar45 + mv1.KVector3.Scalar456 * mv2.Scalar15;
            tempScalar[42] += mv1.KVector3.Scalar124 * mv2.Scalar16 - mv1.KVector3.Scalar234 * mv2.Scalar36 + mv1.KVector3.Scalar245 * mv2.Scalar56 - mv1.KVector3.Scalar126 * mv2.Scalar14 + mv1.KVector3.Scalar236 * mv2.Scalar34 + mv1.KVector3.Scalar146 * mv2.Scalar12 - mv1.KVector3.Scalar346 * mv2.Scalar23 - mv1.KVector3.Scalar256 * mv2.Scalar45 + mv1.KVector3.Scalar456 * mv2.Scalar25;
            tempScalar[44] += mv1.KVector3.Scalar134 * mv2.Scalar16 + mv1.KVector3.Scalar234 * mv2.Scalar26 + mv1.KVector3.Scalar345 * mv2.Scalar56 - mv1.KVector3.Scalar136 * mv2.Scalar14 - mv1.KVector3.Scalar236 * mv2.Scalar24 + mv1.KVector3.Scalar146 * mv2.Scalar13 + mv1.KVector3.Scalar246 * mv2.Scalar23 - mv1.KVector3.Scalar356 * mv2.Scalar45 + mv1.KVector3.Scalar456 * mv2.Scalar35;
            tempScalar[49] += -mv1.KVector3.Scalar125 * mv2.Scalar26 - mv1.KVector3.Scalar135 * mv2.Scalar36 - mv1.KVector3.Scalar145 * mv2.Scalar46 + mv1.KVector3.Scalar126 * mv2.Scalar25 + mv1.KVector3.Scalar136 * mv2.Scalar35 + mv1.KVector3.Scalar146 * mv2.Scalar45 - mv1.KVector3.Scalar256 * mv2.Scalar12 - mv1.KVector3.Scalar356 * mv2.Scalar13 - mv1.KVector3.Scalar456 * mv2.Scalar14;
            tempScalar[50] += mv1.KVector3.Scalar125 * mv2.Scalar16 - mv1.KVector3.Scalar235 * mv2.Scalar36 - mv1.KVector3.Scalar245 * mv2.Scalar46 - mv1.KVector3.Scalar126 * mv2.Scalar15 + mv1.KVector3.Scalar236 * mv2.Scalar35 + mv1.KVector3.Scalar246 * mv2.Scalar45 + mv1.KVector3.Scalar156 * mv2.Scalar12 - mv1.KVector3.Scalar356 * mv2.Scalar23 - mv1.KVector3.Scalar456 * mv2.Scalar24;
            tempScalar[52] += mv1.KVector3.Scalar135 * mv2.Scalar16 + mv1.KVector3.Scalar235 * mv2.Scalar26 - mv1.KVector3.Scalar345 * mv2.Scalar46 - mv1.KVector3.Scalar136 * mv2.Scalar15 - mv1.KVector3.Scalar236 * mv2.Scalar25 + mv1.KVector3.Scalar346 * mv2.Scalar45 + mv1.KVector3.Scalar156 * mv2.Scalar13 + mv1.KVector3.Scalar256 * mv2.Scalar23 - mv1.KVector3.Scalar456 * mv2.Scalar34;
            tempScalar[56] += mv1.KVector3.Scalar145 * mv2.Scalar16 + mv1.KVector3.Scalar245 * mv2.Scalar26 + mv1.KVector3.Scalar345 * mv2.Scalar36 - mv1.KVector3.Scalar146 * mv2.Scalar15 - mv1.KVector3.Scalar246 * mv2.Scalar25 - mv1.KVector3.Scalar346 * mv2.Scalar35 + mv1.KVector3.Scalar156 * mv2.Scalar14 + mv1.KVector3.Scalar256 * mv2.Scalar24 + mv1.KVector3.Scalar356 * mv2.Scalar34;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += -mv1.KVector4.Scalar1235 * mv2.Scalar45 + mv1.KVector4.Scalar1245 * mv2.Scalar35 - mv1.KVector4.Scalar1345 * mv2.Scalar25 + mv1.KVector4.Scalar2345 * mv2.Scalar15 - mv1.KVector4.Scalar1236 * mv2.Scalar46 + mv1.KVector4.Scalar1246 * mv2.Scalar36 - mv1.KVector4.Scalar1346 * mv2.Scalar26 + mv1.KVector4.Scalar2346 * mv2.Scalar16;
            tempScalar[23] += mv1.KVector4.Scalar1234 * mv2.Scalar45 - mv1.KVector4.Scalar1245 * mv2.Scalar34 + mv1.KVector4.Scalar1345 * mv2.Scalar24 - mv1.KVector4.Scalar2345 * mv2.Scalar14 - mv1.KVector4.Scalar1236 * mv2.Scalar56 + mv1.KVector4.Scalar1256 * mv2.Scalar36 - mv1.KVector4.Scalar1356 * mv2.Scalar26 + mv1.KVector4.Scalar2356 * mv2.Scalar16;
            tempScalar[27] += -mv1.KVector4.Scalar1234 * mv2.Scalar35 + mv1.KVector4.Scalar1235 * mv2.Scalar34 - mv1.KVector4.Scalar1345 * mv2.Scalar23 + mv1.KVector4.Scalar2345 * mv2.Scalar13 - mv1.KVector4.Scalar1246 * mv2.Scalar56 + mv1.KVector4.Scalar1256 * mv2.Scalar46 - mv1.KVector4.Scalar1456 * mv2.Scalar26 + mv1.KVector4.Scalar2456 * mv2.Scalar16;
            tempScalar[29] += mv1.KVector4.Scalar1234 * mv2.Scalar25 - mv1.KVector4.Scalar1235 * mv2.Scalar24 + mv1.KVector4.Scalar1245 * mv2.Scalar23 - mv1.KVector4.Scalar2345 * mv2.Scalar12 - mv1.KVector4.Scalar1346 * mv2.Scalar56 + mv1.KVector4.Scalar1356 * mv2.Scalar46 - mv1.KVector4.Scalar1456 * mv2.Scalar36 + mv1.KVector4.Scalar3456 * mv2.Scalar16;
            tempScalar[30] += -mv1.KVector4.Scalar1234 * mv2.Scalar15 + mv1.KVector4.Scalar1235 * mv2.Scalar14 - mv1.KVector4.Scalar1245 * mv2.Scalar13 + mv1.KVector4.Scalar1345 * mv2.Scalar12 - mv1.KVector4.Scalar2346 * mv2.Scalar56 + mv1.KVector4.Scalar2356 * mv2.Scalar46 - mv1.KVector4.Scalar2456 * mv2.Scalar36 + mv1.KVector4.Scalar3456 * mv2.Scalar26;
            tempScalar[39] += mv1.KVector4.Scalar1234 * mv2.Scalar46 + mv1.KVector4.Scalar1235 * mv2.Scalar56 - mv1.KVector4.Scalar1246 * mv2.Scalar34 + mv1.KVector4.Scalar1346 * mv2.Scalar24 - mv1.KVector4.Scalar2346 * mv2.Scalar14 - mv1.KVector4.Scalar1256 * mv2.Scalar35 + mv1.KVector4.Scalar1356 * mv2.Scalar25 - mv1.KVector4.Scalar2356 * mv2.Scalar15;
            tempScalar[43] += -mv1.KVector4.Scalar1234 * mv2.Scalar36 + mv1.KVector4.Scalar1245 * mv2.Scalar56 + mv1.KVector4.Scalar1236 * mv2.Scalar34 - mv1.KVector4.Scalar1346 * mv2.Scalar23 + mv1.KVector4.Scalar2346 * mv2.Scalar13 - mv1.KVector4.Scalar1256 * mv2.Scalar45 + mv1.KVector4.Scalar1456 * mv2.Scalar25 - mv1.KVector4.Scalar2456 * mv2.Scalar15;
            tempScalar[45] += mv1.KVector4.Scalar1234 * mv2.Scalar26 + mv1.KVector4.Scalar1345 * mv2.Scalar56 - mv1.KVector4.Scalar1236 * mv2.Scalar24 + mv1.KVector4.Scalar1246 * mv2.Scalar23 - mv1.KVector4.Scalar2346 * mv2.Scalar12 - mv1.KVector4.Scalar1356 * mv2.Scalar45 + mv1.KVector4.Scalar1456 * mv2.Scalar35 - mv1.KVector4.Scalar3456 * mv2.Scalar15;
            tempScalar[46] += -mv1.KVector4.Scalar1234 * mv2.Scalar16 + mv1.KVector4.Scalar2345 * mv2.Scalar56 + mv1.KVector4.Scalar1236 * mv2.Scalar14 - mv1.KVector4.Scalar1246 * mv2.Scalar13 + mv1.KVector4.Scalar1346 * mv2.Scalar12 - mv1.KVector4.Scalar2356 * mv2.Scalar45 + mv1.KVector4.Scalar2456 * mv2.Scalar35 - mv1.KVector4.Scalar3456 * mv2.Scalar25;
            tempScalar[51] += -mv1.KVector4.Scalar1235 * mv2.Scalar36 - mv1.KVector4.Scalar1245 * mv2.Scalar46 + mv1.KVector4.Scalar1236 * mv2.Scalar35 + mv1.KVector4.Scalar1246 * mv2.Scalar45 - mv1.KVector4.Scalar1356 * mv2.Scalar23 + mv1.KVector4.Scalar2356 * mv2.Scalar13 - mv1.KVector4.Scalar1456 * mv2.Scalar24 + mv1.KVector4.Scalar2456 * mv2.Scalar14;
            tempScalar[53] += mv1.KVector4.Scalar1235 * mv2.Scalar26 - mv1.KVector4.Scalar1345 * mv2.Scalar46 - mv1.KVector4.Scalar1236 * mv2.Scalar25 + mv1.KVector4.Scalar1346 * mv2.Scalar45 + mv1.KVector4.Scalar1256 * mv2.Scalar23 - mv1.KVector4.Scalar2356 * mv2.Scalar12 - mv1.KVector4.Scalar1456 * mv2.Scalar34 + mv1.KVector4.Scalar3456 * mv2.Scalar14;
            tempScalar[54] += -mv1.KVector4.Scalar1235 * mv2.Scalar16 - mv1.KVector4.Scalar2345 * mv2.Scalar46 + mv1.KVector4.Scalar1236 * mv2.Scalar15 + mv1.KVector4.Scalar2346 * mv2.Scalar45 - mv1.KVector4.Scalar1256 * mv2.Scalar13 + mv1.KVector4.Scalar1356 * mv2.Scalar12 - mv1.KVector4.Scalar2456 * mv2.Scalar34 + mv1.KVector4.Scalar3456 * mv2.Scalar24;
            tempScalar[57] += mv1.KVector4.Scalar1245 * mv2.Scalar26 + mv1.KVector4.Scalar1345 * mv2.Scalar36 - mv1.KVector4.Scalar1246 * mv2.Scalar25 - mv1.KVector4.Scalar1346 * mv2.Scalar35 + mv1.KVector4.Scalar1256 * mv2.Scalar24 + mv1.KVector4.Scalar1356 * mv2.Scalar34 - mv1.KVector4.Scalar2456 * mv2.Scalar12 - mv1.KVector4.Scalar3456 * mv2.Scalar13;
            tempScalar[58] += -mv1.KVector4.Scalar1245 * mv2.Scalar16 + mv1.KVector4.Scalar2345 * mv2.Scalar36 + mv1.KVector4.Scalar1246 * mv2.Scalar15 - mv1.KVector4.Scalar2346 * mv2.Scalar35 - mv1.KVector4.Scalar1256 * mv2.Scalar14 + mv1.KVector4.Scalar2356 * mv2.Scalar34 + mv1.KVector4.Scalar1456 * mv2.Scalar12 - mv1.KVector4.Scalar3456 * mv2.Scalar23;
            tempScalar[60] += -mv1.KVector4.Scalar1345 * mv2.Scalar16 - mv1.KVector4.Scalar2345 * mv2.Scalar26 + mv1.KVector4.Scalar1346 * mv2.Scalar15 + mv1.KVector4.Scalar2346 * mv2.Scalar25 - mv1.KVector4.Scalar1356 * mv2.Scalar14 - mv1.KVector4.Scalar2356 * mv2.Scalar24 + mv1.KVector4.Scalar1456 * mv2.Scalar13 + mv1.KVector4.Scalar2456 * mv2.Scalar23;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[31] += -mv1.KVector5.Scalar12346 * mv2.Scalar56 + mv1.KVector5.Scalar12356 * mv2.Scalar46 - mv1.KVector5.Scalar12456 * mv2.Scalar36 + mv1.KVector5.Scalar13456 * mv2.Scalar26 - mv1.KVector5.Scalar23456 * mv2.Scalar16;
            tempScalar[47] += mv1.KVector5.Scalar12345 * mv2.Scalar56 - mv1.KVector5.Scalar12356 * mv2.Scalar45 + mv1.KVector5.Scalar12456 * mv2.Scalar35 - mv1.KVector5.Scalar13456 * mv2.Scalar25 + mv1.KVector5.Scalar23456 * mv2.Scalar15;
            tempScalar[55] += -mv1.KVector5.Scalar12345 * mv2.Scalar46 + mv1.KVector5.Scalar12346 * mv2.Scalar45 - mv1.KVector5.Scalar12456 * mv2.Scalar34 + mv1.KVector5.Scalar13456 * mv2.Scalar24 - mv1.KVector5.Scalar23456 * mv2.Scalar14;
            tempScalar[59] += mv1.KVector5.Scalar12345 * mv2.Scalar36 - mv1.KVector5.Scalar12346 * mv2.Scalar35 + mv1.KVector5.Scalar12356 * mv2.Scalar34 - mv1.KVector5.Scalar13456 * mv2.Scalar23 + mv1.KVector5.Scalar23456 * mv2.Scalar13;
            tempScalar[61] += -mv1.KVector5.Scalar12345 * mv2.Scalar26 + mv1.KVector5.Scalar12346 * mv2.Scalar25 - mv1.KVector5.Scalar12356 * mv2.Scalar24 + mv1.KVector5.Scalar12456 * mv2.Scalar23 - mv1.KVector5.Scalar23456 * mv2.Scalar12;
            tempScalar[62] += mv1.KVector5.Scalar12345 * mv2.Scalar16 - mv1.KVector5.Scalar12346 * mv2.Scalar15 + mv1.KVector5.Scalar12356 * mv2.Scalar14 - mv1.KVector5.Scalar12456 * mv2.Scalar13 + mv1.KVector5.Scalar13456 * mv2.Scalar12;
        }
        
        return Ga6Multivector.Create(tempScalar);
    }
    
    public static Ga6Multivector Cp(this Ga6Multivector mv1, Ga6KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[15] += mv1.KVector1.Scalar1 * mv2.Scalar234 - mv1.KVector1.Scalar2 * mv2.Scalar134 + mv1.KVector1.Scalar3 * mv2.Scalar124 - mv1.KVector1.Scalar4 * mv2.Scalar123;
            tempScalar[23] += mv1.KVector1.Scalar1 * mv2.Scalar235 - mv1.KVector1.Scalar2 * mv2.Scalar135 + mv1.KVector1.Scalar3 * mv2.Scalar125 - mv1.KVector1.Scalar5 * mv2.Scalar123;
            tempScalar[27] += mv1.KVector1.Scalar1 * mv2.Scalar245 - mv1.KVector1.Scalar2 * mv2.Scalar145 + mv1.KVector1.Scalar4 * mv2.Scalar125 - mv1.KVector1.Scalar5 * mv2.Scalar124;
            tempScalar[29] += mv1.KVector1.Scalar1 * mv2.Scalar345 - mv1.KVector1.Scalar3 * mv2.Scalar145 + mv1.KVector1.Scalar4 * mv2.Scalar135 - mv1.KVector1.Scalar5 * mv2.Scalar134;
            tempScalar[30] += mv1.KVector1.Scalar2 * mv2.Scalar345 - mv1.KVector1.Scalar3 * mv2.Scalar245 + mv1.KVector1.Scalar4 * mv2.Scalar235 - mv1.KVector1.Scalar5 * mv2.Scalar234;
            tempScalar[39] += mv1.KVector1.Scalar1 * mv2.Scalar236 - mv1.KVector1.Scalar2 * mv2.Scalar136 + mv1.KVector1.Scalar3 * mv2.Scalar126 - mv1.KVector1.Scalar6 * mv2.Scalar123;
            tempScalar[43] += mv1.KVector1.Scalar1 * mv2.Scalar246 - mv1.KVector1.Scalar2 * mv2.Scalar146 + mv1.KVector1.Scalar4 * mv2.Scalar126 - mv1.KVector1.Scalar6 * mv2.Scalar124;
            tempScalar[45] += mv1.KVector1.Scalar1 * mv2.Scalar346 - mv1.KVector1.Scalar3 * mv2.Scalar146 + mv1.KVector1.Scalar4 * mv2.Scalar136 - mv1.KVector1.Scalar6 * mv2.Scalar134;
            tempScalar[46] += mv1.KVector1.Scalar2 * mv2.Scalar346 - mv1.KVector1.Scalar3 * mv2.Scalar246 + mv1.KVector1.Scalar4 * mv2.Scalar236 - mv1.KVector1.Scalar6 * mv2.Scalar234;
            tempScalar[51] += mv1.KVector1.Scalar1 * mv2.Scalar256 - mv1.KVector1.Scalar2 * mv2.Scalar156 + mv1.KVector1.Scalar5 * mv2.Scalar126 - mv1.KVector1.Scalar6 * mv2.Scalar125;
            tempScalar[53] += mv1.KVector1.Scalar1 * mv2.Scalar356 - mv1.KVector1.Scalar3 * mv2.Scalar156 + mv1.KVector1.Scalar5 * mv2.Scalar136 - mv1.KVector1.Scalar6 * mv2.Scalar135;
            tempScalar[54] += mv1.KVector1.Scalar2 * mv2.Scalar356 - mv1.KVector1.Scalar3 * mv2.Scalar256 + mv1.KVector1.Scalar5 * mv2.Scalar236 - mv1.KVector1.Scalar6 * mv2.Scalar235;
            tempScalar[57] += mv1.KVector1.Scalar1 * mv2.Scalar456 - mv1.KVector1.Scalar4 * mv2.Scalar156 + mv1.KVector1.Scalar5 * mv2.Scalar146 - mv1.KVector1.Scalar6 * mv2.Scalar145;
            tempScalar[58] += mv1.KVector1.Scalar2 * mv2.Scalar456 - mv1.KVector1.Scalar4 * mv2.Scalar256 + mv1.KVector1.Scalar5 * mv2.Scalar246 - mv1.KVector1.Scalar6 * mv2.Scalar245;
            tempScalar[60] += mv1.KVector1.Scalar3 * mv2.Scalar456 - mv1.KVector1.Scalar4 * mv2.Scalar356 + mv1.KVector1.Scalar5 * mv2.Scalar346 - mv1.KVector1.Scalar6 * mv2.Scalar345;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[7] += mv1.KVector2.Scalar14 * mv2.Scalar234 - mv1.KVector2.Scalar24 * mv2.Scalar134 + mv1.KVector2.Scalar34 * mv2.Scalar124 + mv1.KVector2.Scalar15 * mv2.Scalar235 - mv1.KVector2.Scalar25 * mv2.Scalar135 + mv1.KVector2.Scalar35 * mv2.Scalar125 + mv1.KVector2.Scalar16 * mv2.Scalar236 - mv1.KVector2.Scalar26 * mv2.Scalar136 + mv1.KVector2.Scalar36 * mv2.Scalar126;
            tempScalar[11] += -mv1.KVector2.Scalar13 * mv2.Scalar234 + mv1.KVector2.Scalar23 * mv2.Scalar134 - mv1.KVector2.Scalar34 * mv2.Scalar123 + mv1.KVector2.Scalar15 * mv2.Scalar245 - mv1.KVector2.Scalar25 * mv2.Scalar145 + mv1.KVector2.Scalar45 * mv2.Scalar125 + mv1.KVector2.Scalar16 * mv2.Scalar246 - mv1.KVector2.Scalar26 * mv2.Scalar146 + mv1.KVector2.Scalar46 * mv2.Scalar126;
            tempScalar[13] += mv1.KVector2.Scalar12 * mv2.Scalar234 - mv1.KVector2.Scalar23 * mv2.Scalar124 + mv1.KVector2.Scalar24 * mv2.Scalar123 + mv1.KVector2.Scalar15 * mv2.Scalar345 - mv1.KVector2.Scalar35 * mv2.Scalar145 + mv1.KVector2.Scalar45 * mv2.Scalar135 + mv1.KVector2.Scalar16 * mv2.Scalar346 - mv1.KVector2.Scalar36 * mv2.Scalar146 + mv1.KVector2.Scalar46 * mv2.Scalar136;
            tempScalar[14] += -mv1.KVector2.Scalar12 * mv2.Scalar134 + mv1.KVector2.Scalar13 * mv2.Scalar124 - mv1.KVector2.Scalar14 * mv2.Scalar123 + mv1.KVector2.Scalar25 * mv2.Scalar345 - mv1.KVector2.Scalar35 * mv2.Scalar245 + mv1.KVector2.Scalar45 * mv2.Scalar235 + mv1.KVector2.Scalar26 * mv2.Scalar346 - mv1.KVector2.Scalar36 * mv2.Scalar246 + mv1.KVector2.Scalar46 * mv2.Scalar236;
            tempScalar[19] += -mv1.KVector2.Scalar13 * mv2.Scalar235 + mv1.KVector2.Scalar23 * mv2.Scalar135 - mv1.KVector2.Scalar14 * mv2.Scalar245 + mv1.KVector2.Scalar24 * mv2.Scalar145 - mv1.KVector2.Scalar35 * mv2.Scalar123 - mv1.KVector2.Scalar45 * mv2.Scalar124 + mv1.KVector2.Scalar16 * mv2.Scalar256 - mv1.KVector2.Scalar26 * mv2.Scalar156 + mv1.KVector2.Scalar56 * mv2.Scalar126;
            tempScalar[21] += mv1.KVector2.Scalar12 * mv2.Scalar235 - mv1.KVector2.Scalar23 * mv2.Scalar125 - mv1.KVector2.Scalar14 * mv2.Scalar345 + mv1.KVector2.Scalar34 * mv2.Scalar145 + mv1.KVector2.Scalar25 * mv2.Scalar123 - mv1.KVector2.Scalar45 * mv2.Scalar134 + mv1.KVector2.Scalar16 * mv2.Scalar356 - mv1.KVector2.Scalar36 * mv2.Scalar156 + mv1.KVector2.Scalar56 * mv2.Scalar136;
            tempScalar[22] += -mv1.KVector2.Scalar12 * mv2.Scalar135 + mv1.KVector2.Scalar13 * mv2.Scalar125 - mv1.KVector2.Scalar24 * mv2.Scalar345 + mv1.KVector2.Scalar34 * mv2.Scalar245 - mv1.KVector2.Scalar15 * mv2.Scalar123 - mv1.KVector2.Scalar45 * mv2.Scalar234 + mv1.KVector2.Scalar26 * mv2.Scalar356 - mv1.KVector2.Scalar36 * mv2.Scalar256 + mv1.KVector2.Scalar56 * mv2.Scalar236;
            tempScalar[25] += mv1.KVector2.Scalar12 * mv2.Scalar245 + mv1.KVector2.Scalar13 * mv2.Scalar345 - mv1.KVector2.Scalar24 * mv2.Scalar125 - mv1.KVector2.Scalar34 * mv2.Scalar135 + mv1.KVector2.Scalar25 * mv2.Scalar124 + mv1.KVector2.Scalar35 * mv2.Scalar134 + mv1.KVector2.Scalar16 * mv2.Scalar456 - mv1.KVector2.Scalar46 * mv2.Scalar156 + mv1.KVector2.Scalar56 * mv2.Scalar146;
            tempScalar[26] += -mv1.KVector2.Scalar12 * mv2.Scalar145 + mv1.KVector2.Scalar23 * mv2.Scalar345 + mv1.KVector2.Scalar14 * mv2.Scalar125 - mv1.KVector2.Scalar34 * mv2.Scalar235 - mv1.KVector2.Scalar15 * mv2.Scalar124 + mv1.KVector2.Scalar35 * mv2.Scalar234 + mv1.KVector2.Scalar26 * mv2.Scalar456 - mv1.KVector2.Scalar46 * mv2.Scalar256 + mv1.KVector2.Scalar56 * mv2.Scalar246;
            tempScalar[28] += -mv1.KVector2.Scalar13 * mv2.Scalar145 - mv1.KVector2.Scalar23 * mv2.Scalar245 + mv1.KVector2.Scalar14 * mv2.Scalar135 + mv1.KVector2.Scalar24 * mv2.Scalar235 - mv1.KVector2.Scalar15 * mv2.Scalar134 - mv1.KVector2.Scalar25 * mv2.Scalar234 + mv1.KVector2.Scalar36 * mv2.Scalar456 - mv1.KVector2.Scalar46 * mv2.Scalar356 + mv1.KVector2.Scalar56 * mv2.Scalar346;
            tempScalar[35] += -mv1.KVector2.Scalar13 * mv2.Scalar236 + mv1.KVector2.Scalar23 * mv2.Scalar136 - mv1.KVector2.Scalar14 * mv2.Scalar246 + mv1.KVector2.Scalar24 * mv2.Scalar146 - mv1.KVector2.Scalar15 * mv2.Scalar256 + mv1.KVector2.Scalar25 * mv2.Scalar156 - mv1.KVector2.Scalar36 * mv2.Scalar123 - mv1.KVector2.Scalar46 * mv2.Scalar124 - mv1.KVector2.Scalar56 * mv2.Scalar125;
            tempScalar[37] += mv1.KVector2.Scalar12 * mv2.Scalar236 - mv1.KVector2.Scalar23 * mv2.Scalar126 - mv1.KVector2.Scalar14 * mv2.Scalar346 + mv1.KVector2.Scalar34 * mv2.Scalar146 - mv1.KVector2.Scalar15 * mv2.Scalar356 + mv1.KVector2.Scalar35 * mv2.Scalar156 + mv1.KVector2.Scalar26 * mv2.Scalar123 - mv1.KVector2.Scalar46 * mv2.Scalar134 - mv1.KVector2.Scalar56 * mv2.Scalar135;
            tempScalar[38] += -mv1.KVector2.Scalar12 * mv2.Scalar136 + mv1.KVector2.Scalar13 * mv2.Scalar126 - mv1.KVector2.Scalar24 * mv2.Scalar346 + mv1.KVector2.Scalar34 * mv2.Scalar246 - mv1.KVector2.Scalar25 * mv2.Scalar356 + mv1.KVector2.Scalar35 * mv2.Scalar256 - mv1.KVector2.Scalar16 * mv2.Scalar123 - mv1.KVector2.Scalar46 * mv2.Scalar234 - mv1.KVector2.Scalar56 * mv2.Scalar235;
            tempScalar[41] += mv1.KVector2.Scalar12 * mv2.Scalar246 + mv1.KVector2.Scalar13 * mv2.Scalar346 - mv1.KVector2.Scalar24 * mv2.Scalar126 - mv1.KVector2.Scalar34 * mv2.Scalar136 - mv1.KVector2.Scalar15 * mv2.Scalar456 + mv1.KVector2.Scalar45 * mv2.Scalar156 + mv1.KVector2.Scalar26 * mv2.Scalar124 + mv1.KVector2.Scalar36 * mv2.Scalar134 - mv1.KVector2.Scalar56 * mv2.Scalar145;
            tempScalar[42] += -mv1.KVector2.Scalar12 * mv2.Scalar146 + mv1.KVector2.Scalar23 * mv2.Scalar346 + mv1.KVector2.Scalar14 * mv2.Scalar126 - mv1.KVector2.Scalar34 * mv2.Scalar236 - mv1.KVector2.Scalar25 * mv2.Scalar456 + mv1.KVector2.Scalar45 * mv2.Scalar256 - mv1.KVector2.Scalar16 * mv2.Scalar124 + mv1.KVector2.Scalar36 * mv2.Scalar234 - mv1.KVector2.Scalar56 * mv2.Scalar245;
            tempScalar[44] += -mv1.KVector2.Scalar13 * mv2.Scalar146 - mv1.KVector2.Scalar23 * mv2.Scalar246 + mv1.KVector2.Scalar14 * mv2.Scalar136 + mv1.KVector2.Scalar24 * mv2.Scalar236 - mv1.KVector2.Scalar35 * mv2.Scalar456 + mv1.KVector2.Scalar45 * mv2.Scalar356 - mv1.KVector2.Scalar16 * mv2.Scalar134 - mv1.KVector2.Scalar26 * mv2.Scalar234 - mv1.KVector2.Scalar56 * mv2.Scalar345;
            tempScalar[49] += mv1.KVector2.Scalar12 * mv2.Scalar256 + mv1.KVector2.Scalar13 * mv2.Scalar356 + mv1.KVector2.Scalar14 * mv2.Scalar456 - mv1.KVector2.Scalar25 * mv2.Scalar126 - mv1.KVector2.Scalar35 * mv2.Scalar136 - mv1.KVector2.Scalar45 * mv2.Scalar146 + mv1.KVector2.Scalar26 * mv2.Scalar125 + mv1.KVector2.Scalar36 * mv2.Scalar135 + mv1.KVector2.Scalar46 * mv2.Scalar145;
            tempScalar[50] += -mv1.KVector2.Scalar12 * mv2.Scalar156 + mv1.KVector2.Scalar23 * mv2.Scalar356 + mv1.KVector2.Scalar24 * mv2.Scalar456 + mv1.KVector2.Scalar15 * mv2.Scalar126 - mv1.KVector2.Scalar35 * mv2.Scalar236 - mv1.KVector2.Scalar45 * mv2.Scalar246 - mv1.KVector2.Scalar16 * mv2.Scalar125 + mv1.KVector2.Scalar36 * mv2.Scalar235 + mv1.KVector2.Scalar46 * mv2.Scalar245;
            tempScalar[52] += -mv1.KVector2.Scalar13 * mv2.Scalar156 - mv1.KVector2.Scalar23 * mv2.Scalar256 + mv1.KVector2.Scalar34 * mv2.Scalar456 + mv1.KVector2.Scalar15 * mv2.Scalar136 + mv1.KVector2.Scalar25 * mv2.Scalar236 - mv1.KVector2.Scalar45 * mv2.Scalar346 - mv1.KVector2.Scalar16 * mv2.Scalar135 - mv1.KVector2.Scalar26 * mv2.Scalar235 + mv1.KVector2.Scalar46 * mv2.Scalar345;
            tempScalar[56] += -mv1.KVector2.Scalar14 * mv2.Scalar156 - mv1.KVector2.Scalar24 * mv2.Scalar256 - mv1.KVector2.Scalar34 * mv2.Scalar356 + mv1.KVector2.Scalar15 * mv2.Scalar146 + mv1.KVector2.Scalar25 * mv2.Scalar246 + mv1.KVector2.Scalar35 * mv2.Scalar346 - mv1.KVector2.Scalar16 * mv2.Scalar145 - mv1.KVector2.Scalar26 * mv2.Scalar245 - mv1.KVector2.Scalar36 * mv2.Scalar345;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[3] += -mv1.KVector3.Scalar134 * mv2.Scalar234 + mv1.KVector3.Scalar234 * mv2.Scalar134 - mv1.KVector3.Scalar135 * mv2.Scalar235 + mv1.KVector3.Scalar235 * mv2.Scalar135 - mv1.KVector3.Scalar145 * mv2.Scalar245 + mv1.KVector3.Scalar245 * mv2.Scalar145 - mv1.KVector3.Scalar136 * mv2.Scalar236 + mv1.KVector3.Scalar236 * mv2.Scalar136 - mv1.KVector3.Scalar146 * mv2.Scalar246 + mv1.KVector3.Scalar246 * mv2.Scalar146 - mv1.KVector3.Scalar156 * mv2.Scalar256 + mv1.KVector3.Scalar256 * mv2.Scalar156;
            tempScalar[5] += mv1.KVector3.Scalar124 * mv2.Scalar234 - mv1.KVector3.Scalar234 * mv2.Scalar124 + mv1.KVector3.Scalar125 * mv2.Scalar235 - mv1.KVector3.Scalar235 * mv2.Scalar125 - mv1.KVector3.Scalar145 * mv2.Scalar345 + mv1.KVector3.Scalar345 * mv2.Scalar145 + mv1.KVector3.Scalar126 * mv2.Scalar236 - mv1.KVector3.Scalar236 * mv2.Scalar126 - mv1.KVector3.Scalar146 * mv2.Scalar346 + mv1.KVector3.Scalar346 * mv2.Scalar146 - mv1.KVector3.Scalar156 * mv2.Scalar356 + mv1.KVector3.Scalar356 * mv2.Scalar156;
            tempScalar[6] += -mv1.KVector3.Scalar124 * mv2.Scalar134 + mv1.KVector3.Scalar134 * mv2.Scalar124 - mv1.KVector3.Scalar125 * mv2.Scalar135 + mv1.KVector3.Scalar135 * mv2.Scalar125 - mv1.KVector3.Scalar245 * mv2.Scalar345 + mv1.KVector3.Scalar345 * mv2.Scalar245 - mv1.KVector3.Scalar126 * mv2.Scalar136 + mv1.KVector3.Scalar136 * mv2.Scalar126 - mv1.KVector3.Scalar246 * mv2.Scalar346 + mv1.KVector3.Scalar346 * mv2.Scalar246 - mv1.KVector3.Scalar256 * mv2.Scalar356 + mv1.KVector3.Scalar356 * mv2.Scalar256;
            tempScalar[9] += -mv1.KVector3.Scalar123 * mv2.Scalar234 + mv1.KVector3.Scalar234 * mv2.Scalar123 + mv1.KVector3.Scalar125 * mv2.Scalar245 + mv1.KVector3.Scalar135 * mv2.Scalar345 - mv1.KVector3.Scalar245 * mv2.Scalar125 - mv1.KVector3.Scalar345 * mv2.Scalar135 + mv1.KVector3.Scalar126 * mv2.Scalar246 + mv1.KVector3.Scalar136 * mv2.Scalar346 - mv1.KVector3.Scalar246 * mv2.Scalar126 - mv1.KVector3.Scalar346 * mv2.Scalar136 - mv1.KVector3.Scalar156 * mv2.Scalar456 + mv1.KVector3.Scalar456 * mv2.Scalar156;
            tempScalar[10] += mv1.KVector3.Scalar123 * mv2.Scalar134 - mv1.KVector3.Scalar134 * mv2.Scalar123 - mv1.KVector3.Scalar125 * mv2.Scalar145 + mv1.KVector3.Scalar235 * mv2.Scalar345 + mv1.KVector3.Scalar145 * mv2.Scalar125 - mv1.KVector3.Scalar345 * mv2.Scalar235 - mv1.KVector3.Scalar126 * mv2.Scalar146 + mv1.KVector3.Scalar236 * mv2.Scalar346 + mv1.KVector3.Scalar146 * mv2.Scalar126 - mv1.KVector3.Scalar346 * mv2.Scalar236 - mv1.KVector3.Scalar256 * mv2.Scalar456 + mv1.KVector3.Scalar456 * mv2.Scalar256;
            tempScalar[12] += -mv1.KVector3.Scalar123 * mv2.Scalar124 + mv1.KVector3.Scalar124 * mv2.Scalar123 - mv1.KVector3.Scalar135 * mv2.Scalar145 - mv1.KVector3.Scalar235 * mv2.Scalar245 + mv1.KVector3.Scalar145 * mv2.Scalar135 + mv1.KVector3.Scalar245 * mv2.Scalar235 - mv1.KVector3.Scalar136 * mv2.Scalar146 - mv1.KVector3.Scalar236 * mv2.Scalar246 + mv1.KVector3.Scalar146 * mv2.Scalar136 + mv1.KVector3.Scalar246 * mv2.Scalar236 - mv1.KVector3.Scalar356 * mv2.Scalar456 + mv1.KVector3.Scalar456 * mv2.Scalar356;
            tempScalar[17] += -mv1.KVector3.Scalar123 * mv2.Scalar235 - mv1.KVector3.Scalar124 * mv2.Scalar245 - mv1.KVector3.Scalar134 * mv2.Scalar345 + mv1.KVector3.Scalar235 * mv2.Scalar123 + mv1.KVector3.Scalar245 * mv2.Scalar124 + mv1.KVector3.Scalar345 * mv2.Scalar134 + mv1.KVector3.Scalar126 * mv2.Scalar256 + mv1.KVector3.Scalar136 * mv2.Scalar356 + mv1.KVector3.Scalar146 * mv2.Scalar456 - mv1.KVector3.Scalar256 * mv2.Scalar126 - mv1.KVector3.Scalar356 * mv2.Scalar136 - mv1.KVector3.Scalar456 * mv2.Scalar146;
            tempScalar[18] += mv1.KVector3.Scalar123 * mv2.Scalar135 + mv1.KVector3.Scalar124 * mv2.Scalar145 - mv1.KVector3.Scalar234 * mv2.Scalar345 - mv1.KVector3.Scalar135 * mv2.Scalar123 - mv1.KVector3.Scalar145 * mv2.Scalar124 + mv1.KVector3.Scalar345 * mv2.Scalar234 - mv1.KVector3.Scalar126 * mv2.Scalar156 + mv1.KVector3.Scalar236 * mv2.Scalar356 + mv1.KVector3.Scalar246 * mv2.Scalar456 + mv1.KVector3.Scalar156 * mv2.Scalar126 - mv1.KVector3.Scalar356 * mv2.Scalar236 - mv1.KVector3.Scalar456 * mv2.Scalar246;
            tempScalar[20] += -mv1.KVector3.Scalar123 * mv2.Scalar125 + mv1.KVector3.Scalar134 * mv2.Scalar145 + mv1.KVector3.Scalar234 * mv2.Scalar245 + mv1.KVector3.Scalar125 * mv2.Scalar123 - mv1.KVector3.Scalar145 * mv2.Scalar134 - mv1.KVector3.Scalar245 * mv2.Scalar234 - mv1.KVector3.Scalar136 * mv2.Scalar156 - mv1.KVector3.Scalar236 * mv2.Scalar256 + mv1.KVector3.Scalar346 * mv2.Scalar456 + mv1.KVector3.Scalar156 * mv2.Scalar136 + mv1.KVector3.Scalar256 * mv2.Scalar236 - mv1.KVector3.Scalar456 * mv2.Scalar346;
            tempScalar[24] += -mv1.KVector3.Scalar124 * mv2.Scalar125 - mv1.KVector3.Scalar134 * mv2.Scalar135 - mv1.KVector3.Scalar234 * mv2.Scalar235 + mv1.KVector3.Scalar125 * mv2.Scalar124 + mv1.KVector3.Scalar135 * mv2.Scalar134 + mv1.KVector3.Scalar235 * mv2.Scalar234 - mv1.KVector3.Scalar146 * mv2.Scalar156 - mv1.KVector3.Scalar246 * mv2.Scalar256 - mv1.KVector3.Scalar346 * mv2.Scalar356 + mv1.KVector3.Scalar156 * mv2.Scalar146 + mv1.KVector3.Scalar256 * mv2.Scalar246 + mv1.KVector3.Scalar356 * mv2.Scalar346;
            tempScalar[33] += -mv1.KVector3.Scalar123 * mv2.Scalar236 - mv1.KVector3.Scalar124 * mv2.Scalar246 - mv1.KVector3.Scalar134 * mv2.Scalar346 - mv1.KVector3.Scalar125 * mv2.Scalar256 - mv1.KVector3.Scalar135 * mv2.Scalar356 - mv1.KVector3.Scalar145 * mv2.Scalar456 + mv1.KVector3.Scalar236 * mv2.Scalar123 + mv1.KVector3.Scalar246 * mv2.Scalar124 + mv1.KVector3.Scalar346 * mv2.Scalar134 + mv1.KVector3.Scalar256 * mv2.Scalar125 + mv1.KVector3.Scalar356 * mv2.Scalar135 + mv1.KVector3.Scalar456 * mv2.Scalar145;
            tempScalar[34] += mv1.KVector3.Scalar123 * mv2.Scalar136 + mv1.KVector3.Scalar124 * mv2.Scalar146 - mv1.KVector3.Scalar234 * mv2.Scalar346 + mv1.KVector3.Scalar125 * mv2.Scalar156 - mv1.KVector3.Scalar235 * mv2.Scalar356 - mv1.KVector3.Scalar245 * mv2.Scalar456 - mv1.KVector3.Scalar136 * mv2.Scalar123 - mv1.KVector3.Scalar146 * mv2.Scalar124 + mv1.KVector3.Scalar346 * mv2.Scalar234 - mv1.KVector3.Scalar156 * mv2.Scalar125 + mv1.KVector3.Scalar356 * mv2.Scalar235 + mv1.KVector3.Scalar456 * mv2.Scalar245;
            tempScalar[36] += -mv1.KVector3.Scalar123 * mv2.Scalar126 + mv1.KVector3.Scalar134 * mv2.Scalar146 + mv1.KVector3.Scalar234 * mv2.Scalar246 + mv1.KVector3.Scalar135 * mv2.Scalar156 + mv1.KVector3.Scalar235 * mv2.Scalar256 - mv1.KVector3.Scalar345 * mv2.Scalar456 + mv1.KVector3.Scalar126 * mv2.Scalar123 - mv1.KVector3.Scalar146 * mv2.Scalar134 - mv1.KVector3.Scalar246 * mv2.Scalar234 - mv1.KVector3.Scalar156 * mv2.Scalar135 - mv1.KVector3.Scalar256 * mv2.Scalar235 + mv1.KVector3.Scalar456 * mv2.Scalar345;
            tempScalar[40] += -mv1.KVector3.Scalar124 * mv2.Scalar126 - mv1.KVector3.Scalar134 * mv2.Scalar136 - mv1.KVector3.Scalar234 * mv2.Scalar236 + mv1.KVector3.Scalar145 * mv2.Scalar156 + mv1.KVector3.Scalar245 * mv2.Scalar256 + mv1.KVector3.Scalar345 * mv2.Scalar356 + mv1.KVector3.Scalar126 * mv2.Scalar124 + mv1.KVector3.Scalar136 * mv2.Scalar134 + mv1.KVector3.Scalar236 * mv2.Scalar234 - mv1.KVector3.Scalar156 * mv2.Scalar145 - mv1.KVector3.Scalar256 * mv2.Scalar245 - mv1.KVector3.Scalar356 * mv2.Scalar345;
            tempScalar[48] += -mv1.KVector3.Scalar125 * mv2.Scalar126 - mv1.KVector3.Scalar135 * mv2.Scalar136 - mv1.KVector3.Scalar235 * mv2.Scalar236 - mv1.KVector3.Scalar145 * mv2.Scalar146 - mv1.KVector3.Scalar245 * mv2.Scalar246 - mv1.KVector3.Scalar345 * mv2.Scalar346 + mv1.KVector3.Scalar126 * mv2.Scalar125 + mv1.KVector3.Scalar136 * mv2.Scalar135 + mv1.KVector3.Scalar236 * mv2.Scalar235 + mv1.KVector3.Scalar146 * mv2.Scalar145 + mv1.KVector3.Scalar246 * mv2.Scalar245 + mv1.KVector3.Scalar346 * mv2.Scalar345;
            tempScalar[63] += mv1.KVector3.Scalar123 * mv2.Scalar456 - mv1.KVector3.Scalar124 * mv2.Scalar356 + mv1.KVector3.Scalar134 * mv2.Scalar256 - mv1.KVector3.Scalar234 * mv2.Scalar156 + mv1.KVector3.Scalar125 * mv2.Scalar346 - mv1.KVector3.Scalar135 * mv2.Scalar246 + mv1.KVector3.Scalar235 * mv2.Scalar146 + mv1.KVector3.Scalar145 * mv2.Scalar236 - mv1.KVector3.Scalar245 * mv2.Scalar136 + mv1.KVector3.Scalar345 * mv2.Scalar126 - mv1.KVector3.Scalar126 * mv2.Scalar345 + mv1.KVector3.Scalar136 * mv2.Scalar245 - mv1.KVector3.Scalar236 * mv2.Scalar145 - mv1.KVector3.Scalar146 * mv2.Scalar235 + mv1.KVector3.Scalar246 * mv2.Scalar135 - mv1.KVector3.Scalar346 * mv2.Scalar125 + mv1.KVector3.Scalar156 * mv2.Scalar234 - mv1.KVector3.Scalar256 * mv2.Scalar134 + mv1.KVector3.Scalar356 * mv2.Scalar124 - mv1.KVector3.Scalar456 * mv2.Scalar123;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[1] += -mv1.KVector4.Scalar1234 * mv2.Scalar234 - mv1.KVector4.Scalar1235 * mv2.Scalar235 - mv1.KVector4.Scalar1245 * mv2.Scalar245 - mv1.KVector4.Scalar1345 * mv2.Scalar345 - mv1.KVector4.Scalar1236 * mv2.Scalar236 - mv1.KVector4.Scalar1246 * mv2.Scalar246 - mv1.KVector4.Scalar1346 * mv2.Scalar346 - mv1.KVector4.Scalar1256 * mv2.Scalar256 - mv1.KVector4.Scalar1356 * mv2.Scalar356 - mv1.KVector4.Scalar1456 * mv2.Scalar456;
            tempScalar[2] += mv1.KVector4.Scalar1234 * mv2.Scalar134 + mv1.KVector4.Scalar1235 * mv2.Scalar135 + mv1.KVector4.Scalar1245 * mv2.Scalar145 - mv1.KVector4.Scalar2345 * mv2.Scalar345 + mv1.KVector4.Scalar1236 * mv2.Scalar136 + mv1.KVector4.Scalar1246 * mv2.Scalar146 - mv1.KVector4.Scalar2346 * mv2.Scalar346 + mv1.KVector4.Scalar1256 * mv2.Scalar156 - mv1.KVector4.Scalar2356 * mv2.Scalar356 - mv1.KVector4.Scalar2456 * mv2.Scalar456;
            tempScalar[4] += -mv1.KVector4.Scalar1234 * mv2.Scalar124 - mv1.KVector4.Scalar1235 * mv2.Scalar125 + mv1.KVector4.Scalar1345 * mv2.Scalar145 + mv1.KVector4.Scalar2345 * mv2.Scalar245 - mv1.KVector4.Scalar1236 * mv2.Scalar126 + mv1.KVector4.Scalar1346 * mv2.Scalar146 + mv1.KVector4.Scalar2346 * mv2.Scalar246 + mv1.KVector4.Scalar1356 * mv2.Scalar156 + mv1.KVector4.Scalar2356 * mv2.Scalar256 - mv1.KVector4.Scalar3456 * mv2.Scalar456;
            tempScalar[8] += mv1.KVector4.Scalar1234 * mv2.Scalar123 - mv1.KVector4.Scalar1245 * mv2.Scalar125 - mv1.KVector4.Scalar1345 * mv2.Scalar135 - mv1.KVector4.Scalar2345 * mv2.Scalar235 - mv1.KVector4.Scalar1246 * mv2.Scalar126 - mv1.KVector4.Scalar1346 * mv2.Scalar136 - mv1.KVector4.Scalar2346 * mv2.Scalar236 + mv1.KVector4.Scalar1456 * mv2.Scalar156 + mv1.KVector4.Scalar2456 * mv2.Scalar256 + mv1.KVector4.Scalar3456 * mv2.Scalar356;
            tempScalar[16] += mv1.KVector4.Scalar1235 * mv2.Scalar123 + mv1.KVector4.Scalar1245 * mv2.Scalar124 + mv1.KVector4.Scalar1345 * mv2.Scalar134 + mv1.KVector4.Scalar2345 * mv2.Scalar234 - mv1.KVector4.Scalar1256 * mv2.Scalar126 - mv1.KVector4.Scalar1356 * mv2.Scalar136 - mv1.KVector4.Scalar2356 * mv2.Scalar236 - mv1.KVector4.Scalar1456 * mv2.Scalar146 - mv1.KVector4.Scalar2456 * mv2.Scalar246 - mv1.KVector4.Scalar3456 * mv2.Scalar346;
            tempScalar[31] += mv1.KVector4.Scalar1236 * mv2.Scalar456 - mv1.KVector4.Scalar1246 * mv2.Scalar356 + mv1.KVector4.Scalar1346 * mv2.Scalar256 - mv1.KVector4.Scalar2346 * mv2.Scalar156 + mv1.KVector4.Scalar1256 * mv2.Scalar346 - mv1.KVector4.Scalar1356 * mv2.Scalar246 + mv1.KVector4.Scalar2356 * mv2.Scalar146 + mv1.KVector4.Scalar1456 * mv2.Scalar236 - mv1.KVector4.Scalar2456 * mv2.Scalar136 + mv1.KVector4.Scalar3456 * mv2.Scalar126;
            tempScalar[32] += mv1.KVector4.Scalar1236 * mv2.Scalar123 + mv1.KVector4.Scalar1246 * mv2.Scalar124 + mv1.KVector4.Scalar1346 * mv2.Scalar134 + mv1.KVector4.Scalar2346 * mv2.Scalar234 + mv1.KVector4.Scalar1256 * mv2.Scalar125 + mv1.KVector4.Scalar1356 * mv2.Scalar135 + mv1.KVector4.Scalar2356 * mv2.Scalar235 + mv1.KVector4.Scalar1456 * mv2.Scalar145 + mv1.KVector4.Scalar2456 * mv2.Scalar245 + mv1.KVector4.Scalar3456 * mv2.Scalar345;
            tempScalar[47] += -mv1.KVector4.Scalar1235 * mv2.Scalar456 + mv1.KVector4.Scalar1245 * mv2.Scalar356 - mv1.KVector4.Scalar1345 * mv2.Scalar256 + mv1.KVector4.Scalar2345 * mv2.Scalar156 - mv1.KVector4.Scalar1256 * mv2.Scalar345 + mv1.KVector4.Scalar1356 * mv2.Scalar245 - mv1.KVector4.Scalar2356 * mv2.Scalar145 - mv1.KVector4.Scalar1456 * mv2.Scalar235 + mv1.KVector4.Scalar2456 * mv2.Scalar135 - mv1.KVector4.Scalar3456 * mv2.Scalar125;
            tempScalar[55] += mv1.KVector4.Scalar1234 * mv2.Scalar456 - mv1.KVector4.Scalar1245 * mv2.Scalar346 + mv1.KVector4.Scalar1345 * mv2.Scalar246 - mv1.KVector4.Scalar2345 * mv2.Scalar146 + mv1.KVector4.Scalar1246 * mv2.Scalar345 - mv1.KVector4.Scalar1346 * mv2.Scalar245 + mv1.KVector4.Scalar2346 * mv2.Scalar145 + mv1.KVector4.Scalar1456 * mv2.Scalar234 - mv1.KVector4.Scalar2456 * mv2.Scalar134 + mv1.KVector4.Scalar3456 * mv2.Scalar124;
            tempScalar[59] += -mv1.KVector4.Scalar1234 * mv2.Scalar356 + mv1.KVector4.Scalar1235 * mv2.Scalar346 - mv1.KVector4.Scalar1345 * mv2.Scalar236 + mv1.KVector4.Scalar2345 * mv2.Scalar136 - mv1.KVector4.Scalar1236 * mv2.Scalar345 + mv1.KVector4.Scalar1346 * mv2.Scalar235 - mv1.KVector4.Scalar2346 * mv2.Scalar135 - mv1.KVector4.Scalar1356 * mv2.Scalar234 + mv1.KVector4.Scalar2356 * mv2.Scalar134 - mv1.KVector4.Scalar3456 * mv2.Scalar123;
            tempScalar[61] += mv1.KVector4.Scalar1234 * mv2.Scalar256 - mv1.KVector4.Scalar1235 * mv2.Scalar246 + mv1.KVector4.Scalar1245 * mv2.Scalar236 - mv1.KVector4.Scalar2345 * mv2.Scalar126 + mv1.KVector4.Scalar1236 * mv2.Scalar245 - mv1.KVector4.Scalar1246 * mv2.Scalar235 + mv1.KVector4.Scalar2346 * mv2.Scalar125 + mv1.KVector4.Scalar1256 * mv2.Scalar234 - mv1.KVector4.Scalar2356 * mv2.Scalar124 + mv1.KVector4.Scalar2456 * mv2.Scalar123;
            tempScalar[62] += -mv1.KVector4.Scalar1234 * mv2.Scalar156 + mv1.KVector4.Scalar1235 * mv2.Scalar146 - mv1.KVector4.Scalar1245 * mv2.Scalar136 + mv1.KVector4.Scalar1345 * mv2.Scalar126 - mv1.KVector4.Scalar1236 * mv2.Scalar145 + mv1.KVector4.Scalar1246 * mv2.Scalar135 - mv1.KVector4.Scalar1346 * mv2.Scalar125 - mv1.KVector4.Scalar1256 * mv2.Scalar134 + mv1.KVector4.Scalar1356 * mv2.Scalar124 - mv1.KVector4.Scalar1456 * mv2.Scalar123;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[15] += -mv1.KVector5.Scalar12356 * mv2.Scalar456 + mv1.KVector5.Scalar12456 * mv2.Scalar356 - mv1.KVector5.Scalar13456 * mv2.Scalar256 + mv1.KVector5.Scalar23456 * mv2.Scalar156;
            tempScalar[23] += mv1.KVector5.Scalar12346 * mv2.Scalar456 - mv1.KVector5.Scalar12456 * mv2.Scalar346 + mv1.KVector5.Scalar13456 * mv2.Scalar246 - mv1.KVector5.Scalar23456 * mv2.Scalar146;
            tempScalar[27] += -mv1.KVector5.Scalar12346 * mv2.Scalar356 + mv1.KVector5.Scalar12356 * mv2.Scalar346 - mv1.KVector5.Scalar13456 * mv2.Scalar236 + mv1.KVector5.Scalar23456 * mv2.Scalar136;
            tempScalar[29] += mv1.KVector5.Scalar12346 * mv2.Scalar256 - mv1.KVector5.Scalar12356 * mv2.Scalar246 + mv1.KVector5.Scalar12456 * mv2.Scalar236 - mv1.KVector5.Scalar23456 * mv2.Scalar126;
            tempScalar[30] += -mv1.KVector5.Scalar12346 * mv2.Scalar156 + mv1.KVector5.Scalar12356 * mv2.Scalar146 - mv1.KVector5.Scalar12456 * mv2.Scalar136 + mv1.KVector5.Scalar13456 * mv2.Scalar126;
            tempScalar[39] += -mv1.KVector5.Scalar12345 * mv2.Scalar456 + mv1.KVector5.Scalar12456 * mv2.Scalar345 - mv1.KVector5.Scalar13456 * mv2.Scalar245 + mv1.KVector5.Scalar23456 * mv2.Scalar145;
            tempScalar[43] += mv1.KVector5.Scalar12345 * mv2.Scalar356 - mv1.KVector5.Scalar12356 * mv2.Scalar345 + mv1.KVector5.Scalar13456 * mv2.Scalar235 - mv1.KVector5.Scalar23456 * mv2.Scalar135;
            tempScalar[45] += -mv1.KVector5.Scalar12345 * mv2.Scalar256 + mv1.KVector5.Scalar12356 * mv2.Scalar245 - mv1.KVector5.Scalar12456 * mv2.Scalar235 + mv1.KVector5.Scalar23456 * mv2.Scalar125;
            tempScalar[46] += mv1.KVector5.Scalar12345 * mv2.Scalar156 - mv1.KVector5.Scalar12356 * mv2.Scalar145 + mv1.KVector5.Scalar12456 * mv2.Scalar135 - mv1.KVector5.Scalar13456 * mv2.Scalar125;
            tempScalar[51] += -mv1.KVector5.Scalar12345 * mv2.Scalar346 + mv1.KVector5.Scalar12346 * mv2.Scalar345 - mv1.KVector5.Scalar13456 * mv2.Scalar234 + mv1.KVector5.Scalar23456 * mv2.Scalar134;
            tempScalar[53] += mv1.KVector5.Scalar12345 * mv2.Scalar246 - mv1.KVector5.Scalar12346 * mv2.Scalar245 + mv1.KVector5.Scalar12456 * mv2.Scalar234 - mv1.KVector5.Scalar23456 * mv2.Scalar124;
            tempScalar[54] += -mv1.KVector5.Scalar12345 * mv2.Scalar146 + mv1.KVector5.Scalar12346 * mv2.Scalar145 - mv1.KVector5.Scalar12456 * mv2.Scalar134 + mv1.KVector5.Scalar13456 * mv2.Scalar124;
            tempScalar[57] += -mv1.KVector5.Scalar12345 * mv2.Scalar236 + mv1.KVector5.Scalar12346 * mv2.Scalar235 - mv1.KVector5.Scalar12356 * mv2.Scalar234 + mv1.KVector5.Scalar23456 * mv2.Scalar123;
            tempScalar[58] += mv1.KVector5.Scalar12345 * mv2.Scalar136 - mv1.KVector5.Scalar12346 * mv2.Scalar135 + mv1.KVector5.Scalar12356 * mv2.Scalar134 - mv1.KVector5.Scalar13456 * mv2.Scalar123;
            tempScalar[60] += -mv1.KVector5.Scalar12345 * mv2.Scalar126 + mv1.KVector5.Scalar12346 * mv2.Scalar125 - mv1.KVector5.Scalar12356 * mv2.Scalar124 + mv1.KVector5.Scalar12456 * mv2.Scalar123;
        }
        
        if (!mv1.KVector6.IsZero())
        {
            tempScalar[7] += -mv1.KVector6.Scalar123456 * mv2.Scalar456;
            tempScalar[11] += mv1.KVector6.Scalar123456 * mv2.Scalar356;
            tempScalar[13] += -mv1.KVector6.Scalar123456 * mv2.Scalar256;
            tempScalar[14] += mv1.KVector6.Scalar123456 * mv2.Scalar156;
            tempScalar[19] += -mv1.KVector6.Scalar123456 * mv2.Scalar346;
            tempScalar[21] += mv1.KVector6.Scalar123456 * mv2.Scalar246;
            tempScalar[22] += -mv1.KVector6.Scalar123456 * mv2.Scalar146;
            tempScalar[25] += -mv1.KVector6.Scalar123456 * mv2.Scalar236;
            tempScalar[26] += mv1.KVector6.Scalar123456 * mv2.Scalar136;
            tempScalar[28] += -mv1.KVector6.Scalar123456 * mv2.Scalar126;
            tempScalar[35] += mv1.KVector6.Scalar123456 * mv2.Scalar345;
            tempScalar[37] += -mv1.KVector6.Scalar123456 * mv2.Scalar245;
            tempScalar[38] += mv1.KVector6.Scalar123456 * mv2.Scalar145;
            tempScalar[41] += mv1.KVector6.Scalar123456 * mv2.Scalar235;
            tempScalar[42] += -mv1.KVector6.Scalar123456 * mv2.Scalar135;
            tempScalar[44] += mv1.KVector6.Scalar123456 * mv2.Scalar125;
            tempScalar[49] += -mv1.KVector6.Scalar123456 * mv2.Scalar234;
            tempScalar[50] += mv1.KVector6.Scalar123456 * mv2.Scalar134;
            tempScalar[52] += -mv1.KVector6.Scalar123456 * mv2.Scalar124;
            tempScalar[56] += mv1.KVector6.Scalar123456 * mv2.Scalar123;
        }
        
        return Ga6Multivector.Create(tempScalar);
    }
    
    public static Ga6Multivector Cp(this Ga6Multivector mv1, Ga6KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[7] += -mv1.KVector1.Scalar4 * mv2.Scalar1234 - mv1.KVector1.Scalar5 * mv2.Scalar1235 - mv1.KVector1.Scalar6 * mv2.Scalar1236;
            tempScalar[11] += mv1.KVector1.Scalar3 * mv2.Scalar1234 - mv1.KVector1.Scalar5 * mv2.Scalar1245 - mv1.KVector1.Scalar6 * mv2.Scalar1246;
            tempScalar[13] += -mv1.KVector1.Scalar2 * mv2.Scalar1234 - mv1.KVector1.Scalar5 * mv2.Scalar1345 - mv1.KVector1.Scalar6 * mv2.Scalar1346;
            tempScalar[14] += mv1.KVector1.Scalar1 * mv2.Scalar1234 - mv1.KVector1.Scalar5 * mv2.Scalar2345 - mv1.KVector1.Scalar6 * mv2.Scalar2346;
            tempScalar[19] += mv1.KVector1.Scalar3 * mv2.Scalar1235 + mv1.KVector1.Scalar4 * mv2.Scalar1245 - mv1.KVector1.Scalar6 * mv2.Scalar1256;
            tempScalar[21] += -mv1.KVector1.Scalar2 * mv2.Scalar1235 + mv1.KVector1.Scalar4 * mv2.Scalar1345 - mv1.KVector1.Scalar6 * mv2.Scalar1356;
            tempScalar[22] += mv1.KVector1.Scalar1 * mv2.Scalar1235 + mv1.KVector1.Scalar4 * mv2.Scalar2345 - mv1.KVector1.Scalar6 * mv2.Scalar2356;
            tempScalar[25] += -mv1.KVector1.Scalar2 * mv2.Scalar1245 - mv1.KVector1.Scalar3 * mv2.Scalar1345 - mv1.KVector1.Scalar6 * mv2.Scalar1456;
            tempScalar[26] += mv1.KVector1.Scalar1 * mv2.Scalar1245 - mv1.KVector1.Scalar3 * mv2.Scalar2345 - mv1.KVector1.Scalar6 * mv2.Scalar2456;
            tempScalar[28] += mv1.KVector1.Scalar1 * mv2.Scalar1345 + mv1.KVector1.Scalar2 * mv2.Scalar2345 - mv1.KVector1.Scalar6 * mv2.Scalar3456;
            tempScalar[35] += mv1.KVector1.Scalar3 * mv2.Scalar1236 + mv1.KVector1.Scalar4 * mv2.Scalar1246 + mv1.KVector1.Scalar5 * mv2.Scalar1256;
            tempScalar[37] += -mv1.KVector1.Scalar2 * mv2.Scalar1236 + mv1.KVector1.Scalar4 * mv2.Scalar1346 + mv1.KVector1.Scalar5 * mv2.Scalar1356;
            tempScalar[38] += mv1.KVector1.Scalar1 * mv2.Scalar1236 + mv1.KVector1.Scalar4 * mv2.Scalar2346 + mv1.KVector1.Scalar5 * mv2.Scalar2356;
            tempScalar[41] += -mv1.KVector1.Scalar2 * mv2.Scalar1246 - mv1.KVector1.Scalar3 * mv2.Scalar1346 + mv1.KVector1.Scalar5 * mv2.Scalar1456;
            tempScalar[42] += mv1.KVector1.Scalar1 * mv2.Scalar1246 - mv1.KVector1.Scalar3 * mv2.Scalar2346 + mv1.KVector1.Scalar5 * mv2.Scalar2456;
            tempScalar[44] += mv1.KVector1.Scalar1 * mv2.Scalar1346 + mv1.KVector1.Scalar2 * mv2.Scalar2346 + mv1.KVector1.Scalar5 * mv2.Scalar3456;
            tempScalar[49] += -mv1.KVector1.Scalar2 * mv2.Scalar1256 - mv1.KVector1.Scalar3 * mv2.Scalar1356 - mv1.KVector1.Scalar4 * mv2.Scalar1456;
            tempScalar[50] += mv1.KVector1.Scalar1 * mv2.Scalar1256 - mv1.KVector1.Scalar3 * mv2.Scalar2356 - mv1.KVector1.Scalar4 * mv2.Scalar2456;
            tempScalar[52] += mv1.KVector1.Scalar1 * mv2.Scalar1356 + mv1.KVector1.Scalar2 * mv2.Scalar2356 - mv1.KVector1.Scalar4 * mv2.Scalar3456;
            tempScalar[56] += mv1.KVector1.Scalar1 * mv2.Scalar1456 + mv1.KVector1.Scalar2 * mv2.Scalar2456 + mv1.KVector1.Scalar3 * mv2.Scalar3456;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[15] += -mv1.KVector2.Scalar15 * mv2.Scalar2345 + mv1.KVector2.Scalar25 * mv2.Scalar1345 - mv1.KVector2.Scalar35 * mv2.Scalar1245 + mv1.KVector2.Scalar45 * mv2.Scalar1235 - mv1.KVector2.Scalar16 * mv2.Scalar2346 + mv1.KVector2.Scalar26 * mv2.Scalar1346 - mv1.KVector2.Scalar36 * mv2.Scalar1246 + mv1.KVector2.Scalar46 * mv2.Scalar1236;
            tempScalar[23] += mv1.KVector2.Scalar14 * mv2.Scalar2345 - mv1.KVector2.Scalar24 * mv2.Scalar1345 + mv1.KVector2.Scalar34 * mv2.Scalar1245 - mv1.KVector2.Scalar45 * mv2.Scalar1234 - mv1.KVector2.Scalar16 * mv2.Scalar2356 + mv1.KVector2.Scalar26 * mv2.Scalar1356 - mv1.KVector2.Scalar36 * mv2.Scalar1256 + mv1.KVector2.Scalar56 * mv2.Scalar1236;
            tempScalar[27] += -mv1.KVector2.Scalar13 * mv2.Scalar2345 + mv1.KVector2.Scalar23 * mv2.Scalar1345 - mv1.KVector2.Scalar34 * mv2.Scalar1235 + mv1.KVector2.Scalar35 * mv2.Scalar1234 - mv1.KVector2.Scalar16 * mv2.Scalar2456 + mv1.KVector2.Scalar26 * mv2.Scalar1456 - mv1.KVector2.Scalar46 * mv2.Scalar1256 + mv1.KVector2.Scalar56 * mv2.Scalar1246;
            tempScalar[29] += mv1.KVector2.Scalar12 * mv2.Scalar2345 - mv1.KVector2.Scalar23 * mv2.Scalar1245 + mv1.KVector2.Scalar24 * mv2.Scalar1235 - mv1.KVector2.Scalar25 * mv2.Scalar1234 - mv1.KVector2.Scalar16 * mv2.Scalar3456 + mv1.KVector2.Scalar36 * mv2.Scalar1456 - mv1.KVector2.Scalar46 * mv2.Scalar1356 + mv1.KVector2.Scalar56 * mv2.Scalar1346;
            tempScalar[30] += -mv1.KVector2.Scalar12 * mv2.Scalar1345 + mv1.KVector2.Scalar13 * mv2.Scalar1245 - mv1.KVector2.Scalar14 * mv2.Scalar1235 + mv1.KVector2.Scalar15 * mv2.Scalar1234 - mv1.KVector2.Scalar26 * mv2.Scalar3456 + mv1.KVector2.Scalar36 * mv2.Scalar2456 - mv1.KVector2.Scalar46 * mv2.Scalar2356 + mv1.KVector2.Scalar56 * mv2.Scalar2346;
            tempScalar[39] += mv1.KVector2.Scalar14 * mv2.Scalar2346 - mv1.KVector2.Scalar24 * mv2.Scalar1346 + mv1.KVector2.Scalar34 * mv2.Scalar1246 + mv1.KVector2.Scalar15 * mv2.Scalar2356 - mv1.KVector2.Scalar25 * mv2.Scalar1356 + mv1.KVector2.Scalar35 * mv2.Scalar1256 - mv1.KVector2.Scalar46 * mv2.Scalar1234 - mv1.KVector2.Scalar56 * mv2.Scalar1235;
            tempScalar[43] += -mv1.KVector2.Scalar13 * mv2.Scalar2346 + mv1.KVector2.Scalar23 * mv2.Scalar1346 - mv1.KVector2.Scalar34 * mv2.Scalar1236 + mv1.KVector2.Scalar15 * mv2.Scalar2456 - mv1.KVector2.Scalar25 * mv2.Scalar1456 + mv1.KVector2.Scalar45 * mv2.Scalar1256 + mv1.KVector2.Scalar36 * mv2.Scalar1234 - mv1.KVector2.Scalar56 * mv2.Scalar1245;
            tempScalar[45] += mv1.KVector2.Scalar12 * mv2.Scalar2346 - mv1.KVector2.Scalar23 * mv2.Scalar1246 + mv1.KVector2.Scalar24 * mv2.Scalar1236 + mv1.KVector2.Scalar15 * mv2.Scalar3456 - mv1.KVector2.Scalar35 * mv2.Scalar1456 + mv1.KVector2.Scalar45 * mv2.Scalar1356 - mv1.KVector2.Scalar26 * mv2.Scalar1234 - mv1.KVector2.Scalar56 * mv2.Scalar1345;
            tempScalar[46] += -mv1.KVector2.Scalar12 * mv2.Scalar1346 + mv1.KVector2.Scalar13 * mv2.Scalar1246 - mv1.KVector2.Scalar14 * mv2.Scalar1236 + mv1.KVector2.Scalar25 * mv2.Scalar3456 - mv1.KVector2.Scalar35 * mv2.Scalar2456 + mv1.KVector2.Scalar45 * mv2.Scalar2356 + mv1.KVector2.Scalar16 * mv2.Scalar1234 - mv1.KVector2.Scalar56 * mv2.Scalar2345;
            tempScalar[51] += -mv1.KVector2.Scalar13 * mv2.Scalar2356 + mv1.KVector2.Scalar23 * mv2.Scalar1356 - mv1.KVector2.Scalar14 * mv2.Scalar2456 + mv1.KVector2.Scalar24 * mv2.Scalar1456 - mv1.KVector2.Scalar35 * mv2.Scalar1236 - mv1.KVector2.Scalar45 * mv2.Scalar1246 + mv1.KVector2.Scalar36 * mv2.Scalar1235 + mv1.KVector2.Scalar46 * mv2.Scalar1245;
            tempScalar[53] += mv1.KVector2.Scalar12 * mv2.Scalar2356 - mv1.KVector2.Scalar23 * mv2.Scalar1256 - mv1.KVector2.Scalar14 * mv2.Scalar3456 + mv1.KVector2.Scalar34 * mv2.Scalar1456 + mv1.KVector2.Scalar25 * mv2.Scalar1236 - mv1.KVector2.Scalar45 * mv2.Scalar1346 - mv1.KVector2.Scalar26 * mv2.Scalar1235 + mv1.KVector2.Scalar46 * mv2.Scalar1345;
            tempScalar[54] += -mv1.KVector2.Scalar12 * mv2.Scalar1356 + mv1.KVector2.Scalar13 * mv2.Scalar1256 - mv1.KVector2.Scalar24 * mv2.Scalar3456 + mv1.KVector2.Scalar34 * mv2.Scalar2456 - mv1.KVector2.Scalar15 * mv2.Scalar1236 - mv1.KVector2.Scalar45 * mv2.Scalar2346 + mv1.KVector2.Scalar16 * mv2.Scalar1235 + mv1.KVector2.Scalar46 * mv2.Scalar2345;
            tempScalar[57] += mv1.KVector2.Scalar12 * mv2.Scalar2456 + mv1.KVector2.Scalar13 * mv2.Scalar3456 - mv1.KVector2.Scalar24 * mv2.Scalar1256 - mv1.KVector2.Scalar34 * mv2.Scalar1356 + mv1.KVector2.Scalar25 * mv2.Scalar1246 + mv1.KVector2.Scalar35 * mv2.Scalar1346 - mv1.KVector2.Scalar26 * mv2.Scalar1245 - mv1.KVector2.Scalar36 * mv2.Scalar1345;
            tempScalar[58] += -mv1.KVector2.Scalar12 * mv2.Scalar1456 + mv1.KVector2.Scalar23 * mv2.Scalar3456 + mv1.KVector2.Scalar14 * mv2.Scalar1256 - mv1.KVector2.Scalar34 * mv2.Scalar2356 - mv1.KVector2.Scalar15 * mv2.Scalar1246 + mv1.KVector2.Scalar35 * mv2.Scalar2346 + mv1.KVector2.Scalar16 * mv2.Scalar1245 - mv1.KVector2.Scalar36 * mv2.Scalar2345;
            tempScalar[60] += -mv1.KVector2.Scalar13 * mv2.Scalar1456 - mv1.KVector2.Scalar23 * mv2.Scalar2456 + mv1.KVector2.Scalar14 * mv2.Scalar1356 + mv1.KVector2.Scalar24 * mv2.Scalar2356 - mv1.KVector2.Scalar15 * mv2.Scalar1346 - mv1.KVector2.Scalar25 * mv2.Scalar2346 + mv1.KVector2.Scalar16 * mv2.Scalar1345 + mv1.KVector2.Scalar26 * mv2.Scalar2345;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[1] += mv1.KVector3.Scalar234 * mv2.Scalar1234 + mv1.KVector3.Scalar235 * mv2.Scalar1235 + mv1.KVector3.Scalar245 * mv2.Scalar1245 + mv1.KVector3.Scalar345 * mv2.Scalar1345 + mv1.KVector3.Scalar236 * mv2.Scalar1236 + mv1.KVector3.Scalar246 * mv2.Scalar1246 + mv1.KVector3.Scalar346 * mv2.Scalar1346 + mv1.KVector3.Scalar256 * mv2.Scalar1256 + mv1.KVector3.Scalar356 * mv2.Scalar1356 + mv1.KVector3.Scalar456 * mv2.Scalar1456;
            tempScalar[2] += -mv1.KVector3.Scalar134 * mv2.Scalar1234 - mv1.KVector3.Scalar135 * mv2.Scalar1235 - mv1.KVector3.Scalar145 * mv2.Scalar1245 + mv1.KVector3.Scalar345 * mv2.Scalar2345 - mv1.KVector3.Scalar136 * mv2.Scalar1236 - mv1.KVector3.Scalar146 * mv2.Scalar1246 + mv1.KVector3.Scalar346 * mv2.Scalar2346 - mv1.KVector3.Scalar156 * mv2.Scalar1256 + mv1.KVector3.Scalar356 * mv2.Scalar2356 + mv1.KVector3.Scalar456 * mv2.Scalar2456;
            tempScalar[4] += mv1.KVector3.Scalar124 * mv2.Scalar1234 + mv1.KVector3.Scalar125 * mv2.Scalar1235 - mv1.KVector3.Scalar145 * mv2.Scalar1345 - mv1.KVector3.Scalar245 * mv2.Scalar2345 + mv1.KVector3.Scalar126 * mv2.Scalar1236 - mv1.KVector3.Scalar146 * mv2.Scalar1346 - mv1.KVector3.Scalar246 * mv2.Scalar2346 - mv1.KVector3.Scalar156 * mv2.Scalar1356 - mv1.KVector3.Scalar256 * mv2.Scalar2356 + mv1.KVector3.Scalar456 * mv2.Scalar3456;
            tempScalar[8] += -mv1.KVector3.Scalar123 * mv2.Scalar1234 + mv1.KVector3.Scalar125 * mv2.Scalar1245 + mv1.KVector3.Scalar135 * mv2.Scalar1345 + mv1.KVector3.Scalar235 * mv2.Scalar2345 + mv1.KVector3.Scalar126 * mv2.Scalar1246 + mv1.KVector3.Scalar136 * mv2.Scalar1346 + mv1.KVector3.Scalar236 * mv2.Scalar2346 - mv1.KVector3.Scalar156 * mv2.Scalar1456 - mv1.KVector3.Scalar256 * mv2.Scalar2456 - mv1.KVector3.Scalar356 * mv2.Scalar3456;
            tempScalar[16] += -mv1.KVector3.Scalar123 * mv2.Scalar1235 - mv1.KVector3.Scalar124 * mv2.Scalar1245 - mv1.KVector3.Scalar134 * mv2.Scalar1345 - mv1.KVector3.Scalar234 * mv2.Scalar2345 + mv1.KVector3.Scalar126 * mv2.Scalar1256 + mv1.KVector3.Scalar136 * mv2.Scalar1356 + mv1.KVector3.Scalar236 * mv2.Scalar2356 + mv1.KVector3.Scalar146 * mv2.Scalar1456 + mv1.KVector3.Scalar246 * mv2.Scalar2456 + mv1.KVector3.Scalar346 * mv2.Scalar3456;
            tempScalar[31] += -mv1.KVector3.Scalar126 * mv2.Scalar3456 + mv1.KVector3.Scalar136 * mv2.Scalar2456 - mv1.KVector3.Scalar236 * mv2.Scalar1456 - mv1.KVector3.Scalar146 * mv2.Scalar2356 + mv1.KVector3.Scalar246 * mv2.Scalar1356 - mv1.KVector3.Scalar346 * mv2.Scalar1256 + mv1.KVector3.Scalar156 * mv2.Scalar2346 - mv1.KVector3.Scalar256 * mv2.Scalar1346 + mv1.KVector3.Scalar356 * mv2.Scalar1246 - mv1.KVector3.Scalar456 * mv2.Scalar1236;
            tempScalar[32] += -mv1.KVector3.Scalar123 * mv2.Scalar1236 - mv1.KVector3.Scalar124 * mv2.Scalar1246 - mv1.KVector3.Scalar134 * mv2.Scalar1346 - mv1.KVector3.Scalar234 * mv2.Scalar2346 - mv1.KVector3.Scalar125 * mv2.Scalar1256 - mv1.KVector3.Scalar135 * mv2.Scalar1356 - mv1.KVector3.Scalar235 * mv2.Scalar2356 - mv1.KVector3.Scalar145 * mv2.Scalar1456 - mv1.KVector3.Scalar245 * mv2.Scalar2456 - mv1.KVector3.Scalar345 * mv2.Scalar3456;
            tempScalar[47] += mv1.KVector3.Scalar125 * mv2.Scalar3456 - mv1.KVector3.Scalar135 * mv2.Scalar2456 + mv1.KVector3.Scalar235 * mv2.Scalar1456 + mv1.KVector3.Scalar145 * mv2.Scalar2356 - mv1.KVector3.Scalar245 * mv2.Scalar1356 + mv1.KVector3.Scalar345 * mv2.Scalar1256 - mv1.KVector3.Scalar156 * mv2.Scalar2345 + mv1.KVector3.Scalar256 * mv2.Scalar1345 - mv1.KVector3.Scalar356 * mv2.Scalar1245 + mv1.KVector3.Scalar456 * mv2.Scalar1235;
            tempScalar[55] += -mv1.KVector3.Scalar124 * mv2.Scalar3456 + mv1.KVector3.Scalar134 * mv2.Scalar2456 - mv1.KVector3.Scalar234 * mv2.Scalar1456 - mv1.KVector3.Scalar145 * mv2.Scalar2346 + mv1.KVector3.Scalar245 * mv2.Scalar1346 - mv1.KVector3.Scalar345 * mv2.Scalar1246 + mv1.KVector3.Scalar146 * mv2.Scalar2345 - mv1.KVector3.Scalar246 * mv2.Scalar1345 + mv1.KVector3.Scalar346 * mv2.Scalar1245 - mv1.KVector3.Scalar456 * mv2.Scalar1234;
            tempScalar[59] += mv1.KVector3.Scalar123 * mv2.Scalar3456 - mv1.KVector3.Scalar134 * mv2.Scalar2356 + mv1.KVector3.Scalar234 * mv2.Scalar1356 + mv1.KVector3.Scalar135 * mv2.Scalar2346 - mv1.KVector3.Scalar235 * mv2.Scalar1346 + mv1.KVector3.Scalar345 * mv2.Scalar1236 - mv1.KVector3.Scalar136 * mv2.Scalar2345 + mv1.KVector3.Scalar236 * mv2.Scalar1345 - mv1.KVector3.Scalar346 * mv2.Scalar1235 + mv1.KVector3.Scalar356 * mv2.Scalar1234;
            tempScalar[61] += -mv1.KVector3.Scalar123 * mv2.Scalar2456 + mv1.KVector3.Scalar124 * mv2.Scalar2356 - mv1.KVector3.Scalar234 * mv2.Scalar1256 - mv1.KVector3.Scalar125 * mv2.Scalar2346 + mv1.KVector3.Scalar235 * mv2.Scalar1246 - mv1.KVector3.Scalar245 * mv2.Scalar1236 + mv1.KVector3.Scalar126 * mv2.Scalar2345 - mv1.KVector3.Scalar236 * mv2.Scalar1245 + mv1.KVector3.Scalar246 * mv2.Scalar1235 - mv1.KVector3.Scalar256 * mv2.Scalar1234;
            tempScalar[62] += mv1.KVector3.Scalar123 * mv2.Scalar1456 - mv1.KVector3.Scalar124 * mv2.Scalar1356 + mv1.KVector3.Scalar134 * mv2.Scalar1256 + mv1.KVector3.Scalar125 * mv2.Scalar1346 - mv1.KVector3.Scalar135 * mv2.Scalar1246 + mv1.KVector3.Scalar145 * mv2.Scalar1236 - mv1.KVector3.Scalar126 * mv2.Scalar1345 + mv1.KVector3.Scalar136 * mv2.Scalar1245 - mv1.KVector3.Scalar146 * mv2.Scalar1235 + mv1.KVector3.Scalar156 * mv2.Scalar1234;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[3] += mv1.KVector4.Scalar1345 * mv2.Scalar2345 - mv1.KVector4.Scalar2345 * mv2.Scalar1345 + mv1.KVector4.Scalar1346 * mv2.Scalar2346 - mv1.KVector4.Scalar2346 * mv2.Scalar1346 + mv1.KVector4.Scalar1356 * mv2.Scalar2356 - mv1.KVector4.Scalar2356 * mv2.Scalar1356 + mv1.KVector4.Scalar1456 * mv2.Scalar2456 - mv1.KVector4.Scalar2456 * mv2.Scalar1456;
            tempScalar[5] += -mv1.KVector4.Scalar1245 * mv2.Scalar2345 + mv1.KVector4.Scalar2345 * mv2.Scalar1245 - mv1.KVector4.Scalar1246 * mv2.Scalar2346 + mv1.KVector4.Scalar2346 * mv2.Scalar1246 - mv1.KVector4.Scalar1256 * mv2.Scalar2356 + mv1.KVector4.Scalar2356 * mv2.Scalar1256 + mv1.KVector4.Scalar1456 * mv2.Scalar3456 - mv1.KVector4.Scalar3456 * mv2.Scalar1456;
            tempScalar[6] += mv1.KVector4.Scalar1245 * mv2.Scalar1345 - mv1.KVector4.Scalar1345 * mv2.Scalar1245 + mv1.KVector4.Scalar1246 * mv2.Scalar1346 - mv1.KVector4.Scalar1346 * mv2.Scalar1246 + mv1.KVector4.Scalar1256 * mv2.Scalar1356 - mv1.KVector4.Scalar1356 * mv2.Scalar1256 + mv1.KVector4.Scalar2456 * mv2.Scalar3456 - mv1.KVector4.Scalar3456 * mv2.Scalar2456;
            tempScalar[9] += mv1.KVector4.Scalar1235 * mv2.Scalar2345 - mv1.KVector4.Scalar2345 * mv2.Scalar1235 + mv1.KVector4.Scalar1236 * mv2.Scalar2346 - mv1.KVector4.Scalar2346 * mv2.Scalar1236 - mv1.KVector4.Scalar1256 * mv2.Scalar2456 - mv1.KVector4.Scalar1356 * mv2.Scalar3456 + mv1.KVector4.Scalar2456 * mv2.Scalar1256 + mv1.KVector4.Scalar3456 * mv2.Scalar1356;
            tempScalar[10] += -mv1.KVector4.Scalar1235 * mv2.Scalar1345 + mv1.KVector4.Scalar1345 * mv2.Scalar1235 - mv1.KVector4.Scalar1236 * mv2.Scalar1346 + mv1.KVector4.Scalar1346 * mv2.Scalar1236 + mv1.KVector4.Scalar1256 * mv2.Scalar1456 - mv1.KVector4.Scalar2356 * mv2.Scalar3456 - mv1.KVector4.Scalar1456 * mv2.Scalar1256 + mv1.KVector4.Scalar3456 * mv2.Scalar2356;
            tempScalar[12] += mv1.KVector4.Scalar1235 * mv2.Scalar1245 - mv1.KVector4.Scalar1245 * mv2.Scalar1235 + mv1.KVector4.Scalar1236 * mv2.Scalar1246 - mv1.KVector4.Scalar1246 * mv2.Scalar1236 + mv1.KVector4.Scalar1356 * mv2.Scalar1456 + mv1.KVector4.Scalar2356 * mv2.Scalar2456 - mv1.KVector4.Scalar1456 * mv2.Scalar1356 - mv1.KVector4.Scalar2456 * mv2.Scalar2356;
            tempScalar[17] += -mv1.KVector4.Scalar1234 * mv2.Scalar2345 + mv1.KVector4.Scalar2345 * mv2.Scalar1234 + mv1.KVector4.Scalar1236 * mv2.Scalar2356 + mv1.KVector4.Scalar1246 * mv2.Scalar2456 + mv1.KVector4.Scalar1346 * mv2.Scalar3456 - mv1.KVector4.Scalar2356 * mv2.Scalar1236 - mv1.KVector4.Scalar2456 * mv2.Scalar1246 - mv1.KVector4.Scalar3456 * mv2.Scalar1346;
            tempScalar[18] += mv1.KVector4.Scalar1234 * mv2.Scalar1345 - mv1.KVector4.Scalar1345 * mv2.Scalar1234 - mv1.KVector4.Scalar1236 * mv2.Scalar1356 - mv1.KVector4.Scalar1246 * mv2.Scalar1456 + mv1.KVector4.Scalar2346 * mv2.Scalar3456 + mv1.KVector4.Scalar1356 * mv2.Scalar1236 + mv1.KVector4.Scalar1456 * mv2.Scalar1246 - mv1.KVector4.Scalar3456 * mv2.Scalar2346;
            tempScalar[20] += -mv1.KVector4.Scalar1234 * mv2.Scalar1245 + mv1.KVector4.Scalar1245 * mv2.Scalar1234 + mv1.KVector4.Scalar1236 * mv2.Scalar1256 - mv1.KVector4.Scalar1346 * mv2.Scalar1456 - mv1.KVector4.Scalar2346 * mv2.Scalar2456 - mv1.KVector4.Scalar1256 * mv2.Scalar1236 + mv1.KVector4.Scalar1456 * mv2.Scalar1346 + mv1.KVector4.Scalar2456 * mv2.Scalar2346;
            tempScalar[24] += mv1.KVector4.Scalar1234 * mv2.Scalar1235 - mv1.KVector4.Scalar1235 * mv2.Scalar1234 + mv1.KVector4.Scalar1246 * mv2.Scalar1256 + mv1.KVector4.Scalar1346 * mv2.Scalar1356 + mv1.KVector4.Scalar2346 * mv2.Scalar2356 - mv1.KVector4.Scalar1256 * mv2.Scalar1246 - mv1.KVector4.Scalar1356 * mv2.Scalar1346 - mv1.KVector4.Scalar2356 * mv2.Scalar2346;
            tempScalar[33] += -mv1.KVector4.Scalar1234 * mv2.Scalar2346 - mv1.KVector4.Scalar1235 * mv2.Scalar2356 - mv1.KVector4.Scalar1245 * mv2.Scalar2456 - mv1.KVector4.Scalar1345 * mv2.Scalar3456 + mv1.KVector4.Scalar2346 * mv2.Scalar1234 + mv1.KVector4.Scalar2356 * mv2.Scalar1235 + mv1.KVector4.Scalar2456 * mv2.Scalar1245 + mv1.KVector4.Scalar3456 * mv2.Scalar1345;
            tempScalar[34] += mv1.KVector4.Scalar1234 * mv2.Scalar1346 + mv1.KVector4.Scalar1235 * mv2.Scalar1356 + mv1.KVector4.Scalar1245 * mv2.Scalar1456 - mv1.KVector4.Scalar2345 * mv2.Scalar3456 - mv1.KVector4.Scalar1346 * mv2.Scalar1234 - mv1.KVector4.Scalar1356 * mv2.Scalar1235 - mv1.KVector4.Scalar1456 * mv2.Scalar1245 + mv1.KVector4.Scalar3456 * mv2.Scalar2345;
            tempScalar[36] += -mv1.KVector4.Scalar1234 * mv2.Scalar1246 - mv1.KVector4.Scalar1235 * mv2.Scalar1256 + mv1.KVector4.Scalar1345 * mv2.Scalar1456 + mv1.KVector4.Scalar2345 * mv2.Scalar2456 + mv1.KVector4.Scalar1246 * mv2.Scalar1234 + mv1.KVector4.Scalar1256 * mv2.Scalar1235 - mv1.KVector4.Scalar1456 * mv2.Scalar1345 - mv1.KVector4.Scalar2456 * mv2.Scalar2345;
            tempScalar[40] += mv1.KVector4.Scalar1234 * mv2.Scalar1236 - mv1.KVector4.Scalar1245 * mv2.Scalar1256 - mv1.KVector4.Scalar1345 * mv2.Scalar1356 - mv1.KVector4.Scalar2345 * mv2.Scalar2356 - mv1.KVector4.Scalar1236 * mv2.Scalar1234 + mv1.KVector4.Scalar1256 * mv2.Scalar1245 + mv1.KVector4.Scalar1356 * mv2.Scalar1345 + mv1.KVector4.Scalar2356 * mv2.Scalar2345;
            tempScalar[48] += mv1.KVector4.Scalar1235 * mv2.Scalar1236 + mv1.KVector4.Scalar1245 * mv2.Scalar1246 + mv1.KVector4.Scalar1345 * mv2.Scalar1346 + mv1.KVector4.Scalar2345 * mv2.Scalar2346 - mv1.KVector4.Scalar1236 * mv2.Scalar1235 - mv1.KVector4.Scalar1246 * mv2.Scalar1245 - mv1.KVector4.Scalar1346 * mv2.Scalar1345 - mv1.KVector4.Scalar2346 * mv2.Scalar2345;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[7] += mv1.KVector5.Scalar12456 * mv2.Scalar3456 - mv1.KVector5.Scalar13456 * mv2.Scalar2456 + mv1.KVector5.Scalar23456 * mv2.Scalar1456;
            tempScalar[11] += -mv1.KVector5.Scalar12356 * mv2.Scalar3456 + mv1.KVector5.Scalar13456 * mv2.Scalar2356 - mv1.KVector5.Scalar23456 * mv2.Scalar1356;
            tempScalar[13] += mv1.KVector5.Scalar12356 * mv2.Scalar2456 - mv1.KVector5.Scalar12456 * mv2.Scalar2356 + mv1.KVector5.Scalar23456 * mv2.Scalar1256;
            tempScalar[14] += -mv1.KVector5.Scalar12356 * mv2.Scalar1456 + mv1.KVector5.Scalar12456 * mv2.Scalar1356 - mv1.KVector5.Scalar13456 * mv2.Scalar1256;
            tempScalar[19] += mv1.KVector5.Scalar12346 * mv2.Scalar3456 - mv1.KVector5.Scalar13456 * mv2.Scalar2346 + mv1.KVector5.Scalar23456 * mv2.Scalar1346;
            tempScalar[21] += -mv1.KVector5.Scalar12346 * mv2.Scalar2456 + mv1.KVector5.Scalar12456 * mv2.Scalar2346 - mv1.KVector5.Scalar23456 * mv2.Scalar1246;
            tempScalar[22] += mv1.KVector5.Scalar12346 * mv2.Scalar1456 - mv1.KVector5.Scalar12456 * mv2.Scalar1346 + mv1.KVector5.Scalar13456 * mv2.Scalar1246;
            tempScalar[25] += mv1.KVector5.Scalar12346 * mv2.Scalar2356 - mv1.KVector5.Scalar12356 * mv2.Scalar2346 + mv1.KVector5.Scalar23456 * mv2.Scalar1236;
            tempScalar[26] += -mv1.KVector5.Scalar12346 * mv2.Scalar1356 + mv1.KVector5.Scalar12356 * mv2.Scalar1346 - mv1.KVector5.Scalar13456 * mv2.Scalar1236;
            tempScalar[28] += mv1.KVector5.Scalar12346 * mv2.Scalar1256 - mv1.KVector5.Scalar12356 * mv2.Scalar1246 + mv1.KVector5.Scalar12456 * mv2.Scalar1236;
            tempScalar[35] += -mv1.KVector5.Scalar12345 * mv2.Scalar3456 + mv1.KVector5.Scalar13456 * mv2.Scalar2345 - mv1.KVector5.Scalar23456 * mv2.Scalar1345;
            tempScalar[37] += mv1.KVector5.Scalar12345 * mv2.Scalar2456 - mv1.KVector5.Scalar12456 * mv2.Scalar2345 + mv1.KVector5.Scalar23456 * mv2.Scalar1245;
            tempScalar[38] += -mv1.KVector5.Scalar12345 * mv2.Scalar1456 + mv1.KVector5.Scalar12456 * mv2.Scalar1345 - mv1.KVector5.Scalar13456 * mv2.Scalar1245;
            tempScalar[41] += -mv1.KVector5.Scalar12345 * mv2.Scalar2356 + mv1.KVector5.Scalar12356 * mv2.Scalar2345 - mv1.KVector5.Scalar23456 * mv2.Scalar1235;
            tempScalar[42] += mv1.KVector5.Scalar12345 * mv2.Scalar1356 - mv1.KVector5.Scalar12356 * mv2.Scalar1345 + mv1.KVector5.Scalar13456 * mv2.Scalar1235;
            tempScalar[44] += -mv1.KVector5.Scalar12345 * mv2.Scalar1256 + mv1.KVector5.Scalar12356 * mv2.Scalar1245 - mv1.KVector5.Scalar12456 * mv2.Scalar1235;
            tempScalar[49] += mv1.KVector5.Scalar12345 * mv2.Scalar2346 - mv1.KVector5.Scalar12346 * mv2.Scalar2345 + mv1.KVector5.Scalar23456 * mv2.Scalar1234;
            tempScalar[50] += -mv1.KVector5.Scalar12345 * mv2.Scalar1346 + mv1.KVector5.Scalar12346 * mv2.Scalar1345 - mv1.KVector5.Scalar13456 * mv2.Scalar1234;
            tempScalar[52] += mv1.KVector5.Scalar12345 * mv2.Scalar1246 - mv1.KVector5.Scalar12346 * mv2.Scalar1245 + mv1.KVector5.Scalar12456 * mv2.Scalar1234;
            tempScalar[56] += -mv1.KVector5.Scalar12345 * mv2.Scalar1236 + mv1.KVector5.Scalar12346 * mv2.Scalar1235 - mv1.KVector5.Scalar12356 * mv2.Scalar1234;
        }
        
        return Ga6Multivector.Create(tempScalar);
    }
    
    public static Ga6Multivector Cp(this Ga6Multivector mv1, Ga6KVector5 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[63] += mv1.KVector1.Scalar1 * mv2.Scalar23456 - mv1.KVector1.Scalar2 * mv2.Scalar13456 + mv1.KVector1.Scalar3 * mv2.Scalar12456 - mv1.KVector1.Scalar4 * mv2.Scalar12356 + mv1.KVector1.Scalar5 * mv2.Scalar12346 - mv1.KVector1.Scalar6 * mv2.Scalar12345;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[31] += mv1.KVector2.Scalar16 * mv2.Scalar23456 - mv1.KVector2.Scalar26 * mv2.Scalar13456 + mv1.KVector2.Scalar36 * mv2.Scalar12456 - mv1.KVector2.Scalar46 * mv2.Scalar12356 + mv1.KVector2.Scalar56 * mv2.Scalar12346;
            tempScalar[47] += -mv1.KVector2.Scalar15 * mv2.Scalar23456 + mv1.KVector2.Scalar25 * mv2.Scalar13456 - mv1.KVector2.Scalar35 * mv2.Scalar12456 + mv1.KVector2.Scalar45 * mv2.Scalar12356 - mv1.KVector2.Scalar56 * mv2.Scalar12345;
            tempScalar[55] += mv1.KVector2.Scalar14 * mv2.Scalar23456 - mv1.KVector2.Scalar24 * mv2.Scalar13456 + mv1.KVector2.Scalar34 * mv2.Scalar12456 - mv1.KVector2.Scalar45 * mv2.Scalar12346 + mv1.KVector2.Scalar46 * mv2.Scalar12345;
            tempScalar[59] += -mv1.KVector2.Scalar13 * mv2.Scalar23456 + mv1.KVector2.Scalar23 * mv2.Scalar13456 - mv1.KVector2.Scalar34 * mv2.Scalar12356 + mv1.KVector2.Scalar35 * mv2.Scalar12346 - mv1.KVector2.Scalar36 * mv2.Scalar12345;
            tempScalar[61] += mv1.KVector2.Scalar12 * mv2.Scalar23456 - mv1.KVector2.Scalar23 * mv2.Scalar12456 + mv1.KVector2.Scalar24 * mv2.Scalar12356 - mv1.KVector2.Scalar25 * mv2.Scalar12346 + mv1.KVector2.Scalar26 * mv2.Scalar12345;
            tempScalar[62] += -mv1.KVector2.Scalar12 * mv2.Scalar13456 + mv1.KVector2.Scalar13 * mv2.Scalar12456 - mv1.KVector2.Scalar14 * mv2.Scalar12356 + mv1.KVector2.Scalar15 * mv2.Scalar12346 - mv1.KVector2.Scalar16 * mv2.Scalar12345;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[15] += -mv1.KVector3.Scalar156 * mv2.Scalar23456 + mv1.KVector3.Scalar256 * mv2.Scalar13456 - mv1.KVector3.Scalar356 * mv2.Scalar12456 + mv1.KVector3.Scalar456 * mv2.Scalar12356;
            tempScalar[23] += mv1.KVector3.Scalar146 * mv2.Scalar23456 - mv1.KVector3.Scalar246 * mv2.Scalar13456 + mv1.KVector3.Scalar346 * mv2.Scalar12456 - mv1.KVector3.Scalar456 * mv2.Scalar12346;
            tempScalar[27] += -mv1.KVector3.Scalar136 * mv2.Scalar23456 + mv1.KVector3.Scalar236 * mv2.Scalar13456 - mv1.KVector3.Scalar346 * mv2.Scalar12356 + mv1.KVector3.Scalar356 * mv2.Scalar12346;
            tempScalar[29] += mv1.KVector3.Scalar126 * mv2.Scalar23456 - mv1.KVector3.Scalar236 * mv2.Scalar12456 + mv1.KVector3.Scalar246 * mv2.Scalar12356 - mv1.KVector3.Scalar256 * mv2.Scalar12346;
            tempScalar[30] += -mv1.KVector3.Scalar126 * mv2.Scalar13456 + mv1.KVector3.Scalar136 * mv2.Scalar12456 - mv1.KVector3.Scalar146 * mv2.Scalar12356 + mv1.KVector3.Scalar156 * mv2.Scalar12346;
            tempScalar[39] += -mv1.KVector3.Scalar145 * mv2.Scalar23456 + mv1.KVector3.Scalar245 * mv2.Scalar13456 - mv1.KVector3.Scalar345 * mv2.Scalar12456 + mv1.KVector3.Scalar456 * mv2.Scalar12345;
            tempScalar[43] += mv1.KVector3.Scalar135 * mv2.Scalar23456 - mv1.KVector3.Scalar235 * mv2.Scalar13456 + mv1.KVector3.Scalar345 * mv2.Scalar12356 - mv1.KVector3.Scalar356 * mv2.Scalar12345;
            tempScalar[45] += -mv1.KVector3.Scalar125 * mv2.Scalar23456 + mv1.KVector3.Scalar235 * mv2.Scalar12456 - mv1.KVector3.Scalar245 * mv2.Scalar12356 + mv1.KVector3.Scalar256 * mv2.Scalar12345;
            tempScalar[46] += mv1.KVector3.Scalar125 * mv2.Scalar13456 - mv1.KVector3.Scalar135 * mv2.Scalar12456 + mv1.KVector3.Scalar145 * mv2.Scalar12356 - mv1.KVector3.Scalar156 * mv2.Scalar12345;
            tempScalar[51] += -mv1.KVector3.Scalar134 * mv2.Scalar23456 + mv1.KVector3.Scalar234 * mv2.Scalar13456 - mv1.KVector3.Scalar345 * mv2.Scalar12346 + mv1.KVector3.Scalar346 * mv2.Scalar12345;
            tempScalar[53] += mv1.KVector3.Scalar124 * mv2.Scalar23456 - mv1.KVector3.Scalar234 * mv2.Scalar12456 + mv1.KVector3.Scalar245 * mv2.Scalar12346 - mv1.KVector3.Scalar246 * mv2.Scalar12345;
            tempScalar[54] += -mv1.KVector3.Scalar124 * mv2.Scalar13456 + mv1.KVector3.Scalar134 * mv2.Scalar12456 - mv1.KVector3.Scalar145 * mv2.Scalar12346 + mv1.KVector3.Scalar146 * mv2.Scalar12345;
            tempScalar[57] += -mv1.KVector3.Scalar123 * mv2.Scalar23456 + mv1.KVector3.Scalar234 * mv2.Scalar12356 - mv1.KVector3.Scalar235 * mv2.Scalar12346 + mv1.KVector3.Scalar236 * mv2.Scalar12345;
            tempScalar[58] += mv1.KVector3.Scalar123 * mv2.Scalar13456 - mv1.KVector3.Scalar134 * mv2.Scalar12356 + mv1.KVector3.Scalar135 * mv2.Scalar12346 - mv1.KVector3.Scalar136 * mv2.Scalar12345;
            tempScalar[60] += -mv1.KVector3.Scalar123 * mv2.Scalar12456 + mv1.KVector3.Scalar124 * mv2.Scalar12356 - mv1.KVector3.Scalar125 * mv2.Scalar12346 + mv1.KVector3.Scalar126 * mv2.Scalar12345;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[7] += -mv1.KVector4.Scalar1456 * mv2.Scalar23456 + mv1.KVector4.Scalar2456 * mv2.Scalar13456 - mv1.KVector4.Scalar3456 * mv2.Scalar12456;
            tempScalar[11] += mv1.KVector4.Scalar1356 * mv2.Scalar23456 - mv1.KVector4.Scalar2356 * mv2.Scalar13456 + mv1.KVector4.Scalar3456 * mv2.Scalar12356;
            tempScalar[13] += -mv1.KVector4.Scalar1256 * mv2.Scalar23456 + mv1.KVector4.Scalar2356 * mv2.Scalar12456 - mv1.KVector4.Scalar2456 * mv2.Scalar12356;
            tempScalar[14] += mv1.KVector4.Scalar1256 * mv2.Scalar13456 - mv1.KVector4.Scalar1356 * mv2.Scalar12456 + mv1.KVector4.Scalar1456 * mv2.Scalar12356;
            tempScalar[19] += -mv1.KVector4.Scalar1346 * mv2.Scalar23456 + mv1.KVector4.Scalar2346 * mv2.Scalar13456 - mv1.KVector4.Scalar3456 * mv2.Scalar12346;
            tempScalar[21] += mv1.KVector4.Scalar1246 * mv2.Scalar23456 - mv1.KVector4.Scalar2346 * mv2.Scalar12456 + mv1.KVector4.Scalar2456 * mv2.Scalar12346;
            tempScalar[22] += -mv1.KVector4.Scalar1246 * mv2.Scalar13456 + mv1.KVector4.Scalar1346 * mv2.Scalar12456 - mv1.KVector4.Scalar1456 * mv2.Scalar12346;
            tempScalar[25] += -mv1.KVector4.Scalar1236 * mv2.Scalar23456 + mv1.KVector4.Scalar2346 * mv2.Scalar12356 - mv1.KVector4.Scalar2356 * mv2.Scalar12346;
            tempScalar[26] += mv1.KVector4.Scalar1236 * mv2.Scalar13456 - mv1.KVector4.Scalar1346 * mv2.Scalar12356 + mv1.KVector4.Scalar1356 * mv2.Scalar12346;
            tempScalar[28] += -mv1.KVector4.Scalar1236 * mv2.Scalar12456 + mv1.KVector4.Scalar1246 * mv2.Scalar12356 - mv1.KVector4.Scalar1256 * mv2.Scalar12346;
            tempScalar[35] += mv1.KVector4.Scalar1345 * mv2.Scalar23456 - mv1.KVector4.Scalar2345 * mv2.Scalar13456 + mv1.KVector4.Scalar3456 * mv2.Scalar12345;
            tempScalar[37] += -mv1.KVector4.Scalar1245 * mv2.Scalar23456 + mv1.KVector4.Scalar2345 * mv2.Scalar12456 - mv1.KVector4.Scalar2456 * mv2.Scalar12345;
            tempScalar[38] += mv1.KVector4.Scalar1245 * mv2.Scalar13456 - mv1.KVector4.Scalar1345 * mv2.Scalar12456 + mv1.KVector4.Scalar1456 * mv2.Scalar12345;
            tempScalar[41] += mv1.KVector4.Scalar1235 * mv2.Scalar23456 - mv1.KVector4.Scalar2345 * mv2.Scalar12356 + mv1.KVector4.Scalar2356 * mv2.Scalar12345;
            tempScalar[42] += -mv1.KVector4.Scalar1235 * mv2.Scalar13456 + mv1.KVector4.Scalar1345 * mv2.Scalar12356 - mv1.KVector4.Scalar1356 * mv2.Scalar12345;
            tempScalar[44] += mv1.KVector4.Scalar1235 * mv2.Scalar12456 - mv1.KVector4.Scalar1245 * mv2.Scalar12356 + mv1.KVector4.Scalar1256 * mv2.Scalar12345;
            tempScalar[49] += -mv1.KVector4.Scalar1234 * mv2.Scalar23456 + mv1.KVector4.Scalar2345 * mv2.Scalar12346 - mv1.KVector4.Scalar2346 * mv2.Scalar12345;
            tempScalar[50] += mv1.KVector4.Scalar1234 * mv2.Scalar13456 - mv1.KVector4.Scalar1345 * mv2.Scalar12346 + mv1.KVector4.Scalar1346 * mv2.Scalar12345;
            tempScalar[52] += -mv1.KVector4.Scalar1234 * mv2.Scalar12456 + mv1.KVector4.Scalar1245 * mv2.Scalar12346 - mv1.KVector4.Scalar1246 * mv2.Scalar12345;
            tempScalar[56] += mv1.KVector4.Scalar1234 * mv2.Scalar12356 - mv1.KVector4.Scalar1235 * mv2.Scalar12346 + mv1.KVector4.Scalar1236 * mv2.Scalar12345;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[3] += mv1.KVector5.Scalar13456 * mv2.Scalar23456 - mv1.KVector5.Scalar23456 * mv2.Scalar13456;
            tempScalar[5] += -mv1.KVector5.Scalar12456 * mv2.Scalar23456 + mv1.KVector5.Scalar23456 * mv2.Scalar12456;
            tempScalar[6] += mv1.KVector5.Scalar12456 * mv2.Scalar13456 - mv1.KVector5.Scalar13456 * mv2.Scalar12456;
            tempScalar[9] += mv1.KVector5.Scalar12356 * mv2.Scalar23456 - mv1.KVector5.Scalar23456 * mv2.Scalar12356;
            tempScalar[10] += -mv1.KVector5.Scalar12356 * mv2.Scalar13456 + mv1.KVector5.Scalar13456 * mv2.Scalar12356;
            tempScalar[12] += mv1.KVector5.Scalar12356 * mv2.Scalar12456 - mv1.KVector5.Scalar12456 * mv2.Scalar12356;
            tempScalar[17] += -mv1.KVector5.Scalar12346 * mv2.Scalar23456 + mv1.KVector5.Scalar23456 * mv2.Scalar12346;
            tempScalar[18] += mv1.KVector5.Scalar12346 * mv2.Scalar13456 - mv1.KVector5.Scalar13456 * mv2.Scalar12346;
            tempScalar[20] += -mv1.KVector5.Scalar12346 * mv2.Scalar12456 + mv1.KVector5.Scalar12456 * mv2.Scalar12346;
            tempScalar[24] += mv1.KVector5.Scalar12346 * mv2.Scalar12356 - mv1.KVector5.Scalar12356 * mv2.Scalar12346;
            tempScalar[33] += mv1.KVector5.Scalar12345 * mv2.Scalar23456 - mv1.KVector5.Scalar23456 * mv2.Scalar12345;
            tempScalar[34] += -mv1.KVector5.Scalar12345 * mv2.Scalar13456 + mv1.KVector5.Scalar13456 * mv2.Scalar12345;
            tempScalar[36] += mv1.KVector5.Scalar12345 * mv2.Scalar12456 - mv1.KVector5.Scalar12456 * mv2.Scalar12345;
            tempScalar[40] += -mv1.KVector5.Scalar12345 * mv2.Scalar12356 + mv1.KVector5.Scalar12356 * mv2.Scalar12345;
            tempScalar[48] += mv1.KVector5.Scalar12345 * mv2.Scalar12346 - mv1.KVector5.Scalar12346 * mv2.Scalar12345;
        }
        
        if (!mv1.KVector6.IsZero())
        {
            tempScalar[1] += mv1.KVector6.Scalar123456 * mv2.Scalar23456;
            tempScalar[2] += -mv1.KVector6.Scalar123456 * mv2.Scalar13456;
            tempScalar[4] += mv1.KVector6.Scalar123456 * mv2.Scalar12456;
            tempScalar[8] += -mv1.KVector6.Scalar123456 * mv2.Scalar12356;
            tempScalar[16] += mv1.KVector6.Scalar123456 * mv2.Scalar12346;
            tempScalar[32] += -mv1.KVector6.Scalar123456 * mv2.Scalar12345;
        }
        
        return Ga6Multivector.Create(tempScalar);
    }
    
    public static Ga6Multivector Cp(this Ga6Multivector mv1, Ga6KVector6 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6Multivector.Zero;
        
        var tempScalar = new double[64];
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[31] += -mv1.KVector1.Scalar6 * mv2.Scalar123456;
            tempScalar[47] += mv1.KVector1.Scalar5 * mv2.Scalar123456;
            tempScalar[55] += -mv1.KVector1.Scalar4 * mv2.Scalar123456;
            tempScalar[59] += mv1.KVector1.Scalar3 * mv2.Scalar123456;
            tempScalar[61] += -mv1.KVector1.Scalar2 * mv2.Scalar123456;
            tempScalar[62] += mv1.KVector1.Scalar1 * mv2.Scalar123456;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += mv1.KVector3.Scalar456 * mv2.Scalar123456;
            tempScalar[11] += -mv1.KVector3.Scalar356 * mv2.Scalar123456;
            tempScalar[13] += mv1.KVector3.Scalar256 * mv2.Scalar123456;
            tempScalar[14] += -mv1.KVector3.Scalar156 * mv2.Scalar123456;
            tempScalar[19] += mv1.KVector3.Scalar346 * mv2.Scalar123456;
            tempScalar[21] += -mv1.KVector3.Scalar246 * mv2.Scalar123456;
            tempScalar[22] += mv1.KVector3.Scalar146 * mv2.Scalar123456;
            tempScalar[25] += mv1.KVector3.Scalar236 * mv2.Scalar123456;
            tempScalar[26] += -mv1.KVector3.Scalar136 * mv2.Scalar123456;
            tempScalar[28] += mv1.KVector3.Scalar126 * mv2.Scalar123456;
            tempScalar[35] += -mv1.KVector3.Scalar345 * mv2.Scalar123456;
            tempScalar[37] += mv1.KVector3.Scalar245 * mv2.Scalar123456;
            tempScalar[38] += -mv1.KVector3.Scalar145 * mv2.Scalar123456;
            tempScalar[41] += -mv1.KVector3.Scalar235 * mv2.Scalar123456;
            tempScalar[42] += mv1.KVector3.Scalar135 * mv2.Scalar123456;
            tempScalar[44] += -mv1.KVector3.Scalar125 * mv2.Scalar123456;
            tempScalar[49] += mv1.KVector3.Scalar234 * mv2.Scalar123456;
            tempScalar[50] += -mv1.KVector3.Scalar134 * mv2.Scalar123456;
            tempScalar[52] += mv1.KVector3.Scalar124 * mv2.Scalar123456;
            tempScalar[56] += -mv1.KVector3.Scalar123 * mv2.Scalar123456;
        }
        
        if (!mv1.KVector5.IsZero())
        {
            tempScalar[1] += -mv1.KVector5.Scalar23456 * mv2.Scalar123456;
            tempScalar[2] += mv1.KVector5.Scalar13456 * mv2.Scalar123456;
            tempScalar[4] += -mv1.KVector5.Scalar12456 * mv2.Scalar123456;
            tempScalar[8] += mv1.KVector5.Scalar12356 * mv2.Scalar123456;
            tempScalar[16] += -mv1.KVector5.Scalar12346 * mv2.Scalar123456;
            tempScalar[32] += mv1.KVector5.Scalar12345 * mv2.Scalar123456;
        }
        
        return Ga6Multivector.Create(tempScalar);
    }
    
    public static Ga6Multivector Cp(this Ga6Multivector mv1, Ga6Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga6Multivector.Zero;
        
        var tempScalar = new double[64];
        
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
                tempScalar[17] += mv1.KVector1.Scalar1 * mv2.KVector1.Scalar5 - mv1.KVector1.Scalar5 * mv2.KVector1.Scalar1;
                tempScalar[18] += mv1.KVector1.Scalar2 * mv2.KVector1.Scalar5 - mv1.KVector1.Scalar5 * mv2.KVector1.Scalar2;
                tempScalar[20] += mv1.KVector1.Scalar3 * mv2.KVector1.Scalar5 - mv1.KVector1.Scalar5 * mv2.KVector1.Scalar3;
                tempScalar[24] += mv1.KVector1.Scalar4 * mv2.KVector1.Scalar5 - mv1.KVector1.Scalar5 * mv2.KVector1.Scalar4;
                tempScalar[33] += mv1.KVector1.Scalar1 * mv2.KVector1.Scalar6 - mv1.KVector1.Scalar6 * mv2.KVector1.Scalar1;
                tempScalar[34] += mv1.KVector1.Scalar2 * mv2.KVector1.Scalar6 - mv1.KVector1.Scalar6 * mv2.KVector1.Scalar2;
                tempScalar[36] += mv1.KVector1.Scalar3 * mv2.KVector1.Scalar6 - mv1.KVector1.Scalar6 * mv2.KVector1.Scalar3;
                tempScalar[40] += mv1.KVector1.Scalar4 * mv2.KVector1.Scalar6 - mv1.KVector1.Scalar6 * mv2.KVector1.Scalar4;
                tempScalar[48] += mv1.KVector1.Scalar5 * mv2.KVector1.Scalar6 - mv1.KVector1.Scalar6 * mv2.KVector1.Scalar5;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[1] += -mv1.KVector1.Scalar2 * mv2.KVector2.Scalar12 - mv1.KVector1.Scalar3 * mv2.KVector2.Scalar13 - mv1.KVector1.Scalar4 * mv2.KVector2.Scalar14 - mv1.KVector1.Scalar5 * mv2.KVector2.Scalar15 - mv1.KVector1.Scalar6 * mv2.KVector2.Scalar16;
                tempScalar[2] += mv1.KVector1.Scalar1 * mv2.KVector2.Scalar12 - mv1.KVector1.Scalar3 * mv2.KVector2.Scalar23 - mv1.KVector1.Scalar4 * mv2.KVector2.Scalar24 - mv1.KVector1.Scalar5 * mv2.KVector2.Scalar25 - mv1.KVector1.Scalar6 * mv2.KVector2.Scalar26;
                tempScalar[4] += mv1.KVector1.Scalar1 * mv2.KVector2.Scalar13 + mv1.KVector1.Scalar2 * mv2.KVector2.Scalar23 - mv1.KVector1.Scalar4 * mv2.KVector2.Scalar34 - mv1.KVector1.Scalar5 * mv2.KVector2.Scalar35 - mv1.KVector1.Scalar6 * mv2.KVector2.Scalar36;
                tempScalar[8] += mv1.KVector1.Scalar1 * mv2.KVector2.Scalar14 + mv1.KVector1.Scalar2 * mv2.KVector2.Scalar24 + mv1.KVector1.Scalar3 * mv2.KVector2.Scalar34 - mv1.KVector1.Scalar5 * mv2.KVector2.Scalar45 - mv1.KVector1.Scalar6 * mv2.KVector2.Scalar46;
                tempScalar[16] += mv1.KVector1.Scalar1 * mv2.KVector2.Scalar15 + mv1.KVector1.Scalar2 * mv2.KVector2.Scalar25 + mv1.KVector1.Scalar3 * mv2.KVector2.Scalar35 + mv1.KVector1.Scalar4 * mv2.KVector2.Scalar45 - mv1.KVector1.Scalar6 * mv2.KVector2.Scalar56;
                tempScalar[32] += mv1.KVector1.Scalar1 * mv2.KVector2.Scalar16 + mv1.KVector1.Scalar2 * mv2.KVector2.Scalar26 + mv1.KVector1.Scalar3 * mv2.KVector2.Scalar36 + mv1.KVector1.Scalar4 * mv2.KVector2.Scalar46 + mv1.KVector1.Scalar5 * mv2.KVector2.Scalar56;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[15] += mv1.KVector1.Scalar1 * mv2.KVector3.Scalar234 - mv1.KVector1.Scalar2 * mv2.KVector3.Scalar134 + mv1.KVector1.Scalar3 * mv2.KVector3.Scalar124 - mv1.KVector1.Scalar4 * mv2.KVector3.Scalar123;
                tempScalar[23] += mv1.KVector1.Scalar1 * mv2.KVector3.Scalar235 - mv1.KVector1.Scalar2 * mv2.KVector3.Scalar135 + mv1.KVector1.Scalar3 * mv2.KVector3.Scalar125 - mv1.KVector1.Scalar5 * mv2.KVector3.Scalar123;
                tempScalar[27] += mv1.KVector1.Scalar1 * mv2.KVector3.Scalar245 - mv1.KVector1.Scalar2 * mv2.KVector3.Scalar145 + mv1.KVector1.Scalar4 * mv2.KVector3.Scalar125 - mv1.KVector1.Scalar5 * mv2.KVector3.Scalar124;
                tempScalar[29] += mv1.KVector1.Scalar1 * mv2.KVector3.Scalar345 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar145 + mv1.KVector1.Scalar4 * mv2.KVector3.Scalar135 - mv1.KVector1.Scalar5 * mv2.KVector3.Scalar134;
                tempScalar[30] += mv1.KVector1.Scalar2 * mv2.KVector3.Scalar345 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar245 + mv1.KVector1.Scalar4 * mv2.KVector3.Scalar235 - mv1.KVector1.Scalar5 * mv2.KVector3.Scalar234;
                tempScalar[39] += mv1.KVector1.Scalar1 * mv2.KVector3.Scalar236 - mv1.KVector1.Scalar2 * mv2.KVector3.Scalar136 + mv1.KVector1.Scalar3 * mv2.KVector3.Scalar126 - mv1.KVector1.Scalar6 * mv2.KVector3.Scalar123;
                tempScalar[43] += mv1.KVector1.Scalar1 * mv2.KVector3.Scalar246 - mv1.KVector1.Scalar2 * mv2.KVector3.Scalar146 + mv1.KVector1.Scalar4 * mv2.KVector3.Scalar126 - mv1.KVector1.Scalar6 * mv2.KVector3.Scalar124;
                tempScalar[45] += mv1.KVector1.Scalar1 * mv2.KVector3.Scalar346 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar146 + mv1.KVector1.Scalar4 * mv2.KVector3.Scalar136 - mv1.KVector1.Scalar6 * mv2.KVector3.Scalar134;
                tempScalar[46] += mv1.KVector1.Scalar2 * mv2.KVector3.Scalar346 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar246 + mv1.KVector1.Scalar4 * mv2.KVector3.Scalar236 - mv1.KVector1.Scalar6 * mv2.KVector3.Scalar234;
                tempScalar[51] += mv1.KVector1.Scalar1 * mv2.KVector3.Scalar256 - mv1.KVector1.Scalar2 * mv2.KVector3.Scalar156 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar126 - mv1.KVector1.Scalar6 * mv2.KVector3.Scalar125;
                tempScalar[53] += mv1.KVector1.Scalar1 * mv2.KVector3.Scalar356 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar156 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar136 - mv1.KVector1.Scalar6 * mv2.KVector3.Scalar135;
                tempScalar[54] += mv1.KVector1.Scalar2 * mv2.KVector3.Scalar356 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar256 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar236 - mv1.KVector1.Scalar6 * mv2.KVector3.Scalar235;
                tempScalar[57] += mv1.KVector1.Scalar1 * mv2.KVector3.Scalar456 - mv1.KVector1.Scalar4 * mv2.KVector3.Scalar156 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar146 - mv1.KVector1.Scalar6 * mv2.KVector3.Scalar145;
                tempScalar[58] += mv1.KVector1.Scalar2 * mv2.KVector3.Scalar456 - mv1.KVector1.Scalar4 * mv2.KVector3.Scalar256 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar246 - mv1.KVector1.Scalar6 * mv2.KVector3.Scalar245;
                tempScalar[60] += mv1.KVector1.Scalar3 * mv2.KVector3.Scalar456 - mv1.KVector1.Scalar4 * mv2.KVector3.Scalar356 + mv1.KVector1.Scalar5 * mv2.KVector3.Scalar346 - mv1.KVector1.Scalar6 * mv2.KVector3.Scalar345;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[7] += -mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1234 - mv1.KVector1.Scalar5 * mv2.KVector4.Scalar1235 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar1236;
                tempScalar[11] += mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1234 - mv1.KVector1.Scalar5 * mv2.KVector4.Scalar1245 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar1246;
                tempScalar[13] += -mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1234 - mv1.KVector1.Scalar5 * mv2.KVector4.Scalar1345 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar1346;
                tempScalar[14] += mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1234 - mv1.KVector1.Scalar5 * mv2.KVector4.Scalar2345 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar2346;
                tempScalar[19] += mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1235 + mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1245 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar1256;
                tempScalar[21] += -mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1235 + mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1345 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar1356;
                tempScalar[22] += mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1235 + mv1.KVector1.Scalar4 * mv2.KVector4.Scalar2345 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar2356;
                tempScalar[25] += -mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1245 - mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1345 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar1456;
                tempScalar[26] += mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1245 - mv1.KVector1.Scalar3 * mv2.KVector4.Scalar2345 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar2456;
                tempScalar[28] += mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1345 + mv1.KVector1.Scalar2 * mv2.KVector4.Scalar2345 - mv1.KVector1.Scalar6 * mv2.KVector4.Scalar3456;
                tempScalar[35] += mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1236 + mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1246 + mv1.KVector1.Scalar5 * mv2.KVector4.Scalar1256;
                tempScalar[37] += -mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1236 + mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1346 + mv1.KVector1.Scalar5 * mv2.KVector4.Scalar1356;
                tempScalar[38] += mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1236 + mv1.KVector1.Scalar4 * mv2.KVector4.Scalar2346 + mv1.KVector1.Scalar5 * mv2.KVector4.Scalar2356;
                tempScalar[41] += -mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1246 - mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1346 + mv1.KVector1.Scalar5 * mv2.KVector4.Scalar1456;
                tempScalar[42] += mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1246 - mv1.KVector1.Scalar3 * mv2.KVector4.Scalar2346 + mv1.KVector1.Scalar5 * mv2.KVector4.Scalar2456;
                tempScalar[44] += mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1346 + mv1.KVector1.Scalar2 * mv2.KVector4.Scalar2346 + mv1.KVector1.Scalar5 * mv2.KVector4.Scalar3456;
                tempScalar[49] += -mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1256 - mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1356 - mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1456;
                tempScalar[50] += mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1256 - mv1.KVector1.Scalar3 * mv2.KVector4.Scalar2356 - mv1.KVector1.Scalar4 * mv2.KVector4.Scalar2456;
                tempScalar[52] += mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1356 + mv1.KVector1.Scalar2 * mv2.KVector4.Scalar2356 - mv1.KVector1.Scalar4 * mv2.KVector4.Scalar3456;
                tempScalar[56] += mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1456 + mv1.KVector1.Scalar2 * mv2.KVector4.Scalar2456 + mv1.KVector1.Scalar3 * mv2.KVector4.Scalar3456;
            }
            
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[63] += mv1.KVector1.Scalar1 * mv2.KVector5.Scalar23456 - mv1.KVector1.Scalar2 * mv2.KVector5.Scalar13456 + mv1.KVector1.Scalar3 * mv2.KVector5.Scalar12456 - mv1.KVector1.Scalar4 * mv2.KVector5.Scalar12356 + mv1.KVector1.Scalar5 * mv2.KVector5.Scalar12346 - mv1.KVector1.Scalar6 * mv2.KVector5.Scalar12345;
            }
            
            if (!mv2.KVector6.IsZero())
            {
                tempScalar[31] += -mv1.KVector1.Scalar6 * mv2.KVector6.Scalar123456;
                tempScalar[47] += mv1.KVector1.Scalar5 * mv2.KVector6.Scalar123456;
                tempScalar[55] += -mv1.KVector1.Scalar4 * mv2.KVector6.Scalar123456;
                tempScalar[59] += mv1.KVector1.Scalar3 * mv2.KVector6.Scalar123456;
                tempScalar[61] += -mv1.KVector1.Scalar2 * mv2.KVector6.Scalar123456;
                tempScalar[62] += mv1.KVector1.Scalar1 * mv2.KVector6.Scalar123456;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[1] += mv1.KVector2.Scalar12 * mv2.KVector1.Scalar2 + mv1.KVector2.Scalar13 * mv2.KVector1.Scalar3 + mv1.KVector2.Scalar14 * mv2.KVector1.Scalar4 + mv1.KVector2.Scalar15 * mv2.KVector1.Scalar5 + mv1.KVector2.Scalar16 * mv2.KVector1.Scalar6;
                tempScalar[2] += -mv1.KVector2.Scalar12 * mv2.KVector1.Scalar1 + mv1.KVector2.Scalar23 * mv2.KVector1.Scalar3 + mv1.KVector2.Scalar24 * mv2.KVector1.Scalar4 + mv1.KVector2.Scalar25 * mv2.KVector1.Scalar5 + mv1.KVector2.Scalar26 * mv2.KVector1.Scalar6;
                tempScalar[4] += -mv1.KVector2.Scalar13 * mv2.KVector1.Scalar1 - mv1.KVector2.Scalar23 * mv2.KVector1.Scalar2 + mv1.KVector2.Scalar34 * mv2.KVector1.Scalar4 + mv1.KVector2.Scalar35 * mv2.KVector1.Scalar5 + mv1.KVector2.Scalar36 * mv2.KVector1.Scalar6;
                tempScalar[8] += -mv1.KVector2.Scalar14 * mv2.KVector1.Scalar1 - mv1.KVector2.Scalar24 * mv2.KVector1.Scalar2 - mv1.KVector2.Scalar34 * mv2.KVector1.Scalar3 + mv1.KVector2.Scalar45 * mv2.KVector1.Scalar5 + mv1.KVector2.Scalar46 * mv2.KVector1.Scalar6;
                tempScalar[16] += -mv1.KVector2.Scalar15 * mv2.KVector1.Scalar1 - mv1.KVector2.Scalar25 * mv2.KVector1.Scalar2 - mv1.KVector2.Scalar35 * mv2.KVector1.Scalar3 - mv1.KVector2.Scalar45 * mv2.KVector1.Scalar4 + mv1.KVector2.Scalar56 * mv2.KVector1.Scalar6;
                tempScalar[32] += -mv1.KVector2.Scalar16 * mv2.KVector1.Scalar1 - mv1.KVector2.Scalar26 * mv2.KVector1.Scalar2 - mv1.KVector2.Scalar36 * mv2.KVector1.Scalar3 - mv1.KVector2.Scalar46 * mv2.KVector1.Scalar4 - mv1.KVector2.Scalar56 * mv2.KVector1.Scalar5;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[3] += -mv1.KVector2.Scalar13 * mv2.KVector2.Scalar23 + mv1.KVector2.Scalar23 * mv2.KVector2.Scalar13 - mv1.KVector2.Scalar14 * mv2.KVector2.Scalar24 + mv1.KVector2.Scalar24 * mv2.KVector2.Scalar14 - mv1.KVector2.Scalar15 * mv2.KVector2.Scalar25 + mv1.KVector2.Scalar25 * mv2.KVector2.Scalar15 - mv1.KVector2.Scalar16 * mv2.KVector2.Scalar26 + mv1.KVector2.Scalar26 * mv2.KVector2.Scalar16;
                tempScalar[5] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar23 - mv1.KVector2.Scalar23 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar14 * mv2.KVector2.Scalar34 + mv1.KVector2.Scalar34 * mv2.KVector2.Scalar14 - mv1.KVector2.Scalar15 * mv2.KVector2.Scalar35 + mv1.KVector2.Scalar35 * mv2.KVector2.Scalar15 - mv1.KVector2.Scalar16 * mv2.KVector2.Scalar36 + mv1.KVector2.Scalar36 * mv2.KVector2.Scalar16;
                tempScalar[6] += -mv1.KVector2.Scalar12 * mv2.KVector2.Scalar13 + mv1.KVector2.Scalar13 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar24 * mv2.KVector2.Scalar34 + mv1.KVector2.Scalar34 * mv2.KVector2.Scalar24 - mv1.KVector2.Scalar25 * mv2.KVector2.Scalar35 + mv1.KVector2.Scalar35 * mv2.KVector2.Scalar25 - mv1.KVector2.Scalar26 * mv2.KVector2.Scalar36 + mv1.KVector2.Scalar36 * mv2.KVector2.Scalar26;
                tempScalar[9] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar24 + mv1.KVector2.Scalar13 * mv2.KVector2.Scalar34 - mv1.KVector2.Scalar24 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar34 * mv2.KVector2.Scalar13 - mv1.KVector2.Scalar15 * mv2.KVector2.Scalar45 + mv1.KVector2.Scalar45 * mv2.KVector2.Scalar15 - mv1.KVector2.Scalar16 * mv2.KVector2.Scalar46 + mv1.KVector2.Scalar46 * mv2.KVector2.Scalar16;
                tempScalar[10] += -mv1.KVector2.Scalar12 * mv2.KVector2.Scalar14 + mv1.KVector2.Scalar23 * mv2.KVector2.Scalar34 + mv1.KVector2.Scalar14 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar34 * mv2.KVector2.Scalar23 - mv1.KVector2.Scalar25 * mv2.KVector2.Scalar45 + mv1.KVector2.Scalar45 * mv2.KVector2.Scalar25 - mv1.KVector2.Scalar26 * mv2.KVector2.Scalar46 + mv1.KVector2.Scalar46 * mv2.KVector2.Scalar26;
                tempScalar[12] += -mv1.KVector2.Scalar13 * mv2.KVector2.Scalar14 - mv1.KVector2.Scalar23 * mv2.KVector2.Scalar24 + mv1.KVector2.Scalar14 * mv2.KVector2.Scalar13 + mv1.KVector2.Scalar24 * mv2.KVector2.Scalar23 - mv1.KVector2.Scalar35 * mv2.KVector2.Scalar45 + mv1.KVector2.Scalar45 * mv2.KVector2.Scalar35 - mv1.KVector2.Scalar36 * mv2.KVector2.Scalar46 + mv1.KVector2.Scalar46 * mv2.KVector2.Scalar36;
                tempScalar[17] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar25 + mv1.KVector2.Scalar13 * mv2.KVector2.Scalar35 + mv1.KVector2.Scalar14 * mv2.KVector2.Scalar45 - mv1.KVector2.Scalar25 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar35 * mv2.KVector2.Scalar13 - mv1.KVector2.Scalar45 * mv2.KVector2.Scalar14 - mv1.KVector2.Scalar16 * mv2.KVector2.Scalar56 + mv1.KVector2.Scalar56 * mv2.KVector2.Scalar16;
                tempScalar[18] += -mv1.KVector2.Scalar12 * mv2.KVector2.Scalar15 + mv1.KVector2.Scalar23 * mv2.KVector2.Scalar35 + mv1.KVector2.Scalar24 * mv2.KVector2.Scalar45 + mv1.KVector2.Scalar15 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar35 * mv2.KVector2.Scalar23 - mv1.KVector2.Scalar45 * mv2.KVector2.Scalar24 - mv1.KVector2.Scalar26 * mv2.KVector2.Scalar56 + mv1.KVector2.Scalar56 * mv2.KVector2.Scalar26;
                tempScalar[20] += -mv1.KVector2.Scalar13 * mv2.KVector2.Scalar15 - mv1.KVector2.Scalar23 * mv2.KVector2.Scalar25 + mv1.KVector2.Scalar34 * mv2.KVector2.Scalar45 + mv1.KVector2.Scalar15 * mv2.KVector2.Scalar13 + mv1.KVector2.Scalar25 * mv2.KVector2.Scalar23 - mv1.KVector2.Scalar45 * mv2.KVector2.Scalar34 - mv1.KVector2.Scalar36 * mv2.KVector2.Scalar56 + mv1.KVector2.Scalar56 * mv2.KVector2.Scalar36;
                tempScalar[24] += -mv1.KVector2.Scalar14 * mv2.KVector2.Scalar15 - mv1.KVector2.Scalar24 * mv2.KVector2.Scalar25 - mv1.KVector2.Scalar34 * mv2.KVector2.Scalar35 + mv1.KVector2.Scalar15 * mv2.KVector2.Scalar14 + mv1.KVector2.Scalar25 * mv2.KVector2.Scalar24 + mv1.KVector2.Scalar35 * mv2.KVector2.Scalar34 - mv1.KVector2.Scalar46 * mv2.KVector2.Scalar56 + mv1.KVector2.Scalar56 * mv2.KVector2.Scalar46;
                tempScalar[33] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar26 + mv1.KVector2.Scalar13 * mv2.KVector2.Scalar36 + mv1.KVector2.Scalar14 * mv2.KVector2.Scalar46 + mv1.KVector2.Scalar15 * mv2.KVector2.Scalar56 - mv1.KVector2.Scalar26 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar36 * mv2.KVector2.Scalar13 - mv1.KVector2.Scalar46 * mv2.KVector2.Scalar14 - mv1.KVector2.Scalar56 * mv2.KVector2.Scalar15;
                tempScalar[34] += -mv1.KVector2.Scalar12 * mv2.KVector2.Scalar16 + mv1.KVector2.Scalar23 * mv2.KVector2.Scalar36 + mv1.KVector2.Scalar24 * mv2.KVector2.Scalar46 + mv1.KVector2.Scalar25 * mv2.KVector2.Scalar56 + mv1.KVector2.Scalar16 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar36 * mv2.KVector2.Scalar23 - mv1.KVector2.Scalar46 * mv2.KVector2.Scalar24 - mv1.KVector2.Scalar56 * mv2.KVector2.Scalar25;
                tempScalar[36] += -mv1.KVector2.Scalar13 * mv2.KVector2.Scalar16 - mv1.KVector2.Scalar23 * mv2.KVector2.Scalar26 + mv1.KVector2.Scalar34 * mv2.KVector2.Scalar46 + mv1.KVector2.Scalar35 * mv2.KVector2.Scalar56 + mv1.KVector2.Scalar16 * mv2.KVector2.Scalar13 + mv1.KVector2.Scalar26 * mv2.KVector2.Scalar23 - mv1.KVector2.Scalar46 * mv2.KVector2.Scalar34 - mv1.KVector2.Scalar56 * mv2.KVector2.Scalar35;
                tempScalar[40] += -mv1.KVector2.Scalar14 * mv2.KVector2.Scalar16 - mv1.KVector2.Scalar24 * mv2.KVector2.Scalar26 - mv1.KVector2.Scalar34 * mv2.KVector2.Scalar36 + mv1.KVector2.Scalar45 * mv2.KVector2.Scalar56 + mv1.KVector2.Scalar16 * mv2.KVector2.Scalar14 + mv1.KVector2.Scalar26 * mv2.KVector2.Scalar24 + mv1.KVector2.Scalar36 * mv2.KVector2.Scalar34 - mv1.KVector2.Scalar56 * mv2.KVector2.Scalar45;
                tempScalar[48] += -mv1.KVector2.Scalar15 * mv2.KVector2.Scalar16 - mv1.KVector2.Scalar25 * mv2.KVector2.Scalar26 - mv1.KVector2.Scalar35 * mv2.KVector2.Scalar36 - mv1.KVector2.Scalar45 * mv2.KVector2.Scalar46 + mv1.KVector2.Scalar16 * mv2.KVector2.Scalar15 + mv1.KVector2.Scalar26 * mv2.KVector2.Scalar25 + mv1.KVector2.Scalar36 * mv2.KVector2.Scalar35 + mv1.KVector2.Scalar46 * mv2.KVector2.Scalar45;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[7] += mv1.KVector2.Scalar14 * mv2.KVector3.Scalar234 - mv1.KVector2.Scalar24 * mv2.KVector3.Scalar134 + mv1.KVector2.Scalar34 * mv2.KVector3.Scalar124 + mv1.KVector2.Scalar15 * mv2.KVector3.Scalar235 - mv1.KVector2.Scalar25 * mv2.KVector3.Scalar135 + mv1.KVector2.Scalar35 * mv2.KVector3.Scalar125 + mv1.KVector2.Scalar16 * mv2.KVector3.Scalar236 - mv1.KVector2.Scalar26 * mv2.KVector3.Scalar136 + mv1.KVector2.Scalar36 * mv2.KVector3.Scalar126;
                tempScalar[11] += -mv1.KVector2.Scalar13 * mv2.KVector3.Scalar234 + mv1.KVector2.Scalar23 * mv2.KVector3.Scalar134 - mv1.KVector2.Scalar34 * mv2.KVector3.Scalar123 + mv1.KVector2.Scalar15 * mv2.KVector3.Scalar245 - mv1.KVector2.Scalar25 * mv2.KVector3.Scalar145 + mv1.KVector2.Scalar45 * mv2.KVector3.Scalar125 + mv1.KVector2.Scalar16 * mv2.KVector3.Scalar246 - mv1.KVector2.Scalar26 * mv2.KVector3.Scalar146 + mv1.KVector2.Scalar46 * mv2.KVector3.Scalar126;
                tempScalar[13] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar234 - mv1.KVector2.Scalar23 * mv2.KVector3.Scalar124 + mv1.KVector2.Scalar24 * mv2.KVector3.Scalar123 + mv1.KVector2.Scalar15 * mv2.KVector3.Scalar345 - mv1.KVector2.Scalar35 * mv2.KVector3.Scalar145 + mv1.KVector2.Scalar45 * mv2.KVector3.Scalar135 + mv1.KVector2.Scalar16 * mv2.KVector3.Scalar346 - mv1.KVector2.Scalar36 * mv2.KVector3.Scalar146 + mv1.KVector2.Scalar46 * mv2.KVector3.Scalar136;
                tempScalar[14] += -mv1.KVector2.Scalar12 * mv2.KVector3.Scalar134 + mv1.KVector2.Scalar13 * mv2.KVector3.Scalar124 - mv1.KVector2.Scalar14 * mv2.KVector3.Scalar123 + mv1.KVector2.Scalar25 * mv2.KVector3.Scalar345 - mv1.KVector2.Scalar35 * mv2.KVector3.Scalar245 + mv1.KVector2.Scalar45 * mv2.KVector3.Scalar235 + mv1.KVector2.Scalar26 * mv2.KVector3.Scalar346 - mv1.KVector2.Scalar36 * mv2.KVector3.Scalar246 + mv1.KVector2.Scalar46 * mv2.KVector3.Scalar236;
                tempScalar[19] += -mv1.KVector2.Scalar13 * mv2.KVector3.Scalar235 + mv1.KVector2.Scalar23 * mv2.KVector3.Scalar135 - mv1.KVector2.Scalar14 * mv2.KVector3.Scalar245 + mv1.KVector2.Scalar24 * mv2.KVector3.Scalar145 - mv1.KVector2.Scalar35 * mv2.KVector3.Scalar123 - mv1.KVector2.Scalar45 * mv2.KVector3.Scalar124 + mv1.KVector2.Scalar16 * mv2.KVector3.Scalar256 - mv1.KVector2.Scalar26 * mv2.KVector3.Scalar156 + mv1.KVector2.Scalar56 * mv2.KVector3.Scalar126;
                tempScalar[21] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar235 - mv1.KVector2.Scalar23 * mv2.KVector3.Scalar125 - mv1.KVector2.Scalar14 * mv2.KVector3.Scalar345 + mv1.KVector2.Scalar34 * mv2.KVector3.Scalar145 + mv1.KVector2.Scalar25 * mv2.KVector3.Scalar123 - mv1.KVector2.Scalar45 * mv2.KVector3.Scalar134 + mv1.KVector2.Scalar16 * mv2.KVector3.Scalar356 - mv1.KVector2.Scalar36 * mv2.KVector3.Scalar156 + mv1.KVector2.Scalar56 * mv2.KVector3.Scalar136;
                tempScalar[22] += -mv1.KVector2.Scalar12 * mv2.KVector3.Scalar135 + mv1.KVector2.Scalar13 * mv2.KVector3.Scalar125 - mv1.KVector2.Scalar24 * mv2.KVector3.Scalar345 + mv1.KVector2.Scalar34 * mv2.KVector3.Scalar245 - mv1.KVector2.Scalar15 * mv2.KVector3.Scalar123 - mv1.KVector2.Scalar45 * mv2.KVector3.Scalar234 + mv1.KVector2.Scalar26 * mv2.KVector3.Scalar356 - mv1.KVector2.Scalar36 * mv2.KVector3.Scalar256 + mv1.KVector2.Scalar56 * mv2.KVector3.Scalar236;
                tempScalar[25] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar245 + mv1.KVector2.Scalar13 * mv2.KVector3.Scalar345 - mv1.KVector2.Scalar24 * mv2.KVector3.Scalar125 - mv1.KVector2.Scalar34 * mv2.KVector3.Scalar135 + mv1.KVector2.Scalar25 * mv2.KVector3.Scalar124 + mv1.KVector2.Scalar35 * mv2.KVector3.Scalar134 + mv1.KVector2.Scalar16 * mv2.KVector3.Scalar456 - mv1.KVector2.Scalar46 * mv2.KVector3.Scalar156 + mv1.KVector2.Scalar56 * mv2.KVector3.Scalar146;
                tempScalar[26] += -mv1.KVector2.Scalar12 * mv2.KVector3.Scalar145 + mv1.KVector2.Scalar23 * mv2.KVector3.Scalar345 + mv1.KVector2.Scalar14 * mv2.KVector3.Scalar125 - mv1.KVector2.Scalar34 * mv2.KVector3.Scalar235 - mv1.KVector2.Scalar15 * mv2.KVector3.Scalar124 + mv1.KVector2.Scalar35 * mv2.KVector3.Scalar234 + mv1.KVector2.Scalar26 * mv2.KVector3.Scalar456 - mv1.KVector2.Scalar46 * mv2.KVector3.Scalar256 + mv1.KVector2.Scalar56 * mv2.KVector3.Scalar246;
                tempScalar[28] += -mv1.KVector2.Scalar13 * mv2.KVector3.Scalar145 - mv1.KVector2.Scalar23 * mv2.KVector3.Scalar245 + mv1.KVector2.Scalar14 * mv2.KVector3.Scalar135 + mv1.KVector2.Scalar24 * mv2.KVector3.Scalar235 - mv1.KVector2.Scalar15 * mv2.KVector3.Scalar134 - mv1.KVector2.Scalar25 * mv2.KVector3.Scalar234 + mv1.KVector2.Scalar36 * mv2.KVector3.Scalar456 - mv1.KVector2.Scalar46 * mv2.KVector3.Scalar356 + mv1.KVector2.Scalar56 * mv2.KVector3.Scalar346;
                tempScalar[35] += -mv1.KVector2.Scalar13 * mv2.KVector3.Scalar236 + mv1.KVector2.Scalar23 * mv2.KVector3.Scalar136 - mv1.KVector2.Scalar14 * mv2.KVector3.Scalar246 + mv1.KVector2.Scalar24 * mv2.KVector3.Scalar146 - mv1.KVector2.Scalar15 * mv2.KVector3.Scalar256 + mv1.KVector2.Scalar25 * mv2.KVector3.Scalar156 - mv1.KVector2.Scalar36 * mv2.KVector3.Scalar123 - mv1.KVector2.Scalar46 * mv2.KVector3.Scalar124 - mv1.KVector2.Scalar56 * mv2.KVector3.Scalar125;
                tempScalar[37] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar236 - mv1.KVector2.Scalar23 * mv2.KVector3.Scalar126 - mv1.KVector2.Scalar14 * mv2.KVector3.Scalar346 + mv1.KVector2.Scalar34 * mv2.KVector3.Scalar146 - mv1.KVector2.Scalar15 * mv2.KVector3.Scalar356 + mv1.KVector2.Scalar35 * mv2.KVector3.Scalar156 + mv1.KVector2.Scalar26 * mv2.KVector3.Scalar123 - mv1.KVector2.Scalar46 * mv2.KVector3.Scalar134 - mv1.KVector2.Scalar56 * mv2.KVector3.Scalar135;
                tempScalar[38] += -mv1.KVector2.Scalar12 * mv2.KVector3.Scalar136 + mv1.KVector2.Scalar13 * mv2.KVector3.Scalar126 - mv1.KVector2.Scalar24 * mv2.KVector3.Scalar346 + mv1.KVector2.Scalar34 * mv2.KVector3.Scalar246 - mv1.KVector2.Scalar25 * mv2.KVector3.Scalar356 + mv1.KVector2.Scalar35 * mv2.KVector3.Scalar256 - mv1.KVector2.Scalar16 * mv2.KVector3.Scalar123 - mv1.KVector2.Scalar46 * mv2.KVector3.Scalar234 - mv1.KVector2.Scalar56 * mv2.KVector3.Scalar235;
                tempScalar[41] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar246 + mv1.KVector2.Scalar13 * mv2.KVector3.Scalar346 - mv1.KVector2.Scalar24 * mv2.KVector3.Scalar126 - mv1.KVector2.Scalar34 * mv2.KVector3.Scalar136 - mv1.KVector2.Scalar15 * mv2.KVector3.Scalar456 + mv1.KVector2.Scalar45 * mv2.KVector3.Scalar156 + mv1.KVector2.Scalar26 * mv2.KVector3.Scalar124 + mv1.KVector2.Scalar36 * mv2.KVector3.Scalar134 - mv1.KVector2.Scalar56 * mv2.KVector3.Scalar145;
                tempScalar[42] += -mv1.KVector2.Scalar12 * mv2.KVector3.Scalar146 + mv1.KVector2.Scalar23 * mv2.KVector3.Scalar346 + mv1.KVector2.Scalar14 * mv2.KVector3.Scalar126 - mv1.KVector2.Scalar34 * mv2.KVector3.Scalar236 - mv1.KVector2.Scalar25 * mv2.KVector3.Scalar456 + mv1.KVector2.Scalar45 * mv2.KVector3.Scalar256 - mv1.KVector2.Scalar16 * mv2.KVector3.Scalar124 + mv1.KVector2.Scalar36 * mv2.KVector3.Scalar234 - mv1.KVector2.Scalar56 * mv2.KVector3.Scalar245;
                tempScalar[44] += -mv1.KVector2.Scalar13 * mv2.KVector3.Scalar146 - mv1.KVector2.Scalar23 * mv2.KVector3.Scalar246 + mv1.KVector2.Scalar14 * mv2.KVector3.Scalar136 + mv1.KVector2.Scalar24 * mv2.KVector3.Scalar236 - mv1.KVector2.Scalar35 * mv2.KVector3.Scalar456 + mv1.KVector2.Scalar45 * mv2.KVector3.Scalar356 - mv1.KVector2.Scalar16 * mv2.KVector3.Scalar134 - mv1.KVector2.Scalar26 * mv2.KVector3.Scalar234 - mv1.KVector2.Scalar56 * mv2.KVector3.Scalar345;
                tempScalar[49] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar256 + mv1.KVector2.Scalar13 * mv2.KVector3.Scalar356 + mv1.KVector2.Scalar14 * mv2.KVector3.Scalar456 - mv1.KVector2.Scalar25 * mv2.KVector3.Scalar126 - mv1.KVector2.Scalar35 * mv2.KVector3.Scalar136 - mv1.KVector2.Scalar45 * mv2.KVector3.Scalar146 + mv1.KVector2.Scalar26 * mv2.KVector3.Scalar125 + mv1.KVector2.Scalar36 * mv2.KVector3.Scalar135 + mv1.KVector2.Scalar46 * mv2.KVector3.Scalar145;
                tempScalar[50] += -mv1.KVector2.Scalar12 * mv2.KVector3.Scalar156 + mv1.KVector2.Scalar23 * mv2.KVector3.Scalar356 + mv1.KVector2.Scalar24 * mv2.KVector3.Scalar456 + mv1.KVector2.Scalar15 * mv2.KVector3.Scalar126 - mv1.KVector2.Scalar35 * mv2.KVector3.Scalar236 - mv1.KVector2.Scalar45 * mv2.KVector3.Scalar246 - mv1.KVector2.Scalar16 * mv2.KVector3.Scalar125 + mv1.KVector2.Scalar36 * mv2.KVector3.Scalar235 + mv1.KVector2.Scalar46 * mv2.KVector3.Scalar245;
                tempScalar[52] += -mv1.KVector2.Scalar13 * mv2.KVector3.Scalar156 - mv1.KVector2.Scalar23 * mv2.KVector3.Scalar256 + mv1.KVector2.Scalar34 * mv2.KVector3.Scalar456 + mv1.KVector2.Scalar15 * mv2.KVector3.Scalar136 + mv1.KVector2.Scalar25 * mv2.KVector3.Scalar236 - mv1.KVector2.Scalar45 * mv2.KVector3.Scalar346 - mv1.KVector2.Scalar16 * mv2.KVector3.Scalar135 - mv1.KVector2.Scalar26 * mv2.KVector3.Scalar235 + mv1.KVector2.Scalar46 * mv2.KVector3.Scalar345;
                tempScalar[56] += -mv1.KVector2.Scalar14 * mv2.KVector3.Scalar156 - mv1.KVector2.Scalar24 * mv2.KVector3.Scalar256 - mv1.KVector2.Scalar34 * mv2.KVector3.Scalar356 + mv1.KVector2.Scalar15 * mv2.KVector3.Scalar146 + mv1.KVector2.Scalar25 * mv2.KVector3.Scalar246 + mv1.KVector2.Scalar35 * mv2.KVector3.Scalar346 - mv1.KVector2.Scalar16 * mv2.KVector3.Scalar145 - mv1.KVector2.Scalar26 * mv2.KVector3.Scalar245 - mv1.KVector2.Scalar36 * mv2.KVector3.Scalar345;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[15] += -mv1.KVector2.Scalar15 * mv2.KVector4.Scalar2345 + mv1.KVector2.Scalar25 * mv2.KVector4.Scalar1345 - mv1.KVector2.Scalar35 * mv2.KVector4.Scalar1245 + mv1.KVector2.Scalar45 * mv2.KVector4.Scalar1235 - mv1.KVector2.Scalar16 * mv2.KVector4.Scalar2346 + mv1.KVector2.Scalar26 * mv2.KVector4.Scalar1346 - mv1.KVector2.Scalar36 * mv2.KVector4.Scalar1246 + mv1.KVector2.Scalar46 * mv2.KVector4.Scalar1236;
                tempScalar[23] += mv1.KVector2.Scalar14 * mv2.KVector4.Scalar2345 - mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1345 + mv1.KVector2.Scalar34 * mv2.KVector4.Scalar1245 - mv1.KVector2.Scalar45 * mv2.KVector4.Scalar1234 - mv1.KVector2.Scalar16 * mv2.KVector4.Scalar2356 + mv1.KVector2.Scalar26 * mv2.KVector4.Scalar1356 - mv1.KVector2.Scalar36 * mv2.KVector4.Scalar1256 + mv1.KVector2.Scalar56 * mv2.KVector4.Scalar1236;
                tempScalar[27] += -mv1.KVector2.Scalar13 * mv2.KVector4.Scalar2345 + mv1.KVector2.Scalar23 * mv2.KVector4.Scalar1345 - mv1.KVector2.Scalar34 * mv2.KVector4.Scalar1235 + mv1.KVector2.Scalar35 * mv2.KVector4.Scalar1234 - mv1.KVector2.Scalar16 * mv2.KVector4.Scalar2456 + mv1.KVector2.Scalar26 * mv2.KVector4.Scalar1456 - mv1.KVector2.Scalar46 * mv2.KVector4.Scalar1256 + mv1.KVector2.Scalar56 * mv2.KVector4.Scalar1246;
                tempScalar[29] += mv1.KVector2.Scalar12 * mv2.KVector4.Scalar2345 - mv1.KVector2.Scalar23 * mv2.KVector4.Scalar1245 + mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1235 - mv1.KVector2.Scalar25 * mv2.KVector4.Scalar1234 - mv1.KVector2.Scalar16 * mv2.KVector4.Scalar3456 + mv1.KVector2.Scalar36 * mv2.KVector4.Scalar1456 - mv1.KVector2.Scalar46 * mv2.KVector4.Scalar1356 + mv1.KVector2.Scalar56 * mv2.KVector4.Scalar1346;
                tempScalar[30] += -mv1.KVector2.Scalar12 * mv2.KVector4.Scalar1345 + mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1245 - mv1.KVector2.Scalar14 * mv2.KVector4.Scalar1235 + mv1.KVector2.Scalar15 * mv2.KVector4.Scalar1234 - mv1.KVector2.Scalar26 * mv2.KVector4.Scalar3456 + mv1.KVector2.Scalar36 * mv2.KVector4.Scalar2456 - mv1.KVector2.Scalar46 * mv2.KVector4.Scalar2356 + mv1.KVector2.Scalar56 * mv2.KVector4.Scalar2346;
                tempScalar[39] += mv1.KVector2.Scalar14 * mv2.KVector4.Scalar2346 - mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1346 + mv1.KVector2.Scalar34 * mv2.KVector4.Scalar1246 + mv1.KVector2.Scalar15 * mv2.KVector4.Scalar2356 - mv1.KVector2.Scalar25 * mv2.KVector4.Scalar1356 + mv1.KVector2.Scalar35 * mv2.KVector4.Scalar1256 - mv1.KVector2.Scalar46 * mv2.KVector4.Scalar1234 - mv1.KVector2.Scalar56 * mv2.KVector4.Scalar1235;
                tempScalar[43] += -mv1.KVector2.Scalar13 * mv2.KVector4.Scalar2346 + mv1.KVector2.Scalar23 * mv2.KVector4.Scalar1346 - mv1.KVector2.Scalar34 * mv2.KVector4.Scalar1236 + mv1.KVector2.Scalar15 * mv2.KVector4.Scalar2456 - mv1.KVector2.Scalar25 * mv2.KVector4.Scalar1456 + mv1.KVector2.Scalar45 * mv2.KVector4.Scalar1256 + mv1.KVector2.Scalar36 * mv2.KVector4.Scalar1234 - mv1.KVector2.Scalar56 * mv2.KVector4.Scalar1245;
                tempScalar[45] += mv1.KVector2.Scalar12 * mv2.KVector4.Scalar2346 - mv1.KVector2.Scalar23 * mv2.KVector4.Scalar1246 + mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1236 + mv1.KVector2.Scalar15 * mv2.KVector4.Scalar3456 - mv1.KVector2.Scalar35 * mv2.KVector4.Scalar1456 + mv1.KVector2.Scalar45 * mv2.KVector4.Scalar1356 - mv1.KVector2.Scalar26 * mv2.KVector4.Scalar1234 - mv1.KVector2.Scalar56 * mv2.KVector4.Scalar1345;
                tempScalar[46] += -mv1.KVector2.Scalar12 * mv2.KVector4.Scalar1346 + mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1246 - mv1.KVector2.Scalar14 * mv2.KVector4.Scalar1236 + mv1.KVector2.Scalar25 * mv2.KVector4.Scalar3456 - mv1.KVector2.Scalar35 * mv2.KVector4.Scalar2456 + mv1.KVector2.Scalar45 * mv2.KVector4.Scalar2356 + mv1.KVector2.Scalar16 * mv2.KVector4.Scalar1234 - mv1.KVector2.Scalar56 * mv2.KVector4.Scalar2345;
                tempScalar[51] += -mv1.KVector2.Scalar13 * mv2.KVector4.Scalar2356 + mv1.KVector2.Scalar23 * mv2.KVector4.Scalar1356 - mv1.KVector2.Scalar14 * mv2.KVector4.Scalar2456 + mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1456 - mv1.KVector2.Scalar35 * mv2.KVector4.Scalar1236 - mv1.KVector2.Scalar45 * mv2.KVector4.Scalar1246 + mv1.KVector2.Scalar36 * mv2.KVector4.Scalar1235 + mv1.KVector2.Scalar46 * mv2.KVector4.Scalar1245;
                tempScalar[53] += mv1.KVector2.Scalar12 * mv2.KVector4.Scalar2356 - mv1.KVector2.Scalar23 * mv2.KVector4.Scalar1256 - mv1.KVector2.Scalar14 * mv2.KVector4.Scalar3456 + mv1.KVector2.Scalar34 * mv2.KVector4.Scalar1456 + mv1.KVector2.Scalar25 * mv2.KVector4.Scalar1236 - mv1.KVector2.Scalar45 * mv2.KVector4.Scalar1346 - mv1.KVector2.Scalar26 * mv2.KVector4.Scalar1235 + mv1.KVector2.Scalar46 * mv2.KVector4.Scalar1345;
                tempScalar[54] += -mv1.KVector2.Scalar12 * mv2.KVector4.Scalar1356 + mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1256 - mv1.KVector2.Scalar24 * mv2.KVector4.Scalar3456 + mv1.KVector2.Scalar34 * mv2.KVector4.Scalar2456 - mv1.KVector2.Scalar15 * mv2.KVector4.Scalar1236 - mv1.KVector2.Scalar45 * mv2.KVector4.Scalar2346 + mv1.KVector2.Scalar16 * mv2.KVector4.Scalar1235 + mv1.KVector2.Scalar46 * mv2.KVector4.Scalar2345;
                tempScalar[57] += mv1.KVector2.Scalar12 * mv2.KVector4.Scalar2456 + mv1.KVector2.Scalar13 * mv2.KVector4.Scalar3456 - mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1256 - mv1.KVector2.Scalar34 * mv2.KVector4.Scalar1356 + mv1.KVector2.Scalar25 * mv2.KVector4.Scalar1246 + mv1.KVector2.Scalar35 * mv2.KVector4.Scalar1346 - mv1.KVector2.Scalar26 * mv2.KVector4.Scalar1245 - mv1.KVector2.Scalar36 * mv2.KVector4.Scalar1345;
                tempScalar[58] += -mv1.KVector2.Scalar12 * mv2.KVector4.Scalar1456 + mv1.KVector2.Scalar23 * mv2.KVector4.Scalar3456 + mv1.KVector2.Scalar14 * mv2.KVector4.Scalar1256 - mv1.KVector2.Scalar34 * mv2.KVector4.Scalar2356 - mv1.KVector2.Scalar15 * mv2.KVector4.Scalar1246 + mv1.KVector2.Scalar35 * mv2.KVector4.Scalar2346 + mv1.KVector2.Scalar16 * mv2.KVector4.Scalar1245 - mv1.KVector2.Scalar36 * mv2.KVector4.Scalar2345;
                tempScalar[60] += -mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1456 - mv1.KVector2.Scalar23 * mv2.KVector4.Scalar2456 + mv1.KVector2.Scalar14 * mv2.KVector4.Scalar1356 + mv1.KVector2.Scalar24 * mv2.KVector4.Scalar2356 - mv1.KVector2.Scalar15 * mv2.KVector4.Scalar1346 - mv1.KVector2.Scalar25 * mv2.KVector4.Scalar2346 + mv1.KVector2.Scalar16 * mv2.KVector4.Scalar1345 + mv1.KVector2.Scalar26 * mv2.KVector4.Scalar2345;
            }
            
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[31] += mv1.KVector2.Scalar16 * mv2.KVector5.Scalar23456 - mv1.KVector2.Scalar26 * mv2.KVector5.Scalar13456 + mv1.KVector2.Scalar36 * mv2.KVector5.Scalar12456 - mv1.KVector2.Scalar46 * mv2.KVector5.Scalar12356 + mv1.KVector2.Scalar56 * mv2.KVector5.Scalar12346;
                tempScalar[47] += -mv1.KVector2.Scalar15 * mv2.KVector5.Scalar23456 + mv1.KVector2.Scalar25 * mv2.KVector5.Scalar13456 - mv1.KVector2.Scalar35 * mv2.KVector5.Scalar12456 + mv1.KVector2.Scalar45 * mv2.KVector5.Scalar12356 - mv1.KVector2.Scalar56 * mv2.KVector5.Scalar12345;
                tempScalar[55] += mv1.KVector2.Scalar14 * mv2.KVector5.Scalar23456 - mv1.KVector2.Scalar24 * mv2.KVector5.Scalar13456 + mv1.KVector2.Scalar34 * mv2.KVector5.Scalar12456 - mv1.KVector2.Scalar45 * mv2.KVector5.Scalar12346 + mv1.KVector2.Scalar46 * mv2.KVector5.Scalar12345;
                tempScalar[59] += -mv1.KVector2.Scalar13 * mv2.KVector5.Scalar23456 + mv1.KVector2.Scalar23 * mv2.KVector5.Scalar13456 - mv1.KVector2.Scalar34 * mv2.KVector5.Scalar12356 + mv1.KVector2.Scalar35 * mv2.KVector5.Scalar12346 - mv1.KVector2.Scalar36 * mv2.KVector5.Scalar12345;
                tempScalar[61] += mv1.KVector2.Scalar12 * mv2.KVector5.Scalar23456 - mv1.KVector2.Scalar23 * mv2.KVector5.Scalar12456 + mv1.KVector2.Scalar24 * mv2.KVector5.Scalar12356 - mv1.KVector2.Scalar25 * mv2.KVector5.Scalar12346 + mv1.KVector2.Scalar26 * mv2.KVector5.Scalar12345;
                tempScalar[62] += -mv1.KVector2.Scalar12 * mv2.KVector5.Scalar13456 + mv1.KVector2.Scalar13 * mv2.KVector5.Scalar12456 - mv1.KVector2.Scalar14 * mv2.KVector5.Scalar12356 + mv1.KVector2.Scalar15 * mv2.KVector5.Scalar12346 - mv1.KVector2.Scalar16 * mv2.KVector5.Scalar12345;
            }
            
        }
        
        if (!mv1.KVector3.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[15] += mv1.KVector3.Scalar123 * mv2.KVector1.Scalar4 - mv1.KVector3.Scalar124 * mv2.KVector1.Scalar3 + mv1.KVector3.Scalar134 * mv2.KVector1.Scalar2 - mv1.KVector3.Scalar234 * mv2.KVector1.Scalar1;
                tempScalar[23] += mv1.KVector3.Scalar123 * mv2.KVector1.Scalar5 - mv1.KVector3.Scalar125 * mv2.KVector1.Scalar3 + mv1.KVector3.Scalar135 * mv2.KVector1.Scalar2 - mv1.KVector3.Scalar235 * mv2.KVector1.Scalar1;
                tempScalar[27] += mv1.KVector3.Scalar124 * mv2.KVector1.Scalar5 - mv1.KVector3.Scalar125 * mv2.KVector1.Scalar4 + mv1.KVector3.Scalar145 * mv2.KVector1.Scalar2 - mv1.KVector3.Scalar245 * mv2.KVector1.Scalar1;
                tempScalar[29] += mv1.KVector3.Scalar134 * mv2.KVector1.Scalar5 - mv1.KVector3.Scalar135 * mv2.KVector1.Scalar4 + mv1.KVector3.Scalar145 * mv2.KVector1.Scalar3 - mv1.KVector3.Scalar345 * mv2.KVector1.Scalar1;
                tempScalar[30] += mv1.KVector3.Scalar234 * mv2.KVector1.Scalar5 - mv1.KVector3.Scalar235 * mv2.KVector1.Scalar4 + mv1.KVector3.Scalar245 * mv2.KVector1.Scalar3 - mv1.KVector3.Scalar345 * mv2.KVector1.Scalar2;
                tempScalar[39] += mv1.KVector3.Scalar123 * mv2.KVector1.Scalar6 - mv1.KVector3.Scalar126 * mv2.KVector1.Scalar3 + mv1.KVector3.Scalar136 * mv2.KVector1.Scalar2 - mv1.KVector3.Scalar236 * mv2.KVector1.Scalar1;
                tempScalar[43] += mv1.KVector3.Scalar124 * mv2.KVector1.Scalar6 - mv1.KVector3.Scalar126 * mv2.KVector1.Scalar4 + mv1.KVector3.Scalar146 * mv2.KVector1.Scalar2 - mv1.KVector3.Scalar246 * mv2.KVector1.Scalar1;
                tempScalar[45] += mv1.KVector3.Scalar134 * mv2.KVector1.Scalar6 - mv1.KVector3.Scalar136 * mv2.KVector1.Scalar4 + mv1.KVector3.Scalar146 * mv2.KVector1.Scalar3 - mv1.KVector3.Scalar346 * mv2.KVector1.Scalar1;
                tempScalar[46] += mv1.KVector3.Scalar234 * mv2.KVector1.Scalar6 - mv1.KVector3.Scalar236 * mv2.KVector1.Scalar4 + mv1.KVector3.Scalar246 * mv2.KVector1.Scalar3 - mv1.KVector3.Scalar346 * mv2.KVector1.Scalar2;
                tempScalar[51] += mv1.KVector3.Scalar125 * mv2.KVector1.Scalar6 - mv1.KVector3.Scalar126 * mv2.KVector1.Scalar5 + mv1.KVector3.Scalar156 * mv2.KVector1.Scalar2 - mv1.KVector3.Scalar256 * mv2.KVector1.Scalar1;
                tempScalar[53] += mv1.KVector3.Scalar135 * mv2.KVector1.Scalar6 - mv1.KVector3.Scalar136 * mv2.KVector1.Scalar5 + mv1.KVector3.Scalar156 * mv2.KVector1.Scalar3 - mv1.KVector3.Scalar356 * mv2.KVector1.Scalar1;
                tempScalar[54] += mv1.KVector3.Scalar235 * mv2.KVector1.Scalar6 - mv1.KVector3.Scalar236 * mv2.KVector1.Scalar5 + mv1.KVector3.Scalar256 * mv2.KVector1.Scalar3 - mv1.KVector3.Scalar356 * mv2.KVector1.Scalar2;
                tempScalar[57] += mv1.KVector3.Scalar145 * mv2.KVector1.Scalar6 - mv1.KVector3.Scalar146 * mv2.KVector1.Scalar5 + mv1.KVector3.Scalar156 * mv2.KVector1.Scalar4 - mv1.KVector3.Scalar456 * mv2.KVector1.Scalar1;
                tempScalar[58] += mv1.KVector3.Scalar245 * mv2.KVector1.Scalar6 - mv1.KVector3.Scalar246 * mv2.KVector1.Scalar5 + mv1.KVector3.Scalar256 * mv2.KVector1.Scalar4 - mv1.KVector3.Scalar456 * mv2.KVector1.Scalar2;
                tempScalar[60] += mv1.KVector3.Scalar345 * mv2.KVector1.Scalar6 - mv1.KVector3.Scalar346 * mv2.KVector1.Scalar5 + mv1.KVector3.Scalar356 * mv2.KVector1.Scalar4 - mv1.KVector3.Scalar456 * mv2.KVector1.Scalar3;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[7] += -mv1.KVector3.Scalar124 * mv2.KVector2.Scalar34 + mv1.KVector3.Scalar134 * mv2.KVector2.Scalar24 - mv1.KVector3.Scalar234 * mv2.KVector2.Scalar14 - mv1.KVector3.Scalar125 * mv2.KVector2.Scalar35 + mv1.KVector3.Scalar135 * mv2.KVector2.Scalar25 - mv1.KVector3.Scalar235 * mv2.KVector2.Scalar15 - mv1.KVector3.Scalar126 * mv2.KVector2.Scalar36 + mv1.KVector3.Scalar136 * mv2.KVector2.Scalar26 - mv1.KVector3.Scalar236 * mv2.KVector2.Scalar16;
                tempScalar[11] += mv1.KVector3.Scalar123 * mv2.KVector2.Scalar34 - mv1.KVector3.Scalar134 * mv2.KVector2.Scalar23 + mv1.KVector3.Scalar234 * mv2.KVector2.Scalar13 - mv1.KVector3.Scalar125 * mv2.KVector2.Scalar45 + mv1.KVector3.Scalar145 * mv2.KVector2.Scalar25 - mv1.KVector3.Scalar245 * mv2.KVector2.Scalar15 - mv1.KVector3.Scalar126 * mv2.KVector2.Scalar46 + mv1.KVector3.Scalar146 * mv2.KVector2.Scalar26 - mv1.KVector3.Scalar246 * mv2.KVector2.Scalar16;
                tempScalar[13] += -mv1.KVector3.Scalar123 * mv2.KVector2.Scalar24 + mv1.KVector3.Scalar124 * mv2.KVector2.Scalar23 - mv1.KVector3.Scalar234 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar135 * mv2.KVector2.Scalar45 + mv1.KVector3.Scalar145 * mv2.KVector2.Scalar35 - mv1.KVector3.Scalar345 * mv2.KVector2.Scalar15 - mv1.KVector3.Scalar136 * mv2.KVector2.Scalar46 + mv1.KVector3.Scalar146 * mv2.KVector2.Scalar36 - mv1.KVector3.Scalar346 * mv2.KVector2.Scalar16;
                tempScalar[14] += mv1.KVector3.Scalar123 * mv2.KVector2.Scalar14 - mv1.KVector3.Scalar124 * mv2.KVector2.Scalar13 + mv1.KVector3.Scalar134 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar235 * mv2.KVector2.Scalar45 + mv1.KVector3.Scalar245 * mv2.KVector2.Scalar35 - mv1.KVector3.Scalar345 * mv2.KVector2.Scalar25 - mv1.KVector3.Scalar236 * mv2.KVector2.Scalar46 + mv1.KVector3.Scalar246 * mv2.KVector2.Scalar36 - mv1.KVector3.Scalar346 * mv2.KVector2.Scalar26;
                tempScalar[19] += mv1.KVector3.Scalar123 * mv2.KVector2.Scalar35 + mv1.KVector3.Scalar124 * mv2.KVector2.Scalar45 - mv1.KVector3.Scalar135 * mv2.KVector2.Scalar23 + mv1.KVector3.Scalar235 * mv2.KVector2.Scalar13 - mv1.KVector3.Scalar145 * mv2.KVector2.Scalar24 + mv1.KVector3.Scalar245 * mv2.KVector2.Scalar14 - mv1.KVector3.Scalar126 * mv2.KVector2.Scalar56 + mv1.KVector3.Scalar156 * mv2.KVector2.Scalar26 - mv1.KVector3.Scalar256 * mv2.KVector2.Scalar16;
                tempScalar[21] += -mv1.KVector3.Scalar123 * mv2.KVector2.Scalar25 + mv1.KVector3.Scalar134 * mv2.KVector2.Scalar45 + mv1.KVector3.Scalar125 * mv2.KVector2.Scalar23 - mv1.KVector3.Scalar235 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar145 * mv2.KVector2.Scalar34 + mv1.KVector3.Scalar345 * mv2.KVector2.Scalar14 - mv1.KVector3.Scalar136 * mv2.KVector2.Scalar56 + mv1.KVector3.Scalar156 * mv2.KVector2.Scalar36 - mv1.KVector3.Scalar356 * mv2.KVector2.Scalar16;
                tempScalar[22] += mv1.KVector3.Scalar123 * mv2.KVector2.Scalar15 + mv1.KVector3.Scalar234 * mv2.KVector2.Scalar45 - mv1.KVector3.Scalar125 * mv2.KVector2.Scalar13 + mv1.KVector3.Scalar135 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar245 * mv2.KVector2.Scalar34 + mv1.KVector3.Scalar345 * mv2.KVector2.Scalar24 - mv1.KVector3.Scalar236 * mv2.KVector2.Scalar56 + mv1.KVector3.Scalar256 * mv2.KVector2.Scalar36 - mv1.KVector3.Scalar356 * mv2.KVector2.Scalar26;
                tempScalar[25] += -mv1.KVector3.Scalar124 * mv2.KVector2.Scalar25 - mv1.KVector3.Scalar134 * mv2.KVector2.Scalar35 + mv1.KVector3.Scalar125 * mv2.KVector2.Scalar24 + mv1.KVector3.Scalar135 * mv2.KVector2.Scalar34 - mv1.KVector3.Scalar245 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar345 * mv2.KVector2.Scalar13 - mv1.KVector3.Scalar146 * mv2.KVector2.Scalar56 + mv1.KVector3.Scalar156 * mv2.KVector2.Scalar46 - mv1.KVector3.Scalar456 * mv2.KVector2.Scalar16;
                tempScalar[26] += mv1.KVector3.Scalar124 * mv2.KVector2.Scalar15 - mv1.KVector3.Scalar234 * mv2.KVector2.Scalar35 - mv1.KVector3.Scalar125 * mv2.KVector2.Scalar14 + mv1.KVector3.Scalar235 * mv2.KVector2.Scalar34 + mv1.KVector3.Scalar145 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar345 * mv2.KVector2.Scalar23 - mv1.KVector3.Scalar246 * mv2.KVector2.Scalar56 + mv1.KVector3.Scalar256 * mv2.KVector2.Scalar46 - mv1.KVector3.Scalar456 * mv2.KVector2.Scalar26;
                tempScalar[28] += mv1.KVector3.Scalar134 * mv2.KVector2.Scalar15 + mv1.KVector3.Scalar234 * mv2.KVector2.Scalar25 - mv1.KVector3.Scalar135 * mv2.KVector2.Scalar14 - mv1.KVector3.Scalar235 * mv2.KVector2.Scalar24 + mv1.KVector3.Scalar145 * mv2.KVector2.Scalar13 + mv1.KVector3.Scalar245 * mv2.KVector2.Scalar23 - mv1.KVector3.Scalar346 * mv2.KVector2.Scalar56 + mv1.KVector3.Scalar356 * mv2.KVector2.Scalar46 - mv1.KVector3.Scalar456 * mv2.KVector2.Scalar36;
                tempScalar[35] += mv1.KVector3.Scalar123 * mv2.KVector2.Scalar36 + mv1.KVector3.Scalar124 * mv2.KVector2.Scalar46 + mv1.KVector3.Scalar125 * mv2.KVector2.Scalar56 - mv1.KVector3.Scalar136 * mv2.KVector2.Scalar23 + mv1.KVector3.Scalar236 * mv2.KVector2.Scalar13 - mv1.KVector3.Scalar146 * mv2.KVector2.Scalar24 + mv1.KVector3.Scalar246 * mv2.KVector2.Scalar14 - mv1.KVector3.Scalar156 * mv2.KVector2.Scalar25 + mv1.KVector3.Scalar256 * mv2.KVector2.Scalar15;
                tempScalar[37] += -mv1.KVector3.Scalar123 * mv2.KVector2.Scalar26 + mv1.KVector3.Scalar134 * mv2.KVector2.Scalar46 + mv1.KVector3.Scalar135 * mv2.KVector2.Scalar56 + mv1.KVector3.Scalar126 * mv2.KVector2.Scalar23 - mv1.KVector3.Scalar236 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar146 * mv2.KVector2.Scalar34 + mv1.KVector3.Scalar346 * mv2.KVector2.Scalar14 - mv1.KVector3.Scalar156 * mv2.KVector2.Scalar35 + mv1.KVector3.Scalar356 * mv2.KVector2.Scalar15;
                tempScalar[38] += mv1.KVector3.Scalar123 * mv2.KVector2.Scalar16 + mv1.KVector3.Scalar234 * mv2.KVector2.Scalar46 + mv1.KVector3.Scalar235 * mv2.KVector2.Scalar56 - mv1.KVector3.Scalar126 * mv2.KVector2.Scalar13 + mv1.KVector3.Scalar136 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar246 * mv2.KVector2.Scalar34 + mv1.KVector3.Scalar346 * mv2.KVector2.Scalar24 - mv1.KVector3.Scalar256 * mv2.KVector2.Scalar35 + mv1.KVector3.Scalar356 * mv2.KVector2.Scalar25;
                tempScalar[41] += -mv1.KVector3.Scalar124 * mv2.KVector2.Scalar26 - mv1.KVector3.Scalar134 * mv2.KVector2.Scalar36 + mv1.KVector3.Scalar145 * mv2.KVector2.Scalar56 + mv1.KVector3.Scalar126 * mv2.KVector2.Scalar24 + mv1.KVector3.Scalar136 * mv2.KVector2.Scalar34 - mv1.KVector3.Scalar246 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar346 * mv2.KVector2.Scalar13 - mv1.KVector3.Scalar156 * mv2.KVector2.Scalar45 + mv1.KVector3.Scalar456 * mv2.KVector2.Scalar15;
                tempScalar[42] += mv1.KVector3.Scalar124 * mv2.KVector2.Scalar16 - mv1.KVector3.Scalar234 * mv2.KVector2.Scalar36 + mv1.KVector3.Scalar245 * mv2.KVector2.Scalar56 - mv1.KVector3.Scalar126 * mv2.KVector2.Scalar14 + mv1.KVector3.Scalar236 * mv2.KVector2.Scalar34 + mv1.KVector3.Scalar146 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar346 * mv2.KVector2.Scalar23 - mv1.KVector3.Scalar256 * mv2.KVector2.Scalar45 + mv1.KVector3.Scalar456 * mv2.KVector2.Scalar25;
                tempScalar[44] += mv1.KVector3.Scalar134 * mv2.KVector2.Scalar16 + mv1.KVector3.Scalar234 * mv2.KVector2.Scalar26 + mv1.KVector3.Scalar345 * mv2.KVector2.Scalar56 - mv1.KVector3.Scalar136 * mv2.KVector2.Scalar14 - mv1.KVector3.Scalar236 * mv2.KVector2.Scalar24 + mv1.KVector3.Scalar146 * mv2.KVector2.Scalar13 + mv1.KVector3.Scalar246 * mv2.KVector2.Scalar23 - mv1.KVector3.Scalar356 * mv2.KVector2.Scalar45 + mv1.KVector3.Scalar456 * mv2.KVector2.Scalar35;
                tempScalar[49] += -mv1.KVector3.Scalar125 * mv2.KVector2.Scalar26 - mv1.KVector3.Scalar135 * mv2.KVector2.Scalar36 - mv1.KVector3.Scalar145 * mv2.KVector2.Scalar46 + mv1.KVector3.Scalar126 * mv2.KVector2.Scalar25 + mv1.KVector3.Scalar136 * mv2.KVector2.Scalar35 + mv1.KVector3.Scalar146 * mv2.KVector2.Scalar45 - mv1.KVector3.Scalar256 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar356 * mv2.KVector2.Scalar13 - mv1.KVector3.Scalar456 * mv2.KVector2.Scalar14;
                tempScalar[50] += mv1.KVector3.Scalar125 * mv2.KVector2.Scalar16 - mv1.KVector3.Scalar235 * mv2.KVector2.Scalar36 - mv1.KVector3.Scalar245 * mv2.KVector2.Scalar46 - mv1.KVector3.Scalar126 * mv2.KVector2.Scalar15 + mv1.KVector3.Scalar236 * mv2.KVector2.Scalar35 + mv1.KVector3.Scalar246 * mv2.KVector2.Scalar45 + mv1.KVector3.Scalar156 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar356 * mv2.KVector2.Scalar23 - mv1.KVector3.Scalar456 * mv2.KVector2.Scalar24;
                tempScalar[52] += mv1.KVector3.Scalar135 * mv2.KVector2.Scalar16 + mv1.KVector3.Scalar235 * mv2.KVector2.Scalar26 - mv1.KVector3.Scalar345 * mv2.KVector2.Scalar46 - mv1.KVector3.Scalar136 * mv2.KVector2.Scalar15 - mv1.KVector3.Scalar236 * mv2.KVector2.Scalar25 + mv1.KVector3.Scalar346 * mv2.KVector2.Scalar45 + mv1.KVector3.Scalar156 * mv2.KVector2.Scalar13 + mv1.KVector3.Scalar256 * mv2.KVector2.Scalar23 - mv1.KVector3.Scalar456 * mv2.KVector2.Scalar34;
                tempScalar[56] += mv1.KVector3.Scalar145 * mv2.KVector2.Scalar16 + mv1.KVector3.Scalar245 * mv2.KVector2.Scalar26 + mv1.KVector3.Scalar345 * mv2.KVector2.Scalar36 - mv1.KVector3.Scalar146 * mv2.KVector2.Scalar15 - mv1.KVector3.Scalar246 * mv2.KVector2.Scalar25 - mv1.KVector3.Scalar346 * mv2.KVector2.Scalar35 + mv1.KVector3.Scalar156 * mv2.KVector2.Scalar14 + mv1.KVector3.Scalar256 * mv2.KVector2.Scalar24 + mv1.KVector3.Scalar356 * mv2.KVector2.Scalar34;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[3] += -mv1.KVector3.Scalar134 * mv2.KVector3.Scalar234 + mv1.KVector3.Scalar234 * mv2.KVector3.Scalar134 - mv1.KVector3.Scalar135 * mv2.KVector3.Scalar235 + mv1.KVector3.Scalar235 * mv2.KVector3.Scalar135 - mv1.KVector3.Scalar145 * mv2.KVector3.Scalar245 + mv1.KVector3.Scalar245 * mv2.KVector3.Scalar145 - mv1.KVector3.Scalar136 * mv2.KVector3.Scalar236 + mv1.KVector3.Scalar236 * mv2.KVector3.Scalar136 - mv1.KVector3.Scalar146 * mv2.KVector3.Scalar246 + mv1.KVector3.Scalar246 * mv2.KVector3.Scalar146 - mv1.KVector3.Scalar156 * mv2.KVector3.Scalar256 + mv1.KVector3.Scalar256 * mv2.KVector3.Scalar156;
                tempScalar[5] += mv1.KVector3.Scalar124 * mv2.KVector3.Scalar234 - mv1.KVector3.Scalar234 * mv2.KVector3.Scalar124 + mv1.KVector3.Scalar125 * mv2.KVector3.Scalar235 - mv1.KVector3.Scalar235 * mv2.KVector3.Scalar125 - mv1.KVector3.Scalar145 * mv2.KVector3.Scalar345 + mv1.KVector3.Scalar345 * mv2.KVector3.Scalar145 + mv1.KVector3.Scalar126 * mv2.KVector3.Scalar236 - mv1.KVector3.Scalar236 * mv2.KVector3.Scalar126 - mv1.KVector3.Scalar146 * mv2.KVector3.Scalar346 + mv1.KVector3.Scalar346 * mv2.KVector3.Scalar146 - mv1.KVector3.Scalar156 * mv2.KVector3.Scalar356 + mv1.KVector3.Scalar356 * mv2.KVector3.Scalar156;
                tempScalar[6] += -mv1.KVector3.Scalar124 * mv2.KVector3.Scalar134 + mv1.KVector3.Scalar134 * mv2.KVector3.Scalar124 - mv1.KVector3.Scalar125 * mv2.KVector3.Scalar135 + mv1.KVector3.Scalar135 * mv2.KVector3.Scalar125 - mv1.KVector3.Scalar245 * mv2.KVector3.Scalar345 + mv1.KVector3.Scalar345 * mv2.KVector3.Scalar245 - mv1.KVector3.Scalar126 * mv2.KVector3.Scalar136 + mv1.KVector3.Scalar136 * mv2.KVector3.Scalar126 - mv1.KVector3.Scalar246 * mv2.KVector3.Scalar346 + mv1.KVector3.Scalar346 * mv2.KVector3.Scalar246 - mv1.KVector3.Scalar256 * mv2.KVector3.Scalar356 + mv1.KVector3.Scalar356 * mv2.KVector3.Scalar256;
                tempScalar[9] += -mv1.KVector3.Scalar123 * mv2.KVector3.Scalar234 + mv1.KVector3.Scalar234 * mv2.KVector3.Scalar123 + mv1.KVector3.Scalar125 * mv2.KVector3.Scalar245 + mv1.KVector3.Scalar135 * mv2.KVector3.Scalar345 - mv1.KVector3.Scalar245 * mv2.KVector3.Scalar125 - mv1.KVector3.Scalar345 * mv2.KVector3.Scalar135 + mv1.KVector3.Scalar126 * mv2.KVector3.Scalar246 + mv1.KVector3.Scalar136 * mv2.KVector3.Scalar346 - mv1.KVector3.Scalar246 * mv2.KVector3.Scalar126 - mv1.KVector3.Scalar346 * mv2.KVector3.Scalar136 - mv1.KVector3.Scalar156 * mv2.KVector3.Scalar456 + mv1.KVector3.Scalar456 * mv2.KVector3.Scalar156;
                tempScalar[10] += mv1.KVector3.Scalar123 * mv2.KVector3.Scalar134 - mv1.KVector3.Scalar134 * mv2.KVector3.Scalar123 - mv1.KVector3.Scalar125 * mv2.KVector3.Scalar145 + mv1.KVector3.Scalar235 * mv2.KVector3.Scalar345 + mv1.KVector3.Scalar145 * mv2.KVector3.Scalar125 - mv1.KVector3.Scalar345 * mv2.KVector3.Scalar235 - mv1.KVector3.Scalar126 * mv2.KVector3.Scalar146 + mv1.KVector3.Scalar236 * mv2.KVector3.Scalar346 + mv1.KVector3.Scalar146 * mv2.KVector3.Scalar126 - mv1.KVector3.Scalar346 * mv2.KVector3.Scalar236 - mv1.KVector3.Scalar256 * mv2.KVector3.Scalar456 + mv1.KVector3.Scalar456 * mv2.KVector3.Scalar256;
                tempScalar[12] += -mv1.KVector3.Scalar123 * mv2.KVector3.Scalar124 + mv1.KVector3.Scalar124 * mv2.KVector3.Scalar123 - mv1.KVector3.Scalar135 * mv2.KVector3.Scalar145 - mv1.KVector3.Scalar235 * mv2.KVector3.Scalar245 + mv1.KVector3.Scalar145 * mv2.KVector3.Scalar135 + mv1.KVector3.Scalar245 * mv2.KVector3.Scalar235 - mv1.KVector3.Scalar136 * mv2.KVector3.Scalar146 - mv1.KVector3.Scalar236 * mv2.KVector3.Scalar246 + mv1.KVector3.Scalar146 * mv2.KVector3.Scalar136 + mv1.KVector3.Scalar246 * mv2.KVector3.Scalar236 - mv1.KVector3.Scalar356 * mv2.KVector3.Scalar456 + mv1.KVector3.Scalar456 * mv2.KVector3.Scalar356;
                tempScalar[17] += -mv1.KVector3.Scalar123 * mv2.KVector3.Scalar235 - mv1.KVector3.Scalar124 * mv2.KVector3.Scalar245 - mv1.KVector3.Scalar134 * mv2.KVector3.Scalar345 + mv1.KVector3.Scalar235 * mv2.KVector3.Scalar123 + mv1.KVector3.Scalar245 * mv2.KVector3.Scalar124 + mv1.KVector3.Scalar345 * mv2.KVector3.Scalar134 + mv1.KVector3.Scalar126 * mv2.KVector3.Scalar256 + mv1.KVector3.Scalar136 * mv2.KVector3.Scalar356 + mv1.KVector3.Scalar146 * mv2.KVector3.Scalar456 - mv1.KVector3.Scalar256 * mv2.KVector3.Scalar126 - mv1.KVector3.Scalar356 * mv2.KVector3.Scalar136 - mv1.KVector3.Scalar456 * mv2.KVector3.Scalar146;
                tempScalar[18] += mv1.KVector3.Scalar123 * mv2.KVector3.Scalar135 + mv1.KVector3.Scalar124 * mv2.KVector3.Scalar145 - mv1.KVector3.Scalar234 * mv2.KVector3.Scalar345 - mv1.KVector3.Scalar135 * mv2.KVector3.Scalar123 - mv1.KVector3.Scalar145 * mv2.KVector3.Scalar124 + mv1.KVector3.Scalar345 * mv2.KVector3.Scalar234 - mv1.KVector3.Scalar126 * mv2.KVector3.Scalar156 + mv1.KVector3.Scalar236 * mv2.KVector3.Scalar356 + mv1.KVector3.Scalar246 * mv2.KVector3.Scalar456 + mv1.KVector3.Scalar156 * mv2.KVector3.Scalar126 - mv1.KVector3.Scalar356 * mv2.KVector3.Scalar236 - mv1.KVector3.Scalar456 * mv2.KVector3.Scalar246;
                tempScalar[20] += -mv1.KVector3.Scalar123 * mv2.KVector3.Scalar125 + mv1.KVector3.Scalar134 * mv2.KVector3.Scalar145 + mv1.KVector3.Scalar234 * mv2.KVector3.Scalar245 + mv1.KVector3.Scalar125 * mv2.KVector3.Scalar123 - mv1.KVector3.Scalar145 * mv2.KVector3.Scalar134 - mv1.KVector3.Scalar245 * mv2.KVector3.Scalar234 - mv1.KVector3.Scalar136 * mv2.KVector3.Scalar156 - mv1.KVector3.Scalar236 * mv2.KVector3.Scalar256 + mv1.KVector3.Scalar346 * mv2.KVector3.Scalar456 + mv1.KVector3.Scalar156 * mv2.KVector3.Scalar136 + mv1.KVector3.Scalar256 * mv2.KVector3.Scalar236 - mv1.KVector3.Scalar456 * mv2.KVector3.Scalar346;
                tempScalar[24] += -mv1.KVector3.Scalar124 * mv2.KVector3.Scalar125 - mv1.KVector3.Scalar134 * mv2.KVector3.Scalar135 - mv1.KVector3.Scalar234 * mv2.KVector3.Scalar235 + mv1.KVector3.Scalar125 * mv2.KVector3.Scalar124 + mv1.KVector3.Scalar135 * mv2.KVector3.Scalar134 + mv1.KVector3.Scalar235 * mv2.KVector3.Scalar234 - mv1.KVector3.Scalar146 * mv2.KVector3.Scalar156 - mv1.KVector3.Scalar246 * mv2.KVector3.Scalar256 - mv1.KVector3.Scalar346 * mv2.KVector3.Scalar356 + mv1.KVector3.Scalar156 * mv2.KVector3.Scalar146 + mv1.KVector3.Scalar256 * mv2.KVector3.Scalar246 + mv1.KVector3.Scalar356 * mv2.KVector3.Scalar346;
                tempScalar[33] += -mv1.KVector3.Scalar123 * mv2.KVector3.Scalar236 - mv1.KVector3.Scalar124 * mv2.KVector3.Scalar246 - mv1.KVector3.Scalar134 * mv2.KVector3.Scalar346 - mv1.KVector3.Scalar125 * mv2.KVector3.Scalar256 - mv1.KVector3.Scalar135 * mv2.KVector3.Scalar356 - mv1.KVector3.Scalar145 * mv2.KVector3.Scalar456 + mv1.KVector3.Scalar236 * mv2.KVector3.Scalar123 + mv1.KVector3.Scalar246 * mv2.KVector3.Scalar124 + mv1.KVector3.Scalar346 * mv2.KVector3.Scalar134 + mv1.KVector3.Scalar256 * mv2.KVector3.Scalar125 + mv1.KVector3.Scalar356 * mv2.KVector3.Scalar135 + mv1.KVector3.Scalar456 * mv2.KVector3.Scalar145;
                tempScalar[34] += mv1.KVector3.Scalar123 * mv2.KVector3.Scalar136 + mv1.KVector3.Scalar124 * mv2.KVector3.Scalar146 - mv1.KVector3.Scalar234 * mv2.KVector3.Scalar346 + mv1.KVector3.Scalar125 * mv2.KVector3.Scalar156 - mv1.KVector3.Scalar235 * mv2.KVector3.Scalar356 - mv1.KVector3.Scalar245 * mv2.KVector3.Scalar456 - mv1.KVector3.Scalar136 * mv2.KVector3.Scalar123 - mv1.KVector3.Scalar146 * mv2.KVector3.Scalar124 + mv1.KVector3.Scalar346 * mv2.KVector3.Scalar234 - mv1.KVector3.Scalar156 * mv2.KVector3.Scalar125 + mv1.KVector3.Scalar356 * mv2.KVector3.Scalar235 + mv1.KVector3.Scalar456 * mv2.KVector3.Scalar245;
                tempScalar[36] += -mv1.KVector3.Scalar123 * mv2.KVector3.Scalar126 + mv1.KVector3.Scalar134 * mv2.KVector3.Scalar146 + mv1.KVector3.Scalar234 * mv2.KVector3.Scalar246 + mv1.KVector3.Scalar135 * mv2.KVector3.Scalar156 + mv1.KVector3.Scalar235 * mv2.KVector3.Scalar256 - mv1.KVector3.Scalar345 * mv2.KVector3.Scalar456 + mv1.KVector3.Scalar126 * mv2.KVector3.Scalar123 - mv1.KVector3.Scalar146 * mv2.KVector3.Scalar134 - mv1.KVector3.Scalar246 * mv2.KVector3.Scalar234 - mv1.KVector3.Scalar156 * mv2.KVector3.Scalar135 - mv1.KVector3.Scalar256 * mv2.KVector3.Scalar235 + mv1.KVector3.Scalar456 * mv2.KVector3.Scalar345;
                tempScalar[40] += -mv1.KVector3.Scalar124 * mv2.KVector3.Scalar126 - mv1.KVector3.Scalar134 * mv2.KVector3.Scalar136 - mv1.KVector3.Scalar234 * mv2.KVector3.Scalar236 + mv1.KVector3.Scalar145 * mv2.KVector3.Scalar156 + mv1.KVector3.Scalar245 * mv2.KVector3.Scalar256 + mv1.KVector3.Scalar345 * mv2.KVector3.Scalar356 + mv1.KVector3.Scalar126 * mv2.KVector3.Scalar124 + mv1.KVector3.Scalar136 * mv2.KVector3.Scalar134 + mv1.KVector3.Scalar236 * mv2.KVector3.Scalar234 - mv1.KVector3.Scalar156 * mv2.KVector3.Scalar145 - mv1.KVector3.Scalar256 * mv2.KVector3.Scalar245 - mv1.KVector3.Scalar356 * mv2.KVector3.Scalar345;
                tempScalar[48] += -mv1.KVector3.Scalar125 * mv2.KVector3.Scalar126 - mv1.KVector3.Scalar135 * mv2.KVector3.Scalar136 - mv1.KVector3.Scalar235 * mv2.KVector3.Scalar236 - mv1.KVector3.Scalar145 * mv2.KVector3.Scalar146 - mv1.KVector3.Scalar245 * mv2.KVector3.Scalar246 - mv1.KVector3.Scalar345 * mv2.KVector3.Scalar346 + mv1.KVector3.Scalar126 * mv2.KVector3.Scalar125 + mv1.KVector3.Scalar136 * mv2.KVector3.Scalar135 + mv1.KVector3.Scalar236 * mv2.KVector3.Scalar235 + mv1.KVector3.Scalar146 * mv2.KVector3.Scalar145 + mv1.KVector3.Scalar246 * mv2.KVector3.Scalar245 + mv1.KVector3.Scalar346 * mv2.KVector3.Scalar345;
                tempScalar[63] += mv1.KVector3.Scalar123 * mv2.KVector3.Scalar456 - mv1.KVector3.Scalar124 * mv2.KVector3.Scalar356 + mv1.KVector3.Scalar134 * mv2.KVector3.Scalar256 - mv1.KVector3.Scalar234 * mv2.KVector3.Scalar156 + mv1.KVector3.Scalar125 * mv2.KVector3.Scalar346 - mv1.KVector3.Scalar135 * mv2.KVector3.Scalar246 + mv1.KVector3.Scalar235 * mv2.KVector3.Scalar146 + mv1.KVector3.Scalar145 * mv2.KVector3.Scalar236 - mv1.KVector3.Scalar245 * mv2.KVector3.Scalar136 + mv1.KVector3.Scalar345 * mv2.KVector3.Scalar126 - mv1.KVector3.Scalar126 * mv2.KVector3.Scalar345 + mv1.KVector3.Scalar136 * mv2.KVector3.Scalar245 - mv1.KVector3.Scalar236 * mv2.KVector3.Scalar145 - mv1.KVector3.Scalar146 * mv2.KVector3.Scalar235 + mv1.KVector3.Scalar246 * mv2.KVector3.Scalar135 - mv1.KVector3.Scalar346 * mv2.KVector3.Scalar125 + mv1.KVector3.Scalar156 * mv2.KVector3.Scalar234 - mv1.KVector3.Scalar256 * mv2.KVector3.Scalar134 + mv1.KVector3.Scalar356 * mv2.KVector3.Scalar124 - mv1.KVector3.Scalar456 * mv2.KVector3.Scalar123;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[1] += mv1.KVector3.Scalar234 * mv2.KVector4.Scalar1234 + mv1.KVector3.Scalar235 * mv2.KVector4.Scalar1235 + mv1.KVector3.Scalar245 * mv2.KVector4.Scalar1245 + mv1.KVector3.Scalar345 * mv2.KVector4.Scalar1345 + mv1.KVector3.Scalar236 * mv2.KVector4.Scalar1236 + mv1.KVector3.Scalar246 * mv2.KVector4.Scalar1246 + mv1.KVector3.Scalar346 * mv2.KVector4.Scalar1346 + mv1.KVector3.Scalar256 * mv2.KVector4.Scalar1256 + mv1.KVector3.Scalar356 * mv2.KVector4.Scalar1356 + mv1.KVector3.Scalar456 * mv2.KVector4.Scalar1456;
                tempScalar[2] += -mv1.KVector3.Scalar134 * mv2.KVector4.Scalar1234 - mv1.KVector3.Scalar135 * mv2.KVector4.Scalar1235 - mv1.KVector3.Scalar145 * mv2.KVector4.Scalar1245 + mv1.KVector3.Scalar345 * mv2.KVector4.Scalar2345 - mv1.KVector3.Scalar136 * mv2.KVector4.Scalar1236 - mv1.KVector3.Scalar146 * mv2.KVector4.Scalar1246 + mv1.KVector3.Scalar346 * mv2.KVector4.Scalar2346 - mv1.KVector3.Scalar156 * mv2.KVector4.Scalar1256 + mv1.KVector3.Scalar356 * mv2.KVector4.Scalar2356 + mv1.KVector3.Scalar456 * mv2.KVector4.Scalar2456;
                tempScalar[4] += mv1.KVector3.Scalar124 * mv2.KVector4.Scalar1234 + mv1.KVector3.Scalar125 * mv2.KVector4.Scalar1235 - mv1.KVector3.Scalar145 * mv2.KVector4.Scalar1345 - mv1.KVector3.Scalar245 * mv2.KVector4.Scalar2345 + mv1.KVector3.Scalar126 * mv2.KVector4.Scalar1236 - mv1.KVector3.Scalar146 * mv2.KVector4.Scalar1346 - mv1.KVector3.Scalar246 * mv2.KVector4.Scalar2346 - mv1.KVector3.Scalar156 * mv2.KVector4.Scalar1356 - mv1.KVector3.Scalar256 * mv2.KVector4.Scalar2356 + mv1.KVector3.Scalar456 * mv2.KVector4.Scalar3456;
                tempScalar[8] += -mv1.KVector3.Scalar123 * mv2.KVector4.Scalar1234 + mv1.KVector3.Scalar125 * mv2.KVector4.Scalar1245 + mv1.KVector3.Scalar135 * mv2.KVector4.Scalar1345 + mv1.KVector3.Scalar235 * mv2.KVector4.Scalar2345 + mv1.KVector3.Scalar126 * mv2.KVector4.Scalar1246 + mv1.KVector3.Scalar136 * mv2.KVector4.Scalar1346 + mv1.KVector3.Scalar236 * mv2.KVector4.Scalar2346 - mv1.KVector3.Scalar156 * mv2.KVector4.Scalar1456 - mv1.KVector3.Scalar256 * mv2.KVector4.Scalar2456 - mv1.KVector3.Scalar356 * mv2.KVector4.Scalar3456;
                tempScalar[16] += -mv1.KVector3.Scalar123 * mv2.KVector4.Scalar1235 - mv1.KVector3.Scalar124 * mv2.KVector4.Scalar1245 - mv1.KVector3.Scalar134 * mv2.KVector4.Scalar1345 - mv1.KVector3.Scalar234 * mv2.KVector4.Scalar2345 + mv1.KVector3.Scalar126 * mv2.KVector4.Scalar1256 + mv1.KVector3.Scalar136 * mv2.KVector4.Scalar1356 + mv1.KVector3.Scalar236 * mv2.KVector4.Scalar2356 + mv1.KVector3.Scalar146 * mv2.KVector4.Scalar1456 + mv1.KVector3.Scalar246 * mv2.KVector4.Scalar2456 + mv1.KVector3.Scalar346 * mv2.KVector4.Scalar3456;
                tempScalar[31] += -mv1.KVector3.Scalar126 * mv2.KVector4.Scalar3456 + mv1.KVector3.Scalar136 * mv2.KVector4.Scalar2456 - mv1.KVector3.Scalar236 * mv2.KVector4.Scalar1456 - mv1.KVector3.Scalar146 * mv2.KVector4.Scalar2356 + mv1.KVector3.Scalar246 * mv2.KVector4.Scalar1356 - mv1.KVector3.Scalar346 * mv2.KVector4.Scalar1256 + mv1.KVector3.Scalar156 * mv2.KVector4.Scalar2346 - mv1.KVector3.Scalar256 * mv2.KVector4.Scalar1346 + mv1.KVector3.Scalar356 * mv2.KVector4.Scalar1246 - mv1.KVector3.Scalar456 * mv2.KVector4.Scalar1236;
                tempScalar[32] += -mv1.KVector3.Scalar123 * mv2.KVector4.Scalar1236 - mv1.KVector3.Scalar124 * mv2.KVector4.Scalar1246 - mv1.KVector3.Scalar134 * mv2.KVector4.Scalar1346 - mv1.KVector3.Scalar234 * mv2.KVector4.Scalar2346 - mv1.KVector3.Scalar125 * mv2.KVector4.Scalar1256 - mv1.KVector3.Scalar135 * mv2.KVector4.Scalar1356 - mv1.KVector3.Scalar235 * mv2.KVector4.Scalar2356 - mv1.KVector3.Scalar145 * mv2.KVector4.Scalar1456 - mv1.KVector3.Scalar245 * mv2.KVector4.Scalar2456 - mv1.KVector3.Scalar345 * mv2.KVector4.Scalar3456;
                tempScalar[47] += mv1.KVector3.Scalar125 * mv2.KVector4.Scalar3456 - mv1.KVector3.Scalar135 * mv2.KVector4.Scalar2456 + mv1.KVector3.Scalar235 * mv2.KVector4.Scalar1456 + mv1.KVector3.Scalar145 * mv2.KVector4.Scalar2356 - mv1.KVector3.Scalar245 * mv2.KVector4.Scalar1356 + mv1.KVector3.Scalar345 * mv2.KVector4.Scalar1256 - mv1.KVector3.Scalar156 * mv2.KVector4.Scalar2345 + mv1.KVector3.Scalar256 * mv2.KVector4.Scalar1345 - mv1.KVector3.Scalar356 * mv2.KVector4.Scalar1245 + mv1.KVector3.Scalar456 * mv2.KVector4.Scalar1235;
                tempScalar[55] += -mv1.KVector3.Scalar124 * mv2.KVector4.Scalar3456 + mv1.KVector3.Scalar134 * mv2.KVector4.Scalar2456 - mv1.KVector3.Scalar234 * mv2.KVector4.Scalar1456 - mv1.KVector3.Scalar145 * mv2.KVector4.Scalar2346 + mv1.KVector3.Scalar245 * mv2.KVector4.Scalar1346 - mv1.KVector3.Scalar345 * mv2.KVector4.Scalar1246 + mv1.KVector3.Scalar146 * mv2.KVector4.Scalar2345 - mv1.KVector3.Scalar246 * mv2.KVector4.Scalar1345 + mv1.KVector3.Scalar346 * mv2.KVector4.Scalar1245 - mv1.KVector3.Scalar456 * mv2.KVector4.Scalar1234;
                tempScalar[59] += mv1.KVector3.Scalar123 * mv2.KVector4.Scalar3456 - mv1.KVector3.Scalar134 * mv2.KVector4.Scalar2356 + mv1.KVector3.Scalar234 * mv2.KVector4.Scalar1356 + mv1.KVector3.Scalar135 * mv2.KVector4.Scalar2346 - mv1.KVector3.Scalar235 * mv2.KVector4.Scalar1346 + mv1.KVector3.Scalar345 * mv2.KVector4.Scalar1236 - mv1.KVector3.Scalar136 * mv2.KVector4.Scalar2345 + mv1.KVector3.Scalar236 * mv2.KVector4.Scalar1345 - mv1.KVector3.Scalar346 * mv2.KVector4.Scalar1235 + mv1.KVector3.Scalar356 * mv2.KVector4.Scalar1234;
                tempScalar[61] += -mv1.KVector3.Scalar123 * mv2.KVector4.Scalar2456 + mv1.KVector3.Scalar124 * mv2.KVector4.Scalar2356 - mv1.KVector3.Scalar234 * mv2.KVector4.Scalar1256 - mv1.KVector3.Scalar125 * mv2.KVector4.Scalar2346 + mv1.KVector3.Scalar235 * mv2.KVector4.Scalar1246 - mv1.KVector3.Scalar245 * mv2.KVector4.Scalar1236 + mv1.KVector3.Scalar126 * mv2.KVector4.Scalar2345 - mv1.KVector3.Scalar236 * mv2.KVector4.Scalar1245 + mv1.KVector3.Scalar246 * mv2.KVector4.Scalar1235 - mv1.KVector3.Scalar256 * mv2.KVector4.Scalar1234;
                tempScalar[62] += mv1.KVector3.Scalar123 * mv2.KVector4.Scalar1456 - mv1.KVector3.Scalar124 * mv2.KVector4.Scalar1356 + mv1.KVector3.Scalar134 * mv2.KVector4.Scalar1256 + mv1.KVector3.Scalar125 * mv2.KVector4.Scalar1346 - mv1.KVector3.Scalar135 * mv2.KVector4.Scalar1246 + mv1.KVector3.Scalar145 * mv2.KVector4.Scalar1236 - mv1.KVector3.Scalar126 * mv2.KVector4.Scalar1345 + mv1.KVector3.Scalar136 * mv2.KVector4.Scalar1245 - mv1.KVector3.Scalar146 * mv2.KVector4.Scalar1235 + mv1.KVector3.Scalar156 * mv2.KVector4.Scalar1234;
            }
            
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[15] += -mv1.KVector3.Scalar156 * mv2.KVector5.Scalar23456 + mv1.KVector3.Scalar256 * mv2.KVector5.Scalar13456 - mv1.KVector3.Scalar356 * mv2.KVector5.Scalar12456 + mv1.KVector3.Scalar456 * mv2.KVector5.Scalar12356;
                tempScalar[23] += mv1.KVector3.Scalar146 * mv2.KVector5.Scalar23456 - mv1.KVector3.Scalar246 * mv2.KVector5.Scalar13456 + mv1.KVector3.Scalar346 * mv2.KVector5.Scalar12456 - mv1.KVector3.Scalar456 * mv2.KVector5.Scalar12346;
                tempScalar[27] += -mv1.KVector3.Scalar136 * mv2.KVector5.Scalar23456 + mv1.KVector3.Scalar236 * mv2.KVector5.Scalar13456 - mv1.KVector3.Scalar346 * mv2.KVector5.Scalar12356 + mv1.KVector3.Scalar356 * mv2.KVector5.Scalar12346;
                tempScalar[29] += mv1.KVector3.Scalar126 * mv2.KVector5.Scalar23456 - mv1.KVector3.Scalar236 * mv2.KVector5.Scalar12456 + mv1.KVector3.Scalar246 * mv2.KVector5.Scalar12356 - mv1.KVector3.Scalar256 * mv2.KVector5.Scalar12346;
                tempScalar[30] += -mv1.KVector3.Scalar126 * mv2.KVector5.Scalar13456 + mv1.KVector3.Scalar136 * mv2.KVector5.Scalar12456 - mv1.KVector3.Scalar146 * mv2.KVector5.Scalar12356 + mv1.KVector3.Scalar156 * mv2.KVector5.Scalar12346;
                tempScalar[39] += -mv1.KVector3.Scalar145 * mv2.KVector5.Scalar23456 + mv1.KVector3.Scalar245 * mv2.KVector5.Scalar13456 - mv1.KVector3.Scalar345 * mv2.KVector5.Scalar12456 + mv1.KVector3.Scalar456 * mv2.KVector5.Scalar12345;
                tempScalar[43] += mv1.KVector3.Scalar135 * mv2.KVector5.Scalar23456 - mv1.KVector3.Scalar235 * mv2.KVector5.Scalar13456 + mv1.KVector3.Scalar345 * mv2.KVector5.Scalar12356 - mv1.KVector3.Scalar356 * mv2.KVector5.Scalar12345;
                tempScalar[45] += -mv1.KVector3.Scalar125 * mv2.KVector5.Scalar23456 + mv1.KVector3.Scalar235 * mv2.KVector5.Scalar12456 - mv1.KVector3.Scalar245 * mv2.KVector5.Scalar12356 + mv1.KVector3.Scalar256 * mv2.KVector5.Scalar12345;
                tempScalar[46] += mv1.KVector3.Scalar125 * mv2.KVector5.Scalar13456 - mv1.KVector3.Scalar135 * mv2.KVector5.Scalar12456 + mv1.KVector3.Scalar145 * mv2.KVector5.Scalar12356 - mv1.KVector3.Scalar156 * mv2.KVector5.Scalar12345;
                tempScalar[51] += -mv1.KVector3.Scalar134 * mv2.KVector5.Scalar23456 + mv1.KVector3.Scalar234 * mv2.KVector5.Scalar13456 - mv1.KVector3.Scalar345 * mv2.KVector5.Scalar12346 + mv1.KVector3.Scalar346 * mv2.KVector5.Scalar12345;
                tempScalar[53] += mv1.KVector3.Scalar124 * mv2.KVector5.Scalar23456 - mv1.KVector3.Scalar234 * mv2.KVector5.Scalar12456 + mv1.KVector3.Scalar245 * mv2.KVector5.Scalar12346 - mv1.KVector3.Scalar246 * mv2.KVector5.Scalar12345;
                tempScalar[54] += -mv1.KVector3.Scalar124 * mv2.KVector5.Scalar13456 + mv1.KVector3.Scalar134 * mv2.KVector5.Scalar12456 - mv1.KVector3.Scalar145 * mv2.KVector5.Scalar12346 + mv1.KVector3.Scalar146 * mv2.KVector5.Scalar12345;
                tempScalar[57] += -mv1.KVector3.Scalar123 * mv2.KVector5.Scalar23456 + mv1.KVector3.Scalar234 * mv2.KVector5.Scalar12356 - mv1.KVector3.Scalar235 * mv2.KVector5.Scalar12346 + mv1.KVector3.Scalar236 * mv2.KVector5.Scalar12345;
                tempScalar[58] += mv1.KVector3.Scalar123 * mv2.KVector5.Scalar13456 - mv1.KVector3.Scalar134 * mv2.KVector5.Scalar12356 + mv1.KVector3.Scalar135 * mv2.KVector5.Scalar12346 - mv1.KVector3.Scalar136 * mv2.KVector5.Scalar12345;
                tempScalar[60] += -mv1.KVector3.Scalar123 * mv2.KVector5.Scalar12456 + mv1.KVector3.Scalar124 * mv2.KVector5.Scalar12356 - mv1.KVector3.Scalar125 * mv2.KVector5.Scalar12346 + mv1.KVector3.Scalar126 * mv2.KVector5.Scalar12345;
            }
            
            if (!mv2.KVector6.IsZero())
            {
                tempScalar[7] += mv1.KVector3.Scalar456 * mv2.KVector6.Scalar123456;
                tempScalar[11] += -mv1.KVector3.Scalar356 * mv2.KVector6.Scalar123456;
                tempScalar[13] += mv1.KVector3.Scalar256 * mv2.KVector6.Scalar123456;
                tempScalar[14] += -mv1.KVector3.Scalar156 * mv2.KVector6.Scalar123456;
                tempScalar[19] += mv1.KVector3.Scalar346 * mv2.KVector6.Scalar123456;
                tempScalar[21] += -mv1.KVector3.Scalar246 * mv2.KVector6.Scalar123456;
                tempScalar[22] += mv1.KVector3.Scalar146 * mv2.KVector6.Scalar123456;
                tempScalar[25] += mv1.KVector3.Scalar236 * mv2.KVector6.Scalar123456;
                tempScalar[26] += -mv1.KVector3.Scalar136 * mv2.KVector6.Scalar123456;
                tempScalar[28] += mv1.KVector3.Scalar126 * mv2.KVector6.Scalar123456;
                tempScalar[35] += -mv1.KVector3.Scalar345 * mv2.KVector6.Scalar123456;
                tempScalar[37] += mv1.KVector3.Scalar245 * mv2.KVector6.Scalar123456;
                tempScalar[38] += -mv1.KVector3.Scalar145 * mv2.KVector6.Scalar123456;
                tempScalar[41] += -mv1.KVector3.Scalar235 * mv2.KVector6.Scalar123456;
                tempScalar[42] += mv1.KVector3.Scalar135 * mv2.KVector6.Scalar123456;
                tempScalar[44] += -mv1.KVector3.Scalar125 * mv2.KVector6.Scalar123456;
                tempScalar[49] += mv1.KVector3.Scalar234 * mv2.KVector6.Scalar123456;
                tempScalar[50] += -mv1.KVector3.Scalar134 * mv2.KVector6.Scalar123456;
                tempScalar[52] += mv1.KVector3.Scalar124 * mv2.KVector6.Scalar123456;
                tempScalar[56] += -mv1.KVector3.Scalar123 * mv2.KVector6.Scalar123456;
            }
            
        }
        
        if (!mv1.KVector4.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[7] += mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar4 + mv1.KVector4.Scalar1235 * mv2.KVector1.Scalar5 + mv1.KVector4.Scalar1236 * mv2.KVector1.Scalar6;
                tempScalar[11] += -mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar3 + mv1.KVector4.Scalar1245 * mv2.KVector1.Scalar5 + mv1.KVector4.Scalar1246 * mv2.KVector1.Scalar6;
                tempScalar[13] += mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar2 + mv1.KVector4.Scalar1345 * mv2.KVector1.Scalar5 + mv1.KVector4.Scalar1346 * mv2.KVector1.Scalar6;
                tempScalar[14] += -mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar1 + mv1.KVector4.Scalar2345 * mv2.KVector1.Scalar5 + mv1.KVector4.Scalar2346 * mv2.KVector1.Scalar6;
                tempScalar[19] += -mv1.KVector4.Scalar1235 * mv2.KVector1.Scalar3 - mv1.KVector4.Scalar1245 * mv2.KVector1.Scalar4 + mv1.KVector4.Scalar1256 * mv2.KVector1.Scalar6;
                tempScalar[21] += mv1.KVector4.Scalar1235 * mv2.KVector1.Scalar2 - mv1.KVector4.Scalar1345 * mv2.KVector1.Scalar4 + mv1.KVector4.Scalar1356 * mv2.KVector1.Scalar6;
                tempScalar[22] += -mv1.KVector4.Scalar1235 * mv2.KVector1.Scalar1 - mv1.KVector4.Scalar2345 * mv2.KVector1.Scalar4 + mv1.KVector4.Scalar2356 * mv2.KVector1.Scalar6;
                tempScalar[25] += mv1.KVector4.Scalar1245 * mv2.KVector1.Scalar2 + mv1.KVector4.Scalar1345 * mv2.KVector1.Scalar3 + mv1.KVector4.Scalar1456 * mv2.KVector1.Scalar6;
                tempScalar[26] += -mv1.KVector4.Scalar1245 * mv2.KVector1.Scalar1 + mv1.KVector4.Scalar2345 * mv2.KVector1.Scalar3 + mv1.KVector4.Scalar2456 * mv2.KVector1.Scalar6;
                tempScalar[28] += -mv1.KVector4.Scalar1345 * mv2.KVector1.Scalar1 - mv1.KVector4.Scalar2345 * mv2.KVector1.Scalar2 + mv1.KVector4.Scalar3456 * mv2.KVector1.Scalar6;
                tempScalar[35] += -mv1.KVector4.Scalar1236 * mv2.KVector1.Scalar3 - mv1.KVector4.Scalar1246 * mv2.KVector1.Scalar4 - mv1.KVector4.Scalar1256 * mv2.KVector1.Scalar5;
                tempScalar[37] += mv1.KVector4.Scalar1236 * mv2.KVector1.Scalar2 - mv1.KVector4.Scalar1346 * mv2.KVector1.Scalar4 - mv1.KVector4.Scalar1356 * mv2.KVector1.Scalar5;
                tempScalar[38] += -mv1.KVector4.Scalar1236 * mv2.KVector1.Scalar1 - mv1.KVector4.Scalar2346 * mv2.KVector1.Scalar4 - mv1.KVector4.Scalar2356 * mv2.KVector1.Scalar5;
                tempScalar[41] += mv1.KVector4.Scalar1246 * mv2.KVector1.Scalar2 + mv1.KVector4.Scalar1346 * mv2.KVector1.Scalar3 - mv1.KVector4.Scalar1456 * mv2.KVector1.Scalar5;
                tempScalar[42] += -mv1.KVector4.Scalar1246 * mv2.KVector1.Scalar1 + mv1.KVector4.Scalar2346 * mv2.KVector1.Scalar3 - mv1.KVector4.Scalar2456 * mv2.KVector1.Scalar5;
                tempScalar[44] += -mv1.KVector4.Scalar1346 * mv2.KVector1.Scalar1 - mv1.KVector4.Scalar2346 * mv2.KVector1.Scalar2 - mv1.KVector4.Scalar3456 * mv2.KVector1.Scalar5;
                tempScalar[49] += mv1.KVector4.Scalar1256 * mv2.KVector1.Scalar2 + mv1.KVector4.Scalar1356 * mv2.KVector1.Scalar3 + mv1.KVector4.Scalar1456 * mv2.KVector1.Scalar4;
                tempScalar[50] += -mv1.KVector4.Scalar1256 * mv2.KVector1.Scalar1 + mv1.KVector4.Scalar2356 * mv2.KVector1.Scalar3 + mv1.KVector4.Scalar2456 * mv2.KVector1.Scalar4;
                tempScalar[52] += -mv1.KVector4.Scalar1356 * mv2.KVector1.Scalar1 - mv1.KVector4.Scalar2356 * mv2.KVector1.Scalar2 + mv1.KVector4.Scalar3456 * mv2.KVector1.Scalar4;
                tempScalar[56] += -mv1.KVector4.Scalar1456 * mv2.KVector1.Scalar1 - mv1.KVector4.Scalar2456 * mv2.KVector1.Scalar2 - mv1.KVector4.Scalar3456 * mv2.KVector1.Scalar3;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[15] += -mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar45 + mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar35 - mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar25 + mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar15 - mv1.KVector4.Scalar1236 * mv2.KVector2.Scalar46 + mv1.KVector4.Scalar1246 * mv2.KVector2.Scalar36 - mv1.KVector4.Scalar1346 * mv2.KVector2.Scalar26 + mv1.KVector4.Scalar2346 * mv2.KVector2.Scalar16;
                tempScalar[23] += mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar45 - mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar34 + mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar24 - mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar14 - mv1.KVector4.Scalar1236 * mv2.KVector2.Scalar56 + mv1.KVector4.Scalar1256 * mv2.KVector2.Scalar36 - mv1.KVector4.Scalar1356 * mv2.KVector2.Scalar26 + mv1.KVector4.Scalar2356 * mv2.KVector2.Scalar16;
                tempScalar[27] += -mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar35 + mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar34 - mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar23 + mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar13 - mv1.KVector4.Scalar1246 * mv2.KVector2.Scalar56 + mv1.KVector4.Scalar1256 * mv2.KVector2.Scalar46 - mv1.KVector4.Scalar1456 * mv2.KVector2.Scalar26 + mv1.KVector4.Scalar2456 * mv2.KVector2.Scalar16;
                tempScalar[29] += mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar25 - mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar24 + mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar23 - mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar12 - mv1.KVector4.Scalar1346 * mv2.KVector2.Scalar56 + mv1.KVector4.Scalar1356 * mv2.KVector2.Scalar46 - mv1.KVector4.Scalar1456 * mv2.KVector2.Scalar36 + mv1.KVector4.Scalar3456 * mv2.KVector2.Scalar16;
                tempScalar[30] += -mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar15 + mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar14 - mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar13 + mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar12 - mv1.KVector4.Scalar2346 * mv2.KVector2.Scalar56 + mv1.KVector4.Scalar2356 * mv2.KVector2.Scalar46 - mv1.KVector4.Scalar2456 * mv2.KVector2.Scalar36 + mv1.KVector4.Scalar3456 * mv2.KVector2.Scalar26;
                tempScalar[39] += mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar46 + mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar56 - mv1.KVector4.Scalar1246 * mv2.KVector2.Scalar34 + mv1.KVector4.Scalar1346 * mv2.KVector2.Scalar24 - mv1.KVector4.Scalar2346 * mv2.KVector2.Scalar14 - mv1.KVector4.Scalar1256 * mv2.KVector2.Scalar35 + mv1.KVector4.Scalar1356 * mv2.KVector2.Scalar25 - mv1.KVector4.Scalar2356 * mv2.KVector2.Scalar15;
                tempScalar[43] += -mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar36 + mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar56 + mv1.KVector4.Scalar1236 * mv2.KVector2.Scalar34 - mv1.KVector4.Scalar1346 * mv2.KVector2.Scalar23 + mv1.KVector4.Scalar2346 * mv2.KVector2.Scalar13 - mv1.KVector4.Scalar1256 * mv2.KVector2.Scalar45 + mv1.KVector4.Scalar1456 * mv2.KVector2.Scalar25 - mv1.KVector4.Scalar2456 * mv2.KVector2.Scalar15;
                tempScalar[45] += mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar26 + mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar56 - mv1.KVector4.Scalar1236 * mv2.KVector2.Scalar24 + mv1.KVector4.Scalar1246 * mv2.KVector2.Scalar23 - mv1.KVector4.Scalar2346 * mv2.KVector2.Scalar12 - mv1.KVector4.Scalar1356 * mv2.KVector2.Scalar45 + mv1.KVector4.Scalar1456 * mv2.KVector2.Scalar35 - mv1.KVector4.Scalar3456 * mv2.KVector2.Scalar15;
                tempScalar[46] += -mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar16 + mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar56 + mv1.KVector4.Scalar1236 * mv2.KVector2.Scalar14 - mv1.KVector4.Scalar1246 * mv2.KVector2.Scalar13 + mv1.KVector4.Scalar1346 * mv2.KVector2.Scalar12 - mv1.KVector4.Scalar2356 * mv2.KVector2.Scalar45 + mv1.KVector4.Scalar2456 * mv2.KVector2.Scalar35 - mv1.KVector4.Scalar3456 * mv2.KVector2.Scalar25;
                tempScalar[51] += -mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar36 - mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar46 + mv1.KVector4.Scalar1236 * mv2.KVector2.Scalar35 + mv1.KVector4.Scalar1246 * mv2.KVector2.Scalar45 - mv1.KVector4.Scalar1356 * mv2.KVector2.Scalar23 + mv1.KVector4.Scalar2356 * mv2.KVector2.Scalar13 - mv1.KVector4.Scalar1456 * mv2.KVector2.Scalar24 + mv1.KVector4.Scalar2456 * mv2.KVector2.Scalar14;
                tempScalar[53] += mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar26 - mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar46 - mv1.KVector4.Scalar1236 * mv2.KVector2.Scalar25 + mv1.KVector4.Scalar1346 * mv2.KVector2.Scalar45 + mv1.KVector4.Scalar1256 * mv2.KVector2.Scalar23 - mv1.KVector4.Scalar2356 * mv2.KVector2.Scalar12 - mv1.KVector4.Scalar1456 * mv2.KVector2.Scalar34 + mv1.KVector4.Scalar3456 * mv2.KVector2.Scalar14;
                tempScalar[54] += -mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar16 - mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar46 + mv1.KVector4.Scalar1236 * mv2.KVector2.Scalar15 + mv1.KVector4.Scalar2346 * mv2.KVector2.Scalar45 - mv1.KVector4.Scalar1256 * mv2.KVector2.Scalar13 + mv1.KVector4.Scalar1356 * mv2.KVector2.Scalar12 - mv1.KVector4.Scalar2456 * mv2.KVector2.Scalar34 + mv1.KVector4.Scalar3456 * mv2.KVector2.Scalar24;
                tempScalar[57] += mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar26 + mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar36 - mv1.KVector4.Scalar1246 * mv2.KVector2.Scalar25 - mv1.KVector4.Scalar1346 * mv2.KVector2.Scalar35 + mv1.KVector4.Scalar1256 * mv2.KVector2.Scalar24 + mv1.KVector4.Scalar1356 * mv2.KVector2.Scalar34 - mv1.KVector4.Scalar2456 * mv2.KVector2.Scalar12 - mv1.KVector4.Scalar3456 * mv2.KVector2.Scalar13;
                tempScalar[58] += -mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar16 + mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar36 + mv1.KVector4.Scalar1246 * mv2.KVector2.Scalar15 - mv1.KVector4.Scalar2346 * mv2.KVector2.Scalar35 - mv1.KVector4.Scalar1256 * mv2.KVector2.Scalar14 + mv1.KVector4.Scalar2356 * mv2.KVector2.Scalar34 + mv1.KVector4.Scalar1456 * mv2.KVector2.Scalar12 - mv1.KVector4.Scalar3456 * mv2.KVector2.Scalar23;
                tempScalar[60] += -mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar16 - mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar26 + mv1.KVector4.Scalar1346 * mv2.KVector2.Scalar15 + mv1.KVector4.Scalar2346 * mv2.KVector2.Scalar25 - mv1.KVector4.Scalar1356 * mv2.KVector2.Scalar14 - mv1.KVector4.Scalar2356 * mv2.KVector2.Scalar24 + mv1.KVector4.Scalar1456 * mv2.KVector2.Scalar13 + mv1.KVector4.Scalar2456 * mv2.KVector2.Scalar23;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[1] += -mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar234 - mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar235 - mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar245 - mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar345 - mv1.KVector4.Scalar1236 * mv2.KVector3.Scalar236 - mv1.KVector4.Scalar1246 * mv2.KVector3.Scalar246 - mv1.KVector4.Scalar1346 * mv2.KVector3.Scalar346 - mv1.KVector4.Scalar1256 * mv2.KVector3.Scalar256 - mv1.KVector4.Scalar1356 * mv2.KVector3.Scalar356 - mv1.KVector4.Scalar1456 * mv2.KVector3.Scalar456;
                tempScalar[2] += mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar134 + mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar135 + mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar145 - mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar345 + mv1.KVector4.Scalar1236 * mv2.KVector3.Scalar136 + mv1.KVector4.Scalar1246 * mv2.KVector3.Scalar146 - mv1.KVector4.Scalar2346 * mv2.KVector3.Scalar346 + mv1.KVector4.Scalar1256 * mv2.KVector3.Scalar156 - mv1.KVector4.Scalar2356 * mv2.KVector3.Scalar356 - mv1.KVector4.Scalar2456 * mv2.KVector3.Scalar456;
                tempScalar[4] += -mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar124 - mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar125 + mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar145 + mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar245 - mv1.KVector4.Scalar1236 * mv2.KVector3.Scalar126 + mv1.KVector4.Scalar1346 * mv2.KVector3.Scalar146 + mv1.KVector4.Scalar2346 * mv2.KVector3.Scalar246 + mv1.KVector4.Scalar1356 * mv2.KVector3.Scalar156 + mv1.KVector4.Scalar2356 * mv2.KVector3.Scalar256 - mv1.KVector4.Scalar3456 * mv2.KVector3.Scalar456;
                tempScalar[8] += mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar123 - mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar125 - mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar135 - mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar235 - mv1.KVector4.Scalar1246 * mv2.KVector3.Scalar126 - mv1.KVector4.Scalar1346 * mv2.KVector3.Scalar136 - mv1.KVector4.Scalar2346 * mv2.KVector3.Scalar236 + mv1.KVector4.Scalar1456 * mv2.KVector3.Scalar156 + mv1.KVector4.Scalar2456 * mv2.KVector3.Scalar256 + mv1.KVector4.Scalar3456 * mv2.KVector3.Scalar356;
                tempScalar[16] += mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar123 + mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar124 + mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar134 + mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar234 - mv1.KVector4.Scalar1256 * mv2.KVector3.Scalar126 - mv1.KVector4.Scalar1356 * mv2.KVector3.Scalar136 - mv1.KVector4.Scalar2356 * mv2.KVector3.Scalar236 - mv1.KVector4.Scalar1456 * mv2.KVector3.Scalar146 - mv1.KVector4.Scalar2456 * mv2.KVector3.Scalar246 - mv1.KVector4.Scalar3456 * mv2.KVector3.Scalar346;
                tempScalar[31] += mv1.KVector4.Scalar1236 * mv2.KVector3.Scalar456 - mv1.KVector4.Scalar1246 * mv2.KVector3.Scalar356 + mv1.KVector4.Scalar1346 * mv2.KVector3.Scalar256 - mv1.KVector4.Scalar2346 * mv2.KVector3.Scalar156 + mv1.KVector4.Scalar1256 * mv2.KVector3.Scalar346 - mv1.KVector4.Scalar1356 * mv2.KVector3.Scalar246 + mv1.KVector4.Scalar2356 * mv2.KVector3.Scalar146 + mv1.KVector4.Scalar1456 * mv2.KVector3.Scalar236 - mv1.KVector4.Scalar2456 * mv2.KVector3.Scalar136 + mv1.KVector4.Scalar3456 * mv2.KVector3.Scalar126;
                tempScalar[32] += mv1.KVector4.Scalar1236 * mv2.KVector3.Scalar123 + mv1.KVector4.Scalar1246 * mv2.KVector3.Scalar124 + mv1.KVector4.Scalar1346 * mv2.KVector3.Scalar134 + mv1.KVector4.Scalar2346 * mv2.KVector3.Scalar234 + mv1.KVector4.Scalar1256 * mv2.KVector3.Scalar125 + mv1.KVector4.Scalar1356 * mv2.KVector3.Scalar135 + mv1.KVector4.Scalar2356 * mv2.KVector3.Scalar235 + mv1.KVector4.Scalar1456 * mv2.KVector3.Scalar145 + mv1.KVector4.Scalar2456 * mv2.KVector3.Scalar245 + mv1.KVector4.Scalar3456 * mv2.KVector3.Scalar345;
                tempScalar[47] += -mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar456 + mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar356 - mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar256 + mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar156 - mv1.KVector4.Scalar1256 * mv2.KVector3.Scalar345 + mv1.KVector4.Scalar1356 * mv2.KVector3.Scalar245 - mv1.KVector4.Scalar2356 * mv2.KVector3.Scalar145 - mv1.KVector4.Scalar1456 * mv2.KVector3.Scalar235 + mv1.KVector4.Scalar2456 * mv2.KVector3.Scalar135 - mv1.KVector4.Scalar3456 * mv2.KVector3.Scalar125;
                tempScalar[55] += mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar456 - mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar346 + mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar246 - mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar146 + mv1.KVector4.Scalar1246 * mv2.KVector3.Scalar345 - mv1.KVector4.Scalar1346 * mv2.KVector3.Scalar245 + mv1.KVector4.Scalar2346 * mv2.KVector3.Scalar145 + mv1.KVector4.Scalar1456 * mv2.KVector3.Scalar234 - mv1.KVector4.Scalar2456 * mv2.KVector3.Scalar134 + mv1.KVector4.Scalar3456 * mv2.KVector3.Scalar124;
                tempScalar[59] += -mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar356 + mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar346 - mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar236 + mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar136 - mv1.KVector4.Scalar1236 * mv2.KVector3.Scalar345 + mv1.KVector4.Scalar1346 * mv2.KVector3.Scalar235 - mv1.KVector4.Scalar2346 * mv2.KVector3.Scalar135 - mv1.KVector4.Scalar1356 * mv2.KVector3.Scalar234 + mv1.KVector4.Scalar2356 * mv2.KVector3.Scalar134 - mv1.KVector4.Scalar3456 * mv2.KVector3.Scalar123;
                tempScalar[61] += mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar256 - mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar246 + mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar236 - mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar126 + mv1.KVector4.Scalar1236 * mv2.KVector3.Scalar245 - mv1.KVector4.Scalar1246 * mv2.KVector3.Scalar235 + mv1.KVector4.Scalar2346 * mv2.KVector3.Scalar125 + mv1.KVector4.Scalar1256 * mv2.KVector3.Scalar234 - mv1.KVector4.Scalar2356 * mv2.KVector3.Scalar124 + mv1.KVector4.Scalar2456 * mv2.KVector3.Scalar123;
                tempScalar[62] += -mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar156 + mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar146 - mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar136 + mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar126 - mv1.KVector4.Scalar1236 * mv2.KVector3.Scalar145 + mv1.KVector4.Scalar1246 * mv2.KVector3.Scalar135 - mv1.KVector4.Scalar1346 * mv2.KVector3.Scalar125 - mv1.KVector4.Scalar1256 * mv2.KVector3.Scalar134 + mv1.KVector4.Scalar1356 * mv2.KVector3.Scalar124 - mv1.KVector4.Scalar1456 * mv2.KVector3.Scalar123;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[3] += mv1.KVector4.Scalar1345 * mv2.KVector4.Scalar2345 - mv1.KVector4.Scalar2345 * mv2.KVector4.Scalar1345 + mv1.KVector4.Scalar1346 * mv2.KVector4.Scalar2346 - mv1.KVector4.Scalar2346 * mv2.KVector4.Scalar1346 + mv1.KVector4.Scalar1356 * mv2.KVector4.Scalar2356 - mv1.KVector4.Scalar2356 * mv2.KVector4.Scalar1356 + mv1.KVector4.Scalar1456 * mv2.KVector4.Scalar2456 - mv1.KVector4.Scalar2456 * mv2.KVector4.Scalar1456;
                tempScalar[5] += -mv1.KVector4.Scalar1245 * mv2.KVector4.Scalar2345 + mv1.KVector4.Scalar2345 * mv2.KVector4.Scalar1245 - mv1.KVector4.Scalar1246 * mv2.KVector4.Scalar2346 + mv1.KVector4.Scalar2346 * mv2.KVector4.Scalar1246 - mv1.KVector4.Scalar1256 * mv2.KVector4.Scalar2356 + mv1.KVector4.Scalar2356 * mv2.KVector4.Scalar1256 + mv1.KVector4.Scalar1456 * mv2.KVector4.Scalar3456 - mv1.KVector4.Scalar3456 * mv2.KVector4.Scalar1456;
                tempScalar[6] += mv1.KVector4.Scalar1245 * mv2.KVector4.Scalar1345 - mv1.KVector4.Scalar1345 * mv2.KVector4.Scalar1245 + mv1.KVector4.Scalar1246 * mv2.KVector4.Scalar1346 - mv1.KVector4.Scalar1346 * mv2.KVector4.Scalar1246 + mv1.KVector4.Scalar1256 * mv2.KVector4.Scalar1356 - mv1.KVector4.Scalar1356 * mv2.KVector4.Scalar1256 + mv1.KVector4.Scalar2456 * mv2.KVector4.Scalar3456 - mv1.KVector4.Scalar3456 * mv2.KVector4.Scalar2456;
                tempScalar[9] += mv1.KVector4.Scalar1235 * mv2.KVector4.Scalar2345 - mv1.KVector4.Scalar2345 * mv2.KVector4.Scalar1235 + mv1.KVector4.Scalar1236 * mv2.KVector4.Scalar2346 - mv1.KVector4.Scalar2346 * mv2.KVector4.Scalar1236 - mv1.KVector4.Scalar1256 * mv2.KVector4.Scalar2456 - mv1.KVector4.Scalar1356 * mv2.KVector4.Scalar3456 + mv1.KVector4.Scalar2456 * mv2.KVector4.Scalar1256 + mv1.KVector4.Scalar3456 * mv2.KVector4.Scalar1356;
                tempScalar[10] += -mv1.KVector4.Scalar1235 * mv2.KVector4.Scalar1345 + mv1.KVector4.Scalar1345 * mv2.KVector4.Scalar1235 - mv1.KVector4.Scalar1236 * mv2.KVector4.Scalar1346 + mv1.KVector4.Scalar1346 * mv2.KVector4.Scalar1236 + mv1.KVector4.Scalar1256 * mv2.KVector4.Scalar1456 - mv1.KVector4.Scalar2356 * mv2.KVector4.Scalar3456 - mv1.KVector4.Scalar1456 * mv2.KVector4.Scalar1256 + mv1.KVector4.Scalar3456 * mv2.KVector4.Scalar2356;
                tempScalar[12] += mv1.KVector4.Scalar1235 * mv2.KVector4.Scalar1245 - mv1.KVector4.Scalar1245 * mv2.KVector4.Scalar1235 + mv1.KVector4.Scalar1236 * mv2.KVector4.Scalar1246 - mv1.KVector4.Scalar1246 * mv2.KVector4.Scalar1236 + mv1.KVector4.Scalar1356 * mv2.KVector4.Scalar1456 + mv1.KVector4.Scalar2356 * mv2.KVector4.Scalar2456 - mv1.KVector4.Scalar1456 * mv2.KVector4.Scalar1356 - mv1.KVector4.Scalar2456 * mv2.KVector4.Scalar2356;
                tempScalar[17] += -mv1.KVector4.Scalar1234 * mv2.KVector4.Scalar2345 + mv1.KVector4.Scalar2345 * mv2.KVector4.Scalar1234 + mv1.KVector4.Scalar1236 * mv2.KVector4.Scalar2356 + mv1.KVector4.Scalar1246 * mv2.KVector4.Scalar2456 + mv1.KVector4.Scalar1346 * mv2.KVector4.Scalar3456 - mv1.KVector4.Scalar2356 * mv2.KVector4.Scalar1236 - mv1.KVector4.Scalar2456 * mv2.KVector4.Scalar1246 - mv1.KVector4.Scalar3456 * mv2.KVector4.Scalar1346;
                tempScalar[18] += mv1.KVector4.Scalar1234 * mv2.KVector4.Scalar1345 - mv1.KVector4.Scalar1345 * mv2.KVector4.Scalar1234 - mv1.KVector4.Scalar1236 * mv2.KVector4.Scalar1356 - mv1.KVector4.Scalar1246 * mv2.KVector4.Scalar1456 + mv1.KVector4.Scalar2346 * mv2.KVector4.Scalar3456 + mv1.KVector4.Scalar1356 * mv2.KVector4.Scalar1236 + mv1.KVector4.Scalar1456 * mv2.KVector4.Scalar1246 - mv1.KVector4.Scalar3456 * mv2.KVector4.Scalar2346;
                tempScalar[20] += -mv1.KVector4.Scalar1234 * mv2.KVector4.Scalar1245 + mv1.KVector4.Scalar1245 * mv2.KVector4.Scalar1234 + mv1.KVector4.Scalar1236 * mv2.KVector4.Scalar1256 - mv1.KVector4.Scalar1346 * mv2.KVector4.Scalar1456 - mv1.KVector4.Scalar2346 * mv2.KVector4.Scalar2456 - mv1.KVector4.Scalar1256 * mv2.KVector4.Scalar1236 + mv1.KVector4.Scalar1456 * mv2.KVector4.Scalar1346 + mv1.KVector4.Scalar2456 * mv2.KVector4.Scalar2346;
                tempScalar[24] += mv1.KVector4.Scalar1234 * mv2.KVector4.Scalar1235 - mv1.KVector4.Scalar1235 * mv2.KVector4.Scalar1234 + mv1.KVector4.Scalar1246 * mv2.KVector4.Scalar1256 + mv1.KVector4.Scalar1346 * mv2.KVector4.Scalar1356 + mv1.KVector4.Scalar2346 * mv2.KVector4.Scalar2356 - mv1.KVector4.Scalar1256 * mv2.KVector4.Scalar1246 - mv1.KVector4.Scalar1356 * mv2.KVector4.Scalar1346 - mv1.KVector4.Scalar2356 * mv2.KVector4.Scalar2346;
                tempScalar[33] += -mv1.KVector4.Scalar1234 * mv2.KVector4.Scalar2346 - mv1.KVector4.Scalar1235 * mv2.KVector4.Scalar2356 - mv1.KVector4.Scalar1245 * mv2.KVector4.Scalar2456 - mv1.KVector4.Scalar1345 * mv2.KVector4.Scalar3456 + mv1.KVector4.Scalar2346 * mv2.KVector4.Scalar1234 + mv1.KVector4.Scalar2356 * mv2.KVector4.Scalar1235 + mv1.KVector4.Scalar2456 * mv2.KVector4.Scalar1245 + mv1.KVector4.Scalar3456 * mv2.KVector4.Scalar1345;
                tempScalar[34] += mv1.KVector4.Scalar1234 * mv2.KVector4.Scalar1346 + mv1.KVector4.Scalar1235 * mv2.KVector4.Scalar1356 + mv1.KVector4.Scalar1245 * mv2.KVector4.Scalar1456 - mv1.KVector4.Scalar2345 * mv2.KVector4.Scalar3456 - mv1.KVector4.Scalar1346 * mv2.KVector4.Scalar1234 - mv1.KVector4.Scalar1356 * mv2.KVector4.Scalar1235 - mv1.KVector4.Scalar1456 * mv2.KVector4.Scalar1245 + mv1.KVector4.Scalar3456 * mv2.KVector4.Scalar2345;
                tempScalar[36] += -mv1.KVector4.Scalar1234 * mv2.KVector4.Scalar1246 - mv1.KVector4.Scalar1235 * mv2.KVector4.Scalar1256 + mv1.KVector4.Scalar1345 * mv2.KVector4.Scalar1456 + mv1.KVector4.Scalar2345 * mv2.KVector4.Scalar2456 + mv1.KVector4.Scalar1246 * mv2.KVector4.Scalar1234 + mv1.KVector4.Scalar1256 * mv2.KVector4.Scalar1235 - mv1.KVector4.Scalar1456 * mv2.KVector4.Scalar1345 - mv1.KVector4.Scalar2456 * mv2.KVector4.Scalar2345;
                tempScalar[40] += mv1.KVector4.Scalar1234 * mv2.KVector4.Scalar1236 - mv1.KVector4.Scalar1245 * mv2.KVector4.Scalar1256 - mv1.KVector4.Scalar1345 * mv2.KVector4.Scalar1356 - mv1.KVector4.Scalar2345 * mv2.KVector4.Scalar2356 - mv1.KVector4.Scalar1236 * mv2.KVector4.Scalar1234 + mv1.KVector4.Scalar1256 * mv2.KVector4.Scalar1245 + mv1.KVector4.Scalar1356 * mv2.KVector4.Scalar1345 + mv1.KVector4.Scalar2356 * mv2.KVector4.Scalar2345;
                tempScalar[48] += mv1.KVector4.Scalar1235 * mv2.KVector4.Scalar1236 + mv1.KVector4.Scalar1245 * mv2.KVector4.Scalar1246 + mv1.KVector4.Scalar1345 * mv2.KVector4.Scalar1346 + mv1.KVector4.Scalar2345 * mv2.KVector4.Scalar2346 - mv1.KVector4.Scalar1236 * mv2.KVector4.Scalar1235 - mv1.KVector4.Scalar1246 * mv2.KVector4.Scalar1245 - mv1.KVector4.Scalar1346 * mv2.KVector4.Scalar1345 - mv1.KVector4.Scalar2346 * mv2.KVector4.Scalar2345;
            }
            
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[7] += -mv1.KVector4.Scalar1456 * mv2.KVector5.Scalar23456 + mv1.KVector4.Scalar2456 * mv2.KVector5.Scalar13456 - mv1.KVector4.Scalar3456 * mv2.KVector5.Scalar12456;
                tempScalar[11] += mv1.KVector4.Scalar1356 * mv2.KVector5.Scalar23456 - mv1.KVector4.Scalar2356 * mv2.KVector5.Scalar13456 + mv1.KVector4.Scalar3456 * mv2.KVector5.Scalar12356;
                tempScalar[13] += -mv1.KVector4.Scalar1256 * mv2.KVector5.Scalar23456 + mv1.KVector4.Scalar2356 * mv2.KVector5.Scalar12456 - mv1.KVector4.Scalar2456 * mv2.KVector5.Scalar12356;
                tempScalar[14] += mv1.KVector4.Scalar1256 * mv2.KVector5.Scalar13456 - mv1.KVector4.Scalar1356 * mv2.KVector5.Scalar12456 + mv1.KVector4.Scalar1456 * mv2.KVector5.Scalar12356;
                tempScalar[19] += -mv1.KVector4.Scalar1346 * mv2.KVector5.Scalar23456 + mv1.KVector4.Scalar2346 * mv2.KVector5.Scalar13456 - mv1.KVector4.Scalar3456 * mv2.KVector5.Scalar12346;
                tempScalar[21] += mv1.KVector4.Scalar1246 * mv2.KVector5.Scalar23456 - mv1.KVector4.Scalar2346 * mv2.KVector5.Scalar12456 + mv1.KVector4.Scalar2456 * mv2.KVector5.Scalar12346;
                tempScalar[22] += -mv1.KVector4.Scalar1246 * mv2.KVector5.Scalar13456 + mv1.KVector4.Scalar1346 * mv2.KVector5.Scalar12456 - mv1.KVector4.Scalar1456 * mv2.KVector5.Scalar12346;
                tempScalar[25] += -mv1.KVector4.Scalar1236 * mv2.KVector5.Scalar23456 + mv1.KVector4.Scalar2346 * mv2.KVector5.Scalar12356 - mv1.KVector4.Scalar2356 * mv2.KVector5.Scalar12346;
                tempScalar[26] += mv1.KVector4.Scalar1236 * mv2.KVector5.Scalar13456 - mv1.KVector4.Scalar1346 * mv2.KVector5.Scalar12356 + mv1.KVector4.Scalar1356 * mv2.KVector5.Scalar12346;
                tempScalar[28] += -mv1.KVector4.Scalar1236 * mv2.KVector5.Scalar12456 + mv1.KVector4.Scalar1246 * mv2.KVector5.Scalar12356 - mv1.KVector4.Scalar1256 * mv2.KVector5.Scalar12346;
                tempScalar[35] += mv1.KVector4.Scalar1345 * mv2.KVector5.Scalar23456 - mv1.KVector4.Scalar2345 * mv2.KVector5.Scalar13456 + mv1.KVector4.Scalar3456 * mv2.KVector5.Scalar12345;
                tempScalar[37] += -mv1.KVector4.Scalar1245 * mv2.KVector5.Scalar23456 + mv1.KVector4.Scalar2345 * mv2.KVector5.Scalar12456 - mv1.KVector4.Scalar2456 * mv2.KVector5.Scalar12345;
                tempScalar[38] += mv1.KVector4.Scalar1245 * mv2.KVector5.Scalar13456 - mv1.KVector4.Scalar1345 * mv2.KVector5.Scalar12456 + mv1.KVector4.Scalar1456 * mv2.KVector5.Scalar12345;
                tempScalar[41] += mv1.KVector4.Scalar1235 * mv2.KVector5.Scalar23456 - mv1.KVector4.Scalar2345 * mv2.KVector5.Scalar12356 + mv1.KVector4.Scalar2356 * mv2.KVector5.Scalar12345;
                tempScalar[42] += -mv1.KVector4.Scalar1235 * mv2.KVector5.Scalar13456 + mv1.KVector4.Scalar1345 * mv2.KVector5.Scalar12356 - mv1.KVector4.Scalar1356 * mv2.KVector5.Scalar12345;
                tempScalar[44] += mv1.KVector4.Scalar1235 * mv2.KVector5.Scalar12456 - mv1.KVector4.Scalar1245 * mv2.KVector5.Scalar12356 + mv1.KVector4.Scalar1256 * mv2.KVector5.Scalar12345;
                tempScalar[49] += -mv1.KVector4.Scalar1234 * mv2.KVector5.Scalar23456 + mv1.KVector4.Scalar2345 * mv2.KVector5.Scalar12346 - mv1.KVector4.Scalar2346 * mv2.KVector5.Scalar12345;
                tempScalar[50] += mv1.KVector4.Scalar1234 * mv2.KVector5.Scalar13456 - mv1.KVector4.Scalar1345 * mv2.KVector5.Scalar12346 + mv1.KVector4.Scalar1346 * mv2.KVector5.Scalar12345;
                tempScalar[52] += -mv1.KVector4.Scalar1234 * mv2.KVector5.Scalar12456 + mv1.KVector4.Scalar1245 * mv2.KVector5.Scalar12346 - mv1.KVector4.Scalar1246 * mv2.KVector5.Scalar12345;
                tempScalar[56] += mv1.KVector4.Scalar1234 * mv2.KVector5.Scalar12356 - mv1.KVector4.Scalar1235 * mv2.KVector5.Scalar12346 + mv1.KVector4.Scalar1236 * mv2.KVector5.Scalar12345;
            }
            
        }
        
        if (!mv1.KVector5.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[63] += mv1.KVector5.Scalar12345 * mv2.KVector1.Scalar6 - mv1.KVector5.Scalar12346 * mv2.KVector1.Scalar5 + mv1.KVector5.Scalar12356 * mv2.KVector1.Scalar4 - mv1.KVector5.Scalar12456 * mv2.KVector1.Scalar3 + mv1.KVector5.Scalar13456 * mv2.KVector1.Scalar2 - mv1.KVector5.Scalar23456 * mv2.KVector1.Scalar1;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[31] += -mv1.KVector5.Scalar12346 * mv2.KVector2.Scalar56 + mv1.KVector5.Scalar12356 * mv2.KVector2.Scalar46 - mv1.KVector5.Scalar12456 * mv2.KVector2.Scalar36 + mv1.KVector5.Scalar13456 * mv2.KVector2.Scalar26 - mv1.KVector5.Scalar23456 * mv2.KVector2.Scalar16;
                tempScalar[47] += mv1.KVector5.Scalar12345 * mv2.KVector2.Scalar56 - mv1.KVector5.Scalar12356 * mv2.KVector2.Scalar45 + mv1.KVector5.Scalar12456 * mv2.KVector2.Scalar35 - mv1.KVector5.Scalar13456 * mv2.KVector2.Scalar25 + mv1.KVector5.Scalar23456 * mv2.KVector2.Scalar15;
                tempScalar[55] += -mv1.KVector5.Scalar12345 * mv2.KVector2.Scalar46 + mv1.KVector5.Scalar12346 * mv2.KVector2.Scalar45 - mv1.KVector5.Scalar12456 * mv2.KVector2.Scalar34 + mv1.KVector5.Scalar13456 * mv2.KVector2.Scalar24 - mv1.KVector5.Scalar23456 * mv2.KVector2.Scalar14;
                tempScalar[59] += mv1.KVector5.Scalar12345 * mv2.KVector2.Scalar36 - mv1.KVector5.Scalar12346 * mv2.KVector2.Scalar35 + mv1.KVector5.Scalar12356 * mv2.KVector2.Scalar34 - mv1.KVector5.Scalar13456 * mv2.KVector2.Scalar23 + mv1.KVector5.Scalar23456 * mv2.KVector2.Scalar13;
                tempScalar[61] += -mv1.KVector5.Scalar12345 * mv2.KVector2.Scalar26 + mv1.KVector5.Scalar12346 * mv2.KVector2.Scalar25 - mv1.KVector5.Scalar12356 * mv2.KVector2.Scalar24 + mv1.KVector5.Scalar12456 * mv2.KVector2.Scalar23 - mv1.KVector5.Scalar23456 * mv2.KVector2.Scalar12;
                tempScalar[62] += mv1.KVector5.Scalar12345 * mv2.KVector2.Scalar16 - mv1.KVector5.Scalar12346 * mv2.KVector2.Scalar15 + mv1.KVector5.Scalar12356 * mv2.KVector2.Scalar14 - mv1.KVector5.Scalar12456 * mv2.KVector2.Scalar13 + mv1.KVector5.Scalar13456 * mv2.KVector2.Scalar12;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[15] += -mv1.KVector5.Scalar12356 * mv2.KVector3.Scalar456 + mv1.KVector5.Scalar12456 * mv2.KVector3.Scalar356 - mv1.KVector5.Scalar13456 * mv2.KVector3.Scalar256 + mv1.KVector5.Scalar23456 * mv2.KVector3.Scalar156;
                tempScalar[23] += mv1.KVector5.Scalar12346 * mv2.KVector3.Scalar456 - mv1.KVector5.Scalar12456 * mv2.KVector3.Scalar346 + mv1.KVector5.Scalar13456 * mv2.KVector3.Scalar246 - mv1.KVector5.Scalar23456 * mv2.KVector3.Scalar146;
                tempScalar[27] += -mv1.KVector5.Scalar12346 * mv2.KVector3.Scalar356 + mv1.KVector5.Scalar12356 * mv2.KVector3.Scalar346 - mv1.KVector5.Scalar13456 * mv2.KVector3.Scalar236 + mv1.KVector5.Scalar23456 * mv2.KVector3.Scalar136;
                tempScalar[29] += mv1.KVector5.Scalar12346 * mv2.KVector3.Scalar256 - mv1.KVector5.Scalar12356 * mv2.KVector3.Scalar246 + mv1.KVector5.Scalar12456 * mv2.KVector3.Scalar236 - mv1.KVector5.Scalar23456 * mv2.KVector3.Scalar126;
                tempScalar[30] += -mv1.KVector5.Scalar12346 * mv2.KVector3.Scalar156 + mv1.KVector5.Scalar12356 * mv2.KVector3.Scalar146 - mv1.KVector5.Scalar12456 * mv2.KVector3.Scalar136 + mv1.KVector5.Scalar13456 * mv2.KVector3.Scalar126;
                tempScalar[39] += -mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar456 + mv1.KVector5.Scalar12456 * mv2.KVector3.Scalar345 - mv1.KVector5.Scalar13456 * mv2.KVector3.Scalar245 + mv1.KVector5.Scalar23456 * mv2.KVector3.Scalar145;
                tempScalar[43] += mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar356 - mv1.KVector5.Scalar12356 * mv2.KVector3.Scalar345 + mv1.KVector5.Scalar13456 * mv2.KVector3.Scalar235 - mv1.KVector5.Scalar23456 * mv2.KVector3.Scalar135;
                tempScalar[45] += -mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar256 + mv1.KVector5.Scalar12356 * mv2.KVector3.Scalar245 - mv1.KVector5.Scalar12456 * mv2.KVector3.Scalar235 + mv1.KVector5.Scalar23456 * mv2.KVector3.Scalar125;
                tempScalar[46] += mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar156 - mv1.KVector5.Scalar12356 * mv2.KVector3.Scalar145 + mv1.KVector5.Scalar12456 * mv2.KVector3.Scalar135 - mv1.KVector5.Scalar13456 * mv2.KVector3.Scalar125;
                tempScalar[51] += -mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar346 + mv1.KVector5.Scalar12346 * mv2.KVector3.Scalar345 - mv1.KVector5.Scalar13456 * mv2.KVector3.Scalar234 + mv1.KVector5.Scalar23456 * mv2.KVector3.Scalar134;
                tempScalar[53] += mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar246 - mv1.KVector5.Scalar12346 * mv2.KVector3.Scalar245 + mv1.KVector5.Scalar12456 * mv2.KVector3.Scalar234 - mv1.KVector5.Scalar23456 * mv2.KVector3.Scalar124;
                tempScalar[54] += -mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar146 + mv1.KVector5.Scalar12346 * mv2.KVector3.Scalar145 - mv1.KVector5.Scalar12456 * mv2.KVector3.Scalar134 + mv1.KVector5.Scalar13456 * mv2.KVector3.Scalar124;
                tempScalar[57] += -mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar236 + mv1.KVector5.Scalar12346 * mv2.KVector3.Scalar235 - mv1.KVector5.Scalar12356 * mv2.KVector3.Scalar234 + mv1.KVector5.Scalar23456 * mv2.KVector3.Scalar123;
                tempScalar[58] += mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar136 - mv1.KVector5.Scalar12346 * mv2.KVector3.Scalar135 + mv1.KVector5.Scalar12356 * mv2.KVector3.Scalar134 - mv1.KVector5.Scalar13456 * mv2.KVector3.Scalar123;
                tempScalar[60] += -mv1.KVector5.Scalar12345 * mv2.KVector3.Scalar126 + mv1.KVector5.Scalar12346 * mv2.KVector3.Scalar125 - mv1.KVector5.Scalar12356 * mv2.KVector3.Scalar124 + mv1.KVector5.Scalar12456 * mv2.KVector3.Scalar123;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[7] += mv1.KVector5.Scalar12456 * mv2.KVector4.Scalar3456 - mv1.KVector5.Scalar13456 * mv2.KVector4.Scalar2456 + mv1.KVector5.Scalar23456 * mv2.KVector4.Scalar1456;
                tempScalar[11] += -mv1.KVector5.Scalar12356 * mv2.KVector4.Scalar3456 + mv1.KVector5.Scalar13456 * mv2.KVector4.Scalar2356 - mv1.KVector5.Scalar23456 * mv2.KVector4.Scalar1356;
                tempScalar[13] += mv1.KVector5.Scalar12356 * mv2.KVector4.Scalar2456 - mv1.KVector5.Scalar12456 * mv2.KVector4.Scalar2356 + mv1.KVector5.Scalar23456 * mv2.KVector4.Scalar1256;
                tempScalar[14] += -mv1.KVector5.Scalar12356 * mv2.KVector4.Scalar1456 + mv1.KVector5.Scalar12456 * mv2.KVector4.Scalar1356 - mv1.KVector5.Scalar13456 * mv2.KVector4.Scalar1256;
                tempScalar[19] += mv1.KVector5.Scalar12346 * mv2.KVector4.Scalar3456 - mv1.KVector5.Scalar13456 * mv2.KVector4.Scalar2346 + mv1.KVector5.Scalar23456 * mv2.KVector4.Scalar1346;
                tempScalar[21] += -mv1.KVector5.Scalar12346 * mv2.KVector4.Scalar2456 + mv1.KVector5.Scalar12456 * mv2.KVector4.Scalar2346 - mv1.KVector5.Scalar23456 * mv2.KVector4.Scalar1246;
                tempScalar[22] += mv1.KVector5.Scalar12346 * mv2.KVector4.Scalar1456 - mv1.KVector5.Scalar12456 * mv2.KVector4.Scalar1346 + mv1.KVector5.Scalar13456 * mv2.KVector4.Scalar1246;
                tempScalar[25] += mv1.KVector5.Scalar12346 * mv2.KVector4.Scalar2356 - mv1.KVector5.Scalar12356 * mv2.KVector4.Scalar2346 + mv1.KVector5.Scalar23456 * mv2.KVector4.Scalar1236;
                tempScalar[26] += -mv1.KVector5.Scalar12346 * mv2.KVector4.Scalar1356 + mv1.KVector5.Scalar12356 * mv2.KVector4.Scalar1346 - mv1.KVector5.Scalar13456 * mv2.KVector4.Scalar1236;
                tempScalar[28] += mv1.KVector5.Scalar12346 * mv2.KVector4.Scalar1256 - mv1.KVector5.Scalar12356 * mv2.KVector4.Scalar1246 + mv1.KVector5.Scalar12456 * mv2.KVector4.Scalar1236;
                tempScalar[35] += -mv1.KVector5.Scalar12345 * mv2.KVector4.Scalar3456 + mv1.KVector5.Scalar13456 * mv2.KVector4.Scalar2345 - mv1.KVector5.Scalar23456 * mv2.KVector4.Scalar1345;
                tempScalar[37] += mv1.KVector5.Scalar12345 * mv2.KVector4.Scalar2456 - mv1.KVector5.Scalar12456 * mv2.KVector4.Scalar2345 + mv1.KVector5.Scalar23456 * mv2.KVector4.Scalar1245;
                tempScalar[38] += -mv1.KVector5.Scalar12345 * mv2.KVector4.Scalar1456 + mv1.KVector5.Scalar12456 * mv2.KVector4.Scalar1345 - mv1.KVector5.Scalar13456 * mv2.KVector4.Scalar1245;
                tempScalar[41] += -mv1.KVector5.Scalar12345 * mv2.KVector4.Scalar2356 + mv1.KVector5.Scalar12356 * mv2.KVector4.Scalar2345 - mv1.KVector5.Scalar23456 * mv2.KVector4.Scalar1235;
                tempScalar[42] += mv1.KVector5.Scalar12345 * mv2.KVector4.Scalar1356 - mv1.KVector5.Scalar12356 * mv2.KVector4.Scalar1345 + mv1.KVector5.Scalar13456 * mv2.KVector4.Scalar1235;
                tempScalar[44] += -mv1.KVector5.Scalar12345 * mv2.KVector4.Scalar1256 + mv1.KVector5.Scalar12356 * mv2.KVector4.Scalar1245 - mv1.KVector5.Scalar12456 * mv2.KVector4.Scalar1235;
                tempScalar[49] += mv1.KVector5.Scalar12345 * mv2.KVector4.Scalar2346 - mv1.KVector5.Scalar12346 * mv2.KVector4.Scalar2345 + mv1.KVector5.Scalar23456 * mv2.KVector4.Scalar1234;
                tempScalar[50] += -mv1.KVector5.Scalar12345 * mv2.KVector4.Scalar1346 + mv1.KVector5.Scalar12346 * mv2.KVector4.Scalar1345 - mv1.KVector5.Scalar13456 * mv2.KVector4.Scalar1234;
                tempScalar[52] += mv1.KVector5.Scalar12345 * mv2.KVector4.Scalar1246 - mv1.KVector5.Scalar12346 * mv2.KVector4.Scalar1245 + mv1.KVector5.Scalar12456 * mv2.KVector4.Scalar1234;
                tempScalar[56] += -mv1.KVector5.Scalar12345 * mv2.KVector4.Scalar1236 + mv1.KVector5.Scalar12346 * mv2.KVector4.Scalar1235 - mv1.KVector5.Scalar12356 * mv2.KVector4.Scalar1234;
            }
            
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[3] += mv1.KVector5.Scalar13456 * mv2.KVector5.Scalar23456 - mv1.KVector5.Scalar23456 * mv2.KVector5.Scalar13456;
                tempScalar[5] += -mv1.KVector5.Scalar12456 * mv2.KVector5.Scalar23456 + mv1.KVector5.Scalar23456 * mv2.KVector5.Scalar12456;
                tempScalar[6] += mv1.KVector5.Scalar12456 * mv2.KVector5.Scalar13456 - mv1.KVector5.Scalar13456 * mv2.KVector5.Scalar12456;
                tempScalar[9] += mv1.KVector5.Scalar12356 * mv2.KVector5.Scalar23456 - mv1.KVector5.Scalar23456 * mv2.KVector5.Scalar12356;
                tempScalar[10] += -mv1.KVector5.Scalar12356 * mv2.KVector5.Scalar13456 + mv1.KVector5.Scalar13456 * mv2.KVector5.Scalar12356;
                tempScalar[12] += mv1.KVector5.Scalar12356 * mv2.KVector5.Scalar12456 - mv1.KVector5.Scalar12456 * mv2.KVector5.Scalar12356;
                tempScalar[17] += -mv1.KVector5.Scalar12346 * mv2.KVector5.Scalar23456 + mv1.KVector5.Scalar23456 * mv2.KVector5.Scalar12346;
                tempScalar[18] += mv1.KVector5.Scalar12346 * mv2.KVector5.Scalar13456 - mv1.KVector5.Scalar13456 * mv2.KVector5.Scalar12346;
                tempScalar[20] += -mv1.KVector5.Scalar12346 * mv2.KVector5.Scalar12456 + mv1.KVector5.Scalar12456 * mv2.KVector5.Scalar12346;
                tempScalar[24] += mv1.KVector5.Scalar12346 * mv2.KVector5.Scalar12356 - mv1.KVector5.Scalar12356 * mv2.KVector5.Scalar12346;
                tempScalar[33] += mv1.KVector5.Scalar12345 * mv2.KVector5.Scalar23456 - mv1.KVector5.Scalar23456 * mv2.KVector5.Scalar12345;
                tempScalar[34] += -mv1.KVector5.Scalar12345 * mv2.KVector5.Scalar13456 + mv1.KVector5.Scalar13456 * mv2.KVector5.Scalar12345;
                tempScalar[36] += mv1.KVector5.Scalar12345 * mv2.KVector5.Scalar12456 - mv1.KVector5.Scalar12456 * mv2.KVector5.Scalar12345;
                tempScalar[40] += -mv1.KVector5.Scalar12345 * mv2.KVector5.Scalar12356 + mv1.KVector5.Scalar12356 * mv2.KVector5.Scalar12345;
                tempScalar[48] += mv1.KVector5.Scalar12345 * mv2.KVector5.Scalar12346 - mv1.KVector5.Scalar12346 * mv2.KVector5.Scalar12345;
            }
            
            if (!mv2.KVector6.IsZero())
            {
                tempScalar[1] += -mv1.KVector5.Scalar23456 * mv2.KVector6.Scalar123456;
                tempScalar[2] += mv1.KVector5.Scalar13456 * mv2.KVector6.Scalar123456;
                tempScalar[4] += -mv1.KVector5.Scalar12456 * mv2.KVector6.Scalar123456;
                tempScalar[8] += mv1.KVector5.Scalar12356 * mv2.KVector6.Scalar123456;
                tempScalar[16] += -mv1.KVector5.Scalar12346 * mv2.KVector6.Scalar123456;
                tempScalar[32] += mv1.KVector5.Scalar12345 * mv2.KVector6.Scalar123456;
            }
            
        }
        
        if (!mv1.KVector6.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[31] += mv1.KVector6.Scalar123456 * mv2.KVector1.Scalar6;
                tempScalar[47] += -mv1.KVector6.Scalar123456 * mv2.KVector1.Scalar5;
                tempScalar[55] += mv1.KVector6.Scalar123456 * mv2.KVector1.Scalar4;
                tempScalar[59] += -mv1.KVector6.Scalar123456 * mv2.KVector1.Scalar3;
                tempScalar[61] += mv1.KVector6.Scalar123456 * mv2.KVector1.Scalar2;
                tempScalar[62] += -mv1.KVector6.Scalar123456 * mv2.KVector1.Scalar1;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[7] += -mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar456;
                tempScalar[11] += mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar356;
                tempScalar[13] += -mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar256;
                tempScalar[14] += mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar156;
                tempScalar[19] += -mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar346;
                tempScalar[21] += mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar246;
                tempScalar[22] += -mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar146;
                tempScalar[25] += -mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar236;
                tempScalar[26] += mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar136;
                tempScalar[28] += -mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar126;
                tempScalar[35] += mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar345;
                tempScalar[37] += -mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar245;
                tempScalar[38] += mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar145;
                tempScalar[41] += mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar235;
                tempScalar[42] += -mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar135;
                tempScalar[44] += mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar125;
                tempScalar[49] += -mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar234;
                tempScalar[50] += mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar134;
                tempScalar[52] += -mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar124;
                tempScalar[56] += mv1.KVector6.Scalar123456 * mv2.KVector3.Scalar123;
            }
            
            if (!mv2.KVector5.IsZero())
            {
                tempScalar[1] += mv1.KVector6.Scalar123456 * mv2.KVector5.Scalar23456;
                tempScalar[2] += -mv1.KVector6.Scalar123456 * mv2.KVector5.Scalar13456;
                tempScalar[4] += mv1.KVector6.Scalar123456 * mv2.KVector5.Scalar12456;
                tempScalar[8] += -mv1.KVector6.Scalar123456 * mv2.KVector5.Scalar12356;
                tempScalar[16] += mv1.KVector6.Scalar123456 * mv2.KVector5.Scalar12346;
                tempScalar[32] += -mv1.KVector6.Scalar123456 * mv2.KVector5.Scalar12345;
            }
            
        }
        
        return Ga6Multivector.Create(tempScalar);
    }
    
}
