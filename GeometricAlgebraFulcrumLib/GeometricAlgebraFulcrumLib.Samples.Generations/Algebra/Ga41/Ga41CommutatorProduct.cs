using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Samples.Generations.Algebra.Ga41;

public static class Ga41CommutatorProduct
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector0 mv1, Ga41KVector0 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector0 mv1, Ga41KVector1 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector0 mv1, Ga41KVector2 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector0 mv1, Ga41KVector3 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector0 mv1, Ga41KVector4 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector0 mv1, Ga41KVector5 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector0 mv1, Ga41Multivector mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector1 mv1, Ga41KVector0 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector2 Cp(this Ga41KVector1 mv1, Ga41KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector2.Zero;
        
        return new Ga41KVector2
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
            Scalar45 = mv1.Scalar4 * mv2.Scalar5 - mv1.Scalar5 * mv2.Scalar4
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector1 Cp(this Ga41KVector1 mv1, Ga41KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector1.Zero;
        
        return new Ga41KVector1
        {
            Scalar1 = -mv1.Scalar2 * mv2.Scalar12 - mv1.Scalar3 * mv2.Scalar13 - mv1.Scalar4 * mv2.Scalar14 - mv1.Scalar5 * mv2.Scalar15,
            Scalar2 = -mv1.Scalar1 * mv2.Scalar12 - mv1.Scalar3 * mv2.Scalar23 - mv1.Scalar4 * mv2.Scalar24 - mv1.Scalar5 * mv2.Scalar25,
            Scalar3 = -mv1.Scalar1 * mv2.Scalar13 + mv1.Scalar2 * mv2.Scalar23 - mv1.Scalar4 * mv2.Scalar34 - mv1.Scalar5 * mv2.Scalar35,
            Scalar4 = -mv1.Scalar1 * mv2.Scalar14 + mv1.Scalar2 * mv2.Scalar24 + mv1.Scalar3 * mv2.Scalar34 - mv1.Scalar5 * mv2.Scalar45,
            Scalar5 = -mv1.Scalar1 * mv2.Scalar15 + mv1.Scalar2 * mv2.Scalar25 + mv1.Scalar3 * mv2.Scalar35 + mv1.Scalar4 * mv2.Scalar45
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector4 Cp(this Ga41KVector1 mv1, Ga41KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector4.Zero;
        
        return new Ga41KVector4
        {
            Scalar1234 = mv1.Scalar1 * mv2.Scalar234 - mv1.Scalar2 * mv2.Scalar134 + mv1.Scalar3 * mv2.Scalar124 - mv1.Scalar4 * mv2.Scalar123,
            Scalar1235 = mv1.Scalar1 * mv2.Scalar235 - mv1.Scalar2 * mv2.Scalar135 + mv1.Scalar3 * mv2.Scalar125 - mv1.Scalar5 * mv2.Scalar123,
            Scalar1245 = mv1.Scalar1 * mv2.Scalar245 - mv1.Scalar2 * mv2.Scalar145 + mv1.Scalar4 * mv2.Scalar125 - mv1.Scalar5 * mv2.Scalar124,
            Scalar1345 = mv1.Scalar1 * mv2.Scalar345 - mv1.Scalar3 * mv2.Scalar145 + mv1.Scalar4 * mv2.Scalar135 - mv1.Scalar5 * mv2.Scalar134,
            Scalar2345 = mv1.Scalar2 * mv2.Scalar345 - mv1.Scalar3 * mv2.Scalar245 + mv1.Scalar4 * mv2.Scalar235 - mv1.Scalar5 * mv2.Scalar234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector3 Cp(this Ga41KVector1 mv1, Ga41KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector3.Zero;
        
        return new Ga41KVector3
        {
            Scalar123 = -mv1.Scalar4 * mv2.Scalar1234 - mv1.Scalar5 * mv2.Scalar1235,
            Scalar124 = mv1.Scalar3 * mv2.Scalar1234 - mv1.Scalar5 * mv2.Scalar1245,
            Scalar134 = -mv1.Scalar2 * mv2.Scalar1234 - mv1.Scalar5 * mv2.Scalar1345,
            Scalar234 = -mv1.Scalar1 * mv2.Scalar1234 - mv1.Scalar5 * mv2.Scalar2345,
            Scalar125 = mv1.Scalar3 * mv2.Scalar1235 + mv1.Scalar4 * mv2.Scalar1245,
            Scalar135 = -mv1.Scalar2 * mv2.Scalar1235 + mv1.Scalar4 * mv2.Scalar1345,
            Scalar235 = -mv1.Scalar1 * mv2.Scalar1235 + mv1.Scalar4 * mv2.Scalar2345,
            Scalar145 = -mv1.Scalar2 * mv2.Scalar1245 - mv1.Scalar3 * mv2.Scalar1345,
            Scalar245 = -mv1.Scalar1 * mv2.Scalar1245 - mv1.Scalar3 * mv2.Scalar2345,
            Scalar345 = -mv1.Scalar1 * mv2.Scalar1345 + mv1.Scalar2 * mv2.Scalar2345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector1 mv1, Ga41KVector5 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    public static Ga41Multivector Cp(this Ga41KVector1 mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
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
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[1] += -mv1.Scalar2 * mv2.KVector2.Scalar12 - mv1.Scalar3 * mv2.KVector2.Scalar13 - mv1.Scalar4 * mv2.KVector2.Scalar14 - mv1.Scalar5 * mv2.KVector2.Scalar15;
            tempScalar[2] += -mv1.Scalar1 * mv2.KVector2.Scalar12 - mv1.Scalar3 * mv2.KVector2.Scalar23 - mv1.Scalar4 * mv2.KVector2.Scalar24 - mv1.Scalar5 * mv2.KVector2.Scalar25;
            tempScalar[4] += -mv1.Scalar1 * mv2.KVector2.Scalar13 + mv1.Scalar2 * mv2.KVector2.Scalar23 - mv1.Scalar4 * mv2.KVector2.Scalar34 - mv1.Scalar5 * mv2.KVector2.Scalar35;
            tempScalar[8] += -mv1.Scalar1 * mv2.KVector2.Scalar14 + mv1.Scalar2 * mv2.KVector2.Scalar24 + mv1.Scalar3 * mv2.KVector2.Scalar34 - mv1.Scalar5 * mv2.KVector2.Scalar45;
            tempScalar[16] += -mv1.Scalar1 * mv2.KVector2.Scalar15 + mv1.Scalar2 * mv2.KVector2.Scalar25 + mv1.Scalar3 * mv2.KVector2.Scalar35 + mv1.Scalar4 * mv2.KVector2.Scalar45;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[15] += mv1.Scalar1 * mv2.KVector3.Scalar234 - mv1.Scalar2 * mv2.KVector3.Scalar134 + mv1.Scalar3 * mv2.KVector3.Scalar124 - mv1.Scalar4 * mv2.KVector3.Scalar123;
            tempScalar[23] += mv1.Scalar1 * mv2.KVector3.Scalar235 - mv1.Scalar2 * mv2.KVector3.Scalar135 + mv1.Scalar3 * mv2.KVector3.Scalar125 - mv1.Scalar5 * mv2.KVector3.Scalar123;
            tempScalar[27] += mv1.Scalar1 * mv2.KVector3.Scalar245 - mv1.Scalar2 * mv2.KVector3.Scalar145 + mv1.Scalar4 * mv2.KVector3.Scalar125 - mv1.Scalar5 * mv2.KVector3.Scalar124;
            tempScalar[29] += mv1.Scalar1 * mv2.KVector3.Scalar345 - mv1.Scalar3 * mv2.KVector3.Scalar145 + mv1.Scalar4 * mv2.KVector3.Scalar135 - mv1.Scalar5 * mv2.KVector3.Scalar134;
            tempScalar[30] += mv1.Scalar2 * mv2.KVector3.Scalar345 - mv1.Scalar3 * mv2.KVector3.Scalar245 + mv1.Scalar4 * mv2.KVector3.Scalar235 - mv1.Scalar5 * mv2.KVector3.Scalar234;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[7] += -mv1.Scalar4 * mv2.KVector4.Scalar1234 - mv1.Scalar5 * mv2.KVector4.Scalar1235;
            tempScalar[11] += mv1.Scalar3 * mv2.KVector4.Scalar1234 - mv1.Scalar5 * mv2.KVector4.Scalar1245;
            tempScalar[13] += -mv1.Scalar2 * mv2.KVector4.Scalar1234 - mv1.Scalar5 * mv2.KVector4.Scalar1345;
            tempScalar[14] += -mv1.Scalar1 * mv2.KVector4.Scalar1234 - mv1.Scalar5 * mv2.KVector4.Scalar2345;
            tempScalar[19] += mv1.Scalar3 * mv2.KVector4.Scalar1235 + mv1.Scalar4 * mv2.KVector4.Scalar1245;
            tempScalar[21] += -mv1.Scalar2 * mv2.KVector4.Scalar1235 + mv1.Scalar4 * mv2.KVector4.Scalar1345;
            tempScalar[22] += -mv1.Scalar1 * mv2.KVector4.Scalar1235 + mv1.Scalar4 * mv2.KVector4.Scalar2345;
            tempScalar[25] += -mv1.Scalar2 * mv2.KVector4.Scalar1245 - mv1.Scalar3 * mv2.KVector4.Scalar1345;
            tempScalar[26] += -mv1.Scalar1 * mv2.KVector4.Scalar1245 - mv1.Scalar3 * mv2.KVector4.Scalar2345;
            tempScalar[28] += -mv1.Scalar1 * mv2.KVector4.Scalar1345 + mv1.Scalar2 * mv2.KVector4.Scalar2345;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector2 mv1, Ga41KVector0 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector1 Cp(this Ga41KVector2 mv1, Ga41KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector1.Zero;
        
        return new Ga41KVector1
        {
            Scalar1 = mv1.Scalar12 * mv2.Scalar2 + mv1.Scalar13 * mv2.Scalar3 + mv1.Scalar14 * mv2.Scalar4 + mv1.Scalar15 * mv2.Scalar5,
            Scalar2 = mv1.Scalar12 * mv2.Scalar1 + mv1.Scalar23 * mv2.Scalar3 + mv1.Scalar24 * mv2.Scalar4 + mv1.Scalar25 * mv2.Scalar5,
            Scalar3 = mv1.Scalar13 * mv2.Scalar1 - mv1.Scalar23 * mv2.Scalar2 + mv1.Scalar34 * mv2.Scalar4 + mv1.Scalar35 * mv2.Scalar5,
            Scalar4 = mv1.Scalar14 * mv2.Scalar1 - mv1.Scalar24 * mv2.Scalar2 - mv1.Scalar34 * mv2.Scalar3 + mv1.Scalar45 * mv2.Scalar5,
            Scalar5 = mv1.Scalar15 * mv2.Scalar1 - mv1.Scalar25 * mv2.Scalar2 - mv1.Scalar35 * mv2.Scalar3 - mv1.Scalar45 * mv2.Scalar4
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector2 Cp(this Ga41KVector2 mv1, Ga41KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector2.Zero;
        
        return new Ga41KVector2
        {
            Scalar12 = -mv1.Scalar13 * mv2.Scalar23 + mv1.Scalar23 * mv2.Scalar13 - mv1.Scalar14 * mv2.Scalar24 + mv1.Scalar24 * mv2.Scalar14 - mv1.Scalar15 * mv2.Scalar25 + mv1.Scalar25 * mv2.Scalar15,
            Scalar13 = mv1.Scalar12 * mv2.Scalar23 - mv1.Scalar23 * mv2.Scalar12 - mv1.Scalar14 * mv2.Scalar34 + mv1.Scalar34 * mv2.Scalar14 - mv1.Scalar15 * mv2.Scalar35 + mv1.Scalar35 * mv2.Scalar15,
            Scalar23 = mv1.Scalar12 * mv2.Scalar13 - mv1.Scalar13 * mv2.Scalar12 - mv1.Scalar24 * mv2.Scalar34 + mv1.Scalar34 * mv2.Scalar24 - mv1.Scalar25 * mv2.Scalar35 + mv1.Scalar35 * mv2.Scalar25,
            Scalar14 = mv1.Scalar12 * mv2.Scalar24 + mv1.Scalar13 * mv2.Scalar34 - mv1.Scalar24 * mv2.Scalar12 - mv1.Scalar34 * mv2.Scalar13 - mv1.Scalar15 * mv2.Scalar45 + mv1.Scalar45 * mv2.Scalar15,
            Scalar24 = mv1.Scalar12 * mv2.Scalar14 + mv1.Scalar23 * mv2.Scalar34 - mv1.Scalar14 * mv2.Scalar12 - mv1.Scalar34 * mv2.Scalar23 - mv1.Scalar25 * mv2.Scalar45 + mv1.Scalar45 * mv2.Scalar25,
            Scalar34 = mv1.Scalar13 * mv2.Scalar14 - mv1.Scalar23 * mv2.Scalar24 - mv1.Scalar14 * mv2.Scalar13 + mv1.Scalar24 * mv2.Scalar23 - mv1.Scalar35 * mv2.Scalar45 + mv1.Scalar45 * mv2.Scalar35,
            Scalar15 = mv1.Scalar12 * mv2.Scalar25 + mv1.Scalar13 * mv2.Scalar35 + mv1.Scalar14 * mv2.Scalar45 - mv1.Scalar25 * mv2.Scalar12 - mv1.Scalar35 * mv2.Scalar13 - mv1.Scalar45 * mv2.Scalar14,
            Scalar25 = mv1.Scalar12 * mv2.Scalar15 + mv1.Scalar23 * mv2.Scalar35 + mv1.Scalar24 * mv2.Scalar45 - mv1.Scalar15 * mv2.Scalar12 - mv1.Scalar35 * mv2.Scalar23 - mv1.Scalar45 * mv2.Scalar24,
            Scalar35 = mv1.Scalar13 * mv2.Scalar15 - mv1.Scalar23 * mv2.Scalar25 + mv1.Scalar34 * mv2.Scalar45 - mv1.Scalar15 * mv2.Scalar13 + mv1.Scalar25 * mv2.Scalar23 - mv1.Scalar45 * mv2.Scalar34,
            Scalar45 = mv1.Scalar14 * mv2.Scalar15 - mv1.Scalar24 * mv2.Scalar25 - mv1.Scalar34 * mv2.Scalar35 - mv1.Scalar15 * mv2.Scalar14 + mv1.Scalar25 * mv2.Scalar24 + mv1.Scalar35 * mv2.Scalar34
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector3 Cp(this Ga41KVector2 mv1, Ga41KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector3.Zero;
        
        return new Ga41KVector3
        {
            Scalar123 = mv1.Scalar14 * mv2.Scalar234 - mv1.Scalar24 * mv2.Scalar134 + mv1.Scalar34 * mv2.Scalar124 + mv1.Scalar15 * mv2.Scalar235 - mv1.Scalar25 * mv2.Scalar135 + mv1.Scalar35 * mv2.Scalar125,
            Scalar124 = -mv1.Scalar13 * mv2.Scalar234 + mv1.Scalar23 * mv2.Scalar134 - mv1.Scalar34 * mv2.Scalar123 + mv1.Scalar15 * mv2.Scalar245 - mv1.Scalar25 * mv2.Scalar145 + mv1.Scalar45 * mv2.Scalar125,
            Scalar134 = mv1.Scalar12 * mv2.Scalar234 - mv1.Scalar23 * mv2.Scalar124 + mv1.Scalar24 * mv2.Scalar123 + mv1.Scalar15 * mv2.Scalar345 - mv1.Scalar35 * mv2.Scalar145 + mv1.Scalar45 * mv2.Scalar135,
            Scalar234 = mv1.Scalar12 * mv2.Scalar134 - mv1.Scalar13 * mv2.Scalar124 + mv1.Scalar14 * mv2.Scalar123 + mv1.Scalar25 * mv2.Scalar345 - mv1.Scalar35 * mv2.Scalar245 + mv1.Scalar45 * mv2.Scalar235,
            Scalar125 = -mv1.Scalar13 * mv2.Scalar235 + mv1.Scalar23 * mv2.Scalar135 - mv1.Scalar14 * mv2.Scalar245 + mv1.Scalar24 * mv2.Scalar145 - mv1.Scalar35 * mv2.Scalar123 - mv1.Scalar45 * mv2.Scalar124,
            Scalar135 = mv1.Scalar12 * mv2.Scalar235 - mv1.Scalar23 * mv2.Scalar125 - mv1.Scalar14 * mv2.Scalar345 + mv1.Scalar34 * mv2.Scalar145 + mv1.Scalar25 * mv2.Scalar123 - mv1.Scalar45 * mv2.Scalar134,
            Scalar235 = mv1.Scalar12 * mv2.Scalar135 - mv1.Scalar13 * mv2.Scalar125 - mv1.Scalar24 * mv2.Scalar345 + mv1.Scalar34 * mv2.Scalar245 + mv1.Scalar15 * mv2.Scalar123 - mv1.Scalar45 * mv2.Scalar234,
            Scalar145 = mv1.Scalar12 * mv2.Scalar245 + mv1.Scalar13 * mv2.Scalar345 - mv1.Scalar24 * mv2.Scalar125 - mv1.Scalar34 * mv2.Scalar135 + mv1.Scalar25 * mv2.Scalar124 + mv1.Scalar35 * mv2.Scalar134,
            Scalar245 = mv1.Scalar12 * mv2.Scalar145 + mv1.Scalar23 * mv2.Scalar345 - mv1.Scalar14 * mv2.Scalar125 - mv1.Scalar34 * mv2.Scalar235 + mv1.Scalar15 * mv2.Scalar124 + mv1.Scalar35 * mv2.Scalar234,
            Scalar345 = mv1.Scalar13 * mv2.Scalar145 - mv1.Scalar23 * mv2.Scalar245 - mv1.Scalar14 * mv2.Scalar135 + mv1.Scalar24 * mv2.Scalar235 + mv1.Scalar15 * mv2.Scalar134 - mv1.Scalar25 * mv2.Scalar234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector4 Cp(this Ga41KVector2 mv1, Ga41KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector4.Zero;
        
        return new Ga41KVector4
        {
            Scalar1234 = -mv1.Scalar15 * mv2.Scalar2345 + mv1.Scalar25 * mv2.Scalar1345 - mv1.Scalar35 * mv2.Scalar1245 + mv1.Scalar45 * mv2.Scalar1235,
            Scalar1235 = mv1.Scalar14 * mv2.Scalar2345 - mv1.Scalar24 * mv2.Scalar1345 + mv1.Scalar34 * mv2.Scalar1245 - mv1.Scalar45 * mv2.Scalar1234,
            Scalar1245 = -mv1.Scalar13 * mv2.Scalar2345 + mv1.Scalar23 * mv2.Scalar1345 - mv1.Scalar34 * mv2.Scalar1235 + mv1.Scalar35 * mv2.Scalar1234,
            Scalar1345 = mv1.Scalar12 * mv2.Scalar2345 - mv1.Scalar23 * mv2.Scalar1245 + mv1.Scalar24 * mv2.Scalar1235 - mv1.Scalar25 * mv2.Scalar1234,
            Scalar2345 = mv1.Scalar12 * mv2.Scalar1345 - mv1.Scalar13 * mv2.Scalar1245 + mv1.Scalar14 * mv2.Scalar1235 - mv1.Scalar15 * mv2.Scalar1234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector2 mv1, Ga41KVector5 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    public static Ga41Multivector Cp(this Ga41KVector2 mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[1] += mv1.Scalar12 * mv2.KVector1.Scalar2 + mv1.Scalar13 * mv2.KVector1.Scalar3 + mv1.Scalar14 * mv2.KVector1.Scalar4 + mv1.Scalar15 * mv2.KVector1.Scalar5;
            tempScalar[2] += mv1.Scalar12 * mv2.KVector1.Scalar1 + mv1.Scalar23 * mv2.KVector1.Scalar3 + mv1.Scalar24 * mv2.KVector1.Scalar4 + mv1.Scalar25 * mv2.KVector1.Scalar5;
            tempScalar[4] += mv1.Scalar13 * mv2.KVector1.Scalar1 - mv1.Scalar23 * mv2.KVector1.Scalar2 + mv1.Scalar34 * mv2.KVector1.Scalar4 + mv1.Scalar35 * mv2.KVector1.Scalar5;
            tempScalar[8] += mv1.Scalar14 * mv2.KVector1.Scalar1 - mv1.Scalar24 * mv2.KVector1.Scalar2 - mv1.Scalar34 * mv2.KVector1.Scalar3 + mv1.Scalar45 * mv2.KVector1.Scalar5;
            tempScalar[16] += mv1.Scalar15 * mv2.KVector1.Scalar1 - mv1.Scalar25 * mv2.KVector1.Scalar2 - mv1.Scalar35 * mv2.KVector1.Scalar3 - mv1.Scalar45 * mv2.KVector1.Scalar4;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[3] += -mv1.Scalar13 * mv2.KVector2.Scalar23 + mv1.Scalar23 * mv2.KVector2.Scalar13 - mv1.Scalar14 * mv2.KVector2.Scalar24 + mv1.Scalar24 * mv2.KVector2.Scalar14 - mv1.Scalar15 * mv2.KVector2.Scalar25 + mv1.Scalar25 * mv2.KVector2.Scalar15;
            tempScalar[5] += mv1.Scalar12 * mv2.KVector2.Scalar23 - mv1.Scalar23 * mv2.KVector2.Scalar12 - mv1.Scalar14 * mv2.KVector2.Scalar34 + mv1.Scalar34 * mv2.KVector2.Scalar14 - mv1.Scalar15 * mv2.KVector2.Scalar35 + mv1.Scalar35 * mv2.KVector2.Scalar15;
            tempScalar[6] += mv1.Scalar12 * mv2.KVector2.Scalar13 - mv1.Scalar13 * mv2.KVector2.Scalar12 - mv1.Scalar24 * mv2.KVector2.Scalar34 + mv1.Scalar34 * mv2.KVector2.Scalar24 - mv1.Scalar25 * mv2.KVector2.Scalar35 + mv1.Scalar35 * mv2.KVector2.Scalar25;
            tempScalar[9] += mv1.Scalar12 * mv2.KVector2.Scalar24 + mv1.Scalar13 * mv2.KVector2.Scalar34 - mv1.Scalar24 * mv2.KVector2.Scalar12 - mv1.Scalar34 * mv2.KVector2.Scalar13 - mv1.Scalar15 * mv2.KVector2.Scalar45 + mv1.Scalar45 * mv2.KVector2.Scalar15;
            tempScalar[10] += mv1.Scalar12 * mv2.KVector2.Scalar14 + mv1.Scalar23 * mv2.KVector2.Scalar34 - mv1.Scalar14 * mv2.KVector2.Scalar12 - mv1.Scalar34 * mv2.KVector2.Scalar23 - mv1.Scalar25 * mv2.KVector2.Scalar45 + mv1.Scalar45 * mv2.KVector2.Scalar25;
            tempScalar[12] += mv1.Scalar13 * mv2.KVector2.Scalar14 - mv1.Scalar23 * mv2.KVector2.Scalar24 - mv1.Scalar14 * mv2.KVector2.Scalar13 + mv1.Scalar24 * mv2.KVector2.Scalar23 - mv1.Scalar35 * mv2.KVector2.Scalar45 + mv1.Scalar45 * mv2.KVector2.Scalar35;
            tempScalar[17] += mv1.Scalar12 * mv2.KVector2.Scalar25 + mv1.Scalar13 * mv2.KVector2.Scalar35 + mv1.Scalar14 * mv2.KVector2.Scalar45 - mv1.Scalar25 * mv2.KVector2.Scalar12 - mv1.Scalar35 * mv2.KVector2.Scalar13 - mv1.Scalar45 * mv2.KVector2.Scalar14;
            tempScalar[18] += mv1.Scalar12 * mv2.KVector2.Scalar15 + mv1.Scalar23 * mv2.KVector2.Scalar35 + mv1.Scalar24 * mv2.KVector2.Scalar45 - mv1.Scalar15 * mv2.KVector2.Scalar12 - mv1.Scalar35 * mv2.KVector2.Scalar23 - mv1.Scalar45 * mv2.KVector2.Scalar24;
            tempScalar[20] += mv1.Scalar13 * mv2.KVector2.Scalar15 - mv1.Scalar23 * mv2.KVector2.Scalar25 + mv1.Scalar34 * mv2.KVector2.Scalar45 - mv1.Scalar15 * mv2.KVector2.Scalar13 + mv1.Scalar25 * mv2.KVector2.Scalar23 - mv1.Scalar45 * mv2.KVector2.Scalar34;
            tempScalar[24] += mv1.Scalar14 * mv2.KVector2.Scalar15 - mv1.Scalar24 * mv2.KVector2.Scalar25 - mv1.Scalar34 * mv2.KVector2.Scalar35 - mv1.Scalar15 * mv2.KVector2.Scalar14 + mv1.Scalar25 * mv2.KVector2.Scalar24 + mv1.Scalar35 * mv2.KVector2.Scalar34;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[7] += mv1.Scalar14 * mv2.KVector3.Scalar234 - mv1.Scalar24 * mv2.KVector3.Scalar134 + mv1.Scalar34 * mv2.KVector3.Scalar124 + mv1.Scalar15 * mv2.KVector3.Scalar235 - mv1.Scalar25 * mv2.KVector3.Scalar135 + mv1.Scalar35 * mv2.KVector3.Scalar125;
            tempScalar[11] += -mv1.Scalar13 * mv2.KVector3.Scalar234 + mv1.Scalar23 * mv2.KVector3.Scalar134 - mv1.Scalar34 * mv2.KVector3.Scalar123 + mv1.Scalar15 * mv2.KVector3.Scalar245 - mv1.Scalar25 * mv2.KVector3.Scalar145 + mv1.Scalar45 * mv2.KVector3.Scalar125;
            tempScalar[13] += mv1.Scalar12 * mv2.KVector3.Scalar234 - mv1.Scalar23 * mv2.KVector3.Scalar124 + mv1.Scalar24 * mv2.KVector3.Scalar123 + mv1.Scalar15 * mv2.KVector3.Scalar345 - mv1.Scalar35 * mv2.KVector3.Scalar145 + mv1.Scalar45 * mv2.KVector3.Scalar135;
            tempScalar[14] += mv1.Scalar12 * mv2.KVector3.Scalar134 - mv1.Scalar13 * mv2.KVector3.Scalar124 + mv1.Scalar14 * mv2.KVector3.Scalar123 + mv1.Scalar25 * mv2.KVector3.Scalar345 - mv1.Scalar35 * mv2.KVector3.Scalar245 + mv1.Scalar45 * mv2.KVector3.Scalar235;
            tempScalar[19] += -mv1.Scalar13 * mv2.KVector3.Scalar235 + mv1.Scalar23 * mv2.KVector3.Scalar135 - mv1.Scalar14 * mv2.KVector3.Scalar245 + mv1.Scalar24 * mv2.KVector3.Scalar145 - mv1.Scalar35 * mv2.KVector3.Scalar123 - mv1.Scalar45 * mv2.KVector3.Scalar124;
            tempScalar[21] += mv1.Scalar12 * mv2.KVector3.Scalar235 - mv1.Scalar23 * mv2.KVector3.Scalar125 - mv1.Scalar14 * mv2.KVector3.Scalar345 + mv1.Scalar34 * mv2.KVector3.Scalar145 + mv1.Scalar25 * mv2.KVector3.Scalar123 - mv1.Scalar45 * mv2.KVector3.Scalar134;
            tempScalar[22] += mv1.Scalar12 * mv2.KVector3.Scalar135 - mv1.Scalar13 * mv2.KVector3.Scalar125 - mv1.Scalar24 * mv2.KVector3.Scalar345 + mv1.Scalar34 * mv2.KVector3.Scalar245 + mv1.Scalar15 * mv2.KVector3.Scalar123 - mv1.Scalar45 * mv2.KVector3.Scalar234;
            tempScalar[25] += mv1.Scalar12 * mv2.KVector3.Scalar245 + mv1.Scalar13 * mv2.KVector3.Scalar345 - mv1.Scalar24 * mv2.KVector3.Scalar125 - mv1.Scalar34 * mv2.KVector3.Scalar135 + mv1.Scalar25 * mv2.KVector3.Scalar124 + mv1.Scalar35 * mv2.KVector3.Scalar134;
            tempScalar[26] += mv1.Scalar12 * mv2.KVector3.Scalar145 + mv1.Scalar23 * mv2.KVector3.Scalar345 - mv1.Scalar14 * mv2.KVector3.Scalar125 - mv1.Scalar34 * mv2.KVector3.Scalar235 + mv1.Scalar15 * mv2.KVector3.Scalar124 + mv1.Scalar35 * mv2.KVector3.Scalar234;
            tempScalar[28] += mv1.Scalar13 * mv2.KVector3.Scalar145 - mv1.Scalar23 * mv2.KVector3.Scalar245 - mv1.Scalar14 * mv2.KVector3.Scalar135 + mv1.Scalar24 * mv2.KVector3.Scalar235 + mv1.Scalar15 * mv2.KVector3.Scalar134 - mv1.Scalar25 * mv2.KVector3.Scalar234;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[15] += -mv1.Scalar15 * mv2.KVector4.Scalar2345 + mv1.Scalar25 * mv2.KVector4.Scalar1345 - mv1.Scalar35 * mv2.KVector4.Scalar1245 + mv1.Scalar45 * mv2.KVector4.Scalar1235;
            tempScalar[23] += mv1.Scalar14 * mv2.KVector4.Scalar2345 - mv1.Scalar24 * mv2.KVector4.Scalar1345 + mv1.Scalar34 * mv2.KVector4.Scalar1245 - mv1.Scalar45 * mv2.KVector4.Scalar1234;
            tempScalar[27] += -mv1.Scalar13 * mv2.KVector4.Scalar2345 + mv1.Scalar23 * mv2.KVector4.Scalar1345 - mv1.Scalar34 * mv2.KVector4.Scalar1235 + mv1.Scalar35 * mv2.KVector4.Scalar1234;
            tempScalar[29] += mv1.Scalar12 * mv2.KVector4.Scalar2345 - mv1.Scalar23 * mv2.KVector4.Scalar1245 + mv1.Scalar24 * mv2.KVector4.Scalar1235 - mv1.Scalar25 * mv2.KVector4.Scalar1234;
            tempScalar[30] += mv1.Scalar12 * mv2.KVector4.Scalar1345 - mv1.Scalar13 * mv2.KVector4.Scalar1245 + mv1.Scalar14 * mv2.KVector4.Scalar1235 - mv1.Scalar15 * mv2.KVector4.Scalar1234;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector3 mv1, Ga41KVector0 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector4 Cp(this Ga41KVector3 mv1, Ga41KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector4.Zero;
        
        return new Ga41KVector4
        {
            Scalar1234 = mv1.Scalar123 * mv2.Scalar4 - mv1.Scalar124 * mv2.Scalar3 + mv1.Scalar134 * mv2.Scalar2 - mv1.Scalar234 * mv2.Scalar1,
            Scalar1235 = mv1.Scalar123 * mv2.Scalar5 - mv1.Scalar125 * mv2.Scalar3 + mv1.Scalar135 * mv2.Scalar2 - mv1.Scalar235 * mv2.Scalar1,
            Scalar1245 = mv1.Scalar124 * mv2.Scalar5 - mv1.Scalar125 * mv2.Scalar4 + mv1.Scalar145 * mv2.Scalar2 - mv1.Scalar245 * mv2.Scalar1,
            Scalar1345 = mv1.Scalar134 * mv2.Scalar5 - mv1.Scalar135 * mv2.Scalar4 + mv1.Scalar145 * mv2.Scalar3 - mv1.Scalar345 * mv2.Scalar1,
            Scalar2345 = mv1.Scalar234 * mv2.Scalar5 - mv1.Scalar235 * mv2.Scalar4 + mv1.Scalar245 * mv2.Scalar3 - mv1.Scalar345 * mv2.Scalar2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector3 Cp(this Ga41KVector3 mv1, Ga41KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector3.Zero;
        
        return new Ga41KVector3
        {
            Scalar123 = -mv1.Scalar124 * mv2.Scalar34 + mv1.Scalar134 * mv2.Scalar24 - mv1.Scalar234 * mv2.Scalar14 - mv1.Scalar125 * mv2.Scalar35 + mv1.Scalar135 * mv2.Scalar25 - mv1.Scalar235 * mv2.Scalar15,
            Scalar124 = mv1.Scalar123 * mv2.Scalar34 - mv1.Scalar134 * mv2.Scalar23 + mv1.Scalar234 * mv2.Scalar13 - mv1.Scalar125 * mv2.Scalar45 + mv1.Scalar145 * mv2.Scalar25 - mv1.Scalar245 * mv2.Scalar15,
            Scalar134 = -mv1.Scalar123 * mv2.Scalar24 + mv1.Scalar124 * mv2.Scalar23 - mv1.Scalar234 * mv2.Scalar12 - mv1.Scalar135 * mv2.Scalar45 + mv1.Scalar145 * mv2.Scalar35 - mv1.Scalar345 * mv2.Scalar15,
            Scalar234 = -mv1.Scalar123 * mv2.Scalar14 + mv1.Scalar124 * mv2.Scalar13 - mv1.Scalar134 * mv2.Scalar12 - mv1.Scalar235 * mv2.Scalar45 + mv1.Scalar245 * mv2.Scalar35 - mv1.Scalar345 * mv2.Scalar25,
            Scalar125 = mv1.Scalar123 * mv2.Scalar35 + mv1.Scalar124 * mv2.Scalar45 - mv1.Scalar135 * mv2.Scalar23 + mv1.Scalar235 * mv2.Scalar13 - mv1.Scalar145 * mv2.Scalar24 + mv1.Scalar245 * mv2.Scalar14,
            Scalar135 = -mv1.Scalar123 * mv2.Scalar25 + mv1.Scalar134 * mv2.Scalar45 + mv1.Scalar125 * mv2.Scalar23 - mv1.Scalar235 * mv2.Scalar12 - mv1.Scalar145 * mv2.Scalar34 + mv1.Scalar345 * mv2.Scalar14,
            Scalar235 = -mv1.Scalar123 * mv2.Scalar15 + mv1.Scalar234 * mv2.Scalar45 + mv1.Scalar125 * mv2.Scalar13 - mv1.Scalar135 * mv2.Scalar12 - mv1.Scalar245 * mv2.Scalar34 + mv1.Scalar345 * mv2.Scalar24,
            Scalar145 = -mv1.Scalar124 * mv2.Scalar25 - mv1.Scalar134 * mv2.Scalar35 + mv1.Scalar125 * mv2.Scalar24 + mv1.Scalar135 * mv2.Scalar34 - mv1.Scalar245 * mv2.Scalar12 - mv1.Scalar345 * mv2.Scalar13,
            Scalar245 = -mv1.Scalar124 * mv2.Scalar15 - mv1.Scalar234 * mv2.Scalar35 + mv1.Scalar125 * mv2.Scalar14 + mv1.Scalar235 * mv2.Scalar34 - mv1.Scalar145 * mv2.Scalar12 - mv1.Scalar345 * mv2.Scalar23,
            Scalar345 = -mv1.Scalar134 * mv2.Scalar15 + mv1.Scalar234 * mv2.Scalar25 + mv1.Scalar135 * mv2.Scalar14 - mv1.Scalar235 * mv2.Scalar24 - mv1.Scalar145 * mv2.Scalar13 + mv1.Scalar245 * mv2.Scalar23
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector2 Cp(this Ga41KVector3 mv1, Ga41KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector2.Zero;
        
        return new Ga41KVector2
        {
            Scalar12 = -mv1.Scalar134 * mv2.Scalar234 + mv1.Scalar234 * mv2.Scalar134 - mv1.Scalar135 * mv2.Scalar235 + mv1.Scalar235 * mv2.Scalar135 - mv1.Scalar145 * mv2.Scalar245 + mv1.Scalar245 * mv2.Scalar145,
            Scalar13 = mv1.Scalar124 * mv2.Scalar234 - mv1.Scalar234 * mv2.Scalar124 + mv1.Scalar125 * mv2.Scalar235 - mv1.Scalar235 * mv2.Scalar125 - mv1.Scalar145 * mv2.Scalar345 + mv1.Scalar345 * mv2.Scalar145,
            Scalar23 = mv1.Scalar124 * mv2.Scalar134 - mv1.Scalar134 * mv2.Scalar124 + mv1.Scalar125 * mv2.Scalar135 - mv1.Scalar135 * mv2.Scalar125 - mv1.Scalar245 * mv2.Scalar345 + mv1.Scalar345 * mv2.Scalar245,
            Scalar14 = -mv1.Scalar123 * mv2.Scalar234 + mv1.Scalar234 * mv2.Scalar123 + mv1.Scalar125 * mv2.Scalar245 + mv1.Scalar135 * mv2.Scalar345 - mv1.Scalar245 * mv2.Scalar125 - mv1.Scalar345 * mv2.Scalar135,
            Scalar24 = -mv1.Scalar123 * mv2.Scalar134 + mv1.Scalar134 * mv2.Scalar123 + mv1.Scalar125 * mv2.Scalar145 + mv1.Scalar235 * mv2.Scalar345 - mv1.Scalar145 * mv2.Scalar125 - mv1.Scalar345 * mv2.Scalar235,
            Scalar34 = mv1.Scalar123 * mv2.Scalar124 - mv1.Scalar124 * mv2.Scalar123 + mv1.Scalar135 * mv2.Scalar145 - mv1.Scalar235 * mv2.Scalar245 - mv1.Scalar145 * mv2.Scalar135 + mv1.Scalar245 * mv2.Scalar235,
            Scalar15 = -mv1.Scalar123 * mv2.Scalar235 - mv1.Scalar124 * mv2.Scalar245 - mv1.Scalar134 * mv2.Scalar345 + mv1.Scalar235 * mv2.Scalar123 + mv1.Scalar245 * mv2.Scalar124 + mv1.Scalar345 * mv2.Scalar134,
            Scalar25 = -mv1.Scalar123 * mv2.Scalar135 - mv1.Scalar124 * mv2.Scalar145 - mv1.Scalar234 * mv2.Scalar345 + mv1.Scalar135 * mv2.Scalar123 + mv1.Scalar145 * mv2.Scalar124 + mv1.Scalar345 * mv2.Scalar234,
            Scalar35 = mv1.Scalar123 * mv2.Scalar125 - mv1.Scalar134 * mv2.Scalar145 + mv1.Scalar234 * mv2.Scalar245 - mv1.Scalar125 * mv2.Scalar123 + mv1.Scalar145 * mv2.Scalar134 - mv1.Scalar245 * mv2.Scalar234,
            Scalar45 = mv1.Scalar124 * mv2.Scalar125 + mv1.Scalar134 * mv2.Scalar135 - mv1.Scalar234 * mv2.Scalar235 - mv1.Scalar125 * mv2.Scalar124 - mv1.Scalar135 * mv2.Scalar134 + mv1.Scalar235 * mv2.Scalar234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector1 Cp(this Ga41KVector3 mv1, Ga41KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector1.Zero;
        
        return new Ga41KVector1
        {
            Scalar1 = mv1.Scalar234 * mv2.Scalar1234 + mv1.Scalar235 * mv2.Scalar1235 + mv1.Scalar245 * mv2.Scalar1245 + mv1.Scalar345 * mv2.Scalar1345,
            Scalar2 = mv1.Scalar134 * mv2.Scalar1234 + mv1.Scalar135 * mv2.Scalar1235 + mv1.Scalar145 * mv2.Scalar1245 + mv1.Scalar345 * mv2.Scalar2345,
            Scalar3 = -mv1.Scalar124 * mv2.Scalar1234 - mv1.Scalar125 * mv2.Scalar1235 + mv1.Scalar145 * mv2.Scalar1345 - mv1.Scalar245 * mv2.Scalar2345,
            Scalar4 = mv1.Scalar123 * mv2.Scalar1234 - mv1.Scalar125 * mv2.Scalar1245 - mv1.Scalar135 * mv2.Scalar1345 + mv1.Scalar235 * mv2.Scalar2345,
            Scalar5 = mv1.Scalar123 * mv2.Scalar1235 + mv1.Scalar124 * mv2.Scalar1245 + mv1.Scalar134 * mv2.Scalar1345 - mv1.Scalar234 * mv2.Scalar2345
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector3 mv1, Ga41KVector5 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    public static Ga41Multivector Cp(this Ga41KVector3 mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[15] += mv1.Scalar123 * mv2.KVector1.Scalar4 - mv1.Scalar124 * mv2.KVector1.Scalar3 + mv1.Scalar134 * mv2.KVector1.Scalar2 - mv1.Scalar234 * mv2.KVector1.Scalar1;
            tempScalar[23] += mv1.Scalar123 * mv2.KVector1.Scalar5 - mv1.Scalar125 * mv2.KVector1.Scalar3 + mv1.Scalar135 * mv2.KVector1.Scalar2 - mv1.Scalar235 * mv2.KVector1.Scalar1;
            tempScalar[27] += mv1.Scalar124 * mv2.KVector1.Scalar5 - mv1.Scalar125 * mv2.KVector1.Scalar4 + mv1.Scalar145 * mv2.KVector1.Scalar2 - mv1.Scalar245 * mv2.KVector1.Scalar1;
            tempScalar[29] += mv1.Scalar134 * mv2.KVector1.Scalar5 - mv1.Scalar135 * mv2.KVector1.Scalar4 + mv1.Scalar145 * mv2.KVector1.Scalar3 - mv1.Scalar345 * mv2.KVector1.Scalar1;
            tempScalar[30] += mv1.Scalar234 * mv2.KVector1.Scalar5 - mv1.Scalar235 * mv2.KVector1.Scalar4 + mv1.Scalar245 * mv2.KVector1.Scalar3 - mv1.Scalar345 * mv2.KVector1.Scalar2;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[7] += -mv1.Scalar124 * mv2.KVector2.Scalar34 + mv1.Scalar134 * mv2.KVector2.Scalar24 - mv1.Scalar234 * mv2.KVector2.Scalar14 - mv1.Scalar125 * mv2.KVector2.Scalar35 + mv1.Scalar135 * mv2.KVector2.Scalar25 - mv1.Scalar235 * mv2.KVector2.Scalar15;
            tempScalar[11] += mv1.Scalar123 * mv2.KVector2.Scalar34 - mv1.Scalar134 * mv2.KVector2.Scalar23 + mv1.Scalar234 * mv2.KVector2.Scalar13 - mv1.Scalar125 * mv2.KVector2.Scalar45 + mv1.Scalar145 * mv2.KVector2.Scalar25 - mv1.Scalar245 * mv2.KVector2.Scalar15;
            tempScalar[13] += -mv1.Scalar123 * mv2.KVector2.Scalar24 + mv1.Scalar124 * mv2.KVector2.Scalar23 - mv1.Scalar234 * mv2.KVector2.Scalar12 - mv1.Scalar135 * mv2.KVector2.Scalar45 + mv1.Scalar145 * mv2.KVector2.Scalar35 - mv1.Scalar345 * mv2.KVector2.Scalar15;
            tempScalar[14] += -mv1.Scalar123 * mv2.KVector2.Scalar14 + mv1.Scalar124 * mv2.KVector2.Scalar13 - mv1.Scalar134 * mv2.KVector2.Scalar12 - mv1.Scalar235 * mv2.KVector2.Scalar45 + mv1.Scalar245 * mv2.KVector2.Scalar35 - mv1.Scalar345 * mv2.KVector2.Scalar25;
            tempScalar[19] += mv1.Scalar123 * mv2.KVector2.Scalar35 + mv1.Scalar124 * mv2.KVector2.Scalar45 - mv1.Scalar135 * mv2.KVector2.Scalar23 + mv1.Scalar235 * mv2.KVector2.Scalar13 - mv1.Scalar145 * mv2.KVector2.Scalar24 + mv1.Scalar245 * mv2.KVector2.Scalar14;
            tempScalar[21] += -mv1.Scalar123 * mv2.KVector2.Scalar25 + mv1.Scalar134 * mv2.KVector2.Scalar45 + mv1.Scalar125 * mv2.KVector2.Scalar23 - mv1.Scalar235 * mv2.KVector2.Scalar12 - mv1.Scalar145 * mv2.KVector2.Scalar34 + mv1.Scalar345 * mv2.KVector2.Scalar14;
            tempScalar[22] += -mv1.Scalar123 * mv2.KVector2.Scalar15 + mv1.Scalar234 * mv2.KVector2.Scalar45 + mv1.Scalar125 * mv2.KVector2.Scalar13 - mv1.Scalar135 * mv2.KVector2.Scalar12 - mv1.Scalar245 * mv2.KVector2.Scalar34 + mv1.Scalar345 * mv2.KVector2.Scalar24;
            tempScalar[25] += -mv1.Scalar124 * mv2.KVector2.Scalar25 - mv1.Scalar134 * mv2.KVector2.Scalar35 + mv1.Scalar125 * mv2.KVector2.Scalar24 + mv1.Scalar135 * mv2.KVector2.Scalar34 - mv1.Scalar245 * mv2.KVector2.Scalar12 - mv1.Scalar345 * mv2.KVector2.Scalar13;
            tempScalar[26] += -mv1.Scalar124 * mv2.KVector2.Scalar15 - mv1.Scalar234 * mv2.KVector2.Scalar35 + mv1.Scalar125 * mv2.KVector2.Scalar14 + mv1.Scalar235 * mv2.KVector2.Scalar34 - mv1.Scalar145 * mv2.KVector2.Scalar12 - mv1.Scalar345 * mv2.KVector2.Scalar23;
            tempScalar[28] += -mv1.Scalar134 * mv2.KVector2.Scalar15 + mv1.Scalar234 * mv2.KVector2.Scalar25 + mv1.Scalar135 * mv2.KVector2.Scalar14 - mv1.Scalar235 * mv2.KVector2.Scalar24 - mv1.Scalar145 * mv2.KVector2.Scalar13 + mv1.Scalar245 * mv2.KVector2.Scalar23;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[3] += -mv1.Scalar134 * mv2.KVector3.Scalar234 + mv1.Scalar234 * mv2.KVector3.Scalar134 - mv1.Scalar135 * mv2.KVector3.Scalar235 + mv1.Scalar235 * mv2.KVector3.Scalar135 - mv1.Scalar145 * mv2.KVector3.Scalar245 + mv1.Scalar245 * mv2.KVector3.Scalar145;
            tempScalar[5] += mv1.Scalar124 * mv2.KVector3.Scalar234 - mv1.Scalar234 * mv2.KVector3.Scalar124 + mv1.Scalar125 * mv2.KVector3.Scalar235 - mv1.Scalar235 * mv2.KVector3.Scalar125 - mv1.Scalar145 * mv2.KVector3.Scalar345 + mv1.Scalar345 * mv2.KVector3.Scalar145;
            tempScalar[6] += mv1.Scalar124 * mv2.KVector3.Scalar134 - mv1.Scalar134 * mv2.KVector3.Scalar124 + mv1.Scalar125 * mv2.KVector3.Scalar135 - mv1.Scalar135 * mv2.KVector3.Scalar125 - mv1.Scalar245 * mv2.KVector3.Scalar345 + mv1.Scalar345 * mv2.KVector3.Scalar245;
            tempScalar[9] += -mv1.Scalar123 * mv2.KVector3.Scalar234 + mv1.Scalar234 * mv2.KVector3.Scalar123 + mv1.Scalar125 * mv2.KVector3.Scalar245 + mv1.Scalar135 * mv2.KVector3.Scalar345 - mv1.Scalar245 * mv2.KVector3.Scalar125 - mv1.Scalar345 * mv2.KVector3.Scalar135;
            tempScalar[10] += -mv1.Scalar123 * mv2.KVector3.Scalar134 + mv1.Scalar134 * mv2.KVector3.Scalar123 + mv1.Scalar125 * mv2.KVector3.Scalar145 + mv1.Scalar235 * mv2.KVector3.Scalar345 - mv1.Scalar145 * mv2.KVector3.Scalar125 - mv1.Scalar345 * mv2.KVector3.Scalar235;
            tempScalar[12] += mv1.Scalar123 * mv2.KVector3.Scalar124 - mv1.Scalar124 * mv2.KVector3.Scalar123 + mv1.Scalar135 * mv2.KVector3.Scalar145 - mv1.Scalar235 * mv2.KVector3.Scalar245 - mv1.Scalar145 * mv2.KVector3.Scalar135 + mv1.Scalar245 * mv2.KVector3.Scalar235;
            tempScalar[17] += -mv1.Scalar123 * mv2.KVector3.Scalar235 - mv1.Scalar124 * mv2.KVector3.Scalar245 - mv1.Scalar134 * mv2.KVector3.Scalar345 + mv1.Scalar235 * mv2.KVector3.Scalar123 + mv1.Scalar245 * mv2.KVector3.Scalar124 + mv1.Scalar345 * mv2.KVector3.Scalar134;
            tempScalar[18] += -mv1.Scalar123 * mv2.KVector3.Scalar135 - mv1.Scalar124 * mv2.KVector3.Scalar145 - mv1.Scalar234 * mv2.KVector3.Scalar345 + mv1.Scalar135 * mv2.KVector3.Scalar123 + mv1.Scalar145 * mv2.KVector3.Scalar124 + mv1.Scalar345 * mv2.KVector3.Scalar234;
            tempScalar[20] += mv1.Scalar123 * mv2.KVector3.Scalar125 - mv1.Scalar134 * mv2.KVector3.Scalar145 + mv1.Scalar234 * mv2.KVector3.Scalar245 - mv1.Scalar125 * mv2.KVector3.Scalar123 + mv1.Scalar145 * mv2.KVector3.Scalar134 - mv1.Scalar245 * mv2.KVector3.Scalar234;
            tempScalar[24] += mv1.Scalar124 * mv2.KVector3.Scalar125 + mv1.Scalar134 * mv2.KVector3.Scalar135 - mv1.Scalar234 * mv2.KVector3.Scalar235 - mv1.Scalar125 * mv2.KVector3.Scalar124 - mv1.Scalar135 * mv2.KVector3.Scalar134 + mv1.Scalar235 * mv2.KVector3.Scalar234;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[1] += mv1.Scalar234 * mv2.KVector4.Scalar1234 + mv1.Scalar235 * mv2.KVector4.Scalar1235 + mv1.Scalar245 * mv2.KVector4.Scalar1245 + mv1.Scalar345 * mv2.KVector4.Scalar1345;
            tempScalar[2] += mv1.Scalar134 * mv2.KVector4.Scalar1234 + mv1.Scalar135 * mv2.KVector4.Scalar1235 + mv1.Scalar145 * mv2.KVector4.Scalar1245 + mv1.Scalar345 * mv2.KVector4.Scalar2345;
            tempScalar[4] += -mv1.Scalar124 * mv2.KVector4.Scalar1234 - mv1.Scalar125 * mv2.KVector4.Scalar1235 + mv1.Scalar145 * mv2.KVector4.Scalar1345 - mv1.Scalar245 * mv2.KVector4.Scalar2345;
            tempScalar[8] += mv1.Scalar123 * mv2.KVector4.Scalar1234 - mv1.Scalar125 * mv2.KVector4.Scalar1245 - mv1.Scalar135 * mv2.KVector4.Scalar1345 + mv1.Scalar235 * mv2.KVector4.Scalar2345;
            tempScalar[16] += mv1.Scalar123 * mv2.KVector4.Scalar1235 + mv1.Scalar124 * mv2.KVector4.Scalar1245 + mv1.Scalar134 * mv2.KVector4.Scalar1345 - mv1.Scalar234 * mv2.KVector4.Scalar2345;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector4 mv1, Ga41KVector0 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector3 Cp(this Ga41KVector4 mv1, Ga41KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector3.Zero;
        
        return new Ga41KVector3
        {
            Scalar123 = mv1.Scalar1234 * mv2.Scalar4 + mv1.Scalar1235 * mv2.Scalar5,
            Scalar124 = -mv1.Scalar1234 * mv2.Scalar3 + mv1.Scalar1245 * mv2.Scalar5,
            Scalar134 = mv1.Scalar1234 * mv2.Scalar2 + mv1.Scalar1345 * mv2.Scalar5,
            Scalar234 = mv1.Scalar1234 * mv2.Scalar1 + mv1.Scalar2345 * mv2.Scalar5,
            Scalar125 = -mv1.Scalar1235 * mv2.Scalar3 - mv1.Scalar1245 * mv2.Scalar4,
            Scalar135 = mv1.Scalar1235 * mv2.Scalar2 - mv1.Scalar1345 * mv2.Scalar4,
            Scalar235 = mv1.Scalar1235 * mv2.Scalar1 - mv1.Scalar2345 * mv2.Scalar4,
            Scalar145 = mv1.Scalar1245 * mv2.Scalar2 + mv1.Scalar1345 * mv2.Scalar3,
            Scalar245 = mv1.Scalar1245 * mv2.Scalar1 + mv1.Scalar2345 * mv2.Scalar3,
            Scalar345 = mv1.Scalar1345 * mv2.Scalar1 - mv1.Scalar2345 * mv2.Scalar2
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector4 Cp(this Ga41KVector4 mv1, Ga41KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector4.Zero;
        
        return new Ga41KVector4
        {
            Scalar1234 = -mv1.Scalar1235 * mv2.Scalar45 + mv1.Scalar1245 * mv2.Scalar35 - mv1.Scalar1345 * mv2.Scalar25 + mv1.Scalar2345 * mv2.Scalar15,
            Scalar1235 = mv1.Scalar1234 * mv2.Scalar45 - mv1.Scalar1245 * mv2.Scalar34 + mv1.Scalar1345 * mv2.Scalar24 - mv1.Scalar2345 * mv2.Scalar14,
            Scalar1245 = -mv1.Scalar1234 * mv2.Scalar35 + mv1.Scalar1235 * mv2.Scalar34 - mv1.Scalar1345 * mv2.Scalar23 + mv1.Scalar2345 * mv2.Scalar13,
            Scalar1345 = mv1.Scalar1234 * mv2.Scalar25 - mv1.Scalar1235 * mv2.Scalar24 + mv1.Scalar1245 * mv2.Scalar23 - mv1.Scalar2345 * mv2.Scalar12,
            Scalar2345 = mv1.Scalar1234 * mv2.Scalar15 - mv1.Scalar1235 * mv2.Scalar14 + mv1.Scalar1245 * mv2.Scalar13 - mv1.Scalar1345 * mv2.Scalar12
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector1 Cp(this Ga41KVector4 mv1, Ga41KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector1.Zero;
        
        return new Ga41KVector1
        {
            Scalar1 = -mv1.Scalar1234 * mv2.Scalar234 - mv1.Scalar1235 * mv2.Scalar235 - mv1.Scalar1245 * mv2.Scalar245 - mv1.Scalar1345 * mv2.Scalar345,
            Scalar2 = -mv1.Scalar1234 * mv2.Scalar134 - mv1.Scalar1235 * mv2.Scalar135 - mv1.Scalar1245 * mv2.Scalar145 - mv1.Scalar2345 * mv2.Scalar345,
            Scalar3 = mv1.Scalar1234 * mv2.Scalar124 + mv1.Scalar1235 * mv2.Scalar125 - mv1.Scalar1345 * mv2.Scalar145 + mv1.Scalar2345 * mv2.Scalar245,
            Scalar4 = -mv1.Scalar1234 * mv2.Scalar123 + mv1.Scalar1245 * mv2.Scalar125 + mv1.Scalar1345 * mv2.Scalar135 - mv1.Scalar2345 * mv2.Scalar235,
            Scalar5 = -mv1.Scalar1235 * mv2.Scalar123 - mv1.Scalar1245 * mv2.Scalar124 - mv1.Scalar1345 * mv2.Scalar134 + mv1.Scalar2345 * mv2.Scalar234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector2 Cp(this Ga41KVector4 mv1, Ga41KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41KVector2.Zero;
        
        return new Ga41KVector2
        {
            Scalar12 = mv1.Scalar1345 * mv2.Scalar2345 - mv1.Scalar2345 * mv2.Scalar1345,
            Scalar13 = -mv1.Scalar1245 * mv2.Scalar2345 + mv1.Scalar2345 * mv2.Scalar1245,
            Scalar23 = -mv1.Scalar1245 * mv2.Scalar1345 + mv1.Scalar1345 * mv2.Scalar1245,
            Scalar14 = mv1.Scalar1235 * mv2.Scalar2345 - mv1.Scalar2345 * mv2.Scalar1235,
            Scalar24 = mv1.Scalar1235 * mv2.Scalar1345 - mv1.Scalar1345 * mv2.Scalar1235,
            Scalar34 = -mv1.Scalar1235 * mv2.Scalar1245 + mv1.Scalar1245 * mv2.Scalar1235,
            Scalar15 = -mv1.Scalar1234 * mv2.Scalar2345 + mv1.Scalar2345 * mv2.Scalar1234,
            Scalar25 = -mv1.Scalar1234 * mv2.Scalar1345 + mv1.Scalar1345 * mv2.Scalar1234,
            Scalar35 = mv1.Scalar1234 * mv2.Scalar1245 - mv1.Scalar1245 * mv2.Scalar1234,
            Scalar45 = -mv1.Scalar1234 * mv2.Scalar1235 + mv1.Scalar1235 * mv2.Scalar1234
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector4 mv1, Ga41KVector5 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    public static Ga41Multivector Cp(this Ga41KVector4 mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv2.KVector1.IsZero())
        {
            tempScalar[7] += mv1.Scalar1234 * mv2.KVector1.Scalar4 + mv1.Scalar1235 * mv2.KVector1.Scalar5;
            tempScalar[11] += -mv1.Scalar1234 * mv2.KVector1.Scalar3 + mv1.Scalar1245 * mv2.KVector1.Scalar5;
            tempScalar[13] += mv1.Scalar1234 * mv2.KVector1.Scalar2 + mv1.Scalar1345 * mv2.KVector1.Scalar5;
            tempScalar[14] += mv1.Scalar1234 * mv2.KVector1.Scalar1 + mv1.Scalar2345 * mv2.KVector1.Scalar5;
            tempScalar[19] += -mv1.Scalar1235 * mv2.KVector1.Scalar3 - mv1.Scalar1245 * mv2.KVector1.Scalar4;
            tempScalar[21] += mv1.Scalar1235 * mv2.KVector1.Scalar2 - mv1.Scalar1345 * mv2.KVector1.Scalar4;
            tempScalar[22] += mv1.Scalar1235 * mv2.KVector1.Scalar1 - mv1.Scalar2345 * mv2.KVector1.Scalar4;
            tempScalar[25] += mv1.Scalar1245 * mv2.KVector1.Scalar2 + mv1.Scalar1345 * mv2.KVector1.Scalar3;
            tempScalar[26] += mv1.Scalar1245 * mv2.KVector1.Scalar1 + mv1.Scalar2345 * mv2.KVector1.Scalar3;
            tempScalar[28] += mv1.Scalar1345 * mv2.KVector1.Scalar1 - mv1.Scalar2345 * mv2.KVector1.Scalar2;
        }
        
        if (!mv2.KVector2.IsZero())
        {
            tempScalar[15] += -mv1.Scalar1235 * mv2.KVector2.Scalar45 + mv1.Scalar1245 * mv2.KVector2.Scalar35 - mv1.Scalar1345 * mv2.KVector2.Scalar25 + mv1.Scalar2345 * mv2.KVector2.Scalar15;
            tempScalar[23] += mv1.Scalar1234 * mv2.KVector2.Scalar45 - mv1.Scalar1245 * mv2.KVector2.Scalar34 + mv1.Scalar1345 * mv2.KVector2.Scalar24 - mv1.Scalar2345 * mv2.KVector2.Scalar14;
            tempScalar[27] += -mv1.Scalar1234 * mv2.KVector2.Scalar35 + mv1.Scalar1235 * mv2.KVector2.Scalar34 - mv1.Scalar1345 * mv2.KVector2.Scalar23 + mv1.Scalar2345 * mv2.KVector2.Scalar13;
            tempScalar[29] += mv1.Scalar1234 * mv2.KVector2.Scalar25 - mv1.Scalar1235 * mv2.KVector2.Scalar24 + mv1.Scalar1245 * mv2.KVector2.Scalar23 - mv1.Scalar2345 * mv2.KVector2.Scalar12;
            tempScalar[30] += mv1.Scalar1234 * mv2.KVector2.Scalar15 - mv1.Scalar1235 * mv2.KVector2.Scalar14 + mv1.Scalar1245 * mv2.KVector2.Scalar13 - mv1.Scalar1345 * mv2.KVector2.Scalar12;
        }
        
        if (!mv2.KVector3.IsZero())
        {
            tempScalar[1] += -mv1.Scalar1234 * mv2.KVector3.Scalar234 - mv1.Scalar1235 * mv2.KVector3.Scalar235 - mv1.Scalar1245 * mv2.KVector3.Scalar245 - mv1.Scalar1345 * mv2.KVector3.Scalar345;
            tempScalar[2] += -mv1.Scalar1234 * mv2.KVector3.Scalar134 - mv1.Scalar1235 * mv2.KVector3.Scalar135 - mv1.Scalar1245 * mv2.KVector3.Scalar145 - mv1.Scalar2345 * mv2.KVector3.Scalar345;
            tempScalar[4] += mv1.Scalar1234 * mv2.KVector3.Scalar124 + mv1.Scalar1235 * mv2.KVector3.Scalar125 - mv1.Scalar1345 * mv2.KVector3.Scalar145 + mv1.Scalar2345 * mv2.KVector3.Scalar245;
            tempScalar[8] += -mv1.Scalar1234 * mv2.KVector3.Scalar123 + mv1.Scalar1245 * mv2.KVector3.Scalar125 + mv1.Scalar1345 * mv2.KVector3.Scalar135 - mv1.Scalar2345 * mv2.KVector3.Scalar235;
            tempScalar[16] += -mv1.Scalar1235 * mv2.KVector3.Scalar123 - mv1.Scalar1245 * mv2.KVector3.Scalar124 - mv1.Scalar1345 * mv2.KVector3.Scalar134 + mv1.Scalar2345 * mv2.KVector3.Scalar234;
        }
        
        if (!mv2.KVector4.IsZero())
        {
            tempScalar[3] += mv1.Scalar1345 * mv2.KVector4.Scalar2345 - mv1.Scalar2345 * mv2.KVector4.Scalar1345;
            tempScalar[5] += -mv1.Scalar1245 * mv2.KVector4.Scalar2345 + mv1.Scalar2345 * mv2.KVector4.Scalar1245;
            tempScalar[6] += -mv1.Scalar1245 * mv2.KVector4.Scalar1345 + mv1.Scalar1345 * mv2.KVector4.Scalar1245;
            tempScalar[9] += mv1.Scalar1235 * mv2.KVector4.Scalar2345 - mv1.Scalar2345 * mv2.KVector4.Scalar1235;
            tempScalar[10] += mv1.Scalar1235 * mv2.KVector4.Scalar1345 - mv1.Scalar1345 * mv2.KVector4.Scalar1235;
            tempScalar[12] += -mv1.Scalar1235 * mv2.KVector4.Scalar1245 + mv1.Scalar1245 * mv2.KVector4.Scalar1235;
            tempScalar[17] += -mv1.Scalar1234 * mv2.KVector4.Scalar2345 + mv1.Scalar2345 * mv2.KVector4.Scalar1234;
            tempScalar[18] += -mv1.Scalar1234 * mv2.KVector4.Scalar1345 + mv1.Scalar1345 * mv2.KVector4.Scalar1234;
            tempScalar[20] += mv1.Scalar1234 * mv2.KVector4.Scalar1245 - mv1.Scalar1245 * mv2.KVector4.Scalar1234;
            tempScalar[24] += -mv1.Scalar1234 * mv2.KVector4.Scalar1235 + mv1.Scalar1235 * mv2.KVector4.Scalar1234;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector5 mv1, Ga41KVector0 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector5 mv1, Ga41KVector1 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector5 mv1, Ga41KVector2 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector5 mv1, Ga41KVector3 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector5 mv1, Ga41KVector4 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector5 mv1, Ga41KVector5 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41KVector5 mv1, Ga41Multivector mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41Multivector mv1, Ga41KVector0 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    public static Ga41Multivector Cp(this Ga41Multivector mv1, Ga41KVector1 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
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
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[1] += mv1.KVector2.Scalar12 * mv2.Scalar2 + mv1.KVector2.Scalar13 * mv2.Scalar3 + mv1.KVector2.Scalar14 * mv2.Scalar4 + mv1.KVector2.Scalar15 * mv2.Scalar5;
            tempScalar[2] += mv1.KVector2.Scalar12 * mv2.Scalar1 + mv1.KVector2.Scalar23 * mv2.Scalar3 + mv1.KVector2.Scalar24 * mv2.Scalar4 + mv1.KVector2.Scalar25 * mv2.Scalar5;
            tempScalar[4] += mv1.KVector2.Scalar13 * mv2.Scalar1 - mv1.KVector2.Scalar23 * mv2.Scalar2 + mv1.KVector2.Scalar34 * mv2.Scalar4 + mv1.KVector2.Scalar35 * mv2.Scalar5;
            tempScalar[8] += mv1.KVector2.Scalar14 * mv2.Scalar1 - mv1.KVector2.Scalar24 * mv2.Scalar2 - mv1.KVector2.Scalar34 * mv2.Scalar3 + mv1.KVector2.Scalar45 * mv2.Scalar5;
            tempScalar[16] += mv1.KVector2.Scalar15 * mv2.Scalar1 - mv1.KVector2.Scalar25 * mv2.Scalar2 - mv1.KVector2.Scalar35 * mv2.Scalar3 - mv1.KVector2.Scalar45 * mv2.Scalar4;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[15] += mv1.KVector3.Scalar123 * mv2.Scalar4 - mv1.KVector3.Scalar124 * mv2.Scalar3 + mv1.KVector3.Scalar134 * mv2.Scalar2 - mv1.KVector3.Scalar234 * mv2.Scalar1;
            tempScalar[23] += mv1.KVector3.Scalar123 * mv2.Scalar5 - mv1.KVector3.Scalar125 * mv2.Scalar3 + mv1.KVector3.Scalar135 * mv2.Scalar2 - mv1.KVector3.Scalar235 * mv2.Scalar1;
            tempScalar[27] += mv1.KVector3.Scalar124 * mv2.Scalar5 - mv1.KVector3.Scalar125 * mv2.Scalar4 + mv1.KVector3.Scalar145 * mv2.Scalar2 - mv1.KVector3.Scalar245 * mv2.Scalar1;
            tempScalar[29] += mv1.KVector3.Scalar134 * mv2.Scalar5 - mv1.KVector3.Scalar135 * mv2.Scalar4 + mv1.KVector3.Scalar145 * mv2.Scalar3 - mv1.KVector3.Scalar345 * mv2.Scalar1;
            tempScalar[30] += mv1.KVector3.Scalar234 * mv2.Scalar5 - mv1.KVector3.Scalar235 * mv2.Scalar4 + mv1.KVector3.Scalar245 * mv2.Scalar3 - mv1.KVector3.Scalar345 * mv2.Scalar2;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[7] += mv1.KVector4.Scalar1234 * mv2.Scalar4 + mv1.KVector4.Scalar1235 * mv2.Scalar5;
            tempScalar[11] += -mv1.KVector4.Scalar1234 * mv2.Scalar3 + mv1.KVector4.Scalar1245 * mv2.Scalar5;
            tempScalar[13] += mv1.KVector4.Scalar1234 * mv2.Scalar2 + mv1.KVector4.Scalar1345 * mv2.Scalar5;
            tempScalar[14] += mv1.KVector4.Scalar1234 * mv2.Scalar1 + mv1.KVector4.Scalar2345 * mv2.Scalar5;
            tempScalar[19] += -mv1.KVector4.Scalar1235 * mv2.Scalar3 - mv1.KVector4.Scalar1245 * mv2.Scalar4;
            tempScalar[21] += mv1.KVector4.Scalar1235 * mv2.Scalar2 - mv1.KVector4.Scalar1345 * mv2.Scalar4;
            tempScalar[22] += mv1.KVector4.Scalar1235 * mv2.Scalar1 - mv1.KVector4.Scalar2345 * mv2.Scalar4;
            tempScalar[25] += mv1.KVector4.Scalar1245 * mv2.Scalar2 + mv1.KVector4.Scalar1345 * mv2.Scalar3;
            tempScalar[26] += mv1.KVector4.Scalar1245 * mv2.Scalar1 + mv1.KVector4.Scalar2345 * mv2.Scalar3;
            tempScalar[28] += mv1.KVector4.Scalar1345 * mv2.Scalar1 - mv1.KVector4.Scalar2345 * mv2.Scalar2;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    public static Ga41Multivector Cp(this Ga41Multivector mv1, Ga41KVector2 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[1] += -mv1.KVector1.Scalar2 * mv2.Scalar12 - mv1.KVector1.Scalar3 * mv2.Scalar13 - mv1.KVector1.Scalar4 * mv2.Scalar14 - mv1.KVector1.Scalar5 * mv2.Scalar15;
            tempScalar[2] += -mv1.KVector1.Scalar1 * mv2.Scalar12 - mv1.KVector1.Scalar3 * mv2.Scalar23 - mv1.KVector1.Scalar4 * mv2.Scalar24 - mv1.KVector1.Scalar5 * mv2.Scalar25;
            tempScalar[4] += -mv1.KVector1.Scalar1 * mv2.Scalar13 + mv1.KVector1.Scalar2 * mv2.Scalar23 - mv1.KVector1.Scalar4 * mv2.Scalar34 - mv1.KVector1.Scalar5 * mv2.Scalar35;
            tempScalar[8] += -mv1.KVector1.Scalar1 * mv2.Scalar14 + mv1.KVector1.Scalar2 * mv2.Scalar24 + mv1.KVector1.Scalar3 * mv2.Scalar34 - mv1.KVector1.Scalar5 * mv2.Scalar45;
            tempScalar[16] += -mv1.KVector1.Scalar1 * mv2.Scalar15 + mv1.KVector1.Scalar2 * mv2.Scalar25 + mv1.KVector1.Scalar3 * mv2.Scalar35 + mv1.KVector1.Scalar4 * mv2.Scalar45;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[3] += -mv1.KVector2.Scalar13 * mv2.Scalar23 + mv1.KVector2.Scalar23 * mv2.Scalar13 - mv1.KVector2.Scalar14 * mv2.Scalar24 + mv1.KVector2.Scalar24 * mv2.Scalar14 - mv1.KVector2.Scalar15 * mv2.Scalar25 + mv1.KVector2.Scalar25 * mv2.Scalar15;
            tempScalar[5] += mv1.KVector2.Scalar12 * mv2.Scalar23 - mv1.KVector2.Scalar23 * mv2.Scalar12 - mv1.KVector2.Scalar14 * mv2.Scalar34 + mv1.KVector2.Scalar34 * mv2.Scalar14 - mv1.KVector2.Scalar15 * mv2.Scalar35 + mv1.KVector2.Scalar35 * mv2.Scalar15;
            tempScalar[6] += mv1.KVector2.Scalar12 * mv2.Scalar13 - mv1.KVector2.Scalar13 * mv2.Scalar12 - mv1.KVector2.Scalar24 * mv2.Scalar34 + mv1.KVector2.Scalar34 * mv2.Scalar24 - mv1.KVector2.Scalar25 * mv2.Scalar35 + mv1.KVector2.Scalar35 * mv2.Scalar25;
            tempScalar[9] += mv1.KVector2.Scalar12 * mv2.Scalar24 + mv1.KVector2.Scalar13 * mv2.Scalar34 - mv1.KVector2.Scalar24 * mv2.Scalar12 - mv1.KVector2.Scalar34 * mv2.Scalar13 - mv1.KVector2.Scalar15 * mv2.Scalar45 + mv1.KVector2.Scalar45 * mv2.Scalar15;
            tempScalar[10] += mv1.KVector2.Scalar12 * mv2.Scalar14 + mv1.KVector2.Scalar23 * mv2.Scalar34 - mv1.KVector2.Scalar14 * mv2.Scalar12 - mv1.KVector2.Scalar34 * mv2.Scalar23 - mv1.KVector2.Scalar25 * mv2.Scalar45 + mv1.KVector2.Scalar45 * mv2.Scalar25;
            tempScalar[12] += mv1.KVector2.Scalar13 * mv2.Scalar14 - mv1.KVector2.Scalar23 * mv2.Scalar24 - mv1.KVector2.Scalar14 * mv2.Scalar13 + mv1.KVector2.Scalar24 * mv2.Scalar23 - mv1.KVector2.Scalar35 * mv2.Scalar45 + mv1.KVector2.Scalar45 * mv2.Scalar35;
            tempScalar[17] += mv1.KVector2.Scalar12 * mv2.Scalar25 + mv1.KVector2.Scalar13 * mv2.Scalar35 + mv1.KVector2.Scalar14 * mv2.Scalar45 - mv1.KVector2.Scalar25 * mv2.Scalar12 - mv1.KVector2.Scalar35 * mv2.Scalar13 - mv1.KVector2.Scalar45 * mv2.Scalar14;
            tempScalar[18] += mv1.KVector2.Scalar12 * mv2.Scalar15 + mv1.KVector2.Scalar23 * mv2.Scalar35 + mv1.KVector2.Scalar24 * mv2.Scalar45 - mv1.KVector2.Scalar15 * mv2.Scalar12 - mv1.KVector2.Scalar35 * mv2.Scalar23 - mv1.KVector2.Scalar45 * mv2.Scalar24;
            tempScalar[20] += mv1.KVector2.Scalar13 * mv2.Scalar15 - mv1.KVector2.Scalar23 * mv2.Scalar25 + mv1.KVector2.Scalar34 * mv2.Scalar45 - mv1.KVector2.Scalar15 * mv2.Scalar13 + mv1.KVector2.Scalar25 * mv2.Scalar23 - mv1.KVector2.Scalar45 * mv2.Scalar34;
            tempScalar[24] += mv1.KVector2.Scalar14 * mv2.Scalar15 - mv1.KVector2.Scalar24 * mv2.Scalar25 - mv1.KVector2.Scalar34 * mv2.Scalar35 - mv1.KVector2.Scalar15 * mv2.Scalar14 + mv1.KVector2.Scalar25 * mv2.Scalar24 + mv1.KVector2.Scalar35 * mv2.Scalar34;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[7] += -mv1.KVector3.Scalar124 * mv2.Scalar34 + mv1.KVector3.Scalar134 * mv2.Scalar24 - mv1.KVector3.Scalar234 * mv2.Scalar14 - mv1.KVector3.Scalar125 * mv2.Scalar35 + mv1.KVector3.Scalar135 * mv2.Scalar25 - mv1.KVector3.Scalar235 * mv2.Scalar15;
            tempScalar[11] += mv1.KVector3.Scalar123 * mv2.Scalar34 - mv1.KVector3.Scalar134 * mv2.Scalar23 + mv1.KVector3.Scalar234 * mv2.Scalar13 - mv1.KVector3.Scalar125 * mv2.Scalar45 + mv1.KVector3.Scalar145 * mv2.Scalar25 - mv1.KVector3.Scalar245 * mv2.Scalar15;
            tempScalar[13] += -mv1.KVector3.Scalar123 * mv2.Scalar24 + mv1.KVector3.Scalar124 * mv2.Scalar23 - mv1.KVector3.Scalar234 * mv2.Scalar12 - mv1.KVector3.Scalar135 * mv2.Scalar45 + mv1.KVector3.Scalar145 * mv2.Scalar35 - mv1.KVector3.Scalar345 * mv2.Scalar15;
            tempScalar[14] += -mv1.KVector3.Scalar123 * mv2.Scalar14 + mv1.KVector3.Scalar124 * mv2.Scalar13 - mv1.KVector3.Scalar134 * mv2.Scalar12 - mv1.KVector3.Scalar235 * mv2.Scalar45 + mv1.KVector3.Scalar245 * mv2.Scalar35 - mv1.KVector3.Scalar345 * mv2.Scalar25;
            tempScalar[19] += mv1.KVector3.Scalar123 * mv2.Scalar35 + mv1.KVector3.Scalar124 * mv2.Scalar45 - mv1.KVector3.Scalar135 * mv2.Scalar23 + mv1.KVector3.Scalar235 * mv2.Scalar13 - mv1.KVector3.Scalar145 * mv2.Scalar24 + mv1.KVector3.Scalar245 * mv2.Scalar14;
            tempScalar[21] += -mv1.KVector3.Scalar123 * mv2.Scalar25 + mv1.KVector3.Scalar134 * mv2.Scalar45 + mv1.KVector3.Scalar125 * mv2.Scalar23 - mv1.KVector3.Scalar235 * mv2.Scalar12 - mv1.KVector3.Scalar145 * mv2.Scalar34 + mv1.KVector3.Scalar345 * mv2.Scalar14;
            tempScalar[22] += -mv1.KVector3.Scalar123 * mv2.Scalar15 + mv1.KVector3.Scalar234 * mv2.Scalar45 + mv1.KVector3.Scalar125 * mv2.Scalar13 - mv1.KVector3.Scalar135 * mv2.Scalar12 - mv1.KVector3.Scalar245 * mv2.Scalar34 + mv1.KVector3.Scalar345 * mv2.Scalar24;
            tempScalar[25] += -mv1.KVector3.Scalar124 * mv2.Scalar25 - mv1.KVector3.Scalar134 * mv2.Scalar35 + mv1.KVector3.Scalar125 * mv2.Scalar24 + mv1.KVector3.Scalar135 * mv2.Scalar34 - mv1.KVector3.Scalar245 * mv2.Scalar12 - mv1.KVector3.Scalar345 * mv2.Scalar13;
            tempScalar[26] += -mv1.KVector3.Scalar124 * mv2.Scalar15 - mv1.KVector3.Scalar234 * mv2.Scalar35 + mv1.KVector3.Scalar125 * mv2.Scalar14 + mv1.KVector3.Scalar235 * mv2.Scalar34 - mv1.KVector3.Scalar145 * mv2.Scalar12 - mv1.KVector3.Scalar345 * mv2.Scalar23;
            tempScalar[28] += -mv1.KVector3.Scalar134 * mv2.Scalar15 + mv1.KVector3.Scalar234 * mv2.Scalar25 + mv1.KVector3.Scalar135 * mv2.Scalar14 - mv1.KVector3.Scalar235 * mv2.Scalar24 - mv1.KVector3.Scalar145 * mv2.Scalar13 + mv1.KVector3.Scalar245 * mv2.Scalar23;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[15] += -mv1.KVector4.Scalar1235 * mv2.Scalar45 + mv1.KVector4.Scalar1245 * mv2.Scalar35 - mv1.KVector4.Scalar1345 * mv2.Scalar25 + mv1.KVector4.Scalar2345 * mv2.Scalar15;
            tempScalar[23] += mv1.KVector4.Scalar1234 * mv2.Scalar45 - mv1.KVector4.Scalar1245 * mv2.Scalar34 + mv1.KVector4.Scalar1345 * mv2.Scalar24 - mv1.KVector4.Scalar2345 * mv2.Scalar14;
            tempScalar[27] += -mv1.KVector4.Scalar1234 * mv2.Scalar35 + mv1.KVector4.Scalar1235 * mv2.Scalar34 - mv1.KVector4.Scalar1345 * mv2.Scalar23 + mv1.KVector4.Scalar2345 * mv2.Scalar13;
            tempScalar[29] += mv1.KVector4.Scalar1234 * mv2.Scalar25 - mv1.KVector4.Scalar1235 * mv2.Scalar24 + mv1.KVector4.Scalar1245 * mv2.Scalar23 - mv1.KVector4.Scalar2345 * mv2.Scalar12;
            tempScalar[30] += mv1.KVector4.Scalar1234 * mv2.Scalar15 - mv1.KVector4.Scalar1235 * mv2.Scalar14 + mv1.KVector4.Scalar1245 * mv2.Scalar13 - mv1.KVector4.Scalar1345 * mv2.Scalar12;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    public static Ga41Multivector Cp(this Ga41Multivector mv1, Ga41KVector3 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[15] += mv1.KVector1.Scalar1 * mv2.Scalar234 - mv1.KVector1.Scalar2 * mv2.Scalar134 + mv1.KVector1.Scalar3 * mv2.Scalar124 - mv1.KVector1.Scalar4 * mv2.Scalar123;
            tempScalar[23] += mv1.KVector1.Scalar1 * mv2.Scalar235 - mv1.KVector1.Scalar2 * mv2.Scalar135 + mv1.KVector1.Scalar3 * mv2.Scalar125 - mv1.KVector1.Scalar5 * mv2.Scalar123;
            tempScalar[27] += mv1.KVector1.Scalar1 * mv2.Scalar245 - mv1.KVector1.Scalar2 * mv2.Scalar145 + mv1.KVector1.Scalar4 * mv2.Scalar125 - mv1.KVector1.Scalar5 * mv2.Scalar124;
            tempScalar[29] += mv1.KVector1.Scalar1 * mv2.Scalar345 - mv1.KVector1.Scalar3 * mv2.Scalar145 + mv1.KVector1.Scalar4 * mv2.Scalar135 - mv1.KVector1.Scalar5 * mv2.Scalar134;
            tempScalar[30] += mv1.KVector1.Scalar2 * mv2.Scalar345 - mv1.KVector1.Scalar3 * mv2.Scalar245 + mv1.KVector1.Scalar4 * mv2.Scalar235 - mv1.KVector1.Scalar5 * mv2.Scalar234;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[7] += mv1.KVector2.Scalar14 * mv2.Scalar234 - mv1.KVector2.Scalar24 * mv2.Scalar134 + mv1.KVector2.Scalar34 * mv2.Scalar124 + mv1.KVector2.Scalar15 * mv2.Scalar235 - mv1.KVector2.Scalar25 * mv2.Scalar135 + mv1.KVector2.Scalar35 * mv2.Scalar125;
            tempScalar[11] += -mv1.KVector2.Scalar13 * mv2.Scalar234 + mv1.KVector2.Scalar23 * mv2.Scalar134 - mv1.KVector2.Scalar34 * mv2.Scalar123 + mv1.KVector2.Scalar15 * mv2.Scalar245 - mv1.KVector2.Scalar25 * mv2.Scalar145 + mv1.KVector2.Scalar45 * mv2.Scalar125;
            tempScalar[13] += mv1.KVector2.Scalar12 * mv2.Scalar234 - mv1.KVector2.Scalar23 * mv2.Scalar124 + mv1.KVector2.Scalar24 * mv2.Scalar123 + mv1.KVector2.Scalar15 * mv2.Scalar345 - mv1.KVector2.Scalar35 * mv2.Scalar145 + mv1.KVector2.Scalar45 * mv2.Scalar135;
            tempScalar[14] += mv1.KVector2.Scalar12 * mv2.Scalar134 - mv1.KVector2.Scalar13 * mv2.Scalar124 + mv1.KVector2.Scalar14 * mv2.Scalar123 + mv1.KVector2.Scalar25 * mv2.Scalar345 - mv1.KVector2.Scalar35 * mv2.Scalar245 + mv1.KVector2.Scalar45 * mv2.Scalar235;
            tempScalar[19] += -mv1.KVector2.Scalar13 * mv2.Scalar235 + mv1.KVector2.Scalar23 * mv2.Scalar135 - mv1.KVector2.Scalar14 * mv2.Scalar245 + mv1.KVector2.Scalar24 * mv2.Scalar145 - mv1.KVector2.Scalar35 * mv2.Scalar123 - mv1.KVector2.Scalar45 * mv2.Scalar124;
            tempScalar[21] += mv1.KVector2.Scalar12 * mv2.Scalar235 - mv1.KVector2.Scalar23 * mv2.Scalar125 - mv1.KVector2.Scalar14 * mv2.Scalar345 + mv1.KVector2.Scalar34 * mv2.Scalar145 + mv1.KVector2.Scalar25 * mv2.Scalar123 - mv1.KVector2.Scalar45 * mv2.Scalar134;
            tempScalar[22] += mv1.KVector2.Scalar12 * mv2.Scalar135 - mv1.KVector2.Scalar13 * mv2.Scalar125 - mv1.KVector2.Scalar24 * mv2.Scalar345 + mv1.KVector2.Scalar34 * mv2.Scalar245 + mv1.KVector2.Scalar15 * mv2.Scalar123 - mv1.KVector2.Scalar45 * mv2.Scalar234;
            tempScalar[25] += mv1.KVector2.Scalar12 * mv2.Scalar245 + mv1.KVector2.Scalar13 * mv2.Scalar345 - mv1.KVector2.Scalar24 * mv2.Scalar125 - mv1.KVector2.Scalar34 * mv2.Scalar135 + mv1.KVector2.Scalar25 * mv2.Scalar124 + mv1.KVector2.Scalar35 * mv2.Scalar134;
            tempScalar[26] += mv1.KVector2.Scalar12 * mv2.Scalar145 + mv1.KVector2.Scalar23 * mv2.Scalar345 - mv1.KVector2.Scalar14 * mv2.Scalar125 - mv1.KVector2.Scalar34 * mv2.Scalar235 + mv1.KVector2.Scalar15 * mv2.Scalar124 + mv1.KVector2.Scalar35 * mv2.Scalar234;
            tempScalar[28] += mv1.KVector2.Scalar13 * mv2.Scalar145 - mv1.KVector2.Scalar23 * mv2.Scalar245 - mv1.KVector2.Scalar14 * mv2.Scalar135 + mv1.KVector2.Scalar24 * mv2.Scalar235 + mv1.KVector2.Scalar15 * mv2.Scalar134 - mv1.KVector2.Scalar25 * mv2.Scalar234;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[3] += -mv1.KVector3.Scalar134 * mv2.Scalar234 + mv1.KVector3.Scalar234 * mv2.Scalar134 - mv1.KVector3.Scalar135 * mv2.Scalar235 + mv1.KVector3.Scalar235 * mv2.Scalar135 - mv1.KVector3.Scalar145 * mv2.Scalar245 + mv1.KVector3.Scalar245 * mv2.Scalar145;
            tempScalar[5] += mv1.KVector3.Scalar124 * mv2.Scalar234 - mv1.KVector3.Scalar234 * mv2.Scalar124 + mv1.KVector3.Scalar125 * mv2.Scalar235 - mv1.KVector3.Scalar235 * mv2.Scalar125 - mv1.KVector3.Scalar145 * mv2.Scalar345 + mv1.KVector3.Scalar345 * mv2.Scalar145;
            tempScalar[6] += mv1.KVector3.Scalar124 * mv2.Scalar134 - mv1.KVector3.Scalar134 * mv2.Scalar124 + mv1.KVector3.Scalar125 * mv2.Scalar135 - mv1.KVector3.Scalar135 * mv2.Scalar125 - mv1.KVector3.Scalar245 * mv2.Scalar345 + mv1.KVector3.Scalar345 * mv2.Scalar245;
            tempScalar[9] += -mv1.KVector3.Scalar123 * mv2.Scalar234 + mv1.KVector3.Scalar234 * mv2.Scalar123 + mv1.KVector3.Scalar125 * mv2.Scalar245 + mv1.KVector3.Scalar135 * mv2.Scalar345 - mv1.KVector3.Scalar245 * mv2.Scalar125 - mv1.KVector3.Scalar345 * mv2.Scalar135;
            tempScalar[10] += -mv1.KVector3.Scalar123 * mv2.Scalar134 + mv1.KVector3.Scalar134 * mv2.Scalar123 + mv1.KVector3.Scalar125 * mv2.Scalar145 + mv1.KVector3.Scalar235 * mv2.Scalar345 - mv1.KVector3.Scalar145 * mv2.Scalar125 - mv1.KVector3.Scalar345 * mv2.Scalar235;
            tempScalar[12] += mv1.KVector3.Scalar123 * mv2.Scalar124 - mv1.KVector3.Scalar124 * mv2.Scalar123 + mv1.KVector3.Scalar135 * mv2.Scalar145 - mv1.KVector3.Scalar235 * mv2.Scalar245 - mv1.KVector3.Scalar145 * mv2.Scalar135 + mv1.KVector3.Scalar245 * mv2.Scalar235;
            tempScalar[17] += -mv1.KVector3.Scalar123 * mv2.Scalar235 - mv1.KVector3.Scalar124 * mv2.Scalar245 - mv1.KVector3.Scalar134 * mv2.Scalar345 + mv1.KVector3.Scalar235 * mv2.Scalar123 + mv1.KVector3.Scalar245 * mv2.Scalar124 + mv1.KVector3.Scalar345 * mv2.Scalar134;
            tempScalar[18] += -mv1.KVector3.Scalar123 * mv2.Scalar135 - mv1.KVector3.Scalar124 * mv2.Scalar145 - mv1.KVector3.Scalar234 * mv2.Scalar345 + mv1.KVector3.Scalar135 * mv2.Scalar123 + mv1.KVector3.Scalar145 * mv2.Scalar124 + mv1.KVector3.Scalar345 * mv2.Scalar234;
            tempScalar[20] += mv1.KVector3.Scalar123 * mv2.Scalar125 - mv1.KVector3.Scalar134 * mv2.Scalar145 + mv1.KVector3.Scalar234 * mv2.Scalar245 - mv1.KVector3.Scalar125 * mv2.Scalar123 + mv1.KVector3.Scalar145 * mv2.Scalar134 - mv1.KVector3.Scalar245 * mv2.Scalar234;
            tempScalar[24] += mv1.KVector3.Scalar124 * mv2.Scalar125 + mv1.KVector3.Scalar134 * mv2.Scalar135 - mv1.KVector3.Scalar234 * mv2.Scalar235 - mv1.KVector3.Scalar125 * mv2.Scalar124 - mv1.KVector3.Scalar135 * mv2.Scalar134 + mv1.KVector3.Scalar235 * mv2.Scalar234;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[1] += -mv1.KVector4.Scalar1234 * mv2.Scalar234 - mv1.KVector4.Scalar1235 * mv2.Scalar235 - mv1.KVector4.Scalar1245 * mv2.Scalar245 - mv1.KVector4.Scalar1345 * mv2.Scalar345;
            tempScalar[2] += -mv1.KVector4.Scalar1234 * mv2.Scalar134 - mv1.KVector4.Scalar1235 * mv2.Scalar135 - mv1.KVector4.Scalar1245 * mv2.Scalar145 - mv1.KVector4.Scalar2345 * mv2.Scalar345;
            tempScalar[4] += mv1.KVector4.Scalar1234 * mv2.Scalar124 + mv1.KVector4.Scalar1235 * mv2.Scalar125 - mv1.KVector4.Scalar1345 * mv2.Scalar145 + mv1.KVector4.Scalar2345 * mv2.Scalar245;
            tempScalar[8] += -mv1.KVector4.Scalar1234 * mv2.Scalar123 + mv1.KVector4.Scalar1245 * mv2.Scalar125 + mv1.KVector4.Scalar1345 * mv2.Scalar135 - mv1.KVector4.Scalar2345 * mv2.Scalar235;
            tempScalar[16] += -mv1.KVector4.Scalar1235 * mv2.Scalar123 - mv1.KVector4.Scalar1245 * mv2.Scalar124 - mv1.KVector4.Scalar1345 * mv2.Scalar134 + mv1.KVector4.Scalar2345 * mv2.Scalar234;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    public static Ga41Multivector Cp(this Ga41Multivector mv1, Ga41KVector4 mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
        if (!mv1.KVector1.IsZero())
        {
            tempScalar[7] += -mv1.KVector1.Scalar4 * mv2.Scalar1234 - mv1.KVector1.Scalar5 * mv2.Scalar1235;
            tempScalar[11] += mv1.KVector1.Scalar3 * mv2.Scalar1234 - mv1.KVector1.Scalar5 * mv2.Scalar1245;
            tempScalar[13] += -mv1.KVector1.Scalar2 * mv2.Scalar1234 - mv1.KVector1.Scalar5 * mv2.Scalar1345;
            tempScalar[14] += -mv1.KVector1.Scalar1 * mv2.Scalar1234 - mv1.KVector1.Scalar5 * mv2.Scalar2345;
            tempScalar[19] += mv1.KVector1.Scalar3 * mv2.Scalar1235 + mv1.KVector1.Scalar4 * mv2.Scalar1245;
            tempScalar[21] += -mv1.KVector1.Scalar2 * mv2.Scalar1235 + mv1.KVector1.Scalar4 * mv2.Scalar1345;
            tempScalar[22] += -mv1.KVector1.Scalar1 * mv2.Scalar1235 + mv1.KVector1.Scalar4 * mv2.Scalar2345;
            tempScalar[25] += -mv1.KVector1.Scalar2 * mv2.Scalar1245 - mv1.KVector1.Scalar3 * mv2.Scalar1345;
            tempScalar[26] += -mv1.KVector1.Scalar1 * mv2.Scalar1245 - mv1.KVector1.Scalar3 * mv2.Scalar2345;
            tempScalar[28] += -mv1.KVector1.Scalar1 * mv2.Scalar1345 + mv1.KVector1.Scalar2 * mv2.Scalar2345;
        }
        
        if (!mv1.KVector2.IsZero())
        {
            tempScalar[15] += -mv1.KVector2.Scalar15 * mv2.Scalar2345 + mv1.KVector2.Scalar25 * mv2.Scalar1345 - mv1.KVector2.Scalar35 * mv2.Scalar1245 + mv1.KVector2.Scalar45 * mv2.Scalar1235;
            tempScalar[23] += mv1.KVector2.Scalar14 * mv2.Scalar2345 - mv1.KVector2.Scalar24 * mv2.Scalar1345 + mv1.KVector2.Scalar34 * mv2.Scalar1245 - mv1.KVector2.Scalar45 * mv2.Scalar1234;
            tempScalar[27] += -mv1.KVector2.Scalar13 * mv2.Scalar2345 + mv1.KVector2.Scalar23 * mv2.Scalar1345 - mv1.KVector2.Scalar34 * mv2.Scalar1235 + mv1.KVector2.Scalar35 * mv2.Scalar1234;
            tempScalar[29] += mv1.KVector2.Scalar12 * mv2.Scalar2345 - mv1.KVector2.Scalar23 * mv2.Scalar1245 + mv1.KVector2.Scalar24 * mv2.Scalar1235 - mv1.KVector2.Scalar25 * mv2.Scalar1234;
            tempScalar[30] += mv1.KVector2.Scalar12 * mv2.Scalar1345 - mv1.KVector2.Scalar13 * mv2.Scalar1245 + mv1.KVector2.Scalar14 * mv2.Scalar1235 - mv1.KVector2.Scalar15 * mv2.Scalar1234;
        }
        
        if (!mv1.KVector3.IsZero())
        {
            tempScalar[1] += mv1.KVector3.Scalar234 * mv2.Scalar1234 + mv1.KVector3.Scalar235 * mv2.Scalar1235 + mv1.KVector3.Scalar245 * mv2.Scalar1245 + mv1.KVector3.Scalar345 * mv2.Scalar1345;
            tempScalar[2] += mv1.KVector3.Scalar134 * mv2.Scalar1234 + mv1.KVector3.Scalar135 * mv2.Scalar1235 + mv1.KVector3.Scalar145 * mv2.Scalar1245 + mv1.KVector3.Scalar345 * mv2.Scalar2345;
            tempScalar[4] += -mv1.KVector3.Scalar124 * mv2.Scalar1234 - mv1.KVector3.Scalar125 * mv2.Scalar1235 + mv1.KVector3.Scalar145 * mv2.Scalar1345 - mv1.KVector3.Scalar245 * mv2.Scalar2345;
            tempScalar[8] += mv1.KVector3.Scalar123 * mv2.Scalar1234 - mv1.KVector3.Scalar125 * mv2.Scalar1245 - mv1.KVector3.Scalar135 * mv2.Scalar1345 + mv1.KVector3.Scalar235 * mv2.Scalar2345;
            tempScalar[16] += mv1.KVector3.Scalar123 * mv2.Scalar1235 + mv1.KVector3.Scalar124 * mv2.Scalar1245 + mv1.KVector3.Scalar134 * mv2.Scalar1345 - mv1.KVector3.Scalar234 * mv2.Scalar2345;
        }
        
        if (!mv1.KVector4.IsZero())
        {
            tempScalar[3] += mv1.KVector4.Scalar1345 * mv2.Scalar2345 - mv1.KVector4.Scalar2345 * mv2.Scalar1345;
            tempScalar[5] += -mv1.KVector4.Scalar1245 * mv2.Scalar2345 + mv1.KVector4.Scalar2345 * mv2.Scalar1245;
            tempScalar[6] += -mv1.KVector4.Scalar1245 * mv2.Scalar1345 + mv1.KVector4.Scalar1345 * mv2.Scalar1245;
            tempScalar[9] += mv1.KVector4.Scalar1235 * mv2.Scalar2345 - mv1.KVector4.Scalar2345 * mv2.Scalar1235;
            tempScalar[10] += mv1.KVector4.Scalar1235 * mv2.Scalar1345 - mv1.KVector4.Scalar1345 * mv2.Scalar1235;
            tempScalar[12] += -mv1.KVector4.Scalar1235 * mv2.Scalar1245 + mv1.KVector4.Scalar1245 * mv2.Scalar1235;
            tempScalar[17] += -mv1.KVector4.Scalar1234 * mv2.Scalar2345 + mv1.KVector4.Scalar2345 * mv2.Scalar1234;
            tempScalar[18] += -mv1.KVector4.Scalar1234 * mv2.Scalar1345 + mv1.KVector4.Scalar1345 * mv2.Scalar1234;
            tempScalar[20] += mv1.KVector4.Scalar1234 * mv2.Scalar1245 - mv1.KVector4.Scalar1245 * mv2.Scalar1234;
            tempScalar[24] += -mv1.KVector4.Scalar1234 * mv2.Scalar1235 + mv1.KVector4.Scalar1235 * mv2.Scalar1234;
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Ga41KVector0 Cp(this Ga41Multivector mv1, Ga41KVector5 mv2)
    {
        return Ga41KVector0.Zero;
    }
    
    public static Ga41Multivector Cp(this Ga41Multivector mv1, Ga41Multivector mv2)
    {
        if (mv1.IsZero() || mv2.IsZero()) return Ga41Multivector.Zero;
        
        var tempScalar = new double[32];
        
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
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[1] += -mv1.KVector1.Scalar2 * mv2.KVector2.Scalar12 - mv1.KVector1.Scalar3 * mv2.KVector2.Scalar13 - mv1.KVector1.Scalar4 * mv2.KVector2.Scalar14 - mv1.KVector1.Scalar5 * mv2.KVector2.Scalar15;
                tempScalar[2] += -mv1.KVector1.Scalar1 * mv2.KVector2.Scalar12 - mv1.KVector1.Scalar3 * mv2.KVector2.Scalar23 - mv1.KVector1.Scalar4 * mv2.KVector2.Scalar24 - mv1.KVector1.Scalar5 * mv2.KVector2.Scalar25;
                tempScalar[4] += -mv1.KVector1.Scalar1 * mv2.KVector2.Scalar13 + mv1.KVector1.Scalar2 * mv2.KVector2.Scalar23 - mv1.KVector1.Scalar4 * mv2.KVector2.Scalar34 - mv1.KVector1.Scalar5 * mv2.KVector2.Scalar35;
                tempScalar[8] += -mv1.KVector1.Scalar1 * mv2.KVector2.Scalar14 + mv1.KVector1.Scalar2 * mv2.KVector2.Scalar24 + mv1.KVector1.Scalar3 * mv2.KVector2.Scalar34 - mv1.KVector1.Scalar5 * mv2.KVector2.Scalar45;
                tempScalar[16] += -mv1.KVector1.Scalar1 * mv2.KVector2.Scalar15 + mv1.KVector1.Scalar2 * mv2.KVector2.Scalar25 + mv1.KVector1.Scalar3 * mv2.KVector2.Scalar35 + mv1.KVector1.Scalar4 * mv2.KVector2.Scalar45;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[15] += mv1.KVector1.Scalar1 * mv2.KVector3.Scalar234 - mv1.KVector1.Scalar2 * mv2.KVector3.Scalar134 + mv1.KVector1.Scalar3 * mv2.KVector3.Scalar124 - mv1.KVector1.Scalar4 * mv2.KVector3.Scalar123;
                tempScalar[23] += mv1.KVector1.Scalar1 * mv2.KVector3.Scalar235 - mv1.KVector1.Scalar2 * mv2.KVector3.Scalar135 + mv1.KVector1.Scalar3 * mv2.KVector3.Scalar125 - mv1.KVector1.Scalar5 * mv2.KVector3.Scalar123;
                tempScalar[27] += mv1.KVector1.Scalar1 * mv2.KVector3.Scalar245 - mv1.KVector1.Scalar2 * mv2.KVector3.Scalar145 + mv1.KVector1.Scalar4 * mv2.KVector3.Scalar125 - mv1.KVector1.Scalar5 * mv2.KVector3.Scalar124;
                tempScalar[29] += mv1.KVector1.Scalar1 * mv2.KVector3.Scalar345 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar145 + mv1.KVector1.Scalar4 * mv2.KVector3.Scalar135 - mv1.KVector1.Scalar5 * mv2.KVector3.Scalar134;
                tempScalar[30] += mv1.KVector1.Scalar2 * mv2.KVector3.Scalar345 - mv1.KVector1.Scalar3 * mv2.KVector3.Scalar245 + mv1.KVector1.Scalar4 * mv2.KVector3.Scalar235 - mv1.KVector1.Scalar5 * mv2.KVector3.Scalar234;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[7] += -mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1234 - mv1.KVector1.Scalar5 * mv2.KVector4.Scalar1235;
                tempScalar[11] += mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1234 - mv1.KVector1.Scalar5 * mv2.KVector4.Scalar1245;
                tempScalar[13] += -mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1234 - mv1.KVector1.Scalar5 * mv2.KVector4.Scalar1345;
                tempScalar[14] += -mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1234 - mv1.KVector1.Scalar5 * mv2.KVector4.Scalar2345;
                tempScalar[19] += mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1235 + mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1245;
                tempScalar[21] += -mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1235 + mv1.KVector1.Scalar4 * mv2.KVector4.Scalar1345;
                tempScalar[22] += -mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1235 + mv1.KVector1.Scalar4 * mv2.KVector4.Scalar2345;
                tempScalar[25] += -mv1.KVector1.Scalar2 * mv2.KVector4.Scalar1245 - mv1.KVector1.Scalar3 * mv2.KVector4.Scalar1345;
                tempScalar[26] += -mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1245 - mv1.KVector1.Scalar3 * mv2.KVector4.Scalar2345;
                tempScalar[28] += -mv1.KVector1.Scalar1 * mv2.KVector4.Scalar1345 + mv1.KVector1.Scalar2 * mv2.KVector4.Scalar2345;
            }
            
        }
        
        if (!mv1.KVector2.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[1] += mv1.KVector2.Scalar12 * mv2.KVector1.Scalar2 + mv1.KVector2.Scalar13 * mv2.KVector1.Scalar3 + mv1.KVector2.Scalar14 * mv2.KVector1.Scalar4 + mv1.KVector2.Scalar15 * mv2.KVector1.Scalar5;
                tempScalar[2] += mv1.KVector2.Scalar12 * mv2.KVector1.Scalar1 + mv1.KVector2.Scalar23 * mv2.KVector1.Scalar3 + mv1.KVector2.Scalar24 * mv2.KVector1.Scalar4 + mv1.KVector2.Scalar25 * mv2.KVector1.Scalar5;
                tempScalar[4] += mv1.KVector2.Scalar13 * mv2.KVector1.Scalar1 - mv1.KVector2.Scalar23 * mv2.KVector1.Scalar2 + mv1.KVector2.Scalar34 * mv2.KVector1.Scalar4 + mv1.KVector2.Scalar35 * mv2.KVector1.Scalar5;
                tempScalar[8] += mv1.KVector2.Scalar14 * mv2.KVector1.Scalar1 - mv1.KVector2.Scalar24 * mv2.KVector1.Scalar2 - mv1.KVector2.Scalar34 * mv2.KVector1.Scalar3 + mv1.KVector2.Scalar45 * mv2.KVector1.Scalar5;
                tempScalar[16] += mv1.KVector2.Scalar15 * mv2.KVector1.Scalar1 - mv1.KVector2.Scalar25 * mv2.KVector1.Scalar2 - mv1.KVector2.Scalar35 * mv2.KVector1.Scalar3 - mv1.KVector2.Scalar45 * mv2.KVector1.Scalar4;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[3] += -mv1.KVector2.Scalar13 * mv2.KVector2.Scalar23 + mv1.KVector2.Scalar23 * mv2.KVector2.Scalar13 - mv1.KVector2.Scalar14 * mv2.KVector2.Scalar24 + mv1.KVector2.Scalar24 * mv2.KVector2.Scalar14 - mv1.KVector2.Scalar15 * mv2.KVector2.Scalar25 + mv1.KVector2.Scalar25 * mv2.KVector2.Scalar15;
                tempScalar[5] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar23 - mv1.KVector2.Scalar23 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar14 * mv2.KVector2.Scalar34 + mv1.KVector2.Scalar34 * mv2.KVector2.Scalar14 - mv1.KVector2.Scalar15 * mv2.KVector2.Scalar35 + mv1.KVector2.Scalar35 * mv2.KVector2.Scalar15;
                tempScalar[6] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar13 - mv1.KVector2.Scalar13 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar24 * mv2.KVector2.Scalar34 + mv1.KVector2.Scalar34 * mv2.KVector2.Scalar24 - mv1.KVector2.Scalar25 * mv2.KVector2.Scalar35 + mv1.KVector2.Scalar35 * mv2.KVector2.Scalar25;
                tempScalar[9] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar24 + mv1.KVector2.Scalar13 * mv2.KVector2.Scalar34 - mv1.KVector2.Scalar24 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar34 * mv2.KVector2.Scalar13 - mv1.KVector2.Scalar15 * mv2.KVector2.Scalar45 + mv1.KVector2.Scalar45 * mv2.KVector2.Scalar15;
                tempScalar[10] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar14 + mv1.KVector2.Scalar23 * mv2.KVector2.Scalar34 - mv1.KVector2.Scalar14 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar34 * mv2.KVector2.Scalar23 - mv1.KVector2.Scalar25 * mv2.KVector2.Scalar45 + mv1.KVector2.Scalar45 * mv2.KVector2.Scalar25;
                tempScalar[12] += mv1.KVector2.Scalar13 * mv2.KVector2.Scalar14 - mv1.KVector2.Scalar23 * mv2.KVector2.Scalar24 - mv1.KVector2.Scalar14 * mv2.KVector2.Scalar13 + mv1.KVector2.Scalar24 * mv2.KVector2.Scalar23 - mv1.KVector2.Scalar35 * mv2.KVector2.Scalar45 + mv1.KVector2.Scalar45 * mv2.KVector2.Scalar35;
                tempScalar[17] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar25 + mv1.KVector2.Scalar13 * mv2.KVector2.Scalar35 + mv1.KVector2.Scalar14 * mv2.KVector2.Scalar45 - mv1.KVector2.Scalar25 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar35 * mv2.KVector2.Scalar13 - mv1.KVector2.Scalar45 * mv2.KVector2.Scalar14;
                tempScalar[18] += mv1.KVector2.Scalar12 * mv2.KVector2.Scalar15 + mv1.KVector2.Scalar23 * mv2.KVector2.Scalar35 + mv1.KVector2.Scalar24 * mv2.KVector2.Scalar45 - mv1.KVector2.Scalar15 * mv2.KVector2.Scalar12 - mv1.KVector2.Scalar35 * mv2.KVector2.Scalar23 - mv1.KVector2.Scalar45 * mv2.KVector2.Scalar24;
                tempScalar[20] += mv1.KVector2.Scalar13 * mv2.KVector2.Scalar15 - mv1.KVector2.Scalar23 * mv2.KVector2.Scalar25 + mv1.KVector2.Scalar34 * mv2.KVector2.Scalar45 - mv1.KVector2.Scalar15 * mv2.KVector2.Scalar13 + mv1.KVector2.Scalar25 * mv2.KVector2.Scalar23 - mv1.KVector2.Scalar45 * mv2.KVector2.Scalar34;
                tempScalar[24] += mv1.KVector2.Scalar14 * mv2.KVector2.Scalar15 - mv1.KVector2.Scalar24 * mv2.KVector2.Scalar25 - mv1.KVector2.Scalar34 * mv2.KVector2.Scalar35 - mv1.KVector2.Scalar15 * mv2.KVector2.Scalar14 + mv1.KVector2.Scalar25 * mv2.KVector2.Scalar24 + mv1.KVector2.Scalar35 * mv2.KVector2.Scalar34;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[7] += mv1.KVector2.Scalar14 * mv2.KVector3.Scalar234 - mv1.KVector2.Scalar24 * mv2.KVector3.Scalar134 + mv1.KVector2.Scalar34 * mv2.KVector3.Scalar124 + mv1.KVector2.Scalar15 * mv2.KVector3.Scalar235 - mv1.KVector2.Scalar25 * mv2.KVector3.Scalar135 + mv1.KVector2.Scalar35 * mv2.KVector3.Scalar125;
                tempScalar[11] += -mv1.KVector2.Scalar13 * mv2.KVector3.Scalar234 + mv1.KVector2.Scalar23 * mv2.KVector3.Scalar134 - mv1.KVector2.Scalar34 * mv2.KVector3.Scalar123 + mv1.KVector2.Scalar15 * mv2.KVector3.Scalar245 - mv1.KVector2.Scalar25 * mv2.KVector3.Scalar145 + mv1.KVector2.Scalar45 * mv2.KVector3.Scalar125;
                tempScalar[13] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar234 - mv1.KVector2.Scalar23 * mv2.KVector3.Scalar124 + mv1.KVector2.Scalar24 * mv2.KVector3.Scalar123 + mv1.KVector2.Scalar15 * mv2.KVector3.Scalar345 - mv1.KVector2.Scalar35 * mv2.KVector3.Scalar145 + mv1.KVector2.Scalar45 * mv2.KVector3.Scalar135;
                tempScalar[14] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar134 - mv1.KVector2.Scalar13 * mv2.KVector3.Scalar124 + mv1.KVector2.Scalar14 * mv2.KVector3.Scalar123 + mv1.KVector2.Scalar25 * mv2.KVector3.Scalar345 - mv1.KVector2.Scalar35 * mv2.KVector3.Scalar245 + mv1.KVector2.Scalar45 * mv2.KVector3.Scalar235;
                tempScalar[19] += -mv1.KVector2.Scalar13 * mv2.KVector3.Scalar235 + mv1.KVector2.Scalar23 * mv2.KVector3.Scalar135 - mv1.KVector2.Scalar14 * mv2.KVector3.Scalar245 + mv1.KVector2.Scalar24 * mv2.KVector3.Scalar145 - mv1.KVector2.Scalar35 * mv2.KVector3.Scalar123 - mv1.KVector2.Scalar45 * mv2.KVector3.Scalar124;
                tempScalar[21] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar235 - mv1.KVector2.Scalar23 * mv2.KVector3.Scalar125 - mv1.KVector2.Scalar14 * mv2.KVector3.Scalar345 + mv1.KVector2.Scalar34 * mv2.KVector3.Scalar145 + mv1.KVector2.Scalar25 * mv2.KVector3.Scalar123 - mv1.KVector2.Scalar45 * mv2.KVector3.Scalar134;
                tempScalar[22] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar135 - mv1.KVector2.Scalar13 * mv2.KVector3.Scalar125 - mv1.KVector2.Scalar24 * mv2.KVector3.Scalar345 + mv1.KVector2.Scalar34 * mv2.KVector3.Scalar245 + mv1.KVector2.Scalar15 * mv2.KVector3.Scalar123 - mv1.KVector2.Scalar45 * mv2.KVector3.Scalar234;
                tempScalar[25] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar245 + mv1.KVector2.Scalar13 * mv2.KVector3.Scalar345 - mv1.KVector2.Scalar24 * mv2.KVector3.Scalar125 - mv1.KVector2.Scalar34 * mv2.KVector3.Scalar135 + mv1.KVector2.Scalar25 * mv2.KVector3.Scalar124 + mv1.KVector2.Scalar35 * mv2.KVector3.Scalar134;
                tempScalar[26] += mv1.KVector2.Scalar12 * mv2.KVector3.Scalar145 + mv1.KVector2.Scalar23 * mv2.KVector3.Scalar345 - mv1.KVector2.Scalar14 * mv2.KVector3.Scalar125 - mv1.KVector2.Scalar34 * mv2.KVector3.Scalar235 + mv1.KVector2.Scalar15 * mv2.KVector3.Scalar124 + mv1.KVector2.Scalar35 * mv2.KVector3.Scalar234;
                tempScalar[28] += mv1.KVector2.Scalar13 * mv2.KVector3.Scalar145 - mv1.KVector2.Scalar23 * mv2.KVector3.Scalar245 - mv1.KVector2.Scalar14 * mv2.KVector3.Scalar135 + mv1.KVector2.Scalar24 * mv2.KVector3.Scalar235 + mv1.KVector2.Scalar15 * mv2.KVector3.Scalar134 - mv1.KVector2.Scalar25 * mv2.KVector3.Scalar234;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[15] += -mv1.KVector2.Scalar15 * mv2.KVector4.Scalar2345 + mv1.KVector2.Scalar25 * mv2.KVector4.Scalar1345 - mv1.KVector2.Scalar35 * mv2.KVector4.Scalar1245 + mv1.KVector2.Scalar45 * mv2.KVector4.Scalar1235;
                tempScalar[23] += mv1.KVector2.Scalar14 * mv2.KVector4.Scalar2345 - mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1345 + mv1.KVector2.Scalar34 * mv2.KVector4.Scalar1245 - mv1.KVector2.Scalar45 * mv2.KVector4.Scalar1234;
                tempScalar[27] += -mv1.KVector2.Scalar13 * mv2.KVector4.Scalar2345 + mv1.KVector2.Scalar23 * mv2.KVector4.Scalar1345 - mv1.KVector2.Scalar34 * mv2.KVector4.Scalar1235 + mv1.KVector2.Scalar35 * mv2.KVector4.Scalar1234;
                tempScalar[29] += mv1.KVector2.Scalar12 * mv2.KVector4.Scalar2345 - mv1.KVector2.Scalar23 * mv2.KVector4.Scalar1245 + mv1.KVector2.Scalar24 * mv2.KVector4.Scalar1235 - mv1.KVector2.Scalar25 * mv2.KVector4.Scalar1234;
                tempScalar[30] += mv1.KVector2.Scalar12 * mv2.KVector4.Scalar1345 - mv1.KVector2.Scalar13 * mv2.KVector4.Scalar1245 + mv1.KVector2.Scalar14 * mv2.KVector4.Scalar1235 - mv1.KVector2.Scalar15 * mv2.KVector4.Scalar1234;
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
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[7] += -mv1.KVector3.Scalar124 * mv2.KVector2.Scalar34 + mv1.KVector3.Scalar134 * mv2.KVector2.Scalar24 - mv1.KVector3.Scalar234 * mv2.KVector2.Scalar14 - mv1.KVector3.Scalar125 * mv2.KVector2.Scalar35 + mv1.KVector3.Scalar135 * mv2.KVector2.Scalar25 - mv1.KVector3.Scalar235 * mv2.KVector2.Scalar15;
                tempScalar[11] += mv1.KVector3.Scalar123 * mv2.KVector2.Scalar34 - mv1.KVector3.Scalar134 * mv2.KVector2.Scalar23 + mv1.KVector3.Scalar234 * mv2.KVector2.Scalar13 - mv1.KVector3.Scalar125 * mv2.KVector2.Scalar45 + mv1.KVector3.Scalar145 * mv2.KVector2.Scalar25 - mv1.KVector3.Scalar245 * mv2.KVector2.Scalar15;
                tempScalar[13] += -mv1.KVector3.Scalar123 * mv2.KVector2.Scalar24 + mv1.KVector3.Scalar124 * mv2.KVector2.Scalar23 - mv1.KVector3.Scalar234 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar135 * mv2.KVector2.Scalar45 + mv1.KVector3.Scalar145 * mv2.KVector2.Scalar35 - mv1.KVector3.Scalar345 * mv2.KVector2.Scalar15;
                tempScalar[14] += -mv1.KVector3.Scalar123 * mv2.KVector2.Scalar14 + mv1.KVector3.Scalar124 * mv2.KVector2.Scalar13 - mv1.KVector3.Scalar134 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar235 * mv2.KVector2.Scalar45 + mv1.KVector3.Scalar245 * mv2.KVector2.Scalar35 - mv1.KVector3.Scalar345 * mv2.KVector2.Scalar25;
                tempScalar[19] += mv1.KVector3.Scalar123 * mv2.KVector2.Scalar35 + mv1.KVector3.Scalar124 * mv2.KVector2.Scalar45 - mv1.KVector3.Scalar135 * mv2.KVector2.Scalar23 + mv1.KVector3.Scalar235 * mv2.KVector2.Scalar13 - mv1.KVector3.Scalar145 * mv2.KVector2.Scalar24 + mv1.KVector3.Scalar245 * mv2.KVector2.Scalar14;
                tempScalar[21] += -mv1.KVector3.Scalar123 * mv2.KVector2.Scalar25 + mv1.KVector3.Scalar134 * mv2.KVector2.Scalar45 + mv1.KVector3.Scalar125 * mv2.KVector2.Scalar23 - mv1.KVector3.Scalar235 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar145 * mv2.KVector2.Scalar34 + mv1.KVector3.Scalar345 * mv2.KVector2.Scalar14;
                tempScalar[22] += -mv1.KVector3.Scalar123 * mv2.KVector2.Scalar15 + mv1.KVector3.Scalar234 * mv2.KVector2.Scalar45 + mv1.KVector3.Scalar125 * mv2.KVector2.Scalar13 - mv1.KVector3.Scalar135 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar245 * mv2.KVector2.Scalar34 + mv1.KVector3.Scalar345 * mv2.KVector2.Scalar24;
                tempScalar[25] += -mv1.KVector3.Scalar124 * mv2.KVector2.Scalar25 - mv1.KVector3.Scalar134 * mv2.KVector2.Scalar35 + mv1.KVector3.Scalar125 * mv2.KVector2.Scalar24 + mv1.KVector3.Scalar135 * mv2.KVector2.Scalar34 - mv1.KVector3.Scalar245 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar345 * mv2.KVector2.Scalar13;
                tempScalar[26] += -mv1.KVector3.Scalar124 * mv2.KVector2.Scalar15 - mv1.KVector3.Scalar234 * mv2.KVector2.Scalar35 + mv1.KVector3.Scalar125 * mv2.KVector2.Scalar14 + mv1.KVector3.Scalar235 * mv2.KVector2.Scalar34 - mv1.KVector3.Scalar145 * mv2.KVector2.Scalar12 - mv1.KVector3.Scalar345 * mv2.KVector2.Scalar23;
                tempScalar[28] += -mv1.KVector3.Scalar134 * mv2.KVector2.Scalar15 + mv1.KVector3.Scalar234 * mv2.KVector2.Scalar25 + mv1.KVector3.Scalar135 * mv2.KVector2.Scalar14 - mv1.KVector3.Scalar235 * mv2.KVector2.Scalar24 - mv1.KVector3.Scalar145 * mv2.KVector2.Scalar13 + mv1.KVector3.Scalar245 * mv2.KVector2.Scalar23;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[3] += -mv1.KVector3.Scalar134 * mv2.KVector3.Scalar234 + mv1.KVector3.Scalar234 * mv2.KVector3.Scalar134 - mv1.KVector3.Scalar135 * mv2.KVector3.Scalar235 + mv1.KVector3.Scalar235 * mv2.KVector3.Scalar135 - mv1.KVector3.Scalar145 * mv2.KVector3.Scalar245 + mv1.KVector3.Scalar245 * mv2.KVector3.Scalar145;
                tempScalar[5] += mv1.KVector3.Scalar124 * mv2.KVector3.Scalar234 - mv1.KVector3.Scalar234 * mv2.KVector3.Scalar124 + mv1.KVector3.Scalar125 * mv2.KVector3.Scalar235 - mv1.KVector3.Scalar235 * mv2.KVector3.Scalar125 - mv1.KVector3.Scalar145 * mv2.KVector3.Scalar345 + mv1.KVector3.Scalar345 * mv2.KVector3.Scalar145;
                tempScalar[6] += mv1.KVector3.Scalar124 * mv2.KVector3.Scalar134 - mv1.KVector3.Scalar134 * mv2.KVector3.Scalar124 + mv1.KVector3.Scalar125 * mv2.KVector3.Scalar135 - mv1.KVector3.Scalar135 * mv2.KVector3.Scalar125 - mv1.KVector3.Scalar245 * mv2.KVector3.Scalar345 + mv1.KVector3.Scalar345 * mv2.KVector3.Scalar245;
                tempScalar[9] += -mv1.KVector3.Scalar123 * mv2.KVector3.Scalar234 + mv1.KVector3.Scalar234 * mv2.KVector3.Scalar123 + mv1.KVector3.Scalar125 * mv2.KVector3.Scalar245 + mv1.KVector3.Scalar135 * mv2.KVector3.Scalar345 - mv1.KVector3.Scalar245 * mv2.KVector3.Scalar125 - mv1.KVector3.Scalar345 * mv2.KVector3.Scalar135;
                tempScalar[10] += -mv1.KVector3.Scalar123 * mv2.KVector3.Scalar134 + mv1.KVector3.Scalar134 * mv2.KVector3.Scalar123 + mv1.KVector3.Scalar125 * mv2.KVector3.Scalar145 + mv1.KVector3.Scalar235 * mv2.KVector3.Scalar345 - mv1.KVector3.Scalar145 * mv2.KVector3.Scalar125 - mv1.KVector3.Scalar345 * mv2.KVector3.Scalar235;
                tempScalar[12] += mv1.KVector3.Scalar123 * mv2.KVector3.Scalar124 - mv1.KVector3.Scalar124 * mv2.KVector3.Scalar123 + mv1.KVector3.Scalar135 * mv2.KVector3.Scalar145 - mv1.KVector3.Scalar235 * mv2.KVector3.Scalar245 - mv1.KVector3.Scalar145 * mv2.KVector3.Scalar135 + mv1.KVector3.Scalar245 * mv2.KVector3.Scalar235;
                tempScalar[17] += -mv1.KVector3.Scalar123 * mv2.KVector3.Scalar235 - mv1.KVector3.Scalar124 * mv2.KVector3.Scalar245 - mv1.KVector3.Scalar134 * mv2.KVector3.Scalar345 + mv1.KVector3.Scalar235 * mv2.KVector3.Scalar123 + mv1.KVector3.Scalar245 * mv2.KVector3.Scalar124 + mv1.KVector3.Scalar345 * mv2.KVector3.Scalar134;
                tempScalar[18] += -mv1.KVector3.Scalar123 * mv2.KVector3.Scalar135 - mv1.KVector3.Scalar124 * mv2.KVector3.Scalar145 - mv1.KVector3.Scalar234 * mv2.KVector3.Scalar345 + mv1.KVector3.Scalar135 * mv2.KVector3.Scalar123 + mv1.KVector3.Scalar145 * mv2.KVector3.Scalar124 + mv1.KVector3.Scalar345 * mv2.KVector3.Scalar234;
                tempScalar[20] += mv1.KVector3.Scalar123 * mv2.KVector3.Scalar125 - mv1.KVector3.Scalar134 * mv2.KVector3.Scalar145 + mv1.KVector3.Scalar234 * mv2.KVector3.Scalar245 - mv1.KVector3.Scalar125 * mv2.KVector3.Scalar123 + mv1.KVector3.Scalar145 * mv2.KVector3.Scalar134 - mv1.KVector3.Scalar245 * mv2.KVector3.Scalar234;
                tempScalar[24] += mv1.KVector3.Scalar124 * mv2.KVector3.Scalar125 + mv1.KVector3.Scalar134 * mv2.KVector3.Scalar135 - mv1.KVector3.Scalar234 * mv2.KVector3.Scalar235 - mv1.KVector3.Scalar125 * mv2.KVector3.Scalar124 - mv1.KVector3.Scalar135 * mv2.KVector3.Scalar134 + mv1.KVector3.Scalar235 * mv2.KVector3.Scalar234;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[1] += mv1.KVector3.Scalar234 * mv2.KVector4.Scalar1234 + mv1.KVector3.Scalar235 * mv2.KVector4.Scalar1235 + mv1.KVector3.Scalar245 * mv2.KVector4.Scalar1245 + mv1.KVector3.Scalar345 * mv2.KVector4.Scalar1345;
                tempScalar[2] += mv1.KVector3.Scalar134 * mv2.KVector4.Scalar1234 + mv1.KVector3.Scalar135 * mv2.KVector4.Scalar1235 + mv1.KVector3.Scalar145 * mv2.KVector4.Scalar1245 + mv1.KVector3.Scalar345 * mv2.KVector4.Scalar2345;
                tempScalar[4] += -mv1.KVector3.Scalar124 * mv2.KVector4.Scalar1234 - mv1.KVector3.Scalar125 * mv2.KVector4.Scalar1235 + mv1.KVector3.Scalar145 * mv2.KVector4.Scalar1345 - mv1.KVector3.Scalar245 * mv2.KVector4.Scalar2345;
                tempScalar[8] += mv1.KVector3.Scalar123 * mv2.KVector4.Scalar1234 - mv1.KVector3.Scalar125 * mv2.KVector4.Scalar1245 - mv1.KVector3.Scalar135 * mv2.KVector4.Scalar1345 + mv1.KVector3.Scalar235 * mv2.KVector4.Scalar2345;
                tempScalar[16] += mv1.KVector3.Scalar123 * mv2.KVector4.Scalar1235 + mv1.KVector3.Scalar124 * mv2.KVector4.Scalar1245 + mv1.KVector3.Scalar134 * mv2.KVector4.Scalar1345 - mv1.KVector3.Scalar234 * mv2.KVector4.Scalar2345;
            }
            
        }
        
        if (!mv1.KVector4.IsZero())
        {
            if (!mv2.KVector1.IsZero())
            {
                tempScalar[7] += mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar4 + mv1.KVector4.Scalar1235 * mv2.KVector1.Scalar5;
                tempScalar[11] += -mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar3 + mv1.KVector4.Scalar1245 * mv2.KVector1.Scalar5;
                tempScalar[13] += mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar2 + mv1.KVector4.Scalar1345 * mv2.KVector1.Scalar5;
                tempScalar[14] += mv1.KVector4.Scalar1234 * mv2.KVector1.Scalar1 + mv1.KVector4.Scalar2345 * mv2.KVector1.Scalar5;
                tempScalar[19] += -mv1.KVector4.Scalar1235 * mv2.KVector1.Scalar3 - mv1.KVector4.Scalar1245 * mv2.KVector1.Scalar4;
                tempScalar[21] += mv1.KVector4.Scalar1235 * mv2.KVector1.Scalar2 - mv1.KVector4.Scalar1345 * mv2.KVector1.Scalar4;
                tempScalar[22] += mv1.KVector4.Scalar1235 * mv2.KVector1.Scalar1 - mv1.KVector4.Scalar2345 * mv2.KVector1.Scalar4;
                tempScalar[25] += mv1.KVector4.Scalar1245 * mv2.KVector1.Scalar2 + mv1.KVector4.Scalar1345 * mv2.KVector1.Scalar3;
                tempScalar[26] += mv1.KVector4.Scalar1245 * mv2.KVector1.Scalar1 + mv1.KVector4.Scalar2345 * mv2.KVector1.Scalar3;
                tempScalar[28] += mv1.KVector4.Scalar1345 * mv2.KVector1.Scalar1 - mv1.KVector4.Scalar2345 * mv2.KVector1.Scalar2;
            }
            
            if (!mv2.KVector2.IsZero())
            {
                tempScalar[15] += -mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar45 + mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar35 - mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar25 + mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar15;
                tempScalar[23] += mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar45 - mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar34 + mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar24 - mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar14;
                tempScalar[27] += -mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar35 + mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar34 - mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar23 + mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar13;
                tempScalar[29] += mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar25 - mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar24 + mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar23 - mv1.KVector4.Scalar2345 * mv2.KVector2.Scalar12;
                tempScalar[30] += mv1.KVector4.Scalar1234 * mv2.KVector2.Scalar15 - mv1.KVector4.Scalar1235 * mv2.KVector2.Scalar14 + mv1.KVector4.Scalar1245 * mv2.KVector2.Scalar13 - mv1.KVector4.Scalar1345 * mv2.KVector2.Scalar12;
            }
            
            if (!mv2.KVector3.IsZero())
            {
                tempScalar[1] += -mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar234 - mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar235 - mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar245 - mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar345;
                tempScalar[2] += -mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar134 - mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar135 - mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar145 - mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar345;
                tempScalar[4] += mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar124 + mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar125 - mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar145 + mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar245;
                tempScalar[8] += -mv1.KVector4.Scalar1234 * mv2.KVector3.Scalar123 + mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar125 + mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar135 - mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar235;
                tempScalar[16] += -mv1.KVector4.Scalar1235 * mv2.KVector3.Scalar123 - mv1.KVector4.Scalar1245 * mv2.KVector3.Scalar124 - mv1.KVector4.Scalar1345 * mv2.KVector3.Scalar134 + mv1.KVector4.Scalar2345 * mv2.KVector3.Scalar234;
            }
            
            if (!mv2.KVector4.IsZero())
            {
                tempScalar[3] += mv1.KVector4.Scalar1345 * mv2.KVector4.Scalar2345 - mv1.KVector4.Scalar2345 * mv2.KVector4.Scalar1345;
                tempScalar[5] += -mv1.KVector4.Scalar1245 * mv2.KVector4.Scalar2345 + mv1.KVector4.Scalar2345 * mv2.KVector4.Scalar1245;
                tempScalar[6] += -mv1.KVector4.Scalar1245 * mv2.KVector4.Scalar1345 + mv1.KVector4.Scalar1345 * mv2.KVector4.Scalar1245;
                tempScalar[9] += mv1.KVector4.Scalar1235 * mv2.KVector4.Scalar2345 - mv1.KVector4.Scalar2345 * mv2.KVector4.Scalar1235;
                tempScalar[10] += mv1.KVector4.Scalar1235 * mv2.KVector4.Scalar1345 - mv1.KVector4.Scalar1345 * mv2.KVector4.Scalar1235;
                tempScalar[12] += -mv1.KVector4.Scalar1235 * mv2.KVector4.Scalar1245 + mv1.KVector4.Scalar1245 * mv2.KVector4.Scalar1235;
                tempScalar[17] += -mv1.KVector4.Scalar1234 * mv2.KVector4.Scalar2345 + mv1.KVector4.Scalar2345 * mv2.KVector4.Scalar1234;
                tempScalar[18] += -mv1.KVector4.Scalar1234 * mv2.KVector4.Scalar1345 + mv1.KVector4.Scalar1345 * mv2.KVector4.Scalar1234;
                tempScalar[20] += mv1.KVector4.Scalar1234 * mv2.KVector4.Scalar1245 - mv1.KVector4.Scalar1245 * mv2.KVector4.Scalar1234;
                tempScalar[24] += -mv1.KVector4.Scalar1234 * mv2.KVector4.Scalar1235 + mv1.KVector4.Scalar1235 * mv2.KVector4.Scalar1234;
            }
            
        }
        
        return Ga41Multivector.Create(tempScalar);
    }
    
}
